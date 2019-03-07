namespace Net.Share
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Sockets;
    using System.Reflection;
    using System.Text;
    using System.Threading;
    using ThreadState = System.Threading.ThreadState;

    /// <summary>
    /// 网络消息核心类 , 此类负责发送消息 , 接收消息 , 远程过程调用函数 , 系列化 , 反序列化
    /// </summary>
    public struct NetMsg
    {
        /// <summary>
        /// 封包结尾 每个封包的结尾都要有这个修饰符 , 避免少包 , 叠包等BUG
        /// </summary>
        public static string Ending { get; } = "<END>";
        /// <summary> 
        /// 函数名 和 参数 与 命令之间的拆分行
        /// </summary>
        public static string Row { get; set; } = "</R>";
        /// <summary>
        /// 参数类型 和 参数值 之间的拆分修饰符
        /// </summary>
        public static string Space { get; set; } = "</S>";
        /// <summary>
        /// 数组类型 与 数组值 之间的拆分修饰符
        /// </summary>
        public static string ArraySpace { get; set; } = "</AS>";
        /// <summary>
        /// 数组元素的拆分行修饰符
        /// </summary>
        public static string EndArray { get; set; } = "</EA>";
        /// <summary>
        /// 参数为复杂类时 拆分对象行修饰符
        /// </summary>
        public static string ObjRow { get; set; } = "</OR>";
        /// <summary>
        /// 参数为复杂类时 拆分对象字段间隔或类名间隔
        /// </summary>
        public static string ObjSpace { get; set; } = "</OS>";

        /// <summary>
        /// 序列号带有命令的函数
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="funName"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public static string SerializeFun(NetCmd cmd, string funName, params object[] pars)
        {
            string str = "";
            if (cmd != NetCmd.CallRpc)
            {
                str = cmd.GetHashCode().ToString() + Row;
            }
            return GetFunToString(str + funName, pars) + Ending;
        }

        /// <summary>
        /// 系列化函数参数 ( funName:函数名称 , pars:参数集合 )
        /// </summary>
        public static byte[] GetFunToBytes(string funName, params object[] pars)
        {
            return Encoding.Unicode.GetBytes(GetFunToString(funName, pars));
        }

        /// <summary>
        /// 系列化函数参数 ( funName:函数名称 , pars:参数集合 )
        /// </summary>
        public static string GetFunToString(string funName, params object[] pars)
        {
            StringBuilder builder = new StringBuilder(funName + Row);
            if (pars==null) {
                return builder.ToString();
            }
            foreach (object obj2 in pars)
            {
                Type type = obj2.GetType();
                if (type.IsPrimitive | type.FullName == "System.String" | type.IsEnum | 
                    type.FullName.Contains("UnityEngine.Vector") | 
                    type.FullName == "UnityEngine.Rect" | 
                    type.FullName == "UnityEngine.Color" | 
                    type.FullName == "UnityEngine.Quaternion")
                {
                    builder.Append(type.FullName + Space + obj2 + Row);
                }
                else if (type.IsArray)
                {
                    builder.Append(type.FullName + Space);
                    foreach (object obj3 in (Array) obj2)
                    {
                        builder.Append(obj3.GetType().FullName + ArraySpace + obj3 + EndArray);
                    }
                    builder.Append(Row);
                }
                else
                {
                    builder.Append(SerializeFieldToString(obj2));
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// 系列化对象字段转字符串
        /// </summary>
        public static string SerializeField(object obj) => SerializeFieldToString(obj);

        /// <summary>
        /// 系列化对象字段转字符串
        /// </summary>
        public static string SerializeFieldToString(object obj)
        {
            Type type = obj.GetType();
            StringBuilder builder = new StringBuilder(type.FullName + Space);
            foreach (FieldInfo info in type.GetFields())
            {
                try
                {
                    object obj2 = info.GetValue(obj);
                    if (info.FieldType.IsArray)
                    {
                        builder.Append(info.Name + ObjSpace);
                        foreach (object obj3 in (Array)obj2)
                        {
                            builder.Append(obj3.GetType().FullName + ArraySpace + obj3 + EndArray);
                        }
                        builder.Append(ObjRow);
                    }
                    else
                    {
                        builder.Append(info.Name + ObjSpace + info.FieldType.FullName + ObjSpace + obj2 + ObjRow );
                    }
                }
                catch
                {
                }
            }
            return builder.Append(Row).ToString();
        }

        /// <summary>
        /// 反序列化调用函数 ( target:要调用的对象 , funData:函数数据 )
        /// </summary>
        public static void CallFun(object target, string[] funData)
        {
            CallFun(target, funData, 0, funData.Length);
        }

        /// <summary>
        /// 反序列化调用函数 ( target:要调用的对象 , funData:函数数据 )
        /// </summary>
        public static void CallFun(object target, string funData)
        {
            string[] fun = funData.Split(new string[] { Row },StringSplitOptions.RemoveEmptyEntries);
            CallFun(target, fun, 0, fun.Length);
        }

        /// <summary>
        /// 反序列化调用函数 ( target:要调用的对象 , funData:函数数据 , offset:函数名位置偏移 )
        /// </summary>
        public static void CallFun(object target, byte[] funData, int offset)
        {
            string buffer = Encoding.Unicode.GetString(funData);
            string[] fun = buffer.Split(new string[] { Row },StringSplitOptions.RemoveEmptyEntries);
            CallFun(target, fun, offset, fun.Length);
        }

        /// <summary>
        /// 反序列化调用函数 ( target:要调用的对象 , funData:函数数据 , offset:函数名位置偏移 )
        /// </summary>
        public static void CallFun(object target, string[] funData, int offset)
        {
            CallFun(target, funData, offset, funData.Length);
        }

        /// <summary>
        /// 反序列化调用函数 ( target:要调用的对象 , funData:函数数据 , offset:函数名位置偏移 )
        /// </summary>
        public static void CallFun(object target, string funData, int offset)
        {
            string[] fun = funData.Split(new string[] { Row }, StringSplitOptions.RemoveEmptyEntries);
            CallFun(target, fun, offset, fun.Length);
        }

        /// <summary>
        /// 反序列化调用函数 ( target:要调用的对象 , funData:函数数据 , offset:函数名位置偏移 , count:函数参数与函数名的总长度 )
        /// </summary>
        public static void CallFun(object target, byte[] funData, int offset, int count)
        {
            string buffer = Encoding.Unicode.GetString(funData);
            string[] fun = buffer.Split(new string[] { Row }, StringSplitOptions.RemoveEmptyEntries);
            CallFun(target, fun, offset, count);
        }

        /// <summary>
        /// 反序列化调用函数 ( target:要调用的对象 , funData:函数数据 , offset:函数名位置偏移 , count:函数参数与函数名的总长度 )
        /// </summary>
        public static void CallFun(object target, string funData, int offset, int count)
        {
            string[] fun = funData.Split(new string[] { Row }, StringSplitOptions.RemoveEmptyEntries);
            CallFun(target, fun, offset, count);
        }

        /// <summary>
        /// 反序列化调用函数 ( target:要调用的对象 , funData:函数数据 , offset:函数名位置偏移 , count:函数参数与函数名的总长度 )
        /// </summary>
        public static void CallFun(object target, string[] funData, int offset, int count)
        {
            try
            {
                var method = target.GetType().GetMethod(funData[offset]);
                var pars = GetFunParams(funData, offset, count);
                method.Invoke(target, pars);
            }
            catch (Exception exception)
            {
                Debug.WriteLine("远程调用失败:" + exception);
            }
        }

        /// <summary>
        /// 反序列化调用方法
        /// </summary>
        /// <param name="Delegate">方法委托</param>
        /// <param name="funParamsData">函数参数数据</param>
        public static void CallMethod(NetDelegate Delegate, string funParamsData)
        {
            Delegate.method.Invoke(Delegate.target, GetFunParams(funParamsData));
        }

        /// <summary>
        /// 反序列化调用方法
        /// </summary>
        /// <param name="Delegate">方法委托</param>
        /// <param name="funData">函数数据</param>
        public static void CallMethod(NetDelegate Delegate, string[] funData)
        {
            Delegate.method.Invoke(Delegate.target, GetFunParams(funData, 0, funData.Length));
        }

        /// <summary>
        /// 反序列化调用方法
        /// </summary>
        /// <param name="Delegate">方法委托</param>
        /// <param name="funData">函数数据</param>
        /// <param name="offset">函数名索引</param>
        public static void CallMethod(NetDelegate Delegate, string[] funData, int offset)
        {
            Delegate.method.Invoke(Delegate.target, GetFunParams(funData, offset, funData.Length));
        }

        /// <summary>
        /// 反序列化调用方法
        /// </summary>
        /// <param name="Delegate">方法委托</param>
        /// <param name="funData">函数数据</param>
        /// <param name="offset">函数名索引</param>
        /// <param name="count">函数结束索引</param>
        public static void CallMethod(NetDelegate Delegate, string[] funData, int offset, int count)
        {
            Delegate.method.Invoke(Delegate.target, GetFunParams(funData, offset, count));
        }

        /// <summary>
        /// 反序列化函数参数 ( funData:系列化函数数据 )
        /// </summary>
        public static object[] GetFunParams(string funData) => GetFunParams(funData, 0);

        /// <summary>
        /// 反序列化函数参数 ( funData:系列化函数数据 , offset:函数名位置偏移 )
        /// </summary>
        public static object[] GetFunParams(string funData, int offset)
        {
            string[] fun = funData.Split(new string[] { Row },StringSplitOptions.RemoveEmptyEntries);
            return GetFunParams(fun, offset, fun.Length);
        }

        /// <summary>
        /// 反序列化函数参数 ( funData:系列化函数数据 , offset:函数名位置偏移 , count:函数参数加函数名总长度 )
        /// </summary>
        public static object[] GetFunParams(string funData, int offset, int count)
        {
            string[] fun = funData.Split(new string[] { Row }, StringSplitOptions.RemoveEmptyEntries);
            return GetFunParams(fun, offset, count);
        }

        /// <summary>
        /// 反序列化函数参数 ( funData:系列化函数数据数组 , offset:函数名位置偏移 , count:函数参数加函数名总长度 )
        /// </summary>
        public static object[] GetFunParams(string[] funData, int offset, int count)
        {
            object[] objArray = new object[count - offset - 1];
            for (int i = offset + 1; i < count; i++)
            {
                try
                {
                    string[] strArray = funData[i].Split(new string[]{Space},StringSplitOptions.RemoveEmptyEntries);
                    Type elementType = GetType(strArray[0]);
                    if (strArray[0].Contains("[]"))
                    {
                        string[] strArray2 = strArray[1].Split(new string[]{EndArray},StringSplitOptions.RemoveEmptyEntries);
                        Array array = Array.CreateInstance(elementType, strArray2.Length);
                        for (int j = 0; j < strArray2.Length; j++)
                        {
                            string[] strArray3 = strArray2[j].Split(new string[]{ArraySpace},StringSplitOptions.RemoveEmptyEntries);
                            object obj2 = NetConvert.StringToValue(strArray3[0], strArray3[1]);
                            array.SetValue(obj2, j);
                        }
                        objArray[i - (offset + 1)] = array;
                    }
                    else if (elementType.IsPrimitive |
                        elementType.FullName.Contains("UnityEngine.Vector") |
                        elementType.FullName == "System.String" | 
                        elementType.FullName == "UnityEngine.Rect" | 
                        elementType.FullName == "UnityEngine.Color" | 
                        elementType.FullName == "UnityEngine.Quaternion")
                    {
                        objArray[i - (offset + 1)] = NetConvert.StringToValue(strArray[0], strArray[1]);
                    }
                    else if (elementType.IsEnum)
                    {
                        objArray[i - (offset + 1)] = Enum.Parse(elementType, strArray[1]);
                    }
                    else
                    {
                        objArray[i - (offset + 1)] = DeserializeToField(funData[i]);
                    }
                }
                catch (Exception exception)
                {
                    Debug.WriteLine($"反序列化参数{funData[i]}失败:{exception}");
                }
            }
            return objArray;
        }

        /// <summary>
        /// 反序列化类型
        /// </summary>
        public static Type GetType(string typeName)
        {
            typeName = typeName.Replace("&", "");
            typeName = typeName.Replace("*", "");
            typeName = typeName.Replace("[]", "");
            Type type = Type.GetType(typeName);
            if (type != null)
            {
                return type;
            }
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = assembly.GetType(typeName);
                if (type != null)
                {
                    return type;
                }
            }
            return null;
        }

        /// <summary>
        /// 反序列化字段( data:系列化对象数据 , splitField:拆分字段行 , splitFieldPart:拆分字段间隔部分 , splitFieldArray:拆分字段数组行 , splitFieldArray1:拆分字段数组间隔部分 )
        /// </summary>
        public static object DeserializeToField(string data )
        {
            string[] strArray = data.Split(new string[] { Space }, StringSplitOptions.RemoveEmptyEntries);
            Type type = GetType(strArray[0]);
            object obj2 = type.Assembly.CreateInstance(strArray[0]);
            string[] strArray1 = strArray[1].Split(new string[] { ObjRow }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in strArray1)
            {
                try
                {
                    string[] strArray2 = str.Split(new string[] { ObjSpace }, StringSplitOptions.RemoveEmptyEntries);
                    FieldInfo field = type.GetField(strArray2[0]);
                    if (field.FieldType.IsArray)
                    {
                        string[] strArray3 = strArray2[1].Split(new string[] { EndArray }, StringSplitOptions.RemoveEmptyEntries);
                        Array array = Array.CreateInstance(GetType(field.FieldType.FullName), strArray3.Length);
                        for (int j = 0; j < strArray3.Length; j++)
                        {
                            string[] strArray4 = strArray3[j].Split(new string[] { ArraySpace }, StringSplitOptions.RemoveEmptyEntries);
                            object obj3 = NetConvert.StringToValue(strArray4[0], strArray4[1]);
                            array.SetValue(obj3, j);
                        }
                        field.SetValue(obj2, array);
                    }
                    else
                    {
                        field.SetValue(obj2, NetConvert.StringToValue(strArray2[1], strArray2[2]));
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"反序列化字段{str}异常:{e}");
                }
            }
            return obj2;
        }
    }
}
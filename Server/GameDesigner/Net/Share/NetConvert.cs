﻿using System;
using UnityEngine;

namespace Net.Share
{
    /// <summary>
    /// 网络转换类
    /// </summary>
    [Serializable]
    public partial struct NetConvert
    {
        /// <summary>
        /// 字符串转系统基础类型 ( type 给定类型名称 , value 转换这个字符串为type类型的值 )
        /// </summary>
        static public object StringToValue(string type = "Int32", string value = "0")
        {
            switch (type)
            {
                case "System.Int32":
                    return Convert.ToInt32(value);
                case "System.Single":
                    return Convert.ToSingle(value);
                case "System.String":
                    return value;
                case "System.Boolean":
                    return Convert.ToBoolean(value);
                case "System.Char":
                    return Convert.ToChar(value);
                case "System.Int16":
                    return Convert.ToInt16(value);
                case "System.Int64":
                    return Convert.ToInt64(value);
                case "System.UInt16":
                    return Convert.ToUInt16(value);
                case "System.UInt32":
                    return Convert.ToUInt32(value);
                case "System.UInt64":
                    return Convert.ToUInt64(value);
                case "System.Double":
                    return Convert.ToDouble(value);
                case "System.Byte":
                    return Convert.ToByte(value);
                case "System.SByte":
                    return Convert.ToSByte(value);
                case "UnityEngine.Vector2":
                    return ToVector2_3_4(type, value);
                case "UnityEngine.Vector3":
                    return ToVector2_3_4(type, value);
                case "UnityEngine.Vector4":
                    return ToVector2_3_4(type, value);
                case "UnityEngine.Rect":
                    return ToRect(type, value);
                case "UnityEngine.Color":
                    return ToColor(type, value);
                case "UnityEngine.Quaternion":
                    return ToQuaternion(type, value);
            }
            return null;
        }

        /// <summary>
        /// 字符转三维向量值 ( type 以字符形式给定一个类型 , value 类型的值 注：类型的值必须以给定类型的值符合才能转换成功 )
        /// </summary>
        static public object ToVector2_3_4(string type = "UnityEngine.Vector3", string value = "( 0.1 , 0.5 , 1 )")
        {
            return To_Vector234_Rect_Quaternion_Color(type, value);
        }

        /// <summary>
        /// 字符转矩形值 ( type 以字符形式给定一个类型 , value 类型的值 注：类型的值必须以给定类型的值符合才能转换成功 )
        /// </summary>
        static public object ToRect(string type = "UnityEngine.Rect", string value = "( 0.1 , 0.5 , 1 , 1 )")
        {
            return To_Vector234_Rect_Quaternion_Color(type, value);
        }

        /// <summary>
        /// 字符转颜色值 ( type 以字符形式给定一个类型 , value 类型的值 注：类型的值必须以给定类型的值符合才能转换成功 )
        /// </summary>
        static public object ToColor(string type = "UnityEngine.Color", string value = "( 0.1 , 0.5 , 1 , 1 )")
        {
            return To_Vector234_Rect_Quaternion_Color(type, value);
        }

        /// <summary>
        /// 字符转欧拉角值 ( type 以字符形式给定一个类型 , value 类型的值 注：类型的值必须以给定类型的值符合才能转换成功 )
        /// </summary>
        static public object ToQuaternion(string type = "UnityEngine.Quaternion", string value = "( 0.1 , 0.5 , 1 , 1 )")
        {
            return To_Vector234_Rect_Quaternion_Color(type, value);
        }

        /// <summary>
        /// 转换字符为Vector2 或 Vector3 或 Vector4 或 Rect 或 Quaternion 的值 ( type 以字符形式给定一个类型 , value 类型的值 注：类型的值必须以给定类型的值符合才能转换成功 )
        /// </summary>
        static public object To_Vector234_Rect_Quaternion_Color(string type = "UnityEngine.Vector3", string value = "( 0.1 , 0.5 , 1 )")
        {
            value = value.Trim();
            value = value.TrimStart('(');
            value = value.TrimEnd(')');
            string[] array = value.Split(',');

            if (array.Length == 2 & type == "UnityEngine.Vector2")
                return new Vector2(Convert.ToSingle(array[0]), Convert.ToSingle(array[1]));
            if (array.Length == 3 & type == "UnityEngine.Vector3")
                return new Vector3(Convert.ToSingle(array[0]), Convert.ToSingle(array[1]), Convert.ToSingle(array[2]));
            if (array.Length == 4 & type == "UnityEngine.Vector4")
                return new Vector4(Convert.ToSingle(array[0]), Convert.ToSingle(array[1]), Convert.ToSingle(array[2]), Convert.ToSingle(array[3]));
            if (array.Length == 4 & type == "UnityEngine.Rect")
                return new Rect(Convert.ToSingle(array[0].Replace("x:", "")), Convert.ToSingle(array[1].Replace("y:", "")), Convert.ToSingle(array[2].Replace("width:", "")), Convert.ToSingle(array[3].Replace("height:", "")));
            if (array.Length == 4 & type == "UnityEngine.Quaternion")
                return new Quaternion(Convert.ToSingle(array[0]), Convert.ToSingle(array[1]), Convert.ToSingle(array[2]), Convert.ToSingle(array[3]));
            if (array.Length == 4 & type == "UnityEngine.Color")
                return new Color(Convert.ToSingle(array[0].Replace("RGBA(", "")), Convert.ToSingle(array[1]), Convert.ToSingle(array[2]), Convert.ToSingle(array[3]));
            return null;
        }
    }
}

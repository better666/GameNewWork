namespace Net.Client
{
    using Net.Share;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using UnityEngine;

    /// <summary>
    /// 网络行为，此类负责网络增删RPC远程过程调用函数，使用到网络通讯功能，需要继承此类
    /// </summary>
    public abstract class NetBehaviour : MonoBehaviour
    {
        /// <summary>
        /// 远程过程调用委托集合
        /// </summary>
        protected List<NetDelegate> RPCFuns = new List<NetDelegate>();

        /// <summary>
        /// 添加远程过程调用函数的委托
        /// </summary>
        /// <param name="target">远程过程调用指定的对象</param>
        public static void AddRPCDelegate(object target)
        {
            foreach (MethodInfo info in target.GetType().GetMethods())
            {
                Attribute[] customAttributes = info.GetCustomAttributes(false) as Attribute[];
                foreach (Attribute attribute in customAttributes)
                {
                    if (attribute is RPCFun)
                    {
                        NetDelegate item = new NetDelegate(target, info);
                        NetClient.Instance.RPCs.Add(item);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 添加远程过程调用函数的委托
        /// </summary>
        /// <param name="target">远程过程调用指定的对象</param>
        /// <param name="RPCFunctions">远程过程调用函数的集合</param>
        public static void AddRPCDelegate(object target, List<NetDelegate> RPCFunctions)
        {
            foreach (MethodInfo info in target.GetType().GetMethods())
            {
                Attribute[] customAttributes = info.GetCustomAttributes(false) as Attribute[];
                foreach (Attribute attribute in customAttributes)
                {
                    if (attribute is RPCFun)
                    {
                        NetDelegate item = new NetDelegate(target, info);
                        NetClient.Instance.RPCs.Add(item);
                        RPCFunctions.Add(item);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 当派生类被销毁后，RPC也会被移除
        /// </summary>
        public virtual void OnDestroy()
        {
            for (int i = 0; i < RPCFuns.Count; i++)
            {
                for (int j = 0; j < NetClient.Instance.RPCs.Count; j++)
                {
                    if (NetClient.Instance.RPCs[j].target == RPCFuns[i].target)
                    {
                        NetClient.Instance.RPCs.RemoveAt(j);
                    }
                }
            }
        }

        /// <summary>
        /// 移除RPC函数
        /// </summary>
        /// <param name="RPCFunctions">要移除的RPC函数</param>
        public static void RemoveRPCDelegate(List<NetDelegate> RPCFunctions)
        {
            for (int i = 0; i < RPCFunctions.Count; i++)
            {
                for (int j = 0; j < NetClient.Instance.RPCs.Count; j++)
                {
                    if (NetClient.Instance.RPCs[j].target == RPCFunctions[i].target)
                    {
                        NetClient.Instance.RPCs.RemoveAt(j);
                    }
                }
            }
        }

        /// <summary>
        /// 移除RPC函数
        /// </summary>
        /// <param name="target">将此对象的所有带有RPC特性的函数移除</param>
        public static void RemoveRPCDelegate(object target)
        {
            MethodInfo[] methods = target.GetType().GetMethods();
            List<NetDelegate> list = new List<NetDelegate>();
            foreach (MethodInfo info in methods)
            {
                Attribute[] customAttributes = info.GetCustomAttributes(false) as Attribute[];
                foreach (Attribute attribute in customAttributes)
                {
                    if (attribute is RPCFun)
                    {
                        NetDelegate item = new NetDelegate(target, info);
                        list.Add(item);
                        break;
                    }
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < NetClient.Instance.RPCs.Count; j++)
                {
                    if (NetClient.Instance.RPCs[j].target == list[i].target)
                    {
                        NetClient.Instance.RPCs.RemoveAt(j);
                    }
                }
            }
        }

        /// <summary>
        /// 当游戏开始初始化此对象时，搜集次类的使用RPC特性并添加到RPC委托集合变量里
        /// </summary>
        public virtual void Awake()
        {
            foreach (MethodInfo info in GetType().GetMethods())
            {
                Attribute[] customAttributes = info.GetCustomAttributes(false) as Attribute[];
                foreach (Attribute attribute in customAttributes)
                {
                    if (attribute is RPCFun)
                    {
                        NetDelegate item = new NetDelegate(this, info);
                        NetClient.Instance.RPCs.Add(item);
                        RPCFuns.Add(item);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 发送封包数据 - 远程过程调用服务器函数
        /// </summary>
        /// <param name="fun">要调用服务器的函数名</param>
        /// <param name="pars">对应服务器函数的参数</param>
        public void Send(string fun, params object[] pars)
        {
            NetClient.Send(fun, pars);
        }

        /// <summary>
        /// 发送封包数据 - 带有几种命令的远程过程调用函数
        /// </summary>
        /// <param name="cmd">网络命令</param>
        /// <param name="fun">要调用服务器的函数名</param>
        /// <param name="pars">服务器的函数参数</param>
        public void Send(NetCmd cmd, string fun, params object[] pars)
        {
            NetClient.Send(cmd, fun, pars);
        }
    }
}


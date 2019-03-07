using Net.Share;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Net.Server
{
    /// <summary>
    /// 服务器网络行为
    /// </summary>
    public abstract class NetBehaviour
    {
        public NetBehaviour() { }

        /// <summary>
        /// 添加带有RPC特性的所有方法
        /// </summary>
        public static void AddRPCs()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.FriendlyName;
            var exportedTypes = Assembly.LoadFile(path).GetExportedTypes();
            foreach (var type in exportedTypes)
            {
                if (type.BaseType == typeof(NetBehaviour) | type.BaseType == typeof(NetServer))
                {
                    var target = Activator.CreateInstance(type);
                    foreach (MethodInfo info in target.GetType().GetMethods())
                    {
                        if (info.GetCustomAttribute<RPCFun>() != null)
                        {
                            NetServer.RPCs.Add(new NetDelegate(target, info));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 添加所有带有RPC特性的方法
        /// </summary>
        /// <param name="target"></param>
        public static void AddRPCs(object target)
        {
            foreach (MethodInfo info in target.GetType().GetMethods())
            {
                if (info.GetCustomAttribute<RPCFun>() != null)
                {
                    NetServer.RPCs.Add(new NetDelegate(target, info));
                }
            }
        }

        /// <summary>
        /// 获取带有RPC特性的所有方法
        /// </summary>
        /// <param name="target">要获取RPC特性的对象</param>
        /// <returns>返回获取到带有RPC特性的所有公开方法</returns>
        public static List<NetDelegate> GetRPCs(object target)
        {
            List<NetDelegate> RPCs = new List<NetDelegate>();
            foreach (MethodInfo info in target.GetType().GetMethods())
            {
                if (info.GetCustomAttribute<RPCFun>() != null)
                {
                    RPCs.Add(new NetDelegate(target, info));
                }
            }
            return RPCs;
        }

        /// <summary>
        /// 发送封包数据 - 远程过程调用服务器函数
        /// </summary>
        /// <param name="client">发送到的客户端</param>
        /// <param name="fun">要调用服务器的函数名</param>
        /// <param name="pars">对应服务器函数的参数</param>
        public static void Send(NetPlayer client, string fun, params object[] pars)
        {
            NetServer.Send(client,fun,pars);
        }

        /// <summary>
        /// 发送封包数据 - 带有几种命令的远程过程调用函数
        /// </summary>
        /// <param name="client">发送到的客户端</param>
        /// <param name="cmd">网络命令</param>
        /// <param name="fun">要调用服务器的函数名</param>
        /// <param name="pars">服务器的函数参数</param>
        public static void Send(NetPlayer client, NetCmd cmd, string fun, params object[] pars)
        {
            NetServer.Send(client, cmd, fun, pars);
        }
    }
}
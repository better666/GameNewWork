namespace Net.Server
{
    using Net.Share;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Sockets;
    using System.Reflection;
    using System.Text;
    using System.Threading;

    /// <summary>
    /// 网络核心服务器类
    /// </summary>
    public abstract class NetServer
    {
        /// <summary>
        /// 客户端玩家,每当调用服务器函数这个对象就会被赋值为调用方客户端的对象
        /// </summary>
        public static NetPlayer client;
        /// <summary>
        /// 客户端连接集合
        /// </summary>
        public static Dictionary<EndPoint, NetPlayer> clients = new Dictionary<EndPoint, NetPlayer>();
        /// <summary>
        /// 玩家所在的场景 - 当两个以上的玩家在一个场景时会添加他们到一个组中，可以避免群发效率
        /// </summary>
        public static Dictionary<int, NetScene> scenes = new Dictionary<int, NetScene>();
        /// <summary>
        /// 服务器套接字
        /// </summary>
        public static Socket server;
        /// <summary>
        /// 进站密码许可证
        /// </summary>
        public static string inboubdPassword = "2566698989";
        /// <summary>
        /// 服务器接收容量10k
        /// </summary>
        private readonly byte[] buffer = new byte[10240];
        /// <summary>
        /// 远程过程调用委托
        /// </summary>
        public static List<NetDelegate> RPCs = new List<NetDelegate>();

        public NetServer() { }

        /// <summary>
        /// 运行服务器
        /// </summary>
        public void Run() => Start();

        /// <summary>
        /// 开始运行服务器
        /// </summary>
        /// <param name="port">服务器端口</param>
        public void Start(int port = 666)
        {
            if (server == null)//如果服务器套接字已创建
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
                                RPCs.Add(new NetDelegate(target, info));
                            }
                        }
                    }
                }
                IPEndPoint localEP = new IPEndPoint(IPAddress.Any, port);//IP端口设置
                server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);//创建套接字
                server.Bind(localEP);//绑定IP端口
                Thread thread1 = new Thread(HeartbeatUpdate)
                {//创建心跳包线程
                    IsBackground = true
                };
                thread1.Start();
                Thread thread2 = new Thread(UpdateServer)
                {//创建服务器与客户端的每一帧通信线程
                    IsBackground = true
                };
                thread2.Start();
                OnStarting();
                scenes.Add(0, new NetScene(2000));
            }
        }

        /// <summary>
        /// 服务器启动成功调用
        /// </summary>
        public virtual void OnStarting() { }

        /// <summary>
        /// 心跳包线程,每3秒发送一次封包
        /// </summary>
        public virtual void HeartbeatUpdate()
        {
            while (true)
            {
                Thread.Sleep(5000);
                try
                {
                    List<EndPoint> keys = new List<EndPoint>();
                    foreach (var client in clients)//遍历所有在线玩家进行发送心跳包
                    {
                        if (!client.Value.Heart)
                        {//如果在5秒内客户端没有发来心跳数据则执行掉线处理
                            keys.Add(client.Key);
                        }
                        else
                        {
                            client.Value.Heart = false;//处理下一次心跳包
                        }
                    }
                    foreach (var key in keys)
                    {
                        clients.Remove(key);//移除玩家
                    }
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// 接收客户端心跳包
        /// </summary>
        [RPCFun]
        public void Heartbeat()
        {
            client.Heart = true;
            Send(client, "Heartbeat");
        }

        private string stringBuilder = string.Empty;
        private bool Empty = true;

        /// <summary>
        /// 服务器每一帧与客户端通讯线程
        /// </summary>
        private void UpdateServer()
        {
            while (true)
            {
                Thread.Sleep(1);//避免CPU暴走
                try
                {
                    EndPoint remoteEP = server.LocalEndPoint;
                    int count = server.ReceiveFrom(buffer, ref remoteEP);
                    if (clients.ContainsKey(remoteEP))
                    {
                        ReceiveBuffer(clients[remoteEP], buffer, count);//处理缓冲区数据
                    }
                    else
                    {
                        //为了阻止黑客频繁发送数据攻击服务器主机 需要提供入站密码
                        OnHasConnect(remoteEP, buffer, count);//当有客户端连接调用一次虚函数
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"主线程异常:{e}");
                }
            }
        }

        /// <summary>
        /// 当有新的客户端连接时调用
        /// </summary>
        public virtual void OnHasConnect(EndPoint remoteEP, byte[] buffer, int count)
        {
            var str = Encoding.Unicode.GetString(buffer,0, count);
            if (str == inboubdPassword)//进站密码
            {
                NetPlayer netPlayer = new NetPlayer(remoteEP);//创建网络客户端对象
                clients.Add(remoteEP, netPlayer);//将网络玩家添加到集合中
                scenes[0].players.Add(netPlayer);//将网络玩家添加到主场景集合中
                server.SendTo(buffer, count, 0, remoteEP);//发送数据到这个客户端
            }
        }

        /// <summary>
        /// 处理缓冲区数据
        /// </summary>
        /// <param name="client"></param>
        /// <param name="buffer"></param>
        /// <param name="dataLength"></param>
        private void ReceiveBuffer(NetPlayer client, byte[] buffer, int dataLength)
        {
            if (buffer[0] == 49 & buffer[1] == 0)
            {
                server.SendTo(buffer, dataLength, 0, client.remotePoint);//发送数据到这个客户端
            }
            else if (buffer[0] == 50 & buffer[1] == 0)
            {
                if (scenes.ContainsKey(client.sceneIndex))
                {
                    for (int i = 0; i < scenes[client.sceneIndex].players.Count; i++)//遍历当前场景的客户端
                    {
                        server.SendTo(buffer, dataLength, 0, scenes[client.sceneIndex].players[i].remotePoint);//发送数据到这个客户端
                    }
                }
                else
                {
                    server.SendTo(buffer, dataLength, 0, client.remotePoint);//发送数据到这个客户端
                }
            }
            else if (buffer[0] == 51 & buffer[1] == 0)//如果数据包含此字符串，则转发给所有在线的玩家
            {
                foreach (var myClient in clients)//遍历所有在线玩家
                {
                    server.SendTo(buffer, dataLength, 0, myClient.Key);//发送给所有在线的玩家
                }
            }
            else//如果数据包没有命令，也就是空的字符串，则调用服务器函数
            {
                NetServer.client = client;//给受保护字段赋值，可以在任何地方进行引用
                ResolveBuffer(buffer, dataLength);
            }
        }

        /// <summary>
        /// 解析网络数据包
        /// </summary>
        /// <param name="buffer">网络数据</param>
        /// <param name="dataLength">数据长度</param>
        public void ResolveBuffer(byte[] buffer, int dataLength)
        {
            if (Empty)//如果数据包完整
            {
                stringBuilder = Encoding.Unicode.GetString(buffer, 0, dataLength);
            }
            else//数据包不完整则追加数据到当前缓存器中
            {
                stringBuilder += Encoding.Unicode.GetString(buffer, 0, dataLength);
                Empty = true;
            }
            string[] separator = new string[] { "D>" };
            string[] strArray = stringBuilder.Split(separator, StringSplitOptions.RemoveEmptyEntries);//拆分数据包
            for (int i = 0; i < strArray.Length; i++)//遍历数据包数量
            {
                if (strArray[i].Contains("<EN"))
                {//如果数据包包含<EN则说明是完整包 完整包为<END>
                    string newStr = strArray[i].Replace("<EN", "");
                    string[] funData = newStr.Split(new string[] { NetMsg.Row }, StringSplitOptions.RemoveEmptyEntries);//拆分数据封包
                    OnCallFunction(funData);//开始反序列化调用函数
                }
                else
                {//数据包不完整，少包或多包
                    stringBuilder = strArray[i];//将少包或多包放入缓存器中，等待下一次接收它的完整数据包
                    Empty = false;
                }
            }
        }

        /// <summary>
        /// 开始调用远程过程函数
        /// </summary>
        /// <param name="funData">要解析的函数数据</param>
        public virtual void OnCallFunction(string[] funData)
        {
            DeserializeCallFunData(funData);//反序列化调用函数
        }

        /// <summary>
        /// 反序列化调用函数 ( target:调用的对象 , funData:函数数据 )
        /// </summary>
        private void DeserializeCallFunData(string[] funData)
        {
            foreach (var rpc in RPCs)
            {
                if (rpc.method.Name == funData[0])
                {
                    object[] parameters = NetMsg.GetFunParams(funData, 0, funData.Length);//反序列化参数
                    rpc.method.Invoke(rpc.target, parameters);
                }
            }
        }

        /// <summary>
        /// 发送封包数据 - 远程过程调用服务器函数
        /// </summary>
        /// <param name="client">发送到的客户端</param>
        /// <param name="fun">要调用服务器的函数名</param>
        /// <param name="pars">对应服务器函数的参数</param>
        public static void Send(NetPlayer client, string fun, params object[] pars)
        {
            string data = NetMsg.SerializeFun(NetCmd.CallRpc, fun, pars);
            byte[] bytes = Encoding.Unicode.GetBytes(data);
            server.SendTo(bytes, bytes.Length, 0, client.remotePoint);
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
            string data = NetMsg.SerializeFun(cmd, fun, pars);
            byte[] bytes = Encoding.Unicode.GetBytes(data);
            server.SendTo(bytes, bytes.Length, 0, client.remotePoint);
        }
    }
}
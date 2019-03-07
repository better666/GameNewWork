namespace Net.Client
{
    using Net.Share;
    using System;
    using System.Collections.Generic;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;
    using UnityEngine;

    /// <summary>
    /// 网络客户端核心虚类
    /// </summary>
    public abstract class NetClient : MonoBehaviour
    {
        /// <summary>
        /// 客户端套接字
        /// </summary>
        public static Socket client;
        /// <summary>
        /// IP地址
        /// </summary>
        public string host = "127.0.0.1";
        /// <summary>
        /// 端口号
        /// </summary>
        public int port = 666;
        /// <summary>
        /// 入站密码
        /// </summary>
        public string inboubdPassword = "2566698989";
        /// <summary>
        /// 发送缓存器
        /// </summary>
        private static List<SendBuffer> sendBuffers = new List<SendBuffer>();
        /// <summary>
        /// 接收缓存器
        /// </summary>
        private List<NetBuffer> buffers = new List<NetBuffer>();
        /// <summary>
        /// 接收数据缓冲区 - 接收容量大小为10k
        /// </summary>
        private byte[] buffer = new byte[10240];
        /// <summary>
        /// 接收到的数据长度
        /// </summary>
        private int dataLength = 0;
        private string stringBuilder = string.Empty; //接收包字符串
        private bool Empty = true;//少包为假 接收到完整包为真
        private int currFrequency = 0;
        private Thread sendThread;
        private Thread revThread;
        private Thread heartThread;
        private static NetClient instance;
        public static NetClient Instance => (instance ?? (instance = FindObjectOfType<NetClient>()));

        /// <summary>
        /// 网络委托函数
        /// </summary>
        /// <returns></returns>
        public List<NetDelegate> RPCs { get; set; } = new List<NetDelegate>();

        /// <summary>
        /// 构造函数
        /// </summary>
        protected NetClient() { }

        /// <summary>
        /// 重写此函数必须要回到基类执行base.Awake();
        /// </summary>
        public virtual void Awake()
        {
            instance = this;
            NetBehaviour.AddRPCDelegate(this);
            Application.runInBackground = true;
        }

        /// <summary>
        /// 重写此函数必须要回到基类执行base.FixedUpdate();
        /// </summary>
        public virtual void FixedUpdate()
        {
            for (int i = 0; i < buffers.Count; ++i)
            {
                try
                {
                    buffers[i].method.Invoke(buffers[i].target, buffers[i].pars);
                }
                catch (Exception e)
                {
                    Debug.Log($"调用异常:{e}");
                }
                buffers.RemoveAt(i);
            }
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        public void Connect()
        {
            Connect(connected => { });
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="connectResult">连接结果</param>
        /// <returns></returns>
        public void Connect(Action<bool> connectResult)
        {
            Connect(host, port, connectResult);
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="host">IP地址</param>
        /// <param name="port">端口号</param>
        /// <param name="connectResult">连接结果</param>
        public void Connect(string host, int port, Action<bool> connectResult)
        {
            if (client == null) //如果套接字为空则说明没有连接上服务器
            {
                ConnectUdp(host, port, result =>
                {
                    Connected(result);
                    connectResult(result);
                });
            }
            else if (!client.Connected)
            {
                client.Close();
                client = null;
                Debug.Log("服务器连接中断!");
                connectResult(false);
            }
            else
            {
                connectResult(true);
            }
        }

        /// <summary>
        /// 连接成功处理
        /// </summary>
        /// <param name="result">结果</param>
        private void Connected(bool result)
        {
            if (result)
            {
                (sendThread = new Thread(SendHandle) { IsBackground = true }).Start();
                (revThread = new Thread(ReceiveHandle) { IsBackground = true }).Start();
                (heartThread = new Thread(HeartbeatHandle) { IsBackground = true }).Start();
                Debug.Log("成功连接服务器...");
                OnConnected();
            }
            else
            {
                client = null;
                Debug.Log("服务器尚未开启或连接IP端口错误!");
            }
        }

        /// <summary>
        /// 连接Udp服务器
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="connectResult"></param>
        private void ConnectUdp(string host, int port, Action<bool> connectResult)
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);//创建套接字
            socket.Connect(host, port);
            byte[] bytes = Encoding.Unicode.GetBytes(inboubdPassword);
            socket.SendTo(bytes, bytes.Length, 0, socket.RemoteEndPoint);
            bool connetEnd = false, result = false;
            try {
                if (!socket.BeginReceive(buffer, 0, buffer.Length, 0, asyncResult => {
                    try {
                        dataLength = socket.EndReceive(asyncResult);
                        client = socket;
                        result = true;
                    } catch {//连接异常则为连接失败
                        result = false;
                    }
                    connetEnd = true;
                }, null).AsyncWaitHandle.WaitOne(5000)) {//连接服务器同时连接时间在5秒内，超时认为连接失败
                    connetEnd = true;
                    result = false;
                }
            } catch {//连接异常则为连接失败
                connetEnd = true;
                result = false;
            }
            while (!connetEnd) {
                Thread.Sleep(1);
            }
            connectResult(result);
        }

        /// <summary>
        /// 连接成功调用一次
        /// </summary>
        public virtual void OnConnected() { }

        /// <summary>
        /// 发包线程
        /// </summary>
        private void SendHandle()
        {
            while (sendThread.ThreadState != ThreadState.Aborted)
            {
                Thread.Sleep(1);
                try {
                    if (client != null) {
                        if (client.Connected) {
                            Send();
                        }
                    }
                } catch { }
            }
        }

        private void Send()
        {
            for (int i = 0; i < sendBuffers.Count; ++i)
            {
                try
                {
                    string data = NetMsg.SerializeFun(sendBuffers[i].netCmd, sendBuffers[i].funName, sendBuffers[i].pars);
                    byte[] bytes = Encoding.Unicode.GetBytes(data);
                    client.SendTo(bytes, bytes.Length, 0, client.RemoteEndPoint);
                }
                finally
                {
                    sendBuffers.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// 后台线程接收数据
        /// </summary>
        private void ReceiveHandle()
        {
            while (revThread.ThreadState != ThreadState.Aborted)
            {
                Thread.Sleep(1);//解决CPU占用过高
                try {
                    if (client == null) {
                        Reconnection(5000, 10);
                    } else if (!client.Connected) {
                        Reconnection(5000, 10);
                    } else {
                        ReceiveData();
                    }
                } catch {}
            }
        }

        /// <summary>
        /// 接收网络数据
        /// </summary>
        private void ReceiveData()
        {
            dataLength = client.Receive(buffer);
            if (Empty) {//如果是清空 则不+= 解决少包问题
                stringBuilder = Encoding.Unicode.GetString(buffer, 0, dataLength);
            } else {//如果数据包不完整 , 则追加接收包
                stringBuilder += Encoding.Unicode.GetString(buffer, 0, dataLength);
            }
            string[] separator = new string[] { "D>" };//拆分网络协议数据包 包尾为<END> 如果没有<END>结尾则说明少包
            string[] strArray = stringBuilder.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            Empty = true;
            for (int i = 0; i < strArray.Length; i++)//遍历封包数据
            {
                if (strArray[i].Contains("<EN")) {//如果数据包包含<EN则为完整包 (<END>)
                    string newStr = strArray[i].Replace("<EN", "");//将<EN去除
                    string[] codeCmd = newStr.Split(new string[] { NetMsg.Row }, StringSplitOptions.RemoveEmptyEntries);
                    CallRPC(codeCmd);//调用网络数据
                } else {//如果数据包没有<EN字符 则 说明少包
                    stringBuilder = strArray[i];//将这个不完整的数据包 存储到变量里 等待下一次接收数据时追加它的完整包
                    Empty = false;//数据包不完整时设置为假 等待下一次接收包就会被追加数据包
                }
            }
        }

        /// <summary>
        /// 调用网络封包数据
        /// </summary>
        /// <param name="buffer">网络数据缓冲</param>
        public void CallRPC(string[] buffer)
        {
            for (int i = 0; i < RPCs.Count; i++)//遍历远程过程调用的函数委托
            {
                try
                {
                    if (buffer[0] == RPCs[i].method.Name) {//如果网络命令等于函数委托名,调用函数委托
                        var pars = NetMsg.GetFunParams(buffer, 0, buffer.Length);
                        buffers.Add(new NetBuffer(RPCs[i].target, RPCs[i].method, pars));
                    }
                    if (buffer.Length < 2) {//如果网络数据不完整,跳过
                        continue;
                    }
                    if (buffer[1] == RPCs[i].method.Name) {//调用带有网络命令的函数
                        var pars = NetMsg.GetFunParams(buffer, 1, buffer.Length);
                        buffers.Add(new NetBuffer(RPCs[i].target, RPCs[i].method, pars));
                    }
                }
                catch
                {
                    if (RPCs[i].target == null | RPCs[i].method == null)
                    {
                        RPCs.RemoveAt(i);
                    }
                }
            }
        }

        /// <summary>
        /// 断线重新连接
        /// </summary>
        /// <param name="interval">重连间隔（毫秒单位）</param>
        /// <param name="maxFrequency">重连最大次数</param>
        private void Reconnection(int interval, int maxFrequency)
        {
            Thread.Sleep(interval);
            Reconnection(result => {
                if (result) {
                    Debug.Log("重连成功...");
                    currFrequency = 0;
                    Reconnect();
                } else {
                    currFrequency++;
                    if (currFrequency > maxFrequency) {//尝试10次重连，如果失败则退出线程
                        Close();
                    } else {
                        Debug.Log($"尝试重连:{currFrequency}...");
                    }
                }
            });
        }

        /// <summary>
        /// 断线重新连接
        /// </summary>
        /// <param name="connected">连接结果</param>
        private void Reconnection(Action<bool> connected)
        {
            ConnectUdp(host, port, result => {
                connected(result);
            });
        }

        /// <summary>
        /// 后台线程发送心跳包
        /// </summary>
        private void HeartbeatHandle()
        {
            while (heartThread.ThreadState != ThreadState.Aborted)
            {
                Thread.Sleep(3000);//三秒发送一个心跳包
                OnHeartbeat();
            }
        }

        /// <summary>
        /// 当发送心跳包调用一次
        /// </summary>
        public virtual void OnHeartbeat()
        {
            Send("Heartbeat");
        }

        /// <summary>
        /// 断线重连成功后调用一次
        /// </summary>
        public virtual void Reconnect() { }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public virtual void Close()
        {
            if (heartThread != null) {
                heartThread.Abort();
                heartThread = null;
            }
            if (revThread != null) {
                revThread.Abort();
                revThread = null;
            }
            if (sendThread != null) {
                sendThread.Abort();
                sendThread = null;
            }
            if (client != null) {
                client.Close();
                client = null;
            }
        }

        /// <summary>
        /// 发送封包数据 - 远程过程调用服务器函数
        /// </summary>
        /// <param name="fun">要调用服务器的函数名</param>
        /// <param name="pars">对应服务器函数的参数</param>
        public static void Send(string fun, params object[] pars)
        {
            sendBuffers.Add(new SendBuffer(NetCmd.CallRpc, fun, pars));
        }

        /// <summary>
        /// 发送封包数据 - 带有几种命令的远程过程调用函数
        /// </summary>
        /// <param name="cmd">网络命令</param>
        /// <param name="fun">要调用服务器的函数名</param>
        /// <param name="pars">服务器的函数参数</param>
        public static void Send(NetCmd cmd, string fun, params object[] pars)
        {
            sendBuffers.Add(new SendBuffer(cmd, fun, pars));
        }
    }
}
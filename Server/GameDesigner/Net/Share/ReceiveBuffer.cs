namespace Net.Share
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// 网络接收缓存结构体
    /// </summary>
    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct ReceiveBuffer
    {
        /// <summary>
        /// 网络命令协议
        /// </summary>
        public string cmd;
        /// <summary>
        /// 命令+方法+参数代码数组
        /// </summary>
        public string[] codeCmd;
        /// <summary>
        /// 网络接收构造器
        /// </summary>
        public ReceiveBuffer(string cmd, string[] codeCmd)
        {
            this.cmd = cmd;
            this.codeCmd = codeCmd;
        }
    }
}


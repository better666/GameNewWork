namespace Net.Share
{
    /// <summary>
    /// 发送封包缓冲区
    /// </summary>
    public struct SendBuffer
    {
        public NetCmd netCmd;
        public string funName;
        public object[] pars;

        public SendBuffer(NetCmd netCmd, string funName, object[] pars)
        {
            this.netCmd = netCmd;
            this.funName = funName;
            this.pars = pars;
        }
    }
}

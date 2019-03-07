namespace Net.Share// 网络共用(通用)命名空间
{
    /// <summary>
    /// 网络命令
    /// </summary>
    public enum NetCmd
    {
        /// <summary>
        /// 如果是客户端调用则在服务器执行 如果是服务器调用则在客户端执行
        /// </summary>
        CallRpc,
        /// <summary>
        /// 服务器只转发给发送方客户端
        /// </summary>
        LocalCmd,
        /// <summary>
        /// 服务器负责转发给在同一房间或场景内的玩家
        /// </summary>
        SceneCmd,
        /// <summary>
        /// 服务器负责转发给所有在线的玩家
        /// </summary>
        AllCmd,
    }
}
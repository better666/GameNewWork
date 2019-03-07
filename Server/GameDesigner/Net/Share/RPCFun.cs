namespace Net.Share
{
    using System;

    /// <summary>
    /// 标注为远程过程调用函数
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class RPCFun : Attribute
    {
    }
}


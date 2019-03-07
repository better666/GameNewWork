﻿namespace Net.Share
{
    using System;
    using System.Reflection;
    using System.Runtime.InteropServices;

    /// <summary>
    /// 远程过程调用委托
    /// </summary>
    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct NetDelegate
    {
        /// <summary>
        /// 委托函数名
        /// </summary>
        public string Name;
        /// <summary>
        /// 委托对象
        /// </summary>
        public object target;
        /// <summary>
        /// 委托方法
        /// </summary>
        public MethodInfo method;
        /// <summary>
        /// 网络委托构造函数
        /// </summary>
        public NetDelegate(object target, MethodInfo method)
        {
            Name = method.ToString();
            this.target = target;
            this.method = method;
        }
    }
}


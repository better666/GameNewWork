using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Net.Share
{
    /// <summary>
    /// 网络对象缓存器
    /// </summary>
    [Serializable]
    public struct NetBuffer
    {
        public string name;
        /// <summary>
        /// 存储封包反序列化出来的对象
        /// </summary>
        public object target;
        /// <summary>
        /// 存储反序列化的函数
        /// </summary>
        public MethodInfo method;
        /// <summary>
        /// 存储反序列化参数
        /// </summary>
        public object[] pars;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="target">远程调用对象</param>
        /// <param name="method">远程调用方法</param>
        /// <param name="pars">远程调用参数</param>
        public NetBuffer(object target,MethodInfo method,object[] pars)
        {
            name = method.ToString();
            this.target = target;
            this.method = method;
            this.pars = pars;
        }
    }
}

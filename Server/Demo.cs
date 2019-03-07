using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Net.Client;
using Net.Share;
using UnityEngine;

namespace Server
{
    /// <summary>
    /// 连接服务器演示
    /// </summary>
    public class ConnentDemo : NetClient
    {
        private void Start()//当开始unity时自动连接服务器
        {
            //1.连接服务器，IP和端口可在unity监视面板设置
            Connect();

            //2.连接服务器，result为连接服务器的结果
            Connect(result => {
                if (result) {
                    Debug.Log("连接服务器成功");
                } else {
                    Debug.Log("连接服务器失败");
                }
            });

            //3.连接服务器，设置ip和端口，result连接结果逻辑
            Connect("127.0.0.1", 666, result => {
                if (result) {
                    Debug.Log("连接服务器成功");
                } else {
                    Debug.Log("连接服务器失败");
                }
            });
        }

        /// <summary>
        /// 使用UI按钮事件进行连接服务器
        /// </summary>
        public void UIConnentBtn()
        {
            Start();
        }
    }

    /// <summary>
    /// 发送数据演示
    /// </summary>
    public class SendDemo : NetClient
    {
        private void Start()
        {
            //发送一个远程过程调用函数(无参数)，在服务器进行转发给场景内的所有客户端，并调用客户端函数，Test函数必须添加[RPCFun]特性
            Send(NetCmd.SceneCmd, "Test");

            //发送一个远程过程调用函数(带参数)，在服务器进行转发给场景内的所有客户端，并调用客户端函数，Test函数必须添加[RPCFun]特性
            Send(NetCmd.SceneCmd, "Test", transform.position, transform.position);
        }

        [RPCFun]
        public void Test()
        {
            Debug.Log("远程过程调用成功");
            //编写你的逻辑代码...
        }

        [RPCFun]
        public void Test(Vector3 pos, Quaternion roto)
        {
            Debug.Log("远程过程调用成功");
            //编写你的逻辑代码...
        }

        /// <summary>
        /// 所有发送数据的函数介绍
        /// </summary>
        private void Demo()
        {
            //发送一个远程过程调用函数，在服务器进行调用Test函数，如果服务器中有Test函数，则调用成功，否则调用失败
            Send("Test");

            //发送一个远程过程调用函数(带String参数)，在服务器进行调用Test函数，函数参数必须相同才会调用成功，否则调用失败
            Send("Test", "hello 服务器");

            //发送一个远程过程调用函数(带3个参数)，在服务器进行调用Test函数，函数参数必须相同才会调用成功，否则调用失败
            Send("Test", transform.position, transform.rotation, transform.localScale);

            //发送一个远程过程调用函数(无参数)，在服务器进行转发给场景内的所有客户端，并调用客户端函数，Test函数必须添加[RPCFun]特性
            Send(NetCmd.SceneCmd, "Test");

            //发送一个远程过程调用函数(无参数)，在服务器进行转发给全服客户端，并调用客户端函数，Test函数必须添加[RPCFun]特性
            Send(NetCmd.AllCmd, "Test");

            //发送一个远程过程调用函数(无参数)，在服务器进行调用Test函数
            Send(NetCmd.CallRpc, "Test");
        }
    }

    /// <summary>
    /// 重写函数演示
    /// </summary>
    public class OverrideDemo : NetClient
    {
        public override void OnConnected()
        {
            Debug.Log("连接服务器成功");
        }

        public override void OnHeartbeat()
        {
            Debug.Log("发送心跳包");
        }

        public override void Reconnect()
        {
            Debug.Log("断线重连接成功");
        }
    }
}

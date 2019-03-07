using System;
using UnityEngine;
using Net.Server;
using Net.Share;

namespace GameServer
{
    /// <summary>
    /// 网络管理器类01
    /// </summary>
    public partial class ServerMgr : NetServer
    {
        public override void OnStarting()
        {
            Console.WriteLine("服务器启动完成...\r\n");
        }

        /// <summary>
        /// 注册账号
        /// </summary>
        [RPCFun]
        public void SignUp(string playerName,string account,string password)
        {
            int signUp = ServerDataBase.SignUp(playerName, account, password);
            if (signUp == 0)
            {
                Send(client, "LoginMessage", string.Format("注册账号成功: 你的账号为 {0}", account));
                Console.WriteLine(string.Format("注册账号成功: 你的账号为 {0}\r\n", account));
            }
            else
            {
                Send(client, "LoginMessage", string.Format("注册失败，昵称:{0}已存在! 亦注册账号:{1}", playerName, account));
                Console.WriteLine(string.Format("注册失败，昵称:{0}已存在! 亦注册账号:{1}\r\n", playerName, account));
            }
        }

        /// <summary>
        /// 登录游戏
        /// </summary>
        [RPCFun]
        public void Login(string account, string password)
        {
            NetPlayer login = ServerDataBase.Login(account, password);
            if (login != null)
            {
                Send(client, "Login", true, "登录成功", login.playerName);
                Console.WriteLine(string.Format("登录成功:账号:{0}\r\n", account));
                Console.WriteLine(string.Format("在线玩家 昵称:{0} 账号:{1} 登陆时间:{2}", login.playerName, login.account, DateTime.Now.ToString()));
                Console.WriteLine(string.Format("游戏当前在线玩家:{0}\r\n" , clients.Count));
                client.playerName = login.playerName;
                client.account = login.account;
                client.password = login.password;
            }
            else
            {   // 账号不存在 密码错误！
                Send(client, "LoginMessage", "账号不存在或密码错误!");
                Console.WriteLine(string.Format("账号:{0}不存在或密码:{1}错误\r\n", account, password));
            }
        }

        [RPCFun]
        public void FindPlayerName(string name)
        {
            bool findOut = false;
            foreach (var client in ServerDataBase.NetPlayers) {
                if (client.playerName == name) {
                    bool ingame = false;
                    foreach (var c in clients) {
                        if (c.Value.playerName == name) {
                            ingame = true;
                            Send(NetServer.client, "FindReult", true, name, "在线");
                            break;
                        }
                    }
                    if (!ingame) {
                        Send(NetServer.client, "FindReult", true, name, "离线");
                    }
                    findOut = true;
                    break;
                }
            }
            if (!findOut) {
                Send(client, "FindReult", false, name, "没有找到");
            }
        }

        private static int index = 0;

        /// <summary>
        /// 加载作战场景
        /// </summary>
        /// <param name="number">作战人数</param>
        [RPCFun]
        public void LoadScene(string sceneName, int number)
        {
            if (client.state == NetState.WaitTeam | client.state == NetState.InCombat)
                return;
            bool done = true;
            foreach (var scene in scenes)//检查所有桌子
            {
                if (scene.Value.state == NetState.Idle)//如果此桌子在等人入坐状态
                {
                    if (scene.Value.sceneNumber == number & scene.Value.players.Count < scene.Value.sceneNumber )
                    {//如果此桌子人数不够，则可以入坐
                        scene.Value.players.Add(client);
                        client.sceneIndex = scene.Key;
                        done = false;
                        break;
                    }
                }
            }
            if (done)//如果桌子人数已够,则重摆新桌并且让人入桌
            {
                index++;
                scenes.Add(index, new NetScene(client, number));
                client.sceneIndex = index;
            }
            var scene1 = scenes[client.sceneIndex];
            if (scene1.players.Count >= scene1.sceneNumber)
            {//如果场景人数够了，进入作战
                scene1.players[0].enemyCommend = true;
                foreach (var client in scene1.players)
                {
                    Send(client, "LoadScene", sceneName, client.enemyCommend);
                }
                client.state = NetState.InCombat;
                scene1.state = NetState.InCombat;
            }
            else//如果人数不够，进入等待状态
            {
                client.state = NetState.WaitTeam;
            }
        }

        /// <summary>
        /// 退出作战场景
        /// </summary>
        [RPCFun]
        public void ExitScene()
        {
            if (scenes.ContainsKey(client.sceneIndex))
            {
                scenes[client.sceneIndex].players.Remove(client);
                if (scenes[client.sceneIndex].players.Count == 0)
                {
                    scenes[client.sceneIndex].state = NetState.Idle;
                }
                else
                {
                    Send(scenes[client.sceneIndex].players[0], "SyncEnemyControl", true);
                    scenes[client.sceneIndex].players[0].enemyCommend = true;
                }
            }
            client.state = NetState.Idle;
            client.enemyCommend = false;
        }

        /// <summary>
        /// 创建玩家
        /// </summary>
        /// <param name="position">创建玩家的初始位置</param>
        [RPCFun]
        public void CreatorPlayer(Vector3 position)
        {
            //创建我的玩家
            Send(client, "CreatorPlayer", client.playerName, position, client.rotation);
            foreach (var client in scenes[client.sceneIndex].players)
            {
                if (client != NetServer.client)//因为当前客户端也在Scenes[index]集合内,所以必须添加这个判断，否则会创建两个玩家对象
                {
                    //把其他客户端的玩家发送给当前客户端进行创建玩家对象
                    Send(NetServer.client, "CreatorPlayer", client.playerName, client.position, client.rotation);
                    //把当前客户端玩家发送给其他客户端进行创建当前客户端的玩家对象
                    Send(client, "CreatorPlayer", NetServer.client.playerName, position, NetServer.client.rotation);
                }
            }
        }

        /// <summary>
        /// 玩家移动
        /// </summary>
        /// <param name="positon">玩家的当前位置</param>
        /// <param name="rotation">玩家的当前转向</param>
        /// <param name="direction">玩家的当前输入的移动方向</param>
        [RPCFun]
        public void PlayerMove(Vector3 positon,Quaternion rotation)
        {
            client.position = positon;
            client.rotation = rotation;
            foreach (var client in scenes[client.sceneIndex].players)
            {
                Send(client, "PlayerMove", NetServer.client.playerName, positon, rotation);
            }
        }

        /// <summary>
        /// 玩家被攻击
        /// </summary>
        /// <param name="hitPlayer">被攻击的玩家</param>
        /// <param name="attackPlayer">主攻击的玩家</param>
        /// <param name="attackProperty">攻击属性</param>
        /// <param name="attackType">攻击类型</param>
        /// <param name="property">客户端攻击后上传的属性值</param>
        [RPCFun]
        public void PlayerWound(string attackPlayer, string hitPlayer, AttackProperty attackProperty, AttackType attackType)
        {
            foreach (var client in scenes[client.sceneIndex].players) {
                if (client.playerName == hitPlayer)
                {
                    //client.propertyData = property;
                }
                Send(client, "PlayerWound", attackPlayer, hitPlayer, attackProperty, attackType);
            }
        }

        /// <summary>
        /// 玩家退出游戏
        /// </summary>
        [RPCFun]
        public void Quit()
        {
            if (scenes.ContainsKey(client.sceneIndex))
            {
                foreach (var client in scenes[client.sceneIndex].players)
                {
                    if (client != NetServer.client)//如果在同一个房间内
                    {
                        Send(client, "OtherPlayerQuit", NetServer.client.playerName);
                    }
                }
                scenes[client.sceneIndex].players.Remove(client);
            }
            foreach (var client in clients) 
            {
                if (client.Value == NetServer.client)
                {
                    clients.Remove(client.Key);//移除客户端对象
                    return;
                }
            }
        }
    }
}
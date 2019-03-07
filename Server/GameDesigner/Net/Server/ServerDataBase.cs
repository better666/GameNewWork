namespace Net.Server
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    
    /// <summary>
    /// 服务器数据库
    /// </summary>
    public static class ServerDataBase
    {
        /// <summary>
        /// 所有玩家信息
        /// </summary>
        public static List<NetPlayer> NetPlayers = new List<NetPlayer>();

        /// <summary>
        /// 添加新的账号信息
        /// </summary>
        /// <param name="playerName"></param>
        /// <param name="accountNumber"></param>
        /// <param name="password"></param>
        public static void Add(string playerName, string accountNumber, string password)
        {
            NetPlayers.Add(new NetPlayer(playerName, accountNumber, password));
        }

        /// <summary>
        /// 是否包含账号或昵称, 包含昵称和账号返回0, 包含账号返回-1, 包含昵称返回-2
        /// </summary>
        /// <param name="playerName"></param>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        public static int Contains(string playerName, string accountNumber)
        {
            for (int i = 0; i < NetPlayers.Count; i++)
            {
                if (NetPlayers[i].account == accountNumber)
                {
                    return -1;
                }
                if (NetPlayers[i].playerName == playerName)
                {
                    return -2;
                }
            }
            return 0;
        }

        /// <summary>
        /// 加载数据库信息
        /// </summary>
        public static void LoadPlayerDatas()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (!File.Exists(baseDirectory + "/ServerDATA.Data"))
            {
                File.Create(baseDirectory + "/ServerDATA.Data");
            }
            else
            {
                NetPlayers = new List<NetPlayer>();
                string[] strArray = File.ReadAllLines(baseDirectory + "/ServerDATA.Data");
                Console.WriteLine("正在加载服务器数据库文件...\r\n账号数量:" + strArray.Length);
                foreach (string str2 in strArray)
                {
                    string[] strArray3 = str2.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    try {
                        NetPlayers.Add(new NetPlayer(strArray3[0].Trim(), strArray3[1].Trim(), strArray3[2].Trim()));
                    } catch { }
                }
            }
        }

        /// <summary>
        /// 登录游戏
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static NetPlayer Login(string accountNumber, string password)
        {
            for (int i = 0; i < NetPlayers.Count; i++)
            {
                if ((NetPlayers[i].account == accountNumber) & (NetPlayers[i].password == password))
                {
                    return NetPlayers[i];
                }
            }
            return null;
        }

        /// <summary>
        /// 存储数据到数据文件里
        /// </summary>
        public static void SavePlayerDatas()
        {
            string contents = "";
            for (int i = 0; i < NetPlayers.Count; i++)
            {
                string[] textArray1 = new string[] { contents, NetPlayers[i].playerName, " | ", NetPlayers[i].account, " | ", NetPlayers[i].password, "|\n" };
                contents = string.Concat(textArray1);
            }
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "/ServerDATA.Data", contents);
        }

        /// <summary>
        /// 注册账号
        /// </summary>
        /// <param name="playerName"></param>
        /// <param name="accountNumber"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static int SignUp(string playerName, string accountNumber, string password)
        {
            if (((accountNumber == "") | (password == "")) | (playerName == ""))
            {
                return -1;
            }
            for (int i = 0; i < NetPlayers.Count; i++)
            {
                if (NetPlayers[i].playerName == playerName)
                {
                    return -2;
                }
                if (NetPlayers[i].account == accountNumber)
                {
                    return -3;
                }
            }
            NetPlayers.Add(new NetPlayer(playerName, accountNumber, password));
            SavePlayerDatas();
            return 0;
        }
    }
}


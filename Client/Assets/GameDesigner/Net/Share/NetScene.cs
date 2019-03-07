using Net.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.Share
{
    public class NetScene
    {
        /// <summary>
        /// 场景容纳人数
        /// </summary>
        public int sceneNumber = 0;
        /// <summary>
        /// 当前网络场景的玩家
        /// </summary>
        public List<NetPlayer> players = new List<NetPlayer>();
        /// <summary>
        /// 当前网络场景状态
        /// </summary>
        public NetState state = NetState.Idle;
        /// <summary>
        /// 场景内的怪物
        /// </summary>
        public List<NetEnemy> enemys = new List<NetEnemy>();

        /// <summary>
        /// 添加网络主场景并增加主场景最大容纳人数
        /// </summary>
        /// <param name="number">主创建最大容纳人数</param>
        public NetScene(int number)
        {
            sceneNumber = number;
        }

        /// <summary>
        /// 添加网络场景并增加当前场景人数
        /// </summary>
        /// <param name="client">网络玩家</param>
        /// <param name="number">创建场景容纳人数</param>
        public NetScene(NetPlayer client, int number)
        {
            players.Add(client);
            sceneNumber = number;
        }
    }
}
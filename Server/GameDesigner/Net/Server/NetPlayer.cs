namespace Net.Server
{
    using Net.Share;
    using UnityEngine;
    using System.Net;

    public class NetPlayer
    {
        public EndPoint remotePoint;
        public string account = "";
        public string password = "";
        public string playerName = "";
        public Vector3 position = Vector3.zero;
        public Quaternion rotation = Quaternion.identity;
        public PropertyData propertyData = new PropertyData();
        public NetState state = NetState.Idle;

        /// <summary>
        /// 此玩家所在的场景组索引
        /// </summary>
        public int sceneIndex = 0;
        public bool Heart = true;
        /// <summary>
        /// 此客户端得到怪物行为的转发权限
        /// </summary>
        public bool enemyCommend;

        private NetPlayer() { }

        public NetPlayer(EndPoint remotePoint)
        {
            this.remotePoint = remotePoint;
        }

        public NetPlayer(string playerName, string account, string password)
        {
            this.propertyData = new PropertyData();
            this.playerName = playerName;
            this.account = account;
            this.password = password;
        }
    }
}
namespace Net.Client
{
    using Net.Share;
    using System;
    using UnityEngine;

    public class NetScenePlayerStartPoint : NetBehaviour
    {
        [RPCFun]
        public void CreatorPlayer(string playerName, Vector3 position, Quaternion rotation)
        {
            foreach (var client in ClientMgr.Instance.clientPlayers)
            {
                if (client.PlayerName == playerName)//如果其他玩家已经被创建则跳出
                {
                    return;
                }
            }
            ClientMgr.Instance.CreatorPlayer(playerName, position, rotation);
        }

        [RPCFun]
        public void OtherPlayerQuit(string playerName)
        {
            for (int i = 0; i < ClientMgr.Instance.clientPlayers.Count; i++)
            {
                if (ClientMgr.Instance.clientPlayers[i].PlayerName == playerName)
                {
                    GameObject gameObject = ClientMgr.Instance.clientPlayers[i].gameObject;
                    ClientMgr.Instance.clientPlayers.RemoveAt(i);
                    Destroy(gameObject);
                }
            }
        }

        public void Start()
        {
            Send("CreatorPlayer", transform.position);
        }
    }
}


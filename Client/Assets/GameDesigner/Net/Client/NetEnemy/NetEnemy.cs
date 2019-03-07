using UnityEngine;

namespace Net.Client.NetEnemy
{
    public class NetEnemy : NetPlayer
    {
        public override void Awake()
        {
            base.Awake();
            PlayerName = name;
            if (!ClientMgr.Instance.syncEnemyControl) {
                Send(Share.NetCmd.SceneCmd, "ServerProxy", NetClient.client.LocalEndPoint.ToString(), PlayerName);
                enabled = false;
            }
        }

        [Share.RPCFun]
        public void ServerProxy(string ip, string name)
        {
            if (ClientMgr.Instance.syncEnemyControl & name == PlayerName) {
                Send(Share.NetCmd.SceneCmd, "SyncEnemyProperty", ip, PlayerName, transform.position, transform.rotation, Property, stateManager.stateMachine.stateIndex);
            }
        }

        [Share.RPCFun]
        public void SyncEnemyProperty(string ip, string name, Vector3 position, Quaternion rotation, PropertyData property, int stateID)
        {
            if (NetClient.client.LocalEndPoint.ToString() == ip & name == PlayerName) {
                if (property.hp > 0) {
                    Property = property;
                    transform.position = position;
                    transform.rotation = rotation;
                    stateManager.stateMachine.stateIndex = stateID;
                    enabled = true;
                }
            }
        }
    }
}

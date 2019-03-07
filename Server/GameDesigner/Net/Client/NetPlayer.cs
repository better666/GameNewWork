namespace Net.Client
{
    using GameDesigner;
    using Net.Share;
    using UnityEngine;

    public class NetPlayer : Player
    {
        /// <summary>
        /// 发送数据次数
        /// </summary>
        public int sendIndex;

        public StateManager stateManager;

        /// <summary>
        /// 如果是自身玩家为True，是其他本地客户端为False
        /// </summary>
        public bool IsLocalPlayer {
            get {
                if (PlayerName == ClientMgr.Instance.playerName)//如果是本地玩家才可以控制
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 标识RPCFun都属于网络调用函数
        /// 玩家对象移动方法，服务器发送给客户端哪个玩家正在移动，移动的玩家名称，玩家移动的位置，玩家移动的转向，玩家的当前方向
        /// </summary>
        [RPCFun]
        public void PlayerMove(string playerName, Vector3 position, Quaternion rotation)
        {
            //如果服务器传过来的玩家名称等于这个对象的名称，那么这个玩家对象要进行移动逻辑
            if (PlayerName == playerName)
            {
                transform.rotation = rotation;
                NetTransform.SyncPosition(transform, position, 1, 0.2f);
            }
        }

        /// <summary>
        /// 状态机同步
        /// </summary>
        /// <param name="playerName"></param>
        /// <param name="stateID"></param>
        [RPCFun]
        public void SyncStateMgr(string playerName, int stateID)
        {
            if (PlayerName == playerName)
            {
                stateManager.OnEnterNextState(stateID);
            }
        }

        /// <summary>
        /// 标识RPCFun都属于网络调用函数
        /// 玩家被攻击，服务器传来了被攻击的玩家名称，主攻击方的玩家名称，主攻击方玩家的攻击属性，主攻击方玩家的攻击类型，服务器确定被攻击方玩家的结果属性值
        /// </summary>
        [RPCFun]
        public void PlayerWound(string attackPlayer, string hitPlayer, AttackProperty attackProperty, AttackType attackType)
        {
            if (PlayerName == hitPlayer)
            {
                foreach (var player in FindObjectsOfType<PlayerSystem>())
                {
                    if (player.PlayerName == attackPlayer)
                    {
                        Wound(player, attackProperty, attackType);
                        break;
                    }
                }
            }
        }

        public new void Start()
        {
            if (stateManager==null) {
                stateManager = GetComponent<StateManager>();
            }
            if (PlayerName == ClientMgr.Instance.playerName)
            {
                ARPGcamera gcamera = FindObjectOfType<ARPGcamera>();
                gcamera.target = transform;
                gcamera.enabled = true;
            }
        }
    }
}
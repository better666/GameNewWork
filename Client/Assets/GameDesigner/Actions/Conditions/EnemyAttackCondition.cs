using UnityEngine;
using GameDesigner;
using Net.Client;
using Net.Share;

public class EnemyAttackCondition : TransitionBehaviour
{
    private PlayerSystem enemy;
	public float attackDistance = 3f;
	public Timer time = new Timer (0,3f);

    private void Start()
    {
        NetBehaviour.AddRPCDelegate(this);
        enemy = stateManager.GetComponent<PlayerSystem>();
    }

    void LateUpdate()
	{
		if(time.IsOutTime){}
	}

    private void OnDestroy()
    {
        NetBehaviour.RemoveRPCDelegate(this);
    }

    /// <summary>
    /// 当状态每一帧调用这个方法 ( 参数CurrState ： 当前状态 , 参数 NextState ： 下一个状态 , 参数 transition ： 状态链接 , 参数 isEnterNextState ： 是否进入下一个状态 )
    /// </summary>

    override public void OnTransitionUpdate( StateManager manager , State CurrState , State DstState , Transition transition , ref bool isEnterNextState ) 
	{
        if (ClientMgr.Instance.syncEnemyControl)
        {
            if (enemy.Distance < attackDistance)
            {
                if (time.IsTimeOut)
                {
                    Send(NetCmd.SceneCmd, "StartAttack", enemy.PlayerName, DstState.stateID);
                }
            }
        }
	}

    [RPCFun]
    public void StartAttack(string name , int id)
    {
        if (enemy.PlayerName == name & transition.nextState.stateID == id) {
            enemy.transform.LookAt(enemy.attackTarget.transform);
            enemy.transform.rotation = Quaternion.Euler(0, enemy.transform.eulerAngles.y, 0);
            Send(NetCmd.SceneCmd, "SyncStateMgr", enemy.PlayerName, id);
        }
    }
}
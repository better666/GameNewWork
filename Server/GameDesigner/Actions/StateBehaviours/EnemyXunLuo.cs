using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using GameDesigner;
using Net.Client;
using Net.Share;

public class EnemyXunLuo : StateBehaviour
{
	public PlayerSystem enemy = null;
	public Timer sikaoTime = new Timer( 0 , 2f );

    public NetTransform syncPos;

	void Start()
	{
		enemy = stateManager.GetComponent<PlayerSystem>();
        NetBehaviour.AddRPCDelegate(this);
        syncPos.SetLocation(enemy.transform.position, enemy.transform.rotation);
    }

    private void OnDestroy()
    {
        NetBehaviour.RemoveRPCDelegate(this);
    }

    public override void OnEnterState(StateManager stateManager, State currentState, State nextState)
    {
        if (ClientMgr.Instance.enemyCommend)
        {
            Send(NetCmd.SceneCmd, "SyncState", currentState.stateID);
        }
    }

    /// <summary>
    /// 同步敌人状态
    /// </summary>
    /// <param name="stateID"></param>
    [RPCFun]
    public void SyncState(int stateID)
    {
        if(state.stateID != stateID)
            stateManager.OnEnterNextState(stateID);
        foreach (var transition in state.transitions)
        {
            transition.time = 0;//网络同步需要，控制过渡时间保持一致
        }
    }

    /// <summary>
    /// 随机巡逻
    /// </summary>
    override public void OnUpdateState( StateManager stateMachine , State currentState , State nextState )
	{
        if (ClientMgr.Instance.enemyCommend)
        {
            if (sikaoTime.IsTimeOut)
            {
                enemy.transform.Rotate(0, Random.Range(360, -360), 0);
                Send(NetCmd.SceneCmd, "SyncRangeRoto", enemy.transform.position, enemy.transform.rotation, Random.Range(0.5f, 2f));
            }
        }
		enemy.transform.Translate ( 0 , 0 , 3.0F * Time.deltaTime );
	}

    [RPCFun]
    public void SyncRangeRoto(Vector3 position, Quaternion rotation, float rangeTime)
    {
        enemy.transform.rotation = rotation;
        sikaoTime.EndTime = rangeTime;
        NetTransform.SyncPosition(enemy.transform, position, 1, 0.5f);
    }

    /// <summary>
    /// 编辑器扩展 (重要提示!你想扩展编辑器就得返回真,否则显示默认编辑器监视面板) ( 参数state : 这个行为是被这个状态管理 )
    /// </summary>
#if UNITY_EDITOR
    override public bool OnInspectorGUI( State state )
	{
		return false; //返回假: 绘制默认监视面板 | 返回真: 绘制扩展自定义监视面板
	}
	#endif
}
using UnityEngine;
using GameDesigner;

public class EnemyMoveCondition : TransitionBehaviour
{
    public PlayerSystem enemy;
	[Header("真:当玩家在追击范围时进入,假:当玩家在攻击范围内时进入")]
	public bool min_max = true;
	public float runDistance = 15f;
	public float attackDistance = 2.5f;
	public Timer time = new Timer(0,1);

    private void Start()
    {
        enemy = stateManager.GetComponent<PlayerSystem>();
    }

    /// <summary>
    /// 当状态每一帧调用这个方法 ( 参数CurrState ： 当前状态 , 参数 NextState ： 下一个状态 , 参数 transition ： 状态链接 , 参数 isEnterNextState ： 是否进入下一个状态 )
    /// </summary>
    public override void OnTransitionUpdate( StateManager manager , State CurrState , State DstState , Transition transition , ref bool isEnterNextState ) 
	{
        if (!Net.Client.ClientMgr.Instance.syncEnemyControl)
            return;

		if (min_max) 
		{
			if (enemy.Distance < runDistance & enemy.Distance > attackDistance & time.IsTimeOut ) // 如果对象在追击距离内,并且在攻击距离外,进入追击状态
			{
                Send(Net.Share.NetCmd.SceneCmd, "SyncStateMgr", enemy.PlayerName, DstState.stateID);
			}
		} 
		else 
		{
			if (enemy.Distance > runDistance | enemy.Distance < attackDistance ) // 如果对象在追击距离外,或者对象在攻击距离内,退出追击状态进入攻击状态
			{
                Send(Net.Share.NetCmd.SceneCmd, "SyncStateMgr", enemy.PlayerName, DstState.stateID);
            }
		}
	}

	/// <summary>
	/// 编辑器扩展 (重要提示!你想扩展编辑器就得返回真,否则显示默认编辑器监视面板) ( 参数state : 这个行为是被这个状态管理 )
	/// </summary>

	#if UNITY_EDITOR
	override public bool OnInspectorGUI( State state )
	{
		return false; //返回假: 绘制默认监视面板 | 返回真: 绘制扩展自定义监视面板
	}

	void OnDrawGizmos ()
	{
		UnityEditor.Handles.color = Color.yellow;
		UnityEditor.Handles.DrawWireDisc (stateManager.transform.position , Vector3.up , runDistance );
		UnityEditor.Handles.color = Color.red;
		UnityEditor.Handles.DrawWireDisc (stateManager.transform.position , Vector3.up , attackDistance );
	}
	#endif
}
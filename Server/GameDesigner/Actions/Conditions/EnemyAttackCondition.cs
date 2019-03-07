using UnityEngine;
using System.Collections.Generic;
using GameDesigner;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class EnemyAttackCondition : TransitionBehaviour
{
    public PlayerSystem enemy;
	public float attackDistance = 3f;
	public Timer time = new Timer (0,3f);

    private void Start()
    {
        enemy = stateManager.GetComponent<PlayerSystem>();
    }

    void LateUpdate()
	{
		if(time.IsOutTime){}
	}

	/// <summary>
	/// 当状态每一帧调用这个方法 ( 参数CurrState ： 当前状态 , 参数 NextState ： 下一个状态 , 参数 transition ： 状态链接 , 参数 isEnterNextState ： 是否进入下一个状态 )
	/// </summary>

	override public void OnTransitionUpdate( StateManager manager , State CurrState , State DstState , Transition transition , ref bool isEnterNextState ) 
	{
		if (enemy.Distance < attackDistance) 
		{
			if( time.IsOutTime ){
                enemy.transform.LookAt (enemy.attackTarget.transform );
                enemy.transform.rotation = Quaternion.Euler ( 0 , enemy.transform.eulerAngles.y , 0 );
				isEnterNextState = true;
				time.time = 0;
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
	#endif
}
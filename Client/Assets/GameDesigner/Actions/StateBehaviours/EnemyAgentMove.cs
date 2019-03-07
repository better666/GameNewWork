using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameDesigner;
using UnityEngine.AI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class EnemyAgentMove : StateBehaviour
{
	private NavMeshAgent _agent = null;
	public NavMeshAgent agent{
		get{
			if( _agent == null ){
				_agent = stateManager.GetComponent<NavMeshAgent>();
				if(_agent == null)
					_agent = stateManager.gameObject.AddComponent<NavMeshAgent>();
			}
			return _agent;
		}
	}

	public float runDistance = 10f;
	public float attackDistance = 2f;

	/// <summary>
	/// 当状态进入时调用一次 ( 参数 stateMachine ： 状态机处理器 , 参数 layer ： 这个状态在这个层内 , 参数 currentState ： 当前状态 )
	/// </summary>

	override public void OnEnterState( StateManager stateMachine , State currentState , State nextState )
	{

	}

	/// <summary>
	/// 当状态每一帧调用 ( 参数 stateMachine ： 状态机处理器 , 参数 layer ： 这个状态在这个层内 , 参数 currentState ： 当前状态 )
	/// </summary>

	override public void OnUpdateState( StateManager stateMachine , State currentState , State nextState )
	{
		if( GetComponentInParent<GameDesigner.PlayerSystem>().Distance < runDistance ){
			agent.SetDestination(GetComponentInParent<GameDesigner.PlayerSystem>().attackTarget.transform.position);
			agent.stoppingDistance = attackDistance;
		}else{
			
			agent.SetDestination(transform.position);
		}
	}

	/// <summary>
	/// 当状态退出后调用一次 ( 参数 stateMachine ： 状态机处理器 , 参数 layer ： 这个状态在这个层内 , 参数 currentState ： 当前状态 )
	/// </summary>

	override public void OnExitState( StateManager stateMachine , State currentState , State nextState )
	{

	}

	/// <summary>
	/// 编辑器扩展 (重要提示!你想扩展编辑器就得返回真,否则显示默认编辑器监视面板) ( 参数state : 这个行为是被这个状态管理 )
	/// </summary>

	#if UNITY_EDITOR
	override public bool OnInspectorGUI( State state )
	{
		//gameObject.name = EditorGUILayout.TextField ( "游戏物体名称" , gameObject.name ); // 在这里写你的自定义监视面板
		return false; //返回假: 绘制默认监视面板 | 返回真: 绘制扩展自定义监视面板
	}
	#endif
}
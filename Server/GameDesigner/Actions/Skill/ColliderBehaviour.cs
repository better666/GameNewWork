using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDesigner;

public class ColliderBehaviour : IBehaviour 
{
	private StateManager _stateManager = null;
	public new StateManager stateManager{
		get{
			if(_stateManager == null){
				_stateManager = transform.GetComponentInParent<SkillCollider>().parent.GetComponent<StateManager>();
			}
			return _stateManager;
		}
		set{ _stateManager = value; }
	}
		
	private StateMachine _stateMachine = null;
	public new StateMachine stateMachine{
		get{
			if(_stateMachine == null){
				_stateMachine = transform.GetComponentInParent<SkillCollider>().parent.GetComponent<StateManager>().stateMachine;
			}
			return _stateMachine;
		}
		set{ _stateMachine = value; }
	}

	private SkillCollider _skillCollider = null;
	public SkillCollider skillCollider{
		get{
			if(_skillCollider == null){
				_skillCollider = transform.GetComponentInParent<SkillCollider>();
			}
			return _skillCollider;
		}
		set{ _skillCollider = value; }
	}

	/// <summary>
	/// 当状态进入时 ( player 玩家或敌人或怪物基类 )
	/// </summary>

	public virtual void OnEnter ( GameDesigner.PlayerSystem player , Transform parent )
	{

	}

	/// <summary>
	/// 当状态每一帧调用 ( transform  )
	/// </summary>

	public virtual void OnUpdate ( GameDesigner.PlayerSystem player , Transform parent )
	{
		
	}

	/// <summary>
	/// 当状态结束调用 ( action 状态动作管理(也是此类发送到此方法的入口点) )
	/// </summary>

	public virtual void OnExit ( GameDesigner.PlayerSystem player , Transform parent )
	{

	}

	/// <summary>
	/// 当触发进入（主技能碰撞触发管理器 ， 触发的碰撞器 ， 主根对象）
	/// </summary>

	public virtual void OnAllTriggerEnter( SkillCollider skill , Collider other , Transform parent )
	{
		
	}

	/// <summary>
	/// 当进入触发器 ( other参数包含敌人,玩家,怪物等等 )
	/// </summary>

	public virtual void OnSkillTriggerEnter ( SkillCollider skill , GameDesigner.PlayerSystem other , Transform parent )
	{
		
	}
}
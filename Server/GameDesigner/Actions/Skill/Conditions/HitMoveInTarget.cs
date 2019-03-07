using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDesigner;

public class HitMoveInTarget : StateBehaviour {

	public float moveSpeed = 5f;
	[Header("当移动到击中对象附近时清空玩家的击中对象物体")]
	public bool onMoveInTargetEmptyNullPlayerHitTarget = true;

	[Header("当飞行到击中对象附近后获取Q1的击中对象所在的状态")]
	public int onMoveInTargetEnterStateID = 0;

	public HitTarget _hitTarget = null;
	public HitTarget hitTarget{
		get{
			if(_hitTarget==null){
				_hitTarget = stateManager.GetComponent<HitTarget> ();
			}
			return _hitTarget;
		}
	}

	public override void OnEnterState (StateManager stateManager, State currentState, State nextState)
	{
		hitTarget.isMove = true;
	}

	public override void OnExitState (StateManager stateManager, State currentState, State nextState)
	{
		hitTarget.nullTargetTime.time = 0;
		hitTarget.isMove = false;
	}

	public override void OnUpdateState (StateManager stateManager, State currentState, State nextState)
	{
		try{
			stateManager.GetComponent<Rigidbody>().isKinematic = true;
			stateManager.GetComponent<CapsuleCollider>().isTrigger = true; 
		}catch{ }

		if( hitTarget.target == null ){
			foreach (Transition t in currentState.transitions) {
				if (onMoveInTargetEnterStateID == t.nextState.stateID) {
					t.isEnterNextState = true;
				}
			}
		}else if ( Vector3.Distance (stateManager.transform.position, hitTarget.target.position) < 2f) {
			try {
				stateManager.GetComponent<Rigidbody> ().isKinematic = false;
				stateManager.GetComponent<CapsuleCollider> ().isTrigger = false; 
			} catch { }
			if( onMoveInTargetEmptyNullPlayerHitTarget ){
				hitTarget.target = null;
			}
			foreach (Transition t in currentState.transitions) {
				if (onMoveInTargetEnterStateID == t.nextState.stateID) {
					t.isEnterNextState = true;
				}
			}
		} else {
			if( GetComponentInParent<GameDesigner.PlayerSystem>().transform.position.y + 2 > hitTarget.target.position.y | GetComponentInParent<GameDesigner.PlayerSystem>().transform.position.y - 2 < hitTarget.target.position.y )
			{
				GetComponentInParent<GameDesigner.PlayerSystem>().transform.LookAt ( hitTarget.target.position );
			}
			else
			{
				GetComponentInParent<GameDesigner.PlayerSystem>().transform.LookAt ( hitTarget.target.position );
				GetComponentInParent<GameDesigner.PlayerSystem>().transform.Rotate ( 0 , GetComponentInParent<GameDesigner.PlayerSystem>().transform.eulerAngles.y , 0 );
			}
			stateManager.transform.Translate ( 0 , 0 , moveSpeed * Time.deltaTime );
		}
	}
}

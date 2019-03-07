using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDesigner;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;

public class HitTargetCondition : StateBehaviour
{
	/// 当击中对象进入的状态ID索引
	public int onHitEnterStateID = 0;
	/// 当没有击中对象进入的状态ID索引
	public int onNotHitEnterStateID = 0;

	public KeyCode key = KeyCode.Q;

	public SaveHitTarget _hitTarget = null;
	public SaveHitTarget hitTarget{
		get{
			if(_hitTarget==null){
				_hitTarget = stateMachine.GetComponentInChildren<SaveHitTarget> ();
			}
			return _hitTarget;
		}
	}

	void LateUpdate()
	{
		if ( GetComponentInParent<GameDesigner.PlayerSystem>().Hp <= 0 )
			return;

		if ( Input.GetKey (key) & state.stateID != stateMachine.stateIndex & ID ) {
			stateManager.OnEnterNextState (stateMachine.currState, stateMachine.states[state.stateID] );
		}
	}

	bool ID{
		get{
			bool id = true;
			foreach(Transition t in state.transitions)
				if(t.nextState.stateID==stateMachine.stateIndex)
					id = false;
			if(id)
				return true;
			return false;
		}
	}

	public HitTarget _hit = null;
	public HitTarget hit{
		get{ 
			if(_hit==null){
				_hit = stateManager.GetComponent<HitTarget> ();
				if(_hit==null){
					_hit = stateManager.gameObject.AddComponent<HitTarget> ();
				}
			}
			return _hit;
		}
	}

	public override void OnUpdateState (StateManager stateMachine, State currentState, State nextState)
	{
		if (hit.target != null) {
			currentState.transitions[onHitEnterStateID].isEnterNextState = true;
		}else{
			currentState.transitions[onNotHitEnterStateID].isEnterNextState = true;
		}
	}

	#if UNITY_EDITOR
	public override bool OnInspectorGUI (State state)
	{
		key = (KeyCode)EditorGUILayout.EnumPopup ( "按键值" , key );
		onNotHitEnterStateID = EditorGUILayout.Popup( "未击中对象进入状态" , onNotHitEnterStateID , Array.ConvertAll< Transition , string >( state.transitions.ToArray() , new Converter< Transition , string >( delegate ( Transition t ){ return t.currState.name + " => " + t.nextState.name ; } ) ) );
		onHitEnterStateID = EditorGUILayout.Popup( "击中对象进入状态" , onHitEnterStateID , Array.ConvertAll< Transition , string >( state.transitions.ToArray() , new Converter< Transition , string >( delegate ( Transition t ){ return t.currState.name + " => " + t.nextState.name ; } ) ) );
		_hitTarget = (SaveHitTarget)EditorGUILayout.ObjectField ("保存击中目标组件",_hitTarget,typeof(SaveHitTarget),true);
		return true;
	}
	#endif
}

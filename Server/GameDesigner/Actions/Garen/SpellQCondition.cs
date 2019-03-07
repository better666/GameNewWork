using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameDesigner;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;

public class SpellQCondition : StateBehaviour
{
	/// 当击中对象进入的状态ID索引
	public int onHitEnterStateID = 0;
	/// 当没有击中对象进入的状态ID索引
	public int onNotHitEnterStateID = 0;

	public KeyCode key = KeyCode.Q;
	public int DstStateID = 0;

	public int stateID1 = 0;
	public int stateID2 = 0;

	public int clipIndex = 0;
	public string clipName = "";

	public string saveRunClipName = "";

	public Timer time = new Timer( 0 , 3f );

	public int runStateIndex = 0;

	public bool runEnter = false;

	void LateUpdate()
	{
		if (stateMachine == null | GetComponentInParent<GameDesigner.PlayerSystem>().Hp <= 0 )
			return;

		if( runEnter )
		{
			if (GetComponentInParent<GameDesigner.PlayerSystem>().attackTarget != null) 
			{//进入条件状态后自动进入最后一击技能状态(因为范围存在敌人)
				stateMachine.stateManager.OnEnterNextState (stateMachine.currState, stateMachine.states [DstStateID]);
				stateMachine.states [runStateIndex].Action.clipName = saveRunClipName;
				runEnter = false;
				time.time = 0;
			}

			if( time.IsTimeOut )
			{
				stateMachine.states [runStateIndex].Action.clipName = saveRunClipName;
				if( PlayerMove.moveDirection != Vector3.zero )
					stateMachine.stateManager.GetComponent<Animation>().Play (saveRunClipName);
				runEnter = false;
			}
		}
		else
		{
			if ( Input.GetKey (key) & DstStateID != stateMachine.stateIndex & stateID1 != stateMachine.stateIndex & stateID2 != stateMachine.stateIndex ) 
			{
				stateMachine.stateManager.OnEnterNextState (stateMachine.currState, stateMachine.states [DstStateID]);
			}
		}
	}

	public override void OnUpdateState (StateManager stateManager, State currentState, State nextState)
	{
		if (GetComponentInParent<GameDesigner.PlayerSystem>().attackTarget != null) {
			currentState.transitions[onHitEnterStateID].isEnterNextState = true;
		}else{
			currentState.transitions[onNotHitEnterStateID].isEnterNextState = true;
			saveRunClipName = stateMachine.states [runStateIndex].Action.clipName;
			stateMachine.states [runStateIndex].Action.clipName = clipName;
			runEnter = true;
		}
	}

	#if UNITY_EDITOR
	public override bool OnInspectorGUI (State state)
	{
		string[] stateNames = Array.ConvertAll< State , string > (state.stateMachine.states.ToArray (), new Converter< State , string > (delegate ( State s) {
			return state.name + " => " + s.name;
		}));

		key = (KeyCode)EditorGUILayout.EnumPopup ( "按键值" , key );
		DstStateID = EditorGUILayout.Popup( "果断进入状态" , DstStateID , stateNames );

		string[] tranNames = Array.ConvertAll< Transition , string > (state.transitions.ToArray (), new Converter< Transition , string > (delegate ( Transition t) {
			return t.currState.name + " => " + t.nextState.name;
		}));
		onHitEnterStateID = EditorGUILayout.Popup( "敌人在范围内进入状态" , onHitEnterStateID , tranNames );
		onNotHitEnterStateID = EditorGUILayout.Popup( "敌人不在范围内进入状态" , onNotHitEnterStateID , tranNames );

		stateID1 = state.transitions [onNotHitEnterStateID].nextState.stateID;
		stateID2 = state.transitions [onHitEnterStateID].nextState.stateID;

		runStateIndex = EditorGUILayout.Popup ("Run状态", runStateIndex , stateNames );

		try {
			clipIndex = EditorGUILayout.Popup ("修改Run状态动画为冲锋动画", clipIndex, stateMachine.stateManager.clipNames.ToArray());
			clipName = stateMachine.stateManager.clipNames [clipIndex];
		} catch { }

		return true;
	}
	#endif
}
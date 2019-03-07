using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using GameDesigner;

public class ForDeath : StateBehaviour
{
    public PlayerSystem player;
	public int DstStateID = 0;
	public int onHpMaxEnterStateID = 0;

    private void Start()
    {
        player = stateManager.GetComponent<PlayerSystem>();
    }

    void LateUpdate()
	{
		if (player.Hp <= 0 & DstStateID != stateMachine.stateIndex ) {
			stateManager.OnEnterNextState (DstStateID);
		}
	}

	/// <summary>
	/// 当状态每一帧调用 ( 参数 stateMachine ： 状态机处理器 , 参数 layer ： 这个状态在这个层内 , 参数 currentState ： 当前状态 )
	/// </summary>

	override public void OnUpdateState( StateManager stateMachine , State currentState , State nextState )
	{
		if (player.Hp > 0 & currentState.transitions.Count > 0) {
			currentState.transitions[onHpMaxEnterStateID].isEnterNextState = true;
		}
	}

	/// <summary>
	/// 编辑器扩展 (重要提示!你想扩展编辑器就得返回真,否则显示默认编辑器监视面板) ( 参数state : 这个行为是被这个状态管理 )
	/// </summary>

	#if UNITY_EDITOR
	override public bool OnInspectorGUI( State state )
	{
        player = (PlayerSystem)EditorGUILayout.ObjectField("对象", player, typeof(PlayerSystem), true);
        DstStateID = EditorGUILayout.Popup( "当死亡果断进入状态" , DstStateID , Array.ConvertAll( state.stateMachine.states.ToArray() , new Converter< State , string >( delegate ( State s ){ return state.name + " => " + s.name ; } ) ) );
		onHpMaxEnterStateID = EditorGUILayout.Popup( "当玩家复活进入状态" , onHpMaxEnterStateID , Array.ConvertAll( state.transitions.ToArray() , new Converter< Transition , string >( delegate ( Transition t ){ return t.currState.name + " => " + t.nextState.name ; } ) ) );
		return true;
	}
	#endif
}
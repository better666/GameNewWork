using UnityEngine;
using System.Collections.Generic;
using GameDesigner;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class OutHitTarget : ColliderBehaviour
{
	/// 当击中对象进入的状态ID索引
	public int onHitEnterStateID = 0;

	/// <summary>
	/// 当进入触发器 ( other参数包含敌人,玩家,怪物等等 )
	/// </summary>

	public override void OnSkillTriggerEnter ( SkillCollider skill , GameDesigner.PlayerSystem other, Transform parent)
	{
		skill.player.GetComponent<StateManager>().stateMachine.states[onHitEnterStateID].lengqueTime.time = 1000;
	}

	/// <summary>
	/// 编辑器扩展 (重要提示!你想扩展编辑器就得返回真,否则显示默认编辑器监视面板) ( 参数state : 这个行为是被这个状态管理 )
	/// </summary>

	#if UNITY_EDITOR
	override public bool OnInspectorGUI( State state )
	{
		onHitEnterStateID = EditorGUILayout.Popup( "当技能击中修改冷却时间为最大值" , onHitEnterStateID , Array.ConvertAll< State , string >( state.stateMachine.states.ToArray() , new Converter< State , string >( delegate ( State s ){ return state.name + " => " + s.name ; } ) ) );
		
		return false; //返回假: 绘制默认监视面板 | 返回真: 绘制扩展自定义监视面板
	}
	#endif
}
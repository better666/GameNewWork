using UnityEngine;
using System.Collections.Generic;
using GameDesigner;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class LoadSkillEffect : ActionBehaviour
{
	public LoadEffect effect;
	public int index = 0;
	public bool lookIndex = false;
	public bool random = true;

	void Start ()
	{
		effect = stateManager.GetComponent<LoadEffect>();
	}

	/// <summary>
	/// 当动画事件进入 ( action 状态动作管理(也是此类发送到此方法的入口点) , 动画事件时间 )
	/// </summary>

	override public void OnAnimationEventEnter( State state , StateAction action , float animEventTime )
	{
		try{
			if(random)
				action.effectSpwan = Resources.Load(effect.effects[Random.Range( 0,effect.effects.Count)]);
			else{
				action.effectSpwan = Resources.Load(effect.effects[index]);
				if( !lookIndex ) index ++;
			}
		}catch{index = 0;}
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
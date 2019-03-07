using UnityEngine;
using System.Collections.Generic;
using GameDesigner;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ChangeSoundClip : ActionBehaviour
{
	public float clipTime = 50f; 
	public AudioClip clip = null;
	public bool isEnterEvent = false;

	/// <summary>
	/// 当状态每一帧调用 ( action 状态动作管理(也是此类发送到此方法的入口点) )
	/// </summary>

	override public void OnStateUpdate ( State state , StateAction action )
	{
		if (action.animTime >= clipTime & !isEnterEvent) {
            AudioManager.Play(clip);
            isEnterEvent = true;
		}
	}

	/// <summary>
	/// 当状态结束调用 ( action 状态动作管理(也是此类发送到此方法的入口点) )
	/// </summary>

	override public void OnStateExit ( State state , StateAction action )
	{
		isEnterEvent = false;
	}
}
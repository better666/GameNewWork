using UnityEngine;
using System.Collections.Generic;
using GameDesigner;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SpwanLookAtTarget : ColliderBehaviour
{
	public Transform target = null;
	public Vector3 lookatHeight = new Vector3(0,1.5f,0);

	public override void OnEnter ( GameDesigner.PlayerSystem player , Transform pan )
	{
		if( player.attackTarget ){
			target = player.attackTarget.transform;
		}
	}

	public override void OnUpdate ( GameDesigner.PlayerSystem player , Transform transform )
	{
		if( target != null ){
			transform.LookAt( target.position + lookatHeight );
			//transform.forward = target.forward;
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
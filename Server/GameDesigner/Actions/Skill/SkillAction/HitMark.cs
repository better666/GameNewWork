using UnityEngine;
using System.Collections.Generic;
using GameDesigner;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class HitMark : ColliderBehaviour
{
	public GameObject hitEffect = null;
	public float destroyTime = 0;
	public Vector3 point = Vector3.zero;

	public override void OnAllTriggerEnter (SkillCollider skill, Collider other, Transform parent)
	{
		if(other.transform.root!=skill.parent.root){
			if(hitEffect){
				Instantiate (hitEffect,point,Quaternion.identity);
			}
			Debug.Log (other.name);
			Destroy (skill.gameObject);
		}
	}


	/// <summary>
	/// 当进入触发器 ( other参数包含敌人,玩家,怪物等等 )
	/// </summary>

	override public void OnSkillTriggerEnter ( SkillCollider skill , GameDesigner.PlayerSystem other , Transform parent )
	{

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
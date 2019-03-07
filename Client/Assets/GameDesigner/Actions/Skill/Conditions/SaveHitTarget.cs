using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDesigner;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;

public class SaveHitTarget : ColliderBehaviour
{
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

	public override void OnSkillTriggerEnter (SkillCollider skill, GameDesigner.PlayerSystem other, Transform parent)
	{
		hit.target = other.transform;
	}
}
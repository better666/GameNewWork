using UnityEngine;
using System.Collections.Generic;
using GameDesigner;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class BulletCollision : ColliderBehaviour
{
	public float destroyTime = 0f;
	public GameObject hitEffect = null;

	public override void OnAllTriggerEnter (SkillCollider skill, Collider other, Transform parent)
	{
		if (other.transform.root != stateManager.transform) {
			if (hitEffect != null)
				Instantiate (hitEffect).transform.position = parent.position;
			Destroy (parent.gameObject, destroyTime);
		}
	}
}
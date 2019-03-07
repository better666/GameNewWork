using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameDesigner;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ZombieDeath : StateBehaviour
{
	public GameObject target = null;

	void Start()
	{
		target.SetActive (false); 
	}

	void LateUpdate()
	{
		if ( GetComponentInParent<GameDesigner.PlayerSystem>().Hp <= 0 & stateMachine.currState.stateID != state.stateID) {
			stateManager.OnEnterNextState (stateMachine.currState, stateMachine.states [state.stateID]);
			target.SetActive (true);
			stateManager.GetComponent<CapsuleCollider> ().enabled = false;
			stateManager.GetComponent<Rigidbody> ().isKinematic = true;
			stateManager.GetComponent<Animation> ().enabled = false;
			if(GetComponentInParent<GameDesigner.PlayerSystem>().isDeathDestroy){
				Destroy (stateManager.gameObject,5f);
			}
		}
	}

	void OnEnable()
	{
		if(stateManager.GetComponent<CapsuleCollider> ().enabled){
			target.SetActive (false);
		}
	}
}
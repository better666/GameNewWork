using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameDesigner;

public class PlayerJump : MonoBehaviour
{
	public Transform target = null;
	public StateManager stateManager = null;
	public KeyCode key = KeyCode.Space;
	public int[] interruptActionIndexs = new int[1];
	public int jumpStateID = 0;

	void Update()
	{
		if (Input.GetKeyDown (key) & isGround & InterruptAction)
			stateManager.OnEnterNextState (stateManager.stateMachine.currState,stateManager.stateMachine.states[jumpStateID]);
	}

	private bool InterruptAction{
		get{
			foreach(var id in interruptActionIndexs){
				if (stateManager.stateMachine.currState.stateID == id ) {
					return false;
				}
			}
			return true;
		}
	}

	private bool isGround{
		get{
			RaycastHit hit;
			if (Physics.Raycast(target.position , -target.up , out hit, 100)) {
				if(hit.distance<1f){
					return true;
				}
			}
			return false;
		}
	}
}
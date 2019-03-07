using UnityEngine;
using System.Collections.Generic;
using GameDesigner;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class JumpDown : StateBehaviour
{
	public Transform target = null;
	public Vector3 position = new Vector3 (0,0,0);
	public KeyCode key = KeyCode.Space;
	public int[] interruptActionIndexs = new int[1];

	private int index = 0;

	void Update()
	{
		if (Input.GetKeyDown (key) & isGround & InterruptAction)
			stateManager.OnEnterNextState (stateMachine.currState,stateMachine.states[index]);
	}

	private bool InterruptAction{
		get{
			foreach(var id in interruptActionIndexs){
				if (stateMachine.currState.stateID == id ) {
					index = id;
					return true;
				}
			}
			return false;
		}
	}

	private bool isGround{
		get{
			RaycastHit hit;
			if (Physics.Raycast(target.TransformPoint(position) , -target.up , out hit, 100)) {
				if(hit.distance<1f){
					return true;
				}
			}
			return false;
		}
	}
}
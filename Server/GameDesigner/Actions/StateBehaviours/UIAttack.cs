using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameDesigner;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIAttack : StateBehaviour
{
	public UISkillButton uiButton = null;

	public void Start()
	{
		UISkillButton[] skills = FindComponent.FindObjectsOfTypeAll<UISkillButton> (true);
		foreach(var button in skills){
			if(button.key == state.key){
				uiButton = button;
				break;
			}
		}
		if(uiButton == null)
			enabled = false;
	}

	public void Update()
	{
		if (uiButton.down & state.stateID != stateMachine.stateIndex) {
			stateManager.OnEnterNextState (stateMachine.currState, state);
			state.lengqueTime.time = 0;	
		}
	}
}
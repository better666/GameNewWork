using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDesigner;

public class InputEvent : TransitionBehaviour {

	public KeyMode modele =  KeyMode.KeyTrue;
	public KeyCode key = KeyCode.W;

	public override void OnTransitionUpdate (StateManager manager , State CurrState, State DstState, Transition transition, ref bool isEnterNextState)
	{
		switch(modele){
		case KeyMode.KeyTrue:
			if ( Input.GetKey (key) ) {
				transition.isEnterNextState = true;
			}
			break;
		case KeyMode.KeyFalse:
			if ( !Input.GetKey (key) ) {
				transition.isEnterNextState = true;
			}
			break;
		case KeyMode.Down:
			if ( Input.GetKeyDown (key) ) {
				transition.isEnterNextState = true;
			}
			break;
		case KeyMode.Up:
			if ( Input.GetKeyUp (key) ) {
				transition.isEnterNextState = true;
			}
			break;
		}
	}
}

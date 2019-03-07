using UnityEngine;
using System.Collections;
using GameDesigner;

public class FPS_FireToIdleEvent : TransitionBehaviour
{
    public KeyCode keyCode = KeyCode.Mouse0;

    public override void OnTransitionUpdate(StateManager manager, State state, State nextState, Transition transition, ref bool isEnterNextState)
    {
        if (!Input.GetKey(keyCode) & state.Action.animTime >= state.Action.animTimeMax - 5) {
            isEnterNextState = true;
        }
    }
}

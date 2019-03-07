using UnityEngine;
using System.Collections;
using GameDesigner;

public class FPS_InputMove:TransitionBehaviour
{
    [Header("true进入移动状态,false则进入待机状态")]
    public bool enterOrExitMove = true;

    public override void OnTransitionUpdate(StateManager manager, State state, State nextState, Transition transition, ref bool isEnterNextState)
    {
        if (enterOrExitMove) {
            if (InputControl.MoveDirection != Vector3.zero & InputControl.Instance.isGround) {
                isEnterNextState = true;
            }
        } else {
            if (InputControl.MoveDirection == Vector3.zero | !InputControl.Instance.isGround) {
                isEnterNextState = true;
            }
        }
    }
}

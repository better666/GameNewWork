using UnityEngine;
using System.Collections;
using GameDesigner;

public class FPS_Fire:ActionBehaviour
{
    public override void OnAnimationEventEnter(State state, StateAction action, float animEventTime)
    {
        WeaponsManager.Instance.UseWeapon.Fire();
    }
}

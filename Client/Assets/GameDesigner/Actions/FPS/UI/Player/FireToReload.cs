using UnityEngine;
using System.Collections;
using GameDesigner;

/// <summary>
/// 进入装弹状态
/// </summary>
public class FireToReload:TransitionBehaviour
{
    [Header("true当子弹为0时自动填充子弹 , false则检测子弹被打光时进入")]
    public bool autoReload = true;

    public override void OnTransitionUpdate(StateManager manager, State state, State nextState, Transition transition, ref bool isEnterNextState)
    {
        if (autoReload) {
            //如果是枪类型
            if (WeaponsManager.Instance.UseWeapon.weaponType == WeaponType.Gun) {
                //如果武器子弹等于0 或者 按下装弹键 则进入装弹状态
                if ((WeaponsManager.Instance.UseWeapon.quantity <= 0 &
                WeaponsManager.Instance.UseWeapon.total > 0) |
                (Input.GetKey(InputControl.Instance.ReloadKey) &
                WeaponsManager.Instance.UseWeapon.quantity != WeaponsManager.Instance.UseWeapon.cartrNumber &
                WeaponsManager.Instance.UseWeapon.total > 0)
                ) {
                    isEnterNextState = true;
                }
            }
        } else {
            //如果是枪类型
            if (WeaponsManager.Instance.UseWeapon.weaponType == WeaponType.Gun) {
                if (WeaponsManager.Instance.UseWeapon.quantity <= 0 & WeaponsManager.Instance.UseWeapon.total <= 0) {
                    isEnterNextState = true;
                }
            }
        }
    }
}
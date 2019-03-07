using UnityEngine;
using System.Collections;
using GameDesigner;

/// <summary>
/// 装子弹状态
/// </summary>
public class FPS_Reload:ActionBehaviour
{
    public override void OnAnimationEventEnter(State state, StateAction action, float animEventTime)
    {
        if (WeaponsManager.Instance.UseWeapon.total >= WeaponsManager.Instance.UseWeapon.cartrNumber) {
            //弹夹数量 - 当前子弹使用数 = 要填充的子弹数
            var over = WeaponsManager.Instance.UseWeapon.cartrNumber - WeaponsManager.Instance.UseWeapon.quantity;
            WeaponsManager.Instance.UseWeapon.total -= over;
            WeaponsManager.Instance.UseWeapon.quantity = WeaponsManager.Instance.UseWeapon.cartrNumber;
        } else {
            WeaponsManager.Instance.UseWeapon.quantity = WeaponsManager.Instance.UseWeapon.total;
            WeaponsManager.Instance.UseWeapon.total = 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsManager : MonoBehaviour
{
    public List<Weapon> weapons = new List<Weapon>();
    public Weapon UseWeapon = null;
    private static WeaponsManager instance;
    public static WeaponsManager Instance {
        get {
            if (instance==null) {
                instance = FindObjectOfType<WeaponsManager>();
            }
            return instance;
        }
    }

    private void Start ()
    {
        instance = this;
	}

    private void Update()
    {
        for (int i = 0; i < InputControl.Instance.keyCodes.Length; ++i) {
            if (Input.GetKeyDown(InputControl.Instance.keyCodes[i]) & weapons.Count >= i) {
                if (weapons[i].gameObject.activeSelf)
                    continue;
                foreach (var weap in weapons) {
                    weap.gameObject.SetActive(false);
                    weap.stateManager.stateMachine.currState.OnExitState();
                }
                weapons[i].gameObject.SetActive(true);
                weapons[i].stateManager.OnEnterNextState(weapons[i].stateIndex);
                UseWeapon = weapons[i];
                WeaponUI.Instance.Bullets.text = string.Format("{0}/{1}/{2}", UseWeapon.quantity, UseWeapon.cartrNumber,UseWeapon.total);
            }
        }
    }
}

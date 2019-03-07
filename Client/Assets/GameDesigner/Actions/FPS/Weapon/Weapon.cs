using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDesigner;

public class Weapon : MonoBehaviour
{
    //武器类型
    public WeaponType weaponType = WeaponType.None;
    public GameObject bulletObj;
    public Transform bulletPos;
    public Vector3 bulletDir = new Vector3(0,0,10000);
    public StateManager stateManager;
    public int stateIndex = 0;
    public Camera cameTran;
    public WeaponAttackLocation[] Locations = new WeaponAttackLocation[5];

    [Header("枪类属性")]
    public int quantity = 90;//子弹数量
    public int cartrNumber = 30;//弹夹容量
    public int total = 120;//子弹总量
    public float fireDistance = 100;//子弹射程

    private bool aiming;

    // Use this for initialization
    void Start ()
    {
        cameTran = Camera.main;
    }

	// Update is called once per frame
	void Update ()
    {
        if (aiming) {
            cameTran.fieldOfView -= 45 * Time.deltaTime / 0.5f;
            if (cameTran.fieldOfView < 45) {
                cameTran.fieldOfView = 45;
            }
            aiming = false;
        } else {
            cameTran.fieldOfView += 60 * Time.deltaTime * 3;
            if (cameTran.fieldOfView > 60) {
                cameTran.fieldOfView = 60;
            }
        }
    }

    public void Fire()
    {
        switch (WeaponsManager.Instance.UseWeapon.weaponType) {
            case WeaponType.Gun:
            GunFire();
            break;
            case WeaponType.Toss:
            ThrowFire();
            break;
        }
    }

    /// <summary>
    /// 枪射击 参数:sloshing 晃动偏移
    /// </summary>
    public void GunFire(float sloshing = 0.2f)
    {
        var dir = Vector3.forward;
        if (InputControl.Instance.downFire) {
            bulletPos.localPosition = new Vector3(0, 0, bulletPos.localPosition.z);
            InputControl.Instance.downFire = false;
        } else {
            bulletPos.localPosition = new Vector3(Random.Range(-sloshing, sloshing), Random.Range(-sloshing, sloshing), bulletPos.localPosition.z);
        }
        var obj = Instantiate(bulletObj, bulletPos.position, bulletPos.rotation);
        obj.GetComponent<Rigidbody>().AddRelativeForce(bulletDir);
        obj.GetComponent<FPS_BulletCollision>().weapon = this;
        CrossHairUI.SetCrossHair();
        aiming = true;
        quantity--;
        WeaponUI.Instance.Bullets.text = string.Format("{0}/{1}/{2}", quantity,cartrNumber,total);
    }

    /// <summary>
    /// 投掷武器类攻击
    /// </summary>
    public void ThrowFire()
    {
        if (quantity<=0) {
            quantity = total;
            total = 0;
        }
        var obj = Instantiate(bulletObj, bulletPos.position, bulletPos.rotation);
        obj.GetComponent<Rigidbody>().AddRelativeForce(bulletDir);
        obj.GetComponent<GrenadeExplosion>().weapon = this;
        quantity--;
        WeaponUI.Instance.Bullets.text = string.Format("{0}/{1}", quantity, total);
    }
}
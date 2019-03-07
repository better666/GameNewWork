using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponUI:MonoBehaviour
{
    private static WeaponUI instance;
    public static WeaponUI Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<WeaponUI>();
            }
            return instance;
        }
    }

    [Header("子弹数/弹夹数/子弹数量")]
    public Text Bullets;

    private void Start()
    {
        instance = this;
    }
}

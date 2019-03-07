using UnityEngine;
using System.Collections;

public class InputControl:MonoBehaviour
{
    public bool fire = false;
    public bool isGround = true;
    public bool jump = false;
    public bool downFire = false;

    public KeyCode[] keyCodes = new KeyCode[] {
        KeyCode.Alpha1,KeyCode.Alpha2,KeyCode.Alpha3,KeyCode.Alpha4
    };
    public KeyCode ReloadKey = KeyCode.R;

    public static Vector3 MoveDirection {
        get { return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); }
    }

    private static InputControl control;
    public static InputControl Instance {
        get {
            if (control==null) {
                control = FindObjectOfType<InputControl>();
                if (control==null) {
                    control = new GameObject("InputControl").AddComponent<InputControl>();
                }
            }
            return control;
        }
    }

    private void Awake()
    {
        control = this;
    }

    // Update is called once per frame
    void Update()
    {
        fire = Input.GetKey(KeyCode.Mouse0);
    }
}

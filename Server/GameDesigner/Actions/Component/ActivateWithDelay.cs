using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWithDelay : MonoBehaviour {

    public GameObject ActivateThis = null;
    public float delay = 1f;
    public bool Enabled = false;
    [Header("激活Tue还是销毁False")]
    public bool ActivateOrDestroy = true;
    private float delayTime = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        delayTime += Time.deltaTime;
        if (delayTime >= delay) {
            if (ActivateOrDestroy)
            {
                ActivateThis.SetActive(Enabled);
                delayTime = 0;
            }
            else
            {
                Destroy(ActivateThis);
            }
        }
    }

    private void OnEnable()
    {
        delayTime = 0;
    }
}

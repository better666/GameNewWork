using UnityEngine;
using System.Collections;

public class MouseLock : MonoBehaviour {

	public CursorLockMode cursorLockMode = CursorLockMode.Confined;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Cursor.visible = false;
		Cursor.lockState = cursorLockMode;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHotKey : MonoBehaviour {

	public KeyCode setings = KeyCode.Escape;
	public GameObject setingObject = null;
	public bool active = true;
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(setings)){
			setingObject.SetActive (active);
			if (active) {
				GameObject.FindObjectOfType<MouseLock> ().enabled = false;
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
			} else {
				GameObject.FindObjectOfType<MouseLock> ().enabled = true;
			}
		}
	}
}

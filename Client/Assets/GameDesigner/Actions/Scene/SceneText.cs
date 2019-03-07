using UnityEngine;
using System.Collections;

public class SceneText : MonoBehaviour {
    public Transform tarent;
    public Transform thisTarget;
	// Use this for initialization
	void Start () {
        thisTarget = transform;
        tarent = Camera.main.transform;
	}
	
	// Update is called once per frame
	void LateUpdate() {
        thisTarget.LookAt(tarent.position );
	}
}

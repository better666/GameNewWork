using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITopLookAtCamera : MonoBehaviour {

	public new Camera camera = null;

	public enum LookAtModel
	{
		forward,rotation,LookAt
	}

	public LookAtModel lookAtModel = LookAtModel.forward;

	// Use this for initialization
	void Start () {
		if( camera == null )
			camera = Camera.main;
	}
	
	// Update is called once per frame
	void LateUpdate() 
	{
		switch( lookAtModel )
		{
			case LookAtModel.forward:
				transform.forward = camera.transform.forward;
				break;
			case LookAtModel.LookAt:
				transform.rotation = camera.transform.rotation;
				break;
			case LookAtModel.rotation:
				transform.LookAt ( camera.transform.position );
				break;
		}
	}
}

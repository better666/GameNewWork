using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 激活ARPG相机
/// </summary>
public class ActivateARPGCamera : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		var arpg = GameObject.FindObjectOfType<ARPGcamera>();
		if(arpg!=null){
			arpg.target = transform;
			arpg.enabled = true;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class UISensitivityController : MonoBehaviour {
	public Slider slider = null;
	public MouseLook[] looks = new MouseLook[0];
	// Use this for initialization
	void Start () {
		slider = GetComponent<Slider> ();
		looks = GameObject.FindObjectsOfType<MouseLook> ();
	}

	public void UIUpdate()
	{
		foreach(var l in looks){
			l.sensitivityX = slider.value;
			l.sensitivityY = slider.value;
		}
	}
}

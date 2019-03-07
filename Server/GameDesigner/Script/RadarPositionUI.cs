using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarPositionUI : MonoBehaviour {

	private RectTransform rt = null;
	public Transform cameraRoto = null;
	// Use this for initialization
	void Start () {
		rt = GetComponent<RectTransform> ();	
	}
	
	// Update is called once per frame
	void Update () {
		rt.anchoredPosition = new Vector2 (-(cameraRoto.eulerAngles.y * 34 / 5),0);
	}
}

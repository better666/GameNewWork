using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressbarText : MonoBehaviour {

	public Text text = null;
	public Slider slider = null;

	// Update is called once per frame
	public void Update () {
		text.text = slider.value.ToString("F0");
	}
}

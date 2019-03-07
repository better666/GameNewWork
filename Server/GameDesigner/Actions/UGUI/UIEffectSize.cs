using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ParticleSystem))]
[RequireComponent(typeof(RectTransform))]
public class UIEffectSize : MonoBehaviour 
{
	public ParticleSystem ps = null;
	/// 自动自适应UI特效大小,当视窗改变时自动发送过来
	static public bool Scaler = false;

	// Use this for initialization
	void Start () 
	{
		Scaler = true;
	}
	
	// Update is called once per frame
	void LateUpdate() 
	{
		if( Scaler )
		{
			Scaler = false;

			if( ps == null )
			{
				ps = GetComponent<ParticleSystem>();
			}

			var main = ps.main;
			main.startSize3D = true;
			main.startSizeXMultiplier = GetComponent<RectTransform>().sizeDelta.x;
			main.startSizeYMultiplier = GetComponent<RectTransform>().sizeDelta.y;
		}
	}
}

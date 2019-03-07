using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Canvas))]
public class UICameraScaler : MonoBehaviour
{
	protected Canvas canvas = null;
	protected RectTransform rt = null;
	protected RectTransform scalerRt = null;

	// Use this for initialization
	void Start () 
	{
		canvas = GetComponent<Canvas>();
		rt = GetComponent<RectTransform>();
		scalerRt = GetComponent<CanvasScaler>().GetComponent<RectTransform>();
		if( canvas.renderMode != RenderMode.ScreenSpaceOverlay ){
			var go = new GameObject("_Canvas");
			//go.hideFlags = HideFlags.HideInInspector;
			var canva = go.AddComponent<Canvas>();
			canvas = canva;
			rt = canvas.GetComponent<RectTransform>();
			canva.renderMode = RenderMode.ScreenSpaceOverlay;
			var scaler = canva.gameObject.AddComponent<CanvasScaler>();
			scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
			scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
			scalerRt = scaler.GetComponent<RectTransform>();
		}
	}
	
	// Update is called once per frame
	void LateUpdate() 
	{
		if(rt.localScale.x < scalerRt.localScale.x)
		{
			canvas.worldCamera.orthographicSize += 0.5f;
			UIEffectSize.Scaler = true;
		}
		else if(rt.localScale.x > scalerRt.localScale.x)
		{
			canvas.worldCamera.orthographicSize -= 0.5f;
			UIEffectSize.Scaler = true;
		}
	}
}

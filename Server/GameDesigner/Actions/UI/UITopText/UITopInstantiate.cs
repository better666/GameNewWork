using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITopInstantiate : MonoBehaviour 
{
	public string Name = "龙兄の";
	public int lv = 18;
	public UITopTargetPosition uiTopPosition = null;
	public GameDesigner.PlayerSystem target = null;
	public Vector3 topPosition = new Vector3( 0 , 2.5f , 0 );
	public Vector3 rectTransformScale = new Vector3( 0.013f , 0.015f , 0.013f );

	// Use this for initialization
	void Start () 
	{
		if (uiTopPosition) {
			UITopTargetPosition uiTop = Instantiate<UITopTargetPosition> ( uiTopPosition , transform.parent.transform );
			uiTop.target = target ? target.transform : transform.parent.transform;
			uiTop.topPosition = topPosition;
			uiTop.rectTransformScale = rectTransformScale;
			UITopPlayerGUI topGUI = uiTop.GetComponentInChildren<UITopPlayerGUI> ();
			if( topGUI )
			{
				topGUI.Name = Name;
				topGUI.lv = lv;
				topGUI.SetLvOrName ( Name , lv );
			}
		}
	}
}

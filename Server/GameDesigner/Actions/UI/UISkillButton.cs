using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISkillButton : MonoBehaviour , IPointerDownHandler , IPointerUpHandler
{
	public KeyCode key = KeyCode.X;
	public bool down = false;

	public void OnPointerDown( PointerEventData data )
	{
		down = true;
	}

	public void OnPointerUp( PointerEventData data )
	{
		down = false;
	}
}

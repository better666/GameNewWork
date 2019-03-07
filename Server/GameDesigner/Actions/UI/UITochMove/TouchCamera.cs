using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent(typeof(Image))]
public class isTouchCamera : MonoBehaviour , IBeginDragHandler , IDragHandler , IEndDragHandler
{
	protected RectTransform 	rt;
	protected Vector3 			mosPos;
	public ARPGcamera			aRP = null;

	void Start () 
	{
		rt = GetComponent<RectTransform> ();
	}

	public void OnBeginDrag ( PointerEventData data )
	{
		SetTransformPos ( data );
	}

	public void OnDrag ( PointerEventData data )
	{
		SetTransformPos ( data );

        aRP.x += Input.GetAxis("Mouse X") * aRP.xSpeed * 0.02f;
        aRP.y -= Input.GetAxis("Mouse Y") * aRP.ySpeed * 0.02f;
	}

	public void OnEndDrag( PointerEventData data )
	{
		rt.anchoredPosition3D = Vector3.zero;
	}

	public void SetTransformPos ( PointerEventData data )
	{
		if ( RectTransformUtility.ScreenPointToWorldPointInRectangle ( rt , data.position , data.pressEventCamera , out mosPos ) )
		{
			rt.position = mosPos;
		}
	}
}
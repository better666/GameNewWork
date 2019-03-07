using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent(typeof(Image))]
public class DragSightDistance : MonoBehaviour , IBeginDragHandler , IDragHandler , IEndDragHandler
{
	protected RectTransform 	rt;
	protected Vector3 			mosPos;
	public ARPGcamera			arpg = null;

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

        arpg.distance -= (Input.GetAxis("Mouse Y") * Time.deltaTime) * 2f * Mathf.Abs(arpg.distance);
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
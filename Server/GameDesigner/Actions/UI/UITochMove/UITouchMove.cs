using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent(typeof(Image))]
public class UIisTouchMove : MonoBehaviour , IBeginDragHandler , IDragHandler , IPointerDownHandler , IPointerUpHandler
{
	protected RectTransform 	rt;
	public Camera				lookatCamera = null;
	protected Vector3 			mosPos;

	void Start () 
	{
		rt = GetComponent<RectTransform> ();
		if (rt == null)
			enabled = false;
		if (lookatCamera == null)
			lookatCamera = Camera.main;
	}

	void LateUpdate()
	{
		// 如果玩家为空,或玩家的禁止移动为真,或UI方向盘拖动时禁止这个脚本的移动时 返回 如果出现异常也返回
		try{ if (!PlayerMove.isTouch | !PlayerMove.instance.m_Player.isMove | PlayerMove.instance.m_Player.isDeath ) return; } catch { return; } 

		Vector3 moveDirection = new Vector3 ( 
			rt.anchoredPosition.x >= 70.0f ? 70.0f : rt.anchoredPosition.x <= -70.0f ? -70.0f : rt.anchoredPosition.x , 
			rt.anchoredPosition.y >= 70.0f ? 70.0f : rt.anchoredPosition.y <= -70.0f ? -70.0f : rt.anchoredPosition.y , 0 
		);

		PlayerMove.isTouchMove( PlayerMove.instance.target , moveDirection , PlayerMove.instance.m_Player.moveSpeed , lookatCamera ? lookatCamera.transform : lookatCamera.transform );
	}

	public void OnPointerDown ( PointerEventData data )
	{
		SetTransformPos ( data );
		PlayerMove.isTouch = true;
	}

	public void OnPointerUp ( PointerEventData data )
	{
		rt.anchoredPosition3D = Vector3.zero;
		PlayerMove.isTouch = false;
	}

	public void OnBeginDrag ( PointerEventData data )
	{
		SetTransformPos ( data );
	}

	public void OnDrag ( PointerEventData data )
	{
		SetTransformPos ( data );
	}

	public void SetTransformPos ( PointerEventData data )
	{
		if ( RectTransformUtility.ScreenPointToWorldPointInRectangle ( rt , data.position , data.pressEventCamera , out mosPos ) )
		{
			rt.position = mosPos;
		}
	}
}
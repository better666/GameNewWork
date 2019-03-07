using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITopManager : MonoBehaviour 
{
	/// 索引的Canvas类,因为Canvas组建有两个，只能识别其中一个 
	public Canvas m_Canvas = null;
	//相机有多个,所以自己设置一个
	public Camera m_Camera = null;

	static public UITopManager instantiate = null;

	void Awake ()
	{
		if( m_Canvas == null )
		{
			m_Canvas = transform.parent.GetComponent<Canvas>();

			if( m_Canvas == false )
			{
				Debug.Log ( "BUG!没有给Canvas组建赋值！,程序即将退出！" );
				#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
				return;
				#endif
			}
		}

		if( m_Camera == null )
		{
			m_Camera = GameObject.FindObjectOfType<Camera> ();
			m_Canvas.worldCamera = m_Camera;
		}

		instantiate = this;
	}
}

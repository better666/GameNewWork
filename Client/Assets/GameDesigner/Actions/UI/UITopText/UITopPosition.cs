using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class UITopPosition : MonoBehaviour 
{
    public UITopManager m_UITopManager = null;
    public Transform m_TargetPos = null;
    public RectTransform m_rt = null;

    public Vector3 OffsePos = Vector3.up;

	// Use this for initialization
	void Start () 
    {
		m_UITopManager = GameObject.FindObjectOfType<UITopManager> ();
        m_rt = GetComponent<RectTransform> ();

        if (m_UITopManager == null | m_TargetPos == null)
        {
            enabled = false; 
            return;
        }   

        transform.SetParent ( m_UITopManager.transform );

        m_rt.anchoredPosition3D = Vector3.zero;
        m_rt.localRotation = Quaternion.identity;
        m_rt.localScale = Vector3.one;
	}
	
	// Update is called once per frame
	void LateUpdate() 
    {
        Vector3 viewPos = m_UITopManager.m_Canvas.worldCamera.WorldToScreenPoint ( m_TargetPos.position + OffsePos );

        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_rt, new Vector2(viewPos.x, viewPos.y), m_UITopManager.m_Canvas.worldCamera, out viewPos))
        {
            m_rt.position = viewPos;
        }
        else//如果不在矩形内则初始化位置
        {
            m_rt.anchoredPosition3D = Vector3.zero;
            m_rt.localRotation = Quaternion.identity;
            m_rt.localScale = Vector3.one;
        }
	}
}

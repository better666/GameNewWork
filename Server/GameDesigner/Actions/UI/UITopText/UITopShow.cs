using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameDesigner;

[RequireComponent(typeof(RectTransform))]
public class UITopShow : MonoBehaviour 
{
	public Transform m_TargetPos = null;
	public RectTransform m_rt = null;

	public Text m_HitText = null;
	public Image m_HitImage = null;

	public Sprite m_WuLiGongJiImage = null;
	public Sprite m_MoFaGongJiImage = null;
	public Sprite m_ZhenShiGongJiImage = null;
	public Sprite m_BaoJiImage = null;

	public CloseState   m_ExitState = CloseState.Active;
	public Timer 	m_Time = new Timer ( 0 , 0.5f );

	public float size = 20;
	private Vector3 newPos = Vector3.zero;
	private float camDistance , dist;

	// Use this for initialization
	void Start () 
	{
		m_rt = GetComponent<RectTransform> ();

		Canvas canvas = null;
		Canvas[] canvases = FindObjectsOfType<Canvas>();

		for (int i = 0; i < canvases.Length; i++)
		{
			if(canvases[i].enabled && canvases[i].gameObject.activeSelf && canvases[i].renderMode == RenderMode.ScreenSpaceOverlay)
				canvas = canvases[i];
		}

		if( !canvas )
		{
			var canva = new GameObject("_Canvas").AddComponent<Canvas>();
			canva.renderMode = RenderMode.ScreenSpaceOverlay;
			var scaler = canva.gameObject.AddComponent<CanvasScaler>();
			scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
			scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
			canvas = canva;
		}

        if ( m_TargetPos == null)
            enabled = false; 

		transform.SetParent ( canvas.transform );
		m_rt.anchoredPosition3D = Vector3.zero;
		m_rt.localRotation = Quaternion.identity;
		m_rt.localScale = Vector3.one;

        OnEnable();
	}

    void OnEnable() 
    {
        newPos = new Vector3 (Random.Range (-.5f, .5f), Random.Range (1.5f, 2f), 0);
    }

    void OnDisable() 
    {
        
    }
		
	// Update is called once per frame
	void LateUpdate() 
	{
		camDistance = Vector3.Dot(m_TargetPos.position - Camera.main.transform.position, Camera.main.transform.forward);
		dist = size / camDistance;
		m_rt.localScale = new Vector3 (dist, dist , dist );
		m_rt.transform.position = Camera.main.WorldToScreenPoint ( m_TargetPos.position + newPos );

		if( m_Time.IsTimeOut )
		{
			switch( m_ExitState )
			{
			case CloseState.Destroy:
				Destroy( this.gameObject );
				break;
			case CloseState.Active:
				this.gameObject.SetActive(false);
				break;
			}
		}
	}
}

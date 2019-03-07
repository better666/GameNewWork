using UnityEngine;
using System.Collections;

/// <summary>
/// 粒子自适应矩形脚本
/// </summary>

[ExecuteInEditMode]
public class UISetEffectSize : MonoBehaviour 
{
	public RectTransform SetRt;[HideInInspector]
	public RectTransform MiMaRt;//自适应矩形必须是在主对象里面的才能设置好矩形位置，此变量自动设置
	public Vector2 RectOffset;
	public Vector3 PosOffset;
	public bool MinMaxRect = true;
	public bool MinMaxRectPos = false;

	/// <summary>
	/// 编辑器入口(初始化)
	/// </summary>
	public void InitStart () 
	{
		if( MinMaxRect == false ) return;

		if( SetRt == true )
		{
			this.transform.localScale = new Vector3( SetRt.rect.width + RectOffset.x , SetRt.rect.height + RectOffset.y , 0 );

			if( MinMaxRectPos == true )
			{
				if( MiMaRt == null )
				{
					RectTransform rt = new GameObject("MinMaxRect").AddComponent<RectTransform>();
					rt.SetParent( SetRt.transform );
					MiMaRt = rt;
					rt.localScale = Vector3.one;
					rt.anchoredPosition3D = Vector3.zero;
					rt.anchorMax = Vector2.one;
					rt.anchorMin = Vector2.zero;
					rt.offsetMax = Vector2.zero;
					rt.offsetMin = Vector2.zero;
					SetRt = rt;
				}
				this.transform.localPosition = new Vector3( SetRt.localPosition.x + PosOffset.x , SetRt.localPosition.y + PosOffset.y , PosOffset.z );
			}
		}
	}
	
	// 每一帧运行!
	void LateUpdate() 
	{
		InitStart ();
	}
}

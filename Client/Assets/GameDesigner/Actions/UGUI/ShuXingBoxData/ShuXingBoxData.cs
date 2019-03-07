using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

/// <summary>
/// 属性格子数据，这个类负责存放的装备类型，比如武器，鞋子，上衣等等
/// </summary>
public class ShuXingBoxData : GameData , IPointerDownHandler
{
	/// <summary> 当点击物品 </summary>
	
	public void OnPointerDown (PointerEventData data)
	{
		if (!goodsData.m_IsHasWuPin)//判断物品是否存在，存在则往下执行，否则退出
			return;
		
		m_XGameSystem.goodsData.SetShuXingGameDatas = goodsData;
		m_XGameSystem.m_ShuXingBoxData = this;

		if (Input.GetMouseButtonDown (0)) {
			//打开属性面板，公告栏面板 ，公告输入组件，关闭出售面板
			GouMaiMenu goumai = m_XGameSystem.goumaiMenu;
			goumai.gameObject.SetActive (true);
			goumai.gouMaiNameText.text = "卸下装备：";
			goumai.gouMaiJiaGeText.text = goodsData.m_WuPinName;

			//m_GongGaoUI.is_onbutt = true;//bug处理
			RectTransform gm = m_XGameSystem.goumaiMenu.GetComponent<RectTransform> ();
			Vector3 viewPos = m_XGameSystem.camera.ScreenToViewportPoint (Input.mousePosition);
			
			gm.SetParent (m_XGameSystem.canvas.transform);
			gm.gameObject.SetActive (true);
			
			if (viewPos.x < .4f) {
				gm.pivot = new Vector2 (1f, 0.5f);
				gm.position = transform.position;
				gm.anchoredPosition3D = new Vector3 (gm.anchoredPosition.x - 25f, 0, -10f);
			} else {
				gm.pivot = new Vector2 (0f, 0.5f);
				gm.position = transform.position;
				gm.anchoredPosition3D = new Vector3 (gm.anchoredPosition.x + 25f, 0, -10f);
			}

			OPenShuXingUI ();
		} else {
			m_XGameSystem.Yes ();
		}
	}

	public void OPenShuXingUI ()
	{
		if (!goodsData.m_IsHasWuPin)//判断物品是否存在，存在则往下执行，否则退出
			return;
		
		m_XGameSystem.goodsData.SetShuXingGameDatas = goodsData;
		
		RectTransform gm = m_XGameSystem.goodsInfo.GetComponent<RectTransform> ();
		GoodsInfoMenu xsdl = m_XGameSystem.goodsInfo.GetComponent<GoodsInfoMenu> ();
		Vector3 viewPos = m_XGameSystem.camera.ScreenToViewportPoint (Input.mousePosition);
		
		gm.SetParent (m_XGameSystem.canvas.transform);
		gm.gameObject.SetActive (true);
		xsdl.DataLog = goodsData;

		if (viewPos.x < .4f) {
			gm.pivot = new Vector2 (0f, .5f);
			gm.position = transform.position;
			gm.anchoredPosition3D = new Vector3 (gm.anchoredPosition.x + 25f, 0, -10f);
		} else {
			gm.pivot = new Vector2 (1f, .5f);
			gm.position = transform.position;
			gm.anchoredPosition3D = new Vector3 (gm.anchoredPosition.x - 25f, 0, -10f);
		}
	}

	void Start ()
	{
		goodsData.m_WuPinImage = transform.Find ("Image").GetComponent<Image> ();
	}
}

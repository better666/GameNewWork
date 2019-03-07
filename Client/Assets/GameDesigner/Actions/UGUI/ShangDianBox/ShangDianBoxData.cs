using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 商店格子数据,此类负责当用户点击购买物品
/// </summary>
public class ShangDianBoxData : GameData , IPointerDownHandler
{
	/// <summary> 当点击物品 </summary>

	public void OnPointerDown (PointerEventData data)
	{
		if (!goodsData.m_IsHasWuPin)//判断物品是否存在，存在则往下执行，否则退出
			return;
		
		m_XGameSystem.goodsData.SetShangDianGameDatas = goodsData;
		m_XGameSystem.m_ShangDianBoxData = this;
		
		if (Input.GetMouseButtonDown (0)) {
			//打开属性面板，公告栏面板 ，公告输入组件，关闭出售面板
			GouMaiMenu goumai = m_XGameSystem.goumaiMenu;
			goumai.gameObject.SetActive (true);
			goumai.gouMaiNameText.text = goodsData.m_WuPinName;
			goumai.gouMaiJiaGeText.text = "购买价格：" + goodsData.m_GouMaiJiaGeInt;

			m_XGameSystem.m_IsDownButtion = true;//bug处理
			RectTransform gm = m_XGameSystem.goumaiMenu.GetComponent<RectTransform> ();
			Vector3 viewPos = m_XGameSystem.camera.ScreenToViewportPoint (Input.mousePosition);
			
			gm.SetParent (m_XGameSystem.canvas.transform);
			gm.gameObject.SetActive (true);
			
			if (viewPos.x < .4f) {
				gm.pivot = new Vector2 (1f, 0.5f);
				gm.position = transform.position;
				gm.anchoredPosition = new Vector3 (gm.anchoredPosition.x - 25f, 0);
			} else {
				gm.pivot = new Vector2 (0f, 0.5f);
				gm.position = transform.position;
				gm.anchoredPosition = new Vector3 (gm.anchoredPosition.x + 25f, 0);
			}

			OPenShuXingUI ();
		} else {
			m_XGameSystem.Yes ();
		}
	}


	/// <summary>
	/// 打开属性详细面板
	/// </summary>

	public void OPenShuXingUI ()
	{
		if (!goodsData.m_IsHasWuPin)//判断物品是否存在，存在则往下执行，否则退出
			return;

		if (m_XGameSystem.m_IsDownButtion == false)
			m_XGameSystem.goodsData.SetShangDianGameDatas = goodsData;

		RectTransform gm = m_XGameSystem.goodsInfo.GetComponent<RectTransform> ();
		GoodsInfoMenu xsdl = m_XGameSystem.goodsInfo.GetComponent<GoodsInfoMenu> ();
		Vector3 viewPos = m_XGameSystem.camera.ScreenToViewportPoint (Input.mousePosition);

		gm.SetParent (m_XGameSystem.canvas.transform);
		gm.gameObject.SetActive (true);
		xsdl.DataLog = goodsData;
		
		if (viewPos.x < .4f) {
			gm.pivot = new Vector2 (0f, .5f);
			gm.position = transform.position;
			gm.anchoredPosition = new Vector2 (gm.anchoredPosition.x + 25f, 0);
		} else {
			gm.pivot = new Vector2 (1f, .5f);
			gm.position = transform.position;
			gm.anchoredPosition = new Vector2 (gm.anchoredPosition.x - 25f, 0);
		}
	}

	void Start ()
	{
		goodsData.m_WuPinSprite = transform.Find ("Image").GetComponent<Image> ().sprite;
	}
}

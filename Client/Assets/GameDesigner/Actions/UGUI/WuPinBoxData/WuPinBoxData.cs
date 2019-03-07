using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

/// <summary>
/// 物品格子数据，此类负责存放各种物品或各种装备的数据
/// </summary>
public class WuPinBoxData : GameData , IPointerDownHandler
{
	/// 更新物品数据
	public virtual void UpdateWuPinData ()
	{
		if (goodsData.m_WuPinInt <= 0) {
			goodsData.m_WuPinImage.sprite = goodsData.m_WuPinLocalSprite;
			goodsData.m_WuPinText.text = "";
			goodsData.m_IsHasWuPin = false;
		} else {
			goodsData.m_WuPinImage.sprite = goodsData.m_WuPinSprite;
			goodsData.m_WuPinText.text = goodsData.m_WuPinInt.ToString ();
			goodsData.m_IsHasWuPin = true;
		}
	}

	/// <summary> 当点击物品 </summary>
	
	public void OnPointerDown (PointerEventData data)
	{
		if (!goodsData.m_IsHasWuPin)//判断物品是否存在，存在则往下执行，否则退出
			return;
		
		m_XGameSystem.goodsData.SetWuPinGameDatas = goodsData;
		m_XGameSystem.m_WuPinBoxData = this;
		
		if (Input.GetMouseButtonDown (0)) {
			//打开属性面板，公告栏面板 ，公告输入组件，出售面板
			m_XGameSystem.goumaiMenu.gouMaiShuLiang.gameObject.SetActive (true);
			m_XGameSystem.goumaiMenu.gouMaiNameText.text = "是否使用：";
			m_XGameSystem.goumaiMenu.gouMaiJiaGeText.text = goodsData.m_WuPinName;
			m_XGameSystem.chushouMenu.chuShoujiageText.text = "出售价格：" + goodsData.m_ChuShouJiaGeInt;
			m_XGameSystem.chushouMenu.chuShouwupinIndex = m_XGameSystem.m_WuPinBoxData;

			m_XGameSystem.m_IsDownButtion = true;//bug处理
			RectTransform gm = m_XGameSystem.goumaiMenu.GetComponent<RectTransform> ();
			RectTransform cs = m_XGameSystem.chushouMenu.GetComponent<RectTransform> ();
			Vector3 viewPos = m_XGameSystem.camera.ScreenToViewportPoint (Input.mousePosition);
			
			gm.SetParent (m_XGameSystem.canvas.transform);
			gm.gameObject.SetActive (true);
			cs.gameObject.SetActive (true);
			
			if (viewPos.x < .4f) {
				gm.pivot = new Vector2 (1f, 0.5f);
				gm.position = transform.position;
				gm.anchoredPosition3D = new Vector3 (gm.anchoredPosition.x - 25f, -50, -10f);

				cs.pivot = new Vector2 (1f, 0.5f);
				cs.position = transform.position;
				cs.anchoredPosition3D = new Vector3 (cs.anchoredPosition.x - 25f, 150, -10f);
			} else {
				gm.pivot = new Vector2 (0f, 0.5f);
				gm.position = transform.position;
				gm.anchoredPosition3D = new Vector3 (gm.anchoredPosition.x + 25f, -50, -10f);

				cs.pivot = new Vector2 (0f, 0.5f);
				cs.position = transform.position;
				cs.anchoredPosition3D = new Vector3 (cs.anchoredPosition.x + 25f, 150, -10f);
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

		if (m_XGameSystem.m_IsDownButtion == false)
			m_XGameSystem.goodsData.SetWuPinGameDatas = goodsData;
		
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
}

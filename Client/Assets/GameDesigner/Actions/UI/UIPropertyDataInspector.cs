using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPropertyDataInspector : MonoBehaviour 
{
    public Text     HpText = null; /// 获得 或 设置 生命值滑动条                                                                    
    public Slider   HpSlider = null; /// 获得 或 设置 魔法值文本
    public Text     MpText = null; /// 获得 或 设置 魔法值滑动条
    public Slider   MpSlider = null; /// 获得 或 设置 物理攻击文本
    public Text     WuLiGongJiText = null; /// 获得 或 设置 魔法攻击文本
    public Text     MoFaGongJiText= null; /// 获得 或 设置 真实伤害
    public Text     ZhenShiShangHaiText = null; /// 获得 或 设置 物理防御
    public Text     WuLiFangYuText = null; /// 获得 或 设置 魔法防御
    public Text     MoFaFangYuText = null; /// 获得 或 设置 暴击伤害
    public Text     BaoJiShangHaiText = null; /// 获得 或 设置 暴击几率
    public Text     BaoJiJiLuText = null; /// 获得 或 设置 攻击速度
    public Text     AttackSpeedText = null; /// 获得 或 设置 移动速度
    public Text     MoveSpeedText = null; /// 获得 或 设置 冷却缩减
    public Text     LengQueCutText = null;
	public PropertyData data = new PropertyData();
	static public UIPropertyDataInspector _instance = null;
	static public UIPropertyDataInspector instance{
		get{
			if( _instance == null ){
				var datas = Resources.FindObjectsOfTypeAll <UIPropertyDataInspector>();
				foreach( var p in datas ){
					if( p.gameObject.scene.name == UnityEngine.SceneManagement.SceneManager.GetActiveScene().name ){
						_instance = p;
					}
				}
				if( _instance == null ){
					Canvas can = GameObject.FindObjectOfType<Canvas> ();
					if(can == null)
						can = new GameObject("_Canvas").AddComponent<Canvas>();
					_instance = new GameObject("_UIpropertyDataDataInspector").AddComponent<UIPropertyDataInspector>();
					_instance.transform.SetParent (can.transform);
				}
			}
			return _instance;
		}
	}

    /// <summary>
	/// 更新所有英雄属性面板的数值
	/// </summary>

	public void UpdateUIDateInspector ( PropertyData propertyData )
    {
		// 处理字符串GC(垃圾过多) , 如果属性值没有被更改,不需要更新属性文字在监视面板里
		if( data.hp == propertyData.hp & data.hpMax == propertyData.hpMax & data.mp == propertyData.mp &
			data.mpMax == propertyData.mpMax & data.wuLiGongJiLi == propertyData.wuLiGongJiLi & 
			data.moFaGongJiLi == propertyData.moFaGongJiLi & data.zhenShiAttack == propertyData.zhenShiAttack &
			data.wuLiFangYuLi == propertyData.wuLiFangYuLi & data.moFaFangYuLi == propertyData.moFaFangYuLi &
			data.baoJiLu == propertyData.baoJiLu & data.baoJiShangHai == propertyData.baoJiShangHai &
			data.attackSpeed == propertyData.attackSpeed & data.moveSpeed == propertyData.moveSpeed & 
			data.lengQueTime100 == propertyData.lengQueTime100
		)
		{
			return;
		}

		data.SetValue = propertyData;

		if (HpText) {
			HpText.text = "" + data.hp + " / " + data.hpMax;
		}

		if (HpSlider) {
			HpSlider.maxValue = data.hpMax;
			HpSlider.value = data.hp;
        }

		if (MpText) {
			MpText.text = "" + data.mp + " / " + data.mpMax;
		}

		if (MpSlider) {
			MpSlider.maxValue = data.mpMax;
			MpSlider.value = data.mp;
        }

		if (WuLiGongJiText) {
			WuLiGongJiText.text = data.wuLiGongJiLi.ToString ();
		}

		if (MoFaGongJiText) {
			MoFaGongJiText.text = data.moFaGongJiLi.ToString ();
		}

		if (ZhenShiShangHaiText) {
			ZhenShiShangHaiText.text = data.zhenShiAttack.ToString ();
		}

		if (WuLiFangYuText) {
			WuLiFangYuText.text = data.wuLiFangYuLi.ToString ();
		}

		if (MoFaFangYuText) {
			MoFaFangYuText.text = data.moFaFangYuLi.ToString ();
		}

		if (BaoJiJiLuText) {
			BaoJiJiLuText.text = data.baoJiLu.ToString () + "%";
		}

		if (BaoJiShangHaiText) {
			BaoJiShangHaiText.text = data.baoJiShangHai * 100 + "%";
		}

		if (AttackSpeedText) {
			AttackSpeedText.text = data.attackSpeed.ToString ();
		}

		if (MoveSpeedText) {
			MoveSpeedText.text = data.moveSpeed.ToString ();
		}

		if (LengQueCutText) {
			LengQueCutText.text = data.lengQueTime100.ToString () + "%";
		}
    }
}

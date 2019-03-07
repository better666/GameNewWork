using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOtherPropertyDataInspector : MonoBehaviour 
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

	static public PropertyData playerProperty = null;
	static public UIOtherPropertyDataInspector otherPropertyDataInspector = null;

	// Use this for initialization
	void Start () 
	{
		otherPropertyDataInspector = this;

		if (HpText == null)
			DestroyImmediate( this , true );
	}

	// Update is called once per frame
	void LateUpdate() 
	{
		if( playerProperty != null )
		{
			UpdateUIDateInspector( this , playerProperty );
		}
	}

	void OnEnable() 
	{
		Start();
	}

	void OnDisable() 
	{
		enabled = true;
	}

	/// <summary>
	/// 更新所有英雄属性面板的数值
	/// </summary>

	static public void UpdateUIDateInspector ( UIOtherPropertyDataInspector proInspector , PropertyData pro )
	{
		if (proInspector.HpText) {
			proInspector.HpText.text = "" + pro.hp + " / " + pro.hpMax;
		}

		if (proInspector.HpSlider) {
			proInspector.HpSlider.maxValue = pro.hpMax;
			proInspector.HpSlider.value = pro.hp;
		}

		if (proInspector.MpText) {
			proInspector.MpText.text = "" + pro.mp + " / " + pro.mpMax;
		}

		if (proInspector.MpSlider) {
			proInspector.MpSlider.maxValue = pro.mpMax;
			proInspector.MpSlider.value = pro.mp;
		}

		if (proInspector.WuLiGongJiText) {
			proInspector.WuLiGongJiText.text = pro.wuLiGongJiLi.ToString ();
		}

		if (proInspector.MoFaGongJiText) {
			proInspector.MoFaGongJiText.text = pro.moFaGongJiLi.ToString ();
		}

		if (proInspector.ZhenShiShangHaiText) {
			proInspector.ZhenShiShangHaiText.text = pro.zhenShiAttack.ToString ();
		}

		if (proInspector.WuLiFangYuText) {
			proInspector.WuLiFangYuText.text = pro.wuLiFangYuLi.ToString ();
		}

		if (proInspector.MoFaFangYuText) {
			proInspector.MoFaFangYuText.text = pro.moFaFangYuLi.ToString ();
		}

		if (proInspector.BaoJiJiLuText) {
			proInspector.BaoJiJiLuText.text = pro.baoJiLu.ToString () + "%";
		}

		if (proInspector.BaoJiShangHaiText) {
			proInspector.BaoJiShangHaiText.text = pro.baoJiShangHai * 100 + "%";
		}

		if (proInspector.AttackSpeedText) {
			proInspector.AttackSpeedText.text = pro.attackSpeed.ToString ();
		}

		if (proInspector.MoveSpeedText) {
			proInspector.MoveSpeedText.text = pro.moveSpeed.ToString ();
		}

		if (proInspector.LengQueCutText) {
			proInspector.LengQueCutText.text = pro.lengQueTime100.ToString () + "%";
		}
	}
}

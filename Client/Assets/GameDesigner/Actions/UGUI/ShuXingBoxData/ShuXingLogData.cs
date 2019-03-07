using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GameDesigner;

/// <summary>
/// 属性面板数据输出，就是属性栏面板的面板详细值,此类负责调式输出面板的详细数据
/// </summary>
public class ShuXingLogData : GoodsInfoMenu
{
	/// <summary>
	/// 设置属性面板所显示的可视化的全部数据(增加的属性值)
	/// </summary>
	
	public virtual void AddShuXingLogGameDatas (GoodsData value)
	{
		SystemType.SetFieldValue (goodsData.data, value.data, true);	//BUG ==> 避免绑定赋值的父类变量数据
		LOGShuXingDatas ();

		//Debug.Log ("新代码==> UIPropertyDataInspector组件显示属性数据");
	}

	/// <summary>
	/// 设置属性面板所显示的可视化的全部数据(减少的属性值)
	/// </summary>
	
	public virtual void CutShuXingLogGameDatas (GoodsData value)
	{
		SystemType.SetFieldValue (goodsData.data, value.data, false);	//BUG ==> 避免绑定赋值的父类变量数据
		LOGShuXingDatas ();

		//Debug.Log ("新代码==> UIPropertyDataInspector组件显示属性数据");
	}

	/// <summary>
	/// 在属性面板显示出物品属性值XGameData（我的游戏数据）
	/// </summary>

	public virtual void LOGShuXingDatas ()
	{
		生命值.text = "生命值 " + goodsData.hp + "/" + goodsData.hpMax;
		生命回复.text = "生命回复 " + goodsData.hpAdd;
		魔法值.text = "魔法值 " + goodsData.mp + "/" + goodsData.mpMax;	
		魔法回复.text = "魔法回复 " + goodsData.mpAdd;
		物理攻击.text = "物理攻击 " + goodsData.wuLiGongJiLi.ToString ();	
		物理防御.text = "物理防御 " + goodsData.wuLiFangYuLi + "/" + goodsData.wuLiFangYuLiMax;	
		物理穿透.text = "物理穿透 " + goodsData.wuLiChuanTou.ToString ();	
		魔法攻击.text = "魔法攻击 " + goodsData.moFaGongJiLi.ToString ();	
		魔法防御.text = "魔法防御 " + goodsData.moFaFangYuLi + "/" + goodsData.moFaFangYuLiMax;	
		魔法穿透.text = "魔法穿透 " + goodsData.moFaChuanTou.ToString ();		
		真实伤害.text = "真实伤害 " + goodsData.zhenShiAttack.ToString ();	
		暴击率.text = "暴击率 " + goodsData.baoJiLu.ToString ();		
		暴击伤害.text = "暴击伤害 " + goodsData.baoJiShangHai + "/2.5";		
		攻击速度.text = "攻击速度 " + goodsData.attackSpeed + "/" + goodsData.attackSpeedMax;	
		移动速度.text = "移动速度 " + goodsData.moveSpeed + "/" + goodsData.moveSpeedMax;	
		冷却缩减.text = "冷却缩减 " + goodsData.lengQueTime100 + "/100";
	}
}

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 物品信息菜单类
/// </summary>

public class GoodsInfoMenu : MonoBehaviour
{
	public GoodsData goodsData;

	public Text 生命值 = null;
	public Text 生命回复 = null;
	public Text 魔法值 = null;
	public Text 魔法回复 = null;
	public Text 物理攻击 = null;
	public Text 物理防御 = null;
	public Text 物理穿透 = null;
	public Text 物穿防御 = null;
	public Text 魔法攻击 = null;
	public Text 魔法防御 = null;
	public Text 魔法穿透 = null;
	public Text 魔穿防御 = null;
	public Text 真实伤害 = null;
	public Text 暴击率 = null;
	public Text 暴击伤害 = null;
	public Text 攻击速度 = null;
	public Text 移动速度 = null;
	public Text 冷却缩减 = null;
	
	public Text m_SwordNameText = null;
	public Text m_SwordTypeText = null;
	public Text m_SwordPinJiText = null;
	public Text m_SwordTextText = null;

	/// <summary>
	/// 在显示面板显示出物品属性值XGameData（我的游戏数据）
	/// </summary>
	
	public virtual GoodsData DataLog {
		set{
			生命值.text = "生命值 " + value.hp + "/" + value.hpMax;
			生命回复.text = "生命回复 " + value.hpAdd;
			魔法值.text = "魔法值 " + value.mp + "/" + value.mpMax;	
			魔法回复.text = "魔法回复 " + value.mpAdd;
			物理攻击.text = "物理攻击 " + value.wuLiGongJiLi.ToString ();	
			物理防御.text = "物理防御 " + value.wuLiFangYuLi + "/" + value.wuLiFangYuLiMax;	
			物理穿透.text = "物理穿透 " + value.wuLiChuanTou.ToString ();	
			魔法攻击.text = "魔法攻击 " + value.moFaGongJiLi.ToString ();	
			魔法防御.text = "魔法防御 " + value.moFaFangYuLi + "/" + value.moFaFangYuLiMax;	
			魔法穿透.text = "魔法穿透 " + value.moFaChuanTou.ToString ();	
			真实伤害.text = "真实伤害 " + value.zhenShiAttack.ToString ();	
			暴击率.text = "暴击率 " + value.baoJiLu.ToString ();		
			暴击伤害.text = "暴击伤害 " + value.baoJiShangHai + "/2.5";		
			攻击速度.text = "攻击速度 " + value.attackSpeed + "/" + value.attackSpeedMax;	
			移动速度.text = "移动速度 " + value.moveSpeed + "/" + value.moveSpeedMax;	
			冷却缩减.text = "冷却缩减 " + value.lengQueTime100 + "/100";
			
			m_SwordNameText.text = value.m_WuPinName.ToString ();		
			m_SwordTypeText.text = value.m_SwordType.ToString ();		
			m_SwordPinJiText.text = value.m_SwordPinJi.ToString ();	
			m_SwordTextText.text = value.m_SwordText.ToString ();		
		}
	}
}

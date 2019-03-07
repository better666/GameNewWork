using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GameDesigner;

/// <summary>
/// 操作UI状态
/// </summary>
public enum ZhuangTaiUI
{
	///空状态
	Null,

	///购买
	goumai,

	///装备	
	zhuangbei,

	///出售
	chushou,

	///卸下
	xiexia_zhuangbei,

	///掉落物品装备
	diaoluo_zhuangbei,

	///掉落金币
	diaoluo_jinbi
}

/// <summary>
/// 物品类型
/// </summary>
public enum WuPinTag
{
	///空类型
	Null,

	///武器
	WuQi,

	///头肩
	TouJian,

	///上衣
	ShangYi,

	///腰带
	YaoDai,

	///下装
	XiaZhuang,

	///鞋子
	XieZi,

	///生命值
	hp,

	///魔法值
	MP,

	///移动速度
	Speed,

	///攻击速度
	Attack,

	///Buff效果
	Buff
}

/// <summary> 整个XGAME的UI游戏数据 </summary>
[System.Serializable]
public class GoodsData
{
	public string 物品名称 = "洛兰之刃";
	public bool 物品是否存在 = false;
	public int 物品数量 = 0;
	public Sprite 物品默认贴图 = null;
	public Sprite 物品贴图 = null;
	public Image 物品贴图组建 = null;
	public Text 物品数量组建 = null;
	public WuPinTag 物品类型 = WuPinTag.Null;
	public ZhuangTaiUI 物品状态 = ZhuangTaiUI.Null;
	public int 掉落金币 = 520;
	public int 武器强化等级 = 0;
	public int 购买价格 = 1000;
	public int 回收价格 = 100;

	public PropertyData 物品属性 = new PropertyData ();

	public string 武器类型 = "普通武器";
	public string 武器品级 = "最上级";
	public string 物品介绍 = "这是一把生锈的古兰无名战士之刃！";

	/// <summary> 物品名称 </summary>
	public string m_WuPinName {
		get{ return 物品名称; }
		set{ 物品名称 = value; }
	}

	/// <summary> 物品是否存在 </summary>
	public bool m_IsHasWuPin {
		get{ return 物品是否存在; }
		set{ 物品是否存在 = value; }
	}

	/// <summary> 物品自身贴图 </summary>
	public Sprite m_WuPinLocalSprite {
		get{ return 物品默认贴图; }
		set{ 物品默认贴图 = value; }
	}

	/// <summary> 物品贴图 </summary>
	public Sprite m_WuPinSprite {
		get{ return 物品贴图; }
		set{ 物品贴图 = value; }
	}

	/// <summary> 物品贴图组建 </summary>
	public Image m_WuPinImage {
		get{ return 物品贴图组建; }
		set{ 物品贴图组建 = value; }
	}

	/// <summary> 物品数量 </summary>
	public int m_WuPinInt {
		get{ return 物品数量; }
		set{ 物品数量 = value; }
	}

	/// <summary> 物品数量组建 </summary>
	public Text m_WuPinText {
		get{ return 物品数量组建; }
		set{ 物品数量组建 = value; }
	}

	/// <summary> 物品类型 </summary>
	public WuPinTag m_WuPinTag {
		get{ return 物品类型; }
		set{ 物品类型 = value; }
	}

	/// <summary> 物品状态，可通过公告栏输出以下信息：出售，戴上，查看属性 </summary>
	public ZhuangTaiUI m_WuPinState {
		get{ return 物品状态; }
		set{ 物品状态 = value; }
	}

	/// <summary> 掉落金币 </summary>
	public int m_OutJinBi {
		get{ return 掉落金币; }
		set{ 掉落金币 = value; }
	}

	/// <summary> 武器强化等级 </summary>
	public int m_QiangHua_Sword {
		get{ return 武器强化等级; }
		set{ 武器强化等级 = value; }
	}

	/// <summary> 购买价格 </summary>
	public int m_GouMaiJiaGeInt {
		get{ return 购买价格; }
		set{ 购买价格 = value; }
	}

	/// <summary> 回收价格 </summary>
	public int m_ChuShouJiaGeInt {
		get{ return 回收价格; }
		set{ 回收价格 = value; }
	}

	/// <summary> 此物品提升的属性数据值 </summary>
	public PropertyData data {
		get{ return 物品属性; }
		set{ 物品属性 = value; }
	}

	/// <summary> 武器类型 </summary>
	public string m_SwordType {
		get{ return 武器类型; }
		set{ 武器类型 = value; }
	}

	/// <summary> 武器品级 </summary>
	public string m_SwordPinJi {
		get{ return 武器品级; }
		set{ 武器品级 = value; }
	}

	/// <summary> 物品介绍 </summary>
	public string m_SwordText {
		get{ return 物品介绍; }
		set{ 物品介绍 = value; }
	}

	//------------------------------------数据-----------------------------------------//

	public float hp {
		get{ return data.hp; }
		set{ data.hp = data.hp > data.hpMax ? data.hpMax : value; }
	}

	public float hpMax {
		get{ return data.hpMax; }
		set{ data.hpMax = value; }
	}

	public float hpAdd {
		get{ return data.hpAdd; }
		set{ data.hpAdd = value; }
	}

	public float mp {
		get{ return data.mp; }
		set{ data.mp = value; }
	}

	public float mpMax {
		get{ return data.mpMax; }
		set{ data.mpMax = value; }
	}

	public float mpAdd {
		get{ return data.hp; }
		set{ data.hp = value; }
	}

	public float lv {
		get{ return data.lv; }
		set{ data.lv = value; }
	}

	public float lvMax {
		get{ return data.lvMax; }
		set{ data.lvMax = value; }
	}

	public float exp {
		get{ return data.exp; }
		set{ data.exp = value; }
	}

	public float expMax {
		get{ return data.expMax; }
		set{ data.expMax = value; }
	}

	public float wuLiGongJiLi {
		get{ return data.wuLiGongJiLi; }
		set{ data.wuLiGongJiLi = value; }
	}

	public float wuLiFangYuLi {
		get{ return data.wuLiFangYuLi; }
		set{ data.wuLiFangYuLi = value; }
	}

	public float wuLiFangYuLiMax {
		get{ return data.wuLiFangYuLiMax; }
		set{ data.wuLiFangYuLiMax = value; }
	}

	public float wuLiChuanTou {
		get{ return data.wuLiChuanTou; }
		set{ data.wuLiChuanTou = value; }
	}

	public float moFaGongJiLi {
		get{ return data.moFaGongJiLi; }
		set{ data.moFaGongJiLi = value; }
	}

	public float moFaFangYuLi {
		get{ return data.moFaFangYuLi; }
		set{ data.moFaFangYuLi = value; }
	}

	public float moFaFangYuLiMax {
		get{ return data.moFaFangYuLiMax; }
		set{ data.moFaFangYuLiMax = value; }
	}

	public float moFaChuanTou {
		get{ return data.moFaChuanTou; }
		set{ data.moFaChuanTou = value; }
	}

	public float zhenShiAttack {
		get{ return data.zhenShiAttack; }
		set{ data.zhenShiAttack = value; }
	}

	public float attackSpeed {
		get{ return data.attackSpeed; }
		set{ data.attackSpeed = value; }
	}

	public float attackSpeedMax {
		get{ return data.attackSpeedMax; }
		set{ data.attackSpeedMax = value; }
	}

	public float attackSpeed100 {
		get{ return data.attackSpeed100; }
		set{ data.attackSpeed100 = value; }
	}

	public float moveSpeed {
		get{ return data.moveSpeed; }
		set{ data.moveSpeed = value; }
	}

	public float moveSpeedMax {
		get{ return data.moveSpeedMax; }
		set{ data.moveSpeedMax = value; }
	}

	public float moveSpeed100 {
		get{ return data.moveSpeed100; }
		set{ data.moveSpeed100 = value; }
	}

	public float lengQueTime100 {
		get{ return data.lengQueTime100; }
		set{ data.lengQueTime100 = value; }
	}

	public float baoJiLu {
		get{ return data.baoJiLu; }
		set{ data.baoJiLu = value; }
	}

	public float baoJiLuValue {
		get{ return data.baoJiLuValue; }
		set{ data.baoJiLuValue = value; }
	}

	public float baoJiShangHai {
		get{ return data.baoJiShangHai; }
		set{ data.baoJiShangHai = value; }
	}

	public float baoJiShangHaiMax {
		get{ return data.baoJiShangHaiMax; }
		set{ data.baoJiShangHaiMax = value; }
	}

	/// <summary>
	/// 设置整个游戏数据类的值XGameData（我的游戏数据）
	/// </summary>

	public virtual GoodsData SetXGameData {
		set {
			this.m_WuPinName = value.m_WuPinName;		//物品名称
			this.m_IsHasWuPin = value.m_IsHasWuPin;		//物品是否存在
			this.m_WuPinSprite = value.m_WuPinSprite;     	//物品贴图
			this.m_WuPinImage = value.m_WuPinImage;		//物品贴图组建
			this.m_WuPinInt = value.m_WuPinInt;			//物品数量
			this.m_WuPinText = value.m_WuPinText;		//物品数量组建
			this.m_WuPinTag = value.m_WuPinTag;			//物品类型
			this.m_WuPinState = value.m_WuPinState;		//物品状态，可通过公告栏输出以下信息：出售，戴上，查看属性
			this.m_QiangHua_Sword = value.m_QiangHua_Sword;	//武器强化等级
			this.m_GouMaiJiaGeInt = value.m_GouMaiJiaGeInt;	//购买价格
			this.m_ChuShouJiaGeInt = value.m_ChuShouJiaGeInt;	//回收价格

			this.m_SwordType = value.m_SwordType;		//武器类型
			this.m_SwordPinJi = value.m_SwordPinJi;		//武器品级
			this.m_SwordText = value.m_SwordText;		//武器简介

			SystemType.SetFieldValue (this.data, value.data); 	//此物品提升属性数据值	
		}
		get {
			return this;
		}
	}

	/// <summary>
	/// 设置商店数据类的值XGameData（我的游戏数据）
	/// </summary>
	
	public virtual GoodsData SetShangDianGameDatas {
		set {
			SystemType.SetFieldValue (this, value, new string[]
				{ "物品默认贴图", "物品贴图组建", "物品数量", "物品数量组建" } 
			); //此物品提升属性数据值	
		}
	}

	/// <summary>
	/// 设置物品数据类的值XGameData（我的游戏数据）
	/// </summary>
	
	public virtual GoodsData SetWuPinGameDatas {
		set {
			this.SetShangDianGameDatas = value;
			this.m_WuPinState = ZhuangTaiUI.zhuangbei;	//戴上
		}
	}

	/// <summary>
	/// 设置属性数据类的值XGameData（我的游戏数据）
	/// </summary>
	
	public virtual GoodsData SetShuXingGameDatas {
		set {
			this.SetShangDianGameDatas = value;
			this.m_WuPinState = ZhuangTaiUI.xiexia_zhuangbei;	//卸下装备
		}
	}

	/// <summary>
	/// 只设置武器数据
	/// </summary>
	
	public virtual GoodsData SetSwordGameDatas {
		set {
			SystemType.SetFieldValue (this.data, value.data); //此物品提升属性数据值	
		}
	}

	/// <summary>
	/// 只设置一些物品介绍或可视化的说明(跟武器在显示面板的文字相关)
	/// </summary>
	
	public virtual GoodsData SetTextGameDatas {
		set {
			this.m_WuPinName = value.m_WuPinName;		//武器名称
			this.m_SwordType = value.m_SwordType;		//武器类型
			this.m_SwordPinJi = value.m_SwordPinJi;		//武器品级
			this.m_SwordText = value.m_SwordText;		//武器简介
		}
	}

	/// <summary>
	/// 增加游戏数据的值BUFF物品（增加的游戏数据值）
	/// </summary>
	
	public virtual GoodsData AddXGameDatas {
		set {
			SystemType.SetFieldValue (this.data, this.data, true); //此物品提升属性数据值	
		}
	}

	/// <summary>
	/// 减少游戏数据的值BUFF物品结束（减少的游戏数据值BUFF物品结束）
	/// </summary>
	
	public virtual GoodsData CutXGameDatas {
		set {
			SystemType.SetFieldValue (this.data, this.data, false); //此物品提升属性数据值	
		}
	}
}

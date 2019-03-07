using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDesigner;

/// 选择玩家属性状态
public enum SelectProperty
{
	/// 自身属性
	LocalProperty ,
	/// 其他玩家或动类属性
	OtherProperty
}

/// 攻击类型
public enum AttackType
{
	/// 物理攻击类型
	WuLi,
	/// 魔法攻击类型
	MoFa,
	/// 真实攻击类型
	ZhenShi,
    /// 物理魔法攻击类型
    WuLiMoFa,
    /// 物理和真实伤害攻击类型
    WuLiZhenShi,
    /// 魔法和真实伤害攻击类型
    MoFaZhenShi,
    /// 物理魔法真实伤害攻击类型
    WuLiMoFaZhenShi,
}

/// UI 打开类型
public enum UITopOpenType
{
	/// 对象池打开或关闭来启用对象
	SetActive , 
	/// 实例化对象
	Instantiate
}

/// <summary>
/// 对象识别标签
/// </summary>

public enum TargetTag
{
    /// 小兵
    Creeps,
    /// 英雄
    Hero,
    /// 防御塔
    DefenseTower,
    /// 水晶
    Crystal,
    /// 野怪
    Monster,
    /// 全部
    All,
    /// 空对象
    Null,
}

namespace GameDesigner
{
	public class PlayerSystem : Net.Client.NetBehaviour
	{
		public string			角色名称 = "侠客";
		public SelectProperty 	属性设置 = SelectProperty.LocalProperty;
		public PropertyData 	属性数值 = new PropertyData();
		public PropertyData[] 	升级增加属性 = new PropertyData[18];

		public TargetTag        对象种类 = TargetTag.Hero;
		public bool 			是否死亡 = false;
		public bool 			是否移动 = true;
		public bool 			播放动画 = true;
		public bool             是否攻击 = false;
		public bool				是否暴击 = false;
		public bool				死亡销毁 = false;
		public PlayerSystem		攻击对象 = null;
		public UITopShow 		伤害显示 = null;

        public string Name{
			get{ return 角色名称; }
			set{ 角色名称 = value; }
		}

        public string PlayerName { get { return Name; } set { Name = value; } }

        /// 选择玩家属性
        public SelectProperty SelePlayPry 
		{
			get{ return 属性设置; }
			set{ 属性设置 = value; }
		}

		/// 属性数值
		public PropertyData Property 
		{ 
			get{ return 属性数值; } 
			set{ 属性数值 = value; } 
		}

		/// 升级增加属性值
		public PropertyData[] LvUpPropertys 
		{ 
			get{ return 升级增加属性; } 
			set{ 升级增加属性 = value; } 
		}

		public float Hp
		{
			get{ return Property.hp; }
			set{ Property.hp = Property.hp > Property.hpMax ? Property.hpMax : value; }
		}

		public float HpMax
		{
			get{ return Property.hpMax; }
			set{ Property.hpMax = value; }
		}

		public float HpAdd
		{
			get{ return Property.hpAdd; }
			set{ Property.hpAdd = value; }
		}

		public float Mp
		{
			get{ return Property.mp; }
			set{ Property.mp = value; }
		}

		public float mpMax
		{
			get{ return Property.mpMax; }
			set{ Property.mpMax = value; }
		}

		public float mpAdd
		{
			get{ return Property.hp; }
			set{ Property.hp = value; }
		}

		public float lv
		{
			get{ return Property.lv; }
			set{ Property.lv = value; }
		}

		public float lvMax
		{
			get{ return Property.lvMax; }
			set{ Property.lvMax = value; }
		}

		public float exp
		{
			get{ return Property.exp; }
			set{ Property.exp = value; }
		}

		public float expMax
		{
			get{ return Property.expMax; }
			set{ Property.expMax = value; }
		}

		public float wuLiGongJiLi
		{
			get{ return Property.wuLiGongJiLi; }
			set{ Property.wuLiGongJiLi = value; }
		}

		public float wuLiFangYuLi
		{
			get{ return Property.wuLiFangYuLi; }
			set{ Property.wuLiFangYuLi = value; }
		}

		public float wuLiFangYuLiMax
		{
			get{ return Property.wuLiFangYuLiMax; }
			set{ Property.wuLiFangYuLiMax = value; }
		}

		public float wuLiChuanTou
		{
			get{ return Property.wuLiChuanTou; }
			set{ Property.wuLiChuanTou = value; }
		}

		public float moFaGongJiLi
		{
			get{ return Property.moFaGongJiLi; }
			set{ Property.moFaGongJiLi = value; }
		}

		public float moFaFangYuLi
		{
			get{ return Property.moFaFangYuLi; }
			set{ Property.moFaFangYuLi = value; }
		}

		public float moFaFangYuLiMax
		{
			get{ return Property.moFaFangYuLiMax; }
			set{ Property.moFaFangYuLiMax = value; }
		}

		public float moFaChuanTou
		{
			get{ return Property.moFaChuanTou; }
			set{ Property.moFaChuanTou = value; }
		}

		public float zhenShiAttack
		{
			get{ return Property.zhenShiAttack; }
			set{ Property.zhenShiAttack = value; }
		}

		public float attackSpeed
		{
			get{ return Property.attackSpeed; }
			set{ Property.attackSpeed = value; }
		}

		public float attackSpeedMax
		{
			get{ return Property.attackSpeedMax; }
			set{ Property.attackSpeedMax = value; }
		}

		public float attackSpeed100
		{
			get{ return Property.attackSpeed100; }
			set{ Property.attackSpeed100 = value; }
		}

		public float moveSpeed{
			get{ 
				Property.moveSpeed100 = Property.moveSpeed / moveSpeedMax * 100; 
				return (Property.moveSpeed <= 0) ? 0 : Property.moveSpeed;
			}
		}

		public float moveSpeedMax
		{
			get{ return Property.moveSpeedMax; }
			set{ Property.moveSpeedMax = value; }
		}

		public float moveSpeed100
		{
			get{ return Property.moveSpeed100; }
			set{ Property.moveSpeed100 = value; }
		}

		public float lengQueTime100
		{
			get{ return Property.lengQueTime100; }
			set{ Property.lengQueTime100 = value; }
		}

		public float baoJiLu
		{
			get{ return Property.baoJiLu; }
			set{ Property.baoJiLu = value; }
		}

		public float baoJiLuValue
		{
			get{ return Property.baoJiLuValue; }
			set{ Property.baoJiLuValue = value; }
		}

		public float baoJiShangHai
		{
			get{ return Property.baoJiShangHai; }
			set{ Property.baoJiShangHai = value; }
		}

		public float baoJiShangHaiMax
		{
			get{ return Property.baoJiShangHaiMax; }
			set{ Property.baoJiShangHaiMax = value; }
		}

		/// 对象种类
		public TargetTag targetTag { 
			get{ return 对象种类; } 
			set{ 对象种类 = value; } 
		}

		/// 是否死亡
		public bool isDeath { 
			get{ return 是否死亡 = (Hp <= 0) ? true : false; } 
			set{ 是否死亡 = value; } 
		}

		/// 是否移动
		public bool isMove { 
			get{ return 是否移动; } 
			set{ 是否移动 = value; } 
		}

		/// 播放动画
		public bool isPlay { 
			get{ return 播放动画; } 
			set{ 播放动画 = value; } 
		}

		/// 是否攻击
		public bool isAttack { 
			get{ return 是否攻击; } 
			set{ 是否攻击 = value; } 
		}

		/// 你的系列过小的话会被比你大一点的技能中断掉，比如R技能为4，可以中断前面3个技能
		[HideInInspector]
		public int skillUpIndex = 0;

		/// 是否暴击
		public bool isBaoJi { 
			get{ return 是否暴击; } 
			set{ 是否暴击 = value; } 
		}

		/// 是否死亡销毁物体
		public bool isDeathDestroy { 
			get{ return 死亡销毁; } 
			set{ 死亡销毁 = value; } 
		}

		[HideInInspector]
		public int baoJiIndex = 0;

		/// 攻击对象
		public PlayerSystem attackTarget { 
			get{ return 攻击对象; } 
			set{ 攻击对象 = value; } 
		}

		/// UI掉血属性
		public UITopShow uiTopShow { 
			get{ return 伤害显示; } 
			set{ 伤害显示 = value; } 
		}

		[HideInInspector]
		public List<UITopShow>  uiTopShows = new List<UITopShow>(1);

		public float Distance{
			get{
				if( attackTarget != null )
                    return ( transform.position - attackTarget.transform.position ).magnitude;
                return 1000;
			}
		}

		public override void OnDestroy() 
		{
            base.OnDestroy();
			for( int i = 0 ; i < uiTopShows.Count ; i++ )
			{
				if (uiTopShows[i] != null)
				{
					DestroyImmediate( uiTopShows[i].gameObject , true );
				}
			}
		}

		/// 被攻击属性计算方法(mainProperty:主攻方,attackType:攻击类型)
		public void Wound (PlayerSystem otherPlayer, AttackType attackType = AttackType.WuLi)
		{
			Wound(this,otherPlayer,attackType);
		}

		/// 被攻击属性计算方法(mainProperty:主攻方,mainProperty:主攻击方属性(当你不使用玩家系统属性可以更换别的自定义属性),attackType:攻击类型)
		public void Wound (PlayerSystem mainAttack , AttackProperty mainProperty , AttackType attackType = AttackType.WuLi)
		{
			Wound(this,mainAttack,mainProperty,attackType);
		}

		/// 被攻击属性计算方法(suffer:被攻击方,mainProperty:主攻方,attackType:攻击类型)
		static public void Wound(PlayerSystem suffer , PlayerSystem mainAttack , AttackType attackType = AttackType.WuLi )
		{
			Wound(suffer,mainAttack, mainAttack.Property, attackType);
		}

		/// 被攻击属性计算方法(suffer:被攻击方,mainProperty:主攻方,mainProperty:主攻击方属性(当你不使用玩家系统属性可以更换别的自定义属性),attackType:攻击类型)
		static public void Wound(PlayerSystem suffer , PlayerSystem mainAttack , PropertyData mainProperty , AttackType attackType = AttackType.WuLi )
		{
			float wHp = 0;
			float mHp = 0;
			float zHp = 0;
			float buff = 1;

            if (mainProperty.暴击开关)
            {
                buff = BaoJiShangHai(suffer, mainProperty, wHp);
            }

            if (attackType == AttackType.WuLi | attackType == AttackType.WuLiMoFa | attackType == AttackType.WuLiZhenShi | attackType == AttackType.WuLiMoFaZhenShi)
            {
                wHp = AttackCompute(suffer.wuLiFangYuLi, suffer.wuLiFangYuLiMax, mainProperty.wuLiGongJiLi * buff);
                SetUITopShow(suffer, wHp, Color.green, suffer.uiTopShow ? suffer.uiTopShow.m_WuLiGongJiImage : null);
                suffer.Hp -= wHp;
            }

            if (attackType == AttackType.MoFa | attackType == AttackType.WuLiMoFa | attackType == AttackType.MoFaZhenShi | attackType == AttackType.WuLiMoFaZhenShi)
            {
                mHp = AttackCompute(suffer.moFaFangYuLi, suffer.moFaFangYuLiMax, mainProperty.moFaGongJiLi * buff);
                SetUITopShow(suffer, mHp, Color.blue, suffer.uiTopShow ? suffer.uiTopShow.m_MoFaGongJiImage : null);
                suffer.Hp -= mHp;
            }

            if (attackType == AttackType.ZhenShi | attackType == AttackType.WuLiZhenShi | attackType == AttackType.MoFaZhenShi | attackType == AttackType.WuLiMoFaZhenShi)
            {
                zHp = mainProperty.zhenShiAttack;
                SetUITopShow(suffer, zHp, Color.white, suffer.uiTopShow ? suffer.uiTopShow.m_ZhenShiGongJiImage : null);
                suffer.Hp -= zHp;
            }

			if( suffer.isDeath ){
				if( suffer.isDeathDestroy ){
					Destroy ( suffer.gameObject , 5f );
				}
				GetEXP ( suffer , mainAttack );
			}
		}

		/// <summary>
		/// 伤害计算公式 - 百分比 ( 参数 aggres : 外来的攻击力(所受的攻击伤害) , 参数 defense : 当前防御力 , 参数 defenseMax : 防御上限 ) 
		/// 防御力只能抵挡外来的90%伤害(最高防御力) ! 超过受限在90%为最大值
		/// </summary>
		static float AttackCompute( float defense , float defenseMax , float aggres )
		{
			// 防御力只能抵挡外来的90%伤害(最高防御力)
			if (defense >= defenseMax * 0.9f) 
			{ 
				return aggres - (aggres * 0.9f);
			} 
			else // 如果防御力没有到达百分之90,按实际防御力来计算
			{
				return aggres - ((defense / defenseMax) * aggres);
			}
		}

		/// 计算暴击率方法 ( 给攻击方叠加暴击率对象 ) , 返回1则无暴击 , 返回2则有暴击
		public static float BaoJiShangHai(PlayerSystem suffer , PropertyData mainAttack , float attackHp )
		{
			mainAttack.baoJiLuValue += mainAttack.baoJiLu;
			if( mainAttack.baoJiLuValue >= 100 ){
				mainAttack.baoJiLuValue = mainAttack.baoJiLuValue - 100;
				SetUITopShow (suffer,attackHp * (2 + mainAttack.baoJiShangHai) , Color.red, suffer.uiTopShow ? suffer.uiTopShow.m_BaoJiImage : null );
				return 2 + mainAttack.baoJiShangHai;
			}
			return 1f;//返回1就是没有暴击伤害的((假设攻击力)100 * (伤害倍数)1 = 100)
		}

		/// UI头顶显示伤害方法
		public static void SetUITopShow(PlayerSystem suffer , float attackHP , Color textColor , Sprite hitImage )
		{
			if ( suffer.uiTopShow != null )
            {
				for( int i = 0 ; i < suffer.uiTopShows.Count ; i++ )
                {
					if (suffer.uiTopShows[i] == null){
						suffer.uiTopShows.RemoveAt(i);
						continue;
					}
					if( !suffer.uiTopShows[i].gameObject.activeSelf ){
						suffer.uiTopShows[i].gameObject.SetActive( true );
						suffer.uiTopShows[i].m_TargetPos = suffer.transform;
						suffer.uiTopShows[i].m_HitText.color = textColor;
						suffer.uiTopShows[i].m_HitText.text = "-"+attackHP.ToString ();
						if( hitImage != null ) suffer.uiTopShows[i].m_HitImage.sprite = hitImage;
						return;
					}
				}
				UITopShow uts = Instantiate ( suffer.uiTopShow.gameObject ).GetComponent<UITopShow>();
				uts.m_TargetPos = suffer.transform;
				uts.m_HitText.color = textColor;
				uts.m_HitText.text = "-"+attackHP.ToString ();
				if( hitImage != null ) uts.m_HitImage.sprite = hitImage;
				suffer.uiTopShows.Add( uts );
			}
		} 

		protected bool getExp = false;

        /// 获得经验值
        static void GetEXP(PlayerSystem suffer , PlayerSystem mainProperty )
		{
			if( !suffer.getExp ){
				mainProperty.exp += suffer.exp;
				if( mainProperty.exp >= mainProperty.expMax ){
					mainProperty.lv++;
					mainProperty.exp = 0;
					mainProperty.expMax += 250;
					if( mainProperty.lv > mainProperty.lvMax ){
						mainProperty.lv = mainProperty.lvMax;
					}else{
						SystemType.SetFieldValue ( mainProperty.Property , mainProperty.LvUpPropertys[ (int)mainProperty.lv ] , 
							new string[]{ "等级" , "最高等级" , "经验值" , "升级经验值" } , SetValueModel.Add
						);
					}
				}
				suffer.getExp = true;
			}
		}
	}
}
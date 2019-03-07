using UnityEngine;
using System.Collections;

/// <summary>
/// 属性值结构体
/// </summary>

[System.Serializable]
public class PropertyData
{
	public float 生命值 = 1000;
	public float 生命值上限 = 1000;
	public float 生命回复 = 0;
	public float 魔法值 = 1000;
	public float 魔法值上限 = 1000;
	public float 魔法回复 = 0;
	public float 等级 = 0;
	public float 最高等级 = 18;
	public float 经验值 = 0;
	public float 升级经验值 = 250;
	public float 物理攻击力 = 56;
	public float 物理防御力 = 30;
	public float 物理防御力上限 = 500;
	public float 物理穿透 = 0;
	public float 魔法攻击力 = 0;
	public float 魔法防御力 = 30;
	public float 魔法防御力上限 = 500;
	public float 魔法穿透 = 0;
	public float 真实伤害 = 0;
    public bool  暴击开关 = true;
    public float 暴击率 = 0;
	public float 暴击值 = 0;///暴击伤害200% (攻击力 * 200% = 暴击伤害)
	public float 暴击伤害 = 1;///暴击伤害上限250% 2.5f
	public float 暴击伤害上限 = 2.5f;
	public float 攻击速度 = 1f;
	public float 攻击速度上限 = 5f;
    public float 攻击速度率 = 0f;
	public float 移动速度 = 3f;///(最快移动速度)
	public float 移动速度上限 = 20f;
	public float 移动速度率 = 30f;
	public float 冷却缩减率 = 0;

	/// 设置全部变量值
	public PropertyData SetValue{
		set{
			生命值 = value.生命值;
			生命值上限 = value.生命值上限;
			生命回复 = value.生命回复;
			魔法值 = value.魔法值;
			魔法值上限 = value.魔法值上限;
			魔法回复 = value.魔法回复;
			等级 = value.等级;
			最高等级 = value.最高等级;
			经验值 = value.经验值;
			升级经验值 = value.升级经验值;
			物理攻击力 = value.物理攻击力;
			物理防御力 = value.物理防御力;
			物理防御力上限 = value.物理防御力上限;
			物理穿透 = value.物理穿透;
			魔法攻击力 = value.魔法攻击力;
			魔法防御力 = value.魔法防御力;
			魔法防御力上限 = value.魔法防御力上限;
			魔法穿透 = value.魔法穿透;
			真实伤害 = value.真实伤害;
			攻击速度 = value.攻击速度;
			攻击速度上限 = value.攻击速度上限;
			攻击速度率 = value.攻击速度率;
			移动速度 = value.移动速度;
			移动速度上限 = value.移动速度上限;
			移动速度率 = value.移动速度率;
			冷却缩减率 = value.冷却缩减率;
			暴击率 = value.暴击率;
			暴击值 = value.暴击值;
			暴击伤害 = value.暴击伤害;
			暴击伤害上限 = value.暴击伤害上限;
		}
	}

	///生命值
	public float hp {
		get { return 生命值; }
		set { 生命值 = value; }
	}

	///生命值上限
	public float hpMax { 
		get{ return 生命值上限; }
		set{ 生命值上限 = value; }
	}

	///生命回复
	public float hpAdd { 
		get{ return 生命回复; } 
		set{ 生命回复 = value; } 
	}

	///魔法值
	public float mp { 
		get{ return 魔法值; } 
		set{ 魔法值 = value; } 
	}

	///魔法值上限
	public float mpMax { 
		get{ return 魔法值上限; } 
		set{ 魔法值上限 = value; } 
	}

	///魔法回复
	public float mpAdd { 
		get{ return 魔法回复; } 
		set{ 魔法回复 = value; } 
	}

	///当前等级
	public float lv { 
		get{ return 等级; } 
		set{ 等级 = value; } 
	}

	///最高等级
	public float lvMax { 
		get{ return 最高等级; } 
		set{ 最高等级 = value; } 
	}

	///当前经验值
	public float exp { 
		get{ return 经验值; } 
		set{ 经验值 = value; } 
	}

	///升级需要的经验值
	public float expMax { 
		get{ return 升级经验值; } 
		set{ 升级经验值 = value; } 
	}

	///物理攻击力
	public float wuLiGongJiLi { 
		get{ return 物理攻击力; }
		set{ 物理攻击力 = value; } 
	}

	///物理防御力
	public float wuLiFangYuLi { 
		get{ return 物理防御力; } 
		set{ 物理防御力 = value; }
	}

	///物理防御力上限
	public float wuLiFangYuLiMax { 
		get{ return 物理防御力上限; }
		set{ 物理防御力上限 = value; }  
	}

	///物理穿透
	public float wuLiChuanTou { 
		get{ return 物理穿透; }
		set{ 物理穿透 = value; } 
	}

	///魔法攻击力
	public float moFaGongJiLi { 
		get{ return 魔法攻击力; }
		set{ 魔法攻击力 = value; } 
	}

	///魔法防御力 
	public float moFaFangYuLi { 
		get{ return 魔法防御力; }
		set{ 魔法防御力 = value; } 
	}

	///魔法防御力上限
	public float moFaFangYuLiMax { 
		get{ return 魔法防御力上限; }
		set{ 魔法防御力上限 = value; } 
	}

	///魔法穿透
	public float moFaChuanTou { 
		get{ return 魔法穿透; }
		set{ 魔法穿透 = value; } 
	}

	/// 真实伤害
	public float zhenShiAttack { 
		get{ return 真实伤害; } 
		set{ 真实伤害 = value; }
	}

	/// 攻击速度
	public float attackSpeed { 
		get{ return 攻击速度; } 
		set{ 攻击速度 = value; }
	}

	///攻击速度上限
	public float attackSpeedMax { 
		get{ return 攻击速度上限; } 
		set{ 攻击速度上限 = value; }
	}

	///攻击速度百分比
	public float attackSpeed100 { 
		get{ return 攻击速度率; } 
		set{ 攻击速度率 = value; }
	}

	///移动速度
	public float moveSpeed { 
		get{ return 移动速度; } 
		set{ 移动速度 = value; }
	}

	///移动速度上限(最快移动速度)
	public float moveSpeedMax { 
		get{ return 移动速度上限; } 
		set{ 移动速度上限 = value; }
	}

	///移动速度百分比a
	public float moveSpeed100 { 
		get{ return 移动速度率; } 
		set{ 移动速度率 = value; }
	}

	///冷却缩减百分比
	public float lengQueTime100 { 
		get{ return 冷却缩减率; } 
		set{ 冷却缩减率 = value; }
	}

	///暴击率
	public float baoJiLu { 
		get{ return 暴击率; } 
		set{ 暴击率 = value; }
	}

	///暴击值
	public float baoJiLuValue { 
		get{ return 暴击值; } 
		set{ 暴击值 = value; }
	}

	///暴击伤害200% (攻击力 * 200% = 暴击伤害)
	public float baoJiShangHai {
		get{ return 暴击伤害;} 
		set{ 暴击伤害 = value;}
	}

	///暴击伤害上限250% 2.5f
	public float baoJiShangHaiMax {
		get{ return 暴击伤害上限;} 
		set{ 暴击伤害上限 = value;} 
	}

	static public PropertyData operator +(PropertyData a,AttackProperty b)
	{
        PropertyData c = new PropertyData
        {
            生命值 = a.生命值 + b.生命值,
            生命回复 = a.生命回复 + b.生命回复,
            魔法值 = a.魔法值 + b.魔法值,
            魔法回复 = a.魔法回复 + b.魔法回复,
            物理攻击力 = a.物理攻击力 + b.物理攻击力,
            物理防御力 = a.物理防御力 + b.物理防御力,
            物理穿透 = a.物理穿透 + b.物理穿透,
            魔法攻击力 = a.魔法攻击力 + b.魔法攻击力,
            魔法防御力 = a.魔法防御力 + b.魔法防御力,
            魔法穿透 = a.魔法穿透 + b.魔法穿透,
            真实伤害 = a.真实伤害 + b.真实伤害,
            攻击速度率 = a.攻击速度率 + b.攻击速度率,
            移动速度率 = a.移动速度率 + b.移动速度率,
            冷却缩减率 = a.冷却缩减率 + b.冷却缩减率,
            暴击率 = a.暴击率 + b.暴击率,
            暴击伤害 = a.暴击伤害 + b.暴击伤害
        };
        return c;
	}

    static public PropertyData operator -(PropertyData a, AttackProperty b)
    {
        PropertyData c = new PropertyData
        {
            生命值 = a.生命值 - b.生命值,
            生命回复 = a.生命回复 - b.生命回复,
            魔法值 = a.魔法值 - b.魔法值,
            魔法回复 = a.魔法回复 - b.魔法回复,
            物理攻击力 = a.物理攻击力 - b.物理攻击力,
            物理防御力 = a.物理防御力 - b.物理防御力,
            物理穿透 = a.物理穿透 - b.物理穿透,
            魔法攻击力 = a.魔法攻击力 - b.魔法攻击力,
            魔法防御力 = a.魔法防御力 - b.魔法防御力,
            魔法穿透 = a.魔法穿透 - b.魔法穿透,
            真实伤害 = a.真实伤害 - b.真实伤害,
            攻击速度率 = a.攻击速度率 - b.攻击速度率,
            移动速度率 = a.移动速度率 - b.移动速度率,
            冷却缩减率 = a.冷却缩减率 - b.冷却缩减率,
            暴击率 = a.暴击率 - b.暴击率,
            暴击伤害 = a.暴击伤害 - b.暴击伤害
        };
        return c;
    }

    static public implicit operator AttackProperty(PropertyData b)
	{
        AttackProperty a = new AttackProperty
        {
            生命值 = b.生命值,
            生命回复 = b.生命回复,
            魔法值 = b.魔法值,
            魔法回复 = b.魔法回复,
            物理攻击力 = b.物理攻击力,
            物理防御力 = b.物理防御力,
            物理穿透 = b.物理穿透,
            魔法攻击力 = b.魔法攻击力,
            魔法防御力 = b.魔法防御力,
            魔法穿透 = b.魔法穿透,
            真实伤害 = b.真实伤害,
            攻击速度率 = b.攻击速度率,
            移动速度率 = b.移动速度率,
            冷却缩减率 = b.冷却缩减率,
            暴击率 = b.暴击率,
            暴击伤害 = b.暴击伤害,
        };
        return a;
	}

    static public PropertyData operator +(PropertyData a, PropertyData b)
    {
        PropertyData c = new PropertyData
        {
            生命值 = a.生命值 + b.生命值,
            生命值上限 = a.生命值上限 + b.生命值上限,
            生命回复 = a.生命回复 + b.生命回复,
            魔法值 = a.魔法值 + b.魔法值,
            魔法值上限 = a.魔法值上限 + b.魔法值上限,
            魔法回复 = a.魔法回复 + b.魔法回复,
            等级 = a.等级 + b.等级,
            最高等级 = a.最高等级 + b.最高等级,
            经验值 = a.经验值 + b.经验值,
            升级经验值 = a.升级经验值 + b.升级经验值,
            物理攻击力 = a.物理攻击力 + b.物理攻击力,
            物理防御力 = a.物理防御力 + b.物理防御力,
            物理防御力上限 = a.物理防御力上限 + b.物理防御力上限,
            物理穿透 = a.物理穿透 + b.物理穿透,
            魔法攻击力 = a.魔法攻击力 + b.魔法攻击力,
            魔法防御力 = a.魔法防御力 + b.魔法防御力,
            魔法防御力上限 = a.魔法防御力上限 + b.魔法防御力上限,
            魔法穿透 = a.魔法穿透 + b.魔法穿透,
            真实伤害 = a.真实伤害 + b.真实伤害,
            攻击速度 = a.攻击速度 + b.攻击速度,
            攻击速度上限 = a.攻击速度上限 + b.攻击速度上限,
            攻击速度率 = a.攻击速度率 + b.攻击速度率,
            移动速度 = a.移动速度 + b.移动速度,
            移动速度上限 = a.移动速度上限 + b.移动速度上限,
            移动速度率 = a.移动速度率 + b.移动速度率,
            冷却缩减率 = a.冷却缩减率 + b.冷却缩减率,
            暴击率 = a.暴击率 + b.暴击率,
            暴击值 = a.暴击值 + b.暴击值,
            暴击伤害 = a.暴击伤害 + b.暴击伤害,
            暴击伤害上限 = a.暴击伤害上限 + b.暴击伤害上限,
        };
		return c;
	}

    static public PropertyData operator -(PropertyData a, PropertyData b)
    {
        PropertyData c = new PropertyData
        {
            生命值 = a.生命值 - b.生命值,
            生命值上限 = a.生命值上限 - b.生命值上限,
            生命回复 = a.生命回复 - b.生命回复,
            魔法值 = a.魔法值 - b.魔法值,
            魔法值上限 = a.魔法值上限 - b.魔法值上限,
            魔法回复 = a.魔法回复 - b.魔法回复,
            等级 = a.等级 - b.等级,
            最高等级 = a.最高等级 - b.最高等级,
            经验值 = a.经验值 - b.经验值,
            升级经验值 = a.升级经验值 - b.升级经验值,
            物理攻击力 = a.物理攻击力 - b.物理攻击力,
            物理防御力 = a.物理防御力 - b.物理防御力,
            物理防御力上限 = a.物理防御力上限 - b.物理防御力上限,
            物理穿透 = a.物理穿透 - b.物理穿透,
            魔法攻击力 = a.魔法攻击力 - b.魔法攻击力,
            魔法防御力 = a.魔法防御力 - b.魔法防御力,
            魔法防御力上限 = a.魔法防御力上限 - b.魔法防御力上限,
            魔法穿透 = a.魔法穿透 - b.魔法穿透,
            真实伤害 = a.真实伤害 - b.真实伤害,
            攻击速度 = a.攻击速度 - b.攻击速度,
            攻击速度上限 = a.攻击速度上限 - b.攻击速度上限,
            攻击速度率 = a.攻击速度率 - b.攻击速度率,
            移动速度 = a.移动速度 - b.移动速度,
            移动速度上限 = a.移动速度上限 - b.移动速度上限,
            移动速度率 = a.移动速度率 - b.移动速度率,
            冷却缩减率 = a.冷却缩减率 - b.冷却缩减率,
            暴击率 = a.暴击率 - b.暴击率,
            暴击值 = a.暴击值 - b.暴击值,
            暴击伤害 = a.暴击伤害 - b.暴击伤害,
            暴击伤害上限 = a.暴击伤害上限 - b.暴击伤害上限,
        };
        return c;
    }
}
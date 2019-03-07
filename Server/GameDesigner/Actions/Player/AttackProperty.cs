[System.Serializable]
public class AttackProperty
{
    public float 生命值 = 0;
    public float 生命回复 = 0;
    public float 魔法值 = 1000;
    public float 魔法回复 = 0;
    public float 物理攻击力 = 56;
    public float 物理防御力 = 30;
    public float 物理穿透 = 0;
    public float 魔法攻击力 = 0;
    public float 魔法防御力 = 30;
    public float 魔法穿透 = 0;
    public float 真实伤害 = 0;
    public bool  暴击开关 = true;
    public float 暴击率 = 0;
	public float 暴击伤害 = 1;
    public float 攻击速度率 = 0f;
    public float 移动速度率 = 0f;
    public float 冷却缩减率 = 0;

    ///生命值
    public float hp {
		get { return 生命值; }
		set { 生命值 = value; }
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

	///魔法回复
	public float mpAdd { 
		get{ return 魔法回复; } 
		set{ 魔法回复 = value; } 
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
		get{ return 攻击速度率; } 
		set{ 攻击速度率 = value; }
	}

	///暴击率
	public float baoJiLu { 
		get{ return 暴击率; } 
		set{ 暴击率 = value; }
	}

	///暴击伤害200% (攻击力 * 200% = 暴击伤害)
	public float baoJiShangHai {
		get{ return 暴击伤害;} 
		set{ 暴击伤害 = value;}
	}

	static public implicit operator PropertyData(AttackProperty b)
	{
        PropertyData a = new PropertyData
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
            暴击开关 = b.暴击开关,
            暴击率 = b.暴击率,
            暴击伤害 = b.暴击伤害,
            攻击速度率 = b.攻击速度率,
            移动速度率 = b.移动速度率,
            冷却缩减率 = b.冷却缩减率,
        };
        return a;
	}
}
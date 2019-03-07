using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LimbsMode
{
	/// 头部
	Head , 
	/// 脊柱-上身
	Spine ,
    /// 左右上臂
    LR_UpperArm,
    /// 左右下臂
    LR_Forearm, 
	/// 骨盆-下身
	Pelvis ,
    /// 左右大腿
    LR_Thigh,
    /// 左右小腿
    LR_Calf, 
}

/// <summary>
/// 肢体碰撞管理器--用了接受各种武器碰撞
/// </summary>

public class LimbCollisionManager : MonoBehaviour {
	public LimbsMode limbsMode = LimbsMode.Spine;
	public GameDesigner.PlayerSystem player = null;//敌人或玩家系统
}

/// <summary>
/// 武器攻击肢体
/// </summary>
[System.Serializable]
public class WeaponAttackLocation
{
	public LimbsMode limbsMode = LimbsMode.Spine;//肢体模式
	public float outHP = 70;//各种部位所打出的生命值
}
using UnityEngine;
using System.Collections.Generic;
using GameDesigner;

public class ColliderProperty : MonoBehaviour
{
	public AttackType 伤害类型 = AttackType.WuLi;

	public float 技能碰撞大小 = 2f;

	public bool 击飞敌人 = false;
	public Vector3 击飞方向 = Vector3.up; 
	public float 击飞速度 = 1f;
	public GameObject 击飞特效 = null;
	public Vector3 击飞特效位置 = Vector3.up;
	public Timer 击飞时间 = new Timer( 0 , 0.5f );

	public bool 减速敌人 = false;
	public float 减速百分比 = 30f; 
	public GameObject 减速特效 = null;
	public Vector3 减速特效位置 = Vector3.up;
	public Timer 减速时间 = new Timer( 0 , 1f ); 

	public bool 飞行技能 = false;
	public Vector3 飞行方向 = Vector3.forward;
	public float 飞行速度 = 5f;
	public GameObject 飞行特效 = null;
	public Vector3 飞行特效位置 = Vector3.up;
	public Timer 飞行时间 = new Timer( 0 , 1f );

	public bool 击中销毁特效 = false;

	public ColliderProperty setData{
		set{
			伤害类型 = value.伤害类型;
			技能碰撞大小 = value.技能碰撞大小;

			击飞敌人 = value.击飞敌人;
			击飞方向 = value.击飞方向; 
			击飞速度 = value.击飞速度;
			击飞特效 = value.击飞特效;
			击飞特效位置 = value.击飞特效位置;
			击飞时间 = value.击飞时间;

			减速敌人 = value.减速敌人;
			减速百分比 = value.减速百分比; 
			减速特效 = value.减速特效;
			减速特效位置 = value.减速特效位置;
			减速时间 = value.减速时间; 

			飞行技能 = value.飞行技能;
			飞行方向 = value.飞行方向;
			飞行速度 = value.飞行速度;
			飞行特效 = value.飞行特效;
			飞行特效位置 = value.飞行特效位置;
			飞行时间 = value.飞行时间;

			击中销毁特效 = value.击中销毁特效;
		}
	}

	public List<ColliderBehaviour> behaviours = new List<ColliderBehaviour> (0);
	public bool findBehaviours = false;
	public string CreateScriptName = "newAttackBehaviour";

	/// 对单位攻击造成的伤害类型
	public AttackType attackType {
		get{ return 伤害类型; }
		set{ 伤害类型 = value; }
	}

	/// 技能碰撞大小
	public float colliderRadius {
		get{ return 技能碰撞大小; }
		set{ 技能碰撞大小 = value; }
	}

	/// 击飞对方单位(无法移动和选择单位)
	public bool isFlyUp {
		get{ return 击飞敌人; }
		set{ 击飞敌人 = value; }
	}

	/// 击飞对方时间(无法移动和选择单位)
	public Timer flyUpTime {
		get{ return 击飞时间; }
		set{ 击飞时间 = value; }
	}

	/// 击飞对方方向(无法移动和选择单位)
	public Vector3 flyUpDirection {
		get{ return 击飞方向; }
		set{ 击飞方向 = value; }
	}

	/// 击飞对方速度(无法移动和选择单位)
	public float flyUpSpeed {
		get{ return 击飞速度; }
		set{ 击飞速度 = value; }
	}

	/// 控制对方单位移动(无法移动或移动减慢百分比)
	public bool cannotMove {
		get{ return 减速敌人; }
		set{ 减速敌人 = value; }
	}

	/// 被控制后是否可以移动的百分比,默认为不能移动
	public float cannotMove100 {
		get{ return 减速百分比; }
		set{ 减速百分比 = value; }
	}
		
	/// 控制对方单位移动粒子特效
	public GameObject cannotMoveEffect {
		get{ return 减速特效; }
		set{ 减速特效 = value; }
	}

	/// 控制对方单位移动粒子特效存活时间
	public Timer cannotMoveTime {
		get{ return 减速时间; }
		set{ 减速时间 = value; }
	}

	/// 这个是飞行技能吗
	public bool isMove {
		get{ return 飞行技能; }
		set{ 飞行技能 = value; }
	}

	/// 飞行方向
	public Vector3 moveDirection {
		get{ return 飞行方向; }
		set{ 飞行方向 = value; }
	}

	///飞行时间
	public Timer moveTime {
		get{ return 飞行时间; }
		set{ 飞行时间 = value; }
	}

	/// 飞行速度
	public float moveSpeed {
		get{ return 飞行速度; }
		set{ 飞行速度 = value; }
	}

	/// 当击中单位销毁技能粒子物体
	public bool hitDestroy {
		get{ return 击中销毁特效; }
		set{ 击中销毁特效 = value; }
	}
}
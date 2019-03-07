using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// 菜单项
/// </summary>

public class MenuItems : MonoBehaviour 
{
	[MenuItem ("XGAME/敌人与玩家/Player(玩家控制类)")]
	static void PS2()
	{
		Selection.activeGameObject.AddComponent<GameDesigner.Player>();
	}

	[MenuItem ("XGAME/敌人与玩家/Enemy(怪物控制类)")]
	static void PS3()
	{
		Selection.activeGameObject.AddComponent<GameDesigner.Enemy>();
	}
	
	[MenuItem ("XGAME/技能碰撞/SkillCollider(技能碰撞类)")]
	static void PS6()
	{
		Selection.activeGameObject.AddComponent<SkillCollider>();
	}

	[MenuItem ("XGAME/UI/粒子自适应/UISetEffectSize(UI动态自适应类)")]
	static void PS8()
	{
		Selection.activeGameObject.AddComponent<UISetEffectSize>();
	}
	
	[MenuItem ("XGAME/UI/主UI类/MainUI(主UI关键类)")]
	static void PS9()
	{
		Selection.activeGameObject.AddComponent<MainUI>();
	}
	
	[MenuItem ("XGAME/UI/存储/SaveLoading(读取存档类)")]
	static void PS10()
	{
		Selection.activeGameObject.AddComponent<SaveLoading>();
	}
	
	[MenuItem ("XGAME/UI/UI血条/UISliderHPMP(读取玩家当前（血值）（魔值）条类)")]
	static void PS11()
	{
		Selection.activeGameObject.AddComponent<UISliderHPMP>();
	}
	
	[MenuItem ("XGAME/UI/UI等级/LvLayers(读取玩家等级类)")]
	static void PS12()
	{
		Selection.activeGameObject.AddComponent<LvLayers>();
	}
	
	[MenuItem ("XGAME/UI/UI界面/UIDepth(界面遮罩类)")]
	static void PS14()
	{
		Selection.activeGameObject.AddComponent<UIDepth>();
	}

	[MenuItem ("XGAME/UI/物品栏/UISetBoxPosition(快速设置物品格子位置类)")]
	static void PS15()
	{
		Selection.activeGameObject.AddComponent<UISetBoxPosition>();
	}
	
	[MenuItem ("XGAME/UI/物品栏/WuPinBoxList(主物品格子集合索引类)")]
	static void PS16()
	{
		Selection.activeGameObject.AddComponent<WuPinBoxList>();
	}
	
	[MenuItem ("XGAME/UI/物品栏/WuPinBoxData(物品栏格子数据类)")]
	static void PS17()
	{
		Selection.activeGameObject.AddComponent<WuPinBoxData>();
	}
	
	[MenuItem ("XGAME/UI/属性栏/ShuXingBoxList(主属性格子集合索引类)")]
	static void PS18()
	{
		Selection.activeGameObject.AddComponent<ShuXingBoxList>();
	}
	
	[MenuItem ("XGAME/UI/属性栏/ShuXingBoxData(属性栏格子数据类)")]
	static void PS19()
	{
		Selection.activeGameObject.AddComponent<ShuXingBoxData>();
	}
	
	[MenuItem ("XGAME/UI/属性栏/ShuXingLogData(属性栏详细数据类)")]
	static void PS20()
	{
		Selection.activeGameObject.AddComponent<ShuXingLogData>();
	}

	[MenuItem ("XGAME/UI/商店栏/ShangDianBoxData(商店栏格子数据类)")]
	static void PS22()
	{
		Selection.activeGameObject.AddComponent<GameDesigner.Enemy>();
	}

	[MenuItem ("XGAME/UI/公告栏/UIEventManager(<UI系统类>主关键分布UI线路类)")]
	static void PS23()
	{
		Selection.activeGameObject.AddComponent<UIEventManager>();
	}

	[MenuItem ("XGAME/UI/属性显示/GoodsInfoMenu(物品属性详细显示类)")]
	static void PS24()
	{
		Selection.activeGameObject.AddComponent<GoodsInfoMenu>();
	}

	[MenuItem ("XGAME/UI/加载场景/UILoadScene(胜利失败加载目标场景类)")]
	static void PS25()
	{
		Selection.activeGameObject.AddComponent<UILoadScene>();
	}
	
	[MenuItem ("XGAME/UI/登陆游戏/UILoging(主登陆游戏类)")]
	static void PS26()
	{
		Selection.activeGameObject.AddComponent<UILoging>();
	}

	[MenuItem ("XGAME/UI/敌人管理/EnemyGuanLi(敌人管理类)")]
	static void PS27()
	{
		Selection.activeGameObject.AddComponent<EnemyGuanLi>();
	}

	[MenuItem ("XGAME/相机视角/Moba_Camera(游戏相机类)")]
	static void PS28()
	{
		Selection.activeGameObject.AddComponent<MobaCamera>();
	}
}

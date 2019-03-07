using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour 
{
	public GoodsData 	物品数据 = new GoodsData();
	public UIEventManager 	物品操作信息 = null;

	/// 物品数据
	public GoodsData goodsData
	{
		get{ return 物品数据; }
		set{ 物品数据 = value; }
	}

	/// 游戏数据系统
	public UIEventManager m_XGameSystem 
	{
		get 
		{ 
			if (物品操作信息 == null) 
			{
				物品操作信息 = UIEventManager.instantiate;
			}
			return 物品操作信息;
		}
		set{ 物品操作信息 = value; }
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate() {
		
	}
}

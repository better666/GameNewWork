using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShangDianLoadObject : MonoBehaviour 
{
	public Object[] SwordObject;
	public ShangDianBoxData[] sdbds;
	public GoodsData xgamedata;
	public GoodsData xgamedatarun;
	public string path = "Sword";
	public bool find = false;
	
	public void StartEditor () 
	{
		if ( find == false ) return; find = false;
		SwordObject = Resources.LoadAll ( path ) as Object [ ];
		sdbds = transform.GetComponentsInChildren<ShangDianBoxData>();
		xgamedatarun.SetXGameData = xgamedata;//初始化xgamedatarun的值
		xgamedatarun.m_GouMaiJiaGeInt = xgamedata.m_GouMaiJiaGeInt;
		xgamedatarun.m_ChuShouJiaGeInt = xgamedata.m_ChuShouJiaGeInt;
	}

	public void set_GouMaiChuShou( int i )
	{
		xgamedatarun.m_GouMaiJiaGeInt += xgamedata.m_GouMaiJiaGeInt;
		xgamedatarun.m_ChuShouJiaGeInt += xgamedata.m_ChuShouJiaGeInt;
		sdbds[i].goodsData.m_GouMaiJiaGeInt = xgamedatarun.m_GouMaiJiaGeInt;
		sdbds[i].goodsData.m_ChuShouJiaGeInt = xgamedatarun.m_ChuShouJiaGeInt;
	}
}

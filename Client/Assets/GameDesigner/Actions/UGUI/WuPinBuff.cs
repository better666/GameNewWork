using UnityEngine;
using System.Collections;

/// <summary>
/// 物品祝福药水
/// </summary>
public class WuPinBuff : MonoBehaviour 
{
	public GoodsData goodsData;
	public GameDesigner.Player m_Player = null;
	public float m_OffBuff = 10.0F;

	float time = 0.0F;

	void Start ()
	{
		//?m_Player.goodsData.AddXGameDatas = goodsData;
		//?m_Player.SetPlayerData();
	}

	void LateUpdate() 
	{
		time += Time.deltaTime;

		if( time >= m_OffBuff )
		{
			//?m_Player.goodsData.CutXGameDatas = goodsData;
			//?m_Player.SetPlayerData();
			Destroy(this);
		}
	}
}

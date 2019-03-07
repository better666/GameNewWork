using UnityEngine;
using System.Collections;

/// <summary>
/// 物品集合，此类负责当商店栏里购买的物品或属性栏卸下装备的物品索引进入的物品栏格子
/// </summary>

[ExecuteInEditMode]
public class WuPinBoxList : MonoBehaviour 
{
	/// <summary> 物品格子索引 </summary>
	public WuPinBoxData[] m_WuPinList = new WuPinBoxData[44];

	#if UNITY_EDITOR
	public bool init = false;
	void LateUpdate()
	{
		if( init )
		{
			init = false;

			m_WuPinList = transform.GetComponentsInChildren<WuPinBoxData> (true);

		}
	}
	#endif
}

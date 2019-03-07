using UnityEngine;
using System.Collections;

/// <summary>
/// 属性栏集合，也是属性栏索引，当这个装备是武器则索引进入武器格子位置，当这个装备是鞋子则索引进入鞋子格子位置
/// </summary>
public class ShuXingBoxList : MonoBehaviour 
{
	public ShuXingBoxData[] m_ShuXingBoxData = new ShuXingBoxData[5];
}

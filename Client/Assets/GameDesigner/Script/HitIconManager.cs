using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class HitIconManager : MonoBehaviour
{
	public Sprite 刀杀记录图标 = null;
	public Sprite 杀敌记录图标 = null;
	public Sprite 爆头记录图标 = null;
	public Sprite 累计杀敌10人记录图标 = null;

	public Image 击杀图像组件 = null;

	public int 击杀计数 = 0;
	public float 图标间距 = 25f;
	public float 图标初始化位置 = 0;
	public float 图标当前位置 = 0;

	public List<GameObject> 图标管理集合 = new List<GameObject>();
}
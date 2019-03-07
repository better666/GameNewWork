using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GouMaiMenu : MonoBehaviour 
{
	//
	// 字段
	//

	public InputField 		购买数量组建			= null;
	public Text 			购买价格文本组建		= null;
	public Text 			购买物品名称文本组建	= null;

	//
	// 属性
	//

	/// <summary> 购买数量组建 </summary>
	public InputField gouMaiShuLiang
	{ 
		get{ return 购买数量组建; } 
		set{ 购买数量组建 = value; } 
	}

	/// <summary> 购买价格文本组建 </summary>
	public Text gouMaiJiaGeText
	{ 
		get{ return 购买价格文本组建; } 
		set{ 购买价格文本组建 = value; } 
	}

	/// <summary> 购买物品名称文本组建 </summary>
	public Text gouMaiNameText
	{ 
		get{ return 购买物品名称文本组建; } 
		set{ 购买物品名称文本组建 = value; } 
	}

	//
	// 方法
	//

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate() {
		
	}
}

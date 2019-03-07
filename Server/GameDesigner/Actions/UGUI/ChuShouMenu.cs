using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChuShouMenu : MonoBehaviour 
{
	//
	//	字段
	//

	public Text				出售物品名称			= null;
	public InputField 		输入出售数量			= null;
	public Text 			显示出售价格			= null;
	public WuPinBoxData 	出售物品索引			= null; 

	//
	//	属性
	//

	/// 出售物品名称
	public Text chushouName
	{
		get{ return 出售物品名称; }
		set{ 出售物品名称 = value; }
	}

	/// 输入出售数量
	public InputField chushouInput
	{
		get{ return 输入出售数量; }
		set{ 输入出售数量 = value; }
	}

	/// 显示出售价格
	public Text chuShoujiageText
	{
		get{ return 显示出售价格; }
		set{ 显示出售价格 = value; }
	}

	/// 出售物品索引
	public WuPinBoxData chuShouwupinIndex
	{
		get{ return 出售物品索引; }
		set{ 出售物品索引 = value; }
	}

	//
	//  静态属性
	//

	static private ChuShouMenu _instantiate = null;
	static public ChuShouMenu instantiate
	{
		get
		{
			if( _instantiate == null )
			{
				_instantiate = GameObject.FindObjectOfType<ChuShouMenu> ();
				if ( _instantiate == null ) 
				{
					Debug.Log ( "场景中没有XGAMESYSTEM组件或组件处于关闭状态,系统自动创建一个临时类代替!" );
				}
			}
			return _instantiate;
		}
	}

	//
	// 虚方法
	//

	/// 确定出售物品
	virtual public void Yes ( )
	{
		Yes ( this );
	}

	//
	//  静态方法
	//

	/// 确定出售物品
	static public void Yes ( ChuShouMenu chushou )
	{
		if (chushou.chuShouwupinIndex.goodsData.m_WuPinInt >= int.Parse(chushou.chushouInput.text))//判断物品数量是否小于或等于输入数量，如果小于则执行，否则退出
		{
			chushou.chuShouwupinIndex.goodsData.m_WuPinInt -= int.Parse(chushou.chushouInput.text);//正解出售物品数量
			UIEventManager.instantiate.m_MyJinBi += chushou.chuShouwupinIndex.goodsData.m_ChuShouJiaGeInt * int.Parse(chushou.chushouInput.text);
			chushou.chuShouwupinIndex.UpdateWuPinData();//更新物品数量
			UIEventManager.instantiate.LOGJinBi();//更新金币数量
		}
		else 
		{
			//错误出售物品数量
		}
	}

	/// 放弃出售物品
	static public void No ()
	{

	}
}

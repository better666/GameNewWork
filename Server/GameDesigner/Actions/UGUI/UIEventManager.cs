using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary> 游戏数据系统 继承游戏数据 </summary>

public class UIEventManager : MonoBehaviour
{
	//
	//  字段
	//

	public GoodsData 物品数据 = new GoodsData ();
	public GouMaiMenu 购买物品组件 = null;
	public GoodsInfoMenu 物品信息面板 = null;
	public GameObject 强化武器面板 = null;
	public ChuShouMenu 出售物品组件 = null;
	public GameObject 调式武器品级面板 = null;
	public GameObject 合成武器面板 = null;
	public GameObject 分解物品面板 = null;
	public ShuXingLogData 属性值显示组件 = null;
	public ShangDianBoxData 商店操作索引 = null;
	public WuPinBoxData 物品操作索引 = null;
	public ShuXingBoxData 属性操作索引 = null;
	public WuPinBoxList 物品栏管理组件 = null;
	public ShuXingBoxList 属性栏管理组件 = null;
	public int 拥有金币 = 100;
	public Text 物品金币显示组件 = null;
	public Text 商店金币显示组建 = null;
	public GameDesigner.Player 玩家组件 = null;
	public Camera 游戏相机 = null;
	public Canvas 帆布组件 = null;
	public bool 商店物品被点击 = false;
	//BUG

	//
	//  属性
	//

	public GoodsData 	goodsData{ get { return 物品数据; } set { 物品数据 = value; } }

	/// <summary> 是否--购买物品,卸下物品,戴上物品--的公告栏 </summary>
	public GouMaiMenu	goumaiMenu{ get { return 购买物品组件; } set { 购买物品组件 = value; } }

	/// <summary> 属性显示面板 </summary>
	public GoodsInfoMenu goodsInfo{ get { return 物品信息面板; } set { 物品信息面板 = value; } }

	/// <summary> 是否--强化武器--的公告栏 </summary>
	public GameObject	m_IsQiangHuaSwordObject{ get { return 强化武器面板; } set { 强化武器面板 = value; } }

	/// <summary> 是否--出售物品组件--的公告栏 </summary>
	public ChuShouMenu	chushouMenu{ get { return 出售物品组件; } set { 出售物品组件 = value; } }

	/// <summary> 是否--调式武器品级--的公告栏 </summary>
	public GameObject	m_IsDiaoShiSwordObject{ get { return 调式武器品级面板; } set { 调式武器品级面板 = value; } }

	/// <summary> 是否--合成武器--的公告栏 </summary>
	public GameObject	m_IsHeChengSwordObject{ get { return 合成武器面板; } set { 合成武器面板 = value; } }

	/// <summary> 是否--分解物品--的公告栏 </summary>
	public GameObject	m_IsFenJieWuPinObject{ get { return 分解物品面板; } set { 分解物品面板 = value; } }

	/// <summary> 属性显示的可视化数据类 </summary>
	public ShuXingLogData 	m_ShuXingLogData{ get { return 属性值显示组件; } set { 属性值显示组件 = value; } }

	/// <summary> 要购买商店中哪个物品索引 </summary>
	public ShangDianBoxData m_ShangDianBoxData{ get { return 商店操作索引; } set { 商店操作索引 = value; } }

	/// <summary> 要戴上物品栏中哪一个装备索引（被点击的物品索引） </summary>
	public WuPinBoxData m_WuPinBoxData{ get { return 物品操作索引; } set { 物品操作索引 = value; } }

	/// <summary> 要卸下属性栏中的哪一个装备索引（被点击属性栏正在戴上的装备物品索引到此处） </summary>
	public ShuXingBoxData m_ShuXingBoxData{ get { return 属性操作索引; } set { 属性操作索引 = value; } }

	/// <summary> 购买物品后进入物品栏中的哪一个格子的集合类索引 </summary>
	public WuPinBoxList m_WuPinBoxList{ get { return 物品栏管理组件; } set { 物品栏管理组件 = value; } }

	/// <summary> 戴上装备后进入属性栏中的哪一个格子的集合索引 </summary>
	public ShuXingBoxList m_ShuXingBoxList{ get { return 属性栏管理组件; } set { 属性栏管理组件 = value; } }

	/// <summary> 我的当前金币 </summary>
	public int m_MyJinBi{ get { return 拥有金币; } set { 拥有金币 = value; } }

	/// <summary> 物品的金币显示文本组建 </summary>
	public Text m_WuPinJinBiText{ get { return 物品金币显示组件; } set { 物品金币显示组件 = value; } }

	/// <summary> 商店的金币显示文本组建 </summary>
	public Text m_ShangDianJinBiText{ get { return 商店金币显示组建; } set { 商店金币显示组建 = value; } }

	public GameDesigner.Player m_Player{ get { return 玩家组件; } set { 玩家组件 = value; } }

	/// <summary> 商店物品被点击==>BUG </summary>
	public bool m_IsDownButtion{ get { return 商店物品被点击; } set { 商店物品被点击 = value; } }
	//BUG
	/// 游戏相机
	public new Camera camera {
		get {
			if (游戏相机 == null)
				游戏相机 = Camera.main;
			return 游戏相机;
		}
		set{ 游戏相机 = value; }
	}

	/// 帆布组件
	public Canvas canvas {
		get { 
			if (帆布组件 == null) {
				帆布组件 = GameObject.FindObjectOfType<Canvas> ();
				if (帆布组件 == null) {
					帆布组件 = gameObject.AddComponent<Canvas> ();
				}
			}
			return 帆布组件;
		}
		set{ 帆布组件 = value; }
	}

	//
	//  静态属性
	//

	static private UIEventManager _instantiate = null;
	static public UIEventManager instantiate {
		get {
			if (_instantiate == null) {
				_instantiate = GameObject.FindObjectOfType<UIEventManager> ();
				if (_instantiate == null) {
					Debug.Log ("场景中没有XGAMESYSTEM组件或组件处于关闭状态,系统自动创建一个临时类代替!");
					return _instantiate = new UIEventManager ();
				}
			}
			return _instantiate;
		}
	}

	//
	//  方法
	//

	void Start ()
	{
		_instantiate = this;
		canvas = GetComponent<Canvas> ();
		camera = Camera.main;
		m_Player = GameObject.FindObjectOfType<GameDesigner.Player> ();
	}

	void OnEnable ()
	{
		_instantiate = this;
	}

	/// <summary>
	/// 确定--购买、卸下，戴上，出售--的物品
	/// </summary>
	public virtual void Yes ()
	{
		switch (goodsData.m_WuPinState) {
		case ZhuangTaiUI.Null:
			
			break;
		case ZhuangTaiUI.goumai://购买
			GouMai ();
			break;
		case ZhuangTaiUI.zhuangbei://戴上
			ZhuangBei ();
			break;
		case ZhuangTaiUI.xiexia_zhuangbei://卸下
			XieXia ();
			break;
		case ZhuangTaiUI.chushou://出售
			ChuShou ();
			break;
		case ZhuangTaiUI.diaoluo_zhuangbei://掉落装备（物品）
			DiaoLuoZhuangBei ();
			break;
		}
	}

	public virtual void No ()//放弃购买物品、卸下，戴上，出售
	{
		m_IsDownButtion = false; 
	}

	void GouMai ()//购买
	{
		int jb = int.Parse (goumaiMenu.gouMaiShuLiang.text) * goodsData.m_GouMaiJiaGeInt;

		if (m_MyJinBi < jb) {
			Debug.Log ("金币不足！无法购买！");
			return;
		}

		for (int i = 0; i < m_WuPinBoxList.m_WuPinList.Length; i++) {//判断所有物品格子
			WuPinTag wpt = m_WuPinBoxList.m_WuPinList [i].goodsData.m_WuPinTag;
			WuPinBoxData wpbd = m_WuPinBoxList.m_WuPinList [i];

			//判断物品是否存在并且不是武器，上衣，下装，腰带，鞋子类型的物品则放入该物品格子
			if (wpbd.goodsData.m_IsHasWuPin & wpt != WuPinTag.WuQi & wpt != WuPinTag.ShangYi & wpt != WuPinTag.XiaZhuang & wpt != WuPinTag.XieZi & wpt != WuPinTag.TouJian) {
				OutWuPin (wpbd, jb, goumaiMenu.gouMaiShuLiang.text, goodsData);
				break;//跳出，否则物品栏爆满
			} else if (!wpbd.goodsData.m_IsHasWuPin) {//判断物品是否存在，如果不存在则放入该物品，否则提示物品栏物品已满
				OutWuPin (wpbd, jb, goumaiMenu.gouMaiShuLiang.text, goodsData);
				break;//跳出，否则物品栏爆满
			} else {
				//系统提示，物品已满
			}
		}
		
	}

	/// <summary>
	/// 进入物品栏 ( "wpbd"物品格子索引 , "jb"购买后付出的金币 , "gmsl"购买数量 )
	/// </summary>

	void OutWuPin (WuPinBoxData wpbd, int jb, string gmsl, GoodsData xgd)
	{
		wpbd.goodsData.m_IsHasWuPin = true;//放置物品后转换为物品存在
		wpbd.goodsData.SetWuPinGameDatas = xgd;
		wpbd.goodsData.m_WuPinInt += int.Parse (gmsl);//放置物品数量
		wpbd.goodsData.m_WuPinImage.sprite = xgd.m_WuPinSprite;
		wpbd.goodsData.m_WuPinText.text = wpbd.goodsData.m_WuPinInt.ToString ();
		m_MyJinBi -= jb;//购买物品所出的金币
		m_WuPinJinBiText.text = m_MyJinBi.ToString ();
		m_ShangDianJinBiText.text = m_MyJinBi.ToString ();
	}

	void DiaoLuoZhuangBei ()//掉落装备
	{
		for (int i = 0; i < m_WuPinBoxList.m_WuPinList.Length; i++) {//判断所有物品格子
			WuPinTag wpt = m_WuPinBoxList.m_WuPinList [i].goodsData.m_WuPinTag;
			WuPinBoxData wpbd = m_WuPinBoxList.m_WuPinList [i];
			
			//判断物品是否存在并且不是武器，上衣，下装，腰带，鞋子类型的物品则放入该物品格子
			if (wpbd.goodsData.m_IsHasWuPin & wpt != WuPinTag.WuQi & wpt != WuPinTag.ShangYi & wpt != WuPinTag.XiaZhuang & wpt != WuPinTag.XieZi & wpt != WuPinTag.TouJian & wpbd.goodsData.m_WuPinName == goodsData.m_WuPinName) {
				OutWuPin (wpbd, 0, "1", goodsData);
				break;//跳出，否则物品栏爆满
			} else if (!wpbd.goodsData.m_IsHasWuPin) {//判断物品是否存在，如果不存在则放入该物品，否则提示物品栏物品已满
				OutWuPin (wpbd, 0, "1", goodsData);
				break;//跳出，否则物品栏爆满
			} else {
				//系统提示，物品已满
			}
		}
	}

	void ZhuangBei ()//戴上
	{
		ShuXingList (goodsData.m_WuPinTag);
		HPMPBuffList (goodsData.m_WuPinTag);
	}

	void XieXia ()//卸下
	{
		//卸下物品
		for (int b = 0; b < m_WuPinBoxList.m_WuPinList.Length; b++) {//判断所有物品格子
			WuPinBoxData wpbd = m_WuPinBoxList.m_WuPinList [b];
			
			if (wpbd.goodsData.m_IsHasWuPin == false) {//判断物品是否存在或者物品名称与现在购买的物品名称相同，相同则放入该物品格子
				wpbd.goodsData.SetWuPinGameDatas = m_ShuXingBoxData.goodsData;//卸下装备(将物品的属性值一同放入物品格子里)
				OutWuPin (wpbd, 0, "1", m_ShuXingBoxData.goodsData);
				m_ShuXingBoxData.goodsData.m_WuPinImage.sprite = m_ShuXingBoxData.goodsData.m_WuPinLocalSprite;
				m_ShuXingBoxData.goodsData.m_IsHasWuPin = false;
				m_ShuXingLogData.CutShuXingLogGameDatas (m_ShuXingBoxData.goodsData);//减少属性值
				//?m_ShuXingLogData.GetShuXingLogGameDatas( m_Player.goodsData );
				m_ShuXingLogData.LOGShuXingDatas ();
				//?m_Player.SetPlayerData ();
				UIEventManager.instantiate.m_Player.Property.SetValue = m_ShuXingLogData.goodsData.data;
				break;//跳出，否则物品栏爆满
			}
		}
	}

	/// <summary>
	/// 属性集合，（ 装备类型,比如武器，鞋子 ）
	/// </summary>

	void ShuXingList (WuPinTag wptag)
	{
		for (int i = 0; i < m_ShuXingBoxList.m_ShuXingBoxData.Length; i++) {
			ShuXingBoxData sxbd = m_ShuXingBoxList.m_ShuXingBoxData [i];
			
			if (sxbd.goodsData.m_WuPinTag == wptag & sxbd.goodsData.m_IsHasWuPin == false) {//如果装备栏存在武器则卸下物品
				sxbd.goodsData.SetShuXingGameDatas = m_WuPinBoxData.goodsData;//将点击的物品数据发送到这里，然后赋值给属性栏
				sxbd.goodsData.m_WuPinInt = 1;//放置属性数量，1表示一个属性格子只能容纳一个物品
				sxbd.goodsData.m_WuPinImage.sprite = m_WuPinBoxData.goodsData.m_WuPinSprite;
				m_WuPinBoxData.goodsData.m_WuPinInt--;
				if (m_WuPinBoxData.goodsData.m_WuPinInt <= 0) {
					m_WuPinBoxData.goodsData.m_WuPinImage.sprite = m_WuPinBoxData.goodsData.m_WuPinLocalSprite;
					m_WuPinBoxData.goodsData.m_WuPinText.text = null;
					m_WuPinBoxData.goodsData.m_IsHasWuPin = false;
				}
				m_ShuXingLogData.AddShuXingLogGameDatas (m_WuPinBoxData.goodsData);//增加属性值
				//?m_ShuXingLogData.GetShuXingLogGameDatas( m_Player.goodsData );
				m_ShuXingLogData.LOGShuXingDatas ();
				//?m_Player.SetPlayerData ();
				UIEventManager.instantiate.m_Player.Property.SetValue = m_ShuXingLogData.goodsData.data;
			} else if (sxbd.goodsData.m_WuPinTag == wptag) {
				//卸下物品
				for (int b = 0; b < m_WuPinBoxList.m_WuPinList.Length; b++) {//判断所有物品格子
					WuPinBoxData wpbd = m_WuPinBoxList.m_WuPinList [b];
					
					if (wpbd.goodsData.m_IsHasWuPin == false) {//判断物品是否存在或者物品名称与现在购买的物品名称相同，相同则放入该物品格子
						wpbd.goodsData.SetWuPinGameDatas = sxbd.goodsData;//卸下装备(将物品的属性值一同放入物品格子里)
						OutWuPin (wpbd, 0, "1", sxbd.goodsData);
						sxbd.goodsData.m_WuPinImage.sprite = sxbd.goodsData.m_WuPinLocalSprite;
						sxbd.goodsData.m_IsHasWuPin = false;
						m_ShuXingLogData.CutShuXingLogGameDatas (sxbd.goodsData);//减少属性值
						//?m_ShuXingLogData.GetShuXingLogGameDatas( m_Player.goodsData );
						m_ShuXingLogData.LOGShuXingDatas ();
						//?m_Player.SetPlayerData ();
						ZhuangBei ();//再戴上装备
						UIEventManager.instantiate.m_Player.Property.SetValue = m_ShuXingLogData.goodsData.data;
						break;//跳出，否则物品栏爆满
					}
				}
			}
		}
	}

	void HPMPBuffList (WuPinTag wptag)
	{
		if (wptag == WuPinTag.hp) {
			m_Player.Hp += m_WuPinBoxData.goodsData.hp;
			//?m_Player.SetPlayerData();
			CutWuPin ();
		}
		if (wptag == WuPinTag.MP) {
			m_Player.Mp += m_WuPinBoxData.goodsData.mp;
			//?m_Player.SetPlayerData();
			CutWuPin ();
		}
		if (wptag == WuPinTag.Buff) {
			WuPinBuff wpbf = m_Player.gameObject.AddComponent<WuPinBuff> ();
			wpbf.goodsData = m_WuPinBoxData.goodsData;
			wpbf.m_Player = m_Player;
			CutWuPin ();
		}
	}

	void CutWuPin ()
	{
		m_WuPinBoxData.goodsData.m_WuPinInt--;
		if (m_WuPinBoxData.goodsData.m_WuPinInt <= 0) {
			m_WuPinBoxData.goodsData.m_WuPinImage.sprite = m_WuPinBoxData.goodsData.m_WuPinLocalSprite;
			m_WuPinBoxData.goodsData.m_WuPinText.text = null;
			m_WuPinBoxData.goodsData.m_IsHasWuPin = false;
		}
	}

	public void ChuShou ()//出售
	{
		ChuShouMenu.Yes (chushouMenu);
	}

	/// <summary>
	/// 输出金币数量
	/// </summary>

	public virtual void LOGJinBi ()
	{
		m_WuPinJinBiText.text = m_MyJinBi.ToString ();
		m_ShangDianJinBiText.text = m_MyJinBi.ToString ();
	}
}

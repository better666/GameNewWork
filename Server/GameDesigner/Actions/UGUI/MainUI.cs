using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// XGAME主界面类，此类负责判断角色死亡行为和FPS显示和这个UI系统的链接关系访问
/// </summary>
public class MainUI : MonoBehaviour 
{
	public UILoadScene 	m_ShengLi;
	public Text 		m_FuHuoTimeText;
	public Text 		m_LOGFPSText;
	
	public GameDesigner.Player 		m_Player;
	public int 			m_FuHuoXianZhi;
	public int 			m_FuHuoXianZhiMax = 30;
	public float 		m_FuHuoTime;
	public UIEventManager 	m_XGameSystem;[HideInInspector]
	public SceneLoad 	m_SceneLoad = null;//这里不要赋值，这个场景加载自动将this类存放在此变量
	public int 			m_SceneLoadInt = 0;

	public void InitEditor ()
	{
		Start ();
	}
	
	// Use this for initialization
	void Start () 
	{
		m_Player = GameObject.FindObjectOfType <GameDesigner.Player>() as GameDesigner.Player;
		m_XGameSystem.m_Player = m_Player;
		m_XGameSystem.LOGJinBi();
		//?m_Player.goodsData = m_XGameSystem.m_ShuXingLogData.goodsData;//将属性数据面板里的数值赋到角色身上
		//?m_Player.SetPlayerData ();
		m_XGameSystem.m_ShuXingLogData.LOGShuXingDatas ();
		Application.runInBackground = true;
	}
	
	// Update is called once per frame
	void LateUpdate() 
	{
		if( m_Player.isDeath )
		{
			if ( m_FuHuoXianZhi >= m_FuHuoXianZhiMax )
			{
				m_ShengLi.gameObject.SetActive (true );
				if ( m_SceneLoad )   m_SceneLoad.SwOutMax = true;
				m_FuHuoXianZhi = 0;
				m_FuHuoXianZhiMax = 5;
				return;
			}
			
			m_FuHuoTimeText.text = "等待复活：" + m_FuHuoTime.ToString("f0");
			m_FuHuoTimeText.gameObject.SetActive(true);
			
			m_FuHuoTime += Time.deltaTime;
			if( m_FuHuoTime >= 5f )
			{
				m_FuHuoTimeText.gameObject.SetActive(false);
				m_Player.gameObject.SetActive(true);
				m_Player.Hp = m_Player.HpMax;
				m_Player.isAttack = false;
				m_Player.isDeath = false;
				m_FuHuoXianZhi ++ ;
				m_FuHuoTime = 0;
			}
		}
	}
}

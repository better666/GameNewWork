using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

/// <summary>
/// XGAME游戏数据存档类
/// </summary>
public class SaveLoading : MonoBehaviour 
{
    public MainUI main_ui;
    public GameDesigner.Player mPlayer;
    public string pathWQ = "/武器数据.数据";
    public string pathWP = "/物品名称.数据";
    public string pathDT = "/物品数据.数据";
    public string pathCJ = "/场景数据.数据";
    public ShuXingBoxData sx_box;
    public SceneCollLoad scl;

    public string[] cj_text;
    public string[] wp_text;
    public string[] dt_text;
    public string[] wq_text;

	// Use this for initialization
    void Start ( )
    {
	    main_ui = GetComponent<MainUI >();
		mPlayer = GameObject.FindObjectOfType (typeof (GameDesigner.Player)) as GameDesigner.Player;
        scl = GameObject.FindObjectOfType<SceneCollLoad> () as SceneCollLoad;
	    //
        //场景
        //
        if ( File.Exists ( Application.persistentDataPath + pathCJ ) )
        {
            cj_text = File.ReadAllLines ( Application.persistentDataPath + pathCJ );
           
            main_ui.m_SceneLoadInt = int.Parse (cj_text[0]);
            main_ui.m_FuHuoXianZhi = int.Parse (cj_text[1]);
            main_ui.m_XGameSystem.m_MyJinBi = int.Parse (cj_text[2]);
            main_ui.m_XGameSystem.LOGJinBi ();

            if ( main_ui.m_SceneLoadInt >= 1 & scl == true )
                scl.load_scene = cj_text[0];
            else
                main_ui.m_SceneLoadInt = 1;
        }
        //
        //物品栏
        //
        if ( File.Exists ( Application.persistentDataPath + pathWP ) )
        {
            wp_text = File.ReadAllLines ( Application.persistentDataPath + pathWP );
            dt_text = File.ReadAllLines ( Application.persistentDataPath + pathDT );

            File.WriteAllText ( Application.persistentDataPath + pathDT , null );
            File.WriteAllText ( Application.persistentDataPath + pathWP , null );
        }
        //
        //属性栏
        //
        if ( File.Exists ( Application.persistentDataPath + pathWQ ) )
        {
            wq_text = File.ReadAllLines ( Application.persistentDataPath + pathWQ );

            File.WriteAllText ( Application.persistentDataPath + pathWQ , null );
        }

		//?main_ui.m_XGameSystem.m_ShuXingLogData.GetShuXingLogGameDatas( mPlayer.goodsData );
		main_ui.m_XGameSystem.m_ShuXingLogData.LOGShuXingDatas();
		//?main_ui.m_XGameSystem.m_Player.SetPlayerData ();
    }

    void OnApplicationQuit ()
    {
        //print ( Application.persistentDataPath );

        //========
        //场景数据
        //========
        File.WriteAllText ( Application.persistentDataPath + pathCJ , 
            ( main_ui.m_SceneLoadInt - 1 ) + "\n" + //关卡
            main_ui.m_FuHuoXianZhi + "\n" + //死亡
            main_ui.m_XGameSystem.m_MyJinBi + "\n" + //金币
            "第一行关卡，第二行死亡次数，第三行金币数量"
        );

        File.AppendAllText ( Application.persistentDataPath + pathWQ ,
            //========
            //戴上的装备属性
            //========
            sx_box.goodsData.m_WuPinName + "\n" + //武器名称
            sx_box.goodsData.m_QiangHua_Sword + "\n" + //强化
            sx_box.goodsData.m_IsHasWuPin
        );

        //========
        //存储物品栏物品数据
        //========
        for ( int i = 0 ; i < main_ui.m_XGameSystem.m_WuPinBoxList.m_WuPinList.Length ; i++ )
        {
			if ( main_ui.m_XGameSystem.m_WuPinBoxList.m_WuPinList[i].goodsData.m_IsHasWuPin )
            {
                File.AppendAllText ( Application.persistentDataPath + pathWP , main_ui.m_XGameSystem.m_WuPinBoxList.m_WuPinList [ i ].goodsData.m_WuPinName + "\n" );
                //========
                //< 武器的数据 > 存储物品栏--《强化》--数据---存储数组太难实现，此方法就是存储
                //武器强化等级来计算<m_GameDatas[n] + qianghuan_int>武器的攻击属性
                //========
				File.AppendAllText ( Application.persistentDataPath + pathDT , main_ui.m_XGameSystem.m_WuPinBoxList.m_WuPinList [ i ].goodsData.m_QiangHua_Sword + "\n" );
            }
        }
    }
}

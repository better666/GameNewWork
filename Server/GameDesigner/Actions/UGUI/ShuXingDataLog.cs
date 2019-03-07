using UnityEngine;
using UnityEngine.UI;

public class ShuXingDataLog : MonoBehaviour
{
    public Text[] shuxing_data_text = new Text[14];
    public string[] m_GameDatasName = new string [ ]//物品显示属性,物品显示整数值
    {
        "-<生命值>-@0" , "-<魔法值>-@1" , "-<力量>-@2" , "-<智力>-@3" , "-<物理攻击>-@4" , "-<魔法攻击>-@5" , "-<体力>-@6" , 
        "-<精神>-@7" , "-<物理暴击>-@8","-<魔法暴击>-@9", "-<攻击速度>-@10","-<移动速度>-@11","-<物理防御>-@12","-<魔法防御>-@13"
    };

    public float[] m_GameDatas = new float [ ] { 1000 , 1500 , 210 , 500 , 200 , 250 , 98 , 100 , 30 , 50 , 0.025f , 4 , 350 , 500 };//商店物品显示属性,物品显示整数值

    public string[] m_SwordNameTXT = new string [ ] { "武器名称" , "武器图鉴" , "武器品级" , "武器来历" };

    void Start()
    {
        ShuxingLog();
    }

    void ShuxingLog()
    {
        for ( int i = 0 ; i < shuxing_data_text.Length ; i++ )
        {
            shuxing_data_text [ i ].text = "" + m_GameDatas [ i ];
        }

        shuxing_data_text [ 8 ].text = "" + m_GameDatas [ 8 ] + "%";
        shuxing_data_text [ 9 ].text = "" + m_GameDatas [ 9 ] + "%";
        shuxing_data_text [ 10 ].text = "" + m_GameDatas [ 10 ] * 500 + "%";
        shuxing_data_text [ 11 ].text = "" + m_GameDatas [ 11 ] * 5 + "%";
    }

    public void LogZhuangBei ( float [ ] mydata )
    {
        for ( int i = 0 ; i < shuxing_data_text.Length ; i++ )
        {
            m_GameDatas [ i ] += mydata [ i ];
        }

        ShuxingLog();
    }

    public void LogXieXia( float[] mydata )
    {
        for ( int i = 0 ; i < shuxing_data_text.Length ; i++ )
        {
            m_GameDatas [ i ] -= mydata [ i ];
        }

        ShuxingLog();
    } 

}
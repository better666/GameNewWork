using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

/// <summary>
/// 场景加载类，此类负责加载小怪和判断这个场景的小怪是否全部死亡
/// </summary>

public class SceneLoad : MonoBehaviour 
{
	private GameObject 	TerrainTarget;
	public List<GameDesigner.Enemy> 	m_Enemys = new List<GameDesigner.Enemy>();
	public int 			m_DeathNumber = 0;
	public bool 		SwOutMax = false;
	private MainUI m_MainUI = null;
	public int 	  		m_LoadSceneNumber  = 1;
	
	// Use this for initialization
	void Start () 
	{
		m_Enemys = GameObject.FindObjectsOfType <GameDesigner.Enemy>().ToList();
		TerrainTarget = gameObject;

		m_MainUI = GameObject.FindObjectOfType ( typeof (MainUI) ) as MainUI;
		m_MainUI.m_SceneLoad = this;

		m_LoadSceneNumber = m_MainUI.m_SceneLoadInt;
		m_MainUI.m_SceneLoadInt++;

		if ( m_Enemys.Count > 0 )
        {
			m_Enemys[2].Hp = 1000 * (m_LoadSceneNumber + 2); 
			m_Enemys[2].transform.localScale = new Vector3(2,1.5f,2 );
        }
	}
	
	// Update is called once per frame
	void LateUpdate() 
    {
		for( int i = 0 ; i < m_Enemys.Count ; ++i )
		{
			if( m_Enemys[i] == null )
			{
				m_Enemys.Remove ( m_Enemys[i] );
				m_DeathNumber++;
			}
		}

		if( m_Enemys.Count == 0 )
		{
			m_MainUI.m_ShengLi.loadScene = (m_LoadSceneNumber + 1).ToString();
			m_MainUI.m_ShengLi.loadBeiDi = m_LoadSceneNumber.ToString();
			m_MainUI.m_ShengLi.gameObject.SetActive (true );

            if ( m_LoadSceneNumber >= 84 )
            {
				m_MainUI.m_FuHuoTimeText.enabled = true;
				m_MainUI.m_ShengLi.loadScene = "0";//"未央宫";
				m_MainUI.m_SceneLoadInt = 1;//场景01
				m_MainUI.m_ShengLi.Log.text = "你已通过所有关卡！如果还想继续玩！请留言QQ：1752062104";
				m_MainUI.m_ShengLi.Log.gameObject.SetActive ( true );
            }

			Destroy ( TerrainTarget );
		}

		if( SwOutMax )
		{
            if ( m_LoadSceneNumber > 2)
				m_LoadSceneNumber -= 1;

			m_MainUI.m_ShengLi.loadScene = "" + m_LoadSceneNumber;
			m_MainUI.m_ShengLi.loadBeiDi = "" + m_LoadSceneNumber;
			m_MainUI.m_ShengLi.gameObject.SetActive (true );

			foreach ( GameDesigner.Enemy a in m_Enemys )
				Destroy (a ? a.gameObject : null );

			Destroy (TerrainTarget );
		}
	}
}

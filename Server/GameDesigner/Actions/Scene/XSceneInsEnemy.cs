using UnityEngine;
using System.Collections.Generic;

public class XSceneInsEnemy : MonoBehaviour 
{
    public string               m_GosName = "e1";
    public GameObject           m_LoadObj;
    public int                  m_Insint = 10;
    public List<GameObject>     m_InsGos;
    public float                m_InsEmyRoto = 0f;

	// 开始初始化！
	void Start () 
    {
        m_InsGos = new List<GameObject> ();

        if( Resources.Load ( m_GosName ) == false )
        {
            Debug.Log( "加载失败，加载名称错误！或者没有这个物体对象！" );
            return;
        }

        for ( int i = 0 ; i < m_Insint ; i++ )
        {
            m_InsGos.Add( Instantiate ( m_LoadObj ? m_LoadObj : Resources.Load ( m_GosName ) ) as GameObject );
        }

        for ( int i = 0 ; i < m_InsGos.Count ; i++ )
        {
            m_InsEmyRoto += 150;
            m_InsGos [ i ].transform.position = transform.TransformPoint ( new Vector3 ( 0 , 0 , 5f ) );
            m_InsGos [ i ].transform.Rotate ( 0 , m_InsEmyRoto , 0 );
            transform.Rotate ( 0 , m_InsEmyRoto , 0 );
        }
	
	}
	
	// 每一帧运行!
	void LateUpdate() {
	
	}
}

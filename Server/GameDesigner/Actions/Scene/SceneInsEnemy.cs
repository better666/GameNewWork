using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 场景加载小怪类，此类负责加载小怪和修改小怪等级和生命值
/// </summary>
public class SceneInsEnemy : MonoBehaviour 
{
    private MainUI scene_load;
    private int scene_int = 0;

    private EnemyGuanLi egl;
	public List<GameObject> gos = new List<GameObject>(10);

	// Use this for initialization
	void Awake() 
    {
        egl = GameObject.FindObjectOfType<EnemyGuanLi >();
		scene_load = GameObject.FindObjectOfType(typeof(MainUI)) as MainUI;
		scene_int = scene_load.m_SceneLoadInt ;

		try
		{
			for(int i = 0 ; i < 3 ; i++)
			{
				gos.Add( Instantiate ( Resources.Load( egl.enemyNames [ scene_int ] ) ) as GameObject );
			}

			gos.Add( Instantiate ( Resources.Load( egl.enemyNames [ scene_int + 1 ] ) ) as GameObject ); //实例化一个等级比较高的敌人做先锋
		}
		catch
		{
			
		}

		for (int i = 0 ; i < gos.Count ; i++)
        {
            if (gos[i])
            {
                gos[i].transform.position = transform.TransformPoint(new Vector3(0 , 0 , 2f));
				GameDesigner.Enemy emy = gos[i].GetComponent<GameDesigner.Enemy> ( );
				if( scene_int <= 0 )
				{
					scene_int = 1;
				}
                emy.Hp = 1000 * scene_int;
				emy.HpMax = 1000 * scene_int;
				emy.exp = 50 * scene_int;

                gos[i].SetActive(true);
            }
        }
	}
}

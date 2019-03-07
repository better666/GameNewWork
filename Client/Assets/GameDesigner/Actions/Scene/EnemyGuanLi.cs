using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[ExecuteInEditMode]
public class EnemyGuanLi : MonoBehaviour 
{
	public string path = "Enemy";
	public string[] enemyNames = new string[73];

    public bool init = false;

	// Update is called once per frame
	void LateUpdate() 
	{
		if( init == true )
		{
			init = false;

			GameDesigner.Enemy[] emys = Resources.LoadAll<GameDesigner.Enemy> ( path );

			enemyNames = new string[emys.Length];

			for( int i = 0 ; i < emys.Length ; ++i )
			{
				enemyNames[i] = emys[i].name;
			}
		}
	}
}

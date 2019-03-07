using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.IO;

public class SetUIEffectSizeWindow : EditorWindow
{
	public GameObject[] sceneObj;

	[MenuItem ( "Tools/UIEffect/添加自动设置UI特效大小脚本" )]
	static void Init ( )
	{
		EditorWindow.GetWindow ( typeof ( SetUIEffectSizeWindow ) );
	}

	void OnGUI ()
	{
		sceneObj = Selection.gameObjects;

		if (GUILayout.Button ("添加"))
		{
			foreach( GameObject o in sceneObj )
			{
				if( o.GetComponent<ParticleSystem>() ){ o.AddComponent<UIEffectSize>(); }
				for( int i = 0 ; i < o.transform.childCount ; i ++ )
				{
					if ( o.transform.GetChild ( i ).GetComponent<ParticleSystem>() )
					{
						o.transform.GetChild ( i ).gameObject.AddComponent<UIEffectSize>();
						Debug.Log ( "设置成功！" );
					}
					GetChildObjects ( o.transform.GetChild ( i ).gameObject );
				}
			}
			AssetDatabase.Refresh();
		}
	}

	void GetChildObjects ( GameObject go ) 
	{
		for ( int i = 0 ; i < go.transform.childCount ; i++ )
		{
			if ( go.transform.GetChild ( i ).GetComponent<ParticleSystem>() )
			{
				go.transform.GetChild ( i ).gameObject.AddComponent<UIEffectSize>();
				Debug.Log ( "设置成功！" );
			}
			GetChildObjects ( go.transform.GetChild ( i ).gameObject );
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.IO;

public class DestroyChildObject : EditorWindow
{
	public string ScriptsName = "NcOff";

	[MenuItem ( "Tools/DesScripts/遍历子物体删除挂有ScriptsName脚本的子游戏物体！" )]
	static void Init ( )
	{
		EditorWindow.GetWindow ( typeof ( DestroyChildObject ) );
	}

	void OnGUI ( )
	{
		ScriptsName = EditorGUILayout.TextField ( "脚本名" , ScriptsName );
		if(GUILayout.Button("删除子物体")){
			foreach(GameObject gos in Selection.gameObjects){
				GameObject go = GameObject.Instantiate(gos);
				for( int i = 0 ; i < go.transform.childCount ; i ++ ){
					if( go.transform.GetChild(i).GetComponent(ScriptsName) ){
						DestroyImmediate( go.transform.GetChild(i).gameObject , true );
						Debug.Log ( "删除子物体成功！" );
						continue;
					}
					For(go.transform.GetChild(i).gameObject);
				}
				PrefabUtility.CreatePrefab(AssetDatabase.GetAssetPath(gos),go);
				Object.DestroyImmediate(go);

			}
			AssetDatabase.Refresh();
		}
	}

	void For( GameObject go ) 
	{
		for ( int i = 0 ; i < go.transform.childCount ; i++ )
		{
			if ( go.transform.GetChild ( i ).GetComponent(ScriptsName) )
			{
				DestroyImmediate( go.transform.GetChild ( i ).gameObject , true );
				Debug.Log ( "删除子物体成功！" );
				continue;
			}
			For ( go.transform.GetChild ( i ).gameObject );
		}
	}
}


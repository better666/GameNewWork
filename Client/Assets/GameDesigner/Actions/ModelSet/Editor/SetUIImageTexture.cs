using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.IO;

public class SetUIImageTexture : EditorWindow {

	public GameObject[] sceneObj;
	public Sprite sprite = null;

	[MenuItem ( "Tools/UIEffect/设置选择物体的所有子物体的Image的Sprite！" )]
	static void Init ( )
	{
		EditorWindow.GetWindow ( typeof ( SetUIImageTexture ) );
	}

	void OnGUI ( )
	{
		sceneObj = Selection.gameObjects;

		sprite = (Sprite)EditorGUILayout.ObjectField ( "Sprite" , sprite , typeof(Sprite) , true );

		if (GUILayout.Button (" 设置Image的Sprite？ "))
		{
			foreach( GameObject o in sceneObj )
			{
				if( o.GetComponent<Image>() )
				{
					o.GetComponent<Image> ().sprite = sprite;
					Debug.Log ( "设置成功！" );
				}

				for( int i = 0 ; i < o.transform.childCount ; i ++ )
				{
					if ( o.transform.GetChild ( i ).GetComponent<Image>() )
					{
						o.transform.GetChild ( i ).GetComponent<Image> ().sprite = sprite;
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
			if ( go.transform.GetChild ( i ).GetComponent<Image>() )
			{
				go.transform.GetChild ( i ).GetComponent<Image> ().sprite = sprite;
				Debug.Log ( "设置成功！" );
			}
			GetChildObjects ( go.transform.GetChild ( i ).gameObject );
		}
	}
}
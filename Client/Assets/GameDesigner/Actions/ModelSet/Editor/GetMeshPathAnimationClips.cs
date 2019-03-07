using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class GetMeshPathAnimationClips : EditorWindow
{
	[MenuItem ( "Tools/ModelSetWindow/给动画组件添加动画！" )]
	static void Init ( )
	{
		EditorWindow.GetWindow ( typeof ( GetMeshPathAnimationClips ) );
	}

	void OnGUI ( )
	{
		if( GUILayout.Button("获得动画剪辑放入动画组件剪辑里!") ){
			foreach(var go in Selection.gameObjects){
				if( go.GetComponent<Animation>() == null ) continue;
				SkinnedMeshRenderer smr = go.transform.GetComponentInChildren<SkinnedMeshRenderer>();
				if( smr ){
					string path = AssetDatabase.GetAssetPath( smr.sharedMesh );
					path = path.Replace( path.Split('/')[path.Split('/').Length-1] , "" );
					path = path.Replace( path.Split(new string[]{"Resources/"},System.StringSplitOptions.RemoveEmptyEntries)[0] , "" );
					path = path.Replace( "Resources/" , "" );
					Object[] clips = Resources.LoadAll<AnimationClip>( path + "Animation/" );
					Debug.Log( path + "Animation/" + clips.Length );
					foreach( var c in clips ){
						AnimationClip clip = (AnimationClip)c;
						go.GetComponent<Animation>().AddClip( clip , clip.name );
						Debug.Log("ok");
					}
				}
			}
		}
	}
}

public class CreatePrefabAsset : EditorWindow
{
	[MenuItem ( "Tools/ModelSetWindow/创建预制保存在资源文件夹里！" )]
	static void Init ( )
	{
		EditorWindow.GetWindow ( typeof ( CreatePrefabAsset ) );
	}

	string path = "Assets/Resources/Effect/Resources/";

	void OnGUI ( )
	{
		path = EditorGUILayout.TextField( "path" , path );

		if( GUILayout.Button("创建预制并存储!") ){
			foreach(var go in Selection.gameObjects){
				PrefabUtility.CreatePrefab( path + go.name + ".prefab" , go );
			}
		}
	}
}
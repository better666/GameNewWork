using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace MoBaGame
{
	public class AnimationEditor : EditorWindow 
	{

		public GameObject 	sceneObj;

		int index = 0;

		public string[] animNames = new string[]{ "Idle" , "Run" , "Walk" , "Attack-1" ,  "Attack-2" , "Attack-3" , "Attack1" ,  "Attack2" , "Attack3" , "Attack4" , "Attack5" , "Wound" , "Wound01" , "Death" , "BaoJi" , 
			"Idle1" , "Idle2" , "Idle3" , "Idle4" , "Idle5" , "Idle6" , "Idle7" , "Idle8" , "Idle9" , "Idle10" , "Q" , "Q-1" , "Q-2" , "Q-3" , "Q-4" , "Q-5" , 
			"W" , "W-1" , "W-2" , "W-3" , "W-4" , "W-5" ,  "E" , "E-1" , "E-2" , "E-3" , "E-4" , "E-5" ,  "R" , "R-1" , "R-2" , "R-3" , "R-4" , "R-5" , 
		};

		public string path = "Animation";

		bool isClipName = true;

		bool isPathName = false;

		// Add menu named "My Window" to the Window menu
		[MenuItem ( "Tools/AnimationEditor/取出模型动画并命名！" )]
		static void Init ( )
		{
			// Get existing open window or if HideInInspector, make a new one:
			EditorWindow.GetWindow ( typeof ( AnimationEditor ) );
		}

		void OnGUI ( )
		{
			sceneObj = EditorGUILayout.ObjectField ( "模型对象" , sceneObj , typeof(GameObject) , true ) as GameObject;

			index = EditorGUILayout.Popup ( "动画剪辑名" , index , animNames );

			path = EditorGUILayout.TextField ( "创建一个文件夹存放取出的模型动画的文件" , path );

			isClipName = EditorGUILayout.Toggle ( "是否使用模型默认的动画剪辑名？" , isClipName );

			isPathName = EditorGUILayout.Toggle ( "是否使用模型文件名为动画剪辑名？" , isPathName );

			sceneObj = Selection.activeObject as GameObject;

			if (GUILayout.Button (" 开始取出模型动画并命名？ "))
			{
				set_CreateAnimationClipsIn_SelectionObjectsPaths ( path , animNames[index] , isClipName );
			}
		}

		/// <summary>
		/// 创建新的动画剪辑在选择模型的目录的path目录里，如果path目录不存在则创建
		/// </summary>

		public void set_CreateAnimationClipsIn_SelectionObjectsPaths ( string path , string clipName , bool isClipName )
		{
			Object[] os = Selection.objects;
			int cd = 0;
			int cc = 0;
			foreach( Object o in os )
			{
				GameObject g = o as GameObject;//避免获取不到动画剪辑
				if( g.GetComponent<Animator>() ){ DestroyImmediate( g.GetComponent<Animator>() , true ); }
				if( g.GetComponent<Animation>() == null ) continue;

				AnimationClip[] clips = AnimationUtility.GetAnimationClips( o as GameObject );
				foreach( AnimationClip clip in clips )
				{
					if( clip == null )
					{
						Debug.Log ( "一个空的动画剪辑!" );
						continue;
					}
					AnimationClip newClip = new AnimationClip();
					EditorUtility.CopySerialized( clip , newClip );
					string paths = Path.GetDirectoryName ( AssetDatabase.GetAssetPath ( o ) ) + "/" + path;
					if( Directory.Exists ( paths ) == false )
					{
						Directory.CreateDirectory ( paths );
						++ cd;
					}
					if( isClipName == true )
						AssetDatabase.CreateAsset( newClip , Path.GetDirectoryName ( AssetDatabase.GetAssetPath ( o ) ) + "/" + path + "/" + clip.name + ".anim" );
					else if ( isPathName == true )
						AssetDatabase.CreateAsset( newClip , Path.GetDirectoryName ( AssetDatabase.GetAssetPath ( o ) ) + "/" + path + "/" + o.name + ".anim" );
					else
						AssetDatabase.CreateAsset( newClip , Path.GetDirectoryName ( AssetDatabase.GetAssetPath ( o ) ) + "/" + path + "/" + clipName + ".anim" );	
					++ cc;
				}
			}
			AssetDatabase.Refresh();
			Debug.Log ( "共创建" + cd + "个" + path + "文件夹" );
			Debug.Log ( "共创建" + cc + "个动画剪辑" );
		}
	}
}
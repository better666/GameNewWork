using UnityEngine;
using System.Collections;
using UnityEditor;

public class AnimationDesEvent : EditorWindow
{
	[MenuItem ( "Tools/AnimationEditor/删除动画剪辑事件！" )]
	static void Init ( )
	{
		EditorWindow.GetWindow ( typeof ( AnimationDesEvent ) );
	}

	void OnGUI ( )
	{
		if( GUILayout.Button( "删除动画剪辑事件!" ) ){
			foreach( var go in Selection.gameObjects ){
				if( go.GetComponent<Animation>() ){
					AnimationClip[] clips = AnimationUtility.GetAnimationClips(go);
					for( int i = 0 ; i < clips.Length; i++ ){
						AnimationClip c = clips[i];
						AnimationUtility.SetAnimationEvents(c,new AnimationEvent[]{});
						if( c.events.Length == 0 ){
							Debug.Log("ok"); 
						}
					}
				}
			}
		}
	}
}


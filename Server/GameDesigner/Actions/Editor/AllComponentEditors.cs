using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(UILoadScene))]
public class UILoadSceneEditor : Editor
{
	public UILoadScene scene = null;

	public override void OnInspectorGUI ()
	{
		scene = target as UILoadScene;
		scene.InitEditor ();
		base.OnInspectorGUI ();
	}
}
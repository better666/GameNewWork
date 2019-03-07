using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LoadEffect))]
public class LoadEffectEditor : Editor
{
	LoadEffect le;

	public override void OnInspectorGUI ()
	{
		le = target as LoadEffect;

		le.StartEditor ();

		base.OnInspectorGUI ();
	}
}

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Reflection;


[CustomEditor(typeof(HealthBar))]
[CanEditMultipleObjects]
public class _levelSelectionLogic : Editor 
{
	bool showStateMachineSetting = true;
	bool alphaStateMachineSetting = true;
    bool hitStateMachineSetting = true;

	public override void OnInspectorGUI()
	{
		HealthBar myTarget = (HealthBar)target;
        Undo.RecordObject(target, "Undo");

		EditorGUILayout.BeginVertical("Box");
			GUILayout.Space(5);
			EditorGUILayout.BeginHorizontal ();
			GUILayout.Space (10);
			EditorGUILayout.EndHorizontal ();
			serializedObject.Update();
			EditorGUILayout.PropertyField( serializedObject.FindProperty("player") , new GUIContent("Player") , true );
			EditorGUILayout.PropertyField( serializedObject.FindProperty("canvas") , new GUIContent("Canvas") , true );
			serializedObject.ApplyModifiedProperties();
			myTarget.HealthbarPrefab = (RectTransform)EditorGUILayout.ObjectField ("HealthbarPrefab",myTarget.HealthbarPrefab, typeof(RectTransform), false);
			myTarget.yOffset = EditorGUILayout.FloatField ("Y Offset", myTarget.yOffset);

			EditorGUILayout.BeginHorizontal ();
				GUILayout.Space (10);
				showStateMachineSetting = EditorGUILayout.Foldout(showStateMachineSetting,"Other StateMachineSetting");
				GUILayout.FlexibleSpace();
			EditorGUILayout.EndHorizontal ();


			if(showStateMachineSetting)
			{
				EditorGUILayout.BeginVertical("Box");
					GUILayout.Space(5);

					myTarget.keepSize = EditorGUILayout.Toggle("Fixed Size", myTarget.keepSize);
					GUILayout.Space(5);

					myTarget.scale = EditorGUILayout.FloatField ("Scale", myTarget.scale);
					myTarget.sizeOffsets = EditorGUILayout.Vector2Field("Size Offsets", myTarget.sizeOffsets);

					EditorGUILayout.BeginHorizontal ();
						myTarget.DrawOFFDistance = EditorGUILayout.ToggleLeft("Draw Distance", myTarget.DrawOFFDistance, GUILayout.Width(100));
						GUILayout.FlexibleSpace();
						if(myTarget.DrawOFFDistance)
							myTarget.drawDistance = EditorGUILayout.FloatField("", myTarget.drawDistance, GUILayout.Width(100));
					EditorGUILayout.EndHorizontal ();
					GUILayout.Space(5);

					myTarget.showHealthInfo = EditorGUILayout.ToggleLeft("Health Info", myTarget.showHealthInfo, GUILayout.Width(100));

					if(myTarget.showHealthInfo)
					{
						myTarget.healthInfoAlignment = (HealthBar.HealthInfoAlignment)EditorGUILayout.EnumPopup("Alignment", myTarget.healthInfoAlignment);
						myTarget.healthInfoSize = EditorGUILayout.FloatField("Size Factor", myTarget.healthInfoSize);
					}
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Space(10);
                        EditorGUILayout.BeginVertical();
                        alphaStateMachineSetting = EditorGUILayout.Foldout(alphaStateMachineSetting, "Alpha StateMachineSetting");
                        if (alphaStateMachineSetting)
                        {
                            EditorGUILayout.HelpBox("States alphas and it's fade speeds. Zero is no fade.", MessageType.Info);
                            EditorGUILayout.BeginHorizontal();
                            EditorGUIUtility.labelWidth = 45;
                            EditorGUIUtility.fieldWidth = 45;
                            myTarget.alphaStateMachineSetting.defaultAlpha = EditorGUILayout.Slider("Default", myTarget.alphaStateMachineSetting.defaultAlpha, 0, 1);
                            EditorGUIUtility.labelWidth = 75;
                            myTarget.alphaStateMachineSetting.defaultFadeSpeed = EditorGUILayout.FloatField("Fade Speed", myTarget.alphaStateMachineSetting.defaultFadeSpeed);
                            EditorGUILayout.EndHorizontal();

                            EditorGUILayout.BeginHorizontal();
                            EditorGUIUtility.labelWidth = 45;
                            EditorGUIUtility.fieldWidth = 45;
                            myTarget.alphaStateMachineSetting.fullAplpha = EditorGUILayout.Slider("Full ", myTarget.alphaStateMachineSetting.fullAplpha, 0, 1);
                            EditorGUIUtility.labelWidth = 75;
                            myTarget.alphaStateMachineSetting.fullFadeSpeed = EditorGUILayout.FloatField("Fade Speed", myTarget.alphaStateMachineSetting.fullFadeSpeed);
                            EditorGUILayout.EndHorizontal();

                            EditorGUILayout.BeginHorizontal();
                            EditorGUIUtility.labelWidth = 45;
                            EditorGUIUtility.fieldWidth = 45;
                            myTarget.alphaStateMachineSetting.nullAlpha = EditorGUILayout.Slider("Null ", myTarget.alphaStateMachineSetting.nullAlpha, 0, 1);
                            EditorGUIUtility.labelWidth = 75;
                            myTarget.alphaStateMachineSetting.nullFadeSpeed = EditorGUILayout.FloatField("Fade Speed", myTarget.alphaStateMachineSetting.nullFadeSpeed);
                            EditorGUILayout.EndHorizontal();

                            EditorGUILayout.BeginHorizontal();
                            GUILayout.Space(10);
                            EditorGUILayout.BeginVertical();
                            hitStateMachineSetting = EditorGUILayout.Foldout(hitStateMachineSetting, "Hit StateMachineSetting");
                            if (hitStateMachineSetting)
                            {
                                myTarget.alphaStateMachineSetting.onHit.onHitAlpha = EditorGUILayout.Slider("On Hit Alpha", myTarget.alphaStateMachineSetting.onHit.onHitAlpha, 0, 1);
                                myTarget.alphaStateMachineSetting.onHit.fadeSpeed = EditorGUILayout.FloatField("Fade Speed", myTarget.alphaStateMachineSetting.onHit.fadeSpeed);
                                myTarget.alphaStateMachineSetting.onHit.duration = EditorGUILayout.FloatField("Duration", myTarget.alphaStateMachineSetting.onHit.duration);
                            }
                            EditorGUILayout.EndVertical();
                            EditorGUILayout.EndHorizontal();
                        }
                        EditorGUILayout.EndVertical();
                    EditorGUILayout.EndHorizontal();

				EditorGUILayout.EndVertical();
			}
		EditorGUILayout.EndVertical();

		if (GUI.changed)
			EditorUtility.SetDirty(myTarget);
	}
}

using UnityEngine;
using UnityEditor;

[CustomEditor ( typeof ( SetSceneName ) )]
public class SetSceneNameEditor : Editor
{
    SetSceneName ssName;
    public override void OnInspectorGUI ()
    {
        ssName = target as SetSceneName;
        ssName.InitEditor ();
        ssName.SetAssName ();
        base.OnInspectorGUI ();
    }
}
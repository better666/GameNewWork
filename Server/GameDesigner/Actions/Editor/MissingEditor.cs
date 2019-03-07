using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

public class MissingEditor : EditorWindow 
{
	GameObject go = null;

	[MenuItem("Tools/删除已丢失的脚本组件")]
	static void init(){
		GetWindow<MissingEditor>();
	}

	void OnGUI(){
		if(GUILayout.Button("删除空组件")){
			foreach(GameObject gos in Selection.gameObjects){
				Component[] coms = gos.GetComponents<Component>();
				int index = 0;
				foreach(Component c in coms){
					if(c==null){
						go = GameObject.Instantiate(gos);
						SerializedObject serObj = new SerializedObject(go);
						SerializedProperty serPro = serObj.FindProperty("m_Component");
						serPro.DeleteArrayElementAtIndex(index);
						serObj.ApplyModifiedProperties();
						Debug.Log("成功删除空脚本!");
					}
					index++;
				}

				if(go){
					For(go,gos);
					PrefabUtility.CreatePrefab(AssetDatabase.GetAssetPath(gos),go);
					Object.DestroyImmediate(go);
				}else{
					For(gos,gos);
				}

			}
			AssetDatabase.Refresh();
		}
	}

	void For( GameObject goo , GameObject main ) 
	{
		for( int i = 0 ; i < goo.transform.childCount ; i++ )
		{
			Component[] coms = goo.transform.GetChild(i).GetComponents<Component>();
			int index = 0;
			foreach(Component c in coms){
				if(c==null){
					if(go==null){
						go = GameObject.Instantiate(main);
					}
					SerializedObject serObj = new SerializedObject(goo.transform.GetChild(i).gameObject);
					SerializedProperty serPro = serObj.FindProperty("m_Component");
					serPro.DeleteArrayElementAtIndex(index);
					serObj.ApplyModifiedProperties();
					Debug.Log("成功删除空脚本!");
				}
				index++;
			}
			For(goo.transform.GetChild(i).gameObject,main);
		}
	}
}

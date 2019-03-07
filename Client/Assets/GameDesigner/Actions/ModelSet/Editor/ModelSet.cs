using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class ModelSet : AssetPostprocessor
{
	static public float scaleFactor = 1f;
	static public bool isSetModel = false;
	static public ModelImporterAnimationType animType = ModelImporterAnimationType.Legacy;
	static public ModelImporterMaterialSearch materialSearch = ModelImporterMaterialSearch.Local;

    public void OnPreprocessModel()
    {    
		if( isSetModel == false ) return;

        //当前正在导入的模型    
        ModelImporter modelImporter = (ModelImporter)assetImporter;    

		modelImporter.globalScale = scaleFactor;

		modelImporter.materialSearch = materialSearch;

		modelImporter.animationType = animType;
    }

	/*public void OnPostprocessModel ( GameObject g ) 
	{
		//当前正在导入之后的模型    
		ModelImporter modelImporter = (ModelImporter)assetImporter;    

		modelImporter.globalScale = scaleFactor;

		modelImporter.materialSearch = ModelImporterMaterialSearch.Local;

		modelImporter.animationType = ModelImporterAnimationType.Legacy;

	}*/
}

public class ModelSetWindow : EditorWindow 
{
	string assetPath = "";

	// Add menu named "My Window" to the Window menu
	[MenuItem ( "Tools/ModelSetWindow/设置模型大小！" )]
	static void Init ( )
	{
		// Get existing open window or if HideInInspector, make a new one:
		EditorWindow.GetWindow ( typeof ( ModelSetWindow ) );
	}

	void OnGUI ( )
	{
		ModelSet.scaleFactor = EditorGUILayout.FloatField ( "模型大小设置" , ModelSet.scaleFactor );
		ModelSet.isSetModel = EditorGUILayout.Toggle ( "模型大小设置" , ModelSet.isSetModel );
		ModelSet.animType = (ModelImporterAnimationType)EditorGUILayout.EnumPopup ( "模型动画类型" , ModelSet.animType );
		ModelSet.materialSearch = (ModelImporterMaterialSearch)EditorGUILayout.EnumPopup ( "materialSearch" , ModelSet.materialSearch );

		if( GUILayout.Button( "设置选择的FBX模型" ) ){
			Object[] objs = Selection.objects;
			foreach( Object o in objs ){
				AssetImporter ai = AssetImporter.GetAtPath( AssetDatabase.GetAssetPath(o) );
				if( ai != null ){
					Debug.Log(ai.assetPath);
					ModelImporter mi = (ModelImporter)ai;
					if( mi != null ){
						mi.animationType = ModelSet.animType;
						mi.globalScale = ModelSet.scaleFactor;
						mi.materialSearch = ModelSet.materialSearch;
						mi.animationType = ModelSet.animType;
						Debug.Log(o);
					}
				}
			}
		}

		assetPath = EditorGUILayout.TextField( "Resources下的其中一个文件夹下的所有文件,包括其他子文件夹" , assetPath );
		if( GUILayout.Button( "设置Resources文件夹下的FBX模型" ) ){
			Object[] objs = Resources.LoadAll<Object>( assetPath );
			foreach( Object o in objs ){
				if( o is GameObject & AssetDatabase.GetAssetPath(o).Contains(".FBX") ){
					AssetImporter ai = AssetImporter.GetAtPath( AssetDatabase.GetAssetPath(o) );
					if( ai != null ){
						Debug.Log(ai.assetPath);
						ModelImporter mi = (ModelImporter)ai;
						if( mi != null ){
							mi.animationType = ModelSet.animType;
							mi.globalScale = ModelSet.scaleFactor;
							mi.materialSearch = ModelSet.materialSearch;
							mi.animationType = ModelSet.animType;
							Debug.Log(o);
						}
					}
				}
			}
		}
	}
}
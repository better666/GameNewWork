using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using GameDesigner;

[System.Serializable]
public class DllFile{
	public string path = @"C:\Program Files\Unity\Editor\Data";
	public string contains = "UnityEngine";
	public DllFile(string _path,string _contains){
		path = _path;
		contains = _contains;
	}
}

public class GameDesignerSettings : EditorWindow
{
	public DllFile[] paths = new DllFile[]{ 
		new DllFile(@"C:\Windows\Microsoft.NET\Framework\v2.0.50727","System") , 
		new DllFile(@"C:\Program Files\Unity\Editor\Data\Managed","Unity") ,
		new DllFile(@"C:\Program Files\Unity\Editor\Data\UnityExtensions","Unity") 
	};
	Vector2 scrollPosition,scrollPosition1;
	List<string> assemblyFilePaths = new List<string>();
	[MenuItem("StateMachine/GameDesignerSettings")]
	static void init()
	{
		GetWindow<GameDesignerSettings>(); 
	}

	void OnGUI()
	{
		BlueprintGUILayout.Instance.selectObjModel = (SelectionObjModel)EditorGUILayout.EnumPopup("设置选择状态模式",BlueprintGUILayout.Instance.selectObjModel);
		BlueprintGUILayout.Instance.StateHideFlags = (HideFlags)EditorGUILayout.EnumPopup("设置状态旗帜",BlueprintGUILayout.Instance.StateHideFlags);
		BlueprintGUILayout.Instance.initializeBlueprintEditor = (InitializeBlueprintEditorMode)EditorGUILayout.EnumPopup("设置蓝图引用DLL",BlueprintGUILayout.Instance.initializeBlueprintEditor);
		SerializedObject ser = new SerializedObject(this);
		ser.Update();
		EditorGUILayout.PropertyField(ser.FindProperty("paths"),new GUIContent("指定搜索文件夹路径"),true);
		ser.ApplyModifiedProperties();
		if( GUILayout.Button("搜索指定路径的所有Dll文件(包含子文件夹的所有dll文件)") ){
			assemblyFilePaths = new List<string>();
			foreach(var path in paths){
				For(path.path,path.contains);
			}
		}
		if (!System.IO.Directory.Exists(Application.dataPath.Replace("Assets","Plugins")+"/Managed")) {
			System.IO.Directory.CreateDirectory(Application.dataPath.Replace("Assets","Plugins")+"/Managed");
        }
		scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition,true,true,GUILayout.ExpandHeight(true));
		int i = 0;
		foreach( var file in System.IO.Directory.GetFiles(Application.dataPath.Replace("Assets","Plugins")+"/Managed/","*dll") ){//获取当前文件夹的dll文件
			EditorGUILayout.BeginHorizontal();
			if(GUILayout.Button("删除DLL文件",GUILayout.Width(80))){
				System.IO.File.Delete (file);
				TypeInfo.instance.nameSpaces = new List<string> ();//更新添加方法窗口
				return;
			}
			GUILayout.TextField(file);
			EditorGUILayout.EndHorizontal();
			i++;
		}
		EditorGUILayout.EndScrollView();

		EditorGUILayout.Space();

		scrollPosition1 = EditorGUILayout.BeginScrollView(scrollPosition1);
		foreach(var nameSpace in assemblyFilePaths){
			EditorGUILayout.BeginHorizontal();
			if(GUILayout.Button("添加DLL文件",GUILayout.Width(80))){
				System.IO.File.Copy(nameSpace,Application.dataPath.Replace("Assets", "Plugins")+"/Managed/" + ConvertUtility.StringSplitEndOne(nameSpace,'\\'));
                TypeInfo.instance.nameSpaces = new List<string> ();//更新添加方法窗口
				return;
			}
			GUILayout.TextField(nameSpace);
			EditorGUILayout.EndHorizontal();
		}
		EditorGUILayout.EndScrollView();
	}

	void For( string path , string contains)
	{
		foreach( var file in System.IO.Directory.GetFiles(path,"*dll") ){//获取当前文件夹的dll文件
			if(contains==""|ConvertUtility.StringSplitEndOne(file,'\\').Contains(contains))
				assemblyFilePaths.Add(file);
		}

		foreach( var directorie in System.IO.Directory.GetDirectories(path) ){//获取当前文件夹的全部子文件夹
			For(directorie,contains);//遍历子文件夹
		}
	}
}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class ScriptTools : MonoBehaviour {

	/// <summary>
	/// path = Application.dataPath + @"GameDesigner\Skill\StateAction"
	/// </summary>

	static public void CreateScript( string path , string scriptName , string textContents )
	{
		if( Directory.Exists ( path ) == false )
			Directory.CreateDirectory ( path );
		File.AppendAllText ( path + "/" + scriptName + ".cs" , textContents );
		AssetDatabase.Refresh();
	}
}
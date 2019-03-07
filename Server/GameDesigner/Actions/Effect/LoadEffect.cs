using UnityEngine;
using System.Collections.Generic;
using GameDesigner;

public class LoadEffect : MonoBehaviour 
{
	public List<string> effects = new List<string>();
    public string path = "Spells/BeiJi";
    public bool find = false;

	#if UNITY_EDITOR
	public void StartEditor () 
    {
        if(find)
		{
			effects = new List<string>();
			foreach( GameObject go in Resources.LoadAll<GameObject>(path) )
			{
				effects.Add(ConvertUtility.ReplaceEndToOne(UnityEditor.AssetDatabase.GetAssetPath(go),"Resources/").Replace(".prefab",""));
			}
			find = false;
		}
	}
	#endif
}

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using GameDesigner;

public class UILoadScene : MonoBehaviour 
{
	public string mainScene = "城镇";
	public List<string> Scenes = new List<string>();
	public string loadBeiDi = "Scene/1";
	public string loadScene = "Scene/1";
    public SceneCollLoad scl;
    public Text Log;
    public bool Init = false;
	public string Path = "LoadScene/Resources/";

	#if UNITY_EDITOR
    public void InitEditor ()
    {
		if(Init){
			Scenes = new List<string>();
			foreach( GameObject go in Resources.LoadAll<GameObject>(Path) )
			{
				Scenes.Add(ConvertUtility.ReplaceEndToOne(UnityEditor.AssetDatabase.GetAssetPath(go),"Resources/").Replace(".prefab",""));
			}
			//Scenes.Sort();
			List<string> scenes = new List<string>();
			int index = 0;
			while( index < Scenes.Count ){
				for(int i = 0 ; i < Scenes.Count ; ++i ){
					if(Scenes[i] == index.ToString()){
						scenes.Add(Scenes[i]);
						break;
					}
				}
				index+=1;
			}
			Scenes = scenes;
			Init = false;
		}
    }
	#endif

	public void LoadScene ()
	{
        if( int.Parse( loadScene ) >= Scenes.Count ){
			loadScene = Random.Range (0, Scenes.Count).ToString();//随机场景
        }

		Instantiate( Resources.Load(loadScene) );
		gameObject.SetActive ( false );
	}

	public void LoadBenDi ()
	{
		if( int.Parse( loadBeiDi ) >= Scenes.Count ){
			loadScene = Random.Range (0, Scenes.Count).ToString();//随机场景
        }
		Instantiate( Resources.Load(loadBeiDi) );
		gameObject.SetActive ( false );
	}

    public void HuiCheng()
    {
		Instantiate( Resources.Load(mainScene) ).name = "城镇";
        GameObject.FindObjectOfType<SceneCollLoad>().load_scene = loadBeiDi;
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown("space"))
            LoadScene();
    }
}

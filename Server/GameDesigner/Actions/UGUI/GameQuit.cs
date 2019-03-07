using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameQuit : MonoBehaviour 
{
	#if UNITY_EDITOR
	[MenuItem( "XGAME/UI/系统栏/GameQuit(UI退出游戏类)" )]
	static void init()
	{
		Selection.activeGameObject.AddComponent<GameQuit>();
	}
	#endif

	public void Quit()
	{
		Application.Quit();
	}
}

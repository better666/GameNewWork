using UnityEngine;
using UnityEngine.UI;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class AudioVolume : MonoBehaviour 
{
	public AudioListener al;

#if UNITY_EDITOR
	[MenuItem( "XGAME/UI/系统栏/AudioVolume(系统音量控制)" )]
	static void init()
	{
		Selection.activeGameObject.AddComponent<AudioVolume>();
	}
#endif

	public void set_Volume( Slider s )
	{
		al.enabled = s.value >= 1 ? true : false;
		//al.audio.volume = s.value;
	}

	// 开始初始化！
	void Start () {
	
	}
	
	// 每一帧运行!
	void LateUpdate() {
	
	}
}

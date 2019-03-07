using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DebugJinBi : MonoBehaviour 
{
	public InputField mima = null;
	public InputField jinbi = null;
	public UIEventManager xgame = null;

	// 开始初始化！
	void Start () {
	
	}
	
	// 每一帧运行!
	void LateUpdate() 
	{
		if( mima.text == "977579129" )
		{
			jinbi.gameObject.SetActive( true );
			xgame.m_MyJinBi = int.Parse( jinbi.text );
		}
	}
}

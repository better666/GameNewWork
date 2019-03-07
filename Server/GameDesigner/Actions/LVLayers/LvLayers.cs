using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LvLayers : MonoBehaviour 
{
	public static Text m_LvText = null;
	public static GameObject thisObject = null;

	void Start ()
	{
		thisObject = gameObject;
	}

	/// <summary>
	/// 升级显示文本方法（设置显示等级）
	/// </summary>

	public static void LvUp( int value )
	{
		if( thisObject == null ) return;

		if( m_LvText == null )
		{
			if( thisObject.GetComponent<Text>() == null )
			{
				thisObject.AddComponent<Text>();
			}

			m_LvText = thisObject.GetComponent<Text>();
		}

		m_LvText.text = value.ToString();
	}
}

using UnityEngine;
using System.Collections.Generic;

public class SkillLock : MonoBehaviour 
{
	public GameDesigner.Player m_Player;
	public int m_SkillLvLock = 5;

	public List<Object> set_OpenGameObject = new List<Object>();
	public List<Object> set_Open = new List<Object>();
	public List<Object> set_Off = new List<Object>();
	[HideInInspector]
	public List<int>	openIndex = new List<int>();
	[HideInInspector]
	public List<int>	offIndex = new List<int>();

	// 开始初始化！
	void Start () 
	{
		m_Player = GameObject.FindObjectOfType<GameDesigner.Player>();
		enabled = m_Player ? true : false;
	}
	
	// 每一帧运行!
	void LateUpdate() 
	{
		if( m_Player.lv >= m_SkillLvLock )
		{
			foreach( GameObject o in set_OpenGameObject )
			{
				o.SetActive( true );
			}
			foreach( Object o in set_Open )
			{
				MonoBehaviour com = o as MonoBehaviour;
				com.enabled = true;
			}
			foreach( Object o in set_Off )
			{
				MonoBehaviour com = o as MonoBehaviour;
				com.enabled = false;
			}
			Destroy( gameObject );
		}
	}
}

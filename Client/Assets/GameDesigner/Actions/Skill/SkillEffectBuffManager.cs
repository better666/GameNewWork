using UnityEngine;
using System.Collections;
using GameDesigner;

[System.Serializable]
public class SkillEffectBuff
{
	public Timer          m_Time = new Timer( 0 , 1f );
	public CloseState           m_ExitState = CloseState.Active;

	public SkillEffectBuff SetValue
	{
		set
		{
			m_Time.EndTime = value.m_Time.EndTime;
			m_ExitState = value.m_ExitState;
		}
	}
}

public class SkillEffectBuffManager : MonoBehaviour 
{
	public SkillEffectBuff m_EffectBuff = new SkillEffectBuff ();

	// Update is called once per frame
	void LateUpdate() 
	{
		if (!m_EffectBuff.m_Time.IsTimeOut)
			return;

		switch( m_EffectBuff.m_ExitState )
		{
		case CloseState.Destroy:
			Destroy( this.gameObject );
			break;
		case CloseState.Active:
			this.gameObject.SetActive(false);
			break;
		}
	}
}
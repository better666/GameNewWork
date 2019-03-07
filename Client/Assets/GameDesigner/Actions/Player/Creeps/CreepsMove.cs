using UnityEngine;
using System.Collections.Generic;
using GameDesigner;

public class CreepsMove : TransitionBehaviour
{
	public System.Single dis = 3F;
	public GameDesigner.PlayerSystem m_PlayerManager = null;

	public override void OnTransitionUpdate (StateManager manager , State state , State nextState , Transition transition , ref bool isEnterNextState ) // 连接事件每一帧调用
	{
		if((GameDesigner.FlowControls.Branch.IF(m_PlayerManager.Distance,Contition.Min,dis))){
			transition.isEnterNextState = true;
		}
	}
}

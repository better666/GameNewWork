using UnityEngine;
using System.Collections.Generic;
using GameDesigner;

public class Attack : ActionBehaviour
{
	/// <summary>
	/// 当键盘输入在mono行为每一帧时 ( state 所在的状态 , 此类调用此方法 , 按键枚举 , 是否进入状态 )
	/// </summary>

	override public bool OnInputUpdate ( State state , StateAction action , KeyCode key , bool isEnterState )
	{
		if( GetComponentInParent<GameDesigner.PlayerSystem>().attackTarget != null & !GetComponentInParent<GameDesigner.PlayerSystem>().isAttack & !GetComponentInParent<GameDesigner.PlayerSystem>().isDeath & PlayerMove.moveDirection == Vector3.zero ){
			return true;
		}

		return false;
	}
}
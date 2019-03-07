using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameDesigner
{
	/// <summary>
	/// 状态行为数脚本 v2017/12/6
	/// </summary>

	public class StateBehaviour : IBehaviour
	{
		/// <summary>
		/// 当状态进入时调用一次 ( 参数 stateMachine ： 状态机处理器 , 参数 currentState ： 当前状态 )
		/// </summary>

		virtual public void OnEnterState( StateManager stateManager , State currentState , State nextState )
		{
			
		}

		/// <summary>
		/// 当状态每一帧调用 ( 参数 stateMachine ： 状态机处理器 , 参数 currentState ： 当前状态 )
		/// </summary>

		virtual public void OnUpdateState( StateManager stateManager , State currentState , State nextState )
		{
			
		}

		/// <summary>
		/// 当状态退出后调用一次 ( 参数 stateMachine ： 状态机处理器 , 参数 currentState ： 当前状态 )
		/// </summary>

		virtual public void OnExitState( StateManager stateManager , State currentState , State nextState )
		{
			
		}
	}
}
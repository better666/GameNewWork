using UnityEngine;
using System.Collections.Generic;

namespace GameDesigner
{
	/// <summary>
	/// 连接行为--用户可以继承此类添加组件 2017年12月6日(星期三)
	/// </summary>

	public class TransitionBehaviour : IBehaviour
	{
		/// <summary>
		/// 当状态每一帧调用这个方法 ( 参数CurrState ： 当前状态 , 参数 NextState ： 下一个状态 , 参数 transition ： 状态链接 , 参数 isEnterNextState ： 是否进入下一个状态 )
		/// </summary>

		public virtual void OnTransitionUpdate( StateManager manager , State state , State nextState , Transition transition , ref bool isEnterNextState ) 
		{
			
		}
	}
}
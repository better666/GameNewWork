using UnityEngine;
namespace GameDesigner
{
	/// <summary>
	/// 动作行为--用户添加的组件 v2017/12/6
	/// </summary>
	public class ActionBehaviour : IBehaviour
	{
		/// <summary>
		/// 当键盘输入在mono行为每一帧时 ( state 所在的状态 , 此类调用此方法 , 按键枚举 , 是否进入状态 )
		/// </summary>
		public virtual bool OnInputUpdate ( State state , StateAction action , KeyCode key , bool isEnterState )
		{
			return isEnterState;
		}

		/// <summary>
		/// 当状态进入时 ( action 状态动作管理(也是此类发送到此方法的入口点) )
		/// </summary>
		public virtual void OnStateEnter ( State state , StateAction action ) { }

		/// <summary>
		/// 当状态每一帧调用 ( action 状态动作管理(也是此类发送到此方法的入口点) )
		/// </summary>
		public virtual void OnStateUpdate ( State state , StateAction action ) { }

		/// <summary>
		/// 当状态结束调用 ( action 状态动作管理(也是此类发送到此方法的入口点) )
		/// </summary>
		public virtual void OnStateExit ( State state , StateAction action ) { }

		/// <summary>
		/// 当动画事件进入 ( action 状态动作管理(也是此类发送到此方法的入口点) , 动画事件时间 )
		/// </summary>
		public virtual void OnAnimationEventEnter( State state , StateAction action , float animEventTime ) { }

		/// <summary>
		/// 当实例化技能物体时进入 ( action 状态动作管理(也是此类发送到此方法的入口点) , 子弹物体 )
		/// </summary>
		public virtual void OnInstantiateSpwanEnter( State state , StateAction action , GameObject spwan ) { }
	}
}
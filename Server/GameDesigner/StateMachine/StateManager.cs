using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameDesigner
{
    /// <summary>
    /// 状态执行管理类
    /// V2017.12.6
    /// </summary>
    public sealed class StateManager : MonoBehaviour
	{
		public StateMachine stateMachine = null;
		public new Animation animation = null;
        public Animator animator = null;
        public List<string> clipNames = new List<string>();

		void Awake()
		{
			animation = GetComponent<Animation> ();
            animator = GetComponent<Animator>();
            if ( !stateMachine )
				enabled = false;
			if(stateMachine.GetComponentInParent<StateManager>()==null){//当使用本地公用状态机时
				var sm = Instantiate(stateMachine);
				sm.name = stateMachine.name;
				sm.transform.SetParent(transform);
				sm.transform.localPosition = Vector3.zero;
				stateMachine = sm;
			}
		}

		void Start()
		{
			if (stateMachine != null) {
				stateMachine.defaultState.OnEnterState ();
			}
		}

        private void FixedUpdate()
        {
            if (stateMachine.currState == null)
                return;
            OnState(stateMachine.currState);
        }

		/// <summary>
		/// 处理状态各种行为与事件方法
		/// </summary>
		public void OnState ( State state )
		{
			if( state.actionSystem ){
				try{ state.OnUpdateState ( this , state ); } catch ( Exception e ) { Debug.LogWarning( "系统动作出现异常,为了不影响性能,系统即将自动关闭系统动作 == > " + e ); state.actionSystem = false; }
			}
			for( int i = 0 ; i < state.behaviours.Count ; ++i ) {//用户自定义脚本行为
				if( state.behaviours [i].Active ){
					state.behaviours [i].OnUpdateState( this , state , null );//发送给信息，如果重写就会被调用
				}
			}
			for( int i = 0 ; i < state.transitions.Count ; ++i ) {
				OnTransition ( state.transitions[i] );
			}
		}

		/// <summary>
		/// 处理连接行为树方法
		/// </summary>
		public void OnTransition ( Transition transition )
		{
			for( int i = 0 ; i < transition.behaviours.Count ; ++i ){
				if(transition.behaviours[i].Active){
					transition.behaviours [i].OnTransitionUpdate ( this , stateMachine.currState , transition.nextState , transition , ref transition.isEnterNextState );//发送给信息，如果重写就会被调用
				}
			}
            if (transition.model == TransitionModel.ExitTime) {
                transition.time += Time.deltaTime;
                if (transition.time > transition.exitTime)
                {
                    transition.isEnterNextState = true;
                }
            }
            if (transition.isEnterNextState) {
                OnEnterNextState(stateMachine.currState, transition.nextState);
                transition.time = 0;
                transition.isEnterNextState = false;
            }
        }

		/// <summary>
		/// 当进入下一个状态 ( 当前状态 , 即将进入的下一个状态 , 当前状态层 )
		/// 解释 ： 当状态进入一个新的状态时 调用状态(exitStates) 的 所有行为(StateBehaviour)动作 的 OnExitState(当状态退出)方法
		/// </summary>
		public void OnEnterNextState ( State currState , State enterState )
		{
			currState.lengqueTime.time = 0;
			foreach( StateBehaviour behaviour in currState.behaviours )//先退出当前的所有行为状态OnExitState的方法
			{
				if( behaviour.Active ){
					behaviour.OnExitState ( this , currState , enterState );
				}
			}
			foreach( StateBehaviour behaviour in enterState.behaviours )//最后进入新的状态前调用这个新状态的所有行为类的OnEnterState方法
			{
				if( behaviour.Active ){
					behaviour.OnEnterState ( this , enterState, null );
				}
			}
            if (currState.actionSystem) {
                currState.OnExitState();
            }
            if (enterState.actionSystem) {
				enterState.OnEnterState ();
			}
			stateMachine.stateIndex = enterState.stateID;
		}

        /// <summary>
		/// 当进入下一个状态 ( 状态索引 )
		/// </summary>
		public void OnEnterNextState(int nextStateIndex)
        {
            stateMachine.currState.lengqueTime.time = 0;
            foreach (StateBehaviour behaviour in stateMachine.currState.behaviours)//先退出当前的所有行为状态OnExitState的方法
            {
                if (behaviour.Active) {
                    behaviour.OnExitState(this, stateMachine.currState, stateMachine.states[nextStateIndex]);
                }
            }
            foreach (StateBehaviour behaviour in stateMachine.states[nextStateIndex].behaviours)//最后进入新的状态前调用这个新状态的所有行为类的OnEnterState方法
            {
                if (behaviour.Active) {
                    behaviour.OnEnterState(this, stateMachine.states[nextStateIndex], null);
                }
            }
            if (stateMachine.currState.actionSystem) {
                stateMachine.currState.OnExitState();
            }
            if (stateMachine.states[nextStateIndex].actionSystem) {
                stateMachine.states[nextStateIndex].OnEnterState();
            }
            stateMachine.stateIndex = nextStateIndex;
        }
    }
}
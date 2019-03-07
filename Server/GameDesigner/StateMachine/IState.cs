using UnityEngine;
using System.Collections;

namespace GameDesigner
{
	/// <summary>
	/// 状态机基类
	/// </summary>
	public abstract class IState : MonoBehaviour
	{
		public bool findBehaviours = false;
		[HideInInspector]
		public bool makeTransition = false;
		[HideInInspector]
		public bool makeGetValueTransition = false;
		[HideInInspector]
		public bool makeRuntimeTransition = false;
		[HideInInspector]
		public bool makeOutRuntimeTransition = false;
		[HideInInspector]
		public string CreateScriptName = "NewStateBehaviour";
		[HideInInspector]
		public string scriptPath = "/GameDesigner/Actions/StateBehaviours";
		[HideInInspector]
		public bool foldout = true;
		[HideInInspector]
		public int behaviourMenuIndex = 0;
		public Rect rect = new Rect (10, 10, 150, 30);

		[SerializeField][HideInInspector]
		private StateMachine _stateMachine = null;
		public StateMachine stateMachine{
			get{
				if(_stateMachine == null){
					_stateMachine = transform.GetComponentInParent<StateMachine>();
				}
				return _stateMachine;
			}
			set{ _stateMachine = value; }
		}

		[SerializeField][HideInInspector]
		private StateManager _stateManager = null;
		public StateManager stateManager{
			get{
				if(_stateManager == null){
					_stateManager = transform.GetComponentInParent<StateManager>();
				}
				return _stateManager;
			}
			set{ _stateManager = value; }
		}
	}
}
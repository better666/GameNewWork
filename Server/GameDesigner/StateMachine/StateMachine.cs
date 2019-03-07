using UnityEngine;
using System.Collections.Generic;
using System.IO;

namespace GameDesigner
{
	public enum SelectionObjModel
	{
		Null,
		SelectionStateManager,
		SelectionStateMachine,
		SelectionStateObject,
	}

	public enum InitializeBlueprintEditorMode
	{
		///不进行任何设置
		NoSetting,
		///第一次打开蓝图时初始化设置
		FirstOpenBlueprintUpdate,
		///每当你打开蓝图时都会被初始化设置
		OpenBlueprintUpdate,
	}

	/// <summary>
	/// 状态机 v2017/12/6
	/// </summary>

	public class StateMachine : MonoBehaviour
	{
		public int defaulStateID = 0;
		public int stateIndex = 0;
		public List<State> states = new List<State> ();/// 选中的状态,可以多选
		public List<State> selectStates = new List<State> ();/// 选中的连接线,可以多选
		public List<Transition> selectTransitions = new List<Transition>();/// 当前选中的状态或程序运行到此状态时存储到这个变量中
        public AnimationMode animMode = AnimationMode.Animation;

        public State defaultState{
			get{ 
				if(defaulStateID < states.Count)
					return states [defaulStateID]; 
				return null;
			}
			set{ defaulStateID = value.stateID; }
		}

		public State currState{
			get{
				return states [stateIndex];
			}
		}

		public State selectState{
			get{
				if(selectStates.Count > 0)
					return selectStates[0];
				return null;
			}
			set{
				if(!selectStates.Contains(value)&value!=null){
					selectStates.Add (value);
				}
			}
		}

		public Transition selectTransition{
			get{
				if(selectTransitions.Count > 0)
					return selectTransitions[0];
				return null;
			}
			set{
				if(selectTransitions.Count > 0)
					selectTransitions[0] = value;
				else
					selectTransitions.Add (value);
			}
		}

		[SerializeField]
		private StateManager _stateManager = null;
		public StateManager stateManager{
			get{
				if( _stateManager == null ){
					_stateManager = transform.GetComponentInParent<StateManager>();
				}
				return _stateManager;
			}
			set{ _stateManager = value; }
		}

		/// 创建状态机实例
		static public StateMachine CreateStateMachineInstance ( string name = "我的状态机" )
		{
			StateMachine stateMachine = new GameObject( name ).AddComponent<StateMachine>();
			stateMachine.name = name;
			return stateMachine;
		}
	}
}
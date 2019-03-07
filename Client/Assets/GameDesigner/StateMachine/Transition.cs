using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace GameDesigner
{
	/// <summary>
	/// 状态连接组件 2017年12月6日
	/// </summary>

	public sealed class Transition : IState
	{
		//public int DstState = 0;
		public State currState = null;
		public State nextState = null;
		public TransitionModel model = TransitionModel.ScriptControl;
		public float time = 0;
		public float exitTime = 1;
		public List<TransitionBehaviour> behaviours = new List<TransitionBehaviour> ();
		public bool isEnterNextState = false;

		private Transition() 
		{
			
		}

		static public Transition CreateTransitionInstance( State state , State nextState , string transitionName = "新的连接" )
		{
			Transition t = new GameObject( transitionName ).AddComponent<Transition> ();
			t.name = transitionName;
			//t.DstState = nextState.stateID;
			t.currState = state;
			t.nextState = nextState;
			state.transitions.Add( t );
			t.transform.SetParent ( state.transform );
			t.transform.hideFlags = HideFlags.None;
			return t;
		}
	}
}
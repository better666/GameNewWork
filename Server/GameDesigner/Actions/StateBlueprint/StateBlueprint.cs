using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameDesigner;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class StateBlueprint : StateBehaviour
{
	[SerializeField]
	[HideInInspector]
	private Blueprint _designer = null;
	public Blueprint designer{
		get{
			if(_designer == null){
				_designer = new GameObject(name).AddComponent<Blueprint>();
                _designer.transform.SetParent(transform);
            }
			return _designer;
		}
		set{ _designer = value; }
	}

	[HideInInspector]
	public State currentState = null;
	[HideInInspector]
	public State nextState = null;

	[SerializeField][HideInInspector]
	private BlueprintNode _onEnterState = null;
	public BlueprintNode onEnterState{
		get{
			if( _onEnterState == null ){
				_onEnterState = BlueprintNode.CreateFunctionBody( designer , typeof(StateBehaviour) , "OnEnterState" , "StateBehaviour" , "当状态进入时调用一次" );
			}
			return _onEnterState;
		}
	}

	[SerializeField][HideInInspector]
	private BlueprintNode _onUpdateState = null;
	public BlueprintNode onUpdateState{
		get{
			if( _onUpdateState == null ){
				_onUpdateState = BlueprintNode.CreateFunctionBody( designer , typeof(StateBehaviour) , "OnUpdateState" , "StateBehaviour" , "当状态每一帧调用" );
			}
			return _onUpdateState;
		}
	}

	[SerializeField][HideInInspector]
	private BlueprintNode _onExitState = null;
	public BlueprintNode onExitState{
		get{
			if( _onExitState == null ){
				_onExitState = BlueprintNode.CreateFunctionBody( designer , typeof(StateBehaviour) , "OnExitState" , "StateBehaviour" , "当状态退出后调用一次" );
			}
			return _onExitState;
		}
	}

	public void CheckDesigner()
	{
		if(onEnterState&onUpdateState&onExitState) { }
	}

	/// <summary>
	/// 设置当进入,每一帧,退出状态的变量
	/// </summary>

	public void SetOnStateVar( State _currentState , State _nextState )
	{
		currentState = _currentState;
		nextState = _nextState;
	}

	/// <summary>
	/// 当状态进入时调用一次 ( 参数 stateMachine ： 状态机处理器 , 参数 currentState ： 当前状态 )
	/// </summary>

	override public void OnEnterState( StateManager stateManager , State currentState , State nextState )
	{
		if(onEnterState.runtime){
			onEnterState.runtime.Invoke();
		}
	}

	/// <summary>
	/// 当状态每一帧调用 ( 参数 stateMachine ： 状态机处理器 , 参数 currentState ： 当前状态 )
	/// </summary>

	override public void OnUpdateState( StateManager stateManager , State currentState , State nextState )
	{
		if(onUpdateState.runtime){
			onUpdateState.runtime.Invoke();
		}
	}

	/// <summary>
	/// 当状态退出后调用一次 ( 参数 stateMachine ： 状态机处理器 , 参数 currentState ： 当前状态 )
	/// </summary>

	override public void OnExitState( StateManager stateManager , State currentState , State nextState )
	{
		if(onExitState.runtime){
			onExitState.runtime.Invoke();
		}
	}

	/// <summary>
	/// 编辑器扩展 (重要提示!你想扩展编辑器就得返回真,否则显示默认编辑器监视面板) ( 参数state : 这个行为是被这个状态管理 )
	/// </summary>

	#if UNITY_EDITOR
	override public bool OnInspectorGUI( State state )
	{
		Rect r0 = EditorGUILayout.GetControlRect ();
		Rect rr0 = new Rect ( new Vector2 (r0.x + (r0.size.x / 4f), r0.y) , new Vector2( r0.size.x / 2f , 20 ) );
		if( GUI.Button (rr0, "打开蓝图编辑器" )){
			CheckDesigner();
			try{
				System.Type type = System.Reflection.Assembly.Load( "Editor" ).GetType( "GameDesigner.BlueprintEditor" );
				type.GetMethod ("Init").Invoke (null, null);
				type.GetField ("designer").SetValue (type.GetProperty ("instance").GetValue (null, null), designer);
			}catch{
				try{
					System.Type type1 = System.Reflection.Assembly.LoadFile(Application.dataPath.Replace("Assets","Library/ScriptAssemblies/Assembly-CSharp-Editor.dll")).GetType( "GameDesigner.BlueprintEditor" );
					if (type1 != null) {
						type1.GetMethod ("Init").Invoke (null, null);
						type1.GetField ("designer").SetValue (type1.GetProperty ("instance").GetValue (null, null), designer);
					}
				}catch{}
			}
		}
		return true; //返回假: 绘制默认监视面板 | 返回真: 绘制扩展自定义监视面板
	}
	#endif
}
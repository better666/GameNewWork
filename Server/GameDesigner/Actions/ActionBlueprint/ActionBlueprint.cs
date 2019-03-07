using UnityEngine;
using System.Collections.Generic;
using GameDesigner;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ActionBlueprint : ActionBehaviour
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

	[SerializeField][HideInInspector]
	private BlueprintNode _onInputKeyMonoUpdate = null;
	public BlueprintNode onInputKeyMonoUpdate{
		get{
			if( _onInputKeyMonoUpdate == null ){
				_onInputKeyMonoUpdate = BlueprintNode.CreateFunctionBody( designer , typeof(ActionBehaviour) , "OnInputKeyMonoUpdate" , "ActionBehaviour" , "当键盘输入在mono行为每一帧时" );
			}
			return _onInputKeyMonoUpdate;
		}
	}

	[SerializeField][HideInInspector]
	private BlueprintNode _onEnterState = null;
	public BlueprintNode onEnterState{
		get{
			if( _onEnterState == null ){
				_onEnterState = BlueprintNode.CreateFunctionBody( designer , typeof(ActionBehaviour) , "OnStateEnter" , "ActionBehaviour" , "当状态进入时调用一次" );
			}
			return _onEnterState;
		}
	}

	[SerializeField][HideInInspector]
	private BlueprintNode _onUpdateState = null;
	public BlueprintNode onUpdateState{
		get{
			if( _onUpdateState == null ){
				_onUpdateState = BlueprintNode.CreateFunctionBody( designer , typeof(ActionBehaviour) , "OnStateUpdate" , "ActionBehaviour" , "当状态每一帧调用" );
			}
			return _onUpdateState;
		}
	}

	[SerializeField][HideInInspector]
	private BlueprintNode _onExitState = null;
	public BlueprintNode onExitState{
		get{
			if( _onExitState == null ){
				_onExitState = BlueprintNode.CreateFunctionBody( designer , typeof(ActionBehaviour) , "OnStateExit" , "ActionBehaviour" , "当状态退出后调用一次" );
			}
			return _onExitState;
		}
	}

	[SerializeField][HideInInspector]
	private BlueprintNode _onAnimationEventEnter = null;
	public BlueprintNode onAnimationEventEnter{
		get{
			if( _onAnimationEventEnter == null ){
				_onAnimationEventEnter = BlueprintNode.CreateFunctionBody( designer , typeof(ActionBehaviour) , "OnAnimationEventEnter" , "ActionBehaviour" , "当动画事件进入" );
			}
			return _onAnimationEventEnter;
		}
	}

	[SerializeField][HideInInspector]
	private BlueprintNode _onInstantiateSpwanEnter = null;
	public BlueprintNode onInstantiateSpwanEnter{
		get{
			if( _onInstantiateSpwanEnter == null ){
				_onInstantiateSpwanEnter = BlueprintNode.CreateFunctionBody( designer , typeof(ActionBehaviour) , "OnInstantiateSpwanEnter" , "ActionBehaviour" , "当实例化技能物体时进入" );
			}
			return _onInstantiateSpwanEnter;
		}
	}

	public void CheckDesigner()
	{
		if(onInputKeyMonoUpdate&onEnterState&onUpdateState&onExitState&onAnimationEventEnter&onInstantiateSpwanEnter){ }
	}

	/// <summary>
	/// 当键盘输入在mono行为每一帧时 ( state 所在的状态 , 此类调用此方法 , 按键枚举 , 是否进入状态 )
	/// </summary>

	override public bool OnInputUpdate ( State state , StateAction action , KeyCode key , bool isEnterState )
	{
		return isEnterState;
	}

	/// <summary>
	/// 当状态进入时 ( action 状态动作管理(也是此类发送到此方法的入口点) )
	/// </summary>

	override public void OnStateEnter ( State state , StateAction action )
	{
		if( onEnterState.runtime )
			onEnterState.runtime.Invoke();
	}

	/// <summary>
	/// 当状态每一帧调用 ( action 状态动作管理(也是此类发送到此方法的入口点) )
	/// </summary>

	override public void OnStateUpdate ( State state , StateAction action )
	{
		if( onUpdateState.runtime )
			onUpdateState.runtime.Invoke();
	}

	/// <summary>
	/// 当状态结束调用 ( action 状态动作管理(也是此类发送到此方法的入口点) )
	/// </summary>

	override public void OnStateExit ( State state , StateAction action )
	{
		if( onExitState.runtime )
			onExitState.runtime.Invoke();
	}

	/// <summary>
	/// 当动画事件进入 ( action 状态动作管理(也是此类发送到此方法的入口点) , 动画事件时间 )
	/// </summary>

	override public void OnAnimationEventEnter( State state , StateAction action , float animEventTime )
	{
		if( onAnimationEventEnter.runtime )
			onAnimationEventEnter.runtime.Invoke();
	}

	/// <summary>
	/// 当实例化技能物体时进入 ( action 状态动作管理(也是此类发送到此方法的入口点) , 子弹物体 )
	/// </summary>

	override public void OnInstantiateSpwanEnter( State state , StateAction action , GameObject _spwan )
	{
		if( onInstantiateSpwanEnter.runtime )
			onInstantiateSpwanEnter.runtime.Invoke();
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
            try {
                System.Type type1 = System.Reflection.Assembly.LoadFile(Application.dataPath.Replace("Assets", "Library/ScriptAssemblies/Assembly-CSharp-Editor.dll")).GetType("GameDesigner.BlueprintEditor");
                if (type1 != null) {
                    type1.GetMethod("Init").Invoke(null, null);
                    type1.GetField("designer").SetValue(type1.GetProperty("instance").GetValue(null, null), designer);
                }
            } catch { }
        }
		return true; //返回假: 绘制默认监视面板 | 返回真: 绘制扩展自定义监视面板
	}
	#endif
}
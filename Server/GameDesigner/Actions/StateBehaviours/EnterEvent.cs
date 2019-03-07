using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameDesigner;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class EnterEvent : StateBehaviour
{
	[Header("按键模式")]
	public KeyMode keyMode = KeyMode.KeyTrue;
	[Header("按键值对")]
	public KeyCode key = KeyCode.Mouse0;
	[Header("限制进入状态ID")]
	public int[] interruptActionIndexs = new int[1];

	void Update()
	{
		switch(keyMode){
		case KeyMode.KeyTrue:
			if (Input.GetKey (key) & InterruptAction) {
				stateManager.OnEnterNextState (stateMachine.currState, state);
			}
			break;
		case KeyMode.Down:
			if (Input.GetKeyDown (key) & InterruptAction) {
				stateManager.OnEnterNextState (stateMachine.currState, state);
			}
			break;
		}
	}

	private bool InterruptAction{
		get{
			foreach(var id in interruptActionIndexs){
				if (stateMachine.currState.stateID == id ) {
					return false;
				}
			}
			return true;
		}
	}

	/// <summary>
	/// 编辑器扩展 (重要提示!你想扩展编辑器就得返回真,否则显示默认编辑器监视面板) ( 参数state : 这个行为是被这个状态管理 )
	/// </summary>

	#if UNITY_EDITOR
	override public bool OnInspectorGUI( State state )
	{
		for(int i = 0; i < interruptActionIndexs.Length; ++i){
			interruptActionIndexs[i] = EditorGUILayout.Popup( "限制进入状态" , interruptActionIndexs[i] , 
				System.Array.ConvertAll< State , string >( 
					state.stateMachine.states.ToArray() , 
					new System.Converter< State , string >( 
						delegate ( State s ){ 
							return "不能从 " + s.name + " 中断进入 " + state.name + " => " + s.stateID ; 
						} ) ) );
		}
		return false; //返回假: 绘制默认监视面板 | 返回真: 绘制扩展自定义监视面板
	}
	#endif
}
using UnityEngine;
using System.Collections;
using UnityEditor;

public class InsceStateMachineInStateManager : EditorWindow
{
	[MenuItem ( "Tools/ModelSetWindow/实例化状态机在状态管理器子物体内！" )]
	static void Init ( )
	{
		EditorWindow.GetWindow ( typeof ( InsceStateMachineInStateManager ) );
	}

	void OnGUI ( )
	{
		if( GUILayout.Button("实例化状态机在状态管理器子物体内！") ){
			foreach( GameObject go in Selection.gameObjects){
				foreach( GameDesigner.StateMachine s in go.GetComponentsInChildren<GameDesigner.StateMachine>()){
					DestroyImmediate( s.gameObject , true );
				}
				if( go.GetComponent<GameDesigner.StateManager>() ){
					if( go.GetComponent<GameDesigner.StateManager>().stateMachine ){
						GameDesigner.StateMachine sm = GameObject.Instantiate( go.GetComponent<GameDesigner.StateManager>().stateMachine );
						sm.transform.SetParent( go.GetComponent<GameDesigner.StateManager>().transform );
						sm.name = "StateMachine";
						sm.transform.position = Vector3.zero;
						sm.transform.rotation = Quaternion.identity;
						go.GetComponent<GameDesigner.StateManager>().stateMachine = sm;
					}
				}
			}
		}
	}
}


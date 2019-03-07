using UnityEngine;
using System.Collections.Generic;
using GameDesigner;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TransitionBlueprint : TransitionBehaviour
{
	[SerializeField]
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

	[SerializeField]
	private BlueprintNode _onTransitionUpdate = null;
	public BlueprintNode onTransitionUpdate{
		get{
			if( _onTransitionUpdate == null ){
				_onTransitionUpdate = BlueprintNode.CreateFunctionBody( designer , typeof(TransitionBehaviour) , "OnTransitionUpdate" , "TransitionBehaviour" , "连接事件每一帧调用" );
			}
			return _onTransitionUpdate;
		}
	}

	public void CheckDesigner()
	{
		if(onTransitionUpdate) { }
	}

	override public void OnTransitionUpdate( StateManager manager , State CurrState , State DstState , Transition transition , ref bool isEnterNextState ) 
	{
		if(onTransitionUpdate.runtime){
			onTransitionUpdate.runtime.Invoke();
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
using UnityEngine;
using System.Collections.Generic;
using GameDesigner;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ActionMethot : ColliderBehaviour
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

	public override void OnEnter (GameDesigner.PlayerSystem player, Transform parent)
	{
		//Player = player.attackTarget;
	}

	public override void OnUpdate (GameDesigner.PlayerSystem player, Transform parent)
	{
		//Parent = parent;
	}

	/// <summary>
	/// 编辑器扩展 (重要提示!你想扩展编辑器就得返回真,否则显示默认编辑器监视面板) ( 参数state : 这个行为是被这个状态管理 )
	/// </summary>

	#if UNITY_EDITOR
	override public bool OnInspectorGUI( State state )
	{
		if( GUILayout.Button( "打开蓝图编辑器!" ) ){
			System.Type type = System.Reflection.Assembly.Load( "Assembly-CSharp-Editor" ).GetType( "GameDesigner.BlueprintEditor" );
			type.GetMethod( "Init" ).Invoke(null,null);
			type.GetField("designer").SetValue( null , designer );
		}

		return false; //返回假: 绘制默认监视面板 | 返回真: 绘制扩展自定义监视面板
	}
	#endif
}
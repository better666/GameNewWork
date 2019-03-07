﻿#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GameDesigner
{
	public class StateMachineSetting : MonoBehaviour 
	{
		static private StateMachineSetting _instance = null;
		static public StateMachineSetting instance{
			get{
				if( _instance == null ){
					_instance = Resources.Load<StateMachineSetting>("StateMachineSetting");
					if( _instance == null ){
						PrefabUtility.CreatePrefab("Assets/GameDesigner/Editor/Resources/StateMachineSetting.prefab" , new GameObject("StateMachineSetting").AddComponent<StateMachineSetting>().gameObject );
						_instance = Resources.Load<StateMachineSetting>("StateMachineSetting");
					}
				}
				return _instance;
			}
		}

		[SerializeField]
		private GUIStyle _defaultAndSelectStyle = new GUIStyle();
		/// 默认状态和被选中状态的皮肤
		public GUIStyle defaultAndSelectStyle{
			get{
				if( _defaultAndSelectStyle.normal.background == null ){
					BlueprintSetting.SetPropertyValue( _defaultAndSelectStyle , GUI.skin.GetStyle ("flow node 5 on") );
					SetImage ( _defaultAndSelectStyle );
				}
				return _defaultAndSelectStyle;
			}
		}

		[SerializeField]
		private GUIStyle _defaultAndRuntimeIndexStyle = new GUIStyle();
		/// 默认状态和当前执行状态经过的皮肤
		public GUIStyle defaultAndRuntimeIndexStyle{
			get{
				if( _defaultAndRuntimeIndexStyle.normal.background == null ){
					BlueprintSetting.SetPropertyValue( _defaultAndRuntimeIndexStyle , GUI.skin.GetStyle ("flow node 2 on") );
					SetImage ( _defaultAndRuntimeIndexStyle );
				}
				return _defaultAndRuntimeIndexStyle;
			}
		}

		[SerializeField]
		private GUIStyle _stateInDefaultStyle = new GUIStyle();
		/// 默认状态的皮肤
		public GUIStyle stateInDefaultStyle{
			get{
				if( _stateInDefaultStyle.normal.background == null ){
					BlueprintSetting.SetPropertyValue( _stateInDefaultStyle , GUI.skin.GetStyle ("flow node 5") );
					SetImage ( _stateInDefaultStyle );
				}
				return _stateInDefaultStyle;
			}
		}

		[SerializeField]
		private GUIStyle _indexInRuntimeStyle = new GUIStyle();
		/// 状态执行经过的每个状态所显示的皮肤
		public GUIStyle indexInRuntimeStyle{
			get{
				if( _indexInRuntimeStyle.normal.background == null ){
					BlueprintSetting.SetPropertyValue( _indexInRuntimeStyle , GUI.skin.GetStyle ("flow node 2 on") );
					SetImage ( _indexInRuntimeStyle );
				}
				return _indexInRuntimeStyle;
			}
		}

		[SerializeField]
		private GUIStyle _selectStateStyle = new GUIStyle();
		/// 当点击选择状态的皮肤
		public GUIStyle selectStateStyle{
			get{
				if( _selectStateStyle.normal.background == null ){
					BlueprintSetting.SetPropertyValue( _selectStateStyle , GUI.skin.GetStyle ("flow node 1 on") );
					SetImage ( _selectStateStyle );
				}
				return _selectStateStyle;
			}
		}

		[SerializeField]
		private GUIStyle _defaultStyle = new GUIStyle();
		/// 空闲状态的皮肤
		public GUIStyle defaultStyle{
			get{
				if( _defaultStyle.normal.background == null ){
					BlueprintSetting.SetPropertyValue( _defaultStyle , GUI.skin.GetStyle ("flow node 0") );
					SetImage ( _defaultStyle );
				}
				return _defaultStyle;
			}
		}

		[SerializeField]
		private string _designerName = "flow node 6";
		[SerializeField]
		private GUIStyle _designerStyle = new GUIStyle();
		/// 空闲状态的皮肤
		public GUIStyle designerStyle{
			get{
				if( _designerStyle.normal.background == null ){
					BlueprintSetting.SetPropertyValue( _designerStyle , GUI.skin.GetStyle (_designerName) );
					SetImage ( _designerStyle );
				}
				return _designerStyle;
			}
		}

		[SerializeField]
		private string _selectNodesName = "flow node 6 On";
		[SerializeField]
		private GUIStyle _selectNodesStyle = new GUIStyle();
		/// 选择蓝图节点的皮肤
		public GUIStyle selectNodesStyle{
			get{
				if( _selectNodesStyle.normal.background == null ){
					BlueprintSetting.SetPropertyValue( _selectNodesStyle , GUI.skin.GetStyle (_selectNodesName) );
					SetImage ( _selectNodesStyle );
				}
				return _selectNodesStyle;
			}
		}

		[SerializeField]
		private GUIStyle _functionalBlockNodesStyle = new GUIStyle();
		/// 选择蓝图节点的皮肤
		public GUIStyle functionalBlockNodesStyle{
			get{
				if( _functionalBlockNodesStyle.normal.background == null ){
					BlueprintSetting.SetPropertyValue( _functionalBlockNodesStyle , GUI.skin.GetStyle ("flow node 0 On") );
					SetImage ( _functionalBlockNodesStyle );
				}
				return _functionalBlockNodesStyle;
			}
		}

		public Color parameterNameColor = Color.white;

		public Rect getRect = new Rect( -22,0,20,20 );
		public Rect setRect = new Rect( 170,0,20,20 );
		public Vector2 offset = new Vector2(-10,-45);
		public Rect mainRect = new Rect( -22,-30,12,12 );
		public Rect runRect = new Rect( 170,-30,12,12 );
		public float topHeight = 30;

		static public void SetImage( GUIStyle style )
		{
			style.normal.textColor = Color.white;
			style.font = Resources.Load<Font>( "1234567890" );
		}
	}
}
#endif
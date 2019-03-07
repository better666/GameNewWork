using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;
using System.Linq;
using Object = UnityEngine.Object;

namespace GameDesigner
{
    [CustomEditor(typeof(StateManager))]
	public class StateManagerEditor : Editor
	{
		StateManager stateManager = null;

		void OnEnable()
		{
			stateManager = target as StateManager;
			stateManager.animation = stateManager.GetComponent<Animation> ();
			if(stateManager.animation!=null) {
				if( stateManager.clipNames.Count != AnimationUtility.GetAnimationClips (stateManager.gameObject).Length ){
					stateManager.clipNames = new List<string>();
					foreach(AnimationClip clip in AnimationUtility.GetAnimationClips (stateManager.gameObject)){
						stateManager.clipNames.Add(clip.name);
					}
				}
			}
            stateManager.animator = stateManager.GetComponent<Animator>();
            if (stateManager.animator != null) {
                if (stateManager.clipNames.Count != stateManager.animator.runtimeAnimatorController.animationClips.Length) {
                    stateManager.clipNames = new List<string>();
                    foreach (AnimationClip clip in stateManager.animator.runtimeAnimatorController.animationClips) {
                        stateManager.clipNames.Add(clip.name);
                    }
                }
            }
            StateMachineWindow.stateMachine = stateManager.stateMachine;
        }

		public override void OnInspectorGUI ()
		{
			stateManager.stateMachine = (StateMachine)EditorGUILayout.ObjectField ( "状态机控制器" , stateManager.stateMachine , typeof(StateMachine) , true );
            if (GUILayout.Button("打开游戏设计师编辑器", GUI.skin.GetStyle("LargeButtonMid"), GUILayout.ExpandWidth(true))) {
                StateMachineWindow.Init();
            }
			EditorGUILayout.Space ();
			if(stateManager.stateMachine==null)
				return;
			if( stateManager.stateMachine.selectState != null ){
				DrawState(stateManager.stateMachine.selectState , stateManager);
				EditorGUILayout.Space ();
				for( int i = 0 ; i < stateManager.stateMachine.selectState.transitions.Count ; ++i ){
					DrawTransition ( stateManager.stateMachine.selectState.transitions[i] );
				}
			}else if( stateManager.stateMachine.selectTransition != null ){
				DrawTransition ( stateManager.stateMachine.selectTransition );
			}
			EditorGUILayout.Space ();
			Repaint ();
		}

		/// <summary>
		/// 绘制状态监视面板属性
		/// </summary>
		public static void DrawState( State s , StateManager man = null )
		{
			SerializedObject serializedObject = new SerializedObject (s);
			serializedObject.Update();
			GUILayout.Button( "行为属性" , GUI.skin.GetStyle("dragtabdropwindow") );
			EditorGUILayout.BeginVertical ("ProgressBarBack");
			s.name = EditorGUILayout.TextField(new GUIContent("状态名称","name"),s.name);
			EditorGUILayout.IntField ( new GUIContent("状态ID","stateID") , s.stateID );
			EditorGUILayout.PropertyField(serializedObject.FindProperty("actionSystem"),new GUIContent("系统动作","actionSystem  专为玩家角色AI其怪物AI所设计的一套AI系统！"),true);
			if( s.actionSystem )
			{
                man.stateMachine.animMode = (AnimationMode)EditorGUILayout.EnumPopup(new GUIContent("动画模式", "animMode"),man.stateMachine.animMode);
                if (man.stateMachine.animMode== AnimationMode.Animation) {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("anim"), new GUIContent("动画组件", "anim"), true);
                } else {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("animator"), new GUIContent("动画组件", "animator"), true);
                }
				s.mode = (AnimMode)EditorGUILayout.Popup (new GUIContent("动画播放模式","mode"), (int)s.mode , new GUIContent[]{ new GUIContent("随机播放动画","Random"),new GUIContent("顺序播放动画","Sequence") } );
				EditorGUILayout.PropertyField(serializedObject.FindProperty("animSpeed"),new GUIContent("动画速度","animSpeed"),true);
				EditorGUILayout.PropertyField(serializedObject.FindProperty("animLoop"),new GUIContent("动画循环","animLoop"),true);
				s.onAnimExit.isExitState = EditorGUILayout.Toggle(new GUIContent("动画结束退出状态","isExitState"), s.onAnimExit.isExitState );
				if( s.onAnimExit.isExitState ){
					s.onAnimExit.DstStateID = EditorGUILayout.Popup( "进入下一个状态" , s.onAnimExit.DstStateID , Array.ConvertAll( s.transitions.ToArray() , new Converter< Transition , string >( delegate ( Transition t ){ return t.currState.name + " -> " + t.nextState.name + "   ID:" + t.nextState.stateID; } ) ) );
				}

				EditorGUILayout.PropertyField(serializedObject.FindProperty("isEnableKey"),new GUIContent("每帧检测按键","isEnableKey"),true);
				if( s.isEnableKey ){
					EditorGUILayout.PropertyField(serializedObject.FindProperty("key"),new GUIContent("按键值","key"),true);
					s.DstStateID = EditorGUILayout.Popup( "成立进入状态" , s.DstStateID , Array.ConvertAll( s.stateMachine.states.ToArray() , new Converter< State , string >( delegate ( State state ){ return s.name + " -> " + state.name  + "   ID:" + state.stateID; } ) ) );
				}

				EditorGUILayout.PropertyField(serializedObject.FindProperty("useLengqueTime"),new GUIContent("使用冷却时间","useLengqueTime"),true);
				if( s.useLengqueTime ){
					s.lengqueTime.time = EditorGUILayout.FloatField ( new GUIContent("当前时间","lengqueTime.time") , s.lengqueTime.time );
					s.lengqueTime.EndTime = EditorGUILayout.FloatField( new GUIContent("结束时间","lengqueTime.EndTime") , s.lengqueTime.EndTime );
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("lengqueInverse"), new GUIContent("冷却反向", "lengqueInverse"), true);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("lengqueTimeImage"), new GUIContent("Image组件", "lengqueTimeImage"), true);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("lengqueText"), new GUIContent("Text组件", "lengqueText"), true);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("imageFind"), new GUIContent("冷却图标查找", "imageFind"), true);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("imageName"), new GUIContent("冷却图标物体名称", "imageName"), true);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("textFind"), new GUIContent("冷却文字查找", "textFind"), true);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("textName"), new GUIContent("冷却文字物体名称", "textName"), true);
                }

				BlueprintGUILayout.BeginStyleVertical ( "系统基础动作组件" , "ProgressBarBack");
				EditorGUI.indentLevel = 1;
				Rect actRect = EditorGUILayout.GetControlRect ();
				s.foldout = EditorGUI.Foldout ( new Rect ( actRect.position , new Vector2( actRect.size.x - 120f , 15 )) , s.foldout , "动作树" , true );

				if (GUI.Button (new Rect ( new Vector2( actRect.size.x - 40f , actRect.position.y ) , new Vector2( 60 , 16 )), "添加节点" )) {
					s.actions.Add( new StateAction() );
				}
				if (GUI.Button (new Rect ( new Vector2( actRect.size.x - 100, actRect.position.y ) , new Vector2( 60 , 16 )), "移除节点" )) {
					if (s.actions.Count > 1) {
						foreach( ActionBehaviour behaviour in s.actions[s.actions.Count - 1].behaviours ){
							DestroyImmediate ( behaviour.gameObject , true );
						}
						s.actions.RemoveAt (s.actions.Count - 1);
					}
				}

				if( s.foldout )
				{
					EditorGUI.indentLevel = 2;
					for( int a = 0 ; a < s.actions.Count ; ++a ){
						StateAction act = s.actions[a];
						act.foldout = EditorGUILayout.Foldout ( act.foldout , new GUIContent("动作节点 [" + a + "]","actions[" + a + "]") , true );
						if( act.foldout ){
							EditorGUI.indentLevel = 3;
							try {
								act.clipIndex = EditorGUILayout.Popup(new GUIContent("动画剪辑","clipName & clipIndex in action (clipIndex(剪辑索引)是action(StateAction类型)的变量)") , act.clipIndex, Array.ConvertAll(s.stateMachine.stateManager.clipNames.ToArray(),new Converter<string,GUIContent>(delegate(string input) {
									return new GUIContent(input);
								})) );
								act.clipName = s.stateManager.clipNames [act.clipIndex];
							} catch { }

							s.actions[a].isPlayAudio = EditorGUILayout.Toggle( new GUIContent("播放音效","this isPlayAudio in action or actions (Type is StateAction)") , s.actions[a].isPlayAudio );
							if( s.actions[a].isPlayAudio ){
								act.audioModel = (AudioModel)EditorGUILayout.Popup(new GUIContent("播放音效模式","audioModel") , (int)act.audioModel , new GUIContent[]{ new GUIContent("进入播放音效","EnterPlayAudio") , new GUIContent("事件播放音效","AnimEventPlayAudio") , new GUIContent("退出播放音效","ExitPlayAudio") } );
								EditorGUILayout.PropertyField(serializedObject.FindProperty("actions").GetArrayElementAtIndex(a).FindPropertyRelative("audioClips"),new GUIContent("技能随机音效","actions"),true);
							}
							act.animTime = EditorGUILayout.FloatField ( new GUIContent("动画时间","animTime") , act.animTime );
							act.animTimeMax = EditorGUILayout.FloatField ( new GUIContent("动画长度","animTimeMax") , act.animTimeMax );
							act.animEventTime = EditorGUILayout.FloatField ( new GUIContent("动画事件","animEventTime") , act.animEventTime );

							act.effectSpwan = EditorGUILayout.ObjectField ( new GUIContent("技能物体","effectSpwan") , act.effectSpwan , typeof(Object) , true );
							act.activeModel = (ActiveModel)EditorGUILayout.Popup ( new GUIContent("技能生成模式","activeModel") , (int)act.activeModel , new GUIContent[]{ new GUIContent("实例化技能物体","Instantiate") , new GUIContent("对象池管理技能物体","SetActive") } );
							if( act.activeModel == ActiveModel.SetActive ){
								EditorGUILayout.PropertyField(serializedObject.FindProperty("actions").GetArrayElementAtIndex(a).FindPropertyRelative("activeObjs"),new GUIContent("技能对象池","activeObjs"),true);
							}
							act.spwanmode = (SpwanModel)EditorGUILayout.Popup ( new GUIContent("设置位置","spwanmode") , (int)act.spwanmode , new GUIContent[]{ new GUIContent("自身位置","TransformPoint") , new GUIContent("挂载父对象","SetParent") , new GUIContent("设置在对象位置","localPosition") } );
							if( act.spwanmode != SpwanModel.localPosition ){
								act.parent = EditorGUILayout.ObjectField(new GUIContent("父对象","parent") , act.parent , typeof(Transform) , true ) as Transform;
							}
							act.effectPostion = EditorGUILayout.Vector3Field(new GUIContent("技能位置","effectPostion") , act.effectPostion );
							act.spwanTime = EditorGUILayout.FloatField(new GUIContent("技能存活时间","spwanTime") , act.spwanTime );

							for (int i = 0; i < act.behaviours.Count; ++i) {
								if (act.behaviours[i] == null) {
									act.behaviours.RemoveAt (i);
									continue;
								}
								EditorGUILayout.BeginHorizontal ();
								Rect rect = EditorGUILayout.GetControlRect ();
								act.behaviours[i].show = EditorGUI.Foldout( new Rect( rect.x , rect.y , 50 , rect.height ) , act.behaviours[i].show , GUIContent.none );
								act.behaviours[i].Active = EditorGUI.ToggleLeft( new Rect( rect.x + 5 , rect.y , 70 , rect.height ) , GUIContent.none , act.behaviours[i].Active );
								EditorGUI.LabelField ( new Rect( rect.x + 20 , rect.y , rect.width - 15 , rect.height ) , act.behaviours[i].GetType().Name , GUI.skin.GetStyle("BoldLabel") );
								if ( GUI.Button ( new Rect( rect.x + rect.width - 15 , rect.y , rect.width , rect.height ) , GUIContent.none , GUI.skin.GetStyle( "ToggleMixed" ) ) ) {
									act.behaviours[act.behaviourMenuIndex].OnDestroyComponent();
									GameObject.DestroyImmediate ( act.behaviours[i] , true );
									act.behaviours.RemoveAt (i);
									continue;
								}
								if( rect.Contains(Event.current.mousePosition) & Event.current.button == 1 )
								{
									GenericMenu menu = new GenericMenu();
									menu.AddItem( new GUIContent("删除组件") , false , delegate() {
										act.behaviours[act.behaviourMenuIndex].OnDestroyComponent();
										GameObject.DestroyImmediate ( act.behaviours[act.behaviourMenuIndex] , true );
										act.behaviours.RemoveAt (act.behaviourMenuIndex);
										return;
									} );
									menu.AddItem( new GUIContent("复制组件") , false , delegate() {
										StateSystem.CopyComponent = act.behaviours[act.behaviourMenuIndex];
									} );
									menu.AddItem( new GUIContent("粘贴新的组件") , StateSystem.CopyComponent ? true : false , delegate() {
                                        if (StateSystem.CopyComponent)
                                        {
                                            if (StateSystem.CopyComponent.GetType().BaseType == typeof(ActionBehaviour))
                                            {
                                                ActionBehaviour ab = (ActionBehaviour)s.gameObject.AddComponent(StateSystem.CopyComponent.GetType());
                                                act.behaviours.Add(ab);
                                                SystemType.SetFieldValue(ab, StateSystem.CopyComponent);
                                            }
                                        }
                                    } );
									menu.AddItem( new GUIContent("粘贴组件值") , StateSystem.CopyComponent ? true : false , delegate() {
										if(StateSystem.CopyComponent)
										{
											if(StateSystem.CopyComponent.GetType().FullName == act.behaviours[act.behaviourMenuIndex].GetType().FullName )
											{
												SystemType.SetFieldValue( act.behaviours[act.behaviourMenuIndex] , StateSystem.CopyComponent);
											}
										}
									} );
									menu.ShowAsContext();
									act.behaviourMenuIndex = i;
								}
								EditorGUILayout.EndHorizontal ();
								if( act.behaviours[i].show ){
									EditorGUI.indentLevel = 4;
									EditorGUILayout.ObjectField ( "Script" , act.behaviours[i] , typeof(StateAction) , true );
									SerializedObject actSerializedObject = new SerializedObject(act.behaviours[i]);
									actSerializedObject.Update ();
									if( !act.behaviours [i].OnInspectorGUI ( s ) ){
										foreach( FieldInfo f in act.behaviours[i].GetType ().GetFields (BindingFlags.Public|BindingFlags.Instance) ){
											if(act.behaviours [i].GetType () != f.DeclaringType)
												continue;
											EditorGUILayout.PropertyField( actSerializedObject.FindProperty( f.Name ) , true );
										}
									}
									actSerializedObject.ApplyModifiedProperties ();
									GUILayout.Space (4);
									GUILayout.Box ("",BlueprintSetting.instance.HorSpaceStyle , GUILayout.Height(1) , GUILayout.ExpandWidth(true));
									GUILayout.Space (4);
									EditorGUI.indentLevel = 3;
								}
							}

							Rect r = EditorGUILayout.GetControlRect ();
							Rect rr = new Rect (new Vector2 (r.x + (r.size.x / 4f), r.y), new Vector2( r.size.x / 2f , 20 ) );
							if (GUI.Button (rr, "添加动作行为" )) {
								act.findBehaviours = true;
							}

							if (act.findBehaviours) {
								EditorGUILayout.Space ();
								System.Type[] types = Assembly.Load( "Assembly-CSharp" ).GetTypes ().Where (t => t.IsSubclassOf(typeof(ActionBehaviour)) ).ToArray();
								foreach( Type bn in types ){
									if (GUILayout.Button ( bn.Name ) ) {
										ActionBehaviour stb = (ActionBehaviour) s.gameObject.AddComponent ( bn );
										act.behaviours.Add ( stb );
										act.findBehaviours = false;
									}
								}

								EditorGUILayout.Space ();
								EditorGUI.indentLevel = 0;
								EditorGUILayout.LabelField ( "创建脚本路径 : " );
								act.scriptPath = EditorGUILayout.TextField ( act.scriptPath );
								Rect addRect = EditorGUILayout.GetControlRect ();
								act.CreateScriptName = EditorGUI.TextField ( new Rect ( addRect.position , new Vector2( addRect.size.x - 125f , 18 )) , act.CreateScriptName );
								if (GUI.Button (new Rect ( new Vector2( addRect.size.x - 100f , addRect.position.y ) , new Vector2( 120 , 18 )), "添加新的动作脚本" )) {
									ScriptTools.CreateScript ( Application.dataPath + act.scriptPath , act.CreateScriptName , 
										"using UnityEngine;\nusing System.Collections.Generic;\nusing GameDesigner;\n#if UNITY_EDITOR\nusing UnityEditor;\n#endif\n\n" +
										"public class " + act.CreateScriptName + " : ActionBehaviour\n{\n\t/// <summary>\n\t" +
										"/// 当键盘输入在mono行为每一帧时 ( state 所在的状态 , 此类调用此方法 , 按键枚举 , 是否进入状态 )\n\t" +
                                        "/// </summary>\n\n\toverride public bool OnInputUpdate ( State state , StateAction action , KeyCode key , bool isEnterState )\n\t{\n\t\treturn isEnterState;\n\t}\n\n\t" +
										"/// <summary>\n\t/// 当状态进入时 ( action 状态动作管理(也是此类发送到此方法的入口点) )\n\t" +
										"/// </summary>\n\n\toverride public void OnStateEnter ( State state , StateAction action )\n\t{\n\n\t}\n\n\t" +
										"/// <summary>\n\t/// 当状态每一帧调用 ( action 状态动作管理(也是此类发送到此方法的入口点) )\n\t" +
										"/// </summary>\n\n\toverride public void OnStateUpdate ( State state , StateAction action )\n\t{\n\n\t}\n\n\t" +
										"/// <summary>\n\t/// 当状态结束调用 ( action 状态动作管理(也是此类发送到此方法的入口点) )\n\t" +
										"/// </summary>\n\n\toverride public void OnStateExit ( State state , StateAction action )\n\t{\n\n\t}\n\n\t" +
										"/// <summary>\n\t/// 当动画事件进入 ( action 状态动作管理(也是此类发送到此方法的入口点) , 动画事件时间 )\n\t" +
										"/// </summary>\n\n\toverride public void OnAnimationEventEnter( State state , StateAction action , float animEventTime )\n\t{\n\n\t}\n\n\t" +
										"/// <summary>\n\t/// 当实例化技能物体时进入 ( action 状态动作管理(也是此类发送到此方法的入口点) , 子弹物体 )\n\t" +
										"/// </summary>\n\n\toverride public void OnInstantiateSpwanEnter( State state , StateAction action , GameObject spwan )\n\t{\n\n\t}\n\n\t" +
										"/// <summary>\n\t/// 编辑器扩展 (重要提示!你想扩展编辑器就得返回真,否则显示默认编辑器监视面板) ( 参数state : 这个行为是被这个状态管理 )\n\t" +
										"/// </summary>\n\n\t#if UNITY_EDITOR\n\toverride public bool OnInspectorGUI( State state )\n\t{\n\t\t" +
										"return false; //返回假: 绘制默认监视面板 | 返回真: 绘制扩展自定义监视面板\n\t}\n\t#endif\n}"
									);
									act.findBehaviours = false;
								}
								if (GUILayout.Button ( "取消操作" )) {
									act.findBehaviours = false;
								}
							}
							EditorGUILayout.Space ();
						}
						EditorGUI.indentLevel = 2;
					}
				}

				BlueprintGUILayout.EndStyleVertical();
			}

			EditorGUILayout.Space ();
			DrawBehaviours ( s );
			EditorGUILayout.Space ();

			serializedObject.ApplyModifiedProperties();
			EditorGUILayout.EndVertical ();
		}

		/// <summary>
		/// 绘制状态行为
		/// </summary>
		public static void DrawBehaviours( State s )
		{
			GUILayout.Space (10);
			GUILayout.Box ("",BlueprintSetting.instance.HorSpaceStyle , GUILayout.Height(1) , GUILayout.ExpandWidth(true));
			GUILayout.Space (5);

			for (int i = 0; i < s.behaviours.Count; ++i) {
				if (s.behaviours [i] == null) {
					s.behaviours.RemoveAt (i);
					continue;
				}
				EditorGUI.indentLevel = 1;
				EditorGUILayout.BeginHorizontal ();
				Rect rect = EditorGUILayout.GetControlRect ();
				s.behaviours[i].show = EditorGUI.Foldout( new Rect( rect.x , rect.y , 20 , rect.height ) , s.behaviours[i].show , GUIContent.none );
				s.behaviours[i].Active = EditorGUI.ToggleLeft( new Rect( rect.x + 5 , rect.y , 30 , rect.height ) , GUIContent.none , s.behaviours[i].Active );
				EditorGUI.LabelField ( new Rect( rect.x + 20 , rect.y , rect.width - 15 , rect.height ) , s.behaviours[i].GetType().Name , GUI.skin.GetStyle("BoldLabel") );
				if ( GUI.Button ( new Rect( rect.x + rect.width - 15 , rect.y , rect.width , rect.height ) , GUIContent.none , GUI.skin.GetStyle( "ToggleMixed" ) ) ) {
					s.behaviours[s.behaviourMenuIndex].OnDestroyComponent();
					GameObject.DestroyImmediate ( s.behaviours [i] , true );
					s.behaviours.RemoveAt (i);
					continue;
				}
				if( rect.Contains(Event.current.mousePosition) & Event.current.button == 1 )
				{
					GenericMenu menu = new GenericMenu();
					menu.AddItem( new GUIContent("删除组件") , false , delegate() {
						s.behaviours[s.behaviourMenuIndex].OnDestroyComponent();
						GameObject.DestroyImmediate ( s.behaviours[s.behaviourMenuIndex] , true );
						s.behaviours.RemoveAt (s.behaviourMenuIndex);
						return;
					} );
					menu.AddItem( new GUIContent("复制组件") , false , delegate() {
						StateSystem.CopyComponent = s.behaviours[s.behaviourMenuIndex];
					} );
					menu.AddItem( new GUIContent("粘贴新的组件") , StateSystem.CopyComponent ? true : false , delegate() {
						if(StateSystem.CopyComponent)
                        {
                            if (StateSystem.CopyComponent.GetType().BaseType == typeof(StateBehaviour)){
                                StateBehaviour ab = (StateBehaviour)s.gameObject.AddComponent(StateSystem.CopyComponent.GetType());
                                ab.transform.SetParent(s.transform);
                                s.behaviours.Add(ab);
                                SystemType.SetFieldValue(ab, StateSystem.CopyComponent);
                            }
						}
					} );
					menu.AddItem( new GUIContent("粘贴组件值") , StateSystem.CopyComponent ? true : false , delegate() {
						if(StateSystem.CopyComponent)
						{
							if(StateSystem.CopyComponent.GetType().FullName == s.behaviours[s.behaviourMenuIndex].GetType().FullName )
							{
								SystemType.SetFieldValue( s.behaviours[s.behaviourMenuIndex] , StateSystem.CopyComponent);
							}
						}
					} );
					menu.ShowAsContext();
					s.behaviourMenuIndex = i;
				}
				EditorGUILayout.EndHorizontal ();
				if( s.behaviours[i].show ){
					EditorGUILayout.ObjectField ( "Script" , s.behaviours[i] , typeof(StateBehaviour) , true );
					SerializedObject stateSerializedObject = new SerializedObject(s.behaviours [i]);
					stateSerializedObject.Update ();
					if (!s.behaviours[i].OnInspectorGUI ( s ) ) {
						foreach (FieldInfo f in s.behaviours[i].GetType().GetFields(BindingFlags.Public | BindingFlags.Instance )) {
							if (s.behaviours [i].GetType () == f.DeclaringType & !f.IsStatic & f.FieldType.FullName != "System.Object" )
								try{ EditorGUILayout.PropertyField (stateSerializedObject.FindProperty (f.Name), true); } catch { Debug.Log( f.Name + "  " + f.FieldType.FullName ); }
						}
					}
					stateSerializedObject.ApplyModifiedProperties ();
					GUILayout.Space (4);
					GUILayout.Box ("", BlueprintSetting.instance.HorSpaceStyle, GUILayout.Height(1) , GUILayout.ExpandWidth(true));
				}
			}
				
			Rect r = EditorGUILayout.GetControlRect ();
			Rect rr = new Rect (new Vector2 (r.x + (r.size.x / 4f), r.y), new Vector2( r.size.x / 2f , 20 ) );
			if (GUI.Button (rr, "添加状态行为" )) {
				s.findBehaviours = true;
			}

			if (s.findBehaviours) {
				System.Type[] types = Assembly.Load( "Assembly-CSharp" ).GetTypes ().Where (t => t.IsSubclassOf(typeof(StateBehaviour)) ).ToArray<Type> ();
				EditorGUILayout.Space ();
				foreach( Type bn in types ){
					if (GUILayout.Button ( bn.Name ) ) {
						StateBehaviour stb = (StateBehaviour)s.gameObject.AddComponent ( bn );
						s.behaviours.Add ( stb );
						s.findBehaviours = false;
					}
				}

				EditorGUILayout.Space ();
				EditorGUI.indentLevel = 0;
				EditorGUILayout.LabelField ( "创建脚本路径 : " );
				s.scriptPath = EditorGUILayout.TextField ( s.scriptPath );
				Rect addRect = EditorGUILayout.GetControlRect ();
				s.CreateScriptName = EditorGUI.TextField ( new Rect ( addRect.position , new Vector2( addRect.size.x - 125f , 18 )) , s.CreateScriptName );
				if (GUI.Button (new Rect ( new Vector2( addRect.size.x - 105f , addRect.position.y ) , new Vector2( 120 , 18 )), "添加新的行为脚本" )) {
					ScriptTools.CreateScript ( Application.dataPath + s.scriptPath , s.CreateScriptName , 
						"using UnityEngine;\nusing System.Collections;\nusing System.Collections.Generic;\nusing GameDesigner;\n#if UNITY_EDITOR\nusing UnityEditor;\n#endif\n\n" +
						"public class " + s.CreateScriptName + " : StateBehaviour\n{\n\t" +
						"/// <summary>\n\t/// 当状态进入时调用一次 ( 参数 stateMachine ： 状态机处理器 , 参数 currentState ： 当前状态 )\n\t" +
						"/// </summary>\n\n\toverride public void OnEnterState( StateManager stateManager , State currentState , State nextState )\n\t{\n\n\t}\n\n\t" +
						"/// <summary>\n\t/// 当状态每一帧调用 ( 参数 stateMachine ： 状态机处理器 , 参数 currentState ： 当前状态 )\n\t" +
						"/// </summary>\n\n\toverride public void OnUpdateState( StateManager stateManager , State currentState , State nextState )\n\t{\n\n\t}\n\n\t" +
						"/// <summary>\n\t/// 当状态退出后调用一次 ( 参数 stateMachine ： 状态机处理器 , 参数 currentState ： 当前状态 )\n\t" +
						"/// </summary>\n\n\toverride public void OnExitState( StateManager stateManager , State currentState , State nextState )\n\t{\n\n\t}\n\n\t" +
						"/// <summary>\n\t/// 编辑器扩展 (重要提示!你想扩展编辑器就得返回真,否则显示默认编辑器监视面板) ( 参数state : 这个行为是被这个状态管理 )\n\t" +
						"/// </summary>\n\n\t#if UNITY_EDITOR\n\toverride public bool OnInspectorGUI( State state )\n\t{\n\t\t" +
						"//gameObject.name = EditorGUILayout.TextField ( \"游戏物体名称\" , gameObject.name ); // 在这里写你的自定义监视面板\n\t\t" +
						"return false; //返回假: 绘制默认监视面板 | 返回真: 绘制扩展自定义监视面板\n\t}\n\t#endif\n}"
					);
					s.findBehaviours = false;
				}
				if (GUILayout.Button ( "取消操作" )) {
					s.findBehaviours = false;
				}
			}
		}

		/// <summary>
		/// 绘制状态连接行为
		/// </summary>
		public static void DrawTransition ( Transition tr )
		{
			EditorGUI.indentLevel = 0;
			GUIStyle style = GUI.skin.GetStyle ("dragtabdropwindow");
			style.fontStyle = FontStyle.Bold;
			style.font = Resources.Load<Font> ("Arial");
			style.normal.textColor = Color.red;
			GUILayout.Button( "连接属性                 " + tr.currState.name + " -> " + tr.nextState.name , style );
			tr.name = tr.currState.name + " -> " + tr.nextState.name;
			EditorGUILayout.BeginVertical ("ProgressBarBack");

			EditorGUILayout.Space ();

			tr.model = (TransitionModel)EditorGUILayout.Popup ( "过渡条件" , (int)tr.model , Enum.GetNames( typeof(TransitionModel) ) , GUI.skin.GetStyle("PreDropDown") );
			switch(tr.model){
			case TransitionModel.ExitTime:
				tr.time = EditorGUILayout.FloatField ( "过渡当前时间" , tr.time , GUI.skin.GetStyle("PreDropDown") );
				tr.exitTime = EditorGUILayout.FloatField ( "过渡结束时间" , tr.exitTime , GUI.skin.GetStyle("PreDropDown") );
				EditorGUILayout.HelpBox ( "提示 : 当时间到达<过渡结束时间>后自动进入下一个状态,这个是无法控制的!" , MessageType.Info );
				break;
			}

			GUILayout.Space (10);
			GUILayout.Box ("", BlueprintSetting.instance.HorSpaceStyle, GUILayout.Height(1) , GUILayout.ExpandWidth(true));
			GUILayout.Space (10);

			tr.isEnterNextState = EditorGUILayout.Toggle ( "进入下一个状态" , tr.isEnterNextState );

			GUILayout.Space (10);
			GUILayout.Box ("", BlueprintSetting.instance.HorSpaceStyle, GUILayout.Height(1) , GUILayout.ExpandWidth(true));

			for (int i = 0; i < tr.behaviours.Count; ++i) {
				if (tr.behaviours [i] == null) {
					tr.behaviours.RemoveAt (i);
					continue;
				}
				EditorGUI.indentLevel = 1;
				EditorGUILayout.BeginHorizontal ();
				Rect rect = EditorGUILayout.GetControlRect ();
				EditorGUI.LabelField ( new Rect( rect.x + 20 , rect.y , rect.width - 15 , 20 ) , tr.behaviours[i].GetType().Name , GUI.skin.GetStyle("BoldLabel") );
				tr.behaviours[i].show = EditorGUI.Foldout( new Rect( rect.x , rect.y , 20 , 20 ) , tr.behaviours[i].show , GUIContent.none , true );
				tr.behaviours[i].Active = EditorGUI.ToggleLeft( new Rect( rect.x + 5 , rect.y , 30 , 20 ) , GUIContent.none , tr.behaviours[i].Active );
				if ( GUI.Button ( new Rect( rect.x + rect.width - 15 , rect.y , rect.width , rect.height ) , GUIContent.none , GUI.skin.GetStyle( "ToggleMixed" ) ) ) {
					tr.behaviours[tr.behaviourMenuIndex].OnDestroyComponent();
					GameObject.DestroyImmediate ( tr.behaviours[i] , true );
					tr.behaviours.RemoveAt (i);
					continue;
				}
				if( rect.Contains(Event.current.mousePosition) & Event.current.button == 1 )
				{
					GenericMenu menu = new GenericMenu(); 
                    menu.AddItem( new GUIContent("删除组件") , false , delegate() {
						tr.behaviours[tr.behaviourMenuIndex].OnDestroyComponent();
						GameObject.DestroyImmediate(tr.behaviours[tr.behaviourMenuIndex],true);
						tr.behaviours.RemoveAt (tr.behaviourMenuIndex);
						return;
					} );
					menu.AddItem( new GUIContent("复制组件") , false , delegate() {
						StateSystem.CopyComponent = tr.behaviours[tr.behaviourMenuIndex];
					} );
					menu.AddItem( new GUIContent("粘贴新的组件") , StateSystem.CopyComponent ? true : false , delegate() {
						if(StateSystem.CopyComponent)
						{
                            if (StateSystem.CopyComponent.GetType().BaseType == typeof(TransitionBehaviour))
                            {
                                TransitionBehaviour ab = (TransitionBehaviour)tr.gameObject.AddComponent(StateSystem.CopyComponent.GetType());
                                tr.behaviours.Add(ab);
                                SystemType.SetFieldValue(ab, StateSystem.CopyComponent);
                            }
						}
					} );
					menu.AddItem( new GUIContent("粘贴组件值") , StateSystem.CopyComponent ? true : false , delegate() {
						if(StateSystem.CopyComponent)
						{
							if(StateSystem.CopyComponent.GetType().FullName == tr.behaviours[tr.behaviourMenuIndex].GetType().FullName )
							{
								SystemType.SetFieldValue( tr.behaviours[tr.behaviourMenuIndex] , StateSystem.CopyComponent);
							}
						}
					} );
					menu.ShowAsContext();
					tr.behaviourMenuIndex = i;
				}
				EditorGUILayout.EndHorizontal ();
				if( tr.behaviours[i].show ){
					EditorGUILayout.ObjectField ( "Script" , tr.behaviours[i] , typeof(TransitionBehaviour) , true );
					SerializedObject trSerialozedObject = new SerializedObject(tr.behaviours [i]);
					trSerialozedObject.Update ();
					if (!tr.behaviours[i].OnInspectorGUI ( tr.currState ) ) {
						foreach (FieldInfo f in tr.behaviours[i].GetType().GetFields()) {
							if (tr.behaviours [i].GetType () == f.DeclaringType & !f.IsStatic)
								EditorGUILayout.PropertyField (trSerialozedObject.FindProperty (f.Name), true);
						}
					}
					trSerialozedObject.ApplyModifiedProperties ();
					GUILayout.Space (10);
					GUILayout.Box ("", BlueprintSetting.instance.HorSpaceStyle, GUILayout.Height(1) , GUILayout.ExpandWidth(true));

				}
			}

			GUILayout.Space(5);

			Rect r = EditorGUILayout.GetControlRect ();
			Rect rr = new Rect (new Vector2 (r.x + (r.size.x / 4f), r.y), new Vector2( r.size.x / 2f , 20 ) );
			if (GUI.Button (rr, "添加连接行为" )) {
				tr.findBehaviours = true;
			}

			if (tr.findBehaviours) {
				Type[] types = Assembly.Load( "Assembly-CSharp" ).GetTypes ().Where (t => t.IsSubclassOf(typeof(TransitionBehaviour)) ).ToArray<Type> ();
				EditorGUILayout.Space ();
				foreach( Type bn in types ){
					if (GUILayout.Button ( bn.Name ) ) {
						TransitionBehaviour stb = (TransitionBehaviour)tr.gameObject.AddComponent ( bn );
						tr.behaviours.Add ( stb );
						tr.findBehaviours = false;
					}
				}

				EditorGUILayout.Space ();
				EditorGUILayout.LabelField ( "创建脚本路径 : " );
				tr.scriptPath = EditorGUILayout.TextField ( tr.scriptPath );
				Rect addRect = EditorGUILayout.GetControlRect ();
				tr.CreateScriptName = EditorGUI.TextField ( new Rect ( addRect.position , new Vector2( addRect.size.x - 125f , 18 )) , tr.CreateScriptName );
				if (GUI.Button (new Rect ( new Vector2( addRect.size.x - 105f , addRect.position.y ) , new Vector2( 120 , 18 )), "添加新的事件脚本" )) {
					ScriptTools.CreateScript ( Application.dataPath + tr.scriptPath , tr.CreateScriptName , 
						"using UnityEngine;\nusing System.Collections.Generic;\nusing GameDesigner;\n#if UNITY_EDITOR\nusing UnityEditor;\n#endif\n\n" +
						"public class " + tr.CreateScriptName + " : TransitionBehaviour\n{\n\t/// <summary>\n\t" +
						"/// 当状态每一帧调用这个方法 ( 参数CurrState ： 当前状态 , 参数 NextState ： 下一个状态 , 参数 transition ： 状态链接 , 参数 isEnterNextState ： 是否进入下一个状态 )\n\t/// </summary>\n\n\t" +
						"override public void OnTransitionUpdate( StateManager manager , State CurrState , State DstState , Transition transition , ref bool isEnterNextState ) \n\t{\n\t\tisEnterNextState = false;\n\t}\n\n\t" +
						"/// <summary>\n\t/// 编辑器扩展 (重要提示!你想扩展编辑器就得返回真,否则显示默认编辑器监视面板) ( 参数state : 这个行为是被这个状态管理 )\n\t" +
						"/// </summary>\n\n\t#if UNITY_EDITOR\n\toverride public bool OnInspectorGUI( State state )\n\t{\n\t\t" +
						"return false; //返回假: 绘制默认监视面板 | 返回真: 绘制扩展自定义监视面板\n\t}\n\t#endif\n}"
					);
					tr.findBehaviours = false;
				}
				if (GUILayout.Button ( "取消操作" )) {
					tr.findBehaviours = false;
				}
			}
			GUILayout.Space (10);
			EditorGUILayout.HelpBox ( "提示 : 自定义脚本控制条件进入下一个状态!" , MessageType.Info );
			GUILayout.Space (10);
			EditorGUILayout.EndHorizontal();
		}

        public static string ToArrtsString( Parameter[] ps  )
		{
			string str = "(";
			foreach( Parameter p in ps ){
				str += "  " + p.parameterTypeName + "  " + p.name + "  ";
			}
			return str + ")";
		}
	}

}
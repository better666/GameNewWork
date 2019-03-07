using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Reflection;
using System.Linq;
using System;
using System.IO;
using GameDesigner;

public enum BuffModel
{
	AnimExitBuff,TimeExitBuff
}

public class SkillAction : ActionBehaviour
{
	/// 释放技能可以移动
	public bool isMove = false; 
	/// 是否设置为攻击型技能
	public bool isAttack = false;
	/// 无视比这个数小的攻击型技能并中断它们的技能动画果断播放这个技能动画
	public int index = 0;
	/// 使用玩家攻击速度
	public bool useAttackSpeed = true;
	/// 时间增益
	private bool buff = false;
	/// 动画增益
	private bool animBuff = false;
	/// 增益模式
	public BuffModel buffMode = BuffModel.AnimExitBuff;
	/// 时间增益
	public float buffTime = 0;
	/// 增益结束时间
	public float buffTimeMax = 5f;
	/// 是否进入增益
	public bool isEnterBuff = true;
	/// 自身增益属性
	public AttackProperty property = new AttackProperty();

	/// 动作控制属性
	[SerializeField]
	private ColliderProperty _colliderProperty = null;
	public ColliderProperty colliderProperty{
		get{
			if( _colliderProperty == null ){
				_colliderProperty = new GameObject("ColliderProperty").AddComponent<ColliderProperty>();
				_colliderProperty.transform.SetParent(transform);
				_colliderProperty.transform.localPosition = Vector3.zero;
			}
			return _colliderProperty;
		}
	}
		
	void LateUpdate()
	{
		if (isEnterBuff) 
		{
			if( buff )
			{
				buffTime += Time.deltaTime;
				if( buffTime > buffTimeMax )
				{
					buff = false;
					buffTime = 0;
					GetComponentInParent<PlayerSystem>().Property -= property;
				}
			}
		}
	}

	public override void OnDestroyComponent ()
	{
		DestroyImmediate (_colliderProperty.gameObject,true);
	}

	/// <summary>
	/// 当键盘输入在mono行为每一帧时 ( state 所在的状态 , 此类调用此方法 , 按键枚举 , 是否进入状态 )
	/// </summary>

	public override bool OnInputUpdate ( State state , StateAction action , KeyCode key , bool isEnterState )
	{
		if( GetComponentInParent<PlayerSystem>().isDeath )
			return false;

		if (Input.GetKey (key) & !GetComponentInParent<PlayerSystem>().isAttack & state.stateID != state.stateMachine.stateIndex | Input.GetKey (key) & GetComponentInParent<PlayerSystem>().skillUpIndex < index & state.stateID != state.stateMachine.stateIndex) {
			GetComponentInParent<PlayerSystem>().skillUpIndex = index;
			return true;
		}

		return false;
	}

	/// <summary>
	/// 当动画开始播放(进入状态时)
	/// </summary>

	public override void OnStateEnter ( State state , StateAction action )
	{
		GetComponentInParent<PlayerSystem>().isMove = isMove;
		GetComponentInParent<PlayerSystem>().isAttack = isAttack;

		if( GetComponentInParent<PlayerSystem>().isMove ){
			PlayerMove.isTouch = false;
		}

		if( isEnterBuff ){
			if( buffMode == BuffModel.TimeExitBuff ){
				if( !buff ){
					GetComponentInParent<PlayerSystem>().Property += property;
					buff = true;
				}
			}else if( !animBuff ){
				GetComponentInParent<PlayerSystem>().Property += property;
				animBuff = true;
			}
		}

		if( useAttackSpeed ){
			state.animSpeed = GetComponentInParent<PlayerSystem>().attackSpeed;
		}

		foreach(ColliderBehaviour behaviour in colliderProperty.behaviours ){
			if( behaviour.Active ){
				behaviour.OnEnter( GetComponentInParent<PlayerSystem>() , transform );
			}
		}
	}

	/// <summary>
	/// 当动画结束时
	/// </summary>
	public override void OnStateExit ( State state , StateAction action )
	{
		if( isAttack ){
			GetComponentInParent<PlayerSystem>().isAttack = false;
		}

		if ( isEnterBuff & buffMode == BuffModel.AnimExitBuff & animBuff ) {
			GetComponentInParent<PlayerSystem>().Property -= property;
			animBuff = false;
		}

		foreach(ColliderBehaviour behaviour in colliderProperty.behaviours ){
			if( behaviour.Active ){
				behaviour.OnExit( GetComponentInParent<PlayerSystem>() , transform );
			}
		}
	}

	/// <summary>
	/// 当实例化技能物体时进入 ( action 状态动作管理(也是此类发送到此方法的入口点) , 子弹物体 )
	/// </summary>
	public override void OnInstantiateSpwanEnter ( State state, StateAction action, GameObject spwan)
	{
		SkillCollider coller = spwan.GetComponent<SkillCollider>();
		if( coller == null ){
			coller = spwan.AddComponent<SkillCollider>();
			coller.colliderProperty = Instantiate( colliderProperty , coller.transform );
			coller.colliderProperty.transform.localPosition = Vector3.zero;
		}
		if (action.activeModel == ActiveModel.SetActive) {
			coller.exitState = CloseState.ParentToNull;
		} else {
			coller.exitState = CloseState.Destroy;
		}
		coller.parent = stateManager.transform;
		coller.player = GetComponentInParent<PlayerSystem>();
		coller.time.EndTime = action.spwanTime;
		coller.attackProperty = GetComponentInParent<PlayerSystem>().Property;
		coller.colliderProperty.setData = colliderProperty;
	}

	/// <summary>
	/// 编辑器扩展 (重要提示!你想扩展编辑器就得返回真,否则显示默认编辑器监视面板) ( 参数state : 这个行为是被这个状态管理 )
	/// </summary>
	#if UNITY_EDITOR
	bool foldout = false;
	private SerializedObject _collProSerObj = null;
	public SerializedObject collProSerObj{
		get{
			if(_collProSerObj==null){
				_collProSerObj = new SerializedObject(colliderProperty);
			}
			return _collProSerObj;
		}
	}
	override public bool OnInspectorGUI( State state )
	{
		SerializedObject serializedObject = new SerializedObject(this);
		serializedObject.Update ();
		EditorGUILayout.PropertyField ( serializedObject.FindProperty( "isMove" ) , new GUIContent("释放技能可以移动") , true );
		EditorGUILayout.PropertyField ( serializedObject.FindProperty( "isAttack" ) , new GUIContent("是否设置为攻击型技能") , true );
		EditorGUILayout.PropertyField ( serializedObject.FindProperty( "index" ) , new GUIContent("中断技能级别") , true );
		EditorGUILayout.PropertyField ( serializedObject.FindProperty( "useAttackSpeed" ) , new GUIContent("使用玩家攻击速度") , true );
		EditorGUILayout.PropertyField ( serializedObject.FindProperty( "isEnterBuff" ) , new GUIContent("使用技能增益") , true );
		if(isEnterBuff){
			EditorGUILayout.PropertyField ( serializedObject.FindProperty( "buffMode" ) , new GUIContent("增益模式") , true );
			EditorGUILayout.PropertyField ( serializedObject.FindProperty( "buffTime" ) , new GUIContent("增益时间") , true );
			EditorGUILayout.PropertyField ( serializedObject.FindProperty( "buffTimeMax" ) , new GUIContent("增益结束时间") , true );
			EditorGUILayout.PropertyField ( serializedObject.FindProperty( "property" ) , new GUIContent("自身增益属性") , true );
			foldout = EditorGUILayout.Foldout ( foldout , "技能碰撞属性" , true );
			if( foldout )
			{
				EditorGUI.indentLevel = 5;
				collProSerObj.Update();
				EditorGUILayout.PropertyField ( collProSerObj.FindProperty( "伤害类型" ) , true );
				EditorGUILayout.PropertyField ( collProSerObj.FindProperty( "技能碰撞大小" ) , true );

				if( colliderProperty.击飞敌人 ){
					EditorGUILayout.BeginVertical( "box" );
					EditorGUILayout.PropertyField ( collProSerObj.FindProperty( "击飞敌人" ) , true );
					EditorGUILayout.PropertyField ( collProSerObj.FindProperty( "击飞方向" ) , true );
					EditorGUILayout.PropertyField ( collProSerObj.FindProperty( "击飞速度" ) , true );
					EditorGUILayout.PropertyField ( collProSerObj.FindProperty( "击飞特效" ) , true );
					EditorGUILayout.PropertyField ( collProSerObj.FindProperty( "击飞特效位置" ) , true );
					EditorGUILayout.PropertyField ( collProSerObj.FindProperty( "击飞时间" ) , true );
					EditorGUILayout.EndVertical();
				}else
					EditorGUILayout.PropertyField ( collProSerObj.FindProperty( "击飞敌人" ) , true );

				if( colliderProperty.减速敌人 ){
					EditorGUILayout.BeginVertical( "box" );
					EditorGUILayout.PropertyField ( collProSerObj.FindProperty( "减速敌人" ) , true );
					EditorGUILayout.PropertyField ( collProSerObj.FindProperty( "减速百分比" ) , true );
					EditorGUILayout.PropertyField ( collProSerObj.FindProperty( "减速特效" ) , true );
					EditorGUILayout.PropertyField ( collProSerObj.FindProperty( "减速特效位置" ) , true );
					EditorGUILayout.PropertyField ( collProSerObj.FindProperty( "减速时间" ) , true );
					EditorGUILayout.EndVertical();
				}else
					EditorGUILayout.PropertyField ( collProSerObj.FindProperty( "减速敌人" ) , true );

				if( colliderProperty.飞行技能 ){
					EditorGUILayout.BeginVertical( "box" );
					EditorGUILayout.PropertyField ( collProSerObj.FindProperty( "飞行技能" ) , true );
					EditorGUILayout.PropertyField ( collProSerObj.FindProperty( "飞行方向" ) , true );
					EditorGUILayout.PropertyField ( collProSerObj.FindProperty( "飞行速度" ) , true );
					EditorGUILayout.PropertyField ( collProSerObj.FindProperty( "飞行特效" ) , true );
					EditorGUILayout.PropertyField ( collProSerObj.FindProperty( "飞行特效位置" ) , true );
					EditorGUILayout.PropertyField ( collProSerObj.FindProperty( "飞行时间" ) , true );
					EditorGUILayout.EndVertical();
				}else
					EditorGUILayout.PropertyField ( collProSerObj.FindProperty( "飞行技能" ) , true );

				EditorGUILayout.PropertyField ( collProSerObj.FindProperty( "击中销毁特效" ) , true );
				collProSerObj.ApplyModifiedProperties();

				for (int i = 0; i < colliderProperty.behaviours.Count; ++i) 
				{
					if (colliderProperty.behaviours[i] == null) {
						colliderProperty.behaviours.RemoveAt (i);
						continue;
					}
					EditorGUILayout.BeginHorizontal ();
					Rect rect = EditorGUILayout.GetControlRect ();
					colliderProperty.behaviours[i].show = EditorGUI.Foldout( new Rect( rect.x , rect.y , 80 , rect.height ) , colliderProperty.behaviours[i].show , GUIContent.none );
					colliderProperty.behaviours[i].Active = EditorGUI.ToggleLeft( new Rect( rect.x + 5 , rect.y , 100 , rect.height ) , GUIContent.none , colliderProperty.behaviours[i].Active );
					EditorGUI.LabelField ( new Rect( rect.x + 20 , rect.y , rect.width - 15 , rect.height ) , colliderProperty.behaviours[i].GetType().Name + " (ActionBehaviour)" , GUI.skin.GetStyle("BoldLabel") );
					if ( GUI.Button ( new Rect( rect.x + rect.width - 15 , rect.y , rect.width , rect.height ) , GUIContent.none , GUI.skin.GetStyle( "ToggleMixed" ) ) ) {
						DestroyImmediate ( colliderProperty.behaviours[i].gameObject , true );
						colliderProperty.behaviours.RemoveAt (i);
						continue;
					}
					EditorGUILayout.EndHorizontal ();
					if( colliderProperty.behaviours[i].show ){
						EditorGUI.indentLevel = 6;
						SerializedObject behaviourSerialized = new SerializedObject(colliderProperty.behaviours[i]);
						behaviourSerialized.Update ();
						EditorGUILayout.ObjectField ( "Script" , colliderProperty.behaviours[i] , typeof(ColliderBehaviour) , true );
						if( !colliderProperty.behaviours [i].OnInspectorGUI ( state ) ){
							foreach( FieldInfo f in colliderProperty.behaviours[i].GetType ().GetFields () ){
								if ( colliderProperty.behaviours[i].GetType () == f.DeclaringType & !f.IsStatic ) {
									EditorGUILayout.PropertyField( behaviourSerialized.FindProperty( f.Name ) , true );
								}
							}
						}
						behaviourSerialized.ApplyModifiedProperties ();
						GUILayout.Space (4);
						GUILayout.Box ("", BlueprintSetting.instance.HorSpaceStyle, GUILayout.Height(1) , GUILayout.ExpandWidth(true));
						GUILayout.Space (4);

						EditorGUI.indentLevel = 5;
					}
				}

				EditorGUILayout.Space ();
				Rect r = EditorGUILayout.GetControlRect ();
				Rect rr = new Rect (new Vector2 (r.x + (r.size.x / 4f), r.y), new Vector2( r.size.x / 2f , 20 ) );
				if (GUI.Button (rr, "添加碰撞事件" )) {
					colliderProperty.findBehaviours = true;
				}

				if (colliderProperty.findBehaviours) {
					EditorGUILayout.Space ();
					Type[] types = Assembly.Load( "Assembly-CSharp" ).GetTypes ().Where (t => t.IsSubclassOf(typeof(ColliderBehaviour)) ).ToArray<Type> ();
					foreach( Type bn in types ){
						if (GUILayout.Button ( bn.Name ) ) {
							ColliderBehaviour ab = (ColliderBehaviour) new GameObject( bn.Name ).AddComponent ( bn );
							ab.transform.SetParent ( colliderProperty.transform );
							ab.transform.localPosition = Vector3.zero;
							colliderProperty.behaviours.Add ( ab );
							colliderProperty.findBehaviours = false;
						}
					}

					Rect addRect = EditorGUILayout.GetControlRect ();
					colliderProperty.CreateScriptName = EditorGUI.TextField ( new Rect ( new Vector2( addRect.position.x - 70, addRect.position.y ) , new Vector2( addRect.size.x - 70f , 18 )) , colliderProperty.CreateScriptName );
					if (GUI.Button (new Rect ( new Vector2( addRect.size.x - 100f , addRect.position.y ) , new Vector2( 120 , 18 )), "添加新的动作脚本" )) {

						if( Directory.Exists ( Application.dataPath + "/GameDesigner/Actions/Skill/SkillAction" ) == false ) {
							Directory.CreateDirectory ( Application.dataPath + "/GameDesigner/Actions/Skill/SkillAction" );
						}

						File.AppendAllText ( Application.dataPath + "/GameDesigner/Actions/Skill/SkillAction/" + colliderProperty.CreateScriptName + ".cs" , 
							"using UnityEngine;\nusing System.Collections.Generic;\nusing GameDesigner;\n#if UNITY_EDITOR\nusing UnityEditor;\n#endif\n\n" +
							"public class " + colliderProperty.CreateScriptName + " : ColliderBehaviour\n{\n\n\t" +
							"/// <summary>\n\t/// 当进入触发器 ( other参数包含敌人,玩家,怪物等等 )\n\t" +
							"/// </summary>\n\n\toverride public void OnSkillTriggerEnter ( SkillCollider skill , Game.PlayerSystem other , Transform parent )\n\t{\n\n\t}\n\n\t" +
							"/// <summary>\n\t/// 编辑器扩展 (重要提示!你想扩展编辑器就得返回真,否则显示默认编辑器监视面板) ( 参数state : 这个行为是被这个状态管理 )\n\t" +
							"/// </summary>\n\n\t#if UNITY_EDITOR\n\toverride public bool OnInspectorGUI( State state )\n\t{\n\t\t" +
							"return false; //返回假: 绘制默认监视面板 | 返回真: 绘制扩展自定义监视面板\n\t}\n\t#endif\n}"
						);

						AssetDatabase.Refresh();
						colliderProperty.findBehaviours = false;
					}
					if (GUILayout.Button ( "取消操作" )) {
						colliderProperty.findBehaviours = false;
					}
				}
				EditorGUILayout.Space ();
			}
		}

		serializedObject.ApplyModifiedProperties ();

		return true; //返回假: 绘制默认监视面板 | 返回真: 绘制扩展自定义监视面板
	}
	#endif
}
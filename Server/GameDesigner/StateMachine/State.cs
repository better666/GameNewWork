using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace GameDesigner
{
    /// <summary>
    /// 动画模式
    /// </summary>
    public enum AnimationMode
    {
        Animation,
        Animator
    }

	/// <summary>
	/// 状态 -- v2017/12/6
	/// </summary>
	public sealed class State : IState
	{
        /// <summary>
        /// 状态ID
        /// </summary>
		public int stateID = 0;
        /// <summary>
        /// 下一个状态
        /// </summary>
		public State nextState = null;
        /// <summary>
        /// 状态连接集合
        /// </summary>
		public List<Transition> transitions = new List<Transition> ();
        /// <summary>
        /// 状态行为集合
        /// </summary>
		public List<StateBehaviour> behaviours = new List<StateBehaviour> ();
        /// <summary>
        /// 动作系统 使用为真 , 不使用为假
        /// </summary>
		public bool actionSystem = false;
        /// <summary>
        /// 旧动画系统
        /// </summary>
        public Animation anim = null;
        /// <summary>
        /// 新动画系统
        /// </summary>
        public Animator animator = null;
        /// <summary>
        /// 动画循环?
        /// </summary>
        public bool animLoop = true;
        /// <summary>
        /// 动画模式
        /// </summary>
        public AnimMode mode = AnimMode.Random;
        /// <summary>
        /// 动作索引
        /// </summary>
		public int actionIndex = 0;
        /// <summary>
        /// 音效索引
        /// </summary>
		public int audioIndex = 0;
        /// <summary>
        /// 动画速度
        /// </summary>
		public float animSpeed = 1;
        /// <summary>
        /// 动画结束事件
        /// </summary>
		public OnAnimExit onAnimExit = new OnAnimExit ();

        /// <summary>
        /// 状态自身触发按键 启动为真 , 关闭为假
        /// </summary>
        public bool isEnableKey = false;
        /// <summary>
        /// 按键值
        /// </summary>
		public KeyCode key = KeyCode.Q;
        /// <summary>
        /// 状态自身按键进入下一个状态ID
        /// </summary>
		public int DstStateID = 0;
        /// <summary>
        /// 技能冷却 使用为真 , 不使用为假
        /// </summary>
		public bool useLengqueTime = false;
        /// <summary>
        /// 冷却时间
        /// </summary>
		public Timer lengqueTime = new Timer(0,5);
        /// <summary>
        /// 冷却值方向,当lengqueInverse为-1或1时,冷却图片的裁剪是左右方向的
        /// </summary>
		public bool lengqueInverse = false;
        /// <summary>
        /// 冷却图标
        /// </summary>
		public Image lengqueTimeImage = null;
        /// <summary>
        /// 冷却时间文本
        /// </summary>
		public Text lengqueText = null;
        /// <summary>
        /// 冷却图标的查找
        /// </summary>
        public bool imageFind = false;

        /// <summary>
        /// 冷却图标的物体名称
        /// </summary>
        public string imageName = "";
        /// <summary>
        /// 冷却文字的查找
        /// </summary>
        public bool textFind = false;
        /// <summary>
        /// 冷却文字的物体名称
        /// </summary>
        public string textName = "";
        /// <summary>
        /// 状态动作集合
        /// </summary>
		public List<StateAction> actions = new List<StateAction> ();

        private State() { }

        /// <summary>
        /// 创建状态
        /// </summary>
		public static State CreateStateInstance (StateMachine stateMachine , string stateName, Vector2 position)
		{
			State state = new GameObject (stateName).AddComponent<State> ();
			state.transform.hideFlags = HideFlags.None;
			state.stateMachine = stateMachine;
			state.name = "新的状态 " + stateMachine.states.Count;
			state.rect.position = position;
			stateMachine.states.Add (state); 
			state.transform.SetParent (stateMachine.transform);
			state.actions.Add (new StateAction ());
			return state;
		}

        /// <summary>
        /// 构造函数
        /// </summary>
		public State (StateMachine _stateMachine)
		{
			stateMachine = _stateMachine;
		}

        /// <summary>
        /// 构造函数
        /// </summary>
		public State (StateMachine _stateMachine, Vector2 position)
		{
			stateMachine = _stateMachine;
			rect.position = position;
		}

		/// <summary>
        /// 当前状态动作
        /// </summary>
		public StateAction Action{
			get{
				if (actionIndex >= actions.Count)
					actionIndex = 0;
				return actions [actionIndex];
			}
		}

		void Awake ()
		{
            enabled = true;
			anim = stateManager.GetComponent<Animation> ();
            animator = stateManager.GetComponent<Animator>();
            if (useLengqueTime & imageFind)
            {
                lengqueTimeImage = FindComponent.FindObjectsOfTypeAll<Image>(imageName);
            }
            if (useLengqueTime & textFind)
            {
                lengqueText = FindComponent.FindObjectsOfTypeAll<Text>(textName);
            }
        }

		void LateUpdate ()
		{
			if(!actionSystem)//未启用系统动作则返回
				return;

			if(useLengqueTime)
            {
				if(!lengqueTime.IsOutTime)
                {
                    if (lengqueTimeImage != null)
                    {
                        lengqueTimeImage.type = Image.Type.Filled;
                        lengqueTimeImage.fillAmount = lengqueInverse ? 1 - (lengqueTime.time / lengqueTime.EndTime) : lengqueTime.time / lengqueTime.EndTime;
                    }
                    if (lengqueText != null)
                    {
                        lengqueText.text = (lengqueTime.EndTime - lengqueTime.time).ToString("F0");
                        if (lengqueTime.time >= lengqueTime.EndTime - 0.05f)
                        {
                            lengqueText.text = string.Empty;
                        }
                    }
                    return;
                }
			}

			if (isEnableKey) 
			{
				if (Action.behaviours.Count == 0) {
					if (Input.GetKey (key) & DstStateID != stateMachine.stateIndex) {
						stateManager.OnEnterNextState (DstStateID);
						lengqueTime.time = 0;
					}
				} else {
					foreach (ActionBehaviour behaviour in Action.behaviours) {  // 当全局mono每一帧运行时调用(全局输入)
						if (behaviour.Active & behaviour.OnInputUpdate (this, Action, key, Input.GetKey (key))) {
							stateManager.OnEnterNextState (DstStateID);
							lengqueTime.time = 0;
						}
					}
				}
			}
		}

		void OnDestroy ()
		{
			foreach (StateAction act in actions) { // 当意外状态物体被销毁,删除对象池物体
				foreach (GameObject go in act.activeObjs) {
					if (go != null) {
						Destroy (go);
					}
				}
			}
		}

        /// <summary>
        /// 进入状态
        /// </summary>
		public void OnEnterState ()
		{
			foreach (ActionBehaviour behaviour in Action.behaviours) { //当子动作的动画开始进入时调用
				if (behaviour.Active) {
					behaviour.OnStateEnter (this, Action);
				}
			}

			if (mode == AnimMode.Random) {
				actionIndex = Random.Range (0, actions.Count);
			} else {
				actionIndex = (actionIndex < actions.Count - 1) ? actionIndex + 1 : 0;
			}

			if (Action.isPlayAudio & Action.audioModel == AudioModel.EnterPlayAudio) {
				audioIndex = Random.Range (0, Action.audioClips.Count);
                AudioManager.Play(Action.audioClips[audioIndex]);
            }

			try {
                switch (stateMachine.animMode) {
                    case AnimationMode.Animation:
                    anim[Action.clipName].speed = animSpeed;
                    anim.Rewind(Action.clipName);
                    anim.Play(Action.clipName);
                    break;
                    case AnimationMode.Animator:
                    animator.speed = animSpeed;
                    animator.Rebind();
                    animator.Play(Action.clipName);
                    break;
                }
            } catch {
				Debug.Log ("需要动画组件或动画剪辑获取失败!");
			}
		}

        /// <summary>
        /// 状态每一帧
        /// </summary>
		public void OnUpdateState (StateManager stateManager, State state)
		{
            var isPlaying = true;
            switch (stateMachine.animMode) {
                case AnimationMode.Animation:
                Action.animTime = anim[Action.clipName].time / anim[Action.clipName].length * 100;
                isPlaying = anim.isPlaying;
                break;
                case AnimationMode.Animator:
                Action.animTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime / 1 * 100;
                break;
            }
            if (Action.animTime >= Action.animEventTime & !Action.eventEnter) {
				if (Action.effectSpwan != null) {
					if (Action.activeModel == ActiveModel.Instantiate) {
                        Destroy(InstantiateSpwan (stateManager), Action.spwanTime);
					}
					if (Action.activeModel == ActiveModel.SetActive) {
						bool active = false;
						foreach (GameObject go in Action.activeObjs) {
							if (go == null) {
								Action.activeObjs.Remove (go);
								break;
							}
							if (!go.activeSelf) {
								go.SetActive (true);
								go.transform.SetParent (null);
								SetPosition (stateManager, go);
								active = true;
								break;
							}
						}
						if (!active) {
							GameObject go = InstantiateSpwan (stateManager);
							Action.activeObjs.Add (go);
						}
					}
				}
				if (Action.isPlayAudio & Action.audioModel == AudioModel.AnimEventPlayAudio) {
					audioIndex = Random.Range (0, Action.audioClips.Count);
                    AudioManager.Play(Action.audioClips[audioIndex]);
				}
				Action.eventEnter = true;

				foreach (ActionBehaviour behaviour in Action.behaviours) { //当子动作的动画事件进入
					if (behaviour.Active) {
						behaviour.OnAnimationEventEnter (state, Action, Action.animEventTime);
					}
				}
			}

			if (Action.animTime >= Action.animTimeMax | !isPlaying) {
                Action.eventEnter = false;
				if (onAnimExit.isExitState & state.transitions.Count != 0) {
					state.transitions [onAnimExit.DstStateID].isEnterNextState = true;
					return;
				}
				if (animLoop) {
					OnExitState ();//退出函数
					OnEnterState ();//重载进入函数
                    return;
				}
			}

			foreach (ActionBehaviour behaviour in Action.behaviours) {
				if (behaviour.Active) {
					behaviour.OnStateUpdate (state, Action);
				}
			}
		}

        /// <summary>
        /// 设置技能位置
        /// </summary>
		private void SetPosition (StateManager stateManager, GameObject go)
		{
			switch (Action.spwanmode) {
			case SpwanModel.localPosition:
				go.transform.localPosition = stateManager.transform.TransformPoint (Action.effectPostion);
				go.transform.rotation = stateManager.transform.rotation;
				break;
			case SpwanModel.SetParent:
				Action.parent = Action.parent ? Action.parent : stateManager.transform;
				go.transform.SetParent (Action.parent);
				go.transform.position = Action.parent.TransformPoint (Action.effectPostion);
				go.transform.rotation = Action.parent.rotation;
				break;
			case SpwanModel.SetInTargetPosition:
				Action.parent = Action.parent ? Action.parent : stateManager.transform;
				go.transform.SetParent (Action.parent);
				go.transform.position = Action.parent.TransformPoint (Action.effectPostion);
				go.transform.rotation = Action.parent.rotation;
				go.transform.parent = null;
				break;
			}
			foreach (ActionBehaviour behaviour in Action.behaviours) { // 当实例化技能物体调用
				if (behaviour.Active) {
					behaviour.OnInstantiateSpwanEnter (this, Action, go);
				}
			}
		}

        /// <summary>
        /// 技能实例化
        /// </summary>
		private GameObject InstantiateSpwan (StateManager stateManager)
		{
			GameObject go = (GameObject)Instantiate (Action.effectSpwan);
			SetPosition (stateManager, go);
			return go;
		}

        /// <summary>
        /// 当退出状态
        /// </summary>
		public void OnExitState ()
		{
            if (Action.isPlayAudio & Action.audioModel == AudioModel.ExitPlayAudio) {
				audioIndex = Random.Range (0, Action.audioClips.Count);
                AudioManager.Play(Action.audioClips[audioIndex]);
            }
			foreach (ActionBehaviour behaviour in Action.behaviours) { //当子动作结束
				if (behaviour.Active) {
					behaviour.OnStateExit (this, Action);
				}
			}
			Action.eventEnter = false;
		}
	}
}
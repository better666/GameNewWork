namespace GameDesigner
{
    using Net.Client;
    using Net.Share;
    using UnityEngine;

    /// <summary>
    /// 状态行为树基类
    /// </summary>
    public abstract class IBehaviour : MonoBehaviour
	{
		[HideInInspector]
		public bool show = true;

		public bool Active{
			get{ return enabled; }
			set{ enabled = value; }
		}

		[SerializeField]
		private StateMachine _stateMachine = null;
		public StateMachine stateMachine{
			get{
				if(_stateMachine == null){
					_stateMachine = transform.GetComponentInParent<StateMachine>();
				}
				return _stateMachine;
			}
			set{ _stateMachine = value; }
		}

		[SerializeField]
		private StateManager _stateManager = null;
		public StateManager stateManager{
			get{
				if(_stateManager == null){
					_stateManager = transform.GetComponentInParent<StateManager>();
				}
				return _stateManager;
			}
			set{ _stateManager = value; }
		}

		private State _state = null;///状态管辖--一般用于状态行为获取自身的状态对象
		public State state{
			get{
				if(_state==null)
					_state = transform.GetComponentInParent<State> ();
				return _state;
			}
		}

		/// <summary>
		/// 当组件被删除调用一次
		/// </summary>
		public virtual void OnDestroyComponent()
		{
			
		}

		/// <summary>
		/// 编辑器扩展 (重要提示!你想扩展编辑器就得返回真,否则显示默认编辑器监视面板) ( 参数state : 这个行为是被这个状态管理 )
		/// </summary>

		public virtual bool OnInspectorGUI( State state )
		{
			return false; //返回假: 绘制默认监视面板 | 返回真: 绘制扩展自定义监视面板
		}

        /// <summary>
        /// 发送封包数据 - 远程过程调用服务器函数
        /// </summary>
        /// <param name="fun">要调用服务器的函数名</param>
        /// <param name="pars">对应服务器函数的参数</param>
        public void Send(string fun, params object[] pars)
        {
            NetClient.Send(fun, pars);
        }

        /// <summary>
        /// 发送封包数据 - 带有几种命令的远程过程调用函数
        /// </summary>
        /// <param name="cmd">网络命令</param>
        /// <param name="fun">要调用服务器的函数名</param>
        /// <param name="pars">服务器的函数参数</param>
        public void Send(NetCmd cmd, string fun, params object[] pars)
        {
            NetClient.Send(cmd, fun, pars);
        }
    }
}
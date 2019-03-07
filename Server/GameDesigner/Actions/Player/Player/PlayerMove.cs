using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using GameDesigner;

public enum MoveSelection
{
    NavMeshAgent ,
    MoveThisTarget,
	MoveNotRoto,
}

/// <summary>
/// 移动对象组件
/// </summary>

[AddComponentMenu("Game.PlayerSystem/Player/PlayerMove")]
public class PlayerMove : StateBehaviour 
{
    public MoveSelection moveSele = MoveSelection.MoveThisTarget;
    public Transform target = null;
    public NavMeshAgent agent = null;
	public Transform agentHit = null;
	public GameDesigner.PlayerSystem m_Player = null;

	///其它脚本控制此类的静态方法并且还使用了此类的属性变量，比如UIisTouchMove脚本就是使用此类的所有静态和变量
	static public bool isTouch = false;

	static PlayerMove _instance = null;
	static public PlayerMove instance{
		get{
			if( _instance == null ){
				PlayerMove pm = GameObject.FindObjectOfType<PlayerMove>();
				if (pm != null){
					return _instance = pm;
				}else{
					Debug.LogWarning ( "PlayerMove脚本未加载在运行窗口中或你关闭了这个脚本！" );
				}
			}
			return _instance; 
		}
	}

	// Use this for initialization
	void Start () 
	{
		target = transform.root;
		agent = transform.GetComponentInParent<NavMeshAgent>();
		m_Player = transform.GetComponentInParent<GameDesigner.PlayerSystem> ();
		if(m_Player==null)
			enabled = false;
	}

	// Update is called once per frame ---- 全局移动,当灵活技能运行时用到.(技能播放时可以移动用到)
	void Update()
	{
		// 如果玩家为空,或玩家的禁止移动为真,或UI方向盘拖动时禁止这个脚本的移动时 返回 如果出现异常也返回
		if(isTouch | !m_Player.isMove | m_Player.isDeath ) 
			return;

		switch( moveSele )
		{
		case MoveSelection.MoveThisTarget:
			Move ( target , moveDirection , m_Player.moveSpeed , Camera.main );
			break;
		case MoveSelection.NavMeshAgent:
			Move( agent , agentHit , m_Player.moveSpeed , m_Player.isMove , OnClickHit.RaycastHitPoint( agentHit , KeyCode.Mouse0 ) );
			break;
		case MoveSelection.MoveNotRoto:
			if (moveDirection != Vector3.zero) {
				target.Translate ( Camera.main.transform.TransformDirection(moveDirection) * m_Player.moveSpeed * Time.deltaTime);
			}
			break;
		}
	}

	static public Vector3 moveDirection
    {
		get{ return MoveDirection(); }
    }

    /// <summary>
    /// 获取移动方向
    /// </summary>

    static public Vector3 MoveDirection()
    {
        return new Vector3( Input.GetAxis( "Horizontal" ) , 0f , Input.GetAxis( "Vertical" ) );
    }

	/// <summary>
	/// 移动对象方法 ( 参数target(移动对象) , 参数speed(移动速度) )
	/// </summary>

	static public void Move( Transform target , Vector3 moveDirection , float speed , Camera lookTarget )
	{
		Move(target,moveDirection,speed,lookTarget.transform);
	}

    static float Y = 0;

    /// <summary>
    /// 移动对象方法 ( 参数target(移动对象) , 参数moveDire(移动方向) , 参数speed(移动速度) , 参数anim(移动对象动画) , 参数clip(移动对象播放动画剪辑) , 参数PlayAnim(是否移动的时候播放动画)
    /// </summary>
    
	static public void Move( Transform target , Vector3 moveDirection , float speed , Transform lookTarget )
    {
		if( moveDirection != Vector3.zero )
        {
            Y = ( moveDirection.z > .1f ) ? moveDirection.x * 60F :  //前进-和-前进-《左右斜走》60%
                ( moveDirection.z < -.1f ) ? ( moveDirection.x * -60F ) + -180F :  //后进-和-后方-《左右斜走》-60%
                ( moveDirection.x > .1f ) ? ( moveDirection.y * 120F ) + 90F :  //左进
                ( moveDirection.x < -.1f ) ? ( moveDirection.y * -120F ) + -90F : Y;  //右进

            target.rotation = Quaternion.Euler( 0 , lookTarget.eulerAngles.y + Y , 0 );
            target.Translate( 0 , 0 , speed * Time.deltaTime );
        }
    }

	/// <summary>
	/// Android移动对象方法 ( 参数target(移动对象) , 参数moveDire(移动方向) , 参数speed(移动速度) , 参数anim(移动对象动画) , 参数clip(移动对象播放动画剪辑) , 参数PlayAnim(是否移动的时候播放动画)
	/// </summary>
   
	static public void isTouchMove( Transform target , Vector3 moveDirection , float speed , Transform lookTarget )
	{
		if( moveDirection != Vector3.zero )
		{
			Y = ( moveDirection.y > .1f ) ? moveDirection.x :  //前进-和-前进-《左右斜走》-60%
				( moveDirection.y < -.1f ) ? -moveDirection.x -180F :  //后进-和-后方-《左右斜走》-60%
				( moveDirection.x > .1f ) ? moveDirection.y + 50f + 90F :  //左进
				( moveDirection.x < -.1f ) ? -moveDirection.y - 50f-90F : Y;  //右进

			target.rotation = Quaternion.Euler( 0 , lookTarget.eulerAngles.y + Y , 0 );
			target.Translate( 0 , 0 , speed * Time.deltaTime );
		}
	}

    /// <summary>
    /// 移动对象方法 ( 参数speed(移动速度) , 参数anim(移动对象动画) , 参数clip(移动对象播放动画剪辑) , 参数PlayAnim(是否移动的时候播放动画)
    /// </summary>
    
	static public void Move( NavMeshAgent agent , Transform hitTarget , float speed , bool isMove , bool onClickHit )
    {
        if(!isMove)
        {
            agent.destination = agent.transform.position;
            return;
        }

        if (onClickHit & isMove )
        {
            agent.speed = speed;
            agent.destination = hitTarget.position;
        }
    }
}

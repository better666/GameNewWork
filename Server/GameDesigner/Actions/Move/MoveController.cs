using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDesigner;

public class MoveController : StateBehaviour 
{
	[Header("玩家移动对象")]
	public Transform target;
	[Header("玩家移动速度")]
	public float speed = 5f;
	[Header("玩家前进始终面朝相机前进方向")]
	public Camera lookTarget;
	public string FindCamera = "Main Camera";

	float Y = 0;

	public override void OnEnterState (StateManager stateMachine, State currentState, State nextState)
	{
		if( target == null ){
			target = stateMachine.transform;
		}

		if( lookTarget == null ){
			lookTarget = FindComponent.FindObjectsOfTypeAll<Camera>( FindCamera );
		}
	}

	public override void OnUpdateState (StateManager stateMachine, State currentState, State nextState)
	{
		if( moveDirection != Vector3.zero )
		{
			Y = ( moveDirection.z > .1f ) ? moveDirection.x * 60F :  //前进-和-前进-《左右斜走》60%
				( moveDirection.z < -.1f ) ? ( moveDirection.x * -60F ) + -180F :  //后进-和-后方-《左右斜走》-60%
				( moveDirection.x > .1f ) ? ( moveDirection.y * 120F ) + 90F :  //左进
				( moveDirection.x < -.1f ) ? ( moveDirection.y * -120F ) + -90F : Y;  //右进

			target.rotation = Quaternion.Euler( 0 , lookTarget.transform.eulerAngles.y + Y , 0 );

			if (GetComponentInParent<GameDesigner.PlayerSystem>() != null) {
				target.transform.Translate (0, 0, GetComponentInParent<GameDesigner.PlayerSystem>().moveSpeed * Time.deltaTime);
			} else {
				target.transform.Translate (0, 0, speed * Time.deltaTime);
			}
		}
	}

	static public Vector3 moveDirection
	{
		get{ return new Vector3( Input.GetAxis( "Horizontal" ) , 0f , Input.GetAxis( "Vertical" ) ); }
	}

    /// <summary>
	/// 摄像机的Transform通过摇杆输出的方向
	/// </summary>
	public static Vector3 Transform3Dir(Transform t, Vector3 dir)
    {
        //注意：1、摄像机的Y轴角度和摇杆的坐标系是反方向的  2、角度和弧度
        var f = Mathf.Deg2Rad * (-t.rotation.eulerAngles.y);
        //方向标准化
        dir.Normalize();
        //旋转角度
        var ret = new Vector3(dir.x * Mathf.Cos(f) - dir.z * Mathf.Sin(f), 0, dir.x * Mathf.Sin(f) + dir.z * Mathf.Cos(f));
        return ret;
    }
}

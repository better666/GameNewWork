using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MobaCamera : MonoBehaviour
{
	public Transform target;
    public GameDesigner.Player m_Player;
	public Vector3 targetHeight = new Vector3 (0 , 1.2f , 0);
    public float distance = 2f , y = 2f , x ;
    public float yLoL = 60f , xLoL = 135f;
    public float m_LoL_Distance = 30f;
    public bool LoLCameraToUnityCamera = false;
    public bool LookAt = false;
    public float ZoomSpeed = 10;
    public float MovingSpeed = 0.5f;
    public float RotateSpeed = 1;
    public Vector3 viewPos;
    public bool  DisLock = false;
    public float LockDistance = 15F;
    public float MoveDistance = 20f;
    public float yMinLimit = -10;
    public float yMaxLimit = 70;

    private Vector3 position;
    private Quaternion rotation;
    private float delta_x,delta_y,delta_z;
    private float delta_rotation_x,delta_rotation_y;

	public enum ViewportPoint
	{
		targetPosition,mousePosition
	}
	public ViewportPoint viewportPoint = ViewportPoint.targetPosition;
	public Vector2 viewPosMinMax = new Vector2( 0.3f , 0.7f );

	// Use this for initialization
	void Start () 
	{
        if ( m_Player == null )
        {
			m_Player = GameObject.FindObjectOfType<GameDesigner.Player> () as GameDesigner.Player;
            target = m_Player ? m_Player.transform : null;
            enabled = m_Player ? true : false;
        }
        FuHuo ( );
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
        if ( target == false | m_Player == false )
        {
            Debug.Log ( "没有平移对象或玩家脚本！" );
            enabled = false;
            return;
        }

        if ( !LoLCameraToUnityCamera )
        {
            if ( Input.GetKey ( KeyCode.Space ) || LookAt )
            {
                FuHuo ( );
                return;
            }
            LOLCamera ( );
        }
        else
        {
            if ( DisLock == true )
            {
                if ( Vector3.Distance ( transform.position , target.position ) > MoveDistance )
                {
                    distance = LockDistance;
                    UnityZero ( );
                }
            }

            if ( Input.GetKey ( KeyCode.Space ) || LookAt )
            {
                if ( Input.GetButton ( "Fire2" ) )
                {
                    x += Input.GetAxis ( "Mouse X" ) * 100 * 0.02f;
                    y -= Input.GetAxis ( "Mouse Y" ) * 120f * 0.02f;
                }
                UnityZero ( );
                return;
            }
            UnityCamera ( );
        }

	}

	public void SetLOLDistance( Slider value )
	{
		m_LoL_Distance = value.value;
	}

    //  Unity引擎视角 复位
    public void UnityZero ( )
    {
        distance -= ( Input.GetAxis ( "Mouse ScrollWheel" ) * Time.deltaTime ) * 80f * Mathf.Abs ( distance );
        distance = Mathf.Clamp ( distance , 5f , 40f );
        var rotation = Quaternion.Euler ( y , x , 0 );
        transform.rotation = rotation;
        position = target.position - ( rotation * Vector3.forward * distance );
        transform.position = position;
    }

    //  复活 时 对视 角色 复活地点
    public void FuHuo ( ) 
    {
        x = xLoL;
        y = yLoL;
        distance = m_LoL_Distance;
        distance = Mathf.Clamp ( distance , 5f , 20f );
        var rotation = Quaternion.Euler ( y , x , 0 );
        transform.rotation = rotation;
        position = target.position - ( rotation * Vector3.forward * distance );
        transform.position = position + targetHeight;
    }

    //  英雄联盟 视角 LOL
    public void LOLCamera ( ) 
    {
		switch( viewportPoint )
		{
		case ViewportPoint.targetPosition:
			{
				viewPos = GetComponent<Camera>().WorldToViewportPoint ( target.position );
				break;
			}
		case ViewportPoint.mousePosition:
			{
				viewPos = GetComponent<Camera>().ScreenToViewportPoint ( Input.mousePosition );
				break;
			}
		}

		delta_x = ( viewPos.x < viewPosMinMax.x ? m_Player.moveSpeed * 2 * Time.deltaTime : viewPos.x > viewPosMinMax.y ? -m_Player.moveSpeed * 2 * Time.deltaTime : 0f ) * MovingSpeed;
		delta_y = ( viewPos.y < viewPosMinMax.x ? m_Player.moveSpeed * 2 * Time.deltaTime : viewPos.y > viewPosMinMax.y ? -m_Player.moveSpeed * 2 * Time.deltaTime : 0f ) * MovingSpeed;
        
        rotation = Quaternion.Euler ( 0 , transform.rotation.eulerAngles.y , 0 );
        transform.position = rotation * new Vector3 ( -delta_x , 0 , -delta_y ) + transform.position + targetHeight;
    }

    //  Unity 引擎 的 视角
    public void UnityCamera ( ) 
    {
        if ( Input.GetMouseButton ( 0 ) )
        {
            delta_x = Input.GetAxis ( "Mouse X" ) * MovingSpeed;
            delta_y = Input.GetAxis ( "Mouse Y" ) * MovingSpeed;
            rotation = Quaternion.Euler ( 0 , transform.rotation.eulerAngles.y , 0 );
            transform.position = rotation * new Vector3 ( -delta_x , 0 , -delta_y ) + transform.position + targetHeight;
        }

        if ( Input.GetAxis ( "Mouse ScrollWheel" ) != 0 )
        {
            delta_z = -Input.GetAxis ( "Mouse ScrollWheel" ) * ZoomSpeed;
            transform.Translate ( 0 , 0 , -delta_z );
            distance += delta_z;
        }

        if ( Input.GetMouseButton ( 1 ) )
        {
            delta_rotation_x = Input.GetAxis ( "Mouse X" ) * RotateSpeed;
            delta_rotation_y = -Input.GetAxis ( "Mouse Y" ) * RotateSpeed;
            position = transform.rotation * new Vector3 ( 0 , 0 , distance ) + transform.position;
            transform.Rotate ( 0 , delta_rotation_x , 0 , Space.World );
            transform.Rotate ( delta_rotation_y , 0 , 0 );
            transform.position = transform.rotation * new Vector3 ( 0 , 0 , -distance ) + position;
        }

        y = ClampAngle ( y , yMinLimit , yMaxLimit );
        viewPos = GetComponent<Camera>().WorldToViewportPoint ( target.position );

		delta_x = ( viewPos.x < viewPosMinMax.x ? m_Player.moveSpeed * 2 * Time.deltaTime : viewPos.x > viewPosMinMax.y ? -m_Player.moveSpeed * 2 * Time.deltaTime : 0f ) * MovingSpeed;
		delta_y = ( viewPos.y < viewPosMinMax.x ? m_Player.moveSpeed * 2 * Time.deltaTime : viewPos.y > viewPosMinMax.y ? -m_Player.moveSpeed * 2 * Time.deltaTime : 0f ) * MovingSpeed;

        rotation = Quaternion.Euler ( 0 , transform.rotation.eulerAngles.y , 0 );
        transform.position = rotation * new Vector3 ( -delta_x , 0 , -delta_y ) + transform.position + targetHeight;
    }

    static float ClampAngle ( float angle , float min , float max )
    {
        if ( angle < -360 )
            angle += 360;
        if ( angle > 360 )
            angle -= 360;
        return Mathf.Clamp ( angle , min , max );
    }
}

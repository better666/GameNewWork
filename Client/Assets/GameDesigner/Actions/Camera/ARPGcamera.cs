using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/*****************摄像机*************/

public class ARPGcamera : MonoBehaviour 
{

    public Transform target;
    public float targetHeight = 1.2f;
    public float distance = 4.0f;
    public float maxDistance = 20;
    public float minDistance = 1.0f;
    public float xSpeed = 500.0f;
    public float ySpeed = 120.0f;
    public float yMinLimit = -10;
    public float yMaxLimit = 70;
    public float zoomRate = 80;
    public float rotationDampening = 3.0f;
    public float x = 20.0f;
    public float y = 0.0f;
    public float aimAngle = 8;
	public KeyCode key = KeyCode.Mouse1;
	protected Quaternion aim;
	protected Quaternion rotation;
	private Vector3 position;

    void Start ()
    {
		//target = GameObject.FindGameObjectWithTag ("Player").transform;
    }
    
    void Update ()
    {
        if ( !target )//2
            return;

		if (Input.GetKey(key))//4
        {
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
        }

        distance -= (Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime) * zoomRate * Mathf.Abs(distance);
        distance = Mathf.Clamp( distance , minDistance , maxDistance );//6

        y = ClampAngle( y , yMinLimit , yMaxLimit );//7

        // Rotate Camera
        rotation = Quaternion.Euler( y , x , 0 );//8
        transform.rotation = rotation;

        aim = Quaternion.Euler( y - aimAngle , x , 0 );//10

        //Camera Position
		position = target.position - ( rotation * Vector3.forward * distance + new Vector3( 0 , -targetHeight , 0 ) );//11
		transform.position = position;

		//移动到射线击中物体位置
//		if (Physics.Linecast (target.TransformPoint (0, targetHeight, 0) , transform.position , out hit , 1 )) 
//		{
//			transform.position = hit.point + new Vector3 (0, 0.5f , 0);
//		}
//		else if (Raycast (transform.position, -transform.up).distance < 1f ) //不能穿地面
//		{
//			transform.position = hit.point + new Vector3 (0, 0.5f , 0);
//		}
    }

	RaycastHit hit;

	public RaycastHit Raycast( Vector3 origin , Vector3 forward )
	{
		if( Physics.Raycast ( origin , forward , out hit , 100f ) )
		{
			Debug.DrawLine ( transform.position , hit.point , Color.red , 1f);
		}
		return hit;
	}

    static float ClampAngle ( float angle , float min , float max )
    {
        if ( angle < -360 )
            angle += 360;
        if ( angle > 360 )
            angle -= 360;
        return Mathf.Clamp( angle , min , max );
    }
}
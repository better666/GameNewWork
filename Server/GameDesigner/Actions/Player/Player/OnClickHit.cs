using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickHit : MonoBehaviour 
{

    public Transform target = null;

    /// <summary>
    /// 返回指针位置
    /// </summary>
    static public Vector3 RaycastHitPoint()
    {
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );

        Physics.Raycast(ray, out hit);

        return hit.point;
    }

    /// <summary>
    /// 返回指针位置
    /// </summary>
    static public bool RaycastHitPoint( Transform target , KeyCode onclick = KeyCode.Mouse0 )
    {
        if( Input.GetKeyDown( onclick ) )
        {
            target.position = RaycastHitPoint();
            return true;
        }
        return false;
    }

    /// <summary>
    /// 返回指针位置
    /// </summary>
    static public void RaycastHitPoint( Transform target )
    {
        target.position = RaycastHitPoint();
    }

	// Use this for initialization
	void Start () 
    {
        target = transform;
	}
	
	// Update is called once per frame
	void LateUpdate() 
    {
        RaycastHitPoint( target , KeyCode.Mouse0 );
	}
}

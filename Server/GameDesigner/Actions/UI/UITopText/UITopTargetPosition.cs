using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UITopTargetPosition : MonoBehaviour 
{
	public Transform target = null;
	public Vector3 topPosition = new Vector3( 0 , 2.5f , 0 );
	public Vector3 rectTransformScale = new Vector3( 0.013f , 0.015f , 0.013f );

	// Use this for initialization
	void Start () 
	{
		gameObject.GetComponent<RectTransform> ().localScale = rectTransformScale;

		if( target == null )
		{
			target = transform.parent.transform;
		}
		transform.SetParent ( null );
	}
	
	// Update is called once per frame
	void LateUpdate() 
	{
		if (target == null) {
			Destroy (gameObject);
			return;
		}
		transform.position = target.position + topPosition;
	}
}

using UnityEngine;
using System.Collections;
using GameDesigner;

public class CannotMove100 : MonoBehaviour
{
	public Transform parent = null;
	public GameDesigner.PlayerSystem hitPlayer = null;
	public float cannotMove100 = 30f; 
	public GameObject cannotEffect = null;
	public Timer cannotTime = new Timer( 0 , 1f ); 

	// Use this for initialization
	void Start ()
	{
		hitPlayer.moveSpeed100 -= cannotMove100;//百分之几的移动速度
		if (cannotEffect != null) {
			Instantiate( cannotEffect , transform );
		}
	}
	
	// Update is called once per frame
	void LateUpdate()
	{
		if( cannotTime.IsTimeOut ){
			Destroy( gameObject );
		}
	}

	void OnDestroy()
	{
		hitPlayer.moveSpeed100 += cannotMove100;//百分之几的移动速度
	}

	void OnDisable() 
	{
		
	}
}


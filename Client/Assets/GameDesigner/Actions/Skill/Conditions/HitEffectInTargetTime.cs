using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDesigner;

public class HitEffectInTargetTime : MonoBehaviour {

	public GameDesigner.PlayerSystem player = null;

	[Header("当关闭或销毁物体时清空玩家的击中对象变量")]
	public bool isNullHitTarget = true;

	public Timer time = new Timer ( 0 , 1f );

	public CloseState close = CloseState.Destroy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate()
	{
		if( time.IsTimeOut ){
			if( close == CloseState.Active ){
				gameObject.SetActive ( false );
			}
			if( close == CloseState.Destroy ){
				Destroy (gameObject);
			}
			if( close == CloseState.ParentToNull ){
				gameObject.SetActive ( false );
				transform.SetParent ( null );
			}
		}
	}

	void OnDestroy()
	{
		if( isNullHitTarget & player ){
			//player.hitTarget = null;
		}
	}
}

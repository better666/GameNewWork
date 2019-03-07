using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using GameDesigner;

/// 技能关闭状态
public enum CloseState
{
	/// 删除物体
	Destroy ,
	/// 活动物体
	Active ,
	/// 当启用物体时对象父亲为空，当物体关闭后对象回归父物体
	ParentToNull ,
}

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class SkillCollider : MonoBehaviour 
{
	public Timer          time = new Timer( 0 , 1f );
	public CloseState           exitState = CloseState.Active;
	public Transform			parent = null;
	public GameDesigner.PlayerSystem 		player = null;
	public GameDesigner.PlayerSystem 		hitPlayer = null;
	public AttackProperty attackProperty = new AttackProperty();
	private ColliderProperty _colliderProperty = null;
	public ColliderProperty colliderProperty{
		get{
			if( _colliderProperty == null )
				_colliderProperty = new GameObject("ColliderProperty").AddComponent<ColliderProperty>();
			return _colliderProperty;
		}
		set{ _colliderProperty = value; }
	}

	// Use this for initialization
	void Start () 
	{
		gameObject.layer = 1;
		SphereCollider sc = GetComponent<SphereCollider>();
		sc.isTrigger = true;
		sc.radius = colliderProperty.colliderRadius;
		GetComponent<Rigidbody>().isKinematic = true;
	}

	// Update is called once per frame
	void LateUpdate()
	{
		foreach( ColliderBehaviour behaviour in colliderProperty.behaviours ){
			behaviour.OnUpdate( hitPlayer , transform );
		}

		if( colliderProperty.isMove ){
			transform.Translate ( colliderProperty.moveDirection * colliderProperty.moveSpeed );
		}

		if (!time.IsTimeOut)
			return;

		switch( exitState )
		{
		case CloseState.Destroy:
			Destroy( this.gameObject );
			break;
		case CloseState.Active:
			this.gameObject.SetActive(false);
			break;
		case CloseState.ParentToNull:
			if( parent == null ){
				Destroy( gameObject );
				return;
			}
			transform.SetParent ( parent );
			gameObject.SetActive(false);
			break;
		}
	}

	void OnTriggerEnter(Collider other) 
	{
		foreach( ColliderBehaviour behaviour in colliderProperty.behaviours ){
			behaviour.OnAllTriggerEnter( this , other , transform );
		}

		GameDesigner.PlayerSystem pm = other.GetComponent<GameDesigner.PlayerSystem>();
		if( pm != null & pm != player )
		{
			foreach( ColliderBehaviour behaviour in colliderProperty.behaviours ){
				behaviour.OnSkillTriggerEnter( this , pm , transform );
			}

			hitPlayer = pm;
			pm.Wound( player , attackProperty , colliderProperty.attackType ); 

			if( colliderProperty.hitDestroy ){
				switch( exitState )
				{
				case CloseState.Destroy:
					Destroy( gameObject );
					break;
				default :
					gameObject.SetActive(false);
					time.time = 0;
					break;
				}
			}

			if( colliderProperty.isFlyUp ){
				FlyUp fly = new GameObject("击飞协助物体").AddComponent <FlyUp> ();
				fly.transform.position = pm.transform.position;
				fly.transform.forward = transform.forward;
				pm.transform.SetParent ( fly.transform );
				fly.flyPlayer = pm;
				fly.flyUpDirection = colliderProperty.flyUpDirection;
				fly.flyUpSpeed = colliderProperty.flyUpSpeed;
				fly.flyUpTime = colliderProperty.flyUpTime;
				fly.isFlyUp = true;
			}

			if ( colliderProperty.cannotMove ) {
				CannotMove100 can = new GameObject("控制对方移动百分比").AddComponent <CannotMove100> ();
				can.hitPlayer = pm;
				can.parent = transform;
				can.transform.SetParent( hitPlayer.transform );
				can.transform.position = hitPlayer.transform.position;
				can.cannotMove100 = colliderProperty.cannotMove100;
				can.cannotEffect = colliderProperty.cannotMoveEffect;
				can.cannotTime = colliderProperty.cannotMoveTime;
			}
		}
	}
}

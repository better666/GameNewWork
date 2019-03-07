using UnityEngine;
using System.Collections;
using GameDesigner;

public class FlyUp : MonoBehaviour
{
	/// 是否击飞敌方单位
	[Header("击飞对方单位(无法移动和选择单位)")]
	public bool             isFlyUp = false;/// 被击飞时间,几秒
	[Header("击飞对方时间(无法移动和选择单位)")]
	public Timer      flyUpTime = new Timer( 0 , 0.2f );
	[Header("击飞对方方向(无法移动和选择单位)")]
	public Vector3          flyUpDirection = new Vector3( 0 , 0.5f , 0 );
	[Header("击飞对方速度(无法移动和选择单位)")]
	public float            flyUpSpeed = 1f;
	[Header("击飞单位对象")]
	public GameDesigner.PlayerSystem 	flyPlayer = null;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void LateUpdate()
	{
		if( flyPlayer == null )
		{
			Destroy ( gameObject );
			return;
		}

		if( isFlyUp ) //如果被击飞,碰撞管理器发送过来
		{
			if (!flyUpTime.IsTimeOut)
			{
				SetFlyUp( flyPlayer , false , false , true , 10000 );
				transform.Translate ( flyUpDirection * flyUpSpeed );
			}
			else
			{
				SetFlyUp( flyPlayer , true , true , false , 0 );
				isFlyUp = false;
				flyPlayer.transform.parent = null;
				Destroy ( gameObject );
			}
		}
	}

	protected void SetFlyUp( GameDesigner.PlayerSystem m_Player , bool play , bool move , bool attack , int list )
	{
		m_Player.isPlay = play;
		m_Player.isMove = move;
		m_Player.isAttack = attack;
		m_Player.skillUpIndex = list;
	}
}


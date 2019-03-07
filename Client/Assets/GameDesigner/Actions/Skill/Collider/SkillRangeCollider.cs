using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class SkillRangeCollider : MonoBehaviour 
{
    public GameDesigner.PlayerSystem player = null;
    public string playerTag = "Untagged";
	public float attackDistance = 3f;
    public List<GameDesigner.PlayerSystem> enemys = new List<GameDesigner.PlayerSystem>();

	// Use this for initialization
	void Start () 
	{
        enemys = new List<GameDesigner.PlayerSystem>();
        gameObject.layer = 0;
		SphereCollider sc = GetComponent<SphereCollider> ();
		sc.isTrigger = true;
		GetComponent<Rigidbody> ().isKinematic = true;
        player = transform.GetComponentInParent<GameDesigner.PlayerSystem>();
		if (player != null) {
			playerTag = player.tag;
		} else {
			enabled = false;
		}
	}

	// Update is called once per frame
	void LateUpdate()
	{
		if( player.attackTarget == null )
		{
			foreach( GameDesigner.PlayerSystem enemy in enemys )
			{
				if( enemy == null )
				{
					enemys.Remove ( enemy );
					return;
				}

				if ( (player.transform.position - enemy.transform.position).magnitude < attackDistance ) 
				{
					player.attackTarget = enemy;
					return;
				}
			}
		}
		else if ( (player.transform.position - player.attackTarget.transform.position).magnitude > attackDistance )
		{
			player.attackTarget = null;
		}
	}

    void OnTriggerEnter(Collider other) 
    {
		var pm = other.GetComponent<GameDesigner.PlayerSystem>();
        if( pm == null ) return;
        if( pm.transform != player.transform & pm.tag != playerTag )//不是自身玩家和队友或敌方小兵则进入
        {
			foreach( GameDesigner.PlayerSystem pms in enemys )
			{
				if( pms == pm )
				{
					return;
				}
			}
            enemys.Add( pm );
        }
    }

	#if UNITY_EDITOR
	void OnDrawGizmos ()
	{
		UnityEditor.Handles.color = Color.red;
		UnityEditor.Handles.DrawWireDisc ( transform.position , Vector3.up , attackDistance );
	}
	#endif
}

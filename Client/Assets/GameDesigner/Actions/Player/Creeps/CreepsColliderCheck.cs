using UnityEngine;
using System.Collections;

public class CreepsColliderCheck : MonoBehaviour
{
	[SerializeField]
	private GameDesigner.PlayerSystem _player = null;
	public GameDesigner.PlayerSystem player{
		get{
			if(_player == null){
				_player = transform.GetComponentInParent<GameDesigner.PlayerSystem>();
			}
			return _player;
		}
	}
	public string attackTag = "B";
	public string findManagerName = "乙方中路防御塔";
	[SerializeField]
	private DefenseTowerManager _manager = null;
	public DefenseTowerManager manager{
		get{
			if(_manager==null){
				_manager = FindComponent.FindObjectsOfTypeAll<DefenseTowerManager>(findManagerName,true);
				Debug.Log("查找失败!自动创建一个替代品");
				if(_manager==null){
					_manager = new GameObject(findManagerName).AddComponent<DefenseTowerManager>();
				}
			}
			return _manager;
		}
	}

	public float zhuijiDistance = 10;//小兵可以追击英雄的距离内

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if( player.Distance > zhuijiDistance ){
			if( manager.defenseTowers.Count == 0 ){
				enabled = false;
			}
			player.attackTarget = manager.defenseTowers[0];
		}
	}

	void OnTriggerEnter(Collider other)
	{
		var pm = other.GetComponent<GameDesigner.PlayerSystem>();
		if( pm ){
			if( pm.CompareTag( attackTag ) ){
				if( manager.defenseTowers.Count == 0 ){
					enabled = false;
				}
				player.attackTarget = pm;
			}
		}
	}
}


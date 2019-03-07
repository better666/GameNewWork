using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieInstantiate : MonoBehaviour {
	public GameObject target = null;
	GameDesigner.Timer time = new GameDesigner.Timer(3F);
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(time.IsTimeOut){
			Instantiate (target,transform.position,new Quaternion()).SetActive(true);
		}
	}
}

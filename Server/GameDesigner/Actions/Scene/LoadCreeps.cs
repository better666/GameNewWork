using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCreeps : MonoBehaviour 
{
	public GameObject[] Creeps = new GameObject[0];

	// Use this for initialization
	void Start () {
		InvokeRepeating("LaunchProjectile", 10 , 15 );
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LaunchProjectile() {
		foreach( var go in Creeps ){
			if( go ){
				Instantiate( go );
			}
		}
	}
}

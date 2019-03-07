using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseTowerManager : MonoBehaviour 
{
	public List<GameDesigner.PlayerSystem> defenseTowers = new List<GameDesigner.PlayerSystem>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		foreach( var def in defenseTowers ){
			if( def == null | def.isDeath ){
				defenseTowers.Remove( def );
			}
		}
	}
}

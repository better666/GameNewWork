using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDesigner;

namespace GameDesigner
{
	public class Enemy : PlayerSystem
	{
		public Timer lengQueTime = new Timer (0,3f);
		public Timer sikaoTime = new Timer( 0 , 2f ); 

		// Use this for initialization
		public void Start ()
		{
			gameObject.layer = 9;
			attackTarget = FindObjectOfType<Player> ();
		}
    }
}
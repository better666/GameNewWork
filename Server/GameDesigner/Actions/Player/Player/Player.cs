using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDesigner
{
	public class Player : PlayerSystem 
	{
		// Use this for initialization
		public void Start () {
			gameObject.layer = 8;
		}

		// Update is called once per frame
		void LateUpdate() 
		{
			UIPropertyDataInspector.instance.UpdateUIDateInspector( Property );
		}
	}
}
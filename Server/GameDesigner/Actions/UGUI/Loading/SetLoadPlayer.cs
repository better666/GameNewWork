using UnityEngine;
using System;


public class SetLoadPlayer : MonoBehaviour
{
	public GameDesigner.PlayerSystem player = null;


	void Start()
	{
		
	}

	void LateUpdate()
	{
		
	}

	public void LoadPlayer()
	{
		UnityEngine.Object.Instantiate(player, new UnityEngine.Vector3(-0.39f, -1.5f, 7f), new UnityEngine.Quaternion(0f, -180f, 0f, 0f));
	}
}



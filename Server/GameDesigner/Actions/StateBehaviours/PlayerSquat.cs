using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSquat : MonoBehaviour
{
	public CharacterController target = null;
	public GameDesigner.Player player = null;
	public float moveSpeed =1f;
	private float saveMoveSpeed = 3f;
	public KeyCode key = KeyCode.LeftControl;
	//public GameDesigner.Timer time = new GameDesigner.Timer(0.4f);
	public float time = 2;
	public float timeMax = 2f;
	public float squat = 1F;
	public float deltaTime = 0.01f;

	void Update()
	{
		if (Input.GetKey (key)) {
			if (time >= squat) {
				time -= deltaTime;
				target.height = time;
			}
			saveMoveSpeed = player.Property.moveSpeed;
			player.Property.moveSpeed = moveSpeed;
		} else if(Input.GetKeyUp (key)){
			target.height = timeMax;
			player.Property.moveSpeed = saveMoveSpeed;
			time = timeMax;
		}
	}
}
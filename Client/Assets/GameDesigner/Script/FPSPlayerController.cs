using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPlayerController : MonoBehaviour 
{
    public CharacterController controller;
    public float speed = 6.0F;
	public float jumpSpeed = 10F;
	public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    public void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void Update()
    {
        InputControl.Instance.isGround = controller.isGrounded;//注册是否接触地面,方便其他脚本使用
        InputControl.Instance.jump = Input.GetKeyDown(KeyCode.Space);
        if (InputControl.Instance.isGround) {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            if (InputControl.Instance.jump) {
                moveDirection *= speed / 1.5f;
                moveDirection.y = jumpSpeed;
            } else {
                moveDirection *= speed;
            }
		}
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
	}
}

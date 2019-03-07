using UnityEngine;
using System.Collections.Generic;
using GameDesigner;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MovePosition : ActionBehaviour
{
	public enum MoveModel
	{
		enterMove,
		updateMove,
		exitMove,
		animEventMove,
		idle,
	}

	public enum MotionType
	{
		MultipliedBySpeed,
		NotMultipliedBySpeed,
	}

	public MoveModel moveModel = MoveModel.enterMove; 
	public MotionType motionType = MotionType.MultipliedBySpeed;
	public Vector3 direction = new Vector3(0,0,1f);
	public float moveSpeed = 1f;

	/// <summary>
	/// 当状态进入时 ( action 状态动作管理(也是此类发送到此方法的入口点) )
	/// </summary>

	override public void OnStateEnter ( State state , StateAction action )
	{
		if (moveModel == MoveModel.enterMove) {
			if (motionType == MotionType.MultipliedBySpeed)
				stateManager.transform.Translate (direction * moveSpeed);
			else
				stateManager.transform.Translate(direction);
		}
	}

	/// <summary>
	/// 当状态每一帧调用 ( action 状态动作管理(也是此类发送到此方法的入口点) )
	/// </summary>

	override public void OnStateUpdate ( State state , StateAction action )
	{
		if (moveModel == MoveModel.updateMove) {
			if (motionType == MotionType.MultipliedBySpeed)
				stateManager.transform.Translate (direction * moveSpeed);
			else
				stateManager.transform.Translate(direction);
		}
	}

	/// <summary>
	/// 当状态结束调用 ( action 状态动作管理(也是此类发送到此方法的入口点) )
	/// </summary>

	override public void OnStateExit ( State state , StateAction action )
	{
		if (moveModel == MoveModel.exitMove) {
			if (motionType == MotionType.MultipliedBySpeed)
				stateManager.transform.Translate (direction * moveSpeed);
			else
				stateManager.transform.Translate(direction);
		}
	}

	/// <summary>
	/// 当动画事件进入 ( action 状态动作管理(也是此类发送到此方法的入口点) , 动画事件时间 )
	/// </summary>

	override public void OnAnimationEventEnter( State state , StateAction action , float animEventTime )
	{
		if (moveModel == MoveModel.animEventMove) {
			if (motionType == MotionType.MultipliedBySpeed)
				stateManager.transform.Translate (direction * moveSpeed);
			else
				stateManager.transform.Translate(direction);
		}
	}
}
using UnityEngine;
using System.Collections.Generic;
using GameDesigner;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class IsGround : TransitionBehaviour
{
	public Transform target = null;
	public Vector3 position = new Vector3 (0,-1f,0);
	public Timer time = new Timer (0.2f);
	/// <summary>
	/// 当状态每一帧调用这个方法 ( 参数CurrState ： 当前状态 , 参数 NextState ： 下一个状态 , 参数 transition ： 状态链接 , 参数 isEnterNextState ： 是否进入下一个状态 )
	/// </summary>

	override public void OnTransitionUpdate( StateManager manager , State CurrState , State DstState , Transition transition , ref bool isEnterNextState ) 
	{
		if(time.IsOutTime){
			RaycastHit hit;
			if (Physics.Raycast(target.TransformPoint(position) , -target.up , out hit, 100)) {
				Debug.DrawLine (target.TransformPoint(position),hit.point,Color.red,1);
				if(hit.distance<1f){
					isEnterNextState = true;
					time.time = 0;
				}
			}
		}
	}
}
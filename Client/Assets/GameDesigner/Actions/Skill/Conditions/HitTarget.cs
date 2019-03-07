using UnityEngine;
using System.Collections;
using GameDesigner;

public class HitTarget : MonoBehaviour
{
	public Transform target = null;
	public bool isMove = false;
	public Timer nullTargetTime = new Timer(1.5f);

	public void Update()
	{
		if(target&!isMove){
			if(nullTargetTime.IsTimeOut){
				target = null;
			}
		}
	}
}
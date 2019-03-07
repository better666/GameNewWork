using UnityEngine;
using System.Collections.Generic;
using GameDesigner;
using GameDesigner.MathOperations;

public class Enemytest : MonoBehaviour
{
	public Enemy m_Enemy2 = null;
	public System.Single m_Single3 = 0F;
	public Enemy m_Enemy = null;
	public Float m_Float10 = new Float();
	public System.Single m_Single = 2F;
	public void Start () // 当mono开始时调用一次
	{
	}
	public void LateUpdate () // 当mono每一帧执行调用
	{
		if(m_Enemy2.Hp <= m_Single3){
			if((m_Float10).value >= m_Single){
			m_Enemy.Hp = 100F;
			m_Float10.value = 0F;

		}else{
			m_Float10.AddEquals(Time.deltaTime);

		}

		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using GameDesigner;

public class EnemyMove : StateBehaviour
{
    public PlayerSystem enemy;
    public int idleID;

    private void Start()
    {
        enemy = stateManager.GetComponent<PlayerSystem>();
    }

    /// <summary>
    /// 当状态每一帧调用 ( 参数 stateMachine ： 状态机处理器 , 参数 layer ： 这个状态在这个层内 , 参数 currentState ： 当前状态 )
    /// </summary>
    override public void OnUpdateState( StateManager stateMachine , State currentState , State nextState )
	{
        if (enemy.attackTarget)
        {
            enemy.transform.LookAt(enemy.attackTarget.transform);
            enemy.transform.rotation = Quaternion.Euler(0, enemy.transform.eulerAngles.y, 0);
            enemy.transform.Translate(0, 0, enemy.moveSpeed * Time.deltaTime);
        }
        else
        {
            stateManager.OnEnterNextState(idleID);
        }
	}

    #if UNITY_EDITOR
    public override bool OnInspectorGUI(State state)
    {
        idleID = EditorGUILayout.Popup("如果攻击对象找不到进入站立状态", idleID, Array.ConvertAll(
            state.stateMachine.states.ToArray(), new Converter<State, string>(delegate (State s) {
                return state.name + " => " + s.name;
            })));
        return false;
    }
    #endif
}
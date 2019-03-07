using UnityEngine;
using GameDesigner;
using Net.Share;
using Net.Client;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// 网络玩家输入类    此类必须和NetworkMove配合使用
/// </summary>
public class NetworkInput : StateBehaviour
{
    public NetPlayer NetPlayer;

    /// <summary>
    /// 当从移动到站立确认的时候，发送站立状态几次
    /// </summary>
    public int idleInt = 5, moveId;

    public static Vector3 MoveDirection {
        get { return new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")); }
    }

    public override void OnUpdateState(StateManager stateManager, State currentState, State nextState)
    {
        if (NetPlayer.IsLocalPlayer)//如果是本地玩家才可以控制
        {
            if (!NetSendMessage.isMovement | !NetPlayer.isMove)//如果鼠标在聊天状态或其他状态,则不能进行移动
                return;

            if (MoveDirection != Vector3.zero)//如果本地键盘的4个方向键有按下则可以移动
            {
                Send(NetCmd.SceneCmd, "SyncStateMgr", NetPlayer.PlayerName, moveId);
            }
            if (NetPlayer.sendIndex < idleInt)//如果本地玩家的4个方向键没有被按下,则发送n次数据给服务器确认当前的是站立状态
            {
                NetPlayer.sendIndex++;
                Send("PlayerMove", NetPlayer.transform.position, NetPlayer.transform.rotation);
            }
        }
    }

    #if UNITY_EDITOR
    public override bool OnInspectorGUI(State state)
    {
        moveId = EditorGUILayout.Popup("移动状态", moveId, Array.ConvertAll(
            state.stateMachine.states.ToArray(), new Converter<State, string>(delegate (State s) {
                return state.name + " => " + s.name;
            })));
        NetPlayer = (NetPlayer)EditorGUILayout.ObjectField("网络玩家", NetPlayer, typeof(NetPlayer), true);
        idleInt = EditorGUILayout.IntField("发送待机数据次数", idleInt);
        return true;
    }
    #endif
}
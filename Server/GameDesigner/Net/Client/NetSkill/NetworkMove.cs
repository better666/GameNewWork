using UnityEngine;
using GameDesigner;
using System;
using Net.Client;
using Net.Share;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// 网络玩家移动类 , 此类需要配合NetworkInput类使用
/// </summary>
public class NetworkMove : StateBehaviour
{
    public NetPlayer NetPlayer;
    public int moveInt = 5, idleID;
    public float directionDistce = 0.2f;
    private Vector3 direction;

    public static Vector3 MoveDirection {
        get { return new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")); }
    }

    public override void OnUpdateState(StateManager stateManager, State currentState, State nextState)
    {
        if (NetPlayer.IsLocalPlayer) {//如果是本客户端玩家则有控制权
            Vector3 dir = MoveController.Transform3Dir(Camera.main.transform, MoveDirection);
            if (Vector3.Distance(direction, dir) > directionDistce) {//如果移动方向有改变,则发送改变后的方向值到服务器
                direction = dir;
                NetPlayer.sendIndex = 0;
            }
            if (direction == Vector3.zero) {
                Send(NetCmd.SceneCmd, "SyncStateMgr", NetPlayer.PlayerName, idleID);
            } else {
                NetPlayer.transform.rotation = Quaternion.Lerp(NetPlayer.transform.rotation, Quaternion.LookRotation(direction), 0.5f);
            }
            if (NetPlayer.sendIndex < moveInt) {//发送移动数据次数,优化网络流量
                NetPlayer.sendIndex++;
                Send("PlayerMove", NetPlayer.transform.position, NetPlayer.transform.rotation);
            }
        }
        NetPlayer.transform.Translate(Vector3.forward * Time.deltaTime * NetPlayer.moveSpeed);
    }

    #if UNITY_EDITOR
    public override bool OnInspectorGUI( State state )
	{
        idleID = EditorGUILayout.Popup( "待机状态" , idleID, Array.ConvertAll( 
            state.stateMachine.states.ToArray() , new Converter< State , string >( delegate ( State s ){
                return state.name + " => " + s.name ;
            } ) ) );
        NetPlayer = (NetPlayer)EditorGUILayout.ObjectField("网络玩家",NetPlayer,typeof(NetPlayer),true);
        moveInt = EditorGUILayout.IntField("发送移动数据次数", moveInt);
        directionDistce = EditorGUILayout.FloatField("移动的方向误差",directionDistce);
        return true;
	}
	#endif
}

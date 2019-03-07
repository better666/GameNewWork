using UnityEngine;
using System.Collections;
using GameDesigner;
using System;
using Net.Client;

public class NetRandomMove : StateBehaviour
{
    public PlayerSystem player;
    public Timer timer = new Timer(3);
    public int id;

    public override void OnUpdateState(StateManager stateManager, State currentState, State nextState)
    {
        if (ClientMgr.Instance.syncEnemyControl)
        {
            if (timer.IsTimeOut)
            {
                Send(Net.Share.NetCmd.SceneCmd, "SyncStateMgr", player.PlayerName, id);
            }
        }
    }

#if UNITY_EDITOR
    public override bool OnInspectorGUI(State state)
    {
        id = UnityEditor.EditorGUILayout.Popup("巡逻状态或站立状态", id, Array.ConvertAll(
            state.stateMachine.states.ToArray(), new Converter<State, string>(delegate (State s) {
                return state.name + " => " + s.name;
            })));
        return false;
    }
#endif
}

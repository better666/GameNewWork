using GameDesigner;
using Net.Client;
using Net.Share;
using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class NetInputEvent : StateBehaviour
{
    [Header("限制进入状态ID")]
    public int[] interruptActionIndexs = new int[1];
    [Header("按键值对")]
    public KeyCode key = KeyCode.Mouse0;
    [Header("按键模式")]
    public KeyMode keyMode;
    public NetPlayer netPlayer;

    [RPCFun]
    public void Attack(string playerName, int stateID, int actionIndex)
    {
        if ((playerName == netPlayer.PlayerName) & (stateID == state.stateID))
        {
            state.actionIndex = actionIndex;
            stateManager.OnEnterNextState(stateMachine.currState, state);
        }
    }

    private void OnDestroy()
    {
        NetBehaviour.RemoveRPCDelegate(this);
    }

    public override void OnExitState(StateManager stateManager, GameDesigner.State currentState, GameDesigner.State nextState)
    {
        netPlayer.isMove = true;
        netPlayer.sendIndex = 0;
    }

    private void Start()
    {
        NetBehaviour.AddRPCDelegate(this);
    }

    private void Update()
    {
        if (netPlayer.PlayerName != ClientMgr.Instance.playerName) {//如果不是本客户端玩家，退出
            return;
        }

        if (keyMode == KeyMode.KeyTrue)//如果是连续按键类型
        {
            if (Input.GetKey(key) & InterruptAction & !netPlayer.isDeath)
            {
                object[] pars = new object[] { ClientMgr.Instance.playerName, state.stateID, state.actionIndex };
                Send(NetCmd.SceneCmd, "Attack", pars);
            }
        }
        else//如果是按下类型
        {
            if (Input.GetKeyDown(key) & InterruptAction & !netPlayer.isDeath)
            {
                object[] objArray2 = new object[] { ClientMgr.Instance.playerName, state.stateID, state.actionIndex };
                Send(NetCmd.SceneCmd, "Attack", objArray2);
            }
        }
        
    }

    private bool InterruptAction
    {
        get
        {
            foreach (int num2 in interruptActionIndexs)
            {
                if (stateMachine.currState.stateID == num2)
                {
                    return false;
                }
            }
            return true;
        }
    }

    #if UNITY_EDITOR
    public override bool OnInspectorGUI(State state)
    {
        for (int i = 0; i < interruptActionIndexs.Length; ++i){
            interruptActionIndexs[i] = EditorGUILayout.Popup("限制进入状态", interruptActionIndexs[i],
                Array.ConvertAll( state.stateMachine.states.ToArray(), new Converter<State, string>( 
                    delegate (State s) 
                    {
                        return "不能从 " + s.name + " 中断进入 " + state.name + " => " + s.stateID;
                    }
                )));
        }
        return false;
    }
    #endif
}


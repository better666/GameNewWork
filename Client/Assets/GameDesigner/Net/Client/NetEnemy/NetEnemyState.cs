using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDesigner;
#if UNITY_EDITOR
using UnityEditor;
# endif

namespace Net.Client.NetEnemy
{
    public class NetEnemyState : StateBehaviour
    {
        public NetEnemy enemy;
        public float runDisce, attackDisce;
        public int runID, attackID;

        public override void OnUpdateState(StateManager stateManager, State currentState, State nextState)
        {
            if (enemy.attackTarget != null) {
                if (enemy.Distance < runDisce & enemy.Distance > attackDisce) {
                    stateManager.OnEnterNextState(runID);
                }
                else if (enemy.Distance < attackDisce)
                {
                    stateManager.OnEnterNextState(attackID);
                }
            }
        }

#if UNITY_EDITOR
        public override bool OnInspectorGUI(State state)
        {
            runID = EditorGUILayout.Popup("追击状态", runID, Array.ConvertAll(
                state.stateMachine.states.ToArray(), new Converter<State, string>(delegate (State s) {
                    return state.name + " => " + s.name;
                })));
            attackID = EditorGUILayout.Popup("攻击状态", attackID, Array.ConvertAll(
                state.stateMachine.states.ToArray(), new Converter<State, string>(delegate (State s) {
                    return state.name + " => " + s.name;
                })));
            return false;
        }
# endif
    }
}

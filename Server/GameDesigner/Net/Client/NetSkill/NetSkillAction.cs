namespace Net.Client
{
    using GameDesigner;
    using UnityEngine;

    public class NetSkillAction : ActionBehaviour
    {
        public AttackProperty attackProperty;
        public AttackType attackType;
        public float colliderSize = 1f;
        public NetPlayer netPlayer;

        public override void OnInstantiateSpwanEnter(State state, StateAction action, GameObject spwan)
        {
            NetSkillCollider component = spwan.GetComponent<NetSkillCollider>();
            if (component == null)
            {
                component = spwan.AddComponent<NetSkillCollider>();
            }
            component.netPlayer = netPlayer;
            component.attackProperty = netPlayer.Property + attackProperty;//基础属性+技能属性
            component.attackType = attackType;
            component.colliderSize = colliderSize;
            component.destroyTime = action.spwanTime;
        }
    }
}
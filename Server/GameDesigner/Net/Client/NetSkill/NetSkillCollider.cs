using Net.Client;
using Net.Share;
using UnityEngine;

public class NetSkillCollider : MonoBehaviour
{
    public AttackProperty attackProperty;
    public AttackType attackType;
    public new Collider collider;
    public float colliderSize = 1f;
    public float destroyTime = 1f;
    public NetPlayer netPlayer;
    public new Rigidbody rigidbody;

    private void OnTriggerEnter(Collider other)
    {
        NetPlayer enemy = other.GetComponent<NetPlayer>();
        if ((enemy != null) & (enemy != netPlayer))
        {
            enemy.attackTarget = netPlayer;
            object[] pars = new object[] { netPlayer.PlayerName, enemy.PlayerName, attackProperty, attackType };
            NetClient.Send("PlayerWound", pars);
        }
    }

    private void Start()
    {
        if (netPlayer.PlayerName == ClientMgr.Instance.playerName)
        {
            this.collider = GetComponent<Collider>();
            if (this.collider == null)
            {
                collider = gameObject.AddComponent<SphereCollider>();
            }
            this.collider.isTrigger = true;
            if (this.collider is SphereCollider)
            {
                SphereCollider collider = this.collider as SphereCollider;
                collider.radius = colliderSize;
            }
            rigidbody = GetComponent<Rigidbody>();
            if (rigidbody == null)
            {
                rigidbody = gameObject.AddComponent<Rigidbody>();
            }
            rigidbody = GetComponent<Rigidbody>();
            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;
        }
        Destroy(gameObject, destroyTime);
    }
}
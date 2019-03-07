using UnityEngine;
using GameDesigner;
using Net.Client;
using Net.Share;

public class EnemyXunLuo : StateBehaviour
{
	public PlayerSystem enemy = null;
	public Timer sikaoTime = new Timer( 0 , 2f );

    public NetTransform syncPos;

	void Start()
	{
        NetBehaviour.AddRPCDelegate(this);
		enemy = stateManager.GetComponent<PlayerSystem>();
    }

    private void OnDestroy()
    {
        NetBehaviour.RemoveRPCDelegate(this);
    }

    /// <summary>
    /// 随机巡逻
    /// </summary>
    override public void OnUpdateState( StateManager stateMachine , State currentState , State nextState )
	{
        if (ClientMgr.Instance.syncEnemyControl)
        {
            if (sikaoTime.IsTimeOut)
            {
                enemy.transform.Rotate(0, Random.Range(360, -360), 0);
                Send(NetCmd.SceneCmd, "SyncRangeRoto", enemy.name, enemy.transform.position, enemy.transform.rotation, Random.Range(0.5f, 2f));
            }
        }
		enemy.transform.Translate ( 0 , 0 , 3.0F * Time.deltaTime );
	}

    [RPCFun]
    public void SyncRangeRoto(string name, Vector3 position, Quaternion rotation, float rangeTime)
    {
        if (enemy.name != name)
            return;
        enemy.transform.rotation = rotation;
        sikaoTime.EndTime = rangeTime;
        NetTransform.SyncPosition(enemy.transform, position, 1, 0.5f);
    }
}
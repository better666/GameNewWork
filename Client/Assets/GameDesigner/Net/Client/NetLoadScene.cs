namespace Net.Client
{
    using Net.Share;
    using System.Collections.Generic;
    using UnityEngine.SceneManagement;

    public class NetLoadScene : NetBehaviour
    {
        public string sceneName = "scene";
        [UnityEngine.Header("申请服务器的场景作战人数")]
        public int sceneNumber = 50;

        public string SceneName { set { sceneName = value; } }
        public int SceneNumber { set { sceneNumber = value; } }

        public void LoadScene()
        {
            ClientMgr.Instance.clientPlayers = new List<NetPlayer>();
            SceneManager.LoadScene(sceneName);
        }

        [RPCFun]
        public void LoadScene(string sceneName, bool enemyCommend)
        {
            ClientMgr.Instance.clientPlayers = new List<NetPlayer>();
            ClientMgr.Instance.syncEnemyControl = enemyCommend;
            SceneManager.LoadScene(sceneName);
        }

        public void WaitPlayerJoin()
        {
            Send("LoadScene", sceneName, sceneNumber);
        }

        public void StopJoin()
        {
            Send("ExitScene");
        }

        public void QuitCurrentSceneLoadNewScene()
        {
            ClientMgr.Instance.clientPlayers = new List<NetPlayer>();
            SceneManager.LoadScene(sceneName);
            Send(NetCmd.SceneCmd, "OtherPlayerQuit", ClientMgr.Instance.playerName);
            Send("ExitScene");
        }
    }
}


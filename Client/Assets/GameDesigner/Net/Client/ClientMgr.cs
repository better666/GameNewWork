namespace Net.Client
{
    using Net.Share;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    /// <summary>
    /// 客户端网络管理器
    /// </summary>
    public sealed class ClientMgr : NetClient
    {
        /// <summary>
        /// 客户端玩家对象
        /// </summary>
        public NetPlayer clientPlayer;
        /// <summary>
        /// 当前房间或场景内的所有玩家集合
        /// </summary>
        public List<NetPlayer> clientPlayers = new List<NetPlayer>();
        /// <summary>
        /// 登录成功加载场景名
        /// </summary>
        public string loadSceneName = "";
        /// <summary>
        /// 登录游戏进度条
        /// </summary>
        public Slider loading;
        /// <summary>
        /// 加载进度条速度
        /// </summary>
        public float loadSpeed = 1f;
        /// <summary>
        /// 进度条遮罩物体
        /// </summary>
        public GameObject logImage;
        private int loginSuccess;
        /// <summary>
        /// 账号
        /// </summary>
        [SerializeField]
        private string account = "";
        /// <summary>
        /// 密码
        /// </summary>
        [SerializeField]
        private string password = "";
        /// <summary>
        /// 玩家名称
        /// </summary>
        public string playerName = "";
        /// <summary>
        /// 同步敌人动作，如果此客户端为true，则可当做服务器来看待
        /// </summary>
        public bool syncEnemyControl = false;

        private static ClientMgr instance;
        /// <summary>
        /// 客户端管理器实例
        /// </summary>
        public new static ClientMgr Instance => (instance ?? (instance = FindObjectOfType<ClientMgr>()));

        /// <summary>
        /// 唤醒
        /// </summary>
        public override void Awake()
        {
            instance = this;
            NetBehaviour.AddRPCDelegate(this);
            StartCoroutine(LoadingSlider());
            Application.runInBackground = true;
        }

        /// <summary>
        /// 创建玩家
        /// </summary>
        public void CreatorPlayer(Vector3 position, Quaternion rotation)
        {
            CreatorPlayer(playerName, position, rotation);
        }

        /// <summary>
        /// 创建玩家
        /// </summary>
        public void CreatorPlayer(string playerName, Vector3 position, Quaternion rotation)
        {
            NetPlayer component = Instantiate(clientPlayer.gameObject).GetComponent<NetPlayer>();
            component.transform.position = position;
            component.transform.rotation = rotation;
            component.PlayerName = playerName;
            component.name = playerName;
            clientPlayers.Add(component);
        }

        private IEnumerator LoadingSlider()
        {
            while (true) {
                if (loginSuccess == 1) {
                    logImage.SetActive(true);
                    loading.value += Time.deltaTime * loadSpeed;
                    if (loading.value >= loading.maxValue) {
                        loginSuccess = 2;
                    }
                }
                if (loginSuccess == 2) {
                    DontDestroyOnLoad(gameObject);
                    SceneManager.LoadSceneAsync(loadSceneName);
                    loginSuccess = 0;
                    break;//如果没有break则死循环 不会跳出
                }
                yield return null;
            }
        }

        /// <summary>
        /// 登录游戏
        /// </summary>
        public void LoginGame()
        {
            Connect(connected =>
            {
                if (connected)
                {
                    Send("Login", account, password);
                }
            });
        }

        /// <summary>
        /// 接收服务器登录信息
        /// </summary>
        [RPCFun]
        public void Login(bool loginState, string message, string playerName)
        {
            Debug.Log(message);
            if (loginState)
            {
                this.playerName = playerName;
                loginSuccess = 1;
                for (int i = 0; i < RPCs.Count; i++)
                {
                    if ("Login" == RPCs[i].method.Name)
                    {
                        RPCs.RemoveAt(i);
                    }
                }
            }
        }

        /// <summary>
        /// 同步怪物的AI行为
        /// </summary>
        /// <param name="control"></param>
        [RPCFun]
        public void SyncEnemyControl(bool control)
        {
            syncEnemyControl = control;
        }

        /// <summary>
        /// 当断线重连成功调用此函数
        /// </summary>
        public override void Reconnect()
        {
            object[] pars = new object[] { account, password };
            Send("Login", pars);
        }

        /// <summary>
        /// 输入账号或昵称 可当做注册和登录两种模式为一体的设计
        /// </summary>
        public void InputAccountOrPlayName(InputField input)
        {
            var name = input.text;
            account = Random.Range(100000000, 999999999).ToString();
            input.text = account;
            playerName = name;
        }

        /// <summary>
        /// 注册账号
        /// </summary>
        /// <param name="input">注册昵称输入组件</param>
        public void SignUp(InputField input)
        {
            InputAccountOrPlayName(input);
            SignUp();
        }

        /// <summary>
        /// 注册账号
        /// </summary>
        public void SignUp()
        {
            SignUp(account, password);
        }

        /// <summary>
        /// 注册账号
        /// </summary>
        public void SignUp(string account, string password)
        {
            if (playerName.Contains(" ") | playerName == "" | password.Contains(" ") | password == "")
            {
                Debug.Log("0x3");
                return;
            }
            Connect(connected =>
            {
                if (connected) {
                    Send("SignUp", playerName, account, password);
                }
            });
        }

        /// <summary>
        /// 账号输入 - UI Button事件触发
        /// </summary>
        public string AccountInput
        {
            set
            {
                account = value;
            }
        }

        /// <summary>
        /// 服务器IP - UI Button事件触发
        /// </summary>
        public string IPInput
        {
            set
            {
                host = value;
            }
        }

        /// <summary>
        /// 密码输入 - UI Button事件触发
        /// </summary>
        public string PasswordInput
        {
            set
            {
                password = value;
            }
        }

        /// <summary>
        /// 昵称输入 - UI Button事件触发
        /// </summary>
        public string PlayerNameInput
        {
            set
            {
                playerName = value;
            }
        }

        /// <summary>
        /// 输入账号和玩家昵称 - UI Button事件触发
        /// </summary>
        public string InputAccountAndName 
        {
            set {
                playerName = account = value;
            }
        }

        private void OnApplicationQuit()
        {
            Send("Quit");
            Debug.Log("退出游戏");
            Close();
        }
    }
}
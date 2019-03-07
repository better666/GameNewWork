namespace Net.Client
{
    using GameDesigner;
    using Net.Share;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    /// <summary>
    /// 网络聊天组件
    /// </summary>
    public class NetSendMessage : NetBehaviour
    {
        /// <summary>
        /// 聊天输入组件
        /// </summary>
        public InputField input;
        /// <summary>
        /// 进入聊天时限制玩家移动
        /// </summary>
        public static bool isMovement = true;
        /// <summary>
        /// 聊天文本内容
        /// </summary>
        public Text messageText;
        /// <summary>
        /// 聊天滚动矩形组件
        /// </summary>
        public ScrollRect scrollRect;
        /// <summary>
        /// 限制聊天发送速度
        /// </summary>
        private Timer timer = new Timer(0.3f);
        /// <summary>
        /// 发送过快提示
        /// </summary>
        public GameObject waitTile;

        private void Update()
        {
            timer.UpdateTime(false);
            if (Input.GetKeyDown(KeyCode.Return) & IsFocusOnInputText())
            {
                SendMessage();
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                EventSystem.current.SetSelectedGameObject(input.gameObject);
            }
        }

        private bool IsFocusOnInputText()
        {
            isMovement = true;
            return ((EventSystem.current.currentSelectedGameObject?.GetComponent<InputField>() != null) && !(isMovement = false));
        }

        /// <summary>
        /// 发送消息 UI的Button组件事件触发
        /// </summary>
        public void SendMessage()
        {
            if (input.text != "")
            {
                if (timer.IsOutTime)
                {
                    object[] pars = new object[] { $"[<color=red>{ClientMgr.Instance.playerName}</color>]说:{input.text}" };
                    Send( NetCmd.SceneCmd, "PlayerChatMessages", pars);
                    input.text = "";
                    EventSystem.current.SetSelectedGameObject(null);
                    timer.time = 0f;
                }
                else
                {
                    waitTile.SetActive(true);
                }
            }
        }

        /// <summary>
        /// 接收服务器聊天内容
        /// </summary>
        [RPCFun]
        public void PlayerChatMessages(string info)
        {
            messageText.text += info + "\n";
            scrollRect.verticalScrollbar.value = 0f;
        }
    }
}


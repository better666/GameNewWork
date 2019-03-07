using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Net.Client;
using Net.Share;

namespace Net.Client.NetChat
{
    /// <summary>
    /// 好友聊天面板
    /// </summary>
    public class ChatPanel : MonoBehaviour
    {
        /// <summary>
        /// 好友头像
        /// </summary>
        public Image friendAvatarI;
        /// <summary>
        /// 好友昵称
        /// </summary>
        public Text friendNameT;
        /// <summary>
        /// 好友状态
        /// </summary>
        public Text friendStateT;
    }
}

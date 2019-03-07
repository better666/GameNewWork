using UnityEngine;
using UnityEngine.UI;

namespace Net.Client.NetChat
{
    /// <summary>
    /// 聊天提示面板
    /// </summary>
    public class ChatReminderPanel : MonoBehaviour
    {
        /// <summary>
        /// 提示内容
        /// </summary>
        public Text promptContent;

        private static ChatReminderPanel instance;
        public static ChatReminderPanel Instance { get { return instance ?? (instance = FindComponent.FindObjectsOfTypeAll<ChatReminderPanel>()); } }

        private void Awake()
        {
            instance = this;
        }
    }
}
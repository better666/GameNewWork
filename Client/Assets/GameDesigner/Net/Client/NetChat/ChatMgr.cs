using UnityEngine;
using System.Collections.Generic;

namespace Net.Client.NetChat
{
    public class ChatMgr : MonoBehaviour
    {
        private static ChatMgr instance;
        public static ChatMgr Instance { get { return instance ?? (instance = FindObjectOfType<ChatMgr>()); } }

        public List<ChatPanel> chatPanels = new List<ChatPanel>();

        private void Start()
        {
            instance = this;
        }
    }
}
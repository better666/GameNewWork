using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Net.Client;
using UnityEngine.UI;
using Net.Share;
using Net.Client.NetChat;

public class NetFindPlayer : NetBehaviour
{
    public ChatPanel chatPanel;
    public InputField findPlayerI;

    public void FindPlayer()
    {
        Send("FindPlayerName", findPlayerI.text);
    }

    [RPCFun]
    public void FindReult(bool reult, string name, string state)
    {
        string str = "";
        if (reult) {
            var chatTarget = Instantiate(chatPanel.gameObject).GetComponent<ChatPanel>();
            chatTarget.transform.SetParent(chatPanel.transform.parent);
            //chatTarget.friendAvatarI.sprite = Resources.Load<Sprite>(path);
            chatTarget.friendNameT.text = name;
            chatTarget.friendStateT.text = state;
            ChatMgr.Instance.chatPanels.Add(chatTarget);
            str = $"成功添加{findPlayerI.text}为好友";
        }
        else
        {
            str = $"没有找到昵称为:{findPlayerI.text}的好友";
        }
        ChatReminderPanel.Instance.promptContent.text = str;
        ChatReminderPanel.Instance.gameObject.SetActive(true);
    }
}

namespace Net.Client
{
    using Net.Share;
    using UnityEngine;
    using UnityEngine.UI;

    public class NetLoginMessage : NetBehaviour
    {
        public GameObject infoTarget;
        public Text infoText;

        [RPCFun]
        public void LoginMessage(string info)
        {
            this.infoText.text = info;
            this.infoTarget.SetActive(true);
        }
    }
}


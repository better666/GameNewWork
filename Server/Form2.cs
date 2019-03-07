using GameServer;
using Net.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        int count = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (count != NetServer.clients.Count) {
                label1.Text = "当前在线人数:" + NetServer.clients.Count;
                count = NetServer.clients.Count;
            }
            label2.Text = "接收次数:" + NetServer.receiveSize;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //加载服务器
            ServerDataBase.LoadPlayerDatas();
            new ServerMgr().Run();
        }
    }
}

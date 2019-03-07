using Net.Server;
using System;
using System.Windows.Forms;

namespace GameServer
{
    public partial class Form1 : Form
    {
        public static Form1 Instance;

        public Form1()
        {
            Instance = this;
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            ServerDataBase.LoadPlayerDatas();

            for (int i = 0; i < ServerDataBase.NetPlayers.Count; ++i)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = i.ToString();
                dataGridView1.Rows[i].Cells[1].Value = ServerDataBase.NetPlayers[i].account;
                dataGridView1.Rows[i].Cells[2].Value = ServerDataBase.NetPlayers[i].password;
                dataGridView1.Rows[i].Cells[3].Value = ServerDataBase.NetPlayers[i].playerName;
                dataGridView1.Rows[i].Cells[4].Value = "玩家";
                dataGridView1.Rows[i].Cells[5].Value = "男";
                dataGridView1.Rows[i].Cells[6].Value = "1";
                dataGridView1.Rows[i].Cells[7].Value = "0";
                dataGridView1.Rows[i].Cells[8].Value = "0";
                dataGridView1.Rows[i].Cells[9].Value = "none";
                dataGridView1.Rows[i].Cells[10].Value = "none";
                dataGridView1.Rows[i].Cells[11].Value = "正常";
                dataGridView1.Rows[i].Cells[12].Value = "正常";
            }
        }

        private bool stop = false;

        private void Button1_Click(object sender, EventArgs e)
        {
            if (stop)
            {
                if (NetServer.server != null)
                {
                    NetServer.server.Close();
                    NetServer.server = null;
                }
                button1.Text = "启动服务器";
                textBox1.AppendText("服务器已停止！\r\n");
            }
            else
            {
                new ServerMgr().Start();
                button1.Text = "停止服务器";
            }
            stop = !stop;
        }

        public TextBox TextBox
        {
            get
            {
                return Instance.textBox1;
            }
        }

        public Label Label
        {
            get
            {
                return Instance.label1;
            }
        }

        public Label Label1
        {
            get
            {
                return Instance.label2;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ServerCls.path = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ServerCls.path = Application.StartupPath;

            if( ServerCls.path.Length > 0 )
            {
                backgroundWorker1.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("Alege o alta locatie");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = ServerCls.MessageCurrent + Environment.NewLine + ServerCls.path;
        }

        ServerCls server = new ServerCls();

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            server.StartServer();
        }

        private void ReceiveFileButton_Click(object sender, EventArgs e)
        {

        }
    }
}

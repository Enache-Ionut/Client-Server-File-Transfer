using Server_Client_File_Transfer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Browse_Click(object sender, EventArgs e)
        {
            FileDialog fd = new OpenFileDialog();

            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ClientCls.SendFile(fd.FileName);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Message.Text = ClientCls.MessageCurrent;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

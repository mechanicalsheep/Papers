extern alias newt;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerForm
{
    public partial class ServerForm : Form
    {
        ServerNet serverNet;
        public ServerForm()
        {
            InitializeComponent();
            serverNet = new ServerNet(this);
        }

        public void writeline(string message)
        {
            //added an invoke in case the writeline is used by a different thread.
            if(lb_output.InvokeRequired)
           lb_output.Invoke(new Action(() => lb_output.Items.Add(message)));
            else
            {
                lb_output.Items.Add(message);
                
            }

        }

        private void btn_Scan_Click(object sender, EventArgs e)
        {
            lb_onlineComp.Items.Clear();
            List<string> onlineList = serverNet.GetConnection();
            string[] ipPort;
                foreach (var connection in onlineList)
                {
                ipPort = connection.Split(':');

                string key= serverNet.sendMessage(ipPort[0], Convert.ToInt32(ipPort[1]), "GIMME WHAT YOU GOT");
                lb_onlineComp.Items.Add(key);
                }
            
        }
        public void getComputerKey(string ip, int port)
        {

        }

        private void btn_send_Click(object sender, EventArgs e)
        {

            
        }
    }
}

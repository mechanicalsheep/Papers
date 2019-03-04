extern alias newt;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
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
           lb_output.Invoke(new Action(() =>
           {
               lb_output.Items.Add(message);
               lb_output.SelectedIndex = lb_output.Items.Count - 1;
               lb_output.SelectedIndex = -1;
           }));
            else
            {
                lb_output.Items.Add(message);
                lb_output.SelectedIndex = lb_output.Items.Count - 1;
                lb_output.SelectedIndex = -1;

            }

        }

        private void btn_Scan_Click(object sender, EventArgs e)
        {
            lb_onlineComp.Items.Clear();
            List<string> onlineList = serverNet.GetConnection();
            Dictionary<string, string> onlineAddresses = new Dictionary<string, string>();
            string[] ipPort;
                foreach (var connection in onlineList)
                {
                ipPort = connection.Split(':');

                string key= serverNet.sendMessage(ipPort[0], Convert.ToInt32(ipPort[1]), "GIMME WHAT YOU GOT");
                onlineAddresses.Add(key, connection);
                lb_onlineComp.Items.Add(key+" " +connection);
                
                }
            
            
        }
        public void getComputerKey(string ip, int port)
        {

        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            NetworkCredential networkCredential = new NetworkCredential()
            {
                UserName = "administrator",
                Password = tb_password.Text,
                Domain = tb_domain.Text,

            };
            //int index=lb_onlineComp.SelectedIndex;
            
            Console.WriteLine("Key is: " + lb_onlineComp.SelectedItem.ToString().Split(' ').First());
            string ipWithPort = lb_onlineComp.SelectedItem.ToString().Split(' ').Last();
            string ip = ipWithPort.Split(':').First();
            int port = Convert.ToInt32(ipWithPort.Split(':').Last());
            CommandInfo commandInfo = new CommandInfo(networkCredential, tb_command.Text);

            serverNet.sendCommand(ip, port, commandInfo);

            //serverNet.sendCommand()
            
        }
    }
}

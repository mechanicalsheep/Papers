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
        ServerDataHandler data;
        public ServerForm()
        {
            InitializeComponent();
            serverNet = new ServerNet(this);
            data = new ServerDataHandler();
        }

        public void writeline(string message)
        {
            //added an invoke in case the writeline is used by a different thread.
            if (lb_output.InvokeRequired)
                lb_output.Invoke(new Action(() =>
                {
               // lb_output.ForeColor = Color.Green;
               lb_output.Items.Add(message);
                    lb_output.SelectedIndex = lb_output.Items.Count - 1;
                    lb_output.SelectedIndex = -1;
                }));
            else
            {
                // lb_output.ForeColor = Color.Green;
                lb_output.Items.Add(message);
                lb_output.SelectedIndex = lb_output.Items.Count - 1;
                lb_output.SelectedIndex = -1;

            }


        }
        public void ScanForConnections()
        {
            if (lb_onlineComp.InvokeRequired)
            {
                lb_onlineComp.Invoke(new Action(() => {


                    lb_onlineComp.Items.Clear();
                    List<string> onlineList = serverNet.GetConnections();
                    Dictionary<string, string> onlineAddresses = new Dictionary<string, string>();
                    string[] ipPort;
                    foreach (var connection in onlineList)
                    {
                        ipPort = connection.Split(':');

                        string key = serverNet.GetKeyCommad(ipPort[0], Convert.ToInt32(ipPort[1]), "GIMME WHAT YOU GOT");
                        onlineAddresses.Add(key, connection);
                        lb_onlineComp.Items.Add(key + " " + connection);

                    }
                    if (lb_onlineComp.Items.Count > 0)
                    {
                        lb_onlineComp.SelectedIndex = 0;
                    }

                }));
            }
            else
            {
                lb_onlineComp.Items.Clear();
                List<string> onlineList = serverNet.GetConnections();
                Dictionary<string, string> onlineAddresses = new Dictionary<string, string>();
                string[] ipPort;
                foreach (var connection in onlineList)
                {
                    ipPort = connection.Split(':');

                    string key = serverNet.GetKeyCommad(ipPort[0], Convert.ToInt32(ipPort[1]), "GIMME WHAT YOU GOT");
                    onlineAddresses.Add(key, connection);
                    lb_onlineComp.Items.Add(key + " " + connection);

                }
                if (lb_onlineComp.Items.Count > 0)
                {
                    lb_onlineComp.SelectedIndex = 0;
                }
            }


        }
        private void btn_Scan_Click(object sender, EventArgs e)
        {
            ScanForConnections();
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            if (lb_onlineComp.SelectedItems.Count > 0)
            {
                foreach (var item in lb_onlineComp.SelectedItems)
                {

                    NetworkCredential networkCredential = new NetworkCredential()
                    {
                        UserName = tb_username.Text,
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
        public void StartManifest(Computer Computer)
        {
            Console.WriteLine("already have this computer info.");
            Computer computer = Computer;
            Computer savedComputer = data.GetComputer(computer.name);
            if (savedComputer.uniqueKey!=computer.uniqueKey)
            {
                //they are different computer with possibly same name?
                computer.machineNote = "There is possibly another computer with the same name as " + computer.name;
                data.SaveObjectData(computer, computer.name, "WarningComps");
            }
            else if (computer.Equals(savedComputer))
            {
                Console.WriteLine("the computers are equal");
                //don't do anything, we already have the computer snapshot and nothing changed.
            }
            else
            {
                Console.WriteLine("things have been changed");
                Manifest manifest = new Manifest(savedComputer, computer);
                Console.WriteLine("Manifest computer date is: "+manifest.dateTime);
                string[] datestring = computer.dateTime.Split(':');
                data.SaveObjectData(manifest, computer.name + "-" + datestring[0]+datestring[1],"Manifest\\"+computer.name);
                data.SaveObjectData(computer, computer.name, "Computers");
            }

            Console.WriteLine("got data from " + savedComputer.name);

        }
        public void getComputerData(string ipWithPort)
        {

            string ip = ipWithPort.Split(':').First();
            int port = Convert.ToInt32(ipWithPort.Split(':').Last());

            Computer computer = serverNet.GetComputer(ip, port);
            writeline("Computer: " + computer.name);
            writeline("OS: " + computer.OS);

            if (data.ComputerExists(computer.name))
            {
                //add to the manifest
                StartManifest(computer);
            }
            else
            {
                data.SaveObjectData(computer, computer.name, "Computers");
            }
        }
        private void btn_getComputer_Click(object sender, EventArgs e)
        {
            if (lb_onlineComp.SelectedItems.Count > 0)
            {
                foreach (var item in lb_onlineComp.SelectedItems)
                {
                    getComputerData(item.ToString().Split(' ').Last());
                }
            }

        }
    
    }
    
    
}

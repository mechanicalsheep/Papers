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
            //Console.WriteLine("hellow?");
            InitializeComponent();
            serverNet = new ServerNet(this);
            data = new ServerDataHandler();
            //lv_computers.View = View.Details;
            lv_computers.Columns.Add("Computer Name",120,HorizontalAlignment.Left);
            lv_computers.Columns.Add("IP",120);
            lv_computers.Columns.Add("Port");

            ListViewItem item = new ListViewItem(new[] { "hi", "0.0.0.0" });
           // Console.WriteLine("ip is: "+item.SubItems[1]);
           /* ListViewItem item1 = new ListViewItem(new[] { "hi1", "1.0.0.0" });
            ListViewItem qwer = new ListViewItem(new[] { "computex", "1.0.0.0" });
            lv_computers.Items.Add(item);
            lv_computers.Items.Add(item1);
            lv_computers.Items.Add(qwer);
            Console.WriteLine("IndexOfKey = ");
            //Console.WriteLine("LV_COMPUTERS COUNT "+lv_computers.Items.Count);
            lv_computers.Items.Remove(lv_computers.FindItemWithText("computex"));*/


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
        public void AddOnlineComputer(string computerName,string IP, string port)
        {
          

            if (lv_computers.InvokeRequired)
            {
                lv_computers.Invoke(new Action(() => {
                    lv_computers.Items.Add(new ListViewItem(new[]{computerName, IP, port }));
                }));
                
            }
            else
            {
                lv_computers.Items.Add(new ListViewItem(new[] { computerName, IP }));
            }
        }
        public void RemoveOfflineComputer(string item)
        {

            if (lv_computers.InvokeRequired)
            {
                lv_computers.Invoke(new Action(() => {

                    lv_computers.Items.Remove(lv_computers.FindItemWithText(item));

                }));

            }
            else
            {
                lv_computers.Items.Remove(lv_computers.FindItemWithText(item));

            }
        }
       /* public void ScanForConnections()
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

                        string key = serverNet.GetComputerName(ipPort[0], Convert.ToInt32(ipPort[1]), "GIMME WHAT YOU GOT");
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

                    string key = serverNet.GetComputerName(ipPort[0], Convert.ToInt32(ipPort[1]), "GIMME WHAT YOU GOT");
                    onlineAddresses.Add(key, connection);
                    lb_onlineComp.Items.Add(key + " " + connection);

                }
                if (lb_onlineComp.Items.Count > 0)
                {
                    lb_onlineComp.SelectedIndex = 0;
                }
            }


        }*/
       /* private void btn_Scan_Click(object sender, EventArgs e)
        {
            ScanForConnections();
        }*/

        private void btn_send_Click(object sender, EventArgs e)
        {
            /* if (lb_onlineComp.SelectedItems.Count > 0)
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

             }*/
            if (lv_computers.SelectedItems.Count > 0)
            {
                foreach (var item in lv_computers.SelectedItems)
                {
                    ListViewItem lvi = item as ListViewItem; 
                    NetworkCredential networkCredential = new NetworkCredential()
                    {
                        UserName = tb_username.Text,
                        Password = tb_password.Text,
                        Domain = tb_domain.Text,

                    };
                    //int index=lb_onlineComp.SelectedIndex;

                    //Console.WriteLine("Key is: " + lb_onlineComp.SelectedItem.ToString().Split(' ').First());
                    //string ipWithPort = lb_onlineComp.SelectedItem.ToString().Split(' ').Last();
                    string ip = lvi.SubItems[1].Text;
                    int port = Convert.ToInt32(lvi.SubItems[2].Text);
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
        public void getComputerData(string IP, int Port)
        {

            string ip = IP;
            int port = Convert.ToInt32(Port);

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
            if (lv_computers.SelectedItems.Count>0)
            {
                foreach(var item in lv_computers.SelectedItems)
                {
                    ListViewItem lvi = item as ListViewItem;
                    Console.WriteLine("lvi is: IP:"+lvi.SubItems[1].Text);
                    getComputerData(lvi.SubItems[1].Text, Convert.ToInt32(lvi.SubItems[2].Text));
                }
               /* foreach (var item in lb_onlineComp.SelectedItems)
                {
                    
                    getComputerData(item.ToString().Split(' ').Last());
                }*/
            }

        }
    
    }
    
    
}

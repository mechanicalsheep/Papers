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
        public Dictionary<string,Computer> computers;
        public ServerForm()
        {
            //Console.WriteLine("hellow?");
            InitializeComponent();
            serverNet = new ServerNet(this);
            computers = new Dictionary<string, Computer>();
            data = new ServerDataHandler();
            initializeComputers();

            lv_computers.Columns.Add("Computer Name",120,HorizontalAlignment.Left);
            lv_computers.Columns.Add("IP",120);
            lv_computers.Columns.Add("Port");
            lv_computers.Columns.Add("Unique Key");
           

        }
      public void addtoListView(ListViewItem listViewItem)
        {
            if (lv_computers.InvokeRequired)
            {
                lv_computers.Invoke(new Action(()=>
                {
                    lv_computers.Items.Add(listViewItem);
                }));
            }
            else
            {
                lv_computers.Items.Add(listViewItem);
            }
        }
        public void initializeComputers()
        {
            if (lv_computers.InvokeRequired)
            {
                lv_computers.Invoke(new Action(() =>
                {
            lv_computers.Items.Clear();
                    computers = data.GenerateComputerList();
                    writeline("Computer list Count: " + computers.Count);
                    foreach (var computer in computers)
                    {
                        ListViewItem lvi = new ListViewItem(new[] { computer.Value.name, computer.Value.ip, "", computer.Key });
                       /* if (serverNet.isAlive(computer.Value.ip))
                            lvi.ForeColor = Color.Blue;
                        else*/
                            lvi.ForeColor = Color.Gray;

                        //if (NetworkCommsDotNet.NetworkComms.)
                        lv_computers.Items.Add(lvi);
                    }
                }));
            }
            else
            {
                lv_computers.Items.Clear();
                computers = data.GenerateComputerList();
                writeline("Computer list Count: " + computers.Count);
                foreach(var computer in computers)
                {
                    ListViewItem lvi = new ListViewItem(new[] { computer.Value.name, computer.Value.ip,"", computer.Key});
                    lvi.ForeColor = Color.Gray;
                    lv_computers.Items.Add(lvi);
                }

            }

        }
       /* public void updateList(string ComputerName, string ip, string port)
        {
            Computer match = computers.Where(x => x.name == ComputerName);
            if (match!= null)
            {
                Console.WriteLine("MAtch is : " + (Computer)match);
                Computer tempComp=computers[computers.IndexOf(match as Computer)];
                tempComp.ip = "haha";
                data.SaveObjectData(tempComp, tempComp.name, "Computers");
            }
        }*/
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
        public void setOnline(string key, string ip, string port)
        {
            if (lv_computers.InvokeRequired)
            {
                lv_computers.Invoke(new Action(() => {

                    lv_computers.FindItemWithText(key).ForeColor = Color.Green;
                    lv_computers.FindItemWithText(key).SubItems[1].Text = ip;
                    lv_computers.FindItemWithText(key).SubItems[2].Text = port;


                }));

            }
            else
            {
               
                lv_computers.FindItemWithText(key).ForeColor = Color.Green;
                lv_computers.FindItemWithText(key).SubItems[1].Text = ip;
                lv_computers.FindItemWithText(key).SubItems[2].Text = port;

            }
        }
        public void setOnline(string key)
        {
            if (lv_computers.InvokeRequired)
            {
                lv_computers.Invoke(new Action(() => {

                    //lv_computers.Items.Remove(lv_computers.FindItemWithText(item));
                    lv_computers.FindItemWithText(key).ForeColor = Color.Green;

                }));

            }
            else
            {
                lv_computers.FindItemWithText(key).ForeColor = Color.Green;

            }
        }

        public void setOffline(string key)
        {
            if (lv_computers.InvokeRequired)
            {
                lv_computers.Invoke(new Action(() => {

                    //lv_computers.Items.Remove(lv_computers.FindItemWithText(item));
                    lv_computers.FindItemWithText(key).ForeColor = Color.Gray;

                }));

            }
            else
            {
                lv_computers.FindItemWithText(key).ForeColor = Color.Gray;

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
       
        private void btn_send_Click(object sender, EventArgs e)
        {
            
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
                   
                    string ip = lvi.SubItems[1].Text;
                    int port = Convert.ToInt32(lvi.SubItems[2].Text);
                    CommandInfo commandInfo = new CommandInfo(networkCredential, tb_command.Text);

                    serverNet.sendCommand(ip, port, commandInfo);

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
             
            }

        }

        private void lv_computers_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach(ListViewItem item in lv_computers.SelectedItems)
            {
                if (item.ForeColor == Color.Gray)
                {
                    item.Selected = false;
                }
            }
        }
    }
    
    
}

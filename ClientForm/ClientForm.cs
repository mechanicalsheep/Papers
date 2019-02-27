extern alias newt;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace ClientForm
{
    public partial class ClientForm : Form
    {
        string currentPath; 
        string initFile;

        //testing
        string ip;
        int port;
        System.Timers.Timer aTimer;


        ClientNet clientNet;


        public ClientForm()
        {
            ip = "192.168.11.105";
            port = 11111;

            InitializeComponent();
            currentPath = Directory.GetCurrentDirectory();
            initFile = currentPath + @"\initial.json";

            clientNet = new ClientNet(this);
            generateUniqueKey();
            aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(sendMessage);
            aTimer.Interval = 5000;
            aTimer.Enabled = true;


        }

        private void sendMessage(object sender, ElapsedEventArgs e)
        {
            try
            {
                clientNet.sendMessage(ip, port, "still alive!");
            }
            catch (Exception)
            {
                aTimer.Interval=10000;
                writeline("unable to send, awaiting 10 seconds");
            }
        }

        public void writeline(string message)
        {
            if (lb_output.InvokeRequired)
            {
                lb_output.Invoke(new Action(() =>  lb_output.Items.Add(message)));
            }
            else
            lb_output.Items.Add(message);
        }

        //todo: save and read all in different class as a data handler
        void generateUniqueKey()
        {
            

            if (!File.Exists(initFile))
            {
                writeline("file: " + initFile + " does not exist!");
                Guid g = Guid.NewGuid();
                string GuidString = Convert.ToBase64String(g.ToByteArray());
                GuidString = GuidString.Replace("=", "");
                GuidString = GuidString.Replace("+", "");
                var output = newt.Newtonsoft.Json.JsonConvert.SerializeObject(GuidString);

                File.WriteAllText(initFile, output);
            }

           
        }
        public string getUniqueKey()
        {

            try
            {
                var inputs = File.ReadAllText(initFile);
                string uniqueKey = newt.Newtonsoft.Json.JsonConvert.DeserializeObject<string>(inputs);
                return uniqueKey;
                
            }
            catch (Exception err)
            {
                writeline("Error sending key: " + err.Message);
                return null;
            }
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            writeline("Sending Unique Key to server.");
            clientNet.sendMessage(tb_IP.Text, Convert.ToInt32(tb_Port.Text), getUniqueKey());
        }

        private void btn_shutdown_Click(object sender, EventArgs e)
        {
            clientNet.CloseNetwork();
        }
    }

}

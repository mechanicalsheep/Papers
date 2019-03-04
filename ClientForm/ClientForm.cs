﻿extern alias newt;

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Client
{
    public partial class ClientForm : Form
    {
        

        //testing
        string ip;
        int port;
        System.Timers.Timer aTimer;

        Computer computer;
        ClientNet clientNet;
        ClientDataHandler data;

        public ClientForm()
        {
           
            InitializeComponent();

            computer = new Computer(getComputerName());
            data = new ClientDataHandler(computer);
            ip = "192.168.8.100";
            port = 11111;
          
            GetComputerData();
            
            data.SaveComputerData();

            clientNet = new ClientNet(this);


            
            aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(sendMessage);
            aTimer.Interval = 5000;
            aTimer.Enabled = true;
           

        }

        void GetComputerData()
        {
           
            computer.uniqueKey = getUniqueKey();
            computer.OS = getOS();
            computer.software = getInstallations();
          
            

        }

        public string getComputerName()
        {
            string compName = Environment.MachineName;
            return compName;

        }
        #region OS

        public string HKLM_GetString(string path, string key)
        {
            try
            {
                RegistryKey rk = Registry.LocalMachine.OpenSubKey(path);
                if (rk == null) return "";
                return (string)rk.GetValue(key);
            }
            catch { return ""; }
        }

        public string getOS()
        {
            string ProductName = HKLM_GetString(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ProductName");
            string CSDVersion = HKLM_GetString(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CSDVersion");
            if (ProductName != "")
            {
                return (ProductName.StartsWith("Microsoft") ? "" : "Microsoft ") + ProductName +
                            (CSDVersion != "" ? " " + CSDVersion : "");
            }
            return "";
        }
        #endregion

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
                lb_output.Invoke(new Action(() =>
                {
                    lb_output.Items.Add(message);
                    lb_output.SelectedIndex = lb_output.Items.Count - 1;
                    lb_output.SelectedIndex = -1;
                }));

            }
            else
            {
                lb_output.Items.Add(message);
                lb_output.SelectedIndex = lb_output.Items.Count - 1;
                lb_output.SelectedIndex = -1;
            }
            
        }

       
        string generateUniqueKey()
        {
                Guid g = Guid.NewGuid();
                string GuidString = Convert.ToBase64String(g.ToByteArray());
                GuidString = GuidString.Replace("=", "");
                GuidString = GuidString.Replace("+", "");
                data.saveInitFile(GuidString);
            
            return GuidString;

        }
        public string getUniqueKey()
        {
            string initFile = Directory.GetCurrentDirectory() + "\\init.json";
            if (!File.Exists(initFile))
            {
                writeline("no initial.json, generating Key...");
                return generateUniqueKey();
            }
            try
            {

                var inputs = File.ReadAllText(initFile);
                string uniqueKey = newt.Newtonsoft.Json.JsonConvert.DeserializeObject<string>(inputs);
                return uniqueKey;
                
            }
            catch (Exception err)
            {
                writeline("Error getting key: " + err.Message);
                return null;
            }
        }

        public List<string> getInstallations()
        {
            List<string> software = new List<string>();

            Process process = new Process();
            process.StartInfo = new ProcessStartInfo();
            string uKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            RegistryKey rk = Registry.LocalMachine.OpenSubKey(uKey);

            foreach (string key in rk.GetSubKeyNames())
            {
                RegistryKey subKey = rk.OpenSubKey(key);
                try
                {
                    if (subKey.GetValue("UninstallString") != null)
                    {
                        string program = subKey.GetValue("DisplayName").ToString();
                        
                        software.Add(program);

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception Error: " + ex);
                }

            }

            return software;
        }
        private void btn_send_Click(object sender, EventArgs e)
        {
            writeline("Sending Unique Key to server.");
        }

        private void btn_shutdown_Click(object sender, EventArgs e)
        {
            clientNet.CloseNetwork();
        }

        private void btn_Install_Click(object sender, EventArgs e)
        {
            Installer installer = new Installer();
            List<string> output = installer.Install(tb_installer_path.Text);
            foreach(var outs in output)
                if(outs!=null)
            writeline("Output is "+outs);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = "C:\\Users";
            
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // MessageBox.Show("You selected: " + dialog.FileName);
                tb_installer_path.Text = dialog.FileName;
            }
        }
    }

}

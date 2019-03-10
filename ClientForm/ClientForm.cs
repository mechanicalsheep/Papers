﻿extern alias newt;

using Microsoft.Win32;
using System;
using System.Collections.Generic;

using System.Diagnostics;
using System.IO;

using System.Timers;
using System.Windows.Forms;
using System.Net;

namespace Client
{
    public partial class ClientForm : Form
    {
        

        //testing
        string ip;
        int port;
        System.Timers.Timer aTimer;

        public Computer computer;
        ClientNet clientNet;
        ClientDataHandler data;
        //Installer installer;
        public ClientForm()
        {
           
            InitializeComponent();

            computer = new Computer(getComputerName());
            data = new ClientDataHandler();
            ip = "192.168.11.105";
            //ip = "192.168.8.100";
            port = 11111;
            
            clientNet = new ClientNet(this);
            GetComputerData();
            
            data.SaveObjectData(computer, computer.name, "ref");

            
            tb_installer_path.KeyDown += Tb_installer_path_KeyDown;
            
            aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(stillAlive);
            aTimer.Interval = 5000;
            aTimer.Enabled = true;
           

        }

        private void Tb_installer_path_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                choco(tb_command.Text, tb_username.Text, tb_password.Text, tb_domain.Text);
            }
        }

        void GetComputerData()
        {
           
            computer.uniqueKey = getUniqueKey();
            computer.OS = getOS();
            computer.softwares = getInstallations();
            computer.chocoSoftwares = getChocoInstalls();
            computer.dateTime = getDateTime();
          
            

        }
        public string getDateTime()
        {
            DateTime dateTime = DateTime.Now;
            return dateTime.ToString("yyyy'-'MM'-'dd h:mmtt");
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
        public void sendMessage(string message)
        {
            if (message != null)
            clientNet.sendMessage(ip,port,message);
        }
        private void stillAlive(object sender, ElapsedEventArgs e)
        {
            try
            {
                clientNet.sendAlive(ip, port);
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
        public Computer getSavedComputerData()
        {
            return data.GetComputerData();
        }
       
        string generateUniqueKey()
        {
                Guid g = Guid.NewGuid();
                string GuidString = Convert.ToBase64String(g.ToByteArray());
                GuidString = GuidString.Replace("=", "");
                GuidString = GuidString.Replace("+", "");
            //data.saveInitFile(GuidString);
            data.SaveObjectData(GuidString, "init", "ref");
            
            return GuidString;

        }
        public string getUniqueKey()
        {
            string initFile = Directory.GetCurrentDirectory() + "\\ref\\init.json";
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
        public void choco(string Command, string username, string password, string domain)
        {
           Installer installer = new Installer(this);
            if (username == "")
                username = "administrator";
            NetworkCredential credential = new NetworkCredential(username, password, domain);
            List<string> output = installer.Install(Command, credential);
            foreach (var outs in output)
                if (outs != null)
                    writeline(outs);
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
        public List<string> getChocoInstalls()
        {
           Installer installer = new Installer(this);
            List<string> chocolateySoftwares = new List<string>();
            chocolateySoftwares = installer.getChocolateyInstallations();
            Console.WriteLine("chocolatey installer");
            foreach (var chocolate in chocolateySoftwares)
                Console.WriteLine(chocolate);
            return chocolateySoftwares;
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
            choco(tb_command.Text, tb_username.Text, tb_password.Text, tb_domain.Text);
            
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

        private void btn_Note_Click(object sender, EventArgs e)
        {
            computer.note = tb_note.Text;
            computer.dateTime = getDateTime();
            data.SaveObjectData(computer, computer.name, "ref");
            writeline("note saved!");
        }
    }

}

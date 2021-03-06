﻿extern alias newt;

using Microsoft.Win32;
using System;
using System.Collections.Generic;

using System.Diagnostics;
using System.IO;
using System.Management;
using System.Timers;
using System.Windows.Forms;
using System.Net;

namespace Client
{
    public partial class ClientForm : Form
    {


    
        Settings settings = new Settings();
        string settingPath;
        string SettingsFilename;
        string settingFolder;
        string ip;
        string path;
        int port;
        System.Timers.Timer aTimer;
        string fullSettingFilePath;

        public Computer computer;
        ClientNet clientNet;
        ClientDataHandler data;
        ManagementClass mc = new ManagementClass();
  

        public ClientForm()
        {

            path = Directory.GetCurrentDirectory();
            settingFolder="settings";
            SettingsFilename = "Settings";
            fullSettingFilePath = $"{path}\\{settingFolder}\\{SettingsFilename}.json";


            mc.Path = new ManagementPath("Win32_ComputerSystem");
            InitializeComponent();

            computer = new Computer(getComputerName());
            data = new ClientDataHandler();

            ///server-shady as server
            //ip = "192.168.11.193";

            /// SHADY as Server
            ip = "192.168.11.105";

            ///MSI as server
            //ip = "192.168.8.100";

            port = 11111;
            
            clientNet = new ClientNet(this);
            GetComputerData();

            StartManifest(computer);
            
            data.SaveObjectData(computer, computer.uniqueKey, "ref");
           

            tb_installer_path.KeyDown += Tb_installer_path_KeyDown;
            
            aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(stillAlive);
            aTimer.Interval = 5000;
            aTimer.Enabled = true;

            cb_groups.Text = data.GetSettings(fullSettingFilePath).group;
          
        }

        private void Tb_installer_path_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                choco(tb_command.Text, tb_username.Text, tb_password.Text, tb_domain.Text);
            }
        }
        string GetComputerModel()
        {
            ManagementObjectCollection infos = mc.GetInstances();
            string model="";
            Console.WriteLine("infos.count = " + infos.Count);
            foreach (var info in infos)
            {
                
              model=info["Model"].ToString();
            }
            return model;
        }
        public void StartManifest(Computer Computer)
        {
          //  Console.WriteLine("already have this computer info.");
            Computer computer = Computer;
            string path = Directory.GetCurrentDirectory() + "\\ref\\" + computer.uniqueKey+".json";
            if (File.Exists(path))
            {
            Computer savedComputer = data.GetComputer(path);
            //if (savedComputer.uniqueKey != computer.uniqueKey)
            //{
                //they are different computer with possibly same name?
                //computer.machineNote = "There is possibly another computer with the same name as " + computer.name;
                //data.SaveObjectData(computer, computer.name, "WarningComps");
            //}
            if (computer.Equals(savedComputer))
            {
                Console.WriteLine("the computers are equal");
                //don't do anything, we already have the computer snapshot and nothing changed.
            }
            else
            {
                Console.WriteLine("things have been changed");
                Manifest manifest = new Manifest(savedComputer, computer);
                Console.WriteLine("Manifest computer date is: " + manifest.dateTime);
                string[] datestring = computer.dateTime.Split(':');
                data.SaveObjectData(manifest, computer.uniqueKey + "-" + datestring[0] + datestring[1], "Manifest\\" + computer.uniqueKey);
                data.SaveObjectData(computer, computer.uniqueKey, "ref");
            }

            Console.WriteLine("got data from " + savedComputer.name);

            }

        }
        string GetRam()
        {
            string ram = "";

            ManagementObjectCollection infos = mc.GetInstances();
            foreach (var info in infos)
            {

                ram = info["TotalPhysicalMemory"].ToString();
            }
            double gbram = Convert.ToDouble(ram) / 1073741824; ;
           
            Console.WriteLine("RAM: " + Math.Round(gbram).ToString());
            return Math.Ceiling(gbram).ToString();
            //return gbram.ToString();

        }
        string GetProcessor()
        {
            ManagementClass managementClass = new ManagementClass();
            managementClass.Path = new ManagementPath("Win32_OperatingSystem");
            
             ManagementObjectCollection infos = managementClass.GetInstances();
            string processor="";
            Console.WriteLine("infos.count = " + infos.Count);
            foreach (var info in infos)
            {
                
              processor=info["OSArchitecture"].ToString();
            }
            writeline(processor);
            return processor;
        }


       public void setGroup(string group)
        {
            Settings tempSetting = data.GetSettings(fullSettingFilePath);
            tempSetting.group = group;
            data.SaveObjectData(tempSetting,SettingsFilename,"settings");
            getGroup();
        }
        string GetUser()
        {
            string user= System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            writeline("current user is: " + user);
            return user;

            //the following returns cached user's in some cases that are not currently logged in.
            //return Environment.UserName; 
        }
        void GetComputerData()
        {
           
            computer.uniqueKey = getUniqueKey();
            computer.OS = getOS();
            computer.softwares = getInstallations();
            //-- using getChocoInstallFiles instead to just read the files instead of running choco process to list installed. 
            //-- computer.chocoSoftwares = getChocoInstalls();
            computer.chocoSoftwares = getChocoInstallFiles();
            computer.dateTime = getDateTime();
            computer.ip = clientNet.getIP();
            computer.username = GetUser();
            computer.model = GetComputerModel();
            computer.ram = GetRam();
            computer.group = getGroup();
            computer.processor = GetProcessor();
            if (computer.softwares.Contains("AnyDesk"))
            {
                //Installer installer = new Installer(this);
                getAnyDeskKey();
                //writeline("COMPUTER.ANYDESKKEY= " + computer.anyDesk);
               //writeline( getAnyDeskKey());
            }
            writeline("Computer group is: " + computer.group);
            Console.WriteLine("Processor is: "+computer.processor);


        }
        void getAnyDeskKey()
        {
            string anyDeskKey="";

            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;


           
            if(computer.processor=="64-bit")
                p.StartInfo.FileName = Directory.GetCurrentDirectory()+"\\tools\\anydeskid.bat";
            else
                p.StartInfo.FileName = Directory.GetCurrentDirectory() + "\\tools\\anydeskidx86.bat";


            

            p.OutputDataReceived += new DataReceivedEventHandler((s, e) =>
            {

               
                if (e.Data != null || e.Data == "")
                {
                anyDeskKey = e.Data;
                Console.WriteLine("MEOW: "+anyDeskKey);
                    computer.anyDesk = anyDeskKey;
                    

                }
             
            });
            p.ErrorDataReceived += new DataReceivedEventHandler((s, e) =>
            {
              
                if (e.Data != null)
                   writeline("Error running Anydesk Process: "+e.Data);
            });
            p.Start();
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();
            p.WaitForExit(Convert.ToInt32( TimeSpan.FromSeconds(1).TotalMilliseconds));
           // p.WaitForExit();
            writeline("anyDeskKey is: " + anyDeskKey);
            p.Close();

            
        }

        void setAnyDeskKey(string anyKey)
        {
            computer.anyDesk = anyKey;
        }
        string getGroup()
        {
            Settings tempSetting = data.GetSettings(fullSettingFilePath);
            try
            {
            return tempSetting.group;
            }
            catch
            {
                return null;
            }
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
            //System.Environment.
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
                    try
                    {
                    lb_output.Items.Add(message);
                    lb_output.SelectedIndex = lb_output.Items.Count - 1;
                    lb_output.SelectedIndex = -1;

                    }
                    catch
                    {
                        Console.WriteLine("ATTEMPTED TO WRITE NULL STRING IN FORM.WRITELINE()");
                    }
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
            // not allowed strings:
            // " * / : < > ? \ |
            Guid g = Guid.NewGuid();
            Random random = new Random();
            
            
                string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString=GuidString.TrimEnd('=');
            //    //GuidString = GuidString.Replace("=", Convert.ToString(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65))));
            //    //GuidString = GuidString.Replace("+", Convert.ToString(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65))));
            GuidString = GuidString.Replace("\"", Convert.ToString(Convert.ToInt32(Math.Floor(33 * random.NextDouble() + 126))));
            GuidString = GuidString.Replace("\\", Convert.ToString(Convert.ToInt32(Math.Floor(33 * random.NextDouble() + 126))));
            GuidString = GuidString.Replace("/", Convert.ToString(Convert.ToInt32(Math.Floor(33 * random.NextDouble() + 126))));
            GuidString = GuidString.Replace("*", Convert.ToString(Convert.ToInt32(Math.Floor(33 * random.NextDouble() + 126))));
            GuidString = GuidString.Replace(":", Convert.ToString(Convert.ToInt32(Math.Floor(33 * random.NextDouble() + 126))));
            GuidString = GuidString.Replace("<", Convert.ToString(Convert.ToInt32(Math.Floor(33 * random.NextDouble() + 126))));
            GuidString = GuidString.Replace(">", Convert.ToString(Convert.ToInt32(Math.Floor(33 * random.NextDouble() + 126))));
            GuidString = GuidString.Replace("?", Convert.ToString(Convert.ToInt32(Math.Floor(33 * random.NextDouble() + 126))));
            GuidString = GuidString.Replace("/", Convert.ToString(Convert.ToInt32(Math.Floor(33 * random.NextDouble() + 126))));
            GuidString = GuidString.Replace("|", Convert.ToString(Convert.ToInt32(Math.Floor(33 * random.NextDouble() + 126))));
            GuidString = GuidString.Replace("-", Convert.ToString(Convert.ToInt32(Math.Floor(33 * random.NextDouble() + 126))));

            settings.setKey(GuidString);
            data.SaveObjectData(settings,"Settings","settings");
            data.SaveObjectData(GuidString, "init", "settings");
            
            return GuidString;

        }
        public string getUniqueKey()
        {
            string initFile = Directory.GetCurrentDirectory() + "\\Settings\\init.json";
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

        #region Choco installations
        public void choco(string Command, string username, string password, string domain)
        {
           Installer installer = new Installer(this);
            
            NetworkCredential credential = new NetworkCredential(username, password, domain);
            List<string> output = installer.Install(Command, credential);
            foreach (var outs in output)
                if (outs != null)
                    writeline(outs);
        }
        #endregion

        public List<string> getInstallations()
        {
            List<string> software = new List<string>();
            

            //Process process = new Process();
            //process.StartInfo = new ProcessStartInfo();
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
        public List<string> getChocoInstallFiles()
        {
            string chocoPath = @"C:\ProgramData\chocolatey\lib";
            
            List<string> chocos = new List<string>();
            if (Directory.Exists(chocoPath))
            {
                //string[] chocoArr = Directory.GetDirectories(chocoPath);
                //Console.WriteLine("chocoArr[] size = " + chocoArr.Length);
                //Console.WriteLine(Path.(chocoPath));
                List<string> arr= new List<string>(Directory.EnumerateDirectories(chocoPath));
                foreach (string folder in arr)
                {
                    chocos.Add(Path.GetFileName(folder));
                }
            }
            else
            {
                writeline($"-==ERROR GETTING CHOCOLATEY INSTALLATIONS==-  Either chocolatey is not installed or default choco path:" +
                    $"{chocoPath} was not found. ");
            }
            return chocos;
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
            data.SaveObjectData(computer, computer.uniqueKey, "ref");
            writeline("note saved!");
        }

    

        private void btn_saveGroup_Click(object sender, EventArgs e)
        {
            setGroup(cb_groups.Text);
            writeline($"Group {cb_groups.Text} saved.");
        }
    }

}

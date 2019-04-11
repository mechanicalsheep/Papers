using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;

namespace Service
{
    class DataGatherer
    {
        Settings settings = new Settings();
        string settingPath;
        string SettingsFilename;
        string settingFolder;
        public string ip { get; set; }
        string path;
        int port;
        System.Timers.Timer aTimer;
        string fullSettingFilePath;

        public Computer computer;
        //ClientNet clientNet;
       ServiceDataHandler data;
        ManagementClass mc = new ManagementClass();

        public DataGatherer(string Path)
        {
            path = Path;
            settingFolder = "settings";
            SettingsFilename = "Settings";
            fullSettingFilePath = $"{path}\\{settingFolder}\\{SettingsFilename}.json";
            


            mc.Path = new ManagementPath("Win32_ComputerSystem");
            

            computer = new Computer(getComputerName());
            data = new ServiceDataHandler(path);
            GetComputerData();

            StartManifest(computer);

            data.SaveObjectData(computer, computer.uniqueKey, "ref");
        }
        string GetComputerModel()
        {
            ManagementObjectCollection infos = mc.GetInstances();
            string model = "";
            Console.WriteLine("infos.count = " + infos.Count);
            foreach (var info in infos)
            {

                model = info["Model"].ToString();
            }
            return model;
        }
        public void StartManifest(Computer Computer)
        {
            //  Console.WriteLine("already have this computer info.");
            Computer computer = Computer;
            string path = Directory.GetCurrentDirectory() + "\\ref\\" + computer.uniqueKey + ".json";
            if (File.Exists(path))
            {
                Computer savedComputer = data.getComputer();
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
            string processor = "";
            Console.WriteLine("infos.count = " + infos.Count);
            foreach (var info in infos)
            {

                processor = info["OSArchitecture"].ToString();
            }

            return processor;
        }


        public void setGroup(string group)
        {
            Settings tempSetting = data.GetSettings(fullSettingFilePath);
            tempSetting.group = group;
            data.SaveObjectData(tempSetting, SettingsFilename, "settings");
            getGroup();
        }
        /*string GetUser()
        {
            ManagementObjectSearcher Processes = new ManagementObjectSearcher("SELECT * FROM Win32_Process");
            string username="noUser";
            foreach (System.Management.ManagementObject Process in Processes.Get())
            {
                if (Process["ExecutablePath"] != null &&
                    System.IO.Path.GetFileName(Process["ExecutablePath"].ToString()).ToLower() == "explorer.exe")
                {
                    string[] OwnerInfo = new string[2];
                    Process.InvokeMethod("GetOwner", (object[])OwnerInfo);

                    username = OwnerInfo[0];
                    Console.WriteLine(string.Format("Windows Logged-in Interactive UserName={0}", username));

                   
                }
            }
            return username;
            /* string user = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
             //writeline("current user is: " + user);
             return user;

             //the following returns cached user's in some cases that are not currently logged in.
             //return Environment.UserName; 
             
        }*/
        Dictionary<string,DateTime>GetUsers()
        {
            Dictionary<string, DateTime> users = new Dictionary<string, DateTime>();
            DirectoryInfo dir = new DirectoryInfo(@"C:\Users\");
            foreach (var folder in dir.GetDirectories())
            {
                users.Add(folder.Name,folder.LastAccessTime);
                Console.WriteLine("User: " + folder.Name + " last logged in " + folder.LastAccessTime);
            }
            return users;
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
            computer.ip = ip;
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
           // writeline("Computer group is: " + computer.group);
            Console.WriteLine("Processor is: " + computer.processor);


        }
        void getAnyDeskKey()
        {
            string anyDeskKey = "";

            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;



            if (computer.processor == "64-bit")
                p.StartInfo.FileName = path + "tools\\anydeskid.bat";
            else
                p.StartInfo.FileName = path + "tools\\anydeskidx86.bat";




            p.OutputDataReceived += new DataReceivedEventHandler((s, e) =>
            {


                if (e.Data != null || e.Data == "")
                {
                    anyDeskKey = e.Data;
                    Console.WriteLine("MEOW: " + anyDeskKey);
                    computer.anyDesk = anyDeskKey;


                }

            });
            p.ErrorDataReceived += new DataReceivedEventHandler((s, e) =>
            {

                if (e.Data != null)
                    Console.WriteLine("Error running Anydesk Process: " + e.Data);
            });
            p.Start();
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();
            p.WaitForExit(Convert.ToInt32(TimeSpan.FromSeconds(1).TotalMilliseconds));
            // p.WaitForExit();
            Console.WriteLine("anyDeskKey is: " + anyDeskKey);
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
            GuidString = GuidString.TrimEnd('=');
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
            data.SaveObjectData(settings, "Settings", "settings");
            data.SaveObjectData(GuidString, "init", "settings");

            return GuidString;

        }
        public string getUniqueKey()
        {
            
            //string initFile = path + "Settings\\init.json";
            if (!File.Exists(data.initPath))
            {
                Console.WriteLine("no initial.json, generating Key...");
                return generateUniqueKey();
            }
            try
            {
                return data.getKey();
                //var inputs = File.ReadAllText(initFile);
                //string uniqueKey = newt.Newtonsoft.Json.JsonConvert.DeserializeObject<string>(inputs);
                //return uniqueKey;

            }
            catch (Exception err)
            {
                Console.WriteLine("Error getting key: " + err.Message);
                return null;
            }
        }

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
                List<string> arr = new List<string>(Directory.EnumerateDirectories(chocoPath));
                foreach (string folder in arr)
                {
                    chocos.Add(Path.GetFileName(folder));
                }
            }
            else
            {
                Console.WriteLine($"-==ERROR GETTING CHOCOLATEY INSTALLATIONS==-  Either chocolatey is not installed or default choco path:" +
                    $"{chocoPath} was not found. ");
            }
            return chocos;
        }
      

    }
}

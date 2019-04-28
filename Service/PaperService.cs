using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Service
{
    public partial class PaperService : ServiceBase
    {
        string ip;
        int port;
        System.Timers.Timer aTimer;
        //private int eventId = 1;
        string uniqueKey;
        ServiceNet serviceNet;
        public string path { get; set; }
        ServiceDataHandler data;
        DataGatherer dataGatherer;
        public Computer computer;
        public Info info;

        public PaperService()
        {
           
            InitializeComponent();
           Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);


            eventLog1 = new EventLog();
            if (!EventLog.SourceExists("Paper"))
            {
                EventLog.CreateEventSource("Paper", "PaperLog");
            }

            eventLog1.Source = "Paper";
            eventLog1.Log="PaperLog";

           // eventLog1.WriteEntry("Current Directory is: "+Directory.GetCurrentDirectory());

        }
        private void stillAlive(object sender, ElapsedEventArgs e)
        {
            try
            {
                //eventLog1.WriteEntry("attempt to send stillAlive");
                serviceNet.sendAlive(ip, port);
            }
            catch (Exception)
            {
                aTimer.Interval = 10000;
                //writeline("unable to send, awaiting 10 seconds");
            }
        }

        private void OnTimer(object sender, ElapsedEventArgs e)
        {
            //eventLog1.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);
        }
        public void versionSuccess()
        {
            data.SaveObjectDatatoPath("successfully updated!", path + "\\", "Version updated");
        }
        protected override void OnStart(string[] args)
        {

            getServiceUser();
            path = Directory.GetCurrentDirectory();//@"D:\projects\Papers\ClientForm\bin\Debug\";
            //string currentPath = @"D:\projects\Papers\Service\bin\Debug\";
            dataGatherer = new DataGatherer(path);
            data = new ServiceDataHandler(path);
            //string yes = "I DID IT";
           // data.SaveObjectDatatoPath(path, @"D:\projects\Papers\Service\bin\Debug", "Path");
         // eventLog1.WriteEntry("passed serviceDataHandler");
            serviceNet = new ServiceNet(path);
            info = serviceNet.GetInfo();
            computer = data.getComputer();

           // versionSuccess();
            //data.SaveObjectDatatoPath("VERSION 0.0.0.2", path + "\\", "Version updated");
            if (!File.Exists(path + "\\Version.json"))
            {
                string Version = "0.0.0.0";
                data.SaveObjectDatatoPath(Version, path+"\\", "Version");
            }
           // computer.version = "0.0.0.0";
            computer.ip = info.ip;

           ServiceController sc = new ServiceController("PaperService");

            /*Updater updater = new Updater();
             updater.RunUpdate();
             /*if(computer.version != info.version)
             {
                 Updater updater = new Updater();
                 updater.RunUpdate();
             }*/
            Console.WriteLine("IP from serviceNEt that will be saved is: " + dataGatherer.ip);
            data.SaveObjectData(computer, computer.uniqueKey, "ref");

            Console.WriteLine("username logged in is: " + computer.username);

            //ip from file
            ip = info.ip;
            port = Convert.ToInt32(info.port);
            ///server-shady as server
            //ip = "192.168.11.193";

            /// SHADY as Server
            //ip = "192.168.11.105";

            ///MSI as server
            //ip = "192.168.8.100";

            //port = 11111;
            uniqueKey = data.uniqueKey;

            // computer = data.getComputer(@"D:\projects\Papers\ClientForm\bin\Debug\ref\"+uniqueKey+".json");

            serviceNet.sendComputer(ip, port, computer);
            //data.SaveObjectData("hello!","serviceLog","Meow");

            aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(stillAlive);
            aTimer.Interval = 5000;
            aTimer.Enabled = true;

            /* if (sc.Status == ServiceControllerStatus.StartPending)
             {
                 sc.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 10));
             }*/

            Thread t = new Thread(() =>
            {
               sc.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0,0,10));
                    eventLog1.WriteEntry("Computer.version != info.version :: Computer.version= " + computer.version + " info.version= " + info.version);
                if (computer.version != info.version)
                {
                    Updater updater = new Updater(path,info,computer.version);
                    updater.CallUpdater();

                }
                // updater.RunUpdate();
                
                
            });
            t.Start();


        }
        public void getServiceUser()
        {
            /* System.Management.SelectQuery sQuery = new System.Management.SelectQuery(string.Format("select name, startname from Win32_Service")); // where name = '{0}'", "MCShield.exe"));
             using (System.Management.ManagementObjectSearcher mgmtSearcher  = new System.Management.ManagementObjectSearcher(sQuery))
             {
                 foreach (System.Management.ManagementObject service in mgmtSearcher.Get())
                 {
                     string servicelogondetails =
                         string.Format("Name: {0} ,  Logon : {1} ", service["Name"].ToString(), service["startname"]).ToString();
     eventLog1.WriteEntry(servicelogondetails);
                 }
             }*/
            ManagementObject wmiService = new ManagementObject("Win32_Service.Name='" + this.ServiceName + "'");
            wmiService.Get();
            string user = wmiService["startname"].ToString();
            eventLog1.WriteEntry("Service: "+user);
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("In OnStop.");
        }

        private void eventLog1_EntryWritten(object sender, EntryWrittenEventArgs e)
        {

        }
    }
}

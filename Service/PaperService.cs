using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
                eventLog1.WriteEntry("attempt to send stillAlive");
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

        protected override void OnStart(string[] args)
        {
            path = @"D:\projects\Papers\ClientForm\bin\Debug\";
            string currentPath = @"D:\projects\Papers\Service\bin\Debug\";
            dataGatherer = new DataGatherer(currentPath);
            data = new ServiceDataHandler(currentPath);
            serviceNet = new ServiceNet(currentPath);
            info = serviceNet.GetInfo();
            computer = data.getComputer();
            if (!File.Exists(currentPath + "Version.json"))
            {
                string Version = "0.0.0.0";
                data.SaveObjectDatatoPath(Version, @"D:\projects\Papers\Service\bin\Debug", "Version");
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
                if (computer.version != info.version)
                {
                    Updater updater = new Updater(info,computer.version);
                updater.CallUpdater();

                }
              // updater.RunUpdate();
                
            });
            t.Start();


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

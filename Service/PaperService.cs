using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Service
{
    public partial class PaperService : ServiceBase
    {
        string ip;
        int port;
        Timer aTimer;
        //private int eventId = 1;
        string uniqueKey;
        ServiceNet serviceNet;
        public string path { get; set; }
        ServiceDataHandler data;
        DataGatherer dataGatherer;
        Computer computer;

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

            eventLog1.WriteEntry("Current Directory is: "+Directory.GetCurrentDirectory());

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
            /* //ClientDataHandler data = new ClientDataHandler();
             //string hello = "it's working!";
             //data.SaveObjectData(hello, "HELLO", "Works");
             //eventLog1.WriteEntry("In OnStart");
             // Set up a timer that triggers every minute.
             Timer timer = new Timer();

             timer.Interval = 6000; // 6 seconds
             timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
             timer.Start();
             */
            path = @"D:\projects\Papers\ClientForm\bin\Debug\";
            dataGatherer = new DataGatherer(path);
            data = new ServiceDataHandler(path);
            serviceNet = new ServiceNet(path);
            computer = data.getComputer();
            computer.ip = serviceNet.getIP();
            computer.machineNote = "haha it worked";
            Console.WriteLine("IP from serviceNEt that will be saved is: " + computer.ip);
            data.SaveObjectData(computer, computer.uniqueKey, "ref");
            Console.WriteLine("username logged in is: " +computer.username);


            ///server-shady as server
            ip = "192.168.11.193";

            /// SHADY as Server
            //ip = "192.168.11.105";

            ///MSI as server
            //ip = "192.168.8.100";

            port = 11111;
            uniqueKey = data.uniqueKey;

            eventLog1.WriteEntry("Starting timer for sendAlive()");
            aTimer = new Timer();
            aTimer.Elapsed += new ElapsedEventHandler(stillAlive);
            aTimer.Interval = 5000;
            aTimer.Enabled = true;
            //Console.WriteLine("Press any Key to stop");
            //Console.ReadLine();
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

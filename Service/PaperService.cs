﻿using System;
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
        string uniqueKey;
        ServiceNet serviceNet;
        public string path { get; set; }
        ServiceDataHandler data;
        DataGatherer dataGatherer;
        public Computer computer;
        public Info info;
        int stillAliveFailCount = 0;

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

           //eventLog1.WriteEntry("Current Directory is: "+Directory.GetCurrentDirectory());

        }
        

        private void OnTimer(object sender, ElapsedEventArgs e)
        {
           
        }
        public void versionSuccess()
        {
            data.SaveObjectDatatoPath("successfully updated!", path + "\\", "Version updated");
        }
        protected override void OnStart(string[] args)
        {
            try
            {

            //getServiceUser();
            path = Directory.GetCurrentDirectory();
            dataGatherer = new DataGatherer(path);
            data = new ServiceDataHandler(path);           
            serviceNet = new ServiceNet(path);
                info = serviceNet.GetInfo();
            computer = data.getComputer();
            if (!File.Exists(path + "\\Version.json"))
            {
                string Version = "0.0.0.2";
                data.SaveObjectDatatoPath(Version, path+"\\", "Version");
            }
            computer.ip = info.ip;

          

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

            //versionSuccess();

           
            serviceNet.sendComputer(ip, port, computer);
           
           

            if (!isUpdated())
            {
                eventLog1.WriteEntry("computer is not updated, attempting to run update");
                    Updater updater = new Updater(path, info, computer.version);
                    updater.startUpdate();
            }
           

            }
            catch(Exception err)
            {
                eventLog1.WriteEntry("Error starting service: " + err);
            }

            serviceNet.startStillAlive();

        }

        
        public bool isUpdated()
        {
            

            info = serviceNet.GetInfo();
            if (computer.version != info.version)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        public void getServiceUser()
        {
            
            ManagementObject wmiService = new ManagementObject("Win32_Service.Name='" + this.ServiceName + "'");
            wmiService.Get();
            string user = wmiService["startname"].ToString();
            eventLog1.WriteEntry("Service: "+user);
        }

        protected override void OnStop()
        {
            //eventLog1.WriteEntry("In OnStop.");
            
        }

        private void eventLog1_EntryWritten(object sender, EntryWrittenEventArgs e)
        {

        }
    }
}

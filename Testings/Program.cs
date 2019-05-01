using System;
using System.IO;
using System.Threading;
using System.Timers;

namespace Testings
{
    class Program
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
        public Program()
        {
            onStart();

        }

        public void onStart()
        {
            // path = @"D:\projects\Papers\ClientForm\bin\Debug\";
            string currentPath = @"D:\projects\Papers\Testings\bin\Debug\";
            dataGatherer = new DataGatherer(currentPath);
            data = new ServiceDataHandler(currentPath);
            serviceNet = new ServiceNet(currentPath);
            info = serviceNet.GetInfo();
            computer = data.getComputer();
            if (!File.Exists(currentPath + "Version.json"))
            {
                string Version = "0.0.0.0";
                // data.SaveObjectDatatoPath(Version, @"D:\projects\Papers\Service\bin\Debug", Version);
            }
            // computer.version = "0.0.0.0";
            computer.ip = info.ip;

            // ServiceController sc = new ServiceController("PaperService");

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
                //sc.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0,0,10));
                //Updater updater = new Updater(this);
                //updater.RunUpdate();

            });
            t.Start();


        }
        private void stillAlive(object sender, ElapsedEventArgs e)
        {
            try
            {
                serviceNet.sendAlive(ip, port);
            }
            catch (Exception)
            {
                aTimer.Interval = 10000;
                //writeline("unable to send, awaiting 10 seconds");
            }
        }


        static void Main(string[] args)
        {
            Program program = new Program();

            Console.ReadLine();


        }
    }
}

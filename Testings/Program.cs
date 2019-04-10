using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Testings
{
    class Program
    {
        string ip;
        int port;
        Timer aTimer;
        //private int eventId = 1;
        string uniqueKey;
        ServiceNet serviceNet;
        ServiceDataHandler data = new ServiceDataHandler(@"D:\projects\Papers\ClientForm\bin\Debug\");
        Computer computer;
        public Program()
        {
            ///server-shady as server
            //ip = "192.168.11.193";

            /// SHADY as Server
            ip = "192.168.11.105";

            ///MSI as server
            //ip = "192.168.8.100";

            port = 11111;
            uniqueKey = data.uniqueKey;
            // computer = data.getComputer(@"D:\projects\Papers\ClientForm\bin\Debug\ref\"+uniqueKey+".json");
            serviceNet = new ServiceNet(data);

            //data.SaveObjectData("hello!","serviceLog","Meow");
            onStart();

        }

        public void onStart()
        {

            aTimer = new Timer();
            aTimer.Elapsed += new ElapsedEventHandler(stillAlive);
            aTimer.Interval = 5000;
            aTimer.Enabled = true;
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

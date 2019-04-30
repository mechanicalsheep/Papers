using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using NetworkCommsDotNet.Connections.TCP;
using ProtoBuf;
using System.Timers;
//using System.Threading;

namespace Service
{

    public class ServiceNet
    {
        Commands commands;
        string path;
        //private ClientForm form;
        string ip;
        string  localport;
        int serverPort;
        string key;
        Computer computer;
        // DataGatherer gatherer;
        ServiceDataHandler data;
        Info info = new Info();
        Uri uri = new Uri("https://drive.google.com/uc?export=download&id=1feb0bJrQl6wKePaEmhJ8s5fG8m1ZAo2g");
        Timer aTimer;// = new System.Timers.Timer();
        EventLog eventLog = new EventLog();
        public ServiceNet(string path)
        {
            aTimer = new Timer();
            this.path = path;
            commands = new Commands(path);
            eventLog.Source = "Paper";
            eventLog.Log="PaperLog";

            data = new ServiceDataHandler(path);

            info = getInfoFromURL();
           // testGet();
            Console.WriteLine("-==Info Version " + info.version + "==-");
            computer = data.getComputer();
            //eventLog.WriteEntry("got computer data in ServiceNet");
            key = computer.uniqueKey;
          
            
            //eventLog.WriteEntry("key is: " + computer.uniqueKey);
           
            //getIPfrom file.
            ip=info.ip;

            ///server-shady as server
            //ip = "192.168.11.193";

            /// SHADY as Server
            //ip = "192.168.11.105";
            ///MSI as server
            //ip = "192.168.8.100";
            serverPort =Convert.ToInt32(info.port);
            //form = clientForm;
            //form.writeline("-==Client=-");
           
            //sendAlive(ip, Convert.ToInt32(port));
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("ServerToClient", HandleServerMessage);
            NetworkComms.AppendGlobalIncomingPacketHandler<CommandInfo>("getKey", (packetHeader, connection, input) =>
            {
                //form.writeline("Choco command: " + input.command);
                //form.choco(input.command, input.username, input.password, input.domain);
                connection.SendObject("giveKey", key);

            });
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("sendCommand", (packetHeader, connection, command) =>
            {
               List<string> returnedMessage = commands.doCommand(command);
                connection.SendObject("CommandResponse", returnedMessage);
                //form.writeline("Choco command: " + input.command);
                //form.choco(input.command, input.username, input.password, input.domain);
                //connection.SendObject("giveKey", key);

            });
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("setGroup", (packetHeader, connection, group) =>
             {
               //supposed to call set group ofrom here.
             });
            //choco command receiver!
            NetworkComms.AppendGlobalIncomingPacketHandler<CommandInfo>("choco", (packetHeader, connection, input) =>
            {
               // form.writeline("Choco command: "+ input.command);
               // form.choco(input.command,input.username,input.password, input.domain);
               
            
            });
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("GetComputer", (packet, connection, input) =>
             {
                 //Console.WriteLine("Sending computer name: " + computer.name + " that is stored in" + data.computerPath);
                 connection.SendObject<Computer>("SentComputer",computer);
             });
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("snrClient",(packetHeader, connection, input) =>
           {
              // form.writeline("received call from server, sending the server a message");
            //  string message = form.getComputerName();
               
               //When this is received by the client it will complete the synchronous request
             //  connection.SendObject("snrServer", message);
           });

            NetworkComms.AppendGlobalConnectionEstablishHandler((connection) =>
            {
                eventLog.WriteEntry("Connected to : " + connection.ToString());
                stopStillAlive();
            });
              NetworkComms.AppendGlobalConnectionCloseHandler((connection) =>
               {
                   eventLog.WriteEntry("Connection closed to : " + connection.ToString());
                   startStillAlive();
                   aTimer.Start();
               });
          /*  NetworkComms.ConnectionEstablishShutdownDelegate connectionEstablish = (connection) =>
            {

                eventLog.WriteEntry("Connection ESTABLISHED : " + connection.ToString());
                StartStillAlive();
           

            };*/
/*
            NetworkComms.ConnectionEstablishShutdownDelegate connectionShutdownDelegate = (connection) =>
            {

                eventLog.WriteEntry("Connection shutdown : " + connection.ToString());
                StartStillAlive();
                /*string[] ipPort = connection.ConnectionInfo.RemoteEndPoint.ToString().Split(':');

            };*/

            Connection.StartListening(ConnectionType.TCP, new IPEndPoint(IPAddress.Any, 0));
            //clientForm.writeline("Client Listening On: ");
            foreach (IPEndPoint localEndpoint in Connection.ExistingLocalListenEndPoints(ConnectionType.TCP))
            {
               
                string endPointIP = localEndpoint.Address.ToString().Split(':').First();
                Console.WriteLine("string count " + endPointIP.Length);
                if (!endPointIP.Contains( "127.0.0.1" ) && endPointIP.Length>0)
                {

                   // clientForm.writeline(" - " + localEndpoint.Address + " " + localEndpoint.Port);
                   string localIp = localEndpoint.Address.ToString();
                    localport = localEndpoint.Port.ToString();
                }
            }
           
        }
        public void startStillAlive()
        {
           
            aTimer.Elapsed += new ElapsedEventHandler(stillAlive);
            aTimer.Interval = 5000;
            aTimer.Enabled = true;
            
        }
        //PLEASE RENAME!!!!!
        public void StartStillAlive()
        {
            aTimer.Start();
        }
        public void stopStillAlive()
        {
            aTimer.Stop();
        }
        private void stillAlive(object sender, ElapsedEventArgs e)
        {

            try
            {

                sendAlive(ip, serverPort);
                //stillAliveFailCount = 0;
            }
            catch (Exception)
            {

                //stillAliveFailCount++;
                info = GetInfo();
                if (info.version != computer.version)
                {
                    eventLog.WriteEntry("computer is not updated, attempting to run update");
                    Updater updater = new Updater(path,info,computer.version);
                    updater.startUpdate();
                }
                else if (info.ip != ip)
                {
                    ip = info.ip;
                }

                else
                {
                    if (aTimer.Interval < TimeSpan.FromHours(1).TotalMilliseconds)
                    {
                        //stop incrementing after 1 hours.
                        aTimer.Interval = aTimer.Interval * 2;
                    }
                    else
                    {
                        aTimer.Interval = TimeSpan.FromHours(1).TotalMilliseconds;
                    }
                }

            }
        }
        public Info GetInfo()
        {
            return info;
        }
        public void testGet()
        {
            using (WebClient web = new WebClient())
            {
                web.DownloadFile(@"\\192.168.5.10\Software for PC\Software\AnyDesk.exe",path+"\\anydesk.exe");
            }
        }
        public Info getInfoFromURL()
        {
            using(WebClient web = new WebClient())
            {
                web.DownloadFile(uri, path+"\\INFO.json");

                
            }
            Console.WriteLine("Done!");
            Info tempInfo = new Info();
            tempInfo = data.GetInfofromURL(path + "\\INFO.json");
            Console.WriteLine("ip from file is: " + ip);
            return tempInfo;
        }
        public void sendComputer(string ip, int port, Computer computer)
        {
            try
            {

           NetworkComms.SendObject<Computer>("SentComputer", ip,port,computer);
            }
            catch(Exception err)
            {
                Console.WriteLine("Error sending computer, unable to find the server. ");
            }
        }
    public string getIP()
        {
            return ip;
        }
        public string getPort()
        {
            return localport;
        }
        public void sendAlive(string ip, int port)
        {
            try
            {
                //Console.WriteLine("sending object to "+ip+ " from ");
              
                NetworkComms.SendObject("sendAlive", ip, port,key);
            }
            catch (Exception err)
            {
                Console.WriteLine("sendAlive exception thrown: ");// + err.Message);
            }
        }
        public void sendMessage(string ip, int port, string message)
        {
           // form.writeline($"SentFromClient ip: {ip} port: {port} message: {message} ");
            try
            {
            NetworkComms.SendObject("SentFromClient", ip, port, message);
            }
            catch(Exception err)
            {
                //Console.WriteLine("exception thrown: " + err.Message);
            }
        }

        private void HandleServerMessage(PacketHeader packetheader, Connection connection, string incomingobject)
        {
            string output = connection.ConnectionInfo.LocalEndPoint.ToString() + " is sending the following message: " +
                            incomingobject;
            //form.writeline(output);
        }

        
       

        public void CloseNetwork()
        {
            NetworkComms.Shutdown();
        }
    }
    [ProtoContract]
    public class CommandInfo
    {

        [ProtoMember(1)]
        public string username { get; set; }
        [ProtoMember(2)]
        public string password { get; set; }
        [ProtoMember(3)]
        public string domain { get; set; }

        [ProtoMember(4)]
        public string command { get; set; }

        public CommandInfo()
        {

        }

        public CommandInfo(NetworkCredential Credential, string Command)
        {
            username = Credential.UserName;
            password = Credential.Password;
            domain = Credential.Domain;
            command = Command;
        }
    }
}
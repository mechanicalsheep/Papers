using System;
using System.Linq;
using System.Net;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using ProtoBuf;

namespace Service
{

    public class ServiceNet
    {

        //private ClientForm form;
        string ip;
        string  port;
        public ServiceNet()//clientForm)
        {
            port = "11111";
            ///server-shady as server
            //ip = "192.168.11.193";

            /// SHADY as Server
            ip = "192.168.11.105";

            ///MSI as server
            //ip = "192.168.8.100";

            //form = clientForm;
            //form.writeline("-==Client=-");

            sendAlive(ip, Convert.ToInt32(port));
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("ServerToClient", HandleServerMessage);
            NetworkComms.AppendGlobalIncomingPacketHandler<CommandInfo>("getKey", (packetHeader, connection, input) =>
            {
                //form.writeline("Choco command: " + input.command);
                //form.choco(input.command, input.username, input.password, input.domain);
               // connection.SendObject("giveKey",form.getUniqueKey());

            });
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("setGroup", (packetHeader, connection, group) =>
             {
                // form.setGroup(group);
             });
            //choco command receiver!
            NetworkComms.AppendGlobalIncomingPacketHandler<CommandInfo>("choco", (packetHeader, connection, input) =>
            {
               // form.writeline("Choco command: "+ input.command);
               // form.choco(input.command,input.username,input.password, input.domain);
               
            
            });
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("GetComputer", (packet, connection, input) =>
             {
                 
                 //connection.SendObject<Computer>("SentComputer", form.getSavedComputerData());
             });
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("snrClient",(packetHeader, connection, input) =>
           {
              // form.writeline("received call from server, sending the server a message");
            //  string message = form.getComputerName();
               
               //When this is received by the client it will complete the synchronous request
             //  connection.SendObject("snrServer", message);
           });
            
            Connection.StartListening(ConnectionType.TCP, new IPEndPoint(IPAddress.Any, 0));
            //clientForm.writeline("Client Listening On: ");
            foreach (IPEndPoint localEndpoint in Connection.ExistingLocalListenEndPoints(ConnectionType.TCP))
            {
               
                string endPointIP = localEndpoint.Address.ToString().Split(':').First();
                Console.WriteLine("string count " + endPointIP.Length);
                if (!endPointIP.Contains( "127.0.0.1" ) && endPointIP.Length>0)
                {

                   // clientForm.writeline(" - " + localEndpoint.Address + " " + localEndpoint.Port);
                    ip = localEndpoint.Address.ToString();
                    port = localEndpoint.Port.ToString();
                }
            }
        }
    public string getIP()
        {
            return ip;
        }
        public string getPort()
        {
            return port;
        }
        public void sendAlive(string ip, int port)
        {
            try
            {
                //Console.WriteLine("sending object to "+ip+ " from ");
                NetworkComms.SendObject("sendAlive", ip, port,"THE SERVICE IS ONLINE!");
            }
            catch (Exception err)
            {
                //Console.WriteLine("sendAlive exception thrown: " + err.Message);
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
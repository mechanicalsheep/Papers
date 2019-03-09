using System.Runtime.Remoting.Channels;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using System.Net;
using System;
using System.Collections.Generic;
using ProtoBuf;
using System.Linq;

namespace ServerForm
{
    public class ServerNet
    {

        ServerForm form;
        int port = 11111;
        
        public ServerNet(ServerForm serverForm)
        {
            form = serverForm;
            NetworkComms.ConnectionEstablishShutdownDelegate clientEstablishDelegate = (connection) =>

            {
                form.ScanForConnections();
                string[] ipPort = connection.ConnectionInfo.RemoteEndPoint.ToString().Split(':');
                Console.WriteLine(connection.ConnectionInfo.RemoteEndPoint.ToString());
                form.AddOnlineComputer(GetComputerName(ipPort[0],Convert.ToInt32(ipPort[1]),"Whatevs"),ipPort[0]);
              
                Console.WriteLine("Client " + connection.ConnectionInfo + " connected.");

            };
            NetworkComms.ConnectionEstablishShutdownDelegate connectionShutdownDelegate = (connection) =>
              {
                  string ipPort= connection.ConnectionInfo.RemoteEndPoint.ToString();
                  form.ScanForConnections();
                  form.RemoveOfflineComputer(ipPort.Split(':').First());
                  Console.WriteLine("Client" + connection.ConnectionInfo + "disconnected");
              };
            NetworkComms.AppendGlobalConnectionEstablishHandler(clientEstablishDelegate);
            NetworkComms.AppendGlobalConnectionCloseHandler(connectionShutdownDelegate);

            NetworkComms.AppendGlobalIncomingPacketHandler<string>("sendAlive",HandleAliveSend);
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("SentFromClient",HandleStringFromClient);

            Connection.StartListening(ConnectionType.TCP, new IPEndPoint(IPAddress.Any, port));
            form.writeline("-==SERVERNET ONLINE==-");
            form.writeline("Listening on: ");
            foreach (IPEndPoint localEndpoint in Connection.ExistingLocalListenEndPoints(ConnectionType.TCP))
            {
                form.writeline(" - "+localEndpoint.Address+" "+localEndpoint.Port);
            }
           
        }

        private void HandleAliveSend(PacketHeader packetHeader, Connection connection, string incomingObject)
        {
            
        }

        public Computer GetComputer(string ip, int port)
        {
            try
            {
                Computer computer = NetworkComms.SendReceiveObject<string, Computer>("GetComputer", ip, port, "SentComputer", 800000, "gimme computer");
                Console.WriteLine("Computer name received is: "+computer.name);
                return computer;
            }
            catch (Exception)
            {
                return null;
            }
        }
     public void sendCommand(string ip, int port, CommandInfo commandInfo)
        {
            form.writeline("Sending Command: " + commandInfo.command);
            try
            {

                NetworkComms.SendObject<CommandInfo>("choco", ip, port, commandInfo);
            }
            catch (Exception err)
            {
                form.writeline($"EXCEPTION IN SENDING COMMAND: {err.Message}");
            }
        }
        public List<string> GetConnections()
        {
            
            List<string> connectionList = new List<string>();
            foreach(var connection in NetworkComms.AllConnectionInfo())
            {
                connectionList.Add(connection.RemoteEndPoint.ToString());
            }
            return connectionList;
        }
        private void HandleStringFromClient(PacketHeader packetHeader, Connection connection, string incomingObject)
        {
            
            form.writeline($"Client:  {connection.ConnectionInfo.RemoteEndPoint.ToString()} has sent the following");
            
            form.writeline($"Message: {incomingObject}");

           
        }

        
        public string GetComputerName(string ip, int port, string message)
        {
            //form.writeline("Going to send the following");
            //form.writeline($"ServerToClient ip: {ip} port: {port} message: {message} ");
            try
            {
               string clientMessage=NetworkComms.SendReceiveObject<string,string>("snrClient", ip, port, "snrServer", 800000,
                    message);
                form.writeline(clientMessage);
                return clientMessage;

            }
            catch (Exception err)
            {
                form.writeline($"ERROR: {err.Message}");
                return "NOT AVAILABLE";
            }
        }

        internal void CloseNetwork()
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
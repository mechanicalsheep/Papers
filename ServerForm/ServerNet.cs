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
                form.writeline(connection + " has connected in ClientEstablishDelegate");
                string[] ipPort = connection.ConnectionInfo.RemoteEndPoint.ToString().Split(':');
                string uniqueKey = GetUniqueKey(ipPort[0], Convert.ToInt32(ipPort[1]));
                Console.WriteLine($"Server got UniqueKey {uniqueKey} from connection {ipPort[0]}");
                if (form.computers.ContainsKey(uniqueKey))
                {
                    //send data once it connects.
                    form.computers[uniqueKey] = GetComputer(ipPort[0], Convert.ToInt32(ipPort[1]));
                    form.writeline($"Updating {form.computers[uniqueKey].name} to include user: {form.computers[uniqueKey].username}");
                    form.writeline("Computers[] unique key is: "+form.computers[uniqueKey].uniqueKey);
                    form.StartManifest(form.computers[uniqueKey]);

                    //the rest
                    form.computers[uniqueKey].port = ipPort[1];
                    form.setOnline(uniqueKey, ipPort[0],ipPort[1]);
                    form.writeline("Setting " + form.computers[uniqueKey].name + " online");
                }
                else
                {
                    try
                    {

                    form.getComputerData(ipPort[0], Convert.ToInt32(ipPort[1]));
                    Computer computer = GetComputer(ipPort[0], Convert.ToInt32(ipPort[1]));
                        computer.ip = ipPort[0];
                    try
                    {
                        computer.online = true;
                            computer.ip = ipPort[0];
                        computer.port = ipPort[1];
                        //form.setOnline(computer.uniqueKey, ipPort[0], ipPort[1]);
                    form.computers.Add(computer.uniqueKey, computer);


                    }
                    catch
                    {

                    }

                    //System.Windows.Forms.ListViewItem lvi = new System.Windows.Forms.ListViewItem(new[] { computer.name, computer.ip, ipPort[1], computer.uniqueKey });
                    //lvi.ForeColor = System.Drawing.Color.Blue;
                    //lvi.Tag = computer;
                    form.addComputerToList(computer);
                    }
                    catch(Exception err)
                    {
                        Console.WriteLine("ServerNet Exception::: -"+err.Message);
                    }

                }
                Console.WriteLine(connection.ConnectionInfo.RemoteEndPoint.ToString());

                //form.AddOnlineComputer(GetComputerName(ipPort[0],Convert.ToInt32(ipPort[1]),"Whatevs"),ipPort[0], ipPort[1]);
               
            //Console.WriteLine("Client " + connection.ConnectionInfo + " connected.");
                //form.writeline("Client " + connection.ConnectionInfo + " connected.");


            };
            NetworkComms.ConnectionEstablishShutdownDelegate connectionShutdownDelegate = (connection) =>
              {
             
                  string[] ipPort = connection.ConnectionInfo.RemoteEndPoint.ToString().Split(':');
                
                  form.writeline(connection + " has disconnected.");
                  form.setOffline(ipPort[0]);

              };
            NetworkComms.AppendGlobalIncomingPacketHandler<List<string>>("CommandResponse", (packetHeader, connection, command) =>
            {
                foreach(var com in command)
                {
                form.writeline(com);

                }
               
            

            });
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

        public void setGroup(string ip, int port, string group)
        {
            NetworkComms.SendObject("setGroup", ip, port, group);
        }

        public bool isAlive(string ip)
        {
            List<ConnectionInfo> connections= NetworkComms.AllConnectionInfo();
            //bool alive = false;
            foreach(var connection in connections)
            {
                if (connection.LocalEndPoint.ToString().Split(':').First() == ip)
                    return true;
            }
            //ConnectionInfo connection = new ConnectionInfo(ip, port);
            return false;
        }

        private void HandleAliveSend(PacketHeader packetHeader, Connection connection, string incomingObject)
        {
            form.writeline(connection + " is alive! Key= "+incomingObject);
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
        public void sendCommand(string ip, int port, string Command)
        {
            form.writeline("Sending Command: " + Command);
            try
            {

                NetworkComms.SendObject<string>("sendCommand", ip, port, Command);
            }
            catch (Exception err)
            {
                form.writeline($"EXCEPTION IN SENDING COMMAND: {err}");
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

        public string GetUniqueKey(string ip, int port)
        {
            try
            {
                string uniqueKey = NetworkComms.SendReceiveObject<string, string>("getKey", ip, port, "giveKey", 800000, "meow");
                //form.writeline(clientMessage);
                return uniqueKey;

            }
            catch (Exception err)
            {
                form.writeline($"ERROR: {err.Message}");
                return "NOT AVAILABLE";
            }
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
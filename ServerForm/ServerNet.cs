using System.Runtime.Remoting.Channels;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using System.Net;
using System;
using System.Collections.Generic;

namespace ServerForm
{
    public class ServerNet
    {

        ServerForm form;
        int port = 11111;
        
        public ServerNet(ServerForm serverForm)
        {
            form = serverForm;
           
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("SentFromClient",HandleStringFromClient);
            Connection.StartListening(ConnectionType.TCP, new IPEndPoint(IPAddress.Any, port));
            form.writeline("-==SERVERNET ONLINE==-");
            form.writeline("Listening on: ");
            foreach (IPEndPoint localEndpoint in Connection.ExistingLocalListenEndPoints(ConnectionType.TCP))
            {
                form.writeline(" - "+localEndpoint.Address+" "+localEndpoint.Port);
            }
           
        }
      /* public void test()
        {
            
            foreach(var connection in NetworkComms.AllConnectionInfo())
            {
                form.writeline(connection.RemoteEndPoint.ToString());
                
            }
           
        }*/
        public List<string> GetConnection()
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
            form.writeline($"Client: + {connection.ConnectionInfo.RemoteEndPoint.ToString()} has sent the following");
            
            form.writeline($"Message: {incomingObject}");
        }

        public void sendMessage(string ip, int port, string message)
        {
            form.writeline("Going to send the following");
            form.writeline($"ServerToClient ip: {ip} port: {port} message: {message} ");
            try
            {
                //NetworkComms.SendObject("ServerToClient", ip, port, message);
                string clientMessage=NetworkComms.SendReceiveObject<string,string>("snrClient", ip, port, "snrServer", 800000,
                    message);
                form.writeline(clientMessage);

            }
            catch (Exception err)
            {
                form.writeline($"ERROR: {err.Message}");
            }
        }

        internal void CloseNetwork()
        {
            NetworkComms.Shutdown();
        }
    }
}
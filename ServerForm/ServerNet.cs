using System.Runtime.Remoting.Channels;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using System.Net;
using System;
using System.Collections.Generic;

namespace PaperServer
{
    public class ServerNet
    {

        MainForm sf;
        public ServerNet(MainForm form)
        {
            sf = form;
            sf.writeline("-==SERVERNET ONLINE==-");
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("SentFromClient",HandleStringFromClient);
            Connection.StartListening(ConnectionType.TCP, new IPEndPoint(IPAddress.Any, 0));
            sf.writeline("Listening on: ");
            foreach (IPEndPoint localEndpoint in Connection.ExistingLocalListenEndPoints(ConnectionType.TCP))
            {
                sf.writeline(" - "+localEndpoint.Address+" "+localEndpoint.Port);
            }
        }

        private void HandleStringFromClient(PacketHeader packetHeader, Connection connection, string incomingObject)
        {
            sf.writeline($"Client: + {connection.ConnectionInfo.LocalEndPoint.ToString()} has send the following");
            
            sf.writeline($"Message: {incomingObject}");
        }

        public void sendMessage(string ip, int port, string message)
        {
            sf.writeline("Going to send the following");
            sf.writeline($"ServerToClient ip: {ip} port: {port} message: {message} ");
            try
            {
                //NetworkComms.SendObject("ServerToClient", ip, port, message);
                string clientMessage=NetworkComms.SendReceiveObject<string,string>("snrClient", ip, port, "snrServer", 800000,
                    message);
                sf.writeline(clientMessage);

            }
            catch (Exception err)
            {
                sf.writeline($"ERROR: {err.Message}");
            }
        }

        internal List<Computer> getRespondingClients()
        {
            //todo: get the list of responding clients.
            throw new NotImplementedException();
        }

        internal void CloseNetwork()
        {
            NetworkComms.Shutdown();
        }
    }
}
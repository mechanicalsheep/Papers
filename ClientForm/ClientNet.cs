using System.Net;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;

namespace Client
{
    public class ClientNet
    {
        int port = 123456;
        private ClientForm form;
        public ClientNet(ClientForm clientForm)
        {
            form = clientForm;
            form.writeline("-==Client=-");
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("ServerToClient", HandleServerMessage);
           NetworkComms.AppendGlobalIncomingPacketHandler<string>("snrClient",(packetHeader, connection, input) =>
           {
               form.writeline("received call from server, sending the server a message");
               string message = $"Client: {form.getUniqueKey()} sending data.";
               //When this is received by the client it will complete the synchronous request
               connection.SendObject("snrServer", message);
           });
            
            Connection.StartListening(ConnectionType.TCP, new IPEndPoint(IPAddress.Any, 0));
            clientForm.writeline("Client Listening On: ");
            foreach (IPEndPoint localEndpoint in Connection.ExistingLocalListenEndPoints(ConnectionType.TCP))
            {
                clientForm.writeline(" - "+localEndpoint.Address+" "+localEndpoint.Port);
            }
        }

       

        public void sendMessage(string ip, int port, string message)
        {
            //form.writeline("Going to send the following");
            form.writeline($"SentFromClient ip: {ip} port: {port} message: {message} ");
            NetworkComms.SendObject("SentFromClient", ip, port, message);
        }

        private void HandleServerMessage(PacketHeader packetheader, Connection connection, string incomingobject)
        {
            string output = connection.ConnectionInfo.LocalEndPoint.ToString() + " is sending the following message: " +
                            incomingobject;
            form.writeline(output);
        }

        
        string getIP()
        {
            return null;
        }

        public void CloseNetwork()
        {
            NetworkComms.Shutdown();
        }
    }
}
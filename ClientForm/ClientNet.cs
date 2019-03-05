﻿using System.Net;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using ProtoBuf;

namespace Client
{

    public class ClientNet
    {
        
        private ClientForm form;
        public ClientNet(ClientForm clientForm)
        {
            form = clientForm;
            form.writeline("-==Client=-");
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("ServerToClient", HandleServerMessage);
        
            //choco command receiver!
            NetworkComms.AppendGlobalIncomingPacketHandler<CommandInfo>("choco", (packetHeader, connection, input) =>
            {
                form.writeline("Choco command: "+ input.command);
                form.choco(input.command,input.username,input.password, input.domain);
               
            
            });

            NetworkComms.AppendGlobalIncomingPacketHandler<string>("snrClient",(packetHeader, connection, input) =>
           {
               form.writeline("received call from server, sending the server a message");
              string message = form.getUniqueKey();
               
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
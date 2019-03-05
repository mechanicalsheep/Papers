using ProtoBuf;
using System;
using System.Collections.Generic;


namespace ServerForm
{
    [ProtoContract]
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public class Computer
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        [ProtoMember(1)]
        public string name { get; set; }
        [ProtoMember(2)]
        public string OS { get; set; }
        [ProtoMember(3)]
        public string ram { get; set; }
        [ProtoMember(4)]
        public string ip { get; set; }
        [ProtoMember(5)]
        public bool online;
        [ProtoMember(6)]
        public string uniqueKey { get; set; }
        [ProtoMember(7)]
        public string group { get; set; }
        [ProtoMember(8)]
        public List<string> software = new List<string>();
        [ProtoMember(9)]
        public string dateTime { get; set; }

        
        //needed for protobuf
        protected Computer()
        {
            //fillComputerData();
        }
        public Computer(string Name)
        {
            name = Name;
          
        }

      

      
        public override bool Equals(object obj)
        {
            if (!(obj is Computer))
                return false;

            var other = obj as Computer;

            // if (name != other.name || OS != other.OS || ram != other.ram || ip != other.ip || online != other.online || uniqueString != other.uniqueString || software != other.software || group != other.group)
            if (name == other.name && OS == other.OS && ram == other.ram && uniqueKey == other.uniqueKey && group == other.group)
            {
                //if all the others are true, they are the same computer, let's check if the softwares have changed.
                if (software.Count == other.software.Count)
                {
                    foreach(var soft in software)
                    {
                        if (!other.software.Contains(soft))
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
                Console.WriteLine("something is the same!");
                return true; }
            /*if (name != other.name) {
                Console.WriteLine(name+" != "+ other.name);
                    return false;
                    }*/
            Console.WriteLine("returning false");
            return false;
        }

        private bool isOnline()
        {
            //todo add powershell that will ping and return if online
            return online;
        }

        public void getComputerInfo()
        {
            /*todo
             Invoke-Command -ComputerName sheep1 -ScriptBlock { Get-ComputerInfo } -credential mechanicalsheep
             */
        }

        public void getInstalledSoftware()
        {
            //todo powershell command to get all installed.
            
            //delete this later.
            
        }
        
        public void installSoftware(string software)
        {
         /*todo
           powershell command= Invoke-Command -ComputerName sheep1 -ScriptBlock { choco install googlechrome -y } -credential mechanicalsheep
         */
        }
       
    }
 
    }

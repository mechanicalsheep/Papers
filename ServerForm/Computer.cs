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
        [ProtoMember(10)]
        public string machineNote { get; set; }
        [ProtoMember(11)]
        public string note { get; set; }


        //needed for protobuf
        protected Computer()
        {

        }
        public Computer(string Name)
        {
            name = Name;

        }



        /*todo: work on the equal override definition as for now it will return false
         * whether they are two different computers or if they are the same computer but some component changed.
         */
        public override bool Equals(object obj)
        {
            if (!(obj is Computer))
                return false;

            var other = obj as Computer;
            if (uniqueKey == other.uniqueKey)
            {

                if (name == other.name && OS == other.OS && ram == other.ram && group == other.group)
                {
                    //if all the others are true, they are the same computer, let's check if the softwares have changed.
                    if (software.Count == other.software.Count)
                    {
                        foreach (var soft in software)
                        {
                            if (!other.software.Contains(soft))
                            {
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    return false;
                }
                Console.WriteLine("something is the same!");
                return true;
            }

            Console.WriteLine("returning false");
            return false;
        }


    }

}

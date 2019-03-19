using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Client
{
    [ProtoContract]
    public class Settings
    {
        [ProtoMember(1)]
        public string uniquekey { get; set; }
        [ProtoMember(2)]
        public string group { get; set; }
        
        ClientDataHandler data;
        public Settings()
        {
            data = new ClientDataHandler();
        }
        public Settings(string key, string Group)
        {
            data = new ClientDataHandler();
            uniquekey = key;
            group = Group;
        }
        public void setKey(string key)
        {
            uniquekey = key;
        }
        public string getKey()
        {
            return uniquekey;
        }
        public void setGroup(string Group)
        {
            group = Group;
        }
        public string getGroup()
        {
            return group;
        }
     
    }
}

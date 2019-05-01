using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;


namespace Testings
{
    [ProtoContract]
    public class Settings
    {
        [ProtoMember(1)]
        public string uniquekey { get; set; }
        [ProtoMember(2)]
        public string group { get; set; }
        [ProtoMember(3)]
        public string version { get; set; }
        
        ServiceDataHandler data;
        public Settings()
        {
           
        }
        public Settings(string key, string Group)
        {
           
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

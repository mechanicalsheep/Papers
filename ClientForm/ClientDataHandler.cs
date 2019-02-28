
extern alias newt;
using System.Xml.Linq;
using System.Collections.Generic;
using System.IO;

using System;

namespace Client
{
    public class ClientDataHandler
    {
        

        public string path { get; set; }
        public Computer computer;
        string file;
        

     
        public ClientDataHandler(Computer Computer)
        {
            path = Directory.GetCurrentDirectory();
            computer = Computer;
            file = path + @"\" + computer.name + ".json";
        }

       public string GetKey(string str)
        {
            return computer.uniqueKey;
        }

        public void SaveComputerData()
        {
            //save as JSon                      
            var output = newt.Newtonsoft.Json.JsonConvert.SerializeObject(computer);

            File.WriteAllText(file, output);
        }



        public Computer GetComputerData()
        {

            try
            {
                var inputs = File.ReadAllText(file);

                computer = newt.Newtonsoft.Json.JsonConvert.DeserializeObject<Computer>(inputs);

                return computer;
            }
            catch(Exception er)
            {
                Console.WriteLine("Errors: "+er);
            }
            return computer;
        }
            
        
       }
    
    
}
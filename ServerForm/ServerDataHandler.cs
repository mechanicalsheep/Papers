
extern alias newt;
using System.IO;

using System;
using System.Collections.Generic;

namespace ServerForm
{
    public class ServerDataHandler
    {
        

        public string path { get; set; }
     
        string file;
        object obj;
        //string folder;
        string currentDirectory;
        string computerPath;
        
        public ServerDataHandler()
        {
            currentDirectory = Directory.GetCurrentDirectory();
            computerPath = currentDirectory + "\\Computers";
        }
     
    
        public void SaveObjectData(object obj, string FileName, string FolderName)
        {
            path = currentDirectory + "\\" + FolderName;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
          
            this.obj = obj;
            file = path + @"\" + FileName + ".json";
            var output = newt.Newtonsoft.Json.JsonConvert.SerializeObject(obj);

            File.WriteAllText(file, output);
        }
     
        public bool ComputerExists(string computer)
        {
            
            return File.Exists(computerPath+"\\"+computer+".json");
        }
        public Computer GetComputer(string computerName)
        {
            try
            {
            string compFile = computerPath + "\\" + computerName + ".json";
            var inputs = File.ReadAllText(compFile);
                object computer = newt.Newtonsoft.Json.JsonConvert.DeserializeObject<Computer>(inputs);

                return (Computer)computer;

            }
            catch (Exception err)
            {
                Console.WriteLine("ServerDataHandler.GetComputer just threw an error: " + err.Message);
                return null;
            }
        }

        public Dictionary<string,Computer> GenerateComputerList()
        {
            Dictionary<string,Computer> computerList = new Dictionary<string, Computer>();
            DirectoryInfo dir = new DirectoryInfo(computerPath);
            foreach(var file in dir.GetFiles())
            {
                var inputs = File.ReadAllText(file.FullName);

                Computer computer = newt.Newtonsoft.Json.JsonConvert.DeserializeObject<Computer>(inputs);

                computerList.Add(computer.uniqueKey,computer);
            }
            return computerList;
        }
      
            
        
       }
    
    
}
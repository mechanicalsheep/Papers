
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
     
        public bool ComputerExists(string uniqueKey)
        {
            
            return File.Exists(computerPath+"\\"+uniqueKey+".json");
        }
        public Computer GetComputer(string computerKey)
        {
            try
            {
            string compFile = computerPath + "\\" + computerKey + ".json";
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

                try
                {
                computerList.Add(computer.uniqueKey,computer);

                }
                catch
                {
                    Console.WriteLine("ERROR IN GENERATECOMPUTERLIST(): THE UNIQUE KEY POSSIBLY WAS ALREADY ADDED.");
                }
            }
            return computerList;
        }
      
            
        
       }
    
    
}

extern alias newt;
using System.IO;

using System;

namespace ServerForm
{
    public class ServerDataHandler
    {
        

        public string path { get; set; }
       // public Computer computer;
        string file;
        object obj;
        string folder;
        string currentDirectory;
        string computerPath;
        
        public ServerDataHandler()
        {
            currentDirectory = Directory.GetCurrentDirectory();
            computerPath = currentDirectory + "\\Computers";
        }
     
      /* public string GetKey()
        {
            return computer.uniqueKey;
        }*/
        public void SaveObjectData(object obj, string FileName, string FolderName)
        {
            path = currentDirectory + "\\" + FolderName;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            //computer = Computer;
            this.obj = obj;
            file = path + @"\" + FileName + ".json";
            var output = newt.Newtonsoft.Json.JsonConvert.SerializeObject(obj);

            File.WriteAllText(file, output);
        }
       /* public void SaveComputerData()
        {
            //save as JSon                      
            var output = newt.Newtonsoft.Json.JsonConvert.SerializeObject(computer);

            File.WriteAllText(file, output);
        }*/
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
        /*public void saveInitFile(string UniqueKey)
        {
            try
            {
                var initPath = currentDirectory+ "\\init.json";
                var input = newt.Newtonsoft.Json.JsonConvert.SerializeObject(UniqueKey);
                File.WriteAllText(initPath, input);
            }
            catch(Exception err)
            {
                Console.WriteLine("PaperExcceptions from SaveInitFile() Method: " + err.Message);
            }
        }
        public Computer GetComputerData()
        {

            try
            {
                var inputs = File.ReadAllText(file);

                object computer = newt.Newtonsoft.Json.JsonConvert.DeserializeObject<Computer>(inputs);

                return (Computer)computer;
            }
            catch(Exception er)
            {
                Console.WriteLine("Errors: "+er);
            }
            return computer;
        }*/
            
        
       }
    
    
}
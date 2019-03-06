﻿
extern alias newt;
using System.IO;

using System;

namespace ServerForm
{
    public class ServerDataHandler
    {
        

        public string path { get; set; }
        public Computer computer;
        string file;
        object obj;
        
        public ServerDataHandler()
        {

        }
     
      /*  public ServerDataHandler(Computer Computer)
        {
            path = Directory.GetCurrentDirectory();
            computer = Computer;
            file = path + @"\" + computer.name + ".json";
        }
        public ServerDataHandler(object obj, string FileName, string FolderName)
        {
            path = Directory.GetCurrentDirectory() + "\\" + FolderName;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            //computer = Computer;
            this.obj = obj;
            file = path + @"\" + FileName + ".json";
        }
        public ServerDataHandler(Computer Computer, string FolderName)
        {
            path = Directory.GetCurrentDirectory()+"\\"+FolderName;
            if(!Directory.Exists(path))
            Directory.CreateDirectory(path);
            computer = Computer;
            file = path + @"\" + computer.name + ".json";
        }*/

       public string GetKey()
        {
            return computer.uniqueKey;
        }
        public void SaveObjectData(object obj, string FileName, string FolderName)
        {
            path = Directory.GetCurrentDirectory() + "\\" + FolderName;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            //computer = Computer;
            this.obj = obj;
            file = path + @"\" + FileName + ".json";
            var output = newt.Newtonsoft.Json.JsonConvert.SerializeObject(obj);

            File.WriteAllText(file, output);
        }
        public void SaveComputerData()
        {
            //save as JSon                      
            var output = newt.Newtonsoft.Json.JsonConvert.SerializeObject(computer);

            File.WriteAllText(file, output);
        }
        

        public void saveInitFile(string UniqueKey)
        {
            try
            {
                var initPath = Directory.GetCurrentDirectory()+ "\\init.json";
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
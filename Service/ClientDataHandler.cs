
using Newtonsoft;
using System.IO;

using System;

namespace Service
{
    public class ClientDataHandler
    {


        public string path { get; set; }
        //public Computer computer;
        string file;
        object obj;
        //public Settings set;


        public ClientDataHandler()
        {

        }

        public void SaveObjectData(object obj, string FileName, string FolderName)
        {
            path = Directory.GetCurrentDirectory() + "\\" + FolderName;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            //computer = Computer;
            this.obj = obj;
            file = path + @"\" + FileName + ".json";

            var output = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

            File.WriteAllText(file, output);
        }







    }
    
    
}
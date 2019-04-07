extern alias newt;

using System.IO;

using System;

namespace Service
{
    public class ServiceDataHandler
    {


        public string path { get; set; }
        //public Computer computer;
        string file;
        object obj;
        //public Settings set;


        public ServiceDataHandler(string Path)
        {
            path = Path;
        }

        public void SaveObjectData(object obj, string FileName, string FolderName)
        {
         
            path =path + "\\" + FolderName;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            //computer = Computer;
            this.obj = obj;
            file = path + @"\" + FileName + ".json";

            var output = newt.Newtonsoft.Json.JsonConvert.SerializeObject(obj);

            File.WriteAllText(file, output);
        }


        public string getInit(string path)
        {
            
            
            try
            {
                var file = File.ReadAllText(path);
               var input = newt.Newtonsoft.Json.JsonConvert.DeserializeObject(file);
                return (string)input;
            
            }
            catch
            {
                return null;
            }

           

        }




    }
    
    
}
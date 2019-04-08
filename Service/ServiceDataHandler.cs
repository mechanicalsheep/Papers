extern alias newt;

using System.IO;

using System;

namespace Service
{
    public class ServiceDataHandler
    {


        public string initPath { get; set; }
        public string computerPath { get; set; }
        //public Computer computer;
        string file;
        object obj;
        //public Settings set;


        public ServiceDataHandler(string Path)
        {
            initPath = Path;
        }

        public void SaveObjectData(object obj, string FileName, string FolderName)
        {

            initPath = initPath + "\\" + FolderName;
            if (!Directory.Exists(initPath))
                Directory.CreateDirectory(initPath);
            //computer = Computer;
            this.obj = obj;
            file = initPath + @"\" + FileName + ".json";

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

        public Computer getComputer(string path)
        {
            var file = File.ReadAllText(path);
            var input = newt.Newtonsoft.Json.JsonConvert.DeserializeObject(file);
            return (Computer)input;

        }

    }
}
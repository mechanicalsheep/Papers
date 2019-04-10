extern alias newt;

using System.IO;

using System;

namespace Testings
{
    public class ServiceDataHandler
    {

        public string path { get; set; }
        public string initPath { get; set; }
        public string computerPath { get; set; }
        public string uniqueKey { get; set; }
        //public Computer computer;
        string file;
        object obj;
        //public Settings set;


        public ServiceDataHandler(string Path)
        {
            path = Path;
            initPath = path+"settings\\init.json";
            uniqueKey = getKey();

            computerPath = path + "ref\\" + uniqueKey + ".json";
        }

        public void SaveObjectData(object obj, string FileName, string FolderName)
        {

            string Path = path + "\\" + FolderName;
            if (!Directory.Exists(Path))
                Directory.CreateDirectory(Path);
            //computer = Computer;
            this.obj = obj;
            file = Path + @"\" + FileName + ".json";

            var output = newt.Newtonsoft.Json.JsonConvert.SerializeObject(obj);

            File.WriteAllText(file, output);
        }


        public string getKey()
        {


            try
            {
                var file = File.ReadAllText(initPath);
                var input = newt.Newtonsoft.Json.JsonConvert.DeserializeObject(file);
                return (string)input;

            }
            catch
            {
                return null;
            }



        }

        public Computer getComputer()
        {
            Computer input = new Computer();
            try
            {

            var file = File.ReadAllText(computerPath);
             input = newt.Newtonsoft.Json.JsonConvert.DeserializeObject<Computer>(file);
            return input as Computer;
            }
            catch(Exception err)
            {
                Console.WriteLine("getComputer() has thrown an error" + err);
                return null;
            }

        }

    }
}
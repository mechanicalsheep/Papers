extern alias newt;

using System.IO;
using System.IO.Compression;

using System;

namespace Service
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
        Computer computer;

        //create serviceDataHandler for generic writing and reading instead of data gathering based.
        public ServiceDataHandler()
        {
            //should only use for saving objectdata with path
        }
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
        public void SaveObjectDatatoPath(object Object, string Path, string FileName)
        {

            string path = Path;
            if (!Directory.Exists(Path))
                Directory.CreateDirectory(Path);
            //computer = Computer;
           object obj = Object;
            file = Path + @"\" + FileName + ".json";

            var output = newt.Newtonsoft.Json.JsonConvert.SerializeObject(obj);

            File.WriteAllText(file, output);
        }
        public void SaveOutputtoPath(object Object, string Path, string FileName)
        {

            string path = Path;
            if (!Directory.Exists(Path))
                Directory.CreateDirectory(Path);
            //computer = Computer;
            object obj = Object;
            file = Path + @"\" + FileName + ".json";

            var output = newt.Newtonsoft.Json.JsonConvert.SerializeObject(obj);

            File.WriteAllText(file, output);
        }
        public string getString(string path)
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
        public Info GetInfofromURL(string Path)
        {
            try
            {
                var file = File.ReadAllText(Path);
                var input = newt.Newtonsoft.Json.JsonConvert.DeserializeObject<Info>(file);
                return input;

            }
            catch(Exception err)
            {
                Console.WriteLine("Error retrieving Info from getIPfromURL");
                return null;
            }
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
        public Settings GetSettings(string path)
        {
            Settings settings;


            try
            {
                var inputs = File.ReadAllText(path);

                settings = newt.Newtonsoft.Json.JsonConvert.DeserializeObject<Settings>(inputs);

                return settings;
            }
            catch (Exception er)
            {
                Console.WriteLine("UNABLE TO GET SETTINGS: " + er);
            }
            return null;
        }



        public void saveInitFile(string UniqueKey)
        {
            try
            {
                var initPath = Directory.GetCurrentDirectory() + "\\init.json";
                var input = newt.Newtonsoft.Json.JsonConvert.SerializeObject(UniqueKey);
                File.WriteAllText(initPath, input);
            }
            catch (Exception err)
            {
                Console.WriteLine("PaperExcceptions from SaveInitFile() Method: " + err.Message);
            }
        }
        public Computer GetComputerData()
        {

            try
            {
                //file = Directory.GetCurrentDirectory() + "\\ref" + computer.name + ".json";
                var inputs = File.ReadAllText(file);

                computer = newt.Newtonsoft.Json.JsonConvert.DeserializeObject<Computer>(inputs);

                return computer;
            }
            catch (Exception er)
            {
                Console.WriteLine("Errors: " + er);
            }
            return computer;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    class Commands
    {
        string path;
        public Commands(string Path)
        {
            path = Path;
        }
        public string doCommand(string command)
        {
            string message="";
            switch (command)
            {
                case "restartService":
                {
                        try
                        {

                            using (Process p = new Process())
                            {

                                p.StartInfo.FileName = path + "\\tools\\restarter.bat";
                            p.StartInfo.Verb = "runas";
                            p.StartInfo.UseShellExecute = false;
                            p.StartInfo.CreateNoWindow = true;
                            p.Start();

                            message = "Service has been restarted";
                            }
                        }
                        catch(Exception err)
                        {
                            message = "Error restarting service " + err;
                        }
                        break;
                }
                case "hello":
                    {
                        message= "Hello!";
                        break;
                    }
            }
            return message;
            
        }
    }
}

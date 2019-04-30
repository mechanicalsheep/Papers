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
        public List<string> doCommand(string command)
        {
            List<string> message=new List<string>();
            switch (command.StartsWith("choco")? "choco": command)
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

                            message.Add("Service has been restarted");
                            }
                        }
                        catch(Exception err)
                        {
                            message.Add("Error restarting service " + err);
                        }
                        break;
                }
                case "hello":
                    {
                        message.Add("Hello!");
                        break;
                    }
                case "choco":
                    {
                        using (Process p = new Process())
                        {

                            p.StartInfo.UseShellExecute = false;
                        p.StartInfo.RedirectStandardOutput = true;
                        p.StartInfo.RedirectStandardError = true;
                        p.StartInfo.CreateNoWindow = true;
                            //string strCmdText = "/C choco list -l";
                            string strCmdText = "/C ";
                            // Correct way to launch a process with arguments
                        p.StartInfo.FileName = "CMD.exe";

                        p.StartInfo.Arguments = strCmdText + command;

                        List<string> outList = new List<string>();

                        p.OutputDataReceived += new DataReceivedEventHandler((s, e) =>
                        {
                            try
                            {
                                if (!e.Data.Contains("packages installed"))
                                {
                                    Console.WriteLine(e.Data);
                                    outList.Add(e.Data);

                                }
                            }
                            catch (Exception err)
                            {
                                Console.WriteLine("Dataoutput in the process has returned an exception:  " + err.Message);
                            }

                        });
                        p.ErrorDataReceived += new DataReceivedEventHandler((s, e) =>
                        {
                            Console.WriteLine("ErrorHandler " + e.Data);
                            //outList.Add(e.Data);
                            if (e.Data != null)
                                if (e.Data.Contains($"'choco' is not recognized"))
                                {
                                    outList.Add("CHOCO IS NOT INSTALLED ON THIS DEVICE");
                                    //installChocolatey(credential);
                                }
                                else
                                {
                                    outList.Add(e.Data);
                                }
                        });
                        p.Start();
                        p.BeginOutputReadLine();
                        p.BeginErrorReadLine();
                        p.WaitForExit();
                        p.Close();

                            return outList;
                        }
                        //message = "Choco Command was: " + command;
                        //break;
                    }
            }

           
            return message;
            
        }
    }
}

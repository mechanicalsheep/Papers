using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Management.Automation;
//using System.Management.Automation.Runspaces;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Client
{
    class Installer
    {
        /*static void main (string[] args)
        {
            Install();
        }*/
        
        public void Install(string Path)
        {
           string commandString;
            commandString = "/C choco --version";
            
            using(Process process = new Process())
            {
                ProcessStartInfo startInfo = new ProcessStartInfo("cmd.exe")
                {
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                    WorkingDirectory = @"d:\"
                    Arguments=
                };
                // event handlers for output & error
                process.OutputDataReceived += p_OutputDataReceived;
                process.ErrorDataReceived += p_ErrorDataReceived;

                // start process
                process.Start(commandString);
                // send command to its input
                process.StandardInput.Write("dir" + process.StandardInput.NewLine);
                //wait
                process.WaitForExit();
            }
            
            //string path=Path;
            //PowerShell powerShell = null;

            //try
            //{
            //    using (powerShell = PowerShell.Create())
            //    {
            //        powerShell.AddScript($"setup=Start-Process '{path}' -ArgumentList ' /S ' -Wait -PassThru");


            //         foreach (PSObject outputItem in powerShell.Invoke())
            //         {
            //             string test = $"Full name: {outputItem.BaseObject.GetType().FullName}";
            //             Debug.WriteLine(test);
            //        Console.WriteLine((test));
            //         }

            //    }
            //}
            //catch (Exception err)
            //{
            //    string message = $"Error: {err.Message}";
            //    Console.WriteLine(message);


            //}
        }
        static void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Process p = sender as Process;
            if (p == null)
                return;
            Console.WriteLine(e.Data);
        }

        static void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Process p = sender as Process;
            if (p == null)
                return;
            Console.WriteLine(e.Data);
        }
    }
}

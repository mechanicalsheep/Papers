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
        
        public List<string> Install(string Path)
        {
            //string commandString;
            // commandString = "/C choco --version";

            // using(Process process = new Process())
            // {
            //     ProcessStartInfo startInfo = new ProcessStartInfo("cmd.exe")
            //     {
            //         RedirectStandardInput = true,
            //         UseShellExecute = false,
            //         WorkingDirectory = @"d:\",
            //         FileName="cmd.exe",
            //         Arguments = commandString
            //     };
            //     // event handlers for output & error
            //     process.OutputDataReceived += p_OutputDataReceived;
            //     process.ErrorDataReceived += p_ErrorDataReceived;

            //     // start process
            //     process.Start();
            //     // send command to its input
            //     process.StandardInput.Write("dir" + process.StandardInput.NewLine);
            //     //wait
            //     process.WaitForExit();
            // }
            string ipAddress="127.0.0.1";
           Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            string strCmdText;
            //strCmdText = "/C tracert -d " + "127.0.0.1";
            strCmdText = "/C ipconfig";
            // Correct way to launch a process with arguments
            p.StartInfo.FileName = "CMD.exe";
           
            p.StartInfo.Arguments = strCmdText;

            List<string> outList = new List<string>();

            p.OutputDataReceived += new DataReceivedEventHandler((s, e) =>
            {
                Console.WriteLine(e.Data);
                outList.Add(e.Data);
            });
            p.Start();
            p.BeginOutputReadLine();

            //string output = p.StandardOutput.ReadToEnd();

            //string output2;
            
            //while ((output2 = p.StandardOutput.ReadLine()) != null)
            //{
            //    outList.Add(output2);
            //    Console.WriteLine(output2);
            //}
            p.WaitForExit();
            return outList ;
            
        }

        
    }
}

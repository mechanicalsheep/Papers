using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;



namespace Client
{
    class Installer
    {
        /*static void main (string[] args)
        {
            Install();
        }*/
        
        public List<string> Install(string Command, NetworkCredential credential)
        {
            // Retrieve the Windows account token for the current user.
           // IntPtr logonToken = WindowsIdentity.GetCurrent().Token;


           // WindowsIdentity identity = new WindowsIdentity(logonToken);
            //identity.Impersonate();
           
          
           Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.Verb = "runas";
            if (credential.Password != "" && credential.UserName != "")
            {
                p.StartInfo.UserName = credential.UserName;
                p.StartInfo.Password = credential.SecurePassword;
                p.StartInfo.Domain = credential.Domain;
            }
                string strCmdText;
           
            strCmdText = "/C choco "+Command;
            // Correct way to launch a process with arguments
            p.StartInfo.FileName = "CMD.exe";
           
            p.StartInfo.Arguments = strCmdText;

            List<string> outList = new List<string>();

            p.OutputDataReceived += new DataReceivedEventHandler((s, e) =>
            {
                Console.WriteLine(e.Data);
                outList.Add(e.Data);
            });
            p.ErrorDataReceived += new DataReceivedEventHandler((s, e) =>
            {
                Console.WriteLine("ErrorHandler" + e.Data);
                //outList.Add(e.Data);
                if(e.Data!=null)
                if (e.Data.Contains($"'choco' is not recognized"))
                {
                    outList.Add("CHOCO IS NOT INSTALLED ON THIS DEVICE");
                }
            });
            Console.WriteLine($"Username: {p.StartInfo.UserName}  Password: {p.StartInfo.Password}");
            p.Start();
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();
            p.WaitForExit();
            p.Close();
           
            return outList;
            
        }

        
    }
}

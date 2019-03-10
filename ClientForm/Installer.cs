using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.Principal;

namespace Client
{
    class Installer
    {
        /*static void main (string[] args)
        {
            Install();
        }*/
        ClientForm form;
        public Installer(ClientForm Form)
        {
            form = Form;
        }
        public List<string> getChocolateyInstallations()
        {
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            string strCmdText = "/C choco list -l";
            // Correct way to launch a process with arguments
            p.StartInfo.FileName = "CMD.exe";

            p.StartInfo.Arguments = strCmdText;

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
                catch(Exception err)
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
           
            if (credential.Password != "" && credential.UserName != "")
            {
                p.StartInfo.UserName = credential.UserName;
                p.StartInfo.Password = credential.SecurePassword;
                if (credential.Domain != "")
                    p.StartInfo.Domain = credential.Domain;
                else
                    p.StartInfo.Domain=Environment.UserDomainName;
            }
            else
            {
                //TODO: WHAT IF ADMIN NOT WORKING 
                //do SOMETHING HERE!
            }
            p.StartInfo.Verb = "runas";
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
                form.sendMessage(e.Data);
            });
            p.ErrorDataReceived += new DataReceivedEventHandler((s, e) =>
            {
                Console.WriteLine("ErrorHandler " + e.Data);
                //outList.Add(e.Data);
                if(e.Data!=null)
                if (e.Data.Contains($"'choco' is not recognized"))
                {
                    outList.Add("CHOCO IS NOT INSTALLED ON THIS DEVICE");
                        installChocolatey(credential);
                }
                    else
                    {
                        outList.Add(e.Data);
                    }
            });

            
            Console.WriteLine($"Username: {p.StartInfo.UserName}  Password: {p.StartInfo.Password} Domain: {p.StartInfo.Domain}");
            p.Start();
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();
            p.WaitForExit();
            p.Close();
           
            return outList;
            
        }
        void installChocolatey(NetworkCredential Credential)
        {
            Console.WriteLine("Starting to install chocolatey");
            
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;

            if (Credential.Password != "" && Credential.UserName != "")
             {
                 p.StartInfo.UserName = Credential.UserName;
                 p.StartInfo.Password = Credential.SecurePassword;
                if (Credential.Domain != "")
                    p.StartInfo.Domain = Credential.Domain;
                else
                    p.StartInfo.Domain = Environment.UserDomainName;
             }
             
            
            string command = "@\"%SystemRoot%\\System32\\WindowsPowerShell\\v1.0\\powershell.exe\" -NoProfile -InputFormat None -ExecutionPolicy Bypass -Command \"iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))\" && SET \"PATH=%PATH%;%ALLUSERSPROFILE%\\chocolatey\\bin\"";
       
            Console.WriteLine(command);

          
            // p.StartInfo.Verb = "runas";
            p.StartInfo.FileName = @"cmd.exe";
            p.StartInfo.Arguments = "/C "+ command;
            p.EnableRaisingEvents = true;
            p.Exited += (sender, e) => 
            {
                Console.WriteLine("PROCESS COMPLETE");
                form.sendMessage("Choco is now installed");
                
            };
            Console.WriteLine($"Running {p.StartInfo.FileName} in {p.StartInfo.WorkingDirectory} using User: {p.StartInfo.UserName} Password:{p.StartInfo.PasswordInClearText} Domain: {p.StartInfo.Domain}");
            form.writeline($"Running {p.StartInfo.FileName} in {p.StartInfo.WorkingDirectory} using User: {p.StartInfo.UserName} Password:{p.StartInfo.PasswordInClearText} Domain: {p.StartInfo.Domain}");
            p.Start();
            p.WaitForExit();

    



        }


    }
}

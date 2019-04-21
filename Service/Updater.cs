//using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;


namespace Service
{
   
    class Updater
    {

        ServiceController service;
        Info info;
        string previousVersion;
        ServiceDataHandler data;
        //PaperService paper;
     public Updater(Info info, string PreviousVersion)
        {
           // this.paper = paper;
            data = new ServiceDataHandler();
            this.info = info;
            previousVersion = PreviousVersion;
      
            service = new ServiceController("PaperService");


        }
        public void CallUpdater()
        {

            RunUpdate();

                Process p = new Process();
                p.StartInfo.FileName = @"D:\projects\Papers\Service\bin\Debug\tools\updater.bat";
                p.StartInfo.Verb = "runas";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
            List<string> output = new List<string>();
            p.OutputDataReceived += new DataReceivedEventHandler((s, e) =>
            {
                output.Add(e.Data);


            });
            p.ErrorDataReceived += new DataReceivedEventHandler((s, e) =>
            {
                output.Add("Error!!: " + e.Data);

               
            });

            p.Start();
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();
           // p.WaitForExit(Convert.ToInt32(TimeSpan.FromSeconds(15).TotalMilliseconds));
            p.Close();
            data.SaveObjectDatatoPath(output, @"D:\projects\Papers\Service\bin\Debug\", "OUPUT");



        }
        public void RunUpdate()
        {
            //TimeSpan time = new TimeSpan(0,0,10);
            string newVersionPath = $@"\\{ info.ip}\PaperClient\{info.version}.zip";
           // Uri uri = new Uri("www.google.com");
            //data.SaveObjectDatatoPath(newVersionPath, @"D:\projects\Papers\Service\bin\Debug\", "xx");
            //data.SaveObjectDatatoPath(uri, @"D:\projects\Papers\Service\bin\Debug\", "xx");
            /*if (service.Status == ServiceControllerStatus.StartPending)
            {
              service.WaitForStatus(ServiceControllerStatus.Running, time);
            }*/
          ///if(service.Status != ServiceControllerStatus.Stopped && service.Status != ServiceControllerStatus.StopPending)
           /// {
               // string test = "TEST";
               // data.SaveObjectDatatoPath(test, @"D:\projects\Papers\Service\bin\Debug\", "VersionNow");
               // service.Stop();
                //service.WaitForStatus(ServiceControllerStatus.Stopped);
                

                try
                {
                    string filePath = @"D:\projects\Papers\Service\bin\Debug\";
                    DirectoryInfo dir = new DirectoryInfo(filePath);
                    string archiveFolder = filePath + "Archive\\" + previousVersion;
                    string zipPathwithFile = $"{filePath}\\temp\\{info.version}.zip";
                    DirectoryInfo archiveDirectory = new DirectoryInfo( archiveFolder);
                    if (!archiveDirectory.Exists)
                    {
                        archiveDirectory.Create();
                    }
                    if (!Directory.Exists(filePath + "temp"))
                    {
                        Directory.CreateDirectory(filePath + "temp");
                    }
                    if (!Directory.Exists(filePath + "Archive"))
                    {
                        Directory.CreateDirectory(filePath + "Archive");
                    }
                   /* foreach(FileInfo file in dir.GetFiles())
                    {
                        file.CopyTo(archiveFolder);
                    }*/
                    using(WebClient wc = new WebClient())
                    {
                        
                        wc.DownloadFile(newVersionPath,zipPathwithFile);


                    }

                }
                catch
                {

                }
               
               // string message = "Computer version is now" + version;
                //data.SaveObjectDatatoPath(message, @"D:\projects\Papers\Service\bin\Debug\", "VersionNow");
                /*service.Start();
               service.WaitForStatus(ServiceControllerStatus.Running);*/
           // }
        }

    }
}

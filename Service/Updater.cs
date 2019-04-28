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
        string newVersionPath;
        ServiceController service;
        Info info;
        string previousVersion;
        ServiceDataHandler data;
        string path;
        EventLog eventLog = new EventLog();
        //PaperService paper;
     public Updater(string Path, Info info, string PreviousVersion)
        {
            eventLog.Source = "Paper";
            eventLog.Log = "PaperLog";
            // this.paper = paper;
            path = Path;
            data = new ServiceDataHandler();
            this.info = info;
            newVersionPath = $@"\\{ info.ip}\PaperClient\Updates\{info.version}.zip";
            previousVersion = PreviousVersion;
      
            service = new ServiceController("PaperService");


        }
        public void CallUpdater()
        {

            RunUpdate();
            try
            {
using(Process p = new Process())
                {

            //Process p = new Process();
                p.StartInfo.FileName = path+@"\tools\updater.bat";
                p.StartInfo.Verb = "runas";
                p.StartInfo.UseShellExecute = false;
               // p.StartInfo.RedirectStandardOutput = true;
                //p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
            List<string> output = new List<string>();
           /* p.OutputDataReceived += new DataReceivedEventHandler((s, e) =>
            {
                output.Add(e.Data);

            eventLog.WriteEntry(e.Data);

            });
            p.ErrorDataReceived += new DataReceivedEventHandler((s, e) =>
            {
                output.Add("Error!!: " + e.Data);
                eventLog.WriteEntry("Error: "+e.Data);

            });
            */

            p.Start();
          //  p.BeginOutputReadLine();
          //  p.BeginErrorReadLine();
         // p.WaitForExit(Convert.ToInt32(TimeSpan.FromSeconds(15).TotalMilliseconds));
            //p.Close();
             //   p.Dispose();
                }
           // data.SaveObjectDatatoPath(output, path+@"\", "OUPUT");

            }
            catch(Exception err)
            {
                eventLog.WriteEntry("Error starting batch: "+ err);
            }

        }
        public void RunUpdate()
        {
            //TimeSpan time = new TimeSpan(0,0,10);
           
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
                    string filePath = path+@"\";
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
                    /* using (StreamWriter file =
         new StreamWriter(Directory.GetCurrentDirectory()+@"\debug.txt", true))
                     {
                         file.WriteLine("Fourth line");
                     }*/
                    try
                    {
                        // wc.Credentials = System.Net.CredentialCache.DefaultCredentials;
                        //string updater = "https://drive.google.com/file/d/1jrJ5mouyjt_IXal7maXsV_gt_3zSLT6L/view?usp=sharing";
                        string updater = "https://drive.google.com/uc?export=download&id=1jrJ5mouyjt_IXal7maXsV_gt_3zSLT6L";
                        //wc.DownloadFile(newVersionPath,zipPathwithFile);
                    wc.DownloadFile(updater, zipPathwithFile);
                    }
                    catch(Exception err)
                    {
                        using (StreamWriter file =
         new StreamWriter($@"{ path }\debug.txt", true))
                        {
                            file.WriteLine("There was an error processing the download: "+ err);
                        }
                    }


                    }

                }
                catch(Exception err)
                {
                eventLog.WriteEntry("Error updating! " + err);
                }
               
               // string message = "Computer version is now" + version;
                //data.SaveObjectDatatoPath(message, @"D:\projects\Papers\Service\bin\Debug\", "VersionNow");
                /*service.Start();
               service.WaitForStatus(ServiceControllerStatus.Running);*/
           // }
        }

    }
}

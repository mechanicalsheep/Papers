//using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
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
        string path = @"C:\Program Files\\PaperClient";
        DirectoryInfo dir;
        ServiceController service;
        Info info;
        string previousVersion;
        ServiceDataHandler data;
        PaperService paper;
     public Updater(PaperService paper)
        {
            this.paper = paper;
            data = new ServiceDataHandler();
            this.info = paper.info;
            this.previousVersion = paper.computer.version;
      
            service = new ServiceController("PaperService");

            
        }  
        public void RunUpdate()
        {
            TimeSpan time = new TimeSpan(0,0,10);
            Uri uri = new Uri($@"\\{info.ip}\PaperClient\");
            /*if (service.Status == ServiceControllerStatus.StartPending)
            {
              service.WaitForStatus(ServiceControllerStatus.Running, time);
            }*/
          if(service.Status != ServiceControllerStatus.Stopped && service.Status != ServiceControllerStatus.StopPending)
            {
                string test = "TEST";
                data.SaveObjectDatatoPath(test, @"D:\projects\Papers\Service\bin\Debug\", "VersionNow");
                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped);
                data.SaveObjectDatatoPath(test, @"D:\projects\Papers\Service\bin\Debug\", "VersionNow");

                try
                {
                    string filePath = @"D:\projects\Papers\Service\bin\Debug\";
                    DirectoryInfo dir = new DirectoryInfo(filePath);
                    string archiveFolder = filePath + "Archive\\" + previousVersion;
                    string zipPath = $"{filePath}{ info.version}.zip";
                    DirectoryInfo archiveDirectory = new DirectoryInfo( archiveFolder);
                    if (!archiveDirectory.Exists)
                    {
                        archiveDirectory.Create();
                    }
                    foreach(FileInfo file in dir.GetFiles())
                    {
                        file.CopyTo(archiveFolder);
                    }
                    using(WebClient wc = new WebClient())
                    {
                        wc.DownloadFile(uri,zipPath);

                        ZipFile.ExtractToDirectory(zipPath, filePath);
                    }
                    
                }
                catch
                {

                }
                paper.computer.version = info.version;
                string message = "Computer version is now" + paper.computer.version;
                data.SaveObjectDatatoPath(message, @"D:\projects\Papers\Service\bin\Debug\", "VersionNow");
                /*service.Start();
               service.WaitForStatus(ServiceControllerStatus.Running);*/
            }
        }

    }
}

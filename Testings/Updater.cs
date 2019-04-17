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
        public void RunUpdate()
        {
            //TimeSpan time = new TimeSpan(0,0,10);
            string newVersionPath = $@"\\{info.ip}\PaperClient\{info.version}.zip";
           // Uri uri = new Uri("www.google.com");
            data.SaveObjectDatatoPath(newVersionPath, @"D:\projects\Papers\Service\bin\Debug\", "xx");
            //data.SaveObjectDatatoPath(uri, @"D:\projects\Papers\Service\bin\Debug\", "xx");
            /*if (service.Status == ServiceControllerStatus.StartPending)
            {
              service.WaitForStatus(ServiceControllerStatus.Running, time);
            }*/
          if(service.Status != ServiceControllerStatus.Stopped && service.Status != ServiceControllerStatus.StopPending)
            {
                string test = "TEST";
               // data.SaveObjectDatatoPath(test, @"D:\projects\Papers\Service\bin\Debug\", "VersionNow");
                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped);
                

                try
                {
                    string filePath = @"D:\projects\Papers\Service\bin\Debug\";
                    DirectoryInfo dir = new DirectoryInfo(filePath);
                    string archiveFolder = filePath + "Archive\\" + previousVersion;
                    string zipPathwithFile = $"{filePath}{ info.version}.zip";
                    DirectoryInfo archiveDirectory = new DirectoryInfo( archiveFolder);
                    if (!archiveDirectory.Exists)
                    {
                        archiveDirectory.Create();
                    }
                   /* foreach(FileInfo file in dir.GetFiles())
                    {
                        file.CopyTo(archiveFolder);
                    }*/
                    using(WebClient wc = new WebClient())
                    {
                        
                        wc.DownloadFile(newVersionPath,zipPathwithFile);

                       /*ZipArchive archive= ZipFile.OpenRead(zipPathwithFile);
                        foreach(var zip in archive.Entries)
                        {
                            zip.ExtractToFile(filePath + zip.FullName, true);
                        }*/
                       //ZipFile.ExtractToDirectory(zipPathwithFile, filePath);
                    }

                }
                catch
                {

                }
                //paper.computer.version = info.version;
               // string message = "Computer version is now" + paper.computer.version;
               // data.SaveObjectDatatoPath(message, @"D:\projects\Papers\Service\bin\Debug\", "VersionNow");
                /*service.Start();
               service.WaitForStatus(ServiceControllerStatus.Running);*/
            }
        }

    }
}

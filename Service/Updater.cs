using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
   
    class Updater
    {
        string path = @"C:\Program Files\PaperClient\";
        DirectoryInfo dir;
        ServiceController service;
     public Updater()
        {

            dir = new DirectoryInfo(path);
            if (!dir.Exists)
            {
                dir.Create();
            }
            service = new ServiceController("PaperService");

            
        }  
        public void runUpdate()
        {
            if(service.Status != ServiceControllerStatus.Stopped && service.Status != ServiceControllerStatus.StopPending)
            {
                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped);
            }
        }

        public void RunUpate()
        {
            
        }
    }
}

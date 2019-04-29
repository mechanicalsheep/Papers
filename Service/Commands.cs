using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    class Commands
    {
        public Commands()
        {

        }
        public void doCommand(string command)
        {
            switch (command)
            {
                case "restartService":
                {
                        ServiceController sc = new ServiceController("PaperService");
                        sc.Stop();
                        sc.WaitForStatus(ServiceControllerStatus.Stopped);

                        sc.Start();
                        break;
                }
            }
        }
    }
}

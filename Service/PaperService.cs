using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Service
{
    public partial class PaperService : ServiceBase
    {
        private int eventId = 1;
        public PaperService()
        {
            InitializeComponent();
           
            eventLog1 = new EventLog();
            if (!EventLog.SourceExists("MySource"))
            {
                EventLog.CreateEventSource("MySource", "MyNewLog");
            }

            eventLog1.Source = "MySource";
            eventLog1.Log="MyNewLog";

        }

        private void OnTimer(object sender, ElapsedEventArgs e)
        {
            eventLog1.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);
        }

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("In OnStart");

            // Set up a timer that triggers every minute.
            Timer timer = new Timer();

            timer.Interval = 60000; // 60 seconds
            timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
            timer.Start();
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("In OnStop.");
        }
    }
}

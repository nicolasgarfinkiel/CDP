using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.Threading;

namespace CartaDePorte.Service
{
    public partial class ProcessorService : ServiceBase
    {
        public ProcessorService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            ProcessorServiceHelper.Start();
        }

        protected override void OnStop()
        {
            ProcessorServiceHelper.Stop();
        }
    }
}
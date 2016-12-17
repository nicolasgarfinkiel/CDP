using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Threading;
using CartaDePorte.CronTasks;

namespace CartaDePorte.Service
{
    internal class ProcessorServiceHelper
    {
        internal static void Start()
        {
            CronTasksService.Start();
        }

        internal static void Stop()
        {
            CronTasksService.Stop();
        }
    }
}
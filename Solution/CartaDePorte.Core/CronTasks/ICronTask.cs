using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;

namespace CartaDePorte.CronTasks
{
    public interface ICronTask
    {
        void DoTask(DateTime? lastRun);
    }
}
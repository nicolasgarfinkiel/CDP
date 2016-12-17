using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

using CartaDePorte.Common;
using CartaDePorte.Core.DAO;

namespace CartaDePorte.CronTasks
{
    public class TaskRenovarTokenAfip : ICronTask
    {
        public void DoTask(DateTime? lastRun)
        {

            Tools.Logger.Info("TaskRenovarTokenAfip Inicio");


            //SOLO ARGENTINA (CRESUD?)

            try
            {

                AfipAuthDAO.Instance.RenovarTokenAfip();

            }
            catch (Exception ex)
            {
                Tools.Logger.Error(ex);
            }


            Tools.Logger.Info("TaskRenovarTokenAfip Fin");

        }


    }
}

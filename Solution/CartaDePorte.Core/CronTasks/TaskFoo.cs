using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

using CartaDePorte.Common;
using CartaDePorte.Core.DAO;

namespace CartaDePorte.CronTasks
{
    public class TaskFoo : ICronTask
    {
        public void DoTask(DateTime? lastRun)
        {

            Tools.Logger.Info("TaskFoo Inicio");

            try
            {
                var lista = EstablecimientoDAO.Instance.GetAll();


                var item = lista[0];


                EstablecimientoDAO.Instance.SaveOrUpdate(item);


            }
            catch (Exception ex)
            {
                Tools.Logger.Error(ex);
            }


            Tools.Logger.Info("TaskFoo Fin");

        }


    }
}

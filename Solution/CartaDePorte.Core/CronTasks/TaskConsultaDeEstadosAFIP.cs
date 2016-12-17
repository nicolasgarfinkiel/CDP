using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

using CartaDePorte.Common;
using CartaDePorte.Core.DAO;
using CartaDePorte.Core;

namespace CartaDePorte.CronTasks
{
    public class TaskConsultaDeEstadosAFIP : ICronTask
    {
        public void DoTask(DateTime? lastRun)
        {

            Tools.Logger.Info("TaskConsultaDeEstadosAFIP Inicio");

            //SOLO GRUPO CRESUD
            //TODO INICIAR SESION GRUPO CRESUD
            if (App.Usuario.IdEmpresa != App.ID_EMPRESA_CRESUD)
            {
                var usuario = new UsuarioFull();
                usuario.Nombre = "CartaDePorte.Service";
                usuario.Empresas.Add(App.ID_EMPRESA_CRESUD, EmpresaDAO.Instance.GetOneAdmin(App.ID_EMPRESA_CRESUD));
                usuario.SetEmpresa(App.ID_EMPRESA_CRESUD);
                App.Usuario = usuario;
            }


            try
            {

                SolicitudDAO.Instance.ConsultaDeEstadosAFIP();

            }
            catch (Exception ex)
            {
                Tools.Logger.Error(ex);
            }


            Tools.Logger.Info("TaskConsultaDeEstadosAFIP Fin");

        }


    }
}

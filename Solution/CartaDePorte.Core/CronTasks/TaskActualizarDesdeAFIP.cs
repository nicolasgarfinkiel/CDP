using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Configuration;

using CartaDePorte.Common;
using CartaDePorte.Core.Servicios;
using CartaDePorte.Core;
using CartaDePorte.Core.DAO;

namespace CartaDePorte.CronTasks
{
    public class TaskActualizarDesdeAFIP : ICronTask
    {
        public void DoTask(DateTime? lastRun)
        {

            Tools.Logger.Info("TaskActualizarDesdeAFIP Inicio");

            try
            {
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

                wsAfip_v3 wsa = new wsAfip_v3();

                wsa.ActualizarCosecha();
                wsa.ActualizarEspecie();

            }
            catch  (Exception ex)
            {
                Tools.Logger.Error(ex);
            }



            Tools.Logger.Info("TaskActualizarDesdeAFIP Fin");

        }


    
    }
}

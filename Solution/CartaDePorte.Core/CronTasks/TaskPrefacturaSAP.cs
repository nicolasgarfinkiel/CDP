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
    public class TaskPrefacturaSAP : ICronTask
    {
        public void DoTask(DateTime? lastRun)
        {

            Tools.Logger.Info("TaskPrefacturaSAP Inicio");

            //TODO POR EMPRESA
            //TODO INICIAR SESION POR EMPRESA?

            var empresas = EmpresaDAO.Instance.GetAllAdmin();

            foreach (var empresa in empresas)
            {
                try
                {
                    Tools.Logger.InfoFormat("TaskPrefacturaSAP Empresa : {0}", empresa.IdEmpresa);
                    
                    var usuario = new UsuarioFull();
                    usuario.Nombre = "CartaDePorte.Service";
                    usuario.Empresas.Add(empresa.IdEmpresa, empresa);
                    usuario.SetEmpresa(empresa.IdEmpresa);
                    App.Usuario = usuario;


                    String ControlDiaSemana = ConfigurationManager.AppSettings["ControlDiaSemana"];

                    if (ControlDiaSemana.Equals("true"))
                    {
                        if (EsDiaHabil())
                        {
                            if (EsLunes())
                            {
                                if (SuperaLaHora(8))
                                {
                                    wsSAP ws = new wsSAP();
                                    ws.PrefacturaSAP(true); // true= busca a anular
                                    ws.PrefacturaSAP(false); // false = busca a prefacturar                                        
                                }
                            }
                            else
                            {
                                wsSAP ws = new wsSAP();
                                ws.PrefacturaSAP(true); // true= busca a anular
                                ws.PrefacturaSAP(false); // false = busca a prefacturar                                    
                            }
                        }

                    }
                    else
                    {

                        var ws = new wsSAP();
                        ws.PrefacturaSAP(true); // true= busca a anular
                        ws.PrefacturaSAP(false); // false = busca a prefacturar

                    }

                }
                catch (Exception ex)
                {
                    Tools.Logger.Error(ex);
                }

            }




            Tools.Logger.Info("TaskPrefacturaSAP Fin");

        }




        private Boolean EsDiaHabil()
        {
            Boolean habil = true;

            DayOfWeek dia = DateTime.Now.DayOfWeek;
            if (dia == DayOfWeek.Saturday || dia == DayOfWeek.Sunday)
                habil = false;

            return habil;

        }

        private Boolean EsLunes()
        {
            DayOfWeek dia = DateTime.Now.DayOfWeek;
            if (dia == DayOfWeek.Monday)
                return true;

            return false;
        }

        private Boolean SuperaLaHora(int hora)
        {

            if (DateTime.Now.Hour >= hora)
                return true;

            return false;


        }

    }
}

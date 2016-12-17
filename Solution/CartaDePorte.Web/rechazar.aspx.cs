using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using CartaDePorte.Core.Utilidades;
using System.Runtime.InteropServices;
using System.Security.Principal;
using CartaDePorte.Core.Servicios;
using CartaDePorte.Core.Domain;
using CartaDePorte.Core.DAO;
using CartaDePorte.Core.Domain.Seguridad;
using CartaDePorte.Core;

namespace CartaDePorte.Web
{

    public partial class rechazar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            
            if (!App.UsuarioTienePermisos("BaseDeDatos"))
            {
                Response.Redirect("~/SinAutorizacion.aspx");
                return;
            }


            if (Request["idsolis"] != null)
            {
                int idSolicitud = Convert.ToInt32(Request["idsolis"]);

                var wsa = new wsAfip_v3();
                Solicitud sol = SolicitudDAO.Instance.GetOne(idSolicitud);
                var resulArribo = wsa.confirmarArribo(sol);
                var resulRechazo = wsa.rechazarCTG(sol);

                bool errores = false;
                if (resulArribo.arrayErrores.Count() > 0)
                {
                    errores = true;
                    lblEstadoRechazo.Text = "Errores en Arribo:<br>";
                    foreach (string dato in resulArribo.arrayErrores)
                    {
                        lblEstadoRechazo.Text += dato + "<br>";
                    }
                }

                if (!errores)
                {
                    lblEstadoRechazo.Text += "Arribo OK<br/>";
                }

                errores = false;
                if (resulRechazo.arrayErrores.Count() > 0)
                {
                    errores = true;
                    lblEstadoRechazo.Text = "Errores en Rechazo:<br>";
                    foreach (string dato in resulRechazo.arrayErrores)
                    {
                        lblEstadoRechazo.Text += dato + "<br>";
                    }
                }

                if (!errores)
                {
                    lblEstadoRechazo.Text += "Rechazo OK<br/>";
                }

                var resulEstados = wsa.consultarCTG(DateTime.Now.AddDays(-1));
                if (resulRechazo.arrayErrores.Count() > 0)
                {
                    lblEstadoRechazo.Text += "No esta disponible la consulta con AFIP, intente nuevamente mas tarde.<br/>";
                }
                else
                {
                    if (resulEstados.arrayDatosConsultarCTG.Count() > 0)
                    {
                        foreach (var dato in resulEstados.arrayDatosConsultarCTG)
                        {
                            if (dato.estado.Equals("Rechazado"))
                            {   
                                Solicitud solTmp = SolicitudDAO.Instance.GetSolicitudByCTG(dato.ctg.Replace(".", ""));
                                if (solTmp.EstadoEnAFIP != Enums.EstadoEnAFIP.Rechazado)
                                {
                                    lblEstadoRechazo.Text += "Solicitud en estado rechazado: " + sol.IdSolicitud.ToString() + ".<br/>";
                                    solTmp.EstadoEnAFIP = Enums.EstadoEnAFIP.Rechazado;
                                    SolicitudDAO.Instance.SaveOrUpdate(solTmp);
                                }
                            }
                        }
                    }
                }
            }
            else {

                lblEstadoRechazo.Text = "No hay acciones a realizar";
            }



        }






    }
}

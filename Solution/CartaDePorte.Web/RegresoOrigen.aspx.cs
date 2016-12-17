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
using CartaDePorte.Core.Domain;
using CartaDePorte.Core.DAO;
using CartaDePorte.Core.Servicios;
using System.Drawing;
using CartaDePorte.Core.Domain.Seguridad;
using CartaDePorte.Core;

namespace CartaDePorte.Web
{

    public partial class RegresoOrigen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            
            if (!App.UsuarioTienePermisos("Alta Solicitud"))
            {
                Response.Redirect("~/SinAutorizacion.aspx");
                return;
            }



            int SolicitudID = Convert.ToInt32(Request["Id"]);
            Solicitud sol = SolicitudDAO.Instance.GetOne(SolicitudID);

            txtNroCartaDePorte.Text = sol.NumeroCartaDePorte;
            txtCtg.Text = sol.Ctg;
            txtEstablecimientoOrigen.Text = sol.IdEstablecimientoProcedencia.Descripcion;
            txtEstablecimientoDestino.Text = sol.IdEstablecimientoDestino.Descripcion;
            txtChofer.Text = sol.Chofer.Apellido + ", " + sol.Chofer.Nombre;
            txtFechaCreacion.Text = sol.FechaDeCarga.Value.ToString("dd/MM/yyyy HH:mm:ss");

        }

        protected void btnRAO_Click(object sender, EventArgs e)
        {


            string testRegresoAOrigen = System.Configuration.ConfigurationSettings.AppSettings["testRegresoAOrigen"];
            if (testRegresoAOrigen != null && testRegresoAOrigen.ToLower().Equals("true"))
            {
                if (Request["Id"] != null)
                {
                    int SolicitudID = Convert.ToInt32(Request["Id"]);
                    Solicitud sol = SolicitudDAO.Instance.GetOne(SolicitudID);

                    Solicitud solicitudGuardara = SolicitudDAO.Instance.GetOne(SolicitudID);
                    solicitudGuardara.EstadoEnAFIP = Enums.EstadoEnAFIP.Enviado;
                    solicitudGuardara.EstadoEnAFIP = Enums.EstadoEnAFIP.VueltaOrigen;
                    
                    lblMensaje.Text = "Vuelta a Origen realizado";
                    solicitudGuardara.ObservacionAfip = lblMensaje.Text;
                    SolicitudDAO.Instance.SaveOrUpdate(solicitudGuardara);

                    // Envio Anulacion a SAP
                    wsSAP wssap = new wsSAP();
                    wssap.PrefacturaSAP(solicitudGuardara, true, false);

                    Response.Redirect("BandejaDeSalida.aspx");


                }

            }
            else 
            {


                if (Request["Id"] != null)
                {
                    int SolicitudID = Convert.ToInt32(Request["Id"]);
                    Solicitud sol = SolicitudDAO.Instance.GetOne(SolicitudID);

                    //Hago el pedido de Cambio de destino a la afip
                    var ws = new wsAfip_v3();
                    var resul = ws.regresarAOrigenCTGRechazado(sol);

                    Solicitud solicitudGuardara = SolicitudDAO.Instance.GetOne(SolicitudID);
                    solicitudGuardara.EstadoEnAFIP = Enums.EstadoEnAFIP.Enviado;

                    if (resul.arrayErrores.Length > 0)
                    {
                        lblMensaje.ForeColor = Color.Red;
                        lblMensaje.Text = "ERRORES Regreso a Origen en AFIP: <br>";
                        foreach (String errores in resul.arrayErrores)
                        {
                            lblMensaje.Text += errores + "<br>";
                        }
                        solicitudGuardara.EstadoEnAFIP = Enums.EstadoEnAFIP.SinProcesar;
                        SolicitudDAO.Instance.SaveOrUpdate(solicitudGuardara);
                        return;
                    }

                    if (resul.datosResponse != null)
                    {
                        lblMensaje.ForeColor = Color.Black;
                        if (!String.IsNullOrEmpty(resul.datosResponse.fechaHora))
                        {
                            solicitudGuardara.EstadoEnAFIP = Enums.EstadoEnAFIP.VueltaOrigen;
                            lblMensaje.Text = "Vuelta a Origen realizado";
                        }
                    }

                    solicitudGuardara.ObservacionAfip = lblMensaje.Text;
                    SolicitudDAO.Instance.SaveOrUpdate(solicitudGuardara);

                    // Envio Anulacion a SAP
                    wsSAP wssap = new wsSAP();
                    wssap.PrefacturaSAP(solicitudGuardara, true, false);

                    Response.Redirect("BandejaDeSalida.aspx");


                }
            
            }












        }






    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Configuration;

using CartaDePorte.Core.Utilidades;
using CartaDePorte.Core.Domain.Seguridad;
using CartaDePorte.Core;

namespace CartaDePorte.Web
{

    public partial class Reporte : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!App.UsuarioTienePermisos("Reportes"))
            {
                Response.Redirect("~/SinAutorizacion.aspx");
                return;
            }

            if (Request["Id"] != null)
            {
                int idSolicitud = Convert.ToInt32(Request["Id"]);

                if (App.ImpersonationValidateUser())
                {
                    DrawingCDP.Instance.CrearCartaDePorte(idSolicitud);
                    App.ImpersonationUndo();
                }

                string reporteServer = ConfigurationManager.AppSettings["ReportServerURL"] + "?/Reportes/CartaDePorte/CartaDePorteReport&idSolicitud=";
                Response.Redirect(reporteServer + idSolicitud.ToString() + "&rs:Command=Render&rs:format=PDF");
                Response.Redirect("BandejaDeSalida.aspx");

            }

            /*
            rptCartaDePorte.ServerReport.ReportPath = System.Configuration.ConfigurationSettings.AppSettings["ReportPath"];
            rptCartaDePorte.ServerReport.ReportServerUrl = new Uri(System.Configuration.ConfigurationSettings.AppSettings["ReportServerURL"]);

            ReportParameter p1 = new ReportParameter("idSolicitud", Request["Id"].ToString());
            rptCartaDePorte.ServerReport.SetParameters(new ReportParameter[] { p1 });
            rptCartaDePorte.ServerReport.Refresh();
            */

        }


    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Configuration;
using System.IO;
using Microsoft.Reporting.WebForms;
using CartaDePorte.Core.Utilidades;
using CartaDePorte.Core.DAO;
using CartaDePorte.Core.Domain;
using CartaDePorte.Common;
using CartaDePorte.Core;

namespace CartaDePorte.Web
{
    public partial class ReportePDF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Id"] != null)
                {
                    int idSolicitud = Convert.ToInt32(Request["Id"]);
                    Tools.Logger.InfoFormat("ReportePDF idSolicitud: {0}", idSolicitud);

                    if (GrupoEmpresaDAO.Instance.GetOne(App.Usuario.Empresa.IdGrupoEmpresa).Pais.Descripcion.ToUpper() == "PARAGUAY")
                    {
                        var File = new GenerarPDF().SetReport(idSolicitud);

                        var fileInfo = new FileInfo(File);

                        if (fileInfo.Exists)
                        {
                            Response.Clear();
                            Response.AddHeader("Content-Disposition", "attachment; filename=" + fileInfo.Name);
                            Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                            Response.ContentType = "application/octet-stream";
                            Response.Flush();
                            Response.TransmitFile(fileInfo.FullName);
                            Response.End();
                        }
                    }
                    else
                    {
                        SolicitudFull solicitud = SolicitudDAO.Instance.GetOne(idSolicitud);
                        var msg = string.Empty;
                        var file = string.Empty;

                        if (App.ImpersonationValidateUser() || App.Usuario.Nombre.ToUpper() == "IRSACORP\\SPOSZALSKI")
                        {
                            file = PdfCdp.Instance.GenerarPDF(solicitud, out msg);
                            Tools.Logger.InfoFormat("ReportePDF file: {0}", file);
                            App.ImpersonationUndo();
                        }

                        var fileInfo = new FileInfo(file);

                        if (fileInfo.Exists)
                        {
                            Response.Clear();
                            Response.AddHeader("Content-Disposition", "attachment; filename=" + fileInfo.Name);
                            Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                            Response.ContentType = "application/octet-stream";
                            Response.Flush();
                            Response.TransmitFile(fileInfo.FullName);
                            Response.End();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Tools.Logger.Error(ex);
                throw;
            }
        }
    }
}

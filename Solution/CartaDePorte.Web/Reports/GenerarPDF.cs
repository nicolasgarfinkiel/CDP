using CartaDePorte.Core.Domain;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace CartaDePorte.Web
{
    public class GenerarPDF
    {
        public string SetReport(int IdSolicitud)
        {
            try
            {
                string mimeType, encoding, extension;
                string[] streamids;
                Warning[] warnings;
                string format = "PDF";
                var reportDs = new DataSet();
                reportDs.Tables.Add(Core.DAO.RemitoParaguayDAO.Instance.GetRemitoParaguay(IdSolicitud));      
                ReportViewer reportViewer = new ReportViewer();

                using (StreamReader rdlcSR = new StreamReader(string.Format(@"{0}Reports\{1}", AppDomain.CurrentDomain.BaseDirectory, "RemitoCresca.rdlc")))
                {
                    reportViewer.LocalReport.LoadReportDefinition(rdlcSR);
                    reportViewer.LocalReport.Refresh();
                }

                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("RemitoCrescaDS", reportDs.Tables["RemitoParaguayDS"]));

                byte[] bytes = reportViewer.LocalReport.Render(format, "", out mimeType, out encoding, out extension, out streamids, out warnings);
                
                return GuardarArchivo(bytes, IdSolicitud.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string GuardarArchivo(byte[] bytes, string IdSolicitud)
        {
            try
            {
                string PathFile = ConfigurationManager.AppSettings["RemitoParaguay"];

                if (!Directory.Exists(string.Format(@"{0}\{1}\{2}", PathFile, DateTime.Now.Year, DateTime.Now.Month)))
                    Directory.CreateDirectory(string.Format(@"{0}\{1}\{2}", PathFile, DateTime.Now.Year, DateTime.Now.Month));

                if (File.Exists(string.Format(@"{0}\{1}\{2}\", PathFile, DateTime.Now.Year, DateTime.Now.Month, IdSolicitud)))
                    File.Delete(string.Format(@"{0}\{1}\{2}\", PathFile, DateTime.Now.Year, DateTime.Now.Month, IdSolicitud));

                File.WriteAllBytes(string.Format(@"{0}\{1}\{2}\{3}", PathFile, DateTime.Now.Year, DateTime.Now.Month, IdSolicitud + ".pdf"), bytes);
                return string.Format(@"{0}\{1}\{2}\{3}", PathFile, DateTime.Now.Year, DateTime.Now.Month, IdSolicitud + ".pdf");
            }
            catch(Exception ex)
            {
                return string.Empty;
            }
        }
    }
}

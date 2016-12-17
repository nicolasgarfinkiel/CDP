using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CartaDePorte.Core.DAO;
using CartaDePorte.Core.Domain;
using CartaDePorte.Core.Domain.Seguridad;
using CartaDePorte.Core;

namespace CartaDePorte.Web
{
    public partial class cambiosestados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!App.UsuarioTienePermisos("Visualizacion Historial de Estados"))
            {
                Response.Redirect("~/SinAutorizacion.aspx");
                return;
            }


            if (!IsPostBack)
            {
                string cdp = Request["id"];
                CargarTitulos(cdp);
                Datos(cdp);
            }

        }        
        
        private void CargarTitulos(string nrocdp)
        {

            tblData.Rows.Clear();
            var row = new TableRow();
            row.CssClass = "TableRowTitle";
            row.Cells.Add(AddTitleCell("Id", 5));
            row.Cells.Add(AddTitleCell("Nro Carta Porte", 50));
            row.Cells.Add(AddTitleCell("Ctg", 50));
            row.Cells.Add(AddTitleCell("Observacion AFIP", 50));
            row.Cells.Add(AddTitleCell("Afip", 20));
            row.Cells.Add(AddTitleCell("SAP", 20));
            
            row.Cells.Add(AddTitleCell("Codigo Envio SAP", 20));
            row.Cells.Add(AddTitleCell("Codigo Anulacion Afip", 20));
            row.Cells.Add(AddTitleCell("Codigo Anulacion SAP", 20));

            row.Cells.Add(AddTitleCell("Usuario Creacion", 40));
            row.Cells.Add(AddTitleCell("Fecha Creacion", 40));
            row.Cells.Add(AddTitleCell("Usuario Modificacion", 40));
            row.Cells.Add(AddTitleCell("Fecha Modificacion", 40));
            
            tblData.Rows.Add(row);

        }
        private void Datos(string cdp)
        {
            foreach (Solicitud soli in LogSapDAO.Instance.getCambiosEstados(cdp))
            {
                var row = new TableRow();
                row.CssClass = "TableRow";

                row.Cells.Add(AddCell(soli.IdSolicitud.ToString(), string.Empty, HorizontalAlign.Justify));                
                row.Cells.Add(AddCell(soli.NumeroCartaDePorte, string.Empty, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(soli.Ctg.ToString(), string.Empty, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(soli.ObservacionAfip, string.Empty, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(soli.EstadoEnAFIP.ToString(), string.Empty, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(soli.EstadoEnSAP.ToString(), string.Empty, HorizontalAlign.Justify));
                
                row.Cells.Add(AddCell(soli.CodigoRespuestaEnvioSAP.ToString(), string.Empty, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(soli.CodigoAnulacionAfip.ToString(), string.Empty, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(soli.CodigoRespuestaAnulacionSAP.ToString(), string.Empty, HorizontalAlign.Justify));

                row.Cells.Add(AddCell(soli.UsuarioCreacion.ToString(), string.Empty, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(soli.FechaCreacion.ToString("dd/MM/yyyy hh:mm:ss"), string.Empty, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(soli.UsuarioModificacion.ToString(), string.Empty, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(soli.FechaModificacion.ToString("dd/MM/yyyy hh:mm:ss"), string.Empty, HorizontalAlign.Justify));


                tblData.Rows.Add(row);
            }
        }
        
        #region Creacion de celdas

        private TableCell AddCell(string texto, string tooltip, HorizontalAlign ha)
        {
            var cell = new TableCell();
            var lbl = new Label();
            lbl.Text = "&nbsp;&nbsp;" + texto;
            cell.ToolTip = tooltip;
            cell.Height = Unit.Pixel(35);
            cell.Controls.Add(lbl);
            return cell;
        }

        private TableCell AddTitleCell(string texto, int width)
        {
            var cell = new TableCell();
            cell.Text = texto;
            cell.Height = Unit.Pixel(40);
            cell.Width = Unit.Pixel(width);
            return cell;
        }

        #endregion

    }
}

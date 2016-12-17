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
    public partial class LogSapList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!App.UsuarioTienePermisos("Visualizacion Log SAP"))
            {
                Response.Redirect("~/SinAutorizacion.aspx");
                return;
            }

            if (!IsPostBack)
            {
                string cdp = Request["cdp"];                
                CargarTitulos(cdp);
                Datos(cdp);
            }

        }        
        
        private void CargarTitulos(string nrocdp)
        {
            tblData.Rows.Clear();
            var row = new TableRow();
            row.CssClass = "TableRowTitle";

            string linkadetalle = "<a href='cambiosestados.aspx?id=" + nrocdp + "' style='text-decoration: none; color:#FFFFFF;'>Nro Envio</a>";
            row.Cells.Add(AddTitleCell(linkadetalle, 20));

            row.Cells.Add(AddTitleCell("Fecha Generacion", 150));
            row.Cells.Add(AddTitleCell("Origen", 50));
            row.Cells.Add(AddTitleCell("Carta de Porte", 50));
            row.Cells.Add(AddTitleCell("Nro Documento Sap", 50));
            row.Cells.Add(AddTitleCell("Tipo Mensaje", 10));
            row.Cells.Add(AddTitleCell("Texto Mensaje", 400));
            tblData.Rows.Add(row);

        }
        private void Datos(string cdp)
        {
            foreach (LogSap logsap in LogSapDAO.Instance.GetOneByNroCartaDePorte(cdp))
            {
                var row = new TableRow();
                row.CssClass = "TableRow";

                row.Cells.Add(AddCell(logsap.NroEnvio.ToString(), string.Empty, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(logsap.FechaCreacion.ToString("dd/MM/yyyy hh:mm:ss"), string.Empty, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(logsap.Origen, string.Empty, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(logsap.NroDocumentoRE, string.Empty, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(logsap.NroDocumentoSap, string.Empty, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(logsap.TipoMensaje, string.Empty, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(logsap.TextoMensaje, string.Empty, HorizontalAlign.Justify));

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

using CartaDePorte.Core.Domain;
using CartaDePorte.Core.DAO;
using CartaDePorte.Core.Domain.Seguridad;
using CartaDePorte.Core;

namespace CartaDePorte.Web
{
    public partial class ConsultaRangosCartasDePorte : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Main master = (Main)Page.Master;
            master.HiddenValue = "Administracion";
            if (!App.UsuarioTienePermisos("Administracion"))
            {
                Response.Redirect("~/SinAutorizacion.aspx");
                return;
            }
            string fd = Request.Form[txtDateDesde.UniqueID];
            string fh = Request.Form[txtDateHasta.UniqueID];

            txtDateDesde.Text = fd;
            txtDateHasta.Text = fh;

            if (!IsPostBack)
            {
                if (String.IsNullOrEmpty(txtDateDesde.Text))
                    txtDateDesde.Text = DateTime.Today.AddDays(-30).ToString("dd/MM/yyyy");

                if (String.IsNullOrEmpty(txtDateHasta.Text))
                    txtDateHasta.Text = DateTime.Today.ToString("dd/MM/yyyy");
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Validaciones())
            {
                string fd = Request.Form[txtDateDesde.UniqueID];
                string fh = Request.Form[txtDateHasta.UniqueID];

                string[] fechaDesde = fd.Trim().Substring(0, 10).Split('/');
                DateTime FD = new DateTime(Convert.ToInt32(fechaDesde[2]), Convert.ToInt32(fechaDesde[1]), Convert.ToInt32(fechaDesde[0]));

                string[] fechaHasta = fh.Trim().Substring(0, 10).Split('/');
                DateTime FH = new DateTime(Convert.ToInt32(fechaHasta[2]), Convert.ToInt32(fechaHasta[1]), Convert.ToInt32(fechaHasta[0]));

                DataTable tempData = LoteCartasDePorteDAO.Instance.GetAllReporteDataTable(FD, FH.AddHours(23).AddMinutes(59));

                ExportToExcel(tempData);
            }
        }

        private String EspaciosEnCampos(String texto, int pad)
        {
            if (texto == null)
                return string.Empty.PadLeft(pad, ' ');

            return texto.PadLeft(pad, ' ');
        }

        private String CerosEnCampos(String texto, int pad)
        {
            if (texto == null)
                return string.Empty.PadLeft(pad, '0');

            return texto.PadLeft(pad, '0');
        }

        private Boolean Validaciones()
        {
            Boolean result = true;
            string mensaje = string.Empty;
            lblMensaje.Text = string.Empty;

            string fd = Request.Form[txtDateDesde.UniqueID];
            string fh = Request.Form[txtDateHasta.UniqueID];

            if (String.IsNullOrEmpty(fd))
            {
                mensaje += "Debe completar FechaDesde<br>";
            }
            if (String.IsNullOrEmpty(fh))
            {
                mensaje += "Debe completar FechaHasta<br>";
            }

            if (mensaje.Length > 0)
            {
                lblMensaje.Text = mensaje;
                result = false;
            }
            else
            {
                string[] fechaDesde = fd.Trim().Substring(0, 10).Split('/');
                DateTime FD = new DateTime(Convert.ToInt32(fechaDesde[2]), Convert.ToInt32(fechaDesde[1]), Convert.ToInt32(fechaDesde[0]));

                string[] fechaHasta = fh.Trim().Substring(0, 10).Split('/');
                DateTime FH = new DateTime(Convert.ToInt32(fechaHasta[2]), Convert.ToInt32(fechaHasta[1]), Convert.ToInt32(fechaHasta[0]));

                if (FD > FH)
                {
                    mensaje += "La Fecha Desde debe ser menor o igual a la Fecha Hasta<br>";

                    lblMensaje.Text = mensaje;
                    result = false;
                }
            }
            return result;
        }

        private string splitCapitalizacion(string texto)
        {
            string output = "";

            foreach (char letter in texto)
            {
                if (Char.IsUpper(letter) && output.Length > 0)
                    output += " " + letter;
                else
                    output += letter;
            }

            return output;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (Validaciones())
            {
                CargarTitulos();
                Datos();
            }
        }

        private void CargarTitulos()
        {
            tblData.Rows.Clear();
            var row = new TableRow();
            row.CssClass = "TableRowTitle";
            row.Cells.Add(AddTitleCell("Lote", 5));
            row.Cells.Add(AddTitleCell("Desde", 100));
            row.Cells.Add(AddTitleCell("Hasta", 100));

            string pais = PaisDAO.Instance.GetOne(App.Usuario.IdPais).Descripcion.ToUpper();

            if (pais.Contains("ARGENTINA"))
                row.Cells.Add(AddTitleCell("Cee", 150));
            else if (pais.Contains("PARAGUAY"))
                row.Cells.Add(AddTitleCell("RUC", 150));
            else if (pais.Contains("BOLIVIA"))
                row.Cells.Add(AddTitleCell("NIT", 150));

            row.Cells.Add(AddTitleCell("Establecimiento Origen", 150));
            row.Cells.Add(AddTitleCell("Fecha Vencimiento", 100));
            row.Cells.Add(AddTitleCell("Cantidad Disponible", 100));
            row.Cells.Add(AddTitleCell("Usuario Creacion", 100));
            tblData.Rows.Add(row);
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

        private void Datos()
        {
            if (Validaciones())
            {
                string fd = Request.Form[txtDateDesde.UniqueID];
                string fh = Request.Form[txtDateHasta.UniqueID];

                string[] fechaDesde = fd.Trim().Substring(0, 10).Split('/');
                DateTime FD = new DateTime(Convert.ToInt32(fechaDesde[2]), Convert.ToInt32(fechaDesde[1]), Convert.ToInt32(fechaDesde[0]));

                string[] fechaHasta = fh.Trim().Substring(0, 10).Split('/');
                DateTime FH = new DateTime(Convert.ToInt32(fechaHasta[2]), Convert.ToInt32(fechaHasta[1]), Convert.ToInt32(fechaHasta[0]));

                foreach (LoteCartasDePorte lote in LoteCartasDePorteDAO.Instance.GetAllReporte(FD, FH.AddHours(23).AddMinutes(59)))
                {

                    var row = new TableRow();
                    row.CssClass = "TableRow";

                    row.Cells.Add(AddCell(lote.IdLoteCartasDePorte.ToString(), lote.IdLoteCartasDePorte.ToString(), HorizontalAlign.Justify));
                    row.Cells.Add(AddCell(lote.Desde.ToString(), lote.Desde.ToString(), HorizontalAlign.Justify));
                    row.Cells.Add(AddCell(lote.Hasta.ToString(), lote.Hasta.ToString(), HorizontalAlign.Justify));
                    row.Cells.Add(AddCell(lote.Cee, lote.Cee, HorizontalAlign.Justify));
                    row.Cells.Add(AddCell((lote.EstablecimientoOrigen != null) ? lote.EstablecimientoOrigen.Descripcion : string.Empty, (lote.EstablecimientoOrigen != null) ? lote.EstablecimientoOrigen.Descripcion : string.Empty, HorizontalAlign.Justify));
                    row.Cells.Add(AddCell(lote.FechaVencimiento.ToString("dd/MM/yyyy"), lote.FechaVencimiento.ToString("dd/MM/yyyy"), HorizontalAlign.Justify));
                    string cntLote = lote.CartasDisponibles.ToString();
                    row.Cells.Add(AddCell(cntLote, cntLote, HorizontalAlign.Justify));
                    row.Cells.Add(AddCell(lote.UsuarioCreacion, lote.UsuarioCreacion, HorizontalAlign.Justify));

                    tblData.Rows.Add(row);
                }
            }
        }

        public void ExportToExcel(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                String fechahoy = DateTime.Now.Year.ToString().PadLeft(4, '0') +
                DateTime.Now.Month.ToString().PadLeft(2, '0') +
                DateTime.Now.Day.ToString().PadLeft(2, '0') +
                DateTime.Now.Hour.ToString().PadLeft(2, '0') +
                DateTime.Now.Minute.ToString().PadLeft(2, '0') +
                DateTime.Now.Second.ToString().PadLeft(2, '0') +
                DateTime.Now.Millisecond.ToString();

                string filename = "ListaLotesCartasDePorte_" + fechahoy + ".xls";
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                DataGrid dgGrid = new DataGrid();
                dgGrid.DataSource = dt;
                dgGrid.DataBind();

                //Get the HTML for the control.
                dgGrid.RenderControl(hw);
                //Write the HTML back to the browser.
                //Response.ContentType = application/vnd.ms-excel;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                this.EnableViewState = false;
                Response.Write(tw.ToString());
                Response.End();
            }
        }
    }
}
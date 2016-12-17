using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CartaDePorte.Core.Domain;
using CartaDePorte.Core.DAO;
using System.Data;
using System.Globalization;
using CartaDePorte.Common;
using CartaDePorte.Core;

namespace CartaDePorte.Web
{
    public partial class CartasDePorteSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var opcion = Tools.Value2<string>(this.Request["opcion"]);
            if (!string.IsNullOrEmpty(opcion))
            {
                if (opcion.Equals("ELIMINARDISPONIBLES"))
                {
                    var idLoteCartasDePorte = Tools.Value2<int>(this.Request["idLoteCartasDePorte"], 0);

                    CartaDePorteDAO.Instance.EliminarDisponiblesRangoCartaDePorte(idLoteCartasDePorte, App.Usuario.Nombre);

                    var json = string.Format(@"{{ ""idLoteCartasDePorte"": {0} }}", idLoteCartasDePorte);

                    this.Response.Clear();
                    this.Response.ContentType = "application/json";
                    this.Response.Write(json);
                    this.Response.End();
                    return;
                }
            }

            Main master = (Main)Page.Master;
            master.HiddenValue = "Administracion";

            CartaDePorte.Core.Domain.Seguridad.SeguridadUsuario su = (Session["Usuario"] != null) ? (CartaDePorte.Core.Domain.Seguridad.SeguridadUsuario)Session["Usuario"] : null;
            if (su == null)
                return;

            if (!su.CheckPermisoInterno("Administracion"))
            {
                Response.Redirect("../../SinAutorizacion.aspx");
                return;
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("ABMCartasDePorte.aspx");
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

            if (PaisDAO.Instance.GetOne(App.Usuario.IdPais).Descripcion.ToUpper().Contains("ARGENTINA"))
                row.Cells.Add(AddTitleCell("Cee", 150));
            else
                row.Cells.Add(AddTitleCell("Timbrado", 150));
            
            row.Cells.Add(AddTitleCell("Establecimiento Origen", 150));
            row.Cells.Add(AddTitleCell("Fecha Vencimiento", 100));
            row.Cells.Add(AddTitleCell("Cantidad Disponible", 100));
            row.Cells.Add(AddTitleCell("Usuario Creacion", 100));
            row.Cells.Add(AddTitleCell("...", 5));
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
                //string fd = Request.Form[txtDateDesde.UniqueID];
                //string fh = Request.Form[txtDateHasta.UniqueID];

                //string[] fechaDesde = fd.Trim().Substring(0, 10).Split('/');
                //DateTime FD = new DateTime(Convert.ToInt32(fechaDesde[2]), Convert.ToInt32(fechaDesde[1]), Convert.ToInt32(fechaDesde[0]));

                //string[] fechaHasta = fh.Trim().Substring(0, 10).Split('/');
                //DateTime FH = new DateTime(Convert.ToInt32(fechaHasta[2]), Convert.ToInt32(fechaHasta[1]), Convert.ToInt32(fechaHasta[0]));

                var loteDesde = Tools.Value2<int>(this.txtLoteDesde.Text, 0);
                var tieneDisponible = this.chkTienedisponible.Checked ? 1 : 0;

                foreach (LoteCartasDePorte lote in LoteCartasDePorteDAO.Instance.GetFiltro(loteDesde, tieneDisponible, App.Usuario.IdGrupoEmpresa))
                {

                    var row = new TableRow();
                    row.CssClass = "TableRow";

                    row.Cells.Add(AddCell(lote.IdLoteCartasDePorte.ToString(), lote.IdLoteCartasDePorte.ToString(), HorizontalAlign.Justify));
                    row.Cells.Add(AddCell(lote.Desde.ToString(), lote.Desde.ToString(), HorizontalAlign.Justify));
                    row.Cells.Add(AddCell(lote.Hasta.ToString(), lote.Hasta.ToString(), HorizontalAlign.Justify));
                    row.Cells.Add(AddCell(lote.Cee, lote.Cee, HorizontalAlign.Justify));
                    row.Cells.Add(AddCell((lote.EstablecimientoOrigen != null) ? lote.EstablecimientoOrigen.Descripcion : string.Empty, (lote.EstablecimientoOrigen != null) ? lote.EstablecimientoOrigen.Descripcion : string.Empty, HorizontalAlign.Justify));
                    row.Cells.Add(AddCell(lote.FechaVencimiento.ToString("dd/MM/yyyy"), lote.FechaVencimiento.ToString("dd/MM/yyyy"), HorizontalAlign.Justify));
                    var cantidad = (lote.Hasta - lote.Desde) + 1;
                    string cntLote = lote.CartasDisponibles.ToString();
                    row.Cells.Add(AddCell(string.Format("{0}/{1}", cntLote, cantidad), cntLote, HorizontalAlign.Justify));
                    row.Cells.Add(AddCell(lote.UsuarioCreacion, lote.UsuarioCreacion, HorizontalAlign.Justify));

                    string link = "";
                    if (lote.CartasDisponibles > 0)
                    {
                        if (lote.CartasDisponibles == cantidad)
                        {
                            link = string.Format("<a title='Eliminar Lote' href='javascript: EliminarLote({0}, {1}, {2}, {3})'><IMG border='0' style='width: 16px; height: 16px;' src='../../Content/Images/iconotrash.png'></a>", lote.IdLoteCartasDePorte, lote.Desde, lote.Hasta, lote.CartasDisponibles);
                        }
                        else
                        {
                            link = string.Format("<a title='Liberar números no utilizados' href='javascript: EliminarNoUtilizados({0}, {1}, {2}, {3})'><IMG border='0' style='width: 16px; height: 16px;' src='../../Content/Images/iconotrash.png'></a>", lote.IdLoteCartasDePorte, lote.Desde, lote.Hasta, lote.CartasDisponibles);
                        }
                    }
                    row.Cells.Add(AddCell(link, string.Empty, HorizontalAlign.Center));

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

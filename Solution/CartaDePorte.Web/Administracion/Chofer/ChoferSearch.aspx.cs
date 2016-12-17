using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CartaDePorte.Core.Domain;
using CartaDePorte.Core.DAO;
using CartaDePorte.Core.Domain.Seguridad;
using CartaDePorte.Core;

namespace CartaDePorte.Web
{
    public partial class ChoferSearch : System.Web.UI.Page
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
        }

        private void CargarTitulos()
        {
            var row = new TableRow();
            row.CssClass = "TableRowTitle";
            row.Cells.Add(AddTitleCell("Apellido Nombre / Descripcion", 500));

            string pais = PaisDAO.Instance.GetOne(App.Usuario.IdPais).Descripcion;

            if (pais.ToUpper().Contains("PARAGUAY"))
                row.Cells.Add(AddTitleCell("RUC", 100));
            else if (pais.ToUpper().Contains("BOLIVIA"))
                row.Cells.Add(AddTitleCell("NIT", 100));
            else
                row.Cells.Add(AddTitleCell("CUIT", 100));

            row.Cells.Add(AddTitleCell("Transportista", 50));
            row.Cells.Add(AddTitleCell("Fecha Creacion", 130));
            row.Cells.Add(AddTitleCell("Usuario Creacion", 5));
            row.Cells.Add(AddTitleCell("Editar", 5));

            tblData.Rows.Add(row);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarTitulos();
            Datos(txtBuscar.Text.Trim(), chkTransportista.Checked);
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("ABMChofer.aspx?Id=0");
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

        private void Datos(string busqueda, bool soloTransportistas)
        {
            foreach (Chofer chofer in ChoferDAO.Instance.GetFiltro(busqueda))
            {
                var row = new TableRow();
                row.CssClass = "TableRow";

                if (chofer.EsChoferTransportista == Enums.EsChoferTransportista.Si)
                    row.CssClass = "TableRowTransportista";

                row.Cells.Add(AddCell(chofer.Apellido + " " + chofer.Nombre, chofer.Nombre, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(chofer.Cuit, chofer.Cuit, HorizontalAlign.Justify));
                row.Cells.Add(AddCell((chofer.EsChoferTransportista == Enums.EsChoferTransportista.Si) ? "Si" : "No", (chofer.EsChoferTransportista == Enums.EsChoferTransportista.Si) ? "Si" : "No", HorizontalAlign.Justify));
                row.Cells.Add(AddCell(chofer.FechaCreacion.ToShortDateString(), chofer.FechaCreacion.ToShortDateString(), HorizontalAlign.Justify));
                row.Cells.Add(AddCell(chofer.UsuarioCreacion, chofer.UsuarioCreacion, HorizontalAlign.Justify));

                string link = "<a href='ABMChofer.aspx?Id=" + chofer.IdChofer.ToString() +
                              "'><IMG border='0' src='../../Content/Images/pencil.png'></a>";
                row.Cells.Add(AddCell(link, string.Empty, HorizontalAlign.Center));

                if (soloTransportistas)
                {
                    if (chofer.EsChoferTransportista == Enums.EsChoferTransportista.Si)
                        tblData.Rows.Add(row);
                }
                else
                {
                    tblData.Rows.Add(row);
                }
            }
        }
    }
}

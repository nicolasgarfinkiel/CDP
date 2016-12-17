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
    public partial class EstablecimientoSearch : System.Web.UI.Page
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
            row.Cells.Add(AddTitleCell("Descripcion", 230));
            row.Cells.Add(AddTitleCell("Direccion", 250));
            row.Cells.Add(AddTitleCell("Localidad", 100));
            row.Cells.Add(AddTitleCell("Provincia", 100));
            row.Cells.Add(AddTitleCell("Fecha Creacion", 130));
            row.Cells.Add(AddTitleCell("Usuario Creacion", 5));
            row.Cells.Add(AddTitleCell("Editar", 5));

            tblData.Rows.Add(row);
        
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarTitulos();
            Datos(txtBuscar.Text.Trim());
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("ABMEstablecimiento.aspx?Id=0");
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

        private void Datos(string busqueda)
        {
            foreach (Establecimiento establecimiento in EstablecimientoDAO.Instance.GetFiltro(busqueda))
            {
                var row = new TableRow();
                row.CssClass = "TableRow";

                var localidadDesc = "";
                var provinciaDesc = "";
                if (establecimiento.Localidad != null)
                {
                    localidadDesc = establecimiento.Localidad.Descripcion;
                }
                if (establecimiento.Provincia != null)
                {
                    provinciaDesc = establecimiento.Provincia.Descripcion;
                }

                row.Cells.Add(AddCell(establecimiento.Descripcion, establecimiento.Descripcion, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(establecimiento.Direccion, establecimiento.Direccion, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(localidadDesc, localidadDesc, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(provinciaDesc, provinciaDesc, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(establecimiento.FechaCreacion.ToShortDateString(), establecimiento.FechaCreacion.ToShortDateString(), HorizontalAlign.Justify));
                row.Cells.Add(AddCell(establecimiento.UsuarioCreacion, establecimiento.UsuarioCreacion, HorizontalAlign.Justify));

                string link = "<a href='ABMEstablecimiento.aspx?Id=" + establecimiento.IdEstablecimiento.ToString() +
                              "'><IMG border='0' src='../../Content/Images/pencil.png'></a>";
                row.Cells.Add(AddCell(link, string.Empty, HorizontalAlign.Center));

                tblData.Rows.Add(row);

            }
        }

    }
}

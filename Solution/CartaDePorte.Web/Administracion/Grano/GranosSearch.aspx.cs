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
    public partial class GranosSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
            row.Cells.Add(AddTitleCell("Descripcion", 200));
            if (App.Usuario.IdGrupoEmpresa == App.ID_GRUPO_CRESUD)
            {
                row.Cells.Add(AddTitleCell("Especie", 170));
                row.Cells.Add(AddTitleCell("Cosecha", 170));
                row.Cells.Add(AddTitleCell("Tipo", 50));
            }
            row.Cells.Add(AddTitleCell("Sujeto a Lote", 50));
            row.Cells.Add(AddTitleCell("Fecha Creacion", 70));
            row.Cells.Add(AddTitleCell("Usuario Creacion", 70));
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
            Response.Redirect("ABMGrano.aspx?Id=0");
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
            foreach (Grano grano in GranoDAO.Instance.GetFiltro(busqueda))
            {
                var row = new TableRow();
                row.CssClass = "TableRow";

                row.Cells.Add(AddCell(grano.Descripcion, grano.Descripcion, HorizontalAlign.Justify));
                if (App.Usuario.IdGrupoEmpresa == App.ID_GRUPO_CRESUD)
                {
                    row.Cells.Add(AddCell(grano.EspecieAfip.Descripcion, grano.EspecieAfip.Descripcion, HorizontalAlign.Justify));
                    row.Cells.Add(AddCell(grano.CosechaAfip.Descripcion, grano.CosechaAfip.Descripcion, HorizontalAlign.Justify));
                    row.Cells.Add(AddCell((grano.TipoGrano != null) ? grano.TipoGrano.Descripcion : string.Empty, (grano.TipoGrano != null) ? grano.TipoGrano.Descripcion : string.Empty, HorizontalAlign.Justify));
                }
                row.Cells.Add(AddCell(grano.SujetoALote, grano.SujetoALote, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(grano.FechaCreacion.ToShortDateString(), grano.FechaCreacion.ToShortDateString(), HorizontalAlign.Justify));
                row.Cells.Add(AddCell(grano.UsuarioCreacion, grano.UsuarioCreacion, HorizontalAlign.Justify));

                string link = "<a href='ABMGrano.aspx?Id=" + grano.IdGrano.ToString() +
                              "'><IMG border='0' src='../../Content/Images/pencil.png'></a>";
                row.Cells.Add(AddCell(link, string.Empty, HorizontalAlign.Center));

                tblData.Rows.Add(row);

            }
        }

    }
}

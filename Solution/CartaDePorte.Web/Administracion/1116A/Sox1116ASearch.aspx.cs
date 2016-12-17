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
    public partial class Sox1116ASearch : System.Web.UI.Page
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
            row.Cells.Add(AddTitleCell("Id", 5));
            row.Cells.Add(AddTitleCell("Nro Carta Porte", 50));
            row.Cells.Add(AddTitleCell("Ctg", 50));
            row.Cells.Add(AddTitleCell("Fecha 1116A", 50));
            row.Cells.Add(AddTitleCell("Numero 1116A", 50));
            //row.Cells.Add(AddTitleCell("Tipo De Carta", 100));
            //row.Cells.Add(AddTitleCell("Titular Carta De Porte", 100));
            row.Cells.Add(AddTitleCell("Fecha", 40));
            row.Cells.Add(AddTitleCell("Usuario", 40));
            row.Cells.Add(AddTitleCell("", 5));

            tblData.Rows.Add(row);
        
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarTitulos();
            Datos(txtBuscar.Text.Trim());
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
             IList<Solicitud> results = SolicitudDAO.Instance.GetFiltro(busqueda,"-1","-1");
            if(results.Count > 1)
                lblCantidadResultados.Text = "Resultado de la busqueda: "  + results.Count.ToString() + " registros";
            else if (results.Count == 1)
                lblCantidadResultados.Text = "Resultado de la busqueda: " + results.Count.ToString() + " registro";
            else
                lblCantidadResultados.Text = "Resultado de la busqueda: 0 registro";

            foreach (Solicitud solicitud in results)
            {
                if (!String.IsNullOrEmpty(solicitud.Ctg))
                {
                    var row = new TableRow();
                    row.CssClass = "TableRow";

                    row.Cells.Add(AddCell(solicitud.IdSolicitud.ToString(), solicitud.IdSolicitud.ToString(), HorizontalAlign.Justify));
                    row.Cells.Add(AddCell(solicitud.NumeroCartaDePorte, solicitud.NumeroCartaDePorte, HorizontalAlign.Justify));
                    row.Cells.Add(AddCell(solicitud.Ctg, solicitud.Ctg, HorizontalAlign.Justify));
                    row.Cells.Add(AddCell((solicitud.Sox1116A.Fecha1116A > DateTime.MinValue) ? solicitud.Sox1116A.Fecha1116A.ToString("dd/MM/yyyy") : string.Empty, (solicitud.Sox1116A.Fecha1116A != null) ? solicitud.Sox1116A.Fecha1116A.ToString("dd/MM/yyyy") : string.Empty, HorizontalAlign.Justify));
                    row.Cells.Add(AddCell(solicitud.Sox1116A.Numero1116A, solicitud.Sox1116A.Numero1116A, HorizontalAlign.Justify));
                    row.Cells.Add(AddCell(solicitud.FechaCreacion.ToString("dd/MM/yyyy"), solicitud.FechaCreacion.ToString(), HorizontalAlign.Justify));
                    row.Cells.Add(AddCell(solicitud.UsuarioCreacion.Split('\\')[1].ToString(), solicitud.UsuarioCreacion, HorizontalAlign.Justify));

                    // LUPA
                    String linkAlta = "<a href='SoxABM1116A.aspx?Id=" + solicitud.Sox1116A.IdCartaDePorte1116A.ToString() + "&idSol=" + solicitud.IdSolicitud +
                            "'><IMG border='0' src='../../Content/Images/pencil.png'></a>";

                    String linkLupa = "<a href='SoxABM1116A.aspx?Id=" + solicitud.Sox1116A.IdCartaDePorte1116A.ToString() + "&idSol=" + solicitud.IdSolicitud +
                            "'><IMG border='0' src='../../Content/Images/magnify.gif'></a>";

                    if (String.IsNullOrEmpty(solicitud.Sox1116A.Numero1116A))
                        row.Cells.Add(AddCell(linkAlta, "Asociar formulario 1116A", HorizontalAlign.Center));
                    else
                        row.Cells.Add(AddCell(linkLupa, "Ver o editar datos 1116A", HorizontalAlign.Center));

                    tblData.Rows.Add(row);
                }


            }
        
        }


    }
}

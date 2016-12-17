using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CartaDePorte.Core.Domain;
using CartaDePorte.Core.DAO;
using System.Drawing;
using CartaDePorte.Core.Domain.Seguridad;
using CartaDePorte.Core;

namespace CartaDePorte.Web
{
    public partial class Reservas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Main master = (Main)Page.Master;
            master.HiddenValue = "Administracion";

            if (!App.UsuarioTienePermisos("Reservas"))
            {
                Response.Redirect("~/SinAutorizacion.aspx");
                return;
            }

            CargarTitulos();
            Datos();
        }

        private void CargarTitulos()
        {
            tblData.Rows.Clear();
            var row = new TableRow();
            row.CssClass = "TableRowTitle";
            row.Cells.Add(AddTitleCell("Cancelar", 10));
            row.Cells.Add(AddTitleCell("Numero de Carta de Porte", 150));
            row.Cells.Add(AddTitleCell("Cee", 100));
            row.Cells.Add(AddTitleCell("Anulacion", 100));
            row.Cells.Add(AddTitleCell("Fecha Reserva", 170));
            row.Cells.Add(AddTitleCell("Usuario Reserva", 170));            
            row.Cells.Add(AddTitleCell("Cargar", 10));
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
            foreach (Solicitud sol in SolicitudDAO.Instance.GetMisReservas(string.Empty))
            {
                var row = new TableRow();
                row.CssClass = "TableRow";

                string linkCancelacion = "<a href='MisReservas.aspx?cdpAnulada=" + sol.NumeroCartaDePorte +
                              "'><IMG border='0' src='../../Content/Images/icon_Delete.png' width='14' height='14' ></a>";
                row.Cells.Add(AddCell(linkCancelacion, string.Empty, HorizontalAlign.Center));

                row.Cells.Add(AddCell(sol.NumeroCartaDePorte, sol.NumeroCartaDePorte, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(sol.Cee, sol.Cee, HorizontalAlign.Justify));

                string linkAnulacion = "<center><a href='MisReservas.aspx?cdpAnulada2=" + sol.NumeroCartaDePorte +
                    "'><IMG border='0' src='../../Content/Images/reservaANULAR.png' width='14' height='14'></a></center>";
                row.Cells.Add(AddCell(linkAnulacion, "Esta opcion se utiliza unicamente si la Carta de porte reservada fue utilizada por fuera del sistema", HorizontalAlign.Center));

                row.Cells.Add(AddCell(sol.FechaCreacion.ToString("dd/MM/yyyy hh:mm:ss"), sol.FechaCreacion.ToString(), HorizontalAlign.Justify));
                row.Cells.Add(AddCell(sol.UsuarioCreacion, sol.UsuarioCreacion, HorizontalAlign.Justify));

                string linkCarga = "<a href='../../Index.aspx?Id=" + sol.IdSolicitud.ToString() +
                              "'><IMG border='0' src='../../Content/Images/cargaCartaDePorteReservada.png'></a>";
                row.Cells.Add(AddCell(linkCarga, string.Empty, HorizontalAlign.Center));

                tblData.Rows.Add(row);

            }
        }



    }
}

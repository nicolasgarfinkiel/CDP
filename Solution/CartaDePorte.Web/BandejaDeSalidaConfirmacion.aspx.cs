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
    public partial class BandejaDeSalidaConfirmacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var master = (Main)Page.Master;
            master.ValidarMantenimiento();


            if (!App.UsuarioTienePermisos("Confirmar Arribo"))
            {
                Response.Redirect("~/SinAutorizacion.aspx");
                return;
            }


            if (!IsPostBack)
            {
            }

            CargarTitulos();
            Datos();
        }

        private void CargarTitulos()
        {
            tblData.Rows.Clear();
            var row = new TableRow();
            row.CssClass = "TableRowTitle";
            row.Cells.Add(AddTitleCell("Id", 20));
            row.Cells.Add(AddTitleCell("Nro Carta Porte", 80));
            row.Cells.Add(AddTitleCell("Tipo Carta de Porte", 80));
            row.Cells.Add(AddTitleCell("Fecha", 150));
            row.Cells.Add(AddTitleCell("Establecimiento Procedencia", 220));
            row.Cells.Add(AddTitleCell("Establecimiento Destino", 220));
            row.Cells.Add(AddTitleCell("Peso", 100));
            row.Cells.Add(AddTitleCell("Usuario", 100));
            row.Cells.Add(AddTitleCell("VER", 10));
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
            IList<Solicitud> result = SolicitudDAO.Instance.GetTopConfirmacion();

            lblCantidadResultados.Text = "Resultado de la busqueda: " + result.Count.ToString() + " registros";
            
            foreach (Solicitud solicitud in result)
            {                
                var row = new TableRow();
                row.CssClass = "TableRow";

                row.Cells.Add(AddCell(solicitud.IdSolicitud.ToString(), solicitud.IdSolicitud.ToString(), HorizontalAlign.Justify));
                row.Cells.Add(AddCell(solicitud.NumeroCartaDePorte, solicitud.NumeroCartaDePorte, HorizontalAlign.Justify));

                row.Cells.Add(AddCell(solicitud.TipoDeCarta.Descripcion, solicitud.TipoDeCarta.Descripcion, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(solicitud.FechaCreacion.ToString("dd/MM/yyyy hh:mm"), solicitud.FechaCreacion.ToString(), HorizontalAlign.Justify));
                row.Cells.Add(AddCell((solicitud.IdEstablecimientoProcedencia != null) ? solicitud.IdEstablecimientoProcedencia.Descripcion : string.Empty, (solicitud.IdEstablecimientoProcedencia != null) ? solicitud.IdEstablecimientoProcedencia.Descripcion : string.Empty, HorizontalAlign.Justify));
                row.Cells.Add(AddCell((solicitud.IdEstablecimientoDestino != null) ? solicitud.IdEstablecimientoDestino.Descripcion : string.Empty, (solicitud.IdEstablecimientoProcedencia != null) ? solicitud.IdEstablecimientoProcedencia.Descripcion : string.Empty, HorizontalAlign.Justify));

                Int64 peso = 0;
                if (solicitud.CargaPesadaDestino)
                    peso = solicitud.KilogramosEstimados;
                else
                    peso = solicitud.PesoNeto.Value;

                row.Cells.Add(AddCell(peso.ToString(), peso.ToString(), HorizontalAlign.Justify));
                row.Cells.Add(AddCell(solicitud.UsuarioCreacion.Split('\\')[1].ToString(), solicitud.UsuarioCreacion, HorizontalAlign.Justify));

                // LUPA
                String link = "<a href='Index.aspx?Id=" + solicitud.IdSolicitud.ToString() +
                        "'><IMG border='0' src='../../Content/Images/magnify.gif'></a>";

                row.Cells.Add(AddCell(link, "Abrir Solicitud", HorizontalAlign.Center));


                tblData.Rows.Add(row);

            }
        }
        private void DatosFiltro(string busqueda)
        {
            IList<Solicitud> result = SolicitudDAO.Instance.GetFiltroConfirmacion(busqueda);

            if (result.Count > 1)
                lblCantidadResultados.Text = "Resultado de la busqueda: " + result.Count.ToString() + " registros";
            else if (result.Count == 1)
                lblCantidadResultados.Text = "Resultado de la busqueda: " + result.Count.ToString() + " registro";
            else
                lblCantidadResultados.Text = "Resultado de la busqueda: 0 registro";

            foreach (Solicitud solicitud in result)
            {
                var row = new TableRow();
                row.CssClass = "TableRow";

                row.Cells.Add(AddCell(solicitud.IdSolicitud.ToString(), solicitud.IdSolicitud.ToString(), HorizontalAlign.Justify));
                row.Cells.Add(AddCell(solicitud.NumeroCartaDePorte, solicitud.NumeroCartaDePorte, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(solicitud.TipoDeCarta.Descripcion, solicitud.TipoDeCarta.Descripcion, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(solicitud.FechaCreacion.ToString("dd/MM/yyyy hh:mm"), solicitud.FechaCreacion.ToString(), HorizontalAlign.Justify));
                row.Cells.Add(AddCell((solicitud.IdEstablecimientoProcedencia != null) ? solicitud.IdEstablecimientoProcedencia.Descripcion : string.Empty, (solicitud.IdEstablecimientoProcedencia != null) ? solicitud.IdEstablecimientoProcedencia.Descripcion : string.Empty, HorizontalAlign.Justify));
                row.Cells.Add(AddCell((solicitud.IdEstablecimientoDestino != null) ? solicitud.IdEstablecimientoDestino.Descripcion : string.Empty, (solicitud.IdEstablecimientoProcedencia != null) ? solicitud.IdEstablecimientoProcedencia.Descripcion : string.Empty, HorizontalAlign.Justify));

                Int64 peso = 0;
                if (solicitud.CargaPesadaDestino)
                    peso = solicitud.KilogramosEstimados;
                else
                    peso = solicitud.PesoNeto.Value;

                row.Cells.Add(AddCell(peso.ToString(), peso.ToString(), HorizontalAlign.Justify));
                row.Cells.Add(AddCell(solicitud.UsuarioCreacion.Split('\\')[1].ToString(), solicitud.UsuarioCreacion, HorizontalAlign.Justify));

                // LUPA
                String link = "<a href='Index.aspx?Id=" + solicitud.IdSolicitud.ToString() +
                        "'><IMG border='0' src='../../Content/Images/magnify.gif'></a>";

                row.Cells.Add(AddCell(link, "Abrir Solicitud", HorizontalAlign.Center));


                tblData.Rows.Add(row);

            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarTitulos();
            DatosFiltro(txtBuscar.Text.Trim());
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

        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            btnBuscar_Click(null, null);
        }

        protected void linkConfirmacionDeArribo_Click(object sender, EventArgs e)
        {

        }
    }
}

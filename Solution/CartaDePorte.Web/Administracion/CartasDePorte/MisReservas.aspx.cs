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
    public partial class MisReservas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Main master = (Main)Page.Master;
            master.HiddenValue = "Administracion";

            if (!App.UsuarioTienePermisos("Alta Solicitud"))
            {
                Response.Redirect("~/SinAutorizacion.aspx");
                return;
            }


            if (!IsPostBack)
            {
                popTipoCartaDePorte();
                popEstablecimientoProcedencia();
            }

            String cartaDePorteCancelada = Request["cdpAnulada"];
            String cartaDePorteAnulada = Request["cdpAnulada2"];

            if (!String.IsNullOrEmpty(cartaDePorteCancelada))
            {
                lblTituloCancelacionAnulacion.Text = "¿Desea Cancelar la carta de porte " + cartaDePorteAnulada + " previamente reservada?";
                btnCancelarAnular.Text = "Si, Cancelar";
                ConfirmacionCancelacionAnulacion.Visible = true;

                //CartaDePorteDAO.Instance.CancelarReservaCartaDePorte(cartaDePorteCancelada, App.Usuario.Nombre);
                //Response.Redirect("MisReservas.aspx");
            }
            if (!String.IsNullOrEmpty(cartaDePorteAnulada))
            {
                lblTituloCancelacionAnulacion.Text = "¿Desea Anular la carta de porte " + cartaDePorteAnulada + " previamente reservada?. Recuerde que la carta de porte se debe ANULAR solamente si fue utilizada y Anulada fuera del sistema.";
                ConfirmacionCancelacionAnulacion.Visible = true;
                btnCancelarAnular.Text = "Si, Anular";
                //CartaDePorteDAO.Instance.AnularReservaCartaDePorte(cartaDePorteAnulada, App.Usuario.Nombre);
                //Response.Redirect("MisReservas.aspx");
            }

            int disponibles = CartaDePorteDAO.Instance.CantidadCartasDePorteDisponibles();
            lblCantidadCartasDisponibles.Text = "Cantidad de Cartas de porte Disponibles: <b>" + disponibles.ToString() + "</b>";

            if (disponibles < 1) 
            {
                btnReservar.Enabled = false;
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
            foreach (Solicitud sol in SolicitudDAO.Instance.GetMisReservas(App.Usuario.Nombre))
            {
                var row = new TableRow();
                row.CssClass = "TableRow";

                string linkCancelacion = "<a href='MisReservas.aspx?cdpAnulada=" + sol.NumeroCartaDePorte +
                              "'><IMG border='0' src='../../Content/Images/icon_Delete.png' width='14' height='14'></a>";
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

        protected void btnReservar_Click(object sender, EventArgs e)
        {

            if (Validaciones())
            {
                TipoDeCarta tipoCarta = TipoDeCartaDAO.Instance.GetOne(Convert.ToInt32(cboTipoCartaDePorte.SelectedValue));
                Establecimiento estabOrigenParaCDP = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(cboEstablecimientoProcedencia.SelectedValue));

                if (estabOrigenParaCDP != null)
                {
                    int IdTipoCarta = 0;
                    if (tipoCarta != null && tipoCarta.IdTipoDeCarta > 0)
                        IdTipoCarta = tipoCarta.IdTipoDeCarta;

                    int cdp = CartaDePorteDAO.Instance.ReservaCartaDePorte(App.Usuario.Nombre, estabOrigenParaCDP.IdEstablecimiento, IdTipoCarta);

                    if (cdp == 0)
                    {
                        lblMensaje.Text = "No Hay Cartas de porte disponibles para el establecimiento seleccionado.";
                    }

                    CargarTitulos();
                    Datos();                    


                }


            
            }
        }
        private void popTipoCartaDePorte() {

            ListItem li;
            li = new ListItem();
            li.Value = "-1";
            li.Text = "[seleccione...]";
            cboTipoCartaDePorte.Items.Add(li);

            foreach (TipoDeCarta tcp in TipoDeCartaDAO.Instance.GetAll())
            {
                li = new ListItem();
                li.Value = tcp.IdTipoDeCarta.ToString();
                li.Text = tcp.Descripcion;

                if (li.Text.Equals("Venta de granos propios") ||
                    li.Text.Equals("Traslado de granos") ||
                    li.Text.Equals("Canje"))
                {
                    cboTipoCartaDePorte.Items.Add(li);
                }
            }

        
        }
    
        private void popEstablecimientoProcedencia()
        {
            ListItem li;
            li = new ListItem();
            li.Value = "-1";
            li.Text = "[seleccione...]";
            cboEstablecimientoProcedencia.Items.Add(li);

            foreach (Establecimiento e in EstablecimientoDAO.Instance.GetEstablecimientoOrigenDestino(true))
            {
                li = new ListItem();
                li.Value = e.IdEstablecimiento.ToString();
                li.Text = e.Descripcion;

                cboEstablecimientoProcedencia.Items.Add(li);
            }

        }


        private bool Validaciones()
        {
            lblMensaje.Text = string.Empty;

            // Validacion de formatos.
            string mensaje = string.Empty;


            if (cboTipoCartaDePorte.SelectedIndex < 1)
            {
                mensaje += "Debe seleccionar un Tipo de Carta de Porte<br>";
            }

            if (cboEstablecimientoProcedencia.SelectedIndex < 1)
            {
                mensaje += "Debe seleccionar un Establecimiento de Procedencia<br>";
            }

            if (mensaje.Length > 0)
            {
                lblMensaje.ForeColor = Color.Red;
                lblMensaje.Text = mensaje;
                return false;
            }

            lblMensaje.ForeColor = Color.Green;
            return true;
        }

        protected void btnCerrarCliente_Click(object sender, EventArgs e)
        {

            ConfirmacionCancelacionAnulacion.Visible = false;

            String cartaDePorteCancelada = Request["cdpAnulada"];
            String cartaDePorteAnulada = Request["cdpAnulada2"];

            if (!String.IsNullOrEmpty(cartaDePorteCancelada))
            {
                CartaDePorteDAO.Instance.CancelarReservaCartaDePorte(cartaDePorteCancelada, App.Usuario.Nombre);
                Response.Redirect("MisReservas.aspx");
            }
            if (!String.IsNullOrEmpty(cartaDePorteAnulada))
            {
                CartaDePorteDAO.Instance.AnularReservaCartaDePorte(cartaDePorteAnulada, App.Usuario.Nombre);
                Response.Redirect("MisReservas.aspx");
            }

        }

        protected void btnAhorano_Click(object sender, EventArgs e)
        {
            ConfirmacionCancelacionAnulacion.Visible = false;
            Response.Redirect("MisReservas.aspx");
        }


    }
}

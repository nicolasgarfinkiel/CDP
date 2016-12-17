using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CartaDePorte.Core.DAO;
using CartaDePorte.Core.Servicios;
using CartaDePorte.Core.Domain;
using System.Drawing;
using CartaDePorte.Core.Utilidades;
using System.IO;
using CartaDePorte.Core.Domain.Seguridad;
using CartaDePorte.Core;

namespace CartaDePorte.Web
{
    public partial class contingenciasestados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(App.UsuarioTienePermisos("BaseDeDatos") || App.UsuarioTienePermisos("SeguimientoEstados")))
            {
                Response.Redirect("~/SinAutorizacion.aspx");
                return;
            }

            string fv = Request.Form[txtFechaVencimiento.UniqueID];

            if (!IsPostBack)
            {
                CargarCombos();
                hbBuscador.Value = string.Empty;

                if (Request["id"] != null)
                {
                    string idSolicitud = Request["id"];
                    Solicitud sol = SolicitudDAO.Instance.GetOne(Convert.ToInt32(idSolicitud));

                    CargarSolicitud(sol, false);
                }
            }

            ValoresCargados();

            txtCtgManual.Enabled = true;
            cboEstadoEnAFIP.Enabled = true;

            if (PaisDAO.Instance.GetOne(App.Usuario.IdPais).Descripcion.ToUpper().Contains("PARAGUAY"))
            {
                txtCtgManual.Visible = false;
                cboEstadoEnAFIP.Visible = false;
                lblCtg.Visible = false;
                lblMensaje.Visible = false;
                Label30.Visible = false;
            }
        }

        private void CargarSolicitud(Solicitud sol, bool modelo)
        {
            btnGuardar.Visible = false;
            txtCtgManual.Visible = true;
            txtFechaVencimiento.Visible = true;
            txtFechaVencimiento.Enabled = false;
            lblMensaje.Text = sol.ObservacionAfip;

            if (sol.ObservacionAfip.Contains("Reserva"))
            {
                btnGuardar.Visible = false;
                txtCtgManual.Visible = true;
                txtFechaVencimiento.Visible = true;
                txtFechaVencimiento.Enabled = false;
                lblMensaje.Text = sol.ObservacionAfip;
            }
            else
            {
                lblTituloSeccion6.Text = "Carta de Porte: " + sol.NumeroCartaDePorte.ToString();

                txtCtgManual.Text = sol.Ctg;
                txtFechaVencimiento.Text = (sol.FechaDeVencimiento.HasValue) ? sol.FechaDeVencimiento.Value.ToString("dd/MM/yyyy") : string.Empty;
                cboEstadoEnAFIP.SelectedValue = Convert.ToInt32(sol.EstadoEnAFIP).ToString();
                cboEstadoEnSAP.SelectedValue = Convert.ToInt32(sol.EstadoEnSAP).ToString();

                lblMensaje.Text = sol.ObservacionAfip;

                if (!modelo && !String.IsNullOrEmpty(sol.Ctg) && sol.EstadoEnAFIP == Enums.EstadoEnAFIP.Anulada)
                {
                    lblMensaje.Text = sol.ObservacionAfip + " - Codigo de Anulación AFIP: " + sol.CodigoAnulacionAfip;
                }
            }

            txtCtgManual.Visible = true;
            btnGuardar.Visible = true;
            btnGuardar.Enabled = true;
        }

        private void ValoresCargados()
        {
            if (Request["__EVENTTARGET"] == "guardar")
            {
                btnGuardar_Click(null, null);
            }
        }

        private void CargarCombos()
        {
            popEstadoEnAFIP();
            popEstadoEnSAP();
        }

        private void popEstadoEnSAP()
        {
            ListItem li;
            li = new ListItem();
            li.Value = "-1";
            li.Text = "[seleccione...]";
            cboEstadoEnSAP.Items.Add(li);

            foreach (Enums.EstadoEnvioSAP o in Enum.GetValues(typeof(Enums.EstadoEnvioSAP)))
            {
                li = new ListItem();
                li.Value = Convert.ToInt32(o).ToString();
                li.Text = o.ToString();
                cboEstadoEnSAP.Items.Add(li);

            }
        }
        private void popEstadoEnAFIP()
        {
            ListItem li;
            li = new ListItem();
            li.Value = "-1";
            li.Text = "[seleccione...]";
            cboEstadoEnAFIP.Items.Add(li);

            foreach (Enums.EstadoEnAFIP o in Enum.GetValues(typeof(Enums.EstadoEnAFIP)))
            {
                li = new ListItem();
                li.Value = Convert.ToInt32(o).ToString();
                li.Text = o.ToString();
                cboEstadoEnAFIP.Items.Add(li);

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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            btnGuardar.Enabled = false;
            string fv = Request.Form[txtFechaVencimiento.UniqueID];

            if (Validaciones())
            {
                Solicitud solicitud = new Solicitud();

                if (Request["id"] != null)
                {
                    string idSolicitud = Request["id"];
                    solicitud = SolicitudDAO.Instance.GetOne(Convert.ToInt32(idSolicitud));
                }
                solicitud.Ctg = txtCtgManual.Text;

                if (!String.IsNullOrEmpty(fv))
                    solicitud.FechaDeVencimiento = StringToDateTime(fv);

                solicitud.UsuarioModificacion = App.Usuario.Nombre;

                solicitud.EstadoEnSAP = (Enums.EstadoEnvioSAP)Convert.ToInt32(cboEstadoEnSAP.SelectedValue);
                solicitud.EstadoEnAFIP = (Enums.EstadoEnAFIP)Convert.ToInt32(cboEstadoEnAFIP.SelectedValue);
                SolicitudDAO.Instance.SaveOrUpdate(solicitud);
            }
        }

        private bool Validaciones()
        {
            lblMensaje.ForeColor = Color.Green;
            return true;
        }

        private bool ValidarLogicaPorTipoCartaDePorte(Solicitud solicitud)
        {

            string mensaje = string.Empty;

            if (mensaje.Length > 0)
            {
                btnGuardar.Enabled = true;
                lblMensaje.ForeColor = Color.Red;
                lblMensaje.Text = mensaje;
                return false;
            }

            lblMensaje.ForeColor = Color.Green;
            return true;
        }

        private bool isNumeric(string valor)
        {
            try
            {
                if (valor.Trim().Length > 0)
                    Convert.ToDecimal(valor);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool isNumericValidador(string valor)
        {
            try
            {
                Convert.ToDecimal(valor);
                return true;

            }
            catch
            {
                return false;
            }
        }

        protected void btnGuardar_Click1(object sender, EventArgs e)
        {
            btnGuardar_Click(sender, e);
        }

        protected void cboTipoDeCarta_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private DateTime ConvertirFechaEmision(string fecha)
        {
            String[] fechapartes = fecha.Split('/');
            DateTime fechafinal;

            try
            {
                fechafinal = new DateTime(Convert.ToInt32(fechapartes[2]), Convert.ToInt32(fechapartes[1]), Convert.ToInt32(fechapartes[0]));
            }
            catch (Exception)
            {
                return DateTime.Now;
            }

            return fechafinal;
        }


        private DateTime StringToDateTime(String fecha)
        {

            if (fecha.Length == 10)
            {
                String[] partes = fecha.Split('/');
                if (partes.Length == 3)
                {
                    string dia = partes[0];
                    string mes = partes[1];
                    string anio = partes[2];

                    return new DateTime(Convert.ToInt32(anio), Convert.ToInt32(mes), Convert.ToInt32(dia));
                }
            }


            return DateTime.Today;

        }





    }
}

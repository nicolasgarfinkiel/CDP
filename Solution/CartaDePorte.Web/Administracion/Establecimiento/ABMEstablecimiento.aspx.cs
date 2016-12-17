using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using CartaDePorte.Core.DAO;
using CartaDePorte.Core.Domain;
using CartaDePorte.Core.Domain.Seguridad;
using CartaDePorte.Core;

namespace CartaDePorte.Web
{
    public partial class ABMEstablecimiento : System.Web.UI.Page
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

            if (!IsPostBack)
            {
                populateCombos();
                BuscadorCliente.Visible = false;

                string id = Request["Id"];
                if (id != "0")
                {
                    Establecimiento establecimiento = new Establecimiento();
                    establecimiento = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(id));
                    txtDescripcion.Text = establecimiento.Descripcion;
                    txtDireccion.Text = establecimiento.Direccion;
                    if (establecimiento.Provincia != null)
                        cboProvincia.SelectedValue = establecimiento.Provincia.Codigo.ToString();

                    cboProvincia_SelectedIndexChanged(null, null);

                    if (establecimiento.Localidad != null)
                        cboLocalidad.SelectedValue = establecimiento.Localidad.Codigo.ToString();

                    txtIDAlmacenSAP.Text = establecimiento.IDAlmacenSAP.ToString();
                    txtIDCentroSAP.Text = establecimiento.IDCentroSAP.ToString();

                    if (establecimiento.IdInterlocutorDestinatario != null)
                    {
                        txtIdInterlocutorDestinatario.Text = establecimiento.IdInterlocutorDestinatario.RazonSocial;
                        hbIdInterlocutorDestinatario.Value = establecimiento.IdInterlocutorDestinatario.IdCliente.ToString();
                    }

                    cboRecorridoEstablecimiento.SelectedValue = Convert.ToInt32(establecimiento.RecorridoEstablecimiento).ToString();
                    txtcebe.Text = establecimiento.IDCEBE;
                    txtexpedicion.Text = establecimiento.IDExpedicion;
                    txtEstablecimientoAfip.Text = establecimiento.EstablecimientoAfip;
                    chkAsociaCartaDePorte.Checked = establecimiento.AsociaCartaDePorte;

                    cboRecorridoEstablecimiento_SelectedIndexChanged(null, null);

                }
                else
                    btnEliminar.Visible = false;

                if(!PaisDAO.Instance.GetOne(App.Usuario.IdPais).Descripcion.ToUpper().Contains("ARGENTINA"))
                {
                    txtEstablecimientoAfip.Visible = false;
                    lblEstablecimientoAFIP.Visible = false;
                }
            }
            ValoresCargados();
        }

        private void ValoresCargados()
        {
            if (Request["__EVENTTARGET"] == "IdInterlocutorDestinatario")
            {
                if (Request["__EVENTARGUMENT"] != null)
                {
                    Cliente cliente = ClienteDAO.Instance.GetOne(Convert.ToInt32(Request["__EVENTARGUMENT"]));
                    txtIdInterlocutorDestinatario.Text = cliente.RazonSocial;
                    hbIdInterlocutorDestinatario.Value = cliente.IdCliente.ToString();
                    BuscadorCliente.Visible = false;
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Establecimiento establecimiento = new Establecimiento();
            if (Convert.ToInt32(Request["Id"]) > 0)
                establecimiento = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(Request["Id"]));

            if (this.validar())
            {
                establecimiento.Descripcion = txtDescripcion.Text;
                establecimiento.Direccion = txtDireccion.Text;
                establecimiento.Provincia = ProvinciaDAO.Instance.GetOne(Convert.ToInt32(cboProvincia.SelectedValue));
                establecimiento.Localidad = LocalidadDAO.Instance.GetOne(Convert.ToInt32(cboLocalidad.SelectedValue));
                establecimiento.IDAlmacenSAP = Convert.ToInt32(txtIDAlmacenSAP.Text);
                establecimiento.IDCentroSAP = Convert.ToInt32(txtIDCentroSAP.Text);
                establecimiento.IdInterlocutorDestinatario = ClienteDAO.Instance.GetOne(Convert.ToInt32(hbIdInterlocutorDestinatario.Value));
                establecimiento.RecorridoEstablecimiento = (Enums.RecorridoEstablecimiento)Convert.ToInt32(cboRecorridoEstablecimiento.SelectedValue);
                establecimiento.IDCEBE = txtcebe.Text.Trim();
                establecimiento.IDExpedicion = txtexpedicion.Text.Trim();
                establecimiento.EstablecimientoAfip = txtEstablecimientoAfip.Text.Trim();
                establecimiento.AsociaCartaDePorte = chkAsociaCartaDePorte.Checked;

                if (establecimiento.IdEstablecimiento > 0)
                    establecimiento.UsuarioModificacion = App.Usuario.Nombre;
                else
                    establecimiento.UsuarioCreacion = App.Usuario.Nombre;
            }

            if (this.validar())
            {
                if (EstablecimientoDAO.Instance.SaveOrUpdate(establecimiento) < 1)
                {
                    lblMensaje.ForeColor = Color.Green;
                    lblMensaje.Text = "Los datos fueron guardados correctamente.";
                    LimpiarForm();
                }
                else
                {
                    lblMensaje.ForeColor = Color.Red;
                    lblMensaje.Text = "No pudo completarse la operacion, por favor, intentar nuevamente mas tarde.";
                }
            }
        }

        private bool validar()
        {
            lblMensaje.ForeColor = Color.Red;
            lblMensaje.Text = string.Empty;
            string errores = string.Empty;

            if (txtDescripcion.Text.Trim().Length < 1)
            {
                errores = "Debe completar una Descripcion para el Establecimiento<br>";
                lblMensaje.Text += errores;
            }

            if (txtDireccion.Text.Trim().Length < 1)
            {
                errores = "Debe completar una dirección para el establecimiento<br>";
                lblMensaje.Text += errores;
            }

            if (String.IsNullOrEmpty(hbIdInterlocutorDestinatario.Value))
            {
                errores = "Debe seleccionar un cliente como Interlocutor Destinatario<br>";
                lblMensaje.Text += errores;
            }

            if (cboProvincia.SelectedIndex < 1)
            {
                errores = "Debe seleccionar una Provincia<br>";
                lblMensaje.Text += errores;
            }

            if (cboLocalidad.SelectedIndex < 1)
            {
                errores = "Debe seleccionar una Localidad<br>";
                lblMensaje.Text += errores;
            }

            if (String.IsNullOrEmpty(txtexpedicion.Text.Trim()))
            {
                errores = "Debe completar el id de Expedicion<br>";
                lblMensaje.Text += errores;
            }

            if (errores.Length > 0)
            {
                return false;
            }

            lblMensaje.ForeColor = Color.Green;
            lblMensaje.Text = "OK";
            return true;

        }

        private void LimpiarForm()
        {
            txtDescripcion.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            cboProvincia.SelectedIndex = 0;
            cboLocalidad.SelectedIndex = 0;
            cboRecorridoEstablecimiento.SelectedIndex = 0;
        }

        private void populateCombos()
        {
            PopProvincia();
            PopTipo();
        }
        private void PopProvincia()
        {
            ListItem li;
            li = new ListItem();
            li.Value = "-1";
            li.Text = "[seleccione...]";
            cboProvincia.Items.Add(li);

            foreach (Provincia p in ProvinciaDAO.Instance.GetAll())
            {
                li = new ListItem();
                li.Value = p.Codigo.ToString();
                li.Text = p.Descripcion;
                cboProvincia.Items.Add(li);
            }
        }

        private void PopTipo()
        {
            ListItem li;

            foreach (Enums.RecorridoEstablecimiento o in Enum.GetValues(typeof(Enums.RecorridoEstablecimiento)))
            {
                li = new ListItem();
                li.Value = Convert.ToInt32(o).ToString();
                li.Text = o.ToString();
                cboRecorridoEstablecimiento.Items.Add(li);
            }
        }

        protected void cboProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboLocalidad.Items.Clear();
            ListItem li;
            li = new ListItem();
            li.Value = "-1";
            li.Text = "[seleccione...]";
            cboLocalidad.Items.Add(li);

            foreach (Localidad l in LocalidadDAO.Instance.GetLocalidadByIDProvincia(Convert.ToInt32(cboProvincia.SelectedValue)))
            {
                li = new ListItem();
                li.Value = l.Codigo.ToString();
                li.Text = l.Descripcion;
                cboLocalidad.Items.Add(li);
            }
        }

        protected void ImageButtonClienteInterlocutorDestinatario_Click(object sender, ImageClickEventArgs e)
        {
            tblBuscadorCliente.Rows.Clear();
            txtBuscadorCliente.Text = string.Empty;
            BuscadorCliente.Visible = true;
        }

        protected void btnCerrarCliente_Click(object sender, EventArgs e)
        {
            BuscadorCliente.Visible = false;
        }

        private void CargarTitulosBuscadorCliente()
        {
            var row = new TableRow();
            row.CssClass = "TableRowTitle";
            row.Cells.Add(AddTitleCell("Razon Social Cliente", 200));
            row.Cells.Add(AddTitleCell("Cuit Cliente", 100));
            row.Cells.Add(AddTitleCell("Id Cliente", 80));
            row.Cells.Add(AddTitleCell("", 10));

            tblBuscadorCliente.Rows.Add(row);
        }

        private void DatosBuscadorCliente(string busqueda)
        {
            foreach (Cliente dato in ClienteDAO.Instance.GetFiltro(busqueda, false, false))
            {
                var row = new TableRow();
                row.CssClass = "TableRow";

                row.Cells.Add(AddCell(dato.RazonSocial, dato.RazonSocial, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(dato.Cuit, dato.Cuit, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(dato.IdCliente.ToString(), dato.IdCliente.ToString(), HorizontalAlign.Justify));

                string link = "<a href='#' onClick='BuscadorManager2(" + dato.IdCliente.ToString() + ",\"" + "IdInterlocutorDestinatario" + "\",\"" + dato.RazonSocial + "\",\"" + dato.Cuit + "\")" +
                             "'><IMG border='0' src='../../Content/Images/icon_select.gif'></a>";

                row.Cells.Add(AddCell(link, string.Empty, HorizontalAlign.Center));

                tblBuscadorCliente.Rows.Add(row);
            }
        }

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

        protected void btnBuscadorCliente_Click(object sender, EventArgs e)
        {
            CargarTitulosBuscadorCliente();
            DatosBuscadorCliente(txtBuscadorCliente.Text.Trim());
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            EstablecimientoDAO.Instance.EliminarEstablecimiento(Convert.ToInt32(Request["Id"]), App.Usuario.Nombre);
            Response.Redirect("EstablecimientoSearch.aspx");
        }

        protected void cboRecorridoEstablecimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cboRecorridoEstablecimiento.SelectedValue) > -1)
            {
                Enums.RecorridoEstablecimiento recorrido = (Enums.RecorridoEstablecimiento)Convert.ToInt32(cboRecorridoEstablecimiento.SelectedValue);
                if (recorrido == Enums.RecorridoEstablecimiento.SoloDestino)
                    chkAsociaCartaDePorte.Enabled = false;
                else
                    chkAsociaCartaDePorte.Enabled = true;
            }
        }
    }
}
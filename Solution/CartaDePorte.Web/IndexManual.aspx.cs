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
using CartaDePorte.Common;

namespace CartaDePorte.Web
{
    public partial class IndexManual : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Main master = (Main)Page.Master;
            master.HiddenValue = "PrincipalManual";

            
            if (!App.UsuarioTienePermisos("Alta Manual"))
            {
                Response.Redirect("~/SinAutorizacion.aspx");
                return;
            }


            if (!IsPostBack)
            {
                CargarCombos();
                hbBuscador.Value = string.Empty;
                BuscadorEmpresa.Visible = false;
                BuscadorCliente.Visible = false;
                BuscadorProveedor.Visible = false;
                BuscadorChofer.Visible = false;
                
            }
            ValoresCargados();
        }



        private void ValoresCargados()
        {
            if (Request["__EVENTTARGET"] == "ProveedorTitularCartaDePorte")
            {

                if (Request["__EVENTARGUMENT"] != null)
                {
                    Proveedor proveedor = ProveedorDAO.Instance.GetOne(Convert.ToInt32(Request["__EVENTARGUMENT"]));
                    cboProveedorTitularCartaDePorte.Text = proveedor.Nombre;
                    txtCuitProveedorTitularCartaDePorte.Text = proveedor.NumeroDocumento;
                    hbProveedorTitularCartaDePorte.Value = proveedor.IdProveedor.ToString();
                    BuscadorProveedor.Visible = false;

                }
            }

            if (Request["__EVENTTARGET"] == "ClienteIntermediario")
            {
                if (Request["__EVENTARGUMENT"] != null)
                {
                    Cliente cliente = ClienteDAO.Instance.GetOne(Convert.ToInt32(Request["__EVENTARGUMENT"]));
                    cboClienteIntermediario.Text = cliente.RazonSocial;
                    txtCuitClienteIntermediario.Text = cliente.Cuit;
                    hbClienteIntermediario.Value = cliente.IdCliente.ToString();
                    BuscadorCliente.Visible = false;

                }

            }

            if (Request["__EVENTTARGET"] == "ClienteRemitenteComercial")
            {
                if (Request["__EVENTARGUMENT"] != null)
                {
                    Cliente cliente = ClienteDAO.Instance.GetOne(Convert.ToInt32(Request["__EVENTARGUMENT"]));
                    cboClienteRemitenteComercial.Text = cliente.RazonSocial;
                    txtCuitClienteRemitenteComercial.Text = cliente.Cuit;
                    hbClienteRemitenteComercial.Value = cliente.IdCliente.ToString();
                    tblRemitenteComercialComoCanjeador.Visible = true;
                    BuscadorCliente.Visible = false;
                }

            }
            if (Request["__EVENTTARGET"] == "ClienteCorredor")
            {
                if (Request["__EVENTARGUMENT"] != null)
                {
                    Cliente cliente = ClienteDAO.Instance.GetOne(Convert.ToInt32(Request["__EVENTARGUMENT"]));
                    cboClienteCorredor.Text = cliente.RazonSocial;
                    txtCuitClienteCorredor.Text = cliente.Cuit;
                    hbClienteCorredor.Value = cliente.IdCliente.ToString();
                    BuscadorCliente.Visible = false;
                }


            }
            if (Request["__EVENTTARGET"] == "ClienteEntregador")
            {
                if (Request["__EVENTARGUMENT"] != null)
                {
                    Cliente cliente = ClienteDAO.Instance.GetOne(Convert.ToInt32(Request["__EVENTARGUMENT"]));
                    cboClienteEntregador.Text = cliente.RazonSocial;
                    txtCuitClienteEntregador.Text = cliente.Cuit;
                    hbClienteEntregador.Value = cliente.IdCliente.ToString();
                    BuscadorCliente.Visible = false;
                }


            }
            if (Request["__EVENTTARGET"] == "ClienteDestinatario")
            {
                if (Request["__EVENTARGUMENT"] != null)
                {
                    Cliente cliente = ClienteDAO.Instance.GetOne(Convert.ToInt32(Request["__EVENTARGUMENT"]));
                    cboClienteDestinatario.Text = cliente.RazonSocial;
                    txtCuitClienteDestinatario.Text = cliente.Cuit;
                    hbClienteDestinatario.Value = cliente.IdCliente.ToString();
                    BuscadorCliente.Visible = false;
                }

            }
            if (Request["__EVENTTARGET"] == "ClienteDestinatarioCambio")
            {
                if (Request["__EVENTARGUMENT"] != null)
                {
                    Cliente cliente = ClienteDAO.Instance.GetOne(Convert.ToInt32(Request["__EVENTARGUMENT"]));
                    cboClienteDestinatarioCambio.Text = cliente.RazonSocial;
                    txtCuitClienteDestinatarioCambio.Text = cliente.Cuit;
                    hbClienteDestinatarioCambio.Value = cliente.IdCliente.ToString();
                    BuscadorCliente.Visible = false;
                }
            }
            if (Request["__EVENTTARGET"] == "ClienteDestino")
            {
                if (Request["__EVENTARGUMENT"] != null)
                {
                    Cliente cliente = ClienteDAO.Instance.GetOne(Convert.ToInt32(Request["__EVENTARGUMENT"]));
                    cboClienteDestino.Text = cliente.RazonSocial;
                    txtCuitClienteDestino.Text = cliente.Cuit;
                    hbClienteDestino.Value = cliente.IdCliente.ToString();
                    BuscadorCliente.Visible = false;
                }


            }
            if (Request["__EVENTTARGET"] == "ProveedorTransportista")
            {
                if (Request["__EVENTARGUMENT"] != null)
                {

                    Proveedor proveedor = ProveedorDAO.Instance.GetOne(Convert.ToInt32(Request["__EVENTARGUMENT"]));
                    cboProveedorTransportista.Text = proveedor.Nombre;
                    txtCuitProveedorTransportista.Text = proveedor.NumeroDocumento;
                    hbProveedorTransportista.Value = proveedor.IdProveedor.ToString();
                    BuscadorProveedor.Visible = false;
                }

            }
            if (Request["__EVENTTARGET"] == "Chofer")
            {
                if (Request["__EVENTARGUMENT"] != null)
                {

                    Chofer chofer = ChoferDAO.Instance.GetOne(Convert.ToInt32(Request["__EVENTARGUMENT"]));

                    if (Session["OrigenBuscadorChofer"].ToString() != "ChoferTransportista")
                    {
                        cboChofer.Text = chofer.Apellido + ", " + chofer.Nombre;
                        txtCuitChofer.Text = chofer.Cuit;
                        hbChofer.Value = chofer.IdChofer.ToString();
                        txtPatente.Text = chofer.Camion;
                        txtAcoplado.Text = chofer.Acoplado;
                    }
                    else {
                        cboProveedorTransportista.Text = chofer.Nombre;
                        txtCuitProveedorTransportista.Text = chofer.Cuit;
                        hbProveedorTransportista.Value = chofer.IdChofer.ToString();                    
                    }

                    BuscadorChofer.Visible = false;
                }

            }

            if (Request["__EVENTTARGET"] == "ClientePagadorDelFlete")
            {
                if (Request["__EVENTARGUMENT"] != null)
                {
                    Cliente cliente = ClienteDAO.Instance.GetOne(Convert.ToInt32(Request["__EVENTARGUMENT"]));
                    cboClientePagadorDelFlete.Text = cliente.RazonSocial;
                    hbClientePagadorDelFlete.Value = cliente.IdCliente.ToString();
                    if (!ChequeoIntegridadPagadorVsTransportista())
                    {
                        cboProveedorTransportista.Text = string.Empty;
                        txtCuitProveedorTransportista.Text = string.Empty;
                        hbProveedorTransportista.Value = string.Empty; 
                    }
                    BuscadorCliente.Visible = false;

                }

            }



        }

        private void CargarCombos()
        {
            popGrano();
            popEstablecimientoProcedencia();
            popEstablecimientoDestino();
            popEstablecimientoDestinoCambio();
            popTipoDeCarta();

        }

        private void popGrano()
        {
            ListItem li;
            li = new ListItem();
            li.Value = "-1";
            li.Text = "[seleccione...]";
            cboGrano.Items.Add(li);

            foreach (Grano g in GranoDAO.Instance.GetAll())
            {
                li = new ListItem();
                li.Value = g.IdGrano.ToString();
                li.Text = g.Descripcion + " - " + g.CosechaAfip.Codigo;

                cboGrano.Items.Add(li);
            }

        }
        private void popEstablecimientoProcedencia()
        {
            ListItem li;
            li = new ListItem();
            li.Value = "-1";
            li.Text = "[seleccione...]";
            cboIdEstablecimientoProcedencia.Items.Add(li);

            foreach (Establecimiento e in EstablecimientoDAO.Instance.GetEstablecimientoOrigenDestino(true))
            {
                li = new ListItem();
                li.Value = e.IdEstablecimiento.ToString();
                li.Text = e.Descripcion;

                cboIdEstablecimientoProcedencia.Items.Add(li);
            }

        }
        private void popEstablecimientoDestino()
        {
            ListItem li;
            li = new ListItem();
            li.Value = "-1";
            li.Text = "[seleccione...]";
            cboIdEstablecimientoDestino.Items.Add(li);

            foreach (Establecimiento e in EstablecimientoDAO.Instance.GetEstablecimientoOrigenDestino(false))
            {
                li = new ListItem();
                li.Value = e.IdEstablecimiento.ToString();
                li.Text = e.Descripcion;

                cboIdEstablecimientoDestino.Items.Add(li);
            }

        }
        //popEstablecimientoDestinoCambio()
        private void popEstablecimientoDestinoCambio()
        {
            ListItem li;
            li = new ListItem();
            li.Value = "-1";
            li.Text = "[seleccione...]";
            cboIdEstablecimientoDestinoCambio.Items.Add(li);

            foreach (Establecimiento e in EstablecimientoDAO.Instance.GetEstablecimientoOrigenDestino(false))
            {
                li = new ListItem();
                li.Value = e.IdEstablecimiento.ToString();
                li.Text = e.Descripcion;

                cboIdEstablecimientoDestinoCambio.Items.Add(li);
            }

        }
        //popTipoDeCarta();
        private void popTipoDeCarta()
        {
            ListItem li;
            li = new ListItem();
            li.Value = "-1";
            li.Text = "[seleccione...]";
            cboTipoDeCartam.Items.Add(li);

            foreach (TipoDeCarta tcp in TipoDeCartaDAO.Instance.GetAll())
            {
                li = new ListItem();
                li.Value = tcp.IdTipoDeCarta.ToString();
                li.Text = tcp.Descripcion;

                cboTipoDeCartam.Items.Add(li);
            }

        }
        protected void cboGrano_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboGrano.SelectedIndex > 0)
            {
                Grano grano = GranoDAO.Instance.GetOne(Convert.ToInt32(cboGrano.SelectedValue));
                txtTipoGrano.Text = (grano.TipoGrano != null) ? grano.TipoGrano.Descripcion : string.Empty;
                txtCosecha.Text = (grano.CosechaAfip != null) ? grano.CosechaAfip.Descripcion : string.Empty;
            }
            else
            {
                txtTipoGrano.Text = string.Empty;
                txtCosecha.Text = string.Empty;
            }


        }
        protected void btnBuscadorEmpresa_Click(object sender, EventArgs e)
        {
            CargarTitulosBuscadorEmpresa();
            DatosBuscadorEmpresa(txtBuscadorEmpresa.Text.Trim());

        }
        protected void btnBuscadorCliente_Click(object sender, EventArgs e)
        {
            lblMensajeClienteAltaRapida.Text = string.Empty;
            CargarTitulosBuscadorCliente();
            DatosBuscadorCliente(txtBuscadorCliente.Text.Trim());

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

        private void CargarTitulosBuscadorEmpresa()
        {
            var row = new TableRow();
            row.CssClass = "TableRowTitle";
            row.Cells.Add(AddTitleCell("Nombre Empresa", 200));
            //row.Cells.Add(AddTitleCell("Razon Social Cliente", 200));
            row.Cells.Add(AddTitleCell("Cuit Cliente", 100));
            row.Cells.Add(AddTitleCell("Id Cliente", 80));
            row.Cells.Add(AddTitleCell("", 10));

            tblBuscadorEmpresa.Rows.Add(row);

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

        private void DatosBuscadorEmpresa(string busqueda)
        {
            foreach (Empresa dato in EmpresaDAO.Instance.GetFiltro(busqueda))
            {
                var row = new TableRow();
                row.CssClass = "TableRow";

                row.Cells.Add(AddCell(dato.Descripcion, dato.Descripcion, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(dato.Cliente.Cuit, dato.Cliente.Cuit, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(dato.Cliente.IdCliente.ToString(), dato.Cliente.IdCliente.ToString(), HorizontalAlign.Justify));

                string link = "<a href='#' onClick='BuscadorManager(" + dato.IdEmpresa.ToString() + ",\"" + hbBuscador.Value + "\",\"" + dato.Descripcion + "\",\"" + dato.Cliente.Cuit + "\")" +
                             "'><IMG border='0' src='../../Content/Images/icon_select.gif'></a>";

                row.Cells.Add(AddCell(link, string.Empty, HorizontalAlign.Center));

                tblBuscadorEmpresa.Rows.Add(row);

            }
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

                string link = "<a href='#' onClick='BuscadorManager(" + dato.IdCliente.ToString() + ",\"" + hbBuscador.Value + "\",\"" + dato.RazonSocial + "\",\"" + dato.Cuit + "\")" +
                             "'><IMG border='0' src='../../Content/Images/icon_select.gif'></a>";

                row.Cells.Add(AddCell(link, string.Empty, HorizontalAlign.Center));

                tblBuscadorCliente.Rows.Add(row);

            }
        }

        #endregion

        protected void ImageButtonProveedorTitularCartaDePorte_Click(object sender, ImageClickEventArgs e)
        {
            hbBuscador.Value = "ProveedorTitularCartaDePorte";
            tblBuscadorProveedor.Rows.Clear();
            txtBuscadorProveedor.Text = string.Empty;

            lblMensajeProveedorAltaRapida.Text = string.Empty;

            BuscadorProveedor.Visible = true;
            ImageButtonDeleteTitularCartaPorte.Visible = true;

            CargarProveedoresUsados();


        }
        protected void ImageButtonClienteRemitenteComercial_Click(object sender, ImageClickEventArgs e)
        {
            hbBuscador.Value = "ClienteRemitenteComercial";
            tblBuscadorEmpresa.Rows.Clear();
            txtBuscadorEmpresa.Text = string.Empty;
            
            lblMensajeClienteAltaRapida.Text = string.Empty;

            BuscadorCliente.Visible = true;
            ImageButtonDeleteRemitenteComercial.Visible = true;
            CargarClientesUsados();

        }
        protected void ImageButtonClienteIntermediario_Click(object sender, ImageClickEventArgs e)
        {
            hbBuscador.Value = "ClienteIntermediario";
            tblBuscadorCliente.Rows.Clear();
            txtBuscadorCliente.Text = string.Empty;

            lblMensajeClienteAltaRapida.Text = string.Empty;

            BuscadorCliente.Visible = true;
            ImageButtonDeleteIntermediario.Visible = true;
            CargarClientesUsados();

        }
        protected void ImageButtonClienteCorredor_Click(object sender, ImageClickEventArgs e)
        {
            hbBuscador.Value = "ClienteCorredor";
            tblBuscadorCliente.Rows.Clear();
            txtBuscadorCliente.Text = string.Empty;

            lblMensajeClienteAltaRapida.Text = string.Empty;

            BuscadorCliente.Visible = true;
            ImageButtonDeleteCorredor.Visible = true;
            CargarClientesUsados();
        }
        // Proveedor.
        protected void btnBuscadorProveedor_Click(object sender, EventArgs e)
        {
            lblMensajeProveedorAltaRapida.Text = string.Empty;
            CargarTitulosBuscadorProveedor();
            DatosBuscadorProveedor(txtBuscadorProveedor.Text.Trim());

        }
        private void CargarTitulosBuscadorProveedor()
        {
            var row = new TableRow();
            row.CssClass = "TableRowTitle";
            row.Cells.Add(AddTitleCell("Nombre", 200));
            row.Cells.Add(AddTitleCell("Cuit ", 100));
            row.Cells.Add(AddTitleCell("", 10));

            tblBuscadorProveedor.Rows.Add(row);

        }
        private void DatosBuscadorProveedor(string busqueda)
        {
            foreach (Proveedor dato in ProveedorDAO.Instance.GetFiltro(busqueda))
            {
                var row = new TableRow();
                row.CssClass = "TableRow";

                row.Cells.Add(AddCell(dato.Nombre, dato.Nombre, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(dato.NumeroDocumento, dato.NumeroDocumento, HorizontalAlign.Justify));

                string link = "<a href='#' onClick='BuscadorManager(" + dato.IdProveedor.ToString() + ",\"" + hbBuscador.Value + "\",\"" + dato.Nombre + "\",\"" + dato.NumeroDocumento + "\")" +
                             "'><IMG border='0' src='../../Content/Images/icon_select.gif'></a>";

                row.Cells.Add(AddCell(link, string.Empty, HorizontalAlign.Center));

                tblBuscadorProveedor.Rows.Add(row);

            }
        }
        //ClienteEntregador
        protected void ImageButtonClienteEntregador_Click(object sender, ImageClickEventArgs e)
        {
            hbBuscador.Value = "ClienteEntregador";
            tblBuscadorCliente.Rows.Clear();
            txtBuscadorCliente.Text = string.Empty;
            
            lblMensajeClienteAltaRapida.Text = string.Empty;

            BuscadorCliente.Visible = true;
            ImageButtonDeleteRepresentanteEntregador.Visible = true;
            CargarClientesUsados();
        }
        //ClienteDestinatario
        protected void ImageButtonClienteDestinatario_Click(object sender, ImageClickEventArgs e)
        {
            hbBuscador.Value = "ClienteDestinatario";
            tblBuscadorCliente.Rows.Clear();
            txtBuscadorCliente.Text = string.Empty;

            lblMensajeClienteAltaRapida.Text = string.Empty;

            BuscadorCliente.Visible = true;
            ImageButtonDeleteDestinatario.Visible = true;
            CargarClientesUsados();

        }
        //ClienteDestino
        protected void ImageButtonClienteDestino_Click(object sender, ImageClickEventArgs e)
        {
            hbBuscador.Value = "ClienteDestino";
            tblBuscadorCliente.Rows.Clear();
            txtBuscadorCliente.Text = string.Empty;

            lblMensajeClienteAltaRapida.Text = string.Empty;

            BuscadorCliente.Visible = true;
            ImageButtonDeleteDestino.Visible = true;
            CargarClientesUsados();

        }
        //Chofer
        protected void btnBuscadorChofer_Click(object sender, EventArgs e)
        {
            lblMensajeChoferAltaRapida.Text = string.Empty;
            CargarTitulosBuscadorChofer();
            if (Session["OrigenBuscadorChofer"].ToString() == "Chofer")
            {
                DatosBuscadorChofer(txtBuscadorChofer.Text.Trim(), false);
            }
            else
            {
                DatosBuscadorChofer(txtBuscadorChofer.Text.Trim(), true);
            }

        }
        protected void btnCargaRapidaChofer_Click(object sender, EventArgs e)
        {
            if (validacionChoferAltaRapida())
            {
                bool estrans = true;
                //doy de alta el chofer
                Chofer chofernuevo = new Chofer();
                chofernuevo.Nombre = txtChoferAltaRapidaNombre.Text.Trim();                
                chofernuevo.Cuit = txtChoferAltaRapidaCuit.Text.Trim();
                chofernuevo.UsuarioCreacion = App.Usuario.Nombre;
                chofernuevo.EsChoferTransportista = Enums.EsChoferTransportista.Si;

                if (Session["OrigenBuscadorChofer"].ToString() != "ChoferTransportista")
                {
                    estrans = false;
                    chofernuevo.Apellido = txtChoferAltaRapidaApellido.Text.Trim();
                    chofernuevo.Camion = txtChoferAltaRapidaCamion.Text.Trim();
                    chofernuevo.Acoplado = txtChoferAltaRapidaAcoplado.Text.Trim();
                    chofernuevo.EsChoferTransportista = Enums.EsChoferTransportista.No;
                }

                int IdChoferNuevo = ChoferDAO.Instance.SaveOrUpdate(chofernuevo);
                if (IdChoferNuevo > 0)
                {
                    //chofernuevo = ChoferDAO.Instance.GetOne(IdChoferNuevo);
                    
                    //cboChofer.Text = chofernuevo.Apellido + ", " + chofernuevo.Nombre;
                    //txtCuitChofer.Text = chofernuevo.Cuit;
                    //hbChofer.Value = chofernuevo.IdChofer.ToString();

                    CargarTitulosBuscadorChofer();

                    DatosBuscadorChofer(chofernuevo.Cuit, estrans);
                    

                }


            }
        }
        private bool validacionChoferAltaRapida()
        {
            lblMensajeChoferAltaRapida.Text = string.Empty;

            if (String.IsNullOrEmpty(txtChoferAltaRapidaNombre.Text.Trim()))
            {
                lblMensajeChoferAltaRapida.Text = "Debe completar el Nombre del chofer para Alta Rápida";
                return false;
            }

            if (String.IsNullOrEmpty(txtChoferAltaRapidaCuit.Text.Trim()))
            {
                lblMensajeChoferAltaRapida.Text = "Debe completar el Cuit del chofer para Alta Rápida";
                return false;
            }

            if (!CuitValido(txtChoferAltaRapidaCuit.Text.Trim()))
            {
                lblMensajeChoferAltaRapida.Text = "Cuit no válido<br>";
                return false;
            }

            if (Session["OrigenBuscadorChofer"].ToString() != "ChoferTransportista")
            {
                if (String.IsNullOrEmpty(txtChoferAltaRapidaApellido.Text.Trim()))
                {
                    lblMensajeChoferAltaRapida.Text = "Debe completar el Apellido del chofer para Alta Rápida";
                    return false;
                }

                if (!String.IsNullOrEmpty(txtChoferAltaRapidaCamion.Text.Trim()))
                {
                    if (txtChoferAltaRapidaCamion.Text.Trim().Length > 6 || txtChoferAltaRapidaCamion.Text.Trim().Length < 6)
                    {
                        lblMensajeChoferAltaRapida.Text += "La patente del Camión debe tener los 6 caracteres, ej: AAA111<br>";
                    }
                    if (!Tools.ValidarPatente(txtChoferAltaRapidaCamion.Text.Trim().ToUpper()))
                    {
                        lblMensajeChoferAltaRapida.Text += "La patente del Camión tiene formato incorrecto, ej: AAA111<br>";
                    }
                }
                if (!String.IsNullOrEmpty(txtChoferAltaRapidaAcoplado.Text.Trim()))
                {
                    if (txtChoferAltaRapidaAcoplado.Text.Trim().Length > 6 || txtChoferAltaRapidaAcoplado.Text.Trim().Length < 6)
                    {
                        lblMensajeChoferAltaRapida.Text += "La patente del Acoplado debe tener los 6 caracteres, ej: BBB222<br>";
                    }

                    if (!Tools.ValidarPatente(txtChoferAltaRapidaAcoplado.Text.Trim().ToUpper()))
                    {
                        lblMensajeChoferAltaRapida.Text += "La patente del Acoplado tiene formato incorrecto, ej: AAA111<br>";
                    }

                }

                // chequeo si el Cuit del chofer ya existe.
                Chofer choferCheckeo = ChoferDAO.Instance.GetChoferByCuit(txtChoferAltaRapidaCuit.Text.Trim());
                if (choferCheckeo != null)
                {
                     // primero filtro si se trata de Transportista.
                    if (choferCheckeo.EsChoferTransportista.Equals(Enums.EsChoferTransportista.No))
                    {
                        lblMensajeChoferAltaRapida.Text = "El cuit del chofer ya existe a Nombre de: " + choferCheckeo.Apellido + ", " + choferCheckeo.Nombre;
                        return false;
                    
                    }
                }

            }
            else { 
            
                // Chequeo de cuit para choferes transportistas
                foreach (Chofer chof in ChoferDAO.Instance.GetAll())
                {
                    // primero filtro si se trata de Transportista.
                    if (chof.EsChoferTransportista.Equals(Enums.EsChoferTransportista.Si))
                    {
                        if (chof.Cuit.Equals(txtChoferAltaRapidaCuit.Text.Trim()))
                        {
                            lblMensajeChoferAltaRapida.Text = "El cuit del chofer transportista ya existe a Nombre de: " + chof.Nombre;
                            return false;
                        }
                    }

                }


            
            }

            return true;
        }
        protected void btnCargaRapidaCliente_Click(object sender, EventArgs e)
        {
            if (validacionClienteAltaRapida())
            {
                //doy de alta el chofer
                Cliente clientenuevo = new Cliente();
                clientenuevo.RazonSocial = txtClienteAltaRapidaNombre.Text.Trim();
                clientenuevo.NombreFantasia = txtClienteAltaRapidaNombre.Text.Trim();
                clientenuevo.Cuit = txtClienteAltaRapidaCuit.Text.Trim();                
                clientenuevo.EsProspecto = true;                
                clientenuevo.Activo = true;

                clientenuevo.TipoDocumento = TipoDocumentoSAPDAO.Instance.GetOne(80);
                clientenuevo.Calle = string.Empty;
                clientenuevo.ClaveGrupo = string.Empty;
                clientenuevo.Cp = string.Empty;
                clientenuevo.DescripcionGe = string.Empty;
                clientenuevo.Dto = string.Empty;
                clientenuevo.GrupoComercial = string.Empty;                
                clientenuevo.IdCliente =  ClienteDAO.Instance.getIdProspecto();
                clientenuevo.Numero = string.Empty;
                clientenuevo.Piso = string.Empty;
                clientenuevo.Poblacion = string.Empty;
                clientenuevo.Tratamiento = string.Empty;
                
                ClienteDAO.Instance.SaveOrUpdate(clientenuevo);

                CargarTitulosBuscadorCliente();
                DatosBuscadorCliente(clientenuevo.Cuit);


            }
        }
        protected void btnCargaRapidaProveedor_Click(object sender, EventArgs e)
        {
            if (validacionProveedorAltaRapida())
            {
                //doy de alta el chofer
                Proveedor proveedornuevo = new Proveedor();

                proveedornuevo.Sap_Id = ProveedorDAO.Instance.getIdSapProspecto().ToString();
                proveedornuevo.Nombre = txtProveedorAltaRapidaNombre.Text.Trim();
                proveedornuevo.TipoDocumento = TipoDocumentoSAPDAO.Instance.GetOne(80);
                proveedornuevo.NumeroDocumento = txtProveedorAltaRapidaCuit.Text.Trim();
                proveedornuevo.CP = string.Empty;
                proveedornuevo.Activo = true;
                proveedornuevo.Calle = string.Empty;
                proveedornuevo.Ciudad = string.Empty;
                proveedornuevo.Departamento = string.Empty;
                proveedornuevo.EsProspecto = true;
                proveedornuevo.Numero = string.Empty;
                proveedornuevo.Pais = string.Empty;
                proveedornuevo.Piso = string.Empty;

                ProveedorDAO.Instance.SaveOrUpdate(proveedornuevo);

                CargarTitulosBuscadorProveedor();
                DatosBuscadorProveedor(proveedornuevo.NumeroDocumento);

            }
        }
        private bool validacionClienteAltaRapida()
        {
            lblMensajeClienteAltaRapida.Text = string.Empty;

            if (String.IsNullOrEmpty(txtClienteAltaRapidaNombre.Text.Trim()))
            {
                lblMensajeClienteAltaRapida.Text = "Debe completar la Razon Social o Nombre de Fantasia del cliente prospecto para Alta Rápida";
                return false;
            }
            if (String.IsNullOrEmpty(txtClienteAltaRapidaCuit.Text.Trim()))
            {
                lblMensajeClienteAltaRapida.Text = "Debe completar el Cuit del cliente prospecto para Alta Rápida";
                return false;
            }
            if (!CuitValido(txtClienteAltaRapidaCuit.Text.Trim()))
            {
                lblMensajeClienteAltaRapida.Text = "Cuit no válido<br>";
                return false;
            }

            IList<Cliente> clientesCkeckeo = ClienteDAO.Instance.GetClienteByCuit(txtClienteAltaRapidaCuit.Text.Trim());
            foreach (Cliente c in clientesCkeckeo)
            {
                if (c.IdCliente > 0)
                {
                    lblMensajeClienteAltaRapida.Text += "<br/>El cuit del Cliente ya existe a Nombre de: " + c.NombreFantasia;
                    return false;                
                }
            }



            return true;
        
        }
        private bool validacionProveedorAltaRapida()
        {
            lblMensajeProveedorAltaRapida.Text = string.Empty;
            if (String.IsNullOrEmpty(txtProveedorAltaRapidaNombre.Text.Trim()))
            {
                lblMensajeProveedorAltaRapida.Text = "Debe completar Nombre del Proveedor prospecto para Alta Rápida";
                return false;
            }
            if (String.IsNullOrEmpty(txtProveedorAltaRapidaCuit.Text.Trim()))
            {
                lblMensajeProveedorAltaRapida.Text = "Debe completar el Cuit del Proveedor prospecto para Alta Rápida";
                return false;
            }
            if (!CuitValido(txtProveedorAltaRapidaCuit.Text.Trim()))
            {
                lblMensajeProveedorAltaRapida.Text = "Cuit no válido<br>";
                return false;
            }

            IList<Proveedor> ProveedoresCkeckeo = ProveedorDAO.Instance.GetProveedorByNumeroDocumento(txtProveedorAltaRapidaCuit.Text.Trim());
            foreach (Proveedor p in ProveedoresCkeckeo)
            {
                if (p.IdProveedor > 0)
                {
                    lblMensajeProveedorAltaRapida.Text += "<br/>El cuit del Proveedor ya existe a Nombre de: " + p.Nombre;
                    return false;
                }
            }

            return true;

        }
        private void CargarTitulosBuscadorChofer()
        {
            var row = new TableRow();
            row.CssClass = "TableRowTitle";
            row.Cells.Add(AddTitleCell("Apellido y Nombre", 200));
            row.Cells.Add(AddTitleCell("Cuit", 100));
            row.Cells.Add(AddTitleCell("Camion", 100));
            row.Cells.Add(AddTitleCell("Acoplado", 100));
            row.Cells.Add(AddTitleCell("", 10));

            tblBuscadorChofer.Rows.Add(row);

        }
        private void DatosBuscadorChofer(string busqueda,bool transportista)
        {
            foreach (Chofer dato in ChoferDAO.Instance.GetFiltro(busqueda))
            {
                var row = new TableRow();
                row.CssClass = "TableRow";

                row.Cells.Add(AddCell(dato.Nombre + " " + dato.Apellido, dato.Nombre + " " + dato.Apellido, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(dato.Cuit, dato.Cuit, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(dato.Camion, dato.Camion, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(dato.Acoplado, dato.Acoplado, HorizontalAlign.Justify));

                string link = "<a href='#' onClick='BuscadorManager(" + dato.IdChofer.ToString() + ",\"" + hbBuscador.Value + "\",\"" + dato.Nombre + " " + dato.Apellido + "\",\"" + dato.Cuit + "\")" +
                             "'><IMG border='0' src='../../Content/Images/icon_select.gif'></a>";

                row.Cells.Add(AddCell(link, string.Empty, HorizontalAlign.Center));

                if (transportista){
                    if(dato.EsChoferTransportista == Enums.EsChoferTransportista.Si)
                        tblBuscadorChofer.Rows.Add(row);
                }
                else {
                    if (dato.EsChoferTransportista == Enums.EsChoferTransportista.No)
                        tblBuscadorChofer.Rows.Add(row);
                }

                

            }
        }
        protected void ImageButtonBuscadorChofer_Click(object sender, ImageClickEventArgs e)
        {
            hbBuscador.Value = "Chofer";
            tblBuscadorChofer.Rows.Clear();
            txtBuscadorChofer.Text = string.Empty;
            Session["OrigenBuscadorChofer"] = "Chofer";

            lblAltaRapidaChoferesTitulo.Text = "Alta Rapida Chofer (no transportista)";
            txtChoferAltaRapidaApellido.Visible = true;
            txtChoferAltaRapidaCamion.Visible = true;
            txtChoferAltaRapidaAcoplado.Visible = true;

            lblMensajeChoferAltaRapida.Text = string.Empty;
            
            BuscadorChofer.Visible = true;
            ImageButtonDeleteChofer.Visible = true;

            txtChoferAltaRapidaNombre.Text = string.Empty;
            txtChoferAltaRapidaApellido.Text = string.Empty;
            txtChoferAltaRapidaCuit.Text = string.Empty;

            CargarChoferesUsados();
        }
        protected void ImageButtonProveedorTransportista_Click(object sender, ImageClickEventArgs e)
        {
            // Evaluo si lo que voy a abrir es un Proveedor o un Chofer Transportista
            bool seRequiereChofer = false;
            // SI el transportador del flete es Empresa, entonces seRequiereChofer = false, de lo contrario, seRequiereChofer = true;

            if (!String.IsNullOrEmpty(hbClientePagadorDelFlete.Value)) {
                Cliente cliTmp = ClienteDAO.Instance.GetOne(Convert.ToInt32(hbClientePagadorDelFlete.Value));

                if (!cliTmp.EsEmpresa())
                {
                    seRequiereChofer = true;                    
                }
            }

            if (seRequiereChofer)
            {
                hbBuscador.Value = "Chofer";
                tblBuscadorChofer.Rows.Clear();
                txtBuscadorChofer.Text = string.Empty;
                Session["OrigenBuscadorChofer"] = "ChoferTransportista";

                lblMensajeChoferAltaRapida.Text = string.Empty;
                lblAltaRapidaChoferesTitulo.Text = "Alta Rapida Chofer Transportista de Terceros";
                txtChoferAltaRapidaApellido.Visible = false;
                txtChoferAltaRapidaCamion.Visible = false;
                txtChoferAltaRapidaAcoplado.Visible = false;

                BuscadorChofer.Visible = true;
                ImageButtonDeleteChofer.Visible = true;

                txtChoferAltaRapidaNombre.Text = string.Empty;
                txtChoferAltaRapidaApellido.Text = string.Empty;
                txtChoferAltaRapidaCuit.Text = string.Empty;

                CargarChoferesUsados();

            }
            else 
            {
                hbBuscador.Value = "ProveedorTransportista";
                tblBuscadorProveedor.Rows.Clear();
                txtBuscadorProveedor.Text = string.Empty;

                lblMensajeProveedorAltaRapida.Text = string.Empty;

                BuscadorProveedor.Visible = true;
                ImageButtonDeleteTransportista.Visible = true;

                CargarProveedoresUsados();            
            }
            


        }
        //ImageButtonClientePagadorDelFlete_Click
        protected void ImageButtonClientePagadorDelFlete_Click(object sender, ImageClickEventArgs e)
        {
            hbBuscador.Value = "ClientePagadorDelFlete";
            tblBuscadorCliente.Rows.Clear();
            txtBuscadorCliente.Text = string.Empty;

            lblMensajeClienteAltaRapida.Text = string.Empty;

            BuscadorCliente.Visible = true;
            ImageButtonDeletePagadorFlete.Visible = true;
            CargarClientesUsados();

        }
        protected void cboIdEstablecimientoProcedencia_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cboIdEstablecimientoProcedencia.SelectedIndex > 0)
            {
                Establecimiento establecimiento = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(cboIdEstablecimientoProcedencia.SelectedValue));
                txtDireccionEstablecimientoProcedencia.Text = (establecimiento.Direccion != null) ? establecimiento.Direccion : string.Empty;
                txtLocalidadEstablecimientoProcedencia.Text = (establecimiento.Localidad != null) ? establecimiento.Localidad.Descripcion : string.Empty;
                txtProvinciaEstablecimientoProcedencia.Text = (establecimiento.Provincia != null) ? establecimiento.Provincia.Descripcion : string.Empty;
            }
            else
            {
                txtDireccionEstablecimientoProcedencia.Text = string.Empty;
                txtLocalidadEstablecimientoProcedencia.Text = string.Empty;
                txtProvinciaEstablecimientoProcedencia.Text = string.Empty;
            }

        }
        protected void cboIdEstablecimientoDestino_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboIdEstablecimientoDestino.SelectedIndex > 0)
            {
                Establecimiento establecimiento = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(cboIdEstablecimientoDestino.SelectedValue));
                txtDireccionEstablecimientoDestino.Text = (establecimiento.Direccion != null) ? establecimiento.Direccion : string.Empty;
                txtLocalidadEstablecimientoDestino.Text = (establecimiento.Localidad != null) ? establecimiento.Localidad.Descripcion : string.Empty;
                txtProvinciaEstablecimientoDestino.Text = (establecimiento.Provincia != null) ? establecimiento.Provincia.Descripcion : string.Empty;

                cboClienteDestino.Text = establecimiento.IdInterlocutorDestinatario.RazonSocial;
                txtCuitClienteDestino.Text = establecimiento.IdInterlocutorDestinatario.Cuit;
                hbClienteDestino.Value = establecimiento.IdInterlocutorDestinatario.IdCliente.ToString();

            }
            else
            {
                txtDireccionEstablecimientoDestino.Text = string.Empty;
                txtLocalidadEstablecimientoDestino.Text = string.Empty;
                txtProvinciaEstablecimientoDestino.Text = string.Empty;

                cboClienteDestino.Text = string.Empty;
                txtCuitClienteDestino.Text = string.Empty;
                hbClienteDestino.Value = string.Empty;

            }
        }
        private bool ValidacionesCambioDestino()
        {
            lblMensaje.Text = string.Empty;
            string mensaje = string.Empty;

            if (cboIdEstablecimientoDestinoCambio.SelectedIndex < 1)
            {
                mensaje += "Debe seleccionar un DESTINO en Cambio de domicilio de descarga / desvio<br>";
            }
            if (String.IsNullOrEmpty(cboClienteDestinatarioCambio.Text.Trim()))
            {
                mensaje += "Debe seleccionar un NUEVO DESTINATARIO en Cambio de domicilio de descarga / desvio<br>";
            }

            if (mensaje.Length > 0)
            {
                btnSoloGuardar.Enabled = true;
                lblMensaje.ForeColor = Color.Red;
                lblMensaje.Text = mensaje;
                return false;
            }

            lblMensaje.ForeColor = Color.Green;
            return true;

        }
        private bool Validaciones()
        {
            lblMensaje.Text = string.Empty;

            // Validacion de formatos.
            string mensaje = string.Empty;


            if (!isNumeric(txtKrgsEstimados.Text.Trim()))
            {
                mensaje += "La cantidad de Kgrs Estimados debe ser un Valor Numérico<br>";
            }

            if (!isNumeric(txtContratoNro.Text.Trim()))
            {
                mensaje += "El contrato debe ser un valor numerico<br>";
            }

            if (!isNumeric(txtPesoBruto.Text.Trim()))
            {
                mensaje += "El peso bruto debe ser un valor numerico<br>";
            }

            if (!isNumeric(txtPesoTara.Text.Trim()))
            {
                mensaje += "El peso tara debe ser un valor numerico<br>";
            }


            if (cboTipoDeCartam.SelectedIndex < 1)
            {
                mensaje += "Debe seleccionar un Tipo de Carta de Porte<br>";
            }
            if (String.IsNullOrEmpty(cboProveedorTitularCartaDePorte.Text.Trim()))
            {
                mensaje += "Debe seleccionar un Titular Carta De Porte.<br>";
            }

            if (String.IsNullOrEmpty(cboClienteDestinatario.Text.Trim()))
            {
                mensaje += "Debe seleccionar un Cliente Destinatario.<br>";
            }
            if (String.IsNullOrEmpty(cboClienteDestino.Text.Trim()))
            {
                mensaje += "Debe seleccionar un Cliente Destino.<br>";
            }

            if (cboGrano.SelectedIndex < 1)
            {
                mensaje += "Debe seleccionar un Grano.<br>";
            }


            bool ConformeCondicionalConOpcion = false;
            foreach (ListItem li in rblConformeCondicional.Items)
            {
                if (li.Selected)
                {
                    ConformeCondicionalConOpcion = true;
                }

            }

            if (!ConformeCondicionalConOpcion)
            {
                mensaje += "Debe seleccionar Conforme o Condicional.<br>";
            }


            bool rblFletePagadoAPagarConOpcion = false;
            foreach (ListItem li in rblFletePagadoAPagar.Items)
            {
                if (li.Selected)
                {
                    rblFletePagadoAPagarConOpcion = true;
                }

            }

            if (!rblFletePagadoAPagarConOpcion)
            {
                mensaje += "Debe seleccionar Estado del Pago en Flete.<br>";
            }

            if (cboIdEstablecimientoProcedencia.SelectedIndex < 1)
            {
                mensaje += "Debe seleccionar una procedencia de la mercadería<br>";
            }

            if (cboIdEstablecimientoDestino.SelectedIndex < 1)
            {
                mensaje += "Debe seleccionar un destino de la mercadería<br>";
            }


            if (String.IsNullOrEmpty(cboClienteRemitenteComercial.Text.Trim()))
            {
                if ((cboTipoDeCartam.SelectedItem.Text.Equals("Venta de granos de terceros") &&
                    (!String.IsNullOrEmpty(cboClienteDestino.Text.Trim()) && txtCuitClienteDestinatario != txtCuitProveedorTitularCartaDePorte) ||
                    (cboTipoDeCartam.SelectedItem.Text.Equals("Canje"))))
                {
                    mensaje += "Debe seleccionar un Remitente Comercial.<br>";
                }
            }

            if (rbCargaPesadaDestino.Checked)
            {
                if (String.IsNullOrEmpty(txtKrgsEstimados.Text.Trim()))
                {
                    mensaje += "Debe completar la cantidad de kilogramos estimados<br>";
                }
            }
            else
            {
                if (String.IsNullOrEmpty(txtPesoBruto.Text.Trim()))
                {
                    mensaje += "Debe completar la cantidad de Peso Bruto<br>";
                }

                if (String.IsNullOrEmpty(txtPesoTara.Text.Trim()))
                {
                    mensaje += "Debe completar la cantidad de Peso Tara<br>";
                }

            }


            if (String.IsNullOrEmpty(txtCtgManual.Text.Trim()))            
            {                    
                mensaje += "Debe completar El numero de CTG otorgado por AFIP<br>";
            }

            if (String.IsNullOrEmpty(txtFechaVencimiento.Text.Trim()))
            {
                mensaje += "Debe completar la fecha de Vencimiento enviada por AFIP<br>";
                
            }
            if (String.IsNullOrEmpty(txtNumeroCEEManual.Text.Trim()))
            {
                mensaje += "Debe completar El numero de CEE.<br>";
            }


            if (String.IsNullOrEmpty(cboClientePagadorDelFlete.Text.Trim()))
            {
                mensaje += "Debe seleccionar un Pagador del Flete.<br>";
            }
            



            if (String.IsNullOrEmpty(txtTarifaReferencia.Text.Trim()))            
            {       
                mensaje += "Debe completar la tarifa de referencia enviada por AFIP<br>";                
            }


            if (!String.IsNullOrEmpty(txtPatente.Text.Trim()))
            {
                if (txtPatente.Text.Trim().Length > 6 || txtPatente.Text.Trim().Length < 6)
                {
                    mensaje += "La patente del Camión debe tener los 6 caracteres, ej: AAA111<br>";
                }
                if (!Tools.ValidarPatente(txtPatente.Text.Trim().ToUpper()))
                {
                    mensaje += "La patente del Camión tiene formato incorrecto, ej: AAA111<br>";
                }            
            }
            if (!String.IsNullOrEmpty(txtAcoplado.Text.Trim()))
            {
                if (txtAcoplado.Text.Trim().Length > 6 || txtAcoplado.Text.Trim().Length < 6)
                {
                    mensaje += "La patente del Acoplado debe tener los 6 caracteres, ej: BBB222<br>";
                }

                if (!Tools.ValidarPatente(txtAcoplado.Text.Trim().ToUpper()))
                {
                    mensaje += "La patente del Acoplado tiene formato incorrecto, ej: AAA111<br>";
                }

            }


            if (mensaje.Length > 0)
            {
                btnSoloGuardar.Enabled = true;
                lblMensaje.ForeColor = Color.Red;
                lblMensaje.Text = mensaje;
                return false;
            }

            lblMensaje.ForeColor = Color.Green;
            return true;
        }
        private bool ValidarLogicaPorTipoCartaDePorte(Solicitud solicitud)
        {

            string mensaje = string.Empty;

            // Valido la carga del pagador de flete es obligatoria y de serlo ver si
            // es no es empresa para no obligarlo a que cargue los datos del transportista.

            if (solicitud.TipoDeCarta.Descripcion.Equals("Compra de granos") || 
                solicitud.TipoDeCarta.Descripcion.Equals("Venta de granos de terceros") || 
                solicitud.TipoDeCarta.Descripcion.Equals("Terceros por venta  de Granos de producción propia"))
            {
                if (String.IsNullOrEmpty(cboClientePagadorDelFlete.Text.Trim()))
                {
                    mensaje += "Debe seleccionar un Pagador del Flete.<br>";
                }
                else
                {

                    if (solicitud.ClientePagadorDelFlete.EsEmpresa())
                    {
                        if (!isNumeric(txtCantHoras.Text.Trim()))
                        {
                            mensaje += "La cantidad de horas debe ser un Valor Numérico<br>";
                        }
                        if (!isNumeric(txtKmRecorridos.Text.Trim()))
                        {
                            mensaje += "La cantidad de Kilometros debe ser un Valor Numérico<br>";
                        }

                        txtTarifa.Text = txtTarifa.Text.Trim().Replace(',','.').Replace("$","").Trim();
                        if (!isNumeric(txtTarifa.Text.Trim()))
                        {
                            mensaje += "La tarifa real debe ser un valor numerico<br>";
                        }
                        if (String.IsNullOrEmpty(cboProveedorTransportista.Text.Trim()))
                        {
                            mensaje += "Debe seleccionar un Transportista.<br>";
                        }
                        if (String.IsNullOrEmpty(cboChofer.Text.Trim()))
                        {
                            mensaje += "Debe seleccionar un Chofer.<br>";
                        }
                        if (String.IsNullOrEmpty(txtPatente.Text.Trim()))
                        {
                            mensaje = "Debe completar la patente del Camión<br>";
                            lblMensaje.Text += mensaje;
                        }
                        if (String.IsNullOrEmpty(txtAcoplado.Text.Trim()))
                        {
                            mensaje = "Debe completar la patente del Acoplado<br>";
                            lblMensaje.Text += mensaje;
                        }
                        if (txtPatente.Text.Trim().Length > 6)
                        {
                            mensaje = "La patente del Camión no puede superar los 6 caracteres, ej: AAA111<br>";
                            lblMensaje.Text += mensaje;
                        }
                        if (txtAcoplado.Text.Trim().Length > 6)
                        {
                            mensaje = "La patente del Acoplado no puede superar los 6 caracteres, ej: BBB222<br>";
                            lblMensaje.Text += mensaje;
                        }
                        if (!Tools.ValidarPatente(txtPatente.Text.Trim().ToUpper()))
                        {
                            mensaje = "La patente del Camión tiene formato incorrecto, ej: AAA111<br>";
                            lblMensaje.Text += mensaje;
                        }

                        if (!Tools.ValidarPatente(txtAcoplado.Text.Trim().ToUpper()))
                        {
                            mensaje = "La patente del Acoplado tiene formato incorrecto, ej: AAA111<br>";
                            lblMensaje.Text += mensaje;
                        }


                        if (String.IsNullOrEmpty(txtKmRecorridos.Text.Trim()))
                        {
                            mensaje += "Debe completar los Kilómetros a recorrer.<br>";
                        }

                        if (String.IsNullOrEmpty(txtCantHoras.Text.Trim()))
                        {
                            mensaje += "Debe completar la Cantidad de Horas.<br>";
                        }
                        if (String.IsNullOrEmpty(txtTarifaReferencia.Text.Trim()))
                        {
                            mensaje += "Debe completar la tarifa de referencia enviada por AFIP<br>";
                        }
                        if (String.IsNullOrEmpty(txtTarifa.Text.Trim()))
                        {
                            mensaje += "Debe completar la Tarifa Real.<br>";
                        }
                    }
                }

            }
            else {

                if (!isNumeric(txtCantHoras.Text.Trim()))
                {
                    mensaje += "La cantidad de horas debe ser un Valor Numérico<br>";
                }
                if (!isNumeric(txtKmRecorridos.Text.Trim()))
                {
                    mensaje += "La cantidad de Kilometros debe ser un Valor Numérico<br>";
                }
                if (!isNumeric(txtTarifa.Text.Trim().Replace(',', '.').Replace("$", "").Trim()))
                {
                    mensaje += "La tarifa real debe ser un valor numerico<br>";
                }
                if (String.IsNullOrEmpty(cboProveedorTransportista.Text.Trim()))
                {
                    mensaje += "Debe seleccionar un Transportista.<br>";
                }
                if (String.IsNullOrEmpty(cboChofer.Text.Trim()))
                {
                    mensaje += "Debe seleccionar un Chofer.<br>";
                }

                if (!String.IsNullOrEmpty(txtPatente.Text.Trim()))
                {
                    if (txtPatente.Text.Trim().Length > 6 || txtPatente.Text.Trim().Length < 6)
                    {
                        mensaje += "La patente del Camión debe tener los 6 caracteres, ej: AAA111<br>";
                    }
                    if (!Tools.ValidarPatente(txtPatente.Text.Trim().ToUpper()))
                    {
                        mensaje += "La patente del Camión tiene formato incorrecto, ej: AAA111<br>";
                    }
                }
                if (!String.IsNullOrEmpty(txtAcoplado.Text.Trim()))
                {
                    if (txtAcoplado.Text.Trim().Length > 6 || txtAcoplado.Text.Trim().Length < 6)
                    {
                        mensaje += "La patente del Acoplado debe tener los 6 caracteres, ej: BBB222<br>";
                    }

                    if (!Tools.ValidarPatente(txtAcoplado.Text.Trim().ToUpper()))
                    {
                        mensaje += "La patente del Acoplado tiene formato incorrecto, ej: AAA111<br>";
                    }

                }

                if (String.IsNullOrEmpty(txtKmRecorridos.Text.Trim()))
                {
                    mensaje += "Debe completar los Kilómetros a recorrer.<br>";
                }
                if (String.IsNullOrEmpty(txtCantHoras.Text.Trim()))
                {
                    mensaje += "Debe completar la Cantidad de Horas.<br>";
                }
                if (String.IsNullOrEmpty(txtTarifa.Text.Trim()))
                {
                    mensaje += "Debe completar la Tarifa Real.<br>";
                }

            
            
            }



            switch (solicitud.TipoDeCarta.Descripcion)
            {
                case "Venta de granos propios":
                    if (solicitud.ProveedorTitularCartaDePorte != null && (!String.IsNullOrEmpty(solicitud.ProveedorTitularCartaDePorte.Sap_Id)))
                    {
                        Empresa empresaTitular = EmpresaDAO.Instance.GetOneBySap_Id(solicitud.ProveedorTitularCartaDePorte.Sap_Id);
                        if (empresaTitular == null)
                        {
                            mensaje += "Para 'Venta de granos propios' el titular de la carta de porte debe ser una Empresa<br>";
                        }
                    }
                    break;
                case "Venta de granos de terceros":
                    if (solicitud.ClienteRemitenteComercial == null)
                    {
                        mensaje += "Para 'Venta de granos de terceros' debe seleccionar un Remitente Comercial de tipo Empresa<br>";
                    }
                    if (solicitud.ClienteRemitenteComercial != null)
                    {
                        if (!solicitud.ClienteRemitenteComercial.EsEmpresa())
                            mensaje += "Para 'Venta de granos de terceros' el Remitente Comercial debe ser una Empresa<br>";
                    }

                    break;
                case "Compra de granos que transportamos":
                    break;
                case "Compra de granos":
                    break;
                case "Traslado de granos":
                    break;
                case "Canje":
                    break;
                case "Terceros por venta  de Granos de producción propia":
                    if (solicitud.ClienteRemitenteComercial == null)
                    {
                        mensaje += "Para 'Terceros por venta  de Granos de producción propia' debe seleccionar un Remitente Comercial de tipo Empresa<br>";
                    }
                    if (solicitud.ClienteRemitenteComercial != null)
                    {
                        if (!solicitud.ClienteRemitenteComercial.EsEmpresa())
                            mensaje += "Para 'Terceros por venta  de Granos de producción propia' el Remitente Comercial debe ser una Empresa<br>";
                    }

                    break;
                default:
                    break;
            }


            if (mensaje.Length > 0)
            {
                btnSoloGuardar.Enabled = true;
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
                {
                    Convert.ToDecimal(valor);
                }

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
        private Enums.ConformeCondicional getEnumCCValue()
        {

            Enums.ConformeCondicional enumCC = Enums.ConformeCondicional.Condicional;
            foreach (ListItem li in rblConformeCondicional.Items)
            {
                if (li.Selected)
                {
                    if (li.Text == "Conforme")
                    {
                        enumCC = Enums.ConformeCondicional.Conforme;
                    }
                    else
                    {
                        enumCC = Enums.ConformeCondicional.Condicional;
                    }
                }
            }

            return enumCC;

        }
        private Enums.EstadoFlete getEnumEFValue()
        {

            Enums.EstadoFlete enumEF = Enums.EstadoFlete.FleteAPagar;
            foreach (ListItem li in rblFletePagadoAPagar.Items)
            {
                if (li.Selected)
                {
                    if (li.Text == "Flete a Pagar")
                    {
                        enumEF = Enums.EstadoFlete.FleteAPagar;
                    }
                    else
                    {
                        enumEF = Enums.EstadoFlete.FletePagado;
                    }

                }
            }

            return enumEF;

        }
        protected void ImageButtonDeleteTitularCartaPorte_Click(object sender, ImageClickEventArgs e)
        {
            cboProveedorTitularCartaDePorte.Text = string.Empty;
            txtCuitProveedorTitularCartaDePorte.Text = string.Empty;
            hbProveedorTitularCartaDePorte.Value = string.Empty;
            ImageButtonDeleteTitularCartaPorte.Visible = false;
        }
        protected void ImageButtonDeleteIntermediario_Click(object sender, ImageClickEventArgs e)
        {
            cboClienteIntermediario.Text = string.Empty;
            txtCuitClienteIntermediario.Text = string.Empty;
            hbClienteIntermediario.Value = string.Empty;
            ImageButtonDeleteIntermediario.Visible = false;
        }
        protected void ImageButtonDeleteRemitenteComercial_Click(object sender, ImageClickEventArgs e)
        {
            cboClienteRemitenteComercial.Text = string.Empty;
            txtCuitClienteRemitenteComercial.Text = string.Empty;
            hbClienteRemitenteComercial.Value = string.Empty;
            ImageButtonDeleteRemitenteComercial.Visible = false;
            tblRemitenteComercialComoCanjeador.Visible = false;
        }
        protected void ImageButtonDeleteCorredor_Click(object sender, ImageClickEventArgs e)
        {
            cboClienteCorredor.Text = string.Empty;
            txtCuitClienteCorredor.Text = string.Empty;
            hbClienteCorredor.Value = string.Empty;
            ImageButtonDeleteCorredor.Visible = false;
        }
        protected void ImageButtonDeleteRepresentanteEntregador_Click(object sender, ImageClickEventArgs e)
        {
            cboClienteEntregador.Text = string.Empty;
            txtCuitClienteEntregador.Text = string.Empty;
            hbClienteEntregador.Value = string.Empty;
            ImageButtonDeleteRepresentanteEntregador.Visible = false;
        }
        protected void ImageButtonDeleteDestinatario_Click(object sender, ImageClickEventArgs e)
        {
            cboClienteDestinatario.Text = string.Empty;
            txtCuitClienteDestinatario.Text = string.Empty;
            hbClienteDestinatario.Value = string.Empty;
            ImageButtonDeleteDestinatario.Visible = false;
        }
        protected void ImageButtonDeleteDestino_Click(object sender, ImageClickEventArgs e)
        {
            cboClienteDestino.Text = string.Empty;
            txtCuitClienteDestino.Text = string.Empty;
            hbClienteDestino.Value = string.Empty;
            ImageButtonDeleteDestino.Visible = false;
        }
        protected void ImageButtonDeleteTransportista_Click(object sender, ImageClickEventArgs e)
        {
            cboProveedorTransportista.Text = string.Empty;
            txtCuitProveedorTransportista.Text = string.Empty;
            hbProveedorTransportista.Value = string.Empty;
            ImageButtonDeleteTransportista.Visible = false;
        }
        protected void ImageButtonDeleteChofer_Click(object sender, ImageClickEventArgs e)
        {
            cboChofer.Text = string.Empty;
            txtCuitChofer.Text = string.Empty;
            hbChofer.Value = string.Empty;
            ImageButtonDeleteChofer.Visible = false;
        }
        protected void ImageButtonDeletePagadorFlete_Click(object sender, ImageClickEventArgs e)
        {
            cboClientePagadorDelFlete.Text = string.Empty;
            hbClientePagadorDelFlete.Value = string.Empty;
            ImageButtonDeletePagadorFlete.Visible = false;
        }
        protected void btnCerrarEmpresa_Click(object sender, EventArgs e)
        {
            BuscadorEmpresa.Visible = false;
            CloseHidden();
        }
        protected void btnCerrarCliente_Click(object sender, EventArgs e)
        {
            BuscadorCliente.Visible = false;
            CloseHidden();
        }
        protected void btnCerrarProveedor_Click(object sender, EventArgs e)
        {
            BuscadorProveedor.Visible = false;
            CloseHidden();
        }
        protected void btnCerrarChofer_Click(object sender, EventArgs e)
        {
            BuscadorChofer.Visible = false;
            CloseHidden();
        }
        private void CloseHidden()
        {

            switch (hbBuscador.Value)
            {
                case "ProveedorTitularCartaDePorte":
                    {
                        if (String.IsNullOrEmpty(hbProveedorTitularCartaDePorte.Value))
                        {
                            ImageButtonDeleteTitularCartaPorte.Visible = false;

                        }
                        break;
                    }
                case "ClienteRemitenteComercial":
                    {
                        if (String.IsNullOrEmpty(hbClienteRemitenteComercial.Value))
                        {
                            ImageButtonDeleteRemitenteComercial.Visible = false;
                        }
                        break;
                    }
                case "ClienteIntermediario":
                    {
                        if (String.IsNullOrEmpty(hbClienteIntermediario.Value))
                        {
                            ImageButtonDeleteIntermediario.Visible = false;
                        }
                        break;
                    }
                case "ClienteCorredor":
                    {
                        if (String.IsNullOrEmpty(hbClienteCorredor.Value))
                        {
                            ImageButtonDeleteCorredor.Visible = false;
                        }
                        break;
                    }
                case "ClienteEntregador":
                    {
                        if (String.IsNullOrEmpty(hbClienteEntregador.Value))
                        {
                            ImageButtonDeleteRepresentanteEntregador.Visible = false;
                        }
                        break;
                    }
                case "ClienteDestinatario":
                    {
                        if (String.IsNullOrEmpty(hbClienteDestinatario.Value))
                        {
                            ImageButtonDeleteDestinatario.Visible = false;
                        }
                        break;
                    }
                case "ClienteDestino":
                    {
                        if (String.IsNullOrEmpty(hbClienteDestino.Value))
                        {
                            ImageButtonDeleteDestino.Visible = false;
                        }
                        break;
                    }
                case "Chofer":
                    {
                        if (String.IsNullOrEmpty(hbChofer.Value))
                        {
                            ImageButtonDeleteChofer.Visible = false;
                        }
                        break;
                    }
                case "ProveedorTransportista":
                    {
                        if (String.IsNullOrEmpty(hbProveedorTransportista.Value))
                        {
                            ImageButtonDeleteTransportista.Visible = false;
                        }
                        break;
                    }
                default:
                    break;

            }

        }
        private void CargarEmpresasUsadas()
        {
            CargarTitulosBuscadorEmpresa();

            foreach (Empresa dato in EmpresaDAO.Instance.GetUsados(hbBuscador.Value))
            {
                var row = new TableRow();
                row.CssClass = "TableRow";

                row.Cells.Add(AddCell(dato.Descripcion, dato.Descripcion, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(dato.Cliente.Cuit, dato.Cliente.Cuit, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(dato.Cliente.IdCliente.ToString(), dato.Cliente.IdCliente.ToString(), HorizontalAlign.Justify));

                string link = "<a href='#' onClick='BuscadorManager(" + dato.IdEmpresa.ToString() + ",\"" + hbBuscador.Value + "\",\"" + dato.Descripcion + "\",\"" + dato.Cliente.Cuit + "\")" +
                             "'><IMG border='0' src='../../Content/Images/icon_select.gif'></a>";

                row.Cells.Add(AddCell(link, string.Empty, HorizontalAlign.Center));

                tblBuscadorEmpresa.Rows.Add(row);

            }

        }
        private void CargarClientesUsados()
        {
            CargarTitulosBuscadorCliente();

            foreach (Cliente dato in ClienteDAO.Instance.GetUsados(hbBuscador.Value))
            {
                var row = new TableRow();
                row.CssClass = "TableRow";

                row.Cells.Add(AddCell(dato.RazonSocial, dato.RazonSocial, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(dato.Cuit, dato.Cuit, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(dato.IdCliente.ToString(), dato.IdCliente.ToString(), HorizontalAlign.Justify));

                string link = "<a href='#' onClick='BuscadorManager(" + dato.IdCliente.ToString() + ",\"" + hbBuscador.Value + "\",\"" + dato.RazonSocial + "\",\"" + dato.Cuit + "\")" +
                             "'><IMG border='0' src='../../Content/Images/icon_select.gif'></a>";

                row.Cells.Add(AddCell(link, string.Empty, HorizontalAlign.Center));

                tblBuscadorCliente.Rows.Add(row);

            }


        }
        private void CargarProveedoresUsados()
        {
            CargarTitulosBuscadorProveedor();

            foreach (Proveedor dato in ProveedorDAO.Instance.GetUsados(hbBuscador.Value))
            {
                var row = new TableRow();
                row.CssClass = "TableRow";

                row.Cells.Add(AddCell(dato.Nombre, dato.Nombre, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(dato.NumeroDocumento, dato.NumeroDocumento, HorizontalAlign.Justify));

                string link = "<a href='#' onClick='BuscadorManager(" + dato.IdProveedor.ToString() + ",\"" + hbBuscador.Value + "\",\"" + dato.Nombre + "\",\"" + dato.NumeroDocumento + "\")" +
                             "'><IMG border='0' src='../../Content/Images/icon_select.gif'></a>";

                row.Cells.Add(AddCell(link, string.Empty, HorizontalAlign.Center));

                tblBuscadorProveedor.Rows.Add(row);

            }

        }
        private void CargarChoferesUsados()
        {
            CargarTitulosBuscadorChofer();
            foreach (Chofer dato in ChoferDAO.Instance.GetUsados(hbBuscador.Value))
            {
                var row = new TableRow();
                row.CssClass = "TableRow";

                row.Cells.Add(AddCell(dato.Nombre + " " + dato.Apellido, dato.Nombre + " " + dato.Apellido, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(dato.Cuit, dato.Cuit, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(dato.Camion, dato.Cuit, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(dato.Acoplado, dato.Cuit, HorizontalAlign.Justify));

                string link = "<a href='#' onClick='BuscadorManager(" + dato.IdChofer.ToString() + ",\"" + hbBuscador.Value + "\",\"" + dato.Nombre + " " + dato.Apellido + "\",\"" + dato.Cuit + "\")" +
                             "'><IMG border='0' src='../../Content/Images/icon_select.gif'></a>";

                row.Cells.Add(AddCell(link, string.Empty, HorizontalAlign.Center));

                if (Session["OrigenBuscadorChofer"].ToString() != "ChoferTransportista")
                {
                    if (dato.EsChoferTransportista == Enums.EsChoferTransportista.No)
                        tblBuscadorChofer.Rows.Add(row);
                }
                else { 
                    if(dato.EsChoferTransportista == Enums.EsChoferTransportista.Si)
                        tblBuscadorChofer.Rows.Add(row);                
                }

            }

        }
        protected void txtPesoTara_TextChanged(object sender, EventArgs e)
        {
            if (isNumericValidador(txtPesoTara.Text.Trim()))
            {
                if (isNumericValidador(txtPesoBruto.Text.Trim()))
                {
                    txtPesoNeto.Text = (Convert.ToDecimal(txtPesoBruto.Text.Trim()) - Convert.ToDecimal(txtPesoTara.Text.Trim())).ToString();
                }

            }


        }
        protected void txtPesoBruto_TextChanged(object sender, EventArgs e)
        {

            if (isNumericValidador(txtPesoBruto.Text.Trim()))
            {
                if (isNumericValidador(txtPesoTara.Text.Trim()))
                {
                    txtPesoNeto.Text = (Convert.ToDecimal(txtPesoBruto.Text.Trim()) - Convert.ToDecimal(txtPesoTara.Text.Trim())).ToString();
                }
            }

        }
        public bool CuitValido(string cuit)
        {
            if (cuit.Length != 11)
            {
                return false;
            }
            int[] mult = new[] { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2, 1 };
            char[] nums = cuit.ToCharArray();
            int total = 0;
            for (int i = 0; i < mult.Length; i++)
            {
                total += int.Parse(nums[i].ToString()) * mult[i];
            }

            var resto = total % 11;
            //return resto == 0 ? 0 : resto == 1 ? 9 : 11 - resto;

            return (resto == 0);
        }
        protected void rbCargaPesadaDestino_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCargaPesadaDestino.Checked)
            {
                txtKrgsEstimados.Text = string.Empty;
                txtKrgsEstimados.Enabled = true;
                txtPesoBruto.Text = string.Empty;
                txtPesoBruto.Enabled = false;
                txtPesoTara.Text = string.Empty;
                txtPesoTara.Enabled = false;

                txtPesoNeto.Text = string.Empty;
            }
            else
            {
                txtKrgsEstimados.Text = string.Empty;
                txtKrgsEstimados.Enabled = false;
                txtPesoBruto.Text = string.Empty;
                txtPesoBruto.Enabled = true;
                txtPesoTara.Text = string.Empty;
                txtPesoTara.Enabled = true;

                txtPesoNeto.Text = string.Empty;
            }

        }
        protected void btnSoloGuardar_Click(object sender, EventArgs e)
        {
            btnSoloGuardar.Enabled = false;

            if (Validaciones())
            {
                Solicitud solicitud = new Solicitud();

                if (Request["id"] != null)
                {
                    string idSolicitud = Request["id"];
                    solicitud = SolicitudDAO.Instance.GetOne(Convert.ToInt32(idSolicitud));
                }
                                
                solicitud.NumeroCartaDePorte = txtNumeroCDPManual.Text;                
                solicitud.Cee = txtNumeroCEEManual.Text;                
                solicitud.Ctg = txtCtgManual.Text;                
                if (String.IsNullOrEmpty(solicitud.Ctg))
                        solicitud.Ctg = "0";

                solicitud.CantHoras = (long)Convert.ToDecimal(txtCantHoras.Text.Trim());
                solicitud.CargaPesadaDestino = rbCargaPesadaDestino.Checked;

                if (tblRemitenteComercialComoCanjeador.Visible)
                    solicitud.RemitenteComercialComoCanjeador = chkRemitenteComercialComoCanjeador.Checked;

                solicitud.Chofer = ChoferDAO.Instance.GetOne(Convert.ToInt32(hbChofer.Value));
                if (!String.IsNullOrEmpty(hbClienteCorredor.Value))
                    solicitud.ClienteCorredor = ClienteDAO.Instance.GetOne(Convert.ToInt32(hbClienteCorredor.Value));
                else
                {
                    solicitud.ClienteCorredor = new Cliente();
                }

                if (!String.IsNullOrEmpty(hbClienteDestinatario.Value))
                    solicitud.ClienteDestinatario = ClienteDAO.Instance.GetOne(Convert.ToInt32(hbClienteDestinatario.Value));
                else
                {
                    solicitud.ClienteDestinatario = new Cliente();
                }
                if (!String.IsNullOrEmpty(hbClienteDestino.Value))
                    solicitud.ClienteDestino = ClienteDAO.Instance.GetOne(Convert.ToInt32(hbClienteDestino.Value));
                else
                {
                    solicitud.ClienteDestino = new Cliente();
                }

                if (!String.IsNullOrEmpty(hbClienteEntregador.Value))
                    solicitud.ClienteEntregador = ClienteDAO.Instance.GetOne(Convert.ToInt32(hbClienteEntregador.Value));
                else
                {
                    solicitud.ClienteEntregador = new Cliente();
                }
                if (!String.IsNullOrEmpty(hbClienteIntermediario.Value))
                    solicitud.ClienteIntermediario = ClienteDAO.Instance.GetOne(Convert.ToInt32(hbClienteIntermediario.Value));
                else
                {
                    solicitud.ClienteIntermediario = new Cliente();
                }
                if (!String.IsNullOrEmpty(hbClientePagadorDelFlete.Value))
                    solicitud.ClientePagadorDelFlete = ClienteDAO.Instance.GetOne(Convert.ToInt32(hbClientePagadorDelFlete.Value));
                else
                {
                    solicitud.ClientePagadorDelFlete = new Cliente();
                }

                solicitud.ConformeCondicional = getEnumCCValue();
                if (!String.IsNullOrEmpty(hbClienteRemitenteComercial.Value))
                    solicitud.ClienteRemitenteComercial = ClienteDAO.Instance.GetOne(Convert.ToInt32(hbClienteRemitenteComercial.Value));
                else
                {
                    solicitud.ClienteRemitenteComercial = new Cliente();
                }
                if (!String.IsNullOrEmpty(hbProveedorTitularCartaDePorte.Value))
                    solicitud.ProveedorTitularCartaDePorte = ProveedorDAO.Instance.GetOne(Convert.ToInt32(hbProveedorTitularCartaDePorte.Value));
                else
                {
                    solicitud.ProveedorTitularCartaDePorte = new Proveedor();
                }


                if (!String.IsNullOrEmpty(hbProveedorTransportista.Value))
                {
                    if (solicitud.ClientePagadorDelFlete != null && solicitud.ClientePagadorDelFlete.IdCliente > 0 && solicitud.ClientePagadorDelFlete.EsEmpresa())
                    {
                        solicitud.ProveedorTransportista = ProveedorDAO.Instance.GetOne(Convert.ToInt32(hbProveedorTransportista.Value));
                        solicitud.ChoferTransportista = new Chofer();
                    }
                    else
                    {
                        solicitud.ProveedorTransportista = new Proveedor();
                        solicitud.ChoferTransportista = ChoferDAO.Instance.GetOne(Convert.ToInt32(hbProveedorTransportista.Value));
                    }
                }
                else
                {
                    solicitud.ProveedorTransportista = new Proveedor();
                    solicitud.ChoferTransportista = new Chofer();
                }

                solicitud.EstadoFlete = getEnumEFValue();
                solicitud.FechaDeCarga = DateTime.Now;
                // Chequeo integridad de la fecha.
                solicitud.FechaDeEmision = DateTime.Now;
                if (txtFechaDeEmision.Enabled)
                {
                    solicitud.FechaDeEmision = ConvertirFechaEmision(Request.Form[txtFechaDeEmision.UniqueID]);
                }

                if (txtFechaVencimiento.Visible)
                {
                    solicitud.FechaDeVencimiento = ConvertirFechaEmision(txtFechaVencimiento.Text.Trim());
                }

                solicitud.Grano = GranoDAO.Instance.GetOne(Convert.ToInt32(cboGrano.SelectedValue));
                solicitud.IdEstablecimientoDestino = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(cboIdEstablecimientoDestino.SelectedValue));
                solicitud.IdEstablecimientoProcedencia = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(cboIdEstablecimientoProcedencia.SelectedValue));
                solicitud.KmRecorridos = (long)Convert.ToDecimal((txtKmRecorridos.Text.Trim()));
                solicitud.LoteDeMaterial = solicitud.Grano.SujetoALote;

                if (!String.IsNullOrEmpty(txtContratoNro.Text.Trim()))
                    solicitud.NumeroContrato = Convert.ToInt32(txtContratoNro.Text);

                solicitud.Observaciones = txtObsevaciones.Text.Trim();
                solicitud.PatenteAcoplado = txtAcoplado.Text.Trim();
                solicitud.PatenteCamion = txtPatente.Text.Trim();

                if (solicitud.CargaPesadaDestino)
                {
                    solicitud.KilogramosEstimados = (long)Convert.ToDecimal((String.IsNullOrEmpty(txtKrgsEstimados.Text.Trim())) ? "0" : txtKrgsEstimados.Text.Trim());
                }
                else
                {
                    solicitud.PesoBruto = (long)Convert.ToDecimal((txtPesoBruto.Text.Trim()));
                    solicitud.PesoTara = (long)Convert.ToDecimal((txtPesoTara.Text.Trim()));
                    solicitud.PesoNeto = solicitud.PesoBruto - solicitud.PesoTara;
                }

                solicitud.TarifaReal = Convert.ToDecimal((txtTarifa.Text.Trim().Replace(',', '.').Replace("$", "").Trim()));
                solicitud.TipoDeCarta = TipoDeCartaDAO.Instance.GetOne(Convert.ToInt32(cboTipoDeCartam.SelectedValue));
                solicitud.UsuarioCreacion = App.Usuario.Nombre;
                solicitud.UsuarioModificacion = App.Usuario.Nombre;

                solicitud.TarifaReferencia = Convert.ToDecimal(txtTarifaReferencia.Text.Trim().Replace(',', '.').Replace("$", "").Trim());
                
                solicitud.ObservacionAfip = "Solicitud Manual";                
                solicitud.EstadoEnAFIP = Enums.EstadoEnAFIP.CargaManual;            
                solicitud.EstadoEnSAP = Enums.EstadoEnvioSAP.NoEnviadaASap;
            

                // Validacion de Pagador de Flete, Aca pregunto si el pagador de flete es empresa entonces no hago nada,
                // pero si no lo es, tengo que asegurarme que el ProveedorTransportista sea un chofer y no un proveedor.
                if (solicitud.ClientePagadorDelFlete != null && solicitud.ClientePagadorDelFlete.IdCliente > 0)
                {
                    if (!solicitud.ClientePagadorDelFlete.EsEmpresa() && solicitud.ProveedorTransportista.IdProveedor > 0)
                    {
                        lblMensaje.Text += "El transportista debe ser un Chofer Transportista";
                        return;
                    }
                       
                }

                if (solicitud.Validar())
                {
                    //Grabo la solicitud    
                    int SolicitudID = SolicitudDAO.Instance.SaveOrUpdate(solicitud);
                    if (SolicitudID > 0)
                    {
                        Response.Redirect("Index.aspx?Id=" + solicitud.IdSolicitud.ToString());
                    }

                }

            }

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
        protected void cboIdEstablecimientoDestinoCambio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboIdEstablecimientoDestinoCambio.SelectedIndex > 0)
            {
                Establecimiento establecimiento = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(cboIdEstablecimientoDestinoCambio.SelectedValue));
                txtDireccionEstablecimientoDestinoCambio.Text = (establecimiento.Direccion != null) ? establecimiento.Direccion : string.Empty;
                txtLocalidadEstablecimientoDestinoCambio.Text = (establecimiento.Localidad != null) ? establecimiento.Localidad.Descripcion : string.Empty;
                txtCuitDestinoCambio.Text = (establecimiento.IdInterlocutorDestinatario != null) ? establecimiento.IdInterlocutorDestinatario.Cuit : string.Empty;
            }
            else
            {
                txtDireccionEstablecimientoDestinoCambio.Text = string.Empty;
                txtLocalidadEstablecimientoDestinoCambio.Text = string.Empty;
                txtCuitDestinoCambio.Text = string.Empty;
            }

        }
        protected void ImageButtonDeleteDestinatarioCambio_Click(object sender, ImageClickEventArgs e)
        {
            cboClienteDestinatarioCambio.Text = string.Empty;
            txtCuitClienteDestinatarioCambio.Text = string.Empty;
            hbClienteDestinatarioCambio.Value = string.Empty;
            ImageButtonDeleteDestinatarioCambio.Visible = false;
        }
        protected void ImageButtonClienteDestinatarioCambio_Click(object sender, ImageClickEventArgs e)
        {
            hbBuscador.Value = "ClienteDestinatarioCambio";
            tblBuscadorCliente.Rows.Clear();
            txtBuscadorCliente.Text = string.Empty;

            BuscadorCliente.Visible = true;
            ImageButtonDeleteDestinatarioCambio.Visible = true;
            CargarClientesUsados();

        }
        protected void btnGuardarCambio_Click(object sender, EventArgs e)
        {
            if (Request["id"] != null)
            {
                string idSolicitud = Request["id"];
                Solicitud sol = SolicitudDAO.Instance.GetOne(Convert.ToInt32(idSolicitud));

                if (ValidacionesCambioDestino())
                {
                    if (!String.IsNullOrEmpty(hbClienteDestinatarioCambio.Value))
                        sol.ClienteDestinatarioCambio = ClienteDAO.Instance.GetOne(Convert.ToInt32(hbClienteDestinatarioCambio.Value));
                    else
                    {
                        sol.ClienteDestinatarioCambio = new Cliente();
                    }

                    sol.IdEstablecimientoDestinoCambio = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(cboIdEstablecimientoDestinoCambio.SelectedValue));

                    int SolicitudID = SolicitudDAO.Instance.SaveOrUpdate(sol);
                    if (SolicitudID > 0)
                    {
                        //Hago el pedido de Cambio de destino a la afip
                        var ws = new wsAfip_v3();
                        var resul = ws.cambiarDestinoDestinatarioCTGRechazado(sol);

                        Solicitud solicitudGuardara = SolicitudDAO.Instance.GetOne(SolicitudID);
                        solicitudGuardara.EstadoEnAFIP = Enums.EstadoEnAFIP.Enviado;

                        if (resul.arrayErrores.Length > 0)
                        {
                            lblMensaje.ForeColor = Color.Red;
                            lblMensaje.Text = "ERRORES CAMBIO DE DESTINO en AFIP: <br>";
                            foreach (String errores in resul.arrayErrores)
                            {
                                lblMensaje.Text += errores + "<br>";
                            }
                            solicitudGuardara.EstadoEnAFIP = Enums.EstadoEnAFIP.SinProcesar;
                            SolicitudDAO.Instance.SaveOrUpdate(solicitudGuardara);
                            return;
                        }

                        if (resul.datosResponse != null)
                        {
                            lblMensaje.ForeColor = Color.Black;
                            if (!String.IsNullOrEmpty(resul.datosResponse.fechaHora))
                            {
                                solicitudGuardara.EstadoEnAFIP = Enums.EstadoEnAFIP.CambioDestino;
                                lblMensaje.Text = "Cambio de Destino realizado";
                            }

                        }

                        EnvioMailDAO.Instance.sendMail("<b>Guardar cambios</b> <br/><br/>" + lblMensaje.Text + "<br/>" + "Carta De Porte: " + solicitudGuardara.NumeroCartaDePorte + "<br/>" + "Usuario: " + solicitudGuardara.UsuarioCreacion);

                        solicitudGuardara.ObservacionAfip = lblMensaje.Text;
                        SolicitudDAO.Instance.SaveOrUpdate(solicitudGuardara);

                        // Envio Anulacion a SAP
                        // Cuando sap responde la anulacion envio el alta desde el Web Service
                        wsSAP wssap = new wsSAP();
                        wssap.PrefacturaSAP(solicitudGuardara, true, false);

                        Response.Redirect("Index.aspx?Id=" + solicitudGuardara.IdSolicitud.ToString());
                    }

                }

            }


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
        private bool ChequeoIntegridadPagadorVsTransportista() {

            bool resul = true;

            //hbClientePagadorDelFlete.Value
            //hbProveedorTransportista.Value

            if (!String.IsNullOrEmpty(hbClientePagadorDelFlete.Value))
            {
                Cliente cliTmp = ClienteDAO.Instance.GetOne(Convert.ToInt32(hbClientePagadorDelFlete.Value));
                if (cliTmp.EsEmpresa())
                {
                    if (!String.IsNullOrEmpty(hbProveedorTransportista.Value))
                    {
                        // Tengo Cliente pagador de flete y es una empresa.... Entonces el Transportista debe ser un proveedor
                        // de lo contrario no es valido y limpio todos los campos ProveedorTransportista
                        Proveedor provTMP = ProveedorDAO.Instance.GetOne(Convert.ToInt32(hbProveedorTransportista.Value));
                        if (provTMP == null || provTMP.IdProveedor == 0)
                            resul = false;
                        if (provTMP != null && !provTMP.Nombre.Equals(cboProveedorTransportista.Text))
                            resul = false;

                    }

                }
                else { 
                    // El pagado no es empresa, entonces debe ir un transportista chofer.
                    if (!String.IsNullOrEmpty(hbProveedorTransportista.Value))
                    {
                        Chofer tmpChofer = ChoferDAO.Instance.GetOne(Convert.ToInt32(hbProveedorTransportista.Value));
                        if (tmpChofer == null)
                            resul = false;
                        if (tmpChofer != null && !tmpChofer.Nombre.Equals(cboProveedorTransportista.Text))
                            resul = false;
                        if (tmpChofer != null && tmpChofer.EsChoferTransportista != Enums.EsChoferTransportista.Si)
                            resul = false;

                    }
                
                }
            }

            return  resul;
        
        }
      
    }
}

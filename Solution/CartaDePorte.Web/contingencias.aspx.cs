using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;

using CartaDePorte.Core.DAO;
using CartaDePorte.Core.Servicios;
using CartaDePorte.Core.Domain;
using CartaDePorte.Core.Utilidades;
using CartaDePorte.Core.Domain.Seguridad;
using CartaDePorte.Core;

namespace CartaDePorte.Web
{
    public partial class contingencias : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!(App.UsuarioTienePermisos("BaseDeDatos") || App.UsuarioTienePermisos("SeguimientoEstados")))
            {
                Response.Redirect("~/SinAutorizacion.aspx");
                return;
            }



            string fe = Request.Form[txtFechaDeEmision.UniqueID];
            string fv = Request.Form[txtFechaVencimiento.UniqueID];

            if (!IsPostBack)
            {
                CargarCombos();
                hbBuscador.Value = string.Empty;
                BuscadorEmpresa.Visible = false;
                BuscadorCliente.Visible = false;
                BuscadorProveedor.Visible = false;
                BuscadorChofer.Visible = false;

                txtFechaDeEmision.Text = DateTime.Now.ToString("dd/MM/yyyy");

                if (Request["id"] != null)
                {
                    string idSolicitud = Request["id"];
                    Solicitud sol = SolicitudDAO.Instance.GetOne(Convert.ToInt32(idSolicitud));

                    CargarSolicitud(sol, false);

                }
            }


            ValoresCargados();

            if(!App.UsuarioTienePermisos("BaseDeDatos"))
            {
                //Deshabilito todo lo que es solo para Perfil Base de datos                
                divAdminFull.Disabled = true;

                cboTipoDeCarta.Enabled = false;
                txtCtgManual.Enabled = false;
                txtNumeroCDPManual.Enabled = false;
                txtNumeroCEEManual.Enabled = false;
                cboEstadoEnAFIP.Enabled = false;


            }
           

        }


        private void CargarSolicitud(Solicitud sol, bool modelo)
        {


            if (sol.ObservacionAfip.Contains("Reserva"))
            {
                btnGuardar.Visible = false;
                txtCtgManual.Visible = true;
                lblFechaVencimiento.Visible = true;
                txtFechaVencimiento.Visible = true;

                if (sol.IdEstablecimientoProcedencia != null && sol.IdEstablecimientoProcedencia.IdEstablecimiento > 0)
                {
                    cboIdEstablecimientoProcedencia.SelectedValue = sol.IdEstablecimientoProcedencia.IdEstablecimiento.ToString();
                    txtDireccionEstablecimientoProcedencia.Text = sol.IdEstablecimientoProcedencia.Direccion;
                    txtLocalidadEstablecimientoProcedencia.Text = sol.IdEstablecimientoProcedencia.Localidad.Descripcion;
                    txtProvinciaEstablecimientoProcedencia.Text = sol.IdEstablecimientoProcedencia.Provincia.Descripcion;

                }

                cboIdEstablecimientoProcedencia.Enabled = false;

                txtFechaDeEmision.Enabled = false;
                txtFechaVencimiento.Enabled = false;
                txtTarifaReferencia.Enabled = true;
                txtNumeroCDPManual.Text = sol.NumeroCartaDePorte;
                txtNumeroCEEManual.Text = sol.Cee;
                txtFechaDeEmision.Text = sol.FechaCreacion.ToString("dd/MM/yyyy");
                lblMensaje.Text = sol.ObservacionAfip;
            }
            else
            {


                lblTituloSeccion6.Text = "Carta de Porte: " + sol.NumeroCartaDePorte.ToString();

                cboTipoDeCarta.SelectedValue = sol.TipoDeCarta.IdTipoDeCarta.ToString();
                txtCtgManual.Text = sol.Ctg;
                txtNumeroCDPManual.Text = sol.NumeroCartaDePorte;
                txtNumeroCEEManual.Text = sol.Cee;

                cboEstadoEnAFIP.SelectedValue = Convert.ToInt32(sol.EstadoEnAFIP).ToString();
                cboEstadoEnSAP.SelectedValue = Convert.ToInt32(sol.EstadoEnSAP).ToString();

                //cboEmpresaTitularCartaDePorte
                if (sol.ProveedorTitularCartaDePorte != null && sol.ProveedorTitularCartaDePorte.IdProveedor > 0)
                {
                    cboProveedorTitularCartaDePorte.Text = sol.ProveedorTitularCartaDePorte.Nombre;
                    txtCuitProveedorTitularCartaDePorte.Text = sol.ProveedorTitularCartaDePorte.NumeroDocumento;
                    hbProveedorTitularCartaDePorte.Value = sol.ProveedorTitularCartaDePorte.IdProveedor.ToString();
                    ImageButtonDeleteTitularCartaPorte.Visible = true;
                }

                //cboClienteIntermediario
                if (sol.ClienteIntermediario != null && sol.ClienteIntermediario.IdCliente > 0)
                {
                    cboClienteIntermediario.Text = sol.ClienteIntermediario.RazonSocial;
                    txtCuitClienteIntermediario.Text = sol.ClienteIntermediario.Cuit;
                    hbClienteIntermediario.Value = sol.ClienteIntermediario.IdCliente.ToString();
                    ImageButtonDeleteIntermediario.Visible = true;
                }

                //cboEmpresaRemitenteComercial
                if (sol.ClienteRemitenteComercial != null && sol.ClienteRemitenteComercial.IdCliente > 0)
                {
                    cboClienteRemitenteComercial.Text = sol.ClienteRemitenteComercial.RazonSocial;
                    txtCuitClienteRemitenteComercial.Text = sol.ClienteRemitenteComercial.Cuit;
                    hbClienteRemitenteComercial.Value = sol.ClienteRemitenteComercial.IdCliente.ToString();
                    ImageButtonDeleteRemitenteComercial.Visible = true;
                }

                //cboClienteCorredor
                if (sol.ClienteCorredor != null && sol.ClienteCorredor.IdCliente > 0)
                {
                    cboClienteCorredor.Text = sol.ClienteCorredor.RazonSocial;
                    txtCuitClienteCorredor.Text = sol.ClienteCorredor.Cuit;
                    hbClienteCorredor.Value = sol.ClienteCorredor.IdCliente.ToString();
                    ImageButtonDeleteCorredor.Visible = true;
                }
                //cboClienteEntregador
                if (sol.ClienteEntregador != null && sol.ClienteEntregador.IdCliente > 0)
                {
                    cboClienteEntregador.Text = sol.ClienteEntregador.RazonSocial;
                    txtCuitClienteEntregador.Text = sol.ClienteEntregador.Cuit;
                    hbClienteEntregador.Value = sol.ClienteEntregador.IdCliente.ToString();
                    ImageButtonDeleteRepresentanteEntregador.Visible = true;
                }
                //cboClienteDestinatario
                if (sol.ClienteDestinatario != null && sol.ClienteDestinatario.IdCliente > 0)
                {
                    cboClienteDestinatario.Text = sol.ClienteDestinatario.RazonSocial;
                    txtCuitClienteDestinatario.Text = sol.ClienteDestinatario.Cuit;
                    hbClienteDestinatario.Value = sol.ClienteDestinatario.IdCliente.ToString();
                    ImageButtonDeleteDestinatario.Visible = true;
                }
                //cboClienteDestino
                if (sol.ClienteDestino != null && sol.ClienteDestino.IdCliente > 0)
                {
                    cboClienteDestino.Text = sol.ClienteDestino.RazonSocial;
                    txtCuitClienteDestino.Text = sol.ClienteDestino.Cuit;
                    hbClienteDestino.Value = sol.ClienteDestino.IdCliente.ToString();
                    ImageButtonDeleteDestino.Visible = true;
                }
                //cboProveedorTransportista
                //Aca puede ser un ProveedorTransportista o un ChoferTransportista
                if (sol.ClientePagadorDelFlete != null)
                {
                    if (sol.ClientePagadorDelFlete.EsEmpresa())
                    {
                        // SI es empresa entonces voy a buscar un Proveedor
                        if (sol.ProveedorTransportista != null && sol.ProveedorTransportista.IdProveedor > 0)
                        {
                            cboProveedorTransportista.Text = sol.ProveedorTransportista.Nombre;
                            txtCuitProveedorTransportista.Text = sol.ProveedorTransportista.NumeroDocumento;
                            hbProveedorTransportista.Value = sol.ProveedorTransportista.IdProveedor.ToString();
                            ImageButtonDeleteTransportista.Visible = true;
                        }

                    }
                    else
                    {
                        // Al no ser empresa el transportista en caso de haber tiene que ser un chofer transportista
                        if (sol.ChoferTransportista != null && sol.ChoferTransportista.IdChofer > 0)
                        {
                            cboProveedorTransportista.Text = sol.ChoferTransportista.Nombre;
                            txtCuitProveedorTransportista.Text = sol.ChoferTransportista.Cuit;
                            hbProveedorTransportista.Value = sol.ChoferTransportista.IdChofer.ToString();
                            ImageButtonDeleteTransportista.Visible = true;
                        }
                    }
                }



                if (sol.Chofer != null)
                {
                    cboChofer.Text = sol.Chofer.Apellido + ", " + sol.Chofer.Nombre;
                    txtCuitChofer.Text = sol.Chofer.Cuit;
                    hbChofer.Value = sol.Chofer.IdChofer.ToString();
                    ImageButtonDeleteChofer.Visible = true;
                }

                //txtFechaDeEmision            
                txtFechaDeEmision.Text = sol.FechaDeEmision.Value.ToString("dd/MM/yyyy");
                txtFechaVencimiento.Text = (sol.FechaDeVencimiento.HasValue) ? sol.FechaDeVencimiento.Value.ToString("dd/MM/yyyy") : string.Empty;
                //cboGrano            
                cboGrano.SelectedValue = sol.Grano.IdGrano.ToString();
                txtTipoGrano.Text = (sol.Grano.TipoGrano != null) ? sol.Grano.TipoGrano.Descripcion : string.Empty;
                txtCosecha.Text = (sol.Grano.CosechaAfip != null) ? sol.Grano.CosechaAfip.Descripcion : string.Empty;


                //rbCargaPesadaDestino            
                rbCargaPesadaDestino.Checked = sol.CargaPesadaDestino;
                //rbDeclaracionCalidad            
                rbDeclaracionCalidad.Checked = false;

                if (sol.CargaPesadaDestino)
                {
                    txtKrgsEstimados.Enabled = true;
                    txtPesoBruto.Enabled = false;
                    txtPesoTara.Enabled = false;

                }
                else
                {
                    txtKrgsEstimados.Enabled = false;
                    txtPesoBruto.Enabled = true;
                    txtPesoTara.Enabled = true;
                }

                //rblConformeCondicional

                foreach (ListItem li in rblConformeCondicional.Items)
                {
                    if (li.Text == "Conforme")
                    {
                        if ((sol.ConformeCondicional.Value == Enums.ConformeCondicional.Conforme))
                        {
                            li.Selected = true;
                        }
                    }

                    if (li.Text == "Condicional")
                    {
                        if ((sol.ConformeCondicional.Value == Enums.ConformeCondicional.Condicional))
                        {
                            li.Selected = true;
                        }
                    }
                }


                //rblFletePagadoAPagar 

                foreach (ListItem li in rblFletePagadoAPagar.Items)
                {
                    if (li.Text == "Flete a Pagar")
                    {
                        if ((sol.EstadoFlete.Value == Enums.EstadoFlete.FleteAPagar))
                        {
                            li.Selected = true;
                        }
                    }

                    if (li.Text == "Flete Pagado")
                    {
                        if ((sol.EstadoFlete.Value == Enums.EstadoFlete.FletePagado))
                        {
                            li.Selected = true;
                        }
                    }
                }

                //txtPesoBruto
                txtPesoBruto.Text = sol.PesoBruto.ToString();
                //txtPesoTara
                txtPesoTara.Text = sol.PesoTara.ToString();
                //txtPesoNeto
                txtPesoNeto.Text = (sol.PesoBruto - sol.PesoTara).ToString();
                //txtContratoNro
                txtContratoNro.Text = sol.NumeroContrato.ToString();
                //txtObsevaciones
                txtObsevaciones.Text = sol.Observaciones;
                //cboIdEstablecimientoProcedencia
                cboIdEstablecimientoProcedencia.SelectedValue = sol.IdEstablecimientoProcedencia.IdEstablecimiento.ToString();
                txtDireccionEstablecimientoProcedencia.Text = sol.IdEstablecimientoProcedencia.Direccion;
                txtLocalidadEstablecimientoProcedencia.Text = sol.IdEstablecimientoProcedencia.Localidad.Descripcion;
                txtProvinciaEstablecimientoProcedencia.Text = sol.IdEstablecimientoProcedencia.Provincia.Descripcion;

                //cboIdEstablecimientoDestino
                cboIdEstablecimientoDestino.SelectedValue = sol.IdEstablecimientoDestino.IdEstablecimiento.ToString();
                txtDireccionEstablecimientoDestino.Text = sol.IdEstablecimientoDestino.Direccion;
                txtLocalidadEstablecimientoDestino.Text = sol.IdEstablecimientoDestino.Localidad.Descripcion;
                txtProvinciaEstablecimientoDestino.Text = sol.IdEstablecimientoDestino.Provincia.Descripcion;

                //txtKrgsEstimados
                txtKrgsEstimados.Text = (sol.KilogramosEstimados == 0) ? string.Empty : sol.KilogramosEstimados.ToString();
                //txtPatente
                txtPatente.Text = sol.PatenteCamion;
                //txtAcoplado
                txtAcoplado.Text = sol.PatenteAcoplado;
                //txtKmRecorridos
                txtKmRecorridos.Text = sol.KmRecorridos.ToString();
                //txtTarifaReferencia
                txtTarifaReferencia.Text = sol.TarifaReferencia.ToString();
                //txtTarifa
                txtTarifa.Text = sol.TarifaReal.ToString();

                //cboClientePagadorDelFlete hbClientePagadorDelFlete
                if (sol.ClientePagadorDelFlete != null && sol.ClientePagadorDelFlete.IdCliente > 0)
                {
                    cboClientePagadorDelFlete.Text = sol.ClientePagadorDelFlete.RazonSocial;
                    hbClientePagadorDelFlete.Value = sol.ClientePagadorDelFlete.IdCliente.ToString();
                    ImageButtonDeletePagadorFlete.Visible = true;
                }


                txtCantHoras.Text = sol.CantHoras.ToString();
                lblMensaje.Text = sol.ObservacionAfip;

                if (!modelo && !String.IsNullOrEmpty(sol.Ctg) && sol.EstadoEnAFIP == Enums.EstadoEnAFIP.Anulada)
                {
                    lblMensaje.Text = sol.ObservacionAfip + " - Codigo de Anulación AFIP: " + sol.CodigoAnulacionAfip;
                }


                if (sol.IdEstablecimientoDestinoCambio != null && sol.IdEstablecimientoDestinoCambio.IdEstablecimiento > 0)
                {
                    cboIdEstablecimientoDestinoCambio.SelectedValue = sol.IdEstablecimientoDestinoCambio.IdEstablecimiento.ToString();
                    txtDireccionEstablecimientoDestinoCambio.Text = sol.IdEstablecimientoDestinoCambio.Direccion;
                    txtLocalidadEstablecimientoDestinoCambio.Text = sol.IdEstablecimientoDestinoCambio.Localidad.Descripcion;
                    txtCuitDestinoCambio.Text = sol.IdEstablecimientoDestinoCambio.IdInterlocutorDestinatario.Cuit;

                    tableCambioDestino.Visible = true;

                }
                //cboClienteDestinatario
                if (sol.ClienteDestinatarioCambio != null && sol.ClienteDestinatarioCambio.IdCliente > 0)
                {
                    cboClienteDestinatarioCambio.Text = sol.ClienteDestinatarioCambio.RazonSocial;
                    txtCuitClienteDestinatarioCambio.Text = sol.ClienteDestinatarioCambio.Cuit;
                    hbClienteDestinatarioCambio.Value = sol.ClienteDestinatarioCambio.IdCliente.ToString();
                    ImageButtonDeleteDestinatario.Visible = false;

                    tableCambioDestino.Visible = true;
                }


            }


            if (String.IsNullOrEmpty(txtTarifaReferencia.Text.Trim()))
                txtTarifaReferencia.Enabled = true;
            else
                txtTarifaReferencia.Enabled = false;

            if (String.IsNullOrEmpty(txtTarifa.Text.Trim()))
                txtTarifa.Enabled = true;
            else
                txtTarifa.Enabled = false;                


            txtCtgManual.Visible = true;
            txtNumeroCDPManual.Visible = true;
            txtNumeroCEEManual.Visible = true;
            lblFechaVencimiento.Visible = true;
            txtFechaVencimiento.Visible = true;
            txtFechaDeEmision.Enabled = false;
            txtFechaVencimiento.Enabled = false;

            btnGuardar.Visible = true;
            btnGuardar.Enabled = true;


            

        }

        private void ValoresCargados()
        {

            if (Request["__EVENTTARGET"] == "guardar")
            {
                btnGuardar_Click(null, null);
            }
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
                    else
                    {
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
            cboTipoDeCarta.Items.Add(li);

            foreach (TipoDeCarta tcp in TipoDeCartaDAO.Instance.GetAll())
            {
                li = new ListItem();
                li.Value = tcp.IdTipoDeCarta.ToString();
                li.Text = tcp.Descripcion;

                cboTipoDeCarta.Items.Add(li);
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

            BuscadorProveedor.Visible = true;
            ImageButtonDeleteTitularCartaPorte.Visible = true;

            CargarProveedoresUsados();


        }

        protected void ImageButtonClienteRemitenteComercial_Click(object sender, ImageClickEventArgs e)
        {
            hbBuscador.Value = "ClienteRemitenteComercial";
            tblBuscadorEmpresa.Rows.Clear();
            txtBuscadorEmpresa.Text = string.Empty;

            BuscadorCliente.Visible = true;
            ImageButtonDeleteRemitenteComercial.Visible = true;
            CargarClientesUsados();

        }


        protected void ImageButtonClienteIntermediario_Click(object sender, ImageClickEventArgs e)
        {
            hbBuscador.Value = "ClienteIntermediario";
            tblBuscadorCliente.Rows.Clear();
            txtBuscadorCliente.Text = string.Empty;

            BuscadorCliente.Visible = true;
            ImageButtonDeleteIntermediario.Visible = true;
            CargarClientesUsados();

        }

        protected void ImageButtonClienteCorredor_Click(object sender, ImageClickEventArgs e)
        {
            hbBuscador.Value = "ClienteCorredor";
            tblBuscadorCliente.Rows.Clear();
            txtBuscadorCliente.Text = string.Empty;

            BuscadorCliente.Visible = true;
            ImageButtonDeleteCorredor.Visible = true;
            CargarClientesUsados();
        }


        // Proveedor.
        protected void btnBuscadorProveedor_Click(object sender, EventArgs e)
        {
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

            BuscadorCliente.Visible = true;
            ImageButtonDeleteDestino.Visible = true;
            CargarClientesUsados();

        }

        //Chofer
        protected void btnBuscadorChofer_Click(object sender, EventArgs e)
        {
            lblMensajeChoferAltaRapida.Text = string.Empty;
            CargarTitulosBuscadorChofer();
            DatosBuscadorChofer(txtBuscadorChofer.Text.Trim(),false);

        }

        protected void btnCargaRapidaChofer_Click(object sender, EventArgs e)
        {
            if (validacionChoferAltaRapida())
            {
                //doy de alta el chofer
                Chofer chofernuevo = new Chofer();
                chofernuevo.Nombre = txtChoferAltaRapidaNombre.Text.Trim();
                chofernuevo.Apellido = txtChoferAltaRapidaApellido.Text.Trim();
                chofernuevo.Cuit = txtChoferAltaRapidaCuit.Text.Trim();
                chofernuevo.Camion = txtChoferAltaRapidaCamion.Text.Trim();
                chofernuevo.Acoplado = txtChoferAltaRapidaAcoplado.Text.Trim();
                chofernuevo.UsuarioCreacion = App.Usuario.Nombre;

                int IdChoferNuevo = ChoferDAO.Instance.SaveOrUpdate(chofernuevo);
                if (IdChoferNuevo > 0)
                {
                    chofernuevo = ChoferDAO.Instance.GetOne(IdChoferNuevo);
                    cboChofer.Text = chofernuevo.Apellido + ", " + chofernuevo.Nombre;
                    txtCuitChofer.Text = chofernuevo.Cuit;
                    hbChofer.Value = chofernuevo.IdChofer.ToString();
                    //BuscadorChofer.Visible = false;

                    /*
                    string botonEnviarChoferAltaRapida = "<a href='#' onClick='BuscadorManager(" + chofernuevo.IdChofer.ToString() + ",\"" + "Chofer" + "\",\"" + chofernuevo.Apellido + " " + chofernuevo.Nombre + "\",\"" + chofernuevo.Cuit + "\")" +
             "'><IMG border='0' src='../../Content/Images/icon_select.gif'></a>";
                    */

                    CargarTitulosBuscadorChofer();
                    DatosBuscadorChofer(chofernuevo.Cuit,false);

                }


            }
        }

        private bool validacionChoferAltaRapida()
        {
            if (String.IsNullOrEmpty(txtChoferAltaRapidaNombre.Text.Trim()))
            {
                lblMensajeChoferAltaRapida.Text = "Debe completar el Nombre del chofer para un Alta Rapida";
                return false;
            }
            if (String.IsNullOrEmpty(txtChoferAltaRapidaApellido.Text.Trim()))
            {
                lblMensajeChoferAltaRapida.Text = "Debe completar el Apellido del chofer para un Alta Rapida";
                return false;
            }
            if (String.IsNullOrEmpty(txtChoferAltaRapidaCuit.Text.Trim()))
            {
                lblMensajeChoferAltaRapida.Text = "Debe completar el Cuit del chofer para un Alta Rapida";
                return false;
            }
            if (!CuitValido(txtChoferAltaRapidaCuit.Text.Trim()))
            {
                lblMensajeChoferAltaRapida.Text = "Cuit no válido<br>";
                return false;
            }

            if (String.IsNullOrEmpty(txtChoferAltaRapidaCamion.Text.Trim()))
            {
                lblMensajeChoferAltaRapida.Text = "Debe completar la patante del camion del chofer para un Alta Rapida";
                return false;
            }
            if (String.IsNullOrEmpty(txtChoferAltaRapidaAcoplado.Text.Trim()))
            {
                lblMensajeChoferAltaRapida.Text = "Debe completar la patante del Acoplado del chofer para un Alta Rapida";
                return false;
            }

            // chequeo si el Cuit del chofer ya existe.
            Chofer choferCheckeo = ChoferDAO.Instance.GetChoferByCuit(txtChoferAltaRapidaCuit.Text.Trim());
            if (choferCheckeo != null)
            {
                lblMensajeChoferAltaRapida.Text = "El cuit del chofer ya existe a Nombre de: " + choferCheckeo.Apellido + ", " + choferCheckeo.Nombre;
                return false;
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

        private void DatosBuscadorChofer(string busqueda, bool transportista)
        {
            foreach (Chofer dato in ChoferDAO.Instance.GetFiltro(busqueda))
            {
                var row = new TableRow();
                row.CssClass = "TableRow";

                row.Cells.Add(AddCell(dato.Apellido + ", " + dato.Nombre, dato.Apellido + ", " + dato.Nombre, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(dato.Cuit, dato.Cuit, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(dato.Camion, dato.Camion, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(dato.Acoplado, dato.Acoplado, HorizontalAlign.Justify));

                string link = "<a href='#' onClick='BuscadorManager(" + dato.IdChofer.ToString() + ",\"" + hbBuscador.Value + "\",\"" + dato.Nombre + " " + dato.Apellido + "\",\"" + dato.Cuit + "\")" +
                             "'><IMG border='0' src='../../Content/Images/icon_select.gif'></a>";

                row.Cells.Add(AddCell(link, string.Empty, HorizontalAlign.Center));

                if (transportista)
                {
                    if (dato.EsChoferTransportista == Enums.EsChoferTransportista.Si)
                        tblBuscadorChofer.Rows.Add(row);
                }
                else
                {
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

            if (!String.IsNullOrEmpty(hbClientePagadorDelFlete.Value))
            {
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


        protected void btnGuardar_Click(object sender, EventArgs e)
        {

            btnGuardar.Enabled = false;

            string fe = Request.Form[txtFechaDeEmision.UniqueID];
            string fv = Request.Form[txtFechaVencimiento.UniqueID];


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

                solicitud.EstadoEnSAP = (Enums.EstadoEnvioSAP)Convert.ToInt32(cboEstadoEnSAP.SelectedValue);
                solicitud.EstadoEnAFIP = (Enums.EstadoEnAFIP)Convert.ToInt32(cboEstadoEnAFIP.SelectedValue);

                if (!String.IsNullOrEmpty(txtCantHoras.Text.Trim()))
                    solicitud.CantHoras = (long)Convert.ToDecimal(txtCantHoras.Text.Trim());

                solicitud.CargaPesadaDestino = rbCargaPesadaDestino.Checked;

                if (!String.IsNullOrEmpty(hbChofer.Value))
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
                    solicitud.ProveedorTransportista = ProveedorDAO.Instance.GetOne(Convert.ToInt32(hbProveedorTransportista.Value));
                    solicitud.ChoferTransportista = ChoferDAO.Instance.GetOne(Convert.ToInt32(hbProveedorTransportista.Value));
                }
                else
                {
                    solicitud.ProveedorTransportista = new Proveedor();
                }

                solicitud.EstadoFlete = getEnumEFValue();
                solicitud.Grano = GranoDAO.Instance.GetOne(Convert.ToInt32(cboGrano.SelectedValue));
                solicitud.IdEstablecimientoDestino = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(cboIdEstablecimientoDestino.SelectedValue));
                solicitud.IdEstablecimientoProcedencia = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(cboIdEstablecimientoProcedencia.SelectedValue));

                if (!String.IsNullOrEmpty(txtKmRecorridos.Text.Trim()))
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

                if (!String.IsNullOrEmpty(fv))                        
                    solicitud.FechaDeVencimiento = StringToDateTime(fv);

                if (!String.IsNullOrEmpty(fe))
                    solicitud.FechaDeEmision = StringToDateTime(fe);

                if (!String.IsNullOrEmpty(txtTarifaReferencia.Text) && txtTarifaReferencia.Enabled)
                    solicitud.TarifaReferencia = Convert.ToDecimal(txtTarifaReferencia.Text.Trim().Replace(',', '.'));

                if (!String.IsNullOrEmpty(txtTarifa.Text) && txtTarifa.Enabled)
                    solicitud.TarifaReal = Convert.ToDecimal((txtTarifa.Text.Trim().Replace(',', '.').Replace("$","").Trim()));

                solicitud.TipoDeCarta = TipoDeCartaDAO.Instance.GetOne(Convert.ToInt32(cboTipoDeCarta.SelectedValue));

                //Grabo la solicitud              
                SolicitudDAO.Instance.SaveOrUpdate(solicitud);
                //Response.Redirect("contingencias.aspx?id=" + solicitud.IdSolicitud.ToString());
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
                btnGuardar.Enabled = false;
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


            //
            if (!isNumeric(txtContratoNro.Text.Trim()))
            {
                mensaje += "El contrato debe ser un valor numerico<br>";
            }

            //txtPesoBruto
            if (!isNumeric(txtPesoBruto.Text.Trim()))
            {
                mensaje += "El peso bruto debe ser un valor numerico<br>";
            }

            if (!isNumeric(txtPesoTara.Text.Trim()))
            {
                mensaje += "El peso tara debe ser un valor numerico<br>";
            }


            if (cboTipoDeCarta.SelectedIndex < 1)
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
                if ((cboTipoDeCarta.SelectedItem.Text.Equals("Venta de granos de terceros") &&
                    (!String.IsNullOrEmpty(cboClienteDestino.Text.Trim()) && txtCuitClienteDestinatario != txtCuitProveedorTitularCartaDePorte) ||
                    (cboTipoDeCarta.SelectedItem.Text.Equals("Canje"))))
                {
                    mensaje += "Debe seleccionar un Remitente Comercial.<br>";
                }
            }
            /*
            if (!NumeroContrato.HasValue && !SinContrato)
            {
                throw ExceptionFactory.CreateBusiness("Debe completar Numero de Contrato.");
            }

            */

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

            if (txtCtgManual.Visible)
            {
                if (String.IsNullOrEmpty(txtCtgManual.Text.Trim()))
                {
                    //mensaje += "Debe completar El numero de CTG otorgado por AFIP<br>";
                }

                if (String.IsNullOrEmpty(txtFechaVencimiento.Text.Trim()))
                {
                    //mensaje += "Debe completar la fecha de Vencimiento enviada por AFIP<br>";
                }

            }


            if (cboTipoDeCarta.SelectedItem.Text.Equals("Venta de granos de terceros") || 
                cboTipoDeCarta.SelectedItem.Text.Equals("Terceros por venta  de Granos de producción propia"))
            {
                if (String.IsNullOrEmpty(txtNumeroCDPManual.Text.Trim()))
                {
                    mensaje += "Debe completar El numero de Carta de porte.<br>";
                }
                if (String.IsNullOrEmpty(txtNumeroCEEManual.Text.Trim()))
                {
                    //mensaje += "Debe completar El numero de CEE.<br>";
                }
            }

            if (cboTipoDeCarta.SelectedItem.Text.Equals("Venta de granos propios") ||
                cboTipoDeCarta.SelectedItem.Text.Equals("Traslado de granos"))
            {
                if (String.IsNullOrEmpty(cboClientePagadorDelFlete.Text.Trim()))
                {
                    mensaje += "Debe seleccionar un Pagador del Flete.<br>";
                }
            }


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
                            mensaje += "Debe completar la Patente del Camion.<br>";
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
                if (String.IsNullOrEmpty(txtPatente.Text.Trim()))
                {
                    mensaje += "Debe completar la Patente del Camion.<br>";
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

                row.Cells.Add(AddCell(dato.Apellido + ", " + dato.Nombre, dato.Apellido + ", " + dato.Nombre, HorizontalAlign.Justify));
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
                else
                {
                    if (dato.EsChoferTransportista == Enums.EsChoferTransportista.Si)
                        tblBuscadorChofer.Rows.Add(row);
                }

            }
        }


        protected void btnAnular_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtNumeroCDPManual.Text) && !String.IsNullOrEmpty(txtCtgManual.Text))
            {
                var ws = new wsAfip_v3();
                var resul = ws.anularCTG((long)Convert.ToDecimal(txtNumeroCDPManual.Text), (long)Convert.ToDecimal(txtCtgManual.Text));

                string pepe = string.Empty;
                lblMensaje.Text = string.Empty;
                if (resul.arrayErrores.Count() > 0)
                {
                    foreach (string dato in resul.arrayErrores)
                    {
                        lblMensaje.Text += dato + "<br>";
                    }
                }

                if (String.IsNullOrEmpty(lblMensaje.Text))
                {
                    if (resul.datosResponse != null)
                    {
                        //resul.datosResponse.codigoOperacion

                        string idSolicitud = Request["id"];
                        Solicitud solicitudGuardara = SolicitudDAO.Instance.GetOne(Convert.ToInt32(idSolicitud));

                        solicitudGuardara.CodigoAnulacionAfip = resul.datosResponse.codigoOperacion;

                        string[] fecha = resul.datosResponse.fechaHora.Substring(0, 10).Split('/');
                        DateTime fechaCancelacion = new DateTime(Convert.ToInt32(fecha[2]), Convert.ToInt32(fecha[1]), Convert.ToInt32(fecha[0]));
                        solicitudGuardara.FechaAnulacionAfip = fechaCancelacion;
                        solicitudGuardara.EstadoEnAFIP = Enums.EstadoEnAFIP.Anulada;
                        solicitudGuardara.EstadoEnSAP = Enums.EstadoEnvioSAP.PedidoAnulacion;
                        solicitudGuardara.ObservacionAfip = "Carta de porte ANULADA";

                        SolicitudDAO.Instance.SaveOrUpdate(solicitudGuardara);

                        Response.Redirect("Index.aspx?Id=" + solicitudGuardara.IdSolicitud.ToString());
                        return;
                    }

                }
                else
                {

                    string idSolicitud = Request["id"];
                    Solicitud solicitudGuardara = SolicitudDAO.Instance.GetOne(Convert.ToInt32(idSolicitud));
                    solicitudGuardara.ObservacionAfip = lblMensaje.Text.Trim();

                    if (lblMensaje.Text.Contains("La Carta de Porte fue Confirmada o Anulada con anterioridad"))
                        solicitudGuardara.EstadoEnAFIP = Enums.EstadoEnAFIP.Anulada;

                    SolicitudDAO.Instance.SaveOrUpdate(solicitudGuardara);

                    Response.Redirect("Index.aspx?Id=" + solicitudGuardara.IdSolicitud.ToString());

                }

            }
        }

        protected void btnModelo_Click(object sender, EventArgs e)
        {
            Solicitud solicitud = new Solicitud();
            if (Request["id"] != null)
            {
                string idSolicitud = Request["id"];
                solicitud = SolicitudDAO.Instance.GetOne(Convert.ToInt32(idSolicitud));
                Session["activarModelo"] = solicitud;

                Response.Redirect("Index.aspx");
                return;
            }

        }

        protected void btnNueva_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
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


        protected void btnGuardar_Click1(object sender, EventArgs e)
        {
            btnGuardar_Click(sender, e);
        }

        protected void cboTipoDeCarta_SelectedIndexChanged(object sender, EventArgs e)
        {


            txtCtgManual.Visible = true;
            txtNumeroCDPManual.Visible = true;
            txtNumeroCEEManual.Visible = true;
            lblFechaVencimiento.Visible = true;
            txtFechaVencimiento.Visible = true;
            txtFechaDeEmision.Enabled = false;
            txtFechaVencimiento.Enabled = false;
            txtTarifaReferencia.Enabled = true;

            btnGuardar.Visible = true;            
            btnGuardar.Enabled = true;

            txtCtgManual.Visible = true;
            lblFechaVencimiento.Visible = true;
            txtFechaVencimiento.Visible = true;
            txtTarifaReferencia.Enabled = true;


        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReportePDF.aspx?Id=" + Request["id"].ToString());
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





    }
}

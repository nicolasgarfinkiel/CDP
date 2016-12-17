using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;
using System.Security.Principal;
using System.Configuration;
using CartaDePorte.Core.DAO;
using CartaDePorte.Core.Servicios;
using CartaDePorte.Core.Domain;
using CartaDePorte.Core.Utilidades;
using CartaDePorte.Core.Domain.Seguridad;
using CartaDePorte.Core;
using CartaDePorte.Common;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;

namespace CartaDePorte.Web
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var master = (Main)Page.Master;
            master.ValidarMantenimiento();
            master.HiddenValue = "Principal";

            var id = Request["id"];

            if (!IsPostBack)
            {
                if (!App.UsuarioTienePermisos("Alta Solicitud"))
                {
                    if (!(App.UsuarioTienePermisos("Visualizacion Solicitud") && id != null && id.ToString() != "0"))
                    {
                        Response.Redirect("BandejaDeSalida.aspx");
                    }
                }
                Session["Pais"] = GrupoEmpresaDAO.Instance.GetOne(App.Usuario.IdGrupoEmpresa).Pais.Descripcion.ToUpper();
            }

            if (App.Usuario.IdGrupoEmpresa != App.ID_GRUPO_CRESUD)
            {
                this.btnGuardar.Text = "Guardar";
                this.btnSoloGuardar.Style.Add("display", "none");
            }

            var cli = EmpresaDAO.Instance.GetOne(App.Usuario.IdEmpresa).Cliente;
            if (cli != null)
            {
                this.hbClientePagadorDefaultId.Value = cli.IdCliente.ToString();
                this.hbClientePagadorDefaultName.Value = cli.RazonSocial;
            }

            if (id != null)
            {
                if (id.ToString() == "0")
                {
                    Response.Redirect("index.aspx");
                }
            }

            if (!IsPostBack)
            {
                if (Request["CambioDestino"] != null)
                {
                    string CambioDestino = Request["CambioDestino"];
                    if (CambioDestino.Equals("1"))
                    {
                        if (id != null)
                        {
                            CargarCombos();
                            hbBuscador.Value = string.Empty;
                            BuscadorEmpresa.Visible = false;
                            BuscadorCliente.Visible = false;
                            BuscadorProveedor.Visible = false;
                            BuscadorChofer.Visible = false;
                            txtFechaDeEmision.Text = DateTime.Now.ToString("dd/MM/yyyy");
                            txtFechaDeEmision.Enabled = false;
                            txtKrgsEstimados.Enabled = false;

                            if (id != null)
                            {
                                string idSolicitud = id;
                                Solicitud sol = SolicitudDAO.Instance.GetOne(Convert.ToInt32(idSolicitud));

                                CargarSolicitud(sol, false);
                            }

                            tableCarga1.Disabled = true;
                            tableCarga2.Disabled = true;
                            tableCarga3.Disabled = true;
                            tableCarga4.Disabled = true;
                            tableCarga5.Disabled = true;
                            btnGuardarCambio.Visible = App.UsuarioTienePermisos("Alta Solicitud");
                            tableCambioDestino.Visible = true;
                            return;
                        }
                    }
                }
            }

            if (Request["reenvioSAP"] != null)
            {
                string reenvio = Request["reenvioSAP"];
                if (reenvio.Equals("1"))
                {
                    if (id != null)
                    {
                        string idSolicitud = id;
                        Solicitud solicitudGuardada = SolicitudDAO.Instance.GetOne(Convert.ToInt32(idSolicitud));

                        if (solicitudGuardada.TipoDeCarta.Descripcion.Equals("Terceros por venta  de Granos de producción propia"))
                            solicitudGuardada.EstadoEnSAP = Enums.EstadoEnvioSAP.PrimerEnvioTerceros;

                        wsSAP ws = new wsSAP();
                        ws.PrefacturaSAP(solicitudGuardada, false, false);
                        Response.Redirect("BandejaDeSalida.aspx");
                        return;
                    }
                }
            }

            if (Request["reenvioAFIP"] != null)
            {
                string reenvio = Request["reenvioAFIP"];
                if (reenvio.Equals("1"))
                {
                    if (id != null)
                    {
                        CargarCombos();
                        hbBuscador.Value = string.Empty;
                        BuscadorEmpresa.Visible = false;
                        BuscadorCliente.Visible = false;
                        BuscadorProveedor.Visible = false;
                        BuscadorChofer.Visible = false;

                        string idSolicitud = id;
                        Solicitud solicitudGuardada = SolicitudDAO.Instance.GetOne(Convert.ToInt32(idSolicitud));

                        //Compra de granos que transportamos
                        if (solicitudGuardada.TipoDeCarta.Descripcion.Equals("Compra de granos que transportamos"))
                            return;

                        var ws = new wsAfip_v3();
                        var resul = ws.solicitarCTGInicial(solicitudGuardada);
                        solicitudGuardada.EstadoEnAFIP = Enums.EstadoEnAFIP.Enviado;

                        if (resul.arrayErrores.Length > 0)
                        {
                            //Token vencido Fecha y Hora de Vencimiento del Token Enviado: 16-08-2013 02:55:24 - Fecha y Hora Actual del Servidor: 16-08-2013 11:59:58
                            lblMensaje.ForeColor = Color.Red;
                            lblMensaje.Text = "ERRORES AFIP: <br>";
                            foreach (String errores in resul.arrayErrores)
                                lblMensaje.Text += Utils.Instance.NormalizarMensajeErrorAfip(errores) + "<br>";

                            solicitudGuardada.EstadoEnAFIP = Enums.EstadoEnAFIP.SinProcesar;
                        }

                        if (resul.observacion != null && resul.observacion.Length > 0)
                        {
                            lblMensaje.ForeColor = Color.Black;
                            lblMensaje.Text += resul.observacion + "<br>";
                            if (resul.observacion.Contains("CTG otorgado"))
                            {
                                txtCtg.Text = resul.datosSolicitarCTGResponse.datosSolicitarCTG.ctg.ToString();
                                solicitudGuardada.EstadoEnAFIP = Enums.EstadoEnAFIP.Otorgado;
                            }
                        }

                        if (resul.datosSolicitarCTGResponse != null && resul.datosSolicitarCTGResponse.arrayControles != null && resul.datosSolicitarCTGResponse.arrayControles.Length > 0)
                        {
                            lblMensaje.ForeColor = Color.DarkOrange;
                            lblMensaje.Text = "CONTROLES AFIP: <br>";

                            foreach (var control in resul.datosSolicitarCTGResponse.arrayControles)
                                lblMensaje.Text += control.tipo + ": " + control.descripcion + "<br>";

                            solicitudGuardada.EstadoEnAFIP = Enums.EstadoEnAFIP.SinProcesar;
                        }

                        solicitudGuardada.ObservacionAfip = "Reenvio manual:<br> " + lblMensaje.Text;
                        EnvioMailDAO.Instance.sendMail("<b>Envio de Solicitud a Afip</b> <br/><br/>" + solicitudGuardada.ObservacionAfip + "<br/>" + "Carta De Porte: " + solicitudGuardada.NumeroCartaDePorte + "<br/>" + "Usuario: " + solicitudGuardada.UsuarioCreacion);

                        if (resul.datosSolicitarCTGResponse != null && resul.datosSolicitarCTGResponse.datosSolicitarCTG != null)
                        {
                            solicitudGuardada.Ctg = resul.datosSolicitarCTGResponse.datosSolicitarCTG.ctg.ToString();
                            solicitudGuardada.EstadoEnAFIP = Enums.EstadoEnAFIP.Otorgado;

                            if (!String.IsNullOrEmpty(resul.datosSolicitarCTGResponse.datosSolicitarCTG.fechaVigenciaHasta))
                            {
                                string[] fecha = resul.datosSolicitarCTGResponse.datosSolicitarCTG.fechaVigenciaHasta.Split('/');
                                DateTime fechaVigencia = new DateTime(Convert.ToInt32(fecha[2]), Convert.ToInt32(fecha[1]), Convert.ToInt32(fecha[0]));
                                solicitudGuardada.FechaDeVencimiento = fechaVigencia;
                                solicitudGuardada.TarifaReferencia = resul.datosSolicitarCTGResponse.datosSolicitarCTG.tarifaReferencia;
                            }
                        }

                        SolicitudDAO.Instance.SaveOrUpdate(solicitudGuardada);
                        Response.Redirect("Index.aspx?Id=" + solicitudGuardada.IdSolicitud.ToString());
                        return;
                    }
                }
            }

            if (Session["activarModelo"] != null)
            {
                CargarCombos();
                hbBuscador.Value = string.Empty;
                BuscadorEmpresa.Visible = false;
                BuscadorCliente.Visible = false;
                BuscadorProveedor.Visible = false;
                BuscadorChofer.Visible = false;

                btnImprimir.Visible = false;

                Solicitud solicitudModelo = (Solicitud)Session["activarModelo"];
                Session["activarModelo"] = null;
                CargarSolicitud(solicitudModelo, true);

                txtFechaDeEmision.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtFechaDeEmision.Enabled = false;

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
                txtFechaDeEmision.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtFechaDeEmision.Enabled = false;
                txtKrgsEstimados.Enabled = false;

                if (id != null)
                {
                    string idSolicitud = id;
                    Solicitud sol = SolicitudDAO.Instance.GetOne(Convert.ToInt32(idSolicitud));
                    CargarSolicitud(sol, false);
                }
            }
            /*************************************************/
            // SOLO PARA PRUEBAS
            /*************************************************/
            //if (!IsPostBack)
            //{
            //var idTest = Tools.Value2<int>(Request["idTest"], 0);
            //if (idTest > 0)
            //{
            //    var sol = SolicitudDAO.Instance.GetOne(Convert.ToInt32(idTest));
            //    sol.IdSolicitud = 0;
            //    sol.ObservacionAfip = string.Empty;
            //    sol.Observaciones = string.Empty;

            //    CargarSolicitud(sol, false);
            //}
            //}
            ValoresCargados();

            if (!IsPostBack)
            {
                if (Session["Pais"] != null)
                    if (Session["Pais"].ToString().ToUpper().Contains("PARAGUAY"))
                    {
                        tbPorcentajes.Visible = true;

                        txtCtg.Visible = false;
                        txtCtgManual.Visible = false;
                        lblCtg.Visible = false;
                        Label18.Text = "Timbrado";
                        //txtNumeroCEE.Visible = false;
                        //txtNumeroCEEManual.Visible = false;
                        //Label18.Visible = false;
                    }
                    else
                        tbPorcentajes.Visible = false;

                lblCuit1.Text = ConfiguracionRegional.ChagedTextCulture("CUIT Nro", Session["Pais"].ToString());
                lblCuit2.Text = ConfiguracionRegional.ChagedTextCulture("CUIT Nro", Session["Pais"].ToString());
                lblCuit3.Text = ConfiguracionRegional.ChagedTextCulture("CUIT Nro", Session["Pais"].ToString());
                lblCuit4.Text = ConfiguracionRegional.ChagedTextCulture("CUIT Nro", Session["Pais"].ToString());
                lblCuit5.Text = ConfiguracionRegional.ChagedTextCulture("CUIT Nro", Session["Pais"].ToString());
                lblCuit6.Text = ConfiguracionRegional.ChagedTextCulture("CUIT Nro", Session["Pais"].ToString());
                lblCuit7.Text = ConfiguracionRegional.ChagedTextCulture("CUIT Nro", Session["Pais"].ToString());
                lblCuit8.Text = ConfiguracionRegional.ChagedTextCulture("CUIT Nro", Session["Pais"].ToString());

                string placeholder = ConfiguracionRegional.ChagedTextCulture(txtChoferAltaRapidaCuit.Attributes["placeholder"], Session["Pais"].ToString());

                txtProveedorAltaRapidaCuit.Attributes["placeholder"] = placeholder;
                txtClienteAltaRapidaCuit.Attributes["placeholder"] = placeholder;
                txtChoferAltaRapidaCuit.Attributes["placeholder"] = placeholder;

                if (Session["Pais"].ToString() == "ARGENTINA")
                    lblCuit9.Text = "CUIT/CUIL";
                else if (Session["Pais"].ToString() == "BOLIVIA")
                    lblCuit9.Text = "NIT Nro";
                else if (Session["Pais"].ToString() == "PARAGUAY")
                    lblCuit9.Text = "RUC Nro";
            }
        }

        private void CargarSolicitud(Solicitud sol, bool modelo)
        {
            if (sol.ObservacionAfip.Contains("Reserva"))
            {
                btnAnular.Visible = false;
                btnModelo.Visible = false;
                btnGuardar.Visible = false;
                txtCtgManual.Visible = true;
                txtCtg.Visible = false;
                lblFechaVencimiento.Visible = true;
                txtFechaVencimiento.Visible = true;

                if (sol.UsuarioCreacion.ToLower() != App.Usuario.Nombre.ToLower())
                {
                    btnSoloGuardar.Visible = false;
                    btnImprimir.Visible = App.UsuarioTienePermisos("Imprimir Solicitud");
                }

                if (sol.IdEstablecimientoProcedencia != null && sol.IdEstablecimientoProcedencia.IdEstablecimiento > 0)
                {
                    cboIdEstablecimientoProcedencia.SelectedValue = sol.IdEstablecimientoProcedencia.IdEstablecimiento.ToString();
                    txtDireccionEstablecimientoProcedencia.Text = sol.IdEstablecimientoProcedencia.Direccion;
                    txtLocalidadEstablecimientoProcedencia.Text = sol.IdEstablecimientoProcedencia.Localidad.Descripcion;
                    txtProvinciaEstablecimientoProcedencia.Text = sol.IdEstablecimientoProcedencia.Provincia.Descripcion;
                }

                if (sol.TipoDeCarta != null && sol.TipoDeCarta.IdTipoDeCarta > 0)
                {
                    cboTipoDeCarta.SelectedValue = sol.TipoDeCarta.IdTipoDeCarta.ToString();
                    cboTipoDeCarta.Enabled = false;
                }

                cboIdEstablecimientoProcedencia.Enabled = false;
                txtFechaDeEmision.Enabled = true;
                txtTarifaReferencia.Enabled = true;
                txtNumeroCDP.Text = sol.NumeroCartaDePorte;
                txtNumeroCEE.Text = sol.Cee;
                txtFechaDeEmision.Text = sol.FechaCreacion.ToString("dd/MM/yyyy");
                if (Session["Pais"] != null)
                    if (Session["Pais"].ToString() == "ARGENTINA")
                        lblMensaje.Text = sol.ObservacionAfip;
            }
            else
            {
                if (!modelo)
                {
                    if (sol.EstadoEnAFIP == Enums.EstadoEnAFIP.Otorgado)
                        btnAnular.Visible = App.UsuarioTienePermisos("Anular Solicitud");
                    else
                        btnAnular.Visible = false;

                    if (sol.EstadoEnAFIP == Enums.EstadoEnAFIP.Anulada)
                        btnAnular.Enabled = false;
                    else
                        btnAnular.Enabled = true;

                    if (!PaisDAO.Instance.GetOne(App.Usuario.IdPais).Descripcion.ToUpper().Contains("ARGENTINA"))
                        btnAnular.Visible = true;

                    if (!String.IsNullOrEmpty(sol.Ctg) && sol.EstadoEnAFIP != Enums.EstadoEnAFIP.Anulada)
                        btnImprimir.Visible = App.UsuarioTienePermisos("Imprimir Solicitud");
                }

                btnNueva.Visible = App.UsuarioTienePermisos("Alta Solicitud"); // true; // Visualizacion Solicitud
                btnModelo.Visible = App.UsuarioTienePermisos("Alta Solicitud");

                //cboTipoDeCarta
                cboTipoDeCarta.SelectedValue = sol.TipoDeCarta.IdTipoDeCarta.ToString();

                if (!modelo)
                {
                    if (!String.IsNullOrEmpty(sol.Ctg))
                    {
                        if (sol.EstadoEnAFIP == Enums.EstadoEnAFIP.Anulada)
                            txtCtg.Text = sol.Ctg + " (ANULADA)";
                        else
                            txtCtg.Text = sol.Ctg;

                        btnSoloGuardar.Enabled = false;
                        btnGuardar.Enabled = false;
                    }
                    else
                    {
                        btnSoloGuardar.Enabled = true;
                        btnGuardar.Enabled = true;
                    }

                    txtNumeroCDP.Text = sol.NumeroCartaDePorte;
                    txtNumeroCEE.Text = sol.Cee;
                }

                //cboEmpresaTitularCartaDePorte
                if (sol.ProveedorTitularCartaDePorte != null && sol.ProveedorTitularCartaDePorte.IdProveedor > 0)
                {
                    cboProveedorTitularCartaDePorte.Text = sol.ProveedorTitularCartaDePorte.Nombre;
                    txtCuitProveedorTitularCartaDePorte.Text = sol.ProveedorTitularCartaDePorte.NumeroDocumento;
                    hbProveedorTitularCartaDePorte.Value = sol.ProveedorTitularCartaDePorte.IdProveedor.ToString();
                    ImageButtonDeleteTitularCartaPorte.Visible = App.UsuarioTienePermisos("Alta Solicitud");

                    if (sol.ProveedorTitularCartaDePorte.EsProspecto)
                        cboProveedorTitularCartaDePorte.BackColor = Color.Yellow;
                }

                //cboClienteIntermediario
                if (sol.ClienteIntermediario != null && sol.ClienteIntermediario.IdCliente > 0)
                {
                    cboClienteIntermediario.Text = sol.ClienteIntermediario.RazonSocial;
                    txtCuitClienteIntermediario.Text = sol.ClienteIntermediario.Cuit;
                    hbClienteIntermediario.Value = sol.ClienteIntermediario.IdCliente.ToString();
                    ImageButtonDeleteIntermediario.Visible = App.UsuarioTienePermisos("Alta Solicitud");

                    if (sol.ClienteIntermediario.EsProspecto)
                        cboClienteIntermediario.BackColor = Color.Yellow;
                }

                //cboEmpresaRemitenteComercial
                if (sol.ClienteRemitenteComercial != null && sol.ClienteRemitenteComercial.IdCliente > 0)
                {
                    cboClienteRemitenteComercial.Text = sol.ClienteRemitenteComercial.RazonSocial;
                    txtCuitClienteRemitenteComercial.Text = sol.ClienteRemitenteComercial.Cuit;
                    hbClienteRemitenteComercial.Value = sol.ClienteRemitenteComercial.IdCliente.ToString();
                    ImageButtonDeleteRemitenteComercial.Visible = App.UsuarioTienePermisos("Alta Solicitud");
                    tblRemitenteComercialComoCanjeador.Visible = App.UsuarioTienePermisos("Alta Solicitud");
                    chkRemitenteComercialComoCanjeador.Checked = sol.RemitenteComercialComoCanjeador;

                    if (sol.ClienteRemitenteComercial.EsProspecto)
                        cboClienteRemitenteComercial.BackColor = Color.Yellow;
                }

                //cboClienteCorredor
                if (sol.ClienteCorredor != null && sol.ClienteCorredor.IdCliente > 0)
                {
                    cboClienteCorredor.Text = sol.ClienteCorredor.RazonSocial;
                    txtCuitClienteCorredor.Text = sol.ClienteCorredor.Cuit;
                    hbClienteCorredor.Value = sol.ClienteCorredor.IdCliente.ToString();
                    ImageButtonDeleteCorredor.Visible = App.UsuarioTienePermisos("Alta Solicitud");

                    if (sol.ClienteCorredor.EsProspecto)
                        cboClienteCorredor.BackColor = Color.Yellow;
                }
                //cboClienteEntregador
                if (sol.ClienteEntregador != null && sol.ClienteEntregador.IdCliente > 0)
                {
                    cboClienteEntregador.Text = sol.ClienteEntregador.RazonSocial;
                    txtCuitClienteEntregador.Text = sol.ClienteEntregador.Cuit;
                    hbClienteEntregador.Value = sol.ClienteEntregador.IdCliente.ToString();
                    ImageButtonDeleteRepresentanteEntregador.Visible = App.UsuarioTienePermisos("Alta Solicitud");

                    if (sol.ClienteEntregador.EsProspecto)
                        cboClienteEntregador.BackColor = Color.Yellow;
                }
                //cboClienteDestinatario
                if (sol.ClienteDestinatario != null && sol.ClienteDestinatario.IdCliente > 0)
                {
                    cboClienteDestinatario.Text = sol.ClienteDestinatario.RazonSocial;
                    txtCuitClienteDestinatario.Text = sol.ClienteDestinatario.Cuit;
                    hbClienteDestinatario.Value = sol.ClienteDestinatario.IdCliente.ToString();
                    ImageButtonDeleteDestinatario.Visible = App.UsuarioTienePermisos("Alta Solicitud");

                    if (sol.ClienteDestinatario.EsProspecto)
                        cboClienteDestinatario.BackColor = Color.Yellow;
                }
                //cboClienteDestino
                if (sol.ClienteDestino != null && sol.ClienteDestino.IdCliente > 0)
                {
                    cboClienteDestino.Text = sol.ClienteDestino.RazonSocial;
                    txtCuitClienteDestino.Text = sol.ClienteDestino.Cuit;
                    hbClienteDestino.Value = sol.ClienteDestino.IdCliente.ToString();
                    ImageButtonDeleteDestino.Visible = App.UsuarioTienePermisos("Alta Solicitud");

                    if (sol.ClienteDestino.EsProspecto)
                        cboClienteDestino.BackColor = Color.Yellow;
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
                            ImageButtonDeleteTransportista.Visible = App.UsuarioTienePermisos("Alta Solicitud");

                            if (sol.ProveedorTransportista.EsProspecto)
                                cboProveedorTransportista.BackColor = Color.Yellow;
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
                            ImageButtonDeleteTransportista.Visible = App.UsuarioTienePermisos("Alta Solicitud");
                        }
                    }
                }

                if (!modelo)
                {
                    //cboChofer
                    if (sol.Chofer != null)
                    {
                        cboChofer.Text = sol.Chofer.Apellido + ", " + sol.Chofer.Nombre;
                        txtCuitChofer.Text = sol.Chofer.Cuit;
                        hbChofer.Value = sol.Chofer.IdChofer.ToString();
                        ImageButtonDeleteChofer.Visible = App.UsuarioTienePermisos("Alta Solicitud");
                    }

                }

                //txtFechaDeEmision
                txtFechaDeEmision.Text = sol.FechaDeEmision.Value.ToString("dd/MM/yyyy");
                //cboGrano
                if (sol.Grano != null)
                {
                    cboGrano.SelectedValue = sol.Grano.IdGrano.ToString();
                    if (sol.Grano.TipoGrano != null)
                    {
                        txtTipoGrano.Text = Tools.Value2<string>(sol.Grano.TipoGrano.Descripcion, string.Empty);
                    }
                    if (sol.Grano.CosechaAfip != null)
                    {
                        txtCosecha.Text = Tools.Value2<string>(sol.Grano.CosechaAfip.Descripcion, string.Empty);
                    }
                }

                //rbCargaPesadaDestino
                rbCargaPesadaDestino.Checked = sol.CargaPesadaDestino;
                //rbDeclaracionCalidad
                rbDeclaracionCalidad.Checked = false;

                if (tbPorcentajes.Visible)
                {
                    txtPHumedad.Text = sol.PHumedad.ToString() == string.Empty ? "0" : sol.PHumedad.ToString();
                    txtPOtros.Text = sol.POtros.ToString() == string.Empty ? "0" : sol.POtros.ToString();

                    txtPHumedad.Enabled = false;
                    txtPOtros.Enabled = false;
                }



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
                if (!modelo)
                {
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
                }

                //rblFletePagadoAPagar 
                if (!modelo)
                {
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
                    ImageButtonDeletePagadorFlete.Visible = App.UsuarioTienePermisos("Alta Solicitud");

                    if (sol.ClientePagadorDelFlete.EsProspecto)
                        cboClientePagadorDelFlete.BackColor = Color.Yellow;
                }


                txtCantHoras.Text = sol.CantHoras.ToString();


                if (Session["Pais"] != null)
                    if (Session["Pais"].ToString() == "ARGENTINA")
                        lblMensaje.Text = sol.ObservacionAfip;

                if (!modelo && !String.IsNullOrEmpty(sol.Ctg) && sol.EstadoEnAFIP == Enums.EstadoEnAFIP.Anulada)
                {
                    if (Session["Pais"] != null)
                        if (Session["Pais"].ToString() == "ARGENTINA")
                            lblMensaje.Text = sol.ObservacionAfip + " - Codigo de Anulación AFIP: " + sol.CodigoAnulacionAfip;
                }

                if (modelo)
                {
                    //Limpio datos del transportista
                    cboClientePagadorDelFlete.Text = string.Empty;
                    hbClientePagadorDelFlete.Value = string.Empty;
                    txtPatente.Text = string.Empty;
                    txtAcoplado.Text = string.Empty;
                    txtKmRecorridos.Text = string.Empty;
                    txtTarifaReferencia.Text = string.Empty;
                    txtTarifa.Text = string.Empty;
                    txtCantHoras.Text = string.Empty;
                    rbCargaPesadaDestino.Checked = false;
                    txtKrgsEstimados.Text = string.Empty;
                    txtPesoBruto.Text = string.Empty;
                    txtPesoTara.Text = string.Empty;
                    txtPesoNeto.Text = string.Empty;
                    txtContratoNro.Text = string.Empty;
                    txtObsevaciones.Text = string.Empty;

                    lblMensaje.Text = "Carga de porte en base a Modelo";


                }

                if (!modelo)
                {
                    Empresa empresaDestino = EmpresaDAO.Instance.GetOneByIdCliente(sol.IdEstablecimientoDestino.IdInterlocutorDestinatario.IdCliente);
                    if (empresaDestino != null && !String.IsNullOrEmpty(empresaDestino.IdSapOrganizacionDeVenta))
                    {
                        if (sol.EstadoEnAFIP == Enums.EstadoEnAFIP.Otorgado)
                            btnConfirmarArribo.Visible = App.UsuarioTienePermisos("Confirmar Arribo");

                    }


                    if (sol.EstadoEnAFIP == Enums.EstadoEnAFIP.VueltaOrigen)
                        btnConfirmarArribo.Visible = App.UsuarioTienePermisos("Confirmar Arribo");

                }

                if (sol.IdEstablecimientoDestinoCambio != null && sol.IdEstablecimientoDestinoCambio.IdEstablecimiento > 0)
                {
                    cboIdEstablecimientoDestinoCambio.SelectedValue = sol.IdEstablecimientoDestinoCambio.IdEstablecimiento.ToString();
                    txtDireccionEstablecimientoDestinoCambio.Text = sol.IdEstablecimientoDestinoCambio.Direccion;
                    txtLocalidadEstablecimientoDestinoCambio.Text = sol.IdEstablecimientoDestinoCambio.Localidad.Descripcion;
                    txtCuitDestinoCambio.Text = sol.IdEstablecimientoDestinoCambio.IdInterlocutorDestinatario.Cuit;

                    tableCambioDestino.Visible = App.UsuarioTienePermisos("Visualizacion Solicitud");

                }
                //cboClienteDestinatario
                if (sol.ClienteDestinatarioCambio != null && sol.ClienteDestinatarioCambio.IdCliente > 0)
                {
                    cboClienteDestinatarioCambio.Text = sol.ClienteDestinatarioCambio.RazonSocial;
                    txtCuitClienteDestinatarioCambio.Text = sol.ClienteDestinatarioCambio.Cuit;
                    hbClienteDestinatarioCambio.Value = sol.ClienteDestinatarioCambio.IdCliente.ToString();
                    ImageButtonDeleteDestinatario.Visible = false;

                    tableCambioDestino.Visible = true;

                    if (sol.ClienteDestinatarioCambio.EsProspecto)
                        txtCuitClienteDestinatarioCambio.BackColor = Color.Yellow;

                }

                //if (String.IsNullOrEmpty(sol.CodigoRespuestaEnvioSAP))
                //    btnAnular.Enabled = false;


                cboTipoDeCarta_SelectedIndexChanged(null, null);


                if (modelo || Request["id"] != null)
                {
                    txtCtgManual.Visible = false;
                    txtCtg.Visible = true;
                    txtNumeroCDPManual.Visible = false;
                    txtNumeroCDP.Visible = true;
                    txtNumeroCEEManual.Visible = false;
                    txtNumeroCEE.Visible = true;
                    lblFechaVencimiento.Visible = false;
                    txtFechaVencimiento.Visible = false;
                    txtFechaDeEmision.Enabled = false;
                    txtTarifaReferencia.Enabled = false;

                    if (Request["id"] != null)
                    {
                        btnGuardar.Enabled = false;
                        btnSoloGuardar.Enabled = false;

                    }
                }

                if (sol.EstadoEnAFIP == Enums.EstadoEnAFIP.SinProcesar)
                {
                    btnGuardar.Enabled = true;
                    btnSoloGuardar.Enabled = true;

                }

            }

            if (!String.IsNullOrEmpty(sol.Ctg))
            {
                cboTipoDeCarta.Enabled = false;
            }


            lblMensaje.ForeColor = Utils.Instance.getMensajeColor(sol.ObservacionAfip);


            if (App.Usuario.IdGrupoEmpresa != App.ID_GRUPO_CRESUD)
            {
                if (sol.EstadoEnSAP != Enums.EstadoEnvioSAP.Pendiente)
                {
                    this.btnGuardar.Enabled = false;
                }
            }

            if (PaisDAO.Instance.GetOne(App.Usuario.IdPais).Descripcion.ToUpper().Contains("PARAGUAY"))
            {
                tbPorcentajes.Visible = true;
                txtPHumedad.Text = sol.PHumedad.ToString();
                txtPOtros.Text = sol.POtros.ToString();
                txtPHumedad.Enabled = false;
                txtPOtros.Enabled = false;
            }

            if (!PaisDAO.Instance.GetOne(App.Usuario.IdPais).Descripcion.ToUpper().Contains("ARGENTINA"))
            {
                btnAnular.Enabled = true;
            }
        }

        private void ValoresCargados()
        {
            if (Request["__EVENTTARGET"] == "guardar")
            {
                this.GuardarYEnviar();
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
                    tblRemitenteComercialComoCanjeador.Visible = App.UsuarioTienePermisos("Alta Solicitud");
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

            foreach (Grano g in GranoDAO.Instance.GetAll(false))
            {
                li = new ListItem();
                li.Value = g.IdGrano.ToString();
                li.Text = g.Descripcion;
                if (g.CosechaAfip != null)
                {
                    li.Text += " - " + g.CosechaAfip.Codigo;
                }

                cboGrano.Items.Add(li);
            }

        }
        private void popEstablecimientoProcedencia()
        {
            var id = Request["id"];
            ListItem li;
            li = new ListItem();
            li.Value = "-1";
            li.Text = "[seleccione...]";
            cboIdEstablecimientoProcedencia.Items.Add(li);
            var establecimiento = EstablecimientoDAO.Instance.GetEstablecimientoOrigenDestino(true, false);

            if (id == null)
                establecimiento = establecimiento.Where(e => e.Activo == true).ToList();

            foreach (Establecimiento e in establecimiento)
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
            var id = Request["id"];
            var establecimiento = EstablecimientoDAO.Instance.GetEstablecimientoOrigenDestino(false, false);

            if (id == null)
                establecimiento = establecimiento.Where(e => e.Activo == true).ToList();

            foreach (Establecimiento e in establecimiento)
            {
                li = new ListItem();
                li.Value = e.IdEstablecimiento.ToString();
                li.Text = e.Descripcion;

                cboIdEstablecimientoDestino.Items.Add(li);
            }

        }
        private void popEstablecimientoDestinoCambio()
        {
            ListItem li;
            li = new ListItem();
            li.Value = "-1";
            li.Text = "[seleccione...]";
            cboIdEstablecimientoDestinoCambio.Items.Add(li);
            var id = Request["id"];
            var establecimiento = EstablecimientoDAO.Instance.GetEstablecimientoOrigenDestino(false, false);

            if (id == null)
                establecimiento = establecimiento.Where(e => e.Activo == true).ToList();

            foreach (Establecimiento e in establecimiento)
            {
                li = new ListItem();
                li.Value = e.IdEstablecimiento.ToString();
                li.Text = e.Descripcion;

                cboIdEstablecimientoDestinoCambio.Items.Add(li);
            }

        }
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
            row.Cells.Add(AddTitleCell(ConfiguracionRegional.ChagedTextCulture("CUIT", Session["Pais"].ToString()), 100));
            row.Cells.Add(AddTitleCell("Id Cliente", 80));
            row.Cells.Add(AddTitleCell("", 10));

            tblBuscadorEmpresa.Rows.Add(row);

        }

        private void CargarTitulosBuscadorCliente()
        {
            var row = new TableRow();
            row.CssClass = "TableRowTitle";
            row.Cells.Add(AddTitleCell("Razon Social Cliente", 200));
            row.Cells.Add(AddTitleCell(ConfiguracionRegional.ChagedTextCulture("CUIT", Session["Pais"].ToString()), 100));
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
                             "'><IMG border='0' src='Content/Images/icon_select.gif'></a>";

                row.Cells.Add(AddCell(link, string.Empty, HorizontalAlign.Center));

                tblBuscadorEmpresa.Rows.Add(row);

            }
        }

        private void DatosBuscadorCliente(string busqueda)
        {
            bool tomarEnCuantaFiltroPorCresud = (hbBuscador.Value.Equals("ClientePagadorDelFlete"));
            bool esFleteAPAgar = esFleteAPagarMarcado();

            if (tomarEnCuantaFiltroPorCresud && esFleteAPAgar)
                esFleteAPAgar = true;
            else
                esFleteAPAgar = false;

            var cliente = ClienteDAO.Instance.GetFiltro(busqueda, esFleteAPAgar, tomarEnCuantaFiltroPorCresud);

            if (PaisDAO.Instance.GetOne(App.Usuario.IdPais).Descripcion.ToUpper().Contains("PARAGUAY"))
                cliente = cliente.Where(c => !c.IdCliente.ToString().StartsWith("3")).ToList();

            foreach (Cliente dato in cliente)
            {
                var row = new TableRow();
                row.CssClass = "TableRow";

                row.Cells.Add(AddCell(dato.RazonSocial, dato.RazonSocial, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(dato.Cuit, dato.Cuit, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(dato.IdCliente.ToString(), dato.IdCliente.ToString(), HorizontalAlign.Justify));

                string link = "<a href='#' onClick='BuscadorManager(" + dato.IdCliente.ToString() + ",\"" + hbBuscador.Value + "\",\"" + dato.RazonSocial + "\",\"" + dato.Cuit + "\")" +
                             "'><IMG border='0' src='Content/Images/icon_select.gif'></a>";

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

            BuscadorProveedor.Visible = App.UsuarioTienePermisos("Alta Solicitud");
            ImageButtonDeleteTitularCartaPorte.Visible = App.UsuarioTienePermisos("Alta Solicitud");

            CargarProveedoresUsados();


        }
        protected void ImageButtonClienteRemitenteComercial_Click(object sender, ImageClickEventArgs e)
        {
            hbBuscador.Value = "ClienteRemitenteComercial";
            tblBuscadorEmpresa.Rows.Clear();
            txtBuscadorEmpresa.Text = string.Empty;

            lblMensajeClienteAltaRapida.Text = string.Empty;

            BuscadorCliente.Visible = App.UsuarioTienePermisos("Alta Solicitud");
            ImageButtonDeleteRemitenteComercial.Visible = App.UsuarioTienePermisos("Alta Solicitud");
            CargarClientesUsados();

        }
        protected void ImageButtonClienteIntermediario_Click(object sender, ImageClickEventArgs e)
        {
            hbBuscador.Value = "ClienteIntermediario";
            tblBuscadorCliente.Rows.Clear();
            txtBuscadorCliente.Text = string.Empty;

            lblMensajeClienteAltaRapida.Text = string.Empty;

            BuscadorCliente.Visible = App.UsuarioTienePermisos("Alta Solicitud");
            ImageButtonDeleteIntermediario.Visible = App.UsuarioTienePermisos("Alta Solicitud");
            CargarClientesUsados();

        }
        protected void ImageButtonClienteCorredor_Click(object sender, ImageClickEventArgs e)
        {
            hbBuscador.Value = "ClienteCorredor";
            tblBuscadorCliente.Rows.Clear();
            txtBuscadorCliente.Text = string.Empty;

            lblMensajeClienteAltaRapida.Text = string.Empty;

            BuscadorCliente.Visible = App.UsuarioTienePermisos("Alta Solicitud");
            ImageButtonDeleteCorredor.Visible = App.UsuarioTienePermisos("Alta Solicitud");
            CargarClientesUsados();
        }
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
            row.Cells.Add(AddTitleCell(ConfiguracionRegional.ChagedTextCulture("CUIT ", Session["Pais"].ToString()), 100));
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
                             "'><IMG border='0' src='Content/Images/icon_select.gif'></a>";

                row.Cells.Add(AddCell(link, string.Empty, HorizontalAlign.Center));

                tblBuscadorProveedor.Rows.Add(row);

            }
        }
        protected void ImageButtonClienteEntregador_Click(object sender, ImageClickEventArgs e)
        {
            hbBuscador.Value = "ClienteEntregador";
            tblBuscadorCliente.Rows.Clear();
            txtBuscadorCliente.Text = string.Empty;

            lblMensajeClienteAltaRapida.Text = string.Empty;

            BuscadorCliente.Visible = App.UsuarioTienePermisos("Alta Solicitud");
            ImageButtonDeleteRepresentanteEntregador.Visible = App.UsuarioTienePermisos("Alta Solicitud");
            CargarClientesUsados();
        }
        protected void ImageButtonClienteDestinatario_Click(object sender, ImageClickEventArgs e)
        {
            hbBuscador.Value = "ClienteDestinatario";
            tblBuscadorCliente.Rows.Clear();
            txtBuscadorCliente.Text = string.Empty;

            lblMensajeClienteAltaRapida.Text = string.Empty;

            BuscadorCliente.Visible = App.UsuarioTienePermisos("Alta Solicitud");
            ImageButtonDeleteDestinatario.Visible = App.UsuarioTienePermisos("Alta Solicitud");
            CargarClientesUsados();

        }
        protected void ImageButtonClienteDestino_Click(object sender, ImageClickEventArgs e)
        {
            hbBuscador.Value = "ClienteDestino";
            tblBuscadorCliente.Rows.Clear();
            txtBuscadorCliente.Text = string.Empty;

            lblMensajeClienteAltaRapida.Text = string.Empty;

            BuscadorCliente.Visible = App.UsuarioTienePermisos("Alta Solicitud");
            ImageButtonDeleteDestino.Visible = App.UsuarioTienePermisos("Alta Solicitud");
            CargarClientesUsados();

        }
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

            if (App.Usuario.IdGrupoEmpresa == App.ID_GRUPO_CRESUD) //Valido patente solo cresud
            {
                if (!CuitValido(txtChoferAltaRapidaCuit.Text.Trim()))
                {
                    lblMensajeChoferAltaRapida.Text = "Cuit no válido<br>";
                    return false;
                }
            }

            if (Session["OrigenBuscadorChofer"].ToString() != "ChoferTransportista")
            {
                if (String.IsNullOrEmpty(txtChoferAltaRapidaApellido.Text.Trim()))
                {
                    lblMensajeChoferAltaRapida.Text = "Debe completar el Apellido del chofer para Alta Rápida";
                    return false;
                }

                if (App.Usuario.IdGrupoEmpresa == App.ID_GRUPO_CRESUD) //Valido patente solo cresud
                {
                    if (!String.IsNullOrEmpty(txtChoferAltaRapidaCamion.Text.Trim()))
                    {
                        if (txtChoferAltaRapidaCamion.Text.Trim().Length > 7 || txtChoferAltaRapidaCamion.Text.Trim().Length < 6)
                        {
                            lblMensajeChoferAltaRapida.Text += "La patente del Camión debe tener los 6 o 7 caracteres, ej: AAA111 o AA111AA<br>";
                            return false;
                        }
                        if (!Tools.ValidarPatente(txtChoferAltaRapidaCamion.Text.Trim().ToUpper()))
                        {
                            lblMensajeChoferAltaRapida.Text += "La patente del Camión tiene formato incorrecto, ej: AAA111 o AA111AA<br>";
                            return false;
                        }
                    }
                    if (!String.IsNullOrEmpty(txtChoferAltaRapidaAcoplado.Text.Trim()))
                    {
                        if (txtChoferAltaRapidaAcoplado.Text.Trim().Length > 7 || txtChoferAltaRapidaAcoplado.Text.Trim().Length < 6)
                        {
                            lblMensajeChoferAltaRapida.Text += "La patente del Acoplado debe tener los 6 caracteres, ej: AAA111 o AA111AA<br>";
                            return false;
                        }
                        if (!Tools.ValidarPatente(txtChoferAltaRapidaAcoplado.Text.Trim().ToUpper()))
                        {
                            lblMensajeChoferAltaRapida.Text += "La patente del Acoplado tiene formato incorrecto, ej: AAA111 o AA111AA<br>";
                            return false;
                        }
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
            else
            {

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
                clientenuevo.IdCliente = ClienteDAO.Instance.getIdProspecto();
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

            if (PaisDAO.Instance.GetOne(App.Usuario.IdPais).Descripcion.ToUpper().Contains("ARGENTINA"))
            {
                if (!CuitValido(txtClienteAltaRapidaCuit.Text.Trim()))
                {
                    lblMensajeClienteAltaRapida.Text = "Cuit no válido<br>";
                    return false;
                }
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
            row.Cells.Add(AddTitleCell(ConfiguracionRegional.ChagedTextCulture("CUIT ", Session["Pais"].ToString()), 100));
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

                row.Cells.Add(AddCell(dato.Nombre + " " + dato.Apellido, dato.Nombre + " " + dato.Apellido, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(dato.Cuit, dato.Cuit, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(dato.Camion, dato.Camion, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(dato.Acoplado, dato.Acoplado, HorizontalAlign.Justify));

                string link = "<a href='#' onClick='BuscadorManager(" + dato.IdChofer.ToString() + ",\"" + hbBuscador.Value + "\",\"" + dato.Nombre + " " + dato.Apellido + "\",\"" + dato.Cuit + "\")" +
                             "'><IMG border='0' src='Content/Images/icon_select.gif'></a>";

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
        protected void ImageButtonClientePagadorDelFlete_Click(object sender, ImageClickEventArgs e)
        {

            hbBuscador.Value = "ClientePagadorDelFlete";
            tblBuscadorCliente.Rows.Clear();
            txtBuscadorCliente.Text = string.Empty;

            lblMensajeClienteAltaRapida.Text = string.Empty;

            BuscadorCliente.Visible = App.UsuarioTienePermisos("Alta Solicitud");
            ImageButtonDeletePagadorFlete.Visible = App.UsuarioTienePermisos("Alta Solicitud");
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

                if (lblMensaje.Text.Contains("No se puede guarda la Carta de Porte, el Establecimiento de"))
                {
                    btnGuardar.Enabled = true;
                    btnSoloGuardar.Enabled = true;
                    lblMensaje.Visible = false;
                    savepanelSoloGuardar.Update();
                    savepanel.Update();
                    UpdatePanel9.Update();
                    UpdatePaneltxtDireccionEstablecimientoDestino.Update();
                }
            }
            else
            {
                txtDireccionEstablecimientoProcedencia.Text = string.Empty;
                txtLocalidadEstablecimientoProcedencia.Text = string.Empty;
                txtProvinciaEstablecimientoProcedencia.Text = string.Empty;

                if (lblMensaje.Text.Contains("No se puede guarda la Carta de Porte, el Establecimiento de"))
                {
                    btnGuardar.Enabled = true;
                    btnSoloGuardar.Enabled = true;
                    lblMensaje.Visible = false;
                    savepanelSoloGuardar.Update();
                    savepanel.Update();
                    UpdatePanel9.Update();
                    UpdatePaneltxtDireccionEstablecimientoDestino.Update();
                }
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

                if (lblMensaje.Text.Contains("No se puede guarda la Carta de Porte, el Establecimiento de"))
                {
                    btnGuardar.Enabled = true;
                    btnSoloGuardar.Enabled = true;
                    lblMensaje.Visible = false;
                    savepanelSoloGuardar.Update();
                    savepanel.Update();
                    UpdatePanel9.Update();
                    UpdatePaneltxtDireccionEstablecimientoDestino.Update();
                }
            }
            else
            {
                txtDireccionEstablecimientoDestino.Text = string.Empty;
                txtLocalidadEstablecimientoDestino.Text = string.Empty;
                txtProvinciaEstablecimientoDestino.Text = string.Empty;

                cboClienteDestino.Text = string.Empty;
                txtCuitClienteDestino.Text = string.Empty;
                hbClienteDestino.Value = string.Empty;

                if (lblMensaje.Text.Contains("No se puede guarda la Carta de Porte, el Establecimiento de"))
                {
                    btnGuardar.Enabled = true;
                    btnSoloGuardar.Enabled = true;
                    lblMensaje.Visible = false;
                    savepanelSoloGuardar.Update();
                    savepanel.Update();
                    UpdatePanel9.Update();
                    UpdatePaneltxtDireccionEstablecimientoDestino.Update();
                }
            }
        }

        private void GuardarSolo()
        {
            btnSoloGuardar.Enabled = false;

            if (Validaciones())
            {
                //Jira MDS-973
                //Si quiere guardar la carta y un establecimiento esta dado de baja lo notifica
                if (ValidarEstablecimiento())
                    return;

                Solicitud solicitud = new Solicitud();

                if (Request["id"] != null)
                {
                    string idSolicitud = Request["id"];
                    solicitud = SolicitudDAO.Instance.GetOne(Convert.ToInt32(idSolicitud));
                }

                if (solicitud.IdSolicitud < 1)
                {
                    if (cboTipoDeCarta.SelectedItem.Text.Equals("Venta de granos de terceros") ||
                        cboTipoDeCarta.SelectedItem.Text.Equals("Compra de granos") ||
                        cboTipoDeCarta.SelectedItem.Text.Equals("Terceros por venta  de Granos de producción propia"))
                    {
                        // No debo solicitar numero de carta de porte
                    }
                    else
                    {
                        Establecimiento estabOrigenParaCDP = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(cboIdEstablecimientoProcedencia.SelectedValue));
                        int idEstablecimientoParaCDP = 0;
                        if (estabOrigenParaCDP != null && estabOrigenParaCDP.AsociaCartaDePorte)
                            idEstablecimientoParaCDP = estabOrigenParaCDP.IdEstablecimiento;

                        CartasDePorte cartadePorteDisponible = CartaDePorteDAO.Instance.GetCartaDePorteDisponible(idEstablecimientoParaCDP);
                        if (String.IsNullOrEmpty(cartadePorteDisponible.NumeroCartaDePorte))
                        {
                            lblMensaje.Text = "<h1>NO hay cartas de porte disponibles, por favor, ingrese un lote desde la Administracion.</h1>";
                            return;
                        }
                        CartaDePorteDAO.Instance.SetEstadoCartaDePorte(cartadePorteDisponible.IdCartaDePorte, Enums.EstadoRangoCartaDePorte.NoDisponible);
                        solicitud.NumeroCartaDePorte = cartadePorteDisponible.NumeroCartaDePorte;
                        solicitud.Cee = cartadePorteDisponible.NumeroCee;
                    }
                }

                solicitud.CantHoras = Tools.Value2<long>(txtCantHoras.Text, 0);
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
                solicitud.Ctg = "";
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
                solicitud.KmRecorridos = Tools.Value2<long>(txtKmRecorridos.Text, 0);
                solicitud.LoteDeMaterial = solicitud.Grano.SujetoALote;

                if (!String.IsNullOrEmpty(txtContratoNro.Text.Trim()))
                    solicitud.NumeroContrato = Convert.ToInt32(txtContratoNro.Text);

                solicitud.Observaciones = txtObsevaciones.Text.Trim();
                solicitud.PatenteAcoplado = txtAcoplado.Text.Trim();
                solicitud.PatenteCamion = txtPatente.Text.Trim();

                if (solicitud.CargaPesadaDestino)
                {
                    solicitud.KilogramosEstimados = Tools.Value2<long>(txtKrgsEstimados.Text, 0);
                }
                else
                {
                    solicitud.PesoBruto = Tools.Value2<long>(txtPesoBruto.Text, 0);
                    solicitud.PesoTara = Tools.Value2<long>(txtPesoTara.Text, 0);
                    solicitud.PesoNeto = solicitud.PesoBruto - solicitud.PesoTara;
                }

                solicitud.TarifaReal = ConvertToDecimal(txtTarifa.Text.Trim().Replace(',', '.').Replace("$", "").Trim());
                solicitud.TipoDeCarta = TipoDeCartaDAO.Instance.GetOne(Convert.ToInt32(cboTipoDeCarta.SelectedValue));
                solicitud.UsuarioCreacion = App.Usuario.Nombre;
                solicitud.UsuarioModificacion = App.Usuario.Nombre;
                solicitud.EstadoEnSAP = Enums.EstadoEnvioSAP.Pendiente;
                solicitud.EstadoEnAFIP = Enums.EstadoEnAFIP.SinProcesar;
                solicitud.ObservacionAfip = "Solicitud guardada.";


                if (cboTipoDeCarta.SelectedItem.Text.Equals("Compra de granos"))
                {
                    solicitud.EstadoEnSAP = Enums.EstadoEnvioSAP.NoEnviadaASap;
                }

                if (txtCtgManual.Visible)
                {
                    solicitud.Ctg = txtCtgManual.Text.Trim();
                    if (String.IsNullOrEmpty(txtCtgManual.Text.Trim()))
                        solicitud.Ctg = "0";

                    solicitud.TarifaReferencia = ConvertToDecimal(txtTarifaReferencia.Text.Trim().Replace(',', '.').Replace("$", "").Trim());

                    solicitud.ObservacionAfip = "Solicitud Manual";

                    solicitud.EstadoEnAFIP = Enums.EstadoEnAFIP.CargaManual;
                    solicitud.EstadoEnSAP = Enums.EstadoEnvioSAP.Pendiente;
                }

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
                    solicitud.EstadoEnSAP = Utils.Instance.ValidarEstadoParaSAP(solicitud);
                    int SolicitudID = SolicitudDAO.Instance.SaveOrUpdate(solicitud);
                    if (SolicitudID > 0)
                    {
                        Response.Redirect("Index.aspx?Id=" + solicitud.IdSolicitud.ToString());
                    }
                }
            }
        }

        private bool ValidarEstablecimiento()
        {
            //Jira MDS-973
            //Si quiere guardar la carta y un establecimiento esta dado de baja lo notifica
            var EstablecimientoProcedenciaSave = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(cboIdEstablecimientoProcedencia.SelectedValue));
            var EstablecimientoDestinoSave = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(cboIdEstablecimientoDestino.SelectedValue));

            if (EstablecimientoProcedenciaSave.Activo == false)
            {
                lblMensaje.Visible = true;
                lblMensaje.ForeColor = Color.Red;
                lblMensaje.Text = "No se puede guarda la Carta de Porte, el Establecimiento de Procedencia se encuentra dado de baja.";
                return true;
            }

            if (EstablecimientoDestinoSave.Activo == false)
            {
                lblMensaje.Visible = true;
                lblMensaje.ForeColor = Color.Red;
                lblMensaje.Text = "No se puede guarda la Carta de Porte, el Establecimiento de Destino se encuentra dado de baja.";
                return true;
            }
            return false;
        }

        private void GuardarYEnviar()
        {
            btnGuardar.Enabled = false;

            if (Validaciones())
            {
                //Jira MDS-973
                //Si quiere guardar la carta y un establecimiento esta dado de baja lo notifica
                if (ValidarEstablecimiento())
                    return;

                Solicitud solicitud = new Solicitud();

                if (Request["id"] != null)
                {
                    string idSolicitud = Request["id"];
                    solicitud = SolicitudDAO.Instance.GetOne(Convert.ToInt32(idSolicitud));
                }

                if (tbPorcentajes.Visible)
                {
                    solicitud.PHumedad = Convert.ToDecimal(txtPHumedad.Text == string.Empty ? "0" : txtPHumedad.Text.Replace(".", ","));
                    solicitud.POtros = Convert.ToDecimal(txtPOtros.Text == string.Empty ? "0" : txtPOtros.Text.Replace(".", ","));
                }

                if (tblRemitenteComercialComoCanjeador.Visible)
                    solicitud.RemitenteComercialComoCanjeador = chkRemitenteComercialComoCanjeador.Checked;

                if (!String.IsNullOrEmpty(txtCantHoras.Text.Trim()))
                    solicitud.CantHoras = (long)ConvertToDecimal(txtCantHoras.Text.Trim());

                solicitud.CargaPesadaDestino = rbCargaPesadaDestino.Checked;

                if (!String.IsNullOrEmpty(hbChofer.Value))
                    solicitud.Chofer = ChoferDAO.Instance.GetOne(Convert.ToInt32(hbChofer.Value));

                if (!String.IsNullOrEmpty(hbClienteCorredor.Value))
                    solicitud.ClienteCorredor = ClienteDAO.Instance.GetOne(Convert.ToInt32(hbClienteCorredor.Value));
                else
                    solicitud.ClienteCorredor = new Cliente();

                if (!String.IsNullOrEmpty(hbClienteDestinatario.Value))
                    solicitud.ClienteDestinatario = ClienteDAO.Instance.GetOne(Convert.ToInt32(hbClienteDestinatario.Value));
                else
                    solicitud.ClienteDestinatario = new Cliente();

                if (!String.IsNullOrEmpty(hbClienteDestino.Value))
                    solicitud.ClienteDestino = ClienteDAO.Instance.GetOne(Convert.ToInt32(hbClienteDestino.Value));
                else
                    solicitud.ClienteDestino = new Cliente();

                if (!String.IsNullOrEmpty(hbClienteEntregador.Value))
                    solicitud.ClienteEntregador = ClienteDAO.Instance.GetOne(Convert.ToInt32(hbClienteEntregador.Value));
                else
                    solicitud.ClienteEntregador = new Cliente();

                if (!String.IsNullOrEmpty(hbClienteIntermediario.Value))
                    solicitud.ClienteIntermediario = ClienteDAO.Instance.GetOne(Convert.ToInt32(hbClienteIntermediario.Value));
                else
                    solicitud.ClienteIntermediario = new Cliente();

                if (!String.IsNullOrEmpty(hbClientePagadorDelFlete.Value))
                    solicitud.ClientePagadorDelFlete = ClienteDAO.Instance.GetOne(Convert.ToInt32(hbClientePagadorDelFlete.Value));
                else
                    solicitud.ClientePagadorDelFlete = new Cliente();

                solicitud.ConformeCondicional = getEnumCCValue();

                if (!String.IsNullOrEmpty(hbClienteRemitenteComercial.Value))
                    solicitud.ClienteRemitenteComercial = ClienteDAO.Instance.GetOne(Convert.ToInt32(hbClienteRemitenteComercial.Value));
                else
                    solicitud.ClienteRemitenteComercial = new Cliente();

                if (!String.IsNullOrEmpty(hbProveedorTitularCartaDePorte.Value))
                    solicitud.ProveedorTitularCartaDePorte = ProveedorDAO.Instance.GetOne(Convert.ToInt32(hbProveedorTitularCartaDePorte.Value));
                else
                    solicitud.ProveedorTitularCartaDePorte = new Proveedor();

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
                solicitud.FechaDeEmision = DateTime.Now;
                solicitud.Grano = GranoDAO.Instance.GetOne(Convert.ToInt32(cboGrano.SelectedValue));
                solicitud.IdEstablecimientoDestino = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(cboIdEstablecimientoDestino.SelectedValue));
                solicitud.IdEstablecimientoProcedencia = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(cboIdEstablecimientoProcedencia.SelectedValue));

                if (!String.IsNullOrEmpty(txtKmRecorridos.Text.Trim()))
                    solicitud.KmRecorridos = (long)ConvertToDecimal((txtKmRecorridos.Text.Trim()));

                solicitud.LoteDeMaterial = solicitud.Grano.SujetoALote;

                if (!String.IsNullOrEmpty(txtContratoNro.Text.Trim()))
                    solicitud.NumeroContrato = Convert.ToInt32(txtContratoNro.Text);

                solicitud.Observaciones = txtObsevaciones.Text.Trim();
                solicitud.PatenteAcoplado = txtAcoplado.Text.Trim();
                solicitud.PatenteCamion = txtPatente.Text.Trim();

                if (solicitud.CargaPesadaDestino)
                {
                    solicitud.KilogramosEstimados = (long)ConvertToDecimal((String.IsNullOrEmpty(txtKrgsEstimados.Text.Trim())) ? "0" : txtKrgsEstimados.Text.Trim());
                }
                else
                {
                    solicitud.PesoBruto = (long)ConvertToDecimal((txtPesoBruto.Text.Trim()));
                    solicitud.PesoTara = (long)ConvertToDecimal((txtPesoTara.Text.Trim()));
                    solicitud.PesoNeto = solicitud.PesoBruto - solicitud.PesoTara;
                }

                if (cboTipoDeCarta.SelectedItem.Text.Equals("Venta de granos de terceros") ||
                    cboTipoDeCarta.SelectedItem.Text.Equals("Compra de granos") ||
                    cboTipoDeCarta.SelectedItem.Text.Equals("Terceros por venta  de Granos de producción propia"))
                {
                    if (!String.IsNullOrEmpty(txtTarifaReferencia.Text))
                        solicitud.TarifaReferencia = ConvertToDecimal(txtTarifaReferencia.Text.Trim().Replace(',', '.'));
                    if (!String.IsNullOrEmpty(txtFechaVencimiento.Text))
                        solicitud.FechaDeVencimiento = StringToDateTime(txtFechaVencimiento.Text);
                    if (!String.IsNullOrEmpty(txtFechaDeEmision.Text))
                        solicitud.FechaDeEmision = StringToDateTime(Request.Form[txtFechaDeEmision.UniqueID]);
                }

                if (!String.IsNullOrEmpty(txtTarifa.Text))
                    solicitud.TarifaReal = ConvertToDecimal((txtTarifa.Text.Trim().Replace(',', '.').Replace("$", "").Trim()));

                solicitud.TipoDeCarta = TipoDeCartaDAO.Instance.GetOne(Convert.ToInt32(cboTipoDeCarta.SelectedValue));
                solicitud.UsuarioCreacion = App.Usuario.Nombre;
                solicitud.UsuarioModificacion = App.Usuario.Nombre;
                solicitud.EstadoEnSAP = Enums.EstadoEnvioSAP.Pendiente;
                solicitud.EstadoEnAFIP = Enums.EstadoEnAFIP.SinProcesar;

                if (cboTipoDeCarta.SelectedItem.Text.ToUpper().Equals("COMPRA DE GRANOS"))
                    solicitud.EstadoEnSAP = Enums.EstadoEnvioSAP.NoEnviadaASap;

                if (ValidarLogicaPorTipoCartaDePorte(solicitud))
                {
                    // Antes de guardar chequeo si algun cliente o proveedor cargado es prospecto para
                    // setear solicitud.EstadoEnSAP en 9 (EnEsperaProspecto)
                    solicitud.EstadoEnSAP = Utils.Instance.ValidarEstadoParaSAP(solicitud);

                    /**************************************************************************************************************/
                    /**************************************************************************************************************/
                    //Grabo la solicitud 
                    //si CDP 7 No envía a AFIP y deja el estado SAP en NoEnviadaASap.
                    //Es una precarga por lo tanto no debe tomarlo en proceso de windows.
                    if (!PaisDAO.Instance.GetOne(App.Usuario.IdPais).Descripcion.ToUpper().Contains("ARGENTINA"))
                    {
                        solicitud.EstadoEnAFIP = Enums.EstadoEnAFIP.SinProcesar;
                        solicitud.ObservacionAfip = "Solicitud SIN proceso AFIP";
                        solicitud.EstadoEnSAP = Enums.EstadoEnvioSAP.Pendiente;
                    }

                    //if (Environment.MachineName.ToUpper() == "SRV-MS10-ADM" && PaisDAO.Instance.GetOne(App.Usuario.IdPais).Descripcion.ToUpper().Contains("ARGENTINA"))
                    //{
                    //    solicitud.EstadoEnAFIP = Enums.EstadoEnAFIP.Otorgado;
                    //    solicitud.Ctg = "123456";
                    //    solicitud.ObservacionAfip = "Ambiente Bajo sin proceso de AFIP";
                    //}

                    if (solicitud.IdSolicitud < 1)
                    {
                        solicitud.Ctg = "";

                        if (cboTipoDeCarta.SelectedItem.Text.Equals("Venta de granos de terceros") ||
                            cboTipoDeCarta.SelectedItem.Text.Equals("Compra de granos") ||
                            cboTipoDeCarta.SelectedItem.Text.Equals("Terceros por venta  de Granos de producción propia"))
                        {
                            solicitud.NumeroCartaDePorte = txtNumeroCDPManual.Text;
                            solicitud.Cee = txtNumeroCEEManual.Text;
                            solicitud.Ctg = txtCtgManual.Text;
                            if (String.IsNullOrEmpty(solicitud.Ctg))
                                solicitud.Ctg = "0";
                        }
                        else
                        {
                            Establecimiento estabOrigenParaCDP = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(cboIdEstablecimientoProcedencia.SelectedValue));
                            int idEstablecimientoParaCDP = 0;

                            if (estabOrigenParaCDP != null && estabOrigenParaCDP.AsociaCartaDePorte)
                                idEstablecimientoParaCDP = estabOrigenParaCDP.IdEstablecimiento;

                            CartasDePorte cartadePorteDisponible = CartaDePorteDAO.Instance.GetCartaDePorteDisponible(idEstablecimientoParaCDP);
                            if (cartadePorteDisponible == null ||
                                String.IsNullOrEmpty(cartadePorteDisponible.NumeroCartaDePorte))
                            {
                                lblMensaje.Text = "<h1>No hay cartas de porte disponibles, por favor, ingrese un lote desde la Administracion.</h1>";
                                return;
                            }
                            CartaDePorteDAO.Instance.SetEstadoCartaDePorte(cartadePorteDisponible.IdCartaDePorte, Enums.EstadoRangoCartaDePorte.NoDisponible);
                            solicitud.NumeroCartaDePorte = cartadePorteDisponible.NumeroCartaDePorte;
                            solicitud.Cee = cartadePorteDisponible.NumeroCee;
                        }
                    }

                    int SolicitudID = SolicitudDAO.Instance.SaveOrUpdate(solicitud);

                    //if (Environment.MachineName.ToUpper() == "SRV-MS10-ADM" && PaisDAO.Instance.GetOne(App.Usuario.IdPais).Descripcion.ToUpper().Contains("ARGENTINA"))
                    //{
                    //    Response.Redirect("Index.aspx?Id=" + SolicitudID);
                    //    return;
                    //}

                    //Todas aquellas solicitudes sin AFIP no deberian continuar, en este caso solo se hace para CRESCA SA.
                    if (!PaisDAO.Instance.GetOne(App.Usuario.IdPais).Descripcion.ToUpper().Contains("ARGENTINA"))
                    {
                        Response.Redirect("Index.aspx?Id=" + SolicitudID);
                        return;
                    }
                    //Leo la solicitud
                    Solicitud solicitudGuardada = SolicitudDAO.Instance.GetOne(SolicitudID);
                    /**************************************************************************************************************/
                    /**************************************************************************************************************/

                    /* Tipo Solicitud
                    1	Venta de granos propios	True
                    2	Venta de granos de terceros	True
                    3	Compra de granos que transportamos	False
                    4	Compra de granos	False
                    5	Traslado de granos	True
                    6	Canje	False
                    7	Terceros por venta  de Granos de producción propia	True
                    8	Traslado de granos con desconsignacion	True
                    */

                    if (SolicitudID > 0)
                    {
                        if (App.Usuario.IdGrupoEmpresa != App.ID_GRUPO_CRESUD)
                        {
                            //NO ES GRUPO CRESUD
                            solicitudGuardada.EstadoEnAFIP = Enums.EstadoEnAFIP.SinProcesar;
                            solicitudGuardada.ObservacionAfip = "Solicitud SIN proceso AFIP";
                        }
                        else
                        {
                            //PROCESO AFIP SOLO GRUPO CRESUD
                            if (solicitud.TipoDeCarta.IdTipoDeCarta != 2 &&
                                solicitud.TipoDeCarta.IdTipoDeCarta != 4 &&
                                solicitud.TipoDeCarta.IdTipoDeCarta != 7)
                            {
                                //Jira MDS-973
                                //Si quiere guardar la carta y un establecimiento esta dado de baja lo notifica
                                if (ValidarEstablecimiento())
                                    return;

                                //hago el envio a AFIP y a SAP
                                var ws = new wsAfip_v3();
                                var resul = ws.solicitarCTGInicial(solicitud);

                                solicitudGuardada.EstadoEnAFIP = Enums.EstadoEnAFIP.Enviado;

                                if (resul.arrayErrores != null && resul.arrayErrores.Length > 0)
                                {
                                    //Token vencido Fecha y Hora de Vencimiento del Token Enviado: 16-08-2013 02:55:24 - Fecha y Hora Actual del Servidor: 16-08-2013 11:59:58
                                    lblMensaje.ForeColor = Color.Red;
                                    lblMensaje.Text = "ERRORES AFIP: <br>";
                                    foreach (String errores in resul.arrayErrores)
                                    {
                                        lblMensaje.Text += Utils.Instance.NormalizarMensajeErrorAfip(errores) + "<br>";
                                    }

                                    solicitudGuardada.EstadoEnAFIP = Enums.EstadoEnAFIP.SinProcesar;
                                }

                                if (resul.observacion != null && resul.observacion.Length > 0)
                                {
                                    lblMensaje.ForeColor = Color.Black;
                                    lblMensaje.Text += resul.observacion + "<br>";
                                    if (resul.observacion.Contains("CTG otorgado"))
                                    {
                                        txtCtg.Text = resul.datosSolicitarCTGResponse.datosSolicitarCTG.ctg.ToString();
                                        btnImprimir.Visible = App.UsuarioTienePermisos("Imprimir Solicitud");
                                        solicitudGuardada.EstadoEnAFIP = Enums.EstadoEnAFIP.Otorgado;
                                    }
                                }

                                if (resul.datosSolicitarCTGResponse != null && resul.datosSolicitarCTGResponse.arrayControles != null && resul.datosSolicitarCTGResponse.arrayControles.Length > 0)
                                {
                                    lblMensaje.ForeColor = Color.DarkOrange;
                                    lblMensaje.Text = "CONTROLES AFIP: <br>";
                                    foreach (var control in resul.datosSolicitarCTGResponse.arrayControles)
                                    {
                                        lblMensaje.Text += control.tipo + ": " + control.descripcion + "<br>";
                                    }
                                    solicitudGuardada.EstadoEnAFIP = Enums.EstadoEnAFIP.SinProcesar;
                                }

                                solicitudGuardada.ObservacionAfip = lblMensaje.Text;

                                if (resul.datosSolicitarCTGResponse != null && resul.datosSolicitarCTGResponse.datosSolicitarCTG != null)
                                {
                                    solicitudGuardada.Ctg = resul.datosSolicitarCTGResponse.datosSolicitarCTG.ctg.ToString();
                                    solicitudGuardada.EstadoEnAFIP = Enums.EstadoEnAFIP.Otorgado;

                                    if (!String.IsNullOrEmpty(resul.datosSolicitarCTGResponse.datosSolicitarCTG.fechaVigenciaHasta))
                                    {
                                        string[] fecha = resul.datosSolicitarCTGResponse.datosSolicitarCTG.fechaVigenciaHasta.Split('/');
                                        DateTime fechaVigencia = new DateTime(Convert.ToInt32(fecha[2]), Convert.ToInt32(fecha[1]), Convert.ToInt32(fecha[0]));
                                        solicitudGuardada.FechaDeVencimiento = fechaVigencia;
                                        solicitudGuardada.TarifaReferencia = resul.datosSolicitarCTGResponse.datosSolicitarCTG.tarifaReferencia;
                                    }
                                }
                            }
                            else
                            {
                                solicitudGuardada.EstadoEnAFIP = Enums.EstadoEnAFIP.CargaManual;
                                solicitudGuardada.ObservacionAfip = "Carga Manual";
                            }

                            EnvioMailDAO.Instance.sendMail("<b>Envio de Solicitud a Afip</b> <br/><br/>" + solicitudGuardada.ObservacionAfip + "<br/>" + "Carta De Porte: " + solicitudGuardada.NumeroCartaDePorte + "<br/>" + "Usuario: " + solicitudGuardada.UsuarioCreacion);
                        }

                        if (solicitud.TipoDeCarta.IdTipoDeCarta == 7)
                        {
                            solicitudGuardada.EstadoEnAFIP = Enums.EstadoEnAFIP.CargaManual;
                            solicitudGuardada.ObservacionAfip = "Carga Manual";
                            solicitudGuardada.EstadoEnSAP = Enums.EstadoEnvioSAP.Pendiente;
                        }

                        SolicitudDAO.Instance.SaveOrUpdate(solicitudGuardada);

                        Response.Redirect("Index.aspx?Id=" + SolicitudID);
                    }
                }
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
                btnGuardar.Enabled = false;
                btnGuardarCambio.Enabled = true;
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

            // FMG validacion



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

                if (String.IsNullOrEmpty(hbClientePagadorDelFlete.Value))
                {
                    mensaje += "Debe seleccionar un Pagador del Flete.<br>";
                }
            }


            if (txtTarifaReferencia.Enabled)
            {
                if (String.IsNullOrEmpty(txtTarifaReferencia.Text.Trim()))
                {
                    mensaje += "Debe completar la tarifa de referencia enviada por AFIP<br>";
                }
            }

            if (App.Usuario.IdGrupoEmpresa == App.ID_GRUPO_CRESUD) //Valido patente solo cresud
            {
                if (!String.IsNullOrEmpty(txtPatente.Text.Trim()))
                {
                    if (txtPatente.Text.Trim().Length > 7 || txtPatente.Text.Trim().Length < 6)
                        mensaje += "La patente del Camión debe tener los 6 o 7 caracteres, ej: AAA111 o AA111AA<br>";
                    if (!Tools.ValidarPatente(txtPatente.Text.Trim().ToUpper()))
                        mensaje += "La patente del Camión tiene formato incorrecto, ej: AAA111<br>";
                }
                if (!String.IsNullOrEmpty(txtAcoplado.Text.Trim()))
                {
                    if (txtAcoplado.Text.Trim().Length > 7 || txtAcoplado.Text.Trim().Length < 6)
                        mensaje += "La patente del Acoplado debe tener los 6 o 7 caracteres, ej: BBB222<br>";
                    if (!Tools.ValidarPatente(txtAcoplado.Text.Trim().ToUpper()))
                        mensaje += "La patente del Acoplado tiene formato incorrecto, ej: AAA111<br>";
                }
            }

            if (mensaje.Length > 0)
            {
                btnSoloGuardar.Enabled = true;
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
                if (!(solicitud.ClientePagadorDelFlete != null && solicitud.ClientePagadorDelFlete.IdCliente > 0))
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

                        txtTarifa.Text = txtTarifa.Text.Trim().Replace(',', '.').Replace("$", "").Trim();
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

                        if (App.Usuario.IdGrupoEmpresa == App.ID_GRUPO_CRESUD) //Valido patente solo cresud
                        {
                            if (txtPatente.Text.Trim().Length > 7 || txtPatente.Text.Trim().Length < 6)
                            {
                                mensaje = "La patente del Camión no puede superar los 6 o 7 caracteres, ej: AAA111 o AA111AA<br>";
                                lblMensaje.Text += mensaje;
                            }
                            if (txtAcoplado.Text.Trim().Length > 7 || txtAcoplado.Text.Trim().Length < 6)
                            {
                                mensaje = "La patente del Acoplado no puede superar los 6 caracteres, ej: AAA111 o AA111AA<br>";
                                lblMensaje.Text += mensaje;
                            }
                            if (!Tools.ValidarPatente(txtPatente.Text.Trim().ToUpper()))
                            {
                                mensaje = "La patente del Camión tiene formato incorrecto, ej: AAA111 o AA111AA<br>";
                                lblMensaje.Text += mensaje;
                            }
                            if (!Tools.ValidarPatente(txtAcoplado.Text.Trim().ToUpper()))
                            {
                                mensaje = "La patente del Acoplado tiene formato incorrecto, ej: AAA111 o AA111AA<br>";
                                lblMensaje.Text += mensaje;
                            }
                        }

                        if (String.IsNullOrEmpty(txtKmRecorridos.Text.Trim()))
                        {
                            mensaje += "Debe completar los Kilómetros a recorrer.<br>";
                        }
                        if (App.Usuario.IdGrupoEmpresa == App.ID_GRUPO_CRESUD) //Valido patente solo cresud
                        {
                            if (String.IsNullOrEmpty(txtCantHoras.Text.Trim()))
                            {
                                mensaje += "Debe completar la Cantidad de Horas.<br>";
                            }
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
            else
            {

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

                if (App.Usuario.IdGrupoEmpresa == App.ID_GRUPO_CRESUD) //Valido patente solo cresud
                {
                    if (!String.IsNullOrEmpty(txtPatente.Text.Trim()))
                    {
                        if (txtPatente.Text.Trim().Length > 7 || txtPatente.Text.Trim().Length < 6)
                            mensaje += "La patente del Camión debe tener los 6 o 7 caracteres, ej: AAA111 o AA111AA<br>";
                        if (!Tools.ValidarPatente(txtPatente.Text.Trim().ToUpper()))
                            mensaje += "La patente del Camión tiene formato incorrecto, ej: AAA111 o AA111AA<br>";
                    }
                    if (!String.IsNullOrEmpty(txtAcoplado.Text.Trim()))
                    {
                        if (txtAcoplado.Text.Trim().Length > 7 || txtAcoplado.Text.Trim().Length < 6)
                            mensaje += "La patente del Acoplado debe tener los 6 caracteres, ej: AAA111 o AA111AA<br>";
                        if (!Tools.ValidarPatente(txtAcoplado.Text.Trim().ToUpper()))
                            mensaje += "La patente del Acoplado tiene formato incorrecto, ej: AAA111 o AA111AA<br>";
                    }
                }

                if (String.IsNullOrEmpty(txtKmRecorridos.Text.Trim()))
                {
                    mensaje += "Debe completar los Kilómetros a recorrer.<br>";
                }
                if (App.Usuario.IdGrupoEmpresa == App.ID_GRUPO_CRESUD) //Valido patente solo cresud
                {
                    if (String.IsNullOrEmpty(txtCantHoras.Text.Trim()))
                    {
                        mensaje += "Debe completar la Cantidad de Horas.<br>";
                    }
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
                btnGuardar.Enabled = true;
                btnGuardarCambio.Enabled = false;
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
                             "'><IMG border='0' src='Content/Images/icon_select.gif'></a>";

                row.Cells.Add(AddCell(link, string.Empty, HorizontalAlign.Center));

                tblBuscadorEmpresa.Rows.Add(row);

            }

        }
        private void CargarClientesUsados()
        {
            CargarTitulosBuscadorCliente();
            var cliente = ClienteDAO.Instance.GetUsados(hbBuscador.Value);

            if (App.Usuario.Empresa.Descripcion.ToUpper().Contains("CRESUD"))
                cliente = cliente.Where(c => !c.IdCliente.ToString().StartsWith("3")).ToList();

            foreach (Cliente dato in cliente)
            {
                var row = new TableRow();
                row.CssClass = "TableRow";
                string link = "<a href='#' onClick='BuscadorManager(" + dato.IdCliente.ToString() + ",\"" + hbBuscador.Value + "\",\"" + dato.RazonSocial + "\",\"" + dato.Cuit + "\")" +
                             "'><IMG border='0' src='Content/Images/icon_select.gif'></a>";


                if (hbBuscador.Value.Equals("ClientePagadorDelFlete"))
                {
                    if (esFleteAPagarMarcado())
                    {
                        if (dato.IdCliente.ToString().Equals("1000005"))
                        {
                            row.Cells.Add(AddCell(dato.RazonSocial, dato.RazonSocial, HorizontalAlign.Justify));
                            row.Cells.Add(AddCell(dato.Cuit, dato.Cuit, HorizontalAlign.Justify));
                            row.Cells.Add(AddCell(dato.IdCliente.ToString(), dato.IdCliente.ToString(), HorizontalAlign.Justify));

                            row.Cells.Add(AddCell(link, string.Empty, HorizontalAlign.Center));
                        }
                    }
                    else
                    {
                        if (!dato.IdCliente.ToString().Equals("1000005"))
                        {
                            row.Cells.Add(AddCell(dato.RazonSocial, dato.RazonSocial, HorizontalAlign.Justify));
                            row.Cells.Add(AddCell(dato.Cuit, dato.Cuit, HorizontalAlign.Justify));
                            row.Cells.Add(AddCell(dato.IdCliente.ToString(), dato.IdCliente.ToString(), HorizontalAlign.Justify));

                            row.Cells.Add(AddCell(link, string.Empty, HorizontalAlign.Center));
                        }
                    }

                }
                else
                {
                    row.Cells.Add(AddCell(dato.RazonSocial, dato.RazonSocial, HorizontalAlign.Justify));
                    row.Cells.Add(AddCell(dato.Cuit, dato.Cuit, HorizontalAlign.Justify));
                    row.Cells.Add(AddCell(dato.IdCliente.ToString(), dato.IdCliente.ToString(), HorizontalAlign.Justify));

                    row.Cells.Add(AddCell(link, string.Empty, HorizontalAlign.Center));
                }

                tblBuscadorCliente.Rows.Add(row);

            }


        }
        private bool esFleteAPagarMarcado()
        {

            foreach (ListItem li in rblFletePagadoAPagar.Items)
            {
                if (li.Selected)
                {
                    if (li.Text.ToLower() == "flete a pagar")
                    {
                        return true;
                    }

                    if (li.Text.ToLower() == "flete pagado")
                    {
                        return false;
                    }
                }

            }

            return false;


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
                             "'><IMG border='0' src='Content/Images/icon_select.gif'></a>";

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
                             "'><IMG border='0' src='Content/Images/icon_select.gif'></a>";

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
            if (!String.IsNullOrEmpty(txtNumeroCDP.Text) && !String.IsNullOrEmpty(txtCtg.Text))
            {
                var ws = new wsAfip_v3();
                var resul = ws.anularCTG((long)ConvertToDecimal(txtNumeroCDP.Text), (long)ConvertToDecimal(txtCtg.Text));

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
                        Solicitud solicitudGuardada = SolicitudDAO.Instance.GetOne(Convert.ToInt32(idSolicitud));

                        solicitudGuardada.CodigoAnulacionAfip = resul.datosResponse.codigoOperacion;

                        string[] fecha = resul.datosResponse.fechaHora.Substring(0, 10).Split('/');
                        DateTime fechaCancelacion = new DateTime(Convert.ToInt32(fecha[2]), Convert.ToInt32(fecha[1]), Convert.ToInt32(fecha[0]));
                        solicitudGuardada.FechaAnulacionAfip = fechaCancelacion;
                        solicitudGuardada.EstadoEnAFIP = Enums.EstadoEnAFIP.Anulada;
                        solicitudGuardada.EstadoEnSAP = Enums.EstadoEnvioSAP.PedidoAnulacion;
                        solicitudGuardada.ObservacionAfip = "Carta de porte ANULADA";

                        SolicitudDAO.Instance.SaveOrUpdate(solicitudGuardada);

                        Response.Redirect("Index.aspx?Id=" + solicitudGuardada.IdSolicitud.ToString());
                        return;
                    }

                }
                else
                {

                    string idSolicitud = Request["id"];
                    Solicitud solicitudGuardada = SolicitudDAO.Instance.GetOne(Convert.ToInt32(idSolicitud));
                    solicitudGuardada.ObservacionAfip = lblMensaje.Text.Trim();

                    if (lblMensaje.Text.Contains("La Carta de Porte fue Confirmada o Anulada con anterioridad"))
                        solicitudGuardada.EstadoEnAFIP = Enums.EstadoEnAFIP.Anulada;

                    SolicitudDAO.Instance.SaveOrUpdate(solicitudGuardada);

                    Response.Redirect("Index.aspx?Id=" + solicitudGuardada.IdSolicitud.ToString());

                }

            }
            else if (!PaisDAO.Instance.GetOne(App.Usuario.IdPais).Descripcion.ToUpper().Contains("ARGENTINA"))
            {
                string idSolicitud = Request["id"];
                Solicitud solicitudGuardada = SolicitudDAO.Instance.GetOne(Convert.ToInt32(idSolicitud));
                solicitudGuardada.EstadoEnSAP = Enums.EstadoEnvioSAP.PedidoAnulacion;
                solicitudGuardada.ObservacionAfip = "Carta de porte ANULADA";
                SolicitudDAO.Instance.SaveOrUpdate(solicitudGuardada);
                Response.Redirect("Index.aspx?Id=" + solicitudGuardada.IdSolicitud.ToString());
                return;
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
                btnImprimir.Visible = false;
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
                    txtPesoNeto.Text = (ConvertToDecimal(txtPesoBruto.Text.Trim()) - ConvertToDecimal(txtPesoTara.Text.Trim())).ToString();
                }

            }


        }
        protected void txtPesoBruto_TextChanged(object sender, EventArgs e)
        {

            if (isNumericValidador(txtPesoBruto.Text.Trim()))
            {
                if (isNumericValidador(txtPesoTara.Text.Trim()))
                {
                    txtPesoNeto.Text = (ConvertToDecimal(txtPesoBruto.Text.Trim()) - ConvertToDecimal(txtPesoTara.Text.Trim())).ToString();
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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            this.GuardarYEnviar();
        }

        protected void cboTipoDeCarta_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCtgManual.Visible = false;
            txtCtg.Visible = true;
            txtNumeroCDPManual.Visible = false;
            txtNumeroCDP.Visible = true;
            txtNumeroCEEManual.Visible = false;
            txtNumeroCEE.Visible = true;
            lblFechaVencimiento.Visible = false;
            txtFechaVencimiento.Visible = false;
            txtFechaDeEmision.Enabled = false;
            txtTarifaReferencia.Enabled = false;
            //Compra de granos
            if (cboTipoDeCarta.SelectedItem.Text.Equals("Compra de granos"))
            {
                if (Session["activarModelo"] == null)
                {
                    if (!String.IsNullOrEmpty(txtCtg.Text.Trim()))
                    {
                        btnSoloGuardar.Enabled = false;
                        btnGuardar.Enabled = false;
                    }
                    else
                    {
                        btnSoloGuardar.Enabled = true;
                        btnGuardar.Enabled = true;
                    }
                }
                btnGuardar.Enabled = false;
                btnSoloGuardar.Enabled = true;
            }
            if (cboTipoDeCarta.SelectedItem.Text.Equals("Venta de granos de terceros") ||
                cboTipoDeCarta.SelectedItem.Text.Equals("Terceros por venta  de Granos de producción propia") ||
                cboTipoDeCarta.SelectedItem.Text.Equals("Compra de granos"))
            {
                btnAnular.Visible = false;
                //btnModelo.Visible = false;
                btnGuardar.Visible = App.UsuarioTienePermisos("Alta Solicitud");
                btnGuardar.Enabled = App.UsuarioTienePermisos("Alta Solicitud");

                txtCtgManual.Visible = true;
                txtCtg.Visible = false;
                txtNumeroCDPManual.Visible = true;
                txtNumeroCDP.Visible = false;
                txtNumeroCEEManual.Visible = true;
                txtNumeroCEE.Visible = false;
                lblFechaVencimiento.Visible = true;
                txtFechaVencimiento.Visible = true;
                txtFechaDeEmision.Enabled = true;
                txtTarifaReferencia.Enabled = true;

                txtFechaDeEmision.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtFechaVencimiento.Text = DateTime.Today.ToString("dd/MM/yyyy");
            }
            if (lblMensaje.Text.Contains("Reserva"))
            {
                btnAnular.Visible = false;
                btnModelo.Visible = false;
                btnGuardar.Visible = false;
                txtCtgManual.Visible = true;
                txtCtg.Visible = false;
                lblFechaVencimiento.Visible = true;
                txtFechaVencimiento.Visible = true;
                txtFechaDeEmision.Enabled = true;
                txtTarifaReferencia.Enabled = true;
            }

            string pais = PaisDAO.Instance.GetOne(App.Usuario.IdPais).Descripcion;

            if (!pais.ToUpper().Contains("ARGENTINA"))
            {
                txtCtg.Visible = false;
                txtCtgManual.Visible = false;
                lblCtg.Visible = false;
                if (!pais.ToUpper().Contains("PARAGUAY"))
                {
                    Label18.Visible = false;
                    txtNumeroCEE.Visible = false;
                    txtNumeroCEEManual.Visible = false;
                    txtNumeroCEE.Visible = false;
                    txtNumeroCEEManual.Visible = false;
                }
            }
        }

        protected void btnSoloGuardar_Click(object sender, EventArgs e)
        {
            this.GuardarSolo();
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
        protected void btnConfirmarArribo_Click(object sender, EventArgs e)
        {
            if (Request["id"] != null)
            {
                string idSolicitud = Request["id"];
                Solicitud solicitud = SolicitudDAO.Instance.GetOne(Convert.ToInt32(idSolicitud));
                lblMensaje.Text = string.Empty;

                if (solicitud != null)
                {
                    var ws = new wsAfip_v3();
                    var resul = ws.confirmarArribo(solicitud);
                    if (resul.arrayErrores.Count() > 0)
                    {
                        foreach (String mensaje in resul.arrayErrores)
                        {
                            lblMensaje.Text += mensaje + "<br/>";
                        }

                    }

                    if (lblMensaje.Text.Trim().Length > 0)
                    {
                        if (lblMensaje.Text.Contains("confirmado"))
                            solicitud.EstadoEnAFIP = Enums.EstadoEnAFIP.Confirmado;

                        if (lblMensaje.Text.Contains("definitivo"))
                            solicitud.EstadoEnAFIP = Enums.EstadoEnAFIP.ConfirmadoDefinitivo;

                        solicitud.ObservacionAfip = lblMensaje.Text.Trim();
                    }
                    else
                    {
                        solicitud.CodigoAnulacionAfip = resul.datosResponse.codigoOperacion;
                        solicitud.ObservacionAfip = "CTG Confirmado Manual";
                        solicitud.EstadoEnAFIP = Enums.EstadoEnAFIP.Confirmado;
                    }

                    SolicitudDAO.Instance.SaveOrUpdate(solicitud);




                }

                Response.Redirect("Index.aspx?Id=" + Request["id"].ToString());

            }
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

            BuscadorCliente.Visible = App.UsuarioTienePermisos("Alta Solicitud");
            ImageButtonDeleteDestinatarioCambio.Visible = App.UsuarioTienePermisos("Alta Solicitud");
            CargarClientesUsados();

        }
        protected void btnGuardarCambio_Click(object sender, EventArgs e)
        {
            //string testCambioDestino = System.Configuration.ConfigurationSettings.AppSettings["testCambioDestino"];
            //if (testCambioDestino != null && testCambioDestino.ToLower().Equals("true"))
            //{
            //    if (Request["id"] != null)
            //    {
            //        string idSolicitud = Request["id"];
            //        Solicitud sol = SolicitudDAO.Instance.GetOne(Convert.ToInt32(idSolicitud));

            //        if (ValidacionesCambioDestino())
            //        {
            //            //Jira MDS-973
            //            //Si quiere guardar la carta y un establecimiento esta dado de baja lo notifica
            //            if (ValidarEstablecimiento())
            //                return;

            //            if (!String.IsNullOrEmpty(hbClienteDestinatarioCambio.Value))
            //                sol.ClienteDestinatarioCambio = ClienteDAO.Instance.GetOne(Convert.ToInt32(hbClienteDestinatarioCambio.Value));
            //            else
            //            {
            //                sol.ClienteDestinatarioCambio = new Cliente();
            //            }

            //            sol.IdEstablecimientoDestinoCambio = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(cboIdEstablecimientoDestinoCambio.SelectedValue));

            //            int SolicitudID = SolicitudDAO.Instance.SaveOrUpdate(sol);
            //            if (SolicitudID > 0)
            //            {
            //                Solicitud solicitudGuardada = SolicitudDAO.Instance.GetOne(SolicitudID);
            //                solicitudGuardada.EstadoEnAFIP = Enums.EstadoEnAFIP.Enviado;
            //                solicitudGuardada.EstadoEnAFIP = Enums.EstadoEnAFIP.CambioDestino;

            //                EnvioMailDAO.Instance.sendMail("<b>Guardar cambios</b> <br/><br/>" + lblMensaje.Text + "<br/>" + "Carta De Porte: " + solicitudGuardada.NumeroCartaDePorte + "<br/>" + "Usuario: " + solicitudGuardada.UsuarioCreacion);

            //                solicitudGuardada.ObservacionAfip = lblMensaje.Text;
            //                SolicitudDAO.Instance.SaveOrUpdate(solicitudGuardada);

            //                // Envio Anulacion a SAP
            //                // Cuando sap responde la anulacion envio el alta desde el Web Service
            //                wsSAP wssap = new wsSAP();
            //                wssap.PrefacturaSAP(solicitudGuardada, true, false);

            //                Response.Redirect("Index.aspx?Id=" + solicitudGuardada.IdSolicitud.ToString());
            //            }

            //        }
            //    }
            //}
            //else
            //{
            if (Request["id"] != null)
            {
                string idSolicitud = Request["id"];
                Solicitud sol = SolicitudDAO.Instance.GetOne(Convert.ToInt32(idSolicitud));

                if (ValidacionesCambioDestino())
                {
                    //Jira MDS-973
                    //Si quiere guardar la carta y un establecimiento esta dado de baja lo notifica
                    if (ValidarEstablecimiento())
                        return;

                    if (!String.IsNullOrEmpty(hbClienteDestinatarioCambio.Value))
                        sol.ClienteDestinatarioCambio = ClienteDAO.Instance.GetOne(Convert.ToInt32(hbClienteDestinatarioCambio.Value));
                    else
                        sol.ClienteDestinatarioCambio = new Cliente();
                    
                    sol.IdEstablecimientoDestinoCambio = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(cboIdEstablecimientoDestinoCambio.SelectedValue));

                    int SolicitudID = SolicitudDAO.Instance.SaveOrUpdate(sol);
                    if (SolicitudID > 0)
                    {
                        //Hago el pedido de Cambio de destino a la afip
                        var ws = new wsAfip_v3();
                        var resul = ws.cambiarDestinoDestinatarioCTGRechazado(sol);

                        Solicitud solicitudGuardada = SolicitudDAO.Instance.GetOne(SolicitudID);
                        solicitudGuardada.EstadoEnAFIP = Enums.EstadoEnAFIP.Enviado;

                        if (resul.arrayErrores != null && resul.arrayErrores.Length > 0)
                        {
                            lblMensaje.ForeColor = Color.Red;
                            lblMensaje.Text = "ERRORES CAMBIO DE DESTINO en AFIP: <br>";
                            foreach (String errores in resul.arrayErrores)
                            {
                                lblMensaje.Text += errores + "<br>";
                            }
                            solicitudGuardada.EstadoEnAFIP = Enums.EstadoEnAFIP.SinProcesar;
                            SolicitudDAO.Instance.SaveOrUpdate(solicitudGuardada);
                            return;
                        }

                        if (resul.datosResponse != null)
                        {
                            lblMensaje.ForeColor = Color.Black;
                            if (!String.IsNullOrEmpty(resul.datosResponse.fechaHora))
                            {
                                btnImprimir.Visible = false;
                                solicitudGuardada.EstadoEnAFIP = Enums.EstadoEnAFIP.CambioDestino;
                                lblMensaje.Text = "Cambio de Destino realizado";
                            }
                        }

                        EnvioMailDAO.Instance.sendMail("<b>Guardar cambios</b> <br/><br/>" + lblMensaje.Text + "<br/>" + "Carta De Porte: " + solicitudGuardada.NumeroCartaDePorte + "<br/>" + "Usuario: " + solicitudGuardada.UsuarioCreacion);

                        solicitudGuardada.ObservacionAfip = lblMensaje.Text;
                        SolicitudDAO.Instance.SaveOrUpdate(solicitudGuardada);

                        // Envio Anulacion a SAP
                        // Cuando sap responde la anulacion envio el alta desde el Web Service
                        wsSAP wssap = new wsSAP();
                        wssap.PrefacturaSAP(solicitudGuardada, true, false);

                        Response.Redirect("Index.aspx?Id=" + solicitudGuardada.IdSolicitud.ToString());
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
        private bool ChequeoIntegridadPagadorVsTransportista()
        {

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
                else
                {
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

            return resul;

        }
        protected void btnCerrarServiceUnavailable_Click(object sender, EventArgs e)
        {
            this.divServiceUnavailable.Visible = false;

        }
        protected void rblFletePagadoAPagar_SelectedIndexChanged(object sender, EventArgs e)
        {

            //string opcion = ((RadioButtonList)sender).Text;

            //if (opcion.ToLower().Equals("flete a pagar"))
            //{
            //    Cliente cli = ClienteDAO.Instance.GetOne(1000005);
            //    if (cli != null && cli.IdCliente > 0)
            //    {
            //        cboClientePagadorDelFlete.Text = cli.RazonSocial;
            //        hbClientePagadorDelFlete.Value = cli.IdCliente.ToString();
            //        ImageButtonDeletePagadorFlete.Visible = App.UsuarioTienePermisos("Alta Solicitud");
            //    }
            //}
            //else
            //{
            //    cboClientePagadorDelFlete.Text = string.Empty;
            //    hbClientePagadorDelFlete.Value = string.Empty;
            //    ImageButtonDeletePagadorFlete.Visible = false;
            //}


        }


        private decimal ConvertToDecimal(object value)
        {
            return Tools.ConvertToDecimal(value);
        }

    }
}


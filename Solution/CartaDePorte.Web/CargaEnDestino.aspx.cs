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
    public partial class CargaEnDestino : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Main master = (Main)Page.Master;
            master.HiddenValue = "Recibidas";
            

            
            if (!App.UsuarioTienePermisos("Carga en Destino"))
            {
                Response.Redirect("~/SinAutorizacion.aspx");
                return;
            }


            DateTime FE;
            DateTime FD;
            DateTime FA;

            string fe = Request.Form[txtFechaDeEmision.UniqueID];
            string fd = Request.Form[txtFechaDeDescarga.UniqueID];
            string fa = Request.Form[txtFechaDeArribo.UniqueID];

            if (!String.IsNullOrEmpty(fe))
            {
                string[] fechaemision = fe.Trim().Substring(0, 10).Split('/');
                FE = new DateTime(Convert.ToInt32(fechaemision[2]), Convert.ToInt32(fechaemision[1]), Convert.ToInt32(fechaemision[0]));
            }

            if (!String.IsNullOrEmpty(fd))
            {
                string[] fechadescarga = fd.Trim().Substring(0, 10).Split('/');
                FD = new DateTime(Convert.ToInt32(fechadescarga[2]), Convert.ToInt32(fechadescarga[1]), Convert.ToInt32(fechadescarga[0]));
            }

            if (!String.IsNullOrEmpty(fa))
            {
                string[] fechaarribo = fa.Trim().Substring(0, 10).Split('/');
                FA = new DateTime(Convert.ToInt32(fechaarribo[2]), Convert.ToInt32(fechaarribo[1]), Convert.ToInt32(fechaarribo[0]));
            }

            if (Request["id"] != null)
            {
                if (Request["id"].ToString() == "0")
                {
                    Response.Redirect("CargaEnDestino.aspx");
                }
            }

            if (!IsPostBack)
            {
                CargarCombos();

                if (Request["id"] != null)
                {
                    string idSolicitud = Request["id"];
                    SolicitudRecibida sol = SolicitudRecibidaDAO.Instance.GetOne(Convert.ToInt32(idSolicitud));

                    CargarSolicitud(sol);

                }

                
            }


        }
        private void CargarSolicitud(SolicitudRecibida sol)
        {

            cboTipoDeCartam.SelectedValue = Convert.ToInt32(sol.TipoDeCarta).ToString();
            cboTipoDeCartam.Enabled = false;


            txtNumeroCDPManual.Text = sol.NumeroCartaDePorte;
            txtNumeroCEEManual.Text = sol.Cee;
            txtCtgManual.Text = sol.Ctg;
            txtFechaDeEmision.Text = sol.FechaDeEmision.Value.ToString("dd/MM/yyyy");
            txtCuitProveedorTitularCartaDePorte.Text = sol.CuitProveedorTitularCartaDePorte;
            txtCuitClienteIntermediario.Text = sol.CuitClienteIntermediario;
            txtCuitClienteRemitenteComercial.Text = sol.CuitClienteRemitenteComercial;
            txtCuitClienteCorredor.Text = sol.CuitClienteCorredor;
            txtCuitClienteEntregador.Text = sol.CuitClienteEntregador;
            txtCuitClienteDestinatario.Text = sol.CuitClienteDestinatario;
            txtCuitClienteDestino.Text = sol.CuitClienteDestino;
            txtCuitProveedorTransportista.Text = sol.CuitProveedorTransportista;
            txtCuitChofer.Text = sol.CuitChofer;

            cboGrano.SelectedValue = sol.Grano.IdGrano.ToString();
            txtTipoGrano.Text = (sol.Grano.TipoGrano != null) ? sol.Grano.TipoGrano.Descripcion : string.Empty;
            txtCosecha.Text = (sol.Grano.CosechaAfip != null) ? sol.Grano.CosechaAfip.Descripcion : string.Empty;


            rbCargaPesadaDestino.Checked = sol.CargaPesadaDestino;

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
            txtPesoBruto.Text = sol.PesoBruto.ToString();
            txtPesoTara.Text = sol.PesoTara.ToString();
            txtPesoNeto.Text = (sol.PesoBruto - sol.PesoTara).ToString();
            txtContratoNro.Text = sol.NumeroContrato.ToString();
            txtObsevaciones.Text = sol.Observaciones;

            txtCodigoEstablecimientoProcedencia.Text = sol.CodigoEstablecimientoProcedencia;
            txtLocalidad1.Text = LocalidadDAO.Instance.GetOne(sol.IdLocalidadEstablecimientoProcedencia).ToString();
            txtKrgsEstimados.Text = (sol.KilogramosEstimados == 0) ? string.Empty : sol.KilogramosEstimados.ToString();
            txtPatente.Text = sol.PatenteCamion;
            txtAcoplado.Text = sol.PatenteAcoplado;
            txtKmRecorridos.Text = sol.KmRecorridos.ToString();
            txtTarifaReferencia.Text = sol.TarifaReferencia.ToString();
            txtTarifa.Text = sol.TarifaReal.ToString();
            txtCantHoras.Text = sol.CantHoras.ToString();

            txtCódigoEstablecimientoDestinoCambio.Text = sol.CodigoEstablecimientoDestinoCambio;
            txtCódigoEstablecimientoDestinoCambio.Text = sol.CuitEstablecimientoDestinoCambio;

            if (!String.IsNullOrEmpty(sol.IdLocalidadEstablecimientoDestinoCambio.ToString()) && sol.IdLocalidadEstablecimientoDestinoCambio > 0)
            {
                this.txtLocalidad2.Text = LocalidadDAO.Instance.GetOne(sol.IdLocalidadEstablecimientoDestinoCambio).ToString();
            }
            txtFechaDeDescarga.Text = (sol.FechaDeDescarga.HasValue) ? sol.FechaDeDescarga.Value.ToString("dd/MM/yyyy") : "";
            txtFechaDeArribo.Text = (sol.FechaDeArribo.HasValue) ? sol.FechaDeArribo.Value.ToString("dd/MM/yyyy") : "";
            txtPesoNetoDeDescarga.Text = (sol.PesoNetoDescarga.HasValue) ? sol.PesoNetoDescarga.Value.ToString() : "0";


        
        }

        private void CargarCombos()
        {
            popGrano();
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
        private void popTipoDeCarta()
        {
            ListItem li;
            li = new ListItem();
            li.Value = "-1";
            li.Text = "[seleccione...]";
            cboTipoDeCartam.Items.Add(li);
            
            foreach (Enums.TipoCartaDePorteRecibida item in Enum.GetValues(typeof(Enums.TipoCartaDePorteRecibida)))
            {
                li = new ListItem();
                li.Value = Convert.ToInt32(item).ToString();
                li.Text = splitCapitalizacion(item.ToString());
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

        private bool ValidacionesCambioDestino()
        {
            lblMensaje.Text = string.Empty;
            string mensaje = string.Empty;


            if (mensaje.Length > 0)
            {
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
                mensaje += "La cantidad de Kgrs Estimados debe ser un Valor Numérico.<br>";
            }

            if (!isNumeric(txtContratoNro.Text.Trim()))
            {
                mensaje += "El contrato debe ser un valor Numérico.<br>";
            }

            if (String.IsNullOrEmpty(this.txtPesoNetoDeDescarga.Text.Trim()))
            {
                mensaje += "Debe completar el Peso neto de descarga.<br>";
            }
            else {

                if (!isNumeric(txtPesoNetoDeDescarga.Text.Trim()))
                {
                    mensaje += "El Peso neto de descarga debe ser un valor Numérico.<br>";
                }
            }


            if (!isNumeric(txtPesoBruto.Text.Trim()))
            {
                mensaje += "El peso bruto debe ser un valor Numérico<br>";
            }

            if (!isNumeric(txtPesoTara.Text.Trim()))
            {
                mensaje += "El peso tara debe ser un valor Numérico<br>";
            }


            if (cboTipoDeCartam.SelectedIndex < 1)
            {
                mensaje += "Debe seleccionar un Tipo de Carta de Porte<br>";
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

            if (String.IsNullOrEmpty(txtNumeroCEEManual.Text.Trim()))
            {
                mensaje += "Debe completar El numero de CEE.<br>";
            }

            if (String.IsNullOrEmpty(txtCodigoEstablecimientoProcedencia.Text.Trim()))
            {
                mensaje += "Debe completar el codigo de establecimiento de Procedencia, en caso de no tenerlo ingrese 9999<br>";
            }
            
            if (txtCodigoEstablecimientoProcedencia.Text.Trim().Length > 6)
            {
                mensaje += "El codigo de establecimiento de Procedencia no debe tener mas de 6 digitos<br>";
            }
            if (!isNumeric(txtCodigoEstablecimientoProcedencia.Text))
            {
                mensaje += "El codigo de establecimiento de Procedencia debe ser solo numéricos<br>";
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

            //revalidar Cuits
            if (!String.IsNullOrEmpty(this.txtCuitProveedorTitularCartaDePorte.Text) && !this.CuitValido(this.txtCuitProveedorTitularCartaDePorte.Text))
            {
                mensaje += "Cuit Titular carta de porte no es valido<br>";
                this.txtCuitProveedorTitularCartaDePorte1.ForeColor = Color.Red;
            }else
                this.txtCuitProveedorTitularCartaDePorte1.ForeColor = Color.Green;

            if (!String.IsNullOrEmpty(this.txtCuitClienteIntermediario.Text) && !this.CuitValido(this.txtCuitClienteIntermediario.Text))
            {
                mensaje += "Cuit Intermediario no es valido<br>";
                this.txtCuitClienteIntermediario1.ForeColor = Color.Red;
            }
            else
                this.txtCuitClienteIntermediario1.ForeColor = Color.Green;

            if (!String.IsNullOrEmpty(this.txtCuitClienteRemitenteComercial.Text) && !this.CuitValido(this.txtCuitClienteRemitenteComercial.Text))
            {
                mensaje += "Cuit RemitenteComercial no es valido<br>";
                this.txtCuitClienteRemitenteComercial1.ForeColor = Color.Red;
            }
            else
                this.txtCuitClienteRemitenteComercial1.ForeColor = Color.Green;

            if (!String.IsNullOrEmpty(this.txtCuitClienteCorredor.Text) && !this.CuitValido(this.txtCuitClienteCorredor.Text))
            {
                mensaje += "Cuit Corredor no es valido<br>";
                this.txtCuitClienteCorredor1.ForeColor = Color.Red;
            }
            else
                this.txtCuitClienteCorredor1.ForeColor = Color.Green;

            if (!String.IsNullOrEmpty(this.txtCuitClienteEntregador.Text) && !this.CuitValido(this.txtCuitClienteEntregador.Text))
            {
                mensaje += "Cuit Entregador no es valido<br>";
                this.txtCuitClienteEntregador1.ForeColor = Color.Red;
            }
            else
                this.txtCuitClienteEntregador1.ForeColor = Color.Green;


            if (!String.IsNullOrEmpty(this.txtCuitClienteDestinatario.Text) && !this.CuitValido(this.txtCuitClienteDestinatario.Text))
            {
                mensaje += "Cuit Destinatario no es valido<br>";
                this.txtCuitClienteDestinatario1.ForeColor = Color.Red;
            }
            else
                this.txtCuitClienteDestinatario1.ForeColor = Color.Green;

            if (!String.IsNullOrEmpty(this.txtCuitClienteDestino.Text) && !this.CuitValido(this.txtCuitClienteDestino.Text))
            {
                mensaje += "Cuit Destino no es valido<br>";
                this.txtCuitClienteDestino1.ForeColor = Color.Red;
            }
            else
                this.txtCuitClienteDestino1.ForeColor = Color.Green;

            if (!String.IsNullOrEmpty(this.txtCuitProveedorTransportista.Text) && !this.CuitValido(this.txtCuitProveedorTransportista.Text))
            {
                mensaje += "Cuit Transportista no es valido<br>";
                this.txtCuitProveedorTransportista1.ForeColor = Color.Red;
            }
            else
                this.txtCuitProveedorTransportista1.ForeColor = Color.Green;

            if (!String.IsNullOrEmpty(this.txtCuitChofer.Text) && !this.CuitValido(this.txtCuitChofer.Text))
            {
                mensaje += "Cuit Chofer no es valido<br>";
                this.txtCuitChofer1.ForeColor = Color.Red;
            }
            else
                this.txtCuitChofer1.ForeColor = Color.Green;


            if (!String.IsNullOrEmpty(this.txtCuitDestinoCambio.Text) && !this.CuitValido(this.txtCuitDestinoCambio.Text))
            {
                mensaje += "Cuit DestinoCambio no es valido<br>";
                this.txtCuitDestinoCambio1.ForeColor = Color.Red;
            }
            else
                this.txtCuitDestinoCambio1.ForeColor = Color.Green;


            //revalidar Localidades
            IList<Localidad> loc1 = LocalidadDAO.Instance.GetLocalidadByText(this.txtLocalidad1.Text.Trim());
            if (loc1.Count == 0)
            {
                mensaje += "La localidad de procedencia no es valida.<br>";
            }
            else {

                if (loc1[0].ToString() != this.txtLocalidad1.Text.Trim())
                {
                    mensaje += "La localidad de procedencia no es valida.<br>";
                }            
            }
            
            if (!String.IsNullOrEmpty(this.txtLocalidad2.Text.Trim()))
            {
                IList<Localidad> loc2 = LocalidadDAO.Instance.GetLocalidadByText(this.txtLocalidad2.Text.Trim());
                if (loc2.Count == 0)
                {
                    mensaje += "La localidad de cambio de domicilio no es valida.<br>";
                }
                else
                {

                    if (loc2[0].ToString() != this.txtLocalidad1.Text.Trim())
                    {
                        mensaje += "La localidad de cambio de domicilio no es valida.<br>";
                    }
                }
            }


            string fe = Request.Form[txtFechaDeEmision.UniqueID];
            string fd = Request.Form[txtFechaDeDescarga.UniqueID];
            string fa = Request.Form[txtFechaDeArribo.UniqueID];


            if (String.IsNullOrEmpty(fe))
            {
                mensaje += "Debe completar la fecha de emisión.<br>";
            }

            if (String.IsNullOrEmpty(fd))
            {
                mensaje += "Debe completar la fecha de descarga.<br>";
            }

            if (String.IsNullOrEmpty(fa))
            {
                mensaje += "Debe completar la fecha de arribo.<br>";
            }
            if (String.IsNullOrEmpty(this.txtCantHoras.Text.Trim()))
            {
                mensaje += "Debe completar la cantidad de horas.<br>";
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
        private bool ValidarLogicaPorTipoCartaDePorte(Solicitud solicitud)
        {

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

        protected void txtCuit_TextChanged(object sender, EventArgs e)
        {
            String id = ((TextBox)sender).ID + "1";
            Control labelControlAlert = tableCarga1.FindControl(id);
            string cuitString = ((TextBox)sender).Text;

            ((Label)labelControlAlert).Attributes["style"] = "display:visible";
            ((Label)labelControlAlert).ForeColor = Color.Green;

            if (!CuitValido(cuitString))
            {
                ((Label)labelControlAlert).ForeColor = Color.Red;
            }

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
        public String Localidades()
        {
            String result = string.Empty;

            foreach (Localidad row in LocalidadDAO.Instance.GetAll())
            {
                result += "'" + row.ToString() + "',";

            }
            return result;

        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (this.Validaciones())
            {
                // Guardo la solicitud 

                SolicitudRecibida solicitud = new SolicitudRecibida();
                if (Request["id"] != null)
                {
                    string idSolicitud = Request["id"];
                    solicitud = SolicitudRecibidaDAO.Instance.GetOne(Convert.ToInt32(idSolicitud));
                }

                DateTime FE = new DateTime();
                DateTime FD = new DateTime();
                DateTime FA = new DateTime();

                string fe = Request.Form[txtFechaDeEmision.UniqueID];
                string fd = Request.Form[txtFechaDeDescarga.UniqueID];
                string fa = Request.Form[txtFechaDeArribo.UniqueID];

                if (!String.IsNullOrEmpty(fe))
                {
                    string[] fechaemision = fe.Trim().Substring(0, 10).Split('/');
                    FE = new DateTime(Convert.ToInt32(fechaemision[2]), Convert.ToInt32(fechaemision[1]), Convert.ToInt32(fechaemision[0]));
                }

                if (!String.IsNullOrEmpty(fd))
                {
                    string[] fechadescarga = fd.Trim().Substring(0, 10).Split('/');
                    FD = new DateTime(Convert.ToInt32(fechadescarga[2]), Convert.ToInt32(fechadescarga[1]), Convert.ToInt32(fechadescarga[0]));
                }

                if (!String.IsNullOrEmpty(fa))
                {
                    string[] fechaarribo = fa.Trim().Substring(0, 10).Split('/');
                    FA = new DateTime(Convert.ToInt32(fechaarribo[2]), Convert.ToInt32(fechaarribo[1]), Convert.ToInt32(fechaarribo[0]));
                }


                solicitud.TipoDeCarta = (Enums.TipoCartaDePorteRecibida)Convert.ToInt32(cboTipoDeCartam.SelectedValue);
                solicitud.NumeroCartaDePorte = txtNumeroCDPManual.Text.Trim();
                solicitud.Cee = txtNumeroCEEManual.Text.Trim();
                solicitud.Ctg = txtCtgManual.Text.Trim();
                solicitud.FechaDeEmision = FE;
                solicitud.CuitProveedorTitularCartaDePorte = txtCuitProveedorTitularCartaDePorte.Text.Trim();
                solicitud.CuitClienteIntermediario = txtCuitClienteIntermediario.Text.Trim();
                solicitud.CuitClienteRemitenteComercial = txtCuitClienteRemitenteComercial.Text.Trim();
                solicitud.CuitClienteCorredor = txtCuitClienteCorredor.Text.Trim();
                solicitud.CuitClienteEntregador = txtCuitClienteEntregador.Text.Trim();
                solicitud.CuitClienteDestinatario = txtCuitClienteDestinatario.Text.Trim();
                solicitud.CuitClienteDestino = txtCuitClienteDestino.Text.Trim();
                solicitud.CuitProveedorTransportista = txtCuitProveedorTransportista.Text.Trim();
                solicitud.CuitChofer = txtCuitChofer.Text.Trim();

                solicitud.Grano = GranoDAO.Instance.GetOne(Convert.ToInt32(cboGrano.SelectedValue));
                
                if (!String.IsNullOrEmpty(txtKmRecorridos.Text.Trim()))
                    solicitud.KmRecorridos = (long)Convert.ToDecimal((txtKmRecorridos.Text.Trim()));

                if (!String.IsNullOrEmpty(txtContratoNro.Text.Trim()))
                    solicitud.NumeroContrato = Convert.ToInt32(txtContratoNro.Text);


                solicitud.Observaciones = txtObsevaciones.Text.Trim();
                solicitud.PatenteAcoplado = txtAcoplado.Text.Trim();
                solicitud.PatenteCamion = txtPatente.Text.Trim();

                solicitud.CargaPesadaDestino = rbCargaPesadaDestino.Checked;

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

                if (!String.IsNullOrEmpty(txtTarifaReferencia.Text))
                    solicitud.TarifaReferencia = Convert.ToDecimal((txtTarifaReferencia.Text.Trim().Replace("$", "").Trim()));

                if (!String.IsNullOrEmpty(txtTarifa.Text))
                    solicitud.TarifaReal = Convert.ToDecimal((txtTarifa.Text.Trim().Replace("$", "").Trim()));


                if (!String.IsNullOrEmpty(txtCantHoras.Text.Trim()))
                    solicitud.CantHoras = (long)Convert.ToDecimal(txtCantHoras.Text.Trim());


                solicitud.ConformeCondicional = getEnumCCValue();
                solicitud.EstadoFlete = getEnumEFValue();

                solicitud.CodigoEstablecimientoProcedencia = txtCodigoEstablecimientoProcedencia.Text.Trim();
                solicitud.IdLocalidadEstablecimientoProcedencia = LocalidadDAO.Instance.GetLocalidadByText(this.txtLocalidad1.Text.Trim())[0].Codigo;

                solicitud.CodigoEstablecimientoDestinoCambio = txtCódigoEstablecimientoDestinoCambio.Text.Trim();
                solicitud.CuitEstablecimientoDestinoCambio = txtCódigoEstablecimientoDestinoCambio.Text.Trim();

                if (!String.IsNullOrEmpty(this.txtLocalidad2.Text.Trim()))
                {
                    solicitud.IdLocalidadEstablecimientoDestinoCambio = LocalidadDAO.Instance.GetLocalidadByText(this.txtLocalidad2.Text.Trim())[0].Codigo;
                }

                solicitud.FechaDeDescarga = FD;
                solicitud.FechaDeArribo = FA;
                solicitud.PesoNetoDescarga = Convert.ToInt32(txtPesoNetoDeDescarga.Text.Trim());

                solicitud.UsuarioCreacion = App.Usuario.Nombre;
                solicitud.UsuarioModificacion = App.Usuario.Nombre;

                SolicitudRecibidaDAO.Instance.SaveOrUpdate(solicitud);

                lblMensaje.Text = "La solicitud fue guardada exitósamente.";
            
            }



        }


    }
}

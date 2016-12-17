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
using Newtonsoft.Json.Linq;
using CartaDePorte.Core.Domain.Seguridad;
using CartaDePorte.Core;

namespace CartaDePorte.Web
{
    public partial class CargaC1116A : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Main master = (Main)Page.Master;
            master.HiddenValue = "C1116A";
            
            
            if (!App.UsuarioTienePermisos("1116A"))
            {
                Response.Redirect("~/SinAutorizacion.aspx");
                return;
            }
            if (App.Usuario.IdGrupoEmpresa != App.ID_GRUPO_CRESUD)
            {
                Response.Redirect("~/SinAutorizacion.aspx");
                return;
            }



            //string json = Request.Form[this.hdDetalle.UniqueID];

            if (!IsPostBack)
            {
                cargarTipoDomicilio();
                cargarEspecies();
                cargarPartidos(cboCodigoPartidoOrigen);
                cargarPartidos(cboCodigoPartidoEntrega);
                cargarLocalidad();

                if (Request["id"] != null && Request["id"] != "0")
                {
                    string idc1116a = Request["id"];
                    C1116A form = C1116ADAO.Instance.GetOne(Convert.ToInt32(idc1116a));

                    CargarForm(form);

                }


            }


        }
        private void cargarEspecies()
        {
            ListItem li;
            li = new ListItem();
            li.Value = "-1";
            li.Text = "[Especies...]";
            cboEspecie.Items.Add(li);

            foreach (Grano g in GranoDAO.Instance.GetAll())
            {
                li = new ListItem();
                li.Value = g.IdGrano.ToString();
                li.Text = g.Descripcion + " - " + g.CosechaAfip.Codigo;

                cboEspecie.Items.Add(li);
            }
        
        }
        private void cargarPartidos(DropDownList cbo)
        {
            ListItem li;
            li = new ListItem();
            li.Value = "-1";
            li.Text = "[Partidos...]";
            cbo.Items.Add(li);

            foreach (var p in PartidoDAO.Instance.GetAll())
            {
                li = new ListItem();
                li.Value = p.Codigo.ToString();
                li.Text = p.Descripcion;

                cbo.Items.Add(li);
            }

        }
        private void cargarTipoDomicilio()
        {
            ListItem li;

            li = new ListItem();
            li.Text = "[Tipo Domicilio...]";
            li.Value = "-1";
            cboTipoDomicilio.Items.Add(li);

            foreach (Enums.TipoDomicilio item in Enum.GetValues(typeof(Enums.TipoDomicilio)))
            {
                li = new ListItem();
                li.Value = Convert.ToInt32(item).ToString();
                li.Text = splitCapitalizacion(item.ToString());
                cboTipoDomicilio.Items.Add(li);
            }

        
        }
        private void cargarLocalidad()
        {
            ListItem li;
            li = new ListItem();
            li.Value = "-1";
            li.Text = "[Localidades...]";
            cboCodigoLocalidadProductor.Items.Add(li);

            foreach (var l in LocalidadDAO.Instance.GetAll())
            {
                li = new ListItem();
                li.Value = l.Codigo.ToString();
                li.Text = l.Descripcion;

                cboCodigoLocalidadProductor.Items.Add(li);
            }


        }
        private void CargarForm(C1116A form)
        {
            if (form != null)
            {

                btExportar.Visible = true;
                btExportarDetalle.Visible = true;


                txtNumeroCAC.Text = form.NumeroCAC.ToString();
                txtNumeroCertificadoC1116A.Text = form.NroCertificadoc1116a.ToString();
                txtCodigoEstablecimiento.Text = form.CodigoEstablecimiento.ToString();
                txtCuitProductor.Text = form.CuitProveedor.ToString();
                txtRazonSocialProductor.Text = form.RazonSocialProveedor;
                cboTipoDomicilio.SelectedValue = Convert.ToInt32(form.TipoDomicilio).ToString();
                txtCalleRutaProductor.Text = form.CalleRutaProductor;
                txtNroKmProductor.Text = form.NroKmProductor.ToString();
                txtPisoProductor.Text = form.PisoProductor.ToString();
                txtOficinaDtoProductor.Text = form.OficinaDtoProductor;
                //txtCodigoLocalidadProductor.Text = form.CodigoLocalidadProductor.ToString(); // NOMBRE
                cboCodigoLocalidadProductor.SelectedValue = Convert.ToInt32(form.CodigoLocalidadProductor).ToString();
                txtCodigoPostalProductor.Text = form.CodigoPostalProductor;
                cboEspecie.SelectedValue = Convert.ToInt32(form.CodigoEspecie).ToString(); // REVISAR CARGA Y PERSISTENCIA DEL DATO
                txxCosecha.Text = form.Cosecha;
                txtAlmacenajeDíasLibres.Text = form.AlmacenajeDiasLibres.ToString();
                txtTarifaAlmacenaje.Text = form.TarifaAlmacenaje.ToString();
                txtGastosGenerales.Text = form.GastoGenerales.ToString();
                txtZarandeo.Text = form.Zarandeo.ToString();
                txtSecadoDe.Text = form.SecadoDe.ToString();
                txtSecadoA.Text = form.SecadoA.ToString();
                txtTarifaSecado.Text = form.TarifaSecado.ToString();
                txtPuntoExceso.Text = form.PuntoExceso.ToString();
                txtTarifaOtros.Text = form.TarifaOtros.ToString();
                
                //txtCodigoPartidoOrigen.Text = form.CodigoPartidoOrigen.ToString(); // NOMBRE
                //txtCodigoPartidoEntrega.Text = form.CodigoPartidoEntrega.ToString(); // NOMBRE
                cboCodigoPartidoOrigen.SelectedValue = Convert.ToInt32(form.CodigoPartidoOrigen).ToString();
                cboCodigoPartidoEntrega.SelectedValue = Convert.ToInt32(form.CodigoPartidoEntrega).ToString();

                txtNroAnalisis.Text = form.NumeroAnalisis.ToString();
                txtNroBoletin.Text = form.NumeroBoletin.ToString();
                txtFechaAnalisis.Text = form.FechaAnalisis.ToString("dd/MM/yyyy");
                txtGrado.Text = form.Grado.ToString();
                txtFactor.Text = form.Factor.ToString();
                txtContenidoProteico.Text = form.ContenidoProteico.ToString();
                txtCuitLaboratorio.Text = form.CuitLaboratorio.ToString();
                txtNombreLaboratorio.Text = form.NombreLaboratorio;
                txtPesoBruto.Text = form.PesoBruto.ToString();
                txtPesoNeto.Text = form.PesoNeto.ToString();
                txtMermaVolatil.Text = form.MermaVolatil.ToString();
                txtMermaZarandeo.Text = form.MermaZarandeo.ToString();
                txtMermaSecado.Text = form.MermaSecado.ToString();
                txtFechaCierre.Text = form.FechaCierre.ToString("dd/MM/yyyy");
                txtImporteIVAServicios.Text = form.ImporteIVAServicios.ToString();
                txtTotalServicios.Text = form.TotalServicios.ToString();

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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validaciones())
            {

                C1116A form = new C1116A();

                if (Request["id"] != null && Request["id"] != "0")
                {
                    string idc1116a = Request["id"];
                    form = C1116ADAO.Instance.GetOne(Convert.ToInt32(idc1116a));
                }


                form.NumeroCAC = (txtNumeroCAC.Text);
                form.NroCertificadoc1116a = (txtNumeroCertificadoC1116A.Text);
                form.CodigoEstablecimiento = Convert.ToInt32(txtCodigoEstablecimiento.Text);
                form.CuitProveedor = (txtCuitProductor.Text);
                form.RazonSocialProveedor = txtRazonSocialProductor.Text;
                form.TipoDomicilio = (Enums.TipoDomicilio)Convert.ToInt32(cboTipoDomicilio.SelectedValue.ToString());
                form.CalleRutaProductor = txtCalleRutaProductor.Text;
                form.NroKmProductor = Convert.ToInt32(txtNroKmProductor.Text);
                form.PisoProductor= txtPisoProductor.Text;
                form.OficinaDtoProductor = txtOficinaDtoProductor.Text;
                //form.CodigoLocalidadProductor = Convert.ToInt32(txtCodigoLocalidadProductor.Text); // NOMBRE
                form.CodigoLocalidadProductor = Convert.ToInt32(cboCodigoLocalidadProductor.SelectedValue); // NOMBRE
                form.CodigoPostalProductor = txtCodigoPostalProductor.Text;
                form.CodigoEspecie = Convert.ToInt32(cboEspecie.SelectedValue); // REVISAR CARGA Y PERSISTENCIA DEL DATO
                form.Cosecha = txxCosecha.Text;
                form.AlmacenajeDiasLibres = Convert.ToInt32(txtAlmacenajeDíasLibres.Text);
                form.TarifaAlmacenaje = Convert.ToDecimal(txtTarifaAlmacenaje.Text.Replace(".", ","));
                form.GastoGenerales = Convert.ToDecimal(txtGastosGenerales.Text.Replace(".", ","));
                form.Zarandeo = Convert.ToDecimal(txtZarandeo.Text.Replace(".", ","));
                form.SecadoDe = Convert.ToDecimal(txtSecadoDe.Text.Replace(".", ","));
                form.SecadoA = Convert.ToDecimal(txtSecadoA.Text.Replace(".", ","));
                form.TarifaSecado = Convert.ToDecimal(txtTarifaSecado.Text.Replace(".", ","));
                form.PuntoExceso = Convert.ToDecimal(txtPuntoExceso.Text.Replace(".", ","));
                form.TarifaOtros = Convert.ToDecimal(txtTarifaOtros.Text.Replace(".", ","));

                //form.CodigoPartidoOrigen = Convert.ToInt32(txtCodigoPartidoOrigen.Text); // NOMBRE
                //form.CodigoPartidoEntrega = Convert.ToInt32(txtCodigoPartidoEntrega.Text); // NOMBRE
                form.CodigoPartidoOrigen = Convert.ToInt32(cboCodigoPartidoOrigen.SelectedValue); // NOMBRE
                form.CodigoPartidoEntrega = Convert.ToInt32(cboCodigoPartidoEntrega.SelectedValue); // NOMBRE

                form.NumeroAnalisis = txtNroAnalisis.Text;
                form.NumeroBoletin = (txtNroBoletin.Text);
                form.FechaAnalisis = Convert.ToDateTime(txtFechaAnalisis.Text);
                form.Grado = (txtGrado.Text);
                form.Factor = Convert.ToDecimal(txtFactor.Text.Replace(".", ","));
                form.ContenidoProteico = Convert.ToDecimal(txtContenidoProteico.Text.Replace(".", ","));
                form.CuitLaboratorio = (txtCuitLaboratorio.Text);
                form.NombreLaboratorio = txtNombreLaboratorio.Text;
                form.PesoBruto = Convert.ToDecimal(txtPesoBruto.Text.Replace(".", ","));
                form.PesoNeto = Convert.ToDecimal(txtPesoNeto.Text.Replace(".", ","));
                form.MermaVolatil = Convert.ToDecimal(txtMermaVolatil.Text.Replace(".", ","));
                form.MermaZarandeo = Convert.ToDecimal(txtMermaZarandeo.Text.Replace(".", ","));
                form.MermaSecado = Convert.ToDecimal(txtMermaSecado.Text.Replace(".", ","));
                form.FechaCierre = Convert.ToDateTime(txtFechaCierre.Text);
                form.ImporteIVAServicios = Convert.ToDecimal(txtImporteIVAServicios.Text.Replace(".", ","));
                form.TotalServicios = Convert.ToDecimal(txtTotalServicios.Text.Replace(".", ","));
                form.UsuarioCreacion = App.Usuario.Nombre;

                int idFormulario = C1116ADAO.Instance.SaveOrUpdate(form);



                //Elimino el detalle actual.
                C1116ADAO.Instance.deleteDetalleByIDC1116A(idFormulario.ToString());

                //Guardo el detalle.                


                IList<C1116ADetalle> detalles = new List<C1116ADetalle>();

                var obj = JObject.Parse(this.hdDetalle.Value);
                var events = (JArray)obj["lineas"];
                foreach (JObject evt in events)
                {
                    String Idc1116aDetalle = (evt["Idc1116aDetalle"] != null) ? evt["Idc1116aDetalle"].Value<string>() : "0";
                    String Idc1116a = (evt["Idc1116a"] != null) ? evt["Idc1116a"].Value<string>() : "0";
                    String NumeroCartaDePorte = evt["NumeroCartaDePorte"].Value<string>();
                    String NumeroCertificadoAsociado = evt["NumeroCertificadoAsociado"].Value<string>();
                    String KgBrutos = evt["KgBrutos"].Value<string>().Replace(".", ",");
                    String FechaRemesa = evt["FechaRemesa"].Value<string>();

                    C1116ADetalle det = new C1116ADetalle();
                    det.Idc1116aDetalle = 0;
                    det.Idc1116a = idFormulario;
                    det.NumeroCartaDePorte = Convert.ToInt64(NumeroCartaDePorte);
                    det.NumeroCertificadoAsociado = Convert.ToInt64(NumeroCertificadoAsociado);
                    det.KgBrutos = Convert.ToDecimal(KgBrutos);
                    det.FechaRemesa = Convert.ToDateTime(FechaRemesa);
                    detalles.Add(det);

                    C1116ADAO.Instance.SaveOrUpdateDetalle(det);
                }

                //Fin guardo el detalle.

                lblMensaje.Text = "Formulario guardado exitósamente.";
            }
        }

        private bool validaciones()
        {

            lblMensaje.Text = string.Empty;
            string mensaje = string.Empty;

            if (String.IsNullOrEmpty(txtNumeroCAC.Text.Trim()) || !isNumeric(txtNumeroCAC.Text.Trim()))
            {
                mensaje += "Debe completar Numero CAC con un Valor Numérico.<br>";
            }

            if (String.IsNullOrEmpty(txtNumeroCertificadoC1116A.Text.Trim()) || !isNumeric(txtNumeroCertificadoC1116A.Text.Trim()))
            {
                mensaje += "Debe completar Numero Certificado con un Valor Numérico.<br>";
            }

            if (String.IsNullOrEmpty(txtCodigoEstablecimiento.Text.Trim()) || !isNumeric(txtCodigoEstablecimiento.Text.Trim()))
            {
                mensaje += "Debe completar el codigo de establecimiento con un Valor Numérico.<br>";
            }

            if (String.IsNullOrEmpty(txtCuitProductor.Text.Trim()) || !isNumeric(txtCuitProductor.Text.Trim()))
            {
                mensaje += "Debe completar el cuit del productor con un cuit válido.<br>";
            }

            if (String.IsNullOrEmpty(txtRazonSocialProductor.Text.Trim()))
            {
                mensaje += "Debe completar la Razón Social del productor.<br>";
            }

            if (cboTipoDomicilio.SelectedIndex < 1)
            {
                mensaje += "Debe seleccionar un Tipo de Domicilio<br>";
            }

            if (String.IsNullOrEmpty(txtCalleRutaProductor.Text.Trim()))
            {
                mensaje += "Debe completar la calle o ruta del productor.<br>";
            }
            if (String.IsNullOrEmpty(txtNroKmProductor.Text.Trim()) || !isNumeric(txtNroKmProductor.Text.Trim()))
            {
                mensaje += "Debe completar el número o Km del productor.<br>";
            }
            if (String.IsNullOrEmpty(txtPisoProductor.Text.Trim()) || !isNumeric(txtPisoProductor.Text.Trim()))
            {
                mensaje += "Debe completar el piso del productor.<br>";
            }

            if (String.IsNullOrEmpty(txtOficinaDtoProductor.Text.Trim()) || !isNumeric(txtOficinaDtoProductor.Text.Trim()))
            {
                mensaje += "Debe completar la Oficina o Dto. del productor.<br>";
            }

            //if (String.IsNullOrEmpty(txtCodigoLocalidadProductor.Text.Trim()))
            //{
            //    mensaje += "Debe completar la localidad del productor.<br>";
            //}
            if (cboCodigoLocalidadProductor.SelectedIndex < 1)
            {
                mensaje += "Debe seleccionar la localidad del productor.<br>";
            }

            if (String.IsNullOrEmpty(txtCodigoPostalProductor.Text.Trim()))
            {
                mensaje += "Debe completar el código postal del productor.<br>";
            }
            if (cboEspecie.SelectedIndex < 1)
            {
                mensaje += "Debe seleccionar una Especie.<br>";
            }
            if (String.IsNullOrEmpty(txxCosecha.Text.Trim()))
            {
                mensaje += "Debe completar la Cosecha.<br>";
            }
            if (String.IsNullOrEmpty(txtAlmacenajeDíasLibres.Text.Trim()) || !isNumeric(txtAlmacenajeDíasLibres.Text.Trim()))
            {
                mensaje += "Debe completar Almacenaje Dias Libres con un valor numérico.<br>";
            }
            if (String.IsNullOrEmpty(txtGastosGenerales.Text.Trim()) || !isNumeric(txtGastosGenerales.Text.Trim()))
            {
                mensaje += "Debe completar Gastos Generales con un valor numérico.<br>";
            }
            if (String.IsNullOrEmpty(txtZarandeo.Text.Trim()) || !isNumeric(txtZarandeo.Text.Trim()))
            {
                mensaje += "Debe completar Zarandeo con un valor numérico.<br>";
            }
            if (String.IsNullOrEmpty(txtSecadoDe.Text.Trim()) || !isNumeric(txtSecadoDe.Text.Trim()))
            {
                mensaje += "Debe completar SecadoDe con un valor numérico.<br>";
            }
            if (String.IsNullOrEmpty(txtSecadoA.Text.Trim()) || !isNumeric(txtSecadoA.Text.Trim()))
            {
                mensaje += "Debe completar txtSecadoA con un valor numérico.<br>";
            }
            if (String.IsNullOrEmpty(txtTarifaSecado.Text.Trim()) || !isNumeric(txtTarifaSecado.Text.Trim()))
            {
                mensaje += "Debe completar Tarifa Secado con un valor numérico.<br>";
            }

            if (String.IsNullOrEmpty(txtPuntoExceso.Text.Trim()) || !isNumeric(txtPuntoExceso.Text.Trim()))
            {
                mensaje += "Debe completar Punto Exceso con un valor numérico.<br>";
            }

            if (String.IsNullOrEmpty(txtTarifaOtros.Text.Trim()) || !isNumeric(txtTarifaOtros.Text.Trim()))
            {
                mensaje += "Debe completar Tarifa Otros con un valor numérico.<br>";
            }

            //if (String.IsNullOrEmpty(txtCodigoPartidoOrigen.Text.Trim()))
            //{
            //    mensaje += "Debe completar el Partido Origen.<br>";
            //}

            //if (String.IsNullOrEmpty(txtCodigoPartidoEntrega.Text.Trim()))
            //{
            //    mensaje += "Debe completar el Partido Entrega.<br>";
            //}

            if (cboCodigoPartidoOrigen.SelectedIndex < 1)
            {
                mensaje += "Debe seleccionar el Partido Origen.<br>";
            }

            if (cboCodigoPartidoEntrega.SelectedIndex < 1)
            {
                mensaje += "Debe seleccionar el Partido Entrega.<br>";
            }


            if (String.IsNullOrEmpty(txtNroAnalisis.Text.Trim()) || !isNumeric(txtNroAnalisis.Text.Trim()))
            {
                mensaje += "Debe completar Nro Analisis con un valor numérico.<br>";
            }
            if (String.IsNullOrEmpty(txtNroBoletin.Text.Trim()) || !isNumeric(txtNroBoletin.Text.Trim()))
            {
                mensaje += "Debe completar Nro Boletin con un valor numérico.<br>";
            }

            string fc = Request.Form[txtFechaCierre.UniqueID];
            string fa = Request.Form[txtFechaAnalisis.UniqueID];

            if (String.IsNullOrEmpty(fc))
            {
                mensaje += "Debe completar la fecha de cierre.<br>";
            }

            if (String.IsNullOrEmpty(fa))
            {
                mensaje += "Debe completar la fecha de Analisis.<br>";
            }


            if (!String.IsNullOrEmpty(fc))
            {
                if (!esFecha(fc.Trim().PadLeft(10,'0').Substring(0, 10).Split('/')))
                {
                    mensaje += "Debe completar una fecha de Cierre Válida.<br>";
                }
                
            }

            if (!String.IsNullOrEmpty(fa))
            {
                if (!esFecha(fa.Trim().PadLeft(10, '0').Substring(0, 10).Split('/')))
                {
                    mensaje += "Debe completar una fecha de Analisis Válida.<br>";
                }

            }



            if (String.IsNullOrEmpty(txtGrado.Text.Trim()) || !isNumeric(txtGrado.Text.Trim()))
            {
                mensaje += "Debe completar Grado con un valor numérico.<br>";
            }

            if (String.IsNullOrEmpty(txtFactor.Text.Trim()) || !isNumeric(txtFactor.Text.Trim()))
            {
                mensaje += "Debe completar Factor con un valor numérico.<br>";
            }

            if (String.IsNullOrEmpty(txtContenidoProteico.Text.Trim()))
            {
                mensaje += "Debe completar Contenido Proteico.<br>";
            }

            if (String.IsNullOrEmpty(txtCuitLaboratorio.Text.Trim()) || !isNumeric(txtCuitLaboratorio.Text.Trim()))
            {
                mensaje += "Debe completar el cuit del Laboratorio con un cuit válido.<br>";
            }

            if (String.IsNullOrEmpty(txtNombreLaboratorio.Text.Trim()))
            {
                mensaje += "Debe completar el Nombre Laboratorio.<br>";
            }
            if (String.IsNullOrEmpty(txtPesoBruto.Text.Trim()) || !isNumeric(txtPesoBruto.Text.Trim()))
            {
                mensaje += "Debe completar Peso Bruto con un valor numérico.<br>";
            }
            if (String.IsNullOrEmpty(txtPesoNeto.Text.Trim()) || !isNumeric(txtPesoNeto.Text.Trim()))
            {
                mensaje += "Debe completar Peso Neto con un valor numérico.<br>";
            }

            if (String.IsNullOrEmpty(txtMermaVolatil.Text.Trim()) || !isNumeric(txtMermaVolatil.Text.Trim()))
            {
                mensaje += "Debe completar Merma Volatil con un valor numérico.<br>";
            }

            if (String.IsNullOrEmpty(txtMermaZarandeo.Text.Trim()) || !isNumeric(txtMermaZarandeo.Text.Trim()))
            {
                mensaje += "Debe completar Merma Zarandeo con un valor numérico.<br>";
            }

            if (String.IsNullOrEmpty(txtMermaSecado.Text.Trim()) || !isNumeric(txtMermaSecado.Text.Trim()))
            {
                mensaje += "Debe completar Merma Secado con un valor numérico.<br>";
            }

            if (String.IsNullOrEmpty(txtImporteIVAServicios.Text.Trim()) || !isNumeric(txtImporteIVAServicios.Text.Trim()))
            {
                mensaje += "Debe completar Importe IVA Servicios con un valor numérico.<br>";
            }
            if (String.IsNullOrEmpty(txtTotalServicios.Text.Trim()) || !isNumeric(txtTotalServicios.Text.Trim()))
            {
                mensaje += "Debe completar Total Servicios con un valor numérico.<br>";
            }


            IList<C1116ADetalle> detalles = new List<C1116ADetalle>();

            var obj = JObject.Parse(Request.Form[this.hdDetalle.UniqueID]);
            var events = (JArray)obj["lineas"];

            decimal sumaKgBrutos = 0;
            foreach (JObject evt in events)
            {
                String Idc1116aDetalle = (evt["Idc1116aDetalle"] != null) ? evt["Idc1116aDetalle"].Value<string>() : "0";
                String Idc1116a = (evt["Idc1116a"] != null) ? evt["Idc1116a"].Value<string>() : "0";
                String NumeroCartaDePorte = evt["NumeroCartaDePorte"].Value<string>();
                String NumeroCertificadoAsociado = evt["NumeroCertificadoAsociado"].Value<string>();
                String KgBrutos = evt["KgBrutos"].Value<string>().Replace(".", ",");
                String FechaRemesa = evt["FechaRemesa"].Value<string>();

                C1116ADetalle det = new C1116ADetalle();
                det.Idc1116aDetalle = Convert.ToInt32(Idc1116aDetalle);
                det.Idc1116a = Convert.ToInt32(Idc1116a);
                det.NumeroCartaDePorte = Convert.ToInt64(NumeroCartaDePorte);
                det.NumeroCertificadoAsociado = Convert.ToInt64(NumeroCertificadoAsociado);
                det.KgBrutos = Convert.ToDecimal(KgBrutos);
                det.FechaRemesa = Convert.ToDateTime(FechaRemesa);
                detalles.Add(det);


                sumaKgBrutos += det.KgBrutos;

            }

            decimal kgBruto = 0;
            if (isNumeric(txtPesoBruto.Text.Trim()))
            {
                kgBruto = Convert.ToDecimal(txtPesoBruto.Text.Replace(".", ","));
                if (kgBruto != sumaKgBrutos)
                {
                    mensaje += "La suma de los kg bruto del detalle no coincide con el Peso Bruto declarado.<br>";
                }
            }


            if (detalles.Count < 1)
            {
                mensaje += "Debe completar el detalle de Cartas de porte asociadas al formulario.<br>";
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

        private bool esFecha(string[] fecha)
        {

            if (fecha.Length < 3)
                return false;


            try
            {
                DateTime F = new DateTime(Convert.ToInt32(fecha[2]), Convert.ToInt32(fecha[1]), Convert.ToInt32(fecha[0]));
                return true;

            }
            catch
            {   
            }

            return false;
        
        }

        
        protected void btExportar_Click(object sender, EventArgs e)
        {
            
            var idc1116a = Convert.ToInt32(Request["id"]);
            
            var cdp = C1116ADAO.Instance.GetOne(idc1116a);

            if (cdp == null)
            {
                return;
            }


            var tipoDomicilio = Convert.ToInt32(cboTipoDomicilio.SelectedValue.ToString());


            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            context.Response.ContentType = "text/plain";
            context.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=C1116A_{0}.txt", idc1116a));

            context.Response.Write(cdp.NroCertificadoc1116a.ToString().PadLeft(12, '0'));
            context.Response.Write(cdp.CodigoEstablecimiento.ToString().PadLeft(6, '0'));
            context.Response.Write(cdp.CuitProveedor.ToString().PadLeft(11, '0'));
            context.Response.Write(cdp.RazonSocialProveedor.ToString().PadRight(30, ' '));
            context.Response.Write(tipoDomicilio.ToString().PadLeft(1, '0'));
            context.Response.Write(cdp.CalleRutaProductor.ToString().PadRight(35, ' '));
            context.Response.Write(cdp.NroKmProductor.ToString("#0.00").Replace(".", ",").PadLeft(8, '0'));
            context.Response.Write(cdp.PisoProductor.ToString().PadRight(5, ' '));
            context.Response.Write(cdp.OficinaDtoProductor.ToString().PadRight(3, ' '));
            context.Response.Write(cdp.CodigoLocalidadProductor.ToString().PadLeft(5, '0'));
            context.Response.Write(cdp.CodigoPartidoProductor.ToString().PadLeft(5, '0'));
            context.Response.Write(cdp.CodigoPostalProductor.ToString().PadRight(8, ' '));
            context.Response.Write(cdp.CodigoEspecie.ToString().PadLeft(3, '0'));
            context.Response.Write(cdp.Cosecha.ToString().PadRight(5, ' '));
            context.Response.Write(cdp.AlmacenajeDiasLibres.ToString().PadLeft(3, '0'));
            context.Response.Write(cdp.TarifaAlmacenaje.ToString("#0.00").Replace(".", ",").PadLeft(7, '0'));
            context.Response.Write(cdp.GastoGenerales.ToString("#0.00").Replace(".", ",").PadLeft(11, '0'));
            context.Response.Write(cdp.Zarandeo.ToString("#0.00").Replace(".", ",").PadLeft(11, '0'));
            context.Response.Write(cdp.SecadoDe.ToString("#0.00").Replace(".", ",").PadLeft(6, '0'));
            context.Response.Write(cdp.SecadoA.ToString("#0.00").Replace(".", ",").PadLeft(6, '0'));
            context.Response.Write(cdp.TarifaSecado.ToString("#0.00").Replace(".", ",").PadLeft(7, '0'));
            context.Response.Write(cdp.PuntoExceso.ToString("#0.00").Replace(".", ",").PadLeft(11, '0'));
            context.Response.Write(cdp.TarifaOtros.ToString("#0.00").Replace(".", ",").PadLeft(7, '0'));
            context.Response.Write(cdp.CodigoPartidoOrigen.ToString().PadLeft(5, '0'));
            context.Response.Write(cdp.CodigoPartidoEntrega.ToString().PadLeft(5, '0'));
            context.Response.Write(cdp.NumeroAnalisis.ToString().PadRight(10, ' '));
            context.Response.Write(cdp.NumeroBoletin.ToString().PadLeft(8, '0'));
            context.Response.Write(cdp.FechaAnalisis.ToString("ddMMyyyy").PadLeft(8, '0'));
            context.Response.Write(cdp.Grado.ToString().PadLeft(2, '0'));
            context.Response.Write(cdp.Factor.ToString("#0.00").Replace(".", ",").PadLeft(6, '0'));
            context.Response.Write(cdp.ContenidoProteico.ToString("#0.00").Replace(".", ",").PadLeft(6, '0'));
            context.Response.Write(cdp.CuitLaboratorio.ToString().PadLeft(11, '0'));
            context.Response.Write(cdp.NombreLaboratorio.ToString().PadRight(40, ' '));
            context.Response.Write(cdp.PesoBruto.ToString("#0.00").Replace(".", ",").PadLeft(11, '0'));
            context.Response.Write(cdp.MermaVolatil.ToString("#0.00").Replace(".", ",").PadLeft(11, '0'));
            context.Response.Write(cdp.MermaZarandeo.ToString("#0.00").Replace(".", ",").PadLeft(11, '0'));
            context.Response.Write(cdp.MermaSecado.ToString("#0.00").Replace(".", ",").PadLeft(11, '0'));
            context.Response.Write(cdp.PesoNeto.ToString("#0.00").Replace(".", ",").PadLeft(11, '0'));
            context.Response.Write(cdp.FechaCierre.ToString("ddMMyyyy").PadLeft(8, '0'));
            context.Response.Write(cdp.ImporteIVAServicios.ToString("#0.00").Replace(".", ",").PadLeft(11, '0'));
            context.Response.Write(cdp.TotalServicios.ToString("#0.00").Replace(".", ",").PadLeft(11, '0'));
            context.Response.Write(cdp.NumeroCAC.ToString().PadLeft(14, '0'));

            context.Response.End();



        }

        protected void btExportarDetalle_Click(object sender, EventArgs e)
        {
            var idc1116a = Convert.ToInt32(Request["id"]);

            var cdp = C1116ADAO.Instance.GetOne(idc1116a);

            if (cdp == null)
            {
                return;
            }


            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            context.Response.ContentType = "text/plain";
            context.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=C1116A_{0}_detalle.txt", idc1116a));

            
            
            IList<C1116ADetalle> detalles = new List<C1116ADetalle>();

            detalles = C1116ADAO.Instance.GetDetalle(idc1116a.ToString());


            foreach (C1116ADetalle d in detalles)
            {

                context.Response.Write(cdp.NroCertificadoc1116a.ToString().PadLeft(12, '0'));
                
                context.Response.Write(d.NumeroCartaDePorte.ToString().PadLeft(12, '0'));
                context.Response.Write(d.NumeroCertificadoAsociado.ToString().PadLeft(12, '0'));
                context.Response.Write(d.KgBrutos.ToString("#0.00").Replace(".", ",").PadLeft(11, '0'));
                context.Response.Write(d.FechaRemesa.ToString("ddMMyyyy").PadLeft(8, '0'));

                context.Response.Write(Environment.NewLine);
            }



            context.Response.End();


        }



    }
}

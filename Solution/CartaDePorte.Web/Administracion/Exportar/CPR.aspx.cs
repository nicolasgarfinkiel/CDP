using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CartaDePorte.Core.Domain;
using CartaDePorte.Core.DAO;
using System.Data;
using System.Globalization;

namespace CartaDePorte.Web
{
    public partial class CPR : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Main master = (Main)Page.Master;
            master.HiddenValue = "Reportes";

            CartaDePorte.Core.Domain.Seguridad.SeguridadUsuario su = (Session["Usuario"] != null) ? (CartaDePorte.Core.Domain.Seguridad.SeguridadUsuario)Session["Usuario"] : null;
            if (su == null)
                return;

            if (!su.CheckPermisoInterno("Reportes"))
            {
                Response.Redirect("../../SinAutorizacion.aspx");
                return;
            }

            string fd = Request.Form[txtDateDesde.UniqueID];
            string fh = Request.Form[txtDateHasta.UniqueID];

            txtDateDesde.Text = fd;
            txtDateHasta.Text = fh;

            if (!IsPostBack)
            {
                if (String.IsNullOrEmpty(txtDateDesde.Text))
                {
                    txtDateDesde.Text = DateTime.Today.ToString("dd/MM/yyyy");

                }
                if (String.IsNullOrEmpty(txtDateHasta.Text))
                {
                    txtDateHasta.Text = DateTime.Today.ToString("dd/MM/yyyy");
                }
            }


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Validaciones())
            {
                HttpContext context = HttpContext.Current;
                context.Response.Clear();
                context.Response.ContentType = "text/plain";
                context.Response.AddHeader("Content-Disposition", "attachment; filename=CPR.txt");

                string fd = Request.Form[txtDateDesde.UniqueID];
                string fh = Request.Form[txtDateHasta.UniqueID];

                string[] fechaDesde = fd.Trim().Substring(0, 10).Split('/');
                DateTime FD = new DateTime(Convert.ToInt32(fechaDesde[2]), Convert.ToInt32(fechaDesde[1]), Convert.ToInt32(fechaDesde[0]));

                string[] fechaHasta = fh.Trim().Substring(0, 10).Split('/');
                DateTime FH = new DateTime(Convert.ToInt32(fechaHasta[2]), Convert.ToInt32(fechaHasta[1]), Convert.ToInt32(fechaHasta[0]));

                IList<SolicitudRecibida> tempData = SolicitudRecibidaDAO.Instance.GetAllReporteRecibidas(FD, FH.AddHours(23).AddMinutes(59));

                foreach (SolicitudRecibida row in tempData)
                {
                    context.Response.Write("1"); // Tipo de Transporte
                    context.Response.Write(Convert.ToInt32(row.TipoDeCarta).ToString().Trim()); // Tipo de Carta de Porte
                    context.Response.Write(CerosEnCampos(row.NumeroCartaDePorte, 12)); //  Nro. Carta de Porte
                    context.Response.Write(CerosEnCampos(row.Cee, 14)); // Número de CEE 
                    context.Response.Write(CerosEnCampos(row.Ctg, 8)); // Número de CTG
                    context.Response.Write(EspaciosEnCampos(row.FechaDeEmision.Value.ToString("ddMMyyyy"), 8)); // Fecha de Carga
                    context.Response.Write(CerosEnCampos(row.CuitProveedorTitularCartaDePorte.Trim(), 11)); // CUIT Titular Carta de Porte
                    context.Response.Write(CerosEnCampos(row.CuitClienteIntermediario, 11)); // CUIT Intermediario
                    context.Response.Write(CerosEnCampos(row.CuitClienteRemitenteComercial, 11)); // CUIT del Remitente Comercial
                    context.Response.Write(CerosEnCampos(row.CuitClienteCorredor, 11)); //CUIT Corredor
                    context.Response.Write(CerosEnCampos(row.CuitClienteEntregador, 11)); // CUIT Representante Entregador
                    context.Response.Write(CerosEnCampos(row.CuitClienteDestinatario, 11)); // CUIT Destinatario
                    context.Response.Write(CerosEnCampos(row.CuitClienteDestino, 11)); // CUIT Establecimiento Destino
                    context.Response.Write(CerosEnCampos(row.CuitProveedorTransportista, 11)); // CUIT Transportista
                    context.Response.Write(CerosEnCampos(row.CuitChofer, 11)); // CUIT/CUIL del Chofer / Conductor

                    context.Response.Write(EspaciosEnCampos(row.Grano.CosechaAfip.Descripcion, 5)); // Cosecha
                    context.Response.Write(CerosEnCampos(row.Grano.EspecieAfip.Codigo.ToString(), 3)); // Código de Especie
                    context.Response.Write(CerosEnCampos(row.Grano.TipoGrano.IdTipoGrano.ToString(), 2)); // Tipo de Grano

                    //string nroContrato = EspaciosEnCampos(row.NumeroContrato.ToString(), 20).Substring(0,20);
                    string nroContrato = string.Format("                    {0}", row.NumeroContrato);
                    nroContrato = nroContrato.Substring(nroContrato.Length - 20, 20);
                    context.Response.Write(nroContrato); // Contrato / Boleta Compra - Venta

                    context.Response.Write(EspaciosEnCampos((row.CargaPesadaDestino) ? "2" : "1", 1)); // Tipo de Pesado

                    decimal peso;
                    if (row.CargaPesadaDestino)
                        peso = Convert.ToDecimal(row.KilogramosEstimados.ToString()) * 1.00M;
                    else
                        peso = Convert.ToDecimal(row.PesoNeto.Value.ToString()) * 1.00M;

                    context.Response.Write(CerosEnCampos(peso.ToString(), 11).Replace('.', ',')); // Peso Neto de Carga/Peso Total Despachado (Kg)

                    context.Response.Write(CerosEnCampos(row.CodigoEstablecimientoProcedencia, 6)); // Código de Establecimiento de Procedencia
                    context.Response.Write(CerosEnCampos(row.IdLocalidadEstablecimientoProcedencia.ToString(), 5)); // Código de Localidad de Procedencia
                    context.Response.Write(CerosEnCampos(row.CodigoEstablecimientoDestino, 6)); // Código de Establecimiento Destino - 21570
                    context.Response.Write(CerosEnCampos("17693", 5)); // Código de Localidad de Destino - 17693
                    context.Response.Write(CerosEnCampos(row.KmRecorridos.ToString(), 4)); // Km a Recorrer
                    context.Response.Write(EspaciosEnCampos(row.PatenteCamion, 11)); // Patente del Camión
                    context.Response.Write(EspaciosEnCampos(row.PatenteAcoplado, 11)); // Acoplado Patente
                    context.Response.Write(CerosEnCampos(row.TarifaReal.ToString(), 8).Replace('.', ',')); //Tarifa por Tonelada
                    context.Response.Write(EspaciosEnCampos(row.FechaDeDescarga.Value.ToString("ddMMyyyy"), 8)); // Fecha de Descarga
                    context.Response.Write(EspaciosEnCampos(row.FechaDeArribo.Value.ToString("ddMMyyyy"), 8)); // Fecha de Arribo a Destino/Redestino

                    decimal pesoNetoDescarga = Convert.ToDecimal(row.PesoNetoDescarga) * 1.00M;
                    context.Response.Write(CerosEnCampos(pesoNetoDescarga.ToString(), 11).Replace('.', ',')); // Peso Neto de Descarga
                    context.Response.Write(CerosEnCampos(row.CuitEstablecimientoDestinoCambio.Trim(), 11)); // CUIT Establecimiento Redestino
                    context.Response.Write(CerosEnCampos(row.IdLocalidadEstablecimientoDestinoCambio.ToString(), 5)); // Código de Localidad de Redestino
                    context.Response.Write(CerosEnCampos(row.CodigoEstablecimientoDestinoCambio, 6)); // Código de Establecimiento de Redestino
                    context.Response.Write(CerosEnCampos(row.TarifaReferencia.ToString(), 8).Replace('.', ',')); // Flete-Tarifa de Referencia


                    context.Response.Write(Environment.NewLine);
                }
                context.Response.End();

            }

        }

        private String EspaciosEnCampos(String texto, int pad)
        {
            if (texto == null)
                return string.Empty.PadLeft(pad, ' ');

            return texto.PadLeft(pad, ' ');
        }

        private String CerosEnCampos(String texto, int pad)
        {
            if (texto == null)
                return string.Empty.PadLeft(pad, '0');

            return texto.PadLeft(pad, '0');
        }

        private Boolean Validaciones()
        {

            Boolean result = true;
            string mensaje = string.Empty;
            lblMensaje.Text = string.Empty;

            string fd = Request.Form[txtDateDesde.UniqueID];
            string fh = Request.Form[txtDateHasta.UniqueID];

            if (String.IsNullOrEmpty(fd))
            {
                mensaje += "Debe completar FechaDesde<br>";
            }
            if (String.IsNullOrEmpty(fh))
            {
                mensaje += "Debe completar FechaHasta<br>";
            }


            if (mensaje.Length > 0)
            {
                lblMensaje.Text = mensaje;
                result = false;
            }
            else
            {

                string[] fechaDesde = fd.Trim().Substring(0, 10).Split('/');
                DateTime FD = new DateTime(Convert.ToInt32(fechaDesde[2]), Convert.ToInt32(fechaDesde[1]), Convert.ToInt32(fechaDesde[0]));

                string[] fechaHasta = fh.Trim().Substring(0, 10).Split('/');
                DateTime FH = new DateTime(Convert.ToInt32(fechaHasta[2]), Convert.ToInt32(fechaHasta[1]), Convert.ToInt32(fechaHasta[0]));

                if (FD > FH)
                {
                    mensaje += "La Fecha Desde debe ser menor o igual a la Fecha Hasta<br>";

                    lblMensaje.Text = mensaje;
                    result = false;

                }

            }


            return result;
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




        protected void Button2_Click(object sender, EventArgs e)
        {
            if (Validaciones())
            {
                CargarTitulos();
                Datos();
            }
        }

        private void CargarTitulos()
        {
            tblData.Rows.Clear();
            var row = new TableRow();
            row.CssClass = "TableRowTitle";
            row.Cells.Add(AddTitleCell("Tipo de Transporte", 20));
            row.Cells.Add(AddTitleCell("Tipo de Carta de Porte", 20));
            row.Cells.Add(AddTitleCell("Nro. Carta de Porte", 20));
            row.Cells.Add(AddTitleCell("Número de CEE", 20));
            row.Cells.Add(AddTitleCell("Número de CTG", 20));
            row.Cells.Add(AddTitleCell("Fecha de Carga", 20));
            row.Cells.Add(AddTitleCell("CUIT Titular Carta de Porte", 20));
            row.Cells.Add(AddTitleCell("CUIT Intermediario", 20));
            row.Cells.Add(AddTitleCell("CUIT del Remitente Comercial", 20));
            row.Cells.Add(AddTitleCell("CUIT Corredor", 20));
            row.Cells.Add(AddTitleCell("CUIT Representante Entregador", 20));
            row.Cells.Add(AddTitleCell("CUIT Destinario", 20));
            row.Cells.Add(AddTitleCell("CUIT Establecimiento Destino", 20));
            row.Cells.Add(AddTitleCell("CUIT Transportista", 20));
            row.Cells.Add(AddTitleCell("CUIT/CUIL del Chofer / Conductor", 20));
            row.Cells.Add(AddTitleCell("Cosecha", 20));
            row.Cells.Add(AddTitleCell("Código de Especie", 20));
            row.Cells.Add(AddTitleCell("Tipo de Grano", 20));
            row.Cells.Add(AddTitleCell("Contrato / Boleta Compra - Venta", 20));
            row.Cells.Add(AddTitleCell("Tipo de Pesado", 20));
            row.Cells.Add(AddTitleCell("Peso Neto de Carga/Peso Total Despachado (Kg)", 20));
            row.Cells.Add(AddTitleCell("Código de Establecimiento de Procedencia", 20));
            row.Cells.Add(AddTitleCell("Código de Localidad de Procedencia", 20));
            row.Cells.Add(AddTitleCell("Código de Establecimiento Destino", 20));
            row.Cells.Add(AddTitleCell("Código de Localidad de Destino", 20));
            row.Cells.Add(AddTitleCell("Km a Recorrer", 20));
            row.Cells.Add(AddTitleCell("Patente del Camión", 20));
            row.Cells.Add(AddTitleCell("Acoplado Patente", 20));
            row.Cells.Add(AddTitleCell("Tarifa por Tonelada", 20));

            row.Cells.Add(AddTitleCell("Fecha de Descarga", 20));
            row.Cells.Add(AddTitleCell("Fecha de Arribo a Destino/Redestino", 20));
            row.Cells.Add(AddTitleCell("Peso Neto de Descarga", 20));
            row.Cells.Add(AddTitleCell("CUIT Establecimiento Redestino", 20));
            row.Cells.Add(AddTitleCell("Código de Localidad de Redestino", 20));
            row.Cells.Add(AddTitleCell("Código de Establecimiento de Redestino", 20));

            row.Cells.Add(AddTitleCell("Flete-Tarifa de Referencia", 20));
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

            if (Validaciones())
            {
                string fd = Request.Form[txtDateDesde.UniqueID];
                string fh = Request.Form[txtDateHasta.UniqueID];

                string[] fechaDesde = fd.Trim().Substring(0, 10).Split('/');
                DateTime FD = new DateTime(Convert.ToInt32(fechaDesde[2]), Convert.ToInt32(fechaDesde[1]), Convert.ToInt32(fechaDesde[0]));

                string[] fechaHasta = fh.Trim().Substring(0, 10).Split('/');
                DateTime FH = new DateTime(Convert.ToInt32(fechaHasta[2]), Convert.ToInt32(fechaHasta[1]), Convert.ToInt32(fechaHasta[0]));

                IList<SolicitudRecibida> tempData = SolicitudRecibidaDAO.Instance.GetAllReporteRecibidas(FD, FH.AddHours(23).AddMinutes(59));

                foreach (SolicitudRecibida row in tempData)
                {
                    var rowCell = new TableRow();
                    rowCell.CssClass = "TableRow";



                    rowCell.Cells.Add(AddCell("1", string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(Convert.ToInt32(row.TipoDeCarta).ToString().Trim(), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(CerosEnCampos(row.NumeroCartaDePorte, 12), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(CerosEnCampos(row.Cee, 14), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(CerosEnCampos(row.Ctg, 8), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(EspaciosEnCampos(row.FechaDeEmision.Value.ToString("ddMMyyyy"), 8), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(CerosEnCampos(row.CuitProveedorTitularCartaDePorte.Trim(), 11), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(CerosEnCampos(row.CuitClienteIntermediario, 11), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(CerosEnCampos(row.CuitClienteRemitenteComercial, 11), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(CerosEnCampos(row.CuitClienteCorredor, 11), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(CerosEnCampos(row.CuitClienteEntregador, 11), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(CerosEnCampos(row.CuitClienteDestinatario, 11), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(CerosEnCampos(row.CuitClienteDestino, 11), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(CerosEnCampos(row.CuitProveedorTransportista, 11), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(CerosEnCampos(row.CuitChofer, 11), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(EspaciosEnCampos(row.Grano.CosechaAfip.Descripcion, 5), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(CerosEnCampos(row.Grano.EspecieAfip.Codigo.ToString(), 3), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(CerosEnCampos(row.Grano.TipoGrano.IdTipoGrano.ToString(), 2), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(EspaciosEnCampos(row.NumeroContrato.ToString(), 20), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(EspaciosEnCampos((row.CargaPesadaDestino) ? "2" : "1", 1), string.Empty, HorizontalAlign.Justify));

                    Decimal peso;
                    if (row.CargaPesadaDestino)
                        peso = Convert.ToDecimal(row.KilogramosEstimados.ToString()) * 1.00m;
                    else
                        peso = Convert.ToDecimal(row.PesoNeto.Value.ToString()) * 1.00m;

                    rowCell.Cells.Add(AddCell(CerosEnCampos(peso.ToString(), 11).Replace('.', ','), string.Empty, HorizontalAlign.Justify));

                    rowCell.Cells.Add(AddCell(CerosEnCampos(row.CodigoEstablecimientoProcedencia, 6), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(CerosEnCampos(row.IdLocalidadEstablecimientoProcedencia.ToString(), 5), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(CerosEnCampos(row.CodigoEstablecimientoDestino, 6), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(CerosEnCampos("17693", 5), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(CerosEnCampos(row.KmRecorridos.ToString(), 4), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(EspaciosEnCampos(row.PatenteCamion, 11), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(EspaciosEnCampos(row.PatenteAcoplado, 11), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(CerosEnCampos(row.TarifaReal.ToString(), 8).Replace('.', ','), string.Empty, HorizontalAlign.Justify));


                    rowCell.Cells.Add(AddCell(EspaciosEnCampos(row.FechaDeDescarga.Value.ToString("ddMMyyyy"), 8), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(EspaciosEnCampos(row.FechaDeArribo.Value.ToString("ddMMyyyy"), 8), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(CerosEnCampos(row.PesoNetoDescarga.ToString(), 11).Replace('.', ','), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(CerosEnCampos(row.CuitEstablecimientoDestinoCambio.Trim(), 11), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(CerosEnCampos(row.IdLocalidadEstablecimientoDestinoCambio.ToString(), 5), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(CerosEnCampos(row.CodigoEstablecimientoDestinoCambio, 6), string.Empty, HorizontalAlign.Justify));

                    rowCell.Cells.Add(AddCell(CerosEnCampos(row.TarifaReferencia.ToString(), 8).Replace('.', ','), string.Empty, HorizontalAlign.Justify));

                    tblData.Rows.Add(rowCell);
                }

            }

        }









    }
}

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
using CartaDePorte.Core.Domain.Seguridad;
using CartaDePorte.Core;

namespace CartaDePorte.Web
{
    public partial class CPE : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Main master = (Main)Page.Master;
            master.HiddenValue = "Reportes";

            if (!App.UsuarioTienePermisos("Reportes"))
            {
                Response.Redirect("~/SinAutorizacion.aspx");
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
                context.Response.AddHeader("Content-Disposition", "attachment; filename=CPE.txt");

                string fd = Request.Form[txtDateDesde.UniqueID];
                string fh = Request.Form[txtDateHasta.UniqueID];

                string[] fechaDesde = fd.Trim().Substring(0, 10).Split('/');
                DateTime FD = new DateTime(Convert.ToInt32(fechaDesde[2]), Convert.ToInt32(fechaDesde[1]), Convert.ToInt32(fechaDesde[0]));

                string[] fechaHasta = fh.Trim().Substring(0, 10).Split('/');
                DateTime FH = new DateTime(Convert.ToInt32(fechaHasta[2]), Convert.ToInt32(fechaHasta[1]), Convert.ToInt32(fechaHasta[0]));

                IList<Solicitud> tempData = SolicitudDAO.Instance.GetAllReporteEmitidas(FD, FH.AddHours(23).AddMinutes(59));

                foreach (Solicitud row in tempData)
                {
                    context.Response.Write("1");
                    context.Response.Write("5");
                    context.Response.Write(CerosEnCampos(row.NumeroCartaDePorte, 12)); // 
                    context.Response.Write(EspaciosEnCampos(row.Cee, 14));
                    context.Response.Write(EspaciosEnCampos(row.Ctg, 8));
                    context.Response.Write(EspaciosEnCampos(row.FechaDeEmision.Value.ToString("ddMMyyyy"), 8));
                    context.Response.Write(EspaciosEnCampos(row.ProveedorTitularCartaDePorte.NumeroDocumento.Trim(), 11));
                    context.Response.Write(CerosEnCampos(row.ClienteIntermediario.Cuit, 11)); // 
                    context.Response.Write(CerosEnCampos(row.ClienteRemitenteComercial.Cuit, 11)); //
                    context.Response.Write(EspaciosEnCampos(row.ClienteCorredor.Cuit, 11));
                    context.Response.Write(EspaciosEnCampos(row.ClienteEntregador.Cuit, 11));
                    context.Response.Write(EspaciosEnCampos(row.ClienteDestinatario.Cuit, 11));
                    context.Response.Write(EspaciosEnCampos(row.ClienteDestino.Cuit, 11));

                    String CuitTransportista = string.Empty;

                    if (row.ClientePagadorDelFlete.EsEmpresa())
                        CuitTransportista = (row.ProveedorTransportista != null) ? row.ProveedorTransportista.NumeroDocumento : string.Empty;
                    else
                        CuitTransportista = (row.ChoferTransportista != null) ? row.ChoferTransportista.Cuit : string.Empty;

                    context.Response.Write(EspaciosEnCampos(CuitTransportista, 11));

                    context.Response.Write(EspaciosEnCampos(row.Chofer.Cuit, 11));
                    context.Response.Write(EspaciosEnCampos(row.Grano.CosechaAfip.Descripcion, 5));
                    context.Response.Write(CerosEnCampos(row.Grano.EspecieAfip.Codigo.ToString(), 3));
                    context.Response.Write(CerosEnCampos(row.Grano.TipoGrano.IdTipoGrano.ToString(), 2)); //
                    context.Response.Write(EspaciosEnCampos(row.NumeroContrato.ToString(), 20));
                    context.Response.Write(EspaciosEnCampos((row.CargaPesadaDestino) ? "2" : "1", 1));

                    Decimal peso;
                    if (row.CargaPesadaDestino)
                        peso = Convert.ToDecimal(row.KilogramosEstimados.ToString()) * 1.00M;
                    else
                        peso = Convert.ToDecimal(row.PesoNeto.Value.ToString()) * 1.00M;

                    context.Response.Write(CerosEnCampos(peso.ToString(), 11).Replace('.', ','));

                    context.Response.Write(CerosEnCampos(row.IdEstablecimientoProcedencia.EstablecimientoAfip, 6)); //
                    context.Response.Write(CerosEnCampos(row.IdEstablecimientoProcedencia.Localidad.Codigo.ToString(), 5));
                    context.Response.Write(CerosEnCampos(row.IdEstablecimientoDestino.EstablecimientoAfip, 6)); //
                    context.Response.Write(CerosEnCampos(row.IdEstablecimientoDestino.Localidad.Codigo.ToString(), 5));
                    context.Response.Write(CerosEnCampos(row.KmRecorridos.ToString(), 4)); //
                    context.Response.Write(EspaciosEnCampos(row.PatenteCamion, 11));
                    context.Response.Write(EspaciosEnCampos(row.PatenteAcoplado, 11));
                    context.Response.Write(CerosEnCampos(row.TarifaReal.ToString(), 8).Replace('.', ',')); //
                    context.Response.Write(CerosEnCampos(row.TarifaReferencia.ToString(), 8).Replace('.', ',')); //

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

                IList<Solicitud> tempData = SolicitudDAO.Instance.GetAllReporteEmitidas(FD, FH.AddHours(23).AddMinutes(59));

                foreach (Solicitud row in tempData)
                {
                    var rowCell = new TableRow();
                    rowCell.CssClass = "TableRow";

                    rowCell.Cells.Add(AddCell("1", string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell("5", string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(CerosEnCampos(row.NumeroCartaDePorte, 12), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(EspaciosEnCampos(row.Cee, 14), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(EspaciosEnCampos(row.Ctg, 8), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(EspaciosEnCampos(row.FechaDeEmision.Value.ToString("ddMMyyyy"), 8), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(EspaciosEnCampos(row.ProveedorTitularCartaDePorte.NumeroDocumento.Trim(), 11), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(CerosEnCampos(row.ClienteIntermediario.Cuit, 11), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(CerosEnCampos(row.ClienteRemitenteComercial.Cuit, 11), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(EspaciosEnCampos(row.ClienteCorredor.Cuit, 11), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(EspaciosEnCampos(row.ClienteEntregador.Cuit, 11), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(EspaciosEnCampos(row.ClienteDestinatario.Cuit, 11), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(EspaciosEnCampos(row.ClienteDestino.Cuit, 11), string.Empty, HorizontalAlign.Justify));


                    String CuitTransportista = string.Empty;
                    CuitTransportista = (row.ProveedorTransportista != null) ? row.ProveedorTransportista.NumeroDocumento : string.Empty;
                    if (String.IsNullOrEmpty(CuitTransportista))
                        CuitTransportista = (row.ChoferTransportista != null) ? row.ChoferTransportista.Cuit : string.Empty;

                    rowCell.Cells.Add(AddCell(EspaciosEnCampos(CuitTransportista, 11), string.Empty, HorizontalAlign.Justify));
                    
                    
                    rowCell.Cells.Add(AddCell(EspaciosEnCampos(row.Chofer.Cuit, 11), string.Empty, HorizontalAlign.Justify));
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

                    rowCell.Cells.Add(AddCell(CerosEnCampos(row.IdEstablecimientoProcedencia.EstablecimientoAfip, 6), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(CerosEnCampos(row.IdEstablecimientoProcedencia.Localidad.Codigo.ToString(), 5), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(CerosEnCampos(row.IdEstablecimientoDestino.EstablecimientoAfip, 6), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(CerosEnCampos(row.IdEstablecimientoDestino.Localidad.Codigo.ToString(), 5), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(CerosEnCampos(row.KmRecorridos.ToString(), 4), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(EspaciosEnCampos(row.PatenteCamion, 11), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(EspaciosEnCampos(row.PatenteAcoplado, 11), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(CerosEnCampos(row.TarifaReal.ToString(), 8).Replace('.', ','), string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(CerosEnCampos(row.TarifaReferencia.ToString(), 8).Replace('.', ','), string.Empty, HorizontalAlign.Justify));

                    tblData.Rows.Add(rowCell);
                }

            }

        }









    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CartaDePorte.Core.Domain;
using CartaDePorte.Core.DAO;
using CartaDePorte.Core.Domain.Seguridad;
using CartaDePorte.Core;
using OfficeOpenXml;
using System.IO;
using CartaDePorte.Common;

namespace CartaDePorte.Web
{
    public partial class CartasDePorteExportarCSV : System.Web.UI.Page
    {
        public String MisResultados()
        {
            String result = string.Empty;
            return result;
        }

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
                    txtDateDesde.Text = DateTime.Today.ToString("dd/MM/yyyy");
                if (String.IsNullOrEmpty(txtDateHasta.Text))
                    txtDateHasta.Text = DateTime.Today.ToString("dd/MM/yyyy");

                BindCombos();
            }
        }

        #region BindCombos
        protected void BindCombos()
        {
            ListItem li;
            li = new ListItem();
            li.Value = "-1";
            li.Text = "[seleccione...]";
            cboGrano.Items.Add(li);
            cboTitular.Items.Add(li);
            cboIntermediario.Items.Add(li);
            cboRemitenteComercial.Items.Add(li);
            cboCorredor.Items.Add(li);
            cboRepresentanteEntregador.Items.Add(li);
            cboDestinatario.Items.Add(li);
            cboTransportista.Items.Add(li);
            cboChofer.Items.Add(li);
            cboProcedencia.Items.Add(li);
            cboDestino.Items.Add(li);
            cboCosecha.Items.Add(li);

            var dsProveedor = ReporteLoteCDPDAO.Instance.GetProveedoresDS();
            var dsChofer = ReporteLoteCDPDAO.Instance.GetChoferesDS();
            var dsEstOrigen = ReporteLoteCDPDAO.Instance.GetEstablecimientoOrigenAllDS();
            var dsEstDestino = ReporteLoteCDPDAO.Instance.GetEstablecimientoDestinoAllDS();

            for (int i = 0; i < dsProveedor.Tables[0].Rows.Count; i++)
            {
                li = new ListItem();
                li.Value = dsProveedor.Tables[0].Rows[i].ItemArray[0].ToString();
                li.Text = dsProveedor.Tables[0].Rows[i].ItemArray[2].ToString();
                cboTitular.Items.Add(li);
                cboTransportista.Items.Add(li);
            }

            var dsCliente = ReporteLoteCDPDAO.Instance.GetClientesDS("ClienteIntermediario");

            for (int i = 0; i < dsCliente.Tables[0].Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(dsCliente.Tables[0].Rows[i].ItemArray[1].ToString()))
                {
                    li = new ListItem();
                    li.Value = dsCliente.Tables[0].Rows[i].ItemArray[0].ToString();
                    li.Text = dsCliente.Tables[0].Rows[i].ItemArray[1].ToString();
                    cboIntermediario.Items.Add(li);
                }
            }
            dsCliente = null;

            dsCliente = ReporteLoteCDPDAO.Instance.GetClientesDS("ClienteRemitenteComercial");

            for (int i = 0; i < dsCliente.Tables[0].Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(dsCliente.Tables[0].Rows[i].ItemArray[1].ToString()))
                {
                    li = new ListItem();
                    li.Value = dsCliente.Tables[0].Rows[i].ItemArray[0].ToString();
                    li.Text = dsCliente.Tables[0].Rows[i].ItemArray[1].ToString();
                    cboRemitenteComercial.Items.Add(li);
                }
            }

            dsCliente = null;

            dsCliente = ReporteLoteCDPDAO.Instance.GetClientesDS("ClienteCorredor");

            for (int i = 0; i < dsCliente.Tables[0].Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(dsCliente.Tables[0].Rows[i].ItemArray[1].ToString()))
                {
                    li = new ListItem();
                    li.Value = dsCliente.Tables[0].Rows[i].ItemArray[0].ToString();
                    li.Text = dsCliente.Tables[0].Rows[i].ItemArray[1].ToString();
                    cboCorredor.Items.Add(li);
                }
            }

            dsCliente = null;

            dsCliente = ReporteLoteCDPDAO.Instance.GetClientesDS("ClienteEntregador");

            for (int i = 0; i < dsCliente.Tables[0].Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(dsCliente.Tables[0].Rows[i].ItemArray[1].ToString()))
                {
                    li = new ListItem();
                    li.Value = dsCliente.Tables[0].Rows[i].ItemArray[0].ToString();
                    li.Text = dsCliente.Tables[0].Rows[i].ItemArray[1].ToString();
                    cboRepresentanteEntregador.Items.Add(li);
                }
            }

            dsCliente = null;

            dsCliente = ReporteLoteCDPDAO.Instance.GetClientesDS("ClienteDestinatario");

            for (int i = 0; i < dsCliente.Tables[0].Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(dsCliente.Tables[0].Rows[i].ItemArray[1].ToString()))
                {
                    li = new ListItem();
                    li.Value = dsCliente.Tables[0].Rows[i].ItemArray[0].ToString();
                    li.Text = dsCliente.Tables[0].Rows[i].ItemArray[1].ToString();
                    cboDestinatario.Items.Add(li);
                }
            }

            foreach (Cosecha c in CosechaDAO.Instance.GetAll())
            {
                li = new ListItem();
                li.Value = c.IdCosecha.ToString();
                li.Text = c.Descripcion;
                cboCosecha.Items.Add(li);
            }

            foreach (Grano g in GranoDAO.Instance.GetAllDistinct())
            {
                li = new ListItem();
                li.Value = g.IdGrano.ToString();
                li.Text = g.Descripcion;
                
                cboGrano.Items.Add(li);
            }

            for (int i = 0; i < dsChofer.Tables[0].Rows.Count; i++)
            {
                li = new ListItem();
                li.Value = dsChofer.Tables[0].Rows[i].ItemArray[0].ToString();
                li.Text = dsChofer.Tables[0].Rows[i].ItemArray[1].ToString();
                cboChofer.Items.Add(li);
            }

            for (int i = 0; i < dsEstOrigen.Tables[0].Rows.Count; i++)
            {
                li = new ListItem();
                li.Value = dsEstOrigen.Tables[0].Rows[i].ItemArray[0].ToString();
                li.Text = dsEstOrigen.Tables[0].Rows[i].ItemArray[1].ToString();
                cboProcedencia.Items.Add(li);
            }

            for (int i = 0; i < dsEstDestino.Tables[0].Rows.Count; i++)
            {
                li = new ListItem();
                li.Value = dsEstDestino.Tables[0].Rows[i].ItemArray[0].ToString();
                li.Text = dsEstDestino.Tables[0].Rows[i].ItemArray[1].ToString();
                cboDestino.Items.Add(li);
            }
        }
        #endregion

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Validaciones())
            {
                string fd = Request.Form[txtDateDesde.UniqueID];
                string fh = Request.Form[txtDateHasta.UniqueID];

                string[] fechaDesde = fd.Trim().Substring(0, 10).Split('/');
                DateTime FD = new DateTime(Convert.ToInt32(fechaDesde[2]), Convert.ToInt32(fechaDesde[1]), Convert.ToInt32(fechaDesde[0]));

                string[] fechaHasta = fh.Trim().Substring(0, 10).Split('/');
                DateTime FH = new DateTime(Convert.ToInt32(fechaHasta[2]), Convert.ToInt32(fechaHasta[1]), Convert.ToInt32(fechaHasta[0]));

                string Grano = cboGrano.SelectedValue == "-1" ? string.Empty : cboGrano.SelectedItem.Text;
                int Titular = cboTitular.SelectedValue == "-1" ? 0 : Convert.ToInt32(cboTitular.SelectedValue);
                int Intermediario = cboIntermediario.SelectedValue == "-1" ? 0 : Convert.ToInt32(cboIntermediario.SelectedValue);
                int RemitenteComercial = cboRemitenteComercial.SelectedValue == "-1" ? 0 : Convert.ToInt32(cboRemitenteComercial.SelectedValue);
                int Corredor = cboCorredor.SelectedValue == "-1" ? 0 : Convert.ToInt32(cboCorredor.SelectedValue);
                int RepresentanteEntregador = cboRepresentanteEntregador.SelectedValue == "-1" ? 0 : Convert.ToInt32(cboRepresentanteEntregador.SelectedValue);
                int Destinatario = cboDestinatario.SelectedValue == "-1" ? 0 : Convert.ToInt32(cboDestinatario.SelectedValue);
                int Transportista = cboTransportista.SelectedValue == "-1" ? 0 : Convert.ToInt32(cboTransportista.SelectedValue);
                int Chofer = cboChofer.SelectedValue == "-1" ? 0 : Convert.ToInt32(cboChofer.SelectedValue);
                int Procedencia = cboProcedencia.SelectedValue == "-1" ? 0 : Convert.ToInt32(cboProcedencia.SelectedValue);
                int Destino = cboDestino.SelectedValue == "-1" ? 0 : Convert.ToInt32(cboDestino.SelectedValue);
                int Cosecha = cboCosecha.SelectedValue == "-1" ? 0 : Convert.ToInt32(cboCosecha.SelectedValue);

                var data = ReporteLoteCDPDAO.Instance.GetReporteCDP(FD, FH, Grano, Titular, Intermediario, RemitenteComercial, Corredor, RepresentanteEntregador, Destinatario, Transportista, Chofer, Procedencia, Destino, Cosecha);

                if (data.Rows.Count > 0)
                {
                    var excelPackage = GetExcelPackage(data);

                    Response.Buffer = true;
                    Response.Clear();
                    Response.AddHeader("content-disposition", "attachment; filename=" + string.Format("Lote_CDP_{0}.xlsx", DateTime.Now.ToString("dd/MM/yyyy")));
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.BinaryWrite(excelPackage.GetAsByteArray());

                    Response.End();
                }
                else
                    lblMensaje.Text = "No se encontraron datos para el filtro seleccionado";
            }
        }

        private ExcelPackage GetExcelPackage(DataTable data)
        {
            //File para CRESUD
            string File = "Lote_CDP";

            if (!App.Usuario.Empresa.Descripcion.ToUpper().Contains("CRESUD"))
                File = "Lote_CDP_Cresca";

            FileInfo template = new FileInfo(String.Format(@"{0}Reports\{1}.xlsx", AppDomain.CurrentDomain.BaseDirectory, File));
            ExcelPackage pck = new ExcelPackage(template, true);
            var ws = pck.Workbook.Worksheets[1];
            string LogoImg = "LogoCresud";

            if (App.Usuario.Empresa.Descripcion.ToUpper().Contains("CRESCA"))
                LogoImg = "LogoCresca";

            //Para habilitar los Logos de todas las empresas hay que conseguirlos.
            if (App.Usuario.Empresa.Descripcion.ToUpper().Contains("CRESUD") || App.Usuario.Empresa.Descripcion.ToUpper().Contains("CRESCA"))
            {
                FileInfo Logo = new FileInfo(String.Format(@"{0}Content\Images\Logos\{1}.png", AppDomain.CurrentDomain.BaseDirectory, LogoImg));
                var picture = ws.Drawings.AddPicture("Logo", Logo);
                picture.SetPosition(0, 0);
            }

            SetExcel(data, ws);

            return pck;
        }

        private void SetExcel(DataTable data, ExcelWorksheet ws)
        {
            var row = 5;
            #region CRESUD
            if (App.Usuario.Empresa.Descripcion.ToUpper().Contains("CRESUD"))
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    row += 1;
                    ws.Cells[row, 1].Value = data.Rows[i]["IdSolicitud"].ToString();
                    ws.Cells[row, 2].Value = data.Rows[i]["TipoCarta"].ToString();
                    ws.Cells[row, 3].Value = data.Rows[i]["ObservacionAfip"].ToString();
                    ws.Cells[row, 4].Value = data.Rows[i]["NumeroCartaDePorte"].ToString();
                    ws.Cells[row, 5].Value = data.Rows[i]["Cee"].ToString();
                    ws.Cells[row, 6].Value = data.Rows[i]["Ctg"].ToString();
                    ws.Cells[row, 7].Value = data.Rows[i]["FechaDeEmision"].ToString();
                    ws.Cells[row, 8].Value = data.Rows[i]["FechaDeCarga"].ToString();
                    ws.Cells[row, 9].Value = data.Rows[i]["FechaDeVencimiento"].ToString();
                    ws.Cells[row, 10].Value = data.Rows[i]["TitularCDP"].ToString();
                    ws.Cells[row, 11].Value = data.Rows[i]["Intermediario"].ToString();
                    ws.Cells[row, 12].Value = data.Rows[i]["CteRemitenteComecial"].ToString();
                    ws.Cells[row, 13].Value = Tools.Value2<bool>(data.Rows[i]["EsCanjeador"].ToString(), false);
                    ws.Cells[row, 14].Value = data.Rows[i]["CteCorredor"].ToString();
                    ws.Cells[row, 15].Value = data.Rows[i]["Entregador"].ToString();
                    ws.Cells[row, 16].Value = data.Rows[i]["Destinatario"].ToString();
                    ws.Cells[row, 17].Value = data.Rows[i]["Destino"].ToString();
                    ws.Cells[row, 18].Value = data.Rows[i]["Transportista"].ToString();
                    ws.Cells[row, 19].Value = data.Rows[i]["CTransportista"].ToString();
                    ws.Cells[row, 20].Value = data.Rows[i]["Chofer"].ToString();
                    ws.Cells[row, 21].Value = data.Rows[i]["CosechaDescripcion"].ToString();
                    ws.Cells[row, 22].Value = data.Rows[i]["Grano"].ToString();
                    ws.Cells[row, 23].Value = data.Rows[i]["NumeroContrato"].ToString();
                    ws.Cells[row, 24].Value = Tools.Value2<bool>(data.Rows[i]["CargaPesadaDestino"].ToString(), false);
                    ws.Cells[row, 25].Value = data.Rows[i]["KilogramosEstimados"].ToString();
                    ws.Cells[row, 26].Value = (Enums.ConformeCondicional)Convert.ToInt32(data.Rows[i]["ConformeCondicional"].ToString());
                    ws.Cells[row, 27].Value = data.Rows[i]["PesoBruto"].ToString();
                    ws.Cells[row, 28].Value = data.Rows[i]["PesoTara"].ToString();
                    ws.Cells[row, 29].Value = data.Rows[i]["PesoNeto"].ToString();
                    ws.Cells[row, 30].Value = data.Rows[i]["Observaciones"].ToString();
                    ws.Cells[row, 31].Value = data.Rows[i]["EstProcedencia"].ToString();
                    ws.Cells[row, 32].Value = data.Rows[i]["EstDestino"].ToString();
                    ws.Cells[row, 33].Value = data.Rows[i]["PatenteCamion"].ToString();
                    ws.Cells[row, 34].Value = data.Rows[i]["PatenteAcoplado"].ToString();
                    ws.Cells[row, 35].Value = data.Rows[i]["KmRecorridos"].ToString();
                    ws.Cells[row, 36].Value = (Enums.EstadoFlete)Convert.ToInt32(data.Rows[i]["EstadoFlete"].ToString());
                    ws.Cells[row, 37].Value = data.Rows[i]["CantHoras"].ToString();
                    ws.Cells[row, 38].Value = data.Rows[i]["TarifaReferencia"].ToString();
                    ws.Cells[row, 39].Value = data.Rows[i]["TarifaReal"].ToString();
                    ws.Cells[row, 40].Value = data.Rows[i]["CtePagador"].ToString();
                    ws.Cells[row, 41].Value = (Enums.EstadoEnAFIP)Convert.ToInt32(data.Rows[i]["EstadoEnAFIP"].ToString());
                    ws.Cells[row, 42].Value = (Enums.EstadoEnvioSAP)Convert.ToInt32(data.Rows[i]["EstadoEnSAP"].ToString());
                    ws.Cells[row, 43].Value = data.Rows[i]["EstDestinoCambio"].ToString();
                    ws.Cells[row, 44].Value = data.Rows[i]["CteDestinatarioCambio"].ToString();
                    ws.Cells[row, 45].Value = data.Rows[i]["CodigoAnulacionAfip"].ToString();
                    ws.Cells[row, 46].Value = data.Rows[i]["FechaAnulacionAfip"].ToString();
                    ws.Cells[row, 47].Value = data.Rows[i]["CodigoRespuestaEnvioSAP"].ToString();
                    ws.Cells[row, 48].Value = data.Rows[i]["CodigoRespuestaAnulacionSAP"].ToString();
                    ws.Cells[row, 49].Value = data.Rows[i]["FechaCreacion"].ToString();
                    ws.Cells[row, 50].Value = data.Rows[i]["UsuarioCreacion"].ToString();
                    ws.Cells[row, 51].Value = data.Rows[i]["FechaModificacion"].ToString();
                    ws.Cells[row, 52].Value = data.Rows[i]["UsuarioModificacion"].ToString();
                }
            #endregion

            #region Otros
            if (!App.Usuario.Empresa.Descripcion.ToUpper().Contains("CRESUD"))
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    row += 1;
                    ws.Cells[row, 1].Value = data.Rows[i]["IdSolicitud"].ToString();
                    ws.Cells[row, 2].Value = data.Rows[i]["TipoCarta"].ToString();
                    ws.Cells[row, 3].Value = data.Rows[i]["NumeroCartaDePorte"].ToString();
                    ws.Cells[row, 4].Value = data.Rows[i]["Cee"].ToString();
                    ws.Cells[row, 5].Value = data.Rows[i]["FechaDeEmision"].ToString();
                    ws.Cells[row, 6].Value = data.Rows[i]["FechaDeCarga"].ToString();
                    ws.Cells[row, 7].Value = data.Rows[i]["FechaDeVencimiento"].ToString();
                    ws.Cells[row, 8].Value = data.Rows[i]["TitularCDP"].ToString();
                    ws.Cells[row, 9].Value = data.Rows[i]["Intermediario"].ToString();
                    ws.Cells[row, 10].Value = data.Rows[i]["CteRemitenteComecial"].ToString();
                    ws.Cells[row, 11].Value = Tools.Value2<bool>(data.Rows[i]["EsCanjeador"].ToString(), false);
                    ws.Cells[row, 12].Value = data.Rows[i]["CteCorredor"].ToString();
                    ws.Cells[row, 13].Value = data.Rows[i]["Entregador"].ToString();
                    ws.Cells[row, 14].Value = data.Rows[i]["Destinatario"].ToString();
                    ws.Cells[row, 15].Value = data.Rows[i]["Destino"].ToString();
                    ws.Cells[row, 16].Value = data.Rows[i]["Transportista"].ToString();
                    ws.Cells[row, 17].Value = data.Rows[i]["CTransportista"].ToString();
                    ws.Cells[row, 18].Value = data.Rows[i]["Chofer"].ToString();
                    ws.Cells[row, 19].Value = data.Rows[i]["CosechaDescripcion"].ToString();
                    ws.Cells[row, 20].Value = data.Rows[i]["Grano"].ToString();
                    ws.Cells[row, 21].Value = data.Rows[i]["NumeroContrato"].ToString();
                    ws.Cells[row, 22].Value = Tools.Value2<bool>(data.Rows[i]["CargaPesadaDestino"].ToString(), false);
                    ws.Cells[row, 23].Value = data.Rows[i]["KilogramosEstimados"].ToString();
                    ws.Cells[row, 24].Value = (Enums.ConformeCondicional)Convert.ToInt32(data.Rows[i]["ConformeCondicional"].ToString());
                    ws.Cells[row, 25].Value = data.Rows[i]["PesoBruto"].ToString();
                    ws.Cells[row, 26].Value = data.Rows[i]["PesoTara"].ToString();
                    ws.Cells[row, 27].Value = data.Rows[i]["PesoNeto"].ToString();
                    ws.Cells[row, 28].Value = data.Rows[i]["Observaciones"].ToString();
                    ws.Cells[row, 29].Value = data.Rows[i]["EstProcedencia"].ToString();
                    ws.Cells[row, 30].Value = data.Rows[i]["EstDestino"].ToString();
                    ws.Cells[row, 31].Value = data.Rows[i]["PatenteCamion"].ToString();
                    ws.Cells[row, 32].Value = data.Rows[i]["PatenteAcoplado"].ToString();
                    ws.Cells[row, 33].Value = data.Rows[i]["KmRecorridos"].ToString();
                    ws.Cells[row, 34].Value = (Enums.EstadoFlete)Convert.ToInt32(data.Rows[i]["EstadoFlete"].ToString());
                    ws.Cells[row, 35].Value = data.Rows[i]["CantHoras"].ToString();
                    ws.Cells[row, 36].Value = data.Rows[i]["TarifaReferencia"].ToString();
                    ws.Cells[row, 37].Value = data.Rows[i]["TarifaReal"].ToString();
                    ws.Cells[row, 38].Value = data.Rows[i]["CtePagador"].ToString();
                    ws.Cells[row, 39].Value = (Enums.EstadoEnvioSAP)Convert.ToInt32(data.Rows[i]["EstadoEnSAP"].ToString());
                    ws.Cells[row, 40].Value = data.Rows[i]["EstDestinoCambio"].ToString();
                    ws.Cells[row, 41].Value = data.Rows[i]["CteDestinatarioCambio"].ToString();
                    ws.Cells[row, 42].Value = data.Rows[i]["CodigoRespuestaEnvioSAP"].ToString();
                    ws.Cells[row, 43].Value = data.Rows[i]["CodigoRespuestaAnulacionSAP"].ToString();

                    //Porcentajes
                    if (App.Usuario.Empresa.Descripcion.ToUpper().Contains("CRESCA"))
                    {
                        ws.Cells[row, 44].Value = data.Rows[i]["PHumedad"].ToString();
                        ws.Cells[row, 45].Value = data.Rows[i]["POtros"].ToString();
                        ws.Cells[row, 46].Value = data.Rows[i]["FechaCreacion"].ToString();
                        ws.Cells[row, 47].Value = data.Rows[i]["UsuarioCreacion"].ToString();
                        ws.Cells[row, 48].Value = data.Rows[i]["FechaModificacion"].ToString();
                        ws.Cells[row, 49].Value = data.Rows[i]["UsuarioModificacion"].ToString();
                    }

                    if (!App.Usuario.Empresa.Descripcion.ToUpper().Contains("CRESCA"))
                    {
                        ws.Cells[row, 44].Value = data.Rows[i]["FechaCreacion"].ToString();
                        ws.Cells[row, 45].Value = data.Rows[i]["UsuarioCreacion"].ToString();
                        ws.Cells[row, 46].Value = data.Rows[i]["FechaModificacion"].ToString();
                        ws.Cells[row, 47].Value = data.Rows[i]["UsuarioModificacion"].ToString();
                    }

                }
            #endregion
        }

        private Boolean Validaciones()
        {
            Boolean result = true;
            string mensaje = string.Empty;
            lblMensaje.Text = string.Empty;

            string fd = Request.Form[txtDateDesde.UniqueID];
            string fh = Request.Form[txtDateHasta.UniqueID];

            if (String.IsNullOrEmpty(fd))
                mensaje += "Debe completar FechaDesde<br>";
            if (String.IsNullOrEmpty(fh))
                mensaje += "Debe completar FechaHasta<br>";

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
    }
}
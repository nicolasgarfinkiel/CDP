using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Configuration;

using CartaDePorte.Core.Domain;
using CartaDePorte.Core.DAO;
using CartaDePorte.Core.Servicios;
using CartaDePorte.Core.Domain.Seguridad;
using CartaDePorte.Core;

namespace CartaDePorte.Web
{
    public partial class BandejaDeEntrada : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            var master = (Main)Page.Master;
            master.ValidarMantenimiento();
            master.HiddenValue = "Recibidas";


            CargarTitulos();
            //Datos();
            if (!IsPostBack)
            {
                if (!App.UsuarioTienePermisos("Bandeja de Entrada"))
                {
                    Response.Redirect("~/SinAutorizacion.aspx");
                    return;
                }

                DatosFiltro(string.Empty);
            }
            
        }

        private void CargarTitulos()
        {
            tblData.Rows.Clear();
            var row = new TableRow();
            row.CssClass = "TableRowTitle";
            //row.Cells.Add(AddTitleCell("", 5));
            row.Cells.Add(AddTitleCell("Id", 5,"Identificador interno"));
            row.Cells.Add(AddTitleCell("Nro Carta Porte", 50, "Nro Carta Porte"));
            row.Cells.Add(AddTitleCell("Ctg", 50,"Codigo enviado por AFIP"));
            row.Cells.Add(AddTitleCell("Tipo De Carta", 150, "Tipo De Carta"));
            row.Cells.Add(AddTitleCell("Titular", 150, "Titular Carta De Porte"));
            row.Cells.Add(AddTitleCell("Establecimiento Procedencia", 200, "Establecimiento Origen"));
            row.Cells.Add(AddTitleCell("Fecha de Emisión", 150, "Fecha de Emisión"));
            row.Cells.Add(AddTitleCell("Usuario", 100, "Usuario de creación"));
            row.Cells.Add(AddTitleCell("", 5,"Ver o Editar Solicitud de Carta de Porte"));

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

        private TableCell AddTitleCell(string texto, int width,string tooltip)
        {
            var cell = new TableCell();
            cell.Text = texto;
            cell.ToolTip = tooltip;
            cell.Height = Unit.Pixel(40);
            cell.Width = Unit.Pixel(width);
            return cell;
        }

        #endregion

        private void Datos()
        {

            Boolean _ReenviosSAP = false;
            Boolean _BaseDeDatos = false;
            Boolean _SeguimientoEstados = false;
            Boolean _reenviosAFIP = false;

            if (App.Usuario == null || App.Usuario.UsuariosSeguridad.Count == 0)
            {
                return;
            }

            if (App.UsuarioTienePermisos("ReenviosSAP")){_ReenviosSAP = true;}
            if (App.UsuarioTienePermisos("BaseDeDatos")) { _BaseDeDatos = true; }
            if (App.UsuarioTienePermisos("SeguimientoEstados")) { _SeguimientoEstados = true; }
            if (App.UsuarioTienePermisos("ReenviosAFIP")) { _reenviosAFIP = true; }

            foreach (Solicitud solicitud in SolicitudDAO.Instance.GetTop(8))
            {
                lblCantidadResultados.Text = "Resultado de la busqueda: 8 registros";
                var row = new TableRow();
                row.CssClass = "TableRow";

                string linkMantenimiento = "<a href='contingenciasestados.aspx?Id=" + solicitud.IdSolicitud.ToString() +
                                "'><IMG border='0' src='Content/Images/Mantenimiento.png' width='16' height='16'></a>";

                if (_BaseDeDatos)
                {
                    linkMantenimiento = "<a href='contingencias.aspx?Id=" + solicitud.IdSolicitud.ToString() +
                                    "'><IMG border='0' src='Content/Images/Mantenimiento.png' width='16' height='16'></a>";
                }
                if (_SeguimientoEstados)
                {
                    linkMantenimiento = "<a href='contingenciasestados.aspx?Id=" + solicitud.IdSolicitud.ToString() +
                                    "'><IMG border='0' src='Content/Images/Mantenimiento.png' width='16' height='16'></a>";
                }
                
                if (!(_BaseDeDatos || _SeguimientoEstados))
                {
                    linkMantenimiento = string.Empty;
                }

                row.Cells.Add(AddCell(linkMantenimiento, string.Empty, HorizontalAlign.Center));

                row.Cells.Add(AddCell(solicitud.IdSolicitud.ToString(), solicitud.IdSolicitud.ToString(), HorizontalAlign.Justify));
                row.Cells.Add(AddCell(solicitud.NumeroCartaDePorte, solicitud.NumeroCartaDePorte, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(solicitud.Ctg, solicitud.Ctg, HorizontalAlign.Justify));
                row.Cells.Add(AddCell((solicitud.TipoDeCarta != null) ? solicitud.TipoDeCarta.Descripcion : string.Empty, (solicitud.TipoDeCarta != null) ? solicitud.TipoDeCarta.Descripcion : string.Empty, HorizontalAlign.Justify));
                row.Cells.Add(AddCell((solicitud.ProveedorTitularCartaDePorte != null) ? solicitud.ProveedorTitularCartaDePorte.Nombre : string.Empty, (solicitud.ProveedorTitularCartaDePorte != null) ? solicitud.ProveedorTitularCartaDePorte.Nombre : string.Empty, HorizontalAlign.Justify));
                row.Cells.Add(AddCell((solicitud.IdEstablecimientoProcedencia != null) ? solicitud.IdEstablecimientoProcedencia.Descripcion : string.Empty, (solicitud.IdEstablecimientoProcedencia != null) ? solicitud.IdEstablecimientoProcedencia.Descripcion : string.Empty, HorizontalAlign.Justify));

                string iconoVerde = "<center><a><IMG border='0' src='Content/Images/circle_green.png' width='12' height='12'></a></center>";
                string iconoRojo = "<center><a><IMG border='0' src='Content/Images/circle_red.png' width='12' height='12'></a></center>";
                string iconoAmarillo = "<center><a><IMG border='0' src='Content/Images/circle_yellow.png' width='12' height='12'></a></center>";
                string iconoCruz = "<center><a><IMG border='0' src='Content/Images/icon_Delete.png' width='12' height='12'></a></center>";
                string iconoNaranja = "<center><a><IMG border='0' src='Content/Images/Circle_Orange.png' width='12' height='12'></a></center>";
                string iconoCargaManual = "<center><a><IMG border='0' src='Content/Images/circle_greenManual.png' width='12' height='12'></a></center>";
                string iconoConfirmado = "<center><a><IMG border='0' src='Content/Images/iconArribo.png' width='12' height='12'></a></center>";
                string iconoConfirmadoDefinitivo = "<center><a><IMG border='0' src='Content/Images/iconArriboDefinitivo.png' width='12' height='12'></a></center>";
                string iconoRechazado = "<center><a><IMG border='0' src='Content/Images/IconRechazado.png' width='12' height='12'></a></center>";
                string iconoCambioDestino = "<center><a><IMG border='0' src='Content/Images/cambiodestino.png' width='12' height='12'></a></center>";
                string iconoVueltaOrigen = "<center><a><IMG border='0' src='Content/Images/vueltaorigen.png' width='12' height='12'></a></center>";
                string iconoEspera = "<center><a><IMG border='0' src='Content/Images/iconespera.png' width='12' height='12'></a></center>";

                if (solicitud.TipoDeCarta != null && solicitud.TipoDeCarta.Descripcion.Equals("Compra de granos"))
                {
                    row.Cells.Add(AddCell(string.Empty, splitCapitalizacion(solicitud.EstadoEnAFIP.ToString()), HorizontalAlign.Justify));                   
                }
                else {

                    if (solicitud.TipoDeCarta != null)
                    {
                        switch (solicitud.EstadoEnAFIP)
                        {
                            case Enums.EstadoEnAFIP.Enviado:
                                row.Cells.Add(AddCell(iconoAmarillo, splitCapitalizacion(solicitud.EstadoEnAFIP.ToString()), HorizontalAlign.Justify));
                                break;
                            case Enums.EstadoEnAFIP.Otorgado:
                                row.Cells.Add(AddCell(iconoVerde, splitCapitalizacion(solicitud.EstadoEnAFIP.ToString()), HorizontalAlign.Justify));
                                break;
                            case Enums.EstadoEnAFIP.SinProcesar:
                                row.Cells.Add(AddCell(iconoRojo, splitCapitalizacion(solicitud.EstadoEnAFIP.ToString()), HorizontalAlign.Justify));
                                break;
                            case Enums.EstadoEnAFIP.Anulada:
                                row.Cells.Add(AddCell(iconoCruz, splitCapitalizacion(solicitud.EstadoEnAFIP.ToString()) + "\n\rCodigo Anulación AFIP: " + solicitud.CodigoAnulacionAfip.ToString(), HorizontalAlign.Justify));
                                break;
                            case Enums.EstadoEnAFIP.CargaManual:
                                row.Cells.Add(AddCell(iconoCargaManual, splitCapitalizacion(solicitud.EstadoEnAFIP.ToString()), HorizontalAlign.Justify));
                                break;
                            case Enums.EstadoEnAFIP.Confirmado:
                                row.Cells.Add(AddCell(iconoConfirmado, splitCapitalizacion(solicitud.EstadoEnAFIP.ToString()), HorizontalAlign.Justify));
                                break;
                            case Enums.EstadoEnAFIP.ConfirmadoDefinitivo:
                                row.Cells.Add(AddCell(iconoConfirmadoDefinitivo, splitCapitalizacion(solicitud.EstadoEnAFIP.ToString()), HorizontalAlign.Justify));
                                break;
                            case Enums.EstadoEnAFIP.Rechazado:
                                row.Cells.Add(AddCell(iconoRechazado, splitCapitalizacion(solicitud.EstadoEnAFIP.ToString()), HorizontalAlign.Justify));
                                break;
                            case Enums.EstadoEnAFIP.CambioDestino:
                                row.Cells.Add(AddCell(iconoCambioDestino, splitCapitalizacion(solicitud.EstadoEnAFIP.ToString()), HorizontalAlign.Justify));
                                break;
                            case Enums.EstadoEnAFIP.VueltaOrigen:
                                row.Cells.Add(AddCell(iconoVueltaOrigen, splitCapitalizacion(solicitud.EstadoEnAFIP.ToString()), HorizontalAlign.Justify));
                                break;
                            default:
                                row.Cells.Add(AddCell(string.Empty, "estado no definido", HorizontalAlign.Justify));
                                break;
                        }

                    }
                    else 
                    {
                        row.CssClass = "TableRowReserva";
                        row.Cells.Add(AddCell(string.Empty, string.Empty, HorizontalAlign.Justify));
                    }

                }

                if (solicitud.TipoDeCarta != null)
                {
                    switch (solicitud.EstadoEnSAP)
                    {
                        case Enums.EstadoEnvioSAP.EnProceso:
                            row.Cells.Add(AddCell(iconoNaranja, splitCapitalizacion(solicitud.EstadoEnSAP.ToString()), HorizontalAlign.Justify));
                            break;
                        case Enums.EstadoEnvioSAP.FinalizadoConError:
                            row.Cells.Add(AddCell(iconoRojo, splitCapitalizacion(solicitud.EstadoEnSAP.ToString()), HorizontalAlign.Justify));
                            break;
                        case Enums.EstadoEnvioSAP.FinalizadoOk:
                            row.Cells.Add(AddCell(iconoVerde, splitCapitalizacion(solicitud.EstadoEnSAP.ToString()) + "\n\rCodigo Envio SAP:" + solicitud.CodigoRespuestaEnvioSAP.ToString(), HorizontalAlign.Justify));
                            break;
                        case Enums.EstadoEnvioSAP.Pendiente:
                            if (solicitud.EstadoEnAFIP == Enums.EstadoEnAFIP.SinProcesar)
                            {
                                row.Cells.Add(AddCell(iconoRojo, splitCapitalizacion(solicitud.EstadoEnSAP.ToString()), HorizontalAlign.Justify));
                            }
                            else
                            {
                                row.Cells.Add(AddCell(iconoAmarillo, splitCapitalizacion(solicitud.EstadoEnSAP.ToString()), HorizontalAlign.Justify));
                            }

                            break;
                        case Enums.EstadoEnvioSAP.PedidoAnulacion:
                            row.Cells.Add(AddCell(iconoNaranja, splitCapitalizacion(solicitud.EstadoEnSAP.ToString()), HorizontalAlign.Justify));
                            break;
                        case Enums.EstadoEnvioSAP.EnProcesoAnulacion:
                            row.Cells.Add(AddCell(iconoNaranja, splitCapitalizacion(solicitud.EstadoEnSAP.ToString()), HorizontalAlign.Justify));
                            break;
                        case Enums.EstadoEnvioSAP.Anulada:
                            row.Cells.Add(AddCell(iconoCruz, splitCapitalizacion(solicitud.EstadoEnSAP.ToString()) + "\n\rCodigo Anulación SAP:" + solicitud.CodigoRespuestaAnulacionSAP.ToString(), HorizontalAlign.Justify));
                            break;
                        case Enums.EstadoEnvioSAP.NoEnviadaASap:
                            row.Cells.Add(AddCell(string.Empty, splitCapitalizacion(solicitud.EstadoEnSAP.ToString()), HorizontalAlign.Justify));
                            break;
                        case Enums.EstadoEnvioSAP.PrimerEnvioTerceros:
                            row.Cells.Add(AddCell(iconoNaranja, splitCapitalizacion(solicitud.EstadoEnSAP.ToString()), HorizontalAlign.Justify));
                            break;
                        case Enums.EstadoEnvioSAP.EnEsperaPorProspecto:
                            row.Cells.Add(AddCell(iconoEspera, splitCapitalizacion(solicitud.EstadoEnSAP.ToString()), HorizontalAlign.Justify));
                            break;

                        default:
                            break;
                    }
                }else
                {
                    row.Cells.Add(AddCell(string.Empty, string.Empty, HorizontalAlign.Justify));
                } 

                row.Cells.Add(AddCell(solicitud.ObservacionAfip, solicitud.ObservacionAfip, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(solicitud.FechaCreacion.ToString("dd/MM/yyyy"), solicitud.FechaCreacion.ToString(), HorizontalAlign.Justify));
                row.Cells.Add(AddCell(solicitud.UsuarioCreacion.Split('\\')[1].ToString(), solicitud.UsuarioCreacion, HorizontalAlign.Justify));


                // REENVIAR AFIP
                string link = "<a href='Index.aspx?Id=" + solicitud.IdSolicitud.ToString() + "&reenvioAFIP=1" +
                        "'><IMG border='0' src='Content/Images/icon_select.gif'></a>";

                if (!_reenviosAFIP)
                    link = string.Empty;

                if (solicitud.EstadoEnAFIP == Enums.EstadoEnAFIP.Otorgado)
                    link = string.Empty;

                if (solicitud.EstadoEnAFIP == Enums.EstadoEnAFIP.Anulada)
                    link = string.Empty;

                if (solicitud.EstadoEnAFIP == Enums.EstadoEnAFIP.CargaManual)
                    link = string.Empty;

                if (solicitud.EstadoEnAFIP == Enums.EstadoEnAFIP.Confirmado)
                    link = string.Empty;

                if (solicitud.EstadoEnAFIP == Enums.EstadoEnAFIP.ConfirmadoDefinitivo)
                    link = string.Empty;

                if (solicitud.EstadoEnAFIP == Enums.EstadoEnAFIP.Rechazado)
                    link = string.Empty;

                if (solicitud.EstadoEnAFIP == Enums.EstadoEnAFIP.CambioDestino)
                    link = string.Empty;

                if (solicitud.EstadoEnAFIP == Enums.EstadoEnAFIP.VueltaOrigen)
                    link = string.Empty;


                if (solicitud.TipoDeCarta != null && solicitud.TipoDeCarta.Descripcion.Equals("Compra de granos"))
                {
                    link = iconoCruz;
                }

                if (solicitud.TipoDeCarta != null && solicitud.TipoDeCarta.Descripcion.Equals("Venta de granos de terceros"))
                {
                    link = iconoCruz;
                }

                if (solicitud.TipoDeCarta == null)
                {
                    link = string.Empty;
                }

                row.Cells.Add(AddCell(link, string.Empty, HorizontalAlign.Center));

                // REENVIAR A SAP
                link = string.Empty;
                if (solicitud.EstadoEnSAP == Enums.EstadoEnvioSAP.FinalizadoConError)
                {
                    //link = "<a href='Index.aspx?Id=" + solicitud.IdSolicitud.ToString() + "&reenvioSAP=1" +
                    //        "'><IMG border='0' src='Content/Images/icon_select.gif'></a>";
                    link = "<a href='javascript:ReenviarSAP(" + solicitud.IdSolicitud.ToString() + ")'><IMG border='0' src='Content/Images/icon_select.gif'></a>";		
                }
                //if (solicitud.EstadoEnAFIP == Enums.EstadoEnAFIP.Anulada)
                //{
                //    link = string.Empty;
                //}
                if (solicitud.TipoDeCarta != null && solicitud.TipoDeCarta.Descripcion.Equals("Compra de granos que transportamos"))
                {
                    link = iconoCruz;               
                }
                if (solicitud.TipoDeCarta != null && solicitud.TipoDeCarta.Descripcion.Equals("Compra de granos"))
                {
                    link = iconoCruz;
                }
                if (solicitud.TipoDeCarta == null)
                {
                    link = string.Empty;
                }

                if (solicitud.EstadoEnSAP == Enums.EstadoEnvioSAP.PrimerEnvioTerceros)
                {
                    link = string.Empty;
                }


                if (!_ReenviosSAP)
                {
                    link = string.Empty;
                }

                row.Cells.Add(AddCell(link, string.Empty, HorizontalAlign.Center));

                // Lapiz
                link = string.Empty;
                string tool = string.Empty;
                if (!String.IsNullOrEmpty(solicitud.MensajeRespuestaEnvioSAP))
                {
                    link = "<a href='LogSapList.aspx?cdp=" + solicitud.NumeroCartaDePorte +
                    "'><IMG border='0' src='Content/Images/logsap.png' width='16' height='16'></a>";                
                    tool = "Ver detalle respuesta SAP";
                }

                if (!App.UsuarioTienePermisos("Visualizacion Log SAP"))
                    link = string.Empty;


                row.Cells.Add(AddCell(link, tool, HorizontalAlign.Center));

                // Reporte
                link = string.Empty;

                if (solicitud.EstadoEnAFIP != Enums.EstadoEnAFIP.Anulada)
                {
                    link = "<a target='_new' href='ReportePDF.aspx?Id=" + solicitud.IdSolicitud.ToString() +
                    "'><IMG border='0' src='Content/Images/folder-print.png' width='16' height='16'></a>";
                }

                if (!App.UsuarioTienePermisos("Imprimir Solicitud"))
                    link = string.Empty;

                row.Cells.Add(AddCell(link, "Imprimir Cartas de Porte", HorizontalAlign.Center));

                // LUPA
                link = "<a href='Index.aspx?Id=" + solicitud.IdSolicitud.ToString() +
                        "'><IMG border='0' src='Content/Images/magnify.gif'></a>";

                if (solicitud.ObservacionAfip.Contains("Reserva"))
                {
                    link = "<a href='Index.aspx?Id=" + solicitud.IdSolicitud.ToString() +
                            "'><IMG border='0' src='Content/Images/cargaCartaDePorteReservada.png'></a>";
                }

                if (solicitud.EstadoEnAFIP == Enums.EstadoEnAFIP.Anulada && solicitud.EstadoEnSAP == Enums.EstadoEnvioSAP.Anulada)
                {
                    link = string.Empty;
                }

                if (!App.UsuarioTienePermisos("Visualizacion Solicitud"))
                    link = string.Empty;

                row.Cells.Add(AddCell(link, "Abrir Solicitud", HorizontalAlign.Center));


                tblData.Rows.Add(row);

            }
        }
        private void DatosFiltro(string busqueda)
        {
            Session["totalList"] = null;
            Session["tableList"] = null;
            Session["totalList"] = SolicitudRecibidaDAO.Instance.GetFiltro(busqueda);
            Session["tableList"] = addMoreRows();
            popTable();

        }

        private void popTable()
        {

            IList<SolicitudRecibida> tableList = new List<SolicitudRecibida>();
            if ((Session["tableList"] != null))
                tableList = (IList<SolicitudRecibida>)Session["tableList"];

            IList<SolicitudRecibida> totalList = new List<SolicitudRecibida>();
            if ((Session["totalList"] != null))
                totalList = (IList<SolicitudRecibida>)Session["totalList"];


            if (totalList.Count > 1)
                lblCantidadResultados.Text = "Resultado de la busqueda: " + totalList.Count.ToString() + " registros";
            else if (totalList.Count == 1)
                lblCantidadResultados.Text = "Resultado de la busqueda: " + totalList.Count.ToString() + " registro";
            else
                lblCantidadResultados.Text = "Resultado de la busqueda: 0 registro";

            foreach (SolicitudRecibida solicitud in tableList)
            {
                var row = new TableRow();
                row.CssClass = "TableRow";

                row.Cells.Add(AddCell(solicitud.IdSolicitudRecibida.ToString(), solicitud.IdSolicitudRecibida.ToString(), HorizontalAlign.Justify));
                row.Cells.Add(AddCell(solicitud.NumeroCartaDePorte, solicitud.NumeroCartaDePorte, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(solicitud.Ctg, solicitud.Ctg, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(splitCapitalizacion(solicitud.TipoDeCartaString), splitCapitalizacion(solicitud.TipoDeCartaString), HorizontalAlign.Justify));
                row.Cells.Add(AddCell(solicitud.ProveedorTitularCartaDePorteString, solicitud.ProveedorTitularCartaDePorteString, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(solicitud.EstablecimientoProcedenciaString, solicitud.EstablecimientoProcedenciaString, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(solicitud.FechaCreacion.ToString("dd/MM/yyyy"), solicitud.FechaCreacion.ToString(), HorizontalAlign.Justify));
                row.Cells.Add(AddCell(solicitud.UsuarioCreacion.Split('\\')[1].ToString(), solicitud.UsuarioCreacion, HorizontalAlign.Justify));

                // LUPA
                String link = "<a href='CargaEnDestino.aspx?Id=" + solicitud.IdSolicitudRecibida.ToString() +
                        "'><IMG border='0' src='Content/Images/magnify.gif'></a>";

                row.Cells.Add(AddCell(link, "Abrir Solicitud", HorizontalAlign.Center));

                tblData.Rows.Add(row);


            }

        
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            lblCantidadActual.Text = string.Empty;
            CargarTitulos();
            DatosFiltro(txtBuscar.Text.Trim());
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
        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            btnBuscar_Click(null, null);
        }
        private IList<SolicitudRecibida> addMoreRows()
        {

            IList<SolicitudRecibida> tableList = new List<SolicitudRecibida>();
            if ((Session["tableList"] != null))
                tableList = (IList<SolicitudRecibida>)Session["tableList"];

            IList<SolicitudRecibida> totalList = new List<SolicitudRecibida>();
            if ((Session["totalList"] != null))
                totalList = (IList<SolicitudRecibida>)Session["totalList"];

            int rowpagina = 10;
            int cntTableList = tableList.Count;

            int cnt = 0;
            if (Session["totalList"] != null)
            {
                foreach (SolicitudRecibida sol in totalList)
                {
                    if (!tableList.Contains(sol))
                    {
                        if (cnt < rowpagina)
                        {
                            tableList.Add(sol);
                            cnt++;
                        }
                    }
                }

            }

            return tableList;        
        }

        public void MasDatos()
        {
            Session["tableList"] = addMoreRows();
            popTable();

        }

        protected void btnCargarMas_Click(object sender, EventArgs e)
        {
            MasDatos();

            IList<SolicitudRecibida> tmpList = new List<SolicitudRecibida>();
            if ((Session["tableList"] != null))
                tmpList = (IList<SolicitudRecibida>)Session["tableList"];

            IList<SolicitudRecibida> tmpList2 = new List<SolicitudRecibida>();
            if ((Session["totalList"] != null))
                tmpList2 = (IList<SolicitudRecibida>)Session["totalList"];

            lblCantidadActual.Text = "Registros cargados: " + tmpList.Count.ToString() + " de " + tmpList2.Count.ToString();
            lblCantidadActual.ForeColor = Color.DarkGreen;

        }

        protected void btnNueva_Click(object sender, EventArgs e)
        {
            Response.Redirect("CargaEnDestino.aspx");
        }

 
    
    }
}

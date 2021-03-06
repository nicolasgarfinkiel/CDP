﻿using System;
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
using CartaDePorte.Common;
using System.IO;

namespace CartaDePorte.Web
{
    public partial class BandejaDeSalida : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var master = (Main)Page.Master;
            master.ValidarMantenimiento();
            master.HiddenValue = "Bandeja de Salida";
            ConfirmacionReproceso.Attributes.Add("style", "visibility :hidden");

            if (Session["Pais"] == null)
                Session["Pais"] = GrupoEmpresaDAO.Instance.GetOne(App.Usuario.IdGrupoEmpresa).Pais.Descripcion.ToUpper();
            else if (Session["Pais"].ToString() != App.Usuario.IdGrupoEmpresa.ToString())
                Session["Pais"] = GrupoEmpresaDAO.Instance.GetOne(App.Usuario.IdGrupoEmpresa).Pais.Descripcion.ToUpper();

            CargarTitulos();

            if (!IsPostBack)
            {
                if (!App.UsuarioTienePermisos("Bandeja de Salida"))
                {
                    Response.Redirect("~/SinAutorizacion.aspx");
                    return;
                }
                CargoCombos();
                DatosFiltro(string.Empty, "-1", "-1");
                lblBusqueda.Text = ConfiguracionRegional.ChagedTextCulture("CDP/CTG/Usuario/Establecimiento Origen", Session["Pais"].ToString());
            }
        }

        private void CargarTitulos()
        {
            tblData.Rows.Clear();
            var row = new TableRow();
            row.CssClass = "TableRowTitle";
            if (Session["Pais"].ToString() == "ARGENTINA")
            {
                row.Cells.Add(AddTitleCell("", 5));
                row.Cells.Add(AddTitleCell("Id", 5, "Identificador interno"));
                row.Cells.Add(AddTitleCell("Nro Carta Porte", 50, "Nro Carta Porte"));
                row.Cells.Add(AddTitleCell("Ctg", 50, "Codigo enviado por AFIP"));
                row.Cells.Add(AddTitleCell("Tipo De Carta", 100, "Tipo De Carta"));
                row.Cells.Add(AddTitleCell("Titular", 150, "Titular Carta De Porte"));
                row.Cells.Add(AddTitleCell("Establecimiento Procedencia", 100, "Establecimiento Origen"));
                row.Cells.Add(AddTitleCell("Afip", 2, "Estado en AFIP"));
                row.Cells.Add(AddTitleCell("SAP", 2, "Estado en SAP"));
                row.Cells.Add(AddTitleCell("Observaciones AFIP", 120, "Observaciones AFIP"));
                row.Cells.Add(AddTitleCell("Fecha", 40, "Fecha de emision"));
                row.Cells.Add(AddTitleCell("Usuario", 40, "Usuario de creación"));
                row.Cells.Add(AddTitleCell("Re AFIP", 5, "Acción de Reenvio de la solicitud a AFIP"));
                row.Cells.Add(AddTitleCell("Re SAP", 5, "Acción de Reenvio de la solicitud a SAP"));
                row.Cells.Add(AddTitleCell("Log Sap", 5, "Log de respuestas de SAP"));
                row.Cells.Add(AddTitleCell("", 5, "Impresion de la Carta de Porte"));
                row.Cells.Add(AddTitleCell("", 5, "Ver o Editar Solicitud de Carta de Porte"));
            }
            else
            {
                row.Cells.Add(AddTitleCell("", 5));
                row.Cells.Add(AddTitleCell("Id", 5, "Identificador interno"));
                row.Cells.Add(AddTitleCell("Nro Carta Porte", 100, "Nro Carta Porte"));
                row.Cells.Add(AddTitleCell("Tipo De Carta", 100, "Tipo De Carta"));
                row.Cells.Add(AddTitleCell("Titular", 150, "Titular Carta De Porte"));
                row.Cells.Add(AddTitleCell("Establecimiento Procedencia", 150, "Establecimiento Origen"));
                row.Cells.Add(AddTitleCell("SAP", 2, "Estado en SAP"));
                row.Cells.Add(AddTitleCell("Fecha", 40, "Fecha de emision"));
                row.Cells.Add(AddTitleCell("Usuario", 40, "Usuario de creación"));
                row.Cells.Add(AddTitleCell("Re SAP", 45, "Acción de Reenvio de la solicitud a SAP"));
                row.Cells.Add(AddTitleCell("Log Sap", 45, "Log de respuestas de SAP"));
                row.Cells.Add(AddTitleCell("", 5, "Impresion de la Carta de Porte"));
                row.Cells.Add(AddTitleCell("", 5, "Ver o Editar Solicitud de Carta de Porte"));
            }
            tblData.Rows.Add(row);
        }

        private void CargoCombos()
        {
            ListItem li;

            if (Session["Pais"].ToString() != "ARGENTINA")
            {
                Label4.Visible = false;
                cboEstadoAfip.Visible = false;
            }
            else
            {
                li = new ListItem();
                li.Text = "Todos";
                li.Value = "-1";
                cboEstadoAfip.Items.Add(li);

                foreach (Enums.EstadoEnAFIP item in Enum.GetValues(typeof(Enums.EstadoEnAFIP)))
                {
                    li = new ListItem();
                    li.Value = Convert.ToInt32(item).ToString();
                    li.Text = splitCapitalizacion(item.ToString());
                    cboEstadoAfip.Items.Add(li);
                }
            }

            li = new ListItem();
            li.Text = "Todos";
            li.Value = "-1";
            cboEstadoSAP.Items.Add(li);

            foreach (Enums.EstadoEnvioSAP item in Enum.GetValues(typeof(Enums.EstadoEnvioSAP)))
            {
                li = new ListItem();
                li.Value = Convert.ToInt32(item).ToString();
                li.Text = splitCapitalizacion(item.ToString());
                cboEstadoSAP.Items.Add(li);
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

        private TableCell AddTitleCell(string texto, int width, string tooltip)
        {
            var cell = new TableCell();
            cell.Text = texto;
            cell.ToolTip = tooltip;
            cell.Height = Unit.Pixel(40);
            cell.Width = Unit.Pixel(width);
            return cell;
        }
        #endregion
        #region EnDesuso(No se para que lo hicieron)
        //private void Datos()
        //{
        //    Boolean _ReenviosSAP = false;
        //    Boolean _BaseDeDatos = false;
        //    Boolean _SeguimientoEstados = false;
        //    Boolean _reenviosAFIP = false;

        //    if (App.Usuario == null || App.Usuario.UsuariosSeguridad.Count == 0)
        //    {
        //        return;
        //    }

        //    if (App.UsuarioTienePermisos("ReenviosSAP")) { _ReenviosSAP = true; }
        //    if (App.UsuarioTienePermisos("BaseDeDatos")) { _BaseDeDatos = true; }
        //    if (App.UsuarioTienePermisos("SeguimientoEstados")) { _SeguimientoEstados = true; }
        //    if (App.UsuarioTienePermisos("ReenviosAFIP")) { _reenviosAFIP = true; }

        //    foreach (Solicitud solicitud in SolicitudDAO.Instance.GetTop(8))
        //    {
        //        lblCantidadResultados.Text = "Resultado de la busqueda: 8 registros";
        //        var row = new TableRow();
        //        row.CssClass = "TableRow";

        //        string linkMantenimiento = "<a href='contingenciasestados.aspx?Id=" + solicitud.IdSolicitud.ToString() +
        //                        "'><IMG border='0' src='Content/Images/Mantenimiento.png' width='16' height='16'></a>";

        //        if (_BaseDeDatos)
        //        {
        //            linkMantenimiento = "<a href='contingencias.aspx?Id=" + solicitud.IdSolicitud.ToString() +
        //                            "'><IMG border='0' src='Content/Images/Mantenimiento.png' width='16' height='16'></a>";
        //        }
        //        if (_SeguimientoEstados)
        //        {
        //            linkMantenimiento = "<a href='contingenciasestados.aspx?Id=" + solicitud.IdSolicitud.ToString() +
        //                            "'><IMG border='0' src='Content/Images/Mantenimiento.png' width='16' height='16'></a>";
        //        }
        //        if (!(_BaseDeDatos || _SeguimientoEstados))
        //        {
        //            linkMantenimiento = string.Empty;
        //        }

        //        row.Cells.Add(AddCell(linkMantenimiento, string.Empty, HorizontalAlign.Center));

        //        row.Cells.Add(AddCell(solicitud.IdSolicitud.ToString(), solicitud.IdSolicitud.ToString(), HorizontalAlign.Justify));
        //        row.Cells.Add(AddCell(solicitud.NumeroCartaDePorte, solicitud.NumeroCartaDePorte, HorizontalAlign.Justify));
        //        if (!App.Usuario.Empresa.Descripcion.ToUpper().Contains("CRESCA"))
        //            row.Cells.Add(AddCell(solicitud.Ctg, solicitud.Ctg, HorizontalAlign.Justify));
        //        row.Cells.Add(AddCell((solicitud.TipoDeCarta != null) ? solicitud.TipoDeCarta.Descripcion : string.Empty, (solicitud.TipoDeCarta != null) ? solicitud.TipoDeCarta.Descripcion : string.Empty, HorizontalAlign.Justify));
        //        row.Cells.Add(AddCell((solicitud.ProveedorTitularCartaDePorte != null) ? solicitud.ProveedorTitularCartaDePorte.Nombre : string.Empty, (solicitud.ProveedorTitularCartaDePorte != null) ? solicitud.ProveedorTitularCartaDePorte.Nombre : string.Empty, HorizontalAlign.Justify));
        //        row.Cells.Add(AddCell((solicitud.IdEstablecimientoProcedencia != null) ? solicitud.IdEstablecimientoProcedencia.Descripcion : string.Empty, (solicitud.IdEstablecimientoProcedencia != null) ? solicitud.IdEstablecimientoProcedencia.Descripcion : string.Empty, HorizontalAlign.Justify));

        //        string iconoVerde = "<center><a><IMG border='0' src='Content/Images/circle_green.png' width='12' height='12'></a></center>";
        //        string iconoRojo = "<center><a><IMG border='0' src='Content/Images/circle_red.png' width='12' height='12'></a></center>";
        //        string iconoAmarillo = "<center><a><IMG border='0' src='Content/Images/circle_yellow.png' width='12' height='12'></a></center>";
        //        string iconoCruz = "<center><a><IMG border='0' src='Content/Images/icon_Delete.png' width='12' height='12'></a></center>";
        //        string iconoNaranja = "<center><a><IMG border='0' src='Content/Images/Circle_Orange.png' width='12' height='12'></a></center>";
        //        string iconoCargaManual = "<center><a><IMG border='0' src='Content/Images/circle_greenManual.png' width='12' height='12'></a></center>";
        //        string iconoConfirmado = "<center><a><IMG border='0' src='Content/Images/iconArribo.png' width='12' height='12'></a></center>";
        //        string iconoConfirmadoDefinitivo = "<center><a><IMG border='0' src='Content/Images/iconArriboDefinitivo.png' width='12' height='12'></a></center>";
        //        string iconoRechazado = "<center><a><IMG border='0' src='Content/Images/IconRechazado.png' width='12' height='12'></a></center>";
        //        string iconoCambioDestino = "<center><a><IMG border='0' src='Content/Images/cambiodestino.png' width='12' height='12'></a></center>";
        //        string iconoVueltaOrigen = "<center><a><IMG border='0' src='Content/Images/vueltaorigen.png' width='12' height='12'></a></center>";
        //        string iconoEspera = "<center><a><IMG border='0' src='Content/Images/iconespera.png' width='12' height='12'></a></center>";
        //        if (!App.Usuario.Empresa.Descripcion.ToUpper().Contains("CRESCA"))
        //            if (solicitud.TipoDeCarta != null && solicitud.TipoDeCarta.Descripcion.Equals("Compra de granos"))
        //            {
        //                row.Cells.Add(AddCell(string.Empty, splitCapitalizacion(solicitud.EstadoEnAFIP.ToString()), HorizontalAlign.Justify));
        //            }
        //            else
        //            {
        //                if (solicitud.TipoDeCarta != null)
        //                {
        //                    switch (solicitud.EstadoEnAFIP)
        //                    {
        //                        case Enums.EstadoEnAFIP.Enviado:
        //                            row.Cells.Add(AddCell(iconoAmarillo, splitCapitalizacion(solicitud.EstadoEnAFIP.ToString()), HorizontalAlign.Justify));
        //                            break;
        //                        case Enums.EstadoEnAFIP.Otorgado:
        //                            row.Cells.Add(AddCell(iconoVerde, splitCapitalizacion(solicitud.EstadoEnAFIP.ToString()), HorizontalAlign.Justify));
        //                            break;
        //                        case Enums.EstadoEnAFIP.SinProcesar:
        //                            row.Cells.Add(AddCell(iconoRojo, splitCapitalizacion(solicitud.EstadoEnAFIP.ToString()), HorizontalAlign.Justify));
        //                            break;
        //                        case Enums.EstadoEnAFIP.Anulada:
        //                            row.Cells.Add(AddCell(iconoCruz, splitCapitalizacion(solicitud.EstadoEnAFIP.ToString()) + "\n\rCodigo Anulación AFIP: " + solicitud.CodigoAnulacionAfip.ToString(), HorizontalAlign.Justify));
        //                            break;
        //                        case Enums.EstadoEnAFIP.CargaManual:
        //                            row.Cells.Add(AddCell(iconoCargaManual, splitCapitalizacion(solicitud.EstadoEnAFIP.ToString()), HorizontalAlign.Justify));
        //                            break;
        //                        case Enums.EstadoEnAFIP.Confirmado:
        //                            row.Cells.Add(AddCell(iconoConfirmado, splitCapitalizacion(solicitud.EstadoEnAFIP.ToString()), HorizontalAlign.Justify));
        //                            break;
        //                        case Enums.EstadoEnAFIP.ConfirmadoDefinitivo:
        //                            row.Cells.Add(AddCell(iconoConfirmadoDefinitivo, splitCapitalizacion(solicitud.EstadoEnAFIP.ToString()), HorizontalAlign.Justify));
        //                            break;
        //                        case Enums.EstadoEnAFIP.Rechazado:
        //                            row.Cells.Add(AddCell(iconoRechazado, splitCapitalizacion(solicitud.EstadoEnAFIP.ToString()), HorizontalAlign.Justify));
        //                            break;
        //                        case Enums.EstadoEnAFIP.CambioDestino:
        //                            row.Cells.Add(AddCell(iconoCambioDestino, splitCapitalizacion(solicitud.EstadoEnAFIP.ToString()), HorizontalAlign.Justify));
        //                            break;
        //                        case Enums.EstadoEnAFIP.VueltaOrigen:
        //                            row.Cells.Add(AddCell(iconoVueltaOrigen, splitCapitalizacion(solicitud.EstadoEnAFIP.ToString()), HorizontalAlign.Justify));
        //                            break;
        //                        default:
        //                            row.Cells.Add(AddCell(string.Empty, "estado no definido", HorizontalAlign.Justify));
        //                            break;
        //                    }
        //                }
        //                else
        //                {
        //                    row.CssClass = "TableRowReserva";
        //                    row.Cells.Add(AddCell(string.Empty, string.Empty, HorizontalAlign.Justify));
        //                }
        //            }

        //        if (solicitud.TipoDeCarta != null)
        //        {
        //            switch (solicitud.EstadoEnSAP)
        //            {
        //                case Enums.EstadoEnvioSAP.EnProceso:
        //                    row.Cells.Add(AddCell(iconoNaranja, splitCapitalizacion(solicitud.EstadoEnSAP.ToString()), HorizontalAlign.Justify));
        //                    break;
        //                case Enums.EstadoEnvioSAP.FinalizadoConError:
        //                    row.Cells.Add(AddCell(iconoRojo, splitCapitalizacion(solicitud.EstadoEnSAP.ToString()), HorizontalAlign.Justify));
        //                    break;
        //                case Enums.EstadoEnvioSAP.FinalizadoOk:
        //                    row.Cells.Add(AddCell(iconoVerde, splitCapitalizacion(solicitud.EstadoEnSAP.ToString()) + "\n\rCodigo Envio SAP:" + solicitud.CodigoRespuestaEnvioSAP.ToString(), HorizontalAlign.Justify));
        //                    break;
        //                case Enums.EstadoEnvioSAP.Pendiente:
        //                    if (solicitud.EstadoEnAFIP == Enums.EstadoEnAFIP.SinProcesar)
        //                        row.Cells.Add(AddCell(iconoRojo, splitCapitalizacion(solicitud.EstadoEnSAP.ToString()), HorizontalAlign.Justify));
        //                    else
        //                        row.Cells.Add(AddCell(iconoAmarillo, splitCapitalizacion(solicitud.EstadoEnSAP.ToString()), HorizontalAlign.Justify));
        //                    break;
        //                case Enums.EstadoEnvioSAP.PedidoAnulacion:
        //                    row.Cells.Add(AddCell(iconoNaranja, splitCapitalizacion(solicitud.EstadoEnSAP.ToString()), HorizontalAlign.Justify));
        //                    break;
        //                case Enums.EstadoEnvioSAP.EnProcesoAnulacion:
        //                    row.Cells.Add(AddCell(iconoNaranja, splitCapitalizacion(solicitud.EstadoEnSAP.ToString()), HorizontalAlign.Justify));
        //                    break;
        //                case Enums.EstadoEnvioSAP.Anulada:
        //                    row.Cells.Add(AddCell(iconoCruz, splitCapitalizacion(solicitud.EstadoEnSAP.ToString()) + "\n\rCodigo Anulación SAP:" + solicitud.CodigoRespuestaAnulacionSAP.ToString(), HorizontalAlign.Justify));
        //                    break;
        //                case Enums.EstadoEnvioSAP.NoEnviadaASap:
        //                    row.Cells.Add(AddCell(string.Empty, splitCapitalizacion(solicitud.EstadoEnSAP.ToString()), HorizontalAlign.Justify));
        //                    break;
        //                case Enums.EstadoEnvioSAP.PrimerEnvioTerceros:
        //                    row.Cells.Add(AddCell(iconoNaranja, splitCapitalizacion(solicitud.EstadoEnSAP.ToString()), HorizontalAlign.Justify));
        //                    break;
        //                case Enums.EstadoEnvioSAP.EnEsperaPorProspecto:
        //                    row.Cells.Add(AddCell(iconoEspera, splitCapitalizacion(solicitud.EstadoEnSAP.ToString()), HorizontalAlign.Justify));
        //                    break;
        //                default:
        //                    break;
        //            }
        //        }
        //        else
        //        {
        //            row.Cells.Add(AddCell(string.Empty, string.Empty, HorizontalAlign.Justify));
        //        }
        //        if (!App.Usuario.Empresa.Descripcion.ToUpper().Contains("CRESCA"))
        //            row.Cells.Add(AddCell(solicitud.ObservacionAfip, solicitud.ObservacionAfip, HorizontalAlign.Justify));
        //        row.Cells.Add(AddCell(solicitud.FechaCreacion.ToString("dd/MM/yyyy"), solicitud.FechaCreacion.ToString(), HorizontalAlign.Justify));
        //        row.Cells.Add(AddCell(solicitud.UsuarioCreacion.Split('\\')[1].ToString(), solicitud.UsuarioCreacion, HorizontalAlign.Justify));
        //        // REENVIAR AFIP
        //        string link = "<a href='Index.aspx?Id=" + solicitud.IdSolicitud.ToString() + "&reenvioAFIP=1" +
        //                "'><IMG border='0' src='Content/Images/icon_select.gif'></a>";
        //        if (!_reenviosAFIP)
        //            link = string.Empty;

        //        if (solicitud.EstadoEnAFIP == Enums.EstadoEnAFIP.Otorgado)
        //            link = string.Empty;

        //        if (solicitud.EstadoEnAFIP == Enums.EstadoEnAFIP.Anulada)
        //            link = string.Empty;

        //        if (solicitud.EstadoEnAFIP == Enums.EstadoEnAFIP.CargaManual)
        //            link = string.Empty;

        //        if (solicitud.EstadoEnAFIP == Enums.EstadoEnAFIP.Confirmado)
        //            link = string.Empty;

        //        if (solicitud.EstadoEnAFIP == Enums.EstadoEnAFIP.ConfirmadoDefinitivo)
        //            link = string.Empty;

        //        if (solicitud.EstadoEnAFIP == Enums.EstadoEnAFIP.Rechazado)
        //            link = string.Empty;

        //        if (solicitud.EstadoEnAFIP == Enums.EstadoEnAFIP.CambioDestino)
        //            link = string.Empty;

        //        if (solicitud.EstadoEnAFIP == Enums.EstadoEnAFIP.VueltaOrigen)
        //            link = string.Empty;


        //        if (solicitud.TipoDeCarta != null && solicitud.TipoDeCarta.Descripcion.Equals("Compra de granos"))
        //            link = iconoCruz;

        //        if (solicitud.TipoDeCarta != null && solicitud.TipoDeCarta.Descripcion.Equals("Venta de granos de terceros"))
        //            link = iconoCruz;

        //        if (solicitud.TipoDeCarta == null)
        //            link = string.Empty;

        //        if (!App.Usuario.Empresa.Descripcion.ToUpper().Contains("CRESCA"))
        //            row.Cells.Add(AddCell(link, string.Empty, HorizontalAlign.Center));

        //        // REENVIAR A SAP
        //        link = string.Empty;
        //        if (solicitud.EstadoEnSAP == Enums.EstadoEnvioSAP.FinalizadoConError)
        //            link = "<a href='javascript:ReenviarSAP(" + solicitud.IdSolicitud.ToString() + ")'><IMG border='0' src='Content/Images/icon_select.gif'></a>";

        //        if (solicitud.TipoDeCarta != null && solicitud.TipoDeCarta.Descripcion.Equals("Compra de granos que transportamos"))
        //            link = iconoCruz;
        //        if (solicitud.TipoDeCarta != null && solicitud.TipoDeCarta.Descripcion.Equals("Compra de granos"))
        //            link = iconoCruz;
        //        if (solicitud.TipoDeCarta == null)
        //            link = string.Empty;
        //        if (solicitud.EstadoEnSAP == Enums.EstadoEnvioSAP.PrimerEnvioTerceros)
        //            link = string.Empty;

        //        if (!_ReenviosSAP)
        //            link = string.Empty;

        //        row.Cells.Add(AddCell(link, string.Empty, HorizontalAlign.Center));

        //        // Lapiz
        //        link = string.Empty;
        //        string tool = string.Empty;
        //        if (!String.IsNullOrEmpty(solicitud.MensajeRespuestaEnvioSAP))
        //            link = "<a href='LogSapList.aspx?cdp=" + solicitud.NumeroCartaDePorte +
        //            "'><IMG border='0' src='Content/Images/logsap.png' width='16' height='16'></a>";
        //        tool = "Ver detalle respuesta SAP";

        //        if (!App.UsuarioTienePermisos("Visualizacion Log SAP"))
        //            link = string.Empty;

        //        row.Cells.Add(AddCell(link, tool, HorizontalAlign.Center));

        //        // Reporte
        //        link = string.Empty;

        //        if (solicitud.EstadoEnAFIP != Enums.EstadoEnAFIP.Anulada)
        //            link = "<a target='_new' href='ReportePDF.aspx?Id=" + solicitud.IdSolicitud.ToString() +
        //            "'><IMG border='0' src='Content/Images/folder-print.png' width='16' height='16'></a>";

        //        if (!App.UsuarioTienePermisos("Imprimir Solicitud"))
        //            link = string.Empty;

        //        row.Cells.Add(AddCell(link, "Imprimir Cartas de Porte", HorizontalAlign.Center));

        //        // LUPA
        //        link = "<a href='Index.aspx?Id=" + solicitud.IdSolicitud.ToString() +
        //                "'><IMG border='0' src='Content/Images/magnify.gif'></a>";

        //        if (solicitud.ObservacionAfip.Contains("Reserva"))
        //            link = "<a href='Index.aspx?Id=" + solicitud.IdSolicitud.ToString() +
        //                    "'><IMG border='0' src='Content/Images/cargaCartaDePorteReservada.png'></a>";

        //        if (solicitud.EstadoEnAFIP == Enums.EstadoEnAFIP.Anulada && solicitud.EstadoEnSAP == Enums.EstadoEnvioSAP.Anulada)
        //            link = string.Empty;

        //        if (!App.UsuarioTienePermisos("Visualizacion Solicitud"))
        //            link = string.Empty;

        //        row.Cells.Add(AddCell(link, "Abrir Solicitud", HorizontalAlign.Center));
        //        tblData.Rows.Add(row);
        //    }
        //}
        #endregion

        private void DatosFiltro(string busqueda, string estadoAFIP, string estadoSAP)
        {
            Session["totalList"] = null;
            Session["tableList"] = null;
            Session["totalList"] = SolicitudDAO.Instance.GetFiltro(busqueda, estadoAFIP, estadoSAP);
            Session["tableList"] = addMoreRows();
            popTable();
        }

        private void popTable()
        {
            Boolean _ReenviosSAP = false;
            Boolean _BaseDeDatos = false;
            Boolean _SeguimientoEstados = false;
            Boolean _reenviosAFIP = false;

            if (App.Usuario == null || App.Usuario.UsuariosSeguridad.Count == 0)
            {
                return;
            }

            if (App.UsuarioTienePermisos("ReenviosSAP")) { _ReenviosSAP = true; }
            if (App.UsuarioTienePermisos("BaseDeDatos")) { _BaseDeDatos = true; }
            if (App.UsuarioTienePermisos("SeguimientoEstados")) { _SeguimientoEstados = true; }
            if (App.UsuarioTienePermisos("ReenviosAFIP")) { _reenviosAFIP = true; }

            IList<Solicitud> tableList = new List<Solicitud>();
            if ((Session["tableList"] != null))
                tableList = (IList<Solicitud>)Session["tableList"];

            IList<Solicitud> totalList = new List<Solicitud>();
            if ((Session["totalList"] != null))
                totalList = (IList<Solicitud>)Session["totalList"];

            if (totalList.Count > 1)
                lblCantidadResultados.Text = "Resultado de la busqueda: " + totalList.Count.ToString() + " registros";
            else if (totalList.Count == 1)
                lblCantidadResultados.Text = "Resultado de la busqueda: " + totalList.Count.ToString() + " registro";
            else
                lblCantidadResultados.Text = "Resultado de la busqueda: 0 registro";

            foreach (Solicitud solicitud in tableList)
            {
                var row = new TableRow();
                row.CssClass = "TableRow";

                string linkMantenimiento = "<a href='contingenciasestados.aspx?Id=" + solicitud.IdSolicitud.ToString() +
                    "'><IMG border='0' src='Content/Images/Mantenimiento.png' width='16' height='16'></a>";

                if (!(_BaseDeDatos || _SeguimientoEstados))
                {
                    linkMantenimiento = string.Empty;
                }

                row.Cells.Add(AddCell(linkMantenimiento, string.Empty, HorizontalAlign.Center));

                row.Cells.Add(AddCell(solicitud.IdSolicitud.ToString(), solicitud.IdSolicitud.ToString(), HorizontalAlign.Justify));
                row.Cells.Add(AddCell(solicitud.NumeroCartaDePorte, solicitud.NumeroCartaDePorte, HorizontalAlign.Justify));
                if (Session["Pais"].ToString() == "ARGENTINA")
                    row.Cells.Add(AddCell(solicitud.Ctg, solicitud.Ctg, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(solicitud.TipoDeCartaString, solicitud.TipoDeCartaString, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(solicitud.ProveedorTitularCartaDePorteString, solicitud.ProveedorTitularCartaDePorteString, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(solicitud.EstablecimientoProcedenciaString, solicitud.EstablecimientoProcedenciaString, HorizontalAlign.Justify));

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
                string IconoTerceros = "<center><a><IMG border='0' src='Content/Images/DatosPropios.png' width='30' height='30'></a></center>";
                string InconoProspecto = "<center><a><IMG border='0' src='Content/Images/BusquedaEmpleados.png' width='30' height='30'></a></center>";
                if (Session["Pais"].ToString() == "ARGENTINA")
                    if (!String.IsNullOrEmpty(solicitud.TipoDeCartaString) && solicitud.TipoDeCartaString.Equals("Compra de granos"))
                    {
                        row.Cells.Add(AddCell(string.Empty, splitCapitalizacion(solicitud.EstadoEnAFIP.ToString()), HorizontalAlign.Justify));
                    }
                    else
                    {

                        if (!String.IsNullOrEmpty(solicitud.TipoDeCartaString))
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

                if (!String.IsNullOrEmpty(solicitud.TipoDeCartaString))
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
                                row.Cells.Add(AddCell(iconoRojo, splitCapitalizacion(solicitud.EstadoEnSAP.ToString()), HorizontalAlign.Justify));
                            else
                                row.Cells.Add(AddCell(iconoAmarillo, splitCapitalizacion(solicitud.EstadoEnSAP.ToString()), HorizontalAlign.Justify));
                            break;
                        case Enums.EstadoEnvioSAP.PedidoAnulacion:
                            row.Cells.Add(AddCell(iconoEspera, splitCapitalizacion(solicitud.EstadoEnSAP.ToString()), HorizontalAlign.Justify));
                            break;
                        case Enums.EstadoEnvioSAP.EnProcesoAnulacion:
                            row.Cells.Add(AddCell(iconoVueltaOrigen, splitCapitalizacion(solicitud.EstadoEnSAP.ToString()), HorizontalAlign.Justify));
                            break;
                        case Enums.EstadoEnvioSAP.Anulada:
                            row.Cells.Add(AddCell(iconoCruz, splitCapitalizacion(solicitud.EstadoEnSAP.ToString()) + "\n\rCodigo Anulación SAP:" + solicitud.CodigoRespuestaAnulacionSAP.ToString(), HorizontalAlign.Justify));
                            break;
                        case Enums.EstadoEnvioSAP.NoEnviadaASap:
                            row.Cells.Add(AddCell(string.Empty, splitCapitalizacion(solicitud.EstadoEnSAP.ToString()), HorizontalAlign.Justify));
                            break;
                        case Enums.EstadoEnvioSAP.PrimerEnvioTerceros:
                            row.Cells.Add(AddCell(IconoTerceros, splitCapitalizacion(solicitud.EstadoEnSAP.ToString()), HorizontalAlign.Justify));
                            break;
                        case Enums.EstadoEnvioSAP.EnEsperaPorProspecto:
                            row.Cells.Add(AddCell(InconoProspecto, splitCapitalizacion(solicitud.EstadoEnSAP.ToString()), HorizontalAlign.Justify));
                            break;
                        default:
                            row.Cells.Add(AddCell(string.Empty, string.Empty, HorizontalAlign.Justify));
                            break;
                    }
                }
                else
                {
                    row.Cells.Add(AddCell(string.Empty, string.Empty, HorizontalAlign.Justify));
                }
                if (Session["Pais"].ToString() == "ARGENTINA")
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

                if (String.IsNullOrEmpty(solicitud.TipoDeCartaString))
                {
                    link = string.Empty;
                }
                if (Session["Pais"].ToString() == "ARGENTINA")
                    row.Cells.Add(AddCell(link, string.Empty, HorizontalAlign.Center));

                // REENVIAR A SAP
                link = string.Empty;
                if (solicitud.EstadoEnSAP == Enums.EstadoEnvioSAP.FinalizadoConError)
                {
                    link = "<a href='javascript:ReenviarSAP(" + solicitud.IdSolicitud.ToString() + ")'><IMG border='0' src='Content/Images/icon_select.gif'></a>";
                }
                if (!String.IsNullOrEmpty(solicitud.TipoDeCartaString) && solicitud.TipoDeCartaString.Equals("Compra de granos que transportamos"))
                {
                    link = iconoCruz;
                }
                if (!String.IsNullOrEmpty(solicitud.TipoDeCartaString) && solicitud.TipoDeCartaString.Equals("Compra de granos"))
                {
                    link = iconoCruz;
                }
                if (String.IsNullOrEmpty(solicitud.TipoDeCartaString))
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
                
                if (!PaisDAO.Instance.GetOne(App.Usuario.IdPais).Descripcion.ToUpper().Contains("PARAGUAY"))
                {
                    if (solicitud.EstadoEnAFIP != Enums.EstadoEnAFIP.Anulada)
                    {
                        link = "<a target='_new' href='ReportePDF.aspx?Id=" + solicitud.IdSolicitud.ToString() +
                        "'><IMG border='0' src='Content/Images/folder-print.png' width='16' height='16'></a>";
                    }
                }
                else
                {
                    link = "<a target='_new' href='ReportePDF.aspx?Id=" + solicitud.IdSolicitud.ToString() +
                        "'><IMG border='0' src='Content/Images/folder-print.png' width='16' height='16'></a>";

                    //Button btn = new Button();
                    //btn.Text = "Remito";
                    //btn.ID = solicitud.IdSolicitud.ToString();
                    //ScriptManager.GetCurrent(Page).RegisterPostBackControl(btn);
                    //btn.Click += new EventHandler(btnRemitoParaguay_Click);

                    ////AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
                    ////trigger.ControlID = btn;
                    ////trigger.EventName = "Click";
                    ////upTblData.Triggers.Add(trigger);

                    //var cell = new TableCell();
                    //cell.Controls.Add(btn);
                    //row.Cells.Add(cell);

                    //link = "<a href='javascript:WindowOpen(" + "ReportePDF.aspx?Id=" + solicitud.IdSolicitud.ToString() + ")'><IMG border='0' src='Content/Images/icon_select.gif'></a>";

                    //row.Cells.Add(AddCell(link, "Imprimir Cartas de Porte", HorizontalAlign.Center));

                    //LinkButton lnk = new LinkButton();
                    //lnk.ID = solicitud.IdSolicitud.ToString();
                    //lnk.Text = "Remito";
                    //lnk.CommandName = solicitud.IdSolicitud.ToString();
                    //lnk.Font.Bold = true;
                    //lnk.Font.Size = 13;
                    //lnk.Width = 10;
                    //lnk.Click += new EventHandler(btnRemitoParaguay_Click);

                    //var cell = new TableCell();
                    //cell.Controls.Add(lnk);
                    //cell.Controls.Add(new LiteralControl("<br/>"));
                    //row.Cells.Add(cell);
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

                if (!App.UsuarioTienePermisos("Visualizacion Solicitud"))
                    link = string.Empty;

                if (solicitud.EstadoEnAFIP == Enums.EstadoEnAFIP.Anulada && solicitud.EstadoEnSAP == Enums.EstadoEnvioSAP.Anulada)
                {
                    link = string.Empty;
                }

                row.Cells.Add(AddCell(link, "Abrir Solicitud", HorizontalAlign.Center));
                tblData.Rows.Add(row);
            }
        }

        void btnRemitoParaguay_Click(object sender, EventArgs e)
        {
            if (GrupoEmpresaDAO.Instance.GetOne(6).Pais.Descripcion.ToUpper() == "PARAGUAY")
            {
                var File = new GenerarPDF().SetReport(6);

                var fileInfo = new FileInfo(File);

                if (fileInfo.Exists)
                {
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + fileInfo.Name);
                    Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                    Response.ContentType = "application/octet-stream";
                    Response.Flush();
                    Response.TransmitFile(fileInfo.FullName);
                    Response.End();
                }
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            lblCantidadActual.Text = string.Empty;
            CargarTitulos();
            var AFIP = cboEstadoAfip.SelectedValue == string.Empty ? "-1" : cboEstadoAfip.SelectedValue;
            var SAP = cboEstadoSAP.SelectedValue == string.Empty ? "-1" : cboEstadoSAP.SelectedValue;

            DatosFiltro(txtBuscar.Text.Trim(), AFIP, SAP);
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

        protected void linkConfirmacionDeArribo_Click(object sender, EventArgs e)
        {
            Response.Redirect("BandejaDeSalidaConfirmacion.aspx");
        }

        protected void linkTrasladosRechazados_Click(object sender, EventArgs e)
        {
            Response.Redirect("TrasladosRechazados.aspx");
        }


        protected void btnCerrarCliente_Click(object sender, EventArgs e)
        {

            Solicitud solicitudGuardara = SolicitudDAO.Instance.GetOne(Convert.ToInt32(Request["idSolicitudReenviar"]));

            if (solicitudGuardara.TipoDeCarta.Descripcion.Equals("Terceros por venta  de Granos de producción propia"))
            {
                solicitudGuardara.EstadoEnSAP = Enums.EstadoEnvioSAP.PrimerEnvioTerceros;
            }

            wsSAP ws = new wsSAP();
            ws.PrefacturaSAP(solicitudGuardara, false, false);

            Response.Redirect("BandejaDeSalida.aspx");

        }

        private IList<Solicitud> addMoreRows()
        {

            IList<Solicitud> tableList = new List<Solicitud>();
            if ((Session["tableList"] != null))
                tableList = (IList<Solicitud>)Session["tableList"];

            IList<Solicitud> totalList = new List<Solicitud>();
            if ((Session["totalList"] != null))
                totalList = (IList<Solicitud>)Session["totalList"];

            int rowpagina = 10;
            int cntTableList = tableList.Count;

            int cnt = 0;
            if (Session["totalList"] != null)
            {
                foreach (Solicitud sol in totalList)
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

            IList<Solicitud> tmpList = new List<Solicitud>();
            if ((Session["tableList"] != null))
                tmpList = (IList<Solicitud>)Session["tableList"];

            IList<Solicitud> tmpList2 = new List<Solicitud>();
            if ((Session["totalList"] != null))
                tmpList2 = (IList<Solicitud>)Session["totalList"];

            lblCantidadActual.Text = "Registros cargados: " + tmpList.Count.ToString() + " de " + tmpList2.Count.ToString();
            lblCantidadActual.ForeColor = Color.DarkGreen;

        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CartaDePorte.Core.Domain;
using CartaDePorte.Core.DAO;
using CartaDePorte.Core.Servicios;
using CartaDePorte.Core.Domain.Seguridad;
using System.Configuration;
using CartaDePorte.Core;

namespace CartaDePorte.Web
{
    public partial class TrasladosRechazados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!App.UsuarioTienePermisos("Bandeja de Salida"))
            {
                Response.Redirect("~/SinAutorizacion.aspx");
                return;
            }

            if (!IsPostBack)
            {
            }

            CargarTitulos();

            //string testRechazada = ConfigurationManager.AppSettings["testRechazada"];
            //if (testRechazada != null && testRechazada.ToLower().Equals("true"))
            //{
            //DatosLore();
            //}
            //else {
            Datos();
            //}
        }

        private void CargarTitulos()
        {
            tblData.Rows.Clear();
            var row = new TableRow();
            row.CssClass = "TableRowTitle";
            row.Cells.Add(AddTitleCell("Id", 20));
            row.Cells.Add(AddTitleCell("Nro Carta Porte", 80));
            row.Cells.Add(AddTitleCell("Nro de CTG", 80));
            row.Cells.Add(AddTitleCell("Fecha", 170));
            row.Cells.Add(AddTitleCell("Establecimiento Procedencia", 200));
            row.Cells.Add(AddTitleCell("Establecimiento Destino", 200));
            row.Cells.Add(AddTitleCell("Cliente Destinatario", 200));
            row.Cells.Add(AddTitleCell("Peso", 50));
            row.Cells.Add(AddTitleCell("Estado AFIP", 30));
            //row.Cells.Add(AddTitleCell("Usuario", 100));
            row.Cells.Add(AddTitleCell("CDD", 10));
            row.Cells.Add(AddTitleCell("RaO", 10));
            row.Cells.Add(AddTitleCell("VER", 10));
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
            cell.HorizontalAlign = ha;
            cell.VerticalAlign = VerticalAlign.Middle;
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

        private void DatosLore()
        {
            IList<Solicitud> result = new List<Solicitud>();
            IList<Solicitud> resulttmp = new List<Solicitud>();
            resulttmp = SolicitudDAO.Instance.GetRechazadas();

            foreach (Solicitud solLore in resulttmp)
            {
                if (solLore.EstadoEnAFIP == Enums.EstadoEnAFIP.Rechazado)
                    result.Add(solLore);

            }

            lblCantidadResultados.Text = "Resultado de la busqueda: " + result.Count.ToString() + " registros";

            foreach (Solicitud solicitud in result)
            {
                var row = new TableRow();
                row.CssClass = "TableRow";

                row.Cells.Add(AddCell(solicitud.IdSolicitud.ToString(), solicitud.IdSolicitud.ToString(), HorizontalAlign.Justify));
                row.Cells.Add(AddCell(solicitud.NumeroCartaDePorte, solicitud.NumeroCartaDePorte, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(solicitud.Ctg, solicitud.Ctg, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(solicitud.FechaCreacion.ToString("dd/MM/yyyy hh:mm"), solicitud.FechaCreacion.ToString(), HorizontalAlign.Justify));
                row.Cells.Add(AddCell((solicitud.IdEstablecimientoProcedencia != null) ? solicitud.IdEstablecimientoProcedencia.Descripcion : string.Empty, (solicitud.IdEstablecimientoProcedencia != null) ? solicitud.IdEstablecimientoProcedencia.Descripcion : string.Empty, HorizontalAlign.Justify));
                row.Cells.Add(AddCell((solicitud.IdEstablecimientoDestino != null) ? solicitud.IdEstablecimientoDestino.Descripcion : string.Empty, (solicitud.IdEstablecimientoProcedencia != null) ? solicitud.IdEstablecimientoProcedencia.Descripcion : string.Empty, HorizontalAlign.Justify));
                row.Cells.Add(AddCell((solicitud.ClienteDestinatario != null) ? solicitud.ClienteDestinatario.RazonSocial : string.Empty, (solicitud.ClienteDestinatario != null) ? solicitud.ClienteDestinatario.RazonSocial : string.Empty, HorizontalAlign.Justify));

                Int64 peso = 0;
                if (solicitud.CargaPesadaDestino)
                    peso = solicitud.KilogramosEstimados;
                else
                    peso = (solicitud.PesoNeto.HasValue) ? solicitud.PesoNeto.Value : 0;

                row.Cells.Add(AddCell(peso.ToString(), peso.ToString(), HorizontalAlign.Justify));

                string iconoVerde = "<center><a><IMG border='0' src='../../Content/Images/circle_green.png' width='12' height='12'></a></center>";
                string iconoRojo = "<center><a><IMG border='0' src='../../Content/Images/circle_red.png' width='12' height='12'></a></center>";
                string iconoAmarillo = "<center><a><IMG border='0' src='../../Content/Images/circle_yellow.png' width='12' height='12'></a></center>";
                string iconoCruz = "<center><a><IMG border='0' src='../../Content/Images/icon_Delete.png' width='12' height='12'></a></center>";
                string iconoCargaManual = "<center><a><IMG border='0' src='../../Content/Images/circle_greenManual.png' width='12' height='12'></a></center>";
                string iconoConfirmado = "<center><a><IMG border='0' src='../../Content/Images/iconArribo.png' width='12' height='12'></a></center>";
                string iconoConfirmadoDefinitivo = "<center><a><IMG border='0' src='../../Content/Images/iconArriboDefinitivo.png' width='12' height='12'></a></center>";
                string iconoRechazado = "<center><a><IMG border='0' src='../../Content/Images/IconRechazado.png' width='12' height='12'></a></center>";
                string iconoCambioDestino = "<center><a><IMG border='0' src='../../Content/Images/cambiodestino.png' width='12' height='12'></a></center>";
                string iconoVueltaOrigen = "<center><a><IMG border='0' src='../../Content/Images/vueltaorigen.png' width='12' height='12'></a></center>";

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
                        row.Cells.Add(AddCell(iconoCruz, splitCapitalizacion(solicitud.EstadoEnAFIP.ToString()), HorizontalAlign.Justify));
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




                //row.Cells.Add(AddCell(solicitud.UsuarioCreacion.Split('\\')[1].ToString(), solicitud.UsuarioCreacion, HorizontalAlign.Justify));


                String link = string.Empty;

                // Cambio Destino y Destinatario
                link = "<center><a href='Index.aspx?Id=" + solicitud.IdSolicitud.ToString() + "&CambioDestino=1" +
                        "'><IMG border='0' src='../../Content/Images/pencil2.png' width='16' height='16'></a></center>";
                row.Cells.Add(AddCell(link, "Cambio Destino y Destinatario", HorizontalAlign.Center));

                // Regresar a Origen
                link = "<center><a href='RegresoOrigen.aspx?Id=" + solicitud.IdSolicitud.ToString() +
                        "'><IMG border='0' src='../../Content/Images/pencil2.png' width='16' height='16'></a></center>";
                row.Cells.Add(AddCell(link, "Regresar a Origen", HorizontalAlign.Center));

                // LUPA
                link = "<center><a href='Index.aspx?Id=" + solicitud.IdSolicitud.ToString() +
                        "'><IMG border='0' src='../../Content/Images/magnify.gif'></a></center>";

                row.Cells.Add(AddCell(link, "Abrir Solicitud", HorizontalAlign.Center));


                tblData.Rows.Add(row);

            }

        }

        private void Datos()
        {
            IList<Solicitud> result = new List<Solicitud>();
            // Busco datos online de todos los rechazados
            if (Environment.MachineName.ToUpper() == "WI7-SIS22N-ADM" || Environment.MachineName.ToUpper() == "SRV-MS10-ADM")
            {
                var SolicitudesCambiarDestino = CartaDePorte.Core.DAO.SolicitudDAO.Instance.GetFiltro(string.Empty, "7", "-1");
                foreach (var item in SolicitudesCambiarDestino)
                {
                    Solicitud solTmp = SolicitudDAO.Instance.GetSolicitudByCTG(item.Ctg.Replace(".", ""));
                    if (solTmp != null)
                    {
                        // Verifico que sea de Empresa (Por el momento Cresud)
                        Empresa empresaTitular = EmpresaDAO.Instance.GetOneBySap_Id(solTmp.ProveedorTitularCartaDePorte.Sap_Id);
                        if (empresaTitular != null)
                            result.Add(solTmp);
                    }
                }
            }
            else
            {
                var wsa = new wsAfip_v3();
                var resulRechazo = wsa.consultarCTG(DateTime.Now.AddDays(-7));

                if (resulRechazo.arrayErrores.Count() > 0)
                {
                    lblCantidadResultados.Text = "No esta disponible la consulta con AFIP, intente nuevamente mas tarde.";
                }
                else
                {
                    if (resulRechazo.arrayDatosConsultarCTG.Count() > 0)
                    {
                        foreach (var dato in resulRechazo.arrayDatosConsultarCTG)
                        {
                            if (dato.estado.Equals("Rechazado"))
                            {
                                Solicitud solTmp = SolicitudDAO.Instance.GetSolicitudByCTG(dato.ctg.Replace(".", ""));
                                if (solTmp != null && solTmp.EstadoEnAFIP != Enums.EstadoEnAFIP.Rechazado)
                                {
                                    solTmp.EstadoEnAFIP = Enums.EstadoEnAFIP.Rechazado;
                                    SolicitudDAO.Instance.SaveOrUpdate(solTmp);
                                }
                                if (solTmp != null)
                                {
                                    // Verifico que sea de Empresa (Por el momento Cresud)
                                    Empresa empresaTitular = EmpresaDAO.Instance.GetOneBySap_Id(solTmp.ProveedorTitularCartaDePorte.Sap_Id);
                                    if (empresaTitular != null)
                                        result.Add(solTmp);
                                }
                            }
                        }
                    }
                }
            }

            lblCantidadResultados.Text = "Resultado de la busqueda: " + result.Count.ToString() + " registros";

            foreach (Solicitud solicitud in result)
            {
                var row = new TableRow();
                row.CssClass = "TableRow";

                row.Cells.Add(AddCell(solicitud.IdSolicitud.ToString(), solicitud.IdSolicitud.ToString(), HorizontalAlign.Justify));
                row.Cells.Add(AddCell(solicitud.NumeroCartaDePorte, solicitud.NumeroCartaDePorte, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(solicitud.Ctg, solicitud.Ctg, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(solicitud.FechaCreacion.ToString("dd/MM/yyyy hh:mm"), solicitud.FechaCreacion.ToString(), HorizontalAlign.Justify));
                row.Cells.Add(AddCell((solicitud.IdEstablecimientoProcedencia != null) ? solicitud.IdEstablecimientoProcedencia.Descripcion : string.Empty, (solicitud.IdEstablecimientoProcedencia != null) ? solicitud.IdEstablecimientoProcedencia.Descripcion : string.Empty, HorizontalAlign.Justify));
                row.Cells.Add(AddCell((solicitud.IdEstablecimientoDestino != null) ? solicitud.IdEstablecimientoDestino.Descripcion : string.Empty, (solicitud.IdEstablecimientoProcedencia != null) ? solicitud.IdEstablecimientoProcedencia.Descripcion : string.Empty, HorizontalAlign.Justify));
                row.Cells.Add(AddCell((solicitud.ClienteDestinatario != null) ? solicitud.ClienteDestinatario.RazonSocial : string.Empty, (solicitud.ClienteDestinatario != null) ? solicitud.ClienteDestinatario.RazonSocial : string.Empty, HorizontalAlign.Justify));

                Int64 peso = 0;
                if (solicitud.CargaPesadaDestino)
                    peso = solicitud.KilogramosEstimados;
                else
                    peso = solicitud.PesoNeto.Value;

                row.Cells.Add(AddCell(peso.ToString(), peso.ToString(), HorizontalAlign.Justify));

                string iconoVerde = "<center><a><IMG border='0' src='../../Content/Images/circle_green.png' width='12' height='12'></a></center>";
                string iconoRojo = "<center><a><IMG border='0' src='../../Content/Images/circle_red.png' width='12' height='12'></a></center>";
                string iconoAmarillo = "<center><a><IMG border='0' src='../../Content/Images/circle_yellow.png' width='12' height='12'></a></center>";
                string iconoCruz = "<center><a><IMG border='0' src='../../Content/Images/icon_Delete.png' width='12' height='12'></a></center>";
                string iconoCargaManual = "<center><a><IMG border='0' src='../../Content/Images/circle_greenManual.png' width='12' height='12'></a></center>";
                string iconoConfirmado = "<center><a><IMG border='0' src='../../Content/Images/iconArribo.png' width='12' height='12'></a></center>";
                string iconoConfirmadoDefinitivo = "<center><a><IMG border='0' src='../../Content/Images/iconArriboDefinitivo.png' width='12' height='12'></a></center>";
                string iconoRechazado = "<center><a><IMG border='0' src='../../Content/Images/IconRechazado.png' width='12' height='12'></a></center>";
                string iconoCambioDestino = "<center><a><IMG border='0' src='../../Content/Images/cambiodestino.png' width='12' height='12'></a></center>";
                string iconoVueltaOrigen = "<center><a><IMG border='0' src='../../Content/Images/vueltaorigen.png' width='12' height='12'></a></center>";

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
                        row.Cells.Add(AddCell(iconoCruz, splitCapitalizacion(solicitud.EstadoEnAFIP.ToString()), HorizontalAlign.Justify));
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




                //row.Cells.Add(AddCell(solicitud.UsuarioCreacion.Split('\\')[1].ToString(), solicitud.UsuarioCreacion, HorizontalAlign.Justify));


                String link = string.Empty;

                // Cambio Destino y Destinatario
                link = "<center><a href='Index.aspx?Id=" + solicitud.IdSolicitud.ToString() + "&CambioDestino=1" +
                        "'><IMG border='0' src='../../Content/Images/pencil2.png' width='16' height='16'></a></center>";
                row.Cells.Add(AddCell(link, "Cambio Destino y Destinatario", HorizontalAlign.Center));

                // Regresar a Origen
                link = "<center><a href='RegresoOrigen.aspx?Id=" + solicitud.IdSolicitud.ToString() +
                        "'><IMG border='0' src='../../Content/Images/pencil2.png' width='16' height='16'></a></center>";
                row.Cells.Add(AddCell(link, "Regresar a Origen", HorizontalAlign.Center));

                // LUPA
                link = "<center><a href='Index.aspx?Id=" + solicitud.IdSolicitud.ToString() +
                        "'><IMG border='0' src='../../Content/Images/magnify.gif'></a></center>";

                row.Cells.Add(AddCell(link, "Abrir Solicitud", HorizontalAlign.Center));


                tblData.Rows.Add(row);

            }
        }
        private void DatosFiltro(string busqueda)
        {
            IList<Solicitud> result = SolicitudDAO.Instance.GetFiltroConfirmacion(busqueda);

            if (result.Count > 1)
                lblCantidadResultados.Text = "Resultado de la busqueda: " + result.Count.ToString() + " registros";
            else if (result.Count == 1)
                lblCantidadResultados.Text = "Resultado de la busqueda: " + result.Count.ToString() + " registro";
            else
                lblCantidadResultados.Text = "Resultado de la busqueda: 0 registro";

            foreach (Solicitud solicitud in result)
            {
                var row = new TableRow();
                row.CssClass = "TableRow";

                row.Cells.Add(AddCell(solicitud.IdSolicitud.ToString(), solicitud.IdSolicitud.ToString(), HorizontalAlign.Justify));
                row.Cells.Add(AddCell(solicitud.NumeroCartaDePorte, solicitud.NumeroCartaDePorte, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(solicitud.FechaCreacion.ToString("dd/MM/yyyy hh:mm"), solicitud.FechaCreacion.ToString(), HorizontalAlign.Justify));
                row.Cells.Add(AddCell((solicitud.IdEstablecimientoProcedencia != null) ? solicitud.IdEstablecimientoProcedencia.Descripcion : string.Empty, (solicitud.IdEstablecimientoProcedencia != null) ? solicitud.IdEstablecimientoProcedencia.Descripcion : string.Empty, HorizontalAlign.Justify));
                row.Cells.Add(AddCell((solicitud.IdEstablecimientoDestino != null) ? solicitud.IdEstablecimientoDestino.Descripcion : string.Empty, (solicitud.IdEstablecimientoProcedencia != null) ? solicitud.IdEstablecimientoProcedencia.Descripcion : string.Empty, HorizontalAlign.Justify));

                Int64 peso = 0;
                if (solicitud.CargaPesadaDestino)
                    peso = solicitud.KilogramosEstimados;
                else
                    peso = solicitud.PesoNeto.Value;

                row.Cells.Add(AddCell(peso.ToString(), peso.ToString(), HorizontalAlign.Justify));
                row.Cells.Add(AddCell(solicitud.UsuarioCreacion.Split('\\')[1].ToString(), solicitud.UsuarioCreacion, HorizontalAlign.Justify));

                // LUPA
                String link = "<a href='Index.aspx?Id=" + solicitud.IdSolicitud.ToString() +
                        "'><IMG border='0' src='../../Content/Images/magnify.gif'></a>";

                row.Cells.Add(AddCell(link, "Abrir Solicitud", HorizontalAlign.Center));


                tblData.Rows.Add(row);

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

    }
}

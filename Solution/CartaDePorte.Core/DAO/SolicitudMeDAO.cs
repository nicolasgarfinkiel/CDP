using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CartaDePorte.Core.Domain;
using System.Data.SqlClient;
using CartaDePorte.Core.Exception;
using System.Data;
using CartaDePorte.Core.Servicios;
using System.Reflection;
using CartaDePorte.Common;

namespace CartaDePorte.Core.DAO
{
    public class SolicitudMeDAO : BaseDAO
    {

        private static SolicitudMeDAO instance;
        private static string mensaje = string.Empty;
        public SolicitudMeDAO() { }

        public static SolicitudMeDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SolicitudMeDAO();
                }
                return instance;
            }
        }

        //public int SaveOrUpdate(Solicitud solicitud)
        //{
        //    Object resul = null;
        //    SqlConnection conn1 = null;
        //    try
        //    {
        //        string sql = string.Empty;
        //        conn1 = new SqlConnection(connString);

        //        if (solicitud.IdSolicitud > 0)
        //        {
        //            resul = SqlHelper.ExecuteScalar(conn1, "GuardarSolicitud",
        //                solicitud.IdSolicitud,
        //                solicitud.TipoDeCarta.IdTipoDeCarta,
        //                solicitud.ObservacionAfip,
        //                solicitud.NumeroCartaDePorte,
        //                solicitud.Cee,
        //                solicitud.Ctg.Replace(".", ""),
        //                solicitud.FechaDeEmision,
        //                solicitud.FechaDeCarga,
        //                solicitud.FechaDeVencimiento,
        //                (solicitud.ProveedorTitularCartaDePorte != null) ? solicitud.ProveedorTitularCartaDePorte.IdProveedor : 0,
        //                (solicitud.ClienteIntermediario != null) ? solicitud.ClienteIntermediario.IdCliente : 0,
        //                (solicitud.ClienteRemitenteComercial != null) ? solicitud.ClienteRemitenteComercial.IdCliente : 0,
        //                solicitud.RemitenteComercialComoCanjeador,
        //                (solicitud.ClienteCorredor != null) ? solicitud.ClienteCorredor.IdCliente : 0,
        //                (solicitud.ClienteEntregador != null) ? solicitud.ClienteEntregador.IdCliente : 0,
        //                (solicitud.ClienteDestinatario != null) ? solicitud.ClienteDestinatario.IdCliente : 0,
        //                (solicitud.ClienteDestino != null) ? solicitud.ClienteDestino.IdCliente : 0,
        //                (solicitud.ProveedorTransportista != null) ? solicitud.ProveedorTransportista.IdProveedor : 0,
        //                (solicitud.ChoferTransportista != null) ? solicitud.ChoferTransportista.IdChofer : 0,
        //                (solicitud.Chofer != null) ? solicitud.Chofer.IdChofer : 0,
        //                solicitud.Grano.CosechaAfip.Codigo,
        //                solicitud.Grano.EspecieAfip.Codigo,
        //                solicitud.NumeroContrato,
        //                solicitud.SapContrato,
        //                solicitud.SinContrato,
        //                solicitud.CargaPesadaDestino,
        //                solicitud.KilogramosEstimados,
        //                string.Empty, // DeclaracionDeCalidad siempre vacio
        //                Convert.ToInt32(solicitud.ConformeCondicional),
        //                solicitud.PesoBruto,
        //                solicitud.PesoTara,
        //                solicitud.Observaciones,
        //                solicitud.LoteDeMaterial,
        //                (solicitud.IdEstablecimientoProcedencia != null) ? solicitud.IdEstablecimientoProcedencia.IdEstablecimiento : 0,
        //                (solicitud.IdEstablecimientoDestino != null) ? solicitud.IdEstablecimientoDestino.IdEstablecimiento : 0,
        //                solicitud.PatenteCamion,
        //                solicitud.PatenteAcoplado,
        //                solicitud.KmRecorridos,
        //                Convert.ToInt32(solicitud.EstadoFlete),
        //                solicitud.CantHoras,
        //                solicitud.TarifaReferencia,
        //                solicitud.TarifaReal,
        //                (solicitud.ClientePagadorDelFlete != null) ? solicitud.ClientePagadorDelFlete.IdCliente : 0,
        //                Convert.ToInt32(solicitud.EstadoEnSAP),
        //                Convert.ToInt32(solicitud.EstadoEnAFIP),
        //                solicitud.Grano.IdGrano,
        //                solicitud.CodigoAnulacionAfip,
        //                solicitud.FechaAnulacionAfip,
        //                solicitud.CodigoRespuestaEnvioSAP,
        //                solicitud.CodigoRespuestaAnulacionSAP,
        //                solicitud.MensajeRespuestaEnvioSAP,
        //                solicitud.MensajeRespuestaAnulacionSAP,
        //                (solicitud.IdEstablecimientoDestinoCambio != null) ? solicitud.IdEstablecimientoDestinoCambio.IdEstablecimiento : 0,
        //                (solicitud.ClienteDestinatarioCambio != null) ? solicitud.ClienteDestinatarioCambio.IdCliente : 0,
        //                solicitud.UsuarioModificacion,
        //                App.Usuario.IdEmpresa
        //                );


        //        }
        //        else
        //            resul = SqlHelper.ExecuteScalar(conn1, "GuardarSolicitud",
        //                solicitud.IdSolicitud,
        //                solicitud.TipoDeCarta.IdTipoDeCarta,
        //                solicitud.ObservacionAfip,
        //                solicitud.NumeroCartaDePorte,
        //                solicitud.Cee,
        //                solicitud.Ctg.Replace(".", ""),
        //                solicitud.FechaDeEmision,
        //                solicitud.FechaDeCarga,
        //                solicitud.FechaDeVencimiento,
        //                (solicitud.ProveedorTitularCartaDePorte != null) ? solicitud.ProveedorTitularCartaDePorte.IdProveedor : 0,
        //                (solicitud.ClienteIntermediario != null) ? solicitud.ClienteIntermediario.IdCliente : 0,
        //                (solicitud.ClienteRemitenteComercial != null) ? solicitud.ClienteRemitenteComercial.IdCliente : 0,
        //                solicitud.RemitenteComercialComoCanjeador,
        //                (solicitud.ClienteCorredor != null) ? solicitud.ClienteCorredor.IdCliente : 0,
        //                (solicitud.ClienteEntregador != null) ? solicitud.ClienteEntregador.IdCliente : 0,
        //                (solicitud.ClienteDestinatario != null) ? solicitud.ClienteDestinatario.IdCliente : 0,
        //                (solicitud.ClienteDestino != null) ? solicitud.ClienteDestino.IdCliente : 0,
        //                (solicitud.ProveedorTransportista != null) ? solicitud.ProveedorTransportista.IdProveedor : 0,
        //                (solicitud.ChoferTransportista != null) ? solicitud.ChoferTransportista.IdChofer : 0,
        //                (solicitud.Chofer != null) ? solicitud.Chofer.IdChofer : 0,
        //                solicitud.Grano.CosechaAfip.Codigo,
        //                solicitud.Grano.EspecieAfip.Codigo,
        //                solicitud.NumeroContrato,
        //                solicitud.SapContrato,
        //                solicitud.SinContrato,
        //                solicitud.CargaPesadaDestino,
        //                solicitud.KilogramosEstimados,
        //                string.Empty, // DeclaracionDeCalidad siempre vacio
        //                Convert.ToInt32(solicitud.ConformeCondicional),
        //                solicitud.PesoBruto,
        //                solicitud.PesoTara,
        //                solicitud.Observaciones,
        //                solicitud.LoteDeMaterial,
        //                (solicitud.IdEstablecimientoProcedencia != null) ? solicitud.IdEstablecimientoProcedencia.IdEstablecimiento : 0,
        //                (solicitud.IdEstablecimientoDestino != null) ? solicitud.IdEstablecimientoDestino.IdEstablecimiento : 0,
        //                solicitud.PatenteCamion,
        //                solicitud.PatenteAcoplado,
        //                solicitud.KmRecorridos,
        //                Convert.ToInt32(solicitud.EstadoFlete),
        //                solicitud.CantHoras,
        //                solicitud.TarifaReferencia,
        //                solicitud.TarifaReal,
        //                (solicitud.ClientePagadorDelFlete != null) ? solicitud.ClientePagadorDelFlete.IdCliente : 0,
        //                Convert.ToInt32(solicitud.EstadoEnSAP),
        //                Convert.ToInt32(solicitud.EstadoEnAFIP),
        //                solicitud.Grano.IdGrano,
        //                solicitud.CodigoAnulacionAfip,
        //                solicitud.FechaAnulacionAfip,
        //                solicitud.CodigoRespuestaEnvioSAP,
        //                solicitud.CodigoRespuestaAnulacionSAP,
        //                solicitud.MensajeRespuestaEnvioSAP,
        //                solicitud.MensajeRespuestaAnulacionSAP,
        //                (solicitud.IdEstablecimientoDestinoCambio != null) ? solicitud.IdEstablecimientoDestinoCambio.IdEstablecimiento : 0,
        //                (solicitud.ClienteDestinatarioCambio != null) ? solicitud.ClienteDestinatarioCambio.IdCliente : 0,
        //                solicitud.UsuarioCreacion,
        //                App.Usuario.IdEmpresa
        //                );

        //    }
        //    catch (System.Exception ex)
        //    {
        //        throw ExceptionFactory.CreateBusiness(ex, "ERROR Guardar Solicitud: " + ex.Message.ToString());

        //    }
        //    finally
        //    {
        //        conn1.Close();
        //    }
        //    if (solicitud.IdSolicitud > 0)
        //    {
        //        return solicitud.IdSolicitud;
        //    }
        //    else
        //    {
        //        return Convert.ToInt32(resul);
        //    }

        //}
        //public IList<Solicitud> GetAll()
        //{
        //    SqlConnection conn1 = null;
        //    try
        //    {
        //        string sql = string.Empty;
        //        conn1 = new SqlConnection(connString);

        //        DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSolicitud", 0, App.Usuario.IdEmpresa);
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            IList<Solicitud> result = new List<Solicitud>();
        //            foreach (DataRow row in ds.Tables[0].Rows)
        //            {
        //                Solicitud sol = new Solicitud();

        //                if (row["ObservacionAfip"].ToString().Contains("Reserva"))
        //                {
        //                    sol.IdSolicitud = Convert.ToInt32(row["IdSolicitud"]);
        //                    sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
        //                    sol.Cee = row["Cee"].ToString();
        //                    sol.ObservacionAfip = row["ObservacionAfip"].ToString();
        //                    if (!(row["FechaCreacion"] is System.DBNull))
        //                        sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
        //                    sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();

        //                }
        //                else
        //                {
        //                    sol.IdSolicitud = Convert.ToInt32(row["IdSolicitud"]);
        //                    sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
        //                    sol.Cee = row["Cee"].ToString();
        //                    sol.Ctg = row["Ctg"].ToString();
        //                    sol.TipoDeCarta = TipoDeCartaDAO.Instance.GetOne(Convert.ToInt32(row["IdTipoDeCarta"]));
        //                    sol.ProveedorTitularCartaDePorte = ProveedorDAO.Instance.GetOne(Convert.ToInt32(row["IdProveedorTitularCartaDePorte"]));
        //                    sol.IdEstablecimientoProcedencia = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["IdEstablecimientoProcedencia"]));
        //                    sol.IdEstablecimientoDestino = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["IdEstablecimientoDestino"]));
        //                    sol.ObservacionAfip = row["ObservacionAfip"].ToString();
        //                    sol.EstadoEnAFIP = (Enums.EstadoEnAFIP)Convert.ToInt32(row["EstadoEnAFIP"]);
        //                    sol.EstadoEnSAP = (Enums.EstadoEnvioSAP)Convert.ToInt32(row["EstadoEnSAP"]);
        //                    sol.CodigoRespuestaEnvioSAP = row["CodigoRespuestaEnvioSAP"].ToString();
        //                    sol.CodigoRespuestaAnulacionSAP = row["CodigoRespuestaAnulacionSAP"].ToString();
        //                    sol.MensajeRespuestaAnulacionSAP = row["MensajeRespuestaAnulacionSAP"].ToString();
        //                    sol.MensajeRespuestaEnvioSAP = row["MensajeRespuestaEnvioSAP"].ToString();

        //                    if (!(row["CodigoAnulacionAfip"] is System.DBNull))
        //                        sol.CodigoAnulacionAfip = Convert.ToInt64(row["CodigoAnulacionAfip"]);
        //                    if (!(row["FechaAnulacionAfip"] is System.DBNull))
        //                        sol.FechaAnulacionAfip = Convert.ToDateTime(row["FechaAnulacionAfip"]);


        //                    if (!(row["FechaCreacion"] is System.DBNull))
        //                        sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
        //                    sol.UsuarioModificacion = row["UsuarioModificacion"].ToString();

        //                    if (!(row["FechaModificacion"] is System.DBNull))
        //                        sol.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);
        //                    sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();

        //                }


        //                result.Add(sol);
        //            }

        //            return result;
        //        }
        //        else
        //        {
        //            return new List<Solicitud>();
        //        }

        //    }
        //    catch (System.Exception ex)
        //    {
        //        throw ExceptionFactory.CreateBusiness(ex, "ERROR GetAll Solicitud: " + ex.Message.ToString());

        //    }
        //    finally
        //    {
        //        conn1.Close();
        //    }

        //}
        //public IList<Solicitud> GetRechazadas()
        //{
        //    SqlConnection conn1 = null;
        //    try
        //    {
        //        string sql = string.Empty;
        //        conn1 = new SqlConnection(connString);

        //        DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetRechazadas", 0, App.Usuario.IdEmpresa);
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            IList<Solicitud> result = new List<Solicitud>();
        //            foreach (DataRow row in ds.Tables[0].Rows)
        //            {
        //                Solicitud sol = new Solicitud();
        //                if (row["ObservacionAfip"].ToString().Contains("Reserva"))
        //                {
        //                    sol.IdSolicitud = Convert.ToInt32(row["IdSolicitud"]);
        //                    sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
        //                    sol.Cee = row["Cee"].ToString();
        //                    sol.ObservacionAfip = row["ObservacionAfip"].ToString();
        //                    sol.TipoDeCarta = TipoDeCartaDAO.Instance.GetOne(Convert.ToInt32(row["IdTipoDeCarta"]));

        //                    if (!(row["IdEstablecimientoProcedencia"] is System.DBNull))
        //                        sol.IdEstablecimientoProcedencia = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["IdEstablecimientoProcedencia"]));

        //                    if (!(row["FechaCreacion"] is System.DBNull))
        //                        sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
        //                    sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();

        //                }
        //                else
        //                {
        //                    sol.IdSolicitud = Convert.ToInt32(row["IdSolicitud"]);
        //                    sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
        //                    sol.Cee = row["Cee"].ToString();
        //                    sol.Ctg = row["Ctg"].ToString();
        //                    sol.TipoDeCarta = TipoDeCartaDAO.Instance.GetOne(Convert.ToInt32(row["IdTipoDeCarta"]));
        //                    sol.ProveedorTitularCartaDePorte = ProveedorDAO.Instance.GetOne(Convert.ToInt32(row["IdProveedorTitularCartaDePorte"]));

        //                    sol.ObservacionAfip = row["ObservacionAfip"].ToString();
        //                    if (!(row["FechaDeEmision"] is System.DBNull))
        //                        sol.FechaDeEmision = Convert.ToDateTime(row["FechaDeEmision"]);
        //                    if (!(row["FechaDeCarga"] is System.DBNull))
        //                        sol.FechaDeCarga = Convert.ToDateTime(row["FechaDeCarga"]);
        //                    if (!(row["FechaDeVencimiento"] is System.DBNull))
        //                        sol.FechaDeVencimiento = Convert.ToDateTime(row["FechaDeVencimiento"]);
        //                    sol.ClienteIntermediario = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteIntermediario"]));
        //                    sol.ClienteRemitenteComercial = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteRemitenteComercial"]));
        //                    sol.RemitenteComercialComoCanjeador = Convert.ToBoolean(row["RemitenteComercialComoCanjeador"]);
        //                    sol.ClienteCorredor = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteCorredor"]));
        //                    sol.ClienteEntregador = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteEntregador"]));
        //                    sol.ClienteDestinatario = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteDestinatario"]));

        //                    sol.ClienteDestino = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteDestino"]));
        //                    sol.ProveedorTransportista = ProveedorDAO.Instance.GetOne(Convert.ToInt32(row["IdProveedorTransportista"]));
        //                    sol.ChoferTransportista = ChoferDAO.Instance.GetOne(Convert.ToInt32(row["IdChoferTransportista"]));

        //                    sol.Chofer = ChoferDAO.Instance.GetOne(Convert.ToInt32(row["IdChofer"]));
        //                    sol.Grano = GranoDAO.Instance.GetOne(Convert.ToInt32(row["IdGrano"]));
        //                    if (!(row["NumeroContrato"] is System.DBNull))
        //                        sol.NumeroContrato = Convert.ToInt32(row["NumeroContrato"]);
        //                    if (!(row["SapContrato"] is System.DBNull))
        //                        sol.SapContrato = Convert.ToInt32(row["SapContrato"]);

        //                    if (!(row["NumeroContrato"] is System.DBNull))
        //                        sol.SinContrato = true;
        //                    else
        //                        sol.SinContrato = false;

        //                    sol.CargaPesadaDestino = Convert.ToBoolean(row["CargaPesadaDestino"]);

        //                    sol.ConformeCondicional = (Enums.ConformeCondicional)Convert.ToInt32(row["IdConformeCondicional"]);

        //                    if (sol.CargaPesadaDestino)
        //                    {
        //                        sol.KilogramosEstimados = (long)Convert.ToDecimal(row["KilogramosEstimados"]);
        //                    }
        //                    else
        //                    {
        //                        sol.PesoBruto = (long)Convert.ToDecimal(row["PesoBruto"]);
        //                        sol.PesoTara = (long)Convert.ToDecimal(row["PesoTara"]);
        //                        sol.PesoNeto = (sol.PesoBruto - sol.PesoTara);
        //                    }

        //                    sol.Observaciones = row["Observaciones"].ToString();
        //                    sol.LoteDeMaterial = row["LoteDeMaterial"].ToString();

        //                    sol.IdEstablecimientoProcedencia = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["IdEstablecimientoProcedencia"]));
        //                    sol.IdEstablecimientoDestino = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["IdEstablecimientoDestino"]));

        //                    sol.PatenteCamion = row["PatenteCamion"].ToString();
        //                    sol.PatenteAcoplado = row["PatenteAcoplado"].ToString();
        //                    sol.KmRecorridos = (long)Convert.ToDecimal(row["KmRecorridos"]);
        //                    sol.EstadoFlete = (Enums.EstadoFlete)Convert.ToInt32(row["EstadoFlete"]);
        //                    sol.CantHoras = (long)Convert.ToDecimal(row["CantHoras"]);
        //                    sol.TarifaReferencia = Convert.ToDecimal(row["TarifaReferencia"]);
        //                    sol.TarifaReal = Convert.ToDecimal(row["TarifaReal"]);
        //                    sol.ClientePagadorDelFlete = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClientePagadorDelFlete"]));
        //                    sol.EstadoEnSAP = (Enums.EstadoEnvioSAP)Convert.ToInt32(row["EstadoEnSAP"]);
        //                    sol.EstadoEnAFIP = (Enums.EstadoEnAFIP)Convert.ToInt32(row["EstadoEnAFIP"]);
        //                    sol.CodigoRespuestaEnvioSAP = row["CodigoRespuestaEnvioSAP"].ToString();
        //                    sol.CodigoRespuestaAnulacionSAP = row["CodigoRespuestaAnulacionSAP"].ToString();
        //                    sol.MensajeRespuestaAnulacionSAP = row["MensajeRespuestaAnulacionSAP"].ToString();
        //                    sol.MensajeRespuestaEnvioSAP = row["MensajeRespuestaEnvioSAP"].ToString();

        //                    if (!(row["IdEstablecimientoDestinoCambio"] is System.DBNull))
        //                        sol.IdEstablecimientoDestinoCambio = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["IdEstablecimientoDestinoCambio"]));
        //                    if (!(row["IdClienteDestinatarioCambio"] is System.DBNull))
        //                        sol.ClienteDestinatarioCambio = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteDestinatarioCambio"]));


        //                    if (!(row["CodigoAnulacionAfip"] is System.DBNull))
        //                        sol.CodigoAnulacionAfip = Convert.ToInt64(row["CodigoAnulacionAfip"]);
        //                    if (!(row["FechaAnulacionAfip"] is System.DBNull))
        //                        sol.FechaAnulacionAfip = Convert.ToDateTime(row["FechaAnulacionAfip"]);


        //                    if (!(row["FechaCreacion"] is System.DBNull))
        //                        sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
        //                    sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();


        //                    if (!(row["FechaModificacion"] is System.DBNull))
        //                        sol.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);
        //                    sol.UsuarioModificacion = row["UsuarioModificacion"].ToString();


        //                }

        //                result.Add(sol);
        //            }

        //            return result;
        //        }
        //        else
        //        {
        //            return new List<Solicitud>();
        //        }

        //    }
        //    catch (System.Exception ex)
        //    {
        //        throw ExceptionFactory.CreateBusiness(ex, "ERROR GetAll Solicitud: " + ex.Message.ToString());

        //    }
        //    finally
        //    {
        //        conn1.Close();
        //    }

        //}
        //public IList<SolicitudMe> GetTop(int nrotop)
        //{
        //    SqlConnection conn1 = null;
        //    try
        //    {
        //        string sql = string.Empty;
        //        conn1 = new SqlConnection(connString);

        //        DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSolicitudMeTop", nrotop, App.Usuario.IdEmpresa);
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            IList<SolicitudMe> result = new List<SolicitudMe>();
        //            foreach (DataRow row in ds.Tables[0].Rows)
        //            {
        //                var sol = new SolicitudMe();
        //                if (row["ObservacionAfip"].ToString().Contains("Reserva"))
        //                {
        //                    sol.IdSolicitud = Convert.ToInt32(row["IdSolicitud"]);
        //                    sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
        //                    sol.Cee = row["Cee"].ToString();
        //                    sol.ObservacionAfip = row["ObservacionAfip"].ToString();
        //                    if (!(row["FechaCreacion"] is System.DBNull))
        //                        sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
        //                    sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();

        //                    sol.EstadoEnAFIP = (Enums.EstadoEnAFIP)Convert.ToInt32(row["EstadoEnAFIP"]);
        //                    sol.EstadoEnSAP = (Enums.EstadoEnvioSAP)Convert.ToInt32(row["EstadoEnSAP"]);


        //                }
        //                else
        //                {

        //                    sol.IdSolicitud = Convert.ToInt32(row["IdSolicitud"]);
        //                    sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
        //                    sol.Cee = row["Cee"].ToString();
        //                    sol.Ctg = row["Ctg"].ToString();
        //                    sol.TipoDeCarta = TipoDeCartaDAO.Instance.GetOne(Convert.ToInt32(row["IdTipoDeCarta"]));
        //                    sol.ProveedorTitularCartaDePorte = ProveedorDAO.Instance.GetOne(Convert.ToInt32(row["IdProveedorTitularCartaDePorte"]));
        //                    sol.IdEstablecimientoProcedencia = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["IdEstablecimientoProcedencia"]));
        //                    sol.IdEstablecimientoDestino = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["IdEstablecimientoDestino"]));
        //                    sol.ObservacionAfip = row["ObservacionAfip"].ToString();
        //                    sol.EstadoEnAFIP = (Enums.EstadoEnAFIP)Convert.ToInt32(row["EstadoEnAFIP"]);
        //                    sol.EstadoEnSAP = (Enums.EstadoEnvioSAP)Convert.ToInt32(row["EstadoEnSAP"]);
        //                    sol.CodigoRespuestaEnvioSAP = row["CodigoRespuestaEnvioSAP"].ToString();
        //                    sol.CodigoRespuestaAnulacionSAP = row["CodigoRespuestaAnulacionSAP"].ToString();
        //                    sol.MensajeRespuestaAnulacionSAP = row["MensajeRespuestaAnulacionSAP"].ToString();
        //                    sol.MensajeRespuestaEnvioSAP = row["MensajeRespuestaEnvioSAP"].ToString();

        //                    if (!(row["CodigoAnulacionAfip"] is System.DBNull))
        //                        sol.CodigoAnulacionAfip = Convert.ToInt64(row["CodigoAnulacionAfip"]);
        //                    if (!(row["FechaAnulacionAfip"] is System.DBNull))
        //                        sol.FechaAnulacionAfip = Convert.ToDateTime(row["FechaAnulacionAfip"]);


        //                    if (!(row["FechaCreacion"] is System.DBNull))
        //                        sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
        //                    sol.UsuarioModificacion = row["UsuarioModificacion"].ToString();

        //                    if (!(row["FechaModificacion"] is System.DBNull))
        //                        sol.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);
        //                    sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();

        //                }


        //                result.Add(sol);
        //            }

        //            return result;
        //        }
        //        else
        //        {
        //            return new List<SolicitudMe>();
        //        }

        //    }
        //    catch (System.Exception ex)
        //    {
        //        throw ExceptionFactory.CreateBusiness(ex, "ERROR GetAll Solicitud: " + ex.Message.ToString());

        //    }
        //    finally
        //    {
        //        conn1.Close();
        //    }

        //}
        //public IList<Solicitud> GetTopConfirmacion()
        //{
        //    SqlConnection conn1 = null;
        //    try
        //    {
        //        string sql = string.Empty;
        //        conn1 = new SqlConnection(connString);

        //        DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSolicitudTopConfirmacion", App.Usuario.IdEmpresa);
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            IList<Solicitud> result = new List<Solicitud>();
        //            foreach (DataRow row in ds.Tables[0].Rows)
        //            {
        //                Solicitud sol = new Solicitud();

        //                sol.IdSolicitud = Convert.ToInt32(row["IdSolicitud"]);
        //                sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
        //                sol.TipoDeCarta = TipoDeCartaDAO.Instance.GetOne(Convert.ToInt32(row["IdTipoDeCarta"]));
        //                sol.IdEstablecimientoProcedencia = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["IdEstablecimientoProcedencia"]));
        //                sol.IdEstablecimientoDestino = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["IdEstablecimientoDestino"]));
        //                sol.PesoNeto = (row["PesoNeto"] is System.DBNull) ? 0 : Convert.ToInt64(row["PesoNeto"]);
        //                sol.KilogramosEstimados = (row["KilogramosEstimados"] is System.DBNull) ? 0 : Convert.ToInt64(row["KilogramosEstimados"]);
        //                sol.CargaPesadaDestino = Convert.ToBoolean(row["CargaPesadaDestino"]);

        //                if (!(row["FechaCreacion"] is System.DBNull))
        //                    sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
        //                sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();

        //                result.Add(sol);
        //            }

        //            return result;
        //        }
        //        else
        //        {
        //            return new List<Solicitud>();
        //        }

        //    }
        //    catch (System.Exception ex)
        //    {
        //        throw ExceptionFactory.CreateBusiness(ex, "ERROR GetAll Solicitud: " + ex.Message.ToString());

        //    }
        //    finally
        //    {
        //        conn1.Close();
        //    }

        //}
        public IList<SolicitudMe> GetFiltro(string busqueda, string estadoAFIP, string estadoSAP)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSolicitudMeFiltro", busqueda, estadoAFIP, estadoSAP);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<SolicitudMe> result = new List<SolicitudMe>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var sol = new SolicitudMe();

                        mensaje = "IDSolicitud: " + Convert.ToInt32(row["IdSolicitud"]).ToString();

                        if (row["ObservacionAfip"].ToString().Contains("Reserva"))
                        {
                            sol.IdSolicitud = Convert.ToInt32(row["IdSolicitud"]);
                            sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
                            sol.Cee = row["Cee"].ToString();
                            sol.ObservacionAfip = row["ObservacionAfip"].ToString();
                            if (!(row["FechaCreacion"] is System.DBNull))
                                sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
                            sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();

                            sol.EstadoEnAFIP = (Enums.EstadoEnAFIP)Convert.ToInt32(row["EstadoEnAFIP"]);
                            sol.EstadoEnSAP = (Enums.EstadoEnvioSAP)Convert.ToInt32(row["EstadoEnSAP"]);

                        }
                        else
                        {

                            sol.IdSolicitud = Convert.ToInt32(row["IdSolicitud"]); // si
                            sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();// si
                            //sol.Cee = row["Cee"].ToString();
                            sol.Ctg = row["Ctg"].ToString();// si
                            sol.TipoDeCartaString = row["DescripcionTipoDeCarta"].ToString();// si
                            sol.ProveedorTitularCartaDePorteString = row["ProveedorNombreTitularCartaDePorte"].ToString();// si
                            sol.EstablecimientoProcedenciaString = row["DescripcionEstablecimientoOrigen"].ToString();// si
                            //sol.IdEstablecimientoDestino = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["IdEstablecimientoDestino"]));
                            sol.ObservacionAfip = row["ObservacionAfip"].ToString();// si
                            sol.EstadoEnAFIP = (Enums.EstadoEnAFIP)Convert.ToInt32(row["EstadoEnAFIP"]);// si
                            sol.EstadoEnSAP = (Enums.EstadoEnvioSAP)Convert.ToInt32(row["EstadoEnSAP"]);// si
                            sol.CodigoRespuestaEnvioSAP = row["CodigoRespuestaEnvioSAP"].ToString();
                            sol.CodigoRespuestaAnulacionSAP = row["CodigoRespuestaAnulacionSAP"].ToString();
                            sol.MensajeRespuestaAnulacionSAP = row["MensajeRespuestaAnulacionSAP"].ToString();
                            sol.MensajeRespuestaEnvioSAP = row["MensajeRespuestaEnvioSAP"].ToString();

                            if (!(row["CodigoAnulacionAfip"] is System.DBNull))
                                sol.CodigoAnulacionAfip = Convert.ToInt64(row["CodigoAnulacionAfip"]);
                            if (!(row["FechaAnulacionAfip"] is System.DBNull))
                                sol.FechaAnulacionAfip = Convert.ToDateTime(row["FechaAnulacionAfip"]);

                            if (!(row["FechaCreacion"] is System.DBNull))
                                sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);// si
                            sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();


                            if (!(row["FechaModificacion"] is System.DBNull))
                                sol.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);
                            sol.UsuarioModificacion = row["UsuarioModificacion"].ToString();// si
                            //sol.Sox1116A = Sox1116ADAO.Instance.GetOneByIdSoclicitud(sol.IdSolicitud);

                            sol.IdEmpresa = Tools.Value2<int>(row["IdEmpresa"]);

                        }

                        result.Add(sol);
                    }

                    return result;
                }
                else
                {
                    return new List<SolicitudMe>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR GetAll Solicitud: " + mensaje + " - " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }
        //public IList<Solicitud> GetFiltroConfirmacion(string busqueda)
        //{
        //    SqlConnection conn1 = null;
        //    try
        //    {
        //        string sql = string.Empty;
        //        conn1 = new SqlConnection(connString);

        //        DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSolicitudFiltroConfirmacion", busqueda, App.Usuario.IdEmpresa);
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            IList<Solicitud> result = new List<Solicitud>();
        //            foreach (DataRow row in ds.Tables[0].Rows)
        //            {
        //                Solicitud sol = new Solicitud();

        //                sol.IdSolicitud = Convert.ToInt32(row["IdSolicitud"]);
        //                sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
        //                sol.TipoDeCarta = TipoDeCartaDAO.Instance.GetOne(Convert.ToInt32(row["IdTipoDeCarta"]));
        //                sol.IdEstablecimientoProcedencia = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["IdEstablecimientoProcedencia"]));
        //                sol.IdEstablecimientoDestino = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["IdEstablecimientoDestino"]));
        //                sol.PesoNeto = (row["PesoNeto"] is System.DBNull) ? 0 : Convert.ToInt64(row["PesoNeto"]);
        //                sol.KilogramosEstimados = (row["KilogramosEstimados"] is System.DBNull) ? 0 : Convert.ToInt64(row["KilogramosEstimados"]);
        //                sol.CargaPesadaDestino = Convert.ToBoolean(row["CargaPesadaDestino"]);

        //                if (!(row["FechaCreacion"] is System.DBNull))
        //                    sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
        //                sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();

        //                result.Add(sol);
        //            }

        //            return result;
        //        }
        //        else
        //        {
        //            return new List<Solicitud>();
        //        }

        //    }
        //    catch (System.Exception ex)
        //    {
        //        throw ExceptionFactory.CreateBusiness(ex, "ERROR GetAll Solicitud: " + ex.Message.ToString());

        //    }
        //    finally
        //    {
        //        conn1.Close();
        //    }

        //}
        public SolicitudMe GetOne(int IdSolicitud, int IdEmpresa)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSolicitud", IdSolicitud, IdEmpresa);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<SolicitudMe> result = new List<SolicitudMe>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var sol = new SolicitudMe();
                        if (row["ObservacionAfip"].ToString().Contains("Reserva"))
                        {
                            sol.IdSolicitud = Convert.ToInt32(row["IdSolicitud"]);
                            sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
                            sol.Cee = row["Cee"].ToString();
                            sol.ObservacionAfip = row["ObservacionAfip"].ToString();
                            sol.TipoDeCarta = TipoDeCartaDAO.Instance.GetOne(Convert.ToInt32(row["IdTipoDeCarta"]));

                            if (!(row["IdEstablecimientoProcedencia"] is System.DBNull))
                                sol.IdEstablecimientoProcedencia = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["IdEstablecimientoProcedencia"]));

                            if (!(row["FechaCreacion"] is System.DBNull))
                                sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
                            sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();

                        }
                        else
                        {
                            sol.IdSolicitud = Convert.ToInt32(row["IdSolicitud"]);
                            sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
                            sol.Cee = row["Cee"].ToString();
                            sol.Ctg = row["Ctg"].ToString();
                            sol.TipoDeCarta = TipoDeCartaDAO.Instance.GetOne(Convert.ToInt32(row["IdTipoDeCarta"]));
                            sol.ProveedorTitularCartaDePorte = ProveedorDAO.Instance.GetOne(Convert.ToInt32(row["IdProveedorTitularCartaDePorte"]));

                            sol.ObservacionAfip = row["ObservacionAfip"].ToString();
                            if (!(row["FechaDeEmision"] is System.DBNull))
                                sol.FechaDeEmision = Convert.ToDateTime(row["FechaDeEmision"]);
                            if (!(row["FechaDeCarga"] is System.DBNull))
                                sol.FechaDeCarga = Convert.ToDateTime(row["FechaDeCarga"]);
                            if (!(row["FechaDeVencimiento"] is System.DBNull))
                                sol.FechaDeVencimiento = Convert.ToDateTime(row["FechaDeVencimiento"]);
                            sol.ClienteIntermediario = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteIntermediario"]));
                            sol.ClienteRemitenteComercial = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteRemitenteComercial"]));
                            sol.RemitenteComercialComoCanjeador = Convert.ToBoolean(row["RemitenteComercialComoCanjeador"]);
                            sol.ClienteCorredor = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteCorredor"]));
                            sol.ClienteEntregador = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteEntregador"]));
                            sol.ClienteDestinatario = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteDestinatario"]));

                            sol.ClienteDestino = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteDestino"]));
                            sol.ProveedorTransportista = ProveedorDAO.Instance.GetOne(Convert.ToInt32(row["IdProveedorTransportista"]));
                            sol.ChoferTransportista = ChoferDAO.Instance.GetOne(Convert.ToInt32(row["IdChoferTransportista"]));

                            sol.Chofer = ChoferDAO.Instance.GetOne(Convert.ToInt32(row["IdChofer"]));
                            sol.Grano = GranoDAO.Instance.GetOne(Convert.ToInt32(row["IdGrano"]));
                            if (!(row["NumeroContrato"] is System.DBNull))
                                sol.NumeroContrato = Convert.ToInt32(row["NumeroContrato"]);
                            if (!(row["SapContrato"] is System.DBNull))
                                sol.SapContrato = Convert.ToInt32(row["SapContrato"]);

                            if (!(row["NumeroContrato"] is System.DBNull))
                                sol.SinContrato = true;
                            else
                                sol.SinContrato = false;

                            sol.CargaPesadaDestino = Convert.ToBoolean(row["CargaPesadaDestino"]);

                            sol.ConformeCondicional = (Enums.ConformeCondicional)Convert.ToInt32(row["IdConformeCondicional"]);

                            if (sol.CargaPesadaDestino)
                            {
                                sol.KilogramosEstimados = (long)Convert.ToDecimal(row["KilogramosEstimados"]);
                            }
                            else
                            {
                                sol.PesoBruto = (long)Convert.ToDecimal(row["PesoBruto"]);
                                sol.PesoTara = (long)Convert.ToDecimal(row["PesoTara"]);
                                sol.PesoNeto = (sol.PesoBruto - sol.PesoTara);
                            }

                            sol.Observaciones = row["Observaciones"].ToString();
                            sol.LoteDeMaterial = row["LoteDeMaterial"].ToString();

                            sol.IdEstablecimientoProcedencia = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["IdEstablecimientoProcedencia"]));
                            sol.IdEstablecimientoDestino = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["IdEstablecimientoDestino"]));

                            sol.PatenteCamion = row["PatenteCamion"].ToString();
                            sol.PatenteAcoplado = row["PatenteAcoplado"].ToString();
                            sol.KmRecorridos = (long)Convert.ToDecimal(row["KmRecorridos"]);
                            sol.EstadoFlete = (Enums.EstadoFlete)Convert.ToInt32(row["EstadoFlete"]);
                            sol.CantHoras = (long)Convert.ToDecimal(row["CantHoras"]);
                            sol.TarifaReferencia = Convert.ToDecimal(row["TarifaReferencia"]);
                            sol.TarifaReal = Convert.ToDecimal(row["TarifaReal"]);
                            sol.ClientePagadorDelFlete = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClientePagadorDelFlete"]));
                            sol.EstadoEnSAP = (Enums.EstadoEnvioSAP)Convert.ToInt32(row["EstadoEnSAP"]);
                            sol.EstadoEnAFIP = (Enums.EstadoEnAFIP)Convert.ToInt32(row["EstadoEnAFIP"]);
                            sol.CodigoRespuestaEnvioSAP = row["CodigoRespuestaEnvioSAP"].ToString();
                            sol.CodigoRespuestaAnulacionSAP = row["CodigoRespuestaAnulacionSAP"].ToString();
                            sol.MensajeRespuestaAnulacionSAP = row["MensajeRespuestaAnulacionSAP"].ToString();
                            sol.MensajeRespuestaEnvioSAP = row["MensajeRespuestaEnvioSAP"].ToString();

                            if (!(row["IdEstablecimientoDestinoCambio"] is System.DBNull))
                                sol.IdEstablecimientoDestinoCambio = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["IdEstablecimientoDestinoCambio"]));
                            if (!(row["IdClienteDestinatarioCambio"] is System.DBNull))
                                sol.ClienteDestinatarioCambio = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteDestinatarioCambio"]));


                            if (!(row["CodigoAnulacionAfip"] is System.DBNull))
                                sol.CodigoAnulacionAfip = Convert.ToInt64(row["CodigoAnulacionAfip"]);
                            if (!(row["FechaAnulacionAfip"] is System.DBNull))
                                sol.FechaAnulacionAfip = Convert.ToDateTime(row["FechaAnulacionAfip"]);


                            if (!(row["FechaCreacion"] is System.DBNull))
                                sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
                            sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();


                            if (!(row["FechaModificacion"] is System.DBNull))
                                sol.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);
                            sol.UsuarioModificacion = row["UsuarioModificacion"].ToString();


                        }

                        result.Add(sol);
                    }

                    if (result.Count > 0)
                        return result.First();
                    else
                        return null;
                }
                else
                {
                    return null;
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get GetSolicitud: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();

            }

        }
        //public Solicitud GetSolicitudByCDP(string cdp)
        //{
        //    SqlConnection conn1 = null;
        //    try
        //    {
        //        string sql = string.Empty;
        //        conn1 = new SqlConnection(connString);

        //        DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSolicitudByCDP", cdp, App.Usuario.IdEmpresa);
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            IList<Solicitud> result = new List<Solicitud>();
        //            foreach (DataRow row in ds.Tables[0].Rows)
        //            {
        //                Solicitud sol = new Solicitud();

        //                if (row["ObservacionAfip"].ToString().Contains("Reserva"))
        //                {
        //                    sol.IdSolicitud = Convert.ToInt32(row["IdSolicitud"]);
        //                    sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
        //                    sol.Cee = row["Cee"].ToString();
        //                    sol.ObservacionAfip = row["ObservacionAfip"].ToString();
        //                    if (!(row["FechaCreacion"] is System.DBNull))
        //                        sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
        //                    sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();

        //                }
        //                else
        //                {

        //                    sol.IdSolicitud = Convert.ToInt32(row["IdSolicitud"]);
        //                    sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
        //                    sol.Cee = row["Cee"].ToString();
        //                    sol.Ctg = row["Ctg"].ToString();
        //                    sol.TipoDeCarta = TipoDeCartaDAO.Instance.GetOne(Convert.ToInt32(row["IdTipoDeCarta"]));
        //                    sol.ProveedorTitularCartaDePorte = ProveedorDAO.Instance.GetOne(Convert.ToInt32(row["IdProveedorTitularCartaDePorte"]));

        //                    sol.ObservacionAfip = row["ObservacionAfip"].ToString();
        //                    if (!(row["FechaDeEmision"] is System.DBNull))
        //                        sol.FechaDeEmision = Convert.ToDateTime(row["FechaDeEmision"]);
        //                    if (!(row["FechaDeCarga"] is System.DBNull))
        //                        sol.FechaDeCarga = Convert.ToDateTime(row["FechaDeCarga"]);
        //                    if (!(row["FechaDeVencimiento"] is System.DBNull))
        //                        sol.FechaDeVencimiento = Convert.ToDateTime(row["FechaDeVencimiento"]);
        //                    sol.ClienteIntermediario = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteIntermediario"]));
        //                    sol.ClienteRemitenteComercial = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteRemitenteComercial"]));
        //                    sol.RemitenteComercialComoCanjeador = Convert.ToBoolean(row["RemitenteComercialComoCanjeador"]);
        //                    sol.ClienteCorredor = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteCorredor"]));
        //                    sol.ClienteEntregador = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteEntregador"]));
        //                    sol.ClienteDestinatario = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteDestinatario"]));
        //                    sol.ClienteDestino = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteDestino"]));
        //                    sol.ProveedorTransportista = ProveedorDAO.Instance.GetOne(Convert.ToInt32(row["IdProveedorTransportista"]));
        //                    sol.ChoferTransportista = ChoferDAO.Instance.GetOne(Convert.ToInt32(row["IdChoferTransportista"]));

        //                    sol.Chofer = ChoferDAO.Instance.GetOne(Convert.ToInt32(row["IdChofer"]));
        //                    sol.Grano = GranoDAO.Instance.GetOne(Convert.ToInt32(row["IdGrano"]));
        //                    if (!(row["NumeroContrato"] is System.DBNull))
        //                        sol.NumeroContrato = Convert.ToInt32(row["NumeroContrato"]);
        //                    if (!(row["SapContrato"] is System.DBNull))
        //                        sol.SapContrato = Convert.ToInt32(row["SapContrato"]);

        //                    if (!(row["NumeroContrato"] is System.DBNull))
        //                        sol.SinContrato = true;
        //                    else
        //                        sol.SinContrato = false;

        //                    sol.CargaPesadaDestino = Convert.ToBoolean(row["CargaPesadaDestino"]);

        //                    sol.ConformeCondicional = (Enums.ConformeCondicional)Convert.ToInt32(row["IdConformeCondicional"]);

        //                    if (sol.CargaPesadaDestino)
        //                    {
        //                        sol.KilogramosEstimados = (long)Convert.ToDecimal(row["KilogramosEstimados"]);
        //                    }
        //                    else
        //                    {
        //                        sol.PesoBruto = (long)Convert.ToDecimal(row["PesoBruto"]);
        //                        sol.PesoTara = (long)Convert.ToDecimal(row["PesoTara"]);
        //                        sol.PesoNeto = (sol.PesoBruto - sol.PesoTara);
        //                    }

        //                    sol.Observaciones = row["Observaciones"].ToString();
        //                    sol.LoteDeMaterial = row["LoteDeMaterial"].ToString();

        //                    sol.IdEstablecimientoProcedencia = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["IdEstablecimientoProcedencia"]));
        //                    sol.IdEstablecimientoDestino = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["IdEstablecimientoDestino"]));
        //                    sol.PatenteCamion = row["PatenteCamion"].ToString();
        //                    sol.PatenteAcoplado = row["PatenteAcoplado"].ToString();
        //                    sol.KmRecorridos = (long)Convert.ToDecimal(row["KmRecorridos"]);
        //                    sol.EstadoFlete = (Enums.EstadoFlete)Convert.ToInt32(row["EstadoFlete"]);
        //                    sol.CantHoras = (long)Convert.ToDecimal(row["CantHoras"]);
        //                    sol.TarifaReferencia = Convert.ToDecimal(row["TarifaReferencia"]);
        //                    sol.TarifaReal = Convert.ToDecimal(row["TarifaReal"]);
        //                    sol.ClientePagadorDelFlete = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClientePagadorDelFlete"]));
        //                    sol.EstadoEnSAP = (Enums.EstadoEnvioSAP)Convert.ToInt32(row["EstadoEnSAP"]);
        //                    sol.EstadoEnAFIP = (Enums.EstadoEnAFIP)Convert.ToInt32(row["EstadoEnAFIP"]);
        //                    sol.CodigoRespuestaEnvioSAP = row["CodigoRespuestaEnvioSAP"].ToString();
        //                    sol.CodigoRespuestaAnulacionSAP = row["CodigoRespuestaAnulacionSAP"].ToString();
        //                    sol.MensajeRespuestaAnulacionSAP = row["MensajeRespuestaAnulacionSAP"].ToString();
        //                    sol.MensajeRespuestaEnvioSAP = row["MensajeRespuestaEnvioSAP"].ToString();

        //                    if (!(row["IdEstablecimientoDestinoCambio"] is System.DBNull))
        //                        sol.IdEstablecimientoDestinoCambio = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["IdEstablecimientoDestinoCambio"]));
        //                    if (!(row["IdClienteDestinatarioCambio"] is System.DBNull))
        //                        sol.ClienteDestinatarioCambio = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteDestinatarioCambio"]));


        //                    if (!(row["CodigoAnulacionAfip"] is System.DBNull))
        //                        sol.CodigoAnulacionAfip = Convert.ToInt64(row["CodigoAnulacionAfip"]);
        //                    if (!(row["FechaAnulacionAfip"] is System.DBNull))
        //                        sol.FechaAnulacionAfip = Convert.ToDateTime(row["FechaAnulacionAfip"]);


        //                    if (!(row["FechaCreacion"] is System.DBNull))
        //                        sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
        //                    sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();


        //                    if (!(row["FechaModificacion"] is System.DBNull))
        //                        sol.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);
        //                    sol.UsuarioModificacion = row["UsuarioModificacion"].ToString();


        //                }

        //                result.Add(sol);
        //            }

        //            if (result.Count > 0)
        //                return result.First();
        //            else
        //                return null;
        //        }
        //        else
        //        {
        //            return null;
        //        }

        //    }
        //    catch (System.Exception ex)
        //    {
        //        throw ExceptionFactory.CreateBusiness(ex, "ERROR Get GetSolicitud: " + ex.Message.ToString());

        //    }
        //    finally
        //    {
        //        conn1.Close();
        //    }

        //}
        //public Solicitud GetSolicitudByCTG(string ctg)
        //{
        //    SqlConnection conn1 = null;
        //    try
        //    {
        //        string sql = string.Empty;
        //        conn1 = new SqlConnection(connString);

        //        DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSolicitudByCTG", ctg, App.Usuario.IdEmpresa);
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            IList<Solicitud> result = new List<Solicitud>();
        //            foreach (DataRow row in ds.Tables[0].Rows)
        //            {
        //                Solicitud sol = new Solicitud();

        //                if (row["ObservacionAfip"].ToString().Contains("Reserva"))
        //                {
        //                    sol.IdSolicitud = Convert.ToInt32(row["IdSolicitud"]);
        //                    sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
        //                    sol.Cee = row["Cee"].ToString();
        //                    sol.ObservacionAfip = row["ObservacionAfip"].ToString();
        //                    if (!(row["FechaCreacion"] is System.DBNull))
        //                        sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
        //                    sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();

        //                }
        //                else
        //                {

        //                    sol.IdSolicitud = Convert.ToInt32(row["IdSolicitud"]);
        //                    sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
        //                    sol.Cee = row["Cee"].ToString();
        //                    sol.Ctg = row["Ctg"].ToString();
        //                    sol.TipoDeCarta = TipoDeCartaDAO.Instance.GetOne(Convert.ToInt32(row["IdTipoDeCarta"]));
        //                    sol.ProveedorTitularCartaDePorte = ProveedorDAO.Instance.GetOne(Convert.ToInt32(row["IdProveedorTitularCartaDePorte"]));

        //                    sol.ObservacionAfip = row["ObservacionAfip"].ToString();
        //                    if (!(row["FechaDeEmision"] is System.DBNull))
        //                        sol.FechaDeEmision = Convert.ToDateTime(row["FechaDeEmision"]);
        //                    if (!(row["FechaDeCarga"] is System.DBNull))
        //                        sol.FechaDeCarga = Convert.ToDateTime(row["FechaDeCarga"]);
        //                    if (!(row["FechaDeVencimiento"] is System.DBNull))
        //                        sol.FechaDeVencimiento = Convert.ToDateTime(row["FechaDeVencimiento"]);
        //                    sol.ClienteIntermediario = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteIntermediario"]));
        //                    sol.ClienteRemitenteComercial = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteRemitenteComercial"]));
        //                    sol.RemitenteComercialComoCanjeador = Convert.ToBoolean(row["RemitenteComercialComoCanjeador"]);
        //                    sol.ClienteCorredor = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteCorredor"]));
        //                    sol.ClienteEntregador = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteEntregador"]));
        //                    sol.ClienteDestinatario = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteDestinatario"]));
        //                    sol.ClienteDestino = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteDestino"]));
        //                    sol.ProveedorTransportista = ProveedorDAO.Instance.GetOne(Convert.ToInt32(row["IdProveedorTransportista"]));
        //                    sol.ChoferTransportista = ChoferDAO.Instance.GetOne(Convert.ToInt32(row["IdChoferTransportista"]));

        //                    sol.Chofer = ChoferDAO.Instance.GetOne(Convert.ToInt32(row["IdChofer"]));
        //                    sol.Grano = GranoDAO.Instance.GetOne(Convert.ToInt32(row["IdGrano"]));
        //                    if (!(row["NumeroContrato"] is System.DBNull))
        //                        sol.NumeroContrato = Convert.ToInt32(row["NumeroContrato"]);
        //                    if (!(row["SapContrato"] is System.DBNull))
        //                        sol.SapContrato = Convert.ToInt32(row["SapContrato"]);

        //                    if (!(row["NumeroContrato"] is System.DBNull))
        //                        sol.SinContrato = true;
        //                    else
        //                        sol.SinContrato = false;

        //                    sol.CargaPesadaDestino = Convert.ToBoolean(row["CargaPesadaDestino"]);

        //                    sol.ConformeCondicional = (Enums.ConformeCondicional)Convert.ToInt32(row["IdConformeCondicional"]);

        //                    if (sol.CargaPesadaDestino)
        //                    {
        //                        sol.KilogramosEstimados = (long)Convert.ToDecimal(row["KilogramosEstimados"]);
        //                    }
        //                    else
        //                    {
        //                        sol.PesoBruto = (long)Convert.ToDecimal(row["PesoBruto"]);
        //                        sol.PesoTara = (long)Convert.ToDecimal(row["PesoTara"]);
        //                        sol.PesoNeto = (sol.PesoBruto - sol.PesoTara);
        //                    }

        //                    sol.Observaciones = row["Observaciones"].ToString();
        //                    sol.LoteDeMaterial = row["LoteDeMaterial"].ToString();

        //                    sol.IdEstablecimientoProcedencia = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["IdEstablecimientoProcedencia"]));
        //                    sol.IdEstablecimientoDestino = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["IdEstablecimientoDestino"]));
        //                    sol.PatenteCamion = row["PatenteCamion"].ToString();
        //                    sol.PatenteAcoplado = row["PatenteAcoplado"].ToString();
        //                    sol.KmRecorridos = (long)Convert.ToDecimal(row["KmRecorridos"]);
        //                    sol.EstadoFlete = (Enums.EstadoFlete)Convert.ToInt32(row["EstadoFlete"]);
        //                    sol.CantHoras = (long)Convert.ToDecimal(row["CantHoras"]);
        //                    sol.TarifaReferencia = Convert.ToDecimal(row["TarifaReferencia"]);
        //                    sol.TarifaReal = Convert.ToDecimal(row["TarifaReal"]);
        //                    sol.ClientePagadorDelFlete = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClientePagadorDelFlete"]));
        //                    sol.EstadoEnSAP = (Enums.EstadoEnvioSAP)Convert.ToInt32(row["EstadoEnSAP"]);
        //                    sol.EstadoEnAFIP = (Enums.EstadoEnAFIP)Convert.ToInt32(row["EstadoEnAFIP"]);
        //                    sol.CodigoRespuestaEnvioSAP = row["CodigoRespuestaEnvioSAP"].ToString();
        //                    sol.CodigoRespuestaAnulacionSAP = row["CodigoRespuestaAnulacionSAP"].ToString();
        //                    sol.MensajeRespuestaAnulacionSAP = row["MensajeRespuestaAnulacionSAP"].ToString();
        //                    sol.MensajeRespuestaEnvioSAP = row["MensajeRespuestaEnvioSAP"].ToString();

        //                    if (!(row["IdEstablecimientoDestinoCambio"] is System.DBNull))
        //                        sol.IdEstablecimientoDestinoCambio = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["IdEstablecimientoDestinoCambio"]));
        //                    if (!(row["IdClienteDestinatarioCambio"] is System.DBNull))
        //                        sol.ClienteDestinatarioCambio = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteDestinatarioCambio"]));


        //                    if (!(row["CodigoAnulacionAfip"] is System.DBNull))
        //                        sol.CodigoAnulacionAfip = Convert.ToInt64(row["CodigoAnulacionAfip"]);
        //                    if (!(row["FechaAnulacionAfip"] is System.DBNull))
        //                        sol.FechaAnulacionAfip = Convert.ToDateTime(row["FechaAnulacionAfip"]);


        //                    if (!(row["FechaCreacion"] is System.DBNull))
        //                        sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
        //                    sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();

        //                    if (!(row["FechaModificacion"] is System.DBNull))
        //                        sol.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);
        //                    sol.UsuarioModificacion = row["UsuarioModificacion"].ToString();

        //                }

        //                result.Add(sol);
        //            }

        //            if (result.Count > 0)
        //                return result.First();
        //            else
        //                return null;
        //        }
        //        else
        //        {
        //            return null;
        //        }

        //    }
        //    catch (System.Exception ex)
        //    {
        //        throw ExceptionFactory.CreateBusiness(ex, "ERROR Get GetSolicitud: " + ex.Message.ToString());

        //    }
        //    finally
        //    {
        //        conn1.Close();
        //    }

        //}
        //public IList<Solicitud> GetSolicitudAnulacionSAP()
        //{
        //    SqlConnection conn1 = null;
        //    try
        //    {
        //        string sql = string.Empty;
        //        conn1 = new SqlConnection(connString);

        //        DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSolicitudAnulacionSAP", App.Usuario.IdEmpresa);
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            IList<Solicitud> result = new List<Solicitud>();
        //            foreach (DataRow row in ds.Tables[0].Rows)
        //            {
        //                Solicitud sol = new Solicitud();
        //                sol.IdSolicitud = Convert.ToInt32(row["IdSolicitud"]);
        //                sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
        //                sol.Cee = row["Cee"].ToString();
        //                sol.Ctg = row["Ctg"].ToString();
        //                sol.TipoDeCarta = TipoDeCartaDAO.Instance.GetOne(Convert.ToInt32(row["IdTipoDeCarta"]));
        //                sol.ProveedorTitularCartaDePorte = ProveedorDAO.Instance.GetOne(Convert.ToInt32(row["IdProveedorTitularCartaDePorte"]));

        //                sol.ObservacionAfip = row["ObservacionAfip"].ToString();
        //                if (!(row["FechaDeEmision"] is System.DBNull))
        //                    sol.FechaDeEmision = Convert.ToDateTime(row["FechaDeEmision"]);
        //                if (!(row["FechaDeCarga"] is System.DBNull))
        //                    sol.FechaDeCarga = Convert.ToDateTime(row["FechaDeCarga"]);
        //                if (!(row["FechaDeVencimiento"] is System.DBNull))
        //                    sol.FechaDeVencimiento = Convert.ToDateTime(row["FechaDeVencimiento"]);
        //                sol.ClienteIntermediario = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteIntermediario"]));
        //                sol.ClienteRemitenteComercial = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteRemitenteComercial"]));
        //                sol.RemitenteComercialComoCanjeador = Convert.ToBoolean(row["RemitenteComercialComoCanjeador"]);
        //                sol.ClienteCorredor = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteCorredor"]));
        //                sol.ClienteEntregador = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteEntregador"]));
        //                sol.ClienteDestinatario = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteDestinatario"]));
        //                sol.ClienteDestino = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteDestino"]));
        //                sol.ProveedorTransportista = ProveedorDAO.Instance.GetOne(Convert.ToInt32(row["IdProveedorTransportista"]));
        //                sol.ChoferTransportista = ChoferDAO.Instance.GetOne(Convert.ToInt32(row["IdChoferTransportista"]));

        //                sol.Chofer = ChoferDAO.Instance.GetOne(Convert.ToInt32(row["IdChofer"]));
        //                sol.Grano = GranoDAO.Instance.GetOne(Convert.ToInt32(row["IdGrano"]));
        //                if (!(row["NumeroContrato"] is System.DBNull))
        //                    sol.NumeroContrato = Convert.ToInt32(row["NumeroContrato"]);
        //                if (!(row["SapContrato"] is System.DBNull))
        //                    sol.SapContrato = Convert.ToInt32(row["SapContrato"]);

        //                if (!(row["NumeroContrato"] is System.DBNull))
        //                    sol.SinContrato = true;
        //                else
        //                    sol.SinContrato = false;

        //                sol.CargaPesadaDestino = Convert.ToBoolean(row["CargaPesadaDestino"]);

        //                sol.ConformeCondicional = (Enums.ConformeCondicional)Convert.ToInt32(row["IdConformeCondicional"]);

        //                if (sol.CargaPesadaDestino)
        //                {
        //                    sol.KilogramosEstimados = (long)Convert.ToDecimal(row["KilogramosEstimados"]);
        //                }
        //                else
        //                {
        //                    sol.PesoBruto = (long)Convert.ToDecimal(row["PesoBruto"]);
        //                    sol.PesoTara = (long)Convert.ToDecimal(row["PesoTara"]);
        //                    sol.PesoNeto = (sol.PesoBruto - sol.PesoTara);
        //                }

        //                sol.Observaciones = row["Observaciones"].ToString();
        //                sol.LoteDeMaterial = row["LoteDeMaterial"].ToString();

        //                sol.IdEstablecimientoProcedencia = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["IdEstablecimientoProcedencia"]));
        //                sol.IdEstablecimientoDestino = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["IdEstablecimientoDestino"]));
        //                sol.PatenteCamion = row["PatenteCamion"].ToString();
        //                sol.PatenteAcoplado = row["PatenteAcoplado"].ToString();
        //                sol.KmRecorridos = (long)Convert.ToDecimal(row["KmRecorridos"]);
        //                sol.EstadoFlete = (Enums.EstadoFlete)Convert.ToInt32(row["EstadoFlete"]);
        //                sol.CantHoras = (long)Convert.ToDecimal(row["CantHoras"]);
        //                sol.TarifaReferencia = Convert.ToDecimal(row["TarifaReferencia"]);
        //                sol.TarifaReal = Convert.ToDecimal(row["TarifaReal"]);
        //                sol.ClientePagadorDelFlete = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClientePagadorDelFlete"]));
        //                sol.EstadoEnSAP = (Enums.EstadoEnvioSAP)Convert.ToInt32(row["EstadoEnSAP"]);
        //                sol.EstadoEnAFIP = (Enums.EstadoEnAFIP)Convert.ToInt32(row["EstadoEnAFIP"]);
        //                sol.CodigoRespuestaEnvioSAP = row["CodigoRespuestaEnvioSAP"].ToString();
        //                sol.CodigoRespuestaAnulacionSAP = row["CodigoRespuestaAnulacionSAP"].ToString();
        //                sol.MensajeRespuestaAnulacionSAP = row["MensajeRespuestaAnulacionSAP"].ToString();
        //                sol.MensajeRespuestaEnvioSAP = row["MensajeRespuestaEnvioSAP"].ToString();

        //                if (!(row["IdEstablecimientoDestinoCambio"] is System.DBNull))
        //                    sol.IdEstablecimientoDestinoCambio = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["IdEstablecimientoDestinoCambio"]));
        //                if (!(row["IdClienteDestinatarioCambio"] is System.DBNull))
        //                    sol.ClienteDestinatarioCambio = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteDestinatarioCambio"]));


        //                if (!(row["CodigoAnulacionAfip"] is System.DBNull))
        //                    sol.CodigoAnulacionAfip = Convert.ToInt64(row["CodigoAnulacionAfip"]);
        //                if (!(row["FechaAnulacionAfip"] is System.DBNull))
        //                    sol.FechaAnulacionAfip = Convert.ToDateTime(row["FechaAnulacionAfip"]);


        //                if (!(row["FechaCreacion"] is System.DBNull))
        //                    sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
        //                sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();


        //                if (!(row["FechaModificacion"] is System.DBNull))
        //                    sol.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);
        //                sol.UsuarioModificacion = row["UsuarioModificacion"].ToString();



        //                result.Add(sol);
        //            }

        //            if (result.Count > 0)
        //                return result;
        //            else
        //                return null;
        //        }
        //        else
        //        {
        //            return null;
        //        }

        //    }
        //    catch (System.Exception ex)
        //    {
        //        throw ExceptionFactory.CreateBusiness(ex, "ERROR Get GetSolicitud: " + ex.Message.ToString());

        //    }
        //    finally
        //    {
        //        conn1.Close();
        //    }

        //}
        //public IList<Solicitud> GetSolicitudesEnvioSAP()
        //{
        //    SqlConnection conn1 = null;
        //    try
        //    {
        //        string sql = string.Empty;
        //        conn1 = new SqlConnection(connString);

        //        DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSolicitudEnvioSAP", App.Usuario.IdEmpresa);
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            IList<Solicitud> result = new List<Solicitud>();
        //            foreach (DataRow row in ds.Tables[0].Rows)
        //            {
        //                Solicitud sol = new Solicitud();
        //                sol.IdSolicitud = Convert.ToInt32(row["IdSolicitud"]);
        //                sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
        //                sol.Cee = row["Cee"].ToString();
        //                sol.Ctg = row["Ctg"].ToString();
        //                sol.TipoDeCarta = TipoDeCartaDAO.Instance.GetOne(Convert.ToInt32(row["IdTipoDeCarta"]));
        //                sol.ProveedorTitularCartaDePorte = ProveedorDAO.Instance.GetOne(Convert.ToInt32(row["IdProveedorTitularCartaDePorte"]));

        //                sol.ObservacionAfip = row["ObservacionAfip"].ToString();
        //                if (!(row["FechaDeEmision"] is System.DBNull))
        //                    sol.FechaDeEmision = Convert.ToDateTime(row["FechaDeEmision"]);
        //                if (!(row["FechaDeCarga"] is System.DBNull))
        //                    sol.FechaDeCarga = Convert.ToDateTime(row["FechaDeCarga"]);
        //                if (!(row["FechaDeVencimiento"] is System.DBNull))
        //                    sol.FechaDeVencimiento = Convert.ToDateTime(row["FechaDeVencimiento"]);
        //                sol.ClienteIntermediario = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteIntermediario"]));
        //                sol.ClienteRemitenteComercial = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteRemitenteComercial"]));
        //                sol.RemitenteComercialComoCanjeador = Convert.ToBoolean(row["RemitenteComercialComoCanjeador"]);
        //                sol.ClienteCorredor = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteCorredor"]));
        //                sol.ClienteEntregador = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteEntregador"]));
        //                sol.ClienteDestinatario = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteDestinatario"]));
        //                sol.ClienteDestino = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteDestino"]));
        //                sol.ProveedorTransportista = ProveedorDAO.Instance.GetOne(Convert.ToInt32(row["IdProveedorTransportista"]));
        //                sol.ChoferTransportista = ChoferDAO.Instance.GetOne(Convert.ToInt32(row["IdChoferTransportista"]));

        //                sol.Chofer = ChoferDAO.Instance.GetOne(Convert.ToInt32(row["IdChofer"]));
        //                sol.Grano = GranoDAO.Instance.GetOne(Convert.ToInt32(row["IdGrano"]));
        //                if (!(row["NumeroContrato"] is System.DBNull))
        //                    sol.NumeroContrato = Convert.ToInt32(row["NumeroContrato"]);
        //                if (!(row["SapContrato"] is System.DBNull))
        //                    sol.SapContrato = Convert.ToInt32(row["SapContrato"]);

        //                if (!(row["NumeroContrato"] is System.DBNull))
        //                    sol.SinContrato = true;
        //                else
        //                    sol.SinContrato = false;

        //                sol.CargaPesadaDestino = Convert.ToBoolean(row["CargaPesadaDestino"]);

        //                sol.ConformeCondicional = (Enums.ConformeCondicional)Convert.ToInt32(row["IdConformeCondicional"]);

        //                if (sol.CargaPesadaDestino)
        //                {
        //                    sol.KilogramosEstimados = (long)Convert.ToDecimal(row["KilogramosEstimados"]);
        //                }
        //                else
        //                {

        //                    sol.PesoBruto = (long)Convert.ToDecimal(row["PesoBruto"]);
        //                    sol.PesoTara = (long)Convert.ToDecimal(row["PesoTara"]);
        //                    sol.PesoNeto = (sol.PesoBruto - sol.PesoTara);
        //                }


        //                sol.Observaciones = row["Observaciones"].ToString();
        //                sol.LoteDeMaterial = row["LoteDeMaterial"].ToString();

        //                sol.IdEstablecimientoProcedencia = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["IdEstablecimientoProcedencia"]));
        //                sol.IdEstablecimientoDestino = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["IdEstablecimientoDestino"]));
        //                sol.PatenteCamion = row["PatenteCamion"].ToString();
        //                sol.PatenteAcoplado = row["PatenteAcoplado"].ToString();
        //                sol.KmRecorridos = (long)Convert.ToDecimal(row["KmRecorridos"]);
        //                sol.EstadoFlete = (Enums.EstadoFlete)Convert.ToInt32(row["EstadoFlete"]);
        //                sol.CantHoras = (long)Convert.ToDecimal(row["CantHoras"]);
        //                sol.TarifaReferencia = Convert.ToDecimal(row["TarifaReferencia"]);
        //                sol.TarifaReal = Convert.ToDecimal(row["TarifaReal"]);
        //                sol.ClientePagadorDelFlete = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClientePagadorDelFlete"]));
        //                sol.EstadoEnSAP = (Enums.EstadoEnvioSAP)Convert.ToInt32(row["EstadoEnSAP"]);
        //                sol.EstadoEnAFIP = (Enums.EstadoEnAFIP)Convert.ToInt32(row["EstadoEnAFIP"]);
        //                sol.CodigoRespuestaEnvioSAP = row["CodigoRespuestaEnvioSAP"].ToString();
        //                sol.CodigoRespuestaAnulacionSAP = row["CodigoRespuestaAnulacionSAP"].ToString();
        //                sol.MensajeRespuestaAnulacionSAP = row["MensajeRespuestaAnulacionSAP"].ToString();
        //                sol.MensajeRespuestaEnvioSAP = row["MensajeRespuestaEnvioSAP"].ToString();

        //                if (!(row["IdEstablecimientoDestinoCambio"] is System.DBNull))
        //                    sol.IdEstablecimientoDestinoCambio = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["IdEstablecimientoDestinoCambio"]));
        //                if (!(row["IdClienteDestinatarioCambio"] is System.DBNull))
        //                    sol.ClienteDestinatarioCambio = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteDestinatarioCambio"]));


        //                if (!(row["CodigoAnulacionAfip"] is System.DBNull))
        //                    sol.CodigoAnulacionAfip = Convert.ToInt64(row["CodigoAnulacionAfip"]);
        //                if (!(row["FechaAnulacionAfip"] is System.DBNull))
        //                    sol.FechaAnulacionAfip = Convert.ToDateTime(row["FechaAnulacionAfip"]);


        //                if (!(row["FechaCreacion"] is System.DBNull))
        //                    sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
        //                sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();


        //                if (!(row["FechaModificacion"] is System.DBNull))
        //                    sol.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);
        //                sol.UsuarioModificacion = row["UsuarioModificacion"].ToString();


        //                result.Add(sol);
        //            }

        //            if (result.Count > 0)
        //                return result;
        //            else
        //                return null;
        //        }
        //        else
        //        {
        //            return null;
        //        }

        //    }
        //    catch (System.Exception ex)
        //    {
        //        throw ExceptionFactory.CreateBusiness(ex, "ERROR Get GetSolicitud: " + ex.Message.ToString());

        //    }
        //    finally
        //    {
        //        conn1.Close();
        //    }

        //}



        //public IList<Solicitud> GetMisReservas(string Usuario)
        //{
        //    SqlConnection conn1 = null;
        //    try
        //    {
        //        string sql = string.Empty;
        //        conn1 = new SqlConnection(connString);

        //        DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetMisReservas", Usuario, this.GetIdGrupoEmpresa(), App.Usuario.IdEmpresa);
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            IList<Solicitud> result = new List<Solicitud>();
        //            foreach (DataRow row in ds.Tables[0].Rows)
        //            {
        //                Solicitud sol = new Solicitud();
        //                sol.IdSolicitud = Convert.ToInt32(row["IdSolicitud"]);
        //                sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
        //                sol.Cee = row["Cee"].ToString();
        //                sol.ObservacionAfip = row["ObservacionAfip"].ToString();

        //                if (!(row["FechaCreacion"] is System.DBNull))
        //                    sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
        //                sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();

        //                result.Add(sol);
        //            }

        //            return result;
        //        }
        //        else
        //        {
        //            return new List<Solicitud>();
        //        }

        //    }
        //    catch (System.Exception ex)
        //    {
        //        throw ExceptionFactory.CreateBusiness(ex, "ERROR GetAll MisReservas: " + ex.Message.ToString());

        //    }
        //    finally
        //    {
        //        conn1.Close();
        //    }

        //}

        //public void ConsultaDeEstadosAFIP()
        //{


        //    ClienteDAO.Instance.SaveLogInterfaz("ConsultaDeEstadosAFIP()");

        //    var ws = new wsAfip_v3();
            
            
        //    var resul = ws.consultarCTG(DateTime.Now.AddDays(-7));
            

        //    if (resul.arrayDatosConsultarCTG != null)
        //    {
        //        foreach (var dato in resul.arrayDatosConsultarCTG)
        //        {
        //            if (dato != null)
        //            {
        //                String nroCTG = Convert.ToInt64(dato.ctg.Replace(".", string.Empty)).ToString();

        //                if (!String.IsNullOrEmpty(nroCTG))
        //                {
        //                    Solicitud sol = GetSolicitudByCTG(nroCTG);

        //                    ClienteDAO.Instance.SaveLogInterfaz("Solicitud: " + sol.NumeroCartaDePorte + " - EstadoEnAFIP: " + sol.EstadoEnAFIP.ToString());
        //                    if (sol != null)
        //                    {
        //                        if (sol.EstadoEnAFIP == Enums.EstadoEnAFIP.VueltaOrigen)
        //                        { }
        //                        else if (sol.EstadoEnAFIP == Enums.EstadoEnAFIP.CambioDestino)
        //                        { }
        //                        else if (sol.EstadoEnAFIP == Enums.EstadoEnAFIP.Anulada)
        //                        { }
        //                        else
        //                        {
        //                            switch (dato.estado)
        //                            {
        //                                case "Otorgado":
        //                                    sol.EstadoEnAFIP = Enums.EstadoEnAFIP.Otorgado;
        //                                    break;
        //                                case "Anulado":
        //                                    sol.EstadoEnAFIP = Enums.EstadoEnAFIP.Anulada;
        //                                    break;
        //                                case "Confirmado":
        //                                    sol.ObservacionAfip = "CTG Confirmado";
        //                                    sol.EstadoEnAFIP = Enums.EstadoEnAFIP.Confirmado;
        //                                    break;
        //                                case "Confirmación Definitiva":
        //                                    sol.ObservacionAfip = "CTG Confirmación Definitiva";
        //                                    sol.EstadoEnAFIP = Enums.EstadoEnAFIP.ConfirmadoDefinitivo;
        //                                    break;
        //                                case "Rechazado":
        //                                    sol.ObservacionAfip = "CTG Rechazado";
        //                                    sol.EstadoEnAFIP = Enums.EstadoEnAFIP.Rechazado;
        //                                    break;

        //                                default:
        //                                    break;
        //                            }

        //                            SaveOrUpdate(sol);

        //                        }

        //                    }

        //                }
        //            }
        //        }

        //    }

        //}

        //public DataTable GetAllReporte(DateTime FD, DateTime FH)
        //{

        //    SqlConnection conn1 = null;
        //    try
        //    {
        //        string sql = string.Empty;
        //        conn1 = new SqlConnection(connString);

        //        DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSolicitudReporte", FD, FH, App.Usuario.IdEmpresa);
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            IList<SolicitudReporte> result = new List<SolicitudReporte>();
        //            foreach (DataRow row in ds.Tables[0].Rows)
        //            {
        //                SolicitudReporte sol = new SolicitudReporte();
        //                if (row["ObservacionAfip"].ToString().Contains("Reserva"))
        //                {
        //                    sol.IdSolicitud = row["IdSolicitud"].ToString();
        //                    sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
        //                    sol.Cee = row["Cee"].ToString();
        //                    sol.ObservacionAfip = row["ObservacionAfip"].ToString();

        //                    if (!(row["IdEstablecimientoProcedencia"] is System.DBNull))
        //                        sol.EstablecimientoProcedencia = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["IdEstablecimientoProcedencia"])).ToString();

        //                    if (!(row["FechaCreacion"] is System.DBNull))
        //                        sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]).ToString("dd/MM/yyyy HH:mm");
        //                    sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();

        //                }
        //                else
        //                {
        //                    sol.IdSolicitud = row["IdSolicitud"].ToString();
        //                    sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
        //                    sol.Cee = row["Cee"].ToString();
        //                    sol.Ctg = row["Ctg"].ToString();
        //                    sol.TipoDeCarta = TipoDeCartaDAO.Instance.GetOne(Convert.ToInt32(row["IdTipoDeCarta"])).ToString();
        //                    sol.ProveedorTitularCartaDePorte = ProveedorDAO.Instance.GetOne(Convert.ToInt32(row["IdProveedorTitularCartaDePorte"])).ToString();

        //                    sol.ObservacionAfip = row["ObservacionAfip"].ToString();
        //                    if (!(row["FechaDeEmision"] is System.DBNull))
        //                        sol.FechaDeEmision = Convert.ToDateTime(row["FechaDeEmision"]).ToString("dd/MM/yyyy HH:mm");
        //                    if (!(row["FechaDeCarga"] is System.DBNull))
        //                        sol.FechaDeCarga = Convert.ToDateTime(row["FechaDeCarga"]).ToString("dd/MM/yyyy HH:mm");
        //                    if (!(row["FechaDeVencimiento"] is System.DBNull))
        //                        sol.FechaDeVencimiento = Convert.ToDateTime(row["FechaDeVencimiento"]).ToString("dd/MM/yyyy HH:mm");
        //                    sol.ClienteIntermediario = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteIntermediario"])).ToString();
        //                    sol.ClienteRemitenteComercial = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteRemitenteComercial"])).ToString();
        //                    sol.RemitenteComercialComoCanjeador = (Convert.ToBoolean(row["RemitenteComercialComoCanjeador"]) ? "Si" : "No");
        //                    sol.ClienteCorredor = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteCorredor"])).ToString();
        //                    sol.ClienteEntregador = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteEntregador"])).ToString();
        //                    sol.ClienteDestinatario = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteDestinatario"])).ToString();

        //                    sol.ClienteDestino = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteDestino"])).ToString();

        //                    Cliente cliPagadorFlete = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClientePagadorDelFlete"]));
        //                    if (cliPagadorFlete != null && cliPagadorFlete.EsEmpresa())
        //                    {
        //                        sol.ProveedorTransportista = ProveedorDAO.Instance.GetOne(Convert.ToInt32(row["IdProveedorTransportista"])).ToString();
        //                        sol.ChoferTransportista = string.Empty;
        //                    }
        //                    else
        //                    {
        //                        sol.ProveedorTransportista = string.Empty;
        //                        sol.ChoferTransportista = ChoferDAO.Instance.GetOne(Convert.ToInt32(row["IdChoferTransportista"])).ToString();
        //                    }


        //                    sol.Chofer = ChoferDAO.Instance.GetOne(Convert.ToInt32(row["IdChofer"])).ToString();
        //                    sol.Grano = GranoDAO.Instance.GetOne(Convert.ToInt32(row["IdGrano"])).ToString();
        //                    if (!(row["NumeroContrato"] is System.DBNull))
        //                        sol.NumeroContrato = Convert.ToInt32(row["NumeroContrato"]).ToString();

        //                    sol.CargaPesadaDestino = (Convert.ToBoolean(row["CargaPesadaDestino"]) ? "Si" : "No");
        //                    sol.ConformeCondicional = ((Enums.ConformeCondicional)Convert.ToInt32(row["IdConformeCondicional"])).ToString();
        //                    sol.KilogramosEstimados = row["KilogramosEstimados"].ToString();
        //                    sol.PesoBruto = row["PesoBruto"].ToString();
        //                    sol.PesoTara = row["PesoTara"].ToString();

        //                    sol.Observaciones = row["Observaciones"].ToString();

        //                    sol.EstablecimientoProcedencia = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["IdEstablecimientoProcedencia"])).ToString();
        //                    sol.EstablecimientoDestino = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["IdEstablecimientoDestino"])).ToString();

        //                    sol.PatenteCamion = row["PatenteCamion"].ToString();
        //                    sol.PatenteAcoplado = row["PatenteAcoplado"].ToString();
        //                    sol.KmRecorridos = row["KmRecorridos"].ToString();
        //                    sol.EstadoFlete = ((Enums.EstadoFlete)Convert.ToInt32(row["EstadoFlete"])).ToString();
        //                    sol.CantHoras = row["CantHoras"].ToString();
        //                    sol.TarifaReferencia = row["TarifaReferencia"].ToString();
        //                    sol.TarifaReal = row["TarifaReal"].ToString();

        //                    sol.ClientePagadorDelFlete = cliPagadorFlete.ToString();
        //                    sol.EstadoEnSAP = ((Enums.EstadoEnvioSAP)Convert.ToInt32(row["EstadoEnSAP"])).ToString();
        //                    sol.EstadoEnAFIP = ((Enums.EstadoEnAFIP)Convert.ToInt32(row["EstadoEnAFIP"])).ToString();
        //                    sol.CodigoRespuestaEnvioSAP = row["CodigoRespuestaEnvioSAP"].ToString();
        //                    sol.CodigoRespuestaAnulacionSAP = row["CodigoRespuestaAnulacionSAP"].ToString();
        //                    //sol.MensajeRespuestaAnulacionSAP = row["MensajeRespuestaAnulacionSAP"].ToString();
        //                    //sol.MensajeRespuestaEnvioSAP = row["MensajeRespuestaEnvioSAP"].ToString();

        //                    if (!(row["IdEstablecimientoDestinoCambio"] is System.DBNull))
        //                        sol.EstablecimientoDestinoCambio = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["IdEstablecimientoDestinoCambio"])).ToString();
        //                    if (!(row["IdClienteDestinatarioCambio"] is System.DBNull))
        //                        sol.ClienteDestinatarioCambio = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteDestinatarioCambio"])).ToString();


        //                    if (!(row["CodigoAnulacionAfip"] is System.DBNull))
        //                        sol.CodigoAnulacionAfip = row["CodigoAnulacionAfip"].ToString();
        //                    if (!(row["FechaAnulacionAfip"] is System.DBNull))
        //                        sol.FechaAnulacionAfip = Convert.ToDateTime(row["FechaAnulacionAfip"]).ToString("dd/MM/yyyy HH:mm");

        //                    sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();

        //                    if (!(row["FechaCreacion"] is System.DBNull))
        //                        sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]).ToString("dd/MM/yyyy HH:mm");
        //                    sol.UsuarioModificacion = row["UsuarioModificacion"].ToString();

        //                    if (!(row["FechaModificacion"] is System.DBNull))
        //                        sol.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]).ToString("dd/MM/yyyy HH:mm");

        //                }

        //                result.Add(sol);
        //            }

        //            if (result.Count > 0)
        //                return GetDataTableFromIListGeneric(result).Tables[0];
        //            else
        //                return new DataTable();


        //        }

        //    }
        //    catch (System.Exception ex)
        //    {
        //        throw ExceptionFactory.CreateBusiness(ex, "ERROR GetAllReporte Solicitud: " + ex.Message.ToString());

        //    }
        //    finally
        //    {
        //        conn1.Close();
        //    }

        //    IList<SolicitudReporte> listaVacia = new List<SolicitudReporte>();
        //    listaVacia.Add(new SolicitudReporte());

        //    return GetDataTableFromIListGeneric(listaVacia).Tables[0];

        //}
        //public IList<Solicitud> GetAllReporteEmitidas(DateTime FD, DateTime FH)
        //{

        //    SqlConnection conn1 = null;
        //    try
        //    {
        //        string sql = string.Empty;
        //        conn1 = new SqlConnection(connString);

        //        DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSolicitudReporteEmitidas", FD, FH, App.Usuario.IdEmpresa);
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {

        //            IList<Solicitud> result = new List<Solicitud>();
        //            foreach (DataRow row in ds.Tables[0].Rows)
        //            {
        //                Solicitud sol = new Solicitud();
        //                if (row["ObservacionAfip"].ToString().Contains("Reserva"))
        //                {
        //                    sol.IdSolicitud = Convert.ToInt32(row["IdSolicitud"]);
        //                    sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
        //                    sol.Cee = row["Cee"].ToString();
        //                    sol.ObservacionAfip = row["ObservacionAfip"].ToString();

        //                    if (!(row["IdEstablecimientoProcedencia"] is System.DBNull))
        //                        sol.IdEstablecimientoProcedencia = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["IdEstablecimientoProcedencia"]));

        //                    if (!(row["FechaCreacion"] is System.DBNull))
        //                        sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
        //                    sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();

        //                }
        //                else
        //                {
        //                    sol.IdSolicitud = Convert.ToInt32(row["IdSolicitud"]);
        //                    sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
        //                    sol.Cee = row["Cee"].ToString();
        //                    sol.Ctg = row["Ctg"].ToString();
        //                    sol.TipoDeCarta = TipoDeCartaDAO.Instance.GetOne(Convert.ToInt32(row["IdTipoDeCarta"]));
        //                    sol.ProveedorTitularCartaDePorte = ProveedorDAO.Instance.GetOne(Convert.ToInt32(row["IdProveedorTitularCartaDePorte"]));

        //                    sol.ObservacionAfip = row["ObservacionAfip"].ToString();
        //                    if (!(row["FechaDeEmision"] is System.DBNull))
        //                        sol.FechaDeEmision = Convert.ToDateTime(row["FechaDeEmision"]);
        //                    if (!(row["FechaDeCarga"] is System.DBNull))
        //                        sol.FechaDeCarga = Convert.ToDateTime(row["FechaDeCarga"]);
        //                    if (!(row["FechaDeVencimiento"] is System.DBNull))
        //                        sol.FechaDeVencimiento = Convert.ToDateTime(row["FechaDeVencimiento"]);
        //                    sol.ClienteIntermediario = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteIntermediario"]));
        //                    sol.ClienteRemitenteComercial = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteRemitenteComercial"]));
        //                    sol.RemitenteComercialComoCanjeador = Convert.ToBoolean(row["RemitenteComercialComoCanjeador"]);
        //                    sol.ClienteCorredor = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteCorredor"]));
        //                    sol.ClienteEntregador = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteEntregador"]));
        //                    sol.ClienteDestinatario = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteDestinatario"]));

        //                    sol.ClienteDestino = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteDestino"]));
        //                    sol.ProveedorTransportista = ProveedorDAO.Instance.GetOne(Convert.ToInt32(row["IdProveedorTransportista"]));
        //                    sol.ChoferTransportista = ChoferDAO.Instance.GetOne(Convert.ToInt32(row["IdChoferTransportista"]));

        //                    sol.Chofer = ChoferDAO.Instance.GetOne(Convert.ToInt32(row["IdChofer"]));
        //                    sol.Grano = GranoDAO.Instance.GetOne(Convert.ToInt32(row["IdGrano"]));
        //                    if (!(row["NumeroContrato"] is System.DBNull))
        //                        sol.NumeroContrato = Convert.ToInt32(row["NumeroContrato"]);
        //                    if (!(row["SapContrato"] is System.DBNull))
        //                        sol.SapContrato = Convert.ToInt32(row["SapContrato"]);

        //                    if (!(row["NumeroContrato"] is System.DBNull))
        //                        sol.SinContrato = true;
        //                    else
        //                        sol.SinContrato = false;

        //                    sol.CargaPesadaDestino = Convert.ToBoolean(row["CargaPesadaDestino"]);

        //                    sol.ConformeCondicional = (Enums.ConformeCondicional)Convert.ToInt32(row["IdConformeCondicional"]);

        //                    if (sol.CargaPesadaDestino)
        //                    {
        //                        sol.KilogramosEstimados = (long)Convert.ToDecimal(row["KilogramosEstimados"]);
        //                    }
        //                    else
        //                    {
        //                        sol.PesoBruto = (long)Convert.ToDecimal(row["PesoBruto"]);
        //                        sol.PesoTara = (long)Convert.ToDecimal(row["PesoTara"]);
        //                        sol.PesoNeto = (sol.PesoBruto - sol.PesoTara);
        //                    }

        //                    sol.Observaciones = row["Observaciones"].ToString();
        //                    sol.LoteDeMaterial = row["LoteDeMaterial"].ToString();

        //                    sol.IdEstablecimientoProcedencia = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["IdEstablecimientoProcedencia"]));
        //                    sol.IdEstablecimientoDestino = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["IdEstablecimientoDestino"]));

        //                    sol.PatenteCamion = row["PatenteCamion"].ToString();
        //                    sol.PatenteAcoplado = row["PatenteAcoplado"].ToString();
        //                    sol.KmRecorridos = (long)Convert.ToDecimal(row["KmRecorridos"]);
        //                    sol.EstadoFlete = (Enums.EstadoFlete)Convert.ToInt32(row["EstadoFlete"]);
        //                    sol.CantHoras = (long)Convert.ToDecimal(row["CantHoras"]);
        //                    sol.TarifaReferencia = Convert.ToDecimal(row["TarifaReferencia"]);
        //                    sol.TarifaReal = Convert.ToDecimal(row["TarifaReal"]);
        //                    sol.ClientePagadorDelFlete = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClientePagadorDelFlete"]));
        //                    sol.EstadoEnSAP = (Enums.EstadoEnvioSAP)Convert.ToInt32(row["EstadoEnSAP"]);
        //                    sol.EstadoEnAFIP = (Enums.EstadoEnAFIP)Convert.ToInt32(row["EstadoEnAFIP"]);
        //                    sol.CodigoRespuestaEnvioSAP = row["CodigoRespuestaEnvioSAP"].ToString();
        //                    sol.CodigoRespuestaAnulacionSAP = row["CodigoRespuestaAnulacionSAP"].ToString();
        //                    sol.MensajeRespuestaAnulacionSAP = row["MensajeRespuestaAnulacionSAP"].ToString();
        //                    sol.MensajeRespuestaEnvioSAP = row["MensajeRespuestaEnvioSAP"].ToString();

        //                    if (!(row["IdEstablecimientoDestinoCambio"] is System.DBNull))
        //                        sol.IdEstablecimientoDestinoCambio = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["IdEstablecimientoDestinoCambio"]));
        //                    if (!(row["IdClienteDestinatarioCambio"] is System.DBNull))
        //                        sol.ClienteDestinatarioCambio = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdClienteDestinatarioCambio"]));


        //                    if (!(row["CodigoAnulacionAfip"] is System.DBNull))
        //                        sol.CodigoAnulacionAfip = Convert.ToInt64(row["CodigoAnulacionAfip"]);
        //                    if (!(row["FechaAnulacionAfip"] is System.DBNull))
        //                        sol.FechaAnulacionAfip = Convert.ToDateTime(row["FechaAnulacionAfip"]);


        //                    if (!(row["FechaCreacion"] is System.DBNull))
        //                        sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
        //                    sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();

        //                    if (!(row["FechaModificacion"] is System.DBNull))
        //                        sol.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);
        //                    sol.UsuarioModificacion = row["UsuarioModificacion"].ToString();

        //                }

        //                result.Add(sol);
        //            }

        //            if (result.Count > 0)
        //                return result;
        //            else
        //                return new List<Solicitud>();



        //        }

        //    }
        //    catch (System.Exception ex)
        //    {
        //        throw ExceptionFactory.CreateBusiness(ex, "ERROR GetAllReporte Solicitud: " + ex.Message.ToString());

        //    }
        //    finally
        //    {
        //        conn1.Close();
        //    }


        //    return new List<Solicitud>();
        //}

        //public DataTable GetReporteGrafico(DateTime FD, DateTime FH)
        //{
        //    IList<SolicitudGrafico> listaVacia = new List<SolicitudGrafico>();

        //    SqlConnection conn1 = null;
        //    try
        //    {
        //        string sql = string.Empty;
        //        conn1 = new SqlConnection(connString);

        //        DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSolicitudReporteGrafico", FD, FH, App.Usuario.IdEmpresa);
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            IList<SolicitudGrafico> result = new List<SolicitudGrafico>();
        //            foreach (DataRow row in ds.Tables[0].Rows)
        //            {
        //                SolicitudGrafico sol = new SolicitudGrafico();

        //                sol.Fecha = row["Fecha"].ToString();
        //                sol.CntAfip = Convert.ToInt32(row["CntAfip"]);
        //                sol.CntSap = Convert.ToInt32(row["CntSap"]);

        //                result.Add(sol);
        //            }

        //            if (result.Count > 0)
        //            {
        //                return GetDataTableFromIListGeneric(result).Tables[0];
        //            }
        //            else
        //            {
        //                listaVacia = new List<SolicitudGrafico>();
        //                listaVacia.Add(new SolicitudGrafico());

        //                return GetDataTableFromIListGeneric(listaVacia).Tables[0];
        //            }


        //        }

        //    }
        //    catch (System.Exception ex)
        //    {
        //        throw ExceptionFactory.CreateBusiness(ex, "ERROR GetReporteGrafico: " + ex.Message.ToString());

        //    }
        //    finally
        //    {
        //        conn1.Close();
        //    }

        //    listaVacia = new List<SolicitudGrafico>();
        //    listaVacia.Add(new SolicitudGrafico());

        //    return GetDataTableFromIListGeneric(listaVacia).Tables[0];

        //}

        //public DataTable GetReporteGraficoNotIn(DateTime FD, DateTime FH)
        //{
        //    IList<SolicitudGraficoNotIn> listaVacia = new List<SolicitudGraficoNotIn>();

        //    SqlConnection conn1 = null;
        //    try
        //    {
        //        string sql = string.Empty;
        //        conn1 = new SqlConnection(connString);

        //        DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSolicitudReporteGraficoNotIn", FD, FH, App.Usuario.IdEmpresa);
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            IList<SolicitudGraficoNotIn> result = new List<SolicitudGraficoNotIn>();
        //            foreach (DataRow row in ds.Tables[0].Rows)
        //            {
        //                SolicitudGraficoNotIn sol = new SolicitudGraficoNotIn();

        //                sol.Fecha = row["Fecha"].ToString();
        //                sol.Numerocartadeporte = row["Numerocartadeporte"].ToString();
        //                sol.CodigoAfip = row["CodigoAfip"].ToString();
        //                sol.CodigoSAP = row["CodigoSAP"].ToString();
        //                sol.EstadoEnAFIP = ((Enums.EstadoEnAFIP)Convert.ToInt32(row["EstadoEnAFIP"])).ToString();
        //                sol.EstadoEnSAP = ((Enums.EstadoEnvioSAP)Convert.ToInt32(row["EstadoEnSAP"])).ToString();

        //                result.Add(sol);
        //            }

        //            if (result.Count > 0)
        //            {
        //                return GetDataTableFromIListGeneric(result).Tables[0];
        //            }
        //            else
        //            {
        //                listaVacia = new List<SolicitudGraficoNotIn>();
        //                listaVacia.Add(new SolicitudGraficoNotIn());

        //                return GetDataTableFromIListGeneric(listaVacia).Tables[0];
        //            }


        //        }

        //    }
        //    catch (System.Exception ex)
        //    {
        //        throw ExceptionFactory.CreateBusiness(ex, "ERROR GetReporteGrafico: " + ex.Message.ToString());

        //    }
        //    finally
        //    {
        //        conn1.Close();
        //    }

        //    listaVacia = new List<SolicitudGraficoNotIn>();
        //    listaVacia.Add(new SolicitudGraficoNotIn());

        //    return GetDataTableFromIListGeneric(listaVacia).Tables[0];

        //}


        //public DataTable GetAllReporte1116ADataTable(DateTime FD, DateTime FH, int modo)
        //{
        //    IList<SolicitudReporte1116A> resul = GetAllReporte1116A(FD, FH, modo);

        //    if (resul.Count > 0)
        //    {
        //        return GetDataTableFromIListGeneric(resul).Tables[0];

        //    }
        //    else
        //    {
        //        IList<SolicitudReporte1116A> listaVacia = new List<SolicitudReporte1116A>();
        //        listaVacia.Add(new SolicitudReporte1116A());
        //        return GetDataTableFromIListGeneric(listaVacia).Tables[0];
        //    }

        //}

        //public IList<SolicitudReporte1116A> GetAllReporte1116A(DateTime FD, DateTime FH, int modo)
        //{

        //    SqlConnection conn1 = null;
        //    try
        //    {
        //        string sql = string.Empty;
        //        conn1 = new SqlConnection(connString);

        //        DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSolicitudReporte1116A", FD, FH, modo, App.Usuario.IdEmpresa);
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {

        //            IList<SolicitudReporte1116A> result = new List<SolicitudReporte1116A>();
        //            foreach (DataRow row in ds.Tables[0].Rows)
        //            {
        //                SolicitudReporte1116A sol = new SolicitudReporte1116A();
        //                sol.NroCartaDePorte = row["NumeroCartaDePorte"].ToString();
        //                sol.NroCEE = row["Cee"].ToString();
        //                sol.NroCTG = row["Ctg"].ToString();
        //                sol.TitularCartaDePorte = ProveedorDAO.Instance.GetOne(Convert.ToInt32(row["IdProveedorTitularCartaDePorte"])).Nombre;

        //                if (!(row["FechaDeEmision"] is System.DBNull))
        //                    sol.FechaDeEmision = Convert.ToDateTime(row["FechaDeEmision"]).ToString("dd/MM/yyyy");

        //                sol.Numero1116A = row["Numero1116A"].ToString();
        //                if (!(row["Fecha1116A"] is System.DBNull))
        //                    sol.Fecha1116A = Convert.ToDateTime(row["Fecha1116A"]).ToString("dd/MM/yyyy");

        //                result.Add(sol);
        //            }

        //            if (result.Count > 0)
        //                return result;
        //            else
        //                return new List<SolicitudReporte1116A>();

        //        }


        //    }
        //    catch (System.Exception ex)
        //    {
        //        throw ExceptionFactory.CreateBusiness(ex, "ERROR GetAllReporte1116A: " + ex.Message.ToString());

        //    }
        //    finally
        //    {
        //        conn1.Close();
        //    }

        //    IList<SolicitudReporte1116A> listavacia = new List<SolicitudReporte1116A>();
        //    listavacia.Add(new SolicitudReporte1116A());
        //    return new List<SolicitudReporte1116A>();

        //}




        //private static DataSet GetDataTableFromIListGeneric<T>(IList<T> aIList)
        //{
        //    DataTable dTable = new DataTable();
        //    object baseObj = aIList[0];
        //    Type objectType = baseObj.GetType();
        //    PropertyInfo[] properties = objectType.GetProperties();

        //    DataColumn col;
        //    foreach (PropertyInfo property in properties)
        //    {
        //        if ((string)property.Name != "EmpresaNoDataSet")
        //        {
        //            col = new DataColumn();
        //            col.ColumnName = (string)property.Name;
        //            //col.DataType = property.PropertyType;
        //            dTable.Columns.Add(col);
        //        }
        //    }
        //    //Adds the rows to the table
        //    DataRow row;
        //    foreach (object objItem in aIList)
        //    {
        //        row = dTable.NewRow();

        //        foreach (PropertyInfo property in properties)
        //        {
        //            if (property.Name != "EmpresaNoDataSet")
        //            {
        //                row[property.Name] = property.GetValue(objItem, null);
        //            }
        //        }
        //        dTable.Rows.Add(row);
        //    }

        //    DataSet ds = new DataSet("Resultado");
        //    ds.Tables.Add(dTable);
        //    return ds;

        //}


    }



    public class SolicitudMe : Solicitud
    { 
        public SolicitudMe(){ }

        public int IdEmpresa { get; set;}

    }

}

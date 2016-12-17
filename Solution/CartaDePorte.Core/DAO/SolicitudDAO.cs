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
    public class SolicitudDAO : BaseDAO
    {

        private static SolicitudDAO instance;
        private static string mensaje = string.Empty;
        public SolicitudDAO() { }

        public static SolicitudDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SolicitudDAO();
                }
                return instance;
            }
        }


        public int SaveOrUpdateProspecto(int TransportistaId)
        {
            SqlConnection conn1 = null;
            Object resul = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                resul = SqlHelper.ExecuteScalar(conn1, "GuardarSolicitudProveedorProspecto", TransportistaId.ToString());

                return ConvertToInt32(resul);
            }
            catch (System.Exception ex)
            {
                return 0;
            }
            finally
            {
                conn1.Close();
            }
        }

        public int SaveOrUpdate(Solicitud solicitud)
        {
            Object resul = null;
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                int? especieAfipCodigo = -1;
                string cosechaAfipCodigo = null;
                if (solicitud.Grano != null)
                {
                    if (solicitud.Grano.EspecieAfip != null)
                    {
                        especieAfipCodigo = solicitud.Grano.EspecieAfip.Codigo;
                    }
                    if (solicitud.Grano.CosechaAfip != null)
                    {
                        cosechaAfipCodigo = solicitud.Grano.CosechaAfip.Codigo;
                    }
                }

                if (solicitud.IdSolicitud > 0)
                {
                    resul = SqlHelper.ExecuteScalar(conn1, "GuardarSolicitud",
                        solicitud.IdSolicitud,
                        solicitud.TipoDeCarta.IdTipoDeCarta,
                        solicitud.ObservacionAfip,
                        solicitud.NumeroCartaDePorte,
                        solicitud.Cee,
                        solicitud.Ctg.Replace(".", ""),
                        solicitud.FechaDeEmision,
                        solicitud.FechaDeCarga,
                        solicitud.FechaDeVencimiento,
                        (solicitud.ProveedorTitularCartaDePorte != null) ? solicitud.ProveedorTitularCartaDePorte.IdProveedor : 0,
                        (solicitud.ClienteIntermediario != null) ? solicitud.ClienteIntermediario.IdCliente : 0,
                        (solicitud.ClienteRemitenteComercial != null) ? solicitud.ClienteRemitenteComercial.IdCliente : 0,
                        solicitud.RemitenteComercialComoCanjeador,
                        (solicitud.ClienteCorredor != null) ? solicitud.ClienteCorredor.IdCliente : 0,
                        (solicitud.ClienteEntregador != null) ? solicitud.ClienteEntregador.IdCliente : 0,
                        (solicitud.ClienteDestinatario != null) ? solicitud.ClienteDestinatario.IdCliente : 0,
                        (solicitud.ClienteDestino != null) ? solicitud.ClienteDestino.IdCliente : 0,
                        (solicitud.ProveedorTransportista != null) ? solicitud.ProveedorTransportista.IdProveedor : 0,
                        (solicitud.ChoferTransportista != null) ? solicitud.ChoferTransportista.IdChofer : 0,
                        (solicitud.Chofer != null) ? solicitud.Chofer.IdChofer : 0,
                        cosechaAfipCodigo,
                        especieAfipCodigo,
                        solicitud.NumeroContrato,
                        solicitud.SapContrato,
                        solicitud.SinContrato,
                        solicitud.CargaPesadaDestino,
                        solicitud.KilogramosEstimados,
                        string.Empty, // DeclaracionDeCalidad siempre vacio
                        ConvertToInt32(solicitud.ConformeCondicional),
                        solicitud.PesoBruto,
                        solicitud.PesoTara,
                        solicitud.Observaciones,
                        solicitud.LoteDeMaterial,
                        (solicitud.IdEstablecimientoProcedencia != null) ? solicitud.IdEstablecimientoProcedencia.IdEstablecimiento : 0,
                        (solicitud.IdEstablecimientoDestino != null) ? solicitud.IdEstablecimientoDestino.IdEstablecimiento : 0,
                        solicitud.PatenteCamion,
                        solicitud.PatenteAcoplado,
                        solicitud.KmRecorridos,
                        ConvertToInt32(solicitud.EstadoFlete),
                        solicitud.CantHoras,
                        solicitud.TarifaReferencia,
                        solicitud.TarifaReal,
                        (solicitud.ClientePagadorDelFlete != null) ? solicitud.ClientePagadorDelFlete.IdCliente : 0,
                        ConvertToInt32(solicitud.EstadoEnSAP),
                        ConvertToInt32(solicitud.EstadoEnAFIP),
                        solicitud.Grano.IdGrano,
                        solicitud.CodigoAnulacionAfip,
                        solicitud.FechaAnulacionAfip,
                        solicitud.CodigoRespuestaEnvioSAP,
                        solicitud.CodigoRespuestaAnulacionSAP,
                        solicitud.MensajeRespuestaEnvioSAP,
                        solicitud.MensajeRespuestaAnulacionSAP,
                        (solicitud.IdEstablecimientoDestinoCambio != null) ? solicitud.IdEstablecimientoDestinoCambio.IdEstablecimiento : 0,
                        (solicitud.ClienteDestinatarioCambio != null) ? solicitud.ClienteDestinatarioCambio.IdCliente : 0,
                        solicitud.UsuarioModificacion,
                        this.GetIdEmpresa(),
                        solicitud.PHumedad,
                        solicitud.POtros
                        );
                }
                else

                    resul = SqlHelper.ExecuteScalar(conn1, "GuardarSolicitud",
                        solicitud.IdSolicitud,
                        solicitud.TipoDeCarta.IdTipoDeCarta,
                        solicitud.ObservacionAfip,
                        solicitud.NumeroCartaDePorte,
                        solicitud.Cee,
                        solicitud.Ctg.Replace(".", ""),
                        solicitud.FechaDeEmision,
                        solicitud.FechaDeCarga,
                        solicitud.FechaDeVencimiento,
                        (solicitud.ProveedorTitularCartaDePorte != null) ? solicitud.ProveedorTitularCartaDePorte.IdProveedor : 0,
                        (solicitud.ClienteIntermediario != null) ? solicitud.ClienteIntermediario.IdCliente : 0,
                        (solicitud.ClienteRemitenteComercial != null) ? solicitud.ClienteRemitenteComercial.IdCliente : 0,
                        solicitud.RemitenteComercialComoCanjeador,
                        (solicitud.ClienteCorredor != null) ? solicitud.ClienteCorredor.IdCliente : 0,
                        (solicitud.ClienteEntregador != null) ? solicitud.ClienteEntregador.IdCliente : 0,
                        (solicitud.ClienteDestinatario != null) ? solicitud.ClienteDestinatario.IdCliente : 0,
                        (solicitud.ClienteDestino != null) ? solicitud.ClienteDestino.IdCliente : 0,
                        (solicitud.ProveedorTransportista != null) ? solicitud.ProveedorTransportista.IdProveedor : 0,
                        (solicitud.ChoferTransportista != null) ? solicitud.ChoferTransportista.IdChofer : 0,
                        (solicitud.Chofer != null) ? solicitud.Chofer.IdChofer : 0,
                        solicitud.Grano.CosechaAfip.Codigo,
                        especieAfipCodigo,
                        solicitud.NumeroContrato,
                        solicitud.SapContrato,
                        solicitud.SinContrato,
                        solicitud.CargaPesadaDestino,
                        solicitud.KilogramosEstimados,
                        string.Empty, // DeclaracionDeCalidad siempre vacio
                        ConvertToInt32(solicitud.ConformeCondicional),
                        solicitud.PesoBruto,
                        solicitud.PesoTara,
                        solicitud.Observaciones,
                        solicitud.LoteDeMaterial,
                        (solicitud.IdEstablecimientoProcedencia != null) ? solicitud.IdEstablecimientoProcedencia.IdEstablecimiento : 0,
                        (solicitud.IdEstablecimientoDestino != null) ? solicitud.IdEstablecimientoDestino.IdEstablecimiento : 0,
                        solicitud.PatenteCamion,
                        solicitud.PatenteAcoplado,
                        solicitud.KmRecorridos,
                        ConvertToInt32(solicitud.EstadoFlete),
                        solicitud.CantHoras,
                        solicitud.TarifaReferencia,
                        solicitud.TarifaReal,
                        (solicitud.ClientePagadorDelFlete != null) ? solicitud.ClientePagadorDelFlete.IdCliente : 0,
                        ConvertToInt32(solicitud.EstadoEnSAP),
                        ConvertToInt32(solicitud.EstadoEnAFIP),
                        solicitud.Grano.IdGrano,
                        solicitud.CodigoAnulacionAfip,
                        solicitud.FechaAnulacionAfip,
                        solicitud.CodigoRespuestaEnvioSAP,
                        solicitud.CodigoRespuestaAnulacionSAP,
                        solicitud.MensajeRespuestaEnvioSAP,
                        solicitud.MensajeRespuestaAnulacionSAP,
                        (solicitud.IdEstablecimientoDestinoCambio != null) ? solicitud.IdEstablecimientoDestinoCambio.IdEstablecimiento : 0,
                        (solicitud.ClienteDestinatarioCambio != null) ? solicitud.ClienteDestinatarioCambio.IdCliente : 0,
                        solicitud.UsuarioCreacion,
                        this.GetIdEmpresa(),
                        solicitud.PHumedad,
                        solicitud.POtros
                        );
            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Guardar Solicitud: " + ex.Message.ToString());
            }
            finally
            {
                conn1.Close();
            }
            if (solicitud.IdSolicitud > 0)
            {
                return solicitud.IdSolicitud;
            }
            else
            {
                return ConvertToInt32(resul);
            }
        }

        public IList<Solicitud> GetAll()
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSolicitud", 0, this.GetIdEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Solicitud> result = new List<Solicitud>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Solicitud sol = new Solicitud();

                        if (row["ObservacionAfip"].ToString().Contains("Reserva"))
                        {
                            sol.IdSolicitud = ConvertToInt32(row["IdSolicitud"]);
                            sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
                            sol.Cee = row["Cee"].ToString();
                            sol.ObservacionAfip = row["ObservacionAfip"].ToString();
                            if (!(row["FechaCreacion"] is System.DBNull))
                                sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
                            sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();

                        }
                        else
                        {
                            sol.IdSolicitud = ConvertToInt32(row["IdSolicitud"]);
                            sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
                            sol.Cee = row["Cee"].ToString();
                            sol.Ctg = row["Ctg"].ToString();
                            sol.TipoDeCarta = TipoDeCartaDAO.Instance.GetOne(ConvertToInt32(row["IdTipoDeCarta"]));
                            sol.ProveedorTitularCartaDePorte = ProveedorDAO.Instance.GetOne(ConvertToInt32(row["IdProveedorTitularCartaDePorte"]));
                            sol.IdEstablecimientoProcedencia = EstablecimientoDAO.Instance.GetOne(ConvertToInt32(row["IdEstablecimientoProcedencia"]));
                            sol.IdEstablecimientoDestino = EstablecimientoDAO.Instance.GetOne(ConvertToInt32(row["IdEstablecimientoDestino"]));
                            sol.ObservacionAfip = row["ObservacionAfip"].ToString();
                            sol.EstadoEnAFIP = (Enums.EstadoEnAFIP)ConvertToInt32(row["EstadoEnAFIP"]);
                            sol.EstadoEnSAP = (Enums.EstadoEnvioSAP)ConvertToInt32(row["EstadoEnSAP"]);
                            sol.CodigoRespuestaEnvioSAP = row["CodigoRespuestaEnvioSAP"].ToString();
                            sol.CodigoRespuestaAnulacionSAP = row["CodigoRespuestaAnulacionSAP"].ToString();
                            sol.MensajeRespuestaAnulacionSAP = row["MensajeRespuestaAnulacionSAP"].ToString();
                            sol.MensajeRespuestaEnvioSAP = row["MensajeRespuestaEnvioSAP"].ToString();

                            if (!(row["CodigoAnulacionAfip"] is System.DBNull))
                                sol.CodigoAnulacionAfip = ConvertToInt64(row["CodigoAnulacionAfip"]);
                            if (!(row["FechaAnulacionAfip"] is System.DBNull))
                                sol.FechaAnulacionAfip = Convert.ToDateTime(row["FechaAnulacionAfip"]);


                            if (!(row["FechaCreacion"] is System.DBNull))
                                sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
                            sol.UsuarioModificacion = row["UsuarioModificacion"].ToString();

                            if (!(row["FechaModificacion"] is System.DBNull))
                                sol.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);
                            sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();

                        }


                        result.Add(sol);
                    }

                    return result;
                }
                else
                {
                    return new List<Solicitud>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR GetAll Solicitud: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }

        public IList<Solicitud> GetSolicitudByTransportista(int TransportistaId)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSolicitudByTransportistaProspecto", TransportistaId.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Solicitud> result = new List<Solicitud>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Solicitud sol = new Solicitud();

                        sol.IdSolicitud = ConvertToInt32(row["IdSolicitud"]);
                        sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
                        sol.Cee = row["Cee"].ToString();
                        sol.Ctg = row["Ctg"].ToString();
                        sol.TipoDeCarta = TipoDeCartaDAO.Instance.GetOne(ConvertToInt32(row["IdTipoDeCarta"]));
                        sol.ProveedorTitularCartaDePorte = ProveedorDAO.Instance.GetOne(ConvertToInt32(row["IdProveedorTitularCartaDePorte"]));
                        sol.IdEstablecimientoProcedencia = EstablecimientoDAO.Instance.GetOne(ConvertToInt32(row["IdEstablecimientoProcedencia"]));
                        sol.IdEstablecimientoDestino = EstablecimientoDAO.Instance.GetOne(ConvertToInt32(row["IdEstablecimientoDestino"]));
                        sol.ObservacionAfip = row["ObservacionAfip"].ToString();
                        sol.EstadoEnAFIP = (Enums.EstadoEnAFIP)ConvertToInt32(row["EstadoEnAFIP"]);
                        sol.EstadoEnSAP = (Enums.EstadoEnvioSAP)ConvertToInt32(row["EstadoEnSAP"]);
                        sol.CodigoRespuestaEnvioSAP = row["CodigoRespuestaEnvioSAP"].ToString();
                        sol.CodigoRespuestaAnulacionSAP = row["CodigoRespuestaAnulacionSAP"].ToString();
                        sol.MensajeRespuestaAnulacionSAP = row["MensajeRespuestaAnulacionSAP"].ToString();
                        sol.MensajeRespuestaEnvioSAP = row["MensajeRespuestaEnvioSAP"].ToString();

                        if (!(row["CodigoAnulacionAfip"] is System.DBNull))
                            sol.CodigoAnulacionAfip = ConvertToInt64(row["CodigoAnulacionAfip"]);
                        if (!(row["FechaAnulacionAfip"] is System.DBNull))
                            sol.FechaAnulacionAfip = Convert.ToDateTime(row["FechaAnulacionAfip"]);


                        if (!(row["FechaCreacion"] is System.DBNull))
                            sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
                        sol.UsuarioModificacion = row["UsuarioModificacion"].ToString();

                        if (!(row["FechaModificacion"] is System.DBNull))
                            sol.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);
                        sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();
                        result.Add(sol);
                    }

                    return result;
                }
                else
                {
                    return new List<Solicitud>();
                }
            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR GetAll Solicitud: " + ex.Message.ToString());
            }
            finally
            {
                conn1.Close();
            }
        }

        public IList<Solicitud> GetRechazadas()
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetRechazadas", 0, this.GetIdEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Solicitud> result = new List<Solicitud>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Solicitud sol = new Solicitud();
                        if (row["ObservacionAfip"].ToString().Contains("Reserva"))
                        {
                            sol.IdSolicitud = ConvertToInt32(row["IdSolicitud"]);
                            sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
                            sol.Cee = row["Cee"].ToString();
                            sol.ObservacionAfip = row["ObservacionAfip"].ToString();
                            sol.TipoDeCarta = TipoDeCartaDAO.Instance.GetOne(ConvertToInt32(row["IdTipoDeCarta"]));

                            if (!(row["IdEstablecimientoProcedencia"] is System.DBNull))
                                sol.IdEstablecimientoProcedencia = EstablecimientoDAO.Instance.GetOne(ConvertToInt32(row["IdEstablecimientoProcedencia"]));

                            if (!(row["FechaCreacion"] is System.DBNull))
                                sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
                            sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();

                        }
                        else
                        {
                            sol.IdSolicitud = ConvertToInt32(row["IdSolicitud"]);
                            sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
                            sol.Cee = row["Cee"].ToString();
                            sol.Ctg = row["Ctg"].ToString();
                            sol.TipoDeCarta = TipoDeCartaDAO.Instance.GetOne(ConvertToInt32(row["IdTipoDeCarta"]));
                            sol.ProveedorTitularCartaDePorte = ProveedorDAO.Instance.GetOne(ConvertToInt32(row["IdProveedorTitularCartaDePorte"]));

                            sol.ObservacionAfip = row["ObservacionAfip"].ToString();
                            if (!(row["FechaDeEmision"] is System.DBNull))
                                sol.FechaDeEmision = Convert.ToDateTime(row["FechaDeEmision"]);
                            if (!(row["FechaDeCarga"] is System.DBNull))
                                sol.FechaDeCarga = Convert.ToDateTime(row["FechaDeCarga"]);
                            if (!(row["FechaDeVencimiento"] is System.DBNull))
                                sol.FechaDeVencimiento = Convert.ToDateTime(row["FechaDeVencimiento"]);
                            sol.ClienteIntermediario = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteIntermediario"]));
                            sol.ClienteRemitenteComercial = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteRemitenteComercial"]));
                            sol.RemitenteComercialComoCanjeador = ConvertToBoolean(row["RemitenteComercialComoCanjeador"]);
                            sol.ClienteCorredor = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteCorredor"]));
                            sol.ClienteEntregador = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteEntregador"]));
                            sol.ClienteDestinatario = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteDestinatario"]));

                            sol.ClienteDestino = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteDestino"]));
                            sol.ProveedorTransportista = ProveedorDAO.Instance.GetOne(ConvertToInt32(row["IdProveedorTransportista"]));
                            sol.ChoferTransportista = ChoferDAO.Instance.GetOne(ConvertToInt32(row["IdChoferTransportista"]));

                            sol.Chofer = ChoferDAO.Instance.GetOne(ConvertToInt32(row["IdChofer"]));
                            sol.Grano = GranoDAO.Instance.GetOne(ConvertToInt32(row["IdGrano"]));
                            if (!(row["NumeroContrato"] is System.DBNull))
                                sol.NumeroContrato = ConvertToInt32(row["NumeroContrato"]);
                            if (!(row["SapContrato"] is System.DBNull))
                                sol.SapContrato = ConvertToInt32(row["SapContrato"]);

                            if (!(row["NumeroContrato"] is System.DBNull))
                                sol.SinContrato = true;
                            else
                                sol.SinContrato = false;

                            sol.CargaPesadaDestino = ConvertToBoolean(row["CargaPesadaDestino"]);

                            sol.ConformeCondicional = (Enums.ConformeCondicional)ConvertToInt32(row["IdConformeCondicional"]);

                            if (sol.CargaPesadaDestino)
                            {
                                sol.KilogramosEstimados = (long)ConvertToDecimal(row["KilogramosEstimados"]);
                            }
                            else
                            {
                                sol.PesoBruto = (long)ConvertToDecimal(row["PesoBruto"]);
                                sol.PesoTara = (long)ConvertToDecimal(row["PesoTara"]);
                                sol.PesoNeto = (sol.PesoBruto - sol.PesoTara);
                            }

                            sol.Observaciones = row["Observaciones"].ToString();
                            sol.LoteDeMaterial = row["LoteDeMaterial"].ToString();

                            sol.IdEstablecimientoProcedencia = EstablecimientoDAO.Instance.GetOne(ConvertToInt32(row["IdEstablecimientoProcedencia"]));
                            sol.IdEstablecimientoDestino = EstablecimientoDAO.Instance.GetOne(ConvertToInt32(row["IdEstablecimientoDestino"]));

                            sol.PatenteCamion = row["PatenteCamion"].ToString();
                            sol.PatenteAcoplado = row["PatenteAcoplado"].ToString();
                            sol.KmRecorridos = (long)ConvertToDecimal(row["KmRecorridos"]);
                            sol.EstadoFlete = (Enums.EstadoFlete)ConvertToInt32(row["EstadoFlete"]);
                            sol.CantHoras = (long)ConvertToDecimal(row["CantHoras"]);
                            sol.TarifaReferencia = ConvertToDecimal(row["TarifaReferencia"]);
                            sol.TarifaReal = ConvertToDecimal(row["TarifaReal"]);
                            sol.ClientePagadorDelFlete = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClientePagadorDelFlete"]));
                            sol.EstadoEnSAP = (Enums.EstadoEnvioSAP)ConvertToInt32(row["EstadoEnSAP"]);
                            sol.EstadoEnAFIP = (Enums.EstadoEnAFIP)ConvertToInt32(row["EstadoEnAFIP"]);
                            sol.CodigoRespuestaEnvioSAP = row["CodigoRespuestaEnvioSAP"].ToString();
                            sol.CodigoRespuestaAnulacionSAP = row["CodigoRespuestaAnulacionSAP"].ToString();
                            sol.MensajeRespuestaAnulacionSAP = row["MensajeRespuestaAnulacionSAP"].ToString();
                            sol.MensajeRespuestaEnvioSAP = row["MensajeRespuestaEnvioSAP"].ToString();

                            if (!(row["IdEstablecimientoDestinoCambio"] is System.DBNull))
                                sol.IdEstablecimientoDestinoCambio = EstablecimientoDAO.Instance.GetOne(ConvertToInt32(row["IdEstablecimientoDestinoCambio"]));
                            if (!(row["IdClienteDestinatarioCambio"] is System.DBNull))
                                sol.ClienteDestinatarioCambio = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteDestinatarioCambio"]));


                            if (!(row["CodigoAnulacionAfip"] is System.DBNull))
                                sol.CodigoAnulacionAfip = ConvertToInt64(row["CodigoAnulacionAfip"]);
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

                    return result;
                }
                else
                {
                    return new List<Solicitud>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR GetAll Solicitud: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }
        public IList<Solicitud> GetTop(int nrotop)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSolicitudTop", nrotop, this.GetIdEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Solicitud> result = new List<Solicitud>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Solicitud sol = new Solicitud();
                        if (row["ObservacionAfip"].ToString().Contains("Reserva"))
                        {
                            sol.IdSolicitud = ConvertToInt32(row["IdSolicitud"]);
                            sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
                            sol.Cee = row["Cee"].ToString();
                            sol.ObservacionAfip = row["ObservacionAfip"].ToString();
                            if (!(row["FechaCreacion"] is System.DBNull))
                                sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
                            sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();

                            sol.EstadoEnAFIP = (Enums.EstadoEnAFIP)ConvertToInt32(row["EstadoEnAFIP"]);
                            sol.EstadoEnSAP = (Enums.EstadoEnvioSAP)ConvertToInt32(row["EstadoEnSAP"]);


                        }
                        else
                        {

                            sol.IdSolicitud = ConvertToInt32(row["IdSolicitud"]);
                            sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
                            sol.Cee = row["Cee"].ToString();
                            sol.Ctg = row["Ctg"].ToString();
                            sol.TipoDeCarta = TipoDeCartaDAO.Instance.GetOne(ConvertToInt32(row["IdTipoDeCarta"]));
                            sol.ProveedorTitularCartaDePorte = ProveedorDAO.Instance.GetOne(ConvertToInt32(row["IdProveedorTitularCartaDePorte"]));
                            sol.IdEstablecimientoProcedencia = EstablecimientoDAO.Instance.GetOne(ConvertToInt32(row["IdEstablecimientoProcedencia"]));
                            sol.IdEstablecimientoDestino = EstablecimientoDAO.Instance.GetOne(ConvertToInt32(row["IdEstablecimientoDestino"]));
                            sol.ObservacionAfip = row["ObservacionAfip"].ToString();
                            sol.EstadoEnAFIP = (Enums.EstadoEnAFIP)ConvertToInt32(row["EstadoEnAFIP"]);
                            sol.EstadoEnSAP = (Enums.EstadoEnvioSAP)ConvertToInt32(row["EstadoEnSAP"]);
                            sol.CodigoRespuestaEnvioSAP = row["CodigoRespuestaEnvioSAP"].ToString();
                            sol.CodigoRespuestaAnulacionSAP = row["CodigoRespuestaAnulacionSAP"].ToString();
                            sol.MensajeRespuestaAnulacionSAP = row["MensajeRespuestaAnulacionSAP"].ToString();
                            sol.MensajeRespuestaEnvioSAP = row["MensajeRespuestaEnvioSAP"].ToString();

                            if (!(row["CodigoAnulacionAfip"] is System.DBNull))
                                sol.CodigoAnulacionAfip = ConvertToInt64(row["CodigoAnulacionAfip"]);
                            if (!(row["FechaAnulacionAfip"] is System.DBNull))
                                sol.FechaAnulacionAfip = Convert.ToDateTime(row["FechaAnulacionAfip"]);


                            if (!(row["FechaCreacion"] is System.DBNull))
                                sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
                            sol.UsuarioModificacion = row["UsuarioModificacion"].ToString();

                            if (!(row["FechaModificacion"] is System.DBNull))
                                sol.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);
                            sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();

                        }


                        result.Add(sol);
                    }

                    return result;
                }
                else
                {
                    return new List<Solicitud>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR GetAll Solicitud: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }
        public IList<Solicitud> GetTopConfirmacion()
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSolicitudTopConfirmacion", this.GetIdEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Solicitud> result = new List<Solicitud>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Solicitud sol = new Solicitud();

                        sol.IdSolicitud = ConvertToInt32(row["IdSolicitud"]);
                        sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
                        sol.TipoDeCarta = TipoDeCartaDAO.Instance.GetOne(ConvertToInt32(row["IdTipoDeCarta"]));
                        sol.IdEstablecimientoProcedencia = EstablecimientoDAO.Instance.GetOne(ConvertToInt32(row["IdEstablecimientoProcedencia"]));
                        sol.IdEstablecimientoDestino = EstablecimientoDAO.Instance.GetOne(ConvertToInt32(row["IdEstablecimientoDestino"]));
                        sol.PesoNeto = (row["PesoNeto"] is System.DBNull) ? 0 : ConvertToInt64(row["PesoNeto"]);
                        sol.KilogramosEstimados = (row["KilogramosEstimados"] is System.DBNull) ? 0 : ConvertToInt64(row["KilogramosEstimados"]);
                        sol.CargaPesadaDestino = ConvertToBoolean(row["CargaPesadaDestino"]);

                        if (!(row["FechaCreacion"] is System.DBNull))
                            sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
                        sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();

                        result.Add(sol);
                    }

                    return result;
                }
                else
                {
                    return new List<Solicitud>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR GetAll Solicitud: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }

        public IList<Solicitud> GetFiltro(string busqueda, string estadoAFIP, string estadoSAP)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSolicitudFiltro", busqueda, estadoAFIP, estadoSAP, this.GetIdEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Solicitud> result = new List<Solicitud>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Solicitud sol = new Solicitud();

                        mensaje = "IDSolicitud: " + ConvertToInt32(row["IdSolicitud"]).ToString();

                        if (row["ObservacionAfip"].ToString().Contains("Reserva"))
                        {
                            sol.IdSolicitud = ConvertToInt32(row["IdSolicitud"]);
                            sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
                            sol.Cee = row["Cee"].ToString();
                            sol.ObservacionAfip = row["ObservacionAfip"].ToString();
                            if (!(row["FechaCreacion"] is System.DBNull))
                                sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
                            sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();

                            sol.EstadoEnAFIP = (Enums.EstadoEnAFIP)ConvertToInt32(row["EstadoEnAFIP"]);
                            sol.EstadoEnSAP = (Enums.EstadoEnvioSAP)ConvertToInt32(row["EstadoEnSAP"]);

                        }
                        else
                        {

                            sol.IdSolicitud = ConvertToInt32(row["IdSolicitud"]); // si
                            sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();// si
                            //sol.Cee = row["Cee"].ToString();
                            sol.Ctg = row["Ctg"].ToString();// si
                            sol.TipoDeCartaString = row["DescripcionTipoDeCarta"].ToString();// si
                            sol.ProveedorTitularCartaDePorteString = row["ProveedorNombreTitularCartaDePorte"].ToString();// si
                            sol.EstablecimientoProcedenciaString = row["DescripcionEstablecimientoOrigen"].ToString();// si
                            //sol.IdEstablecimientoDestino = EstablecimientoDAO.Instance.GetOne(ConvertToInt32(row["IdEstablecimientoDestino"]));
                            sol.ObservacionAfip = row["ObservacionAfip"].ToString();// si
                            sol.EstadoEnAFIP = (Enums.EstadoEnAFIP)ConvertToInt32(row["EstadoEnAFIP"]);// si
                            sol.EstadoEnSAP = (Enums.EstadoEnvioSAP)ConvertToInt32(row["EstadoEnSAP"]);// si
                            sol.CodigoRespuestaEnvioSAP = row["CodigoRespuestaEnvioSAP"].ToString();
                            sol.CodigoRespuestaAnulacionSAP = row["CodigoRespuestaAnulacionSAP"].ToString();
                            sol.MensajeRespuestaAnulacionSAP = row["MensajeRespuestaAnulacionSAP"].ToString();
                            sol.MensajeRespuestaEnvioSAP = row["MensajeRespuestaEnvioSAP"].ToString();

                            if (!(row["CodigoAnulacionAfip"] is System.DBNull))
                                sol.CodigoAnulacionAfip = ConvertToInt64(row["CodigoAnulacionAfip"]);
                            if (!(row["FechaAnulacionAfip"] is System.DBNull))
                                sol.FechaAnulacionAfip = Convert.ToDateTime(row["FechaAnulacionAfip"]);

                            if (!(row["FechaCreacion"] is System.DBNull))
                                sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);// si
                            sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();


                            if (!(row["FechaModificacion"] is System.DBNull))
                                sol.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);
                            sol.UsuarioModificacion = row["UsuarioModificacion"].ToString();// si
                            //sol.Sox1116A = Sox1116ADAO.Instance.GetOneByIdSoclicitud(sol.IdSolicitud);
                        }

                        result.Add(sol);
                    }

                    return result;
                }
                else
                {
                    return new List<Solicitud>();
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

        public IList<Solicitud> GetSolicitudByEmpresa(int IdEmpresa)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSolicitudByEmpresa", IdEmpresa.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Solicitud> result = new List<Solicitud>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Solicitud sol = new Solicitud();
                        sol.IdSolicitud = ConvertToInt32(row["IdSolicitud"]);
                        result.Add(sol);
                    }

                    return result;
                }
                else
                {
                    return new List<Solicitud>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR GetSolicitudByEmpresa: " + mensaje + " - " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }
        }

        public int GetSolicitudByEmpresaCount(int IdEmpresa)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSolicitudByEmpresaCount", IdEmpresa.ToString());

                if (ds.Tables[0].Rows.Count > 0)
                    return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                else
                    return 0;
            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR GetSolicitudByEmpresaCount: " + mensaje + " - " + ex.Message.ToString());
            }
            finally
            {
                conn1.Close();
            }
        }

        public IList<Solicitud> GetFiltroConfirmacion(string busqueda)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSolicitudFiltroConfirmacion", busqueda, this.GetIdEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Solicitud> result = new List<Solicitud>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Solicitud sol = new Solicitud();

                        sol.IdSolicitud = ConvertToInt32(row["IdSolicitud"]);
                        sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
                        sol.TipoDeCarta = TipoDeCartaDAO.Instance.GetOne(ConvertToInt32(row["IdTipoDeCarta"]));
                        sol.IdEstablecimientoProcedencia = EstablecimientoDAO.Instance.GetOne(ConvertToInt32(row["IdEstablecimientoProcedencia"]));
                        sol.IdEstablecimientoDestino = EstablecimientoDAO.Instance.GetOne(ConvertToInt32(row["IdEstablecimientoDestino"]));
                        sol.PesoNeto = (row["PesoNeto"] is System.DBNull) ? 0 : ConvertToInt64(row["PesoNeto"]);
                        sol.KilogramosEstimados = (row["KilogramosEstimados"] is System.DBNull) ? 0 : ConvertToInt64(row["KilogramosEstimados"]);
                        sol.CargaPesadaDestino = ConvertToBoolean(row["CargaPesadaDestino"]);

                        if (!(row["FechaCreacion"] is System.DBNull))
                            sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
                        sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();

                        result.Add(sol);
                    }

                    return result;
                }
                else
                {
                    return new List<Solicitud>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR GetAll Solicitud: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }
        public SolicitudFull GetOne(int IdSolicitud)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSolicitud", IdSolicitud, this.GetIdEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<SolicitudFull> result = new List<SolicitudFull>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        SolicitudFull sol = new SolicitudFull();
                        if (row["ObservacionAfip"].ToString().Contains("Reserva"))
                        {
                            sol.IdSolicitud = ConvertToInt32(row["IdSolicitud"]);
                            sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
                            sol.Cee = row["Cee"].ToString();
                            sol.ObservacionAfip = row["ObservacionAfip"].ToString();
                            sol.TipoDeCarta = TipoDeCartaDAO.Instance.GetOne(ConvertToInt32(row["IdTipoDeCarta"]));

                            if (!(row["IdEstablecimientoProcedencia"] is System.DBNull))
                                sol.IdEstablecimientoProcedencia = EstablecimientoDAO.Instance.GetOne(ConvertToInt32(row["IdEstablecimientoProcedencia"]));

                            if (!(row["FechaCreacion"] is System.DBNull))
                                sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
                            sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();

                        }
                        else
                        {
                            sol.IdSolicitud = ConvertToInt32(row["IdSolicitud"]);
                            sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
                            sol.Cee = row["Cee"].ToString();
                            sol.Ctg = row["Ctg"].ToString();
                            sol.TipoDeCarta = TipoDeCartaDAO.Instance.GetOne(ConvertToInt32(row["IdTipoDeCarta"]));
                            sol.ProveedorTitularCartaDePorte = ProveedorDAO.Instance.GetOne(ConvertToInt32(row["IdProveedorTitularCartaDePorte"]));

                            sol.ObservacionAfip = row["ObservacionAfip"].ToString();
                            if (!(row["FechaDeEmision"] is System.DBNull))
                                sol.FechaDeEmision = Convert.ToDateTime(row["FechaDeEmision"]);
                            if (!(row["FechaDeCarga"] is System.DBNull))
                                sol.FechaDeCarga = Convert.ToDateTime(row["FechaDeCarga"]);
                            if (!(row["FechaDeVencimiento"] is System.DBNull))
                                sol.FechaDeVencimiento = Convert.ToDateTime(row["FechaDeVencimiento"]);
                            sol.ClienteIntermediario = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteIntermediario"]));
                            sol.ClienteRemitenteComercial = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteRemitenteComercial"]));
                            sol.RemitenteComercialComoCanjeador = ConvertToBoolean(row["RemitenteComercialComoCanjeador"]);
                            sol.ClienteCorredor = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteCorredor"]));
                            sol.ClienteEntregador = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteEntregador"]));
                            sol.ClienteDestinatario = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteDestinatario"]));

                            sol.ClienteDestino = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteDestino"]));
                            sol.ProveedorTransportista = ProveedorDAO.Instance.GetOne(ConvertToInt32(row["IdProveedorTransportista"]));
                            sol.ChoferTransportista = ChoferDAO.Instance.GetOne(ConvertToInt32(row["IdChoferTransportista"]));

                            sol.Chofer = ChoferDAO.Instance.GetOne(ConvertToInt32(row["IdChofer"]));
                            sol.Grano = GranoDAO.Instance.GetOne(ConvertToInt32(row["IdGrano"]));
                            if (!(row["NumeroContrato"] is System.DBNull))
                                sol.NumeroContrato = ConvertToInt32(row["NumeroContrato"]);
                            if (!(row["SapContrato"] is System.DBNull))
                                sol.SapContrato = ConvertToInt32(row["SapContrato"]);

                            if (!(row["NumeroContrato"] is System.DBNull))
                                sol.SinContrato = true;
                            else
                                sol.SinContrato = false;

                            sol.CargaPesadaDestino = ConvertToBoolean(row["CargaPesadaDestino"]);

                            sol.ConformeCondicional = (Enums.ConformeCondicional)ConvertToInt32(row["IdConformeCondicional"]);

                            if (sol.CargaPesadaDestino)
                            {
                                sol.KilogramosEstimados = (long)ConvertToDecimal(row["KilogramosEstimados"]);
                            }
                            else
                            {
                                sol.PesoBruto = (long)ConvertToDecimal(row["PesoBruto"]);
                                sol.PesoTara = (long)ConvertToDecimal(row["PesoTara"]);
                                sol.PesoNeto = (sol.PesoBruto - sol.PesoTara);
                            }

                            sol.Observaciones = row["Observaciones"].ToString();
                            sol.LoteDeMaterial = row["LoteDeMaterial"].ToString();

                            sol.IdEstablecimientoProcedencia = EstablecimientoDAO.Instance.GetOne(ConvertToInt32(row["IdEstablecimientoProcedencia"]));
                            sol.IdEstablecimientoDestino = EstablecimientoDAO.Instance.GetOne(ConvertToInt32(row["IdEstablecimientoDestino"]));

                            sol.PatenteCamion = row["PatenteCamion"].ToString();
                            sol.PatenteAcoplado = row["PatenteAcoplado"].ToString();
                            sol.KmRecorridos = (long)ConvertToDecimal(row["KmRecorridos"]);
                            sol.EstadoFlete = (Enums.EstadoFlete)ConvertToInt32(row["EstadoFlete"]);
                            sol.CantHoras = (long)ConvertToDecimal(row["CantHoras"]);
                            sol.TarifaReferencia = ConvertToDecimal(row["TarifaReferencia"]);
                            sol.TarifaReal = ConvertToDecimal(row["TarifaReal"]);
                            sol.ClientePagadorDelFlete = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClientePagadorDelFlete"]));
                            sol.EstadoEnSAP = (Enums.EstadoEnvioSAP)ConvertToInt32(row["EstadoEnSAP"]);
                            sol.EstadoEnAFIP = (Enums.EstadoEnAFIP)ConvertToInt32(row["EstadoEnAFIP"]);
                            sol.CodigoRespuestaEnvioSAP = row["CodigoRespuestaEnvioSAP"].ToString();
                            sol.CodigoRespuestaAnulacionSAP = row["CodigoRespuestaAnulacionSAP"].ToString();
                            sol.MensajeRespuestaAnulacionSAP = row["MensajeRespuestaAnulacionSAP"].ToString();
                            sol.MensajeRespuestaEnvioSAP = row["MensajeRespuestaEnvioSAP"].ToString();

                            if (!(row["IdEstablecimientoDestinoCambio"] is System.DBNull))
                                sol.IdEstablecimientoDestinoCambio = EstablecimientoDAO.Instance.GetOne(ConvertToInt32(row["IdEstablecimientoDestinoCambio"]));
                            if (!(row["IdClienteDestinatarioCambio"] is System.DBNull))
                                sol.ClienteDestinatarioCambio = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteDestinatarioCambio"]));


                            if (!(row["CodigoAnulacionAfip"] is System.DBNull))
                                sol.CodigoAnulacionAfip = ConvertToInt64(row["CodigoAnulacionAfip"]);
                            if (!(row["FechaAnulacionAfip"] is System.DBNull))
                                sol.FechaAnulacionAfip = Convert.ToDateTime(row["FechaAnulacionAfip"]);


                            if (!(row["FechaCreacion"] is System.DBNull))
                                sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
                            sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();


                            if (!(row["FechaModificacion"] is System.DBNull))
                                sol.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);
                            sol.UsuarioModificacion = row["UsuarioModificacion"].ToString();

                            sol.PHumedad = row["PHumedad"].ToString() == string.Empty ? 0 : Convert.ToDecimal(row["PHumedad"].ToString());
                            sol.POtros = row["POtros"].ToString() == string.Empty ? 0 : Convert.ToDecimal(row["POtros"].ToString());
                        }

                        try
                        {
                            sol.IdEmpresa = Tools.Value2<int>(row["IdEmpresa"], 0);
                            sol.IdGrupoEmpresa = EmpresaDAO.Instance.GetOneAdmin(sol.IdEmpresa).IdGrupoEmpresa;
                        }
                        catch
                        { }


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


        public int GetSolicitudByCDP(string cdp, out int idEmpresa)
        {
            int idSolicitud = 0;

            idEmpresa = 0;

            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSolicitudByCDP", cdp);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];

                    idSolicitud = ConvertToInt32(row["IdSolicitud"]);
                    idEmpresa = Tools.Value2<int>(row["IdEmpresa"], 0);
                    //sol.IdGrupoEmpresa = EmpresaDAO.Instance.GetOneAdmin(sol.IdEmpresa).IdGrupoEmpresa;
                }
            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get GetSolicitudByCDP: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

            return idSolicitud;
        }

        //Se agrega metodo ya que de SAP el input es el Numero de Carta. En CDP este dato no es unico.
        //Se mitiga realizando la búsqueda por Organizacion de Venta
        public int GetSolicitudByCDPSAP(string cdp, out int idEmpresa)
        {
            int idSolicitud = 0;

            idEmpresa = 0;

            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);
                IList<Empresa> Empresa = new List<Empresa>();

                Char delimiter = '|';
                string[] OrgVenta = cdp.Split(delimiter);

                if (OrgVenta.Count() > 1)
                    Empresa = (IList<Empresa>)EmpresaDAO.Instance.GetAll().Where(e => e.IdSapOrganizacionDeVenta == OrgVenta[1].ToString()).ToList();

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSolicitudByCDP", OrgVenta[0].ToString());

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow row = ds.Tables[0].Rows[i];

                    if (Empresa.Count > 0)
                    {
                        if (Empresa.Where(e => e.IdEmpresa == Convert.ToInt32(row["IdEmpresa"].ToString())).ToList().Count > 0)
                        {
                            idSolicitud = ConvertToInt32(row["IdSolicitud"]);
                            idEmpresa = Tools.Value2<int>(row["IdEmpresa"], 0);
                        }
                    }
                    else
                    {
                        idSolicitud = ConvertToInt32(row["IdSolicitud"]);
                        idEmpresa = Tools.Value2<int>(row["IdEmpresa"], 0);
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get GetSolicitudByCDP: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

            return idSolicitud;
        }

        //private SolicitudFull GetSolicitudByCDP(string cdp)
        //{
        //    SqlConnection conn1 = null;
        //    try
        //    {
        //        string sql = string.Empty;
        //        conn1 = new SqlConnection(connString);

        //        DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSolicitudByCDP", cdp);
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            IList<SolicitudFull> result = new List<SolicitudFull>();
        //            foreach (DataRow row in ds.Tables[0].Rows)
        //            {
        //                SolicitudFull sol = new SolicitudFull();

        //                if (row["ObservacionAfip"].ToString().Contains("Reserva"))
        //                {
        //                    sol.IdSolicitud = ConvertToInt32(row["IdSolicitud"]);
        //                    sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
        //                    sol.Cee = row["Cee"].ToString();
        //                    sol.ObservacionAfip = row["ObservacionAfip"].ToString();
        //                    if (!(row["FechaCreacion"] is System.DBNull))
        //                        sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
        //                    sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();

        //                }
        //                else
        //                {

        //                    sol.IdSolicitud = ConvertToInt32(row["IdSolicitud"]);
        //                    sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
        //                    sol.Cee = row["Cee"].ToString();
        //                    sol.Ctg = row["Ctg"].ToString();
        //                    sol.TipoDeCarta = TipoDeCartaDAO.Instance.GetOne(ConvertToInt32(row["IdTipoDeCarta"]));
        //                    sol.ProveedorTitularCartaDePorte = ProveedorDAO.Instance.GetOne(ConvertToInt32(row["IdProveedorTitularCartaDePorte"]));

        //                    sol.ObservacionAfip = row["ObservacionAfip"].ToString();
        //                    if (!(row["FechaDeEmision"] is System.DBNull))
        //                        sol.FechaDeEmision = Convert.ToDateTime(row["FechaDeEmision"]);
        //                    if (!(row["FechaDeCarga"] is System.DBNull))
        //                        sol.FechaDeCarga = Convert.ToDateTime(row["FechaDeCarga"]);
        //                    if (!(row["FechaDeVencimiento"] is System.DBNull))
        //                        sol.FechaDeVencimiento = Convert.ToDateTime(row["FechaDeVencimiento"]);
        //                    sol.ClienteIntermediario = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteIntermediario"]));
        //                    sol.ClienteRemitenteComercial = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteRemitenteComercial"]));
        //                    sol.RemitenteComercialComoCanjeador = ConvertToBoolean(row["RemitenteComercialComoCanjeador"]);
        //                    sol.ClienteCorredor = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteCorredor"]));
        //                    sol.ClienteEntregador = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteEntregador"]));
        //                    sol.ClienteDestinatario = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteDestinatario"]));
        //                    sol.ClienteDestino = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteDestino"]));
        //                    sol.ProveedorTransportista = ProveedorDAO.Instance.GetOne(ConvertToInt32(row["IdProveedorTransportista"]));
        //                    sol.ChoferTransportista = ChoferDAO.Instance.GetOne(ConvertToInt32(row["IdChoferTransportista"]));

        //                    sol.Chofer = ChoferDAO.Instance.GetOne(ConvertToInt32(row["IdChofer"]));
        //                    sol.Grano = GranoDAO.Instance.GetOne(ConvertToInt32(row["IdGrano"]));
        //                    if (!(row["NumeroContrato"] is System.DBNull))
        //                        sol.NumeroContrato = ConvertToInt32(row["NumeroContrato"]);
        //                    if (!(row["SapContrato"] is System.DBNull))
        //                        sol.SapContrato = ConvertToInt32(row["SapContrato"]);

        //                    if (!(row["NumeroContrato"] is System.DBNull))
        //                        sol.SinContrato = true;
        //                    else
        //                        sol.SinContrato = false;

        //                    sol.CargaPesadaDestino = ConvertToBoolean(row["CargaPesadaDestino"]);

        //                    sol.ConformeCondicional = (Enums.ConformeCondicional)ConvertToInt32(row["IdConformeCondicional"]);

        //                    if (sol.CargaPesadaDestino)
        //                    {
        //                        sol.KilogramosEstimados = (long)ConvertToDecimal(row["KilogramosEstimados"]);
        //                    }
        //                    else
        //                    {
        //                        sol.PesoBruto = (long)ConvertToDecimal(row["PesoBruto"]);
        //                        sol.PesoTara = (long)ConvertToDecimal(row["PesoTara"]);
        //                        sol.PesoNeto = (sol.PesoBruto - sol.PesoTara);
        //                    }

        //                    sol.Observaciones = row["Observaciones"].ToString();
        //                    sol.LoteDeMaterial = row["LoteDeMaterial"].ToString();

        //                    sol.IdEstablecimientoProcedencia = EstablecimientoDAO.Instance.GetOne(ConvertToInt32(row["IdEstablecimientoProcedencia"]));
        //                    sol.IdEstablecimientoDestino = EstablecimientoDAO.Instance.GetOne(ConvertToInt32(row["IdEstablecimientoDestino"]));
        //                    sol.PatenteCamion = row["PatenteCamion"].ToString();
        //                    sol.PatenteAcoplado = row["PatenteAcoplado"].ToString();
        //                    sol.KmRecorridos = (long)ConvertToDecimal(row["KmRecorridos"]);
        //                    sol.EstadoFlete = (Enums.EstadoFlete)ConvertToInt32(row["EstadoFlete"]);
        //                    sol.CantHoras = (long)ConvertToDecimal(row["CantHoras"]);
        //                    sol.TarifaReferencia = ConvertToDecimal(row["TarifaReferencia"]);
        //                    sol.TarifaReal = ConvertToDecimal(row["TarifaReal"]);
        //                    sol.ClientePagadorDelFlete = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClientePagadorDelFlete"]));
        //                    sol.EstadoEnSAP = (Enums.EstadoEnvioSAP)ConvertToInt32(row["EstadoEnSAP"]);
        //                    sol.EstadoEnAFIP = (Enums.EstadoEnAFIP)ConvertToInt32(row["EstadoEnAFIP"]);
        //                    sol.CodigoRespuestaEnvioSAP = row["CodigoRespuestaEnvioSAP"].ToString();
        //                    sol.CodigoRespuestaAnulacionSAP = row["CodigoRespuestaAnulacionSAP"].ToString();
        //                    sol.MensajeRespuestaAnulacionSAP = row["MensajeRespuestaAnulacionSAP"].ToString();
        //                    sol.MensajeRespuestaEnvioSAP = row["MensajeRespuestaEnvioSAP"].ToString();

        //                    if (!(row["IdEstablecimientoDestinoCambio"] is System.DBNull))
        //                        sol.IdEstablecimientoDestinoCambio = EstablecimientoDAO.Instance.GetOne(ConvertToInt32(row["IdEstablecimientoDestinoCambio"]));
        //                    if (!(row["IdClienteDestinatarioCambio"] is System.DBNull))
        //                        sol.ClienteDestinatarioCambio = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteDestinatarioCambio"]));


        //                    if (!(row["CodigoAnulacionAfip"] is System.DBNull))
        //                        sol.CodigoAnulacionAfip = ConvertToInt64(row["CodigoAnulacionAfip"]);
        //                    if (!(row["FechaAnulacionAfip"] is System.DBNull))
        //                        sol.FechaAnulacionAfip = Convert.ToDateTime(row["FechaAnulacionAfip"]);


        //                    if (!(row["FechaCreacion"] is System.DBNull))
        //                        sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
        //                    sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();


        //                    if (!(row["FechaModificacion"] is System.DBNull))
        //                        sol.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);
        //                    sol.UsuarioModificacion = row["UsuarioModificacion"].ToString();

        //                    try
        //                    {
        //                        sol.IdEmpresa = Tools.Value2<int>(row["IdEmpresa"], 0);
        //                        sol.IdGrupoEmpresa = EmpresaDAO.Instance.GetOneAdmin(sol.IdEmpresa).IdGrupoEmpresa;
        //                    }
        //                    catch
        //                    { }

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
        public Solicitud GetSolicitudByCTG(string ctg)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSolicitudByCTG", ctg, this.GetIdEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Solicitud> result = new List<Solicitud>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Solicitud sol = new Solicitud();

                        if (row["ObservacionAfip"].ToString().Contains("Reserva"))
                        {
                            sol.IdSolicitud = ConvertToInt32(row["IdSolicitud"]);
                            sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
                            sol.Cee = row["Cee"].ToString();
                            sol.ObservacionAfip = row["ObservacionAfip"].ToString();
                            if (!(row["FechaCreacion"] is System.DBNull))
                                sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
                            sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();

                        }
                        else
                        {

                            sol.IdSolicitud = ConvertToInt32(row["IdSolicitud"]);
                            sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
                            sol.Cee = row["Cee"].ToString();
                            sol.Ctg = row["Ctg"].ToString();
                            sol.TipoDeCarta = TipoDeCartaDAO.Instance.GetOne(ConvertToInt32(row["IdTipoDeCarta"]));
                            sol.ProveedorTitularCartaDePorte = ProveedorDAO.Instance.GetOne(ConvertToInt32(row["IdProveedorTitularCartaDePorte"]));

                            sol.ObservacionAfip = row["ObservacionAfip"].ToString();
                            if (!(row["FechaDeEmision"] is System.DBNull))
                                sol.FechaDeEmision = Convert.ToDateTime(row["FechaDeEmision"]);
                            if (!(row["FechaDeCarga"] is System.DBNull))
                                sol.FechaDeCarga = Convert.ToDateTime(row["FechaDeCarga"]);
                            if (!(row["FechaDeVencimiento"] is System.DBNull))
                                sol.FechaDeVencimiento = Convert.ToDateTime(row["FechaDeVencimiento"]);
                            sol.ClienteIntermediario = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteIntermediario"]));
                            sol.ClienteRemitenteComercial = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteRemitenteComercial"]));
                            sol.RemitenteComercialComoCanjeador = ConvertToBoolean(row["RemitenteComercialComoCanjeador"]);
                            sol.ClienteCorredor = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteCorredor"]));
                            sol.ClienteEntregador = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteEntregador"]));
                            sol.ClienteDestinatario = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteDestinatario"]));
                            sol.ClienteDestino = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteDestino"]));
                            sol.ProveedorTransportista = ProveedorDAO.Instance.GetOne(ConvertToInt32(row["IdProveedorTransportista"]));
                            sol.ChoferTransportista = ChoferDAO.Instance.GetOne(ConvertToInt32(row["IdChoferTransportista"]));

                            sol.Chofer = ChoferDAO.Instance.GetOne(ConvertToInt32(row["IdChofer"]));
                            sol.Grano = GranoDAO.Instance.GetOne(ConvertToInt32(row["IdGrano"]));
                            if (!(row["NumeroContrato"] is System.DBNull))
                                sol.NumeroContrato = ConvertToInt32(row["NumeroContrato"]);
                            if (!(row["SapContrato"] is System.DBNull))
                                sol.SapContrato = ConvertToInt32(row["SapContrato"]);

                            if (!(row["NumeroContrato"] is System.DBNull))
                                sol.SinContrato = true;
                            else
                                sol.SinContrato = false;

                            sol.CargaPesadaDestino = ConvertToBoolean(row["CargaPesadaDestino"]);

                            sol.ConformeCondicional = (Enums.ConformeCondicional)ConvertToInt32(row["IdConformeCondicional"]);

                            if (sol.CargaPesadaDestino)
                            {
                                sol.KilogramosEstimados = (long)ConvertToDecimal(row["KilogramosEstimados"]);
                            }
                            else
                            {
                                sol.PesoBruto = (long)ConvertToDecimal(row["PesoBruto"]);
                                sol.PesoTara = (long)ConvertToDecimal(row["PesoTara"]);
                                sol.PesoNeto = (sol.PesoBruto - sol.PesoTara);
                            }

                            sol.Observaciones = row["Observaciones"].ToString();
                            sol.LoteDeMaterial = row["LoteDeMaterial"].ToString();

                            sol.IdEstablecimientoProcedencia = EstablecimientoDAO.Instance.GetOne(ConvertToInt32(row["IdEstablecimientoProcedencia"]));
                            sol.IdEstablecimientoDestino = EstablecimientoDAO.Instance.GetOne(ConvertToInt32(row["IdEstablecimientoDestino"]));
                            sol.PatenteCamion = row["PatenteCamion"].ToString();
                            sol.PatenteAcoplado = row["PatenteAcoplado"].ToString();
                            sol.KmRecorridos = (long)ConvertToDecimal(row["KmRecorridos"]);
                            sol.EstadoFlete = (Enums.EstadoFlete)ConvertToInt32(row["EstadoFlete"]);
                            sol.CantHoras = (long)ConvertToDecimal(row["CantHoras"]);
                            sol.TarifaReferencia = ConvertToDecimal(row["TarifaReferencia"]);
                            sol.TarifaReal = ConvertToDecimal(row["TarifaReal"]);
                            sol.ClientePagadorDelFlete = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClientePagadorDelFlete"]));
                            sol.EstadoEnSAP = (Enums.EstadoEnvioSAP)ConvertToInt32(row["EstadoEnSAP"]);
                            sol.EstadoEnAFIP = (Enums.EstadoEnAFIP)ConvertToInt32(row["EstadoEnAFIP"]);
                            sol.CodigoRespuestaEnvioSAP = row["CodigoRespuestaEnvioSAP"].ToString();
                            sol.CodigoRespuestaAnulacionSAP = row["CodigoRespuestaAnulacionSAP"].ToString();
                            sol.MensajeRespuestaAnulacionSAP = row["MensajeRespuestaAnulacionSAP"].ToString();
                            sol.MensajeRespuestaEnvioSAP = row["MensajeRespuestaEnvioSAP"].ToString();

                            if (!(row["IdEstablecimientoDestinoCambio"] is System.DBNull))
                                sol.IdEstablecimientoDestinoCambio = EstablecimientoDAO.Instance.GetOne(ConvertToInt32(row["IdEstablecimientoDestinoCambio"]));
                            if (!(row["IdClienteDestinatarioCambio"] is System.DBNull))
                                sol.ClienteDestinatarioCambio = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteDestinatarioCambio"]));


                            if (!(row["CodigoAnulacionAfip"] is System.DBNull))
                                sol.CodigoAnulacionAfip = ConvertToInt64(row["CodigoAnulacionAfip"]);
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
        public IList<Solicitud> GetSolicitudAnulacionSAP()
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSolicitudAnulacionSAP", this.GetIdEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Solicitud> result = new List<Solicitud>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Solicitud sol = new Solicitud();
                        sol.IdSolicitud = ConvertToInt32(row["IdSolicitud"]);
                        sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
                        sol.Cee = row["Cee"].ToString();
                        sol.Ctg = row["Ctg"].ToString();
                        sol.TipoDeCarta = TipoDeCartaDAO.Instance.GetOne(ConvertToInt32(row["IdTipoDeCarta"]));
                        sol.ProveedorTitularCartaDePorte = ProveedorDAO.Instance.GetOne(ConvertToInt32(row["IdProveedorTitularCartaDePorte"]));

                        sol.ObservacionAfip = row["ObservacionAfip"].ToString();
                        if (!(row["FechaDeEmision"] is System.DBNull))
                            sol.FechaDeEmision = Convert.ToDateTime(row["FechaDeEmision"]);
                        if (!(row["FechaDeCarga"] is System.DBNull))
                            sol.FechaDeCarga = Convert.ToDateTime(row["FechaDeCarga"]);
                        if (!(row["FechaDeVencimiento"] is System.DBNull))
                            sol.FechaDeVencimiento = Convert.ToDateTime(row["FechaDeVencimiento"]);
                        sol.ClienteIntermediario = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteIntermediario"]));
                        sol.ClienteRemitenteComercial = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteRemitenteComercial"]));
                        sol.RemitenteComercialComoCanjeador = ConvertToBoolean(row["RemitenteComercialComoCanjeador"]);
                        sol.ClienteCorredor = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteCorredor"]));
                        sol.ClienteEntregador = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteEntregador"]));
                        sol.ClienteDestinatario = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteDestinatario"]));
                        sol.ClienteDestino = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteDestino"]));
                        sol.ProveedorTransportista = ProveedorDAO.Instance.GetOne(ConvertToInt32(row["IdProveedorTransportista"]));
                        sol.ChoferTransportista = ChoferDAO.Instance.GetOne(ConvertToInt32(row["IdChoferTransportista"]));

                        sol.Chofer = ChoferDAO.Instance.GetOne(ConvertToInt32(row["IdChofer"]));
                        sol.Grano = GranoDAO.Instance.GetOne(ConvertToInt32(row["IdGrano"]));
                        if (!(row["NumeroContrato"] is System.DBNull))
                            sol.NumeroContrato = ConvertToInt32(row["NumeroContrato"]);
                        if (!(row["SapContrato"] is System.DBNull))
                            sol.SapContrato = ConvertToInt32(row["SapContrato"]);

                        if (!(row["NumeroContrato"] is System.DBNull))
                            sol.SinContrato = true;
                        else
                            sol.SinContrato = false;

                        sol.CargaPesadaDestino = ConvertToBoolean(row["CargaPesadaDestino"]);

                        sol.ConformeCondicional = (Enums.ConformeCondicional)ConvertToInt32(row["IdConformeCondicional"]);

                        if (sol.CargaPesadaDestino)
                        {
                            sol.KilogramosEstimados = (long)ConvertToDecimal(row["KilogramosEstimados"]);
                        }
                        else
                        {
                            sol.PesoBruto = (long)ConvertToDecimal(row["PesoBruto"]);
                            sol.PesoTara = (long)ConvertToDecimal(row["PesoTara"]);
                            sol.PesoNeto = (sol.PesoBruto - sol.PesoTara);
                        }

                        sol.Observaciones = row["Observaciones"].ToString();
                        sol.LoteDeMaterial = row["LoteDeMaterial"].ToString();

                        sol.IdEstablecimientoProcedencia = EstablecimientoDAO.Instance.GetOne(ConvertToInt32(row["IdEstablecimientoProcedencia"]));
                        sol.IdEstablecimientoDestino = EstablecimientoDAO.Instance.GetOne(ConvertToInt32(row["IdEstablecimientoDestino"]));
                        sol.PatenteCamion = row["PatenteCamion"].ToString();
                        sol.PatenteAcoplado = row["PatenteAcoplado"].ToString();
                        sol.KmRecorridos = (long)ConvertToDecimal(row["KmRecorridos"]);
                        sol.EstadoFlete = (Enums.EstadoFlete)ConvertToInt32(row["EstadoFlete"]);
                        sol.CantHoras = (long)ConvertToDecimal(row["CantHoras"]);
                        sol.TarifaReferencia = ConvertToDecimal(row["TarifaReferencia"]);
                        sol.TarifaReal = ConvertToDecimal(row["TarifaReal"]);
                        sol.ClientePagadorDelFlete = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClientePagadorDelFlete"]));
                        sol.EstadoEnSAP = (Enums.EstadoEnvioSAP)ConvertToInt32(row["EstadoEnSAP"]);
                        sol.EstadoEnAFIP = (Enums.EstadoEnAFIP)ConvertToInt32(row["EstadoEnAFIP"]);
                        sol.CodigoRespuestaEnvioSAP = row["CodigoRespuestaEnvioSAP"].ToString();
                        sol.CodigoRespuestaAnulacionSAP = row["CodigoRespuestaAnulacionSAP"].ToString();
                        sol.MensajeRespuestaAnulacionSAP = row["MensajeRespuestaAnulacionSAP"].ToString();
                        sol.MensajeRespuestaEnvioSAP = row["MensajeRespuestaEnvioSAP"].ToString();

                        if (!(row["IdEstablecimientoDestinoCambio"] is System.DBNull))
                            sol.IdEstablecimientoDestinoCambio = EstablecimientoDAO.Instance.GetOne(ConvertToInt32(row["IdEstablecimientoDestinoCambio"]));
                        if (!(row["IdClienteDestinatarioCambio"] is System.DBNull))
                            sol.ClienteDestinatarioCambio = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteDestinatarioCambio"]));


                        if (!(row["CodigoAnulacionAfip"] is System.DBNull))
                            sol.CodigoAnulacionAfip = ConvertToInt64(row["CodigoAnulacionAfip"]);
                        if (!(row["FechaAnulacionAfip"] is System.DBNull))
                            sol.FechaAnulacionAfip = Convert.ToDateTime(row["FechaAnulacionAfip"]);


                        if (!(row["FechaCreacion"] is System.DBNull))
                            sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
                        sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();


                        if (!(row["FechaModificacion"] is System.DBNull))
                            sol.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);
                        sol.UsuarioModificacion = row["UsuarioModificacion"].ToString();



                        result.Add(sol);
                    }

                    if (result.Count > 0)
                        return result;
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
        public IList<Solicitud> GetSolicitudesEnvioSAP()
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSolicitudEnvioSAP", this.GetIdEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Solicitud> result = new List<Solicitud>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Solicitud sol = new Solicitud();
                        sol.IdSolicitud = ConvertToInt32(row["IdSolicitud"]);
                        sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
                        sol.Cee = row["Cee"].ToString();
                        sol.Ctg = row["Ctg"].ToString();
                        sol.TipoDeCarta = TipoDeCartaDAO.Instance.GetOne(ConvertToInt32(row["IdTipoDeCarta"]));
                        sol.ProveedorTitularCartaDePorte = ProveedorDAO.Instance.GetOne(ConvertToInt32(row["IdProveedorTitularCartaDePorte"]));

                        sol.ObservacionAfip = row["ObservacionAfip"].ToString();
                        if (!(row["FechaDeEmision"] is System.DBNull))
                            sol.FechaDeEmision = Convert.ToDateTime(row["FechaDeEmision"]);
                        if (!(row["FechaDeCarga"] is System.DBNull))
                            sol.FechaDeCarga = Convert.ToDateTime(row["FechaDeCarga"]);
                        if (!(row["FechaDeVencimiento"] is System.DBNull))
                            sol.FechaDeVencimiento = Convert.ToDateTime(row["FechaDeVencimiento"]);
                        sol.ClienteIntermediario = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteIntermediario"]));
                        sol.ClienteRemitenteComercial = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteRemitenteComercial"]));
                        sol.RemitenteComercialComoCanjeador = ConvertToBoolean(row["RemitenteComercialComoCanjeador"]);
                        sol.ClienteCorredor = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteCorredor"]));
                        sol.ClienteEntregador = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteEntregador"]));
                        sol.ClienteDestinatario = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteDestinatario"]));
                        sol.ClienteDestino = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteDestino"]));
                        sol.ProveedorTransportista = ProveedorDAO.Instance.GetOne(ConvertToInt32(row["IdProveedorTransportista"]));
                        sol.ChoferTransportista = ChoferDAO.Instance.GetOne(ConvertToInt32(row["IdChoferTransportista"]));

                        sol.Chofer = ChoferDAO.Instance.GetOne(ConvertToInt32(row["IdChofer"]));
                        sol.Grano = GranoDAO.Instance.GetOne(ConvertToInt32(row["IdGrano"]));
                        if (!(row["NumeroContrato"] is System.DBNull))
                            sol.NumeroContrato = ConvertToInt32(row["NumeroContrato"]);
                        if (!(row["SapContrato"] is System.DBNull))
                            sol.SapContrato = ConvertToInt32(row["SapContrato"]);

                        if (!(row["NumeroContrato"] is System.DBNull))
                            sol.SinContrato = true;
                        else
                            sol.SinContrato = false;

                        sol.CargaPesadaDestino = ConvertToBoolean(row["CargaPesadaDestino"]);

                        sol.ConformeCondicional = (Enums.ConformeCondicional)ConvertToInt32(row["IdConformeCondicional"]);

                        if (sol.CargaPesadaDestino)
                        {
                            sol.KilogramosEstimados = (long)ConvertToDecimal(row["KilogramosEstimados"]);
                        }
                        else
                        {

                            sol.PesoBruto = (long)ConvertToDecimal(row["PesoBruto"]);
                            sol.PesoTara = (long)ConvertToDecimal(row["PesoTara"]);
                            sol.PesoNeto = (sol.PesoBruto - sol.PesoTara);
                        }


                        sol.Observaciones = row["Observaciones"].ToString();
                        sol.LoteDeMaterial = row["LoteDeMaterial"].ToString();

                        sol.IdEstablecimientoProcedencia = EstablecimientoDAO.Instance.GetOne(ConvertToInt32(row["IdEstablecimientoProcedencia"]));
                        sol.IdEstablecimientoDestino = EstablecimientoDAO.Instance.GetOne(ConvertToInt32(row["IdEstablecimientoDestino"]));
                        sol.PatenteCamion = row["PatenteCamion"].ToString();
                        sol.PatenteAcoplado = row["PatenteAcoplado"].ToString();
                        sol.KmRecorridos = (long)ConvertToDecimal(row["KmRecorridos"]);
                        sol.EstadoFlete = (Enums.EstadoFlete)ConvertToInt32(row["EstadoFlete"]);
                        sol.CantHoras = (long)ConvertToDecimal(row["CantHoras"]);
                        sol.TarifaReferencia = ConvertToDecimal(row["TarifaReferencia"]);
                        sol.TarifaReal = ConvertToDecimal(row["TarifaReal"]);
                        sol.ClientePagadorDelFlete = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClientePagadorDelFlete"]));
                        sol.EstadoEnSAP = (Enums.EstadoEnvioSAP)ConvertToInt32(row["EstadoEnSAP"]);
                        sol.EstadoEnAFIP = (Enums.EstadoEnAFIP)ConvertToInt32(row["EstadoEnAFIP"]);
                        sol.CodigoRespuestaEnvioSAP = row["CodigoRespuestaEnvioSAP"].ToString();
                        sol.CodigoRespuestaAnulacionSAP = row["CodigoRespuestaAnulacionSAP"].ToString();
                        sol.MensajeRespuestaAnulacionSAP = row["MensajeRespuestaAnulacionSAP"].ToString();
                        sol.MensajeRespuestaEnvioSAP = row["MensajeRespuestaEnvioSAP"].ToString();

                        if (!(row["PHumedad"] is System.DBNull))
                            sol.PHumedad = Convert.ToDecimal(row["PHumedad"].ToString());

                        if (!(row["POtros"] is System.DBNull))
                            sol.POtros = Convert.ToDecimal(row["POtros"].ToString());

                        if (!(row["IdEstablecimientoDestinoCambio"] is System.DBNull))
                            sol.IdEstablecimientoDestinoCambio = EstablecimientoDAO.Instance.GetOne(ConvertToInt32(row["IdEstablecimientoDestinoCambio"]));
                        if (!(row["IdClienteDestinatarioCambio"] is System.DBNull))
                            sol.ClienteDestinatarioCambio = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteDestinatarioCambio"]));


                        if (!(row["CodigoAnulacionAfip"] is System.DBNull))
                            sol.CodigoAnulacionAfip = ConvertToInt64(row["CodigoAnulacionAfip"]);
                        if (!(row["FechaAnulacionAfip"] is System.DBNull))
                            sol.FechaAnulacionAfip = Convert.ToDateTime(row["FechaAnulacionAfip"]);


                        if (!(row["FechaCreacion"] is System.DBNull))
                            sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
                        sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();


                        if (!(row["FechaModificacion"] is System.DBNull))
                            sol.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);
                        sol.UsuarioModificacion = row["UsuarioModificacion"].ToString();


                        result.Add(sol);
                    }

                    if (result.Count > 0)
                        return result;
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



        public IList<Solicitud> GetMisReservas(string Usuario)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetMisReservas", Usuario, this.GetIdGrupoEmpresa(), this.GetIdEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Solicitud> result = new List<Solicitud>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Solicitud sol = new Solicitud();
                        sol.IdSolicitud = ConvertToInt32(row["IdSolicitud"]);
                        sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
                        sol.Cee = row["Cee"].ToString();
                        sol.ObservacionAfip = row["ObservacionAfip"].ToString();

                        if (!(row["FechaCreacion"] is System.DBNull))
                            sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
                        sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();

                        result.Add(sol);
                    }

                    return result;
                }
                else
                {
                    return new List<Solicitud>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR GetAll MisReservas: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }

        public void ConsultaDeEstadosAFIP()
        {
            ClienteDAO.Instance.SaveLogInterfaz("ConsultaDeEstadosAFIP()");
            if (Environment.MachineName.ToUpper() == "WI7-SIS22N-ADM" || Environment.MachineName.ToUpper() == "SRV-MS10-ADM")
            {
                ClienteDAO.Instance.SaveLogInterfaz("QAS_SinIntegracionAfip");
                return;
            }
            var ws = new wsAfip_v3();
            var resul = ws.consultarCTG(DateTime.Now.AddDays(-7));
            if (resul.arrayDatosConsultarCTG != null)
            {
                foreach (var dato in resul.arrayDatosConsultarCTG)
                {
                    if (dato != null)
                    {
                        String nroCTG = ConvertToInt64(dato.ctg.Replace(".", string.Empty)).ToString();
                        if (!String.IsNullOrEmpty(nroCTG))
                        {
                            ClienteDAO.Instance.SaveLogInterfaz("Busco Sol. por CTG : " + nroCTG);
                            Solicitud sol = GetSolicitudByCTG(nroCTG);

                            if (sol == null)
                            {
                                ClienteDAO.Instance.SaveLogInterfaz("Solicitud: " + nroCTG + " NO ENCONTRADA");
                            }
                            else
                            {
                                ClienteDAO.Instance.SaveLogInterfaz("Solicitud: " + sol.NumeroCartaDePorte + " - EstadoEnAFIP: " + sol.EstadoEnAFIP.ToString());

                                if (sol.EstadoEnAFIP == Enums.EstadoEnAFIP.VueltaOrigen)
                                { }
                                else if (sol.EstadoEnAFIP == Enums.EstadoEnAFIP.CambioDestino)
                                { }
                                else if (sol.EstadoEnAFIP == Enums.EstadoEnAFIP.Anulada)
                                { }
                                else
                                {
                                    switch (dato.estado)
                                    {
                                        case "Otorgado":
                                            sol.EstadoEnAFIP = Enums.EstadoEnAFIP.Otorgado;
                                            break;
                                        case "Anulado":
                                            sol.EstadoEnAFIP = Enums.EstadoEnAFIP.Anulada;
                                            break;
                                        case "Confirmado":
                                            sol.ObservacionAfip = "CTG Confirmado";
                                            sol.EstadoEnAFIP = Enums.EstadoEnAFIP.Confirmado;
                                            break;
                                        case "Confirmación Definitiva":
                                            sol.ObservacionAfip = "CTG Confirmación Definitiva";
                                            sol.EstadoEnAFIP = Enums.EstadoEnAFIP.ConfirmadoDefinitivo;
                                            break;
                                        case "Rechazado":
                                            sol.ObservacionAfip = "CTG Rechazado";
                                            sol.EstadoEnAFIP = Enums.EstadoEnAFIP.Rechazado;
                                            break;

                                        default:
                                            break;
                                    }
                                    SaveOrUpdate(sol);
                                }
                            }
                        }
                    }
                }
            }
        }

        public DataTable GetAllReporte(DateTime FD, DateTime FH)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSolicitudReporte", FD, FH, this.GetIdEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<SolicitudReporte> result = new List<SolicitudReporte>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        SolicitudReporte sol = new SolicitudReporte();
                        if (row["ObservacionAfip"].ToString().Contains("Reserva"))
                        {
                            sol.IdSolicitud = row["IdSolicitud"].ToString();
                            sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
                            sol.Cee = row["Cee"].ToString();
                            sol.ObservacionAfip = row["ObservacionAfip"].ToString();

                            if (!(row["IdEstablecimientoProcedencia"] is System.DBNull))
                                sol.EstablecimientoProcedencia = EstablecimientoDAO.Instance.GetOne(ConvertToInt32(row["IdEstablecimientoProcedencia"])).ToString();

                            if (!(row["FechaCreacion"] is System.DBNull))
                                sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]).ToString("dd/MM/yyyy HH:mm");
                            sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();

                        }
                        else
                        {
                            sol.IdSolicitud = row["IdSolicitud"].ToString();
                            sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
                            sol.Cee = row["Cee"].ToString();
                            sol.Ctg = row["Ctg"].ToString();
                            sol.TipoDeCarta = TipoDeCartaDAO.Instance.GetOne(ConvertToInt32(row["IdTipoDeCarta"])).ToString();
                            sol.ProveedorTitularCartaDePorte = ProveedorDAO.Instance.GetOne(ConvertToInt32(row["IdProveedorTitularCartaDePorte"])).ToString();

                            sol.ObservacionAfip = row["ObservacionAfip"].ToString();
                            if (!(row["FechaDeEmision"] is System.DBNull))
                                sol.FechaDeEmision = Convert.ToDateTime(row["FechaDeEmision"]).ToString("dd/MM/yyyy HH:mm");
                            if (!(row["FechaDeCarga"] is System.DBNull))
                                sol.FechaDeCarga = Convert.ToDateTime(row["FechaDeCarga"]).ToString("dd/MM/yyyy HH:mm");
                            if (!(row["FechaDeVencimiento"] is System.DBNull))
                                sol.FechaDeVencimiento = Convert.ToDateTime(row["FechaDeVencimiento"]).ToString("dd/MM/yyyy HH:mm");
                            sol.ClienteIntermediario = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteIntermediario"])).ToString();
                            sol.ClienteRemitenteComercial = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteRemitenteComercial"])).ToString();
                            sol.RemitenteComercialComoCanjeador = (ConvertToBoolean(row["RemitenteComercialComoCanjeador"]) ? "Si" : "No");
                            sol.ClienteCorredor = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteCorredor"])).ToString();
                            sol.ClienteEntregador = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteEntregador"])).ToString();
                            sol.ClienteDestinatario = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteDestinatario"])).ToString();

                            sol.ClienteDestino = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteDestino"])).ToString();

                            Cliente cliPagadorFlete = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClientePagadorDelFlete"]));
                            if (cliPagadorFlete != null && cliPagadorFlete.EsEmpresa())
                            {
                                sol.ProveedorTransportista = ProveedorDAO.Instance.GetOne(ConvertToInt32(row["IdProveedorTransportista"])).ToString();
                                sol.ChoferTransportista = string.Empty;
                            }
                            else
                            {
                                sol.ProveedorTransportista = string.Empty;
                                sol.ChoferTransportista = ChoferDAO.Instance.GetOne(ConvertToInt32(row["IdChoferTransportista"])).ToString();
                            }


                            sol.Chofer = ChoferDAO.Instance.GetOne(ConvertToInt32(row["IdChofer"])).ToString();
                            sol.Grano = GranoDAO.Instance.GetOne(ConvertToInt32(row["IdGrano"])).ToString();
                            if (!(row["NumeroContrato"] is System.DBNull))
                                sol.NumeroContrato = ConvertToInt32(row["NumeroContrato"]).ToString();

                            sol.CargaPesadaDestino = (ConvertToBoolean(row["CargaPesadaDestino"]) ? "Si" : "No");
                            sol.ConformeCondicional = ((Enums.ConformeCondicional)ConvertToInt32(row["IdConformeCondicional"])).ToString();
                            sol.KilogramosEstimados = row["KilogramosEstimados"].ToString();
                            sol.PesoBruto = row["PesoBruto"].ToString();
                            sol.PesoTara = row["PesoTara"].ToString();

                            sol.Observaciones = row["Observaciones"].ToString();

                            sol.EstablecimientoProcedencia = EstablecimientoDAO.Instance.GetOne(ConvertToInt32(row["IdEstablecimientoProcedencia"])).ToString();
                            sol.EstablecimientoDestino = EstablecimientoDAO.Instance.GetOne(ConvertToInt32(row["IdEstablecimientoDestino"])).ToString();

                            sol.PatenteCamion = row["PatenteCamion"].ToString();
                            sol.PatenteAcoplado = row["PatenteAcoplado"].ToString();
                            sol.KmRecorridos = row["KmRecorridos"].ToString();
                            sol.EstadoFlete = ((Enums.EstadoFlete)ConvertToInt32(row["EstadoFlete"])).ToString();
                            sol.CantHoras = row["CantHoras"].ToString();
                            sol.TarifaReferencia = row["TarifaReferencia"].ToString();
                            sol.TarifaReal = row["TarifaReal"].ToString();

                            sol.ClientePagadorDelFlete = cliPagadorFlete.ToString();
                            sol.EstadoEnSAP = ((Enums.EstadoEnvioSAP)ConvertToInt32(row["EstadoEnSAP"])).ToString();
                            sol.EstadoEnAFIP = ((Enums.EstadoEnAFIP)ConvertToInt32(row["EstadoEnAFIP"])).ToString();
                            sol.CodigoRespuestaEnvioSAP = row["CodigoRespuestaEnvioSAP"].ToString();
                            sol.CodigoRespuestaAnulacionSAP = row["CodigoRespuestaAnulacionSAP"].ToString();
                            //sol.MensajeRespuestaAnulacionSAP = row["MensajeRespuestaAnulacionSAP"].ToString();
                            //sol.MensajeRespuestaEnvioSAP = row["MensajeRespuestaEnvioSAP"].ToString();

                            if (!(row["IdEstablecimientoDestinoCambio"] is System.DBNull))
                                sol.EstablecimientoDestinoCambio = EstablecimientoDAO.Instance.GetOne(ConvertToInt32(row["IdEstablecimientoDestinoCambio"])).ToString();
                            if (!(row["IdClienteDestinatarioCambio"] is System.DBNull))
                                sol.ClienteDestinatarioCambio = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteDestinatarioCambio"])).ToString();


                            if (!(row["CodigoAnulacionAfip"] is System.DBNull))
                                sol.CodigoAnulacionAfip = row["CodigoAnulacionAfip"].ToString();
                            if (!(row["FechaAnulacionAfip"] is System.DBNull))
                                sol.FechaAnulacionAfip = Convert.ToDateTime(row["FechaAnulacionAfip"]).ToString("dd/MM/yyyy HH:mm");

                            sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();

                            if (!(row["FechaCreacion"] is System.DBNull))
                                sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]).ToString("dd/MM/yyyy HH:mm");
                            sol.UsuarioModificacion = row["UsuarioModificacion"].ToString();

                            if (!(row["FechaModificacion"] is System.DBNull))
                                sol.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]).ToString("dd/MM/yyyy HH:mm");

                        }

                        result.Add(sol);
                    }

                    if (result.Count > 0)
                        return GetDataTableFromIListGeneric(result).Tables[0];
                    else
                        return new DataTable();


                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR GetAllReporte Solicitud: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

            IList<SolicitudReporte> listaVacia = new List<SolicitudReporte>();
            listaVacia.Add(new SolicitudReporte());

            return GetDataTableFromIListGeneric(listaVacia).Tables[0];

        }
        public IList<Solicitud> GetAllReporteEmitidas(DateTime FD, DateTime FH)
        {

            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSolicitudReporteEmitidas", FD, FH, this.GetIdEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {

                    IList<Solicitud> result = new List<Solicitud>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Solicitud sol = new Solicitud();
                        if (row["ObservacionAfip"].ToString().Contains("Reserva"))
                        {
                            sol.IdSolicitud = ConvertToInt32(row["IdSolicitud"]);
                            sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
                            sol.Cee = row["Cee"].ToString();
                            sol.ObservacionAfip = row["ObservacionAfip"].ToString();

                            if (!(row["IdEstablecimientoProcedencia"] is System.DBNull))
                                sol.IdEstablecimientoProcedencia = EstablecimientoDAO.Instance.GetOne(ConvertToInt32(row["IdEstablecimientoProcedencia"]));

                            if (!(row["FechaCreacion"] is System.DBNull))
                                sol.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
                            sol.UsuarioCreacion = row["UsuarioCreacion"].ToString();

                        }
                        else
                        {
                            sol.IdSolicitud = ConvertToInt32(row["IdSolicitud"]);
                            sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
                            sol.Cee = row["Cee"].ToString();
                            sol.Ctg = row["Ctg"].ToString();
                            sol.TipoDeCarta = TipoDeCartaDAO.Instance.GetOne(ConvertToInt32(row["IdTipoDeCarta"]));
                            sol.ProveedorTitularCartaDePorte = ProveedorDAO.Instance.GetOne(ConvertToInt32(row["IdProveedorTitularCartaDePorte"]));

                            sol.ObservacionAfip = row["ObservacionAfip"].ToString();
                            if (!(row["FechaDeEmision"] is System.DBNull))
                                sol.FechaDeEmision = Convert.ToDateTime(row["FechaDeEmision"]);
                            if (!(row["FechaDeCarga"] is System.DBNull))
                                sol.FechaDeCarga = Convert.ToDateTime(row["FechaDeCarga"]);
                            if (!(row["FechaDeVencimiento"] is System.DBNull))
                                sol.FechaDeVencimiento = Convert.ToDateTime(row["FechaDeVencimiento"]);
                            sol.ClienteIntermediario = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteIntermediario"]));
                            sol.ClienteRemitenteComercial = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteRemitenteComercial"]));
                            sol.RemitenteComercialComoCanjeador = ConvertToBoolean(row["RemitenteComercialComoCanjeador"]);
                            sol.ClienteCorredor = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteCorredor"]));
                            sol.ClienteEntregador = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteEntregador"]));
                            sol.ClienteDestinatario = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteDestinatario"]));

                            sol.ClienteDestino = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteDestino"]));
                            sol.ProveedorTransportista = ProveedorDAO.Instance.GetOne(ConvertToInt32(row["IdProveedorTransportista"]));
                            sol.ChoferTransportista = ChoferDAO.Instance.GetOne(ConvertToInt32(row["IdChoferTransportista"]));

                            sol.Chofer = ChoferDAO.Instance.GetOne(ConvertToInt32(row["IdChofer"]));
                            sol.Grano = GranoDAO.Instance.GetOne(ConvertToInt32(row["IdGrano"]));
                            if (!(row["NumeroContrato"] is System.DBNull))
                                sol.NumeroContrato = ConvertToInt32(row["NumeroContrato"]);
                            if (!(row["SapContrato"] is System.DBNull))
                                sol.SapContrato = ConvertToInt32(row["SapContrato"]);

                            if (!(row["NumeroContrato"] is System.DBNull))
                                sol.SinContrato = true;
                            else
                                sol.SinContrato = false;

                            sol.CargaPesadaDestino = ConvertToBoolean(row["CargaPesadaDestino"]);

                            sol.ConformeCondicional = (Enums.ConformeCondicional)ConvertToInt32(row["IdConformeCondicional"]);

                            if (sol.CargaPesadaDestino)
                            {
                                sol.KilogramosEstimados = (long)ConvertToDecimal(row["KilogramosEstimados"]);
                            }
                            else
                            {
                                sol.PesoBruto = (long)ConvertToDecimal(row["PesoBruto"]);
                                sol.PesoTara = (long)ConvertToDecimal(row["PesoTara"]);
                                sol.PesoNeto = (sol.PesoBruto - sol.PesoTara);
                            }

                            sol.Observaciones = row["Observaciones"].ToString();
                            sol.LoteDeMaterial = row["LoteDeMaterial"].ToString();

                            sol.IdEstablecimientoProcedencia = EstablecimientoDAO.Instance.GetOne(ConvertToInt32(row["IdEstablecimientoProcedencia"]));
                            sol.IdEstablecimientoDestino = EstablecimientoDAO.Instance.GetOne(ConvertToInt32(row["IdEstablecimientoDestino"]));

                            sol.PatenteCamion = row["PatenteCamion"].ToString();
                            sol.PatenteAcoplado = row["PatenteAcoplado"].ToString();
                            sol.KmRecorridos = (long)ConvertToDecimal(row["KmRecorridos"]);
                            sol.EstadoFlete = (Enums.EstadoFlete)ConvertToInt32(row["EstadoFlete"]);
                            sol.CantHoras = (long)ConvertToDecimal(row["CantHoras"]);
                            sol.TarifaReferencia = ConvertToDecimal(row["TarifaReferencia"]);
                            sol.TarifaReal = ConvertToDecimal(row["TarifaReal"]);
                            sol.ClientePagadorDelFlete = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClientePagadorDelFlete"]));
                            sol.EstadoEnSAP = (Enums.EstadoEnvioSAP)ConvertToInt32(row["EstadoEnSAP"]);
                            sol.EstadoEnAFIP = (Enums.EstadoEnAFIP)ConvertToInt32(row["EstadoEnAFIP"]);
                            sol.CodigoRespuestaEnvioSAP = row["CodigoRespuestaEnvioSAP"].ToString();
                            sol.CodigoRespuestaAnulacionSAP = row["CodigoRespuestaAnulacionSAP"].ToString();
                            sol.MensajeRespuestaAnulacionSAP = row["MensajeRespuestaAnulacionSAP"].ToString();
                            sol.MensajeRespuestaEnvioSAP = row["MensajeRespuestaEnvioSAP"].ToString();

                            if (!(row["IdEstablecimientoDestinoCambio"] is System.DBNull))
                                sol.IdEstablecimientoDestinoCambio = EstablecimientoDAO.Instance.GetOne(ConvertToInt32(row["IdEstablecimientoDestinoCambio"]));
                            if (!(row["IdClienteDestinatarioCambio"] is System.DBNull))
                                sol.ClienteDestinatarioCambio = ClienteDAO.Instance.GetOne(ConvertToInt32(row["IdClienteDestinatarioCambio"]));


                            if (!(row["CodigoAnulacionAfip"] is System.DBNull))
                                sol.CodigoAnulacionAfip = ConvertToInt64(row["CodigoAnulacionAfip"]);
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
                        return result;
                    else
                        return new List<Solicitud>();



                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR GetAllReporte Solicitud: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }


            return new List<Solicitud>();
        }

        public DataTable GetReporteGrafico(DateTime FD, DateTime FH)
        {
            IList<SolicitudGrafico> listaVacia = new List<SolicitudGrafico>();

            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSolicitudReporteGrafico", FD, FH, this.GetIdEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<SolicitudGrafico> result = new List<SolicitudGrafico>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        SolicitudGrafico sol = new SolicitudGrafico();

                        sol.Fecha = row["Fecha"].ToString();
                        sol.CntAfip = ConvertToInt32(row["CntAfip"]);
                        sol.CntSap = ConvertToInt32(row["CntSap"]);

                        result.Add(sol);
                    }

                    if (result.Count > 0)
                    {
                        return GetDataTableFromIListGeneric(result).Tables[0];
                    }
                    else
                    {
                        listaVacia = new List<SolicitudGrafico>();
                        listaVacia.Add(new SolicitudGrafico());

                        return GetDataTableFromIListGeneric(listaVacia).Tables[0];
                    }


                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR GetReporteGrafico: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

            listaVacia = new List<SolicitudGrafico>();
            listaVacia.Add(new SolicitudGrafico());

            return GetDataTableFromIListGeneric(listaVacia).Tables[0];

        }

        public DataTable GetReporteGraficoNotIn(DateTime FD, DateTime FH)
        {
            IList<SolicitudGraficoNotIn> listaVacia = new List<SolicitudGraficoNotIn>();

            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSolicitudReporteGraficoNotIn", FD, FH, this.GetIdEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<SolicitudGraficoNotIn> result = new List<SolicitudGraficoNotIn>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        SolicitudGraficoNotIn sol = new SolicitudGraficoNotIn();

                        sol.Fecha = row["Fecha"].ToString();
                        sol.Numerocartadeporte = row["Numerocartadeporte"].ToString();
                        sol.CodigoAfip = row["CodigoAfip"].ToString();
                        sol.CodigoSAP = row["CodigoSAP"].ToString();
                        sol.EstadoEnAFIP = ((Enums.EstadoEnAFIP)ConvertToInt32(row["EstadoEnAFIP"])).ToString();
                        sol.EstadoEnSAP = ((Enums.EstadoEnvioSAP)ConvertToInt32(row["EstadoEnSAP"])).ToString();

                        result.Add(sol);
                    }

                    if (result.Count > 0)
                    {
                        return GetDataTableFromIListGeneric(result).Tables[0];
                    }
                    else
                    {
                        listaVacia = new List<SolicitudGraficoNotIn>();
                        listaVacia.Add(new SolicitudGraficoNotIn());

                        return GetDataTableFromIListGeneric(listaVacia).Tables[0];
                    }


                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR GetReporteGrafico: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

            listaVacia = new List<SolicitudGraficoNotIn>();
            listaVacia.Add(new SolicitudGraficoNotIn());

            return GetDataTableFromIListGeneric(listaVacia).Tables[0];

        }


        public DataTable GetAllReporte1116ADataTable(DateTime FD, DateTime FH, int modo)
        {
            IList<SolicitudReporte1116A> resul = GetAllReporte1116A(FD, FH, modo);

            if (resul.Count > 0)
            {
                return GetDataTableFromIListGeneric(resul).Tables[0];

            }
            else
            {
                IList<SolicitudReporte1116A> listaVacia = new List<SolicitudReporte1116A>();
                listaVacia.Add(new SolicitudReporte1116A());
                return GetDataTableFromIListGeneric(listaVacia).Tables[0];
            }

        }

        public IList<SolicitudReporte1116A> GetAllReporte1116A(DateTime FD, DateTime FH, int modo)
        {

            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSolicitudReporte1116A", FD, FH, modo, this.GetIdEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {

                    IList<SolicitudReporte1116A> result = new List<SolicitudReporte1116A>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        SolicitudReporte1116A sol = new SolicitudReporte1116A();
                        sol.NroCartaDePorte = row["NumeroCartaDePorte"].ToString();
                        sol.NroCEE = row["Cee"].ToString();
                        sol.NroCTG = row["Ctg"].ToString();
                        sol.TitularCartaDePorte = ProveedorDAO.Instance.GetOne(ConvertToInt32(row["IdProveedorTitularCartaDePorte"])).Nombre;

                        if (!(row["FechaDeEmision"] is System.DBNull))
                            sol.FechaDeEmision = Convert.ToDateTime(row["FechaDeEmision"]).ToString("dd/MM/yyyy");

                        sol.Numero1116A = row["Numero1116A"].ToString();
                        if (!(row["Fecha1116A"] is System.DBNull))
                            sol.Fecha1116A = Convert.ToDateTime(row["Fecha1116A"]).ToString("dd/MM/yyyy");

                        result.Add(sol);
                    }

                    if (result.Count > 0)
                        return result;
                    else
                        return new List<SolicitudReporte1116A>();

                }


            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR GetAllReporte1116A: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

            IList<SolicitudReporte1116A> listavacia = new List<SolicitudReporte1116A>();
            listavacia.Add(new SolicitudReporte1116A());
            return new List<SolicitudReporte1116A>();

        }




        private static DataSet GetDataTableFromIListGeneric<T>(IList<T> aIList)
        {
            DataTable dTable = new DataTable();
            object baseObj = aIList[0];
            Type objectType = baseObj.GetType();
            PropertyInfo[] properties = objectType.GetProperties();

            DataColumn col;
            foreach (PropertyInfo property in properties)
            {
                if ((string)property.Name != "EmpresaNoDataSet")
                {
                    col = new DataColumn();
                    col.ColumnName = (string)property.Name;
                    //col.DataType = property.PropertyType;
                    dTable.Columns.Add(col);
                }
            }
            //Adds the rows to the table
            DataRow row;
            foreach (object objItem in aIList)
            {
                row = dTable.NewRow();

                foreach (PropertyInfo property in properties)
                {
                    if (property.Name != "EmpresaNoDataSet")
                    {
                        row[property.Name] = property.GetValue(objItem, null);
                    }
                }
                dTable.Rows.Add(row);
            }

            DataSet ds = new DataSet("Resultado");
            ds.Tables.Add(dTable);
            return ds;

        }


    }


    public class SolicitudReporte1116A
    {

        private String nroCartaDePorte;

        public String NroCartaDePorte
        {
            get { return nroCartaDePorte; }
            set { nroCartaDePorte = value; }
        }
        private String nroCEE;

        public String NroCEE
        {
            get { return nroCEE; }
            set { nroCEE = value; }
        }
        private String nroCTG;

        public String NroCTG
        {
            get { return nroCTG; }
            set { nroCTG = value; }
        }
        private String fechaDeEmision;

        public String FechaDeEmision
        {
            get { return fechaDeEmision; }
            set { fechaDeEmision = value; }
        }
        private String titularCartaDePorte;

        public String TitularCartaDePorte
        {
            get { return titularCartaDePorte; }
            set { titularCartaDePorte = value; }
        }
        private String numero1116A;

        public String Numero1116A
        {
            get { return numero1116A; }
            set { numero1116A = value; }
        }
        private String fecha1116A;

        public String Fecha1116A
        {
            get { return fecha1116A; }
            set { fecha1116A = value; }
        }


    }

    public class SolicitudReporte
    {
        private String idSolicitud;

        public String IdSolicitud
        {
            get { return idSolicitud; }
            set { idSolicitud = value; }
        }
        private String tipoDeCarta;

        public String TipoDeCarta
        {
            get { return tipoDeCarta; }
            set { tipoDeCarta = value; }
        }
        private String observacionAfip;

        public String ObservacionAfip
        {
            get { return observacionAfip; }
            set { observacionAfip = value; }
        }
        private String numeroCartaDePorte;

        public String NumeroCartaDePorte
        {
            get { return numeroCartaDePorte; }
            set { numeroCartaDePorte = value; }
        }
        private String cee;

        public String Cee
        {
            get { return cee; }
            set { cee = value; }
        }
        private String ctg;

        public String Ctg
        {
            get { return ctg; }
            set { ctg = value; }
        }
        private String fechaDeEmision;

        public String FechaDeEmision
        {
            get { return fechaDeEmision; }
            set { fechaDeEmision = value; }
        }
        private String fechaDeCarga;

        public String FechaDeCarga
        {
            get { return fechaDeCarga; }
            set { fechaDeCarga = value; }
        }
        private String fechaDeVencimiento;

        public String FechaDeVencimiento
        {
            get { return fechaDeVencimiento; }
            set { fechaDeVencimiento = value; }
        }
        private String proveedorTitularCartaDePorte;

        public String ProveedorTitularCartaDePorte
        {
            get { return proveedorTitularCartaDePorte; }
            set { proveedorTitularCartaDePorte = value; }
        }
        private String clienteIntermediario;

        public String ClienteIntermediario
        {
            get { return clienteIntermediario; }
            set { clienteIntermediario = value; }
        }
        private String clienteRemitenteComercial;

        public String ClienteRemitenteComercial
        {
            get { return clienteRemitenteComercial; }
            set { clienteRemitenteComercial = value; }
        }

        private string remitenteComercialComoCanjeador;
        public String RemitenteComercialComoCanjeador
        {
            get { return remitenteComercialComoCanjeador; }
            set { remitenteComercialComoCanjeador = value; }
        }

        private String clienteCorredor;

        public String ClienteCorredor
        {
            get { return clienteCorredor; }
            set { clienteCorredor = value; }
        }
        private String clienteEntregador;

        public String ClienteEntregador
        {
            get { return clienteEntregador; }
            set { clienteEntregador = value; }
        }
        private String clienteDestinatario;

        public String ClienteDestinatario
        {
            get { return clienteDestinatario; }
            set { clienteDestinatario = value; }
        }
        private String clienteDestino;

        public String ClienteDestino
        {
            get { return clienteDestino; }
            set { clienteDestino = value; }
        }
        private String proveedorTransportista;

        public String ProveedorTransportista
        {
            get { return proveedorTransportista; }
            set { proveedorTransportista = value; }
        }
        private String choferTransportista;

        public String ChoferTransportista
        {
            get { return choferTransportista; }
            set { choferTransportista = value; }
        }
        private String chofer;

        public String Chofer
        {
            get { return chofer; }
            set { chofer = value; }
        }
        private String grano;

        public String Grano
        {
            get { return grano; }
            set { grano = value; }
        }
        private String numeroContrato;

        public String NumeroContrato
        {
            get { return numeroContrato; }
            set { numeroContrato = value; }
        }
        private String cargaPesadaDestino;

        public String CargaPesadaDestino
        {
            get { return cargaPesadaDestino; }
            set { cargaPesadaDestino = value; }
        }
        private String kilogramosEstimados;

        public String KilogramosEstimados
        {
            get { return kilogramosEstimados; }
            set { kilogramosEstimados = value; }
        }
        private String conformeCondicional;

        public String ConformeCondicional
        {
            get { return conformeCondicional; }
            set { conformeCondicional = value; }
        }
        private String pesoBruto;

        public String PesoBruto
        {
            get { return pesoBruto; }
            set { pesoBruto = value; }
        }
        private String pesoTara;

        public String PesoTara
        {
            get { return pesoTara; }
            set { pesoTara = value; }
        }
        private String pesoNeto;

        public String PesoNeto
        {
            get { return pesoNeto; }
            set { pesoNeto = value; }
        }
        private String observaciones;

        public String Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }
        private String establecimientoProcedencia;

        public String EstablecimientoProcedencia
        {
            get { return establecimientoProcedencia; }
            set { establecimientoProcedencia = value; }
        }
        private String establecimientoDestino;

        public String EstablecimientoDestino
        {
            get { return establecimientoDestino; }
            set { establecimientoDestino = value; }
        }
        private String patenteCamion;

        public String PatenteCamion
        {
            get { return patenteCamion; }
            set { patenteCamion = value; }
        }
        private String patenteAcoplado;

        public String PatenteAcoplado
        {
            get { return patenteAcoplado; }
            set { patenteAcoplado = value; }
        }
        private String kmRecorridos;

        public String KmRecorridos
        {
            get { return kmRecorridos; }
            set { kmRecorridos = value; }
        }
        private String estadoFlete;

        public String EstadoFlete
        {
            get { return estadoFlete; }
            set { estadoFlete = value; }
        }
        private String cantHoras;

        public String CantHoras
        {
            get { return cantHoras; }
            set { cantHoras = value; }
        }
        private String tarifaReferencia;

        public String TarifaReferencia
        {
            get { return tarifaReferencia; }
            set { tarifaReferencia = value; }
        }
        private String tarifaReal;

        public String TarifaReal
        {
            get { return tarifaReal; }
            set { tarifaReal = value; }
        }
        private String clientePagadorDelFlete;

        public String ClientePagadorDelFlete
        {
            get { return clientePagadorDelFlete; }
            set { clientePagadorDelFlete = value; }
        }
        private String estadoEnAFIP;

        public String EstadoEnAFIP
        {
            get { return estadoEnAFIP; }
            set { estadoEnAFIP = value; }
        }
        private String estadoEnSAP;

        public String EstadoEnSAP
        {
            get { return estadoEnSAP; }
            set { estadoEnSAP = value; }
        }
        private String establecimientoDestinoCambio;

        public String EstablecimientoDestinoCambio
        {
            get { return establecimientoDestinoCambio; }
            set { establecimientoDestinoCambio = value; }
        }
        private String clienteDestinatarioCambio;

        public String ClienteDestinatarioCambio
        {
            get { return clienteDestinatarioCambio; }
            set { clienteDestinatarioCambio = value; }
        }
        private String codigoAnulacionAfip;

        public String CodigoAnulacionAfip
        {
            get { return codigoAnulacionAfip; }
            set { codigoAnulacionAfip = value; }
        }
        private String fechaAnulacionAfip;

        public String FechaAnulacionAfip
        {
            get { return fechaAnulacionAfip; }
            set { fechaAnulacionAfip = value; }
        }
        private String codigoRespuestaEnvioSAP;

        public String CodigoRespuestaEnvioSAP
        {
            get { return codigoRespuestaEnvioSAP; }
            set { codigoRespuestaEnvioSAP = value; }
        }
        //private String mensajeRespuestaEnvioSAP;

        //public String MensajeRespuestaEnvioSAP
        //{
        //    get { return mensajeRespuestaEnvioSAP.Replace(";", ""); }
        //    set { mensajeRespuestaEnvioSAP = value; }
        //}
        private String codigoRespuestaAnulacionSAP;

        public String CodigoRespuestaAnulacionSAP
        {
            get { return codigoRespuestaAnulacionSAP; }
            set { codigoRespuestaAnulacionSAP = value; }
        }
        //private String mensajeRespuestaAnulacionSAP;

        //public String MensajeRespuestaAnulacionSAP
        //{
        //    get { return mensajeRespuestaAnulacionSAP.Replace(";", ""); }
        //    set { mensajeRespuestaAnulacionSAP = value; }
        //}
        private String fechaCreacion;

        public String FechaCreacion
        {
            get { return fechaCreacion; }
            set { fechaCreacion = value; }
        }
        private String usuarioCreacion;

        public String UsuarioCreacion
        {
            get { return usuarioCreacion; }
            set { usuarioCreacion = value; }
        }
        private String fechaModificacion;

        public String FechaModificacion
        {
            get { return fechaModificacion; }
            set { fechaModificacion = value; }
        }
        private String usuarioModificacion;

        public String UsuarioModificacion
        {
            get { return usuarioModificacion; }
            set { usuarioModificacion = value; }
        }

    }

    public class SolicitudGrafico
    {

        private string fecha;

        public string Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
        private int cntAfip;

        public int CntAfip
        {
            get { return cntAfip; }
            set { cntAfip = value; }
        }
        private int cntSap;

        public int CntSap
        {
            get { return cntSap; }
            set { cntSap = value; }
        }


    }

    public class SolicitudGraficoNotIn
    {
        private string fecha;

        public string Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
        private string numerocartadeporte;

        public string Numerocartadeporte
        {
            get { return numerocartadeporte; }
            set { numerocartadeporte = value; }
        }
        private string codigoAfip;

        public string CodigoAfip
        {
            get { return codigoAfip; }
            set { codigoAfip = value; }
        }
        private string codigoSAP;

        public string CodigoSAP
        {
            get { return codigoSAP; }
            set { codigoSAP = value; }
        }
        private string estadoEnAFIP;

        public string EstadoEnAFIP
        {
            get { return estadoEnAFIP; }
            set { estadoEnAFIP = value; }
        }
        private string estadoEnSAP;

        public string EstadoEnSAP
        {
            get { return estadoEnSAP; }
            set { estadoEnSAP = value; }
        }


    }
}

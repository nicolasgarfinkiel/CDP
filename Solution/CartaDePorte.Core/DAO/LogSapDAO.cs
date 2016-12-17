using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CartaDePorte.Core.Exception;
using CartaDePorte.Core.Domain;
using System.Data;

namespace CartaDePorte.Core.DAO
{
    public class LogSapDAO : BaseDAO
    {
        private static LogSapDAO instance;
        public LogSapDAO() { }

        public static LogSapDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LogSapDAO();
                }
                return instance;
            }
        }



        public void SaveOrUpdate(LogSap logsap)
        {
            Object resul = null;
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                resul = SqlHelper.ExecuteScalar(conn1, "GuardarLogSap",
                        logsap.IDoc,
                        logsap.Origen,
                        logsap.NroDocumentoRE,
                        logsap.NroDocumentoSap,
                        logsap.TipoMensaje,
                        logsap.TextoMensaje,
                        logsap.NroEnvio);


            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Guardar LogSap: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }


        }


        public IList<LogSap> GetOneByNroCartaDePorte(string nroDocumentoRE)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                if (PaisDAO.Instance.GetOne(App.Usuario.IdPais).Descripcion.ToUpper().Contains("PARAGUAY"))
                    nroDocumentoRE = nroDocumentoRE + "|" + EmpresaDAO.Instance.GetOne(App.Usuario.Empresa.IdEmpresa).IdSapOrganizacionDeVenta;

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetLogSap", nroDocumentoRE);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<LogSap> result = new List<LogSap>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        LogSap log = new LogSap();
                        log.IdLogSap = Convert.ToInt32(row["IdLogSap"]);
                        log.IDoc = row["IDoc"].ToString();
                        log.Origen = row["Origen"].ToString();
                        log.NroDocumentoRE = row["NroDocumentoRE"].ToString();
                        log.NroDocumentoSap = row["NroDocumentoSap"].ToString();
                        log.TipoMensaje = row["TipoMensaje"].ToString();
                        log.TextoMensaje = row["TextoMensaje"].ToString();
                        log.NroEnvio = Convert.ToInt32(row["NroEnvio"]);
                        log.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);

                        result.Add(log);
                    }

                    return result;
                }
                else
                {
                    return new List<LogSap>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get LogSap: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }

        public int GetLogSapUltimoNroEnvio(string nroCDP)
        {
            Object resul = null;
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                resul = SqlHelper.ExecuteScalar(conn1, "GetLogSapUltimoNroEnvio", nroCDP);

                if (resul != null)
                    return Convert.ToInt32(resul);
                else
                    return 0;

            }
            catch (System.Exception ex)
            {
                string exep = ex.Message.ToString();
                return 0;

            }
            finally
            {
                conn1.Close();
            }
        }

        public IList<Solicitud> getCambiosEstados(string cdp)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetCambiosEstados", cdp);
                if (ds.Tables[0].Rows.Count > 0)
                {


                    IList<Solicitud> result = new List<Solicitud>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Solicitud sol = new Solicitud();
                        sol.IdSolicitud = Convert.ToInt32(row["IdSolicitud"]); // si
                        sol.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();// si
                        sol.Cee = row["Cee"].ToString();
                        sol.Ctg = row["Ctg"].ToString();// si
                        sol.ObservacionAfip = row["ObservacionAfip"].ToString();// si
                        sol.EstadoEnAFIP = (Enums.EstadoEnAFIP)Convert.ToInt32(row["EstadoEnAFIP"]);// si
                        sol.EstadoEnSAP = (Enums.EstadoEnvioSAP)Convert.ToInt32(row["EstadoEnSAP"]);// si

                        sol.CodigoRespuestaEnvioSAP = row["CodigoRespuestaEnvioSAP"].ToString();
                        sol.MensajeRespuestaEnvioSAP = row["MensajeRespuestaEnvioSAP"].ToString();
                        sol.CodigoRespuestaAnulacionSAP = row["CodigoRespuestaAnulacionSAP"].ToString();
                        sol.MensajeRespuestaAnulacionSAP = row["MensajeRespuestaAnulacionSAP"].ToString();

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
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get CambiosEstados: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }


        }

    }
}

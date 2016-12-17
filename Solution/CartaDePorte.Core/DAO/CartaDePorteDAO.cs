using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CartaDePorte.Core.Domain;
using CartaDePorte.Core.Exception;
using CartaDePorte.Common;

namespace CartaDePorte.Core.DAO
{
    public class CartaDePorteDAO : BaseDAO
    {
        private static CartaDePorteDAO instance;
        public CartaDePorteDAO() { }

        public static CartaDePorteDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CartaDePorteDAO();
                }
                return instance;
            }
        }



        public CartasDePorte GetCartaDePorteDisponible(int idEstablecimientoOrigen)
        {

            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetCartaDePorteDisponible", idEstablecimientoOrigen, this.GetIdGrupoEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<CartasDePorte> result = new List<CartasDePorte>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        CartasDePorte cartasDePorte = new CartasDePorte();
                        cartasDePorte.IdCartaDePorte = Tools.Value2<int>(row["IdCartaDePorte"]);
                        cartasDePorte.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
                        cartasDePorte.NumeroCee = row["NumeroCee"].ToString();
                        cartasDePorte.IdLoteLoteCartasDePorte = Tools.Value2<int>(row["IdLoteLoteCartasDePorte"]);
                        cartasDePorte.Estado = (Enums.EstadoRangoCartaDePorte)Tools.Value2<int>(row["Estado"]);

                        result.Add(cartasDePorte);
                    }

                    return result.First();

                }
                else
                {
                    return new CartasDePorte();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get CartasDePorte: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }
        //[CantidadCartasDePorteDisponibles]
        public int CantidadCartasDePorteDisponibles()
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                Object resul = SqlHelper.ExecuteScalar(conn1, "CantidadCartasDePorteDisponibles", this.GetIdGrupoEmpresa());

                return Convert.ToInt32(resul);

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Guardar get CantidadCartasDePorteDisponibles: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }
        }

        public int AltaRangoCartaDePorte(string Desde, string Hasta, string NumeroCEE, DateTime FechaVencimiento, String establecimientoOrigen, String usuario, int Sucursal, int PtoEmision, DateTime FechaDesde, string HabilitacionNro)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                Object resul = SqlHelper.ExecuteScalar(conn1, "GuardarCartasDePorte", Desde, Hasta, NumeroCEE, FechaVencimiento, usuario, establecimientoOrigen, this.GetIdGrupoEmpresa(), Sucursal.ToString("000"), PtoEmision.ToString("000"), FechaDesde, HabilitacionNro == string.Empty ? "0" : HabilitacionNro);

                return Convert.ToInt32(resul);

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Guardar CartasDePorte: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }


        }

        public int SetEstadoCartaDePorte(int idCartaDePorte, Enums.EstadoRangoCartaDePorte estado)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                return SqlHelper.ExecuteNonQuery(conn1, "GuardarEstadoCartaDePorte", idCartaDePorte, Convert.ToInt32(estado), this.GetIdGrupoEmpresa());

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR SetEstadoCartaDePorte: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }


        }


        public IList<CartasDePorte> GetAll()
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetCartasDePorte", 0, this.GetIdGrupoEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<CartasDePorte> result = new List<CartasDePorte>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        CartasDePorte cdp = new CartasDePorte();
                        cdp.IdCartaDePorte = Convert.ToInt32(row["IdCartaDePorte"]);
                        cdp.NumeroCartaDePorte = row["NumeroCartaDePorte"].ToString();
                        cdp.NumeroCee = row["NumeroCee"].ToString();
                        cdp.IdLoteLoteCartasDePorte = Convert.ToInt32(row["IdLoteLoteCartasDePorte"]);
                        cdp.Estado = (Enums.EstadoRangoCartaDePorte)Convert.ToInt32(row["Estado"]);

                        result.Add(cdp);
                    }

                    return result;
                }
                else
                {
                    return new List<CartasDePorte>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get CartasDePorte: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }

        //ReservaCartaDePorte
        public int ReservaCartaDePorte(string Usuario, int idEstablecimiento, int idTipoCartaDePorte)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                Object resul = SqlHelper.ExecuteScalar(conn1, "ReservaCartaDePorte", Usuario, idEstablecimiento, idTipoCartaDePorte, this.GetIdGrupoEmpresa(), App.Usuario.IdEmpresa);

                return Convert.ToInt32(resul);

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Guardar ReservaCartaDePorte: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }


        }

        //CancelarReservaCartaDePorte
        public int CancelarReservaCartaDePorte(string NroCartaDePorte, string Usuario)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                Object resul = SqlHelper.ExecuteScalar(conn1, "CancelarReservaCartaDePorte", NroCartaDePorte, Usuario, this.GetIdGrupoEmpresa(), App.Usuario.IdEmpresa);

                return Convert.ToInt32(resul);

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Guardar CancelarReservaCartaDePorte: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }


        }
        public int AnularReservaCartaDePorte(string NroCartaDePorte, string Usuario)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                Object resul = SqlHelper.ExecuteScalar(conn1, "AnularReservaCartaDePorte", NroCartaDePorte, Usuario, this.GetIdGrupoEmpresa(), App.Usuario.IdEmpresa);

                return Convert.ToInt32(resul);

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Guardar AnularReservaCartaDePorte: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }


        }


        public int EliminarRangoCartaDePorte(int id, string usuario)
        {
            Object resul = null;
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);
                resul = SqlHelper.ExecuteNonQuery(conn1, "EliminarCartasDePorte", id, usuario);

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR en Eliminar RangoCartaDePorte: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

            return Convert.ToInt32(resul);

        }



        public int EliminarDisponiblesRangoCartaDePorte(int id, string usuario)
        {
            Object resul = null;
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);
                resul = SqlHelper.ExecuteNonQuery(conn1, "EliminarCartasDePorteDisponibles", id, usuario);

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR en Eliminar Disponibles RangoCartaDePorte: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

            return Convert.ToInt32(resul);

        }
    }
}

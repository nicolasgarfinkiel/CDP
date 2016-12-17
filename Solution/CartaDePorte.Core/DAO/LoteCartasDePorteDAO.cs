using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CartaDePorte.Core.Domain;
using CartaDePorte.Core.Exception;
using System.Reflection;
using CartaDePorte.Common;

namespace CartaDePorte.Core.DAO
{
    public class LoteCartasDePorteDAO : BaseDAO
    {

        private static LoteCartasDePorteDAO instance;
        public LoteCartasDePorteDAO() { }

        public static LoteCartasDePorteDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LoteCartasDePorteDAO();
                }
                return instance;
            }
        }

        public IList<LoteCartasDePorte> GetAll(int IdGrupoEmpresa)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetLoteCartasDePorte", IdGrupoEmpresa);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<LoteCartasDePorte> result = new List<LoteCartasDePorte>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        LoteCartasDePorte lcdp = new LoteCartasDePorte();
                        lcdp.IdLoteCartasDePorte = Convert.ToInt32(row["IdLoteCartasDePorte"]);
                        lcdp.Desde = Convert.ToInt32(row["Desde"]);
                        lcdp.Hasta = Convert.ToInt32(row["Hasta"]);
                        lcdp.Cee = row["Cee"].ToString();
                        lcdp.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
                        lcdp.FechaVencimiento = Convert.ToDateTime(row["FechaVencimiento"]);
                        lcdp.UsuarioCreacion = (row["UsuarioCreacion"] is System.DBNull) ? string.Empty : row["UsuarioCreacion"].ToString();
                        if (!(row["EstablecimientoOrigen"] is System.DBNull))
                            lcdp.EstablecimientoOrigen = EstablecimientoDAO.Instance.GetOne(Tools.Value2<int>(row["EstablecimientoOrigen"], 0));

                        lcdp.NumeroSucursal = Convert.ToInt32(row["Sucursal"].ToString() == string.Empty ? "0" : row["Sucursal"].ToString());
                        lcdp.PtoEmision = Convert.ToInt32(row["PuntoEmision"].ToString() == string.Empty ? "0" : row["PuntoEmision"].ToString());
                        if (!(row["FechaDesde"] is System.DBNull))
                            lcdp.FechaDesde = Convert.ToDateTime(row["FechaDesde"]);
                        result.Add(lcdp);
                    }
                    return result;
                }
                else
                {
                    return new List<LoteCartasDePorte>();
                }
            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get LoteCartasDePorte: " + ex.Message.ToString());
            }
            finally
            {
                conn1.Close();
            }
        }

        public LoteCartasDePorte GetOne(int IdLoteCartasDePorte)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetLoteCartasDePorte", IdLoteCartasDePorte);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<LoteCartasDePorte> result = new List<LoteCartasDePorte>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        LoteCartasDePorte lcdp = new LoteCartasDePorte();
                        lcdp.IdLoteCartasDePorte = Convert.ToInt32(row["IdLoteCartasDePorte"]);
                        lcdp.Desde = Convert.ToInt32(row["Desde"]);
                        lcdp.Hasta = Convert.ToInt32(row["Hasta"]);
                        lcdp.Cee = row["Cee"].ToString();
                        lcdp.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
                        lcdp.FechaVencimiento = Convert.ToDateTime(row["FechaVencimiento"]);
                        lcdp.UsuarioCreacion = (row["UsuarioCreacion"] is System.DBNull) ? string.Empty : row["UsuarioCreacion"].ToString();
                        if (!(row["EstablecimientoOrigen"] is System.DBNull))
                            lcdp.EstablecimientoOrigen = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["EstablecimientoOrigen"]));

                        result.Add(lcdp);
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
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Localidad: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }
        public DataTable GetAllReporteDataTable(DateTime fd, DateTime fh)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetLoteCartasDePorteReporte", fd, fh);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<LoteCartasDePorte> result = new List<LoteCartasDePorte>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        LoteCartasDePorte lcdp = new LoteCartasDePorte();
                        lcdp.IdLoteCartasDePorte = Convert.ToInt32(row["IdLoteCartasDePorte"]);
                        lcdp.Desde = Convert.ToInt32(row["Desde"]);
                        lcdp.Hasta = Convert.ToInt32(row["Hasta"]);
                        lcdp.Cee = row["Cee"].ToString();
                        lcdp.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
                        lcdp.FechaVencimiento = Convert.ToDateTime(row["FechaVencimiento"]);
                        lcdp.UsuarioCreacion = (row["UsuarioCreacion"] is System.DBNull) ? string.Empty : row["UsuarioCreacion"].ToString();
                        if (!(row["EstablecimientoOrigen"] is System.DBNull))
                            lcdp.EstablecimientoOrigen = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["EstablecimientoOrigen"]));

                        result.Add(lcdp);
                    }

                    if (result.Count > 0)
                        return GetDataTableFromIListGeneric(result).Tables[0];
                    else
                        return new DataTable();


                }
                else
                {
                    IList<LoteCartasDePorte> listaVacia = new List<LoteCartasDePorte>();
                    listaVacia.Add(new LoteCartasDePorte());

                    return GetDataTableFromIListGeneric(listaVacia).Tables[0];

                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get LoteCartasDePorte: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }





        public IList<LoteCartasDePorte> GetFiltro(int loteDesde, int tieneDisponible, int IdGrupoEmpresa)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetLoteCartasDePorteFiltro", loteDesde, tieneDisponible, IdGrupoEmpresa);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<LoteCartasDePorte> result = new List<LoteCartasDePorte>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        LoteCartasDePorte lcdp = new LoteCartasDePorte();
                        lcdp.IdLoteCartasDePorte = Convert.ToInt32(row["IdLoteCartasDePorte"]);
                        lcdp.Desde = Convert.ToInt32(row["Desde"]);
                        lcdp.Hasta = Convert.ToInt32(row["Hasta"]);
                        lcdp.Cee = row["Cee"].ToString();
                        lcdp.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
                        lcdp.FechaVencimiento = Convert.ToDateTime(row["FechaVencimiento"]);
                        lcdp.UsuarioCreacion = (row["UsuarioCreacion"] is System.DBNull) ? string.Empty : row["UsuarioCreacion"].ToString();
                        if (!(row["EstablecimientoOrigen"] is System.DBNull))
                            lcdp.EstablecimientoOrigen = EstablecimientoDAO.Instance.GetOne(Tools.Value2<int>(row["EstablecimientoOrigen"], 0));

                        result.Add(lcdp);
                    }

                    return result;
                }
                else
                {
                    return new List<LoteCartasDePorte>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get LoteCartasDePorte: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }





        public IList<LoteCartasDePorte> GetAllReporte(DateTime fd, DateTime fh)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetLoteCartasDePorteReporte", fd, fh);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<LoteCartasDePorte> result = new List<LoteCartasDePorte>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        LoteCartasDePorte lcdp = new LoteCartasDePorte();
                        lcdp.IdLoteCartasDePorte = Convert.ToInt32(row["IdLoteCartasDePorte"]);
                        lcdp.Desde = Convert.ToInt32(row["Desde"]);
                        lcdp.Hasta = Convert.ToInt32(row["Hasta"]);
                        lcdp.Cee = row["Cee"].ToString();
                        lcdp.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
                        lcdp.FechaVencimiento = Convert.ToDateTime(row["FechaVencimiento"]);
                        lcdp.UsuarioCreacion = (row["UsuarioCreacion"] is System.DBNull) ? string.Empty : row["UsuarioCreacion"].ToString();
                        if (!(row["EstablecimientoOrigen"] is System.DBNull))
                            lcdp.EstablecimientoOrigen = EstablecimientoDAO.Instance.GetOne(Convert.ToInt32(row["EstablecimientoOrigen"]));

                        result.Add(lcdp);
                    }

                    return result;
                }
                else
                {
                    return new List<LoteCartasDePorte>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get LoteCartasDePorte: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }




        public int GetDisponiblePorLote(int idLote)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                Object resul = SqlHelper.ExecuteScalar(conn1, "GetDisponiblePorLote", idLote);

                return Convert.ToInt32(resul);

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Guardar Get DisponiblePorLote: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }


        }



        public int GetUtilizadoPorLote(int idLote)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                Object resul = SqlHelper.ExecuteScalar(conn1, "GetUtilizadoPorLote", idLote);

                return Convert.ToInt32(resul);

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Guardar Get UtilizadoPorLote: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }


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
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CartaDePorte.Core.Exception;
using CartaDePorte.Core.Domain;

namespace CartaDePorte.Core.DAO
{
    public class CosechaDAO : BaseDAO
    {
         
        private static CosechaDAO instance;
        public CosechaDAO() { }

        public static CosechaDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CosechaDAO();
                }
                return instance;
            }
        }
        public int SaveOrUpdate(Cosecha cosecha)
        {
            Object resul = null;
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                resul = SqlHelper.ExecuteNonQuery(conn1, "GuardarCosecha", cosecha.IdCosecha, cosecha.Codigo, cosecha.Descripcion, App.ID_GRUPO_CRESUD);

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Guardar Cosecha: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

            return Convert.ToInt32(resul);

        }
        public IList<Cosecha> GetAll()
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetCosecha", 0, App.ID_GRUPO_CRESUD);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Cosecha> result = new List<Cosecha>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Cosecha cosecha = new Cosecha();
                        cosecha.IdCosecha = Convert.ToInt32(row["IdCosecha"]);
                        cosecha.Codigo = row["Codigo"].ToString();
                        cosecha.Descripcion = row["Descripcion"].ToString();

                        result.Add(cosecha);
                    }

                    return result;
                }
                else
                {
                    return new List<Cosecha>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Cosecha: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }

        public Cosecha GetOne(int IdCosecha)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetCosecha", IdCosecha, App.ID_GRUPO_CRESUD);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Cosecha> result = new List<Cosecha>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Cosecha cosecha = new Cosecha();
                        cosecha.IdCosecha = Convert.ToInt32(row["IdCosecha"]);
                        cosecha.Codigo = row["Codigo"].ToString();
                        cosecha.Descripcion = row["Descripcion"].ToString();

                        result.Add(cosecha);
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
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Cosecha: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }
        public Cosecha GetOneByCodigo(string codigo)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetCosechaByCodigo", codigo);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Cosecha> result = new List<Cosecha>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Cosecha cosecha = new Cosecha();
                        cosecha.IdCosecha = Convert.ToInt32(row["IdCosecha"]);
                        cosecha.Codigo = row["Codigo"].ToString();
                        cosecha.Descripcion = row["Descripcion"].ToString();

                        result.Add(cosecha);
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
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Get Cosecha By Codigo: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }



    }
}

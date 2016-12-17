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
    public class TipoGranoDAO : BaseDAO
    {

        private static TipoGranoDAO instance;
        public TipoGranoDAO() { }

        public static TipoGranoDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TipoGranoDAO();
                }
                return instance;
            }
        }

        public IList<TipoGrano> GetAll()
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetTipoGrano", 0);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<TipoGrano> result = new List<TipoGrano>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        TipoGrano tipoGrano = new TipoGrano();
                        tipoGrano.IdTipoGrano = Convert.ToInt32(row["IdTipoGrano"]);
                        tipoGrano.Descripcion = row["Descripcion"].ToString();

                        result.Add(tipoGrano);
                    }

                    return result;
                }
                else
                {
                    return new List<TipoGrano>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get TipoGrano: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }
        public TipoGrano GetOne(int IdTipoGrano)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetTipoGrano", IdTipoGrano);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<TipoGrano> result = new List<TipoGrano>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        TipoGrano tipoGrano = new TipoGrano();
                        tipoGrano.IdTipoGrano = Convert.ToInt32(row["IdTipoGrano"]);
                        tipoGrano.Descripcion = row["Descripcion"].ToString();

                        result.Add(tipoGrano);
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
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get TipoGrano: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }
    }
}

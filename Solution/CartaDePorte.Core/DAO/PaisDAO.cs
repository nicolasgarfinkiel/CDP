using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CartaDePorte.Core.Domain;
using System.Data.SqlClient;
using System.Data;
using CartaDePorte.Core.Exception;
using CartaDePorte.Common;

namespace CartaDePorte.Core.DAO
{
    public class PaisDAO : BaseDAO
    {
        private static PaisDAO instance;
        public PaisDAO() { }

        public static PaisDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PaisDAO();
                }
                return instance;
            }
        }

        public IList<Pais> GetAll()
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetPais", 0);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Pais> result = new List<Pais>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Pais Pais = new Pais();
                        Pais.IdPais = Convert.ToInt32(row["IdPais"]);
                        Pais.Descripcion = row["Descripcion"].ToString();
                        result.Add(Pais);
                    }
                    return result;
                }
                else
                {
                    return new List<Pais>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Pais: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }
        public Pais GetOne(int idPais)
        {
            if (idPais == 0)
                return new Pais();

            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetPais", idPais);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Pais> result = new List<Pais>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Pais Pais = new Pais();
                        Pais.IdPais = Convert.ToInt32(row["IdPais"]);
                        Pais.Descripcion = row["Descripcion"].ToString();

                        result.Add(Pais);
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
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Empresa: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }
        }
    }
}

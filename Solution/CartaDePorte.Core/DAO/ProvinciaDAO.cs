using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CartaDePorte.Core.Domain;
using System.Data.SqlClient;
using System.Data;
using CartaDePorte.Core.Exception;

namespace CartaDePorte.Core.DAO
{
    public class ProvinciaDAO : BaseDAO
    {

        private static ProvinciaDAO instance;
        public ProvinciaDAO() { }

        public static ProvinciaDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProvinciaDAO();
                }
                return instance;
            }
        }

        public IList<Provincia> GetAll()
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetProvincia", -1, App.Usuario.IdPais);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Provincia> result = new List<Provincia>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Provincia provincia = new Provincia();
                        provincia.Codigo = Convert.ToInt32(row["Codigo"]);
                        provincia.Descripcion = row["Descripcion"].ToString();

                        result.Add(provincia);
                    }

                    return result;
                }
                else
                {
                    return new List<Provincia>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Provincia: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }
        public Provincia GetOne(int idProvincia)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetProvincia", idProvincia, null);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Provincia> result = new List<Provincia>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Provincia provincia = new Provincia();
                        provincia.Codigo = Convert.ToInt32(row["Codigo"]);
                        provincia.Descripcion = row["Descripcion"].ToString();

                        result.Add(provincia);
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
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Provincia: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }

    }
}

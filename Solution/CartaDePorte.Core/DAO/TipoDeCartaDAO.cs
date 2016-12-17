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
    public class TipoDeCartaDAO : BaseDAO
    {
        private static TipoDeCartaDAO instance;
        public TipoDeCartaDAO() { }

        public static TipoDeCartaDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TipoDeCartaDAO();
                }
                return instance;
            }
        }


        public IList<TipoDeCarta> GetAll()
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetTipoDeCarta", 0);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<TipoDeCarta> result = new List<TipoDeCarta>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        TipoDeCarta tipoCartaDePorte = new TipoDeCarta();
                        tipoCartaDePorte.IdTipoDeCarta = Convert.ToInt32(row["IdTipoDeCarta"]);
                        tipoCartaDePorte.Descripcion = row["Descripcion"].ToString();
                        tipoCartaDePorte.Activo = Convert.ToBoolean(row["Activo"]);

                        result.Add(tipoCartaDePorte);
                    }

                    return result;
                }
                else
                {
                    return new List<TipoDeCarta>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get TipoDeCarta: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }
        public TipoDeCarta GetOne(int IdTipoDeCarta)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetTipoDeCarta", IdTipoDeCarta);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<TipoDeCarta> result = new List<TipoDeCarta>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        TipoDeCarta tipoCartaDePorte = new TipoDeCarta();
                        tipoCartaDePorte.IdTipoDeCarta = Convert.ToInt32(row["IdTipoDeCarta"]);
                        tipoCartaDePorte.Descripcion = row["Descripcion"].ToString();
                        tipoCartaDePorte.Activo = Convert.ToBoolean(row["Activo"]);

                        result.Add(tipoCartaDePorte);
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
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get TipoDeCarta: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }


    }
}

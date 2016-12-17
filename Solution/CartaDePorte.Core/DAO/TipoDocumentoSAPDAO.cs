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
    public class TipoDocumentoSAPDAO : BaseDAO
    {
        private static TipoDocumentoSAPDAO instance;
        public TipoDocumentoSAPDAO() { }

        public static TipoDocumentoSAPDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TipoDocumentoSAPDAO();
                }
                return instance;
            }
        }

        public IList<TipoDocumentoSAP> GetAll()
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetTipoDocumentoSAP", 0);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<TipoDocumentoSAP> result = new List<TipoDocumentoSAP>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        TipoDocumentoSAP tipoDocumentoSAP = new TipoDocumentoSAP();
                        tipoDocumentoSAP.IDTipoDocumentoSAP = Convert.ToInt32(row["IdTipoDocumentoSAP"]);
                        tipoDocumentoSAP.SAP_Id = row["SAP_Id"].ToString();
                        tipoDocumentoSAP.Nombre = row["Nombre"].ToString();

                        result.Add(tipoDocumentoSAP);
                    }
                    return result;
                }
                else
                {
                    return new List<TipoDocumentoSAP>();
                }
            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get TipoDocumentoSAP: " + ex.Message.ToString());
            }
            finally
            {
                conn1.Close();
            }
        }

        public TipoDocumentoSAP GetOne(int IdTipoDocumentoSAP)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetTipoDocumentoSAP", IdTipoDocumentoSAP);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<TipoDocumentoSAP> result = new List<TipoDocumentoSAP>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        TipoDocumentoSAP tipoDocumentoSAP = new TipoDocumentoSAP();
                        tipoDocumentoSAP.IDTipoDocumentoSAP = Convert.ToInt32(row["IdTipoDocumentoSAP"]);
                        tipoDocumentoSAP.SAP_Id = row["SAP_Id"].ToString();
                        tipoDocumentoSAP.Nombre = row["Nombre"].ToString();

                        result.Add(tipoDocumentoSAP);
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
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get TipoDocumentoSAP: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }
        }
    }
}

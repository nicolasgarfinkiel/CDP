using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CartaDePorte.Core.Domain;
using System.Data.SqlClient;
using CartaDePorte.Core.Exception;
using System.Data;

namespace CartaDePorte.Core.DAO
{
    public class EspecieDAO : BaseDAO
    {
        private static EspecieDAO instance;
        public EspecieDAO() { }

        public static EspecieDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EspecieDAO();
                }
                return instance;
            }
        }

        public int SaveOrUpdate(Especie especie)
        {
            Object resul = null;
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);
    
                resul = SqlHelper.ExecuteNonQuery(conn1, "GuardarEspecie", especie.IdEspecie, especie.Codigo, especie.Descripcion, this.GetIdGrupoEmpresa());

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Guardar Especie: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

            return Convert.ToInt32(resul);

        }



        public IList<Especie> GetAll()
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetEspecie", 0, this.GetIdGrupoEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Especie> result = new List<Especie>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Especie especie = new Especie();
                        especie.IdEspecie = Convert.ToInt32(row["IdEspecie"]);
                        especie.Codigo = Convert.ToInt32(row["Codigo"]);
                        especie.Descripcion = row["Descripcion"].ToString();

                        result.Add(especie);
                    }

                    return result;
                }
                else
                {
                    return new List<Especie>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Especie: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }
        public Especie GetOne(int IdEspecie)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetEspecie", IdEspecie, this.GetIdGrupoEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Especie> result = new List<Especie>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Especie especie = new Especie();
                        especie.IdEspecie = Convert.ToInt32(row["IdEspecie"]);
                        especie.Codigo = Convert.ToInt32(row["Codigo"]);
                        especie.Descripcion = row["Descripcion"].ToString();

                        result.Add(especie);
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
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Especie: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }
        public Especie GetOneByCodigo(int codigo)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetOneByCodigo", codigo, this.GetIdGrupoEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Especie> result = new List<Especie>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Especie especie = new Especie();
                        especie.IdEspecie = Convert.ToInt32(row["IdEspecie"]);
                        especie.Codigo = Convert.ToInt32(row["Codigo"]);
                        especie.Descripcion = row["Descripcion"].ToString();

                        result.Add(especie);
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
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Especie: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }


    }
}

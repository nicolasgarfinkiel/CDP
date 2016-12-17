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
    public class LocalidadDAO : BaseDAO
    {
        private static LocalidadDAO instance;
        public LocalidadDAO() { }

        public static LocalidadDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LocalidadDAO();
                }
                return instance;
            }
        }

        public IList<Localidad> GetAll()
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetLocalidad", 0);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Localidad> result = new List<Localidad>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Localidad localidad = new Localidad();
                        //localidad.Provincia = ProvinciaDAO.Instance.GetOne(Convert.ToInt32(row["IdProvincia"]));
                        localidad.Codigo = Convert.ToInt32(row["Codigo"]);
                        localidad.Descripcion = row["Descripcion"].ToString();
                        localidad.NombreProvincia = row["NombreProvincia"].ToString();
                        localidad.Provincia = new Provincia() { Codigo = Convert.ToInt32(row["idprovincia"]), Descripcion = row["NombreProvincia"].ToString() };

                        result.Add(localidad);
                    }

                    return result;
                }
                else
                {
                    return new List<Localidad>();
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
        public Localidad GetOne(int IdLocalidad)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetLocalidad", IdLocalidad);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Localidad> result = new List<Localidad>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Localidad localidad = new Localidad();
                        //localidad.Provincia = ProvinciaDAO.Instance.GetOne(Convert.ToInt32(row["IdProvincia"]));
                        localidad.Codigo = Convert.ToInt32(row["Codigo"]);
                        localidad.Descripcion = row["Descripcion"].ToString();
                        localidad.NombreProvincia = row["NombreProvincia"].ToString();

                        result.Add(localidad);
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

        public IList<Localidad> GetLocalidadByIDProvincia(int idProvincia)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetLocalidadByIDProvincia", idProvincia);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Localidad> result = new List<Localidad>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Localidad localidad = new Localidad();
                        localidad.Provincia = ProvinciaDAO.Instance.GetOne(Convert.ToInt32(row["IdProvincia"]));
                        localidad.Codigo = Convert.ToInt32(row["Codigo"]);
                        localidad.Descripcion = row["Descripcion"].ToString();
                        localidad.NombreProvincia = row["NombreProvincia"].ToString();

                        result.Add(localidad);
                    }

                    return result;
                }
                else
                {
                    return new List<Localidad>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Localidad por provincia: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }
        public IList<Localidad> GetLocalidadByFiltro(string filtro)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetLocalidadByFiltro", filtro);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Localidad> result = new List<Localidad>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Localidad localidad = new Localidad();
                        localidad.Provincia = ProvinciaDAO.Instance.GetOne(Convert.ToInt32(row["IdProvincia"]));
                        localidad.Codigo = Convert.ToInt32(row["Codigo"]);
                        localidad.Descripcion = row["Descripcion"].ToString();
                        localidad.NombreProvincia = row["NombreProvincia"].ToString();

                        result.Add(localidad);
                    }

                    return result;
                }
                else
                {
                    return new List<Localidad>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Localidad por provincia: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }

        public IList<Localidad> GetLocalidadByText(string filtro)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetLocalidadByText", filtro);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Localidad> result = new List<Localidad>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Localidad localidad = new Localidad();
                        //localidad.Provincia = ProvinciaDAO.Instance.GetOne(Convert.ToInt32(row["IdProvincia"]));
                        localidad.Codigo = Convert.ToInt32(row["Codigo"]);
                        localidad.Descripcion = row["Descripcion"].ToString();
                        localidad.NombreProvincia = row["NombreProvincia"].ToString();

                        result.Add(localidad);
                    }

                    return result;
                }
                else
                {
                    return new List<Localidad>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Localidad por provincia: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }



    }
}

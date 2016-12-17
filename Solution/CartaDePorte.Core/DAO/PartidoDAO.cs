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
    public class PartidoDAO : BaseDAO
    {
        private static PartidoDAO instance;
        public PartidoDAO() { }

        public static PartidoDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PartidoDAO();
                }
                return instance;
            }
        }

        public IList<Partido> GetAll()
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetPartido", 0);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Partido> result = new List<Partido>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Partido Partido = new Partido();
                        Partido.Provincia = ProvinciaDAO.Instance.GetOne(Convert.ToInt32(row["IdProvincia"]));
                        Partido.Codigo = Convert.ToInt32(row["Codigo"]);
                        Partido.Descripcion = row["Descripcion"].ToString();
                        Partido.NombreProvincia = row["NombreProvincia"].ToString();

                        result.Add(Partido);
                    }

                    return result;
                }
                else
                {
                    return new List<Partido>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Partido: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }
        public Partido GetOne(int IdPartido)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetPartido", IdPartido);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Partido> result = new List<Partido>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Partido Partido = new Partido();
                        //Partido.Provincia = ProvinciaDAO.Instance.GetOne(Convert.ToInt32(row["IdProvincia"]));
                        Partido.Codigo = Convert.ToInt32(row["Codigo"]);
                        Partido.Descripcion = row["Descripcion"].ToString();
                        Partido.NombreProvincia = row["NombreProvincia"].ToString();

                        result.Add(Partido);
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
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Partido: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }

        public IList<Partido> GetPartidoByIDProvincia(int idProvincia)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetPartidoByIDProvincia", idProvincia);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Partido> result = new List<Partido>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Partido Partido = new Partido();
                        Partido.Provincia = ProvinciaDAO.Instance.GetOne(Convert.ToInt32(row["IdProvincia"]));
                        Partido.Codigo = Convert.ToInt32(row["Codigo"]);
                        Partido.Descripcion = row["Descripcion"].ToString();
                        Partido.NombreProvincia = row["NombreProvincia"].ToString();

                        result.Add(Partido);
                    }

                    return result;
                }
                else
                {
                    return new List<Partido>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Partido por provincia: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }
        public IList<Partido> GetPartidoByFiltro(string filtro)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetPartidoByFiltro", filtro);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Partido> result = new List<Partido>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Partido Partido = new Partido();
                        Partido.Provincia = ProvinciaDAO.Instance.GetOne(Convert.ToInt32(row["IdProvincia"]));
                        Partido.Codigo = Convert.ToInt32(row["Codigo"]);
                        Partido.Descripcion = row["Descripcion"].ToString();
                        Partido.NombreProvincia = row["NombreProvincia"].ToString();

                        result.Add(Partido);
                    }

                    return result;
                }
                else
                {
                    return new List<Partido>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Partido por provincia: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }

        public IList<Partido> GetPartidoByText(string filtro)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetPartidoByText", filtro);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Partido> result = new List<Partido>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Partido Partido = new Partido();
                        //Partido.Provincia = ProvinciaDAO.Instance.GetOne(Convert.ToInt32(row["IdProvincia"]));
                        Partido.Codigo = Convert.ToInt32(row["Codigo"]);
                        Partido.Descripcion = row["Descripcion"].ToString();
                        Partido.NombreProvincia = row["NombreProvincia"].ToString();

                        result.Add(Partido);
                    }

                    return result;
                }
                else
                {
                    return new List<Partido>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Partido por provincia: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }



    }
}

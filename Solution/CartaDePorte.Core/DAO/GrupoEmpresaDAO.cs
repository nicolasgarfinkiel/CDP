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
    public class GrupoEmpresaDAO : BaseDAO
    {
        private static GrupoEmpresaDAO instance;
        public GrupoEmpresaDAO() { }

        public static GrupoEmpresaDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GrupoEmpresaDAO();
                }
                return instance;
            }
        }

        public IList<GrupoEmpresa> GetAll()
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetGrupoEmpresa", 0);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<GrupoEmpresa> result = new List<GrupoEmpresa>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        GrupoEmpresa GrupoEmpresa = new GrupoEmpresa();
                        GrupoEmpresa.IdGrupoEmpresa = Convert.ToInt32(row["IdGrupoEmpresa"]);
                        GrupoEmpresa.Descripcion = row["Descripcion"].ToString();
                        GrupoEmpresa.Activo = row["Activo"].ToString() == "0" ? true : false;
                        GrupoEmpresa.Pais = new PaisDAO().GetOne(Convert.ToInt32(row["IdPais"]));
                        GrupoEmpresa.IdApp = Convert.ToInt32(row["IdApp"]);

                        result.Add(GrupoEmpresa);
                    }
                    return result;
                }
                else
                {
                    return new List<GrupoEmpresa>();
                }
            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get GrupoEmpresa: " + ex.Message.ToString());
            }
            finally
            {
                conn1.Close();
            }
        }

        public IList<GrupoEmpresa> GetGrupoEmpresaEmpresa(int IdGrupoEmpresa, int IdEmpresa)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                string condicion = string.Empty;

                if (IdGrupoEmpresa != 0 && IdGrupoEmpresa != -1)
                    condicion = "GE.IdGrupoEmpresa = " + IdGrupoEmpresa.ToString();

                if (IdEmpresa != 0 && IdEmpresa != -1 && !string.IsNullOrEmpty(condicion))
                    condicion += "AND E.IdEmpresa = " + IdEmpresa.ToString();
                else if (IdEmpresa != 0 && IdEmpresa != -1)
                    condicion += "E.IdEmpresa = " + IdEmpresa.ToString();

                if (string.IsNullOrEmpty(condicion))
                    condicion += "1 = 1";

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetGrupoEmpresa&Empresa", condicion);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<GrupoEmpresa> result = new List<GrupoEmpresa>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        GrupoEmpresa GrupoEmpresa = new GrupoEmpresa();
                        GrupoEmpresa.IdGrupoEmpresa = Convert.ToInt32(row["IdGrupoEmpresa"]);
                        GrupoEmpresa.Descripcion = row["Descripcion"].ToString();
                        GrupoEmpresa.Activo = row["Activo"].ToString() == "0" ? true : false;
                        GrupoEmpresa.Pais = new PaisDAO().GetOne(Convert.ToInt32(row["IdPais"]));
                        GrupoEmpresa.IdApp = Convert.ToInt32(row["IdApp"]);
                        GrupoEmpresa.Empresa = new EmpresaDAO().GetOne(Convert.ToInt32(row["IdEmpresa"]));

                        result.Add(GrupoEmpresa);
                    }
                    return result;
                }
                else
                {
                    return new List<GrupoEmpresa>();
                }
            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get GrupoEmpresa&Empresa: " + ex.Message.ToString());
            }
            finally
            {
                conn1.Close();
            }
        }

        public GrupoEmpresa GetOne(int idGrupoEmpresa)
        {
            if (idGrupoEmpresa == 0)
                return new GrupoEmpresa();

            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetGrupoEmpresa", idGrupoEmpresa);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<GrupoEmpresa> result = new List<GrupoEmpresa>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        GrupoEmpresa GrupoEmpresa = new GrupoEmpresa();

                        GrupoEmpresa.IdGrupoEmpresa = Convert.ToInt32(row["IdGrupoEmpresa"]);
                        GrupoEmpresa.Descripcion = row["Descripcion"].ToString();
                        GrupoEmpresa.Activo = row["Activo"].ToString() == "0" ? true : false;
                        GrupoEmpresa.Pais = new PaisDAO().GetOne(Convert.ToInt32(row["IdPais"]));
                        GrupoEmpresa.IdApp = Convert.ToInt32(row["IdApp"]);

                        result.Add(GrupoEmpresa);
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
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get GrupoEmpresa: " + ex.Message.ToString());
            }
            finally
            {
                conn1.Close();
            }
        }

        public IList<GrupoEmpresa> GetOneByDescripcion(string DescripcionGrupoEmpresa)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);
                IList<GrupoEmpresa> result = new List<GrupoEmpresa>();

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetGrupoEmpresaByDescripcion", DescripcionGrupoEmpresa);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        GrupoEmpresa GrupoEmpresa = new GrupoEmpresa();
                        GrupoEmpresa.IdGrupoEmpresa = Convert.ToInt32(row["IdGrupoEmpresa"]);
                        GrupoEmpresa.Descripcion = row["Descripcion"].ToString();
                        GrupoEmpresa.Activo = row["Activo"].ToString() == "0" ? true : false;
                        GrupoEmpresa.Pais = new PaisDAO().GetOne(Convert.ToInt32(row["IdPais"]));
                        GrupoEmpresa.IdApp = Convert.ToInt32(row["IdApp"]);

                        result.Add(GrupoEmpresa);
                    }
                    return result;
                }
                else
                    return result;
            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR GetGrupoEmpresaByDescripcion: " + ex.Message.ToString());
            }
            finally
            {
                conn1.Close();
            }
        }

        public int SaveOrUpdate(GrupoEmpresa GrupoEmpresa)
        {
            Object resul = null;
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                if (GrupoEmpresa.IdGrupoEmpresa > 0)
                    resul = SqlHelper.ExecuteNonQuery(conn1, "GuardarGrupoEmpresa", GrupoEmpresa.IdGrupoEmpresa, GrupoEmpresa.Descripcion, GrupoEmpresa.Activo, GrupoEmpresa.IdPais, GrupoEmpresa.IdApp);
                else
                    resul = SqlHelper.ExecuteScalar(conn1, "GuardarGrupoEmpresa", GrupoEmpresa.IdGrupoEmpresa, GrupoEmpresa.Descripcion, GrupoEmpresa.Activo, GrupoEmpresa.IdPais, GrupoEmpresa.IdApp);
            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Guardar GrupoEmpresa: " + ex.Message.ToString());
            }
            finally
            {
                conn1.Close();
            }
            return Convert.ToInt32(resul);
        }
    }
}

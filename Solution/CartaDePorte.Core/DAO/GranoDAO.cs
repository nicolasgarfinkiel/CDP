using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using CartaDePorte.Core;
using CartaDePorte.Core.Domain;
using CartaDePorte.Core.Exception;

namespace CartaDePorte.Core.DAO
{
    public class GranoDAO : BaseDAO
    {

        private static GranoDAO instance;
        public GranoDAO() { }

        public static GranoDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GranoDAO();
                }
                return instance;
            }
        }

        public int SaveOrUpdate(Grano grano)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                int idEspecie = 0;
                int idCosecha = 0;

                if (grano.EspecieAfip != null)
                    idEspecie = grano.EspecieAfip.IdEspecie;
                if (grano.CosechaAfip != null)
                    idCosecha = grano.CosechaAfip.IdCosecha;

                if (grano.IdGrano > 0)
                    return SqlHelper.ExecuteNonQuery(conn1, "GuardarGrano", grano.IdGrano, grano.Descripcion, grano.IdMaterialSap, idEspecie, idCosecha, (grano.TipoGrano != null) ? grano.TipoGrano.IdTipoGrano : 0, grano.SujetoALote, grano.UsuarioModificacion, this.GetIdGrupoEmpresa());
                else
                    return SqlHelper.ExecuteNonQuery(conn1, "GuardarGrano", grano.IdGrano, grano.Descripcion, grano.IdMaterialSap, idEspecie, idCosecha, (grano.TipoGrano != null) ? grano.TipoGrano.IdTipoGrano : 0, grano.SujetoALote, grano.UsuarioCreacion, this.GetIdGrupoEmpresa());

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Guardar Grano: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }


        public IList<Grano> GetAll()
        {
            return this.GetAll(true);
        }
        public IList<Grano> GetAll(bool fullLoad)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetGrano", 0, this.GetIdGrupoEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Grano> result = new List<Grano>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Grano grano = new Grano();
                        grano.IdGrano = Convert.ToInt32(row["IdGrano"]);
                        grano.Descripcion = row["Descripcion"].ToString();
                        grano.IdMaterialSap = row["IdMaterialSap"].ToString();

                        grano.CosechaAfip = CosechaDAO.Instance.GetOne(Convert.ToInt32(row["IdCosechaAfip"]));

                        if (fullLoad)
                        {
                            grano.EspecieAfip = EspecieDAO.Instance.GetOne(Convert.ToInt32(row["IdEspecieAfip"]));
                            if (!(row["IdTipoGrano"] is System.DBNull))
                                grano.TipoGrano = TipoGranoDAO.Instance.GetOne(Convert.ToInt32(row["IdTipoGrano"]));
                        }

                        grano.SujetoALote = row["SujetoALote"].ToString();

                        grano.UsuarioCreacion = row["UsuarioCreacion"].ToString();
                        grano.UsuarioModificacion = row["UsuarioModificacion"].ToString();

                        if (!(row["FechaCreacion"] is System.DBNull))
                            grano.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);

                        if (!(row["FechaModificacion"] is System.DBNull))
                            grano.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);

                        result.Add(grano);
                    }

                    return result;
                }
                else
                {
                    return new List<Grano>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Grano: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }

        public IList<Grano> GetAllDistinct()
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetGrano", 0, this.GetIdGrupoEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Grano> result = new List<Grano>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Grano grano = new Grano();
                        grano.IdGrano = Convert.ToInt32(row["IdGrano"]);
                        grano.Descripcion = row["Descripcion"].ToString();

                        int Count = Convert.ToInt32(result.Where(g => g.Descripcion == grano.Descripcion).ToList().Count);
                        if (Count == 0)
                            result.Add(grano);
                    }

                    return result;
                }
                else
                {
                    return new List<Grano>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Grano: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }

        public Grano GetOne(int IdGrano)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetGrano", IdGrano, this.GetIdGrupoEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Grano> result = new List<Grano>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Grano grano = new Grano();
                        grano.IdGrano = Convert.ToInt32(row["IdGrano"]);
                        grano.Descripcion = row["Descripcion"].ToString();
                        grano.IdMaterialSap = row["IdMaterialSap"].ToString();
                        grano.EspecieAfip = EspecieDAO.Instance.GetOne(Convert.ToInt32(row["IdEspecieAfip"]));
                        grano.CosechaAfip = CosechaDAO.Instance.GetOne(Convert.ToInt32(row["IdCosechaAfip"]));

                        if (!(row["IdTipoGrano"] is System.DBNull))
                            grano.TipoGrano = TipoGranoDAO.Instance.GetOne(Convert.ToInt32(row["IdTipoGrano"]));

                        grano.SujetoALote = row["SujetoALote"].ToString();

                        grano.UsuarioCreacion = row["UsuarioCreacion"].ToString();
                        grano.UsuarioModificacion = row["UsuarioModificacion"].ToString();

                        if (!(row["FechaCreacion"] is System.DBNull))
                            grano.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);

                        if (!(row["FechaModificacion"] is System.DBNull))
                            grano.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);

                        result.Add(grano);
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
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Grano: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }



        public IList<Grano> GetFiltro(string busqueda)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetGranoFiltro", busqueda, this.GetIdGrupoEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Grano> result = new List<Grano>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Grano grano = new Grano();
                        grano.IdGrano = Convert.ToInt32(row["IdGrano"]);
                        grano.Descripcion = row["Descripcion"].ToString();
                        grano.IdMaterialSap = row["IdMaterialSap"].ToString();
                        grano.EspecieAfip = EspecieDAO.Instance.GetOne(Convert.ToInt32(row["IdEspecieAfip"]));
                        grano.CosechaAfip = CosechaDAO.Instance.GetOne(Convert.ToInt32(row["IdCosechaAfip"]));

                        if (!(row["IdTipoGrano"] is System.DBNull))
                            grano.TipoGrano = TipoGranoDAO.Instance.GetOne(Convert.ToInt32(row["IdTipoGrano"]));

                        grano.SujetoALote = row["SujetoALote"].ToString();

                        grano.UsuarioCreacion = row["UsuarioCreacion"].ToString();
                        grano.UsuarioModificacion = row["UsuarioModificacion"].ToString();

                        if (!(row["FechaCreacion"] is System.DBNull))
                            grano.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);

                        if (!(row["FechaModificacion"] is System.DBNull))
                            grano.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);

                        result.Add(grano);
                    }

                    return result;
                }
                else
                {
                    return new List<Grano>();
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

        public int EliminarGrano(int id, string usuario)
        {
            Object resul = null;
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);
                resul = SqlHelper.ExecuteNonQuery(conn1, "EliminarGrano", id, usuario);

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR en Eliminar rGrano: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

            return Convert.ToInt32(resul);

        }


    }
}

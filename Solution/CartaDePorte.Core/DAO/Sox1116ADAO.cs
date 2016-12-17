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
    public class Sox1116ADAO : BaseDAO
    {

        private static Sox1116ADAO instance;
        public Sox1116ADAO() { }

        public static Sox1116ADAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Sox1116ADAO();
                }
                return instance;
            }
        }

        public int SaveOrUpdate(Sox1116A entidad)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                if (entidad.IdCartaDePorte1116A > 0)
                    return SqlHelper.ExecuteNonQuery(conn1, "GuardarSox1116A", entidad.IdCartaDePorte1116A, entidad.Solicitud.IdSolicitud, entidad.Numero1116A, entidad.Fecha1116A, entidad.UsuarioModificacion);
                else
                    return SqlHelper.ExecuteNonQuery(conn1, "GuardarSox1116A", entidad.IdCartaDePorte1116A, entidad.Solicitud.IdSolicitud, entidad.Numero1116A, entidad.Fecha1116A, entidad.UsuarioCreacion);

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Guardar 1116A: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }
        public IList<Sox1116A> GetAll()
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSox1116A", 0);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Sox1116A> result = new List<Sox1116A>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Sox1116A entidad = new Sox1116A();
                        entidad.IdCartaDePorte1116A = Convert.ToInt32(row["IdCartaDePorte1116A"]);
                        entidad.Solicitud = SolicitudDAO.Instance.GetOne(Convert.ToInt32(row["IdSolicitud"]));

                        entidad.Numero1116A = row["Numero1116A"].ToString();

                        if (!(row["Fecha1116A"] is System.DBNull))
                            entidad.Fecha1116A = Convert.ToDateTime(row["Fecha1116A"]);

                        entidad.UsuarioCreacion = row["UsuarioCreacion"].ToString();
                        entidad.UsuarioModificacion = row["UsuarioModificacion"].ToString();

                        if (!(row["FechaCreacion"] is System.DBNull))
                            entidad.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);

                        if (!(row["FechaModificacion"] is System.DBNull))
                            entidad.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);

                        result.Add(entidad);
                    }

                    return result;
                }
                else
                {
                    return new List<Sox1116A>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get 1116A: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }
        public Sox1116A GetOne(int id)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSox1116A", id);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Sox1116A> result = new List<Sox1116A>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Sox1116A entidad = new Sox1116A();
                        entidad.IdCartaDePorte1116A = Convert.ToInt32(row["IdCartaDePorte1116A"]);
                        entidad.Solicitud = SolicitudDAO.Instance.GetOne(Convert.ToInt32(row["IdSolicitud"]));

                        entidad.Numero1116A = row["Numero1116A"].ToString();
                        if (!(row["Fecha1116A"] is System.DBNull))
                            entidad.Fecha1116A = Convert.ToDateTime(row["Fecha1116A"]);

                        entidad.UsuarioCreacion = row["UsuarioCreacion"].ToString();
                        entidad.UsuarioModificacion = row["UsuarioModificacion"].ToString();

                        if (!(row["FechaCreacion"] is System.DBNull))
                            entidad.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);

                        if (!(row["FechaModificacion"] is System.DBNull))
                            entidad.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);

                        result.Add(entidad);
                    }

                    if(result.Count > 0)
                        return result.First();
                    else
                        return new Sox1116A();

                }
                else
                {
                    return new Sox1116A();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR GetOne 1116A: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }


        }

        public Sox1116A GetOneByIdSoclicitud(int id)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetSox1116AByIdSolicitud", id);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Sox1116A> result = new List<Sox1116A>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Sox1116A entidad = new Sox1116A();
                        entidad.IdCartaDePorte1116A = Convert.ToInt32(row["IdCartaDePorte1116A"]);
                        entidad.Solicitud = SolicitudDAO.Instance.GetOne(Convert.ToInt32(row["IdSolicitud"]));

                        entidad.Numero1116A = row["Numero1116A"].ToString();
                        if (!(row["Fecha1116A"] is System.DBNull))
                            entidad.Fecha1116A = Convert.ToDateTime(row["Fecha1116A"]);

                        entidad.UsuarioCreacion = row["UsuarioCreacion"].ToString();
                        entidad.UsuarioModificacion = row["UsuarioModificacion"].ToString();

                        if (!(row["FechaCreacion"] is System.DBNull))
                            entidad.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);

                        if (!(row["FechaModificacion"] is System.DBNull))
                            entidad.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);

                        result.Add(entidad);
                    }

                    if (result.Count > 0)
                        return result.First();
                    else
                        return new Sox1116A();

                }
                else
                {
                    return new Sox1116A();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR GetOne 1116A: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }


        }

        public int Eliminar(int id, string usuario)
        {
            Object resul = null;
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);
                resul = SqlHelper.ExecuteNonQuery(conn1, "Eliminar1116A", id, usuario);

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR en Eliminar 1116A: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

            return Convert.ToInt32(resul);

        }


    }
}

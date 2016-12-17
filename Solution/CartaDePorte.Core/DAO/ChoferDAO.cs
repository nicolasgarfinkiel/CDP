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
    public class ChoferDAO : BaseDAO
    {
        private static ChoferDAO instance;
        public ChoferDAO() { }

        public static ChoferDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ChoferDAO();
                }
                return instance;
            }
        }

        public int SaveOrUpdate(Chofer chofer)
        {
            Object resul = null;
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                if (chofer.IdChofer > 0)
                    resul = SqlHelper.ExecuteNonQuery(conn1, "GuardarChofer", chofer.IdChofer, chofer.Nombre, chofer.Apellido, chofer.Cuit, chofer.Camion, chofer.Acoplado, chofer.UsuarioModificacion, chofer.EsChoferTransportista, this.GetIdGrupoEmpresa(), chofer.Domicilio, chofer.Marca);
                else
                    resul = SqlHelper.ExecuteScalar(conn1, "GuardarChofer", chofer.IdChofer, chofer.Nombre, chofer.Apellido, chofer.Cuit, chofer.Camion, chofer.Acoplado, chofer.UsuarioCreacion, chofer.EsChoferTransportista, this.GetIdGrupoEmpresa(), chofer.Domicilio, chofer.Marca);
            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Guardar Chofer: " + ex.Message.ToString());
            }
            finally
            {
                conn1.Close();
            }
            return Convert.ToInt32(resul);
        }

        public IList<Chofer> GetAll()
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetChofer", 0, this.GetIdGrupoEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Chofer> result = new List<Chofer>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Chofer chofer = new Chofer();
                        chofer.IdChofer = Convert.ToInt32(row["IdChofer"]);
                        chofer.Nombre = row["Nombre"].ToString();
                        chofer.Apellido = row["Apellido"].ToString();
                        chofer.Cuit = row["Cuit"].ToString();
                        chofer.Camion = row["Camion"].ToString();
                        chofer.Acoplado = row["Acoplado"].ToString();
                        chofer.UsuarioCreacion = row["UsuarioCreacion"].ToString();
                        chofer.EsChoferTransportista = (Convert.ToBoolean(row["EsChoferTransportista"])) ? Enums.EsChoferTransportista.Si : Enums.EsChoferTransportista.No;
                        chofer.Domicilio = row["Domicilio"].ToString();
                        chofer.Marca = row["Marca"].ToString();

                        if (!(row["FechaCreacion"] is System.DBNull))
                            chofer.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
                        chofer.UsuarioModificacion = row["UsuarioModificacion"].ToString();

                        if (!(row["FechaModificacion"] is System.DBNull))
                            chofer.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);

                        result.Add(chofer);
                    }
                    return result;
                }
                else
                {
                    return new List<Chofer>();
                }
            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Chofer: " + ex.Message.ToString());
            }
            finally
            {
                conn1.Close();
            }
        }

        public IList<Chofer> GetFiltro(string filtro)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetChoferFiltro", filtro, this.GetIdGrupoEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Chofer> result = new List<Chofer>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Chofer chofer = new Chofer();
                        chofer.IdChofer = Convert.ToInt32(row["IdChofer"]);
                        chofer.Nombre = row["Nombre"].ToString();
                        chofer.Apellido = row["Apellido"].ToString();
                        chofer.Cuit = row["Cuit"].ToString();
                        chofer.Camion = row["Camion"].ToString();
                        chofer.Acoplado = row["Acoplado"].ToString();
                        chofer.UsuarioCreacion = row["UsuarioCreacion"].ToString();
                        chofer.EsChoferTransportista = (Convert.ToBoolean(row["EsChoferTransportista"])) ? Enums.EsChoferTransportista.Si : Enums.EsChoferTransportista.No;

                        if (!(row["FechaCreacion"] is System.DBNull))
                            chofer.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
                        chofer.UsuarioModificacion = row["UsuarioModificacion"].ToString();

                        if (!(row["FechaModificacion"] is System.DBNull))
                            chofer.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);

                        result.Add(chofer);
                    }

                    return result;
                }
                else
                {
                    return new List<Chofer>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get ChoferFiltro: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }
        public Chofer GetOne(int IdChofer)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetChofer", IdChofer, this.GetIdGrupoEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Chofer> result = new List<Chofer>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Chofer chofer = new Chofer();
                        chofer.IdChofer = Convert.ToInt32(row["IdChofer"]);
                        chofer.Nombre = row["Nombre"].ToString();
                        chofer.Apellido = row["Apellido"].ToString();
                        chofer.Cuit = row["Cuit"].ToString();
                        chofer.Camion = row["Camion"].ToString();
                        chofer.Acoplado = row["Acoplado"].ToString();
                        chofer.UsuarioCreacion = row["UsuarioCreacion"].ToString();
                        chofer.EsChoferTransportista = (Convert.ToBoolean(row["EsChoferTransportista"])) ? Enums.EsChoferTransportista.Si : Enums.EsChoferTransportista.No;
                        chofer.Domicilio = row["Domicilio"].ToString();
                        chofer.Marca = row["Marca"].ToString();

                        if (!(row["FechaCreacion"] is System.DBNull))
                            chofer.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
                        chofer.UsuarioModificacion = row["UsuarioModificacion"].ToString();

                        if (!(row["FechaModificacion"] is System.DBNull))
                            chofer.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);

                        result.Add(chofer);
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
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Chofer: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }
        //[GetChoferByCuit]
        public Chofer GetChoferByCuit(string cuit)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetChoferByCuit", cuit, this.GetIdGrupoEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Chofer> result = new List<Chofer>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Chofer chofer = new Chofer();
                        chofer.IdChofer = Convert.ToInt32(row["IdChofer"]);
                        chofer.Nombre = row["Nombre"].ToString();
                        chofer.Apellido = row["Apellido"].ToString();
                        chofer.Cuit = row["Cuit"].ToString();
                        chofer.Camion = row["Camion"].ToString();
                        chofer.Acoplado = row["Acoplado"].ToString();
                        chofer.UsuarioCreacion = row["UsuarioCreacion"].ToString();
                        chofer.EsChoferTransportista = (Convert.ToBoolean(row["EsChoferTransportista"])) ? Enums.EsChoferTransportista.Si : Enums.EsChoferTransportista.No;

                        if (!(row["FechaCreacion"] is System.DBNull))
                            chofer.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
                        chofer.UsuarioModificacion = row["UsuarioModificacion"].ToString();

                        if (!(row["FechaModificacion"] is System.DBNull))
                            chofer.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);

                        result.Add(chofer);
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
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Chofer: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }
        public IList<Chofer> GetUsados(string busqueda)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);
                DataSet ds = new DataSet();


                switch (busqueda)
                {
                    case "Chofer":
                        ds = SqlHelper.ExecuteDataset(conn1, "GetChoferUsadas", this.GetIdGrupoEmpresa());
                        break;
                    default:
                        break;

                }



                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Chofer> result = new List<Chofer>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Chofer chofer = new Chofer();
                        chofer.IdChofer = Convert.ToInt32(row["IdChofer"]);
                        chofer.Nombre = row["Nombre"].ToString();
                        chofer.Apellido = row["Apellido"].ToString();
                        chofer.Cuit = row["Cuit"].ToString();
                        chofer.Camion = row["Camion"].ToString();
                        chofer.Acoplado = row["Acoplado"].ToString();
                        chofer.UsuarioCreacion = row["UsuarioCreacion"].ToString();
                        chofer.EsChoferTransportista = (Convert.ToBoolean(row["EsChoferTransportista"])) ? Enums.EsChoferTransportista.Si : Enums.EsChoferTransportista.No;

                        if (!(row["FechaCreacion"] is System.DBNull))
                            chofer.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
                        chofer.UsuarioModificacion = row["UsuarioModificacion"].ToString();

                        if (!(row["FechaModificacion"] is System.DBNull))
                            chofer.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);

                        result.Add(chofer);
                    }

                    return result;
                }
                else
                {
                    return new List<Chofer>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get ChoferFiltro: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }


        public int EliminarChofer(int id, string usuario)
        {
            Object resul = null;
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);
                resul = SqlHelper.ExecuteNonQuery(conn1, "EliminarChofer", id, usuario);

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR en Eliminar Chofer: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

            return Convert.ToInt32(resul);

        }


    }
}

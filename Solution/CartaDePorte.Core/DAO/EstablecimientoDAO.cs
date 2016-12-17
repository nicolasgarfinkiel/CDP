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
    public class EstablecimientoDAO : BaseDAO
    {
        private static EstablecimientoDAO instance;
        public EstablecimientoDAO() { }

        public static EstablecimientoDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EstablecimientoDAO();
                }
                return instance;
            }
        }

        public int SaveOrUpdate(Establecimiento establecimiento)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                if (establecimiento.IdEstablecimiento > 0)
                    return SqlHelper.ExecuteNonQuery(conn1, "GuardarEstablecimiento", establecimiento.IdEstablecimiento, establecimiento.Descripcion, establecimiento.Direccion, establecimiento.Localidad.Codigo, establecimiento.Provincia.Codigo, establecimiento.IDAlmacenSAP, establecimiento.IDCentroSAP, establecimiento.IdInterlocutorDestinatario.IdCliente, Convert.ToInt32(establecimiento.RecorridoEstablecimiento).ToString(), establecimiento.IDCEBE, establecimiento.IDExpedicion, establecimiento.EstablecimientoAfip , Convert.ToInt16(establecimiento.AsociaCartaDePorte) ,establecimiento.UsuarioModificacion, App.Usuario.IdEmpresa);
                else
                    return SqlHelper.ExecuteNonQuery(conn1, "GuardarEstablecimiento", establecimiento.IdEstablecimiento, establecimiento.Descripcion, establecimiento.Direccion, establecimiento.Localidad.Codigo, establecimiento.Provincia.Codigo, establecimiento.IDAlmacenSAP, establecimiento.IDCentroSAP, establecimiento.IdInterlocutorDestinatario.IdCliente, Convert.ToInt32(establecimiento.RecorridoEstablecimiento).ToString(), establecimiento.IDCEBE, establecimiento.IDExpedicion, establecimiento.EstablecimientoAfip, Convert.ToInt16(establecimiento.AsociaCartaDePorte), establecimiento.UsuarioCreacion, App.Usuario.IdEmpresa);

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Guardar Establecimiento: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }



        public IList<Establecimiento> GetAll()
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetEstablecimiento", 0, App.Usuario.IdEmpresa);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Establecimiento> result = new List<Establecimiento>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Establecimiento establecimiento = new Establecimiento();
                        establecimiento.Activo = Convert.ToInt32(row["Activo"]) == 0 ? false : true;
                        establecimiento.IdEstablecimiento = Convert.ToInt32(row["IdEstablecimiento"]);
                        establecimiento.Descripcion = row["Descripcion"].ToString();
                        establecimiento.Direccion = row["Direccion"].ToString();
                        establecimiento.Localidad = LocalidadDAO.Instance.GetOne(Convert.ToInt32(row["Localidad"]));
                        establecimiento.Provincia = ProvinciaDAO.Instance.GetOne(Convert.ToInt32(row["Provincia"]));
                        establecimiento.IDAlmacenSAP = (!String.IsNullOrEmpty(row["IDAlmacenSAP"].ToString())) ? Convert.ToInt32(row["IDAlmacenSAP"]) : 0;
                        establecimiento.IDCentroSAP = (!String.IsNullOrEmpty(row["IDCentroSAP"].ToString())) ? Convert.ToInt32(row["IDCentroSAP"]) : 0;
                        establecimiento.IdInterlocutorDestinatario = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdInterlocutorDestinatario"]));
                        establecimiento.RecorridoEstablecimiento = (Enums.RecorridoEstablecimiento)Convert.ToInt32(row["RecorridoEstablecimiento"]);
                        establecimiento.IDCEBE = row["IDCEBE"].ToString();
                        establecimiento.IDExpedicion = row["IDExpedicion"].ToString();
                        establecimiento.EstablecimientoAfip = row["EstablecimientoAfip"].ToString();
                        establecimiento.AsociaCartaDePorte = Convert.ToBoolean(row["AsociaCartaDePorte"]);

                        establecimiento.UsuarioCreacion = row["UsuarioCreacion"].ToString();
                        establecimiento.UsuarioModificacion = row["UsuarioModificacion"].ToString();
                        
                        if (!(row["FechaCreacion"] is System.DBNull))
                            establecimiento.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);                        

                        if (!(row["FechaModificacion"] is System.DBNull))
                            establecimiento.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);

                        result.Add(establecimiento);
                    }

                    return result;
                }
                else
                {
                    return new List<Establecimiento>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Establecimiento: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }
        public Establecimiento GetOne(int IdEstablecimiento)
        {
            if (IdEstablecimiento == 0)
                return new Establecimiento();

            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetEstablecimiento", IdEstablecimiento, null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Establecimiento> result = new List<Establecimiento>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Establecimiento establecimiento = new Establecimiento();
                        establecimiento.Activo = Convert.ToInt32(row["Activo"]) == 0 ? false : true;
                        establecimiento.IdEstablecimiento = Convert.ToInt32(row["IdEstablecimiento"]);
                        establecimiento.Descripcion = row["Descripcion"].ToString();
                        establecimiento.Direccion = row["Direccion"].ToString();
                        establecimiento.Localidad = LocalidadDAO.Instance.GetOne(Convert.ToInt32(row["Localidad"]));
                        establecimiento.Provincia = ProvinciaDAO.Instance.GetOne(Convert.ToInt32(row["Provincia"]));
                        establecimiento.IDAlmacenSAP = (!String.IsNullOrEmpty(row["IDAlmacenSAP"].ToString())) ? Convert.ToInt32(row["IDAlmacenSAP"]) : 0;
                        establecimiento.IDCentroSAP = (!String.IsNullOrEmpty(row["IDCentroSAP"].ToString())) ? Convert.ToInt32(row["IDCentroSAP"]) : 0;
                        establecimiento.IdInterlocutorDestinatario = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdInterlocutorDestinatario"]));
                        establecimiento.RecorridoEstablecimiento = (Enums.RecorridoEstablecimiento)Convert.ToInt32(row["RecorridoEstablecimiento"]);
                        establecimiento.IDCEBE = row["IDCEBE"].ToString();
                        establecimiento.IDExpedicion = row["IDExpedicion"].ToString();
                        establecimiento.EstablecimientoAfip = row["EstablecimientoAfip"].ToString();
                        establecimiento.AsociaCartaDePorte = Convert.ToBoolean(row["AsociaCartaDePorte"]);

                        establecimiento.UsuarioCreacion = row["UsuarioCreacion"].ToString();
                        establecimiento.UsuarioModificacion = row["UsuarioModificacion"].ToString();

                        if (!(row["FechaCreacion"] is System.DBNull))
                            establecimiento.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);

                        if (!(row["FechaModificacion"] is System.DBNull))
                            establecimiento.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);

                        result.Add(establecimiento);
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
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Establecimiento: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }


        public IList<Establecimiento> GetFiltro(string busqueda)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetEstablecimientoFiltro", busqueda, App.Usuario.IdEmpresa);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Establecimiento> result = new List<Establecimiento>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Establecimiento establecimiento = new Establecimiento();
                        establecimiento.Activo = Convert.ToInt32(row["Activo"]) == 0 ? false : true;
                        establecimiento.IdEstablecimiento = Convert.ToInt32(row["IdEstablecimiento"]);
                        establecimiento.Descripcion = row["Descripcion"].ToString();
                        establecimiento.Direccion = row["Direccion"].ToString();
                        establecimiento.Localidad = LocalidadDAO.Instance.GetOne(Convert.ToInt32(row["Localidad"]));
                        establecimiento.Provincia = ProvinciaDAO.Instance.GetOne(Convert.ToInt32(row["Provincia"]));
                        establecimiento.IDAlmacenSAP = (!String.IsNullOrEmpty(row["IDAlmacenSAP"].ToString())) ? Convert.ToInt32(row["IDAlmacenSAP"]) : 0;
                        establecimiento.IDCentroSAP = (!String.IsNullOrEmpty(row["IDCentroSAP"].ToString())) ? Convert.ToInt32(row["IDCentroSAP"]) : 0;
                        establecimiento.IdInterlocutorDestinatario = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdInterlocutorDestinatario"]));
                        establecimiento.RecorridoEstablecimiento = (Enums.RecorridoEstablecimiento)Convert.ToInt32(row["RecorridoEstablecimiento"]);
                        establecimiento.IDCEBE = row["IDCEBE"].ToString();
                        establecimiento.IDExpedicion = row["IDExpedicion"].ToString();
                        establecimiento.EstablecimientoAfip = row["EstablecimientoAfip"].ToString();
                        establecimiento.AsociaCartaDePorte = Convert.ToBoolean(row["AsociaCartaDePorte"]);

                        establecimiento.UsuarioCreacion = row["UsuarioCreacion"].ToString();
                        establecimiento.UsuarioModificacion = row["UsuarioModificacion"].ToString();

                        if (!(row["FechaCreacion"] is System.DBNull))
                            establecimiento.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);

                        if (!(row["FechaModificacion"] is System.DBNull))
                            establecimiento.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);

                        result.Add(establecimiento);
                    }

                    return result;
                }
                else
                {
                    return new List<Establecimiento>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Establecimiento: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }


        public IList<Establecimiento> GetEstablecimientoOrigenDestino(bool Origen)
        {
            return this.GetEstablecimientoOrigenDestino(Origen, true);
        }

        public IList<Establecimiento> GetEstablecimientoOrigenDestino(bool Origen, bool fullLoad)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetEstablecimientoOrigenDestino", Convert.ToInt32(Origen), App.Usuario.IdEmpresa);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Establecimiento> result = new List<Establecimiento>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Establecimiento establecimiento = new Establecimiento();
                        establecimiento.Activo = Convert.ToInt32(row["Activo"]) == 0 ? false : true;
                        establecimiento.IdEstablecimiento = Convert.ToInt32(row["IdEstablecimiento"]);
                        establecimiento.Descripcion = row["Descripcion"].ToString();
                        establecimiento.Direccion = row["Direccion"].ToString();
                        establecimiento.IDAlmacenSAP = (!String.IsNullOrEmpty(row["IDAlmacenSAP"].ToString())) ? Convert.ToInt32(row["IDAlmacenSAP"]) : 0;
                        establecimiento.IDCentroSAP = (!String.IsNullOrEmpty(row["IDCentroSAP"].ToString())) ? Convert.ToInt32(row["IDCentroSAP"]) : 0;
                        establecimiento.RecorridoEstablecimiento = (Enums.RecorridoEstablecimiento)Convert.ToInt32(row["RecorridoEstablecimiento"]);
                        establecimiento.IDCEBE = row["IDCEBE"].ToString();
                        establecimiento.IDExpedicion = row["IDExpedicion"].ToString();
                        establecimiento.EstablecimientoAfip = row["EstablecimientoAfip"].ToString();
                        establecimiento.AsociaCartaDePorte = Convert.ToBoolean(row["AsociaCartaDePorte"]);

                        establecimiento.UsuarioCreacion = row["UsuarioCreacion"].ToString();
                        establecimiento.UsuarioModificacion = row["UsuarioModificacion"].ToString();

                        if (fullLoad)
                        {
                            establecimiento.Localidad = LocalidadDAO.Instance.GetOne(Convert.ToInt32(row["Localidad"]));
                            establecimiento.Provincia = ProvinciaDAO.Instance.GetOne(Convert.ToInt32(row["Provincia"]));
                            establecimiento.IdInterlocutorDestinatario = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdInterlocutorDestinatario"]));
                        }

                        if (!(row["FechaCreacion"] is System.DBNull))
                            establecimiento.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);

                        if (!(row["FechaModificacion"] is System.DBNull))
                            establecimiento.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);

                        result.Add(establecimiento);
                    }

                    return result;
                }
                else
                {
                    return new List<Establecimiento>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Establecimiento: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }


        public int EliminarEstablecimiento(int id, string usuario)
        {
            Object resul = null;
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);
                resul = SqlHelper.ExecuteNonQuery(conn1, "EliminarEstablecimiento", id, usuario);

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR en Eliminar Establecimiento: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

            return Convert.ToInt32(resul);

        }





    }
}

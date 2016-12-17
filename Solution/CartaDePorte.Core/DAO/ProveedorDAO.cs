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
    public class ProveedorDAO : BaseDAO
    {

        private static ProveedorDAO instance;
        public ProveedorDAO() { }

        public static ProveedorDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProveedorDAO();
                }
                return instance;
            }
        }

        public int SaveOrUpdate(Proveedor proveedor)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                return SqlHelper.ExecuteNonQuery(conn1, "GuardarProveedor", proveedor.IdProveedor, proveedor.Sap_Id, proveedor.Nombre, (proveedor.TipoDocumento != null) ? proveedor.TipoDocumento.IDTipoDocumentoSAP : 0, proveedor.NumeroDocumento, proveedor.Calle, proveedor.Piso, proveedor.Departamento, proveedor.Numero, proveedor.CP, proveedor.Ciudad, proveedor.Pais, proveedor.Activo, proveedor.EsProspecto, proveedor.IdSapOrganizacionDeVenta, this.GetIdEmpresa());

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Guardar Proveedor: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }
        public Int64 getIdSapProspecto()
        {
            Int64 result = 0;
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "getIdSapProveedorProspecto", this.GetIdEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        result = Convert.ToInt64(row["ID"]);
                    }
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR getIdProspecto: " + ex.Message.ToString());
            }
            finally
            {
                conn1.Close();
            }

            return result;


        }
        public int SaveOrUpdateProspecto(Proveedor proveedor, Int64 idproveedorprospecto)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                return SqlHelper.ExecuteNonQuery(conn1, "GuardarUpdateProveedorProspecto", proveedor.IdProveedor, proveedor.Sap_Id, proveedor.Nombre, (proveedor.TipoDocumento != null) ? proveedor.TipoDocumento.IDTipoDocumentoSAP : 0, proveedor.NumeroDocumento, proveedor.Calle, proveedor.Piso, proveedor.Departamento, proveedor.Numero, proveedor.CP, proveedor.Ciudad, proveedor.Pais, proveedor.Activo, proveedor.EsProspecto, idproveedorprospecto, this.GetIdEmpresa());

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR GuardarUpdateProveedorProspecto: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }


        public IList<Proveedor> GetAll()
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetProveedor", 0, this.GetIdEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Proveedor> result = new List<Proveedor>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Proveedor proveedor = new Proveedor();
                        proveedor.IdProveedor = Convert.ToInt32(row["IdProveedor"]);
                        proveedor.Sap_Id = row["Sap_Id"].ToString();
                        proveedor.Nombre = row["Nombre"].ToString();
                        proveedor.TipoDocumento = TipoDocumentoSAPDAO.Instance.GetOne(Convert.ToInt32(row["IdTipoDocumentoSAP"]));
                        proveedor.NumeroDocumento = row["NumeroDocumento"].ToString();
                        proveedor.Calle = row["Calle"].ToString();
                        proveedor.Piso = row["Piso"].ToString();
                        proveedor.Departamento = row["Departamento"].ToString();
                        proveedor.Numero = row["Numero"].ToString();
                        proveedor.CP = row["CP"].ToString();
                        proveedor.Ciudad = row["Ciudad"].ToString();
                        proveedor.Pais = row["Pais"].ToString();
                        proveedor.Activo = Convert.ToBoolean(row["Activo"]);
                        proveedor.EsProspecto = Convert.ToBoolean(row["EsProspecto"]);

                        result.Add(proveedor);
                    }

                    return result;
                }
                else
                {
                    return new List<Proveedor>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Proveedor: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }
        public IList<Proveedor> GetFiltro(string busqueda)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetProveedorFiltro", busqueda, this.GetIdEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Proveedor> result = new List<Proveedor>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Proveedor proveedor = new Proveedor();
                        proveedor.IdProveedor = Convert.ToInt32(row["IdProveedor"]);
                        proveedor.Sap_Id = row["Sap_Id"].ToString();
                        proveedor.Nombre = row["Nombre"].ToString();
                        proveedor.TipoDocumento = TipoDocumentoSAPDAO.Instance.GetOne(Convert.ToInt32(row["IdTipoDocumentoSAP"]));
                        proveedor.NumeroDocumento = row["NumeroDocumento"].ToString();
                        proveedor.Calle = row["Calle"].ToString();
                        proveedor.Piso = row["Piso"].ToString();
                        proveedor.Departamento = row["Departamento"].ToString();
                        proveedor.Numero = row["Numero"].ToString();
                        proveedor.CP = row["CP"].ToString();
                        proveedor.Ciudad = row["Ciudad"].ToString();
                        proveedor.Pais = row["Pais"].ToString();
                        proveedor.Activo = Convert.ToBoolean(row["Activo"]);
                        proveedor.EsProspecto = Convert.ToBoolean(row["EsProspecto"]);

                        result.Add(proveedor);
                    }

                    return result;
                }
                else
                {
                    return new List<Proveedor>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Proveedor: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }
        public IList<Proveedor> GetProveedorByNumeroDocumento(string numeroDocumento)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetProveedorByNumeroDocumento", numeroDocumento, this.GetIdEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Proveedor> result = new List<Proveedor>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Proveedor proveedor = new Proveedor();
                        proveedor.IdProveedor = Convert.ToInt32(row["IdProveedor"]);
                        proveedor.Sap_Id = row["Sap_Id"].ToString();
                        proveedor.Nombre = row["Nombre"].ToString();
                        proveedor.TipoDocumento = TipoDocumentoSAPDAO.Instance.GetOne(Convert.ToInt32(row["IdTipoDocumentoSAP"]));
                        proveedor.NumeroDocumento = row["NumeroDocumento"].ToString();
                        proveedor.Calle = row["Calle"].ToString();
                        proveedor.Piso = row["Piso"].ToString();
                        proveedor.Departamento = row["Departamento"].ToString();
                        proveedor.Numero = row["Numero"].ToString();
                        proveedor.CP = row["CP"].ToString();
                        proveedor.Ciudad = row["Ciudad"].ToString();
                        proveedor.Pais = row["Pais"].ToString();
                        proveedor.Activo = Convert.ToBoolean(row["Activo"]);
                        proveedor.EsProspecto = Convert.ToBoolean(row["EsProspecto"]);

                        result.Add(proveedor);
                    }

                    return result;
                }
                else
                {
                    return new List<Proveedor>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Proveedor: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }
        //GetProveedorBySAPID
        public Proveedor GetOneBySAPID(string Sap_ID, string Sap_IdOrganizacionDeVenta)
        {

            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetProveedorBySAPID", Sap_ID, Sap_IdOrganizacionDeVenta);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Proveedor> result = new List<Proveedor>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Proveedor proveedor = new Proveedor();
                        proveedor.IdProveedor = Convert.ToInt32(row["IdProveedor"]);
                        proveedor.Sap_Id = row["Sap_Id"].ToString();
                        proveedor.Nombre = row["Nombre"].ToString();
                        proveedor.TipoDocumento = TipoDocumentoSAPDAO.Instance.GetOne(Tools.Value2<int>(row["IdTipoDocumentoSAP"], 0));
                        proveedor.NumeroDocumento = row["NumeroDocumento"].ToString();
                        proveedor.Calle = row["Calle"].ToString();
                        proveedor.Piso = row["Piso"].ToString();
                        proveedor.Departamento = row["Departamento"].ToString();
                        proveedor.Numero = row["Numero"].ToString();
                        proveedor.CP = row["CP"].ToString();
                        proveedor.Ciudad = row["Ciudad"].ToString();
                        proveedor.Pais = row["Pais"].ToString();
                        proveedor.Activo = Convert.ToBoolean(row["Activo"]);
                        proveedor.EsProspecto = Convert.ToBoolean(row["EsProspecto"]);
                        proveedor.IdSapOrganizacionDeVenta = Tools.Value2<int>(row["IdSapOrganizacionDeVenta"], 0);
                        result.Add(proveedor);
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
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get ProveedorBySapID: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }

        public IList<Proveedor> GetOneByCuitOrgSAPID(string NumeroDocumento)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetProveedorByCuitOrgSAPID", NumeroDocumento);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Proveedor> result = new List<Proveedor>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Proveedor proveedor = new Proveedor();
                        proveedor.IdProveedor = Convert.ToInt32(row["IdProveedor"]);
                        proveedor.Sap_Id = row["Sap_Id"].ToString();
                        proveedor.Nombre = row["Nombre"].ToString();
                        proveedor.TipoDocumento = TipoDocumentoSAPDAO.Instance.GetOne(Tools.Value2<int>(row["IdTipoDocumentoSAP"], 0));
                        proveedor.NumeroDocumento = row["NumeroDocumento"].ToString();
                        proveedor.Calle = row["Calle"].ToString();
                        proveedor.Piso = row["Piso"].ToString();
                        proveedor.Departamento = row["Departamento"].ToString();
                        proveedor.Numero = row["Numero"].ToString();
                        proveedor.CP = row["CP"].ToString();
                        proveedor.Ciudad = row["Ciudad"].ToString();
                        proveedor.Pais = row["Pais"].ToString();
                        proveedor.Activo = Convert.ToBoolean(row["Activo"]);
                        proveedor.EsProspecto = Convert.ToBoolean(row["EsProspecto"]);
                        proveedor.IdSapOrganizacionDeVenta = Tools.Value2<int>(row["IdSapOrganizacionDeVenta"], 0);
                        result.Add(proveedor);
                    }

                    if (result.Count > 0)
                        return result;
                    else
                        return null;

                }
                else
                    return null;
            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get GetProveedorByCuitOrgSAPID: " + ex.Message.ToString());
            }
            finally
            {
                conn1.Close();
            }
        }



        public Proveedor GetOne(int idProveedor)
        {
            if (idProveedor == 0)
                return new Proveedor();

            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetProveedor", idProveedor, this.GetIdEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Proveedor> result = new List<Proveedor>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Proveedor proveedor = new Proveedor();
                        proveedor.IdProveedor = Convert.ToInt32(row["IdProveedor"]);
                        proveedor.Sap_Id = row["Sap_Id"].ToString();
                        proveedor.Nombre = row["Nombre"].ToString();
                        proveedor.TipoDocumento = TipoDocumentoSAPDAO.Instance.GetOne(Convert.ToInt32(row["IdTipoDocumentoSAP"]));
                        proveedor.NumeroDocumento = row["NumeroDocumento"].ToString();
                        proveedor.Calle = row["Calle"].ToString();
                        proveedor.Piso = row["Piso"].ToString();
                        proveedor.Departamento = row["Departamento"].ToString();
                        proveedor.Numero = row["Numero"].ToString();
                        proveedor.CP = row["CP"].ToString();
                        proveedor.Ciudad = row["Ciudad"].ToString();
                        proveedor.Pais = row["Pais"].ToString();
                        proveedor.Activo = Convert.ToBoolean(row["Activo"]);
                        proveedor.EsProspecto = Convert.ToBoolean(row["EsProspecto"]);
                        proveedor.IdSapOrganizacionDeVenta = Tools.Value2<int>(row["IdSapOrganizacionDeVenta"], 0);

                        result.Add(proveedor);
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
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Proveedor: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }

        private static IList<Proveedor> combo;
        public static IList<Proveedor> Combo
        {
            get { return combo; }
            set { combo = value; }
        }

        public IList<Proveedor> GetStaticCombo()
        {
            if (Combo == null)
            {
                Combo = GetCombo();
            }
            return Combo;
        }



        public IList<Proveedor> GetCombo()
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetProveedor", 0, this.GetIdEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Proveedor> result = new List<Proveedor>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Proveedor proveedor = new Proveedor();
                        proveedor.IdProveedor = Convert.ToInt32(row["IdProveedor"]);
                        proveedor.Sap_Id = row["Sap_Id"].ToString();
                        proveedor.Nombre = row["Nombre"].ToString();
                        proveedor.TipoDocumento = null; // TipoDocumentoSAPDAO.Instance.Get(Convert.ToInt32(row["IdTipoDocumentoSAP"])).First();
                        proveedor.NumeroDocumento = row["NumeroDocumento"].ToString();
                        proveedor.Calle = row["Calle"].ToString();
                        proveedor.Piso = row["Piso"].ToString();
                        proveedor.Departamento = row["Departamento"].ToString();
                        proveedor.Numero = row["Numero"].ToString();
                        proveedor.CP = row["CP"].ToString();
                        proveedor.Ciudad = row["Ciudad"].ToString();
                        proveedor.Pais = row["Pais"].ToString();
                        proveedor.Activo = Convert.ToBoolean(row["Activo"]);
                        proveedor.EsProspecto = Convert.ToBoolean(row["EsProspecto"]);

                        result.Add(proveedor);
                    }

                    return result;
                }
                else
                {
                    return new List<Proveedor>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Proveedor: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }



        public IList<Proveedor> GetUsados(string busqueda)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);
                DataSet ds = new DataSet();

                switch (busqueda)
                {
                    case "ProveedorTransportista":
                        ds = SqlHelper.ExecuteDataset(conn1, "GetProveedorTransportistaUsadas", this.GetIdEmpresa());
                        break;
                    case "ProveedorTitularCartaDePorte":
                        ds = SqlHelper.ExecuteDataset(conn1, "GetProveedorTitularCartaDePorteUsadas", this.GetIdEmpresa());
                        break;
                    default:
                        break;

                }


                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Proveedor> result = new List<Proveedor>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Proveedor proveedor = new Proveedor();
                        proveedor.IdProveedor = Convert.ToInt32(row["IdProveedor"]);
                        proveedor.Sap_Id = row["Sap_Id"].ToString();
                        proveedor.Nombre = row["Nombre"].ToString();
                        proveedor.TipoDocumento = TipoDocumentoSAPDAO.Instance.GetOne(Convert.ToInt32(row["IdTipoDocumentoSAP"]));
                        proveedor.NumeroDocumento = row["NumeroDocumento"].ToString();
                        proveedor.Calle = row["Calle"].ToString();
                        proveedor.Piso = row["Piso"].ToString();
                        proveedor.Departamento = row["Departamento"].ToString();
                        proveedor.Numero = row["Numero"].ToString();
                        proveedor.CP = row["CP"].ToString();
                        proveedor.Ciudad = row["Ciudad"].ToString();
                        proveedor.Pais = row["Pais"].ToString();
                        proveedor.Activo = Convert.ToBoolean(row["Activo"]);
                        proveedor.EsProspecto = Convert.ToBoolean(row["EsProspecto"]);

                        result.Add(proveedor);
                    }

                    return result;
                }
                else
                {
                    return new List<Proveedor>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Proveedor: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }

    }
}

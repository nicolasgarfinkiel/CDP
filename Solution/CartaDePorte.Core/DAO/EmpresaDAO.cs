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
    public class EmpresaDAO : BaseDAO
    {
        
        private static EmpresaDAO instance;
        public EmpresaDAO() { }

        public static EmpresaDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EmpresaDAO();
                }
                return instance;
            }
        }


        public IList<Empresa> GetAll()
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetEmpresa", 0);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Empresa> result = new List<Empresa>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Empresa empresa = new Empresa();
                        empresa.IdEmpresa = Convert.ToInt32(row["IdEmpresa"]);
                        empresa.Cliente = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdCliente"]));
                        empresa.Descripcion = row["Descripcion"].ToString();
                        empresa.IdSapOrganizacionDeVenta = row["IdSapOrganizacionDeVenta"].ToString();
                        empresa.IdSapSector = row["IdSapSector"].ToString();
                        empresa.IdSapCanalLocal = row["IdSapCanalLocal"].ToString();
                        empresa.IdSapCanalExpor = row["IdSapCanalExpor"].ToString();
                        empresa.Sap_Id = row["Sap_Id"].ToString();
                        empresa.IdSapMoneda = row["IdSapMoneda"].ToString();

                        result.Add(empresa);
                    }

                    return result;
                }
                else
                {
                    return new List<Empresa>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Empresa: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }
        }

        public Empresa GetOne(int idEmpresa)
        {
            if (idEmpresa == 0)
                return new Empresa();

            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetEmpresa", idEmpresa);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Empresa> result = new List<Empresa>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Empresa empresa = new Empresa();
                        empresa.IdEmpresa = Convert.ToInt32(row["IdEmpresa"]);
                        empresa.Cliente = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdCliente"]));
                        empresa.Descripcion = row["Descripcion"].ToString();
                        empresa.IdSapOrganizacionDeVenta = row["IdSapOrganizacionDeVenta"].ToString();
                        empresa.IdSapSector = row["IdSapSector"].ToString();
                        empresa.IdSapCanalLocal = row["IdSapCanalLocal"].ToString();
                        empresa.IdSapCanalExpor = row["IdSapCanalExpor"].ToString();
                        empresa.Sap_Id = row["Sap_Id"].ToString();
                        empresa.IdSapMoneda = row["IdSapMoneda"].ToString();

                        result.Add(empresa);
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
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Empresa: " + ex.Message.ToString());
            }
            finally
            {
                conn1.Close();
            }
        }

        public EmpresaAdmin GetOneAdmin(int idEmpresa)
        {
            if (idEmpresa == 0)
                return new EmpresaAdmin();

            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetEmpresaAdmin", idEmpresa);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<EmpresaAdmin> result = new List<EmpresaAdmin>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var empresa = new EmpresaAdmin();
                        empresa.IdEmpresa = Convert.ToInt32(row["IdEmpresa"]);
                        empresa.Cliente = null;
                        empresa.Descripcion = row["Descripcion"].ToString();
                        empresa.IdSapOrganizacionDeVenta = row["IdSapOrganizacionDeVenta"].ToString();
                        empresa.IdSapSector = row["IdSapSector"].ToString();
                        empresa.IdSapCanalLocal = row["IdSapCanalLocal"].ToString();
                        empresa.IdSapCanalExpor = row["IdSapCanalExpor"].ToString();
                        empresa.Sap_Id = row["Sap_Id"].ToString();
                        empresa.IdSapMoneda = row["IdSapMoneda"].ToString();
                        empresa.IdGrupoEmpresa = Tools.Value2<int>(row["IdGrupoEmpresa"], 0);
                        empresa.IdApp = Tools.Value2<int>(row["IdApp"], 0);
                        empresa.IdPais = Tools.Value2<int>(row["IdPais"], 0);

                        result.Add(empresa);
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
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Empresa: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }
        }

        public IList<EmpresaAdmin> GetAllAdmin()
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetEmpresaAdmin", 0);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    var result = new List<EmpresaAdmin>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var empresa = new EmpresaAdmin();
                        empresa.IdEmpresa = Convert.ToInt32(row["IdEmpresa"]);
                        empresa.Cliente = null;
                        empresa.Descripcion = row["Descripcion"].ToString();
                        empresa.IdSapOrganizacionDeVenta = row["IdSapOrganizacionDeVenta"].ToString();
                        empresa.IdSapSector = row["IdSapSector"].ToString();
                        empresa.IdSapCanalLocal = row["IdSapCanalLocal"].ToString();
                        empresa.IdSapCanalExpor = row["IdSapCanalExpor"].ToString();
                        empresa.Sap_Id = row["Sap_Id"].ToString();
                        empresa.IdSapMoneda = row["IdSapMoneda"].ToString();
                        empresa.IdGrupoEmpresa = Tools.Value2<int>(row["IdGrupoEmpresa"], 0);
                        empresa.IdApp = Tools.Value2<int>(row["IdApp"], 0);
                        empresa.IdPais = Tools.Value2<int>(row["IdPais"], 0);

                        result.Add(empresa);
                    }

                    return result;
                }
                else
                {
                    return new List<EmpresaAdmin>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Empresa: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }

        public Empresa GetOneByIdCliente(int idCliente)
        {
            if (idCliente == 0)
                return null;

            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetEmpresaByIDCliente", idCliente);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Empresa> result = new List<Empresa>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Empresa empresa = new Empresa();
                        empresa.IdEmpresa = Convert.ToInt32(row["IdEmpresa"]);
                        empresa.Cliente = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdCliente"]));
                        empresa.Descripcion = row["Descripcion"].ToString();
                        empresa.IdSapOrganizacionDeVenta = row["IdSapOrganizacionDeVenta"].ToString();
                        empresa.IdSapSector = row["IdSapSector"].ToString();
                        empresa.IdSapCanalLocal = row["IdSapCanalLocal"].ToString();
                        empresa.IdSapCanalExpor = row["IdSapCanalExpor"].ToString();
                        empresa.Sap_Id = row["Sap_Id"].ToString();
                        empresa.IdSapMoneda = row["IdSapMoneda"].ToString();

                        result.Add(empresa);
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
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Empresa by IDCliente: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }

        public Empresa GetOneBySap_Id(string sap_id)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetEmpresaBySap_Id", sap_id);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Empresa> result = new List<Empresa>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Empresa empresa = new Empresa();
                        empresa.IdEmpresa = Convert.ToInt32(row["IdEmpresa"]);
                        empresa.Cliente = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdCliente"]));
                        empresa.Descripcion = row["Descripcion"].ToString();
                        empresa.IdSapOrganizacionDeVenta = row["IdSapOrganizacionDeVenta"].ToString();
                        empresa.IdSapSector = row["IdSapSector"].ToString();
                        empresa.IdSapCanalLocal = row["IdSapCanalLocal"].ToString();
                        empresa.IdSapCanalExpor = row["IdSapCanalExpor"].ToString();
                        empresa.Sap_Id = row["Sap_Id"].ToString();
                        empresa.IdSapMoneda = row["IdSapMoneda"].ToString();

                        result.Add(empresa);
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
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Empresa by IDCliente: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }

        public int SaveOrUpdate(Empresa empresa, int IdGrupoEmpresa, string Moneda)
        {
            Object resul = null;
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                if (empresa.IdEmpresa > 0)
                    resul = SqlHelper.ExecuteNonQuery(conn1, "GuardarEmpresaABM", empresa.IdEmpresa, empresa.Cliente.IdCliente, empresa.Descripcion, empresa.IdSapOrganizacionDeVenta, DBNull.Value, DBNull.Value, DBNull.Value, empresa.Sap_Id, IdGrupoEmpresa, Moneda);
                else
                    resul = SqlHelper.ExecuteScalar(conn1, "GuardarEmpresaABM", empresa.IdEmpresa, empresa.Cliente.IdCliente, empresa.Descripcion, empresa.IdSapOrganizacionDeVenta, DBNull.Value, DBNull.Value, DBNull.Value, empresa.Sap_Id, IdGrupoEmpresa, Moneda);
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

        private static IList<Empresa> combo;
        public static IList<Empresa> Combo
        {
            get { return combo; }
            set { combo = value; }
        }

        public IList<Empresa> GetStaticCombo()
        {
            if (Combo == null)
            {
                Combo = GetCombo();
            }
            return Combo;
        }

        public IList<Empresa> GetCombo()
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetEmpresa", 0);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Empresa> result = new List<Empresa>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Empresa empresa = new Empresa();
                        empresa.IdEmpresa = Convert.ToInt32(row["IdEmpresa"]);
                        empresa.Cliente = null;
                        empresa.Descripcion = row["Descripcion"].ToString();
                        empresa.IdSapOrganizacionDeVenta = row["IdSapOrganizacionDeVenta"].ToString();
                        empresa.IdSapSector = row["IdSapSector"].ToString();
                        empresa.IdSapCanalLocal = row["IdSapCanalLocal"].ToString();
                        empresa.IdSapCanalExpor = row["IdSapCanalExpor"].ToString();
                        empresa.Sap_Id = row["Sap_Id"].ToString();

                        result.Add(empresa);
                    }

                    return result;
                }
                else
                {
                    return new List<Empresa>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Empresa: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }


        public IList<Empresa> GetFiltro(string busqueda)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetEmpresaFiltro", busqueda);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Empresa> result = new List<Empresa>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Empresa empresa = new Empresa();
                        empresa.IdEmpresa = Convert.ToInt32(row["IdEmpresa"]);
                        empresa.Cliente = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdCliente"]));
                        empresa.Descripcion = row["Descripcion"].ToString();
                        empresa.IdSapOrganizacionDeVenta = row["IdSapOrganizacionDeVenta"].ToString();
                        empresa.IdSapSector = row["IdSapSector"].ToString();
                        empresa.IdSapCanalLocal = row["IdSapCanalLocal"].ToString();
                        empresa.IdSapCanalExpor = row["IdSapCanalExpor"].ToString();
                        empresa.Sap_Id = row["Sap_Id"].ToString();

                        result.Add(empresa);
                    }

                    return result;
                }
                else
                {
                    return new List<Empresa>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Empresa: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }
        }

        public IList<Empresa> GetEmpresaByGrupoEmpresa(string IdGrupoEmpresa)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetEmpresaByGrupoEmpresa", IdGrupoEmpresa);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Empresa> result = new List<Empresa>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Empresa empresa = new Empresa();
                        empresa.IdEmpresa = Convert.ToInt32(row["IdEmpresa"]);
                        empresa.Cliente = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdCliente"]));
                        empresa.Descripcion = row["Descripcion"].ToString();
                        empresa.IdSapOrganizacionDeVenta = row["IdSapOrganizacionDeVenta"].ToString();
                        empresa.IdSapSector = row["IdSapSector"].ToString();
                        empresa.IdSapCanalLocal = row["IdSapCanalLocal"].ToString();
                        empresa.IdSapCanalExpor = row["IdSapCanalExpor"].ToString();
                        empresa.Sap_Id = row["Sap_Id"].ToString();

                        result.Add(empresa);
                    }
                    return result;
                }
                else
                {
                    return new List<Empresa>();
                }
            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Empresa: " + ex.Message.ToString());
            }
            finally
            {
                conn1.Close();
            }
        }
        
        public IList<Empresa> GetUsados(string busqueda)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);
                DataSet ds = new DataSet();

                switch (busqueda)
                {
                    case "ProveedorTitularCartaDePorte":
                        ds = SqlHelper.ExecuteDataset(conn1, "GetProveedorTitularCartaDePorteUsadas");
                        break;
                    case "EmpresaRemitenteComercial":
                        ds = SqlHelper.ExecuteDataset(conn1, "GetEmpresaRemitenteComercialUsadas");
                        break;
                    default:
                        break;

                }

                
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Empresa> result = new List<Empresa>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Empresa empresa = new Empresa();
                        empresa.IdEmpresa = Convert.ToInt32(row["IdEmpresa"]);
                        empresa.Cliente = ClienteDAO.Instance.GetOne(Convert.ToInt32(row["IdCliente"]));
                        empresa.Descripcion = row["Descripcion"].ToString();
                        empresa.IdSapOrganizacionDeVenta = row["IdSapOrganizacionDeVenta"].ToString();
                        empresa.IdSapSector = row["IdSapSector"].ToString();
                        empresa.IdSapCanalLocal = row["IdSapCanalLocal"].ToString();
                        empresa.IdSapCanalExpor = row["IdSapCanalExpor"].ToString();
                        empresa.Sap_Id = row["Sap_Id"].ToString();

                        result.Add(empresa);
                    }

                    return result;
                }
                else
                {
                    return new List<Empresa>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Empresa: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }



    }
}

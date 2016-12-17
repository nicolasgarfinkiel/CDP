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
    public class ClienteDAO : BaseDAO
    {
        private static ClienteDAO instance;

        public ClienteDAO() { }

        public static ClienteDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ClienteDAO();
                }
                return instance;
            }
        }


        public IList<Cliente> GetAll()
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetCliente", 0, this.GetIdEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Cliente> result = new List<Cliente>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Cliente cliente = new Cliente();
                        cliente.IdCliente = Convert.ToInt32(row["IdCliente"]);
                        cliente.RazonSocial = row["RazonSocial"].ToString();
                        cliente.NombreFantasia = row["NombreFantasia"].ToString();
                        cliente.Cuit = row["Cuit"].ToString();

                        if (!(row["IdTipoDocumentoSAP"] is System.DBNull))
                            cliente.TipoDocumento = TipoDocumentoSAPDAO.Instance.GetOne(Convert.ToInt32(row["IdTipoDocumentoSAP"]));

                        cliente.ClientePrincipal = null; // ClienteDAO.instance.Get(Convert.ToInt32(row["IdClientePrincipal"])).First();
                        cliente.Calle = row["Calle"].ToString();
                        cliente.Numero = row["Numero"].ToString();
                        cliente.Dto = row["Dto"].ToString();
                        cliente.Piso = row["Piso"].ToString();
                        cliente.Cp = row["Cp"].ToString();
                        cliente.Poblacion = row["Poblacion"].ToString();
                        cliente.Activo = Convert.ToBoolean(row["Activo"]);
                        cliente.GrupoComercial = row["GrupoComercial"].ToString();
                        cliente.ClaveGrupo = row["ClaveGrupo"].ToString();
                        cliente.Tratamiento = row["Tratamiento"].ToString();
                        cliente.DescripcionGe = row["DescripcionGe"].ToString();
                        cliente.EsProspecto = Convert.ToBoolean(row["EsProspecto"]);

                        result.Add(cliente);
                    }

                    return result;
                }
                else
                {
                    return new List<Cliente>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Cliente: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }
        public IList<Cliente> GetFiltro(string busqueda, bool esFleteAPagarMarcado, bool tomarEnCuantaFiltroPorCresud)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetClienteFiltro", busqueda, this.GetIdEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Cliente> result = new List<Cliente>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Cliente cliente = new Cliente();
                        cliente.IdCliente = Convert.ToInt32(row["IdCliente"]);
                        cliente.RazonSocial = row["RazonSocial"].ToString();
                        cliente.NombreFantasia = row["NombreFantasia"].ToString();
                        cliente.Cuit = row["Cuit"].ToString();

                        if (!(row["IdTipoDocumentoSAP"] is System.DBNull))
                            cliente.TipoDocumento = TipoDocumentoSAPDAO.Instance.GetOne(Convert.ToInt32(row["IdTipoDocumentoSAP"]));

                        cliente.ClientePrincipal = null; // ClienteDAO.instance.Get(Convert.ToInt32(row["IdClientePrincipal"])).First();
                        cliente.Calle = row["Calle"].ToString();
                        cliente.Numero = row["Numero"].ToString();
                        cliente.Dto = row["Dto"].ToString();
                        cliente.Piso = row["Piso"].ToString();
                        cliente.Cp = row["Cp"].ToString();
                        cliente.Poblacion = row["Poblacion"].ToString();
                        cliente.Activo = Convert.ToBoolean(row["Activo"]);
                        cliente.GrupoComercial = row["GrupoComercial"].ToString();
                        cliente.ClaveGrupo = row["ClaveGrupo"].ToString();
                        cliente.Tratamiento = row["Tratamiento"].ToString();
                        cliente.DescripcionGe = row["DescripcionGe"].ToString();
                        cliente.EsProspecto = Convert.ToBoolean(row["EsProspecto"]);

                        if (esFleteAPagarMarcado)
                        {
                            if (cliente.IdCliente.ToString().Equals("1000005"))
                            {
                                result.Add(cliente);
                            }
                        }
                        else
                        {
                            if (tomarEnCuantaFiltroPorCresud)
                            {
                                if (!cliente.IdCliente.ToString().Equals("1000005"))
                                {
                                    result.Add(cliente);
                                }
                            }
                            else
                            {
                                result.Add(cliente);
                            }
                        }

                    }

                    return result;
                }
                else
                {
                    return new List<Cliente>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Cliente: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }
        public IList<Cliente> GetClienteByCuit(String cuit)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetClienteByCuit", cuit, this.GetIdEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Cliente> result = new List<Cliente>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Cliente cliente = new Cliente();
                        cliente.IdCliente = Convert.ToInt32(row["IdCliente"]);
                        cliente.RazonSocial = row["RazonSocial"].ToString();
                        cliente.NombreFantasia = row["NombreFantasia"].ToString();
                        cliente.Cuit = row["Cuit"].ToString();

                        if (!(row["IdTipoDocumentoSAP"] is System.DBNull))
                            cliente.TipoDocumento = TipoDocumentoSAPDAO.Instance.GetOne(Convert.ToInt32(row["IdTipoDocumentoSAP"]));

                        cliente.ClientePrincipal = null; // ClienteDAO.instance.Get(Convert.ToInt32(row["IdClientePrincipal"])).First();
                        cliente.Calle = row["Calle"].ToString();
                        cliente.Numero = row["Numero"].ToString();
                        cliente.Dto = row["Dto"].ToString();
                        cliente.Piso = row["Piso"].ToString();
                        cliente.Cp = row["Cp"].ToString();
                        cliente.Poblacion = row["Poblacion"].ToString();
                        cliente.Activo = Convert.ToBoolean(row["Activo"]);
                        cliente.GrupoComercial = row["GrupoComercial"].ToString();
                        cliente.ClaveGrupo = row["ClaveGrupo"].ToString();
                        cliente.Tratamiento = row["Tratamiento"].ToString();
                        cliente.DescripcionGe = row["DescripcionGe"].ToString();
                        cliente.EsProspecto = Convert.ToBoolean(row["EsProspecto"]);

                        result.Add(cliente);
                    }

                    return result;
                }
                else
                {
                    return new List<Cliente>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Cliente: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }


        }

        public IList<Cliente> GetClienteByOrganizacionVenta(int IdOrganizacionVenta)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);
                int a = 0;
                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetClienteByOrganizacionVenta", IdOrganizacionVenta);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Cliente> result = new List<Cliente>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Cliente cliente = new Cliente();
                        cliente.IdCliente = Convert.ToInt32(row["IdCliente"]);
                        cliente.RazonSocial = row["RazonSocial"].ToString();
                        cliente.Cuit = row["Cuit"].ToString();
                        cliente.IdSapOrganizacionDeVenta = Convert.ToInt32(row["IdSapOrganizacionDeVenta"]);
                        
                        result.Add(cliente);
                    }
                    return result;
                }
                else
                {
                    return new List<Cliente>();
                }
            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Cliente: " + ex.Message.ToString());
            }
            finally
            {
                conn1.Close();
            }
        }

        public int getIdProspecto()
        {
            int result = 0;
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "getIdClienteProspecto", this.GetIdEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        result = Convert.ToInt32(row["ID"]);
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

        public Cliente GetOneBySAPID(string sapId, string sapIdOrganizacionDeVenta)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetClienteBySAPID", sapId, sapIdOrganizacionDeVenta);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Cliente> result = new List<Cliente>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Cliente cliente = new Cliente();
                        cliente.IdCliente = Convert.ToInt32(row["IdCliente"]);
                        cliente.RazonSocial = row["RazonSocial"].ToString();
                        cliente.NombreFantasia = row["NombreFantasia"].ToString();
                        cliente.Cuit = row["Cuit"].ToString();

                        if (!(row["IdTipoDocumentoSAP"] is System.DBNull))
                            cliente.TipoDocumento = TipoDocumentoSAPDAO.Instance.GetOne(Convert.ToInt32(row["IdTipoDocumentoSAP"]));

                        cliente.ClientePrincipal = null; // ClienteDAO.instance.Get(Convert.ToInt32(row["IdClientePrincipal"])).First();
                        cliente.Calle = row["Calle"].ToString();
                        cliente.Numero = row["Numero"].ToString();
                        cliente.Dto = row["Dto"].ToString();
                        cliente.Piso = row["Piso"].ToString();
                        cliente.Cp = row["Cp"].ToString();
                        cliente.Poblacion = row["Poblacion"].ToString();
                        cliente.Activo = Convert.ToBoolean(row["Activo"]);
                        cliente.GrupoComercial = row["GrupoComercial"].ToString();
                        cliente.ClaveGrupo = row["ClaveGrupo"].ToString();
                        cliente.Tratamiento = row["Tratamiento"].ToString();
                        cliente.DescripcionGe = row["DescripcionGe"].ToString();
                        cliente.EsProspecto = Convert.ToBoolean(row["EsProspecto"]);

                        result.Add(cliente);
                    }

                    if (result.Count > 0)
                        return result.First();
                    else
                        return new Cliente();
                }
                else
                {
                    return new Cliente();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Cliente: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }


        }


        public Cliente GetOne(int idCliente)
        {
            if (idCliente == 0)
                return new Cliente();

            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetCliente", idCliente, this.GetIdEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Cliente> result = new List<Cliente>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Cliente cliente = new Cliente();
                        cliente.IdCliente = Convert.ToInt32(row["IdCliente"]);
                        cliente.RazonSocial = row["RazonSocial"].ToString();
                        cliente.NombreFantasia = row["NombreFantasia"].ToString();
                        cliente.Cuit = row["Cuit"].ToString();

                        if (!(row["IdTipoDocumentoSAP"] is System.DBNull))
                            cliente.TipoDocumento = TipoDocumentoSAPDAO.Instance.GetOne(Convert.ToInt32(row["IdTipoDocumentoSAP"]));

                        cliente.ClientePrincipal = null; // ClienteDAO.instance.Get(Convert.ToInt32(row["IdClientePrincipal"])).First();
                        cliente.Calle = row["Calle"].ToString();
                        cliente.Numero = row["Numero"].ToString();
                        cliente.Dto = row["Dto"].ToString();
                        cliente.Piso = row["Piso"].ToString();
                        cliente.Cp = row["Cp"].ToString();
                        cliente.Poblacion = row["Poblacion"].ToString();
                        cliente.Activo = Convert.ToBoolean(row["Activo"]);
                        cliente.GrupoComercial = row["GrupoComercial"].ToString();
                        cliente.ClaveGrupo = row["ClaveGrupo"].ToString();
                        cliente.Tratamiento = row["Tratamiento"].ToString();
                        cliente.DescripcionGe = row["DescripcionGe"].ToString();
                        cliente.EsProspecto = Convert.ToBoolean(row["EsProspecto"]);

                        result.Add(cliente);
                    }

                    if (result.Count > 0)
                        return result.First();
                    else
                        return new Cliente();
                }
                else
                {
                    return new Cliente();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Cliente: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }
        private static IList<Cliente> clientesCombo;
        public static IList<Cliente> ClientesCombo
        {
            get { return clientesCombo; }
            set { clientesCombo = value; }
        }

        public IList<Cliente> GetStaticCombo()
        {

            if (ClientesCombo == null)
            {
                ClientesCombo = GetCombo();
            }

            return ClientesCombo;

        }

        public IList<Cliente> GetCombo()
        {
            SqlConnection conn1 = null;
            try
            {

                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetCliente", 0, this.GetIdEmpresa());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<Cliente> result = new List<Cliente>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Cliente cliente = new Cliente();
                        cliente.IdCliente = Convert.ToInt32(row["IdCliente"]);
                        cliente.RazonSocial = row["RazonSocial"].ToString();
                        cliente.NombreFantasia = row["NombreFantasia"].ToString();
                        cliente.Cuit = row["Cuit"].ToString();
                        cliente.TipoDocumento = null;// TipoDocumentoSAPDAO.Instance.Get(Convert.ToInt32(row["IdTipoDocumentoSAP"])).First();
                        cliente.ClientePrincipal = null; // ClienteDAO.instance.Get(Convert.ToInt32(row["IdClientePrincipal"])).First();
                        cliente.Calle = row["Calle"].ToString();
                        cliente.Numero = row["Numero"].ToString();
                        cliente.Dto = row["Dto"].ToString();
                        cliente.Piso = row["Piso"].ToString();
                        cliente.Cp = row["Cp"].ToString();
                        cliente.Poblacion = row["Poblacion"].ToString();
                        cliente.Activo = Convert.ToBoolean(row["Activo"]);
                        cliente.GrupoComercial = row["GrupoComercial"].ToString();
                        cliente.ClaveGrupo = row["ClaveGrupo"].ToString();
                        cliente.Tratamiento = row["Tratamiento"].ToString();
                        cliente.DescripcionGe = row["DescripcionGe"].ToString();
                        cliente.EsProspecto = Convert.ToBoolean(row["EsProspecto"]);

                        result.Add(cliente);
                    }

                    return result;
                }
                else
                {
                    return new List<Cliente>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Cliente: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }


        }

        public int SaveOrUpdate(Cliente cliente)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                return SqlHelper.ExecuteNonQuery(conn1, "GuardarCliente", cliente.IdCliente, cliente.RazonSocial, cliente.NombreFantasia, cliente.Cuit, (cliente.TipoDocumento != null) ? cliente.TipoDocumento.IDTipoDocumentoSAP : 0, (cliente.ClientePrincipal != null) ? cliente.ClientePrincipal.IdCliente : 0, cliente.Calle, cliente.Numero, cliente.Dto, cliente.Piso, cliente.Cp, cliente.Poblacion, cliente.Activo, cliente.GrupoComercial, cliente.ClaveGrupo, cliente.Tratamiento, cliente.DescripcionGe, cliente.EsProspecto, cliente.IdSapOrganizacionDeVenta, this.GetIdEmpresa());

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Guardar Cliente: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }


        public int SaveOrUpdateProspecto(Cliente cliente, Int64 idclienteprospecto)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                return SqlHelper.ExecuteNonQuery(conn1, "GuardarUpdateClienteProspecto", cliente.IdCliente, cliente.RazonSocial, cliente.NombreFantasia, cliente.Cuit, (cliente.TipoDocumento != null) ? cliente.TipoDocumento.IDTipoDocumentoSAP : 0, (cliente.ClientePrincipal != null) ? cliente.ClientePrincipal.IdCliente : 0, cliente.Calle, cliente.Numero, cliente.Dto, cliente.Piso, cliente.Cp, cliente.Poblacion, cliente.Activo, cliente.GrupoComercial, cliente.ClaveGrupo, cliente.Tratamiento, cliente.DescripcionGe, cliente.EsProspecto, idclienteprospecto, cliente.IdSapOrganizacionDeVenta, this.GetIdEmpresa());

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR GuardarUpdateClienteProspecto: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }


        public IList<Cliente> GetUsados(string busqueda)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);
                DataSet ds = new DataSet();

                switch (busqueda)
                {
                    case "ClienteIntermediario":
                        ds = SqlHelper.ExecuteDataset(conn1, "GetClienteIntermediarioUsadas", this.GetIdEmpresa());
                        break;
                    case "ClienteCorredor":
                        ds = SqlHelper.ExecuteDataset(conn1, "GetClienteCorredorUsadas", this.GetIdEmpresa());
                        break;
                    case "ClienteEntregador":
                        ds = SqlHelper.ExecuteDataset(conn1, "GetClienteEntregadorUsadas", this.GetIdEmpresa());
                        break;
                    case "ClienteDestinatario":
                        ds = SqlHelper.ExecuteDataset(conn1, "GetClienteDestinatarioUsadas", this.GetIdEmpresa());
                        break;
                    case "ClienteDestinatarioCambio":
                        ds = SqlHelper.ExecuteDataset(conn1, "GetClienteDestinatarioUsadas", this.GetIdEmpresa());
                        break;
                    case "ClienteDestino":
                        ds = SqlHelper.ExecuteDataset(conn1, "GetClienteDestinoUsadas", this.GetIdEmpresa());
                        break;
                    case "ClientePagadorDelFlete":
                        ds = SqlHelper.ExecuteDataset(conn1, "GetClientePagadorDelFleteUsadas", this.GetIdEmpresa());
                        break;
                    case "ClienteRemitenteComercial":
                        ds = SqlHelper.ExecuteDataset(conn1, "GetClienteRemitenteComercialUsadas", this.GetIdEmpresa());
                        break;
                    default:
                        break;

                }



                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    IList<Cliente> result = new List<Cliente>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Cliente cliente = new Cliente();
                        cliente.IdCliente = Convert.ToInt32(row["IdCliente"]);
                        cliente.RazonSocial = row["RazonSocial"].ToString();
                        cliente.NombreFantasia = row["NombreFantasia"].ToString();
                        cliente.Cuit = row["Cuit"].ToString();

                        if (!(row["IdTipoDocumentoSAP"] is System.DBNull))
                            cliente.TipoDocumento = TipoDocumentoSAPDAO.Instance.GetOne(Convert.ToInt32(row["IdTipoDocumentoSAP"]));

                        cliente.ClientePrincipal = null; // ClienteDAO.instance.Get(Convert.ToInt32(row["IdClientePrincipal"])).First();
                        cliente.Calle = row["Calle"].ToString();
                        cliente.Numero = row["Numero"].ToString();
                        cliente.Dto = row["Dto"].ToString();
                        cliente.Piso = row["Piso"].ToString();
                        cliente.Cp = row["Cp"].ToString();
                        cliente.Poblacion = row["Poblacion"].ToString();
                        cliente.Activo = Convert.ToBoolean(row["Activo"]);
                        cliente.GrupoComercial = row["GrupoComercial"].ToString();
                        cliente.ClaveGrupo = row["ClaveGrupo"].ToString();
                        cliente.Tratamiento = row["Tratamiento"].ToString();
                        cliente.DescripcionGe = row["DescripcionGe"].ToString();
                        cliente.EsProspecto = Convert.ToBoolean(row["EsProspecto"]);

                        result.Add(cliente);
                    }

                    return result;
                }
                else
                {
                    return new List<Cliente>();
                }

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get Cliente: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }


        public int SaveLogInterfaz(string mensaje)
        {

            Tools.Logger.InfoFormat("ClienteDAO LogInterfaz : {0}", mensaje);

            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                return SqlHelper.ExecuteNonQuery(conn1, CommandType.Text, "insert into logInterfaz (texto) values ('" + mensaje + "')");

            }
            catch (System.Exception ex)
            {
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Guardar logInterfaz: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }

    }
}


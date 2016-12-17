using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Net;
using CartaDePorte.Core.DAO;
using CartaDePorte.Core.Domain;
using System.Collections;
using CartaDePorte.Core.Servicios;
using CartaDePorte.Common;
using CartaDePorte.Core;
using Newtonsoft.Json;
using System.Text;

namespace CartaDePorte.WebService
{

    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://scr.irsa.com/WebServices/")]
    public class cdpSAP : cdpSAPBase
    {
    }

    public class cdpSAPBase : System.Web.Services.WebService
    {

        private static object obClientesLock = new object();
        private static object objProveedoresLock = new object();

        [WebMethod]
        public String testDebug()
        {
            return "test resultado";
        }

        [WebMethod(false, Description = "Recibe los datos de un cliente para guardarlos en Carta de Porte", BufferResponse = true, EnableSession = true)]
        public MsgOut ProcesarCliente(ClienteXI wsCliente)
        {
            ClienteDAO.Instance.SaveLogInterfaz("Ingreso a ProcesarCliente cdpSAP");

            if (wsCliente != null)
            {
                ClienteDAO.Instance.SaveLogInterfaz("Cliente es distinto de null");
                try
                {
                    string json = JsonConvert.SerializeObject(wsCliente);
                    ClienteDAO.Instance.SaveLogInterfaz("Cliente Recibido : " + json);
                }
                catch { }
            }
            else
                ClienteDAO.Instance.SaveLogInterfaz("Cliente es NULL");
            bool actualizacionProspecto = false;
            Int64 idClienteProspecto = 0;
            lock (obClientesLock)
            {
                try
                {

                    ClienteDAO.Instance.SaveLogInterfaz("Busco cliente en base por idCliente: " + wsCliente.IdCliente.ToString());
                    Cliente customer = ClienteDAO.Instance.GetOneBySAPID(wsCliente.IdCliente, wsCliente.IdSapOrganizacionDeVenta);

                    if (customer == null || customer.IdCliente == 0)
                    {

                        // Lo busco por Cuit porque quizas lo tengo dado de alta como Cliente Prospecto
                        IList<Cliente> customersCuit = ClienteDAO.Instance.GetClienteByCuit(wsCliente.CUIT);

                        if (customersCuit.Count > 0 && customersCuit[0].IdCliente > 0)
                        {
                            foreach (Cliente cliPros in customersCuit)
                            {
                                if (cliPros.EsProspecto)
                                {
                                    idClienteProspecto = cliPros.IdCliente;
                                    actualizacionProspecto = true;
                                    customer = cliPros;
                                    ClienteDAO.Instance.SaveLogInterfaz("Es una actualizacion de cliente prospecto");
                                }
                            }

                            if (customer == null)
                            {
                                customer = new Cliente();
                            }

                        }
                        else
                        {
                            ClienteDAO.Instance.SaveLogInterfaz("Es un cliente nuevo");
                            customer = new Cliente();
                        }

                        customer.IdCliente = Convert.ToInt32(wsCliente.IdCliente);
                        ClienteDAO.Instance.SaveLogInterfaz("con id: " + wsCliente.IdCliente);

                    }

                    ClienteDAO.Instance.SaveLogInterfaz("Comienzo a mapear el objeto");
                    #region Mapeo de las propiedades
                    customer.Activo = wsCliente.Activo;
                    customer.Calle = wsCliente.Calle;
                    customer.ClaveGrupo = wsCliente.ClaveGrupo;
                    customer.Cp = wsCliente.CP;
                    customer.Dto = wsCliente.Dto;
                    customer.GrupoComercial = wsCliente.GrupoComercial;
                    customer.NombreFantasia = wsCliente.NombreFantasia;
                    customer.Numero = wsCliente.Numero;
                    customer.Piso = wsCliente.Piso;
                    customer.Poblacion = wsCliente.Poblacion;
                    customer.Tratamiento = wsCliente.Tratamiento;
                    customer.DescripcionGe = wsCliente.DescripcionGE;
                    customer.EsProspecto = false;
                    customer.IdSapOrganizacionDeVenta = Tools.Value2<int>(wsCliente.IdSapOrganizacionDeVenta, 0);

                    ClienteDAO.Instance.SaveLogInterfaz("Cliente.IdCliente: " + wsCliente.IdCliente + " - Cliente.NombreFantasia: " + wsCliente.NombreFantasia);

                    ClienteDAO.Instance.SaveLogInterfaz("Busco Cliente Principal");
                    #region busqueda del cliente principal
                    if (String.IsNullOrEmpty(wsCliente.ClientePrincipal))
                    {

                        ClienteDAO.Instance.SaveLogInterfaz("No hay Cliente Principal");
                        /// si es un cliente principal
                        customer.ClientePrincipal = null;
                        customer.RazonSocial = wsCliente.RazonSocial;
                        customer.Cuit = wsCliente.CUIT;
                        #region busqueda de documentos
                        customer.TipoDocumento = null;

                        if (!string.IsNullOrEmpty(wsCliente.TipoDocumento))
                        {
                            TipoDocumentoSAP Documento = TipoDocumentoSAPDAO.Instance.GetOne(Convert.ToInt32(wsCliente.TipoDocumento));
                            if (Documento != null)
                            {
                                customer.TipoDocumento = Documento;
                            }
                        }
                        #endregion
                    }
                    else
                    {

                        ClienteDAO.Instance.SaveLogInterfaz("Aparentemente hay Cliente Principal");
                        #region Buscar cliente
                        customer.ClientePrincipal = ClienteDAO.Instance.GetOne(Convert.ToInt32(wsCliente.ClientePrincipal));
                        if (customer.ClientePrincipal == null)
                        {
                            ClienteDAO.Instance.SaveLogInterfaz("No se encuentra el cliente principal." + this.MensajeCliente(wsCliente));
                            return new MsgOut(0, "No se encuentra el cliente principal." + this.MensajeCliente(wsCliente));
                        }
                        #endregion
                        customer.TipoDocumento = customer.ClientePrincipal.TipoDocumento;
                        customer.Cuit = customer.ClientePrincipal.Cuit;
                        customer.RazonSocial = customer.ClientePrincipal.RazonSocial;
                    }
                    #endregion


                    #endregion

                    if (!actualizacionProspecto)
                    {
                        ClienteDAO.Instance.SaveOrUpdate(customer);
                    }
                    else
                    {
                        ClienteDAO.Instance.SaveLogInterfaz("Es Cleinte Prospecto");
                        ClienteDAO.Instance.SaveOrUpdateProspecto(customer, idClienteProspecto);
                    }

                    try
                    {
                        string json = JsonConvert.SerializeObject(customer);
                        ClienteDAO.Instance.SaveLogInterfaz("Cliente Grabado : " + json);
                    }
                    catch { }


                    ClienteDAO.Instance.SaveLogInterfaz("GRABO CLIENTE EN BASE");

                }
                catch (Exception ex)
                {
                    Tools.Logger.Error(ex);
                    ClienteDAO.Instance.SaveLogInterfaz("Error: " + ex.Message + this.MensajeCliente(wsCliente));
                    try { EnvioMailDAO.Instance.sendMail("<b>Interfaz Clientes SAP:</b> <br/><br/>Error: " + ex.Message + "<br/>" + this.MensajeCliente(wsCliente)); }
                    catch { }
                    return new MsgOut(0, "Error: " + ex.Message + this.MensajeCliente(wsCliente));
                }


                ClienteDAO.Instance.SaveLogInterfaz("DEVUELVO OK A XI");
                return new MsgOut(1, "OK");
            }

        }
        private string MensajeCliente(ClienteXI clienteWs)
        {
            try
            {
                return (string.Format("IdCliente: {0}, RazonSocial: {1}, NombreFantasia: {2}, ClientePrincipal: {3}, Activo: {4}, CUIT: {5}, TipoDocumento: {6}, GrupoComercial: {7}, ClaveGrupo: {8}, Poblacion: {9}, Calle: {10}, Numero: {11}, Dto: {12}, Piso: {13}, CP: {14}, Tratamiento: {15},DescripcionGE: {16}",
                  string.IsNullOrEmpty(clienteWs.IdCliente) ? string.Empty : clienteWs.IdCliente,
                  string.IsNullOrEmpty(clienteWs.RazonSocial) ? string.Empty : clienteWs.RazonSocial,
                  string.IsNullOrEmpty(clienteWs.NombreFantasia) ? string.Empty : clienteWs.NombreFantasia,
                  string.IsNullOrEmpty(clienteWs.ClientePrincipal) ? string.Empty : clienteWs.ClientePrincipal,
                  clienteWs.Activo.ToString(),
                  string.IsNullOrEmpty(clienteWs.CUIT) ? string.Empty : clienteWs.CUIT,
                  string.IsNullOrEmpty(clienteWs.TipoDocumento) ? string.Empty : clienteWs.TipoDocumento,
                  string.IsNullOrEmpty(clienteWs.GrupoComercial) ? string.Empty : clienteWs.GrupoComercial,
                  string.IsNullOrEmpty(clienteWs.ClaveGrupo) ? string.Empty : clienteWs.ClaveGrupo,
                  string.IsNullOrEmpty(clienteWs.Poblacion) ? string.Empty : clienteWs.Poblacion,
                  string.IsNullOrEmpty(clienteWs.Calle) ? string.Empty : clienteWs.Calle,
                  string.IsNullOrEmpty(clienteWs.Numero) ? string.Empty : clienteWs.Numero,
                  string.IsNullOrEmpty(clienteWs.Dto) ? string.Empty : clienteWs.Dto,
                  string.IsNullOrEmpty(clienteWs.Piso) ? string.Empty : clienteWs.Piso,
                  string.IsNullOrEmpty(clienteWs.CP) ? string.Empty : clienteWs.CP,
                  string.IsNullOrEmpty(clienteWs.Tratamiento) ? string.Empty : clienteWs.Tratamiento,
                  string.IsNullOrEmpty(clienteWs.DescripcionGE) ? string.Empty : clienteWs.DescripcionGE));
            }
            catch (Exception ex)
            {
                Tools.Logger.Error(ex);
                return ("Error al generar el mensaje con datos del Cliente. Error: " + ex.Message);
            }
        }

        //[WebMethod(false, Description = "Recibe los datos de un proveedor para guardarlos en Carta de Porte", BufferResponse = true, EnableSession = true)]
        //public MsgOut ProcesarProveedor(ProveedorXI wsProveedor = null)
        //{
        //    StringBuilder LogInput = new StringBuilder();
        //    if (wsProveedor == null)
        //    {
        //        LogInput.AppendLine("Registro Recibido de XI: NULL");
        //        Logging.SendEmail("Proveedor Recibido de XI NULL", LogInput.ToString());
        //        return new MsgOut(0, "Proveedor Recibido de XI NULL");
        //    }

        //    lock (objProveedoresLock)
        //    {
        //        try
        //        {
        //            ClienteDAO.Instance.SaveLogInterfaz("Ingreso a ProcesarProveedor: cdpSAP");
        //            try
        //            {
        //                LogInput.AppendLine("Registro Recibido de XI: ");
        //                LogInput.AppendLine("Calle: " + wsProveedor.Calle);
        //                LogInput.AppendLine(", Piso: " + wsProveedor.Piso);
        //                LogInput.AppendLine(", Departamento: " + wsProveedor.Departamento);
        //                LogInput.AppendLine(", Numero: " + wsProveedor.Numero);
        //                LogInput.AppendLine(", CP: " + wsProveedor.CP);
        //                LogInput.AppendLine(", Ciudad: " + wsProveedor.Ciudad);
        //                LogInput.AppendLine(", Pais: " + wsProveedor.Pais);
        //                LogInput.AppendLine(", Id_Proveedor: " + wsProveedor.Id_Proveedor);
        //                LogInput.AppendLine(", Nombre: " + wsProveedor.Nombre);
        //                LogInput.AppendLine(", TipoDocumento: " + wsProveedor.TipoDocumento);
        //                LogInput.AppendLine(", NumeroDocumento: " + wsProveedor.NumeroDocumento);
        //                LogInput.AppendLine(", Activo: " + wsProveedor.Activo);
        //                LogInput.AppendLine(", IdSapOrganizacionDeVenta: " + wsProveedor.IdSapOrganizacionDeVenta);

        //                try
        //                {
        //                    Logging.SendEmail("Proveedor Recibido de XI", LogInput.ToString());
        //                }
        //                catch { }
        //                Tools.Logger.InfoFormat(LogInput.ToString());
        //                string json = JsonConvert.SerializeObject(wsProveedor);
        //                ClienteDAO.Instance.SaveLogInterfaz("Proveedor Recibido : " + json);
        //            }
        //            catch (Exception ex) { Logging.SendEmail("Error Proveedor Recibido", Logging.StackTrace(ex)); }
        //            ClienteDAO.Instance.SaveLogInterfaz(string.Format("Ingresa proveedor Id: {0}, IdSapOrganizacionDeVenta: {1}", wsProveedor.Id_Proveedor, wsProveedor.IdSapOrganizacionDeVenta));
        //            IList<Proveedor> proveedorList = ProveedorDAO.Instance.GetOneByCuitOrgSAPID(wsProveedor.NumeroDocumento);
        //            Proveedor proveedor = new Proveedor();

        //            if (proveedorList != null)
        //            {
        //                proveedor = proveedorList.Where(p => p.IdSapOrganizacionDeVenta == Convert.ToInt32(wsProveedor.IdSapOrganizacionDeVenta) &&
        //                                                     p.Sap_Id == wsProveedor.Id_Proveedor).FirstOrDefault();

        //                if (proveedor == null || proveedor.IdProveedor == 0)
        //                    proveedor = proveedorList.Where(p => p.IdSapOrganizacionDeVenta == Convert.ToInt32(wsProveedor.IdSapOrganizacionDeVenta)).FirstOrDefault();
        //            }

        //            if (proveedor == null || proveedor.IdProveedor == 0)
        //                proveedor = new Proveedor();

        //            #region Obsoleto
        //            //ES ALTA, SI NO LO ENCUENTRA
        //            //SI ID = 0
        //            //O LO ENCUENTRA PERO IdSapOrganizacionDeVenta NO Coincide
        //            //if (proveedor == null || proveedor.IdProveedor == 0)
        //            //{
        //            //    ClienteDAO.Instance.SaveLogInterfaz("Nuevo proveedor:" + wsProveedor.Id_Proveedor);
        //            //    ClienteDAO.Instance.SaveLogInterfaz("Numero Documento:" + wsProveedor.NumeroDocumento);

        //            //    // Lo busco por NumeroDocumento porque quizas lo tengo dado de alta como Proveedor Prospecto
        //            //    IList<Proveedor> proveedorNumeroDocumento = ProveedorDAO.Instance.GetProveedorByNumeroDocumento(wsProveedor.NumeroDocumento);

        //            //    if (proveedorNumeroDocumento.Count > 0 && proveedorNumeroDocumento[0].IdProveedor > 0)
        //            //    {
        //            //        foreach (Proveedor proPros in proveedorNumeroDocumento)
        //            //        {
        //            //            if (proPros.EsProspecto)
        //            //            {
        //            //                idproveedorprospecto = proPros.IdProveedor;
        //            //                ClienteDAO.Instance.SaveLogInterfaz("Actualizacion Prospecto:" + wsProveedor.Id_Proveedor);
        //            //                actualizacionProspecto = true;
        //            //                proveedor = proPros;
        //            //            }
        //            //        }

        //            //        if (proveedor == null)
        //            //            proveedor = new Proveedor();
        //            //    }
        //            //    else
        //            //    {
        //            //        proveedor = new Proveedor();
        //            //    }
        //            //}
        //            #endregion

        //            #region Mapeo de las propiedades
        //            proveedor.Activo = wsProveedor.Activo;
        //            proveedor.Sap_Id = wsProveedor.Id_Proveedor;
        //            proveedor.NumeroDocumento = wsProveedor.NumeroDocumento;
        //            proveedor.Nombre = wsProveedor.Nombre;
        //            proveedor.Calle = wsProveedor.Calle;
        //            proveedor.Piso = wsProveedor.Piso;
        //            proveedor.Pais = wsProveedor.Pais;
        //            proveedor.Ciudad = wsProveedor.Ciudad;
        //            proveedor.CP = wsProveedor.CP;
        //            proveedor.Departamento = wsProveedor.Departamento;
        //            proveedor.Numero = wsProveedor.Numero;
        //            proveedor.IdSapOrganizacionDeVenta = Tools.Value2<int>(wsProveedor.IdSapOrganizacionDeVenta, 0);

        //            #region busqueda de documentos
        //            if (!String.IsNullOrEmpty(wsProveedor.TipoDocumento))
        //            {
        //                TipoDocumentoSAP Documento = TipoDocumentoSAPDAO.Instance.GetOne(Convert.ToInt32(wsProveedor.TipoDocumento));
        //                if (Documento != null)
        //                {
        //                    ClienteDAO.Instance.SaveLogInterfaz("TipoDocumento Encontrado:" + wsProveedor.TipoDocumento);
        //                    proveedor.TipoDocumento = Documento;
        //                }
        //                else
        //                {
        //                    ClienteDAO.Instance.SaveLogInterfaz("TipoDocumento No encontrado:" + wsProveedor.TipoDocumento);
        //                }
        //            }
        //            #endregion
        //            #endregion

        //            ProveedorDAO.Instance.SaveOrUpdate(proveedor);
        //            //En caso de ser prospecto cambio el estado de pendiente de envio a sap
        //            if (proveedor.EsProspecto)
        //            {
        //                ClienteDAO.Instance.SaveLogInterfaz("Es Proveedor Prospecto");
        //                SolicitudDAO.Instance.SaveOrUpdateProspecto(proveedor.IdProveedor);
        //            }

        //            try
        //            {
        //                string json = JsonConvert.SerializeObject(proveedor);
        //                ClienteDAO.Instance.SaveLogInterfaz("Proveedor Grabado : " + json);
        //            }
        //            catch (Exception ex) { Logging.SendEmail("Error Proveedor Grabado", Logging.StackTrace(ex)); }
        //            ClienteDAO.Instance.SaveLogInterfaz("Guardo proveedor");
        //        }
        //        catch (Exception ex)
        //        {
        //            Tools.Logger.Error(ex);
        //            try { EnvioMailDAO.Instance.sendMail("<b>Interfaz Proveedores SAP:</b> <br/><br/>Error: " + ex.Message + "<br/>" + this.MensajeProveedor(wsProveedor)); }
        //            catch { }
        //            Logging.SendEmail("Error Proveedor XI", Logging.StackTrace(ex));
        //            return new MsgOut(0, ex.Message + " " + this.MensajeProveedor(wsProveedor));
        //        }

        //        return new MsgOut(1, "OK");
        //    }
        //}


        [WebMethod(false, Description = "Recibe los datos de un proveedor para guardarlos en Carta de Porte", BufferResponse = true, EnableSession = true)]
        public MsgOut ProcesarProveedor(ProveedorXI wsProveedor)
        {
            MsgOut result;
            lock (cdpSAPBase.objProveedoresLock)
            {
                try
                {
                    ClienteDAO.Instance.SaveLogInterfaz("Ingreso a ProcesarProveedor: cdpSAP");
                    try
                    {
                        string json = JsonConvert.SerializeObject(wsProveedor);
                        ClienteDAO.Instance.SaveLogInterfaz("Proveedor Recibido : " + json);
                    }
                    catch
                    {
                    }
                    bool actualizacionProspecto = false;
                    long idproveedorprospecto = 0L;
                    ClienteDAO.Instance.SaveLogInterfaz(string.Format("Ingresa proveedor Id: {0}, IdSapOrganizacionDeVenta: {1}", wsProveedor.Id_Proveedor, wsProveedor.IdSapOrganizacionDeVenta));
                    Proveedor proveedor = ProveedorDAO.Instance.GetOneBySAPID(wsProveedor.Id_Proveedor, wsProveedor.IdSapOrganizacionDeVenta);

                    if (proveedor == null || proveedor.IdProveedor == 0)
                    {
                        ClienteDAO.Instance.SaveLogInterfaz("Nuevo proveedor:" + wsProveedor.Id_Proveedor);
                        ClienteDAO.Instance.SaveLogInterfaz("Numero Documento:" + wsProveedor.NumeroDocumento);
                        IList<Proveedor> proveedorNumeroDocumento = ProveedorDAO.Instance.GetOneByCuitOrgSAPID(wsProveedor.NumeroDocumento);
                        if (proveedorNumeroDocumento.Count > 0 && proveedorNumeroDocumento[0].IdProveedor > 0)
                        {
                            foreach (Proveedor proPros in proveedorNumeroDocumento)
                            {
                                if (proPros.EsProspecto)
                                {
                                    idproveedorprospecto = (long)proPros.IdProveedor;
                                    ClienteDAO.Instance.SaveLogInterfaz("Actualizacion Prospecto:" + wsProveedor.Id_Proveedor);
                                    actualizacionProspecto = true;
                                    proveedor = proPros;
                                }
                            }
                            if (proveedor == null)
                            {
                                proveedor = new Proveedor();
                            }
                        }
                        else
                        {
                            proveedor = new Proveedor();
                        }
                    }
                    proveedor.Activo = wsProveedor.Activo;
                    proveedor.Sap_Id = wsProveedor.Id_Proveedor;
                    proveedor.NumeroDocumento = wsProveedor.NumeroDocumento;
                    proveedor.Nombre = wsProveedor.Nombre;
                    proveedor.Calle = wsProveedor.Calle;
                    proveedor.Piso = wsProveedor.Piso;
                    proveedor.Pais = wsProveedor.Pais;
                    proveedor.Ciudad = wsProveedor.Ciudad;
                    proveedor.CP = wsProveedor.CP;
                    proveedor.Departamento = wsProveedor.Departamento;
                    proveedor.Numero = wsProveedor.Numero;
                    proveedor.EsProspecto = false;
                    proveedor.IdSapOrganizacionDeVenta = Tools.Value2<int>(wsProveedor.IdSapOrganizacionDeVenta, 0);
                    if (!string.IsNullOrEmpty(wsProveedor.TipoDocumento))
                    {
                        TipoDocumentoSAP Documento = TipoDocumentoSAPDAO.Instance.GetOne(Convert.ToInt32(wsProveedor.TipoDocumento));
                        if (Documento != null)
                        {
                            ClienteDAO.Instance.SaveLogInterfaz("TipoDocumento Encontrado:" + wsProveedor.TipoDocumento);
                            proveedor.TipoDocumento = Documento;
                        }
                        else
                        {
                            ClienteDAO.Instance.SaveLogInterfaz("TipoDocumento No encontrado:" + wsProveedor.TipoDocumento);
                        }
                    }
                    if (!actualizacionProspecto)
                    {
                        ProveedorDAO.Instance.SaveOrUpdate(proveedor);
                    }
                    else
                    {
                        ClienteDAO.Instance.SaveLogInterfaz("Es Proveedor Prospecto");
                        ProveedorDAO.Instance.SaveOrUpdateProspecto(proveedor, idproveedorprospecto);
                    }
                    try
                    {
                        string json = JsonConvert.SerializeObject(proveedor);
                        ClienteDAO.Instance.SaveLogInterfaz("Proveedor Grabado : " + json);
                    }
                    catch
                    {
                    }
                    ClienteDAO.Instance.SaveLogInterfaz("Guardo proveedor");
                }
                catch (Exception ex)
                {
                    Tools.Logger.Error(ex);
                    try
                    {
                        EnvioMailDAO.Instance.sendMail("<b>Interfaz Proveedores SAP:</b> <br/><br/>Error: " + ex.Message + "<br/>" + this.MensajeProveedor(wsProveedor));
                    }
                    catch
                    {
                    }
                    result = new MsgOut(0, ex.Message + " " + this.MensajeProveedor(wsProveedor));
                    return result;
                }
                result = new MsgOut(1, "OK");
            }
            return result;
        }
        /// <summary>
        /// Genera un string con los datos del proveedor
        /// </summary>
        /// <param name="wsProveedor"></param>
        /// <returns></returns>
        private string MensajeProveedor(ProveedorXI wsProveedor)
        {
            try
            {
                return (string.Format("IdProveedor: {0}, Nombre: {1}, Activo: {2}, TipoDocumento: {3}, NumeroDocumento: {4}",
                    string.IsNullOrEmpty(wsProveedor.Id_Proveedor) ? string.Empty : wsProveedor.Id_Proveedor,
                string.IsNullOrEmpty(wsProveedor.Nombre) ? string.Empty : wsProveedor.Nombre, wsProveedor.Activo.ToString(),
                string.IsNullOrEmpty(wsProveedor.TipoDocumento) ? string.Empty : wsProveedor.TipoDocumento,
                string.IsNullOrEmpty(wsProveedor.NumeroDocumento) ? string.Empty : wsProveedor.NumeroDocumento));
            }
            catch (Exception ex)
            {
                Tools.Logger.Error(ex);
                return ("Error al generar el mensaje con datos del proveedor. Error: " + ex.Message);
            }
        }
        [WebMethod]
        public void RecibirPrefacturacion(SapMensajePrefactura[] mensajes)
        {
            try
            {
                Tools.Logger.InfoFormat("RecibirPrefacturacion Ingreso");
                //SETEO USUARIO GENERICO CON TODAS LAS EMPRESAS
                var empresas = EmpresaDAO.Instance.GetAllAdmin();
                var usuario = new UsuarioFull();
                foreach (var empresa in empresas)
                {
                    usuario.Empresas.Add(empresa.IdEmpresa, empresa);
                }
                usuario.Nombre = "CartaDePorte.WS";
                App.Usuario = usuario;

                int nroEnvio = 1;
                Boolean dioError = false;
                int cnt = 0;
                if (mensajes.Count() > 0)
                {
                    nroEnvio = LogSapDAO.Instance.GetLogSapUltimoNroEnvio(mensajes[0].NroDocumentoRE);
                    nroEnvio++;
                }

                Tools.Logger.InfoFormat("RecibirPrefacturacion nroEnvio: {0}", nroEnvio);
                foreach (SapMensajePrefactura mensaje in mensajes)
                {
                    if (!dioError)
                    {
                        if (mensaje.TipoMensaje.Equals("E"))
                        {
                            dioError = true;
                            Tools.Logger.InfoFormat("RecibirPrefacturacion GetSolicitudByCDP: {0}", mensaje.NroDocumentoRE);

                            //SolicitudFull solicitud = SolicitudDAO.Instance.GetSolicitudByCDP(mensaje.NroDocumentoRE);
                            int idEmpresa;
                            int idSolicitud = SolicitudDAO.Instance.GetSolicitudByCDPSAP(mensaje.NroDocumentoRE, out idEmpresa);

                            Tools.Logger.InfoFormat("RecibirPrefacturacion idSolicitud: {0}", idSolicitud);
                            Tools.Logger.InfoFormat("RecibirPrefacturacion idEmpresa: {0}", idEmpresa);

                            //SETEO EMPRESA CORRESPONDIENTE A LA SOLICITUD
                            App.Usuario.SetEmpresa(idEmpresa);

                            SolicitudFull solicitud = SolicitudDAO.Instance.GetOne(idSolicitud);

                            if (solicitud.EstadoEnSAP == Enums.EstadoEnvioSAP.EnProcesoAnulacion)
                            {
                                if (!String.IsNullOrEmpty(mensaje.TextoMensaje))
                                    solicitud.MensajeRespuestaAnulacionSAP = mensaje.TextoMensaje;

                                if (solicitud.IdEstablecimientoDestinoCambio != null && solicitud.IdEstablecimientoDestinoCambio.IdEstablecimiento > 0)
                                {
                                    Tools.Logger.InfoFormat("RecibirPrefacturacion wssap.PrefacturaSAP: {0}", solicitud.IdSolicitud);
                                    wsSAP wssap = new wsSAP();
                                    wssap.PrefacturaSAP(solicitud, false, true);
                                }
                                else
                                {
                                    if (!String.IsNullOrEmpty(mensaje.NroDocumentoSap))
                                        solicitud.CodigoRespuestaEnvioSAP = mensaje.NroDocumentoSap;

                                    solicitud.EstadoEnSAP = Enums.EstadoEnvioSAP.FinalizadoConError;
                                }
                                //"Terceros por venta  de Granos de producción propia"
                                //EnvioPrefacturaSAPTerceros(solicitud);
                            }
                            else
                            {
                                if (solicitud.TipoDeCarta.Descripcion.Equals("Terceros por venta  de Granos de producción propia") &&
                                    solicitud.EstadoEnSAP == Enums.EstadoEnvioSAP.PrimerEnvioTerceros)
                                {
                                    solicitud.EstadoEnSAP = Enums.EstadoEnvioSAP.PrimerEnvioTerceros;
                                }
                                else
                                {
                                    solicitud.EstadoEnSAP = Enums.EstadoEnvioSAP.FinalizadoConError;
                                }

                                if (!String.IsNullOrEmpty(mensaje.TextoMensaje))
                                    solicitud.MensajeRespuestaEnvioSAP = mensaje.TextoMensaje;
                            }
                            Tools.Logger.InfoFormat("RecibirPrefacturacion SaveOrUpdate: {0}", solicitud.IdSolicitud);
                            SolicitudDAO.Instance.SaveOrUpdate(solicitud);
                        }
                    }
                }

                foreach (SapMensajePrefactura mensaje in mensajes)
                {
                    if (!dioError)
                    {
                        if (mensaje.TipoMensaje.Equals("S"))
                        {
                            if (cnt == 0)
                            {
                                if (!String.IsNullOrEmpty(mensaje.NroDocumentoRE))
                                {
                                    Tools.Logger.InfoFormat("RecibirPrefacturacion GetSolicitudByCDP: {0}", mensaje.NroDocumentoRE);
                                    //SolicitudFull solicitud = SolicitudDAO.Instance.GetSolicitudByCDP(mensaje.NroDocumentoRE);
                                    int idEmpresa;
                                    int idSolicitud = SolicitudDAO.Instance.GetSolicitudByCDPSAP(mensaje.NroDocumentoRE, out idEmpresa);

                                    Tools.Logger.InfoFormat("RecibirPrefacturacion idSolicitud: {0}", idSolicitud);
                                    Tools.Logger.InfoFormat("RecibirPrefacturacion idEmpresa: {0}", idEmpresa);

                                    //SETEO EMPRESA CORRESPONDIENTE A LA SOLICITUD
                                    App.Usuario.SetEmpresa(idEmpresa);

                                    SolicitudFull solicitud = SolicitudDAO.Instance.GetOne(idSolicitud);

                                    if (solicitud.EstadoEnSAP == Enums.EstadoEnvioSAP.EnProcesoAnulacion)
                                    {
                                        solicitud.EstadoEnSAP = Enums.EstadoEnvioSAP.Anulada;
                                        solicitud.CodigoRespuestaAnulacionSAP = mensaje.NroDocumentoSap;
                                        if (!String.IsNullOrEmpty(mensaje.TextoMensaje))
                                            solicitud.MensajeRespuestaAnulacionSAP = mensaje.TextoMensaje;

                                        if (solicitud.IdEstablecimientoDestinoCambio != null && solicitud.IdEstablecimientoDestinoCambio.IdEstablecimiento > 0)
                                        {
                                            Tools.Logger.InfoFormat("RecibirPrefacturacion wssap.PrefacturaSAP: {0}", solicitud.IdSolicitud);
                                            wsSAP wssap = new wsSAP();
                                            wssap.PrefacturaSAP(solicitud, false, true);
                                        }
                                        //"Terceros por venta  de Granos de producción propia"
                                        //EnvioPrefacturaSAPTerceros(solicitud);
                                    }
                                    else
                                    {
                                        if (solicitud.TipoDeCarta.Descripcion.Equals("Terceros por venta  de Granos de producción propia") &&
                                            solicitud.EstadoEnSAP == Enums.EstadoEnvioSAP.PrimerEnvioTerceros)
                                        {
                                            solicitud.EstadoEnSAP = Enums.EstadoEnvioSAP.PrimerEnvioTerceros;
                                        }
                                        else
                                        {
                                            if (solicitud.TipoDeCarta.Descripcion.Equals("Terceros por venta  de Granos de producción propia"))
                                            {
                                                solicitud.EstadoEnSAP = Enums.EstadoEnvioSAP.FinalizadoOk;
                                                if (!FinalizadoOK(solicitud))
                                                {
                                                    solicitud.EstadoEnSAP = Enums.EstadoEnvioSAP.FinalizadoConError;
                                                }
                                            }
                                            else
                                            {
                                                solicitud.EstadoEnSAP = Enums.EstadoEnvioSAP.FinalizadoOk;
                                                if (!FinalizadoOK(solicitud))
                                                {
                                                    solicitud.EstadoEnSAP = Enums.EstadoEnvioSAP.FinalizadoConError;
                                                }
                                            }
                                        }
                                        solicitud.CodigoRespuestaEnvioSAP = mensaje.NroDocumentoSap;
                                        if (!String.IsNullOrEmpty(mensaje.TextoMensaje))
                                            solicitud.MensajeRespuestaEnvioSAP = mensaje.TextoMensaje;
                                    }
                                    Tools.Logger.InfoFormat("RecibirPrefacturacion SaveOrUpdate: {0}", solicitud.IdSolicitud);
                                    SolicitudDAO.Instance.SaveOrUpdate(solicitud);
                                }
                            }
                        }
                        cnt++;
                    }
                    SaveLog(mensaje, nroEnvio);
                }
                if (dioError)
                {
                    try { EnvioMailDAO.Instance.sendMail("<b>Error proceso SapMensajePrefactura:</b><br/><br/>Carta de porte: " + mensajes[0].NroDocumentoRE); }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                Tools.Logger.Error(ex);
                throw;
            }
        }

        private void SaveLog(SapMensajePrefactura mensaje, int nroenvio)
        {
            // guardo todo en el log de respuestas de SAP
            // LogSap
            LogSap logsap = new LogSap();
            logsap.IDoc = mensaje.IDoc;
            logsap.Origen = mensaje.Origen;

            Char delimiter = '|';
            string[] OrgVenta = mensaje.NroDocumentoRE.Split(delimiter);

            if (OrgVenta[1].ToString() != string.Empty)
                logsap.NroDocumentoRE = mensaje.NroDocumentoRE;
            else
                logsap.NroDocumentoRE = mensaje.NroDocumentoRE.Replace("|", string.Empty);

            logsap.NroDocumentoSap = mensaje.NroDocumentoSap;
            logsap.TipoMensaje = mensaje.TipoMensaje;
            logsap.TextoMensaje = mensaje.TextoMensaje;
            logsap.NroEnvio = nroenvio;

            Tools.Logger.InfoFormat("RecibirPrefacturacion LogSapDAO: {0}|{1}|{2}|{3}|{4}|{5}|{6}", mensaje.IDoc, mensaje.Origen, mensaje.NroDocumentoRE, mensaje.NroDocumentoSap, mensaje.TipoMensaje, mensaje.TextoMensaje, nroenvio);

            LogSapDAO.Instance.SaveOrUpdate(logsap);
        }
        private Boolean FinalizadoOK(Solicitud solicitud)
        {
            IList<LogSap> lista = LogSapDAO.Instance.GetOneByNroCartaDePorte(solicitud.NumeroCartaDePorte);
            if (lista.Count > 0)
            {
                foreach (LogSap ls in lista)
                {
                    if (ls.TipoMensaje.Equals("E"))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        private void EnvioPrefacturaSAPTerceros(Solicitud solicitud)
        {
            if (solicitud.TipoDeCarta.Descripcion.Equals("Terceros por venta  de Granos de producción propia"))
            {
                wsSAP wssap = new wsSAP();
                wssap.PrefacturaSAP(solicitud, false, false);
            }

        }
        #region Credenciales
        public NetworkCredential NetWorkCredential
        {
            get
            {
                return new NetworkCredential("procmailer", "prc01mail#07snd", "IRSACORP");
            }
        }

        public WebProxy WebProxy
        {
            get
            {
                WebProxy proxy = new WebProxy("http://SRV-MS20-ADM:8080");
                proxy.Credentials = NetWorkCredential;

                return proxy;
            }
        }
        #endregion

    }


    public class ClienteXI
    {
        #region Privadas
        private string razonSocial;
        private string nombreFantasia;
        private string cuit;
        private string tipoDocumento;
        private string idcliente;
        private string clienteprincipal;
        private string calle;
        private string numero;
        private string piso;
        private string dto;
        private string cp;
        private string poblacion;
        private bool activo;
        private string grupoComercial;
        private string claveGrupo;
        private string tratamiento;
        private string descripcionGE;
        private string idSapOrganizacionDeVenta;
        #endregion

        #region Propiedades
        /// <summary>
        /// Razón social del cliente
        /// </summary>
        public string RazonSocial
        {
            get { return razonSocial; }
            set { razonSocial = value; }
        }
        /// <summary>
        /// Nombre de fantasía del cliente
        /// </summary>
        public string NombreFantasia
        {
            get { return this.nombreFantasia; }
            set { this.nombreFantasia = value; }
        }
        /// <summary>
        /// CUIT (Código único de Identificación Tributaria) del cliente
        /// </summary>
        public string CUIT
        {
            get { return this.cuit; }
            set { this.cuit = value; }
        }
        /// <summary>
        /// Tipo de documento del cliente
        /// </summary>
        public string TipoDocumento
        {
            get { return this.tipoDocumento; }
            set { this.tipoDocumento = value; }
        }
        /// <summary>
        /// Identidicador del cliente para el ERP
        /// </summary>
        public string IdCliente
        {
            get { return this.idcliente; }
            set { this.idcliente = value; }
        }
        /// <summary>
        /// Si tiene datos es un cliente subsidiario. En caso contrario, este es un cliente principal. 
        /// El dato en este campo es el IDCliente del cliente principal.
        /// </summary>
        public string ClientePrincipal
        {
            get { return this.clienteprincipal; }
            set { this.clienteprincipal = value; }
        }
        /// <summary>
        /// Dirección del cliente (calle)
        /// </summary>
        public string Calle
        {
            get { return this.calle; }
            set { this.calle = value; }
        }
        /// <summary>
        /// Dirección del cliente (número)
        /// </summary>
        public string Numero
        {
            get { return this.numero; }
            set { this.numero = value; }
        }
        /// <summary>
        /// Dirección del cliente (Departamento)
        /// </summary>
        public string Dto
        {
            get { return this.dto; }
            set { this.dto = value; }
        }
        /// <summary>
        /// Dirección del cliente (Piso)
        /// </summary>
        public string Piso
        {
            get { return this.piso; }
            set { this.piso = value; }
        }
        /// <summary>
        /// Código postal
        /// </summary>
        /// 
        public string CP
        {
            get { return this.cp; }
            set { this.cp = value; }
        }
        /// <summary>
        /// ???
        /// </summary>
        public string Poblacion
        {
            get { return this.poblacion; }
            set { this.poblacion = value; }
        }
        /// <summary>
        /// Define el estado del cliente. Si está Activo o no (borrado).
        /// </summary>
        public bool Activo
        {
            get { return this.activo; }
            set { this.activo = value; }
        }
        /// <summary>
        /// ???
        /// </summary>
        public string GrupoComercial
        {
            get { return this.grupoComercial; }
            set { this.grupoComercial = value; }
        }
        /// <summary>
        /// ???
        /// </summary>
        public string ClaveGrupo
        {
            get { return this.claveGrupo; }
            set { this.claveGrupo = value; }
        }
        /// <summary>
        /// ???
        /// </summary>
        public string Tratamiento
        {
            get { return this.tratamiento; }
            set { this.tratamiento = value; }
        }
        /// <summary>
        /// Descripcion Grupo Economico.
        /// </summary>
        public string DescripcionGE
        {
            get { return this.descripcionGE; }
            set { this.descripcionGE = value; }
        }

        /// <summary>
        /// IdSapOrganizacionDeVenta
        /// </summary>
        public string IdSapOrganizacionDeVenta
        {
            get { return this.idSapOrganizacionDeVenta; }
            set { this.idSapOrganizacionDeVenta = value; }
        }

        #endregion
    }
    public class ProveedorXI
    {
        #region Privadas
        private string id_proveedor;
        private string nombre;
        private string calle;
        private string piso;
        private string departamento;
        private string numero;
        private string cp;
        private string ciudad;
        private string pais;
        private string tipo_Doc;
        private string numero_Doc;
        private bool activo;
        private string idSapOrganizacionDeVenta;
        #endregion

        #region Propiedades
        public string Calle
        {
            get { return calle; }
            set { calle = value; }
        }
        public string Piso
        {
            get { return piso; }
            set { piso = value; }
        }
        public string Departamento
        {
            get { return departamento; }
            set { departamento = value; }
        }
        public string Numero
        {
            get { return numero; }
            set { numero = value; }
        }
        public string CP
        {
            get { return cp; }
            set { cp = value; }
        }
        public string Ciudad
        {
            get { return ciudad; }
            set { ciudad = value; }
        }
        public string Pais
        {
            get { return pais; }
            set { pais = value; }
        }
        /// <summary>
        /// Nombre del cliente
        /// </summary>
        public string Id_Proveedor
        {
            get { return id_proveedor; }
            set { id_proveedor = value; }
        }
        /// <summary>
        /// Nombre del cliente
        /// </summary>
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        /// <summary>
        /// Tipo de documento
        /// </summary>
        public string TipoDocumento
        {
            get { return tipo_Doc; }
            set { tipo_Doc = value; }
        }
        /// <summary>
        /// Número de documento
        /// </summary>
        public string NumeroDocumento
        {
            get { return numero_Doc; }
            set { numero_Doc = value; }
        }
        /// <summary>
        /// El cliente está activo o no (borrado)
        /// </summary>
        public bool Activo
        {
            get { return activo; }
            set { activo = value; }
        }

        public string IdSapOrganizacionDeVenta
        {
            get { return this.idSapOrganizacionDeVenta; }
            set { this.idSapOrganizacionDeVenta = value; }
        }
        #endregion
    }

    public class MsgOut
    {
        #region Variables privadas
        private int _Valor;
        private string _Msg;
        #endregion

        #region Constructores
        /// <summary>
        /// Contructor básico
        /// </summary>
        public MsgOut()
        {
        }

        /// <summary>
        /// Constructor completo
        /// </summary>
        /// <param name="valor">
        ///     Si es 1 Todo está OK.
        ///     Si es 0 es que hay algún problema hay
        /// </param>
        /// <param name="message">Mensaje de error si es que el value es 0.</param>
        public MsgOut(int valor, string message)
        {
            _Valor = valor;
            _Msg = message;
        }
        #endregion

        #region Propiedades
        /// <summary>
        ///     Si es 1 Todo está OK.
        ///     Si es 0 es que hay algún problema hay
        /// </summary>
        public int pValor
        {
            get { return _Valor; }
            set { _Valor = value; }
        }

        /// <summary>
        /// Mensaje de error si es que el value es 0.
        /// </summary>
        public string pMsg
        {
            get { return _Msg; }
            set { _Msg = value; }
        }
        #endregion
    }

    //[Serializable]
    //public class SapMensajeFacturaCollection : ArrayList
    //{
    //    public SapMensajeFacturaCollection()
    //        : base()
    //    {
    //    }


    //    public void Add(SapMensajeFactura message)
    //    {
    //        base.Add(message);
    //    }


    //    new public SapMensajeFactura this[int index]
    //    {
    //        get { return ((SapMensajeFactura)base[index]); }
    //        set { base[index] = value; }
    //    }
    //}

    //[Serializable]
    //public class SapMensajeFactura
    //{
    //    private string _IDoc; //Nro. Factura legal
    //    private string _nroDocumentoReferencia; //Nro. Factura legal
    //    private string _nroDocumentoComercial; //Nro interno de sap (no importa)
    //    private decimal _tipoCambioMoneda; //Valor del cambio segun moneda
    //    private DateTime _fechaFactura;
    //    private string _nroDocumentoModelo; //Nro. Prefactura (SAP)
    //    private string _origen;


    //    public SapMensajeFactura()
    //    {
    //    }


    //    public string Origen
    //    {
    //        get { return _origen; }
    //        set { _origen = value; }
    //    }

    //    public string IDoc
    //    {
    //        get { return _IDoc; }
    //        set { _IDoc = value; }
    //    }

    //    public string NroDocumentoReferencia
    //    {
    //        get { return _nroDocumentoReferencia; }
    //        set { _nroDocumentoReferencia = value; }
    //    }

    //    public string NroDocumentoComercial
    //    {
    //        get { return _nroDocumentoComercial; }
    //        set { _nroDocumentoComercial = value; }
    //    }

    //    public decimal TipoCambioMoneda
    //    {
    //        get { return _tipoCambioMoneda; }
    //        set { _tipoCambioMoneda = value; }
    //    }

    //    public DateTime FechaFactura
    //    {
    //        get { return _fechaFactura; }
    //        set { _fechaFactura = value; }
    //    }

    //    public string NroDocumentoModelo
    //    {
    //        get { return _nroDocumentoModelo; }
    //        set { _nroDocumentoModelo = value; }
    //    }
    //}

    [Serializable]
    public class SapMensajePrefacturaCollection : ArrayList
    {
        public SapMensajePrefacturaCollection()
            : base()
        {
        }


        public void Add(SapMensajePrefactura message)
        {
            base.Add(message);
        }


        new public SapMensajePrefactura this[int index]
        {
            get { return ((SapMensajePrefactura)base[index]); }
            set { base[index] = value; }
        }
    }

    [Serializable]
    public class SapMensajePrefactura
    {
        private string _IDoc;
        private string _origen;
        private string _nroDocumentoRE; //Id de Cabecer del mensaje enviado
        private string _nroDocumentoSap; //Numero de prefactura de sap.
        private string _tipoMensaje; //(S)uccess or (E)rror
        private string _textoMensaje; //Informacion descriptiva


        public SapMensajePrefactura()
        {
        }


        public string IDoc
        {
            get { return _IDoc; }
            set { _IDoc = value; }
        }
        public string Origen
        {
            get { return _origen; }
            set { _origen = value; }
        }

        /// <summary>
        /// Id de Cabecera del mensaje enviado
        /// </summary>
        public string NroDocumentoRE
        {
            get { return _nroDocumentoRE; }
            set { _nroDocumentoRE = value; }
        }

        /// <summary>
        /// Numero de prefactura de sap.
        /// </summary>
        public string NroDocumentoSap
        {
            get { return _nroDocumentoSap; }
            set { _nroDocumentoSap = value; }
        }

        /// <summary>
        /// (S)uccess or (E)rror
        /// </summary>
        public string TipoMensaje
        {
            get { return _tipoMensaje; }
            set { _tipoMensaje = value; }
        }

        /// <summary>
        /// Informacion descriptiva
        /// </summary>
        public string TextoMensaje
        {
            get { return _textoMensaje; }
            set { _textoMensaje = value; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Configuration;
using Microsoft.VisualBasic;


using CartaDePorte.Core.Exception;
using CartaDePorte.Core.Domain;
using CartaDePorte.Common;


namespace CartaDePorte.Core.DAO
{
    public class AfipAuthDAO : BaseDAO
    {
        private static AfipAuthDAO instance;
        public AfipAuthDAO() {}

        public static AfipAuthDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AfipAuthDAO();
                }
                return instance;
            }
        }


        public int SaveOrUpdate(AfipAuth afipAuth)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                return SqlHelper.ExecuteNonQuery(conn1, "GuardarAfipAuth", afipAuth.Token, afipAuth.Sign, afipAuth.GenerationTime, afipAuth.ExpirationTime, afipAuth.Service, afipAuth.UniqueID);

            }
            catch (System.Exception ex)
            {
                Tools.Logger.Error(ex);
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Guardar AfipAuth: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }
        public AfipAuth Get(int idAfipAuth)
        {
            SqlConnection conn1 = null;
            try
            {
                string sql = string.Empty;
                conn1 = new SqlConnection(connString);

                DataSet ds = SqlHelper.ExecuteDataset(conn1, "GetAfipAuth", idAfipAuth);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    IList<AfipAuth> result = new List<AfipAuth>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        AfipAuth afipauth = new AfipAuth();
                        afipauth.Token = row["Token"].ToString();
                        afipauth.Sign = row["Sign"].ToString();
                        afipauth.GenerationTime = Convert.ToDateTime(row["GenerationTime"]);
                        afipauth.ExpirationTime = Convert.ToDateTime(row["ExpirationTime"]);
                        afipauth.Service = row["Service"].ToString();
                        afipauth.UniqueID = row["UniqueID"].ToString();
                        afipauth.CuitRepresentado = (long)Convert.ToDouble(row["CuitRepresentado"]);

                        result.Add(afipauth);
                    }

                    return result.First();
                }
                else
                {
                    return new AfipAuth();
                }

            }
            catch (System.Exception ex)
            {
                Tools.Logger.Error(ex);
                throw ExceptionFactory.CreateBusiness(ex, "ERROR Get AfipAuth: " + ex.Message.ToString());

            }
            finally
            {
                conn1.Close();
            }

        }

        public int RenovarTokenAfip()
        {
            string strUrlWsaaWsdl = ConfigurationManager.AppSettings["WSAAServiceURL"];
            string strIdServicioNegocio = "wsctg";
            string strRutaCertSigner = ConfigurationManager.AppSettings["RutaCertificadop12"];
            bool blnVerboseMode = true;

            LoginTicket objTicketRespuesta;
            string strTicketRespuesta;

            try
            {

                if (blnVerboseMode)
                {
                    Tools.Logger.InfoFormat("Servicio a acceder: {0}", strIdServicioNegocio);
                    Tools.Logger.InfoFormat("URL del WSAA: {0}", strUrlWsaaWsdl);
                    Tools.Logger.InfoFormat("Ruta del certificado: {0}", strRutaCertSigner);
                    Tools.Logger.InfoFormat("Modo verbose: {0}", blnVerboseMode);

                }

                objTicketRespuesta = new LoginTicket();

                if (blnVerboseMode)
                {
                    Tools.Logger.InfoFormat("Accediendo a {0}", strUrlWsaaWsdl);
                }

                strTicketRespuesta = objTicketRespuesta.ObtenerLoginTicketResponse(strIdServicioNegocio, strUrlWsaaWsdl, strRutaCertSigner, blnVerboseMode);

                if (blnVerboseMode)
                {
                    AfipAuth aa = new AfipAuth();
                    aa.Token = objTicketRespuesta.Token;
                    aa.Sign = objTicketRespuesta.Sign;
                    aa.GenerationTime = objTicketRespuesta.GenerationTime;
                    aa.ExpirationTime = objTicketRespuesta.ExpirationTime;
                    aa.Service = objTicketRespuesta.Service;
                    aa.UniqueID = Convert.ToString(objTicketRespuesta.UniqueId);

                    this.SaveOrUpdate(aa);
                }
            }

            catch (System.Exception excepcionAlObtenerTicket)
            {
                Tools.Logger.Error("EXCEPCION AL OBTENER TICKET:");
                Tools.Logger.Error(excepcionAlObtenerTicket);
                return -10;

            }
            return 0;

        }


    
    
    }

    class LoginTicket
    {
        // Entero de 32 bits sin signo que identifica el requerimiento 
        public UInt32 UniqueId;
        // Momento en que fue generado el requerimiento 
        public DateTime GenerationTime;
        // Momento en el que exoira la solicitud 
        public DateTime ExpirationTime;
        // Identificacion del WSN para el cual se solicita el TA 
        public string Service;
        // Firma de seguridad recibida en la respuesta 
        public string Sign;
        // Token de seguridad recibido en la respuesta 
        public string Token;

        public XmlDocument XmlLoginTicketRequest = null;
        public XmlDocument XmlLoginTicketResponse = null;
        public string RutaDelCertificadoFirmante;
        public string XmlStrLoginTicketRequestTemplate = "<loginTicketRequest><header><uniqueId></uniqueId><generationTime></generationTime><expirationTime></expirationTime></header><service></service></loginTicketRequest>";

        private bool _verboseMode = true;

        // OJO! NO ES THREAD-SAFE 
        private static UInt32 _globalUniqueID = 0;

        /// <summary> 
        /// Construye un Login Ticket obtenido del WSAA 
        /// </summary> 
        /// <param name="argServicio">Servicio al que se desea acceder</param> 
        /// <param name="argUrlWsaa">URL del WSAA</param> 
        /// <param name="argRutaCertX509Firmante">Ruta del certificado X509 (con clave privada) usado para firmar</param> 
        /// <param name="argVerbose">Nivel detallado de descripcion? true/false</param> 
        /// <remarks></remarks> 
        public string ObtenerLoginTicketResponse(string argServicio, string argUrlWsaa, string argRutaCertX509Firmante, bool argVerbose)
        {

            this.RutaDelCertificadoFirmante = argRutaCertX509Firmante;
            this._verboseMode = argVerbose;
            CertificadosX509Lib.VerboseMode = argVerbose;

            string cmsFirmadoBase64;
            string loginTicketResponse;

            XmlNode xmlNodoUniqueId;
            XmlNode xmlNodoGenerationTime;
            XmlNode xmlNodoExpirationTime;
            XmlNode xmlNodoService;

            // PASO 1: Genero el Login Ticket Request 
            try
            {
                XmlLoginTicketRequest = new XmlDocument();
                XmlLoginTicketRequest.LoadXml(XmlStrLoginTicketRequestTemplate);

                xmlNodoUniqueId = XmlLoginTicketRequest.SelectSingleNode("//uniqueId");
                xmlNodoGenerationTime = XmlLoginTicketRequest.SelectSingleNode("//generationTime");
                xmlNodoExpirationTime = XmlLoginTicketRequest.SelectSingleNode("//expirationTime");
                xmlNodoService = XmlLoginTicketRequest.SelectSingleNode("//service");

                xmlNodoGenerationTime.InnerText = DateTime.Now.AddMinutes(-10).ToString("s");
                xmlNodoExpirationTime.InnerText = DateTime.Now.AddMinutes(+10).ToString("s");
                xmlNodoUniqueId.InnerText = Convert.ToString(_globalUniqueID);
                xmlNodoService.InnerText = argServicio;
                this.Service = argServicio;

                _globalUniqueID += 1;

                if (this._verboseMode)
                {
                    Tools.Logger.InfoFormat(XmlLoginTicketRequest.OuterXml);
                }
            }

            catch (System.Exception excepcionAlGenerarLoginTicketRequest)
            {
                Tools.Logger.Error(excepcionAlGenerarLoginTicketRequest);
                throw new System.Exception("Error GENERANDO el LoginTicketRequest : " + excepcionAlGenerarLoginTicketRequest.Message);
            }

            // PASO 2: Firmo el Login Ticket Request 
            try
            {
                if (this._verboseMode)
                {
                    Tools.Logger.InfoFormat("Leyendo certificado: {0}", RutaDelCertificadoFirmante);
                }

                X509Certificate2 certFirmante = CertificadosX509Lib.ObtieneCertificadoDesdeArchivo(RutaDelCertificadoFirmante);

                if (this._verboseMode)
                {
                    Tools.Logger.InfoFormat("Firmando: ");
                    Tools.Logger.InfoFormat(XmlLoginTicketRequest.OuterXml);
                }

                // Convierto el login ticket request a bytes, para firmar 
                Encoding EncodedMsg = Encoding.UTF8;
                byte[] msgBytes = EncodedMsg.GetBytes(XmlLoginTicketRequest.OuterXml);

                // Firmo el msg y paso a Base64 
                byte[] encodedSignedCms = CertificadosX509Lib.FirmaBytesMensaje(msgBytes, certFirmante);
                cmsFirmadoBase64 = Convert.ToBase64String(encodedSignedCms);
            }

            catch (System.Exception excepcionAlFirmar)
            {
                Tools.Logger.Error(excepcionAlFirmar);
                throw new System.Exception("***Error FIRMANDO el LoginTicketRequest : " + excepcionAlFirmar.Message);
            }

            // PASO 3: Invoco al WSAA para obtener el Login Ticket Response 
            try
            {
                if (this._verboseMode)
                {
                    Tools.Logger.InfoFormat("Llamando al WSAA en URL: {0}", argUrlWsaa);
                    Tools.Logger.InfoFormat("Argumento en el request:");
                    Tools.Logger.InfoFormat(cmsFirmadoBase64);
                }

                CartaDePorte.Core.Wsaa.LoginCMSService servicioWsaa = new CartaDePorte.Core.Wsaa.LoginCMSService();
                servicioWsaa.Url = argUrlWsaa;
                WebProxy proxy = new WebProxy(System.Configuration.ConfigurationSettings.AppSettings["Proxy"]);
                proxy.Credentials = new NetworkCredential(System.Configuration.ConfigurationSettings.AppSettings["UserProxy"], System.Configuration.ConfigurationSettings.AppSettings["PassProxy"], System.Configuration.ConfigurationSettings.AppSettings["Domain"]);

                servicioWsaa.Proxy = proxy;

                loginTicketResponse = servicioWsaa.loginCms(cmsFirmadoBase64);

                if (this._verboseMode)
                {
                    Tools.Logger.InfoFormat("LoguinTicketResponse: ");
                    Tools.Logger.InfoFormat(loginTicketResponse);
                }
            }

            catch (System.Exception excepcionAlInvocarWsaa)
            {
                Tools.Logger.Error(excepcionAlInvocarWsaa);
                throw new System.Exception("***Error INVOCANDO al servicio WSAA : " + excepcionAlInvocarWsaa.Message);
            }


            // PASO 4: Analizo el Login Ticket Response recibido del WSAA 
            try
            {
                XmlLoginTicketResponse = new XmlDocument();
                XmlLoginTicketResponse.LoadXml(loginTicketResponse);

                this.UniqueId = UInt32.Parse(XmlLoginTicketResponse.SelectSingleNode("//uniqueId").InnerText);
                this.GenerationTime = DateTime.Parse(XmlLoginTicketResponse.SelectSingleNode("//generationTime").InnerText);
                this.ExpirationTime = DateTime.Parse(XmlLoginTicketResponse.SelectSingleNode("//expirationTime").InnerText);
                this.Sign = XmlLoginTicketResponse.SelectSingleNode("//sign").InnerText;
                this.Token = XmlLoginTicketResponse.SelectSingleNode("//token").InnerText;
            }
            catch (System.Exception excepcionAlAnalizarLoginTicketResponse)
            {
                Tools.Logger.Error(excepcionAlAnalizarLoginTicketResponse);
                throw new System.Exception("***Error ANALIZANDO el LoginTicketResponse : " + excepcionAlAnalizarLoginTicketResponse.Message);
            }

            return loginTicketResponse;

        }


    }

    class CertificadosX509Lib
    {

        public static bool VerboseMode = false;

        /// <summary> 
        /// Firma mensaje 
        /// </summary> 
        /// <param name="argBytesMsg">Bytes del mensaje</param> 
        /// <param name="argCertFirmante">Certificado usado para firmar</param> 
        /// <returns>Bytes del mensaje firmado</returns> 
        /// <remarks></remarks> 
        public static byte[] FirmaBytesMensaje(byte[] argBytesMsg, X509Certificate2 argCertFirmante)
        {
            try
            {
                // Pongo el mensaje en un objeto ContentInfo (requerido para construir el obj SignedCms) 
                ContentInfo infoContenido = new ContentInfo(argBytesMsg);
                SignedCms cmsFirmado = new SignedCms(infoContenido);

                // Creo objeto CmsSigner que tiene las caracteristicas del firmante 
                CmsSigner cmsFirmante = new CmsSigner(argCertFirmante);
                cmsFirmante.IncludeOption = X509IncludeOption.EndCertOnly;

                if (VerboseMode)
                {
                    Tools.Logger.InfoFormat("Firmando bytes del mensaje...");
                }
                // Firmo el mensaje PKCS #7 
                cmsFirmado.ComputeSignature(cmsFirmante);

                if (VerboseMode)
                {
                    Tools.Logger.InfoFormat("OK mensaje firmado");
                }

                // Encodeo el mensaje PKCS #7. 
                return cmsFirmado.Encode();
            }
            catch (System.Exception excepcionAlFirmar)
            {
                Tools.Logger.Error(excepcionAlFirmar);
                throw new System.Exception("***Error al firmar: " + excepcionAlFirmar.Message);
            }
        }

        /// <summary> 
        /// Lee certificado de disco 
        /// </summary> 
        /// <param name="argArchivo">Ruta del certificado a leer.</param> 
        /// <returns>Un objeto certificado X509</returns> 
        /// <remarks></remarks> 
        public static X509Certificate2 ObtieneCertificadoDesdeArchivo(string argArchivo)
        {
            X509Certificate2 objCert = new X509Certificate2(); // new X509Certificate2(argArchivo, "", X509KeyStorageFlags.MachineKeySet); 

            try
            {
                objCert.Import(Microsoft.VisualBasic.FileIO.FileSystem.ReadAllBytes(argArchivo), System.Configuration.ConfigurationSettings.AppSettings["Certificadop12Password"], X509KeyStorageFlags.PersistKeySet);
                //objCert.Import(Microsoft.VisualBasic.FileIO.FileSystem.ReadAllBytes(argArchivo), "123456", X509KeyStorageFlags.PersistKeySet);
                return objCert;
            }
            catch (System.Exception excepcionAlImportarCertificado)
            {
                Tools.Logger.Error(excepcionAlImportarCertificado);
                throw new System.Exception("argArchivo=" + argArchivo + " excepcion=" + excepcionAlImportarCertificado.Message + " " + excepcionAlImportarCertificado.StackTrace);

            }
        }

    }





}

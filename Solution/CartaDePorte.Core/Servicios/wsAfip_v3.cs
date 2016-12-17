using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using CartaDePorte.Core.Domain;
using CartaDePorte.Core.DAO;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Xml.Serialization;
using System.Xml;
using CartaDePorte.Core.CTGService_v3;

namespace CartaDePorte.Core.Servicios
{
    public class wsAfip_v3
    {
        static AfipAuth afipAuth = null;
        static String CTGServiceURL = null;
        private static readonly string LogFileAfip = AppDomain.CurrentDomain.BaseDirectory + @"LogAfip\" + string.Format("{0}{1}{2}.txt", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), DateTime.Now.Day.ToString());

        public wsAfip_v3()
        {
            afipAuth = AfipAuthDAO.Instance.Get(1);
            CTGServiceURL = ConfigurationSettings.AppSettings["CTGServiceURL"];
        }

        #region Metodos Afip
        public solicitarCTGReturnType solicitarCTGInicial(Solicitud solicitud)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback
            = (sender, certificate, chain, sslPolicyErrors) => { return true; };

            CTGService_v3.CTGService_v30 service = new CTGService_v30();
            service.Proxy = WebProxy;
            service.Timeout = 3600000;
            service.Url = CTGServiceURL;

            if (Environment.MachineName.ToUpper() == "WI7-SIS22N-ADM" || Environment.MachineName.ToUpper() == "SRV-MS10-ADM")
            {
                Log("Integracion Afip Deshabilitada - CTG otorgado automaticamente.", "Success");
                solicitarCTGReturnType RetornarAfipHomo = new solicitarCTGReturnType();
                datosSolicitarCTGResponseType Response = new datosSolicitarCTGResponseType();
                datosSolicitarCTGType datos = new datosSolicitarCTGType();
                //var CTGHomo = new wsAfip_v3Homo().solicitarCTGInicial(solicitud);
                RetornarAfipHomo.arrayErrores = new string[0];
                RetornarAfipHomo.observacion = "CTG otorgado";
                RetornarAfipHomo.datosSolicitarCTGResponse = Response;
                RetornarAfipHomo.datosSolicitarCTGResponse.datosSolicitarCTG = datos;
                Random rnd = new Random();
                RetornarAfipHomo.datosSolicitarCTGResponse.datosSolicitarCTG.ctg = Convert.ToInt64(rnd.Next(100000000, 999999999));
                return RetornarAfipHomo;
            }

            solicitarCTGInicialRequestType request = new solicitarCTGInicialRequestType();
            request.auth = new authType();
            request.auth.token = afipAuth.Token;
            request.auth.sign = afipAuth.Sign;
            request.auth.cuitRepresentado = afipAuth.CuitRepresentado;

            request.datosSolicitarCTGInicial = new datosSolicitarCTGInicialType();

            request.datosSolicitarCTGInicial.cartaPorte = (long)Convert.ToDouble(solicitud.NumeroCartaDePorte);
            request.datosSolicitarCTGInicial.codigoEspecie = solicitud.Grano.EspecieAfip.Codigo;

            if (solicitud.ClienteDestino != null && (!String.IsNullOrEmpty(solicitud.ClienteDestino.Cuit)))
                request.datosSolicitarCTGInicial.cuitDestino = (long)Convert.ToDouble(solicitud.ClienteDestino.Cuit);

            if (solicitud.ClienteDestinatario != null && (!String.IsNullOrEmpty(solicitud.ClienteDestinatario.Cuit)))
                request.datosSolicitarCTGInicial.cuitDestinatario = (long)Convert.ToDouble(solicitud.ClienteDestinatario.Cuit);

            request.datosSolicitarCTGInicial.codigoLocalidadOrigen = solicitud.IdEstablecimientoProcedencia.Localidad.Codigo;
            request.datosSolicitarCTGInicial.codigoLocalidadDestino = solicitud.IdEstablecimientoDestino.Localidad.Codigo;
            request.datosSolicitarCTGInicial.codigoCosecha = solicitud.Grano.CosechaAfip.Codigo;

            if (solicitud.CargaPesadaDestino)
                request.datosSolicitarCTGInicial.pesoNeto = (long)solicitud.KilogramosEstimados;
            else
                request.datosSolicitarCTGInicial.pesoNeto = (long)solicitud.PesoNeto.Value;

            request.datosSolicitarCTGInicial.patente = solicitud.PatenteCamion;
            request.datosSolicitarCTGInicial.cantHorasSpecified = true;
            request.datosSolicitarCTGInicial.cantHoras = Convert.ToInt32(solicitud.CantHoras);

            if (solicitud.ClientePagadorDelFlete != null && solicitud.ClientePagadorDelFlete.IdCliente > 0 && solicitud.ClientePagadorDelFlete.EsEmpresa())
            {
                if (solicitud.ProveedorTransportista != null)
                {
                    request.datosSolicitarCTGInicial.cuitTransportistaSpecified = true;
                    request.datosSolicitarCTGInicial.cuitTransportista = (long)Convert.ToDouble(solicitud.ProveedorTransportista.NumeroDocumento); //27054888649;
                }
            }
            else
            {
                if (solicitud.ChoferTransportista != null)
                {
                    request.datosSolicitarCTGInicial.cuitTransportistaSpecified = true;
                    request.datosSolicitarCTGInicial.cuitTransportista = (long)Convert.ToDouble(solicitud.ChoferTransportista.Cuit); //27054888649;
                }
            }

            //Cambio solicitado por Maximiliano y Pedro
            //Si existe intermediario lo manda y no el comercial
            if (!String.IsNullOrEmpty(solicitud.ClienteIntermediario.Cuit))
            {
                request.datosSolicitarCTGInicial.remitenteComercialComoCanjeador = (solicitud.RemitenteComercialComoCanjeador ? "SI" : "NO");
                request.datosSolicitarCTGInicial.cuitCanjeadorSpecified = true;
                request.datosSolicitarCTGInicial.cuitCanjeador = (long)Convert.ToDouble(solicitud.ClienteIntermediario.Cuit);// 33504619589;
            }
            else if (!String.IsNullOrEmpty(solicitud.ClienteRemitenteComercial.Cuit))
            {
                request.datosSolicitarCTGInicial.remitenteComercialComoCanjeador = (solicitud.RemitenteComercialComoCanjeador ? "SI" : "NO");
                request.datosSolicitarCTGInicial.cuitCanjeadorSpecified = true;
                request.datosSolicitarCTGInicial.cuitCanjeador = (long)Convert.ToDouble(solicitud.ClienteRemitenteComercial.Cuit);// 33504619589;
            }

            //Si la procedencia es las vertientes debe 
            if (solicitud.IdEstablecimientoProcedencia.Descripcion.Contains("Las Vertientes") && solicitud.IdEstablecimientoProcedencia.IdEstablecimiento == 72)
                request.datosSolicitarCTGInicial.remitenteComercialcomoProductor = "SI";

            request.datosSolicitarCTGInicial.kmARecorrer = (uint)Convert.ToDouble(solicitud.KmRecorridos);
            //PS 27052015
            request.datosSolicitarCTGInicial.cuitCorredorSpecified = false;
            if (solicitud.ClienteCorredor != null && !String.IsNullOrEmpty(solicitud.ClienteCorredor.Cuit))
            {
                request.datosSolicitarCTGInicial.cuitCorredor = long.Parse(solicitud.ClienteCorredor.Cuit);
                request.datosSolicitarCTGInicial.cuitCorredorSpecified = true;
            }
            solicitarCTGReturnType resul;
            try
            {
                resul = service.solicitarCTGInicial(request);
                Log(LogToAFIP(request, null, solicitud.IdSolicitud.ToString(), solicitud.NumeroCartaDePorte.ToString()).ToString(), "Success");
                Log(LogToAFIP(null, resul, solicitud.IdSolicitud.ToString(), solicitud.NumeroCartaDePorte.ToString()).ToString(), "Respuesta");
            }
            catch (System.Exception e)
            {
                Log(LogToAFIP(request, null, solicitud.IdSolicitud.ToString(), solicitud.NumeroCartaDePorte.ToString()).ToString(), "BeginError");
                string StackTrace = string.Empty;
                var message = new StringBuilder();
                StackTrace = String.Format("{0}\n{1}", e.Message, e.StackTrace);
                int bucle = 0;

                while (e.InnerException != null)
                {
                    bucle += 1;
                    message.AppendLine("----------------------Exception " + bucle.ToString() + "-----------------------");
                    message.AppendLine();
                    message.AppendLine(e.InnerException.Message);
                    message.AppendLine();
                    message.AppendLine(e.InnerException.StackTrace);
                    message.AppendLine();
                    message.AppendLine("-------------------------------------------------------------------------------");

                    e = e.InnerException;
                }

                if (message != null && Environment.MachineName.ToUpper() != "WI7-SIS22N-ADM")
                    SendEmail("Exception solicitarCTGInicial", StackTrace, "SPoszalski@cresud.com.ar");

                Log(StackTrace, "Error");

                solicitarCTGReturnType resul2 = new solicitarCTGReturnType();
                resul2.arrayErrores = new string[1];
                resul2.arrayErrores[0] = e.Message.ToString();
                envioMailAlertaAfip("<b>Envio de Solicitud a Afip</b> <br/>" + e.Message.ToString());
                return resul2;
            }
            return resul;
        }

        public cosechaType[] consultarCosechas()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback
            = (sender, certificate, chain, sslPolicyErrors) => { return true; };

            CTGService_v3.CTGService_v30 service = new CTGService_v30();
            service.Proxy = WebProxy;
            service.Timeout = 3600000;
            service.Url = CTGServiceURL;

            string url = service.Url;
            consultarCosechasRequestType request = new consultarCosechasRequestType();
            request.auth = new authType();
            request.auth.token = afipAuth.Token;
            request.auth.sign = afipAuth.Sign;
            request.auth.cuitRepresentado = afipAuth.CuitRepresentado;

            consultarCosechasReturnType resul = null;
            try
            {
                resul = service.consultarCosechas(request);
                Log(LogToAFIP(request, null, string.Empty, string.Empty).ToString(), "Success");
                Log(LogToAFIP(null, resul, string.Empty, string.Empty).ToString(), "Respuesta");

            }
            catch (System.Exception e)
            {
                Log(LogToAFIP(request, null, string.Empty, string.Empty).ToString(), "BeginError");
                string StackTrace = string.Empty;
                var message = new StringBuilder();
                StackTrace = String.Format("{0}\n{1}", e.Message, e.StackTrace);
                int bucle = 0;

                while (e.InnerException != null)
                {
                    bucle += 1;
                    message.AppendLine("----------------------Exception " + bucle.ToString() + "-----------------------");
                    message.AppendLine();
                    message.AppendLine(e.InnerException.Message);
                    message.AppendLine();
                    message.AppendLine(e.InnerException.StackTrace);
                    message.AppendLine();
                    message.AppendLine("-------------------------------------------------------------------------------");

                    e = e.InnerException;
                }

                if (message != null && Environment.MachineName.ToUpper() != "WI7-SIS22N-ADM")
                    SendEmail("Exception consultarCosechas", StackTrace, "SPoszalski@cresud.com.ar");

                Log(StackTrace, "Error");

                envioMailAlertaAfip("<b>Consultar Cosechas</b> <br/>" + e.Message.ToString());
                throw e;
            }

            if (resul != null)
                return resul.arrayCosechas;
            else
                return null;
        }

        public especieType[] consultarEspecies()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback
            = (sender, certificate, chain, sslPolicyErrors) => { return true; };

            CTGService_v30 service = new CTGService_v30();
            service.Proxy = WebProxy;
            service.Timeout = 3600000;
            service.Url = CTGServiceURL;
            string url = service.Url;

            consultarEspeciesRequestType request = new consultarEspeciesRequestType();
            request.auth = new authType();
            request.auth.token = afipAuth.Token;
            request.auth.sign = afipAuth.Sign;
            request.auth.cuitRepresentado = afipAuth.CuitRepresentado;

            consultarEspeciesReturnType resul = null;
            try
            {
                resul = service.consultarEspecies(request);
                Log(LogToAFIP(request, null, string.Empty, string.Empty).ToString(), "Success");
                Log(LogToAFIP(null, resul, string.Empty, string.Empty).ToString(), "Respuesta");

            }
            catch (System.Exception e)
            {
                Log(LogToAFIP(request, null, string.Empty, string.Empty).ToString(), "BeginError");
                string StackTrace = string.Empty;
                var message = new StringBuilder();
                StackTrace = String.Format("{0}\n{1}", e.Message, e.StackTrace);
                int bucle = 0;

                while (e.InnerException != null)
                {
                    bucle += 1;
                    message.AppendLine("----------------------Exception " + bucle.ToString() + "-----------------------");
                    message.AppendLine();
                    message.AppendLine(e.InnerException.Message);
                    message.AppendLine();
                    message.AppendLine(e.InnerException.StackTrace);
                    message.AppendLine();
                    message.AppendLine("-------------------------------------------------------------------------------");

                    e = e.InnerException;
                }

                if (message != null && Environment.MachineName.ToUpper() != "WI7-SIS22N-ADM")
                    SendEmail("Exception consultarCosechas", StackTrace, "SPoszalski@cresud.com.ar");

                Log(StackTrace, "Error");
                envioMailAlertaAfip("<b>Consultar Especies</b> <br/>" + e.Message.ToString());
                throw e;
            }

            if (resul != null)
                return resul.arrayEspecies;
            else
                return null;
        }

        public localidadType[] consultarLocalidadesPorProvinicia(sbyte codigoProvincia)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback
            = (sender, certificate, chain, sslPolicyErrors) => { return true; };

            CTGService_v3.CTGService_v30 service = new CTGService_v30();
            service.Proxy = WebProxy;
            service.Timeout = 3600000;
            service.Url = CTGServiceURL;
            string url = service.Url;

            consultarLocalidadesPorProvinciaRequestType request = new consultarLocalidadesPorProvinciaRequestType();
            request.auth = new authType();
            request.auth.token = afipAuth.Token;
            request.auth.sign = afipAuth.Sign;
            request.auth.cuitRepresentado = afipAuth.CuitRepresentado;
            request.codigoProvincia = codigoProvincia;

            consultarLocalidadesPorProvinciaReturnType resul = null;
            try
            {
                resul = service.consultarLocalidadesPorProvincia(request);
                Log(LogToAFIP(request, null, string.Empty, string.Empty).ToString(), "Success");
                Log(LogToAFIP(null, resul, string.Empty, string.Empty).ToString(), "Respuesta");

            }
            catch (System.Exception e)
            {
                Log(LogToAFIP(request, null, string.Empty, string.Empty).ToString(), "BeginError");
                string StackTrace = string.Empty;
                var message = new StringBuilder();
                StackTrace = String.Format("{0}\n{1}", e.Message, e.StackTrace);
                int bucle = 0;

                while (e.InnerException != null)
                {
                    bucle += 1;
                    message.AppendLine("----------------------Exception " + bucle.ToString() + "-----------------------");
                    message.AppendLine();
                    message.AppendLine(e.InnerException.Message);
                    message.AppendLine();
                    message.AppendLine(e.InnerException.StackTrace);
                    message.AppendLine();
                    message.AppendLine("-------------------------------------------------------------------------------");

                    e = e.InnerException;
                }

                if (message != null && Environment.MachineName.ToUpper() != "WI7-SIS22N-ADM")
                    SendEmail("Exception consultarCosechas", StackTrace, "SPoszalski@cresud.com.ar");

                Log(StackTrace, "Error");
            }
            return resul.arrayLocalidades;
        }

        public provinciaType[] consultarProvincias()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback
            = (sender, certificate, chain, sslPolicyErrors) => { return true; };

            CTGService_v3.CTGService_v30 service = new CTGService_v30();
            service.Proxy = WebProxy;
            service.Timeout = 3600000;
            service.Url = CTGServiceURL;
            string url = service.Url;

            consultarProvinciasRequestType request = new consultarProvinciasRequestType();
            request.auth = new authType();
            request.auth.token = afipAuth.Token;
            request.auth.sign = afipAuth.Sign;
            request.auth.cuitRepresentado = afipAuth.CuitRepresentado;

            consultarProvinciasReturnType resul = null;
            try
            {
                resul = service.consultarProvincias(request);
                Log(LogToAFIP(request, null, string.Empty, string.Empty).ToString(), "Success");
                Log(LogToAFIP(null, resul, string.Empty, string.Empty).ToString(), "Respuesta");

            }
            catch (System.Exception e)
            {
                Log(LogToAFIP(request, null, string.Empty, string.Empty).ToString(), "BeginError");
                string StackTrace = string.Empty;
                var message = new StringBuilder();
                StackTrace = String.Format("{0}\n{1}", e.Message, e.StackTrace);
                int bucle = 0;

                while (e.InnerException != null)
                {
                    bucle += 1;
                    message.AppendLine("----------------------Exception " + bucle.ToString() + "-----------------------");
                    message.AppendLine();
                    message.AppendLine(e.InnerException.Message);
                    message.AppendLine();
                    message.AppendLine(e.InnerException.StackTrace);
                    message.AppendLine();
                    message.AppendLine("-------------------------------------------------------------------------------");

                    e = e.InnerException;
                }

                if (message != null && Environment.MachineName.ToUpper() != "WI7-SIS22N-ADM")
                    SendEmail("Exception consultarCosechas", StackTrace, "SPoszalski@cresud.com.ar");

                Log(StackTrace, "Error");
                throw e;
            }
            return resul.arrayProvincias; // totales;
        }

        public long[] consultarEstablecimientos()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback
            = (sender, certificate, chain, sslPolicyErrors) => { return true; };

            CTGService_v3.CTGService_v30 service = new CTGService_v30();
            service.Proxy = WebProxy;
            service.Timeout = 3600000;
            service.Url = CTGServiceURL;
            string url = service.Url;

            consultarEstablecimientosRequestType request = new consultarEstablecimientosRequestType();
            request.auth = new authType();
            request.auth.token = afipAuth.Token;
            request.auth.sign = afipAuth.Sign;
            request.auth.cuitRepresentado = afipAuth.CuitRepresentado;

            consultarEstablecimientosReturnType resul = null;
            try
            {
                resul = service.consultarEstablecimientos(request);
                Log(LogToAFIP(request, null, string.Empty, string.Empty).ToString(), "Success");
                Log(LogToAFIP(null, resul, string.Empty, string.Empty).ToString(), "Respuesta");

            }
            catch (System.Exception e)
            {
                Log(LogToAFIP(request, null, string.Empty, string.Empty).ToString(), "BeginError");
                string StackTrace = string.Empty;
                var message = new StringBuilder();
                StackTrace = String.Format("{0}\n{1}", e.Message, e.StackTrace);
                int bucle = 0;

                while (e.InnerException != null)
                {
                    bucle += 1;
                    message.AppendLine("----------------------Exception " + bucle.ToString() + "-----------------------");
                    message.AppendLine();
                    message.AppendLine(e.InnerException.Message);
                    message.AppendLine();
                    message.AppendLine(e.InnerException.StackTrace);
                    message.AppendLine();
                    message.AppendLine("-------------------------------------------------------------------------------");

                    e = e.InnerException;
                }

                if (message != null && Environment.MachineName.ToUpper() != "WI7-SIS22N-ADM")
                    SendEmail("Exception consultarCosechas", StackTrace, "SPoszalski@cresud.com.ar");

                Log(StackTrace, "Error");
                throw e;
            }
            return resul.arrayEstablecimientos;
        }

        public consultarConstanciaCTGPDFReturnType consultarConstanciaCTGPDF(Int64 Ctg)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback
            = (sender, certificate, chain, sslPolicyErrors) => { return true; };

            CTGService_v3.CTGService_v30 service = new CTGService_v30();
            service.Proxy = WebProxy;
            service.Timeout = 3600000;
            service.Url = CTGServiceURL;
            string url = service.Url;

            consultarConstanciaCTGPDFRequestType request = new consultarConstanciaCTGPDFRequestType();
            request.auth = new authType();
            request.auth.token = afipAuth.Token;
            request.auth.sign = afipAuth.Sign;
            request.auth.cuitRepresentado = afipAuth.CuitRepresentado;
            request.ctg = Ctg;

            consultarConstanciaCTGPDFReturnType resul = null;
            try
            {
                resul = service.consultarConstanciaCTGPDF(request);
                Log(LogToAFIP(request, null, string.Empty, string.Empty).ToString(), "Success");
                Log(LogToAFIP(null, resul, string.Empty, string.Empty).ToString(), "Respuesta");

            }
            catch (System.Exception e)
            {
                Log(LogToAFIP(request, null, string.Empty, string.Empty).ToString(), "BeginError");
                string StackTrace = string.Empty;
                var message = new StringBuilder();
                StackTrace = String.Format("{0}\n{1}", e.Message, e.StackTrace);
                int bucle = 0;

                while (e.InnerException != null)
                {
                    bucle += 1;
                    message.AppendLine("----------------------Exception " + bucle.ToString() + "-----------------------");
                    message.AppendLine();
                    message.AppendLine(e.InnerException.Message);
                    message.AppendLine();
                    message.AppendLine(e.InnerException.StackTrace);
                    message.AppendLine();
                    message.AppendLine("-------------------------------------------------------------------------------");

                    e = e.InnerException;
                }

                if (message != null && Environment.MachineName.ToUpper() != "WI7-SIS22N-ADM")
                    SendEmail("Exception consultarCosechas", StackTrace, "SPoszalski@cresud.com.ar");

                Log(StackTrace, "Error");
                throw e;
            }
            return resul;
        }

        public String consultarCTGExcel(Int64 Ctg)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback
            = (sender, certificate, chain, sslPolicyErrors) => { return true; };

            CTGService_v3.CTGService_v30 service = new CTGService_v30();
            service.Proxy = WebProxy;
            service.Timeout = 3600000;
            service.Url = CTGServiceURL;
            string url = service.Url;

            consultarCTGRequestType request = new consultarCTGRequestType();
            request.auth = new authType();
            request.auth.token = afipAuth.Token;
            request.auth.sign = afipAuth.Sign;
            request.auth.cuitRepresentado = afipAuth.CuitRepresentado;
            request.consultarCTGDatos = new consultarCTGDatosType();

            consultarCTGExcelReturnType resul = null;
            try
            {
                resul = service.consultarCTGExcel(request);
                Log(LogToAFIP(request, null, string.Empty, string.Empty).ToString(), "Success");
                Log(LogToAFIP(null, resul, string.Empty, string.Empty).ToString(), "Respuesta");

            }
            catch (System.Exception e)
            {
                Log(LogToAFIP(request, null, string.Empty, string.Empty).ToString(), "BeginError");
                string StackTrace = string.Empty;
                var message = new StringBuilder();
                StackTrace = String.Format("{0}\n{1}", e.Message, e.StackTrace);
                int bucle = 0;

                while (e.InnerException != null)
                {
                    bucle += 1;
                    message.AppendLine("----------------------Exception " + bucle.ToString() + "-----------------------");
                    message.AppendLine();
                    message.AppendLine(e.InnerException.Message);
                    message.AppendLine();
                    message.AppendLine(e.InnerException.StackTrace);
                    message.AppendLine();
                    message.AppendLine("-------------------------------------------------------------------------------");

                    e = e.InnerException;
                }

                if (message != null && Environment.MachineName.ToUpper() != "WI7-SIS22N-ADM")
                    SendEmail("Exception consultarCosechas", StackTrace, "SPoszalski@cresud.com.ar");

                Log(StackTrace, "Error");
                throw e;
            }
            return resul.archivo;
        }

        public operacionCTGReturnType anularCTG(Int64 NroCartaDePorte, Int64 Ctg)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback
            = (sender, certificate, chain, sslPolicyErrors) => { return true; };

            CTGService_v3.CTGService_v30 service = new CTGService_v30();
            service.Proxy = WebProxy;
            service.Timeout = 3600000;
            service.Url = CTGServiceURL;
            string url = service.Url;

            anularCTGRequestType request = new anularCTGRequestType();
            request.auth = new authType();
            request.auth.token = afipAuth.Token;
            request.auth.sign = afipAuth.Sign;
            request.auth.cuitRepresentado = afipAuth.CuitRepresentado;
            request.datosAnularCTG = new datosCTGType();
            request.datosAnularCTG.cartaPorte = NroCartaDePorte;
            request.datosAnularCTG.ctg = Ctg;

            operacionCTGReturnType resul = null;
            try
            {
                resul = service.anularCTG(request);
                Log(LogToAFIP(request, null, string.Empty, string.Empty).ToString(), "Success");
                Log(LogToAFIP(null, resul, string.Empty, string.Empty).ToString(), "Respuesta");
            }
            catch (System.Exception e)
            {
                Log(LogToAFIP(request, null, string.Empty, string.Empty).ToString(), "BeginError");
                string StackTrace = string.Empty;
                var message = new StringBuilder();
                StackTrace = String.Format("{0}\n{1}", e.Message, e.StackTrace);
                int bucle = 0;

                while (e.InnerException != null)
                {
                    bucle += 1;
                    message.AppendLine("----------------------Exception " + bucle.ToString() + "-----------------------");
                    message.AppendLine();
                    message.AppendLine(e.InnerException.Message);
                    message.AppendLine();
                    message.AppendLine(e.InnerException.StackTrace);
                    message.AppendLine();
                    message.AppendLine("-------------------------------------------------------------------------------");

                    e = e.InnerException;
                }

                if (message != null && Environment.MachineName.ToUpper() != "WI7-SIS22N-ADM")
                    SendEmail("Exception consultarCosechas", StackTrace, "SPoszalski@cresud.com.ar");

                Log(StackTrace, "Error");
                envioMailAlertaAfip("<b>Anular CTG</b> <br/>" + e.Message.ToString() + "<br/>cdp: " + NroCartaDePorte.ToString() + "<br/>ctg: " + Ctg.ToString());
                throw e;
            }
            return resul;
        }

        public consultarCTGActivosPorPatenteReturnType consultarCTGActivosPorPatente(string patente)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback
            = (sender, certificate, chain, sslPolicyErrors) => { return true; };

            CTGService_v3.CTGService_v30 service = new CTGService_v30();
            service.Proxy = WebProxy;
            service.Timeout = 3600000;
            service.Url = CTGServiceURL;
            string url = service.Url;

            consultarCTGActivosPorPatenteRequestType request = new consultarCTGActivosPorPatenteRequestType();
            request.auth = new authType();
            request.auth.token = afipAuth.Token;
            request.auth.sign = afipAuth.Sign;
            request.auth.cuitRepresentado = afipAuth.CuitRepresentado;
            request.patente = patente;

            consultarCTGActivosPorPatenteReturnType resul = null;
            try
            {
                resul = service.consultarCTGActivosPorPatente(request);
                Log(LogToAFIP(request, null, string.Empty, string.Empty).ToString(), "Success");
                Log(LogToAFIP(null, resul, string.Empty, string.Empty).ToString(), "Respuesta");

            }
            catch (System.Exception e)
            {
                Log(LogToAFIP(request, null, string.Empty, string.Empty).ToString(), "BeginError");
                string StackTrace = string.Empty;
                var message = new StringBuilder();
                StackTrace = String.Format("{0}\n{1}", e.Message, e.StackTrace);
                int bucle = 0;

                while (e.InnerException != null)
                {
                    bucle += 1;
                    message.AppendLine("----------------------Exception " + bucle.ToString() + "-----------------------");
                    message.AppendLine();
                    message.AppendLine(e.InnerException.Message);
                    message.AppendLine();
                    message.AppendLine(e.InnerException.StackTrace);
                    message.AppendLine();
                    message.AppendLine("-------------------------------------------------------------------------------");

                    e = e.InnerException;
                }

                if (message != null && Environment.MachineName.ToUpper() != "WI7-SIS22N-ADM")
                    SendEmail("Exception consultarCosechas", StackTrace, "SPoszalski@cresud.com.ar");

                Log(StackTrace, "Error");
                throw e;
            }
            return resul;
        }

        public consultarCTGReturnType consultarCTG(DateTime fechadesde)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback
            = (sender, certificate, chain, sslPolicyErrors) => { return true; };

            CTGService_v3.CTGService_v30 service = new CTGService_v30();
            service.Proxy = WebProxy;
            service.Timeout = 3600000;
            service.Url = CTGServiceURL;
            string url = service.Url;

            if (Environment.MachineName.ToUpper() == "WI7-SIS22N-ADM" || Environment.MachineName.ToUpper() == "SRV-MS10-ADM")
            {
                Log("Integracion Afip Deshabilitada - consultarCTG Desactivado.", "Success");
                consultarCTGReturnType RetornarAfipHomo = new consultarCTGReturnType();
                RetornarAfipHomo.arrayErrores = new string[0];
                return RetornarAfipHomo;
            }

            consultarCTGRequestType request = new consultarCTGRequestType();
            request.auth = new authType();
            request.auth.token = afipAuth.Token;
            request.auth.sign = afipAuth.Sign;
            request.auth.cuitRepresentado = afipAuth.CuitRepresentado;
            request.consultarCTGDatos = new consultarCTGDatosType();
            request.consultarCTGDatos.fechaEmisionDesde = fechadesde.ToString("dd/MM/yyyy");

            try
            {
                var resul = service.consultarCTG(request);
                return resul;
                Log(LogToAFIP(request, null, string.Empty, string.Empty).ToString(), "Success");
                Log(LogToAFIP(null, resul, string.Empty, string.Empty).ToString(), "Respuesta");

            }
            catch (System.Exception e)
            {
                Log(LogToAFIP(request, null, string.Empty, string.Empty).ToString(), "BeginError");
                string StackTrace = string.Empty;
                var message = new StringBuilder();
                StackTrace = String.Format("{0}\n{1}", e.Message, e.StackTrace);
                int bucle = 0;

                while (e.InnerException != null)
                {
                    bucle += 1;
                    message.AppendLine("----------------------Exception " + bucle.ToString() + "-----------------------");
                    message.AppendLine();
                    message.AppendLine(e.InnerException.Message);
                    message.AppendLine();
                    message.AppendLine(e.InnerException.StackTrace);
                    message.AppendLine();
                    message.AppendLine("-------------------------------------------------------------------------------");

                    e = e.InnerException;
                }

                if (message != null && Environment.MachineName.ToUpper() != "WI7-SIS22N-ADM")
                    SendEmail("Exception consultarCosechas", StackTrace, "SPoszalski@cresud.com.ar");

                Log(StackTrace, "Error");
                envioMailAlertaAfip("<b>Consultar CTG</b> <br/>" + e.Message.ToString() + "<br/>fechaDesde: " + fechadesde.ToString("dd/MM/yyyy"));
                throw e;
            }
            return null;
        }

        public operacionCTGReturnType confirmarArribo(Solicitud sol)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback
            = (sender, certificate, chain, sslPolicyErrors) => { return true; };

            CTGService_v3.CTGService_v30 service = new CTGService_v30();
            service.Proxy = WebProxy;
            service.Timeout = 3600000;
            service.Url = CTGServiceURL;
            string url = service.Url;

            confirmarArriboRequestType request = new confirmarArriboRequestType();
            request.auth = new authType();
            request.auth.token = afipAuth.Token;
            request.auth.sign = afipAuth.Sign;
            request.auth.cuitRepresentado = afipAuth.CuitRepresentado;
            request.datosConfirmarArribo = new datosConfirmarArriboType();
            request.datosConfirmarArribo.ctg = Convert.ToInt64(sol.Ctg);
            request.datosConfirmarArribo.cantKilosCartaPorte = sol.KilogramosEstimados;
            request.datosConfirmarArribo.cartaPorte = Convert.ToInt64(sol.NumeroCartaDePorte);
            request.datosConfirmarArribo.consumoPropio = "S";

            Int64 cuitTransportista = 0;

            if (sol.ClientePagadorDelFlete != null && sol.ClientePagadorDelFlete.IdCliente > 0 && sol.ClientePagadorDelFlete.EsEmpresa())
            {
                if (sol.ProveedorTransportista != null)
                    cuitTransportista = Convert.ToInt64(sol.ProveedorTransportista.NumeroDocumento);
            }
            else
            {
                if (sol.ChoferTransportista != null)
                    cuitTransportista = Convert.ToInt64(sol.ChoferTransportista.Cuit);
            }

            request.datosConfirmarArribo.cuitTransportista = cuitTransportista;
            request.datosConfirmarArribo.establecimientoSpecified = false;
            if (!String.IsNullOrEmpty(sol.IdEstablecimientoProcedencia.EstablecimientoAfip.Trim()))
            {
                request.datosConfirmarArribo.establecimiento = Convert.ToInt64(sol.IdEstablecimientoProcedencia.EstablecimientoAfip.Trim());
                request.datosConfirmarArribo.establecimientoSpecified = true;
                request.datosConfirmarArribo.consumoPropio = "N";
            }

            operacionCTGReturnType resul = null;
            try
            {
                resul = service.confirmarArribo(request);
                Log(LogToAFIP(request, null, string.Empty, string.Empty).ToString(), "Success");
                Log(LogToAFIP(null, resul, string.Empty, string.Empty).ToString(), "Respuesta");

            }
            catch (System.Exception e)
            {
                Log(LogToAFIP(request, null, string.Empty, string.Empty).ToString(), "BeginError");
                string StackTrace = string.Empty;
                var message = new StringBuilder();
                StackTrace = String.Format("{0}\n{1}", e.Message, e.StackTrace);
                int bucle = 0;

                while (e.InnerException != null)
                {
                    bucle += 1;
                    message.AppendLine("----------------------Exception " + bucle.ToString() + "-----------------------");
                    message.AppendLine();
                    message.AppendLine(e.InnerException.Message);
                    message.AppendLine();
                    message.AppendLine(e.InnerException.StackTrace);
                    message.AppendLine();
                    message.AppendLine("-------------------------------------------------------------------------------");

                    e = e.InnerException;
                }

                if (message != null && Environment.MachineName.ToUpper() != "WI7-SIS22N-ADM")
                    SendEmail("Exception consultarCosechas", StackTrace, "SPoszalski@cresud.com.ar");

                Log(StackTrace, "Error");
                envioMailAlertaAfip("<b>Confirmar Arribo</b> <br/>" + e.Message.ToString() + "<br/>NumeroCartadeporte: " + sol.NumeroCartaDePorte + "<br/>UsuarioCreacion: " + sol.UsuarioCreacion);
                throw e;
            }
            return resul;
        }

        public confirmarDefinitivoReturnType confirmarDefinitivo(Solicitud sol)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback
            = (sender, certificate, chain, sslPolicyErrors) => { return true; };

            CTGService_v3.CTGService_v30 service = new CTGService_v30();
            service.Proxy = WebProxy;
            service.Timeout = 3600000;
            service.Url = CTGServiceURL;
            string url = service.Url;

            confirmarDefinitivoRequestType request = new confirmarDefinitivoRequestType();
            request.auth = new authType();
            request.auth.token = afipAuth.Token;
            request.auth.sign = afipAuth.Sign;
            request.auth.cuitRepresentado = afipAuth.CuitRepresentado;
            request.datosConfirmarDefinitivo = new datosConfirmarDefinitivoType();
            request.datosConfirmarDefinitivo.ctg = Convert.ToInt64(sol.Ctg);
            request.datosConfirmarDefinitivo.cartaPorte = Convert.ToInt64(sol.NumeroCartaDePorte);
            confirmarDefinitivoReturnType resul = null;

            try
            {
                resul = service.confirmarDefinitivo(request);
                Log(LogToAFIP(request, null, string.Empty, string.Empty).ToString(), "Success");
                Log(LogToAFIP(null, resul, string.Empty, string.Empty).ToString(), "Respuesta");

            }
            catch (System.Exception e)
            {
                Log(LogToAFIP(request, null, string.Empty, string.Empty).ToString(), "BeginError");
                string StackTrace = string.Empty;
                var message = new StringBuilder();
                StackTrace = String.Format("{0}\n{1}", e.Message, e.StackTrace);
                int bucle = 0;

                while (e.InnerException != null)
                {
                    bucle += 1;
                    message.AppendLine("----------------------Exception " + bucle.ToString() + "-----------------------");
                    message.AppendLine();
                    message.AppendLine(e.InnerException.Message);
                    message.AppendLine();
                    message.AppendLine(e.InnerException.StackTrace);
                    message.AppendLine();
                    message.AppendLine("-------------------------------------------------------------------------------");

                    e = e.InnerException;
                }

                if (message != null && Environment.MachineName.ToUpper() != "WI7-SIS22N-ADM")
                    SendEmail("Exception consultarCosechas", StackTrace, "SPoszalski@cresud.com.ar");

                Log(StackTrace, "Error");
                throw e;
            }
            return resul;
        }

        /// <summary>
        /// Cambio de destino
        /// </summary>
        /// <param name="sol">Una Solicitud: datos que requiere son: cartaPorte, codigoLocalidadDestino, ctg, cuitDestinatario, cuitDestinatario, cuitDestino</param>
        /// <returns></returns>
        public operacionCTGReturnType cambiarDestinoDestinatarioCTGRechazado(Solicitud sol)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback
            = (sender, certificate, chain, sslPolicyErrors) => { return true; };

            CTGService_v3.CTGService_v30 service = new CTGService_v30();
            service.Proxy = WebProxy;
            service.Timeout = 3600000;
            service.Url = CTGServiceURL;
            string url = service.Url;

            if (Environment.MachineName.ToUpper() == "WI7-SIS22N-ADM" || Environment.MachineName.ToUpper() == "SRV-MS10-ADM")
            {
                Log("Integracion Afip Deshabilitada - CambioDestino automatico.", "Success");
                datosOperacionCTGResponseType Response = new datosOperacionCTGResponseType();
                operacionCTGReturnType RetornarAfipHomo = new operacionCTGReturnType();

                RetornarAfipHomo.datosResponse = Response;
                RetornarAfipHomo.datosResponse.cartaPorte = Convert.ToInt64(sol.NumeroCartaDePorte);
                RetornarAfipHomo.datosResponse.ctg = Convert.ToInt64(sol.Ctg);
                RetornarAfipHomo.datosResponse.codigoOperacion = Convert.ToInt64("1234");
                RetornarAfipHomo.datosResponse.fechaHora = DateTime.Now.ToString();
                RetornarAfipHomo.arrayErrores = new string[0];

                return RetornarAfipHomo;
            }

            cambiarDestinoDestinatarioCTGRechazadoRequestType request = new cambiarDestinoDestinatarioCTGRechazadoRequestType();
            request.auth = new authType();
            request.auth.token = afipAuth.Token;
            request.auth.sign = afipAuth.Sign;
            request.auth.cuitRepresentado = afipAuth.CuitRepresentado;
            request.datosCambiarDestinoDestinatarioCTGRechazado = new datosCambiarDestinoDestinatarioCTGRechazadoType();
            request.datosCambiarDestinoDestinatarioCTGRechazado.cartaPorte = Convert.ToInt64(sol.NumeroCartaDePorte);
            request.datosCambiarDestinoDestinatarioCTGRechazado.codigoLocalidadDestino = sol.IdEstablecimientoDestinoCambio.Localidad.Codigo;
            request.datosCambiarDestinoDestinatarioCTGRechazado.ctg = Convert.ToInt64(sol.Ctg);
            request.datosCambiarDestinoDestinatarioCTGRechazado.cuitDestinatario = Convert.ToInt64(sol.ClienteDestinatarioCambio.Cuit);
            request.datosCambiarDestinoDestinatarioCTGRechazado.cuitDestino = Convert.ToInt64(sol.IdEstablecimientoDestinoCambio.IdInterlocutorDestinatario.Cuit);
            request.datosCambiarDestinoDestinatarioCTGRechazado.kmARecorrer = (uint)sol.KmRecorridos;

            operacionCTGReturnType resul = null;
            try
            {
                resul = service.cambiarDestinoDestinatarioCTGRechazado(request);
                Log(LogToAFIP(request, null, string.Empty, string.Empty).ToString(), "Success");
                Log(LogToAFIP(null, resul, string.Empty, string.Empty).ToString(), "Respuesta");

            }
            catch (System.Exception e)
            {
                Log(LogToAFIP(request, null, string.Empty, string.Empty).ToString(), "BeginError");
                string StackTrace = string.Empty;
                var message = new StringBuilder();
                StackTrace = String.Format("{0}\n{1}", e.Message, e.StackTrace);
                int bucle = 0;

                while (e.InnerException != null)
                {
                    bucle += 1;
                    message.AppendLine("----------------------Exception " + bucle.ToString() + "-----------------------");
                    message.AppendLine();
                    message.AppendLine(e.InnerException.Message);
                    message.AppendLine();
                    message.AppendLine(e.InnerException.StackTrace);
                    message.AppendLine();
                    message.AppendLine("-------------------------------------------------------------------------------");

                    e = e.InnerException;
                }

                if (message != null && Environment.MachineName.ToUpper() != "WI7-SIS22N-ADM")
                    SendEmail("Exception consultarCosechas", StackTrace, "SPoszalski@cresud.com.ar");

                Log(StackTrace, "Error");
                envioMailAlertaAfip("<b>cambiarDestinoDestinatarioCTGRechazado</b> <br/>" + e.Message.ToString() + "<br/>NumeroCartadeporte: " + sol.NumeroCartaDePorte + "<br/>UsuarioCreacion: " + sol.UsuarioCreacion);
                throw e;
            }

            return resul;
        }

        /// <summary>
        /// Desvio CTG a otro destino
        /// </summary>
        /// <param name="sol">Una Solicitud, con estos datos: cuitDestino, cartaPorte, ctg</param>
        /// <returns></returns>
        public operacionCTGReturnType desviarCTGAOtroDestino(Solicitud sol)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback
            = (sender, certificate, chain, sslPolicyErrors) => { return true; };

            CTGService_v3.CTGService_v30 service = new CTGService_v30();
            service.Proxy = WebProxy;
            service.Timeout = 3600000;
            service.Url = CTGServiceURL;
            string url = service.Url;

            desviarCTGAOtroDestinoRequestType request = new desviarCTGAOtroDestinoRequestType();
            request.auth = new authType();
            request.auth.token = afipAuth.Token;
            request.auth.sign = afipAuth.Sign;
            request.auth.cuitRepresentado = afipAuth.CuitRepresentado;
            request.cuitDestino = Convert.ToInt64(sol.ClienteDestino.Cuit);
            request.datosDesviarCTG.cartaPorte = Convert.ToInt64(sol.NumeroCartaDePorte);
            request.datosDesviarCTG.ctg = Convert.ToInt64(sol.Ctg);

            operacionCTGReturnType resul = null;
            try
            {
                resul = service.desviarCTGAOtroDestino(request);
                Log(LogToAFIP(request, null, string.Empty, string.Empty).ToString(), "Success");
                Log(LogToAFIP(null, resul, string.Empty, string.Empty).ToString(), "Respuesta");

            }
            catch (System.Exception e)
            {
                Log(LogToAFIP(request, null, string.Empty, string.Empty).ToString(), "BeginError");
                string StackTrace = string.Empty;
                var message = new StringBuilder();
                StackTrace = String.Format("{0}\n{1}", e.Message, e.StackTrace);
                int bucle = 0;

                while (e.InnerException != null)
                {
                    bucle += 1;
                    message.AppendLine("----------------------Exception " + bucle.ToString() + "-----------------------");
                    message.AppendLine();
                    message.AppendLine(e.InnerException.Message);
                    message.AppendLine();
                    message.AppendLine(e.InnerException.StackTrace);
                    message.AppendLine();
                    message.AppendLine("-------------------------------------------------------------------------------");

                    e = e.InnerException;
                }

                if (message != null && Environment.MachineName.ToUpper() != "WI7-SIS22N-ADM")
                    SendEmail("Exception consultarCosechas", StackTrace, "SPoszalski@cresud.com.ar");

                Log(StackTrace, "Error");
                throw e;
            }
            return resul;
        }

        /// <summary>
        /// Desviar CTG a otro establecimiento
        /// </summary>
        /// <param name="sol">Una Solicitud con los datos: cartaPorte, ctg, codigoLocalidadDestino </param>
        /// <returns></returns>
        public operacionCTGReturnType desviarCTGAOtroEstablecimiento(Solicitud sol)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback
            = (sender, certificate, chain, sslPolicyErrors) => { return true; };

            CTGService_v3.CTGService_v30 service = new CTGService_v30();
            service.Proxy = WebProxy;
            service.Timeout = 3600000;
            service.Url = CTGServiceURL;
            string url = service.Url;

            desviarCTGAOtroEstablecimientoRequestType request = new desviarCTGAOtroEstablecimientoRequestType();
            request.auth = new authType();
            request.auth.token = afipAuth.Token;
            request.auth.sign = afipAuth.Sign;
            request.auth.cuitRepresentado = afipAuth.CuitRepresentado;
            request.datosDesviarCTG = new datosDesviarCTGType();
            request.datosDesviarCTG.cartaPorte = Convert.ToInt64(sol.NumeroCartaDePorte);
            request.datosDesviarCTG.ctg = Convert.ToInt64(sol.Ctg);
            request.datosDesviarCTG.codigoLocalidadDestino = sol.IdEstablecimientoDestino.Localidad.Codigo;

            operacionCTGReturnType resul = null;
            try
            {
                resul = service.desviarCTGAOtroEstablecimiento(request);
                Log(LogToAFIP(request, null, string.Empty, string.Empty).ToString(), "Success");
                Log(LogToAFIP(null, resul, string.Empty, string.Empty).ToString(), "Respuesta");

            }
            catch (System.Exception e)
            {
                Log(LogToAFIP(request, null, string.Empty, string.Empty).ToString(), "BeginError");
                string StackTrace = string.Empty;
                var message = new StringBuilder();
                StackTrace = String.Format("{0}\n{1}", e.Message, e.StackTrace);
                int bucle = 0;

                while (e.InnerException != null)
                {
                    bucle += 1;
                    message.AppendLine("----------------------Exception " + bucle.ToString() + "-----------------------");
                    message.AppendLine();
                    message.AppendLine(e.InnerException.Message);
                    message.AppendLine();
                    message.AppendLine(e.InnerException.StackTrace);
                    message.AppendLine();
                    message.AppendLine("-------------------------------------------------------------------------------");

                    e = e.InnerException;
                }

                if (message != null && Environment.MachineName.ToUpper() != "WI7-SIS22N-ADM")
                    SendEmail("Exception consultarCosechas", StackTrace, "SPoszalski@cresud.com.ar");

                Log(StackTrace, "Error");
                throw e;
            }
            return resul;
        }

        public operacionCTGReturnType rechazarCTG(Solicitud sol)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback
            = (sender, certificate, chain, sslPolicyErrors) => { return true; };

            CTGService_v3.CTGService_v30 service = new CTGService_v30();
            service.Proxy = WebProxy;
            service.Timeout = 3600000;
            service.Url = CTGServiceURL;
            string url = service.Url;

            rechazarCTGRequestType request = new rechazarCTGRequestType();
            request.auth = new authType();
            request.auth.token = afipAuth.Token;
            request.auth.sign = afipAuth.Sign;
            request.auth.cuitRepresentado = afipAuth.CuitRepresentado;
            request.datosRechazarCTG = new datosRechazarCTGType();
            request.datosRechazarCTG.cartaPorte = Convert.ToInt64(sol.NumeroCartaDePorte);
            request.datosRechazarCTG.ctg = Convert.ToInt64(sol.Ctg);
            request.datosRechazarCTG.motivoRechazo = string.Empty;

            operacionCTGReturnType resul = null;
            try
            {
                resul = service.rechazarCTG(request);
                Log(LogToAFIP(request, null, string.Empty, string.Empty).ToString(), "Success");
                Log(LogToAFIP(null, resul, string.Empty, string.Empty).ToString(), "Respuesta");

            }
            catch (System.Exception e)
            {
                Log(LogToAFIP(request, null, string.Empty, string.Empty).ToString(), "BeginError");
                string StackTrace = string.Empty;
                var message = new StringBuilder();
                StackTrace = String.Format("{0}\n{1}", e.Message, e.StackTrace);
                int bucle = 0;

                while (e.InnerException != null)
                {
                    bucle += 1;
                    message.AppendLine("----------------------Exception " + bucle.ToString() + "-----------------------");
                    message.AppendLine();
                    message.AppendLine(e.InnerException.Message);
                    message.AppendLine();
                    message.AppendLine(e.InnerException.StackTrace);
                    message.AppendLine();
                    message.AppendLine("-------------------------------------------------------------------------------");

                    e = e.InnerException;
                }

                if (message != null && Environment.MachineName.ToUpper() != "WI7-SIS22N-ADM")
                    SendEmail("Exception consultarCosechas", StackTrace, "SPoszalski@cresud.com.ar");

                Log(StackTrace, "Error");
                throw e;
            }
            return resul;
        }

        public solicitarCTGReturnType solicitarCTGDatoPendiente(Solicitud sol)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback
            = (sender, certificate, chain, sslPolicyErrors) => { return true; };

            CTGService_v3.CTGService_v30 service = new CTGService_v30();
            service.Proxy = WebProxy;
            service.Timeout = 3600000;
            service.Url = CTGServiceURL;
            string url = service.Url;

            solicitarCTGDatoPendienteRequestType request = new solicitarCTGDatoPendienteRequestType();
            request.auth = new authType();
            request.auth.token = afipAuth.Token;
            request.auth.sign = afipAuth.Sign;
            request.auth.cuitRepresentado = afipAuth.CuitRepresentado;
            request.datosSolicitarCTGDatoPendiente = new datosSolicitarCTGDatoPendienteType();
            request.datosSolicitarCTGDatoPendiente.cantHoras = Convert.ToInt32(sol.CantHoras);
            request.datosSolicitarCTGDatoPendiente.cantHorasSpecified = true;
            request.datosSolicitarCTGDatoPendiente.cartaPorte = Convert.ToInt64(sol.NumeroCartaDePorte);
            request.datosSolicitarCTGDatoPendiente.cuitTransportistaSpecified = false;
            request.datosSolicitarCTGDatoPendiente.patente = sol.PatenteCamion;

            solicitarCTGReturnType resul = null;
            try
            {
                resul = service.solicitarCTGDatoPendiente(request);
                Log(LogToAFIP(request, null, string.Empty, string.Empty).ToString(), "Success");
                Log(LogToAFIP(null, resul, string.Empty, string.Empty).ToString(), "Respuesta");

            }
            catch (System.Exception e)
            {
                Log(LogToAFIP(request, null, string.Empty, string.Empty).ToString(), "BeginError");
                string StackTrace = string.Empty;
                var message = new StringBuilder();
                StackTrace = String.Format("{0}\n{1}", e.Message, e.StackTrace);
                int bucle = 0;

                while (e.InnerException != null)
                {
                    bucle += 1;
                    message.AppendLine("----------------------Exception " + bucle.ToString() + "-----------------------");
                    message.AppendLine();
                    message.AppendLine(e.InnerException.Message);
                    message.AppendLine();
                    message.AppendLine(e.InnerException.StackTrace);
                    message.AppendLine();
                    message.AppendLine("-------------------------------------------------------------------------------");

                    e = e.InnerException;
                }

                if (message != null && Environment.MachineName.ToUpper() != "WI7-SIS22N-ADM")
                    SendEmail("Exception consultarCosechas", StackTrace, "SPoszalski@cresud.com.ar");

                Log(StackTrace, "Error");
                throw e;
            }
            return resul;
        }

        public operacionCTGReturnType regresarAOrigenCTGRechazado(Solicitud sol)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback
            = (sender, certificate, chain, sslPolicyErrors) => { return true; };

            CTGService_v3.CTGService_v30 service = new CTGService_v30();
            service.Proxy = WebProxy;
            service.Timeout = 3600000;
            service.Url = CTGServiceURL;
            string url = service.Url;

            regresarAOrigenCTGRechazadoRequestType request = new regresarAOrigenCTGRechazadoRequestType();
            request.auth = new authType();
            request.auth.token = afipAuth.Token;
            request.auth.sign = afipAuth.Sign;
            request.auth.cuitRepresentado = afipAuth.CuitRepresentado;
            request.datosRegresarAOrigenCTGRechazado = new datosRegresarAOrigenCTGRechazadoType();
            request.datosRegresarAOrigenCTGRechazado.cartaPorte = Convert.ToInt64(sol.NumeroCartaDePorte);
            request.datosRegresarAOrigenCTGRechazado.ctg = Convert.ToInt64(sol.Ctg);
            request.datosRegresarAOrigenCTGRechazado.kmARecorrer = (uint)sol.KmRecorridos;

            operacionCTGReturnType resul = null;
            try
            {
                resul = service.regresarAOrigenCTGRechazado(request);
                Log(LogToAFIP(request, null, string.Empty, string.Empty).ToString(), "Success");
                Log(LogToAFIP(null, resul, string.Empty, string.Empty).ToString(), "Respuesta");

            }
            catch (System.Exception e)
            {
                Log(LogToAFIP(request, null, string.Empty, string.Empty).ToString(), "BeginError");
                string StackTrace = string.Empty;
                var message = new StringBuilder();
                StackTrace = String.Format("{0}\n{1}", e.Message, e.StackTrace);
                int bucle = 0;

                while (e.InnerException != null)
                {
                    bucle += 1;
                    message.AppendLine("----------------------Exception " + bucle.ToString() + "-----------------------");
                    message.AppendLine();
                    message.AppendLine(e.InnerException.Message);
                    message.AppendLine();
                    message.AppendLine(e.InnerException.StackTrace);
                    message.AppendLine();
                    message.AppendLine("-------------------------------------------------------------------------------");

                    e = e.InnerException;
                }

                if (message != null && Environment.MachineName.ToUpper() != "WI7-SIS22N-ADM")
                    SendEmail("Exception consultarCosechas", StackTrace, "SPoszalski@cresud.com.ar");

                Log(StackTrace, "Error");
                envioMailAlertaAfip("<b>regresarAOrigenCTGRechazado</b> <br/>" + e.Message.ToString() + "<br/>NumeroCartadeporte: " + sol.NumeroCartaDePorte + "<br/>UsuarioCreacion: " + sol.UsuarioCreacion);
                throw e;
            }
            return resul;
        }
        #endregion

        #region Actualizacion Datos Afip
        public void ActualizarEspecie()
        {
            Especie especie;

            especieType[] resul = consultarEspecies();
            if (resul != null)
            {
                foreach (especieType dato in resul)
                {
                    especie = EspecieDAO.Instance.GetOneByCodigo(dato.codigo);
                    if (especie == null)
                        especie = new Especie();

                    especie.Codigo = dato.codigo;
                    especie.Descripcion = dato.descripcion;

                    if (especie.IdEspecie == 0)
                        EspecieDAO.Instance.SaveOrUpdate(especie);

                    System.Threading.Thread.Sleep(2000);
                }
            }
        }

        public void ActualizarCosecha()
        {
            Cosecha cosecha = new Cosecha(); ;

            cosechaType[] resul = consultarCosechas();
            if (resul != null)
            {
                foreach (cosechaType dato in resul)
                {
                    cosecha = CosechaDAO.Instance.GetOneByCodigo(dato.codigo);
                    if (cosecha == null)
                        cosecha = new Cosecha();

                    cosecha.Codigo = dato.codigo;
                    cosecha.Descripcion = dato.descripcion;

                    if (cosecha.IdCosecha == 0)
                        CosechaDAO.Instance.SaveOrUpdate(cosecha);

                    System.Threading.Thread.Sleep(2000);
                }
            }
        }
        #endregion

        #region NetworkCredential
        public NetworkCredential NetWorkCredential
        {
            get
            {
                return new NetworkCredential(ConfigurationSettings.AppSettings["UserProxy"], ConfigurationSettings.AppSettings["PassProxy"], ConfigurationSettings.AppSettings["Domain"]);
            }
        }

        public WebProxy WebProxy
        {
            get
            {
                WebProxy proxy = new WebProxy(ConfigurationSettings.AppSettings["Proxy"]);
                proxy.Credentials = NetWorkCredential;
                return proxy;
            }
        }
        #endregion

        #region envioAlertas
        private void envioMailAlertaAfip(String mensaje)
        {
            EnvioMailDAO.Instance.sendMail(mensaje);
        }

        private static void Log(string sMessage, string action)
        {
            try
            {
                sMessage = sMessage.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", string.Empty).Replace(" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", string.Empty);
                if (!File.Exists(LogFileAfip))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(LogFileAfip));
                    File.CreateText(LogFileAfip).Close();
                }

                switch (action)
                {
                    case "Success":
                        File.AppendAllText(LogFileAfip, DateTime.Now.ToString() + " - Success: " + sMessage + "\r\n");
                        break;
                    case "Respuesta":
                        File.AppendAllText(LogFileAfip, DateTime.Now.ToString() + " - RespuestaAFIP: " + sMessage + "\r\n");
                        break;
                    case "BeginError":
                        File.AppendAllText(LogFileAfip, DateTime.Now.ToString() + " - BeginError: " + sMessage + "\r\n");
                        break;
                    case "Error":
                        File.AppendAllText(LogFileAfip, DateTime.Now.ToString() + " - StackTrace:\r\n" + sMessage + "\r\n");
                        break;
                }
            }
            catch (System.Exception ex)
            {
                string StackTrace = string.Empty;
                var message = new StringBuilder();

                StackTrace = String.Format("{0}\n{1}", ex.Message, ex.StackTrace);
                int bucle = 0;

                while (ex.InnerException != null)
                {
                    bucle += 1;
                    message.AppendLine("----------------------Exception Log " + bucle.ToString() + "-----------------------");
                    message.AppendLine();
                    message.AppendLine(ex.InnerException.Message);
                    message.AppendLine();
                    message.AppendLine(ex.InnerException.StackTrace);
                    message.AppendLine();
                    message.AppendLine("-------------------------------------------------------------------------------");

                    ex = ex.InnerException;
                }

                if (message != null)
                    SendEmail("Exception Log", StackTrace, "SPoszalski@cresud.com.ar");
            }
        }

        public StringWriter LogToAFIP(object request = null, object resul = null, string IdSolicitud = null, string NDP = null)
        {
            StringBuilder EnvioAFIP = new StringBuilder();

            if (IdSolicitud != string.Empty && NDP != string.Empty)
                EnvioAFIP.AppendLine(string.Format("log envío AFIP: IdSolicitud {0}, NumeroCDP {1} ", IdSolicitud, NDP));
            else
                EnvioAFIP.AppendLine("log envío AFIP");

            var stringwriter = new System.IO.StringWriter();
            if (request != null)
            {
                var serializer = new XmlSerializer(request.GetType());
                serializer.Serialize(stringwriter, request);
            }
            else
            {
                var serializer = new XmlSerializer(resul.GetType());
                serializer.Serialize(stringwriter, resul);
            }
            return stringwriter;
        }

        public static void SendEmail(string subject, string body, string to, string FileName = "")
        {
            string from = "no_reply@cresud.com.ar";
            MailAddress addressBCC = new MailAddress("SPoszalski@cresud.com.ar");

            string servidor = System.Environment.MachineName;

            to = to == string.Empty ? "SPoszalski@cresud.com.ar" : to;

            var message = new MailMessage(from, to, subject, body) { Priority = MailPriority.High };
            message.IsBodyHtml = true;
            message.Bcc.Add(addressBCC);

            if (FileName != string.Empty)
            {
                Attachment data = new Attachment(FileName);
                // Add time stamp information for the file.
                ContentDisposition disposition = data.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(FileName);
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(FileName);
                disposition.ReadDate = System.IO.File.GetLastAccessTime(FileName);
                // Add the file attachment to this e-mail message.
                message.Attachments.Add(data);
            }

            var client = new SmtpClient();
            SmtpClient SMTP = new SmtpClient();
            SMTP.Host = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            SMTP.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"].ToString());
            if (Environment.MachineName.ToUpper() != "WI7-SIS22N-ADM")
                SMTP.Send(message);
        }
        #endregion
    }
}

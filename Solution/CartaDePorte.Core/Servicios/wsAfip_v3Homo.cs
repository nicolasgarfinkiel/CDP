using CartaDePorte.Core.CTGService_v3;
using CartaDePorte.Core.DAO;
using CartaDePorte.Core.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Xml.Serialization;

namespace CartaDePorte.Core.Servicios
{
    public class wsAfip_v3Homo
    {
        static AfipAuth afipAuth = null;
        static String CTGServiceURL = null;
        private static readonly string LogFileAfip = AppDomain.CurrentDomain.BaseDirectory + @"LogAfip\" + string.Format("{0}{1}{2}.txt", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), DateTime.Now.Day.ToString());

        #region NetworkCredential
        public wsAfip_v3Homo()
        {
            afipAuth = AfipAuthDAO.Instance.Get(1);
            CTGServiceURL = ConfigurationSettings.AppSettings["CTGServiceURL"];
        }
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

        #region ConsultaAnulacionHomologacion
        public void CTGsPendientesResolucionHomo(CTGsPendientesResolucionReturnType Pendientes, CTGService_v30 service, CTGService_v3.authType auth)
        {
            try
            {
                foreach (var item in Pendientes.arrayCTGsConfirmadosAResolver)
                {
                    confirmarDefinitivoRequestType confirmDefinitivo = new confirmarDefinitivoRequestType();
                    datosConfirmarDefinitivoType confirmDefinitivoDatos = new datosConfirmarDefinitivoType();
                    confirmDefinitivo.auth = auth;
                    confirmDefinitivo.datosConfirmarDefinitivo = confirmDefinitivoDatos;
                    confirmDefinitivo.datosConfirmarDefinitivo.cartaPorte = item.cartaPorte;
                    confirmDefinitivo.datosConfirmarDefinitivo.codigoCosecha = "1314";
                    confirmDefinitivo.datosConfirmarDefinitivo.ctg = item.ctg;
                    confirmDefinitivo.datosConfirmarDefinitivo.especie = Convert.ToInt64("1");
                    confirmDefinitivo.datosConfirmarDefinitivo.especieSpecified = true;
                    confirmDefinitivo.datosConfirmarDefinitivo.pesoNeto = Convert.ToInt64("12345");
                    confirmDefinitivo.datosConfirmarDefinitivo.pesoNetoSpecified = true;

                    var ConfirmDef = service.confirmarDefinitivo(confirmDefinitivo);
                }

                foreach (var item in Pendientes.arrayCTGsOtorgadosAResolver)
                {
                    anularCTGRequestType anular = new anularCTGRequestType();
                    anular.datosAnularCTG = new datosCTGType();

                    anular.auth = auth;
                    anular.datosAnularCTG.cartaPorte = item.cartaPorte;
                    anular.datosAnularCTG.ctg = item.ctg;

                    var Anular = service.anularCTG(anular);
                }
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region AfipMethods
        public solicitarCTGReturnType solicitarCTGInicial(Solicitud solicitud)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => { return true; };
            CTGService_v30 service = new CTGService_v30();
            service.Proxy = WebProxy;
            service.Timeout = 3600000;
            service.Url = CTGServiceURL;

            solicitarCTGInicialRequestType request = new solicitarCTGInicialRequestType();
            request.auth = new authType();
            request.auth.token = afipAuth.Token;
            request.auth.sign = afipAuth.Sign;
            request.auth.cuitRepresentado = afipAuth.CuitRepresentado;

            if (Environment.MachineName.ToUpper() == "WI7-SIS22N-ADM")
            {
                ctgsPendientesResolucionRequestType Pendientes = new ctgsPendientesResolucionRequestType();
                Pendientes.auth = new authType();
                Pendientes.auth.token = afipAuth.Token;
                Pendientes.auth.sign = afipAuth.Sign;
                Pendientes.auth.cuitRepresentado = afipAuth.CuitRepresentado;
                CTGsPendientesResolucionReturnType PendientesResolucion = service.CTGsPendientesResolucion(Pendientes);

                if (PendientesResolucion.arrayCTGsConfirmadosAResolver.Count() > 0 ||
                PendientesResolucion.arrayCTGsOtorgadosAResolver.Count() > 0 ||
                PendientesResolucion.arrayCTGsRechazadosAResolver.Count() > 0)
                    new wsAfip_v3Homo().CTGsPendientesResolucionHomo(PendientesResolucion, service, Pendientes.auth);
            }

            request.datosSolicitarCTGInicial = new datosSolicitarCTGInicialType();
            CartaDePorte.Core.CTGService_v3.solicitarCTGReturnType resul;
            try
            {
                request.datosSolicitarCTGInicial.cartaPorte = Convert.ToInt64(solicitud.NumeroCartaDePorte);
                request.datosSolicitarCTGInicial.codigoEspecie = 23;
                request.datosSolicitarCTGInicial.cuitCanjeador = Convert.ToInt64("20267565393");
                request.datosSolicitarCTGInicial.cuitDestino = Convert.ToInt64("20267565393");
                request.datosSolicitarCTGInicial.cuitDestinatario = Convert.ToInt64("20267565393");
                request.datosSolicitarCTGInicial.codigoLocalidadOrigen = 3058;
                request.datosSolicitarCTGInicial.codigoLocalidadDestino = 3059;
                request.datosSolicitarCTGInicial.codigoCosecha = "1415";
                request.datosSolicitarCTGInicial.pesoNeto = 1000;
                request.datosSolicitarCTGInicial.cantHoras = 10;
                request.datosSolicitarCTGInicial.patente = "AAA000";
                request.datosSolicitarCTGInicial.cuitTransportista = Convert.ToInt64("2026756539");

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
                return resul2;
            }
            return resul;
        }
        #endregion

        #region Log


        private static void Log(string sMessage, string action)
        {
            try
            {
                sMessage = sMessage.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", string.Empty).Replace(" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", string.Empty);
                if (!File.Exists(LogFileAfip))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(LogFileAfip));
                    File.CreateText(LogFileAfip);
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

        public StringWriter LogToAFIP(solicitarCTGInicialRequestType request = null, solicitarCTGReturnType resul = null, string IdSolicitud = null, string NDP = null)
        {
            StringBuilder EnvioAFIP = new StringBuilder();

            EnvioAFIP.AppendLine(string.Format("log envío AFIP: IdSolicitud {0}, NumeroCDP {1} ", IdSolicitud, NDP));

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
            SMTP.Send(message);
        }
        #endregion
    }
}

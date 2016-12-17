using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Reflection;
using CartaDePorte.Core.Exception;
using CartaDePorte.Core.Domain;

namespace CartaDePorte.Core.DAO
{
    public class EnvioMailDAO : BaseDAO
    {
        private static EnvioMailDAO instance;
        public EnvioMailDAO() { }

        public static EnvioMailDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EnvioMailDAO();
                }
                return instance;
            }
        }

        public bool sendMail(Email email)
        {
            if (email == null)
                return false;

            bool result = true;

            try
            {
                MailMessage eMailMessage = new MailMessage();
                eMailMessage.To.Add(email.Destinatarios);

                if (!String.IsNullOrEmpty(email.CopiaDestinatarios))
                    eMailMessage.Bcc.Add(email.CopiaDestinatarios);

                eMailMessage.From = new MailAddress(email.Remitente);
                eMailMessage.Subject = email.Asunto;
                eMailMessage.Body = email.Cuerpo;
                eMailMessage.IsBodyHtml = true;

                foreach (Attachment attachment in email.DocumentosAdjuntos)
                    eMailMessage.Attachments.Add(attachment);

                SmtpClient SMTP = new SmtpClient();
                SMTP.Host = ConfigurationManager.AppSettings["SMTPHost"].ToString();
                SMTP.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"].ToString());

                SMTP.Send(eMailMessage);
                eMailMessage.Dispose();
            }
            catch { }
            return result;
        }

        public bool sendMail(String mensaje)
        {
            Email email = crearEmail(mensaje);

            if (email == null)
                return false;

            bool result = true;

            try
            {
                MailMessage eMailMessage = new MailMessage();
                eMailMessage.To.Add(email.Destinatarios);

                if (!String.IsNullOrEmpty(email.CopiaDestinatarios))
                    eMailMessage.Bcc.Add(email.CopiaDestinatarios);

                eMailMessage.From = new MailAddress(email.Remitente);
                eMailMessage.Subject = email.Asunto;
                eMailMessage.Body = email.Cuerpo;
                eMailMessage.IsBodyHtml = true;

                foreach (Attachment attachment in email.DocumentosAdjuntos)
                    eMailMessage.Attachments.Add(attachment);

                SmtpClient SMTP = new SmtpClient();
                SMTP.Host = ConfigurationManager.AppSettings["SMTPHost"].ToString();
                SMTP.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"].ToString());

                SMTP.Send(eMailMessage);

                eMailMessage.Dispose();
            }
            catch { }
            return result;
        }

        public Email crearEmail(String mensaje)
        {
            Email eMail = null;
            String destinatario = ConfigurationManager.AppSettings["AlertaDestinatarios"].ToString();
            String copias = ConfigurationManager.AppSettings["AlertaCopiaDestinatarios"].ToString();
            String remitente = ConfigurationManager.AppSettings["AlertaRemitente"].ToString();

            if (String.IsNullOrEmpty(destinatario) || String.IsNullOrEmpty(remitente))
                return null;

            if (!String.IsNullOrEmpty(destinatario) && !String.IsNullOrEmpty(remitente))
            {
                eMail = new Email();
                eMail.Destinatarios = destinatario;
                eMail.CopiaDestinatarios = copias;
                eMail.Remitente = remitente;
                eMail.Asunto = "Mensaje Carta de Porte " + Environment.MachineName;
                eMail.Cuerpo = mensaje;
            }
            return eMail;
        }
    }
}
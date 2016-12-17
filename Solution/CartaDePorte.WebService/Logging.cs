using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;

namespace CartaDePorte.WebService
{
    public class Logging
    {
        private static readonly string Mail = @ConfigurationManager.AppSettings.Get("LoggingMail");

        public static void SendEmail(string subject, string body, string FileName = "")
        {
            string from = "no_reply@irsacorp.com.ar";
            MailAddress addressBCC = new MailAddress(Mail);

            string servidor = System.Environment.MachineName;
            var message = new MailMessage(from, Mail, "[" + servidor + "] " + subject, body) { Priority = MailPriority.High };
            message.IsBodyHtml = true;
            message.Bcc.Add(addressBCC);

            if (FileName != string.Empty)
            {
                Attachment data = new Attachment(FileName);
                ContentDisposition disposition = data.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(FileName);
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(FileName);
                disposition.ReadDate = System.IO.File.GetLastAccessTime(FileName);
                message.Attachments.Add(data);
            }

            var client = new SmtpClient();
            client.Host = "SMTPCORP.irsa.corp.ar";
            client.Port = 25;
            client.Send(message);
        }

        public static string StackTrace(Exception ex)
        {
            var message = new StringBuilder();
            int bucle = 0;

            message.AppendLine(String.Format("{0}\n{1}", ex.Message, ex.StackTrace));

            while (ex.InnerException != null)
            {
                bucle += 1;
                message.AppendLine("----------------------Exception " + bucle.ToString() + "-----------------------");
                message.AppendLine();
                message.AppendLine(ex.InnerException.Message);
                message.AppendLine();
                message.AppendLine(ex.InnerException.StackTrace);
                message.AppendLine();
                message.AppendLine("-------------------------------------------------------------------------------");

                ex = ex.InnerException;
            }

            return message.ToString();
        }
    }
}
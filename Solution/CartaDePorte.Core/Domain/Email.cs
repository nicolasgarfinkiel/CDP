using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.IO;
using System.Data;

namespace CartaDePorte.Core.Domain
{
    public class Email
    {
        #region Propiedades Privadas

        private string _remitente;
        private string _destinatarios;
        private string _copiaDestinatarios;
        private string _asunto;
        private string _cuerpo;
        private IList<Attachment> _documentosAdjuntos;

        #endregion

        #region Propiedades Publicas

        public String Remitente
        {
            get { return _remitente; }
            set { _remitente = value; }
        }

        public String Destinatarios
        {
            get { return _destinatarios; }
            set { _destinatarios = value; }
        }

        public String CopiaDestinatarios
        {
            get { return _copiaDestinatarios; }
            set { _copiaDestinatarios = value; }
        }

        public String Asunto
        {
            get { return _asunto; }
            set { _asunto = value; }
        }

        public String Cuerpo
        {
            get { return _cuerpo; }
            set { _cuerpo = value; }
        }

        public IList<Attachment> DocumentosAdjuntos
        {
            get { return _documentosAdjuntos; }
            set { _documentosAdjuntos = value; }
        }

        #endregion

        public Email()
        {
            _documentosAdjuntos = new List<Attachment>();
        }

        public void AgregarDocumentoAdjunto(Attachment archivo)
        {
            this.DocumentosAdjuntos.Add(archivo);
        }

        public void AgregarDocumentoAdjunto(DataSet dataSet, String nombreArchivo)
        {
            byte[] xml = Encoding.ASCII.GetBytes(dataSet.GetXml());
            this.DocumentosAdjuntos.Add(new Attachment(new MemoryStream(xml), nombreArchivo));
        }

        public void BorrarDocumentosAdjuntos()
        {
            this.DocumentosAdjuntos.Clear();
        }
    }
}

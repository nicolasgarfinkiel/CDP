using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CartaDePorte.Core.Domain
{
    public class LogSap
    {
        public LogSap() { }

        private int idLogSap;

        public int IdLogSap
        {
            get { return idLogSap; }
            set { idLogSap = value; }
        }

        private string iDoc;

        public string IDoc
        {
            get { return iDoc; }
            set { iDoc = value; }
        }
        private string origen;

        public string Origen
        {
            get { return origen; }
            set { origen = value; }
        }
        private string nroDocumentoRE;

        public string NroDocumentoRE
        {
            get { return nroDocumentoRE; }
            set { nroDocumentoRE = value; }
        }
        private string nroDocumentoSap;

        public string NroDocumentoSap
        {
            get { return nroDocumentoSap; }
            set { nroDocumentoSap = value; }
        }
        private string tipoMensaje;

        public string TipoMensaje
        {
            get { return tipoMensaje; }
            set { tipoMensaje = value; }
        }
        private string textoMensaje;

        public string TextoMensaje
        {
            get { return textoMensaje; }
            set { textoMensaje = value; }
        }

        private int nroEnvio;

        public int NroEnvio
        {
            get { return nroEnvio; }
            set { nroEnvio = value; }
        }

        private DateTime fechaCreacion;

        public DateTime FechaCreacion
        {
            get { return fechaCreacion; }
            set { fechaCreacion = value; }
        }
    }
}

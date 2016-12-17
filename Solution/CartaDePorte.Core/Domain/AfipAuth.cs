using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CartaDePorte.Core.Domain
{
    public class AfipAuth
    {
        public AfipAuth() { }

        private int idAfipAuth;

        public int IdAfipAuth
        {
            get { return idAfipAuth; }
            set { idAfipAuth = value; }
        }

        private string token;
        public string Token
        {
            get { return token; }
            set { token = value; }
        }
        private string sign;
        public string Sign
        {
            get { return sign; }
            set { sign = value; }
        }
        private long cuitRepresentado;
        public long CuitRepresentado
        {
            get { return cuitRepresentado; }
            set { cuitRepresentado = value; }
        }

        private DateTime generationTime;

        public DateTime GenerationTime
        {
            get { return generationTime; }
            set { generationTime = value; }
        }
        private DateTime expirationTime;

        public DateTime ExpirationTime
        {
            get { return expirationTime; }
            set { expirationTime = value; }
        }
        private string service;

        public string Service
        {
            get { return service; }
            set { service = value; }
        }
        private string uniqueID;

        public string UniqueID
        {
            get { return uniqueID; }
            set { uniqueID = value; }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CartaDePorte.Core.Domain
{
    public class TipoDocumentoSAP
    {
        private int iDTipoDocumentoSAP;
        private string _nombre;
        private string _sap_Id;


        public int IDTipoDocumentoSAP
        {
            get { return iDTipoDocumentoSAP; }
            set { iDTipoDocumentoSAP = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public virtual string SAP_Id
        {
            get { return _sap_Id; }
            set { _sap_Id = value; }
        }

        public bool Validar()
        {
            return true;
        }

        public override string ToString()
        {
            return this.Nombre;
        }

    }
}

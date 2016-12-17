using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CartaDePorte.Core.Domain
{
    public class Proveedor
    {
        #region Privadas
        private int idProveedor;
        private string _sap_Id;
        private string _nombre;
        private TipoDocumentoSAP _tipoDocumento;
        private string _numeroDocumento;
        private bool _activo;
        private string _calle;
        private string _piso;
        private string _departamento;
        private string _numero;
        private string _cp;
        private string _ciudad;
        private string _pais;
        private bool esProspecto = false;
        #endregion
        #region Constructores
        public Proveedor() { }
        public Proveedor(string nombre, string sapid)
        {
            this._nombre = nombre;
            this._sap_Id = sapid;
        }
        #endregion
        #region Publicas
        public int IdProveedor
        {
            get { return idProveedor; }
            set { idProveedor = value; }
        }
        public string Sap_Id
        {
            get { return _sap_Id; }
            set { _sap_Id = value; }
        }
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        public TipoDocumentoSAP TipoDocumento
        {
            get { return _tipoDocumento; }
            set { _tipoDocumento = value; }
        }
        public string NumeroDocumento
        {
            get { return _numeroDocumento; }
            set { _numeroDocumento = value; }
        }
        public string Calle
        {
            get { return _calle; }
            set { _calle = value; }
        }
        public string Piso
        {
            get { return _piso; }
            set { _piso = value; }
        }
        public string Departamento
        {
            get { return _departamento; }
            set { _departamento = value; }
        }
        public string Numero
        {
            get { return _numero; }
            set { _numero = value; }
        }
        public string CP
        {
            get { return _cp; }
            set { _cp = value; }
        }
        public string Ciudad
        {
            get { return _ciudad; }
            set { _ciudad = value; }
        }
        public string Pais
        {
            get { return _pais; }
            set { _pais = value; }
        }
        public bool Activo
        {
            get { return _activo; }
            set { _activo = value; }
        }
        public string Domicilio
        {
            get
            {
                string domicilio = "";

                if (!String.IsNullOrEmpty(_calle))
                    domicilio = domicilio + _calle;

                if (!String.IsNullOrEmpty(_numero))
                    domicilio = domicilio + _numero;

                if (!String.IsNullOrEmpty(_piso))
                    domicilio = domicilio + " Piso: " + _piso;

                if (!String.IsNullOrEmpty(_departamento))
                    domicilio = domicilio + " Depto: " + _departamento;

                return domicilio;
            }
        }
        public bool EsProspecto
        {
            get { return this.esProspecto; }
            set { this.esProspecto = value; }
        }
        #endregion
                
        public bool Validar()
        {
            if (this.Nombre == null)
                throw new System.Exception();

            return true;
        }

        public override string ToString()
        {
            return this.Nombre;
        }



        public int IdSapOrganizacionDeVenta
        {
            get;
            set;
        }

    }
}

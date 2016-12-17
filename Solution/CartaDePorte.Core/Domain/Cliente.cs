using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CartaDePorte.Core.DAO;

namespace CartaDePorte.Core.Domain
{
    public class Cliente
    {
        #region Privadas
        private int _idCliente;
        private string _razonSocial;
        private string _nombreFantasia;
        private string _cuit;
        private TipoDocumentoSAP _tipoDocumento;
        private Cliente _clientePrincipal;
        private string _calle;
        private string _numero;
        private string _piso;
        private string _dto;
        private string _cp;
        private string _poblacion;
        private bool _activo;
        private string _grupoComercial;
        private string _claveGrupo;
        private string _tratamiento;
        private string _descripcionGe;
        private bool esProspecto = false;
        #endregion

        #region Constructores
        public Cliente() { }
        #endregion

        #region Publicas
        public int IdCliente
        {
            get { return _idCliente; }
            set { _idCliente = value; }
        }

        /// <summary>
        /// Razón social del cliente
        /// </summary>
        public string RazonSocial
        {
            get { return this._razonSocial; }
            set { this._razonSocial = value; }
        }

        /// <summary>
        /// Nombre de fantasía del cliente
        /// </summary>
        public string NombreFantasia
        {
            get { return this._nombreFantasia; }
            set { this._nombreFantasia = value; }
        }

        /// <summary>
        /// CUIT (Código único de Identificación Tributaria) del cliente
        /// </summary>
        public string Cuit
        {
            get { return this._cuit; }
            set { this._cuit = value; }
        }

        /// <summary>
        /// Tipo de documento del cliente
        /// </summary>
        public TipoDocumentoSAP TipoDocumento
        {
            get { return this._tipoDocumento; }
            set { this._tipoDocumento = value; }
        }

        /// <summary>
        /// Si tiene datos es un cliente subsidiario. En caso contrario, este es un cliente principal. 
        /// El dato en este campo es el IDCliente del cliente principal.
        /// </summary>
        public Cliente ClientePrincipal
        {
            get { return this._clientePrincipal; }
            set { this._clientePrincipal = value; }
        }

        /// <summary>
        /// Dirección del cliente (calle)
        /// </summary>
        public string Calle
        {
            get { return this._calle; }
            set { this._calle = value; }
        }

        /// <summary>
        /// Dirección del cliente (número)
        /// </summary>
        public string Numero
        {
            get { return this._numero; }
            set { this._numero = value; }
        }

        /// <summary>
        /// Dirección del cliente (Departamento)
        /// </summary>
        public string Dto
        {
            get { return this._dto; }
            set { this._dto = value; }
        }

        /// <summary>
        /// Dirección del cliente (Piso)
        /// </summary>
        public string Piso
        {
            get { return this._piso; }
            set { this._piso = value; }
        }

        /// <summary>
        /// Código postal
        /// </summary>
        /// 
        public string Cp
        {
            get { return this._cp; }
            set { this._cp = value; }
        }

        /// <summary>
        /// ???
        /// </summary>
        public string Poblacion
        {
            get { return this._poblacion; }
            set { this._poblacion = value; }
        }

        /// <summary>
        /// Define el estado del cliente. Si está Activo o no (borrado).
        /// </summary>
        public bool Activo
        {
            get { return this._activo; }
            set { this._activo = value; }
        }

        /// <summary>
        /// ???
        /// </summary>
        public string GrupoComercial
        {
            get { return this._grupoComercial; }
            set { this._grupoComercial = value; }
        }

        /// <summary>
        /// ???
        /// </summary>
        public string ClaveGrupo
        {
            get { return this._claveGrupo; }
            set { this._claveGrupo = value; }
        }
        /// <summary>
        /// ???
        /// </summary>
        public string Tratamiento
        {
            get { return this._tratamiento; }
            set { this._tratamiento = value; }
        }

        /// <summary>
        /// Descripcion Grupo Ecoomico.
        /// </summary>
        public string DescripcionGe
        {
            get { return this._descripcionGe; }
            set { this._descripcionGe = value; }
        }


        public bool EsProspecto
        {
            get { return this.esProspecto; }
            set { this.esProspecto = value; }
        }

        #endregion


        public bool Validar()
        {
            return true;
        }

        public override string ToString()
        {
            return this.RazonSocial;
        }

        public bool EsEmpresa()
        {
            bool esempresa = false;

            Empresa empresaPagadorFlete = EmpresaDAO.Instance.GetOneByIdCliente(IdCliente);
            if (empresaPagadorFlete != null && !String.IsNullOrEmpty(empresaPagadorFlete.IdSapOrganizacionDeVenta))
                esempresa = true;


            return esempresa;

        }
        public Empresa getEmpresa()
        {
            Empresa empresaPagadorFlete = EmpresaDAO.Instance.GetOneByIdCliente(IdCliente);
            if (empresaPagadorFlete != null && !String.IsNullOrEmpty(empresaPagadorFlete.IdSapOrganizacionDeVenta))
                return empresaPagadorFlete;

            return null;
        
        }


        public int IdSapOrganizacionDeVenta
        {
            get;
            set;
        }


    }
}

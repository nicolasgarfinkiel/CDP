using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CartaDePorte.Core.Domain
{
    public class Empresa
    {
        #region Privadas
        private int idEmpresa;
        private Cliente _cliente;
        private string descripcion;
        private string _idSapOrganizacionDeVenta;
        private string _idSapSector = "00";
        private string idSapCanalLocal;
        private string _idSapCanalExpor;
        private string _sap_Id;
        private string _idSapMoneda;
        #endregion

        #region Constructores
        public Empresa() { }
        #endregion

        #region Publicas
        public int IdEmpresa
        {
            get { return idEmpresa; }
            set { idEmpresa = value; }
        }
        public Cliente Cliente
        {
            get { return _cliente; }
            set { _cliente = value; }
        }
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        public string IdSapOrganizacionDeVenta
        {
            get { return _idSapOrganizacionDeVenta; }
            set { _idSapOrganizacionDeVenta = value; }
        }
        public string IdSapSector
        {
            get { return _idSapSector; }
            set { _idSapSector = value; }
        }
        public string IdSapCanalLocal
        {
            get { return idSapCanalLocal; }
            set { idSapCanalLocal = value; }
        }
        public string IdSapCanalExpor
        {
            get { return _idSapCanalExpor; }
            set { _idSapCanalExpor = value; }
        }
        public string Sap_Id
        {
            get { return _sap_Id; }
            set { _sap_Id = value; }
        }
        public string IdSapMoneda
        {
            get { return _idSapMoneda; }
            set { _idSapMoneda = value; }
        }        
        #endregion

        public bool Validar()
        {
            return true;
        }


        public override string ToString()
        {
            return this.Descripcion;
        }

    }


    public class EmpresaAdmin : Empresa
    {

        public int IdGrupoEmpresa { get; set;}

        public int IdApp { get; set; }

        public int IdPais { get; set; }

    }
}

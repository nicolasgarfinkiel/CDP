using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CartaDePorte.Core.Domain
{
    public class GrupoEmpresa
    {
        #region Constructores
        public GrupoEmpresa() { }
        #endregion

        #region Privadas
        private int idGrupoEmpresa;
        private string descripcion;
        private bool activo;
        private int idPais;
        private int idApp;
        private Pais pais;
        private Empresa empresa;
        #endregion

        public int IdGrupoEmpresa
        {
            get { return idGrupoEmpresa; }
            set { idGrupoEmpresa = value; }
        }
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        public bool Activo
        {
            get { return activo; }
            set { activo = value; }
        }
        public int IdPais
        {
            get { return idPais; }
            set { idPais = value; }
        }
        public int IdApp
        {
            get { return idApp; }
            set { idApp = value; }
        }
        public Pais Pais
        {
            get { return pais; }
            set { pais = value; }
        }

        public Empresa Empresa
        {
            get { return empresa; }
            set { empresa = value; }
        }
    }
}

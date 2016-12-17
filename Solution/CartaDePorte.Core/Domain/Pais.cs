using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CartaDePorte.Core.Domain
{
    public class Pais
    {
        #region Constructores
        public Pais() { }
        #endregion

        #region Privadas
        private int idPais;
        private string descripcion;
        #endregion

        public int IdPais
        {
            get { return idPais; }
            set { idPais = value; }
        }
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
    }
}

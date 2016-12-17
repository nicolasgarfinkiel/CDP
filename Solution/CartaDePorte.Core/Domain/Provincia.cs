using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CartaDePorte.Core.Domain
{
    /// <summary>
    /// Provincias de AFIP, Ws Metodo consultarProvincias
    /// </summary>
    public class Provincia
    {
        public Provincia() { }

        private int codigo;
        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private String descripcion;
        public String Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public override string ToString()
        {
            return this.Descripcion;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CartaDePorte.Core.Domain
{
    /// <summary>
    /// Partido por provincias... 
    /// </summary>
    public class Partido
    {
        public Partido() { }

        private Provincia idProvincia;

        public Provincia Provincia
        {
            get { return idProvincia; }
            set { idProvincia = value; }
        }
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

        private String nombreProvincia;
        public String NombreProvincia
        {
            get { return nombreProvincia; }
            set { nombreProvincia = value; }
        }

        public override string ToString()
        {
            return this.Descripcion + " (" + this.nombreProvincia + ")";
        }
    }
}

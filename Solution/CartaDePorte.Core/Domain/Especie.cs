using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CartaDePorte.Core.Domain
{
    public class Especie
    {
        public Especie() { }

        private int idEspecie;

        public int IdEspecie
        {
            get { return idEspecie; }
            set { idEspecie = value; }
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


        public override string ToString()
        {
            return this.Descripcion;
        }
    }
}

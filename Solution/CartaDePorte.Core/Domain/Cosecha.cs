using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CartaDePorte.Core.Domain
{
    public class Cosecha
    {

        public Cosecha() { }

        private int idCosecha;

        public int IdCosecha
        {
            get { return idCosecha; }
            set { idCosecha = value; }
        }
        private String codigo;

        public String Codigo
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
            return this.Codigo;
        }
    }
}

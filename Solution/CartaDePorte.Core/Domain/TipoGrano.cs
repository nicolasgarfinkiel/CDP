using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CartaDePorte.Core.Domain
{
    public class TipoGrano
    {
        public TipoGrano() { }

        private int idTipoGrano;

        public int IdTipoGrano
        {
            get { return idTipoGrano; }
            set { idTipoGrano = value; }
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

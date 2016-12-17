using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CartaDePorte.Core.Domain
{
    public class TipoDeCarta
    {
        public TipoDeCarta() { }

        private int idTipoDeCarta;

        public int IdTipoDeCarta
        {
            get { return idTipoDeCarta; }
            set { idTipoDeCarta = value; }
        }
        private String descripcion;

        public String Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        private Boolean activo;

        public Boolean Activo
        {
            get { return activo; }
            set { activo = value; }
        }
        public override string ToString()
        {
            return this.Descripcion;
        }
    }
}

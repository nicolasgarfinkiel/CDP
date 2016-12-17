using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CartaDePorte.Core.Domain
{
    public class CartasDePorte
    {
        public CartasDePorte() { }

        private int idCartaDePorte;

        public int IdCartaDePorte
        {
            get { return idCartaDePorte; }
            set { idCartaDePorte = value; }
        }
        private string numeroCartaDePorte;

        public string NumeroCartaDePorte
        {
            get { return numeroCartaDePorte; }
            set { numeroCartaDePorte = value; }
        }
        private string numeroCee;

        public string NumeroCee
        {
            get { return numeroCee; }
            set { numeroCee = value; }
        }
        private Enums.EstadoRangoCartaDePorte estado;

        public Enums.EstadoRangoCartaDePorte Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        private int idLoteLoteCartasDePorte;

        public int IdLoteLoteCartasDePorte
        {
            get { return idLoteLoteCartasDePorte; }
            set { idLoteLoteCartasDePorte = value; }
        }
        public override string ToString()
        {   
            return this.NumeroCartaDePorte.ToString();
        }
    }
}

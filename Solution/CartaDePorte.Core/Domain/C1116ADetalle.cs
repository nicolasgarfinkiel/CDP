using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CartaDePorte.Core.Exception;

namespace CartaDePorte.Core.Domain
{
    public class C1116ADetalle
    {
        public C1116ADetalle() { }

        private int idc1116aDetalle;
        public int Idc1116aDetalle
        {
            get { return idc1116aDetalle; }
            set { idc1116aDetalle = value; }
        }

        private int idc1116a;
        public int Idc1116a
        {
            get { return idc1116a; }
            set { idc1116a = value; }
        }


        private Int64 numeroCartaDePorte;
        public Int64 NumeroCartaDePorte
        {
            get { return numeroCartaDePorte; }
            set { numeroCartaDePorte = value; }
        }



        private Int64 numeroCertificadoAsociado;
        public Int64 NumeroCertificadoAsociado
        {
            get { return numeroCertificadoAsociado; }
            set { numeroCertificadoAsociado = value; }
        }




        private Decimal kgBrutos;
        public Decimal KgBrutos
        {
            get { return kgBrutos; }
            set { kgBrutos = value; }
        }



        private DateTime fechaRemesa;
        public DateTime FechaRemesa
        {
            get { return fechaRemesa; }
            set { fechaRemesa = value; }
        }

        public bool Validar() 
        {

            return true;
        }

        public override string ToString()
        {
            return this.numeroCartaDePorte.ToString() + " (" + this.numeroCertificadoAsociado.ToString() + ")";
        
        }

    }
}

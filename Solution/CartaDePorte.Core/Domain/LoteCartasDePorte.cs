using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CartaDePorte.Core.DAO;

namespace CartaDePorte.Core.Domain
{
    public class LoteCartasDePorte
    {
        public LoteCartasDePorte() { }

        private int idLoteCartasDePorte;
        public int IdLoteCartasDePorte
        {
            get { return idLoteCartasDePorte; }
            set { idLoteCartasDePorte = value; }
        }

        private int desde;
        public int Desde
        {
            get { return desde; }
            set { desde = value; }
        }

        private int hasta;
        public int Hasta
        {
            get { return hasta; }
            set { hasta = value; }
        }

        private string cee;
        public string Cee
        {
            get { return cee; }
            set { cee = value; }
        }

        private DateTime fechaCreacion;
        public DateTime FechaCreacion
        {
            get { return fechaCreacion; }
            set { fechaCreacion = value; }
        }

        private DateTime fechaVencimiento;
        public DateTime FechaVencimiento
        {
            get { return fechaVencimiento; }
            set { fechaVencimiento = value; }
        }

        private Establecimiento establecimientoOrigen;
        public Establecimiento EstablecimientoOrigen
        {
            get { return establecimientoOrigen; }
            set { establecimientoOrigen = value; }
        }

        private String usuarioCreacion;

        public String UsuarioCreacion
        {
            get { return usuarioCreacion; }
            set { usuarioCreacion = value; }
        }

        private int numeroSucursal;
        public int NumeroSucursal
        {
            get { return numeroSucursal; }
            set { numeroSucursal = value; }
        }

        private int ptoEmision;
        public int PtoEmision
        {
            get { return ptoEmision; }
            set { ptoEmision = value; }
        }

        private DateTime fechaDesde;
        public DateTime FechaDesde
        {
            get { return fechaDesde; }
            set { fechaDesde = value; }
        }

        public int CartasDisponibles
        {
            get { return LoteCartasDePorteDAO.Instance.GetDisponiblePorLote(IdLoteCartasDePorte); }
        }
    }
}

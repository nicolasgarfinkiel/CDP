using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CartaDePorte.Core.Exception;

namespace CartaDePorte.Core.Domain
{
    public class Chofer
    {
        public Chofer() { }

        private int idChofer;

        public int IdChofer
        {
            get { return idChofer; }
            set { idChofer = value; }
        }
        private String nombre;

        public String Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        private String apellido;

        public String Apellido
        {
            get { return apellido; }
            set { apellido = value; }
        }
        private String cuit;

        public String Cuit
        {
            get { return cuit; }
            set { cuit = value; }
        }
        private DateTime fechaCreacion;

        private String camion;
        public String Camion
        {
            get { return camion; }
            set { camion = value; }
        }

        private String acoplado;
        public String Acoplado
        {
            get { return acoplado; }
            set { acoplado = value; }
        }

        public DateTime FechaCreacion
        {
            get { return fechaCreacion; }
            set { fechaCreacion = value; }
        }
        private String usuarioCreacion;

        public String UsuarioCreacion
        {
            get { return usuarioCreacion; }
            set { usuarioCreacion = value; }
        }
        private DateTime fechaModificacion;

        public DateTime FechaModificacion
        {
            get { return fechaModificacion; }
            set { fechaModificacion = value; }
        }
        private String usuarioModificacion;

        public String UsuarioModificacion
        {
            get { return usuarioModificacion; }
            set { usuarioModificacion = value; }
        }

        private string domicilio;
        public string Domicilio
        {
            get { return domicilio; }
            set { domicilio = value; }
        }

        private string marca;
        public string Marca
        {
            get { return marca; }
            set { marca = value; }
        }

        private Boolean activo;

        public Boolean Activo
        {
            get { return activo; }
            set { activo = value; }
        }

        private CartaDePorte.Core.Domain.Enums.EsChoferTransportista esChoferTransportista;

        public CartaDePorte.Core.Domain.Enums.EsChoferTransportista EsChoferTransportista
        {
            get { return esChoferTransportista; }
            set { esChoferTransportista = value; }
        }

        public bool Validar() 
        {

            if (string.IsNullOrEmpty(this.Nombre.Trim()))
            {
                throw ExceptionFactory.CreateBusiness(new System.Exception("Debe completar un Nombre."));
            }

            if (string.IsNullOrEmpty(this.Apellido.Trim()))
            {
                throw ExceptionFactory.CreateBusiness(new System.Exception("Debe completar un Apellido."));
            }

            if (string.IsNullOrEmpty(this.Cuit.Trim()))
            {
                throw ExceptionFactory.CreateBusiness(new System.Exception("Debe completar un Cuit."));
            }

            return true;
        }

        public override string ToString()
        {
            if (this.EsChoferTransportista == Enums.EsChoferTransportista.Si)
                return this.nombre;
            else
                return this.Apellido + ", " + this.Nombre;
        }

    }
}

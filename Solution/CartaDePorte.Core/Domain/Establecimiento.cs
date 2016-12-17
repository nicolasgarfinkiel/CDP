using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CartaDePorte.Core.Domain
{
    public class Establecimiento
    {
        public Establecimiento() { }

        private int idEstablecimiento;
        public int IdEstablecimiento
        {
            get { return idEstablecimiento; }
            set { idEstablecimiento = value; }
        }

        private String descripcion;
        public String Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        private String direccion;
        public String Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }

        private Localidad localidad;
        public Localidad Localidad
        {
            get { return localidad; }
            set { localidad = value; }
        }

        private Provincia provincia;
        public Provincia Provincia
        {
            get { return provincia; }
            set { provincia = value; }
        }

        private int iDAlmacenSAP;
        public int IDAlmacenSAP
        {
            get { return iDAlmacenSAP; }
            set { iDAlmacenSAP = value; }
        }

        private int iDCentroSAP;
        public int IDCentroSAP
        {
            get { return iDCentroSAP; }
            set { iDCentroSAP = value; }
        }

        private String iDCEBE;
        public String IDCEBE
        {
            get { return iDCEBE; }
            set { iDCEBE = value; }
        }

        private String iDExpedicion;
        public String IDExpedicion
        {
            get { return iDExpedicion; }
            set { iDExpedicion = value; }
        }

        private Cliente idInterlocutorDestinatario;
        public Cliente IdInterlocutorDestinatario
        {
            get { return idInterlocutorDestinatario; }
            set { idInterlocutorDestinatario = value; }
        }

        private Enums.RecorridoEstablecimiento recorridoEstablecimiento;
        public Enums.RecorridoEstablecimiento RecorridoEstablecimiento
        {
            get { return recorridoEstablecimiento; }
            set { recorridoEstablecimiento = value; }
        }

        private String establecimientoAfip;
        public String EstablecimientoAfip
        {
            get { return establecimientoAfip; }
            set { establecimientoAfip = value; }
        }

        private Boolean asociaCartaDePorte;
        public Boolean AsociaCartaDePorte
        {
            get { return asociaCartaDePorte; }
            set { asociaCartaDePorte = value; }
        }

        private DateTime fechaCreacion;
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

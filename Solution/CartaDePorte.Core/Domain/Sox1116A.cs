using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CartaDePorte.Core.Domain
{
    /// <summary>
    /// Carga de Fecha y numero de 11116A. Tabla CartaDePorte1116A
    /// </summary>
    public class Sox1116A
    {
        public Sox1116A() { }

        private Solicitud solicitud;

        public Solicitud Solicitud
        {
            get { return solicitud; }
            set { solicitud = value; }
        }
        private Int32 idCartaDePorte1116A;

        public Int32 IdCartaDePorte1116A
        {
            get { return idCartaDePorte1116A; }
            set { idCartaDePorte1116A = value; }
        }
        private String numero1116A;

        public String Numero1116A
        {
            get { return numero1116A; }
            set { numero1116A = value; }
        }
        private DateTime fecha1116A;

        public DateTime Fecha1116A
        {
            get { return fecha1116A; }
            set { fecha1116A = value; }
        }
        private String usuarioCreacion;

        public String UsuarioCreacion
        {
            get { return usuarioCreacion; }
            set { usuarioCreacion = value; }
        }
        private DateTime fechaCreacion;

        public DateTime FechaCreacion
        {
            get { return fechaCreacion; }
            set { fechaCreacion = value; }
        }
        private String usuarioModificacion;

        public String UsuarioModificacion
        {
            get { return usuarioModificacion; }
            set { usuarioModificacion = value; }
        }
        private DateTime fechaModificacion;

        public DateTime FechaModificacion
        {
            get { return fechaModificacion; }
            set { fechaModificacion = value; }
        }


    }
}

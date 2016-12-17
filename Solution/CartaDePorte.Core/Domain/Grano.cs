using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CartaDePorte.Core.Exception;

namespace CartaDePorte.Core.Domain
{
    public class Grano
    {
        public Grano() { }

        private int idGrano;
        public int IdGrano
        {
            get { return idGrano; }
            set { idGrano = value; }
        }
        
        private String descripcion;
        public String Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        
        private String idMaterialSap;
        public String IdMaterialSap
        {
            get { return idMaterialSap; }
            set { idMaterialSap = value; }
        }
        
        private Especie idEspecieAfip;
        public Especie EspecieAfip
        {
            get { return idEspecieAfip; }
            set { idEspecieAfip = value; }
        }
        
        private Cosecha idCosechaAfip;
        public Cosecha CosechaAfip
        {
            get { return idCosechaAfip; }
            set { idCosechaAfip = value; }
        }

        private TipoGrano idTipoGrano;
        public TipoGrano TipoGrano
        {
            get { return idTipoGrano; }
            set { idTipoGrano = value; }
        }

        private String sujetoALote;
        public String SujetoALote
        {
            get { return sujetoALote; }
            set { sujetoALote = value; }
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

        public bool Validar() 
        {
            if (string.IsNullOrEmpty(this.Descripcion.Trim()))
            {
                throw ExceptionFactory.CreateBusiness(new System.Exception("Debe completar una descripcion para el Grano"));
            }

            if (string.IsNullOrEmpty(this.idMaterialSap.Trim()))
            {
                throw ExceptionFactory.CreateBusiness(new System.Exception("Debe completar un material de SAP"));
            }

            if (EspecieAfip == null)
            {
                throw ExceptionFactory.CreateBusiness(new System.Exception("Debe seleccionar una especie."));
            }

            if (CosechaAfip == null)
            {
                throw ExceptionFactory.CreateBusiness(new System.Exception("Debe seleccionar una cosecha."));
            }

            if (TipoGrano == null)
            {
                throw ExceptionFactory.CreateBusiness(new System.Exception("Debe seleccionar un tipo de grano."));
            }


            return true;
        }


        public override string ToString()
        {
            return this.Descripcion;
        }
    }
}

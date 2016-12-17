using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Configuration;

using CartaDePorte.Core.wsFrameworkSeguridad;


namespace CartaDePorte.Core.Domain.Seguridad
{
    public class SeguridadUsuario
    {
        private User user = null;
        private SecurityProvider securityProvider = null;
        //private Dictionary<int, EmpresaAdmin> _empresas = new Dictionary<int, EmpresaAdmin>();

        /// <summary>
        /// Usuario actual del thread.
        /// </summary>
        public User User
        {
            get { return this.user; }
        }

        public SecurityProvider Provider
        {
            get { return this.securityProvider; }
        }

        public SeguridadUsuario(string usuario, int codigoApp)
        {
            this.Usuario = usuario;
            this.CodigoAplicacion = codigoApp;

            CargarPermisos();

        }

        private int codigoAplicacion;
        public int CodigoAplicacion
        {
            get { return codigoAplicacion; }
            set { codigoAplicacion = value; }
        }

        private String usuario;
        public String Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }
        private IList<Permission> permisos = new List<Permission>();
        public IList<Permission> Permisos
        {
            get { return permisos; }
            set { permisos = value; }
        }


        public bool TieneAlgunPermiso
        {
            get
            {
                return (this.Permisos != null && this.Permisos.Count > 0);
            }
        }

        private void CargarPermisos()
        {
            string usercredentials = ConfigurationManager.AppSettings["WSUserName"];
            string passcredentials = ConfigurationManager.AppSettings["WSPassword"];
            string domaincredentials = ConfigurationManager.AppSettings["WSDomain"];

            securityProvider = new SecurityProvider();
            securityProvider.Credentials = new System.Net.NetworkCredential(usercredentials, passcredentials, domaincredentials);
            securityProvider.Url = ConfigurationManager.AppSettings["URLWSSeguridad"];

            this.user = securityProvider.UserLogonByName(this.Usuario, this.CodigoAplicacion);
            IList<Group> oGrupos = securityProvider.GroupsListPerUser(this.user, CodigoAplicacion);
            foreach (Group oGrupo in oGrupos)
            {
                foreach (Permission oPermiso in securityProvider.PermissionListPerGroup(oGrupo))
                {
                    Permisos.Add(oPermiso);
                }
            }

        }


        private int GetIdPermiso(string permiso)
        {

            int AppID = this.CodigoAplicacion;
            Permission p = new Permission();
            p.IdApplication = AppID;
            Permission[] pl = securityProvider.PermissionList(p);

            foreach (Permission per in pl)
            {
                if (per.Description.Contains(permiso))
                    return per.Id;

            }

            return 0;

        }


        public bool CheckPermisoInterno(string nomPermiso)
        {

            bool r = false;

            foreach (Permission p in this.permisos)
            {
                if (p.Description.Contains(nomPermiso))
                    return true;
            }


            return r;
        }


        //public int IdPais { get; set; }
        //public int IdGrupoEmpresa { get; private set;}
        //public int IdEmpresa { get; private set; }

        //public void SetEmpresa(int idEmpresa)
        //{
        //    var empresa = this.Empresas[idEmpresa];
        //    this.IdEmpresa = empresa.IdEmpresa;
        //    this.IdGrupoEmpresa = empresa.IdGrupoEmpresa;
        //}

        //public Dictionary<int,EmpresaAdmin> Empresas { 
        //    get{
        //        return this._empresas;
        //    }
        //}

    }
}

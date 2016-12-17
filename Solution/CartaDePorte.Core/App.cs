using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Runtime.Remoting.Messaging;
using System.IO;
using System.Runtime.InteropServices;
using CartaDePorte.Core.Domain.Seguridad;
using CartaDePorte.Core.DAO;
using CartaDePorte.Common;
using CartaDePorte.Core.Domain;
namespace CartaDePorte.Core
{
    public class App
    {
        #region Impersonate
        [DllImport("advapi32.dll")]
        public static extern int LogonUserA(String lpszUserName,
            String lpszDomain,
            String lpszPassword,
            int dwLogonType,
            int dwLogonProvider,
            ref IntPtr phToken);
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int DuplicateToken(IntPtr hToken,
            int impersonationLevel,
            ref IntPtr hNewToken);
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool RevertToSelf();
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool CloseHandle(IntPtr handle);
        public const int LOGON32_LOGON_INTERACTIVE = 2;
        public const int LOGON32_PROVIDER_DEFAULT = 0;
        private static WindowsImpersonationContext _impersonationContext = null;
        #endregion
        public const int ID_PAIS_ARGENTINA = 1;
        public const int ID_GRUPO_CRESUD = 1;
        public const int ID_EMPRESA_CRESUD = 22;
        public const int ID_CLIENTE_CRESUD = 1000005;
        public const string USER_SVC_ARGENTINA = "SVC_ARG";
        private static LocalSession _localSession = null;
        private static IList<EmpresaAdmin> _empresas = null;
        /// <summary>
        /// Hay que utilizar esta Wrapper en lugar de HttpContext.Current.Session
        /// Por que el modelo (domain) tambien se ejecuta en un proceso Windows, donde la sesion WEB no exite
        /// Esto simula la sesion tanto para WIN como para WEB
        /// </summary>
        public static LocalSession LocalSession
        {
            get
            {
                //var key = "LocalSession";
                //var localSession = CallContext.GetData(key) as LocalSession;
                //if (localSession == null)
                //{
                //    localSession = new LocalSession();
                //    CallContext.FreeNamedDataSlot(key);
                //    CallContext.SetData(key, localSession);
                //}

                //return localSession;
                return new LocalSession();
            }
        }

        public static int UsuarioLastEmpresa
        {
            get
            {
                var empresaId = 0;
                if (App.LocalSession.Items.ContainsKey("UsuarioLastEmpresa"))
                {
                    empresaId = Tools.Value2<int>(App.LocalSession.Items["UsuarioLastEmpresa"], 0);
                }
                return empresaId;
            }
            set
            {
                App.LocalSession.Items.Remove("UsuarioLastEmpresa");
                App.LocalSession.Items.Add("UsuarioLastEmpresa", value);
                if (System.Web.HttpContext.Current != null)
                {
                    HttpCookie userInfoCookies = HttpContext.Current.Request.Cookies["UserInfo"];

                    if (userInfoCookies != null)
                    {
                        userInfoCookies.Values.Remove("DefaultEmpresa");
                        userInfoCookies["DefaultEmpresa"] = value.ToString();
                        userInfoCookies.Expires = DateTime.Now.AddYears(5);
                        HttpContext.Current.Response.Cookies.Add(userInfoCookies);
                    }
                }
            }
        }

        public static string UsuarioLastPage
        {
            get
            {
                var page = "index.aspx";

                if (System.Web.HttpContext.Current != null)
                {
                    page = Path.GetFileName(HttpContext.Current.Request.PhysicalPath);

                    if (App.LocalSession.Items.ContainsKey("UsuarioLastPage"))
                    {
                        page = App.LocalSession.Items["UsuarioLastPage"] as string;
                    }
                }
                return page;
            }
            set
            {
                App.LocalSession.Items.Remove("UsuarioLastPage");
                App.LocalSession.Items.Add("UsuarioLastPage", value);

                if (System.Web.HttpContext.Current != null)
                {
                    HttpCookie userInfoCookies = HttpContext.Current.Request.Cookies["UserInfo"];

                    if (userInfoCookies != null)
                    {
                        userInfoCookies.Values.Remove("DefaultPage");
                        userInfoCookies["DefaultPage"] = value;
                        userInfoCookies.Expires = DateTime.Now.AddYears(5);
                        HttpContext.Current.Response.Cookies.Add(userInfoCookies);
                    }
                }
            }
        }

        public static UsuarioFull Usuario
        {
            get
            {
                if (!App.LocalSession.Items.ContainsKey("Usuario"))
                {
                    var usuario = new UsuarioFull();

                    if (System.Web.HttpContext.Current != null)
                    {
                        //Es un etorno WEB tomo el usuario de HttpContext
                        usuario.Nombre = HttpContext.Current.User.Identity.Name.ToString().Trim();
                    }
                    else
                    {
                        //Es un entorno Windows (Servicio) tomo el usuario de Red
                        usuario.Nombre = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    }
                    App.LocalSession.Items.Remove("Usuario");
                    App.LocalSession.Items.Add("Usuario", usuario);

                    //Si el Nombre esta vacio no hago mas nada. No tiene permisos para nada
                    if (string.IsNullOrEmpty(usuario.Nombre))
                    {
                        Tools.Logger.Info("Ingreso usuario anonimo. NO SE ASIGNAN PERMISOS!!!");
                    }
                    else
                    {
                        Tools.Logger.InfoFormat("Ingreso usuario {0}", usuario.Nombre);

                        if (App._empresas == null)
                        {
                            App._empresas = EmpresaDAO.Instance.GetAllAdmin();
                        }
                        foreach (var empresa in App._empresas)
                        {
                            var appID = empresa.IdApp;

                            if (!usuario.UsuariosSeguridad.Keys.Contains(appID))
                            {
                                var seguridad = new SeguridadUsuario(usuario.Nombre, appID);
                                if (seguridad.TieneAlgunPermiso)
                                {
                                    Tools.Logger.InfoFormat("Usuario {0} IDAPP: {1}", usuario.Nombre, appID);
                                    usuario.UsuariosSeguridad.Add(appID, seguridad);
                                }
                            }
                            if (usuario.UsuariosSeguridad.Keys.Contains(appID))
                            {
                                Tools.Logger.InfoFormat("Usuario {0} IDEmpresa: {1}", usuario.Nombre, empresa.IdEmpresa);
                                usuario.Empresas.Add(empresa.IdEmpresa, empresa);
                            }
                        }
                        if (usuario.UsuariosSeguridad != null && usuario.UsuariosSeguridad.Count == 0)
                        {
                            Tools.Logger.InfoFormat("Usuario {0} SIN PERMISOS ASIGANDOS", usuario.Nombre);
                        }
                        //Uso Cookies para guardar ultima empresa  seleccionada y ultima pagina visitada
                        //para la proxima session, solo si es entorno WEB
                        if (System.Web.HttpContext.Current != null && usuario.UsuariosSeguridad != null && usuario.UsuariosSeguridad.Count > 0)
                        {
                            var cookieName = string.Format("UserInfo_{0}", App.Usuario.Nombre);
                            var currentPage = Path.GetFileName(HttpContext.Current.Request.PhysicalPath);
                            var userInfoCookies = HttpContext.Current.Request.Cookies[cookieName];

                            if (userInfoCookies == null)
                            {
                                var empresaDefault = App.Usuario.IdEmpresa;
                                if (empresaDefault == 0)
                                {
                                    empresaDefault = App.Usuario.Empresas.First().Value.IdEmpresa;
                                }
                                userInfoCookies = new HttpCookie(cookieName);
                                userInfoCookies["UserName"] = App.Usuario.Nombre;
                                userInfoCookies["DefaultPage"] = currentPage;
                                userInfoCookies["DefaultEmpresa"] = empresaDefault.ToString();
                                userInfoCookies.Expires = DateTime.Now.AddYears(5);
                                HttpContext.Current.Response.Cookies.Add(userInfoCookies);
                            }

                            var idEmpresa = Tools.Value2<int>(userInfoCookies["DefaultEmpresa"], 0);
                            App.Usuario.SetEmpresa(idEmpresa);
                            App.UsuarioLastEmpresa = idEmpresa;
                            App.UsuarioLastPage = currentPage;
                        }
                    }
                }
                return App.LocalSession.Items["Usuario"] as UsuarioFull;
            }
            set
            {
                App.LocalSession.Items.Remove("Usuario");
                App.LocalSession.Items.Add("Usuario", value);
            }
        }

        public static bool UsuarioTienePermisos(string id)
        {
            var tienePermiso = false;
            var usuario = App.Usuario;
            var nombre = "";

            if (usuario != null && usuario.UsuariosSeguridad != null)
            {
                nombre = usuario.Nombre;
                if (usuario.UsuariosSeguridad.Keys.Contains(usuario.IdApp))
                {
                    tienePermiso = usuario.UsuariosSeguridad[usuario.IdApp].CheckPermisoInterno(id);
                }
            }
            if (!tienePermiso)
            {
                Tools.Logger.InfoFormat("Usuario {2} ID: {0} = {1}", id, tienePermiso, nombre);
            }
            return tienePermiso;
        }
        #region Impersonate Metodos
        public static bool ImpersonationValidateUser()
        {
            var domain = ConfigurationManager.AppSettings["Domain"];
            var userDomain = ConfigurationManager.AppSettings["UserDomain"];
            var passDomain = ConfigurationManager.AppSettings["PassDomain"];

            WindowsIdentity tempWindowsIdentity;
            IntPtr token = IntPtr.Zero;
            IntPtr tokenDuplicate = IntPtr.Zero;

            if (RevertToSelf())
            {
                if (LogonUserA(userDomain, domain, passDomain, LOGON32_LOGON_INTERACTIVE,
                    LOGON32_PROVIDER_DEFAULT, ref token) != 0)
                {
                    if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                    {
                        tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                        App._impersonationContext = tempWindowsIdentity.Impersonate();
                        if (App._impersonationContext != null)
                        {
                            CloseHandle(token);
                            CloseHandle(tokenDuplicate);
                            return true;
                        }
                    }
                }
            }
            if (token != IntPtr.Zero)
                CloseHandle(token);
            if (tokenDuplicate != IntPtr.Zero)
                CloseHandle(tokenDuplicate);
            return false;
        }

        public static void ImpersonationUndo()
        {
            if (App._impersonationContext != null)
            {
                App._impersonationContext.Undo();
            }
        }
        #endregion
    }

    public class UsuarioFull
    {
        private Dictionary<int, SeguridadUsuario> _usuariosSeguridad = new Dictionary<int, SeguridadUsuario>();
        private Dictionary<int, EmpresaAdmin> _empresas = new Dictionary<int, EmpresaAdmin>();
        public string Nombre { get; set; }
        public int IdPais { get; set; }
        public int IdGrupoEmpresa { get; private set; }
        public int IdEmpresa { get; private set; }
        public int IdApp { get; private set; }
        public EmpresaAdmin Empresa { get; private set; }

        public void SetEmpresa(int idEmpresa)
        {
            if (idEmpresa == 0)
            {
            }
            this.Empresa = this.Empresas[idEmpresa];
            this.IdEmpresa = this.Empresa.IdEmpresa;
            this.IdGrupoEmpresa = this.Empresa.IdGrupoEmpresa;
            this.IdPais = this.Empresa.IdPais;
            this.IdApp = this.Empresa.IdApp;
        }
        public Dictionary<int, SeguridadUsuario> UsuariosSeguridad
        {
            get
            {
                return this._usuariosSeguridad;
            }
        }
        public Dictionary<int, EmpresaAdmin> Empresas
        {
            get
            {
                return this._empresas;
            }
        }
    }

    public class LocalSession
    {
        private const string _key = "__SESSION";
        private Dictionary<string, object> _items;
        /// <summary>
        /// Acceso a la sesion local.
        /// </summary>
        public LocalSession()
        {
            if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Session != null)
                this._items = System.Web.HttpContext.Current.Session[_key] as Dictionary<string, object>;
            else
                this._items = CallContext.GetData(_key) as Dictionary<string, object>;


            if (this._items == null)
                this._items = this.ItemsCreate();
        }
        /// <summary>
        /// Items de la sesion
        /// </summary>
        public Dictionary<string, object> Items
        {
            get { return this._items; }
        }
        /// <summary>
        /// Crea un item en la sesion.
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, object> ItemsCreate()
        {
            Dictionary<string, object> items;
            items = new Dictionary<string, object>();

            if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Session != null)
            {
                System.Web.HttpContext.Current.Session.Add(_key, items);
            }
            else
            {
                CallContext.FreeNamedDataSlot(_key);
                CallContext.SetData(_key, items);
            }
            return items;
        }
    }
}

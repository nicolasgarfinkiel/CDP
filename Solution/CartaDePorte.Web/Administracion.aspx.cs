using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CartaDePorte.Core.DAO;
using CartaDePorte.Core.Domain.Seguridad;
using CartaDePorte.Core;
using CartaDePorte.Common;

namespace CartaDePorte.Web
{
    public partial class Administracion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            var master = (Main)Page.Master;
            master.ValidarMantenimiento();
            master.HiddenValue = "Administracion";

            if (App.Usuario == null || App.Usuario.UsuariosSeguridad.Count == 0)
            {
                Response.Redirect("~/SinAutorizacion.aspx");
                return;
            }
            if (App.Usuario.IdGrupoEmpresa != App.ID_GRUPO_CRESUD)
            {
                this.liAsociar1116A.Style.Add("display", "none");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            AfipAuthDAO.Instance.RenovarTokenAfip();
        }


        [System.Web.Services.WebMethod]
        public static string GetCurrentTime(string name)
        {
            return "Hello " + name + Environment.NewLine + "The Current Time is: " + DateTime.Now.ToString();
        }


        [System.Web.Services.WebMethod]
        public static string SetEmpresa(string empresaId)
        {
            var idEmpresa = Tools.Value2<int>(empresaId, 0);

            App.Usuario.SetEmpresa(idEmpresa);
            App.UsuarioLastEmpresa = idEmpresa;

            return App.UsuarioLastPage;
        }


        [System.Web.Services.WebMethod]
        public static string SetLastPage(string page)
        {
            App.UsuarioLastPage = page;

            return string.Empty;
        }

    }
}

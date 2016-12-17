using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CartaDePorte.Core.DAO;
using CartaDePorte.Core.Domain.Seguridad;
using CartaDePorte.Core;

namespace CartaDePorte.Web
{
    public partial class Reportes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            Main master = (Main)Page.Master;
            master.HiddenValue = "Reportes";

            if (!App.UsuarioTienePermisos("Reportes"))
            {
                Response.Redirect("~/SinAutorizacion.aspx");
                return;
            }
            if (App.Usuario.IdGrupoEmpresa != App.ID_GRUPO_CRESUD)
            {
                this.liConsulta1116A.Style.Add("display", "none");
            }
        }




    }
}

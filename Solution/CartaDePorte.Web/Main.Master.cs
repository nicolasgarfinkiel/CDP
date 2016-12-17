using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Principal;
using System.Web.Security;
using System.Configuration;
using CartaDePorte.Core.Domain.Seguridad;
using CartaDePorte.Core;
using CartaDePorte.Core.DAO;
using CartaDePorte.Common;

namespace CartaDePorte.Web
{
    public partial class Main : System.Web.UI.MasterPage
    {
        public Main()
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.DataBind();
            if (App.Usuario != null)
            {
                //this.lblInfoAdmin.Text = string.Format("Usuario: {0}, Pais:{1}, AdminEmpresa: {2}, GrupoEmpresa: {3}", App.Usuario.Usuario, App.Usuario.IdPais, App.Usuario.IdEmpresa, App.Usuario.IdGrupoEmpresa);
                this.lblInfoUsuario.Text = string.Format("Usuario: {0}", App.Usuario.Nombre);

                this.lblInfoEmpresa.Text = string.Format("Empresa: ");
                if (App.Usuario.IdGrupoEmpresa != App.ID_GRUPO_CRESUD)
                {
                    this.menuC116A.Style.Add("display", "none");
                }
            }

            if (!IsPostBack)
            {
                ListItem li;
                foreach (var empresa in App.Usuario.Empresas)
                {
                    li = new ListItem();
                    li.Value = empresa.Value.IdEmpresa.ToString();
                    li.Text = empresa.Value.Descripcion;
                    this.cboEmpresa.Items.Add(li);
                }

                this.cboEmpresa.SelectedValue = App.Usuario.IdEmpresa.ToString();
            }
        }

        public void ValidarMantenimiento()
        {
            string mantenimiento = ConfigurationManager.AppSettings["Mantenimiento"];
            if (mantenimiento.ToLower().Equals("true"))
            {
                Response.Redirect("~/Mantenimiento.aspx");
            }
        }

        public String HiddenValue
        {
            get { return hiddenTab.Value; }
            set { hiddenTab.Value = value; }
        }

        public string GetFullRootUrl()
        {
            var appPath = Request.ApplicationPath;
            if (appPath.Equals(@"/"))
                appPath = string.Empty;
            return Request.Url.GetLeftPart(UriPartial.Authority) + appPath;
        }
    }
}

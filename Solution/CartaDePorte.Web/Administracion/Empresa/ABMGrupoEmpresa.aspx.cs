using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using CartaDePorte.Core.Domain;
using CartaDePorte.Core.DAO;
using CartaDePorte.Core.Domain.Seguridad;
using CartaDePorte.Core;
using CartaDePorte.Common;

namespace CartaDePorte.Web
{
    public partial class ABMGrupoEmpresa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Main master = (Main)Page.Master;
            master.HiddenValue = "Administracion";

            if (!App.UsuarioTienePermisos("Administracion"))
                Response.Redirect("~/SinAutorizacion.aspx");

            if (!IsPostBack)
            {
                lblTitulo.Text = "Alta de Grupo Empresa";
                CargarComboInicial();
            }
        }

        protected void CargarComboInicial()
        {
            txtGrupoEmpresa.Text = string.Empty;
            btnAceptar.Enabled = false;
            cboPais.Items.Clear();
            ListItem li;
            li = new ListItem();
            li.Value = "-1";
            li.Text = "[seleccione...]";
            cboPais.Items.Add(li);

            foreach (Pais pais in PaisDAO.Instance.GetAll())
            {
                li = new ListItem();
                li.Value =  pais.IdPais.ToString();
                li.Text = pais.Descripcion;

                cboPais.Items.Add(li);
            }
            UPForm.Update();
        }

        protected void Limpiar()
        {
            CargarComboInicial();
        }

        protected void cboPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnAceptar.Enabled = true;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmpresaSearch.aspx");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected bool Validate()
        {
            if (GrupoEmpresaDAO.Instance.GetOneByDescripcion(txtGrupoEmpresa.Text).Count > 0)
            {
                lblMessage.Text = "Ya existe un Grupo Empresa creado con esa Descripción";
                lblMessage.Visible = true;
                return true;
            }

            if (string.IsNullOrEmpty(txtGrupoEmpresa.Text))
            {
                lblMessage.Text = "Debe ingresar una descripción";
                lblMessage.Visible = true;
                return true;
            }

            if (string.IsNullOrEmpty(cboPais.SelectedValue))
            {
                lblMessage.Text = "Debe seleccionar un País";
                lblMessage.Visible = true;
                return true;
            }
            return false;
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if(Validate())
                    return;

                GrupoEmpresa GE = new GrupoEmpresa();
                GE.IdGrupoEmpresa = 0;
                GE.Descripcion = txtGrupoEmpresa.Text;
                GE.Activo = true;
                GE.IdPais = Convert.ToInt32(cboPais.SelectedValue);
                GE.IdApp = 0;

                int GrupoEmpresa = GrupoEmpresaDAO.Instance.SaveOrUpdate(GE);

                if (GrupoEmpresa != 0)
                    Response.Redirect("EmpresaSearch.aspx?IdGrupoEmpresa=" + GrupoEmpresa.ToString());
                else
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Error al guardar el Grupo";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Error al guardar el Grupo";
            }
        }
    }
}

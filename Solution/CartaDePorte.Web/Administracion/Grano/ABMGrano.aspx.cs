using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CartaDePorte.Core.DAO;
using CartaDePorte.Core.Domain;
using System.Drawing;
using CartaDePorte.Core.Domain.Seguridad;
using CartaDePorte.Core;

namespace CartaDePorte.Web
{
    public partial class ABMGrano : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!App.UsuarioTienePermisos("Reportes"))
            {
                Response.Redirect("~/SinAutorizacion.aspx");
                return;
            }

            if (App.Usuario.IdGrupoEmpresa != App.ID_GRUPO_CRESUD)
            {
                this.trEspecie.Style.Add("display", "none");
                //this.trCosecha.Style.Add("display", "none");
                this.trTipoGrano.Style.Add("display", "none");
            }

            if (!IsPostBack)
            {
                populateCombos();

                string id = Request["Id"];
                if (id != "0")
                {
                    Grano grano = new Grano();
                    grano = GranoDAO.Instance.GetOne(Convert.ToInt32(id));
                    txtDescripcion.Text = grano.Descripcion;
                    txtMaterialSAP.Text = grano.IdMaterialSap;

                    if (grano.CosechaAfip != null)
                        cboCosecha.SelectedValue = grano.CosechaAfip.IdCosecha.ToString();

                    if (App.Usuario.IdGrupoEmpresa == App.ID_GRUPO_CRESUD)
                    {
                        if (grano.EspecieAfip != null)
                            cboEspecie.SelectedValue = grano.EspecieAfip.IdEspecie.ToString();

                        if (grano.TipoGrano != null)
                            cboTipoGrano.SelectedValue = grano.TipoGrano.IdTipoGrano.ToString();
                    }
                    txtSujetoALote.Text = grano.SujetoALote;
                }
                else
                    btnEliminar.Visible = false;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Grano grano = new Grano();
            if (Convert.ToInt32(Request["Id"]) > 0)
                grano = GranoDAO.Instance.GetOne(Convert.ToInt32(Request["Id"]));

            if (this.validar())
            {
                grano.Descripcion = txtDescripcion.Text.Trim();
                grano.IdMaterialSap = txtMaterialSAP.Text.Trim();
                grano.EspecieAfip = EspecieDAO.Instance.GetOne(Convert.ToInt32(cboEspecie.SelectedValue));
                grano.CosechaAfip = CosechaDAO.Instance.GetOne(Convert.ToInt32(cboCosecha.SelectedValue));

                if (cboTipoGrano.SelectedIndex > 0)
                    grano.TipoGrano = TipoGranoDAO.Instance.GetOne(Convert.ToInt32(cboTipoGrano.SelectedValue));

                grano.SujetoALote = txtSujetoALote.Text.Trim();

                if (grano.IdGrano > 0)
                    grano.UsuarioModificacion = App.Usuario.Nombre;
                else
                    grano.UsuarioCreacion = App.Usuario.Nombre;
            }

            if (this.validar())
            {
                if (GranoDAO.Instance.SaveOrUpdate(grano) < 1)
                {
                    lblMensaje.ForeColor = Color.Green;
                    lblMensaje.Text = "Los datos fueron guardados correctamente.";
                    LimpiarForm();
                }
                else
                {
                    lblMensaje.ForeColor = Color.Red;
                    lblMensaje.Text = "No pudo completarse la operacion, por favor , intentar nuevamente mas tarde.";
                }
            }
        }

        private bool validar()
        {
            lblMensaje.ForeColor = Color.Red;
            lblMensaje.Text = string.Empty;
            string errores = string.Empty;

            if (txtDescripcion.Text.Trim().Length < 1)
            {
                errores = "Debe completar una Descripcion para el Grano<br>";
                lblMensaje.Text += errores;
            }

            if (txtMaterialSAP.Text.Trim().Length < 1)
            {
                errores = "Debe completar un ID de Material de SAP asociado al Grano<br>";
                lblMensaje.Text += errores;
            }

            if (cboCosecha.SelectedIndex < 1)
            {
                errores = "Debe seleccionar una Cosecha<br>";
                lblMensaje.Text += errores;
            }

            if (App.Usuario.IdGrupoEmpresa == App.ID_GRUPO_CRESUD)
            {
                if (cboEspecie.SelectedIndex < 1)
                {
                    errores = "Debe seleccionar una Especie<br>";
                    lblMensaje.Text += errores;
                }
            }

            if (errores.Length > 0)
            {
                return false;
            }

            lblMensaje.ForeColor = Color.Green;
            lblMensaje.Text = "OK";
            return true;
        }

        private void LimpiarForm()
        {
            txtDescripcion.Text = string.Empty;
            txtMaterialSAP.Text = string.Empty;
            txtSujetoALote.Text = string.Empty;
            cboEspecie.SelectedIndex = 0;
            cboCosecha.SelectedIndex = 0;
            cboTipoGrano.SelectedIndex = 0;
        }

        private void populateCombos()
        {
            PopEspecie();
            PopCosecha();
            PopTipo();
        }

        private void PopEspecie()
        {
            ListItem li;
            li = new ListItem();
            li.Value = "-1";
            li.Text = "[seleccione...]";
            cboEspecie.Items.Add(li);

            foreach (Especie e in EspecieDAO.Instance.GetAll())
            {
                li = new ListItem();
                li.Value = e.IdEspecie.ToString();
                li.Text = e.Descripcion;
                cboEspecie.Items.Add(li);
            }
        }

        private void PopCosecha()
        {
            ListItem li;
            li = new ListItem();
            li.Value = "-1";
            li.Text = "[seleccione...]";
            cboCosecha.Items.Add(li);

            foreach (Cosecha c in CosechaDAO.Instance.GetAll())
            {
                li = new ListItem();
                li.Value = c.IdCosecha.ToString();
                li.Text = c.Descripcion;
                cboCosecha.Items.Add(li);
            }
        }

        private void PopTipo()
        {
            ListItem li;
            li = new ListItem();
            li.Value = "-1";
            li.Text = "[seleccione...]";
            cboTipoGrano.Items.Add(li);

            foreach (TipoGrano t in TipoGranoDAO.Instance.GetAll())
            {
                li = new ListItem();
                li.Value = t.IdTipoGrano.ToString();
                li.Text = t.Descripcion;
                cboTipoGrano.Items.Add(li);
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            GranoDAO.Instance.EliminarGrano(Convert.ToInt32(Request["Id"]), App.Usuario.Nombre);
            Response.Redirect("GranosSearch.aspx");
        }
    }
}

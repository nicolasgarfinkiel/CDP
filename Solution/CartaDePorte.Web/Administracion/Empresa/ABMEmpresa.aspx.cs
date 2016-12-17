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
    public partial class ABMEmpresa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Main master = (Main)Page.Master;
            master.HiddenValue = "Administracion";

            if (!App.UsuarioTienePermisos("Administracion"))
                Response.Redirect("~/SinAutorizacion.aspx");

            if (!IsPostBack)
            {
                lblTitulo.Text = "Alta de Empresa";
                Session["IdEmpresa"] = 0;
                string IdEmpresa = Request["IdEmpresa"];
                string IdGrupoEmpresa = Request["IdGrupoEmpresa"];

                if (IdEmpresa == "0" || IdEmpresa == null)
                    CargarComboInicial();

                if (IdEmpresa != "0" & IdEmpresa != null)
                {
                    CargarFormulario(Convert.ToInt32(IdGrupoEmpresa), Convert.ToInt32(IdEmpresa));
                    lblTitulo.Text = "Editar Empresa";
                    Session["IdEmpresa"] = IdEmpresa;

                    var EmpresaSolicitud = new SolicitudDAO().GetSolicitudByEmpresaCount(Convert.ToInt32(IdEmpresa));

                    if (EmpresaSolicitud > 0)
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = "La empresa ya tiene asociada una Carta, no puede editarse";
                        cboGrupoEmpresa.Enabled = false;
                        cboOrganizacion.Enabled = false;
                        cboClienteEmpresa.Enabled = false;
                        btnAceptar.Enabled = false;
                        btnLimpiar.Enabled = false;
                    }
                }
            }
        }

        protected void CargarFormulario(int IdGrupoEmpresa, int IdEmpresa)
        {
            var GE = GrupoEmpresaDAO.Instance.GetGrupoEmpresaEmpresa(IdGrupoEmpresa, IdEmpresa).First();
            cboGrupoEmpresa.Items.Clear();
            IList<GrupoEmpresa> grupo = GrupoEmpresaDAO.Instance.GetAll();
            grupo.Add(new GrupoEmpresa { IdGrupoEmpresa = -1, Descripcion = "[seleccione...]" });

            cboGrupoEmpresa.DataSource = grupo.OrderBy(ge => ge.IdGrupoEmpresa);
            cboGrupoEmpresa.DataValueField = "IdGrupoEmpresa";
            cboGrupoEmpresa.DataTextField = "Descripcion";
            cboGrupoEmpresa.DataBind();
            cboGrupoEmpresa.Items.FindByValue(IdGrupoEmpresa.ToString()).Selected = true;
            lblSeleccionPais.Visible = true;
            lblPais.Visible = true;
            lblSeleccionPais.Text = GE.Pais.Descripcion;

            ListItem li;

            foreach (Enums.OrganizacionVenta OV in Enum.GetValues(typeof(Enums.OrganizacionVenta)))
            {
                if (Convert.ToInt32(OV) == Convert.ToInt32(GE.Empresa.IdSapOrganizacionDeVenta))
                {
                    li = new ListItem();
                    li.Value = (GE.Empresa.IdSapOrganizacionDeVenta).ToString();
                    li.Text = ((int)OV).ToString() + "." + OV.ToString().Replace("IRSA", "I.R.S.A.").Replace("_", " ").Replace("SACIFyA", "S.A.C.I.F.yA.");

                    cboOrganizacion.Items.Add(li);
                }
            }

            IList<Cliente> Cliente = new List<Cliente>();
            Cliente.Add(GE.Empresa.Cliente);
            cboClienteEmpresa.DataSource = Cliente;
            cboClienteEmpresa.DataValueField = "IdCliente";
            cboClienteEmpresa.DataTextField = "RazonSocial";
            cboClienteEmpresa.DataBind();
            cboClienteEmpresa.Items.FindByValue(GE.Empresa.Cliente.IdCliente.ToString()).Selected = true;

        }

        protected void CargarComboInicial()
        {
            lblPais.Visible = false;
            lblSeleccionPais.Visible = false;
            cboGrupoEmpresa.Items.Clear();
            cboOrganizacion.Items.Clear();
            cboClienteEmpresa.Items.Clear();
            ListItem li;
            li = new ListItem();
            li.Value = "-1";
            li.Text = "[seleccione...]";
            cboGrupoEmpresa.Items.Add(li);
            cboOrganizacion.Items.Add(li);
            cboClienteEmpresa.Items.Add(li);
            cboOrganizacion.Enabled = false;
            cboClienteEmpresa.Enabled = false;
            lblMessage.Visible = false;

            foreach (GrupoEmpresa GE in GrupoEmpresaDAO.Instance.GetAll())
            {
                li = new ListItem();
                li.Value = GE.IdGrupoEmpresa.ToString();
                li.Text = GE.Descripcion.ToString().Replace("IRSA", "I.R.S.A.").Replace("_", " ").Replace("SACIFyA", "S.A.C.I.F.yA.");

                cboGrupoEmpresa.Items.Add(li);
            }
            UPForm.Update();
        }

        protected void Limpiar()
        {
            CargarComboInicial();
        }

        protected void CargarCombo(object sender)
        {
            var combo = (DropDownList)sender;
            ListItem li;

            switch (combo.ID)
            {
                case "cboGrupoEmpresa":
                    cboOrganizacion.Items.Clear();
                    cboClienteEmpresa.Items.Clear();
                    li = new ListItem();
                    li.Value = "-1";
                    li.Text = "[seleccione...]";
                    cboOrganizacion.Items.Add(li);
                    cboClienteEmpresa.Items.Add(li);
                    cboOrganizacion.Enabled = true;

                    lblPais.Visible = true;
                    lblSeleccionPais.Visible = true;

                    lblSeleccionPais.Text = GrupoEmpresaDAO.Instance.GetAll().Where(ge => ge.IdGrupoEmpresa == Convert.ToInt32(cboGrupoEmpresa.SelectedValue))
                                                           .FirstOrDefault().Pais.Descripcion;

                    foreach (Enums.OrganizacionVenta OV in Enum.GetValues(typeof(Enums.OrganizacionVenta)))
                    {
                        li = new ListItem();
                        li.Value = ((int)OV).ToString();
                        li.Text = ((int)OV).ToString() + "." + OV.ToString().Replace("IRSA", "I.R.S.A.").Replace("_", " ").Replace("SACIFyA", "S.A.C.I.F.yA.");

                        cboOrganizacion.Items.Add(li);
                    }
                    break;
                case "cboOrganizacion":
                    cboClienteEmpresa.Items.Clear();
                    li = new ListItem();
                    li.Value = "-1";
                    li.Text = "[seleccione...]";
                    cboClienteEmpresa.Items.Add(li);
                    cboClienteEmpresa.Enabled = true;

                    foreach (Cliente cte in ClienteDAO.Instance.GetClienteByOrganizacionVenta(Convert.ToInt32(cboOrganizacion.SelectedValue)))
                    {
                        li = new ListItem();
                        li.Value = cte.IdCliente.ToString();
                        li.Text = cte.Cuit + " - " + cte.RazonSocial;

                        cboClienteEmpresa.Items.Add(li);
                    }
                    break;
                default:
                    break;
            }
            UPForm.Update();
        }

        protected void cboGrupoEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarCombo(sender);
        }

        protected void cboOrganizacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarCombo(sender);
        }

        protected void cboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarCombo(sender);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmpresaSearch.aspx");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Session["IdEmpresa"] = null;
            Limpiar();
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Empresa empresa = new Empresa();
                Proveedor proveedor = new Proveedor();
                Cliente cliente = ClienteDAO.Instance.GetClienteByOrganizacionVenta(Convert.ToInt32(cboOrganizacion.SelectedValue)).Where(c => c.IdCliente == Convert.ToInt32(cboClienteEmpresa.SelectedValue)).FirstOrDefault();

                try
                {
                    proveedor = ProveedorDAO.Instance.GetOneByCuitOrgSAPID(cliente.Cuit).Where(p => p.IdSapOrganizacionDeVenta == cliente.IdSapOrganizacionDeVenta).FirstOrDefault();

                    if (proveedor == null)
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = "La empresa no tiene un proveedor registardo en CDP";
                        return;
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "La empresa no tiene un proveedor registardo en CDP";
                    return;
                }

                if (Session["IdEmpresa"] != null)
                    empresa.IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);

                empresa.Cliente = cliente;
                empresa.Descripcion = cliente.RazonSocial;
                empresa.IdSapOrganizacionDeVenta = cboOrganizacion.SelectedValue;
                empresa.Sap_Id = proveedor.Sap_Id;
                int IdGrupoEmpresa = Convert.ToInt32(cboGrupoEmpresa.SelectedValue);
                string Moneda = "ARG";

                int Empresa = EmpresaDAO.Instance.SaveOrUpdate(empresa, IdGrupoEmpresa, Moneda);

                if (Empresa != 0)
                    Response.Redirect("EmpresaSearch.aspx?Id=" + Empresa.ToString());
                else
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Error al guardar la empresa";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Error al guardar la empresa";
            }
        }
    }
}

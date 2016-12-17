using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CartaDePorte.Core.Domain;
using CartaDePorte.Core.DAO;
using CartaDePorte.Core.Domain.Seguridad;
using CartaDePorte.Core;

namespace CartaDePorte.Web
{
    public partial class EmpresaSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Main master = (Main)Page.Master;
            master.HiddenValue = "Administracion";

            if (!App.UsuarioTienePermisos("Administracion"))
                Response.Redirect("~/SinAutorizacion.aspx");

            if (!IsPostBack)
            {
                CargarComboInicial();
                string IdGrupoEmpresa = Request["IdGrupoEmpresa"];
                if (IdGrupoEmpresa != "0" & IdGrupoEmpresa != null)
                {
                    cboGrupoEmpresa.ClearSelection();
                    cboGrupoEmpresa.Items.FindByValue(IdGrupoEmpresa).Selected = true;

                    for (int i = 0; i < cboGrupoEmpresa.Items.Count; i++)
                    {
                        if (cboGrupoEmpresa.Items[i].Value == IdGrupoEmpresa)
                            cboGrupoEmpresa.SelectedIndex = i;
                    }

                    tblData.Rows.Clear();
                    CargarGrilla();
                }
            }
        }

        protected void CargarComboInicial()
        {
            cboGrupoEmpresa.Items.Clear();

            IList<GrupoEmpresa> grupo = GrupoEmpresaDAO.Instance.GetAll();
            grupo.Add(new GrupoEmpresa { IdGrupoEmpresa = -1, Descripcion = "[seleccione...]" });

            cboGrupoEmpresa.DataSource = grupo.OrderBy(ge => ge.IdGrupoEmpresa);
            cboGrupoEmpresa.DataValueField = "IdGrupoEmpresa";
            cboGrupoEmpresa.DataTextField = "Descripcion";
            cboGrupoEmpresa.DataBind();
            cboGrupoEmpresa.Items.FindByValue("-1").Selected = true;

            cboEmpresa.Items.Clear();

            IList<Empresa> empresa = new List<Empresa>();
            empresa.Add(new Empresa { IdEmpresa = -1, Descripcion = "[seleccione...]" });

            cboEmpresa.DataSource = empresa;
            cboEmpresa.DataValueField = "IdEmpresa";
            cboEmpresa.DataTextField = "Descripcion";
            cboEmpresa.DataBind();
            cboEmpresa.Items.FindByValue("-1").Selected = true;

            UPForm.Update();
        }

        protected void CargarCombo(object sender)
        {
            var combo = (DropDownList)sender;

            switch (combo.ID)
            {
                case "cboGrupoEmpresa":
                    cboEmpresa.Items.Clear();
                    ListItem li;
                    li = new ListItem();
                    li.Value = "-1";
                    li.Text = "[seleccione...]";
                    cboEmpresa.Items.Add(li);

                    foreach (Empresa GE in EmpresaDAO.Instance.GetEmpresaByGrupoEmpresa(cboGrupoEmpresa.SelectedValue))
                    {
                        li = new ListItem();
                        li.Value = GE.IdEmpresa.ToString();
                        li.Text = GE.Descripcion;

                        cboEmpresa.Items.Add(li);
                    }
                    break;
                default:
                    break;
            }
            UPForm.Update();
        }

        protected void cboGrupoEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            tblData.Rows.Clear();
            lblCantidadResultados.Text = string.Empty;
            CargarCombo(sender);
        }

        protected void cboEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            tblData.Rows.Clear();
            lblCantidadResultados.Text = string.Empty;
            UPForm.Update();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            tblData.Rows.Clear();
            lblCantidadResultados.Text = string.Empty;
            CargarComboInicial();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            tblData.Rows.Clear();
            CargarGrilla();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("ABMEmpresa.aspx?Id=0");
        }

        protected void btnNuevoGrupo_Click(object sender, EventArgs e)
        {
            Response.Redirect("ABMGrupoEmpresa.aspx");
        }

        private void CargarTitulos()
        {
            tblData.Rows.Clear();
            var row = new TableRow();
            row.CssClass = "TableRowTitle";
            row.Cells.Add(AddTitleCell("Empresa", 150, "Descripcion Empresa"));
            //row.Cells.Add(AddTitleCell("IdCliente", 50, "IdCliente"));
            //row.Cells.Add(AddTitleCell("Organizacion Venta", 100, "IdSapOrganizacionDeVenta"));
            //row.Cells.Add(AddTitleCell("Sector", 15, "IdSapSector"));
            //row.Cells.Add(AddTitleCell("Canal Local", 100, "IdSapCanalLocal"));
            //row.Cells.Add(AddTitleCell("Canal Expor", 100, "IdSapCanalExpor"));
            //row.Cells.Add(AddTitleCell("Sap_Id", 20, "Sap_Id"));
            row.Cells.Add(AddTitleCell("Moneda", 10, "IdSapMoneda"));
            row.Cells.Add(AddTitleCell("Pais", 100, "Pais"));
            row.Cells.Add(AddTitleCell("Descripcion", 5, "Descripcion"));
            //row.Cells.Add(AddTitleCell("", 5, "Editar Empresa"));
            row.Cells.Add(AddTitleCell("Editar", 5, "Editar Empresa"));
            tblData.Rows.Add(row);
        }

        private TableCell AddTitleCell(string texto, int width, string tooltip)
        {
            var cell = new TableCell();
            cell.Text = texto;
            if (!string.IsNullOrEmpty(tooltip))
                cell.ToolTip = tooltip;
            cell.Height = Unit.Pixel(40);
            cell.Width = Unit.Pixel(width);
            return cell;
        }

        private void CargarGrilla()
        {
            var IdGrupoEmpresa = Convert.ToInt32(cboGrupoEmpresa.SelectedValue);
            var IdEmpresa = Convert.ToInt32(cboEmpresa.SelectedValue);

            Session["totalList"] = null;
            Session["tableList"] = null;
            Session["totalList"] = GrupoEmpresaDAO.Instance.GetGrupoEmpresaEmpresa(IdGrupoEmpresa, IdEmpresa); //.OrderBy(e => e.Empresa.Descripcion).ToList();
            Session["tableList"] = AddRows();

            IList<GrupoEmpresa> tableList = new List<GrupoEmpresa>();
            tableList = (IList<GrupoEmpresa>)Session["tableList"];

            if (tableList.Count != 0)
            {
                CargarTitulos();

                if (tableList.Count > 1)
                    lblCantidadResultados.Text = "Resultado de la búsqueda: " + tableList.Count.ToString() + " registros";
                else if (tableList.Count == 1)
                    lblCantidadResultados.Text = "Resultado de la búsqueda: " + tableList.Count.ToString() + " registro";
            }
            else
                lblCantidadResultados.Text = "Resultado de la búsqueda: 0 registro";

            foreach (GrupoEmpresa item in tableList)
            {
                var row = new TableRow();
                row.CssClass = "TableRow";

                row.Cells.Add(AddCell(item.Empresa.Descripcion.ToString(), string.Empty, HorizontalAlign.Center));
                //row.Cells.Add(AddCell(item.Empresa.Cliente.IdCliente.ToString(), string.Empty, HorizontalAlign.Center));
                //row.Cells.Add(AddCell(item.Empresa.IdSapOrganizacionDeVenta.ToString(), string.Empty, HorizontalAlign.Center));
                //row.Cells.Add(AddCell(item.Empresa.IdSapSector.ToString(), string.Empty, HorizontalAlign.Center));
                //row.Cells.Add(AddCell(item.Empresa.IdSapCanalLocal.ToString(), string.Empty, HorizontalAlign.Center));
                //row.Cells.Add(AddCell(item.Empresa.IdSapCanalExpor.ToString(), string.Empty, HorizontalAlign.Center));
                //row.Cells.Add(AddCell(item.Empresa.Sap_Id.ToString(), string.Empty, HorizontalAlign.Center));
                row.Cells.Add(AddCell(item.Empresa.IdSapMoneda.ToString(), string.Empty, HorizontalAlign.Center));
                row.Cells.Add(AddCell(item.Pais.Descripcion.ToString(), string.Empty, HorizontalAlign.Center));
                row.Cells.Add(AddCell(item.Descripcion.ToString(), string.Empty, HorizontalAlign.Center));

                //string linkEmpresa = "<a href='ABMEmpresa.aspx?Id=" + item.Empresa.IdEmpresa.ToString() +
                //        "'><IMG border='0' src='~/Content/Images/magnify.gif'></a>";

                string linkGrupoEmpresa = "<a href='ABMEmpresa.aspx?IdEmpresa=" + item.Empresa.IdEmpresa.ToString() +
                        "&IdGrupoEmpresa=" + item.IdGrupoEmpresa + " '><IMG border='0' src='../../Content/Images/magnify.gif'></a>";

                //row.Cells.Add(AddCell(linkEmpresa, string.Empty, HorizontalAlign.Center));
                row.Cells.Add(AddCell(linkGrupoEmpresa, string.Empty, HorizontalAlign.Center));

                tblData.Rows.Add(row);
            }
            UPForm.Update();
        }

        private IList<GrupoEmpresa> AddRows()
        {
            IList<GrupoEmpresa> tableList = new List<GrupoEmpresa>();
            if ((Session["tableList"] != null))
                tableList = (IList<GrupoEmpresa>)Session["tableList"];

            IList<GrupoEmpresa> totalList = new List<GrupoEmpresa>();
            if ((Session["totalList"] != null))
                totalList = (IList<GrupoEmpresa>)Session["totalList"];

            int rowpagina = 10;
            int cntTableList = tableList.Count;

            int cnt = 0;
            if (Session["totalList"] != null)
            {
                foreach (GrupoEmpresa GE in totalList)
                {
                    if (!tableList.Contains(GE))
                    {
                        if (cnt < rowpagina)
                        {
                            tableList.Add(GE);
                            cnt++;
                        }
                    }
                }
            }
            return tableList;
        }

        private TableCell AddCell(string texto, string tooltip, HorizontalAlign ha)
        {
            var cell = new TableCell();
            var lbl = new Label();
            lbl.Text = "&nbsp;&nbsp;" + texto;
            cell.ToolTip = tooltip;
            cell.Height = Unit.Pixel(35);
            cell.Controls.Add(lbl);
            return cell;
        }
    }
}

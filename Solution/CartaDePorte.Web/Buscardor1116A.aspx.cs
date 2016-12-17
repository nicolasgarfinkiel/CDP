using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CartaDePorte.Core.Domain;
using CartaDePorte.Core.DAO;
using CartaDePorte.Core.Servicios;
using System.Drawing;
using CartaDePorte.Core.Domain.Seguridad;
using CartaDePorte.Core;

namespace CartaDePorte.Web
{
    public partial class Buscardor1116A : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            var master = (Main)Page.Master;
            master.ValidarMantenimiento();
            master.HiddenValue = "C1116A";


            if (!App.UsuarioTienePermisos("1116A"))
            {
                Response.Redirect("~/SinAutorizacion.aspx");
                return;
            }
            if (App.Usuario.IdGrupoEmpresa != App.ID_GRUPO_CRESUD)
            {
                Response.Redirect("~/SinAutorizacion.aspx");
                return;
            }


            CargarTitulos();
            //Datos();
            if (!IsPostBack)
            {

                DatosFiltro(string.Empty);

            }
            
        }

        private void CargarTitulos()
        {
            tblData.Rows.Clear();
            var row = new TableRow();
            row.CssClass = "TableRowTitle";
            //row.Cells.Add(AddTitleCell("", 5));
            row.Cells.Add(AddTitleCell("Id", 10, "Identificador interno")); // Idc1116a
            row.Cells.Add(AddTitleCell("Nro Certificado", 170, "Nro Certificado")); // NroCertificadoc1116a
            row.Cells.Add(AddTitleCell("Razon Social Proveedor", 270, "Razon Social Proveedor")); //RazonSocialProveedor
            row.Cells.Add(AddTitleCell("Fecha Creacion", 170, "Fecha Creacion")); // FechaCreacion
            row.Cells.Add(AddTitleCell("Usuario", 170, "Usuario de creación")); // UsuarioCreacion
            row.Cells.Add(AddTitleCell("", 5,"Ver o Editar Formulario C1116A"));

            tblData.Rows.Add(row);

        }

        #region Creacion de celdas

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

        private TableCell AddTitleCell(string texto, int width)
        {
            var cell = new TableCell();
            cell.Text = texto;
            cell.Height = Unit.Pixel(40);
            cell.Width = Unit.Pixel(width);
            return cell;
        }

        private TableCell AddTitleCell(string texto, int width,string tooltip)
        {
            var cell = new TableCell();
            cell.Text = texto;
            cell.ToolTip = tooltip;
            cell.Height = Unit.Pixel(40);
            cell.Width = Unit.Pixel(width);
            return cell;
        }

        #endregion

        private void DatosFiltro(string busqueda)
        {
            Session["totalList"] = null;
            Session["tableList"] = null;
            Session["totalList"] = C1116ADAO.Instance.GetByFiltro(txtBuscar.Text.Trim());
            Session["tableList"] = addMoreRows();
            popTable();

        }

        private void popTable()
        {

            IList<C1116A> tableList = new List<C1116A>();
            if ((Session["tableList"] != null))
                tableList = (IList<C1116A>)Session["tableList"];

            IList<C1116A> totalList = new List<C1116A>();
            if ((Session["totalList"] != null))
                totalList = (IList<C1116A>)Session["totalList"];


            if (totalList.Count > 1)
                lblCantidadResultados.Text = "Resultado de la busqueda: " + totalList.Count.ToString() + " registros";
            else if (totalList.Count == 1)
                lblCantidadResultados.Text = "Resultado de la busqueda: " + totalList.Count.ToString() + " registro";
            else
                lblCantidadResultados.Text = "Resultado de la busqueda: 0 registro";

            foreach (C1116A solicitud in tableList)
            {
                var row = new TableRow();
                row.CssClass = "TableRow";

                row.Cells.Add(AddCell(solicitud.Idc1116a.ToString(), solicitud.Idc1116a.ToString(), HorizontalAlign.Justify));
                row.Cells.Add(AddCell(solicitud.NroCertificadoc1116a.ToString(), solicitud.NroCertificadoc1116a.ToString(), HorizontalAlign.Justify));
                row.Cells.Add(AddCell(solicitud.RazonSocialProveedor, solicitud.RazonSocialProveedor, HorizontalAlign.Justify));
                row.Cells.Add(AddCell(solicitud.FechaCreacion.ToString("dd/MM/yyyy"), solicitud.FechaCreacion.ToString(), HorizontalAlign.Justify));
                row.Cells.Add(AddCell(solicitud.UsuarioCreacion.Split('\\')[1].ToString(), solicitud.UsuarioCreacion, HorizontalAlign.Justify));

                // LUPA
                String link = "<a href='CargaC1116A.aspx?id=" + solicitud.Idc1116a.ToString() +
                        "'><IMG border='0' src='Content/Images/magnify.gif'></a>";

                row.Cells.Add(AddCell(link, "Abrir Formulario C1116A", HorizontalAlign.Center));

                tblData.Rows.Add(row);


            }

        
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            lblCantidadActual.Text = string.Empty;
            CargarTitulos();
            DatosFiltro(txtBuscar.Text.Trim());
        }
        private string splitCapitalizacion(string texto)
        {
            string output = "";

            foreach (char letter in texto)
            {
                if (Char.IsUpper(letter) && output.Length > 0)
                    output += " " + letter;
                else
                    output += letter;
            }

            return output;
        }
        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            btnBuscar_Click(null, null);
        }
        private IList<C1116A> addMoreRows()
        {

            IList<C1116A> tableList = new List<C1116A>();
            if ((Session["tableList"] != null))
                tableList = (IList<C1116A>)Session["tableList"];

            IList<C1116A> totalList = new List<C1116A>();
            if ((Session["totalList"] != null))
                totalList = (IList<C1116A>)Session["totalList"];

            int rowpagina = 10;
            int cntTableList = tableList.Count;

            int cnt = 0;
            if (Session["totalList"] != null)
            {
                foreach (C1116A sol in totalList)
                {
                    if (!tableList.Contains(sol))
                    {
                        if (cnt < rowpagina)
                        {
                            tableList.Add(sol);
                            cnt++;
                        }
                    }
                }

            }

            return tableList;        
        }

        public void MasDatos()
        {
            Session["tableList"] = addMoreRows();
            popTable();

        }

        protected void btnCargarMas_Click(object sender, EventArgs e)
        {
            MasDatos();

            IList<C1116A> tmpList = new List<C1116A>();
            if ((Session["tableList"] != null))
                tmpList = (IList<C1116A>)Session["tableList"];

            IList<C1116A> tmpList2 = new List<C1116A>();
            if ((Session["totalList"] != null))
                tmpList2 = (IList<C1116A>)Session["totalList"];

            lblCantidadActual.Text = "Registros cargados: " + tmpList.Count.ToString() + " de " + tmpList2.Count.ToString();
            lblCantidadActual.ForeColor = Color.DarkGreen;

        }

        protected void btnNueva_Click(object sender, EventArgs e)
        {
            Response.Redirect("CargaC1116A.aspx?id=0");
        }

 
    
    }
}

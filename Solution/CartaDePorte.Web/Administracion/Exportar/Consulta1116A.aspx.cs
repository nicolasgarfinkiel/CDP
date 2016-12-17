using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

using CartaDePorte.Core.Domain;
using CartaDePorte.Core.DAO;
using CartaDePorte.Core.Domain.Seguridad;
using CartaDePorte.Core;


namespace CartaDePorte.Web
{
    public partial class Consulta1116A : System.Web.UI.Page
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


            string fd = Request.Form[txtDateDesde.UniqueID];
            string fh = Request.Form[txtDateHasta.UniqueID];

            txtDateDesde.Text = fd;
            txtDateHasta.Text = fh;

            if (!IsPostBack)
            {
                foreach (ListItem li in rblFechas.Items)
                {
                    if (li.Text == "Por fecha de Emision")
                    {
                            li.Selected = true;
                    }
                }

                if (String.IsNullOrEmpty(txtDateDesde.Text))
                {
                    txtDateDesde.Text = DateTime.Today.ToString("dd/MM/yyyy");
                
                }
                if (String.IsNullOrEmpty(txtDateHasta.Text))
                {
                    txtDateHasta.Text = DateTime.Today.ToString("dd/MM/yyyy");
                }


            }




        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Validaciones())
            {
                HttpContext context = HttpContext.Current;
                context.Response.Clear();
                context.Response.ContentType = "text/csv";
                context.Response.AddHeader("Content-Disposition", "attachment; filename=CDP1116A.csv");

                string fd = Request.Form[txtDateDesde.UniqueID];
                string fh = Request.Form[txtDateHasta.UniqueID];

                string[] fechaDesde = fd.Trim().Substring(0, 10).Split('/');
                DateTime FD = new DateTime(Convert.ToInt32(fechaDesde[2]), Convert.ToInt32(fechaDesde[1]), Convert.ToInt32(fechaDesde[0]));

                string[] fechaHasta = fh.Trim().Substring(0, 10).Split('/');
                DateTime FH = new DateTime(Convert.ToInt32(fechaHasta[2]), Convert.ToInt32(fechaHasta[1]), Convert.ToInt32(fechaHasta[0]));

                int modo = 2;
                if (rblFechas.SelectedItem.Text == "Por fecha de Emision")
                    modo = 1;

                DataTable tempData = SolicitudDAO.Instance.GetAllReporte1116ADataTable(FD, FH.AddHours(23).AddMinutes(59), modo);

                for (int i = 0; i <= tempData.Columns.Count - 1; i++)
                {
                    context.Response.Write(splitCapitalizacion(tempData.Columns[i].ColumnName));
                    context.Response.Write(";");
                }
                context.Response.Write(Environment.NewLine);

                foreach (DataRow row in tempData.Rows)
                {
                    for (int i = 0; i <= tempData.Columns.Count - 1; i++)
                    {
                        context.Response.Write(row[i]);
                        context.Response.Write(";");
                    }
                    context.Response.Write(Environment.NewLine);
                }
                context.Response.End();

            }

        }

        private String EspaciosEnCampos(String texto, int pad)
        {
            if (texto == null)
                return string.Empty.PadLeft(pad, ' ');

            return texto.PadLeft(pad, ' ');      
        }

        private String CerosEnCampos(String texto, int pad)
        {
            if (texto == null)
                return string.Empty.PadLeft(pad, '0');

            return texto.PadLeft(pad, '0');      
        }

        private Boolean Validaciones()
        {

            Boolean result = true;
            string mensaje = string.Empty;
            lblMensaje.Text = string.Empty;

            string fd = Request.Form[txtDateDesde.UniqueID];
            string fh = Request.Form[txtDateHasta.UniqueID];

            if (String.IsNullOrEmpty(fd))
            {
                mensaje += "Debe completar FechaDesde<br>";
            }
            if (String.IsNullOrEmpty(fh))
            {
                mensaje += "Debe completar FechaHasta<br>";
            }


            if (mensaje.Length > 0)
            {
                lblMensaje.Text = mensaje;
                result = false;
            }
            else 
            {

                string[] fechaDesde = fd.Trim().Substring(0, 10).Split('/');
                DateTime FD = new DateTime(Convert.ToInt32(fechaDesde[2]), Convert.ToInt32(fechaDesde[1]), Convert.ToInt32(fechaDesde[0]));

                string[] fechaHasta = fh.Trim().Substring(0, 10).Split('/');
                DateTime FH = new DateTime(Convert.ToInt32(fechaHasta[2]), Convert.ToInt32(fechaHasta[1]), Convert.ToInt32(fechaHasta[0]));

                if (FD > FH)
                {
                    mensaje += "La Fecha Desde debe ser menor o igual a la Fecha Hasta<br>";

                    lblMensaje.Text = mensaje;
                    result = false;
                
                }
            
            }
                

            return result;
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

        


        protected void Button2_Click(object sender, EventArgs e)
        {
            if (Validaciones())
            {
                CargarTitulos();
                Datos();
            }
        }

        private void CargarTitulos()
        {
            tblData.Rows.Clear();
            var row = new TableRow();
            row.CssClass = "TableRowTitle";
            row.Cells.Add(AddTitleCell("Nro. Carta de Porte", 50));
            row.Cells.Add(AddTitleCell("Número de CEE", 50));
            row.Cells.Add(AddTitleCell("Número de CTG", 50));
            row.Cells.Add(AddTitleCell("Fecha de Emision", 100));
            row.Cells.Add(AddTitleCell("Titular Carta de Porte", 250));
            row.Cells.Add(AddTitleCell("Número 1116A", 100));
            row.Cells.Add(AddTitleCell("Fecha 1116A", 100));
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

        #endregion


        private void Datos()
        {

            if (Validaciones())
            {
                string fd = Request.Form[txtDateDesde.UniqueID];
                string fh = Request.Form[txtDateHasta.UniqueID];

                string[] fechaDesde = fd.Trim().Substring(0, 10).Split('/');
                DateTime FD = new DateTime(Convert.ToInt32(fechaDesde[2]), Convert.ToInt32(fechaDesde[1]), Convert.ToInt32(fechaDesde[0]));

                string[] fechaHasta = fh.Trim().Substring(0, 10).Split('/');
                DateTime FH = new DateTime(Convert.ToInt32(fechaHasta[2]), Convert.ToInt32(fechaHasta[1]), Convert.ToInt32(fechaHasta[0]));

                int modo = 2;
                if (rblFechas.SelectedItem.Text == "Por fecha de Emision")
                    modo = 1;

                IList<SolicitudReporte1116A> tempData = SolicitudDAO.Instance.GetAllReporte1116A(FD, FH.AddHours(23).AddMinutes(59), modo);

                foreach (SolicitudReporte1116A row in tempData)
                {
                    var rowCell = new TableRow();
                    rowCell.CssClass = "TableRow";

                    rowCell.Cells.Add(AddCell(row.NroCartaDePorte, string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(row.NroCEE, string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(row.NroCTG, string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(row.FechaDeEmision, string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(row.TitularCartaDePorte.Trim(), string.Empty, HorizontalAlign.Justify));

                    rowCell.Cells.Add(AddCell(row.Numero1116A, string.Empty, HorizontalAlign.Justify));
                    rowCell.Cells.Add(AddCell(row.Fecha1116A, string.Empty, HorizontalAlign.Justify));
                    
                    tblData.Rows.Add(rowCell);
                }

            }

        }









    }
}

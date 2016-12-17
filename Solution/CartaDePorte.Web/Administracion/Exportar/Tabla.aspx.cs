using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CartaDePorte.Core.Domain;
using CartaDePorte.Core.DAO;
using System.Data;
using CartaDePorte.Core.Domain.Seguridad;
using CartaDePorte.Core;

namespace CartaDePorte.Web
{
    public partial class Tabla : System.Web.UI.Page
    {

        public String Resultados()
        {
            DateTime FD;
            DateTime FH;

            string fd = Request.Form[txtDateDesde.UniqueID];
            string fh = Request.Form[txtDateHasta.UniqueID];
            if (fd == null){
                FD = DateTime.Today.AddDays(-1);
            }
            else {
                string[] fechaDesde = fd.Trim().Substring(0, 10).Split('/');
                FD = new DateTime(Convert.ToInt32(fechaDesde[2]), Convert.ToInt32(fechaDesde[1]), Convert.ToInt32(fechaDesde[0]));            
            }
            if (fh == null)
            {
                FH = DateTime.Today;
            }
            else
            {
                string[] fechaHasta = fh.Trim().Substring(0, 10).Split('/');
                FH = new DateTime(Convert.ToInt32(fechaHasta[2]), Convert.ToInt32(fechaHasta[1]), Convert.ToInt32(fechaHasta[0]));
            }

            String result = string.Empty;
            DataTable dt = SolicitudDAO.Instance.GetAllReporte(FD, FH.AddHours(23).AddMinutes(59).AddSeconds(59));

            result += "['Nro Carta De Porte','Cee','Ctg','Tipo De Carta','Estado En SAP','Estado En AFIP','Codigo Respuesta Envio SAP','Codigo Respuesta Anulacion SAP','Fecha De Emision','Usuario Creacion'],";

            foreach (DataRow row in dt.Rows)
            {
                 // Create and draw the visualization.

                result += "['" + row["NumeroCartaDePorte"].ToString() + "','" +
                    row["Cee"].ToString() + "','" +
                    row["Ctg"].ToString() + "','" +
                    row["TipoDeCarta"].ToString() + "','" +
                    row["EstadoEnSAP"].ToString() + "','" +
                    row["EstadoEnAFIP"].ToString() + "','" +
                    row["CodigoRespuestaEnvioSAP"].ToString() + "','" +
                    row["CodigoRespuestaAnulacionSAP"].ToString() + "','" +
                    row["FechaDeEmision"].ToString() + "','" +
                    row["UsuarioCreacion"].ToString() + "'],";

            }

            return result;
        }

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
                Resultados();
            }

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
    }
}

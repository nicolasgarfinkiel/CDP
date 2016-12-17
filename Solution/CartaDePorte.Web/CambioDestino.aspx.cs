using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using CartaDePorte.Core.Utilidades;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace CartaDePorte.Web
{

    public partial class CambioDestino : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Id"] != null)
            {
                int idSolicitud = Convert.ToInt32(Request["Id"]);

            }



        }






    }
}

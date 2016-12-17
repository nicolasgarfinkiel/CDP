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

namespace CartaDePorte.Web
{
    public partial class SoxABM1116A : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Main master = (Main)Page.Master;
            master.HiddenValue = "Administracion";


            if (!App.UsuarioTienePermisos("Administracion"))
            {
                Response.Redirect("~/SinAutorizacion.aspx");
                return;
            }


            string f = Request.Form[txtFecha.UniqueID];
            txtFecha.Text = f;
            
            string id = Request["Id"];
            string idSol = Request["IdSol"];

            if (!IsPostBack)
            {

                if (id != "0")
                {
                    Sox1116A sox = new Sox1116A();
                    sox = Sox1116ADAO.Instance.GetOne(Convert.ToInt32(id));

                    txtNumeroCartaDePorte.Text = (sox.Solicitud != null) ? sox.Solicitud.NumeroCartaDePorte : string.Empty;
                    txtCTG.Text = (sox.Solicitud != null) ? sox.Solicitud.Ctg : string.Empty;
                    txtTipoCartaDePorte.Text = (sox.Solicitud != null && sox.Solicitud.TipoDeCarta != null) ? sox.Solicitud.TipoDeCarta.Descripcion : string.Empty;
                    txtEstadoEnAFIP.Text = sox.Solicitud.EstadoEnAFIP.ToString();

                    txtNumero1116A.Text = sox.Numero1116A.ToString();
                    txtFecha.Text = sox.Fecha1116A.ToString("dd/MM/yyyy");

                }
                else
                {

                    Solicitud sol = new Solicitud();
                    sol = SolicitudDAO.Instance.GetOne(Convert.ToInt32(idSol));

                    txtNumeroCartaDePorte.Text = (sol != null) ? sol.NumeroCartaDePorte : string.Empty;
                    txtCTG.Text = (sol != null) ? sol.Ctg : string.Empty;
                    txtTipoCartaDePorte.Text = (sol.TipoDeCarta != null) ? sol.TipoDeCarta.Descripcion : string.Empty;
                    txtEstadoEnAFIP.Text = (sol != null) ? sol.EstadoEnAFIP.ToString() : string.Empty;
                    btnEliminar.Visible = false;
                }

            }
           




        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            Sox1116A sox = new Sox1116A();
            if(Convert.ToInt32(Request["Id"]) > 0)
                sox = Sox1116ADAO.Instance.GetOne(Convert.ToInt32(Request["Id"]));

            Sox1116A form = Sox1116ADAO.Instance.GetOneByIdSoclicitud(Convert.ToInt32(Request["idSol"]));
            if (form.IdCartaDePorte1116A > 0)
            {
                sox = form;
            }

           if (this.validar())
           {
                string idSol = Request["IdSol"];
                Solicitud sol = new Solicitud();
                sox.Solicitud = sol = SolicitudDAO.Instance.GetOne(Convert.ToInt32(idSol));
                sox.Numero1116A = txtNumero1116A.Text;

                string f = Request.Form[txtFecha.UniqueID];
                string[] fecha = f.Trim().Substring(0, 10).Split('/');
                DateTime F = new DateTime(Convert.ToInt32(fecha[2]), Convert.ToInt32(fecha[1]), Convert.ToInt32(fecha[0]));
                sox.Fecha1116A = F;

               
                if (sox.IdCartaDePorte1116A > 0)
                    sox.UsuarioModificacion = App.Usuario.Nombre;
                else
                    sox.UsuarioCreacion = App.Usuario.Nombre;


                if (Sox1116ADAO.Instance.SaveOrUpdate(sox) > 0)
                {
                    lblMensaje.ForeColor = Color.Red;
                    lblMensaje.Text = "No pudo completarse la operacion, por favor , intentar nuevamente mas tarde.";
                }
                else {
                    lblMensaje.ForeColor = Color.Green;
                    lblMensaje.Text = "Los datos fueron guardados correctamente.";
                    btnEliminar.Visible = false;
                    LimpiarForm();
                }
            }



        }

        private bool validar()
        {

            lblMensaje.ForeColor = Color.Red;
            lblMensaje.Text = string.Empty;
            string errores = string.Empty;

            if (txtNumero1116A.Text.Trim().Length < 1)
            {
                errores = "Debe completar el numero del formulario 1116A<br>";
                lblMensaje.Text += errores;                
            }

            if (txtFecha.Text.Trim().Length < 1)
            {
                errores = "Debe completar la fecha del documento<br>";
                lblMensaje.Text += errores;                
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
            txtNumero1116A.Text = string.Empty;
        
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Sox1116ADAO.Instance.Eliminar(Convert.ToInt32(Request["Id"]), App.Usuario.Nombre);
            Response.Redirect("Sox1116ASearch.aspx");
        }

    }
}

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
    public partial class ABMChofer : System.Web.UI.Page
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

            if (!IsPostBack)
            {
                string id = Request["Id"];
                if (id != "0")
                {
                    Chofer chofer = new Chofer();
                    chofer = ChoferDAO.Instance.GetOne(Convert.ToInt32(id));

                    foreach (ListItem li in rblTransportista.Items)
                    {
                        if (li.Text == "Si")
                        {
                            if ((chofer.EsChoferTransportista == Enums.EsChoferTransportista.Si))
                            {
                                li.Selected = true;
                            }
                        }
                        if (li.Text == "No")
                        {
                            if ((chofer.EsChoferTransportista == Enums.EsChoferTransportista.No))
                            {
                                li.Selected = true;
                            }
                        }
                    }

                    if ((chofer.EsChoferTransportista == Enums.EsChoferTransportista.No))
                    {
                        Visibilidad(true);
                        txtNombre.Text = chofer.Nombre;
                        txtApellido.Text = chofer.Apellido;
                        txtCuit.Text = chofer.Cuit;
                        txtCamion.Text = chofer.Camion;
                        txtAcoplado.Text = chofer.Acoplado;
                        if (PaisDAO.Instance.GetOne(App.Usuario.IdPais).Descripcion.ToUpper().Contains("PARAGUAY"))
                        {
                            tblDomicilio.Visible = true;
                            txtDomicilio.Text = chofer.Domicilio;
                            txtMarca.Text = chofer.Marca;
                        }
                    }
                    else
                    {
                        Visibilidad(false);
                        txtNombre.Text = chofer.Nombre;
                        txtCuit.Text = chofer.Cuit;
                    }
                }
                else
                {
                    InicioForm();
                    btnEliminar.Visible = false;
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            Chofer chofer = new Chofer();
            if (Convert.ToInt32(Request["Id"]) > 0)
                chofer = ChoferDAO.Instance.GetOne(Convert.ToInt32(Request["Id"]));

            chofer.Nombre = txtNombre.Text.Trim();
            chofer.Apellido = txtApellido.Text.Trim();
            chofer.Cuit = txtCuit.Text.Trim();
            chofer.Camion = txtCamion.Text.Trim().ToUpper();
            chofer.Acoplado = txtAcoplado.Text.Trim().ToUpper();
            chofer.EsChoferTransportista = getEnumEFValue();
            chofer.Domicilio = txtDomicilio.Text.Trim();
            chofer.Marca = txtMarca.Text.Trim();

            if (chofer.IdChofer > 0)
                chofer.UsuarioModificacion = App.Usuario.Nombre;
            else
                chofer.UsuarioCreacion = App.Usuario.Nombre;

            if (this.validar())
            {
                if (ChoferDAO.Instance.SaveOrUpdate(chofer) > 0)
                {
                    lblMensaje.ForeColor = Color.Green;
                    lblMensaje.Text = "Los datos fueron guardados correctamente.";
                    LimpiarForm();
                }
                else
                {
                    lblMensaje.ForeColor = Color.Red;
                    lblMensaje.Text = "Error al guardar";                    
                }
            }
        }

        private bool validar()
        {
            lblMensaje.ForeColor = Color.Red;
            lblMensaje.Text = string.Empty;
            string errores = string.Empty;

            bool rblConOpcion = false;
            foreach (ListItem li in rblTransportista.Items)
            {
                if (li.Selected)
                {
                    rblConOpcion = true;
                }
            }

            if (!rblConOpcion)
            {
                lblMensaje.Text += "Debe seleccionar Si es o no Transportista.<br>";
            }

            Enums.EsChoferTransportista esTransportista = getEnumEFValue();

            if (esTransportista == Enums.EsChoferTransportista.No)
            {
                //Solo se chequea para los choferes que no son transportistas
                if (txtApellido.Text.Trim().Length < 1)
                {
                    errores = "Debe completar un Apellido<br>";
                    lblMensaje.Text += errores;
                }

                if (App.Usuario.IdGrupoEmpresa == App.ID_GRUPO_CRESUD)
                {
                    if (txtCamion.Text.Trim().Length < 1)
                    {
                        errores = "Debe completar la patente del Camión<br>";
                        lblMensaje.Text += errores;
                    }
                    if (txtAcoplado.Text.Trim().Length < 1)
                    {
                        errores = "Debe completar la patente del Acoplado<br>";
                        lblMensaje.Text += errores;
                    }
                    if (txtCamion.Text.Trim().Length > 7 || txtCamion.Text.Trim().Length < 6)
                    {
                        errores = "La patente del Camión no puede superar los 6 o 7 caracteres, ej: AAA111 o AA111AA<br>";
                        lblMensaje.Text += errores;
                    }
                    if (txtAcoplado.Text.Trim().Length > 7 || txtAcoplado.Text.Trim().Length < 6)
                    {
                        errores = "La patente del Acoplado no puede superar los 6 o 7 caracteres, ej: BBB222 o BB222BB<br>";
                        lblMensaje.Text += errores;
                    }
                    if (!Tools.ValidarPatente(txtCamion.Text.Trim().ToUpper()))
                    {
                        errores = "La patente del Camión tiene formato incorrecto, ej: AAA111 o AA111AA<br>";
                        lblMensaje.Text += errores;
                    }

                    if (!Tools.ValidarPatente(txtAcoplado.Text.Trim().ToUpper()))
                    {
                        errores = "La patente del Acoplado tiene formato incorrecto, ej: AAA111 o AA111AA<br>";
                        lblMensaje.Text += errores;
                    }
                }
                else if (PaisDAO.Instance.GetOne(App.Usuario.IdPais).Descripcion.ToUpper().Contains("PARAGUAY"))
                {
                    if (string.IsNullOrEmpty(txtDomicilio.Text.Trim()))
                    {
                        errores = "El domicilio es obligatorio<br>";
                        lblMensaje.Text += errores;
                    }
                    if (string.IsNullOrEmpty(txtMarca.Text.Trim()))
                    {
                        errores = "La marca del vehiculo es obligatoria<br>";
                        lblMensaje.Text += errores;
                    }
                }
            }

            // Validacion para todos los choferes, transportistas o no.
            if (txtNombre.Text.Trim().Length < 1)
            {
                errores = "Debe completar un Nombre<br>";
                if (esTransportista == Enums.EsChoferTransportista.Si)
                    errores = "Debe completar una Descripcion para el Chofer Transportista<br>";

                lblMensaje.Text += errores;
            }

            if (App.Usuario.IdGrupoEmpresa == App.ID_GRUPO_CRESUD)
            {
                if (txtCuit.Text.Trim().Length < 1)
                {
                    errores = "Debe completar un Cuit<br>";
                    lblMensaje.Text += errores;
                }
                if (!CuitValido(txtCuit.Text.Trim()))
                {
                    errores = "Cuit no válido<br>";
                    lblMensaje.Text += errores;
                }
                if (CuitExistente(esTransportista))
                {
                    errores = "El Cuit ya existe.<br>";
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

        private bool IsNumeric(Char c)
        {
            try
            {
                Convert.ToDecimal(c.ToString());
                return true;
            }
            catch { }
            return false;
        }

        private Boolean CuitExistente(Enums.EsChoferTransportista esTransportista)
        {
            foreach (Chofer cho in ChoferDAO.Instance.GetAll())
            {
                // primero filtro si se trata de Transportista o solo chofer.
                if (cho.EsChoferTransportista.Equals(esTransportista))
                {
                    if (cho.Cuit.Equals(txtCuit.Text.Trim()) &&
                        !Convert.ToInt32(Request["Id"]).Equals(cho.IdChofer))
                        return true;
                }
            }
            return false;
        }

        public bool CuitValido(string cuit)
        {
            if (cuit.Length != 11)
            {
                return false;
            }
            int[] mult = new[] { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2, 1 };
            char[] nums = cuit.ToCharArray();
            int total = 0;
            for (int i = 0; i < mult.Length; i++)
            {
                total += int.Parse(nums[i].ToString()) * mult[i];
            }
            var resto = total % 11;
            //return resto == 0 ? 0 : resto == 1 ? 9 : 11 - resto;
            return (resto == 0);
        }

        private void LimpiarForm()
        {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtCuit.Text = string.Empty;
            txtCamion.Text = string.Empty;
            txtAcoplado.Text = string.Empty;
            txtDomicilio.Text = string.Empty;
            txtMarca.Text = string.Empty;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ChoferDAO.Instance.EliminarChofer(Convert.ToInt32(Request["Id"]), App.Usuario.Nombre);
            Response.Redirect("ChoferSearch.aspx");
        }

        private Enums.EsChoferTransportista getEnumEFValue()
        {
            Enums.EsChoferTransportista enumEF = Enums.EsChoferTransportista.No;
            foreach (ListItem li in rblTransportista.Items)
            {
                if (li.Selected)
                {
                    if (li.Text == "Si")
                    {
                        enumEF = Enums.EsChoferTransportista.Si;
                    }
                    else
                    {
                        enumEF = Enums.EsChoferTransportista.No;
                    }
                }
            }
            return enumEF;
        }

        protected void rblTransportista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblTransportista.SelectedItem.Text == "No")
                Visibilidad(true);
            else
                Visibilidad(false);
        }

        private void Visibilidad(bool visible)
        {
            if (!visible)
                lblNombreDescripcion.Text = "Descripcion";
            else
                lblNombreDescripcion.Text = "Nombre";
            //Estos dos campos siempre van a estar visibles.
            lblNombreDescripcion.Visible = true;
            txtNombre.Visible = true;
            lblCuit.Visible = true;
            txtCuit.Visible = true;

            // Estos campos dependen de si es Transportista o no.
            lblApellido.Visible = visible;
            txtApellido.Visible = visible;
            lblCamion.Visible = visible;
            txtCamion.Visible = visible;
            lblAcoplado.Visible = visible;
            txtAcoplado.Visible = visible;
            if (visible)
                tblDomicilio.Visible = PaisDAO.Instance.GetOne(App.Usuario.IdPais).Descripcion.ToUpper().Contains("PARAGUAY");
        }

        private void InicioForm()
        {
            lblNombreDescripcion.Visible = false;
            txtNombre.Visible = false;
            lblCuit.Visible = false;
            txtCuit.Visible = false;

            lblApellido.Visible = false;
            txtApellido.Visible = false;
            lblCamion.Visible = false;
            txtCamion.Visible = false;
            lblAcoplado.Visible = false;
            txtAcoplado.Visible = false;
        }

    }
}

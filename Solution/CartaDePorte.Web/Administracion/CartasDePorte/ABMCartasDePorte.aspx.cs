using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Configuration;
using CartaDePorte.Core.Domain;
using CartaDePorte.Core.DAO;
using CartaDePorte.Core.Utilidades;
using CartaDePorte.Core.Domain.Seguridad;
using CartaDePorte.Core;
using CartaDePorte.Common;

namespace CartaDePorte.Web
{
    public partial class ABMCartasDePorte : System.Web.UI.Page
    {
        #region Impersonate
        [DllImport("advapi32.dll")]
        public static extern int LogonUserA(String lpszUserName,
            String lpszDomain,
            String lpszPassword,
            int dwLogonType,
            int dwLogonProvider,
            ref IntPtr phToken);
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int DuplicateToken(IntPtr hToken,
            int impersonationLevel,
            ref IntPtr hNewToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool RevertToSelf();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool CloseHandle(IntPtr handle);

        public const int LOGON32_LOGON_INTERACTIVE = 2;
        public const int LOGON32_PROVIDER_DEFAULT = 0;

        WindowsImpersonationContext impersonationContext;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            Main master = (Main)Page.Master;
            master.HiddenValue = "Administracion";

            //SeguridadUsuario su = (Session["Usuario"] != null) ? (SeguridadUsuario)Session["Usuario"] : null;
            //if (su == null)
            //    return;
            //if (!su.CheckPermisoInterno("Alta PDF Lotes Cartas de Porte"))
            if (!App.UsuarioTienePermisos("Alta PDF Lotes Cartas de Porte"))
            {
                Response.Redirect("../../SinAutorizacion.aspx");
                return;
            }


            if (!IsPostBack)
            {
                lblMensaje.ForeColor = Color.Black;
                lblCantidadCartasDisponibles.Text = "Cantidad de Cartas de porte Disponibles: <b>" + CartaDePorteDAO.Instance.CantidadCartasDePorteDisponibles().ToString() + "</b>";

                if (PaisDAO.Instance.GetOne(App.Usuario.IdPais).Descripcion.ToUpper().Contains("PARAGUAY"))
                {
                    Label4.Text = "Timbrado";
                    trSucPto.Visible = true;
                    trFechaDesde.Visible = true;
                    trFile.Visible = false;
                    trEstablecimientoOrigen.Visible = false;
                    Label1.Visible = false;
                }
                popEstablecimientoProcedencia();
                CargaTabla();
            }
        }

        protected void GrabarDatos(object sender, EventArgs e)
        {
            if (Validaciones())
            {
                var fecVencimiento = Tools.Value2<DateTime>(this.txtFechaVencimiento.Text, DateTime.Now);
                var fecDesde = Tools.Value2<DateTime>(this.txtFechaDesde.Text, DateTime.Now);
                string establecimientoOrigen = null;
                if (cboEstablecimientoOrigen.SelectedItem != null)
                {
                    establecimientoOrigen = cboEstablecimientoOrigen.SelectedItem.Value;
                }
                //int resul = CartaDePorteDAO.Instance.AltaRangoCartaDePorte(txtRangoDesde.Text.Trim(), txtRangoHasta.Text.Trim(), txtCee.Text.Trim(), Calendar1.SelectedDate, cboEstablecimientoOrigen.SelectedItem.Value, App.Usuario.Nombre);
                string NumeroHabilitacion = txtHabilitacion.Text.Trim();

                int resul = CartaDePorteDAO.Instance.AltaRangoCartaDePorte(txtRangoDesde.Text.Trim(), txtRangoHasta.Text.Trim(), txtCee.Text.Trim(), fecVencimiento, establecimientoOrigen, App.Usuario.Nombre, Convert.ToInt32(txtSucursal.Text.Trim() == string.Empty ? "0" : txtSucursal.Text.Trim()), Convert.ToInt32(txtPtoEmision.Text.Trim() == string.Empty ? "0" : txtPtoEmision.Text.Trim()), fecDesde, NumeroHabilitacion);
                if (resul > 0)
                {
                    lblMensaje.ForeColor = Color.Red;
                    lblMensaje.Text = "No se han guardado nuevas cartas de porte.";
                    lblMensaje.ForeColor = Color.Green;
                    lblMensaje.Text = "Rango <br />Desde: " + txtRangoDesde.Text.Trim() + "<br />Hasta: " + txtRangoHasta.Text.Trim() + "<br />Total registros dados de alta exitosamente: " + resul.ToString() + "<br />";
                    lblCantidadCartasDisponibles.Text = "Cantidad de Cartas de portes Disponibles: <b>" + CartaDePorteDAO.Instance.CantidadCartasDePorteDisponibles().ToString() + "</b>";
                }
                else
                {
                    lblMensaje.ForeColor = Color.Green;
                    lblMensaje.Text = "Rango <br />Desde: " + txtRangoDesde.Text.Trim() + "<br />Hasta: " + txtRangoHasta.Text.Trim() + "<br />Total registros dados de alta exitosamente: " + resul.ToString() + "<br />";
                    lblCantidadCartasDisponibles.Text = "Cantidad de Cartas de portes Disponibles: <b>" + CartaDePorteDAO.Instance.CantidadCartasDePorteDisponibles().ToString() + "</b>";
                }
                LimpiarForm();
            }
        }

        private bool Validaciones()
        {
            lblMensaje.Text = string.Empty;
            lblMensaje.ForeColor = Color.Black;

            if (!isNumeric(txtRangoDesde.Text.Trim()))
            {
                lblMensaje.Text += "El numero de rango DESDE debe ser numérico.<br/>";
            }
            if (!isNumeric(txtRangoHasta.Text.Trim()))
            {
                lblMensaje.Text += "El numero de rango HASTA debe ser numérico.<br/>";
            }
            if (!isNumeric(txtCee.Text.Trim()))
            {
                lblMensaje.Text += "El numero de CEE debe ser numérico.<br/>";
            }
            var fecVencimiento = Tools.Value2<DateTime>(this.txtFechaVencimiento.Text, DateTime.MinValue);
            if (fecVencimiento == DateTime.MinValue)
            {
                lblMensaje.Text += "Debe seleccionar una fecha de vencimiento.<br/>";
            }
            if (fecVencimiento <= DateTime.Now)
            {
                lblMensaje.Text += "La fecha de vencimiento debe ser mayor a la fecha actual.<br/>";
            }

            if (PaisDAO.Instance.GetOne(App.Usuario.IdPais).Descripcion.ToUpper().Contains("PARAGUAY"))
            {
                if (string.IsNullOrEmpty(txtFechaDesde.Text))
                {
                    lblMensaje.Text += "La fecha desde es obligatoria.<br/>";
                }

                if (string.IsNullOrEmpty(txtSucursal.Text))
                {
                    lblMensaje.Text += "La Sucursal es obligatoria.<br/>";
                }

                if (string.IsNullOrEmpty(txtPtoEmision.Text))
                {
                    lblMensaje.Text += "El punto de emisión es obligatorio.<br/>";
                }

                if (string.IsNullOrEmpty(txtHabilitacion.Text))
                {
                    lblMensaje.Text += "La habilitación número es obligatorio.<br/>";
                }
            }

            if (lblMensaje.Text.Length > 0)
            {
                lblMensaje.ForeColor = Color.Red;
                return false;
            }
            return true;
        }

        private void CargaTabla()
        {
            CargarTitulos();
            Datos();
        }
        private void LimpiarForm()
        {
            txtRangoDesde.Text = string.Empty;
            txtRangoHasta.Text = string.Empty;
            txtCee.Text = string.Empty;
            txtSucursal.Text = string.Empty;
            txtPtoEmision.Text = string.Empty;
            txtFechaDesde.Text = string.Empty;
            txtHabilitacion.Text = string.Empty;
            cboEstablecimientoOrigen.ClearSelection();
            txtFechaVencimiento.Text = string.Empty;
            CargaTabla();
        }

        private bool isNumeric(string valor)
        {
            try
            {
                Convert.ToDecimal(valor);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void CargarTitulos()
        {
            tblLoteCartasDePorte.Rows.Clear();
            var row = new TableRow();
            row.CssClass = "TableRowTitle";
            if (!PaisDAO.Instance.GetOne(App.Usuario.IdPais).Descripcion.ToUpper().Contains("PARAGUAY"))
            {
                row.Cells.Add(AddTitleCell("Lote", 5));
                row.Cells.Add(AddTitleCell("Desde", 100));
                row.Cells.Add(AddTitleCell("Hasta", 100));
                row.Cells.Add(AddTitleCell("Cee", 150));
                row.Cells.Add(AddTitleCell("Establecimiento Origen", 150));
                row.Cells.Add(AddTitleCell("Fecha Vencimiento", 100));
                row.Cells.Add(AddTitleCell("Cantidad Disponible", 100));
                row.Cells.Add(AddTitleCell("Usuario Creacion", 100));
            }
            else
            {
                row.Cells.Add(AddTitleCell("Sucursal", 50));
                row.Cells.Add(AddTitleCell("Punto de Venta", 50));
                row.Cells.Add(AddTitleCell("Lote", 5));
                row.Cells.Add(AddTitleCell("Desde", 100));
                row.Cells.Add(AddTitleCell("Hasta", 100));
                row.Cells.Add(AddTitleCell("Timbrado", 150));
                row.Cells.Add(AddTitleCell("Establecimiento Origen", 150));
                row.Cells.Add(AddTitleCell("Fecha Desde", 100));
                row.Cells.Add(AddTitleCell("Fecha Vencimiento", 100));
                row.Cells.Add(AddTitleCell("Cantidad Disponible", 100));
                row.Cells.Add(AddTitleCell("Usuario Creacion", 100));
            }
            tblLoteCartasDePorte.Rows.Add(row);
        }

        private void Datos()
        {
            foreach (LoteCartasDePorte lote in LoteCartasDePorteDAO.Instance.GetAll(App.Usuario.IdGrupoEmpresa))
            {
                if (lote.FechaVencimiento >= DateTime.Now)
                {
                    var row = new TableRow();
                    row.CssClass = "TableRow";
                    string cntLote = "0";
                    if (!PaisDAO.Instance.GetOne(App.Usuario.IdPais).Descripcion.ToUpper().Contains("PARAGUAY"))
                    {
                        row.Cells.Add(AddCell(lote.IdLoteCartasDePorte.ToString(), lote.IdLoteCartasDePorte.ToString(), HorizontalAlign.Justify));
                        row.Cells.Add(AddCell(lote.Desde.ToString(), lote.Desde.ToString(), HorizontalAlign.Justify));
                        row.Cells.Add(AddCell(lote.Hasta.ToString(), lote.Hasta.ToString(), HorizontalAlign.Justify));
                        row.Cells.Add(AddCell(lote.Cee, lote.Cee, HorizontalAlign.Justify));
                        row.Cells.Add(AddCell((lote.EstablecimientoOrigen != null) ? lote.EstablecimientoOrigen.Descripcion : string.Empty, (lote.EstablecimientoOrigen != null) ? lote.EstablecimientoOrigen.Descripcion : string.Empty, HorizontalAlign.Justify));
                        row.Cells.Add(AddCell(lote.FechaVencimiento.ToString("dd/MM/yyyy"), lote.FechaVencimiento.ToString("dd/MM/yyyy"), HorizontalAlign.Justify));
                        cntLote = lote.CartasDisponibles.ToString();
                        row.Cells.Add(AddCell(cntLote, cntLote, HorizontalAlign.Justify));
                        row.Cells.Add(AddCell(lote.UsuarioCreacion, lote.UsuarioCreacion, HorizontalAlign.Justify));
                    }
                    else
                    {
                        row.Cells.Add(AddCell(lote.IdLoteCartasDePorte.ToString(), string.Empty, HorizontalAlign.Justify));
                        row.Cells.Add(AddCell(lote.NumeroSucursal.ToString(), string.Empty, HorizontalAlign.Justify));
                        row.Cells.Add(AddCell(lote.PtoEmision.ToString(), string.Empty, HorizontalAlign.Justify));
                        row.Cells.Add(AddCell(lote.Desde.ToString(), string.Empty, HorizontalAlign.Justify));
                        row.Cells.Add(AddCell(lote.Hasta.ToString(), string.Empty, HorizontalAlign.Justify));
                        row.Cells.Add(AddCell(lote.Cee, string.Empty, HorizontalAlign.Justify));
                        row.Cells.Add(AddCell((lote.EstablecimientoOrigen != null) ? lote.EstablecimientoOrigen.Descripcion : string.Empty, (lote.EstablecimientoOrigen != null) ? lote.EstablecimientoOrigen.Descripcion : string.Empty, HorizontalAlign.Justify));
                        row.Cells.Add(AddCell(lote.FechaDesde.ToString("dd/MM/yyyy"), string.Empty, HorizontalAlign.Justify));
                        row.Cells.Add(AddCell(lote.FechaVencimiento.ToString("dd/MM/yyyy"), string.Empty, HorizontalAlign.Justify));
                        cntLote = lote.CartasDisponibles.ToString();
                        row.Cells.Add(AddCell(cntLote, string.Empty, HorizontalAlign.Justify));
                        row.Cells.Add(AddCell(lote.UsuarioCreacion, string.Empty, HorizontalAlign.Justify));
                    }
                    if (!cntLote.Equals("0"))
                        tblLoteCartasDePorte.Rows.Add(row);
                }
            }
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

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            var msg = string.Empty;
            if (Validaciones())
            {
                if (FileUpload1.HasFile)
                {
                    if (validarExtension())
                    {
                        var domain = ConfigurationManager.AppSettings["Domain"];
                        var userDomain = ConfigurationManager.AppSettings["UserDomain"];
                        var passDomain = ConfigurationManager.AppSettings["PassDomain"];

                        if (impersonateValidUser(userDomain, domain, passDomain))
                        {
                            msg = SaveFile(FileUpload1.PostedFile);
                        }
                    }
                }
                else
                {
                    lblMensaje.Text = "No hay archivo seleccionado.";
                }
                if (msg.Length > 0)
                    lblMensaje.Text = msg;
                else
                    GrabarDatos(null, null);
            }
        }

        private string SaveFile(HttpPostedFile file)
        {
            string msg = string.Empty;
            string savePath = ConfigurationManager.AppSettings["RutaOriginalCartaDePorte"] + "Cargas\\";
            string fileName = FileUpload1.FileName;
            string pathToCheck = savePath + fileName;
            string tempfileName = "";

            if (System.IO.File.Exists(pathToCheck))
            {
                int counter = 2;
                while (System.IO.File.Exists(pathToCheck))
                {
                    tempfileName = counter.ToString() + fileName;
                    pathToCheck = savePath + tempfileName;
                    counter++;
                }

                fileName = tempfileName;
                lblMensaje.Text = "Un Archivo con ese nombre ya existe." + "<br />Su archivo fue guardado con el nombre: " + fileName;
            }
            else
            {
                lblMensaje.Text = "Su archivo fue subido con exito.";
            }

            savePath += fileName;

            FileUpload1.SaveAs(savePath);


            var rangoDesde = Tools.Value2<int>(txtRangoDesde.Text);
            var rangoHasta = Tools.Value2<int>(txtRangoHasta.Text);
            var cantidadCDP = (rangoHasta - rangoDesde) + 1;

            PdfCdp.Instance.SplitePDF(savePath, rangoDesde, cantidadCDP, out msg);


            return msg;
        }

        private bool validarExtension()
        {

            string[] validFileTypes = { "pdf" };
            string ext = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
            bool isValidFile = false;
            for (int i = 0; i < validFileTypes.Length; i++)
            {
                if (ext == "." + validFileTypes[i])
                {
                    isValidFile = true;
                    break;
                }
            }
            if (!isValidFile)
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Archivo invalido. Por favor, seleccione archivos con formato PDF.";
            }

            return isValidFile;

        }

        private void popEstablecimientoProcedencia()
        {
            ListItem li;
            li = new ListItem();
            li.Value = "-1";
            li.Text = "[seleccione...]";
            cboEstablecimientoOrigen.Items.Add(li);

            foreach (Establecimiento e in EstablecimientoDAO.Instance.GetEstablecimientoOrigenDestino(true))
            {
                if (e.AsociaCartaDePorte)
                {
                    li = new ListItem();
                    li.Value = e.IdEstablecimiento.ToString();
                    li.Text = e.Descripcion;

                    cboEstablecimientoOrigen.Items.Add(li);

                }
            }

        }

        #region Impersonate Metodos
        private bool impersonateValidUser(String userName, String domain, String password)
        {
            WindowsIdentity tempWindowsIdentity;
            IntPtr token = IntPtr.Zero;
            IntPtr tokenDuplicate = IntPtr.Zero;

            if (RevertToSelf())
            {
                if (LogonUserA(userName, domain, password, LOGON32_LOGON_INTERACTIVE,
                    LOGON32_PROVIDER_DEFAULT, ref token) != 0)
                {
                    if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                    {
                        tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                        impersonationContext = tempWindowsIdentity.Impersonate();
                        if (impersonationContext != null)
                        {
                            CloseHandle(token);
                            CloseHandle(tokenDuplicate);
                            return true;
                        }
                    }
                }
            }
            if (token != IntPtr.Zero)
                CloseHandle(token);
            if (tokenDuplicate != IntPtr.Zero)
                CloseHandle(tokenDuplicate);
            return false;
        }

        private void undoImpersonation()
        {
            impersonationContext.Undo();
        }
        #endregion
    }
}


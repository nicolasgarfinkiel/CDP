using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Web.Services.Description;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Xml.Serialization;
using System.ServiceModel;

using CartaDePorte.Test.servicio;
using CartaDePorte.Core.wsFrameworkSeguridad;
using CartaDePorte.Core.Domain;

namespace CartaDePorte.Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<SapMensajePrefactura> datos = new List<SapMensajePrefactura>();
            SapMensajePrefactura d = new SapMensajePrefactura();
            d.NroDocumentoRE = "536044895";
            d.NroDocumentoSap = "0000000001847";
            d.Origen = "CARTADEPORTE";
            d.TextoMensaje = "bla bla bla";
            d.TipoMensaje = "S";

            datos.Add(d);

            servicio.cdpSAP ser = new CartaDePorte.Test.servicio.cdpSAP();

            ser.RecibirPrefacturacion(datos.ToArray());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int i = (int)Enums.EstadoEnAFIP.Enviado;
            i = (int)Enums.EstadoEnAFIP.Otorgado;
            i = (int)Enums.EstadoEnAFIP.SinProcesar;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var x = "123".PadLeft(5, '0');

            var x2 = (100).ToString("#0.00").PadLeft(15, '0');


        }

        private void button4_Click(object sender, EventArgs e)
        {
            var wsdlPath = @"D:\Desarrollo\IRSA\CartaDePorte\_misc\SecurityProvider.wsdl";
            var outputFilePath = @"D:\Desarrollo\IRSA\CartaDePorte\_misc\SecurityProvider.asmx";
            

            if (File.Exists(wsdlPath) == false)
            {
                return;
            }

            ServiceDescription wsdlDescription = ServiceDescription.Read(wsdlPath);
            ServiceDescriptionImporter wsdlImporter = new ServiceDescriptionImporter();

            wsdlImporter.ProtocolName = "Soap12";
            wsdlImporter.AddServiceDescription(wsdlDescription, null, null);
            wsdlImporter.Style = ServiceDescriptionImportStyle.Server;

            wsdlImporter.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties;

            CodeNamespace codeNamespace = new CodeNamespace();
            CodeCompileUnit codeUnit = new CodeCompileUnit();
            codeUnit.Namespaces.Add(codeNamespace);

            ServiceDescriptionImportWarnings importWarning = wsdlImporter.Import(codeNamespace, codeUnit);

            if (importWarning == 0)
            {
                StringBuilder stringBuilder = new StringBuilder();
                StringWriter stringWriter = new StringWriter(stringBuilder);

                CodeDomProvider codeProvider = CodeDomProvider.CreateProvider("CSharp");
                codeProvider.GenerateCodeFromCompileUnit(codeUnit, stringWriter, new CodeGeneratorOptions());

                stringWriter.Close();

                File.WriteAllText(outputFilePath, stringBuilder.ToString(), Encoding.UTF8);
            }
            else
            {
                Console.WriteLine(importWarning);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string usercredentials = System.Configuration.ConfigurationSettings.AppSettings["WSUserName"];
            string passcredentials = System.Configuration.ConfigurationSettings.AppSettings["WSPassword"];
            string domaincredentials = System.Configuration.ConfigurationSettings.AppSettings["WSDomain"];

            var securityProvider = new SecurityProvider();
            securityProvider.Credentials = new System.Net.NetworkCredential(usercredentials, passcredentials, domaincredentials);
            securityProvider.Url = System.Configuration.ConfigurationSettings.AppSettings["URLWSSeguridad"];

            var codigoAplicacion = 29;
            var userName = "pablo";
            
            var user = securityProvider.UserLogonByName( userName, codigoAplicacion);

            IList<Group> oGrupos = securityProvider.GroupsListPerUser(user, codigoAplicacion);

            var permisos = new List<Permission>();
            foreach (Group oGrupo in oGrupos)
            {
                foreach (Permission oPermiso in securityProvider.PermissionListPerGroup(oGrupo))
                {
                    permisos.Add(oPermiso);
                }
            }

            //var seguridad = new CartaDePorte.Test.Seguridad;



        }

        private void button6_Click(object sender, EventArgs e)
        {
            var granos = CartaDePorte.Core.DAO.GranoDAO.Instance.GetAll();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            new Servicio().OnStart(null);
        }

        private void button8_Click(object sender, EventArgs e)
        {


            try
            {
                //var wsclient = new wsCDPSAP2.cdpSAP2SoapClient(new BasicHttpBinding(), new EndpointAddress("http://localhost/CartaDePorte.WS/cdpSAP2.asmx"));

                var wsclient = new wsCDPSAP.cdpSAPSoapClient(new BasicHttpBinding(), new EndpointAddress("http://localhost/CartaDePorte.WS/cdpSAP.asmx"));

                ////var proveedor = new wsCDPSAP2.ProveedorXI();
                var proveedor = new wsCDPSAP.ProveedorXI();

                ////proveedor.Activo = true;
                ////proveedor.Id_Proveedor = "0000100000";
                ////proveedor.NumeroDocumento = "30563429875";
                ////proveedor.Nombre = "Pablo Slucka";
                ////proveedor.Calle = "Ambrosetti";
                ////proveedor.Piso = "1";
                ////proveedor.Pais = "AR";
                ////proveedor.Ciudad = "CABA";
                ////proveedor.CP = "1405";
                ////proveedor.Departamento = "A";
                ////proveedor.Numero = "90";
                ////proveedor.IdSapOrganizacionDeVenta = "5000";
                ////wsclient.ProcesarProveedor(proveedor);


                ////proveedor.Activo = true;
                ////proveedor.Id_Proveedor = "0000110000";
                ////proveedor.NumeroDocumento = "1234567890";
                ////proveedor.Nombre = "Pablo Perez";
                ////proveedor.Calle = "Ambrosetti";
                ////proveedor.Piso = "1";
                ////proveedor.Pais = "AR";
                ////proveedor.Ciudad = "CABA";
                ////proveedor.CP = "1405";
                ////proveedor.Departamento = "A";
                ////proveedor.Numero = "90";
                ////proveedor.IdSapOrganizacionDeVenta = "5000";
                ////wsclient.ProcesarProveedor(proveedor);


                //proveedor.Activo = true;
                //proveedor.Id_Proveedor = "0004100000";
                //proveedor.NumeroDocumento = "30563429875";
                //proveedor.Nombre = "Pablo Slucka";
                //proveedor.Calle = "Ambrosetti";
                //proveedor.Piso = "1";
                //proveedor.Pais = "AR";
                //proveedor.Ciudad = "CABA";
                //proveedor.CP = "1405";
                //proveedor.Departamento = "A";
                //proveedor.Numero = "90";
                //proveedor.IdSapOrganizacionDeVenta = "5000";
                //wsclient.ProcesarProveedor(proveedor);


                //proveedor.Activo = true;
                //proveedor.Id_Proveedor = "0004100000";
                //proveedor.NumeroDocumento = "1234567890";
                //proveedor.Nombre = "Pablo Perez";
                //proveedor.Calle = "Ambrosetti";
                //proveedor.Piso = "1";
                //proveedor.Pais = "AR";
                //proveedor.Ciudad = "CABA";
                //proveedor.CP = "1405";
                //proveedor.Departamento = "A";
                //proveedor.Numero = "90";
                //proveedor.IdSapOrganizacionDeVenta = "5001";
                //wsclient.ProcesarProveedor(proveedor);
                ////proveedor.Activo = true;
                ////proveedor.Id_Proveedor = "0002100001";
                ////proveedor.NumeroDocumento = "20204276669";
                ////proveedor.Nombre = "Pablo Slucka";
                ////proveedor.Calle = "Ambrosetti";
                ////proveedor.Piso = "1";
                ////proveedor.Pais = "AR";
                ////proveedor.Ciudad = "CABA";
                ////proveedor.CP = "1405";
                ////proveedor.Departamento = "A";
                ////proveedor.Numero = "90";
                ////proveedor.IdSapOrganizacionDeVenta = "5001";
                ////wsclient.ProcesarProveedor(proveedor);



                ////var cliente = new wsCDPSAP2.ClienteXI();
                var cliente = new wsCDPSAP.ClienteXI();

                cliente.IdCliente = "1000000";
                cliente.Activo = true;
                cliente.Calle = "Ambrosetti";
                cliente.ClaveGrupo = "";
                cliente.CP = "1405";
                cliente.Dto = "A";
                cliente.GrupoComercial = "";
                cliente.NombreFantasia = "PS";
                cliente.Numero = "90";
                cliente.Piso = "1";
                cliente.Poblacion = "";
                cliente.Tratamiento = "";
                cliente.DescripcionGE = "";
                cliente.IdSapOrganizacionDeVenta = "5000";
                wsclient.ProcesarCliente(cliente);


                cliente.IdCliente = "1000000";
                cliente.Activo = true;
                cliente.Calle = "Ambrosetti";
                cliente.ClaveGrupo = "";
                cliente.CP = "1405";
                cliente.Dto = "A";
                cliente.GrupoComercial = "";
                cliente.NombreFantasia = "PS";
                cliente.Numero = "90";
                cliente.Piso = "1";
                cliente.Poblacion = "";
                cliente.Tratamiento = "";
                cliente.DescripcionGE = "";
                cliente.IdSapOrganizacionDeVenta = "8000";
                wsclient.ProcesarCliente(cliente);

                //cliente.IdCliente = "9999991";
                //cliente.Activo = true;
                //cliente.Calle = "Ambrosetti";
                //cliente.ClaveGrupo = "";
                //cliente.CP = "1405";
                //cliente.Dto = "A";
                //cliente.GrupoComercial = "";
                //cliente.NombreFantasia = "PS";
                //cliente.Numero = "90";
                //cliente.Piso = "1";
                //cliente.Poblacion = "";
                //cliente.Tratamiento = "";
                //cliente.DescripcionGE = "";
                //cliente.IdSapOrganizacionDeVenta = "5000";
                //wsclient.ProcesarCliente(cliente);

                //cliente.IdCliente = "0001005901";
                //cliente.RazonSocial = "NEW HARVEST S.R.L.";
                //cliente.CUIT = "1014639025";
                //cliente.Activo = true;
                //cliente.Calle = "Av Cristo Redentor E 5° y 6° Anillo";
                //cliente.ClaveGrupo = "";
                //cliente.CP = "1405";
                //cliente.Dto = "A";
                //cliente.GrupoComercial = "";
                //cliente.NombreFantasia = "NEW HARVEST S.R.L.";
                //cliente.Numero = "90";
                //cliente.Piso = "1";
                //cliente.Poblacion = "Santa Cruz de la Sierra";
                //cliente.Tratamiento = "";
                //cliente.DescripcionGE = "";
                //cliente.IdSapOrganizacionDeVenta = "8000";
                //wsclient.ProcesarCliente(cliente);




                //var lista = new List<CartaDePorte.Test.wsCDPSAP.SapMensajePrefactura>();
                //CartaDePorte.Test.wsCDPSAP.SapMensajePrefactura mensaje;

                //lista.Clear();
                //mensaje = new CartaDePorte.Test.wsCDPSAP.SapMensajePrefactura();
                //mensaje.TipoMensaje = "S";
                //mensaje.NroDocumentoRE = "548642021";
                //mensaje.NroDocumentoSap = "0000046436";
                //mensaje.TextoMensaje = "1234567";
                //lista.Add(mensaje);
                //wsclient.RecibirPrefacturacion(lista.ToArray());

                //lista.Clear();
                //mensaje = new CartaDePorte.Test.wsCDPSAP.SapMensajePrefactura();
                //mensaje.TipoMensaje = "S";
                //mensaje.NroDocumentoRE = "543911513";
                //mensaje.NroDocumentoSap = "0000044374";
                //mensaje.TextoMensaje = "1234567";
                //lista.Add(mensaje);
                //wsclient.RecibirPrefacturacion(lista.ToArray());


                //lista.Clear();
                //mensaje = new CartaDePorte.Test.wsCDPSAP.SapMensajePrefactura();
                //mensaje.TipoMensaje = "S";
                //mensaje.NroDocumentoRE = "10037";
                //mensaje.NroDocumentoSap = "0000044373";
                //mensaje.TextoMensaje = "1234567";
                //lista.Add(mensaje);

                //wsclient.RecibirPrefacturacion(lista.ToArray());


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CartaDePorte.Core.Domain;
using CartaDePorte.Core.DAO;
using CartaDePorte.Core.Domain.Seguridad;
using CartaDePorte.Core;
using System.ServiceModel;

namespace CartaDePorte.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CosechaAlta()
        {

            //usuario del proceso
            var cosecha = new Cosecha();
            cosecha.Codigo = DateTime.Now.Ticks.ToString();
            cosecha.Descripcion = DateTime.Now.Ticks.ToString();

            CosechaDAO.Instance.SaveOrUpdate(cosecha); 
            
            
            //fuerzo usuario
            var usuario = new UsuarioFull();
            usuario.SetEmpresa(1);
            usuario.IdPais = 1;
            App.Usuario = usuario;


            cosecha = new Cosecha();
            cosecha.Codigo = DateTime.Now.Ticks.ToString();
            cosecha.Descripcion = DateTime.Now.Ticks.ToString();
            CosechaDAO.Instance.SaveOrUpdate(cosecha);


            cosecha = CosechaDAO.Instance.GetOneByCodigo(cosecha.Codigo);




            //fuerzo otro usuario
            usuario = new UsuarioFull();
            usuario.SetEmpresa(3);
            usuario.IdPais = 3;
            App.Usuario = usuario;

            cosecha = new Cosecha();
            cosecha.Codigo = DateTime.Now.Ticks.ToString();
            cosecha.Descripcion = DateTime.Now.Ticks.ToString();
            CosechaDAO.Instance.SaveOrUpdate(cosecha);

            cosecha = new Cosecha();
            cosecha.Codigo = DateTime.Now.Ticks.ToString();
            cosecha.Descripcion = DateTime.Now.Ticks.ToString();
            CosechaDAO.Instance.SaveOrUpdate(cosecha);


            var all = CosechaDAO.Instance.GetAll();


        }



        [TestMethod]
        public void EspecieAlta()
        {

            //usuario del proceso
            var Especie = new Especie();
            Especie.Codigo = DateTime.Now.Millisecond;
            Especie.Descripcion = DateTime.Now.Ticks.ToString();

            EspecieDAO.Instance.SaveOrUpdate(Especie);


            //fuerzo usuario
            var usuario = new UsuarioFull();
            usuario.SetEmpresa(1);
            usuario.IdPais = 1;
            App.Usuario = usuario;


            Especie = new Especie();
            Especie.Codigo = DateTime.Now.Millisecond;
            Especie.Descripcion = DateTime.Now.Ticks.ToString();
            EspecieDAO.Instance.SaveOrUpdate(Especie);


            Especie = EspecieDAO.Instance.GetOneByCodigo(Especie.Codigo);




            //fuerzo otro usuario
            usuario = new UsuarioFull();
            usuario.SetEmpresa(3);
            usuario.IdPais = 3;
            App.Usuario = usuario;

            Especie = new Especie();
            Especie.Codigo = DateTime.Now.Millisecond;
            Especie.Descripcion = DateTime.Now.Ticks.ToString();
            EspecieDAO.Instance.SaveOrUpdate(Especie);

            Especie = new Especie();
            Especie.Codigo = DateTime.Now.Millisecond;
            Especie.Descripcion = DateTime.Now.Ticks.ToString();
            EspecieDAO.Instance.SaveOrUpdate(Especie);


            var all = EspecieDAO.Instance.GetAll();


        }


        [TestMethod]
        public void ProveedorAlta()
        {

            //usuario del proceso
            var proveedor = new Proveedor();
            proveedor.Activo = true;
            proveedor.Calle = "1";
            proveedor.Ciudad = "1";
            proveedor.CP = "1";
            proveedor.Departamento = "1";
            proveedor.EsProspecto = false;
            proveedor.Nombre = DateTime.Now.Ticks.ToString();
            proveedor.Numero = DateTime.Now.Ticks.ToString();
            proveedor.NumeroDocumento = "1";
            proveedor.Pais = "1";
            proveedor.Sap_Id = DateTime.Now.Ticks.ToString();
            proveedor.TipoDocumento = new TipoDocumentoSAP();
            

            ProveedorDAO.Instance.SaveOrUpdate(proveedor);


            //fuerzo usuario
            var usuario = new UsuarioFull();
            usuario.SetEmpresa(1);
            usuario.IdPais = 1;
            App.Usuario = usuario;


            proveedor = new Proveedor();
            proveedor.Activo = true;
            proveedor.Calle = "1";
            proveedor.Ciudad = "1";
            proveedor.CP = "1";
            proveedor.Departamento = "1";
            proveedor.EsProspecto = false;
            proveedor.Nombre = DateTime.Now.Ticks.ToString();
            proveedor.Numero = DateTime.Now.Ticks.ToString();
            proveedor.NumeroDocumento = "1";
            proveedor.Pais = "1";
            proveedor.Sap_Id = DateTime.Now.Ticks.ToString();
            proveedor.TipoDocumento = new TipoDocumentoSAP();
            ProveedorDAO.Instance.SaveOrUpdate(proveedor);


            proveedor = ProveedorDAO.Instance.GetOne(proveedor.IdProveedor);



            //fuerzo otro usuario
            usuario = new UsuarioFull();
            usuario.SetEmpresa(3);
            usuario.IdPais = 3;
            App.Usuario = usuario;

            proveedor = new Proveedor();
            proveedor.Activo = true;
            proveedor.Calle = "1";
            proveedor.Ciudad = "1";
            proveedor.CP = "1";
            proveedor.Departamento = "1";
            proveedor.EsProspecto = false;
            proveedor.Nombre = DateTime.Now.Ticks.ToString();
            proveedor.Numero = DateTime.Now.Ticks.ToString();
            proveedor.NumeroDocumento = "1";
            proveedor.Pais = "1";
            proveedor.Sap_Id = DateTime.Now.Ticks.ToString();
            proveedor.TipoDocumento = new TipoDocumentoSAP();
            ProveedorDAO.Instance.SaveOrUpdate(proveedor);

            proveedor = new Proveedor();
            proveedor.Activo = true;
            proveedor.Calle = "1";
            proveedor.Ciudad = "1";
            proveedor.CP = "1";
            proveedor.Departamento = "1";
            proveedor.EsProspecto = false;
            proveedor.Nombre = DateTime.Now.Ticks.ToString();
            proveedor.Numero = DateTime.Now.Ticks.ToString();
            proveedor.NumeroDocumento = "1";
            proveedor.Pais = "1";
            proveedor.Sap_Id = DateTime.Now.Ticks.ToString();
            proveedor.TipoDocumento = new TipoDocumentoSAP();
            ProveedorDAO.Instance.SaveOrUpdate(proveedor);


            var all = ProveedorDAO.Instance.GetAll();


        }

        [TestMethod]
        public void ClienteAlta()
        {

            //usuario del proceso
            var cliente = new Cliente();
            cliente.Activo = true;
            cliente.Cuit = "1";
            cliente.DescripcionGe = "1";
            cliente.EsProspecto = false;
            cliente.NombreFantasia = DateTime.Now.Ticks.ToString();
            cliente.IdCliente = DateTime.Now.Millisecond;
            cliente.Numero = DateTime.Now.Millisecond.ToString();
            cliente.RazonSocial = DateTime.Now.Ticks.ToString();
            cliente.TipoDocumento = new TipoDocumentoSAP();

            ClienteDAO.Instance.SaveOrUpdate(cliente);


            //fuerzo usuario
            var usuario = new UsuarioFull();
            App.Usuario = usuario;
            usuario.SetEmpresa(1);
            usuario.IdPais = 1;


            cliente = new Cliente();
            cliente.Activo = true;
            cliente.Cuit = "1";
            cliente.DescripcionGe = "1";
            cliente.EsProspecto = false;
            cliente.NombreFantasia = DateTime.Now.Ticks.ToString();
            cliente.IdCliente = DateTime.Now.Millisecond;
            cliente.Numero = DateTime.Now.Millisecond.ToString();
            cliente.RazonSocial = DateTime.Now.Ticks.ToString();
            cliente.TipoDocumento = new TipoDocumentoSAP();
            ClienteDAO.Instance.SaveOrUpdate(cliente);


            cliente = ClienteDAO.Instance.GetOne(cliente.IdCliente);



            //fuerzo otro usuario
            usuario = new UsuarioFull();
            usuario.SetEmpresa(3);
            usuario.IdPais = 3;
            App.Usuario = usuario;

            cliente = new Cliente();
            cliente.Activo = true;
            cliente.Cuit = "1";
            cliente.DescripcionGe = "1";
            cliente.EsProspecto = false;
            cliente.NombreFantasia = DateTime.Now.Ticks.ToString();
            cliente.IdCliente = DateTime.Now.Millisecond;
            cliente.Numero = DateTime.Now.Millisecond.ToString();
            cliente.RazonSocial = DateTime.Now.Ticks.ToString();
            cliente.TipoDocumento = new TipoDocumentoSAP();
            ClienteDAO.Instance.SaveOrUpdate(cliente);

            cliente = new Cliente();
            cliente.Activo = true;
            cliente.Cuit = "1";
            cliente.DescripcionGe = "1";
            cliente.EsProspecto = false;
            cliente.NombreFantasia = DateTime.Now.Ticks.ToString();
            cliente.IdCliente = DateTime.Now.Millisecond;
            cliente.Numero = DateTime.Now.Millisecond.ToString();
            cliente.RazonSocial = DateTime.Now.Ticks.ToString();
            cliente.TipoDocumento = new TipoDocumentoSAP();
            ClienteDAO.Instance.SaveOrUpdate(cliente);


            var all = ClienteDAO.Instance.GetAll();


        }


        [TestMethod]
        public void WsProveedorProcesar()
        {

            var wsclient = new wsCDPSAP.cdpSAPSoapClient(new BasicHttpBinding(), new EndpointAddress("http://localhost/CartaDePorte.WS/cdpSAP.asmx"));

            ////var proveedor = new wsCDPSAP2.ProveedorXI();
            var proveedor = new wsCDPSAP.ProveedorXI();

            //proveedor.Activo = true;
            //proveedor.Id_Proveedor = "0000100000";
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
            //proveedor.Id_Proveedor = "0000110000";
            //proveedor.NumeroDocumento = "1234567890";
            //proveedor.Nombre = "Pablo Perez";
            //proveedor.Calle = "Ambrosetti";
            //proveedor.Piso = "1";
            //proveedor.Pais = "AR";
            //proveedor.Ciudad = "CABA";
            //proveedor.CP = "1405";
            //proveedor.Departamento = "A";
            //proveedor.Numero = "90";
            //proveedor.IdSapOrganizacionDeVenta = "5000";
            //wsclient.ProcesarProveedor(proveedor);


            proveedor.Activo = true;
            proveedor.Id_Proveedor = "0004100000";
            proveedor.NumeroDocumento = "30563429875";
            proveedor.Nombre = "Pablo Slucka";
            proveedor.Calle = "Ambrosetti";
            proveedor.Piso = "1";
            proveedor.Pais = "AR";
            proveedor.Ciudad = "CABA";
            proveedor.CP = "1405";
            proveedor.Departamento = "A";
            proveedor.Numero = "90";
            proveedor.IdSapOrganizacionDeVenta = "5000";
            wsclient.ProcesarProveedor(proveedor);


            proveedor.Activo = true;
            proveedor.Id_Proveedor = "0004100000";
            proveedor.NumeroDocumento = "1234567890";
            proveedor.Nombre = "Pablo Perez";
            proveedor.Calle = "Ambrosetti";
            proveedor.Piso = "1";
            proveedor.Pais = "AR";
            proveedor.Ciudad = "CABA";
            proveedor.CP = "1405";
            proveedor.Departamento = "A";
            proveedor.Numero = "90";
            proveedor.IdSapOrganizacionDeVenta = "5001";
            wsclient.ProcesarProveedor(proveedor);
        }

        [TestMethod]
        public void WsClienteProcesar()
        {
            //try
            //{
                //var wsclient = new wsCDPSAP2.cdpSAP2SoapClient(new BasicHttpBinding(), new EndpointAddress("http://localhost/CartaDePorte.WS/cdpSAP2.asmx"));

                var wsclient = new wsCDPSAP.cdpSAPSoapClient(new BasicHttpBinding(), new EndpointAddress("http://localhost/CartaDePorte.WS/cdpSAP.asmx"));

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


            //}
            //catch (Exception ex)
            //{
            //    //MessageBox.Show(ex.Message);
            //}


        }
    }
}

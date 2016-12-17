//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Net;
//using CartaDePorte.Core.Domain;
//using CartaDePorte.Core.DAO;

//namespace CartaDePorte.Core.Servicios
//{
//    public class wsAfip
//    {
//        static AfipAuth afipAuth = null;
//        static String CTGServiceURL = null;


//        public wsAfip() {
//            afipAuth = AfipAuthDAO.Instance.Get(1);
//            CTGServiceURL = System.Configuration.ConfigurationSettings.AppSettings["CTGServiceURL"];
//        }

//        #region Metodos Afip
//        public CartaDePorte.Core.CTGService.solicitarCTGReturnType solicitarCTGInicial(Solicitud solicitud)
//        {
//            System.Net.ServicePointManager.ServerCertificateValidationCallback
//            = (sender, certificate, chain, sslPolicyErrors) => { return true; };


//            CTGService.CTGService_v20 service = new CartaDePorte.Core.CTGService.CTGService_v20();
//            service.Proxy = WebProxy;
//            service.Timeout = 3600000;
//            service.Url = CTGServiceURL;

//            CartaDePorte.Core.CTGService.solicitarCTGInicialRequestType request = new CartaDePorte.Core.CTGService.solicitarCTGInicialRequestType();
//            request.auth = new CartaDePorte.Core.CTGService.authType();
//            request.auth.token = afipAuth.Token;
//            request.auth.sign = afipAuth.Sign;
//            request.auth.cuitRepresentado = afipAuth.CuitRepresentado;

//            request.datosSolicitarCTGInicial = new CartaDePorte.Core.CTGService.datosSolicitarCTGInicialType();

//            request.datosSolicitarCTGInicial.cartaPorte = (long)Convert.ToDouble(solicitud.NumeroCartaDePorte);
//            request.datosSolicitarCTGInicial.codigoEspecie = solicitud.Grano.EspecieAfip.Codigo;

//            if (solicitud.ClienteDestino != null && (!String.IsNullOrEmpty(solicitud.ClienteDestino.Cuit)))
//                request.datosSolicitarCTGInicial.cuitDestino = (long)Convert.ToDouble(solicitud.ClienteDestino.Cuit);

//            if (solicitud.ClienteDestinatario != null && (!String.IsNullOrEmpty(solicitud.ClienteDestinatario.Cuit)))
//                request.datosSolicitarCTGInicial.cuitDestinatario = (long)Convert.ToDouble(solicitud.ClienteDestinatario.Cuit);
            
//            request.datosSolicitarCTGInicial.codigoLocalidadOrigen = solicitud.IdEstablecimientoProcedencia.Localidad.Codigo;
//            request.datosSolicitarCTGInicial.codigoLocalidadDestino = solicitud.IdEstablecimientoDestino.Localidad.Codigo;
//            request.datosSolicitarCTGInicial.codigoCosecha = solicitud.Grano.CosechaAfip.Codigo;

//            if (solicitud.CargaPesadaDestino)
//                request.datosSolicitarCTGInicial.pesoNeto = (long)solicitud.KilogramosEstimados;                
//            else {
//                request.datosSolicitarCTGInicial.pesoNeto = (long)solicitud.PesoNeto.Value;
//            }

//            request.datosSolicitarCTGInicial.patente = solicitud.PatenteCamion;

//            request.datosSolicitarCTGInicial.cantHorasSpecified = true;
//            request.datosSolicitarCTGInicial.cantHoras = Convert.ToInt32(solicitud.CantHoras);



//            if (solicitud.ClientePagadorDelFlete != null && solicitud.ClientePagadorDelFlete.IdCliente > 0 && solicitud.ClientePagadorDelFlete.EsEmpresa())
//            {
//                if (solicitud.ProveedorTransportista != null)
//                {
//                    request.datosSolicitarCTGInicial.cuitTransportistaSpecified = true;
//                    request.datosSolicitarCTGInicial.cuitTransportista = (long)Convert.ToDouble(solicitud.ProveedorTransportista.NumeroDocumento); //27054888649;
//                }

//            }
//            else {

//                if (solicitud.ChoferTransportista != null)
//                {
//                    request.datosSolicitarCTGInicial.cuitTransportistaSpecified = true;
//                    request.datosSolicitarCTGInicial.cuitTransportista = (long)Convert.ToDouble(solicitud.ChoferTransportista.Cuit); //27054888649;
//                }
            
//            }




//            if (solicitud.ClienteRemitenteComercial != null)
//            {
//                if (!String.IsNullOrEmpty(solicitud.ClienteRemitenteComercial.Cuit))
//                {
//                    request.datosSolicitarCTGInicial.remitenteComercialComoCanjeador = (solicitud.RemitenteComercialComoCanjeador ? "SI" : "NO");
//                    request.datosSolicitarCTGInicial.cuitCanjeadorSpecified = true;
//                    request.datosSolicitarCTGInicial.cuitCanjeador = (long)Convert.ToDouble(solicitud.ClienteRemitenteComercial.Cuit);// 33504619589;
//                }
//            }
            
//            request.datosSolicitarCTGInicial.kmARecorrer = (uint)Convert.ToDouble(solicitud.KmRecorridos);

//            CartaDePorte.Core.CTGService.solicitarCTGReturnType resul;

//            try
//            {
//                resul = service.solicitarCTGInicial(request);
                
//            }
//            catch (System.Exception e)
//            {
//                CartaDePorte.Core.CTGService.solicitarCTGReturnType resul2 = new CartaDePorte.Core.CTGService.solicitarCTGReturnType();
//                resul2.arrayErrores = new string[1];
//                resul2.arrayErrores[0] = e.Message.ToString();
//                envioMailAlertaAfip("<b>Envio de Solicitud a Afip</b> <br/>" + e.Message.ToString());
//                return resul2;
//            }
//            return resul;
//        }                
//        public CartaDePorte.Core.CTGService.cosechaType[] consultarCosechas()
//        {
//            System.Net.ServicePointManager.ServerCertificateValidationCallback
//            = (sender, certificate, chain, sslPolicyErrors) => { return true; };

//            CTGService.CTGService_v20 service = new CartaDePorte.Core.CTGService.CTGService_v20();
//            service.Proxy = WebProxy;
//            service.Timeout = 3600000;
//            service.Url = CTGServiceURL;

//            string url = service.Url;

//            CartaDePorte.Core.CTGService.consultarCosechasRequestType request = new CartaDePorte.Core.CTGService.consultarCosechasRequestType();
//            request.auth = new CartaDePorte.Core.CTGService.authType();
//            request.auth.token = afipAuth.Token;
//            request.auth.sign = afipAuth.Sign;
//            request.auth.cuitRepresentado = afipAuth.CuitRepresentado;

//            CartaDePorte.Core.CTGService.consultarCosechasReturnType resul = null;
//            try
//            {
//                resul = service.consultarCosechas(request);
//            }
//            catch (System.Exception e)
//            {
//                envioMailAlertaAfip("<b>Consultar Cosechas</b> <br/>" + e.Message.ToString());
//                throw e;
//            }

//            return resul.arrayCosechas;
//        }
//        public CartaDePorte.Core.CTGService.especieType[] consultarEspecies()
//        {

//            System.Net.ServicePointManager.ServerCertificateValidationCallback
//            = (sender, certificate, chain, sslPolicyErrors) => { return true; };

//            CTGService.CTGService_v20 service = new CartaDePorte.Core.CTGService.CTGService_v20();
//            service.Proxy = WebProxy;
//            service.Timeout = 3600000;
//            service.Url = CTGServiceURL;
//            string url = service.Url;


//            CartaDePorte.Core.CTGService.consultarEspeciesRequestType request = new CartaDePorte.Core.CTGService.consultarEspeciesRequestType();
//            request.auth = new CartaDePorte.Core.CTGService.authType();
//            request.auth.token = afipAuth.Token;
//            request.auth.sign = afipAuth.Sign;
//            request.auth.cuitRepresentado = afipAuth.CuitRepresentado;


//            CartaDePorte.Core.CTGService.consultarEspeciesReturnType resul = null;
//            try
//            {
//                resul = service.consultarEspecies(request);
 
//            }
//            catch (System.Exception e)
//            {
//                envioMailAlertaAfip("<b>Consultar Especies</b> <br/>" + e.Message.ToString());
//                throw e;
//            }

//            return resul.arrayEspecies;
//        }
//        public CartaDePorte.Core.CTGService.localidadType[] consultarLocalidadesPorProvinicia(sbyte codigoProvincia)
//        {

//            System.Net.ServicePointManager.ServerCertificateValidationCallback
//            = (sender, certificate, chain, sslPolicyErrors) => { return true; };


//            CTGService.CTGService_v20 service = new CartaDePorte.Core.CTGService.CTGService_v20();
//            service.Proxy = WebProxy;
//            service.Timeout = 3600000;
//            service.Url = CTGServiceURL;
//            string url = service.Url;


//            CartaDePorte.Core.CTGService.consultarLocalidadesPorProvinciaRequestType request = new CartaDePorte.Core.CTGService.consultarLocalidadesPorProvinciaRequestType();
//            request.auth = new CartaDePorte.Core.CTGService.authType();
//            request.auth.token = afipAuth.Token;
//            request.auth.sign = afipAuth.Sign;
//            request.auth.cuitRepresentado = afipAuth.CuitRepresentado;
//            request.codigoProvincia = codigoProvincia;

//            CartaDePorte.Core.CTGService.consultarLocalidadesPorProvinciaReturnType resul = null;
//            try
//            {
//                resul = service.consultarLocalidadesPorProvincia(request);
//            }
//            catch{}

//            return resul.arrayLocalidades;

//        }
//        public CartaDePorte.Core.CTGService.provinciaType[] consultarProvincias()
//        {

//            System.Net.ServicePointManager.ServerCertificateValidationCallback
//            = (sender, certificate, chain, sslPolicyErrors) => { return true; };

//            CTGService.CTGService_v20 service = new CartaDePorte.Core.CTGService.CTGService_v20();
//            service.Proxy = WebProxy;
//            service.Timeout = 3600000;
//            service.Url = CTGServiceURL;
//            string url = service.Url;

//            CartaDePorte.Core.CTGService.consultarProvinciasRequestType request = new CartaDePorte.Core.CTGService.consultarProvinciasRequestType();
//            request.auth = new CartaDePorte.Core.CTGService.authType();
//            request.auth.token = afipAuth.Token;
//            request.auth.sign = afipAuth.Sign;
//            request.auth.cuitRepresentado = afipAuth.CuitRepresentado;


//            CartaDePorte.Core.CTGService.consultarProvinciasReturnType resul = null;
//            try
//            {
//                resul = service.consultarProvincias(request);
//            }
//            catch (System.Exception e)
//            {
//                throw e;
//            }


//            return resul.arrayProvincias; // totales;
//        }
//        public long[] consultarEstablecimientos()
//        {

//            System.Net.ServicePointManager.ServerCertificateValidationCallback
//            = (sender, certificate, chain, sslPolicyErrors) => { return true; };

//            CTGService.CTGService_v20 service = new CartaDePorte.Core.CTGService.CTGService_v20();
//            service.Proxy = WebProxy;
//            service.Timeout = 3600000;
//            service.Url = CTGServiceURL;
//            string url = service.Url;

//            CartaDePorte.Core.CTGService.consultarEstablecimientosRequestType request = new CartaDePorte.Core.CTGService.consultarEstablecimientosRequestType();
//            request.auth = new CartaDePorte.Core.CTGService.authType();
//            request.auth.token = afipAuth.Token;
//            request.auth.sign = afipAuth.Sign;
//            request.auth.cuitRepresentado = afipAuth.CuitRepresentado;

//            CartaDePorte.Core.CTGService.consultarEstablecimientosReturnType resul = null;
//            try
//            {
//                resul = service.consultarEstablecimientos(request);
               
//            }
//            catch (System.Exception e)
//            {

//                throw e;
//            }

//            return resul.arrayEstablecimientos;
//        }
//        public CartaDePorte.Core.CTGService.consultarConstanciaCTGPDFReturnType consultarConstanciaCTGPDF(Int64 Ctg)
//        {
//            System.Net.ServicePointManager.ServerCertificateValidationCallback
//            = (sender, certificate, chain, sslPolicyErrors) => { return true; };

//            CTGService.CTGService_v20 service = new CartaDePorte.Core.CTGService.CTGService_v20();
//            service.Proxy = WebProxy;
//            service.Timeout = 3600000;
//            service.Url = CTGServiceURL;
//            string url = service.Url;

//            CartaDePorte.Core.CTGService.consultarConstanciaCTGPDFRequestType request = new CartaDePorte.Core.CTGService.consultarConstanciaCTGPDFRequestType();
//            request.auth = new CartaDePorte.Core.CTGService.authType();
//            request.auth.token = afipAuth.Token;
//            request.auth.sign = afipAuth.Sign;
//            request.auth.cuitRepresentado = afipAuth.CuitRepresentado;
//            request.ctg = Ctg;

//            CartaDePorte.Core.CTGService.consultarConstanciaCTGPDFReturnType resul = null;
//            try
//            {
//                resul = service.consultarConstanciaCTGPDF(request);
                
//            }
//            catch (System.Exception e)
//            {
//                throw e;
//            }


//            return resul; // totales;
//        }
//        public String consultarCTGExcel(Int64 Ctg)
//        {
//            System.Net.ServicePointManager.ServerCertificateValidationCallback
//            = (sender, certificate, chain, sslPolicyErrors) => { return true; };

//            CTGService.CTGService_v20 service = new CartaDePorte.Core.CTGService.CTGService_v20();
//            service.Proxy = WebProxy;
//            service.Timeout = 3600000;
//            service.Url = CTGServiceURL;
//            string url = service.Url;

//            CartaDePorte.Core.CTGService.consultarCTGRequestType request = new CartaDePorte.Core.CTGService.consultarCTGRequestType();
//            request.auth = new CartaDePorte.Core.CTGService.authType();
//            request.auth.token = afipAuth.Token;
//            request.auth.sign = afipAuth.Sign;
//            request.auth.cuitRepresentado = afipAuth.CuitRepresentado;
//            request.consultarCTGDatos = new CartaDePorte.Core.CTGService.consultarCTGDatosType();


//            CartaDePorte.Core.CTGService.consultarCTGExcelReturnType resul = null;
//            try
//            {
//                resul = service.consultarCTGExcel(request);
//            }
//            catch (System.Exception e)
//            {
//                throw e;
//            }


//            return resul.archivo; // totales;
//        }
//        public CartaDePorte.Core.CTGService.operacionCTGReturnType anularCTG(Int64 NroCartaDePorte,Int64 Ctg)
//        {
//            System.Net.ServicePointManager.ServerCertificateValidationCallback
//            = (sender, certificate, chain, sslPolicyErrors) => { return true; };

//            CTGService.CTGService_v20 service = new CartaDePorte.Core.CTGService.CTGService_v20();
//            service.Proxy = WebProxy;
//            service.Timeout = 3600000;
//            service.Url = CTGServiceURL;
//            string url = service.Url;

//            CartaDePorte.Core.CTGService.anularCTGRequestType request = new CartaDePorte.Core.CTGService.anularCTGRequestType();
//            request.auth = new CartaDePorte.Core.CTGService.authType();
//            request.auth.token = afipAuth.Token;
//            request.auth.sign = afipAuth.Sign;
//            request.auth.cuitRepresentado = afipAuth.CuitRepresentado;
//            request.datosAnularCTG = new CartaDePorte.Core.CTGService.datosCTGType();
//            request.datosAnularCTG.cartaPorte = NroCartaDePorte;
//            request.datosAnularCTG.ctg = Ctg;

//            CartaDePorte.Core.CTGService.operacionCTGReturnType resul = null;
//            try
//            {
//                resul = service.anularCTG(request);

//            }
//            catch (System.Exception e)
//            {
//                envioMailAlertaAfip("<b>Anular CTG</b> <br/>" + e.Message.ToString() + "<br/>cdp: " + NroCartaDePorte.ToString() + "<br/>ctg: " + Ctg.ToString());
//                throw e;
//            }

//            return resul;
//        }
//        public CartaDePorte.Core.CTGService.consultarCTGActivosPorPatenteReturnType consultarCTGActivosPorPatente(string patente)
//        {
//            System.Net.ServicePointManager.ServerCertificateValidationCallback
//            = (sender, certificate, chain, sslPolicyErrors) => { return true; };

//            CTGService.CTGService_v20 service = new CartaDePorte.Core.CTGService.CTGService_v20();
//            service.Proxy = WebProxy;
//            service.Timeout = 3600000;
//            service.Url = CTGServiceURL;
//            string url = service.Url;

//            CartaDePorte.Core.CTGService.consultarCTGActivosPorPatenteRequestType request = new CartaDePorte.Core.CTGService.consultarCTGActivosPorPatenteRequestType();
//            request.auth = new CartaDePorte.Core.CTGService.authType();
//            request.auth.token = afipAuth.Token;
//            request.auth.sign = afipAuth.Sign;
//            request.auth.cuitRepresentado = afipAuth.CuitRepresentado;
//            request.patente = patente;

//            CartaDePorte.Core.CTGService.consultarCTGActivosPorPatenteReturnType resul = null;
//            try
//            {
//                resul = service.consultarCTGActivosPorPatente(request);
//                //service.consultarCTG
//                //service.confirmarArribo
//                //service.confirmarDefinitivo
                    

//            }
//            catch (System.Exception e)
//            {
//                throw e;
//            }

//            return resul;
//        }
//        public CartaDePorte.Core.CTGService.consultarCTGReturnType consultarCTG(DateTime fechadesde)
//        {
//            System.Net.ServicePointManager.ServerCertificateValidationCallback
//            = (sender, certificate, chain, sslPolicyErrors) => { return true; };

//            CTGService.CTGService_v20 service = new CartaDePorte.Core.CTGService.CTGService_v20();
//            service.Proxy = WebProxy;
//            service.Timeout = 3600000;
//            service.Url = CTGServiceURL;
//            string url = service.Url;

//            CartaDePorte.Core.CTGService.consultarCTGRequestType request = new CartaDePorte.Core.CTGService.consultarCTGRequestType();
//            request.auth = new CartaDePorte.Core.CTGService.authType();
//            request.auth.token = afipAuth.Token;
//            request.auth.sign = afipAuth.Sign;
//            request.auth.cuitRepresentado = afipAuth.CuitRepresentado;
//            request.consultarCTGDatos = new CartaDePorte.Core.CTGService.consultarCTGDatosType();
//            request.consultarCTGDatos.fechaEmisionDesde = fechadesde.ToString("dd/MM/yyyy");

//            CartaDePorte.Core.CTGService.consultarCTGReturnType resul = null;            
//            try
//            {
//                resul = service.consultarCTG(request);
//                //service.confirmarArribo
//                //service.confirmarDefinitivo


//            }
//            catch (System.Exception e)
//            {
//                envioMailAlertaAfip("<b>Consultar CTG</b> <br/>" + e.Message.ToString() + "<br/>fechaDesde: " + fechadesde.ToString("dd/MM/yyyy"));
//                throw e;
//            }

//            return resul;
//        }
//        public CartaDePorte.Core.CTGService.operacionCTGReturnType confirmarArribo(Solicitud sol)
//        {
//            System.Net.ServicePointManager.ServerCertificateValidationCallback
//            = (sender, certificate, chain, sslPolicyErrors) => { return true; };

//            CTGService.CTGService_v20 service = new CartaDePorte.Core.CTGService.CTGService_v20();
//            service.Proxy = WebProxy;
//            service.Timeout = 3600000;
//            service.Url = CTGServiceURL;
//            string url = service.Url;

//            CartaDePorte.Core.CTGService.confirmarArriboRequestType request = new CartaDePorte.Core.CTGService.confirmarArriboRequestType();
//            request.auth = new CartaDePorte.Core.CTGService.authType();
//            request.auth.token = afipAuth.Token;
//            request.auth.sign = afipAuth.Sign;
//            request.auth.cuitRepresentado = afipAuth.CuitRepresentado;
//            request.datosConfirmarArribo = new CartaDePorte.Core.CTGService.datosConfirmarArriboType();
//            request.datosConfirmarArribo.ctg = Convert.ToInt64(sol.Ctg);
//            request.datosConfirmarArribo.cantKilosCartaPorte = sol.KilogramosEstimados;
//            request.datosConfirmarArribo.cartaPorte = Convert.ToInt64(sol.NumeroCartaDePorte);
//            request.datosConfirmarArribo.consumoPropio = "S";

//            Int64 cuitTransportista = 0;

//            if (sol.ClientePagadorDelFlete != null && sol.ClientePagadorDelFlete.IdCliente > 0 && sol.ClientePagadorDelFlete.EsEmpresa())
//            {
//                if (sol.ProveedorTransportista != null)
//                    cuitTransportista = Convert.ToInt64(sol.ProveedorTransportista.NumeroDocumento);
//            }
//            else {

//                if (sol.ChoferTransportista != null)
//                    cuitTransportista = Convert.ToInt64(sol.ChoferTransportista.Cuit);
//            }


//            request.datosConfirmarArribo.cuitTransportista = cuitTransportista;

//            request.datosConfirmarArribo.establecimientoSpecified = false;
//            if (!String.IsNullOrEmpty(sol.IdEstablecimientoProcedencia.EstablecimientoAfip.Trim()))
//            {
//                request.datosConfirmarArribo.establecimiento = Convert.ToInt64(sol.IdEstablecimientoProcedencia.EstablecimientoAfip.Trim());
//                request.datosConfirmarArribo.establecimientoSpecified = true;
//                request.datosConfirmarArribo.consumoPropio = "N";
//            }
            
//            CartaDePorte.Core.CTGService.operacionCTGReturnType resul = null;
//            try
//            {
//                resul = service.confirmarArribo(request);                
//                //service.confirmarDefinitivo
//            }
//            catch (System.Exception e)
//            {
//                envioMailAlertaAfip("<b>Confirmar Arribo</b> <br/>" + e.Message.ToString() + "<br/>NumeroCartadeporte: " + sol.NumeroCartaDePorte + "<br/>UsuarioCreacion: " + sol.UsuarioCreacion);
//                throw e;
//            }

//            return resul;
//        }
//        public CartaDePorte.Core.CTGService.confirmarDefinitivoReturnType confirmarDefinitivo(Solicitud sol)
//        {
//            System.Net.ServicePointManager.ServerCertificateValidationCallback
//            = (sender, certificate, chain, sslPolicyErrors) => { return true; };

//            CTGService.CTGService_v20 service = new CartaDePorte.Core.CTGService.CTGService_v20();
//            service.Proxy = WebProxy;
//            service.Timeout = 3600000;
//            service.Url = CTGServiceURL;
//            string url = service.Url;

//            CartaDePorte.Core.CTGService.confirmarDefinitivoRequestType request = new CartaDePorte.Core.CTGService.confirmarDefinitivoRequestType();
//            request.auth = new CartaDePorte.Core.CTGService.authType();
//            request.auth.token = afipAuth.Token;
//            request.auth.sign = afipAuth.Sign;
//            request.auth.cuitRepresentado = afipAuth.CuitRepresentado;
//            request.datosConfirmarDefinitivo = new CartaDePorte.Core.CTGService.datosConfirmarDefinitivoType();
//            request.datosConfirmarDefinitivo.ctg = Convert.ToInt64(sol.Ctg);
//            request.datosConfirmarDefinitivo.cartaPorte = Convert.ToInt64(sol.NumeroCartaDePorte);

//            CartaDePorte.Core.CTGService.confirmarDefinitivoReturnType resul = null;
//            try
//            {
//                resul = service.confirmarDefinitivo(request);
//            }
//            catch (System.Exception e)
//            {
//                throw e;
//            }

//            return resul;
//        }
//        /// <summary>
//        /// Cambio de destino
//        /// </summary>
//        /// <param name="sol">Una Solicitud: datos que requiere son: cartaPorte, codigoLocalidadDestino, ctg, cuitDestinatario, cuitDestinatario, cuitDestino</param>
//        /// <returns></returns>
//        public CartaDePorte.Core.CTGService.operacionCTGReturnType cambiarDestinoDestinatarioCTGRechazado(Solicitud sol)
//        {
//            System.Net.ServicePointManager.ServerCertificateValidationCallback
//            = (sender, certificate, chain, sslPolicyErrors) => { return true; };

//            CTGService.CTGService_v20 service = new CartaDePorte.Core.CTGService.CTGService_v20();
//            service.Proxy = WebProxy;
//            service.Timeout = 3600000;
//            service.Url = CTGServiceURL;
//            string url = service.Url;

//            CartaDePorte.Core.CTGService.cambiarDestinoDestinatarioCTGRechazadoRequestType request = new CartaDePorte.Core.CTGService.cambiarDestinoDestinatarioCTGRechazadoRequestType();
//            request.auth = new CartaDePorte.Core.CTGService.authType();
//            request.auth.token = afipAuth.Token;
//            request.auth.sign = afipAuth.Sign;
//            request.auth.cuitRepresentado = afipAuth.CuitRepresentado;
//            request.datosCambiarDestinoDestinatarioCTGRechazado = new CartaDePorte.Core.CTGService.datosCambiarDestinoDestinatarioCTGRechazadoType();
//            request.datosCambiarDestinoDestinatarioCTGRechazado.cartaPorte =  Convert.ToInt64(sol.NumeroCartaDePorte);
//            request.datosCambiarDestinoDestinatarioCTGRechazado.codigoLocalidadDestino = sol.IdEstablecimientoDestinoCambio.Localidad.Codigo;
//            request.datosCambiarDestinoDestinatarioCTGRechazado.ctg = Convert.ToInt64(sol.Ctg);
//            request.datosCambiarDestinoDestinatarioCTGRechazado.cuitDestinatario = Convert.ToInt64(sol.ClienteDestinatarioCambio.Cuit);
//            request.datosCambiarDestinoDestinatarioCTGRechazado.cuitDestino = Convert.ToInt64(sol.IdEstablecimientoDestinoCambio.IdInterlocutorDestinatario.Cuit);
//            request.datosCambiarDestinoDestinatarioCTGRechazado.kmARecorrer = (uint)sol.KmRecorridos;

//            CartaDePorte.Core.CTGService.operacionCTGReturnType resul = null;
//            try
//            {
//                resul = service.cambiarDestinoDestinatarioCTGRechazado(request);
//            }
//            catch (System.Exception e)
//            {
//                envioMailAlertaAfip("<b>cambiarDestinoDestinatarioCTGRechazado</b> <br/>" + e.Message.ToString() + "<br/>NumeroCartadeporte: " + sol.NumeroCartaDePorte + "<br/>UsuarioCreacion: " + sol.UsuarioCreacion);
//                throw e;
//            }

//            return resul;
//        }
//        /// <summary>
//        /// Desvio CTG a otro destino
//        /// </summary>
//        /// <param name="sol">Una Solicitud, con estos datos: cuitDestino, cartaPorte, ctg</param>
//        /// <returns></returns>
//        public CartaDePorte.Core.CTGService.operacionCTGReturnType desviarCTGAOtroDestino(Solicitud sol)
//        {
//            System.Net.ServicePointManager.ServerCertificateValidationCallback
//            = (sender, certificate, chain, sslPolicyErrors) => { return true; };

//            CTGService.CTGService_v20 service = new CartaDePorte.Core.CTGService.CTGService_v20();
//            service.Proxy = WebProxy;
//            service.Timeout = 3600000;
//            service.Url = CTGServiceURL;
//            string url = service.Url;

//            CartaDePorte.Core.CTGService.desviarCTGAOtroDestinoRequestType request = new CartaDePorte.Core.CTGService.desviarCTGAOtroDestinoRequestType();
//            request.auth = new CartaDePorte.Core.CTGService.authType();
//            request.auth.token = afipAuth.Token;
//            request.auth.sign = afipAuth.Sign;
//            request.auth.cuitRepresentado = afipAuth.CuitRepresentado;
//            request.cuitDestino = Convert.ToInt64(sol.ClienteDestino.Cuit);
//            request.datosDesviarCTG.cartaPorte = Convert.ToInt64(sol.NumeroCartaDePorte);
//            request.datosDesviarCTG.ctg = Convert.ToInt64(sol.Ctg);


//            CartaDePorte.Core.CTGService.operacionCTGReturnType resul = null;
//            try
//            {
//                resul = service.desviarCTGAOtroDestino(request);
//            }
//            catch (System.Exception e)
//            {
//                throw e;
//            }

//            return resul;
//        }
//        /// <summary>
//        /// Desviar CTG a otro establecimiento
//        /// </summary>
//        /// <param name="sol">Una Solicitud con los datos: cartaPorte, ctg, codigoLocalidadDestino </param>
//        /// <returns></returns>
//        public CartaDePorte.Core.CTGService.operacionCTGReturnType desviarCTGAOtroEstablecimiento(Solicitud sol)
//        {
//            System.Net.ServicePointManager.ServerCertificateValidationCallback
//            = (sender, certificate, chain, sslPolicyErrors) => { return true; };

//            CTGService.CTGService_v20 service = new CartaDePorte.Core.CTGService.CTGService_v20();
//            service.Proxy = WebProxy;
//            service.Timeout = 3600000;
//            service.Url = CTGServiceURL;
//            string url = service.Url;

//            CartaDePorte.Core.CTGService.desviarCTGAOtroEstablecimientoRequestType request = new CartaDePorte.Core.CTGService.desviarCTGAOtroEstablecimientoRequestType();
//            request.auth = new CartaDePorte.Core.CTGService.authType();
//            request.auth.token = afipAuth.Token;
//            request.auth.sign = afipAuth.Sign;
//            request.auth.cuitRepresentado = afipAuth.CuitRepresentado;
//            request.datosDesviarCTG = new CartaDePorte.Core.CTGService.datosDesviarCTGType();
//            request.datosDesviarCTG.cartaPorte = Convert.ToInt64(sol.NumeroCartaDePorte);
//            request.datosDesviarCTG.ctg = Convert.ToInt64(sol.Ctg);
//            request.datosDesviarCTG.codigoLocalidadDestino = sol.IdEstablecimientoDestino.Localidad.Codigo;

//            CartaDePorte.Core.CTGService.operacionCTGReturnType resul = null;
//            try
//            {
//                resul = service.desviarCTGAOtroEstablecimiento(request);
//            }
//            catch (System.Exception e)
//            {
//                throw e;
//            }

//            return resul;
//        }
//        public CartaDePorte.Core.CTGService.operacionCTGReturnType rechazarCTG(Solicitud sol)
//        {
//            System.Net.ServicePointManager.ServerCertificateValidationCallback
//            = (sender, certificate, chain, sslPolicyErrors) => { return true; };

//            CTGService.CTGService_v20 service = new CartaDePorte.Core.CTGService.CTGService_v20();
//            service.Proxy = WebProxy;
//            service.Timeout = 3600000;
//            service.Url = CTGServiceURL;
//            string url = service.Url;

//            CartaDePorte.Core.CTGService.rechazarCTGRequestType request = new CartaDePorte.Core.CTGService.rechazarCTGRequestType();
//            request.auth = new CartaDePorte.Core.CTGService.authType();
//            request.auth.token = afipAuth.Token;
//            request.auth.sign = afipAuth.Sign;
//            request.auth.cuitRepresentado = afipAuth.CuitRepresentado;
//            request.datosRechazarCTG = new CartaDePorte.Core.CTGService.datosRechazarCTGType();
//            request.datosRechazarCTG.cartaPorte = Convert.ToInt64(sol.NumeroCartaDePorte);
//            request.datosRechazarCTG.ctg = Convert.ToInt64(sol.Ctg);
//            request.datosRechazarCTG.motivoRechazo = string.Empty;

//            CartaDePorte.Core.CTGService.operacionCTGReturnType resul = null;
//            try
//            {
//                resul = service.rechazarCTG(request);                
//            }
//            catch (System.Exception e)
//            {
//                throw e;
//            }

//            return resul;
//        }
//        public CartaDePorte.Core.CTGService.solicitarCTGReturnType solicitarCTGDatoPendiente(Solicitud sol)
//        {
//            System.Net.ServicePointManager.ServerCertificateValidationCallback
//            = (sender, certificate, chain, sslPolicyErrors) => { return true; };

//            CTGService.CTGService_v20 service = new CartaDePorte.Core.CTGService.CTGService_v20();
//            service.Proxy = WebProxy;
//            service.Timeout = 3600000;
//            service.Url = CTGServiceURL;
//            string url = service.Url;

//            CartaDePorte.Core.CTGService.solicitarCTGDatoPendienteRequestType request = new CartaDePorte.Core.CTGService.solicitarCTGDatoPendienteRequestType();
//            request.auth = new CartaDePorte.Core.CTGService.authType();
//            request.auth.token = afipAuth.Token;
//            request.auth.sign = afipAuth.Sign;
//            request.auth.cuitRepresentado = afipAuth.CuitRepresentado;
//            request.datosSolicitarCTGDatoPendiente = new CartaDePorte.Core.CTGService.datosSolicitarCTGDatoPendienteType();
//            request.datosSolicitarCTGDatoPendiente.cantHoras = Convert.ToInt32(sol.CantHoras);
//            request.datosSolicitarCTGDatoPendiente.cantHorasSpecified = true;
//            request.datosSolicitarCTGDatoPendiente.cartaPorte = Convert.ToInt64(sol.NumeroCartaDePorte);
//            request.datosSolicitarCTGDatoPendiente.cuitTransportistaSpecified = false;
//            request.datosSolicitarCTGDatoPendiente.patente = sol.PatenteCamion;

//            CartaDePorte.Core.CTGService.solicitarCTGReturnType resul = null;
//            try
//            {
//                resul = service.solicitarCTGDatoPendiente(request);
//            }
//            catch (System.Exception e)
//            {
//                throw e;
//            }

//            return resul;
//        }
//        public CartaDePorte.Core.CTGService.operacionCTGReturnType regresarAOrigenCTGRechazado(Solicitud sol)
//        {
//            System.Net.ServicePointManager.ServerCertificateValidationCallback
//            = (sender, certificate, chain, sslPolicyErrors) => { return true; };

//            CTGService.CTGService_v20 service = new CartaDePorte.Core.CTGService.CTGService_v20();
//            service.Proxy = WebProxy;
//            service.Timeout = 3600000;
//            service.Url = CTGServiceURL;
//            string url = service.Url;

//            CartaDePorte.Core.CTGService.regresarAOrigenCTGRechazadoRequestType request = new CartaDePorte.Core.CTGService.regresarAOrigenCTGRechazadoRequestType();
//            request.auth = new CartaDePorte.Core.CTGService.authType();
//            request.auth.token = afipAuth.Token;
//            request.auth.sign = afipAuth.Sign;
//            request.auth.cuitRepresentado = afipAuth.CuitRepresentado;
//            request.datosRegresarAOrigenCTGRechazado = new CartaDePorte.Core.CTGService.datosRegresarAOrigenCTGRechazadoType();
//            request.datosRegresarAOrigenCTGRechazado.cartaPorte = Convert.ToInt64(sol.NumeroCartaDePorte);
//            request.datosRegresarAOrigenCTGRechazado.ctg = Convert.ToInt64(sol.Ctg);
//            request.datosRegresarAOrigenCTGRechazado.kmARecorrer = (uint)sol.KmRecorridos;


//            CartaDePorte.Core.CTGService.operacionCTGReturnType resul = null;
//            try
//            {
//                resul = service.regresarAOrigenCTGRechazado(request);
//            }
//            catch (System.Exception e)
//            {
//                envioMailAlertaAfip("<b>regresarAOrigenCTGRechazado</b> <br/>" + e.Message.ToString() + "<br/>NumeroCartadeporte: " + sol.NumeroCartaDePorte + "<br/>UsuarioCreacion: " + sol.UsuarioCreacion);
//                throw e;
//            }

//            return resul;
//        }

//        #endregion

//        #region Actualizacion Datos Afip

//        public void ActualizarEspecie()
//        {
//            Especie especie;

//            CartaDePorte.Core.CTGService.especieType[] resul = consultarEspecies();
//            foreach (CartaDePorte.Core.CTGService.especieType dato in resul)
//            {
//                especie = EspecieDAO.Instance.GetOneByCodigo(dato.codigo);
//                if (especie == null)
//                    especie = new Especie();

//                especie.Codigo = dato.codigo;
//                especie.Descripcion = dato.descripcion;
                
//                if(especie.IdEspecie == 0)
//                    EspecieDAO.Instance.SaveOrUpdate(especie);

//                System.Threading.Thread.Sleep(2000);

//            }
        
//        }

//        public void ActualizarCosecha()
//        {
//            Cosecha cosecha = new Cosecha();;

//            CartaDePorte.Core.CTGService.cosechaType[] resul = consultarCosechas();
//            foreach (CartaDePorte.Core.CTGService.cosechaType dato in resul)
//            {
//                cosecha = CosechaDAO.Instance.GetOneByCodigo(dato.codigo);
//                if (cosecha == null)
//                    cosecha = new Cosecha();

//                cosecha.Codigo = dato.codigo;
//                cosecha.Descripcion = dato.descripcion;

//                if (cosecha.IdCosecha == 0)
//                    CosechaDAO.Instance.SaveOrUpdate(cosecha);

//                System.Threading.Thread.Sleep(2000);

//            }

//        }

//        #endregion

//        #region NetworkCredential

//        public NetworkCredential NetWorkCredential
//        {
//            get
//            {
//                return new NetworkCredential("procmailer", "prc01mail#07snd", "IRSACORP");
//            }
//        }

//        public WebProxy WebProxy
//        {
//            get
//            {
//                WebProxy proxy = new WebProxy("http://SRV-MS20-ADM:8080");
//                proxy.Credentials = NetWorkCredential;

//                return proxy;
//            }
//        }
//        #endregion

//        #region envioAlertas
//        private void envioMailAlertaAfip(String mensaje)
//        {
//            EnvioMailDAO.Instance.sendMail(mensaje);
//        }
        
//        #endregion

//    }


    
//}

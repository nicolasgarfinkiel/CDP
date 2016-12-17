using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Configuration;

using CartaDePorte.Core.DAO;
using CartaDePorte.Core.Domain;
using CartaDePorte.Common;

namespace CartaDePorte.Core.Utilidades
{
    public class DrawingCDP
    {
        #region Constructor
        private static DrawingCDP instance;


        private string _rutaOriginalCartaDePorte;

        public DrawingCDP()
        {
            this._rutaOriginalCartaDePorte = ConfigurationManager.AppSettings["RutaOriginalCartaDePorte"];
        }

        public static DrawingCDP Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DrawingCDP();
                }
                return instance;
            }
        }
        #endregion

        public void CrearCartaDePorte(int idSolicitud)
        {
            SolicitudFull solicitud = SolicitudDAO.Instance.GetOne(idSolicitud);
            string imageFilePath = string.Empty;
            if (solicitud != null)
            {
                SolicitudParaImagen solicitudP = CargarSolicitud(solicitud);

                System.IO.Directory.CreateDirectory(this._rutaOriginalCartaDePorte + "\\" + idSolicitud.ToString());

                GenerarContenido(idSolicitud.ToString(), solicitudP, 1);
                GenerarContenido(idSolicitud.ToString(), solicitudP, 2);
                GenerarContenido(idSolicitud.ToString(), solicitudP, 3);
                GenerarContenido(idSolicitud.ToString(), solicitudP, 4);

            }

        }
        private void GenerarContenido(string idSolicitud, SolicitudParaImagen solicitud, int nroOrden)
        {
            string imageFilePath = string.Empty;
            imageFilePath = this._rutaOriginalCartaDePorte + "CP" + nroOrden.ToString() + "b.jpg";
            Bitmap bitmap = (Bitmap)Image.FromFile(imageFilePath);
            Graphics graphics = Graphics.FromImage(bitmap);
            GenerarTextos(graphics, solicitud, 0);
            bitmap.Save(this._rutaOriginalCartaDePorte + "\\" + idSolicitud + "\\" + idSolicitud + "_" + nroOrden.ToString() + ".jpg");

        }
        private void GenerarTextos(Graphics graphics, SolicitudParaImagen solicitud, float yInicial)
        {

            // Codigo de barras
            //SeteoCodigoBarras(graphics, solicitud.NumeroCartaDePorte, 530, 90, 12, yInicial);
            //SeteoCodigoBarras(graphics, solicitud.NumeroCEE, 900, 90, 12, yInicial);
            SeteoTexto(graphics, solicitud.NumeroCartaDePorte, 530, 90, 12, yInicial);
            SeteoTexto(graphics, solicitud.NumeroCEE, 950, 90, 12, yInicial);


            if (!String.IsNullOrEmpty(solicitud.NumeroCTG))
            {
                // Cabecera
                SeteoTexto(graphics, solicitud.NumeroCTG, 600, 195, 6, yInicial);
                SeteoTexto(graphics, solicitud.FechaCargaDia, 1277, 180, 6, yInicial);
                SeteoTexto(graphics, solicitud.FechaVencimiento, 1277, 215, 6, yInicial);

                //Datos de intervinientes en el traslado de granos
                float distanciaRow = 50f;
                float nombre = 400;
                float cuit = 1180;
                int font = 6;

                SeteoTexto(graphics, solicitud.NombreTitularCartaPorte, nombre, 370, font, yInicial);
                SeteoTexto(graphics, solicitud.CuitTitularCartaPorte, cuit, 370, font, yInicial);

                SeteoTexto(graphics, solicitud.NombreIntermediario, nombre, 370 + (distanciaRow * 1), font, yInicial);
                SeteoTexto(graphics, solicitud.CuitIntermediario, cuit, 370 + (distanciaRow * 1), font, yInicial);

                SeteoTexto(graphics, solicitud.NombreRemitenteComercial, nombre, 370 + (distanciaRow * 2), font, yInicial);
                SeteoTexto(graphics, solicitud.CuitRemitenteComercial, cuit, 370 + (distanciaRow * 2), font, yInicial);

                SeteoTexto(graphics, solicitud.NombreCorredor, nombre, 370 + (distanciaRow * 3), font, yInicial);
                SeteoTexto(graphics, solicitud.CuitCorredor, cuit, 370 + (distanciaRow * 3), font, yInicial);

                SeteoTexto(graphics, solicitud.NombreEntregador, nombre, 370 + (distanciaRow * 4), font, yInicial);
                SeteoTexto(graphics, solicitud.CuitEntregador, cuit, 370 + (distanciaRow * 4), font, yInicial);

                SeteoTexto(graphics, solicitud.NombreDestinatario, nombre, 370 + (distanciaRow * 5), font, yInicial);
                SeteoTexto(graphics, solicitud.CuitDestinatario, cuit, 370 + (distanciaRow * 5), font, yInicial);

                SeteoTexto(graphics, solicitud.NombreDestino, nombre, 370 + (distanciaRow * 6), font, yInicial);
                SeteoTexto(graphics, solicitud.CuitDestino, cuit, 370 + (distanciaRow * 6), font, yInicial);

                SeteoTexto(graphics, solicitud.NombreTransportista, nombre, 370 + (distanciaRow * 7), font, yInicial);
                SeteoTexto(graphics, solicitud.CuitTransportista, cuit, 370 + (distanciaRow * 7), font, yInicial);

                SeteoTexto(graphics, solicitud.NombreChofer, nombre, 370 + (distanciaRow * 8), font, yInicial);
                SeteoTexto(graphics, solicitud.CuitChofer, cuit, 370 + (distanciaRow * 8), font, yInicial);

                //Datos de los granos / Especies Transportados
                SeteoTexto(graphics, solicitud.GranoEspecie, 252, 865, font, yInicial);
                SeteoTexto(graphics, solicitud.TipoGranoEspecie, 672, 865, font, yInicial);
                SeteoTexto(graphics, solicitud.Cosecha, 1152, 820, font, yInicial);
                SeteoTexto(graphics, solicitud.NroContrato, 1152, 865, font, yInicial);

                SeteoTexto(graphics, solicitud.CargaPesadaDestino, 320, 930, font, yInicial);
                SeteoTexto(graphics, solicitud.KgrsEstimados, 255, 990, font, yInicial);
                SeteoTexto(graphics, solicitud.Conforme, 618, 950, font, yInicial);
                SeteoTexto(graphics, solicitud.Condicional, 618, 993, font, yInicial);

                SeteoTexto(graphics, solicitud.PesoBruto, 870, 905, font, yInicial);
                SeteoTexto(graphics, solicitud.PesoTara, 870, 948, font, yInicial);
                SeteoTexto(graphics, solicitud.PesoNeto, 870, 990, font, yInicial);

                SeteoTexto(graphics, (solicitud.Observaciones.Length > 29) ? solicitud.Observaciones.Substring(0, 30) : solicitud.Observaciones, 1000, 940, 6, yInicial);

                // Procedencia de la mercaderia
                SeteoTexto(graphics, solicitud.DireccionEstablecimientoProcedencia, 200, 1097, font, yInicial);
                SeteoTexto(graphics, solicitud.NombreEstablecimientoProcedencia, 1040, 1035, font, yInicial);
                SeteoTexto(graphics, solicitud.LocalidadEstablecimientoProcedencia, 1040, 1077, 5, yInicial);
                SeteoTexto(graphics, solicitud.ProvinciaEstablecimientoProcedencia, 1040, 1118, 5, yInicial);

                // Destino de la mercaderia
                SeteoTexto(graphics, solicitud.DireccionEstablecimientoDestino, 200, 1231, font, yInicial);
                SeteoTexto(graphics, solicitud.LocalidadEstablecimientoDestino, 1040, 1212, 5, yInicial);
                SeteoTexto(graphics, solicitud.ProvinciaEstablecimientoDestino, 1040, 1255, 5, yInicial);

                // Datos del transportista
                SeteoTexto(graphics, solicitud.Camion, 240, 1348, font, yInicial);
                SeteoTexto(graphics, solicitud.Acoplado, 240, 1393, font, yInicial);
                SeteoTexto(graphics, solicitud.KmRecorrer, 240, 1430, font, yInicial);

                SeteoTexto(graphics, solicitud.NombrePagadorDelFlete, 870, 1305, font, yInicial);

                SeteoTexto(graphics, solicitud.FletePag, 490, 1348, font, yInicial);
                SeteoTexto(graphics, solicitud.FleteAPag, 700, 1348, font, yInicial); //

                SeteoTexto(graphics, solicitud.TarifaReferencia, 610, 1392, font, yInicial);
                SeteoTexto(graphics, solicitud.TarifaReal, 610, 1432, font, yInicial);


            }

        }
        private void GenerarTextosTodoEnUno(Graphics graphics, SolicitudParaImagen solicitud, float yInicial)
        {
            // Cabecera
            SeteoTexto(graphics, solicitud.NumeroCTG, 320, 100, 11, yInicial);
            SeteoTexto(graphics, solicitud.FechaCargaDia, 655, 93, 10, yInicial);
            SeteoTexto(graphics, solicitud.FechaVencimiento, 655, 111, 10, yInicial);

            //Datos de intervinientes en el traslado de granos
            float distanciaRow = 25.8f;
            float nombre = 200;
            float cuit = 610;
            int font = 10;

            SeteoTexto(graphics, solicitud.NombreTitularCartaPorte, nombre, 190, font, yInicial);
            SeteoTexto(graphics, solicitud.CuitTitularCartaPorte, cuit, 190, font, yInicial);

            SeteoTexto(graphics, solicitud.NombreIntermediario, nombre, 190 + (distanciaRow * 1), font, yInicial);
            SeteoTexto(graphics, solicitud.CuitIntermediario, cuit, 190 + (distanciaRow * 1), font, yInicial);

            SeteoTexto(graphics, solicitud.NombreRemitenteComercial, nombre, 190 + (distanciaRow * 2), font, yInicial);
            SeteoTexto(graphics, solicitud.CuitRemitenteComercial, cuit, 190 + (distanciaRow * 2), font, yInicial);

            SeteoTexto(graphics, solicitud.NombreCorredor, nombre, 190 + (distanciaRow * 3), font, yInicial);
            SeteoTexto(graphics, solicitud.CuitCorredor, cuit, 190 + (distanciaRow * 3), font, yInicial);

            SeteoTexto(graphics, solicitud.NombreEntregador, nombre, 190 + (distanciaRow * 4), font, yInicial);
            SeteoTexto(graphics, solicitud.CuitEntregador, cuit, 190 + (distanciaRow * 4), font, yInicial);

            SeteoTexto(graphics, solicitud.NombreDestinatario, nombre, 190 + (distanciaRow * 5), font, yInicial);
            SeteoTexto(graphics, solicitud.CuitDestinatario, cuit, 190 + (distanciaRow * 5), font, yInicial);

            SeteoTexto(graphics, solicitud.NombreDestino, nombre, 190 + (distanciaRow * 6), font, yInicial);
            SeteoTexto(graphics, solicitud.CuitDestino, cuit, 190 + (distanciaRow * 6), font, yInicial);

            SeteoTexto(graphics, solicitud.NombreTransportista, nombre, 190 + (distanciaRow * 7), font, yInicial);
            SeteoTexto(graphics, solicitud.CuitTransportista, cuit, 190 + (distanciaRow * 7), font, yInicial);

            SeteoTexto(graphics, solicitud.NombreChofer, nombre, 190 + (distanciaRow * 8), font, yInicial);
            SeteoTexto(graphics, solicitud.CuitChofer, cuit, 190 + (distanciaRow * 8), font, yInicial);


            //Datos de los granos / Especies Transportados
            SeteoTexto(graphics, solicitud.GranoEspecie, 125, 445, font, yInicial);
            SeteoTexto(graphics, solicitud.TipoGranoEspecie, 340, 445, font, yInicial);
            SeteoTexto(graphics, solicitud.Cosecha, 590, 425, font, yInicial);
            SeteoTexto(graphics, solicitud.NroContrato, 590, 445, font, yInicial);

            SeteoTexto(graphics, solicitud.CargaPesadaDestino, 165, 479, font, yInicial);
            SeteoTexto(graphics, solicitud.KgrsEstimados, 130, 512, font, yInicial);
            SeteoTexto(graphics, solicitud.Conforme, 318, 490, font, yInicial);
            SeteoTexto(graphics, solicitud.Condicional, 318, 512, font, yInicial);

            SeteoTexto(graphics, solicitud.PesoBruto, 450, 468, font, yInicial);
            SeteoTexto(graphics, solicitud.PesoTara, 450, 490, font, yInicial);
            SeteoTexto(graphics, solicitud.PesoNeto, 450, 512, font, yInicial);

            SeteoTexto(graphics, (solicitud.Observaciones.Length > 29) ? solicitud.Observaciones.Substring(0, 30) : solicitud.Observaciones, 511, 482, 9, yInicial);

            // Procedencia de la mercaderia
            SeteoTexto(graphics, solicitud.DireccionEstablecimientoProcedencia, 100, 565, font, yInicial);
            SeteoTexto(graphics, solicitud.NombreEstablecimientoProcedencia, 532, 533, 9, yInicial);
            SeteoTexto(graphics, solicitud.LocalidadEstablecimientoProcedencia, 532, 555, 8, yInicial);
            SeteoTexto(graphics, solicitud.ProvinciaEstablecimientoProcedencia, 532, 577, 8, yInicial);

            // Destino de la mercaderia
            SeteoTexto(graphics, solicitud.DireccionEstablecimientoDestino, 100, 635, font, yInicial);
            SeteoTexto(graphics, solicitud.LocalidadEstablecimientoDestino, 532, 625, 8, yInicial);
            SeteoTexto(graphics, solicitud.ProvinciaEstablecimientoDestino, 532, 647, 8, yInicial);

            // Datos del transportista
            SeteoTexto(graphics, solicitud.Camion, 120, 695, font, yInicial);
            SeteoTexto(graphics, solicitud.Acoplado, 120, 718, font, yInicial);
            SeteoTexto(graphics, solicitud.KmRecorrer, 120, 739, font, yInicial);

            SeteoTexto(graphics, solicitud.FletePag, 255, 695, font, yInicial);
            SeteoTexto(graphics, solicitud.FleteAPag, 360, 695, font, yInicial);

            SeteoTexto(graphics, solicitud.TarifaReferencia, 310, 718, font, yInicial);
            SeteoTexto(graphics, solicitud.TarifaReal, 310, 739, font, yInicial);


        }
        private void SeteoTexto(Graphics grafico, String texto, float x, float y, int fontsize, float yInicial)
        {
            using (Font arialFont = new Font("Arial", fontsize))
            {
                PointF location = new PointF(x, y + yInicial);
                grafico.DrawString(texto, arialFont, Brushes.Black, location);
            }
        }
        private void SeteoCodigoBarras(Graphics grafico, String texto, float x, float y, int fontsize, float yInicial)
        {
            using (Font interFont = new Font("Interleaved 2of5", fontsize))
            {
                PointF location = new PointF(x, y + yInicial);
                grafico.DrawString(texto, interFont, Brushes.Black, location);
            }
        }
        private SolicitudParaImagen CargarSolicitud(SolicitudFull solicitud)
        {

            SolicitudParaImagen solRep = new SolicitudParaImagen();
            if (solicitud != null)
            {

                // Codigo de barra
                solRep.NumeroCartaDePorte = solicitud.NumeroCartaDePorte;
                solRep.NumeroCEE = solicitud.Cee;
                solRep.NumeroCTG = solicitud.Ctg;

                if (!String.IsNullOrEmpty(solRep.NumeroCTG) || solicitud.IdGrupoEmpresa != App.ID_GRUPO_CRESUD)
                {

                    // Cabecera
                    solRep.NumeroCTG = solicitud.Ctg;

                    if (solicitud.FechaDeCarga.HasValue)
                        solRep.FechaCargaDia = solicitud.FechaDeCarga.Value.ToString("dd/MM/yyyy");


                    if (solicitud.FechaDeVencimiento.HasValue)
                        solRep.FechaVencimiento = solicitud.FechaDeVencimiento.Value.ToString("dd/MM/yyyy");

                    // Intervinientes del traslado
                    if (solicitud.ProveedorTitularCartaDePorte != null)
                    {
                        solRep.NombreTitularCartaPorte = solicitud.ProveedorTitularCartaDePorte.Nombre;
                        solRep.CuitTitularCartaPorte = solicitud.ProveedorTitularCartaDePorte.NumeroDocumento;
                    }

                    if (solicitud.ClienteIntermediario != null)
                    {
                        solRep.NombreIntermediario = solicitud.ClienteIntermediario.RazonSocial;
                        solRep.CuitIntermediario = solicitud.ClienteIntermediario.Cuit;
                    }

                    if (solicitud.ClienteRemitenteComercial != null)
                    {
                        solRep.NombreRemitenteComercial = solicitud.ClienteRemitenteComercial.RazonSocial;
                        solRep.CuitRemitenteComercial = solicitud.ClienteRemitenteComercial.Cuit;
                    }

                    if (solicitud.ClienteCorredor != null)
                    {
                        solRep.NombreCorredor = solicitud.ClienteCorredor.RazonSocial;
                        solRep.CuitCorredor = solicitud.ClienteCorredor.Cuit;
                    }

                    if (solicitud.ClienteEntregador != null)
                    {
                        solRep.NombreEntregador = solicitud.ClienteEntregador.RazonSocial;

                        solRep.CuitEntregador = solicitud.ClienteEntregador.Cuit;
                    }

                    if (solicitud.ClienteDestinatario != null)
                    {
                        solRep.NombreDestinatario = solicitud.ClienteDestinatario.RazonSocial;
                        solRep.CuitDestinatario = solicitud.ClienteDestinatario.Cuit;
                    }

                    if (solicitud.ClienteDestino != null)
                    {
                        solRep.NombreDestino = solicitud.ClienteDestino.RazonSocial;
                        solRep.CuitDestino = solicitud.ClienteDestino.Cuit;
                    }

                    String NombreTransportista = string.Empty;
                    String CuitTransportista = string.Empty;

                    if (solicitud.ProveedorTransportista != null)
                    {
                        NombreTransportista = solicitud.ProveedorTransportista.Nombre;

                        if (String.IsNullOrEmpty(NombreTransportista) && solicitud.ChoferTransportista != null)
                            NombreTransportista = solicitud.ChoferTransportista.Nombre;

                        CuitTransportista = solicitud.ProveedorTransportista.NumeroDocumento;

                        if (String.IsNullOrEmpty(CuitTransportista) && solicitud.ChoferTransportista != null)
                            CuitTransportista = solicitud.ChoferTransportista.Cuit;

                    }

                    solRep.NombreTransportista = NombreTransportista;
                    solRep.CuitTransportista = CuitTransportista;

                    if (solicitud.Chofer != null)
                    {
                        solRep.NombreChofer = string.Format("{0}, {1}", solicitud.Chofer.Apellido, solicitud.Chofer.Nombre);
                        solRep.CuitChofer = solicitud.Chofer.Cuit;
                    }

                    //datos de los granos.
                    if (solicitud.Grano != null)
                    {
                        if (solicitud.Grano.EspecieAfip != null)
                            solRep.GranoEspecie = solicitud.Grano.EspecieAfip.Descripcion;
                        if (solicitud.Grano.TipoGrano != null)
                            solRep.TipoGranoEspecie = solicitud.Grano.TipoGrano.Descripcion;
                        if (solicitud.Grano.CosechaAfip != null)
                            solRep.Cosecha = solicitud.Grano.CosechaAfip.Descripcion;

                        if (solicitud.IdGrupoEmpresa != App.ID_GRUPO_CRESUD)
                        {
                            solRep.GranoEspecie = solicitud.Grano.Descripcion;
                        }
                    }

                    solRep.NroContrato = Tools.Value2<string>(solicitud.NumeroContrato, string.Empty);

                    if (solicitud.CargaPesadaDestino)
                    {
                        solRep.CargaPesadaDestino = "X";
                        solRep.KgrsEstimados = solicitud.KilogramosEstimados.ToString();
                    }
                    else
                    {
                        solRep.PesoBruto = Tools.Value2<string>(solicitud.PesoBruto, string.Empty);
                        solRep.PesoTara = Tools.Value2<string>(solicitud.PesoTara, string.Empty);
                        solRep.PesoNeto = Tools.Value2<string>(solicitud.PesoTara, string.Empty);
                    }

                    if (solicitud.ConformeCondicional.Value == Enums.ConformeCondicional.Conforme)
                        solRep.Conforme = "X";

                    if (solicitud.ConformeCondicional.Value == Enums.ConformeCondicional.Condicional)
                        solRep.Condicional = "X";

                    solRep.Observaciones = Tools.Value2<string>(solicitud.Observaciones, string.Empty);

                    // Procedencia de la mercaderia.
                    if (solicitud.IdEstablecimientoProcedencia != null)
                    {
                        solRep.NombreEstablecimientoProcedencia = solicitud.IdEstablecimientoProcedencia.Descripcion;
                        solRep.DireccionEstablecimientoProcedencia = solicitud.IdEstablecimientoProcedencia.Direccion;

                        if (solicitud.IdEstablecimientoProcedencia.Localidad != null)
                            solRep.LocalidadEstablecimientoProcedencia = solicitud.IdEstablecimientoProcedencia.Localidad.Descripcion;

                        if (solicitud.IdEstablecimientoProcedencia.Provincia != null)
                            solRep.ProvinciaEstablecimientoProcedencia = solicitud.IdEstablecimientoProcedencia.Provincia.Descripcion;
                    }

                    // Destino de la mercaderia.
                    if (solicitud.IdEstablecimientoDestino != null)
                    {
                        solRep.DireccionEstablecimientoDestino = solicitud.IdEstablecimientoDestino.Direccion;

                        if (solicitud.IdEstablecimientoDestino.Localidad != null)
                            solRep.LocalidadEstablecimientoDestino = solicitud.IdEstablecimientoDestino.Localidad.Descripcion;

                        if (solicitud.IdEstablecimientoDestino.Provincia != null)
                            solRep.ProvinciaEstablecimientoDestino = solicitud.IdEstablecimientoDestino.Provincia.Descripcion;
                    }

                    // Datos del transportista
                    solRep.Camion = solicitud.PatenteCamion;
                    solRep.Acoplado = solicitud.PatenteAcoplado;
                    solRep.KmRecorrer = Tools.Value2<string>(solicitud.KmRecorridos, string.Empty);

                    if (solicitud.EstadoFlete.HasValue && solicitud.EstadoFlete.Value == Enums.EstadoFlete.FletePagado)
                        solRep.FletePag = "X";

                    if (solicitud.EstadoFlete.HasValue && solicitud.EstadoFlete.Value == Enums.EstadoFlete.FleteAPagar)
                        solRep.FleteAPag = "X";

                    solRep.TarifaReferencia = Tools.Value2<string>(solicitud.TarifaReferencia, string.Empty);
                    solRep.TarifaReal = Tools.Value2<string>(solicitud.TarifaReal, string.Empty);

                    if (solicitud.ClientePagadorDelFlete != null)
                        solRep.NombrePagadorDelFlete = solicitud.ClientePagadorDelFlete.RazonSocial;

                }

            }

            return solRep;
        }

    }

    public class SolicitudParaImagen
    {
        private string numeroCartaDePorte = string.Empty;
        private string numeroCEE = string.Empty;
        private string numeroCTG = string.Empty;
        private string fechaCargaDia = string.Empty;
        private string fechaCargaMes = string.Empty;
        private string fechaCargaAnio = string.Empty;
        private string fechaVencimiento = string.Empty;
        private string nombreTitularCartaPorte = string.Empty;
        private string cuitTitularCartaPorte = string.Empty;
        private string nombreIntermediario = string.Empty;
        private string cuitIntermediario = string.Empty;
        private string nombreRemitenteComercial = string.Empty;
        private string cuitRemitenteComercial = string.Empty;
        private string nombreCorredor = string.Empty;
        private string cuitCorredor = string.Empty;
        private string nombreEntregador = string.Empty;
        private string cuitEntregador = string.Empty;
        private string nombreDestinatario = string.Empty;
        private string cuitDestinatario = string.Empty;
        private string nombreDestino = string.Empty;
        private string cuitDestino = string.Empty;
        private string nombreTransportista = string.Empty;
        private string cuitTransportista = string.Empty;
        private string nombreChofer = string.Empty;
        private string cuitChofer = string.Empty;
        private string granoEspecie = string.Empty;
        private string tipoGranoEspecie = string.Empty;
        private string cosecha = string.Empty;
        private string nroContrato = string.Empty;
        private string cargaPesadaDestino = string.Empty;
        private string conforme = string.Empty;
        private string condicional = string.Empty;
        private string kgrsEstimados = string.Empty;
        private string pesoBruto = string.Empty;
        private string pesoTara = string.Empty;
        private string pesoNeto = string.Empty;
        private string observaciones = string.Empty;
        private string nombreEstablecimientoProcedencia = string.Empty;
        private string direccionEstablecimientoProcedencia = string.Empty;
        private string localidadEstablecimientoProcedencia = string.Empty;
        private string provinciaEstablecimientoProcedencia = string.Empty;
        private string direccionEstablecimientoDestino = string.Empty;
        private string localidadEstablecimientoDestino = string.Empty;
        private string provinciaEstablecimientoDestino = string.Empty;
        private string nombrePagadorDelFlete = string.Empty;
        private string camion = string.Empty;
        private string acoplado = string.Empty;
        private string kmRecorrer = string.Empty;
        private string fletePag = string.Empty;
        private string fleteAPag = string.Empty;
        private string tarifaReferencia = string.Empty;
        private string tarifaReal = string.Empty;

        public string NumeroCartaDePorte
        {
            get { return numeroCartaDePorte; }
            set { numeroCartaDePorte = value; }
        }

        public string NumeroCEE
        {
            get { return numeroCEE; }
            set { numeroCEE = value; }
        }

        public string NumeroCTG
        {
            get { return numeroCTG; }
            set { numeroCTG = value; }
        }

        public string FechaCargaDia
        {
            get { return fechaCargaDia; }
            set { fechaCargaDia = value; }
        }

        public string FechaCargaMes
        {
            get { return fechaCargaMes; }
            set { fechaCargaMes = value; }
        }

        public string FechaCargaAnio
        {
            get { return fechaCargaAnio; }
            set { fechaCargaAnio = value; }
        }


        public string FechaVencimiento
        {
            get { return fechaVencimiento; }
            set { fechaVencimiento = value; }
        }

        public string NombreTitularCartaPorte
        {
            get { return nombreTitularCartaPorte; }
            set { nombreTitularCartaPorte = value; }
        }

        public string CuitTitularCartaPorte
        {
            get { return cuitTitularCartaPorte; }
            set { cuitTitularCartaPorte = value; }
        }

        public string NombreIntermediario
        {
            get { return nombreIntermediario; }
            set { nombreIntermediario = value; }
        }

        public string CuitIntermediario
        {
            get { return cuitIntermediario; }
            set { cuitIntermediario = value; }
        }

        public string NombreRemitenteComercial
        {
            get { return nombreRemitenteComercial; }
            set { nombreRemitenteComercial = value; }
        }

        public string CuitRemitenteComercial
        {
            get { return cuitRemitenteComercial; }
            set { cuitRemitenteComercial = value; }
        }

        public string NombreCorredor
        {
            get { return nombreCorredor; }
            set { nombreCorredor = value; }
        }

        public string CuitCorredor
        {
            get { return cuitCorredor; }
            set { cuitCorredor = value; }
        }

        public string NombreEntregador
        {
            get { return nombreEntregador; }
            set { nombreEntregador = value; }
        }

        public string CuitEntregador
        {
            get { return cuitEntregador; }
            set { cuitEntregador = value; }
        }

        public string NombreDestinatario
        {
            get { return nombreDestinatario; }
            set { nombreDestinatario = value; }
        }

        public string CuitDestinatario
        {
            get { return cuitDestinatario; }
            set { cuitDestinatario = value; }
        }

        public string NombreDestino
        {
            get { return nombreDestino; }
            set { nombreDestino = value; }
        }

        public string CuitDestino
        {
            get { return cuitDestino; }
            set { cuitDestino = value; }
        }

        public string NombreTransportista
        {
            get { return nombreTransportista; }
            set { nombreTransportista = value; }
        }

        public string CuitTransportista
        {
            get { return cuitTransportista; }
            set { cuitTransportista = value; }
        }

        public string NombreChofer
        {
            get { return nombreChofer; }
            set { nombreChofer = value; }
        }

        public string CuitChofer
        {
            get { return cuitChofer; }
            set { cuitChofer = value; }
        }


        public string GranoEspecie
        {
            get { return granoEspecie; }
            set { granoEspecie = value; }
        }

        public string TipoGranoEspecie
        {
            get { return tipoGranoEspecie; }
            set { tipoGranoEspecie = value; }
        }

        public string Cosecha
        {
            get { return cosecha; }
            set { cosecha = value; }
        }

        public string NroContrato
        {
            get { return nroContrato; }
            set { nroContrato = value; }
        }


        public string CargaPesadaDestino
        {
            get { return cargaPesadaDestino; }
            set { cargaPesadaDestino = value; }
        }

        public string Conforme
        {
            get { return conforme; }
            set { conforme = value; }
        }

        public string Condicional
        {
            get { return condicional; }
            set { condicional = value; }
        }

        public string KgrsEstimados
        {
            get { return kgrsEstimados; }
            set { kgrsEstimados = value; }
        }


        public string PesoBruto
        {
            get { return pesoBruto; }
            set { pesoBruto = value; }
        }

        public string PesoTara
        {
            get { return pesoTara; }
            set { pesoTara = value; }
        }

        public string PesoNeto
        {
            get { return pesoNeto; }
            set { pesoNeto = value; }
        }

        public string Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }


        public string NombreEstablecimientoProcedencia
        {
            get { return nombreEstablecimientoProcedencia; }
            set { nombreEstablecimientoProcedencia = value; }
        }

        public string DireccionEstablecimientoProcedencia
        {
            get { return direccionEstablecimientoProcedencia; }
            set { direccionEstablecimientoProcedencia = value; }
        }

        public string LocalidadEstablecimientoProcedencia
        {
            get { return localidadEstablecimientoProcedencia; }
            set { localidadEstablecimientoProcedencia = value; }
        }

        public string ProvinciaEstablecimientoProcedencia
        {
            get { return provinciaEstablecimientoProcedencia; }
            set { provinciaEstablecimientoProcedencia = value; }
        }


        public string DireccionEstablecimientoDestino
        {
            get { return direccionEstablecimientoDestino; }
            set { direccionEstablecimientoDestino = value; }
        }

        public string LocalidadEstablecimientoDestino
        {
            get { return localidadEstablecimientoDestino; }
            set { localidadEstablecimientoDestino = value; }
        }

        public string ProvinciaEstablecimientoDestino
        {
            get { return provinciaEstablecimientoDestino; }
            set { provinciaEstablecimientoDestino = value; }
        }


        public string NombrePagadorDelFlete
        {
            get { return nombrePagadorDelFlete; }
            set { nombrePagadorDelFlete = value; }
        }


        public string Camion
        {
            get { return camion; }
            set { camion = value; }
        }

        public string Acoplado
        {
            get { return acoplado; }
            set { acoplado = value; }
        }

        public string KmRecorrer
        {
            get { return kmRecorrer; }
            set { kmRecorrer = value; }
        }

        public string FletePag
        {
            get { return fletePag; }
            set { fletePag = value; }
        }

        public string FleteAPag
        {
            get { return fleteAPag; }
            set { fleteAPag = value; }
        }


        public string TarifaReferencia
        {
            get { return tarifaReferencia; }
            set { tarifaReferencia = value; }
        }

        public string TarifaReal
        {
            get { return tarifaReal; }
            set { tarifaReal = value; }
        }

        public int IdGrupoEmpresa { get; set; }
    }
}

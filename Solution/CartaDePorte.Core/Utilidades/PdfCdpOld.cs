using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using System.Security.Principal;
using System.Configuration;

using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.text.pdf.parser;

using CartaDePorte.Core.Domain;


namespace CartaDePorte.Core.Utilidades
{
    public class PdfCdpOld
    {


        #region Constructor
        private static PdfCdpOld instance;
        
        private string _rutaOriginalCartaDePorte;
        private string _rutaOriginalCartaDePorteGenerica;

        public PdfCdpOld() {
            this._rutaOriginalCartaDePorte = ConfigurationManager.AppSettings["RutaOriginalCartaDePorte"];
            this._rutaOriginalCartaDePorteGenerica = ConfigurationManager.AppSettings["RutaOriginalCartaDePorteGenerica"];
            
        }

        public static PdfCdpOld Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PdfCdpOld();
                }
                return instance;
            }
        }
        #endregion

        #region GenerarPDF
        public void GenerarPDF(Solicitud solicitud, out string mensaje)
        {
            mensaje = string.Empty;


            if (App.ImpersonationValidateUser()) 
            {   
                DividirCartaDePorte(solicitud, out mensaje);
                if (!String.IsNullOrEmpty(solicitud.Ctg) || App.Usuario.IdGrupoEmpresa != App.ID_GRUPO_CRESUD)
                {
                    AgregarTextos(solicitud);
                    StampFecha(solicitud);
                }
                AppendearCartaDePorte(solicitud);
                BorrarTemporales(solicitud);

                App.ImpersonationUndo();
            }


        }
        public void DividirCartaDePorte(Solicitud solicitud, out string mensaje)
        {
            

            var solRep = CargarSolicitud(solicitud);

            var path = string.Format(@"{0}\Cargas\{1}", this._rutaOriginalCartaDePorte, solicitud.IdSolicitud);
            var file = string.Format(@"{0}\Cargas\{1}\{2}.pdf", this._rutaOriginalCartaDePorte, solicitud.IdSolicitud, solRep.NumeroCartaDePorte);

            var oldFile = string.Format(@"{0}\Cargas\{1}.pdf", this._rutaOriginalCartaDePorte, solRep.NumeroCartaDePorte);
            if (App.Usuario.IdGrupoEmpresa != App.ID_GRUPO_CRESUD)
            {
                //oldFile = string.Format(@"{0}\Cargas\generico.pdf", this._rutaOriginalCartaDePorte);
                oldFile = _rutaOriginalCartaDePorteGenerica;
            }

            Directory.CreateDirectory(path);

            int currentPage = 1;
            int pageCount = 0;
            mensaje = string.Empty;

            var encoding = new System.Text.UTF8Encoding();
            var reader = new iTextSharp.text.pdf.PdfReader(oldFile);
            reader.RemoveUnusedObjects();
            pageCount = reader.NumberOfPages;

            if (pageCount != 4)
            {
                mensaje = "La carta de porte no contiene los 4 formularios esperados";
                return;
            }


            string ext = Path.GetExtension(oldFile);
            for (int i = 1; i <= pageCount; i++)
            {
                var outfile = string.Format(@"{0}\Cargas\{1}\{2}_{3}.pdf", this._rutaOriginalCartaDePorte, solicitud.IdSolicitud, solRep.NumeroCartaDePorte, i);

                var reader1 = new iTextSharp.text.pdf.PdfReader(oldFile);
                reader1.RemoveUnusedObjects();
                var doc = new iTextSharp.text.Document(reader.GetPageSizeWithRotation(currentPage));
                var pdfCpy = new iTextSharp.text.pdf.PdfCopy(doc, new FileStream(outfile, System.IO.FileMode.Create));
                doc.Open();
                for (int j = 1; j <= 1; j++)
                {
                    var page = pdfCpy.GetImportedPage(reader1, currentPage);
                    pdfCpy.SetFullCompression();
                    pdfCpy.AddPage(page);
                    currentPage++;
                }

                doc.Close();
                pdfCpy.Close();
                reader1.Close();
                reader.Close();

            }

        }
        public void AgregarTextos(Solicitud solicitud)
        {

            var solRep = CargarSolicitud(solicitud);

            for (int i = 1; i <= 4; i++)
            {
                var oldFile = string.Format(@"{0}\Cargas\{1}\{2}_{3}.pdf", this._rutaOriginalCartaDePorte, solicitud.IdSolicitud, solRep.NumeroCartaDePorte, i);
                var reader = new PdfReader(oldFile);
                var size = reader.GetPageSizeWithRotation(1);
                var document = new Document(size);

                var newFile = string.Format(@"{0}\Cargas\{1}\{2}_{3}a.pdf", this._rutaOriginalCartaDePorte, solicitud.IdSolicitud, solRep.NumeroCartaDePorte, i);
                var fs = new FileStream(newFile, FileMode.Create, FileAccess.Write);
                var writer = PdfWriter.GetInstance(document, fs);
                document.Open();

                var cb = writer.DirectContent;

                var bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb.SetColorFill(BaseColor.BLACK);
                cb.SetFontAndSize(bf, 9);

                GenerarTextos(writer, bf, cb, solRep, i);

                var page = writer.GetImportedPage(reader, 1);
                cb.AddTemplate(page, 0, 0);

                document.Close();
                fs.Close();
                writer.Close();
                reader.Close();
            }

        }
        private void StampFecha(Solicitud solicitud)
        {

            var solRep = CargarSolicitud(solicitud);
            var stringFont = new System.Drawing.Font("Helvetica", 40, FontStyle.Bold);

            DrawText(solRep, solicitud, solRep.FechaCargaDia + "/" + solRep.FechaCargaMes + "/" + solRep.FechaCargaAnio, stringFont, Color.Black, Color.White);


            for (int i = 1; i <= 4; i++)
            {

                using (Stream inputPdfStream = new FileStream(this._rutaOriginalCartaDePorte + "Cargas\\" + solicitud.IdSolicitud.ToString() + "\\" + solRep.NumeroCartaDePorte + "_" + i.ToString() + "a.pdf", FileMode.Open, FileAccess.Read, FileShare.Read))
                using (Stream inputImageStream = new FileStream(this._rutaOriginalCartaDePorte + "Cargas\\" + solicitud.IdSolicitud.ToString() + "\\" + solRep.NumeroCartaDePorte + ".png", FileMode.Open, FileAccess.Read, FileShare.Read))
                using (Stream outputPdfStream = new FileStream(this._rutaOriginalCartaDePorte + "Cargas\\" + solicitud.IdSolicitud.ToString() + "\\" + solRep.NumeroCartaDePorte + "_" + i.ToString() + "as.pdf", FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    var reader = new PdfReader(inputPdfStream);
                    var stamper = new PdfStamper(reader, outputPdfStream);
                    var pdfContentByte = stamper.GetOverContent(1);

                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(inputImageStream);
                    image.ScaleAbsolute(60, 12.1f);
                    image.SetAbsolutePosition(504, 758);
                    pdfContentByte.AddImage(image);
                    stamper.Close();
                }

            }

        }
        public void AppendearCartaDePorte(Solicitud solicitud)
        {
            using (var output = new MemoryStream())
            {
                var document = new Document();
                var writer = new PdfCopy(document, output);
                document.Open();

                string file = this._rutaOriginalCartaDePorte + "Cargas\\" + solicitud.IdSolicitud.ToString() + "\\" + solicitud.NumeroCartaDePorte + ".pdf";
                string file1 = this._rutaOriginalCartaDePorte + "Cargas\\" + solicitud.IdSolicitud.ToString() + "\\" + solicitud.NumeroCartaDePorte + "_1as.pdf";
                string file2 = this._rutaOriginalCartaDePorte + "Cargas\\" + solicitud.IdSolicitud.ToString() + "\\" + solicitud.NumeroCartaDePorte + "_2as.pdf";
                string file3 = this._rutaOriginalCartaDePorte + "Cargas\\" + solicitud.IdSolicitud.ToString() + "\\" + solicitud.NumeroCartaDePorte + "_3as.pdf";
                string file4 = this._rutaOriginalCartaDePorte + "Cargas\\" + solicitud.IdSolicitud.ToString() + "\\" + solicitud.NumeroCartaDePorte + "_4as.pdf";

                //if (String.IsNullOrEmpty(solicitud.Ctg))
                //{
                //    file1 = this._rutaOriginalCartaDePorte + "Cargas\\" + solicitud.IdSolicitud.ToString() + "\\" + solicitud.NumeroCartaDePorte + "_1.pdf";
                //    file2 = this._rutaOriginalCartaDePorte + "Cargas\\" + solicitud.IdSolicitud.ToString() + "\\" + solicitud.NumeroCartaDePorte + "_2.pdf";
                //    file3 = this._rutaOriginalCartaDePorte + "Cargas\\" + solicitud.IdSolicitud.ToString() + "\\" + solicitud.NumeroCartaDePorte + "_3.pdf";
                //    file4 = this._rutaOriginalCartaDePorte + "Cargas\\" + solicitud.IdSolicitud.ToString() + "\\" + solicitud.NumeroCartaDePorte + "_4.pdf";
                //}

                foreach (var filePdf in new[] { file1, file2, file3, file4 })
                {
                    var reader = new PdfReader(filePdf);
                    int n = reader.NumberOfPages;
                    PdfImportedPage page;
                    for (int p = 1; p <= n; p++)
                    {
                        page = writer.GetImportedPage(reader, p);
                        writer.AddPage(page);
                    }
                    reader.Close();
                }
                document.Close();
                File.WriteAllBytes(file, output.ToArray());
                writer.Close();
            }

        }
        private void GenerarTextos(PdfWriter writer, BaseFont bf, PdfContentByte cb, SolicitudParaImagen solicitud, int nroForm)
        {


            //Imprimo CTG
            AddText(cb, bf, false, 9, solicitud.NumeroCTG, 250, 753);

            //DATOS DE INTERVINIENTES EN EL TRASLADO DE GRANOS
            int ColNombre = 156;
            int ColCuit = 470;

            //Intermediario
            AddText(cb, bf, true, 7, solicitud.NombreIntermediario, ColNombre, 665);
            AddText(cb, bf, true, 10, solicitud.CuitIntermediario, ColCuit, 665);

            //Remitente comercial
            AddText(cb, bf, true, 7, solicitud.NombreRemitenteComercial, ColNombre, 645);
            AddText(cb, bf, true, 10, solicitud.CuitRemitenteComercial, ColCuit, 645);

            //Corredor
            AddText(cb, bf, true, 7, solicitud.NombreCorredor, ColNombre, 625);
            AddText(cb, bf, true, 10, solicitud.CuitCorredor, ColCuit, 625);

            //Representante/Entregador
            AddText(cb, bf, true, 7, solicitud.NombreEntregador, ColNombre, 605);
            AddText(cb, bf, true, 10, solicitud.CuitEntregador, ColCuit, 605);

            //Destinatario
            AddText(cb, bf, true, 7, solicitud.NombreDestinatario, ColNombre, 585);
            AddText(cb, bf, true, 10, solicitud.CuitDestinatario, ColCuit, 585);

            //Destino
            AddText(cb, bf, true, 7, solicitud.NombreDestino, ColNombre, 565);
            AddText(cb, bf, true, 10, solicitud.CuitDestino, ColCuit, 565);

            //Transportista
            AddText(cb, bf, true, 7, solicitud.NombreTransportista, ColNombre, 545);
            AddText(cb, bf, true, 10, solicitud.CuitTransportista, ColCuit, 545);

            //Chofer
            AddText(cb, bf, true, 7, solicitud.NombreChofer, ColNombre, 525);
            AddText(cb, bf, true, 10, solicitud.CuitChofer, ColCuit, 525);

            //Datos de los granos / Especies Transportados
            AddText(cb, bf, false, 8, solicitud.GranoEspecie, 100, 488);
            AddText(cb, bf, false, 8, solicitud.TipoGranoEspecie, 265, 488);
            AddText(cb, bf, false, 8, solicitud.NroContrato, 460, 488);
            AddText(cb, bf, false, 8, solicitud.Cosecha, 460, 505);
            AddText(cb, bf, true, 10, solicitud.CargaPesadaDestino, 130, 461);
            AddText(cb, bf, false, 8, solicitud.KgrsEstimados, 100, 437);

            AddText(cb, bf, true, 10, solicitud.Conforme, 249, 453);
            AddText(cb, bf, true, 10, solicitud.Condicional, 249, 436);
            AddText(cb, bf, false, 10, solicitud.PesoBruto, 343, 470);
            AddText(cb, bf, false, 10, solicitud.PesoTara, 343, 453);
            AddText(cb, bf, false, 10, solicitud.PesoNeto, 343, 436);

            if (solicitud.Observaciones != null)
            {
                GenerarTextoObservaciones(cb, bf, false, 8, solicitud.Observaciones, 400, 458);
            }

            //PROCEDENCIA DE LA MERCADERÍA
            AddText(cb, bf, false, 9, solicitud.DireccionEstablecimientoProcedencia, 78, 393);
            AddText(cb, bf, false, 9, solicitud.NombreEstablecimientoProcedencia, 415, 420);
            AddText(cb, bf, false, 9, solicitud.LocalidadEstablecimientoProcedencia, 415, 403);
            AddText(cb, bf, false, 9, solicitud.ProvinciaEstablecimientoProcedencia, 415, 385);

            //LUGAR DE DESTINO DE LOS GRANOS
            AddText(cb, bf, false, 9, solicitud.DireccionEstablecimientoDestino, 78, 340);
            AddText(cb, bf, false, 9, solicitud.LocalidadEstablecimientoDestino, 415, 349);
            AddText(cb, bf, false, 9, solicitud.ProvinciaEstablecimientoDestino, 415, 331);

            //DATOS DEL TRANSPORTE
            AddText(cb, bf, false, 9, solicitud.Camion, 95, 295);
            AddText(cb, bf, false, 9, solicitud.Acoplado, 95, 278);
            AddText(cb, bf, false, 9, solicitud.KmRecorrer, 95, 261);
            AddText(cb, bf, false, 9, solicitud.FletePag, 200, 295);//Flete Pag.            
            AddText(cb, bf, false, 9, solicitud.FleteAPag, 280, 295);//Flete a Pag            
            AddText(cb, bf, false, 9, solicitud.TarifaReferencia, 243, 278);//Tarifa de Referencia            
            AddText(cb, bf, false, 9, solicitud.TarifaReal, 243, 261);//Tarifa

            //Pagador del Flete
            if (nroForm != 2)
                AddText(cb, bf, false, 9, solicitud.NombrePagadorDelFlete, 350, 311);
            else
                AddText(cb, bf, false, 9, solicitud.NombrePagadorDelFlete, 252, 311);

        }
        private void GenerarTextoObservaciones(PdfContentByte cb, BaseFont font, bool BOLD, int size, string text, int x, int y)
        {

            string texto = text;

            if (text.Length > 45)
            {
                AddText(cb, font, BOLD, size, texto.Substring(0, 45), 400, 458);
                if (text.Length > 90)
                {
                    AddText(cb, font, BOLD, size, texto.Substring(45, 45), 400, 448);
                    if (text.Length > 135)
                    {
                        AddText(cb, font, BOLD, size, texto.Substring(90, 45), 400, 438);
                    }
                    else
                    {
                        AddText(cb, font, BOLD, size, texto.Substring(90, text.Length - 90), 400, 438);
                    }
                }
                else
                {
                    AddText(cb, font, BOLD, size, texto.Substring(45, text.Length - 45), 400, 448);
                }

            }
            else
            {
                AddText(cb, font, BOLD, size, text, 400, 458);
            }

        }
        private void AddText(PdfContentByte cb, BaseFont font, bool BOLD, int size, string text, int x, int y)
        {
            if (!String.IsNullOrEmpty(text))
            {
                if (BOLD)
                    font = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                else
                    font = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

                cb.BeginText();
                cb.SetFontAndSize(font, size);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, text, x, y, 0);
                cb.EndText();

            }
        }
        private System.Drawing.Image DrawText(SolicitudParaImagen solRep, Solicitud solicitud, String text, System.Drawing.Font font, Color textColor, Color backColor)
        {

            Bitmap bm = new Bitmap(1, 1);
            System.Drawing.Image img = bm;
            Graphics drawing = Graphics.FromImage(img);

            SizeF textSize = drawing.MeasureString(text, font);

            img.Dispose();
            drawing.Dispose();

            img = new Bitmap((int)textSize.Width, (int)textSize.Height);
            drawing = Graphics.FromImage(img);
            drawing.Clear(backColor);
            Brush textBrush = new SolidBrush(textColor);
            drawing.DrawString(text, font, textBrush, 0, 0);
            drawing.Save();

            img.Save(this._rutaOriginalCartaDePorte + "Cargas\\" + solicitud.IdSolicitud.ToString() + "\\" + solicitud.NumeroCartaDePorte + ".png", ImageFormat.Png);

            textBrush.Dispose();
            drawing.Dispose();

            return img;

        }
        private SolicitudParaImagen CargarSolicitud(Solicitud solicitud)
        {

            SolicitudParaImagen solRep = new SolicitudParaImagen();
            if (solicitud != null)
            {

                solRep.NumeroCartaDePorte = solicitud.NumeroCartaDePorte;
                solRep.NumeroCEE = solicitud.Cee;
                solRep.NumeroCTG = solicitud.Ctg;

                if (!String.IsNullOrEmpty(solRep.NumeroCTG) || App.Usuario.IdGrupoEmpresa != App.ID_GRUPO_CRESUD)
                {
                    // Cabecera
                    solRep.NumeroCTG = solicitud.Ctg;
                    solRep.FechaCargaDia = solicitud.FechaDeCarga.Value.ToString("dd");
                    solRep.FechaCargaMes = solicitud.FechaDeCarga.Value.ToString("MM");
                    solRep.FechaCargaAnio = solicitud.FechaDeCarga.Value.ToString("yyyy");
                    solRep.FechaVencimiento = (solicitud.FechaDeVencimiento.HasValue) ? solicitud.FechaDeVencimiento.Value.ToString("dd/MM/yyyy") : string.Empty;

                    // Intervinientes del traslado
                    solRep.NombreTitularCartaPorte = (solicitud.ProveedorTitularCartaDePorte != null) ? solicitud.ProveedorTitularCartaDePorte.Nombre : string.Empty;
                    solRep.CuitTitularCartaPorte = (solicitud.ProveedorTitularCartaDePorte != null) ? solicitud.ProveedorTitularCartaDePorte.NumeroDocumento : string.Empty;
                    solRep.NombreIntermediario = (solicitud.ClienteIntermediario != null) ? solicitud.ClienteIntermediario.RazonSocial : string.Empty;
                    solRep.CuitIntermediario = (solicitud.ClienteIntermediario != null) ? solicitud.ClienteIntermediario.Cuit : string.Empty;
                    solRep.NombreRemitenteComercial = (solicitud.ClienteRemitenteComercial != null) ? solicitud.ClienteRemitenteComercial.RazonSocial : string.Empty;
                    solRep.CuitRemitenteComercial = (solicitud.ClienteRemitenteComercial != null) ? solicitud.ClienteRemitenteComercial.Cuit : string.Empty;
                    solRep.NombreCorredor = (solicitud.ClienteCorredor != null) ? solicitud.ClienteCorredor.RazonSocial : string.Empty;
                    solRep.CuitCorredor = (solicitud.ClienteCorredor != null) ? solicitud.ClienteCorredor.Cuit : string.Empty;
                    solRep.NombreEntregador = (solicitud.ClienteEntregador != null) ? solicitud.ClienteEntregador.RazonSocial : string.Empty;
                    solRep.CuitEntregador = (solicitud.ClienteEntregador != null) ? solicitud.ClienteEntregador.Cuit : string.Empty;
                    solRep.NombreDestinatario = (solicitud.ClienteDestinatario != null) ? solicitud.ClienteDestinatario.RazonSocial : string.Empty;
                    solRep.CuitDestinatario = (solicitud.ClienteDestinatario != null) ? solicitud.ClienteDestinatario.Cuit : string.Empty;
                    solRep.NombreDestino = (solicitud.ClienteDestino != null) ? solicitud.ClienteDestino.RazonSocial : string.Empty;
                    solRep.CuitDestino = (solicitud.ClienteDestino != null) ? solicitud.ClienteDestino.Cuit : string.Empty;

                    String NombreTransportista = string.Empty;
                    String CuitTransportista = string.Empty;

                    NombreTransportista = (solicitud.ProveedorTransportista != null) ? solicitud.ProveedorTransportista.Nombre : string.Empty;
                    if (String.IsNullOrEmpty(NombreTransportista))
                        NombreTransportista = (solicitud.ChoferTransportista != null) ? solicitud.ChoferTransportista.Nombre : string.Empty;

                    CuitTransportista = (solicitud.ProveedorTransportista != null) ? solicitud.ProveedorTransportista.NumeroDocumento : string.Empty;
                    if (String.IsNullOrEmpty(CuitTransportista))
                        CuitTransportista = (solicitud.ChoferTransportista != null) ? solicitud.ChoferTransportista.Cuit : string.Empty;

                    solRep.NombreTransportista = NombreTransportista;
                    solRep.CuitTransportista = CuitTransportista;

                    solRep.NombreChofer = (solicitud.Chofer != null) ? (solicitud.Chofer.Apellido + ", " + solicitud.Chofer.Nombre) : string.Empty;
                    solRep.CuitChofer = (solicitud.Chofer != null) ? solicitud.Chofer.Cuit : string.Empty;

                    //datos de los granos.
                    solRep.GranoEspecie = (solicitud.Grano != null) ? solicitud.Grano.EspecieAfip.Descripcion : string.Empty;
                    solRep.TipoGranoEspecie = (solicitud.Grano != null) ? solicitud.Grano.TipoGrano.Descripcion : string.Empty;
                    solRep.Cosecha = (solicitud.Grano != null) ? solicitud.Grano.CosechaAfip.Descripcion : string.Empty;
                    solRep.NroContrato = (solicitud.NumeroContrato.HasValue) ? solicitud.NumeroContrato.Value.ToString() : string.Empty;
                    if (solicitud.CargaPesadaDestino)
                    {
                        solRep.CargaPesadaDestino = "X";
                        solRep.KgrsEstimados = solicitud.KilogramosEstimados.ToString();
                    }
                    else
                    {
                        solRep.PesoBruto = (solicitud.PesoBruto.HasValue) ? solicitud.PesoBruto.Value.ToString() : string.Empty;
                        solRep.PesoTara = (solicitud.PesoTara.HasValue) ? solicitud.PesoTara.Value.ToString() : string.Empty;
                        solRep.PesoNeto = (solicitud.PesoNeto.HasValue) ? solicitud.PesoNeto.Value.ToString() : string.Empty;
                    }

                    if (solicitud.ConformeCondicional.Value == Enums.ConformeCondicional.Conforme)
                        solRep.Conforme = "X";

                    if (solicitud.ConformeCondicional.Value == Enums.ConformeCondicional.Condicional)
                        solRep.Condicional = "X";

                    solRep.Observaciones = (!String.IsNullOrEmpty(solicitud.Observaciones)) ? solicitud.Observaciones : string.Empty;

                    // Procedencia de la mercaderia.
                    solRep.NombreEstablecimientoProcedencia = (solicitud.IdEstablecimientoProcedencia != null) ? solicitud.IdEstablecimientoProcedencia.Descripcion : string.Empty;
                    solRep.DireccionEstablecimientoProcedencia = (solicitud.IdEstablecimientoProcedencia != null) ? solicitud.IdEstablecimientoProcedencia.Direccion : string.Empty;
                    solRep.LocalidadEstablecimientoProcedencia = (solicitud.IdEstablecimientoProcedencia != null) ? solicitud.IdEstablecimientoProcedencia.Localidad.Descripcion : string.Empty;
                    solRep.ProvinciaEstablecimientoProcedencia = (solicitud.IdEstablecimientoProcedencia != null) ? solicitud.IdEstablecimientoProcedencia.Provincia.Descripcion : string.Empty;

                    // Destino de la mercaderia.
                    solRep.DireccionEstablecimientoDestino = (solicitud.IdEstablecimientoDestino != null) ? solicitud.IdEstablecimientoDestino.Direccion : string.Empty;
                    solRep.LocalidadEstablecimientoDestino = (solicitud.IdEstablecimientoDestino != null) ? solicitud.IdEstablecimientoDestino.Localidad.Descripcion : string.Empty;
                    solRep.ProvinciaEstablecimientoDestino = (solicitud.IdEstablecimientoDestino != null) ? solicitud.IdEstablecimientoDestino.Provincia.Descripcion : string.Empty;

                    // Datos del transportista
                    solRep.Camion = solicitud.PatenteCamion;
                    solRep.Acoplado = solicitud.PatenteAcoplado;
                    solRep.KmRecorrer = (solicitud.KmRecorridos > 0) ? solicitud.KmRecorridos.ToString() : string.Empty;

                    if (solicitud.EstadoFlete.Value == Enums.EstadoFlete.FletePagado)
                        solRep.FletePag = "X";

                    if (solicitud.EstadoFlete.Value == Enums.EstadoFlete.FleteAPagar)
                        solRep.FleteAPag = "X";

                    solRep.TarifaReferencia = solicitud.TarifaReferencia.ToString();
                    solRep.TarifaReal = solicitud.TarifaReal.ToString();
                    solRep.NombrePagadorDelFlete = (solicitud.ClientePagadorDelFlete != null) ? solicitud.ClientePagadorDelFlete.RazonSocial : string.Empty;


                }



            }

            return solRep;
        }
        private void BorrarTemporales(Solicitud solicitud)
        {

            try
            {
                string file1 = this._rutaOriginalCartaDePorte + "Cargas\\" + solicitud.IdSolicitud.ToString() + "\\" + solicitud.NumeroCartaDePorte + "_1.pdf";
                string file2 = this._rutaOriginalCartaDePorte + "Cargas\\" + solicitud.IdSolicitud.ToString() + "\\" + solicitud.NumeroCartaDePorte + "_2.pdf";
                string file3 = this._rutaOriginalCartaDePorte + "Cargas\\" + solicitud.IdSolicitud.ToString() + "\\" + solicitud.NumeroCartaDePorte + "_3.pdf";
                string file4 = this._rutaOriginalCartaDePorte + "Cargas\\" + solicitud.IdSolicitud.ToString() + "\\" + solicitud.NumeroCartaDePorte + "_4.pdf";
                string file1a = this._rutaOriginalCartaDePorte + "Cargas\\" + solicitud.IdSolicitud.ToString() + "\\" + solicitud.NumeroCartaDePorte + "_1a.pdf";
                string file2a = this._rutaOriginalCartaDePorte + "Cargas\\" + solicitud.IdSolicitud.ToString() + "\\" + solicitud.NumeroCartaDePorte + "_2a.pdf";
                string file3a = this._rutaOriginalCartaDePorte + "Cargas\\" + solicitud.IdSolicitud.ToString() + "\\" + solicitud.NumeroCartaDePorte + "_3a.pdf";
                string file4a = this._rutaOriginalCartaDePorte + "Cargas\\" + solicitud.IdSolicitud.ToString() + "\\" + solicitud.NumeroCartaDePorte + "_4a.pdf";
                string file1as = this._rutaOriginalCartaDePorte + "Cargas\\" + solicitud.IdSolicitud.ToString() + "\\" + solicitud.NumeroCartaDePorte + "_1as.pdf";
                string file2as = this._rutaOriginalCartaDePorte + "Cargas\\" + solicitud.IdSolicitud.ToString() + "\\" + solicitud.NumeroCartaDePorte + "_2as.pdf";
                string file3as = this._rutaOriginalCartaDePorte + "Cargas\\" + solicitud.IdSolicitud.ToString() + "\\" + solicitud.NumeroCartaDePorte + "_3as.pdf";
                string file4as = this._rutaOriginalCartaDePorte + "Cargas\\" + solicitud.IdSolicitud.ToString() + "\\" + solicitud.NumeroCartaDePorte + "_4as.pdf";
                string fileImage = this._rutaOriginalCartaDePorte + "Cargas\\" + solicitud.IdSolicitud.ToString() + "\\" + solicitud.NumeroCartaDePorte + ".png";

                foreach (var filePdf in new[] { file1, file2, file3, file4, file1a, file2a, file3a, file4a, file1as, file2as, file3as, file4as, fileImage })
                {
                    System.IO.File.Delete(filePdf);
                }

            }
            catch { }

        }
        #endregion

        #region metodos de apoyo
        public void SplitePDF(string filepath, Int64 CartaDePorteInicial, int Cantidad, out string mensaje)
        {
            iTextSharp.text.pdf.PdfReader reader = null;
            int currentPage = 1;
            int pageCount = 0;
            Int64 NextCartaDePorteInicial = CartaDePorteInicial;
            mensaje = string.Empty;

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            reader = new iTextSharp.text.pdf.PdfReader(filepath);
            reader.RemoveUnusedObjects();
            pageCount = reader.NumberOfPages;

            if ((pageCount / 4) != Cantidad)
            {
                mensaje = "La cantidad de cartas de porte no coincide con el valor ingresado";
                return;
            }


            string ext = System.IO.Path.GetExtension(filepath);
            for (int i = 1; i <= pageCount / 4; i++)
            {
                iTextSharp.text.pdf.PdfReader reader1 = new iTextSharp.text.pdf.PdfReader(filepath);

                string outfile = filepath.Replace(System.IO.Path.GetFileName(filepath), string.Empty) + NextCartaDePorteInicial.ToString() + ext;
                reader1.RemoveUnusedObjects();
                iTextSharp.text.Document doc = new iTextSharp.text.Document(reader.GetPageSizeWithRotation(currentPage));
                iTextSharp.text.pdf.PdfCopy pdfCpy = new iTextSharp.text.pdf.PdfCopy(doc, new System.IO.FileStream(outfile, System.IO.FileMode.Create));
                doc.Open();
                for (int j = 1; j <= 4; j++)
                {
                    iTextSharp.text.pdf.PdfImportedPage page = pdfCpy.GetImportedPage(reader1, currentPage);
                    pdfCpy.SetFullCompression();
                    pdfCpy.AddPage(page);
                    currentPage += 1;
                }


                NextCartaDePorteInicial++;
                doc.Close();
                pdfCpy.Close();
                reader1.Close();
                reader.Close();

            }

        }
        #endregion


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using CartaDePorte.Core.Domain;
//using GhostscriptSharp;
//using GhostscriptSharp.Settings;

namespace CartaDePorte.Core.Utilidades
{
    public class Utils
    {
        private static Utils instance;
        public Utils() { }
        public static Utils Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Utils();
                }
                return instance;
            }
        }

        public String NormalizarMensajeErrorAfip(String texto)
        {
            String mensaje = texto;

            if(texto.Contains("The connection was closed unexpectedly"))
                mensaje = "<b>AFIP Temporalmente sin servicio. Por favor, Intente nuevamente mas tarde.</b> (" + texto + ")";

            if(texto.Contains("Service Temporarily Unavailable"))
                mensaje = "<b>AFIP Temporalmente sin servicio. Por favor, Intente nuevamente mas tarde.</b> (" + texto + ")";

            if (texto.Contains("JDBC Connection"))
                mensaje = "<b>AFIP Temporalmente sin servicio. Por favor, Intente nuevamente mas tarde.</b> (" + texto + ")";

            if (texto.Contains("character string buffer too small"))
                mensaje = "Verifique el formato de los datos de patentes. (" + texto + ")";

            return mensaje;
        
        }

        public Color getMensajeColor(String texto)
        {
            Color resultColor = Color.Black;

            if (texto.ToLower().Contains("sin servicio"))
                resultColor = Color.Red;

            if (texto.ToLower().Contains("errores afip"))
                resultColor = Color.Red;

            if (texto.ToLower().Contains("controles afip"))
                resultColor = Color.DarkOrange;

            return resultColor;
        }


        public Enums.EstadoEnvioSAP ValidarEstadoParaSAP(Solicitud solicitud)
        {
            if ((solicitud.ClienteIntermediario != null && solicitud.ClienteIntermediario.EsProspecto) ||
                (solicitud.ClienteRemitenteComercial != null && solicitud.ClienteRemitenteComercial.EsProspecto) ||
                (solicitud.ClienteCorredor != null && solicitud.ClienteCorredor.EsProspecto) ||
                (solicitud.ClienteEntregador != null && solicitud.ClienteEntregador.EsProspecto) ||
                (solicitud.ClienteDestinatario != null && solicitud.ClienteDestinatario.EsProspecto) ||
                (solicitud.ClienteDestino != null && solicitud.ClienteDestino.EsProspecto) ||
                (solicitud.ClientePagadorDelFlete != null && solicitud.ClientePagadorDelFlete.EsProspecto) ||
                (solicitud.ClienteDestinatarioCambio != null && solicitud.ClienteDestinatarioCambio.EsProspecto) ||
                (solicitud.ProveedorTitularCartaDePorte != null && solicitud.ProveedorTitularCartaDePorte.EsProspecto) ||
                (solicitud.ProveedorTransportista != null && solicitud.ProveedorTransportista.EsProspecto))
            {
                return Enums.EstadoEnvioSAP.EnEsperaPorProspecto;
            }

            return solicitud.EstadoEnSAP;
        }

        /*
        public void GetPdfThumbnail(string sourcePdfFilePath, string destinationPngFilePath)
        {
            // Use GhostscriptSharp to convert the pdf to a png
            GhostscriptWrapper.GenerateOutput(sourcePdfFilePath, destinationPngFilePath,
                new GhostscriptSettings
                {
                    Device = GhostscriptDevices.pngalpha,
                    Page = new GhostscriptPages
                    {
                        // Only make a thumbnail of the first page
                        Start = 1,
                        End = 1,
                        AllPages = false
                    },
                    Resolution = new Size
                    {
                        // Render at 72x72 dpi
                        Height = 72,
                        Width = 72
                    },
                    Size = new GhostscriptPageSize
                    {
                        // The dimentions of the incoming PDF must be
                        // specified. The example PDF is US Letter sized.
                        Native = GhostscriptPageSizes.letter
                    }
                }
            );
        }
        */
    }
}

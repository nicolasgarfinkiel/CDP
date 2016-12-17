using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CartaDePorte.Core.Domain
{
    public class Enums
    {
        public enum ConformeCondicional
        {
            Conforme = 1,
            Condicional = 2
        }
        public enum EstadoFlete
        {
            FletePagado = 1,
            FleteAPagar = 2
        }
        public enum EstadoEnAFIP
        {
            Enviado = 0,
            Otorgado = 1,
            SinProcesar = 2,
            Anulada = 3,
            CargaManual = 4,
            Confirmado = 5,
            ConfirmadoDefinitivo = 6,
            Rechazado = 7,
            CambioDestino = 8,
            VueltaOrigen = 9
        }
        public enum EstadoEnvioSAP
        {
            Pendiente = 0,
            EnProceso = 1,
            FinalizadoOk = 2,
            FinalizadoConError = 3,
            Anulada = 4,
            PedidoAnulacion = 5,
            EnProcesoAnulacion = 6,
            NoEnviadaASap = 7,
            PrimerEnvioTerceros = 8,
            EnEsperaPorProspecto = 9
        }
        public enum OrganizacionVenta
        {
            IRSA = 1000,
            IRSA_Propiedades_Comerciaes = 3000,
            Cresud_SACIFyA = 5000,
            Export_Agroind_Arg = 5100,
            Agrop_Acres_del_Sud = 8000,
            Ombu_Agropecuaria_SA = 8100,
            Yuchan_Agropecuaria = 8600,
            Yatay_Agropecuaria = 8700,
            Cresca_SA = 8800
        }
        public enum RecorridoEstablecimiento
        {
            SoloOrigen,
            SoloDestino,
            OrigenYDestino
        }
        public enum EstadoRangoCartaDePorte
        {
            Disponible,
            NoDisponible

        }

        public enum EsChoferTransportista
        {
            No,
            Si
        }
        public enum TipoCartaDePorteRecibida
        {
            Recibida = 1,
            CambioDeDestino = 3
        }

        public enum TipoDomicilio
        {
            Urbano = 1,
            Rural = 2
        }


    }
}

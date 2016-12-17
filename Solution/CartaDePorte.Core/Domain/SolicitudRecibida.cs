using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CartaDePorte.Core.Exception;

namespace CartaDePorte.Core.Domain
{
    public class SolicitudRecibida
    {
        #region Atributos        
        private int idSolicitudRecibida;
        private String tipoTransporte = "1"; // siempre es 1 
        private Enums.TipoCartaDePorteRecibida tipoDeCarta; //1 – Recibida o 3 – Cambio de Destino
        private String numeroCartaDePorte; //Carga manual del dato
        private String cee;// Carga manual del dato
        private String ctg;// Carga manual del dato
        private DateTime? fechaDeEmision;// Carga manual del dato

        private String cuitProveedorTitularCartaDePorte; // Carga manual del dato con validación
        private String cuitClienteIntermediario; // Carga manual del dato con validación
        private String cuitClienteRemitenteComercial; // Carga manual del dato con validación
        private String cuitClienteCorredor; // Carga manual del dato con validación
        private String cuitClienteEntregador; // Carga manual del dato con validación
        private String cuitClienteDestinatario; // Carga manual del dato con validación
        private String cuitClienteDestino; // Carga manual del dato con validación
        private String cuitProveedorTransportista; // Carga manual del dato con validación
        private String cuitChofer; // Carga manual del dato con validación


        //Datos de los Granos/ Especies Transportados:
        private Grano grano;
        private Int64? numeroContrato;
        private Boolean cargaPesadaDestino;
        private Int64 kilogramosEstimados;
        private Nullable<Enums.ConformeCondicional> conformeCondicional;
        private Int64? pesoBruto;
        private Int64? pesoTara;
        private Int64? pesoNeto;
        private String observaciones;
        private String loteDeMaterial;


        //Procedencia de la Mercadería: 
        private String codigoEstablecimientoProcedencia;
        private int idLocalidadEstablecimientoProcedencia;

        //Destino de los Granos
        private String codigoEstablecimientoDestino = "21570";

        //Datos del Transportista
        private String patenteCamion;
        private String patenteAcoplado;
        private Int64 kmRecorridos;
        private Nullable<Enums.EstadoFlete> estadoFlete;
        private Int64 cantHoras;
        private Decimal tarifaReferencia;
        private Decimal tarifaReal;

        //Recepción de la Mercadería
        private DateTime? fechaDeDescarga;
        private DateTime? fechaDeArribo;
        private Int64? pesoNetoDescarga;

        //Cambio del domicilio de descarga/desvío
        private String codigoEstablecimientoDestinoCambio;
        private String cuitEstablecimientoDestinoCambio;
        private int idLocalidadEstablecimientoDestinoCambio;


        // Auditoria
        private DateTime fechaCreacion;
        private String usuarioCreacion;
        private DateTime fechaModificacion;
        private String usuarioModificacion;


        private String tipoDeCartaString;
        private String proveedorTitularCartaDePorteString;
        private String establecimientoProcedenciaString;


        #endregion

        #region Constructores
        public SolicitudRecibida() { }
        #endregion

        #region Propiedades
        public int IdSolicitudRecibida
        {
            get { return idSolicitudRecibida; }
            set { idSolicitudRecibida = value; }
        }
        public String TipoTransporte
        {
            get { return tipoTransporte; }
            set { tipoTransporte = value; }
        }
        public Enums.TipoCartaDePorteRecibida TipoDeCarta
        {
            get { return tipoDeCarta; }
            set { tipoDeCarta = value; }
        }
        public String NumeroCartaDePorte
        {
            get { return numeroCartaDePorte; }
            set { numeroCartaDePorte = value; }
        }
        public String Cee
        {
            get { return cee; }
            set { cee = value; }
        }
        public String Ctg
        {
            get { return ctg; }
            set { ctg = value; }
        }
        public DateTime? FechaDeEmision
        {
            get { return fechaDeEmision; }
            set { fechaDeEmision = value; }
        }

        public String CuitProveedorTitularCartaDePorte
        {
            get { return cuitProveedorTitularCartaDePorte; }
            set { cuitProveedorTitularCartaDePorte = value; }
        }
        public String CuitClienteIntermediario
        {
            get { return cuitClienteIntermediario; }
            set { cuitClienteIntermediario = value; }
        }
        public String CuitClienteRemitenteComercial
        {
            get { return cuitClienteRemitenteComercial; }
            set { cuitClienteRemitenteComercial = value; }
        }
        public String CuitClienteCorredor
        {
            get { return cuitClienteCorredor; }
            set { cuitClienteCorredor = value; }
        }
        public String CuitClienteEntregador
        {
            get { return cuitClienteEntregador; }
            set { cuitClienteEntregador = value; }
        }
        public String CuitClienteDestinatario
        {
            get { return cuitClienteDestinatario; }
            set { cuitClienteDestinatario = value; }
        }
        public String CuitClienteDestino
        {
            get { return cuitClienteDestino; }
            set { cuitClienteDestino = value; }
        }
        public String CuitProveedorTransportista
        {
            get { return cuitProveedorTransportista; }
            set { cuitProveedorTransportista = value; }
        }
        public String CuitChofer
        {
            get { return cuitChofer; }
            set { cuitChofer = value; }
        }

        public Grano Grano
        {
            get { return grano; }
            set { grano = value; }
        }
        public Int64? NumeroContrato
        {
            get { return numeroContrato; }
            set { numeroContrato = value; }
        }
        public Boolean CargaPesadaDestino
        {
            get { return cargaPesadaDestino; }
            set { cargaPesadaDestino = value; }
        }
        public Int64 KilogramosEstimados
        {
            get { return kilogramosEstimados; }
            set { kilogramosEstimados = value; }
        }
        public Nullable<Enums.ConformeCondicional> ConformeCondicional
        {
            get { return conformeCondicional; }
            set { conformeCondicional = value; }
        }
        public Int64? PesoBruto
        {
            get { return pesoBruto; }
            set { pesoBruto = value; }
        }
        public Int64? PesoTara
        {
            get { return pesoTara; }
            set { pesoTara = value; }
        }
        public Int64? PesoNeto
        {
            get { return pesoNeto; }
            set { pesoNeto = value; }
        }
        public String Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }
        public String LoteDeMaterial
        {
            get { return loteDeMaterial; }
            set { loteDeMaterial = value; }
        }
        public String CodigoEstablecimientoDestino
        {
            get { return codigoEstablecimientoDestino; }
            set { codigoEstablecimientoDestino = value; }
        }
        public String PatenteCamion
        {
            get { return patenteCamion; }
            set { patenteCamion = value; }
        }
        public String PatenteAcoplado
        {
            get { return patenteAcoplado; }
            set { patenteAcoplado = value; }
        }
        public Int64 KmRecorridos
        {
            get { return kmRecorridos; }
            set { kmRecorridos = value; }
        }
        public Nullable<Enums.EstadoFlete> EstadoFlete
        {
            get { return estadoFlete; }
            set { estadoFlete = value; }
        }
        public Int64 CantHoras
        {
            get { return cantHoras; }
            set { cantHoras = value; }
        }
        public Decimal TarifaReferencia
        {
            get { return tarifaReferencia; }
            set { tarifaReferencia = value; }
        }
        public Decimal TarifaReal
        {
            get { return tarifaReal; }
            set { tarifaReal = value; }
        }


        public String CodigoEstablecimientoProcedencia
        {
            get { return codigoEstablecimientoProcedencia; }
            set { codigoEstablecimientoProcedencia = value; }
        }

        public int IdLocalidadEstablecimientoProcedencia
        {
            get { return idLocalidadEstablecimientoProcedencia; }
            set { idLocalidadEstablecimientoProcedencia = value; }
        }

        public String CodigoEstablecimientoDestinoCambio
        {
            get { return codigoEstablecimientoDestinoCambio; }
            set { codigoEstablecimientoDestinoCambio = value; }
        }

        public String CuitEstablecimientoDestinoCambio
        {
            get { return cuitEstablecimientoDestinoCambio; }
            set { cuitEstablecimientoDestinoCambio = value; }
        }

        public int IdLocalidadEstablecimientoDestinoCambio
        {
            get { return idLocalidadEstablecimientoDestinoCambio; }
            set { idLocalidadEstablecimientoDestinoCambio = value; }
        }

        public DateTime? FechaDeDescarga
        {
            get { return fechaDeDescarga; }
            set { fechaDeDescarga = value; }
        }

        public DateTime? FechaDeArribo
        {
            get { return fechaDeArribo; }
            set { fechaDeArribo = value; }
        }

        public Int64? PesoNetoDescarga
        {
            get { return pesoNetoDescarga; }
            set { pesoNetoDescarga = value; }
        }







        public DateTime FechaCreacion
        {
            get { return fechaCreacion; }
            set { fechaCreacion = value; }
        }
        public String UsuarioCreacion
        {
            get { return usuarioCreacion; }
            set { usuarioCreacion = value; }
        }
        public DateTime FechaModificacion
        {
            get { return fechaModificacion; }
            set { fechaModificacion = value; }
        }
        public String UsuarioModificacion
        {
            get { return usuarioModificacion; }
            set { usuarioModificacion = value; }
        }

        public String TipoDeCartaString
        {
            get { return tipoDeCarta.ToString(); }
        }

        public String ProveedorTitularCartaDePorteString
        {
            get { return cuitProveedorTitularCartaDePorte; }
        }

        public String EstablecimientoProcedenciaString
        {
            get { return codigoEstablecimientoProcedencia; }
        }

        #endregion


        #region Validacion

        public bool Validar()
        {
            return true;
        }
        #endregion

    }
}

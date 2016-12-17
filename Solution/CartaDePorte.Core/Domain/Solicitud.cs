using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CartaDePorte.Core.Exception;

namespace CartaDePorte.Core.Domain
{
    public class Solicitud
    {
        #region Atributos
        private int idSolicitud;
        private TipoDeCarta tipoDeCarta;
        private String observacionAfip;
        private String numeroCartaDePorte;
        private String cee;
        private String ctg;
        private DateTime? fechaDeEmision;
        private DateTime? fechaDeCarga;
        private DateTime? fechaDeVencimiento;
        private Proveedor idProveedorTitularCartaDePorte;
        private Cliente idClienteIntermediario;
        private Cliente idClienteRemitenteComercial;
        private bool remitenteComercialComoCanjeador = false;
        private Cliente idClienteCorredor;
        private Cliente idClienteEntregador;
        private Cliente idClienteDestinatario;
        private Cliente idClienteDestino;
        private Proveedor idProveedorTransportista;
        private Chofer idChoferTransportista;
        private Chofer idChofer;
        private Grano grano;
        private Int64? numeroContrato;
        private Int64? sapContrato;
        private Boolean sinContrato;
        private Boolean cargaPesadaDestino;
        private Int64 kilogramosEstimados;
        private Nullable<Enums.ConformeCondicional> conformeCondicional;
        private Int64? pesoBruto;
        private Int64? pesoTara;
        private Int64? pesoNeto;
        private String observaciones;
        private String loteDeMaterial;
        private Establecimiento idEstablecimientoProcedencia;
        private Establecimiento idEstablecimientoDestino;
        private String patenteCamion;
        private String patenteAcoplado;
        private Int64 kmRecorridos;
        private Nullable<Enums.EstadoFlete> estadoFlete;
        private Int64 cantHoras;
        private Decimal tarifaReferencia;
        private Decimal tarifaReal;
        private Cliente idClientePagadorDelFlete;
        private Enums.EstadoEnvioSAP estadoEnSAP;
        private Enums.EstadoEnAFIP estadoEnAFIP;
        private DateTime fechaCreacion;
        private String usuarioCreacion;
        private DateTime fechaModificacion;
        private String usuarioModificacion;
        private Int64 codigoAnulacionAfip;
        private DateTime? fechaAnulacionAfip;
        private String codigoRespuestaEnvioSAP;
        private String mensajeRespuestaEnvioSAP;
        private String codigoRespuestaAnulacionSAP;
        private String mensajeRespuestaAnulacionSAP;
        private Sox1116A sox1116A = new Sox1116A();
        private String tipoDeCartaString;
        private String proveedorTitularCartaDePorteString;
        private String establecimientoProcedenciaString;
        private decimal pHumedad;
        private decimal pOtros;

        public Sox1116A Sox1116A
        {
            get { return sox1116A; }
            set { sox1116A = value; }
        }

        private Establecimiento idEstablecimientoDestinoCambio;
        public Establecimiento IdEstablecimientoDestinoCambio
        {
            get { return idEstablecimientoDestinoCambio; }
            set { idEstablecimientoDestinoCambio = value; }
        }

        private Cliente idClienteDestinatarioCambio;
        public Cliente ClienteDestinatarioCambio
        {
            get { return idClienteDestinatarioCambio; }
            set { idClienteDestinatarioCambio = value; }
        }
        #endregion

        #region Constructores
        public Solicitud(){ }
        #endregion

        #region Propiedades
        public int IdSolicitud
        {
            get { return idSolicitud; }
            set { idSolicitud = value; }
        }
        public TipoDeCarta TipoDeCarta
        {
            get { return tipoDeCarta; }
            set { tipoDeCarta = value; }
        }
        public String ObservacionAfip
        {
            get { return observacionAfip; }
            set { observacionAfip = value; }
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
        public DateTime? FechaDeCarga
        {
            get { return fechaDeCarga; }
            set { fechaDeCarga = value; }
        }
        public DateTime? FechaDeVencimiento
        {
            get { return fechaDeVencimiento; }
            set { fechaDeVencimiento = value; }
        }
        public Proveedor ProveedorTitularCartaDePorte
        {
            get { return idProveedorTitularCartaDePorte; }
            set { idProveedorTitularCartaDePorte = value; }
        }
        public Cliente ClienteIntermediario
        {
            get { return idClienteIntermediario; }
            set { idClienteIntermediario = value; }
        }
        public Cliente ClienteRemitenteComercial
        {
            get { return idClienteRemitenteComercial; }
            set { idClienteRemitenteComercial = value; }
        }
        public bool RemitenteComercialComoCanjeador
        {
            get { return remitenteComercialComoCanjeador; }
            set { remitenteComercialComoCanjeador = value; }
        }
        public Cliente ClienteCorredor
        {
            get { return idClienteCorredor; }
            set { idClienteCorredor = value; }
        }
        public Cliente ClienteEntregador
        {
            get { return idClienteEntregador; }
            set { idClienteEntregador = value; }
        }
        public Cliente ClienteDestinatario
        {
            get { return idClienteDestinatario; }
            set { idClienteDestinatario = value; }
        }
        public Cliente ClienteDestino
        {
            get { return idClienteDestino; }
            set { idClienteDestino = value; }
        }
        public Proveedor ProveedorTransportista
        {
            get { return idProveedorTransportista; }
            set { idProveedorTransportista = value; }
        }
        public Chofer ChoferTransportista
        {
            get { return idChoferTransportista; }
            set { idChoferTransportista = value; }
        }

        public Chofer Chofer
        {
            get { return idChofer; }
            set { idChofer = value; }
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
        public Int64? SapContrato
        {
            get { return sapContrato; }
            set { sapContrato = value; }
        }
        public Boolean SinContrato
        {
            get { return sinContrato; }
            set { sinContrato = value; }
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
        public Establecimiento IdEstablecimientoProcedencia
        {
            get { return idEstablecimientoProcedencia; }
            set { idEstablecimientoProcedencia = value; }
        }
        public Establecimiento IdEstablecimientoDestino
        {
            get { return idEstablecimientoDestino; }
            set { idEstablecimientoDestino = value; }
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
        public Cliente ClientePagadorDelFlete
        {
            get { return idClientePagadorDelFlete; }
            set { idClientePagadorDelFlete = value; }
        }
        public Enums.EstadoEnvioSAP EstadoEnSAP
        {
            get { return estadoEnSAP; }
            set { estadoEnSAP = value; }
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
        public Enums.EstadoEnAFIP EstadoEnAFIP
        {
            get { return estadoEnAFIP; }
            set { estadoEnAFIP = value; }
        }
        public Int64 CodigoAnulacionAfip
        {
            get { return codigoAnulacionAfip; }
            set { codigoAnulacionAfip = value; }
        }
        public DateTime? FechaAnulacionAfip
        {
            get { return fechaAnulacionAfip; }
            set { fechaAnulacionAfip = value; }
        }
        public String CodigoRespuestaEnvioSAP
        {
            get { return codigoRespuestaEnvioSAP; }
            set { codigoRespuestaEnvioSAP = value; }
        }
        public String CodigoRespuestaAnulacionSAP
        {
            get { return codigoRespuestaAnulacionSAP; }
            set { codigoRespuestaAnulacionSAP = value; }
        }
        public String MensajeRespuestaEnvioSAP
        {
            get { return mensajeRespuestaEnvioSAP; }
            set { mensajeRespuestaEnvioSAP = value; }
        }

        public String MensajeRespuestaAnulacionSAP
        {
            get { return mensajeRespuestaAnulacionSAP; }
            set { mensajeRespuestaAnulacionSAP = value; }
        }

        public String TipoDeCartaString
        {
            get { return tipoDeCartaString; }
            set { tipoDeCartaString = value; }
        }

        public String ProveedorTitularCartaDePorteString
        {
            get { return proveedorTitularCartaDePorteString; }
            set { proveedorTitularCartaDePorteString = value; }
        }

        public String EstablecimientoProcedenciaString
        {
            get { return establecimientoProcedenciaString; }
            set { establecimientoProcedenciaString = value; }
        }

        public decimal PHumedad
        {
            get { return pHumedad; }
            set { pHumedad = value; }
        }

        public decimal POtros
        {
            get { return pOtros; }
            set { pOtros = value; }
        }

        #endregion


        #region Validacion

        public bool Validar()
        {
            return true;
        }
        #endregion

    }


    public class SolicitudFull: Solicitud
    {
        public int IdEmpresa { get; set; }

        public int IdGrupoEmpresa { get; set; }
    }

}

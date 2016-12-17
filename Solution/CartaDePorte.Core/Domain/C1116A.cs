using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CartaDePorte.Core.Exception;

namespace CartaDePorte.Core.Domain
{
    public class C1116A
    {
        public C1116A() { }

        private int idc1116a;
        public int Idc1116a
        {
            get { return idc1116a; }
            set { idc1116a = value; }
        }
        
        private String nroCertificadoc1116a;
        public String NroCertificadoc1116a
        {
            get { return nroCertificadoc1116a; }
            set { nroCertificadoc1116a = value; }
        }
        
        private int codigoEstablecimiento;
        public int CodigoEstablecimiento
        {
            get { return codigoEstablecimiento; }
            set { codigoEstablecimiento = value; }
        }

        private String cuitProveedor;
        public String CuitProveedor
        {
            get { return cuitProveedor; }
            set { cuitProveedor = value; }
        }

        private String razonSocialProveedor;
        public String RazonSocialProveedor
        {
            get { return razonSocialProveedor; }
            set { razonSocialProveedor = value; }
        }
        
        private Enums.TipoDomicilio tipoDomicilio;
        public Enums.TipoDomicilio TipoDomicilio
        {
            get { return tipoDomicilio; }
            set { tipoDomicilio = value; }
        }
        
        private String calleRutaProductor;
        public String CalleRutaProductor
        {
            get { return calleRutaProductor; }
            set { calleRutaProductor = value; }
        }
        
        private int nroKmProductor;
        public int NroKmProductor
        {
            get { return nroKmProductor; }
            set { nroKmProductor = value; }
        }
        
        private String pisoProductor;
        public String PisoProductor
        {
            get { return pisoProductor; }
            set { pisoProductor = value; }
        }
        
        private String oficinaDtoProductor;
        public String OficinaDtoProductor
        {
            get { return oficinaDtoProductor; }
            set { oficinaDtoProductor = value; }
        }
        
        private int codigoLocalidadProductor;
        public int CodigoLocalidadProductor
        {
            get { return codigoLocalidadProductor; }
            set { codigoLocalidadProductor = value; }
        }
        
        private int codigoPartidoProductor;
        public int CodigoPartidoProductor
        {
            get { return codigoPartidoProductor; }
            set { codigoPartidoProductor = value; }
        }
        
        private String codigoPostalProductor;
        public String CodigoPostalProductor
        {
            get { return codigoPostalProductor; }
            set { codigoPostalProductor = value; }
        }
        
        private int codigoEspecie;
        public int CodigoEspecie
        {
            get { return codigoEspecie; }
            set { codigoEspecie = value; }
        }

        private String cosecha;
        public String Cosecha
        {
            get { return cosecha; }
            set { cosecha = value; }
        }
        
        private int almacenajeDiasLibres;
        public int AlmacenajeDiasLibres
        {
            get { return almacenajeDiasLibres; }
            set { almacenajeDiasLibres = value; }
        }
        
        private decimal tarifaAlmacenaje;
        public decimal TarifaAlmacenaje
        {
            get { return tarifaAlmacenaje; }
            set { tarifaAlmacenaje = value; }
        }
        
        private decimal gastoGenerales;
        public decimal GastoGenerales
        {
            get { return gastoGenerales; }
            set { gastoGenerales = value; }
        }
        
        private decimal zarandeo;
        public decimal Zarandeo
        {
            get { return zarandeo; }
            set { zarandeo = value; }
        }
        
        private decimal secadoDe;
        public decimal SecadoDe
        {
            get { return secadoDe; }
            set { secadoDe = value; }
        }
        
        private decimal secadoA;
        public decimal SecadoA
        {
            get { return secadoA; }
            set { secadoA = value; }
        }
        
        private decimal tarifaSecado;
        public decimal TarifaSecado
        {
            get { return tarifaSecado; }
            set { tarifaSecado = value; }
        }
        
        private decimal puntoExceso;
        public decimal PuntoExceso
        {
            get { return puntoExceso; }
            set { puntoExceso = value; }
        }
        
        private decimal tarifaOtros;
        public decimal TarifaOtros
        {
            get { return tarifaOtros; }
            set { tarifaOtros = value; }
        }
        
        private int codigoPartidoOrigen;
        public int CodigoPartidoOrigen
        {
            get { return codigoPartidoOrigen; }
            set { codigoPartidoOrigen = value; }
        }
        
        private int codigoPartidoEntrega;
        public int CodigoPartidoEntrega
        {
            get { return codigoPartidoEntrega; }
            set { codigoPartidoEntrega = value; }
        }

        private String numeroAnalisis;
        public String NumeroAnalisis
        {
            get { return numeroAnalisis; }
            set { numeroAnalisis = value; }
        }

        private String numeroBoletin;
        public String NumeroBoletin
        {
            get { return numeroBoletin; }
            set { numeroBoletin = value; }
        }
        
        private DateTime fechaAnalisis;
        public DateTime FechaAnalisis
        {
            get { return fechaAnalisis; }
            set { fechaAnalisis = value; }
        }

        private String grado;
        public String Grado
        {
            get { return grado; }
            set { grado = value; }
        }
        
        private decimal factor;
        public decimal Factor
        {
            get { return factor; }
            set { factor = value; }
        }
        
        private decimal contenidoProteico;
        public decimal ContenidoProteico
        {
            get { return contenidoProteico; }
            set { contenidoProteico = value; }
        }

        private String cuitLaboratorio;
        public String CuitLaboratorio
        {
            get { return cuitLaboratorio; }
            set { cuitLaboratorio = value; }
        }

        private String nombreLaboratorio;
        public String NombreLaboratorio
        {
            get { return nombreLaboratorio; }
            set { nombreLaboratorio = value; }
        }
        
        private decimal pesoBruto;
        public decimal PesoBruto
        {
            get { return pesoBruto; }
            set { pesoBruto = value; }
        }
        
        private decimal mermaVolatil;
        public decimal MermaVolatil
        {
            get { return mermaVolatil; }
            set { mermaVolatil = value; }
        }
        
        private decimal mermaZarandeo;
        public decimal MermaZarandeo
        {
            get { return mermaZarandeo; }
            set { mermaZarandeo = value; }
        }
        
        private decimal mermaSecado;
        public decimal MermaSecado
        {
            get { return mermaSecado; }
            set { mermaSecado = value; }
        }
        
        private decimal pesoNeto;
        public decimal PesoNeto
        {
            get { return pesoNeto; }
            set { pesoNeto = value; }
        }
        
        private DateTime fechaCierre;
        public DateTime FechaCierre
        {
            get { return fechaCierre; }
            set { fechaCierre = value; }
        }
        
        private decimal importeIVAServicios;
        public decimal ImporteIVAServicios
        {
            get { return importeIVAServicios; }
            set { importeIVAServicios = value; }
        }

        private decimal totalServicios;
        public decimal TotalServicios
        {
            get { return totalServicios; }
            set { totalServicios = value; }
        }

        private String numeroCAC;
        public String NumeroCAC
        {
            get { return numeroCAC; }
            set { numeroCAC = value; }
        }

        private string usuarioCreacion;
        public string UsuarioCreacion
        {
            get { return usuarioCreacion; }
            set { usuarioCreacion = value; }
        }
        
        private string usuarioModificacion;
        public string UsuarioModificacion
        {
            get { return usuarioModificacion; }
            set { usuarioModificacion = value; }
        }
        
        private DateTime fechaCreacion;
        public DateTime FechaCreacion
        {
            get { return fechaCreacion; }
            set { fechaCreacion = value; }
        }
        
        private DateTime fechaModificacion;
        public DateTime FechaModificacion
        {
            get { return fechaModificacion; }
            set { fechaModificacion = value; }
        }

        public bool Validar() 
        {

            return true;
        }

        public override string ToString()
        {
            return this.nroCertificadoc1116a.ToString();
        
        }

    }
}

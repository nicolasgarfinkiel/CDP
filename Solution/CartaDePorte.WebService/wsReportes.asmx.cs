using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Reflection;
using CartaDePorte.Core.DAO;
using CartaDePorte.Core.Domain;

namespace CartaDePorte.WebService
{
    /// <summary>
    /// Summary description for wsReportes
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class wsReportes : System.Web.Services.WebService
    {

        private static DataSet GetDataTableFromIListGeneric<T>(IList<T> aIList)
        {
            DataTable dTable = new DataTable();
            object baseObj = aIList[0];
            Type objectType = baseObj.GetType();
            PropertyInfo[] properties = objectType.GetProperties();

            DataColumn col;
            foreach (PropertyInfo property in properties)
            {
                if ((string)property.Name != "EmpresaNoDataSet")
                {
                    col = new DataColumn();
                    col.ColumnName = (string)property.Name;
                    //col.DataType = property.PropertyType;
                    dTable.Columns.Add(col);
                }
            }
            //Adds the rows to the table
            DataRow row;
            foreach (object objItem in aIList)
            {
                row = dTable.NewRow();

                foreach (PropertyInfo property in properties)
                {
                    if (property.Name != "EmpresaNoDataSet")
                    {
                        row[property.Name] = property.GetValue(objItem, null);
                    }
                }
                dTable.Rows.Add(row);
            }

            DataSet ds = new DataSet("Resultado");
            ds.Tables.Add(dTable);
            return ds;

        }       

        [WebMethod(EnableSession = true)]
        public DataSet getCartaDePorte(int idSolicitud)
        {

            Solicitud solicitud = SolicitudDAO.Instance.GetOne(idSolicitud);

            IList<SolicitudReporte> solicitudes = new List<SolicitudReporte>();

            SolicitudReporte solRep = new SolicitudReporte();
            if (solicitud != null)
            {
                // Cabecera
                solRep.NumeroCTG = solicitud.Ctg;
                solRep.FechaCarga = solicitud.FechaDeCarga.Value.ToShortDateString();
                solRep.FechaVencimiento = (solicitud.FechaDeVencimiento.HasValue) ? solicitud.FechaDeVencimiento.Value.ToShortDateString() : string.Empty;
                
                // Intervinientes del traslado
                solRep.NombreTitularCartaPorte = (solicitud.ProveedorTitularCartaDePorte != null) ? solicitud.ProveedorTitularCartaDePorte.Nombre : string.Empty;
                solRep.CuitTitularCartaPorte = (solicitud.ProveedorTitularCartaDePorte != null) ? solicitud.ProveedorTitularCartaDePorte.NumeroDocumento : string.Empty;
                solRep.NombreIntermediario = (solicitud.ClienteIntermediario != null) ? solicitud.ClienteIntermediario.RazonSocial : string.Empty;
                solRep.CuitIntermediario = (solicitud.ClienteIntermediario != null) ? solicitud.ClienteIntermediario.Cuit : string.Empty;
                solRep.NombreRemitenteComercial = (solicitud.ClienteRemitenteComercial != null) ? solicitud.ClienteRemitenteComercial.RazonSocial : string.Empty;
                solRep.CuitRemitenteComercial = (solicitud.ClienteRemitenteComercial != null) ? solicitud.ClienteRemitenteComercial.Cuit : string.Empty;
                solRep.NombreCorredor = (solicitud.ClienteCorredor!= null) ? solicitud.ClienteCorredor.RazonSocial : string.Empty;
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
                solRep.CuitChofer = (solicitud.Chofer != null) ? solicitud.Chofer.Cuit: string.Empty;
                
                //datos de los granos.
                solRep.GranoEspecie = (solicitud.Grano != null) ? solicitud.Grano.EspecieAfip.Descripcion: string.Empty;
                solRep.TipoGranoEspecie = (solicitud.Grano != null) ? solicitud.Grano.TipoGrano.Descripcion: string.Empty;
                solRep.Cosecha = (solicitud.Grano != null) ? solicitud.Grano.CosechaAfip.Descripcion : string.Empty;
                if (solicitud.CargaPesadaDestino)
                {
                    solRep.CargaPesadaDestino = "X";
                    solRep.KgrsEstimados = solicitud.KilogramosEstimados.ToString();
                }
                else 
                {
                    solRep.PesoBruto = (solicitud.PesoBruto.HasValue) ? solicitud.PesoBruto.Value.ToString(): string.Empty;
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
                solRep.DireccionEstablecimientoProcedencia = (solicitud.IdEstablecimientoProcedencia != null) ? solicitud.IdEstablecimientoProcedencia.Direccion: string.Empty;
                solRep.LocalidadEstablecimientoProcedencia = (solicitud.IdEstablecimientoProcedencia != null) ? solicitud.IdEstablecimientoProcedencia.Localidad.Descripcion: string.Empty;
                solRep.ProvinciaEstablecimientoProcedencia = (solicitud.IdEstablecimientoProcedencia != null) ? solicitud.IdEstablecimientoProcedencia.Provincia.Descripcion : string.Empty;

                // Destino de la mercaderia.
                solRep.DireccionEstablecimientoDestino = (solicitud.IdEstablecimientoDestino != null) ? solicitud.IdEstablecimientoDestino.Direccion : string.Empty;
                solRep.LocalidadEstablecimientoDestino = (solicitud.IdEstablecimientoDestino != null) ? solicitud.IdEstablecimientoDestino.Localidad.Descripcion: string.Empty;
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
                solRep.NombrePagadorDelFlete = (solicitud.ClientePagadorDelFlete != null) ? solicitud.ClientePagadorDelFlete.RazonSocial: string.Empty;

                solicitudes.Add(solRep);
            }
            
            if (solicitudes.Count == 0)
            {
                solicitudes.Add(new SolicitudReporte());
                return GetDataTableFromIListGeneric(solicitudes);
            }
            return GetDataTableFromIListGeneric(solicitudes);

        }


    }


    public class SolicitudReporte
    {
        private string numeroCartaDePorte;

        public string NumeroCartaDePorte
        {
            get { return numeroCartaDePorte; }
            set { numeroCartaDePorte = value; }
        }
        private string numeroCEE;

        public string NumeroCEE
        {
            get { return numeroCEE; }
            set { numeroCEE = value; }
        }
        private string numeroCTG;

        public string NumeroCTG
        {
            get { return numeroCTG; }
            set { numeroCTG = value; }
        }
        private string fechaCarga;

        public string FechaCarga
        {
            get { return fechaCarga; }
            set { fechaCarga = value; }
        }
        private string fechaVencimiento;

        public string FechaVencimiento
        {
            get { return fechaVencimiento; }
            set { fechaVencimiento = value; }
        }
        private string nombreTitularCartaPorte;

        public string NombreTitularCartaPorte
        {
            get { return nombreTitularCartaPorte; }
            set { nombreTitularCartaPorte = value; }
        }
        private string cuitTitularCartaPorte;

        public string CuitTitularCartaPorte
        {
            get { return cuitTitularCartaPorte; }
            set { cuitTitularCartaPorte = value; }
        }
        private string nombreIntermediario;

        public string NombreIntermediario
        {
            get { return nombreIntermediario; }
            set { nombreIntermediario = value; }
        }
        private string cuitIntermediario;

        public string CuitIntermediario
        {
            get { return cuitIntermediario; }
            set { cuitIntermediario = value; }
        }
        private string nombreRemitenteComercial;

        public string NombreRemitenteComercial
        {
            get { return nombreRemitenteComercial; }
            set { nombreRemitenteComercial = value; }
        }
        private string cuitRemitenteComercial;

        public string CuitRemitenteComercial
        {
            get { return cuitRemitenteComercial; }
            set { cuitRemitenteComercial = value; }
        }
        private string nombreCorredor;

        public string NombreCorredor
        {
            get { return nombreCorredor; }
            set { nombreCorredor = value; }
        }
        private string cuitCorredor;

        public string CuitCorredor
        {
            get { return cuitCorredor; }
            set { cuitCorredor = value; }
        }
        private string nombreEntregador;

        public string NombreEntregador
        {
            get { return nombreEntregador; }
            set { nombreEntregador = value; }
        }
        private string cuitEntregador;

        public string CuitEntregador
        {
            get { return cuitEntregador; }
            set { cuitEntregador = value; }
        }
        private string nombreDestinatario;

        public string NombreDestinatario
        {
            get { return nombreDestinatario; }
            set { nombreDestinatario = value; }
        }
        private string cuitDestinatario;

        public string CuitDestinatario
        {
            get { return cuitDestinatario; }
            set { cuitDestinatario = value; }
        }
        private string nombreDestino;

        public string NombreDestino
        {
            get { return nombreDestino; }
            set { nombreDestino = value; }
        }
        private string cuitDestino;

        public string CuitDestino
        {
            get { return cuitDestino; }
            set { cuitDestino = value; }
        }
        private string nombreTransportista;

        public string NombreTransportista
        {
            get { return nombreTransportista; }
            set { nombreTransportista = value; }
        }
        private string cuitTransportista;

        public string CuitTransportista
        {
            get { return cuitTransportista; }
            set { cuitTransportista = value; }
        }
        private string nombreChofer;

        public string NombreChofer
        {
            get { return nombreChofer; }
            set { nombreChofer = value; }
        }
        private string cuitChofer;

        public string CuitChofer
        {
            get { return cuitChofer; }
            set { cuitChofer = value; }
        }

        private string granoEspecie;

        public string GranoEspecie
        {
            get { return granoEspecie; }
            set { granoEspecie = value; }
        }
        private string tipoGranoEspecie;

        public string TipoGranoEspecie
        {
            get { return tipoGranoEspecie; }
            set { tipoGranoEspecie = value; }
        }
        private string cosecha;

        public string Cosecha
        {
            get { return cosecha; }
            set { cosecha = value; }
        }
        private string nroContrato;

        public string NroContrato
        {
            get { return nroContrato; }
            set { nroContrato = value; }
        }

        private string cargaPesadaDestino;

        public string CargaPesadaDestino
        {
            get { return cargaPesadaDestino; }
            set { cargaPesadaDestino = value; }
        }
        private string conforme;

        public string Conforme
        {
            get { return conforme; }
            set { conforme = value; }
        }
        private string condicional;

        public string Condicional
        {
            get { return condicional; }
            set { condicional = value; }
        }
        private string kgrsEstimados;

        public string KgrsEstimados
        {
            get { return kgrsEstimados; }
            set { kgrsEstimados = value; }
        }

        private string pesoBruto;

        public string PesoBruto
        {
            get { return pesoBruto; }
            set { pesoBruto = value; }
        }
        private string pesoTara;

        public string PesoTara
        {
            get { return pesoTara; }
            set { pesoTara = value; }
        }
        private string pesoNeto;

        public string PesoNeto
        {
            get { return pesoNeto; }
            set { pesoNeto = value; }
        }
        private string observaciones;

        public string Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }

        private string nombreEstablecimientoProcedencia;

        public string NombreEstablecimientoProcedencia
        {
            get { return nombreEstablecimientoProcedencia; }
            set { nombreEstablecimientoProcedencia = value; }
        }
        private string direccionEstablecimientoProcedencia;

        public string DireccionEstablecimientoProcedencia
        {
            get { return direccionEstablecimientoProcedencia; }
            set { direccionEstablecimientoProcedencia = value; }
        }
        private string localidadEstablecimientoProcedencia;

        public string LocalidadEstablecimientoProcedencia
        {
            get { return localidadEstablecimientoProcedencia; }
            set { localidadEstablecimientoProcedencia = value; }
        }
        private string provinciaEstablecimientoProcedencia;

        public string ProvinciaEstablecimientoProcedencia
        {
            get { return provinciaEstablecimientoProcedencia; }
            set { provinciaEstablecimientoProcedencia = value; }
        }

        private string direccionEstablecimientoDestino;

        public string DireccionEstablecimientoDestino
        {
            get { return direccionEstablecimientoDestino; }
            set { direccionEstablecimientoDestino = value; }
        }
        private string localidadEstablecimientoDestino;

        public string LocalidadEstablecimientoDestino
        {
            get { return localidadEstablecimientoDestino; }
            set { localidadEstablecimientoDestino = value; }
        }
        private string provinciaEstablecimientoDestino;

        public string ProvinciaEstablecimientoDestino
        {
            get { return provinciaEstablecimientoDestino; }
            set { provinciaEstablecimientoDestino = value; }
        }

        private string nombrePagadorDelFlete;

        public string NombrePagadorDelFlete
        {
            get { return nombrePagadorDelFlete; }
            set { nombrePagadorDelFlete = value; }
        }

        private string camion;

        public string Camion
        {
            get { return camion; }
            set { camion = value; }
        }
        private string acoplado;

        public string Acoplado
        {
            get { return acoplado; }
            set { acoplado = value; }
        }
        private string kmRecorrer;

        public string KmRecorrer
        {
            get { return kmRecorrer; }
            set { kmRecorrer = value; }
        }
        private string fletePag;

        public string FletePag
        {
            get { return fletePag; }
            set { fletePag = value; }
        }
        private string fleteAPag;

        public string FleteAPag
        {
            get { return fleteAPag; }
            set { fleteAPag = value; }
        }

        private string tarifaReferencia;

        public string TarifaReferencia
        {
            get { return tarifaReferencia; }
            set { tarifaReferencia = value; }
        }
        private string tarifaReal;

        public string TarifaReal
        {
            get { return tarifaReal; }
            set { tarifaReal = value; }
        }

    }
}

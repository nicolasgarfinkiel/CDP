﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.34014.
// 
#pragma warning disable 1591

namespace CartaDePorte.Core.wsSAPPrefacturas {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="re_prefacturas_out_async_MIBinding", Namespace="http://irsa.com/xi/re/prefact")]
    public partial class re_prefacturas_out_async_MIService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback re_prefacturas_out_async_MIOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public re_prefacturas_out_async_MIService() {
            this.Url = global::CartaDePorte.Core.Properties.Settings.Default.CartaDePorte_Core_wsSAPPrefacturas_re_prefacturas_out_async_MIService;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event re_prefacturas_out_async_MICompletedEventHandler re_prefacturas_out_async_MICompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://sap.com/xi/WebService/soap1.1", OneWay=true, Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        public void re_prefacturas_out_async_MI([System.Xml.Serialization.XmlElementAttribute(Namespace="http://irsa.com/xi/re/prefact")] prefact_DT prefact_MT) {
            this.Invoke("re_prefacturas_out_async_MI", new object[] {
                        prefact_MT});
        }
        
        /// <remarks/>
        public void re_prefacturas_out_async_MIAsync(prefact_DT prefact_MT) {
            this.re_prefacturas_out_async_MIAsync(prefact_MT, null);
        }
        
        /// <remarks/>
        public void re_prefacturas_out_async_MIAsync(prefact_DT prefact_MT, object userState) {
            if ((this.re_prefacturas_out_async_MIOperationCompleted == null)) {
                this.re_prefacturas_out_async_MIOperationCompleted = new System.Threading.SendOrPostCallback(this.Onre_prefacturas_out_async_MIOperationCompleted);
            }
            this.InvokeAsync("re_prefacturas_out_async_MI", new object[] {
                        prefact_MT}, this.re_prefacturas_out_async_MIOperationCompleted, userState);
        }
        
        private void Onre_prefacturas_out_async_MIOperationCompleted(object arg) {
            if ((this.re_prefacturas_out_async_MICompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.re_prefacturas_out_async_MICompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34230")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://irsa.com/xi/re/prefact")]
    public partial class prefact_DT {
        
        private prefact_DTSAPSDLiquidacionCabecera[] sAPSDLiquidacionCabeceraField;
        
        private prefact_DTSAPSDLiquidacionPosicion[] sAPSDLiquidacionPosicionField;
        
        private prefact_DTSAPSDLiquidacionPosicionTextos[] sAPSDLiquidacionPosicionTextosField;
        
        private prefact_DTSAPSDLiquidacionCondicion[] sAPSDLiquidacionCondicionField;
        
        private prefact_DTSAPSDLiquidacionCabeceraTextos[] sAPSDLiquidacionCabeceraTextosField;
        
        private prefact_DTSAPSDLiquidacionCuotas[] sAPSDLiquidacionCuotasField;
        
        private prefact_DTSAPSDLiquidacionReferencia[] sAPSDLiquidacionReferenciaField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SAPSDLiquidacionCabecera", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public prefact_DTSAPSDLiquidacionCabecera[] SAPSDLiquidacionCabecera {
            get {
                return this.sAPSDLiquidacionCabeceraField;
            }
            set {
                this.sAPSDLiquidacionCabeceraField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SAPSDLiquidacionPosicion", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public prefact_DTSAPSDLiquidacionPosicion[] SAPSDLiquidacionPosicion {
            get {
                return this.sAPSDLiquidacionPosicionField;
            }
            set {
                this.sAPSDLiquidacionPosicionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SAPSDLiquidacionPosicionTextos", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public prefact_DTSAPSDLiquidacionPosicionTextos[] SAPSDLiquidacionPosicionTextos {
            get {
                return this.sAPSDLiquidacionPosicionTextosField;
            }
            set {
                this.sAPSDLiquidacionPosicionTextosField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SAPSDLiquidacionCondicion", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public prefact_DTSAPSDLiquidacionCondicion[] SAPSDLiquidacionCondicion {
            get {
                return this.sAPSDLiquidacionCondicionField;
            }
            set {
                this.sAPSDLiquidacionCondicionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SAPSDLiquidacionCabeceraTextos", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public prefact_DTSAPSDLiquidacionCabeceraTextos[] SAPSDLiquidacionCabeceraTextos {
            get {
                return this.sAPSDLiquidacionCabeceraTextosField;
            }
            set {
                this.sAPSDLiquidacionCabeceraTextosField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SAPSDLiquidacionCuotas", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public prefact_DTSAPSDLiquidacionCuotas[] SAPSDLiquidacionCuotas {
            get {
                return this.sAPSDLiquidacionCuotasField;
            }
            set {
                this.sAPSDLiquidacionCuotasField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SAPSDLiquidacionReferencia", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public prefact_DTSAPSDLiquidacionReferencia[] SAPSDLiquidacionReferencia {
            get {
                return this.sAPSDLiquidacionReferenciaField;
            }
            set {
                this.sAPSDLiquidacionReferenciaField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34230")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://irsa.com/xi/re/prefact")]
    public partial class prefact_DTSAPSDLiquidacionCabecera {
        
        private string id_LiquidacionField;
        
        private string sAP_Id_SociedadField;
        
        private string sAP_Id_MonedaField;
        
        private string sAP_Id_Punto_de_VentaField;
        
        private string id_Tipo_DocumentoField;
        
        private string sAP_Id_OperacionField;
        
        private string sAP_Id_SubOperaciónField;
        
        private string sAP_Id_Doc_ReferenciaField;
        
        private string sAP_Indicador_Doc_RefenciaField;
        
        private decimal sAP_Tipo_de_CambioField;
        
        private string sAP_Fecha_EmisionField;
        
        private string sAP_Fecha_FacturaField;
        
        private string sAP_Fecha_PrestacionField;
        
        private string sAP_Fecha_valorField;
        
        private string sAP_Id_Condicion_de_pagoField;
        
        private string sAP_Id_Ag_SolicitanteField;
        
        private string sAP_Id_We_DestintarioField;
        
        private string sAP_Id_Zs_CedenteField;
        
        private string referencia_DocumentosField;
        
        private string debito_directoField;
        
        private string sAP_Id_Tr_TransportistaField;
        
        private string sAP_Id_CentroField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Id_Liquidacion {
            get {
                return this.id_LiquidacionField;
            }
            set {
                this.id_LiquidacionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SAP_Id_Sociedad {
            get {
                return this.sAP_Id_SociedadField;
            }
            set {
                this.sAP_Id_SociedadField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SAP_Id_Moneda {
            get {
                return this.sAP_Id_MonedaField;
            }
            set {
                this.sAP_Id_MonedaField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SAP_Id_Punto_de_Venta {
            get {
                return this.sAP_Id_Punto_de_VentaField;
            }
            set {
                this.sAP_Id_Punto_de_VentaField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Id_Tipo_Documento {
            get {
                return this.id_Tipo_DocumentoField;
            }
            set {
                this.id_Tipo_DocumentoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SAP_Id_Operacion {
            get {
                return this.sAP_Id_OperacionField;
            }
            set {
                this.sAP_Id_OperacionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SAP_Id_SubOperación {
            get {
                return this.sAP_Id_SubOperaciónField;
            }
            set {
                this.sAP_Id_SubOperaciónField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SAP_Id_Doc_Referencia {
            get {
                return this.sAP_Id_Doc_ReferenciaField;
            }
            set {
                this.sAP_Id_Doc_ReferenciaField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SAP_Indicador_Doc_Refencia {
            get {
                return this.sAP_Indicador_Doc_RefenciaField;
            }
            set {
                this.sAP_Indicador_Doc_RefenciaField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal SAP_Tipo_de_Cambio {
            get {
                return this.sAP_Tipo_de_CambioField;
            }
            set {
                this.sAP_Tipo_de_CambioField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SAP_Fecha_Emision {
            get {
                return this.sAP_Fecha_EmisionField;
            }
            set {
                this.sAP_Fecha_EmisionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SAP_Fecha_Factura {
            get {
                return this.sAP_Fecha_FacturaField;
            }
            set {
                this.sAP_Fecha_FacturaField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SAP_Fecha_Prestacion {
            get {
                return this.sAP_Fecha_PrestacionField;
            }
            set {
                this.sAP_Fecha_PrestacionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SAP_Fecha_valor {
            get {
                return this.sAP_Fecha_valorField;
            }
            set {
                this.sAP_Fecha_valorField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SAP_Id_Condicion_de_pago {
            get {
                return this.sAP_Id_Condicion_de_pagoField;
            }
            set {
                this.sAP_Id_Condicion_de_pagoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SAP_Id_Ag_Solicitante {
            get {
                return this.sAP_Id_Ag_SolicitanteField;
            }
            set {
                this.sAP_Id_Ag_SolicitanteField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SAP_Id_We_Destintario {
            get {
                return this.sAP_Id_We_DestintarioField;
            }
            set {
                this.sAP_Id_We_DestintarioField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SAP_Id_Zs_Cedente {
            get {
                return this.sAP_Id_Zs_CedenteField;
            }
            set {
                this.sAP_Id_Zs_CedenteField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Referencia_Documentos {
            get {
                return this.referencia_DocumentosField;
            }
            set {
                this.referencia_DocumentosField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Debito_directo {
            get {
                return this.debito_directoField;
            }
            set {
                this.debito_directoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SAP_Id_Tr_Transportista {
            get {
                return this.sAP_Id_Tr_TransportistaField;
            }
            set {
                this.sAP_Id_Tr_TransportistaField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SAP_Id_Centro {
            get {
                return this.sAP_Id_CentroField;
            }
            set {
                this.sAP_Id_CentroField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34230")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://irsa.com/xi/re/prefact")]
    public partial class prefact_DTSAPSDLiquidacionPosicion {
        
        private string id_LiquidacionField;
        
        private string id_PosicionField;
        
        private string sAP_Id_MaterialField;
        
        private string sAP_UtilizacionField;
        
        private string id_UnidadField;
        
        private string agrupar_Cta_CteField;
        
        private string sAP_Id_CEBEField;
        
        private string referenciaField;
        
        private decimal cantidadField;
        
        private decimal pesoField;
        
        private string sAP_id_CentroField;
        
        private string sAP_id_AlmacenField;
        
        private string sAP_id_ExpedicionField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Id_Liquidacion {
            get {
                return this.id_LiquidacionField;
            }
            set {
                this.id_LiquidacionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Id_Posicion {
            get {
                return this.id_PosicionField;
            }
            set {
                this.id_PosicionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SAP_Id_Material {
            get {
                return this.sAP_Id_MaterialField;
            }
            set {
                this.sAP_Id_MaterialField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SAP_Utilizacion {
            get {
                return this.sAP_UtilizacionField;
            }
            set {
                this.sAP_UtilizacionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Id_Unidad {
            get {
                return this.id_UnidadField;
            }
            set {
                this.id_UnidadField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Agrupar_Cta_Cte {
            get {
                return this.agrupar_Cta_CteField;
            }
            set {
                this.agrupar_Cta_CteField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SAP_Id_CEBE {
            get {
                return this.sAP_Id_CEBEField;
            }
            set {
                this.sAP_Id_CEBEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Referencia {
            get {
                return this.referenciaField;
            }
            set {
                this.referenciaField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal Cantidad {
            get {
                return this.cantidadField;
            }
            set {
                this.cantidadField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal Peso {
            get {
                return this.pesoField;
            }
            set {
                this.pesoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SAP_id_Centro {
            get {
                return this.sAP_id_CentroField;
            }
            set {
                this.sAP_id_CentroField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SAP_id_Almacen {
            get {
                return this.sAP_id_AlmacenField;
            }
            set {
                this.sAP_id_AlmacenField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SAP_id_Expedicion {
            get {
                return this.sAP_id_ExpedicionField;
            }
            set {
                this.sAP_id_ExpedicionField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34230")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://irsa.com/xi/re/prefact")]
    public partial class prefact_DTSAPSDLiquidacionPosicionTextos {
        
        private string id_LiquidacionField;
        
        private string id_PosicionField;
        
        private string idField;
        
        private string textoField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Id_Liquidacion {
            get {
                return this.id_LiquidacionField;
            }
            set {
                this.id_LiquidacionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Id_Posicion {
            get {
                return this.id_PosicionField;
            }
            set {
                this.id_PosicionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Texto {
            get {
                return this.textoField;
            }
            set {
                this.textoField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34230")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://irsa.com/xi/re/prefact")]
    public partial class prefact_DTSAPSDLiquidacionCondicion {
        
        private string id_LiquidacionField;
        
        private string id_PosicionField;
        
        private string sAP_Id_Clase_de_CondicionField;
        
        private decimal importe_CondicionField;
        
        private decimal porcentaje_CondicionField;
        
        private decimal porcentaje_No_GravadoField;
        
        private string sAP_Id_Moneda_CondicionField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Id_Liquidacion {
            get {
                return this.id_LiquidacionField;
            }
            set {
                this.id_LiquidacionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Id_Posicion {
            get {
                return this.id_PosicionField;
            }
            set {
                this.id_PosicionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SAP_Id_Clase_de_Condicion {
            get {
                return this.sAP_Id_Clase_de_CondicionField;
            }
            set {
                this.sAP_Id_Clase_de_CondicionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal Importe_Condicion {
            get {
                return this.importe_CondicionField;
            }
            set {
                this.importe_CondicionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal Porcentaje_Condicion {
            get {
                return this.porcentaje_CondicionField;
            }
            set {
                this.porcentaje_CondicionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal Porcentaje_No_Gravado {
            get {
                return this.porcentaje_No_GravadoField;
            }
            set {
                this.porcentaje_No_GravadoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SAP_Id_Moneda_Condicion {
            get {
                return this.sAP_Id_Moneda_CondicionField;
            }
            set {
                this.sAP_Id_Moneda_CondicionField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34230")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://irsa.com/xi/re/prefact")]
    public partial class prefact_DTSAPSDLiquidacionCabeceraTextos {
        
        private string id_LiquidacionField;
        
        private string idField;
        
        private string textoField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Id_Liquidacion {
            get {
                return this.id_LiquidacionField;
            }
            set {
                this.id_LiquidacionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Texto {
            get {
                return this.textoField;
            }
            set {
                this.textoField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34230")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://irsa.com/xi/re/prefact")]
    public partial class prefact_DTSAPSDLiquidacionCuotas {
        
        private string id_LiquidacionField;
        
        private string fecha_VtoField;
        
        private string cuotaField;
        
        private decimal porcentajeField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Id_Liquidacion {
            get {
                return this.id_LiquidacionField;
            }
            set {
                this.id_LiquidacionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Fecha_Vto {
            get {
                return this.fecha_VtoField;
            }
            set {
                this.fecha_VtoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Cuota {
            get {
                return this.cuotaField;
            }
            set {
                this.cuotaField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal Porcentaje {
            get {
                return this.porcentajeField;
            }
            set {
                this.porcentajeField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34230")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://irsa.com/xi/re/prefact")]
    public partial class prefact_DTSAPSDLiquidacionReferencia {
        
        private string id_LiquidacionField;
        
        private string ref_Sap_Id_prefacturaField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Id_Liquidacion {
            get {
                return this.id_LiquidacionField;
            }
            set {
                this.id_LiquidacionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Ref_Sap_Id_prefactura {
            get {
                return this.ref_Sap_Id_prefacturaField;
            }
            set {
                this.ref_Sap_Id_prefacturaField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    public delegate void re_prefacturas_out_async_MICompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
}

#pragma warning restore 1591
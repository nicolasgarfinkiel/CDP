/* JS para la pagina Index.aspx*/
var hiddenCambioDestino;
var hiddenreenvioSAP;
var hiddenreenvioAFIP;
var hiddenactivarModelo;
var tipoCartaDePorte;


$(document).ready(function() {
//SeteoCartaDePorteInicial();
//SeteoCartaDePorte($("[id$=cboTipoDeCarta] :selected").text());


});


function updateNavigation(title) {
    alert(title);
}



$(function() {
    $("[id$=cboTipoDeCarta]").change(function() {
        SeteoCartaDePorte($("[id$=cboTipoDeCarta] :selected").text());

    });

});


$(function() {
    $("[id$=txtFechaDeEmision]").datepicker({
        showOn: 'button',
        buttonImageOnly: true,
        dateFormat: "dd/mm/yy",
        buttonImage: 'Content/Images/Calendar.gif'
    });
});

function ValidarGuardarCambioDesvio() {
    return window.confirm("Los unicos datos que se guardaran son los correspondientes al cuadro 6.Cambio de descarga / desvio")
}


function ValidarAnular() {
    return window.confirm("¿ Desea anular esta Carta de Porte ?")
}

function ValidarConfirmarArribo() {
    return window.confirm("¿ Desea Confirmar el Arribo ?")
}

function disableGuardar() {
    var control = "ctl00_ContentPlaceHolder1_btnGuardar";
    document.getElementById(control).disabled = true;
    __doPostBack("guardar", "guardar");

}

function disableSoloGuardar() {
    var control = "ctl00_ContentPlaceHolder1_btnSoloGuardar";
    document.getElementById(control).disabled = true;
    __doPostBack("Sologuardar", "Sologuardar");

}
function BuscadorManager(idempresa, control, valordescripcion, valorcuit) {
    var cuit = "ctl00_ContentPlaceHolder1_txtCuit" + control;
    var descripcion = "ctl00_ContentPlaceHolder1_cbo" + control;
    var hide = "ctl00_ContentPlaceHolder1_hb" + control;

    var oCuit = document.getElementById(cuit);
    if (oCuit != null)
        oCuit.innerHTML = valorcuit;

    var oDescripcion = document.getElementById(descripcion);
    if (oDescripcion != null)
        oDescripcion.innerHTML = valordescripcion;

    var hidde = document.getElementById(hide);
    if (hidde != null)
        hidde.Value = idempresa;

    __doPostBack(control, idempresa);

}

function SeteoCartaDePorteInicial() {

    var hiddenCambioDestino = $("[id$=hiddenCambioDestino]").val();
    var hiddenreenvioSAP = $("[id$=hiddenreenvioSAP]").val();
    var hiddenreenvioAFIP = $("[id$=hiddenreenvioAFIP]").val();
    var hiddenactivarModelo = $("[id$=hiddenactivarModelo]").val();
    var tipoCartaDePorte = $("[id$=cboTipoDeCarta] :selected").text();


    // Seteos Generales
    // Botones
    $("[id$=btnGuardar]").enabled(false);
    $("[id$=btnSoloGuardar]").enabled(false);

    $("[id$=btnNueva]").enabled(false);
    $("[id$=btnModelo]").enabled(false);
    $("[id$=btnImprimir]").enabled(false);
    $("[id$=btnAnular]").enabled(false);
    $("[id$=btnModelo]").enabled(false);
    $("[id$=btnConfirmarArribo]").enabled(false);

    // Labels
    $("[id$=lblFechaVencimiento]").visible(false);

    // inputs
    $("[id$=txtCtgManual]").visible(false);
    $("[id$=txtCtg]").visible(true);

    $("[id$=txtNumeroCDPManual]").visible(false);
    $("[id$=txtNumeroCDP]").visible(true);

    $("[id$=txtNumeroCEEManual]").visible(false);
    $("[id$=txtNumeroCEE]").visible(true);

    $("[id$=txtFechaVencimiento]").visible(false);
    $("[id$=txtFechaDeEmision]").visible(true);
    $("[id$=txtTarifaReferencia]").enabled(false);

    // Extras
    $("[id$=btnGuardarCambio]").visible(false);
    $("[id$=tableCambioDestino]").visible(false);


}


function SeteoCartaDePorte(tcdp) {
    //  Sin TipoCartaDePorte = Reserva, Con TipoCartaDePorte = Edicion.
   
   switch (tcdp) {
        case "Venta de granos propios":
            
            $("[id$=btnGuardar]").enabled(true);
            $("[id$=btnSoloGuardar]").enabled(true);

            $("[id$=btnNueva]").enabled(true);
            $("[id$=btnModelo]").enabled(false);
            $("[id$=btnImprimir]").enabled(true);
            $("[id$=btnAnular]").enabled(true);
            $("[id$=btnConfirmarArribo]").enabled(false);

            // inputs
            $("[id$=txtCtgManual]").visible(false);
            $("[id$=txtCtg]").visible(true);

            $("[id$=txtNumeroCDPManual]").visible(false);
            $("[id$=txtNumeroCDP]").visible(true);

            $("[id$=txtNumeroCEEManual]").visible(false);
            $("[id$=txtNumeroCEE]").visible(true);

            $("[id$=lblFechaVencimiento]").visible(false);
            $("[id$=txtFechaVencimiento]").visible(false);
            $("[id$=txtFechaDeEmision]").visible(true);
            $("[id$=txtTarifaReferencia]").enabled(false);

            break;
        case "Venta de granos de terceros":

            $("[id$=btnGuardar]").enabled(true); //
            $("[id$=btnSoloGuardar]").enabled(true);

            $("[id$=btnNueva]").enabled(true);
            $("[id$=btnModelo]").enabled(false);
            $("[id$=btnImprimir]").enabled(true);
            $("[id$=btnAnular]").enabled(false); // 
            $("[id$=btnModelo]").enabled(false);
            $("[id$=btnConfirmarArribo]").enabled(false);

            // inputs
            $("[id$=txtCtgManual]").visible(true); //
            $("[id$=txtCtg]").visible(false); //

            $("[id$=txtNumeroCDPManual]").visible(true); //
            $("[id$=txtNumeroCDP]").visible(false); //

            $("[id$=txtNumeroCEEManual]").visible(true); //
            $("[id$=txtNumeroCEE]").visible(false); //

            $("[id$=lblFechaVencimiento]").visible(true);
            $("[id$=txtFechaVencimiento]").visible(true);
            $("[id$=txtFechaDeEmision]").visible(true);
            $("[id$=txtTarifaReferencia]").enabled(true);

            //$("[id$=txtFechaDeEmision]").val(getHoy())
            //$("[id$=txtFechaVencimiento]").val(getHoy())
            //$("[id$=txtCtgManual]").val($("[id$=txtCtg]").getText())
            //$("[id$=txtNumeroCDPManual]").val($("[id$=txtNumeroCDP]").getText())
            //$("[id$=txtNumeroCEEManual]").val($("[id$=txtNumeroCEE]").getText())

            break;
        case "Compra de granos":
            $("[id$=btnGuardar]").enabled(false); //
            $("[id$=btnSoloGuardar]").enabled(true);

            $("[id$=btnNueva]").enabled(true);
            $("[id$=btnModelo]").enabled(false);
            $("[id$=btnImprimir]").enabled(true);
            $("[id$=btnAnular]").enabled(false); // 
            $("[id$=btnModelo]").enabled(false);
            $("[id$=btnConfirmarArribo]").enabled(false);

            // inputs
            $("[id$=txtCtgManual]").visible(true); //
            $("[id$=txtCtg]").visible(false); //

            $("[id$=txtNumeroCDPManual]").visible(true); //
            $("[id$=txtNumeroCDP]").visible(false); //

            $("[id$=txtNumeroCEEManual]").visible(true); //
            $("[id$=txtNumeroCEE]").visible(false); //

            $("[id$=lblFechaVencimiento]").visible(true);
            $("[id$=txtFechaVencimiento]").visible(true);
            $("[id$=txtFechaDeEmision]").visible(true);
            $("[id$=txtTarifaReferencia]").enabled(true);


            
            //$("[id$=txtFechaDeEmision]").val(getHoy())
            //$("[id$=txtFechaVencimiento]").val(getHoy())
            //$("[id$=txtCtgManual]").val($("[id$=txtCtg]").getText())
            //$("[id$=txtNumeroCDPManual]").val($("[id$=txtNumeroCDP]").getText())
            //$("[id$=txtNumeroCEEManual]").val($("[id$=txtNumeroCEE]").getText())

            break;
        case "Traslado de granos":
            $("[id$=btnGuardar]").enabled(true);
            $("[id$=btnSoloGuardar]").enabled(true);

            $("[id$=btnNueva]").enabled(true);
            $("[id$=btnModelo]").enabled(false);
            $("[id$=btnImprimir]").enabled(true);
            $("[id$=btnAnular]").enabled(false);
            $("[id$=btnModelo]").enabled(false);
            $("[id$=btnConfirmarArribo]").enabled(false);

            // inputs
            $("[id$=txtCtgManual]").visible(false);
            $("[id$=txtCtg]").visible(true);

            $("[id$=txtNumeroCDPManual]").visible(false);
            $("[id$=txtNumeroCDP]").visible(true);

            $("[id$=txtNumeroCEEManual]").visible(false);
            $("[id$=txtNumeroCEE]").visible(true);

            $("[id$=lblFechaVencimiento]").visible(false);
            $("[id$=txtFechaVencimiento]").visible(false);
            $("[id$=txtFechaDeEmision]").visible(true);
            $("[id$=txtTarifaReferencia]").enabled(false);
            
            break;
        case "Canje":
            $("[id$=btnGuardar]").enabled(true);
            $("[id$=btnSoloGuardar]").enabled(true);

            $("[id$=btnNueva]").enabled(true);
            $("[id$=btnModelo]").enabled(false);
            $("[id$=btnImprimir]").enabled(true);
            $("[id$=btnAnular]").enabled(false);
            $("[id$=btnModelo]").enabled(false);
            $("[id$=btnConfirmarArribo]").enabled(false);

            // inputs
            $("[id$=txtCtgManual]").visible(false);
            $("[id$=txtCtg]").visible(true);

            $("[id$=txtNumeroCDPManual]").visible(false);
            $("[id$=txtNumeroCDP]").visible(true);

            $("[id$=txtNumeroCEEManual]").visible(false);
            $("[id$=txtNumeroCEE]").visible(true);

            $("[id$=lblFechaVencimiento]").visible(false);
            $("[id$=txtFechaVencimiento]").visible(false);
            $("[id$=txtFechaDeEmision]").visible(true);
            $("[id$=txtTarifaReferencia]").enabled(false);
            
            break;
        default:
            //alert("error al seleccionar carta de porte");            
            break;
    }
    

    if (tcdp.substring(0, 5) == "Terce")
    {
        //Terceros por venta  de Granos de producción propia
        $("[id$=btnGuardar]").enabled(true); //
        $("[id$=btnSoloGuardar]").enabled(true);

        $("[id$=btnNueva]").enabled(true);
        $("[id$=btnModelo]").enabled(false);
        $("[id$=btnImprimir]").enabled(true);
        $("[id$=btnAnular]").enabled(false); // 
        $("[id$=btnModelo]").enabled(false);
        $("[id$=btnConfirmarArribo]").enabled(false);

        // inputs
        $("[id$=txtCtgManual]").visible(true); //
        $("[id$=txtCtg]").visible(false); //

        $("[id$=txtNumeroCDPManual]").visible(true); //
        $("[id$=txtNumeroCDP]").visible(false); //

        $("[id$=txtNumeroCEEManual]").visible(true); //
        $("[id$=txtNumeroCEE]").visible(false); //

        $("[id$=lblFechaVencimiento]").visible(true);
        $("[id$=txtFechaVencimiento]").visible(true);
        $("[id$=txtFechaDeEmision]").visible(true);
        $("[id$=txtTarifaReferencia]").enabled(true);


        //$("[id$=txtFechaDeEmision]").val(getHoy())
        //$("[id$=txtFechaVencimiento]").val(getHoy())
        //$("[id$=txtCtgManual]").val($("[id$=txtCtg]").getText())
        //$("[id$=txtNumeroCDPManual]").val($("[id$=txtNumeroCDP]").getText())
        //$("[id$=txtNumeroCEEManual]").val($("[id$=txtNumeroCEE]").getText())
        
    
    }

    // Particulares
    if (hiddenCambioDestino == '1') {
        $("[id$=btnGuardarCambio]").visible(true);
        $("[id$=tableCambioDestino]").visible(true);
    }

    if ($("[id$=lblMensaje]").contains("Reserva")) {
        
        $("[id$=btnGuardar]").enabled(false);
        $("[id$=btnSoloGuardar]").enabled(true);

        $("[id$=btnNueva]").enabled(false);
        $("[id$=btnModelo]").enabled(false);
        $("[id$=btnImprimir]").enabled(true);
        $("[id$=btnAnular]").enabled(false);
        $("[id$=btnModelo]").enabled(false);
        $("[id$=btnConfirmarArribo]").enabled(false);

        // inputs
        $("[id$=txtCtgManual]").visible(true)
        //$("[id$=txtCtgManual]").val($("[id$=txtCtg]").text())
        $("[id$=txtCtg]").visible(false)
        

        $("[id$=txtNumeroCDPManual]").visible(false);
        $("[id$=txtNumeroCDP]").visible(true);

        $("[id$=txtNumeroCEEManual]").visible(false);
        $("[id$=txtNumeroCEE]").visible(true);

        $("[id$=lblFechaVencimiento]").visible(true);
        $("[id$=txtFechaVencimiento]").visible(true);
        $("[id$=txtFechaDeEmision]").visible(true);
        $("[id$=txtTarifaReferencia]").enabled(true);
        
        if ($("[id$=hiddenusuariosolicitud]").val() != $("[id$=hiddenusuariologueado]").val()) {
            $("[id$=btnSoloGuardar]").enabled(false);
            $("[id$=btnImprimir]").enabled(true);
            $("[id$=txtCtgManual]").visible(false)
            $("[id$=txtCtg]").visible(true)
            $("[id$=txtTarifaReferencia]").enabled(false);            
        }
    }

    
    if (hiddenactivarModelo == 'si') {
        alert('entramos por modelo');
        $("[id$=btnGuardar]").enabled(true);
        $("[id$=btnSoloGuardar]").enabled(true);

        $("[id$=btnNueva]").enabled(false);
        $("[id$=btnModelo]").enabled(false);
        $("[id$=btnImprimir]").enabled(true);
        $("[id$=btnAnular]").enabled(false);
        $("[id$=btnModelo]").enabled(false);
        $("[id$=btnConfirmarArribo]").enabled(false);

        // inputs
        $("[id$=txtCtgManual]").visible(false);
        $("[id$=txtCtg]").visible(true);

        $("[id$=txtNumeroCDPManual]").visible(false);
        $("[id$=txtNumeroCDP]").visible(true);

        $("[id$=txtNumeroCEEManual]").visible(false);
        $("[id$=txtNumeroCEE]").visible(true);

        $("[id$=lblFechaVencimiento]").visible(false);
        $("[id$=txtFechaVencimiento]").visible(false);
        $("[id$=txtFechaDeEmision]").visible(true);
        $("[id$=txtTarifaReferencia]").enabled(false);

    }

    // Validaciones generales
    if ($.trim($("[id$=txtCtg]").getText()).length > 5 || $.trim($("[id$=txtCtgManual]").getText()).length > 5) {
        //debugger;
        $("[id$=btnGuardar]").enabled(false);
        $("[id$=btnSoloGuardar]").enabled(false);
        $("[id$=cboTipoDeCarta]").enabled(false);
        $("[id$=btnModelo]").enabled(true);
    }
    

}


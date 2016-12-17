//Funcion Javascript para el resize de la grilla

//jQuery(window).bind('resize', function() {
//    var contenedorGrilla = window.document.getElementById("tdContenedorGrilla");
//    var grilla = jQuery('#tbGrilla');

//    SetHeightAndWidthjqGridResize(contenedorGrilla, grilla);
//}).trigger('resize');


function EjecutarOnLoad() {
    SetearFocoPrimerElemento();
    return true;
}

function SetearFocoPrimerElemento() {
    var cantidadControles = 255;
    for (index = 1; index <= cantidadControles; index++) {
        var seteoFoco = true;
        var control = 'input[tabindex=' + index.toString() + ']';
        try { $(control).focus(); }
        catch (ex) { seteoFoco = false; }
        if (seteoFoco)
            break;
    }
}

function SetearActionForm(url) {
    document.forms['aspnetForm'].action = url;
}

function SetearTargetForm(target) {
    document.forms['aspnetForm'].target = target;
}

function DeshabilitarEnter(event) {
    var teclaApretada = (document.all) ? event.keyCode : event.which;
    return (teclaApretada != 13);
}

function HacerSubmit() {
    document.forms["aspnetForm"].submit();
}

function EjecutarActionResult(url) {
    window.location = url;
}

function EjecutarActionResultInNewWindow(url) {
    SetearTargetForm("_blank");
    window.location = url;
    SetearTargetForm("_self");
}

function EjecutarActionResultHaciendoSubmit(url) {
    SetearActionForm(url);
    HacerSubmit();
}

function EjecutarActionResultHaciendoSubmitInNewWindow(url) {
    SetearActionForm(url);
    SetearTargetForm("_blank");
    HacerSubmit();
    SetearTargetForm("_self");
}

function CerrarModal() {
    $.modal.close();
}

function SetHeightAndWidthjqGridResize(controlContenedorGrilla, controlGrilla) {
    if (controlContenedorGrilla != null && controlGrilla != null) {
        var widthClient = GetWidthjqGrid(controlContenedorGrilla);
        var heightClient = GetHeightjqGrid(controlContenedorGrilla);
        controlGrilla.setGridWidth(widthClient, true);
        controlGrilla.setGridHeight(heightClient);
    }
}

function GetHeightjqGrid(contenedorGrilla) {
    if (contenedorGrilla != null)
        return contenedorGrilla.clientHeight - 130;
       
    return 0;
}

function GetWidthjqGrid(contenedorGrilla) {
    if (contenedorGrilla != null)
        return contenedorGrilla.clientWidth - 35;
       
    return 0;
}

function GetImagenSiNo(valorSiNo) {
    if (valorSiNo == "Si" || valorSiNo == "SI" || valorSiNo == "si")
        return "<img alt=\"Si\" src=\"/Content/Images/icon_ok.gif\" />";
    else
        return "<img alt=\"No\" src=\"/Content/Images/icon_close.gif\" />";
}

function EjecutarModal(controlModal, widthModal) {
    $(controlModal)[0].style.width = widthModal;
    $(controlModal).modal({
        containerCss: { width: widthModal },
        onOpen: function(dialog) {
            dialog.container.show();
            dialog.data.show();
            dialog.overlay.show();
            dialog.wrap.show();
        },
        onClose: function() {
            $.modal.close();
        }
    });
    $("#simplemodal-container")[0].style.width = widthModal + 35;
    $("#simplemodal-container")[0].style.top = 225;
}

function EjecutarModalBorrar() {
    EjecutarModal("#ModalBorrar", 180);
}

function EjecutarModalSalirSistema() {
    EjecutarModal("#ModalSalirSistema", 180);
}

function Borrar(url) {
    var seGeneroClickBorrar = jQuery("#SeGeneroClickBorrar").val();
    if (seGeneroClickBorrar != "true") {
        jQuery("#SeGeneroClickBorrar")[0].value = true;
        jQuery("#btBorrarYes").click(function() {
            EjecutarActionResult(url);
        });
    }
    EjecutarModalBorrar();
}

function BorrarHaciendoSubmit(url) {
    var seGeneroClickBorrar = jQuery("#SeGeneroClickBorrar").val();
    if (seGeneroClickBorrar != "true") {
        jQuery("#SeGeneroClickBorrar")[0].value = true;
        jQuery("#btBorrarYes").click(function() {
            EjecutarActionResultHaciendoSubmit(url);
        });
    }
    EjecutarModalBorrar();
}

function Grabar() {
    HacerSubmit();
}

function ActivarFilterToolbarGrilla(controlGrilla, searchEnter) {
    jQuery(controlGrilla).jqGrid('filterToolbar', { stringResult: true, searchOnEnter: searchEnter });
}

function ActivarPagerGrilla(controlGrilla, controlPagerGrilla) {
    jQuery(controlGrilla).jqGrid('navGrid', controlPagerGrilla, {
        refresh: true,
        add: false,
        edit: false,
        del: false,
        search: false,
        view: false
    }, {}, {}, {}, {}, {});
}

function ActivarBotonColumnasGrilla(controlGrilla, controlPagerGrilla) {
    jQuery(controlGrilla).jqGrid('navButtonAdd', controlPagerGrilla, {
        caption: 'Columnas',
        title: 'Mostrar/Ocultar Columnas',
        onClickButton: function() {
            jQuery(controlGrilla).setColumns({
                caption: 'Mostrar/Ocultar Columnas',
                colnameview: false,
                bSubmit: 'OK',
                bCancel: 'Salir',
                closeAfterSubmit: false,
                updateAfterCheck: true
            });
        }
    });
}

function SetIdsSeleccionadosGrilla(controlGrilla, controlId) {
    var idsGrilla = jQuery(controlGrilla).jqGrid('getDataIDs');
    var ids = "";
    var idsAExcluir = "";

    for (var i = 0; i < idsGrilla.length; i++) {
        if (jQuery("#jqg_" + controlGrilla.substring(1, controlGrilla.length) + "_" + idsGrilla[i])[0].checked) {
            if (ids != "")
                ids = ids + ",";
            ids = ids + idsGrilla[i];
        }
        else {
            if (idsAExcluir != "")
                idsAExcluir = idsAExcluir + ",";
            idsAExcluir = idsAExcluir + idsGrilla[i];
        }
    }

    var idsSeleccionados = jQuery(controlId)[0].value.split(',');
    for (var indexId in idsSeleccionados) {
        var id = idsSeleccionados[indexId];
        if (id != "") {
            var excluir = false;
            var idsExcluidos = idsAExcluir.split(',');
            for (var indexIdExcluido in idsExcluidos)
                if (idsExcluidos[indexIdExcluido] == id) {
                excluir = true;
                break;
            }
            if (!excluir) {
                if (ids != "")
                    ids = ids + ",";
                ids = ids + id;
            }
        }
    }

    jQuery(controlId)[0].value = ids;
}

function ActivarMascara(controlFecha, mascara) {
    jQuery(function($) { $(controlFecha).mask(mascara, { placeholder: " " }); });
}

function ActivarDatePicker(controlFecha) {
    $(function() {
        $(controlFecha).datepicker({
            dateFormat: 'dd/mm/yy ',
            altFormat: 'dd/mm/yy ',
            nextText: 'Próximo',
            prevText: 'Anterior',
            monthNamesShort: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
            monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
            dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sa'],
            dayNamesShort: ['Domingo', 'Lunes', 'Martes', 'Miercoles', 'Jueves', 'Viernes', 'Sabado'],
            dayNames: ['Domingo', 'Lunes', 'Martes', 'Miercoles', 'Jueves', 'Viernes', 'Sabado'],
            currentText: 'Hoy',
            closeText: 'Salir',
            buttonText: 'Ver calendario',
            showButtonPanel: true,
            buttonImage: "/Content/Images/Calendar.gif",
            buttonImageOnly: true,
            showOn: 'both',
            changeYear: true,
            changeMonth: true,
            isRTL: false,
            showMonthAfterYear: false,
            yearSuffix: ''
        });
    });
}

function ActivarMascaraFecha(controlFecha) {
    ActivarMascara(controlFecha, "99/99/9999");
}

function ActivarMascaraNumero(controlNumero, negativo) {
    jQuery(function($) { $(controlNumero).numeric({ decimal: "." }); });
    jQuery(function($) { $(controlNumero).numeric({ negative: negativo }); });
}
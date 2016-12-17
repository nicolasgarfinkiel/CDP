(function($) {

    $.fn.visible = function(valor) {
        return this.each(function() {
            if (valor)
                $(this).css("visibility", "visible");
            else
                $(this).css("visibility", "hidden");
        });
    };

    $.fn.enabled = function(valor) {
        return this.each(function() {
            $(this).prop("disabled", !valor);
        });
    };

    $.fn.IsNullOrEmpty = function() {
        return this.each(function() {
            if ($(this).length > 0) {
                if ($.trim($(this).getText()).length > 0) {
                    return false;
                }
                else {
                    return true;
                }

            } else {
                return false;
            }
        });
    };

    $.fn.contains = function(valor) {

        if ($(this).length > 0) {
            if ($(this).getText().indexOf(valor) != -1) {
                return true;
            }
            else {
                return false;
            }
        } else {
            return false
        }



    };

    $.fn.extend({
        getText: function() {
            if ($(this).length > 0) {
                var texto;

                if ($(this)[0].tagName == 'INPUT') {
                    return $.trim($(this).val())
                } else {

                    return $.trim($(this).text())
                }
            }

        }
    });

    $.fn.setText = function(valor) {
        // ???? Mejorarlo para no setear "val" y "text" todas las veces.
        $(this).val(valor)
        $(this).text(valor)         
    };


} (jQuery));



function getHoy() {

    var date = new Date();
    var d = date.getDate();
    var m = date.getMonth() + 1;
    var y = date.getFullYear();
    return (d <= 9 ? '0' + d : d) + '/' + (m <= 9 ? '0' + m : m) + '/' + y;

}



$(document).ready(function() {


    $('input:radio[name=ctl00$ContentPlaceHolder1$rblFletePagadoAPagar]').change(function () {
        if (this.value == 'Flete Pagado') {
            $("[id$=cboClientePagadorDelFlete]").text("");
            $("[id$=hbClientePagadorDelFlete]").val("");
            $("[id$=ImageButtonDeletePagadorFlete]").visible(false);
            $("[id$=ImageButtonClientePagadorDelFlete]").enabled(true);
            $("[id$=ImageButtonClientePagadorDelFlete]").visible(true);
        }
        else if (this.value == 'Flete a Pagar') {
            $("[id$=cboClientePagadorDelFlete]").text($('[id$=hbClientePagadorDefaultName]').val());
            $("[id$=hbClientePagadorDelFlete]").val($('[id$=hbClientePagadorDefaultId]').val());
            $("[id$=ImageButtonDeletePagadorFlete]").visible(false);
            $("[id$=ImageButtonClientePagadorDelFlete]").enabled(false);
            $("[id$=ImageButtonClientePagadorDelFlete]").visible(false);
        }
    });



    $("[id$=txtLocalidad1]").keyup(function() {
        $("[id$=txtLocalidad1]").autocomplete({
            source: "localidades.ashx?q=" + $(this).val(),
            minLength: 1,
            select: function(event, ui) {
            }
        });
    });

    $("[id$=txtLocalidad2]").keyup(function() {
        $("[id$=txtLocalidad2]").autocomplete({
            source: "localidades.ashx?q=" + $(this).val(),
            minLength: 1,
            select: function(event, ui) {
            }
        });
    });

    $("[id$=txtCuitProveedorTitularCartaDePorte]").keyup(function() {
        $("[id$=txtCuitProveedorTitularCartaDePorte]").autocomplete({
            source: "CuitHandler.ashx?campo=idProveedorTitularCartaDePorte&dato=" + $(this).val(),
            minLength: 1,
            select: function(event, ui) {
                txtCuitClienteDestinatariochange('txtCuitProveedorTitularCartaDePorte');
            }
        });
    });
    $("[id$=txtCuitClienteIntermediario]").keyup(function() {
        $("[id$=txtCuitClienteIntermediario]").autocomplete({
            source: "CuitHandler.ashx?campo=IdClienteIntermediario&dato=" + $(this).val(),
            minLength: 1,
            select: function(event, ui) {
                txtCuitClienteDestinatariochange('txtCuitClienteIntermediario');
            }
        });
    });

    $("[id$=txtCuitDestinoCambio]").keyup(function() {
        $("[id$=txtCuitDestinoCambio]").autocomplete({
            source: "CuitHandler.ashx?campo=CuitEstablecimientoDestinoCambio&dato=" + $(this).val(),
            minLength: 1,
            select: function(event, ui) {
                txtCuitClienteDestinatariochange('txtCuitDestinoCambio');
            }
        });
    });


    $("[id$=txtCuitClienteRemitenteComercial]").keyup(function() {
        $("[id$=txtCuitClienteRemitenteComercial]").autocomplete({
            source: "CuitHandler.ashx?campo=IdClienteRemitenteComercial&dato=" + $(this).val(),
            minLength: 1,
            select: function(event, ui) {
                txtCuitClienteDestinatariochange('txtCuitClienteRemitenteComercial');
            }

        });
    });


    $("[id$=txtCuitClienteCorredor]").keyup(function() {
        $("[id$=txtCuitClienteCorredor]").autocomplete({
            source: "CuitHandler.ashx?campo=IdClienteCorredor&dato=" + $(this).val(),
            minLength: 1,
            select: function(event, ui) {
                txtCuitClienteDestinatariochange('txtCuitClienteCorredor');
            }
        });
    });

    $("[id$=txtCuitClienteEntregador]").keyup(function() {
        $("[id$=txtCuitClienteEntregador]").autocomplete({
            source: "CuitHandler.ashx?campo=IdClienteEntregador&dato=" + $(this).val(),
            minLength: 1,
            select: function(event, ui) {
                txtCuitClienteDestinatariochange('txtCuitClienteEntregador');
            }
        });
    });


    $("[id$=txtCuitClienteDestinatario]").keyup(function() {
        $("[id$=txtCuitClienteDestinatario]").autocomplete({
            source: "CuitHandler.ashx?campo=IdClienteDestinatario&dato=" + $(this).val(),
            minLength: 1,
            select: function(event, ui) {
                txtCuitClienteDestinatariochange('txtCuitClienteDestinatario');
            }
        });
    });


    $("[id$=txtCuitClienteDestino]").keyup(function() {
        $("[id$=txtCuitClienteDestino]").autocomplete({
            source: "CuitHandler.ashx?campo=IdClienteDestino&dato=" + $(this).val(),
            minLength: 1,
            select: function(event, ui) {
                txtCuitClienteDestinatariochange('txtCuitClienteDestino');
            }
        });
    });

    $("[id$=txtCuitProveedorTransportista]").keyup(function() {
        $("[id$=txtCuitProveedorTransportista]").autocomplete({
            source: "CuitHandler.ashx?campo=IdProveedorTransportista&dato=" + $(this).val(),
            minLength: 1,
            select: function(event, ui) {
                txtCuitClienteDestinatariochange('txtCuitProveedorTransportista');
            }
        });
    });


    $("[id$=txtCuitChofer]").keyup(function() {
        $("[id$=txtCuitChofer]").autocomplete({
            source: "CuitHandler.ashx?campo=IdChofer&dato=" + $(this).val(),
            minLength: 1,
            select: function(event, ui) {
                txtCuitClienteDestinatariochange('txtCuitChofer');
            }
        });
    });


    $("[id$=txtCuitProductor]").keyup(function() {
        $("[id$=txtCuitProductor]").autocomplete({
            source: "CuitHandler.ashx?campo=IdChofer&dato=" + $(this).val(),
            minLength: 1,
            select: function(event, ui) {
                txtCuitClienteDestinatariochange('txtCuitProductor');
            }
        });
    });

    $("[id$=txtCuitLaboratorio]").keyup(function() {
        $("[id$=txtCuitLaboratorio]").autocomplete({
            source: "CuitHandler.ashx?campo=IdChofer&dato=" + $(this).val(),
            minLength: 1,
            select: function(event, ui) {
                txtCuitClienteDestinatariochange('txtCuitLaboratorio');
            }
        });
    });





    $("[id$=txtLocalidad1]").focus(function() {
        $(this).select();
    }).blur(function() {

        $.getJSON("localidades.ashx?p=" + $(this).val(), function(result) {
            var isValido = false;

            $.each(result, function(i, field) {
                isValido = true;
                $("[id$=txtLocalidad1]").css('cssText', 'color:#000000;width:300px;');
            });

            if (!isValido) {
                $("[id$=txtLocalidad1]").css('cssText', 'color:#f00;width:300px;');
                //$("[id$=txtLocalidad1]").focus();
                //$("[id$=txtLocalidad1]").select();
            }

        });


    });


    $("[id$=txtLocalidad2]").focus(function() {
        $(this).select();
    }).blur(function() {
        $.getJSON("localidades.ashx?p=" + $(this).val(), function(result) {
            var isValido = false;

            $.each(result, function(i, field) {
                isValido = true;
                $("[id$=txtLocalidad2]").css('cssText', 'color:#000000;width:300px;');
            });

            if (!isValido) {
                $("[id$=txtLocalidad2]").css('cssText', 'color:#f00;width:300px;');
                //$("[id$=txtLocalidad2]").focus();
                //$("[id$=txtLocalidad2]").select();
            }

        });


    });










});


$(function() {
    $("[id$=txtFechaCierre]").datepicker({
        showOn: 'button',
        buttonImageOnly: true,
        dateFormat: "dd/mm/yy",
        buttonImage: 'Content/Images/Calendar.gif'
    });
});


$(function() {
    $("[id$=txtFechaAnalisis]").datepicker({
        showOn: 'button',
        buttonImageOnly: true,
        dateFormat: "dd/mm/yy",
        buttonImage: 'Content/Images/Calendar.gif'
    });
});

$(function() {
    $("[id$=txtFechaVencimientoC1116A]").datepicker({
        showOn: 'button',
        buttonImageOnly: true,
        dateFormat: "dd/mm/yy",
        buttonImage: 'Content/Images/Calendar.gif'
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
$(function() {
    $("[id$=txtFechaDeDescarga]").datepicker({
        showOn: 'button',
        buttonImageOnly: true,
        dateFormat: "dd/mm/yy",
        buttonImage: 'Content/Images/Calendar.gif'
    });
});

$(function() {
    $("[id$=txtFechaDeArribo]").datepicker({
        showOn: 'button',
        buttonImageOnly: true,
        dateFormat: "dd/mm/yy",
        buttonImage: 'Content/Images/Calendar.gif'
    });
});


function txtCuitClienteDestinatariochange(control) {
    $("[id$=" + control + "]").validocuit();

}

$.fn.validocuit = function() {
   
    if ($(this)[0].value.length != 11) {
        var idstatus = this.name + "1";
        $(idstatus).visible(true);
        //document.getElementById($(this)[0].id + "1").style.display = "block";
        //document.getElementById($(this)[0].id + "1").style.color = "#ccc";
        return false;

    } else {

        var mult = [5, 4, 3, 2, 7, 6, 5, 4, 3, 2, 1];
        var nums = $(this)[0].value.split("");

        var total = 0;
        for (i = 0; i < mult.length; i++) {
            total += nums[i] * mult[i];
        }
        
        document.getElementById($(this)[0].id + "1").style.display = "block";
        document.getElementById($(this)[0].id + "1").style.color = "#FF0000";
        document.getElementById($(this)[0].id).style.color = "#FF0000";

        var resto = total % 11;
        if (resto == 0) {
            document.getElementById($(this)[0].id + "1").style.color = "#0AFF0A";
            document.getElementById($(this)[0].id).style.color = "#000000";
        }
    }
};

$.fn.cuitvalido = function() {

    if ($(this)[0].value.length != 11) {
        var idstatus = this.name + "1";
        $(idstatus).visible(true);
        return false;

    } else {

        var mult = [5, 4, 3, 2, 7, 6, 5, 4, 3, 2, 1];
        var nums = $(this)[0].value.split("");

        var total = 0;
        for (i = 0; i < mult.length; i++) {
            total += nums[i] * mult[i];
        }
        var resto = total % 11;
        return resto == 0;
    }
};





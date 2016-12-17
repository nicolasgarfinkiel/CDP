//-----------------------------------//
// Fuerza la recarga de la pagina al retroceder (back)
//-----------------------------------//
onload = function () {
    var e = document.getElementById("refreshed");
    if (e.value == "no") e.value = "yes";
    else { e.value = "no"; location.reload(); }
}
//-----------------------------------//


$(document).ready(function () {

    var hiddenTab = $("[id$=hiddenTab]").val();
    var emptyHidden = (hiddenTab == "") ? true : false;

    $('#divNav').each(function () {
        $(this).find('li').each(function () {
            var current = $(this);
            if (current.text() == hiddenTab) {
                current.find('a').each(function () {
                    var aCur = $(this);
                    aCur.css("background", "#DAE0C7");
                });
            }
        });
    });


});

$(function () {
    $(".mymenu").jRMenuMore();
});

$("#Navigation li").live('click', function (e) {

    e.preventDefault;
    $(this).blur();

});




function menu_OnClick(page) {

    var url = siteRoot + "/Administracion.aspx/SetLastPage";

    $.ajax({
        url: url,
        type: "POST",
        data: '{page: "' + page + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            alert(response.d);
        },
        success: function (response) {
        },
    });

    $(".mainContent").toggle("slow", function () {
        $(location).attr('href', siteRoot + '/' + page);
    });
}



function cboEmpresa_OnChange(target) {

    var url = siteRoot + "/Administracion.aspx/SetEmpresa";

    var empresaId = target.value;

    $.ajax({
        url: url,
        type: "POST",
        data: '{empresaId: "' + empresaId + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        error: function (response) {
            alert('Error: ' + response);
        },
        success: function (response) {
            var url = siteRoot + "/" + response.d;
  
            $(".mainContent").toggle("slow", function () {
                $(location).attr('href', url);
            });
        },
    });

}
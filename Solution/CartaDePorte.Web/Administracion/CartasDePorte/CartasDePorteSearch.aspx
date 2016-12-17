<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CartasDePorteSearch.aspx.cs" Inherits="CartaDePorte.Web.CartasDePorteSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1 {
            width: 100%;
            border: 1px solid #C0C0C0;
        }

        .style2 {
            width: 100px;
        }

        .style3 {
        }

        .style4 {
            width: 126px;
        }

        .style5 {
            width: 179px;
        }

        .style6 {
            width: 94px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">



        function EliminarLote(idLoteCartasDePorte, desde, hasta, disponible) {

            if (disponible > 0) {
                var eliminardesde = (hasta - disponible) + 1;
                var msj = 'El lote ' + idLoteCartasDePorte + ', no fue utilizado por lo tanto sera eliminado. Items no utilizados,<br />desde el número ' + eliminardesde + ', <br />hasta el número ' + hasta;

                $('<div></div>').appendTo('body')
                .html('<div><h4>' + msj + '.<br />¿ Desea continuar ?</h4></div>')
                .dialog({
                    modal: true,
                    title: 'Eliminar Lote',
                    zIndex: 10000,
                    autoOpen: true,
                    width: 'auto',
                    resizable: false,
                    buttons: {
                        Si: function () {
                            $(this).dialog("close");
                            EliminarNoUtilizadosEjecutar(idLoteCartasDePorte);
                        },
                        No: function () {
                            $(this).dialog("close");
                        }
                    },
                    close: function (event, ui) {
                        $(this).remove();
                    }
                });
            }

        }


        function EliminarNoUtilizados(idLoteCartasDePorte, desde, hasta, disponible) {

            if (disponible > 0) {
                var eliminardesde = (hasta - disponible) + 1;
                var msj = 'Se eliminarán ' + disponible + ' items no utilizados,<br />desde el número ' + eliminardesde + ', <br />hasta el número ' + hasta;

                $('<div></div>').appendTo('body')
                .html('<div><h4>' + msj + '.<br />¿ Desea continuar ?</h4></div>')
                .dialog({
                    modal: true,
                    title: 'Liberar números no utilizados',
                    zIndex: 10000,
                    autoOpen: true,
                    width: 'auto',
                    resizable: false,
                    buttons: {
                        Si: function () {
                            $(this).dialog("close");
                            EliminarNoUtilizadosEjecutar(idLoteCartasDePorte);
                        },
                        No: function () {
                            $(this).dialog("close");
                        }
                    },
                    close: function (event, ui) {
                        $(this).remove();
                    }
                });
            }

        }


        function EliminarNoUtilizadosEjecutar(idLoteCartasDePorte) {

            var url = 'CartasDePorteSearch.aspx?opcion=ELIMINARDISPONIBLES&idLoteCartasDePorte=' + idLoteCartasDePorte;

            $.ajax({
                async: false,
                cache: false,
                type: "POST",
                url: url,
                data: '',
                contentType: "application/json; charset=utf-8",
                dataType: 'text json',
                success: EliminarNoUtilizadosReturn,
                error: EliminarNoUtilizadosError,
            });

        }


        function EliminarNoUtilizadosReturn(result) {

            $('<div></div>').appendTo('body')
            .html('<div><h4>Operación exitosa!</h4></div>')
            .dialog({
                modal: true,
                title: 'Liberar números no utilizados',
                zIndex: 10000,
                autoOpen: true,
                width: 'auto',
                resizable: false,
                buttons: {
                    Aceptar: function () {
                        $(this).dialog("close");
                        $(".Button2").click();
                    },
                },
                close: function (event, ui) {
                    $(this).remove();
                }
            });

        }

        function EliminarNoUtilizadosError(xhr, errorType, exception) {

            $('<div></div>').appendTo('body')
            .html('<div><h4>' + xhr.responseText + '</h4></div>')
            .dialog({
                modal: true,
                title: 'Liberar números no utilizados',
                zIndex: 10000,
                autoOpen: true,
                width: '600px',
                resizable: false,
                buttons: {
                    Aceptar: function () {
                        $(this).dialog("close");
                    },
                },
                close: function (event, ui) {
                    $(this).remove();
                }
            });

        }
    </script>

    <br />
    <br />
    <div class="divTopPage4">
    </div>
    <br />
    <asp:Label ID="Label3" runat="server" Text="Buscador de Lotes de Cartas de Porte" CssClass="TituloReporte"></asp:Label>
    <br />
    <br />

    <table class="style1">
        <tr>
            <td class="style4">
                <asp:Label ID="Label4" runat="server" Text="Número Desde"></asp:Label>
            </td>
            <td class="style5">
                <asp:TextBox ID="txtLoteDesde" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td class="style3" style="text-align:center">
                <asp:CheckBox ID="chkTienedisponible" runat="server" Text="Solo con números disponibles" Checked="true" />
            </td>
            <td class="style6" style="width: 20%;text-align:right">
                <asp:Button ID="Button2" runat="server" Text="Buscar" Width="150px" OnClick="Button2_Click" CssClass="Button2" />
            </td>
            <td class="style6">
                <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" OnClick="btnNuevo_Click" />
            </td>
        </tr>
    </table>

    <br />
    <br />
    <div id="divTable" style="height: 400px; width: 100%; overflow: auto;">
        <asp:Table ID="tblData" runat="server">
        </asp:Table>
    </div>
</asp:Content>

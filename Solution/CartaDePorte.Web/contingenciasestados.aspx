<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="contingenciasestados.aspx.cs" Inherits="CartaDePorte.Web.contingenciasestados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/jscript" language="javascript">

        function ValidarGuardarCambioDesvio() {
            return window.confirm("Los unicos datos que se guardaran son los correspondientes al cuadro 6.Cambio de descarga / desvio")
        }


        function ValidarAnular() {
            return window.confirm("¿ Desea anular la Carta de Porte ?")
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



    </script>

    <style type="text/css">
        .style1 {
            width: 95%;
        }

        .style3 {
        }

        .style58 {
            width: 817px;
        }

        .tabla1 {
            width: 100%;
            border-color: 1;
            border-style: solid;
            border-width: thin;
        }

        .SubTitulos {
            font-family: Arial, Helvetica, sans-serif;
            font-size: small;
            font-weight: bold;
        }

        .SubTitulosPrincipal {
            font-family: Arial, Helvetica, sans-serif;
            font-size: larger;
            font-weight: bold;
        }

        .style75 {
            width: 629px;
        }

        .style76 {
            width: 865px;
        }

        .style80 {
            width: 53px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <script type="text/javascript">

        $(function () {
            $("[id$=txtFechaVencimiento]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                dateFormat: "dd/mm/yy",
                buttonImage: 'Content/Images/Calendar.gif'
            });
        });
    </script>

    <table style="width: 956px">
        <tr>
            <td class="style76">
                <table align="right" style="width: 78px">
                    <tr>
                        <td class="style80"></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                </table>
            </td>
            <td class="style75">&nbsp;
            </td>
        </tr>
        <tr>
            <td style="font-family: Arial, Helvetica, sans-serif; font-size: large" class="style76">
                <asp:Label ID="lblTituloSeccion6" runat="server" Text="Carta de Porte" CssClass="SubTitulosPrincipal"></asp:Label>
            </td>
            <td style="font-family: Arial, Helvetica, sans-serif; font-size: large" class="style75">&nbsp;
            </td>
        </tr>
    </table>
    <table class="tabla1">
        <tr>
            <td align="left" class="style3">
                <asp:Label ID="lblCtg" runat="server" Text="Ctg"></asp:Label>
            </td>
            <td align="left" style="margin-left: 200px">
                <asp:UpdatePanel ID="UpdatePanetxtCtgManual" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:TextBox ID="txtCtgManual" runat="server" Width="150px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td align="center">&nbsp;
            </td>
            <td>Fecha De Vencimiento</td>
            <td align="center">&nbsp;</td>
            <td align="left">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:TextBox ID="txtFechaVencimiento" runat="server" Width="93px"
                            Visible="false"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </td>
            <td align="center">&nbsp;</td>
        </tr>
        <tr>
            <td align="left" class="style3">
                <asp:Label ID="Label29" runat="server" Text="Estado En SAP"></asp:Label>
            </td>
            <td align="left" style="margin-left: 200px">
                <asp:DropDownList ID="cboEstadoEnSAP" runat="server" Height="21px" Style="margin-left: 0px"
                    Width="300px">
                </asp:DropDownList>
            </td>
            <td align="center">&nbsp;
            </td>
            <td>
                <asp:Label ID="Label30" runat="server" Text="Estado En AFIP"></asp:Label>
            </td>
            <td align="center" colspan="3">
                <asp:DropDownList ID="cboEstadoEnAFIP" runat="server" Height="21px" Style="margin-left: 0px"
                    Width="300px">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <table class="style1">
        <tr>
            <td>&nbsp;
            </td>
            <td class="style58">&nbsp;
            </td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td class="style58">
                <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td class="style58" align="right">
                <asp:HiddenField ID="hbBuscador" runat="server" />
            </td>
            <td align="center">
                <asp:UpdatePanel ID="savepanel" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Button ID="btnGuardar" runat="server" Text="Guardar solamente" OnClick="btnGuardar_Click1" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdateProgress ID="UpdateProgressProcesoSolicitud" runat="server" AssociatedUpdatePanelID="savepanel">
                            <ProgressTemplate>
                                <div style="position: fixed; top: 0px; bottom: 0px; left: 0px; right: 0px; overflow: hidden; padding: 0; margin: 0; background-color: #000000; filter: alpha(opacity=50); opacity: 0.5; z-index: 100000;">
                                </div>
                                <div style="position: fixed; top: 40%; left: 40%; height: 20%; width: 20%; z-index: 100001; background-color: #FFFFFF; border: 2px solid #000000; background-image: url('Content/Images/ajax_loader_large.gif'); background-size: 32px 32px; background-repeat: no-repeat; background-position: center;">
                                    <center>
                                        <br />
                                        Procesando Solicitud...</center>
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>

</asp:Content>

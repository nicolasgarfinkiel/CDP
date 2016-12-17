<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="IndexManual.aspx.cs" Inherits="CartaDePorte.Web.IndexManual" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(function () {
            $("[id$=txtFechaDeEmision]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                dateFormat: "dd/mm/yy",
                buttonImage: 'Content/Images/Calendar.gif'
            });
        });
        $(function () {
            $("[id$=txtFechaVencimiento]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                dateFormat: "dd/mm/yy",
                buttonImage: 'Content/Images/Calendar.gif'
            });
        });




    </script>

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
        .contenedordiv {
            z-index: 100;
            position: relative;
        }

        #watermark {
            color: #f1f1f1;
            font-size: 100pt;
            -webkit-transform: rotate(-45deg);
            -moz-transform: rotate(-45deg);
            position: absolute;
            width: 100%;
            height: 100%;
            margin: 0;
            z-index: 10;
            vertical-align: middle;
        }

        .styleAltaRapida {
            display: block;
            padding: 2px 2px 2px 2px;
            color: #000000;
            text-decoration: none;
            background: #F2F2F2;
        }

        .style1 {
            width: 95%;
        }

        .style3 {
        }

        .style5 {
            width: 173px;
        }

        .style6 {
            width: 30px;
        }

        .style7 {
            height: 7px;
        }

        .style9 {
            width: 31px;
            height: 7px;
        }

        .style10 {
            width: 173px;
            height: 7px;
        }

        .style11 {
        }

        .style13 {
            width: 31px;
        }

        .style14 {
            width: 189px;
            margin-left: 80px;
        }

        .style15 {
            width: 189px;
            height: 7px;
        }

        .style16 {
            width: 33px;
        }

        .style28 {
        }

        .style29 {
            width: 268435408px;
        }

        .style33 {
        }

        .style35 {
        }

        .style39 {
        }

        .style40 {
            width: 671px;
        }

        .style41 {
            width: 238px;
        }

        .style42 {
        }

        .style44 {
            width: 25px;
        }

        .style50 {
            width: 157px;
        }

        .style52 {
            width: 201px;
        }

        .style53 {
        }

        .style54 {
            width: 81px;
        }

        .style55 {
        }

        .style56 {
            width: 121px;
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

        .style59 {
            width: 26px;
        }

        .style60 {
            width: 440px;
        }

        .style63 {
        }

        .style64 {
            width: 328px;
        }

        .style65 {
        }

        .style68 {
            width: 95px;
        }

        .style69 {
            width: 210px;
        }

        .style72 {
            width: 74px;
        }

        .style73 {
            width: 190px;
        }

        .style74 {
            width: 52px;
        }

        .style75 {
            width: 629px;
        }

        .style76 {
            width: 865px;
        }

        .style77 {
            width: 65px;
        }

        .style78 {
            width: 155px;
        }

        .style79 {
            width: 69px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>


    <div id="watermark">
        <p>Carga Manual<br>
            Carga Manual<br>
            Carga Manual<br>
        </p>
    </div>
    <br />
    <br />
    <div class="divTopPage1">
    </div>
    <br />
    <div class="contenedordiv">
        <table style="width: 956px">
            <tr>
                <td class="style76"></td>
                <td class="style75">&nbsp;
                </td>
            </tr>
            <tr>
                <td style="font-family: Arial, Helvetica, sans-serif; font-size: large" class="style76">
                    <asp:Label ID="lblTituloSeccion6" runat="server" Text="Solicitud de Carta de Porte"
                        CssClass="SubTitulosPrincipal"></asp:Label>
                </td>
                <td style="font-family: Arial, Helvetica, sans-serif; font-size: large" class="style75">
                    <table align="right">
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="savepanelSoloGuardar" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="btnSoloGuardar" runat="server" Text="Solo Guardar" Width="110px"
                                            OnClick="btnSoloGuardar_Click" />
                                        <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="savepanelSoloGuardar">
                                            <ProgressTemplate>
                                                <div style="position: fixed; top: 0px; bottom: 0px; left: 0px; right: 0px; overflow: hidden; padding: 0; margin: 0; background-color: #000000; filter: alpha(opacity=50); opacity: 0.5; z-index: 100000;">
                                                </div>
                                                <div style="position: fixed; top: 40%; left: 40%; height: 20%; width: 20%; z-index: 100001; background-color: #FFFFFF; border: 2px solid #000000; background-image: url('Content/Images/ajax_loader_large.gif'); background-size: 32px 32px; background-repeat: no-repeat; background-position: center;">
                                                    <center>
                                                        <br />
                                                        Procesando solo guardar...</center>
                                                </div>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <div class="contenedordiv">
        <table class="tabla1">
            <tr>
                <td colspan="7">&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTipoDeCarta" runat="server" Text="Tipo Carta Porte"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="cboTipoDeCartam" runat="server" Height="21px" Style="margin-left: 0px"
                        Width="300px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label ID="lblFechaDeEmision" runat="server" Text="Fecha De Emision"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboTipoDeCartam" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox ID="txtFechaDeEmision" runat="server" Width="80"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboTipoDeCartam" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label ID="lblFechaVencimiento" runat="server" Text="Fecha De Vencimiento"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboTipoDeCartam" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox ID="txtFechaVencimiento" runat="server" Width="80"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboTipoDeCartam" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="center" class="style3" colspan="7">&nbsp;
                </td>
            </tr>
            <tr>
                <td align="left" class="style3">
                    <asp:Label ID="lblCtg" runat="server" Text="Ctg"></asp:Label>
                </td>
                <td align="left" style="margin-left: 200px">
                    <asp:UpdatePanel ID="UpdatePanetxtCtgManual" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox ID="txtCtgManual" runat="server" Width="150px"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboTipoDeCartam" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="center">
                    <asp:Label ID="Label17" runat="server" Text="Carta De Porte"></asp:Label>
                </td>
                <td></td>
                <td align="center">
                    <asp:UpdatePanel ID="UpdatePaneLNumeroCDP" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox ID="txtNumeroCDPManual" runat="server" Width="100px"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboTipoDeCartam" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="center">
                    <asp:Label ID="Label18" runat="server" Text="CEE"></asp:Label>
                </td>
                </td>
            <td align="center">
                <asp:UpdatePanel ID="UpdatePanelNumeroCEE" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:TextBox ID="txtNumeroCEEManual" runat="server" Width="100px"></asp:TextBox>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cboTipoDeCartam" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <div class="contenedordiv">
        <table class="tabla1" id="tableCarga1" runat="server" visible="true">
            <tr>
                <td align="center" class="style3" colspan="7">
                    <asp:Label ID="lblTituloSeccion" runat="server" Text="CARTA DE PORTE PARA EL TRANSPORTE AUTOMOTOR DE GRANOS"
                        CssClass="SubTitulos"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" class="style3" colspan="7">
                    <table class="style1">
                        <tr>
                            <td class="style6">1
                            </td>
                            <td align="left">
                                <asp:Label ID="lblTituloSeccion1" runat="server" Text="DATOS DE INTERVINIENTES EN EL TRASLADO DE GRANOS"
                                    CssClass="SubTitulos"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style14">
                    <asp:Label ID="lblProveedorTitularCartaDePorte" runat="server" Text="Titular Carta Porte"></asp:Label>
                </td>
                <td>
                    <asp:ImageButton ID="ImageButtonDeleteTitularCartaPorte" runat="server" ImageUrl="~/Content/Images/iconotrash.png"
                        Height="12px" Width="12px" Visible="False" OnClick="ImageButtonDeleteTitularCartaPorte_Click" />
                </td>
                <td>
                    <asp:Label ID="cboProveedorTitularCartaDePorte" runat="server" Text="" BorderColor="Silver"
                        BorderStyle="Solid" BorderWidth="1px" Height="20px" Width="300px"></asp:Label>
                </td>
                <td class="style13">
                    <asp:ImageButton ID="ImageButtonProveedorTitularCartaDePorte" runat="server" ImageUrl="~/Content/Images/magnify.gif"
                        OnClick="ImageButtonProveedorTitularCartaDePorte_Click" />
                </td>
                <td>
                    <asp:HiddenField ID="hbProveedorTitularCartaDePorte" runat="server" />
                </td>
                <td class="style5">
                    <asp:Label ID="lblCuit1" runat="server" Text="CUIT Nro"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtCuitProveedorTitularCartaDePorte" runat="server" BorderColor="Silver"
                        BorderStyle="Solid" BorderWidth="1px" Height="20px" Width="200px"></asp:Label>
                </td>
                <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    </td>
                </ContentTemplate>
            </asp:UpdatePanel>--%>
            </tr>
            <tr>
                <td class="style14">
                    <asp:Label ID="lblProveedorTitularCartaDePorte0" runat="server" Text="Intermediario"></asp:Label>
                </td>
                <td>
                    <asp:ImageButton ID="ImageButtonDeleteIntermediario" runat="server" ImageUrl="~/Content/Images/iconotrash.png"
                        Visible="False" Height="12px" Width="12px" OnClick="ImageButtonDeleteIntermediario_Click" />
                </td>
                <td>
                    <asp:Label ID="cboClienteIntermediario" runat="server" BorderColor="Silver" BorderStyle="Solid"
                        BorderWidth="1px" Height="20px" Width="300px"> </asp:Label>
                </td>
                <td class="style13">
                    <asp:ImageButton ID="ImageButtonClienteIntermediario" runat="server" ImageUrl="~/Content/Images/magnify.gif"
                        OnClick="ImageButtonClienteIntermediario_Click" />
                    &nbsp;
                </td>
                <td class="style13">
                    <asp:HiddenField ID="hbClienteIntermediario" runat="server" />
                </td>
                <td class="style5">
                    <asp:Label ID="lblCuit2" runat="server" Text="CUIT Nro"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtCuitClienteIntermediario" runat="server" BorderColor="Silver" BorderStyle="Solid"
                        BorderWidth="1px" Height="20px" Width="200px"></asp:Label>
                </td>
                <%--            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    </td>
                </ContentTemplate>
            </asp:UpdatePanel>
                --%>
            </tr>
            <tr>
                <td class="style14">
                    <asp:Label ID="lblProveedorTitularCartaDePorte1" runat="server" Text="Remitente Comercial"></asp:Label>
                </td>
                <td>
                    <asp:ImageButton ID="ImageButtonDeleteRemitenteComercial" runat="server" ImageUrl="~/Content/Images/iconotrash.png"
                        Height="12px" Width="12px" Visible="False" OnClick="ImageButtonDeleteRemitenteComercial_Click" />
                </td>
                <td>
                    <asp:Label ID="cboClienteRemitenteComercial" runat="server" BorderColor="Silver"
                        BorderStyle="Solid" BorderWidth="1px" Height="20px" Width="300px"> </asp:Label>
                </td>
                <td class="style13">
                    <asp:ImageButton ID="ImageButtonClienteRemitenteComercial" runat="server" ImageUrl="~/Content/Images/magnify.gif"
                        OnClick="ImageButtonClienteRemitenteComercial_Click" />
                </td>
                <td class="style13">
                    <asp:HiddenField ID="hbClienteRemitenteComercial" runat="server" />
                </td>
                <td class="style5">
                    <asp:Label ID="lblCuit3" runat="server" Text="CUIT Nro"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtCuitClienteRemitenteComercial" runat="server" BorderColor="Silver"
                        BorderStyle="Solid" BorderWidth="1px" Height="20px" Width="200px"></asp:Label>
                </td>
                <%--            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    </td>
                </ContentTemplate>
            </asp:UpdatePanel>--%>
            </tr>
            <tr>
                <td class="style15">
                    <asp:Label ID="lblProveedorTitularCartaDePorte2" runat="server" Text="Corredor"></asp:Label>
                </td>
                <td>
                    <asp:ImageButton ID="ImageButtonDeleteCorredor" runat="server" ImageUrl="~/Content/Images/iconotrash.png"
                        Visible="False" Height="12px" Width="12px" OnClick="ImageButtonDeleteCorredor_Click" />
                </td>
                <td>
                    <asp:Label ID="cboClienteCorredor" runat="server" BorderColor="Silver" BorderStyle="Solid"
                        BorderWidth="1px" Height="20px" Width="300px"> </asp:Label>
                </td>
                <td class="style9">
                    <asp:ImageButton ID="ImageButtonClienteCorredor" runat="server" ImageUrl="~/Content/Images/magnify.gif"
                        OnClick="ImageButtonClienteCorredor_Click" />
                </td>
                <td class="style9">
                    <asp:HiddenField ID="hbClienteCorredor" runat="server" />
                </td>
                <td class="style10">
                    <asp:Label ID="lblCuit4" runat="server" Text="CUIT Nro"></asp:Label>
                </td>
                <td class="style7">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label ID="txtCuitClienteCorredor" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                BorderWidth="1px" Height="20px" Width="200px"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="style14">
                    <asp:Label ID="lblProveedorTitularCartaDePorte3" runat="server" Text="Representante/Entregador"></asp:Label>
                </td>
                <td>
                    <asp:ImageButton ID="ImageButtonDeleteRepresentanteEntregador" runat="server" ImageUrl="~/Content/Images/iconotrash.png"
                        Height="12px" Width="12px" Visible="False" OnClick="ImageButtonDeleteRepresentanteEntregador_Click" />
                </td>
                <td>
                    <asp:Label ID="cboClienteEntregador" runat="server" BorderColor="Silver" BorderStyle="Solid"
                        BorderWidth="1px" Height="20px" Width="300px"> </asp:Label>
                </td>
                <td class="style13">
                    <asp:ImageButton ID="ImageButtonClienteEntregador" runat="server" ImageUrl="~/Content/Images/magnify.gif"
                        OnClick="ImageButtonClienteEntregador_Click" />
                    &nbsp;
                </td>
                <td class="style13">
                    <asp:HiddenField ID="hbClienteEntregador" runat="server" />
                </td>
                <td class="style5">
                    <asp:Label ID="lblCuit5" runat="server" Text="CUIT Nro"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtCuitClienteEntregador" runat="server" BorderColor="Silver" BorderStyle="Solid"
                        BorderWidth="1px" Height="20px" Width="200px"></asp:Label>
                </td>
                <%--            <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    </td>
                </ContentTemplate>
            </asp:UpdatePanel>
                --%>
            </tr>
            <tr>
                <td class="style14">
                    <asp:Label ID="lblProveedorTitularCartaDePorte4" runat="server" Text="Destinatario"></asp:Label>
                </td>
                <td>
                    <asp:ImageButton ID="ImageButtonDeleteDestinatario" runat="server" ImageUrl="~/Content/Images/iconotrash.png"
                        Visible="False" Height="12px" Width="12px" OnClick="ImageButtonDeleteDestinatario_Click" />
                </td>
                <td>
                    <asp:Label ID="cboClienteDestinatario" runat="server" BorderColor="Silver" BorderStyle="Solid"
                        BorderWidth="1px" Height="20px" Width="300px"> </asp:Label>
                </td>
                <td class="style13">
                    <asp:ImageButton ID="ImageButtonClienteDestinatario" runat="server" ImageUrl="~/Content/Images/magnify.gif"
                        OnClick="ImageButtonClienteDestinatario_Click" />
                    &nbsp;
                </td>
                <td class="style13">
                    <asp:HiddenField ID="hbClienteDestinatario" runat="server" />
                </td>
                <td class="style5">
                    <asp:Label ID="lblCuit6" runat="server" Text="CUIT Nro"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtCuitClienteDestinatario" runat="server" BorderColor="Silver" BorderStyle="Solid"
                        BorderWidth="1px" Height="20px" Width="200px"></asp:Label>
                </td>
                <%--            <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    </td>
                </ContentTemplate>
            </asp:UpdatePanel>--%>
            </tr>
            <tr>
                <td class="style14">
                    <asp:Label ID="lblProveedorTitularCartaDePorte5" runat="server" Text="Destino"></asp:Label>
                </td>
                <td>
                    <asp:ImageButton ID="ImageButtonDeleteDestino" runat="server" ImageUrl="~/Content/Images/iconotrash.png"
                        Visible="False" Height="12px" Width="12px" OnClick="ImageButtonDeleteDestino_Click" />
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanelcboClienteDestino" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label ID="cboClienteDestino" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                BorderWidth="1px" Height="20px" Width="300px"> </asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboIdEstablecimientoDestino" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td class="style13">
                    <asp:ImageButton ID="ImageButtonClienteDestino" runat="server" ImageUrl="~/Content/Images/magnify.gif"
                        OnClick="ImageButtonClienteDestino_Click" Visible="false" />
                    &nbsp;
                </td>
                <td class="style13">
                    <asp:UpdatePanel ID="UpdatePanelhbClienteDestino" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:HiddenField ID="hbClienteDestino" runat="server" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboIdEstablecimientoDestino" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td class="style5">
                    <asp:Label ID="lblCuit7" runat="server" Text="CUIT Nro"></asp:Label>
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePaneltxtCuitClienteDestino" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label ID="txtCuitClienteDestino" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                BorderWidth="1px" Height="20px" Width="200px"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboIdEstablecimientoDestino" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="style14">
                    <asp:Label ID="lblProveedorTitularCartaDePorte6" runat="server" Text="Transportista"></asp:Label>
                </td>
                <td>
                    <asp:ImageButton ID="ImageButtonDeleteTransportista" runat="server" ImageUrl="~/Content/Images/iconotrash.png"
                        Height="12px" Width="12px" Visible="False" OnClick="ImageButtonDeleteTransportista_Click" />
                </td>
                <td>
                    <asp:Label ID="cboProveedorTransportista" runat="server" BorderColor="Silver" BorderStyle="Solid"
                        BorderWidth="1px" Height="20px" Width="300px"> </asp:Label>
                </td>
                <td class="style13">
                    <asp:ImageButton ID="ImageButtonProveedorTransportista" runat="server" ImageUrl="~/Content/Images/magnify.gif"
                        OnClick="ImageButtonProveedorTransportista_Click" />
                    &nbsp;
                </td>
                <td class="style13">
                    <asp:HiddenField ID="hbProveedorTransportista" runat="server" />
                </td>
                <td class="style5">
                    <asp:Label ID="lblCuit8" runat="server" Text="CUIT Nro"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtCuitProveedorTransportista" runat="server" BorderColor="Silver"
                        BorderStyle="Solid" BorderWidth="1px" Height="20px" Width="200px"></asp:Label>
                </td>
                <%--            <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    </td></ContentTemplate>
            </asp:UpdatePanel>--%>
            </tr>
            <tr>
                <td class="style14">
                    <asp:Label ID="lblProveedorTitularCartaDePorte7" runat="server" Text="Chofer"></asp:Label>
                </td>
                <td>
                    <asp:ImageButton ID="ImageButtonDeleteChofer" runat="server" ImageUrl="~/Content/Images/iconotrash.png"
                        Visible="False" Height="12px" Width="12px" OnClick="ImageButtonDeleteChofer_Click" />
                </td>
                <td>
                    <asp:Label ID="cboChofer" runat="server" BorderColor="Silver" BorderStyle="Solid"
                        BorderWidth="1px" Height="20px" Width="300px"> </asp:Label>
                </td>
                <td class="style13">
                    <asp:ImageButton ID="ImageButtonBuscadorChofer" runat="server" ImageUrl="~/Content/Images/magnify.gif"
                        OnClick="ImageButtonBuscadorChofer_Click" />
                    &nbsp;
                </td>
                <td class="style13">
                    <asp:HiddenField ID="hbChofer" runat="server" />
                </td>
                <td class="style5">
                    <asp:Label ID="lblCuit9" runat="server" Text="CUIT/CUIL"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtCuitChofer" runat="server" BorderColor="Silver" BorderStyle="Solid"
                        BorderWidth="1px" Height="20px" Width="200px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style11" colspan="7">&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="contenedordiv">
        <table align="center" class="tabla1" id="tblRemitenteComercialComoCanjeador" runat="server"
            visible="false">
            <tr>
                <td>&nbsp;&nbsp;
                <asp:CheckBox ID="chkRemitenteComercialComoCanjeador" runat="server" Text="¿El remitente comercial es canjeador?" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="contenedordiv">
        <table align="center" class="tabla1" id="tableCarga2" runat="server" visible="true">
            <tr>
                <td colspan="7">
                    <table class="style1">
                        <tr>
                            <td align="center" class="style16">2
                            </td>
                            <td>&nbsp;<asp:Label ID="lblTituloSeccion2" runat="server" Text="DATOS DE LOS GRANOS / ESPECIES TRANSPORTADOS"
                                CssClass="SubTitulos"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style33">
                    <asp:Label ID="lblProveedorTitularCartaDePorte8" runat="server" Text="Grano"></asp:Label>
                </td>
                <td colspan="1">
                    <asp:DropDownList ID="cboGrano" runat="server" AutoPostBack="True" Height="21px"
                        Width="240px" OnSelectedIndexChanged="cboGrano_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td class="style72">
                    <asp:Label ID="lblProveedorTitularCartaDePorte9" runat="server" Text="Tipo"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox ID="txtTipoGrano" runat="server" Width="199px" Enabled="false"></asp:TextBox></td>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboGrano" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td class="style28">
                    <asp:Label ID="lblEmpresaTitularCartaDePorte10" runat="server" Text="Cosecha"></asp:Label>
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox ID="txtCosecha" runat="server" Width="127px" Enabled="false"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboGrano" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="style33" rowspan="2" colspan="2">
                    <asp:CheckBox ID="rbCargaPesadaDestino" runat="server" Text="La carga sera pesada en destino"
                        AutoPostBack="True" OnCheckedChanged="rbCargaPesadaDestino_CheckedChanged" />
                </td>
                <td class="style35">
                    <asp:CheckBox ID="rbDeclaracionCalidad" runat="server" Text="Declaracion Calidad"
                        Enabled="false" />
                </td>
                <td class="style73">
                    <asp:Label ID="lblEmpresaTitularCartaDePorte11" runat="server" Text="Peso Bruto (Kgrs.)"></asp:Label>
                </td>
                <td class="style69">
                    <asp:UpdatePanel ID="UpdatePaneltxtPesoBruto" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox ID="txtPesoBruto" runat="server" Width="80px" OnTextChanged="txtPesoBruto_TextChanged"
                                AutoPostBack="True"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="rbCargaPesadaDestino" EventName="CheckedChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td class="style28">
                    <asp:Label ID="lblEmpresaTitularCartaDePorte14" runat="server" Text="Contrato Nro."></asp:Label>
                </td>
                <td class="style29">
                    <asp:TextBox ID="txtContratoNro" runat="server" Width="127px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style35">
                    <asp:RadioButtonList ID="rblConformeCondicional" runat="server" Height="16px" RepeatDirection="Vertical"
                        Width="189px">
                        <asp:ListItem>Conforme</asp:ListItem>
                        <asp:ListItem>Condicional</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="style73">
                    <asp:Label ID="lblEmpresaTitularCartaDePorte12" runat="server" Text="Peso Tara (Kgrs.)"></asp:Label>
                </td>
                <td class="style69">
                    <asp:UpdatePanel ID="UpdatePaneltxtPesoTara" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox ID="txtPesoTara" runat="server" Width="80px" OnTextChanged="txtPesoTara_TextChanged"
                                AutoPostBack="True"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="rbCargaPesadaDestino" EventName="CheckedChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td rowspan="2">
                    <asp:Label ID="lblEmpresaTitularCartaDePorte15" runat="server" Text="Observaciones"></asp:Label>
                </td>
                <td rowspan="2" class="style29">
                    <asp:TextBox ID="txtObsevaciones" runat="server" Width="130px" Height="63px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style33" rowspan="2">&nbsp;
                <asp:Label ID="lblkgsEstimados" runat="server" Text="Kgrs Estimados"></asp:Label>
                    &nbsp;
                </td>
                <td class="style68" rowspan="2">
                    <asp:UpdatePanel ID="UpdatePaneltxtKrgsEstimados" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox ID="txtKrgsEstimados" runat="server" Width="129px"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="rbCargaPesadaDestino" EventName="CheckedChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td rowspan="2">&nbsp;
                &nbsp; &nbsp; &nbsp;
                </td>
                <td class="style73">
                    <asp:Label ID="lblEmpresaTitularCartaDePorte13" runat="server" Text="Peso Neto (Kgrs.)"></asp:Label>
                </td>
                <td class="style69">
                    <asp:UpdatePanel ID="UpdatePaneltxtPesoNeto" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox ID="txtPesoNeto" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtPesoTara" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="txtPesoBruto" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="rbCargaPesadaDestino" EventName="CheckedChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="style73">&nbsp;
                </td>
                <td class="style69">&nbsp;
                </td>
                <td class="style28">&nbsp;
                </td>
                <td class="style29">&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <div class="contenedordiv">
        <table class="tabla1" id="tableCarga3" runat="server" visible="true">
            <tr>
                <td align="center" class="style39" colspan="2">
                    <asp:Label ID="lblTituloSeccion3" runat="server" Text="PROCEDENCIA DE LA MERCADERIA"
                        CssClass="SubTitulos"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblEmpresaTitularCartaDePorte16" runat="server" Text="Establecimiento"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cboIdEstablecimientoProcedencia" runat="server" AutoPostBack="True"
                        Height="21px" Width="200px" OnSelectedIndexChanged="cboIdEstablecimientoProcedencia_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style41">
                    <asp:Label ID="lblEmpresaTitularCartaDePorte17" runat="server" Text="Dirección"></asp:Label>
                </td>
                <td class="style40" rowspan="2">
                    <asp:UpdatePanel ID="UpdatePanelDireccionEstablecimientoProcedencia" runat="server"
                        UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox ID="txtDireccionEstablecimientoProcedencia" runat="server" Width="545px"
                                Enabled="false"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboIdEstablecimientoProcedencia" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td>
                    <asp:Label ID="lblEmpresaTitularCartaDePorte18" runat="server" Text="Localidad"></asp:Label>
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanelLocalidadEstablecimientoProcedencia" runat="server"
                        UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox ID="txtLocalidadEstablecimientoProcedencia" runat="server" Width="200px"
                                Enabled="false"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboIdEstablecimientoProcedencia" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="style41">&nbsp;
                </td>
                <td>
                    <asp:Label ID="lblEmpresaTitularCartaDePorte19" runat="server" Text="Provincia"></asp:Label>
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanelProvinciaEstablecimientoProcedencia" runat="server"
                        UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox ID="txtProvinciaEstablecimientoProcedencia" runat="server" Width="200px"
                                Enabled="false"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboIdEstablecimientoProcedencia" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <div class="contenedordiv">
        <table class="tabla1" id="tableCarga4" runat="server" visible="true">
            <tr>
                <td class="style39" colspan="2">
                    <table class="style1">
                        <tr>
                            <td class="style44">3
                            </td>
                            <td>
                                <asp:Label ID="lblTituloSeccion4" runat="server" Text="LUGAR DE DESTINO DE LOS GRANOS"
                                    CssClass="SubTitulos"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="style78">
                    <asp:Label ID="Label1" runat="server" Text="Destino"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cboIdEstablecimientoDestino" runat="server" AutoPostBack="True"
                        Height="21px" Width="200px" OnSelectedIndexChanged="cboIdEstablecimientoDestino_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style68">
                    <asp:Label ID="Label2" runat="server" Text="Dirección"></asp:Label>
                </td>
                <td class="style79" rowspan="2">
                    <asp:UpdatePanel ID="UpdatePaneltxtDireccionEstablecimientoDestino" runat="server"
                        UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox ID="txtDireccionEstablecimientoDestino" runat="server" Width="519px"
                                Enabled="false" Style="margin-left: 13px; margin-right: 41px"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboIdEstablecimientoDestino" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td class="style78">
                    <asp:Label ID="Label3" runat="server" Text="Localidad"></asp:Label>
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePaneltxtLocalidadEstablecimientoDestino" runat="server"
                        UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox ID="txtLocalidadEstablecimientoDestino" runat="server" Width="200px"
                                Enabled="false"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboIdEstablecimientoDestino" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="style68">&nbsp;
                </td>
                <td class="style78">
                    <asp:Label ID="Label4" runat="server" Text="Provincia"></asp:Label>
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePaneltxtProvinciaEstablecimientoDestino" runat="server"
                        UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox ID="txtProvinciaEstablecimientoDestino" runat="server" Width="200px"
                                Enabled="false"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboIdEstablecimientoDestino" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <div class="contenedordiv">
        <table align="center" class="tabla1" id="tableCarga5" runat="server" visible="true">
            <tr>
                <td colspan="4">
                    <table>
                        <tr>
                            <td class="style59">4
                            </td>
                            <td>
                                <asp:Label ID="lblTituloSeccion5" runat="server" Text="DATOS DEL TRANSPORTISTA" CssClass="SubTitulos"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="style50">
                    <asp:Label ID="Label5" runat="server" Text="Pagador del flete"></asp:Label>
                </td>
                <td>
                    <asp:ImageButton ID="ImageButtonDeletePagadorFlete" runat="server" ImageUrl="~/Content/Images/iconotrash.png"
                        Visible="False" Height="12px" Width="12px" OnClick="ImageButtonDeletePagadorFlete_Click" />
                    <asp:Label ID="cboClientePagadorDelFlete" runat="server" BorderColor="Silver" BorderStyle="Solid"
                        BorderWidth="1px" Height="20px" Width="180px"></asp:Label>
                    <asp:HiddenField ID="hbClientePagadorDelFlete" runat="server" />
                </td>
                <td>
                    <asp:ImageButton ID="ImageButtonClientePagadorDelFlete" runat="server" ImageUrl="~/Content/Images/magnify.gif"
                        OnClick="ImageButtonClientePagadorDelFlete_Click" />
                </td>
            </tr>
            <tr>
                <td class="style53">
                    <asp:Label ID="Label6" runat="server" Text="Camión"></asp:Label>
                </td>
                <td class="style54">
                    <asp:TextBox ID="txtPatente" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td class="style55" colspan="2">
                    <asp:RadioButtonList ID="rblFletePagadoAPagar" runat="server" Height="16px" RepeatDirection="Horizontal"
                        Width="324px">
                        <asp:ListItem>Flete Pagado</asp:ListItem>
                        <asp:ListItem>Flete a Pagar</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:Label ID="lblCantidadDeHoras" runat="server" Text="Cantidad de Horas"></asp:Label>
                </td>
                <td class="style52">
                    <asp:TextBox ID="txtCantHoras" runat="server" Width="50px"></asp:TextBox>
                </td>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td class="style53">
                    <asp:Label ID="Label7" runat="server" Text="Acoplado"></asp:Label>
                </td>
                <td class="style54">
                    <asp:TextBox ID="txtAcoplado" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td class="style56">
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label ID="Label10" runat="server" Text="Tarifa de Referencia"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboTipoDeCartam" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td class="style74">
                    <asp:TextBox ID="txtTarifaReferencia" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td>&nbsp;
                </td>
                <td class="style52">&nbsp;
                </td>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td class="style53">
                    <asp:Label ID="Label8" runat="server" Text="Km a recorrer"></asp:Label>
                </td>
                <td class="style54">
                    <asp:TextBox ID="txtKmRecorridos" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td class="style56">
                    <asp:Label ID="Label11" runat="server" Text="Tarifa"></asp:Label>
                </td>
                <td class="style74">
                    <asp:TextBox ID="txtTarifa" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td>&nbsp;
                </td>
                <td class="style52">&nbsp;
                </td>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td class="style53">&nbsp;
                </td>
                <td class="style54">&nbsp;
                </td>
                <td class="style56">&nbsp;
                </td>
                <td class="style74">&nbsp;
                </td>
                <td>&nbsp;
                </td>
                <td class="style52">&nbsp;
                </td>
                <td>&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="contenedordiv">
        <table class="tabla1" id="tableCambioDestino" runat="server" visible="false">
            <tr>
                <td class="style39" colspan="2">
                    <table class="style1">
                        <tr>
                            <td class="style44">6
                            </td>
                            <td>
                                <asp:Label ID="Label24" runat="server" Text="CAMBIO DEL DOMICILIO DE DESCARGA / DESVIO"
                                    CssClass="SubTitulos"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <asp:Label ID="Label25" runat="server" Text="Destino"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cboIdEstablecimientoDestinoCambio" runat="server" AutoPostBack="True"
                        Height="21px" Width="200px" OnSelectedIndexChanged="cboIdEstablecimientoDestinoCambio_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style42" rowspan="2">
                    <asp:Label ID="Label26" runat="server" Text="Dirección"></asp:Label>
                    &nbsp;
                </td>
                <td class="style77" rowspan="2">
                    <asp:UpdatePanel ID="UpdatePaneltxtDireccionEstablecimientoDestinoCambio" runat="server"
                        UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox ID="txtDireccionEstablecimientoDestinoCambio" runat="server" Width="550px"
                                Enabled="false"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboIdEstablecimientoDestinoCambio" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td>
                    <asp:Label ID="Label27" runat="server" Text="Localidad"></asp:Label>
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePaneltxtLocalidadEstablecimientoDestinoCambio" runat="server"
                        UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox ID="txtLocalidadEstablecimientoDestinoCambio" runat="server" Width="200px"
                                Enabled="false"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboIdEstablecimientoDestinoCambio" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label28" runat="server" Text="CUIT"></asp:Label>
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePaneltxtProvinciaEstablecimientoDestinoCambio" runat="server"
                        UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox ID="txtCuitDestinoCambio" runat="server" Width="200px" Enabled="false"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboIdEstablecimientoDestinoCambio" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="style42" colspan="3">
                    <table>
                        <tr>
                            <td class="style14">
                                <asp:Label ID="lblDestinatarioCambio" runat="server" Text="Nuevo Destinatario"></asp:Label>
                            </td>
                            <td>
                                <asp:ImageButton ID="ImageButtonDeleteDestinatarioCambio" runat="server" ImageUrl="~/Content/Images/iconotrash.png"
                                    Visible="False" Height="12px" Width="12px" OnClick="ImageButtonDeleteDestinatarioCambio_Click"
                                    ImageAlign="Middle" />
                            </td>
                            <td>
                                <asp:Label ID="cboClienteDestinatarioCambio" runat="server" BorderColor="Silver"
                                    BorderStyle="Solid" BorderWidth="1px" Height="20px" Width="300px">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:ImageButton ID="ImageButtonClienteDestinatarioCambio" runat="server" ImageUrl="~/Content/Images/magnify.gif"
                                    OnClick="ImageButtonClienteDestinatarioCambio_Click" ImageAlign="Middle" />
                                &nbsp;
                            </td>
                            <td>
                                <asp:HiddenField ID="hbClienteDestinatarioCambio" runat="server" />
                            </td>
                            <td class="style5">
                                <asp:Label ID="lblCuitDestinatarioCambio" runat="server" Text="CUIT Nro"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="txtCuitClienteDestinatarioCambio" runat="server" Width="200px" CssClass="LabelBuscadorResul"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div class="contenedordiv">
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
                            <asp:AsyncPostBackTrigger ControlID="btnSoloGuardar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="cboTipoDeCartam" EventName="SelectedIndexChanged" />
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
                <td align="right"></td>
            </tr>
        </table>
    </div>
    <div class="contenedordiv">
        <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="Buscadores" id="BuscadorEmpresa" runat="server">
                    <table style="width: 599px; height: 50px">
                        <tr>
                            <td class="style63" colspan="3">
                                <br />
                                <asp:Label ID="lblTituloBuscadorEmpresa" runat="server" Text="BUSCADOR DE EMPRESAS"
                                    CssClass="SubTitulos"></asp:Label><br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtBuscadorEmpresa" placeholder="BUSCAR" runat="server" Width="335px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnBuscadorEmpresa" runat="server" Text="Buscar" OnClick="btnBuscadorEmpresa_Click" />
                            </td>
                            <td class="style60">
                                <asp:Button ID="btnCerrarEmpresa" runat="server" Text="Cerrar" OnClick="btnCerrarEmpresa_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <div class="grillaBuscador">
                                    <asp:Table ID="tblBuscadorEmpresa" runat="server">
                                    </asp:Table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                            <td>&nbsp;
                            </td>
                            <td>&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="Buscadores" id="BuscadorCliente" runat="server">
                    <table style="width: 599px; height: 50px">
                        <tr>
                            <td class="style63" colspan="3">
                                <br />
                                &nbsp;&nbsp;
                            <asp:Label ID="Label9" runat="server" Text="BUSCADOR DE CLIENTES" CssClass="SubTitulos"></asp:Label><br />
                            </td>
                        </tr>
                        <tr class="styleAltaRapida">
                            <td>&nbsp;&nbsp;
                            <asp:TextBox ID="txtBuscadorCliente" placeholder="BUSCAR" runat="server" Width="335px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnBuscadorCliente" runat="server" Text="Buscar" OnClick="btnBuscadorCliente_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnCerrarCliente" runat="server" Text="Cerrar" OnClick="btnCerrarCliente_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <table>
                                    <tr>
                                        &nbsp;&nbsp; Alta Cliente Prospecto
                                    </tr>
                                    <tr>
                                        <td>&nbsp;
                                        <asp:TextBox ID="txtClienteAltaRapidaNombre" placeholder="NOMBRE" runat="server"
                                            Width="150px"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;
                                        <asp:TextBox ID="txtClienteAltaRapidaCuit" placeholder="CUIT" runat="server" Width="100px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <div id="divClienteAltaRapida" runat="server">
                                                <asp:Button ID="btnCargaRapidaCliente" runat="server" Text="Alta Prospecto" OnClick="btnCargaRapidaCliente_Click" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="lblMensajeClienteAltaRapida" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <div class="grillaBuscador">
                                    <asp:Table ID="tblBuscadorCliente" runat="server">
                                    </asp:Table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                            <td>&nbsp;
                            </td>
                            <td>&nbsp;
                            </td>
                            <td>&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="Buscadores" id="BuscadorProveedor" runat="server">
                    <table style="width: 599px; height: 50px">
                        <tr>
                            <td class="style63" colspan="3">
                                <br />
                                <asp:Label ID="Label13" runat="server" Text="BUSCADOR DE PROVEEDORES" CssClass="SubTitulos"></asp:Label><br />
                            </td>
                        </tr>
                        <tr class="styleAltaRapida">
                            <td>
                                <asp:TextBox ID="txtBuscadorProveedor" placeholder="BUSCAR" runat="server" Width="335px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnBuscadorProveedor" runat="server" Text="Buscar" OnClick="btnBuscadorProveedor_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnCerrarProveedor" runat="server" Text="Cerrar" OnClick="btnCerrarProveedor_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <table>
                                    <tr>
                                        <td colspan="3">
                                            <table>
                                                <tr>
                                                    &nbsp;&nbsp; Alta Proveedor Prospecto
                                                </tr>
                                                <td>&nbsp;
                                                <asp:TextBox ID="txtProveedorAltaRapidaNombre" placeholder="NOMBRE" runat="server"
                                                    Width="150px"></asp:TextBox>
                                                </td>
                                                <td>&nbsp;
                                                <asp:TextBox ID="txtProveedorAltaRapidaCuit" placeholder="CUIT" runat="server" Width="100px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <div id="div2" runat="server">
                                                        <asp:Button ID="btnCargaRapidaProveedor" runat="server" Text="Alta Prospecto" OnClick="btnCargaRapidaProveedor_Click" />
                                                    </div>
                                                </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="lblMensajeProveedorAltaRapida" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <div class="grillaBuscador">
                                    <asp:Table ID="tblBuscadorProveedor" runat="server">
                                    </asp:Table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="style65">&nbsp;
                            </td>
                            <td class="style64">&nbsp;
                            </td>
                            <td class="style60">&nbsp;
                            </td>
                            <td class="style60">&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="BuscadoresChofer" id="BuscadorChofer" runat="server">
                    <table style="width: 699px; height: 50px">
                        <tr>
                            <td class="style63" colspan="3">
                                <br />
                                &nbsp;&nbsp;
                            <asp:Label ID="Label15" runat="server" Text="BUSCADOR DE CHOFERES" CssClass="SubTitulos"></asp:Label><br />
                            </td>
                        </tr>
                        <tr class="styleAltaRapida">
                            <td>&nbsp;&nbsp;
                            <asp:TextBox ID="txtBuscadorChofer" placeholder="BUSCAR" runat="server" Width="500px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnBuscadorChofer" runat="server" Text="Buscar" OnClick="btnBuscadorChofer_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnCerrarChofer" runat="server" Text="Cerrar" OnClick="btnCerrarChofer_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <table>
                                    <tr>
                                        &nbsp;&nbsp;
                                        <asp:Label ID="lblAltaRapidaChoferesTitulo" runat="server" Text=""></asp:Label>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtChoferAltaRapidaNombre" placeholder="NOMBRE" runat="server" Width="150px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtChoferAltaRapidaApellido" placeholder="APELLIDO" runat="server"
                                                Width="150px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtChoferAltaRapidaCuit" placeholder="CUIT" runat="server" Width="100px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtChoferAltaRapidaCamion" placeholder="CAMION" runat="server" Width="80px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtChoferAltaRapidaAcoplado" placeholder="ACOPLADO" runat="server"
                                                Width="80px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <div id="divChoferAltaRapida" runat="server">
                                                <asp:Button ID="btnCargaRapidaChofer" runat="server" Text="Alta Rapida" OnClick="btnCargaRapidaChofer_Click" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <asp:Label ID="lblMensajeChoferAltaRapida" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <div class="grillaBuscador">
                                    <asp:Table ID="tblBuscadorChofer" runat="server">
                                    </asp:Table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="style65">&nbsp;
                            </td>
                            <td class="style64">&nbsp;
                            </td>
                            <td class="style60">&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="divServiceUnavailable" runat="server" visible="false">
        <div style="position: fixed; top: 0px; bottom: 0px; left: 0px; right: 0px; overflow: hidden; padding: 0; margin: 0; background-color: #000000; filter: alpha(opacity=50); opacity: 0.5; z-index: 100000;">
        </div>
        <div style="position: fixed; top: 40%; left: 40%; height: 20%; width: 20%; z-index: 100001; background-color: #FFFFFF; border: 2px solid #000000; background-image: url('Content/Images/ajax_loader_large.gif'); background-size: 32px 32px; background-repeat: no-repeat; background-position: center;">
            <center>
                <br />
                Servicio de Afip<br />
                Temporalmente no disponible</center>
        </div>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="CargaEnDestino.aspx.cs" Inherits="CartaDePorte.Web.CargaEnDestino" %>

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
        .contenedordiv
        {
            z-index: 100;
            position: relative;
        }
        #watermark
        {
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
        .styleAltaRapida
        {
            display: block;
            padding: 2px 2px 2px 2px;
            color: #000000;
            text-decoration: none;
            background: #F2F2F2;
        }
        .style1
        {
            width: 94%;
        }
        .style3
        {
        }
        .style5
        {
            width: 173px;
        }
        .style6
        {
            width: 30px;
        }
        .style7
        {
            height: 7px;
        }
        .style10
        {
            width: 173px;
            height: 7px;
        }
        .style11
        {
        }
        .style14
        {
            width: 189px;
            margin-left: 80px;
        }
        .style15
        {
            width: 189px;
            height: 7px;
        }
        .style16
        {
            width: 33px;
        }
        .style28
        {
        }
        .style29
        {
            width: 268435408px;
        }
        .style33
        {
        }
        .style35
        {
        }
        .style39
        {
        }
        .style42
        {
        }
        .style44
        {
            width: 25px;
        }
        .style50
        {
            width: 157px;
        }
        .style52
        {
            width: 201px;
        }
        .style53
        {
        }
        .style54
        {
            width: 81px;
        }
        .style56
        {
            width: 121px;
        }
        .style58
        {
            width: 817px;
        }
        .tabla1
        {
            width: 100%;
            border-color: 1;
            border-style: solid;
            border-width: thin;
        }
        .SubTitulos
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: small;
            font-weight: bold;
        }
        .SubTitulosPrincipal
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: larger;
            font-weight: bold;
        }
        .style59
        {
            width: 26px;
        }
        .style68
        {
            width: 95px;
        }
        .style69
        {
            width: 210px;
        }
        .style72
        {
            width: 74px;
        }
        .style73
        {
            width: 190px;
        }
        .style74
        {
            width: 52px;
        }
        .style75
        {
            width: 629px;
        }
        .style76
        {
            width: 865px;
        }
        .style80
        {
            width: 192px;
        }
        .style85
        {
            width: 124px;
        }
        .style86
        {
            width: 224px;
        }
        .style87
        {
            width: 231px;
        }
        .style88
        {
            width: 136px;
        }
        .style89
        {
            width: 100%;
        }
        .style90
        {
            width: 188px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <br />
    <br />
    <div class="divTopPage1">
    </div>
    <br />
    <div class="contenedordiv">
        <table style="width: 956px">
            <tr>
                <td class="style76">
                </td>
                <td class="style75">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="font-family: Arial, Helvetica, sans-serif; font-size: large" class="style76">
                    <asp:Label ID="lblTituloSeccion6" runat="server" Text="Carta de Porte Recibida" CssClass="SubTitulosPrincipal"></asp:Label>
                </td>
                <td style="font-family: Arial, Helvetica, sans-serif; font-size: large" class="style75">
                    <table align="right">
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="savepanelSoloGuardar" UpdateMode="Conditional" runat="server">
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
                <td colspan="7">
                    &nbsp;
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
                <td class="style88">
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
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboTipoDeCartam" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cboTipoDeCartam" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="center" class="style3" colspan="7">
                    &nbsp;
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
                    &nbsp;</td>
                <td class="style88">
                    <asp:Label ID="Label17" runat="server" Text="Carta De Porte"></asp:Label>
                </td>
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
                <td align="center" class="style3" colspan="3">
                    <asp:Label ID="lblTituloSeccion" runat="server" Text="CARTA DE PORTE PARA EL TRANSPORTE AUTOMOTOR DE GRANOS"
                        CssClass="SubTitulos"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" class="style3" colspan="3">
                    <table class="style1">
                        <tr>
                            <td class="style6">
                                &nbsp;</td>
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
                <td class="style5">
                    <asp:Label ID="lblCuit1" runat="server" Text="CUIT Nro"></asp:Label>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtCuitProveedorTitularCartaDePorte" runat="server" Width="300px"
                                    OnTextChanged="txtCuit_TextChanged" AutoPostBack="True"></asp:TextBox>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="txtCuitProveedorTitularCartaDePorte1" runat="server" Text="*" Style="display: none"></asp:Label>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtCuitProveedorTitularCartaDePorte" EventName="TextChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style14">
                    <asp:Label ID="lblProveedorTitularCartaDePorte0" runat="server" Text="Intermediario"></asp:Label>
                </td>
                <td class="style5">
                    <asp:Label ID="lblCuit2" runat="server" Text="CUIT Nro"></asp:Label>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtCuitClienteIntermediario" runat="server" Width="300px" AutoPostBack="True"
                                    OnTextChanged="txtCuit_TextChanged"></asp:TextBox>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="txtCuitClienteIntermediario1" runat="server" Text="*" Style="display: none"></asp:Label>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtCuitClienteIntermediario" EventName="TextChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style14">
                    <asp:Label ID="lblProveedorTitularCartaDePorte1" runat="server" Text="Remitente Comercial"></asp:Label>
                </td>
                <td class="style5">
                    <asp:Label ID="lblCuit3" runat="server" Text="CUIT Nro"></asp:Label>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtCuitClienteRemitenteComercial" runat="server" Width="300px" AutoPostBack="True"
                                    OnTextChanged="txtCuit_TextChanged"></asp:TextBox>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="txtCuitClienteRemitenteComercial1" runat="server" Text="*" Style="display: none"></asp:Label>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtCuitClienteRemitenteComercial" EventName="TextChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style15">
                    <asp:Label ID="lblProveedorTitularCartaDePorte2" runat="server" Text="Corredor"></asp:Label>
                </td>
                <td class="style10">
                    <asp:Label ID="lblCuit4" runat="server" Text="CUIT Nro"></asp:Label>
                </td>
                <td class="style7">
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtCuitClienteCorredor" runat="server" Width="300px" AutoPostBack="True"
                                    OnTextChanged="txtCuit_TextChanged"></asp:TextBox>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="txtCuitClienteCorredor1" runat="server" Text="*" Style="display: none"></asp:Label>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtCuitClienteCorredor" EventName="TextChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style14">
                    <asp:Label ID="lblProveedorTitularCartaDePorte3" runat="server" Text="Representante/Entregador"></asp:Label>
                </td>
                <td class="style5">
                    <asp:Label ID="lblCuit5" runat="server" Text="CUIT Nro"></asp:Label>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtCuitClienteEntregador" runat="server" Width="300px" AutoPostBack="True"
                                    OnTextChanged="txtCuit_TextChanged"></asp:TextBox>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="txtCuitClienteEntregador1" runat="server" Text="*" Style="display: none"></asp:Label>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtCuitClienteEntregador" EventName="TextChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style14">
                    <asp:Label ID="lblProveedorTitularCartaDePorte4" runat="server" Text="Destinatario"></asp:Label>
                </td>
                <td class="style5">
                    <asp:Label ID="lblCuit6" runat="server" Text="CUIT Nro"></asp:Label>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtCuitClienteDestinatario" runat="server" Width="300px" AutoPostBack="True"
                                    OnTextChanged="txtCuit_TextChanged" ></asp:TextBox>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel14" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="txtCuitClienteDestinatario1" runat="server" Text="*" Style="display: none"></asp:Label>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtCuitClienteDestinatario" EventName="TextChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style14">
                    <asp:Label ID="lblProveedorTitularCartaDePorte5" runat="server" Text="Destino"></asp:Label>
                </td>
                <td class="style5">
                    <asp:Label ID="lblCuit7" runat="server" Text="CUIT Nro"></asp:Label>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtCuitClienteDestino" runat="server" Width="300px" AutoPostBack="True"
                                    OnTextChanged="txtCuit_TextChanged"></asp:TextBox>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel15" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="txtCuitClienteDestino1" runat="server" Text="*" Style="display: none"></asp:Label>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtCuitClienteDestino" EventName="TextChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style14">
                    <asp:Label ID="lblProveedorTitularCartaDePorte6" runat="server" Text="Transportista"></asp:Label>
                </td>
                <td class="style5">
                    <asp:Label ID="lblCuit8" runat="server" Text="CUIT Nro"></asp:Label>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtCuitProveedorTransportista" runat="server" Width="300px" AutoPostBack="True"
                                    OnTextChanged="txtCuit_TextChanged"></asp:TextBox>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="txtCuitProveedorTransportista1" runat="server" Text="*" Style="display: none"></asp:Label>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtCuitProveedorTransportista" EventName="TextChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style14">
                    <asp:Label ID="lblProveedorTitularCartaDePorte7" runat="server" Text="Chofer"></asp:Label>
                </td>
                <td class="style5">
                    <asp:Label ID="lblCuit9" runat="server" Text="CUIT/CUIL"></asp:Label>
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtCuitChofer" runat="server" Width="300px" AutoPostBack="True"
                                    OnTextChanged="txtCuit_TextChanged"></asp:TextBox>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel17" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="txtCuitChofer1" runat="server" Text="*" Style="display: none"></asp:Label>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtCuitChofer" EventName="TextChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style11" colspan="3">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="contenedordiv">
        <table align="center" class="tabla1" id="tblRemitenteComercialComoCanjeador" runat="server"
            visible="false">
            <tr>
                <td>
                    &nbsp;&nbsp;
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
                            <td align="center" class="style16">
                                &nbsp;</td>
                            <td>
                                &nbsp;<asp:Label ID="lblTituloSeccion2" runat="server" Text="DATOS DE LOS GRANOS / ESPECIES TRANSPORTADOS"
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
                            <asp:TextBox ID="txtTipoGrano" runat="server" Width="199px" Enabled="false"></asp:TextBox></td></ContentTemplate>
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
                    &nbsp;
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
                <td class="style33" rowspan="2">
                    &nbsp;
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
                <td rowspan="2">
                    &nbsp; &nbsp; &nbsp; &nbsp;
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
                <td class="style73">
                    &nbsp;
                </td>
                <td class="style69">
                    &nbsp;
                </td>
                <td class="style28">
                    &nbsp;
                </td>
                <td class="style29">
                    &nbsp;
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
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style80">
                    <asp:Label ID="lblEmpresaTitularCartaDePorte17" runat="server" Text="Código de Establecimiento"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCodigoEstablecimientoProcedencia" runat="server" Width="160px"
                        Enabled="true"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblEmpresaTitularCartaDePorte18" runat="server" Text="Localidad"></asp:Label>
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanelLocalidad1" runat="server" UpdateMode="Conditional">
	                    <ContentTemplate>
		                    <asp:TextBox ID="txtLocalidad1" runat="server" Width="300px" Enabled="true" ></asp:TextBox>
	                    </ContentTemplate>
                    </asp:UpdatePanel>
                </td>                                    
            </tr>
            <tr>
                <td class="style80">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanelProvinciaEstablecimientoProcedencia" runat="server"
                        UpdateMode="Conditional">
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <div class="contenedordiv">
        <table class="style89">
            <tr>
                <td class="style90">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style90">
                    <asp:Label ID="lblEmpresaTitularCartaDePorte19" runat="server" 
                        Text="Fecha de descarga"></asp:Label>
                </td>
                <td>
                            <asp:TextBox ID="txtFechaDeDescarga" runat="server" Width="80"></asp:TextBox>
                        </td>
            </tr>
            <tr>
                <td class="style90">
                    <asp:Label ID="lblEmpresaTitularCartaDePorte20" runat="server" 
                        Text="Fecha de Arribo"></asp:Label>
                </td>
                <td>
                            <asp:TextBox ID="txtFechaDeArribo" runat="server" Width="80"></asp:TextBox>
                        </td>
            </tr>
            <tr>
                <td class="style90">
                    <asp:Label ID="lblEmpresaTitularCartaDePorte21" runat="server" 
                        Text="Peso Neto de Descarga"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPesoNetoDeDescarga" runat="server" Width="80px"></asp:TextBox>
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
                            <td class="style59">
                                &nbsp;</td>
                            <td>
                                <asp:Label ID="lblTituloSeccion5" runat="server" Text="DATOS DEL TRANSPORTISTA" CssClass="SubTitulos"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="style50">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style53">
                    <asp:Label ID="Label6" runat="server" Text="Camión"></asp:Label>
                </td>
                <td class="style54">
                    <asp:TextBox ID="txtPatente" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td colspan="2">
                    <asp:RadioButtonList ID="rblFletePagadoAPagar" runat="server" Height="16px" RepeatDirection="Horizontal"
                        Width="315px">
                        <asp:ListItem>Flete Pagado</asp:ListItem>
                        <asp:ListItem>Flete a Pagar</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:Label ID="lblCantidadDeHoras" runat="server" Text="Cantidad de Horas"></asp:Label>
                </td>
                <td class="style52">
                    <asp:TextBox ID="txtCantHoras" runat="server" Width="92px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
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
                <td>
                    &nbsp;
                </td>
                <td class="style52">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
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
                <td>
                    &nbsp;
                </td>
                <td class="style52">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style53">
                    &nbsp;
                </td>
                <td class="style54">
                    &nbsp;
                </td>
                <td class="style56">
                    &nbsp;
                </td>
                <td class="style74">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td class="style52">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="contenedordiv">
        <table class="tabla1" id="tableCambioDestino" runat="server" visible="true">
            <tr>
                <td class="style39" colspan="2">
                    <table class="style1">
                        <tr>
                            <td class="style44">
                                &nbsp;</td>
                            <td>
                                <asp:Label ID="Label24" runat="server" Text="CAMBIO DEL DOMICILIO DE DESCARGA / DESVIO"
                                    CssClass="SubTitulos"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="style85">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style87">
                    <asp:Label ID="Label26" runat="server" Text="Código"></asp:Label>
                    &nbsp;
                </td>
                <td class="style86">
                    <asp:TextBox ID="txtCódigoEstablecimientoDestinoCambio" runat="server" Width="222px"
                        Enabled="true"></asp:TextBox>
                </td>
                <td class="style85">
                    <asp:Label ID="Label27" runat="server" Text="Localidad"></asp:Label>
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePaneltxtLocalidadEstablecimientoDestinoCambio" runat="server"
                        UpdateMode="Conditional">
                        <ContentTemplate>
                                <asp:TextBox ID="txtLocalidad2" runat="server" Width="300px"
                                    Enabled="true"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="style87">
                    &nbsp;
                </td>
                <td class="style86">
                </td>
                <td class="style85">
                    <asp:Label ID="Label28" runat="server" Text="CUIT"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCuitDestinoCambio" runat="server" Width="300px" 
                        Enabled="true" ontextchanged="txtCuit_TextChanged" AutoPostBack="True"></asp:TextBox>
                </td>
                <td>                
                    <asp:UpdatePanel ID="UpdatePanel18" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label ID="txtCuitDestinoCambio1" runat="server" Text="*" Style="display: none"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtCuitDestinoCambio" EventName="TextChanged" />
                        </Triggers>
                    </asp:UpdatePanel>

                </td>
            </tr>
            <tr>
                <td class="style42" colspan="3">
                    <table>
                        <tr>
                            <td class="style14">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td class="style5">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
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
                <td>
                    &nbsp;
                </td>
                <td class="style58">
                    &nbsp;
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td class="style58">
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td>
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" 
                        onclick="btnGuardar_Click" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td class="style58" align="right">
                </td>
                <td align="right">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

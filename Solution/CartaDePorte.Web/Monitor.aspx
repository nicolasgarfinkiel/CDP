<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Monitor.aspx.cs" Inherits="CartaDePorte.Web.Monitor" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .style2 {
            width: 293px;
        }
    </style>--%>

    <script type="text/jscript" language="javascript">

        $(document).ready(function () {

            $('#infoEmpresa').css({ "visibility": "hidden" });

        });


        $(window).scroll(function () {
            if ($(window).scrollTop() + $(window).height() > $(document).height() - .001 * $(document).height()) {
                //el scroll esta abajo
            }
        });

        function ReenviarSAP(idsolicitud) {
            document.getElementById('ctl00_ContentPlaceHolder1_ConfirmacionReproceso').style.visibility = "visible";
            document.getElementById('idSolicitudReenviar').value = idsolicitud;
            document.getElementById('ctl00_ContentPlaceHolder1_lblTituloReproceso').value = "¿ Desea reenviar esta solicitud " + idsolicitud + " a SAP ?";
        }

        function ocultar() {
            document.getElementById('ctl00_ContentPlaceHolder1_ConfirmacionReproceso').style.visibility = "hidden";
        }

    </script>

    <style type="text/css">
        .tabla1 {
            width: 100%;
            border-color: 1;
            border-style: solid;
            border-width: thin;
        }

        .style2 {
        }

        .style3 {
            width: 564px;
        }

        .style5 {
            width: 100%;
        }

        .style6 {
            width: 280px;
        }

        .style8 {
            width: 347px;
        }

        .style9 {
            width: 77px;
        }

        .style10 {
            width: 471px;
        }

        .styleLink {
            font-family: Arial, Helvetica, sans-serif;
            font-size: small;
            color: #009933;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <input type="hidden" id="idSolicitudReenviar" name="idSolicitudReenviar" value="" />
    <br />
    <br />
    <div class="divTopPage2">
    </div>
    <br />
    <asp:UpdatePanel ID="upTblData" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <table style="width: 929px">
                <tr>
                    <td class="style10">
                        <asp:Label ID="Label3" runat="server" Text="Monitoreo de Carta de Porte" CssClass="TituloReporte"></asp:Label>
                    </td>
                    <td class="styleLink" align="right">
                        <asp:LinkButton ID="linkConfirmacionDeArribo" runat="server" OnClick="linkConfirmacionDeArribo_Click">Confirmaciones de Arribo</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td class="style10"></td>
                    <td class="styleLink" align="right">
                        <asp:UpdatePanel ID="panelRechazado" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:LinkButton ID="linkTrasladosRechazados" runat="server" OnClick="linkTrasladosRechazados_Click">Cambio Destino y Destinatario a CTG Rechazado</asp:LinkButton>
                                <asp:UpdateProgress ID="UpdateProgressRechazado" runat="server" AssociatedUpdatePanelID="panelRechazado">
                                    <ProgressTemplate>
                                        <div style="position: fixed; top: 0px; bottom: 0px; left: 0px; right: 0px; overflow: hidden; padding: 0; margin: 0; background-color: #000000; filter: alpha(opacity=50); opacity: 0.5; z-index: 100000;">
                                        </div>
                                        <div style="position: fixed; top: 40%; left: 40%; height: 20%; width: 20%; z-index: 100010; background-color: #FFFFFF; border: 2px solid #000000;">
                                            <center>
                                                <br />
                                                <br />
                                                Consultando Rechazados.
                                                <br />
                                                Por favor, espere...</center>
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <table class="tabla1">
                <tr>
                    <td class="style6">
                        <asp:Label ID="Label2" runat="server" Text="CDP/CTG/Usuario/Establecimiento Origen"></asp:Label>
                    </td>
                    <td class="style3">
                        <asp:TextBox ID="txtBuscar" runat="server" Width="535px" OnTextChanged="txtBuscar_TextChanged"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="style2" colspan="3">
                        <table class="style5">
                            <tr>
                                <td class="style6">
                                    <asp:Label ID="Label4" runat="server" Text="Estado AFIP"></asp:Label>
                                </td>
                                <td class="style8">
                                    <asp:DropDownList ID="cboEstadoAfip" runat="server" Height="21px" Style="margin-left: 0px"
                                        Width="200px">
                                    </asp:DropDownList>
                                </td>
                                <td class="style9">
                                    <asp:Label ID="Label5" runat="server" Text="Estado SAP"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cboEstadoSAP" runat="server" Height="21px" Style="margin-left: 0px"
                                        Width="200px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cboIdEmpresa" runat="server" onchange="cboEmpresa_OnChange(this)"></asp:DropDownList>
                                </td>
                            </tr>
                            <%--                        
                        <tr>
                            <td class="style6">
                                <asp:Label ID="Label6" runat="server" Text="Fecha Desde"></asp:Label>
                            </td>
                            <td class="style8">
                                <asp:TextBox ID="txtFechaDesde" runat="server"></asp:TextBox>
                            </td>
                            <td class="style9">
                                <asp:Label ID="Label7" runat="server" Text="Fecha Hasta"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFechaHasta" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                            --%>
                        </table>
                    </td>
                </tr>
            </table>
            <br />
            <asp:Label ID="lblCantidadResultados" runat="server" Text=""></asp:Label>
            <br />
            <div>
                <%--<div style="overflow: auto; height: 370px;">--%>
                <asp:Table ID="tblData" runat="server">
                </asp:Table>
            </div>
            <asp:Label ID="lblCantidadActual" runat="server" Text=""></asp:Label>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnCargarMas" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgressBusqueda" runat="server" AssociatedUpdatePanelID="upTblData">
        <ProgressTemplate>
            <div style="position: fixed; top: 0px; bottom: 0px; left: 0px; right: 0px; overflow: hidden; padding: 0; margin: 0; background-color: #000000; filter: alpha(opacity=50); opacity: 0.5; z-index: 100000;">
            </div>
            <div style="position: fixed; top: 40%; left: 40%; height: 20%; width: 20%; z-index: 100050; background-color: #d3d3d3; border: 1px solid #000000; background-image: url('Content/Images/ajax_loader_large.gif'); background-size: 32px 32px; background-repeat: no-repeat; background-position: center;">
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <br />
    <asp:Button ID="btnCargarMas" runat="server" Text="Ver mas Cartas de Porte" OnClick="btnCargarMas_Click" />
    <br />
    <div class="PopUpConfirmacionReenvio" id="ConfirmacionReproceso" runat="server">
        <table style="width: 400px; height: 150px">
            <tr>
                <td>
                    <br />
                    <center>
                        <asp:Label ID="lblTituloReproceso" name="lblTituloReproceso" runat="server" Text="¿ Desea reenviar esta solicitud a SAP ?"
                            CssClass="SubTitulos"></asp:Label><br />
                    </center>
                </td>
            </tr>
            <tr>
                <td>
                    <center>
                        <asp:Button ID="btnCancelarAnular" runat="server" Text="Si, reenviar a SAP." OnClick="btnCerrarCliente_Click" />
                        <br />
                        <asp:Button ID="btnAhorano" runat="server" Text="Ahora no" OnClientClick="javascript:ocultar();" /></center>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

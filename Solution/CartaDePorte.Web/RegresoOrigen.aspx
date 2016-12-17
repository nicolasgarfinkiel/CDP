<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="RegresoOrigen.aspx.cs" Inherits="CartaDePorte.Web.RegresoOrigen" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .style2 {
            width: 226px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div>
        <table style="width: 929px">
            <tr>
                <td class="style10">
                    <br />
                    <asp:Label ID="Label3" runat="server" Text="Regresar a Origen" CssClass="TituloReporte"></asp:Label>
                    <br />
                    <br />
                    <table class="style1">
                        <tr>
                            <td class="style2">
                                <asp:Label ID="lblNroCartaDePorte" runat="server" Text="Numero Carta de Porte"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="txtNroCartaDePorte" runat="server" Text="Numero Carta de Porte" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Height="20px" Width="400px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <asp:Label ID="lblCtg" runat="server" Text="Numero de CTG"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="txtCtg" runat="server" Text="Numero de CTG" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Height="20px" Width="400px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <asp:Label ID="lblEstablecimientoOrigen" runat="server" Text="Establecimiento Origen"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="txtEstablecimientoOrigen" runat="server" Text="Establecimiento Origen" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Height="20px" Width="400px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <asp:Label ID="lblEstablecimientoDestino" runat="server" Text="Establecimiento Destino"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="txtEstablecimientoDestino" runat="server" Text="Establecimiento Destino" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Height="20px" Width="400px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <asp:Label ID="lblChofer" runat="server" Text="Chofer"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="txtChofer" runat="server" Text="Chofer" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Height="20px" Width="400px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <asp:Label ID="lblFechaCreacion" runat="server" Text="Fecha Generacion"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="txtFechaCreacion" runat="server" Text="Fecha Generacion" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Height="20px" Width="400px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <table class="style1">
                        <tr>
                            <td>






                                <asp:UpdatePanel ID="panelRechazado" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="btnRAO" runat="server" OnClick="btnRAO_Click"
                                            Text="Regresar a Origen" Width="160px" />
                                        <asp:UpdateProgress ID="UpdateProgressRechazado" runat="server" AssociatedUpdatePanelID="panelRechazado">
                                            <ProgressTemplate>
                                                <div style="position: fixed; top: 0px; bottom: 0px; left: 0px; right: 0px; overflow: hidden; padding: 0; margin: 0; background-color: #000000; filter: alpha(opacity=50); opacity: 0.5; z-index: 100000;">
                                                </div>
                                                <div style="position: fixed; top: 40%; left: 40%; height: 20%; width: 20%; z-index: 100010; background-color: #FFFFFF; border: 2px solid #000000;">
                                                    <center>
                                                        <br />
                                                        <br />
                                                        Procesando la vuelta a origen
                                                        <br />
                                                        Por favor, espere...</center>
                                                </div>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </ContentTemplate>
                                </asp:UpdatePanel>




                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

    </div>
</asp:Content>

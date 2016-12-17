<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="BandejaDeEntrada.aspx.cs" Inherits="CartaDePorte.Web.BandejaDeEntrada" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/jscript" language="javascript">

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
                        <asp:Label ID="Label3" runat="server" Text="Búsqueda de Carta de Porte Recibida" CssClass="TituloReporte"></asp:Label>
                    </td>
                    <td class="styleLink" align="right">
                        <asp:Button ID="btnNueva" runat="server"
                            Text="Nueva" Width="78px" OnClick="btnNueva_Click" />
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
                    <td class="style2" colspan="3"></td>
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
</asp:Content>

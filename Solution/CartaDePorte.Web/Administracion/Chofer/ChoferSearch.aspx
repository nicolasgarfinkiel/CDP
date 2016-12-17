<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ChoferSearch.aspx.cs" Inherits="CartaDePorte.Web.ChoferSearch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .tabla1
        {
        	width: 100%;
            border-color: 1;
            border-style: solid;
            border-width: thin;
        }
        .style2
        {
            width: 215px;
        }
        .style3
        {
            width: 564px;
        }
        .style4
        {
            width: 215px;
            height: 30px;
        }
        .style5
        {
            width: 564px;
            height: 30px;
        }
        .style6
        {
            height: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        <br />
        <div class="divTopPage3">
        </div>
        <br />
        <asp:Label ID="Label1" runat="server" Text="Buscador de Choferes" CssClass="TituloReporte"></asp:Label>
        <br />
        <br />
        <table class="tabla1">
            <tr>
                <td class="style4">
        <asp:Label ID="Label2" runat="server" Text="Nombre / Apellido / CUIT Chofer"></asp:Label>
                </td>
                <td class="style5">
                    <asp:TextBox ID="txtBuscar" runat="server" Width="552px"></asp:TextBox>
                </td>
                <td class="style6">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" 
                        onclick="btnBuscar_Click" />
                </td>
                <td class="style6">
                    <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" 
                        onclick="btnNuevo_Click" />
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td class="style3">
                    <asp:CheckBox ID="chkTransportista" runat="server" Text="Solo Transportistas"/></td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        <br />
        <asp:Table ID="tblData" runat="server">
        </asp:Table>
        <br />
        <br />
        <br />
        <br />
        <br /><br />
        <br /><br />
        <br /><br />
    </p>
    
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Sox1116ASearch.aspx.cs" Inherits="CartaDePorte.Web.Sox1116ASearch" %>
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        <br />
        <div class="divTopPage3">
        </div>
        <br />

        <asp:Label ID="Label1" runat="server" Text="Buscador de Carta de Porte para 1116A" CssClass="TituloReporte"></asp:Label>
        <br />
        <br />
        <table class="tabla1">
            <tr>
                <td >
        <asp:Label ID="Label2" runat="server" Text="CDP / CTG / Usuario / Establecimiento Origen"></asp:Label>
                </td>
                <td class="style3">
                    <asp:TextBox ID="txtBuscar" runat="server" Width="480px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" 
                        onclick="btnBuscar_Click" />
                </td>
            </tr>
        </table>
        <br />
        <asp:Label ID="lblCantidadResultados" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <asp:Table ID="tblData" runat="server">
        </asp:Table>
        <br />
        <br />
        <br /><br />
        <br /><br />
        <br /><br />
    </p>
    
</asp:Content>

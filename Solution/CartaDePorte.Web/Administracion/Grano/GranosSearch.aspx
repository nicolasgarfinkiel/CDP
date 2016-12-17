<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="GranosSearch.aspx.cs" Inherits="CartaDePorte.Web.GranosSearch" %>
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
        <asp:Label ID="Label1" runat="server" Text="Buscador de Granos" CssClass="TituloReporte"></asp:Label>
        <br />
        <br />
        <table class="tabla1">
            <tr>
                <td class="style2">
        <asp:Label ID="Label2" runat="server" Text="Descripcion / Cosecha / Especie"></asp:Label>
                </td>
                <td class="style3">
                    <asp:TextBox ID="txtBuscar" runat="server" Width="552px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" 
                        onclick="btnBuscar_Click" /></td><td>
                    <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" 
                        onclick="btnNuevo_Click" />
                </td>
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

<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="TrasladosRechazados.aspx.cs" Inherits="CartaDePorte.Web.TrasladosRechazados" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .tabla1
        {
        	width: 100%;
            border-color: 1;
            border-style: solid;
            border-width: thin;
        }
        .style10
        {
            width: 471px;
        }
        .styleLink
        {
	        font-family: Arial, Helvetica, sans-serif;
	        font-size: small;
	        color: #009933;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <br />
        <table style="width: 929px">
            <tr>
                <td class="style10">
                    <asp:Label ID="Label3" runat="server" Text="Cambio Destino y Destinatario a CTG Rechazado" CssClass="TituloReporte"></asp:Label>
                </td>
                    <td class="styleLink" align="right">
                </td>
            </tr>
        </table>
        <br />
        <asp:Label ID="lblCantidadResultados" runat="server" Text=""></asp:Label>
        <br />
        <div style="overflow: auto; height: 370px;">
                <asp:Table ID="tblData" runat="server"></asp:Table>
        </div>
        <br />
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CambioDestino.aspx.cs" Inherits="CartaDePorte.Web.CambioDestino" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/js/webkit.js" />
        </Scripts>
    </asp:ScriptManager>


<DIV>
        <table style="width: 929px">
            <tr>
                <td class="style10">
                    <asp:Label ID="Label3" runat="server" Text="Cambio Destino / Destinatario" CssClass="TituloReporte"></asp:Label>
                </td>
            </tr>
        </table>

</DIV>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="rechazar.aspx.cs" Inherits="CartaDePorte.Web.rechazar" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div>
        <table style="width: 929px">
            <tr>
                <td class="style10">
                    <asp:Label ID="lblEstadoRechazo" runat="server" Text="" CssClass="TituloReporte"></asp:Label>
                </td>
            </tr>
        </table>

    </div>
</asp:Content>

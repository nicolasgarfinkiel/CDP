<%@ Page Title="Carta de Porte - Seguridad" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SinAutorizacion.aspx.cs" Inherits="CartaDePorte.Web.SinAutorizacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%--    <div style="background-color: #F6F6F6; height: 147px;">
        <div style="font-family: Arial, Helvetica, sans-serif; font-size: x-large; color: #FFFFFF; xbackground-color: #0299A5; height: 46px;">&nbsp;&nbsp; Carta de Porte</div><br/>--%>
    <br />
    <br />
    <div class="divTopPage2">
    </div>
        <br/>
        &nbsp;&nbsp;

        <asp:Label ID="Label2" class="SinPermisos" runat="server" 
            Text="Usted no tiene permisos sobre esta sección" Font-Names="Arial" 
            Font-Size="Large" ForeColor="#FF0000"></asp:Label>
        <br/>
        <br/>
        <br/>
    
    <%--</div>--%>

</asp:Content>


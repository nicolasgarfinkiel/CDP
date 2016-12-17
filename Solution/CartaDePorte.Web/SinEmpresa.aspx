<%@ Page Title="Carta de Porte - Seguridad" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SinEmpresa.aspx.cs" Inherits="CartaDePorte.Web.SinEmpresa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="background-color: #F6F6F6; height: 147px;">
        <div style="font-family: Arial, Helvetica, sans-serif; font-size: x-large; color: #FFFFFF; background-color: #0299A5; height: 46px;">&nbsp;&nbsp; Carta de Porte</div><br/>
        <br/>
        &nbsp;&nbsp;
        <asp:Label ID="Label2" class="SinPermisos" runat="server" 
            Text="SinEmpresa" Font-Names="Arial" 
            Font-Size="Large" ForeColor="#FF0000"></asp:Label>
        <br/>
        <br/>
        <br/>
    
    </div>

</asp:Content>


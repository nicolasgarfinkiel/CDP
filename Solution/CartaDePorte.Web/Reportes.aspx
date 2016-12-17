<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="CartaDePorte.Web.Reportes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 293px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <br />
        <br />
        <div class="divTopPage4">
        </div>
        <br />
        <asp:Label ID="Label1" runat="server" Text="Reportes e Interfaces" CssClass="TituloReporte"></asp:Label>
        <br />
        <br />
    <div class="pagemenure">
        <ul>
            <li><a href="Administracion/Exportar/CartasDePorteExportarCSV.aspx">Exportar Cartas de Porte</a></li>
            <li><a href="Administracion/Exportar/CPE.aspx">Cartas de Porte Emitidas (CPE)</a></li>
            <li><a href="Administracion/Exportar/CPR.aspx">Cartas de Porte Recibidas (CPR)</a></li>
            <li><a href="Administracion/Exportar/ReporteActividad.aspx">Actividad</a></li>
            <li id="liConsulta1116A" runat="server"><a href="Administracion/Exportar/Consulta1116A.aspx">Consulta 1116A</a></li>
        </ul>
    </div>
<BR/><BR/><BR/><BR/><BR/><BR/><BR/><BR/><BR/><BR/><BR/>
    <BR/><BR/><BR/><BR/><BR/><BR/><BR/><BR/><BR/><BR/><BR/>
</asp:Content>

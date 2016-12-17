<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Administracion.aspx.cs" Inherits="CartaDePorte.Web.Administracion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .style2 {
            width: 293px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <div class="divTopPage1">
    </div>
    <br />
    <asp:Label ID="Label1" runat="server" Text="Administración" CssClass="TituloReporte"></asp:Label>
    <br />
    <br />
    <div class="pagemenu" style="height:250px;">
        <ul>
            <li><a href="Administracion/Chofer/ChoferSearch.aspx">ABM Chofer</a></li>
            <li><a href="Administracion/Grano/GranosSearch.aspx">ABM Grano</a></li>
            <li><a href="Administracion/Establecimiento/EstablecimientoSearch.aspx">ABM Establecimiento</a></li>
            <li><a href="Administracion/CartasDePorte/CartasDePorteSearch.aspx">ABM Rangos de Cartas de Porte</a></li>
            <li><a href="Administracion/Empresa/EmpresaSearch.aspx">ABM Grupo Empresa - Empresa</a></li>
        </ul>
    </div>
    <div class="pagemenu">
        <ul>
            <%--<li><a href="Administracion/CartasDePorte/ABMCartasDePorte.aspx">Alta de Rango de Cartas de Porte</a></li>--%>
            <li><a href="Administracion/Exportar/ConsultaRangosCartasDePorte.aspx">Consulta de Rango de Cartas de Porte</a></li>
            <li id="liAsociar1116A" runat="server"><a href="Administracion/1116A/Sox1116ASearch.aspx">Asociacion formulario 1116A</a></li>
        </ul>
    </div>
    <div class="pagemenu">
        <ul>
            <li><a href="Administracion/CartasDePorte/MisReservas.aspx">Reservas de Cartas de Porte</a></li>
            <li><a href="Administracion/CartasDePorte/Reservas.aspx">Todas las Reservas Pendientes de Carga</a></li>
            <li><a href="IndexManual.aspx">Carga Solicitud Manual</a></li>
            <%--<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />--%>
        </ul>
    </div>


    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Reservas.aspx.cs" Inherits="CartaDePorte.Web.Reservas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/jscript" language="javascript">

        function confirmarReserva() {
            return window.confirm("¿ Desea reservar un numero de carta de porte ?")
        }

    </script>


    <style type="text/css">
        .tabla1
        {
        	width: 100%;
            border-color: 1;
            border-style: solid;
            border-width: thin;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        <br />
        <div class="divTopPage3">
        </div>
        <br />

        <asp:Label ID="Label3" runat="server" Text="Todas las Reservas pendientes de carga" 
            CssClass="TituloReporte"></asp:Label>
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

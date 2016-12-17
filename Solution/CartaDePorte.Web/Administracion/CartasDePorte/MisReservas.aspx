<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="MisReservas.aspx.cs" Inherits="CartaDePorte.Web.MisReservas" %>
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
        .style3
        {
            width: 237px;
        }
        .style4
        {
            width: 201px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        <br />
        <div class="divTopPage3">
        </div>
        <br />

        <asp:Label ID="Label1" runat="server" Text="Reserva de Carta de Porte" CssClass="TituloReporte"></asp:Label>
        <br />
        <br />
              <asp:Label ID="lblCantidadCartasDisponibles" runat="server" Text=""></asp:Label>        
        <br />
        <table class="tabla1">
            <tr>
                <td class="style4">
              <asp:Label ID="lblTipoCartaDePorte" runat="server" 
                        Text="Tipo Carta de Porte"></asp:Label>        
                </td>
                <td>
                    <asp:DropDownList ID="cboTipoCartaDePorte" runat="server" 
                        Height="21px" Width="400px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style4">
              <asp:Label ID="lblEstablecimientoAsociado" runat="server" Text="Establecimiento Procedencia"></asp:Label>        
                </td>
                <td>
                    <asp:DropDownList ID="cboEstablecimientoProcedencia" runat="server" Height="21px" Width="400px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style4">
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btnReservar" runat="server" Text="Reservar" OnClientClick="return confirmarReserva();" 
                        Width="180px" onclick="btnReservar_Click" />
                </td>
            </tr>
            <tr>
                <td class="style4">
                    &nbsp;</td>
                <td>
              <asp:Label ID="lblMensaje" runat="server" 
                        Text=""></asp:Label>        
                </td>
            </tr>
        </table>
        <br />
        <asp:Label ID="Label3" runat="server" Text="Mis Reservas pendientes de carga" 
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
        
        
        <div class="PopUpConfirmacion" id="ConfirmacionCancelacionAnulacion" runat="server" visible="false">
            <table style="width: 400px; height: 150px">
                <tr>
                    <td>
                        <br /><center>
                        <asp:Label ID="lblTituloCancelacionAnulacion" runat="server" Text="" CssClass="SubTitulos"></asp:Label><br /></center>
                    </td>
               </tr>
               <tr>
                    <td>
                        <center><asp:Button ID="btnCancelarAnular" runat="server" Text="" 
                            onclick="btnCerrarCliente_Click"/>
                            <asp:Button ID="btnAhorano" runat="server" Text="Ahora no" 
                            onclick="btnAhorano_Click"/></center>
                    </td>
                </tr>
            </table>            
        </div>
        
    </p>
    
</asp:Content>

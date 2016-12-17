<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SoxABM1116A.aspx.cs" Inherits="CartaDePorte.Web.SoxABM1116A" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/jscript" language="javascript">

        function ValidarEliminar() {
            return window.confirm("¿ Desea eliminar el formulario 1116A ?")
        }

    </script>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 153px;
        }
        .style3
        {
            width: 375px;
        }
        .style33
        {
        	width: 100%;
	        height: 5px;
	        background-color: #DAE0C7;
	        margin-top:18px;	
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        $(function() {
            $("[id$=txtFecha]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                dateFormat: "dd/mm/yy",
                buttonImage: '../../Content/Images/Calendar.gif'
            });
        });
    </script>
        <br />
        <div class="style33">
        </div>
        <br />
        <asp:Label ID="Label1" runat="server" Text="Formulario 1116A" CssClass="TituloReporte"></asp:Label>
        <br />
        <br />
        <table class="style1">
            <tr>
                <td>
                    <asp:Label ID="lblNumeroCartaDePorte" runat="server" Text="Numero Carta de Porte"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtNumeroCartaDePorte" runat="server"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCTG" runat="server" 
                        Text="Numero de CTG"></asp:Label>
                </td>
                <td class="style3">
                    <asp:Label ID="txtCTG" runat="server"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTipoCartaDePorte" runat="server" 
                        Text="Tipo Carta de Porte"></asp:Label>
                </td>
                <td class="style3">
                    <asp:Label ID="txtTipoCartaDePorte" runat="server"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" 
                        Text="Estado en AFIP"></asp:Label>
                </td>
                <td class="style3">
                    <asp:Label ID="txtEstadoEnAFIP" runat="server"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td class="style3">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label2" runat="server" Text="Numero de 1116A"></asp:Label></td>
                <td class="style3">
                    <asp:TextBox ID="txtNumero1116A" placeholder="Ingrese Numero de 1116A" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label3" runat="server" Text="Fecha de 1116A"></asp:Label></td>
                <td class="style3">
                    <asp:TextBox ID="txtFecha" runat="server" Width="90px" ReadOnly="true"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td class="style3">
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td class="style3">
                    <asp:Button ID="Button1" runat="server" Text="Guardar" 
                        onclick="Button1_Click" />
                </td>
                <td>
                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar"
                        onclick="btnEliminar_Click"  OnClientClick="return ValidarEliminar()" />
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td class="style3">
                    &nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <br />
 
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CPR.aspx.cs" Inherits="CartaDePorte.Web.CPR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
            border: 1px solid #C0C0C0;
        }
        .style2
        {
            width: 100px;
        }
        .style3
        {
        }
        .style4
        {
            width: 126px;
        }
        .style5
        {
            width: 179px;
        }
        .style6
        {
            width: 94px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        $(function() {
            $("[id$=txtDateDesde]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                dateFormat: "dd/mm/yy",
                buttonImage: '../../Content/Images/Calendar.gif'
            });
        });

        $(function() {
            $("[id$=txtDateHasta]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                dateFormat: "dd/mm/yy",
                buttonImage: '../../Content/Images/Calendar.gif'
            });
        });
    </script>
   
        <br />
        <br />
        <div class="divTopPage4">
        </div>
        <br />

    <asp:Label ID="Label3" runat="server" Text="Cartas de Porte Recibidas" CssClass="TituloReporte"></asp:Label>
    <br />
    <br />
    <table class="style1">
        <tr>
            <td class="style2">
                <asp:Label ID="Label2" runat="server" Text="Fecha Desde"></asp:Label>
            </td>
            <td class="style3">
    
    <asp:TextBox ID="txtDateDesde" runat="server" ReadOnly = "true" Width="90px" 
                    EnableViewState="False"></asp:TextBox>
            </td>
            <td class="style4">
                <asp:Label ID="Label1" runat="server" Text="Fecha Hasta"></asp:Label>
            </td>
            <td class="style5">
    <asp:TextBox ID="txtDateHasta" runat="server" ReadOnly = "true" Width="90px"></asp:TextBox>
    
            </td>
            <td class="style6">
                <asp:Button ID="Button2" runat="server" Text="Buscar" Width="150px" 
                    onclick="Button2_Click"/>
            </td>
            <td>
                <asp:Button ID="Button1" runat="server" Text="Exportar interfaz" 
                    onclick="Button1_Click" Width="150px" />
            </td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td class="style3" colspan="5">    
                <asp:Label ID="lblMensaje" runat="server" ForeColor="#CC3300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td class="style3" colspan="5">    
                &nbsp;</td>
        </tr>
    </table>
    <BR/><BR/>
    <div id="divTable" style="height: 400px; width:100%; overflow: auto;">
        <asp:Table ID="tblData" runat="server">
        </asp:Table>
    </div>    
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="BandejaDeSalidaConfirmacion.aspx.cs" Inherits="CartaDePorte.Web.BandejaDeSalidaConfirmacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .tabla1 {
            width: 100%;
            border-color: 1;
            border-style: solid;
            border-width: thin;
        }

        .style2 {
        }

        .style3 {
            width: 564px;
        }

        .style5 {
            width: 100%;
        }

        .style6 {
            width: 280px;
        }

        .style8 {
            width: 347px;
        }

        .style9 {
            width: 77px;
        }

        .style10 {
            width: 471px;
        }

        .styleLink {
            font-family: Arial, Helvetica, sans-serif;
            font-size: small;
            color: #009933;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <br />
    <table style="width: 929px">
        <tr>
            <td class="style10">
                <asp:Label ID="Label3" runat="server" Text="Confirmaciones de Arribo Pendientes" CssClass="TituloReporte"></asp:Label>
            </td>
            <td class="styleLink" align="right"></td>
        </tr>
    </table>
    <br />
    <br />
    <table class="tabla1">
        <tr>
            <td class="style6">
                <asp:Label ID="Label2" runat="server" Text="Empresa/Establecimiento Destino/Usuario"></asp:Label>
            </td>
            <td class="style3">
                <asp:TextBox ID="txtBuscar" runat="server" Width="535px"
                    OnTextChanged="txtBuscar_TextChanged"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar"
                    OnClick="btnBuscar_Click" />
            </td>
        </tr>
        <tr>
            <td class="style2" colspan="3"></td>
        </tr>
    </table>
    <br />
    <asp:Label ID="lblCantidadResultados" runat="server" Text=""></asp:Label>
    <br />
    <div style="overflow: auto; height: 370px;">
        <asp:Table ID="tblData" runat="server"></asp:Table>
    </div>
    <br />
</asp:Content>

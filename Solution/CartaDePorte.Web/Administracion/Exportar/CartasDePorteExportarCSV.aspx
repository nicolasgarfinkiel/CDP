<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="CartasDePorteExportarCSV.aspx.cs" Inherits="CartaDePorte.Web.CartasDePorteExportarCSV" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1 {
            width: 100%;
            border: 1px solid #C0C0C0;
        }

        .style2 {
            width: 100px;
        }

        .style3 {
        }

        .style4 {
            width: 126px;
        }

        .style5 {
            width: 181px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">

        $(function () {
            $("[id$=txtDateDesde]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                dateFormat: "dd/mm/yy",
                buttonImage: '../../Content/Images/Calendar.gif'
            });
        });

        $(function () {
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
    <asp:Label ID="Label3" runat="server" Text="Exportar Cartas de Porte por Rangos de Fecha de Emision"
        CssClass="TituloReporte"></asp:Label>
    <br />
    <br />

    <table>
        <tr>
            <td class="style2">&nbsp;
            </td>
            <td class="style3" colspan="4">
                <asp:Label ID="lblMensaje" runat="server" ForeColor="#CC3300"></asp:Label>
            </td>
        </tr>
    </table>
    <table class="style1">
        <tr>
            <td class="style2">Fecha Desde: 
            </td>
            <td class="style3">
                <asp:TextBox ID="txtDateDesde" runat="server" ReadOnly="true" Width="90px" EnableViewState="False"></asp:TextBox>
            </td>
            <td class="style4">Fecha Hasta: 
            </td>
            <td class="style5">
                <asp:TextBox ID="txtDateHasta" runat="server" ReadOnly="true" Width="90px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Titular CDP: </td>
            <td>
                <asp:DropDownList ID="cboTitular" runat="server" Height="21px"
                    Style="margin-left: 0px" Width="350px">
                </asp:DropDownList></td>
            <td>Intermediario: </td>
            <td>
                <asp:DropDownList ID="cboIntermediario" runat="server" Height="21px"
                    Style="margin-left: 0px" Width="350px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td>Remitente Comercial: </td>
            <td>
                <asp:DropDownList ID="cboRemitenteComercial" runat="server" Height="21px"
                    Style="margin-left: 0px" Width="350px">
                </asp:DropDownList></td>
            <td>Corredor: </td>
            <td>
                <asp:DropDownList ID="cboCorredor" runat="server" Height="21px"
                    Style="margin-left: 0px" Width="350px">
                </asp:DropDownList></td>

        </tr>
        <tr>
            <td>Representante/Entregador: </td>
            <td>
                <asp:DropDownList ID="cboRepresentanteEntregador" runat="server" Height="21px"
                    Style="margin-left: 0px" Width="350px">
                </asp:DropDownList></td>

            <td>Destinatario: </td>
            <td>
                <asp:DropDownList ID="cboDestinatario" runat="server" Height="21px"
                    Style="margin-left: 0px" Width="350px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td>Transportista: </td>
            <td>
                <asp:DropDownList ID="cboTransportista" runat="server" Height="21px"
                    Style="margin-left: 0px" Width="350px">
                </asp:DropDownList></td>
            <td>Chofer: </td>
            <td>
                <asp:DropDownList ID="cboChofer" runat="server" Height="21px"
                    Style="margin-left: 0px" Width="350px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td>Cosecha: </td>
            <td>
                <asp:DropDownList ID="cboCosecha" runat="server" Height="21px"
                    Style="margin-left: 0px" Width="350px">
                </asp:DropDownList></td>
            <td>Grano: </td>
            <td>
                <asp:DropDownList ID="cboGrano" runat="server" Height="21px"
                    Style="margin-left: 0px" Width="350px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td>Procedencia: </td>
            <td>
                <asp:DropDownList ID="cboProcedencia" runat="server" Height="21px"
                    Style="margin-left: 0px" Width="350px">
                </asp:DropDownList></td>
            <td>Lugar de destino: </td>
            <td>
                <asp:DropDownList ID="cboDestino" runat="server" Height="21px"
                    Style="margin-left: 0px" Width="350px">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <asp:Button ID="Button1" runat="server" Text="Exportar" OnClick="Button1_Click" />
            </td>
        </tr>
    </table>
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

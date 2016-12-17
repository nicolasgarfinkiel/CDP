<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ABMCartasDePorte.aspx.cs" Inherits="CartaDePorte.Web.ABMCartasDePorte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/jscript" language="javascript">
        function ValidarCargar() {
            if ($("#<%= txtFechaDesde.ClientID %>").val() == '')
                return window.confirm("¿ Esta seguro que selecciono un archivo PDF con un RANGO CORRELATIVO de cartas de porte y la numeracion ingresada en los campos DESDE y HASTA coincide con el contenido del archivo? Tambien es importante verificar la correcta carga del CEE y la fecha de vencimiento. Si esta seguro continue presionando OK. De lo contrario verifique los datos antes de continuar.")
            else
                true;
        }
    </script>
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .style2 {
            width: 153px;
        }

        .style3 {
            width: 375px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $("[id$=txtFechaVencimiento]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                dateFormat: "dd/mm/yy",
                buttonImage: '../../Content/Images/Calendar.gif'
            });
        });

        $(function () {
            $("[id$=txtFechaDesde]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                dateFormat: "dd/mm/yy",
                buttonImage: '../../Content/Images/Calendar.gif'
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div class="divTopPage3">
    </div>
    <br />
    <asp:Label ID="Label1" runat="server" Text="Alta de Rango de Cartas de Porte solicitadas a la AFIP" CssClass="TituloReporte"></asp:Label>
    <br />
    <br />
    <asp:Label ID="lblCantidadCartasDisponibles" runat="server" Text=""></asp:Label>
    <br />
    <br />
    <table class="style1">
        <tr id="trSucPto" runat="server" visible="false">
            <td colspan="2">
                <table>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="lblSucursal" runat="server" Text="Sucursal Nro"></asp:Label>
                        </td>
                        <td class="style3">
                            <asp:TextBox ID="txtSucursal" runat="server" Width="50px" MaxLength="3"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="lblPtoEmision" runat="server" Text="Punto Emisión"></asp:Label>
                        </td>
                        <td class="style3">
                            <asp:TextBox ID="txtPtoEmision" runat="server" Width="50px" MaxLength="3"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="Label5" runat="server" Text="Timbrado de Habilitacion Nro"></asp:Label>
                        </td>
                        <td class="style3">
                            <asp:TextBox ID="txtHabilitacion" runat="server" Width="100px" MaxLength="13"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="Label2" runat="server" Text="Rango CDP Desde"></asp:Label>
            </td>
            <td class="style3">
                <asp:TextBox ID="txtRangoDesde" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="Label3" runat="server" Text="Rango CDP Hasta"></asp:Label>
            </td>
            <td class="style3">
                <asp:TextBox ID="txtRangoHasta" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="Label4" runat="server" Text="Numero de CEE"></asp:Label>
            </td>
            <td class="style3">
                <asp:TextBox ID="txtCee" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr runat="server" id="trEstablecimientoOrigen" visible="true">
            <td class="style2">
                <asp:Label ID="lblEstablecimientoOrigen" runat="server" Text="Establecimiento Origen"></asp:Label>
            </td>
            <td class="style3">
                <asp:DropDownList ID="cboEstablecimientoOrigen" runat="server" Height="21px" Width="200px">
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr id="trFechaDesde" runat="server" visible="false">
            <td class="style2">
                <asp:Label ID="lblFechaDesde" runat="server" Text="Fecha Desde"></asp:Label>
            </td>
            <td class="style3">
                <asp:TextBox ID="txtFechaDesde" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="lblFechaVencimiento" runat="server" Text="Fecha Vencimiento"></asp:Label>
            </td>
            <td class="style3">
                <asp:TextBox ID="txtFechaVencimiento" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr id="trFile" runat="server">
            <td class="style2">
                <asp:Label ID="lblArchivoAfip" runat="server" Text="Archivo PDF Afip"></asp:Label>
            </td>
            <td class="style3">
                <asp:FileUpload ID="FileUpload1" runat="server" Width="300px"></asp:FileUpload>
            </td>
            <td>Importante: El Archivo PDF descargado de AFIP debe contener un <b>rango 
                    correlativo</b> de cartas de porte.</td>
        </tr>
        <tr>
            <td class="style2">&nbsp;</td>
            <td class="style3">
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="style2">&nbsp;</td>
            <td class="style3">
                <%--<asp:Button ID="Button11" runat="server" Text="Guardar" OnClick="Button1_Click" Visible="False" />--%>
                <asp:Button ID="UploadButton" Text="Guardar" runat="server" OnClientClick="return ValidarCargar();" OnClick="UploadButton_Click"></asp:Button>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="style2">&nbsp;</td>
            <td class="style3">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <asp:Table ID="tblLoteCartasDePorte" runat="server">
    </asp:Table>
    <br />
</asp:Content>

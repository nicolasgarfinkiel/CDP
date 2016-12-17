<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ABMChofer.aspx.cs" Inherits="CartaDePorte.Web.ABMChofer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/jscript" language="javascript">

        function ValidarEliminar() {
            return window.confirm("¿ Desea eliminar el chofer ?")
        }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
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

        .auto-style1 {
            width: 180px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div class="divTopPage3">
    </div>
    <br />
    <asp:label id="Label1" runat="server" text="Chofer" cssclass="TituloReporte"></asp:label>
    <br />
    <br />
    <table class="style1">
        <tr>
            <td class="style2">
                <asp:label id="lblAcoplado0" runat="server" text="Es Transportista"></asp:label>
            </td>
            <td class="style3">
                <asp:radiobuttonlist id="rblTransportista" runat="server" height="16px" repeatdirection="Horizontal"
                    width="123px" autopostback="True"
                    onselectedindexchanged="rblTransportista_SelectedIndexChanged" cellpadding="0" cellspacing="0" textalign="Left">
                        <asp:ListItem>No</asp:ListItem>
                        <asp:ListItem>Si</asp:ListItem>
                    </asp:radiobuttonlist>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:label id="lblNombreDescripcion" runat="server" text="Nombre"></asp:label>
            </td>
            <td class="style3">
                <asp:textbox id="txtNombre" runat="server" width="354px"></asp:textbox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:label id="lblApellido" runat="server" text="Apellido"></asp:label>
            </td>
            <td class="style3">
                <asp:textbox id="txtApellido" runat="server" width="354px"></asp:textbox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:label id="lblCuit" runat="server" text="CUIT"></asp:label>
            </td>
            <td class="style3">
                <asp:textbox id="txtCuit" onkeypress="return isNumberKey(event)" runat="server" width="354px" MaxLength="11"></asp:textbox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table id="tblDomicilio" runat="server" visible="false">
                    <tr>
                        <td class="auto-style1">
                            <asp:label id="lblDomicilio" runat="server" text="Domicilio" />
                        </td>
                        <td class="style3">
                            <asp:textbox id="txtDomicilio" runat="server" width="354px" MaxLength="100"></asp:textbox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">
                            <asp:label id="lblMarca" runat="server" text="Marca del Vehículo" />
                        </td>
                        <td class="style3">
                            <asp:textbox id="txtMarca" runat="server" width="354px" MaxLength="100"></asp:textbox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:label id="lblCamion" runat="server" text="Camión"></asp:label>
            </td>
            <td class="style3">
                <asp:textbox id="txtCamion" runat="server" width="125px"></asp:textbox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:label id="lblAcoplado" runat="server" text="Acoplado"></asp:label>
            </td>
            <td class="style3">
                <asp:textbox id="txtAcoplado" runat="server" width="125px"></asp:textbox>
            </td>
        </tr>
        <tr>
            <td class="style2">&nbsp;</td>
            <td class="style3">
                <asp:label id="lblMensaje" runat="server"></asp:label>
            </td>
        </tr>
        <tr>
            <td class="style2">&nbsp;</td>
            <td class="style3">
                <asp:button id="Button1" runat="server" text="Guardar"
                    onclick="Button1_Click" />
            </td>
            <td>
                <asp:button id="btnEliminar" runat="server" text="Eliminar"
                    onclick="btnEliminar_Click" onclientclick="return ValidarEliminar()" />
            </td>
        </tr>
    </table>
    <br />
</asp:Content>

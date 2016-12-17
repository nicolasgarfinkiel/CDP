<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ABMGrano.aspx.cs" Inherits="CartaDePorte.Web.ABMGrano" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/jscript" language="javascript">
        function ValidarEliminar() {
            return window.confirm("¿ Desea eliminar el grano ?")
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

        .style4 {
            width: 153px;
            height: 24px;
        }

        .style5 {
            width: 375px;
            height: 24px;
        }

        .style6 {
            height: 24px;
        }

        .style7 {
            width: 153px;
            height: 42px;
        }

        .style8 {
            width: 375px;
            height: 42px;
        }

        .style9 {
            height: 42px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        <br />
        <div class="divTopPage3">
        </div>
        <br />

        <asp:Label ID="Label1" runat="server" Text="Grano" CssClass="TituloReporte"></asp:Label>
        <br />
        <br />
        <table class="style1">
            <tr>
                <td class="style2">
                    <asp:Label ID="Label2" runat="server" Text="Descripcion"></asp:Label>
                </td>
                <td class="style3">
                    <asp:TextBox ID="txtDescripcion" runat="server" Width="350px"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label6" runat="server" Text="Material SAP"></asp:Label>
                </td>
                <td class="style3">
                    <asp:TextBox ID="txtMaterialSAP" runat="server" Width="165px"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr id="trEspecie" runat="server">
                <td class="style2">
                    <asp:Label ID="Label3" runat="server" Text="Especie"></asp:Label>
                </td>
                <td class="style3">
                    <asp:DropDownList ID="cboEspecie" runat="server" Height="21px"
                        Style="margin-left: 0px" Width="350px">
                    </asp:DropDownList>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr id="trCosecha" runat="server">
                <td class="style2">
                    <asp:Label ID="Label4" runat="server" Text="Cosecha"></asp:Label>
                </td>
                <td class="style3">
                    <asp:DropDownList ID="cboCosecha" runat="server" Height="24px"
                        Style="margin-left: 0px" Width="350px">
                    </asp:DropDownList>

                </td>
                <td>&nbsp;</td>
            </tr>
            <tr id="trTipoGrano" runat="server">
                <td class="style2">
                    <asp:Label ID="Label5" runat="server" Text="Tipo"></asp:Label>
                </td>
                <td class="style3">

                    <asp:DropDownList ID="cboTipoGrano" runat="server" Height="24px"
                        Style="margin-left: 0px" Width="350px">
                    </asp:DropDownList>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="style4">
                    <asp:Label ID="Label7" runat="server" Text="Sujeto a Lote"></asp:Label>
                </td>
                <td class="style5">
                    <asp:TextBox ID="txtSujetoALote" runat="server" Width="165px"></asp:TextBox>
                </td>
                <td class="style6"></td>
            </tr>
            <tr>
                <td class="style7"></td>
                <td class="style8">
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td class="style9"></td>
            </tr>
            <tr>
                <td class="style2">&nbsp;</td>
                <td class="style3">
                    <asp:Button ID="Button1" runat="server" Text="Guardar"
                        OnClick="Button1_Click" />
                </td>
                <td>
                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar"
                        OnClick="btnEliminar_Click" OnClientClick="return ValidarEliminar()" />
                </td>
            </tr>
        </table>
        <br />
    </p>
</asp:Content>

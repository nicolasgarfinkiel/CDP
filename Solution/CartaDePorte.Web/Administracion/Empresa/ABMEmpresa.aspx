<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ABMEmpresa.aspx.cs" Inherits="CartaDePorte.Web.ABMEmpresa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000"
        EnablePageMethods="true" ScriptMode="Auto">
    </asp:ScriptManager>

    <asp:UpdatePanel ID="UPForm" UpdateMode="Conditional" runat="server" EnableViewState="true">
        <ContentTemplate>
            <p>
                <br />
                <div class="divTopPage3">
                </div>
                <br />
                <asp:Label ID="lblTitulo" runat="server" Text="" CssClass="TituloReporte"></asp:Label>
                <br />
                <br />
                <table class="tabla1">
                    <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                    <tr></tr>
                    <tr>
                        <td class="style4">
                            <asp:Label ID="lblGrupoEmpresa" runat="server" Text="Grupo Empresa: "></asp:Label>
                        </td>
                        <td class="style5">
                            <asp:DropDownList ID="cboGrupoEmpresa" runat="server" Height="21px" Style="margin-left: 0px"
                                Width="300px" OnSelectedIndexChanged="cboGrupoEmpresa_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                        <td class="style4">
                            <asp:Label ID="lblPais" runat="server" Text="Pais: " Visible="false"></asp:Label>
                        </td>
                        <td class="style4">
                            <asp:Label ID="lblSeleccionPais" runat="server" Text="" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                            <asp:Label ID="lblOrganizacionSAP" runat="server" Text="Organizacion: "></asp:Label>
                        </td>
                        <td class="style5">
                            <asp:DropDownList ID="cboOrganizacion" runat="server" Height="21px" Style="margin-left: 0px"
                                Width="300px" OnSelectedIndexChanged="cboOrganizacion_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                            <asp:Label ID="lblEmpresa" runat="server" Text="Empresa: "></asp:Label>
                        </td>
                        <td class="style5">
                            <asp:DropDownList ID="cboClienteEmpresa" runat="server" Height="21px" Style="margin-left: 0px; margin: 0px;"
                                Width="300px" AutoPostBack="true" OnSelectedIndexChanged="cboCliente_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td align="center">
                            <table>
                                <tr>
                                    <td class="style6">
                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar"
                                            OnClick="btnCancelar_Click" />
                                    </td>
                                    <td class="style6">
                                        <asp:Button ID="btnAceptar" runat="server" Text="Aceptar"
                                            OnClick="btnAceptar_Click" />
                                    </td>
                                    <td class="style6">
                                        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar"
                                            OnClick="btnLimpiar_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
            </p>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

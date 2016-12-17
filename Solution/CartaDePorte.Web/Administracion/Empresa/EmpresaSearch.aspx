<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="EmpresaSearch.aspx.cs" Inherits="CartaDePorte.Web.EmpresaSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/jscript" language="javascript">
        $(document).ready(function () {
            $("select").searchable();
        });
    </script>
    <style type="text/css">
        .tabla1 {
            width: 100%;
            border-color: 1;
            border-style: solid;
            border-width: thin;
        }

        .style2 {
            width: 215px;
        }

        .style3 {
            width: 564px;
        }

        .style4 {
            width: 215px;
            height: 30px;
        }

        .style5 {
            width: 564px;
            height: 30px;
        }

        .style6 {
            height: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UPForm" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <p>
                <br />
                <div class="divTopPage3">
                </div>
                <br />
                <asp:Label ID="Label1" runat="server" Text="Administración de Empresas" CssClass="TituloReporte"></asp:Label>
                <br />
                <br />
                <table class="tabla1">
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
                            <asp:Label ID="lblEmpresa" runat="server" Text="Empresa: "></asp:Label>
                        </td>
                        <td class="style5">
                            <asp:DropDownList ID="cboEmpresa" runat="server" Height="21px" Style="margin-left: 0px; margin: 0px;"
                                Width="300px" AutoPostBack="true" OnSelectedIndexChanged="cboEmpresa_SelectedIndexChanged">
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
                                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar"
                                            OnClick="btnBuscar_Click" />
                                    </td>
                                    <td class="style6">
                                        <asp:Button ID="btnNuevo" runat="server" OnClick="btnNuevo_Click" Text="Nueva Empresa" />
                                    </td>
                                    <td class="style6">
                                        <asp:Button ID="btnGrupoEmpresa" runat="server" OnClick="btnNuevoGrupo_Click" Text="Nuevo Grupo Empresa" />
                                    </td>
                                    <td class="style6">
                                        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar Filtros"
                                            OnClick="btnLimpiar_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Label ID="lblCantidadResultados" runat="server" Text=""></asp:Label>
                <asp:Table ID="tblData" runat="server">
                </asp:Table>
            </p>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

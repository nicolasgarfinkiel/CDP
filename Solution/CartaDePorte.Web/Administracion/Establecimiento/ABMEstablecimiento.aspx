<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ABMEstablecimiento.aspx.cs" Inherits="CartaDePorte.Web.ABMEstablecimiento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/jscript" language="javascript">

        function BuscadorManager2(idempresa, control, valordescripcion, valorcuit) {
            var descripcion = "ctl00$ContentPlaceHolder1$txt" + control;
            var hide = "ctl00$ContentPlaceHolder1$hb" + control;

            var oDescripcion = document.getElementById(descripcion);
            if (oDescripcion != null)
                oDescripcion.innerHTML = valordescripcion;

            var hidde = document.getElementById(hide);
            if (hidde != null)
                hidde.Value = idempresa;

            __doPostBack(control, idempresa);
           
        }

        function ValidarEliminar() {
            return window.confirm("¿ Desea eliminar el establecimiento ?")
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
        .style4
        {
            width: 153px;
            height: 24px;
        }
        .style5
        {
            width: 375px;
            height: 24px;
        }
        .style6
        {
            height: 24px;
        }
        .style10
        {
            width: 153px;
            height: 27px;
        }
        .style11
        {
            width: 375px;
            height: 27px;
        }
        .style12
        {
            height: 27px;
        }
        .style13
        {
            width: 153px;
            height: 26px;
        }
        .style15
        {
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <p>
        <br />
        <div class="divTopPage3">
        </div>
        <br />

        <asp:Label ID="Label13" runat="server" Text="Establecimiento" CssClass="TituloReporte"></asp:Label>
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
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label6" runat="server" Text="Dirección"></asp:Label>
                </td>
                <td class="style3">
                    
                    <asp:TextBox ID="txtDireccion" runat="server" Width="600px"></asp:TextBox>

                    </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label3" runat="server" Text="Provincia"></asp:Label>
                </td>
                <td class="style3">
                    <asp:DropDownList ID="cboProvincia" runat="server" Height="21px" 
                        style="margin-left: 0px" Width="350px" AutoPostBack="True" 
                        onselectedindexchanged="cboProvincia_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label4" runat="server" Text="Localidad"></asp:Label>
                </td>
                <td class="style3">
                 <asp:DropDownList ID="cboLocalidad" runat="server" Height="24px" 
                    style="margin-left: 0px" Width="350px">
                 </asp:DropDownList>

                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style4">
                    <asp:Label ID="Label7" runat="server" Text="Almacen SAP"></asp:Label>
                    </td>
                <td class="style5">
                    
                    <asp:TextBox ID="txtIDAlmacenSAP" runat="server" Width="165px"></asp:TextBox>

                </td>
                <td class="style6">
                    </td>
            </tr>
            <tr>
                <td class="style10">
                    <asp:Label ID="Label8" runat="server" Text="Centro SAP"></asp:Label>
                    </td>
                <td class="style11">
                    
                    <asp:TextBox ID="txtIDCentroSAP" runat="server" Width="165px"></asp:TextBox>

                </td>
                <td class="style12">
                    </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label9" runat="server" Text="Interlocutor Destinatario"></asp:Label>
                    </td>
                <td class="style3">
                     <asp:HiddenField ID="hbIdInterlocutorDestinatario" runat="server" />
                    <asp:TextBox ID="txtIdInterlocutorDestinatario" runat="server" Width="348px"></asp:TextBox>
                    <asp:ImageButton ID="ImageButtonClienteInterlocutorDestinatario" runat="server" 
                        ImageUrl="~/Content/Images/magnify.gif" 
                        onclick="ImageButtonClienteInterlocutorDestinatario_Click" />


                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="Label10" runat="server" Text="Tipo Origen Destino"></asp:Label>
                    </td>
                <td class="style3">
                    
                                    <asp:DropDownList ID="cboRecorridoEstablecimiento" 
                        runat="server" Height="24px" 
                    style="margin-left: 0px" Width="350px" AutoPostBack="True" 
                                        onselectedindexchanged="cboRecorridoEstablecimiento_SelectedIndexChanged">
                </asp:DropDownList>

                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="lblcebe" runat="server" Text="CEBE"></asp:Label>
                    </td>
                <td class="style3">
                    
                    <asp:TextBox ID="txtcebe" runat="server" Width="165px"></asp:TextBox>

                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style10">
                    <asp:Label ID="lblexpedicion" runat="server" Text="Expedicion"></asp:Label>
                    </td>
                <td class="style11">
                    
                    <asp:TextBox ID="txtexpedicion" runat="server" Width="165px"></asp:TextBox>

                </td>
                <td class="style12">
                    </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Label ID="lblEstablecimientoAFIP" runat="server" Text="Establecimiento AFIP
                    "></asp:Label>
                    </td>
                <td class="style3">
                    
                    <asp:TextBox ID="txtEstablecimientoAfip" runat="server" Width="165px"></asp:TextBox>

                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style13">
                    &nbsp;</td>
                <td>
                    <asp:CheckBox ID="chkAsociaCartaDePorte" runat="server" text="Debe utilizar unicamente Lotes de Carta de porte asignados a este establecimiento"/>
                </td>
                <td class="style15">
                    </td>
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
                    &nbsp;</td>
                <td class="style3">
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
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
                        onclick="btnEliminar_Click"  OnClientClick="return ValidarEliminar()"/>
                </td>
            </tr>
        </table>
        <br />
    
    <div class="Buscadores" id="BuscadorCliente" runat="server">
                <table style="width: 599px; height: 50px">
                    <tr>
                        <td class="style63" colspan="3">
                            <br />
                            <asp:Label ID="Label1" runat="server" Text="BUSCADOR DE CLIENTES" CssClass="SubTitulos"></asp:Label><br />
                        </td>
                    </tr>
                    <tr>
                        <td class="style65">
                            <asp:Label ID="Label12" runat="server" Text="Descripcion"></asp:Label>
                        </td>
                        <td class="style64">
                            <asp:TextBox ID="txtBuscadorCliente" runat="server" Width="335px"></asp:TextBox>
                        </td>
                        <td class="style60">
                            <asp:Button ID="btnBuscadorCliente" runat="server" Text="Buscar" OnClick="btnBuscadorCliente_Click" />
                        </td>
                        <td class="style60">
                            <asp:Button ID="btnCerrarCliente" runat="server" Text="Cerrar" 
                                onclick="btnCerrarCliente_Click"/>
                        </td>
                        
                    </tr>
                    <tr>
                        <td colspan="4">
                        <div class="grillaBuscador">
                            <asp:Table ID="tblBuscadorCliente" runat="server">
                            </asp:Table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="style65">
                            &nbsp;
                        </td>
                        <td class="style64">
                            &nbsp;
                        </td>
                        <td class="style60">
                            &nbsp;
                        </td>
                        <td class="style60">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
    
    </p>
</asp:Content>

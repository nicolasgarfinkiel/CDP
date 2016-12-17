<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="CargaC1116A.aspx.cs" Inherits="CartaDePorte.Web.CargaC1116A" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" type="text/css" href="http://www.jeasyui.com/easyui/themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="http://www.jeasyui.com/easyui/themes/icon.css">
    <link rel="stylesheet" type="text/css" href="http://www.jeasyui.com/easyui/demo/demo.css">
    <%--<script type="text/javascript" src="http://code.jquery.com/jquery-1.6.1.min.js"></script>--%>
    <script type="text/javascript" src="http://www.jeasyui.com/easyui/jquery.easyui.min.js"></script>
    <script type="text/jscript" language="javascript">

        function getUrlVars() {
            var vars = [], hash;
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < hashes.length; i++) {
                hash = hashes[i].split('=');
                vars.push(hash[0]);
                vars[hash[0]] = hash[1];
            }
            return vars;
        }

        $.extend($.fn.datebox.defaults.rules, {
            validDate: {
                validator: function (value) {
                    if (value.length < 10)
                        return false;

                    var ss = value.split('/');
                    var d = parseInt(ss[0], 10);
                    var m = parseInt(ss[1], 10);
                    var y = parseInt(ss[2], 10);
                    //$.messager.alert('f', y + "" + m + "" + d);                    
                    return true;

                },
                message: 'Ingrese un formato de fecha válido'

            }
        });



        $(function () {
            $('#tt').datagrid({
                title: 'Detalle',
                iconCls: '',
                width: 660,
                height: 250,
                singleSelect: true,
                idField: 'Idc1116aDetalle',
                url: 'getC1116ADetalleByID.ashx?q=' + getUrlVars()["id"],
                columns: [[
					{ field: 'NumeroCartaDePorte', title: 'Numero Carta De Porte', width: 170, align: 'right', editor: { type: 'numberbox', options: { precision: 0, required: true } } },
					{ field: 'NumeroCertificadoAsociado', title: 'Numero Certificado Asociado', width: 170, align: 'right', editor: { type: 'numberbox', options: { required: true } } },
					{ field: 'KgBrutos', title: 'Kg. Brutos', width: 80, align: 'right', editor: { type: 'numberbox', options: { precision: 2, required: true } } },
					{ field: 'FechaRemesa', title: 'Fecha Remesa', width: 100, editor: { type: 'textbox', options: { required: true, validType: 'validDate[\'dd/MM/yyyy\']' } } },
					{
					    field: 'action', title: 'Action', width: 120, align: 'center',
					    formatter: function (value, row, index) {
					        if (row.editing) {
					            var s = '<a href="#" onclick="saverow(this)">Guardar</a> ';
					            var c = '<a href="#" onclick="cancelrow(this)">Cancelar</a>';
					            return s + c;
					        } else {
					            var e = '<a href="#" onclick="editrow(this)">Editar</a> ';
					            var d = '<a href="#" onclick="deleterow(this)">Borrar</a>';
					            return e + d;
					        }
					    }
					}
                ]],
                onBeforeEdit: function (index, row) {
                    row.editing = true;
                    updateActions(index);
                },
                onAfterEdit: function (index, row) {
                    row.editing = false;
                    updateActions(index);
                },
                onCancelEdit: function (index, row) {
                    row.editing = false;
                    updateActions(index);
                }
            });
        });
        function updateActions(index) {
            $('#tt').datagrid('updateRow', {
                index: index,
                row: {}
            });
        }

        function getRowIndex(target) {
            var tr = $(target).closest('tr.datagrid-row');
            return parseInt(tr.attr('datagrid-row-index'));
        }
        function editrow(target) {
            $('#tt').datagrid('beginEdit', getRowIndex(target));
        }
        function deleterow(target) {
            $.messager.confirm('Confirm', 'Are you sure?', function (r) {
                if (r) {
                    $('#tt').datagrid('deleteRow', getRowIndex(target));
                }
            });
        }
        function saverow(target) {
            $('#tt').datagrid('endEdit', getRowIndex(target));
        }
        function cancelrow(target) {
            $('#tt').datagrid('cancelEdit', getRowIndex(target));
        }
        function insert() {
            var row = $('#tt').datagrid('getSelected');
            if (row) {
                var index = $('#tt').datagrid('getRowIndex', row);
            } else {
                index = 0;
            }
            $('#tt').datagrid('insertRow', {
                index: index,
                row: {
                    status: 'P'
                }
            });
            $('#tt').datagrid('selectRow', index);
            $('#tt').datagrid('beginEdit', index);
        }

        function gete() {
            var facturas = {
                lineas: []
            };

            linea_facturas = "";
            var contieneErrores = false;
            //Armado del arreglo JSON a enviar
            rows = $('#tt').datagrid('getRows');  // get all rows of Datagrid
            for (var i = 0; i < rows.length; i++) {
                var renglon = rows[i];
                facturas.lineas.push({
                    "Idc1116aDetalle": renglon.Idc1116aDetalle,
                    "Idc1116a": renglon.Idc1116a,
                    "NumeroCartaDePorte": renglon.NumeroCartaDePorte,
                    "NumeroCertificadoAsociado": renglon.NumeroCertificadoAsociado,
                    "KgBrutos": renglon.KgBrutos,
                    "FechaRemesa": renglon.FechaRemesa
                });
                linea_facturas =
                     linea_facturas +
                     renglon.Idc1116aDetalle + "," +
                     renglon.Idc1116a + "," +
                     renglon.NumeroCartaDePorte + "," +
                     renglon.NumeroCertificadoAsociado + "," +
                     renglon.KgBrutos + "," +
                     renglon.FechaRemesa + "&";


                if (renglon.NumeroCartaDePorte.length == 0 ||
                renglon.NumeroCertificadoAsociado.length == 0 ||
                renglon.KgBrutos.length == 0 ||
                renglon.FechaRemesa.length == 0) {
                    contieneErrores = true;
                }
            }

            var jsonText = JSON.stringify(facturas); //Convierte un valor de JavaScript en una cadena de la notación de objetos JavaScript (JSON).
            //$.messager.alert('Info', jsonText);
            //window.console.log(linea_facturas);

            $("[id$=hdDetalle]").val(jsonText);

            var mensaje = "";
            //document.getElementById("lblMensaje").style.color = "Red";
            if (jsonText.length == 13) {
                mensaje += "<font color='Red'>Debe completar el detalle de cartas de porte asociadas al formulario.<br/></font>";

            }

            if (contieneErrores) {
                mensaje += "<font color='Red'>Debe completar Todos los datos del detalle de cartas de porte asociadas al formulario.<br/></font>";

            }



            //VALIDACIONES GENERALES FORMULARIO
            if ($("[id$=txtNumeroCAC]").val() == "") {
                mensaje += "<font color='Red'>Debe Completar el Numero CAC.<br/></font>";
            }
            if ($("[id$=txtNumeroCertificadoC1116A]").val() == "") {
                mensaje += "<font color='Red'>Debe Completar el Numero Certificado C1116A.<br/></font>";
            }

            if ($("[id$=txtCodigoEstablecimiento]").val() == "") {
                mensaje += "<font color='Red'>Debe Completar Codigo Establecimiento.<br/></font>";
            }
            if ($("[id$=txtCuitProductor]").val() == "") {
                mensaje += "<font color='Red'>Debe Completar Cuit Productor.<br/></font>";
            }
            if (!$("[id$=txtCuitProductor]").cuitvalido()) {
                mensaje += "<font color='Red'>Debe Completar un Cuit Productor valido.<br/></font>";
            }

            if ($("[id$=txtRazonSocialProductor]").val() == "") {
                mensaje += "<font color='Red'>Debe Completar Razon Social Productor.<br/></font>";
            }
            if ($("[id$=cboTipoDomicilio]").val() == "-1") {
                mensaje += "<font color='Red'>Debe Completar Tipo Domicilio.<br/></font>";
            }
            if ($("[id$=txtCalleRutaProductor]").val() == "") {
                mensaje += "<font color='Red'>Debe Completar Calle/Ruta Productor.<br/></font>";
            }

            if ($("[id$=txtNroKmProductor]").val() == "") {
                mensaje += "<font color='Red'>Debe Completar Nro/Km Productor.<br/></font>";
            }
            if ($("[id$=txtPisoProductor]").val() == "") {
                mensaje += "<font color='Red'>Debe Completar Piso Productor.<br/></font>";
            }
            if ($("[id$=txtOficinaDtoProductor]").val() == "") {
                mensaje += "<font color='Red'>Debe Completar Oficina/Dto Productor.<br/></font>";
            }
            //if ($("[id$=txtCodigoLocalidadProductor]").val() == "") {
            //    mensaje += "<font color='Red'>Debe Completar Codigo Localidad Productor.<br/></font>";
            //}
            if ($("[id$=cboCodigoLocalidadProductor]").val() == "-1") {
                mensaje += "<font color='Red'>Debe Completar Codigo Localidad Productor.<br/></font>";
            }
            if ($("[id$=txtCodigoPostalProductor]").val() == "") {
                mensaje += "<font color='Red'>Debe Completar Codigo Postal Productor.<br/></font>";
            }

            if ($("[id$=cboEspecie]").val() == "-1") {
                mensaje += "<font color='Red'>Debe Completar Especie.<br/></font>";
            }
            if ($("[id$=txxCosecha]").val() == "") {
                mensaje += "<font color='Red'>Debe Completar Cosecha.<br/></font>";
            }

            if ($("[id$=txtAlmacenajeDíasLibres]").val() == "") {
                mensaje += "<font color='Red'>Debe Completar Almacenaje Días Libres.<br/></font>";
            }
            if ($("[id$=txtTarifaAlmacenaje]").val() == "") {
                mensaje += "<font color='Red'>Debe Completar Tarifa Almacenaje.<br/></font>";
            }
            if ($("[id$=txtGastosGenerales]").val() == "") {
                mensaje += "<font color='Red'>Debe Completar Gastos Generales.<br/></font>";
            }
            if ($("[id$=txtZarandeo]").val() == "") {
                mensaje += "<font color='Red'>Debe Completar Zarandeo.<br/></font>";
            }
            if ($("[id$=txtSecadoDe]").val() == "") {
                mensaje += "<font color='Red'>Debe Completar Secado De.<br/></font>";
            }
            if ($("[id$=txttxtSecadoA]").val() == "") {
                mensaje += "<font color='Red'>Debe Completar Secado A.<br/></font>";
            }
            if ($("[id$=txtTarifaSecado]").val() == "") {
                mensaje += "<font color='Red'>Debe Completar Tarifa Secado.<br/></font>";
            }
            if ($("[id$=txtPuntoExceso]").val() == "") {
                mensaje += "<font color='Red'>Debe Completar Punto Exceso.<br/></font>";
            }
            if ($("[id$=txtTarifaOtros]").val() == "") {
                mensaje += "<font color='Red'>Debe Completar Tarifa Otros.<br/></font>";
            }
            //if ($("[id$=txtCodigoPartidoOrigen]").val() == "") {
            //    mensaje += "<font color='Red'>Debe Completar Codigo Partido Origen.<br/></font>";
            //}
            //if ($("[id$=txtCodigoPartidoEntrega]").val() == "") {
            //    mensaje += "<font color='Red'>Debe Completar Codigo Partido Entrega.<br/></font>";
            //}
            if ($("[id$=cboCodigoPartidoOrigen]").val() == "-1") {
                mensaje += "<font color='Red'>Debe Completar Codigo Partido Origen.<br/></font>";
            }
            if ($("[id$=cboCodigoPartidoEntrega]").val() == "-1") {
                mensaje += "<font color='Red'>Debe Completar Codigo Partido Entrega.<br/></font>";
            }
            if ($("[id$=txtNroAnalisis]").val() == "") {
                mensaje += "<font color='Red'>Debe Completar Nro Analisis.<br/></font>";
            }
            if ($("[id$=txtNroBoletin]").val() == "") {
                mensaje += "<font color='Red'>Debe Completar Nro Boletin.<br/></font>";
            }
            if ($("[id$=txtFechaAnalisis]").val() == "") {
                mensaje += "<font color='Red'>Debe Completar Fecha Analisis.<br/></font>";
            }
            if ($("[id$=txtGrado]").val() == "") {
                mensaje += "<font color='Red'>Debe Completar Grado.<br/></font>";
            }
            if ($("[id$=txtFactor]").val() == "") {
                mensaje += "<font color='Red'>Debe Completar Factor.<br/></font>";
            }
            if ($("[id$=txtContenidoProteico]").val() == "") {
                mensaje += "<font color='Red'>Debe Completar Contenido Proteico.<br/></font>";
            }
            if ($("[id$=txtCuitLaboratorio]").val() == "") {
                mensaje += "<font color='Red'>Debe Completar Cuit Laboratorio.<br/></font>";
            }
            if (!$("[id$=txtCuitLaboratorio]").cuitvalido()) {
                mensaje += "<font color='Red'>Debe Completar un Cuit Laboratorio valido.<br/></font>";
            }

            if ($("[id$=txtNombreLaboratorio]").val() == "") {
                mensaje += "<font color='Red'>Debe Completar Nombre Laboratorio.<br/></font>";
            }
            if ($("[id$=txtPesoBruto]").val() == "") {
                mensaje += "<font color='Red'>Debe Completar Peso Bruto.<br/></font>";
            }
            if ($("[id$=txtPesoNeto]").val() == "") {
                mensaje += "<font color='Red'>Debe Completar Peso Neto.<br/></font>";
            }
            if ($("[id$=txtMermaVolatil]").val() == "") {
                mensaje += "<font color='Red'>Debe Completar Merma Volatil.<br/></font>";
            }
            if ($("[id$=txtMermaZarandeo]").val() == "") {
                mensaje += "<font color='Red'>Debe Completar Merma Zarandeo.<br/></font>";
            }
            if ($("[id$=txtMermaSecado]").val() == "") {
                mensaje += "<font color='Red'>Debe Completar Merma Secado.<br/></font>";
            }
            if ($("[id$=txtFechaCierre]").val() == "") {
                mensaje += "<font color='Red'>Debe Completar Fecha Cierre.<br/></font>";
            }
            if ($("[id$=txtImporteIVAServicios]").val() == "") {
                mensaje += "<font color='Red'>Debe Completar Importe IVA Servicios.<br/></font>";
            }
            if ($("[id$=txtTotalServicios]").val() == "") {
                mensaje += "<font color='Red'>Debe Completar Total Servicios.<br/></font>";
            }


            if (mensaje.length > 0) {
                $("[id$=lblMensaje]").html(mensaje);
                return false;
            }



        }

    </script>

    <style type="text/css">
        .contenedordiv {
            z-index: 100;
            position: relative;
            top: 0px;
            left: 0px;
        }

        .SubTitulos {
            font-family: Arial, Helvetica, sans-serif;
            font-size: small;
            font-weight: bold;
        }

        .SubTitulosPrincipal {
            font-family: Arial, Helvetica, sans-serif;
            font-size: larger;
            font-weight: bold;
        }

        .auto-style1 {
            height: 23px;
        }

        .auto-style2 {
            height: 18px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <br />
    <br />
    <div class="divTopPage2">
    </div>
    <br />
    <div class="contenedordiv">
        <table style="width: 956px" border="0">
            <tr>
                <td>&nbsp;
                </td>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td style="font-family: Arial, Helvetica, sans-serif; font-size: large; width: 100%;" class="style76">
                    <asp:Label ID="lblTituloSeccion" runat="server" Text="Carga Formulario C1116A" CssClass="SubTitulosPrincipal"></asp:Label>
                </td>
                <td style="font-family: Arial, Helvetica, sans-serif; font-size: large;" class="style75">
                    <table border="0">
                        <tr>
                            <td>
                                <asp:Button ID="btExportar" runat="server" Text="Exportar Cabecera" OnClick="btExportar_Click" Width="150px" Visible="false" />
                            </td>
                            <td>
                                <asp:Button ID="btExportarDetalle" runat="server" Text="Exportar Detalle" OnClick="btExportarDetalle_Click" Width="150px" Visible="false" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <div class="contenedordiv">
        <table style="width: 100%;">
            <tr>
                <td>
                    <asp:Label ID="lblNumeroCAC" runat="server" Text="Número de CAC"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>
                    <asp:Label ID="lblNumeroCertificadoC1116A" runat="server" Text="Número Certificado C1116A"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox Style="width: 200px;" ID="txtNumeroCAC" runat="server" placeholder="" ToolTip="Número de CAC"></asp:TextBox>
                </td>
                <td>
                    <div style="width: 205px;"></div>
                    <%--<asp:TextBox style="width: 100px;" ID="txtFechaVencimientoC1116A" runat="server" placeholder=""></asp:TextBox>--%>
                </td>
                <td>
                    <asp:TextBox Style="width: 200px;" ID="txtNumeroCertificadoC1116A" runat="server" placeholder="" ToolTip="Número Certificado C1116A"></asp:TextBox>
                </td>
            </tr>
        </table>

    </div>
    <div class="contenedordiv">
        <table style="width: 100%;">
            <tr>
                <td>
                    <asp:Label ID="label1" runat="server" Text="Codigo Establecimiento"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="label2" runat="server" Text="CUIT Productor"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="label3" runat="server" Text="Razon Social Productor"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox Style="width: 200px;" ID="txtCodigoEstablecimiento" runat="server" placeholder="" ToolTip="Codigo Establecimiento"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox Style="width: 200px;" ID="txtCuitProductor" runat="server" placeholder="" ToolTip="CUIT Productor"></asp:TextBox>
                    <asp:Label ID="txtCuitProductor1" runat="server" Text="*" Style="display: none"></asp:Label>
                </td>
                <td>
                    <asp:TextBox Style="width: 200px;" ID="txtRazonSocialProductor" runat="server" placeholder="" ToolTip="Razon Social Productor"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="label4" runat="server" Text="Tipo Domicilio"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="label5" runat="server" Text="Calle o Ruta del Productor"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="label6" runat="server" Text="Nro"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList Style="width: 200px;" ID="cboTipoDomicilio" runat="server" ToolTip="Tipo Domicilio"></asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox Style="width: 200px;" ID="txtCalleRutaProductor" runat="server" placeholder="" ToolTip="Calle o Ruta del Productor"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox Style="width: 50px;" ID="txtNroKmProductor" runat="server" placeholder="" ToolTip="Nro"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="label7" runat="server" Text="Piso"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="label8" runat="server" Text="Oficina"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="label9" runat="server" Text="Código Localidad del Productor"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox Style="width: 200px;" ID="txtPisoProductor" runat="server" placeholder="" ToolTip="Piso"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox Style="width: 200px;" ID="txtOficinaDtoProductor" runat="server" placeholder="" ToolTip="Oficina"></asp:TextBox>
                </td>
                <td>
                    <%--<asp:TextBox Style="width: 200px;" ID="txtCodigoLocalidadProductor" runat="server" placeholder="" ToolTip="Código Localidad del Productor"></asp:TextBox>--%>
                    <asp:DropDownList Style="width: 200px;" ID="cboCodigoLocalidadProductor" runat="server" placeholder="" ToolTip="Código Localidad del Productor"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="label10" runat="server" Text="Código Postal del Productor"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td>
                    <asp:TextBox Style="width: 200px;" ID="txtCodigoPostalProductor" runat="server" placeholder="" ToolTip="Código Postal del Productor"></asp:TextBox>
                </td>
                <td>
                    <div style="width: 200px;">
                    </div>

                </td>
                <td>
                    <div style="width: 200px;">
                    </div>

                </td>
            </tr>

        </table>
    </div>
    <div class="contenedordiv">
        <table style="width: 100%;">
            <tr>
                <td>
                    <asp:Label ID="label11" runat="server" Text="Especie"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="label13" runat="server" Text="Cosecha"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList Style="width: 200px;" ID="cboEspecie" runat="server" ToolTip="Especie"></asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox Style="width: 200px;" ID="txxCosecha" runat="server" placeholder="" ToolTip="Cosecha"></asp:TextBox>
                </td>
                <td>
                    <div style="width: 200px;"></div>
                </td>
            </tr>
            <tr>
                <td style="margin-left: 40px">
                    <asp:Label ID="label12" runat="server" Text="Almacenaje Días Libres"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="label14" runat="server" Text="Tarifa Almacenaje cada 100 Kg"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="label15" runat="server" Text="Gastos Generales"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:TextBox Style="width: 200px;" ID="txtAlmacenajeDíasLibres" runat="server" placeholder="" ToolTip="Almacenaje Días Libres"></asp:TextBox>
                </td>
                <td class="auto-style1">
                    <asp:TextBox Style="width: 200px;" ID="txtTarifaAlmacenaje" runat="server" placeholder="" ToolTip="Tarifa Almacenaje cada 100 Kg"></asp:TextBox>
                </td>
                <td class="auto-style1">
                    <asp:TextBox Style="width: 200px;" ID="txtGastosGenerales" runat="server" placeholder="" ToolTip="Gastos Generales"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="label16" runat="server" Text="Zarandeo"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="label17" runat="server" Text="Secado De %"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="label18" runat="server" Text="Secado A %"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox Style="width: 200px;" ID="txtZarandeo" runat="server" placeholder="" ToolTip="Zarandeo"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox Style="width: 200px;" ID="txtSecadoDe" runat="server" placeholder="" ToolTip="Secado De %"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox Style="width: 200px;" ID="txtSecadoA" runat="server" placeholder="" ToolTip="Secado A %"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="label19" runat="server" Text="Tarifa Secado"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="label20" runat="server" Text="Punto Exceso"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="label21" runat="server" Text="Tarifa Otros"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox Style="width: 200px;" ID="txtTarifaSecado" runat="server" placeholder="" ToolTip="Tarifa Secado"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox Style="width: 200px;" ID="txtPuntoExceso" runat="server" placeholder="" ToolTip="Punto Exceso"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox Style="width: 200px;" ID="txtTarifaOtros" runat="server" placeholder="" ToolTip="Tarifa Otros"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="contenedordiv">
        <table style="width: 100%;">
            <tr>
                <td>
                    <asp:Label ID="label22" runat="server" Text="Codigo de Partido de Origen"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="label23" runat="server" Text="Código de Partido de Entrega"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="label24" runat="server" Text="Nro. de Análisis"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <%--<asp:TextBox Style="width: 200px;" ID="txtCodigoPartidoOrigen" runat="server" placeholder="" ToolTip="Codigo de Partido de Origen"></asp:TextBox>--%>
                    <asp:DropDownList Style="width: 200px;" ID="cboCodigoPartidoOrigen" runat="server" ToolTip="Codigo de Partido de Origen"></asp:DropDownList>
                </td>
                <td>
                    <%--<asp:TextBox Style="width: 200px;" ID="txtCodigoPartidoEntrega" runat="server" placeholder="" ToolTip="Código de Partido de Entrega"></asp:TextBox>--%>
                    <asp:DropDownList Style="width: 200px;" ID="cboCodigoPartidoEntrega" runat="server" ToolTip="Código de Partido de Entrega"></asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox Style="width: 200px;" ID="txtNroAnalisis" runat="server" placeholder="" ToolTip="Nro. de Análisis"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="label25" runat="server" Text="Nro. Boletín"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="label26" runat="server" Text="Fecha Análisis"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="label27" runat="server" Text="Grado"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox Style="width: 200px;" ID="txtNroBoletin" runat="server" placeholder="" ToolTip="Nro. Boletín"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox Style="width: 100px;" ID="txtFechaAnalisis" runat="server" placeholder="" ToolTip="Fecha Análisis"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox Style="width: 200px;" ID="txtGrado" runat="server" placeholder="" ToolTip="Grado"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="label28" runat="server" Text="Factor"></asp:Label>
                </td>
                <td class="auto-style2">
                    <asp:Label ID="label29" runat="server" Text="Contenido Proteico %"></asp:Label>
                </td>
                <td class="auto-style2">
                    <asp:Label ID="label30" runat="server" Text="Cuit del Laboratorio"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox Style="width: 200px;" ID="txtFactor" runat="server" placeholder="" ToolTip="Factor"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox Style="width: 200px;" ID="txtContenidoProteico" runat="server" placeholder="" ToolTip="Contenido Proteico %"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox Style="width: 200px;" ID="txtCuitLaboratorio" runat="server" placeholder="" ToolTip="Cuit del Laboratorio"></asp:TextBox>
                    <asp:Label ID="txtCuitLaboratorio1" runat="server" Text="*" Style="display: none"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="label31" runat="server" Text="Nombre del Laboratorio"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="label32" runat="server" Text="Peso Bruto Kg."></asp:Label>
                </td>
                <td>
                    <asp:Label ID="label33" runat="server" Text="Peso Neto Kg."></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox Style="width: 200px;" ID="txtNombreLaboratorio" runat="server" placeholder="" ToolTip="Nombre del Laboratorio"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox Style="width: 200px;" ID="txtPesoBruto" runat="server" placeholder="" ToolTip="Peso Bruto Kg."></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox Style="width: 200px;" ID="txtPesoNeto" runat="server" placeholder="" ToolTip="Peso Neto Kg."></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="label34" runat="server" Text="Merma Kg. Volátil"></asp:Label>
                </td>
                <td class="auto-style2">
                    <asp:Label ID="label35" runat="server" Text="Merma Kg. Zarandeo"></asp:Label>
                </td>
                <td class="auto-style2">
                    <asp:Label ID="label36" runat="server" Text="Merma Kg. Secado"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox Style="width: 200px;" ID="txtMermaVolatil" runat="server" placeholder="" ToolTip="Merma Kg. Volátil"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox Style="width: 200px;" ID="txtMermaZarandeo" runat="server" placeholder="" ToolTip="Merma Kg. Zarandeo"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox Style="width: 200px;" ID="txtMermaSecado" runat="server" placeholder="" ToolTip="Merma Kg. Secado"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="label37" runat="server" Text="Fecha Cierre"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="label38" runat="server" Text="Importe IVA servicios"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="label39" runat="server" Text="Total Servicios"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox Style="width: 100px;" ID="txtFechaCierre" runat="server" placeholder="" ToolTip="Fecha Cierre"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox Style="width: 200px;" ID="txtImporteIVAServicios" runat="server" placeholder="" ToolTip="Importe IVA servicios"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox Style="width: 200px;" ID="txtTotalServicios" runat="server" placeholder="" ToolTip="Total Servicios"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <div style="margin: 10px 0">
        <a href="#" class="easyui-linkbutton" onclick="insert()">Agregar Carta de Porte</a>
    </div>
    <table id="tt"></table>
    <br />
    <br />
    <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
    <br />
    <br />
    <asp:HiddenField ID="hdDetalle" runat="server" />
    <div class="contenedordiv" style="text-align: right">
        <asp:Button ID="btnGuardar" OnClientClick="javascript: return gete();" runat="server" Text="Guardar"
            OnClick="btnGuardar_Click" Style="width: 68px" />
    </div>
    <br />
    <br />

</asp:Content>

﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="ReporteActividad.aspx.cs" Inherits="CartaDePorte.Web.ReporteActividad" %>

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
            width: 181px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

       
    <script type="text/javascript">

        google.load("visualization", "1", {packages:["corechart"]});
        google.setOnLoadCallback(drawVisualization);
        function drawVisualization() {
             var data = google.visualization.arrayToDataTable([
                 ['Fecha', 'Aprobadas AFIP', 'Aprobadas SAP'],
                  <%=Resultados()%>

            ]);
    
            var options =  {
                        title:"",
                        width:950, height:380,
                        'colors' : ['#59B300', '#336600'],
                        legend: { position: "bottom" },
                        backgroundColor: '#F5F5F5',
                        vAxis: {title: ""},
                        hAxis: {title: ""}
                    };
        
            new google.visualization.ColumnChart(document.getElementById('visualization')).
            draw(data,options);
        }

        
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
    <asp:Label ID="Label3" runat="server" Text="Actividad en Carta de porte"
        CssClass="TituloReporte"></asp:Label>
    <br />
    <br />
    <table class="style1">
        <tr>
            <td class="style2">
                <asp:Label ID="Label2" runat="server" Text="Fecha Desde"></asp:Label>
            </td>
            <td class="style3">
                <asp:TextBox ID="txtDateDesde" runat="server" ReadOnly="true" Width="90px" EnableViewState="False"></asp:TextBox>
            </td>
            <td class="style4">
                <asp:Label ID="Label1" runat="server" Text="Fecha Hasta"></asp:Label>
            </td>
            <td class="style5">
                <asp:TextBox ID="txtDateHasta" runat="server" ReadOnly="true" Width="90px"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="Button1" runat="server" Text="Aceptar" OnClick="Button1_Click" />
            </td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;
            </td>
            <td class="style3" colspan="4">
                <asp:Label ID="lblMensaje" runat="server" ForeColor="#CC3300"></asp:Label>
            </td>
        </tr>
    </table>
    <div id="visualization">
    <br />
    </div>
</asp:Content>
<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Tabla.aspx.cs" Inherits="CartaDePorte.Web.Tabla" %>

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

        var visualization;
     
        
        google.load('visualization', '1', {packages: ['table']});
        function drawVisualization() {
            // Create and populate the data table.
            var data = google.visualization.arrayToDataTable([
            <%=Resultados()%>
            ]);

            // Create and draw the visualization.
            visualization = new google.visualization.Table(document.getElementById('visualization'));            
            visualization.draw(data, options);
            
        }


        google.setOnLoadCallback(drawVisualization);
        
        
           
        var options = {
              annotations: {
                boxStyle: {
                  stroke: '#888',           // Color of the box outline.
                  strokeWidth: 1,           // Thickness of the box outline.
                  rx: 10,                   // x-radius of the corner curvature.
                  ry: 10,                   // y-radius of the corner curvature.
                  gradient: {               // Attributes for linear gradient fill.
                    color1: '#fbf6a7',      // Start color for gradient.
                    color2: '#33b679',      // Finish color for gradient.
                    x1: '0%', y1: '0%',     // Where on the boundary to start and end the
                    x2: '100%', y2: '100%', // color1/color2 gradient, relative to the
                                            // upper left corner of the boundary.
                    useObjectBoundingBoxUnits: true // If true, the boundary for x1, y1,
                                                    // x2, and y2 is the box. If false,
                                                    // it's the entire chart.
                  }
                }
              }
            };
            
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

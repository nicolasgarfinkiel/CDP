<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="LogSapList.aspx.cs" Inherits="CartaDePorte.Web.LogSapList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .tabla1
        {
        	width: 100%;
            border-color: 1;
            border-style: solid;
            border-width: thin;
        }
        .style2
        {
        }
        .style3
        {
            width: 564px;
        }
        .style5
        {
            width: 100%;
        }
        .style6
        {
            width: 210px;
        }
        .style8
        {
            width: 347px;
        }
        .style9
        {
            width: 77px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <br/>
        <br/>
         <asp:Label ID="lblTituloSeccion6" runat="server" Text="Log Respuesta de SAP"
                    CssClass="TituloReporte"></asp:Label>
        
        <br/>
        <br/>
        <div style="overflow: auto; height: 450px;">
                <asp:Table ID="tblData" runat="server"></asp:Table>
        </div>
        <br/>



</asp:Content>

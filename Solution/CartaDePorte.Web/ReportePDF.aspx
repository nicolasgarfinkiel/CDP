<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ReportePDF.aspx.cs" Inherits="CartaDePorte.Web.ReportePDF" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <asp:Label ID="Label1" runat="server" Text="123564789789"
        Font-Names="Interleaved 2of5" Font-Size="30px"></asp:Label>
    <br />
    <br />
    <br />
    <br />
    <div id="divReport" runat="server" style="overflow: auto; height: 550px">
        <rsweb:ReportViewer ID="rptCartaDePorte" runat="server" Width="900px"
            Height="450px" ProcessingMode="Remote" ShowDocumentMapButton="False"
            ShowFindControls="False" ShowPromptAreaButton="False" ShowRefreshButton="False"
            ShowZoomControl="False" Visible="False">
            <ServerReport ReportPath="/Reportes/CartaDePorte/CartaDePorteReport"
                ReportServerUrl="http://srv-rst02-adm/ReportServer" />
        </rsweb:ReportViewer>


        <rsweb:ReportViewer ID="rptRemitoParaguay" runat="server"></rsweb:ReportViewer>

    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd.Master" AutoEventWireup="true" CodeBehind="SSO.aspx.cs" Inherits="WFTCloud.User.SSO" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Including main template styles -->
    <link href="../css/Gridstyle.css" rel="stylesheet" type="text/css" />

    <link rel="stylesheet" href="/css/main.css">
     
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="content">
        <asp:ToolkitScriptManager ID="CloudAnalyticsToolkitScriptManager" runat="server">
        </asp:ToolkitScriptManager>
        <div class="white-section">

            <div class="container">

                <div class="row">
                  
                    
                    

                  
                </div>
                <!-- row end -->
                  
            </div>
            <!-- conteiner end -->

        </div>
        <!-- white-section end -->
        <a id="SamplePDF" runat ="server" href="http://wftcloud.com/UploadedContents/whitepapers/SampleWFTQuote.pdf" target="_self"></a>
        
    </section>
    <div>
        <iframe id="iframeFile" runat="server" style="display: none;"></iframe>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerSEOContent" runat="server">
</asp:Content>

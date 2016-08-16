<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd.Master" AutoEventWireup="true" CodeBehind="download-SAP-cloud-computing-brochures.aspx.cs" Inherits="WFTCloud.User.download_SAP_cloud_computing_brochures" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <!-- Including main template styles -->
        <link rel="stylesheet" href="/css/main.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <section id="content">
            <div class="white-section">
                <div class="container">
                     <div class="breadcrumb">
                                    <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" />
                                    <asp:SiteMapPath ID="SiteMap1" runat="server"></asp:SiteMapPath>
                                </div>
                    <asp:Repeater ID="rptrBrochureCarousel" runat="server" 
                        onitemdatabound="rptrBrochureCarousel_ItemDataBound">
                    <ItemTemplate>
                    <div class="row center">
                        <asp:HiddenField ID="hdnPageNumber" runat="server" Value='<%# Eval("PageNo")%>' />   
                        <div class="row-fluid" >
                    	    <asp:Repeater ID="rptrBrochureInnerGroupItems" runat="server">
                            <ItemTemplate>
                            <div class="span2">
                                <a href="<%# Eval("Path")%>" target="_blank" style="font-weight:bold;"><div><img src='<%# Eval("ThumbnailPath")%>'></div><%# Eval("Title")%></a>
                            </div>
                            </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <!--/row-fluid--> 
                    </div>
                    <br><hr><br>
                    </ItemTemplate>
                    </asp:Repeater>
            </div><!-- conteiner end -->
        </div><!-- white-section end -->
    </section><!-- content end -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerSEOContent" runat="server">
      <hr />
      <div class="row">
        <div class="span12">
              <h2 style="font-size:12px;">Download SAP Cloud Computing Solutions Brochure:</h2>
              	<p >WFTCloud is a certified provider of SAP Cloud Computing Solutions, Cloud SAP ERP solution, Cloud SAP modules, & SAP on Cloud support services.You can implement SAP Cloud Computing Solutions & SAP on Cloud support services at a fraction of conventional SAP implementation costs. <strong>Download our SAP Cloud Computing Solutions brochure</strong> for more information.<p>
                      <h2 style="font-size: 12px;">SAP Cloud Computing information & info downloads:</h2>
                <p >We focus on reducing your SAP implementation cost by introducing a pay per use model for Cloud SAP modules, Cloud SAP ERP systems & SAP on Cloud support services. To know more about our pricing packages & other SAP Cloud Computing information, please <strong>download our Cloud SAP brochures.</strong><p>
                    <h2 style="font-size: 12px;">Free Download SAP on Cloud Brochure:</h2>
                <p >As a Certified SAP Cloud Computing Solutions provider, WFT has pioneered to provide SAP systems on the Cloud. You can implement SAP on Cloud Solutions & SAP on Cloud support services at a fraction of conventional SAP implementation costs. <strong>Download our SAP on Cloud brochure for more information.</strong><p>

        <%--<p style="font-size:12px;"><strong>Download SAP Cloud Computing Solutions Brochure:</strong><br>
WFTCloud is a certified provider of SAP Cloud Computing Solutions, Cloud SAP ERP solution, Cloud SAP modules, & SAP on Cloud support services.You can implement SAP Cloud Computing Solutions & SAP on Cloud support services at a fraction of conventional SAP implementation costs. Download our SAP Cloud Computing Solutions brochure for more information.<p>
                <p style="font-size:12px;"><strong>SAP Cloud Computing information & info downloads:</strong><br>
We focus on reducing your SAP implementation cost by introducing a pay per use model for Cloud SAP modules, Cloud SAP ERP systems & SAP on Cloud support services. To know more about our pricing packages & other SAP Cloud Computing information, please download our Cloud SAP brochures.<p>
                <p style="font-size:12px;"><strong>Free Download SAP on Cloud Brochure:</strong><br>
As a Certified SAP Cloud Computing Solutions provider, WFT has pioneered to provide SAP systems on the Cloud. You can implement SAP on Cloud Solutions & SAP on Cloud support services at a fraction of conventional SAP implementation costs. Download our SAP on Cloud brochure for more information.<p>
        --%>
    </div>
      </div>
</asp:Content>
<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd.Master" AutoEventWireup="true" CodeBehind="System-Requirements.aspx.cs" Inherits="WFTCloud.User.System_Requirements" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Including main template styles -->
        <link rel="stylesheet" href="/css/main.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="content">
    <div class="white-section">
    <div class="container row">
          
     <div class="row">
          
          <div class="span12">
                 <div class="breadcrumb">
                                    <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" />
                                    <asp:SiteMapPath ID="SiteMap1" runat="server"></asp:SiteMapPath>
                                </div>
              <asp:Literal ID="Literal1" runat="server"></asp:Literal>
            <%--<h4>System Requirements</h4>
            <p>Shared Access - You need a PC with IE 7 or above or Firefox to access the SAP systems. Alternatively you can connect through a SAPGUI. A high speed internet connection is required.</p>
            <p>Dedicated Access- You need a PC with IE 7 or above or Firefox to access the SAP systems. Alternatively you can connect through a SAPGUI. A high speed internet connection is required. Also you could explore the possibility of extending your machines into this network for ease in data migrations. </p>--%>
          </div>
          
          <!-- span12 end -->
          
          </div>
          
          </div>
         </div>
                </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerSEOContent" runat="server">
      <hr />
      <div class="row">
        <div class="span12">
            <h2 style="font-size:12px;">WFTCloud offers affordable & low cost SAP Cloud services & SAP Cloud solutions!</h2>
            <p style="font-size:12px;">WFTCloud.com offers affordable & low cost SAP Cloud services & SAP Cloud solutions at an unmatched cost. Utilize WFT's expertise in low cost & affordable SAP Cloud services, SAP Cloud solutions & Cloud ERP software for your business. Call Now!!!</p>
            <h2 style="font-size:12px;">Pay per use model for cloud ERP software, SaaS ERP software, ondeamand ERP & ERM software solution.</h2>
            <p style="font-size:12px;">We focus on reducing your ERP implementation cost by introducing pay per use model for <strong>Cloud ERP software, SaaS ERP software, ondemand ERP & CRM solutions</strong>. To know more about our pricing packages for <strong>Cloud ERP software, SaaS ERP software, on demand ERP solutions & on demand CRM software solutions,</strong> Contact Us Now!</p>
            <h2 style="font-size:12px;">SAP certified provider of affordable & low cost web based ERP software solution & Cloud SAP ERP software solution.</h2>
            <p style="font-size:12px;">WFTCloud is a certified provider of cheap, affordable & low cost web based ERP software solution & Cloud SAP ERP software solution. Get implementation of cheap, low cost & affordable web based ERP software solution & Cloud SAP ERP software solution within a fraction of conventional cost.</p>

               <%--       	<p style="font-size:12px;"><strong>WFTCloud offers legal SAP online access for cheap SAP systems & solutions!</strong><br>
WFTCloud.com offers legal SAP online access for small and medium scale enterprises and relatively cheap SAP systems & solutions on cloud SAP software at an unmatched cost. Utilize WFT's expertise in legal SAP online access & relatively cheap SAP systems & solutions for your business. Call Now!!!<p>
                <p style="font-size:12px;"><strong>Pay per use model for affordable & low cost SAP system for small business.</strong><br>
We focus on reducing your ERP implementation cost by introducing pay per use model for cheap, affordable & low cost SAP system for small business. To know more about our pricing packages for Cloud SAP ERP, SAP on Cloud software services and cheap, affordable & low cost SAP system for small business, Contact Us Now! <p>
                <p style="font-size:12px;"><strong>SAP certified provider of low cost & affordable SAP system for small business to large enterprises.</strong><br>
WFTCloud is a certified provider of low cost & affordable SAP system for small business to large enterprises. Get implementation of low cost & affordable SAP system for small business to large enterprises within a fraction of conventional cost.<p>--%>

        </div>
      </div>
</asp:Content>

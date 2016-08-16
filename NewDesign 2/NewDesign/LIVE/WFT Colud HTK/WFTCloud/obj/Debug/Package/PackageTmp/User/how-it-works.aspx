<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd.Master" AutoEventWireup="true" CodeBehind="how-it-works.aspx.cs" Inherits="WFTCloud.User.how_it_works" %>
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
            <%--<h4>How it Works</h4>
            <p>WFTCloud is a very simple concept. You can buy SAP systems on a shared or dedicated model. The packages are attractively priced and you could choose the one that meets your needs. In a shared model you would be provided with a single user login and for dedicated access you could get upto five users. Development keys are provided at an additional cost if you desire to use them for development.</p>
            <p>We also offer BASIS support to get you settled into the new systems and our technical consulting team can help you in migrations and landscape planning. You would be billed on a monthly basis. The costs can be further reduced if you shut down your dedicated systems at close of business.</p>--%>
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
            <h2 style="font-size:12px;">WFTCloud offers on-demand ERP solutions & on-demand CRM solutions!</h2>
            <p style="font-size:12px;">WFTCloud.com offers on- demand ERP solutions, on demand CRM solutions & SAP on the cloud support services at an unmatched cost. Utilize WFT's expertise in on-demand erp solutions, on-demand CRM solutions & SAP on the cloud support services for your business. Call Now!!!</p>
            <h2 style="font-size:12px;">Pay per use model for on-demand ERP and on-demand CRM solution on the cloud.</h2>
            <p style="font-size:12px;">We focus on reducing your ERP implementation cost by introducing pay per use model for <strong>on demand ERP solution & on-demand CRM solutions.</strong>To know more about our pricing packages for <strong>on-demand ERP solutions and on-demand CRM solutions</strong>, Contact Us Now!</p>
            <h2 style="font-size:12px;">SAP certified provider of affordable & low cost ERP on-demand solutions and SAP on cloud support services.</h2>
            <p style="font-size:12px;">WFTCloud is a certified provider of affordable & low cost ERP on demand solutions and SAP on cloud support services. Get implementation of ERP on demand solutions, on demand CRM, SAP on cloud support services & SAP remote access at a fraction of conventional cost.</p>

                  <%--    	<p style="font-size:12px;"><strong>WFTCloud offers legal SAP online access for cheap SAP systems & solutions!</strong><br>
WFTCloud.com offers legal SAP online access for small and medium scale enterprises and relatively cheap SAP systems & solutions on cloud SAP software at an unmatched cost. Utilize WFT's expertise in legal SAP online access & relatively cheap SAP systems & solutions for your business. Call Now!!!<p>
                <p style="font-size:12px;"><strong>Pay per use model for affordable & low cost SAP system for small business.</strong><br>
We focus on reducing your ERP implementation cost by introducing pay per use model for cheap, affordable & low cost SAP system for small business. To know more about our pricing packages for Cloud SAP ERP, SAP on Cloud software services and cheap, affordable & low cost SAP system for small business, Contact Us Now! <p>
                <p style="font-size:12px;"><strong>SAP certified provider of low cost & affordable SAP system for small business to large enterprises.</strong><br>
WFTCloud is a certified provider of low cost & affordable SAP system for small business to large enterprises. Get implementation of low cost & affordable SAP system for small business to large enterprises within a fraction of conventional cost.<p>--%>

        </div>
      </div>
</asp:Content>

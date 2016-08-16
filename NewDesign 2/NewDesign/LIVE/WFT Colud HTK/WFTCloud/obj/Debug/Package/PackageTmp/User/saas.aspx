<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd.Master" AutoEventWireup="true" CodeBehind="saas.aspx.cs" Inherits="WFTCloud.User.saas" %>
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
            <%--<h4>SAAS</h4>
            <p><strong>Software as a Service (SaaS)</strong> is very common these days. Initially propelled by salesforce.com and also by google applications <strong>SaaS</strong> has benefitted individual users, small business owners and even enterprises who require different iterations for different organizational units. WFT's initiative of provide <strong>SAP as a Service through SaaS ERP software</strong> is aligned to SAP's cloud computing approach. We, as <strong>ERP Saas vendor</strong>, believe that enterprises and customers can seamlessly migrate to a SaaS model and save on infrastructure costs and also reduce their carbon footprint by moving into a resource on demand model.</p>--%>
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
            <h2 style="font-size:12px;">WFTCloud – A global ERP SaaS vendor offers SaaS ERP software solutions!</h2>
            <p style="font-size:12px;">WFTCloud.com are the leading <strong>ERP SaaS vendors</strong>; offering <strong>SaaS ERP software solutions, ERP system on cloud</strong> & SAP on the cloud support services at an unmatched cost. Utilize expertise of a global<strong> ERP SaaS vendor in SaaS ERP software solutions, ERP system on cloud & SAP on the cloud support services </strong>for your business. Call Now!!!</p>
            <h2 style="font-size:12px;">Pay per use model for SaaS ERP software by leading ERP SaaS vendor.</h2>
            <p style="font-size:12px;">We focus on reducing your ERP implementation cost by introducing pay per use model for <strong>SaaS ERP software & cloud ERP software systems.</strong>To know more about pricing packages from a leading <strong>ERP SaaS vendor for SaaS ERP software & cloud ERP software systems</strong>, Contact Us Now!</p>
            <h2 style="font-size:12px;">SAP certified provider of affordable & low cost ERP software on cloud and SAP remote access.</h2>
            <p style="font-size:12px;">WFTCloud is a certified provider of affordable & low cost ERP software on cloud solutions & SAP remote access. Get implementation of ERP software on cloud services & SAP remote access from a leading ERP SaaS vendor for SaaS ERP software & cloud ERP software systems at a fraction of conventional cost.</p>


                    <%--  	<p style="font-size:12px;"><strong>WFTCloud offers legal SAP online access for cheap SAP systems & solutions!</strong><br>
WFTCloud.com offers legal SAP online access for small and medium scale enterprises and relatively cheap SAP systems & solutions on cloud SAP software at an unmatched cost. Utilize WFT's expertise in legal SAP online access & relatively cheap SAP systems & solutions for your business. Call Now!!!<p>
                <p style="font-size:12px;"><strong>Pay per use model for affordable & low cost SAP system for small business.</strong><br>
We focus on reducing your ERP implementation cost by introducing pay per use model for cheap, affordable & low cost SAP system for small business. To know more about our pricing packages for Cloud SAP ERP, SAP on Cloud software services and cheap, affordable & low cost SAP system for small business, Contact Us Now! <p>
                <p style="font-size:12px;"><strong>SAP certified provider of low cost & affordable SAP system for small business to large enterprises.</strong><br>
WFTCloud is a certified provider of low cost & affordable SAP system for small business to large enterprises. Get implementation of low cost & affordable SAP system for small business to large enterprises within a fraction of conventional cost.<p>--%>

        </div>
      </div>
</asp:Content>

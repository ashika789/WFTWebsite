<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd.Master" AutoEventWireup="true" CodeBehind="cloud-computing.aspx.cs" Inherits="WFTCloud.User.cloud_computing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Including main template styles -->
        <link rel="stylesheet" href="/css/main.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="content">
    <div class="white-section">
    <div class="container row">
          
          <div class="span12">
                 <div class="breadcrumb">
                                    <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" />
                                    <asp:SiteMapPath ID="SiteMap1" runat="server"></asp:SiteMapPath>
                                </div>
              <asp:Literal ID="Literal1" runat="server"></asp:Literal>
            <%--<h4>Cloud Computing</h4>
            <p><strong>Cloud Computing is the technology that allows IT consumers to use computing resources on demand. The main qualities of Cloud Computing are</strong></p>
            <ul>
            	<li>Ubiquitous Computing- One can access the resources from anywhere in the world with an internet connection</li>
                <li>Elasticity - Computing resources can be scaled up or down on demand</li>
                <li>Self or Rapid Provisioning - One can get resources added on demand</li>
                <li>Services Approach - Any computing resource is available as a service on demand</li>
            </ul>
            <h6>WFT and Cloud Computing</h6>
            <p>WFT has been involved in implementing large scale disaster recovery and business continuity solutions for SAP systems and has stayed in the forefront of technologies used to sustain this. In early 2006 WFT started a program to explore the use of SAP Adaptive computing and this led to research in Cloud based technologies . Through 2008 and 2009 WFT has worked extensively to come up with innovative solutions on the cloud for its clients. From mid 2010, WFT has worked extensively with Amazon and SAP to bring SAP on to the Amazon platform and now WFT is able to provide this service to end users with backing from both SAP and Amazon.</p>--%>
          </div>
          
          <!-- span12 end -->
          
          </div>
         </div>
                </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerSEOContent" runat="server">
      <hr />
      <div class="row">
        <div class="span12">
            <h2 style="font-size:12px;">WFTCloud offers cloud ERP solutions and cloud CRM solutions!</h2>
            <p style="font-size:12px;">WFTCloud.com offers <strong>cloud ERP solutions, cloud CRM solution</strong>& SAP on the cloud support services at an unmatched cost. Utilize WFT's expertise in <strong>cloud ERP solutions, cloud CRM solutions & SAP on the cloud support services </strong>for your business. Call Now!!!</p>
            <h2 style="font-size:12px;">Pay per use model for ERP on the cloud / ERP in the cloud.</h2>
            <p style="font-size:12px;">We focused on reducing your ERP implementation cost by introducing pay per use model for <strong>ERP on the cloud / ERP in the cloud & SAP on cloud support services.</strong> To know more about our pricing packages for <strong>ERP on the cloud / ERP in the cloud & SAP on cloud support services</strong>, Contact Us Now!</p>
            <h2 style="font-size:12px;">Affordable & low cost ERP cloud service system with SAP certified provider.</h2>
            <p style="font-size:12px;">Get affordable & low cost ERP cloud service system with SAP certified provider - WFTCloud. Get implementation of ERP cloud services & systems with our ERP on the cloud / ERP in the cloud solutions, systems & services at a fraction of conventional cost.</p>

                      	<%--<p style="font-size:12px;"><strong>WFTCloud offers legal SAP online access for cheap SAP systems & solutions!</strong><br>
WFTCloud.com offers legal SAP online access for small and medium scale enterprises and relatively cheap SAP systems & solutions on cloud SAP software at an unmatched cost. Utilize WFT's expertise in legal SAP online access & relatively cheap SAP systems & solutions for your business. Call Now!!!<p>
                <p style="font-size:12px;"><strong>Pay per use model for affordable & low cost SAP system for small business.</strong><br>
We focus on reducing your ERP implementation cost by introducing pay per use model for cheap, affordable & low cost SAP system for small business. To know more about our pricing packages for Cloud SAP ERP, SAP on Cloud software services and cheap, affordable & low cost SAP system for small business, Contact Us Now! <p>
                <p style="font-size:12px;"><strong>SAP certified provider of low cost & affordable SAP system for small business to large enterprises.</strong><br>
WFTCloud is a certified provider of low cost & affordable SAP system for small business to large enterprises. Get implementation of low cost & affordable SAP system for small business to large enterprises within a fraction of conventional cost.<p>--%>

        </div>
      </div>
</asp:Content>

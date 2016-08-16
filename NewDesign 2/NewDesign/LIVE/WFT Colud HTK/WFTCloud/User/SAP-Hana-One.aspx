<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd.Master" AutoEventWireup="true" CodeBehind="SAP-Hana-One.aspx.cs" Inherits="WFTCloud.User.SAP_Hana_One" %>
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
           <%-- <h4>SAP HANA One <span style="font-size:14px;">- a combination of the fastest in-memory platform and the cloud</span></h4>
            <p>SAP HANA One is the SAP in-memory platform combining transactional and analytical processing together, enabling real-time business in the cloud. Customers can perform real-time analysis, develop and deploy real-time applications with the SAP HANA One, an instance of SAP HANA in-memory platform. Natively build on in-memory technology and now available through WFT Cloud, SAP HANA One is designed to accelerate transactional processing, operational reporting, OLAP, predictive and text analysis while by-passing traditional data latency and maintenance issues created through pre- materializing views and pre-caching query results.</p>
            <h6>WFT Cloud - HANA One</h6>
            <p>WFT Cloud HANA One offers a perfect platform for developers, startups, and customers who require proof-of-concept environments. In addition to providing a cloud based HANA One Solution, WFT also offers complete support services for the HANA One during the subscription period.</p>
            <h4>WFT Cloud HANA Cloud offerings:</h4>
            <h6>HANA ONE DEV EDITION - Provides developers immediate access to SAP HANA for the following two configurations:</h6>
            <ul>
            	<li>16GB RAM / 4 vCPU / 200GB Disk</li>
                <li>32GB RAM / 6 vCPU / 300GB Disk</li>
            </ul>
            <h4>WFT Cloud HANA One - Key Benefits:</h4>
            <h6>Fast Provisioning</h6>
            <ul>
            	<li>HANA One Systems are Available in Hours</li>
                <li>Pre-Configured to Customer Requirements</li>
            </ul>
            <h6>Integration and Data Migration Support</h6>
            <ul>
            	<li>Assistance and support for Data Integration</li>
                <li>Assistance and support for Data Migration</li>
            </ul>
            <h6>Free Operational Management</h6>
            <ul>
            	<li>WFTCloud is responsible for operational support of HANA One.</li>
                <li>SLA covers the Availability and Data Protection requirements of the Client</li>
            </ul>
            <h6>Enterprise-class infrastructure</h6>
            <ul>
            	<li>Actively managed and maintained</li>
                <li>Ensures security and reliability</li>
            </ul>--%>
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
                      	<p style="font-size:12px;"><strong>WFTCloud offers legal SAP online access for cheap SAP systems & solutions!</strong><br>
WFTCloud.com offers legal SAP online access for small and medium scale enterprises and relatively cheap SAP systems & solutions on cloud SAP software at an unmatched cost. Utilize WFT's expertise in legal SAP online access & relatively cheap SAP systems & solutions for your business. Call Now!!!<p>
                <p style="font-size:12px;"><strong>Pay per use model for affordable & low cost SAP system for small business.</strong><br>
We focus on reducing your ERP implementation cost by introducing pay per use model for cheap, affordable & low cost SAP system for small business. To know more about our pricing packages for Cloud SAP ERP, SAP on Cloud software services and cheap, affordable & low cost SAP system for small business, Contact Us Now! <p>
                <p style="font-size:12px;"><strong>SAP certified provider of low cost & affordable SAP system for small business to large enterprises.</strong><br>
WFTCloud is a certified provider of low cost & affordable SAP system for small business to large enterprises. Get implementation of low cost & affordable SAP system for small business to large enterprises within a fraction of conventional cost.<p>

        </div>
      </div>
</asp:Content>

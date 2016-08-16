<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd.Master" AutoEventWireup="true" CodeBehind="AboutUs.aspx.cs" Inherits="WFTCloud.User.AboutUs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <!-- Including main template styles -->
        <link rel="stylesheet" href="/css/main.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="content">
    <div class="white-section">
      <div class="container">
        <div class="row">
               <div class="breadcrumb">
                                    <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" />
                                    <asp:SiteMapPath ID="SiteMap1" runat="server"></asp:SiteMapPath>
                                </div>
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
         <%-- <div class="span7">
            <h4>About Us</h4>
            <h6>Welcome to WFT Cloud<br>
              - <span style="font-size:14px;">A certified SAP Hosting and Cloud Services Partner</span> </h6>
            <p>WFTCloud is an initiative by Wharfedale Technologies, Inc. providing Cloud SAP ERP software solutions. We are the certified provider of Cloud SAP ERP modules and Cloud SAP ERP systems. We offer Amazon’s Virtual Private Cloud Environment with restricted access and WFT’s technical expertise on SAP on Cloud support services. We ensure no problem SAP access. We are our client’s trusted advisor and a premier provider of Infrastructure Integration, delivering:</p>
            <ul>
                <li>High Quality</li>
                <li>Flexibility</li>
                <li>Cost-Effectiveness</li>
            </ul>
              <p>Founded in 2000, Wharfedale Technologies, Inc. (WFT) is a leader in providing innovative solutions to Fortune 1000 and enterprise-class clients. </p>
            <p>With extensive expertise in both the SAP application environments and infrastructure Solutions, WFT works closely with our partners to develop and deliver solutions to clients in the financial, pharmaceutical, manufacturing, retail, distribution and entertainment industries, as well as key government agencies. </p>
            <p>Our SAP services & innovative solutions have a proven track record in the industry. Our rich experience spans global SAP implementation, upgrade, rollout, application maintenance and support, solution deployment and business transformation. </p>
          </div>
          <!-- span7 end -->
          
          <div class="span5">
            <h4>WFT Cloud</h4>
            <p> WFT Cloud, launched in 2011, is a fully owned subsidiary of Wharfedale Technologies Inc.(WFT), specializing in cloud based SAP system infrastructure integration and operational management services for SAP® solutions. </p>
            <p>While, WFT Cloud started with offering shared access SAP Services alone, the overwhelming demands and the goal to provide excellent service to our clients around the globe, we have deployed dedicated model with complete SAP Basis support and maintenance included in the package. </p>
             <h6>Mission of WFT </h6>
             <blockquote class="articleabout"> "To help Customers achieve their business objectives, by providing Innovative, Best-in- class IT Consulting, Solutions and Services."</blockquote>
          </div>
          <!-- span5 end -->
          <p style="clear:both;"></p>
          <p>Our services are categorized into two levels to meet various SAP-on-Cloud requirements from clients worldwide.</p>
          <ul class="otherlinks">
            <li>Enterprise Cloud    For clients of medium and large enterprise level </li>
            <li>Express Cloud     For clients of small enterprise, individual developer, training institute, proof-of-concept (POC), development, and sandbox</li>
          </ul>
          
          <h6>Enterprise Cloud </h6>
          <ul class="article">
            <li><strong>Private  Cloud</strong>&nbsp; &nbsp; Building SAP  on-premise Enterprise Private Cloud including hardware with utility model &amp;  support </li>
            <li><strong>SAP HANA  Services </strong>&nbsp; &nbsp;Assessment, Migration /  Support.</li>
            <li><strong>SAP Basis  Services </strong>&nbsp; &nbsp;Remote Basis Support at  utility pricing per hour per server with 24*7*365 support</li>
            <li><strong>Disaster  Recovery </strong>&nbsp; &nbsp;Recovery as a Service  for SAP (RaaS) </li>
            <li><strong>SAP  Infrastructure Services</strong>&nbsp;  &nbsp; SAP Landscape Design &amp; Optimization, High Availability, Disaster  Recovery, Virtualization, SAP Upgrades, etc.</li>
            <li><strong>SAP  Migration Services</strong>&nbsp; &nbsp;SAP Cross Platform  OS/DB migration with minimum downtime </li>
          </ul>
          <h6>Express Cloud </h6>
          <p>Serving customers with express delivery of the non-productive SAP systems on a single click. Customers can leverage this Express Cloud services effectively for POC, training, testing, and development purpose. </p>
          <ul class="article">
            <li>Dedicated Server</li>
            <li>Dedicated Client</li>
            <li>IDES-Shared Access</li>
            <li>SAP HANA One</li>
          </ul>
            <h6>Benefits of WFT Cloud</h6>
            <ul class="article">
              <li>SAP Certified Hosting and Cloud  Services Provider</li>
              <li>Reliable cost effective  infrastructure tailored for enterprise needs</li>
              <li>Flexible open system architecture to  support any business needs</li>
              <li>Highly Scalable to varying resource  requirements</li>
              <li>Convenient monthly utility billing</li>
              <li>Responsive and effective customer  support</li>
              <li>Access to industry best practices  and whitepapers</li>
              <li>SAP basic BASIS Support is included  with all our Express Cloud Services.</li>
            </ul>
            <h6>Value Proposition </h6>
            <p> "We believe in becoming our client's trusted advisor and an extension of their team. We achieve this goal with every client by utilizing our TCO methodology to identify their true IT needs, present them with options, and develop a flexible solution that meets their current and future requirements.  This unbiased methodology has helped us develop long-term and trusting relationships with our clients." as said by Mr. Ganesh Radhakrishnan, Chief Executive Officer, WFT Cloud.</p>
            <p>Our flexible, high quality and cost effective managed services and hosting solutions set us apart from the competition and deliver a reduction in our clients:
            <ul class="article">
              <li>TCO by 15% to 40% </li>
              <li>Time of deployment down time </li>
              <li>Focus on continuous support </li>
            </ul>
            </p>
            <p> ...While significantly improving quality of service.
              Our clients attest to the WFT value proposition as evidenced by the following quote from a Fortune 500 CIO: "I can honestly say that we are receiving outstanding support. WFT is more than a vendor of managed services to us; they have become trusted advisors and integral members of my team." </p>
            </p>--%>
          </div>
        </div>
        <!-- row end --> 
        
        <!-- row end --> 
        
      </div>
    <!-- conteiner end --> 
    </section>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerSEOContent" runat="server">
      <hr />
      <div class="row">
        <div class="span12">
               <h2 style="font-size:12px;">WFTCloud offers cloud SAP ERP software & Cloud SAP ERP solutions! </h2>
              	<p style="font-size:12px;">WFTCloud.com offers its services in Cloud SAP ERP software, Cloud SAP ERP solution & SAP on the Cloud support services at an unmatched cost. Utilize WFT's expertise in Cloud SAP ERP software, Cloud SAP ERP solution & SAP on the Cloud support services for your business. Call Now!!!<p>
                <h2 style="font-size:12px;">Pay per use model for Cloud SAP ERP modules & Cloud SAP ERP Systems.</h2>
                <p style="font-size:12px;">We focus on reducing your ERP implementation cost by introducing pay per use model for Cloud SAP ERP modules, Cloud SAP ERP systems & SAP on Cloud support services. To know more about our pricing packages for Cloud SAP ERP modules, Cloud SAP ERP systems & SAP on Cloud support services, Contact Us Now!<p>
                <h2 style="font-size:12px;">SAP certified provider of affordable & low cost SAP system and SAP remote access. </h2>
                <p style="font-size:12px;">WFTCloud is a certified provider of cloud SAP ERP software, cloud SAP ERP solution, cloud SAP ERP modules, cloud SAP ERP systems & SAP on cloud support services. Get implementation of cloud SAP ERP software, cloud SAP ERP solution, cloud SAP ERP modules, cloud SAP ERP systems & SAP on cloud support services at a fraction of conventional cost.<p>

       <%-- 	<p style="font-size:12px;"><strong>WFTCloud offers cloud SAP ERP software & Cloud SAP ERP solutions! </strong><br>
WFTCloud.com offers its services in Cloud SAP ERP software, Cloud SAP ERP solution & SAP on the Cloud support services at an unmatched cost. Utilize WFT's expertise in Cloud SAP ERP software, Cloud SAP ERP solution & SAP on the Cloud support services for your business. Call Now!!!<p>
                <p style="font-size:12px;"><strong>Pay per use model for Cloud SAP ERP modules & Cloud SAP ERP Systems.</strong><br>
We focus on reducing your ERP implementation cost by introducing pay per use model for Cloud SAP ERP modules, Cloud SAP ERP systems & SAP on Cloud support services. To know more about our pricing packages for Cloud SAP ERP modules, Cloud SAP ERP systems & SAP on Cloud support services Contact Us Now!<p>
                <p style="font-size:12px;"><strong>SAP certified provider of affordable & low cost SAP system and SAP remote access. </strong><br>
WFTCloud is a certified provider of cloud SAP ERP software, cloud SAP ERP solution, cloud SAP ERP modules, cloud SAP ERP systems & SAP on cloud support services. Get implementation of cloud SAP ERP software, cloud SAP ERP solution, cloud SAP ERP modules, cloud SAP ERP systems & SAP on cloud support services at a fraction of conventional cost.<p>
       --%>
         </div>
      </div>
</asp:Content>
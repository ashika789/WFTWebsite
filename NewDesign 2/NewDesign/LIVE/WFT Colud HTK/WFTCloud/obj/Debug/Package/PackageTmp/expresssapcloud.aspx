﻿<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="expresssapcloud.aspx.cs" Inherits="WFTCloud.expresssapcloud" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <meta charset="utf-8">
  <title>WFT Cloud - Express SAP Cloud</title>
  <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
  <meta name="description" content="">
  <meta name="author" content="">
  <meta name="keywords" content="">
  <!-- base styles -->
  <link href="asset/css/styles.css" rel="stylesheet">
  <link href="asset/css/styles-custom.css" rel="stylesheet">
  <!-- google font styles -->
  <!-- <link href='http://fonts.googleapis.com/css?family=Merriweather:400,700,900' rel='stylesheet' type='text/css'> -->
  <!-- Le HTML5 shim, for IE6-8 support of HTML5 elements -->
  <!--[if lt IE 9]>
  <script src="asset/js/vendor/html5shiv.min.js"></script>
  <script src="asset/js/vendor/respond.min.js"></script>
  <![endif]-->
  <!-- Le fav and touch icons -->
  <link rel="shortcut icon" href="asset/images/favicon.ico">
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css">
</head>
<body>
     <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="CloudAnalyticsToolkitScriptManager" runat="server">
        </asp:ToolkitScriptManager>
<header class="navbar-fixed-top">
  <nav class="top-navbar">
    <div class="container">
      <div class="row">
        <div class="col-md-6">
          <div class="top-left-sap">
            <a href="#"> Cloud Enablement for SAP Customers</a>
          </div>
        </div>
        <div class="col-md-6">
          <ul class="top-right-nav">
             <li style="color:white;">Call Us Today! 1-888-533-3113</li>
            <li><a href="Support.html"><img src="img/phone-s.png" />Support</a></li>
            <li><a href="contact.aspx"><img src="img/emailicon.png" />Contact</a></li>
            <li><a id="A1" runat="server" href="/Login.aspx" data-toggle="modal">
            <%--<i class="icon-user"></i>--%><img src="/img/log-in-s.jpg" /><strong id="loginUser" runat="server" >&nbsp;&nbsp;Member Login</strong>
            </a>          
            </li>
                        <li runat="server" id="lilogout" visible="false">
              <strong>
                  <asp:LinkButton runat="server" ID="lkbtnLogOut" style="color: #F0F0F0" OnClick="lkbtnLogOut_Click" >
                      <strong>Log Out</strong>
                     </asp:LinkButton>
               </strong>
              </li>
          </ul>
        </div>
      </div>
    </div>
  </nav>
  <!-- main navigation -->
  <div class="primary-navbar">
    <div class="container">
      <div class="row">
        <div class="col-md-12">
          <nav class="navbar">
            <div class="container">
              <div class="navbar-header">
                <button type="button" class="navbar-toggle collapsed" data-toggle="collapse"
                        data-target="#navbar" aria-expanded="false" aria-controls="navbar">
                  <span class="sr-only">Toggle navigation</span>
                  <span class="icon-bar"></span>
                  <span class="icon-bar"></span>
                  <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand logo" href="Home.aspx"> <img
                  src="asset/images/WFTCloud-Logo.jpg" width="218" height="98" alt=""> </a>
              </div>
              <div id="navbar" class="navbar-collapse collapse">
                <ul class="nav navbar-nav navbar-right">
                  <li class="active"><a href="expresssapcloud.aspx">sap ides</a></li>
                  <li><a href="ExpressCloud.aspx">pricing</a></li>
                   <li><a id="hrefloggedInLink" runat="server" href="Login.aspx">MY ACCOUNT</a></li>
                  <li><a href="faqs.html">faq</a></li>
                  <li><a href="terms.html">terms</a></li>
                  <li><a href="http://blog.wftcloud.com/" target="_blank">blog</a></li>
                  <li><a href="contact.aspx">contact us</a></li>
                </ul>
              </div><!--/.nav-collapse -->
            </div>
          </nav>
        </div>
      </div>
    </div>
  </div>
</header>
<div class="clearfix"></div>
<!-- /header -->
<section class="main-banner">
  <div class="container">
    <div class="row">
      <div class="col-md-12">
        <img class="img-responsive" src="asset/images/banner-02.jpg" alt="">
      </div>
    </div>
  </div>
</section>
<!-- /banner -->
<section class="main-content solutions-content">
  <div class="container">
    <div class="row">
      <div class="col-md-12">
        <div class="">
          <h2>Express SAP Cloud Hosting Services</h2>
          <p style="font-size: 120%;">Host your SAP landscape in the cloud provided by WFT Cloud Hosting Services and managed by WFTCloud. We make sure
            that SAP systems are administered according to the best practices provided by WFT Cloud Hosting Services Web Services and
            SAP.</p>
        </div>
      </div>
    </div>

      <div class="row">
      <div class="col-md-12 text-center">
        <h3>Explore Our Preconfigured SAP Hosting Solutions</h3>
        <p class="col-md-10 col-md-offset-1" style="font-size: 120%;">For small and mid-size companies we have prepared several out-of-the-box solutions for hosting and supporting their SAP systems. Our out-of-the-box solutions include preconfigured SAP systems deployed on servers in the compute cloud powered by WFT Cloud. The database and SAP application server software are installed on the same host for these solutions.</p>
      </div>
    </div>
   
        <div class="row">
            <div class="col-md-3">
                <div class="panel panel-default pricing-panel" style="background-color: #edfb9c;background-color: #1afbdc;box-shadow: 5px 5px 10px #b5b5b5;border: 3px solid #35998b;">
                    <div class="panel-heading">IDES Shared</div>
                    <div class="panel-body">
                        <span>$</span>
                        <span class="price">9.99</span>
                        <span>/MO</span>
                    </div>
                    
                    <a href="ExpressCloud.aspx" class="btn btn-warning btn-block"><i class="fa fa-chevron-circle-right p-r-sm"></i> Learn More</a>
                </div>
            </div>
            <div class="col-md-3">
                <div class="panel panel-default pricing-panel" style="background-color: #A4F3C6">
                    <div class="panel-heading">IDES Dedicated Client</div>
                    <div class="panel-body">
                        <span>$</span>
                        <span class="price">0.62</span>
                        <span>/HOUR</span>
                        <span class="fine-print">* Monthly 728 Hours</span>
                    </div>
                    
                    <a href="ExpressCloud.aspx" class="btn btn-warning btn-block"><i class="fa fa-chevron-circle-right p-r-sm"></i> Learn More</a>
                </div>
            </div>
            <div class="col-md-3">
                <div class="panel panel-default pricing-panel" style="background-color: #BFF4FC">
                    <div class="panel-heading">Dedicated Server</div>
                    <div class="panel-body">
                        <span>$</span>
                        <span class="price">0.62</span>
                        <span>/HOUR</span>
                        <span class="fine-print">* Monthly 728 Hours</span>
                    </div>
                   
                    <a href="ExpressCloud.aspx" class="btn btn-warning btn-block"><i class="fa fa-chevron-circle-right p-r-sm"></i> Learn More</a>
                </div>
            </div>
            <div class="col-md-3">
                <div class="panel panel-default pricing-panel" style="background-color: #A2F4F0">
                    <div class="panel-heading">HANA Shared</div>
                    <div class="panel-body">
                        <span>$</span>
                        <span class="price">0.40</span>
                        <span>/HOUR</span>
                        <span class="fine-print">* Monthly 728 Hours</span>
                    </div>
                   
                    <a href="ExpressCloud.aspx" class="btn btn-warning btn-block"><i class="fa fa-chevron-circle-right p-r-sm"></i> Learn More</a>
                </div>
            </div>
            
        </div>
         <div class="row">
            
           
            <div class="col-md-3">
                <div class="panel panel-default pricing-panel" style="background-color: #F5C283">
                    <div class="panel-heading">HANA Dedicated</div>
                    <div class="panel-body">
                        <span>$</span>
                        <span class="price">0.78</span>
                        <span>/HOUR</span>
                        <span class="fine-print">* Monthly 728 Hours</span>
                    </div>
                    
                    <a href="ExpressCloud.aspx" class="btn btn-warning btn-block"><i class="fa fa-chevron-circle-right p-r-sm"></i> Learn More</a>
                </div>
            </div>
            <div class="col-md-3">
                <div class="panel panel-default pricing-panel" style="background-color: #F5C283">
                    <div class="panel-heading">Shared Combo</div>
                    <div class="panel-body">
                        <span>$</span>
                        <span class="price">0.78</span>
                        <span>/HOUR</span>
                        <span class="fine-print">* Monthly 728 Hours</span>
                    </div>
                    
                    <a href="ExpressCloud.aspx" class="btn btn-warning btn-block"><i class="fa fa-chevron-circle-right p-r-sm"></i> Learn More</a>
                </div>
            </div>
            <div class="col-md-3">
                <div class="panel panel-default pricing-panel" style="background-color: #B6DCFF">
                    <div class="panel-heading">Remote BASIS Support</div>
                    <div class="panel-body">
                        <span>$</span>
                        <span class="price">2.29</span>
                        <span>/HOUR</span>
                        <span class="fine-print">* Monthly 728 Hours</span>
                    </div>
                  
                    <a href="ExpressCloud.aspx" class="btn btn-warning btn-block"><i class="fa fa-chevron-circle-right p-r-sm"></i> Learn More</a>
                </div>
            </div>
        </div>
       <hr class="divider">
    <div class="row">
      <div class="col-md-12 text-center">
        <h3>Reliable Infrastructure from WFT Cloud Hosting Services</h3>
        <p class="col-md-10 col-md-offset-1" style="font-size: 120%;text-align:justify;">WFT Cloud Hosting Services is an SAP-certified Global Technology Partner and focuses on providing a highly reliable, scalable, low-cost infrastructure platform in the cloud. WFT Cloud Hosting Services DOES NOT offer services specific to SAP such SAP managed hosting services.</p>
      </div>
    </div>
   <div class="row m-t-md m-b-md">
      <div class="col-md-4 text-center">
        <i class="fa fa-4x fa-hand-o-up c-blue"></i>
        <h3>Flexible</h3>
        <p class="col-md-10 col-md-offset-1" style="text-align:justify">Eliminate guesswork on your SAP infrastructure capacity needs. With AWS you
          can access as much or as little capacity as you need, and scale up and down as required in only a few
          minutes.</p>
      </div>
      <div class="col-md-4 text-center">
        <i class="fa fa-4x fa-money c-blue"></i>
        <h3>Cost-Effective</h3>
        <p class="col-md-10 col-md-offset-1" style="text-align:justify">Start an SAP implementation or project on AWS without any upfront cost or
          commitment for compute, storage, or net-work infrastructure (OpEx instead of CapEx).</p>
      </div>
      <div class="col-md-4 text-center">
        <i class="fa fa-4x fa-shield c-blue"></i>
        <h3>Secure</h3>
        <p class="col-md-10 col-md-offset-1" style="text-align:justify">Connect directly via the Internet or connect your corporate data center to
          your WFT Cloud Hosting Services Virtual Private Cloud securely with either a VPN or a private, dedicated
          network connection.</p>
      </div>
      <div class="col-md-4 text-center">
        <i class="fa fa-4x fa-tasks c-blue"></i>
        <h3>Reliable</h3>
        <p class="col-md-10 col-md-offset-1" style="text-align:justify">WFT Cloud Hosting Services EC2 offers a highly reliable environment where
          replacement instances can be rapidly and predictably commissioned. The service runs within WFT Cloud Hosting
          Services’s proven net-work infrastructure and data centers.</p>
      </div>
      <div class="col-md-4 text-center">
        <i class="fa fa-4x fa-rocket c-blue"></i>
        <h3>Powerful</h3>
        <p class="col-md-10 col-md-offset-1" style="text-align:justify">AWS servers are optimized to deliver high performance for SAP systems. SAP
          HANA was certified by SAP to be run on a variety of WFT Cloud Hosting Services EC2 instance types.</p>
      </div>
      <div class="col-md-4 text-center">
        <i class="fa fa-4x fa-bar-chart-o c-blue"></i>
        <h3>Competitive</h3>
        <p class="col-md-10 col-md-offset-1" style="text-align:justify">Wish there was a simple step you could take to become more competitive?
          Moving to the cloud gives access to enterprise-class SAP hosting, for everyone. It also allows smaller
          businesses to act faster than big, established competitors.</p>
      </div>
    </div>
   
    
  </div>
</section>

<!-- footer -->
<footer class="moderate-blue global-sap">
  <div class="container">
    <div class="row">
      <div class="col-md-2">
        <div class="list-with-heading">
          <h4>Solutions</h4>
          <ul>
            <li><a href="solution-01.html">SAP Managed Cloud</a></li>
            <li><a href="solution-02.html">SAP DRaaS</a></li>
            <li><a href="solution-03.html">SAP Legacy Data for Audit</a></li>
            <li><a href="solution-04.html">SAP HANA Solutions</a></li>
            <li><a href="express-sap-cloud.html">Express SAP Cloud</a></li>
          </ul>
        </div>
      </div>
      <div class="col-md-2">
        <div class="list-with-heading">
          <h4>Company</h4>
          <ul>
            <li><a href="about.html">About Us</a></li>
            <li><a href="customers.html">Customers</a></li>
            <li><a href="partners.html">Partners</a></li>
            <li><a href="request-quote.html">Support</a></li>
            <li><a href="contact.aspx">Contact Us</a></li>
          </ul>
        </div>
      </div>
      <div class="col-md-2">
        <div class="list-with-heading">
          <h4>Support Center</h4>
          <ul>
            <li><a href="mailto:support@wftcloud.com">support@wftcloud.com</a></li>
            <li><a href="#">Live support</a></li>
            <li><a href="contact.aspx">Contact us</a></li>
          </ul>
        </div>
      </div>
      <div class="col-md-3">
        <div class="list-with-heading text-center">
          <h4>Verified & Secured by</h4>
          <ul class="">
            <li style="margin-bottom: 10px;"><img
              src="https://www.paypalobjects.com/webstatic/en_IN/mktg/logos/pp_cc_mark_37x23.jpg" border="0"
              alt="PayPal Logo"></li>
            <li style="margin-bottom: 10px;"><img src="https://verify.authorize.net/anetseal/images/secure90x72.gif"
                                                  border="0" alt="Authorize.net Logo"></li>
            <li style="margin-bottom: 10px;">
              <span id="siteseal">
                <script type="text/javascript"
                        src="https://seal.godaddy.com/getSeal?sealID=V5TQsXzyyUZb5UGNilQkjMFnJoP89ydXeVA0DzJ9xvyDi7Da3eNuU2kfIoyX"></script>
              </span>
            </li>
          </ul>
        </div>
      </div>
      <div class="col-md-3">
        <div class="list-with-heading">
          <h4>Connect with us</h4>
          <ul class="social-media">
            <li>
              <a href="http://twitter.com/wftus">
                <span class="fa-stack fa-lg">
                  <i class="fa fa-square-o fa-stack-2x"></i>
                  <i class="fa fa-twitter fa-stack-1x"></i>
                </span>
              </a>
            </li>
            <li><a href="http://facebook.com/wftcloud">
              <span class="fa-stack fa-lg">
                  <i class="fa fa-square-o fa-stack-2x"></i>
                  <i class="fa fa-facebook fa-stack-1x"></i>
                </span>
            </a></li>
            <li><a href="https://plus.google.com/100277864576102202208/posts">
              <span class="fa-stack fa-lg">
                  <i class="fa fa-square-o fa-stack-2x"></i>
                  <i class="fa fa-google-plus fa-stack-1x"></i>
                </span>
            </a></li>
            <li><a href="http://www.linkedin.com/company/wft-cloud">
              <span class="fa-stack fa-lg">
                  <i class="fa fa-square-o fa-stack-2x"></i>
                  <i class="fa fa-linkedin fa-stack-1x"></i>
                </span>
            </a></li>
          </ul>
        </div>
      </div>
      <div class="col-md-12 m-t-md">
        <p class="footer-para">WFTCloud offers SAP, ERP Cloud computing solutions & systems! WFTCloud.com offers SAP on
          the cloud computing solutions, services & systems including Cloud ERP & CRM on-demand solutions at an
          unmatched cost. Utilize WFT's expertise for SAP cloud computing solutions including Cloud ERP & CRM on-demand
          solutions for your business. Call Now!!!</p>
        <p class="footer-para"><strong>PAY PER USE MODEL FOR CLOUD SAP ERP SYSTEMS & ERP ON THE CLOUD
          SOLUTIONS.</strong></p>
        <p class="footer-para">We drastically reduced your SAP implementation cost by introducing a pay per use model
          for online SAP access, cloud SAP ERP system, on demand SAP & ERP on the cloud solutions. To know more about
          our pricing packages for cloud SAP ERP solutions, on demand ERP, web based ERP systems & SAP ERP on the cloud
          services Contact Us Now!</p>
        <p class="footer-para"><strong>SAP CERTIFIED PROVIDER OF SAP, ERP CLOUD SERVICES.</strong></p>
        <p class="footer-para">WFTCloud is a certified provider of SAP cloud computing solutions, cloud SAP ERP systems,
          ERP on the cloud, on demand ERP, web based ERP systems & SAP cloud services. Get implementation of cloud SAP
          ERP system, ERP on the cloud, on demand ERP, web based ERP system & SAP cloud services at a fraction of
          conventional cost.</p>
      </div>
    </div>
  </div>
</footer>

<section class="dark-blue copyright">
  <div class="container">
    <div class="row">
      <div class="col-md-12">
        <p>&copy; Copyright 2016. WFTCloud. All right reserved.</p>
      </div>
    </div>
  </div>
</section>


<!-- Placed at the end of the document so the pages load faster -->
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
<script src="asset/js/bootstrap.js"></script>
<!-- <script src="asset/js/bootstrap-hover-dropdown.min.js"></script> -->
</form> 
</body>
</html>

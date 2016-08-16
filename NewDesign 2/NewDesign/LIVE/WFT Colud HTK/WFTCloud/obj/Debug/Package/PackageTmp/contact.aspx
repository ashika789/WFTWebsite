﻿<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="contact.aspx.cs" Inherits="WFTCloud.contact" %>

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
             <li><a href="company.html">Company</a></li>
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
                  <li><a href="expresssapcloud.aspx">sap ides</a></li>
                  <li><a href="ExpressCloud.aspx">pricing</a></li>
                     <li><a id="hrefloggedInLink" runat="server" href="Login.aspx">MY ACCOUNT</a></li>
                  <li><a href="faqs.html">faq</a></li>
                  <li><a href="terms.html">terms</a></li>
                  <li><a href="http://blog.wftcloud.com/" target="_blank">blog</a></li>
                  <li class="active"><a href="contact.aspx">contact us</a></li>
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
<section class="main-content solutions-content m-b-lg">
  <div class="container">
    <div class="row">
      <div class="col-md-12">
        <div class="">
          <h2>Contact Us</h2>
        </div>
      </div>
    </div>
    <div class="row">
      <div class="col-md-7">
        <h3>Contact Wharfedale Technologies</h3>
        <h4>A certified provider of Web based ERP system and software solutions</h4>
        <p style="text-align:justify;">Wharfedale Technologies corporate office is located in Central New Jersey, within easy reach of New York City
          and Philadelphia. Wharfedale also has one office dedicated to our Federal Division which is located in
          Northern Virginia. WFTCloud is an initiative by Wharfedale Technologies, Inc. providing web based ERP system
          &amp; software solutions on the Cloud. We are a certified provider of web-based ERP software solutions by
          SAP. </p>

        <h4 style="text-align:left;">Contact info</h4>

        <div class="row">
          <div class="col-md-6">
            <h4>USA</h4>
            <ul>
              <li><i class="fa fa-briefcase c-blue m-r-sm"></i>Wharfedale Technologies Inc. (HQ)</li>
              <li><i class="fa fa-map-marker c-blue m-r-sm"></i>2850 Brunswick Pike, Lawrenceville, NJ 08648</li>
              <li><i class="fa fa-phone c-blue m-r-sm"></i>Phone: 609-882-8826</li>
              <li><i class="fa fa-envelope c-blue m-r-sm"></i>Email: <a href="mailto:wftsales@wftus.com">wftsales@wftus.com</a>
              </li>
            </ul>
          </div>
          <div class="col-md-6">
            <h4>Canada</h4>
            <ul>
              <li><i class="fa fa-briefcase c-blue m-r-sm"></i>Wharfedale Technologies Inc.</li>
              <li><i class="fa fa-map-marker c-blue m-r-sm"></i>888 3rd Street SW Suite 1000, Calgary, Alberta, T2P 5C5
              </li>
              <li><i class="fa fa-phone c-blue m-r-sm"></i>Phone: 403-444-5550</li>
              <li><i class="fa fa-envelope c-blue m-r-sm"></i>Email: <a href="mailto:wftsales@wftus.com">wftsales@wftus.com</a>
              </li>
            </ul>
          </div>
          <div class="col-md-6">
            <h4>India</h4>
            <ul>
              <li><i class="fa fa-briefcase c-blue m-r-sm"></i>Wharfedale Technologies Inc.</li>
              <li><i class="fa fa-map-marker c-blue m-r-sm"></i>#40,Ground Floor, <br>SP Infocity
                M G R Salai, Perungudi,<br>Chennai - 600096, <br>Tamil Nadu
              </li>
              <li><i class="fa fa-phone c-blue m-r-sm"></i>Phone: +91 9443387808</li>
              <li><i class="fa fa-envelope c-blue m-r-sm"></i>Email: <a href="mailto:wftsales@wftus.com">wftsales@wftus.com</a>
              </li>
            </ul>
          </div>
        </div>
      </div>
      <div class="col-md-5">
        <div>
          <iframe width="425" height="400" frameborder="0" scrolling="no" marginheight="0" marginwidth="0"
                  src="https://maps.google.com/maps?q=Wharfedale+Technologies,+2850+US+Highway+1+%23+2,+Lawrenceville,+NJ+08648&amp;hl=en&amp;sll=46.980252,-79.453125&amp;sspn=24.246317,86.572266&amp;hq=Wharfedale+Technologies,+2850+US+Highway+1+%23+2,&amp;hnear=Lawrenceville,+Lawrence+Township,+Mercer,+New+Jersey&amp;t=m&amp;ie=UTF8&amp;cid=17621828771337059437&amp;source=embed&amp;ll=40.269343,-74.712063&amp;spn=0.002865,0.00456&amp;z=17&amp;iwloc=A&amp;output=embed"></iframe>
        </div>
      </div>
      <div class="col-md-12 m-t-md">
        <h3>How Can WFT Help You?</h3>
        <p>Give us a call or e-mail to discuss all of the available WFT Hosting solutions and how they will benefit your organization.</p>
        <ul class="list-unstyled">
          <li><strong>Sales &amp; Information:</strong> <a href="mailto:wftsales@wftus.com">wftsales@wftus.com</a></li>
          <li><strong>Customer Service:</strong> <a href="mailto:admin@wftus.com">admin@wftus.com</a></li>
          <li><strong>Technical Support:</strong> <a href="mailto:support@wftus.com">support@wftus.com</a></li>
        </ul>
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
            
          </ul>
            <h4>Contact</h4>
          <ul>
            <li><a href="contact.aspx">Contact Details</a></li>
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
            <div class="list-with-heading">
          <h4>Signup for Newsletter</h4>
          <input type="email" class="form-control m-b-sm" id="rq-name" placeholder="Enter your email ID">
          <button type="submit" class="btn btn-primary">Submit</button>
        </div>
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

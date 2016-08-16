<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="WFTCloud.Home" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<html lang="en">
<head>
  <meta charset="utf-8">
  <title>WFT Cloud Server</title>
  <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no"/>

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
    <form runat="server" >
<header class="navbar-fixed-top">
  <nav class="top-navbar">
    <div class="container">
      <div class="row visible-md visible-lg">
        <div class="col-md-6">
          <div class="top-left-sap">
            <a href="#">Cloud Enablement for SAP Customers</a>
          </div>
        </div>
        <div class="col-md-6">
          <ul class="top-right-nav">
            <%--<li><a href="company.html">Company</a></li>--%>
            <%--  <li style="color:white;">Call Us Today! 1-888-533-3113</li>
            <li><a href="Support.html"><img src="img/phone-s.png" />Support</a></li>--%>
            <li><a href="contact.aspx"><img src="img/emailicon.png" />Contact</a></li>
              <li runat="server" id="licart" visible="false">
             <a href="/Customer/CloudPackages.aspx?userid=<%=UserMembershipID %>&showview=ShowMyCart"><img src="/img/shopcart.jpg" /><strong> My Cart</strong></a>
        </li> 
              <li><a id="hrefloggedInLink" runat="server" href="/Login.aspx" data-toggle="modal">
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
   <div class="primary-navbar hidden-xs hidden-sm">
    <div class="container">
      <div class="row">
        <div class="col-md-12">
          <nav class="navbar">
            <div class="container">
              <div class="navbar-header">
                <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#main-navbar"
                        aria-expanded="false" aria-controls="navbar">
                  <span class="sr-only">Toggle navigation</span>
                  <span class="icon-bar"></span>
                  <span class="icon-bar"></span>
                  <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand logo" href="Home.aspx"> <img src="asset/images/WFTCloud-Logo.jpg" width="218"
                                                                     height="98" alt=""> </a>
              </div>
              <div id="main-navbar" class="navbar-collapse collapse">
                <ul class="nav navbar-nav navbar-right">
                  <li class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown">SAP APPLICATIONS</a>
                    <ul class="dropdown-menu">
                      <li><a href="basissupport.html">SAP BASIS SUPPORT</a></li>
                      <li><a href="sapbasismonitoring.html">SAP BASIS MONITORING</a></li>
                      <li><a href="saptechnicalproject.html">SAP TECHNICAL PROJECT</a></li>
                      <li><a href="sapsecuritysolutions.html">SAP SECURITY SOLUTIONS</a></li>
                      <li><a href="sapaudit.html">SAP AUDITS AND COMPLIANCE</a></li>
                      <li><a href="sapcloudsolutions.html">SAP CLOUD SOLUTIONS</a></li>
                      <li><a href="saphostingoptions.html">SAP HOSTING OPTIONS</a></li>
                      <li><a href="saphighavailability.html">SAP HIGH AVAILABILITY</a></li>
                      <li><a href="saphighavailability.html">SAP DISASTER RECOVERY</a></li>
                    </ul></li>
                  <li class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown">SAP INFRASTRUCTURE</a>
                    <ul class="dropdown-menu">
                      <li><a href="azuremanagedservices.html">AZURE MANAGED SERVICES</a></li>
                      <li><a href="awsmanagedservices.html">AWS MANAGED SERVICES</a></li>
                      <li><a href="disasterrecoveryservices.html">DISASTER RECOVERY AS SERVICES</a></li>
                      <li><a href="mergerandaquisitionitservices.html">MERGER & ACQUISION IT SERVICES</a></li>
                      <li><a href="itdivestureservices.html">IT DIVESTITURE AS A SERVICE</a></li>
                      <li><a href="managedserverhosting.html">MANAGED SERVER HOSTING</a></li>
                    </ul>
                  </li>
                  <li class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown">CONSULTING</a>
                    <ul class="dropdown-menu">
                      <li><a href="cloudtransformation.html">SAP CLOUD TRANSFORMATION</a></li>
                      <li><a href="hanamigration.html">S4 HANA MIGRATION</a></li>
                      <li><a href="itoutsourcing.html">IT OUTSOURCING</a></li>
                      <li><a href="disasterrecovery.html">DISASTER RECOVERY</a></li>
                    </ul>
                  </li>
                  <li><a href="ExpressCloud.aspx">SAP EXPRESS CLOUD</a></li>
                  <li class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown">RESOURCES</a>
                    <ul class="dropdown-menu">
                      <li><a href="partners.html">PARTNERS</a></li>
                      <li><a href="press-release-content.html">PRESS RELEASE</a></li>
                      <li><a href="http://blog.wftcloud.com/">BLOG</a></li>
                      <li><a href="download-SAP-cloud-computing-brochures.html">BROCHURE</a></li>
                      <li><a href="videos.html">VIDEOS</a></li>
                    </ul>
                  </li>
                  <li class="dropdown"><a href="about.html" class="dropdown-toggle" data-toggle="dropdown">ABOUT US</a>
                    <ul class="dropdown-menu">
                      <li><a href="about.html">COMPANY</a></li>
                      <li><a href="company.html">MANAGEMENT TEAM</a></li>
                      <li><a href="testimonials.html">TESTIMONIALS</a></li>
                      <li><a href="contact.aspx">CONTACT US</a></li>
                      <li><a href="faqs.html">FAQ</a></li>
                    </ul>
                  </li>
                </ul>
              </div><!--/.nav-collapse -->
            </div>
          </nav>
        </div>
      </div>
    </div>
  </div>
    <!-- tablet and mobile navigation -->
  <div class="container-fluid hidden-md hidden-lg mob-tab-menu">
    <div class="col-xs-10 p-l-xxs">
      <a class="" href="index.html">
        <img src="asset/images/WFTCloud-Logo.jpg" height="80px" alt="">
      </a>
    </div>
    <div class="col-xs-2 text-center">
      <div class="dropdown pull-right p-t-md">
        <a href="#" class="btn btn-default btn-lg dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false"><i class="fa fa-bars"></i></a>
        <ul class="dropdown-menu" aria-labelledby="drop3">
          <li><a href="#">SAP APPLICATIONS</a></li>
          <li role="separator" class="divider"></li>
          <li><a href="#">SAP INFRASTRUCTURE</a></li>
          <li role="separator" class="divider"></li>
          <li><a href="#">CONSULTING</a></li>
          <li role="separator" class="divider"></li>
          <li><a href="#">RESOURCES</a></li>
          <li role="separator" class="divider"></li>
          <li><a href="#">ABOUT US</a></li>
        </ul>
      </div>
    </div>
  </div>
</header>
<div class="clearfix"></div>
<!-- Banner -->
<section class="main-banner">
  <div class="container hidden-xs">
    <div class="row">
      <div class="col-md-12">
        <div id="carousel-example-generic" class="carousel slide" data-ride="carousel">
          <!-- Indicators -->
          <ol class="carousel-indicators">
            <li data-target="#carousel-example-generic" data-slide-to="0" class="active"></li>
            <li data-target="#carousel-example-generic" data-slide-to="1"></li>
            <li data-target="#carousel-example-generic" data-slide-to="2"></li>
          </ol>

          <!-- Wrapper for slides -->
          <div class="carousel-inner" role="listbox">
            <div class="item active">
              <img src="asset/images/banner-01.jpg">
              <div class="carousel-caption">
                 <h1 class="caption-lg">We ARE the SAP Managed Cloud</h1>
                <p class="caption-md">Harnessing the power of the cloud for customers worldwide</p>
                <p class="caption-sm" style="font-size:large;"><a href="solution-01.html"><i class="fa fa fa-chevron-right"></i> <b style="color:#f3894c;font-size:large;">Learn More</b></a> about Managed
                  Cloud Services</p>
              </div>
            </div>
            <div class="item">
              <img src="asset/images/home-banner-02.jpg">
              <div class="carousel-caption">
                 <h1 class="caption-lg">DRaaS</h1>
                <p class="caption-md">Higher availability. Advanced security. Maximum results.</p>
                <p class="caption-sm" style="font-size:large;"><a href="solution-02.html"><i class="fa fa fa-chevron-right"></i> <b style="color:#f3894c;font-size:large;">Learn More</b></a> about DRaaS
                  service</p>
              </div>
            </div>
            <div class="item">
              <img src="asset/images/home-banner-03.jpg">
              <div class="carousel-caption">
               <h1 class="caption-lg">SAP Express Cloud</h1>
                <p class="caption-md">We ARE Trust Exceeding your highest expectations</p>
                <p class="caption-sm" style="font-size:large;"><a href="ExpressCloud.aspx"><i class="fa fa fa-chevron-right"></i> <b style="color:#f3894c;font-size:large;">Learn More</b></a> about SAP Express
                   Cloud</p>
              </div>
            </div>
          </div>

          <!-- Controls -->
          <a href="#carousel-example-generic" class="left carousel-control" data-slide="prev">
            <span class="icon-prev">
                <i class="fa fa-angle-left fa-stack-1x"></i>
            </span>
          </a>
          <a href="#carousel-example-generic" class="right carousel-control" data-slide="next">
            <span class="icon-next">
                <i class="fa fa-angle-right fa-stack-1x"></i>
            </span>
          </a>
        </div>
      </div>
    </div>
  </div>
</section>
<section class="service-wrap">
  <div class="container">
    <div class="row row-pad-m row-eq-height hidden-sm hidden-xs">
      <div class="col-md-2 col-pad-xs">
        <div class="service-box sap-cloud">
          <a href="solution-01.html"><h3>SAP Managed <br>Cloud</h3></a>
          <hr class="divider">
          <p>Fully Managed enterprise class SAP cloud solution provider, specializing in transforming enterprise IT to
            the cloud: Private, Public or Hybrid.</p>
          <a href="solution-01.html" class="btn btn-sap-cloud learn-more"><i class="fa fa-arrow-circle-right"></i> Learn
            More</a>
        </div>
      </div>
      <div class="col-md-2 col-pad-xs">
        <div class="service-box sap-draas">
          <a href="solution-02.html"><h3>SAP <br> DRaaS</h3></a>
          <hr class="divider">
          <p>Our Resilient Disaster Recovery As A Service for SAP in cloud offers flexible options on both physical &
            virtual servers to address complete range of RTO and RPO objectives.</p>
          <a href="solution-02.html" class="btn sap-draas learn-more"><i class="fa fa-arrow-circle-right"></i> Learn
            More</a>
        </div>
      </div>
      <div class="col-md-2 col-pad-xs">
        <div class="service-box audit-compliance">
          <a href="solution-03.html"><h3>Regulatory Compliance & Legacy Systems
          </h3></a>
          <hr class="divider">
          <p>Retain retired ERP systems for regulatory & compliance purpose, post merger and acquisition (M&A) or
            divestiture. Supporting major platforms (AIX, HPUX, SOLARIS & INTEL) based Landscape.</p>
          <a href="solution-03.html" class="btn btn-audit learn-more"><i class="fa fa-arrow-circle-right"></i> Learn
            More</a>
        </div>
      </div>
      <div class="col-md-2 col-pad-xs">
        <div class="service-box s4-hana">
          <a href="solution-04.html"><h3>SAP HANA <br>Solutions</h3></a>
          <hr class="divider">
          <p>Our solutions include HANA Automation, Migration Planning, Sizing, Infrastructure Design, Installation &
            Performance Optimization, TDI Validation and HANA Managed Services.</p>
          <a href="solution-04.html" class="btn s4-hana learn-more"><i class="fa fa-arrow-circle-right"></i> Learn More</a>
        </div>
      </div>
      <div class="col-md-2 col-pad-xs">
        <div class="service-box express-sap">
          <a href="ExpressCloud.aspx"><h3>Express SAP <br>Cloud</h3></a>
          <hr class="divider">
          <p>Cost effective SAP Access in Cloud for Non-Productive use cases such as Development, Training and Sandbox
            landscapes.  <br /><b style="color:brown;">For more information call 1-888-533-3113</b></p>
          <a href="ExpressCloud.aspx" class="btn btn-express learn-more"><i class="fa fa-arrow-circle-right"></i>
            Learn More</a>
        </div>
      </div>
    </div>
    <div class="row row-pad-m hidden-lg hidden-md">
      <div class="col-md-2 col-pad-xs">
        <div class="service-box sap-cloud">
          <a href="solution-01.html"><h3>SAP Managed <br>Cloud</h3></a>
          <hr class="divider">
          <p>Fully Managed enterprise class SAP cloud solution provider, specializing in transforming enterprise IT to
            the cloud: Private, Public or Hybrid.</p>
          <a href="solution-01.html" class="btn btn-sap-cloud learn-more"><i class="fa fa-arrow-circle-right"></i> Learn
            More</a>
        </div>
      </div>
      <div class="col-md-2 col-pad-xs">
        <div class="service-box sap-draas">
          <a href="solution-02.html"><h3>SAP <br> DRaaS</h3></a>
          <hr class="divider">
          <p>Our Resilient Disaster Recovery As A Service for SAP in cloud offers flexible options on both physical &
            virtual servers to address complete range of RTO and RPO objectives.</p>
          <a href="solution-02.html" class="btn sap-draas learn-more"><i class="fa fa-arrow-circle-right"></i> Learn
            More</a>
        </div>
      </div>
      <div class="col-md-2 col-pad-xs">
        <div class="service-box audit-compliance">
          <a href="solution-03.html"><h3>Regulatory Compliance & Legacy Systems
          </h3></a>
          <hr class="divider">
          <p>Retain retired ERP systems for regulatory & compliance purpose, post merger and acquisition (M&A) or
            divestiture. Supporting major platforms (AIX, HPUX, SOLARIS & INTEL) based Landscape.</p>
          <a href="solution-03.html" class="btn btn-audit learn-more"><i class="fa fa-arrow-circle-right"></i> Learn
            More</a>
        </div>
      </div>
      <div class="col-md-2 col-pad-xs">
        <div class="service-box s4-hana">
          <a href="solution-04.html"><h3>SAP HANA <br>Solutions</h3></a>
          <hr class="divider">
          <p>Our solutions include HANA Automation, Migration Planning, Sizing, Infrastructure Design, Installation &
            Performance Optimization, TDI Validation and HANA Managed Services.</p>
          <a href="solution-04.html" class="btn s4-hana learn-more"><i class="fa fa-arrow-circle-right"></i> Learn More</a>
        </div>
      </div>
      <div class="col-md-2 col-pad-xs">
        <div class="service-box express-sap">
          <a href="express-sap-cloud.html"><h3>Express SAP <br>Cloud</h3></a>
          <hr class="divider">
          <p>Cost effective SAP Access in Cloud for Non-Productive use cases such as Development, Training and Sandbox
            landscapes.</p>
          <a href="express-sap-cloud.html" class="btn btn-express learn-more"><i class="fa fa-arrow-circle-right"></i>
            Learn More</a>
        </div>
      </div>
    </div>
  </div>
</section>
<!-- /services -->
<section class="moderate-blue sap-support">
  <div class="inner-container">
    <div class="text-center">
      <h3>Have questions? we can help with answers.</h3>
    </div>
    <div class="row">
      <div class="col-md-5">
        <div class="tel-number">
          <a href="tel:732-319-2691">Call 732-319-2691</a>
        </div>
      </div>
      <div class="col-md-1">
        <p>or</p>
      </div>
      <div class="col-md-6">
        <a href="requestquote.aspx" class="btn btn-danger btn-block btn-lg" type="button"> Request Quote</a>
      </div>
    </div>
  </div>
</section>
<!-- /call -->
<section class="content-wrap">
  <div class="container">
    <div class="row">
      <div class="col-md-6">
        <div class="content-left">
          <h3>SAP Cloud transformation</h3>
          <hr class="divider">
          <figure class="text-center">
            <img class="img-responsive" src="asset/images/sap-cloud-enablement.png" alt="">
          </figure>
          <figcaption>
            <p>Transform your business and unlock new value for your enterprise by migrating SAP to Cloud. WFTCloud's vendor agnostic approach to cloud helps you to make the most of your investment in your SAP landscape's migration to cloud.<br /><br /></p>
          </figcaption>
        </div>

      </div>
      <div class="col-md-6">
        <div class="content-right">
          <h3>HANA Infrastructure solutions</h3>
          <hr class="divider">
          <figure class="text-center">
            <img class="img-responsive" src="asset/images/sap-hub-and-spoke.png" alt="">
          </figure>
          <figcaption>
            <p>SAP HANA in-memory computing technology is a highly innovative approach for real-time access to business critical information. Combined with optimized infrastructure and comprehensive services from WFT, SAP HANA enables instant data access resulting in smarter business decisions and faster response to changing market conditions.</p>
          </figcaption>
        </div>
      </div>
    </div>
  </div>
</section>
<!-- /content-wrap -->
<section class="customers-wrap">
  <div class="container">
    <div class="row">
      <div class="col-md-6">
        <div class="cloud-customers">
          <h2>Our Customers</h2>
          <hr class="divider">
          <p class="cloud-customer-content">We strive to establish long-term collaborative partnerships with our
            customers and become their trusted partner. Such partnerships allow us to build dedicated teams, to create
            and maintain client-specific knowledge, and to seek overall process improvements and efficiency gains over
            time.</p>
          <div class="marquee">
            <ul>
              <li>
                <img src="asset/images/home-customers/allergan.jpg">
                <img src="asset/images/home-customers/amgen.jpg">
                <img src="asset/images/home-customers/asc.jpg">
                <img src="asset/images/home-customers/brother.jpg">
                <img src="asset/images/home-customers/callaway.jpg">
                <img src="asset/images/home-customers/camel.jpg">
                <img src="asset/images/home-customers/cemex.jpg">
                <img src="asset/images/home-customers/cp.jpg">
                <img src="asset/images/home-customers/crestron.jpg">
                <img src="asset/images/home-customers/dte.jpg">
                <img src="asset/images/home-customers/grainger.jpg">
                <img src="asset/images/home-customers/gsk.jpg">
                <img src="asset/images/home-customers/honeywell.jpg">
              </li>
              <li>
                <img src="asset/images/home-customers/johnson.jpg">
                <img src="asset/images/home-customers/nike.jpg">
                <img src="asset/images/home-customers/nova.jpg">
                <img src="asset/images/home-customers/rockwell.jpg">
                <img src="asset/images/home-customers/saudimoney.jpg">
                <img src="asset/images/home-customers/transalta.jpg">
                <img src="asset/images/home-customers/un.jpg">
                <img src="asset/images/home-customers/walmart.jpg">
                <img src="asset/images/home-customers/worldbank.jpg">
                <img src="asset/images/home-customers/wyeth.jpg">
              </li>
            </ul>
          </div>

        </div>
      </div>
      <div class="col-md-6">
        <div class="cloud-customers" style="padding: 0 15px;">
          <h2>Case Studies</h2>
          <hr class="divider">
          <div class="media">
            <div class="media-left">
              <a href="#">
                <img class="media-object" height="60" src="asset/images/cs-01-train.jpg"
                     alt="Largest transportation company based in Canada">
              </a>
            </div>
            <div class="media-body">
              <h3 class="media-heading">Largest transportation company based in Canada</h3>
              <p>Successful SAP Production Landscape Data Migration from legacy infrastructure to Private Cloud, in
                Record Time.</p>
            </div>
          </div>
          <hr>
          <div class="media">
            <div class="media-left">
              <a href="#">
                <img class="media-object" height="60" src="asset/images/cs-02-windmills.jpg"
                     alt="Electric Power Generation Company in Western Canada">
              </a>
            </div>
            <div class="media-body">
              <h3 class="media-heading">#1 Electric Power Generation Company in Western Canada</h3>
              <p>SAP Landscape migration to Private Cloud</p>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</section>
<!-- /customer wrap -->
<section class="testimonial grey-wrap global-sap">
  <div class="container">
    <div class="row">
      <div class="col-md-12">
        <div class="text-center">
          <h2 class="m-t-xxs">What our Customer's say</h2>
        </div>
      </div>
    </div>
    <hr class="divider">
    <div class="row">
      <div class="col-md-6">
        <div class="testimonial-block">
          <p>Our experience with WFT is exceptional. WFT has helped us realize a vastly improved Return on Investment
            with our Vblock and HANA deployments. They have demonstrated commitment to professionalism and bring a
            breadth of technical knowledge and skills. I would definitely recommend WFT as a partner in any project or
            operations relating to Converged Infrastructure, SAN, Backup, Disaster Recovery, and High Availability.</p>
          <h3>Michael Moore,</h3>
          <h4>IT Director, <br>TransAlta Corp.</h4>
        </div>
      </div>
      <div class="col-md-6">
        <div class="testimonial-block">
          <p>WFT is a solution-based team of people dedicated to solving problems creatively and cost-efficiently. For
            over seven years WFT has provided strategic direction to ensure that our technology has aligned with our
            business objectives, and acted as a true technology partner. They are responsive and knowledge, and we have
            great peace of mind with WFT there for us whenever we need them.</p>
          <h3>Benjamin Fausto,</h3>
          <h4>Director of Business Projects, SAP Operations and Application Development, <br>Crestron Electronics, Inc.
          </h4>
        </div>
      </div>
    </div>
  </div>
</section>
<!-- testimonial -->
<section class="partners global-sap">
  <div class="container">
    <div class="row">
      <div class="col-md-10 col-md-offset-1">
        <div class="text-center">
          <h2>Our Partners</h2>
          <hr class="divider">
          <!--<ul class="customers-brand">
            <li> <img src="asset/images/partners/img-01.jpg" alt=""></li>
            <li> <img src="asset/images/partners/img-02.jpg" alt=""></li>
            <li> <img src="asset/images/partners/img-03.jpg" alt=""></li>
            <li> <img src="asset/images/partners/img-04.jpg" alt=""></li>
            <li> <img src="asset/images/partners/img-05.jpg" alt=""></li>
            <li> <img src="asset/images/partners/img-06.jpg" alt=""></li>
            <li> <img src="asset/images/partners/img-07.jpg" alt=""></li>
            <li> <img src="asset/images/partners/img-08.jpg" alt=""></li>
            <li> <img src="asset/images/partners/img-09.jpg" alt=""></li>
            <li> <img src="asset/images/partners/img-10.jpg" alt=""></li>
            <li> <img src="asset/images/partners/img-11.jpg" alt=""></li>
          </ul>-->
          <img class="img-responsive m-t-md" src="asset/images/partner-logos.png" alt="Partners">
        </div>
      </div>
    </div>
  </div>
</section>
<!-- footer -->
<footer class="moderate-blue global-sap">
  <div class="container">
    <div class="row p-b-sm bor-b-grey">
      <div class="col-md-3">
        <div class="list-with-heading text-center">
          <!-- <h4>Certifications</h4> -->

        </div>
      </div>
      <div class="col-md-6">
        <div class="list-with-heading">
          <h4>Certifications</h4>
          <div class="row">
            <div class="col-md-4">
              <img src="asset/images/SAPcloud.jpg" class="img-responsive" alt="">
            </div>
            <div class="col-md-4">
              <img src="asset/images/SAPHosting.jpg" class="img-responsive" alt="">
            </div>
            <div class="col-md-4">
              <img src="asset/images/SAPInfrastructure.png" class="img-responsive" alt="">
            </div>
          </div>
          <h4>Verified & Secured by</h4>
           <div class="row">
            <div class="col-md-4">
              <img src="https://www.paypalobjects.com/webstatic/en_IN/mktg/logos/pp_cc_mark_37x23.jpg" class="img-responsive" alt="">
            </div>
            <div class="col-md-4">
              <img src="https://verify.authorize.net/anetseal/images/secure90x72.gif" class="img-responsive" alt="">
            </div>
            <div class="col-md-4">
              <span id="siteseal">
                <script type="text/javascript"
                        src="https://seal.godaddy.com/getSeal?sealID=V5TQsXzyyUZb5UGNilQkjMFnJoP89ydXeVA0DzJ9xvyDi7Da3eNuU2kfIoyX"></script>
              </span>
            </div>
          </div>
        </div>
      </div>
      <div class="col-md-3">
        <div class="list-with-heading">
          <h4>Support Center</h4>
          <ul>
            <li><a href="mailto:support@wftcloud.com"><i class="fa fa-envelope text-white m-r-sm"></i>support@wftcloud.com</a></li>
            <li><a href="requestquote.aspx"><i class="fa fa-file-o text-white m-r-sm"></i>Request a Quote</a></li>
            <li><a href="contact-us.html"><i class="fa fa-phone text-white m-r-sm"></i>Contact us</a></li>
          </ul>
        </div>
      </div>
    </div>
    <div class="row bor-b-grey">
      <div class="col-md-12">
        <div class="list-with-heading">
         <ul class="horz-list text-center">
             <li><a href="private-cloud-on-premise.html">Private Cloud - on Premise</a></li>
               <li><a href="SAP-HANA-Services.html">SAP HANA</a></li>
               <li><a href="SAP-remote-basis-Support.html">SAP Remote Basis Support</a></li>
               <li><a href="disaster-recovery-service.html">SAP Disaster Recovery</a></li>
               <li><a href="SAP-infra-services.html">SAP Infrastructure Services</a></li>
               <li><a href="SAP-migration.html">SAP Migration</a></li>
               <li><a href="SAPHANAONCLOUD.html">SAP Data Protection Services</a></li>
               <li><a href="SAP-High-Availablity.html">SAP High Availability</a></li>
            <li><a href="SAPHANAONCLOUD.html">HANA on Cloud</a></li>
            <li><a href="SAPHANATDI.html">SAP TDI</a></li>
            <li><a href="SAPLVM.html">SAP LVM</a></li>
            <li><a href="SAP-on-Cloud.html">SAP Cloud</a></li>
            <li><a href="cloud-SAP-ERP-solutions-n-services.html">Cloud SAP ERP</a></li>
            <li><a href="affordable-sap-system-for-small-business.html">SAP Systems for Small Business</a></li>
            <li><a href="SAP-cloud-computing-solutions.html">SAP Cloud Computing Solutions</a></li>
            <li><a href="ERP-solutions-on-cloud.html">ERP Solutions on Cloud</a></li>
            <li><a href="faqs.html">FAQ</a></li>
            <li><a href="cloud-computing.html">Cloud Computing</a></li>
            <li><a href="how-it-works.html">How it Works?</a></li>
            <li><a href="saas.html">SaaS</a></li>
            <li><a href="SAP-Hana-on-cloud.html">SAP HANA Cloud</a></li>
            <li><a href="SAP-Hana-One.html">SAP HANA One</a></li>
            <li><a href="System-Requirements.html">System Requirements</a></li>
            <li><a href="terms.html">Terms of use</a></li>
          </ul>
        </div>
           <div class="list-with-heading">
         <ul class="horz-list text-center">
            <li><a href="solution-01.html">SAP Managed Cloud</a></li>
            <li><a href="solution-02.html">SAP DRaaS</a></li>
            <li><a href="solution-03.html">SAP Legacy Data for Audit</a></li>
            <li><a href="solution-04.html">SAP HANA Solutions</a></li>
            <li><a href="ExpressCloud.aspx">Express SAP Cloud</a></li>
          </ul>
        </div>
          <div class="list-with-heading">
         <ul class="horz-list text-center">
            <li><a href="about.html">About Us</a></li>
            <li><a href="customers.html">Customers</a></li>
            <li><a href="partners.html">Partners</a></li>
            <li><a href="press-release-content.html">Press Release</a></li>
            <li><a href="http://blog.wftcloud.com/" target="_blank">Blog</a></li>
             <li><a href="download-SAP-cloud-computing-brochures.html">Brochure</a></li>
             <li><a href="videos.html">Videos</a></li>
             <li><a href="company.html">Management Team</a></li>
             <li><a href="testimonials.html">Testimonial</a></li>
             <li><a href="faqs.html">FAQ</a></li>
          </ul>
        </div>
      </div>
    </div>
    <div class="row bor-b-grey">
      <div class="col-md-6">
        <div class="list-with-heading" style="margin-bottom: 0;">
          <div class="row">
            <h4 class="col-md-4 text-right">Signup for Newsletter</h4>
            <div class="col-md-6">
              <input type="email" class="form-control m-b-sm" id="rq-name" placeholder="Enter your email ID">
            </div>
            <button type="submit" class="btn btn-primary col-md-2">Submit</button>
          </div>
        </div>
      </div>
      <div class="col-md-6">
        <div class="list-with-heading">
          <div class="row">
            <h4 class="col-md-4 text-right">Connect with us</h4>
            <ul class="social-media col-md-6">
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
      </div>
    </div>
    <div class="row">
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
<script src="https://www.callus.io/embed/1453/1477.js" async></script>
<!-- <script src="asset/js/bootstrap-hover-dropdown.min.js"></script> -->
<script>
    $(function () {
        $("#main-navbar").find(".dropdown").hover(
          function () {
              $('.dropdown-menu', this).stop(true, true).fadeIn("fast");
              $(this).toggleClass('open');
              $('b', this).toggleClass("caret caret-up");
          },
          function () {
              $('.dropdown-menu', this).stop(true, true).fadeOut("fast");
              $(this).toggleClass('open');
              $('b', this).toggleClass("caret caret-up");
          });
    });
</script>
        </form>
</body>
</html>

<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="requestquote.aspx.cs" Inherits="WFTCloud.requestquote" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <title>WFT Cloud - Request Quote</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="description" content="">
    <meta name="author" content="">
    <meta name="keywords" content="">
    <!-- base styles -->
    <link href="../css/Gridstyle.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="asset/css/home.css">
    <link rel="stylesheet" href="asset/css/styles-custom.css">
    <link rel="stylesheet" href="asset/css/styles.css">
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
            <li><a href="company.html">Company</a></li>
             <li><a href="Support.html"><img src="img/phone-s.png" />Support</a></li>
            <li><a href="contact.aspx"><img src="img/emailicon.png" />Contact</a></li>
           <li><a href="Login.aspx"><img src="/img/log-in-s.jpg" />Member Login</a></li>
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
                <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
                  <span class="sr-only">Toggle navigation</span>
                  <span class="icon-bar"></span>
                  <span class="icon-bar"></span>
                  <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand logo" href="Home.aspx"> <img src="asset/images/WFTCloud-Logo.jpg" width="218" height="98" alt=""> </a>
              </div>
              <div id="navbar" class="navbar-collapse collapse">
                <ul class="nav navbar-nav navbar-right">
                  <li><a href="solutions.html">solutions</a></li>
                  <li><a href="customers.html">customers</a></li>
                  <li><a href="expresssapcloud.aspx">sap express cloud</a></li>
                  <li><a href="partners.html">partners</a></li>
                  <li><a href="about.html">about us</a></li>
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
          <h2>Request Quote</h2>
            <div id="divRegisterSuccess" runat="server" visible="false" class="alert alert-success ">
                                                            <i class="icon-remove"></i>
                                                            <asp:Label ID="lblRegisterSuccess" Text="Admin has been notified about your request, our sales Manager will contact you earlier." runat="server"></asp:Label>
                                                            <span></span>
                                                        </div>
          <h3 class="text-center">Fill in the form below or call 732-319-2691</h3>
          <p class="text-center">After you fill in this form, our sales Manager will contact you to discuss your requirements and our Solutions.</p>
        </div>
      </div>
    </div>
  </div>
</section>
        <section class="requestquote">
  <div class="container">
    <div class="row">
      <form class="col-md-8 col-md-offset-2" >
        <div class="row">
          <div class="col-md-3" style="padding-right: 0;margin-left:150px;">
          <asp:TextBox ID="txtName" runat="server" CssClass="form-control input-lg m-b-sm" Width="280px" AutoCompleteType="Disabled" autocomplete="off" placeholder="Full Name"></asp:TextBox>
           <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtName" ValidationGroup="Login" Display="None" ErrorMessage="Name required" ForeColor="Red"></asp:RequiredFieldValidator>
           <asp:ValidatorCalloutExtender runat="Server" ID="vcouserName" TargetControlID="rfvUserName" Width="180px" CssClass="customCalloutStyle" PopupPosition="left" WarningIconImageUrl="~/img/warning-icon.png"  />
          <asp:TextBox ID="txtContactNumber" runat="server" CssClass="form-control input-lg m-b-sm" Width="280px" placeholder="Phone"></asp:TextBox>
           <asp:RequiredFieldValidator ID="rfvContactNumber" runat="server" ControlToValidate="txtContactNumber" ValidationGroup="Login" Display="None" ErrorMessage="Contact no required" ForeColor="Red"></asp:RequiredFieldValidator>
           <asp:ValidatorCalloutExtender runat="Server" ID="vceContact" TargetControlID="rfvContactNumber" Width="180px" CssClass="customCalloutStyle" PopupPosition="left" WarningIconImageUrl="~/img/warning-icon.png"  />
          <asp:TextBox ID="txtEmailID" runat="server" CssClass="form-control input-lg m-b-sm" AutoCompleteType="Disabled" Width="280px" autocomplete="off" placeholder="Email"></asp:TextBox>
           <asp:RequiredFieldValidator ID="rfvEmailID" runat="server" ValidationGroup="Login" ControlToValidate="txtEmailID" Display="None" ErrorMessage="Email required" ForeColor="Red"></asp:RequiredFieldValidator>
           <asp:RegularExpressionValidator ID="revEmailID" runat="server" ValidationGroup="Login" ControlToValidate="txtEmailID" Display="None" ErrorMessage="Type valid email address" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
           <asp:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender10" TargetControlID="rfvEmailID" Width="150px" CssClass="customCalloutStyle" PopupPosition="Left" WarningIconImageUrl="~/img/warning-icon.png"  />
           <asp:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender11" TargetControlID="revEmailID" Width="210px" CssClass="customCalloutStyle" PopupPosition="Left" WarningIconImageUrl="~/img/warning-icon.png"  />
          <asp:TextBox ID="txtCompany" runat="server" CssClass="form-control input-lg m-b-sm" Width="280px" placeholder="Company"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvCompany" runat="server" ControlToValidate="txtCompany" ValidationGroup="Login" Display="None" ErrorMessage="Company required" ForeColor="Red"></asp:RequiredFieldValidator>
           <asp:ValidatorCalloutExtender runat="Server" ID="vceCompany" TargetControlID="rfvCompany" Width="180px" CssClass="customCalloutStyle" PopupPosition="left" WarningIconImageUrl="~/img/warning-icon.png"  />    
              
              <%--<asp:TextBox ID="txtAddress" runat="server" CssClass="form-control input-lg m-b-sm" Width="280px" TextMode="MultiLine" placeholder="Address"></asp:TextBox>--%>
               <asp:TextBox ID="txtCity" runat="server" CssClass="form-control input-lg m-b-sm" placeholder="City"></asp:TextBox>
               <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="txtCity" ValidationGroup="Login" Display="None" ErrorMessage="City required" ForeColor="Red"></asp:RequiredFieldValidator>
           <asp:ValidatorCalloutExtender runat="Server" ID="vcecity" TargetControlID="rfvCity" Width="180px" CssClass="customCalloutStyle" PopupPosition="left" WarningIconImageUrl="~/img/warning-icon.png"  />
               <asp:TextBox ID="txtState" runat="server" CssClass="form-control input-lg m-b-sm" placeholder="State"></asp:TextBox>
               <asp:RequiredFieldValidator ID="rfvState" runat="server" ControlToValidate="txtState" ValidationGroup="Login" Display="None" ErrorMessage="State required" ForeColor="Red"></asp:RequiredFieldValidator>
           <asp:ValidatorCalloutExtender runat="Server" ID="vceState" TargetControlID="rfvState" Width="180px" CssClass="customCalloutStyle" PopupPosition="left" WarningIconImageUrl="~/img/warning-icon.png"  />
               <asp:TextBox ID="txtCountry" runat="server" CssClass="form-control input-lg m-b-sm" placeholder="Country"></asp:TextBox>
               <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ControlToValidate="txtCountry" ValidationGroup="Login" Display="None" ErrorMessage="Country required" ForeColor="Red"></asp:RequiredFieldValidator>
           <asp:ValidatorCalloutExtender runat="Server" ID="vcerCountry" TargetControlID="rfvCountry" Width="180px" CssClass="customCalloutStyle" PopupPosition="left" WarningIconImageUrl="~/img/warning-icon.png"  />
          </div>
          <div class="col-md-7">
                <asp:TextBox ID="txtDecscription" runat="server" CssClass="form-control input-lg" TextMode="MultiLine" Height="380px" Width="500px" placeholder="Requirement Description:"></asp:TextBox>
          </div>
         
        </div>
        <div class="row m-b-lg">
          <div class="col-md-5" style="padding-right: 0;margin-left:400px;">
              <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="280px" CssClass="btn btn-info btn-lg btn-block"  ValidationGroup="Login" OnClick="btnRegisterCode_Click"  />
              
          </div>
             
        </div>

      </form>
    </div>
  </div> <!-- end container-->
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
        <script src="asset/js/jquery.shuffle.min.js"></script>
        <script src="asset/js/shuffleProject.js"></script>
        <!-- <script src="asset/js/bootstrap-hover-dropdown.min.js"></script> -->
    </form>
</body>
</html>

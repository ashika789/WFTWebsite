<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WFTCloud.Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<link href="../css/Gridstyle.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" href="asset/css/home.css">
<link rel="stylesheet" href="asset/css/styles-custom.css">
<link rel="stylesheet" href="asset/css/styles.css">
    <!-- <link href='http://fonts.googleapis.com/css?family=Merriweather:400,700,900' rel='stylesheet' type='text/css'> -->
     <link rel="shortcut icon" href="asset/images/favicon.ico">
   <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css">
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
                  <li><a href="expresssapcloud.aspx">SAP IDES</a></li>
                  <li class="active"><a href="Login.aspx">My Account</a></li>
                  <li><a href="faqs.html">FAQ</a></li>
                  <li><a href="terms.html">Terms</a></li>
                  <li><a href="http://blog.wftcloud.com/" target="_blank">Blog</a></li>
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

        <section class="main-content">
  <div class="container" style="padding-top: 100px;">
    <div class="col-md-4 col-md-offset-4 login-panel">
      <div class="row m-t-xs bg-c-01">
        <h3 class="text-center text-c-01"> Login</h3>
      </div>
        <div style="margin-left:50px;">
      <div class="row m-t-sm">
        <form class="col-md-10 col-md-offset-1">
          <div class="form-group">

              <asp:Panel ID="pnlLogin" runat="server" DefaultButton="btnLogin">
                 
                <label for="l-user">Username</label>
           <asp:TextBox ID="txtEmail" runat="server" class="form-control" Width="280px" placeholder="User Name" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
          <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtEmail" ValidationGroup="Login" Display="None" ErrorMessage="User name required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                 <asp:ValidatorCalloutExtender runat="Server" ID="vcouserName" TargetControlID="rfvUserName" Width="180px" CssClass="customCalloutStyle" PopupPosition="Right" WarningIconImageUrl="~/img/warning-icon.png"  />
          </div><br />
          <div class="form-group">
           <label for="l-pass">Password</label>
               <asp:TextBox ID="txtPassword" runat="server" type="password" Width="280px" class="form-control" placeholder="Password" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                               <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ValidationGroup="Login" Display="None" ErrorMessage="Password required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                 <asp:ValidatorCalloutExtender runat="Server" ID="vcoPassword" TargetControlID="rfvPassword" Width="180px" CssClass="customCalloutStyle" PopupPosition="Right" WarningIconImageUrl="~/img/warning-icon.png"  />
          </div>
            
         
            <asp:Button ID="btnLogin" runat="server" Text="Login" Width="280px" CssClass="btn btn-primary" OnClick="btnLogin_Click" ValidationGroup="Login"  />
             </asp:Panel> 
            <div id="divEmailSuccess" runat="server" visible="false" class="alert alert-success">
                                                    <i class="icon-remove"></i>
                                                    <asp:Label ID="lblEmailSuccess" Text="Email Send Successfully" runat="server"></asp:Label>
                                                </div>
                                                <div id="divEmailError" runat="server" visible="false" class="alert alert-error ">
                                                    <i class="icon-remove"></i>
                                                    <asp:Label ID="lblEmailError" Text="" runat="server" ForeColor="Red"></asp:Label>
                                                </div>

             <asp:Panel ID="pnlForgotPassword" runat="server" DefaultButton="btnSendPassword" Visible ="false">
                            <div class="span3"></div>
                            <div class="span6">
                                <div>
                                    <h5><a href="/Login.aspx?ShowView=Login">&lt;&lt;</a>&nbsp;Retrieve Password</h5>
                                </div>
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblEmail" runat="server" Text="User Name"></asp:Label>&nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtForgotEmail" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvForgotEmail" ControlToValidate="txtForgotEmail" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="Forgot"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="btnSendPassword" runat="server" Text="Send Mail" ValidationGroup="Forgot" CssClass="btn btn-primary" OnClick="btnSendPassword_Click"  />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div id="divForgotSuccess" runat="server" visible="false" class="alert alert-success ">
                                                <asp:Label ID="lblForgotSuccess" Text="Email Sent Successfully" runat="server"></asp:Label>
                                            </div>
                                            <div id="divForgotError" runat="server" visible="false" class="alert alert-error ">
                                                <i class="icon-remove"></i>
                                                <asp:Label ID="lblForgotError" Text="" runat="server" ForeColor="Red"></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="span3"></div>
                        </asp:Panel>
        </form>
      </div>
            </div>
      <div class="row m-t-md m-b-md text-center bg-c-01">
       
          <a class="btn btn-link text-c-01" href="/Login.aspx?ShowView=ForgotPassword">Forgot your Password?</a>
          <a class="btn btn-link text-c-01" href="/Register.aspx">Sign up</a>
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
          <h2>Our Customers</h2>
        </div>
       </div>
    </div>
    <hr class="divider">
    <div class="row p-a-md" style="background-color: #fff">
      <div class="marquee">
        <ul>
          <li>
            <img src="asset/images/customers/ADP.jpg">
            <img src="asset/images/customers/AMB.jpg">
            <img src="asset/images/customers/American-Standard.jpg">
            <img src="asset/images/customers/Amgen.jpg">
            <img src="asset/images/customers/Applied-Materials.jpg">
            <img src="asset/images/customers/AstraZeneca.jpg">
            <img src="asset/images/customers/barnes-and-noble.jpg">
            <img src="asset/images/customers/Basf.jpg">
            <img src="asset/images/customers/Bbraun.jpg">
            <img src="asset/images/customers/Bentley.jpg">
            <img src="asset/images/customers/Black-Decker.jpg">
            <img src="asset/images/customers/Bloomberg.jpg">

            <img src="asset/images/customers/blue cross-blue shield.jpg">
            <img src="asset/images/customers/BOEHRINGER_INGELHEIM.jpg">
            <img src="asset/images/customers/Braun.jpg">
            <img src="asset/images/customers/BRISTOL_MYERS_SQUIBB.jpg">
            <img src="asset/images/customers/Brother.jpg">
            <img src="asset/images/customers/cable_and_wireless.jpg">
            <img src="asset/images/customers/CALLAWAY.jpg">
            <img src="asset/images/customers/Canadian Pacific.jpg">
            <img src="asset/images/customers/cdc.jpg">
            <img src="asset/images/customers/cemex.jpg">
            <img src="asset/images/customers/CENTER_FOR_DISCEASE_CONTROL.jpg">
            <img src="asset/images/customers/Checkpoint.jpg">
            <img src="asset/images/customers/Clorox.jpg">
            <img src="asset/images/customers/Columbia.jpg">

            <img src="asset/images/customers/Commerce-Bank.jpg">
            <img src="asset/images/customers/Commercial-Metals.jpg">
            <img src="asset/images/customers/Computer-Horizons.jpg">
            <img src="asset/images/customers/CSC.jpg">
            <img src="asset/images/customers/DC-Superior.jpg">
            <img src="asset/images/customers/Disney.jpg">
            <img src="asset/images/customers/Equate.jpg">
            <img src="asset/images/customers/ERP-Logic.jpg">
            <img src="asset/images/customers/Everis.jpg">
            <img src="asset/images/customers/FIAT.jpg">
            <img src="asset/images/customers/Florida.jpg">
            <img src="asset/images/customers/Forest-Lab.jpg">
            <img src="asset/images/customers/General-Atomics.jpg">
            <img src="asset/images/customers/Grainger.jpg">
            <img src="asset/images/customers/GSA.jpg">
            <img src="asset/images/customers/GSK-Glaxo.jpg">
            <img src="asset/images/customers/HSBC.jpg">
            <img src="asset/images/customers/Intel.jpg">

            <img src="asset/images/customers/JAG.jpg">
            <img src="asset/images/customers/Jet-Blue.jpg">
            <img src="asset/images/customers/Johnson.jpg">
            <img src="asset/images/customers/JPmorgan.jpg">
            <img src="asset/images/customers/Kenneth-Cole.jpg">
            <img src="asset/images/customers/Loreal.jpg">
            <img src="asset/images/customers/Marriott.jpg">
            <img src="asset/images/customers/Mars-Incorporated.jpg">
            <img src="asset/images/customers/Merrill-Lynch.jpg">
            <img src="asset/images/customers/Milliken.jpg">
            <img src="asset/images/customers/Momentive.jpg">
            <img src="asset/images/customers/NASA.jpg">
            <img src="asset/images/customers/National-Semiconductor.jpg">
            <img src="asset/images/customers/National-Starch.jpg">
            <img src="asset/images/customers/Nestle.jpg">
            <img src="asset/images/customers/Neustar.jpg">
            <img src="asset/images/customers/Newyork-life.jpg">

            <img src="asset/images/customers/Nike.jpg">
            <img src="asset/images/customers/NorthRop.jpg">
            <img src="asset/images/customers/Pacificorp.jpg">
            <img src="asset/images/customers/Pemex.jpg">
            <img src="asset/images/customers/PepBoys.jpg">
            <img src="asset/images/customers/Pepsi.jpg">
            <img src="asset/images/customers/Petrobras.jpg">
            <img src="asset/images/customers/Pfizer.jpg">
            <img src="asset/images/customers/PSEG.jpg">
            <img src="asset/images/customers/PWC.jpg">
            <img src="asset/images/customers/Quadrant-4.jpg">
            <img src="asset/images/customers/Rockwell-Collins.jpg">
            <img src="asset/images/customers/ROHM-Haas.jpg">
            <img src="asset/images/customers/RUSS.jpg">
            <img src="asset/images/customers/Sanofi.jpg">
            <img src="asset/images/customers/Saudi-Aramco.jpg">
            <img src="asset/images/customers/Siemens.jpg">
            <img src="asset/images/customers/SMUD.jpg">

            <img src="asset/images/customers/Sony.jpg">
            <img src="asset/images/customers/Star-Media.jpg">
            <img src="asset/images/customers/Talk-America.jpg">
            <img src="asset/images/customers/Towers-Perrin.jpg">
            <img src="asset/images/customers/TransAlta.jpg">
            <img src="asset/images/customers/under-armour.jpg">
            <img src="asset/images/customers/Union-Carbide.jpg">
            <img src="asset/images/customers/United-Nations.jpg">
            <img src="asset/images/customers/UPS.jpg">
            <img src="asset/images/customers/US-Bancorp.jpg">
            <img src="asset/images/customers/US-Postal.jpg">
            <img src="asset/images/customers/Vastera.jpg">
            <img src="asset/images/customers/Verisig.jpg">
            <img src="asset/images/customers/West-Pharma.jpg">
            <img src="asset/images/customers/World-Bank.jpg">
            <img src="asset/images/customers/World-Kitchen.jpg">
            <img src="asset/images/customers/Wyeth.jpg">
          </li>
        </ul>
      </div>
    </div>
  </div>
</section>

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
              <a href="http://twitter.com/wftus" target="_blank">
                <span class="fa-stack fa-lg">
                  <i class="fa fa-square-o fa-stack-2x"></i>
                  <i class="fa fa-twitter fa-stack-1x"></i>
                </span>
              </a>
            </li>
            <li><a href="http://facebook.com/wftcloud" target="_blank">
              <span class="fa-stack fa-lg">
                  <i class="fa fa-square-o fa-stack-2x"></i>
                  <i class="fa fa-facebook fa-stack-1x"></i>
                </span>
            </a></li>
            <li><a href="https://plus.google.com/100277864576102202208/posts" target="_blank">
              <span class="fa-stack fa-lg">
                  <i class="fa fa-square-o fa-stack-2x"></i>
                  <i class="fa fa-google-plus fa-stack-1x"></i>
                </span>
            </a></li>
            <li><a href="http://www.linkedin.com/company/wft-cloud" target="_blank">
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

    </form>
</body>
</html>

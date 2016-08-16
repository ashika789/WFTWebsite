
<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="ExpressCloud.aspx.cs" Inherits="WFTCloud.ExpressCloud" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html lang="en">
<head>
   <meta charset="utf-8">
   <title>WFT Cloud - Pricing</title>
   <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no"/>

   <meta name="description" content="">
   <meta name="author" content="">
   <meta name="keywords" content="">
   <!-- base styles -->
    <link href="../css/Gridstyle.css" rel="stylesheet" type="text/css" />
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
                                <li><a href="expresssapcloud.aspx">SAP IDES</a></li>
                                <li class="active"><a href="ExpressCloud.aspx">PRICING</a></li>
                                  <li><a id="hrefloggedInLink" runat="server" href="Login.aspx">MY ACCOUNT</a></li>
                                <li><a href="faqs.html">FAQ</a></li>
                                <li><a href="terms.html">TERMS</a></li>
                                <li><a href="http://blog.wftcloud.com/" target="blank">BLOG</a></li>
                                <li><a href="contact.aspx">CONTACT US</a></li>
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
                <div>
                    <h2 class="bor-b-grey">Express SAP Cloud</h2>
                    <p>SAP servers are no more a cost intensive capital investment. WFT makes SAP servers affordable for even individual users and small enterprises. The pay as you use model lets you optimize your cost to levels where you could run any of your training, test and development experiments. </p>
                </div>
            </div>
        </div>

         
       
        
         
        <div class="row">
            <div class="col-md-3">
                <div class="panel panel-default pricing-panel" style="background-color: #edfb9c;background-color: #A4F3C6;box-shadow: 5px 5px 10px #b5b5b5;border: 3px solid #35998b;">
                    <div class="panel-heading">IDES Shared</div>
                    <div class="panel-body">
                        <span>$</span>
                        <span class="price">9.99</span>
                        <span>/MO</span>
                    </div>
                    
                    <a href="#pack1" class="btn btn-warning btn-block"><i class="fa fa-chevron-circle-right p-r-sm"></i> Sign Up</a>
                </div>
            </div>
            <div class="col-md-3">
                <div class="panel panel-default pricing-panel" style="background-color: #bfc4f6">
                    <div class="panel-heading">IDES Dedicated Client</div>
                    <div class="panel-body">
                        <span>$</span>
                        <span class="price">0.62</span>
                        <span>/HOUR</span>
                        <span class="fine-print">* Monthly 728 Hours</span>
                    </div>
                    
                    <a href="#pack2" class="btn btn-warning btn-block"><i class="fa fa-chevron-circle-right p-r-sm"></i> Sign Up</a>
                </div>
            </div>
            <div class="col-md-3">
                <div class="panel panel-default pricing-panel" style="background-color: #bfc4f6">
                    <div class="panel-heading">Dedicated Server</div>
                    <div class="panel-body">
                        <span>$</span>
                        <span class="price">0.62</span>
                        <span>/HOUR</span>
                        <span class="fine-print">* Monthly 728 Hours</span>
                    </div>
                   
                    <a href="#pack4" class="btn btn-warning btn-block"><i class="fa fa-chevron-circle-right p-r-sm"></i> Sign Up</a>
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
                   
                    <a href="#pack5" class="btn btn-warning btn-block"><i class="fa fa-chevron-circle-right p-r-sm"></i> Sign Up</a>
                </div>
            </div>
            
        </div>
          <div class="row">
            
           
            <div class="col-md-3">
                <div class="panel panel-default pricing-panel" style="background-color: #A2F4F0">
                    <div class="panel-heading">HANA Dedicated</div>
                    <div class="panel-body">
                        <span>$</span>
                        <span class="price">0.78</span>
                        <span>/HOUR</span>
                        <span class="fine-print">* Monthly 728 Hours</span>
                    </div>
                    
                    <a href="#pack6" class="btn btn-warning btn-block"><i class="fa fa-chevron-circle-right p-r-sm"></i> Sign Up</a>
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
                    
                    <a href="#pack7" class="btn btn-warning btn-block"><i class="fa fa-chevron-circle-right p-r-sm"></i> Sign Up</a>
                </div>
            </div>
            <div class="col-md-3">
                <div class="panel panel-default pricing-panel" style="background-color: #d8e87b">
                    <div class="panel-heading">Remote BASIS Support</div>
                    <div class="panel-body">
                        <span>$</span>
                        <span class="price">2.29</span>
                        <span>/HOUR</span>
                        <span class="fine-print">* Monthly 728 Hours</span>
                    </div>
                  
                    <a href="#pack8" class="btn btn-warning btn-block"><i class="fa fa-chevron-circle-right p-r-sm"></i> Sign Up</a>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div>
                    <h2 class="bor-b-grey">Subscription Models</h2>
                   
                </div>
            </div>
            <div class="col-md-4">
                <div class="panel panel-default pricing-panel" style="background-color: #E0E0E0">
                   <div class="panel-heading" style="text-align: left;"><span class="icon-30px"><img
            src="asset/images/pr-ico-01.png" alt=""></span>IDES Dedicated Client
          </div>
                    <ul class="list-group">
                        <li class="list-group-item" style="text-align:justify;">SAP IDES Access [Dedicated Client] means that you will have a subscription to a client in an SAP system that is not shared with anybody. Other customers cannot modify master data or customizing that you created. It is also possible to create and modify ABAP programs.</li>
                    </ul>
                </div>
            </div>
            <div class="col-md-4">
                <div class="panel panel-default pricing-panel" style="background-color: #E0E0E0">
                    <div class="panel-heading" style="text-align: left;"><span class="icon-30px"><img
            src="asset/images/pr-ico-02.png" alt=""></span>Shared
          </div>
                    <ul class="list-group">
                        <li class="list-group-item" style="text-align:justify;">SAP IDES Access [Shared] means that you will have a subscription to a client in an SAP system that is shared among multiple customers. Other customers might occasionally modify master data or customizing that you created but usually it doesn't happen.</li>
                    </ul>
                </div>
            </div>
            <div class="col-md-4">
                <div class="panel panel-default pricing-panel" style="background-color: #E0E0E0">
                    <div class="panel-heading" style="text-align: left;"><span class="icon-30px"><img
            src="asset/images/pr-ico-03.png" alt=""></span>Dedicated Server
          </div>
                    <ul class="list-group">
                        <li class="list-group-item" style="text-align:justify;">SAP IDES Access [Dedicated Server] means that you will have a subscription to a server with an SAP system. Only you will have access to this SAP server. It is possible run any SAP Basis transactions, create new user accounts and do ABAP development with this subscription.</li>
                    </ul>
                </div>
            </div>
        </div>
      
        
      <!-- Pack 1 -->
      <div class="row">
            <div class="col-md-12">
                <div>
                    <h2 style="position:relative;"><span id="pack1" style="position:absolute; top:-120px;"></span>IDES Shared</h2>
                    <hr class="divider" style="margin: 10px 0;">
                    <p>SAP IDES Access [Shared] means that you will have a subscription to a client in an SAP system that is shared among multiple customers. Other customers might occasionally modify master data or customizing that you created but usually it doesn't happen. </p>
                </div>
            </div>
            <div class="clearfix">
                <div class="col-md-3">
                    <div class="panel panel-default pricing-panel" style="background-color: #BADEFF">
                        <div class="panel-heading"><asp:Label ID="lblSAPERP" runat ="server" Text="SAP ERP EHP7"></asp:Label><span class="price-v">7.0 EHP2</span></div>
                        <ul class="list-group text-center">
                             <li class="list-group-item" style="font-size:larger;">$7.99/month</li>
                            <li class="list-group-item">SAP ECC IDES-shared access is provided on the purchase of this service. The user is provided with a single user login for access to SAP IDES system.
<br /><br /></li>
                        </ul>
                        <a href='/ExpressCloud.aspx?AddToCart=177' class="btn btn-primary btn-block"><i class="fa fa-shopping-cart p-r-sm"></i> Add to Cart</a>
                        
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="panel panel-default pricing-panel" style="background-color: #BADEFF">
                        <div class="panel-heading">SAP BW<span class="price-v">6.0/7.0</span></div>
                        <ul class="list-group text-center">
                            <li class="list-group-item" style="font-size:larger;">$9.99/month</li>
                            <li class="list-group-item">SAP SEM-BW IDES-shared access is provided on the purchase of this service. The user is provided with a single user login for access to SAP IDES system.</li>
                        </ul>
                        <a href="/ExpressCloud.aspx?AddToCart=177" class="btn btn-primary btn-block"><i class="fa fa-shopping-cart p-r-sm"></i> Add to Cart</a>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="panel panel-default pricing-panel" style="background-color: #BADEFF">
                        <div class="panel-heading">SAP CRM<span class="price-v">7.0 EHP2</span></div>
                        <ul class="list-group text-center">
                            <li class="list-group-item" style="font-size:larger;">$9.99/month</li>
                            <li class="list-group-item">SAP CRM IDES-Shared 7.0 EHp2 access is provided on the purchase of this service. The user is provided with a single user login for access to SAP IDES system.</li>
                        </ul>
                        <a href="/ExpressCloud.aspx?AddToCart=177" class="btn btn-primary btn-block"><i class="fa fa-shopping-cart p-r-sm"></i> Add to Cart</a>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="panel panel-default pricing-panel" style="background-color: #BADEFF">
                        <div class="panel-heading">SAP SRM<span class="price-v">5.0</span></div>
                        <ul class="list-group text-center">
                            <li class="list-group-item" style="font-size:larger;">$9.99/month </li>
                            <li class="list-group-item">SAP SRM IDES-Shared 5.0 access is provided on the purchase of this service. The user is provided with a single user login for access to SAP IDES system.</li>
                        </ul>
                        <a href="/ExpressCloud.aspx?AddToCart=177" class="btn btn-primary btn-block"><i class="fa fa-shopping-cart p-r-sm"></i> Add to Cart</a>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="panel panel-default pricing-panel" style="background-color: #BADEFF">
                        <div class="panel-heading">SAP IS-Retail<span class="price-v">6.0</span></div>
                        <ul class="list-group text-center">
                            <li class="list-group-item" style="font-size:larger;">$9.99/month </li>
                            <li class="list-group-item">SAP IS-Retail IDES-Shared 6.0 access is provided on the purchase of this service. The user is provided with a single user login for access to SAP IDES system.</li>
                        </ul>
                        <a href="/ExpressCloud.aspx?AddToCart=177" class="btn btn-primary btn-block"><i class="fa fa-shopping-cart p-r-sm"></i> Add to Cart</a>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="panel panel-default pricing-panel" style="background-color: #BADEFF">
                        <div class="panel-heading">SAP AFS<span class="price-v">6.0</span></div>
                        <ul class="list-group text-center">
                            <li class="list-group-item" style="font-size:larger;">$7.99/month </li>
                            <li class="list-group-item">SAP Apparel and Footwear (AFS) IDES-Shared 6.0 access is provided on the purchase of this service. The user is provided with a single user login for access to SAP IDES system.</li>
                        </ul>
                        <a href="/ExpressCloud.aspx?AddToCart=177" class="btn btn-primary btn-block"><i class="fa fa-shopping-cart p-r-sm"></i> Add to Cart</a>
                    </div>
                </div>
            </div>
        </div>
      <!-- Pack 2 -->
      <div class="row">
            <div class="col-md-12">
                <div>
                    <h2 style="position:relative;"><span id="pack2" style="position:absolute; top:-120px;"></span>IDES Dedicated Cient</h2>
                    <hr class="divider" style="margin: 10px 0;">
                    <p>SAP IDES Access [Dedicated Client] means that you will have a subscription to a client in an SAP system that is not shared with anybody. Other customers cannot modify master data or customizing that you created. It is also possible to create and modify ABAP programs.</p>
                <p>The user is provided with five (5) user login IDs and one (1) developer key for access to the SAP IDES-Dedicated system. Additional developer keys can be subscribed separately.</p>
                </div>
            </div>
            <div class="clearfix">
                <div class="col-md-3">
                    <div class="panel panel-default pricing-panel" style="background-color: #BADEFF">
                        <div class="panel-heading">SAP IS Retail<span class="price-v">6.0</span></div>
                        <ul class="list-group text-center">
                            <li class="list-group-item">$150.00/month</li>
                            <li class="list-group-item">SAP IS Retail IDES-dedicated access is provided on the purchase of this service. </li>
                        </ul>
                        <a href="/ExpressCloud.aspx?AddToCart=177" class="btn btn-primary btn-block"><i class="fa fa-shopping-cart p-r-sm"></i> Add to Cart</a>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="panel panel-default pricing-panel" style="background-color: #BADEFF">
                        <div class="panel-heading">SAP AFS<span class="price-v">6.0</span></div>
                        <ul class="list-group text-center">
                            <li class="list-group-item">$150.00/month</li>
                            <li class="list-group-item">SAP AFS IDES-dedicated access is provided on the purchase of this service. </li>
                        </ul>
                        <a href="/ExpressCloud.aspx?AddToCart=177" class="btn btn-primary btn-block"><i class="fa fa-shopping-cart p-r-sm"></i> Add to Cart</a>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="panel panel-default pricing-panel" style="background-color: #BADEFF">
                        <div class="panel-heading">SAP ERP<span class="price-v">6.0 EHP6</span></div>
                        <ul class="list-group text-center">
                            <li class="list-group-item">$150.00/month</li>
                            <li class="list-group-item">SAP ERP IDES-dedicated access is provided on the purchase of this service. </li>
                        </ul>
                        <a href="/ExpressCloud.aspx?AddToCart=177" class="btn btn-primary btn-block"><i class="fa fa-shopping-cart p-r-sm"></i> Add to Cart</a>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="panel panel-default pricing-panel" style="background-color: #BADEFF">
                        <div class="panel-heading">SAP CRM<span class="price-v">7.0 EHP2</span></div>
                        <ul class="list-group text-center">
                            <li class="list-group-item">$150.00/month</li>
                            <li class="list-group-item">SAP CRM IDES-dedicated access is provided on the purchase of this service. </li>
                        </ul>
                        <a href="/ExpressCloud.aspx?AddToCart=177" class="btn btn-primary btn-block"><i class="fa fa-shopping-cart p-r-sm"></i> Add to Cart</a>
                    </div>
                </div>
            </div>
        </div>
      <!-- Pack 3 -->
   
      <!-- Pack 4 -->
      <div class="row">
            <div class="col-md-12">
                <div>
                    <h2 style="position:relative;"><span id="pack4" style="position:absolute; top:-120px;"></span>Dedicated Server</h2>
                    <hr class="divider" style="margin: 10px 0;">
                    <p>SAP IDES Access [Dedicated Server] means that you will have a subscription to a server with an SAP system. Only you will have access to this SAP server. It is possible run any SAP Basis transactions, create new user accounts and do ABAP development with this subscription. </p>
                <p>This system can be scaled up to ten concurrent SAP users. The dedicated SAP system comes with an initial 90-day temporary license. Customer is responsible for their own SAP License for dedicated SAP systems.</p>
                </div>
            </div>
            <div class="clearfix">
                <div class="col-md-3">
                    <div class="panel panel-default pricing-panel" style="background-color: #BADEFF">
                        <div class="panel-heading">SAP ERP<span class="price-v">6.0 EHP6</span></div>
                        <ul class="list-group text-center">
                            <li class="list-group-item">$425.00/month</li>
                            <li class="list-group-item">SAP ERP Dedicated Server access is provided on a fixed-cost model. A minimum three-month subscription is required for dedicated servers. </li>
                        </ul>
                        <a href="/ExpressCloud.aspx?AddToCart=177" class="btn btn-primary btn-block"><i class="fa fa-shopping-cart p-r-sm"></i> Add to Cart</a>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="panel panel-default pricing-panel" style="background-color: #BADEFF">
                        <div class="panel-heading">SAP BW<span class="price-v">NW 7.4</span></div>
                        <ul class="list-group text-center">
                            <li class="list-group-item">$425.00/month</li>
                            <li class="list-group-item">SAP BW Dedicated Server access is provided on a fixed-cost model. A minimum three-month subscription is required for dedicated servers. </li>
                        </ul>
                        <a href="/ExpressCloud.aspx?AddToCart=177" class="btn btn-primary btn-block"><i class="fa fa-shopping-cart p-r-sm"></i> Add to Cart</a>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="panel panel-default pricing-panel" style="background-color: #BADEFF">
                        <div class="panel-heading">SAP CRM<span class="price-v">7.0 EHP4</span></div>
                        <ul class="list-group text-center">
                            <li class="list-group-item">$425.00/month</li>
                            <li class="list-group-item">SAP CRM Dedicated Server access is provided on a fixed-cost model. A minimum three-month subscription is required for dedicated servers. </li>
                        </ul>
                        <a href="/ExpressCloud.aspx?AddToCart=177" class="btn btn-primary btn-block"><i class="fa fa-shopping-cart p-r-sm"></i> Add to Cart</a>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="panel panel-default pricing-panel" style="background-color: #BADEFF">
                        <div class="panel-heading">SAP IS-Retail<span class="price-v">6.0</span></div>
                        <ul class="list-group text-center">
                            <li class="list-group-item">$425.00/month</li>
                            <li class="list-group-item">SAP IS-Retail Dedicated Server access is provided on a fixed-cost model. A minimum three-month subscription is required for dedicated servers. </li>
                        </ul>
                        <a href="/ExpressCloud.aspx?AddToCart=177" class="btn btn-primary btn-block"><i class="fa fa-shopping-cart p-r-sm"></i> Add to Cart</a>
                    </div>
                </div>
              <div class="col-md-3">
                <div class="panel panel-default pricing-panel" style="background-color: #BADEFF">
                  <div class="panel-heading">SAP AFS<span class="price-v">6.0</span></div>
                  <ul class="list-group text-center">
                    <li class="list-group-item">$425.00/month</li>
                    <li class="list-group-item">SAP AFS(Apparel/Footwear) Dedicated Server access is provided on a fixed-cost model. A minimum three-month subscription is required for dedicated servers. </li>
                  </ul>
                  <a href="#" class="btn btn-primary btn-block"><i class="fa fa-shopping-cart p-r-sm"></i> Add to Cart</a>
                </div>
              </div>
              <div class="col-md-3">
                <div class="panel panel-default pricing-panel" style="background-color: #BADEFF">
                  <div class="panel-heading">SAP ERP<span class="price-v">ECC6 EHP8</span></div>
                  <ul class="list-group text-center">
                    <li class="list-group-item">$425.00/month</li>
                    <li class="list-group-item">SAP ERP Dedicated Server access is provided on a fixed-cost model. A minimum three-month subscription is required for dedicated servers. </li>
                  </ul>
                  <a href="/ExpressCloud.aspx?AddToCart=177" class="btn btn-primary btn-block"><i class="fa fa-shopping-cart p-r-sm"></i> Add to Cart</a>
                </div>
              </div>
              <div class="col-md-3">
                <div class="panel panel-default pricing-panel" style="background-color: #BADEFF">
                  <div class="panel-heading">SAP SCM<span class="price-v">7.0 EHP4</span></div>
                  <ul class="list-group text-center">
                    <li class="list-group-item">$425.00/month</li>
                    <li class="list-group-item">SAP Supply Chain Management Dedicated Server access is provided on a fixed-cost model. A minimum three-month subscription is required for dedicated servers. </li>
                  </ul>
                  <a href="/ExpressCloud.aspx?AddToCart=177" class="btn btn-primary btn-block"><i class="fa fa-shopping-cart p-r-sm"></i> Add to Cart</a>
                </div>
              </div>
              <div class="col-md-3">
                <div class="panel panel-default pricing-panel" style="background-color: #BADEFF">
                  <div class="panel-heading">SAP IS-Oil<span class="price-v">6.0</span></div>
                  <ul class="list-group text-center">
                    <li class="list-group-item">$425.00/month</li>
                    <li class="list-group-item">SAP IS-Oil Dedicated Server access is provided on a fixed-cost model. A minimum three-month subscription is required for dedicated servers. </li>
                  </ul>
                  <a href="/ExpressCloud.aspx?AddToCart=177" class="btn btn-primary btn-block"><i class="fa fa-shopping-cart p-r-sm"></i> Add to Cart</a>
                </div>
              </div>
              <div class="col-md-3">
                <div class="panel panel-default pricing-panel" style="background-color: #BADEFF">
                  <div class="panel-heading">SAP DIMP<span class="price-v">ERP 6.0 EhP5</span></div>
                  <ul class="list-group text-center">
                    <li class="list-group-item">$425.00/month</li>
                    <li class="list-group-item">SAP Discrete Ind & Mill Products Dedicated Server access is provided on a fixed-cost model. A minimum three-month subscription is required for dedicated servers. </li>
                  </ul>
                  <a href="/ExpressCloud.aspx?AddToCart=177" class="btn btn-primary btn-block"><i class="fa fa-shopping-cart p-r-sm"></i> Add to Cart</a>
                </div>
              </div>
              <div class="col-md-3">
                <div class="panel panel-default pricing-panel" style="background-color: #BADEFF">
                  <div class="panel-heading">SAP IS-CWM<span class="price-v">6.03</span></div>
                  <ul class="list-group text-center">
                    <li class="list-group-item">$425.00/month</li>
                    <li class="list-group-item">SAP Catch Weight Management Dedicated Server access is provided on a fixed-cost model. A minimum three-month subscription is required for dedicated servers. </li>
                  </ul>
                  <a href="/ExpressCloud.aspx?AddToCart=177" class="btn btn-primary btn-block"><i class="fa fa-shopping-cart p-r-sm"></i> Add to Cart</a>
                </div>
              </div>
              <div class="col-md-3">
                <div class="panel panel-default pricing-panel" style="background-color: #BADEFF">
                  <div class="panel-heading">SAP SRM<span class="price-v">7.0 EHP3</span></div>
                  <ul class="list-group text-center">
                    <li class="list-group-item">$425.00/month</li>
                    <li class="list-group-item">SAP Supplier Relationship Management Dedicated Server access is provided on a fixed-cost model. A minimum three-month subscription is required for dedicated servers. </li>
                  </ul>
                  <a href="/ExpressCloud.aspx?AddToCart=177" class="btn btn-primary btn-block"><i class="fa fa-shopping-cart p-r-sm"></i> Add to Cart</a>
                </div>
              </div>
              <div class="col-md-3">
                <div class="panel panel-default pricing-panel" style="background-color: #BADEFF">
                  <div class="panel-heading">SAP Solution Manager<span class="price-v">7.2</span></div>
                  <ul class="list-group text-center">
                    <li class="list-group-item">$425.00/month</li>
                    <li class="list-group-item">SAP Solution Manager Dedicated Server access is provided on a fixed-cost model. A minimum three-month subscription is required for dedicated servers. </li>
                  </ul>
                  <a href="/ExpressCloud.aspx?AddToCart=177" class="btn btn-primary btn-block"><i class="fa fa-shopping-cart p-r-sm"></i> Add to Cart</a>
                </div>
              </div>
              <div class="col-md-3">
                <div class="panel panel-default pricing-panel" style="background-color: #BADEFF">
                  <div class="panel-heading">SAP NW Gateway<span class="price-v">7.5</span></div>
                  <ul class="list-group text-center">
                    <li class="list-group-item">$425.00/month</li>
                    <li class="list-group-item">SAP NW Gateway Dedicated Server access is provided on a fixed-cost model. A minimum three-month subscription is required for dedicated servers. </li>
                  </ul>
                  <a href="/ExpressCloud.aspx?AddToCart=177" class="btn btn-primary btn-block"><i class="fa fa-shopping-cart p-r-sm"></i> Add to Cart</a>
                </div>
              </div>
              <div class="col-md-3">
                <div class="panel panel-default pricing-panel" style="background-color: #BADEFF">
                  <div class="panel-heading">SAP TM<span class="price-v">9.0</span></div>
                  <ul class="list-group text-center">
                    <li class="list-group-item">$425.00/month</li>
                    <li class="list-group-item">SAP Transportation Management Dedicated Server access is provided on a fixed-cost model. A minimum three-month subscription is required for dedicated servers. </li>
                  </ul>
                  <a href="/ExpressCloud.aspx?AddToCart=177" class="btn btn-primary btn-block"><i class="fa fa-shopping-cart p-r-sm"></i> Add to Cart</a>
                </div>
              </div>
              <div class="col-md-3">
                <div class="panel panel-default pricing-panel" style="background-color: #BADEFF">
                  <div class="panel-heading">SAP EM<span class="price-v">9.0</span></div>
                  <ul class="list-group text-center">
                    <li class="list-group-item">$425.00/month</li>
                    <li class="list-group-item">SAP Event Management Dedicated Server access is provided on a fixed-cost model. A minimum three-month subscription is required for dedicated servers. </li>
                  </ul>
                  <a href="/ExpressCloud.aspx?AddToCart=177" class="btn btn-primary btn-block"><i class="fa fa-shopping-cart p-r-sm"></i> Add to Cart</a>
                </div>
              </div>
              <div class="col-md-3">
                <div class="panel panel-default pricing-panel" style="background-color: #BADEFF">
                  <div class="panel-heading">SAP CE<span class="price-v">7.3</span></div>
                  <ul class="list-group text-center">
                    <li class="list-group-item">$425.00/month</li>
                    <li class="list-group-item">SAP NetWeaver Composition Environment Dedicated Server access is provided on a fixed-cost model. A minimum three-month subscription is required for dedicated servers. </li>
                  </ul>
                  <a href="/ExpressCloud.aspx?AddToCart=177" class="btn btn-primary btn-block"><i class="fa fa-shopping-cart p-r-sm"></i> Add to Cart</a>
                </div>
              </div>
              <div class="col-md-3">
                <div class="panel panel-default pricing-panel" style="background-color: #BADEFF">
                  <div class="panel-heading">SAP EP<span class="price-v">7.5</span></div>
                  <ul class="list-group text-center">
                    <li class="list-group-item">$425.00/month</li>
                    <li class="list-group-item">SAP Enterprise Portal Dedicated Server access is provided on a fixed-cost model. A minimum three-month subscription is required for dedicated servers. </li>
                  </ul>
                  <a href="/ExpressCloud.aspx?AddToCart=177" class="btn btn-primary btn-block"><i class="fa fa-shopping-cart p-r-sm"></i> Add to Cart</a>
                </div>
              </div>
              <div class="col-md-3">
                <div class="panel panel-default pricing-panel" style="background-color: #BADEFF">
                  <div class="panel-heading">SAP GRC<span class="price-v">10.0</span></div>
                  <ul class="list-group text-center">
                    <li class="list-group-item">$425.00/month</li>
                    <li class="list-group-item">SAP Governance, Risk, and Compliance (GRC)  Dedicated Server access is provided on a fixed-cost model. A minimum three-month subscription is required for dedicated servers. </li>
                  </ul>
                  <a href="/ExpressCloud.aspx?AddToCart=177" class="btn btn-primary btn-block"><i class="fa fa-shopping-cart p-r-sm"></i> Add to Cart</a>
                </div>
              </div>
              <div class="col-md-3">
                <div class="panel panel-default pricing-panel" style="background-color: #BADEFF">
                  <div class="panel-heading">SAP ERP<span class="price-v">ECC6 EHP7</span></div>
                  <ul class="list-group text-center">
                    <li class="list-group-item">$425.00/month</li>
                    <li class="list-group-item">SAP ERP IDES-dedicated access is provided on a fixed-cost model. A minimum three-month subscription is required for dedicated servers. </li>
                  </ul>
                  <a href="/ExpressCloud.aspx?AddToCart=177" class="btn btn-primary btn-block"><i class="fa fa-shopping-cart p-r-sm"></i> Add to Cart</a>
                </div>
              </div>
            </div>
        </div>
      <!-- Pack 5 -->
      <div class="row">
        <div class="col-md-12">
          <div>
              <h2 style="position:relative;"><span id="pack5" style="position:absolute; top:-120px;"></span>HANA Shared</h2>
            <hr class="divider" style="margin: 10px 0;">
            <p>On the purchase of this service, you will provided access to the SAP HANA Database through SAP HANA Studio, on a shared basis. Not all privileges will be made available for shared users. The SAP HANA DB license is strictly for non-commercial use only. </p>
          </div>
        </div>
        <div class="clearfix">
          <div class="col-md-3">
            <div class="panel panel-default pricing-panel" style="background-color: #BADEFF">
              <div class="panel-heading">HANA ONE<span class="price-v">Rev 68.0</span></div>
              <ul class="list-group text-center">
                <li class="list-group-item">Special Offer</li>
                <li class="list-group-item">$300.00/month</li>
                <li class="list-group-item">Get remote access to SAP HANA Database with SAP HANA Studio. This SAP HANA Database is shared between multiple customers and can be access via the Internet.</li>
              </ul>
              <a href="/ExpressCloud.aspx?AddToCart=177" class="btn btn-primary btn-block"><i class="fa fa-shopping-cart p-r-sm"></i> Add to Cart</a>
            </div>
          </div>
        </div>
      </div>
      <!-- Pack 6 -->
      <div class="row">
        <div class="col-md-12">
          <div>
              <h2 style="position:relative;"><span id="pack6" style="position:absolute; top:-120px;"></span>HANA Dedicated</h2>
            <hr class="divider" style="margin: 10px 0;">
            <p>Get remote access to SAP HANA Database with SAP HANA Studio. This SAP HANA Database will be set up exclusively for you and can be access via the Internet.Dedicated SAP HANA servers are perfect for situations when you need an isolated environment </p>
          </div>
        </div>
        <div class="clearfix">
          <div class="col-md-3">
            <div class="panel panel-default pricing-panel" style="background-color: #BADEFF">
              <div class="panel-heading">HANA ONE<span class="price-v">Rev 68.0</span></div>
              <ul class="list-group text-center">
                   <li class="list-group-item">$425.00/month</li>
                <li class="list-group-item">SAP HANA database comes with SAP HANA Studio that provide integrated development environment (IDE) for developing artifacts in SAP HANA server.</li>
                
              </ul>
              <a href="/ExpressCloud.aspx?AddToCart=177" class="btn btn-primary btn-block"><i class="fa fa-shopping-cart p-r-sm"></i> Add to Cart</a>
            </div>
          </div>
        </div>
      </div>
        <!-- Pack 7 -->
        <div class="row">
        <div class="col-md-12">
          <div>
              <h2 style="position:relative;"><span id="pack7" style="position:absolute; top:-120px;"></span>Shared Combo</h2>
            <hr class="divider" style="margin: 10px 0;">
            <p>Get remote access to SAP Business Warehouse (BW) system that is integrated with SAP Enterprise Resource Planning (ERP) system. Your subscription will include remote access to two SAP systems at a reduced price. The data from SAP ERP system can be uploaded to SAP BW system via an RFC connection. These IDES system are shared between multiple customers and can be accessed via the Internet.</p>
          </div>
        </div>
        <div class="clearfix">
          <div class="col-md-3">
            <div class="panel panel-default pricing-panel" style="background-color: #BADEFF">
              <div class="panel-heading">Shared Combo<span class="price-v">6.0 EHP 6 / 7.0</span></div>
              <ul class="list-group text-center">
                  <li class="list-group-item">SAP ERP+BW</li>
                <li class="list-group-item">29.99/month</li>
                <li class="list-group-item">These two systems are properly set up to work together. It means that the data from the SAP ECC system will be integrated to the SAP BW system. The subscription includes access to both systems.</li>
              </ul>
              <a href="/ExpressCloud.aspx?AddToCart=177" class="btn btn-primary btn-block"><i class="fa fa-shopping-cart p-r-sm"></i> Add to Cart</a>
            </div>
          </div>
        </div>
      </div>

        <!-- Pack 8 -->
          <div class="row">
        <div class="col-md-12">
          <div>
              <h2 style="position:relative;"><span id="pack8" style="position:absolute; top:-120px;"></span>SAP Remote BASIS Support</h2>
            <hr class="divider" style="margin: 10px 0;">
            <p>WFT’s SAP Remote BASIS support service is a comprehensive operational management offering that assists clients to improve the quality of overall SAP BASIS support operations while reducing the overall operational management cost.</p>
          </div>
        </div>
        <div class="clearfix">
          <div class="col-md-3">
            <div class="panel panel-default pricing-panel" style="background-color: #BADEFF">
              <div class="panel-heading">SAP BASIS Support<span class="price-v">All</span></div>
              <ul class="list-group text-center">
                  <li class="list-group-item">24x7 Availability</li>
                <li class="list-group-item">1717.50/month</li>
                <li class="list-group-item">Providing an enterprise class SAP Remote BASIS Support services for SAP solutions presents a tremendous opportunity for our clients.</li>
              </ul>
              <a href="/ExpressCloud.aspx?AddToCart=177" class="btn btn-primary btn-block"><i class="fa fa-shopping-cart p-r-sm"></i> Add to Cart</a>
            </div>
          </div>
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
            <li><a href="https://plus.google.com/10027786457610.02208/posts">
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

     <!--Start of Zopim Live Chat Script-->
<script type="text/javascript">
    window.$zopim || (function (d, s) {
        var z = $zopim = function (c) { z._.push(c) }, $ = z.s =
d.createElement(s), e = d.getElementsByTagName(s)[0]; z.set = function (o) {
    z.set.
_.push(o)
}; z._ = []; z.set._ = []; $.async = !0; $.setAttribute('charset', 'utf-8');
        $.src = '//cdn.zopim.com/?1CwXZ82hvcgjTOSSiwJqa7R8MstmnUZz'; z.t = +new Date; $.
type = 'text/javascript'; e.parentNode.insertBefore($, e)
    })(document, 'script');
</script>
<!--End of Zopim Live Chat Script-->


</body>
</html>

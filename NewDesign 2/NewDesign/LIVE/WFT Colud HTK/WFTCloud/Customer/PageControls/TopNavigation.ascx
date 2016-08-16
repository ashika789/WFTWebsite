<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopNavigation.ascx.cs" Inherits="WFTCloud.Customer.PageControls.TopNavigation" %>

<!--Include custom.css to customzie the top menu-->
  <link rel="stylesheet" href="../../assets/css/custom.css" />

			<%--<div class="navbar-inner">
				<div class="container-fluid">
					<a href="/ExpressCloud.aspx" class="xbrand">
						<small>
							<img src="/assets/img/logo-inner.png" style="height:50px;" alt="SAP Cloud Computings"/>
						</small>
					</a><!--/.brand-->
                   
					<ul class="nav ace-nav pull-right">
                        <li class="light-blue">
                        <asp:ImageButton ID="SupportButton" runat ="server" ImageUrl="~/img/SupportIcon.png" OnClick="SupportButton_Click" ToolTip="Click  Icon for Service Support"  />
                           
                             </li>
						<li class="light-blue">
                            <asp:LinkButton ID="lkbtnLogout" runat="server" ForeColor="Azure" OnClick="lkbtnLogout_Click">Logout</asp:LinkButton>
                        </li>
                    </ul>
                  
				</div><!--/.container-fluid-->
			</div><!--/.navbar-inner-->--%>

<!--New Customized Menu-->
<div class="navbar custom-navbar">
  <div class="navbar-inner">
    <div class="container">
      <a class="btn btn-navbar" data-toggle="collapse" data-target=".navbar-responsive-collapse">
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
      </a>
      <a class="brand" href="../../Home.aspx">
        <img src="../../img/WFTCloud-Logo-Small.png" width="150px" alt="">
      </a>
      <div class="nav-collapse collapse navbar-responsive-collapse">
        <ul class="nav pull-right">
          <li><a href="../../Home.aspx">Home</a></li>
          <li><a href="../../ExpressCloud.aspx">SAP IDES</a></li>
          <li><a href="../../faqs.html">FAQ</a></li>
          <li><a href="../../terms.html">TERMS</a></li>
            <li><a href="../../contact.aspx">CONTACT US</a></li>
            <li><asp:ImageButton ID="SupportButton" runat ="server" ImageUrl="~/img/SupportIcon.png" OnClick="SupportButton_Click" ToolTip="Click  Icon for Service Support"  /></li>
        <li><asp:LinkButton ID="lkbtnLogout" runat="server" ForeColor="#747a9a" OnClick="lkbtnLogout_Click">Logout</asp:LinkButton></li>
        </ul>
      </div><!-- /.nav-collapse -->
    </div>
  </div><!-- /navbar-inner -->
</div>


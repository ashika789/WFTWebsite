<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopNav.ascx.cs" Inherits="WFTCloud.Admin.PageControls.TopNav" %>
			<div class="navbar-inner">
				<div class="container-fluid">
					<a href="/index.aspx" class="xbrand">
						<small>
							<img src="/assets/img/logo-inner.png" style="height:50px;" alt="SAP Cloud Computings"/>
						</small>
					</a><!--/.brand-->

<%--					<ul class="nav ace-nav pull-right">

						<li class="light-blue">
							<a data-toggle="dropdown" href="#" class="dropdown-toggle">
								<span class="user-info">
									<small>Welcome,</small>
									<asp:Label runat="server" ID="lblUserName" Text="User Name"></asp:Label>
								</span>

								<i class="icon-caret-down"></i>
							</a>
						</li>
					</ul><!--/.ace-nav-->--%>

							<ul class="nav ace-nav pull-right">
								<li class="light-blue">
									<asp:LinkButton ID="lkbtnLogout" runat="server" OnClick="lkbtnLogout_Click">Logout</asp:LinkButton>
								</li>
							</ul>
				</div><!--/.container-fluid-->
			</div><!--/.navbar-inner-->
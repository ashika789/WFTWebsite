<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftNavigation.ascx.cs" Inherits="WFTCloud.Customer.PageControls.LeftNavigation" %>
<ul class="nav nav-list">
<%--	<li runat="server" id="liDashBoard" class="" >
		<a href="Dashboard.aspx">
			<i class="icon-dashboard"></i>
			<span class="menu-text"> Customers </span>
		</a>
	</li>--%>
 <%--   <li runat="server" id="liDashBoard" class="" style="background-color:black" >
		<asp:Label runat="server" ID="lblUserName" Text="User Name" ForeColor="White"></asp:Label>
			</li>--%>
    	<li runat="server" id="liuserProfiles" class="active" >
		<a href="#" class="dropdown-toggle">
			<i class="icon-user"></i>
			<span class="menu-text"> User Profiles</span>
			<b class="arrow icon-angle-down"></b>
		</a>

		<ul class="submenu">
			<li>
				<a href="/Customer/UserProfiles.aspx?userid=<%=UserMembershipID %>&showview=UserDetails">
					<i class="icon-double-angle-right"></i>
					User Details
				</a>
			</li>
			<li>
				<a href="/Customer/UserProfiles.aspx?userid=<%=UserMembershipID %>&showview=ChangePassword">
					<i class="icon-double-angle-right"></i>
					Change Password
				</a>
			</li>
			<li>
				<a href="/Customer/UserProfiles.aspx?userid=<%=UserMembershipID %>&showview=ChangePaymentDetails">
					<i class="icon-double-angle-right"></i>
					Change Payment details
				</a>
			</li>
<%--            <li>
				<a href="/Customer/UserProfiles.aspx?userid=<%=UserMembershipID %>&showview=CancelSubscription">
					<i class="icon-double-angle-right"></i>
					Cancel Subscription
				</a>
			</li>--%>
		</ul>
	</li>
	<li runat="server" id="liWftCloudPackages" class="active" >
		<a href="#" class="dropdown-toggle">
			<i class="icon-cloud"></i> 
			<span class="menu-text">WFT Cloud Packages</span>
            <b class="arrow icon-angle-down"></b>
		</a>
        <ul class="submenu">
			<li>
				<a href="/Customer/CloudPackages.aspx?userid=<%=UserMembershipID %>&showview=AvailableService">
					<i class="icon-double-angle-right"></i>
					Available Services
				</a>
			</li>
			<li>
				<a href="/Customer/CloudPackages.aspx?userid=<%=UserMembershipID %>&showview=SubscribedService&status=Active">
					<i class="icon-double-angle-right"></i>
					Subscribed Services
				</a>
			</li>
            <li>
				<a href="/Customer/CloudPackages.aspx?userid=<%=UserMembershipID %>&showview=ShowMyCart">
					<i class="icon-double-angle-right"></i>
					My Cart
				</a>
			</li>
            <li>
				<a href="/Customer/UserDashboard.aspx?userid=<%=UserMembershipID %>">
					<i class="icon-double-angle-right"></i>
					Dashboard
				</a>
			</li>
         </ul>
	</li>
    <li runat="server" id="liCRMRequest" >
        <a href="/Customer/CRMRequests.aspx?userid=<%=UserMembershipID %>">
			<i class="icon-file"></i> 
			<span class="menu-text">CRM Request</span>
		</a>
    </li>
    <li runat="server" id="liManageCRM" visible="false">
        <a href="/Customer/ManageCRMRequests.aspx?userid=<%=UserMembershipID %>">
			<i class="icon-file"></i> 
			<span class="menu-text">Manage CRM Request</span>
		</a>
    </li>
    <li runat="server" id="liUserOrderDetails" >
        <a href="/Customer/UserOrderDetails.aspx?userid=<%=UserMembershipID %>">
			<i class="icon-shopping-cart"></i> 
			<span class="menu-text">Order History</span>
		</a>
    </li>
    <li runat="server" id="li1" >
        <a href="/Customer/UserPaymentHistory.aspx?userid=<%=UserMembershipID %>">
			<i class="icon-shopping-cart"></i> 
			<span class="menu-text">Payment History</span>
		</a>
    </li>
	<li runat="server" id="liDownloads" >
		<a href="/Customer/Downloads.aspx?userid=<%=UserMembershipID %>">
			<i class="icon-download"></i>
			<span class="menu-text">Downloads</span></a>
    </li>
     <li>
		<a href="/User/CancelationPolicy.aspx" target="_blank">
			<i class="icon-file"></i>
			<span class="menu-text">Cancelation Policy</span>
           
		</a>
	</li>
    <li>
		<a href="/User/FAQ.aspx" target="_blank">
			<i class="icon-question-sign"></i>
			<span class="menu-text">FAQ</span>
           
		</a>
	</li>
    
</ul><!--/.nav-list-->

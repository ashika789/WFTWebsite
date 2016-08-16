<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftNav.ascx.cs" Inherits="WFTCloud.Admin.PageControls.LeftNav" %>
<ul class="nav nav-list">
     <%--<li runat="server" id="liDashBoard" class="" style="background-color:black" >
		<asp:Label runat="server" ID="lblUserName" Text="User Name" ForeColor="White"></asp:Label>
	</li>--%>
	<li runat="server" id="liUserManagement">
		<a href="#" class="dropdown-toggle">
			<i class="icon-user"></i>
			<span class="menu-text"> User Management </span>
            <b class="arrow icon-angle-down"></b>
		</a>

                        
		<ul class="submenu">
			<li>
				<a href="/Admin/UserList.aspx">
					<i class="icon-double-angle-right"></i>
					Users List
				</a>
			</li>
		</ul>
	</li>

	<li runat="server" id="liAdminList">
		<a href="#" class="dropdown-toggle">
			<i class="icon-group"></i>
			<span class="menu-text"> Admin Management </span>
			<b class="arrow icon-angle-down"></b>
		</a>

		<ul class="submenu">
			<li>
				<a href="/Admin/administratorslist.aspx">
					<i class="icon-double-angle-right"></i>
					Administrators List
				</a>
			</li>
            <li>
				<a href="/Admin/UserSubscriptionDetails.aspx">
					<i class="icon-double-angle-right"></i>
					User Subscription History
				</a>
			</li>
             <li>
				<a href="/Admin/UsersServiceHistory.aspx">
					<i class="icon-double-angle-right"></i>
					User Service History
				</a>
			</li>
             <li>
				<a href="/Admin/newslettermanagement.aspx">
					<i class="icon-double-angle-right"></i>
			NewsLetter Email
				</a>
			</li>
            <li>
				<a href="/Admin/admindashboard.aspx">
					<i class="icon-double-angle-right"></i>
			Admin Dashboard
				</a>
			</li>
		</ul>
	</li>
	<li runat="server" id="liServiceList">
		<a href="#" class="dropdown-toggle">
			<i class="icon-file"></i>
			<span class="menu-text"> Service List </span>
			<b class="arrow icon-angle-down"></b>
		</a>

		<ul class="submenu">
			<li>
				<a href="/Admin/CategoriesList.aspx">
					<i class="icon-double-angle-right"></i>
					Service Categories
				</a>
			</li>
			<li>
				<a href="/Admin/WFTServices.aspx">
					<i class="icon-double-angle-right"></i>
					WFT Services
				</a>
			</li>
            <li>
				<a href="/Admin/ServiceProvisioningDetails.aspx">
					<i class="icon-double-angle-right"></i>
					Service Provisioning Details
				</a>
			</li>
             <li>
				<a href="/Admin/CustomServiceProvisioningDetails.aspx">
					<i class="icon-double-angle-right"></i>
					Custom Service Provisioning Details
				</a>
			</li>
            <li>
				<a href="/Admin/CloudResourceManagement.aspx">
					<i class="icon-double-angle-right"></i>
					Cloud Resource Management
				</a>
			</li>
		</ul>
	</li>

<%--	<li>
		<a href="#" class="dropdown-toggle">
			<i class="icon-headphones"></i>
			<span class="menu-text"> User Services </span>
			<b class="arrow icon-angle-down"></b>
		</a>

		<ul class="submenu">
			<li>
				<a href="tables.html">
					<i class="icon-double-angle-right"></i>
					New Service Request
				</a>
			</li>
			<li>
				<a href="jqgrid.html">
					<i class="icon-double-angle-right"></i>
					Update User Service
				</a>
			</li>
			<li>
				<a href="jqgrid.html">
					<i class="icon-double-angle-right"></i>
					(De)Activate Services
				</a>
			</li>
		</ul>
	</li>
    <li>
		<a href="">
			<i class="icon-calendar"></i>
			<span class="menu-text"> Trigger Monthly Payment</span>
		</a>
	</li>
    <li>
		<a href="#" class="dropdown-toggle">
			<i class="icon-desktop"></i>
			<span class="menu-text"> Help Desk </span>
			<b class="arrow icon-angle-down"></b>
		</a>

		<ul class="submenu">
			<li>
				<a href="jqgrid.html">
					<i class="icon-double-angle-right"></i>
					Request List
				</a>
			</li>
			<li>
				<a href="tables.html">
					<i class="icon-double-angle-right"></i>
					New Request
				</a>
			</li>
		</ul>
	</li>
    <li>
		<a href="">
			<i class="icon-dollar"></i>
			<span class="menu-text"> Update Usage</span>
		</a>
	</li>
    <li>
		<a href="">
			<i class="icon-upload-alt"></i>
			<span class="menu-text"> Site Maintenance</span>
		</a>
	</li>--%>
	<li runat="server" id="liCoupon">
		<a href="#" class="dropdown-toggle">
			<i class="icon-shopping-cart"></i>
			<span class="menu-text"> Coupons </span>

			<b class="arrow icon-angle-down"></b>
		</a>
		<ul class="submenu">
			<li>
				<a href="/Admin/CouponManagement.aspx">
                    <i class="icon-list"></i>
					List Coupons
				</a>
			</li>
		</ul>
	</li>
	<li runat="server" id="liContentManagment">
		<a href="#" class="dropdown-toggle">
			<i class="icon-file-alt"></i>
			<span class="menu-text"> Content Management </span>

			<b class="arrow icon-angle-down"></b>
		</a>
		<ul class="submenu">
			<li>
				<a href="/Admin/ContentManagementScreen.aspx">
                    <i class="icon-list"></i>
					Manage Page Contents								
				</a>
			</li>
            <li>
				<a href="/Admin/ManagePressRelease.aspx">
                    <i class="icon-list"></i>
					Manage Press Release								
				</a>
			</li>
			<li>
				<a href="/Admin/ManageIndexData.aspx">
                    <i class="icon-list"></i>
					Manage Index Data								
				</a>
			</li>
			<li>
				<a href="/Admin/ManageFAQ.aspx">
					<i class="icon-list"></i>
					Manage FAQ 
				</a>
			</li>
			<li>
				<a href="/Admin/ManageBanners.aspx">
					<i class="icon-list"></i>
					Manage Banners
				</a>
			</li>
             <li>
                <a href="/Admin/ManageTestimonials.aspx">
                    <i class="icon-list"></i>
                    Manage Testimonials
                </a>
            </li>
		</ul>
	</li>
    <li runat="server" id="liGenerateReports">
		<a href="#" class="dropdown-toggle">
			<i class="icon-table"></i>
			<span class="menu-text"> Reports</span>
            <b class="arrow icon-angle-down"></b>
		</a>
        		<ul class="submenu">
			<li>
				<a href="/Admin/WFTReports.aspx">
                    <i class="icon-list"></i>
					 Generate Reports
				</a>
			</li>
		</ul>
	</li>
    <li runat="server" id="liCloudGenerateReports">
		<a href="#" class="dropdown-toggle">
			<i class="icon-table"></i>
			<span class="menu-text">Cloud Reports</span>
            <b class="arrow icon-angle-down"></b>
		</a>
        		<ul class="submenu">
			<li>
				<a href="/Admin/CloudReports.aspx">
                    <i class="icon-list"></i>
					 Generate Reports
				</a>
			</li>
		</ul>
	</li>
	<li runat="server" id="liFinancialAccounting">
		<a href="" class="dropdown-toggle">
			<i class="icon-suitcase"></i>
			<span class="menu-text"> Financial Accounting</span>
            <b class="arrow icon-angle-down"></b>
		</a>
		<ul class="submenu">
			<li>
				<a href="/Admin/ProcessedPayments.aspx">
                    <i class="icon-list"></i>
					Manage Refunds							
				</a>
			</li>
			<li>
				<a href="/Admin/AutomatedPayments.aspx">
					<i class="icon-list"></i>
					<%--Refund Details--%>
                    Automated Payments
				</a>
			</li>
            <li>
				<a href="/Admin/PaymentTransactionUpdate.aspx">
					<i class="icon-list"></i>
					<%--Refund Details--%>
                    Mannual Payments
				</a>
			</li>
		</ul>
	</li>
    <li id="liSettings" runat="server">
        <a href="#" class="dropdown-toggle">
			<i class="icon-cogs"></i>
			<span class="menu-text">Settings</span>
            <b class="arrow icon-angle-down"></b>
		</a>
        <ul class="submenu">
            <li>
                <a href="/Admin/Settings.aspx">
                    <i class="icon-double-angle-right"></i>
					 Settings						
				</a>
            </li>
        </ul>
    </li>
       <li id="liTrainningAndVisitors" runat="server">
		<a href="#" class="dropdown-toggle">
			<i class="icon-book"></i>
			<span class="menu-text">Training And Visitors</span>
            <b class="arrow icon-angle-down"></b>
		</a>
		<ul class="submenu">
			<li>
				<a href="/Admin/ManageTrainingDetails.aspx">
                    <i class="icon-double-angle-right"></i>
					Manage Training							
				</a>
			</li>
			<li>
				<a href="/Admin/ManageCourses.aspx">
					<i class="icon-double-angle-right"></i>
					Manage Courses
				</a>
			</li>
            <li>
                <a href="/Admin/ManageVisitors.aspx">
                    <i class="icon-double-angle-right"></i>
                    Manage Visitors
                </a>
            </li>            
		</ul>
	</li>
    <li runat="server" id="liManageCRMRequests">
                <a href="#"  class="dropdown-toggle">
                    <i class="icon-file"></i>
                    <span class="menu-text">Manage CRM</span>
                    <b class="arrow icon-angle-down"></b>
                </a>
        		<ul class="submenu">
			<li>
				<a href="/Admin/ManageCRMRequests.aspx">
                    <i class="icon-list"></i>
					 Manage CRM Requests
				</a>
			</li>
		</ul>
            </li>
    <li>
                <a>
                    <i class="icon-cloud"></i>
                    <span class="menu-text"> Amazon Web Services</span>
                </a>
    </li>
</ul><!--/.nav-list-->

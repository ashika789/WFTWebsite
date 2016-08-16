<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminPages.Master" AutoEventWireup="true" CodeBehind="UserServices.aspx.cs" Inherits="WFTCloud.Admin.UserServices" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>User Service List</title>
	    <meta name="description" content="we have an three tabs, they are Available services, Subscribed services and Manage CRM Issues. Each view shows the respective details" />
        <meta name="keywords" content="WFT Services and Manage CRM Issue" />
    <style type="text/css">
        .controls {
            margin-top: 0px;
        }
    </style>
    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager ID="scriptManager" runat="server"></asp:ScriptManager>
    <a href="UserList.aspx">&lt;&lt; Back to User list&nbsp;</a>
    <div class="table-header">
	    <strong>Viewing the details of <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label></strong>	        
    </div>
    <div class="row-fluid">
		<div class="span12 widget-container-span">
			<div class="widget-box">
				<div class="widget-header no-border">
					<ul class="nav nav-tabs" id="Ul1">
							<li id="liAvailable" runat="server">
								<a href="UserServices.aspx?userid=<%=UserMembershipID %>&showview=AvailableService">Available Services</a>
							</li>

							<li id="liSubscribed" runat="server">
								<a href="UserServices.aspx?userid=<%=UserMembershipID %>&showview=SubscribedService">Subscribed Services</a>
							</li>

							<%--<li id="liManage" runat="server">
								<a href="UserServices.aspx?userid=<%=UserMembershipID %>&showview=ManageCrmIssue">Manage CRM Issues</a>
							</li>--%>
						</ul>
				</div>
				<div class="widget-body">
					<div class="widget-main padding-12">
						<div class="tab-content">
							<div id="Available" class="tab-pane" runat="server">
								<div class="span12">
                                    <div class="row-fluid">
                                        <div id="divAvaSuccessMessage" runat="server" visible="false" class="alert alert-block alert-success span6">
				                            <button data-dismiss="alert" class="close" type="button">
					                            <i class="icon-remove"></i>
				                            </button>
				                            <p>
                                                <i class="icon-ok"></i>
					                            <asp:Label ID="lblAvaSuccessmsg" runat="server"></asp:Label>
				                            </p>
                                        </div>
                                        <div id="divAvaErrorMessage" runat="server" visible="false" class="alert alert-error span6">
				                            <button data-dismiss="alert" class="close" type="button">
					                            <i class="icon-remove"></i>
				                            </button>
                                            <i class="icon-remove"></i>
                                            <asp:Label ID="lblAvaErrorMessage" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row-fluid">
                                        <div class="table-header">
                                            Available Services
                                        </div>
                                        <div class="dataTables_wrapper">
                                            <div role="grid" class="dataTables_wrapper">
                                                <table class="table table-striped table-bordered" id="tblAvailableService">

                                                    <tbody role="alert">
                                                        <asp:Repeater ID="rptrWFTCloudPackagesCategoryName" runat="server"  OnItemDataBound="rptrWFTCloudPackagesCategoryName_ItemDataBound1">
                                                            <ItemTemplate>
                                                                <thead>
                                                                    <tr role="row">
                                                                        <th role="columnheader" colspan="4" class="blue" style="text-align: center"><strong><%# Eval("CategoryName")%></strong>
                                                                            <asp:HiddenField ID="hdnServiceCategoryID" runat="server" Value='<%# Eval("ServiceCategoryID")%>' />
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody role="alert">
                                                                    <asp:Repeater ID="rptrWFTCloudPackages" runat="server">
                                                                        <HeaderTemplate>
                                                                            <tr>
                                                                                <td>
                                                                                    <table class="WFTCloudPackages table table-striped table-bordered table-hover dataTable">
                                                                                        <thead>
                                                                                            <tr role="row">
                                                                                               <%-- <th class="center" role="columnheader">
                                                                                                    <label><input type="checkbox" class="ace"/>
                                                                                                        <span class="lbl"></span>
                                                                                                    </label>
                                                                                                </th>--%>
                                                                                                <th role="columnheader" style="width: 25%">Service Name</th>
                                                                                                <th role="columnheader" style="width: 20%; text-align: center;">Release Version</th>
                                                                                                <th role="columnheader" style="width: 15%; text-align: center;">Price Model</th>
                                                                                                <th role="columnheader" style="width: 20%; text-align: center;">Amount</th>
                                                                                                <th role="columnheader" style="width: 10%; text-align: center;">Add To Cart</th>
                                                                                            </tr>
                                                                                        </thead>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                              <%--  <td class="center">
                                                                                    <asp:CheckBox ID="chkSelect" runat="server" class="ace" />
                                                                                </td>--%>
                                                                                <td><%# Eval("ServiceName")%>
                                                                                    <asp:HiddenField ID="hdnNewServicesID" runat="server" Value='<%# Eval("ServiceID")%>' />
                                                                                </td>
                                                                                <td style="text-align: center;"><%# Eval("ReleaseVersion")%></td>
                                                                                <td>
                                                                                    <asp:Label runat="server" ID="lblPriceModel" Text="Fixed Price" Visible='<%#  Eval("IsPayAsYouGo")!=null?(Eval("IsPayAsYouGo").ToString().ToLower() =="true"?false:true):true %>'></asp:Label>
                                                                                    <asp:DropDownList ID="ddlPriceModel" runat="server" Visible='<%# Eval("IsPayAsYouGo")!=null?(Eval("IsPayAsYouGo").ToString().ToLower() =="true"?true:false):false %>' ><%--OnSelectedIndexChanged="ddlPriceModel_SelectedIndexChanged" AutoPostBack="true">--%>
                                                                                        <asp:ListItem Value="0">Fixed Price </asp:ListItem>
                                                                                        <asp:ListItem Value="1">Pay As You Go </asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                               <td style="text-align: center;">$<%#Eval("InitialHoldAmount") %></td>
                                                                                <td style="text-align: center; vertical-align: middle;">
                                                                                    <asp:LinkButton data-rel="tooltip" title="Add Service" ID="lkbAddServiceToUser" runat="server" OnClick="lkbAddServiceToUser_Click" CssClass="green"><i class="icon-plus-sign-alt bigger-130"></i></asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            </table>
                                                                          <%--  <div class="row-fluid">
                                    <asp:Button ID="btnAddService" runat="server" Text="Add Services" 
                                    class="btn btn-primary" OnClick="btnAddService_Click" /></div>--%>
                                                                        </FooterTemplate>
                                                                    </asp:Repeater>
                                                                    <asp:Repeater ID="rptrWFTCloudNonPayAsYouGo" runat="server">
                                                                        <HeaderTemplate>
                                                                            <tr>
                                                                                <td>
                                                                                    <table class="WFTCloudPackages table table-striped table-bordered table-hover dataTable">
                                                                                        <thead>
                                                                                            <tr role="row">
                                                                                               <%-- <th class="center" role="columnheader">
                                                                                                    <label><input type="checkbox" class="ace"/>
                                                                                                        <span class="lbl"></span>
                                                                                                    </label>
                                                                                                </th>--%>
                                                                                                <th role="columnheader" style="width: 25%">Service Name</th>
                                                                                                <th role="columnheader" style="width: 20%; text-align: center;">Release Version</th>
                                                                                                <th role="columnheader" style="width: 15%; text-align: center;">Price Model</th>
                                                                                                <th role="columnheader" style="width: 20%; text-align: center;">Amount</th>
                                                                                                <th role="columnheader" style="width: 10%; text-align: center;">Add To Cart</th>
                                                                                            </tr>
                                                                                        </thead>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                              <%--  <td class="center">
                                                                                    <asp:CheckBox ID="chkSelect" runat="server" class="ace" />
                                                                                </td>--%>
                                                                                <td><%# Eval("ServiceName")%>
                                                                                    <asp:HiddenField ID="hdnNewServicesID" runat="server" Value='<%# Eval("ServiceID")%>' />
                                                                                </td>
                                                                                <td style="text-align: center;"><%# Eval("ReleaseVersion")%></td>
                                                                                <td>
                                                                                    <asp:Label runat="server" ID="lblPriceModel" Text="Fixed Price"></asp:Label>
                                                                                </td>
                                                                               <td style="text-align: center;">$ <%#Eval("InitialHoldAmount") %></td>
                                                                                <td style="text-align: center; vertical-align: middle;">
                                                                                    <asp:LinkButton data-rel="tooltip" title="Add Service" ID="lkbAddServiceToUser" runat="server" OnClick="lkbAddServiceToUser_Click" CssClass="green"><i class="icon-plus-sign-alt bigger-130"></i></asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            </table>
                                         <%--       <div class="row-fluid">
                                    <asp:Button ID="btnAddService" runat="server" Text="Add Services" 
                                    class="btn btn-primary" OnClick="btnAddService_Click" /></div>--%>
                                                                        </FooterTemplate>
                                                                    </asp:Repeater>
                                                                </tbody>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
							</div>

							<div id="Subscribed" class="tab-pane active" runat="server">
								<div class="span12">
                                <div class="row-fluid">
                                    <asp:MultiView ID="mvSubscribed" runat="server" ActiveViewIndex="0">
                                        <asp:View ID="vwServiceList" runat="server">
                                                <div class="row-fluid">
                                            <div class="span12">
                                                <div class="row-fluid">
                                                    <div id="divSubscribedSuccess" runat="server" visible="false" class="alert alert-block alert-success span6">
                                                        <button data-dismiss="alert" class="close" type="button">
                                                            <i class="icon-remove"></i>
                                                        </button>
                                                        <p>
                                                            <i class="icon-ok"></i>
                                                            Subscribed services status updated successfully.
                                                        </p>
                                                    </div>
                                                </div>
                                            <div class="row-fluid">
	                                            <div class="table-header">
		                                            User Service Details	        
                                                </div>
                                                <div class="dataTables_wrapper">
                                                    <div class="row-fluid">
                                                        <div class="span8">
                                                            <asp:DropDownList ID="ddlCouponCode" runat="server" OnSelectedIndexChanged="ddlCouponCode_SelectedIndexChanged" AutoPostBack="true" class="span4">
                                                                <asp:ListItem Value="0" Text="All"></asp:ListItem>
                                                                <asp:ListItem Value="1" Text="Select Services with Coupon Code"></asp:ListItem>
                                                                <asp:ListItem Value="2" Text="Select Services without Coupon Code"></asp:ListItem>
                                                            </asp:DropDownList><br />
                                                        </div>
                                                    </div>
                                                    <div role="grid" class="dataTables_wrapper">
                                                        <table id="tblSubscribedService" class="table table-striped table-bordered table-hover dataTable">
                                                            <thead>
                                                            <tr role="row">
                                                                <th class="center" role="columnheader">
                                                                    <label><input type="checkbox" class="ace"/>
                                                                        <span class="lbl"></span>
                                                                    </label>
                                                                </th>
                                                                <th role="columnheader">Subscription ID</th>
                                                                <th role="columnheader">Service Name</th>
                                                                <asp:Panel ID="pnlWithCouponHeader" runat="server"><th role="columnheader">Coupon Code</th></asp:Panel>
                                                                <%--<th role="columnheader">Credit</th>
                                                                <th role="columnheader">Usage</th>--%>
                                                                <th role="columnheader">Provisioning</th>
                                                                <th role="columnheader">Price Model</th>
                                                                <th role="columnheader">Status</th>
                                                                <th role="columnheader" style="text-align:center;">Options</th>
                                                            </tr>
                                                            </thead>
                                                            <tbody role="alert">
                                                            <asp:Repeater ID="rptrSubscribedServices" runat="server">
                                                            <ItemTemplate>
                                                            <tr>
                                                                <td class="center">
                                                                    <asp:CheckBox ID="chkSubscribedSelect" runat="server" class="ace" />
                                                                </td>
                                                                <td><%# Eval("UserSubscriptionID") %></td>
                                                                <td><%# GetServiceName(Eval("ServiceID").ToString())%>
                                                                    <asp:HiddenField ID="hdnServiceID" runat="server" Value='<%# Eval("UserSubscriptionID")%>' />
                                                                </td>
                                                                <asp:Panel ID="pnlWithCouponTd" runat="server"><td><%# Eval("CouponCode")==null?"Not Applied":Eval("CouponCode")%></td></asp:Panel>
                                                                 <td><%# Eval("AutoProvisioningDone") != null?SubProvisionStatus(Eval("AutoProvisioningDone").ToString()):"Provision Pending"%></td>
                                                                 <td>
                                                                     <%# Eval("IsPayAsYouGo") != null? (Eval("IsPayAsYouGo").ToString().ToLower()== "true"?"Pay As You Go":"Fixed Price"):"Fixed Price"%></td>
                                                                </td>
                                                                <td><%#ShowSubscribedServiceStatus(Eval("UserSubscriptionID").ToString()) %></td>
                                                                <td>
                                                                    <div class="action-buttons">
                                                                    <a data-rel="tooltip" title="Edit Service" href='UserServices.aspx?SubscribedServiceid=<%# Eval("UserSubscriptionID")%>&userid=<%=UserMembershipID %>&showview=SubscribedService' class="green">
                                                                        <i class="icon-pencil bigger-130"></i>
                                                                    </a>
                                                                     <a data-rel="tooltip" title="User Service Provision" href='UserServices.aspx?UserServiceProvisionid=<%# Eval("UserSubscriptionID")%>&userid=<%=UserMembershipID %>&showview=SubscribedService' class="purple">
                                                                        <i class="icon-bookmark"></i>
                                                                    </a>
                                                                    <a data-rel="tooltip" title="Activate/Deactivate" href='UserServices.aspx?<%# WFTCloud.GeneralReusableMethods.GetActivateDeactivateAction(Eval("RecordStatus").ToString()) %>=<%# Eval("UserSubscriptionID")%>&userid=<%=UserMembershipID %>&showview=SubscribedService' class="blue">
                                                                        <i class='<%# WFTCloud.GeneralReusableMethods.GetActivateDeactivateIcon(Eval("RecordStatus").ToString()) %>'></i>
                                                                    </a>
                                                                   <%-- <a data-rel="tooltip" title="Delete Service" href='UserServices.aspx?delete=<%# Eval("ServiceID")%>&userid=<%=UserMembershipID %>&showview=SubscribedService' class="red">
                                                                        <i class="icon-trash bigger-130"></i>
                                                                    </a>--%>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            </ItemTemplate>
                                                            </asp:Repeater>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                            </div>
                                                <div class="row-fluid">
                                                    <asp:Button ID="btnSubscribedActivate" runat="server" Text="Activate" 
                                                        class="btn btn-info" OnClick="btnSubscribedActivate_Click" />
                                                    <asp:Button ID="btnSubscribedDeactivate" runat="server" Text="Deactivate" 
                                                        class="btn btn-warning" OnClick="btnSubscribedDeactivate_Click" />
                                                    <%--<asp:Button ID="btnSubscribedDelete" runat="server" Text="Delete" 
                                                        class="btn btn-danger" OnClick="btnSubscribedDelete_Click" />--%>
                                                    <asp:CheckBox ID="SubscribedShowDeleted" runat="server"
                                                            AutoPostBack="true" OnCheckedChanged="SubscribedShowDeleted_CheckedChanged"/>
                                                            <span class="label label-warning arrowed-right">Show Deactivated</span>
                                                </div>
                                            </div>
                                            </div>
                                            </div>
                                        </asp:View>
                                        <asp:View ID="vwEditService" runat="server">
                                            <div class="row-fluid">
                                                <div id="lblSubscribedSuccessMessage" runat="server" visible="false" class="alert alert-block alert-success span6">
							                        <button data-dismiss="alert" class="close" type="button">
								                        <i class="icon-remove"></i>
							                        </button>
							                        <p>
                                                        <i class="icon-ok"></i>
								                        Service Details updated successfully.
							                        </p>
						                        </div>
                                                <div id="lblSubscribedErrorMessage" runat="server" visible="false" class="alert alert-error span6">
						                            <button data-dismiss="alert" class="close" type="button">
							                            <i class="icon-remove"></i>
						                            </button>
                                                    <i class="icon-remove"></i>
					                                Error saving Service details.
                                                </div>
                                            </div>
	                                            <a href="UserServices.aspx?userid=<%=UserMembershipID %>&showview=SubscribedService">&lt;&lt; Back&nbsp;</a>
                                                <div class="table-header">
		                                            Edit User Subscribed Service
	                                            </div>
                                            <div class="row-fluid">
                                                <table class="table table-hover dataTable table-bordered ">
                                                    <tr>
                                                        <td class="span3">Service ID
                                                        </td>
                                                        <td><asp:Label ID="lblServiceID" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="span3">User SubscriptionID
                                                        </td>
                                                        <td><asp:Label ID="lblUserSubscriptionID" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="span2">Service Category
                                                        </td>
                                                        <td>  
                                                            <asp:TextBox ID="txtServiceCategory" runat="server" ReadOnly="true"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="span2">Service Name
                                                        </td>
                                                        <td>  
                                                            <asp:TextBox ID="txtServiceName" runat="server" ReadOnly="true"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="span2">Service User Name
                                                        </td>
                                                        <td><asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvusername" runat="server" ControlToValidate="txtUserName" ErrorMessage="*" ForeColor="Red" ValidationGroup="ServiceProvision"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr runat="server" id="trServicePassword">
                                                        <td class="span2">Service Password
                                                        </td>
                                                        <td><asp:TextBox ID="txtServicePassword" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvEmailID0" runat="server" ControlToValidate="txtServicePassword" ErrorMessage="*" ForeColor="Red" ValidationGroup="ServiceProvision"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="span2">Service URL
                                                        </td>
                                                        <td><asp:TextBox ID="txtServiceURL" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvEmailID" runat="server" ControlToValidate="txtServiceURL" ErrorMessage="*" ForeColor="Red" ValidationGroup="ServiceProvision"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="span2">Service Information
                                                        </td>
                                                        <td>  
                                                            <asp:TextBox ID="txtServiceInformation" runat="server" TextMode="MultiLine" Height="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="span2">Coupon Code
                                                        </td>
                                                        <td>  
                                                            <asp:TextBox ID="txtCouponCode" runat="server" ReadOnly="true"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="span2">Initial Hold Amount
                                                        </td>
                                                        <td>  
                                                            <asp:TextBox ID="txtInitialHoldAmount" runat="server" ReadOnly="true"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="span2">Usage Units
                                                        </td>
                                                        <td>  
                                                            <asp:TextBox ID="txtUsageUnit" runat="server" ReadOnly="true"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                       <tr id="trWFTCloudPrice" runat="server" >
                                                        <td class="span2">WFT Cloud Price
                                                        </td>
                                                        <td>  
                                                            <asp:TextBox ID="txtWFTCloudPrice" runat="server" ReadOnly="true"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="span2">Service IP Address
                                                        </td>
                                                        <td>  
                                                            <asp:TextBox ID="txtIPAddress" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Expiration Date </td>
                                                        <td><asp:Label runat="server" ID="lblExpDate"></asp:Label> </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="span2">Service Status
                                                        </td>
                                                        <td> <asp:DropDownList ID="ddlRecordStatus" runat="server">
                                                                <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                                                                <asp:ListItem Value="0" Text="Inactive"></asp:ListItem>
                                                                <asp:ListItem Value="-1" Text="Deleted"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="span2">
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnSubscribedSave" runat="server" Text="Save" OnClick="btnSubscribedSave_Click" CssClass="btn btn-info" ValidationGroup="ServiceProvision"/>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:View>
                                        <asp:View ID="vwNoAvailableServices" runat="server">
                                            <asp:Panel ID="pnlBackToSubscribedService" runat="server" Visible="false">
                                                <div>
                                                    <a href="UserServices.aspx?userid=<%=UserMembershipID %>&showview=SubscribedService">&lt;&lt; Back&nbsp;</a>
                                                </div>
                                            </asp:Panel>
                                            <div id="NoAvailableService" runat="server" class="alert alert-warning" >
                                                <button data-dismiss="alert" class="close" type="button">
                                                <i class="icon-remove"></i>
                                                </button>
                                                <i class="icon-remove"></i>
                                                <strong><asp:Label ID="lblSubscribedWarning" runat="server" Text="There are no User Prescribed Services."></asp:Label></strong>
                                            </div>
                                        </asp:View>
                                        <asp:View ID="vwUserServiceProvision" runat="server">
                                            <div class="row-fluid">
                                                <div class="row-fluid">
                                                    <div id="divUSPDSuccessMessage" runat="server" visible="false" class="alert alert-block alert-success span6">
                                                        <button data-dismiss="alert" class="close" type="button">
                                                            <i class="icon-remove"></i>
                                                        </button>
                                                        <p>
                                                            <i class="icon-ok"></i>
                                                            User Service Provision Details updated successfully.
                                                        </p>
                                                    </div>
                                                        <div id="divUSPDErrorMessage" runat="server" visible="false" class="alert alert-error span6">
				                                        <button data-dismiss="alert" class="close" type="button">
					                                        <i class="icon-remove"></i>
				                                        </button>
                                                        <i class="icon-remove"></i>
                                                        <asp:Label ID="lblUSPDError" runat="server"></asp:Label>
                                                     </div>
                                                    </div>
                                                <a href="UserServices.aspx?userid=<%=UserMembershipID %>&showview=SubscribedService">&lt;&lt; Back&nbsp;</a>
                                                <div class="table-header">
                                                    Edit User Service Provisioning Details
                                                </div>
                                                <table class="table table-hover dataTable table-bordered ">
                                                     <tr>
                                                        <td class="span2">User SubscriptionID </td>
                                                        <td>
                                                            <asp:Label ID="lblSubscriptionID" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="span4">User Name</td>
                                                        <td>
                                                            <asp:Label ID="lblUSPDUserName" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="span2">Category Name</td>
                                                        <td>
                                                            <asp:Label ID="lblUSPDCategoryName" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="span2">Service Name</td>
                                                        <td>
                                                            <asp:Label ID="lblUSPDServiceName" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td class="span2">SAP/Service User Name</td>
                                                        <td>
                                                            <asp:TextBox ID="txtUSPDServiceUserName" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvServiceUserName" runat="server" ControlToValidate="txtUSPDServiceUserName" ErrorMessage="*" ForeColor="Red" ValidationGroup="UserServiceProvision"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="span2">SAP/Service Password</td>
                                                        <td>
                                                            <asp:TextBox ID="txtUSPDServicePassword" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvServicePassword" runat="server" ControlToValidate="txtUSPDServicePassword" ErrorMessage="*" ForeColor="Red" ValidationGroup="UserServiceProvision"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="span2">SAP/Service URL</td>
                                                        <td>
                                                            <asp:TextBox ID="txtUSPDServiceURL" runat="server">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvServiceURL" runat="server" ControlToValidate="txtUSPDServiceURL" ErrorMessage="*" ForeColor="Red" ValidationGroup="UserServiceProvision"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td class="span2">SID On Server</td>
                                                        <td>
                                                            <asp:TextBox ID="txtUSPDUIDOnServer" runat="server">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvUIDOnServer" runat="server" ControlToValidate="txtUSPDUIDOnServer" ErrorMessage="*" ForeColor="Red" ValidationGroup="UserServiceProvision"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="span2">Instance Number</td>
                                                        <td>
                                                            <asp:TextBox ID="txtInstanceNumber" runat="server">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvInstanceNumber" runat="server" ControlToValidate="txtInstanceNumber" ErrorMessage="*" ForeColor="Red" ValidationGroup="UserServiceProvision"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="span2">Application Server</td>
                                                        <td>
                                                            <asp:TextBox ID="txtApplicationServer" runat="server">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvApplicationServer" runat="server" ControlToValidate="txtApplicationServer" ErrorMessage="*" ForeColor="Red" ValidationGroup="UserServiceProvision"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td class="span2">Developer Key</td>
                                                        <td>
                                                            <asp:TextBox ID="txtDeveloperKey" runat="server">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="span2">Other Information</td>
                                                        <td>
                                                            <asp:TextBox ID="txtUSPDOtherInormation" runat="server" TextMode="MultiLine">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvOtherInformation" runat="server" ControlToValidate="txtUSPDOtherInormation" ErrorMessage="*" ForeColor="Red" ValidationGroup="UserServiceProvision"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="span2">Add Image Attachment </td>
                                                        <td>
                                                            <asp:FileUpload ID="fluUSPDAddAttachments" runat="server" Width="237px" /><br />
                                                            <asp:HyperLink runat="server" Target="_blank" ID="hlAttachment" Visible="false" Text="Click here to view attachment"></asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="span2">Add Attachment </td>
                                                        <td>
                                                            <asp:FileUpload ID="fluUSPDAddAtta" runat="server" Width="237px" /><br />
                                                            <asp:HyperLink runat="server" Target="_blank" ID="hlAttachment2" Visible="false" Text="Click here to view attachment"></asp:HyperLink>
                                                        </td>
                                                    </tr>
<%--                                                    <tr>
                                                        <td class="span2">Expiration Period</td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlExpirationPeriod" runat="server" Height="30px" Width="198px">
                                                                <asp:ListItem Text="0" Value="0" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>--%>
                                                    <tr>
                                                        <td class="span2">Expiration Date</td>
                                                        <td>
                                                            <%--<ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
                                                            <asp:TextBox ID="txtUSPDExpirationDate" runat="server" ReadOnly="true"></asp:TextBox>
                                                            <%--<ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtExpirationDate" runat="server" Format="dd/MM/yyyy"></ajaxToolkit:CalendarExtender>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <asp:Button ID="btnUSPDSubmit" runat="server" Text="Submit" class="btn btn-primary" ValidationGroup="UserServiceProvision" OnClick="btnUSPDSubmit_Click" /></td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:View>
                                    </asp:MultiView>
                                </div>
                                </div>
							</div>

<%--// Manage Crm Request HTML Template Here--%>
						</div>
					</div>
				</div>
			</div>
		</div>
        <asp:HiddenField ID="hdnFlagForCancel" Value="0" runat="server" />
        <asp:ModalPopupExtender ID="mpopupCanCelServices" runat="server" TargetControlID="hfCancel" BackgroundCssClass="modalBackground" PopupControlID="divCancelServices" CancelControlID="lblBack"></asp:ModalPopupExtender>
        <asp:HiddenField ID="hfCancel" runat="server" />
        <div id="divCancelServices" runat="server">
            <asp:Panel runat="server" ID="pnlCancel" Width="500px">
                <div id="login-box" class="login-box visible widget-box no-border">
                    <div class="widget-body">

                        <div class="widget-main">
                            <h4 class="header blue lighter bigger">
                                <i class="icon-coffee green"></i>
                                Please Enter Comments about the service</h4>

                            <div class="space-6"></div>


                            <span style="color: red;">*</span>
                                Reason :
															<span class="block input-icon input-icon-right" style="resize: none;">
                                                                <asp:TextBox ID="txtReasons" runat="server" TextMode="MultiLine" CssClass="span12" Width="475px" Height="70px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator runat="server" ID="rfvtxtReasons" ControlToValidate="txtReasons" ValidationGroup="CancelServices"></asp:RequiredFieldValidator>
                                                            </span>
                             <span style="color: red;">*</span>
                            Feedback : <span class="block input-icon input-icon-right" style="resize: none;">
                            <asp:TextBox ID="txtFeedbacks" runat="server" CssClass="span12" Height="70px" TextMode="MultiLine" Width="475px"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfvFeedBacks" ControlToValidate="txtFeedbacks" ValidationGroup="CancelServices"></asp:RequiredFieldValidator>
                            </span>

                            <span style="color: red;">*</span>
                            <asp:Label ID="lblRateourService" runat="server" Text="Rate Our Service"></asp:Label>
                            <span class="block input-icon input-icon-right" style="resize: none;">
                            <asp:RadioButtonList ID="rblServiceRating" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="3">3</asp:ListItem>
                                <asp:ListItem Value="4">4</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                                <asp:ListItem Value="6">6</asp:ListItem>
                                <asp:ListItem Value="7">7</asp:ListItem>
                                <asp:ListItem Value="8">8</asp:ListItem>
                                <asp:ListItem Value="9">9</asp:ListItem>
                                <asp:ListItem Value="10">10</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator runat="server" ID="rfvServiceRating" ControlToValidate="rblServiceRating" ValidationGroup="CancelServices"></asp:RequiredFieldValidator>
                            </span>
                            <div class="clearfix">
                               <asp:Button ID="btnCancelServices" runat="server" style="float:right;" CssClass="btn btn-danger btn-small" OnClick="btnCancelServices_Click" Text="Cancel Services"  ValidationGroup="CancelServices" />
                               <%-- <a data-rel="tooltip" style="float:right;"  title="Cancel Service"  class="btn btn-danger btn-small">
                                   
                                </a>--%>

                            </div>
                            <div class="space-4"></div>


                        </div>
                        <!--/widget-main-->
                        <div class="toolbar clearfix">
                            <div>
                                <asp:LinkButton ID="lblBack" runat="server" class="forgot-password-link"><i class="icon-arrow-left"></i>
														        Back</asp:LinkButton>
                            </div>
                        </div>

                    </div>
                    <!--/widget-body-->
                </div>
                <!--/login-box-->
            </asp:Panel>


        </div>
	</div>
    <script type="text/javascript">
        jQuery(function ($) {
            $('.WFTCloudPackages').dataTable(
                {
                    "aoColumnDefs": [
                        { 'bSortable': false, 'aTargets': [4] }
                    ]
                }
         );
        });
        jQuery(function ($) {
            $('#tblSubscribedService').dataTable(
                {
                    "aoColumnDefs": [
                        { 'bSortable': false, 'aTargets': [0, 7] }
                    ]
                }
         );
        });
        jQuery(function ($) {
            $('#tblManageCrm').dataTable(
                {
                    "aoColumnDefs": [
                        { 'bSortable': false, 'aTargets': [0, 5] }
                    ]
                }
         );
        });
   </script>
</asp:Content>

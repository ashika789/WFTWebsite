<%@ Page Title="" Language="C#" MasterPageFile="~/Customer/CustomerPages.Master" EnableEventValidation="true" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="CloudPackages.aspx.cs" Inherits="WFTCloud.Customer.CloudPackages" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>WFT Cloud Packages</title>
    <meta name="description" content="WFT Cloud Packages to display the available resources" />
    <meta name="keywords" content="WFT Cloud Packages" />
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
    <script type="text/javascript">


        function ShowModalPopup() {
            $find("ContentPlaceHolder1_mpopupTermsCondition").show();
            return false;
        }
        function HideModalPopup() {
            $find("ContentPlaceHolder1_mpopupTermsCondition").hide();
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="scriptManager" runat="server"></asp:ScriptManager>
    <div class="table-header">
        <strong>WFT Cloud Packages</strong>
    </div>
    <div class="row-fluid">
        <div class="span12 widget-container-span">
            <div class="widget-box">
                <div class="widget-header no-border">
                    <ul class="nav nav-tabs" id="Ul1">
                        <li id="liCloudPackages" runat="server" class="active">
                            <%--<asp:LinkButton runat="server" ID="lkbtnCloudPackages" Text="Available Services" OnClick="lkbtnCloudPackages_Click"></asp:LinkButton>--%>
                            <a href="/Customer/CloudPackages.aspx?userid=<%=UserMembershipID %>&showview=AvailableService">Available Services</a>
                        </li>

                        <li id="liSubscribedPackages" runat="server">
                            <%--<asp:LinkButton runat="server" ID="lkbtnSubscribedPackages" Text="Subscribed Services" OnClick="lkbtnSubscribedPackages_Click" ></asp:LinkButton>--%>
                            <a href="/Customer/CloudPackages.aspx?userid=<%=UserMembershipID %>&showview=SubscribedService&status=Active">Subscribed Services</a>
                        </li>
                        <li id="liMyCart" runat="server">
                            <%--<asp:LinkButton runat="server" ID="lkbtnMyCart" Text="My Cart" OnClick="lkbtnMyCart_Click" ></asp:LinkButton>--%>
                            <a href="/Customer/CloudPackages.aspx?userid=<%=UserMembershipID %>&showview=ShowMyCart">My Cart</a>
                        </li>
                    </ul>
                </div>
                <div class="widget-body">
                    <div class="widget-main padding-12">
                        <div class="tab-content">
                            <div id="WFTCloudPackages" class="tab-pane" runat="server">
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

                                        <asp:Button ID="btnShowMyCart0" data-rel="tooltip" title="Show My Cart" runat="server" Text="Show My Cart" Style="float: right;"
                                            class="btn btn-primary icon-shopping-cart" OnClick="btnShowMyCart_Click" Visible="False" />
                                        <br />
                                        <br />

                                        <div class="table-header">
                                            Available Services
                                        </div>
                                        <div class="dataTables_wrapper">
                                            <div role="grid" class="dataTables_wrapper">
                                                <table class="table table-striped table-bordered" id="tblWFTCloudPackages">

                                                    <tbody role="alert">
                                                        <asp:Repeater ID="rptrWFTCloudPackagesCategoryName" runat="server" OnItemDataBound="rptrWFTCloudPackagesCategoryName_ItemDataBound1">
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
                                                                                <td><%# Eval("ServiceName")%>
                                                                                    <asp:HiddenField ID="hdnNewServicesID" runat="server" Value='<%# Eval("ServiceID")%>' />
                                                                                </td>
                                                                                <td style="text-align: center;"><%# Eval("ReleaseVersion")%></td>
                                                                                <td>
                                                                                    <asp:Label runat="server" ID="lblPriceModel" Text="Fixed Price" Visible='<%#  Eval("IsPayAsYouGo")!=null?(Eval("IsPayAsYouGo").ToString().ToLower() =="true"?false:true):true %>'></asp:Label>
                                                                                    <asp:DropDownList ID="ddlPriceModel" runat="server" Visible='<%# Eval("IsPayAsYouGo")!=null?(Eval("IsPayAsYouGo").ToString().ToLower() =="true"?true:false):false %>'>
                                                                                        <%--OnSelectedIndexChanged="ddlPriceModel_SelectedIndexChanged" AutoPostBack="true">--%>
                                                                                        <asp:ListItem Value="0">Fixed Price </asp:ListItem>
                                                                                        <asp:ListItem Value="1">Pay As You Go </asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td style="text-align: center;">$<%#Eval("InitialHoldAmount") %></td>
                                                                                <td style="text-align: center; vertical-align: middle;">
                                                                                    <%--  <a data-rel="tooltip" title="Add To Cart" href='/Customer/CloudPackages.aspx?userid=<%=UserMembershipID %>&showview=AvailableService&AddToCart=<%# Eval("ServiceID")%>' class="btn btn-primary btn-small">
                                                                                        <i class="icon-shopping-cart"></i>
                                                                                    </a>--%>
                                                                                    <asp:LinkButton ID="lkbAddCart" runat="server" data-rel="tooltip" title="Add To Cart" class="btn btn-primary btn-small" OnClick="lkbAddCart_Click"><i class="icon-shopping-cart"></i></asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            </table>
                                                </td>
                                                </tr>
                                                                        </FooterTemplate>
                                                                    </asp:Repeater>
                                                                    <asp:Repeater ID="rptrWFTCloudNonPayAsYouGo" runat="server">
                                                                        <HeaderTemplate>
                                                                            <tr>
                                                                                <td>
                                                                                    <table class="WFTCloudPackages table table-striped table-bordered table-hover dataTable">
                                                                                        <thead>
                                                                                            <tr role="row">
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
                                                                                <td><%# Eval("ServiceName")%>
                                                                                    <asp:HiddenField ID="hdnNewServicesID" runat="server" Value='<%# Eval("ServiceID")%>' />
                                                                                </td>
                                                                                <td style="text-align: center;"><%# Eval("ReleaseVersion")%></td>
                                                                                <td>
                                                                                    <asp:Label runat="server" ID="lblPriceModel" Text="Fixed Price"></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: center;">$ <%#Eval("InitialHoldAmount") %></td>
                                                                                <td style="text-align: center; vertical-align: middle;">
                                                                                    <asp:LinkButton ID="lkbAddCart" runat="server" data-rel="tooltip" title="Add To Cart" class="btn btn-primary btn-small" OnClick="lkbAddCart_Click"><i class="icon-shopping-cart"></i></asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            </table>
                                                </td>
                                                </tr>
                                                                        </FooterTemplate>
                                                                    </asp:Repeater>
                                                                </tbody>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                        <div class="row-fluid">
                                            <br />
                                            <asp:Button ID="btnShowMyCart" runat="server" data-rel="tooltip" title="Proceed to Pay" Text="Proceed to Pay" Style="float: right;"
                                                class="btn btn-primary" OnClick="btnShowMyCart_Click" Visible="False" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="SubscribedPackages" class="tab-pane" runat="server">
                                <asp:MultiView runat="server" ID="mvSubservice" ActiveViewIndex="0">
                                    <asp:View runat="server" ID="vwSubServices">
                                        <div class="span12">
                                            <div class="row-fluid">
                                                <div class="row-fluid">
                                                    <div id="divSubscribedSuccess" runat="server" visible="false" class="alert alert-block alert-success ">
                                                        <button data-dismiss="alert" class="close" type="button">
                                                            <i class="icon-remove"></i>
                                                        </button>
                                                        <p>
                                                            <i class="icon-ok"></i>
                                                            <asp:Label ID="lblSubscribedSuccess" runat="server"></asp:Label>
                                                        </p>
                                                    </div>
                                                    <div id="divSubscribedFailed" runat="server" visible="false" class="alert alert-error ">
                                                        <button data-dismiss="alert" class="close" type="button">
                                                            <i class="icon-remove"></i>
                                                        </button>
                                                        <i class="icon-remove"></i>
                                                        <asp:Label ID="lblSubscribedfailed" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <asp:Button ID="btnShowMyCart3" runat="server" data-rel="tooltip" title="Show My Cart" Text="Show My Cart" Style="float: right;"
                                                    class="btn btn-primary" OnClick="btnShowMyCart_Click" Visible="False" />
                                                <table class="table table-hover dataTable table-bordered ">
                                                    <tr>
                                                        <td class="span2">Subscription Status
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Expired" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="Cancelled" Value="3"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>

                                                <div class="table-header">
                                                    Subscribed Services       
                                                </div>
                                                <div class="dataTables_wrapper">
                                                    <div role="grid" class="dataTables_wrapper">
                                                        <table class="table table-striped table-bordered table-hover dataTable" id="tblSubscribedPackages">
                                                            <thead>
                                                                <tr role="row">
                                                                    <th role="columnheader">Subscription ID</th>
                                                                    <th role="columnheader">Category Name</th>
                                                                    <th role="columnheader">Service Name</th>
                                                                    <asp:Panel ID="pnlWithCouponHeader" runat="server">
                                                                        <th role="columnheader" style="text-align: center;">Coupon Code</th>
                                                                    </asp:Panel>
                                                                    <th role="columnheader" style="text-align: center;">Amount</th>
                                                                    <th role="columnheader">Activated Date</th>
                                                                    <th role="columnheader">Status</th>

                                                                    <th role="columnheader" style="text-align: center;">Action</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody role="alert">
                                                                <asp:Repeater ID="rptrSubscribedPackages" runat="server">
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td><%# Eval("UserSubscriptionID") %></td>
                                                                            <td><%# GetCategoryName(Eval("ServiceCategoryID").ToString()) %></td>
                                                                            <td><%# ServiceName(Eval("ServiceID").ToString()) %>
                                                                                <asp:HiddenField ID="hdnServiceID" runat="server" Value='<%# Eval("ServiceID")%>' />
                                                                            </td>
                                                                            <asp:Panel ID="pnlWithCouponTd" runat="server">
                                                                                <td style="text-align: center;"><%# Eval("CouponCode") == null? "Not Applied":Eval("CouponCode")%></td>
                                                                            </asp:Panel>
                                                                            <td style="text-align: center;">$ <%# Eval("InitialHoldAmount")%></td>
                                                                            <td><%# Eval("ActiveDate","{0:dd-MMM-yyyy}")%></td>
                                                                            <td><%# ShowSubscribedServiceStatus(Eval("UserSubscriptionID").ToString()) %></td>
                                                                            <td style="text-align: center; vertical-align: middle;">
                                                                                <%--<a data-rel="tooltip" title="Launch Service" target="_blank" href='<%# Eval("ServiceUrl")%>' style='<%# Eval("AutoProvisioningDone") != null? (Eval("AutoProvisioningDone").ToString().ToLower() == "true"?"display:block;":"display:none;"):"display:none;"%>' >
                                                                            <i class="icon-desktop"></i>
                                                                        </a>--%>
                                                                                <asp:Panel runat="server" ID="pnlLaunch" Visible='<%# LaunchServiceStatus(Eval("UserSubscriptionID").ToString()) %>'>
                                                                                    <asp:LinkButton ID="lkbLaunchService" runat="server" OnClick="lkbLaunchService_Click" data-rel="tooltip" title="Launch Service" Visible='<%# Eval("AutoProvisioningDone") != null? (Eval("AutoProvisioningDone").ToString().ToLower() == "true"?true:false):false%>'><i class="icon-desktop"></i></asp:LinkButton>
                                                                                </asp:Panel>
                                                                                <asp:HiddenField ID="hdnLaunchLink" runat="server" Value='<%# Eval("ServiceUrl")%>' />
                                                                                <a data-rel="tooltip" title="View Service Details" href='/Customer/CloudPackages.aspx?userid=<%=UserMembershipID %>&showview=SubscribedService&ShowUserSubscriptionDetails=<%# Eval("UserSubscriptionID")%>'>
                                                                                    <i class="icon-file blue"></i>
                                                                                </a>
                                                                                <a data-rel="tooltip" title="Cancel Service" href='/Customer/CloudPackages.aspx?userid=<%=UserMembershipID %>&showview=SubscribedService&CancelSubscribedService=<%# Eval("UserSubscriptionID")%>' style='<%# (ShowSubscribedServiceStatus(Eval("UserSubscriptionID").ToString())  != "Expired" && ShowSubscribedServiceStatus(Eval("UserSubscriptionID").ToString())  != "Cancelled")? "display:block;": "display:none;"%>'>
                                                                                    <i class="icon-remove-sign red"></i>
                                                                                </a>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:View>
                                    <asp:View runat="server" ID="vwSubServiceDetails">
                                        <%--<h4><a href="/Customer/CloudPackages.aspx?userid=<%=UserMembershipID %>&showview=SubscribedService"><< Back</a></h4>--%><asp:Button ID="btnback" runat="server" Text="<<Back" class="btn btn-primary" align="justify" OnClick="backbtn_Click"/>
                                        <div class="table-header">
                                            <strong>
                                                <asp:Label runat="server" ID="lblUserSubServiceIDHead"></asp:Label>
                                            </strong>
                                        </div>
                                        <table class="table table-striped table-bordered table-hover dataTable">
                                            <tr>
                                                <td class="span3">Service Category</td>
                                                <td class="span9">
                                                    <asp:Label ID="lblShowServiceCategory" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="span3">Service Name</td>
                                                <td>
                                                    <asp:Label ID="lblShowServiceName" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Activated Date</td>
                                                <td>
                                                    <asp:Label ID="lblShowActivatedDate" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="trShowServiceUserName" runat="server" visible="false">
                                                <td>Service User Name</td>
                                                <td>
                                                    <asp:Label ID="lblShowServiceUserName" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="trShowLanchService" runat="server" visible="false">
                                                <td>Launch Service</td>
                                                <td>
                                                    <asp:HyperLink runat="server" data-rel="tooltip" title="Launch Service" ID="hypShowLanchService" Target="_blank" CssClass="btn btn-primary btn-small">
                                                        <i class="icon-desktop"></i>Launch
                                                    </asp:HyperLink>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Initial Hold Amount 
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblShowInitialHoldAmount" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Other Service Infomation
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblShowOtherServiceInfomation" runat="server" CssClass="span12" Height="100px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Expiration Period
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl" runat="server" Text="This is a paid service. Expiration will be based on service payments made."></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Cancelled Date
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCanceledDate" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Terms & Conditions
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbltermsandcondition" runat="server" Text="<b>Cancellation & Refund Policy</b><br>The service order is processed your account will remain active unless you choose to terminate your subscription. To stop your service subscription, you need to log into your profile in WFT Cloud Portal. You will have option to cancel your service under subscribed services. No Email requests are accepted for cancellation of your services. Any violation of WFT's Terms of Service Agreement & cancellation policy shall result in NO Refund."></asp:Label>
                                                </td>
                                            </tr>

                                        </table>
                                    </asp:View>
                                </asp:MultiView>
                            </div>
                            <div id="MyCart" class="tab-pane active" runat="server">

                                <div class="span12">

                                    <div runat="server" id="zMycart" visible="true">
                                        <div class="row-fluid">
                                            <div id="divMyDeleteCartSuccess" runat="server" visible="false" class="alert alert-block alert-success span6">
                                                <button data-dismiss="alert" class="close" type="button">
                                                    <i class="icon-remove"></i>
                                                </button>
                                                <p>
                                                    <i class="icon-ok"></i>
                                                    <asp:Label ID="lblMyDeleteCartSuccess" runat="server"></asp:Label>
                                                </p>
                                            </div>
                                            <div id="divMyDeleteCartFailed" runat="server" visible="false" class="alert alert-error span6">
                                                <button data-dismiss="alert" class="close" type="button">
                                                    <i class="icon-remove"></i>
                                                </button>
                                                <i class="icon-remove"></i>
                                                <asp:Label ID="lblMyDeleteCartFailed" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row-fluid">
                                            <div class="table-header">
                                                My Cart     
                                            </div>
                                            <div class="dataTables_wrapper">
                                                <div role="grid" class="dataTables_wrapper">
                                                    <table class="table table-striped table-bordered table-hover dataTable" id="tblMyCart">
                                                        <thead>
                                                            <tr role="row">
                                                                <th role="columnheader" style="width: 25%;">Service Name</th>
                                                                <th role="columnheader" style="width: 10%;">Price Model</th>
                                                                <th role="columnheader" style="width: 15%;">Discount Type</th>
                                                                <th role="columnheader" style="width: 10%;">Unit Price</th>
                                                                <th role="columnheader" style="width: 10%;">Discount </th>
                                                                <th role="columnheader" style="width: 10%;">Quantity</th>
                                                                <th role="columnheader" style="width: 10%; text-align: center;">Total Price</th>
                                                                <th role="columnheader" style="width: 10%; text-align: center;">Delete</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody role="alert">
                                                            <asp:Repeater ID="rptrMyCart" runat="server" OnItemDataBound="rptrMyCart_ItemDataBound">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td><%# ServiceName(Eval("ServiceID").ToString()) %>
                                                                            <asp:HiddenField ID="hidUserCartID" runat="server" Value='<%# Eval("UserCartID")%>' />
                                                                            <asp:HiddenField ID="hdnUserProfileID" runat="server" Value='<%# Eval("UserProfileID")%>' />
                                                                            <asp:HiddenField ID="hdnServiceID" runat="server" Value='<%# Eval("ServiceID")%>' />
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("IsPayAsYouGo")!= null ?(Eval("IsPayAsYouGo").ToString().ToLower() == "true"? "Pay As You Go":"Fixed Price"): "Fixed Price"%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lblDiscountType" Text="No Discount" Visible='<%#Eval("IsPayAsYouGo")!= null ?( Eval("IsPayAsYouGo").ToString().ToLower() =="true"?true:false):false %>'></asp:Label>
                                                                            <asp:DropDownList ID="ddlDiscountType" Style="width: 100px;" runat="server" SelectedValue='<%# Eval("SelectedDiscount")!=null?Eval("SelectedDiscount"):"0" %>' OnSelectedIndexChanged="ddlDiscountType_SelectedIndexChanged" AutoPostBack="true" Visible='<%#Eval("IsPayAsYouGo")!= null ?(Eval("IsPayAsYouGo").ToString().ToLower() =="true"?false:true):true %>'>
                                                                                <asp:ListItem Value="0">None</asp:ListItem>
                                                                                <asp:ListItem Value="1">Pre-Pay 3 Months Savings</asp:ListItem>
                                                                                <asp:ListItem Value="2">Pre-Pay 6 Months Savings</asp:ListItem>
                                                                                <asp:ListItem Value="3">Pre-Pay 9 Months Savings</asp:ListItem>
                                                                                <asp:ListItem Value="4">Pre-Pay 12 Months Savings</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td style="text-align: center;">$
                                                                            <asp:Label runat="server" ID="lblUnitPrice" Text='<%# ServiceUnitPrice(Eval("ServiceID").ToString()) %>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lblDiscountPercentage" Text='<%# GetDiscountValue(Eval("ServiceID").ToString(),Eval("SelectedDiscount")!=null? Eval("SelectedDiscount").ToString():"0") %>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: center;">
                                                                            <asp:TextBox runat="server" ID="lblQty" Text='<%#Eval("Quantity") %>' CssClass="Numeric1 span12" AutoPostBack="true" OnTextChanged="lblQty_TextChanged" Style="text-align: right;"></asp:TextBox></td>
                                                                    
                                                                        <td style="text-align: center;">$
                                                                            <asp:Label runat="server" ID="lblPrice" Text='<%# ServicePrice(Eval("ServiceID").ToString(),Eval("Quantity").ToString(),Eval("SelectedDiscount")== null?"0":Eval("SelectedDiscount").ToString()) %>'></asp:Label></td>
                                                                        <td style="text-align: center; vertical-align: middle;">
                                                                            <a data-rel="tooltip" title="Delete From Cart" onclick="return ConfirmOnDeleteCart()" href='/Customer/CloudPackages.aspx?userid=<%=UserMembershipID %>&showview=ShowMyCart&deleteMycart=<%# Eval("UserCartID")%>' class="btn btn-danger btn-small">
                                                                                <i class="icon-trash"></i>
                                                                            </a>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>

                                                </div>
                                            </div>
                                            <div id="divDiscountTypeSuccess" runat="server" visible="false" class="alert alert-warning">
                                                <button data-dismiss="alert" class="close" type="button">
                                                                <i class="icon-remove"></i>
                                                            </button>
                                                Once your chosen discount subscription period is over, the following month you will be charged the regular monthly subscription rate as listed in our website.
                                                <br />
                                            </div>
                                        </div>

                                        <div class="row-fluid" runat="server" id="divPaymentDetails" visible="false">
                                            <br />
                                            <asp:UpdatePanel runat="server" ID="upnlPaymentCoupon">
                                                <ContentTemplate>

                                                    <asp:HiddenField runat="server" Value="" ID="hidCouponCode" />
                                                    <asp:HiddenField runat="server" Value="" ID="hidDisCountValue" />
                                                    <div class="row-fluid" runat="server" id="divAppyCouponCode" visible="false">
                                                        If you have any coupon code then please enter here<br />

                                                        <asp:TextBox runat="server" ID="txtCouponCode" CssClass="span6" AutoPostBack="true" OnTextChanged="txtCouponCode_TextChanged" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtCouponCode" ValidationGroup="ApplyCoupon"></asp:RequiredFieldValidator>
                                                        &nbsp;&nbsp;&nbsp;
            <asp:Button runat="server" ID="btnApplyCoupon" ValidationGroup="ApplyCoupon" OnClick="btnApplyCoupon_Click" Style="margin-top: -10px;" Text="Apply" CssClass="btn btn-success" />
                                                        <asp:Button runat="server" ID="btnClearDiscount" ValidationGroup="ApplyCoupon" OnClick="btnClearDiscount_Click" Style="margin-top: -10px;" Text="Clear Discount" CssClass="btn btn-warning" />
                                                        <br />
                                                       <%-- <small><span style="color: red;">*</span> If You have mutiple coupon code please enter the code separate by comma (,) </small>--%>
                                                        <br />
                                                        <div id="divCouponSuccess" runat="server" visible="false" class="alert alert-block alert-success span6">
                                                            <button data-dismiss="alert" class="close" type="button">
                                                                <i class="icon-remove"></i>
                                                            </button>
                                                            <p>
                                                                <i class="icon-ok"></i>
                                                                <asp:Label ID="lblCouponSuccess" runat="server"></asp:Label>
                                                            </p>
                                                        </div>
                                                        <div id="divCouponFailed" runat="server" visible="false" class="alert alert-error span6">
                                                            <button data-dismiss="alert" class="close" type="button">
                                                                <i class="icon-remove"></i>
                                                            </button>
                                                            <i class="icon-remove"></i>
                                                            <asp:Label ID="lblCouponFailed" runat="server"></asp:Label>
                                                        </div>
                                                        <h2>
                                                            <asp:Label runat="server" ID="lblTotalPrice" Text="" Style="float: right;"></asp:Label>
                                                        </h2>

                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <br />
                                            <table class="table table-bordered " runat="server" id="PaypalTable">
                                                <tr>
                                                    <td class="span3">Pay Amount Using</td>
                                                    <td class="span6">&nbsp;&nbsp;&nbsp;
                                                        <%--<asp:ImageButton CssClass="" ImageUrl="/img/authorizenet.png" runat="server" AlternateText="Pay With Authorize.net" ID="ibtnAuthNet" OnClick="ibtnAuthNet_Click" Width="100px" Height="60px" />--%>

                                                        <asp:ImageButton CssClass="" ImageUrl="/img/PPal.png" runat="server" AlternateText="Pay With PayPal" ID="ibtnPayPal" OnClick="ibtnPayPal_Click" Width="100px" Height="60px" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                            <asp:Panel runat="server" ID="pnlPaymentDetails" DefaultButton="btnPaymentSubmit">
                                                <div class="row-fluid">
                                                    <div id="divPaymentSuccess1" runat="server" visible="false" class="alert alert-success span12">
                                                        <button data-dismiss="alert" class="close" type="button">
                                                            <i class="icon-remove"></i>
                                                        </button>
                                                        <i class="icon-remove"></i>
                                                        <asp:Label ID="lblPaymentSuccess1" runat="server"></asp:Label>
                                                    </div>
                                                    <div id="divPaymentfailed1" runat="server" visible="false" class="alert alert-error span12">
                                                        <button data-dismiss="alert" class="close" type="button">
                                                            <i class="icon-remove"></i>
                                                        </button>
                                                        <i class="icon-remove"></i>
                                                        <asp:Label ID="lblPaymentFailed1" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                                    <ContentTemplate>
                                                        <!-- Credit Card Details -->
                                                        <div class="table-header">
                                                            Pay with Credit Card	        
                                                        </div>
                                                        <div class="dataTables_wrapper">
                                                            <table class="table table-hover dataTable table-bordered ">
                                                                <div runat="server" id="divExistingCardDetails" visible="true">
                                                                    <tr runat="server" id="trExistingCards" visible="false">
                                                                        <td class="span3">Registered Credit Cards</td>
                                                                        <td>
                                                                            <asp:DropDownList runat="server" ID="ddlExistingCards" AutoPostBack="true" OnSelectedIndexChanged="ddlExistingCards_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                </div>
                                                                <div runat="server" id="divNewCardDetails" visible="false">
                                                                    <div runat="server" id="divExistingCrdCardList">
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                <div class="alert alert-warning">
                                                                                    To update any information to your registered Credit Card contact admin@wftcloud.com or you can choose to<br />
                                                                                    add a new Credit Card with updated information by choosing New Credit Card option from the Registered Cards drop down menu.<br />
                                                                                    <br />
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="span3">Select Credit Card to be deleted from your profile </td>
                                                                            <td>

                                                                                <asp:RadioButtonList runat="server" ID="rblCreditCard" RepeatLayout="Flow">
                                                                                </asp:RadioButtonList>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="rblCreditCard" ValidationGroup="PaymentDetails"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>

                                                                    </div>
                                                                    <tr>
                                                                        <td class="span3">Credit Card Number</td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtCreditCardNumber" runat="server" AutoCompleteType="Disabled" autocomplete="off" AutoPostBack="True" OnTextChanged="txtCreditCardNumber_TextChanged"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="rfvLastName0" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtCreditCardNumber" ValidationGroup="PaymentDetails"></asp:RequiredFieldValidator>
                                                                            <asp:RegularExpressionValidator ID="valPassword" runat="server" ValidationGroup="PaymentDetails" ControlToValidate="txtCreditCardNumber" ErrorMessage="Invalid Card Number" ForeColor="Red" ValidationExpression="^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr runat="server" visible="false" id="trCardType">
                                                                        <td class="span3">Card Type
                                                                        </td>
                                                                        <td>
                                                                            <div>
                                                                                <div id="divValidCard" runat="server" visible="false" class="alert alert-block alert-success span5">
                                                                                    <p>
                                                                                        <i class="icon-ok"></i>
                                                                                        <asp:Label ID="lblCardType" runat="server"></asp:Label>
                                                                                    </p>
                                                                                </div>
                                                                                <div id="divInValidCard" runat="server" visible="True" class="alert alert-error span12">

                                                                                    <i class="icon-remove"></i>
                                                                                    <asp:Label ID="lblInvalidCard" runat="server" Text="Enter Valid Credit Card Number"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="span2">CVV Code</td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtVerifiCode" runat="server" TextMode="Password" Style="margin-left: 5px;" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtVerifiCode" ValidationGroup="PaymentDetails"></asp:RequiredFieldValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="span2">Expiration Date</td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlExpMonth" runat="server" Width="100px">
                                                                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                                                                <asp:ListItem Value="01">Jan</asp:ListItem>
                                                                                <asp:ListItem Value="02">Feb</asp:ListItem>
                                                                                <asp:ListItem Value="03">Mar</asp:ListItem>
                                                                                <asp:ListItem Value="04">Apr</asp:ListItem>
                                                                                <asp:ListItem Value="05">May</asp:ListItem>
                                                                                <asp:ListItem Value="06">Jun</asp:ListItem>
                                                                                <asp:ListItem Value="07">Jul</asp:ListItem>
                                                                                <asp:ListItem Value="08">Aug</asp:ListItem>
                                                                                <asp:ListItem Value="09">Sep</asp:ListItem>
                                                                                <asp:ListItem Value="10">Oct</asp:ListItem>
                                                                                <asp:ListItem Value="11">Nov</asp:ListItem>
                                                                                <asp:ListItem Value="12">Dec</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="rfv" runat="server" InitialValue="Select" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlExpMonth" ValidationGroup="PaymentDetails"></asp:RequiredFieldValidator>
                                                                            <asp:DropDownList ID="ddlExpYear" runat="server" Width="100px">
                                                                                <asp:ListItem>Select</asp:ListItem>
                                                                                <asp:ListItem>2013</asp:ListItem>
                                                                                <asp:ListItem>2014</asp:ListItem>
                                                                                <asp:ListItem>2015</asp:ListItem>
                                                                                <asp:ListItem>2016</asp:ListItem>
                                                                                <asp:ListItem>2017</asp:ListItem>
                                                                                <asp:ListItem>2018</asp:ListItem>
                                                                                <asp:ListItem>2019</asp:ListItem>
                                                                                <asp:ListItem>2020</asp:ListItem>
                                                                                <asp:ListItem>2021</asp:ListItem>
                                                                                <asp:ListItem>2022</asp:ListItem>
                                                                                <asp:ListItem>2023</asp:ListItem>
                                                                                <asp:ListItem>2024</asp:ListItem>
                                                                                <asp:ListItem>2025</asp:ListItem>
                                                                                <asp:ListItem>2026</asp:ListItem>
                                                                                <asp:ListItem>2027</asp:ListItem>
                                                                                <asp:ListItem>2028</asp:ListItem>
                                                                                <asp:ListItem>2029</asp:ListItem>
                                                                                <asp:ListItem>2030</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="rfvLastName1" runat="server" InitialValue="Select" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlExpYear" ValidationGroup="PaymentDetails"></asp:RequiredFieldValidator>
                                                                        </td>
                                                                    </tr>
                                                                </div>
                                                            </table>
                                                            <br />
                                                        </div>

                                                        <!-- Payment details-->
                                                        <div class="table-header">
                                                            Card Holder Details	        
                                                        </div>
                                                        <div class="dataTables_wrapper">
                                                            <table class="table table-hover dataTable table-bordered " id="tblChangePaymentDetails">
                                                                <tr>
                                                                    <td class="span3">Name On Card</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtNameOnCard" runat="server" AutoCompleteType="Disabled" autocomplete="off" MaxLength="30"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtNameOnCard" ValidationGroup="PaymentDetails"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="span2">Address1</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtAddress1" runat="server" TextMode="MultiLine" Style="margin-left: 5px;" AutoCompleteType="Disabled" autocomplete="off" MaxLength="30"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvAddress1" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="PaymentDetails" ControlToValidate="txtAddress1"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtAddress1" ErrorMessage="Address1 cannot exceed 30 characters in length" ForeColor="Red" ValidationExpression="^[\s\S]{0,30}$" ValidationGroup="PaymentDetails"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="span2">Address2(Optional)</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtAddress2" runat="server" TextMode="MultiLine" Style="margin-left: 5px;" AutoCompleteType="Disabled" autocomplete="off" MaxLength="30"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtAddress2" ErrorMessage="Address2 cannot exceed 30 characters in length" ForeColor="Red" ValidationExpression="^[\s\S]{0,30}$" ValidationGroup="PaymentDetails"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="span2">City</td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="txtCity" AutoCompleteType="Disabled" autocomplete="off" MaxLength="30"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvLastName2" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtCity" ValidationGroup="PaymentDetails"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <%--<asp:UpdatePanel runat="server" ID="upnlCountryDDLChanged" UpdateMode="Conditional">
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="ddlCountry" EventName="SelectedIndexChanged" />
                                                                    </Triggers>
                                                                    <ContentTemplate>--%>

                                                                <tr>
                                                                    <td class="span2">Country</td>
                                                                    <td>
                                                                        <asp:UpdatePanel ID="CountryUpdatePanel" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="ddlCountry" runat="server" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="true">
                                                                                </asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="rfvLastName4" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlCountry" ValidationGroup="PaymentDetails" InitialValue="0"></asp:RequiredFieldValidator>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>State</td>
                                                                    <td>
                                                                        <asp:UpdatePanel ID="StateUpdatePanel" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList runat="server" ID="ddlStateName"></asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlStateName" ValidationGroup="PaymentDetails" InitialValue="0"></asp:RequiredFieldValidator>
                                                                            </ContentTemplate>
                                                                            <Triggers>
                                                                                <asp:AsyncPostBackTrigger ControlID="ddlCountry" />
                                                                            </Triggers>
                                                                        </asp:UpdatePanel>
                                                                    </td>
                                                                </tr>
                                                                <%--  </ContentTemplate>
                                                                </asp:UpdatePanel>--%>
                                                                <tr>
                                                                    <td class="span2">Postal/Zip Code</td>
                                                                    <td>

                                                                        <asp:TextBox ID="txtPostalZipCode" runat="server" AutoCompleteType="Disabled" autocomplete="off" MaxLength="30"></asp:TextBox>

                                                                        <asp:RequiredFieldValidator ID="rfvAddress3" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="PaymentDetails" ControlToValidate="txtPostalZipCode"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="span2">Contact Number</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtContactNumberPtDet" runat="server" AutoCompleteType="Disabled" autocomplete="off" MaxLength="30"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvAddress2" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="PaymentDetails" ControlToValidate="txtContactNumberPtDet"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                            </table>

                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <table class="table table-hover dataTable table-bordered ">
                                                    <tr>
                                                        <td colspan="2">
                                                            <input type="checkbox" id="chkAgree" runat="server" />
                                                            &nbsp&nbsp&nbsp&nbsp<asp:ImageButton ID="Termscondition" runat="server" ImageUrl="~/img/TermsCondition.png" OnClientClick="return ShowModalPopup()" ToolTip="Click here to view Terms of Service" />
                                                        </td>
                                                        <div id="divRegisterError" runat="server" visible="false" class="alert alert-error ">
                                                            <i class="icon-remove"></i>
                                                            <asp:Label ID="lblRegisterError" Text="" runat="server" ForeColor="Red"></asp:Label>
                                                        </div>
                                                    </tr>
                                                    <tr>
                                                        <td class="span3"></td>
                                                        <td>
                                                            <asp:Button ID="btnPaymentSubmit" Text="Submit Payment" runat="server" OnClick="btnPaymentSubmit_Click" class="btn btn-primary" align="justify" ValidationGroup="PaymentDetails" />

                                                        </td>
                                                    </tr>

                                                </table>

                                            </asp:Panel>


                                        </div>
                                    </div>

                                    <div id="zOrderDetails" runat="server" visible="false">
                                        <h4>
                                            <asp:LinkButton runat="server" Text="&lt;&lt; Back" ID="lkbtnBakcToOrderDetails" OnClick="lkbtnBakcToOrderDetails_Click"></asp:LinkButton></h4>
                                        <div class="row-fluid">
                                            <div id="divPaymentSuccess" runat="server" visible="false" class="alert alert-block alert-success span6">
                                                <button data-dismiss="alert" class="close" type="button">
                                                    <i class="icon-remove"></i>
                                                </button>
                                                <p>
                                                    <i class="icon-ok"></i>
                                                    <asp:Label ID="lblPaymentSuccess" runat="server"></asp:Label>
                                                </p>
                                            </div>
                                            <div id="divPaymentfailed" runat="server" visible="false" class="alert alert-error span6">
                                                <button data-dismiss="alert" class="close" type="button">
                                                    <i class="icon-remove"></i>
                                                </button>
                                                <i class="icon-remove"></i>
                                                <asp:Label ID="lblPaymentFailed" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="table-header">
                                            <strong>Order Number :
                                            <asp:Label ID="lblOrderCode" runat="server"></asp:Label>
                                            </strong>
                                        </div>
                                        <table class="table table-striped table-bordered table-hover dataTable">
                                            <tr>
                                                <td class="span3">Order Date</td>
                                                <td>
                                                    <asp:Label ID="lblOrderDate" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Total Amount
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblOrdeAmount" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Coupon Used</td>
                                                <td>
                                                    <asp:Label ID="lblCouponUsed" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="trCouponCode" visible="false">
                                                <td>Coupon Code</td>
                                                <td>
                                                    <asp:Label ID="lblCouponCode" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="trDiscountedAmount" visible="false">
                                                <td>Discounted Amount</td>
                                                <td>
                                                    <asp:Label ID="lblDisCountAmount" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <div class="table-header">
                                            <strong>Order Details </strong>
                                        </div>
                                        <table class="table table-striped table-bordered table-hover dataTable" id="Table1">
                                            <thead>
                                                <tr role="row">
                                                    <th role="columnheader">Category Name</th>
                                                    <th role="columnheader">Service Name</th>
                                                    <th role="columnheader">Quantity</th>
                                                    <th role="columnheader">Order Total </th>
                                                </tr>
                                            </thead>
                                            <tbody role="alert">
                                                <asp:Repeater ID="rptrOrderDetails" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%# CategoryName(Eval("ServiceID").ToString()) %>
                                                                <asp:HiddenField ID="hidUserCartID" runat="server" Value='<%# Eval("UserOrderDetailID")%>' />
                                                            </td>
                                                            <td><%# ServiceName(Eval("ServiceID").ToString()) %>
                                                                <asp:HiddenField ID="hdnServiceID" runat="server" Value='<%# Eval("ServiceID")%>' />
                                                            </td>
                                                            <td><%#Eval("Quantity") %></td>
                                                            <td>$
                                                                <asp:Label runat="server" ID="lblPrice" Text='<%# Eval("InitialHoldAmount") %>'></asp:Label></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                        <br />
                                        <div class="table-header">
                                            <strong>Payment Transaction Details
                                            </strong>
                                        </div>
                                        <table class="table table-striped table-bordered table-hover dataTable">
                                            <tr>
                                                <td class="span3">Invoice Number</td>
                                                <td>
                                                    <asp:Label ID="lblInvoiceNumber" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Payment Date</td>
                                                <td>
                                                    <asp:Label ID="lblPaymentDate" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Total Amount 
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPaymentAmount" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Credit Card Number </td>
                                                <td>
                                                    <asp:Label ID="lblCreditCardnumber" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Payment Status</td>
                                                <td>
                                                    <asp:Label ID="lblAmountPaid" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Message</td>
                                                <td>
                                                    <asp:Label ID="lblPaymentMessage" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

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
                                <asp:Button ID="btnCancelServices" runat="server" Style="float: right;" CssClass="btn btn-danger btn-small" Text="Cancel Subscription" OnClick="btnCancelServices_Click" ValidationGroup="CancelServices" />
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




        <asp:ModalPopupExtender ID="mpopupTermsCondition" runat="server" TargetControlID="hfTermsCancel" BackgroundCssClass="modalBackground" PopupControlID="divTermsAndCondition" CancelControlID="lblTermsBack"></asp:ModalPopupExtender>
        <asp:HiddenField ID="hfTermsCancel" runat="server" />
        <div id="divTermsAndCondition" runat="server">
            <asp:Panel runat="server" ID="Panel1" Width="900px">
                <div id="Div2" class="login-box visible widget-box no-border">
                    <div class="widget-body">

                        <div class="widget-main">
                            <h4 class="header blue lighter bigger">
                                <i class="icon-coffee green"></i>

                                WFT Cloud Payment Terms:</h4>
                            <p style="text-align: justify">
                                1: By beginning your subscription, you authorize WFT Cloud to charge you a monthly fee at the designated rate.<br />
                                <br />

                                2: Your can manage your subscription through WFT Cloud website by logging to your account.  Your subscription will remain active unless you choose to terminate your service.  Any email requests or other forms of communication for cancellation will neither be honoured nor acknowledged as an acceptance of cancellation.<br />
                                <br />

                                3: Your account will be charged on the day the order is processed and the next payment will deducted in 30days from the first order is processed
                                <br />
                                <br />

                                4: WFT Cloud will not be held responsible for the cancellation of the services managed by customers.<br />
                                <br />

                                5: WFT Cloud will not provide any <b>refund</b> if the customer fails to cancel their services through the WFT Cloud website before the next billing cycle.<br />
                                <br />

                                6: There are no refunds for partial used periods. Following any cancellation; however, you will continue to have access to the service through the end of your current period.
                                <br />
                                <br />

                                7: WFT has the rights to suspend your account any time incase of payment failure for the subscription
                            </p>

                            <div class="space-6"></div>





                            <div class="space-4"></div>


                        </div>
                        <!--/widget-main-->
                        <div class="toolbar clearfix">
                            <div>
                                <asp:LinkButton ID="lblTermsBack" runat="server" class="forgot-password-link"><i class="icon-arrow-left"></i>
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
            $('#tblSubscribedPackages').dataTable(
                {
                    "aoColumnDefs": [
                        { 'bSortable': false, 'aTargets': [7] }
                    ]
                }
        );
        });
        jQuery(function ($) {
            $('#tblMyCart').dataTable(
                {
                    "aoColumnDefs": [
                        { 'bSortable': false, 'aTargets': [2, 7] }
                    ]
                }
            );
        });
    </script>
    <script type="text/javascript">
        function ConfirmOnDelete() {
            if (confirm("Are you sure want to cancel this service?") == true)
                return true;
            else
                return false;
        }
        function ConfirmOnDeleteCart() {
            if (confirm("Are you sure want to delete this service from your cart?") == true)
                return true;
            else
                return false;
        }
    </script>
</asp:Content>

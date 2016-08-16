<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminPages.Master" AutoEventWireup="true" CodeBehind="WFTReports.aspx.cs" Inherits="WFTCloud.Admin.Reports1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField runat="server" ID="hidFromDate" />
    <asp:HiddenField runat="server" ID="hidToDate" />
    <asp:HiddenField runat="server" ID="hidReportType" />
    <div class="table-header">
        <strong>Generate Reports</strong>
    </div>
    <table class="table table-hover dataTable table-bordered ">
        <tr>
            <td class="span2">Select Report Type</td>
            <td>
                <asp:DropDownList ID="ddlReportType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged">
                    <asp:ListItem Value="0">-Select Report-      </asp:ListItem>
                    <asp:ListItem Value="1">All Payments</asp:ListItem>
                    <asp:ListItem Value="2">Successful Payments Report</asp:ListItem>
                    <asp:ListItem Value="3">Failed Payments Report</asp:ListItem>
                    <asp:ListItem Value="4">New User Signup Report</asp:ListItem>
                    <%--<asp:ListItem>Free User Who Purchased Service Report</asp:ListItem>--%>
                    <asp:ListItem Value="5">User with no services Report</asp:ListItem>
                    <asp:ListItem Value="6">User Report by Expiry Date</asp:ListItem>
                    <asp:ListItem Value="7">User Report - Trial Services</asp:ListItem>
                    <asp:ListItem Value="8">User Report - Dedicated Services</asp:ListItem>
                    <asp:ListItem Value="9">How did you hear about us</asp:ListItem>
                    <asp:ListItem Value="10">User Order Details Failed On Purchase</asp:ListItem>
                    <asp:ListItem Value="11">WFTCloud.com Newsletter Subscribers List</asp:ListItem>
                    <asp:ListItem Value="12">WFTCloud Org Newsletter Subscribers List</asp:ListItem>
                    <asp:ListItem Value="13">New Service Registration Report</asp:ListItem>
                    <asp:ListItem Value="14">Training Request Report</asp:ListItem>
                    <asp:ListItem Value="15">Monthly Failed Payments Report</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ID="rfvReportType" InitialValue="0" ControlToValidate="ddlReportType" ForeColor="Red"
                    ErrorMessage="Please select Report Type" ValidationGroup="Reports"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <div runat="server" id="dvDate" visible="true">
            <tr>
                <td class="span2">Range From

                </td>
                <td>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:TextBox ID="txtFromDate" runat="server" ValidationGroup="Datecompare"></asp:TextBox>
                    <asp:CalendarExtender ID="cerange" runat="server" TargetControlID="txtFromDate" Format="dd-MMM-yyyy">
                    </asp:CalendarExtender>
                    <asp:RequiredFieldValidator runat="server" ID="rfvRangeFrom" ControlToValidate="txtFromDate" ForeColor="Red"
                        ErrorMessage="*" ValidationGroup="Reports"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="span2">Range To</td>
                <td>
                    <asp:TextBox ID="txtToDate" runat="server"></asp:TextBox>
                    <asp:CalendarExtender ID="TextBox2_CalendarExtender" runat="server" TargetControlID="txtToDate" Format="dd-MMM-yyyy">
                    </asp:CalendarExtender>
                    <asp:RequiredFieldValidator runat="server" ID="rfvRangeTo" ControlToValidate="txtToDate" ForeColor="Red"
                        ErrorMessage="*" ValidationGroup="Reports"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </div>
        <tr>
            <td class="span2"></td>

            <td>
                <asp:Button ID="btn_Generate" runat="server" Text="Generate" CssClass="btn btn-primary" ValidationGroup="Reports" BackColor="#3459FC" OnClick="btn_Generate_Click" />
                <asp:Button runat="server" ID="btnExportToExcel" CssClass="btn btn-danger" Text="Export To Excel" OnClick="btnExportToExcel_Click" Visible="False" />
            </td>
        </tr>
    </table>
    <br />

    <asp:MultiView ID="mvContainer" runat="server">
        <asp:View ID="ReportsOrderView" runat="server">
            <div class="table-header">
                <strong>Payment History Details&nbsp;
                    <asp:Label runat="server" ID="lblPaymentLabel"></asp:Label></strong>
            </div>
            <div class="row-fluid">
                <div class="span12 widget-container-span">
                    <div class="widget-box">
                        <div class="widget-body">
                            <div class="widget-main padding-12">
                                <div class="tab-content">
                                    <div id="Manage" class="tab-pane in active" runat="server">
                                        <asp:MultiView ID="mvOrderDetails" runat="server" ActiveViewIndex="0">
                                            <asp:View ID="vwOrderHistroy" runat="server">
                                                <div class="row-fluid">
                                                    <div class="span12">
                                                        <div class="row-fluid">
                                                            <div role="grid" class="dataTables_wrapper">
                                                                <div role="grid" class="dataTables_wrapper">
                                                                    <asp:Repeater ID="rptrPaymentHistroy" runat="server">
                                                                        <HeaderTemplate>
                                                                            <table id="tblUsersList" class="table table-striped table-bordered table-hover dataTable">
                                                                                <tr role="row">
                                                                                    <th role="columnheader">Invoice Number</th>
                                                                                    <th role="columnheader">Payment Date</th>
                                                                                    <th role="columnheader">Amount</th>
                                                                                    <th role="columnheader">Status</th>
                                                                                    <th role="columnheader">User EmailID</th>
                                                                                </tr>
                                                                                <tbody role="alert">
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td><%# Eval("InvoiceNumber")%></td>
                                                                                <td><%#string.Format("{0:dd-MMM-yyyy}", Eval("PaymentDateTime")).ToUpper()%></td>
                                                                                <td>$ <%# Eval("Amount")%></td>
                                                                                <td><%# (Eval("Approved") == null || Eval("Approved").ToString() == "False" )? "Failed" :"Success" %></td>
                                                                                <td>
                                                                                    <a href='mailto:<%# UserEmailID(Eval("UserProfileID").ToString()) %>'>
                                                                                        <asp:Label ID="UserMailID" runat="server" Text='<%# UserEmailID(Eval("UserProfileID").ToString()) %>'></asp:Label></a>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            </tbody>
                                                    </table>
                                                                        </FooterTemplate>
                                                                    </asp:Repeater>
                                                                </div>
                                                            </div>
                                                            <br />
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:View>
                                            <asp:View ID="vwNoOrderHistroy" runat="server">
                                                <div class="alert alert-warning" id="divNoCrmIssue" runat="server">
                                                    <i class="icon-remove"></i><strong>No Payments found within the selected date interval.</strong>
                                                </div>
                                            </asp:View>
                                        </asp:MultiView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:View>
        <asp:View ID="vwUserDetails" runat="server">
            <div class="table-header">
                <strong>User Details
                    <asp:Label runat="server" ID="lblUserDEtails"></asp:Label></strong>
            </div>
            <div class="row-fluid">
                <div class="span12 widget-container-span">
                    <div class="widget-box">
                        <div class="widget-body">
                            <div class="widget-main padding-12">
                                <div class="tab-content">
                                    <asp:MultiView runat="server" ID="mvUserDetails" ActiveViewIndex="0">
                                        <asp:View runat="server" ID="vwUserrptr">
                                            <div class="row-fluid">
                                                <div class="span12">
                                                    <div class="row-fluid">

                                                        <div role="grid" class="dataTables_wrapper">
                                                            <div role="grid" class="dataTables_wrapper">

                                                                <asp:Repeater ID="rptrUsersList" runat="server">
                                                                    <HeaderTemplate>
                                                                        <table id="tblUsersList" class="table table-striped table-bordered table-hover dataTable">
                                                                            <thead>
                                                                                <tr role="row">
                                                                                    <th role="columnheader">User Name</th>
                                                                                    <th role="columnheader">Email</th>
                                                                                    <th role="columnheader">Registration Date and Time</th>
                                                                                    <th role="columnheader">Status</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody role="alert">
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <%# Eval("FirstName")%> <%# Eval("MiddleName")%> <%# Eval("LastName")%>
                                                                            </td>
                                                                            <td><a href='mailto:<%#Eval("EmailID") %>'>
                                                                                <asp:Label ID="UserMailID" runat="server" Text='<%# Eval("EmailID")%>'></asp:Label></a></td>
                                                                            <td><%# Eval("CreatedOn")%></td>
                                                                            <td><%# WFTCloud.GeneralReusableMethods.GetStatusString(Eval("RecordStatus").ToString()) %></td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        </tbody>
                                                        </table>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                                <asp:Repeater ID="rptrWithNoSubScribedServices" runat="server">
                                                                    <HeaderTemplate>
                                                                        <table id="tblUsersList1" class="table table-striped table-bordered table-hover dataTable">
                                                                            <thead>
                                                                                <tr role="row">
                                                                                    <th role="columnheader">User Name</th>
                                                                                    <th role="columnheader">Email</th>
                                                                                    <th role="columnheader">Registration Date and Time</th>
                                                                                    <th role="columnheader">Status</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody role="alert">
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <%# Eval("FirstName")%> <%# Eval("MiddleName")%> <%# Eval("LastName")%>
                                                                            </td>
                                                                            <td><a href='mailto:<%#Eval("EmailID") %>'>
                                                                                <asp:Label ID="UserMailID" runat="server" Text='<%# Eval("EmailID")%>'></asp:Label></a></td>
                                                                            <td><%# Eval("CreatedOn")%></td>
                                                                            <td><%# WFTCloud.GeneralReusableMethods.GetStatusString(Eval("RecordStatus").ToString()) %></td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        </tbody>
                                        </table>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:View>
                                        <asp:View runat="server" ID="vwNoUserDetailsFound">
                                            <div class="alert alert-warning" id="div1" runat="server">
                                                <i class="icon-remove"></i><strong>No User Details found within the selected date interval.</strong>
                                            </div>
                                        </asp:View>
                                    </asp:MultiView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:View>
        <asp:View ID="vwServiceUserDetails" runat="server">
            <div class="table-header">
                <strong>User Details
                    <asp:Label runat="server" ID="lblServiceUserNameTitle"></asp:Label></strong>
            </div>
            <div class="row-fluid">
                <div class="span12 widget-container-span">
                    <div class="widget-box">
                        <div class="widget-body">
                            <div class="widget-main padding-12">
                                <div class="tab-content">
                                    <asp:MultiView runat="server" ID="mvServiceUserDEtails" ActiveViewIndex="0">
                                        <asp:View runat="server" ID="vwServUserDetails">
                                            <div class="row-fluid">
                                                <div class="span12">
                                                    <div class="row-fluid">

                                                        <div role="grid" class="dataTables_wrapper">
                                                            <div role="grid" class="dataTables_wrapper">

                                                                <asp:Repeater ID="rptrServiceUserDetails" runat="server">
                                                                    <HeaderTemplate>
                                                                        <table id="tblServiceCate" class="table table-striped table-bordered table-hover dataTable">
                                                                            <thead>
                                                                                <tr role="row">
                                                                                    <th role="columnheader">Category Name</th>
                                                                                    <th role="columnheader">Service Name</th>
                                                                                    <th role="columnheader">Subscribed Date</th>
                                                                                    <th role="columnheader">Expiration Date</th>
                                                                                    <th role="columnheader">User Name</th>
                                                                                    <th role="columnheader">Email</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody role="alert">
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td><%# Eval("CategoryName")%></td>
                                                                            <td><%# Eval("ServiceName") %></td>
                                                                            <td><%#Eval("ServiceCreatedOn")!=null? Eval("ServiceCreatedOn","{0:dd-MMM-yyyy}"):"-"%></td>
                                                                            <td><%#Eval("ExpirationDate")!=null? Eval("ExpirationDate","{0:dd-MMM-yyyy}"):"-"%></td>
                                                                            <td>
                                                                                <%# Eval("FirstName")%> <%# Eval("MiddleName")%> <%# Eval("LastName")%></td>
                                                                            <td><a href='mailto:<%#Eval("EmailID") %>'>
                                                                                <asp:Label ID="UserMailID" runat="server" Text='<%# Eval("EmailID")%>'></asp:Label></a></td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        </tbody>
                                        </table>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </asp:View>
                                        <asp:View runat="server" ID="vwNoUserDeatils">
                                            <div class="alert alert-warning" id="div2" runat="server">
                                                <i class="icon-remove"></i><strong>No User Details found within the selected date interval.</strong>
                                            </div>
                                        </asp:View>
                                    </asp:MultiView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:View>
        <asp:View ID="View1" runat="server">
            <div class="table-header">
                <strong>User Details - How did you hear about us</strong>
            </div>
            <div class="row-fluid">
                <div class="span12 widget-container-span">
                    <div class="widget-box">
                        <div class="widget-body">
                            <div class="widget-main padding-12">
                                <div class="tab-content">
                                    <asp:MultiView runat="server" ID="mvHearAboutUs" ActiveViewIndex="0">
                                        <asp:View runat="server" ID="View2">
                                            <div class="row-fluid">
                                                <div class="span12">
                                                    <div class="row-fluid">
                                                        <div role="grid" class="dataTables_wrapper">
                                                            <div role="grid" class="dataTables_wrapper">
                                                                <asp:Repeater ID="rptrHearAboutUs" runat="server">
                                                                    <HeaderTemplate>
                                                                        <table id="tblUsersList" class="table table-striped table-bordered table-hover dataTable">
                                                                            <thead>
                                                                                <tr role="row">
                                                                                    <th role="columnheader">User Name</th>
                                                                                    <th role="columnheader">Email</th>
                                                                                    <th role="columnheader">Heard about us via</th>
                                                                                    <%--<th role="columnheader">Others</th>--%>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody role="alert">
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <%# Eval("FirstName")%> <%# Eval("LastName")%>
                                                                            </td>
                                                                            <td><a href='mailto:<%#Eval("EmailID") %>'>
                                                                                <asp:Label ID="UserMailID" runat="server" Text='<%# Eval("EmailID")%>'></asp:Label></a></td>
                                                                            <%--<td><%# Eval("SurveyAnswer")%></td>--%>
                                                                            <td><%# Eval("SurveyAnswer")!=null?(Eval("SurveyAnswer")!=""?Eval("SurveyAnswer"):" - "):" - "%></td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        </tbody>
                                        </table>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:View>
                                        <asp:View runat="server" ID="View3">
                                            <div class="alert alert-warning" id="div3" runat="server">
                                                <i class="icon-remove"></i><strong>No Details found within the selected date interval.</strong>
                                            </div>
                                        </asp:View>
                                    </asp:MultiView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:View>
        <asp:View ID="vwUserOrderDetailsFailedOnPurchase" runat="server">
            <div class="table-header">
                <strong>User Order Details Failed On Purchase</strong>
            </div>
            <div class="row-fluid">
                <div class="span12 widget-container-span">
                    <div class="widget-box">
                        <div class="widget-body">
                            <div class="widget-main padding-12">
                                <div class="tab-content">
                                    <asp:MultiView runat="server" ID="mvOrderDetailsFailedOnPurchase" ActiveViewIndex="0">
                                        <asp:View runat="server" ID="View5">
                                            <div class="row-fluid">
                                                <div class="span12">
                                                    <div class="row-fluid">
                                                        <div role="grid" class="dataTables_wrapper">
                                                            <div role="grid" class="dataTables_wrapper">
                                                                <asp:Repeater ID="rptrOrderDetailsFailedOnPurchase" runat="server">
                                                                    <HeaderTemplate>
                                                                        <table id="tblOrderDetailsFailedOnPurchase" class="table table-striped table-bordered table-hover dataTable">
                                                                            <thead>
                                                                                <tr role="row">
                                                                                    <th role="columnheader">Order Code</th>
                                                                                    <th role="columnheader">Failed DateTime</th>
                                                                                    <th role="columnheader">Category Name</th>
                                                                                    <th role="columnheader">Serivce Name</th>
                                                                                    <th role="columnheader">User Name</th>
                                                                                    <th role="columnheader">EmailID</th>
                                                                                    <th role="columnheader">Phone No</th>
                                                                                    <th role="columnheader">Mobile No</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody role="alert">
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>Order<%# Eval("UserOrderID")%></td>
                                                                            <td><%#string.Format("{0:dd-MMM-yyyy}", Eval("OrderDateTime")).ToUpper()%></td>
                                                                            <td><%# CategoryName(Eval("ServiceID").ToString())%></td>
                                                                            <td><%# ServiceName(Eval("ServiceID").ToString())%></td>
                                                                            <td><%# Eval("FirstName")%> <%# Eval("MiddleName")%> <%# Eval("LastName")%></td>
                                                                            <td><a href='mailto:<%#Eval("EmailID") %>'>
                                                                                <asp:Label ID="UserMailID" runat="server" Text='<%# Eval("EmailID")%>'></asp:Label></a></td>
                                                                            <td><%#Eval("PhoneNumber") %></td>
                                                                            <td><%# Eval("MobileNumber")%></td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        </tbody>
                                        </table>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:View>
                                        <asp:View runat="server" ID="View6">
                                            <div class="alert alert-warning" id="div4" runat="server">
                                                <i class="icon-remove"></i><strong>No Details found within the selected date interval.</strong>
                                            </div>
                                        </asp:View>
                                    </asp:MultiView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:View>
        <asp:View ID="VwNewsLetterSubscriberLIst" runat="server">
            <div class="table-header">
                <strong>WFTCloud.Com NewsLetter Subscribers List </strong>
            </div>
            <div class="row-fluid">
                <div class="span12 widget-container-span">
                    <div class="widget-box">
                        <div class="widget-body">
                            <div class="widget-main padding-12">
                                <div class="tab-content">
                                    <asp:MultiView runat="server" ID="mvNewsLetterInnverMV" ActiveViewIndex="0">
                                        <asp:View runat="server" ID="vwNewsLetter">
                                            <div class="row-fluid">
                                                <div class="span12">
                                                    <div class="row-fluid">
                                                        <div role="grid" class="dataTables_wrapper">
                                                            <div role="grid" class="dataTables_wrapper">
                                                                <asp:Repeater ID="rptrSubscriberList" runat="server">
                                                                    <HeaderTemplate>
                                                                        <table id="tblUsersList" class="table table-striped table-bordered table-hover dataTable">
                                                                            <thead>
                                                                                <tr role="row">
                                                                                    <th role="columnheader">First Name</th>
                                                                                    <th role="columnheader">Last Name</th>
                                                                                    <th role="columnheader">Email</th>
                                                                                    <th role="columnheader">Date Signed Up</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody role="alert">
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td><%#Eval("FirstName")%></td>
                                                                            <td><%#Eval("LastName")%></td>
                                                                            <td><a href='mailto:<%#Eval("EmailID") %>'>
                                                                                <asp:Label ID="UserMailID" runat="server" Text='<%# Eval("EmailID")%>'></asp:Label></a>
                                                                            </td>
                                                                            <td><%#Eval("DateSignedUp")!=null? Eval("DateSignedUp","{0:dd-MMM-yyyy}"):"-"%></td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        </tbody>
                                        </table>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:View>
                                        <asp:View runat="server" ID="vwNoNewsLetter">
                                            <div class="alert alert-warning" id="div5" runat="server">
                                                <i class="icon-remove"></i><strong>No Details found.</strong>
                                            </div>
                                        </asp:View>
                                    </asp:MultiView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:View>
        <asp:View ID="VwOrgNewsLetterSubscriberLIst" runat="server">
            <div class="table-header">
                <strong>WFTCloud Org NewsLetter Subscribers List </strong>
            </div>
            <div class="row-fluid">
                <div class="span12 widget-container-span">
                    <div class="widget-box">
                        <div class="widget-body">
                            <div class="widget-main padding-12">
                                <div class="tab-content">
                                    <asp:MultiView runat="server" ID="mvOrgNewsLetterInnverMV" ActiveViewIndex="0">
                                        <asp:View runat="server" ID="vwOrgNewsLetter">
                                            <div class="row-fluid">
                                                <div class="span12">
                                                    <div class="row-fluid">
                                                        <div role="grid" class="dataTables_wrapper">
                                                            <div role="grid" class="dataTables_wrapper">
                                                                <asp:Repeater ID="rptrOrgSubscriberList" runat="server">
                                                                    <HeaderTemplate>
                                                                        <table id="tblUsersList" class="table table-striped table-bordered table-hover dataTable">
                                                                            <thead>
                                                                                <tr role="row">
                                                                                    <th role="columnheader">Email</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody role="alert">
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td><a href='mailto:<%#Eval("Email") %>'>
                                                                                <asp:Label ID="UserMailID" runat="server" Text='<%# Eval("Email")%>'></asp:Label></a>
                                                                            </td>

                                                                        </tr>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        </tbody>
                                        </table>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:View>
                                        <asp:View runat="server" ID="vwOrgNoNewsLetter">
                                            <div class="alert alert-warning" id="div6" runat="server">
                                                <i class="icon-remove"></i><strong>No Details found.</strong>
                                            </div>
                                        </asp:View>
                                    </asp:MultiView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:View>

        <asp:View ID="VwNewServicePurchasedList" runat="server">
            <div class="table-header">
                <strong>New Service Purchased from
                    <asp:Label runat="server" ID="lblNewServicePurchasedTitle"></asp:Label></strong>
            </div>
            <div class="row-fluid">
                <div class="span12 widget-container-span">
                    <div class="widget-box">
                        <div class="widget-body">
                            <div class="widget-main padding-12">
                                <div class="tab-content">
                                    <asp:MultiView runat="server" ID="mvNewServicePurchasedDetails" ActiveViewIndex="0">
                                        <asp:View runat="server" ID="vwNewServicePurchasedDetails">
                                            <div class="row-fluid">
                                                <div class="span12">
                                                    <div class="row-fluid">

                                                        <div role="grid" class="dataTables_wrapper">
                                                            <div role="grid" class="dataTables_wrapper">

                                                                <asp:Repeater ID="rptrNewServicePurchasedDetails" runat="server">
                                                                    <HeaderTemplate>
                                                                        <table id="tblServiceCate" class="table table-striped table-bordered table-hover dataTable">
                                                                            <thead>
                                                                                <tr role="row">
                                                                                    <th role="columnheader">User Name</th>
                                                                                    <th role="columnheader">Email ID</th>
                                                                                    <th role="columnheader">Service Name</th>
                                                                                    <th role="columnheader">Category Name</th>
                                                                                    <th role="columnheader">UserSubscriptionID</th>
                                                                                    <th role="columnheader">Active Date</th>
                                                                                    <th role="columnheader">Amount</th>
                                                                                    <th role="columnheader">Status</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody role="alert">
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td><%# Eval("UserName")%></td>
                                                                            <td><%# Eval("EmailID")%></td>
                                                                            <td><%# Eval("ServiceName")%></td>
                                                                            <td><%# Eval("CategoryName") %></td>
                                                                            <td><%# Eval("UserSubscriptionID") %></td>
                                                                            <td><%#Eval("ActiveDate")!=null? Eval("ActiveDate","{0:dd-MMM-yyyy}"):"-"%></td>
                                                                            <td><%# Eval("Amount")%></td>
                                                                            <td><%# Eval("Status")%></td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        </tbody>
                                        </table>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </asp:View>
                                        <asp:View runat="server" ID="View8">
                                            <div class="alert alert-warning" id="div7" runat="server">
                                                <i class="icon-remove"></i><strong>No User Service Details found within the selected date interval.</strong>
                                            </div>
                                        </asp:View>
                                    </asp:MultiView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:View>

        <asp:View ID="View4" runat="server">
            <div class="table-header">
                <strong>Training Request from
                    <asp:Label runat="server" ID="lblTrainingRequestTitle"></asp:Label></strong>
            </div>
            <div class="row-fluid">
                <div class="span12 widget-container-span">
                    <div class="widget-box">
                        <div class="widget-body">
                            <div class="widget-main padding-12">
                                <div class="tab-content">
                                    <asp:MultiView runat="server" ID="mvTrainingRequestDetails" ActiveViewIndex="0">
                                        <asp:View runat="server" ID="vwTrainingRequestDetails">
                                            <div class="row-fluid">
                                                <div class="span12">
                                                    <div class="row-fluid">

                                                        <div role="grid" class="dataTables_wrapper">
                                                            <div role="grid" class="dataTables_wrapper">

                                                                <asp:Repeater ID="rptrTrainingRequestDetails" runat="server">
                                                                    <HeaderTemplate>
                                                                        <table id="tblServiceCate" class="table table-striped table-bordered table-hover dataTable">
                                                                            <thead>
                                                                                <tr role="row">
                                                                                    <th role="columnheader">User Name</th>
                                                                                    <th role="columnheader">Email ID</th>
                                                                                    <th role="columnheader">Phone No</th>
                                                                                    <th role="columnheader">Location</th>
                                                                                    <th role="columnheader">Survey Answer</th>
                                                                                    <th role="columnheader">Course</th>
                                                                                    <th role="columnheader">Module</th>
                                                                                    <th role="columnheader">Comments</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody role="alert">
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td><%# Eval("UserName")%></td>
                                                                            <td><%# Eval("Email")%></td>
                                                                            <td><%# Eval("PhoneNumber")%></td>
                                                                            <td><%# Eval("Location") %></td>
                                                                            <td><%# Eval("SurveyAnswer") %></td>
                                                                            <td><%#Eval("Course")%></td>
                                                                            <td><%# Eval("Module")%></td>
                                                                            <td><%# Eval("Comments")%></td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        </tbody>
                                        </table>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </asp:View>
                                        <asp:View runat="server" ID="View9">
                                            <div class="alert alert-warning" id="div8" runat="server">
                                                <i class="icon-remove"></i><strong>No User Service Details found within the selected date interval.</strong>
                                            </div>
                                        </asp:View>
                                    </asp:MultiView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:View>


           <asp:View ID="VwFailedPaymentList" runat="server">
            <div class="table-header">
                <strong>Failed Payments Report from
                    <asp:Label runat="server" ID="lblFailedPaymentsReport"></asp:Label></strong>
            </div>
            <div class="row-fluid">
                <div class="span12 widget-container-span">
                    <div class="widget-box">
                        <div class="widget-body">
                            <div class="widget-main padding-12">
                                <div class="tab-content">
                                    <asp:MultiView runat="server" ID="mvFailedPaymentsDetails" ActiveViewIndex="0">
                                        <asp:View runat="server" ID="vwFailedPaymentsDetails">
                                            <div class="row-fluid">
                                                <div class="span12">
                                                    <div class="row-fluid">

                                                        <div role="grid" class="dataTables_wrapper">
                                                            <div role="grid" class="dataTables_wrapper">

                                                                <asp:Repeater ID="rptrFailedPaymentsDetails" runat="server">
                                                                    <HeaderTemplate>
                                                                        <table id="tblServiceCate" class="table table-striped table-bordered table-hover dataTable">
                                                                            <thead>
                                                                                <tr role="row">
                                                                                    <th role="columnheader">UserSubscription ID</th>
                                                                                    <th role="columnheader">Active Date</th>
                                                                                    <th role="columnheader">Payment Date</th>
                                                                                    <th role="columnheader">Payment Status</th>
                                                                                    <th role="columnheader">Payment Amount</th>
                                                                                    <th role="columnheader">Transaction ID</th>
                                                                                    <th role="columnheader">Invoice Number</th>
                                                                                    <th role="columnheader">Payment Method</th>
                                                                                    <th role="columnheader">SAP UserName</th>
                                                                                    <th role="columnheader">Last Logon Date</th>
                                                                                    <th role="columnheader">Cancelled Date</th>
                                                                                    <th role="columnheader">Actual Charge</th>
                                                                                    <th role="columnheader">Locked Date</th>
                                                                                    <th role="columnheader">UnSubscribed Date</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody role="alert">
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td><%# Eval("UserSubscriptionID")%></td>
                                                                            <td><%#string.Format("{0:dd-MMM-yyyy}", Eval("ActiveDate")).ToUpper()%></td>
                                                                            <td><%#string.Format("{0:dd-MMM-yyyy}", Eval("PaymentDate")).ToUpper()%></td>
                                                                            <td><%# Eval("PaymentStatus") %></td>
                                                                            <td><%# Eval("PaymentAmount") %></td>
                                                                            <td><%#Eval("TransactionID")%></td>
                                                                            <td><%# Eval("InvoiceNumber")%></td>
                                                                            <td><%# Eval("PaymentMethod")%></td>
                                                                            <td><%# Eval("SAPUserName")%></td>
                                                                            <td><%# Eval("LastLogonDate")%></td>
                                                                            <td><%# Eval("CancelledDate")%></td>
                                                                            <td><%# Eval("ActualCharge")%></td>
                                                                            <td><%# Eval("LockedDate")%></td>
                                                                            <td><%# Eval("UnSubscribedDate")%></td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        </tbody>
                                        </table>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </asp:View>
                                        <asp:View runat="server" ID="View11">
                                            <div class="alert alert-warning" id="div9" runat="server">
                                                <i class="icon-remove"></i><strong>No User Service Details found within the selected date interval.</strong>
                                            </div>
                                        </asp:View>
                                    </asp:MultiView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:View>

    </asp:MultiView>
    <script type="text/javascript">
        jQuery(function ($) {
            $('#tblPaymentHistroy1').dataTable();
        });
        jQuery(function ($) {
            $('#tblServiceCate').dataTable();
        });
        jQuery(function ($) {
            $('#tblUsersList').dataTable();
        });
        jQuery(function ($) {
            $('#tblUsersList1').dataTable();
        });
        jQuery(function ($) {
            $('#tblOrderDetailsFailedOnPurchase').dataTable();
        });
    </script>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminPages.Master" AutoEventWireup="true" CodeBehind="UserFullHistory.aspx.cs" Inherits="WFTCloud.Admin.UserFullHistory" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>User List</title>
    <meta name="description" content="List of Users can be seen here and we have an options of Check services, Add new services, Access acount, Delete User, Manage CRM issue and View User Profile" />
    <meta name="keywords" content="WFT Users list" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField runat="server" ID="hidReportType" />
    <div class="row-fluid">
        <div class="span12 widget-container-span">
            <div class="widget-box">
                <div class="widget-header no-border">
                    <ul class="nav nav-tabs" id="Ul1">
                        <li id="liManage" runat="server" class="active">
                            <a>Subscription Histroy</a>
                        </li>
                    </ul>
                </div>
                <div class="widget-body">
                    <div class="widget-main padding-12">
                        <div class="tab-content">
                            <div id="Manage" class="tab-pane in active" runat="server">
                                <asp:MultiView ID="mvSubscriptionDetails" runat="server" ActiveViewIndex="1">
                                    <asp:View ID="vwSubscriptionHistroy" runat="server">
                                        <div class="row-fluid">
                                        </div>
                                    </asp:View>
                                    <asp:View ID="vmUserPaymentHistoryDetails" runat="server">
                                        <h4><a href='/Admin/UsersServiceHistory.aspx'><< Back</a></h4>
                                        <div class="table-header">
                                            <strong>Service details of
                                            <asp:Label ID="lblUserNamefor" runat="server"></asp:Label>
                                            </strong>
                                        </div>
                                        <table class="table table-striped table-bordered table-hover dataTable">
                                            <tr>
                                                <td>User Name</td>
                                                <td>
                                                    <asp:Label ID="lblUserName" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Company Name</td>
                                                <td>
                                                    <asp:Label ID="lblCompanyName" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width:20%;">Contact Address</td>
                                                <td>
                                                    <asp:Label ID="lblContactAddress" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Contact Number</td>
                                                <td>
                                                    <asp:Label ID="lblContactNumber" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Location</td>
                                                <td>
                                                    <asp:Label ID="lblLocation" runat="server"></asp:Label>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td>Download All Payment Details</td>
                                                <td>
                                                    <asp:Button runat="server" ID="btnExportToExcel" CssClass="btn btn-danger" Text="Export To Excel" OnClick="btnExportToExcel_Click"  />
                                                </td>
                                               
                                            </tr>
                                        </table>
                                        <br />

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
                                                                    <th role="columnheader" style="text-align: center;">Initial Hold Amount</th>
                                                                    <th role="columnheader">Activated Date</th>
                                                                    <th role="columnheader">Status</th>

                                                                   
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
                                                                            
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                     

                                        <div class="alert alert-warning" id="divNoCrmIssue" runat="server" visible="false">
                                            <button class="close" data-dismiss="alert" type="button">
                                                <i class="icon-remove"></i>
                                            </button>
                                            <i class="icon-remove"></i><strong>No History Found</strong>
                                        </div>
                                        <br />

                                    </asp:View>

                                </asp:MultiView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        jQuery(function ($) {
            $('#tblOrderHistroy').dataTable(
                {
                    "aoColumnDefs": [
                        { 'bSortable': false, 'aTargets': [5] }
                    ]
                }
         );
        });
    </script>
</asp:Content>

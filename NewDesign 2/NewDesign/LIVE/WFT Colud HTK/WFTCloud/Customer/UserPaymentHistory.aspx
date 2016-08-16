<%@ Page Title="" Language="C#" MasterPageFile="~/Customer/CustomerPages.Master" AutoEventWireup="true" CodeBehind="UserPaymentHistory.aspx.cs" Inherits="WFTCloud.Customer.UserPaymentHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>User Order Histroy</title>
    <meta name="description" content="we have an three tabs, they are Available services, Subscribed services and Manage CRM Issues. Each view shows the respective details" />
    <meta name="keywords" content="WFT Services and Manage CRM Issue" />
    <style type="text/css">
        .controls {
            margin-top: 0px;
        }

        .auto-style1 {
            width: 104px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="table-header">
        <strong>Subscription History Details</strong>
    </div>
    <div class="row-fluid">
        <div class="span12 widget-container-span">
            <div class="widget-box">
                <div class="widget-header no-border">
                    <ul class="nav nav-tabs" id="Ul1">
                        <li id="liManage" runat="server" class="active">
                            <a href='/Customer/UserPaymentHistory.aspx?userid=<%=UserMembershipID %>'>My Subscription Histroy</a>
                        </li>
                    </ul>
                </div>
                <div class="widget-body">
                    <div class="widget-main padding-12">
                        <div class="tab-content">
                            <div id="Manage" class="tab-pane in active" runat="server">
                                <asp:MultiView ID="mvSubscriptionDetails" runat="server" ActiveViewIndex="0">
                                    <asp:View ID="vwSubscriptionHistroy" runat="server">
                                        <div class="row-fluid">
                                            <div class="span12">
                                                <div class="row-fluid">
                                                    <div class="table-header">
                                                        My Payment Histroy	        
                                                    </div>
                                                    <div class="dataTables_wrapper">
                                                        <div role="grid" class="dataTables_wrapper">
                                                            <table id="tblOrderHistroy" class="table table-striped table-bordered table-hover dataTable">
                                                                <thead>
                                                                    <tr role="row">
                                                                        <th role="columnheader">Subscription ID</th>
                                                                       <th role="columnheader">Category Name</th>
                                                                       <th role="columnheader">Service Name</th>
                                                                        <th role="columnheader">Amount</th>
                                                                        <th role="columnheader">Active Date</th>
                                                                         <th role="columnheader">Status</th>
                                                                         <th role="columnheader">Payment Mode</th>
                                                                        <th role="columnheader" style="text-align: center;">View</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody role="alert">
                                                                    <asp:Repeater ID="rptrSubscriptionHistroy" runat="server">
                                                                        <%--OnItemCommand="rptrOrderHistroy_ItemCommand">--%>
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td><%# Eval("UserSubscriptionID") %></td>
                                                                            <td><%# GetCategoryName(Eval("ServiceCategoryID").ToString()) %></td>
                                                                            <td><%# ServiceName(Eval("ServiceID").ToString()) %>
                                                                                <asp:HiddenField ID="hdnServiceID" runat="server" Value='<%# Eval("ServiceID")%>' />
                                                                            </td>
                                                                                <td>$ <%# Eval("InitialHoldAmount")%></td>
                                                                                <td><%# Eval("ActiveDate","{0:dd-MMM-yyyy}")%></td>
                                                                                <td><%# ShowSubscribedServiceStatus(Eval("UserSubscriptionID").ToString()) %></td>
                                                                                 <td><%# ShowPaymentMode(Eval("UserSubscriptionID").ToString()) %></td>
                                                                                <td style="text-align: center; vertical-align: middle;">
                                                                                    <a data-rel="tooltip" title="View Details" class="btn btn-primary btn-small" href='/Customer/UserPaymentHistory.aspx?userid=<%=UserMembershipID %>&UserSubscription=<%#Eval("UserSubscriptionID") %>'>
                                                                                        <i class="icon-file-alt bigger-130"></i>
                                                                                    </a>
                                                                                    <%--<asp:LinkButton data-rel="tooltip" title="View Details" runat="server" ID="lkbtnViewOrderDetails" CssClass="btn btn-primary btn-small" CommandName='<%#Eval("UserOrderID") %>' >
                                                                        <i class="icon-file-alt bigger-130"></i>
                                                                    </asp:LinkButton>--%>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </tbody>
                                                            </table>

                                                        </div>
                                                    </div>
                                                    <br />
                                                </div>
                                            </div>
                                        </div>
                                    </asp:View>
                                    <asp:View ID="vmUserPaymentHistoryDetails" runat="server">
                                        <h4><a href='/Customer/UserPaymentHistory.aspx?userid=<%=UserMembershipID %>'><< Back</a></h4>
                                        <div class="table-header">
                                            <strong>Subscription ID :
                                            <asp:Label ID="lblSubscriptionID" runat="server"></asp:Label>
                                            </strong>
                                        </div>
                                        <table class="table table-striped table-bordered table-hover dataTable">
                                              
                                            <tr>
                                                <td class="span3">Payment Mode</td>
                                                <td>
                                                    <asp:Label ID="lblPaymentMode" runat="server"></asp:Label>
                                                    <img id="PayPalImage" runat="server" src="~/img/Paypal.png"/>
                                                    <img id="AuthorizeImage" runat="server" src="../img/authorize.png" />
                                                </td>
                                                
                                            </tr>
                                            
                                            
                                        </table>
                                        <br />

                                        <div class="table-header">
                                            <strong>Purchase History</strong>
                                        </div>

                                         <div id="PurchaseAuthorizeDiv" runat="server">
                                        <table class="table table-striped table-bordered table-hover dataTable" id="Table2">
                                            <thead>
                                                <tr role="row">
                                                    <th role="columnheader">Payment Date</th>
                                                   <%-- <th role="columnheader">Customer Profile ID</th>
                                                    <th role="columnheader">Payment Profile ID</th>--%>
                                                    <th role="columnheader">Total Amount</th>
                                                    <th role="columnheader">Transaction ID</th>
                                                    <th role="columnheader">Invoice Number</th>
                                                    <th role="columnheader">Payment Status</th>
                                                </tr>
                                            </thead>
                                            <tbody role="alert">
                                                <asp:Repeater ID="rptrPurchaseSubscriptionHistoryDetails" runat="server" OnItemDataBound="rptrPurchaseSubscriptionHistoryDetails_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr>
                                                           <td><%# Eval("PaymentDate","{0:dd-MMM-yyyy}")%></td>
                                                            <%--<td><asp:Label runat="server" ID="Label1" Text='<%# Eval("CustomerProfileID") %>'></asp:Label></td>
                                                            <td><asp:Label runat="server" ID="Label4" Text='<%# Eval("CustomerPaymentProfileID") %>'></asp:Label></td>--%>
                                                            <td>
                                                                <asp:HiddenField ID="hdnUserOrderID" runat="server" Value='<%# Eval("UserOrderID")%>' />
                                                                <asp:HiddenField ID="hdnServiceID" runat="server" Value='<%# Eval("ServiceID")%>' />
                                                                $<asp:Label runat="server" ID="lblPaymentAmount"></asp:Label></td>
                                                              <td><asp:Label runat="server" ID="Label4" Text='<%# Eval("TransactionID") == "" ? Eval("PalpalPaymentTransactionID") : Eval("TransactionID") %>'></asp:Label></td>
                                                            <td><asp:Label runat="server" ID="Label3" Text='<%# Eval("InvoiceNumber") %>'></asp:Label></td>
                                                            <td><asp:Label runat="server" ID="lblPrice" Text='<%# Eval("PaymentStatus") %>'></asp:Label></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                            </div>

                                       
                                        <br />
                                        <div class="table-header">
                                            <strong>Recurring History </strong>
                                        </div>
                                        <div id="AuthorizeDiv" runat="server">
                                        <table class="table table-striped table-bordered table-hover dataTable" id="tblMyCart">
                                            <thead>
                                                <tr role="row">
                                                    <th role="columnheader">Payment Date</th>
                                                   <%-- <th role="columnheader">Customer Profile ID</th>
                                                    <th role="columnheader">Payment Profile ID</th>--%>
                                                    <th role="columnheader">Payment Amount</th>
                                                    <th role="columnheader">Transaction ID</th>
                                                    <th role="columnheader">Invoice Number</th>
                                                    <th role="columnheader">Payment Status</th>
                                                     <th role="columnheader">Payment Method</th>
                                                </tr>
                                            </thead>
                                            <tbody role="alert">
                                                <asp:Repeater ID="rptrSubscriptionHistoryDetails" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                           <td><%# Eval("PaymentDate","{0:dd-MMM-yyyy}")%></td>
                                                            <%--<td><asp:Label runat="server" ID="Label1" Text='<%# Eval("CustomerProfileID") %>'></asp:Label></td>
                                                            <td><asp:Label runat="server" ID="Label4" Text='<%# Eval("CustomerPaymentProfileID") %>'></asp:Label></td>--%>
                                                            <td>$<asp:Label runat="server" ID="Label2" Text='<%# Eval("PaymentAmount") %>'></asp:Label></td>
                                                             <td><asp:Label runat="server" ID="Label7" Text='<%# Eval("TransactionID") == "" ? Eval("PaypalPaymentTransactionID") : Eval("TransactionID") %>'></asp:Label></td>
                                                            <td><asp:Label runat="server" ID="Label3" Text='<%# Eval("InvoiceNumber") %>'></asp:Label></td>
                                                            <td><asp:Label runat="server" ID="lblPrice" Text='<%# Eval("PaymentStatus") %>'></asp:Label></td>
                                                            <td><asp:Label runat="server" ID="Label1" Text='<%# Eval("PaymentMethod") %>'></asp:Label></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                            </div>


                                        <div class="alert alert-warning" id="divNoCrmIssue" runat="server" visible ="false">
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

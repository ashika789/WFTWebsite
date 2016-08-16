<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminPages.Master" AutoEventWireup="true" CodeBehind="UserSubscriptionHistory.aspx.cs" Inherits="WFTCloud.Admin.UserSubscriptionHistory" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>User List</title>
    <meta name="description" content="List of Users can be seen here and we have an options of Check services, Add new services, Access acount, Delete User, Manage CRM issue and View User Profile" />
    <meta name="keywords" content="WFT Users list" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
                                <asp:MultiView ID="mvSubscriptionDetails" runat="server" ActiveViewIndex="0">
                                    <asp:View ID="vwSubscriptionHistroy" runat="server">
                                        <div class="row-fluid">
                                        </div>
                                    </asp:View>
                                    <asp:View ID="vmUserPaymentHistoryDetails" runat="server">
                                        <h4><a href='/Admin/UserSubscriptionDetails.aspx'><< Back</a></h4>
                                        <div class="table-header">
                                            <strong>Subscription ID :
                                            <asp:Label ID="lblSubscriptionID" runat="server"></asp:Label>
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
                                                <td style="width:20%;">Payment Mode</td>
                                                <td>
                                                    <asp:Label ID="lblPaymentMode" runat="server"></asp:Label>
                                                    <img id="PayPalImage" runat="server" src="~/img/Paypal.png" />
                                                    <img id="AuthorizeImage" runat="server" src="../img/authorize.png" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>SAP User Name</td>
                                                <td>
                                                    <asp:Label ID="lblSAPUserName" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>

                                                <td>Expire Date</td>
                                                <td>
                                                    <asp:Label ID="lblExpireDate" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Work Log</td>
                                                <td>
                                                    <asp:Label ID="lblWorkLog" runat="server"></asp:Label>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td>Activate Subscription</td>
                                                <td>
                                                    <asp:Button ID="btnServiceActivation" runat ="server" class="btn btn-primary" Text="Activate" OnClick="btnServiceActivation_Click" />
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
                                                        <th role="columnheader">Payment Amount</th>
                                                       <%-- <th role="columnheader">Transaction ID</th>--%>
                                                        <th role="columnheader">Invoice Number</th>
                                                        <th role="columnheader">Payment Status</th>
                                                    </tr>
                                                </thead>
                                                <tbody role="alert">
                                                    <asp:Repeater ID="rptrPurchaseSubscriptionHistoryDetails" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Eval("PaymentDate","{0:dd-MMM-yyyy}")%></td>
                                                                <%--<td>
                                                                    <asp:Label runat="server" ID="Label1" Text='<%# Eval("CustomerProfileID") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label4" Text='<%# Eval("CustomerPaymentProfileID") %>'></asp:Label></td>--%>
                                                                <td>$<asp:Label runat="server" ID="Label2" Text='<%# Eval("PaymentAmount") %>'></asp:Label></td>
                                                                <%--<td>
                                                                    <asp:Label runat="server" ID="Label7" Text='<%# Eval("TransactionID") %>'></asp:Label></td>--%>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label3" Text='<%# Eval("InvoiceNumber") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="lblPrice" Text='<%# Eval("PaymentStatus") %>'></asp:Label></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </div>

                                        <div id="PurchasePayPalDiv" runat="server">
                                            <table class="table table-striped table-bordered table-hover dataTable" id="Table3">
                                                <thead>
                                                    <tr role="row">
                                                        <th role="columnheader">Payment Date</th>
                                                        <%--<th role="columnheader">Payer ID</th>
                                                        <th role="columnheader">Billing Agreement ID</th>
                                                        <th role="columnheader">Payment Transaction ID</th>--%>
                                                        <th role="columnheader">Payment Amount</th>
                                                        <th role="columnheader">Invoice Number</th>
                                                        <th role="columnheader">Payment Status</th>
                                                    </tr>
                                                </thead>
                                                <tbody role="alert">
                                                    <asp:Repeater ID="rptrPurchasePayPalSubscriptionHistoryDetails" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Eval("PaymentDate","{0:dd-MMM-yyyy}")%></td>
                                                              <%--  <td>
                                                                    <asp:Label runat="server" ID="Label5" Text='<%# Eval("PaypalPayerID") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label4" Text='<%# Eval("PaypalBillingAgreementID") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label6" Text='<%# Eval("PalpalPaymentTransactionID") %>'></asp:Label></td>--%>
                                                                <td>$<asp:Label runat="server" ID="Label2" Text='<%# Eval("PaymentAmount") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label3" Text='<%# Eval("InvoiceNumber") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="lblPrice" Text='<%# Eval("PaymentStatus") %>'></asp:Label></td>
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
                                                        <%--<th role="columnheader">Transaction ID</th>--%>
                                                        <th role="columnheader">Invoice Number</th>
                                                        <th role="columnheader">Payment Status</th>
                                                    </tr>
                                                </thead>
                                                <tbody role="alert">
                                                    <asp:Repeater ID="rptrSubscriptionHistoryDetails" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Eval("PaymentDate","{0:dd-MMM-yyyy}")%></td>
                                                               <%-- <td>
                                                                    <asp:Label runat="server" ID="Label1" Text='<%# Eval("CustomerProfileID") %>'></asp:Label></td>--%>
                                                               <%-- <td>
                                                                    <asp:Label runat="server" ID="Label4" Text='<%# Eval("CustomerPaymentProfileID") %>'></asp:Label></td>--%>
                                                                <td>$<asp:Label runat="server" ID="Label2" Text='<%# Eval("PaymentAmount") %>'></asp:Label></td>
                                                               <%-- <td>
                                                                    <asp:Label runat="server" ID="Label7" Text='<%# Eval("TransactionID") %>'></asp:Label></td>--%>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label3" Text='<%# Eval("InvoiceNumber") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="lblPrice" Text='<%# Eval("PaymentStatus") %>'></asp:Label></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </div>

                                        <div id="PayPalDiv" runat="server">
                                            <table class="table table-striped table-bordered table-hover dataTable" id="Table1">
                                                <thead>
                                                    <tr role="row">
                                                        <th role="columnheader">Payment Date</th>
                                                       <%-- <th role="columnheader">Payer ID</th>
                                                        <th role="columnheader">Billing Agreement ID</th>
                                                        <th role="columnheader">Payment Transaction ID</th>--%>
                                                        <th role="columnheader">Payment Amount</th>
                                                        <th role="columnheader">Invoice Number</th>
                                                        <th role="columnheader">Payment Status</th>
                                                    </tr>
                                                </thead>
                                                <tbody role="alert">
                                                    <asp:Repeater ID="rptrPayPalSubscriptionHistoryDetails" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Eval("PaymentDate","{0:dd-MMM-yyyy}")%></td>
                                                               <%-- <td>
                                                                    <asp:Label runat="server" ID="Label5" Text='<%# Eval("PaypalPayerID") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label4" Text='<%# Eval("PaypalBillingAgreementID") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label6" Text='<%# Eval("PaypalPaymentTransactionID") %>'></asp:Label></td>--%>
                                                                <td>$<asp:Label runat="server" ID="Label2" Text='<%# Eval("PaymentAmount") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="Label3" Text='<%# Eval("InvoiceNumber") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="lblPrice" Text='<%# Eval("PaymentStatus") %>'></asp:Label></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
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

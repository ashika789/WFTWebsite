<%@ Page Title="" Language="C#" MasterPageFile="~/Customer/CustomerPages.Master" AutoEventWireup="true" CodeBehind="Success.aspx.cs" Inherits="WFTCloud.Customer.Success" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h4><a href="/Customer/CloudPackages.aspx?userid=<%=UserMembershipID %>&showview=SubscribedService"><< Back</a></h4>
     <h3>Thank You! We have received your payment.</h3>
    <div class="alertmsg_succ">
        <p>
            <h4 id="lblMessage" runat="server" class="alert alert-success">Thank You! We have received your payment. Kindly, check your <a href="/Customer/CloudPackages.aspx?userid=<%=UserMembershipID %>&showview=SubscribedService">Subscribed Services</a></h4>
        </p>
    </div>
        <div id="zOrderDetails" runat="server">
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
                                                <td >Amount
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
                                                    <th role="columnheader">Initial Hold Amount </th>
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
                                                <td >Amount 
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPaymentAmount" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Payer ID </td>
                                                <td>
                                                    <asp:Label ID="lblPayPalPayerId" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Billing Agreement ID </td>
                                                <td>
                                                    <asp:Label ID="lblPayPalPaymentId" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Payment Transaction ID </td>
                                                <td>
                                                    <asp:Label ID="lblPayPalSalesId" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Amount Paid</td>
                                                <td>
                                                    <asp:Label ID="lblAmountPaid" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>

</asp:Content>

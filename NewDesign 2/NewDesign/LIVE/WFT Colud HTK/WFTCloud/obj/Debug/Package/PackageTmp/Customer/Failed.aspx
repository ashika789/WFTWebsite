<%@ Page Title="" Language="C#" MasterPageFile="~/Customer/CustomerPages.Master" AutoEventWireup="true" CodeBehind="Failed.aspx.cs" Inherits="WFTCloud.Customer.Failed" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h4><a href="/Customer/CloudPackages.aspx?userid=<%=UserMembershipID %>&showview=ShowMyCart"><< Back</a></h4>
    <h3>Sorry! Payment Failed.</h3>
    <div class="alertmsg_error">
        <p>
            <h4 id="lblMessage" runat="server" class="alert alert-error">Sorry! We are not able to process your payment successfully. Please try again.<a href="/Customer/CloudPackages.aspx?userid=<%=UserMembershipID %>&showview=ShowMyCart">My Cart</a></h4>
        </p>
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
                                                    <th role="columnheader" class="span3">Category Name</th>
                                                    <th role="columnheader" class="span3">Service Name</th>
                                                    <th role="columnheader"  class="span3">Quantity</th>
                                                    <th role="columnheader"  class="span3">Initial Hold Amount </th>
                                                </tr>
                                            </thead>
                                            <tbody role="alert">
                                                <asp:Repeater ID="rptrOrderDetails" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="span3"><%# CategoryName(Eval("ServiceID").ToString()) %>
                                                                <asp:HiddenField ID="hidUserCartID" runat="server" Value='<%# Eval("UserOrderDetailID")%>' />
                                                            </td>
                                                            <td class="span3"><%# ServiceName(Eval("ServiceID").ToString()) %>
                                                                <asp:HiddenField ID="hdnServiceID" runat="server" Value='<%# Eval("ServiceID")%>' />
                                                            </td>
                                                            <td class="span3"><%#Eval("Quantity") %></td>
                                                            <td class="span3">$
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
                                                <td>Amount Paid</td>
                                                <td>
                                                    <asp:Label ID="lblAmountPaid" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
    </div>
</asp:Content>

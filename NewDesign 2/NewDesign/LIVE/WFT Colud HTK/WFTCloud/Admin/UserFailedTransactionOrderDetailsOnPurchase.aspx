<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminPages.Master" AutoEventWireup="true" CodeBehind="UserFailedTransactionOrderDetailsOnPurchase.aspx.cs" Inherits="WFTCloud.Admin.UserFailedTransactionOrderDetailsOnPurchase" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="table-header">
	    <strong>Customer Failed Transaction and Order Details</strong>	        
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
	                                        <div class="table-header">
		                                       Customer Failed Transaction and Order Details
                                            </div>
                                            <div class="dataTables_wrapper">
                    <div class="row-fluid">
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager> 
                        <div class="span10">
                            <asp:Label ID="lblStartDate" runat="server" Text="Filter From"></asp:Label>
                            <asp:TextBox ID="txtStartDate" runat="server" CssClass="span3" ></asp:TextBox>
                            <asp:CalendarExtender ID="ceStartDate" TargetControlID="txtStartDate" PopupButtonID="txtStartDate" runat="server" Format="dd-MMM-yyyy" />
                            <asp:Label ID="lblEndDate" runat="server" Text="To"></asp:Label>
                            <asp:TextBox ID="txtEndDate" runat="server" CssClass="span3"></asp:TextBox>
                            <asp:CalendarExtender ID="ceEndDate" TargetControlID="txtEndDate" PopupButtonID="txtEndDate"  runat="server" Format="dd-MMM-yyyy" />
                            <asp:Button ID="btnGo" runat="server" Text="Go" OnClick="btnGo_Click" class="btn btn-primary btn-small"  />
                        </div>
                        <br />
                    </div>
                                                <div role="grid" class="dataTables_wrapper">
                                                    <table id="tblOrderHistroy" class="table table-striped table-bordered table-hover dataTable">
                                                        <thead>
                                                        <tr role="row">
                                                            <th role="columnheader">OrderNo</th>
                                                            <th role="columnheader">Order Date</th>
                                                            <th role="columnheader">Amount</th>
                                                            <th role="columnheader">User Email ID</th>
                                                            <th role="columnheader" style="text-align:center;">View</th>
                                                        </tr>
                                                        </thead>
                                                        <tbody role="alert">
                                                        <asp:Repeater ID="rptrOrderHistroy" runat="server" ><%--OnItemCommand="rptrOrderHistroy_ItemCommand">--%>
                                                        <ItemTemplate>
                                                        <tr>
                                                            <td>Order<%# Eval("UserOrderID")%><asp:HiddenField ID="hdnUserOrderID" runat="server" Value='<%# Eval("UserOrderID")%>' />
                                                            </td>
                                                            <td><%#string.Format("{0:dd-MMM-yyyy}", Eval("OrderDateTime")).ToUpper()%></td>
                                                            <td style="text-align:right;">$ <%# Eval("OrderTotal")%></td>
                                                            <td><%# GetUserMailID(Eval("UserProfileID").ToString())%></td>
                                                            <td style="text-align:center; vertical-align:middle;">
                                                                <a data-rel="tooltip" title="View Details" href='/Admin/UserFailedTransactionOrderDetailsOnPurchase.aspx?UserOrder=<%#Eval("UserOrderID") %>' >
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
                                    <asp:View ID="vmOrderDetails" runat="server">
                                       <h4> <a href="/Admin/UserFailedTransactionOrderDetailsOnPurchase.aspx" ><< Back</a></h4>
                                        <div class="table-header">
		                                    <strong>Order Number :
                                            <asp:Label ID="lblOrderCode" runat="server"></asp:Label>
                                            </strong></div>
                                        <table class="table table-striped table-bordered table-hover dataTable">
                                            <tr>
                                                <td class="span3">Order Date</td>
                                                <td>
                                                    <asp:Label ID="lblOrderDate" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                    <tr>
                                        <td class="auto-style1">Amount
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
                                        <td >
                                              <asp:Label ID="lblCouponCode" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trDiscountedAmount" visible="false">
                                        <td>Discounted Amount</td>
                                        <td >
                                              <asp:Label ID="lblDisCountAmount" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                        <br />
                                        <div class="table-header">
                                            <strong>Order Details </strong>
                                        </div>
                                        <table class="table table-striped table-bordered table-hover dataTable" id="tblMyCart">
                    <thead>
                        <tr role="row">
                            <th role="columnheader">Category Name</th>
                            <th role="columnheader">Service Name</th>
                            <th role="columnheader">Quantity</th>
                            <th role="columnheader">Initial Hold Amount </th>
                        </tr>
                    </thead>
                    <tbody role="alert">
                        <asp:Repeater ID="rptrOrderDetails" runat="server" >
                            <ItemTemplate>
                                <tr>
                                    <td><%# CategoryName(Eval("ServiceID").ToString()) %>
                                        <asp:HiddenField ID="hidUserCartID" runat="server" Value='<%# Eval("UserOrderDetailID")%>' />
                                    </td>
                                     <td><%# ServiceName(Eval("ServiceID").ToString()) %>
                            <asp:HiddenField ID="hdnServiceID" runat="server" Value='<%# Eval("ServiceID")%>' />
                                     </td>
                                    <td><%#Eval("Quantity") %></td>
                                    <td>$ <asp:Label runat="server" ID="lblPrice" Text='<%# Eval("InitialHoldAmount") %>'></asp:Label></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
                                        <br />
                                       <div class="table-header">
		                                    <strong>Payment Transaction Details
                                            </strong></div>
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
                                        <td class="auto-style1">Amount 
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPaymentAmount" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                        <tr>
                         <td>Mode</td>           
                          <td><asp:Label runat="server" ID="lblMode"></asp:Label></td>
                        </tr>
                                    <tr runat="server" id="trAuthCard1" visible="true">
                                                <td>Credit Card Number </td>
                                                <td>
                                                    <asp:Label ID="lblCreditCardnumber" runat="server"></asp:Label>
                                                </td>
                                    </tr>
                                            <asp:Panel runat="server" ID="pnlPaypalShow" Visible="false">
                                            <tr>
                                                <td>Paypal Payment ID </td>
                                                <td>
                                                    <asp:Label ID="lblPayPalPaymentId" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Paypal Payer ID </td>
                                                <td>
                                                    <asp:Label ID="lblPayPalPayerId" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Paypal Sale ID </td>
                                                <td>
                                                    <asp:Label ID="lblPayPalSalesId" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            </asp:Panel>
                                    <tr>
                                        <td>Amount Paid</td>
                                        <td >
                                              <asp:Label ID="lblAmountPaid" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trAuthMessage1" visible="true">
                                        <td>Message</td>
                                        <td >
                                              <asp:Label ID="lblPaymentMessage" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                    </asp:View>
                                    <asp:View ID="vwNoOrderHistroy" runat="server">
                                        <div class="alert alert-warning" id="divNoCrmIssue" runat="server">
		                                    <button class="close" data-dismiss="alert" type="button">
                                                <i class="icon-remove"></i>
                                            </button>
                                            <i class="icon-remove"></i><strong>No Orders Found</strong></div>
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
                        { 'bSortable': false, 'aTargets': [4] }
                    ]
                }
         );
        });
   </script>
</asp:Content>

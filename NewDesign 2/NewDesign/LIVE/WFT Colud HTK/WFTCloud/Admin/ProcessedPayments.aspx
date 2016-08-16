<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminPages.Master" AutoEventWireup="true" CodeBehind="ProcessedPayments.aspx.cs" Inherits="WFTCloud.Admin.ProcessedPayments" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Processed Payments</title>
    <meta name="Description" content="Payment process are Managed Here." />
    <meta name="Keywords" content="Processed Payments Management" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row-fluid">
        <div class="span12">
            <asp:MultiView runat="server" ID="mvPayments" ActiveViewIndex="0">
                <asp:View runat="server" ID="vwPayment">

            <div class="row-fluid">
                <div id="divRefundSuccess" runat="server" visible="false" class="alert alert-success span8">
				        <button data-dismiss="alert" class="close" type="button">
					        <i class="icon-remove"></i>
				        </button>
                        <i class="icon-remove"></i>
                        <asp:Label ID="lblRefundSuccess" runat="server"></asp:Label>
                    </div>
                   <div id="divPaymentError" runat="server" visible="false" class="alert alert-error spa8">
				        <button data-dismiss="alert" class="close" type="button">
					        <i class="icon-remove"></i>
				        </button>
                        <i class="icon-remove"></i>
                        <asp:Label ID="lblPaymentError" runat="server"></asp:Label>
                    </div>
            </div>
                     <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager> 
                <div class="row-fluid">
                    <div class="table-header">
                        Processed Payments Management
                    </div>
                <div class="dataTables_wrapper">
                    <div class="row-fluid">
                        <div class="span10">
                            <asp:Label ID="lblStartDate" runat="server" Text="Filter From"></asp:Label>
                            <asp:TextBox ID="txtStartDate" runat="server" CssClass="span3" ></asp:TextBox>
                            <asp:CalendarExtender ID="ceStartDate" TargetControlID="txtStartDate" PopupButtonID="txtStartDate" runat="server" Format="dd-MMM-yyyy" />
                            <asp:Label ID="lblEndDate" runat="server" Text="To"></asp:Label>
                            <asp:TextBox ID="txtEndDate" runat="server" CssClass="span3"></asp:TextBox>
                            <asp:CalendarExtender ID="ceEndDate" TargetControlID="txtEndDate" PopupButtonID="txtEndDate"  runat="server" Format="dd-MMM-yyyy" />
                            <asp:Button ID="btnGo" runat="server" Text="Go" OnClick="btnGo_Click" class="btn btn-primary"  />
                        </div>
                    </div>
                    <div role="grid" class="dataTables_wrapper">
                    <table id="tblProcessedPayments" class="table table-striped table-bordered table-hover dataTable">
                        <thead>
                            <tr role="row">
                                <th role="columnheader" class="center">Invoice Number</th>
                                <th role="columnheader" class="center">Name</th>
                                <th role="columnheader" class="center">Mode</th>
                                <th role="columnheader" class="center">Email</th>
                                <th role="columnheader" class="center">Billed Amount</th>
                                <th role="columnheader" class="center">Payment Date</th>
                                <th role="columnheader" class="center">Is Refunded</th>
                                <th role="columnheader" class="center" style="text-align:center; ">Options</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptrProcessedPayment" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td class="center">
                                            <%#Eval("InvoiceNumber")%>
                                        </td>
                                        <td><%# Username(Eval("UserProfileID").ToString())%>
                                            <asp:HiddenField ID="hdnUserPaymentTransactionID" runat="server" Value='<%# Eval("UserPaymentTransactionID")%>' />
                                        </td>
                                        <td><%#Eval("PaymentMethod")== null?"Authorize.net":Eval("PaymentMethod")%></td>
                                        <td><a href='mailto:<%#UserEmailID(Eval("UserProfileID").ToString()) %>'><%# UserEmailID(Eval("UserProfileID").ToString())%></a></td>
                                        <td style="text-align:right;">$ <%# Eval("Amount")%></td>
                                        <td><%#Eval("PaymentDateTime", "{0:dd-MMM-yyyy}")%></td>
                                        <td><%# Eval("IsRefunded") != null? WFTCloud.GeneralReusableMethods.GetStatusForRefund(Eval("IsRefunded").ToString()):"No" %></td>
                                        <td>
                                        <div class="action-buttons">
                                            <a data-rel="tooltip" title="Refund" href='/Admin/ProcessedPayments.aspx?UserProfileID=<%# Eval("UserProfileID")%>&UserPaymentTransactionID=<%#Eval("UserPaymentTransactionID") %>' class="green">
                                                <i class="icon-reply bigger-130"></i>
                                            </a>
                                        </div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                    </div>
<div class="row-fluid">
                                                <asp:CheckBox ID="chkShowRefunded" runat="server"
                                        AutoPostBack="true" OnCheckedChanged="chkShowRefunded_CheckedChanged"/>
                                        <span class="label label-warning arrowed-right">Show Refunded</span>
                                            </div>
                </div>
            </div>

                    
                </asp:View>
                <asp:View runat="server" ID="vwPaymentDetails">
                    <h4><a href="/Admin/ProcessedPayments.aspx"><< Back</a></h4>
            <div class="row-fluid">
                <div id="divRefundedSuccessly" runat="server" visible="false" class="alert alert-success span8">
				        <button data-dismiss="alert" class="close" type="button">
					        <i class="icon-remove"></i>
				        </button>
                        <i class="icon-remove"></i>
                        <asp:Label ID="lblRefundedSuccess" runat="server"></asp:Label>
                    </div>
                   <div id="divRefundedFailed" runat="server" visible="false" class="alert alert-error spa8">
				        <button data-dismiss="alert" class="close" type="button">
					        <i class="icon-remove"></i>
				        </button>
                        <i class="icon-remove"></i>
                        <asp:Label ID="lblRefunedFailed" runat="server"></asp:Label>
                    </div>
            </div>
              <div class="row-fluid">
                    <div class="table-header">
                        Refund Details for the Invoice Number - <asp:Label runat="server" ID="lblInvoiceNumber"></asp:Label>
                    </div>
                <div class="dataTables_wrapper">
                    <div role="grid" class="dataTables_wrapper">
                    <table  class="table table-striped table-bordered table-hover dataTable">
                     <tr>
                         <td>User Full Name</td>           
                          <td><asp:Label runat="server" ID="lbluserFullName"></asp:Label>    </td>
                      </tr>
                      <tr>
                         <td class="span2">User Email ID</td>           
                          <td><asp:Label runat="server" ID="lblUserMailID"></asp:Label>    </td>
                      </tr>
                      <tr>
                         <td>Invoice Number</td>           
                          <td><asp:Label runat="server" ID="lblPaymentInVoiceNumber"></asp:Label></td>
                      </tr>
                        <tr>
                         <td>Mode</td>           
                          <td><asp:Label runat="server" ID="lblMode"></asp:Label></td>
                        </tr>
                      <tr>
                         <td>Payment Date</td>           
                          <td><asp:Label runat="server" ID="lblPaymentDate"></asp:Label>    </td>
                      </tr>
                       <tr>
                         <td>Billed Amount</td>           
                          <td style="vertical-align:middle;">$ <asp:TextBox runat="server" ID="txtBilledAmount" ReadOnly="true" CssClass="span2"></asp:TextBox>    </td>
                      </tr>
                        <tr>
                         <td>Is Refunded</td>           
                          <td><asp:Label runat="server" ID="lblIsRefuned"></asp:Label>    </td>
                      </tr>
                        <tr runat="server" id="tramountToRefund">
                         <td>Amount To Refund</td>           
                          <td style="vertical-align:middle;">$ <asp:TextBox runat="server" ID="txtAmountToRefund" CssClass="span2" ValidationGroup="Refund"></asp:TextBox>    
                              <asp:RequiredFieldValidator ID="rfvAmtToRefund" runat="server" ControlToValidate="txtAmountToRefund" ErrorMessage="*" ForeColor="Red" ValidationGroup="Refund"></asp:RequiredFieldValidator>
                              <asp:Label ID="lblAmounSholubeLessthanBilledAmount" runat="server" ForeColor="Red" Text="Refund Amount must be Lower than or Equal to Billed Amount." Visible="False"></asp:Label>
                            </td>
                      </tr>
                        <tr runat="server" id="trBtnRefund">
                         <td></td>           
                          <td><asp:Button runat="server" ID="btnRefund" CssClass="btn btn-warning" OnClick="btnRefund_Click" Text="Refund" ValidationGroup="Refund" /> </td>
                      </tr>
                    </table>
                    </div>
                </div>
            </div>
                    <br />
<div class="row-fluid" runat="server" id="divRefundHistroy" visible="false">
                    <div class="table-header">
                        Refund Transaction History
                    </div>
                <div class="dataTables_wrapper">
                    <div role="grid" class="dataTables_wrapper">
                    <table  class="table table-striped table-bordered table-hover dataTable">
                     <thead>
                            <tr role="row">
                                <th role="columnheader" class="center" style="width: 15%;">Invoice Number</th>
                                <th role="columnheader" class="center" style="width: 15%;">Refund Amount</th>
                                <th role="columnheader" class="center" style="width: 15%;">Payment Date</th>
                                <th role="columnheader" class="center" style="width: 15%;">Is Refunded</th>
                                <th role="columnheader" class="center" style="width: 40%;">Message</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptrRefundHistory" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("UserPaymentInvoiceNumber")%></td>
                                        <td style="text-align:right;">$ <%# Eval("AuthResponseAmount")%></td>
                                        <td><%#Eval("PaymentDateTime", "{0:dd-MMM-yyyy}")%></td>
                                        <td><%# WFTCloud.GeneralReusableMethods.GetStatusForRefund(Eval("AuthResponseApproved").ToString()) %></td>
                                        <td><%# Eval("AuthResponseMessage")%></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                    </div>
                </div>
            </div>
                </asp:View>
               <asp:View runat="server" ID="vwNoPaymentDEtails">
        <h4><a href="/Admin/ProcessedPayments.aspx"><< Back</a></h4>
                   <div id="divNoPymtDetails" runat="server" class="alert alert-warning">
                                            <button data-dismiss="alert" class="close" type="button">
                                            <i class="icon-remove"></i>
                                            </button>
                                            <i class="icon-remove"></i>
                                            <strong>There are no Payment transaction details.</strong>
                                        </div>
                </asp:View>
            </asp:MultiView>
            </div>
        </div>
    <script type="text/javascript">
        jQuery(function ($) {
            $('#tblProcessedPayments').dataTable(
                  {
                      "aoColumnDefs": [
                          { 'bSortable': false, 'aTargets': [7] }
                      ]
                  }
         );
        });
   </script>
</asp:Content>

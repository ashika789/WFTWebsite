<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminPages.Master" AutoEventWireup="true" CodeBehind="AutomatedPayments.aspx.cs" Inherits="WFTCloud.Admin.AutomatedPayments" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Automated Payments</title>
    <meta name="Description" content="Automated Payment Details are managed here." />
    <meta name="Keywords" content="WFT Automated Payments" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="mvContainer" runat="server" ActiveViewIndex="0">
        <asp:View ID="APView" runat="server">
            <div class="row-fluid">
                <div class="span12">
                    <div class="row-fluid">
                        <div id="divAPSuccessMessage" runat="server" visible="false" class="alert alert-block alert-success span6">
                            <button data-dismiss="alert" class="close" type="button">
                                <i class="icon-remove"></i>
                            </button>
                            <p>
                                <i class="icon-ok"></i>
                                Automated Payment Status Updated successfully.
                            </p>
                        </div>
                        <div id="divAPErrorMessage" runat="server" visible="false" class="alert alert-block alert-error span6">
                            <button data-dismiss="alert" class="close" type="button">
                                <i class="icon-remove"></i>
                            </button>
                            <p>
                                <i class="icon-ok"></i>
                                Enter the Valid Payment Details.
                            </p>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="table-header">
                            Automated Payments        
                        </div>
                    </div>
                    <div role="grid" class="dataTables_wrapper">
                         <div class="row-fluid">
                        <div class="span10">
                            <asp:Label ID="lblStartDate" runat="server" Text="Filter From"></asp:Label>
                            <asp:TextBox ID="txtStartDate" runat="server" CssClass="span3"></asp:TextBox>
                            <asp:CalendarExtender ID="ceStartDate" TargetControlID="txtStartDate" PopupButtonID="txtStartDate" runat="server" Format="dd-MMM-yyyy" />
                            <asp:Label ID="lblEndDate" runat="server" Text="To"></asp:Label>
                            <asp:TextBox ID="txtEndDate" runat="server" CssClass="span3"></asp:TextBox>
                            <asp:CalendarExtender ID="ceEndDate" TargetControlID="txtEndDate" PopupButtonID="txtEndDate"  runat="server" Format="dd-MMM-yyyy" />
                            <asp:Button ID="btnGo" runat="server" Text="Go" OnClick="btnGo_Click" class="btn btn-primary"  />
                        </div>
                    </div>
                        <table id="tblAutomatedPayments" class="table table-striped table-bordered table-hover dataTable">
                            <thead>
                                <tr role="row">
                                    <th role="columnheader" class="center">Customer Name</th>
                                    <th role="columnheader" class="center">Subscription ID</th>
                                    <th role="columnheader" class="center">Category-Service</th>
                                    <th role="columnheader" class="center">Initial Subscription Date</th>
                                    <th role="columnheader" class="center">Payment Date</th>
                                    <th role="columnheader" class="center">Amount To Bill</th>
                                    <th role="columnheader" class="center">Payment Status</th>
                                    <th role="columnheader" class="center">Amount Billed</th>
                                    <th role="columnheader" class="center">Mode</th>
                                    <th role="columnheader" class="center">Options</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptrAutomatedPayments" runat="server">
                                    <itemtemplate>
                                        <tr>
                                            <td><%# Eval("FirstName")%> <%# Eval("MiddleName")%> <%# Eval("LastName")%></td>
                                            <td><%# Eval("UserSubscriptionID")%></td>
                                            <td><%# Eval("CategoryName")%> - <%# Eval("ServiceName")%></td>
                                            <td><%# String.Format("{0:dd-MMM-yyyy}", Eval("InitialSubscriptionDate"))%></td>
                                            <td><%# String.Format("{0:dd-MMM-yyyy}", Eval("PaymentDate"))%></td>
                                            <td>$ <%# Eval("CurrentMonthBilling")%></td>
                                            <td><%# Eval("PaymentStatus")%></td>
                                            <td><%# Eval("PaymentAmount") == null?"-":("$ "+Eval("PaymentAmount"))%></td>
                                            <td><%#Eval("PaymentMethod")== null?"Authorize.net":Eval("PaymentMethod")%></td>
                                            <td>
                                                <div class="action-buttons">
                                                    <a data-rel="tooltip" title="Edit Payment Details" href='/Admin/AutomatedPayments.aspx?editPaymentdetailsid=<%# Eval("AllPaymentID")%>' class="green">
                                                        <i class="icon-pencil bigger-130"></i>
                                                    </a>
                                                     <a data-rel="tooltip" title="Refund" href='/Admin/ProcessedPayments.aspx?UserProfileID=<%# Eval("UserProfileID")%>&InvoiceNumber=<%#Eval("InvoiceNumber") %>' style='<%# Eval("PaymentStatus").ToString().Contains("success") == true?"display:block;":"display:none;"%>'" class="pink">
                                                        <i class="icon-reply bigger-130"></i>
                                                    </a>
                                                </div>
                                            </td>
                                        </tr>
                                    </itemtemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                    <div class="row-fluid">
                        <asp:CheckBox runat="server" AutoPostBack="true" ID="chkShowCancelledSubscriptionPayments" OnCheckedChanged="chkShowCancelledSubscriptionPayments_CheckedChanged" />
                        <span class="label label-warning arrowed-right">Show Cancelled Subscriptions</span>
                    </div>
                </div>
            </div>
        </asp:View>
        <asp:View ID="MTDEdit" runat="server">
            <div class="row-fluid">
                <h4><a href="AutomatedPayments.aspx">&lt;&lt; Back&nbsp;</a></h4><br />
                <div id="lblSuccessMessage" runat="server" class="alert alert-block alert-success" visible="false">
                    <button class="close" data-dismiss="alert" type="button">
                        <i class="icon-remove"></i>
                    </button>
                    <p>
                        <i class="icon-ok"></i>Payment Details Updated successfully.
                    </p>
                </div>
                <div id="lblPaymentsErrorMsg" runat="server" class="alert alert-error" visible="false">
                    <button class="close" data-dismiss="alert" type="button">
                        <i class="icon-remove"></i>
                    </button>
                    <i class="icon-remove"></i>
                    <asp:Label ID="lblPaymentsErrorText" runat="server"></asp:Label>
                </div>
                &nbsp;<div class="table-header">
                    Edit Payments Details
                </div>
                <table class="table table-hover dataTable table-bordered ">
                    <tr>
                        <td class="span2">User Name</td>
                        <td><asp:Label runat="server" ID="lblusername"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="span4">User Email ID</td>
                        <td><asp:HyperLink runat="server" ID="hypUserEmailID"></asp:HyperLink></td>
                    </tr>
                    <tr>
                        <td class="span2">Category Name</td>
                        <td><asp:Label runat="server" ID="lblCategoryName"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="span2">Service Name</td>
                        <td><asp:Label runat="server" ID="lblServiceName"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="span2">Initial Subscription Date</td>
                        <td>
                            <asp:Label ID="lblInitialSubscriptionDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Payment Mode</td>
                        <td>
                            <asp:Label ID="lblMode" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr runat="server" id="trPayPalCards">
                                            <td class="span3">Registered PayPal EmailID's</td>
                                            <td>
                                            <asp:DropDownList runat="server" ID="ddlRegisteredPayPalEmails" AutoPostBack="true" OnSelectedIndexChanged="ddlRegisteredPayPalEmails_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            </td>
                                            </tr>
                    <tr id="trCreditCardUsedForBilling" runat ="server" visible ="false">
                        <td>
                            Credit Card Used For Billing
                        </td>
                        <td>
                            <asp:Label ID="lblCreditCardUsedForBilling" runat ="server" ></asp:Label>
                        </td>
                    </tr>
                       <tr runat="server" id="trExistingCards" visible="false">
                                            <td class="span3">Registered Cards</td>
                                            <td>
                                            <asp:DropDownList runat="server" ID="ddlExistingCards" AutoPostBack="true" OnSelectedIndexChanged="ddlExistingCards_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            </td>
                                            </tr>
                    <tr runat="server" id="trMakeThisCardForFutureTransaction" visible="false">
                        <td>Make This Card For Future Transaction</td>
                        <td>
                            <asp:CheckBox runat="server" ID="chkMakeThisCardForFutureTransaction" />
                        </td>
                    </tr>
                    <tr  runat="server" id="trPaymentDate" visible="false">
                        <td class="span2">Payment Date</td>
                        <td><asp:Label ID="lblPaymentDate" runat="server"></asp:Label></td>
                    </tr>
                    <tr  runat="server" id="trPaymentDateEdit">
                        <td class="span2">Payment Date</td>                        
                        <td>
                            <asp:ScriptManager runat="server" ID="smd"></asp:ScriptManager>
                            &nbsp;<asp:TextBox ID="txtPaymentDate" runat="server" CssClass="span3" ></asp:TextBox>
                             <asp:CalendarExtender ID="cetxtPaymentDate" TargetControlID="txtPaymentDate" PopupButtonID="txtPaymentDate" runat="server" Format="dd-MMM-yyyy" />
                            <asp:RequiredFieldValidator ID="rfvDate" runat="server" ControlToValidate="txtPaymentDate" ErrorMessage="*" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Change Payment Date For Future Transaction
                            <%--<br /><small>( To Change Future Transactions Date the Payment date should be less the 29 of Month.)</small>--%>
                            <%--<br /><small>( Days difference between the old payment date & new payment date will be added to all future transaction's Payment Date
                             <br /> Eg: 10-Aug-2014 was changed to 15-Aug-2014 means 5 days will be added to all future transaction's Payment Date )</small>--%>
                        </td>
                        <td style="vertical-align:middle;">
                           <asp:CheckBox runat="server" ID="chkChangePaymentDateToAllFeaturePayments" Text="" />
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Amount To Bill</td>
                        <td style="vertical-align:middle;">
                           $ <asp:TextBox ID="txtAmountToBill" runat="server" CssClass="span3"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtAmountToBill" runat="server" ControlToValidate="txtAmountToBill" ErrorMessage="*" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revAmount" runat="server" ControlToValidate="txtAmountToBill" ErrorMessage="Enter Valid Amount" ForeColor="Red" ValidationExpression="^([1-9]\d{0,2}(\,\d{3})*|([1-9]\d*))(\.\d{2})?$" ValidationGroup="Save"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Change Amount To Bill For Future Transaction</td>
                        <td style="vertical-align:middle;">
                           <asp:CheckBox runat="server" ID="chkAmountChangeToAllFeaturePayments" Text="" />
                        </td>
                    </tr>
<%--                    <tr runat="server" id="trPaymentAmount" visible="false">
                        <td class="span2">Payment Amount</td>
                        <td>
                            <asp:Label ID="lblPaymentAmount" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr runat="server" id="trPaymentAmountEdit" visible="false">
                        <td class="span2">Payment Amount</td>
                        <td style="vertical-align:middle;">
                           $ <asp:TextBox ID="txtPaymentAmount" runat="server" CssClass="span3"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDate0" runat="server" ControlToValidate="txtPaymentAmount" ErrorMessage="*" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revAmount" runat="server" ControlToValidate="txtPaymentAmount" ErrorMessage="Enter Valid Amount" ForeColor="Red" ValidationExpression="^([1-9]\d{0,2}(\,\d{3})*|([1-9]\d*))(\.\d{2})?$" ValidationGroup="Save"></asp:RegularExpressionValidator>
                        </td>
                    </tr>--%>
                    <tr >
                        <td class="span2">Payment Status</td>
                        <td style="vertical-align:middle;">
                           <asp:Label ID="lblPaymentStatus" runat="server" CssClass="span2"></asp:Label>
                        </td>
                    </tr>
                    <tr  runat="server" id="trWorkLogEdit">
                        <td class="span2">Work log</td>
                        <td style="vertical-align:middle;">
                           <asp:TextBox runat="server" ID="txtWorkLog" TextMode="MultiLine" Height="50px" CssClass="span10"></asp:TextBox>
                        </td>
                    </tr>
                    <tr >
                        <td class="span2">Work Log History</td>
                        <td style="vertical-align:middle;">
                           <asp:TextBox runat="server" ID="txtWorkLogHistory" TextMode="MultiLine" Height="100px" ReadOnly="true" CssClass="span10"></asp:TextBox>
                        </td>
                    </tr>
                    <tr runat="server" id="trPaymentDetEdit">
                        <td class="span2"></td>
                        <td>
                            <asp:Button ID="btnSave" Text="Save" runat="server" class="btn btn-primary" BackColor="#3459FC" ValidationGroup="Save" OnClick="btnSave_Click" />
                            <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-warning" OnClick="btnReset_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="row-fluid" runat="server" id="dvAuthresponse">
                <div class="table-header">
                    <asp:Label runat="server" ID="lblPaymentMethodType" Text="Authorize.net Reponses"></asp:Label>
                </div>
                <table class="table table-hover dataTable table-bordered ">
                    <tr>
                        <td class="span4">Approval Status</td>
                        <td><asp:Label runat="server" ID="lblAuthApprovalStatus"></asp:Label></td>
                    </tr>
                    <asp:Panel runat="server" ID="pnlAuthResponse">
                    <tr>
                        <td class="span4">Authorization Code </td>
                        <td><asp:Label runat="server" ID="lblAuthorizationCode"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="span4">Card Number  </td>
                        <td><asp:Label runat="server" ID="lblCardNumber"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="span4">Message </td>
                        <td><asp:Label runat="server" ID="lblMessage"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="span4">Response Code  </td>
                        <td><asp:Label runat="server" ID="lblResponseCode"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="span4">Transaction ID  </td>
                        <td><asp:Label runat="server" ID="lblTransactionID"></asp:Label></td>
                    </tr>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlPaypalResponse" >
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
                    </asp:Panel>
                </table>
            </div>
        </asp:View>
    </asp:MultiView>
    <script type="text/javascript">
        jQuery(function ($) {
            $('#tblAutomatedPayments').dataTable(
                 {
                     
                     "aoColumnDefs": [
                         { 'bSortable': false, 'aTargets': [9] }
                     ]
                 }
         );
        });
   </script>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminPages.Master" AutoEventWireup="true" CodeBehind="FinancialAccounting.aspx.cs" Inherits="WFTCloud.Admin.FinancialAccounting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>WFT Financial Accounting</title>
    <meta name="Description" content="Financial datas are managed here" />
    <meta name="Keywords" content="WFT Financial Accounting" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <asp:MultiView ID="mvContainer" runat="server" ActiveViewIndex="1">
        <asp:View ID="FAView" runat="server">
            <div class="row-fluid">
                <div class="span12">
                    <div class="row-fluid">
                        <div id="divFASuccessMessage" runat="server" visible="false" class="alert alert-block alert-success span6">
                            <button data-dismiss="alert" class="close" type="button">
                                <i class="icon-remove"></i>
                            </button>
                            <p>
                                <i class="icon-ok"></i>
                                Financial Accounting Process handled successfully.
                            </p>
                            </div>
                        <div class="row-fluid">
                            <div class="table-header">
                               WFT Cloud Refund Management      
                            </div>
                        </div>
                        <div role="grid" class="dataTables_wrapper">
                            <table class="table table-striped table-bordered table-hover dataTable">
                                <thead>
                                    <tr role="row">
                                        <th role="columnheader" class="center">InvoiceID</th>
                                        <th role="columnheader" class="center">TransactionID</th>
                                        <th role="columnheader" class="center">Credit Card</th>
                                        <th role="columnheader" class="center">Amount</th>
                                        <th role="columnheader" class="center">User Service ID</th>
                                        <th role="columnheader" class="center">Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rptrFinancialAccounting" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Eval("InvoiceID")%></td>
                                                <td><%# Eval("TransactionID")%></td>
                                                <td><%# Eval("Credit Card")%></td>
                                                <td><%# Eval("Amount")%></td>
                                                <td><%# Eval("User Service ID")%></td>
                                                <td>
                                                    <div class="action-buttons">
                                                        <a data-rel="USD" title="Refund Amount" href='FinancialAccounting.aspx?refundamount=<%# Eval("InvoiceID")%>' class="blue">
                                                            <i class="icon-usd"></i>
                                                        </a>
                                                    </div>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </asp:View>
        <asp:View ID="FADetails" runat="server">
            <div class="row-fluid">
                <div id="divFAErrorMessage" runat="server" visible="false" class="alert alert-block alert-success span6">
                    <button data-dismiss="alert" class="close" type="button">
                        <i class="icon-remove"></i>
                    </button>
                    <p>
                        <i class="icon-ok"></i>
                        Please Verify Your Details.
                    </p>
                </div>
                <a href="FinancialAccounting.aspx">&lt;&lt; Back&nbsp;</a>
                <div class="table-header">
                    Conform Your Refund Process
                </div>
                <table class="table table-hover dataTable table-bordered ">
                    <tr>
                        <td class="span2">Invoice ID
                        </td>
                        <td>
                            <asp:TextBox ID="txtInvoiceID" runat="server" ReadOnly="true">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Transaction ID</td>
                        <td>
                            <asp:TextBox ID="txtTransactionID" runat="server" ReadOnly="true">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Credit Card</td>
                        <td>
                            <asp:TextBox ID="txtCreditCard" runat="server" ReadOnly="true">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Amount</td>
                        <td>
                            <asp:TextBox ID="txtAmount" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">User Service ID</td>
                        <td>
                            <asp:TextBox ID="txtUserServiceID" runat="server" ReadOnly="true">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2"></td>
                        <td>
                            <asp:Button ID="btnConform" Text="Conform Request" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>

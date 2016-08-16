<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminPages.Master" AutoEventWireup="true" CodeBehind="CloudReports.aspx.cs" Inherits="WFTCloud.Admin.CloudReports" %>

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
                    <asp:ListItem Value="1">Service Wise Registration Report</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ID="rfvReportType" InitialValue="0" ControlToValidate="ddlReportType" ForeColor="Red"
                    ErrorMessage="Please select Report Type" ValidationGroup="Reports"></asp:RequiredFieldValidator>
            </td>
        </tr>
       
        <div runat="server" id="DivServiceList" visible="true">
            <tr>
                <td class="span4">Choose Category
                </td>
                <td>
                    <asp:DropDownList ID="ddlChooseCategory" runat="server" OnSelectedIndexChanged="ddlChooseCategory_SelectedIndexChanged" AutoPostBack="true">
                        <%--<asp:ListItem Text="" Value="" Selected="True"></asp:ListItem>--%>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="span2">For Service
                </td>
                <td>
                    <asp:DropDownList ID="ddlServices" runat="server">
                    </asp:DropDownList>
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
     

        <asp:View ID="VwServicewisePurchasedList" runat="server">
            <div class="table-header">
                <strong>Service wise Purchased 
                    <asp:Label runat="server" ID="lblServiceWisePurchasedTitle"></asp:Label></strong>
            </div>
            <div class="row-fluid">
                <div class="span12 widget-container-span">
                    <div class="widget-box">
                        <div class="widget-body">
                            <div class="widget-main padding-12">
                                <div class="tab-content">
                                    <asp:MultiView runat="server" ID="mvServiceWisePurchasedDetails" ActiveViewIndex="0">
                                        <asp:View runat="server" ID="vwServiceWisePurchasedDetails">
                                            <div class="row-fluid">
                                                <div class="span12">
                                                    <div class="row-fluid">

                                                        <div role="grid" class="dataTables_wrapper">
                                                            <div role="grid" class="dataTables_wrapper">

                                                                <asp:Repeater ID="rptrServiceWisePurchasedDetails" runat="server">
                                                                    <HeaderTemplate>
                                                                        <table id="tblServiceCate" class="table table-striped table-bordered table-hover dataTable">
                                                                            <thead>
                                                                                <tr role="row">
                                                                                    <th role="columnheader">Email ID</th>
                                                                                     <th role="columnheader">Category Name</th>
                                                                                    <th role="columnheader">Service Name</th>
                                                                                     <th role="columnheader">SAP User Name</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody role="alert">
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td><a href='mailto:<%# UserEmailID(Eval("UserProfileID").ToString()) %>'>
                                                                                        <asp:Label ID="UserMailID" runat="server" Text='<%# UserEmailID(Eval("UserProfileID").ToString()) %>'></asp:Label></a></td>
                                                                            <td><%# CategoryName(Eval("ServiceID").ToString())%></td>
                                                                            <td><%# ServiceName(Eval("ServiceID").ToString())%></td>
                                                                            <td><%# Eval("ServiceUserName").ToString()%></td>
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

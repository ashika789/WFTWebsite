<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminPages.Master" AutoEventWireup="true" CodeBehind="CouponManagement.aspx.cs" Inherits="WFTCloud.Admin.CouponManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Coupon Management</title>
    <meta name="description" content="List of coupons." />
    <meta name="keywords" content="WFT Coupons" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="mvContainer" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwList" runat="server">
            <div class="row-fluid">
                <div class="span12">
                    <div class="row-fluid">
                        <div id="divCatSuccessMessage" runat="server" visible="false" class="alert alert-block alert-success span6">
                            <button data-dismiss="alert" class="close" type="button">
                                <i class="icon-remove"></i>
                            </button>
                            <p>
                                <i class="icon-ok"></i>
                                Coupon changes saved successfully.
                            </p>
                        </div>
                        <div id="divCatErrorMessage" runat="server" visible="false" class="alert alert-error span6">
                            <button data-dismiss="alert" class="close" type="button">
                                <i class="icon-remove"></i>
                            </button>
                            <i class="icon-remove"></i>
                            <asp:Label ID="lblCatErrorMessage" runat="server" Text="Error saving coupon changes."></asp:Label>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="table-header">
                            Coupons	        
                        </div>
                        <div class="dataTables_wrapper">
                            <div role="grid" class="dataTables_wrapper">
                                <table id="tblCoupons" class="table table-striped table-bordered table-hover dataTable">
                                    <thead>
                                        <tr role="row">
                                            <th class="center" role="columnheader">
                                                <label>
                                                    <input type="checkbox" class="ace" />
                                                    <span class="lbl"></span>
                                                </label>
                                            </th>
                                            <th role="columnheader">Coupon Name</th>
                                            <th role="columnheader">Coupon Code</th>
                                            <th role="columnheader">Coupon Type</th>
                                            <th role="columnheader">Discount</th>
                                            <th role="columnheader">Validity In Days</th>
                                            <th role="columnheader">User Specific</th>
                                            <th role="columnheader">Service Specific</th>
                                            <th role="columnheader">Status</th>
                                            <th role="columnheader" style="text-align: center;">Options</th>
                                        </tr>
                                    </thead>
                                    <tbody role="alert">
                                        <asp:Repeater ID="rptrCoupons" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="center">
                                                        <asp:CheckBox ID="chkSelect" runat="server" class="ace" />
                                                    </td>
                                                    <td>
                                                        <a href='CouponManagement.aspx?showview=editcoupon&couponid=<%# Eval("CouponID")%>'>
                                                            <%# Eval("CouponName")%>
                                                        </a>
                                                        <asp:HiddenField ID="hdnCouponID" runat="server" Value='<%# Eval("CouponID")%>' />
                                                    </td>
                                                    <td><%# Eval("CouponCode")%></td>
                                                    <td><%# Eval("CouponType")%></td>
                                                    <td><%# Eval("Discount")%></td>
                                                    <td><%# Eval("ValidityInDays")%></td>
                                                    <td><%# Eval("UserSpecific")%></td>
                                                    <td><%# Eval("ServiceSpecific")%></td>
                                                    <td><%# WFTCloud.GeneralReusableMethods.GetStatusString(Eval("RecordStatus").ToString()) %></td>
                                                    <td>
                                                        <div class="action-buttons">
                                                            <a data-rel="tooltip" title="Edit Coupon Details" href='CouponManagement.aspx?showview=editcoupon&couponid=<%# Eval("CouponID")%>' class="green">
                                                                <i class="icon-pencil bigger-130"></i>
                                                            </a>
                                                            <a data-rel="tooltip" title="Activate/Deactivate" href='CouponManagement.aspx?<%# WFTCloud.GeneralReusableMethods.GetActivateDeactivateAction(Eval("RecordStatus").ToString()) %>=<%# Eval("CouponID")%>' class="blue">
                                                                <i class='<%# WFTCloud.GeneralReusableMethods.GetActivateDeactivateIcon(Eval("RecordStatus").ToString()) %>'></i>
                                                            </a>
                                                            <%--  <a data-rel="tooltip" title="Delete Coupon Details" href='CouponManagement.aspx?delete=<%# Eval("CouponID")%>' class="red">
                                    <i class="icon-trash bigger-130"></i>
                                </a>--%>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <asp:Button ID="btnAddNew" runat="server" Text="Add New"
                                class="btn btn-primary" OnClick="btnAddNew_Click" />
                            <asp:Button ID="btnActivate" runat="server" Text="Activate"
                                class="btn btn-info" OnClick="btnActivate_Click" />
                            <asp:Button ID="btnDeactivate" runat="server" Text="Deactivate"
                                class="btn btn-warning" OnClick="btnDeactivate_Click" />
                            <%--<asp:Button ID="btnDelete" runat="server" Text="Delete" 
                    class="btn btn-danger" onclick="btnDelete_Click"/>--%>
                            <asp:CheckBox ID="chkShowDeleted" runat="server" AutoPostBack="True" OnCheckedChanged="chkShowDeleted_CheckedChanged" />
                            <span class="label label-warning arrowed-right">Show De-Activated</span>
                        </div>
                    </div>
                </div>
            </div>
        </asp:View>
        <asp:View ID="vwEdit" runat="server">
            <div class="row-fluid">
                <div class="span12">
                    <div class="row-fluid">
                        <a href="CouponManagement.aspx">&lt;&lt; Back&nbsp;</a>
                        <div class="row-fluid">
                            <div id="lblSuccessMessage" runat="server" visible="false" class="alert alert-block alert-success span6">
                                <button data-dismiss="alert" class="close" type="button">
                                    <i class="icon-remove"></i>
                                </button>
                                <p>
                                    <i class="icon-ok"></i>
                                    Coupon changes saved successfully.
                                </p>
                            </div>
                            <div id="lblErrorMessage" runat="server" visible="false" class="alert alert-error span6">
                                <button data-dismiss="alert" class="close" type="button">
                                    <i class="icon-remove"></i>
                                </button>
                                <i class="icon-remove"></i>
                                <asp:Label ID="lblErrorMessageText" runat="server" Text="Error saving coupon details. Please try again later."></asp:Label>
                            </div>
                        </div>
                        <div class="table-header">
                            <asp:Label ID="lblTableHeader" runat="server" Text="Edit Coupon Details"></asp:Label>
                        </div>
                        <table class="table table-hover dataTable table-bordered ">
                            <tr>
                                <td class="span2">Coupon Name
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCouponName" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvCouponName" runat="server"
                                        ErrorMessage="Coupon Name" ControlToValidate="txtCouponName"
                                        Text="*" ForeColor="Red" ValidationGroup="CouponManagement"></asp:RequiredFieldValidator>
                                    <asp:HiddenField ID="hdnCouponID" runat="server" Value="0" />
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Coupon Code
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCouponCode" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvCouponCode" runat="server"
                                        ErrorMessage="Coupon Code" ControlToValidate="txtCouponCode"
                                        Text="*" ForeColor="Red" ValidationGroup="CouponManagement"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Coupon Type
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCouponTypes" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCouponTypes_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Discount %</td>
                                <td>
                                    <asp:TextBox ID="txtDiscount" runat="server" value="0.00"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvDiscount" runat="server"
                                        ErrorMessage="Coupon Discount" ControlToValidate="txtDiscount"
                                        Text="*" ForeColor="Red" ValidationGroup="CouponManagement"></asp:RequiredFieldValidator>
                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtDiscount" ErrorMessage="Discount % Sholud be less than 99%" ForeColor="Red" MaximumValue="95" MinimumValue="0" Type="Double" ValidationGroup="CouponManagement"></asp:RangeValidator>
                                </td>
                            </tr>
                            <asp:Panel runat="server" ID="pnlCouponCount" Visible="false">
                                <tr>
                                    <td class="span2">Coupon Count
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtCouponCount"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCouponCount" runat="server"
                                            ErrorMessage="*" ControlToValidate="txtCouponCount"
                                            Text="*" ForeColor="Red" ValidationGroup="CouponManagement"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revCouponCount" runat="server" ValidationGroup="CouponManagement" ControlToValidate="txtCouponCount" ErrorMessage="Enter number only" ForeColor="Red" ValidationExpression="^[0-9]*" />
                                    </td>
                                </tr>
                            </asp:Panel>
                            <%--   <tr>
                <td class="span2">Coupon Type
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtCouponType"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="rfvCouponType" runat="server" 
                    ErrorMessage="*" ControlToValidate="txtCouponType"
                    Text="*" ForeColor="Red" ValidationGroup="CouponManagement"></asp:RequiredFieldValidator>
                </td>
            </tr>--%>
                            <tr>
                                <td class="span2">Is Used
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkIsUsed" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">User Count
                                </td>
                                <td>
                                    <asp:TextBox ID="txtUserCount" runat="server" value="0"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvUserCount" runat="server"
                                        ErrorMessage="User Count" ControlToValidate="txtUserCount"
                                        Text="*" ForeColor="Red" ValidationGroup="CouponManagement"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revUserCount" runat="server" ValidationGroup="CouponManagement" ControlToValidate="txtUserCount" ErrorMessage="Enter number only" ForeColor="Red" ValidationExpression="^[0-9]*" />
                                </td>
                            </tr>
                            <%--<tr>
                <td class="span2">Validity in days
                </td>
                <td>
                    <asp:TextBox ID="txtValidityDays" runat="server" value="0"></asp:TextBox>
                </td>
            </tr>--%>
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <tr>
                                <td class="span2">Validity Date
                                </td>
                                <td>
                                    <asp:TextBox ID="txtValidityDate" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtValidityDate" PopupButtonID="txtValidityDate" runat="server" Format="dd-MMM-yyyy" />
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">For User
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlUsers" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
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
                            <tr>
                                <td class="span2">Coupon Status
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlRecordStatus" runat="server">
                                        <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="Inactive"></asp:ListItem>
                                        <asp:ListItem Value="-1" Text="Deleted"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2"></td>
                                <td>
                                    <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary" OnClick="btnSave_Click" ValidationGroup="CouponManagement" />
                                    <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-warning"
                                        ValidationGroup="vgReset" OnClick="btnReset_Click" />

                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </asp:View>
    </asp:MultiView>
    <script type="text/javascript">
        jQuery(function ($) {
            $('#tblCoupons').dataTable(
                 {
                     "aoColumnDefs": [
                         { 'bSortable': false, 'aTargets': [0, 9] }
                     ]
                 }
            );
        });
    </script>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminPages.Master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="WFTCloud.Admin.Settings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <title>Email Settings</title>
    <meta name="Description" content="All email id's are handled here for email notifications" />
    <meta name="Keywords" content="Email ID" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row-fluid">
                <div class="span12">
                    <div class="row-fluid">
                        <%--<a href="Settings.aspx">&lt;&lt; Back&nbsp;</a>--%>
                        <br />
                        <div class="row-fluid">
                               <div id="divEmailSuccess" runat="server" visible="false" class="alert alert-block alert-success span6">
                                            <button data-dismiss="alert" class="close" type="button">
                                                <i class="icon-remove"></i>
                                            </button>
                                            <p>
                                                <i class="icon-ok"></i>
                                                Administrator Settings updates successfully.
                                            </p>
                                        </div>
                                        <div id="divEmailError" runat="server" visible="false" class="alert alert-error span6">
				                        <button data-dismiss="alert" class="close" type="button">
					                        <i class="icon-remove"></i>
				                        </button>
                                        <i class="icon-remove"></i>
                                        <asp:Label ID="lblEmailError" runat="server"></asp:Label>
                                    </div>
                        </div>
                        <div class="table-header">
                            Email Settings
                        </div>
                        <table class="table table-hover dataTable table-bordered ">
                            <tr>
                                <td class="span4">Admin Email</td>
                                <td>
                                    <asp:TextBox ID="txtAdminEmail" runat="server" CssClass="span10">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvNewTitle" runat="server" ControlToValidate="txtAdminEmail" ErrorMessage="*" ForeColor="Red"
                                         ValidationGroup="ManageEmail"></asp:RequiredFieldValidator><br />
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Support Team Email</td>
                                <td>
                                    <asp:TextBox ID="txtSupportTeam" runat="server" CssClass="span10">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtSupportTeam" runat="server" ControlToValidate="txtSupportTeam" ErrorMessage="*"
                                         ForeColor="Red" ValidationGroup="ManageEmail"></asp:RequiredFieldValidator><br />
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Sales Team Email</td>
                                <td>
                                    <asp:TextBox ID="txtSalesEmail" runat="server" CssClass="span10">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvSalesEmail" runat="server" ControlToValidate="txtSalesEmail" ErrorMessage="*"
                                         ForeColor="Red" ValidationGroup="ManageEmail"></asp:RequiredFieldValidator><br />
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Technical Team Email</td>
                                <td>
                                    <asp:TextBox ID="txtTechnicalEmail" runat="server" CssClass="span10">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvTechnicalEmail" runat="server" ControlToValidate="txtTechnicalEmail" ErrorMessage="*"
                                         ForeColor="Red" ValidationGroup="ManageEmail"></asp:RequiredFieldValidator><br />
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Maintenance Email</td>
                                <td>
                                    <asp:TextBox ID="txtMaintenanceEmail" runat="server" CssClass="span10">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvMaintenanceEmail" runat="server" ControlToValidate="txtMaintenanceEmail" ErrorMessage="*"
                                         ForeColor="Red" ValidationGroup="ManageEmail"></asp:RequiredFieldValidator><br />
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">SAP Basis Email</td>
                                <td>
                                    <asp:TextBox ID="txtSAPBasisEmail" runat="server" CssClass="span10">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvSAPBasisEmail" runat="server" ControlToValidate="txtSAPBasisEmail" ErrorMessage="*"
                                         ForeColor="Red" ValidationGroup="ManageEmail"></asp:RequiredFieldValidator><br />
                                </td>
                            </tr>
                        </table>
                        <div class="table-header">
                            Other Settings
                        </div>
                        <table class="table table-hover dataTable table-bordered ">
                            <tr runat="server" id="trHrMonth" visible="false">
                                <td class="span4">Hours/Month</td>
                                <td>
                                    <asp:TextBox ID="txtHoursPerMonth" runat="server">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvHoursPerMonth" runat="server" ControlToValidate="txtHoursPerMonth" ErrorMessage="*" ForeColor="Red"
                                         ValidationGroup="ManageEmail"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revHoursPerMonth" runat="server" ValidationGroup="ManageEmail" ControlToValidate="txtHoursPerMonth" ErrorMessage="Enter Numbers only" ForeColor="Red" ValidationExpression="^[0-9]*" />
                                </td>
                            </tr>
                            <tr runat="server" id="trDaysMonth" visible="false">
                                <td class="span4">Days/Month</td>
                                <td>
                                    <asp:TextBox ID="txtDaysPerMonth" runat="server">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvDaysPerMonth" runat="server" ControlToValidate="txtDaysPerMonth" ErrorMessage="*"
                                         ForeColor="Red" ValidationGroup="ManageEmail"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revDaysPerMonth" runat="server" ValidationGroup="ManageEmail" ControlToValidate="txtDaysPerMonth" ErrorMessage="Enter Numbers only" ForeColor="Red" ValidationExpression="^[0-9]*" />
                                </td>
                            </tr>
                            <tr>
                                <td class="span4">Application URL</td>
                                <td>
                                    <asp:TextBox ID="txtAppURL" runat="server">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAppURL" ErrorMessage="*"
                                         ForeColor="Red" ValidationGroup="ManageEmail"></asp:RequiredFieldValidator>

                                </td>
                            </tr>
                        </table>
                         <div class="table-header">
                            Site Setting
                        </div>
                        <table class="table table-hover dataTable table-bordered ">
                            <tr>
                                <td class="span4">Site Down For Maintenance</td>
                                <td>
                                    <asp:CheckBox runat="server" ID="chkSiteDownForMaintenance"/>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnNewSave" runat="server" Text="Submit"  OnClientClick="return ConfirmOnDelete()" class="btn btn-primary" ValidationGroup="ManageEmail" OnClick="btnNewSave_Click"/>
                                    <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-warning" OnClick="btnReset_Click" />
                                </td>
                            </tr>
                            </table>
                    </div>
                </div>
            </div>
        <script type="text/javascript">
            function ConfirmOnDelete() {
                var cb = document.getElementById('ContentPlaceHolder1_chkSiteDownForMaintenance');
                if (cb.checked) {
                    if (confirm("Are You Sure to down site for maintainance?") == true)
                        return true;
                    else
                        return false;
                }
            }
    </script>
</asp:Content>

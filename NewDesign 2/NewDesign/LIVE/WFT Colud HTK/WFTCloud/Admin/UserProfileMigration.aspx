<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminPages.Master" AutoEventWireup="true" CodeBehind="UserProfileMigration.aspx.cs" Inherits="WFTCloud.Admin.UserProfileMigration" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>User Email Migration</title>
    <meta name="description" content="List of Users can be seen here and we have an options of Check services, Add new services, Access acount, Delete User, Manage CRM issue and View User Profile" />
    <meta name="keywords" content="WFT Users list" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField runat="server" ID="hidReportType" />
    <div class="row-fluid">
        <div class="span12 widget-container-span">
            <div class="widget-box">
                <div class="widget-header no-border">
                    <ul class="nav nav-tabs" id="Ul1">
                        <li id="liManage" runat="server" class="active">
                            <a>User Information</a>
                        </li>
                    </ul>
                </div>
                <div class="widget-body">
                    <div class="widget-main padding-12">
                        <div class="tab-content">
                            <div id="Manage" class="tab-pane in active" runat="server">
                                <asp:MultiView ID="mvSubscriptionDetails" runat="server" ActiveViewIndex="1">
                                    <asp:View ID="vwSubscriptionHistroy" runat="server">
                                        <div class="row-fluid">
                                        </div>
                                    </asp:View>
                                    <asp:View ID="vmUserPaymentHistoryDetails" runat="server">
                                        <div id="divEmailSuccess" runat="server" visible="false" class="alert alert-block alert-success">
                                            <button data-dismiss="alert" class="close" type="button">
                                                <i class="icon-remove"></i>
                                            </button>
                                            <p>
                                                <i class="icon-ok"></i>
                                                Email update successfully!!!
                                            </p>
                                        </div>
                                        <h4><a href='/Admin/UsersServiceHistory.aspx'><< Back</a></h4>
                                        <div class="table-header">
                                            <strong>Account details of
                                            <asp:Label ID="lblUserNamefor" runat="server"></asp:Label>
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
                                                <td>New Email Address</td>
                                                <td>
                                                    <asp:TextBox ID="txtNewEmailAddress" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvConfirmEmail" runat="server" ErrorMessage="Enter new email" ControlToValidate="txtNewEmailAddress" Text="*" ValidationGroup="Email" ForeColor="Red">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td></td>
                                                <td>
                                                    <asp:Button runat="server" ID="btnUpdateEmailAddress" CssClass="btn btn-danger" ValidationGroup="Email" Text="Submit" OnClick="btnUpdateEmailAddress_Click" />
                                                </td>

                                            </tr>
                                        </table>
                                        <br />


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

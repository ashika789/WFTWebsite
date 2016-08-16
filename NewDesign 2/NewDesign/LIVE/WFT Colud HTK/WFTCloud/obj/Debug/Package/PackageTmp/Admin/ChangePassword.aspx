<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminPages.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="WFTCloud.Admin.ChangePassword" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .controls {
            margin-top: 0px;
        }
    </style>
    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="scriptManager" runat="server"></asp:ScriptManager>
            <asp:ModalPopupExtender ID="mpopupCanCelServices" runat="server" TargetControlID="hfCancel" BackgroundCssClass="modalBackground" PopupControlID="divCancelServices" CancelControlID="lktempButton"></asp:ModalPopupExtender>
        <asp:HiddenField ID="hfCancel" runat="server" />
        <div id="divCancelServices" runat="server">
            <asp:Panel runat="server" ID="pnlCancel" Class="span7">
                <asp:LinkButton runat="server" ID="lktempButton" ></asp:LinkButton>
                <div id="login-box" class="login-box visible widget-box no-border">
                    <div class="widget-body">
                        <div class="toolbar clearfix">
                            <div>
                            </div>
                        </div>
                        <div class="widget-main">
                            <h4 class="header blue lighter bigger">
                                <i class="icon-coffee green"></i>
                                Please Change Your Password</h4>

                            <div class="space-6"></div>


                                                            <div class="span8">
                                    <div class="row-fluid">
                                        <div class="dataTables_wrapper">
                                            <table class="table table-hover dataTable table-bordered span9" id="tblChangePassword">
                                                <tr>
                                                    <td class="span4">New Password</td>
                                                    <td class="span8">
                                                        <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" ValidationGroup="Password"></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ErrorMessage="Please Enter New Password" ForeColor="Red" ControlToValidate="txtNewPassword" ValidationGroup="Password"></asp:RequiredFieldValidator>--%>
                                                        <asp:RequiredFieldValidator ID="rfvOldPassword0" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtNewPassword" ValidationGroup="Password"></asp:RequiredFieldValidator>
                                                        <br />
                                                        <asp:RegularExpressionValidator ID="valPassword" runat="server" ValidationGroup="Password" ControlToValidate="txtNewPassword" ErrorMessage="Minimum 6 Character Required"  ForeColor="Red" ValidationExpression=".{6}.*" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="span2"  style="vertical-align:top;">Confirm Password</td>
                                                    <td>
                                                        <asp:TextBox ID="txtConformPassword" runat="server" TextMode="Password" ValidationGroup="Password" ></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvOldPassword1" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtConformPassword" ValidationGroup="Password"></asp:RequiredFieldValidator>
                                                        <br />
                                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="txtNewPassword" ControlToValidate="txtConformPassword" ErrorMessage="Your New Password & Confirm Password did not match." ForeColor="Red" ValidationGroup="Password" ></asp:CompareValidator>
                                                        <%--<asp:RequiredFieldValidator ID="rfvConformPassword" runat="server" ErrorMessage="Please Enter Confirm Password" ForeColor="Red" ControlToValidate="txtConformPassword" ValidationGroup="Password"></asp:RequiredFieldValidator>--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="span2"></td>
                                                    <td>
                                                       <asp:Button ID="btnSavePassword" runat="server" align="center" OnClick="btnSavePassword_Click" class="btn btn-primary" Text="Save Password" ValidationGroup="Password" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>

                            <div class="clearfix" style="text-align: center;">
                               <%-- <a data-rel="tooltip" style="float:right;"  title="Cancel Service"  class="btn btn-danger btn-small">
                                   
                                </a>--%>

                            </div>
                            <div class="space-4"></div>


                        </div>
                        <!--/widget-main-->
                        <div class="toolbar clearfix">
                            <div>
                            </div>
                        </div>

                    </div>
                    <!--/widget-body-->
                </div>
                <!--/login-box-->
            </asp:Panel>


        </div>
</asp:Content>

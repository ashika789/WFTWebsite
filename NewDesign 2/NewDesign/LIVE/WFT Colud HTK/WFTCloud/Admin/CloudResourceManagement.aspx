<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminPages.Master" AutoEventWireup="true" CodeBehind="CloudResourceManagement.aspx.cs" Inherits="WFTCloud.Admin.CloudResourceManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <title>Cloud Resource Management</title>
    <meta name="Description" content="Cloud Resources are Managed Here." />
    <meta name="Keywords" content="WFT Cloud Resource Management" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="mvContainer" runat="server" ActiveViewIndex="0">
        <asp:View ID="CRMView" runat="server">
            <div class="row-fluid">
                <div class="span12">
                    <div class="row-fluid">
                        <div id="divCloudSuccessMessage" runat="server" visible="false" class="alert alert-block alert-success span6">
                            <button data-dismiss="alert" class="close" type="button">
                                <i class="icon-remove"></i>
                            </button>
                            <p>
                                <i class="icon-ok"></i>
                                Cloud resources status updated successfully.
                            </p>
                        </div>
                        </div>
                        <div class="row-fluid">
                            <div class="table-header">
                                Cloud Resource Management
                            </div>
                        <div role="grid" class="dataTables_wrapper">
                            <div role="grid" class="dataTables_wrapper">
                            <table id="tblCloudResourceManagement" class="table table-striped table-bordered table-hover dataTable">
                                <thead>
                                    <tr role="row">
                                        <th class="center" role="columnheader">
                                            <label><input type="checkbox" class="ace"/>
                                                <span class="lbl"></span>
                                            </label>
                                        </th>
                                        <th role="columnheader" class="center">Title</th>
                                        <th role="columnheader" class="center">Link</th>
                                        <th role="columnheader" class="center">User Type</th>
                                        <th role="columnheader" class="center">Staus</th>
                                        <th role="columnheader" class="center" style="text-align:center;">Options</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rptrCloudResourceManagement" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="center">
                                                    <asp:CheckBox ID="chkSelect" runat="server" class="ace" />
                                                </td>
                                                <td><%# Eval("Title")%><asp:HiddenField ID="hdnWftCloudID" runat="server" Value='<%# Eval("ResourceID")%>' /></td>
                                                <td><a href="<%# Eval("Path")%>" target="_blank"><%# Eval("Path")%></a></td>
                                                <td><%# Eval("WFTUserTypeText")%></td>
                                                <td><%# WFTCloud.GeneralReusableMethods.GetStatusString(Eval("RecordStatus").ToString()) %></td>
                                                <td>
                                                <div class="action-buttons">
                                                    <a data-rel="tooltip" title="Edit Wft Cloud Resource" href='CloudResourceManagement.aspx?editWftCloudid=<%# Eval("ResourceID")%>' class="green">
                                                        <i class="icon-pencil bigger-130"></i>
                                                    </a>
                                                    <a data-rel="tooltip" title="Activate/Deactivate" href='CloudResourceManagement.aspx?<%# WFTCloud.GeneralReusableMethods.GetActivateDeactivateAction(Eval("RecordStatus").ToString()) %>=<%# Eval("ResourceID")%>' class="blue">
                                                        <i class='<%# WFTCloud.GeneralReusableMethods.GetActivateDeactivateIcon(Eval("RecordStatus").ToString()) %>'></i>
                                                    </a>
                                                    <%--<a data-rel="tooltip" title="Delete Wft Cloud Resource" href='CloudResourceManagement.aspx?delete=<%# Eval("ResourceID")%>' class="red">
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
                            <asp:Button ID="btnAddNewCloudResorce" runat="server" Text="Add New Cloud Resource" 
                                class="btn btn-primary" OnClick="btnAddNewCloudResorce_Click" />
                            <asp:Button ID="btnActivate" runat="server" Text="Activate" 
                                class="btn btn-info" OnClick="btnActivate_Click"/>
                            <asp:Button ID="btnDeactivate" runat="server" Text="Deactivate" 
                                class="btn btn-warning" OnClick="btnDeactivate_Click" />
                            <%--<asp:Button ID="btnDelete" runat="server" Text="Delete" 
                                class="btn btn-danger" OnClick="btnDelete_Click" />--%>
                            <asp:CheckBox ID="chkShowDeleted" runat="server" AutoPostBack="True" OnCheckedChanged="chkShowDeleted_CheckedChanged" />
                            <span class="label label-warning arrowed-right">Show De-Activated</span>
                        </div>
                    </div>
                  </div>
                </div>
        </asp:View>
        <asp:View ID="CRMEdit" runat="server">
            <div class="row-fluid">
                <div class="span12">
                    <div class="row-fluid">
                        <a href="CloudResourceManagement.aspx">&lt;&lt; Back&nbsp;</a>
                        <br />
                        <div class="row-fluid">
                             <div id="divEditCloudResourceManagement" runat="server" visible="false" class="alert alert-block alert-success span6">
                                            <button data-dismiss="alert" class="close" type="button">
                                                <i class="icon-remove"></i>
                                            </button>
                                            <p>
                                                <i class="icon-ok"></i>
                                                Cloud Resources updated successfully.
                                            </p>
                                        </div>
                                        <div id="divEditCloudResourceError" runat="server" visible="false" class="alert alert-error span6">
				                        <button data-dismiss="alert" class="close" type="button">
					                        <i class="icon-remove"></i>
				                        </button>
                                        <i class="icon-remove"></i>
                                        <asp:Label ID="lblEditCloudResourceError" runat="server"></asp:Label>
                                    </div>
                        </div>
                        <div class="table-header">
                            Edit Cloud Resource Management
                        </div>
                        <table class="table table-hover dataTable table-bordered ">
                            <tr>
                                <td class="span2">Title</td>
                                <td>
                                    <asp:TextBox ID="txtTitle" runat="server">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle" ErrorMessage="*" ForeColor="Red" ValidationGroup="CloudResource"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Link</td>
                                <td>
                                    <asp:TextBox ID="txtLink" runat="server">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvLink" runat="server" ControlToValidate="txtLink" ErrorMessage="*"
                                         ForeColor="Red" ValidationGroup="CloudResource"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Status</td>
                                <td>
                                    <asp:DropDownList ID="ddlStatus" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">User Type</td>
                                <td>
                                    <asp:DropDownList ID="ddlUserType" runat="server">
                                        <asp:ListItem Value="PersonalUser">Personal User</asp:ListItem>
                                        <asp:ListItem Value="BusinessUser">Business User</asp:ListItem>
                                        <asp:ListItem Value="EnterpriseUser">Enterprise User</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn-primary" ValidationGroup="CloudResource" OnClick="btnSubmit_Click" />
                                       
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </asp:View>
        <asp:View ID="CRMNew" runat="server">
            <div class="row-fluid">
                <div class="span12">
                    <div class="row-fluid">
                        <a href="CloudResourceManagement.aspx">&lt;&lt; Back&nbsp;</a>
                        <br />
                        <div class="row-fluid">
                               <div id="divNewCloudResourceSuccess" runat="server" visible="false" class="alert alert-block alert-success span6">
                                            <button data-dismiss="alert" class="close" type="button">
                                                <i class="icon-remove"></i>
                                            </button>
                                            <p>
                                                <i class="icon-ok"></i>
                                                Cloud Resources added successfully.
                                            </p>
                                        </div>
                                        <div id="divNewCloudResourceError" runat="server" visible="false" class="alert alert-error span6">
				                        <button data-dismiss="alert" class="close" type="button">
					                        <i class="icon-remove"></i>
				                        </button>
                                        <i class="icon-remove"></i>
                                        <asp:Label ID="lblNewCloudResourceError" runat="server"></asp:Label>
                                    </div>
                        </div>
                        <div class="table-header">
                            New Cloud Resource Management
                        </div>
                        <table class="table table-hover dataTable table-bordered ">
                            <tr>
                                <td class="span2">Title</td>
                                <td>
                                    <asp:TextBox ID="txtNewTitle" runat="server">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvNewTitle" runat="server" ControlToValidate="txtNewTitle" ErrorMessage="*" ForeColor="Red" ValidationGroup="NewCloudResource"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Link</td>
                                <td>
                                    <asp:TextBox ID="txtNewLink" runat="server">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvNewlink" runat="server" ControlToValidate="txtNewLink" ErrorMessage="*"
                                         ForeColor="Red" ValidationGroup="NewCloudResource"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Status</td>
                                <td>
                                    <asp:DropDownList ID="ddlNewStatus" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">User Type</td>
                                <td>
                                    <asp:DropDownList ID="ddlNewUserType" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnNewSave" runat="server" Text="Submit" class="btn btn-primary" ValidationGroup="NewCloudResource" OnClick="btnNewSave_Click" />
                                    <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-warning" OnClick="btnReset_Click" />
                                     
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
            $('#tblCloudResourceManagement').dataTable(
                {
                    "aoColumnDefs": [
                        { 'bSortable': false, 'aTargets': [0, 5] }
                    ]
                }
         );
        });
   </script>
</asp:Content>

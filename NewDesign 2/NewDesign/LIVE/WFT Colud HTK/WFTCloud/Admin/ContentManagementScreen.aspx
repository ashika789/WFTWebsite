<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminPages.Master" AutoEventWireup="true" CodeBehind="ContentManagementScreen.aspx.cs" Inherits="WFTCloud.Admin.ContentManagementScreen" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <title>ContentManagementScreen</title>
    <meta name="Description" content="Contents of the Label are managed here." />
    <meta name="Keywords" content="WFT Content Management" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="mvContainer" runat="server" ActiveViewIndex="0">
        <asp:View ID="CMView" runat="server">
            <div class="row-fluid">
                <div class="span12">
                    <div class="row-fluid">
                        <div id="divCMSuccessMessage" runat="server" visible="false" class="alert alert-block alert-success span6">
                            <button data-dismiss="alert" class="close" type="button">
                                <i class="icon-remove"></i>
                            </button>
                            <p>
                                <i class="icon-ok"></i>
                               Content updated successfully.
                            </p>
                        </div>
                        </div>
                    <div class="row-fluid">
                            <div class="table-header">
                                Content Management  
                            </div>
                        <div class="dataTables_wrapper">
                            <div role="grid" class="dataTables_wrapper">
                                <table id="tblContentManagement" class="table table-striped table-bordered table-hover dataTable">
                                    <thead>
                                        <tr role="row">
                                            <th class="center" role="columnheader">
                                                <label><input type="checkbox" class="ace"/>
                                                    <span class="lbl"></span>
                                                </label>
                                            </th>
                                            <th role="columnheader" class="center">Page Name</th>
                                            <th role="columnheader" class="center">Page Title</th>
                                            <th role="columnheader" class="center">Language</th>
                                            <th role="columnheader" class="center">Record Status</th>
                                            <th role="columnheader" class="center" style="text-align:center;">Options</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rptrContentManagement" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="center">
                                                        <asp:CheckBox ID="chkSelect" runat="server" class="ace"/>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PageRelativeUrl")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PageTitle")%>
                                                        <asp:HiddenField ID="hdnCMSID" runat="server" Value='<%# Eval("PageID")%>' />
                                                    </td>
                                                    <td><%# Eval("Language") %></td>
                                                    <td><%# WFTCloud.GeneralReusableMethods.GetStatusString(Eval("RecordStatus").ToString()) %></td>
                                                    <td>
                                                        <div class="action-buttons">
                                                            <a data-rel="tooltip" title="Edit CMS" href='ContentManagementScreen.aspx?editcmsid=<%# Eval("PageID")%>' class="green">
                                                                <i class="icon-pencil bigger-130"></i>
                                                            </a>
                                                            <a data-rel="tooltip" title="Activate/Deactivate" href='ContentManagementScreen.aspx?<%# WFTCloud.GeneralReusableMethods.GetActivateDeactivateAction(Eval("RecordStatus").ToString()) %>=<%# Eval("PageID")%>' class="blue">
                                                                <i class='<%# WFTCloud.GeneralReusableMethods.GetActivateDeactivateIcon(Eval("RecordStatus").ToString()) %>'></i>
                                                            </a>
                                                           <%-- <a data-rel="tooltip" title="Delete CMS" href='ContentManagementScreen.aspx?delete=<%# Eval("PageID")%>' class="red">
                                                                <i class="icon-trash bigger-130"></i>
                                                            </a>--%>
                                                        </div>
                                                    </td>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                     </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <asp:Button ID="btnAddNewCMS" runat="server" Text="Add New CMS" 
                                 class="btn btn-primary" OnClick="btnAddNewCMS_Click1" />
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
        <asp:View ID="CMEdit" runat="server">
            <div class="row-fluid">
                <div class="span12">
                    <div class="row-fluid">
                         <a href="ContentManagementScreen.aspx">&lt;&lt; Back&nbsp;</a>
                        <br />
                        <div class="row-fluid">
                             <div id="divEditContentSuccess" runat="server" visible="false" class="alert alert-block alert-success span6">
                                        <button data-dismiss="alert" class="close" type="button">
                                            <i class="icon-remove"></i>
                                        </button>
                                        <p>
                                            <i class="icon-ok"></i>
                                            Content updated successfully.
                                        </p>
                                    </div>
                        </div>
                        <div class="table-header">
                            Edit Content Management
                        </div>
                        <table class="table table-hover dataTable table-bordered ">
                           <%-- <tr>
                                <td class="span2">Language</td>
                                <td>
                                      <asp:DropDownList ID="ddlEditLanguage" runat="server" AppendDataBoundItems="True">
                                            <asp:ListItem><--Select Language--></asp:ListItem>
                                            <asp:ListItem Value="English">English</asp:ListItem>
                                            <asp:ListItem Value="Spanish">Spanish</asp:ListItem>
                                        </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlEditLanguage" ValidationGroup="ContentManagement" InitialValue="<--Select Language-->" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>--%>
                              <tr>
                                <td class="span2">Language</td>
                                <td>
                                    <asp:Label ID="lblEditLanguage" runat="server" Text="-" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Page Name</td>
                                <td>
                                    <asp:TextBox ID="txtPageName" runat="server" Font-Size="Medium" class="span4"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvPageName" ControlToValidate="txtPageName" ValidationGroup="ContentManagement" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                     <asp:RegularExpressionValidator ID="revPageName" runat="server" ControlToValidate="txtPageName" ErrorMessage="Page Name should end with .aspx format" 
                                        ValidationExpression="\w+([-+.']\w+)*\.aspx*" ValidationGroup="ContentManagement" ForeColor="Red"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Page Title</td>
                                <td>
                                    <asp:TextBox ID="txtPageTitle" runat="server" Font-Size="Medium" class="span10"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvPageTitle" ControlToValidate="txtPageTitle" ValidationGroup="ContentManagement" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Page Meta Keyword</td>
                                <td>
                                    <asp:TextBox ID="txtMetaKeyword" runat="server" Font-Size="Medium" Height="100px" TextMode="MultiLine" class="span10"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvMetaKeyword" ControlToValidate="txtMetaKeyword" ValidationGroup="ContentManagement" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Page Meta Description</td>
                                <td>
                                    <asp:TextBox ID="txtMetaDescription" runat="server" Font-Size="Medium" Height="100px" TextMode="MultiLine" class="span10"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvMetaDescription" ControlToValidate="txtMetaDescription" ValidationGroup="ContentManagement" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Zone Name</td>
                                <td>
                                    <asp:TextBox ID="txtZoneName" runat="server" Font-Size="Medium" class=""></asp:TextBox>
                                </td>
                            </tr>
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                          <%--  <tr>
                                <td class="span2">Content</td>
                                <td>
                                    <asp:TextBox ID="txtEditContent" runat="server" Font-Size="Medium" TextMode="MultiLine" class="span10" Height="300px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvEditContent" ControlToValidate="txtEditContent" ValidationGroup="ContentManagement" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:HtmlEditorExtender ID="HtmlEditorEditContent" runat="server" TargetControlID="txtEditContent" DisplaySourceTab="true">
                                    </asp:HtmlEditorExtender>       
                                </td>
                            </tr>--%>
                            <tr>
                                <td class="span2">HTML Content</td>
                                <td>
                                    <asp:TextBox ID="txtEditHtmlContent" runat="server" Font-Size="Medium" TextMode="MultiLine" class="span10" Height="300px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtEditHtmlContent" ValidationGroup="ContentManagement" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>      
                                </td>
                            </tr>
                            <tr>
                                <td class="span2"></td>
                                <td>
                                    <asp:Button ID="btnSaveCMS" runat="server" Text="Save" class="btn btn-primary" OnClick="btnSaveCMS_Click" ValidationGroup="ContentManagement" />
                                    <asp:Button ID="btnPreview" runat="server" Text="Preview" class="btn btn-warning" OnClick="btnPreview_Click" ValidationGroup="ContentManagement" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </asp:View>
        <asp:View ID="vwAddNewCMS" runat="server">
            <div class="row-fluid">
                <div class="span12">
                    <div class="row-fluid">
                         <a href="ContentManagementScreen.aspx">&lt;&lt; Back&nbsp;</a>
                        <br />
                        <div class="row-fluid">
                              <div id="divAddNewCmsSuccess" runat="server" visible="false" class="alert alert-block alert-success span6">
                                        <button data-dismiss="alert" class="close" type="button">
                                            <i class="icon-remove"></i>
                                        </button>
                                        <p>
                                            <i class="icon-ok"></i>
                                            Content Management Added Successfully.
                                        </p>
                                    </div>
                        </div>
                        <div class="table-header">
                            Add New Content Management
                        </div>
                        <table class="table table-hover dataTable table-bordered ">
                             <tr>
                                <td class="span2">Language</td>
                                <td>
                                      <asp:DropDownList ID="ddlMewLanguage" runat="server" AppendDataBoundItems="True">
                                            <asp:ListItem><--Select Language--></asp:ListItem>
                                            <asp:ListItem Value="English">English</asp:ListItem>
                                            <asp:ListItem Value="Spanish">Spanish</asp:ListItem>
                                        </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ddlMewLanguage" ValidationGroup="AddNewContentManagement" InitialValue="<--Select Language-->" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Page Name</td>
                                <td>
                                    <asp:TextBox ID="txtAddNewPageName" runat="server" Font-Size="Medium" Placeholder="NewPageName.aspx" class="span4"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvAddNewPageName" ControlToValidate="txtAddNewPageName" ValidationGroup="AddNewContentManagement" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revAddNewPageName" runat="server" ControlToValidate="txtAddNewPageName" ErrorMessage="Page Name should end with .aspx format" 
                                        ValidationExpression="\w+([-+.']\w+)*\.aspx*" ValidationGroup="AddNewContentManagement" ForeColor="Red"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Page Title</td>
                                <td>
                                    <asp:TextBox ID="txtAddNewPageTitle" runat="server" Font-Size="Medium" class="span10"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvAddNewPageTitle" ControlToValidate="txtAddNewPageTitle" ValidationGroup="AddNewContentManagement" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Page Meta Keyword</td>
                                <td>
                                    <asp:TextBox ID="txtAddNewMetaKeyword" runat="server" Font-Size="Medium" TextMode="MultiLine" Height="100px" class="span10"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvAddNewMetaKeyword" ControlToValidate="txtAddNewMetaKeyword" ValidationGroup="AddNewContentManagement" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Page Meta Description</td>
                                <td>
                                    <asp:TextBox ID="txtAddnewMetaDescription" runat="server" Font-Size="Medium" TextMode="MultiLine" Height="100px" class="span10"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvAddnewMetaDescription" ControlToValidate="txtAddnewMetaDescription" ValidationGroup="AddNewContentManagement" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Zone Name</td>
                                <td>
                                    <asp:TextBox ID="txtAddnewZoneName" runat="server" Font-Size="Medium" class=""></asp:TextBox>
                                </td>
                            </tr>
                           <%-- <tr>
                                <td class="span2">Content</td>
                                <td>
                                    <asp:TextBox ID="txtAddNewContent" runat="server" Font-Size="Medium" TextMode="MultiLine" class="span10" Height="300px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvAddNewContent" ControlToValidate="txtAddNewContent" ValidationGroup="AddNewContentManagement" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                     <asp:HtmlEditorExtender ID="HtmlEditorAddNewContent" runat="server" TargetControlID="txtAddNewContent" DisplaySourceTab="true">
                                    </asp:HtmlEditorExtender> 
                                </td>
                            </tr>--%>
                            <tr>
                                <td class="span2">HTML Content</td>
                                <td>
                                    <asp:TextBox ID="txtAddNewHTMLContent" runat="server" Font-Size="Medium" TextMode="MultiLine" class="span10" Height="300px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtAddNewHTMLContent" ValidationGroup="AddNewContentManagement" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>      
                                </td>
                            </tr>
                            <tr>
                                <td class="span2"></td>
                                <td>
                                    <asp:Button ID="btnAddNewSaveCMS" runat="server" Text="Save" OnClick="btnAddNewSaveCMS_Click" class="btn btn-primary" ValidationGroup="AddNewContentManagement" />
                                    <asp:Button ID="btnAddNewPreview" runat="server" Text="Preview" class="btn btn-warning" OnClick="btnAddNewPreview_Click"  ValidationGroup="AddNewContentManagement"  />
                                  
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
            $('#tblContentManagement').dataTable(
                {
                    "aoColumnDefs": [
                        { 'bSortable': false, 'aTargets': [0, 5] }
                    ]
                }
         );
        });
   </script>
</asp:Content>

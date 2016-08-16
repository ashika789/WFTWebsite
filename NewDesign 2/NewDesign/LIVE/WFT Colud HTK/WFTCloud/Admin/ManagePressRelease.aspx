<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminPages.Master" AutoEventWireup="true" CodeBehind="ManagePressRelease.aspx.cs" Inherits="WFTCloud.Admin.ManagePressRelease" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>PressReleaseScreen</title>
    <meta name="Description" content="Contents of the press release are managed here." />
    <meta name="Keywords" content="Press Release" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <asp:MultiView ID="mvContainer" runat="server" ActiveViewIndex="0">
        <asp:View ID="CMView" runat="server">
            <div class="row-fluid">
                <div class="span12">
                    <div class="row-fluid">
                        <div id="divPRSuccessMessage" runat="server" visible="false" class="alert alert-block alert-success span6">
                            <button data-dismiss="alert" class="close" type="button">
                                <i class="icon-remove"></i>
                            </button>
                            <p>
                                <i class="icon-ok"></i>
                               Press Release updated successfully.
                            </p>
                        </div>
                        </div>
                    <div class="row-fluid">
                            <div class="table-header">
                                Press Release Management  
                            </div>
                        <div class="dataTables_wrapper">
                            <div role="grid" class="dataTables_wrapper">
                                <table id="tblPressReleaseManagement" class="table table-striped table-bordered table-hover dataTable">
                                    <thead>
                                        <tr role="row">
                                            <th class="center" role="columnheader">
                                                <label><input type="checkbox" class="ace"/>
                                                    <span class="lbl"></span>
                                                </label>
                                            </th>
                                            <th role="columnheader" class="center">Company Name</th>
                                            <th role="columnheader" class="center">Press Release Date</th>
                                            <th role="columnheader" class="center">Place Name</th>
                                            <th role="columnheader" class="center">Record Status</th>
                                            <th role="columnheader" class="center" style="text-align:center;">Options</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rptrPressRelease" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="center">
                                                        <asp:CheckBox ID="chkSelect" runat="server" class="ace"/>
                                                    </td>
                                                    <td>
                                                        <%# Eval("CompanyName")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PressReleaseDate", "{0:MMMM dd, yyyy}")%>
                                                        <asp:HiddenField ID="hdnPRID" runat="server" Value='<%# Eval("PressReleaseID")%>' />
                                                    </td>
                                                    <td><%# Eval("PlaceName") %></td>
                                                    <td><%# WFTCloud.GeneralReusableMethods.GetStatusString(Eval("RecordStatus").ToString()) %></td>
                                                    <td>
                                                        <div class="action-buttons">
                                                            <a data-rel="tooltip" title="Edit PressRelease" href='ManagePressRelease.aspx?editpressreleaseid=<%# Eval("PressReleaseID")%>' class="green">
                                                                <i class="icon-pencil bigger-130"></i>
                                                            </a>
                                                            <a data-rel="tooltip" title="Activate/Deactivate" href='ManagePressRelease.aspx?<%# WFTCloud.GeneralReusableMethods.GetActivateDeactivateAction(Eval("RecordStatus").ToString()) %>=<%# Eval("PressReleaseID")%>' class="blue">
                                                                <i class='<%# WFTCloud.GeneralReusableMethods.GetActivateDeactivateIcon(Eval("RecordStatus").ToString()) %>'></i>
                                                            </a>
                                                        </div>
                                                    </td>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                     </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <asp:Button ID="btnAddNewCMS" runat="server" Text="Add New CMS" OnClick="btnAddNewCMS_Click"
                                 class="btn btn-primary" />
                            <asp:Button ID="btnActivate" runat="server" Text="Activate" OnClick="btnActivate_Click"
                                class="btn btn-info"/>
                            <asp:Button ID="btnDeactivate" runat="server" Text="Deactivate" OnClick="btnDeactivate_Click"
                                class="btn btn-warning" />
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
                         <a href="ManagePressRelease.aspx">&lt;&lt; Back&nbsp;</a>
                        <br />
                        <div class="row-fluid">
                             <div id="divPRSuccess" runat="server" visible="false" class="alert alert-block alert-success span6">
                                        <button data-dismiss="alert" class="close" type="button">
                                            <i class="icon-remove"></i>
                                        </button>
                                        <p>
                                            <i class="icon-ok"></i>
                                            Press Release updated successfully.
                                        </p>
                                    </div>
                            <div id="divPRError" runat="server" visible="false" class="alert alert-error span6">
				                        <button data-dismiss="alert" class="close" type="button">
					                        <i class="icon-remove"></i>
				                        </button>
                                        <i class="icon-remove"></i>
                                        <asp:Label ID="lblPRError" runat="server"></asp:Label>
                                    </div>
                        </div>
                        <div class="table-header">
                            <asp:Label ID="lblTableHeader" runat="server" Text=""></asp:Label>
                        </div>
                        <table class="table table-hover dataTable table-bordered ">
                            <tr>
                                <td class="span2">Company Name</td>
                                <td>
                                    <asp:TextBox ID="txtCompanyName" runat="server" Font-Size="Medium" class="span10"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvPageName" ControlToValidate="txtCompanyName" ValidationGroup="PressRelease" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:HiddenField ID="hdnFlagPressID" runat="server" Value="0" />
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Press Release Header</td>
                                <td>
                                    <asp:TextBox ID="txtPRHeader" runat="server" Font-Size="Medium" class="span10"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvPageTitle" ControlToValidate="txtPRHeader" ValidationGroup="PressRelease" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Press Release Date</td>
                                <td>
                                    <asp:TextBox ID="txtPRDate" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender ID="ceStartDate" TargetControlID="txtPRDate" PopupButtonID="txtPRDate" runat="server" Format="MMM-dd-yyyy" />
                                    <asp:RequiredFieldValidator ID="rfvMetaKeyword" ControlToValidate="txtPRDate" ValidationGroup="PressRelease" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Company Description</td>
                                <td>
                                    <asp:TextBox ID="txtCompanyDescription" runat="server" Font-Size="Medium" Height="100px" TextMode="MultiLine" class="span10"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvMetaDescription" ControlToValidate="txtCompanyDescription" ValidationGroup="PressRelease" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Place Name</td>
                                <td>
                                    <asp:TextBox ID="txtPlaceName" runat="server" Font-Size="Medium" class="span5"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPlaceName" ValidationGroup="PressRelease" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <%--<tr>
                                <td class="span2">Press Release Content</td>
                                <td>
                                    <asp:TextBox ID="txtPRContent" runat="server" Font-Size="Medium" TextMode="MultiLine" class="span10" Height="300px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvEditContent" ControlToValidate="txtPRContent" ValidationGroup="PressRelease" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:HtmlEditorExtender ID="HtmlEditorEditContent" runat="server" TargetControlID="txtPRContent" DisplaySourceTab="true">
                                    </asp:HtmlEditorExtender>       
                                </td>
                            </tr>--%>
                             <tr>
                                <td class="span2">Press Release Content</td>
                                <td>
                                    <asp:TextBox ID="txtPRMainContent" runat="server" Font-Size="Medium" TextMode="MultiLine" class="span10" Height="300px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvEditContent" ControlToValidate="txtPRMainContent" ValidationGroup="PressRelease" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>      
                                </td>
                            </tr>
                              <tr>
                                <td class="span2">Quote Content</td>
                                <td>
                                    <asp:TextBox ID="txtQuote" runat="server" Font-Size="Medium" Height="100px" TextMode="MultiLine" class="span10"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtQuote" ValidationGroup="PressRelease" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Video url</td>
                                <td>
                                    <asp:TextBox ID="txtVideoURL" runat="server" Font-Size="Medium" class="span10"></asp:TextBox><br />
                                    Video url format should be in proper format (eg : https://www.youtube.com/embed/o11FU73WGvs)
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtVideoURL" ValidationGroup="PressRelease" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Actual PRweb Link</td>
                                <td>
                                    <asp:TextBox ID="txtActualPRWebLink" runat="server" Font-Size="Medium" class="span10"></asp:TextBox><br />
                                    PRweb link url format should be in proper format (eg : https://www.google.com)
                                </td>
                            </tr>
                            <%--<asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="conditional">
                                <Triggers >
                                    <asp:AsyncPostBackTrigger ControlID = "btnPreview" />
                                    <%--<asp:AsyncPostBackTrigger ControlID = "btnSaveCMS" />--%>
                                    <%--<asp:PostBackTrigger ControlID="btnPreview" />--%>
                                <%--</Triggers>
                                <ContentTemplate>
                                    <Table class="table table-hover dataTable table-bordered ">--%>
                                    <tr>
                                        <td class="span2">Image</td>
                                        <td>
                                            <asp:FileUpload ID="fluImage" runat="server" Width="237px" />
                                            <br /><asp:HyperLink runat="server" ID="hypThumbnailImageLink" Target="_blank" Text="Click Here to View" Visible="false"></asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="span2"></td>
                                        <td>
                                            <asp:Button ID="btnSaveCMS" runat="server" Text="Save" class="btn btn-primary" ValidationGroup="PressRelease" OnClick="btnSaveCMS_Click" />
                                            <asp:Button ID="btnPreview" runat="server" Text="Preview" class="btn btn-warning" OnClick="btnPreview_Click" ValidationGroup="PressRelease" />
                                        </td>
                                    </tr>
                               <%--     </Table>
                                </ContentTemplate>
                            </asp:UpdatePanel>--%>
                        </table>
                    </div>
                </div>
            </div>
        </asp:View>
    </asp:MultiView>
    <script type="text/javascript">
        jQuery(function ($) {
            $('#tblPressReleaseManagement').dataTable(
                {
                    "aoColumnDefs": [
                        { 'bSortable': false, 'aTargets': [0, 5] }
                    ]
                }
         );
        });
   </script>
</asp:Content>

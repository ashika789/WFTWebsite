<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminPages.Master" AutoEventWireup="true" CodeBehind="ManageFAQ.aspx.cs" Inherits="WFTCloud.Admin.ManageFAQ" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <title>Manage FAQ</title>
    <meta name="Description" content="FAQ's are managed here. " />
    <meta name="Keywords" content="WFT Manage FAQ" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="mvContainer" runat="server" ActiveViewIndex="0">
        <asp:View ID="MFAQView" runat="server">
            <div class="row-fluid">
                <div class="span12">
                    <div class="row-fluid">
                        <div id="divMFAQSuccessMessage" runat="server" visible="false" class="alert alert-block alert-success span6">
                            <button data-dismiss="alert" class="close" type="button">
                                <i class="icon-remove"></i>
                            </button>
                            <p>
                                <i class="icon-ok"></i>
                                FAQ changes saved successfully.
                            </p>
                        </div>
                        </div>
                        <div class="row-fluid">
                        <div class="table-header">
                            Manage FAQ
                        </div>
                        <div class="dataTables_wrapper">
                        <div role="grid" class="dataTables_wrapper">
                            <table id="tblManageFAQ" class="table table-striped table-bordered table-hover dataTable">
                                <thead>
                                    <tr role="row">
                                        <th class="center" role="columnheader" style="width:5%">
                                            <label><input type="checkbox" class="ace"/>
                                                <span class="lbl"></span>
                                            </label>
                                        </th>
                                        <th role="columnheader" class="center" style="width:25%">Question</th>
                                        <th role="columnheader" class="center" style="width:26%">Answer</th>
                                        <th role="columnheader" class="center" style="width:10%">User Type</th>
                                        <th role="columnheader" class="center" style="width:10%">Category</th>
                                        <th role="columnheader" class="center" style="width:8%">Priority</th>
                                        <th role="columnheader" class="center" style="width:8%">Status</th>
                                        <th role="columnheader" class="center" style="text-align:center; width:8%">Options</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rptrManageFAQ" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="center">
                                                    <asp:CheckBox ID="chkSelect" runat="server" class="ace" />
                                                </td>
                                                <td><%# Eval("Question")%><asp:HiddenField ID="hdnFAQID" runat="server" Value='<%# Eval("FAQID")%>' /></td>
                                                <td> <%# Eval("Answer")%></td>
                                                <td><%# Eval("FAQTypeText")%></td>
                                                <td><%# Eval("FAQCategoryName")%></td>
                                                <td><%# Eval("Priority")%></td>
                                                <td><%# WFTCloud.GeneralReusableMethods.GetStatusString(Eval("RecordStatus").ToString()) %></td>
                                                <td>
                                                <div class="action-buttons">
                                                    <a data-rel="tooltip" title="Edit FAQ" href='ManageFAQ.aspx?editfaqid=<%# Eval("FAQID")%>' class="green">
                                                        <i class="icon-pencil bigger-130"></i>
                                                    </a>
                                                    <a data-rel="tooltip" title="Activate/Deactivate" href='ManageFAQ.aspx?<%# WFTCloud.GeneralReusableMethods.GetActivateDeactivateAction(Eval("RecordStatus").ToString()) %>=<%# Eval("FAQID")%>' class="blue">
                                                        <i class='<%# WFTCloud.GeneralReusableMethods.GetActivateDeactivateIcon(Eval("RecordStatus").ToString()) %>'></i>
                                                    </a>
                                                   <%-- <a data-rel="tooltip" title="Delete FAQ" href='ManageFAQ.aspx?delete=<%# Eval("FAQID")%>' class="red">
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
                            <asp:Button ID="btnAddNew" runat="server" Text="New FAQ" 
                                class="btn btn-info" onclick="btnAddNew_Click"/>
                            <asp:Button ID="btnActivate" runat="server" Text="Activate" 
                                class="btn btn-info" OnClick="btnActivate_Click" />
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
        <asp:View ID="MFAQEdit" runat="server">
            <div class="row-fluid">
                <div class="span12">
                    <div class="row-fluid">
                        <a href="ManageFAQ.aspx">&lt;&lt; Back&nbsp;</a>
                        <br />
                        <div class="row-fluid">
                             <div id="divEditFAQ" runat="server" visible="false" class="alert alert-block alert-success span6">
                                        <button data-dismiss="alert" class="close" type="button">
                                            <i class="icon-remove"></i>
                                        </button>
                                        <p>
                                            <i class="icon-ok"></i>
                                            FAQ updated successfully.
                                        </p>
                                    </div>
                                    <div id="divErrorMessage" runat="server" visible="false" class="alert alert-block alert-error span6">
                                        <button data-dismiss="alert" class="close" type="button">
                                            <i class="icon-remove"></i>
                                        </button>
                                        <p>
                                            <i class="icon-ok"></i>
                                            Error while saving details.
                                        </p>
                                    </div>
                        </div>
                        <div class="table-header">
                            <asp:Label ID="lblHeading" runat="server" Text="Edit Frequently Asked Question"></asp:Label>
                        </div>
                        <table class="table table-hover dataTable table-bordered ">
                            <asp:Panel ID="pnlLanguage" runat="server" Visible="false">
                            <tr>
                                <td class="span2">Language</td>
                                <td>
                                      <asp:DropDownList ID="ddlLanguage" runat="server" AppendDataBoundItems="True">
                                            <asp:ListItem><--Select Language--></asp:ListItem>
                                            <asp:ListItem Value="English">English</asp:ListItem>
                                            <asp:ListItem Value="Spanish">Spanish</asp:ListItem>
                                        </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlLanguage" ValidationGroup="EditFAQ" InitialValue="<--Select Language-->" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            </asp:Panel>
                            <tr>
                                <td class="span2">Question</td>
                                <td>
                                    <asp:TextBox ID="txtQuestion" runat="server" TextMode="MultiLine" class="span10">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvQuestion" runat="server" ControlToValidate="txtQuestion" ErrorMessage="*" ForeColor="Red" ValidationGroup="EditFAQ"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Answer</td>
                                <td>
                                    <asp:TextBox ID="txtAnswer" runat="server" TextMode="MultiLine" class="span10">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvAnswer" runat="server" ControlToValidate="txtAnswer" ErrorMessage="*" ForeColor="Red" ValidationGroup="EditFAQ"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">User Type</td>
                                <td>
                                    <asp:DropDownList ID="ddlUserType" runat="server" Height="30px" Width="198px">
                                        <asp:ListItem Selected="True" Text="Select" Value="Select">Select</asp:ListItem>
                                        <asp:ListItem>Member</asp:ListItem>
                                        <asp:ListItem>Non-Member</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Category</td>
                                <td>
                                    <asp:DropDownList ID="ddlCategory" runat="server" Height="30px" Width="198px">
                                        <asp:ListItem Selected="True" Text="Select" Value="Select">Select</asp:ListItem>
                                        <asp:ListItem>General</asp:ListItem>
                                        <asp:ListItem>Technical</asp:ListItem>
                                        <asp:ListItem>Sales</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Priority</td>
                                <td>
                                    <asp:DropDownList ID="ddlPriority" runat="server" Height="30px" Width="198px">
                                        <asp:ListItem Selected="True" Value="1">1</asp:ListItem>
                                        <asp:ListItem Value="2">2</asp:ListItem>
                                        <asp:ListItem Value="3">3</asp:ListItem>
                                        <asp:ListItem Value="4">4</asp:ListItem>
                                        <asp:ListItem Value="5">5</asp:ListItem>
                                        <asp:ListItem Value="6">6</asp:ListItem>
                                        <asp:ListItem Value="7">7</asp:ListItem>
                                        <asp:ListItem Value="8">8</asp:ListItem>
                                        <asp:ListItem Value="9">9</asp:ListItem>
                                        <asp:ListItem Value="10">10</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                             <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnAddFAQ" runat="server" Text="Save" class="btn btn-primary" ValidationGroup="EditFAQ" OnClick="btnAddFAQ_Click"/>
                                   
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
             $('#tblManageFAQ').dataTable(
                 {
                     "aoColumnDefs": [
                         { 'bSortable': false, 'aTargets': [0, 7] }
                     ]
                 }
          );
         });
   </script>
</asp:Content>

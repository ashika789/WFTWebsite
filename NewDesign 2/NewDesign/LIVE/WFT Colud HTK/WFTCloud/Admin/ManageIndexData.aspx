<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminPages.Master" AutoEventWireup="true" CodeBehind="ManageIndexData.aspx.cs" Inherits="WFTCloud.Admin.ManageIndexData" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>ManageIndexData</title>
    <meta name="Description" content="Index Datas Are Managed Here." />
    <meta name="Keywords" content="WFT Add New Index Data" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="mvContainer" runat="server" ActiveViewIndex="0">
        <asp:View ID="ANIDView" runat="server">
            <div class="row-fluid">
                <div class="span12">
                    <div class="row-fluid">
                        <div id="divANIDSuccessMessage" runat="server" visible="false" class="alert alert-block alert-success span6">
                            <button data-dismiss="alert" class="close" type="button">
                                <i class="icon-remove"></i>
                            </button>
                            <p>
                                <i class="icon-ok"></i>
                                Index data updated successfully.
                            </p>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="table-header">
                            Manage Index Data 
                        </div>
                        <div class="dataTables_wrapper">
                            <div class="row-fluid">
                            <strong>Filter</strong>
                            <asp:DropDownList ID="ddlFilter" runat="server" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
                                <asp:ListItem Value="1">Certificate</asp:ListItem>
                                <asp:ListItem Value="2">Video</asp:ListItem>
                                <asp:ListItem Value="3">Partner</asp:ListItem>
                                <asp:ListItem Value="4">Client</asp:ListItem>
                                <asp:ListItem Value="5">Brochure</asp:ListItem>
                                <asp:ListItem Value="6">WhitePaper</asp:ListItem>
                            </asp:DropDownList>
                            </div>
                            <div role="grid" class="dataTables_wrapper">
                            <table id="tblIndexData" class="table table-striped table-bordered table-hover dataTable">
                                <thead>
                                    <tr role="row">
                                        <th class="center" role="columnheader">
                                            <label><input type="checkbox" class="ace"/>
                                                <span class="lbl"></span>
                                            </label>
                                        </th>
                                        <th role="columnheader" class="center">Category</th>
                                        <th role="columnheader" class="center">Title</th>
                                        <th role="columnheader" class="center">File</th>
                                        <th role="columnheader" class="center">Priority</th>
                                        <th role="columnheader" class="center">Record Status</th>
                                        <th role="columnheader" class="center" style="text-align:center;">Options</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rptrAddNewIndexData" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="center">
                                                    <asp:CheckBox ID="chkSelect" runat="server" class="ace" />
                                                </td>
                                                <td><%# Eval("IndexDataTypeText")%><asp:HiddenField ID="hdnIndexDataID" runat="server" Value='<%# Eval("IndexDataID")%>' /></td>
                                                <td><%# Eval("Title")%></td>
                                                <td style="text-align:center"><a href='<%#Eval("Path")%>' target="_blank"><img src='<%#Eval("ThumbnailPath")%>' alt="Banner Image" style="width:200px; height:80px;" /></a></td>
                                               <%-- <td class="center">
                                                    <a data-rel="tooltip" title='<%# WFTCloud.GeneralReusableMethods.GetIndexDataToolTip(Eval("IndexDataTypeID").ToString()) %>' href='<%# Eval("Path")%>' target="_blank" class="<%# WFTCloud.GeneralReusableMethods.GetIndexDataIconColor(Eval("IndexDataTypeID").ToString()) %>">
                                                            <i class='<%# WFTCloud.GeneralReusableMethods.GetIndexDataClass(Eval("IndexDataTypeID").ToString()) %>'></i>
                                                        </a>
                                                </td>--%>
                                                <td><%# Eval("Priority")%></td>
                                                <td><%# WFTCloud.GeneralReusableMethods.GetStatusString(Eval("RecordStatus").ToString()) %></td>
                                                <td>
                                                    <div class="action-buttons">
                                                        <a data-rel="tooltip" title="Edit Index Data" href='ManageIndexData.aspx?editindexdataid=<%# Eval("IndexDataID")%>' class="green">
                                                            <i class="icon-pencil bigger-130"></i>
                                                        </a>
                                                        <a data-rel="tooltip" title="Activate/Deactivate" href='ManageIndexData.aspx?<%# WFTCloud.GeneralReusableMethods.GetActivateDeactivateAction(Eval("RecordStatus").ToString()) %>=<%# Eval("IndexDataID")%>' class="blue">
                                                            <i class='<%# WFTCloud.GeneralReusableMethods.GetActivateDeactivateIcon(Eval("RecordStatus").ToString()) %>'></i>
                                                        </a>
                                                        <%--<a data-rel="tooltip" title="Delete Index Data" href='ManageIndexData.aspx?delete=<%# Eval("IndexDataID")%>' class="red">
                                                            <i class="icon-trash bigger-130"></i>
                                                        </a>--%>
                                                    </div>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                            </table>
                        </div>
                        </div>
                        <div class="row-fluid">
                            <asp:Button ID="btnAddNewIndexData" runat="server" Text="Add New Index Data" 
                                class="btn btn-primary" OnClick="btnAddNewIndexData_Click" />
                            <asp:Button ID="btnActivate" runat="server" Text="Activate" 
                                class="btn btn-info" OnClick="btnActivate_Click" />
                            <asp:Button ID="btnDeactivate" runat="server" Text="Deactivate" 
                                class="btn btn-warning" OnClick="btnDeactivate_Click" />
                           <%-- <asp:Button ID="btnDelete" runat="server" Text="Delete" 
                                class="btn btn-danger" OnClick="btnDelete_Click" />--%>
                            <asp:CheckBox ID="chkShowDeleted" runat="server" AutoPostBack="True" OnCheckedChanged="chkShowDeleted_CheckedChanged" />
                            <span class="label label-warning arrowed-right">Show De-Activated</span>
                        </div>
                     </div>
                </div>
            </div>
        </asp:View>
        <asp:View ID="ANIDEdit" runat="server">
            <div class="row-fluid">
                <div class="span12">
                    <div class="row-fluid">
                        <a href="ManageIndexData.aspx">&lt;&lt; Back&nbsp;</a>
                        <br />
                        <div class="row-fluid">
                              <div id="divIndexDataSuccess" runat="server" visible="false" class="alert alert-block alert-success span6">
                                        <button data-dismiss="alert" class="close" type="button">
                                            <i class="icon-remove"></i>
                                        </button>
                                        <p>
                                            <i class="icon-ok"></i>
                                            Index data Updated successfully.
                                        </p>
                                    </div>
                                    <div id="divIndexDataError" runat="server" visible="false" class="alert alert-error span6">
				                        <button data-dismiss="alert" class="close" type="button">
					                        <i class="icon-remove"></i>
				                        </button>
                                        <i class="icon-remove"></i>
                                        <asp:Label ID="lblIndexDataError" runat="server"></asp:Label>
                                    </div>
                        </div>
                        <div class="table-header">
                            Edit Index Datas
                        </div>
                        <table class="table table-hover dataTable table-bordered ">
                            <tr>
                                <td class="span2">Category</td>
                                <td>
                                    <strong>
                                        <asp:Label ID="lblEditCategory" runat="server" Text="Label"></asp:Label>
                                    </strong>
                                </td>
                            </tr>
                            <asp:Panel ID="pnlIndexDataImage" runat="server">
                            <tr>
                                <td class="span2">Upload File</td>
                                <td>
                                    <asp:FileUpload ID="flUploadImage" runat="server" Width="237px" />
                                    <br /><asp:HyperLink runat="server" ID="hypImageLink" Target="_blank" Text="Click Here to View"></asp:HyperLink>
                                   <%-- <asp:RequiredFieldValidator ID="rfvUploadImage" runat="server" ControlToValidate="flUploadImage" ErrorMessage="Only Image Files Are Allowed Here" ForeColor="Red" ValidationGroup="IndexData"></asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            </asp:Panel>
                            <asp:Panel ID="pnlIndexDataVideo" runat="server">
                            <tr>
                                <td class="span2">Video Link</td>
                                <td>
                                    <asp:TextBox ID="txtVideoLink" runat="server">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvVideoLink" runat="server" ErrorMessage="*" ControlToValidate="txtVideoLink" ForeColor="Red" ValidationGroup="IndexData"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            </asp:Panel>
                            <tr>
                                <td class="span2">Thumbnail</td>
                                <td>
                                    <asp:FileUpload ID="fluEditThumbnail" runat="server" Width="237px" />
                                    <br /><asp:HyperLink runat="server" ID="hypThumbnailImageLink" Target="_blank" Text="Click Here to View"></asp:HyperLink>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Title</td>
                                <td>
                                    <asp:TextBox ID="txtTitle" runat="server">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle" ErrorMessage="*" ForeColor="Red" ValidationGroup="IndexData"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Description</td>
                                <td>
                                    <asp:TextBox ID="txtDescription" runat="server">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ControlToValidate="txtDescription"
                                         ErrorMessage="*" ForeColor="Red" ValidationGroup="IndexData"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                             <tr>
                                <td class="span2">Priority</td>
                                <td>
                                    <asp:TextBox ID="txtEditPriority" runat="server" MaxLength="2">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEditPriority"
                                         ErrorMessage="*" ForeColor="Red" ValidationGroup="IndexData"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                           <%-- <tr>
                                <td class="span2">Priority</td>
                                <td>
                                    <asp:DropDownList ID="ddlPriority" runat="server" Height="30px" Width="198px">
                                        <asp:ListItem Value="1">1</asp:ListItem>
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
                            </tr>--%>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn-primary" ValidationGroup="IndexData" OnClick="btnSubmit_Click" />
                                    <asp:Button ID="btmEditReset" runat="server" Text="Reset" class="btn btn-warning" OnClick="btmEditReset_Click" />
                                  
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </asp:View>
        <asp:View ID="vwNewIndex" runat="server">
            <div class="row-fluid">
                <div class="span12">
                    <div class="row-fluid">
                        <a href="ManageIndexData.aspx">&lt;&lt; Back&nbsp;</a>
                        <br />
                        <div class="row-fluid">
                             <div id="divAddNewIndexDataSuccess" runat="server" visible="false" class="alert alert-block alert-success span6">
                                        <button data-dismiss="alert" class="close" type="button">
                                            <i class="icon-remove"></i>
                                        </button>
                                        <p>
                                            <i class="icon-ok"></i>
                                            Index data added successfully.
                                        </p>
                                    </div>
                                    <div id="divAddNewIndexDataErrorMessage" runat="server" visible="false" class="alert alert-error span6">
				                        <button data-dismiss="alert" class="close" type="button">
					                        <i class="icon-remove"></i>
				                        </button>
                                        <i class="icon-remove"></i>
                                        <asp:Label ID="lblindexErrorMessage" runat="server"></asp:Label>
                                    </div>
                        </div>
                        <div class="table-header">
                            Add New Index Data
                        </div>
                        <table class="table table-hover dataTable table-bordered ">
                            <tr>
                                <td class="span2">Select Category</td>
                                <td>
                                    <asp:DropDownList ID="ddlAddNewIndexCategory" runat="server" Height="30px" Width="198px"
                                         AutoPostBack="true" OnSelectedIndexChanged="ddlAddNewIndexCategory_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="0">Select Category</asp:ListItem>
                                        <asp:ListItem Value="1">Certificate</asp:ListItem>
                                        <asp:ListItem Value="2">Video</asp:ListItem>
                                        <asp:ListItem Value="3">Partner</asp:ListItem>
                                        <asp:ListItem Value="4">Client</asp:ListItem>
                                        <asp:ListItem Value="5">Brochure</asp:ListItem>
                                        <asp:ListItem Value="6">White Paper</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvDropdownCategory" runat="server" ErrorMessage="*" Text="Please Select Any Category" InitialValue="0"
                                         ControlToValidate="ddlAddNewIndexCategory" ValidationGroup="AddNewIndexData" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <asp:Panel ID="pnlImage" runat="server" Visible="false">
                            <tr>
                                <td class="span2">Upload File</td>
                                <td>
                                    <asp:FileUpload ID="FileUpload1" runat="server" Width="237px" />
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="flUploadImage" ErrorMessage="Only Image Files Are Allowed Here" ForeColor="Red" ValidationGroup="AddNewIndexData"></asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            </asp:Panel>
                            <asp:Panel ID="pnlVideo" runat="server" Visible="false">
                            <tr>
                                <td class="span2">Video Link</td>
                                <td>
                                    <asp:TextBox ID="txtAddNewIndexVideo" runat="server">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvAddNewIndexVideo" runat="server" ControlToValidate="txtAddNewIndexVideo"
                                         ErrorMessage="*" ForeColor="Red" ValidationGroup="AddNewIndexData"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            </asp:Panel>
                            <tr>
                                <td class="span2">Thumbnail</td>
                                <td>
                                    <asp:FileUpload ID="fluNewThumbnail" runat="server" Width="237px" />
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Title</td>
                                <td>
                                    <asp:TextBox ID="txtAddNewIndexTitle" runat="server">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvAddNewIndexTitle" runat="server" ControlToValidate="txtAddNewIndexTitle"
                                         ErrorMessage="*" ForeColor="Red" ValidationGroup="AddNewIndexData"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Description</td>
                                <td>
                                    <asp:TextBox ID="txtAddNewIndexDescription" runat="server">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvAddNewIndexDescription" runat="server" ControlToValidate="txtAddNewIndexDescription"
                                         ErrorMessage="*" ForeColor="Red" ValidationGroup="AddNewIndexData"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                              <tr>
                                <td class="span2">Priority</td>
                                <td>
                                    <asp:TextBox ID="txtNewPriority" runat="server" MaxLength="2" Text="5">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNewPriority"
                                         ErrorMessage="*" ForeColor="Red" ValidationGroup="AddNewIndexData"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                           <%-- <tr>
                                <td class="span2">Priority</td>
                                <td>
                                    <asp:DropDownList ID="ddlAddNewIndexPriority" runat="server" Height="30px" Width="198px">
                                        <asp:ListItem Value="1" Selected="True">1</asp:ListItem>
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
                            </tr>--%>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnAddNewIndexSave" runat="server" Text="Submit" class="btn btn-primary" ValidationGroup="AddNewIndexData" OnClick="btnAddNewIndexSave_Click" />
                                    <asp:Button ID="btnNewReset" runat="server" Text="Reset" class="btn btn-warning" OnClick="btnNewReset_Click1" />
                                   
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
            $('#tblIndexData').dataTable(
                {
                    "aoColumnDefs": [
                        { 'bSortable': false, 'aTargets': [0, 6] }
                    ]
                }
         );
        });
   </script>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminPages.Master" AutoEventWireup="true" CodeBehind="ManageBanners.aspx.cs" Inherits="WFTCloud.Admin.ManageBanners" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <title>Update Banner Priority</title>
    <meta name="Description" content="Banner Priority Can be Updated here." />
    <meta name="Keywords" content="WFT Update Banner Priority" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="mvContainer" runat="server" ActiveViewIndex="0">
        <asp:View ID="UBPView" runat="server">
            <div class="row-fluid">
                <div class="span12">
                    <div class="row-fluid">
                        <div id="divUBPSuccessMessage" runat="server" visible="false" class="alert alert-block alert-success span6">
                            <button data-dismiss="alert" class="close" type="button">
                                <i class="icon-remove"></i>
                            </button>
                            <p>
                                <i class="icon-ok"></i>
                                Banners updated successfully.
                            </p>
                        </div>
                        </div>
                        <div class="row-fluid">
                            <div class="table-header">
                                Manage Banners
                            </div>
                        <div class="dataTables_wrapper">
                        <div role="grid" class="dataTables_wrapper">
                            <table id="tblBanners" class="table table-striped table-bordered table-hover dataTable">
                                <thead>
                                    <tr role="row">
                                        <th class="center" role="columnheader">
                                            <label><input type="checkbox" class="ace"/>
                                                <span class="lbl"></span>
                                            </label>
                                        </th>
                                        <th role="columnheader" class="center">Banner Title</th>
                                        <th role="columnheader" class="center">Banner Image</th>
                                        <%--<th role="columnheader" class="center">Priority</th>--%>
                                        <th role="columnheader" class="center">Banner Staus</th>
                                        <th role="columnheader" class="center" style="text-align:center;">Options</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rptrUpdateBannerPriority" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="center">
                                                    <asp:CheckBox ID="chkSelect" runat="server" class="ace" />
                                                </td>
                                                <td><a href='<%# Eval("RedirectUrl")==null ?"#":Eval("RedirectUrl") %>' target="_blank"><%# Eval("ImgTitle")%></a><asp:HiddenField ID="hdnBannerID" runat="server" Value='<%# Eval("BannerID")%>' /></td>
                                                <td style="text-align:center"><a href='<%#Eval("ImgPath")%>' target="_blank"><img src='<%#Eval("ImgPath")%>' alt="Banner Image" style="width:200px; height:80px;" /></a></td>
                                                <%--<td><%# Eval("ImgPriority")%></td>--%>
                                                <td><%# WFTCloud.GeneralReusableMethods.GetStatusString(Eval("RecordStatus").ToString()) %></td>
                                                <td>
                                                    <div class="action-buttons">
                                                        <a data-rel="tooltip" title="Edit Banner" href='ManageBanners.aspx?editbannerid=<%# Eval("BannerID")%>' class="green">
                                                            <i class="icon-pencil bigger-130"></i>
                                                        </a>
                                                        <a data-rel="tooltip" title="Activate/Deactivate" href='ManageBanners.aspx?<%# WFTCloud.GeneralReusableMethods.GetActivateDeactivateAction(Eval("RecordStatus").ToString()) %>=<%# Eval("BannerID")%>' class="blue">
                                                            <i class='<%# WFTCloud.GeneralReusableMethods.GetActivateDeactivateIcon(Eval("RecordStatus").ToString()) %>'></i>
                                                        </a>
                                                       <%-- <a data-rel="tooltip" title="Delete Banner" href='ManageBanners.aspx?delete=<%# Eval("BannerID")%>' class="red">
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
                            <asp:Button ID="btnAddNewIndexData" runat="server" Text="Add New Banner" 
                                class="btn btn-primary" OnClick="btnAddNewIndexData_Click" />
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
        <asp:View ID="EditBanner" runat="server">
            <div class="row-fluid">
                <div class="span12">
                    <div class="row-fluid">
                        <a href="ManageBanners.aspx">&lt;&lt; Back&nbsp;</a>
                        <br />
                        <div class="row-fluid">
                            <div id="divBannerSuccess" runat="server" visible="false" class="alert alert-block alert-success span6">
                                <button data-dismiss="alert" class="close" type="button">
                                    <i class="icon-remove"></i>
                                </button>
                                <p>
                                    <i class="icon-ok"></i>
                                    Banner updated successfully.
                                </p>
                            </div>
                            <div id="divBannerError" runat="server" visible="false" class="alert alert-error span6">
				                <button data-dismiss="alert" class="close" type="button">
					                <i class="icon-remove"></i>
				                </button>
                                <i class="icon-remove"></i>
                                <asp:Label ID="lblBannerError" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="table-header">
                            Edit Banner
                        </div>
                        <table class="table table-hover dataTable table-bordered ">
                            <tr>
                                <td class="span2">Upload Banner</td>
                                <td>
                                    <asp:FileUpload ID="flUploadBanner" runat="server"/>
                                    <br /><asp:HyperLink runat="server" ID="hypbannerImageLink" Target="_blank" Text="Click Here to View"></asp:HyperLink>
                                    <%--<asp:RequiredFieldValidator ID="rfvUploadBanner" runat="server" ControlToValidate="flUploadBanner" ErrorMessage="Only Banners Are Allowed Here" ForeColor="Red" ValidationGroup="BannerPriority"></asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Banner Title</td>
                                <td>
                                    <asp:TextBox ID="txtBannerTitle" runat="server">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvBannerTitle" runat="server" ControlToValidate="txtBannerTitle" ErrorMessage="*" ForeColor="Red" ValidationGroup="BannerPriority"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Banner Redirect Link</td>
                                <td>
                                    <asp:TextBox ID="txtBannerRedirectLink" runat="server">
                                    </asp:TextBox>
                                    <asp:RegularExpressionValidator ID="revBannerRedirectLink" runat="server" ControlToValidate="txtBannerRedirectLink"  ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?"
                                         ErrorMessage="Enter valid Url" ForeColor="Red" ValidationGroup="BannerPriority"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                             <tr>
                                <td class="span2">Banner Priority</td>
                                <td>
                                    <asp:DropDownList ID="ddlBannerPriority" runat="server">
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
                            </tr>
                               <tr>
                                <td class="span2">Mandatory</td>
                                <td>
                                    <asp:CheckBox ID="chkEditMandatory" runat="server"></asp:CheckBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Banner Status</td>
                                <td>
                                    <asp:DropDownList ID="ddlBannerStatus" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                             <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn-primary" ValidationGroup="BannerPriority" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btmEditReset" runat="server" Text="Reset" class="btn btn-warning" OnClick="btmEditReset_Click" />
                            
                            </td>
                        </tr>
                        </table>
                    </div>
                </div>
            </div>
        </asp:View>
        <asp:View ID="NewBanner" runat="server">
            <div class="row-fluid">
                <div class="span12">
                    <div class="row-fluid">
                        <a href="ManageBanners.aspx">&lt;&lt; Back&nbsp;</a>
                        <br />
                        <div class="row-fluid">
                            <div id="divNewBannerSuccess" runat="server" visible="false" class="alert alert-block alert-success span6">
                                <button data-dismiss="alert" class="close" type="button">
                                    <i class="icon-remove"></i>
                                </button>
                                <p>
                                    <i class="icon-ok"></i>
                                    New banner added successfully.
                                </p>
                            </div>
                            <div id="divNewBannerError" runat="server" visible="false" class="alert alert-error span6">
				                <button data-dismiss="alert" class="close" type="button">
					                <i class="icon-remove"></i>
				                </button>
                                <i class="icon-remove"></i>
                                <asp:Label ID="lblNewBannerError" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="table-header">
                            Add New Banner
                        </div>
                        <table class="table table-hover dataTable table-bordered ">
                            <tr>
                                <td class="span2">Upload Banner</td>
                                <td>
                                    <asp:FileUpload ID="flupNewBanner" runat="server"/>
                                    <%--<asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="flUploadBanner" ErrorMessage="Only Banners Are Allowed Here" ForeColor="Red" ValidationGroup="BannerPriority"></asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Banner Title</td>
                                <td>
                                    <asp:TextBox ID="txtNewBannerTitle" runat="server">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvNewBannerTitle" runat="server" ControlToValidate="txtNewBannerTitle" ErrorMessage="*" ForeColor="Red" ValidationGroup="NewBanner"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Banner Redirect Link</td>
                                <td>
                                    <asp:TextBox ID="txtNewRedirectLink" runat="server">
                                    </asp:TextBox>
                                    <asp:RegularExpressionValidator ID="revNewRedirectLink" runat="server" ControlToValidate="txtNewRedirectLink"  ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?"
                                         ErrorMessage="Enter Valid Url" ForeColor="Red" ValidationGroup="NewBanner"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                             <tr>
                                <td class="span2">Banner Priority</td>
                                <td>
                                    <asp:DropDownList ID="ddlNewBannerPriority" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Mandatory</td>
                                <td>
                                    <asp:CheckBox ID="chkMandatory" runat="server"></asp:CheckBox>
                                 <%--   <asp:Label ID="lblSwitch" runat="server">
                                        <asp:TextBox ID="txtSwitch" runat="server" class="ace ace-switch ace-switch-6" type="checkbox" ></asp:TextBox>
                                        <span class="lbl"></span>
                                    </asp:Label>--%>
                                <%--    <label>
										<input name="switch-field-1" class="ace ace-switch ace-switch-6" type="checkbox" />
										<span class="lbl"></span>
									</label>--%>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Banner Status</td>
                                <td>
                                    <asp:DropDownList ID="ddlNewBannerStatus" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                             <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="btnNewBannerSave" runat="server" Text="Submit" class="btn btn-primary" ValidationGroup="NewBanner" OnClick="btnNewBannerSave_Click" />
                            <asp:Button ID="btnNewReset" runat="server" Text="Reset" class="btn btn-warning" OnClick="btnNewReset_Click" />
                            
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
            $('#tblBanners').dataTable(
                 {
                     "aoColumnDefs": [
                         { 'bSortable': false, 'aTargets': [0, 4] }
                     ]
                 }
         );
        });
   </script>
</asp:Content>

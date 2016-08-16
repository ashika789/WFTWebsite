<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminPages.Master" AutoEventWireup="true" CodeBehind="CategoriesList.aspx.cs" Inherits="WFTCloud.Admin.CategoriesList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <title>Service Categories List</title>
	    <meta name="description" content="List of service categories can be seen here." />
        <meta name="keywords" content="WFT Service cateory" />
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
					Categories changes saved successfully.
				</p>
            </div>
            <div id="divCatErrorMessage" runat="server" visible="false" class="alert alert-error span6">
				<button data-dismiss="alert" class="close" type="button">
					<i class="icon-remove"></i>
				</button>
                <i class="icon-remove"></i>
                <asp:Label ID="lblCatErrorMessage" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row-fluid">
	        <div class="table-header">
		        Categories	        
            </div>
            <div class="dataTables_wrapper">
                <div role="grid" class="dataTables_wrapper">
                    <table id="tblCategories" class="table table-striped table-bordered table-hover dataTable">
                        <thead>
                        <tr role="row">
                            <th class="center" role="columnheader">
                                <label><input type="checkbox" class="ace"/>
                                    <span class="lbl"></span>
                                </label>
                            </th>
                            <th role="columnheader">Category Name</th>
                            <th role="columnheader">Priority</th>
                            <th role="columnheader">Category Status</th>
                            <th role="columnheader" style="text-align:center;">Options</th>
                        </tr>
                        </thead>
                        <tbody role="alert">
                        <asp:Repeater ID="rptrCategory" runat="server">
                        <ItemTemplate>
                        <tr>
                            <td class="center">
                                <asp:CheckBox ID="chkSelect" runat="server" class="ace" />
                            </td>
                            <td>
                                <a href='CategoriesList.aspx?editcategoryid=<%# Eval("ServiceCategoryID")%>'>
                                <%# Eval("CategoryName")%>
                                </a>
                                <asp:HiddenField ID="hdnCategoryID" runat="server" Value='<%# Eval("ServiceCategoryID")%>' />
                            </td>
                            <td><%# Eval("CategoryPriority")%></td>
                            <td><%# WFTCloud.GeneralReusableMethods.GetStatusString(Eval("RecordStatus").ToString()) %></td>
                            <td>
                                <div class="action-buttons">
                                <a data-rel="tooltip" title="Edit Category" href='CategoriesList.aspx?editcategoryid=<%# Eval("ServiceCategoryID")%>' class="green">
                                    <i class="icon-pencil bigger-130"></i>
                                </a>
                                <a data-rel="tooltip" title="Activate/Deactivate" href='CategoriesList.aspx?<%# WFTCloud.GeneralReusableMethods.GetActivateDeactivateAction(Eval("RecordStatus").ToString()) %>=<%# Eval("ServiceCategoryID")%>' class="blue">
                                    <i class='<%# WFTCloud.GeneralReusableMethods.GetActivateDeactivateIcon(Eval("RecordStatus").ToString()) %>'></i>
                                </a>
                               <%-- <a data-rel="tooltip" title="Delete Category" href='CategoriesList.aspx?delete=<%# Eval("ServiceCategoryID")%>' class="red">
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
                    class="btn btn-primary" onclick="btnAddNew_Click" />
                <asp:Button ID="btnActivate" runat="server" Text="Activate" 
                    class="btn btn-info" onclick="btnActivate_Click" />
                <asp:Button ID="btnDeactivate" runat="server" Text="Deactivate" 
                    class="btn btn-warning" onclick="btnDeactivate_Click" />
                <%--<asp:Button ID="btnDelete" runat="server" Text="Delete" 
                    class="btn btn-danger" onclick="btnDelete_Click" />--%>
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
	        <a href="CategoriesList.aspx">&lt;&lt; Back&nbsp;</a>
            <div class="row-fluid">
                 <div id="lblSuccessMessage" runat="server" visible="false" class="alert alert-block alert-success span6">
							<button data-dismiss="alert" class="close" type="button">
								<i class="icon-remove"></i>
							</button>
							<p>
                                <i class="icon-ok"></i>
								Category changes saved successfully.
							</p>
						</div>
                        <div id="lblErrorMessage" runat="server" visible="false" class="alert alert-error span6">
						    <button data-dismiss="alert" class="close" type="button">
							    <i class="icon-remove"></i>
						    </button>
                            <i class="icon-remove"></i>
                            <asp:Label ID="lblErrorMessageText" runat="server"></asp:Label>
                        </div>
            </div>
            <div class="table-header">
                <asp:Label ID="lblHeaderCategory" runat="server" Text="Edit Category"></asp:Label>
	        </div>
            <table class="table table-hover dataTable table-bordered ">
                <tr>
                    <td class="span2">CategoryID
                    </td>
                    <td><asp:Label ID="lblCategoryID" runat="server" Text="Label"></asp:Label>
                        <asp:HiddenField ID="hdnCategoryID" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="span2">Category Name
                    </td>
                    <td><asp:TextBox ID="txtCategoryName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvCategoryName" runat="server" 
                        ErrorMessage="Category Name" ControlToValidate="txtCategoryName"
                        Text="*" ForeColor="Red" ValidationGroup="Categories">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="span2">Category Priority
                    </td>
                    <td>  
                        <asp:DropDownList ID="ddlCategoryPriority" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="span2">Dedicated Services Server / Client</td>
                    <td>  <asp:RadioButtonList runat="server" ID="rblDedicatedServices" CssClass="radio" RepeatDirection="Horizontal" Width="150px" style="border:none;">
                            <asp:ListItem Value="true" Text="Yes"></asp:ListItem>
                            <asp:ListItem Value="false" Text="No" Selected="True"></asp:ListItem>
                          </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="rfvDedicated" runat="server" ControlToValidate="rblDedicatedServices" ErrorMessage="*" ForeColor="Red" Text="*" ValidationGroup="Categories"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="span2">Category Status
                    </td>
                    <td> <asp:DropDownList ID="ddlRecordStatus" runat="server">
                            <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Inactive"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="Deleted"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="span2">Is Pay-As-You-Go Model Applicable?
                    </td>
                    <td>  
                        <asp:CheckBox ID="chkIsPayAsYouGo" runat="server"/>
                    </td>
                </tr>
                <tr>
                    <td class="span2">
                    </td>
                    <td><asp:Button ID="btnSave" OnClientClick="return ConfirmOnDelete()" runat="server" Text="Save" class="btn btn-primary" 
                            onclick="btnSave_Click"  ValidationGroup="Categories" />
                        <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-warning" 
                            onclick="btnReset_Click"/>
                       
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
            $('#tblCategories').dataTable(
                  {
                      "aoColumnDefs": [
                          { 'bSortable': false, 'aTargets': [0, 4] }
                      ]
                  }
         );
        });
   </script>
    <script type="text/javascript">
        function ConfirmOnDelete() {
            var cb = document.getElementById('ContentPlaceHolder1_chkIsPayAsYouGo');
            if (cb.checked) {
                return true;
            }
            else {
                if (confirm("This change in setting will revoke the Pay-As-You-Go model in all related services of this Category. Please confirm if you still want to revoke this setting?") == true)
                    return true;
                else
                    cb.checked = true;
                    return false;
            }
        }
    </script>
</asp:Content>

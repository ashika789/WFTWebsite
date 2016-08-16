<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminPages.Master" AutoEventWireup="true" CodeBehind="WFTServices.aspx.cs" Inherits="WFTCloud.Admin.WFTServices" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <title>WFT Services</title>
	    <meta name="description" content="List of service categories can be seen here." />
        <meta name="keywords" content="WFT Service cateory" />
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <style type="text/css">
        .ajax__colorPicker_container {
            width:50%;
        }
    </style>
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
					Packages changes saved successfully.
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
            <ul id="tabPackages" class="nav nav-tabs">
                <li id="liStandardPackage" runat="server" class="active">
                    <a href="/Admin/WFTServices.aspx?showview=standardpackages">
                        <i class="green icon-th-list bigger-110"></i>
						Standard Packages
                    </a>
                </li>
                <li id="liCustomPackage" runat="server">
                    <a href="/Admin/WFTServices.aspx?showview=custompackages">
                        <i class="green icon-list-alt bigger-110"></i>
						Custom Packages
                    </a>
                </li>
            </ul>
            <div class="tab-content">
            <div id="divStandardPackages" class="tab-pane in active" runat="server">
                <div class="row-fluid">
                        <div id="divWFTServicesSuccessMessage" runat="server" visible="false" class="alert alert-block alert-success span6">
                            <button data-dismiss="alert" class="close" type="button">
                                <i class="icon-remove"></i>
                            </button>
                            <p>
                                <i class="icon-ok"></i>
                                Packages changes saved successfully.</p>
                        </div>
                        </div>
                <div class="table-header">
		            Standard Packages	        
                </div>
                <div class="dataTables_wrapper">
                    <div role="grid" class="dataTables_wrapper">
                        <table id="tblStandard" class="table table-striped table-bordered table-hover dataTable">
                            <thead>
                            <tr role="row">
                                <th class="center" role="columnheader">
                                    <label><input type="checkbox" class="ace"/>
                                        <span class="lbl"></span>
                                    </label>
                                </th>
                                <th role="columnheader">Service Name</th>
                                <th role="columnheader">Release</th>
                                <th role="columnheader">Type of System</th>
                                <th role="columnheader">Total Cost ($)</th>
                                <th role="columnheader">WFT Cloud Cost ($)</th>
                                <th role="columnheader">Third Party Cost ($)</th>
                                <th role="columnheader">Basic Support</th>
                                <th role="columnheader">Priority</th>
                                <th role="columnheader">Status</th>
                                <th role="columnheader" style="text-align:center;">Options</th>
                            </tr>
                            </thead>
                            <tbody role="alert">
                            <asp:Repeater ID="rptrStdWFTServices" runat="server">
                            <ItemTemplate>
                            <tr>
                                <td class="center">
                                    <asp:CheckBox ID="chkSelect" runat="server" class="ace" />
                                </td>
                                <td>
                                    <a href='WFTServices.aspx?showview=editservice&serviceid=<%# Eval("ServiceID")%>'>
                                    <%# Eval("ServiceName")%>
                                    </a>
                                    <asp:HiddenField ID="hdnServiceID" runat="server" Value='<%# Eval("ServiceID")%>' />
                                </td>
                                <td><%# Eval("ReleaseVersion")%></td>
                                <td><%# Eval("SystemType")%></td>
                                <td><%# Eval("WFTCloudPrice")%></td>
                                <td><%# Eval("ReleaseVersion")%></td>
                                <td><%# Eval("ThirdPartyServicePrice")%></td>
                                <td><%# Eval("SupportIncluded")%></td>
                                <td><%# Eval("Priority")%></td>
                                <td><%# WFTCloud.GeneralReusableMethods.GetStatusString(Eval("RecordStatus").ToString()) %></td>
                                <td>
                                    <div class="action-buttons">
                                    <a data-rel="tooltip" title="Edit Standard Package" href='WFTServices.aspx?showview=editservice&serviceid=<%# Eval("ServiceID")%>' class="green">
                                        <i class="icon-pencil bigger-130"></i>
                                    </a>
                                    <a data-rel="tooltip" title="Activate/Deactivate" href='WFTServices.aspx?<%# WFTCloud.GeneralReusableMethods.GetActivateDeactivateAction(Eval("RecordStatus").ToString()) %>=<%# Eval("ServiceID")%>&showview=standardpackages' class="blue">
                                        <i class='<%# WFTCloud.GeneralReusableMethods.GetActivateDeactivateIcon(Eval("RecordStatus").ToString()) %>'></i>
                                    </a>
                                  <%--  <a data-rel="tooltip" title="Delete Standard Package" href='WFTServices.aspx?delete=<%# Eval("ServiceID")%>&showview=standardpackages' class="red">
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
                    <asp:Button ID="btnStdAddNew" runat="server" Text="Add New" 
                        class="btn btn-primary" onclick="btnStdAddNew_Click" />
                    <asp:Button ID="btnStdActivate" runat="server" Text="Activate" 
                        class="btn btn-info" onclick="btnStdActivate_Click" />
                    <asp:Button ID="btnStdDeactivate" runat="server" Text="Deactivate" 
                        class="btn btn-warning" onclick="btnStdDeactivate_Click" />
                    <%--<asp:Button ID="btnStdDelete" runat="server" Text="Delete" 
                        class="btn btn-danger" onclick="btnStdDelete_Click" />--%>
                    <asp:CheckBox ID="chkStdShowDeleted" runat="server" AutoPostBack="True" 
                        oncheckedchanged="chkStdShowDeleted_CheckedChanged"/>
                    <span class="label label-warning arrowed-right">Show De-Activated Services</span>
                </div>
            </div>
            <div id="divCustomPackages" class="tab-pane" runat="server">
                <div class="row-fluid">
                        <div id="divCustomSuccess" runat="server" visible="false" class="alert alert-block alert-success span6">
                            <button data-dismiss="alert" class="close" type="button">
                                <i class="icon-remove"></i>
                            </button>
                            <p>
                                <i class="icon-ok"></i>
                                WFT Services status updated successfully.
                            </p>
                        </div>
                        </div>
	            <div class="table-header">
		            Custom Packages	        
                </div>
                <div class="dataTables_wrapper">
                    <div role="grid" class="dataTables_wrapper">
                        <table id="tblCustomPackages" class="table table-striped table-bordered table-hover dataTable">
                            <thead>
                            <tr role="row">
                                <th class="center" role="columnheader">
                                    <label><input type="checkbox" class="ace"/>
                                        <span class="lbl"></span>
                                    </label>
                                </th>
                                <th role="columnheader">Service Name</th>
                                <th role="columnheader">Release</th>
                                <th role="columnheader">Type of System</th>
                                <th role="columnheader">Total Cost ($)</th>
                                <th role="columnheader">WFT Cloud Cost ($)</th>
                                <th role="columnheader">Third Party Cost ($)</th>
                                <th role="columnheader">Basic Support</th>
                                <th role="columnheader">Priority</th>
                                <th role="columnheader">Status</th>
                                <th role="columnheader" style="text-align:center; width:6%">Options</th>
                            </tr>
                            </thead>
                            <tbody role="alert">
                            <asp:Repeater ID="rptrCustomServices" runat="server">
                            <ItemTemplate>
                            <tr>
                                <td class="center">
                                    <asp:CheckBox ID="chkSelect" runat="server" class="ace" />
                                </td>
                                <td>
                                    <a href='WFTServices.aspx?showview=editservice&serviceid=<%# Eval("ServiceID")%>'>
                                    <%# Eval("ServiceName")%>
                                    </a>
                                    <asp:HiddenField ID="hdnCustServiceID" runat="server" Value='<%# Eval("ServiceID")%>' />
                                </td>
                                <td><%# Eval("ReleaseVersion")%></td>
                                <td><%# Eval("SystemType")%></td>
                                <td><%# Eval("WFTCloudPrice")%></td>
                                <td><%# Eval("ReleaseVersion")%></td>
                                <td><%# Eval("ThirdPartyServicePrice")%></td>
                                <td><%# Eval("SupportIncluded")%></td>
                                <td><%# Eval("Priority")%></td>
                                <td><%# WFTCloud.GeneralReusableMethods.GetStatusString(Eval("RecordStatus").ToString()) %></td>
                                <td>
                                    <div class="action-buttons">
                                    <a data-rel="tooltip" title="Edit Custom Package" href='WFTServices.aspx?showview=editservice&serviceid=<%# Eval("ServiceID")%>' class="green">
                                        <i class="icon-pencil bigger-130"></i>
                                    </a>
                                    <a data-rel="tooltip" title="Activate/Deactivate" href='WFTServices.aspx?<%# WFTCloud.GeneralReusableMethods.GetActivateDeactivateAction(Eval("RecordStatus").ToString()) %>=<%# Eval("ServiceID")%>&showview=custompackages' class="blue">
                                        <i class='<%# WFTCloud.GeneralReusableMethods.GetActivateDeactivateIcon(Eval("RecordStatus").ToString()) %>'></i>
                                    </a>
                                  <%--  <a data-rel="tooltip" title="Delete Custom Package" href='WFTServices.aspx?delete=<%# Eval("ServiceID")%>&showview=custompackages' class="red">
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
                    <asp:Button ID="btnAddNewCustom" runat="server" Text="Add New" OnClick="btnAddNewCustom_Click" 
                        class="btn btn-primary" />
                    <asp:Button ID="btnActivateCustom" runat="server" Text="Activate" OnClick="btnActivateCustom_Click"
                        class="btn btn-info" />
                    <asp:Button ID="btnDeactivateCustom" runat="server" Text="Deactivate" OnClick="btnDeactivateCustom_Click"
                        class="btn btn-warning" />
                  <%--  <asp:Button ID="btnDeleteCustom" runat="server" Text="Delete" OnClick="btnDeleteCustom_Click"
                        class="btn btn-danger" />--%>
                    <asp:CheckBox ID="chkCustomShowDeleted" runat="server" AutoPostBack="True" 
                        oncheckedchanged="chkCustomShowDeleted_CheckedChanged"/>
                    <span class="label label-warning arrowed-right">Show Deactivated Services</span>
                </div>
            </div>
            </div>
        </div>
        </div>
        </div>
    </asp:View>
    <asp:View ID="vwEdit" runat="server">
        <div class="row-fluid">
        <div class="span12">
        <div class="row-fluid">
	        <a href="WFTServices.aspx?showview=standardpackages">&lt;&lt; Back&nbsp;</a>
            <div class="row-fluid">
                <div id="lblSuccessMessage" runat="server" visible="false" class="alert alert-block alert-success span6">
				    <button data-dismiss="alert" class="close" type="button">
					    <i class="icon-remove"></i>
				    </button>
				    <p>
                        <i class="icon-ok"></i>
					    Service changes saved successfully.
				    </p>
			    </div>
                <div id="lblErrorMessage" runat="server" visible="false" class="alert alert-error span6">
				    <button data-dismiss="alert" class="close" type="button">
					    <i class="icon-remove"></i>
				    </button>
                    <i class="icon-remove"></i>
                    <asp:Label ID="lblErrorMessageText" runat="server" Text="Error saving service details. Please try again later."></asp:Label>
                </div>
            </div>
            <div class="table-header">
                <asp:Label ID="lblHeader" runat="server" Text="Service Details"></asp:Label>
	        </div>
            <table class="table table-hover dataTable table-bordered ">
                <tr>
                    <td class="span2">Service Category
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlServiceCategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlServiceCategory_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="span4">Service Name
                    </td>
                    <td>
                        <asp:TextBox ID="txtServiceName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvServiceName" runat="server"  ValidationGroup="WFTServices"
                        ErrorMessage="Service Name" ControlToValidate="txtServiceName"
                        Text="*" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                        <asp:HiddenField ID="hdnServiceID" runat="server" Value="0" />
                    </td>
                </tr>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <tr>
                    <td class="span2">Service Description
                    </td>
                    <td>
                        <asp:TextBox ID="txtServiceDesc" runat="server" TextMode="MultiLine" class="span12" Height="200px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvServiceDesc" runat="server"  ValidationGroup="WFTServices"
                        ErrorMessage="Service Description" ControlToValidate="txtServiceDesc"
                        Text="*" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                         <asp:HtmlEditorExtender ID="HtmlEditorServiceDesc" runat="server" TargetControlID="txtServiceDesc" DisplaySourceTab="true">
                            </asp:HtmlEditorExtender>
                    </td>
                </tr>
                <tr>
                    <td class="span2">System Type
                    </td>
                    <td>
                        <asp:TextBox ID="txtSystemType" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvSystemType" runat="server"  ValidationGroup="WFTServices"
                        ErrorMessage="System Type" ControlToValidate="txtSystemType"
                        Text="*" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="span2">Release Version
                    </td>
                    <td>
                        <asp:TextBox ID="txtReleaseVersion" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  ValidationGroup="WFTServices"
                        ErrorMessage="Release Version" ControlToValidate="txtReleaseVersion"
                        Text="*" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="span2">WFT Cloud Charge
                    </td>
                    <td>
                        <asp:TextBox ID="txtWFTCloudCharge" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvWFTCloudCharge" runat="server"  ValidationGroup="WFTServices"
                        ErrorMessage="WFT Cloud Charge" ControlToValidate="txtWFTCloudCharge"
                        Text="*" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="span2">Third Party Service Charge
                    </td>
                    <td>
                        <asp:TextBox ID="txt3rdPartyService" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv3rdPartyService" runat="server"  ValidationGroup="WFTServices"
                        ErrorMessage="Third Party Service Charge" ControlToValidate="txt3rdPartyService"
                        Text="*" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="span2">Initial Hold Amount
                    </td>
                    <td>
                        <asp:TextBox ID="txtInitialHoldAmount" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvInitialHoldAmount" runat="server"  ValidationGroup="WFTServices"
                        ErrorMessage="Initial Hold Amount" ControlToValidate="txtInitialHoldAmount"
                        Text="*" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="span2">Usage Unit
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlUsageUnit" runat="server">
                            <asp:ListItem Text="Days" Value="Day" />
                            <asp:ListItem Text="Hours" Value="Hour" />
                            <asp:ListItem Text="Months" Value="Month" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="span2">Support Included
                    </td>
                    <td>
                        <asp:CheckBox ID="chkSupportIncluded" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="span2">User Specific
                    </td>
                    <td>
                        <asp:CheckBox ID="chkUserSpecific" runat="server" OnCheckedChanged="chkUserSpecific_CheckedChanged" AutoPostBack="true" />
                    </td>
                </tr>
                <asp:Panel ID="pnlUserSpecific" runat="server" Visible="false">
                    <tr>
                        <td class="span2">
                            For User
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlUserSpecific">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </asp:Panel>
                <tr>
                    <td class="span2">Pay as you go
                    </td>
                    <td>
                        <asp:CheckBox ID="chkPayAsYouGo" runat="server" OnCheckedChanged="chkPayAsYouGo_CheckedChanged" AutoPostBack="true" />
                    </td>
                </tr>
                <asp:Panel runat="server" ID="PnlPayAsYouGo" Visible="false">
                    <tr>
                        <td class="span2">
                            Minimum Usage In Hours
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtMinimumHours"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"  ValidationGroup="WFTServices"
                        ErrorMessage="Priority" ControlToValidate="txtMinimumHours"
                        Text="*" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtMinimumHours" runat="server" ErrorMessage="Enter numbers only" ValidationGroup="WFTServices"
                            ForeColor="Red" ValidationExpression="^[0-9]+$"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Cost Per Minimum Usage
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtCostMinimumUsage"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"  ValidationGroup="WFTServices"
                        ErrorMessage="Priority" ControlToValidate="txtCostMinimumUsage"
                        Text="*" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Cost Per Every Additional Hours
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtCostAdditionalHours"></asp:TextBox>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"  ValidationGroup="WFTServices"
                        ErrorMessage="Priority" ControlToValidate="txtCostAdditionalHours"
                        Text="*" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </asp:Panel>
                <tr>
                    <td class="span2">Priority
                    </td>
                    <td>
                        <asp:TextBox ID="txtServicePriority" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvServicePriority" runat="server"  ValidationGroup="WFTServices"
                        ErrorMessage="Priority" ControlToValidate="txtServicePriority"
                        Text="*" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revPriority" ControlToValidate="txtServicePriority" runat="server" ErrorMessage="Enter numbers only" ValidationGroup="WFTServices"
                             ForeColor="Red" ValidationExpression="^[0-9]+$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="span2">Package length in months
                    </td>
                    <td>
                        <asp:TextBox ID="txtPackageLength" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvPackageLength" runat="server" 
                        ErrorMessage="Package length" ControlToValidate="txtPackageLength" ValidationGroup="WFTServices"
                        Text="*" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="span2">3 Months Discount %</td>
                    <td>
                        <asp:TextBox ID="txt3MonthsSaving" runat="server"></asp:TextBox>
                        <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Discount % Sholud be less than 99%" ForeColor="Red" MinimumValue="0" Type="Double" ValidationGroup="WFTServices" ControlToValidate="txt3MonthsSaving" MaximumValue="95"></asp:RangeValidator>
                    </td>
                </tr>
                <tr>
                    <td class="span2">6 Months Discount %</td>
                    <td>
                        <asp:TextBox ID="txt6MonthsSaving" runat="server"></asp:TextBox>
                        <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="Discount % Sholud be less than 99%" ForeColor="Red" MinimumValue="0" Type="Double" ValidationGroup="WFTServices" ControlToValidate="txt6MonthsSaving" MaximumValue="99"></asp:RangeValidator>
                    </td>
                </tr>
                <tr>
                    <td class="span2">9 Months Discount %</td>
                    <td>
                        <asp:TextBox ID="txt9MonthsSaving" runat="server"></asp:TextBox>
                        <asp:RangeValidator ID="RangeValidator3" runat="server" ErrorMessage="Discount % Sholud be less than 99%" ForeColor="Red" MinimumValue="0" Type="Double" ValidationGroup="WFTServices" ControlToValidate="txt9MonthsSaving" MaximumValue="99"></asp:RangeValidator>
                    </td>
                </tr>
                <tr>
                    <td class="span2">12 Months Discount %</td>
                    <td>
                        <asp:TextBox ID="txt12MonthsSaving" runat="server"></asp:TextBox>
                        <asp:RangeValidator ID="RangeValidator4" runat="server" ErrorMessage="Discount % Sholud be less than 99%" ForeColor="Red" MinimumValue="0" Type="Double" ValidationGroup="WFTServices" ControlToValidate="txt12MonthsSaving" MaximumValue="99"></asp:RangeValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Trial Service
                    </td>
                    <td>
                        <asp:CheckBox runat="server" ID="chkTrialService" />
                    </td>
                </tr>
                <%--<tr>
                    <td class="span2">Trial Amount
                    </td>
                    <td>
                        <asp:TextBox ID="txtTrialAmount" runat="server"></asp:TextBox>
                    </td>
                </tr>--%>
                <tr>
                    <td class="span2">Service Status
                    </td>
                    <td> <asp:DropDownList ID="ddlRecordStatus" runat="server">
                            <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Inactive"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="Deleted"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="span2">Display as Blinking Text
                    </td>
                    <td>
                        <asp:CheckBox ID="chkDisplayBlinking" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="span2">Display Text Color
                    </td>
                    <td>
                        <asp:TextBox ID="txtDisplayTextColor" runat="server" ReadOnly="true"></asp:TextBox>
        <div id="preview" style="width:70px;height:70px;border:1px solid #000;">
        </div>
        <asp:colorpickerextender id="Colorpickerextender1" runat="server"
             targetcontrolid="txtDisplayTextColor"
             samplecontrolid="preview"
             popupbuttonid="txtDisplayTextColor"
             PopupPosition ="Center"    
              />
                    </td>
                </tr>
                <tr>
                    <td class="span2">
                    </td>
                    <td><asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary" ValidationGroup="WFTServices" 
                            onclick="btnSave_Click"/>
                        <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-warning" 
                            ValidationGroup="vgReset" onclick="btnReset_Click"/>
                     
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
        $('#tblStandard').dataTable(
        {
            "aoColumnDefs": [
                { 'bSortable': false, 'aTargets': [0, 10] }
            ]
        }
        );
        $('#tblCustomPackages').dataTable(
        {
            "aoColumnDefs": [
                { 'bSortable': false, 'aTargets': [0, 10] }
            ]
        }
        );
        $(".ajax__colorPicker_container").css("width", "50%");
    });
    $(document).ready(function () {
        $(".ajax__colorPicker_container").css("width", "50%");
    });
</script>
</asp:Content>

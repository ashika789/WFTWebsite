<%@ Page Title="" Language="C#" MasterPageFile="~/Customer/CustomerPages.Master" AutoEventWireup="true" CodeBehind="CRMRequests.aspx.cs" Inherits="WFTCloud.Customer.CRMRequests" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>CRM Requests</title>
	    <meta name="description" content="we have an three tabs, they are Available services, Subscribed services and Manage CRM Issues. Each view shows the respective details" />
        <meta name="keywords" content="WFT Services and Manage CRM Issue" />
    <style type="text/css">
        .controls {
            margin-top: 0px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hidCrmRequestID" runat="server"  Value="" />
    <div class="table-header">
	    <strong>CRM Request Details </strong>	        
    </div>
    <div class="row-fluid">
		<div class="span12 widget-container-span">
			<div class="widget-box">
				<div class="widget-header no-border">
					<ul class="nav nav-tabs" id="Ul1">
							<li id="liManage" runat="server" class="active">
								<a href="#">My CRM Requests</a>
							</li>
						</ul>
				</div>
				<div class="widget-body">
					<div class="widget-main padding-12">
						<div class="tab-content">
							<div id="Manage" class="tab-pane in active" runat="server">
                                <asp:MultiView ID="mvCrmIssues" runat="server" ActiveViewIndex="0">
                                    <asp:View ID="vwManageCRM" runat="server">
                                        <div class="row-fluid">
                                        <div class="span12">
                                        <div class="row-fluid">
	                                        <div class="table-header">
		                                       My CRM Requests	        
                                            </div>
                                            <div class="dataTables_wrapper">
                                                <div role="grid" class="dataTables_wrapper">
                                                    <table id="tblManageCrm" class="table table-striped table-bordered table-hover dataTable">
                                                        <thead>
                                                        <tr role="row">
                                                            <th role="columnheader">CRM Code</th>
                                                            <th role="columnheader">Category</th>
                                                            <th role="columnheader">Service</th>
                                                            <th role="columnheader">Issue Type</th>
                                                            <th role="columnheader">Status</th>
                                                            <th role="columnheader" style="text-align:center;">Options</th>
                                                        </tr>
                                                        </thead>
                                                        <tbody role="alert">
                                                        <asp:Repeater ID="rptrManageCRMIssue" runat="server">
                                                        <ItemTemplate>
                                                        <tr>
                                                            <td>CRM<%#Eval("CRMRequestID") %></td>
                                                            <td><%# Eval("CategoryName") != null?Eval("CategoryName"):"-"%>
                                                                <asp:HiddenField ID="hdnCRMRequestID" runat="server" Value='<%# Eval("CRMRequestID")%>' />
                                                            </td>
                                                            <td><%# Eval("ServiceName")== null? "-":Eval("ServiceName")%></td>
                                                            <td><%# Eval("CRMIssueTypeDesc")%></td>
                                                            <td><%# Eval("CRMRequestStatusDesc").ToString()=="Assigned"?"Work In Progress":(Eval("CRMRequestStatusDesc")) %></td>
                                                            <td style="text-align:center; vertical-align:middle;">
                                                                <div class="action-buttons">
                                                                <a data-rel="tooltip" title="View CRM Request" href="/Customer/CRMRequests.aspx?userid=<%#Eval("CustomerID") %>&crmid=<%#Eval("CRMRequestID")%>" class="green">
                                                                    <i class="icon-pencil bigger-130"></i>
                                                                </a>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        </ItemTemplate>
                                                        </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                                </div>
                                        </div>
                                            <br />
                                            <div class="row-fluid">
                                                <asp:Button runat="server" ID="btnNewRequest" Text="New CRM Request" style="float:left; top: 0px; left: 0px;" CssClass="btn btn-primary" OnClick="btnNewRequest_Click" Visible="False"/>
                                            </div>
                                        </div>
                                        </div>
                                        </div>
                                    </asp:View>
                                    <asp:View ID="vmCRMForm" runat="server">
                                        <div class="row-fluid">
                                            <div id="divManageSuccessMessage" runat="server" visible="false" class="alert alert-block alert-success span6">
							                    <button data-dismiss="alert" class="close" type="button">
								                    <i class="icon-remove"></i>
							                    </button>
							                    <p>
                                                    <i class="icon-ok"></i>
                                                    <asp:Label ID="lblCRMCuccess" runat="server" Text="CRM Request Submitted successfully."></asp:Label>
							                    </p>
						                    </div>
                                            <div id="divManageErrorMessage" runat="server" visible="false" class="alert alert-error span6">
						                        <button data-dismiss="alert" class="close" type="button">
							                        <i class="icon-remove"></i>
						                        </button>
                                                <i class="icon-remove"></i>
					                            Error saving CRM Request details.
                                            </div>
                                        </div>
                                       <h4><a href="/Customer/CRMRequests.aspx?userid=<%=UserMembershipID %>" >< Back To CRM Requests </a></h4>
                                        <div class="table-header">
                                            <asp:Label ID="lblCRMHeader" runat="server" Text="New CRM Request"></asp:Label>
                                        </div>
                                        <table class="table table-striped table-bordered table-hover dataTable">
                                    <tr>
                                        <td class="span2">CRM Request Type
                                        </td>
                                        <td><asp:RadioButtonList runat="server" ID="rblCRMRqType" AutoPostBack="true" OnSelectedIndexChanged="rblCRMRqType_SelectedIndexChanged" RepeatDirection="Horizontal" RepeatLayout="Flow" Width="348px" CssClass="radio">
                                            <asp:ListItem Value="1" Selected="True" Text="Existing Service Related"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="Other"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr id="trServiceCategory" runat="server" >
                                        <td class="span2">Service Category
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlServiceCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlServiceCategory_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvServiceCategory" runat="server" ErrorMessage="*"
                                                 ControlToValidate="ddlServiceCategory" InitialValue="0" ValidationGroup="CRMIssue" Display="Dynamic" CssClass="error" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr id="trServiceName" runat="server" >
                                        <td class="span2">Service Name</td>
                                        <td><asp:DropDownList ID="ddlReportAgainst" runat="server">
                                    </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>Subject</td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtSubject" MaxLength="99" TextMode="MultiLine" Width="90%" Rows="1" ></asp:TextBox> 
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                            ErrorMessage="*" ControlToValidate="txtSubject" ValidationGroup="CRMIssue"
                                            Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="span3">Issue Description
                                        </td>
                                        <td><asp:TextBox ID="txtIssue" runat="server" TextMode="MultiLine" Height="200px" Width="90%"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvIssue" runat="server" 
                                            ErrorMessage="Category Name" ControlToValidate="txtIssue" ValidationGroup="CRMIssue"
                                            Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trAddAttachment">
                                        <td class="span2">Add Attachment</td>
                                        <td>
                                            <asp:FileUpload ID="fluAddAttachments" runat="server" />
                                        </td>
                                    </tr>
                                     <tr runat="server" id="trAttachment">
                                                <td>
                                                    Attachment
                                                </td>
                                                <td>
                                                    <asp:HyperLink runat="server" ID="hypAttchmentLink" Target="_blank" >Click Here To View</asp:HyperLink>
                                                    &nbsp;<asp:Label runat="server" ID="lblNoAttachment" Text="No Attachment" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                    <tr>
                                        <td class="span2">Issue Type</td>
                                        <td><asp:DropDownList ID="ddlIssueType" runat="server">
                                            </asp:DropDownList></td>
                                     </tr>
                                    <asp:Panel ID="pnlCRMStatus" runat="server" Visible="false">
                                    <tr>
                                        <td>CRM Status</td>
                                        <td><asp:Label runat="server" ID="lblCRMStatusEdit"></asp:Label></td>
                                    </tr>
                                    </asp:Panel>
                                    <tr runat="server" id="trBtnSave">
                                        <td class="span2"></td>
                                        <td >
                                            <asp:Button ID="btnCrmIssueSave" runat="server" Text="Save" class="btn btn-primary" OnClick="btnCrmIssueSave_Click" ValidationGroup="CRMIssue" />
                                        </td>
                                    </tr>
                                </table>
                                    </asp:View>
                                    <asp:View ID="vwCRMDEtails" runat="server">
                                        <asp:HiddenField runat="server" ID="hidCRMRequetIDVw" />
                                         <h4><a href="/Customer/CRMRequests.aspx?userid=<%=UserMembershipID %>" >< Back To CRM Requests </a></h4>
                                        <div class="table-header">
                                            <asp:Label ID="lblCRMHeadVw" runat="server" Text="New CRM Request"></asp:Label>
                                        </div>
                                        <table class="table table-striped table-bordered table-hover dataTable">
                                            <tr>
                                                <td class="span2">CRM Code</td>
                                                <td><asp:Label runat="server" ID="lblCRMCodeVw"></asp:Label></td>
                                            </tr>
                                            <tr runat="server" id="trServiceCategoryVw">
                                                <td class="span2">Service Category</td>
                                                <td><asp:Label runat="server" ID="lblServiceCategoryvw"></asp:Label></td>
                                            </tr>
                                            <tr runat="server" id="trServiceVw">
                                                <td class="span2">Service Name</td>
                                                <td><asp:Label runat="server" ID="lblSevicenameVw"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>Subject</td>
                                                <td><asp:Label runat="server" ID="lblSubjectVw"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="span2">Issue Description</td>
                                                <td><asp:Label runat="server" ID="lblIssueDescVw"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="span2">Attachment</td>
                                                <td>
                                                    <asp:HyperLink runat="server" ID="hypAttachmentVw" Target="_blank"></asp:HyperLink>
                                                    <asp:Label runat="server" ID="lblNoAttachmentVw" Text="No Attachment"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="span2">Issue Type</td>
                                                <td><asp:Label runat="server" ID="lblIssueTypeVw"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="span2">CRM Status</td>
                                                <td><asp:Label runat="server" ID="lblCRMStatusVw"></asp:Label></td>
                                            </tr>
                                            <tr runat="server" id="trResolutionDesc" visible="false">
                                                <td class="span2">Resolution Description</td>
                                                <td><asp:Label runat="server" ID="lblResolutionDesc"></asp:Label></td>
                                            </tr>
                                            <tr runat="server" id="trReslAttach" visible="false">
                                                <td class="span2">Resolution Attachment</td>
                                                <td>
                                                    <asp:HyperLink runat="server" ID="hypReslnAttach" Target="_blank"></asp:HyperLink>
                                                    <asp:Label runat="server" ID="lblNoReslnAttach" Text="No Attachment"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="trEditIfResloved" visible="false">
                                                <td class="span2"></td>
                                                <td><asp:Button runat="server" ID="btnEdit" OnClick="btnEdit_Click" CssClass="btn btn-primary" Text="Edit" /></td>
                                            </tr>
                                        </table>
                                        <br />
                                        <br />
                                        <div runat="server" id="dvCRMHistory" visible="false">
                                        <div class="table-header">
                                            <asp:Label ID="Label1" runat="server" Text="CRM Request History"></asp:Label>
                                        </div>
                                        <div class="dataTables_wrapper">
                                                <div role="grid" class="dataTables_wrapper">
                                                    <table id="Table1" class="table table-striped table-bordered table-hover dataTable">
                                                        <thead>
                                                        <tr role="row">
                                                            <th role="columnheader">Subject</th>
                                                            <th role="columnheader">Issue Description</th>
                                                            <th role="columnheader">Issue Type</th>
                                                            <th role="columnheader">Resolution</th>
                                                            <th role="columnheader">Status</th>
                                                            <th role="columnheader" style="text-align:center;">Options</th>
                                                        </tr>
                                                        </thead>
                                                        <tbody role="alert">
                                                        <asp:Repeater ID="rptrHistory" runat="server">
                                                        <ItemTemplate>
                                                        <tr>
                                                            <td><%# Eval("CRMSubject")== null?"-":Eval("CRMSubject") %></td>
                                                            <td><%# Eval("IssueDescription")== null?"-":Eval("IssueDescription") %></td>
                                                            <td><%# Eval("CRMIssueTypeDesc")%></td>
                                                            <td><%# Eval("CRMResolutionSent")== null?"-":Eval("CRMResolutionSent") %></td>
                                                            <td><%# Eval("CRMRequestStatusDesc")%></td>
                                                            <td style="text-align:center; vertical-align:middle;">
                                                                <div class="action-buttons">
                                                                <a data-rel="tooltip" title="View CRM Request" href="/Customer/CRMRequests.aspx?userid=<%#Eval("CustomerID") %>&crmid=<%#Eval("CRMRequestID")%>&Crmhistoryid=<%#Eval("CRMHistoryID")%>" class="green">
                                                                    <i class="icon-pencil bigger-130"></i>
                                                                </a>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        </ItemTemplate>
                                                        </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                                </div>
                                        </div>
                                        </div>
                                    </asp:View>
                                    <asp:View ID="vwNoCrmIssue" runat="server">
                                        <div id="divNoCrmIssue" runat="server" class="alert alert-warning">
                                            <button data-dismiss="alert" class="close" type="button">
                                            <i class="icon-remove"></i>
                                            </button>
                                            <i class="icon-remove"></i>
                                            <strong>
                                                <asp:Label ID="lblNoCRM" runat="server" Text="There are no Subscribed Services for the user."></asp:Label>
                                            </strong>
                                        </div>
                                    </asp:View>
                                    <asp:View ID="vwCRMHistoryDetails" runat="server">
                                         <h4><a href='/Customer/CRMRequests.aspx?userid=<%=UserMembershipID %>&crmid=<%= CRMCodeRequestID %>' >&lt; Back </a></h4>
                                        <div class="table-header">
                                            <asp:Label ID="Label2" runat="server" Text="CRM Request History Detail"></asp:Label>
                                        </div>
                                        <table class="table table-striped table-bordered table-hover dataTable">
                                            <tr>
                                                <td class="span2">CRM Code</td>
                                                <td><asp:Label runat="server" ID="lblCRMHCode"></asp:Label></td>
                                            </tr>
                                            <tr runat="server" id="trCRMHCategory">
                                                <td class="span2">Service Category</td>
                                                <td><asp:Label runat="server" ID="lblCRMHCategory"></asp:Label></td>
                                            </tr>
                                            <tr runat="server" id="trCRMHService">
                                                <td class="span2">Service Name</td>
                                                <td><asp:Label runat="server" ID="lblCRMHServiceName"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>Subject</td>
                                                <td><asp:Label runat="server" ID="lblCRMHSubject"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="span2">Issue Description</td>
                                                <td><asp:Label runat="server" ID="lblCRMHIssueDesc"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="span2">Attachment</td>
                                                <td>
                                                    <asp:HyperLink runat="server" ID="hypCRMHAttachment" Target="_blank">[hypCRMHAttachment]</asp:HyperLink>
                                                    <asp:Label runat="server" ID="lblCRMHAttachment" Text="No Attachment"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="span2">Issue Type</td>
                                                <td><asp:Label runat="server" ID="lblCRMHIssueType"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="span2">CRM Status</td>
                                                <td><asp:Label runat="server" ID="lblCRMHStatus"></asp:Label></td>
                                            </tr>
                                            <tr runat="server" id="trCRMHResolutionDesc" visible="false">
                                                <td class="span2">Resolution Description</td>
                                                <td><asp:Label runat="server" ID="lblCRMHResolutionDesc"></asp:Label></td>
                                            </tr>
                                            <tr runat="server" id="trCRMHResAttachement" visible="false">
                                                <td class="span2">Resolution Attachment</td>
                                                <td>
                                                    <asp:HyperLink runat="server" ID="hypCRMHResAttachement" Target="_blank">[hypCRMHResAttachement]</asp:HyperLink>
                                                    <asp:Label runat="server" ID="lblCRMHResAttach" Text="No Attachment"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
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
            $('#tblManageCrm').dataTable(
                {
                    "aoColumnDefs": [
                        { 'bSortable': false, 'aTargets': [5] }
                    ]
                    , "aaSorting": [[0, "desc"]]
                }
         );
        });
        jQuery(function ($) {
            $('#Table1').dataTable(
                {
                    "aoColumnDefs": [
                        { 'bSortable': false, 'aTargets': [5] }
                    ]
                }
         );
        }); 
   </script>
</asp:Content>

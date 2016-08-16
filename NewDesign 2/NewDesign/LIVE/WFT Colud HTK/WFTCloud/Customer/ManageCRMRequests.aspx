<%@ Page Title="" Language="C#" MasterPageFile="~/Customer/CustomerPages.Master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="ManageCRMRequests.aspx.cs" Inherits="WFTCloud.Customer.ManageCRMRequests" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title> Manage CRM Requests </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager ID="scriptManager" runat="server"></asp:ScriptManager>
    
    <div class="row-fluid">
		<div class="span12 widget-container-span">
			<div class="widget-box">
				<div class="widget-header no-border">
					<ul class="nav nav-tabs" id="Ul1">
							<li id="liManage" runat="server">
								<a href="/Customer/ManageCRMRequests.aspx?userid=<%=UserMembershipID %>">Manage CRM Issues</a>
							</li>
						</ul>
				</div>
				<div class="widget-body">
					<div class="widget-main padding-12">
						<div class="tab-content">
							<div id="Manage" class="tab-pane active" runat="server">
                                <asp:MultiView ID="mvCrmIssues" runat="server" ActiveViewIndex="0">
                                    <asp:View ID="vwManageCRM" runat="server">
                                        <div class="row-fluid">
                                        <div class="span12">
                                        <div class="row-fluid">
	                                        <div class="table-header">
		                                        Manage CRM Issues	        
                                            </div>
                                            <div class="dataTables_wrapper">
                                                <div role="grid" class="dataTables_wrapper">
                                                    <table id="tblManageCrm" class="table table-striped table-bordered table-hover dataTable">
                                                        <thead>
                                                        <tr role="row">
                                                            <th role="columnheader">CRM Code</th>
                                                            <th role="columnheader">User Email</th>
                                                            <th role="columnheader">Service</th>
                                                            <th role="columnheader">Issue Type</th>
                                                            <th role="columnheader">Subject</th>
                                                            <th role="columnheader">Status</th>
                                                            
                                                            <th role="columnheader" style="text-align:center;">Options</th>
                                                        </tr>
                                                        </thead>
                                                        <tbody role="alert">
                                                        <asp:Repeater ID="rptrManageCRMIssue" runat="server">
                                                        <ItemTemplate>
                                                        <tr>
                                                            <td>CRM<%#Eval("CRMRequestID") %></td>
                                                            <td><%#Eval("EmailID") %><asp:HiddenField ID="hdnCRMRequestID" runat="server" Value='<%# Eval("CRMRequestID")%>' /></td>
                                                            <td><%# Eval("ServiceName")== null? "-":Eval("ServiceName")%></td>
                                                            <td><%# Eval("CRMIssueTypeDesc")%></td>                                                            
                                                            <td><%# Eval("CRMSubject")%></td>
                                                            <td><%# Eval("CRMRequestStatusDesc") %></td>
                                                            <%--<td><%#Eval("AssignedEmailID")== null?"Not Yet Worked":Eval("AssignedEmailID") %></td>--%>
                                                            <td style="text-align:center;">
                                                                <div class="action-buttons">
                                                                <a data-rel="tooltip" title="View CRM Request" href="/Customer/ManageCRMRequests.aspx?userid=<%=UserMembershipID %>&crmrequestid=<%# Eval("CRMRequestID")%>" class="green">
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
                                            <div class="row-fluid">
                                                <asp:CheckBox ID="ManageShowDeleted" runat="server"
                                        AutoPostBack="true" OnCheckedChanged="ManageShowDeleted_CheckedChanged"/>
                                        <span class="label label-warning arrowed-right">Show All</span>
                                            </div>
                                        </div>
                                        </div>
                                        </div>
                                    </asp:View>
                                    <asp:View ID="vmCRMForm" runat="server">
                                        <h5>
                                        <a href="/Customer/ManageCRMRequests.aspx?userid=<%=UserMembershipID %>">&lt; Back CRM Requests&nbsp;</a>
                                        </h5>
                                        <div class="table-header">
		                            Edit CRM Issues
	                            </div>
                                        <table class="table table-striped table-bordered table-hover dataTable">
                                    <tr>
                                        <td class="span3">CRM Code
                                        </td>
                                        <td><asp:Label ID="lblCurrentCRMID" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td class="span2">Customer Name and Email ID
                                        </td>
                                        <td><asp:Label ID="lblCustomerName" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="trServiceCategory" runat="server">
                                        <td class="span2">Category Name
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblcategoryName" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="trService" runat="server">
                                        <td class="span2">Service Name
                                        </td>
                                        <td>
                                        <asp:Label runat="server" ID="lblServiceName"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="span2">Subject
                                        </td>
                                        <td>
                                        <asp:Label runat="server" ID="lblSubject"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="span2">Issue Description
                                        </td>
                                        <td><asp:TextBox ID="txtIssue" runat="server" TextMode="MultiLine" Height="200px" Enabled="False" Width="90%"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvIssue" runat="server" 
                                            ErrorMessage="Category Name" ControlToValidate="txtIssue" ValidationGroup="CRMIssue"
                                            Text="*" ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td class="span2">Issue Type
                                        </td>
                                        <td>  
                                            <asp:Label ID="lblIssueType" runat="server" ></asp:Label> </td>
                                    </tr>
                                    <tr>
                                                <td>
                                                    Attachment
                                                </td>
                                                <td>
                                                    <asp:HyperLink runat="server" ID="hypAttchmentLink" Target="_blank" >Click Here To View</asp:HyperLink>
                                                    &nbsp;<asp:Label runat="server" ID="lblNoAttachment" Text="No Attachment" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                    <tr runat="server" id="trWorklogNotes">
                                        <td class="span2">Worklog Notes
                                        </td>
                                        <td><asp:TextBox ID="txtAdminNotes" runat="server" TextMode="MultiLine" Height="100px" Width="90%"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvAdminNotes" runat="server" 
                                            ErrorMessage="*" ControlToValidate="txtAdminNotes" ValidationGroup="CRMIssue"
                                            Text="*" ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                            <asp:UpdatePanel runat="server" ID="upnlStatusChanged">
                                                <ContentTemplate>
                                    <tr>
                                        <td class="span2">Status
                                        </td>
                                        <td> <asp:DropDownList ID="ddlCrmStatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCrmStatus_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
<%--                                    <tr runat="server" id="trAssignTo">
                                        <td class="span2">Assign To
                                        </td>
                                        <td>  
                                            <asp:DropDownList ID="ddlAssignAdmin" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAssignAdmin_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlAssignAdmin" ErrorMessage="*" ForeColor="Red" InitialValue="0" Text="*" ValidationGroup="CRMIssue"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>--%>
                                    <tr runat="server" id="trAddAttachment" visible="false">
                                        <td class="span2">Add Attachment</td>
                                        <td>
                                            <asp:FileUpload ID="fluAddAttachments" runat="server" />
                                        </td>
                                    </tr>

                                    <tr runat="server" id="trContentCustomer" visible="false">
                                        <td>
                                           <asp:Label runat="server" ID="lblCustomerContent" Text="CRM Resolution to be sent to Customer"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCustomerMailContent" runat="server" TextMode="MultiLine" Height="200px" Width="90%"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                            ErrorMessage="*" ControlToValidate="txtCustomerMailContent" ValidationGroup="CRMIssue"
                                            Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <%--<tr runat="server" id="trCRMReslnAttachment" visible="false">
                                        <td>
                                           CRM Resolution Attachment
                                        </td>
                                        <td>
                                             <asp:HyperLink runat="server" ID="hypReslonAttachment" Target="_blank" >Click Here To View</asp:HyperLink>
                                             <asp:Label runat="server" ID="lblhypReslonAttachment" Text="No Attachment" Visible="false"></asp:Label>
                                        </td>
                                    </tr>--%>
                                                </ContentTemplate>
                                                <Triggers  >
                                                    <asp:PostBackTrigger ControlID="ddlCrmStatus" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                    <tr runat="server" id="trButtonSave">
                                        <td class="span2">
                                        </td>
                                        <td><asp:Button ID="btnCrmIssueSave" runat="server" Text="Save" class="btn btn-primary" OnClick="btnCrmIssueSave_Click" ValidationGroup="CRMIssue" />
                                              <asp:Button ID="btnReset" runat="server" CssClass="btn btn-warning" Text="Reset" OnClick="btnReset_Click" />
                                            <asp:Button ID="btnChangeissueType" runat="server" CssClass="btn btn-success" Text="Change Issue Technical" OnClick="btnChangeissueType_Click" />
                                        </td>
                                    </tr>
                                </table>
                                        <br />
                                            <div id="lblManageSuccessMessage" runat="server" visible="false" class="alert alert-block alert-success">
							                    <button data-dismiss="alert" class="close" type="button">
								                    <i class="icon-remove"></i>
							                    </button>
							                    <p>
                                                    <i class="icon-ok"></i>
								                    CRM Issue updated successfully.
							                    </p>
						                    </div>
                                            <div id="lblManageErrorMessage" runat="server" visible="false" class="alert alert-error">
						                        <button data-dismiss="alert" class="close" type="button">
							                        <i class="icon-remove"></i>
						                        </button>
                                                <i class="icon-remove"></i>
					                            Error while save CRM Details. Please try again.
                                            </div>
                                        <br />
                                        <div runat="server" id="dvWorkLog" visible="false">
                                        <div class="table-header">
                                            <asp:Label ID="Label3" runat="server" Text="Work Log"></asp:Label>
                                            &nbsp;History</div>
                                        <div class="dataTables_wrapper">
                                                <div role="grid" class="dataTables_wrapper">
                                                    <table id="tableARMWorkLog" class="table table-striped table-bordered table-hover dataTable">
                                                        <thead>
                                                        <tr role="row">
                                                            <th role="columnheader">Worked by</th>
                                                            <th role="columnheader">Worklog Notes</th>
                                                            <th role="columnheader">Resolution Notes</th>
                                                            <th role="columnheader">Resolution Attachment</th>
                                                            <th role="columnheader">Status</th>
                                                            <th role="columnheader">Updated On</th>
                                                        </tr>
                                                        </thead>
                                                        <tbody role="alert">
                                                        <asp:Repeater ID="rptrWorkLogHistory" runat="server">
                                                        <ItemTemplate>
                                                        <tr>
                                                            <td><%# UserMailIDbyGuid(Eval("CreatedBy").ToString()) %></td>
                                                            <td><%# Eval("CRMUpdate")%></td>
                                                            <td><%# Eval("ResolutionSentToCustomer")== null?"-":Eval("ResolutionSentToCustomer") %></td>
                                                            <td>
                                                                <a runat="server" target="_blank" id="attachment" visible='<%# Eval("AttachmentSentToCustomer")== null?false:true%>' href='<%# Eval("AttachmentSentToCustomer")== null?"#":Eval("AttachmentSentToCustomer") %>' >Click Here</a>
                                                                <asp:Label runat="server" ID="lblNoattachment" Text="No Attachment"  Visible='<%# Eval("AttachmentSentToCustomer")== null?true:false%>'></asp:Label>
                                                            </td>

                                                            <td><%# CRMRecordStatus(Eval("CRMStatusChangeTo").ToString())%></td>
                                                            <td><%# Eval("CreatedOn")%></td>
                                                        </tr>
                                                        </ItemTemplate>
                                                        </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                                </div>
                                        </div>
                                        </div>
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
                                                                <a data-rel="tooltip" title="View CRM Request" href="/Customer/ManageCRMRequests.aspx?userid=<%=UserMembershipID %>&crmrequestid=<%#Eval("CRMRequestID")%>&Crmhistoryid=<%#Eval("CRMHistoryID")%>" class="green">
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
                                            <strong>There are no CRM Issues .</strong>
                                        </div>
                                    </asp:View>
                                    <asp:View ID="vwCRMHistoryDetails" runat="server">
                                         <h4><a href='/Customer/ManageCRMRequests.aspx?userid=<%=UserMembershipID %>&crmrequestid=<%= CRMCodeRequestID %>' >&lt; Back </a></h4>
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
                        { 'bSortable': false, 'aTargets': [6] }
                    ]
                    ,"aaSorting": [[ 0, "desc" ]]
                }
         );
        });
   </script>
</asp:Content>

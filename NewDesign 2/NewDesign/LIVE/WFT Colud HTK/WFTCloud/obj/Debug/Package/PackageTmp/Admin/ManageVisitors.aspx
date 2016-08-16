<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminPages.Master" AutoEventWireup="true" CodeBehind="ManageVisitors.aspx.cs" Inherits="WFTCloud.Admin.ManageVisitors" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Manage Visitors Details</title>
    <meta name="Description" content="Details of the Visitors are managed here." />
    <meta name="Keywords" content="WFT Manage Visitors Details " />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="mvContainer" runat="server" ActiveViewIndex="0">
        <asp:View ID="MVDView" runat="server">
            <div class="row-fluid">
                <div class="span12">
                    <div class="row-fluid">
                        <div id="divMVDSuccessMessage" runat="server" visible="false" class="alert alert-block alert-success span6">
                            <button data-dismiss="alert" class="close" type="button">
                                <i class="icon-remove"></i>
                            </button>
                            <p>
                                <i class="icon-ok"></i>
                                Visitors Information Status updated successfully.
                            </p>
                        </div>
                        <div id="divMVDErrorMessage" runat="server" visible="false" class="alert alert-block alert-error span6">
                            <button data-dismiss="alert" class="close" type="button">
                                <i class="icon-remove"></i>
                            </button>
                            <p>
                                <i class="icon-ok"></i>
                                Please Enter the Valid Visitor Details.
                            </p>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="table-header">
                            Manage Visitor Details        
                        </div>
                    </div>
                    <div role="grid" class="dataTables_wrapper">
                        <table id="tblManageVisitors" class="table table-striped table-bordered table-hover dataTable">
                            <thead>
                                <tr role="row">
                                    <th class="center" role="columnheader">
                                        <label>
                                            <input type="checkbox" class="ace" />
                                            <span class="lbl"></span>
                                        </label>
                                    </th>
                                    <th role="columnheader" class="center">First Name</th>
                                    <th role="columnheader" class="center">Last Name</th>
                                    <th role="columnheader" class="center">Email ID</th>
                                    <th role="columnheader" class="center">Phone Number</th>
                                    <th role="columnheader" class="center">Trainee</th>
<%--                                    <th role="columnheader" class="center">Company Name & Address</th>
                                    <th role="columnheader" class="center">To Meet</th>
                                    <th role="columnheader" class="center">Purpose</th>
                                    <th role="columnheader" class="center">Time In</th>
                                    <th role="columnheader" class="center">Time Out</th>--%>
                                    <th role="columnheader" class="center">Org NewsLetter</th>
                                    <th role="columnheader" class="center" style="text-align:center; width:8%">Options</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptrVisitorsDetails" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="center">
                                                <asp:CheckBox ID="chkSelect" runat="server" class="ace" />
                                            </td>
                                            <td><%# Eval("FirstName")%><asp:HiddenField ID="hdnVisitorDetail" runat="server" Value='<%# Eval("VisitorID")%>' /></td>
                                            <td><%# Eval("LastName")%></td>
                                            <td><%# Eval("Email")%></td>
                                            <td><%# Eval("PhoneNumber")%></td>
                                            <td><%# Eval("TrainnerID") != null ?"Yes": "No" %></td>
<%--                                            <td><%# Eval("CompanyNameAddress")%></td>
                                            <td><%# Eval("ToMeet")%></td>
                                            <td><%# Eval("Purpose")%></td>
                                            <td><%# Eval("TimeIn")%></td>
                                            <td><%# Eval("TimeOut")%></td>--%>
                                             <td><%# Eval("Status").ToString() == "1" ?"Yes": "No" %></td>
                                            <td>
                                                <div class="action-buttons">
                                                    <a data-rel="tooltip" title="Edit Visitor Details" href='ManageVisitors.aspx?editvisitordetails=<%# Eval("VisitorID")%>' class="green">
                                                        <i class="icon-pencil bigger-130"></i>
                                                    </a>
                                                   <%-- <a data-rel="tooltip" id="Activate/DeActivate" title="Activate/Deactivate" href='ManageVisitors.aspx?<%# WFTCloud.GeneralReusableMethods.GetActivateDeactivateAction(Eval("Status").ToString()) %>=<%# Eval("VisitorID")%>' class="blue">
                                                        <i class='<%# WFTCloud.GeneralReusableMethods.GetActivateDeactivateIcon(Eval("Status").ToString()) %>'></i>
                                                    </a>
                                                   --%>
                                                    
                                                    <a data-rel="tooltip" id="A1"  title="Add to Trainee" target="_blank" href='/Admin/ManageTrainingDetails.aspx?VisitorID=<%# Eval("VisitorID")%>' style='<%# Eval("TrainnerID") != null ?"display:none;" : "" %>'  class="blue">
                                                        <i class='icon-file-alt'></i>
                                                    </a>

                                                    <%--<a data-rel="tooltip" title="Delete Visitor Details" href="ManageVisitors.aspx?delete=<%# Eval("VisitorID")%>" class="red">
                                                        <i class="icon-trash bigger-150"></i>
                                                    </a>--%>
                                                </div>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                    <div class="row-fluid">
                            <asp:Button ID="btnNewVisitorDetail" runat="server" Text="Add New" class="btn btn-primary" OnClick="btnNewVisitorDetail_Click"/>
                       <%-- <asp:Button ID="btnActivate" runat="server" Text="Activate" class="btn btn-info" OnClick="btnActivate_Click"/>
                        <asp:Button ID="btnDeactivate" runat="server" Text="Deactivate" class="btn btn-warning" OnClick="btnDeactivate_Click" />
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" class="btn btn-danger" OnClick="btnDelete_Click" />
                        <asp:CheckBox ID="chkShowDeActivated" runat="server" AutoPostBack="True" OnCheckedChanged="chkShowDeActivated_CheckedChanged" />
                        <span class="label label-warning arrowed-right">Show De-Activated</span>
                           --%>
                    </div>
                </div>
            </div>
        </asp:View>
        <asp:View ID="MVDEdit" runat="server">
            <div class="row-fluid">
                <div class="span12">
             <div class="row-fluid">
                 <a href="ManageVisitors.aspx">&lt;&lt; Back&nbsp;</a>
                 <div class="row-fluid">
                       <div id="lblSuccessMessage" runat="server" visible="false" class="alert alert-block alert-success span6">
							<button data-dismiss="alert" class="close" type="button">
								<i class="icon-remove"></i>
							</button>
							<p>
                                <i class="icon-ok"></i>
								Visitors details updated successfully.
							</p>
						</div>
                             <div id="lblErrorMessage" runat="server" visible="false" class="alert alert-error span6">
						    <button data-dismiss="alert" class="close" type="button">
							    <i class="icon-remove"></i>
						    </button>
                            <i class="icon-remove"></i>
                            <asp:Label ID="lblVisitorDetailsError" runat="server"></asp:Label>
                        </div>
                 </div>
                 <div class="table-header">
                     Edit Visitors Details
                 </div>
                 <table class="table table-hover dataTable table-bordered ">
                    <tr>
                        <td class="span2">First Name</td>
                        <td>
                            <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ErrorMessage="*" ControlToValidate="txtFirstName" ForeColor="Red" ValidationGroup="Visitors"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Last name</td>
                        <td>
                            <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ErrorMessage="*" ControlToValidate="txtLastName" ForeColor="Red" ValidationGroup="Visitors"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Email ID</td>
                        <td>
                            <asp:TextBox ID="txtEmailID" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtEmailID" ForeColor="Red" ValidationGroup="Visitors"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="*" ControlToValidate="txtEmailID" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Visitors"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Phone Number</td>
                        <td>
                            <asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPhoneNumber" runat="server" ControlToValidate="txtPhoneNumber" ErrorMessage="*" ForeColor="Red" ValidationGroup="Visitors"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                      <tr>
                        <td class="span2">Visitor Image</td>
                        <td>
                            <asp:FileUpload ID="fluVisitorImage" runat="server" />
                            <br />
                            <asp:Image runat="server" ID="imgVisitorImage" Width="100px" Height="100px" />
                        </td>
                    </tr>
<%--                    <tr>
                        <td class="span2">Company Name & Address</td>
                        <td>
                            <asp:TextBox ID="txtCompanyNameAddress" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCompanyNameAddress" runat="server" ErrorMessage="*" ControlToValidate="txtCompanyNameAddress" ForeColor="Red" ValidationGroup="Visitors"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">To Meet</td>
                        <td>
                            <asp:TextBox ID="txtToMeet" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvToMeet" runat="server" ControlToValidate="txtToMeet" ErrorMessage="*" ForeColor="Red" ValidationGroup="Visitors"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Purpose</td>
                        <td>
                            <asp:TextBox ID="txtPurpose" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPurpose" runat="server" ErrorMessage="*" ControlToValidate="txtPurpose" ForeColor="Red" ValidationGroup="Visitors"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Time In</td>
                        <td>
                            <asp:TextBox ID="txtTimeIn" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvTimeIn" runat="server" ErrorMessage="*" ControlToValidate="txtTimeIn" ForeColor="Red" ValidationGroup="Visitors"></asp:RequiredFieldValidator>
                            <asp:CalendarExtender ID="ceEditTimeIN" runat="server" Enabled="true" TargetControlID="txtTimeIn" Format="dd'-'MMM'-'yyyy HH':'mm':'ss">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Time Out</td>
                        <td>
                            <asp:TextBox ID="txtTimeOut" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvTimeOut" runat="server" ErrorMessage="*" ControlToValidate="txtTimeOut" ForeColor="Red" ValidationGroup="Visitors"></asp:RequiredFieldValidator>
                             <asp:CalendarExtender ID="ceEditTimeOut" runat="server" Enabled="true" TargetControlID="txtTimeOut" Format="dd'-'MMM'-'yyyy HH':'mm':'ss">
                            </asp:CalendarExtender>
                        </td>
                    </tr>--%>
                    <tr>
                        <td class="span2">Org News Letter</td>
                        <td>
                            <asp:DropDownList ID="ddlStatus" runat="server">
                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2"></td>
                        <td>
                            <asp:Button ID="btnSave" Text="Save" runat="server" class="btn btn-primary" BackColor="#3459FC" ValidationGroup="Visitors" OnClick="btnSave_Click"/>
                            <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-warning" OnClick ="btnReset_Click" />
                           
                        </td>
                    </tr>
                </table>
             </div>
                    </div>
                </div>
         </asp:View>
        <asp:View runat="server" ID="NewMVD">
            <div class="row-fluid">
                <div class="span12">
             <div class="row-fluid">
                 <a href="ManageVisitors.aspx">&lt;&lt; Back&nbsp;</a>
                 <div class="row-fluid">
                      <div id="divNewSuccess" runat="server" visible="false" class="alert alert-block alert-success span6">
							<button data-dismiss="alert" class="close" type="button">
								<i class="icon-remove"></i>
							</button>
							<p>
                                <i class="icon-ok"></i>
                                Visitor details added successfully.
							</p>
						</div>
                             <div id="divNewError" runat="server" visible="false" class="alert alert-error span6">
						    <button data-dismiss="alert" class="close" type="button">
							    <i class="icon-remove"></i>
						    </button>
                            <i class="icon-remove"></i>
                            <asp:Label ID="lblNewError" runat="server"></asp:Label>
                        </div>
                 </div>
                 <div class="table-header">
                     New Visitors Details
                 </div>
                 <table class="table table-hover dataTable table-bordered ">
                    <tr>
                        <td class="span4">First Name</td>
                        <td>
                            <asp:TextBox ID="txtNewFirstName" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNewFirstName" runat="server" ErrorMessage="*" ControlToValidate="txtNewFirstName" ForeColor="Red" ValidationGroup="NewVisitors"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Last name</td>
                        <td>
                            <asp:TextBox ID="txtNewLastName" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNewLastName" runat="server" ErrorMessage="*" ControlToValidate="txtNewLastName" ForeColor="Red" ValidationGroup="NewVisitors"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Email ID</td>
                        <td>
                            <asp:TextBox ID="txtNewEmailID" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtNewEmailID" ForeColor="Red" ValidationGroup="NewVisitors"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="rfvNewEmailID" runat="server" ErrorMessage="*" ControlToValidate="txtNewEmailID" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="NewVisitors"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Phone Number</td>
                        <td>
                            <asp:TextBox ID="txtNewPhoneNumber" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNewPhoneNumber" runat="server" ControlToValidate="txtNewPhoneNumber" ErrorMessage="*" ForeColor="Red" ValidationGroup="NewVisitors"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                      <tr>
                        <td class="span2">Visitor Image</td>
                        <td>
                            <asp:FileUpload ID="fluNewVisitorImage" runat="server" />
                        </td>
                    </tr>
                    <%--<tr>
                        <td class="span2">Company Name & Address</td>
                        <td>
                            <asp:TextBox ID="txtNewCompanyNameAddress" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNewCompanyNameAddress" runat="server" ErrorMessage="*" ControlToValidate="txtNewCompanyNameAddress" ForeColor="Red" ValidationGroup="NewVisitors"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">To Meet</td>
                        <td>
                            <asp:TextBox ID="txtNewToMeet" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNewToMeet" runat="server" ControlToValidate="txtNewToMeet" ErrorMessage="*" ForeColor="Red" ValidationGroup="NewVisitors"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Purpose</td>
                        <td>
                            <asp:TextBox ID="txtNewPurpose" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNewPurpose" runat="server" ErrorMessage="*" ControlToValidate="txtNewPurpose" ForeColor="Red" ValidationGroup="NewVisitors"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Time In</td>
                        <td>
                            <asp:TextBox ID="txtNewTimeIN" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNewTimeIN" runat="server" ErrorMessage="*" ControlToValidate="txtNewTimeIN" ForeColor="Red" ValidationGroup="NewVisitors"></asp:RequiredFieldValidator>
                            <asp:CalendarExtender ID="ceTimeIN" runat="server" Enabled="true" TargetControlID="txtNewTimeIN" Format="dd'-'MMM'-'yyyy HH':'mm':'ss">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                       <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                    <tr>
                        <td class="span2">Time Out</td>
                        <td>
                            <asp:TextBox ID="txtNewTimeOut" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNewTimeOut" runat="server" ErrorMessage="*" ControlToValidate="txtNewTimeOut" ForeColor="Red" ValidationGroup="NewVisitors"></asp:RequiredFieldValidator>
                             <asp:CalendarExtender ID="ceTimeOUT" runat="server" TargetControlID="txtNewTimeOut" Format="dd'-'MMM'-'yyyy HH':'mm':'ss">
                            </asp:CalendarExtender>
                        </td>
                    </tr>--%>
                    <tr>
                        <td class="span2">Org News Letter</td>
                        <td>
                            <asp:DropDownList ID="ddlNewStatus" runat="server">
                                 <asp:ListItem Selected="True" Text="Yes" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2"></td>
                        <td>
                            <asp:Button ID="btnNewVisitorSave" Text="Save" runat="server" class="btn btn-primary" BackColor="#3459FC" ValidationGroup="NewVisitors" OnClick="btnNewVisitorSave_Click"/>
                            
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
            $('#tblManageVisitors').dataTable(
                 {
                     "aoColumnDefs": [
                         { 'bSortable': false, 'aTargets': [0, 7] }
                     ]
                 }
         );
        });
   </script>
</asp:Content>

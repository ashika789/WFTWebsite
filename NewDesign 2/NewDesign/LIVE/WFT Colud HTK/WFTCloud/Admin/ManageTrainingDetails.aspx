<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminPages.Master" AutoEventWireup="true" CodeBehind="ManageTrainingDetails.aspx.cs" Inherits="WFTCloud.Admin.ManageTrainingDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Manage Training Details</title>
    <meta name="Description" content="Training Details of the Visitors are managed here." />
    <meta name="Keywords" content="WFT Manage Training Details " />
    <style type="text/css">
        .auto-style1
        {
            width: 104px;
            height: 57px;
        }
        .auto-style2
        {
            height: 57px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="mvContainer" runat="server" ActiveViewIndex="0">
        <asp:View ID="MTDView" runat="server">
            <div class="row-fluid">
                <div class="span12">
                    <div class="row-fluid">
                        <div id="divMTDSuccessMsg" runat="server" visible="false" class="alert alert-block alert-success span6">
                            <button data-dismiss="alert" class="close" type="button">
                                <i class="icon-remove"></i>
                            </button>
                            <p>
                                <i class="icon-ok"></i>
                                 Training status updated successfully.
                            </p>
                        </div>
                        <div id="divMTDErrorMsg" runat="server" visible="false" class="alert alert-block alert-error span6">
                            <button data-dismiss="alert" class="close" type="button">
                                <i class="icon-remove"></i>
                            </button>
                            <p>
                                <i class="icon-ok"></i>
                                Enter the Valid Training Details.
                            </p>
                        </div>
                    </div>
                        <div class="row-fluid">
                            <div class="table-header">
                                Manage Training Details        
                            </div>
                        </div>
                        <div role="grid" class="dataTables_wrapper">
                            <table id="tblManageTrainingDetails" class="table table-striped table-bordered table-hover dataTable">
                                <thead>
                                    <tr role="row">
                                        <th class="center" role="columnheader">
                                            <label>
                                                <input type="checkbox" class="ace" />
                                                <span class="lbl"></span>
                                            </label>
                                        </th>
                                        <th role="columnheader" class="center">Name</th>
                                        <th role="columnheader" class="center">Email ID</th>
                                        <th role="columnheader" class="center">Phone Number</th>
                                        <th role="columnheader" class="center">Course Of Interest</th>
                                        <th role="columnheader" class="center">Status</th>
                                        <th role="columnheader" class="center" style="text-align:center;">Options</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rptrTrainingDetails" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                 <td class="center">
                                                    <asp:CheckBox ID="chkSelect" runat="server" class="ace" />
                                                </td>
                                                <td><%# Eval("FirstName")%> <%# Eval("LastName")%><asp:HiddenField ID="hdnTrainingDetail" runat="server" Value='<%# Eval("TrainnerID")%>' /></td>
                                                <td><a href='<%# Eval("Email")%>'>
                                                    <%# Eval("Email")%>
                                                </a></td>
                                                <td><%# Eval("PhoneNumber")%></td>
                                               <td><%# WFTCloud.GeneralReusableMethods.GetCourseName(Eval("CourseOfInterest").ToString()) %></td>
                                                <td><%# WFTCloud.GeneralReusableMethods.GetStatusString(Eval("Status").ToString()) %></td>
                                                <td>
                                                    <div class="action-buttons">
                                                        <a data-rel="tooltip" title="Edit Training Details" href='ManageTrainingDetails.aspx?edittrainingdetailid=<%# Eval("TrainnerID")%>' class="green">
                                                            <i class="icon-pencil bigger-130"></i>
                                                        </a>
                                                        <a data-rel="tooltip" title="Activate/Deactivate" href='ManageTrainingDetails.aspx?<%# WFTCloud.GeneralReusableMethods.GetActivateDeactivateAction(Eval("Status").ToString()) %>=<%# Eval("TrainnerID")%>' class="blue">
                                                            <i class='<%# WFTCloud.GeneralReusableMethods.GetActivateDeactivateIcon(Eval("Status").ToString()) %>'></i>
                                                        </a>
                                                        <%--<a data-rel="tooltip" title="Delete Training Details" href='ManageTrainingDetails.aspx?delete=<%# Eval("TrainnerID")%>' class="red">
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
                    <div class="row-fluid">
                            <asp:Button ID="btnNewMTD" runat="server" Text="Add New" class="btn btn-primary" OnClick="btnNewMTD_Click"/>
                        <asp:Button ID="btnActivate" runat="server" Text="Activate" class="btn btn-info" OnClick="btnActivate_Click"/>
                        <asp:Button ID="btnDeactivate" runat="server" Text="Deactivate" class="btn btn-warning" OnClick="btnDeactivate_Click"/>
<%--                        <asp:Button ID="btnDelete" runat="server" Text="Delete" class="btn btn-danger" OnClick="btnDelete_Click"/>
                        --%>
                        <asp:CheckBox ID="chkShowDeActivated" runat="server" AutoPostBack="True"  OnCheckedChanged="chkShowDeActivated_CheckedChanged"/>
                            <span class="label label-warning arrowed-right">Show De-Activated</span>
                    </div>
                </div>
            </div>
        </asp:View>
        <asp:View ID="MTDEdit" runat="server">
            <div class="row-fluid">
                <div class="span12">
            <div class="row-fluid">
                <a href="ManageTrainingDetails.aspx">&lt;&lt; Back&nbsp;</a>
                <div class="row-fluid">
                      <div id="lblSuccessMessage" runat="server" visible="false" class="alert alert-block alert-success span6">
							<button data-dismiss="alert" class="close" type="button">
								<i class="icon-remove"></i>
							</button>
							<p>
                                <i class="icon-ok"></i>
								Training details updated successfully.
							</p>
						</div>
                             <div id="lblErrorMessage" runat="server" visible="false" class="alert alert-error span6">
						    <button data-dismiss="alert" class="close" type="button">
							    <i class="icon-remove"></i>
						    </button>
                            <i class="icon-remove"></i>
                            <asp:Label ID="lblTrainingDetailsError" runat="server"></asp:Label>
                        </div>
                </div>
                <div class="table-header">
                    Edit Training Details
                </div>
                <table class="table table-hover dataTable table-bordered ">
                    <tr>
                        <td class="span4">First Name</td>
                        <td>
                            <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ErrorMessage="*" ControlToValidate="txtFirstName" ForeColor="Red" ValidationGroup="Training"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Last name</td>
                        <td>
                            <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ErrorMessage="*" ControlToValidate="txtLastName" ForeColor="Red" ValidationGroup="Training"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Email ID</td>
                        <td>
                            <asp:TextBox ID="txtEmailID" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtEmailID" ForeColor="Red" ValidationGroup="Training"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="*" ControlToValidate="txtEmailID" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Training"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Phone Number</td>
                        <td>
                            <asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtPhoneNumber" ForeColor="Red" ValidationGroup="Training"></asp:RequiredFieldValidator>
                            <%--<asp:RegularExpressionValidator ID="revPhoneNumber" runat="server" ControlToValidate="txtPhoneNumber" ErrorMessage="*" ForeColor="Red" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}" ValidationGroup="Training"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Permanent Address</td>
                        <td>
                            <asp:TextBox ID="txtPermanentAddress" runat="server" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPermanentAddress" runat="server" ErrorMessage="*" ControlToValidate="txtPermanentAddress" ForeColor="Red" ValidationGroup="Training"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Temporary Address</td>
                        <td>
                            <asp:TextBox ID="txtTemporaryAddress" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td class="span2">Trainner Image</td>
                        <td>
                            <asp:FileUpload ID="fluTraineeImage" runat="server" />
                            <br />
                            <asp:Image runat="server" ID="imgTraineeImg" Width="100px" Height="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Course Of Interest</td>
                        <td>
                            <asp:DropDownList ID="ddlCourseOfInterest" runat="server">
                                <asp:ListItem>Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">How Did You Know About Us</td>
                        <td>
                            <asp:DropDownList ID="ddlKnowAboutUs" runat="server">
                                <asp:ListItem>Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Others</td>
                        <td>
                            <asp:TextBox ID="txtOthers" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">UG College</td>
                        <td>
                            <asp:TextBox ID="txtUGCollege" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUGCollege" runat="server" ErrorMessage="*" ControlToValidate="txtUGCollege" ForeColor="Red" ValidationGroup="Training"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">UG Percentage</td>
                        <td>
                            <asp:TextBox ID="txtUGPercentage" runat="server" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUGPercentage" runat="server" ErrorMessage="*" ControlToValidate="txtUGPercentage" ForeColor="Red" ValidationGroup="Training"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">PG College</td>
                        <td>
                            <asp:TextBox ID="txtPGCollege" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPGCollege" runat="server" ErrorMessage="*" ControlToValidate="txtPGCollege" ForeColor="Red" ValidationGroup="Training"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">PG Percentage</td>
                        <td>
                            <asp:TextBox ID="txtPGPercentage" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPGPercentage" runat="server" ErrorMessage="*" ControlToValidate="txtPGPercentage" ForeColor="Red" ValidationGroup="Training"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Other Qualification</td>
                        <td>
                            <asp:TextBox ID="txtOQCollege" runat="server"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="rfvOQCollege" runat="server" ErrorMessage="*" ControlToValidate="txtOQCollege" ForeColor="Red" ValidationGroup="Training"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Other Qualification Percentage</td>
                        <td>
                            <asp:TextBox ID="txtOQPercentage" runat="server"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="rfvOQPercentage" runat="server" ErrorMessage="*" ControlToValidate="txtOQPercentage" ForeColor="Red" ValidationGroup="Training"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Years Of Experience</td>
                        <td>
                            <asp:TextBox ID="txtYearOfExperience" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvYearOfExperience" runat="server" ErrorMessage="*" ControlToValidate="txtYearOfExperience" ForeColor="Red" ValidationGroup="Training"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Current Company</td>
                        <td>
                            <asp:TextBox ID="txtCurrentCompany" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCurrentCompany" runat="server" ErrorMessage="*" ControlToValidate="txtCurrentCompany" ForeColor="Red" ValidationGroup="Training"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Technology Currently Worked</td>
                        <td>
                            <asp:TextBox ID="txtTechnology" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">Status</td>
                        <td class="auto-style2">
                            <asp:DropDownList ID="ddlStatus" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2"></td>
                        <td>
                            <asp:Button ID="btnSave" Text="Save" runat="server" class="btn btn-primary" BackColor="#3459FC" ValidationGroup="Training" OnClick="btnSave_Click"/>
                            <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-warning" OnClick ="btnReset_Click"/>
                           
                        </td>
                    </tr>
                </table>
            </div>
                    </div>
                </div>
        </asp:View> 
        <asp:View runat="server" ID="NewMTD">
            <div class="row-fluid">
                <div class="span12">
            <div class="row-fluid">
                <a href="ManageTrainingDetails.aspx">&lt;&lt; Back&nbsp;</a>
                <div class="row-fluid">
                     <div id="divNewSuccess" runat="server" visible="false" class="alert alert-block alert-success span6">
							<button data-dismiss="alert" class="close" type="button">
								<i class="icon-remove"></i>
							</button>
							<p>
                                <i class="icon-ok"></i>
								Training details added successfully.
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
                    New Training Details
                </div>
                <table class="table table-hover dataTable table-bordered ">
                    <tr>
                        <td class="span4">First Name</td>
                        <td>
                            <asp:TextBox ID="txtNewFirstName" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNewFirstName" runat="server" ErrorMessage="*" ControlToValidate="txtNewFirstName" ForeColor="Red" ValidationGroup="NewTraining"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Last name</td>
                        <td>
                            <asp:TextBox ID="txtNewLastName" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNewLastName" runat="server" ErrorMessage="*" ControlToValidate="txtNewLastName" ForeColor="Red" ValidationGroup="NewTraining"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Email ID</td>
                        <td>
                            <asp:TextBox ID="txtNewEmailID" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvnewEmail" runat="server" ErrorMessage="*" ControlToValidate="txtNewEmailID" ValidationGroup="NewTraining" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="rfvNewEmailID" runat="server" ErrorMessage="Enter valid Email" ControlToValidate="txtNewEmailID" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="NewTraining"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Phone Number</td>
                        <td>
                            <asp:TextBox ID="txtNewPhoneNumber" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="txtNewPhoneNumber" ValidationGroup="NewTraining" ForeColor="Red"></asp:RequiredFieldValidator>
                            <%--<asp:RegularExpressionValidator ID="revNewPhoneNumber" runat="server" ControlToValidate="txtNewPhoneNumber" ErrorMessage="*" ForeColor="Red" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}" ValidationGroup="NewTraining"></asp:RegularExpressionValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Permanent Address</td>
                        <td>
                            <asp:TextBox ID="txtNewPermanentAddress" runat="server" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNewPermanentAddress" runat="server" ErrorMessage="*" ControlToValidate="txtNewPermanentAddress" ForeColor="Red" ValidationGroup="NewTraining"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Temporary Address</td>
                        <td>
                            <asp:TextBox ID="txtNewTemporary" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td class="span2">Trainee Image</td>
                        <td>
                            <asp:FileUpload ID="fluNewTraineeImage" runat="server" />
                            <br />
                            <asp:Image runat="server" ID="imgNewTraineeImage" Width="100px" Height="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Course Of Interest</td>
                        <td>
                            <asp:DropDownList ID="ddlNewCourseOfInterest" runat="server">
                                <asp:ListItem>Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">How Did You Know About Us</td>
                        <td>
                            <asp:DropDownList ID="ddlNewHearAbout" runat="server">
                                <asp:ListItem>Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Others</td>
                        <td>
                            <asp:TextBox ID="txtNewOthers" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">UG College</td>
                        <td>
                            <asp:TextBox ID="txtNewUGCollege" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNewUGCollege" runat="server" ErrorMessage="*" ControlToValidate="txtNewUGCollege" ForeColor="Red" ValidationGroup="NewTraining"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">UG Percentage</td>
                        <td>
                            <asp:TextBox ID="txtNewUGPerc" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNewUGPerc" runat="server" ErrorMessage="*" ControlToValidate="txtNewUGPerc" ForeColor="Red" ValidationGroup="NewTraining"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">PG College</td>
                        <td>
                            <asp:TextBox ID="txtNewPGCollege" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNewPGCollege" runat="server" ErrorMessage="*" ControlToValidate="txtNewPGCollege" ForeColor="Red" ValidationGroup="NewTraining"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">PG Percentage</td>
                        <td>
                            <asp:TextBox ID="txtNewPGPerc" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNewPGPerc" runat="server" ErrorMessage="*" ControlToValidate="txtNewPGPerc" ForeColor="Red" ValidationGroup="NewTraining"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Other Qualification </td>
                        <td>
                            <asp:TextBox ID="txtNewOQCollege" runat="server"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="rfvNewOQCollege" runat="server" ErrorMessage="*" ControlToValidate="txtNewOQCollege" ForeColor="Red" ValidationGroup="NewTraining"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Other Qualification Percentage</td>
                        <td>
                            <asp:TextBox ID="txtNewOQPerc" runat="server"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="rfvNewOQPerc" runat="server" ErrorMessage="*" ControlToValidate="txtNewOQPerc" ForeColor="Red" ValidationGroup="NewTraining"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Years Of Experience</td>
                        <td>
                            <asp:TextBox ID="txtNewYearOfExperience" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNewYearOfExperience" runat="server" ErrorMessage="*" ControlToValidate="txtNewYearOfExperience" ForeColor="Red" ValidationGroup="NewTraining"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Current Company</td>
                        <td>
                            <asp:TextBox ID="txtNewCurrentCompany" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNewCurrentCompany" runat="server" ErrorMessage="*" ControlToValidate="txtNewCurrentCompany" ForeColor="Red" ValidationGroup="NewTraining"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Technology Currently Worked</td>
                        <td>
                            <asp:TextBox ID="txtTechCurrentlyWork" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">Status</td>
                        <td class="auto-style2">
                            <asp:DropDownList ID="ddlNewStatus" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2"></td>
                        <td>
                            <asp:Button ID="btnNewSave" Text="Save" runat="server" class="btn btn-primary" BackColor="#3459FC" ValidationGroup="NewTraining" OnClick="btnNewSave_Click"/>
                            
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
        $('#tblManageTrainingDetails').dataTable(
            {
                "aoColumnDefs": [
                    { 'bSortable': false, 'aTargets': [0, 6] }
                ]
            }
     );
    });
   </script>
</asp:Content>

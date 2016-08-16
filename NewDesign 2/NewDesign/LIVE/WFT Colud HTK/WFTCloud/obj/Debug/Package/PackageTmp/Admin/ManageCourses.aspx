<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminPages.Master" AutoEventWireup="true" CodeBehind="ManageCourses.aspx.cs" Inherits="WFTCloud.Admin.ManageCourses" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Manage Course Details</title>
    <meta name="Description" content="Course Details of the Visitors are managed here." />
    <meta name="Keywords" content="WFT Manage Course Details " />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="mvContainer" runat="server" ActiveViewIndex="0">
        <asp:View ID="MCDView" runat="server">
            <div class="row-fluid">
                <div class="span12">
                    <div class="row-fluid">
                        <div id="divMCDSuccessMessage" runat="server" visible="false" class="alert alert-block alert-success span6">
                            <button data-dismiss="alert" class="close" type="button">
                                <i class="icon-remove"></i>
                            </button>
                            <p>
                                <i class="icon-ok"></i>
                                Course information status updated successfully.
                            </p>
                        </div>
                        <div id="divMCDErrorMessage" runat="server" visible="false" class="alert alert-block alert-error span6">
                            <button data-dismiss="alert" class="close" type="button">
                                <i class="icon-remove"></i>
                            </button>
                            <p>
                                <i class="icon-ok"></i>
                                 Enter the Valid Course Details.
                            </p>
                        </div>
                        </div>
                        <div class="row-fluid">
                            <div class="table-header">
                                Manage Course Details        
                            </div>
                        </div>
                        <div role="grid" class="dataTables_wrapper">
                            <table id="tblManageCourses" class="table table-striped table-bordered table-hover dataTable">
                                <thead>
                                    <tr role="row">
                                        <th class="center" role="columnheader" style="width:5%">
                                            <label>
                                                <input type="checkbox" class="ace" />
                                                <span class="lbl"></span>
                                            </label>
                                        </th>
                                        <th role="columnheader" class="center" style="width:10%">Course Name</th>
                                        <th role="columnheader" class="center" style="width:40%">Description</th>
                                        <th role="columnheader" class="center" style="width:12%">Course Duration</th>
                                        <th role="columnheader" class="center" style="width:13%">Opportunities</th>
                                        <th role="columnheader" class="center" style="width:10%">Status</th>
                                        <th role="columnheader" class="center" style="text-align:center; width:10%">Options</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rptrCourseDetails" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="center">
                                                    <asp:CheckBox ID="chkSelect" runat="server" class="ace" />
                                                </td>
                                                <td><%# Eval("CourseName")%><asp:HiddenField ID="hdnCourseDetailId" runat="server" Value='<%# Eval("CourseID")%>' /></td>
                                                <td><%# Eval("Description")%></td>
                                                <td><%# Eval("CourseDuration")%></td>
                                                <td><%# Eval("Opportunities")%></td>
                                                <td><%# WFTCloud.GeneralReusableMethods.GetStatusString(Eval("Status").ToString()) %></td>
                                                <td>
                                                    <div class="action-buttons">
                                                        <a data-rel="tooltip" title="Edit Course Details" href='ManageCourses.aspx?editcoursedetailid=<%# Eval("CourseID")%>' class="green">
                                                            <i class="icon-pencil bigger-130"></i>
                                                        </a>
                                                         <a data-rel="tooltip" title="Activate/Deactivate" href='ManageCourses.aspx?<%# WFTCloud.GeneralReusableMethods.GetActivateDeactivateAction(Eval("Status").ToString()) %>=<%# Eval("CourseID")%>' class="blue">
                                                            <i class='<%# WFTCloud.GeneralReusableMethods.GetActivateDeactivateIcon(Eval("Status").ToString()) %>'></i>
                                                        </a>
                                                      <%--  <a data-rel="tooltip" title="Delete Course Details" href='ManageCourses.aspx?delete=<%# Eval("CourseID")%>' class="red">
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
                             <asp:Button ID="btnAddNewCourseDetails" runat="server" Text="Add New Course" class="btn btn-primary"   OnClick="btnAddNewCourseDetails_Click" />
                            <asp:Button ID="btnActivate" runat="server" Text="Activate" class="btn btn-info" OnClick="btnActivate_Click"/>
                            <asp:Button ID="btnDeactivate" runat="server" Text="Deactivate" class="btn btn-warning" OnClick="btnDeactivate_Click" />
                          <%--  <asp:Button ID="btnDelete" runat="server" Text="Delete" class="btn btn-danger" OnClick="btnDelete_Click"/>
                         --%>   <asp:CheckBox ID="chkShowDeActivated" runat="server" AutoPostBack="True" OnCheckedChanged="chkShowDeActivated_CheckedChanged"/>
                            <span class="label label-warning arrowed-right">Show De-Activated</span>
                        </div>
                </div>
            </div>
        </asp:View>
        <asp:View ID="MCDEdit" runat="server">
            <div class="row-fluid">
                <div class="span12">
             <div class="row-fluid">
                 <a href="ManageCourses.aspx">&lt;&lt; Back&nbsp;</a>
                 <div class="row-fluid">
                      <div id="lblSuccessMessage" runat="server" visible="false" class="alert alert-block alert-success span6">
							<button data-dismiss="alert" class="close" type="button">
								<i class="icon-remove"></i>
							</button>
							<p>
                                <i class="icon-ok"></i>
                                New Course details added successfully.
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
                     Edit Course Details
                 </div>
                 <table class="table table-hover dataTable table-bordered ">
                     <tr>
                         <td class="span2">Course Name</td>
                         <td>
                             <asp:TextBox ID="txtCourseName" runat="server"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="rfvCourseName" runat="server" ErrorMessage="*" ControlToValidate="txtCourseName" ForeColor="Red" ValidationGroup="Courses"></asp:RequiredFieldValidator>
                         </td>
                     </tr>
                     <tr>
                         <td class="span2">Description</td>
                         <td>
                             <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ErrorMessage="*" ControlToValidate="txtDescription" ForeColor="Red" ValidationGroup="Courses"></asp:RequiredFieldValidator>
                         </td>
                     </tr>
                     <tr>
                         <td class="span2">Course Duration</td>
                         <td>
                             <asp:TextBox ID="txtCourseDuration" runat="server"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="rfvCourseDuration" runat="server" ControlToValidate="txtCourseDuration" ErrorMessage="*" ForeColor="Red" ValidationGroup="Courses"></asp:RequiredFieldValidator>
                         </td>
                     </tr>
                     <tr>
                         <td class="span2">Opportunities</td>
                         <td>
                             <asp:TextBox ID="txtOpportunities" runat="server" TextMode="MultiLine"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="rfvOpportunities" ControlToValidate="txtOpportunities" ForeColor="Red" runat="server" ErrorMessage="*" ValidationGroup="Courses"></asp:RequiredFieldValidator>
                         </td>
                     </tr>
                     <tr>
                         <td class="span2">Status</td>
                         <td>
                             <asp:DropDownList ID="ddlStatus" runat="server" ValidationGroup="Courses">
                                 <asp:ListItem>Select</asp:ListItem>
                             </asp:DropDownList>
                         </td>
                     </tr>
                     <tr>
                         <td class="span2"></td>
                         <td>
                             <asp:Button ID="btnSave" Text="Save" runat="server" ValidationGroup="Courses" class="btn btn-primary" BackColor="#3459FC" OnClick="btnSave_Click"/>
                             <asp:Button ID="btnReset" runat="server" Text="Reset" class=" btn btn-warning"  OnClick="btnReset_Click"/>
                            
                         </td>
                     </tr>
                 </table>
             </div>
                    </div>
                </div>
         </asp:View>
        <asp:View ID="MCDNew" runat="server">
            <div class="row-fluid">
                <div class="span12">
             <div class="row-fluid">
                 <a href="ManageCourses.aspx">&lt;&lt; Back&nbsp;</a>
                 <div class="row-fluid">
                       <div id="lblSuccessMsg" runat="server" visible="false" class="alert alert-block alert-success span6">
							<button data-dismiss="alert" class="close" type="button">
								<i class="icon-remove"></i>
							</button>
							<p>
                                <i class="icon-ok"></i>
								Course details updated successfully.
							</p>
						</div>
                             <div id="lblErrorMsg" runat="server" visible="false" class="alert alert-error span6">
						    <button data-dismiss="alert" class="close" type="button">
							    <i class="icon-remove"></i>
						    </button>
                            <i class="icon-remove"></i>
                            <asp:Label ID="lblErrorMsgText" runat="server"></asp:Label>
                        </div>
                 </div>
                 <div class="table-header">
                     Add New Course Details
                 </div>
                 <table class="table table-hover dataTable table-bordered ">
                     <tr>
                         <td class="span2">Course Name</td>
                         <td>
                             <asp:TextBox ID="txtAddCourseName" runat="server"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="rfvAddCourseName" runat="server" ErrorMessage="*" ControlToValidate="txtAddCourseName" ForeColor="Red" ValidationGroup="NewCourse"></asp:RequiredFieldValidator>
                         </td>
                     </tr>
                     <tr>
                         <td class="span2">Description</td>
                         <td>
                             <asp:TextBox ID="txtAddDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="rfvAddDescription" runat="server" ErrorMessage="*" ControlToValidate="txtAddDescription" ForeColor="Red" ValidationGroup="NewCourse"></asp:RequiredFieldValidator>
                         </td>
                     </tr>
                     <tr>
                         <td class="span2">Course Duration</td>
                         <td>
                             <asp:TextBox ID="txtAddCourseDuration" runat="server"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="rfvAddCourseDuration" runat="server" ControlToValidate="txtAddCourseDuration" ErrorMessage="*" ForeColor="Red" ValidationGroup="NewCourse"></asp:RequiredFieldValidator>
                         </td>
                     </tr>
                     <tr>
                         <td class="span2">Opportunities</td>
                         <td>
                             <asp:TextBox ID="txtAddOpportunities" runat="server" TextMode="MultiLine"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="rfvAddOpportunities" ControlToValidate="txtAddOpportunities" ForeColor="Red" runat="server" ErrorMessage="*" ValidationGroup="NewCourse"></asp:RequiredFieldValidator>
                         </td>
                     </tr>
                     <tr>
                         <td class="span2">Status</td>
                         <td>
                             <asp:DropDownList ID="ddlAddStatus" runat="server" ValidationGroup="NewCourse">
                                 <asp:ListItem>Select</asp:ListItem>
                             </asp:DropDownList>
                         </td>
                     </tr>
                     <tr>
                         <td class="span2"></td>
                         <td>
                             <asp:Button ID="btnAddNew" Text="Save" runat="server" ValidationGroup="NewCourse" class="btn btn-primary" BackColor="#3459FC" OnClick ="btnAddNew_Click"/>
                             <asp:Button ID="btnAddReset" runat="server" Text="Reset" class="btn btn-warning" OnClick="btnAddReset_Click" />
                           
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
            $('#tblManageCourses').dataTable(
                {
                    "aoColumnDefs": [
                        { 'bSortable': false, 'aTargets': [0, 6] }
                    ]
                }
         );
        });
   </script>
</asp:Content>

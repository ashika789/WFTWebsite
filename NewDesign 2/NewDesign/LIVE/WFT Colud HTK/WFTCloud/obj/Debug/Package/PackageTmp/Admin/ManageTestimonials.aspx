<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminPages.Master" AutoEventWireup="true" CodeBehind="ManageTestimonials.aspx.cs" Inherits="WFTCloud.Admin.ManageTestimonials" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <title>Manage Testimonials</title>
    <meta name="Description" content="Testimonial Details  are managed here." />
    <meta name="Keywords" content="WFT Manage Testimonials " />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="mvContainer" runat="server" ActiveViewIndex="0">
        <asp:View ID="MTView" runat="server">
            <div class="row-fluid">
                <div class="span12">
                    <div class="row-fluid">
                        <div id="divMTSuccessMessage" runat="server" visible="false" class="alert alert-block alert-success span6">
                            <button data-dismiss="alert" class="close" type="button">
                                <i class="icon-remove"></i>
                            </button>
                            <p>
                                <i class="icon-ok"></i>
                                Testimonial Status Updated successfully.
                            </p>
                        </div>
                        <div id="divMTErrorMessage" runat="server" visible="false" class="alert alert-block alert-error span6">
                            <button data-dismiss="alert" class="close" type="button">
                                <i class="icon-remove"></i>
                            </button>
                            <p>
                                <i class="icon-ok"></i>
                                Enter the Valid Testimonial Informations.
                            </p>
                        </div>
                    </div>
                    <div class="row-fluid">
                        <div class="table-header">
                            Manage Testimonials        
                        </div>
                    </div>
                    <div role="grid" class="dataTables_wrapper">
                        <table id="tblManageTestimonials" class="table table-striped table-bordered table-hover dataTable">
                            <thead>
                                <tr role="row">
                                    <th class="center" role="columnheader" style="width:5%;">
                                        <label>
                                            <input type="checkbox" class="ace" />
                                            <span class="lbl"></span>
                                        </label>
                                    </th>
                                    <th role="columnheader" class="center" style="width:60%;">Testimonial</th>
                                    <th role="columnheader" class="center" style="width:10%;">Priority</th>
                                    <th role="columnheader" class="center" style="width:15%;">Record Status</th>
                                    <th role="columnheader" class="center" style="text-align:center; width:10%;">Options</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptrTestimonials" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="center">
                                                <asp:CheckBox ID="chkSelect" runat="server" class="ace" />
                                            </td>
                                            <td><%# Eval("Testimonial1")%><asp:HiddenField ID="hdnTestimonialsId" runat="server" Value='<%# Eval("TestimonialID")%>' />
                                            </td>
                                            <td><%# Eval("Priority")%></td>
                                            <td><%# WFTCloud.GeneralReusableMethods.GetStatusString(Eval("RecordStatus").ToString()) %></td>
                                            <td>
                                                <div class="action-buttons">
                                                    <a data-rel="tooltip" title="Edit Testimonial Informations" href='ManageTestimonials.aspx?editTestimonialsid=<%# Eval("TestimonialID")%>' class="green">
                                                        <i class="icon-pencil bigger-130"></i>
                                                    </a>
                                                    <a data-rel="tooltip" title="Activate/Deactivate" href='ManageTestimonials.aspx?<%# WFTCloud.GeneralReusableMethods.GetActivateDeactivateAction(Eval("RecordStatus").ToString()) %>=<%# Eval("TestimonialID")%>' class="blue">
                                                        <i class='<%# WFTCloud.GeneralReusableMethods.GetActivateDeactivateIcon(Eval("RecordStatus").ToString()) %>'></i>
                                                    </a>
                                                   <%-- <a data-rel="tooltip" title="Delete Testimonial" href='ManageTestimonials.aspx?delete=<%# Eval("TestimonialID")%>' class="red">
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
                        <asp:Button ID="btnAddNewTestimonials" runat="server" Text="Add New Testimonials" class="btn btn-primary" OnClick="btnAddNewTestimonials_Click" />
                        <asp:Button ID="btnActivate" runat="server" Text="Activate" class="btn btn-info" OnClick="btnActivate_Click" />
                        <asp:Button ID="btnDeactivate" runat="server" Text="Deactivate" class="btn btn-warning" OnClick="btnDeactivate_Click" />
                        <%--<asp:Button ID="btnDelete" runat="server" Text="Delete" class="btn btn-danger" OnClick="btnDelete_Click" />--%>
                        <asp:CheckBox ID="chkShowDeleted" runat="server" AutoPostBack="True" OnCheckedChanged="chkShowDeleted_CheckedChanged" />
                        <span class="label label-warning arrowed-right">Show De-Activated</span>
                    </div>
                </div>
            </div>
        </asp:View>
        <asp:View ID="MTEdit" runat="server">
            <div class="row-fluid">
                <div class="span12">
            <div class="row-fluid">
                <a href="ManageTestimonials.aspx">&lt;&lt; Back&nbsp;</a>
                <div class="row-fluid">
                      <div id="lblSuccessMessage" runat="server" visible="false" class="alert alert-block alert-success span6">
                                <button data-dismiss="alert" class="close" type="button">
                                    <i class="icon-remove"></i>
                                </button>
                                <p>
                                    <i class="icon-ok"></i>
                                    Testimonial Informations Updated Successfully.
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
                    Edit Testimonials Informations
                </div>
                <table class="table dataTable table-bordered ">
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <tr>
                        <td class="span3">Customer Name</td>
                        <td>
                             <asp:TextBox ID="txtEditCustomerName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="span3">Designation</td>
                        <td>
                             <asp:TextBox ID="txtEditDesignation" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEditDesignaiton" runat="server" ErrorMessage="*" ControlToValidate="txtEditDesignation" ForeColor="Red" ValidationGroup="Testimonials"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span3">Customer Organisation</td>
                        <td>
                             <asp:TextBox ID="txtEditCustOrg" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEditCustOrg" runat="server" ErrorMessage="*" ControlToValidate="txtEditCustOrg" ForeColor="Red" ValidationGroup="Testimonials"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                     <tr>
                        <td class="span3">Customer Since</td>
                        <td>
                             <asp:TextBox ID="txtEditCustSince" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCustSince" runat="server" ErrorMessage="*" ControlToValidate="txtEditCustSince" ForeColor="Red" ValidationGroup="Testimonials"></asp:RequiredFieldValidator>
                        <asp:CalendarExtender ID="ceEditCustSince" runat="server" Enabled="true" TargetControlID="txtEditCustSince" Format="dd-MMM-yyyy">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                      <tr>
                        <td class="span3">No. of Dedicated Systems</td>
                        <td>
                             <asp:TextBox ID="txtEditDedicateSystems" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEditDedicateSystems" runat="server" ErrorMessage="*" ControlToValidate="txtEditDedicateSystems" ForeColor="Red" ValidationGroup="Testimonials"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                      <tr>
                        <td class="span3">Usage</td>
                        <td>
                             <asp:TextBox ID="txtEditUsage" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEditUsage" runat="server" ErrorMessage="*" ControlToValidate="txtEditUsage" ForeColor="Red" ValidationGroup="Testimonials"></asp:RequiredFieldValidator>
                        </td>
                    </tr>


                    <tr>
                        <td class="span3">Testimonial</td>
                        <td style="vertical-align: top;">
                            <asp:TextBox ID="txtTestimoinal" runat="server" TextMode="MultiLine"  Width="90%" Rows="20"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvTestimonial" runat="server" ErrorMessage="*" ControlToValidate="txtTestimoinal" ForeColor="Red" ValidationGroup="Testimonials"></asp:RequiredFieldValidator>
                            <%--<asp:HtmlEditorExtender ID="HtmleditorTestimonial" runat="server" TargetControlID="txtTestimoinal" DisplaySourceTab="true">
                            </asp:HtmlEditorExtender>--%>
                        </td>
                    </tr>
                     <tr>
                        <td class="span3">Home Page Version</td>
                        <td style="vertical-align: top;">
                            <asp:TextBox ID="txtHomePageVersion" runat="server" TextMode="MultiLine" Width="90%" Rows="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvHomePageVersion" runat="server" ErrorMessage="*" ControlToValidate="txtHomePageVersion" ForeColor="Red" ValidationGroup="Testimonials"></asp:RequiredFieldValidator>
                            <%--<asp:HtmlEditorExtender ID="HtmlEditorHPV" runat="server" TargetControlID="txtHomePageVersion" DisplaySourceTab="true">
                            </asp:HtmlEditorExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="span3">Priority</td>
                        <td>
                            <asp:DropDownList ID="ddlPriority" runat="server" ValidationGroup="Testimonials">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="span3">Record Status</td>
                        <td>
                            <asp:DropDownList ID="ddlRecordStatus" runat="server" ValidationGroup="Testimonials">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="span3"></td>
                        <td>
                            <asp:Button ID="btnSave" Text="Save" runat="server" ValidationGroup="Testimonials" class="btn btn-primary" BackColor="#3459FC" OnClick="btnSave_Click" />
                            <asp:Button ID="btnReset" runat="server" Text="Reset" class=" btn btn-warning" OnClick="btnReset_Click" />
                          
                        </td>
                    </tr>
                </table>
            </div>
                    </div>
                </div>
        </asp:View>
        <asp:View ID="MTNew" runat="server">
            <div class="row-fluid">
                <div class="span12">
            <div class="row-fluid">
                <a href="ManageTestimonials.aspx">&lt;&lt; Back&nbsp;</a>
                <div class="row-fluid">
                     <div id="lblSuccessMsg" runat="server" visible="false" class="alert alert-block alert-success span6">
                                <button data-dismiss="alert" class="close" type="button">
                                    <i class="icon-remove"></i>
                                </button>
                                <p>
                                    <i class="icon-ok"></i>
                                    Testimonial Information's Added successfully.
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
                    Add New Testimonials
                </div>
                <table class="table dataTable table-bordered ">
                    <tr>
                        <td class="span3">Customer Name</td>
                        <td>
                             <asp:TextBox ID="txtAddCustName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="span3">Designation</td>
                        <td>
                             <asp:TextBox ID="txtAddDesignation" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvAddDesignation" runat="server" ErrorMessage="*" ControlToValidate="txtAddDesignation" ForeColor="Red" ValidationGroup="NewTestimonial"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span3">Customer Organisation</td>
                        <td>
                             <asp:TextBox ID="txtAddCustOrg" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvAddCustOrg" runat="server" ErrorMessage="*" ControlToValidate="txtAddCustOrg" ForeColor="Red" ValidationGroup="NewTestimonial"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                     <tr>
                        <td class="span3">Customer Since</td>
                        <td>
                             <asp:TextBox ID="txtAddCustomerSince" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvAddCustomerSince" runat="server" ErrorMessage="*" ControlToValidate="txtAddCustomerSince" ForeColor="Red" ValidationGroup="NewTestimonial"></asp:RequiredFieldValidator>
                       <asp:CalendarExtender ID="ceAddCustomerSince" runat="server" Enabled="true" TargetControlID="txtAddCustomerSince" Format="dd-MMM-yyyy">
                            </asp:CalendarExtender>
                              </td>
                    </tr>
                      <tr>
                        <td class="span3">No. of Dedicated Systems</td>
                        <td>
                             <asp:TextBox ID="txtAddDedicatedSystems" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvAddDedicatedSystems" runat="server" ErrorMessage="*" ControlToValidate="txtAddDedicatedSystems" ForeColor="Red" ValidationGroup="NewTestimonial"></asp:RequiredFieldValidator>
                        </td>
                    </tr>

                     <tr>
                        <td class="span3">Usage</td>
                        <td>
                             <asp:TextBox ID="txtAddUsage" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvAddUsage" runat="server" ErrorMessage="*" ControlToValidate="txtAddUsage" ForeColor="Red" ValidationGroup="Testimonials"></asp:RequiredFieldValidator>
                        </td>
                    </tr>


                    <tr>
                        <td class="span3">Testimonial</td>
                        <td style="vertical-align: top;">
                            <asp:TextBox ID="txtAddTestimonial" runat="server" TextMode="MultiLine" Width="90%" Rows="20"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvAddTestimonial" runat="server" ErrorMessage="*" ControlToValidate="txtAddTestimonial" ForeColor="Red" ValidationGroup="NewTestimonial"></asp:RequiredFieldValidator>
                            <%--<asp:HtmlEditorExtender ID="HtmlEditorAddTestimonial" runat="server" TargetControlID="txtAddTestimonial" DisplaySourceTab="true">
                            </asp:HtmlEditorExtender>--%>
                        </td>
                    </tr>
                       <tr>
                        <td class="span3">Home Page Version</td>
                        <td style="vertical-align: top;">
                            <asp:TextBox ID="txtNewHomePageVersion" runat="server" TextMode="MultiLine" Width="90%" Rows="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNewHomePageVersion" runat="server" ErrorMessage="*" ControlToValidate="txtNewHomePageVersion" ForeColor="Red" ValidationGroup="NewTestimonial"></asp:RequiredFieldValidator>
                            <%--<asp:HtmlEditorExtender ID="HtmlEditorNewHPV" runat="server" TargetControlID="txtNewHomePageVersion" DisplaySourceTab="true">
                            </asp:HtmlEditorExtender>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="span3">Priority</td>
                        <td>
                            <asp:DropDownList ID="ddlAddPriority" runat="server" ValidationGroup="NewTestimonial">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="span3">Record Status</td>
                        <td>
                            <asp:DropDownList ID="ddlAddRecordStatus" runat="server" ValidationGroup="NewTestimonial">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="span3"></td>
                        <td>
                            <asp:Button ID="btnAddSave" Text="Save" runat="server" ValidationGroup="NewTestimonial" class="btn btn-primary" BackColor="#3459FC" OnClick="btnAddSave_Click"/>
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
            $('#tblManageTestimonials').dataTable(
                 {
                     "aoColumnDefs": [
                         { 'bSortable': false, 'aTargets': [0, 4] }
                     ]
                 }
         );
        });
   </script>
</asp:Content>

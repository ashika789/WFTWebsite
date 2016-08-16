<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminPages.Master" AutoEventWireup="true" CodeBehind="ServiceProvisioningDetails.aspx.cs" Inherits="WFTCloud.Admin.ServiceProvisioningDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>StandardServiceProvisioningDetails</title>
    <meta name="Description" content="Services conigured at category level can be updated here" />
    <meta name="Keywords" content="WFT Service Provisioning Details" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="mvContainer" runat="server" ActiveViewIndex="0">
        <asp:View ID="SPView" runat="server">
            <div class="row-fluid">
                <div class="span12">
                    <div class="row-fluid">
                        <div id="divSPDSuccessMessage" runat="server" visible="false" class="alert alert-block alert-success span6">
                            <button data-dismiss="alert" class="close" type="button">
                                <i class="icon-remove"></i>
                            </button>
                            <p>
                                <i class="icon-ok"></i>
                                Service Provision Updated successfully.
                            </p>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="table-header">
                                Standard Service Provisioning Details        
                            </div>
                        <div role="grid" class="dataTables_wrapper">
                        <div role="grid" class="dataTables_wrapper">
                            <table id="tblServiceProvision" class="table table-striped table-bordered table-hover dataTable">
                                <thead>
                                    <tr role="row">
                                        <th class="center" role="columnheader">
                                            <label><input type="checkbox" class="ace"/>
                                                <span class="lbl"></span>
                                            </label>
                                        </th>
                                        <th role="columnheader" class="center">Service Category</th>
                                        <th role="columnheader" class="center">Service Name</th>
                                        <th role="columnheader" class="center">UserMin</th>
                                        <th role="columnheader" class="center">UserMax</th>
                                        <th role="columnheader" class="center">ServiceUrl</th>
                                        <th role="columnheader" class="center">Attachment</th>
                                        <%--<th role="columnheader" class="center">Expiration Period</th>--%>
                                        <th role="columnheader" class="center">Record Status</th>
                                        <th role="columnheader" class="center" style="text-align:center;">Options</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rptrServiceProvision" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="center">
                                                    <asp:CheckBox ID="chkSelect" runat="server" class="ace" />
                                                </td>
                                                <td><%# Eval("CategoryName")%><asp:HiddenField ID="hdnServiceProvisionID" runat="server" Value='<%# Eval("ServiceProvisionID")%>' /></td>
                                                <td><%# Eval("ServiceName")%></td>
                                                <td><%# Eval("UserMin")%></td>
                                                <td><%# Eval("UserMax")%></td>
                                                <td><a href="<%# Eval("ServiceUrl")%>" target="_blank"><%# Eval("ServiceUrl")%></a></td>
                                                <td style="text-align:center">
                                                    <%# (Eval("AttachmentPath")  != null ? (Eval("AttachmentPath").ToString() != ""? "":"-"):"-")%>
                                                    <a data-rel="tooltip" title="Preview Attachment" target="_blank" href='<%# Eval("AttachmentPath")%>' class="purple" style='<%# (Eval("AttachmentPath")  != null ? (Eval("AttachmentPath").ToString() != ""? "display:block;":"display:none;"):"display:none;")%>' >
                                                        <i class="icon-file bigger-130"></i>
                                                    </a>
                                                </td>
                                                <%--<td><%# Eval("ExpirationPeriod")%></td>--%>
                                                <td><%# WFTCloud.GeneralReusableMethods.GetStatusString(Eval("RecordStatus").ToString()) %></td>
                                                <td>
                                                <div class="action-buttons">
                                                    <a data-rel="tooltip" title="Edit Service Provision" href='ServiceProvisioningDetails.aspx?editserviceprovisionid=<%# Eval("ServiceProvisionID")%>' class="green">
                                                        <i class="icon-pencil bigger-130"></i>
                                                    </a>
                                                    <a data-rel="tooltip" title="Activate/Deactivate" href='ServiceProvisioningDetails.aspx?<%# WFTCloud.GeneralReusableMethods.GetActivateDeactivateAction(Eval("RecordStatus").ToString()) %>=<%# Eval("ServiceProvisionID")%>' class="blue">
                                                        <i class='<%# WFTCloud.GeneralReusableMethods.GetActivateDeactivateIcon(Eval("RecordStatus").ToString()) %>'></i>
                                                    </a>
                                                   <%-- <a data-rel="tooltip" title="Delete Service Provision" href='ServiceProvisioningDetails.aspx?delete=<%# Eval("ServiceProvisionID")%>' class="red">
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
                            <asp:Button ID="btnNewServiceProvision" runat="server" Text="Add New" 
                                class="btn btn-primary" OnClick="btnNewServiceProvision_Click"/>
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
        <asp:View ID="SPUpdate" runat="server">
            <div class="row-fluid">
                <div class="span12">
                <div class="row-fluid">
                <div id="divServiceProvisionSuccess" runat="server" visible="false" class="alert alert-block alert-success span6">
                    <button data-dismiss="alert" class="close" type="button">
                        <i class="icon-remove"></i>
                    </button>
                    <p>
                        <i class="icon-ok"></i>
                        Service Provision details updated successfully.
                    </p>
                </div>
                <div id="divServiceProvisionError" runat="server" visible="false" class="alert alert-error span6">
				    <button data-dismiss="alert" class="close" type="button">
					    <i class="icon-remove"></i>
				    </button>
                    <i class="icon-remove"></i>
                    <asp:Label ID="lblServiceProvisionError" runat="server"></asp:Label>
                </div>
                </div>
                <a href="ServiceProvisioningDetails.aspx">&lt;&lt; Back&nbsp;</a>
                <div class="table-header">
                    Edit Standard Service Provisioning Details
                </div>
                <table class="table table-hover dataTable table-bordered ">
                    <tr>
                        <td class="span4">Choose Category
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlChooseCategory" runat="server" OnSelectedIndexChanged="ddlChooseCategory_SelectedIndexChanged" AutoPostBack="true" Enabled="False">
                                <asp:ListItem Text="" Value="" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Choose Standard Service</td>
                        <td>
                            <asp:DropDownList ID="ddlChooseService" runat="server" Enabled="False">
                                <asp:ListItem Text="" Value="" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">User Name</td>
                        <td>
                            <asp:TextBox ID="txtUserName" runat="server">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName" ErrorMessage="*" ForeColor="Red" ValidationGroup="ServiceProvision"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Service Password</td>
                        <td>
                            <asp:TextBox ID="txtServicePassword" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvServicePassword" runat="server" ControlToValidate="txtServicePassword" ErrorMessage="*" ForeColor="Red" ValidationGroup="ServiceProvision"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Service URL</td>
                        <td>
                            <asp:TextBox ID="txtServiceURL" runat="server">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvServiceURL" runat="server" ControlToValidate="txtServiceURL" ErrorMessage="*" ForeColor="Red" ValidationGroup="ServiceProvision"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Instance Number</td>
                        <td>
                            <asp:TextBox ID="txtInstanceNumber" runat="server">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvInstanceNumber" runat="server" ControlToValidate="txtInstanceNumber" ErrorMessage="*" ForeColor="Red" ValidationGroup="NewServiceProvision"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Application Server</td>
                        <td>
                            <asp:TextBox ID="txtApplicationServer" runat="server">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvApplicationServer" runat="server" ControlToValidate="txtApplicationServer" ErrorMessage="*" ForeColor="Red" ValidationGroup="NewServiceProvision"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">SID On Server</td>
                        <td>
                            <asp:TextBox ID="txtUIDOnServer" runat="server">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUIDOnServer" runat="server" ControlToValidate="txtUIDOnServer" ErrorMessage="*" ForeColor="Red" ValidationGroup="ServiceProvision"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                      <tr>
                        <td class="span2">Developer Key</td>
                        <td>
                            <asp:TextBox ID="txtDeveloperKey" runat="server">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Other Information</td>
                        <td>
                            <asp:TextBox ID="txtOtherInormation" runat="server" TextMode="MultiLine">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">UMin</td>
                        <td>
                            <asp:TextBox ID="txtUMin" runat="server">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUMin" runat="server" ControlToValidate="txtUMin" ErrorMessage="*" ForeColor="Red" ValidationGroup="ServiceProvision"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cvumin" runat="server" ErrorMessage="UMin value should be less than UMax" ControlToCompare="txtUMax" ValidationGroup="ServiceProvision"
                                 ControlToValidate="txtUMin" ForeColor="Red" Operator="LessThan" Type="Integer"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">UMax</td>
                        <td>
                            <asp:TextBox ID="txtUMax" runat="server">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUMax" runat="server" ControlToValidate="txtUMax" ErrorMessage="*" ForeColor="Red" ValidationGroup="ServiceProvision"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Add Attachments</td>
                        <td>
                            <asp:FileUpload ID="fluAddAttachments" runat="server" /><br />
                            <asp:HyperLink runat="server" ID="hlAttachment" Visible="false" Target="_blank"></asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Add Image Attachments
                        </td>
                        <td>
                            <asp:FileUpload ID="fluediImageAttachment" runat="server" />
                            <asp:RegularExpressionValidator ID="revImagesFileUpload" runat="server" ErrorMessage="Invalid Image File" ForeColor="Red" ValidationExpression="^([0-9a-zA-Z_\-~ :\\])+(.jpg|.JPG|.jpeg|.JPEG|.bmp|.BMP|.gif|.GIF|.png|.PNG)$" ValidationGroup="ServiceProvision" ControlToValidate="fluediImageAttachment"></asp:RegularExpressionValidator>
                            <br /><asp:HyperLink runat="server" ID="hlImageAttachment" Visible="false" Target="_blank"></asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Current User Counter</td>
                        <td>
                            <asp:TextBox ID="txtCurrentUserCounter" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCurrentUserCounter" runat="server" ControlToValidate="txtCurrentUserCounter" ErrorMessage="*" ForeColor="Red" ValidationGroup="ServiceProvision"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Notification Required At</td>
                        <td>
                            <asp:DropDownList ID="ddlNotificaion" runat="server" >
                                <asp:ListItem Text="0" Value="0"></asp:ListItem>
                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                <asp:ListItem Text="200" Value="200"></asp:ListItem>
                                <asp:ListItem Text="300" Value="300"></asp:ListItem>
                                <asp:ListItem Text="400" Value="400"></asp:ListItem>
                                <asp:ListItem Text="500" Value="500"></asp:ListItem>
                                <asp:ListItem Text="600" Value="600"></asp:ListItem>
                                <asp:ListItem Text="700" Value="700"></asp:ListItem>
                                <asp:ListItem Text="800" Value="800"></asp:ListItem>
                                <asp:ListItem Text="900" Value="900"></asp:ListItem>
                                <asp:ListItem Text="1000" Value="1000"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
<%--                    <tr>
                        <td class="span2">Expiration Period</td>
                        <td>
                            <asp:DropDownList ID="ddlExpirationPeriod" runat="server">
                                <asp:ListItem Text="0" Value="0"></asp:ListItem>
                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>--%>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" class="btn btn-primary" OnClick="btnSubmit_Click" ValidationGroup="ServiceProvision" />
                        </td>
                    </tr>
                </table>
            </div>
            </div>
        </asp:View> 
        <asp:View runat="server" ID="NewSP">
            <div class="row-fluid">
                <div class="span12">
                <div class="row-fluid">
                <div id="divNewSuccess" runat="server" visible="false" class="alert alert-block alert-success span6">
                    <button data-dismiss="alert" class="close" type="button">
                        <i class="icon-remove"></i>
                    </button>
                    <p>
                        <i class="icon-ok"></i>
                        New Service Provision added successfully.
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
                <a href="ServiceProvisioningDetails.aspx">&lt;&lt; Back&nbsp;</a>
                    <asp:HiddenField runat="server" ID="hidServiceProvisionID" value=""/>
                <div class="table-header">
                    New Standard Service Provisioning Details
                </div>
                <table class="table table-hover dataTable table-bordered ">
                    <tr>
                        <td class="span4">Choose Category
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlNewCategory" runat="server" OnSelectedIndexChanged="ddlNewCategory_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Choose Standard Service</td>
                        <td>
                            <asp:DropDownList ID="ddlNewService" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlNewService_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvNewUserName0" runat="server" ControlToValidate="ddlNewService" ErrorMessage="*" ForeColor="Red" ValidationGroup="NewServiceProvision"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">User Name</td>
                        <td>
                            <asp:TextBox ID="txtNewUserName" runat="server">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNewUserName" runat="server" ControlToValidate="txtNewUserName" ErrorMessage="*" ForeColor="Red" ValidationGroup="NewServiceProvision"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Service Password</td>
                        <td>
                            <asp:TextBox ID="txtNewServicePassword" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNewServicePassword" runat="server" ControlToValidate="txtNewServicePassword" ErrorMessage="*" ForeColor="Red" ValidationGroup="NewServiceProvision"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Service URL</td>
                        <td>
                            <asp:TextBox ID="txtNewServiceURL" runat="server">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNewServiceURL" runat="server" ControlToValidate="txtNewServiceURL" ErrorMessage="*" ForeColor="Red" ValidationGroup="NewServiceProvision"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Instance Number</td>
                        <td>
                            <asp:TextBox ID="txtNewInstanceNumber" runat="server">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNewInstanceNumber" runat="server" ControlToValidate="txtNewInstanceNumber" ErrorMessage="*" ForeColor="Red" ValidationGroup="NewServiceProvision"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Application Server</td>
                        <td>
                            <asp:TextBox ID="txtNewApplicationServer" runat="server">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNewApplicationServer" runat="server" ControlToValidate="txtNewApplicationServer" ErrorMessage="*" ForeColor="Red" ValidationGroup="NewServiceProvision"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">SID On Server</td>
                        <td>
                            <asp:TextBox ID="txtNewUID" runat="server">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNewUID" runat="server" ControlToValidate="txtNewUID" ErrorMessage="*" ForeColor="Red" ValidationGroup="NewServiceProvision"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Developer Key</td>
                        <td>
                            <asp:TextBox ID="txtNewDeveloperKey" runat="server">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Other Information</td>
                        <td>
                            <asp:TextBox ID="txtNewOtherInfo" runat="server" TextMode="MultiLine">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">UMin</td>
                        <td>
                            <asp:TextBox ID="txtNewUmin" runat="server">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNewUmin" runat="server" ControlToValidate="txtNewUmin" ErrorMessage="*" ForeColor="Red" ValidationGroup="NewServiceProvision"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cvNewUmin" runat="server" ErrorMessage="UMin value should be less than UMax" ControlToCompare="txtNewUmax" ValidationGroup="NewServiceProvision"
                                 ControlToValidate="txtNewUmin" ForeColor="Red" Operator="LessThan" Type="Integer"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">UMax</td>
                        <td>
                            <asp:TextBox ID="txtNewUmax" runat="server">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNewUmax" runat="server" ControlToValidate="txtNewUmax" ErrorMessage="*" ForeColor="Red" ValidationGroup="NewServiceProvision"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Add Attachments</td>
                        <td>
                            <asp:FileUpload ID="fluNewAddAttachment" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Add Image Attachments
                        </td>
                        <td>
                            <asp:FileUpload ID="fluNewImageAttachment" runat="server" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="fluNewImageAttachment" ErrorMessage="Invalid Image File" ForeColor="Red" ValidationExpression="^([0-9a-zA-Z_\-~ :\\])+(.jpg|.JPG|.jpeg|.JPEG|.bmp|.BMP|.gif|.GIF|.png|.PNG)$" ValidationGroup="NewServiceProvision"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Current User Counter</td>
                        <td>
                            <asp:TextBox ID="txtNewCurrentUserCount" runat="server" ReadOnly="True">0</asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNewCurrentUserCount" runat="server" ControlToValidate="txtNewCurrentUserCount" ErrorMessage="*" ForeColor="Red" ValidationGroup="NewServiceProvision"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Notification Required At</td>
                        <td>
                            <asp:DropDownList ID="ddlNewNotificationReqAt" runat="server" >
                                <asp:ListItem Text="0" Value="0"></asp:ListItem>
                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                <asp:ListItem Text="200" Value="200"></asp:ListItem>
                                <asp:ListItem Text="300" Value="300"></asp:ListItem>
                                <asp:ListItem Text="400" Value="400"></asp:ListItem>
                                <asp:ListItem Text="500" Value="500"></asp:ListItem>
                                <asp:ListItem Text="600" Value="600"></asp:ListItem>
                                <asp:ListItem Text="700" Value="700"></asp:ListItem>
                                <asp:ListItem Text="800" Value="800"></asp:ListItem>
                                <asp:ListItem Text="900" Value="900"></asp:ListItem>
                                <asp:ListItem Text="1000" Value="1000"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <%--<tr>
                        <td class="span2">Expiration Period</td>
                        <td>
                            <asp:DropDownList ID="ddlNewExpirationPeriod" runat="server">
                                <asp:ListItem Text="0" Value="0"></asp:ListItem>
                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>--%>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="btnNewSave" runat="server" Text="Submit" class="btn btn-primary" OnClick="btnNewSave_Click" ValidationGroup="NewServiceProvision" />
                        </td>
                    </tr>
                </table>
            </div>
            </div>
        </asp:View>
    </asp:MultiView>
    <script type="text/javascript">
        jQuery(function ($) {
            $('#tblServiceProvision').dataTable(
                {
                    "aoColumnDefs": [
                        { 'bSortable': false, 'aTargets': [0, 8] }
                    ]
                }
         );
        });
   </script>
    </asp:Content>
<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminPages.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="WFTCloud.Admin.UserList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>User List</title>
    <meta name="description" content="List of Users can be seen here and we have an options of Check services, Add new services, Access acount, Delete User, Manage CRM issue and View User Profile" />
    <meta name="keywords" content="WFT Users list" />
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript">
        function ValidateSelection() {
            if ($('#tblUsersList input:checked').length > 0) {
                return true;
            }
            else {
                alert('Please select atleast one user.');
                return false;
            }
        }
        </script>
      <asp:ScriptManager ID="scriptManager" runat="server"></asp:ScriptManager>

    <asp:MultiView ID="mvContainer" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwUserList" runat="server">
         
                    <div class="row-fluid">
                        <div class="span12">
                            <div class="row-fluid">
                                <div id="DivUserListSuccess" runat="server" visible="false" class="alert alert-block alert-success span6">
                                    <button data-dismiss="alert" class="close" type="button">
                                        <i class="icon-remove"></i>
                                    </button>
                                    <p>
                                        <i class="icon-ok"></i>
                                        User Status updated successfully.
                                    </p>
                                </div>
                    <div id="DivUserListStatusFailed" runat="server" visible="false" class="alert alert-error span12">
                        <button data-dismiss="alert" class="close" type="button">
                            <i class="icon-remove"></i>
                        </button>
                        <i class="icon-remove"></i>
                        <asp:Label runat="server" ID="lblStatusUpdateError"></asp:Label>
                    </div>
                            </div>
                            <div class="row-fluid">
                                <div class="row-fluid">
                                    <%--<asp:CheckBox ID="chkShowAllUser" runat="server" AutoPostBack="true" OnCheckedChanged="chkShowAllUser_CheckedChanged" />
                                    <span class="label label-warning arrowed-right">Show All User</span>--%>
                                    <asp:Label ID="lblMail" runat="server" Text="Enter User Name"></asp:Label>
                                    <asp:TextBox ID="txtSearchUserName" runat="server" class="customer-login-input" placeholder="User Name" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                               
                                    <asp:Button ID="SearcButton" runat ="server" CssClass="btn btn-primary" Text ="Search" OnClick="SearchButton_Click" />
                                </div>  
                               
                                <br />
                                <div class="table-header">
                                    <asp:Label runat="server" ID="lblUserListTitle" Text="Users List"></asp:Label>                                    	        
                                </div>
                                <div class="dataTables_wrapper">
                                    <div role="grid" class="dataTables_wrapper">
                                        <table id="tblUsersList" class="table table-striped table-bordered table-hover dataTable">
                                            <thead>
                                                <tr role="row">
                                                    <th class="center" role="columnheader">
                                                        <label>
                                                            <input type="checkbox" class="ace" />
                                                            <span class="lbl"></span>
                                                        </label>
                                                    </th>
                                                    <th role="columnheader">Name</th>
                                                    <th role="columnheader">Email</th>
                                                    <th role="columnheader">Registration Date and Time</th>
                                                    <th role="columnheader">Status</th>
                                                    <th role="columnheader">Options</th>
                                                </tr>
                                            </thead>
                                            <tbody role="alert">
                                                <asp:Repeater ID="rptrUsersList" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="center">
                                                                <asp:CheckBox ID="chkSelect" runat="server" class="ace" />
                                                                <asp:HiddenField ID="hdnMembershipID" runat="server" Value='<%# Eval("UserMembershipID")%>' />
                                                            </td>
                                                            <td><a href='Userlist.aspx?viewuserprofileid=<%#Eval("UserMembershipID") %>'>
                                                                <%# Eval("FirstName")%> <%# Eval("MiddleName")%> <%# Eval("LastName")%></a>
                                                                
                                                            </td>
                                                            <td><a href='mailto:<%#Eval("EmailID") %>'>
                                                                <asp:Label ID="UserMailID" runat="server" Text='<%# Eval("EmailID")%>'></asp:Label></a></td>
                                                            <td><%# Eval("CreatedOn")%></td>
                                                            <td><%# WFTCloud.GeneralReusableMethods.GetStatusString(Eval("RecordStatus").ToString()) %></td>
                                                            <td>
                                                                <div class="action-buttons">
                                                                    <a data-rel="tooltip" title="Check Services" href='UserServices.aspx?userid=<%#Eval("UserMembershipID") %>&showview=SubscribedService' class="blue">
                                                                        <i class="icon-ok-sign bigger-130"></i>
                                                                    </a>
                                                                    <a data-rel="tooltip" title="Add New Service" href='UserServices.aspx?userid=<%# Eval("UserMembershipID")%>&showview=AvailableService' class="green">
                                                                        <i class="icon-plus-sign-alt bigger-130"></i>
                                                                    </a>
                                                                    <%--  <a data-rel="tooltip" title="Delete User" href='UserList.aspx?deactivate=<%# Eval("UserMembershipID")%>' class="red">
                                    <i class="icon-trash bigger-130"></i>
                                </a>--%>
                                                                    <a data-rel="tooltip" title="Activate/Deactivate User" href='UserList.aspx?<%# WFTCloud.GeneralReusableMethods.GetActivateDeactivateAction(Eval("RecordStatus").ToString()) %>=<%# Eval("UserMembershipID")%>' class="blue">
                                                                        <i class='<%# WFTCloud.GeneralReusableMethods.GetActivateDeactivateIcon(Eval("RecordStatus").ToString()) %>'></i>
                                                                    </a>
                                                                    <%--<a data-rel="tooltip" title="Manage CRM Issue" href='UserServices.aspx?userid=<%#Eval("UserMembershipID") %>&showview=ManageCrmIssue' class="purple">
                                                                        <i class="icon-briefcase bigger-130"></i>
                                                                    </a>--%>
                                                                    <a data-rel="tooltip" title="View User Profile" href='Userlist.aspx?viewuserprofileid=<%#Eval("UserMembershipID") %>' class="blue">
                                                                        <i class="icon-user bigger-130"></i>
                                                                    </a>
                                                                    <a data-rel="tooltip" title="Send Email" href='Userlist.aspx?sendemailid=<%#Eval("UserMembershipID") %>' class="yellow">
                                                                        <i class="icon-envelope-alt bigger-130"></i>
                                                                    </a>
                                                                    <asp:LinkButton data-rel="tooltip" title="Access Account" ID="lkbAccessAccount" runat="server" OnClick="lkbAccessAccount_Click" OnClientClick="return ConfirmAccessAccount()"><i class="icon-desktop bigger-130"></i></asp:LinkButton>
                                                                   
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
                                    <asp:Button ID="btnAddNewUser" runat="server" Text="Add New User"
                                        class="btn btn-primary" OnClick="btnAddNewUser_Click" />
                                    <asp:Button ID="btnActivate" runat="server" Text="Activate"
                                        class="btn btn-info" OnClick="btnActivate_Click" />
                                    <asp:Button ID="btnDeactivate" runat="server" Text="Deactivate"
                                        class="btn btn-warning" OnClick="btnDeactivate_Click" />
                                    <asp:Button ID="btnNotifyEmail" runat="server" Text="Notify Email"
                                        class="btn btn-purple" OnClick="btnNotifyEmail_Click" />
                                   <%-- <asp:CheckBox ID="chkShowDeleted" runat="server" AutoPostBack="True"
                                        OnCheckedChanged=" " />
                                    <span class="label label-warning arrowed-right">Show De-Activated</span>--%>
                                </div>
                            </div>
                        </div>
                    </div>
              
        </asp:View>
        <asp:View ID="vwUserProfile" runat="server">
            <div class="row-fluid">
                 <div class="span12">
               <div class="row-fluid">
                <a href="UserList.aspx">&lt;&lt; Back&nbsp;</a>
                <div class="row-fluid">
                    <div id="divProfileSuccess" runat="server" visible="false" class="alert alert-block alert-success span6">
                        <button data-dismiss="alert" class="close" type="button">
                            <i class="icon-remove"></i>
                        </button>
                        <p>
                            <i class="icon-ok"></i>
                            Profile details updated successfully.
                        </p>
                    </div>
                    <div id="divProfileError" runat="server" visible="false" class="alert alert-error span6">
                        <button data-dismiss="alert" class="close" type="button">
                            <i class="icon-remove"></i>
                        </button>
                        <i class="icon-remove"></i>
                        <asp:Label ID="lblProfileError" runat="server"></asp:Label>
                    </div>
                    <div id="PasswordSuccessMessage" runat="server" visible="false" class="alert alert-block alert-success span6">
                        <button data-dismiss="alert" class="close" type="button">
                            <i class="icon-remove"></i>
                        </button>
                        <p>
                            <i class="icon-ok"></i>
                            New Password updated successfully.
                        </p>
                    </div>
                    <div id="PasswordErrorMessage" runat="server" visible="false" class="alert alert-error span6">
                        <button data-dismiss="alert" class="close" type="button">
                            <i class="icon-remove"></i>
                        </button>
                        <i class="icon-remove"></i>
                        <asp:Label ID="lblPasswordErrorMessage" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="table-header">
                    User Profile
                </div>
                <table class="table table-hover dataTable table-bordered ">
                    <tr>
                        <td class="span4">First Name
                        </td>
                        <td>
                            <asp:TextBox ID="txtProfileFirstName" runat="server" AutoCompleteType="Disabled" autocomplete="off" Enabled="false"></asp:TextBox>
                            <a id="FirstEnable" data-rel="tooltip" title="Enable Edit" href="#" onclick="FirstEnable()" class="green"><i class="icon-pencil bigger-130"></i></a>
                            <a id="FirstDisable" data-rel="tooltip" title="Disable Edit" href="#" onclick="FirstDisable()" class="blue" style="visibility:hidden"><i class="icon-pencil bigger-130"></i></a>
                            <asp:RequiredFieldValidator ID="rfvUserFirstName" runat="server" ControlToValidate="txtProfileFirstName" ErrorMessage="*" ForeColor="Red" ValidationGroup="SaveUPD"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Middle Name
                        </td>
                        <td>
                            <asp:TextBox ID="txtProfileMiddleName" runat="server" AutoCompleteType="Disabled" autocomplete="off" Enabled="false"></asp:TextBox>
                            <a id="MiddleEnable" data-rel="tooltip" title="Enable Edit" href="#" onclick="MiddleEnable()" class="green"><i class="icon-pencil bigger-130"></i></a>
                            <a id="MiddleDisable" data-rel="tooltip" title="Disable Edit" href="#" onclick="MiddleDisable()" class="blue" style="visibility:hidden"><i class="icon-pencil bigger-130"></i></a>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Last Name
                        </td>
                        <td>
                            <asp:TextBox ID="txtProfileLastName" runat="server" AutoCompleteType="Disabled" autocomplete="off" Enabled="false"></asp:TextBox>
                            <a id="LastEnable" data-rel="tooltip" title="Enable Edit" href="#" onclick="LastEnable()" class="green"><i class="icon-pencil bigger-130"></i></a>
                            <a id="LastDisable" data-rel="tooltip" title="Disable Edit" href="#" onclick="LastDisable()" class="blue" style="visibility:hidden"><i class="icon-pencil bigger-130"></i></a>
                            <asp:RequiredFieldValidator ID="rfvUserLastName" runat="server" ControlToValidate="txtProfileLastName" ErrorMessage="*" ForeColor="Red" ValidationGroup="SaveUPD"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">User Name
                        </td>
                        <td>
                            <asp:TextBox ID="txtProfileEmailId" runat="server" ReadOnly="True"></asp:TextBox>
                            <asp:HiddenField ID="hdnChangePasswordID" runat="server" Value='<%# Eval("UserMembershipID")%>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">User Type
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlProfileUserType" runat="server">
                                <asp:ListItem Value="Personal User" Text="Personal User"></asp:ListItem>
                                <asp:ListItem Value="Business User" Text="Business User"></asp:ListItem>
                                <asp:ListItem Value="Enterprise User" Text="Enterprise User"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                     <tr>
                        <td class="span2">Geographic location
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlProfileCountry" runat="server">
                                        <asp:ListItem Value="0" Selected="True" Text="<--Select Country-->"></asp:ListItem>
                                        <asp:ListItem Value="Afghanistan">Afghanistan</asp:ListItem>
                                        <asp:ListItem Value="Albania">Albania</asp:ListItem>
                                        <asp:ListItem Value="Algeria">Algeria</asp:ListItem>
                                        <asp:ListItem Value="American Samoa">American Samoa</asp:ListItem>
                                        <asp:ListItem Value="Andorra">Andorra</asp:ListItem>
                                        <asp:ListItem Value="Angola">Angola</asp:ListItem>
                                        <asp:ListItem Value="Anguilla">Anguilla</asp:ListItem>
                                        <asp:ListItem Value="Antarctica">Antarctica</asp:ListItem>
                                        <asp:ListItem Value="Antigua And Barbuda">Antigua And Barbuda</asp:ListItem>
                                        <asp:ListItem Value="Argentina">Argentina</asp:ListItem>
                                        <asp:ListItem Value="Armenia">Armenia</asp:ListItem>
                                        <asp:ListItem Value="Aruba">Aruba</asp:ListItem>
                                        <asp:ListItem Value="Australia">Australia</asp:ListItem>
                                        <asp:ListItem Value="Austria">Austria</asp:ListItem>
                                        <asp:ListItem Value="Azerbaijan">Azerbaijan</asp:ListItem>
                                        <asp:ListItem Value="Bahamas">Bahamas</asp:ListItem>
                                        <asp:ListItem Value="Bahrain">Bahrain</asp:ListItem>
                                        <asp:ListItem Value="Bangladesh">Bangladesh</asp:ListItem>
                                        <asp:ListItem Value="Barbados">Barbados</asp:ListItem>
                                        <asp:ListItem Value="Belarus">Belarus</asp:ListItem>
                                        <asp:ListItem Value="Belgium">Belgium</asp:ListItem>
                                        <asp:ListItem Value="Belize">Belize</asp:ListItem>
                                        <asp:ListItem Value="Benin">Benin</asp:ListItem>
                                        <asp:ListItem Value="Bermuda">Bermuda</asp:ListItem>
                                        <asp:ListItem Value="Bhutan">Bhutan</asp:ListItem>
                                        <asp:ListItem Value="Bolivia">Bolivia</asp:ListItem>
                                        <asp:ListItem Value="Bosnia And Herzegowina">Bosnia And Herzegowina</asp:ListItem>
                                        <asp:ListItem Value="Botswana">Botswana</asp:ListItem>
                                        <asp:ListItem Value="Bouvet Island">Bouvet Island</asp:ListItem>
                                        <asp:ListItem Value="Brazil">Brazil</asp:ListItem>
                                        <asp:ListItem Value="British Indian Ocean Territory">British Indian Ocean Territory</asp:ListItem>
                                        <asp:ListItem Value="Brunei Darussalam">Brunei Darussalam</asp:ListItem>
                                        <asp:ListItem Value="Bulgaria">Bulgaria</asp:ListItem>
                                        <asp:ListItem Value="Burkina Faso">Burkina Faso</asp:ListItem>
                                        <asp:ListItem Value="Burundi">Burundi</asp:ListItem>
                                        <asp:ListItem Value="Cambodia">Cambodia</asp:ListItem>
                                        <asp:ListItem Value="Cameroon">Cameroon</asp:ListItem>
                                        <asp:ListItem Value="Canada">Canada</asp:ListItem>
                                        <asp:ListItem Value="Cape Verde">Cape Verde</asp:ListItem>
                                        <asp:ListItem Value="Cayman Islands">Cayman Islands</asp:ListItem>
                                        <asp:ListItem Value="Central African Republic">Central African Republic</asp:ListItem>
                                        <asp:ListItem Value="Chad">Chad</asp:ListItem>
                                        <asp:ListItem Value="Chile">Chile</asp:ListItem>
                                        <asp:ListItem Value="China">China</asp:ListItem>
                                        <asp:ListItem Value="Christmas Island">Christmas Island</asp:ListItem>
                                        <asp:ListItem Value="Cocos (Keeling) Islands">Cocos (Keeling) Islands</asp:ListItem>
                                        <asp:ListItem Value="Colombia">Colombia</asp:ListItem>
                                        <asp:ListItem Value="Comoros">Comoros</asp:ListItem>
                                        <asp:ListItem Value="Congo">Congo</asp:ListItem>
                                        <asp:ListItem Value="Cook Islands">Cook Islands</asp:ListItem>
                                        <asp:ListItem Value="Costa Rica">Costa Rica</asp:ListItem>
                                        <asp:ListItem Value="Cote D'Ivoire">Cote D'Ivoire</asp:ListItem>
                                        <asp:ListItem Value="Croatia (Local Name: Hrvatska)">Croatia (Local Name: Hrvatska)</asp:ListItem>
                                        <asp:ListItem Value="Cuba">Cuba</asp:ListItem>
                                        <asp:ListItem Value="Cyprus">Cyprus</asp:ListItem>
                                        <asp:ListItem Value="Czech Republic">Czech Republic</asp:ListItem>
                                        <asp:ListItem Value="Denmark">Denmark</asp:ListItem>
                                        <asp:ListItem Value="Djibouti">Djibouti</asp:ListItem>
                                        <asp:ListItem Value="Dominica">Dominica</asp:ListItem>
                                        <asp:ListItem Value="Dominican Republic">Dominican Republic</asp:ListItem>
                                        <asp:ListItem Value="East Timor">East Timor</asp:ListItem>
                                        <asp:ListItem Value="Ecuador">Ecuador</asp:ListItem>
                                        <asp:ListItem Value="Egypt">Egypt</asp:ListItem>
                                        <asp:ListItem Value="El Salvador">El Salvador</asp:ListItem>
                                        <asp:ListItem Value="Equatorial Guinea">Equatorial Guinea</asp:ListItem>
                                        <asp:ListItem Value="Eritrea">Eritrea</asp:ListItem>
                                        <asp:ListItem Value="Estonia">Estonia</asp:ListItem>
                                        <asp:ListItem Value="Ethiopia">Ethiopia</asp:ListItem>
                                        <asp:ListItem Value="Falkland Islands (Malvinas)">Falkland Islands (Malvinas)</asp:ListItem>
                                        <asp:ListItem Value="Faroe Islands">Faroe Islands</asp:ListItem>
                                        <asp:ListItem Value="Fiji">Fiji</asp:ListItem>
                                        <asp:ListItem Value="Finland">Finland</asp:ListItem>
                                        <asp:ListItem Value="France">France</asp:ListItem>
                                        <asp:ListItem Value="French Guiana">French Guiana</asp:ListItem>
                                        <asp:ListItem Value="French Polynesia">French Polynesia</asp:ListItem>
                                        <asp:ListItem Value="French Southern Territories">French Southern Territories</asp:ListItem>
                                        <asp:ListItem Value="Gabon">Gabon</asp:ListItem>
                                        <asp:ListItem Value="Gambia">Gambia</asp:ListItem>
                                        <asp:ListItem Value="Georgia">Georgia</asp:ListItem>
                                        <asp:ListItem Value="Germany">Germany</asp:ListItem>
                                        <asp:ListItem Value="Ghana">Ghana</asp:ListItem>
                                        <asp:ListItem Value="Gibraltar">Gibraltar</asp:ListItem>
                                        <asp:ListItem Value="Greece">Greece</asp:ListItem>
                                        <asp:ListItem Value="Greenland">Greenland</asp:ListItem>
                                        <asp:ListItem Value="Grenada">Grenada</asp:ListItem>
                                        <asp:ListItem Value="Guadeloupe">Guadeloupe</asp:ListItem>
                                        <asp:ListItem Value="Guam">Guam</asp:ListItem>
                                        <asp:ListItem Value="Guatemala">Guatemala</asp:ListItem>
                                        <asp:ListItem Value="Guinea">Guinea</asp:ListItem>
                                        <asp:ListItem Value="Guinea-Bissau">Guinea-Bissau</asp:ListItem>
                                        <asp:ListItem Value="Guyana">Guyana</asp:ListItem>
                                        <asp:ListItem Value="Haiti">Haiti</asp:ListItem>
                                        <asp:ListItem Value="Heard And Mc Donald Islands">Heard And Mc Donald Islands</asp:ListItem>
                                        <asp:ListItem Value="Holy See (Vatican City State)">Holy See (Vatican City State)</asp:ListItem>
                                        <asp:ListItem Value="Honduras">Honduras</asp:ListItem>
                                        <asp:ListItem Value="Hong Kong">Hong Kong</asp:ListItem>
                                        <asp:ListItem Value="Hungary">Hungary</asp:ListItem>
                                        <asp:ListItem Value="Icel And">Icel And</asp:ListItem>
                                        <asp:ListItem Value="India">India</asp:ListItem>
                                        <asp:ListItem Value="Indonesia">Indonesia</asp:ListItem>
                                        <asp:ListItem Value="Iran (Islamic Republic Of)">Iran (Islamic Republic Of)</asp:ListItem>
                                        <asp:ListItem Value="Iraq">Iraq</asp:ListItem>
                                        <asp:ListItem Value="Ireland">Ireland</asp:ListItem>
                                        <asp:ListItem Value="Israel">Israel</asp:ListItem>
                                        <asp:ListItem Value="Italy">Italy</asp:ListItem>
                                        <asp:ListItem Value="Jamaica">Jamaica</asp:ListItem>
                                        <asp:ListItem Value="Japan">Japan</asp:ListItem>
                                        <asp:ListItem Value="Jordan">Jordan</asp:ListItem>
                                        <asp:ListItem Value="Kazakhstan">Kazakhstan</asp:ListItem>
                                        <asp:ListItem Value="Kenya">Kenya</asp:ListItem>
                                        <asp:ListItem Value="Kiribati">Kiribati</asp:ListItem>
                                        <asp:ListItem Value="Korea, Dem People'S Republic">Korea, Dem People'S Republic</asp:ListItem>
                                        <asp:ListItem Value="Korea, Republic Of">Korea, Republic Of</asp:ListItem>
                                        <asp:ListItem Value="Kuwait">Kuwait</asp:ListItem>
                                        <asp:ListItem Value="Kyrgyzstan">Kyrgyzstan</asp:ListItem>
                                        <asp:ListItem Value="Lao People'S Dem Republic">Lao People'S Dem Republic</asp:ListItem>
                                        <asp:ListItem Value="Latvia">Latvia</asp:ListItem>
                                        <asp:ListItem Value="Lebanon">Lebanon</asp:ListItem>
                                        <asp:ListItem Value="Lesotho">Lesotho</asp:ListItem>
                                        <asp:ListItem Value="Liberia">Liberia</asp:ListItem>
                                        <asp:ListItem Value="Libyan Arab Jamahiriya">Libyan Arab Jamahiriya</asp:ListItem>
                                        <asp:ListItem Value="Liechtenstein">Liechtenstein</asp:ListItem>
                                        <asp:ListItem Value="Lithuania">Lithuania</asp:ListItem>
                                        <asp:ListItem Value="Luxembourg">Luxembourg</asp:ListItem>
                                        <asp:ListItem Value="Macau">Macau</asp:ListItem>
                                        <asp:ListItem Value="Macedonia">Macedonia</asp:ListItem>
                                        <asp:ListItem Value="Madagascar">Madagascar</asp:ListItem>
                                        <asp:ListItem Value="Malawi">Malawi</asp:ListItem>
                                        <asp:ListItem Value="Malaysia">Malaysia</asp:ListItem>
                                        <asp:ListItem Value="Maldives">Maldives</asp:ListItem>
                                        <asp:ListItem Value="Mali">Mali</asp:ListItem>
                                        <asp:ListItem Value="Malta">Malta</asp:ListItem>
                                        <asp:ListItem Value="Marshall Islands">Marshall Islands</asp:ListItem>
                                        <asp:ListItem Value="Martinique">Martinique</asp:ListItem>
                                        <asp:ListItem Value="Mauritania">Mauritania</asp:ListItem>
                                        <asp:ListItem Value="Mauritius">Mauritius</asp:ListItem>
                                        <asp:ListItem Value="Mayotte">Mayotte</asp:ListItem>
                                        <asp:ListItem Value="Mexico">Mexico</asp:ListItem>
                                        <asp:ListItem Value="Micronesia, Federated States">Micronesia, Federated States</asp:ListItem>
                                        <asp:ListItem Value="Moldova, Republic Of">Moldova, Republic Of</asp:ListItem>
                                        <asp:ListItem Value="Monaco">Monaco</asp:ListItem>
                                        <asp:ListItem Value="Mongolia">Mongolia</asp:ListItem>
                                        <asp:ListItem Value="Montserrat">Montserrat</asp:ListItem>
                                        <asp:ListItem Value="Morocco">Morocco</asp:ListItem>
                                        <asp:ListItem Value="Mozambique">Mozambique</asp:ListItem>
                                        <asp:ListItem Value="Myanmar">Myanmar</asp:ListItem>
                                        <asp:ListItem Value="Namibia">Namibia</asp:ListItem>
                                        <asp:ListItem Value="Nauru">Nauru</asp:ListItem>
                                        <asp:ListItem Value="Nepal">Nepal</asp:ListItem>
                                        <asp:ListItem Value="Netherlands">Netherlands</asp:ListItem>
                                        <asp:ListItem Value="Netherlands Ant Illes">Netherlands Ant Illes</asp:ListItem>
                                        <asp:ListItem Value="New Caledonia">New Caledonia</asp:ListItem>
                                        <asp:ListItem Value="New Zealand">New Zealand</asp:ListItem>
                                        <asp:ListItem Value="Nicaragua">Nicaragua</asp:ListItem>
                                        <asp:ListItem Value="Niger">Niger</asp:ListItem>
                                        <asp:ListItem Value="Nigeria">Nigeria</asp:ListItem>
                                        <asp:ListItem Value="Niue">Niue</asp:ListItem>
                                        <asp:ListItem Value="Norfolk Island">Norfolk Island</asp:ListItem>
                                        <asp:ListItem Value="Northern Mariana Islands">Northern Mariana Islands</asp:ListItem>
                                        <asp:ListItem Value="Norway">Norway</asp:ListItem>
                                        <asp:ListItem Value="Oman">Oman</asp:ListItem>
                                        <asp:ListItem Value="Pakistan">Pakistan</asp:ListItem>
                                        <asp:ListItem Value="Palau">Palau</asp:ListItem>
                                        <asp:ListItem Value="Panama">Panama</asp:ListItem>
                                        <asp:ListItem Value="Papua New Guinea">Papua New Guinea</asp:ListItem>
                                        <asp:ListItem Value="Paraguay">Paraguay</asp:ListItem>
                                        <asp:ListItem Value="Peru">Peru</asp:ListItem>
                                        <asp:ListItem Value="Philippines">Philippines</asp:ListItem>
                                        <asp:ListItem Value="Pitcairn">Pitcairn</asp:ListItem>
                                        <asp:ListItem Value="Poland">Poland</asp:ListItem>
                                        <asp:ListItem Value="Portugal">Portugal</asp:ListItem>
                                        <asp:ListItem Value="Puerto Rico">Puerto Rico</asp:ListItem>
                                        <asp:ListItem Value="Qatar">Qatar</asp:ListItem>
                                        <asp:ListItem Value="Reunion">Reunion</asp:ListItem>
                                        <asp:ListItem Value="Romania">Romania</asp:ListItem>
                                        <asp:ListItem Value="Russian Federation">Russian Federation</asp:ListItem>
                                        <asp:ListItem Value="Rwanda">Rwanda</asp:ListItem>
                                        <asp:ListItem Value="Saint K Itts And Nevis">Saint K Itts And Nevis</asp:ListItem>
                                        <asp:ListItem Value="Saint Lucia">Saint Lucia</asp:ListItem>
                                        <asp:ListItem Value="Saint Vincent, The Grenadines">Saint Vincent, The Grenadines</asp:ListItem>
                                        <asp:ListItem Value="Samoa">Samoa</asp:ListItem>
                                        <asp:ListItem Value="San Marino">San Marino</asp:ListItem>
                                        <asp:ListItem Value="Sao Tome And Principe">Sao Tome And Principe</asp:ListItem>
                                        <asp:ListItem Value="Saudi Arabia">Saudi Arabia</asp:ListItem>
                                        <asp:ListItem Value="Senegal">Senegal</asp:ListItem>
                                        <asp:ListItem Value="Seychelles">Seychelles</asp:ListItem>
                                        <asp:ListItem Value="Sierra Leone">Sierra Leone</asp:ListItem>
                                        <asp:ListItem Value="Singapore">Singapore</asp:ListItem>
                                        <asp:ListItem Value="Slovakia (Slovak Republic)">Slovakia (Slovak Republic)</asp:ListItem>
                                        <asp:ListItem Value="Slovenia">Slovenia</asp:ListItem>
                                        <asp:ListItem Value="Solomon Islands">Solomon Islands</asp:ListItem>
                                        <asp:ListItem Value="Somalia">Somalia</asp:ListItem>
                                        <asp:ListItem Value="South Africa">South Africa</asp:ListItem>
                                        <asp:ListItem Value="South Georgia , S Sandwich Is.">South Georgia , S Sandwich Is.</asp:ListItem>
                                        <asp:ListItem Value="Spain">Spain</asp:ListItem>
                                        <asp:ListItem Value="Sri Lanka">Sri Lanka</asp:ListItem>
                                        <asp:ListItem Value="St. Helena">St. Helena</asp:ListItem>
                                        <asp:ListItem Value="St. Pierre And Miquelon">St. Pierre And Miquelon</asp:ListItem>
                                        <asp:ListItem Value="Sudan">Sudan</asp:ListItem>
                                        <asp:ListItem Value="Suriname">Suriname</asp:ListItem>
                                        <asp:ListItem Value="Svalbard, Jan Mayen Islands">Svalbard, Jan Mayen Islands</asp:ListItem>
                                        <asp:ListItem Value="Sw Aziland">Sw Aziland</asp:ListItem>
                                        <asp:ListItem Value="Sweden">Sweden</asp:ListItem>
                                        <asp:ListItem Value="Switzerland">Switzerland</asp:ListItem>
                                        <asp:ListItem Value="Syrian Arab Republic">Syrian Arab Republic</asp:ListItem>
                                        <asp:ListItem Value="Taiwan">Taiwan</asp:ListItem>
                                        <asp:ListItem Value="Tajikistan">Tajikistan</asp:ListItem>
                                        <asp:ListItem Value="Tanzania, United Republic Of">Tanzania, United Republic Of</asp:ListItem>
                                        <asp:ListItem Value="Thailand">Thailand</asp:ListItem>
                                        <asp:ListItem Value="Togo">Togo</asp:ListItem>
                                        <asp:ListItem Value="Tokelau">Tokelau</asp:ListItem>
                                        <asp:ListItem Value="Tonga">Tonga</asp:ListItem>
                                        <asp:ListItem Value="Trinidad And Tobago">Trinidad And Tobago</asp:ListItem>
                                        <asp:ListItem Value="Tunisia">Tunisia</asp:ListItem>
                                        <asp:ListItem Value="Turkey">Turkey</asp:ListItem>
                                        <asp:ListItem Value="Turkmenistan">Turkmenistan</asp:ListItem>
                                        <asp:ListItem Value="Turks And Caicos Islands">Turks And Caicos Islands</asp:ListItem>
                                        <asp:ListItem Value="Tuvalu">Tuvalu</asp:ListItem>
                                        <asp:ListItem Value="Uganda">Uganda</asp:ListItem>
                                        <asp:ListItem Value="Ukraine">Ukraine</asp:ListItem>
                                        <asp:ListItem Value="United Arab Emirates">United Arab Emirates</asp:ListItem>
                                        <asp:ListItem Value="United Kingdom">United Kingdom</asp:ListItem>
                                        <asp:ListItem Value="United States">United States</asp:ListItem>
                                        <asp:ListItem Value="United States Minor Is.">United States Minor Is</asp:ListItem>
                                        <asp:ListItem Value="Uruguay">Uruguay</asp:ListItem>
                                        <asp:ListItem Value="Uzbekistan">Uzbekistan</asp:ListItem>
                                        <asp:ListItem Value="Vanuatu">Vanuatu</asp:ListItem>
                                        <asp:ListItem Value="Venezuela">Venezuela</asp:ListItem>
                                        <asp:ListItem Value="Viet Nam">Viet Nam</asp:ListItem>
                                        <asp:ListItem Value="Virgin Islands (British)">Virgin Islands (British)</asp:ListItem>
                                        <asp:ListItem Value="Virgin Islands (U.S.)">Virgin Islands (U.S.)</asp:ListItem>
                                        <asp:ListItem Value="Wallis And Futuna Islands">Wallis And Futuna Islands</asp:ListItem>
                                        <asp:ListItem Value="Western Sahara">Western Sahara</asp:ListItem>
                                        <asp:ListItem Value="Yemen">Yemen</asp:ListItem>
                                        <asp:ListItem Value="Zaire">Zaire</asp:ListItem>
                                        <asp:ListItem Value="Zambia">Zambia</asp:ListItem>
                                        <asp:ListItem Value="Zimbabwe">Zimbabwe</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvCNO2" runat="server" ControlToValidate="ddlProfileCountry" InitialValue="0" ErrorMessage="*" ForeColor="Red" ValidationGroup="SaveUPD"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                     <tr>
                        <td class="span2">Contact Number
                        </td>
                        <td>
                            <asp:TextBox ID="txtProfileContactNumber" runat="server" AutoCompleteType="Disabled" autocomplete="off" Enabled="false"></asp:TextBox>
                            <a id="ContactEnable" data-rel="tooltip" title="Enable Edit" href="#" onclick="ContactEnable()" class="green"><i class="icon-pencil bigger-130"></i></a>
                            <a id="ContactDisable" data-rel="tooltip" title="Disable Edit" href="#" onclick="ContactDisable()" class="blue" style="visibility:hidden"><i class="icon-pencil bigger-130"></i></a>
                            <asp:RequiredFieldValidator ID="rfvCNO1" runat="server" ControlToValidate="txtProfileContactNumber" ErrorMessage="*" ForeColor="Red" ValidationGroup="SaveUPD"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtProfileContactNumber" ErrorMessage="Enter Number Only" ForeColor="Red"
                                    ValidationExpression="^\+?[0-9 ]+$" ValidationGroup="SaveUPD"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                     <tr>
                        <td class="span2">Phone Number (Official)
                        </td>
                        <td>
                            <asp:TextBox ID="txtProfilePhoneNumber" runat="server" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtProfilePhoneNumber" ErrorMessage="Enter Number Only" ForeColor="Red"
                                ValidationExpression="^\+?[0-9 ]+$" ValidationGroup="SaveUPD"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                     <tr>
                        <td class="span2">Mobile Number (Personal)
                        </td>
                        <td>
                            <asp:TextBox ID="txtProfileMobileNumber" runat="server" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtProfileMobileNumber" ErrorMessage="Enter Number Only" ForeColor="Red"
                                ValidationExpression="^\+?[0-9 ]+$" ValidationGroup="SaveUPD"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                    <td class="span2">Mailing Address
                        </td>
                        <td>
                            <asp:TextBox ID="txtProfileMailingAddress" runat="server" TextMode="MultiLine" Height="100px" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                    <td class="span2">Company Name
                        </td>
                        <td>
                            <asp:TextBox ID="txtProfileCompanyName" runat="server" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Heard about us via
                        </td>
                        <td>
                            <asp:Label ID="lblHearAboutUs" runat="server" Text="Label" Style="font-weight: bold"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="span2">
                        </td>
                        <td>
                            <asp:Button runat="server" ID="btnProfileSave" OnClick="btnProfileSave_Click" Text="Save" ValidationGroup="SaveUPD" class="btn btn-primary" />
                        </td>
                    </tr>
                    <asp:Panel ID="pnlButtonPassword" runat="server" Visible="true">
                        <tr>
                            <td class="span2"></td>
                            <td>
                                <asp:Button ID="btnChangePassword" runat="server" Text="Change Password" class="btn btn-warning" OnClick="btnChangePassword_Click" />
                            </td>
                        </tr>
                    </asp:Panel>
                </table>
                <asp:Panel ID="pnlPassword" runat="server" Visible="false">
                    <div class="table-header">
                        Change Password
                    </div>
                    <table class="table table-hover dataTable table-bordered ">
                        <tr>
                            <td class="span4">New Password
                            </td>
                            <td>
                                <asp:TextBox ID="txtNewPassword" runat="server" ValidationGroup="Password"
                                    TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ErrorMessage="Enter New Password" ControlToValidate="txtNewPassword" Text="*" ValidationGroup="Password" ForeColor="Red">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="span4">Confirm Password
                            </td>
                            <td>
                                <asp:TextBox ID="txtConfirmPassword" runat="server" ValidationGroup="Password"
                                    TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ErrorMessage="Enter Confirm Password" ControlToValidate="txtConfirmPassword" Text="*" ValidationGroup="Password" ForeColor="Red">
                                </asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="cvPassword" runat="server" ErrorMessage="The Password you entered is not same." ControlToValidate="txtConfirmPassword" ControlToCompare="txtNewPassword" ValidationGroup="Password" ForeColor="Red"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="span4"></td>
                            <td>
                                <asp:Button ID="btnPasswordSave" runat="server" Text="Save Password" class="btn btn-primary" OnClick="btnPasswordSave_Click" ValidationGroup="Password" />
                                <asp:Button ID="btnPasswordCancel" runat="server" Text="Cancel" class="btn btn-warning" OnClick="btnPasswordCancel_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <div class="table-header">
                    User Survey Response
                </div>
                <asp:Panel ID="pnlUserSurvey" runat="server">
                    <table class="table table-hover dataTable table-bordered ">
                        <thead>
                            <tr role="row">
                                <th role="columnheader" style="text-align: center">Survey Question</th>
                                <th role="columnheader" style="text-align: center">Survey Response</th>
                            </tr>
                        </thead>
                        <tbody role="alert">
                            <asp:Repeater ID="rptrUserSurvey" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("SurveyQuestion") %></td>
                                        <td><%#Eval("SurveyAnswer") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlNoUserSurvey" runat="server">
                    <div class="alert alert-warning">
                        <button type="button" class="close" data-dismiss="alert">
                            <i class="icon-remove"></i>
                        </button>
                        <strong>There are no User survey respone.
                        </strong>
                        <br />
                    </div>
                </asp:Panel>
            </div>
            </div>
            </div>
        </asp:View>
        <asp:View ID="vwEmail" runat="server">
            <div class="row-fluid">
                <div>
                    <a href="UserList.aspx">&lt;&lt; Back to User list</a>
                </div>
                <div>
                    <span class="error">Recipients:</span><br />
                    <asp:TextBox ID="txtEmailIDs" ValidationGroup="EmailSend" runat="server" CssClass="span10"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEmailIDs" runat="server" ValidationGroup="EmailSend"
                        ErrorMessage="*" CssClass="aster" ControlToValidate="txtEmailIDs" ForeColor="red"></asp:RequiredFieldValidator>
                </div>
                <div>
                    <span class="error">Subject:</span><br />
                    <asp:TextBox ID="txtSubject" ValidationGroup="EmailSend" runat="server" CssClass="span10"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvSubject" runat="server" ValidationGroup="EmailSend"
                        ErrorMessage="*" CssClass="aster" ControlToValidate="txtSubject" ForeColor="red"></asp:RequiredFieldValidator>
                </div>
                <div>
                    <span class="error">Message:</span>
                </div>
                <div>
                    <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" CssClass="span10" Rows="10" ValidationGroup="EmailSend"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvMessage" runat="server" ValidationGroup="EmailSend"
                        ErrorMessage="*" CssClass="aster" ControlToValidate="txtMessage" ForeColor="red"></asp:RequiredFieldValidator>
                </div>
                <div>
                    <asp:Button ID="btnSendEmail" runat="server" Text="Send Email" ValidationGroup="EmailSend" CssClass="btn btn-primary" OnClick="btnSendEmail_Click" />
                    <div id="divEmailSuccess" runat="server" visible="false" class="alert alert-block alert-success">
                        <button data-dismiss="alert" class="close" type="button">
                            <i class="icon-remove"></i>
                        </button>
                        <p>
                            <i class="icon-ok"></i>
                            Email sent successfully!!!
                        </p>
                    </div>
                </div>
            </div>
        </asp:View>
        <asp:View ID="vwNewUser" runat="server">
            <div class="row-fluid">
                <div class="span12">
                <div class="row-fluid">
                <a href="UserList.aspx">&lt;&lt; Back&nbsp;</a>
                <div class="row-fluid">
                    <div id="divNewUserSuccess" runat="server" visible="false" class="alert alert-block alert-success span6">
                        <button data-dismiss="alert" class="close" type="button">
                            <i class="icon-remove"></i>
                        </button>
                        <p>
                            <i class="icon-ok"></i>
                            New User saved successfully.
                        </p>
                    </div>
                    <div id="divNewUserError" runat="server" visible="false" class="alert alert-error span6">
                        <button data-dismiss="alert" class="close" type="button">
                            <i class="icon-remove"></i>
                        </button>
                        <i class="icon-remove"></i>
                        User already exist.
                    </div>
                </div>
                <div class="table-header">
                    New User Profile
                </div>
                <table class="table table-hover dataTable table-bordered ">
                    <tr>
                        <td class="span2">First Name
                        </td>
                        <td>
                            <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtFirstName" ValidationGroup="NewUserPassword"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Middle Name
                        </td>
                        <td>
                            <asp:TextBox ID="txtMiddleName" runat="server"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="rfvMiddleName" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtMiddleName" ValidationGroup="NewUserPassword"></asp:RequiredFieldValidator>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Last Name
                        </td>
                        <td>
                            <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtLastName" ValidationGroup="NewUserPassword"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">User Type
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlUserRole" runat="server">
                                <asp:ListItem Value="Personal User" Text="Personal User"></asp:ListItem>
                                <asp:ListItem Value="Business User" Text="Business User"></asp:ListItem>
                                <asp:ListItem Value="Enterprise User" Text="Enterprise User"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                      <tr>
                        <td style="width: 30%; vertical-align: top;">Geographic location</td>
                        <td>
                            <asp:DropDownList ID="ddlCountry" runat="server">
                                <asp:ListItem Value="0" Selected="True" Text="<--Select Country-->"></asp:ListItem>
                                <asp:ListItem Value="Afghanistan">Afghanistan</asp:ListItem>
                                <asp:ListItem Value="Albania">Albania</asp:ListItem>
                                <asp:ListItem Value="Algeria">Algeria</asp:ListItem>
                                <asp:ListItem Value="American Samoa">American Samoa</asp:ListItem>
                                <asp:ListItem Value="Andorra">Andorra</asp:ListItem>
                                <asp:ListItem Value="Angola">Angola</asp:ListItem>
                                <asp:ListItem Value="Anguilla">Anguilla</asp:ListItem>
                                <asp:ListItem Value="Antarctica">Antarctica</asp:ListItem>
                                <asp:ListItem Value="Antigua And Barbuda">Antigua And Barbuda</asp:ListItem>
                                <asp:ListItem Value="Argentina">Argentina</asp:ListItem>
                                <asp:ListItem Value="Armenia">Armenia</asp:ListItem>
                                <asp:ListItem Value="Aruba">Aruba</asp:ListItem>
                                <asp:ListItem Value="Australia">Australia</asp:ListItem>
                                <asp:ListItem Value="Austria">Austria</asp:ListItem>
                                <asp:ListItem Value="Azerbaijan">Azerbaijan</asp:ListItem>
                                <asp:ListItem Value="Bahamas">Bahamas</asp:ListItem>
                                <asp:ListItem Value="Bahrain">Bahrain</asp:ListItem>
                                <asp:ListItem Value="Bangladesh">Bangladesh</asp:ListItem>
                                <asp:ListItem Value="Barbados">Barbados</asp:ListItem>
                                <asp:ListItem Value="Belarus">Belarus</asp:ListItem>
                                <asp:ListItem Value="Belgium">Belgium</asp:ListItem>
                                <asp:ListItem Value="Belize">Belize</asp:ListItem>
                                <asp:ListItem Value="Benin">Benin</asp:ListItem>
                                <asp:ListItem Value="Bermuda">Bermuda</asp:ListItem>
                                <asp:ListItem Value="Bhutan">Bhutan</asp:ListItem>
                                <asp:ListItem Value="Bolivia">Bolivia</asp:ListItem>
                                <asp:ListItem Value="Bosnia And Herzegowina">Bosnia And Herzegowina</asp:ListItem>
                                <asp:ListItem Value="Botswana">Botswana</asp:ListItem>
                                <asp:ListItem Value="Bouvet Island">Bouvet Island</asp:ListItem>
                                <asp:ListItem Value="Brazil">Brazil</asp:ListItem>
                                <asp:ListItem Value="British Indian Ocean Territory">British Indian Ocean Territory</asp:ListItem>
                                <asp:ListItem Value="Brunei Darussalam">Brunei Darussalam</asp:ListItem>
                                <asp:ListItem Value="Bulgaria">Bulgaria</asp:ListItem>
                                <asp:ListItem Value="Burkina Faso">Burkina Faso</asp:ListItem>
                                <asp:ListItem Value="Burundi">Burundi</asp:ListItem>
                                <asp:ListItem Value="Cambodia">Cambodia</asp:ListItem>
                                <asp:ListItem Value="Cameroon">Cameroon</asp:ListItem>
                                <asp:ListItem Value="Canada">Canada</asp:ListItem>
                                <asp:ListItem Value="Cape Verde">Cape Verde</asp:ListItem>
                                <asp:ListItem Value="Cayman Islands">Cayman Islands</asp:ListItem>
                                <asp:ListItem Value="Central African Republic">Central African Republic</asp:ListItem>
                                <asp:ListItem Value="Chad">Chad</asp:ListItem>
                                <asp:ListItem Value="Chile">Chile</asp:ListItem>
                                <asp:ListItem Value="China">China</asp:ListItem>
                                <asp:ListItem Value="Christmas Island">Christmas Island</asp:ListItem>
                                <asp:ListItem Value="Cocos (Keeling) Islands">Cocos (Keeling) Islands</asp:ListItem>
                                <asp:ListItem Value="Colombia">Colombia</asp:ListItem>
                                <asp:ListItem Value="Comoros">Comoros</asp:ListItem>
                                <asp:ListItem Value="Congo">Congo</asp:ListItem>
                                <asp:ListItem Value="Cook Islands">Cook Islands</asp:ListItem>
                                <asp:ListItem Value="Costa Rica">Costa Rica</asp:ListItem>
                                <asp:ListItem Value="Cote D'Ivoire">Cote D'Ivoire</asp:ListItem>
                                <asp:ListItem Value="Croatia (Local Name: Hrvatska)">Croatia (Local Name: Hrvatska)</asp:ListItem>
                                <asp:ListItem Value="Cuba">Cuba</asp:ListItem>
                                <asp:ListItem Value="Cyprus">Cyprus</asp:ListItem>
                                <asp:ListItem Value="Czech Republic">Czech Republic</asp:ListItem>
                                <asp:ListItem Value="Denmark">Denmark</asp:ListItem>
                                <asp:ListItem Value="Djibouti">Djibouti</asp:ListItem>
                                <asp:ListItem Value="Dominica">Dominica</asp:ListItem>
                                <asp:ListItem Value="Dominican Republic">Dominican Republic</asp:ListItem>
                                <asp:ListItem Value="East Timor">East Timor</asp:ListItem>
                                <asp:ListItem Value="Ecuador">Ecuador</asp:ListItem>
                                <asp:ListItem Value="Egypt">Egypt</asp:ListItem>
                                <asp:ListItem Value="El Salvador">El Salvador</asp:ListItem>
                                <asp:ListItem Value="Equatorial Guinea">Equatorial Guinea</asp:ListItem>
                                <asp:ListItem Value="Eritrea">Eritrea</asp:ListItem>
                                <asp:ListItem Value="Estonia">Estonia</asp:ListItem>
                                <asp:ListItem Value="Ethiopia">Ethiopia</asp:ListItem>
                                <asp:ListItem Value="Falkland Islands (Malvinas)">Falkland Islands (Malvinas)</asp:ListItem>
                                <asp:ListItem Value="Faroe Islands">Faroe Islands</asp:ListItem>
                                <asp:ListItem Value="Fiji">Fiji</asp:ListItem>
                                <asp:ListItem Value="Finland">Finland</asp:ListItem>
                                <asp:ListItem Value="France">France</asp:ListItem>
                                <asp:ListItem Value="French Guiana">French Guiana</asp:ListItem>
                                <asp:ListItem Value="French Polynesia">French Polynesia</asp:ListItem>
                                <asp:ListItem Value="French Southern Territories">French Southern Territories</asp:ListItem>
                                <asp:ListItem Value="Gabon">Gabon</asp:ListItem>
                                <asp:ListItem Value="Gambia">Gambia</asp:ListItem>
                                <asp:ListItem Value="Georgia">Georgia</asp:ListItem>
                                <asp:ListItem Value="Germany">Germany</asp:ListItem>
                                <asp:ListItem Value="Ghana">Ghana</asp:ListItem>
                                <asp:ListItem Value="Gibraltar">Gibraltar</asp:ListItem>
                                <asp:ListItem Value="Greece">Greece</asp:ListItem>
                                <asp:ListItem Value="Greenland">Greenland</asp:ListItem>
                                <asp:ListItem Value="Grenada">Grenada</asp:ListItem>
                                <asp:ListItem Value="Guadeloupe">Guadeloupe</asp:ListItem>
                                <asp:ListItem Value="Guam">Guam</asp:ListItem>
                                <asp:ListItem Value="Guatemala">Guatemala</asp:ListItem>
                                <asp:ListItem Value="Guinea">Guinea</asp:ListItem>
                                <asp:ListItem Value="Guinea-Bissau">Guinea-Bissau</asp:ListItem>
                                <asp:ListItem Value="Guyana">Guyana</asp:ListItem>
                                <asp:ListItem Value="Haiti">Haiti</asp:ListItem>
                                <asp:ListItem Value="Heard And Mc Donald Islands">Heard And Mc Donald Islands</asp:ListItem>
                                <asp:ListItem Value="Holy See (Vatican City State)">Holy See (Vatican City State)</asp:ListItem>
                                <asp:ListItem Value="Honduras">Honduras</asp:ListItem>
                                <asp:ListItem Value="Hong Kong">Hong Kong</asp:ListItem>
                                <asp:ListItem Value="Hungary">Hungary</asp:ListItem>
                                <asp:ListItem Value="Icel And">Icel And</asp:ListItem>
                                <asp:ListItem Value="India">India</asp:ListItem>
                                <asp:ListItem Value="Indonesia">Indonesia</asp:ListItem>
                                <asp:ListItem Value="Iran (Islamic Republic Of)">Iran (Islamic Republic Of)</asp:ListItem>
                                <asp:ListItem Value="Iraq">Iraq</asp:ListItem>
                                <asp:ListItem Value="Ireland">Ireland</asp:ListItem>
                                <asp:ListItem Value="Israel">Israel</asp:ListItem>
                                <asp:ListItem Value="Italy">Italy</asp:ListItem>
                                <asp:ListItem Value="Jamaica">Jamaica</asp:ListItem>
                                <asp:ListItem Value="Japan">Japan</asp:ListItem>
                                <asp:ListItem Value="Jordan">Jordan</asp:ListItem>
                                <asp:ListItem Value="Kazakhstan">Kazakhstan</asp:ListItem>
                                <asp:ListItem Value="Kenya">Kenya</asp:ListItem>
                                <asp:ListItem Value="Kiribati">Kiribati</asp:ListItem>
                                <asp:ListItem Value="Korea, Dem People'S Republic">Korea, Dem People'S Republic</asp:ListItem>
                                <asp:ListItem Value="Korea, Republic Of">Korea, Republic Of</asp:ListItem>
                                <asp:ListItem Value="Kuwait">Kuwait</asp:ListItem>
                                <asp:ListItem Value="Kyrgyzstan">Kyrgyzstan</asp:ListItem>
                                <asp:ListItem Value="Lao People'S Dem Republic">Lao People'S Dem Republic</asp:ListItem>
                                <asp:ListItem Value="Latvia">Latvia</asp:ListItem>
                                <asp:ListItem Value="Lebanon">Lebanon</asp:ListItem>
                                <asp:ListItem Value="Lesotho">Lesotho</asp:ListItem>
                                <asp:ListItem Value="Liberia">Liberia</asp:ListItem>
                                <asp:ListItem Value="Libyan Arab Jamahiriya">Libyan Arab Jamahiriya</asp:ListItem>
                                <asp:ListItem Value="Liechtenstein">Liechtenstein</asp:ListItem>
                                <asp:ListItem Value="Lithuania">Lithuania</asp:ListItem>
                                <asp:ListItem Value="Luxembourg">Luxembourg</asp:ListItem>
                                <asp:ListItem Value="Macau">Macau</asp:ListItem>
                                <asp:ListItem Value="Macedonia">Macedonia</asp:ListItem>
                                <asp:ListItem Value="Madagascar">Madagascar</asp:ListItem>
                                <asp:ListItem Value="Malawi">Malawi</asp:ListItem>
                                <asp:ListItem Value="Malaysia">Malaysia</asp:ListItem>
                                <asp:ListItem Value="Maldives">Maldives</asp:ListItem>
                                <asp:ListItem Value="Mali">Mali</asp:ListItem>
                                <asp:ListItem Value="Malta">Malta</asp:ListItem>
                                <asp:ListItem Value="Marshall Islands">Marshall Islands</asp:ListItem>
                                <asp:ListItem Value="Martinique">Martinique</asp:ListItem>
                                <asp:ListItem Value="Mauritania">Mauritania</asp:ListItem>
                                <asp:ListItem Value="Mauritius">Mauritius</asp:ListItem>
                                <asp:ListItem Value="Mayotte">Mayotte</asp:ListItem>
                                <asp:ListItem Value="Mexico">Mexico</asp:ListItem>
                                <asp:ListItem Value="Micronesia, Federated States">Micronesia, Federated States</asp:ListItem>
                                <asp:ListItem Value="Moldova, Republic Of">Moldova, Republic Of</asp:ListItem>
                                <asp:ListItem Value="Monaco">Monaco</asp:ListItem>
                                <asp:ListItem Value="Mongolia">Mongolia</asp:ListItem>
                                <asp:ListItem Value="Montserrat">Montserrat</asp:ListItem>
                                <asp:ListItem Value="Morocco">Morocco</asp:ListItem>
                                <asp:ListItem Value="Mozambique">Mozambique</asp:ListItem>
                                <asp:ListItem Value="Myanmar">Myanmar</asp:ListItem>
                                <asp:ListItem Value="Namibia">Namibia</asp:ListItem>
                                <asp:ListItem Value="Nauru">Nauru</asp:ListItem>
                                <asp:ListItem Value="Nepal">Nepal</asp:ListItem>
                                <asp:ListItem Value="Netherlands">Netherlands</asp:ListItem>
                                <asp:ListItem Value="Netherlands Ant Illes">Netherlands Ant Illes</asp:ListItem>
                                <asp:ListItem Value="New Caledonia">New Caledonia</asp:ListItem>
                                <asp:ListItem Value="New Zealand">New Zealand</asp:ListItem>
                                <asp:ListItem Value="Nicaragua">Nicaragua</asp:ListItem>
                                <asp:ListItem Value="Niger">Niger</asp:ListItem>
                                <asp:ListItem Value="Nigeria">Nigeria</asp:ListItem>
                                <asp:ListItem Value="Niue">Niue</asp:ListItem>
                                <asp:ListItem Value="Norfolk Island">Norfolk Island</asp:ListItem>
                                <asp:ListItem Value="Northern Mariana Islands">Northern Mariana Islands</asp:ListItem>
                                <asp:ListItem Value="Norway">Norway</asp:ListItem>
                                <asp:ListItem Value="Oman">Oman</asp:ListItem>
                                <asp:ListItem Value="Pakistan">Pakistan</asp:ListItem>
                                <asp:ListItem Value="Palau">Palau</asp:ListItem>
                                <asp:ListItem Value="Panama">Panama</asp:ListItem>
                                <asp:ListItem Value="Papua New Guinea">Papua New Guinea</asp:ListItem>
                                <asp:ListItem Value="Paraguay">Paraguay</asp:ListItem>
                                <asp:ListItem Value="Peru">Peru</asp:ListItem>
                                <asp:ListItem Value="Philippines">Philippines</asp:ListItem>
                                <asp:ListItem Value="Pitcairn">Pitcairn</asp:ListItem>
                                <asp:ListItem Value="Poland">Poland</asp:ListItem>
                                <asp:ListItem Value="Portugal">Portugal</asp:ListItem>
                                <asp:ListItem Value="Puerto Rico">Puerto Rico</asp:ListItem>
                                <asp:ListItem Value="Qatar">Qatar</asp:ListItem>
                                <asp:ListItem Value="Reunion">Reunion</asp:ListItem>
                                <asp:ListItem Value="Romania">Romania</asp:ListItem>
                                <asp:ListItem Value="Russian Federation">Russian Federation</asp:ListItem>
                                <asp:ListItem Value="Rwanda">Rwanda</asp:ListItem>
                                <asp:ListItem Value="Saint K Itts And Nevis">Saint K Itts And Nevis</asp:ListItem>
                                <asp:ListItem Value="Saint Lucia">Saint Lucia</asp:ListItem>
                                <asp:ListItem Value="Saint Vincent, The Grenadines">Saint Vincent, The Grenadines</asp:ListItem>
                                <asp:ListItem Value="Samoa">Samoa</asp:ListItem>
                                <asp:ListItem Value="San Marino">San Marino</asp:ListItem>
                                <asp:ListItem Value="Sao Tome And Principe">Sao Tome And Principe</asp:ListItem>
                                <asp:ListItem Value="Saudi Arabia">Saudi Arabia</asp:ListItem>
                                <asp:ListItem Value="Senegal">Senegal</asp:ListItem>
                                <asp:ListItem Value="Seychelles">Seychelles</asp:ListItem>
                                <asp:ListItem Value="Sierra Leone">Sierra Leone</asp:ListItem>
                                <asp:ListItem Value="Singapore">Singapore</asp:ListItem>
                                <asp:ListItem Value="Slovakia (Slovak Republic)">Slovakia (Slovak Republic)</asp:ListItem>
                                <asp:ListItem Value="Slovenia">Slovenia</asp:ListItem>
                                <asp:ListItem Value="Solomon Islands">Solomon Islands</asp:ListItem>
                                <asp:ListItem Value="Somalia">Somalia</asp:ListItem>
                                <asp:ListItem Value="South Africa">South Africa</asp:ListItem>
                                <asp:ListItem Value="South Georgia , S Sandwich Is.">South Georgia , S Sandwich Is.</asp:ListItem>
                                <asp:ListItem Value="Spain">Spain</asp:ListItem>
                                <asp:ListItem Value="Sri Lanka">Sri Lanka</asp:ListItem>
                                <asp:ListItem Value="St. Helena">St. Helena</asp:ListItem>
                                <asp:ListItem Value="St. Pierre And Miquelon">St. Pierre And Miquelon</asp:ListItem>
                                <asp:ListItem Value="Sudan">Sudan</asp:ListItem>
                                <asp:ListItem Value="Suriname">Suriname</asp:ListItem>
                                <asp:ListItem Value="Svalbard, Jan Mayen Islands">Svalbard, Jan Mayen Islands</asp:ListItem>
                                <asp:ListItem Value="Sw Aziland">Sw Aziland</asp:ListItem>
                                <asp:ListItem Value="Sweden">Sweden</asp:ListItem>
                                <asp:ListItem Value="Switzerland">Switzerland</asp:ListItem>
                                <asp:ListItem Value="Syrian Arab Republic">Syrian Arab Republic</asp:ListItem>
                                <asp:ListItem Value="Taiwan">Taiwan</asp:ListItem>
                                <asp:ListItem Value="Tajikistan">Tajikistan</asp:ListItem>
                                <asp:ListItem Value="Tanzania, United Republic Of">Tanzania, United Republic Of</asp:ListItem>
                                <asp:ListItem Value="Thailand">Thailand</asp:ListItem>
                                <asp:ListItem Value="Togo">Togo</asp:ListItem>
                                <asp:ListItem Value="Tokelau">Tokelau</asp:ListItem>
                                <asp:ListItem Value="Tonga">Tonga</asp:ListItem>
                                <asp:ListItem Value="Trinidad And Tobago">Trinidad And Tobago</asp:ListItem>
                                <asp:ListItem Value="Tunisia">Tunisia</asp:ListItem>
                                <asp:ListItem Value="Turkey">Turkey</asp:ListItem>
                                <asp:ListItem Value="Turkmenistan">Turkmenistan</asp:ListItem>
                                <asp:ListItem Value="Turks And Caicos Islands">Turks And Caicos Islands</asp:ListItem>
                                <asp:ListItem Value="Tuvalu">Tuvalu</asp:ListItem>
                                <asp:ListItem Value="Uganda">Uganda</asp:ListItem>
                                <asp:ListItem Value="Ukraine">Ukraine</asp:ListItem>
                                <asp:ListItem Value="United Arab Emirates">United Arab Emirates</asp:ListItem>
                                <asp:ListItem Value="United Kingdom">United Kingdom</asp:ListItem>
                                <asp:ListItem Value="United States">United States</asp:ListItem>
                                <asp:ListItem Value="United States Minor Is.">United States Minor Is</asp:ListItem>
                                <asp:ListItem Value="Uruguay">Uruguay</asp:ListItem>
                                <asp:ListItem Value="Uzbekistan">Uzbekistan</asp:ListItem>
                                <asp:ListItem Value="Vanuatu">Vanuatu</asp:ListItem>
                                <asp:ListItem Value="Venezuela">Venezuela</asp:ListItem>
                                <asp:ListItem Value="Viet Nam">Viet Nam</asp:ListItem>
                                <asp:ListItem Value="Virgin Islands (British)">Virgin Islands (British)</asp:ListItem>
                                <asp:ListItem Value="Virgin Islands (U.S.)">Virgin Islands (U.S.)</asp:ListItem>
                                <asp:ListItem Value="Wallis And Futuna Islands">Wallis And Futuna Islands</asp:ListItem>
                                <asp:ListItem Value="Western Sahara">Western Sahara</asp:ListItem>
                                <asp:ListItem Value="Yemen">Yemen</asp:ListItem>
                                <asp:ListItem Value="Zaire">Zaire</asp:ListItem>
                                <asp:ListItem Value="Zambia">Zambia</asp:ListItem>
                                <asp:ListItem Value="Zimbabwe">Zimbabwe</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfvCountry" ControlToValidate="ddlCountry" InitialValue="0" ForeColor="Red" ErrorMessage="Please select country" ValidationGroup="NewUserPassword"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Email
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmail1" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtEmail1" ValidationGroup="NewUserPassword"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Invalid Email format" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ControlToValidate="txtEmail1" ValidationGroup="NewUserPassword" ForeColor="Red"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Contact Number
                        </td>
                        <td>
                            <asp:TextBox ID="txtContactNumber" runat="server" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtContactNumber" ValidationGroup="NewUserPassword"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revContactNumber" runat="server" ControlToValidate="txtContactNumber" ErrorMessage="Enter Number Only" ForeColor="Red"
                                                             ValidationExpression="^\+?[0-9 ]+$" ValidationGroup="NewUserPassword"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                     <tr>
                        <td class="span2">Phone Number (Official)
                        </td>
                        <td>
                            <asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPhoneNumber" ErrorMessage="Enter Number Only" ForeColor="Red"
                                                             ValidationExpression="^\+?[0-9 ]+$" ValidationGroup="NewUserPassword"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                     <tr>
                        <td class="span2">Mobile Number (Personal)
                        </td>
                        <td>
                            <asp:TextBox ID="txtMobileNumber" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtMobileNumber" ErrorMessage="Enter Number Only" ForeColor="Red"
                                                             ValidationExpression="^\+?[0-9 ]+$" ValidationGroup="NewUserPassword"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                      <tr>
                        <td class="span2">Mailinig Address
                        </td>
                        <td>
                            <asp:TextBox ID="txtMailingAddress" runat="server" TextMode="MultiLine" Height="100px" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="span2">Company Name
                        </td>
                        <td>
                            <asp:TextBox ID="txtCompanyName" runat="server" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="span4">Password
                        </td>
                        <td>
                            <asp:TextBox ID="txtNewUserPassword1" runat="server"
                                ValidationGroup="NewUserPassword" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNewUserPassword" runat="server" ErrorMessage="Enter Password" ControlToValidate="txtNewUserPassword1" Text="*" ValidationGroup="NewUserPassword" ForeColor="Red">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="span4">Confirm Password
                        </td>
                        <td>
                            <asp:TextBox ID="txtNewUserConfirmPassword" runat="server"
                                ValidationGroup="NewUserPassword" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNewUserConfirmPassword" runat="server" ErrorMessage="Enter Confirm Password" ControlToValidate="txtNewUserConfirmPassword" Text="*" ValidationGroup="NewUserPassword" ForeColor="Red">
                            </asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cvNewUserConfirmPassword" runat="server" ErrorMessage="The Password you entered is not same." ControlToValidate="txtNewUserConfirmPassword" ControlToCompare="txtNewUserPassword1" ValidationGroup="NewUserPassword" ForeColor="Red"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="btnNewUserSave" runat="server" Text="Save" class="btn btn-primary" ValidationGroup="NewUserPassword" OnClick="btnNewUserSave_Click" />
                            <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-warning" OnClick="btnReset_Click" />
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
            $('#tblUsersList').dataTable(
                {
                    "aoColumnDefs": [
                        { 'bSortable': false, 'aTargets': [0, 5] }
                    ]
                }
         );
        });
    </script>
    <script type="text/javascript">
        function ConfirmAccessAccount() {
            if (confirm("Are you sure want to access this user's account?") == true)
                return true;
            else
                return false;
        }
    </script>
    <script type="text/javascript">
        function FirstEnable() {
            document.getElementById("ContentPlaceHolder1_txtProfileFirstName").disabled = false;
            document.getElementById("FirstEnable").style.visibility = 'hidden';
            document.getElementById("FirstDisable").style.visibility = 'visible';
        }
        function FirstDisable() {
            document.getElementById("ContentPlaceHolder1_txtProfileFirstName").disabled = true;
            document.getElementById("FirstEnable").style.visibility = 'visible';
            document.getElementById("FirstDisable").style.visibility = 'hidden';
        }

        function MiddleEnable() {
            document.getElementById("ContentPlaceHolder1_txtProfileMiddleName").disabled = false;
            document.getElementById("MiddleEnable").style.visibility = 'hidden';
            document.getElementById("MiddleDisable").style.visibility = 'visible';
        }
        function MiddleDisable() {
            document.getElementById("ContentPlaceHolder1_txtProfileMiddleName").disabled = true;
            document.getElementById("MiddleEnable").style.visibility = 'visible';
            document.getElementById("MiddleDisable").style.visibility = 'hidden';
        }

        function LastEnable() {
            document.getElementById("ContentPlaceHolder1_txtProfileLastName").disabled = false;
            document.getElementById("LastEnable").style.visibility = 'hidden';
            document.getElementById("LastDisable").style.visibility = 'visible';
        }
        function LastDisable() {
            document.getElementById("ContentPlaceHolder1_txtProfileLastName").disabled = true;
            document.getElementById("LastEnable").style.visibility = 'visible';
            document.getElementById("LastDisable").style.visibility = 'hidden';
        }

        function ContactEnable() {
            document.getElementById("ContentPlaceHolder1_txtProfileContactNumber").disabled = false;
            document.getElementById("ContactEnable").style.visibility = 'hidden';
            document.getElementById("ContactDisable").style.visibility = 'visible';
        }
        function ContactDisable() {
            document.getElementById("ContentPlaceHolder1_txtProfileContactNumber").disabled = true;
            document.getElementById("ContactEnable").style.visibility = 'visible';
            document.getElementById("ContactDisable").style.visibility = 'hidden';
        }
    </script>
</asp:Content>

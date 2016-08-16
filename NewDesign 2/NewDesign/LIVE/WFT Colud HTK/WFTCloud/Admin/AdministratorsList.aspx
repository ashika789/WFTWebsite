<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminPages.Master" AutoEventWireup="true" CodeBehind="AdministratorsList.aspx.cs" Inherits="WFTCloud.Admin.AdministratorsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Administrators List</title>
    <meta name="description" content="List of administrators can be seen here." />
    <meta name="keywords" content="WFT administrators" />
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
                                Admin changes saved successfully!!!
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
                            Administrators	        
                        </div>
                        <div class="dataTables_wrapper">
                            <div role="grid" class="dataTables_wrapper">
                                <table id="tblAdministrators" class="table table-striped table-bordered table-hover dataTable">
                                    <thead>
                                        <tr role="row">
                                            <th class="center" role="columnheader">
                                                <label>
                                                    <input type="checkbox" class="ace" />
                                                    <span class="lbl"></span>
                                                </label>
                                            </th>
                                            <th role="columnheader">First Name</th>
                                            <th role="columnheader">LastName</th>
                                            <th role="columnheader">Email</th>
                                            <th role="columnheader">Status</th>
                                            <th role="columnheader" style="text-align: center;">Options</th>
                                        </tr>
                                    </thead>
                                    <tbody role="alert">
                                        <asp:Repeater ID="rptrUserProfiles" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="center">
                                                        <asp:CheckBox ID="chkSelect" runat="server" class="ace" />
                                                    </td>
                                                    <td>
                                                        <a href='AdministratorsList.aspx?edituser=<%# Eval("UserProfileID")%>'>
                                                            <%# Eval("FirstName")%>
                                                        </a>
                                                        <asp:HiddenField ID="hdnUserProfileID" runat="server" Value='<%# Eval("UserProfileID")%>' />
                                                    </td>
                                                    <td><%# Eval("LastName")%></td>
                                                    <td><%# Eval("EmailID")%></td>
                                                    <td><%# WFTCloud.GeneralReusableMethods.GetStatusString(Eval("RecordStatus").ToString()) %></td>
                                                    <td>
                                                        <div class="action-buttons">
                                                            <a data-rel="tooltip" title="Edit Admin Details" href='AdministratorsList.aspx?edituser=<%# Eval("UserProfileID")%>' class="green">
                                                                <i class="icon-pencil bigger-130"></i>
                                                            </a>
                                                            <a data-rel="tooltip" title="Activate/Deactivate" href='AdministratorsList.aspx?<%# WFTCloud.GeneralReusableMethods.GetActivateDeactivateAction(Eval("RecordStatus").ToString()) %>=<%# Eval("UserProfileID")%>' class="blue">
                                                                <i class='<%# WFTCloud.GeneralReusableMethods.GetActivateDeactivateIcon(Eval("RecordStatus").ToString()) %>'></i>
                                                            </a>
                                                            <a data-rel="tooltip" title="Change Password" href='AdministratorsList.aspx?showview=changepassword&userprofileid=<%# Eval("UserProfileID")%>' class="blue">
                                                                <i class="icon-key bigger-130"></i>
                                                            </a>
                                                            <a data-rel="tooltip" title="Grant/Revoke Page Access" href='AdministratorsList.aspx?showview=pageaccess&userprofileid=<%# Eval("UserProfileID")%>' class="blue">
                                                                <i class="icon-desktop bigger-130"></i>
                                                            </a>
                                                            <%-- <a data-rel="tooltip" title="Delete Admin" href='AdministratorsList.aspx?delete=<%# Eval("UserProfileID")%>' class="red">
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
                                class="btn btn-primary" OnClick="btnAddNew_Click" />
                            <asp:Button ID="btnActivate" runat="server" Text="Activate"
                                class="btn btn-info" OnClick="btnActivate_Click" />
                            <asp:Button ID="btnDeactivate" runat="server" Text="Deactivate"
                                class="btn btn-warning" OnClick="btnDeactivate_Click" />
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
                        <a href="AdministratorsList.aspx">&lt;&lt; Back&nbsp;</a>
                        <div class="row-fluid">
                            <div id="lblSuccessMessage" runat="server" visible="false" class="alert alert-block alert-success span6">
                                <button data-dismiss="alert" class="close" type="button">
                                    <i class="icon-remove"></i>
                                </button>
                                <p>
                                    <i class="icon-ok"></i>
                                     Admin changes saved successfully!!!
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
                            <asp:Label ID="lblAdminHeader" runat="server" Text="Edit"></asp:Label>
                        </div>
                        <table class="table table-hover dataTable table-bordered ">
                            <tr>
                                <td class="span2"></td>
                                <td>
                                    <asp:HiddenField ID="hdnUserProfileID" runat="server" Value="0" />
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">First Name
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server"
                                        ErrorMessage="First Name" ControlToValidate="txtFirstName" ValidationGroup="Admin"
                                        Text="*" ForeColor="Red">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Middle Name
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMiddleName" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Last Name
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvLastName" runat="server"
                                        ErrorMessage="Last Name" ControlToValidate="txtLastName" ValidationGroup="Admin"
                                        Text="*" ForeColor="Red">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Email ID
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEmailID" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvEmailID" runat="server"
                                        ErrorMessage="Email ID" ControlToValidate="txtEmailID" ValidationGroup="Admin"
                                        Text="*" ForeColor="Red">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revEmailID" runat="server" ForeColor="Red" ErrorMessage="Enter Valid Mailid" ControlToValidate="txtEmailID"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Admin"></asp:RegularExpressionValidator>
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
                                    <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ErrorMessage="Please select country" ControlToValidate="ddlCountry" InitialValue="0" ForeColor="Red" ValidationGroup="Admin" ></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">User Role
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlUserRoles" runat="server">
                                        <asp:ListItem Text="Administrator" Value="Admin" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Super Admin" Value="Super Admin"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Contact Number
                                </td>
                                <td>
                                    <asp:TextBox ID="txtContactNumber" runat="server" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtContactNumber" ValidationGroup="Admin"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revContactNumber" runat="server" ControlToValidate="txtContactNumber" ErrorMessage="Enter Number Only" ForeColor="Red"
                                                                     ValidationExpression="^\+?[0-9 ]+$" ValidationGroup="Admin"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                             <tr>
                                <td class="span2">Phone Number (Official)
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPhoneNumber" ErrorMessage="Enter Number Only" ForeColor="Red"
                                                                     ValidationExpression="^\+?[0-9 ]+$" ValidationGroup="Admin"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                             <tr>
                                <td class="span2">Mobile Number (Personal)
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMobileNumber" runat="server"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtMobileNumber" ErrorMessage="Enter Number Only" ForeColor="Red"
                                                                     ValidationExpression="^\+?[0-9 ]+$" ValidationGroup="Admin"></asp:RegularExpressionValidator>
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
                                <td class="span2">User Status
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlRecordStatus" runat="server">
                                        <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="Inactive"></asp:ListItem>
                                        <asp:ListItem Value="-1" Text="Deleted"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2"></td>
                                <td>
                                    <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary" ValidationGroup="Admin"
                                        OnClick="btnSave_Click" />
                                    <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-warning"
                                        OnClick="btnReset_Click" />

                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </asp:View>
        <asp:View ID="vwChangePassword" runat="server">
            <div class="row-fluid">
                <div class="span12">
                    <div class="row-fluid">
                        <a href="AdministratorsList.aspx">&lt;&lt; Back&nbsp;</a>
                        <div class="row-fluid">
                            <div id="divCPSucess" runat="server" visible="false" class="alert alert-block alert-success span6">
                                <button data-dismiss="alert" class="close" type="button">
                                    <i class="icon-remove"></i>
                                </button>
                                <p>
                                    <i class="icon-ok"></i>
                                    Password Changed Successfully
                                </p>
                            </div>
                            <div id="divCPError" runat="server" visible="false" class="alert alert-error span6">
                                <button data-dismiss="alert" class="close" type="button">
                                    <i class="icon-remove"></i>
                                </button>
                                <i class="icon-remove"></i>
                                Error changing password. Please try later.
                            </div>
                        </div>
                        <div class="table-header">
                            Change Password
                        </div>
                        <table class="table table-hover dataTable table-bordered ">
                            <tr>
                                <td class="span4">First Name
                                </td>
                                <td>
                                    <asp:Label ID="lblCPFirstName" runat="server" Style="font-weight: bold"></asp:Label>
                                    <asp:HiddenField ID="hdnCPUserProfileID" runat="server" Value="0" />
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Middle Name
                                </td>
                                <td>
                                    <asp:Label ID="lblCPMiddleName" runat="server" Style="font-weight: bold"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Last Name
                                </td>
                                <td>
                                    <asp:Label ID="lblCPLastName" runat="server" Style="font-weight: bold"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Email ID
                                </td>
                                <td>
                                    <asp:Label ID="lblCPEmailID" runat="server" Style="font-weight: bold"></asp:Label>
                                </td>
                            </tr>
                              <tr>
                                <td class="span2">Geographic location
                                </td>
                                <td>
                                    <asp:Label ID="lblCountry" runat="server" Style="font-weight: bold"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Contact Number
                                </td>
                                <td>
                                    <asp:Label ID="lblContactNumber" runat="server" Text=" - " Style="font-weight: bold"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="span2">Phone Number (Official)
                                </td>
                                <td>
                                <asp:Label ID="lblPhoneNumber" runat="server" Text=" - " Style="font-weight: bold"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="span2">Mobile Number (Personal)
                                </td>
                                <td>
                                <asp:Label ID="lblMobileNumber" runat="server" Text=" - " Style="font-weight: bold"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="span2">Mailing Address
                                </td>
                                <td>
                                <asp:Label ID="lblMailingAddress" runat="server" Text=" - " Style="font-weight: bold"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="span2">Company Name
                                </td>
                                <td>
                                <asp:Label ID="lblCompanyName" runat="server" Text=" - " Style="font-weight: bold"></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="span2">Password
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ValidationGroup="Password"
                                        ErrorMessage="Password" ControlToValidate="txtPassword"
                                        Text="*" ForeColor="Red">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Confirm Password
                                </td>
                                <td>
                                    <asp:TextBox ID="txtConfPassword" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvConfPassword" runat="server" ValidationGroup="Password"
                                        ErrorMessage="Confirm Password" ControlToValidate="txtConfPassword"
                                        Text="*" ForeColor="Red">
                                    </asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="cvConfPassword" runat="server" ValidationGroup="Password"
                                        ControlToValidate="txtConfPassword" ControlToCompare="txtPassword"
                                        ErrorMessage="Password and confirm password must match."
                                        ForeColor="Red">
                                    </asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td colspan="2">
                                    <div class="row-fluid">
                                        <asp:Button ID="btnSavePassword" runat="server" Text="Save Password" ValidationGroup="Password"
                                            class="btn btn-primary" OnClick="btnSavePassword_Click" />

                                    </div>
                                </td>
                            </tr>
                            <%--<tr>
                    <td colspan="2">
                        
                    </td>
                </tr>--%>
                        </table>
                    </div>
                </div>
            </div>
        </asp:View>
        <asp:View ID="vwPageAccess" runat="server">
            <div class="row-fluid">
                <div class="span12">
                    <div class="row-fluid">
                        <a href="AdministratorsList.aspx">&lt;&lt; Back&nbsp;</a>
                        <div class="row-fluid">
                            <div id="divPASuccess" runat="server" visible="false" class="alert alert-block alert-success span6">
                                <button data-dismiss="alert" class="close" type="button">
                                    <i class="icon-remove"></i>
                                </button>
                                <p>
                                    <i class="icon-ok"></i>
                                    Page Access saved successfully.
                                </p>
                            </div>
                            <div id="divPAError" runat="server" visible="false" class="alert alert-error span6">
                                <button data-dismiss="alert" class="close" type="button">
                                    <i class="icon-remove"></i>
                                </button>
                                <i class="icon-remove"></i>
                                Error saving page access. Please try later.
                            </div>
                        </div>
                        <div class="table-header">
                            Page Access
                        </div>
                        <table class="table table-hover dataTable table-bordered">
                            <tr>
                                <td class="span2">First Name
                                </td>
                                <td colspan="2">
                                    <asp:Label ID="lblPAFirstName" runat="server"></asp:Label>
                                    <asp:HiddenField ID="hdnPAUserProfileID" runat="server" Value="0" />
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Middle Name
                                </td>
                                <td colspan="2">
                                    <asp:Label ID="lblPAMiddleName" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Last Name
                                </td>
                                <td colspan="2">
                                    <asp:Label ID="lblPALastName" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="span2">Email ID
                                </td>
                                <td colspan="2">
                                    <asp:Label ID="lblPAEmailID" runat="server"></asp:Label>
                                </td>
                            </tr>
                             <tr>
                                <td class="span2">Geographic location
                                </td>
                                <td colspan="2">
                                    <asp:Label ID="lblAccCountry" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="span3 table-header">Pages To Give Access
                                </td>
                                <td class="span1 table-header"></td>
                                <td class="span3 table-header">Pages With Access
                                </td>
                            </tr>
                            <tr>
                                <td class="span3">
                                    <asp:ListBox ID="lbxToAccess" runat="server" SelectionMode="Multiple"
                                        Height="300px"></asp:ListBox>
                                </td>
                                <td class="span1">
                                    <asp:Button ID="btnAddToAccess" runat="server" Text=">> Grant Access" CssClass="btn btn-pink"
                                        OnClick="btnAddToAccess_Click" />
                                    <br />
                                    <br />
                                    <asp:Button ID="btnRemoveAccess" runat="server" Text="<< Remove Access" CssClass="btn btn-purple"
                                        OnClick="btnRemoveAccess_Click" />
                                </td>
                                <td class="span3">
                                    <asp:ListBox ID="lbxWithAccess" runat="server" SelectionMode="Multiple"
                                        Height="300px"></asp:ListBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <div class="row-fluid">
                                        <asp:Button ID="btnSavePageAccess" runat="server" Text="Save Page Access"
                                            class="btn btn-primary" OnClick="btnSavePageAccess_Click" />
                                        <asp:Button ID="btnResetPageAccess" runat="server" Text="Reset Page Access"
                                            class="btn btn-warning" OnClick="btnResetPageAccess_Click" />

                                    </div>
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
            $('#tblAdministrators').dataTable(
                  {
                      "aoColumnDefs": [
                          { 'bSortable': false, 'aTargets': [0, 5] }
                      ]
                  }
         );
        });
    </script>
</asp:Content>

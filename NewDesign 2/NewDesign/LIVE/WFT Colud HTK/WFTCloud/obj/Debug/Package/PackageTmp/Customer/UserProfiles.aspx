<%@ Page Title="" Language="C#" MasterPageFile="~/Customer/CustomerPages.Master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="UserProfiles.aspx.cs" Inherits="WFTCloud.Customer.UserProfiles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>User Profile</title>

    <meta name="description" content="Details about the users of WFT Cloud." />
    <meta name="keywords" content="User Profiles" />
    <style type="text/css">
        .controls {
            margin-top: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<asp:ScriptManager runat="server" ID="scrptMgr"></asp:ScriptManager>--%>
    <div class="table-header">
        <strong>User Profile Details </strong>
    </div>
    <div class="row-fluid">
        <div class="span12 widget-container-span">
            <div class="widget-box">
                <div class="widget-header no-border">
                    <ul class="nav nav-tabs" id="Ultabs">
                        <li id="liUserDetails" runat="server" class="active">
                            <a href="/Customer/UserProfiles.aspx?userid=<%=UserMembershipID %>&showview=UserDetails">User Details
                            </a>
                        </li>
                        <li id="liChangePassword" runat="server">
                            <a href="/Customer/UserProfiles.aspx?userid=<%=UserMembershipID %>&showview=ChangePassword">Change Password
                            </a>
                        </li>
                        <li id="liChangePaymentDetails" runat="server">
                            <a href="/Customer/UserProfiles.aspx?userid=<%=UserMembershipID %>&showview=ChangePaymentDetails">Change Payment Details
                            </a>
                        </li>
                        <%--<li id="liCancelSubscription" runat="server">
				<a href="/Customer/UserProfiles.aspx?userid=<%=UserMembershipID %>&showview=CancelSubscription">
					Cancel Subscription
				</a>
                        </li>--%>
                    </ul>
                </div>
                <div class="widget-body">
                    <div class="widget-main padding-12">
                        <div class="tab-content">
                            <div id="UserProfilesDetails" class="tab-pane in active" runat="server">
                                <div class="span12">
                                    <div class="row-fluid">
                                        <div>
                                            <div id="divUserSuccessMessage" runat="server" visible="false" class="alert alert-block alert-success span6">
                                                <button data-dismiss="alert" class="close" type="button">
                                                    <i class="icon-remove"></i>
                                                </button>
                                                <p>
                                                    <i class="icon-ok"></i>
                                                    <asp:Label ID="lblUserSuccess" runat="server"></asp:Label>
                                                </p>
                                            </div>
                                            <div id="divUserErrorMessage" runat="server" visible="false" class="alert alert-error span6">
                                                <button data-dismiss="alert" class="close" type="button">
                                                    <i class="icon-remove"></i>
                                                </button>
                                                <i class="icon-remove"></i>
                                                <asp:Label ID="lblUserErrorMessage" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row-fluid">
                                        <div class="table-header">
                                            User Details	        
                                        </div>
                                        <div class="dataTables_wrapper">
                                            <table class="table table-hover dataTable table-bordered " id="tblUserProfilesDetails">
                                                <tr>
                                                    <td class="span3">First Name
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFirstName" runat="server" AutoCompleteType="Disabled" autocomplete="off" Enabled="false"></asp:TextBox>
                                                        <a id="FirstEnable" data-rel="tooltip" title="Enable Edit" href="#" onclick="FirstEnable()" class="green"><i class="icon-pencil bigger-130"></i></a>
                                                        <a id="FirstDisable" data-rel="tooltip" title="Disable Edit" href="#" onclick="FirstDisable()" class="blue" style="visibility: hidden"><i class="icon-pencil bigger-130"></i></a>
                                                        <asp:RequiredFieldValidator ID="rfvUserFirstName" runat="server" ControlToValidate="txtFirstName" ErrorMessage="*" ForeColor="Red" ValidationGroup="SaveUPD"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="span2">Middle Name</td>
                                                    <td>
                                                        <asp:TextBox ID="txtMiddleName" runat="server" AutoCompleteType="Disabled" autocomplete="off" Enabled="false"></asp:TextBox>
                                                        <a id="MiddleEnable" data-rel="tooltip" title="Enable Edit" href="#" onclick="MiddleEnable()" class="green"><i class="icon-pencil bigger-130"></i></a>
                                                        <a id="MiddleDisable" data-rel="tooltip" title="Disable Edit" href="#" onclick="MiddleDisable()" class="blue" style="visibility: hidden"><i class="icon-pencil bigger-130"></i></a>
                                                        <%--<asp:RequiredFieldValidator ID="rfvUserMiddleName" runat="server" ControlToValidate="txtMiddleName" ErrorMessage="*" ForeColor="Red" ValidationGroup="SaveUPD"></asp:RequiredFieldValidator>--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="span2">Last Name</td>
                                                    <td>
                                                        <asp:TextBox ID="txtLastName" runat="server" AutoCompleteType="Disabled" autocomplete="off" Enabled="false"></asp:TextBox>
                                                        <a id="LastEnable" data-rel="tooltip" title="Enable Edit" href="#" onclick="LastEnable()" class="green"><i class="icon-pencil bigger-130"></i></a>
                                                        <a id="LastDisable" data-rel="tooltip" title="Disable Edit" href="#" onclick="LastDisable()" class="blue" style="visibility: hidden"><i class="icon-pencil bigger-130"></i></a>
                                                        <%--<asp:RequiredFieldValidator ID="rfvUserLastName" runat="server" ControlToValidate="txtLastName" ErrorMessage="*" ForeColor="Red" ValidationGroup="SaveUPD"></asp:RequiredFieldValidator>--%>
                                                        <asp:RequiredFieldValidator ID="rfvUserLastName" runat="server" ControlToValidate="txtLastName" ErrorMessage="*" ForeColor="Red" ValidationGroup="SaveUPD"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="span2">Email id</td>
                                                    <td>
                                                        <asp:TextBox ID="txtEmailId" runat="server" ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="span2">Contact Number</td>
                                                    <td>
                                                        <asp:TextBox ID="txtContactNumber" runat="server" AutoCompleteType="Disabled" autocomplete="off" Enabled="false"></asp:TextBox>
                                                        <a id="ContactEnable" data-rel="tooltip" title="Enable Edit" href="#" onclick="ContactEnable()" class="green"><i class="icon-pencil bigger-130"></i></a>
                                                        <a id="ContactDisable" data-rel="tooltip" title="Disable Edit" href="#" onclick="ContactDisable()" class="blue" style="visibility: hidden"><i class="icon-pencil bigger-130"></i></a>
                                                        <asp:RequiredFieldValidator ID="rfvCNO1" runat="server" ControlToValidate="txtContactNumber" ErrorMessage="*" ForeColor="Red" ValidationGroup="SaveUPD"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revContactNumber" runat="server" ControlToValidate="txtContactNumber" ErrorMessage="Enter Number Only" ForeColor="Red"
                                                            ValidationExpression="^\+?[0-9]+$" ValidationGroup="SaveUPD"></asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td class="span2">Phone Number (Official)</td>
                                                    <td>
                                                        <asp:TextBox ID="txtPhoneNumberOff" runat="server" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPhoneNumberOff" ErrorMessage="Enter Number Only" ForeColor="Red"
                                                            ValidationExpression="^\+?[0-9]+$" ValidationGroup="SaveUPD"></asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="span2">Mobile Number (Personal)</td>
                                                    <td>
                                                        <asp:TextBox ID="txtMobileNumberPer" runat="server" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtMobileNumberPer" ErrorMessage="Enter Number Only" ForeColor="Red"
                                                            ValidationExpression="^\+?[0-9]+$" ValidationGroup="SaveUPD"></asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="span2">Mailing Address</td>
                                                    <td>
                                                        <asp:TextBox ID="txtMailingAddress" runat="server" TextMode="MultiLine" Height="100px" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="span2">Company Name</td>
                                                    <td>
                                                        <asp:TextBox ID="txtCompanyName" runat="server" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 30%; vertical-align: top;">Geographic location</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlCountry2" runat="server">
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
                                                        <asp:RequiredFieldValidator ID="rfvCNO2" runat="server" ControlToValidate="ddlCountry2" InitialValue="0" ErrorMessage="*" ForeColor="Red" ValidationGroup="SaveUPD"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <asp:Button ID="BtnConfirm" runat="server" CssClass="btn btn-primary" Text="Save" OnClick="BtnConfirm_Click" ValidationGroup="SaveUPD" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="ChangePassword" class="tab-pane  in active" runat="server">
                                <div class="span12">
                                    <div class="row-fluid">
                                        <div class="table-header">
                                            Change Password	        
                                        </div>
                                        <div class="dataTables_wrapper">
                                            <table class="table table-hover dataTable table-bordered " id="tblChangePassword">
                                                <tr>
                                                    <td class="span3">Current Password</td>
                                                    <td>
                                                        <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password" ValidationGroup="Password"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvOldPassword" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtOldPassword" ValidationGroup="Password"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="span2">New Password</td>
                                                    <td>
                                                        <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" ValidationGroup="Password"></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ErrorMessage="Please Enter New Password" ForeColor="Red" ControlToValidate="txtNewPassword" ValidationGroup="Password"></asp:RequiredFieldValidator>--%>
                                                        <asp:RequiredFieldValidator ID="rfvOldPassword0" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtNewPassword" ValidationGroup="Password"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="valPassword" runat="server" ValidationGroup="Password" ControlToValidate="txtNewPassword" ErrorMessage="Minimum 6 Character Required" ForeColor="Red" ValidationExpression=".{6}.*" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="span2">Confirm Password</td>
                                                    <td>
                                                        <asp:TextBox ID="txtConformPassword" runat="server" TextMode="Password" ValidationGroup="Password"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvOldPassword1" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtConformPassword" ValidationGroup="Password"></asp:RequiredFieldValidator>
                                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="txtNewPassword" ControlToValidate="txtConformPassword" ErrorMessage="Your New Password & Confirm Password did not match." ForeColor="Red" ValidationGroup="Password"></asp:CompareValidator>
                                                        <%--<asp:RequiredFieldValidator ID="rfvConformPassword" runat="server" ErrorMessage="Please Enter Confirm Password" ForeColor="Red" ControlToValidate="txtConformPassword" ValidationGroup="Password"></asp:RequiredFieldValidator>--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <asp:Button ID="btnSavePassword" runat="server" Text="Save" class="btn btn-primary" align="left" ValidationGroup="Password" OnClick="btnSavePassword_Click" />
                                                        <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-warning" align="justify" OnClick="btnReset_Click" />
                                                        <div>
                                                            <div id="divPasswordSuccessMessage" runat="server" visible="false" class="alert alert-block alert-success ">
                                                                <button data-dismiss="alert" class="close" type="button">
                                                                    <i class="icon-remove"></i>
                                                                </button>
                                                                <p>
                                                                    <i class="icon-ok"></i>
                                                                    <asp:Label ID="lblPasswordSuccessmsg" runat="server"></asp:Label>
                                                                </p>
                                                            </div>
                                                            <div id="divPasswordErrorMessage" runat="server" visible="false" class="alert alert-error ">
                                                                <button data-dismiss="alert" class="close" type="button">
                                                                    <i class="icon-remove"></i>
                                                                </button>
                                                                <i class="icon-remove"></i>
                                                                <asp:Label ID="lblPasswordErrormsg" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="ChangePaymentDetails" class="tab-pane in active" runat="server">
                                <asp:ScriptManager runat="server" ID="ScriptManager"></asp:ScriptManager>

                                <asp:Panel runat="server" ID="pnlchangpayment" DefaultButton="btnSave">
                                    <div class="span12">
                                        <div class="row-fluid">
                                            <div class="row-fluid">
                                                <div>
                                                    <div id="divPaymentSuccessMessage" runat="server" visible="false" class="alert alert-block alert-success span6">
                                                        <button data-dismiss="alert" class="close" type="button">
                                                            <i class="icon-remove"></i>
                                                        </button>
                                                        <p>
                                                            <i class="icon-ok"></i>
                                                            <asp:Label ID="lblPaymentSuccessmsg" runat="server"></asp:Label>
                                                        </p>
                                                    </div>
                                                    <div id="divPaymentErrorMessage" runat="server" visible="false" class="alert alert-error span6">
                                                        <button data-dismiss="alert" class="close" type="button">
                                                            <i class="icon-remove"></i>
                                                        </button>
                                                        <i class="icon-remove"></i>
                                                        <asp:Label ID="lblPaymentErrormsg" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:Panel ID="pnlPayment" runat="server" DefaultButton="btnSave">
                                                <%--                                <asp:UpdatePanel runat="server" ID="upnlPayments">
                                    <ContentTemplate>--%>
                                                <div class="table-header">
                                                    Card Details	        
                                                </div>
                                                <div class="dataTables_wrapper">
                                                    <table class="table table-hover dataTable table-bordered ">
                                                        <tr runat="server" id="trExistingCards" visible="false">
                                                            <td class="span3">Registered Cards</td>
                                                            <td>
                                                                <asp:DropDownList runat="server" ID="ddlExistingCards" AutoPostBack="true" OnSelectedIndexChanged="ddlExistingCards_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <%--<asp:Button ID="btnNewCard" runat="server" align="justify" class="btn btn-primary" CssClass="btn btn-success" OnClick="btnNewCard_Click" Text="New Credit Card" />
                                            <asp:Button ID="btnExistingcard" runat="server" align="justify" class="btn btn-primary" CssClass="btn btn-warning" OnClick="btnExistingcard_Click" Text="Existing Card" Visible="False" />--%>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" id="trReadOnlyDefaultMod" visible="true">
                                                            <td class="span3">Default Credit Card for Payments</td>
                                                            <td>Yes </td>
                                                        </tr>
                                                        <tr runat="server" id="trUpdateDefaultMod" visible="false">
                                                            <td class="span3">Make This Card As My Default Credit Card for Payments
                                                            </td>
                                                            <td style="vertical-align: middle;">
                                                                <asp:CheckBox runat="server" ID="chkDefalutCreditCard" Checked="true" />
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnUpdate" runat="server" align="justify" class="btn btn-primary" OnClick="btnUpdate_Click" Text="Update" Visible="False" />
                                                            </td>
                                                        </tr>
                                                        <div runat="server" id="dvCardDetails" visible="false">
                                                            <div runat="server" id="divExistingCrdCardList" visible="false">
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <div class="alert alert-warning">
                                                                            To update any information to your registered Credit Card contact admin@wftcloud.com or you can choose to<br />
                                                                            add a new Credit Card with updated information by choosing New Credit Card option from the Registered Cards drop down menu.<br />
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Select Credit Card to be deleted from your profile</td>
                                                                    <td>
                                                                        <asp:RadioButtonList runat="server" ID="rblCreditCard" RepeatLayout="Flow" OnSelectedIndexChanged="rblCreditCard_SelectedIndexChanged" AutoPostBack="true">
                                                                        </asp:RadioButtonList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="rblCreditCard" ValidationGroup="PaymentDetails"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>

                                                            </div>
                                                            <tr>
                                                                <td class="span3">Credit Card Number</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtCreditCardNumber" runat="server" AutoCompleteType="Disabled" autocomplete="off" AutoPostBack="True" OnTextChanged="txtCreditCardNumber_TextChanged"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtCreditCardNumber" ValidationGroup="PaymentDetails"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="valPassword0" runat="server" ValidationGroup="PaymentDetails" ControlToValidate="txtCreditCardNumber" ErrorMessage="Invaild Card Number" ForeColor="Red" ValidationExpression="^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$" />
                                                                </td>
                                                            </tr>
                                                            <tr runat="server" visible="false" id="trCardType">
                                                                <td class="span3">Card Type</td>
                                                                <td class="">

                                                                    <div>
                                                                        <div id="divValidCard" runat="server" visible="false" class="alert alert-block alert-success span5">
                                                                            <p>
                                                                                <i class="icon-ok"></i>
                                                                                <asp:Label ID="lblCardType" runat="server"></asp:Label>
                                                                            </p>
                                                                        </div>
                                                                        <div id="divInValidCard" runat="server" visible="True" class="alert alert-error span12">

                                                                            <i class="icon-remove"></i>
                                                                            <asp:Label ID="lblInvalidCard" runat="server" Text="Enter Valid Credit Card Number"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="span2">CVV Code</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtVerifiCode1" runat="server" TextMode="Password" Style="margin-left: 5px;" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtVerifiCode1" ValidationGroup="PaymentDetails"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="span2">Expiration Date</td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlExpMonth" runat="server" Width="100px">
                                                                        <asp:ListItem Value="Select">Select</asp:ListItem>
                                                                        <asp:ListItem Value="01">Jan</asp:ListItem>
                                                                        <asp:ListItem Value="02">Feb</asp:ListItem>
                                                                        <asp:ListItem Value="03">Mar</asp:ListItem>
                                                                        <asp:ListItem Value="04">Apr</asp:ListItem>
                                                                        <asp:ListItem Value="05">May</asp:ListItem>
                                                                        <asp:ListItem Value="06">Jun</asp:ListItem>
                                                                        <asp:ListItem Value="07">Jul</asp:ListItem>
                                                                        <asp:ListItem Value="08">Aug</asp:ListItem>
                                                                        <asp:ListItem Value="09">Sep</asp:ListItem>
                                                                        <asp:ListItem Value="10">Oct</asp:ListItem>
                                                                        <asp:ListItem Value="11">Nov</asp:ListItem>
                                                                        <asp:ListItem Value="12">Dec</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue="Select" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlExpMonth" ValidationGroup="PaymentDetails"></asp:RequiredFieldValidator>
                                                                    <asp:DropDownList ID="ddlExpYear" runat="server" Width="100px">
                                                                        <asp:ListItem>Select</asp:ListItem>
                                                                        <asp:ListItem>2013</asp:ListItem>
                                                                        <asp:ListItem>2014</asp:ListItem>
                                                                        <asp:ListItem>2015</asp:ListItem>
                                                                        <asp:ListItem>2016</asp:ListItem>
                                                                        <asp:ListItem>2017</asp:ListItem>
                                                                        <asp:ListItem>2018</asp:ListItem>
                                                                        <asp:ListItem>2019</asp:ListItem>
                                                                        <asp:ListItem>2020</asp:ListItem>
                                                                        <asp:ListItem>2021</asp:ListItem>
                                                                        <asp:ListItem>2022</asp:ListItem>
                                                                        <asp:ListItem>2023</asp:ListItem>
                                                                        <asp:ListItem>2024</asp:ListItem>
                                                                        <asp:ListItem>2025</asp:ListItem>
                                                                        <asp:ListItem>2026</asp:ListItem>
                                                                        <asp:ListItem>2027</asp:ListItem>
                                                                        <asp:ListItem>2028</asp:ListItem>
                                                                        <asp:ListItem>2029</asp:ListItem>
                                                                        <asp:ListItem>2030</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" InitialValue="Select" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlExpYear" ValidationGroup="PaymentDetails"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                        </div>
                                                    </table>
                                                    <br />
                                                </div>
                                                <!-- Payment details-->
                                                <div class="table-header">
                                                    Card Holder Details	        
                                                </div>
                                                <div class="dataTables_wrapper">
                                                    <table class="table table-hover dataTable table-bordered " id="tblChangePaymentDetails">
                                                        <tr>
                                                            <td class="span3">Name On Card</td>
                                                            <td>
                                                                <asp:TextBox ID="txtNameOnCard" runat="server" AutoCompleteType="Disabled" autocomplete="off" MaxLength="30"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtNameOnCard" ValidationGroup="PaymentDetails"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="span2">Address1</td>
                                                            <td>
                                                                <asp:TextBox ID="txtAddress1" runat="server" TextMode="MultiLine" Style="margin-left: 5px;" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvAddress1" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="PaymentDetails" ControlToValidate="txtAddress1"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtAddress1" ErrorMessage="Address1 cannot exceed 30 characters in length" ForeColor="Red" ValidationExpression="^[\s\S]{0,30}$" ValidationGroup="PaymentDetails"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="span2">Address2(Optional)</td>
                                                            <td>
                                                                <asp:TextBox ID="txtAddress2" runat="server" TextMode="MultiLine" Style="margin-left: 5px;" AutoCompleteType="Disabled" autocomplete="off" MaxLength="30"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="rev3" runat="server" ControlToValidate="txtAddress2" ErrorMessage="Address2 cannot exceed 30 characters in length" ForeColor="Red" ValidationExpression="^[\s\S]{0,30}$" ValidationGroup="PaymentDetails"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="span2">City</td>
                                                            <td>
                                                                <asp:TextBox runat="server" ID="txtCity" AutoCompleteType="Disabled" autocomplete="off" MaxLength="30"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvLastName2" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtCity" ValidationGroup="PaymentDetails"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <%--  <asp:UpdatePanel runat="server" ID="upnlCountryDDLChanged">
                                                    <Triggers >
                                                        <asp:AsyncPostBackTrigger ControlID="ddlCountry" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>--%>

                                                        <tr>
                                                            <td class="span2">Country</td>
                                                            <td>
                                                                <asp:UpdatePanel ID="CountryUpdatePanel" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:DropDownList ID="ddlCountry" runat="server" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="True">
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="rfvLastName4" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlCountry" ValidationGroup="PaymentDetails" InitialValue="0"></asp:RequiredFieldValidator>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>State</td>
                                                            <td>
                                                                <asp:UpdatePanel ID="StateUpdatePanel" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:DropDownList runat="server" ID="ddlState"></asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlState" ValidationGroup="PaymentDetails" InitialValue="0"></asp:RequiredFieldValidator>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="ddlCountry" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </tr>
                                                        <%--                                                        </ContentTemplate>
                                                </asp:UpdatePanel>--%>
                                                        <tr>
                                                            <td class="span2">Postal/Zip Code</td>
                                                            <td>

                                                                <asp:TextBox ID="txtPostalZipCode" runat="server" AutoCompleteType="Disabled" autocomplete="off" MaxLength="30"></asp:TextBox>

                                                                <asp:RequiredFieldValidator ID="rfvAddress3" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="PaymentDetails" ControlToValidate="txtPostalZipCode"></asp:RequiredFieldValidator>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="span2">Contact Number</td>
                                                            <td>
                                                                <asp:TextBox ID="txtContactNumberPtDet" runat="server" AutoCompleteType="Disabled" autocomplete="off" MaxLength="30"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvAddress2" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="PaymentDetails" ControlToValidate="txtContactNumberPtDet"></asp:RequiredFieldValidator>
                                                                <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtContactNumberPtDet" ErrorMessage="Enter Number Only" ForeColor="Red" ValidationExpression="\d+" ValidationGroup="PaymentDetails"></asp:RegularExpressionValidator>--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <table class="table table-hover dataTable table-bordered ">
                                                    <tr runat="server" id="trSaveCrediCard">
                                                        <td class="span3"></td>
                                                        <td>
                                                            <asp:Button ID="btnSave" Text="Save" runat="server" class="btn btn-primary" align="justify" ValidationGroup="PaymentDetails" OnClick="btnSave_Click1" />
                                                        </td>

                                                    </tr>
                                                </table>
                                                <%--                                                                                </ContentTemplate>
                                </asp:UpdatePanel>--%>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </asp:Panel>


                            </div>
                            <%--<div id="CancelSubscription" class="tab-pane" runat="server">
                                <div class="span12">

                                    <div class="row-fluid">
                                        <div class="table-header">
                                           Cancel Subscription      
                                        </div>
                                        <div class="dataTables_wrapper">
                                            <table class="table table-hover dataTable table-bordered " id="tblCancelSubscription">
                                                <tr>
                                                    <td class="span8">
                                                       To Close WFT Cloud Account.<br />
                                                        Please click Cancel Subscription. 
                                                    </td>
                                                    <td style="text-align:center; vertical-align:middle;">
                                                        <asp:LinkButton  data-rel="tooltip" OnClientClick="return ConfirmOnDelete()" title="Cancel Subscription" runat="server" ID="lkbtnUnSubscribe" OnClick="lkbtnUnSubscribe_Click" >
                                                            <i class="icon-ban-circle bigger-300 red"></i>
                                                        </asp:LinkButton>
                                                </tr>
                                            </table>
                                                                                <div>
                                        <div id="divCancelSuccessMessage" runat="server" visible="false" class="alert alert-block alert-success ">
                                            <button data-dismiss="alert" class="close" type="button">
                                                <i class="icon-remove"></i>
                                            </button>
                                            <p>
                                                <i class="icon-ok"></i>
                                                <asp:Label ID="lblCancelSuccessmsg" runat="server"></asp:Label>
                                            </p>
                                        </div>
                                        <div id="divCancelErrorMessage" runat="server" visible="false" class="alert alert-error ">
                                            <button data-dismiss="alert" class="close" type="button">
                                                <i class="icon-remove"></i>
                                            </button>
                                            <i class="icon-remove"></i>
                                            <asp:Label ID="lblCancelErrormsg" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                        </div>
                                    </div>
                                </div>
                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        jQuery(function ($) {
            $('#tblUserProfilesDetails').dataTable(
         );
        });
        jQuery(function ($) {
            $('#tblChangePassword').dataTable(
         );
        });
        jQuery(function ($) {
            $('#tblChangePaymentDetails').dataTable(
         );
        });
        jQuery(function ($) {
            $('#tblCancelSubscription').dataTable(
         );
        });
    </script>
    <script type="text/javascript">
        function ConfirmOnDelete() {
            if (confirm("Are you sure want to cancel your account?") == true)
                return true;
            else
                return false;
        }
    </script>

    <script type="text/javascript">
        function FirstEnable() {
            document.getElementById("ContentPlaceHolder1_txtFirstName").disabled = false;
            document.getElementById("FirstEnable").style.visibility = 'hidden';
            document.getElementById("FirstDisable").style.visibility = 'visible';
        }
        function FirstDisable() {
            document.getElementById("ContentPlaceHolder1_txtFirstName").disabled = true;
            document.getElementById("FirstEnable").style.visibility = 'visible';
            document.getElementById("FirstDisable").style.visibility = 'hidden';
        }

        function MiddleEnable() {
            document.getElementById("ContentPlaceHolder1_txtMiddleName").disabled = false;
            document.getElementById("MiddleEnable").style.visibility = 'hidden';
            document.getElementById("MiddleDisable").style.visibility = 'visible';
        }
        function MiddleDisable() {
            document.getElementById("ContentPlaceHolder1_txtMiddleName").disabled = true;
            document.getElementById("MiddleEnable").style.visibility = 'visible';
            document.getElementById("MiddleDisable").style.visibility = 'hidden';
        }

        function LastEnable() {
            document.getElementById("ContentPlaceHolder1_txtLastName").disabled = false;
            document.getElementById("LastEnable").style.visibility = 'hidden';
            document.getElementById("LastDisable").style.visibility = 'visible';
        }
        function LastDisable() {
            document.getElementById("ContentPlaceHolder1_txtLastName").disabled = true;
            document.getElementById("LastEnable").style.visibility = 'visible';
            document.getElementById("LastDisable").style.visibility = 'hidden';
        }

        function ContactEnable() {
            document.getElementById("ContentPlaceHolder1_txtContactNumber").disabled = false;
            document.getElementById("ContactEnable").style.visibility = 'hidden';
            document.getElementById("ContactDisable").style.visibility = 'visible';
        }
        function ContactDisable() {
            document.getElementById("ContentPlaceHolder1_txtContactNumber").disabled = true;
            document.getElementById("ContactEnable").style.visibility = 'visible';
            document.getElementById("ContactDisable").style.visibility = 'hidden';
        }
    </script>
</asp:Content>

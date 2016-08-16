<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd.Master" AutoEventWireup="true" CodeBehind="BWHANARegistration.aspx.cs" Inherits="WFTCloud.User.BWHANARegistration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Including main template styles -->
        <link rel="stylesheet" href="/css/main.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="content">
               
                <div class="white-section" style ="background-image:url(../img/BG-HANA.jpg); background-size:100%">
                    
                    <div class="container">

                        <div class="row">
                           
                          
                            <div>
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 50%;">
                                            <div>
                                                <h4>Please Submit Your Query:</h4>
                                            </div>
                                            <table>
                                                  <tr>
                                                <td colspan="2">
                                                        <div id="divRegisterSuccess" runat="server" visible="false" class="alert alert-success ">
                                                            <i class="icon-remove"></i>
                                                            <asp:Label ID="lblRegisterSuccess" Text="Admin has been notified about a SAP BW & HANA request, our administrator will contact you earlier." runat="server"></asp:Label>
                                                            <span></span>
                                                        </div>
                                                    </td>
                                                    </tr>
                                                <tr>
                                                    <td style="width: 30%; vertical-align: top;">Company Name
                                                       
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCompanyName" runat="server" CssClass="input-medium" AutoCompleteType="Disabled" autocomplete="off" Width="300px"></asp:TextBox>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td style="width: 30%; vertical-align: top;">First Name
                                                        <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" ValidationGroup="Register" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="input-medium" AutoCompleteType="Disabled" autocomplete="off" Width="300px"></asp:TextBox>
                                                    </td>

                                                </tr>
                                               
                                                <tr>
                                                    <td style="width: 30%; vertical-align: top;">Last Name
                                                        <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ValidationGroup="Register" ControlToValidate="txtLastName" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtLastName" runat="server" CssClass="input-medium" AutoCompleteType="Disabled" autocomplete="off" Width="300px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" style="width: 30%; vertical-align: top;">Email ID
                                                        <asp:RequiredFieldValidator ID="rfvEmailID" runat="server" ValidationGroup="Register" ControlToValidate="txtEmailID" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revEmailID" runat="server" ValidationGroup="Register" ControlToValidate="txtEmailID" ErrorMessage="*" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtEmailID" runat="server" CssClass="input-medium" AutoCompleteType="Disabled" autocomplete="off" Width="300px"></asp:TextBox>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="width: 30%; vertical-align: top;">Contact Number
                                                        <asp:RequiredFieldValidator ID="rfv" runat="server" ValidationGroup="Register" ControlToValidate="txtContactNumber" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtContactNumber" ErrorMessage="*" ForeColor="Red" ValidationExpression="^\+?[0-9]+$" ValidationGroup="Register"></asp:RegularExpressionValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtContactNumber" runat="server" CssClass="input-medium" Width="300px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 30%; vertical-align: top;">Geographic location
                                                        <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ErrorMessage="*" ValidationGroup="Register" ControlToValidate="ddlCountry" InitialValue="0" ForeColor="Red"  ></asp:RequiredFieldValidator>
                                                   </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlCountry" runat="server" Width="310px">
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
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td style="width: 30%; vertical-align: top;">How did you hear about us?
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3"  runat="server" ForeColor="Red" ValidationGroup="Register" ControlToValidate="txtOthers" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlHearAboutUs" runat="server" Width="310px" OnSelectedIndexChanged="ddlHearAboutUs_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Text="Google" Value="Google"></asp:ListItem>
                                                            <asp:ListItem Text="Facebook" Value="Facebook"></asp:ListItem>
                                                            <asp:ListItem Text="LinkedIn" Value="LinkedIn"></asp:ListItem>
                                                            <asp:ListItem Text="Twitter" Value="Twitter"></asp:ListItem>
                                                            <asp:ListItem Text="Press Release" Value="Press Release"></asp:ListItem>
                                                            <asp:ListItem Text="Others" Value="Others"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>

                                                </tr>
                                                 <tr runat="server" id="trOthers" visible="false">
                                                    <td style="text-align: right;">
                                                        <asp:RequiredFieldValidator ID="rfvCountry0" runat="server" ControlToValidate="ddlHearAboutUs" ErrorMessage="*" ValidationGroup="Register" ForeColor="Red" InitialValue="0" ></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtOthers" runat="server" placeholder="Other referral" CssClass="input-medium"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td style="width: 30%; vertical-align: top;">Subject
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Register" ControlToValidate="txtSubject" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSubject" runat="server" CssClass="input-medium" TextMode="MultiLine" Height="50px" Width="310px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td style="text-align:center">
                                                        <br />
                                                        <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/img/save.png"  ToolTip="Click here to submit" ValidationGroup="Register" OnClick="btnRegisterCode_Click" ForeColor="White" />
                                                       <%-- <asp:Button ID="btnRegisterCode" CssClass="btn btn-primary" runat="server" Text="Register" ValidationGroup="Register" OnClick="btnRegisterCode_Click"/>--%>
                                                    </td>
                                                </tr>
                                            </table>

                                        </td>
                                    </tr>
                                </table>
                                </div>
                           <%-- </div>--%>
                       </div><!-- row end -->

                    </div><!-- conteiner end -->

                </div><!-- white-section end -->

            </section>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerSEOContent" runat="server">
</asp:Content>

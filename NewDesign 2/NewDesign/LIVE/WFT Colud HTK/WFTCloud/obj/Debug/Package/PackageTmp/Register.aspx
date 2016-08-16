<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WFTCloud.Register" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8">
    <title>WFT Cloud</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">

    <meta name="description" content="">
    <meta name="author" content="">
    <meta name="keywords" content="">
    <!-- base styles -->
    <link href="../css/Gridstyle.css" rel="stylesheet" type="text/css" />
    <link href="asset/css/styles.css" rel="stylesheet">
    <link href="asset/css/styles-custom.css" rel="stylesheet">
    <!-- google font styles -->
    <!-- <link href='http://fonts.googleapis.com/css?family=Merriweather:400,700,900' rel='stylesheet' type='text/css'> -->
    <!-- Le HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
  <script src="asset/js/vendor/html5shiv.min.js"></script>
  <script src="asset/js/vendor/respond.min.js"></script>
  <![endif]-->
    <!-- Le fav and touch icons -->
    <link rel="shortcut icon" href="asset/images/favicon.ico">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css">
</head>

<header class="navbar-fixed-top">
  <nav class="top-navbar">
    <div class="container">
      <div class="row">
        <div class="col-md-6">
          <div class="top-left-sap">
            <a href="#"> Cloud Enablement for SAP Customers</a>
          </div>
        </div>
        <div class="col-md-6">
          <ul class="top-right-nav">
            <li><a href="company.html">Company</a></li>
            <li><a href="Support.html"><img src="img/phone-s.png" />Support</a></li>
            <li><a href="contact.aspx"><img src="img/emailicon.png" />Contact</a></li>
            <li><a href="Login.aspx"><img src="/img/log-in-s.jpg" />Member Login</a></li>
          </ul>
        </div>
      </div>
    </div>
  </nav>
  <!-- main navigation -->
  <div class="primary-navbar">
    <div class="container">
      <div class="row">
        <div class="col-md-12">
          <nav class="navbar">
            <div class="container">
              <div class="navbar-header">
                <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar"
                        aria-expanded="false" aria-controls="navbar">
                  <span class="sr-only">Toggle navigation</span>
                  <span class="icon-bar"></span>
                  <span class="icon-bar"></span>
                  <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand logo" href="Home.aspx"> <img src="asset/images/WFTCloud-Logo.jpg" width="218"
                                                            height="98" alt=""> </a>
              </div>
              <div id="navbar" class="navbar-collapse collapse">
                <ul class="nav navbar-nav navbar-right">
                  <li><a href="Home.aspx">Enterprise SAP</a></li>
                  <li><a href="expresssapcloud.aspx">Express SAP Cloud</a></li>
                  <li><a href="#">Trainings</a></li>
                  <li><a href="#">Media</a></li>
                  <li><a href="about.html">About Us</a></li>
                </ul>
              </div><!--/.nav-collapse -->
            </div>
          </nav>
        </div>
      </div>
    </div>
  </div>
</header>
<div class="clearfix"></div>
<!-- /header -->
<body>

    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="CloudAnalyticsToolkitScriptManager" runat="server">
        </asp:ToolkitScriptManager>
        <section class="main-content">
  <div class="container p-b-lg" style="padding-top: 100px;">
    <h3 class="text-center text-c-01">Sign up for WFT Cloud</h3>
    <div class="col-md-6">
      <h3 class="text-c-01">Register</h3>
      <hr>
        <div id="divRegisterSuccess" runat="server" visible="false" class="alert alert-success ">
                                                            <i class="icon-remove"></i>
                                                            <asp:Label ID="lblRegisterSuccess" Text="Email Send Successfully" runat="server"></asp:Label>
                                                            <span>Click here to <a href="Login.aspx"><u>login.</u></a></span>
                                                        </div>
                                                        <div id="divRegisterError" runat="server" visible="false" class="alert alert-error ">
                                                            <i class="icon-remove"></i>
                                                            <asp:Label ID="lblRegisterError" Text="" runat="server" ForeColor="Red"></asp:Label>
                                                        </div>
      <form class="m-t-md">
        <div class="row">
          <div class="form-group col-md-4 p-r-xxs">
            <label for="fname">First Name</label>
           <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" placeholder="First Name" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
              <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" ValidationGroup="Register" Display="None" ErrorMessage="First name required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                 <asp:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender8" TargetControlID="rfvFirstName" Width="180px" CssClass="customCalloutStyle" PopupPosition="BottomRight" WarningIconImageUrl="~/img/warning-icon.png"  />
          </div>
          <div class="form-group col-md-4 p-r-xxs">
            <label for="mname">Middle Name</label>
           <asp:TextBox ID="txtMiddleName" runat="server" CssClass="form-control" AutoCompleteType="Disabled" placeholder="Middle Name" autocomplete="off"></asp:TextBox>
          </div>
          <div class="form-group col-md-4">
            <label for="lname">Last Name</label>
           <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" AutoCompleteType="Disabled" placeholder="Last Name" autocomplete="off"></asp:TextBox>
               <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName" ValidationGroup="Register" Display="None" ErrorMessage="Last name required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                <asp:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender9" TargetControlID="rfvLastName" Width="180px" CssClass="customCalloutStyle" PopupPosition="BottomRight" WarningIconImageUrl="~/img/warning-icon.png" />
          </div>
        </div>
        <div class="form-group">
          <label for="exampleInputEmail1">Email address</label>
         <asp:TextBox ID="txtEmailID" runat="server" CssClass="form-control" AutoCompleteType="Disabled" autocomplete="off" placeholder="User Name"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvEmailID" runat="server" ValidationGroup="Register" ControlToValidate="txtEmailID" Display="None" ErrorMessage="Email required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revEmailID" runat="server" ValidationGroup="Register" ControlToValidate="txtEmailID" Display="None" ErrorMessage="Type valid email address" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                <asp:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender10" TargetControlID="rfvEmailID" Width="150px" CssClass="customCalloutStyle" PopupPosition="TopRight" WarningIconImageUrl="~/img/warning-icon.png"  />
                                        <asp:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender11" TargetControlID="revEmailID" Width="210px" CssClass="customCalloutStyle" PopupPosition="BottomLeft" WarningIconImageUrl="~/img/warning-icon.png"  />
        </div>
        <div class="row">
          <div class="form-group col-md-6">
            <label for="pass1">Password</label>
           <asp:TextBox ID="txtRegPassword" runat="server" CssClass="form-control" AutoCompleteType="Disabled" autocomplete="off" placeholder="Password" TextMode="Password"></asp:TextBox>
           <asp:RequiredFieldValidator ID="rfvregpassword" runat="server" ControlToValidate="txtRegPassword" ValidationGroup="Register" Display="None" ErrorMessage="pasword required" ForeColor="Red"></asp:RequiredFieldValidator>
              <asp:ValidatorCalloutExtender runat="Server" ID="vcepassword" TargetControlID="rfvregpassword" Width="180px" CssClass="customCalloutStyle" PopupPosition="BottomLeft" WarningIconImageUrl="~/img/warning-icon.png" />

          </div>
          <div class="form-group col-md-6">
            <label for="pass2">Confirm Password</label>
            <asp:TextBox ID="txtConfPassword" runat="server" CssClass="form-control" AutoCompleteType="Disabled" autocomplete="off" placeholder="Confirm Password" TextMode="Password"></asp:TextBox><br />
          <asp:RequiredFieldValidator ID="rfvconpwd" runat="server" ControlToValidate="txtConfPassword" ValidationGroup="Register" Display="None" ErrorMessage="pasword required" ForeColor="Red"></asp:RequiredFieldValidator>
              <asp:ValidatorCalloutExtender runat="Server" ID="vceconfpassword" TargetControlID="rfvconpwd" Width="180px" CssClass="customCalloutStyle" PopupPosition="BottomLeft" WarningIconImageUrl="~/img/warning-icon.png" />
          </div>
        </div>
        <div class="form-group">
          <label for="exampleInputEmail1">Geographic Location</label>
        <asp:DropDownList ID="ddlCountry" class="form-control" runat="server">
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
             <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ControlToValidate="ddlCountry" ValidationGroup="Register" Display="None" InitialValue="0" ErrorMessage="Select country" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1" TargetControlID="rfvCountry" Width="180px" CssClass="customCalloutStyle" PopupPosition="TopRight" WarningIconImageUrl="~/img/warning-icon.png" />
        </div>
        <div class="form-group">
          <label for="exampleInputEmail1">How did you hear about us?</label>
          <asp:DropDownList ID="ddlHearAboutUs" runat="server" class="form-control" Width="165px" OnSelectedIndexChanged="ddlHearAboutUs_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Text="Google" Value="Google"></asp:ListItem>
                                                            <asp:ListItem Text="Facebook" Value="Facebook"></asp:ListItem>
                                                            <asp:ListItem Text="LinkedIn" Value="LinkedIn"></asp:ListItem>
                                                            <asp:ListItem Text="Twitter" Value="Twitter"></asp:ListItem>
                                                            <asp:ListItem Text="Press Release" Value="Press Release"></asp:ListItem>
                                                            <asp:ListItem Text="Others" Value="Others"></asp:ListItem>
                                                        </asp:DropDownList>
        </div>
          
               <asp:TextBox ID="txtOthers" runat="server" Visible="false" placeholder="Other referral" CssClass="form-control"></asp:TextBox>
             <%--<asp:RequiredFieldValidator ID="rfvOthers" runat="server" ControlToValidate="txtOthers" ValidationGroup="Register" Display="None" ErrorMessage="referral name" ForeColor="Red"></asp:RequiredFieldValidator>
                                                 <asp:ValidatorCalloutExtender runat="Server" ID="vceothers" TargetControlID="rfvOthers" Width="180px" CssClass="customCalloutStyle" PopupPosition="TopRight" WarningIconImageUrl="~/img/warning-icon.png"  />--%>
        <div class="checkbox">
          <label style="font-weight: bold">
             <input type="checkbox" id="chkAgree" runat="server" />&nbsp;&nbsp;I agree to the Terms of Service
          </label>
        </div>
        <div class="row">
          <div class="col-md-6">
              <asp:Button ID="btnRegisterCode" CssClass="btn btn-primary btn-lg btn-block" runat="server" OnClick="btnRegisterCode_Click"
                                                            Text="Register" ValidationGroup="Register" />
              <asp:RegularExpressionValidator ID="rvUserPassword" runat="server" ValidationGroup="Register" ControlToValidate="txtRegPassword" ErrorMessage="Password should be at least 6 characters in length.&lt;br/&gt;" ForeColor="Red" ValidationExpression=".{6}.*"></asp:RegularExpressionValidator>
                                                        <asp:CompareValidator ID="cvConfPassword" ValidationGroup="Register" runat="server"  ControlToCompare="txtRegPassword" ControlToValidate="txtConfPassword" ErrorMessage="Your New Password & Confirm Password did not match" ForeColor="Red"></asp:CompareValidator>
          </div>
          <div class="col-md-6">
              <asp:Button ID="btnResetForm" CssClass="btn btn-primary btn-lg btn-block" runat="server" 
                                                            Text="Clear All"  />
          </div>
        </div>
          <div id="divForgotSuccess" runat="server" visible="false" class="alert alert-success ">
                                                <asp:Label ID="lblForgotSuccess" Text="Email Sent Successfully" runat="server"></asp:Label>
                                            </div>
                                            <div id="divForgotError" runat="server" visible="false" class="alert alert-error ">
                                                <i class="icon-remove"></i>
                                                <asp:Label ID="lblForgotError" Text="" runat="server" ForeColor="Red"></asp:Label>
                                            </div>
      </form>
    </div>
    <div class="col-md-6">
      <h3 class="text-c-01">Terms & Conditions</h3>

      <hr>
      <div class="col-md-12 p-a-md" style="background-color: #f8f8f8">
        <span id="" style="display:inline-block; height:400px; overflow: scroll;">
                         <p>
                         </p><h4>WFT Incorporated WFTCloud Terms of Service</h4>

                      <p>WFTCloud is wholly owned by Wharfedale Technologies Incorporated. WFT's terms of service govern
                        the use of WFTCloud.</p>
                      <h4>Content</h4>
                      <p>All services provided by WFT, Inc. are to be used for lawful purposes only. Transmission,
                        storage, or presentation of any information, data or material in violation of any United States
                        Federal, State or Local law is prohibited. This includes, but is not limited to: copyrighted
                        material, material we judge to be threatening or obscene, material that jeopardizes national
                        security, or material protected by trade secret or other laws. The subscriber agrees to
                        indemnify and hold harmless WFT, Inc., from any claims resulting from the subscriber's use of
                        WFTCloud services which damages the subscriber or any other party.</p>
                      <p><b>Examples of prohibited content or links include (but are not limited to):</b></p>
                      <ul>
                        <li>Pirated software</li>

                        <li>Hacking sites, programs or archives</li>
                        <li>Warez Sites</li>
                        <li>Pornographic and other similar material</li>
                        <li>Distribution of Movies, Music or any copyrighted material other than those explicitly agreed
                          by both parties.
                        </li>
                      </ul>
                      <p>WFT, Inc. will be the sole arbiter as to what constitutes a violation of this provision.
                        Content that does not meet these standards will be removed without prior notice to the
                        subscriber.</p>

                      <p><b>Housing of any of the following files is considered a violation of the terms of service:</b>
                      </p><ul>
          <li><b> Commercial and Non Commercial</b> Software that could degrade the performance of SAP</li>
          <li><b>IRC - </b>We currently do not allow IRC, Egg Drops, BNC, or IRC bots to be operated on our servers or
            network. Files with references to IRC or any likeness thereof are prohibited.
          </li>
          <li><b>Proxies - </b>We do not allow proxy servers of any kind, whether for personal or business use. Files
            with references to any proxy or likeness thereof are prohibited.
          </li>

          <li><b>PortScanning - </b>We do not allow any kind of portscanning to be done on or from our servers or
            network.
          </li>
        </ul>
                      <p></p>
                      <h4>Commercial Advertising - Email</h4>
                      <ul style="padding-top:10px;">
                        <li>Spamming, i.e. the sending of unsolicited email, from any WFT's Web server or any server
                          located on the WFTCloud network is STRICTLY prohibited. WFT will be the sole arbiter as to
                          what constitutes a violation of this provision. This also includes Opt-in Opt-out mail
                          programs and mail that either directly or indirectly references a domain contained within an
                          account at WFT.
                        </li>
                        <li>Running Unconfirmed Mailing Lists. Subscribing email addresses to any mailing list without
                          the express and verifiable permission of the email address owner is prohibited. All mailing
                          lists run by WFT customers must be Closed-loop ("Confirmed Opt-in"). The subscription
                          confirmation message received from each address owner must be kept on file for the duration of
                          the existence of the mailing list. Purchasing or selling lists of email addresses from 3rd
                          parties for mailing to or from any WFT .-hosted domain, or referencing any WFT account, is
                          prohibited.
                        </li>

                        <li>Advertising, transmitting, or otherwise making available any software, program, product, or
                          service that is designed to violate this AUP or the AUP of any other Internet Service
                          Provider, which includes, but is not limited to, the facilitation of the means to send
                          Unsolicited Bulk Email, initiation of pinging, flooding, mail-bombing, denial of service
                          attacks is prohibited.
                        </li>
                        <li> Email address cultivating, or any unauthorized collecting of email addresses without prior
                          notification of the email address owner is strictly prohibited.
                        </li>
                        <li>Operating an account on behalf of, or in connection with, or reselling any service to,
                          persons or firms listed in the Spamhaus Register of Known Spam Operations (ROKSO) database at
                          www.spamhaus.org is prohibited.
                        </li>
                        <li>All commercial email marketing companies must adhere to the Controlling the Assault of
                          Non-Solicited Pornography And Marketing (CAN-SPAM) Act of 2003. In addition such companies are
                          prohibited the sending of bulk mail from "disposable domain names" with whois privacy
                          protection.
                        </li>
                      </ul>
                      <h4>Server Abuse</h4>

                      <p>Any attempts to undermine or cause harm to a WFT server or subscriber of WFT is strictly
                        prohibited including, but not limited to:
                      </p><ul>
          <li> Logging into a server or account that you are not authorized to access.</li>
          <li>Accessing data or taking any action to obtain services not intended for you or your use.</li>
          <li>Attempting to probe, scan or test the vulnerability of any system, subsystem or network.</li>
          <li>Tampering, hacking, modifying, or otherwise corrupting or breaching security or authentication measures
            without proper authorization.
          </li>
          <li>Transmitting material that contains viruses, Trojan horses, worms, time bombs, cancelbots or other
            computer programming routines or engines with the intent or effect of damaging, destroying, disrupting or
            otherwise impairing a computer's functionality or the operation of the System.
          </li>

          <li>Interfering with, intercepting or expropriating any system, data or information.</li>
          <li>Interfering with service to any user, host or network including, without limitation, by means of
            overloading, "flooding," "mailbombing," or "crashing" any computer system.
          </li>
        </ul>
                      <p></p>
                      <p>You will be held responsible for all actions performed by your account whether it be done by
                        you or by others.</p>
                      <p>All sub-networks of WFT Inc. and all servers must adhere to the above policies.</p>

                      <h4>Server Support</h4>

                      <p>Basic support and maintenance of dedicated servers is provided at the discretion of WFT, Inc.
                        In the case of erroneous instances or support issues extending beyond what we determine to be
                        within the realm of reasonable assistance, support is priced as follows:
                      </p><ul>
          <li> $49/hour</li>
        </ul><p></p>
                      <h4>Scheduled Maintenance and Downtime</h4>
                      <p>WFT will use its commercially reasonably efforts to provide services 24 hours a day, seven days
                        a week. Subscriber acknowledges that from time to time the services may be inaccessible or
                        inoperable for various reasons, including periodic maintenance procedures or upgrades
                        ("Scheduled Downtime"); or service malfunctions, and causes beyond WFT's control or which are
                        not reasonably foreseeable by WFT including the interruption or failure of telecommunications or
                        digital transmission links, hostile network attacks, or network congestion or other failures.
                        WFT will provide at least 48 hours advance notice to the subscriber for Scheduled Downtimes, and
                        will use commercially reasonable efforts to minimize any other disruption, inaccessibility
                        and/or inoperability of its web servers. WFT has no responsibility for downtime resulting from a
                        user's actions. </p>
                      <h4>Fees</h4>
                      <p>WFT Cloud will charge you the fees stated in your Order. When launching a WFT Cloud Server,
                        your credit card will be charged the full monthly subscription, for the price of the SAP
                        Landscape Server that was selected. WFT Cloud Servers are charged in monthly billing increments.
                        WFT Cloud uses a fixed-cost pricing model – in other words, your invoiced amount will be charged
                        in its entirety at the beginning of your monthly billing cycle. If you choose a month-to-month
                        contract, you will be charged based on your initial invoice and no discounts will be applicable
                        to such accounts. Discounts are offered for multi-month subscription for dedicated servers. If
                        you choose this option, you are bound to stay for the entire contracted period to avail the
                        discounted price. Should you choose to cancel your service prior to the end of your discounted
                        term, the remaining balance of the total amount will be billed and service will continue to be
                        active for the remainder of the contracted period. WFT Cloud does not offer pro-rated billing
                        services or pay-as-you-go option currently for the Express Cloud services.

                        Unless you have made other arrangements, WFT will charge your credit card with invoice amount as
                        follows: (i) for recurring fees, in advance, on or around the first day of each billing cycle,
                        and (ii) for non-recurring fees (such as fees for initial set-up, overages, compute cycle fees,
                        and domain name registration) on or around the date incurred, or on or around the first day of
                        the billing cycle that follows the date incurred. Unless otherwise agreed in the Order, your
                        billing cycle will be pre-paid monthly, beginning on the date that WFT Cloud first makes the
                        services available to you.
                        In some cases WFT Cloud will present an "Estimated Monthly Charge". The "Estimated Monthly
                        Charge" is provided as a courtesy to you to help budget your monthly expenses. However, the
                        Estimated Monthly Charge is a rough estimation of your charges based on your current usage and
                        will not be your guaranteed monthly charge. Many items can change your monthly charge,
                        including, but not limited to, backups, firewalls, bandwidth usage, support, domain names,
                        private networking and any other optional WFTCloud service which you choose to enable.
                        WFT Cloud may suspend all services (including services provided pursuant to any unrelated Order
                        or other agreement we may have with you) if our charges to your credit card are rejected for any
                        reason. WFT may charge interest on overdue amounts at 1.5% per month (or the maximum legal rate
                        if it is less than 1.5%). If any amount is overdue by more than thirty (30) days, and WFT brings
                        a legal action to collect, or engages a collection agency, you must also pay WFT's reasonable
                        costs of collection, including attorney fees and court costs. All fees are stated and will be
                        charged in US Dollars. Any "credit" that we may owe you, such as a credit for failure to meet a
                        service level guaranty, will be applied to fees due from you for services, and will not be paid
                        to you as a refund. Charges that are not disputed within sixty (60) days of the date charged are
                        conclusively deemed accurate. You must provide WFT with accurate factual information to help WFT
                        determine if any tax is due with respect to the provision of the Services, and if WFT is
                        required by law to collect taxes on the provision of the Services, you must pay WFT the amount
                        of the tax that is due or provide satisfactory evidence of your exemption from the tax. You
                        authorize WFT to obtain a credit report at any time during the term of the Agreement. Any credit
                        that we may owe you, such as a credit for a SLA remedy, will be applied to unpaid fees for
                        services or refunded to you, at our discretion.</p>
                      <h4>Cancellation &amp; Refunds</h4>
                      <p style="text-align:justify">
                        1: By beginning your subscription, you authorize WFT Cloud to charge you a monthly fee at the
                        designated rate.<br><br>

                        2: Your can manage your subscription through WFT Cloud website by logging to your account. Your
                        subscription will remain active unless you choose to terminate your service. Any email requests
                        or other forms of communication for cancellation will neither be honoured nor acknowledged as an
                        acceptance of cancellation.<br><br>

                        3: Your account will be charged on the day the order is processed and the next payment will
                        deducted in 30days from the first order is processed <br><br>

                        4: WFT Cloud will not be held responsible for the cancellation of the services managed by
                        customers.<br><br>

                        5: WFT Cloud will not provide any <b>refund</b> if the customer fails to cancel their services
                        through the WFT Cloud website before the next billing cycle.<br><br>

                        6: There are no refunds for partial used periods. Following any cancellation; however, you will
                        continue to have access to the service through the end of your current period. <br><br>

                        7: WFT has the rights to suspend your account any time incase of payment failure for the
                        subscription </p>
                      <h4>Account Setup, Termination, and Billing</h4>
                      <p>The account will be charged on the day the order is processed, regardless of the account
                        billing date. The account will not be charged again until the next month's billing cycle. </p>

                      <h4>Promotional Use</h4>
                      <p>WFT or WFTCloud may refer to You, Your company, or your logo's for promotional purposes. Your
                        company name, logos and the services that were provided to WFT may be used in promotional
                        materials, advertising, marketing releases, newsletter, public disclosures and on the WFT
                        website. This reference will be strictly limited to disclosure that WFT has provided services to
                        the company and will not contain any confidential, sensitive or proprietary information in such
                        a reference. The reference will also not provide any personally identifiable information about
                        the individual or technical information regarding the landscape and design used by the customer
                        at WFTCloud. However, WFT may disclose any information requested by law enforcement or when
                        compelled by court order, applicable laws or regulations.</p>
                      <p>Any work or professional services performed or provided by WFT under this Agreement shall not
                        be deemed .Work For Hire,. but WFT shall grant a non-exclusive, non-transferable license to You,
                        for the duration of this Agreement, its employees, affiliates, and third parties commissioned by
                        WFT.</p>

                      <h4>Microsoft Software License Policy.</h4>
                      <p>Microsoft, as well as WFT company policy, does not allow mixing of Microsoft license ownership.
                        For an example, a customer cannot use a WFT purchased Windows Server license in conjunction with
                        their own customer license of MSSQL or any other Microsoft Product. This is a violation of
                        Microsoft's licensing policies. The customer would have to obtain all Microsoft licenses through
                        WFT and this cost is factored in the Windows Server Purchase. In some instances, we can allow
                        the customer to supply all Microsoft licensed products, including the OS. However, all licenses
                        must be provided by the customer and may not be mixed with WFT licensed Microsoft products. Any
                        questions regarding this policy can be addressed to <a href="mailto:support@wftcloud.com">support@wftcloud.com</a>
                      </p>
                      <h4>Limitation of Damages</h4>
                      <p>Recovery of damages from WFT may not exceed the amount of fees it has collected on the
                        account.</p>
                      <h4>General</h4>
                      <ul style="padding-top:10px;">

                        <li>Terms Of Service are subject to change without any prior notification.</li>
                        <li>These Terms of Service are a legally binding contract between the subscriber and WFT, Inc.
                        </li>
                        <li> By opening an account, the subscriber agrees to the above-stated terms.</li>
                        <li> Anything not listed in the Terms of Service is open to interpretation and change by WFT,
                          Inc. administrators without prior notice.
                        </li>
                        <li> All prices, with the exception of the 30-day money back guarantee, are nonrefundable and
                          nonnegotiable.
                        </li>

                        <li> The 30-day money back guarantee does not pertain to Virtual Dedicated and Full Dedicated
                          server plans.
                        </li>
                        <li>
                          Any violation of these Terms of Service will result in termination of the account. WFT, Inc.
                          maintains the right to terminate accounts without prior notification.
                        </li>
                        <li>We reserve the right to remove any account with 15 days prior notice.</li>
                      </ul>
            <p>&nbsp;</p>
 <h4>Dispute Resolution</h4>
 <p>Any dispute between WFT and a subscriber shall be determined by arbitration conducted by the American Arbitration
   Association pursuant to its commercial arbitration rules. The arbitrator shall decide any dispute in accordance with
   State of New Jersey law, without the application of choice of law principles. Each party shall bear its own expenses
   and legal fees for the arbitration. The arbitration shall be conducted in Monmouth County , NJ, unless both parties
   agree in writing to a different location. The arbitration award is enforceable as a judgment of any court having
   proper jurisdiction.</p>

          </span>
      </div>
    </div>
  </div>
</section>

        <!-- /customer wrap -->
        <section class="testimonial grey-wrap global-sap">
  <div class="container">
    <div class="row">
      <div class="col-md-12">
        <div class="text-center">
          <h1>Our Customers</h1>
        </div>
      </div>
    </div>
    <hr class="divider">
    <div class="row p-a-md" style="background-color: #fff">
      <div class="marquee">
        <ul>
          <li>
            <img src="asset/images/customers/ADP.jpg">
            <img src="asset/images/customers/AMB.jpg">
            <img src="asset/images/customers/American-Standard.jpg">
            <img src="asset/images/customers/Amgen.jpg">
            <img src="asset/images/customers/Applied-Materials.jpg">
            <img src="asset/images/customers/AstraZeneca.jpg">
            <img src="asset/images/customers/barnes-and-noble.jpg">
            <img src="asset/images/customers/Basf.jpg">
            <img src="asset/images/customers/Bbraun.jpg">
            <img src="asset/images/customers/Bentley.jpg">
            <img src="asset/images/customers/Black-Decker.jpg">
            <img src="asset/images/customers/Bloomberg.jpg">

            <img src="asset/images/customers/blue cross-blue shield.jpg">
            <img src="asset/images/customers/BOEHRINGER_INGELHEIM.jpg">
            <img src="asset/images/customers/Braun.jpg">
            <img src="asset/images/customers/BRISTOL_MYERS_SQUIBB.jpg">
            <img src="asset/images/customers/Brother.jpg">
            <img src="asset/images/customers/cable_and_wireless.jpg">
            <img src="asset/images/customers/CALLAWAY.jpg">
            <img src="asset/images/customers/Canadian Pacific.jpg">
            <img src="asset/images/customers/cdc.jpg">
            <img src="asset/images/customers/cemex.jpg">
            <img src="asset/images/customers/CENTER_FOR_DISCEASE_CONTROL.jpg">
            <img src="asset/images/customers/Checkpoint.jpg">
            <img src="asset/images/customers/Clorox.jpg">
            <img src="asset/images/customers/Columbia.jpg">

            <img src="asset/images/customers/Commerce-Bank.jpg">
            <img src="asset/images/customers/Commercial-Metals.jpg">
            <img src="asset/images/customers/Computer-Horizons.jpg">
            <img src="asset/images/customers/CSC.jpg">
            <img src="asset/images/customers/DC-Superior.jpg">
            <img src="asset/images/customers/Disney.jpg">
            <img src="asset/images/customers/Equate.jpg">
            <img src="asset/images/customers/ERP-Logic.jpg">
            <img src="asset/images/customers/Everis.jpg">
            <img src="asset/images/customers/FIAT.jpg">
            <img src="asset/images/customers/Florida.jpg">
            <img src="asset/images/customers/Forest-Lab.jpg">
            <img src="asset/images/customers/General-Atomics.jpg">
            <img src="asset/images/customers/Grainger.jpg">
            <img src="asset/images/customers/GSA.jpg">
            <img src="asset/images/customers/GSK-Glaxo.jpg">
            <img src="asset/images/customers/HSBC.jpg">
            <img src="asset/images/customers/Intel.jpg">

            <img src="asset/images/customers/JAG.jpg">
            <img src="asset/images/customers/Jet-Blue.jpg">
            <img src="asset/images/customers/Johnson.jpg">
            <img src="asset/images/customers/JPmorgan.jpg">
            <img src="asset/images/customers/Kenneth-Cole.jpg">
            <img src="asset/images/customers/Loreal.jpg">
            <img src="asset/images/customers/Marriott.jpg">
            <img src="asset/images/customers/Mars-Incorporated.jpg">
            <img src="asset/images/customers/Merrill-Lynch.jpg">
            <img src="asset/images/customers/Milliken.jpg">
            <img src="asset/images/customers/Momentive.jpg">
            <img src="asset/images/customers/NASA.jpg">
            <img src="asset/images/customers/National-Semiconductor.jpg">
            <img src="asset/images/customers/National-Starch.jpg">
            <img src="asset/images/customers/Nestle.jpg">
            <img src="asset/images/customers/Neustar.jpg">
            <img src="asset/images/customers/Newyork-life.jpg">

            <img src="asset/images/customers/Nike.jpg">
            <img src="asset/images/customers/NorthRop.jpg">
            <img src="asset/images/customers/Pacificorp.jpg">
            <img src="asset/images/customers/Pemex.jpg">
            <img src="asset/images/customers/PepBoys.jpg">
            <img src="asset/images/customers/Pepsi.jpg">
            <img src="asset/images/customers/Petrobras.jpg">
            <img src="asset/images/customers/Pfizer.jpg">
            <img src="asset/images/customers/PSEG.jpg">
            <img src="asset/images/customers/PWC.jpg">
            <img src="asset/images/customers/Quadrant-4.jpg">
            <img src="asset/images/customers/Rockwell-Collins.jpg">
            <img src="asset/images/customers/ROHM-Haas.jpg">
            <img src="asset/images/customers/RUSS.jpg">
            <img src="asset/images/customers/Sanofi.jpg">
            <img src="asset/images/customers/Saudi-Aramco.jpg">
            <img src="asset/images/customers/Siemens.jpg">
            <img src="asset/images/customers/SMUD.jpg">

            <img src="asset/images/customers/Sony.jpg">
            <img src="asset/images/customers/Star-Media.jpg">
            <img src="asset/images/customers/Talk-America.jpg">
            <img src="asset/images/customers/Towers-Perrin.jpg">
            <img src="asset/images/customers/TransAlta.jpg">
            <img src="asset/images/customers/under-armour.jpg">
            <img src="asset/images/customers/Union-Carbide.jpg">
            <img src="asset/images/customers/United-Nations.jpg">
            <img src="asset/images/customers/UPS.jpg">
            <img src="asset/images/customers/US-Bancorp.jpg">
            <img src="asset/images/customers/US-Postal.jpg">
            <img src="asset/images/customers/Vastera.jpg">
            <img src="asset/images/customers/Verisig.jpg">
            <img src="asset/images/customers/West-Pharma.jpg">
            <img src="asset/images/customers/World-Bank.jpg">
            <img src="asset/images/customers/World-Kitchen.jpg">
            <img src="asset/images/customers/Wyeth.jpg">
          </li>
        </ul>
      </div>
    </div>
  </div>
</section>

        <!-- footer -->
        <footer class="moderate-blue global-sap">
  <div class="container">
    <div class="row">
      <div class="col-md-3">
        <div class="list-with-heading">
          <h4>Customers</h4>
          <ul>
            <li><a href="">Customer List</a></li>
            <li><a href="">Case Studies</a></li>
          </ul>
        </div>
        <div class="list-with-heading">
          <h4>Solutions</h4>
          <ul>
            <li><a href="">Enterprise IT</a></li>
            <li><a href="">Vertical Solutions</a></li>
            <li><a href="">Enterprise Applications</a></li>
            <li><a href="">SAP in the cloud</a></li>
            <li><a href="">Enterprise Risk Management </a></li>
            <li><a href="">Use Cases</a></li>
          </ul>
        </div>
      </div>
      <div class="col-md-3">
        <div class="list-with-heading">
          <h4>Technology</h4>
          <ul>
            <li><a href="">MVM (Micro VM)</a></li>
            <li><a href="">True Consumption</a></li>
            <li><a href="">Security & Compliance</a></li>
            <li><a href="">Performance, Availability & SLAs</a></li>
          </ul>
        </div>
      </div>
      <div class="col-md-3">
        <div class="list-with-heading">
          <h4>Company</h4>
          <ul>
            <li><a href="">Company Profile</a></li>
            <li><a href="">Team</a></li>
            <li><a href="">Affiliations</a></li>
            <li><a href="">Awards</a></li>
            <li><a href="">Careers</a></li>
            <li><a href="">Support</a></li>
            <li><a href="">Contact Us</a></li>
            <li><a href="">Buzz</a></li>
            <li><a href="">Blog</a></li>
          </ul>
        </div>
      </div>
      <div class="col-md-3">
        <div class="list-with-heading">
          <h4>Connect with us</h4>
          <ul class="social-media">
            <li><a href="#"><img src="asset/images/twitter-footer.jpg" alt=""></a></li>
            <li><a href="#"><img src="asset/images/linkedin-footer.jpg" alt=""></a></li>
            <li><a href="#"><img src="asset/images/youtube-footer.jpg" alt=""></a></li>
            <li><a href="#"><img src="asset/images/fb-footer.jpg" alt=""></a></li>
          </ul>
        </div>
      </div>
    </div>
  </div>
</footer>
        <section class="dark-blue copyright">
  <div class="container">
    <div class="row">
      <div class="col-md-12">
        <p>&copy; Copyright 2016. WFTCloud. All right reserved.</p>
      </div>
    </div>
  </div>
</section>


        <!-- Placed at the end of the document so the pages load faster -->
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
        <script src="asset/js/bootstrap.js"></script>
        <!-- <script src="asset/js/bootstrap-hover-dropdown.min.js"></script> -->
    </form>
</body>
</html>


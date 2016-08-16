<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd.Master" AutoEventWireup="true" CodeBehind="SAP-training.aspx.cs" Inherits="WFTCloud.User.SAP_training" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <!-- Including main template styles -->
        <link rel="stylesheet" href="/css/main.css">
      <script type="text/javascript">
          $(document).ready(function () {
              $("#ContentPlaceHolder1_trOthers").hide();
              $("#ContentPlaceHolder1_ddlHearAboutUs").change(function () {
                  if ($("#ContentPlaceHolder1_ddlHearAboutUs option:selected").text() == "Others") {
                      $("#ContentPlaceHolder1_trOthers").show();
                      $("#ContentPlaceHolder1_txtOthers").val('');
                  }
                  else {
                      $("#ContentPlaceHolder1_trOthers").hide();
                  }
              });
          });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

            <section id="content">
               
                <div class="white-section">
                    
                    <div class="container">

                        <div class="row">
                             
                             <div class="breadcrumb">
                                    <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" />
                                    <asp:SiteMapPath ID="SiteMap1" runat="server"></asp:SiteMapPath>
                                </div>
                            
                            <div class="span8">
                                <h4>SAP Training</h4>

                                <!--p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Magnam repellendus itaque aut adipisci perspiciatis magni aperiam sit reprehenderit. Officia earum.</p-->
                                
                                <div id="accordion" class="accordion">

                                    <div class="accordion-group">

                                        <div class="accordion-heading">

                                            <a class="accordion-toggle opened" data-toggle="collapse" data-parent="#accordion" href="#collapseOne"><strong>SAP HANA Administration / Migration Online Training</strong></a>

                                        </div><!-- accordion-heading end -->

                                        <div id="collapseOne" class="accordion-body collapse">

                                            <div class="accordion-inner">
                                            <h2>SAP HANA Administration / Migration Online Training </h2>
                                              <p> WFT now offers a comprehensive SAP HANA administration / migration online training. WFT's SAP HANA administration / migration online training course is aimed at enabling the trainees to be practically well versed (productive) & at the same time be familiar with Industry best practices & emerging trends. WFT's SAP HANA administration / migration online training course will be constantly upgraded / refined to maintain sync with Industry requirements. Experienced & Certified technologists deliver the training programs & mentor the trainees. WFT trainers combine technology expertise with cognitive understanding in ensuring each of the trainees is able to learn & apply complex of technologies with relative ease. WFT's SAP HANA administration / migration online training programs are offered in different formats & patterns to suit the convenience of the target audience.</p>
                                              <p>Experienced & Certified technologists deliver the training programs & mentor the trainees. WFT trainers combine technology expertise with cognitive understanding in ensuring each of the trainee is able to learn & apply complex of technologies with relative ease. A perfect mix of class room and practical lab experiments are offered in different formats & patterns to suit the target audience. </p>
                                              <h2>Professional SAP HANA Administration / Migration Online Training </h2>
                                             <p>SAP HANA takes in-memory computing to an exciting new level with a host of compelling features that SAP customers are scrambling for, but of course quality SAP HANA administration / migration online training will always come at a price. WFT’s SAP HANA administration / migration online training will be a game changing one with online and class room training available and FAST TRACK YOUR CAREER BY SURFING IN THIS SAP HANA WAVE. </p>
                                              <hr/><br/>
                                              <h2>SAP HANA Administration / Migration Training </h2>
                                              <h4>Syllabus Overview </h4>
                                              
<ul>
 <li>SAP HANA       Picture and Appliance</li>
  <li>HANA       Architecture </li>
  <li>HANA       Studio - Look and Feel </li>
  <li>HANA       Backup and Recovery </li>
  <li>SAP BW       Migration to HANA </li>
  <li>Implement       SAP NW 7.3 on HANA </li>
  <li>HANA       Deployment Scenario </li>
  <li>Future       roadmap on HANA </li>
</ul>
 
 <b>HANA online course details are as below:</b>
 <ul>
   <li>Duration : <strong>40 hours</strong></li>
  <li>Material       will be provided</li>
  <li>Training       fees includes server access</li>
  <li><strong>24 x 7</strong> HANA       System access during the training period</li>

 </ul>
<h4>Trainers</h4>
<p>Training will be provided by our expert staff of SAP certified migration consultants. We have the industry top experts with hands-on experience on SAP HANA who will deliver these online classes at the scheduled time. </p>
<p><strong>Who can benefit from this training? </strong>
<bR>
These courses are best suited for working professionals, experienced SAP consultants, and new graduates. 
</p>
<p><b>Method of Course Delivery </b>
<br>Instructor-led training will be provided on a scheduled basis at Chennai with hands on data migration exposure on real time scenarios. Please call us or send us an email to know more the class room training.</p>
<p>Webinar based training / Online training is available to provide more flexibility. The meeting details and conference numbers along with the batch schedule will be given after registration. Participants will receive HANA Training materials & other documents on HANA System administration, Security & Authorization,BW on HANA, etc. </p>
<p><strong>Other important details: Date, Time, Venue</strong> 
    
<br>
The venue, date and time will be provided subject to the course availability during registration. Please contact us to learn more details or send your inquires to <a href="mailto:training@wftcloud.com">training@wftcloud.com</a> or <strong>call us at 877-544-5543 , +91-94434 47775 or +91-78456 45678</strong>. <br /></p>
    <p class="split-para"><span style="float:left"><strong><a href="http://www.wftcloud.org" target="_blank">www.wftcloud.org</a></strong></span>
        <span style="float:right"><%--<asp:button text="Sign Up" ID="btn1SignUp" CssClass="btn btn-primary" style="background:#0065CC;" runat="server" OnClick="btn1SignUp_Click" />--%></span></p>


                                            </div><!-- accordion-inner end -->

                                        </div><!-- accordion-body end -->

                                    </div><!-- accordion-group end -->

                                    <div class="accordion-group">

                                        <div class="accordion-heading">

                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo"><strong>SAP Basis Administration - Online Training</strong></a>

                                        </div><!-- accordion-heading end -->

                                        <div id="collapseTwo" class="accordion-body collapse">

                                            <div class="accordion-inner">

                                              BASIS is a business application software integrated solution. Simply, BASIS is the administration of the SAP system. It's a piece of middle-ware which links the application with the database and the operating system. We at WFT Operate on installations, upgrade and migrations combined with the operational support for the customers which brings in a value add to the trainess gaining real time exposure and troubleshooting experience. 
                                              <h2>SAP BASIS Administration Training</h2>
                                              <ul>
                                                <li><strong>Syllabus Overvie</strong>w </li>
  <li>SAP Overview   </li>
  <li>Installation       of ECC 6.0</li>
  <li>SAP Basis       Technology Overview – I</li>
  <li>Basis System       and System Environment</li>
  <li>SAP       Basis Technology Overview - II</li>
  <li>R/3 start and stop processes for Unix and Windows       NT</li>
 <li>R/3 Installation Backup.</li>
</ul>
<p>
<b>Basis online course details are as below:</b>
<ul>
  <li>Duration : <strong>40 hours</strong></li>
  <li>Material will be provided</li>
  <li>Training fees includes server access</li>
  <li><strong>24 x 7</strong> System access during the training period for practice.&nbsp;</li>

</ul>
</p>
<p><strong>Trainers</strong>
<br>
Training will be provided by our expert staff of SAP certified BASIS/Migration consultants. We have the industry top experts with hands-on experience on SAP BASIS who will deliver these online classes at the scheduled time. </p>
<p><b>Who can benefit from this training?</b>
<br>These courses are best suited for working professionals, experienced SAP consultants, and new graduates. </p>
<p><b>Method of Course Delivery</b>
<br>Customized Instructor-led training will be provided on a scheduled basis at Chennai with hands on exposure to real time scenarios. Please call us or send us an email to know more the class room training.</p>
<p>Webinar based training / Online training is available to provide more flexibility. The meeting details and conference numbers along with the batch schedule will be given after registration. Participants will receive BASIS Training materials & other documents on BASIS Training.</p>
<p><b>Other important details: Date, Time, Venue</b> 
    
<br>The venue, date and time will be provided subject to the course availability during registration.</p>
<p> Please contact us to learn more details or send your inquires to <a href="mailto:training@wftcloud.com">training@wftcloud.com</a>, <a href="mailto:vrpathy@wftus.com">vrpathy@wftus.com</a> .     call us at 877-544-5543 , +91-94434 47775 or +91-78456 45678. </p>
<p class="split-para"><span style="float:left"><strong><a href="http://www.wftcloud.org" target="_blank">www.wftcloud.org</a></strong></span>
   <span style="float:right"><%--<asp:button text="Sign Up" ID="btn2SignUp" CssClass="btn btn-primary" style="background:#0065CC;" runat="server" OnClick="btn2SignUp_Click" />--%></span></p>

                                            </div><!-- accordion-inner end -->

                                        </div><!-- accordion-body end -->

                                    </div><!-- accordion-group end -->

                                    <div class="accordion-group">

                                        <div class="accordion-heading">

                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapseThree"><strong>Cloud Computing  Training</strong></a>

                                        </div><!-- accordion-heading end -->

                                        <div id="collapseThree" class="accordion-body collapse">

                                            <div class="accordion-inner">

                                                <p>WFT now offers comprehensive Cloud Computing Training. WFT's Cloud Computing Training course is aimed at enabling the trainees to be practically well versed (productive) & at the same time be familiar with Industry best practices & emerging trends. </p>
                                                <p>Experienced & Certified VM Ware technologists deliver the training programs & mentor the trainees. WFT trainers combine technology expertise with cognitive understanding in ensuring each of the trainees is able to learn & apply complex of technologies with relative ease. </p>
<h2>CLOUD COMPUTING- An Overview.</h2>
<p>Cloud computing is a colloquial expression used to describe a variety of computing concepts that involve a large number of computers that are connected through a real-time communication network (typically the Internet).Moving to the cloud is the next big thing which the IT world is talking about. There was a time when every business wanted to have the latest infrastructure in place for it provide the best in class IT services.</p>
<p>The Cloud offers flexibility in terms of Infrastructure (IaaS), Software (SaaS), Platform (Paas) and much more. A recent Gartner survey projects a global demand of 14 million jobs by the end of2016 with an expected market value of about $75 billion. With such a huge demand, cloud technicians and cloud savvy entrepreneurs are already much sought after. Wharfedale Technologies strongly believes that, by creating technically skilled professionals and placing them in the market, both the students and Wharfedale as a whole will have a strong hand in promoting the Cloud Technology as well as creating new rooms for R&D to bring up Cloud Computing to a whole new level</p>
<h2>     Cloud Computing Training content.</h2>
<ul>
  <li>Introduction to  Key Cloud and Networking Concepts</li>
  <li>Cloud Storage  &amp; Virtualization concepts (CSVC)</li>
  <li>Virtualization  Management (VM)</li>
  <li>Cloud  Configuration Management (CCM)</li>
</ul>

<h3> Cloud Computing course details are as below:</h3>
<ul>
<li>Duration : 40 hours</li>
  <li>Material will be  provided</li>
  <li>Training  fees includes server access</li>
  <li><strong>24 x 7</strong>   System/Server  access during the  training period</li>
</ul>
<h3>Trainers</h3>
<p>Training will be provided by our expert staff of VM Ware Consutants.. We have the industry top experts withhands-on experience on Cloud Computing, who will deliver these class room training at the scheduled time. </p>
<h3>Who can get trained? </h3>
<p>These courses are best suited for practicing Consultants, Young Engineers / MCA's, /Software graduates / other suitable graduates.</p>
<h3>	
Method of Course Delivery</h3>
<p> Class Room  training will provide with more flexibility. The training details and other relevant particulars along with the batch schedule will be given after registration. Participants will receive Training materials & other documents.
</p>
<h3>Other important details: Date, Time, Venue
                                                </h3>
<p>The venue, date and time will be provided subject to the course availability during registration. Please contact us to learn more details or send your inquires to <a href="mailto:training@wftcloud.com">training@wftcloud.com</a>, <a href="mailto:vrpathy@wftus.com">vrpathy@wftus.com</a> or call us at 001-877-544-5543, +91-94434 47775 or +91-78456 45678. </p>
<p class="split-para"><span style="float:left"><strong><a href="http://www.wftcloud.org" target="_blank">www.wftcloud.org</a></strong></span>
    <span style="float:right"><%--<asp:button text="Sign Up" ID="btn3SignUp" CssClass="btn btn-primary" style="background:#0065CC;" runat="server" OnClick="btn3SignUp_Click" />--%></span></p>


                                            </div><!-- accordion-inner end -->

                                        </div><!-- accordion-body end -->

                                    </div><!-- accordion-group end -->

                                </div><!-- accordion end -->

                            </div><!-- span8 end -->
                            
                            <div class="span4">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 50%;">
                                            <div>
                                                <h4>Register:</h4>
                                            </div>
                                            <table>
                                                <tr>
                                                    <td colspan="2">
                                                        <div id="divRegisterSuccess" runat="server" visible="false" class="alert alert-success ">
                                                            <i class="icon-remove"></i>
                                                            <asp:Label ID="lblRegisterSuccess" Text="Email Send Successfully" runat="server"></asp:Label>
                                                            <span>Click here to <a href="LoginPage.aspx"><u>login.</u></a></span>
                                                        </div>
                                                        <div id="divRegisterError" runat="server" visible="false" class="alert alert-error ">
                                                            <i class="icon-remove"></i>
                                                            <asp:Label ID="lblRegisterError" Text="" runat="server" ForeColor="Red"></asp:Label>
                                                        </div>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="width: 30%; vertical-align: top;">First Name
          <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="input-medium" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td style="width: 30%; vertical-align: top;">Middle Name
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtMiddleName" runat="server" CssClass="input-medium" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 30%; vertical-align: top;">Last Name
            <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtLastName" runat="server" CssClass="input-medium" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" style="width: 30%; vertical-align: top;">Email ID
            <asp:RequiredFieldValidator ID="rfvEmailID" runat="server" ControlToValidate="txtEmailID" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revEmailID" runat="server" ControlToValidate="txtEmailID" ErrorMessage="*" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtEmailID" runat="server" CssClass="input-medium" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="width: 30%; vertical-align: top;">Password
                                                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtRegPassword" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtRegPassword" runat="server" CssClass="input-medium" AutoCompleteType="Disabled" autocomplete="off" TextMode="Password"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 30%; vertical-align: top;">Confirm Password
                                                        <asp:RequiredFieldValidator ID="rfvConfPassword" runat="server" ControlToValidate="txtConfPassword" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtConfPassword" runat="server" CssClass="input-medium" AutoCompleteType="Disabled" autocomplete="off" TextMode="Password"></asp:TextBox><br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 30%; vertical-align: top;">Geographic location<asp:RequiredFieldValidator ID="rfvCountry" runat="server" ErrorMessage="*" ControlToValidate="ddlCountry" InitialValue="0" ForeColor="Red"  ></asp:RequiredFieldValidator>
                                                   </td>
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
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 30%; vertical-align: top;">How did you hear about us?
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlHearAboutUs" runat="server" Width="165px">
                                                            <asp:ListItem Text="Google" Value="Google"></asp:ListItem>
                                                            <asp:ListItem Text="Facebook" Value="Facebook"></asp:ListItem>
                                                            <asp:ListItem Text="LinkedIn" Value="LinkedIn"></asp:ListItem>
                                                            <asp:ListItem Text="Twitter" Value="Twitter"></asp:ListItem>
                                                            <asp:ListItem Text="Press Release" Value="Press Release"></asp:ListItem>
                                                            <asp:ListItem Text="Others" Value="Others"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>

                                                </tr>
                                                <tr runat="server" id="trOthers">
                                                    <td></td>
                                                    <td>
                                                        <asp:TextBox ID="txtOthers" runat="server" placeholder="Other referral" CssClass="input-medium"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <input type="checkbox" id="chkAgree" runat="server" checked="checked" />&nbsp;&nbsp;I agree to the Terms of Service
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <br />
                                                        <asp:Button ID="btnRegisterCode" CssClass="btn btn-primary" runat="server" Text="Register" OnClick="btnRegisterCode_Click"/>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:RegularExpressionValidator ID="rvUserPassword" runat="server" ControlToValidate="txtRegPassword" ErrorMessage="Password should be at least 6 characters in length.&lt;br/&gt;" ForeColor="Red" ValidationExpression=".{6}.*" ValidationGroup="save1"></asp:RegularExpressionValidator>
                                                        <asp:CompareValidator ID="cvConfPassword" runat="server" ControlToCompare="txtRegPassword" ControlToValidate="txtConfPassword" ErrorMessage="Your New Password & Confirm Password did not match" ForeColor="Red"></asp:CompareValidator>
                                                    </td>
                                                </tr>
                                            </table>

                                        </td>
                                    </tr>
                                </table>
                                <%--<img src="/img/page-images/server.png" alt="Server" class="pull-down">--%>

                                <!--p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Similique consectetur eaque vero nesciunt consequuntur reiciendis sunt fugit quos saepe corrupti tempore sint architecto dolores a quaerat facilis pariatur.</p-->

                            </div><!-- span4 end -->

                        </div><!-- row end -->
                        
                        
                        <!-- row end -->   

                        <!--div class="row">
                            
                            <div class="span12">
                                
                                <div class="well clearfix">
                                    
                                    <p>If you need a <strong>custom plan</strong> simply contact with us <p class="visible-desktop">and we will provide it for you.</p></p>

                                    <a href="/user/contactus.aspx" class="button simple arrow pull-right">Get custom quote</a>

                                </div><!-- message-box end -->

                                <!--p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Totam ipsam cumque officiis eos id facere esse libero possimus ut dolores tempore voluptates natus quia. Numquam ipsa dolores placeat earum ratione facilis commodi rerum odit sint expedita aliquid similique quisquam enim fugiat non vitae eos necessitatibus itaque neque quidem ipsam perspiciatis nesciunt sit sunt dolor quae natus assumenda sed quod quasi.</p>

                            </div--><!-- span12 end -->

                        <!--/div><!-- row end -->     

                    </div><!-- conteiner end -->

                </div><!-- white-section end -->

            </section><!-- content end -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerSEOContent" runat="server">
      <hr />
      <div class="row">
        <div class="span12">
             <h2 style="font-size:12px;">WFTC offers SAP HANA administration / migration online training!</h2>
              	<p>WFT now offers a comprehensive SAP HANA administration / migration online training. WFT's SAP HANA administration / migration online training course will fast-track your SAP career. Call Now!!!<p>
                <h2 style="font-size:12px;">WFT’s Professional & Comprehensive SAP HANA administration / migration online training!</h2>
                <p>WFT now offers a comprehensive SAP HANA administration / migration online training for the aspiring SAP Professional. WFT's SAP HANA administration / migration online training course will help aspiring SAP professionals to ride the SAP HANA Wave. Contact Us Now!<p>

        <%--<p style="font-size:12px;"><strong>WFTC offers SAP HANA administration / migration online training!</strong><br>
WFT now offers a comprehensive SAP HANA administration / migration online training. WFT's SAP HANA administration / migration online training course will fast-track your SAP career. Call Now!!!<p>
                <p style="font-size:12px;"><strong>WFT’s Professional & Comprehensive SAP HANA administration / migration online training!</strong><br>
WFT now offers a comprehensive SAP HANA administration / migration online training for the aspiring SAP Professional. WFT's SAP HANA administration / migration online training course will help aspiring SAP professionals to ride the SAP HANA Wave. Contact Us Now!<p>
        --%>
    </div>
      </div>
</asp:Content>
<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd.Master" AutoEventWireup="true" CodeBehind="SAP-HANA-Administration.aspx.cs" Inherits="WFTCloud.User.SAP_HANA_Administration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <!-- Including main template styles -->
        <link rel="stylesheet" href="/css/main.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <asp:ScriptManager ID="scriptManager" runat="server"></asp:ScriptManager>
    <section id="content">
               
                <div class="white-section">
                    
                    <div class="container">

                        <div class="row">
                            <div class="breadcrumb">
                                    <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" />
                                    <asp:SiteMapPath ID="SiteMap1" runat="server"></asp:SiteMapPath>
                                </div>
                            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                           <%-- <div class="span8">
                                
                               <!-- <img src="/img/page-images/server2.png" alt="Server" class="pull-right">-->

                                <h3>SAP HANA Administration / Migration Online Training </h3>
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
       
                            </div><!-- span8 end -->

                            <div class="span4">
                            	<h6>Other Links</h6>
                            	<ul class="otherlinks">
                                    <li><a href="/user/SAP-Basic-Administration.aspx">SAP Basis administration-online Training</a></li>
                                    <li><a href="/user/SAP-Cloud-computing.aspx">Cloud Computing</a></li>
                                </ul>
                            </div>--%>
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
                                                            <asp:Label ID="lblRegisterSuccess" Text="Admin has been notified about your training request, our administrator will contact you earlier." runat="server"></asp:Label>
                                                            <span></span>
                                                        </div>
                                                    </td>
                                                    </tr>
                                                <tr>
                                                    <td style="width: 30%; vertical-align: top;">First Name
                                                    <asp:RequiredFieldValidator ID="rfvFirstName" ValidationGroup="Register" runat="server" ControlToValidate="txtFirstName" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
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
                                                     <asp:RequiredFieldValidator ID="rfvLastName" ValidationGroup="Register" runat="server" ControlToValidate="txtLastName" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtLastName" runat="server" CssClass="input-medium" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" style="width: 30%; vertical-align: top;">Email ID
                                                        <asp:RequiredFieldValidator ID="rfvEmailID" runat="server" ValidationGroup="Register" ControlToValidate="txtEmailID" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revEmailID" runat="server" ValidationGroup="Register" ControlToValidate="txtEmailID" ErrorMessage="*" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtEmailID" runat="server" CssClass="input-medium" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" style="width: 30%; vertical-align: top;">Course
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Register" ControlToValidate="ddlCourse" InitialValue="0" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:UpdatePanel ID="CourseUpdatePanel" runat="server">
                                                                            <ContentTemplate>
                                                        <asp:DropDownList ID="ddlCourse" runat ="server" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                       </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td valign="top" style="width: 30%; vertical-align: top;">Module
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Register" ControlToValidate="ddlModule" InitialValue="0" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                     <asp:UpdatePanel ID="ModuleUpdatePanel" runat="server">
                                                                            <ContentTemplate>
                                                         <asp:DropDownList ID="ddlModule" runat ="server" >
                                                        </asp:DropDownList>
                                                                                 </ContentTemplate>
                                                                            <Triggers>
                                                                                <asp:AsyncPostBackTrigger ControlID="ddlCourse" />
                                                                            </Triggers>
                                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 30%; vertical-align: top;">Contact Number
                                                        <asp:RequiredFieldValidator ID="rfv" runat="server" ValidationGroup="Register" ControlToValidate="txtContactNumber" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtContactNumber" ErrorMessage="*" ForeColor="Red" ValidationExpression="^\+?[0-9]+$" ValidationGroup="Register"></asp:RegularExpressionValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtContactNumber" runat="server" CssClass="input-medium"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 30%; vertical-align: top;">City
                                                        <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ErrorMessage="*" ValidationGroup="Register" ControlToValidate="txtCity"  ForeColor="Red"  ></asp:RequiredFieldValidator>
                                                   </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCity" runat="server" CssClass="input-medium"></asp:TextBox>
                                                        
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 30%; vertical-align: top;">State
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ValidationGroup="Register" ControlToValidate="txtState"  ForeColor="Red"  ></asp:RequiredFieldValidator>
                                                   </td>
                                                    <td>
                                                       <asp:TextBox ID="txtState" runat="server" CssClass="input-medium"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td style="width: 30%; vertical-align: top;">Country
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ValidationGroup="Register" ControlToValidate="txtCountry"  ForeColor="Red"  ></asp:RequiredFieldValidator>
                                                   </td>
                                                    <td>
                                                       <asp:TextBox ID="txtCountry" runat="server" CssClass="input-medium"></asp:TextBox>
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
                                                 <tr>
                                                    <td style="width: 30%; vertical-align: top;">Comments
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Register" ControlToValidate="txtComments" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtComments" runat="server" CssClass="input-medium" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <br />
                                                        <asp:Button ID="btnRegisterCode" CssClass="btn btn-primary" runat="server" Text="Register" OnClick="btnRegisterCode_Click" ValidationGroup="Register"/>
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

            </section><!-- content end -->


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerSEOContent" runat="server">
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd.Master" AutoEventWireup="true" CodeBehind="ContactUs.aspx.cs" Inherits="WFTCloud.User.ContactUs" MaintainScrollPositionOnPostback="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <!-- Including main template styles -->
        <link rel="stylesheet" href="/css/main.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <section id="content">
               
                <div class="white-section contact">
                    
                    <div class="container">

                        <div class="row">
                                 <div class="breadcrumb">
                                    <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" />
                                    <asp:SiteMapPath ID="SiteMap1" runat="server"></asp:SiteMapPath>
                                </div>
                            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                            <%--<div class="span7">
                              
                                
                                <h4>Contact Wharfedale Technologies</h4>
                                <h2 style="font-size:12px">– A certified provider of Web based ERP system and software solutions</h2>
                                <p>Wharfedale Technologies corporate office is located in Central New Jersey, within easy reach of New York City and Philadelphia. Wharfedale also has one office dedicated to our Federal Division which is located in Northern Virginia. WFTCloud is an initiative by Wharfedale Technologies, Inc. providing web based ERP system & software solutions on the Cloud. We are a certified provider of web-based ERP software solutions by SAP. </p>

                                <h4>Contact info</h4>

                                                                <ul class="pull-left"><h6>USA</h6>
                                    <li><img src="/img/company-s.jpg" /> Wharfedale Technologies Inc. (HQ)</li>
                                    <li><img src="/img/address-s.jpg" /> 2850 Brunswick Pike,
                                    <br />Lawrenceville, NJ 08648</li>
                                    <li><img src="/img/phone-ss.jpg" /> Phone:  609-882-8826 </li>
                                    <li><img src="/img/email-ss.jpg" /> Email: <a href="mailto:wftsales@wftus.com">wftsales@wftus.com</a></li>
                                    
                                </ul>
                                
                                <ul class="pull-right"><h6>Canada</h6>
                                    <li><!--<img src="/img/company-s.jpg" />--><img src="/img/company-s.jpg" /> Wharfedale Technologies Inc.</li>
                                    <li><!--<img src="/img/address-s.jpg" />--><img src="/img/address-s.jpg" /> 888 3rd Street SW Suite 1000
                                    <br>Calgary, Alberta, T2P 5C5</li>
                                    <li><img src="/img/phone-ss.jpg" /> Phone:  403-444-5550 </li>
                                    <li><img src="/img/email-ss.jpg" /> Email: <a href="mailto:wftsales@wftus.com">wftsales@wftus.com</a></li>
                                    
                                </ul>
                                <div style="clear:both;"><hr><br></div>
                                <h6> India</h6>
                                <ul class="pull-left">
                                    <li><img src="/img/company-s.jpg" /> Wharfedale Technologies Inc.</li>
                                    <li><img src="/img/address-s.jpg" />#40,Ground Floor, <br>SP Infocity
M G R Salai, Perungudi,<br>Chennai - 600096, <br>Tamil Nadu </li>
                                    
                                    <li><img src="/img/phone-ss.jpg" /> Phone: +91 44 4398 9900 </li>
                                    <li><img src="/img/phone-ss.jpg" /> Phone: +91 44 4398 9999 </li>
                                    <li><img src="/img/email-ss.jpg" /> Email: <a href="mailto:wftsales@wftus.com">wftsales@wftus.com</a></li>
                                    
                                </ul>
                                <ul class="pull-right">
                                    <li><img src="/img/company-s.jpg" /> Wharfedale Technologies Inc.</li>
                                    <li><img src="/img/address-s.jpg" /> #721/5, Trichy Road, <br>Ramanathapuram,
                                    <br>

Coimbatore - 641045, <br>Tamil Nadu</li>
                                    <li><img src="/img/phone-ss.jpg" /> Phone:  +91 78456 45678</li>
                                    <li><img src="/img/email-ss.jpg" /> Email: <a href="mailto:wftsales@wftus.com">wftsales@wftus.com</a></li>
                                    
                                </ul>

                                 <div style="clear:both;"><hr><br></div>

                            </div><!-- span6 end -->--%>
                            
                            <div class="span5">

                                <div ><iframe width="425" height="400" frameborder="0" scrolling="no" marginheight="0" marginwidth="0" src="https://maps.google.com/maps?q=Wharfedale+Technologies,+2850+US+Highway+1+%23+2,+Lawrenceville,+NJ+08648&amp;hl=en&amp;sll=46.980252,-79.453125&amp;sspn=24.246317,86.572266&amp;hq=Wharfedale+Technologies,+2850+US+Highway+1+%23+2,&amp;hnear=Lawrenceville,+Lawrence+Township,+Mercer,+New+Jersey&amp;t=m&amp;ie=UTF8&amp;cid=17621828771337059437&amp;source=embed&amp;ll=40.269343,-74.712063&amp;spn=0.002865,0.00456&amp;z=17&amp;iwloc=A&amp;output=embed"></iframe></div>
                                <p>&nbsp;</p>
<div class="pull-right" style="padding-left:15px;">
                                 <h6>How Can WFT Help You? 1-888-533-3113 </h6>
                                 <p> Give us a call or e-mail to discuss all of the available WFT solutions and how they will benefit your organization.</p>
                                 Sales &amp; Information :
                 <a href="mailto:wftsales@wftus.com">wftsales@wftus.com</a>
                 <br>Customer Service :
             <a href="mailto:admin@wftcloud.com">admin@wftcloud.com</a>
             <br>Technical Support : 
             <a href="mailto:support@wftcloud.com">support@wftcloud.com</a>
             <br>Training:   
            <a href="mailto:training@wftcloud.com">training@wftcloud.com</a>
            </div>

                            </div><!-- span6 end -->

                        </div><!-- row end -->

                        <div class="row">

                            <div class="span12" id="contactForm" name="ContactForm">
                                    <p>Your name:
                                    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtContactName" ErrorMessage="*" ForeColor="Red" ></asp:RequiredFieldValidator>
                                    </p>
                                    <input type="text" id="txtContactName" runat="server" class="input-box" name="user-name" placeholder="Please enter your name."/>
                                    <p>Email address:
                                    <asp:RequiredFieldValidator ID="rfvEmailID" runat="server" ControlToValidate="txtEmailAddress" ErrorMessage="*" ForeColor="Red" ></asp:RequiredFieldValidator>
                                    </p>
                                    <input type="text" id="txtEmailAddress" runat="server" class="input-box" name="user-email" placeholder="Please enter your email address."/>
                                    <asp:RegularExpressionValidator ID="revEmailID" ControlToValidate="txtEmailAddress" 
                                    runat="server" ErrorMessage="Invalid Email ID" ForeColor="Red" 
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                    ValidationGroup="register"></asp:RegularExpressionValidator>
                                    <p>Subject:
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSubject" ErrorMessage="*" ForeColor="Red" ></asp:RequiredFieldValidator>
                                    </p>
                                    <input type="text" id="txtSubject" runat="server" class="input-box" name="user-subject" placeholder="Purpose of this message."/>
                                    <p class="right-message-box">Message:
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMessage" ErrorMessage="*" ForeColor="Red" ></asp:RequiredFieldValidator>
                                    </p>
                                    <textarea class="input-box right-message-box message-box" id="txtMessage" runat="server" name="user-message" placeholder="Your message."></textarea>
                                    <asp:Button ID="btnSubmit" Text="Send your message" runat="server" style="height:50px; margin-top:-60px; width:48%;" 
                                        CssClass="btn btn-primary" onclick="btnSubmit_Click" />
                                    <br />
                                    <asp:Label ID="lblSuccess" runat="server" ForeColor="Green"></asp:Label>

                            </div><!-- span12 end -->
                        
                        </div><!-- row end -->

                    </div><!-- container end -->

                </div><!-- white-section end -->

            </section><!-- content end -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerSEOContent" runat="server">
      <hr />
      <div class="row">
        <div class="span12">
            	<h2 style="font-size:12px;">A certified provider of Web based ERP system and software solutions</h2>             
                ------
                <h2 style="font-size:12px;">WFTCloud offers cloud ERP solutions and cloud CRM solutions!</h2>
              	<p style="font-size:12px;">WFTCloud.com offers cloud ERP solutions, cloud CRM solution & SAP on the cloud support services at an unmatched cost. Utilize WFT's expertise in cloud ERP solutions, cloud CRM solutions & SAP on the cloud support services for your business. Call Now!!!<p>
                <h2 style="font-size:12px;">Pay per use model for ERP on the cloud / ERP in the cloud.</h2>
                <p style="font-size:12px;">We focused on reducing your ERP implementation cost by introducing pay per use model for ERP on the cloud / ERP in the cloud & SAP on cloud support services. To know more about our pricing packages for ERP on the cloud / ERP in the cloud & SAP on cloud support services, Contact Us Now!  <p>
                <h2 style="font-size:12px;">Affordable & low cost ERP cloud service system with SAP certified provider.</h2>
                <p style="font-size:12px;">Get affordable & low cost ERP cloud service system with SAP certified provider - WFTCloud. Get implementation of ERP cloud services & systems with our ERP on the cloud / ERP in the cloud solutions, systems & services at a fraction of conventional cost.<p>

    <%--    <p style="font-size:12px;"><strong>WFTCloud offers cloud ERP solutions and cloud CRM solutions!</strong><br>
WFTCloud.com offers cloud ERP solutions, cloud CRM solution & SAP on the cloud support services at an unmatched cost. Utilize WFT's expertise in cloud ERP solutions, cloud CRM solutions & SAP on the cloud support services for your business. Call Now!!!<p>
                <p style="font-size:12px;"><strong>Pay per use model for ERP on the cloud / ERP in the cloud.</strong><br>
We focused on reducing your ERP implementation cost by introducing pay per use model for ERP on the cloud / ERP in the cloud & SAP on cloud support services. To know more about our pricing packages for ERP on the cloud / ERP in the cloud & SAP on cloud support services Contact Us Now!  <p>
                <p style="font-size:12px;"><strong>Affordable & low cost ERP cloud service system with SAP certified provider.</strong><br>
Get affordable & low cost ERP cloud service system with SAP certified provider - WFTCloud. Get implementation of ERP cloud services & systems with our ERP on the cloud / ERP in the cloud solutions, systems & services at a fraction of conventional cost.<p>
        --%>
        </div>
      </div>
</asp:Content>
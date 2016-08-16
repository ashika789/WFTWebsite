<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd.Master" AutoEventWireup="true" CodeBehind="SAP-High-Availablity.aspx.cs" Inherits="WFTCloud.User.SAP_High_Availablity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Including main template styles -->
        <link rel="stylesheet" href="/css/main.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="content">
               
                <div class="white-section">
                    
                    <div class="container">

                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                        <%--<div class="row">
                <div class="span3">
                    <h6>Enterprise Cloud</h6>
                            	<ul class="otherlinks">
                                	<li><a href="/user/private-cloud-on-premise.aspx">Private Cloud - on Premise</a></li>
                                    <li><a href="/user/SAP-HANA-Services.aspx">SAP HANA</a></li>
                                    <li><a href="/user/SAP-remote-basis-Support.aspx">SAP Remote Basis Support</a></li>
                                    <li><a href="/User/disaster-recovery-service.aspx">SAP Disaster Recovery</a></li>
                                    <li><a href="/user/SAP-infra-services.aspx">SAP Infrastructure Services</a></li>
                                    <li><a href="/user/SAP-migration.aspx">SAP Migration</a></li>   
                                    <li><a href="/user/Services.aspx">SAP Data Protection Services</a></li>
                                    <li><a href="/user/SAP-High-Availablity.aspx">SAP High Availability</a></li>                                 
                                </ul>   
                      
                </div><!-- span4 end -->
                            <div class="span9">
                            <h5>SAP High Availability</h5>
                            <p>WFT will design and implement high-availability solutions for complex SAP environments. WFT has expertise in implementing SAP HA solutions on various UNIX and Windows platforms using SQL, DB2 and ORACLE databases. WFT has designed implemented solutions using, HACMP, MC Service guard, VERITAS VCS and MSCS.</p>

                            <h6>SAP Replicated Enqueue with High Availability</h6>
                            <p>A typical high availability solution in a SAP environment does not address the failure of the enqueue service. Leveraging the firm's deep expertise in the SAP application and the associated infrastructure components, the SAP Replicated Enqueue with High Availability solution facilitates the elimination of single points of failure in a SAP environment at the operating system, storage, network, database and application level, thereby minimizing downtime in the customer's environment. This solution enables seamless failover of the enqueue service along with the rest of the components to achieve true high availability in the SAP landscape.</p>

                            <h6>SAP Integration with VCS, MC Service Guard and HACMP</h6>
                            <p>High Availability within an SAP or Oracle landscape is a requirement. WFT's SAP High availability solution addresses the different Single Points of Failure within a SAP Landscape like the Message server, Enqueue server and Web dispatcher. WFT solutions also integrate DB and Infrastructure tools to provide application availability as defined by your company's SLA's. Our engineers look to employ all available technologies including SAP Replicated En-Queue, Oracle RAC and server clustering to achieve high availability.</p>
                          
                            </div><!-- span9 end -->

                        </div><!-- row end -->--%>

                    </div><!-- conteiner end -->

                </div><!-- white-section end -->

            </section><!-- content end -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerSEOContent" runat="server">
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd.Master" AutoEventWireup="true" CodeBehind="EnterpriseCloud.aspx.cs" Inherits="WFTCloud.EnterpriseCloud" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <!-- Including main template styles -->
       <link rel="stylesheet" href="/css/main.css">
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
                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
             <%--   <div class="span3">
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

                    <!--p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Similique consectetur eaque vero nesciunt consequuntur reiciendis sunt fugit quos saepe corrupti tempore sint architecto dolores a quaerat facilis pariatur.</p-->

                </div><!-- span4 end -->--%>
                <%--<div class="span9">                            
                    <h4>Enterprise Cloud</h4>
                    <!--p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Magnam repellendus itaque aut adipisci perspiciatis magni aperiam sit reprehenderit. Officia earum.</p-->
                                
                    <div id="accordion" class="accordion">

                        <div class="accordion-group">

                            <div class="accordion-heading">

                                <a class="accordion-toggle opened" data-toggle="collapse" data-parent="#accordion" href="#collapseOne"><strong>WFT-Cloud Enterprise Cloud Services for SAP Applications</strong></a>

                            </div><!-- accordion-heading end -->

                            <div id="collapseOne" class="accordion-body collapse in">

                                <div class="accordion-inner">

                                    <p>The leading SAP Cloud Premier partner with the implementation experience of SAP private, and Hybrid Cloud.</p>
                                    <p>Opportunity, growth, and change are accelerating relentlessly, placing enormous pressure on companies to deliver services instantly and efficiently. To meet these challenges and maximize these opportunities, you need a new approach. WFT and SAP worked jointly to create this new model where IT resources are shared and delivered dynamically for SAP Applications. To increase the value of your investment in SAP Applications you need to break free from traditional models of isolated computing and take advantage of a virtual private cloud.</p>
                                    <p>The WFT Cloud Services for SAP Applications will provide the flexibility to manage your IT budgets without making a large capital investment. The solution includes infrastructure, cloud hosting and managed services that is highly standardized, flexible, and operational optimized that enables rapid provisioning and scaling.</p>

                                </div><!-- accordion-inner end -->

                            </div><!-- accordion-body end -->

                        </div><!-- accordion-group end -->

                        <div class="accordion-group">

                            <div class="accordion-heading">

                                <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo"><strong>Application transformation to cloud</strong></a>

                            </div><!-- accordion-heading end -->

                            <div id="collapseTwo" class="accordion-body collapse">

                                <div class="accordion-inner">

                                    <p>WFT Cloud offers a robust set of cloud transformation services to support customer road map to the cloud. WFT Cloud works with customer business and IT organization to align business and IT in cloud strategy.</p>
                                    <ul>
                                        <li><strong>SAP Assessment Services</strong> - Understand business needs for cloud deployment and define a road map</li>
                                        <li><strong>Planning Services</strong> - Create a plan that addresses a cloud model that reflects your business needs</li>
                                        <li><strong>Implementation Services</strong> - Build customer global private cloud model and deploy the solution as laid out by the model</li>
                                        <li><strong>Managed Services</strong> - Run and manage customer SAP Private Cloud </li>
                                    </ul>
                                    <p>Each option has certain attributes that translate into a unique combination of cost, benefits, and risks based on an enterprise’s situation. It is important to assess the individual and cumulative risks. Introducing third parties into the supply chain increases dependencies; failure on the part of a provider in a critical element of the business can be debilitating. The existing enterprise solution landscape can also be a constraining factor.</p>
                                    <p>WFT understands the complexity and created this new cloud service to fit customer solution landscape which aligns to long term strategies -business, operational, and technology to have sustained value.</p>

                                </div><!-- accordion-inner end -->

                            </div><!-- accordion-body end -->

                        </div><!-- accordion-group end -->

                        <div class="accordion-group">

                            <div class="accordion-heading">

                                <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapseThree"><strong>Worldwide 24/7 support via mail, phone or live chat</strong></a>

                            </div><!-- accordion-heading end -->

                            <div id="collapseThree" class="accordion-body collapse">

                                <div class="accordion-inner">

                                    <p>WFT Cloud is a Certified SAP Hosting Partner as well as Cloud Services Partner. WFT leverages SAP best practices on operating SAP environments to deliver high-quality services in support of SAP solutions to their clients. </p>
                                    <p>Please call 1-888-533-3113, or email sales@wftus.com or sales@wftcloud.com for more information. </p>

                                </div><!-- accordion-inner end -->

                            </div><!-- accordion-body end -->

                        </div><!-- accordion-group end -->

                    </div><!-- accordion end -->

                </div><!-- span8 end -->--%>


            </div><!-- row end -->
                        
            <br><hr><br>

            <%--<div class="row center">
 
                <div class="span4">

                    <h4>Private Cloud - on Premise</h4>
                     <a href="/user/private-cloud-on-premise.aspx">
                    <div class="huge-round-icon"><i class="icon-cloud"></i></div>
                    </a>
                    <!--p>Lorem ipsum dolor sit amet, consectetur pers piciatis culpa praes entium cupid.</p-->
                    <a href="/user/private-cloud-on-premise.aspx" class="button simple arrow">Read More</a>
                                
                </div><!-- span3 end -->

                <div class="span4">
                                
                    <h4>SAP HANA</h4>
                    <a href="/user/SAP-HANA-Services.aspx">
                    <div class="huge-round-icon"><i class="icon-cloud"></i></div>
                    </a>
                    <!--p>Lorem ipsum dolor sit amet, consectetur pers piciatis culpa praes entium cupid.</p--> 
                    <a href="/user/SAP-HANA-Services.aspx" class="button simple arrow">Read More</a>

                </div><!-- span3 end -->    

                <div class="span4">

                    <h4>SAP Remote Basis Support</h4>
                    <a href="/user/SAP-remote-basis-Support.aspx">
                    <div class="huge-round-icon"><i class="icon-cloud"></i></div>
                    </a>
                    <!--p>Lorem ipsum dolor sit amet, consectetur pers piciatis culpa praes entium cupid.</p-->
                    <a href="/user/SAP-remote-basis-Support.aspx" class="button simple arrow">Read More</a>
                                
                </div><!-- span3 end -->

                <!--div class="span3">
                                
                    <h4>Reseller hosting</h4>
                    <div class="huge-round-icon"><i class="icon-refresh"></i></div>
                    <p>Lorem ipsum dolor sit amet, consectetur pers piciatis culpa praes entium cupid.</p> 
                    <a href="reseller-hosting.html" class="button simple arrow">Reseller hosting plans</a>

                </div--><!-- span3 end -->  
                        
            </div><!-- row end -->
                        
            <br><hr><br>
                        
            <div class="row center">
 
                <div class="span4">

                    <h4>SAP Disaster Recovery</h4>
                    <a href="/User/disaster-recovery-service.aspx">
                    <div class="huge-round-icon"><i class="icon-cloud"></i></div>
                    </a>
                    <!--p>Lorem ipsum dolor sit amet, consectetur pers piciatis culpa praes entium cupid.</p-->
                    <a href="/User/disaster-recovery-service.aspx"  class="button simple arrow">Read More</a>
                                
                </div><!-- span3 end -->

                <div class="span4">
                                
                    <h4>SAP Infrastructure Services</h4>
                    <a href="/user/SAP-infra-services.aspx">
                    <div class="huge-round-icon"><i class="icon-cloud"></i></div>
                    </a>
                    <!--p>Lorem ipsum dolor sit amet, consectetur pers piciatis culpa praes entium cupid.</p--> 
                    <a href="/user/SAP-infra-services.aspx"  class="button simple arrow">Read More</a>

                </div><!-- span3 end -->    

                <div class="span4">

                    <h4>SAP Migration</h4>
                    <a href="/user/SAP-migration.aspx">
                    <div class="huge-round-icon"><i class="icon-cloud"></i></div>
                    </a>
                    <!--p>Lorem ipsum dolor sit amet, consectetur pers piciatis culpa praes entium cupid.</p-->
                    <a href="/user/SAP-migration.aspx" class="button simple arrow">Read More</a>
                                
                </div><!-- span3 end -->

                <!--div class="span3">
                                
                    <h4>Reseller hosting</h4>
                    <div class="huge-round-icon"><i class="icon-refresh"></i></div>
                    <p>Lorem ipsum dolor sit amet, consectetur pers piciatis culpa praes entium cupid.</p> 
                    <a href="reseller-hosting.html" class="button simple arrow">Reseller hosting plans</a>

                </div--><!-- span3 end -->  
                        
            </div><!-- row end -->   --%>

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
<asp:Content ID="Content3" ContentPlaceHolderID="footerSEOContent" runat="server"></asp:Content>

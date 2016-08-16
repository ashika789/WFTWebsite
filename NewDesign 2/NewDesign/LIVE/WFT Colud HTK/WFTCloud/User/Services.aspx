<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd.Master" AutoEventWireup="true" CodeBehind="Services.aspx.cs" Inherits="WFTCloud.User.Services" %>
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
                            <h4>Our Services</h4>
                            <h5>SAP Disaster Recovery Services:</h5>
                            <h6>SAP Push button Disaster Recovery solution</h6>
                            <p>SAP has standard methodologies for doing backups and restoring the SAP
                                environment, but there is nothing built into the SAP application that 
                                specifically addresses disaster recovery. Traditional SAP DR solutions 
                                leave many organizations unable to meet recovery time and recovery point
                                objectives. The recovery processes common in traditional DR solutions 
                                are prone to errors and result infrequent failures. WFT's expertise in 
                                understanding the complex and distributed nature of the various 
                                components in a landscape and the associated integration with bolt-on's 
                                makes it a leader in the design and implementation of SAP Push button 
                                Disaster Recovery solution.</p>
                            <h6>SAP Technical Business Impact Assessment (BIA)</h6>
                            <p>WFT's technical business impact assessment focuses on the critical 
                                technical interdependencies and impacts in the implementation of a SAP 
                                disaster recovery solution. Mapping of infrastructure functionality to 
                                infrastructure components. and identifying internal and external 
                                dependencies to achieve the required RTO/RPO is a major factor in the 
                                successful deployment of a SAP DR solution. This service also includes 
                                prioritizing critical applications and identifying associated 
                                infrastructure components in the disaster recovery strategy.</p>
                            <h6>SAP Disaster Recovery with Repurposing</h6>
                            <p>
                                WFT's SAP Disaster recovery with repurposing enables customer to utilize
                                the hardware at the disaster recovery site by running non-production 
                                landscapes at the disaster recovery site until a disaster occurs. This 
                                solution enables customers to rapidly re-purpose servers and their 
                                connectivity to appropriate networks and storage without repeatedly 
                                reworking or manually configuring the data center infrastructure. This 
                                solution also involves workflow automation to repurpose systems at the 
                                disaster recovery site to support production applications in case of a 
                                disaster.</p>
                            <h6>SAP Disaster Recovery on Cloud</h6>
                            <p>Utilizing cloud model for SAP disaster recovery has multiple benefits in terms of reduced capital expenditure and transferring to a utility based billing model. With the firm's extensive experience in deploying SAP in cloud environments and with a deep background in deploying SAP Disaster Recovery solutions, WFT's SAP Disaster Recovery on Cloud solution allows SAP customers the flexibility of deploying the Disaster Recovery Solution for their SAP Production landscape in a Public, Private or Hybrid Cloud environment.</p>
                                
                                
                                <hr><br />
                            <h5>SAP Data Protection services</h5>

                            <h6>SAP Backup integration services:</h6>
                            <p>Defining a backup strategy that meets different recovery requirements and designing recovery scenarios for different error conditions are essential for effective and prompt recovery of data within a landscape. Backup and Recovery Solution ensures that your application landscape is protected and recoverable while supporting multiple Local RPO/RTO targets.</p>
                            <p>Data protection of the SAP server involves steps to protect of all the software components that are needed by the SAP system to operate. The base components of the SAP server are the operating system, the SAP application server, database instance, and data files. Each component requires different data protection techniques. SAP non-database file systems contain various files necessary for SAP system operation like executables, transport files, diagnostic agent global scripts, profiles, and so on. The protection of the SAP database has two parts: protecting the database binaries and configuration files and protecting data stored in the data files. WFT's expertise and deep understanding of the different components in the SAP landscape that need to be backed up and the integration of these components with the right tools provides a end-to-end backup restore solution for the entire SAP landscape.</p>

                            <h6>SAP Backup with de-duplication:</h6>
                            <p>Some of the challenges faced by SAP customers specific to backup include growth of data, shrinking backup windows, management and maintenance issues and need to minimize impact on business operations. WFT's SAP Backup with De-duplication solution addresses these concerns by accelerating backup and restore tasks, eliminating performance impact on production systems and reducing the backup storage cost by utilizing de-duplication technology coupled with enterprise backup software. WFT presents the pros and cons of source side and target side de-duplication and integrates SAP application with the backup software and the de-duplication appliance to realize the benefits listed above. </p>
                             
                                 
                                <hr><br />
                            <h5>SAP Infrastructure Services</h5>

                            <h6>SAP Landscape Design and Optimization</h6>
                            <p>Implementing a new application environment or performing an upgrade is the perfect time to architect and plan out your entire SAP or Oracle Landscape. WFT landscape design solution takes into account all layers of the infrastructure, DB, Central Instance and Application/Batch Servers. Emphasis is placed on ease of management, scalability, sustainable performance, growth and recoverability. This solution follows a best practices approach to evaluate and design the necessary infrastructure to support the landscape requirements.</p>
                            <p>The Landscape Design for SAP Service help customers leverage WFT's vast customer experience and industry best practices to design a optimal landscape for new S AP implementations or improve existing landscapes. This will consider all aspects of sizing the landscape to provide an accurate SAP application landscape Sizing and complete landscape design. Deliverables include Advanced Sizing information with hardware specifications, Logical SAP landscape diagrams, physical landscape diagrams, Bill of materials and Project plan with detailed tasks, resource requirements and duration for each phases.</p>

                            <h6>SAP cross platform (OS/DB) migration</h6>
                            <p>This service will help customers to Migrate an SAP system to another hardware platform, another operating system, and another database. WFT will analyze and design an optimal plan specific to client's specific SAP landscape and help migrate to new platform with minimal downtime.</p>

                            <h6>SAP Storage Layout</h6>
                            <p>Landscapes can range from dozens to hundreds of servers. This translates to the potential of having a total number of filesystems that can reach into the thousands. WFT's SAP Data Layout service translates the unique performance and capacity characteristics of each individual filesystem (SAP Data, Indexes, etc…) and maps them directly to the SAN/NAS storage array.SAP specific metrics like SAPS for each component are taken into consideration in designing this layout so that each component resides on the appropriate storage tier.</p>

                            <h6>SAP Automated System Copies</h6>
                            <p>NetWeaver and Infrastructure administrators can spend countless number of weeks per year performing the necessary procedure of an SAP System Copy. WFT Automated System Copies allows for the creation of job streams that automate not only the infrastructure components, but also all of the direct SAP GUI Pre/Post Processing steps—saving weeks of manual effort. This automated process removes the need for human dependencies and potential human errors.</p>

                            <h6>SAP Landscape Replication</h6>
                            <p>SAP Landscape Replication for ABAP & J2EE is a solution that can help companies build new SAP landscapes by replicating existing NetWeaver landscapes. WFT's integration of infrastructure and NetWeaver tools allows this process to be accomplished in a fraction of the time with zero source system downtime thus saving time, valuable resources and money. </p>

                            <h6>SAP Performance Assessment</h6>
                            <p>Many landscapes experience a degradation in overall performance, especially as the landscape grows in capacity and concurrent users. Performance Optimization solution evaluates all the critical components of the SAP or Oracle infrastructure, from data layout to database SQL tuning and all layers in-between, to pinpoint the bottlenecks and determine whether an individual component reached it's performance limit. The uniqueness of this solution lies in the fact that the analysis is done at a specific point in time across all the layers in the I/O stack starting all the way from the application down to the disk spindle in the storage array. </p>

                            <h6>SAP Infrastructure Virtualization</h6>
                            <p>WFT will design, architect and build SAP landscape on Virtual Infrastructure provided by VMware. The services include SAP Landscape Design utilizing VMware HA, DRS and VMOTION, SAP technical Architecture design, Local Recovery and Disaster Recovery, System copy automation for SAP landscape on Virtual infrastructure.</p>

                            <h6>SAP Network Optimization with Cisco</h6>
                            <p>WFT's WAN optimization solution accelerates SAP applications over the WAN and also addresses poor application performance across the WAN in accessing centralized data centers. WFT's WAAS solution allows IT departments to centralize applications and storage in the data center while maintaining LAN-like application performance and provides locally hosted IT services while reducing the branch-office device footprint.</p>
                            
                              
                            </div><!-- span9 end -->

                        </div><!-- row end -->--%>

                    </div><!-- conteiner end -->

                </div><!-- white-section end -->

            </section><!-- content end -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerSEOContent" runat="server">
</asp:Content>

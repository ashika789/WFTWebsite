<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd.Master" AutoEventWireup="true" CodeBehind="TermsAndConditions.aspx.cs" Inherits="WFTCloud.User.TermsAndConditions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Including main template styles -->
        <link rel="stylesheet" href="/css/main.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="content">
    <div class="white-section">
    <div class="container row">
          
          <div class="span12">
                 <div class="breadcrumb">
                                    <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" />
                                    <asp:SiteMapPath ID="SiteMap1" runat="server"></asp:SiteMapPath>
                                </div>
              <asp:Literal ID="Literal1" runat="server"></asp:Literal>
           <%-- <div>
                                                <h2 id="H2">Terms And Conditions:</h2>
                                            </div>
                         <p>
                             <h4>WFT Incorporated WFTCloud Terms of Service</h4>

                      <p>WFTCloud  is wholly owned by Wharfedale Technologies  Incorporated providing <strong>SAP Cloud computing solutions & SAP ERP software solutions on Cloud</strong>. We are a certified provider of <strong>SAP in the Cloud</strong>. WFT's terms of service govern the use of WFTCloud.</p>
                      <h4>Content</h4>
                      <p>All services provided by WFT, Inc. are to be used for lawful purposes only. Transmission, storage, or presentation of any information, data or material in violation of any United States Federal, State or Local law is prohibited. This includes, but is not limited to: copyrighted material, material we judge to be threatening or obscene, material that jeopardizes national security, or material protected by trade secret or other laws. The subscriber agrees to indemnify and hold harmless WFT, Inc., from any claims resulting from the subscriber's use of WFTCloud  services which damages the subscriber or any other party.</p>
                      <p><b>Examples of prohibited content or links include (but are not limited to):</b></p>
                      <ul>
                      	<li>Pirated software</li>

                        <li>Hacking sites, programs or archives</li>
                        <li>Warez Sites</li>
                        <li>Pornographic and other similar material</li>
                        <li>Distribution of Movies, Music or any copyrighted material other than those explicitly agreed by both parties.</li>
                      </ul>
                      <p>WFT, Inc. will be the sole arbiter as to what constitutes a violation of this provision. Content that does not meet these standards will be removed without prior notice to the subscriber.</p>

                      <p><b>Housing of any of the following files is considered a violation of the terms of service:</b>
                      </p><ul>
                      	<li> <b> Commercial and Non Commercial</b> Software that could degrade the performance of SAP</li>
                        <li><b>IRC - </b>We currently do not allow IRC, Egg Drops, BNC, or IRC bots to be operated on our servers or network. Files with references to IRC or any likeness thereof are prohibited.</li>
                        <li><b>Proxies - </b>We do not allow proxy servers of any kind, whether for personal or business use. Files with references to any proxy or likeness thereof are prohibited.</li>

                        <li><b>PortScanning - </b>We do not allow any kind of portscanning to be done on or from our servers or network.</li>
                      </ul>
                      <p></p>
                      <h4>Commercial Advertising - Email</h4>
                      <ul style="padding-top:10px;">
                      	<li>Spamming, i.e. the sending of unsolicited email, from any WFT's  Web server or any server located on the WFTCloud  network is STRICTLY prohibited. WFT will be the sole arbiter as to what constitutes a violation of this provision. This also includes Opt-in Opt-out mail programs and mail that either directly or indirectly references a domain contained within an account at WFT.</li>
                        <li>Running Unconfirmed Mailing Lists. Subscribing email addresses to any mailing list without the express and verifiable permission of the email address owner is prohibited. All mailing lists run by WFT customers must be Closed-loop ("Confirmed Opt-in"). The subscription confirmation message received from each address owner must be kept on file for the duration of the existence of the mailing list. Purchasing or selling lists of email addresses from 3rd parties for mailing to or from any WFT .-hosted domain, or referencing any WFT account, is prohibited.</li>

                        <li>Advertising, transmitting, or otherwise making available any software, program, product, or service that is designed to violate this AUP or the AUP of any other Internet Service Provider, which includes, but is not limited to, the facilitation of the means to send Unsolicited Bulk Email, initiation of pinging, flooding, mail-bombing, denial of service attacks is prohibited.</li>
                        <li> Email address cultivating, or any unauthorized collecting of email addresses without prior notification of the email address owner is strictly prohibited.</li>
                        <li>Operating an account on behalf of, or in connection with, or reselling any service to, persons or firms listed in the Spamhaus Register of Known Spam Operations (ROKSO) database at www.spamhaus.org is prohibited.</li>
                        <li>All commercial email marketing companies must adhere to the Controlling the Assault of Non-Solicited Pornography And Marketing (CAN-SPAM) Act of 2003. In addition such companies are prohibited the sending of bulk mail from "disposable domain names" with whois privacy protection.</li>
                      </ul>
                      <h4>Server Abuse</h4>

                      <p>Any attempts to undermine or cause harm to a WFT server or subscriber of WFT  is strictly prohibited including, but not limited to:
                      </p><ul>
                      	<li>  Logging into a server or account that you are not authorized to access.</li>
                        <li>Accessing data or taking any action to obtain services not intended for you or your use.</li>
                        <li>Attempting to probe, scan or test the vulnerability of any system, subsystem or network.</li>
                        <li>Tampering, hacking, modifying, or otherwise corrupting or breaching security or authentication measures without proper authorization.</li>
                        <li>Transmitting material that contains viruses, Trojan horses, worms, time bombs, cancelbots or other computer programming routines or engines with the intent or effect of damaging, destroying, disrupting or otherwise impairing a computer's functionality or the operation of the System.</li>

                        <li>Interfering with, intercepting or expropriating any system, data or information.</li>
                        <li>Interfering with service to any user, host or network including, without limitation, by means of overloading, "flooding," "mailbombing," or "crashing" any computer system.</li>
                      </ul>
                      <p></p>
                      <p>You will be held responsible for all actions performed by your account whether it be done by you or by others.</p>
                      <p>All sub-networks of WFT  Inc. and all servers must adhere to the above policies.</p>
                      
                      <h4>Server Support</h4>

                      <p>Basic support and maintenance of dedicated servers is provided at the discretion of WFT, Inc. In the case of erroneous instances or support issues extending beyond what we determine to be within the realm of reasonable assistance, support is priced as follows:
                      </p><ul>
                      <li> $49/hour</li></ul><p></p>
                      <h4>Scheduled Maintenance and Downtime</h4>
                      <p>WFT  will use its commercially reasonably efforts to provide services 24 hours a day, seven days a week. Subscriber acknowledges that from time to time the services may be inaccessible or inoperable for various reasons, including periodic maintenance procedures or upgrades ("Scheduled Downtime"); or service malfunctions, and causes beyond  WFT's control or which are not reasonably foreseeable by WFT  including the interruption or failure of telecommunications or digital transmission links, hostile network attacks, or network congestion or other failures. WFT  will provide at least 48 hours advance notice to the subscriber for Scheduled Downtimes, and will use commercially reasonable efforts to minimize any other disruption, inaccessibility and/or inoperability of its web servers. WFT  has no responsibility for downtime resulting from a user's actions. </p>
                      <h4>Fees</h4>
                      <p>WFT Cloud will charge you the fees stated in your Order. When launching a WFT Cloud Server, your credit card will be charged the full monthly subscription, for the price of the SAP Landscape Server that was selected.  WFT Cloud Servers are charged in monthly billing increments.  WFT Cloud uses a fixed-cost pricing model – in other words, your invoiced amount will be charged in its entirety at the beginning of your monthly billing cycle. If you choose a month-to-month contract, you will be charged based on your initial invoice and no discounts will be applicable to such accounts. Discounts are offered for multi-month subscription for dedicated servers.  If you choose this option, you are bound to stay for the entire contracted period to avail the discounted price. Should you choose to cancel your service prior to the end of your discounted term, the remaining balance of the total amount will be billed and service will continue to be active for the remainder of the contracted period.  WFT Cloud does not offer pro-rated billing services or pay-as-you-go option currently for the Express Cloud services.

Unless you have made other arrangements, WFT will charge your credit card with invoice amount as follows: (i) for recurring fees, in advance, on or around the first day of each billing cycle, and (ii) for non-recurring fees (such as fees for initial set-up, overages, compute cycle fees, and domain name registration) on or around the date incurred, or on or around the first day of the billing cycle that follows the date incurred. Unless otherwise agreed in the Order, your billing cycle will be pre-paid monthly, beginning on the date that WFT Cloud first makes the services available to you.
In some cases WFT Cloud will present an "Estimated Monthly Charge". The "Estimated Monthly Charge" is provided as a courtesy to you to help budget your monthly expenses. However, the Estimated Monthly Charge is a rough estimation of your charges based on your current usage and will not be your guaranteed monthly charge. Many items can change your monthly charge, including, but not limited to, backups, firewalls, bandwidth usage, support, domain names, private networking and any other optional WFTCloud service which you choose to enable.
WFT Cloud may suspend all services (including services provided pursuant to any unrelated Order or other agreement we may have with you) if our charges to your credit card are rejected for any reason. WFT may charge interest on overdue amounts at 1.5% per month (or the maximum legal rate if it is less than 1.5%). If any amount is overdue by more than thirty (30) days, and WFT brings a legal action to collect, or engages a collection agency, you must also pay WFT's reasonable costs of collection, including attorney fees and court costs. All fees are stated and will be charged in US Dollars. Any "credit" that we may owe you, such as a credit for failure to meet a service level guaranty, will be applied to fees due from you for services, and will not be paid to you as a refund. Charges that are not disputed within sixty (60) days of the date charged are conclusively deemed accurate. You must provide WFT with accurate factual information to help WFT determine if any tax is due with respect to the provision of the Services, and if WFT is required by law to collect taxes on the provision of the Services, you must pay WFT the amount of the tax that is due or provide satisfactory evidence of your exemption from the tax. You authorize WFT to obtain a credit report at any time during the term of the Agreement. Any credit that we may owe you, such as a credit for a SLA remedy, will be applied to unpaid fees for services or refunded to you, at our discretion.</p>
                      <h4>Cancellation &amp; Refunds</h4>
                      <p>Your WFTCloud account will remain active unless you choose to terminate your subscription.  To terminate your WFTCloud account, you will have to use the WFT Cloud portal to cancel your subscription(s). Email requests for cancellation will not be honoured nor acknowledged. 
Any violation of WFT's Terms of Service Agreement shall result in no refund.</p>
                      <h4>Account Setup, Termination, and Billing</h4>
                      <p>The account will be charged on the day the order is processed, regardless of the account billing date. The account will not be charged again until the next month's billing cycle. </p>

                      <h4>Promotional Use</h4>
                      <p>WFT or WFTCloud may refer to You, Your company, or your logo's for promotional purposes. Your company name, logos and the services that were provided to  WFT may be used in promotional materials, advertising, marketing releases, newsletter, public disclosures and on the WFT website. This reference will be strictly limited to disclosure that WFT has provided services to the company and will not contain any confidential, sensitive or proprietary information in such a reference. The reference will also not provide any personally identifiable information about the individual or technical information regarding the landscape and design used by the customer at WFTCloud. However, WFT may disclose any information requested by law enforcement or when compelled by court order, applicable laws or regulations.</p>
                      <p>Any work or professional services performed or provided by WFT under this Agreement shall not be deemed .Work For Hire,. but WFT shall grant a non-exclusive, non-transferable license to You, for the duration of this Agreement, its employees, affiliates, and third parties commissioned by WFT.</p>

                      <h4>Microsoft Software License Policy.</h4>
                      <p>Microsoft, as well as WFT company policy, does not allow mixing of Microsoft license ownership. For an example, a customer cannot use a WFT purchased Windows Server license in conjunction with their own customer license of MSSQL or any other Microsoft Product. This is a violation of Microsoft's licensing policies. The customer would have to obtain all Microsoft licenses through WFT and this cost is factored in the Windows Server Purchase. In some instances, we can allow the customer to supply all Microsoft licensed products, including the OS. However, all licenses must be provided by the customer and may not be mixed with WFT licensed Microsoft products. Any questions regarding this policy can be addressed to <a href="mailto:support@wftcloud.com">support@wftcloud.com</a></p>
                      <h4>Limitation of Damages</h4>
                      <p>Recovery of damages from WFT may not exceed the amount of fees it has collected on the account.</p>
                      <h4>General</h4>
                      <ul style="padding-top:10px;">

                      <li>Terms Of Service are subject to change without any prior notification.</li>
                      <li>These Terms of Service are a legally binding contract between the subscriber and WFT, Inc.</li>
                      <li> By opening an account, the subscriber agrees to the above-stated terms.</li>
                      <li> Anything not listed in the Terms of Service is open to interpretation and change by WFT, Inc. administrators without prior notice.</li>
                      <li> All prices, with the exception of the 30-day money back guarantee, are nonrefundable and nonnegotiable.</li>

                      <li> The 30-day money back guarantee does not pertain to Virtual Dedicated and Full Dedicated server plans.</li>
                      <li>          
 Any violation of these Terms of Service will result in termination of the account. WFT, Inc. maintains the right to terminate accounts without prior notification.</li>
 <li>We reserve the right to remove any account with 15 days prior notice.</li>
 </ul>
            <p>&nbsp;</p>
 <h4>Dispute Resolution</h4>
 <p>Any dispute between WFT and a subscriber shall be determined by arbitration conducted by the American Arbitration Association pursuant to its commercial arbitration rules. The arbitrator shall decide any dispute in accordance with State of New Jersey  law, without the application of choice of law principles. Each party shall bear its own expenses and legal fees for the arbitration. The arbitration shall be conducted in Monmouth County , NJ, unless both parties agree in writing to a different location. The arbitration award is enforceable as a judgment of any court having proper jurisdiction.</p>
                               --%>
          </div>
          
          <!-- span12 end -->
          
          </div>
         </div>
                </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerSEOContent" runat="server">
      <hr />
      <div class="row">
        <div class="span12">
              <h2 style="font-size:12px;">WFTCloud offers SAP cloud computing solutions and ERP solutions on cloud!</h2>
              	<p style="font-size:12px;">WFTCloud.com offers its services in <strong>SAP cloud computing solutions &ERP solution on cloud</strong> at an unmatched cost. Utilize WFT's expertise in <strong>SAP cloud computing solution & ERP solution on cloud</strong> for your business. Call Now!!!<p>
                <h2 style="font-size:12px;">Pay per use model for ERP software on cloud and ERP system on cloud.</h2>
                <p style="font-size:12px;">We focus on reducing your ERP implementation cost by introducing pay per use model for <strong>ERP software on cloud, ERP system on cloud & SAP on cloud support services</strong>. To know more about our pricing packages for <strong>ERP software on cloud, ERP system on cloud & SAP on cloud support services</strong>, Contact Us Now! <p>
                <h2 style="font-size:12px;">SAP certified provider of affordable & low cost SAP in the cloud solution.</h2>
                <p style="font-size:12px;">WFTCloud is a certified provider of <strong>cloud SAP in the cloud solution, ERP solution on cloud & SAP cloud computing solution</strong>. Get implementation of cloud <strong>SAP in the cloud solution, ERP solution on cloud & SAP cloud computing solution</strong> at a fraction of conventional cost.<p>

           <%--           	<p style="font-size:12px;"><strong>WFTCloud offers legal SAP online access for cheap SAP systems & solutions!</strong><br>
WFTCloud.com offers legal SAP online access for small and medium scale enterprises and relatively cheap SAP systems & solutions on cloud SAP software at an unmatched cost. Utilize WFT's expertise in legal SAP online access & relatively cheap SAP systems & solutions for your business. Call Now!!!<p>
                <p style="font-size:12px;"><strong>Pay per use model for affordable & low cost SAP system for small business.</strong><br>
We focus on reducing your ERP implementation cost by introducing pay per use model for cheap, affordable & low cost SAP system for small business. To know more about our pricing packages for Cloud SAP ERP, SAP on Cloud software services and cheap, affordable & low cost SAP system for small business, Contact Us Now! <p>
                <p style="font-size:12px;"><strong>SAP certified provider of low cost & affordable SAP system for small business to large enterprises.</strong><br>
WFTCloud is a certified provider of low cost & affordable SAP system for small business to large enterprises. Get implementation of low cost & affordable SAP system for small business to large enterprises within a fraction of conventional cost.<p>--%>

        </div>
      </div>
</asp:Content>

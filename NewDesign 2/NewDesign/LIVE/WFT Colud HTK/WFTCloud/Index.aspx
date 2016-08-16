<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WFTCloud.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Including main template styles -->
    <link rel="stylesheet" href="/css/main-4.css">
    
    
	<link href="js/ticker-style.css" rel="stylesheet" type="text/css" />
	
	<script src="js/jquery.ticker.js" type="text/javascript"></script>
	<script src="js/site.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <section id="content">
<div class="white-section">
  <div class="container">
    <div class="row">
        <div class="span12">
            <ul id="newsticker_1" class="newsticker">
                <asp:Repeater ID="rptrTwitterFeeds" runat="server">
            <ItemTemplate>
                <li><%# Eval("tweetHTMLText") %></li>
            </ItemTemplate>
            </asp:Repeater>
            </ul>
            <!--h6 class="span12 homepage-style" style="font-weight:normal;">WFT Cloud pioneers in bringing SAP on Cloud for both Enterprises and Individual users. Cost effective, Highly Scalable and Reliable SAP servers on cloud are our earner endeavor.</h6-->       
        </div>
    </div>
       
      <div class="row">
      <div class="span12">
        <h4 style="margin-top:10px;">Trending Now</h4>
      </div>
     <ul id="js-news" class="js-hidden">
		<li class="news-item"><img src="img/India.png" /> India, Karnataka &nbsp&nbsp&nbsp<img src="img/SAP.png" />IDES ERP6 EHP6 Dedicated client</li>
		<li class="news-item"><img src="img/America.png" /> USA, Los Angeles &nbsp&nbsp&nbsp<img src="img/SAP.png" />IDES ERP6 EHP7 Dedicated Server</li>
		<li class="news-item"><img src="img/America.png" /> USA, Los Angeles &nbsp&nbsp&nbsp<img src="img/SAP.png" />HANA Developer One Edition</li>
		<li class="news-item"><img src="img/Southafrica.png" /> South Africa, Johannesburg &nbsp&nbsp&nbsp<img src="img/SAP.png" />ERP - BW Combo System</li>
		<li class="news-item"><img src="img/Australia.png" /> Australia, Sydney &nbsp&nbsp&nbsp<img src="img/SAP.png" />IDES ERP6 EHP6 Dedicated client</li>
		<li class="news-item"><img src="img/India.png" /> India, TamilNadu &nbsp&nbsp&nbsp<img src="img/SAP.png" />IDES EHP6 IS-Utilities</li>
		<li class="news-item"><img src="img/America.png" /> USA, Texas &nbsp&nbsp&nbsp<img src="img/SAP.png" />ERP6 EHP6</li>
	</ul>
    
      <!-- span3 end -->
     
    </div>
    <div class="row">
      <div class="span12">
        <h4 style="margin-top:10px;">Enterprise Offerings</h4>
      </div>
      <div class="span3 staff"> <a href="/user/private-cloud-on-premise.aspx"><img src="/img/private-cloud.png" alt="private cloud"></a>
        <h5><a href="/user/private-cloud-on-premise.aspx">Private Cloud</a></h5>
        <p>Building SAP on-premise Enterprise Private Cloud incl. HW with Utility Model &amp; Support </p>
      </div>
      <!-- span3 end -->
      
      <div class="span3 staff"> <a href="/user/SAP-HANA-Services.aspx"><img src="/img/sap-hana.png" alt="sap hana"></a>
        <h5><a href="/user/SAP-HANA-Services.aspx">SAP HANA Services</a></h5>
        <p>Assessment Migration /<br>
          Support Proof of<br>
          Concept</p>
      </div>
      <!-- span3 end -->
      
      <div class="span3 staff"> <a href="/user/SAP-infra-services.aspx"><img src="/img/sap-basis.png" alt="sap basis"></a>
        <h5><a href="/user/SAP-infra-services.aspx">SAP Basis Services</a></h5>
        <p>Remote Basis Support<br>
          @ $2.29/hour/per server<br>
          24x7x365 Support </p>
      </div>
      <!-- span3 end -->
      
      <div class="span3 staff"> <a href="/user/SAP-disaster-recovery.aspx"><img src="/img/disaster.png" alt="disaster"></a>
        <h5><a href="/user/SAP-disaster-recovery.aspx">Disaster Recovery</a></h5>
        <p>Recovery<br>
          As A Service for <br>
          SAP (RaaS)</p>
      </div>
      <!-- span3 end --> 
      
    </div>
    <!-- row end --> 
  </div>
  <!-- container end --> 
  
</div>
<!-- white-section end -->

<div class="blue-section">
  <div class="container"> 
    
    <!-- row end --> 
    
    <div class="row">
      <div class="span12">
        <h4>SAP Express Service</h4>
        <p>SAP servers are no more a cost intensive capital investment. WFT makes SAP servers affordable for even individual users and small enterprises. The pay as you use model lets you optimize your cost to levels where you could run any of your training, test and development experiments.</p>
        <div class="tabs">
            <ul class="tab-nav">
                <asp:Repeater ID="rptrServiceCategoryTab" runat="server">
                <ItemTemplate>
                    <li class='<%# GetActiveClassForCategoryTab(Eval("ServiceCategoryID").ToString()) %>'><a href="#tab-<%# Eval("ServiceCategoryID")%>" data-toggle="tab"><%# Eval("CategoryName")%></a></li>
                </ItemTemplate>
                </asp:Repeater>
            </ul>

            <div class="tab-content" style="height:250px;">
                <asp:Repeater ID="rptrServiceCategoryTabContent" runat="server" 
                    onitemdatabound="rptrServiceCategoryTabContent_ItemDataBound">
                <ItemTemplate>
                <div class="tab-cont <%# GetActiveClassForCategoryTab(Eval("ServiceCategoryID").ToString()) %>" id="tab-<%# Eval("ServiceCategoryID")%>">  
                    <asp:HiddenField ID="hdnCategoryID" runat="server" Value='<%# Eval("ServiceCategoryID")%>' />                                     
                    <table class="table alert alert-warning" style="padding:0px !important; margin:0px !important;">
                      <tr class="th-head">
                        <th width="30%" align="left" >SAP Module</th>
                        <th width="25%">Release</th>
                        <th width="20%" align="left">Type of System</th>
                        <th width="15%" align="left">Cost*</th>
                        <th width="10%">Buy Online</th>
                      </tr>
                      <tbody>
                        <asp:Repeater ID="rptrCategoryServices" runat="server">
                        <ItemTemplate>
                            <tr class="th-body">
                              <td class='<%# GetBlinkClass(Eval("ServiceID").ToString())%>' ><a href="#" class="wftoffer" rel="popover" data-content='<%# Eval("ServiceDescription")%> * $<%#Eval("InitialHoldAmount")%> All Cloud Services Provided by WFT Cloud are Charged on a Prepaid Basis. Please Read the Terms & Conditions for Details.' data-original-title="<%# Eval("ServiceName")%>" style="<%# GetBlinkClass(Eval("ServiceID").ToString()) == "blink_me"?"font-weight:bold;":"" %>"><%# Eval("ServiceName")%></a></td>
                              <td><%# Eval("ReleaseVersion")%></td>
                              <td> <%# Eval("SystemType")%> </td>
                              <td>$<span><%# Eval("WFTCloudPrice")%>/<%# Eval("UsageUnit")%></span></td>
                              <td><a href='/Index.aspx?AddToCart=<%# Eval("ServiceID")%>' class="tab-buy"><img src="/img/buy.png"></a>
                              </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr>
                              <td class='<%# GetBlinkClass(Eval("ServiceID").ToString())%>' ><a href="#" class="wftoffer" rel="popover" data-content='<%# Eval("ServiceDescription")%> * $<%#Eval("InitialHoldAmount")%> All Cloud Services Provided by WFT Cloud are Charged on a Prepaid Basis. Please Read the Terms & Conditions for Details.' data-original-title="<%# Eval("ServiceName")%>" style="<%# GetBlinkClass(Eval("ServiceID").ToString()) == "blink_me"?"font-weight:bold;":"" %>"><%# Eval("ServiceName")%></a></td>
                              <td><%# Eval("ReleaseVersion")%></td>
                              <td> <%# Eval("SystemType")%> </td>
                              <td>$<span><%# Eval("WFTCloudPrice")%>/<%# Eval("UsageUnit")%></span></td>
                              <td><a href='/Index.aspx?AddToCart=<%# Eval("ServiceID")%>' class="tab-buy"><img src="/img/buy.png"></a>
                              </td>
                            </tr>
                        </AlternatingItemTemplate>
                        </asp:Repeater>
                      </tbody>
                      </table> 
                
                </div>                    
                </ItemTemplate>
                </asp:Repeater>
            </div>
            <br />
              <table width="100%" style="margin-top:-10px; margin-bottom:-10px;">
                        <tbody>
                        <tr>
                        <td><p style="font-size:9px;">*SAP Basis standard support included | *Strictly NON-Production only | *Customers responsible for SAP License | *Test/Demo Systems are for WFT Customers Only<br>
                          *All Express Cloud services are charged on a monthly flat-rate basis</p></td>
                        </tr>
                        </tbody>
              </table> 
        </div> <!-- tabbable end --> 
        
      </div>
      <!-- span12 end --> 
      
    </div>
    <!-- row end --> 
    
    <!--div class="row">
          <p class="span12">These are jsut some of the comments from our clients. To see more more testimonials go to our <a href="#">Testimonial Page</a></p>
        </div--> 
    <!-- row end --> 
    
  </div>
  <!-- container end --> 
  
</div>
<!-- blue-section end -->


<div class="white-section">
  <div class="container">
    <div class="row" style="margin-top:0px;">
      <div class="span12">
        <div class="well clearfix" style="margin-top:10px;">
          <p>Didn't find the specs you were looking for? <strong>No problem!&nbsp;</strong></p>
          <p class="visible-desktop"> Get your <strong>custom plan</strong> now!</p>
          <a href="/user/contactus.aspx" class="button simple arrow pull-right">Get custom quote</a> </div>
        <!-- message-box end --> 
        
      </div>
      <!-- span12 end --> 
      
    </div>
    <!-- row end --> 
    
    <!-- row start -->
    <div class="alert alert-info" style="clear:both; float:left; margin-left:10px;">
      <h4>Media</h4>
      <br>
        <div class="pull-left span6">
            <div style="display:none;margin:0 auto; float:left; margin-right:12px;" class="html5gallery hidden-desktop" data-autoplayvideo="false" data-skin="horizontal" data-width="275" data-height="280"> 
                <!-- Add Youtube video to Gallery --> 
                <asp:Repeater ID="rptrDesktopVideos" runat="server">
                <ItemTemplate>
                    <a href="<%# Eval("Path")%>"><img src="<%# Eval("ThumbnailPath")%>" alt=""></a> 
                </ItemTemplate>
                </asp:Repeater>
              </div>
              <div style="display:none;margin:0 auto; float:left; margin-right:12px; " class="html5gallery hidden-phone" data-autoplayvideo="false" data-skin="horizontal" data-width="430" data-height="280" id="vd1"> 
                <!-- Add Youtube video to Gallery --> 
                <asp:Repeater ID="rptrPhoneVideos" runat="server">
                <ItemTemplate>
                    <a href="<%# Eval("Path")%>"><img src="<%# Eval("ThumbnailPath")%>" alt=""></a> 
                </ItemTemplate>
                </asp:Repeater>
              </div>
          </div>
      <!--<img src="img/media1.jpg" width="442" height="325">-->
      <%--<div><img src="/img/media2.jpg" style="margin-left:3px;"></div>--%>
        <div class="pull-right span5">
            <div id="wft-hlght">
              <h2>WFT Cloud Highlights</h2
>
              <ul class="wft-hlghts">
                <li>SAP Certified Hosting and Cloud Services Provider</li>
                <li>Reliable cost effective infrastructure tailored for enterprise needs</li>
                <li>Flexible open system architecture to support any business needs</li>
                <li>Highly Scalable to varying resource requirements</li>
                <li>Convenient monthly utility billing</li>
                <li>Responsive and effective customer support</li>
                <li>Access to industry best practices and whitepapers</li>
                <li>SAP basic BASIS Support is included with all our Express Cloud Service</li>
              </ul>
            </div>
          </div>
    </div>
      <div style="clear:both;"></div>
    <!-- container end --> 
    <!-- row end -->
    
    <div class="alert alert-info">
      <h4> Brochures / White Paper</h4>
      <br>
      <div class="container">
        <div class="span12">
          <div >
            <div id="myCarousel" class="carousel slide"> 
              
              <!-- Carousel items -->
              <div class="carousel-inner" style="height:185px !important; ">
                <asp:Repeater ID="rptrBrochureCarousel" runat="server" 
                      onitemdatabound="rptrBrochureCarousel_ItemDataBound">
                <ItemTemplate>
                <div class="item <%# GetActiveClassForPageNumber(Eval("PageNo").ToString()) %>">
                  <asp:HiddenField ID="hdnPageNumber" runat="server" Value='<%# Eval("PageNo")%>' />   
                  <div class="row-fluid" >
                    	<asp:Repeater ID="rptrBrochureInnerGroupItems" runat="server">
                        <ItemTemplate>
                        <div class="span2">
                            <a href="<%# Eval("Path")%>" target="_blank"><div><img src='<%# Eval("ThumbnailPath")%>'></div><%# Eval("Title")%></a>
                        </div>
                        </ItemTemplate>
                        </asp:Repeater>
                  </div>
                  <!--/row-fluid--> 
                </div>
                
                </ItemTemplate>
                </asp:Repeater>

              </div>
              <!--/carousel-inner--> 
              
              <%--<a class="left carousel-control" href="#myCarousel" data-slide="prev" style="left:-60px !important;" id="al1"><img src="/img/bt-prev-fo.png" alt="image" style="margin-top:-2px;"></a> <a class="right carousel-control" href="#myCarousel" data-slide="next" style="right:15px !important;"><img src="img/bt-next-fo.png" alt="image" style="margin-top:-2px;"></a> </div>--%>
                <a class="left carousel-control hidden-phone" href="#myCarousel" data-slide="prev" style="left:-47px !important;" id="al1"><img src="img/bt-prev-fo.png" alt="image" style="margin-top:-2px;"></a> <a class="right carousel-control hidden-phone" href="#myCarousel" id="ar1" data-slide="next" style="right:15px !important;"><img src="img/bt-next-fo.png" alt="image" style="margin-top:-2px;"></a> <a class="left carousel-control hidden-desktop" href="#myCarousel" data-slide="prev" style="left:-40px !important;"><img src="img/bt-prev-fo.png" alt="image" style="margin-top:-2px;"></a> <a class="right carousel-control hidden-desktop" href="#myCarousel" data-slide="next" style="right:25px !important;"><img src="img/bt-next-fo.png" alt="image" style="margin-top:-2px;"></a> </div>
            <!--/myCarousel--> 
            
          </div>
          <!--/well--> 
        </div>
      </div>
    </div>
  </div>
 </div>
  <div class="blue-section">
    <div class="container">
      <div class="row">
        <h1 class="span12 homepage-style">What our clients say...</h1>
      </div>
      <!-- row end -->
      
      <div class="row">
        <p class="span12" align="center">A testimony is worth a thousand medals and here goes a few of what we cherish from our delighted customers.</p>
      </div>
      <!-- row end -->
      
      <div class="row">
                    <ul class="testimonials">
                        <li class="span6">
                            <asp:HyperLink runat="server" ID="hyptest1" Target="_blank">
                                <div class="testimonial">

                                    <%--<a href="/User/Testimonials.aspx"  target="_blank">--%>
                                    <img class="testimonial-image" alt="Person image" src="/img/testimonial-picture-com.jpg">
                                    <table>
                                        <tr>
                                            <td style="height: 65px;">
                                                <div style="width: 100%; height: 100%; overflow: hidden;">

                                                    <asp:Label ID="ltTestimonial1" runat="server" Style="cursor: pointer; text-align: justify;"> </asp:Label>
                                                </div>

                                            </td>

                                        </tr>

                                    </table>
                                    <br />

                                    <%-- <div style="float: right;">
                                        <asp:HyperLink ID="hlTestimonial1" runat="server" Style="cursor: pointer; word-wrap: break-word;" Font-Size="Small" Target="_blank"></asp:HyperLink>
                                    </div>

                                    <div style="clear: right; float: right;">
                                        <asp:HyperLink ID="hlTestimonialOrg1" runat="server" Style="cursor: pointer;" Font-Size="Small" Target="_blank"></asp:HyperLink>
                                    </div>--%>
                                    <table >
                                        <tr>
                                            <td style="height: 20px;">
                                                <div style="width: 100%; height: 100%; overflow: hidden;">
                                                    <asp:HyperLink ID="hlTestimonial1" runat="server" Style="word-wrap: break-word;padding-right:5px;" Font-Size="Small" Target="_blank" Font-Bold="true" ></asp:HyperLink>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 20px;">
                                                <div style="width: 100%; height: 100%; overflow: hidden;">
                                                    <asp:HyperLink ID="hlTestimonialOrg1" runat="server" Style="clear: right; word-wrap: break-word;padding-right:5px;" Font-Size="Small"   Target="_blank"></asp:HyperLink>

                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                </div>
                            </asp:HyperLink>
                            <!-- testimonial end -->
                        </li>
                        <li class="span6">
                            <asp:HyperLink runat="server" ID="hyptest2" Target="_blank">
                                <div class="testimonial">

                                    <%--<a href="/User/Testimonials.aspx"  target="_blank">--%>
                                    <img class="testimonial-image" alt="Person image" src="/img/testimonial-picture-com-2.jpg">

                                    <table>
                                        <tr>
                                            <td style="height: 65px;">
                                                <div style="width: 100%; height: 100%; overflow: hidden;">
                                                    <asp:Label ID="ltTestimonial2" runat="server" Style="cursor: pointer; text-align: justify;"> </asp:Label>

                                                </div>

                                            </td>

                                        </tr>

                                    </table>
                                    <%--<asp:HyperLink ID="ltTestimonial2" runat="server" Style="text-align: justify;" > </asp:HyperLink>--%>
                                    <br />

                                    <table>
                                        <tr>
                                            <td style="height: 20px;">
                                                <div style="width: 100%; height: 100%; overflow: hidden;">
                                                    <asp:HyperLink ID="hlTestimonial2" runat="server" Style="word-wrap: break-word;padding-right:5px;" Font-Size="Small" Font-Bold="true" Target="_blank"></asp:HyperLink>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 20px;">
                                                <div style="width: 100%; height: 100%; overflow: hidden;">
                                                    <asp:HyperLink ID="hlTestimonialOrg2" runat="server" Style="word-wrap: break-word;padding-right:5px;" Font-Size="Small" Target="_blank"></asp:HyperLink>
                                                </div>
                                            </td>
                                        </tr>


                                    </table>
                                    <br />

                                </div>
                            </asp:HyperLink>
                            <!-- testimonial end -->
                        </li>
                    </ul>
        <!--p class="span12" align="center">These are jsut some of the comments from our clients. To see more more testimonials go to our <a href="#">Testimonial Page</a></p--> 
      </div>
      <!-- row end --> 
      
    </div>
    <!-- container end --> 
    
  </div>

  </section>
    <!-- content end -->
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="footerSEOContent" runat="server">
    <hr />
    <div class="row">
        <div class="span12">
            <p style="font-size: 12px;">
                WFTCloud offers SAP, ERP Cloud computing solutions & systems! WFTCloud.com offers SAP on the cloud computing solutions, services & systems including Cloud ERP & CRM on-demand solutions at an unmatched cost. Utilize WFT's expertise for SAP cloud computing solutions including Cloud ERP & CRM on-demand solutions for your business. Call Now!!!
                <p>
                    <h2 style="font-size: 12px;">Pay per Use model for Cloud SAP ERP systems & ERP on the Cloud solutions.</h2>
                    <p style="font-size: 12px;">
                        We drastically reduced your SAP implementation cost by introducing a pay per use model for online SAP access, cloud SAP ERP system, on demand SAP & ERP on the cloud solutions. To know more about our pricing packages for cloud SAP ERP solutions, on demand ERP, web based ERP systems & SAP ERP on the cloud services Contact Us Now!
                        <p>
                            <h2 style="font-size: 12px;">SAP Certified provider of SAP, ERP cloud services.</h2>
                            <p style="font-size: 12px;">
                                WFTCloud is a certified provider of SAP cloud computing solutions, cloud SAP ERP systems, ERP on the cloud, on demand ERP, web based ERP systems & SAP cloud services. Get implementation of cloud SAP ERP system, ERP on the cloud, on demand ERP, web based ERP system & SAP cloud services at a fraction of conventional cost.
                                <p>

                                    <%--<p style="font-size:12px;">WFTCloud offers SAP, ERP Cloud computing solutions & systems! WFTCloud.com offers SAP on the cloud computing solutions, services & systems including Cloud ERP & CRM on-demand solutions at an unmatched cost. Utilize WFT's expertise for SAP cloud computing solutions including Cloud ERP & CRM on-demand solutions for your business. Call Now!!! <p>
    <p style="font-size:12px;"><strong>Pay per Use model for Cloud SAP ERP systems & ERP on the Cloud solutions.</strong><br>
    We drastically reduced your SAP implementation cost by introducing a pay per use model for online SAP access, cloud SAP ERP system, on demand SAP & ERP on the cloud solutions. To know more about our pricing packages for cloud SAP ERP solutions, on demand ERP, web based ERP systems & SAP ERP on the cloud services Contact Us Now! <p>
    <p style="font-size:12px;"><strong>SAP Certified provider of SAP, ERP cloud services.</strong><br>
    WFTCloud is a certified provider of SAP cloud computing solutions, cloud SAP ERP systems, ERP on the cloud, on demand ERP, web based ERP systems & SAP cloud services. Get implementation of cloud SAP ERP system, ERP on the cloud, on demand ERP, web based ERP system & SAP cloud services at a fraction of conventional cost. <p>--%>
        </div>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd.Master" AutoEventWireup="true" CodeBehind="express-cloud.aspx.cs" Inherits="WFTCloud.User.express_cloud" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <!-- Including main template styles -->
        <link rel="stylesheet" href="/css/main.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

            <section id="content">
               
                <div class="white-section">
                    
                    <div class="container">

                        <div class="row">
                            
                            <div class="span12">
                              <div class="breadcrumb">
                                    <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" />
                                    <asp:SiteMapPath ID="SiteMap1" runat="server"></asp:SiteMapPath>
                                </div>
                                <h4>Express Cloud - Test/Demo/Dev Access</h4>
        <asp:Repeater runat="server" ID="rptrCategories" OnItemDataBound="rptrCategories_ItemDataBound">
             <HeaderTemplate>
                <table width="100%" class="alert alert-info">
                  <tr class="th-head">
                    <th width="30%" align="left" >SAP Module</th>
                    <th width="25%" align="left">Release</th>
                    <th width="20%" align="left">Type of System</th>
                    <th width="15%" align="left">Cost*</th>
                    <th width="10%">Buy Online </th>
                  </tr>
             </HeaderTemplate>
             <ItemTemplate>
                 <tr>
                     <td colspan="5" style=" vertical-align:middle;"><h6><%# Eval("CategoryName")%></h6></td>
                  </tr>
                 <tr>
                <td colspan="5">
                  <table class="table alert alert-info">
                 <tbody>
                 <asp:HiddenField ID="hdnServiceCategoryID" runat="server" Value='<%# Eval("ServiceCategoryID")%>' />
                  <asp:Repeater ID="rptrServices" runat="server">
                      <ItemTemplate>
                        <tr class="th-body">
                              <td  width="30%"><a href="#" class="wftoffer1" rel="popover" data-content="<%# Eval("ServiceDescription")%>" data-original-title="<%# Eval("ServiceName")%>"><%# Eval("ServiceName")%></a></td>
                              <td  width="25%"><%# Eval("ReleaseVersion")%></td>
                              <td  width="20%"> <%# Eval("SystemType")%> </td>
                              <td  width="15%">$<span><%# Eval("WFTCloudPrice")%>/<%# Eval("UsageUnit")%></span></td>
                              <td  width="10%"><a href='/User/express-cloud.aspx?AddToCart=<%# Eval("ServiceID")%>' class="tab-buy"><img src="/img/buy.png"></a>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr>
                              <td  width="30%"><a href="#" class="wftoffer1" rel="popover" data-content="<%# Eval("ServiceDescription")%>" data-original-title="<%# Eval("ServiceName")%>"><%# Eval("ServiceName")%></a></td>
                              <td  width="25%"><%# Eval("ReleaseVersion")%></td>
                              <td  width="20%"> <%# Eval("SystemType")%> </td>
                              <td  width="15%">$<span><%# Eval("WFTCloudPrice")%>/<%# Eval("UsageUnit")%></span></td>
                              <td  width="10%"><a href='/User/express-cloud.aspx?AddToCart=<%# Eval("ServiceID")%>' class="tab-buy"><img src="/img/buy.png"></a>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                  </asp:Repeater>
                  </tbody>
               </table>
             </ItemTemplate>
             <FooterTemplate>
                <tr>
                <td colspan="5">
                <table width="100%">
                    <tr>
                    <td valign="top"><p>*SAP Basis standard support included</p>
                            <p>*Strictly NON-Production only</p>
                            <p>*All Express Cloud services are charged on a monthly flat-rate basis</p>
                    </td>
                    <td>
                        <p>*Customers responsible for SAP License</p>
                        <p>*Test/Demo Systems are for WFT Customers Only</p>
                    </td>
                    </tr>
                </table>
                </td>
                </tr>
              </table>
           </FooterTemplate>
      </asp:Repeater>
                                <!--p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Magnam repellendus itaque aut adipisci perspiciatis magni aperiam sit reprehenderit. Officia earum.</p-->
                                
                                <!-- accordion end -->

                            </div><!-- span8 end -->
                            
                            <!-- span4 end -->

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
     <script>
         $(function () {
             $(".wftoffer1").popover();
         });
         </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerSEOContent" runat="server">
      <hr />
      <div class="row">
        <div class="span12">
             <h2 style="font-size:12px;">WFTCloud offers Cloud based SAP systems & solutions!</h2>
              	<p style="font-size:12px;">WFTCloud.com offers Cloud based SAP systems, services & SAP on Cloud solutions at an unmatched cost. Utilize the WFT's expertise for Cloud based SAP solutions & systems, & SAP on Cloud services for your business. Call Now!!!<p>
                <h2 style="font-size:12px;">Pay per use model for SAP in the Cloud, SAP on Cloud & SAP Cloud computing Solutions.</h2>
                <p style="font-size:12px;">We focus on reducing your ERP implementation cost by introducing pay per use model for SAP in the Cloud, SAP on Cloud & SAP Cloud computing solution. To know more about our pricing package for SAP in the Cloud, SAP on Cloud & SAP Cloud computing solution Contact Us Now! <p>
                <h2 style="font-size:12px;">SAP Certified provider of Cloud based SAP Systems & SAP on Cloud solutions and SAP in the Cloud services.</h2>
                <p style="font-size:12px;">WFTCloud is a certified provider of Cloud based SAP system & solution; SAP in the Cloud services, SAP on Cloud services & SAP Cloud computing solutions. Get implementation of Cloud based SAP solution & system, SAP in the Cloud services & SAP Cloud computing solutions within a faction of conventional cost. <p>

              <%--        	<p style="font-size:12px;"><strong>WFTCloud offers Cloud based SAP systems & solutions!</strong><br>
WFTCloud.com offers Cloud based SAP systems, services & SAP on Cloud solutions at an unmatched cost. Utilize the WFT's expertise for Cloud based SAP solutions & systems, & SAP on Cloud services for your business. Call Now!!!<p>
                <p style="font-size:12px;"><strong>Pay per use model for SAP in the Cloud, SAP on Cloud & SAP Cloud computing Solutions.</strong><br>
We focus on reducing your ERP implementation cost by introducing pay per use model for SAP in the Cloud, SAP on Cloud & SAP Cloud computing solution. To know more about our pricing package for SAP in the Cloud, SAP on Cloud & SAP Cloud computing solution Contact Us Now! <p>
                <p style="font-size:12px;"><strong>SAP Certified provider of Cloud based SAP Systems & SAP on Cloud solutions and SAP in the Cloud services.</strong><br>
WFTCloud is a certified provider of Cloud based SAP system & solution; SAP in the Cloud services, SAP on Cloud services & SAP Cloud computing solutions. Get implementation of Cloud based SAP solution & system, SAP in the Cloud services & SAP Cloud computing solutions within a faction of conventional cost. <p>--%>

        </div>
      </div>
</asp:Content>
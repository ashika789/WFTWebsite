<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd.Master" AutoEventWireup="true" CodeBehind="FAQ.aspx.cs" Inherits="WFTCloud.User.FAQ" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <!-- Including main template styles -->
        <link rel="stylesheet" href="/css/main.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<section id="content">
               
                <div class="white-section">
                    
                    <div class="container">

                        <div align="justify" class="row">
 
                            <div class="span12">
                                   <div class="breadcrumb">
                                    <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" />
                                    <asp:SiteMapPath ID="SiteMap1" runat="server"></asp:SiteMapPath>
                                </div>

                                <h4>Welcome to WFTCloud.com</h4>

                                <p>WFTCloud has pioneered the concept of providing online SAP access or remote SAP access at very affordable rates. We are a certified provider of low cost and affordable SAP in the Cloud systems &amp; SAP on Cloud solutions.</p>
                                
                                <div class="tabs">
                                    
                                    <ul class="tab-nav">
                                        <li class="active"><a data-toggle="tab" href="#tab-one">General</a></li>
                                        <li><a data-toggle="tab" href="#tab-two">Technical</a></li>
                                        <li><a data-toggle="tab" href="#tab-three">Sales</a></li>
                                    </ul>

                                    <div class="tab-content">
                                        <div id="tab-one" class="tab-cont active">                                       
                                            <asp:Repeater ID="rptrGeneralFAQ" runat="server">
                                            <ItemTemplate>
                                            <div>
                                                <h5><%# Eval("Question") %></h5>
                                                <p>
                                                    <%# Eval("Answer") %>
                                                </p>
                                            </div>
                                            </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                        <div id="tab-two" class="tab-cont">
                                            <asp:Repeater ID="rptrTechnical" runat="server">
                                            <ItemTemplate>
                                            <div>
                                                <h5><%# Eval("Question") %></h5>
                                                <p>
                                                    <%# Eval("Answer") %>
                                                </p>
                                            </div>
                                            </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                        <div id="tab-three" class="tab-cont">
                                            <asp:Repeater ID="rptrSales" runat="server">
                                            <ItemTemplate>
                                            <div>
                                                <h5><%# Eval("Question") %></h5>
                                                <p>
                                                    <%# Eval("Answer") %>
                                                </p>
                                            </div>
                                            </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>

                                </div> <!-- tabbable end -->
                                
                            </div><!-- span6 end -->
                        
                        </div><!-- row end -->     

                    </div><!-- conteiner end -->

                </div><!-- white-section end -->

            </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerSEOContent" runat="server">
      <hr />
      <div class="row">
        <div class="span12">
            <div class="row">
                <div class="span12">
                    	<h2 style="font-size:12px;">WFTCloud offers affordable & low cost SAP systems!</h2>
                <p style="font-size:12px;">WFTCloud.com offers affordable & low cost SAP systems & cheap cloud based SAP services at an unmatched cost. With our Cloud SAP solutions & online SAP access model, you can get your SAP provisioned in minutes. Utilize WFT's expertise in low cost & affordable SAP system & cheap cloud based SAP services for your business. Call Now!!!<p>
                <h2 style="font-size:12px;">Online SAP access model for Legal SAP online access & SAP remote access.</h2>
                <p style="font-size:12px;">We focused on reducing your SAP implementation cost by introducing pay per use model for legal SAP online access & SAP remote access. With our affordable & low cost Cloud SAP solutions & online SAP access model, you can get your SAP provisioned in minutes. To know more about our pricing packages for SAP remote access & online SAP access Contact Us Now! <p>
                <h2 style="font-size:12px;">SAP certified provider of affordable & low cost SAP system and SAP remote access.</h2>
                <p style="font-size:12px;">WFTCloud is a certified provider of cheap, affordable & low cost SAP system, legal SAP online access & SAP remote access. Get implementation of cheap, low cost & affordable SAP system, online SAP access & SAP remote access at a fraction of conventional cost.<p>

              <%--  <h2 style="font-size:12px;">WFTCloud offers affordable & low cost SAP systems!</h2>
                <p style="font-size:12px;">WFTCloud.com offers affordable & low cost SAP systems & cheap cloud based SAP services at an unmatched cost. With our Cloud SAP solutions & online SAP access model, you can get your SAP provisioned in minutes. Utilize WFT's expertise in low cost & affordable SAP system & cheap cloud based SAP services for your business. Call Now!!!<p>
                <p style="font-size:12px;"><strong>Online SAP access model for Legal SAP online access & SAP remote access.</strong><br>
We focused on reducing your SAP implementation cost by introducing pay per use model for legal SAP online access & SAP remote access. With our affordable & low cost Cloud SAP solutions & online SAP access model, you can get your SAP provisioned in minutes. To know more about our pricing packages for SAP remote access & online SAP access Contact Us Now! <p>
                <p style="font-size:12px;"><strong>SAP certified provider of affordable & low cost SAP system and SAP remote access.</strong><br>
WFTCloud is a certified provider of cheap, affordable & low cost SAP system, legal SAP online access & SAP remote access. Get implementation of cheap, low cost & affordable SAP system, online SAP access & SAP remote access at a fraction of conventional cost.<p>
                --%>
                </div>
            </div>
        </div>
        </div>
</asp:Content>
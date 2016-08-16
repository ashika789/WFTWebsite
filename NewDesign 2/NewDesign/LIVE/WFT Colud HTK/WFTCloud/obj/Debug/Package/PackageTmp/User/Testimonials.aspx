<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd.Master" AutoEventWireup="true" CodeBehind="Testimonials.aspx.cs" Inherits="WFTCloud.User.Testimonials" %>

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
                    <h4>Testimonials</h4>
                    <asp:Repeater ID="rptrTestimonial" runat="server">
                        <ItemTemplate>
                            <div id="testimonial<%#Eval("TestimonialID")%>">
                                <div style="margin-left: 0px;" class="comment-body">
                                   <%-- <div class="comment-meta">
                                        <a href="#" class="comment-user"><%# Eval("CustomerName") %> </a>
                                        &nbsp; Customer Since :  <%# Eval("CustSince","{0:MM/dd/yyyy}") %>
                                    </div>--%>
                                    <!-- comment-meta end -->
                                    <div class="comment-text">
                                     <%--   <strong>No. of Dedicated Systems : </strong>
                                        <%# Eval("NoOfDedicatedSystems") %>
                                        <br />

                                        <strong>Usage : </strong>
                                        <%# Eval("Usage") %>
                                        <br />
                                        <br />--%>
                                        <p>
                                        <%# Eval("Testimonial1").ToString().Replace("\r\n\r\n","<br/><br/>") %>
                                       </p>
                                        <div>
                                                    <strong>
                                                        <%# Eval("CustomerName") != null ?(Eval("CustomerName").ToString()!=""?Eval("CustomerName")+",":"" ):Eval("CustomerName")+","%>
                                                        <%# Eval("Designation") != null ?(Eval("Designation").ToString()!=""?Eval("Designation")+",":"" ):Eval("Designation")+","%>
                                                        <%# Eval("CustOrg") != null ?(Eval("CustOrg").ToString()!=""?Eval("CustOrg"):"" ):Eval("CustOrg")%>
                                                    </strong>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <!-- row end -->

                <!-- row end -->

            </div>
            <!-- conteiner end -->

        </div>
        <!-- white-section end -->

    </section>
    <!-- content end -->
</asp:Content>



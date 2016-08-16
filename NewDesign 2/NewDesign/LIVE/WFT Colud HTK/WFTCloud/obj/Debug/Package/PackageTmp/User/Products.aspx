<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="WFTCloud.User.Products" %>
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
          <%--<h4>Other Products</h4>
          <div class="row">
            <div class="span12">
                <div class="comment-body" style="margin-left:0px;">
                  <h4>COODAM Education Portal (Coodam.com)</h4>
                  <p> Coodam.com is now available to tutors and students for online teaching and learning. Tutors and Students can sign up free and post their listing. Students have the option to name the price they are willing to pay and allow the Tutors to Bid.</p>
                  <br><a href="http://wftus.com/coodam.html" target="_blank" class="prev"><b>Read More</b></a>
                </div>
            </div>
          </div>
          <div class="row">
            <div class="span12">
                <div class="comment-body" style="margin-left:0px;">
                  <h4>MOZHI Automation</h4>
                  <p>WFT has developed a groundbreaking solution for SAP customers looking to automate repeatable processes that take place within the SAP landscape. Mozhi uses object-oriented technology to automate either SAP GUI transactions and/or external routines. Through a single interface you can automate and control multiple SAP processes across multiple systems in a landscape.</p>
                  <p>Mozhi EPP also has integrated reporting and audit capabilities which can be useful in tracking systems change activity or for regulatory and audit compliance purposes.</p>
                  <br><a href="http://wftus.com/mozhi.html" target="_blank" class="prev"><b>Read More</b></a>
                </div>
            </div>
          </div>--%>
         <%-- <div class="row">
              <div class="span12">
                  <div class="comment-body" style="margin-left:0px;">
                      <h4>WFTCloud</h4>
                      <p>WFT has pioneered to provide SAP systems on the Cloud to end users using state of the art SAP Cloud computing solutions.</p>
                      <br><a href="http://www.wftcloud.com/index.aspx" target="_blank" class="prev"><b>Read More</b></a>
                  </div>
              </div>
          </div>--%>
        </div>
        <!-- row end --> 
        
      </div>
      <!-- conteiner end --> 
      
    </div>
    <!-- white-section end --> 
    
  </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerSEOContent" runat="server">
</asp:Content>

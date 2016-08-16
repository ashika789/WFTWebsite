<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd.Master" AutoEventWireup="true" CodeBehind="press-release-staticcontent.aspx.cs" Inherits="WFTCloud.User.press_release_staticcontent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <!-- Including main template styles -->
        <link rel="stylesheet" href="/css/main.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="white-section">
      <div class="container">
        <div class="row">
          <h4>Press Release </h4>
            <div>
                <div class="comment-body" style="margin-left:0px;">
                    <div class="comment-meta"> 
                        <a class="comment-user" href="#">
                            <asp:Label ID="lblPressReleaseHeader" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </a><br> 
                        <span class="comment-date">
                            <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
                        </span> 
                    </div>
                    <!-- comment-meta end -->
                
                    <div class="comment-text">
                        <p>
                            <asp:Label ID="lblCompanyDescription" runat="server" Text=""></asp:Label>
                        </p>
                        <h6><asp:Label ID="lblPlaceName" runat="server" Text="" Font-Bold="true"></asp:Label></h6>
                        <div style="clear:both;"><hr><br></div>
                        <div class="span7">
                            <asp:Label ID="lblPressReleaseContent" runat="server" Text=""></asp:Label>
                            <a id="ActualPRLink" runat="server" target="_blank" visible="false"><img src="../img/readmore.png" alt="Read More..."></a>
                        </div>
                         <div class="span4">
                            <asp:Panel runat="server" ID="pnlVideo" Visible="false">
                               <iframe runat="server" id="iframe" allowfullscreen="" frameborder="0" height="196" mozallowfullscreen="" origheight="196" origwidth="307" webkitallowfullscreen="" width="307" wmode="transparent"></iframe>
                            <br> <hr><br>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pnlImage" Visible="false">
                               <p align="center"><img runat="server" id="Image" src="img/press-release/1.png" style="border:1px solid #ededed;"></p>
                            </asp:panel>
                            <asp:Panel runat="server" ID="pnlQuote" Visible="false">
                               <blockquote  class="article" style="width:100%;"><asp:Label ID="lblCaption" runat="server" Text=""></asp:Label></blockquote>
                            </asp:panel>
                         </div>
                        <div style="clear:both;"><hr><br></div>
                        <p>  <a href="press-release-content.aspx">Back to Press Release</a>
                        </p>
                    </div>
                     <!-- comment-text end --> 
                </div>
                <!-- comment-body end --> 
            </div>
         </div>
      </div>
     </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerSEOContent" runat="server">
</asp:Content>

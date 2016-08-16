<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd.Master" AutoEventWireup="true" CodeBehind="press-release-content.aspx.cs" Inherits="WFTCloud.User.press_release_content" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <!-- Including main template styles -->
        <link rel="stylesheet" href="/css/main.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="content">
        <div class="white-section">
            <div class="container">
                <div class="row">

                    <h4>Press Release</h4>
                    <asp:Repeater ID="rptrPressRelease" runat="server">
                        <ItemTemplate>
                          <div class="span5"><div>
                              <div class="comment-body" style="margin-left:0px;">
                                  <div class="comment-meta">
                                       <a class="comment-user" href="#"><%# Eval("CompanyName") %></a> <span class="comment-date"><%# Eval("PressReleaseDate", "{0:MMM dd, yyyy}") %></span> 
                                  </div>
                                  <div class="comment-text">
                                      <p>
                                          <asp:Label ID="lblHeader" runat="server" Text='<%# (Eval("PressReleaseHeader").ToString().Length>=85?Eval("PressReleaseHeader").ToString().Substring(0,84)+" ..":Eval("PressReleaseHeader") )%>'></asp:Label>
                                          
                                        <br><a href="/User/press-release-staticcontent.aspx?PressReleaseID=<%# Eval("PressReleaseID") %>" target="_blank" class="prev">Read More</a>
                                      </p>
                                  </div>
                              </div>
                          </div></div>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerSEOContent" runat="server">
</asp:Content>

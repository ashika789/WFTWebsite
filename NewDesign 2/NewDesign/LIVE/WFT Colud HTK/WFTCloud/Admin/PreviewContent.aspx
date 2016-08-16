<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd.Master" AutoEventWireup="true" CodeBehind="PreviewContent.aspx.cs" Inherits="WFTCloud.Admin.PreviewContent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <!-- Including main template styles -->
       <link rel="stylesheet" href="/css/main.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <section id="content">
        <div class="white-section">
            <div class="container">
                <div class="row">
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                </div>
            <br><hr><br>
            </div>
        </div>
        </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerSEOContent" runat="server">
</asp:Content>

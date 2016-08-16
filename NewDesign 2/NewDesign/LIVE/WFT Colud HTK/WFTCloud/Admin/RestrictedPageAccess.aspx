<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminPages.Master" AutoEventWireup="true" CodeBehind="RestrictedPageAccess.aspx.cs" Inherits="WFTCloud.Admin.RestictedPageAccess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="alert alert-error">
        <h4><i class="icon-remove"></i>&nbsp;&nbsp;You don't have privileges to access the page . Please contact Admin to access the page.</h4>
      </div>
   
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="WFTCloud.User.Checkout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Including main template styles -->
    <link rel="stylesheet" href="/css/main.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<h1>Cart Items</h1>
<table class="table alert alert-warning hidden-phone">
    <tr class="th-head">
    <th width="30%" align="left">SAP Module</th>
    <th width="25%">Release</th>
    <th width="20%" align="left">Type of System</th>
    <th width="10%">Price</th>
    <th width="10%">Remove</th>
    </tr>
    <asp:Repeater ID="rptrCartItems" runat="server">
    <ItemTemplate>
        <tr class="th-body">
            <td><a href="servicedetails.aspx" class="tweenbox"><%# Eval("ServiceName")%></a></td>
            <td><%# Eval("ReleaseVersion")%></td>
            <td><%# Eval("SystemType")%></td>
            <td>$<span><%# Eval("InitialHoldAmount")%></a></td>
            <td><a href='/User/Checkout.aspx?RemoveService=<%# Eval("ServiceID")%>'>Remove</a></td>
        </tr>
    </ItemTemplate>
    </asp:Repeater>
    <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>Total: $<asp:Label ID="lblTotal" runat="server" Font-Bold="true"></asp:Label></td>
        <td>&nbsp;</td>
    </tr>
    <tr>
    <td colspan="5">&nbsp;</td>
    </tr>
</table>
</asp:Content>

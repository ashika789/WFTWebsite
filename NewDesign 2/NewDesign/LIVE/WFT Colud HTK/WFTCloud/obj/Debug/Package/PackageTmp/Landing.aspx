<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Landing.aspx.cs" Inherits="WFTCloud.Landing" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .stylediv {
            background-image: url("../img/BG.png");
            width: 800px;
            height: 340px;
        }

        .stylediv1 {
            position: absolute;
            right: 23%;
            top: 33%;
            z-index: 2;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="stylediv">
            <div class="stylediv1">
                <table>
                    <tr>
                        <td colspan="2">
                            <div id="divRegisterSuccess" runat="server" visible="false" class="alert alert-success ">
                                <i class="icon-remove"></i>
                                <asp:Label ID="lblRegisterSuccess" Text="Admin has been notified about a Infrastructure Migration request, our administrator will contact you earlier." runat="server"></asp:Label>
                                <span></span>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%;">First Name
                                                        <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" ValidationGroup="Register" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="input-medium" AutoCompleteType="Disabled" autocomplete="off" Width="300px"></asp:TextBox>
                        </td>

                    </tr>

                    <tr>
                        <td style="width: 40%;">Last Name
                                                        <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ValidationGroup="Register" ControlToValidate="txtLastName" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLastName" runat="server" CssClass="input-medium" AutoCompleteType="Disabled" autocomplete="off" Width="300px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="width: 40%;">Email ID
                                                        <asp:RequiredFieldValidator ID="rfvEmailID" runat="server" ValidationGroup="Register" ControlToValidate="txtEmailID" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revEmailID" runat="server" ValidationGroup="Register" ControlToValidate="txtEmailID" ErrorMessage="*" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmailID" runat="server" CssClass="input-medium" AutoCompleteType="Disabled" autocomplete="off" Width="300px"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 40%;">Contact Number
                                                        <asp:RequiredFieldValidator ID="rfv" runat="server" ValidationGroup="Register" ControlToValidate="txtContactNumber" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtContactNumber" ErrorMessage="*" ForeColor="Red" ValidationExpression="^\+?[0-9]+$" ValidationGroup="Register"></asp:RegularExpressionValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtContactNumber" runat="server" CssClass="input-medium" Width="300px"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td></td>
                        <td style="text-align: center">
                            <br />
                            <asp:Button ID="btnRegisterCode" CssClass="btn btn-primary" runat="server" Text="Register" ValidationGroup="Register" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>

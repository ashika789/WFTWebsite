<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd.Master" AutoEventWireup="true" CodeBehind="SAP-Cloud-computing.aspx.cs" Inherits="WFTCloud.User.SAP_Cloud_computing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Including main template styles -->
    <link rel="stylesheet" href="/css/main.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager ID="scriptManager" runat="server"></asp:ScriptManager>
    <section id="content">
               
                <div class="white-section">
                    
                    <div class="container">

                        <div class="row">
                            <div class="breadcrumb">
                                    <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" />
                                    <asp:SiteMapPath ID="SiteMap1" runat="server"></asp:SiteMapPath>
                                </div>
                            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                          
                            <div class="span12">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 50%;">
                                            <div>
                                                <h4>Register:</h4>
                                            </div>
                                            <table>
                                                  <tr>
                                                <td colspan="2">
                                                        <div id="divRegisterSuccess" runat="server" visible="false" class="alert alert-success ">
                                                            <i class="icon-remove"></i>
                                                            <asp:Label ID="lblRegisterSuccess" Text="Admin has been notified about your training request, our administrator will contact you earlier." runat="server"></asp:Label>
                                                            <span></span>
                                                        </div>
                                                    </td>
                                                    </tr>
                                                <tr>
                                                    <td style="width: 30%; vertical-align: top;">First Name
                                                        <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" ValidationGroup="Register" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="input-medium" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td style="width: 30%; vertical-align: top;">Middle Name
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtMiddleName" runat="server" CssClass="input-medium" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 30%; vertical-align: top;">Last Name
                                                        <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ValidationGroup="Register" ControlToValidate="txtLastName" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtLastName" runat="server" CssClass="input-medium" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" style="width: 30%; vertical-align: top;">Email ID
                                                        <asp:RequiredFieldValidator ID="rfvEmailID" runat="server" ValidationGroup="Register" ControlToValidate="txtEmailID" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revEmailID" runat="server" ValidationGroup="Register" ControlToValidate="txtEmailID" ErrorMessage="*" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtEmailID" runat="server" CssClass="input-medium" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" style="width: 30%; vertical-align: top;">Course
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Register" ControlToValidate="ddlCourse" InitialValue="0" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:UpdatePanel ID="CourseUpdatePanel" runat="server">
                                                                            <ContentTemplate>
                                                        <asp:DropDownList ID="ddlCourse" runat ="server" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                       </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td valign="top" style="width: 30%; vertical-align: top;">Module
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Register" ControlToValidate="ddlModule" InitialValue="0" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                     <asp:UpdatePanel ID="ModuleUpdatePanel" runat="server">
                                                                            <ContentTemplate>
                                                         <asp:DropDownList ID="ddlModule" runat ="server" >
                                                        </asp:DropDownList>
                                                                                 </ContentTemplate>
                                                                            <Triggers>
                                                                                <asp:AsyncPostBackTrigger ControlID="ddlCourse" />
                                                                            </Triggers>
                                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 30%; vertical-align: top;">Contact Number
                                                        <asp:RequiredFieldValidator ID="rfv" runat="server" ValidationGroup="Register" ControlToValidate="txtContactNumber" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtContactNumber" ErrorMessage="*" ForeColor="Red" ValidationExpression="^\+?[0-9]+$" ValidationGroup="Register"></asp:RegularExpressionValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtContactNumber" runat="server" CssClass="input-medium"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 30%; vertical-align: top;">City
                                                        <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ErrorMessage="*" ValidationGroup="Register" ControlToValidate="txtCity"  ForeColor="Red"  ></asp:RequiredFieldValidator>
                                                   </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCity" runat="server" CssClass="input-medium"></asp:TextBox>
                                                        
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 30%; vertical-align: top;">State
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ValidationGroup="Register" ControlToValidate="txtState"  ForeColor="Red"  ></asp:RequiredFieldValidator>
                                                   </td>
                                                    <td>
                                                       <asp:TextBox ID="txtState" runat="server" CssClass="input-medium"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td style="width: 30%; vertical-align: top;">Country
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ValidationGroup="Register" ControlToValidate="txtCountry"  ForeColor="Red"  ></asp:RequiredFieldValidator>
                                                   </td>
                                                    <td>
                                                       <asp:TextBox ID="txtCountry" runat="server" CssClass="input-medium"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 30%; vertical-align: top;">How did you hear about us?
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlHearAboutUs" runat="server" Width="165px">
                                                            <asp:ListItem Text="Google" Value="Google"></asp:ListItem>
                                                            <asp:ListItem Text="Facebook" Value="Facebook"></asp:ListItem>
                                                            <asp:ListItem Text="LinkedIn" Value="LinkedIn"></asp:ListItem>
                                                            <asp:ListItem Text="Twitter" Value="Twitter"></asp:ListItem>
                                                            <asp:ListItem Text="Press Release" Value="Press Release"></asp:ListItem>
                                                            <asp:ListItem Text="Others" Value="Others"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>

                                                </tr>
                                                 <tr>
                                                    <td style="width: 30%; vertical-align: top;">Comments
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Register" ControlToValidate="txtComments" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtComments" runat="server" CssClass="input-medium" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <br />
                                                        <asp:Button ID="btnRegisterCode" CssClass="btn btn-primary" runat="server" Text="Register" ValidationGroup="Register" OnClick="btnRegisterCode_Click"/>
                                                    </td>
                                                </tr>
                                            </table>

                                        </td>
                                    </tr>
                                </table>
                                </div>
                            </div>
                       </div><!-- row end -->

                    </div><!-- conteiner end -->

                </div><!-- white-section end -->

            </section>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerSEOContent" runat="server">
</asp:Content>

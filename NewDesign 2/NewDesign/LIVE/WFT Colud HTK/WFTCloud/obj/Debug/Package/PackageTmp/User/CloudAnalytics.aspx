<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd.Master" AutoEventWireup="true" CodeBehind="CloudAnalytics.aspx.cs" Inherits="WFTCloud.User.CloudAnalytics" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Including main template styles -->
    <link href="../css/Gridstyle.css" rel="stylesheet" type="text/css" />

    <link rel="stylesheet" href="/css/main.css">
     <style>
        .stylediv {
            position: absolute;
            background-image: url("../img/box.png");
            width: 580px;
            height: 509px;
            right: 0px;
            top: 16%;
            right: -8%;
            z-index: 2;
        }

       
    </style>
    <script type ="text/javascript">
       
        function ClickLink() {
            
            document.getElementById('ContentPlaceHolder1_SamplePDF').click();
        }

        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="content">
        <asp:ToolkitScriptManager ID="CloudAnalyticsToolkitScriptManager" runat="server">
        </asp:ToolkitScriptManager>
        <div class="white-section">

            <div class="container">

                <div class="row">
                  
                    <div style="margin-left: 10px;">
                        <img src="../img/bg.jpg" width="900" height="318" alt="">
                    </div>
                    <div class="stylediv">

                        <table style="width: 100%;margin-left: 30px; margin-top: 25px;">
                            <tr>
                                <td style="width: 50%;">
                                    <table>
                                       
                                        <tr>
                                            <td style="width: 32%; vertical-align: top">First Name <span style="color:red;">*</span>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFirstName" ValidationGroup="Register" Display="None" ErrorMessage="First name required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                 <asp:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender8" TargetControlID="RequiredFieldValidator2" Width="180px" CssClass="customCalloutStyle" PopupPosition="Right" WarningIconImageUrl="~/img/warning-icon.png" CloseImageUrl="~/img/iconclose.png" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="input-medium" AutoCompleteType="Disabled" autocomplete="off" Width="200px" ></asp:TextBox>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="width: 30%; vertical-align: top">Last Name <span style="color:red;">*</span>
                                                        <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtLastName" ValidationGroup="Register" Display="None" ErrorMessage="Last name required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                <asp:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender9" TargetControlID="rfvFirstName" Width="180px" CssClass="customCalloutStyle" PopupPosition="Right" WarningIconImageUrl="~/img/warning-icon.png" CloseImageUrl="~/img/iconclose.png" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtLastName" runat="server" CssClass="input-medium" AutoCompleteType="Disabled" autocomplete="off" Width="200px"></asp:TextBox>
                                            </td>

                                        </tr>

                                        <tr>
                                            <td style="width: 30%; vertical-align: top">Company Name<span style="color:red;">*</span>
                                                <asp:RequiredFieldValidator ID="rfvCompanyName" runat="server" ControlToValidate="txtCompanyName" ValidationGroup="Register" Display="None" ErrorMessage="Company name required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                <asp:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1" TargetControlID="rfvCompanyName" Width="180px" CssClass="customCalloutStyle" PopupPosition="Right" WarningIconImageUrl="~/img/warning-icon.png" CloseImageUrl="~/img/iconclose.png" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCompanyName" runat="server" CssClass="input-medium" AutoCompleteType="Disabled" autocomplete="off" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30%; vertical-align: top">Email ID <span style="color:red;">*</span>
                                                        <asp:RequiredFieldValidator ID="rfvEmailID" runat="server" ValidationGroup="Register" ControlToValidate="txtEmailID" Display="None" ErrorMessage="Email required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revEmailID" runat="server" ValidationGroup="Register" ControlToValidate="txtEmailID" Display="None" ErrorMessage="Type valid email address" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                <asp:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender10" TargetControlID="rfvEmailID" Width="150px" CssClass="customCalloutStyle" PopupPosition="Right" WarningIconImageUrl="~/img/warning-icon.png" CloseImageUrl="~/img/iconclose.png" />
                                        <asp:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender11" TargetControlID="revEmailID" Width="210px" CssClass="customCalloutStyle" PopupPosition="Right" WarningIconImageUrl="~/img/warning-icon.png" CloseImageUrl="~/img/iconclose.png" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEmailID" runat="server" CssClass="input-medium" AutoCompleteType="Disabled" autocomplete="off" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30%; vertical-align: top">Telephone<span style="color:red;">*</span>
                                                <asp:RequiredFieldValidator ID="rfvTelephone" runat="server" ControlToValidate="txtTelephone" ValidationGroup="Register" Display="None" ErrorMessage="Telephone number required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                <asp:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender2" TargetControlID="rfvTelephone" Width="180px" CssClass="customCalloutStyle" PopupPosition="Right" WarningIconImageUrl="~/img/warning-icon.png" CloseImageUrl="~/img/iconclose.png" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtTelephone" runat="server" CssClass="input-medium" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>



                                        <tr>
                                            <td style="width: 30%; vertical-align: top">Current SAP Modules
                                            </td>
                                            <td>
                                              <asp:CheckBoxList ID="chkSAPModules" runat="server" RepeatDirection="Horizontal" Width ="350px">
                                                    <asp:ListItem Value="ERP" Text=" ERP"></asp:ListItem>
                                                    <asp:ListItem Value="BW" Text=" BW"></asp:ListItem>
                                                    <asp:ListItem Value="CRM" Text=" CRM"></asp:ListItem>
                                                    <asp:ListItem Value="EP" Text=" EP"></asp:ListItem>
                                                    <asp:ListItem Value="SOLMAN" Text=" SOLMAN"></asp:ListItem>
                                                </asp:CheckBoxList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                               
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30%; vertical-align: top">Others
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSAPModulesOthers" runat="server" CssClass="input-medium" AutoCompleteType="Disabled" autocomplete="off" Width="200px"></asp:TextBox>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="width: 30%; vertical-align: top">Current Landscape(s)
                                                      <%--  <asp:RequiredFieldValidator ID="rfvLandscape" runat="server" ErrorMessage="*" ValidationGroup="Register" ControlToValidate="chkSAPLandscape" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </td>
                                            <td>
                                                
                                               <asp:CheckBoxList ID="chkSAPLandscape" runat="server" RepeatDirection="Horizontal" Width ="350px">
                                                    <asp:ListItem Value="DEV" Text=" DEV"></asp:ListItem>
                                                    <asp:ListItem Value="QA" Text=" QA"></asp:ListItem>
                                                    <asp:ListItem Value="PRD" Text=" PRD"></asp:ListItem>
                                                    <asp:ListItem Value="SBX" Text=" SBX"></asp:ListItem>
                                                    <asp:ListItem Value="TRN" Text=" TRN"></asp:ListItem>
                                                   <asp:ListItem Value="PRE-PROD" Text=" PRE-PROD"></asp:ListItem>
                                                </asp:CheckBoxList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30%; vertical-align: top">Others
                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFirstName" ValidationGroup="Register" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtLandscapeOthers" runat="server" CssClass="input-medium" AutoCompleteType="Disabled" autocomplete="off" Width="200px"></asp:TextBox>
                                            </td>

                                        </tr>
                                        <tr>
                                                    <td style="width: 30%; vertical-align: top;">Description
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Register" ControlToValidate="txtDescription" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDescription" runat="server" CssClass="input-medium" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                        <tr>
                                            <td></td>
                                            <td style="text-align:left;">
                                              
                                                
                                                <asp:Button ID="btnViewReport" CssClass="btn btn-primary" runat="server" Text="View Sample Report" ValidationGroup="Register" OnClick="btnViewReport_Click"  />
                                           
                                                    </td>
                                        </tr>
                                         
                                    </table>

                                </td>
                            </tr>
                        </table>


                    </div>

                   <%-- <div style="margin-left: 10px;">
                        <img src="../img/bg1.png" width="900" height="318" alt="">
                    </div>--%>
                </div>
                <!-- row end -->
                   <div id="divRegisterSuccess" runat="server" visible="false" class="alert alert-success ">
                                                    <i class="icon-remove"></i>
                                                    <asp:Label ID="lblRegisterSuccess" Text="Admin has been notified about a Infrastructure Migration request. We will get in touch with you soon." runat="server"></asp:Label>
                                                    <span></span>
                                                </div>
            </div>
            <!-- conteiner end -->

        </div>
        <!-- white-section end -->
        <a id="SamplePDF" runat ="server" href="http://wftcloud.com/UploadedContents/whitepapers/SampleWFTQuote.pdf" target="_self"></a>
        
    </section>
    <div>
        <iframe id="iframeFile" runat="server" style="display: none;"></iframe>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerSEOContent" runat="server">
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd.Master" AutoEventWireup="true" CodeBehind="Newsletter.aspx.cs" Inherits="WFTCloud.User.Newsletter" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Including main template styles -->
    <link href="../css/Gridstyle.css" rel="stylesheet" type="text/css" />

    <link rel="stylesheet" href="/css/main.css">
     <style>
        .stylediv {
            position: absolute;
            width: 580px;
            height: 319px;
            right: 6%;
            top: 35%;
            right: 5%;
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
                   <div id="divRegisterSuccess" runat="server" visible="false" class="alert alert-success ">
                                                    <i class="icon-remove"></i>
                                                    <asp:Label ID="Label1" Text="Your subscription to our list has confirmed. Thank you for subscribing!" runat="server"></asp:Label>
                                                    <span></span>
                                                </div>
                    
                    <div style="margin-left: 140px;">
                        <img src="../img/Newslett.png" alt="">
                    </div>
                    <div class="stylediv">

                        <table style="width: 100%;margin-left: 30px; margin-top: 25px;">
                            
                            <tr>
                                <td style="width: 50%;">
                                    <table>
                                       <tr>
                                            <td style="width: 32%; vertical-align: top">Email Address 
                                                       
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="input-medium" AutoCompleteType="Disabled" autocomplete="off" Enabled="false" Width="200px" ></asp:TextBox>
                                            </td>

                                        </tr>
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
                                            <td></td>
                                            <td style="text-align:left;">
                                              
                                                
                                                <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Submit" ValidationGroup="Register" OnClick="btnSubmitReport_Click"  />
                                           
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

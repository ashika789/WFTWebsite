<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminPages.Master" AutoEventWireup="true" CodeBehind="UserPaymentInformation.aspx.cs" Inherits="WFTCloud.Admin.UserPaymentInformation" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>User List</title>
    <meta name="description" content="List of Users can be seen here and we have an options of Check services, Add new services, Access acount, Delete User, Manage CRM issue and View User Profile" />
    <meta name="keywords" content="WFT Users list" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField runat="server" ID="hidReportType" />
    <div class="row-fluid">
        <div class="span12 widget-container-span">
            <div class="widget-box">
                <div class="widget-header no-border">
                    <ul class="nav nav-tabs" id="Ul1">
                        <li id="liManage" runat="server" class="active">
                            <a>Subscription Histroy</a>
                        </li>
                    </ul>
                </div>
                <div class="widget-body">
                    <div class="widget-main padding-12">
                        <div class="tab-content">
                            <div id="Manage" class="tab-pane in active" runat="server">
                                <div class="row-fluid">
                                             <div>
                                                <div id="divPaymentSuccessMessage" runat="server" visible="false" class="alert alert-block alert-success span6">
                                                    <button data-dismiss="alert" class="close" type="button">
                                                        <i class="icon-remove"></i>
                                                    </button>
                                                    <p>
                                                        <i class="icon-ok"></i>
                                                        <asp:Label ID="lblPaymentSuccessmsg" runat="server"></asp:Label>
                                                    </p>
                                                </div>
                                                <div id="divPaymentErrorMessage" runat="server" visible="false" class="alert alert-error span6">
                                                    <button data-dismiss="alert" class="close" type="button">
                                                        <i class="icon-remove"></i>
                                                    </button>
                                                    <i class="icon-remove"></i>
                                                    <asp:Label ID="lblPaymentErrormsg" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                <asp:MultiView ID="mvSubscriptionDetails" runat="server" ActiveViewIndex="1">
                                    <asp:View ID="vwSubscriptionHistroy" runat="server">
                                         
                                    </asp:View>
                                    <asp:View ID="vmUserPaymentHistoryDetails" runat="server">
                                        <h4><a href='/Admin/UsersServiceHistory.aspx'><< Back</a></h4>
                                        <div class="table-header">
                                            <strong>Payment details of
                                            <asp:Label ID="lblUserNamefor" runat="server"></asp:Label>
                                            </strong>
                                        </div>
                                      <asp:Panel ID="pnlPayment" runat="server" DefaultButton="btnSave">
<%--                                <asp:UpdatePanel runat="server" ID="upnlPayments">
                                    <ContentTemplate>--%>
                                            <div class="table-header">
                                            Card Details	        
                                            </div>
                                            <div class="dataTables_wrapper">
                                            <table class="table table-hover dataTable table-bordered ">
                                            <tr runat="server" id="trExistingCards" visible="false">
                                            <td class="span3">Registered Cards</td>
                                            <td>
                                            <asp:DropDownList runat="server" ID="ddlExistingCards" AutoPostBack="true" OnSelectedIndexChanged="ddlExistingCards_SelectedIndexChanged">

                                            </asp:DropDownList>
                                            <%--<asp:Button ID="btnNewCard" runat="server" align="justify" class="btn btn-primary" CssClass="btn btn-success" OnClick="btnNewCard_Click" Text="New Credit Card" />
                                            <asp:Button ID="btnExistingcard" runat="server" align="justify" class="btn btn-primary" CssClass="btn btn-warning" OnClick="btnExistingcard_Click" Text="Existing Card" Visible="False" />--%>
                                            </td>
                                            </tr>
                                                <tr runat="server" id="trReadOnlyDefaultMod" visible="true">
                                                    <td class="span3">Default Credit Card for Payments</td>
                                                    <td> Yes </td>
                                                </tr>
                                            <tr runat="server" id="trUpdateDefaultMod" visible="false">
                                                <td class="span3">
                                                    Make This Card As My Default Credit Card for Payments
                                                </td>
                                                <td style="vertical-align: middle;">
                                                    <asp:CheckBox runat="server" ID="chkDefalutCreditCard" Checked="true" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnUpdate" runat="server" align="justify" class="btn btn-primary" OnClick="btnUpdate_Click" Text="Update" Visible="False" />
                                                </td>
                                            </tr>
                                                <div runat="server" id="dvCardDetails" visible="false">
                                                                                                                <div runat="server" id="divExistingCrdCardList" visible="false">
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <div class="alert alert-warning">To update any information to your registered Credit Card contact admin@wftcloud.com or you can choose to<br/>
 add a new Credit Card with updated information by choosing New Credit Card option from the Registered Cards drop down menu.<br /></div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Select Credit Card to be deleted from your profile</td>
                                                                    <td>
                                                                        <asp:RadioButtonList runat="server" ID="rblCreditCard" RepeatLayout="Flow" OnSelectedIndexChanged="rblCreditCard_SelectedIndexChanged" AutoPostBack="true" >

                                                                        </asp:RadioButtonList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="rblCreditCard" ValidationGroup="PaymentDetails"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>

                                                            </div>
                                            <tr>
                                            <td class="span3">Credit Card Number</td>
                                            <td>
                                            <asp:TextBox ID="txtCreditCardNumber" runat="server" AutoCompleteType="Disabled" autocomplete="off" AutoPostBack="True" OnTextChanged="txtCreditCardNumber_TextChanged"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtCreditCardNumber" ValidationGroup="PaymentDetails"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="valPassword0" runat="server" ValidationGroup="PaymentDetails" ControlToValidate="txtCreditCardNumber" ErrorMessage="Invaild Card Number"  ForeColor="Red" ValidationExpression="^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$" />
                                            </td>
                                            </tr>
                                            <tr  runat="server" visible="false" id="trCardType">
                                            <td class="span3">Card Type</td>
                                            <td class="">

                                             <div>
                                                <div id="divValidCard" runat="server" visible="false" class="alert alert-block alert-success span5">
                                                    <p>
                                                        <i class="icon-ok"></i>
                                                        <asp:Label ID="lblCardType" runat="server"></asp:Label>
                                                    </p>
                                                </div>
                                                <div id="divInValidCard" runat="server" visible="True" class="alert alert-error span12">
                                                    
                                                    <i class="icon-remove"></i>
                                                    <asp:Label ID="lblInvalidCard" runat="server" Text ="Enter Valid Credit Card Number"></asp:Label>
                                                </div>
                                            </div>
                                            </td>
                                            </tr>
                                            <tr>
                                            <td class="span2">CVV Code</td>
                                            <td>
                                            <asp:TextBox ID="txtVerifiCode1" runat="server" TextMode="Password" style="margin-left: 5px;" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtVerifiCode1" ValidationGroup="PaymentDetails"></asp:RequiredFieldValidator>
                                            </td>
                                            </tr>
                                            <tr>
                                            <td class="span2">Expiration Date</td>
                                            <td>
                                            <asp:DropDownList ID="ddlExpMonth" runat="server" Width="100px">
                                            <asp:ListItem Value="Select">Select</asp:ListItem>
                                            <asp:ListItem Value="01">Jan</asp:ListItem>
                                            <asp:ListItem Value="02">Feb</asp:ListItem>
                                            <asp:ListItem Value="03">Mar</asp:ListItem>
                                            <asp:ListItem Value="04">Apr</asp:ListItem>
                                            <asp:ListItem Value="05">May</asp:ListItem>
                                            <asp:ListItem Value="06">Jun</asp:ListItem>
                                            <asp:ListItem Value="07">Jul</asp:ListItem>
                                            <asp:ListItem Value="08">Aug</asp:ListItem>
                                            <asp:ListItem Value="09">Sep</asp:ListItem>
                                            <asp:ListItem Value="10">Oct</asp:ListItem>
                                            <asp:ListItem Value="11">Nov</asp:ListItem>
                                            <asp:ListItem Value="12">Dec</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue="Select" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlExpMonth" ValidationGroup="PaymentDetails"></asp:RequiredFieldValidator>
                                            <asp:DropDownList ID="ddlExpYear" runat="server"  Width="100px">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>2013</asp:ListItem>
                                            <asp:ListItem>2014</asp:ListItem>
                                            <asp:ListItem>2015</asp:ListItem>
                                            <asp:ListItem>2016</asp:ListItem>
                                            <asp:ListItem>2017</asp:ListItem>
                                            <asp:ListItem>2018</asp:ListItem>
                                            <asp:ListItem>2019</asp:ListItem>
                                            <asp:ListItem>2020</asp:ListItem>
                                            <asp:ListItem>2021</asp:ListItem>
                                            <asp:ListItem>2022</asp:ListItem>
                                            <asp:ListItem>2023</asp:ListItem>
                                            <asp:ListItem>2024</asp:ListItem>
                                            <asp:ListItem>2025</asp:ListItem>
                                            <asp:ListItem>2026</asp:ListItem>
                                            <asp:ListItem>2027</asp:ListItem>
                                            <asp:ListItem>2028</asp:ListItem>
                                            <asp:ListItem>2029</asp:ListItem>
                                            <asp:ListItem>2030</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" InitialValue="Select" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlExpYear" ValidationGroup="PaymentDetails"></asp:RequiredFieldValidator>
                                            </td>
                                            </tr>
                                                </div>
                                            </table>
                                            <br />
                                            </div>
                                            <!-- Payment details-->
                                            <div class="table-header">
                                            Card Holder Details	        
                                            </div>
                                            <div class="dataTables_wrapper">
                                            <table class="table table-hover dataTable table-bordered " id="tblChangePaymentDetails">
                                            <tr>
                                            <td class="span3">Name On Card</td>
                                            <td>
                                            <asp:TextBox ID="txtNameOnCard" runat="server" AutoCompleteType="Disabled" autocomplete="off" MaxLength="30"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtNameOnCard" ValidationGroup="PaymentDetails"></asp:RequiredFieldValidator>
                                            </td>
                                            </tr>
                                            <tr>
                                            <td class="span2">Address1</td>
                                            <td>
                                            <asp:TextBox ID="txtAddress1" runat="server" TextMode="MultiLine" style="margin-left: 5px;" AutoCompleteType="Disabled" autocomplete="off" ></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvAddress1" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="PaymentDetails" ControlToValidate="txtAddress1"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtAddress1" ErrorMessage="Address1 cannot exceed 30 characters in length" ForeColor="Red" ValidationExpression="^[\s\S]{0,30}$" ValidationGroup="PaymentDetails"></asp:RegularExpressionValidator>
                                            </td>
                                            </tr>
                                            <tr>
                                            <td class="span2">Address2(Optional)</td>
                                            <td>
                                            <asp:TextBox ID="txtAddress2" runat="server" TextMode="MultiLine" style="margin-left: 5px;" AutoCompleteType="Disabled" autocomplete="off"  maxlength="30"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="rev3" runat="server" ControlToValidate="txtAddress2" ErrorMessage="Address2 cannot exceed 30 characters in length" ForeColor="Red" ValidationExpression="^[\s\S]{0,30}$" ValidationGroup="PaymentDetails"></asp:RegularExpressionValidator>
                                            </td>
                                            </tr>
                                            <tr>
                                            <td class="span2">City</td>
                                            <td>
                                            <asp:TextBox runat="server" ID="txtCity" AutoCompleteType="Disabled" autocomplete="off" MaxLength="30"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvLastName2" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtCity" ValidationGroup="PaymentDetails"></asp:RequiredFieldValidator>
                                            </td>
                                            </tr>
                                              <%--  <asp:UpdatePanel runat="server" ID="upnlCountryDDLChanged">
                                                    <Triggers >
                                                        <asp:AsyncPostBackTrigger ControlID="ddlCountry" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>--%>

                                            <tr>
                                            <td class="span2">Country</td>
                                            <td>
                                            <asp:DropDownList ID="ddlCountry" runat="server" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="True">
                                                                </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvLastName4" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlCountry" ValidationGroup="PaymentDetails" InitialValue="0"></asp:RequiredFieldValidator>
                                            </td>
                                            </tr>
                                                <tr>
                                                    <td>State</td>
                                                    <td><asp:DropDownList runat="server" ID="ddlState"></asp:DropDownList> 
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlState" ValidationGroup="PaymentDetails" InitialValue="0"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
<%--                                                        </ContentTemplate>
                                                </asp:UpdatePanel>--%>
                                            <tr>
                                            <td class="span2">Postal/Zip Code</td>
                                            <td>
                                                        
                                            <asp:TextBox ID="txtPostalZipCode" runat="server" AutoCompleteType="Disabled" autocomplete="off" MaxLength="30"></asp:TextBox>
                                                        
                                            <asp:RequiredFieldValidator ID="rfvAddress3" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="PaymentDetails" ControlToValidate="txtPostalZipCode"></asp:RequiredFieldValidator>
                                                        
                                            </td>
                                            </tr>
                                            <tr>
                                            <td class="span2">Contact Number</td>
                                            <td>
                                            <asp:TextBox ID="txtContactNumberPtDet" runat="server" AutoCompleteType="Disabled" autocomplete="off" MaxLength="30"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvAddress2" runat="server" ErrorMessage="*" ForeColor="Red" ValidationGroup="PaymentDetails" ControlToValidate="txtContactNumberPtDet"></asp:RequiredFieldValidator>
                                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtContactNumberPtDet" ErrorMessage="Enter Number Only" ForeColor="Red" ValidationExpression="\d+" ValidationGroup="PaymentDetails"></asp:RegularExpressionValidator>--%>
                                            </td>
                                            </tr>
                                                </table>
                                            </div>
                                            <table class="table table-hover dataTable table-bordered ">
                                            <tr runat="server" id="trSaveCrediCard">
                                            <td class="span3"></td>
                                            <td >
                                            <asp:Button ID="btnSave" Text="Save" runat="server" class="btn btn-primary" align="justify" ValidationGroup="PaymentDetails" OnClick="btnSave_Click1"/>
                                                 <asp:Button ID="btnUpdateCreditCard" Text="Update" runat="server" class="btn btn-primary" align="justify" ValidationGroup="PaymentDetails" OnClick="btnUpdateCreditCard_Click" />
                                            </td>

                                            </tr>
                                            </table>
<%--                                                                                </ContentTemplate>
                                </asp:UpdatePanel>--%>
                                            </asp:Panel>
                                        <br />

                                      

                                    </asp:View>

                                </asp:MultiView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        jQuery(function ($) {
            $('#tblOrderHistroy').dataTable(
                {
                    "aoColumnDefs": [
                        { 'bSortable': false, 'aTargets': [5] }
                    ]
                }
         );
        });
    </script>
</asp:Content>

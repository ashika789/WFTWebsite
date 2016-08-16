<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd.Master" AutoEventWireup="true" CodeBehind="CloudCalculator.aspx.cs" Inherits="WFTCloud.User.CloudCalculator" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Including main template styles -->
    <link rel="stylesheet" href="/css/main.css">
    <link href="../css/Gridstyle.css" rel="stylesheet" type="text/css" />
    <link href="../css/shadows.css" rel="stylesheet" type="text/css" />
    <link href="../css/modal.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">
        function Validate() {
            var isValid = false;
            isValid = Page_ClientValidate('Register');
            if (isValid) {
                isValid = Page_ClientValidate('Register1');
            }
            if (isValid) {
                isValid = Page_ClientValidate('Register2');
            }
            return isValid;
        }


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="content">
        <asp:ToolkitScriptManager ID="CloudCalcToolkitScriptManager" runat="server">
        </asp:ToolkitScriptManager>
        
        <div class="white-section" style ="background-image:url(../img/BG-calc.jpg); background-size:100%">

            <div class="container">

                <div class="row">
                    <div id="divRegisterSuccess" runat="server" visible="false" class="alert alert-success ">
                            <i class="icon-remove"></i>
                            <asp:Label ID="lblRegisterSuccess" Text="Admin has been notified about a Infrastructure Migration request. You will shortly receive a report." runat="server"></asp:Label>
                            <span></span>
                        </div>
                    <div style="margin-left: 100px;">
                        <div style="width: 76%; float:left">
                            <img src="../img/calclogo.png" alt="">
                        </div>
                        <div style="width: 24%; float:right"><asp:ImageButton ID="btnViewSample" runat="server" ValidationGroup="Register"  ImageUrl="~/img/viewreport.png" OnClick="btnViewSample_Click"  /></div>
                      
                        
                        <div class="drop-shadow raised" style="width: 90%;">
                            <table style="width: 98%; margin-left: 10px;font-size:12px;">
                                <tr>
                                    <td style="text-align: left; font-family: Cambria; color: #494444;">First Name
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFirstName" Display="None" ValidationGroup="Register" ErrorMessage="First name required" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender8" TargetControlID="RequiredFieldValidator2" Width="180px" CssClass="customCalloutStyle" PopupPosition="Right" WarningIconImageUrl="~/img/warning-icon.png" CloseImageUrl="~/img/iconclose.png" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="input-medium" AutoCompleteType="Disabled" autocomplete="off" Width="200px"></asp:TextBox>
                                    </td>


                                    <td style="text-align: left; font-family: Cambria; color: #494444;">Last Name
                                        <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtLastName" ValidationGroup="Register" Display="None" ErrorMessage="Last name required" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender9" TargetControlID="rfvFirstName" Width="180px" CssClass="customCalloutStyle" PopupPosition="Right" WarningIconImageUrl="~/img/warning-icon.png" CloseImageUrl="~/img/iconclose.png" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLastName" runat="server" CssClass="input-medium" AutoCompleteType="Disabled" autocomplete="off" Width="200px"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="text-align: left; font-family: Cambria; color: #494444;">Company Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCompanyName" runat="server" CssClass="input-medium" AutoCompleteType="Disabled" autocomplete="off" Width="200px"></asp:TextBox>
                                    </td>

                                    <td style="text-align: left; font-family: Cambria; color: #494444;">Email ID
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
                                    <td style="text-align: left; font-family: Cambria; color: #494444;">Telephone
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTelephone" runat="server" CssClass="input-medium" Width="200px"></asp:TextBox>
                                    </td>
                                     <td style="text-align:right;vertical-align:bottom;"  colspan="2">
                                         <div style="position: absolute;right: 3%;top: 68%;right: 3%;z-index: 2;">
                                         <asp:ImageButton ID="btnShowCompute" runat="server" ImageUrl="~/img/ViewSample.png"  />
                        <asp:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnShowCompute"
                            CancelControlID="btnClose" BackgroundCssClass="modalBackground">
                        </asp:ModalPopupExtender></div>
                                    </td>
                                </tr>
                                
                            </table>
                            
                        </div>

                        <div>
                            <div  style="width: 75%; float:left"><h4>COMPUTE </h4></div>
                        <%--<div style="width: 25%; float:right"><asp:ImageButton ID="btnShowCompute" runat="server" ImageUrl="~/img/ViewSample.png"  /></div>
                        <asp:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnShowCompute"
                            CancelControlID="btnClose" BackgroundCssClass="modalBackground">
                        </asp:ModalPopupExtender>--%>
                        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: none;">
                            <div style="margin-left:94%;">
                                <asp:Button ID="btnClose" class="ModelPopUpImg" Width ="36px" Height="36px"  runat="server"/>
                            </div>
                            <div style="height:550px;">
                                <img src="../img/ComputeHelp.png" />
                            </div>
                            
                        </asp:Panel>
                        <div class="drop-shadow raised" style="width: 90%;">
                            <asp:UpdatePanel ID="ComputeDetailsUpdatePanel" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                <ContentTemplate>

                                    <asp:GridView ID="grvComputDetails" runat="server" ShowFooter="True" AutoGenerateColumns="False"
                                        CellPadding="4" GridLines="None" CssClass="Grid" AlternatingRowStyle-CssClass="alt" OnRowDeleting="grvComputDetails_RowDeleting"
                                        PagerStyle-CssClass="pgr" OnRowDataBound="grvComputDetails_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Server Name">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtServerName" runat="server" Width="190px" MaxLength="50" ToolTip="Server Name"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfv" runat="server" ValidationGroup="Register1" ControlToValidate="txtServerName" ErrorMessage="Server name required" Display="None" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtServerName" ErrorMessage="Special characters not allowed" Display="None" ForeColor="Red" ValidationExpression="^[0-9a-zA-Z ]+$" ValidationGroup="Register1"></asp:RegularExpressionValidator>
                                                    <asp:ValidatorCalloutExtender runat="Server" ID="vcle2" TargetControlID="RegularExpressionValidator5" Width="250px" CssClass="customCalloutStyle" PopupPosition="Right" WarningIconImageUrl="~/img/warning-icon.png" CloseImageUrl="~/img/iconclose.png" />
                                                    <asp:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1" TargetControlID="rfv" Width="200px" CssClass="customCalloutStyle" PopupPosition="Right" WarningIconImageUrl="~/img/warning-icon.png" CloseImageUrl="~/img/iconclose.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="OS">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="drpOS" runat="server" Width="90px" ToolTip="Operating System">
                                                        <asp:ListItem Value="Select">Select</asp:ListItem>
                                                        <asp:ListItem Value="Windows">Windows</asp:ListItem>
                                                        <asp:ListItem Value="SUSE Linux">SUSE Linux</asp:ListItem>
                                                        <asp:ListItem Value="Red Hat Linux">Red Hat Linux</asp:ListItem>
                                                        <asp:ListItem Value="AIX">AIX</asp:ListItem>
                                                        <asp:ListItem Value="HP-UX">HP-UX</asp:ListItem>
                                                        <asp:ListItem Value="IBM i">IBM i</asp:ListItem>
                                                        <asp:ListItem Value="Linux on IBM Power">Linux on IBM Power</asp:ListItem>
                                                        <asp:ListItem Value="Linux on IBM system z">Linux on IBM system z</asp:ListItem>
                                                        <asp:ListItem Value="Solaris">Solaris</asp:ListItem>
                                                        <asp:ListItem Value="z/OS">z/OS</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvOS" runat="server" ErrorMessage="Choose Operating System" ValidationGroup="Register1" Display="None" ControlToValidate="drpOS" InitialValue="Select" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <asp:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender3" TargetControlID="rfvOS" Width="210px" CssClass="customCalloutStyle" PopupPosition="Right" WarningIconImageUrl="~/img/warning-icon.png" CloseImageUrl="~/img/iconclose.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CPU">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="drpCPU" runat="server" Width="80px" ToolTip="Number of CPUs">
                                                        <asp:ListItem Value="Select">Select</asp:ListItem>
                                                        <asp:ListItem Value="1">1</asp:ListItem>
                                                        <asp:ListItem Value="2">2</asp:ListItem>
                                                        <asp:ListItem Value="3">3</asp:ListItem>
                                                        <asp:ListItem Value="4">4</asp:ListItem>
                                                        <asp:ListItem Value="5">5</asp:ListItem>
                                                        <asp:ListItem Value="6">6</asp:ListItem>
                                                        <asp:ListItem Value="7">7</asp:ListItem>
                                                        <asp:ListItem Value="8">8</asp:ListItem>
                                                        <asp:ListItem Value="9">9</asp:ListItem>
                                                        <asp:ListItem Value="10">10</asp:ListItem>
                                                        <asp:ListItem Value="11">11</asp:ListItem>
                                                        <asp:ListItem Value="12">12</asp:ListItem>
                                                        <asp:ListItem Value="13">13</asp:ListItem>
                                                        <asp:ListItem Value="14">14</asp:ListItem>
                                                        <asp:ListItem Value="15">15</asp:ListItem>
                                                        <asp:ListItem Value="16">16</asp:ListItem>
                                                        <asp:ListItem Value="17">17</asp:ListItem>
                                                        <asp:ListItem Value="18">18</asp:ListItem>
                                                        <asp:ListItem Value="19">19</asp:ListItem>
                                                        <asp:ListItem Value="20">20</asp:ListItem>
                                                        <asp:ListItem Value="21">21</asp:ListItem>
                                                        <asp:ListItem Value="22">22</asp:ListItem>
                                                        <asp:ListItem Value="23">23</asp:ListItem>
                                                        <asp:ListItem Value="24">24</asp:ListItem>
                                                        <asp:ListItem Value="25">25</asp:ListItem>
                                                        <asp:ListItem Value="26">26</asp:ListItem>
                                                        <asp:ListItem Value="27">27</asp:ListItem>
                                                        <asp:ListItem Value="28">28</asp:ListItem>
                                                        <asp:ListItem Value="29">29</asp:ListItem>
                                                        <asp:ListItem Value="30">30</asp:ListItem>
                                                        <asp:ListItem Value="31">31</asp:ListItem>
                                                        <asp:ListItem Value="32">32</asp:ListItem>
                                                        <asp:ListItem Value="33">33</asp:ListItem>
                                                        <asp:ListItem Value="34">34</asp:ListItem>
                                                        <asp:ListItem Value="35">35</asp:ListItem>
                                                        <asp:ListItem Value="36">36</asp:ListItem>
                                                        <asp:ListItem Value="37">37</asp:ListItem>
                                                        <asp:ListItem Value="38">38</asp:ListItem>
                                                        <asp:ListItem Value="39">39</asp:ListItem>
                                                        <asp:ListItem Value="40">40</asp:ListItem>
                                                        <asp:ListItem Value="41">41</asp:ListItem>
                                                        <asp:ListItem Value="42">42</asp:ListItem>
                                                        <asp:ListItem Value="43">43</asp:ListItem>
                                                        <asp:ListItem Value="44">44</asp:ListItem>
                                                        <asp:ListItem Value="45">45</asp:ListItem>
                                                        <asp:ListItem Value="46">46</asp:ListItem>
                                                        <asp:ListItem Value="47">47</asp:ListItem>
                                                        <asp:ListItem Value="48">48</asp:ListItem>
                                                        <asp:ListItem Value="49">49</asp:ListItem>
                                                        <asp:ListItem Value="50">50</asp:ListItem>
                                                        <asp:ListItem Value="51">51</asp:ListItem>
                                                        <asp:ListItem Value="52">52</asp:ListItem>
                                                        <asp:ListItem Value="53">53</asp:ListItem>
                                                        <asp:ListItem Value="54">54</asp:ListItem>
                                                        <asp:ListItem Value="55">55</asp:ListItem>
                                                        <asp:ListItem Value="56">56</asp:ListItem>
                                                        <asp:ListItem Value="57">57</asp:ListItem>
                                                        <asp:ListItem Value="58">58</asp:ListItem>
                                                        <asp:ListItem Value="59">59</asp:ListItem>
                                                        <asp:ListItem Value="60">60</asp:ListItem>
                                                        <asp:ListItem Value="61">61</asp:ListItem>
                                                        <asp:ListItem Value="62">62</asp:ListItem>
                                                        <asp:ListItem Value="63">63</asp:ListItem>
                                                        <asp:ListItem Value="64">64</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvCPU" runat="server" ErrorMessage="Choose number of CPUs" ValidationGroup="Register1" Display="None" ControlToValidate="drpCPU" InitialValue="Select" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <asp:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender4" TargetControlID="rfvCPU" Width="200px" CssClass="customCalloutStyle" PopupPosition="Right" WarningIconImageUrl="~/img/warning-icon.png" CloseImageUrl="~/img/iconclose.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MEM <br> (GB)">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="drpMemory" runat="server" Width="80px" ToolTip="Memory">
                                                        <asp:ListItem>Select</asp:ListItem>
                                                        <asp:ListItem Value="1">1</asp:ListItem>
                                                        <asp:ListItem Value="2">2</asp:ListItem>
                                                        <asp:ListItem Value="4">4</asp:ListItem>
                                                        <asp:ListItem Value="6">6</asp:ListItem>
                                                        <asp:ListItem Value="8">8</asp:ListItem>
                                                        <asp:ListItem Value="16">16</asp:ListItem>
                                                        <asp:ListItem Value="24">24</asp:ListItem>
                                                        <asp:ListItem Value="32">32</asp:ListItem>
                                                        <asp:ListItem Value="48">48</asp:ListItem>
                                                        <asp:ListItem Value="64">64</asp:ListItem>
                                                        <asp:ListItem Value="96">96</asp:ListItem>
                                                        <asp:ListItem Value="128">128</asp:ListItem>
                                                        <asp:ListItem Value="256">256</asp:ListItem>
                                                        <asp:ListItem Value="512">512</asp:ListItem>
                                                        <asp:ListItem Value="756">756</asp:ListItem>
                                                        <asp:ListItem Value="1024">1024</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvMemory" runat="server" ErrorMessage="Choose memory size" ValidationGroup="Register1" Display="None" ControlToValidate="drpMemory" InitialValue="Select" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <asp:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender15" TargetControlID="rfvMemory" Width="200px" CssClass="customCalloutStyle" PopupPosition="Right" WarningIconImageUrl="~/img/warning-icon.png" CloseImageUrl="~/img/iconclose.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SAPS">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSAPS" runat="server" MaxLength="6" Width="50px" ToolTip="SAPS"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtSAPS" Display="None" ErrorMessage="Numeric value required" ForeColor="Red" ValidationExpression="^\d+$" ValidationGroup="Register1"></asp:RegularExpressionValidator>
                                                    <asp:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender14" TargetControlID="RegularExpressionValidator6" Width="200px" CssClass="customCalloutStyle" PopupPosition="Right" WarningIconImageUrl="~/img/warning-icon.png" CloseImageUrl="~/img/iconclose.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Storage<br> (GB)">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtStorage" runat="server" MaxLength="4" Width="40px" ToolTip="Storage size"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfv5" runat="server" ValidationGroup="Register1" ControlToValidate="txtStorage" Display="None" ErrorMessage="Storage size required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtStorage" Display="None" ErrorMessage="Numeric value required" ForeColor="Red" ValidationExpression="^\d+$" ValidationGroup="Register1"></asp:RegularExpressionValidator>
                                                    <asp:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender5" TargetControlID="rfv5" Width="200px" CssClass="customCalloutStyle" PopupPosition="Right" WarningIconImageUrl="~/img/warning-icon.png" CloseImageUrl="~/img/iconclose.png" />
                                                    <asp:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender6" TargetControlID="RegularExpressionValidator7" Width="200px" CssClass="customCalloutStyle" PopupPosition="Right" WarningIconImageUrl="~/img/warning-icon.png" CloseImageUrl="~/img/iconclose.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="drpType" runat="server" Width="85px" ToolTip="Select your SAP environment type">
                                                        <asp:ListItem>Select</asp:ListItem>
                                                        <asp:ListItem Value="DEV">DEV</asp:ListItem>
                                                        <asp:ListItem Value="QAS">QAS</asp:ListItem>
                                                        <asp:ListItem Value="PRD">PRD</asp:ListItem>
                                                        <asp:ListItem Value="Pre-Prod">Pre-Prod</asp:ListItem>
                                                        <asp:ListItem Value="TRN">TRN</asp:ListItem>
                                                        <asp:ListItem Value="SBX">SBX</asp:ListItem>
                                                        <asp:ListItem Value="TST">TST</asp:ListItem>
                                                        <asp:ListItem Value="Others">Others</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HA">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkHA" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SR">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSR" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BK">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkBK" runat="server" />
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:ImageButton ID="ButtonAdd" runat="server" ToolTip="Add New Row" ValidationGroup="Register1" ImageUrl="~/Img/add.png" OnClick="ButtonAdd_Click" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowDeleteButton="True" DeleteImageUrl="~/Img/Delete.png" ButtonType="Image" />
                                        </Columns>

                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:Label ID="lblNote" runat="server" Text="* HA - High Availability / * SR - Systems Refresh / * BK - Backup" Font-Size="Small"></asp:Label>
                        </div>
                 
                        </div>

                          <div>
                         <div  style="width: 75%; float:left"><h4>STORAGE </h4></div>
                        <%--<div style="width: 25%; float:right"><asp:ImageButton ID="btnShowStorage" runat="server" ImageUrl="~/img/ViewSample.png"  /></div>
                        <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="Panel2" TargetControlID="btnShowStorage"
                            CancelControlID="btnStoClose" BackgroundCssClass="modalBackground">
                        </asp:ModalPopupExtender>--%>
                        <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" align="center" Style="display: none">
                            <div style="margin-left:94%;">
                                <asp:Button ID="btnStoClose" class="ModelPopUpImg" Width ="36px" Height="36px" runat="server"/>
                            </div>
                            <div style="height:auto">
                                <img src="../img/StorageHelp.png" />
                            </div>
                        </asp:Panel>

                        <div class="drop-shadow raised" style="width: 90%;">
                            <asp:UpdatePanel ID="DescriptionUpdatePanel" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="grvDescriptionDetails" runat="server" ShowFooter="True" AutoGenerateColumns="False"
                                        CellPadding="4" GridLines="None" CssClass="Grid" AlternatingRowStyle-CssClass="alt" OnRowDeleting="grvDescriptionDetails_RowDeleting"
                                        PagerStyle-CssClass="pgr">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDescription" runat="server" MaxLength="100" Width="305px" ToolTip="Description"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfv14" runat="server" ValidationGroup="Register2" Display="None" ControlToValidate="txtDescription" ErrorMessage="Description required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <asp:ValidatorCalloutExtender runat="Server" ID="vcle" TargetControlID="rfv14" Width="200px" CssClass="customCalloutStyle" PopupPosition="Right" WarningIconImageUrl="~/img/warning-icon.png" CloseImageUrl="~/img/iconclose.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Volume Type">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="drpVolumeType" runat="server" Width="100px" ToolTip="Choose volume type">
                                                        <asp:ListItem>Select</asp:ListItem>
                                                        <asp:ListItem Value="Magnetic">Magnetic</asp:ListItem>
                                                        <asp:ListItem Value="SSD">SSD</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Volumes">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtVolumes" runat="server" MaxLength="3" Width="50px" ToolTip="Specify the number of volumes for this storage type"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtVolumes" Display="None" ErrorMessage="Numeric value required" ForeColor="Red" ValidationExpression="^\d+$" ValidationGroup="Register"></asp:RegularExpressionValidator>
                                                    <asp:ValidatorCalloutExtender runat="Server" ID="vcle3" TargetControlID="RegularExpressionValidator8" Width="200px" CssClass="customCalloutStyle" PopupPosition="Right" WarningIconImageUrl="~/img/warning-icon.png" CloseImageUrl="~/img/iconclose.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Storage <br> (GB)">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtServerStorage" runat="server" MaxLength="4" Width="66px" ToolTip="Specify the size of this storage media"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfv9" runat="server" ValidationGroup="Register2" ControlToValidate="txtServerStorage" Display="None" ErrorMessage="Storage size required" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtServerStorage" Display="None" ErrorMessage="Numeric value required" ForeColor="Red" ValidationExpression="^\d+$" ValidationGroup="Register2"></asp:RegularExpressionValidator>
                                                    <asp:ValidatorCalloutExtender runat="Server" ID="vcle1" TargetControlID="RegularExpressionValidator9" Width="200px" CssClass="customCalloutStyle" PopupPosition="Right" WarningIconImageUrl="~/img/warning-icon.png" CloseImageUrl="~/img/iconclose.png" />
                                                    <asp:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender7" TargetControlID="rfv9" Width="200px" CssClass="customCalloutStyle" PopupPosition="Right" WarningIconImageUrl="~/img/warning-icon.png" CloseImageUrl="~/img/iconclose.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IOPS">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtIOPS" runat="server" MaxLength="6" Width="66px" ToolTip="Specify the number of IOPS for this storage media"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="txtIOPS" Display="None" ErrorMessage="Numeric value required" ForeColor="Red" ValidationExpression="^\d+$" ValidationGroup="Register2"></asp:RegularExpressionValidator>
                                                    <asp:ValidatorCalloutExtender runat="Server" ID="vcle9" TargetControlID="RegularExpressionValidator10" Width="200px" CssClass="customCalloutStyle" PopupPosition="Right" WarningIconImageUrl="~/img/warning-icon.png" CloseImageUrl="~/img/iconclose.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Snapshot">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSnapshot" runat="server" MaxLength="3" Width="66px" ToolTip="Specify the required snapshot storage size in GB"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ControlToValidate="txtSnapshot" Display="None" ErrorMessage="Numeric value required" ForeColor="Red" ValidationExpression="^\d+$" ValidationGroup="Register2"></asp:RegularExpressionValidator>
                                                    <asp:ValidatorCalloutExtender runat="Server" ID="vcle10" TargetControlID="RegularExpressionValidator11" Width="200px" CssClass="customCalloutStyle" PopupPosition="Right" WarningIconImageUrl="~/img/warning-icon.png" CloseImageUrl="~/img/iconclose.png" />
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:ImageButton ID="btnDescAddition" runat="server" ValidationGroup="Register2" ToolTip="Add New Row" ImageUrl="~/Img/add.png" OnClick="btnDescAddition_Click" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowDeleteButton="True" DeleteImageUrl="~/Img/Delete.png" ButtonType="Image" ItemStyle-CssClass="commandstyle" />
                                        </Columns>

                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                              </div>
                        <br />
                        <br />
                        <div>
                        <div class="drop-shadow raised" style="width: 90%;">
                            <table style="width: 75%; font-family: Cambria; font-weight: bold; font-size:12px; margin-left: auto; margin-right: auto; color: #494444;">
                                <tr>
                                    <td style="text-align: left;">Estimated Bandwidth Used Per Month (GB)
                                                       
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtEstimatedBandwidth" runat="server" MaxLength="5" AutoCompleteType="Disabled" ToolTip="Specify your total egress and ingress traffic for the entire landscape" autocomplete="off" Width="200px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator18" runat="server" Display="None" ControlToValidate="txtEstimatedBandwidth" ErrorMessage="Numeric value required" ForeColor="Red" ValidationExpression="^\d+$" ValidationGroup="Register"></asp:RegularExpressionValidator>
                                        <asp:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender18" TargetControlID="RegularExpressionValidator18" Width="200px" CssClass="customCalloutStyle" PopupPosition="Right" WarningIconImageUrl="~/img/warning-icon.png" CloseImageUrl="~/img/iconclose.png" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left;">Support Type
                        
                                    </td>
                                    <td style="text-align: left; vertical-align:sub;">
                                        <asp:RadioButtonList ID="rdbSupportType" runat="server" Width="300px" ToolTip="Choose the level of required support"
                                            RepeatDirection="Horizontal" RepeatLayout="Table">
                                            <asp:ListItem Text=" 24 x 7" Value="24x7"></asp:ListItem>
                                            <asp:ListItem Text=" 8x5 (M-F)" Value="8x5(M-F)"></asp:ListItem>
                                            <asp:ListItem Text=" Outsourced" Value="Outsourced"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                            </div>
                        
                    </div>
                </div>
                <!-- row end -->
                <div style="margin-left:40%;">
                            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/img/save.png" OnClientClick="return Validate()" ToolTip="Click here to submit" OnClick="btnSave_Click" ForeColor="White" />
                        </div>
            </div>
            <!-- conteiner end -->

        </div>
        <!-- white-section end -->

    </section>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerSEOContent" runat="server">
    <script type="text/javascript">
        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
            m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-22849164-1', 'auto');
        ga('send', 'pageview');

    </script>
</asp:Content>

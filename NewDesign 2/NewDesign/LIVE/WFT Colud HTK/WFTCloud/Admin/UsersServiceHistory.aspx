<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminPages.Master" AutoEventWireup="true" CodeBehind="UsersServiceHistory.aspx.cs" Inherits="WFTCloud.Admin.UsersServiceHistory" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>User Service History</title>
    <meta name="description" content="List of Users can be seen here and we have an options of Check services, Add new services, Access acount, Delete User, Manage CRM issue and View User Profile" />
    <meta name="keywords" content="WFT Users list" />
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript">
        function ValidateSelection() {
            if ($('#tblUsersList input:checked').length > 0) {
                return true;
            }
            else {
                alert('Please select atleast one user.');
                return false;
            }
        }
        </script>
      <asp:ScriptManager ID="scriptManager" runat="server"></asp:ScriptManager>

    <asp:MultiView ID="mvContainer" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwUserList" runat="server">
         
                    <div class="row-fluid">
                        <div class="span12">
                            
                            <div class="row-fluid">
                                <div class="row-fluid">
                                    <%--<asp:CheckBox ID="chkShowAllUser" runat="server" AutoPostBack="true" OnCheckedChanged="chkShowAllUser_CheckedChanged" />
                                    <span class="label label-warning arrowed-right">Show All User</span>--%>
                                    <asp:Label ID="lblMail" runat="server" Text="Enter User Name"></asp:Label>
                                    <asp:TextBox ID="txtSearchUserName" runat="server" class="customer-login-input" placeholder="User Name" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                               
                                    <asp:Button ID="SearcButton" runat ="server" CssClass="btn btn-primary" Text ="Search" OnClick="SearchButton_Click" />
                                </div>  
                               
                                <br />
                                <div class="table-header">
                                    <asp:Label runat="server" ID="lblUserListTitle" Text="Users List"></asp:Label>                                    	        
                                </div>
                                <div class="dataTables_wrapper">
                                    <div role="grid" class="dataTables_wrapper">
                                        <table id="tblUsersList" class="table table-striped table-bordered table-hover dataTable">
                                            <thead>
                                                <tr role="row">
                                                    <th class="center" role="columnheader">
                                                        <label>
                                                            <input type="checkbox" class="ace" />
                                                            <span class="lbl"></span>
                                                        </label>
                                                    </th>
                                                    <th role="columnheader">Name</th>
                                                    <th role="columnheader">Email</th>
                                                    <th role="columnheader">Registration Date and Time</th>
                                                    <th role="columnheader">Status</th>
                                                    <th role="columnheader">Options</th>
                                                </tr>
                                            </thead>
                                            <tbody role="alert">
                                                <asp:Repeater ID="rptrUsersList" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="center">
                                                                <asp:CheckBox ID="chkSelect" runat="server" class="ace" />
                                                                <asp:HiddenField ID="hdnMembershipID" runat="server" Value='<%# Eval("UserMembershipID")%>' />
                                                            </td>
                                                            <td><a href='Userlist.aspx?viewuserprofileid=<%#Eval("UserMembershipID") %>'>
                                                                <%# Eval("FirstName")%> <%# Eval("MiddleName")%> <%# Eval("LastName")%></a>
                                                                
                                                            </td>
                                                            <td><a href='mailto:<%#Eval("EmailID") %>'>
                                                                <asp:Label ID="UserMailID" runat="server" Text='<%# Eval("EmailID")%>'></asp:Label></a></td>
                                                            <td><%# Eval("CreatedOn")%></td>
                                                            <td><%# WFTCloud.GeneralReusableMethods.GetStatusString(Eval("RecordStatus").ToString()) %></td>
                                                            <td>
                                                                <div class="action-buttons">
                                                                    
                                                                    <a data-rel="tooltip" title="View User Profile" href='UserFullHistory.aspx?viewuserprofileid=<%#Eval("UserMembershipID") %>' class="blue">
                                                                        <i class="icon-user bigger-130"></i>
                                                                    </a>
                                                                     <a data-rel="tooltip" title="Update User Credit Card Info" href='UserPaymentInformation.aspx?userid=<%#Eval("UserMembershipID") %>' class="blue">
                                                                        <i class="icon-briefcase bigger-130"></i>
                                                                    </a>
                                                                    <a data-rel="tooltip" title="Update User Email" href='UserProfileMigration.aspx?viewuserprofileid=<%#Eval("UserMembershipID") %>' class="blue">
                                                                        <i class="icon-ok-sign bigger-130"></i>
                                                                    </a>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                
                            </div>
                        </div>
                    </div>
              
        </asp:View>
        
    </asp:MultiView>
    <script type="text/javascript">
        jQuery(function ($) {
            $('#tblUsersList').dataTable(
                {
                    "aoColumnDefs": [
                        { 'bSortable': false, 'aTargets': [0, 5] }
                    ]
                }
         );
        });
    </script>
    <script type="text/javascript">
        function ConfirmAccessAccount() {
            if (confirm("Are you sure want to access this user's account?") == true)
                return true;
            else
                return false;
        }
    </script>
    <script type="text/javascript">
        function FirstEnable() {
            document.getElementById("ContentPlaceHolder1_txtProfileFirstName").disabled = false;
            document.getElementById("FirstEnable").style.visibility = 'hidden';
            document.getElementById("FirstDisable").style.visibility = 'visible';
        }
        function FirstDisable() {
            document.getElementById("ContentPlaceHolder1_txtProfileFirstName").disabled = true;
            document.getElementById("FirstEnable").style.visibility = 'visible';
            document.getElementById("FirstDisable").style.visibility = 'hidden';
        }

        function MiddleEnable() {
            document.getElementById("ContentPlaceHolder1_txtProfileMiddleName").disabled = false;
            document.getElementById("MiddleEnable").style.visibility = 'hidden';
            document.getElementById("MiddleDisable").style.visibility = 'visible';
        }
        function MiddleDisable() {
            document.getElementById("ContentPlaceHolder1_txtProfileMiddleName").disabled = true;
            document.getElementById("MiddleEnable").style.visibility = 'visible';
            document.getElementById("MiddleDisable").style.visibility = 'hidden';
        }

        function LastEnable() {
            document.getElementById("ContentPlaceHolder1_txtProfileLastName").disabled = false;
            document.getElementById("LastEnable").style.visibility = 'hidden';
            document.getElementById("LastDisable").style.visibility = 'visible';
        }
        function LastDisable() {
            document.getElementById("ContentPlaceHolder1_txtProfileLastName").disabled = true;
            document.getElementById("LastEnable").style.visibility = 'visible';
            document.getElementById("LastDisable").style.visibility = 'hidden';
        }

        function ContactEnable() {
            document.getElementById("ContentPlaceHolder1_txtProfileContactNumber").disabled = false;
            document.getElementById("ContactEnable").style.visibility = 'hidden';
            document.getElementById("ContactDisable").style.visibility = 'visible';
        }
        function ContactDisable() {
            document.getElementById("ContentPlaceHolder1_txtProfileContactNumber").disabled = true;
            document.getElementById("ContactEnable").style.visibility = 'visible';
            document.getElementById("ContactDisable").style.visibility = 'hidden';
        }
    </script>
</asp:Content>

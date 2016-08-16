<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminPages.Master" AutoEventWireup="true" CodeBehind="newslettermanagement.aspx.cs" Inherits="WFTCloud.Admin.newslettermanagement" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>NewsLetter Management</title>
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
       
        <asp:View ID="vwEmail" runat="server">
            <div class="row-fluid">
                <div class="table-header">
                            Newsletter Email Management	        
                        </div>
                <div>
                    <span class="error">User Content:</span><br />
                    <asp:TextBox ID="txtContent" ValidationGroup="EmailSend" runat="server" CssClass="span10"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="EmailSend"
                        ErrorMessage="*" CssClass="aster" ControlToValidate="txtHeader" ForeColor="red"></asp:RequiredFieldValidator>
                </div>
                <div>
                    <span class="error">Header:</span><br />
                    <asp:TextBox ID="txtHeader" ValidationGroup="EmailSend" runat="server" CssClass="span10"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvHeader" runat="server" ValidationGroup="EmailSend"
                        ErrorMessage="*" CssClass="aster" ControlToValidate="txtHeader" ForeColor="red"></asp:RequiredFieldValidator>
                </div>
                <div>
                    <span class="error">Subject:</span><br />
                    <asp:TextBox ID="txtSubject" ValidationGroup="EmailSend" runat="server" CssClass="span10"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvSubject" runat="server" ValidationGroup="EmailSend"
                        ErrorMessage="*" CssClass="aster" ControlToValidate="txtSubject" ForeColor="red"></asp:RequiredFieldValidator>
                </div>
                <div>
                    <span class="error">Message:</span>
                </div>
                <div>
                    <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" CssClass="span10" Rows="10" ValidationGroup="EmailSend"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvMessage" runat="server" ValidationGroup="EmailSend"
                        ErrorMessage="*" CssClass="aster" ControlToValidate="txtMessage" ForeColor="red"></asp:RequiredFieldValidator>
                </div>
                <div>
                    <asp:Button ID="btnSendEmail" runat="server" Text="Send Email" ValidationGroup="EmailSend" CssClass="btn btn-primary" OnClick="btnSendEmail_Click" />
                    <div id="divEmailSuccess" runat="server" visible="false" class="alert alert-block alert-success">
                        <button data-dismiss="alert" class="close" type="button">
                            <i class="icon-remove"></i>
                        </button>
                        <p>
                            <i class="icon-ok"></i>
                            Email sent successfully!!!
                        </p>
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

<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminPages.Master" AutoEventWireup="true" CodeBehind="UserSubscriptionDetails.aspx.cs" Inherits="WFTCloud.Admin.UserSubscriptionDetails" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>User Subscription History</title>
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
                                    <asp:Label ID="lblMail" runat="server" Text="Enter Subscription details"></asp:Label>
                                    <asp:TextBox ID="txtSearchSubscription" runat="server" class="customer-login-input" placeholder="Subscription details" AutoCompleteType="Disabled" autocomplete="off"></asp:TextBox>
                               
                                    <asp:Button ID="SearcButton" runat ="server" CssClass="btn btn-primary" Text ="Search" OnClick="SearchButton_Click" />
                                </div>  
                               
                                <br />
                                <div class="table-header">
                                    <asp:Label runat="server" ID="lblUserListTitle" Text="Subscriptions List"></asp:Label>                                    	        
                                </div>
                                  <div class="dataTables_wrapper">
                                    <div role="grid" class="dataTables_wrapper">
                                        <table id="tblUsersList" class="table table-striped table-bordered table-hover dataTable">
                                            <thead>
                                                <tr role="row">
                                                    <th role="columnheader">UserSubscriptionID</th>
                                                                        <th role="columnheader">User Name</th>
                                                                       <th role="columnheader">Service Name</th>
                                                                       <th role="columnheader">Category Name</th>
                                                                        <th role="columnheader">Amount</th>
                                                                         <th role="columnheader">Active Date</th>
                                                                         <th role="columnheader">Status</th>
                                                                        
                                                                        <th role="columnheader" style="text-align: center;">View</th>
                                                </tr>
                                            </thead>
                                            <tbody role="alert">
                                                  <asp:Repeater ID="rptrSubscriptionHistroy" runat="server">
                                                                        <%--OnItemCommand="rptrOrderHistroy_ItemCommand">--%>
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td><%# Eval("UserSubscriptionID") %></td>
                                                                                 <td><%# Eval("UserName") %></td>
                                                                            <td><%# Eval("ServiceName") %></td>
                                                                             <td><%# Eval("CategoryName") %></td>
                                                                                <td>$ <%# Eval("Amount")%></td>
                                                                                <td><%# Eval("ActiveDate","{0:dd-MMM-yyyy}")%></td>
                                                                                <td><%# ShowSubscribedServiceStatus(Eval("UserSubscriptionID").ToString()) %></td>
                                                                                
                                                                                <td style="text-align: center; vertical-align: middle;">
                                                                                  
                                                                                    <a data-rel="tooltip" title="View Subscription" href='UserSubscriptionHistory.aspx?UserSubscription=<%#Eval("UserSubscriptionID") %>' class="blue">
                                                                        <i class="icon-briefcase bigger-130"></i>
                                                                    </a>
                                                                                    <%--<asp:LinkButton data-rel="tooltip" title="View Details" runat="server" ID="lkbtnViewOrderDetails" CssClass="btn btn-primary btn-small" CommandName='<%#Eval("UserOrderID") %>' >
                                                                        <i class="icon-file-alt bigger-130"></i>
                                                                    </asp:LinkButton>--%>
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
  
   
</asp:Content>

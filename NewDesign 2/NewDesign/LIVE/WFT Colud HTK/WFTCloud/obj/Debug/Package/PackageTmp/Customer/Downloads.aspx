<%@ Page Title="" Language="C#" MasterPageFile="~/Customer/CustomerPages.Master" AutoEventWireup="true" CodeBehind="Downloads.aspx.cs" Inherits="WFTCloud.Customer.Downloads" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>WFT Cloud Resource</title>
<meta name="description" content="WFT Cloud Resource to display the download link of resources" />
<meta name="keywords" content="WFT Cloud Resource" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:MultiView ID="mvContainer" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwWFTCloudResource" runat="server">
    <div class="row-fluid">
                <div class="span12">
                        <div class="row-fluid">
                            <div class="table-header">
                                WFT Cloud Resources</div>
                        <div role="grid" class="dataTables_wrapper">
                            <div role="grid" class="dataTables_wrapper">
                            <table id="tblCloudResourceManagement" class="table table-striped table-bordered table-hover dataTable">
                                <thead>
                                    <tr role="row">
                                        <th role="columnheader" class="center">Resource Name</th>
                                        <th role="columnheader" class="center">Link</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rptrWFTCloudResource" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Eval("Title")%></td>
                                                <td><a href="<%# Eval("Path")%>" target="_blank">Download</a></td>
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
         <asp:View ID="vwNoWftResource" runat="server">
               <div id="divNoWftCloudResource" runat="server" class="alert alert-warning">
                    <button data-dismiss="alert" class="close" type="button">
                    <i class="icon-remove"></i>
                    </button>
                    <i class="icon-remove"></i>
                    <strong>
                        <asp:Label ID="lblNoWFT" runat="server" Text="There are no WFT Cloud Resources."></asp:Label>
                    </strong>
                </div>
             </asp:View>
         </asp:MultiView>
        <script type="text/javascript">
            jQuery(function ($) {
                $('#tblCloudResourceManagement').dataTable(
                    {
                        "aoColumnDefs": [
                            { 'bSortable': false, 'aTargets': [1] }
                        ]
                    }
             );
            });
   </script>
</asp:Content>

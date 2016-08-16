<%@ Page Title="" Language="C#" MasterPageFile="~/Customer/CustomerPages.Master" AutoEventWireup="true" CodeBehind="UserDashboard.aspx.cs" Inherits="WFTCloud.Customer.UserDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>User Dashboard</title>
    <meta name="description" content="we have an three tabs, they are Available services, Subscribed services and Manage CRM Issues. Each view shows the respective details" />
    <meta name="keywords" content="WFT Services and Manage CRM Issue" />
    <style type="text/css">
        .controls {
            margin-top: 0px;
        }

        .auto-style1 {
            width: 104px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="table-header">
        <strong>Dashboard </strong>
    </div>
    <div class="row-fluid">
        <div class="span12 widget-container-span">
            <div class="widget-box">
                <div class="widget-header no-border">
                </div>
                <div class="widget-body">
                    <div class="widget-main padding-12">
                        <div class="tab-content">


                            <div class="vspace-12-sm"></div>

                            <div class="col-sm-5">
                                <div class="widget-box">
                                    <div class="widget-header widget-header-flat widget-header-small">
                                        <h5 class="widget-title">
                                            <i class="ace-icon fa fa-signal"></i>
                                            Service Status
                                        </h5>


                                    </div>

                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <div id="piechart-placeholder"></div>

                                            <div class="hr hr8 hr-double"></div>

                                            <table width="100%">
                                                <tr>
                                                    <td>

                                                        <div class="col-sm-7 infobox-container">
                                                            <div class="infobox infobox-green">


                                                                <div class="infobox-data">
                                                                    <div class="infobox-content">Active Services</div>
                                                                    <span class="infobox-data-number">
                                                                        <asp:Label ID="lblActiveServices" runat="server"></asp:Label></span>

                                                                </div>


                                                            </div>



                                                            <div class="infobox infobox-red">
                                                                <div class="infobox-icon">
                                                                    <i class="ace-icon fa fa-flask"></i>
                                                                </div>

                                                                <div class="infobox-data">
                                                                    <div class="infobox-content">Cancelled Services</div>
                                                                    <span class="infobox-data-number">
                                                                        <asp:Label ID="lblCancelledServices" runat="server"></asp:Label></span>

                                                                </div>
                                                            </div>



                                                            <div class="space-6"></div>




                                                        </div>
                                                    </td>
                                                    <td><div class="col-xs-12 col-sm-3 widget-container-col" id="PaymentDiv" runat ="server">
										<div class="widget-box widget-color-dark light-border">
											<div class="widget-header">
												<h5 class="widget-title smaller">Payment Information</h5>

												<div class="widget-toolbar">
													<span class="badge badge-danger">Alert</span>
												</div>
											</div>

											<div class="widget-body">
												<div class="widget-main padding-6">
													<div class="alert alert-info">  <table class="table table-striped table-bordered table-hover dataTable" id="Table1">
                                            <thead>
                                                <tr role="row">
                                                    <th role="columnheader">UserSubscriptionID</th>
                                                    <th role="columnheader">Active Date</th>
                                                    <th role="columnheader">Payment Date</th>
                                                    <th role="columnheader">Payment Amount</th>
                                                </tr>
                                            </thead>
                                            <tbody role="alert">
                                                <asp:Repeater ID="rptrFailedPaymentHistoryDetails" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%# Eval("UserSubscriptionID") %></td>
                                                            <td><%# Eval("ActiveDate","{0:dd-MMM-yyyy}")%></td>
                                                            <td><%# Eval("PaymentDate","{0:dd-MMM-yyyy}")%></td>
                                                            <td><%# Eval("PaymentAmount") %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table></div>
												</div>
											</div>
										</div>
									</div></td>
                                                </tr>
                                            </table>
                                        </div>
                                        <!-- /.widget-main -->
                                    </div>
                                    <!-- /.widget-body -->

                                    <div class="col-sm-5">
                                        <div class="widget-header widget-header-flat widget-header-small">
                                            <h5 class="widget-title">
                                                <i class="ace-icon fa fa-signal"></i>
                                                Subscription History
                                            </h5>

                                            <div class="widget-toolbar no-border">
                                            </div>
                                        </div>
                                        <table class="table table-striped table-bordered table-hover dataTable" id="Table2">
                                            <thead>
                                                <tr role="row">
                                                    <th role="columnheader">Service Name</th>
                                                    <th role="columnheader">Release Version</th>
                                                    <th role="columnheader">Category Name</th>
                                                    <th role="columnheader">Total Service Purchased</th>
                                                </tr>
                                            </thead>
                                            <tbody role="alert">
                                                <asp:Repeater ID="rptrMonthlySubscriptionHistoryDetails" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%# Eval("ServiceName") %></td>
                                                            <td><%# Eval("ReleaseVersion") %></td>
                                                            <td><%# Eval("CategoryName") %></td>
                                                            <td><%# Eval("TotalService") %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                    <!-- /.col -->
                                </div>
                                <!-- /.widget-box -->
                            </div>
                            <!-- /.col -->

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

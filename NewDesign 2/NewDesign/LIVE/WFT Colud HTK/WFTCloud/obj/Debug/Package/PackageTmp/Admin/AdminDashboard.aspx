<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminPages.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="WFTCloud.Admin.AdminDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <title>Admin Dashboard</title>
    <meta name="Description" content="All email id's are handled here for email notifications" />
    <meta name="Keywords" content="Email ID" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row-fluid">
                <div class="span12">
                    <div class="row-fluid">
                        <%--<a href="Settings.aspx">&lt;&lt; Back&nbsp;</a>--%>
                        <br />
                        <div class="row-fluid">
                               
                                        <div id="divEmailError" runat="server" visible="false" class="alert alert-error span6"> 
				                        <button data-dismiss="alert" class="close" type="button">
					                        <i class="icon-remove"></i>
				                        </button>
                                        <i class="icon-remove"></i>
                                        <asp:Label ID="lblEmailError" runat="server"></asp:Label>
                                    </div>
                        </div>
                        <div class="table-header">
                            Admin Dashboard
                        </div>
                     
                        
                                  

									


                               
                                <div class="vspace-12-sm"></div>

									<div class="col-sm-5">
										<div class="widget-box">
											<div class="widget-header widget-header-flat widget-header-small">
												<h5 class="widget-title">
													<i class="ace-icon fa fa-signal"></i>
													Traffic Sources
												</h5>

												<div class="widget-toolbar no-border">
													<div class="inline dropdown-hover">
														<button class="btn btn-minier btn-primary">
															Recent Customer
															<i class="ace-icon fa fa-angle-down icon-on-right bigger-110"></i>
														</button>

													
													</div>
												</div>
											</div>

											<div class="widget-body">
												<div class="widget-main">
													<div id="piechart-placeholder"></div>

													<div class="hr hr8 hr-double"></div>

													<div class="clearfix">
														<div class="grid3">
															<span class="grey">
																<i class="ace-icon fa fa-facebook-square fa-2x blue"></i>
																&nbsp; Customer Name
															</span>
															<h4 class="bigger pull-right"><asp:Label ID="lblrecentCustomer" runat ="server"></asp:Label></h4>
														</div>

														<div class="grid3">
															<span class="grey">
																<i class="ace-icon fa fa-twitter-square fa-2x purple"></i>
																&nbsp; Service Name
															</span>
															<h4 class="bigger pull-right"><asp:Label ID="lblrecentService" runat ="server"></asp:Label></h4>
														</div>

														<div class="grid3">
															<span class="grey">
																<i class="ace-icon fa fa-pinterest-square fa-2x red"></i>
																&nbsp; Category
															</span>
															<h4 class="bigger pull-right"><asp:Label ID="lblrecentCategory" runat ="server"></asp:Label></h4>
														</div>
													</div>
												</div><!-- /.widget-main -->
											</div><!-- /.widget-body -->
										</div><!-- /.widget-box -->
									</div><!-- /.col -->
                        
                      
									

									
								

                        
									<div class="col-sm-5">
                                        <div class="widget-header widget-header-flat widget-header-small">
												<h5 class="widget-title">
													<i class="ace-icon fa fa-signal"></i>
													Current Month Sales
												</h5>

												<div class="widget-toolbar no-border">
													<div class="inline dropdown-hover">
														<button class="btn btn-minier btn-primary">
															Recent Sales
															<i class="ace-icon fa fa-angle-down icon-on-right bigger-110"></i>
														</button>

													
													</div>
												</div>
											</div>
 <table class="table table-striped table-bordered table-hover dataTable" id="Table2">
                                                <thead>
                                                    <tr role="row">
                                                        <th role="columnheader">Service Name</th>
                                                        <th role="columnheader">Release Version</th>
                                                        <th role="columnheader">Category Name</th>
                                                        <th role="columnheader">Total</th>
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
									</div><!-- /.col -->

									
								
                    </div>
                </div>
            </div>
        <script type="text/javascript">
            function ConfirmOnDelete() {
                var cb = document.getElementById('ContentPlaceHolder1_chkSiteDownForMaintenance');
                if (cb.checked) {
                    if (confirm("Are You Sure to down site for maintainance?") == true)
                        return true;
                    else
                        return false;
                }
            }
    </script>
</asp:Content>

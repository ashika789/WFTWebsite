<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BreadCrumb.ascx.cs" Inherits="WFTCloud.Admin.PageControls.BreadCrumb" %>
<div class="breadcrumbs" id="breadcrumbs">
<script type="text/javascript">
	try { ace.settings.check('breadcrumbs', 'fixed') } catch (e) { }
</script>

<ul class="breadcrumb">
    <li>
		<asp:Label runat="server" ID="lblUserName" Text="User Name" Font-Bold="true"></asp:Label>
			</li>
	<%--<li>
		<i class="icon-home home-icon"></i>
		<a href="#">Admin</a>

		<span class="divider">
			<i class="icon-angle-right arrow-icon"></i>
		</span>
	</li>
	<li class="active">Dashboard</li>--%>
</ul><!--.breadcrumb-->

</div>
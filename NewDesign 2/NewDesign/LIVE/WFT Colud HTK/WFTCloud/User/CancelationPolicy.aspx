<%@ Page Title="" Language="C#" MasterPageFile="~/FrontEnd.Master" AutoEventWireup="true" CodeBehind="CancelationPolicy.aspx.cs" Inherits="WFTCloud.User.CancelationPolicy" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Including main template styles -->
        <link rel="stylesheet" href="/css/main.css" />

    <script type="text/javascript">
        function ShowCancelPolicy() {
            document.getElementById("CancelId").focus();
        }
        </script>
  
        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="content">
    <div class="white-section">
    <div class="container row">
          
          <div class="span12">
                 <div class="breadcrumb">
                                    <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" />
                                    <asp:SiteMapPath ID="SiteMap1" runat="server"></asp:SiteMapPath>
                                </div>
              <asp:Literal ID="Literal1" runat="server"></asp:Literal>
              <h4>Cancellation &amp; Refunds</h4>
                      <p style="text-align:justify">
                                1: By beginning your subscription, you authorize WFT Cloud to charge you a monthly fee at the designated rate.<br /><br />

                                2: Your can manage your subscription through WFT Cloud website by logging to your account.  Your subscription will remain active unless you choose to terminate your service.  Any email requests or other forms of communication for cancellation will neither be honoured nor acknowledged as an acceptance of cancellation.<br /><br />

                                3: Your account will be charged on the day the order is processed and the next payment will deducted in 30days from the first order is processed <br /><br />
                                
                                4: WFT Cloud will not be held responsible for the cancellation of the services managed by customers.<br /><br />

                                5: WFT Cloud will not provide any <b>refund</b> if the customer fails to cancel their services through the WFT Cloud website before the next billing cycle.<br /><br />

                                6: There are no refunds for partial used periods. Following any cancellation; however, you will continue to have access to the service through the end of your current period. <br /><br />
                                
                                7: WFT has the rights to suspend your account any time incase of payment failure for the subscription </p>
           
          </div>
         
          <!-- span12 end -->
          
          </div>
         </div>
                </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerSEOContent" runat="server">
      <hr />
      <div class="row">
        <div class="span12">
              <h2 style="font-size:12px;">WFTCloud offers SAP cloud computing solutions and ERP solutions on cloud!</h2>
              	<p style="font-size:12px;">WFTCloud.com offers its services in <strong>SAP cloud computing solutions &ERP solution on cloud</strong> at an unmatched cost. Utilize WFT's expertise in <strong>SAP cloud computing solution & ERP solution on cloud</strong> for your business. Call Now!!!<p>
                <h2 style="font-size:12px;">Pay per use model for ERP software on cloud and ERP system on cloud.</h2>
                <p style="font-size:12px;">We focus on reducing your ERP implementation cost by introducing pay per use model for <strong>ERP software on cloud, ERP system on cloud & SAP on cloud support services</strong>. To know more about our pricing packages for <strong>ERP software on cloud, ERP system on cloud & SAP on cloud support services</strong>, Contact Us Now! <p>
                <h2 style="font-size:12px;">SAP certified provider of affordable & low cost SAP in the cloud solution.</h2>
                <p style="font-size:12px;">WFTCloud is a certified provider of <strong>cloud SAP in the cloud solution, ERP solution on cloud & SAP cloud computing solution</strong>. Get implementation of cloud <strong>SAP in the cloud solution, ERP solution on cloud & SAP cloud computing solution</strong> at a fraction of conventional cost.<p>

           <%--           	<p style="font-size:12px;"><strong>WFTCloud offers legal SAP online access for cheap SAP systems & solutions!</strong><br>
WFTCloud.com offers legal SAP online access for small and medium scale enterprises and relatively cheap SAP systems & solutions on cloud SAP software at an unmatched cost. Utilize WFT's expertise in legal SAP online access & relatively cheap SAP systems & solutions for your business. Call Now!!!<p>
                <p style="font-size:12px;"><strong>Pay per use model for affordable & low cost SAP system for small business.</strong><br>
We focus on reducing your ERP implementation cost by introducing pay per use model for cheap, affordable & low cost SAP system for small business. To know more about our pricing packages for Cloud SAP ERP, SAP on Cloud software services and cheap, affordable & low cost SAP system for small business, Contact Us Now! <p>
                <p style="font-size:12px;"><strong>SAP certified provider of low cost & affordable SAP system for small business to large enterprises.</strong><br>
WFTCloud is a certified provider of low cost & affordable SAP system for small business to large enterprises. Get implementation of low cost & affordable SAP system for small business to large enterprises within a fraction of conventional cost.<p>--%>

        </div>
      </div>
</asp:Content>

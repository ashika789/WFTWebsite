using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;
using WFTCloud.ResuableRoutines;
using WFTCloud.ResuableRoutines.SMTPManager;

namespace WFTCloud.Customer
{
    public partial class Failed : System.Web.UI.Page
    {
        #region Global Variables and Properties
        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        public string UserMembershipID;
        #endregion

        #region Page Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.Page.Title = "WFTCloud - Sorry! We are not able to process your payment successfully. Please try again.";
                if (!IsPostBack && !string.IsNullOrEmpty(Request.QueryString["unpaid"]) && !string.IsNullOrEmpty(Request.QueryString["userid"]) && !string.IsNullOrEmpty(Request.QueryString["InvoiceNumber"]))
                {
                    UserMembershipID = Request.QueryString["userid"];
                    string InvoiceNumber = Request.QueryString["InvoiceNumber"];
                    var OrderDetail = objDBContext.UserOrders.FirstOrDefault(a => a.InvoiceNumber == InvoiceNumber && a.OrderStatus == 999);
                    if (OrderDetail != null)
                    {
                        OrderDetail.OrderStatus = 0;
                        objDBContext.SaveChanges();
                        GetExpressCheckoutDetailsCommand obj = new GetExpressCheckoutDetailsCommand();
                        var trans1Response = obj.GetExpressCheckoutDetailsAPIOperation(Session["strToken"].ToString());
                        if(trans1Response != null)
                            Session["strPayerID"] = trans1Response.GetExpressCheckoutDetailsResponseDetails.PayerInfo.PayerID;
                    
                        UserPaymentTransaction NewUPT = new UserPaymentTransaction();
                        NewUPT.Amount = OrderDetail.OrderTotal;
                        NewUPT.Approved = false;
                        NewUPT.PaypalPayerMailID = trans1Response.GetExpressCheckoutDetailsResponseDetails.PayerInfo.Payer;
                        NewUPT.PaypalPayerID = trans1Response.GetExpressCheckoutDetailsResponseDetails.PayerInfo.PayerID;
                        NewUPT.PaymentMethod = "PayPal";
                        NewUPT.AuthAuthorizationCode = "";
                        NewUPT.AuthCardNumber = "";
                        NewUPT.AuthMessage = "";
                        NewUPT.AuthResponseCode = "";
                        NewUPT.AuthTransactionID = "";
                        OrderDetail.PaypalBillingAgreementID = "";
                        OrderDetail.PaypalPaymentTransactionID = "";
                        NewUPT.PaypalBillingAgreementID = "";
                        NewUPT.PalpalPaymentTransactionID = "";
                        
                        NewUPT.PaymentDateTime = DateTime.Now;
                        NewUPT.UserProfileID = OrderDetail.UserProfileID;
                        NewUPT.InvoiceNumber = Request.QueryString["InvoiceNumber"];
                        objDBContext.UserPaymentTransactions.AddObject(NewUPT);
                        objDBContext.SaveChanges();
                        DisplayOrderStatusDetail(OrderDetail, OrderDetail.InvoiceNumber);
                    }
                    else
                    {
                        Response.Redirect("/Customer/CloudPackages.aspx?userid=" + UserMembershipID + "&showview=ShowMyCart", false);
                    }
                }
                else
                {
                    Response.Redirect("/Loginpage.aspx", false);
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        #endregion

        #region Reusable Routine
        private void DisplayOrderStatusDetail(UserOrder OrderDetail, string InvoiceNumber)
        {
            var UserOrder = objDBContext.UserOrders.FirstOrDefault(uo => uo.UserOrderID == OrderDetail.UserOrderID);
            lblOrderCode.Text = "Order" + OrderDetail.UserOrderID.ToString();
            lblOrderDate.Text = Convert.ToDateTime(UserOrder.OrderDateTime).ToString("dd-MMM-yyyy").ToUpper();
            lblOrdeAmount.Text = "$ " + UserOrder.OrderTotal.ToString();
            lblCouponUsed.Text = UserOrder.IsCouponCode == null ? "Not Applied" : "Applied";
            if (lblCouponUsed.Text == "Applied")
            {
                trCouponCode.Visible = trDiscountedAmount.Visible = true;
                lblCouponCode.Text = UserOrder.IsCouponCode;
                trDiscountedAmount.Visible = UserOrder.IsDiscountValue == null ? false : true;
                lblDisCountAmount.Text = "$ " + UserOrder.IsDiscountValue.ToString();
            }
            else
            {
                trCouponCode.Visible = trDiscountedAmount.Visible = false;
            }
            rptrOrderDetails.DataSource = objDBContext.UserOrderDetails.Where(a => a.UserOrderID == OrderDetail.UserOrderID);
            rptrOrderDetails.DataBind();
            lblInvoiceNumber.Text = OrderDetail.InvoiceNumber;
            var PaymentDetails = objDBContext.UserPaymentTransactions.FirstOrDefault(pt => pt.InvoiceNumber == OrderDetail.InvoiceNumber);
            lblPaymentDate.Text = Convert.ToDateTime(PaymentDetails.PaymentDateTime).ToString("dd-MMM-yyyy").ToUpper();
            lblPaymentAmount.Text = "$ " + PaymentDetails.Amount.ToString();
            lblAmountPaid.Text = PaymentDetails.Approved == true ? "Paid" : "Not Paid";            
            var user = objDBContext.UserProfiles.FirstOrDefault(a => a.UserProfileID == UserOrder.UserProfileID);
            string UserFullName = user.FirstName + " " + user.MiddleName + " " + user.LastName;
                string ServiceDEtails = "<table style='width:100%; height:auto;'>" +
                "<tr><td style='width:49%;'>User Name</td><td style='width:2%;'>:</td><td style='width:49%;'>" + UserFullName+ "</td></tr>" +
                "<tr><td style='width:49%;'>User EmailID</td><td style='width:2%;'>:</td><td style='width:49%;'>" + user.EmailID + "</td></tr>" +
                "<tr><td style='width:49%;'>Order Number</td><td style='width:2%;'>:</td><td style='width:49%;'>" + lblOrderCode.Text + "</td></tr>" +
                "<tr><td style='width:49%;'>Order Date</td><td style='width:2%;'>:</td><td style='width:49%;'>" + lblOrderDate.Text + "</td></tr>" +
                "<tr><td style='width:49%;'>Amount</td><td style='width:2%;'>:</td><td style='width:49%;'>" + lblOrdeAmount.Text + "</td></tr>" +
                "<tr><td style='width:49%;'>Coupon Used</td><td style='width:2%;'>:</td><td style='width:49%;'>" + lblCouponUsed.Text + "</td></tr>" +
                "</table>" +
                "<h4 style='color:#0c87c7'>Order Details</h4>" +
                "<table style='width:100%; height:auto;'>" +
                "<tr><td><b>S.No</b></td><td><b>Category Name</b></td><td><b>Service Name</b></td><td><b>Quantity</b></td><td><b>Initial hold Amount</b></td></tr>";
                var mycarts = objDBContext.UserCarts.Where(A => A.UserProfileID == UserOrder.UserProfileID && A.RecordStatus == 999);
                int s = 0;
                foreach (var result in mycarts)
                {
                    s += 1;
                    ServiceDEtails += "<tr><td style='text-align:center;'>" + s + "</td><td>" + CategoryName(result.ServiceID.ToString()) + "</td><td>" + ServiceName(result.ServiceID.ToString()) + "</td><td  style='text-align:center;'>" + result.Quantity + "</td><td style='text-align:center;'> $ " +
                                     ServicePrice(result.ServiceID.ToString(), result.Quantity.ToString(), (result.SelectedDiscount == null ? "0" : result.SelectedDiscount.ToString())) + "</td></tr>";
                }
                ServiceDEtails += "</table>" +
                                  "<h4 style='color:#0c87c7'>Payment Transaction Details</h4>" +
                                  "<table style='width:100%; height:auto;'>" +
                                  "<tr><td style='width:49%;'>Invoice Number</td><td style='width:2%;'>:</td><td style='width:49%;'>" + lblInvoiceNumber.Text + "</td></tr>" +
                                  "<tr><td style='width:49%;'>Payment Date</td><td style='width:2%;'>:</td><td style='width:49%;'>" + lblPaymentDate.Text + "</td></tr>" +
                                  "<tr><td style='width:49%;'>Amount</td><td style='width:2%;'>:</td><td style='width:49%;'>" + lblPaymentAmount.Text + "</td></tr>" +
                                  "<tr><td style='width:49%;'>Mode</td><td style='width:2%;'>:</td><td style='width:49%;'>" + (PaymentDetails.PaymentMethod == "PayPal" ? "PayPal" : "Authorize.net") + "</td></tr>" +
                                  "</table>";


                string EmailContent = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["PaymentTransactionCancelledByCustomerInPaypal"]));
                EmailContent = EmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                EmailContent = EmailContent.Replace("++name++", " " + UserFullName);
                string AdminContent = EmailContent.Replace("++AddContentHere++", ServiceDEtails);
                SMTPManager.SendAdminNotificationEmail(AdminContent, "Payment Manually cancelled by Customer : " + UserFullName, false);

        }
        public string CategoryName(string SID)
        {
            int serviceID = Convert.ToInt32(SID);
            var services = objDBContext.ServiceCatalogs.FirstOrDefault(s => s.ServiceID == serviceID);
            var category = objDBContext.ServiceCategories.FirstOrDefault(d => d.ServiceCategoryID == services.ServiceCategoryID);
            return category.CategoryName;
        }

        public string ServiceName(string SID)
        {
            int serviceID = Convert.ToInt32(SID);
            var services = objDBContext.ServiceCatalogs.FirstOrDefault(s => s.ServiceID == serviceID);
            return services.ServiceName;
        }

        public string ServicePrice(string ServiceID, string Qty, string Discount)
        {
            int serviceID = Convert.ToInt32(ServiceID);
            int Quantity = Convert.ToInt32(Qty);
            var services = objDBContext.ServiceCatalogs.FirstOrDefault(s => s.ServiceID == serviceID);
            decimal DiscountPrice = 0;
            if (Discount == "0")
                DiscountPrice = 0;
            else if (Discount == "1")
                DiscountPrice = Convert.ToDecimal(services.ThreeMonthsSaving);
            else if (Discount == "2")
                DiscountPrice = Convert.ToDecimal(services.SixMonthsSaving);
            else if (Discount == "3")
                DiscountPrice = Convert.ToDecimal(services.NineMonthsSaving);
            else if (Discount == "4")
                DiscountPrice = Convert.ToDecimal(services.TwelveMonthsSaving);
            decimal ReducedPrice = ((DiscountPrice / 100) * Convert.ToDecimal(services.InitialHoldAmount)) * Quantity;
            decimal OriginalPrice = Convert.ToDecimal((services.InitialHoldAmount * Quantity) - ReducedPrice);
            return OriginalPrice.ToString("0.00");
        }
        #endregion
    }
}
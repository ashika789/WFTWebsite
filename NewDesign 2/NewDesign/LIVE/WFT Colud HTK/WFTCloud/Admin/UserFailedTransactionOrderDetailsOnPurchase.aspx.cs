using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;
using WFTCloud.ResuableRoutines;

namespace WFTCloud.Admin
{
    public partial class UserFailedTransactionOrderDetailsOnPurchase : System.Web.UI.Page
    {
        #region Global Variables and Properties
        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        public string UserMembershipID;

        #endregion

        #region Pageload

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txtEndDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                    txtStartDate.Text = DateTime.Now.AddDays(-30).ToString("dd-MMM-yyyy"); 
                    //Show records based on pagination value user order histroy
                    UserOrderHistroy();
                    // selected order details
                    ShowOrderDetails();
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name ), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }


        #endregion

        #region ControlEvents

        #endregion

        #region Resuable Routines
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


        private void UserOrderHistroy()
        {
            try
            {
                if (!Request.QueryString["UserOrder"].IsValid())
                {
                    DateTime FromDate = Convert.ToDateTime(txtStartDate.Text).Date;
                    DateTime ToDate = Convert.ToDateTime(txtEndDate.Text).Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                    var ohist = objDBContext.vwUserPaymentTransactionFailedOnPurchases.Where(a => a.OrderDateTime >= FromDate && a.OrderDateTime <= ToDate);
                    rptrOrderHistroy.DataSource = ohist;
                    rptrOrderHistroy.DataBind();
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        public string GetUserMailID(string UserProfileID)
        {
            int UsPid = Convert.ToInt32(UserProfileID);
            var objdusr = objDBContext.UserProfiles.First(A => A.UserProfileID == UsPid);
            return objdusr.EmailID;
        }
        public void ShowOrderDetails()
        {
            try
            {
                if (Request.QueryString["UserOrder"].IsValid())
                {

                    int UserOrderID = Convert.ToInt32(Request.QueryString["UserOrder"]);
                    var UserOrder = objDBContext.UserOrders.FirstOrDefault(uo => uo.UserOrderID == UserOrderID);
                    lblOrderCode.Text = "Order" + UserOrderID.ToString();
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
                    rptrOrderDetails.DataSource = objDBContext.UserOrderDetails.Where(a => a.UserOrderID == UserOrderID);
                    rptrOrderDetails.DataBind();
                    lblInvoiceNumber.Text = "WFT" + UserOrderID.ToString();
                    var PaymentDetails = objDBContext.UserPaymentTransactions.FirstOrDefault(pt => pt.InvoiceNumber == UserOrder.InvoiceNumber);
                    if (PaymentDetails != null)
                    {
                        lblPaymentDate.Text = Convert.ToDateTime(PaymentDetails.PaymentDateTime).ToString("dd-MMM-yyyy").ToUpper();
                        lblPaymentAmount.Text = "$ " + PaymentDetails.Amount.ToString();
                        lblPaymentMessage.Text = PaymentDetails.AuthMessage;
                        lblAmountPaid.Text = PaymentDetails.Approved == true ? "Paid" : "Not Paid";
                        lblCreditCardnumber.Text = PaymentDetails.AuthCardNumber;
                        lblMode.Text = PaymentDetails.AuthMessage == "PayPal" ? "PayPal" : "Authorize.net";
                        if (lblMode.Text == "PayPal")
                        {
                            if (PaymentDetails.Approved)
                            {
                                pnlPaypalShow.Visible = true;
                                lblPayPalPaymentId.Text = PaymentDetails.AuthAuthorizationCode;
                                lblPayPalPayerId.Text = PaymentDetails.AuthCardNumber;
                                lblPayPalSalesId.Text = PaymentDetails.AuthTransactionID;
                            }
                            else
                            {
                                pnlPaypalShow.Visible = false;
                            }
                            trAuthCard1.Visible = trAuthMessage1.Visible = false;
                        }
                        else
                        {
                            pnlPaypalShow.Visible = false;
                            trAuthCard1.Visible = trAuthMessage1.Visible = true;
                        }
                    }
                    else
                    {
                        lblPaymentDate.Text = lblPaymentAmount.Text = lblPaymentMessage.Text = lblAmountPaid.Text = lblCreditCardnumber.Text = "-";
                    }
                    mvOrderDetails.ActiveViewIndex = 1;
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        #endregion

        protected void btnGo_Click(object sender, EventArgs e)
        {
            UserOrderHistroy();
        }
    }
}
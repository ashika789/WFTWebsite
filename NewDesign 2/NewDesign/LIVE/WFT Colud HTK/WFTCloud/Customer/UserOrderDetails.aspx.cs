using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;
using WFTCloud.ResuableRoutines;

namespace WFTCloud.Customer
{
    public partial class UserOrderDetails : System.Web.UI.Page
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
                UserMembershipID = Request.QueryString["userid"];
                if (!IsPostBack && UserMembershipID!=null && UserMembershipID != "")
                {
                    //Show records based on pagination value user order histroy
                    UserOrderHistroy();
                    // selected order details
                    ShowOrderDetails();
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
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
        //public string UserSubscriptionID(string UserOrderID)
        //{
        //    int UserOrder = Convert.ToInt32(UserOrderID);
        //    var Subscription = objDBContext.UserSubscribedServices.FirstOrDefault(s => s.UserOrderID == UserOrder);
        //    return Subscription.UserSubscriptionID.ToString();
        //}
       

        private void UserOrderHistroy()
        {
            try
            {
                if (!Request.QueryString["UserOrder"].IsValid())
                {
                    string UserMembershipID = Request.QueryString["userid"];
                    Guid ID = new Guid(UserMembershipID);
                    var USer = objDBContext.UserProfiles.FirstOrDefault(s => s.UserMembershipID == ID);
                    var ohist = objDBContext.UserOrders.Where(oh => oh.UserProfileID == USer.UserProfileID && oh.OrderStatus != 999).OrderBy(id => id.UserOrderID).OrderByDescending(d => d.OrderDateTime);
                    rptrOrderHistroy.DataSource = ohist;
                    rptrOrderHistroy.DataBind();
                    if (ohist.Count() <= 0)
                    {
                        mvOrderDetails.ActiveViewIndex = 2;
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        public string GetOrderStatus(string UOid)
        {
            int UserOrderID = Convert.ToInt32(UOid);
            var UserOrder = objDBContext.UserOrders.FirstOrDefault(uo => uo.UserOrderID == UserOrderID);
            var PaymentDetails = objDBContext.UserPaymentTransactions.FirstOrDefault(pt => pt.InvoiceNumber == UserOrder.InvoiceNumber);
            string PayStatus = PaymentDetails.Approved == true ? "Paid" : "Not Paid";


            return PayStatus;


        }
        public void ShowOrderDetails()
        {
            try
            {
                string UserSubscriptionIDs = "";
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
                    var usersubscriptions = objDBContext.UserSubscribedServices.Where(A => A.UserOrderID == UserOrderID);
                    foreach (var res in usersubscriptions)
                    {
                        UserSubscriptionIDs += (res.UserSubscriptionID.ToString() + ",");
                    }
                    lblSubscriptionID.Text = UserSubscriptionIDs.Remove(UserSubscriptionIDs.LastIndexOf(","));
                    rptrOrderDetails.DataSource = objDBContext.UserOrderDetails.Where(a => a.UserOrderID == UserOrderID);
                    rptrOrderDetails.DataBind();
                    lblInvoiceNumber.Text = UserOrder.InvoiceNumber;
                    var PaymentDetails = objDBContext.UserPaymentTransactions.FirstOrDefault(pt => pt.InvoiceNumber == UserOrder.InvoiceNumber);
                    if (PaymentDetails != null)
                    {
                        lblPaymentDate.Text = Convert.ToDateTime(PaymentDetails.PaymentDateTime).ToString("dd-MMM-yyyy").ToUpper();
                        lblPaymentAmount.Text = "$ " + PaymentDetails.Amount.ToString();
                        lblPaymentMessage.Text = PaymentDetails.AuthMessage.IsValid()?PaymentDetails.AuthMessage:"-";
                        lblAmountPaid.Text = PaymentDetails.Approved == true ? "Paid" : "Not Paid";
                        lblCreditCardnumber.Text = PaymentDetails.AuthCardNumber.IsValid()?PaymentDetails.AuthCardNumber:"-";
                        lblMode.Text = PaymentDetails.PaymentMethod.IsValid()  ? PaymentDetails.PaymentMethod : "-";
                        if (lblMode.Text == "PayPal")
                        {
                            if (PaymentDetails.Approved)
                            {
                                pnlPaypalShow.Visible = true;
                                lblPayPalPaymentId.Text = PaymentDetails.PaypalBillingAgreementID.IsValid() ? PaymentDetails.PaypalBillingAgreementID : "-";
                                lblPayPalPayerId.Text = PaymentDetails.PaypalPayerID.IsValid() ? PaymentDetails.PaypalPayerID : "-";
                                lblPayPalSalesId.Text = PaymentDetails.PalpalPaymentTransactionID.IsValid() ? PaymentDetails.PalpalPaymentTransactionID : "-";
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
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        #endregion
    }
}
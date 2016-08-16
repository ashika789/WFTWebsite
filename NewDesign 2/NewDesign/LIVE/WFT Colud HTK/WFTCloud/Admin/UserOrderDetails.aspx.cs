using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;
using WFTCloud.ResuableRoutines;

namespace WFTCloud.Admin
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
                if (!IsPostBack )
                {
                    //Show records based on pagination value user order histroy
                    UserOrderHistroy();
                    
                    
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
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
                var ohist = objDBContext.UserOrders;
                rptrOrderHistroy.DataSource = ohist;
                rptrOrderHistroy.DataBind();
                if (ohist.Count() <= 0)
                {
                    mvOrderDetails.ActiveViewIndex = 2;
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        public string UserEmailID(string UserProfileID)
        {
            int userid = Convert.ToInt32(UserProfileID);
            var Users = objDBContext.UserProfiles.FirstOrDefault(u => u.UserProfileID == userid);
            return Users.EmailID;
        }
        #endregion

        protected void rptrOrderHistroy_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName != "")
                {
                    int UserOrderID = Convert.ToInt32(e.CommandName);
                    var UserOrder = objDBContext.UserOrders.FirstOrDefault(uo => uo.UserOrderID == UserOrderID);
                    lblUserEmailID.Text = UserEmailID(UserOrder.UserProfileID.ToString());
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
                    var PaymentDetails = objDBContext.UserPaymentTransactions.FirstOrDefault(pt => pt.InvoiceNumber == lblInvoiceNumber.Text);
                    lblPaymentDate.Text = Convert.ToDateTime(PaymentDetails.PaymentDateTime).ToString("dd-MMM-yyyy").ToUpper();
                    lblPaymentAmount.Text = "$ "+PaymentDetails.Amount.ToString();
                    lblPaymentMessage.Text = PaymentDetails.AuthMessage;
                    lblAmountPaid.Text = PaymentDetails.Approved == true ? "Paid" : "Not Paid";
                    lblCreditCardnumber.Text = PaymentDetails.AuthCardNumber;
                    mvOrderDetails.ActiveViewIndex = 1;
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void lkbtnBakcToOrderDetails_Click(object sender, EventArgs e)
        {
            mvOrderDetails.ActiveViewIndex = 0;
        }
    }
}
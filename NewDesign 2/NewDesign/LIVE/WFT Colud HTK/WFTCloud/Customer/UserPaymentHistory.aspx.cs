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
    public partial class UserPaymentHistory : System.Web.UI.Page
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
                    UserSubscriptionHistroy();
                    // selected order details
                    ShowSubscribedRecords();
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
      
        private void UserSubscriptionHistroy()
        {
            try
            {
                if (!Request.QueryString["UserOrder"].IsValid())
                {
                    string UserMembershipID = Request.QueryString["userid"];
                    Guid ID = new Guid(UserMembershipID);
                    var USer = objDBContext.UserSubscribedServices.FirstOrDefault(s => s.UserID == ID);
                    var ohist = objDBContext.UserSubscribedServices.Where(s => s.UserID == ID).OrderBy(id => id.UserSubscriptionID).OrderByDescending(d => d.ActiveDate);
                    rptrSubscriptionHistroy.DataSource = ohist;
                    rptrSubscriptionHistroy.DataBind();
                    if (ohist.Count() <= 0)
                    {
                        mvSubscriptionDetails.ActiveViewIndex = 2;
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        public string ShowSubscribedServiceStatus(string Usid)
        {
            int UsubId = Convert.ToInt32(Usid);
            var usdetails = objDBContext.UserSubscribedServices.FirstOrDefault(u => u.UserSubscriptionID == UsubId);

            if (usdetails.RecordStatus == 0)
                return "Expired";
            else if (usdetails.RecordStatus == -1)
                return "Cancelled";
            else
            {
                if (usdetails.ExpirationDate == null)
                {
                    return "Expired";
                }
                DateTime ExpDate = Convert.ToDateTime(usdetails.ExpirationDate);
                if (ExpDate < DateTime.Now)
                {
                    return "Expired";
                }
                if (usdetails.AutoProvisioningDone == true)
                    return "Active";
                else
                    return "Provision Pending";
            }

        }
        public string ShowPaymentMode(string Usid)
        {
            int UsubId = Convert.ToInt32(Usid);
           
            var USer = objDBContext.UserSubscribedServices.FirstOrDefault(s => s.UserSubscriptionID == UsubId);

            var UserOrder = objDBContext.UserOrders.FirstOrDefault(uo => uo.UserOrderID == USer.UserOrderID);
            String PayMode = string.Empty;
            PayMode = UserOrder.PaymentMethod == "PayPal" ? "PayPal" : "Authorize.Net";


            return PayMode;
           

        }
        public string GetCategoryName(string CID)
        {
            int CategoryID = Convert.ToInt32(CID);
            var services = objDBContext.ServiceCategories.FirstOrDefault(s => s.ServiceCategoryID == CategoryID);
            return services.CategoryName;
        }        
       
        private void ShowSubscribedRecords()
        {
            try
            {
                if (Request.QueryString["UserSubscription"].IsValid())
                {
                    int UserSubscriptionID = Convert.ToInt32(Request.QueryString["UserSubscription"]);
                    string UserMembershipID = Request.QueryString["userid"];
                    Guid ID = new Guid(UserMembershipID);
                    var USer = objDBContext.UserSubscribedServices.FirstOrDefault(s => s.UserSubscriptionID == UserSubscriptionID);

                    var UserOrder = objDBContext.UserOrders.FirstOrDefault(uo => uo.UserOrderID == USer.UserOrderID);
                    lblSubscriptionID.Text = UserSubscriptionID.ToString();
                    lblPaymentMode.Text = UserOrder.PaymentMethod =="PayPal" ? "PayPal" : "Authorize.Net";

                    if (UserOrder.PaymentMethod == "PayPal")
                    {
                        PayPalImage.Visible = true;
                        AuthorizeImage.Visible = false;
                    }
                    else
                    {
                        AuthorizeImage.Visible = true;
                        PayPalImage.Visible = false;
                    }
                        var PurchaseSubscribedService = objDBContext.vwGetPurchasePaymentHistories.Where(Data => Data.UserSubscriptionID == UserSubscriptionID);

                        if (PurchaseSubscribedService != null)
                        {
                            rptrPurchaseSubscriptionHistoryDetails.DataSource = PurchaseSubscribedService;
                            rptrPurchaseSubscriptionHistoryDetails.DataBind();
                        }
                       
                            
                            
                        
                        
                        var SubscribedService = objDBContext.vwGetPaymentHistories.Where(cat => cat.UsersubscriptionID == UserSubscriptionID);

                        if (SubscribedService.Count() > 0)
                        {
                            rptrSubscriptionHistoryDetails.DataSource = SubscribedService;
                            rptrSubscriptionHistoryDetails.DataBind();
                        }
                        else
                        {
                            divNoCrmIssue.Visible = true;
                        }
                        
                       

                            
                      
                    mvSubscriptionDetails.ActiveViewIndex = 1;
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void rptrPurchaseSubscriptionHistoryDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hidUserCartID = (HiddenField)e.Item.FindControl("hdnUserOrderID");
                HiddenField ServiceID = (HiddenField)e.Item.FindControl("hdnServiceID");
                Label lblPaymentAmount = (Label)e.Item.FindControl("lblPaymentAmount");
                int userOrderID = Convert.ToInt32(hidUserCartID.Value);
                int SerID = Convert.ToInt32(ServiceID.Value);
                var Amount = objDBContext.UserOrderDetails.FirstOrDefault(a => a.UserOrderID == userOrderID && a.ServiceID == SerID);
                lblPaymentAmount.Text = Amount.InitialHoldAmount.ToString();
               

            }
        }

        #endregion
    }
}
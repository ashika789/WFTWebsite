using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;
using WFTCloud.ResuableRoutines;
using WFTCloud.ResuableRoutines.SMTPManager;
using System.Web.Security;
using System.Security.Principal;

namespace WFTCloud.Admin
{
    public partial class UserSubscriptionHistory : System.Web.UI.Page
    {
        #region Global Variables and Properties
        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        #endregion

        #region Page Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
               
             
                 HttpCookie cookie = (HttpCookie)Request.Cookies[FormsAuthentication.FormsCookieName];
                 if (cookie != null)
                 {
                     string UserName = FormsAuthentication.Decrypt(cookie.Value).Name;
                     string Rolename = Roles.GetRolesForUser(UserName).First();
                     if (Rolename == DBKeys.Role_Administrator || Rolename == DBKeys.Role_SuperAdministrator)
                     {
                         if (!IsPostBack)
                         {
                             //Show records based on pagination value and deleted flag.
                            
                             ShowSubscribedRecords();
                         }
                     }
                     else
                     {
                         Response.Redirect("/LoginPage.aspx");
                     }
                 }
                 else
                 {
                     Response.Redirect("/LoginPage.aspx");
                 }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        #endregion

        #region ControlEvents
        protected void btnServiceActivation_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["UserSubscription"].IsValid())
                {
                    int UserSubscriptionID = Convert.ToInt32(Request.QueryString["UserSubscription"]);

                    var USer = objDBContext.UserSubscribedServices.FirstOrDefault(s => s.UserSubscriptionID == UserSubscriptionID);
                    var UnSubscribe = objDBContext.pr_ReActivateUserSubscription(UserSubscriptionID);
                }

                
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        #endregion

        #region Resuable Routines
       
       
        
        
        
      
       
        private void ShowSubscribedRecords()
        {
            try
            {
                if (Request.QueryString["UserSubscription"].IsValid())
                {
                    int UserSubscriptionID = Convert.ToInt32(Request.QueryString["UserSubscription"]);
                   
                    var USer = objDBContext.UserSubscribedServices.FirstOrDefault(s => s.UserSubscriptionID == UserSubscriptionID);

                    var UserOrder = objDBContext.UserOrders.FirstOrDefault(uo => uo.UserOrderID == USer.UserOrderID);
                    var User = objDBContext.UserProfiles.FirstOrDefault(u => u.UserProfileID == USer.UserProfileID);
                    lblSubscriptionID.Text = UserSubscriptionID.ToString();
                    lblPaymentMode.Text = UserOrder.PaymentMethod == "PayPal" ? "PayPal" : "Authorize.Net";
                    lblUserName.Text = User.LastName + " " + User.FirstName + " (" + User.EmailID + ")";
                    lblSAPUserName.Text = USer.ServiceUserName;
                    lblExpireDate.Text = Convert.ToDateTime(USer.ExpirationDate).ToString("dd-MMM-yy");
                    lblWorkLog.Text = USer.WorkLog;
                    if (USer.RecordStatus == 1)
                    {
                        btnServiceActivation.Visible = false;
                    }
                    if (UserOrder.PaymentMethod == "PayPal")
                    {
                        PurchaseAuthorizeDiv.Visible = false;
                        var PurchaseSubscribedService = objDBContext.vwGetPurchasePaymentHistories.Where(Data => Data.UserSubscriptionID == UserSubscriptionID);

                        if (PurchaseSubscribedService != null)
                        {
                            rptrPurchasePayPalSubscriptionHistoryDetails.DataSource = PurchaseSubscribedService;
                            rptrPurchasePayPalSubscriptionHistoryDetails.DataBind();
                        }
                        else
                        {
                            PurchasePayPalDiv.Visible = false;
                            PayPalImage.Visible = false;
                            divNoCrmIssue.Visible = true;
                        }

                        AuthorizeDiv.Visible = false;
                        AuthorizeImage.Visible = false;
                        var SubscribedService = objDBContext.vwGetPaymentHistories.Where(cat => cat.UsersubscriptionID == UserSubscriptionID);

                        if (SubscribedService != null)
                        {
                            rptrPayPalSubscriptionHistoryDetails.DataSource = SubscribedService;
                            rptrPayPalSubscriptionHistoryDetails.DataBind();
                        }
                        else
                        {
                            PayPalDiv.Visible = false;
                            PayPalImage.Visible = false;
                            divNoCrmIssue.Visible = true;
                        }


                    }
                    else
                    {
                        PurchasePayPalDiv.Visible = false;
                        var PurchaseSubscribedService = objDBContext.vwGetPurchasePaymentHistories.Where(Data => Data.UserSubscriptionID == UserSubscriptionID);
                        if (PurchaseSubscribedService.Count() > 0)
                        {
                            rptrPurchaseSubscriptionHistoryDetails.DataSource = PurchaseSubscribedService;
                            rptrPurchaseSubscriptionHistoryDetails.DataBind();
                        }
                        else
                        {
                            PurchaseAuthorizeDiv.Visible = false;
                            divNoCrmIssue.Visible = true;
                        }

                        PayPalDiv.Visible = false;
                        PayPalImage.Visible = false;
                        var SubscribedService = objDBContext.vwGetPaymentHistories.Where(cat => cat.UsersubscriptionID == UserSubscriptionID);
                        if (SubscribedService.Count() > 0)
                        {
                            rptrSubscriptionHistoryDetails.DataSource = SubscribedService;
                            rptrSubscriptionHistoryDetails.DataBind();
                        }
                        else
                        {
                            AuthorizeDiv.Visible = false;
                            divNoCrmIssue.Visible = true;
                        }
                    }
                    mvSubscriptionDetails.ActiveViewIndex = 1;
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
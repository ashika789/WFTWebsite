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
    public partial class UserSubscriptionDetails : System.Web.UI.Page
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
                            ShowPaginatedAndDeletedRecords();
                           
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
        private void ShowPaginatedAndDeletedRecords()
        {
            try
            {
                string UserSubscription = txtSearchSubscription.Text;
                var aptmts = objDBContext.pr_SearchUserSubscription(UserSubscription);
                rptrSubscriptionHistroy.DataSource = aptmts;
                rptrSubscriptionHistroy.DataBind();

                
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
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

     
        #endregion


        protected void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {

                string UserName = txtSearchSubscription.Text.Trim();
                var aptmts = objDBContext.pr_SearchUserSubscription(UserName);
                rptrSubscriptionHistroy.DataSource = aptmts;
                rptrSubscriptionHistroy.DataBind();
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now); throw;
            }
        }

    }
}
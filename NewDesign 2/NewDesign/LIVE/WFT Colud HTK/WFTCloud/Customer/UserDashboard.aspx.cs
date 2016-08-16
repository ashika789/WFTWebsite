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
    public partial class UserDashboard : System.Web.UI.Page
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
        

       
      
        private void UserSubscriptionHistroy()
        {
            try
            {
                string UserMembershipID = Request.QueryString["userid"];
                Guid ID = new Guid(UserMembershipID);
                var UserInfo=objDBContext.UserProfiles.FirstOrDefault(u => u.UserMembershipID == ID);
                var Active = objDBContext.UserSubscribedServices.Where(s => s.UserID == ID && s.RecordStatus==1);
                 var Cancelled = objDBContext.UserSubscribedServices.Where(s => s.UserID == ID && s.RecordStatus==-1);
                int ActiveServices = Active.Count();
                int CancelledServces = Cancelled.Count ();
                lblActiveServices.Text = ActiveServices.ToString ();
                lblCancelledServices.Text = CancelledServces.ToString();

                var MonthlySubscribedHsitory = objDBContext.vw_GetUserSubscriptionHistory.Where(a => a.UserProfileID == UserInfo.UserProfileID).OrderBy(a => a.CategoryName); ;
                if (MonthlySubscribedHsitory.Count() > 0)
                {
                    rptrMonthlySubscriptionHistoryDetails.DataSource = MonthlySubscribedHsitory;
                    rptrMonthlySubscriptionHistoryDetails.DataBind();
                }

                var FailedPaymentHsitory = objDBContext.vw_GetUserPaymentFailedHistory.Where(a => a.UserProfileID == UserInfo.UserProfileID).OrderBy(a => a.UserSubscriptionID); ;
                if (FailedPaymentHsitory.Count() > 0)
                {
                    rptrFailedPaymentHistoryDetails.DataSource = FailedPaymentHsitory;
                    rptrFailedPaymentHistoryDetails.DataBind();
                }
                else
                {
                    PaymentDiv.Visible = false;
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
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
using System.Text;
using System.IO;

namespace WFTCloud.Admin
{
    public partial class UserProfileMigration : System.Web.UI.Page
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

        #endregion

        #region Resuable Routines


       
        private void ShowSubscribedRecords()
        {
            try
            {
                if (Request.QueryString["viewuserprofileid"].IsValid())
                {
                    
                    string UserMembershipID = Request.QueryString["viewuserprofileid"];
                    Guid ID = new Guid(UserMembershipID);
                    var UserDetails = objDBContext.UserProfiles.FirstOrDefault(u => u.UserMembershipID == ID);
                    lblUserNamefor.Text = UserDetails.FirstName + " " + UserDetails.LastName;
                    lblUserName.Text = UserDetails.FirstName + " " + UserDetails.LastName + " " + UserDetails.MiddleName + "(" + UserDetails.EmailID + ")";
                    
                   
                    
                    mvSubscriptionDetails.ActiveViewIndex = 1;

                   

                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        #endregion

        protected void btnUpdateEmailAddress_Click(object sender, EventArgs e)
        {
            try
            {
                 string UserMembershipID = Request.QueryString["viewuserprofileid"];
                 Guid ID = new Guid(UserMembershipID);
                 var UserDetails = objDBContext.UserProfiles.FirstOrDefault(u => u.UserMembershipID == ID);
                 var ServicePurchased = objDBContext.pr_UpdateUserEmailID(UserDetails.UserProfileID,txtNewEmailAddress.Text.Trim());

                 divEmailSuccess.Visible = true;
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

      
       

        
        
    }
}
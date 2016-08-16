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
    public partial class UsersServiceHistory : System.Web.UI.Page
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
                             //Check if the screen should delete any userprofile from query string parameter.
                            
                            
                             
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

        #region Control Events

       

        protected void btnShowAllRecords_Click(object sender, EventArgs e)
        {
            ShowPaginatedAndDeletedRecords();
        }

       
      
        protected void showDeleted_CheckedChanged1(object sender, EventArgs e)
        {
            ShowPaginatedAndDeletedRecords();
        }


       

        
       

       

      
       

        
        #endregion

        #region Reusableroutines

       

       

       

       

        private UserProfile GetMembershipID(string usermembershipID)
        {
            Guid ID = new Guid(usermembershipID);
            return objDBContext.UserProfiles.FirstOrDefault(cat => cat.UserMembershipID == ID);
        }

        private void ShowPaginatedAndDeletedRecords()
        {
            try
            {
                string UserName = txtSearchUserName.Text;
                var aptmts = objDBContext.pr_SearchUserName(UserName);
                rptrUsersList.DataSource = aptmts;
                rptrUsersList.DataBind();
              
               
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        #endregion

       

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                
                string UserName = txtSearchUserName.Text.Trim();
                var aptmts = objDBContext.pr_SearchUserName(UserName);
                rptrUsersList.DataSource = aptmts;
                rptrUsersList.DataBind();
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now); throw;
            }
        }
        
    }
}
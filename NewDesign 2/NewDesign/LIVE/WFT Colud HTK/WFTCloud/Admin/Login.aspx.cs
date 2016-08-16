using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using WFTCloud.DataAccess;
using WFTCloud.ResuableRoutines;
namespace WFTCloud
{
    public partial class Login : System.Web.UI.Page
    {
        #region Global Variables and Properties
        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                txtUserName.Focus();
                if (!IsPostBack)
                {
                    HttpCookie cookie = (HttpCookie)Request.Cookies[FormsAuthentication.FormsCookieName];
                    if (cookie != null)
                    {
                        FormsAuthentication.SignOut();
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void lkbtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPassWord.Text != "" && txtUserName.Text != "")
                {
                    MembershipUser MSU = Membership.GetUser(txtUserName.Text);
                    if (MSU != null)
                    {
                        //string pass = MSU.GetPassword();
                        if (MSU.IsLockedOut)
                            MSU.UnlockUser();
                        var UserProfile = objDBContext.UserProfiles.FirstOrDefault(usr => usr.EmailID == txtUserName.Text && usr.RecordStatus == DBKeys.RecordStatus_Active);
                        if (Membership.ValidateUser(txtUserName.Text, txtPassWord.Text) && UserProfile != null)
                        {
                            string RoleName = Roles.GetRolesForUser(txtUserName.Text).First();
                            if (RoleName == DBKeys.Role_Administrator || RoleName == DBKeys.Role_SuperAdministrator)
                            {
                                FormsAuthentication.RedirectFromLoginPage(txtUserName.Text, true);
                                Response.Redirect("/admin/UserList.aspx", false);
                            }
                            else
                            {
                                divUserErrorMessage.Visible = true;
                            }
                        }
                        else
                        {
                            divUserErrorMessage.Visible = true;
                        }
                    }
                    else
                    {
                        divUserErrorMessage.Visible = true;
                    }
                }
                else
                {
                    divUserErrorMessage.Visible = true;
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        
    }
}
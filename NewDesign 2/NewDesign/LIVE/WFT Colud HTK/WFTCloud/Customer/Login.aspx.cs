using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using WFTCloud.ResuableRoutines;
using WFTCloud.DataAccess;
using WFTCloud.ResuableRoutines.SMTPManager;
namespace WFTCloud.Customer
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
                        Session.Clear();
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
                    if (MSU.IsLockedOut)
                        MSU.UnlockUser();
                    var UserProfile = objDBContext.UserProfiles.FirstOrDefault(usr => usr.EmailID == txtUserName.Text && usr.RecordStatus == DBKeys.RecordStatus_Active);
                    if (Membership.ValidateUser(txtUserName.Text, txtPassWord.Text) && UserProfile != null)
                    {
                        string RoleName = Roles.GetRolesForUser(txtUserName.Text).First();
                        if (RoleName.Contains(DBKeys.Role_BusinessUser) || RoleName.Contains(DBKeys.Role_EnterpriseUser) || RoleName.Contains(DBKeys.Role_PersonalUser))
                        {
                            FormsAuthentication.RedirectFromLoginPage(txtUserName.Text, true);
                            Response.Redirect("/Customer/UserProfiles.aspx?userid=" + MSU.ProviderUserKey + "&showview=UserDetails", false);
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

        protected void lbForgotPassword_Click(object sender, EventArgs e)
        {
            txtPassWord.Text = txtUserName.Text = string.Empty;
            mvLogin.ActiveViewIndex = 1;
        }

        protected void lbBackToLogin_Click(object sender, EventArgs e)
        {
            txtEmail.Text = string.Empty;
            mvLogin.ActiveViewIndex = 0;
            divEmailError.Visible = divEmailSuccess.Visible = false;
        }

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            try
            {
                MembershipUser MSU = Membership.GetUser(txtEmail.Text);
                if (MSU != null)
                {
                    string Password = MSU.GetPassword();
                    var UserProfile = objDBContext.UserProfiles.FirstOrDefault(usr => usr.EmailID == txtEmail.Text);
                    SMTPManager.SendEmail("Hi " + UserProfile.FirstName + ",\n\n     Your current EmailID and Password is\nEmail: " + txtEmail.Text + "\nPassword : " + Password + "\n\nRegards,\nWFT Cloud Team.", "Forgot Password", txtEmail.Text, false);
                    divEmailError.Visible = false;
                    divEmailSuccess.Visible = true;
                }
                else
                {
                    divEmailError.Visible = true;
                    divEmailSuccess.Visible = false;
                    lblEmailError.Text = "Email you enterd is not valid";
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
    }
}
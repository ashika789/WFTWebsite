using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;
using WFTCloud.ResuableRoutines;
using WFTCloud.ResuableRoutines.SMTPManager;

namespace WFTCloud 
{
    public partial class LoginPage : System.Web.UI.Page
    {
        #region Global Variables and Properties
        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        #endregion 

        #region PageEvents

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsLocal && Request.IsSecureConnection)
            {
                string redirectUrl = Request.Url.ToString().Replace("https:", "http:");
                Response.Redirect(redirectUrl, false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
                if (!IsPostBack)
                {
                    ShowLoginViewOnlyOnSiteDown();
                    try
                    {
                        clearCookie();
                        LoadTabView();
                    }
                    catch (Exception Ex)
                    {
                        ReusableRoutines.LogException(this.GetType().ToString(), "Page_Load", Ex.Message, Ex.StackTrace, DateTime.Now);
                    }
                }
        }

        private void clearCookie()
        {
            HttpCookie cookie = (HttpCookie)Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {

                Session.Clear();
                FormsAuthentication.SignOut();
                HttpContext.Current.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
            }
        }

        #endregion

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string Email = txtEmail.Text;
                string CurrentEmail = Email.Trim(' ');
                MembershipUser MSU = Membership.GetUser(CurrentEmail);
                if (MSU != null)
                {
                    if (MSU.IsLockedOut)
                        MSU.UnlockUser();
                    if (Membership.ValidateUser(CurrentEmail, txtPassword.Text))
                    {
                        //FormsAuthentication.SetAuthCookie(CurrentEmail, true);
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(CurrentEmail, false, 60);
                        string encTicket = FormsAuthentication.Encrypt(ticket);
                        Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                        Guid MuID = Guid.Parse(MSU.ProviderUserKey.ToString());
                        UserProfile upf = objDBContext.UserProfiles.FirstOrDefault(upd => upd.UserMembershipID == MuID);
                        string RoleName = Roles.GetRolesForUser(CurrentEmail).First();
                        if (RoleName == DBKeys.Role_Administrator || RoleName == DBKeys.Role_SuperAdministrator)
                        {
                                Response.Redirect("/Admin/UserList.aspx", false);
                        }
                        else
                        {
                            var setting = objDBContext.WftSettings.FirstOrDefault(st => st.SettingKey == "SITE_LOCKED" && st.SettingValue == "1");
                            if (setting == null)
                            {
                                if (upf.TempPasswordProvided == true)
                                    Response.Redirect("/Customer/ChangePassword.aspx?userid=" + MuID.ToString(), false);
                                else
                                    Response.Redirect("/Customer/CloudPackages.aspx?userid=" + MuID.ToString() + "&showview=SubscribedService&status=Active", false);
                            }
                            else
                            {
                                Response.Redirect("/SiteDownForMaintenance.html", false);
                            }
                        }
                    }
                    else
                    {
                        divEmailSuccess.Visible = false;
                        divEmailError.Visible = true;
                        lblEmailError.Text = "Invalid User Name or Password.";
                    }
                }
                else
                {
                    divEmailSuccess.Visible = false;
                    divEmailError.Visible = true;
                    lblEmailError.Text = "Invalid User Name or Password.";
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), "btnLogin_Click1", Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void LoadTabView()
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.ShowView].IsValid())
                {
                    string View = (Request.QueryString[QueryStringKeys.ShowView]);
                    if (View == "Register")
                    {
                        mvContainer.ActiveViewIndex = 1;
                    }
                    else if (View == "Login")
                    {
                        mvContainer.ActiveViewIndex = 0;
                    }
                    else if (View == "ForgotPassword")
                    {
                        mvContainer.ActiveViewIndex = 2;
                    }
                    divEmailSuccess.Visible = false;
                    divEmailError.Visible = false;
                    if (View == "Register")
                        ShowLoginViewOnlyOnSiteDown();
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), "LoadTabView", Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void ShowLoginViewOnlyOnSiteDown()
        {
            var setting = objDBContext.WftSettings.FirstOrDefault(st => st.SettingKey == "SITE_LOCKED" && st.SettingValue == "1");
            if (setting != null)
            {
                hlRegister.Visible = false;
                mvContainer.ActiveViewIndex = 0;
            }
        }

        protected void btnRegisterCode_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkAgree.Checked)
                {
                    var IsUserNameAlreadyExist = Membership.GetUser(txtEmailID.Text);
                    if (IsUserNameAlreadyExist == null)
                    {
                        MembershipUser mUser = Membership.CreateUser(txtEmailID.Text, txtRegPassword.Text,txtEmailID.Text);
                        mUser.IsApproved = true;
                        Membership.UpdateUser(mUser);
                        Roles.AddUserToRole(txtEmailID.Text, "Personal User");
                        UserProfile upf = new UserProfile();
                        upf.FirstName = txtFirstName.Text;
                        upf.MiddleName = txtMiddleName.Text;
                        upf.LastName = txtLastName.Text;
                        upf.UserMembershipID = Guid.Parse(mUser.ProviderUserKey.ToString());
                        upf.RecordStatus = DBKeys.RecordStatus_Active;
                        upf.EmailID = txtEmailID.Text;
                        upf.CreatedOn = DateTime.Now;
                        upf.UserRole = "Personal User";
                        upf.Location = ddlCountry.SelectedValue;
                        objDBContext.UserProfiles.AddObject(upf);

                        UserSurveyRespons objsr = new UserSurveyRespons();
                        objsr.CreatedBy = Guid.Parse(mUser.ProviderUserKey.ToString());
                        objsr.CreatedOn = DateTime.Now;
                        if (ddlHearAboutUs.SelectedValue == "Others")
                        {
                            objsr.SurveyAnswer = txtOthers.Text;
                        }
                        else
                        {
                            objsr.SurveyAnswer = ddlHearAboutUs.SelectedValue;
                        }
                        objsr.SurveyQuestionID = 1;
                        objDBContext.UserSurveyResponses.AddObject(objsr);

                        objDBContext.SaveChanges();
                        txtFirstName.Text = txtMiddleName.Text = txtLastName.Text = txtRegPassword.Text = txtEmailID.Text = txtConfPassword.Text = txtOthers.Text = string.Empty;
                        ddlCountry.SelectedValue = "0";
                        ddlHearAboutUs.SelectedValue = "Google";
                        divRegisterError.Visible = false;
                        lblRegisterSuccess.Text = "User registered successfully. Please click here to Login";
                        divRegisterSuccess.Visible = true;

                        //Add Item to Cart
                        if(Request.QueryString["AddToCart"].IsValid())
                        {
                            UserCart usrCart = new UserCart();
                            usrCart.UserProfileID = upf.UserProfileID;
                            usrCart.ServiceID = int.Parse(Request.QueryString["AddToCart"]);
                            usrCart.RecordStatus = 999;
                            usrCart.Quantity = 1;

                            objDBContext.UserCarts.AddObject(usrCart);
                            objDBContext.SaveChanges();
                        }
                    }
                    else
                    {
                        divRegisterError.Visible = true;
                        lblRegisterError.Text = "User Name already exists. Please register with an alternate email address.";
                        divRegisterSuccess.Visible = false;
                    }
                }
                else
                {
                    divRegisterError.Visible = true;
                    lblRegisterError.Text = "Please accept Terms and Conditions.";
                    divRegisterSuccess.Visible = false;
                    if (ddlHearAboutUs.SelectedValue == "Others")
                    {
                        txtOthers.Visible = true;
                        trOthers.Style.Add("display", "block");
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), "btnRegister_Click", Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

       
        protected void btnSendPassword_Click(object sender, EventArgs e)
        {
            try
            {
                MembershipUser MSU = Membership.GetUser(txtForgotEmail.Text);
                string Password =Membership.GeneratePassword(8, 2);
                if (MSU != null)
                {
                     MSU.ChangePassword(MSU.GetPassword(),Password);
                    var UserProfile = objDBContext.UserProfiles.FirstOrDefault(usr => usr.EmailID == txtForgotEmail.Text);
                    UserProfile.TempPasswordProvided = true;
                    objDBContext.SaveChanges();
                    string message = (("<strong></strong> " + UserProfile.FirstName +" "+ UserProfile.MiddleName +" "+ UserProfile.LastName));
                    string Username = (("<strong></strong> " + UserProfile.EmailID + "<br />"));
                    string tempPassword = (("<strong></strong> " + Password + "<br />"));
                    string EmailContent = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["ForGotPassword"]));//"/wftcloud/UploadedContents/EmailTemplates/new-password.html"));
                    //string EmailContent = objDBContext.EmailTemplates.FirstOrDefault(pass => pass.Reason == DBKeys.NewPassword).HTMLContent;
                    //EmailContent = WebUtility.HtmlDecode(EmailContent);
                    EmailContent = EmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                    string AdminContent = EmailContent.Replace("++name++", message).Replace ("++Username++", Username).Replace("++tempPassword++", tempPassword);
                     
                    // AdminContent = EmailContent.
                    //SMTPManager.SendAdminNotificationEmail(AdminContent, "Reset password request from " + UserProfile.FirstName + UserProfile.MiddleName + UserProfile.LastName, false);
                    //SMTPManager.SendEmail(" Hi" + UserProfile.FirstName + "," + AdminContent + " ", "Forgot Password", UserProfile.EmailID, false, false);
                    SMTPManager.SendEmail(AdminContent, "New Password from WFT Cloud Administrator",UserProfile.EmailID,false,true);
                    divForgotError.Visible = false;
                    divForgotSuccess.Visible = true;
                    lblForgotError.Text = "Email to reset your password sent successfully!!!";
                    btnSendPassword.Enabled = false;
                }
                else
                {
                    divForgotError.Visible = true;
                    divForgotSuccess.Visible = false;
                    lblForgotError.Text = "Please enter a valid email address to proceed.";
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), "btnSendPassword_Click", Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void ddlHearAboutUs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlHearAboutUs.SelectedValue == "Others")
            {
                trOthers.Visible = true;
            }
            else
            {
                trOthers.Visible = false;
            }
        }
    }
}
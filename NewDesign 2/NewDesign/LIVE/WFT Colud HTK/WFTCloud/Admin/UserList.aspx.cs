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
    public partial class UserList : System.Web.UI.Page
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
                             UpdateUserProfileStatus(QueryStringKeys.Delete, DBKeys.RecordStatus_Delete);
                             //Check if the screen should activate any category from query string parameter.
                             UpdateUserProfileStatus(QueryStringKeys.Activate, DBKeys.RecordStatus_Active);
                             //Check if the screen should deactivate any category from query string parameter.
                             UpdateUserProfileStatus(QueryStringKeys.Deactivate, DBKeys.RecordStatus_Inactive);
                             //Check if the screen should load view user profile from query string parameter.
                             LoadViewUserProfile();
                             //check for send email.
                             LoadSendEmail();
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

        protected void btnPasswordSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.ViewUserProfileID].IsValid())
                {
                    string UserMembershipID = (Request.QueryString[QueryStringKeys.ViewUserProfileID]);
                    Guid ID = new Guid(UserMembershipID);
                    string selUserEmailID = objDBContext.UserProfiles.FirstOrDefault(cat => cat.UserMembershipID == ID).EmailID;
                    MembershipUser MSU = Membership.GetUser(selUserEmailID);
                    string CurrentPassword = MSU.GetPassword();
                    if (Membership.ValidateUser(selUserEmailID, CurrentPassword))
                    {
                        MSU.ChangePassword(CurrentPassword, txtConfirmPassword.Text);
                        Membership.UpdateUser(MSU);
                        PasswordSuccessMessage.Visible = true;
                    }
                    else
                    {
                        PasswordErrorMessage.Visible = true;
                        lblPasswordErrorMessage.Text = "The user for whom you are trying to makes changes, is not valid or is Inactive.";
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void btnShowAllRecords_Click(object sender, EventArgs e)
        {
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            pnlButtonPassword.Visible = false;
            pnlPassword.Visible = true;
        }

        protected void btnPasswordCancel_Click(object sender, EventArgs e)
        {
            pnlButtonPassword.Visible = true;
            pnlPassword.Visible = lblPasswordErrorMessage.Visible = PasswordSuccessMessage.Visible = false;
            txtConfirmPassword.Text = txtNewPassword.Text = string.Empty;
        }

        protected void showDeleted_CheckedChanged1(object sender, EventArgs e)
        {
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnActivate_Click(object sender, EventArgs e)
        {
            DivUserListSuccess.Visible = false;
            ActivateDeactivateCategories(DBKeys.RecordStatus_Active);
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnDeactivate_Click(object sender, EventArgs e)
        {
            DivUserListSuccess.Visible = false;
            ActivateDeactivateCategories(DBKeys.RecordStatus_Inactive);
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            DivUserListSuccess.Visible = false;
            ActivateDeactivateCategories(DBKeys.RecordStatus_Delete);
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.SendEmail].IsValid())
                {
                    SMTPManager.SendEmail(txtMessage.Text, txtSubject.Text, txtEmailIDs.Text, false,false);
                    divEmailSuccess.Visible = true;
                }
                else
                {
                    SMTPManager.SendEmail(txtMessage.Text, txtSubject.Text, txtEmailIDs.Text, true,false);
                    divEmailSuccess.Visible = true;
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void btnNotifyEmail_Click(object sender, EventArgs e)
        {
            try
            {
                string emailids = string.Empty;

                foreach (RepeaterItem rptrItem in rptrUsersList.Items)
                {
                    //Retrieve the state of the CheckBox
                    CheckBox cbEdit = (CheckBox)rptrItem.FindControl("chkSelect");
                    Label ProjectMailID = (Label)rptrItem.FindControl("UserMailID");
                    if (cbEdit.Checked)
                    {
                        emailids += ProjectMailID.Text + ",";
                    }
                }

                if (emailids.EndsWith(","))
                    emailids = emailids.Substring(0, emailids.Length - 1);

                if (emailids.Length > 0)
                {
                    mvContainer.ActiveViewIndex = 2;
                    txtEmailIDs.Text = emailids;
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void btnAddNewUser_Click(object sender, EventArgs e)
        {
            mvContainer.ActiveViewIndex = 3;
            txtEmail1.Text = txtNewUserPassword1.Text = string.Empty;
        }

        protected void btnNewUserSave_Click(object sender, EventArgs e)
        {
            try
            {
                var IsUserNameAlreadyExist = Membership.GetUser(txtEmail1.Text);
                if (IsUserNameAlreadyExist == null)
                {
                    MembershipUser MSU = Membership.CreateUser(txtEmail1.Text, txtNewUserPassword1.Text, txtEmail1.Text);
                    MSU.IsApproved = true;
                    Membership.UpdateUser(MSU);
                    Roles.AddUserToRole(txtEmail1.Text, ddlUserRole.SelectedValue);
                    string userId = Convert.ToString(Membership.GetUser(txtEmail1.Text, false).ProviderUserKey);
                    Guid ID = new Guid(userId);
                    UserProfile UPD = new UserProfile();
                    UPD.UserMembershipID = ID;
                    UPD.FirstName = txtFirstName.Text;
                    UPD.MiddleName = txtMiddleName.Text;
                    UPD.LastName = txtLastName.Text;
                    UPD.UserRole = ddlUserRole.SelectedValue;
                    UPD.Location = ddlCountry.SelectedValue;
                    UPD.EmailID = txtEmail1.Text;
                    UPD.ContactNumber = txtContactNumber.Text;
                    UPD.PhoneNumber = txtPhoneNumber.Text;
                    UPD.MobileNumber = txtMobileNumber.Text;
                    UPD.MailingAddress = txtMailingAddress.Text;
                    UPD.CompanyName = txtCompanyName.Text;
                    UPD.CreatedOn = DateTime.Now;
                    UPD.RecordStatus = DBKeys.RecordStatus_Active;
                    objDBContext.UserProfiles.AddObject(UPD);
                    objDBContext.SaveChanges();
                    divNewUserSuccess.Visible = true;
                    divNewUserError.Visible = false;
                }
                else
                {
                    divNewUserSuccess.Visible = false;
                    divNewUserError.Visible = true;
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        //protected void chkShowDeleted_CheckedChanged(object sender, EventArgs e)
        //{
        //    ShowPaginatedAndDeletedRecords();
        //    DivUserListSuccess.Visible = false;
        //}

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtFirstName.Text = txtMiddleName.Text = txtLastName.Text = txtEmail1.Text = txtNewUserPassword1.Text = txtNewUserConfirmPassword.Text = string.Empty;
            ddlUserRole.SelectedValue = "Personal User";
            ddlCountry.SelectedValue = "0";
        }

        protected void lkbAccessAccount_Click(object sender, EventArgs e)
        {
            LinkButton lkbAddCart = ((LinkButton)(sender));
            RepeaterItem rItem = ((RepeaterItem)(lkbAddCart.NamingContainer));
            HiddenField hdnMembershipID = (rItem.FindControl("hdnMembershipID") as HiddenField);
            string UserMembershipID = (hdnMembershipID.Value);

            Guid ID = new Guid(UserMembershipID);

            HttpCookie cookie = (HttpCookie)Request.Cookies[FormsAuthentication.FormsCookieName];
            string UserName = FormsAuthentication.Decrypt(cookie.Value).Name;
            var UserProfiles = objDBContext.UserProfiles.FirstOrDefault(u => u.EmailID == UserName);

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('/Customer/CloudPackages.aspx?userid=" + UserMembershipID + "&showview=SubscribedService&status=Active', '_blank');", true);
        }

        protected void btnProfileSave_Click(object sender, EventArgs e)
        {
            try
            {
                string userId = Convert.ToString(Membership.GetUser(txtProfileEmailId.Text, false).ProviderUserKey);
                Guid ID = new Guid(userId);
                var UPD = objDBContext.UserProfiles.FirstOrDefault(u => u.UserMembershipID == ID);
                UPD.UserMembershipID = ID;
                UPD.FirstName = txtProfileFirstName.Text;
                UPD.MiddleName = txtProfileMiddleName.Text;
                UPD.LastName = txtProfileLastName.Text;
                UPD.UserRole = ddlProfileUserType.SelectedValue;
                UPD.Location = ddlProfileCountry.SelectedValue;
                UPD.EmailID = txtProfileEmailId.Text;
                UPD.ContactNumber = txtProfileContactNumber.Text;
                UPD.PhoneNumber = txtProfilePhoneNumber.Text;
                UPD.MobileNumber = txtProfileMobileNumber.Text;
                UPD.MailingAddress = txtProfileMailingAddress.Text;
                UPD.CompanyName = txtProfileCompanyName.Text;
                UPD.LastModifiedOn = DateTime.Now;

                var newUser = Membership.GetUser(UPD.UserMembershipID);
                string OldRole = Roles.GetRolesForUser(txtProfileEmailId.Text).First();
                if (OldRole != ddlProfileUserType.SelectedValue)
                {
                    Roles.RemoveUserFromRole(txtProfileEmailId.Text, OldRole);
                    Roles.AddUserToRole(txtProfileEmailId.Text, ddlProfileUserType.SelectedValue);
                }
                Membership.UpdateUser(newUser);

                objDBContext.SaveChanges();
                divProfileSuccess.Visible = true;
                PasswordSuccessMessage.Visible = PasswordErrorMessage.Visible = divProfileError.Visible = false;
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                divProfileError.Visible = true;
                PasswordSuccessMessage.Visible = PasswordErrorMessage.Visible = divProfileSuccess.Visible = false;
                lblProfileError.Text = "Error while updating user profile";
            }
        }

        //protected void chkShowAllUser_CheckedChanged(object sender, EventArgs e)
        //{
        //    if(chkShowAllUser.Checked)
        //        Response.Redirect("/Admin/UserList.aspx?ShowAll=yes", false);
        //    else
        //        Response.Redirect("/Admin/UserList.aspx", false);
        //}

        #endregion

        #region Reusableroutines

        private void LoadSendEmail()
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.SendEmail].IsValid())
                {
                    mvContainer.ActiveViewIndex = 2;
                    string UserMembershipID = (Request.QueryString[QueryStringKeys.SendEmail]);
                    Guid ID = new Guid(UserMembershipID);
                    string EmailID = objDBContext.UserProfiles.FirstOrDefault(em => em.UserMembershipID == ID).EmailID;
                    txtEmailIDs.Text = EmailID;
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void LoadViewUserProfile()
        {
            try
            {
               
                if (Request.QueryString[QueryStringKeys.ViewUserProfileID].IsValid())
                {
                    mvContainer.ActiveViewIndex = 1;
                    string UserMembershipID = (Request.QueryString[QueryStringKeys.ViewUserProfileID]);
                    Guid ID = new Guid(UserMembershipID);
                    var selUserList = objDBContext.UserProfiles.FirstOrDefault(cat => cat.UserMembershipID == ID);
                    var HearAboutUs = objDBContext.UserSurveyResponses.FirstOrDefault(us => us.CreatedBy == ID);
                    if (selUserList != null)
                    {
                        txtProfileEmailId.Text = selUserList.EmailID;
                        txtProfileFirstName.Text = selUserList.FirstName;
                        txtProfileLastName.Text = selUserList.LastName;
                        txtProfileMiddleName.Text = selUserList.MiddleName;
                        //lblUserType.Text = selUserList.UserRole;
                        ddlProfileUserType.SelectedValue = selUserList.UserRole;
                        ddlProfileCountry.SelectedValue = (selUserList.Location != null ? (selUserList.Location != "" ? selUserList.Location : "0") : "0");
                        //string strCountry = selUserList.Location;
                        //ListItem lst = ddlCountry.Items.FindByValue(strCountry);
                        //lblCountry.Text = strCountry != null ? (strCountry != "" ? strCountry : " - ") : " - ";
                        txtProfileContactNumber.Text = selUserList.ContactNumber;
                        txtProfilePhoneNumber.Text = selUserList.PhoneNumber;
                        txtProfileMobileNumber.Text = selUserList.MobileNumber;
                        txtProfileMailingAddress.Text = selUserList.MailingAddress;
                        txtProfileCompanyName.Text = selUserList.CompanyName;
                        if (HearAboutUs != null)
                            lblHearAboutUs.Text = HearAboutUs.SurveyAnswer != null ? (HearAboutUs.SurveyAnswer != "" ? HearAboutUs.SurveyAnswer : " - ") : " - ";
                        else
                            lblHearAboutUs.Text = " - ";
                    }
                    var UserSurvey = objDBContext.vwUserSurveyQuestionsResponses.OrderBy(obj => obj.SurveyQuestionID).Where(sur => sur.CreatedBy == ID && sur.RecordStatus != DBKeys.RecordStatus_Delete);
                    if (UserSurvey != null)
                    {
                        rptrUserSurvey.DataSource = UserSurvey;
                        rptrUserSurvey.DataBind();
                        //txtSurveyQuestion.Text = UserSurvey.SurveyQuestion;
                        //txtSurveyResponse.Text = UserSurvey.SurveyAnswer;
                        pnlNoUserSurvey.Visible = false;
                    }
                    else
                    {
                        pnlUserSurvey.Visible = false;
                        pnlNoUserSurvey.Visible = true;
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void ActivateDeactivateCategories(int categoryStatus)
        {
            try
            {
                foreach (RepeaterItem rItem in rptrUsersList.Items)
                {
                    CheckBox chkSelect = (rItem.FindControl("chkSelect") as CheckBox);
                    HiddenField hdnMembershipID = (rItem.FindControl("hdnMembershipID") as HiddenField);
                    if (chkSelect.Checked)
                    {
                        var selCategory = GetMembershipID(hdnMembershipID.Value);
                        if (selCategory.IsNotNull())
                        {
                            selCategory.RecordStatus = categoryStatus;
                            if (categoryStatus == 0 || categoryStatus == -1)
                            {
                                MembershipUser MSU = Membership.GetUser(selCategory.EmailID);
                                MSU.IsApproved = false;
                                Membership.UpdateUser(MSU);
                            }
                            if (categoryStatus == 1)
                            {
                                MembershipUser MSU = Membership.GetUser(selCategory.EmailID);
                                MSU.IsApproved = true;
                                Membership.UpdateUser(MSU);
                            }
                            DivUserListSuccess.Visible = true;
                        }
                    }
                }

                objDBContext.SaveChanges();
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void UpdateUserProfileStatus(string QueryStringKey, int RecordStatus)
        {
            try
            {
                //If redirected for delete then do delete
                if (Request.QueryString[QueryStringKey].IsValid())
                {
                    string usermembershipID = (Request.QueryString[QueryStringKey]);
                    var objToDelete = GetMembershipID(usermembershipID);
                    if (objToDelete.IsNotNull())
                    {
                        objToDelete.RecordStatus = RecordStatus;
                        objDBContext.SaveChanges();
                        if (RecordStatus == 0 || RecordStatus == -1)
                        {
                            MembershipUser MSU = Membership.GetUser(objToDelete.EmailID);
                            MSU.IsApproved = false;
                            Membership.UpdateUser(MSU);
                        }
                        if (RecordStatus == 1)
                        {
                            MembershipUser MSU = Membership.GetUser(objToDelete.EmailID);
                            MSU.IsApproved = true;
                            Membership.UpdateUser(MSU);
                        }
                        ShowPaginatedAndDeletedRecords();
                        DivUserListSuccess.Visible = true;
                    }
                    //Redirect to same page without query string
                    //Response.Redirect("/Admin/UserList.aspx", true);
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

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
              
                //if (Request.QueryString["ShowAll"].IsValid())
                //{
                //    chkShowAllUser.Checked = true;

                //    if (chkShowDeleted.Checked)
                //    {
                //        var DelUsersDetails = objDBContext.vwManageAllUserDetails.OrderByDescending(obj => obj.UserProfileID).Where(usr => usr.UserRole != DBKeys.Role_Administrator && usr.UserRole != DBKeys.Role_SuperAdministrator);
                //        //rptrUsersList.DataSource = objDBContext.UserProfiles.OrderByDescending(obj => obj.UserProfileID).Where(usr => usr.UserRole != DBKeys.Role_Administrator && usr.UserRole != DBKeys.Role_SuperAdministrator);
                //        rptrUsersList.DataSource = DelUsersDetails;
                //        rptrUsersList.DataBind();
                //    }
                //    else
                //    {
                //        var UsersDetails = objDBContext.vwManageAllUserDetails.OrderByDescending(obj => obj.UserProfileID).Where(usr => usr.UserRole != DBKeys.Role_Administrator && usr.UserRole != DBKeys.Role_SuperAdministrator && usr.RecordStatus == DBKeys.RecordStatus_Active);
                //        //rptrUsersList.DataSource = objDBContext.UserProfiles.OrderByDescending(obj => obj.UserProfileID).Where(usr => usr.UserRole != DBKeys.Role_Administrator && usr.UserRole != DBKeys.Role_SuperAdministrator && usr.RecordStatus == DBKeys.RecordStatus_Active);
                //        rptrUsersList.DataSource = UsersDetails;
                //        rptrUsersList.DataBind();
                //    }
                //}
                //else
                //{
                //    chkShowAllUser.Checked = false;
                //    if (chkShowDeleted.Checked)
                //    {
                //        var AllUsersDetails = objDBContext.vwManageAllUserDetails.OrderByDescending(obj => obj.UserProfileID).Where(usr => usr.UserRole != DBKeys.Role_Administrator && usr.UserRole != DBKeys.Role_SuperAdministrator).Take(100);
                //        //rptrUsersList.DataSource = objDBContext.UserProfiles.OrderByDescending(obj => obj.UserProfileID).Where(usr => usr.UserRole != DBKeys.Role_Administrator && usr.UserRole != DBKeys.Role_SuperAdministrator).Take(10);
                //        rptrUsersList.DataSource = AllUsersDetails;
                //        rptrUsersList.DataBind();
                //    }
                //    else
                //    {
                //        var UsersDetails = objDBContext.vwManageAllUserDetails.OrderByDescending(obj => obj.UserProfileID).Where(usr => usr.UserRole != DBKeys.Role_Administrator && usr.UserRole != DBKeys.Role_SuperAdministrator && usr.RecordStatus == DBKeys.RecordStatus_Active).Take(100);
                //        //rptrUsersList.DataSource = objDBContext.UserProfiles.OrderByDescending(obj => obj.UserProfileID).Where(usr => usr.UserRole != DBKeys.Role_Administrator && usr.UserRole != DBKeys.Role_SuperAdministrator && usr.RecordStatus == DBKeys.RecordStatus_Active).Take(10);
                //        rptrUsersList.DataSource = UsersDetails;
                //        rptrUsersList.DataBind();
                //    }
                //}
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
                DivUserListSuccess.Visible = false;
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
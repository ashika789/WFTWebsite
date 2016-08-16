using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;
using System.Web.Security;
using System.Configuration;
using WFTCloud.ResuableRoutines;

namespace WFTCloud.Admin
{
    public partial class AdministratorsList : System.Web.UI.Page
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

                        //Clear All Labels
                        ClearLabels();

                        if (!IsPostBack)
                        {
                            //Show appropriate view
                            ShowRequestedView();
                            //Check if the screen should load edit category from query string parameter.
                            LoadEditUser();
                            //Check if the screen should delete any category from query string parameter.
                            UpdateAdminStatus(QueryStringKeys.Delete, DBKeys.RecordStatus_Delete);
                            //Check if the screen should activate any category from query string parameter.
                            UpdateAdminStatus(QueryStringKeys.Activate, DBKeys.RecordStatus_Active);
                            //Check if the screen should deactivate any category from query string parameter.
                            UpdateAdminStatus(QueryStringKeys.Deactivate, DBKeys.RecordStatus_Inactive);
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

        private void ClearLabels()
        {
            lblErrorMessage.Visible = false;
            lblSuccessMessage.Visible = false;
            divCatErrorMessage.Visible = false;
            divCatSuccessMessage.Visible = false;
        }

        #endregion

        #region Control Events

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int userID = int.Parse(hdnUserProfileID.Value);
                var selUser = GetUserProfileFromID(userID);
                //If new category then add to dbcontext
                if (selUser == null)
                {
                    var IsUserNameAlreadyExist = Membership.GetUser(txtEmailID.Text);
                    if (IsUserNameAlreadyExist == null)
                    {
                        UserProfile objUserProfile = new UserProfile();
                        LoadDataFromFields(objUserProfile);
                        var newUser = Membership.CreateUser(objUserProfile.EmailID, ConfigurationManager.AppSettings[ConfigKeys.DefaultAdminPassword], objUserProfile.EmailID);
                        newUser.IsApproved = (objUserProfile.RecordStatus == DBKeys.RecordStatus_Active);
                        Roles.AddUserToRole(txtEmailID.Text, ddlUserRoles.SelectedValue);
                        Membership.UpdateUser(newUser);
                        objUserProfile.UserMembershipID = Guid.Parse(newUser.ProviderUserKey.ToString());
                        objUserProfile.CreatedOn = DateTime.Now;
                        objDBContext.UserProfiles.AddObject(objUserProfile);
                        objDBContext.SaveChanges();
                        hdnUserProfileID.Value = objUserProfile.UserProfileID.ToString();
                        lblSuccessMessage.Visible = true;
                        lblErrorMessage.Visible = false;
                    }
                    else
                    {
                        lblSuccessMessage.Visible = false;
                        lblErrorMessage.Visible = true;
                        lblErrorMessageText.Text = "User Name already exists. Please register with an alternate email address.";
                    }
                }
                else
                {
                    LoadDataFromFields(selUser);
                    selUser.LastModifiedOn = DateTime.Now;
                    var newUser = Membership.GetUser(selUser.UserMembershipID);
                    newUser.IsApproved = (selUser.RecordStatus == DBKeys.RecordStatus_Active);
                    string OldRole = Roles.GetRolesForUser(txtEmailID.Text).First();
                    if (OldRole != ddlUserRoles.SelectedValue)
                    {
                        Roles.RemoveUserFromRole(txtEmailID.Text, OldRole);
                        Roles.AddUserToRole(txtEmailID.Text, ddlUserRoles.SelectedValue);
                    }
                    Membership.UpdateUser(newUser);
                    objDBContext.SaveChanges();
                    lblSuccessMessage.Visible = true;
                    lblErrorMessage.Visible = false;
                }

                objDBContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, objDBContext.UserProfiles);
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                lblSuccessMessage.Visible = false;
                lblErrorMessage.Visible = true;
                lblErrorMessageText.Text = "Error occured while saving details.";
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                int userID = int.Parse(Request.QueryString[QueryStringKeys.EditUser]);
                if (userID != 0)
                {
                    LoadFieldsFromData(GetUserProfileFromID(int.Parse(hdnUserProfileID.Value)));
                }
                else
                {
                    txtFirstName.Text = txtMiddleName.Text = txtLastName.Text = txtEmailID.Text = txtContactNumber.Text = txtPhoneNumber.Text = txtMobileNumber.Text = txtMailingAddress.Text = txtCompanyName.Text = string.Empty;
                    ddlRecordStatus.SelectedValue = ddlUserRoles.SelectedValue = "1";
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdministratorsList.aspx?edituser=0");
        }

        protected void btnActivate_Click(object sender, EventArgs e)
        {
            divCatSuccessMessage.Visible = false;
            ActivateDeactivateAdmins(DBKeys.RecordStatus_Active);
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnDeactivate_Click(object sender, EventArgs e)
        {
            divCatSuccessMessage.Visible = false;
            ActivateDeactivateAdmins(DBKeys.RecordStatus_Inactive);
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            divCatSuccessMessage.Visible = false;
            ActivateDeactivateAdmins(DBKeys.RecordStatus_Delete);
            ShowPaginatedAndDeletedRecords();
        }

        protected void showDeleted_CheckedChanged(object sender, EventArgs e)
        {
            ShowPaginatedAndDeletedRecords();
        }

        protected void ddlNoOfRecords_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowPaginatedAndDeletedRecords();
        }

        private void LoadEditUser()
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.EditUser].IsValid())
                {
                    mvContainer.ActiveViewIndex = 1;
                    int userID = int.Parse(Request.QueryString[QueryStringKeys.EditUser]);
                    var selUser = objDBContext.UserProfiles.FirstOrDefault(cat => cat.UserProfileID == userID);
                    if (selUser != null)
                    {
                        LoadFieldsFromData(selUser);
                        lblAdminHeader.Text = "Edit Administrator Details";
                    }
                    else
                    {
                        lblAdminHeader.Text = "New Administrator Details";
                        hdnUserProfileID.Value = "0";
                        txtFirstName.Text = txtMiddleName.Text = txtLastName.Text = txtContactNumber.Text = txtPhoneNumber.Text = txtMobileNumber.Text = txtMailingAddress.Text = txtCompanyName.Text = string.Empty;
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        #endregion

        #region Resuable Routines

        private void ActivateDeactivateAdmins(int recStatus)
        {
            try
            {
                foreach (RepeaterItem rItem in rptrUserProfiles.Items)
                {
                    CheckBox chkSelect = (rItem.FindControl("chkSelect") as CheckBox);
                    HiddenField hdnCategoryID = (rItem.FindControl("hdnUserProfileID") as HiddenField);
                    if (chkSelect.Checked)
                    {
                        var selCategory = GetUserProfileFromID(int.Parse(hdnCategoryID.Value));
                        if (selCategory.IsNotNull())
                        {
                            selCategory.RecordStatus = recStatus;
                            if (recStatus == 0 || recStatus == -1)
                            {
                                MembershipUser MSU = Membership.GetUser(selCategory.EmailID);
                                MSU.IsApproved = false;
                                Membership.UpdateUser(MSU);
                            }
                            if (recStatus == 1)
                            {
                                MembershipUser MSU = Membership.GetUser(selCategory.EmailID);
                                MSU.IsApproved = true;
                                Membership.UpdateUser(MSU);
                            }
                            divCatSuccessMessage.Visible = true;
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

        private void UpdateAdminStatus(string QueryStringKey, int RecordStatus)
        {
            try
            {
                //If redirected for delete then do delete
                if (Request.QueryString[QueryStringKey].IsValid())
                {
                    int userID = int.Parse(Request.QueryString[QueryStringKey]);
                    var objToDelete = GetUserProfileFromID(userID);
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
                    }

                    divCatSuccessMessage.Visible = true;
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void LoadDataFromFields(UserProfile objUserProfile)
        {
            try
            {
                objUserProfile.FirstName = txtFirstName.Text;
                objUserProfile.MiddleName = txtMiddleName.Text;
                objUserProfile.LastName = txtLastName.Text;
                objUserProfile.EmailID = txtEmailID.Text;
                objUserProfile.Location = ddlCountry.SelectedValue;
                objUserProfile.UserRole = ddlUserRoles.SelectedValue.ToString();
                objUserProfile.RecordStatus = int.Parse(ddlRecordStatus.SelectedValue);
                objUserProfile.ContactNumber = txtContactNumber.Text;
                objUserProfile.PhoneNumber = txtPhoneNumber.Text;
                objUserProfile.MobileNumber = txtMobileNumber.Text;
                objUserProfile.MailingAddress = txtMailingAddress.Text;
                objUserProfile.CompanyName = txtCompanyName.Text;
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void LoadFieldsFromData(UserProfile objUserProfile)
        {
            try
            {
                hdnUserProfileID.Value = objUserProfile.UserProfileID.ToString();
                txtFirstName.Text = objUserProfile.FirstName;
                txtMiddleName.Text = objUserProfile.MiddleName;
                txtLastName.Text = objUserProfile.LastName;
                txtEmailID.Text = objUserProfile.EmailID;
                ddlCountry.SelectedValue = objUserProfile.Location != null ? (objUserProfile.Location != "" ? objUserProfile.Location : "0") : objUserProfile.Location = "0";
                ddlUserRoles.SelectedValue = objUserProfile.UserRole.ToLower();
                ddlRecordStatus.SelectedValue = objUserProfile.RecordStatus.ToString();

                txtContactNumber.Text = objUserProfile.ContactNumber;
                txtPhoneNumber.Text = objUserProfile.PhoneNumber;
                txtMobileNumber.Text = objUserProfile.MobileNumber;
                txtMailingAddress.Text = objUserProfile.MailingAddress;
                txtCompanyName.Text = objUserProfile.CompanyName;
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private UserProfile GetUserProfileFromID(int UserID)
        {
            return objDBContext.UserProfiles.FirstOrDefault(cat => cat.UserProfileID == UserID);
        }

        private void ShowPaginatedAndDeletedRecords()
        {
            try
            {
                if (chkShowDeleted.Checked)
                {
                    var adminList = objDBContext.UserProfiles.Where(obj => obj.UserRole == DBKeys.Role_Administrator || obj.UserRole == DBKeys.Role_SuperAdministrator).OrderBy(obj => obj.FirstName);
                    rptrUserProfiles.DataSource = adminList;
                    rptrUserProfiles.DataBind();
                }
                else
                {
                    var adminList = objDBContext.UserProfiles.Where(obj => obj.UserRole == DBKeys.Role_Administrator || obj.UserRole == DBKeys.Role_SuperAdministrator).Where(cat => cat.RecordStatus == DBKeys.RecordStatus_Active).OrderBy(obj => obj.FirstName);
                    rptrUserProfiles.DataSource = adminList;
                    rptrUserProfiles.DataBind();
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        #endregion

        protected void btnSavePassword_Click(object sender, EventArgs e)
        {
            try
            {
                int userProfileID = int.Parse(hdnCPUserProfileID.Value);
                var userProfile = objDBContext.UserProfiles.FirstOrDefault(usr => usr.UserProfileID == userProfileID);
                if (userProfile != null)
                {
                    var membershipUser = Membership.GetUser(userProfile.UserMembershipID);
                    membershipUser.ChangePassword(membershipUser.GetPassword(), txtPassword.Text);
                    Membership.UpdateUser(membershipUser);
                    divCPSucess.Visible = true;
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                divCPError.Visible = false;
            }
        }

        protected void btnSavePageAccess_Click(object sender, EventArgs e)
        {
            try
            {
                int userProfileID = int.Parse(hdnPAUserProfileID.Value);

                foreach (var objAccess in objDBContext.AdminPageAccesses.Where(obj => obj.AdminUserID == userProfileID))
                {
                    objDBContext.AdminPageAccesses.DeleteObject(objAccess);
                }
                objDBContext.SaveChanges();
                objDBContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, objDBContext.AdminPageAccesses);

                foreach (ListItem item in lbxWithAccess.Items)
                {
                    int pageID = int.Parse(item.Value);
                    objDBContext.AdminPageAccesses.AddObject(new AdminPageAccess() { AdminUserID = userProfileID, WebPageID = pageID });
                }
                objDBContext.SaveChanges();
                objDBContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, objDBContext.AdminPageAccesses);

                divPASuccess.Visible = true;
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void btnResetPageAccess_Click(object sender, EventArgs e)
        {
            LoadPagesForAccess();
        }

        private void ShowRequestedView()
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.ShowView].IsValid())
                {
                    switch (Request.QueryString[QueryStringKeys.ShowView])
                    {
                        case QueryStringKeys.PageAccess:
                            mvContainer.ActiveViewIndex = 3;
                            hdnPAUserProfileID.Value = Request.QueryString[QueryStringKeys.UserProfileID];
                            LoadPagesForAccess();
                            break;
                        case QueryStringKeys.ChangePassword:
                            mvContainer.ActiveViewIndex = 2;
                            hdnCPUserProfileID.Value = Request.QueryString[QueryStringKeys.UserProfileID];
                            LoadChangePasswordPanel();
                            break;
                        case QueryStringKeys.EditUser:
                            mvContainer.ActiveViewIndex = 1;
                            break;
                        default:
                            mvContainer.ActiveViewIndex = 0;
                            break;
                    }
                }
                else
                {
                    mvContainer.ActiveViewIndex = 0;
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void LoadChangePasswordPanel()
        {
            try
            {
                int userProfileID = int.Parse(hdnCPUserProfileID.Value);
                var userProfile = objDBContext.UserProfiles.FirstOrDefault(usr => usr.UserProfileID == userProfileID);
                if (userProfile != null)
                {
                    lblCPFirstName.Text = userProfile.FirstName;
                    lblCPLastName.Text = userProfile.LastName;
                    lblCPMiddleName.Text = userProfile.MiddleName != null ? (userProfile.MiddleName != "" ? userProfile.MiddleName : " - ") : " - ";
                    lblCPEmailID.Text = userProfile.EmailID;
                    lblContactNumber.Text = userProfile.ContactNumber != null ? (userProfile.ContactNumber != "" ? userProfile.ContactNumber : " - ") : " - ";
                    lblPhoneNumber.Text = userProfile.PhoneNumber != null ? (userProfile.PhoneNumber != "" ? userProfile.PhoneNumber : " - ") : " - ";
                    lblMobileNumber.Text = userProfile.MobileNumber != null ? (userProfile.MobileNumber != "" ? userProfile.MobileNumber : " - ") : " - ";
                    lblMailingAddress.Text = userProfile.MailingAddress != null ? (userProfile.MailingAddress != "" ? userProfile.MailingAddress : " - ") : " - ";
                    lblCompanyName.Text = userProfile.CompanyName != null ? (userProfile.CompanyName != "" ? userProfile.CompanyName : " - ") : " - ";
                    lblCountry.Text = userProfile.Location != null ? (userProfile.Location != "" ? userProfile.Location : " - ") : userProfile.Location = " - ";
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void LoadPagesForAccess()
        {
            try
            {
                int userProfileID = int.Parse(hdnPAUserProfileID.Value);
                var userProfile = objDBContext.UserProfiles.FirstOrDefault(usr => usr.UserProfileID == userProfileID);
                if (userProfile != null)
                {
                    lblPAEmailID.Text = userProfile.EmailID;
                    lblPAFirstName.Text = userProfile.FirstName;
                    lblPALastName.Text = userProfile.LastName;
                    lblPAMiddleName.Text = userProfile.MiddleName;

                    lbxToAccess.DataSource = objDBContext.pr_GetPagesForAdminAccessAssigment(userProfile.UserProfileID).OrderBy(obj => obj.WebPageName);
                    lbxToAccess.DataTextField = "WebPageName";
                    lbxToAccess.DataValueField = "WebPageID";
                    lbxToAccess.DataBind();

                    lbxWithAccess.DataSource = objDBContext.pr_GetPagesWithAdminAccess(userProfile.UserProfileID).OrderBy(obj => obj.WebPageName);
                    lbxWithAccess.DataTextField = "WebPageName";
                    lbxWithAccess.DataValueField = "WebPageID";
                    lbxWithAccess.DataBind();
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void btnAddToAccess_Click(object sender, EventArgs e)
        {
            try
            {
                List<ListItem> selItems = new List<ListItem>();

                foreach (ListItem item in lbxToAccess.Items)
                {
                    if (item.Selected)
                    {
                        lbxWithAccess.Items.Insert(0, new ListItem() { Text = item.Text, Value = item.Value });
                        selItems.Add(item);
                    }
                }

                foreach (ListItem item in selItems)
                {
                    lbxToAccess.Items.Remove(item);
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void btnRemoveAccess_Click(object sender, EventArgs e)
        {
            try
            {
                List<ListItem> selItems = new List<ListItem>();

                foreach (ListItem item in lbxWithAccess.Items)
                {
                    if (item.Selected)
                    {
                        lbxToAccess.Items.Insert(0, new ListItem() { Text = item.Text, Value = item.Value });
                        selItems.Add(item);
                    }
                }

                foreach (ListItem item in selItems)
                {
                    lbxWithAccess.Items.Remove(item);
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void chkShowDeleted_CheckedChanged(object sender, EventArgs e)
        {
            divCatSuccessMessage.Visible = false;
            ShowPaginatedAndDeletedRecords();
        }
    }
}
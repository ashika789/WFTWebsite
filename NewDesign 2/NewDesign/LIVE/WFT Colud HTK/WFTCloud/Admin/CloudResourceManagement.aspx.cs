using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;
using WFTCloud.ResuableRoutines;
using System.Web.Security;
using System.IO;

namespace WFTCloud.Admin
{
    public partial class CloudResourceManagement : System.Web.UI.Page
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
                            //Check if the screen should load edit Cloud Resource from query string parameter.
                            LoadEditWftCloud();
                            //Check if the screen should delete any Cloud Resource from query string parameter.
                            UpdateIndexDataStatus(QueryStringKeys.Delete, DBKeys.RecordStatus_Delete);
                            //Check if the screen should activate any Cloud Resource from query string parameter.
                            UpdateIndexDataStatus(QueryStringKeys.Activate, DBKeys.RecordStatus_Active);
                            //Check if the screen should deactivate any Cloud Resource from query string parameter.
                            UpdateIndexDataStatus(QueryStringKeys.Deactivate, DBKeys.RecordStatus_Inactive);
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

        protected void chkShowDeleted_CheckedChanged(object sender, EventArgs e)
        {
            ShowPaginatedAndDeletedRecords();
            divCloudSuccessMessage.Visible = false;
        }

        protected void btnActivate_Click(object sender, EventArgs e)
        {
            divCloudSuccessMessage.Visible = false;
            ActivateDeactivateWftCloud(DBKeys.RecordStatus_Active);
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnDeactivate_Click(object sender, EventArgs e)
        {
            divCloudSuccessMessage.Visible = false;
            ActivateDeactivateWftCloud(DBKeys.RecordStatus_Inactive);
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            divCloudSuccessMessage.Visible = false;
            ActivateDeactivateWftCloud(DBKeys.RecordStatus_Delete);
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnAddNewCloudResorce_Click(object sender, EventArgs e)
        {
            mvContainer.ActiveViewIndex = 2;
            LoadStatusAndUserType();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.EditWftCloudResource].IsValid())
                {
                    int ResourceID = int.Parse(Request.QueryString[QueryStringKeys.EditWftCloudResource]);
                    var selIndexData = objDBContext.WftCloudResources.FirstOrDefault(cat => cat.ResourceID == ResourceID);
                    if (selIndexData != null)
                    {
                        selIndexData.Title = txtTitle.Text;
                        selIndexData.Path = txtLink.Text;
                        selIndexData.RecordStatus = Convert.ToInt32(ddlStatus.SelectedValue);
                        selIndexData.UserType = Convert.ToInt32(ddlUserType.SelectedValue);
                        objDBContext.SaveChanges();
                        divEditCloudResourceError.Visible = false;
                        divEditCloudResourceManagement.Visible = true;
                    }
                    else
                    {
                        divEditCloudResourceError.Visible = true;
                        lblEditCloudResourceError.Text = "There are No WFT Cloud resource available.";
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                divEditCloudResourceError.Visible = true;
                lblEditCloudResourceError.Text = "Error while updating WFT Cloud Resource.";
            }
        }

        protected void btnNewSave_Click(object sender, EventArgs e)
        {
            try
            {
                WftCloudResource NewWFT = new WftCloudResource();
                NewWFT.Title = txtNewTitle.Text;
                NewWFT.Path = txtNewLink.Text;
                NewWFT.UserType = Convert.ToInt32(ddlNewUserType.SelectedValue);
                NewWFT.RecordStatus = Convert.ToInt32(ddlNewStatus.SelectedValue);
                NewWFT.CreatedOn = DateTime.Now;
                objDBContext.WftCloudResources.AddObject(NewWFT);
                objDBContext.SaveChanges();
                divNewCloudResourceError.Visible = false;
                divNewCloudResourceSuccess.Visible = true;
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                divNewCloudResourceSuccess.Visible = false;
                divNewCloudResourceError.Visible = true;
                lblNewCloudResourceError.Text = "Error while adding new WFT Cloud Resource";
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtNewTitle.Text = txtNewLink.Text = string.Empty;
        }

        #endregion

        #region ReusableRoutines

        private void LoadEditWftCloud()
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.EditWftCloudResource].IsValid())
                {
                    mvContainer.ActiveViewIndex = 1;
                    int ResourceID = int.Parse(Request.QueryString[QueryStringKeys.EditWftCloudResource]);
                    var selIndexData = objDBContext.WftCloudResources.FirstOrDefault(cat => cat.ResourceID == ResourceID);
                    if (selIndexData != null)
                    {
                        LoadStatusAndUserType();
                        txtTitle.Text = selIndexData.Title;
                        txtLink.Text = selIndexData.Path;
                        ddlStatus.SelectedValue = Convert.ToString(selIndexData.RecordStatus);
                        ddlUserType.SelectedValue = Convert.ToString(selIndexData.UserType);
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void LoadStatusAndUserType()
        {
            try
            {
                ddlStatus.DataSource = ddlNewStatus.DataSource = objDBContext.RecordStatus.OrderBy(a => a.RecordStatusDesc);
                ddlStatus.DataTextField = ddlNewStatus.DataTextField = "RecordStatusDesc";
                ddlStatus.DataValueField = ddlNewStatus.DataValueField = "RecordStatusID";
                ddlStatus.DataBind();
                ddlNewStatus.DataBind();
                ddlUserType.DataSource = ddlNewUserType.DataSource = objDBContext.WFTCloudUserTypes.OrderBy(usr => usr.WFTUserTypeID);
                ddlUserType.DataTextField = ddlNewUserType.DataTextField = "WFTUserTypeText";
                ddlUserType.DataValueField = ddlNewUserType.DataValueField = "WFTUserTypeID";
                ddlUserType.DataBind();
                ddlNewUserType.DataBind();
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void ActivateDeactivateWftCloud(int CloudResourceStatus)
        {
            try
            {
                foreach (RepeaterItem rItem in rptrCloudResourceManagement.Items)
                {
                    CheckBox chkSelect = (rItem.FindControl("chkSelect") as CheckBox);
                    HiddenField hdnWftCloudID = (rItem.FindControl("hdnWftCloudID") as HiddenField);
                    if (chkSelect.Checked)
                    {
                        var selWftCloud = GetCloudResourceFromID(int.Parse(hdnWftCloudID.Value));
                        if (selWftCloud.IsNotNull())
                        {
                            selWftCloud.RecordStatus = CloudResourceStatus;
                            divCloudSuccessMessage.Visible = true;
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

        private void UpdateIndexDataStatus(string QueryStringKey, int RecordStatus)
        {
            try
            {
                //If redirected for delete then do delete
                if (Request.QueryString[QueryStringKey].IsValid())
                {
                    int CloudResourceID = int.Parse(Request.QueryString[QueryStringKey]);
                    var objToDelete = GetCloudResourceFromID(CloudResourceID);
                    if (objToDelete.IsNotNull())
                    {
                        objToDelete.RecordStatus = RecordStatus;
                        objDBContext.SaveChanges();
                        ShowPaginatedAndDeletedRecords();
                    }
                    divCloudSuccessMessage.Visible = true;
                }
            }
            catch(Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private WftCloudResource GetCloudResourceFromID(int ResourceID)
        {
            return objDBContext.WftCloudResources.FirstOrDefault(cat => cat.ResourceID == ResourceID);
        }
    
        private void ShowPaginatedAndDeletedRecords()
        {
            try
            {
                if (chkShowDeleted.Checked)
                {
                    rptrCloudResourceManagement.DataSource = objDBContext.vwCloudResourceManagements.OrderByDescending(obj => obj.ResourceID);
                    rptrCloudResourceManagement.DataBind();
                }
                else
                {
                    rptrCloudResourceManagement.DataSource = objDBContext.vwCloudResourceManagements.OrderByDescending(obj => obj.ResourceID).Where(usr => usr.RecordStatus == DBKeys.RecordStatus_Active);
                    rptrCloudResourceManagement.DataBind();
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        #endregion

    }
}
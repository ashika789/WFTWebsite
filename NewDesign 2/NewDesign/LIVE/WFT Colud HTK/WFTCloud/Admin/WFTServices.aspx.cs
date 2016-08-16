using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;
using WFTCloud.ResuableRoutines;

namespace WFTCloud.Admin
{
    public partial class WFTServices : System.Web.UI.Page
    {        
        #region Global Variables and Properties

        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        string LoginEmailAddress = string.Empty;
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
                    string LoginEmailAddress = objDBContext.UserProfiles.FirstOrDefault(usr => usr.EmailID == UserName).EmailID;
                    if (Rolename == DBKeys.Role_Administrator || Rolename == DBKeys.Role_SuperAdministrator)
                    {
                        //Clear All Labels
                        ClearLabels();

                        if (!IsPostBack)
                        {
                            //Show appropriate view
                            ShowRequestedView();
                            //Check if the screen should delete any category from query string parameter.
                            UpdateAdminStatus(QueryStringKeys.Delete, DBKeys.RecordStatus_Delete);
                            //Check if the screen should activate any category from query string parameter.
                            UpdateAdminStatus(QueryStringKeys.Activate, DBKeys.RecordStatus_Active);
                            //Check if the screen should deactivate any category from query string parameter.
                            UpdateAdminStatus(QueryStringKeys.Deactivate, DBKeys.RecordStatus_Inactive);

                            //Show records
                            ShowPaginatedAndDeletedRecords();
                            //show tab view
                            LoadTabView();
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

        private void UpdateAdminStatus(string QueryStringKey, int RecordStatus)
        {
            try
            {
                //If redirected for delete then do delete
                if (Request.QueryString[QueryStringKey].IsValid())
                {
                    int serviceID = int.Parse(Request.QueryString[QueryStringKey]);
                    var objToDelete = GetServiceFromID(serviceID);
                    if (objToDelete.IsNotNull())
                    {
                        objToDelete.RecordStatus = RecordStatus;
                        objDBContext.SaveChanges();
                    }

                    divCatSuccessMessage.Visible = true;
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private ServiceCatalog GetServiceFromID(int serviceID)
        {
            return objDBContext.ServiceCatalogs.FirstOrDefault(obj => obj.ServiceID == serviceID);
        }

        private void ShowRequestedView()
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.ShowView].IsValid())
                {
                    switch (Request.QueryString[QueryStringKeys.ShowView])
                    {
                        case QueryStringKeys.EditService:
                            mvContainer.ActiveViewIndex = 1;
                            hdnServiceID.Value = Request.QueryString[QueryStringKeys.ServiceID];
                            LoadEditServiceView();
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

        private void LoadEditServiceView()
        {
            try
            {
                int ServiceID = int.Parse(hdnServiceID.Value);
                if (ServiceID > 0)
                {
                    LoadFieldsFromData(objDBContext.ServiceCatalogs.First(obj => obj.ServiceID == ServiceID));
                    lblHeader.Text = "Edit Service Details";
                }
                else
                {
                    ClearFormForNewEntry();
                    lblHeader.Text = "Service Details";
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void ClearFormForNewEntry()
        {
            try
            {
                hdnServiceID.Value = "0";
                txt12MonthsSaving.Text = txt3MonthsSaving.Text = txt3rdPartyService.Text = txt6MonthsSaving.Text = txt9MonthsSaving.Text = "0.00";
                txtInitialHoldAmount.Text = txt3rdPartyService.Text = "0.00"; //txtTrialAmount.Text = "0.00";
                txtPackageLength.Text = "0";
                txtSystemType.Text = txtReleaseVersion.Text = txtServiceDesc.Text = txtServiceName.Text = txtServicePriority.Text = string.Empty;
                txtWFTCloudCharge.Text = "0.00";

                ddlServiceCategory.DataSource = objDBContext.ServiceCategories.Where(ser => ser.RecordStatus == DBKeys.RecordStatus_Active);
                ddlServiceCategory.DataTextField = "CategoryName";
                ddlServiceCategory.DataValueField = "ServiceCategoryID";
                ddlServiceCategory.DataBind();

                int CategoryID = Convert.ToInt32(ddlServiceCategory.SelectedValue);
                var CategoryDetails = objDBContext.ServiceCategories.FirstOrDefault(cat => cat.ServiceCategoryID == CategoryID);

                if (CategoryDetails.IsPayAsYouGo == true)
                {
                    chkPayAsYouGo.Enabled = true;
                }
                else
                {
                    chkPayAsYouGo.Enabled = chkPayAsYouGo.Checked = false;
                    PnlPayAsYouGo.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void LoadFieldsFromData(ServiceCatalog serviceCatalog)
        {
            try
            {
                txt3rdPartyService.Text = (serviceCatalog.ThirdPartyServicePrice ?? 0.00M).ToString();
                txt3MonthsSaving.Text = (serviceCatalog.ThreeMonthsSaving ?? 0.00M).ToString();
                txt6MonthsSaving.Text = (serviceCatalog.SixMonthsSaving ?? 0.00M).ToString();
                txt9MonthsSaving.Text = (serviceCatalog.NineMonthsSaving ?? 0.00M).ToString();
                txt12MonthsSaving.Text = (serviceCatalog.TwelveMonthsSaving ?? 0.00M).ToString();

                txtInitialHoldAmount.Text = (serviceCatalog.InitialHoldAmount ?? 0.00M).ToString();
                txtPackageLength.Text = (serviceCatalog.PackageLengthInMonths ?? 0.00M).ToString();
                //txtTrialAmount.Text = (serviceCatalog.TrialAmount ?? 0.00M).ToString();
                txtReleaseVersion.Text = serviceCatalog.ReleaseVersion;
                txtServicePriority.Text = (serviceCatalog.Priority ?? 0).ToString();
                txtSystemType.Text = serviceCatalog.SystemType;
                txtWFTCloudCharge.Text = serviceCatalog.WFTCloudPrice.ToString();

                txtServiceName.Text = serviceCatalog.ServiceName;
                txtServiceDesc.Text = serviceCatalog.ServiceDescription;

                ddlServiceCategory.DataSource = objDBContext.ServiceCategories.Where(ser => ser.RecordStatus == DBKeys.RecordStatus_Active);
                ddlServiceCategory.DataTextField = "CategoryName";
                ddlServiceCategory.DataValueField = "ServiceCategoryID";
                ddlServiceCategory.DataBind();
    
                ddlServiceCategory.SelectedValue = serviceCatalog.ServiceCategoryID.ToString();
                ddlServiceCategory.Enabled = false;
                ddlUsageUnit.SelectedValue = serviceCatalog.UsageUnit;
                chkDisplayBlinking.Checked = serviceCatalog.DisplayBlinking ?? false;
                chkPayAsYouGo.Checked = serviceCatalog.IsPayAsYouGo ?? false;
                chkSupportIncluded.Checked = serviceCatalog.SupportIncluded ?? false;
                chkUserSpecific.Checked = serviceCatalog.UserSpecific ?? false;
                chkTrialService.Checked = serviceCatalog.IsTrial ?? false;
                int CatID = Convert.ToInt32(serviceCatalog.ServiceCategoryID);
                var CategoryDetails = objDBContext.ServiceCategories.FirstOrDefault(a => a.ServiceCategoryID == CatID);
                if (CategoryDetails.IsPayAsYouGo == true)
                {
                    if (chkPayAsYouGo.Checked == true)
                    {
                        PnlPayAsYouGo.Visible = true;
                        txtMinimumHours.Text = serviceCatalog.MinimumUsageInHours.ToString();
                        txtCostMinimumUsage.Text = serviceCatalog.CostPerMinUsage.ToString();
                        txtCostAdditionalHours.Text = serviceCatalog.CostPerAdditionalHours.ToString();
                    }
                    else
                    {
                        PnlPayAsYouGo.Visible = false;
                    }
                }
                else
                {
                    chkPayAsYouGo.Enabled = chkPayAsYouGo.Checked = false;
                    PnlPayAsYouGo.Visible = false;
                }
                if (chkUserSpecific.Checked == true)
                {
                    pnlUserSpecific.Visible = true;
                    LoadUserSpecific();
                    string ProfileID = Convert.ToString(serviceCatalog.UserProfileID);
                    if (ProfileID != null)
                    {
                        ddlUserSpecific.SelectedValue = ProfileID;
                    }
                }
                else
                {
                    pnlUserSpecific.Visible = false;
                }

                txtDisplayTextColor.Text = serviceCatalog.DisplayColor;

                string SupportEmail = objDBContext.WftSettings.FirstOrDefault(s => s.SettingsID == DBKeys.Support_Email).SettingValue;
                if(SupportEmail.Contains(LoginEmailAddress))
                {
                    txtInitialHoldAmount.Enabled = false;
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

        private void ShowPaginatedAndDeletedRecords()
        {
            try
            {
                if (chkStdShowDeleted.Checked)
                {
                    rptrStdWFTServices.DataSource = objDBContext.ServiceCatalogs.Where(obj => obj.UserSpecific == false);
                    rptrStdWFTServices.DataBind();
                }
                else
                {
                    rptrStdWFTServices.DataSource = objDBContext.ServiceCatalogs.Where(obj => obj.UserSpecific == false && obj.RecordStatus == DBKeys.RecordStatus_Active);
                    rptrStdWFTServices.DataBind();
                }

                if (chkCustomShowDeleted.Checked)
                {
                    rptrCustomServices.DataSource = objDBContext.ServiceCatalogs.Where(obj => obj.UserSpecific == true);
                    rptrCustomServices.DataBind();
                }
                else
                {
                    rptrCustomServices.DataSource = objDBContext.ServiceCatalogs.Where(obj => obj.UserSpecific == true && obj.RecordStatus == DBKeys.RecordStatus_Active);
                    rptrCustomServices.DataBind();
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        
        #endregion  

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int ServiceID = int.Parse(hdnServiceID.Value);
                if (ServiceID > 0)
                {
                    LoadDataFromFields(objDBContext.ServiceCatalogs.FirstOrDefault(obj => obj.ServiceID == ServiceID));
                }
                else
                {
                    ServiceCatalog objService = new ServiceCatalog();
                    LoadDataFromFields(objService);
                    objDBContext.ServiceCatalogs.AddObject(objService);
                }

                objDBContext.SaveChanges();
                objDBContext.Refresh(System.Data.Objects.RefreshMode.StoreWins, objDBContext.ServiceCatalogs);

                lblSuccessMessage.Visible = true;
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                lblErrorMessage.Visible = true;
            }
        }

        private void LoadDataFromFields(ServiceCatalog serviceCatalog)
        {
            try
            {
                serviceCatalog.InitialHoldAmount = decimal.Parse(txtInitialHoldAmount.Text);
                serviceCatalog.IsPayAsYouGo = chkPayAsYouGo.Checked;
                serviceCatalog.NineMonthsSaving = decimal.Parse(txt9MonthsSaving.Text);
                serviceCatalog.PackageLengthInMonths = int.Parse(txtPackageLength.Text);
                serviceCatalog.Priority = int.Parse(txtServicePriority.Text);
                serviceCatalog.RecordStatus = int.Parse(ddlRecordStatus.SelectedValue);
                serviceCatalog.ReleaseVersion = txtReleaseVersion.Text;
                serviceCatalog.ServiceCategoryID = int.Parse(ddlServiceCategory.SelectedValue);
                serviceCatalog.ServiceDescription = txtServiceDesc.Text;
                serviceCatalog.ServiceName = txtServiceName.Text;
                serviceCatalog.SixMonthsSaving = decimal.Parse(txt6MonthsSaving.Text);
                serviceCatalog.SupportIncluded = chkSupportIncluded.Checked;
                serviceCatalog.SystemType = txtSystemType.Text;
                serviceCatalog.ThirdPartyServicePrice = decimal.Parse(txt3rdPartyService.Text);
                serviceCatalog.ThreeMonthsSaving = decimal.Parse(txt3MonthsSaving.Text);
                //serviceCatalog.TrialAmount = decimal.Parse(txtTrialAmount.Text);
                serviceCatalog.TwelveMonthsSaving = decimal.Parse(txt12MonthsSaving.Text);
                serviceCatalog.UsageUnit = ddlUsageUnit.SelectedValue;
                serviceCatalog.UserSpecific = chkUserSpecific.Checked;
                serviceCatalog.WFTCloudPrice = decimal.Parse(txtWFTCloudCharge.Text);
                serviceCatalog.DisplayBlinking = chkDisplayBlinking.Checked;
                serviceCatalog.DisplayColor = txtDisplayTextColor.Text;
                serviceCatalog.IsTrial = chkTrialService.Checked;
                if (chkUserSpecific.Checked)
                {
                    serviceCatalog.UserProfileID = Convert.ToInt32(ddlUserSpecific.SelectedValue);
                }
                if (chkPayAsYouGo.Checked == true)
                {
                    serviceCatalog.MinimumUsageInHours = decimal.Parse(txtMinimumHours.Text);
                    serviceCatalog.CostPerMinUsage = decimal.Parse(txtCostMinimumUsage.Text);
                    serviceCatalog.CostPerAdditionalHours = decimal.Parse(txtCostAdditionalHours.Text);
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                int ServiceID = int.Parse(hdnServiceID.Value);
                if (ServiceID > 0)
                {
                    LoadFieldsFromData(objDBContext.ServiceCatalogs.First(obj => obj.ServiceID == ServiceID));
                }
                else
                {
                    ClearFormForNewEntry();
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
             

        protected void btnStdAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("WFTServices.aspx?showview=editservice&serviceid=0");
        }

        protected void btnStdActivate_Click(object sender, EventArgs e)
        {
            divWFTServicesSuccessMessage.Visible = false;
            ActivateDeactivateStdServices(DBKeys.RecordStatus_Active);
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnStdDeactivate_Click(object sender, EventArgs e)
        {
            divWFTServicesSuccessMessage.Visible = false;
            ActivateDeactivateStdServices(DBKeys.RecordStatus_Inactive);
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnStdDelete_Click(object sender, EventArgs e)
        {
            divWFTServicesSuccessMessage.Visible = false;
            ActivateDeactivateStdServices(DBKeys.RecordStatus_Delete);
            ShowPaginatedAndDeletedRecords();
        }

        private void ActivateDeactivateStdServices(int recStatus)
        {
            try
            {
                foreach (RepeaterItem rItem in rptrStdWFTServices.Items)
                {
                    CheckBox chkSelect = (rItem.FindControl("chkSelect") as CheckBox);
                    HiddenField hdnServiceID = (rItem.FindControl("hdnServiceID") as HiddenField);
                    if (chkSelect.Checked)
                    {
                        var selService = GetServiceFromID(int.Parse(hdnServiceID.Value));
                        if (selService.IsNotNull())
                        {
                            selService.RecordStatus = recStatus;
                            divWFTServicesSuccessMessage.Visible = true;
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

        private void ActivateDeactivateCustServices(int recStatus)
        {
            try
            {
                foreach (RepeaterItem rItem in rptrCustomServices.Items)
                {
                    CheckBox chkSelect = (rItem.FindControl("chkSelect") as CheckBox);
                    HiddenField hdnServiceID = (rItem.FindControl("hdnCustServiceID") as HiddenField);
                    if (chkSelect.Checked)
                    {
                        var selService = GetServiceFromID(int.Parse(hdnServiceID.Value));
                        if (selService.IsNotNull())
                        {
                            selService.RecordStatus = recStatus;
                            divCustomSuccess.Visible = true;
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

        private void ActivateDeactivateCustomServices(int recStatus)
        {
            try
            {
                foreach (RepeaterItem rItem in rptrCustomServices.Items)
                {
                    CheckBox chkSelect = (rItem.FindControl("chkSelect") as CheckBox);
                    HiddenField hdnServiceID = (rItem.FindControl("hdnServiceID") as HiddenField);
                    if (chkSelect.Checked)
                    {
                        var selService = GetServiceFromID(int.Parse(hdnServiceID.Value));
                        if (selService.IsNotNull())
                        {
                            selService.RecordStatus = recStatus;
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

        protected void btnAddNewCustom_Click(object sender, EventArgs e)
        {
            Response.Redirect("WFTServices.aspx?showview=editservice&serviceid=0");
        }

        protected void btnActivateCustom_Click(object sender, EventArgs e)
        {
            divCustomSuccess.Visible = false;
            ActivateDeactivateCustServices(DBKeys.RecordStatus_Active);
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnDeactivateCustom_Click(object sender, EventArgs e)
        {
            divCustomSuccess.Visible = false;
            ActivateDeactivateCustServices(DBKeys.RecordStatus_Inactive);
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnDeleteCustom_Click(object sender, EventArgs e)
        {
            divCustomSuccess.Visible = false;
            ActivateDeactivateCustServices(DBKeys.RecordStatus_Delete);
            ShowPaginatedAndDeletedRecords();
        }

        protected void chkStdShowDeleted_CheckedChanged(object sender, EventArgs e)
        {
            ShowPaginatedAndDeletedRecords();
        }

        protected void chkCustomShowDeleted_CheckedChanged(object sender, EventArgs e)
        {
            ShowPaginatedAndDeletedRecords();
        }

        protected void chkUserSpecific_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUserSpecific.Checked)
            {
                pnlUserSpecific.Visible = true;
                LoadUserSpecific();
            }
            else
            {
                pnlUserSpecific.Visible = false;
            }
        }
        private void LoadUserSpecific()
        {
            try
            {
                var UserNames = objDBContext.UserProfiles.OrderBy(usr => usr.EmailID).Where(ac => ac.RecordStatus == DBKeys.RecordStatus_Active && ac.UserRole != DBKeys.Role_Administrator && ac.UserRole != DBKeys.Role_SuperAdministrator);
                ddlUserSpecific.DataSource = UserNames;
                ddlUserSpecific.DataTextField = "EmailID";
                ddlUserSpecific.DataValueField = "UserProfileID";
                ddlUserSpecific.DataBind();
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void LoadTabView()
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.ShowView].IsValid())
                {
                    string View = (Request.QueryString[QueryStringKeys.ShowView]);
                    if (View == "standardpackages")
                    {
                        liStandardPackage.Attributes.Add("class", "active");
                        divStandardPackages.Attributes.Add("class", "tab-pane in active");
                        liCustomPackage.Attributes.Add("class", "");
                        divCustomPackages.Attributes.Add("class", "tab-pane");
                    }
                    else if (View == "custompackages")
                    {
                        liCustomPackage.Attributes.Add("class", "active");
                        divCustomPackages.Attributes.Add("class", "tab-pane in active");
                        liStandardPackage.Attributes.Add("class", "");
                        divStandardPackages.Attributes.Add("class", "tab-pane");
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void chkPayAsYouGo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPayAsYouGo.Checked)
            {
                PnlPayAsYouGo.Visible = true;
            }
            else
            {
                PnlPayAsYouGo.Visible = false;
            }
        }

        protected void ddlServiceCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int CategoryID = Convert.ToInt32(ddlServiceCategory.SelectedValue);
            var CategoryDetails = objDBContext.ServiceCategories.FirstOrDefault(cat => cat.ServiceCategoryID == CategoryID);

            if (CategoryDetails.IsPayAsYouGo == true)
            {
                chkPayAsYouGo.Enabled = true;
            }
            else
            {
                chkPayAsYouGo.Enabled = chkPayAsYouGo.Checked = false;
                PnlPayAsYouGo.Visible = false;
            }
        }
    }
}
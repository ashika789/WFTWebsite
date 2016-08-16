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
using System.Configuration;

namespace WFTCloud.Admin
{
    public partial class ServiceProvisioningDetails : System.Web.UI.Page
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
                            //Check if the screen should load edit Service Provision from query string parameter.
                            LoadEditServiceProvision();
                            //Check if the screen should delete any Service Provision from query string parameter.
                            UpdateServiceStatus(QueryStringKeys.Delete, DBKeys.RecordStatus_Delete);
                            //Check if the screen should activate any Service Provision from query string parameter.
                            UpdateServiceStatus(QueryStringKeys.Activate, DBKeys.RecordStatus_Active);
                            //Check if the screen should deactivate any Service Provision from query string parameter.
                            UpdateServiceStatus(QueryStringKeys.Deactivate, DBKeys.RecordStatus_Inactive);
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
            divSPDSuccessMessage.Visible = false;
        }

        protected void btnActivate_Click(object sender, EventArgs e)
        {
            divSPDSuccessMessage.Visible = false;
            ActivateDeactivateService(DBKeys.RecordStatus_Active);
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnDeactivate_Click(object sender, EventArgs e)
        {
            divSPDSuccessMessage.Visible = false;
            ActivateDeactivateService(DBKeys.RecordStatus_Inactive);
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            divSPDSuccessMessage.Visible = false;
            ActivateDeactivateService(DBKeys.RecordStatus_Delete);
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.EditServiceProvision].IsValid())
                {
                    int ServiceProvisionID = int.Parse(Request.QueryString[QueryStringKeys.EditServiceProvision]);
                    var selServiceProvision = objDBContext.ServiceProvisions.FirstOrDefault(cat => cat.ServiceProvisionID == ServiceProvisionID);
                    if (selServiceProvision != null)
                    {

                        if (fluAddAttachments.HasFile)
                        {
                            //string type = fluAddAttachments.FileName.Substring(fluAddAttachments.FileName.LastIndexOf('.') + 1).ToLower();
                            //if (type == "jpg" || type == "pdf")
                            //{
                                string EditPath = "";
                                string DeletePath = "";
                                EditPath = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/ServiceProvisions/" + ServiceProvisionID + "/");
                                DeletePath = ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/ServiceProvisions/" + ServiceProvisionID;
                                if (!Directory.Exists(EditPath))
                                {
                                    Directory.CreateDirectory(EditPath);
                                }
                                string Filename = EditPath + (fluAddAttachments.FileName.Contains(" ") ? fluAddAttachments.FileName.Replace(" ", "_") : fluAddAttachments.FileName);

                                if (File.Exists(Filename))
                                {
                                    File.Delete(Filename);
                                }
                                string RealFileName = (fluAddAttachments.FileName.Contains(" ") ? fluAddAttachments.FileName.Replace(" ", "_") : fluAddAttachments.FileName);
                                fluAddAttachments.SaveAs(EditPath +RealFileName);
                                selServiceProvision.AttachmentPath = "/UploadedContents/ServiceProvisions/" + ServiceProvisionID + "/"+RealFileName;
                                
                            //}
                            //else
                            //{
                            //    divServiceProvisionSuccess.Visible = false;
                            //    divServiceProvisionError.Visible = true;
                            //    lblServiceProvisionError.Text = "Only jpg and pdf formats are supported.";
                            //}
                        }

                        if (fluediImageAttachment.HasFile)
                        {
                            string EditPath = "";
                            string DeletePath = "";
                            EditPath = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/ServiceProvisions/" + ServiceProvisionID + "/");
                            DeletePath = ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/ServiceProvisions/" + ServiceProvisionID;
                            if (!Directory.Exists(EditPath))
                            {
                                Directory.CreateDirectory(EditPath);
                            }
                            string Filename = EditPath + (fluediImageAttachment.FileName.Contains(" ") ? fluediImageAttachment.FileName.Replace(" ", "_") : fluediImageAttachment.FileName);

                            if (File.Exists(Filename))
                            {
                                File.Delete(Filename);
                            }
                            string RealFileName=(fluediImageAttachment.FileName.Contains(" ") ? fluediImageAttachment.FileName.Replace(" ", "_") : fluediImageAttachment.FileName);
                            fluediImageAttachment.SaveAs(EditPath + RealFileName );
                            selServiceProvision.AttachmentImgPath = "/UploadedContents/ServiceProvisions/" + ServiceProvisionID + "/" + RealFileName;                         
                          }

                        //selServiceProvision.ServiceCategoryID = Convert.ToInt32(ddlChooseCategory.SelectedValue);
                        //selServiceProvision.ServiceID = Convert.ToInt32(ddlChooseService.SelectedValue);
                        //selServiceProvision.ExpirationPeriod = Convert.ToInt32(ddlExpirationPeriod.SelectedValue);
                        selServiceProvision.UserName = txtUserName.Text;
                        selServiceProvision.ServicePassword = txtServicePassword.Text;
                        selServiceProvision.ServiceUrl = txtServiceURL.Text;
                        selServiceProvision.InstanceNumber = txtInstanceNumber.Text;
                        selServiceProvision.ApplicationServer = txtApplicationServer.Text;
                        selServiceProvision.UIDOnServer = txtUIDOnServer.Text;
                        selServiceProvision.DeveloperKey = txtDeveloperKey.Text;
                        selServiceProvision.AdditionalInformation = txtOtherInormation.Text;
                        selServiceProvision.UserMax = Convert.ToInt32(txtUMax.Text);
                        selServiceProvision.UserMin = Convert.ToInt32(txtUMin.Text);
                        selServiceProvision.CurrentUserCounter = Convert.ToInt32(txtCurrentUserCounter.Text);
                        selServiceProvision.NotificationRequiredAt = Convert.ToInt32(ddlNotificaion.SelectedValue);
                        
                        objDBContext.SaveChanges();
                        divServiceProvisionError.Visible = false;
                        divServiceProvisionSuccess.Visible = true;
                    }
                }
                else
                {
                    divServiceProvisionError.Visible = true;
                    divServiceProvisionSuccess.Visible = false;
                    lblServiceProvisionError.Text = "Service Provision is not valid";
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                divServiceProvisionError.Visible = true;
                divServiceProvisionSuccess.Visible = false;
                lblServiceProvisionError.Text = "Error while updating Service Provision Details";
            }
        }

        protected void ddlChooseCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int SelectedCategory = Convert.ToInt32(ddlChooseCategory.SelectedValue);

                ddlChooseService.DataSource = objDBContext.ServiceCatalogs.OrderBy(ser => ser.ServiceID).Where(cat => cat.ServiceCategoryID == SelectedCategory);
                ddlChooseService.DataTextField = "ServiceName";
                ddlChooseService.DataValueField = "ServiceID";
                ddlChooseService.DataBind();
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void btnNewServiceProvision_Click(object sender, EventArgs e)
        {
            divNewError.Visible = divNewSuccess.Visible = false;
            hidServiceProvisionID.Value = "";
            mvContainer.ActiveViewIndex = 2;
            LoadNewServiceAndCategory();
            txtNewCurrentUserCount.Text = "0";
        }

        protected void ddlNewCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            divNewError.Visible = divNewSuccess.Visible = false;
            bindDataToServices();
                if (ddlNewService.Items.Count != 0)
                {
                    UpdateNewServiceProvision();
                }
                else
                {
                    divNewError.Visible = true;
                    lblNewError.Text = "All Services in this category has provision details.";
                    txtNewCurrentUserCount.Text = txtNewOtherInfo.Text = txtNewServicePassword.Text = txtNewServiceURL.Text = "";
                    txtNewUID.Text = txtNewUmax.Text = txtNewUmin.Text = txtNewUserName.Text = "";
                    ddlNewNotificationReqAt.SelectedValue = "0"; //ddlNewExpirationPeriod.SelectedValue = "0";
                    txtNewCurrentUserCount.Text = "0";
                }

        }

        private void bindDataToServices()
        {
            int Category = Convert.ToInt32(ddlNewCategory.SelectedValue);
            var servicesWithNoProvisions = objDBContext.pr_ServicewithnoServiceProvision();
            var SWNoPro = from snp in servicesWithNoProvisions
                          where snp.ServiceCategoryID == Category && snp.RecordStatus == DBKeys.RecordStatus_Active && snp.UserSpecific != true
                          select new
                          {
                              snp.ServiceName,
                              snp.ServiceID
                          };
            ddlNewService.DataSource = SWNoPro;
            ddlNewService.DataTextField = "ServiceName";
            ddlNewService.DataValueField = "ServiceID";
            ddlNewService.DataBind();
        }

        protected void btnNewSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlNewService.Items.Count != 0)
                {
                    int serviceid = Convert.ToInt32(ddlNewService.SelectedValue);
                    var selServiceProvision1 = objDBContext.ServiceProvisions.FirstOrDefault(cat => cat.ServiceID == serviceid);
                    if (selServiceProvision1 == null)
                    {

                        int ServiceProvisionCount = objDBContext.ServiceProvisions.Count() > 0 ? Convert.ToInt32(objDBContext.ServiceProvisions.Max(count => count.ServiceProvisionID)):0;
                        int ServiceProvisionID = ServiceProvisionCount + 1;

                        ServiceProvision selServiceProvision = new ServiceProvision();
                        if (fluNewAddAttachment.HasFile)
                        {
                            //string type = fluNewAddAttachment.FileName.Substring(fluNewAddAttachment.FileName.LastIndexOf('.') + 1).ToLower();
                            //if (type == "jpg" || type == "pdf")
                            //{
                            string EditPath = "";
                            string DeletePath = "";
                            EditPath = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/ServiceProvisions/" + ServiceProvisionID + "/");
                            DeletePath = ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/ServiceProvisions/" + ServiceProvisionID;

                            if (!Directory.Exists(EditPath))
                            {
                                Directory.CreateDirectory(EditPath);
                            }
                            string Filename = EditPath + (fluNewAddAttachment.FileName.Contains(" ") ? fluNewAddAttachment.FileName.Replace(" ", "_") : fluNewAddAttachment.FileName);

                            if (File.Exists(Filename))
                            {
                                File.Delete(Filename);
                            }
                            string RealFileName = (fluNewAddAttachment.FileName.Contains(" ") ? fluNewAddAttachment.FileName.Replace(" ", "_") : fluNewAddAttachment.FileName);
                            fluNewAddAttachment.SaveAs(EditPath + RealFileName);
                            selServiceProvision.AttachmentImgPath = "/UploadedContents/ServiceProvisions/" + ServiceProvisionID + "/" + RealFileName;
                            //}
                            //else
                            //{
                            //    divNewSuccess.Visible = false;
                            //    divNewError.Visible = true;
                            //    lblNewError.Text = "Only jpg and pdf formats are supported.";
                            //}
                        }
                        if (fluNewImageAttachment.HasFile)
                        {
                            string EditPath = "";
                            string DeletePath = "";
                            EditPath = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/ServiceProvisions/" + ServiceProvisionID + "/");
                            DeletePath = ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/ServiceProvisions/" + ServiceProvisionID;
                            if (!Directory.Exists(EditPath))
                            {
                                Directory.CreateDirectory(EditPath);
                            }
                            string Filename = EditPath + (fluNewImageAttachment.FileName.Contains(" ") ? fluNewImageAttachment.FileName.Replace(" ", "_") : fluNewImageAttachment.FileName);

                            if (File.Exists(Filename))
                            {
                                File.Delete(Filename);
                            }
                            string RealFileName = (fluNewImageAttachment.FileName.Contains(" ") ? fluNewImageAttachment.FileName.Replace(" ", "_") : fluNewImageAttachment.FileName);
                            fluNewImageAttachment.SaveAs(EditPath + RealFileName);
                            selServiceProvision.AttachmentImgPath = "/UploadedContents/ServiceProvisions/" + ServiceProvisionID + "/" + RealFileName;

                        }

                        selServiceProvision.ServiceCategoryID = Convert.ToInt32(ddlNewCategory.SelectedValue);
                        selServiceProvision.ServiceID = Convert.ToInt32(ddlNewService.SelectedValue);
                        selServiceProvision.UserName = txtNewUserName.Text;
                        selServiceProvision.ServicePassword = txtNewServicePassword.Text;
                        selServiceProvision.ServiceUrl = txtNewServiceURL.Text;
                        selServiceProvision.InstanceNumber = txtNewInstanceNumber.Text;
                        selServiceProvision.ApplicationServer = txtNewApplicationServer.Text;
                        selServiceProvision.UIDOnServer = txtNewUID.Text;
                        selServiceProvision.DeveloperKey = txtNewDeveloperKey.Text;
                        selServiceProvision.AdditionalInformation = txtNewOtherInfo.Text;
                        selServiceProvision.UserMax = Convert.ToInt32(txtNewUmax.Text);
                        selServiceProvision.UserMin = Convert.ToInt32(txtNewUmin.Text);
                        selServiceProvision.CurrentUserCounter = Convert.ToInt32(txtNewCurrentUserCount.Text);
                        selServiceProvision.NotificationRequiredAt = Convert.ToInt32(ddlNewNotificationReqAt.SelectedValue);
                        //selServiceProvision.ExpirationPeriod = Convert.ToInt32(ddlNewExpirationPeriod.SelectedValue);
                        selServiceProvision.RecordStatus = DBKeys.RecordStatus_Active;
                        objDBContext.ServiceProvisions.AddObject(selServiceProvision);
                        objDBContext.SaveChanges();

                        divNewError.Visible = false;
                        divNewSuccess.Visible = true;
                    }
                    else
                    {
                    LoadNewServiceAndCategory();
                    }

                }
                else
                {
                    divNewSuccess.Visible = false;
                    divNewError.Visible = true;
                    lblNewError.Text = "All Services in this category has provision details.";
                    txtNewCurrentUserCount.Text = txtNewOtherInfo.Text = txtNewServicePassword.Text = txtNewServiceURL.Text = "";
                    txtNewUID.Text = txtNewUmax.Text = txtNewUmin.Text = txtNewUserName.Text = "";
                    ddlNewNotificationReqAt.SelectedValue = "0";//ddlNewExpirationPeriod.SelectedValue = "0";
                    txtNewCurrentUserCount.Text = "0";
                }

            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                divNewError.Visible = true;
                divNewSuccess.Visible = false;
                lblNewError.Text = "Error while updating Service Provision Details";
            }
        }

        #endregion

        #region ReusableRoutines

        private void LoadEditServiceProvision()
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.EditServiceProvision].IsValid())
                {
                    mvContainer.ActiveViewIndex = 1;
                    int ServiceProvisionID = int.Parse(Request.QueryString[QueryStringKeys.EditServiceProvision]);
                    var selServiceProvision = objDBContext.ServiceProvisions.FirstOrDefault(cat => cat.ServiceProvisionID == ServiceProvisionID);
                    if (selServiceProvision != null)
                    {
                        
                        ddlChooseCategory.SelectedValue = Convert.ToString(selServiceProvision.ServiceCategoryID);
                        LoadServiceAndCategory();
                        ddlChooseService.SelectedValue = Convert.ToString(selServiceProvision.ServiceID);
                        txtUserName.Text = selServiceProvision.UserName;
                        txtServicePassword.Text = selServiceProvision.ServicePassword;
                        txtServiceURL.Text = selServiceProvision.ServiceUrl;
                        txtInstanceNumber.Text = selServiceProvision.InstanceNumber;
                        txtApplicationServer.Text = selServiceProvision.ApplicationServer;
                        txtUIDOnServer.Text = selServiceProvision.UIDOnServer;
                        txtDeveloperKey.Text = selServiceProvision.DeveloperKey;
                        txtOtherInormation.Text = selServiceProvision.AdditionalInformation;
                        txtUMax.Text = Convert.ToString(selServiceProvision.UserMax);
                        txtUMin.Text = Convert.ToString(selServiceProvision.UserMin);
                        txtCurrentUserCounter.Text = Convert.ToString(selServiceProvision.CurrentUserCounter);
                        ddlNotificaion.SelectedValue = Convert.ToString(selServiceProvision.NotificationRequiredAt);
                        //ddlExpirationPeriod.SelectedValue = Convert.ToString(selServiceProvision.ExpirationPeriod);

                        if (selServiceProvision.AttachmentPath != null && selServiceProvision.AttachmentPath != "")
                        {
                            hlAttachment.Visible = true;
                            hlAttachment.Text = "Click here to view attachment";
                            hlAttachment.NavigateUrl = selServiceProvision.AttachmentPath;
                        }
                        else
                        {
                            hlAttachment.Visible = false;
                        }
                        if (selServiceProvision.AttachmentImgPath != null && selServiceProvision.AttachmentImgPath != "")
                        {
                            hlImageAttachment.Visible = true;
                            hlImageAttachment.Text = "Click here to view image attachment";
                            hlImageAttachment.NavigateUrl = selServiceProvision.AttachmentImgPath;
                        }
                        else
                        {
                            hlImageAttachment.Visible = false;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void LoadServiceAndCategory()
        {
            try
            {
                ddlChooseCategory.DataSource = objDBContext.ServiceCategories.OrderBy(cat => cat.CategoryName);
                ddlChooseCategory.DataTextField = "CategoryName";
                ddlChooseCategory.DataValueField = "ServiceCategoryID";
                ddlChooseCategory.DataBind();
                int Category =Convert.ToInt32(ddlChooseCategory.SelectedValue);
                ddlChooseService.DataSource = objDBContext.ServiceCatalogs.OrderBy(ser => ser.ServiceName).Where(st => st.ServiceCategoryID == Category);
                ddlChooseService.DataTextField = "ServiceName";
                ddlChooseService.DataValueField = "ServiceID";
                ddlChooseService.DataBind();
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void LoadNewServiceAndCategory()
        {
            try
            {
                ddlNewCategory.DataSource = objDBContext.ServiceCategories.OrderBy(cat => cat.ServiceCategoryID).Where(st => st.RecordStatus != DBKeys.RecordStatus_Delete && st.RecordStatus != DBKeys.RecordStatus_Inactive);
                ddlNewCategory.DataTextField = "CategoryName";
                ddlNewCategory.DataValueField = "ServiceCategoryID";
                ddlNewCategory.DataBind();
                bindDataToServices();
                if (ddlNewService.Items.Count != 0)
                {
                    UpdateNewServiceProvision();
                }
                else
                {
                    divNewError.Visible = true;
                    lblNewError.Text = "All Services in this category has provision details.";
                    txtNewCurrentUserCount.Text = txtNewOtherInfo.Text = txtNewServicePassword.Text = txtNewServiceURL.Text = "";
                    txtNewUID.Text = txtNewUmax.Text = txtNewUmin.Text = txtNewUserName.Text = "";
                    ddlNewNotificationReqAt.SelectedValue = "0";//ddlNewExpirationPeriod.SelectedValue = "0";
                    txtNewCurrentUserCount.Text = "0";
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void ActivateDeactivateService(int ServiceProvisionStatus)
        {
            try
            {
                foreach (RepeaterItem rItem in rptrServiceProvision.Items)
                {
                    CheckBox chkSelect = (rItem.FindControl("chkSelect") as CheckBox);
                    HiddenField hdnServiceProvisionID = (rItem.FindControl("hdnServiceProvisionID") as HiddenField);
                    if (chkSelect.Checked)
                    {
                        var selWftCloud = GetServiceProvisionFromID(int.Parse(hdnServiceProvisionID.Value));
                        if (selWftCloud.IsNotNull())
                        {
                            selWftCloud.RecordStatus = ServiceProvisionStatus;
                            divSPDSuccessMessage.Visible = true;
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

        private void UpdateServiceStatus(string QueryStringKey, int RecordStatus)
        {
            try
            {
                //If redirected for delete then do delete
                if (Request.QueryString[QueryStringKey].IsValid())
                {
                    int ServiceProvisionID = int.Parse(Request.QueryString[QueryStringKey]);
                    var objToDelete = GetServiceProvisionFromID(ServiceProvisionID);
                    if (objToDelete.IsNotNull())
                    {
                        objToDelete.RecordStatus = RecordStatus;
                        objDBContext.SaveChanges();
                        ShowPaginatedAndDeletedRecords();
                    }
                    divSPDSuccessMessage.Visible = true;
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private ServiceProvision GetServiceProvisionFromID(int ProvisionID)
        {
            return objDBContext.ServiceProvisions.FirstOrDefault(cat => cat.ServiceProvisionID == ProvisionID);
        }
    
        private void ShowPaginatedAndDeletedRecords()
        {
            try
            {
                if (chkShowDeleted.Checked)
                {
                    rptrServiceProvision.DataSource = objDBContext.vwServiceProvisionDetails.Where(usr => usr.UserSpecific != true).OrderByDescending(obj => obj.ServiceProvisionID);
                    rptrServiceProvision.DataBind();
                }
                else
                {
                    rptrServiceProvision.DataSource = objDBContext.vwServiceProvisionDetails.OrderByDescending(obj => obj.ServiceProvisionID).Where(usr => usr.RecordStatus == DBKeys.RecordStatus_Active && usr.UserSpecific !=true);
                    rptrServiceProvision.DataBind();
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        #endregion

        protected void ddlNewService_SelectedIndexChanged(object sender, EventArgs e)
        {
            divNewError.Visible = divNewSuccess.Visible = false;
            UpdateNewServiceProvision();
        }

        private void UpdateNewServiceProvision()
        {
            hidServiceProvisionID.Value = "";

            int ServiceID = int.Parse(ddlNewService.SelectedValue);
            int CategoryID = int.Parse(ddlNewCategory.SelectedValue);
            var selServiceProvision = objDBContext.ServiceProvisions.FirstOrDefault(cat => cat.ServiceID == ServiceID && cat.ServiceCategoryID == CategoryID);
            if (selServiceProvision != null)
            {
                hidServiceProvisionID.Value = selServiceProvision.ServiceProvisionID.ToString();
                txtNewUserName.Text = selServiceProvision.UserName;
                txtNewServicePassword.Text = selServiceProvision.ServicePassword;
                txtNewServiceURL.Text = selServiceProvision.ServiceUrl;
                txtNewInstanceNumber.Text = selServiceProvision.InstanceNumber;
                txtNewApplicationServer.Text = selServiceProvision.ApplicationServer;
                txtNewUID.Text = selServiceProvision.UIDOnServer;
                txtNewDeveloperKey.Text = selServiceProvision.DeveloperKey;
                txtNewOtherInfo.Text = selServiceProvision.AdditionalInformation;
                txtNewUmax.Text = Convert.ToString(selServiceProvision.UserMax);
                txtNewUmin.Text = Convert.ToString(selServiceProvision.UserMin);
                txtNewCurrentUserCount.Text = Convert.ToString(selServiceProvision.CurrentUserCounter);
                ddlNewNotificationReqAt.SelectedValue = Convert.ToString(selServiceProvision.NotificationRequiredAt);
                //ddlNewExpirationPeriod.SelectedValue = Convert.ToString(selServiceProvision.ExpirationPeriod);
            }
            else
            {
                txtNewCurrentUserCount.Text = txtNewOtherInfo.Text = txtNewServicePassword.Text = txtNewServiceURL.Text = "";
                txtNewUID.Text = txtNewUmax.Text = txtNewUmin.Text = txtNewUserName.Text = txtNewInstanceNumber.Text = txtNewApplicationServer.Text = txtNewDeveloperKey.Text = "";
                ddlNewNotificationReqAt.SelectedValue = "0";// ddlNewExpirationPeriod.SelectedValue = "0";
                txtNewCurrentUserCount.Text = "0";
            }

        }

    }
}
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
    public partial class ManageBanners : System.Web.UI.Page
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
                            //Check if the screen should load edit IndexData from query string parameter.
                            LoadEditBanner();
                            //Check if the screen should delete any Banner from query string parameter.
                            UpdateBannerStatus(QueryStringKeys.Delete, DBKeys.RecordStatus_Delete);
                            //Check if the screen should activate any Banner from query string parameter.
                            UpdateBannerStatus(QueryStringKeys.Activate, DBKeys.RecordStatus_Active);
                            //Check if the screen should deactivate any Banner from query string parameter.
                            UpdateBannerStatus(QueryStringKeys.Deactivate, DBKeys.RecordStatus_Inactive);
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

        protected void btnNewReset_Click(object sender, EventArgs e)
        {
            txtNewBannerTitle.Text = txtNewRedirectLink.Text = string.Empty;
            ddlNewBannerPriority.SelectedValue = ddlNewBannerStatus.SelectedValue = "1";
            chkMandatory.Checked =divNewBannerSuccess.Visible=divNewBannerError.Visible= false;
        }

        protected void chkShowDeleted_CheckedChanged(object sender, EventArgs e)
        {
            ShowPaginatedAndDeletedRecords();
            divUBPSuccessMessage.Visible = false;
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnActivate_Click(object sender, EventArgs e)
        {
            divUBPSuccessMessage.Visible = false;
            ActivateDeactivateBanner(DBKeys.RecordStatus_Active);
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnDeactivate_Click(object sender, EventArgs e)
        {
            divUBPSuccessMessage.Visible = false;
            ActivateDeactivateBanner(DBKeys.RecordStatus_Inactive);
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            divUBPSuccessMessage.Visible = false;
            ActivateDeactivateBanner(DBKeys.RecordStatus_Delete);
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnAddNewIndexData_Click(object sender, EventArgs e)
        {
            try
            {
                int BannerCount = objDBContext.Banners.Count();
                ddlNewBannerPriority.DataSource = Enumerable.Range(1, BannerCount + 1);
                ddlNewBannerPriority.DataBind();
                mvContainer.ActiveViewIndex = 2;
                LoadStatus();
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.EditBanner].IsValid())
                {
                    divBannerError.Visible = false;
                    int BannerID = int.Parse(Request.QueryString[QueryStringKeys.EditBanner]);
                    if (flUploadBanner.HasFile)
                    {
                        string type = flUploadBanner.FileName.Substring(flUploadBanner.FileName.LastIndexOf('.') + 1).ToLower();
                        if (type == "jpg" || type == "jpeg" || type == "png")
                        {
                            string EditPath = "";
                            string DeletePath = "";
                            EditPath = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/Banners/" + BannerID + "/");
                            DeletePath = ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/Banners/" + BannerID;
                            if (Directory.Exists(EditPath))
                            {
                                DirectoryInfo Folder = new DirectoryInfo(Server.MapPath(DeletePath));
                                if (Folder.Exists)
                                {
                                    foreach (FileInfo fl in Folder.GetFiles())
                                    {
                                        fl.Delete();
                                    }
                                    Folder.Delete();
                                }
                            }
                            if (!Directory.Exists(EditPath))
                            {
                                Directory.CreateDirectory(EditPath);
                            }
                            flUploadBanner.SaveAs(EditPath + flUploadBanner.FileName);
                        }
                        else
                        {
                            divBannerSuccess.Visible = false;
                            divBannerError.Visible = true;
                            lblBannerError.Text = "Only jpg,jpeg and png formats are supported.";
                        }
                    }
                    if (divBannerError.Visible == false)
                    {
                        string ImageURL = "";
                        ImageURL = "/UploadedContents/Banners/" + BannerID + "/" + flUploadBanner.FileName;
                        var EditBanner = objDBContext.Banners.FirstOrDefault(ed => ed.BannerID == BannerID);
                        EditBanner.ImgTitle = txtBannerTitle.Text;
                        EditBanner.RedirectUrl = txtBannerRedirectLink.Text;
                        if (ddlBannerPriority.SelectedValue == null || ddlBannerPriority.SelectedValue == "0")
                        {
                            EditBanner.ImgPriority = null;
                        }
                        else
                        {
                            EditBanner.ImgPriority = Convert.ToInt32(ddlBannerPriority.SelectedValue);
                        }
                        EditBanner.RecordStatus = Convert.ToInt32(ddlBannerStatus.SelectedValue);
                        EditBanner.LastModifiedOn = DateTime.Now;
                        if (chkEditMandatory.Checked)
                        {
                            EditBanner.Mandatory = true;
                        }
                        else
                        {
                            EditBanner.Mandatory = false;
                        }
                        if (flUploadBanner.HasFile)
                        {
                            EditBanner.ImgPath = ImageURL;
                        }
                        objDBContext.SaveChanges();
                        divBannerSuccess.Visible = true;
                        divBannerError.Visible = false;
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                divBannerError.Visible = true;
                lblBannerError.Text = "Error while updating Banner.";
            }
        }

        protected void btnNewBannerSave_Click(object sender, EventArgs e)
        {
            try
            {
                divNewBannerError.Visible = false;
                int BannerCount =objDBContext.Banners.Count() >0? Convert.ToInt32(objDBContext.Banners.Max(count => count.BannerID)):0;
                int BannerID = BannerCount + 1;
                if (flupNewBanner.HasFile)
                {
                    string type = flupNewBanner.FileName.Substring(flupNewBanner.FileName.LastIndexOf('.') + 1).ToLower();
                    if (type == "jpg" || type == "jpeg" || type == "png")
                    {
                        string EditPath = "";
                        string DeletePath = "";
                        EditPath = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] +"/UploadedContents/Banners/" + BannerID + "/");
                        DeletePath = ConfigurationManager.AppSettings["UploadeContent"] +"/UploadedContents/Banners/" + BannerID;
                        if (Directory.Exists(EditPath))
                        {
                            DirectoryInfo Folder = new DirectoryInfo(Server.MapPath(DeletePath));
                            if (Folder.Exists)
                            {
                                foreach (FileInfo fl in Folder.GetFiles())
                                {
                                    fl.Delete();
                                }
                                Folder.Delete();
                            }
                        }
                        if (!Directory.Exists(EditPath))
                        {
                            Directory.CreateDirectory(EditPath);
                        }
                        flupNewBanner.SaveAs(EditPath + flupNewBanner.FileName);

                        string ImageURL = "";
                        ImageURL = "/UploadedContents/Banners/" + BannerID + "/" + flupNewBanner.FileName;
                        Banner NewBanner = new Banner();
                        NewBanner.ImgTitle = txtNewBannerTitle.Text;
                        NewBanner.RedirectUrl = txtNewRedirectLink.Text;
                        NewBanner.ImgPriority = Convert.ToInt32(ddlNewBannerPriority.SelectedValue);
                        NewBanner.RecordStatus = Convert.ToInt32(ddlNewBannerStatus.SelectedValue);
                        NewBanner.CreatedOn = DateTime.Now;
                        NewBanner.RecordStatus = 1;
                        NewBanner.ImgPath = ImageURL;
                        if (chkMandatory.Checked)
                        {
                            NewBanner.Mandatory = true;
                        }
                        else
                        {
                            NewBanner.Mandatory = false;
                        }
                        objDBContext.Banners.AddObject(NewBanner);
                        objDBContext.SaveChanges();
                        divNewBannerSuccess.Visible = true;
                    }
                    else
                    {
                        divNewBannerSuccess.Visible = false;
                        divNewBannerError.Visible = true;
                        lblNewBannerError.Text = "Only jpg,jpeg and png formats are supported.";
                    }
                }
                else
                {
                    divNewBannerSuccess.Visible = false;
                    divNewBannerError.Visible = true;
                    lblNewBannerError.Text = "Please select any image for Banner.";
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                divNewBannerError.Visible = true;
                lblNewBannerError.Text = "Error while updating Banner.";
            }
        }

        #endregion 

        #region ReusableRoutines

        protected void btmEditReset_Click(object sender, EventArgs e)
        {
            LoadEditBanner();
            divBannerError.Visible = divBannerSuccess.Visible = false;
        }

        private void LoadEditBanner()
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.EditBanner].IsValid())
                {
                    mvContainer.ActiveViewIndex = 1;
                    int BannerID = int.Parse(Request.QueryString[QueryStringKeys.EditBanner]);
                    var selBanner = objDBContext.Banners.FirstOrDefault(cat => cat.BannerID == BannerID);
                    if (selBanner != null)
                    {
                        LoadStatus();
                        txtBannerTitle.Text = selBanner.ImgTitle;
                        if (selBanner.ImgPath != null)
                        {
                            hypbannerImageLink.NavigateUrl = selBanner.ImgPath;
                            hypbannerImageLink.Visible = true;
                        }
                        txtBannerRedirectLink.Text = selBanner.RedirectUrl;
                        if (selBanner.Mandatory == true)
                        {
                            chkEditMandatory.Checked = true;
                        }
                        else
                        {
                            chkEditMandatory.Checked = false;
                        }
                        int BannerCount = objDBContext.Banners.Count();
                        ddlBannerPriority.DataSource = Enumerable.Range(0, BannerCount + 1);
                        ddlBannerPriority.DataBind();
                        if (selBanner.ImgPriority != null)
                        {
                            ddlBannerPriority.SelectedValue = Convert.ToString(selBanner.ImgPriority);
                        }
                        else
                        {
                            ddlBannerPriority.SelectedValue = "0";
                        }
                        ddlBannerStatus.SelectedValue = Convert.ToString(selBanner.RecordStatus);
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        private void LoadStatus()
        {
            try
            {
                ddlBannerStatus.DataSource = ddlNewBannerStatus.DataSource = objDBContext.RecordStatus.OrderBy(a => a.RecordStatusDesc);
                ddlBannerStatus.DataTextField = ddlNewBannerStatus.DataTextField = "RecordStatusDesc";
                ddlBannerStatus.DataValueField = ddlNewBannerStatus.DataValueField = "RecordStatusID";
                ddlBannerStatus.DataBind();
                ddlNewBannerStatus.DataBind();
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void ActivateDeactivateBanner(int BannerStatus)
        {
            try
            {
                foreach (RepeaterItem rItem in rptrUpdateBannerPriority.Items)
                {
                    CheckBox chkSelect = (rItem.FindControl("chkSelect") as CheckBox);
                    HiddenField hdnBannerID = (rItem.FindControl("hdnBannerID") as HiddenField);
                    if (chkSelect.Checked)
                    {
                        var selBanner = GetBannerFromID(int.Parse(hdnBannerID.Value));
                        if (selBanner.IsNotNull())
                        {
                            selBanner.RecordStatus = BannerStatus;
                            divUBPSuccessMessage.Visible = true;
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

        private void UpdateBannerStatus(string QueryStringKey, int RecordStatus)
        {
            try
            {
                //If redirected for delete then do delete
                if (Request.QueryString[QueryStringKey].IsValid())
                {
                    int BannerID = int.Parse(Request.QueryString[QueryStringKey]);
                    var objToDelete = GetBannerFromID(BannerID);
                    if (objToDelete.IsNotNull())
                    {
                        objToDelete.RecordStatus = RecordStatus;
                        objDBContext.SaveChanges();
                        ShowPaginatedAndDeletedRecords();
                    }
                    divUBPSuccessMessage.Visible = true;
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private Banner GetBannerFromID(int BannerID)
        {
            return objDBContext.Banners.FirstOrDefault(cat => cat.BannerID == BannerID);
        }
    
        private void ShowPaginatedAndDeletedRecords()
        {
            try
            {
                if (chkShowDeleted.Checked)
                {
                    rptrUpdateBannerPriority.DataSource = objDBContext.Banners.OrderByDescending(obj => obj.BannerID);
                    rptrUpdateBannerPriority.DataBind();
                }
                else
                {
                    rptrUpdateBannerPriority.DataSource = objDBContext.Banners.OrderByDescending(obj => obj.BannerID).Where(usr => usr.RecordStatus == DBKeys.RecordStatus_Active);
                    rptrUpdateBannerPriority.DataBind();
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
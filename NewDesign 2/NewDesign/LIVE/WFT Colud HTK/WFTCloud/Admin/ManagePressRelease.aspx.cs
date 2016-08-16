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
    public partial class ManagePressRelease : System.Web.UI.Page
    {
        #region Global Variables and Properties
        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        #endregion

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
                            //Check if the screen should load edit Press Release from query string parameter.
                            LoadEditPressRelease();
                            //Check if the screen should delete any Press Release from query string parameter.
                            UpdatePressReleaseStatus(QueryStringKeys.Delete, DBKeys.RecordStatus_Delete);
                            //Check if the screen should activate any Press Release from query string parameter.
                            UpdatePressReleaseStatus(QueryStringKeys.Activate, DBKeys.RecordStatus_Active);
                            //Check if the screen should deactivate any Press Release from query string parameter.
                            UpdatePressReleaseStatus(QueryStringKeys.Deactivate, DBKeys.RecordStatus_Inactive);
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

        #region ControlEvents

        protected void chkShowDeleted_CheckedChanged(object sender, EventArgs e)
        {
            ShowPaginatedAndDeletedRecords();
            divPRSuccessMessage.Visible = false;
        }

        protected void btnActivate_Click(object sender, EventArgs e)
        {
            divPRSuccessMessage.Visible = false;
            ActivateDeactivatePress(DBKeys.RecordStatus_Active);
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnDeactivate_Click(object sender, EventArgs e)
        {
            divPRSuccessMessage.Visible = false;
            ActivateDeactivatePress(DBKeys.RecordStatus_Inactive);
            ShowPaginatedAndDeletedRecords();
        }

        #endregion 

        #region ReusableRoutines

        private void LoadEditPressRelease()
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.EditPressRelease].IsValid())
                {
                    mvContainer.ActiveViewIndex = 1;
                    int PressReleaseID = int.Parse(Request.QueryString[QueryStringKeys.EditPressRelease]);
                    var selPressRelease = GetPressReleaseFromID(PressReleaseID);
                    if (selPressRelease != null)
                    {
                        lblTableHeader.Text = "Edit Press Release";
                        hdnFlagPressID.Value = PressReleaseID.ToString();
                        LoadFieldsFromData(selPressRelease);
                    }
                    else
                    {
                        lblTableHeader.Text = "New Press Release";
                        hdnFlagPressID.Value = "0";
                        txtCompanyName.Text = txtPRHeader.Text = txtCompanyDescription.Text = txtPlaceName.Text = txtPRMainContent.Text = txtQuote.Text = txtVideoURL.Text = txtActualPRWebLink.Text = string.Empty;
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void LoadFieldsFromData(PressRelease selPressRelease)
        {
            try
            {
                txtCompanyName.Text = selPressRelease.CompanyName;
                txtPRHeader.Text = selPressRelease.PressReleaseHeader;
                txtPRDate.Text = Convert.ToDateTime(selPressRelease.PressReleaseDate).ToString("dd - MMM - yyyy");
                txtCompanyDescription.Text = selPressRelease.CompanyDescription;
                txtPlaceName.Text = selPressRelease.PlaceName;
                txtPRMainContent.Text = (selPressRelease.PressReleaseContent);
                txtQuote.Text = selPressRelease.CaptionContent;
                txtVideoURL.Text = selPressRelease.VideoURL;
                txtActualPRWebLink.Text = selPressRelease.ActualPRLink;
                if (selPressRelease.ImagePath != null && selPressRelease.ImagePath != "")
                {
                    hypThumbnailImageLink.Visible = true;
                    hypThumbnailImageLink.NavigateUrl = selPressRelease.ImagePath;
                }
                else
                    hypThumbnailImageLink.Visible = false;
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        private void UpdatePressReleaseStatus(string QueryStringKey, int RecordStatus)
        {
            try
            {
                //If redirected for delete then do delete
                if (Request.QueryString[QueryStringKey].IsValid())
                {
                    int PressReleaseID = int.Parse(Request.QueryString[QueryStringKey]);
                    var objToDelete = GetPressReleaseFromID(PressReleaseID);
                    if (objToDelete.IsNotNull())
                    {
                        objToDelete.RecordStatus = RecordStatus;
                        objDBContext.SaveChanges();
                        ShowPaginatedAndDeletedRecords();
                    }
                    divPRSuccessMessage.Visible = true;
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private PressRelease GetPressReleaseFromID(int PressReleaseID)
        {
            return objDBContext.PressReleases.FirstOrDefault(cat => cat.PressReleaseID == PressReleaseID);
        }
    
        private void ShowPaginatedAndDeletedRecords()
        {
            try
            {
                if (chkShowDeleted.Checked)
                {
                    rptrPressRelease.DataSource = objDBContext.PressReleases.OrderByDescending(obj => obj.PressReleaseID);
                    rptrPressRelease.DataBind();
                }
                else
                {
                    rptrPressRelease.DataSource = objDBContext.PressReleases.OrderByDescending(obj => obj.PressReleaseID).Where(usr => usr.RecordStatus == DBKeys.RecordStatus_Active);
                    rptrPressRelease.DataBind();
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void ActivateDeactivatePress(int PressStatusStatus)
        {
            try
            {
                foreach (RepeaterItem rItem in rptrPressRelease.Items)
                {
                    CheckBox chkSelect = (rItem.FindControl("chkSelect") as CheckBox);
                    HiddenField hdnPRID = (rItem.FindControl("hdnPRID") as HiddenField);
                    if (chkSelect.Checked)
                    {
                        var selPress = GetPressReleaseFromID(int.Parse(hdnPRID.Value));
                        if (selPress.IsNotNull())
                        {
                            selPress.RecordStatus = PressStatusStatus;
                            divPRSuccessMessage.Visible = true;
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

        #endregion

        protected void btnAddNewCMS_Click(object sender, EventArgs e)
        {
            mvContainer.ActiveViewIndex = 1;
            divPRSuccess.Visible = divPRError.Visible = false;
            lblTableHeader.Text = "New Press Release";
            hdnFlagPressID.Value = "0";
            txtCompanyName.Text = txtPRHeader.Text = txtCompanyDescription.Text = txtPlaceName.Text = txtPRMainContent.Text = txtQuote.Text = txtVideoURL.Text = txtActualPRWebLink.Text = string.Empty;
        }

        protected void btnSaveCMS_Click(object sender, EventArgs e)
        {
            try
            {
                string CompanyName = txtCompanyName.Text;
                string PRHeader = txtPRHeader.Text;
                DateTime PRdate = Convert.ToDateTime(txtPRDate.Text);

                int PressID = int.Parse(hdnFlagPressID.Value);
                //If new Press Release then add to dbcontext
                if (PressID > 0)
                {
                    var PR=objDBContext.PressReleases.FirstOrDefault(obj => obj.PressReleaseID == PressID);
                    LoadDataFromFields(PR);
                    if (fluImage.HasFile)
                    {
                        LoadFileUpload(PressID);
                        PR.ImagePath = "/UploadedContents/PressRelease/" + PressID + "/" + fluImage.FileName;
                    }
                }
                else
                {
                    var PRExist = objDBContext.PressReleases.FirstOrDefault(p => p.PreviewCompanyName == CompanyName && p.PreviewPressReleaseHeader == PRHeader && p.PreviewPressReleaseDate == PRdate);
                    if (PRExist != null)
                    {
                        int PRID = PRExist.PressReleaseID;
                        LoadDataFromFields(PRExist);
                        if (fluImage.HasFile)
                        {
                            LoadFileUpload(PRID);
                            PRExist.ImagePath = "/UploadedContents/PressRelease/" + PRID + "/" + fluImage.FileName;
                        }
                        PRExist.RecordStatus = DBKeys.RecordStatus_Active;
                    }
                    else
                    {
                        int PrCount = objDBContext.PressReleases.Count() + 1;
                        PressRelease objPressRelease = new PressRelease();
                        LoadDataFromFields(objPressRelease);
                        if (fluImage.HasFile)
                        {
                            LoadFileUpload(PrCount);
                            objPressRelease.ImagePath = "/UploadedContents/PressRelease/" + PrCount + "/" + fluImage.FileName;
                        }
                        objPressRelease.RecordStatus = DBKeys.RecordStatus_Active;
                        objDBContext.PressReleases.AddObject(objPressRelease);
                    }
                }
                objDBContext.SaveChanges();
                divPRError.Visible = false;
                divPRSuccess.Visible = true;
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                divPRSuccess.Visible = false;
                divPRError.Visible = true;
                lblPRError.Text = "Error occured while saving details.";
            }
        }

        private void LoadFileUpload(int ImageUpload)
        {
            try
            {
                string Thumbnailtype = fluImage.FileName.Substring(fluImage.FileName.LastIndexOf('.') + 1).ToLower();
                string FilePath = "";
                string DeletePath = "";
                if (fluImage.HasFile)
                {
                    if (Thumbnailtype == "jpg" || Thumbnailtype == "jpeg" || Thumbnailtype == "png" || Thumbnailtype == "gif" || Thumbnailtype == "bmp" || Thumbnailtype == "tif")
                    {
                        FilePath = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/PressRelease/" + ImageUpload + "/");
                        DeletePath = ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/PressRelease/" + ImageUpload + "/";
                        if (Directory.Exists(FilePath))
                        {
                            //Directory.Delete(Path);
                            DirectoryInfo Folder = new DirectoryInfo(Server.MapPath(DeletePath));
                            if (Folder.Exists)
                            {
                                foreach (FileInfo fl in Folder.GetFiles(fluImage.FileName))
                                {
                                    fl.Delete();
                                }
                            }
                        }
                        if (!Directory.Exists(FilePath))
                        {
                            Directory.CreateDirectory(FilePath);
                        }
                        fluImage.SaveAs(FilePath + fluImage.FileName);
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void LoadDataFromFields(PressRelease selPressRelease)
        {
            try
            {
                selPressRelease.CompanyName = txtCompanyName.Text;
                selPressRelease.PressReleaseHeader = txtPRHeader.Text;
                selPressRelease.PressReleaseDate = Convert.ToDateTime(txtPRDate.Text);
                selPressRelease.CompanyDescription = txtCompanyDescription.Text;
                selPressRelease.PlaceName = txtPlaceName.Text;
                selPressRelease.PressReleaseContent = txtPRMainContent.Text;
                selPressRelease.CaptionContent = txtQuote.Text;
                selPressRelease.VideoURL = txtVideoURL.Text;
                selPressRelease.ActualPRLink = txtActualPRWebLink.Text.Trim(); 
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                string CompanyName = txtCompanyName.Text;
                string PRHeader = txtPRHeader.Text;
                DateTime PRdate = Convert.ToDateTime(txtPRDate.Text);
                int PRID = 0;

                int PressID = int.Parse(hdnFlagPressID.Value);
                //If new Press Release then add to dbcontext
                if (PressID > 0)
                {
                    var PR = objDBContext.PressReleases.FirstOrDefault(obj => obj.PressReleaseID == PressID);
                    LoadPreviewFromFields(PR,PressID);
                    LoadPreviewFileUpload(PressID);
                    //if (fluImage.HasFile)
                    //{
                    //    PR.PreviewImagePath = "/UploadedContents/PressRelease/" + PressID + "/" + fluImage.FileName;
                    //}
                    PRID = PressID;
                }
                else
                {
                    var PRExist = objDBContext.PressReleases.FirstOrDefault(p => p.PreviewCompanyName == CompanyName && p.PreviewPressReleaseHeader == PRHeader && p.PreviewPressReleaseDate == PRdate);
                    if (PRExist != null)
                    {
                        int PRExistID = PRExist.PressReleaseID;
                        LoadPreviewFromFields(PRExist, PRExistID);
                        LoadPreviewFileUpload(PRExistID);
                        //if (fluImage.HasFile)
                        //{
                        //    PRExist.PreviewImagePath = "/UploadedContents/PressRelease/" + PRExistID + "/" + fluImage.FileName;
                        //}
                        PRID = PRExistID;
                    }
                    else
                    {
                        int PrCount = objDBContext.PressReleases.Count() + 1;
                        PressRelease objPressRelease = new PressRelease();
                        LoadPreviewFromFields(objPressRelease,PrCount);
                        objPressRelease.RecordStatus = DBKeys.RecordStatus_Delete;
                        LoadPreviewFileUpload(PrCount);
                        //if (fluImage.HasFile)
                        //{
                        //    objPressRelease.PreviewImagePath = "/UploadedContents/PressRelease/" + PrCount + "/" + fluImage.FileName;
                        //}
                        objDBContext.PressReleases.AddObject(objPressRelease);
                    }
                }
                objDBContext.SaveChanges();
                if (PRID <= 0)
                {
                    PRID = objDBContext.PressReleases.FirstOrDefault(p => p.PreviewCompanyName == CompanyName && p.PreviewPressReleaseHeader == PRHeader && p.PreviewPressReleaseDate == PRdate).PressReleaseID;
                }
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('/User/press-release-staticcontent.aspx?previewid=" + PRID + "&view=preview', '_blank');", true);
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                divPRSuccess.Visible = false;
                divPRError.Visible = true;
                lblPRError.Text = "Error occured while processing preview details.";
            }
        }

        private void LoadPreviewFromFields(PressRelease selPressRelease,int PressID)
        {
            try
            {
                selPressRelease.PreviewCompanyName = txtCompanyName.Text;
                selPressRelease.PreviewPressReleaseHeader = txtPRHeader.Text;
                selPressRelease.PreviewPressReleaseDate = Convert.ToDateTime(txtPRDate.Text);
                selPressRelease.PreviewCompanyDescription = txtCompanyDescription.Text;
                selPressRelease.PreviewPlaceName = txtPlaceName.Text;
                selPressRelease.PreviewPressReleaseContent = txtPRMainContent.Text;
                selPressRelease.PreviewCaptionContent = txtQuote.Text;
                selPressRelease.PreviewVideoURL = txtVideoURL.Text;
                selPressRelease.ActualPRLink = txtActualPRWebLink.Text;
                if (fluImage.HasFile)
                {
                    selPressRelease.ImagePath = "/UploadedContents/PressRelease/" + PressID + "/" + fluImage.FileName;
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void LoadPreviewFileUpload(int ImageUpload)
        {
            try
            {
                string Thumbnailtype = fluImage.FileName.Substring(fluImage.FileName.LastIndexOf('.') + 1).ToLower();
                string FilePath = "";
                string DeletePath = "";
                if (fluImage.HasFile)
                {
                    if (Thumbnailtype == "jpg" || Thumbnailtype == "jpeg" || Thumbnailtype == "png" || Thumbnailtype == "gif" || Thumbnailtype == "bmp" || Thumbnailtype == "tif")
                    {
                        FilePath = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/PressRelease/" + ImageUpload + "/");
                        DeletePath = ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/PressRelease/" + ImageUpload + "/";
                        if (Directory.Exists(FilePath))
                        {
                            //Directory.Delete(Path);
                            DirectoryInfo Folder = new DirectoryInfo(Server.MapPath(DeletePath));
                            if (Folder.Exists)
                            {
                                foreach (FileInfo fl in Folder.GetFiles(fluImage.FileName))
                                {
                                    fl.Delete();
                                }
                            }
                        }
                        if (!Directory.Exists(FilePath))
                        {
                            Directory.CreateDirectory(FilePath);
                        }
                        fluImage.SaveAs(FilePath + fluImage.FileName);
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
    }
}
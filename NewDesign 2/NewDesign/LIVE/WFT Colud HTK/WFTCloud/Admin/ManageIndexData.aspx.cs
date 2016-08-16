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
    public partial class ManageIndexData : System.Web.UI.Page
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
                            LoadEditIndexData();
                            //Check if the screen should delete any IndexData from query string parameter.
                            UpdateIndexDataStatus(QueryStringKeys.Delete, DBKeys.RecordStatus_Delete);
                            //Check if the screen should activate any IndexData from query string parameter.
                            UpdateIndexDataStatus(QueryStringKeys.Activate, DBKeys.RecordStatus_Active);
                            //Check if the screen should deactivate any IndexData from query string parameter.
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

        protected void btnNewReset_Click1(object sender, EventArgs e)
        {
            txtAddNewIndexVideo.Text = txtAddNewIndexTitle.Text = txtAddNewIndexDescription.Text = string.Empty;
            ddlAddNewIndexCategory.SelectedValue = "0";
        }

        protected void btmEditReset_Click(object sender, EventArgs e)
        {
            LoadEditIndexData();
        }

        protected void chkShowDeleted_CheckedChanged(object sender, EventArgs e)
        {
            ShowPaginatedAndDeletedRecords();
            divANIDSuccessMessage.Visible = false;
        }

        protected void btnActivate_Click(object sender, EventArgs e)
        {
            ActivateDeactivateIndexData(DBKeys.RecordStatus_Active);
            divANIDSuccessMessage.Visible = true;
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnDeactivate_Click(object sender, EventArgs e)
        {
            ActivateDeactivateIndexData(DBKeys.RecordStatus_Inactive);
            divANIDSuccessMessage.Visible = true;
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ActivateDeactivateIndexData(DBKeys.RecordStatus_Delete);
            divANIDSuccessMessage.Visible = true;
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnAddNewIndexData_Click(object sender, EventArgs e)
        {
            mvContainer.ActiveViewIndex = 2;
        }

        protected void ddlAddNewIndexCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int Category = Convert.ToInt32(ddlAddNewIndexCategory.SelectedValue);
                if (Category == 2)
                {
                    pnlImage.Visible = false;
                    pnlVideo.Visible = true;
                }
                else if (Category == 0 || Category == 3 || Category == 4)
                {
                    pnlImage.Visible = false;
                    pnlVideo.Visible = false;
                }
                else
                {
                    pnlImage.Visible = true;
                    pnlVideo.Visible = false;
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void btnAddNewIndexSave_Click(object sender, EventArgs e)
        {
            try
            {
                string type = FileUpload1.FileName.Substring(FileUpload1.FileName.LastIndexOf('.') + 1).ToLower();
                string Thumbnailtype = fluNewThumbnail.FileName.Substring(fluNewThumbnail.FileName.LastIndexOf('.') + 1).ToLower();
                string VideoCheck = txtAddNewIndexVideo.Text;
                int IndexDataCount = objDBContext.IndexDatas.Count() >0? Convert.ToInt32(objDBContext.IndexDatas.Max(count => count.IndexDataID)):0;
                int IndexDataID = IndexDataCount + 1;
                string FilePath = "";
                int IndexDataTypes = Convert.ToInt32(ddlAddNewIndexCategory.SelectedValue);
                if (FileUpload1.HasFile || fluNewThumbnail.HasFile)
                {
                    if (type == "jpg" || type == "pdf" || VideoCheck != string.Empty || Thumbnailtype == "jpg" || Thumbnailtype == "jpeg" || Thumbnailtype == "png" || Thumbnailtype == "gif" || Thumbnailtype == "bmp" || Thumbnailtype == "tif")
                    {
                        if (IndexDataTypes != 2)
                        {
                            if (IndexDataTypes == 1)
                            {
                                FilePath = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/IndexDatas/Certificate/" + IndexDataID + "/");
                            }
                            else if (IndexDataTypes == 3)
                            {
                                FilePath = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/IndexDatas/Partner/" + IndexDataID + "/");
                            }
                            else if (IndexDataTypes == 4)
                            {
                                FilePath = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/IndexDatas/Client/" + IndexDataID + "/");
                            }
                            else if (IndexDataTypes == 5)
                            {
                                FilePath = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/IndexDatas/Brochure/" + IndexDataID + "/");
                            }
                            else if (IndexDataTypes == 6)
                            {
                                FilePath = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/IndexDatas/WhitePaper/" + IndexDataID + "/");
                            }
                            if (!Directory.Exists(FilePath))
                            {
                                Directory.CreateDirectory(FilePath);
                            }
                            if (IndexDataTypes != 3 || IndexDataTypes != 4)
                            {
                                FileUpload1.SaveAs(FilePath + FileUpload1.FileName);
                            }
                            fluNewThumbnail.SaveAs(FilePath + fluNewThumbnail.FileName);
                        }
                        else if (IndexDataTypes == 2)
                        {
                            string VideoThumbnail = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/IndexDatas/Video/" + IndexDataID + "/");
                            if (!Directory.Exists(VideoThumbnail))
                            {
                                Directory.CreateDirectory(VideoThumbnail);
                            }
                            fluNewThumbnail.SaveAs(VideoThumbnail + fluNewThumbnail.FileName);
                        }
                        string ImageURL = "";
                        string ThumbnailImageURL = "";
                        if (IndexDataTypes == 1)
                        {
                            ImageURL = "/UploadedContents/IndexDatas/Certificate/" + IndexDataID + "/" + FileUpload1.FileName;
                            ThumbnailImageURL = "/UploadedContents/IndexDatas/Certificate/" + IndexDataID + "/" + fluNewThumbnail.FileName;
                        }
                        else if (IndexDataTypes == 2)
                        {
                            ThumbnailImageURL = "/UploadedContents/IndexDatas/Video/" + IndexDataID + "/" + fluNewThumbnail.FileName;
                        }
                        else if (IndexDataTypes == 3)
                        {
                            ImageURL = "/UploadedContents/IndexDatas/Partner/" + IndexDataID + "/" + FileUpload1.FileName;
                            ThumbnailImageURL = "/UploadedContents/IndexDatas/Partner/" + IndexDataID + "/" + fluNewThumbnail.FileName;
                        }
                        else if (IndexDataTypes == 4)
                        {
                            ImageURL = "/UploadedContents/IndexDatas/Client/" + IndexDataID + "/" + FileUpload1.FileName;
                            ThumbnailImageURL = "/UploadedContents/IndexDatas/Client/" + IndexDataID + "/" + fluNewThumbnail.FileName;
                        }
                        else if (IndexDataTypes == 5)
                        {
                            ImageURL = "/UploadedContents/IndexDatas/Brochure/" + IndexDataID + "/" + FileUpload1.FileName;
                            ThumbnailImageURL = "/UploadedContents/IndexDatas/Brochure/" + IndexDataID + "/" + fluNewThumbnail.FileName;
                        }
                        else if (IndexDataTypes == 6)
                        {
                            ImageURL = "/UploadedContents/IndexDatas/WhitePaper/" + IndexDataID + "/" + FileUpload1.FileName;
                            ThumbnailImageURL = "/UploadedContents/IndexDatas/WhitePaper/" + IndexDataID + "/" + fluNewThumbnail.FileName;
                        }
                        var userImageProof1 = objDBContext.IndexDatas.FirstOrDefault(imageurl => imageurl.Path == ImageURL);
                        if (userImageProof1 == null)
                        {
                            //int PriorityCount=0;
                            IndexData NewIndexData = new IndexData();
                            NewIndexData.IndexDataTypeID = Convert.ToInt32(ddlAddNewIndexCategory.SelectedValue);
                            NewIndexData.Description = txtAddNewIndexDescription.Text;
                            NewIndexData.Title = txtAddNewIndexTitle.Text;
                            NewIndexData.ThumbnailPath = ThumbnailImageURL;
                            NewIndexData.Priority = Convert.ToInt32(txtNewPriority.Text);
                            if (IndexDataTypes == 1)
                            {
                                //PriorityCount = objDBContext.IndexDatas.Where(c => c.IndexDataTypeID == 1).Count();
                                //NewIndexData.Priority = PriorityCount + 1;
                                NewIndexData.Path = ImageURL;
                            }
                            else if (IndexDataTypes == 2)
                            {
                                //PriorityCount = objDBContext.IndexDatas.Where(c => c.IndexDataTypeID == 2).Count();
                                //NewIndexData.Priority = PriorityCount + 1;
                                NewIndexData.Path = txtAddNewIndexVideo.Text;
                            }
                            else if (IndexDataTypes == 3 )
                            {
                                //PriorityCount = objDBContext.IndexDatas.Where(c => c.IndexDataTypeID == 3).Count();
                                //NewIndexData.Priority = PriorityCount + 1;
                                NewIndexData.Path = ImageURL;
                            }
                            else if (IndexDataTypes == 4)
                            {
                                //PriorityCount = objDBContext.IndexDatas.Where(c => c.IndexDataTypeID == 4).Count();
                                //NewIndexData.Priority = PriorityCount + 1;
                                NewIndexData.Path = ImageURL;
                            }
                            else if (IndexDataTypes == 5 || IndexDataTypes == 6)
                            {
                                //PriorityCount = objDBContext.IndexDatas.Where(c => c.IndexDataTypeID == 5 || c.IndexDataTypeID == 6).Count();
                                //NewIndexData.Priority = PriorityCount + 1;
                                NewIndexData.Path = ImageURL;
                            }
                            NewIndexData.CreatedOn = DateTime.Now;
                            NewIndexData.RecordStatus = 1;
                            objDBContext.IndexDatas.AddObject(NewIndexData);
                            objDBContext.SaveChanges();
                        }
                        divAddNewIndexDataErrorMessage.Visible = false;
                        divAddNewIndexDataSuccess.Visible = true;
                    }
                    else
                    {
                        divAddNewIndexDataErrorMessage.Visible = true;
                        divAddNewIndexDataSuccess.Visible = false;
                        lblindexErrorMessage.Text = "The file format you have selected is not supported.";
                    }
                }
                else
                {
                    divAddNewIndexDataErrorMessage.Visible = true;
                    divAddNewIndexDataSuccess.Visible = false;
                    lblindexErrorMessage.Text = "Please select any file for Index Data.";
                }
                ClearAddNewIndex();
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                divAddNewIndexDataErrorMessage.Visible = true;
                lblindexErrorMessage.Text = "Error while saving Index data.";
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.EditIndexData].IsValid())
                {
                    divIndexDataError.Visible = false;
                    lblIndexDataError.Text = string.Empty;
                    int IndexDataID = int.Parse(Request.QueryString[QueryStringKeys.EditIndexData]);
                    string type = flUploadImage.FileName.Substring(flUploadImage.FileName.LastIndexOf('.') + 1).ToLower();
                    string Thumbnailtype = fluEditThumbnail.FileName.Substring(fluEditThumbnail.FileName.LastIndexOf('.') + 1).ToLower();
                    //string VideoCheck = txtVideoLink.Text;
                    string Path = "";
                    string DeletePath = "";
                    string ThumbnailPath = "";
                    string DeleteThumbnailPath = "";
                    int IndexDataType = objDBContext.IndexDatas.FirstOrDefault(index => index.IndexDataID == IndexDataID).IndexDataTypeID;
                    if (flUploadImage.HasFile == true)
                    {
                        if (type == "jpg" || type == "pdf")
                        {
                            if (IndexDataType != 2)
                            {
                                if (IndexDataType == 1)
                                {
                                    Path = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/IndexDatas/Certificate/" + IndexDataID + "/");
                                    DeletePath = ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/IndexDatas/Certificate/" + IndexDataID + "/";
                                }
                                else if (IndexDataType == 3)
                                {
                                    Path = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/IndexDatas/Partner/" + IndexDataID + "/");
                                    DeletePath = ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/IndexDatas/Partner/" + IndexDataID + "/";
                                }
                                else if (IndexDataType == 4)
                                {
                                    Path = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/IndexDatas/Client/" + IndexDataID + "/");
                                    DeletePath = ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/IndexDatas/Client/" + IndexDataID + "/";
                                }
                                else if (IndexDataType == 5)
                                {
                                    Path = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/IndexDatas/Brochure/" + IndexDataID + "/");
                                    DeletePath = ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/IndexDatas/Brochure/" + IndexDataID + "/";
                                }
                                else if (IndexDataType == 6)
                                {
                                    Path = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/IndexDatas/WhitePaper/" + IndexDataID + "/");
                                    DeletePath = ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/IndexDatas/WhitePaper/" + IndexDataID + "/";
                                }
                                if (Directory.Exists(Path))
                                {
                                    //Directory.Delete(Path);
                                    DirectoryInfo Folder = new DirectoryInfo(Server.MapPath(DeletePath));
                                    if (Folder.Exists)
                                    {
                                        foreach (FileInfo fl in Folder.GetFiles(flUploadImage.FileName))
                                        {
                                            fl.Delete();
                                        }
                                    }
                                }
                                if (!Directory.Exists(Path))
                                {
                                    Directory.CreateDirectory(Path);
                                }
                                flUploadImage.SaveAs(Path + flUploadImage.FileName);
                            }
                        }
                        else
                        {
                            divIndexDataError.Visible = true;
                            divIndexDataSuccess.Visible = false;
                            lblIndexDataError.Text = "Please select .jpg or .pdf file for Image Upload.";
                        }
                    }
                    if (fluEditThumbnail.HasFile == true)
                    {
                        if (Thumbnailtype == "jpg" || Thumbnailtype == "jpeg" || Thumbnailtype == "png" || Thumbnailtype == "gif" || Thumbnailtype == "bmp" || Thumbnailtype == "tif")
                        {
                            if (IndexDataType == 1)
                            {
                                ThumbnailPath = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/IndexDatas/Certificate/" + IndexDataID + "/");
                                DeleteThumbnailPath = ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/IndexDatas/Certificate/" + IndexDataID + "/";
                            }
                            else if (IndexDataType == 2)
                            {
                                ThumbnailPath = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/IndexDatas/Video/" + IndexDataID + "/");
                                DeleteThumbnailPath = ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/IndexDatas/Video/" + IndexDataID + "/";
                            }
                            else if (IndexDataType == 3)
                            {
                                ThumbnailPath = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/IndexDatas/Partner/" + IndexDataID + "/");
                                DeleteThumbnailPath = ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/IndexDatas/Partner/" + IndexDataID + "/";
                            }
                            else if (IndexDataType == 4)
                            {
                                ThumbnailPath = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/IndexDatas/Client/" + IndexDataID + "/");
                                DeleteThumbnailPath = ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/IndexDatas/Client/" + IndexDataID + "/";
                            }
                            else if (IndexDataType == 5)
                            {
                                ThumbnailPath = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/IndexDatas/Brochure/" + IndexDataID + "/");
                                DeleteThumbnailPath = ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/IndexDatas/Brochure/" + IndexDataID + "/";
                            }
                            else if (IndexDataType == 6)
                            {
                                ThumbnailPath = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/IndexDatas/WhitePaper/" + IndexDataID + "/");
                                DeleteThumbnailPath = ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/IndexDatas/WhitePaper/" + IndexDataID + "/";
                            }
                            if (Directory.Exists(ThumbnailPath))
                            {
                                //Directory.Delete(ThumbnailPath);
                                DirectoryInfo Folder = new DirectoryInfo(Server.MapPath(DeleteThumbnailPath));
                                if (Folder.Exists)
                                {
                                    foreach (FileInfo fl in Folder.GetFiles(fluEditThumbnail.FileName))
                                    {
                                        fl.Delete();
                                    }
                                }
                            }
                            if (!Directory.Exists(ThumbnailPath))
                            {
                                Directory.CreateDirectory(ThumbnailPath);
                            }
                            fluEditThumbnail.SaveAs(ThumbnailPath + fluEditThumbnail.FileName);
                        }
                        else
                        {
                            divIndexDataError.Visible = true;
                            divIndexDataSuccess.Visible = false;
                            lblIndexDataError.Text = "The file format you have selected is not supported.";
                        }
                    }
                    if (divIndexDataError.Visible == false)
                    {
                        string ImageURL = "";
                        string ThumbnailImageURL = "";
                        if (IndexDataType == 1)
                        {
                            ImageURL = "/UploadedContents/IndexDatas/Certificate/" + IndexDataID + "/" + flUploadImage.FileName;
                            ThumbnailImageURL = "/UploadedContents/IndexDatas/Certificate/" + IndexDataID + "/" + fluEditThumbnail.FileName;
                        }
                        else if (IndexDataType == 2)
                        {
                            ImageURL = "/UploadedContents/IndexDatas/Video/" + IndexDataID + "/" + flUploadImage.FileName;
                            ThumbnailImageURL = "/UploadedContents/IndexDatas/Video/" + IndexDataID + "/" + fluEditThumbnail.FileName;
                        }
                        else if (IndexDataType == 3)
                        {
                            ImageURL = "/UploadedContents/IndexDatas/Partner/" + IndexDataID + "/" + flUploadImage.FileName;
                            ThumbnailImageURL = "/UploadedContents/IndexDatas/Partner/" + IndexDataID + "/" + fluEditThumbnail.FileName;
                        }
                        else if (IndexDataType == 4)
                        {
                            ImageURL = "/UploadedContents/IndexDatas/Client/" + IndexDataID + "/" + flUploadImage.FileName;
                            ThumbnailImageURL = "/UploadedContents/IndexDatas/Client/" + IndexDataID + "/" + fluEditThumbnail.FileName;
                        }
                        else if (IndexDataType == 5)
                        {
                            ImageURL = "/UploadedContents/IndexDatas/Brochure/" + IndexDataID + "/" + flUploadImage.FileName;
                            ThumbnailImageURL = "/UploadedContents/IndexDatas/Brochure/" + IndexDataID + "/" + fluEditThumbnail.FileName;
                        }
                        else if (IndexDataType == 6)
                        {
                            ImageURL = "/UploadedContents/IndexDatas/WhitePaper/" + IndexDataID + "/" + flUploadImage.FileName;
                            ThumbnailImageURL = "/UploadedContents/IndexDatas/WhitePaper/" + IndexDataID + "/" + fluEditThumbnail.FileName;
                        }
                        var NewIndexData = objDBContext.IndexDatas.FirstOrDefault(Update => Update.IndexDataID == IndexDataID);
                        //NewIndexData.Priority = Convert.ToInt32(ddlPriority.SelectedValue);
                        NewIndexData.Priority = Convert.ToInt32(txtEditPriority.Text);
                        NewIndexData.Description = txtDescription.Text;
                        NewIndexData.Title = txtTitle.Text;
                        NewIndexData.ModifiedOn = DateTime.Now;
                        if (IndexDataType == 2 && fluEditThumbnail.HasFile)
                        {
                            NewIndexData.Path = txtVideoLink.Text;
                            NewIndexData.ThumbnailPath = ThumbnailImageURL;
                        }
                        else
                        {
                            NewIndexData.Path = txtVideoLink.Text;
                        }
                        if (flUploadImage.HasFile)
                        {
                            NewIndexData.Path = ImageURL;
                        }
                        if(fluEditThumbnail.HasFile)
                        {
                            NewIndexData.ThumbnailPath = ThumbnailImageURL;
                        }
                        objDBContext.SaveChanges();
                        divIndexDataError.Visible = false;
                        divIndexDataSuccess.Visible = true;
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                divIndexDataError.Visible = true;
                lblIndexDataError.Text = "Error while updating Index data.";
            }
        }

        protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int SelectedType = Convert.ToInt32(ddlFilter.SelectedValue);
                if (SelectedType == 0)
                {
                    ShowPaginatedAndDeletedRecords();
                }
                if (SelectedType == 1)
                {
                    if (chkShowDeleted.Checked)
                    {
                        rptrAddNewIndexData.DataSource = objDBContext.vwManageIndexDatas.OrderByDescending(obj => obj.IndexDataID).Where(type => type.IndexDataTypeID == 1);
                        rptrAddNewIndexData.DataBind();
                    }
                    else
                    {
                        rptrAddNewIndexData.DataSource = objDBContext.vwManageIndexDatas.OrderByDescending(obj => obj.IndexDataID).Where(usr => usr.RecordStatus == DBKeys.RecordStatus_Active && usr.IndexDataTypeID == 1);
                        rptrAddNewIndexData.DataBind();
                    }
                }
                if (SelectedType == 2)
                {
                    if (chkShowDeleted.Checked)
                    {
                        rptrAddNewIndexData.DataSource = objDBContext.vwManageIndexDatas.OrderByDescending(obj => obj.IndexDataID).Where(type => type.IndexDataTypeID == 2);
                        rptrAddNewIndexData.DataBind();
                    }
                    else
                    {
                        rptrAddNewIndexData.DataSource = objDBContext.vwManageIndexDatas.OrderByDescending(obj => obj.IndexDataID).Where(usr => usr.RecordStatus == DBKeys.RecordStatus_Active && usr.IndexDataTypeID == 2);
                        rptrAddNewIndexData.DataBind();
                    }
                }
                if (SelectedType == 3)
                {
                    if (chkShowDeleted.Checked)
                    {
                        rptrAddNewIndexData.DataSource = objDBContext.vwManageIndexDatas.OrderByDescending(obj => obj.IndexDataID).Where(type => type.IndexDataTypeID == 3);
                        rptrAddNewIndexData.DataBind();
                    }
                    else
                    {
                        rptrAddNewIndexData.DataSource = objDBContext.vwManageIndexDatas.OrderByDescending(obj => obj.IndexDataID).Where(usr => usr.RecordStatus == DBKeys.RecordStatus_Active && usr.IndexDataTypeID == 3);
                        rptrAddNewIndexData.DataBind();
                    }
                }
                if (SelectedType == 4)
                {
                    if (chkShowDeleted.Checked)
                    {
                        rptrAddNewIndexData.DataSource = objDBContext.vwManageIndexDatas.OrderByDescending(obj => obj.IndexDataID).Where(type => type.IndexDataTypeID == 4);
                        rptrAddNewIndexData.DataBind();
                    }
                    else
                    {
                        rptrAddNewIndexData.DataSource = objDBContext.vwManageIndexDatas.OrderByDescending(obj => obj.IndexDataID).Where(usr => usr.RecordStatus == DBKeys.RecordStatus_Active && usr.IndexDataTypeID == 4);
                        rptrAddNewIndexData.DataBind();
                    }
                }
                if (SelectedType == 5)
                {
                    if (chkShowDeleted.Checked)
                    {
                        rptrAddNewIndexData.DataSource = objDBContext.vwManageIndexDatas.OrderByDescending(obj => obj.IndexDataID).Where(type => type.IndexDataTypeID == 5);
                        rptrAddNewIndexData.DataBind();
                    }
                    else
                    {
                        rptrAddNewIndexData.DataSource = objDBContext.vwManageIndexDatas.OrderByDescending(obj => obj.IndexDataID).Where(usr => usr.RecordStatus == DBKeys.RecordStatus_Active && usr.IndexDataTypeID == 5);
                        rptrAddNewIndexData.DataBind();
                    }
                }
                if (SelectedType == 6)
                {
                    if (chkShowDeleted.Checked)
                    {
                        rptrAddNewIndexData.DataSource = objDBContext.vwManageIndexDatas.OrderByDescending(obj => obj.IndexDataID).Where(type => type.IndexDataTypeID == 6);
                        rptrAddNewIndexData.DataBind();
                    }
                    else
                    {
                        rptrAddNewIndexData.DataSource = objDBContext.vwManageIndexDatas.OrderByDescending(obj => obj.IndexDataID).Where(usr => usr.RecordStatus == DBKeys.RecordStatus_Active && usr.IndexDataTypeID == 6);
                        rptrAddNewIndexData.DataBind();
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        #endregion

        #region ReusableRoutines

        private void ClearAddNewIndex()
        {
            txtAddNewIndexVideo.Text = txtAddNewIndexTitle.Text = txtAddNewIndexDescription.Text = string.Empty;
        }

        private void ActivateDeactivateIndexData(int IndexDataStatus)
        {
            try
            {
                foreach (RepeaterItem rItem in rptrAddNewIndexData.Items)
                {
                    CheckBox chkSelect = (rItem.FindControl("chkSelect") as CheckBox);
                    HiddenField hdnIndexDataID = (rItem.FindControl("hdnIndexDataID") as HiddenField);
                    if (chkSelect.Checked)
                    {
                        var selCategory = GetIndexDataFromID(int.Parse(hdnIndexDataID.Value));
                        if (selCategory.IsNotNull())
                        {
                            selCategory.RecordStatus = IndexDataStatus;
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

        private void LoadEditIndexData()
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.EditIndexData].IsValid())
                {
                    mvContainer.ActiveViewIndex = 1;
                    int IndexDataID = int.Parse(Request.QueryString[QueryStringKeys.EditIndexData]);
                    var selIndexData = objDBContext.vwManageIndexDatas.FirstOrDefault(cat => cat.IndexDataID == IndexDataID && cat.RecordStatus != DBKeys.RecordStatus_Delete);
                    int Type = selIndexData.IndexDataTypeID;
                    if (Type == 2)
                    {
                        pnlIndexDataImage.Visible = false;
                        pnlIndexDataVideo.Visible = true;
                    }
                    else if (Type == 3 || Type == 4)
                    {
                        pnlIndexDataImage.Visible = false;
                        pnlIndexDataVideo.Visible = false;
                    }
                    else
                    {
                        pnlIndexDataImage.Visible = true;
                        pnlIndexDataVideo.Visible = false;
                    }
                    if (selIndexData != null)
                    {
                        lblEditCategory.Text = selIndexData.IndexDataTypeText;
                        txtVideoLink.Text = selIndexData.Path;
                        txtTitle.Text = selIndexData.Title;
                        txtDescription.Text = selIndexData.Description;
                        txtEditPriority.Text = Convert.ToString(selIndexData.Priority);
                        //ddlPriority.SelectedValue = Convert.ToString(selIndexData.Priority);
                    }
                    if (selIndexData.Path != null)
                    {
                        hypImageLink.NavigateUrl = selIndexData.Path;
                        hypImageLink.Visible = true;
                    }
                    if(selIndexData.ThumbnailPath != null)
                    {
                        hypThumbnailImageLink.NavigateUrl= selIndexData.ThumbnailPath;
                        hypThumbnailImageLink.Visible=true;
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void ShowPaginatedAndDeletedRecords()
        {
            try
            {
                if (chkShowDeleted.Checked)
                {
                    rptrAddNewIndexData.DataSource = objDBContext.vwManageIndexDatas.OrderByDescending(obj => obj.IndexDataID);
                    rptrAddNewIndexData.DataBind();
                }
                else
                {
                    rptrAddNewIndexData.DataSource = objDBContext.vwManageIndexDatas.OrderByDescending(obj => obj.IndexDataID).Where(usr => usr.RecordStatus == DBKeys.RecordStatus_Active);
                    rptrAddNewIndexData.DataBind();
                }
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
                    int IndexDataID = int.Parse(Request.QueryString[QueryStringKey]);
                    var objToDelete = GetIndexDataFromID(IndexDataID);
                    if (objToDelete.IsNotNull())
                    {
                        objToDelete.RecordStatus = RecordStatus;
                        objDBContext.SaveChanges();
                        ShowPaginatedAndDeletedRecords();
                    }
                    divANIDSuccessMessage.Visible = true;
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private IndexData GetIndexDataFromID(int IndexDataID)
        {
            return objDBContext.IndexDatas.FirstOrDefault(cat => cat.IndexDataID == IndexDataID);
        }
    
        #endregion

    }
}
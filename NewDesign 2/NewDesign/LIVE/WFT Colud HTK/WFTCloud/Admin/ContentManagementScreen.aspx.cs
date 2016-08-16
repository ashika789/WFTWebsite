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
    public partial class ContentManagementScreen : System.Web.UI.Page
    {
        #region Global Variables and Properties
        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        #endregion

        #region PageEvents

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
                            //Check if the screen should delete any CMS from query string parameter.
                            UpdateCMSStatus(QueryStringKeys.Delete, DBKeys.RecordStatus_Delete);
                            //Check if the screen should activate any CMS from query string parameter.
                            UpdateCMSStatus(QueryStringKeys.Activate, DBKeys.RecordStatus_Active);
                            //Check if the screen should deactivate any CMS from query string parameter.
                            UpdateCMSStatus(QueryStringKeys.Deactivate, DBKeys.RecordStatus_Inactive);
                            //Check if the screen should load edit CMS from query string parameter.
                            LoadEditCMS();
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
            catch(Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        #endregion

        #region ControlEvents

        protected void chkShowDeleted_CheckedChanged(object sender, EventArgs e)
        {
            ShowPaginatedAndDeletedRecords();
            divCMSuccessMessage.Visible = false;
        }

        protected void btnActivate_Click(object sender, EventArgs e)
        {
            divCMSuccessMessage.Visible = false;
            ActivateDeactivateCMS(DBKeys.RecordStatus_Active);
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnDeactivate_Click(object sender, EventArgs e)
        {
            divCMSuccessMessage.Visible = false;
            ActivateDeactivateCMS(DBKeys.RecordStatus_Inactive);
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            divCMSuccessMessage.Visible = false;
            ActivateDeactivateCMS(DBKeys.RecordStatus_Delete);
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnSaveCMS_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.EditCMS].IsValid())
                {
                    int CMSID = int.Parse(Request.QueryString[QueryStringKeys.EditCMS]);
                    var selCMS = objDBContext.SitePagesAndContents.FirstOrDefault(cat => cat.PageID == CMSID);
                    if (selCMS != null)
                    {
                        selCMS.PageRelativeUrl = txtPageName.Text;
                        selCMS.PageTitle = txtPageTitle.Text;
                        selCMS.MetaKeywords = txtMetaKeyword.Text;
                        selCMS.MetaDescription = txtMetaDescription.Text;
                        selCMS.ContentZoneName = txtZoneName.Text;
                        string EncodedString;
                        string DecodedString;
                        //selCMS.HTMLContent = HttpUtility.HtmlEncode(txtEditContent.Text);
                        EncodedString = HttpUtility.HtmlEncode(txtEditHtmlContent.Text);
                        DecodedString = HttpUtility.HtmlDecode(EncodedString);
                        selCMS.HTMLContent = DecodedString;
                        selCMS.PreviewHTMLContent = DecodedString;
                        //selCMS.Language = ddlEditLanguage.SelectedValue;
                        objDBContext.SaveChanges();
                        divEditContentSuccess.Visible = true;
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void btnAddNewCMS_Click1(object sender, EventArgs e)
        {
            mvContainer.ActiveViewIndex = 2;
        }

        protected void btnAddNewSaveCMS_Click(object sender, EventArgs e)
        {
            try
            {
                string PageName = txtAddNewPageName.Text;
                string SelectedLanguage = ddlMewLanguage.SelectedValue;
                var CmsExist = objDBContext.SitePagesAndContents.FirstOrDefault(c => c.PageRelativeUrl == PageName && c.Language == SelectedLanguage);
                if (CmsExist != null)
                {
                    CmsExist.PageRelativeUrl = PageName;
                    CmsExist.PageTitle = txtAddNewPageTitle.Text;
                    CmsExist.MetaKeywords = txtAddNewMetaKeyword.Text;
                    CmsExist.MetaDescription = txtAddnewMetaDescription.Text;
                    CmsExist.ContentZoneName = txtAddnewZoneName.Text;
                    string EncodedString = HttpUtility.HtmlEncode(txtAddNewHTMLContent.Text);
                    string DecodedString = HttpUtility.HtmlDecode(EncodedString);
                    CmsExist.HTMLContent = DecodedString;
                    CmsExist.CreatedOn = DateTime.Now;
                    CmsExist.RecordStatus = DBKeys.RecordStatus_Active;
                    CmsExist.Language = SelectedLanguage;
                }
                else
                {
                    SitePagesAndContent AddCMS = new SitePagesAndContent();
                    AddCMS.PageRelativeUrl = PageName;
                    AddCMS.PageTitle = txtAddNewPageTitle.Text;
                    AddCMS.MetaKeywords = txtAddNewMetaKeyword.Text;
                    AddCMS.MetaDescription = txtAddnewMetaDescription.Text;
                    AddCMS.ContentZoneName = txtAddnewZoneName.Text;
                    //AddCMS.HTMLContent = HttpUtility.HtmlEncode(txtAddNewContent.Text); 
                    string EncodedString = HttpUtility.HtmlEncode(txtAddNewHTMLContent.Text);
                    string DecodedString = HttpUtility.HtmlDecode(EncodedString);
                    AddCMS.HTMLContent = DecodedString;
                    AddCMS.CreatedOn = DateTime.Now;
                    AddCMS.RecordStatus = DBKeys.RecordStatus_Active;
                    AddCMS.Language = SelectedLanguage;
                    objDBContext.SitePagesAndContents.AddObject(AddCMS);
                }
                objDBContext.SaveChanges();
                divAddNewCmsSuccess.Visible = true;
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        //protected void btnReset_Click(object sender, EventArgs e)
        //{
        //    txtAddNewPageName.Text = txtAddNewPageTitle.Text = txtAddNewMetaKeyword.Text = txtAddnewMetaDescription.Text = txtAddnewZoneName.Text = string.Empty;
        //}

        #endregion

        #region ReusableRoutines

        private void LoadEditCMS()
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.EditCMS].IsValid())
                {
                    mvContainer.ActiveViewIndex = 1;
                    int CMSID = int.Parse(Request.QueryString[QueryStringKeys.EditCMS]);
                    var selCMS = objDBContext.SitePagesAndContents.FirstOrDefault(cat => cat.PageID == CMSID && cat.RecordStatus != DBKeys.RecordStatus_Delete);
                    if (selCMS != null)
                    {
                        txtPageName.Text = selCMS.PageRelativeUrl;
                        txtPageTitle.Text = selCMS.PageTitle;
                        //txtEditContent.Text =HttpUtility.HtmlDecode(selCMS.HTMLContent);
                        txtEditHtmlContent.Text = selCMS.HTMLContent;
                        txtMetaKeyword.Text = selCMS.MetaKeywords;
                        txtMetaDescription.Text = selCMS.MetaDescription;
                        txtZoneName.Text = selCMS.ContentZoneName;
                        //ddlEditLanguage.SelectedValue = selCMS.Language;
                        lblEditLanguage.Text = selCMS.Language;
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void ActivateDeactivateCMS(int CMSStatus)
        {
            try
            {
                foreach (RepeaterItem rItem in rptrContentManagement.Items)
                {
                    CheckBox chkSelect = (rItem.FindControl("chkSelect") as CheckBox);
                    HiddenField hdnCMSID = (rItem.FindControl("hdnCMSID") as HiddenField);
                    if (chkSelect.Checked)
                    {
                        var selCMS = GetCMSFromID(int.Parse(hdnCMSID.Value));
                        if (selCMS.IsNotNull())
                        {
                            selCMS.RecordStatus = CMSStatus;
                            divCMSuccessMessage.Visible = true;
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

        private void UpdateCMSStatus(string QueryStringKey, int RecordStatus)
        {
            try
            {
                //If redirected for delete then do delete
                if (Request.QueryString[QueryStringKey].IsValid())
                {
                    int CMSID = int.Parse(Request.QueryString[QueryStringKey]);
                    var objToDelete = GetCMSFromID(CMSID);
                    if (objToDelete.IsNotNull())
                    {
                        objToDelete.RecordStatus = RecordStatus;
                        objDBContext.SaveChanges();
                        ShowPaginatedAndDeletedRecords();
                    }
                    divCMSuccessMessage.Visible = true;
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private SitePagesAndContent GetCMSFromID(int CMSID)
        {
            return objDBContext.SitePagesAndContents.FirstOrDefault(cat => cat.PageID == CMSID);
        }
    
        private void ShowPaginatedAndDeletedRecords()
        {
            try
            {
                if (chkShowDeleted.Checked)
                {
                    rptrContentManagement.DataSource = objDBContext.SitePagesAndContents.OrderBy(obj => obj.PageTitle);
                    rptrContentManagement.DataBind();
                }
                else
                {
                    rptrContentManagement.DataSource = objDBContext.SitePagesAndContents.OrderBy(obj => obj.PageTitle).Where(pc => pc.RecordStatus == DBKeys.RecordStatus_Active);
                    rptrContentManagement.DataBind();
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        #endregion

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.EditCMS].IsValid())
                {
                    int CMSID = int.Parse(Request.QueryString[QueryStringKeys.EditCMS]);
                    var selCMS = objDBContext.SitePagesAndContents.FirstOrDefault(cat => cat.PageID == CMSID);
                    if (selCMS != null)
                    {
                        string EncodedString = HttpUtility.HtmlEncode(txtEditHtmlContent.Text);
                        string DecodedString = HttpUtility.HtmlDecode(EncodedString);
                        selCMS.PreviewHTMLContent = DecodedString;
                        objDBContext.SaveChanges();
                        //Response.Redirect("/Admin/PreviewContent.aspx?previewid=" + CMSID, false);
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('/Admin/PreviewContent.aspx?previewid=" + CMSID + "', '_blank');", true);
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void btnAddNewPreview_Click(object sender, EventArgs e)
        {
            try
            {
                string PageName = txtAddNewPageName.Text;
                string Language = ddlMewLanguage.SelectedValue;
                var CmsExist = objDBContext.SitePagesAndContents.FirstOrDefault(c => c.PageRelativeUrl == PageName && c.Language == Language);
                if (CmsExist != null)
                {
                    string EncodedString = HttpUtility.HtmlEncode(txtAddNewHTMLContent.Text);
                    string DecodedString = HttpUtility.HtmlDecode(EncodedString);
                    CmsExist.PreviewHTMLContent = DecodedString;
                    CmsExist.HTMLContent = "";
                    CmsExist.RecordStatus = DBKeys.RecordStatus_Delete;
                }
                else
                {
                    SitePagesAndContent AddCMS = new SitePagesAndContent();
                    AddCMS.PageRelativeUrl = PageName;
                    string EncodedString = HttpUtility.HtmlEncode(txtAddNewHTMLContent.Text);
                    string DecodedString = HttpUtility.HtmlDecode(EncodedString);
                    AddCMS.PreviewHTMLContent = DecodedString;
                    AddCMS.HTMLContent = "";
                    AddCMS.RecordStatus = DBKeys.RecordStatus_Delete;
                    AddCMS.Language = Language;
                    objDBContext.SitePagesAndContents.AddObject(AddCMS);
                }
                objDBContext.SaveChanges();
                int CMSID = objDBContext.SitePagesAndContents.FirstOrDefault(cms => cms.PageRelativeUrl == PageName && cms.Language == Language).PageID;
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('/Admin/PreviewContent.aspx?previewid=" + CMSID + "', '_blank');", true);
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

    }
}
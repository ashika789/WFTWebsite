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
using System.IO;

namespace WFTCloud.Admin
{
    public partial class ManageFAQ : System.Web.UI.Page
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
                             //Check if the screen should load edit category from query string parameter.
                             LoadEditFAQ();
                             //Show Manage FAQ records based on pagination value and deleted flag.
                             ShowPaginatedAndDeletedRecords();
                             //Check if the screen should delete any category from query string parameter.
                             UpdateManageFAQStatus(QueryStringKeys.Delete, DBKeys.RecordStatus_Delete);
                             //Check if the screen should activate any category from query string parameter.
                             UpdateManageFAQStatus(QueryStringKeys.Activate, DBKeys.RecordStatus_Active);
                             //Check if the screen should deactivate any category from query string parameter.
                             UpdateManageFAQStatus(QueryStringKeys.Deactivate, DBKeys.RecordStatus_Inactive);
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

        private void LoadEditFAQ()
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.EditFAQ].IsValid())
                {
                    mvContainer.ActiveViewIndex = 1;
                    lblHeading.Text = "New Frequently Asked Question";
                    int FAQID = int.Parse(Request.QueryString[QueryStringKeys.EditFAQ]);
                    var selFAQ = objDBContext.FAQs.FirstOrDefault(cat => cat.FAQID == FAQID && cat.RecordStatus != DBKeys.RecordStatus_Delete);
                    if (selFAQ != null)
                    {
                        pnlLanguage.Visible = false;
                        txtQuestion.Text = selFAQ.Question;
                        txtAnswer.Text = selFAQ.Answer;
                        ddlPriority.SelectedValue = Convert.ToString(selFAQ.Priority);
                        LoadUserTypeAndCategory();
                        ddlCategory.SelectedValue = Convert.ToString(selFAQ.FAQCategoryTypeID);
                        ddlUserType.SelectedValue = Convert.ToString(selFAQ.FAQTypeID);
                        //ddlLanguage.SelectedValue = selFAQ.Language;
                        lblHeading.Text = "Edit Frequently Asked Question";
                    }
                    else
                    {
                        LoadUserTypeAndCategory();
                        pnlLanguage.Visible = true;
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void chkShowDeleted_CheckedChanged(object sender, EventArgs e)
        {
            ShowPaginatedAndDeletedRecords();
            divMFAQSuccessMessage.Visible = false;
        }

        protected void btnActivate_Click(object sender, EventArgs e)
        {
            divMFAQSuccessMessage.Visible = false;
            ActivateDeactivateCategories(DBKeys.RecordStatus_Active);
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnDeactivate_Click(object sender, EventArgs e)
        {
            divMFAQSuccessMessage.Visible = false;
            ActivateDeactivateCategories(DBKeys.RecordStatus_Inactive);
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            divMFAQSuccessMessage.Visible = false;
            ActivateDeactivateCategories(DBKeys.RecordStatus_Delete);
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnAddFAQ_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.EditFAQ].IsValid())
                {
                    int FAQID = int.Parse(Request.QueryString[QueryStringKeys.EditFAQ]);
                    var selFAQ = objDBContext.FAQs.FirstOrDefault(cat => cat.FAQID == FAQID);
                    if (selFAQ == null)
                    {
                        selFAQ = new FAQ();
                        selFAQ.Answer = txtAnswer.Text;
                        selFAQ.Question = txtQuestion.Text;
                        selFAQ.FAQTypeID = Convert.ToInt32(ddlUserType.SelectedValue);
                        selFAQ.FAQCategoryTypeID = Convert.ToInt32(ddlCategory.SelectedValue);
                        selFAQ.Priority = Convert.ToInt32(ddlPriority.SelectedValue);
                        selFAQ.Language = ddlLanguage.SelectedValue;
                        objDBContext.FAQs.AddObject(selFAQ);
                        objDBContext.SaveChanges();
                        divEditFAQ.Visible = true;
                        divErrorMessage.Visible = false;
                    }
                    else
                    {
                        selFAQ.Answer = txtAnswer.Text;
                        selFAQ.Question = txtQuestion.Text;
                        selFAQ.FAQTypeID = Convert.ToInt32(ddlUserType.SelectedValue);
                        selFAQ.FAQCategoryTypeID = Convert.ToInt32(ddlCategory.SelectedValue);
                        selFAQ.Priority = Convert.ToInt32(ddlPriority.SelectedValue);
                        //selFAQ.Language = ddlLanguage.SelectedValue;
                        selFAQ.RecordStatus = DBKeys.RecordStatus_Active;
                        objDBContext.SaveChanges();
                        divErrorMessage.Visible = false;
                        divEditFAQ.Visible = true;
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

        private void LoadUserTypeAndCategory()
        {
            try
            {
                ddlUserType.DataSource = objDBContext.FAQTypes.OrderBy(u => u.FAQTypeText);
                ddlUserType.DataTextField = "FAQTypeText";
                ddlUserType.DataValueField = "FAQTypeID";
                ddlUserType.DataBind();
                ddlCategory.DataSource = objDBContext.FAQCategoryTypes.OrderBy(c => c.FAQCategoryName);
                ddlCategory.DataTextField = "FAQCategoryName";
                ddlCategory.DataValueField = "FAQCategoryID";
                ddlCategory.DataBind();
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void ActivateDeactivateCategories(int FAQStatus)
        {
            try
            {
                foreach (RepeaterItem rItem in rptrManageFAQ.Items)
                {
                    CheckBox chkSelect = (rItem.FindControl("chkSelect") as CheckBox);
                    HiddenField hdnFAQID = (rItem.FindControl("hdnFAQID") as HiddenField);
                    if (chkSelect.Checked)
                    {
                        var selFAQ = GetFAQFromID(int.Parse(hdnFAQID.Value));
                        if (selFAQ.IsNotNull())
                        {
                            selFAQ.RecordStatus = FAQStatus;
                            divMFAQSuccessMessage.Visible = true;
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

        private void UpdateManageFAQStatus(string QueryStringKey, int RecordStatus)
        {
            try
            {
                //If redirected for delete then do delete
                if (Request.QueryString[QueryStringKey].IsValid())
                {
                    int FAQID = int.Parse(Request.QueryString[QueryStringKey]);
                    var objToDelete = GetFAQFromID(FAQID);
                    if (objToDelete.IsNotNull())
                    {
                        objToDelete.RecordStatus = RecordStatus;
                        objDBContext.SaveChanges();
                        ShowPaginatedAndDeletedRecords();
                    }

                    divMFAQSuccessMessage.Visible = true;
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private FAQ GetFAQFromID(int FAQID)
        {
            return objDBContext.FAQs.FirstOrDefault(cat => cat.FAQID == FAQID);
        }

        private void ShowPaginatedAndDeletedRecords()
        {
            try
            {
                if (chkShowDeleted.Checked)
                {
                    rptrManageFAQ.DataSource = objDBContext.vwManageFAQs.OrderBy(obj => obj.Priority);
                    rptrManageFAQ.DataBind();
                }
                else
                {
                    rptrManageFAQ.DataSource = objDBContext.vwManageFAQs.OrderBy(obj => obj.Priority).Where(usr => usr.RecordStatus == DBKeys.RecordStatus_Active);
                    rptrManageFAQ.DataBind();
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        #endregion

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Admin/ManageFAQ.aspx?editfaqid=0");
        }
    }
}
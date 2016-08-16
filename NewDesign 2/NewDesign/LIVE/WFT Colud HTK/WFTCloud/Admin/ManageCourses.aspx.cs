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
    public partial class ManageCourses : System.Web.UI.Page
    {
        #region Global Variables and Properties
        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //Show records based on pagination value and deleted flag.
                    ShowPaginatedAndDeletedRecords();
                    //Check if the screen should delete any userprofile from query string parameter.
                    UpdateCourseDetailsStatus(QueryStringKeys.Delete, DBKeys.RecordStatus_Delete);
                    //Check if the screen should activate any category from query string parameter.
                    UpdateCourseDetailsStatus(QueryStringKeys.Activate, DBKeys.RecordStatus_Active);
                    //Check if the screen should deactivate any category from query string parameter.
                    UpdateCourseDetailsStatus(QueryStringKeys.Deactivate, DBKeys.RecordStatus_Inactive);
                    //Check if the screen should load edit category from query string parameter.
                    LoadEditCourses();
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        #endregion

        #region ControlEvents

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.EditCourseDetails].IsValid())
                {
                    int CourseId = int.Parse(Request.QueryString[QueryStringKeys.EditCourseDetails]);
                    var selCourses = objDBContext.CourseDetails.FirstOrDefault(cat => cat.CourseID == CourseId);
                    if (selCourses != null)
                    {
                        selCourses.CourseName = txtCourseName.Text;
                        selCourses.Description = txtDescription.Text;
                        selCourses.CourseDuration = txtCourseDuration.Text;
                        selCourses.Opportunities = txtOpportunities.Text;
                        selCourses.Status = Convert.ToInt32(ddlStatus.SelectedValue);
                        objDBContext.SaveChanges();
                        lblSuccessMessage.Visible = true;
                        divMCDErrorMessage.Visible = false;
                    }
                    else
                    {
                        divMCDErrorMessage.Visible = true;
                        lblErrorMessageText.Text = "There are Courses available.";
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                divMCDErrorMessage.Visible = true;
                lblErrorMessageText.Text = "Error while editing course details.";
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            LoadEditCourses();
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                CourseDetail  selCourses = new CourseDetail();
                selCourses.CourseName = txtAddCourseName.Text;
                selCourses.Description = txtAddDescription.Text;
                selCourses.CourseDuration = txtAddCourseDuration.Text;
                selCourses.Opportunities = txtAddOpportunities.Text;
                selCourses.Status = Convert.ToInt32(ddlAddStatus.SelectedValue);
                objDBContext.CourseDetails.AddObject(selCourses);
                objDBContext.SaveChanges();
                lblSuccessMsg.Visible = true;
                lblErrorMsg.Visible = false;
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                lblSuccessMessage.Visible = false;
                divMCDErrorMessage.Visible = true;
                lblErrorMsgText.Text = "Error while adding new Course Details";
            }
        }

        protected void btnAddReset_Click(object sender, EventArgs e)
        {
            txtAddCourseName.Text = txtAddDescription.Text = txtAddCourseDuration.Text = txtAddOpportunities.Text = string.Empty;
        }

        protected void btnAddNewCourseDetails_Click(object sender, EventArgs e)
        {
            mvContainer.ActiveViewIndex = 2;
            LoadStatus();
        }

        protected void chkShowDeActivated_CheckedChanged(object sender, EventArgs e)
        {
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnActivate_Click(object sender, EventArgs e)
        {
            ActivateDeactivateCourses(DBKeys.RecordStatus_Active);
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnDeactivate_Click(object sender, EventArgs e)
        {
            ActivateDeactivateCourses(DBKeys.RecordStatus_Inactive);
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ActivateDeactivateCourses(DBKeys.RecordStatus_Delete);
            ShowPaginatedAndDeletedRecords();
        }

        #endregion


        #region ReusableRoutines

        private void ShowPaginatedAndDeletedRecords()
        {
            try
            {
                if (chkShowDeActivated.Checked)
                {
                    rptrCourseDetails.DataSource = objDBContext.CourseDetails.OrderByDescending(obj => obj.CourseID);
                    rptrCourseDetails.DataBind();
                }
                else
                {
                    rptrCourseDetails.DataSource = objDBContext.CourseDetails.OrderByDescending(obj => obj.CourseID).Where(usr => usr.Status == DBKeys.RecordStatus_Active);
                    rptrCourseDetails.DataBind();
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void LoadEditCourses()
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.EditCourseDetails].IsValid())
                {
                    mvContainer.ActiveViewIndex = 1;
                    int CourseId = int.Parse(Request.QueryString[QueryStringKeys.EditCourseDetails]);
                    var selCourse = objDBContext.CourseDetails.FirstOrDefault(cat => cat.CourseID == CourseId);
                    if (selCourse != null)
                    {
                        LoadStatus();
                        txtCourseName.Text = selCourse.CourseName;
                        txtDescription.Text = selCourse.Description;
                        txtCourseDuration.Text = selCourse.CourseDuration;
                        txtOpportunities.Text = selCourse.Opportunities;
                        ddlStatus.SelectedValue = Convert.ToString(selCourse.Status);
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
                ddlStatus.DataSource = objDBContext.RecordStatus.OrderBy(a => a.RecordStatusDesc);
                ddlStatus.DataTextField = "RecordStatusDesc";
                ddlStatus.DataValueField = "RecordStatusID";
                ddlStatus.DataBind();
                ddlAddStatus.DataSource = objDBContext.RecordStatus.OrderBy(a => a.RecordStatusDesc);
                ddlAddStatus.DataTextField = "RecordStatusDesc";
                ddlAddStatus.DataValueField = "RecordStatusID";
                ddlAddStatus.DataBind();
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void ActivateDeactivateCourses(int CourseStatus)
        {
            try
            {
                foreach (RepeaterItem rItem in rptrCourseDetails.Items)
                {
                    CheckBox chkSelect = (rItem.FindControl("chkSelect") as CheckBox);
                    HiddenField hdnCourseDetailId = (rItem.FindControl("hdnCourseDetailId") as HiddenField);
                    if (chkSelect.Checked)
                    {
                        var course = GetDetailsfromID(int.Parse(hdnCourseDetailId.Value));
                        if (course.IsNotNull())
                        {
                            course.Status = CourseStatus;
                            divMCDSuccessMessage.Visible = true;
                        }
                    }
                    else
                    {
                        divMCDSuccessMessage.Visible = false;
                    }
                }
                objDBContext.SaveChanges();
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void UpdateCourseDetailsStatus(string QueryStringKey, int RecordStatus)
        {
            try
            {
                ////If redirected for delete then do delete
                if (Request.QueryString[QueryStringKey].IsValid())
                {
                    int CourseID = int.Parse(Request.QueryString[QueryStringKey]);
                    var objToDelete = GetDetailsfromID(CourseID);
                    if (objToDelete.IsNotNull())
                    {
                        objToDelete.Status = RecordStatus;
                        objDBContext.SaveChanges();
                        ShowPaginatedAndDeletedRecords();
                    }
                    divMCDSuccessMessage.Visible = true;
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private CourseDetail GetDetailsfromID(int CourseId)
        {
            return objDBContext.CourseDetails.FirstOrDefault(cat => cat.CourseID == CourseId);
        }

        #endregion
    }
}
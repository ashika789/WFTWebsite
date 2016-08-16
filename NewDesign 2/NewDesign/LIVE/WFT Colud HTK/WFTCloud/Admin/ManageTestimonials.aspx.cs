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
    public partial class ManageTestimonials : System.Web.UI.Page
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
                    UpdateTestimonialStatus(QueryStringKeys.Delete, DBKeys.RecordStatus_Delete);
                    //Check if the screen should activate any category from query string parameter.
                    UpdateTestimonialStatus(QueryStringKeys.Activate, DBKeys.RecordStatus_Active);
                    //Check if the screen should deactivate any category from query string parameter.
                    UpdateTestimonialStatus(QueryStringKeys.Deactivate, DBKeys.RecordStatus_Inactive);
                    //Check if the screen should load edit category from query string parameter.
                    LoadEditTestimonial();
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        #endregion

        #region ControlEvents

        protected void btnAddNewTestimonials_Click(object sender, EventArgs e)
        {
            try
            {
                int count = objDBContext.Testimonials.Count();
                ddlAddPriority.DataSource = Enumerable.Range(1, count + 1);
                ddlAddPriority.DataBind();
                mvContainer.ActiveViewIndex = 2;
                LoadStatus();
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.EditTestimonials].IsValid())
                {
                    int TestId = int.Parse(Request.QueryString[QueryStringKeys.EditTestimonials]);
                    var selTest = objDBContext.Testimonials.FirstOrDefault(cat => cat.TestimonialID == TestId);
                    if (selTest != null)
                    {
                        selTest.CustomerName = txtEditCustomerName.Text;
                        selTest.Designation = txtEditDesignation.Text;
                        selTest.CustOrg = txtEditCustOrg.Text;
                        selTest.CustSince = Convert.ToDateTime(txtEditCustSince.Text);
                        selTest.NoOfDedicatedSystems = txtEditDedicateSystems.Text;
                        selTest.Usage = txtEditUsage.Text;
                        selTest.Testimonial1 = txtTestimoinal.Text;
                        selTest.HomePageVersion = txtHomePageVersion.Text;
                        selTest.Priority = Convert.ToInt32(ddlPriority.SelectedValue);
                        selTest.RecordStatus = Convert.ToInt32(ddlRecordStatus.SelectedValue);
                        objDBContext.SaveChanges();
                        lblSuccessMessage.Visible = true;
                        divMTErrorMessage.Visible = false;
                    }
                    else
                    {
                        divMTErrorMessage.Visible = true;
                        lblErrorMessageText.Text = "There are Testimonials available.";
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                divMTErrorMessage.Visible = true;
                lblErrorMessageText.Text = "Error while editing Testimonial Informations.";
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            LoadEditTestimonial();
            lblSuccessMessage.Visible = lblErrorMessage.Visible = false;
        }

        protected void chkShowDeleted_CheckedChanged(object sender, EventArgs e)
        {
            ShowPaginatedAndDeletedRecords();
            lblSuccessMessage.Visible = false;
            divMTSuccessMessage.Visible = false;
        }

        protected void btnActivate_Click(object sender, EventArgs e)
        {
            ActivateDeactivateTestimonials(DBKeys.RecordStatus_Active);
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnDeactivate_Click(object sender, EventArgs e)
        {
            ActivateDeactivateTestimonials(DBKeys.RecordStatus_Inactive);
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ActivateDeactivateTestimonials(DBKeys.RecordStatus_Delete);
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnAddSave_Click(object sender, EventArgs e)
        {
            try
            {
                Testimonial selTest = new  Testimonial();
                selTest.CustomerName = txtAddCustName.Text;
                selTest.Designation = txtAddDesignation.Text;
                selTest.CustOrg = txtAddCustOrg.Text;
                selTest.CustSince =Convert.ToDateTime(txtAddCustomerSince.Text);
                selTest.NoOfDedicatedSystems = txtAddDedicatedSystems.Text;
                selTest.Usage = txtAddUsage.Text;
                selTest.Testimonial1 = txtAddTestimonial.Text;
                selTest.HomePageVersion = txtNewHomePageVersion.Text;
                selTest.Priority = Convert.ToInt32(ddlAddPriority.SelectedValue);
                selTest.RecordStatus = Convert.ToInt32(ddlAddRecordStatus.SelectedValue);
                objDBContext.Testimonials.AddObject(selTest);
                objDBContext.SaveChanges();
                lblSuccessMsg.Visible = true;
                lblErrorMsg.Visible = false;
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                lblSuccessMsg.Visible = false;
                lblErrorMsg.Visible = true;
                lblErrorMsgText.Text = "Error while adding new Testimonial Informations";
            }
        }

        protected void btnAddReset_Click(object sender, EventArgs e)
        {
            txtAddTestimonial.Text = txtNewHomePageVersion.Text = string.Empty;
            lblSuccessMsg.Visible =lblErrorMsg.Visible= false;
        }

        #endregion

        #region ReusableRoutines

        private void ShowPaginatedAndDeletedRecords()
        {
            try
            {
                if (chkShowDeleted.Checked)
                {
                    rptrTestimonials.DataSource = objDBContext.Testimonials.OrderByDescending(obj => obj.TestimonialID);
                    rptrTestimonials.DataBind();
                }
                else
                {
                    rptrTestimonials.DataSource = objDBContext.Testimonials.OrderByDescending(obj => obj.TestimonialID).Where(usr => usr.RecordStatus == DBKeys.RecordStatus_Active);
                    rptrTestimonials.DataBind();
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void LoadEditTestimonial()
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.EditTestimonials].IsValid())
                {
                    mvContainer.ActiveViewIndex = 1;
                    int TestID = int.Parse(Request.QueryString[QueryStringKeys.EditTestimonials]);
                    var selTest = objDBContext.Testimonials.FirstOrDefault(cat => cat.TestimonialID == TestID);
                    if (selTest != null)
                    {
                        LoadStatus();
                        int count = objDBContext.Testimonials.Count();
                        ddlPriority.DataSource = Enumerable.Range(0, count + 1);
                        ddlPriority.DataBind();
                        txtEditCustomerName.Text = selTest.CustomerName;
                        txtEditDesignation.Text =selTest.Designation;
                        txtEditCustOrg.Text = selTest.CustOrg;
                        txtEditCustSince.Text =Convert.ToDateTime(selTest.CustSince).ToString("dd-MMM-yyyy");
                        txtEditDedicateSystems.Text = selTest.NoOfDedicatedSystems;
                        txtEditUsage.Text = selTest.Usage;

                        txtTestimoinal.Text = selTest.Testimonial1;
                        txtHomePageVersion.Text = selTest.HomePageVersion;
                        ddlPriority.SelectedValue = Convert.ToString(selTest.Priority);
                        ddlRecordStatus.SelectedValue = Convert.ToString(selTest.RecordStatus);
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
                ddlRecordStatus.DataSource = objDBContext.RecordStatus.OrderBy(a => a.RecordStatusDesc);
                ddlRecordStatus.DataTextField = "RecordStatusDesc";
                ddlRecordStatus.DataValueField = "RecordStatusID";
                ddlRecordStatus.DataBind();
                //ddlPriority.DataSource = objDBContext.Testimonials.OrderBy(c => c.TestimonialID);
                //ddlPriority.DataTextField = "Priority";
                //ddlPriority.DataValueField = "TestimonialID";
                //ddlPriority.DataBind();
                ddlAddRecordStatus.DataSource = objDBContext.RecordStatus.OrderBy(a => a.RecordStatusDesc);
                ddlAddRecordStatus.DataTextField = "RecordStatusDesc";
                ddlAddRecordStatus.DataValueField = "RecordStatusID";
                ddlAddRecordStatus.DataBind();
                //ddlAddPriority.DataSource = objDBContext.Testimonials.OrderBy(d => d.TestimonialID);
                //ddlAddPriority.DataTextField = "Priority";
                //ddlAddPriority.DataValueField = "TestimonialID";
                //ddlAddPriority.DataBind();
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void ActivateDeactivateTestimonials(int TestimonialStatus)
        {
            try
            {
                foreach (RepeaterItem rItem in rptrTestimonials.Items)
                {
                    CheckBox chkSelect = (rItem.FindControl("chkSelect") as CheckBox);
                    HiddenField hdnTestimonialsId = (rItem.FindControl("hdnTestimonialsId") as HiddenField);
                    if (chkSelect.Checked)
                    {
                        var testimonials = GetDetailsfromID(int.Parse(hdnTestimonialsId.Value));
                        if (testimonials.IsNotNull())
                        {
                            testimonials.RecordStatus = TestimonialStatus;
                            divMTSuccessMessage.Visible = true;
                        }
                    }
                    else
                    {
                        divMTSuccessMessage.Visible = false;
                    }
                }
                objDBContext.SaveChanges();
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private Testimonial GetDetailsfromID(int TestID)
        {
            return objDBContext.Testimonials.FirstOrDefault(cat => cat.TestimonialID == TestID);
        }

        private void UpdateTestimonialStatus(string QueryStringKey, int RecordStatus)
        {
            try
            {
                ////If redirected for delete then do delete
                if (Request.QueryString[QueryStringKey].IsValid())
                {
                    int TestimonialID = int.Parse(Request.QueryString[QueryStringKey]);
                    var objToDelete = GetDetailsfromID(TestimonialID);
                    if (objToDelete.IsNotNull())
                    {
                        objToDelete.RecordStatus = RecordStatus;
                        objDBContext.SaveChanges();
                        ShowPaginatedAndDeletedRecords();
                    }
                    divMTSuccessMessage.Visible = true;
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
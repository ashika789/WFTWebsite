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
using System.Configuration;

namespace WFTCloud.Admin
{
    public partial class ManageTrainingDetails : System.Web.UI.Page
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
                    UpdateTrainingDetailsStatus(QueryStringKeys.Delete, DBKeys.RecordStatus_Delete);
                    //Check if the screen should activate any category from query string parameter.
                    UpdateTrainingDetailsStatus(QueryStringKeys.Activate, DBKeys.RecordStatus_Active);
                    //Check if the screen should deactivate any category from query string parameter.
                    UpdateTrainingDetailsStatus(QueryStringKeys.Deactivate, DBKeys.RecordStatus_Inactive);
                    //Add Trainee From Visitor
                    AddTraineeFromVisitor();
                    //Check if the screen should load edit category from query string parameter.
                    EditTraining();

                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        #endregion

        #region ControlEvents

        protected void chkShowDeActivated_CheckedChanged(object sender, EventArgs e)
        {
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnActivate_Click(object sender, EventArgs e)
        {
            ActivateDeactivateTraining(DBKeys.RecordStatus_Active);
            ShowPaginatedAndDeletedRecords();
        }


        protected void btnDeactivate_Click(object sender, EventArgs e)
        {
            ActivateDeactivateTraining(DBKeys.RecordStatus_Inactive);
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ActivateDeactivateTraining(DBKeys.RecordStatus_Delete);
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.EditTrainingDetails].IsValid())
                {
                    int TrainingID = int.Parse(Request.QueryString[QueryStringKeys.EditTrainingDetails]);
                    var selTraining = objDBContext.TrainingDetails.FirstOrDefault(cat => cat.TrainnerID == TrainingID);
                    if (selTraining != null)
                    {
                        string ImageURL = "";
                        
                        if (fluTraineeImage.HasFile)
                        {
                            string type = fluTraineeImage.FileName.Substring(fluTraineeImage.FileName.LastIndexOf('.') + 1).ToLower();
                            if (type == "jpg" || type == "jpeg" || type == "png")
                            {
                                string ImageName = DateTime.Now.ToString("ddMMyyyymmssfff");
                                string ImageFullPath = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/IpadImages/");
                               // string presentimagePath = Server.MapPath(selTraining.ImgPath);
                                if (!Directory.Exists(ImageFullPath))
                                {
                                    Directory.CreateDirectory(ImageFullPath);
                                }
                               
                                fluTraineeImage.SaveAs(ImageFullPath + ImageName + ".jpg");
                                ImageURL = "/UploadedContents/IpadImages/" + ImageName + ".jpg";
                            }
                            else
                            {
                                lblSuccessMessage.Visible = false;
                                lblErrorMessage.Visible = true;
                                lblTrainingDetailsError.Text = "Please enter valid image file of the type *.jpg / *.jpeg / *.png";
                                return;
                            }
                        }
                        selTraining.FirstName = txtFirstName.Text;
                        selTraining.LastName = txtLastName.Text;
                        selTraining.Email = txtEmailID.Text;
                        selTraining.PhoneNumber = txtPhoneNumber.Text;
                        selTraining.PermanentAddress = txtPermanentAddress.Text;
                        selTraining.TemporaryAddress = txtTemporaryAddress.Text;
                        selTraining.CourseOfInterest = Convert.ToInt32(ddlCourseOfInterest.SelectedValue);
                        selTraining.KnowAboutUs = Convert.ToInt32(ddlKnowAboutUs.SelectedValue);
                        selTraining.Others = txtOthers.Text;
                        selTraining.UGCollege = txtUGCollege.Text;
                        selTraining.UGPercentage = Convert.ToDecimal(txtUGPercentage.Text);
                        selTraining.PGCollege = txtPGCollege.Text;
                        selTraining.PGPercentage = Convert.ToDecimal(txtPGPercentage.Text);
                        selTraining.OQCollege = txtOQCollege.Text;
                        if(txtOQPercentage.Text.Trim() != "")
                         selTraining.OQPercentage = Convert.ToDecimal(txtOQPercentage.Text);

                        selTraining.YearOfExperience = Convert.ToString(txtYearOfExperience.Text);
                        selTraining.CurrentCompany = txtCurrentCompany.Text;
                        selTraining.TechnologyCurrentlyWork = txtTechnology.Text;
                        selTraining.Status = Convert.ToInt32(ddlStatus.SelectedValue);
                        if (ImageURL != "")
                            selTraining.ImgPath = ImageURL;
                        objDBContext.SaveChanges();
                        lblErrorMessage.Visible = false;
                        lblSuccessMessage.Visible = true;
                        EditTraining();
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                divMTDSuccessMsg.Visible = false;
                lblErrorMessage.Visible = true;
                lblTrainingDetailsError.Text = "Errors Occured while saving training details";
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            EditTraining();
        }

        #endregion

        #region ReusableRoutines

        private void EditTraining()
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.EditTrainingDetails].IsValid())
                {
                    mvContainer.ActiveViewIndex = 1;
                    int TrainingID = int.Parse(Request.QueryString[QueryStringKeys.EditTrainingDetails]);
                    var selTraining = objDBContext.TrainingDetails.FirstOrDefault(cat => cat.TrainnerID == TrainingID);
                    if (selTraining != null)
                    {
                        LoadStatus();
                        txtFirstName.Text = selTraining.FirstName;
                        txtLastName.Text = selTraining.LastName;
                        txtEmailID.Text = selTraining.Email;
                        txtPhoneNumber.Text = selTraining.PhoneNumber;
                        txtPermanentAddress.Text = selTraining.PermanentAddress;
                        txtTemporaryAddress.Text = selTraining.TemporaryAddress;
                        ddlCourseOfInterest.SelectedValue = Convert.ToString(selTraining.CourseOfInterest);
                        ddlKnowAboutUs.SelectedValue = Convert.ToString(selTraining.KnowAboutUs);
                        txtOthers.Text = selTraining.Others;
                        txtUGCollege.Text = selTraining.UGCollege;
                        txtUGPercentage.Text = Convert.ToString(selTraining.UGPercentage);
                        txtPGCollege.Text = selTraining.PGCollege;
                        txtPGPercentage.Text = Convert.ToString(selTraining.PGPercentage);
                        txtOQCollege.Text = selTraining.OQCollege;
                        txtOQPercentage.Text = Convert.ToString(selTraining.OQPercentage);
                        txtYearOfExperience.Text = Convert.ToString(selTraining.YearOfExperience);
                        txtCurrentCompany.Text = selTraining.CurrentCompany;
                        txtTechnology.Text = selTraining.TechnologyCurrentlyWork;
                        ddlStatus.SelectedValue = Convert.ToString(selTraining.Status);
                        if (selTraining.ImgPath != null && selTraining.ImgPath != "")
                        {
                            imgTraineeImg.ImageUrl = selTraining.ImgPath;
                            imgTraineeImg.Visible = true; 
                        }
                        else
                        {
                            imgTraineeImg.Visible = false;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void AddTraineeFromVisitor()
        {
            try
            {
                if (Request.QueryString["VisitorID"].IsValid())
                {
                    
                    int VisitorID = int.Parse(Request.QueryString["VisitorID"]);
                    var trainnerdetails = objDBContext.TrainingDetails.FirstOrDefault(c => c.VisitorID == VisitorID);
                    var selVisitor = objDBContext.VisitorsDetails.FirstOrDefault(cat => cat.VisitorID == VisitorID);
                    if (selVisitor != null && trainnerdetails== null)
                    {
                        mvContainer.ActiveViewIndex = 2;
                        divNewError.Visible = divNewSuccess.Visible = false;
                        LoadStatus();
                        txtNewFirstName.Text = selVisitor.FirstName;
                        txtNewLastName.Text = selVisitor.LastName;
                        txtNewEmailID.Text = selVisitor.Email;
                        txtNewPhoneNumber.Text = selVisitor.PhoneNumber;
                        ddlNewStatus.SelectedValue = Convert.ToString(selVisitor.Status);
                        if (selVisitor.ImgPath != null && selVisitor.ImgPath != "")
                        {
                            imgNewTraineeImage.ImageUrl = selVisitor.ImgPath;
                            imgNewTraineeImage.Visible = true;
                        }
                        else
                        {
                            imgNewTraineeImage.Visible = false;
                        }
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
                ddlStatus.DataSource = ddlNewStatus.DataSource = objDBContext.RecordStatus.OrderBy(a => a.RecordStatusDesc);
                ddlStatus.DataTextField = ddlNewStatus.DataTextField = "RecordStatusDesc";
                ddlStatus.DataValueField = ddlNewStatus.DataValueField = "RecordStatusID";
                ddlStatus.DataBind();
                ddlNewStatus.DataBind();
                ddlCourseOfInterest.DataSource = ddlNewCourseOfInterest.DataSource = objDBContext.CourseDetails.OrderBy(c => c.CourseName).Where(c=>c.Status ==1);
                ddlCourseOfInterest.DataTextField = ddlNewCourseOfInterest.DataTextField = "CourseName";
                ddlCourseOfInterest.DataValueField = ddlNewCourseOfInterest.DataValueField = "CourseID";

                ddlCourseOfInterest.DataBind();
                ddlNewCourseOfInterest.DataBind();
                ddlKnowAboutUs.DataSource = ddlNewHearAbout.DataSource = objDBContext.KnowAboutUs.OrderBy(us => us.ModeNames).Where(c => c.Status == 1);
                ddlKnowAboutUs.DataTextField = ddlNewHearAbout.DataTextField = "ModeNames";
                ddlKnowAboutUs.DataValueField = ddlNewHearAbout.DataValueField = "ID";
                ddlKnowAboutUs.DataBind();
                ddlNewHearAbout.DataBind();
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void ActivateDeactivateTraining(int TrainingStatus)
        {
            try
            {
                foreach (RepeaterItem rItem in rptrTrainingDetails.Items)
                {
                    CheckBox chkSelect = (rItem.FindControl("chkSelect") as CheckBox);
                    HiddenField hdnTrainingDetail = (rItem.FindControl("hdnTrainingDetail") as HiddenField);
                    if (chkSelect.Checked)
                    {
                        var selTraining = GetDetailsfromID(int.Parse(hdnTrainingDetail.Value));
                        if (selTraining.IsNotNull())
                        {
                            selTraining.Status = TrainingStatus;
                        }
                    }
                }
                objDBContext.SaveChanges();
                divMTDSuccessMsg.Visible = true;
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void UpdateTrainingDetailsStatus(string QueryStringKey, int RecordStatus)
        {
            try
            {
                ////If redirected for delete then do delete
                if (Request.QueryString[QueryStringKey].IsValid())
                {
                    int TrainerID = int.Parse(Request.QueryString[QueryStringKey]);
                    var objToDelete = GetDetailsfromID(TrainerID);
                    if (objToDelete.IsNotNull())
                    {
                        objToDelete.Status = RecordStatus;
                        objDBContext.SaveChanges();
                        ShowPaginatedAndDeletedRecords();
                    }
                    divMTDSuccessMsg.Visible = true;
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private TrainingDetail GetDetailsfromID(int TrainerID)
        {
            return objDBContext.TrainingDetails.FirstOrDefault(cat => cat.TrainnerID == TrainerID);
        }



        private void ShowPaginatedAndDeletedRecords()
        {
            try
            {
                if (chkShowDeActivated.Checked)
                {
                    rptrTrainingDetails.DataSource = objDBContext.TrainingDetails.OrderByDescending(obj => obj.FirstName);
                    rptrTrainingDetails.DataBind();
                }
                else
                {
                    rptrTrainingDetails.DataSource = objDBContext.TrainingDetails.OrderByDescending(obj => obj.FirstName).Where(usr => usr.Status == DBKeys.RecordStatus_Active);
                    rptrTrainingDetails.DataBind();
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        #endregion

        protected void btnNewMTD_Click(object sender, EventArgs e)
        {
            LoadStatus();
            mvContainer.ActiveViewIndex = 2;
            divNewError.Visible = divNewSuccess.Visible = false;
            txtNewCurrentCompany.Text = txtNewEmailID.Text = txtNewFirstName.Text = txtNewLastName.Text = txtNewOQCollege.Text = txtNewOQPerc.Text = txtNewOthers.Text = txtNewPermanentAddress.Text =
txtNewPGCollege.Text = txtNewPGPerc.Text = txtNewPhoneNumber.Text = txtNewTemporary.Text = txtNewUGCollege.Text = txtNewUGPerc.Text = txtNewYearOfExperience.Text = txtTechCurrentlyWork.Text = ""; 
        }

        protected void btnNewSave_Click(object sender, EventArgs e)
        {
            try
            {
                string ImageURL = "";
                
                if (fluNewTraineeImage.HasFile)
                {
                    string type = fluNewTraineeImage.FileName.Substring(fluNewTraineeImage.FileName.LastIndexOf('.') + 1).ToLower();
                    if (type == "jpg" || type == "jpeg" || type == "png")
                    {
                        string ImageName = DateTime.Now.ToString("ddMMyyyymmssfff");
                        string ImageFullPath = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/IpadImages/");
                        if (!Directory.Exists(ImageFullPath))
                        {
                            Directory.CreateDirectory(ImageFullPath);
                        }
                        fluNewTraineeImage.SaveAs(ImageFullPath + ImageName + ".jpg");
                        ImageURL = "/UploadedContents/IpadImages/" + ImageName + ".jpg";
                    }
                    else
                    {
                        divNewSuccess.Visible = false;
                        divNewError.Visible = true;
                        lblNewError.Text = "Please enter valid image file of the type *.jpg / *.jpeg / *.png";
                        return;
                    }
                }
                int VisitorID  = 0;
                TrainingDetail selTraining = new TrainingDetail();
                if (Request.QueryString["VisitorID"].IsValid())
                {

                    VisitorID = int.Parse(Request.QueryString["VisitorID"]);
                    selTraining.VisitorID = VisitorID;
                    if (imgNewTraineeImage.Visible)
                        selTraining.ImgPath = imgNewTraineeImage.ImageUrl;
                }
                selTraining.FirstName = txtNewFirstName.Text;
                selTraining.LastName = txtNewLastName.Text;
                selTraining.Email = txtNewEmailID.Text;
                selTraining.PhoneNumber = txtNewPhoneNumber.Text;
                selTraining.PermanentAddress = txtNewPermanentAddress.Text;
                selTraining.TemporaryAddress = txtNewTemporary.Text;
                selTraining.CourseOfInterest = Convert.ToInt32(ddlNewCourseOfInterest.SelectedValue);
                selTraining.KnowAboutUs = Convert.ToInt32(ddlNewHearAbout.SelectedValue);
                selTraining.Others = txtNewOthers.Text;
                selTraining.UGCollege = txtNewUGCollege.Text;
                selTraining.UGPercentage = Convert.ToDecimal(txtNewUGPerc.Text);
                selTraining.PGCollege = txtNewPGCollege.Text;
                selTraining.PGPercentage = Convert.ToDecimal(txtNewPGPerc.Text);
                selTraining.OQCollege = txtNewOQCollege.Text;
                if(txtNewOQPerc.Text.Trim() != "")
                selTraining.OQPercentage = Convert.ToDecimal(txtNewOQPerc.Text);
                selTraining.YearOfExperience = Convert.ToString(txtNewYearOfExperience.Text);
                selTraining.CurrentCompany = txtNewCurrentCompany.Text;
                selTraining.TechnologyCurrentlyWork = txtTechCurrentlyWork.Text;
                selTraining.Status = Convert.ToInt32(ddlNewStatus.SelectedValue);
                selTraining.CreatedOn = DateTime.Now;
               
                if (ImageURL != "")
                    selTraining.ImgPath = ImageURL;

                
                objDBContext.TrainingDetails.AddObject(selTraining);
                objDBContext.SaveChanges();
                var selVisitor = objDBContext.VisitorsDetails.FirstOrDefault(cat => cat.VisitorID == VisitorID);
                if (selVisitor != null)
                {
                    selVisitor.TrainnerID = objDBContext.TrainingDetails.Max(a => a.TrainnerID);
                    objDBContext.SaveChanges();
                }

                divNewError.Visible = false;
                divNewSuccess.Visible = true;
                txtNewCurrentCompany.Text = txtNewEmailID.Text = txtNewFirstName.Text = txtNewLastName.Text = txtNewOQCollege.Text = txtNewOQPerc.Text = txtNewOthers.Text = txtNewPermanentAddress.Text =
                txtNewPGCollege.Text = txtNewPGPerc.Text = txtNewPhoneNumber.Text = txtNewTemporary.Text = txtNewUGCollege.Text = txtNewUGPerc.Text = txtNewYearOfExperience.Text = txtTechCurrentlyWork.Text = "";
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                divNewSuccess.Visible = false;
                divNewError.Visible = true;
                lblNewError.Text = "Error occured while saving training details";
            }
        }

       

    }
}
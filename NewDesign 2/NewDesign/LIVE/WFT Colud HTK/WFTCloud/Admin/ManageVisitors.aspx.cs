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
    public partial class ManageVisitors : System.Web.UI.Page
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
                    UpdateVisitorsDetailsStatus(QueryStringKeys.Delete, DBKeys.RecordStatus_Delete);
                    //Check if the screen should activate any category from query string parameter.
                    UpdateVisitorsDetailsStatus(QueryStringKeys.Activate, DBKeys.RecordStatus_Active);
                    //Check if the screen should deactivate any category from query string parameter.
                    UpdateVisitorsDetailsStatus(QueryStringKeys.Deactivate, DBKeys.RecordStatus_Inactive);
                    //Check if the screen should load edit category from query string parameter.
                    EditVisitorDetails();
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
                if (Request.QueryString[QueryStringKeys.EditVisitorDetails].IsValid())
                {
                    int VisitorId = int.Parse(Request.QueryString[QueryStringKeys.EditVisitorDetails]);
                    var newvisitor = objDBContext.VisitorsDetails.FirstOrDefault(cat => cat.VisitorID == VisitorId);
                    if (newvisitor != null) 
                    {
                        string ImageURL = "";

                        if (fluVisitorImage.HasFile)
                        {
                            string type = fluVisitorImage.FileName.Substring(fluVisitorImage.FileName.LastIndexOf('.') + 1).ToLower();
                            if (type == "jpg" || type == "jpeg" || type == "png")
                            {
                                string ImageName = DateTime.Now.ToString("ddMMyyyymmssfff");
                                string ImageFullPath = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/IpadImages/");
                                //string presentimagePath = Server.MapPath(newvisitor.ImgPath);
                                if (!Directory.Exists(ImageFullPath))
                                {
                                    Directory.CreateDirectory(ImageFullPath);
                                }
                               
                                fluVisitorImage.SaveAs(ImageFullPath + ImageName + ".jpg");
                                ImageURL = "/UploadedContents/IpadImages/" + ImageName + ".jpg";
                            }
                            else
                            {
                                lblSuccessMessage.Visible = false;
                                lblErrorMessage.Visible = true;
                                lblVisitorDetailsError.Text = "Please enter valid image file of the type *.jpg / *.jpeg / *.png";
                                return;
                            }
                        }
                        newvisitor.FirstName = txtFirstName.Text;
                        newvisitor.LastName = txtLastName.Text;
                        newvisitor.Email = txtEmailID.Text;
                        newvisitor.PhoneNumber = Convert.ToString(txtPhoneNumber.Text);
                        if (ImageURL != "")
                            newvisitor.ImgPath = ImageURL;
                        //newvisitor.CompanyNameAddress = txtCompanyNameAddress.Text;
                        //newvisitor.ToMeet = txtToMeet.Text;
                        //newvisitor.Purpose = txtPurpose.Text;
                        //newvisitor.TimeIn = Convert.ToDateTime(txtTimeIn.Text);
                        //newvisitor.TimeOut = Convert.ToDateTime(txtTimeOut.Text);
                        newvisitor.Status = Convert.ToInt32(ddlStatus.SelectedValue);
                        lblSuccessMessage.Visible = true;
                        lblVisitorDetailsError.Text = "Visitors Details are Invalid.";
                        objDBContext.SaveChanges();
                        EditVisitorDetails();
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                lblSuccessMessage.Visible = false;
                lblErrorMessage.Visible = true;
                lblVisitorDetailsError.Text = "Errors Occured while saving the details!";
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            EditVisitorDetails();
        }


        protected void btnActivate_Click(object sender, EventArgs e)
        {
            ActivateDeactivateVisitors(DBKeys.RecordStatus_Active);
            ShowPaginatedAndDeletedRecords();
        }

        protected void btnDeactivate_Click(object sender, EventArgs e)
        {
            ActivateDeactivateVisitors(DBKeys.RecordStatus_Inactive);
            ShowPaginatedAndDeletedRecords();

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ActivateDeactivateVisitors(DBKeys.RecordStatus_Delete);
            ShowPaginatedAndDeletedRecords();
        }

        protected void chkShowDeActivated_CheckedChanged(object sender, EventArgs e)
        {
            ShowPaginatedAndDeletedRecords();
        }

        #endregion

        #region ReusableRoutines

        private void ShowPaginatedAndDeletedRecords()
        {
            try
            {
                
                    rptrVisitorsDetails.DataSource = objDBContext.VisitorsDetails.OrderByDescending(obj => obj.VisitorID);
                    rptrVisitorsDetails.DataBind();
               
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void EditVisitorDetails()
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.EditVisitorDetails].IsValid())
                {
                    mvContainer.ActiveViewIndex = 1;
                    int VisitorId = int.Parse(Request.QueryString[QueryStringKeys.EditVisitorDetails]);
                    var selVisitors = objDBContext.VisitorsDetails.FirstOrDefault(cat => cat.VisitorID == VisitorId);
                    if (selVisitors != null)
                    {
                        LoadEditStatus();
                        txtFirstName.Text = selVisitors.FirstName;
                        txtLastName.Text = selVisitors.LastName;
                        txtEmailID.Text = selVisitors.Email;
                        txtPhoneNumber.Text = selVisitors.PhoneNumber;
                        if (selVisitors.ImgPath != null)
                            imgVisitorImage.ImageUrl = selVisitors.ImgPath;
                        else
                            imgVisitorImage.Visible = false;
                        //txtCompanyNameAddress.Text = selVisitors.CompanyNameAddress;
                        //txtToMeet.Text = selVisitors.ToMeet;
                        //txtPurpose.Text = selVisitors.Purpose;
                        //txtTimeIn.Text = Convert.ToString(selVisitors.TimeIn);
                        //txtTimeOut.Text = Convert.ToString(selVisitors.TimeOut);
                        if(Convert.ToString(selVisitors.Status) =="1")
                          ddlStatus.SelectedValue = "1" ;
                        else
                            ddlStatus.SelectedValue = "0";
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void LoadEditStatus()
        {
            //ddlStatus.DataSource =ddlNewStatus.DataSource= objDBContext.RecordStatus.OrderBy(a => a.RecordStatusDesc);
            //ddlStatus.DataTextField =ddlNewStatus.DataTextField= "RecordStatusDesc";
            //ddlStatus.DataValueField =ddlNewStatus.DataValueField= "RecordStatusID";
            //ddlStatus.DataBind();
            //ddlNewStatus.DataBind();
        }

        private void ActivateDeactivateVisitors(int Status)
        {
            try
            {
                foreach (RepeaterItem rItem in rptrVisitorsDetails.Items)
                {
                    CheckBox chkSelect = (rItem.FindControl("chkSelect") as CheckBox);
                    HiddenField hdnVisitorDetail = (rItem.FindControl("hdnVisitorDetail") as HiddenField);
                    if (chkSelect.Checked)
                    {
                        var selVisitors = GetDetailsfromID(int.Parse(hdnVisitorDetail.Value));
                        if (selVisitors.IsNotNull())
                        {
                            selVisitors.Status = Status;
                            divMVDSuccessMessage.Visible = true;
                        }
                        else
                        {
                            divMVDSuccessMessage.Visible = false;
                        }
                    }
                    objDBContext.SaveChanges();
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private VisitorsDetail GetDetailsfromID(int VisitorId)
        {
            return objDBContext.VisitorsDetails.FirstOrDefault(use => use.VisitorID == VisitorId);
        }

        private void UpdateVisitorsDetailsStatus(string QueryStringKey, int RecordStatus)
        {
            try
            {
                //If redirected for delete then do delete
                if (Request.QueryString[QueryStringKey].IsValid())
                {
                    int VisitorID = int.Parse(Request.QueryString[QueryStringKey]);
                    var objToDelete = GetDetailsfromID(VisitorID);
                    if (objToDelete.IsNotNull())
                    {
                        objToDelete.Status = RecordStatus;
                        objDBContext.SaveChanges();
                        ShowPaginatedAndDeletedRecords();
                    }
                    divMVDSuccessMessage.Visible = true;
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        #endregion

        protected void btnNewVisitorDetail_Click(object sender, EventArgs e)
        {
            mvContainer.ActiveViewIndex = 2;
            LoadEditStatus();
            divNewError.Visible = divNewSuccess.Visible = false;
        }

        protected void btnNewVisitorSave_Click(object sender, EventArgs e)
        {
            try
            {
                string ImageURL = "";

                if (fluNewVisitorImage.HasFile)
                {
                    string type = fluNewVisitorImage.FileName.Substring(fluNewVisitorImage.FileName.LastIndexOf('.') + 1).ToLower();
                    if (type == "jpg" || type == "jpeg" || type == "png")
                    {
                        string ImageName = DateTime.Now.ToString("ddMMyyyymmssfff");
                        string ImageFullPath = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/IpadImages/");
                        if (!Directory.Exists(ImageFullPath))
                        {
                            Directory.CreateDirectory(ImageFullPath);
                        }
                        fluNewVisitorImage.SaveAs(ImageFullPath + ImageName + ".jpg");
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

                VisitorsDetail newvisitor = new VisitorsDetail();
                newvisitor.FirstName = txtNewFirstName.Text;
                newvisitor.LastName = txtNewLastName.Text;
                newvisitor.Email = txtNewEmailID.Text;
                newvisitor.PhoneNumber = Convert.ToString(txtNewPhoneNumber.Text);
                if(ImageURL != "")
                    newvisitor.ImgPath = ImageURL;
                //newvisitor.CompanyNameAddress = txtNewCompanyNameAddress.Text;
                //newvisitor.ToMeet = txtNewToMeet.Text;
                //newvisitor.Purpose = txtNewPurpose.Text;
                //newvisitor.TimeIn = Convert.ToDateTime(txtNewTimeIN.Text);
                //newvisitor.TimeOut = Convert.ToDateTime(txtNewTimeOut.Text);
                newvisitor.Status = Convert.ToInt32(ddlNewStatus.SelectedValue);
                newvisitor.CreatedOn = DateTime.Now;

                objDBContext.VisitorsDetails.AddObject(newvisitor);
                objDBContext.SaveChanges();
                divNewSuccess.Visible = true;
                divNewError.Visible = false;
                txtNewEmailID.Text = txtNewFirstName.Text = txtNewLastName.Text = txtNewPhoneNumber.Text = "";
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                divNewSuccess.Visible = false;
                divNewError.Visible = true;
                lblNewError.Text = "Error Occured while saving Visitor details!";
            }
        }

       
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;
using WFTCloud.ResuableRoutines;
using WFTCloud.ResuableRoutines.SMTPManager;

namespace WFTCloud.Admin
{
    public partial class ManageCRMRequests : System.Web.UI.Page
    {
        #region Global Variables and Properties
        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        public string CRMCodeRequestID;
        #endregion

        #region Pageload
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblManageSuccessMessage.Visible = lblManageErrorMessage.Visible = false;

                if (!IsPostBack)
                {

                    //Show records based on pagination value and deleted flag for Manage Crm Issues.
                    ShowManageCrmPaginatedAndDeletedRecords();
                    // Load Select CRM Issues
                    LoadManageCrmIssue();
                    // Load selected CRM history
                    LoadCrmIssueHistory();
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name ), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        #endregion
        #region Control Events

        #endregion
        #region Reusable Routines
        public void LoadCrmIssueHistory()
        {
            try
            {
                string Crmhistoryid = Request.QueryString["Crmhistoryid"];
                if (Request.QueryString[QueryStringKeys.EditCrmIssues].IsValid() && Request.QueryString["Crmhistoryid"].IsValid())
                {
                    int HistoryID = int.Parse(Crmhistoryid);
                    var selRequest = objDBContext.vwCRMIssueHistoryDetails.FirstOrDefault(cat => cat.CRMHistoryID == HistoryID);
                    if (selRequest != null)
                    {
                        mvCrmIssues.ActiveViewIndex = 3;
                        lblCRMHCode.Text = "CRM" + selRequest.CRMRequestID;
                        if (selRequest.CategoryName == null)
                        {
                            trCRMHCategory.Visible = trCRMHService.Visible = false;
                        }
                        else
                        {
                            trCRMHCategory.Visible = trCRMHService.Visible = true;
                            lblCRMHCategory.Text = selRequest.CategoryName;
                            lblCRMHServiceName.Text = selRequest.ServiceName;
                        }

                        lblCRMHIssueDesc.Text = selRequest.IssueDescription;
                        lblCRMHIssueType.Text = selRequest.CRMIssueTypeDesc;
                        lblCRMHStatus.Text = selRequest.CRMRequestStatusDesc == "Assigned" ? "Work In Process" : selRequest.CRMRequestStatusDesc;
                        lblCRMHSubject.Text = selRequest.CRMSubject;

                        if (selRequest.AttachmentURL != null)
                        {
                            hypCRMHAttachment.Text = "Click Here To View";
                            hypCRMHAttachment.NavigateUrl = selRequest.AttachmentURL;
                            hypCRMHAttachment.Visible = true;
                            lblCRMHAttachment.Visible = false;
                        }
                        else
                        {
                            hypCRMHAttachment.Visible = false;
                            lblCRMHAttachment.Visible = true;
                        }
                        if (selRequest.CRMRequestStatusDesc == "Resolved" || selRequest.CRMRequestStatusDesc == "Closed" || selRequest.CRMRequestStatusDesc == "Unable To Resolve" || selRequest.CRMRequestStatusDesc == "User Error")
                        {
                            lblCRMHResolutionDesc.Text = selRequest.CRMResolutionSent;
                            if (selRequest.CRMAdminAttachment != null)
                            {
                                hypCRMHResAttachement.NavigateUrl = selRequest.CRMAdminAttachment;
                                hypCRMHResAttachement.Visible = true;
                                lblCRMHResAttach.Visible = false;
                            }
                            else
                            {
                                hypCRMHResAttachement.NavigateUrl = "";
                                hypCRMHResAttachement.Visible = false;
                                lblCRMHResAttach.Visible = true;
                            }
                            trCRMHResAttachement.Visible = true;
                            trCRMHResolutionDesc.Visible = true;
                        }
                        else
                        {
                            trCRMHResAttachement.Visible = false;
                            trCRMHResolutionDesc.Visible = false;
                        }
                    }
                }

            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name ), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        private void LoadManageCrmIssue()
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.EditCrmIssues].IsValid())
                {
                    int RequestID = int.Parse(Request.QueryString[QueryStringKeys.EditCrmIssues]);
                    CRMCodeRequestID = Request.QueryString[QueryStringKeys.EditCrmIssues];
                    var selRequest = objDBContext.vwManageCRMIssues.FirstOrDefault(cat => cat.CRMRequestID == RequestID);
                    if (selRequest != null)
                    {
                        dvCRMHistory.Visible = false;
                        var CRmHistories = objDBContext.vwCRMIssueHistoryDetails.Where(cat => cat.CRMRequestID == RequestID);
                        if (CRmHistories.Count() > 0)
                        {
                            dvCRMHistory.Visible = true;
                            rptrHistory.DataSource = CRmHistories;
                            rptrHistory.DataBind();
                        }
                        else
                        {
                            dvCRMHistory.Visible = false;
                        }
                        dvWorkLog.Visible = false;
                        var CRMWorkLogHistories = objDBContext.CRMRequestNotes.Where(cat => cat.CRMRequestID == RequestID).OrderByDescending(a => a.LastModifiedOn);
                        if (CRMWorkLogHistories.Count() > 0)
                        {
                            dvWorkLog.Visible = true;
                            rptrWorkLogHistory.DataSource = CRMWorkLogHistories;
                            rptrWorkLogHistory.DataBind();
                        }
                        else
                        {
                            dvWorkLog.Visible = false;
                        }
                        lblCustomerName.Text = selRequest.CustomerFullName +" - "+ selRequest.EmailID;
                        mvCrmIssues.ActiveViewIndex = 1;
                        lblCurrentCRMID.Text = "CRM" + Convert.ToString(selRequest.CRMRequestID);
                        txtIssue.Text = selRequest.IssueDescription;
                        //txtAdminNotes.Text = selRequest.AdminNotes;
                        lblIssueType.Text = selRequest.CRMIssueTypeDesc;
                        lblSubject.Text = selRequest.CRMSubject;
                        if (!lblSubject.Text.IsValid())
                            lblSubject.Text = "CRM Request Status - CRM" + RequestID.ToString();
                        btnChangeissueType.Text = selRequest.IssueType == 1 ? "Change Issue To Sales" : "Change Issue To Technical";
                        if (selRequest.AttachmentURL != null)
                        {
                            hypAttchmentLink.NavigateUrl = selRequest.AttachmentURL;
                            lblNoAttachment.Visible = false;
                        }
                        else
                        {
                            hypAttchmentLink.Visible = false;
                            lblNoAttachment.Visible = true;
                        }
                        //if (selRequest.CRMAdminAttachment != null)
                        //{
                        //    hypReslonAttachment.NavigateUrl = selRequest.CRMAdminAttachment;
                        //    lblhypReslonAttachment.Visible = false;
                        //}
                        //else
                        //{
                        //    hypReslonAttachment.Visible = false;
                        //    lblhypReslonAttachment.Visible = true;
                        //}
                        if (selRequest.ServiceCategoryID != null)
                        {
                            trServiceCategory.Visible = trService.Visible = true;
                            lblcategoryName.Text = selRequest.CategoryName.IsValid() ? selRequest.CategoryName : "-";
                            lblServiceName.Text = selRequest.ServiceName.IsValid() ? selRequest.ServiceName : "-";
                        }
                        else
                        {
                            trServiceCategory.Visible = trService.Visible = false;
                        }
                        string EmailIDs = "";

                        if (selRequest.CRMIssueTypeDesc.Contains("Technical"))
                        {
                            EmailIDs = objDBContext.WftSettings.FirstOrDefault(a => a.SettingKey == "TECH_EMAIL").SettingValue;
                        }
                        else if (selRequest.CRMIssueTypeDesc.Contains("Sales"))
                        {
                            EmailIDs = objDBContext.WftSettings.FirstOrDefault(a => a.SettingKey == "SALES_EMAIL").SettingValue;
                        }
                        string[] AdminMailIDs = Regex.Split(EmailIDs, ",");
                        List<UserProfile> TeamList = new List<UserProfile>();
                        foreach (string MailId in AdminMailIDs)
                        {
                            var Userprofiles = objDBContext.UserProfiles.FirstOrDefault(u => u.EmailID == MailId && u.RecordStatus == DBKeys.RecordStatus_Active);
                            if (Userprofiles != null)
                                TeamList.Add(Userprofiles);
                        }
                        if (EmailIDs != "" && TeamList.Count() == 0)
                        {
                            var Userprofiles = objDBContext.UserProfiles.FirstOrDefault(u => u.EmailID == EmailIDs && u.RecordStatus == DBKeys.RecordStatus_Active);
                            if (Userprofiles != null)
                                TeamList.Add(Userprofiles);
                        }
                        /*
                        ddlAssignAdmin.DataSource = TeamList;
                        ddlAssignAdmin.DataTextField = "EmailID";
                        ddlAssignAdmin.DataValueField = "UserMembershipID";
                        ddlAssignAdmin.DataBind();
                        ddlAssignAdmin.Items.Insert(0, new ListItem("<-- Select -->", "0"));
                        HttpCookie cookie = (HttpCookie)Request.Cookies[FormsAuthentication.FormsCookieName];
                        if (cookie != null)
                        {
                            string LoggedUserName = FormsAuthentication.Decrypt(cookie.Value).Name;
                            trAssignTo.Visible = EmailIDs.ToLower().Contains(LoggedUserName) ? false : true;
                            btnChangeissueType.Visible = trAssignTo.Visible==true? true:false;
                        }
                        string AssignAdmin = Convert.ToString(selRequest.AssignedToAdmin);
                        if (AssignAdmin != "" || AssignAdmin != string.Empty)
                        {
                            ddlAssignAdmin.SelectedValue = Convert.ToString(AssignAdmin);
                        }
                         */
                        if (selRequest.CRMResolutionSent.IsValid())
                        {
                            //txtCustomerMailContent.Text = selRequest.CRMResolutionSent;
                            trContentCustomer.Visible = true;
                            lblCustomerContent.Text = "CRM Resolution sent to Customer";
                            //trCRMReslnAttachment.Visible = true;
                        }

                        List<CRMRequestStatu> CRS = new List<CRMRequestStatu>();
                        var open = objDBContext.CRMRequestStatus.FirstOrDefault(s => s.CRMRequestStatusDesc == "Open");
                        CRS.Add(open);
                        var Assigned = objDBContext.CRMRequestStatus.FirstOrDefault(s => s.CRMRequestStatusDesc == "Assigned");
                        CRS.Add(Assigned);
                        var wip = objDBContext.CRMRequestStatus.FirstOrDefault(s => s.CRMRequestStatusDesc == "Work In Progress");
                        CRS.Add(wip);
                        var Resolved = objDBContext.CRMRequestStatus.FirstOrDefault(s => s.CRMRequestStatusDesc == "Resolved");
                        CRS.Add(Resolved);
                        var Closed = objDBContext.CRMRequestStatus.FirstOrDefault(s => s.CRMRequestStatusDesc == "Closed");
                        CRS.Add(Closed);
                        var UnableToResolve = objDBContext.CRMRequestStatus.FirstOrDefault(s => s.CRMRequestStatusDesc == "Unable To Resolve");
                        CRS.Add(UnableToResolve);
                        var UserError = objDBContext.CRMRequestStatus.FirstOrDefault(s => s.CRMRequestStatusDesc == "User Error");
                        CRS.Add(UserError);
                        ddlCrmStatus.DataSource = CRS;
                        ddlCrmStatus.DataTextField = "CRMRequestStatusDesc";
                        ddlCrmStatus.DataValueField = "CRMRequestStatusID";
                        ddlCrmStatus.DataBind();
                        ddlCrmStatus.SelectedValue = Convert.ToString(selRequest.CRMRequestStatus);
                        if (ddlCrmStatus.SelectedItem.Text == "Resolved" || ddlCrmStatus.SelectedItem.Text == "Closed" || ddlCrmStatus.SelectedItem.Text == "User Error" || ddlCrmStatus.SelectedItem.Text == "Unable To Resolve")//&& trAssignTo.Visible==false)
                        {
                            txtCustomerMailContent.Enabled = false;
                            ddlCrmStatus.Enabled = false;
                            trButtonSave.Visible = false;
                            trWorklogNotes.Visible = false;
                            trAddAttachment.Visible = false;
                            trContentCustomer.Visible = false;
                        }
                        else
                        {
                            txtCustomerMailContent.Enabled = true;
                            ddlCrmStatus.Enabled = true;
                            trButtonSave.Visible = true;
                            trWorklogNotes.Visible = true;
                            trAddAttachment.Visible = false;
                            trContentCustomer.Visible = false;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name ), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        private void ShowManageCrmPaginatedAndDeletedRecords()
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.EditCrmIssues].IsValid() == false)
                {
                    var CrmRequestCount = objDBContext.CRMRequests.OrderByDescending(obj => obj.CRMRequestID);
                    if (CrmRequestCount.Count() > 0)
                    {
                        var CrmRequest = objDBContext.vwManageCRMIssues.OrderBy(obj => obj.CRMRequestID);
                        string UserType = "";
                        string UserName = "";
                        Guid ID = new Guid();
                        HttpCookie cookie = (HttpCookie)Request.Cookies[FormsAuthentication.FormsCookieName];
                        if (cookie != null)
                        {
                            UserName = FormsAuthentication.Decrypt(cookie.Value).Name;
                            var user = objDBContext.UserProfiles.FirstOrDefault(a => a.EmailID == UserName);
                            var techmails = objDBContext.WftSettings.FirstOrDefault(a => a.SettingKey == "TECH_EMAIL");
                            if (techmails != null)
                            {
                                UserType = techmails.SettingValue.ToLower().Contains(UserName.ToLower()) ? "Technical" : "";
                            }
                            var sales = objDBContext.WftSettings.FirstOrDefault(a => a.SettingKey == "SALES_EMAIL");
                            if (sales != null)
                            {
                                UserType = sales.SettingValue.ToLower().Contains(UserName.ToLower()) ? "Sales" : UserType;
                            }
                            if (UserType != "")
                                ID = new Guid(user.UserMembershipID.ToString());
                        }
                        if (UserType != "")
                        { btnChangeissueType.Visible = false; btnChangeissueType.Enabled = false; }
                        if (ManageShowDeleted.Checked)
                        {
                            if (UserType == "")
                                rptrManageCRMIssue.DataSource = CrmRequest;
                            else if (UserType != "")
                                rptrManageCRMIssue.DataSource = CrmRequest.Where(a => a.CRMIssueTypeDesc.ToLower().Contains(UserType.ToLower()));

                            rptrManageCRMIssue.DataBind();
                        }
                        else
                        {
                            if (UserType == "")
                                rptrManageCRMIssue.DataSource = CrmRequest.Where(a => a.CRMRequestStatusDesc == "Open" || a.CRMRequestStatusDesc == "Assigned" || a.CRMRequestStatusDesc == "Work In Progress");
                            else if (UserType != "")
                                rptrManageCRMIssue.DataSource = rptrManageCRMIssue.DataSource = CrmRequest.Where(a => a.CRMRequestStatusDesc == "Open" || a.CRMRequestStatusDesc == "Assigned" || a.CRMRequestStatusDesc == "Work In Progress").Where(b => b.CRMIssueTypeDesc.ToLower().Contains(UserType.ToLower()));

                            rptrManageCRMIssue.DataBind();
                        }
                    }
                    else
                    {
                        mvCrmIssues.ActiveViewIndex = 2;
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name ), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        #endregion

        protected void ManageShowDeleted_CheckedChanged(object sender, EventArgs e)
        {
            ShowManageCrmPaginatedAndDeletedRecords();
        }

        protected void btnCrmIssueSave_Click(object sender, EventArgs e)
        {
            try
            {
                int IssueID = int.Parse(Request.QueryString[QueryStringKeys.EditCrmIssues]);
                string name = fluAddAttachments.FileName;
                string OrgName = DateTime.Now.ToString("ddMMyyyyhhmmss") + name.Replace(" ", "_");
                if (Request.QueryString[QueryStringKeys.EditCrmIssues].IsValid())
                {
                    var CrmIssue = objDBContext.CRMRequests.FirstOrDefault(usr => usr.CRMRequestID == IssueID);
                    if (fluAddAttachments.HasFile)
                    {
                        string EditPath = "";
                        EditPath = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/CRMRequest/" + IssueID + "/");
                        if (!Directory.Exists(EditPath))
                        {
                            Directory.CreateDirectory(EditPath);
                        }
                        fluAddAttachments.SaveAs(EditPath + OrgName);
                    }
                    string url = HttpContext.Current.Request.Url.AbsoluteUri;
                    HttpCookie cookie = (HttpCookie)Request.Cookies[FormsAuthentication.FormsCookieName];
                    Guid LoggedUseriD = new Guid();
                    if (cookie != null )
                    {
                        string UserName = FormsAuthentication.Decrypt(cookie.Value).Name;
                        var loggeduser = objDBContext.UserProfiles.FirstOrDefault(A => A.EmailID == UserName);
                        LoggedUseriD = loggeduser.UserMembershipID;
                    }

                    CRMRequestNote CRnot = new CRMRequestNote();
                    CRnot.CreatedBy = LoggedUseriD;
                    CRnot.CreatedOn = DateTime.Now;
                    CRnot.CRMRequestID = CrmIssue.CRMRequestID;
                    CRnot.CRMStatusChangeFrom = CrmIssue.CRMRequestStatus;
                    CRnot.CRMStatusChangeTo = Convert.ToInt32(ddlCrmStatus.SelectedValue);
                    CRnot.CRMUpdate = txtAdminNotes.Text;
                    CRnot.LastModifiedBy = LoggedUseriD;
                    CRnot.LastModifiedOn = DateTime.Now;
                    if (trContentCustomer.Visible == true)
                        CRnot.ResolutionSentToCustomer = txtCustomerMailContent.Text;
                    if (trAddAttachment.Visible == true && fluAddAttachments.HasFile)
                    {
                        CrmIssue.CRMAdminAttachment = CRnot.AttachmentSentToCustomer = "/UploadedContents/CRMRequest/" + IssueID + "/" + OrgName;
                    }
                    objDBContext.CRMRequestNotes.AddObject(CRnot);

                    int StatucB4 = CrmIssue.CRMRequestStatus;
                    CrmIssue.CRMRequestStatus = Convert.ToInt32(ddlCrmStatus.SelectedValue);
                    string a = CrmIssue.AssignedToAdmin != null ? CrmIssue.AssignedToAdmin.ToString() : "0";
                    //if (a != ddlAssignAdmin.SelectedValue)
                    //{
                    //    string Admin = ddlAssignAdmin.SelectedValue;
                    //    Guid AdminID = new Guid(Admin);
                    //    CrmIssue.AssignedToAdmin = AdminID;
                    //}
                    CrmIssue.AssignedToAdmin = LoggedUseriD;// Currently working user membership id
                    CrmIssue.AdminNotes = txtAdminNotes.Text;
                    if (trContentCustomer.Visible && CrmIssue.CRMResolutionSent != txtCustomerMailContent.Text)
                    {
                        CrmIssue.CRMResolutionSent = txtCustomerMailContent.Text;
                    }

                    CrmIssue.LastModifiedBy = LoggedUseriD;
                    CrmIssue.LastModifiedOn = DateTime.Now;
                    objDBContext.SaveChanges();
                    lblManageSuccessMessage.Visible = true;
                    //if ((a != ddlAssignAdmin.SelectedValue)&& ddlAssignAdmin.SelectedValue!="0")
                    //    SentMailToSelectedUSer();
                    if (CrmIssue.CRMRequestStatus == 2 || CrmIssue.CRMRequestStatus == 3 || CrmIssue.CRMRequestStatus == 5 || CrmIssue.CRMRequestStatus == 6)
                    {
                        if (StatucB4 != CrmIssue.CRMRequestStatus)
                        {
                            SendCRMDetailToAdminCustomer();
                        }
                    }
                    LoadManageCrmIssue();
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name ), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        public string CRMRecordStatus(string StatusID)
        {
            try
            {
                int id = Convert.ToInt32(StatusID);
                return objDBContext.CRMRequestStatus.FirstOrDefault(A => A.CRMRequestStatusID == id).CRMRequestStatusDesc;
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name ), Ex.Message, Ex.StackTrace, DateTime.Now);
                return "-";
            }
        }
        public string UserMailIDbyGuid(string membeshipID)
        {
            try
            {
                Guid id = new Guid(membeshipID);
                return objDBContext.UserProfiles.FirstOrDefault(A => A.UserMembershipID == id).EmailID;
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name ), Ex.Message, Ex.StackTrace, DateTime.Now);
                return "-";
            }
        }
        public void SendEmailAttachment(string messageBody, string subject, string ToMail, string CcMail, bool sendInBCC, bool IsHtml, string attachmentPath)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"]);
                string FromMail = Convert.ToString(ConfigurationManager.AppSettings["FromEmail"]);
                mail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"]);
                if (!sendInBCC)
                    mail.To.Add(ToMail);
                else
                {
                    mail.Bcc.Add(ToMail);
                }
                if (CcMail != string.Empty)
                {
                    mail.CC.Add(CcMail);
                }
                mail.Subject = subject;
                mail.Body = messageBody;
                mail.IsBodyHtml = IsHtml;
                try
                {
                    if (attachmentPath != "")
                    {
                        // Add attachment
                        attachmentPath = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + attachmentPath);
                        if (File.Exists(attachmentPath))
                        { mail.Attachments.Add(new Attachment(attachmentPath)); }
                    }
                }
                catch (Exception Ex)
                {
                    ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name ), Ex.Message, Ex.StackTrace, DateTime.Now);
                }

                SmtpServer.Port = int.Parse(ConfigurationManager.AppSettings["SMTPPort"]);
                SmtpServer.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SMTPUsername"], ConfigurationManager.AppSettings["SMTPPassword"]);
                SmtpServer.EnableSsl = bool.Parse(ConfigurationManager.AppSettings["EnableSSL"]);

                SmtpServer.Send(mail);
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name ), Ex.Message, Ex.StackTrace, DateTime.Now);
            }

        }
        /*
        public void SentMailToSelectedUSer()
        {
            try
            {
                //if (trAssignTo.Visible == true && ddlAssignAdmin.SelectedValue != "0")
                {
                    int RequestID = int.Parse(Request.QueryString[QueryStringKeys.EditCrmIssues]);
                    var selRequest = objDBContext.vwManageCRMIssues.FirstOrDefault(cat => cat.CRMRequestID == RequestID);
                    string EmailContent = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["CRMRequestToSaleOrTechTeam"]));
                    string Details = "";
                    Details = "<strong>CRM Code:</strong>CRM" + selRequest.CRMRequestID+ "<br/>";
                    var ServiceProvisioinDetails = objDBContext.UserSubscribedServices.Where(a => a.UserID == selRequest.CustomerID && a.ServiceID == selRequest.ServiceID);
                    if (ServiceProvisioinDetails.Count() > 0)
                        Details += "<strong>Service Provisioning Details:</strong><br/><br/>";
                    if (trService.Visible == true)
                    {
                        Details += "<strong>Service Category : </strong>" + selRequest.CategoryName + "<br/>"
                                  + "<strong>Service Name : </strong>" + selRequest.ServiceName + "<br/>";
                        foreach (var result in ServiceProvisioinDetails)
                        {
                            Details += "<strong>User Subscription ID : </strong>" + result.UserSubscriptionID + "<br/>"
                            + "<strong>Subscribed On : </strong>" + (result.CreatedOn != null ? (Convert.ToDateTime(result.CreatedOn).ToString("dd-MMM-yyyy")) : "-") + "<br/>"
                            + "<strong>Service/SAP UserName : </strong>" + (result.ServiceUserName.IsValid() ? result.ServiceUserName : "-") + "<br/>"
                            + "<strong>Service/SAP Password : </strong>" + (result.ServicePassword.IsValid() ? result.ServicePassword : "-") + "<br/>"
                            + "<strong>Service/SAP URL : </strong>" + (result.ServiceUrl.IsValid() ? "<a href='" + result.ServiceUrl + "'" + ">Click Here</a><br/>" : "-<br/>")
                            + "<strong>Service Other Information : </strong>" + result.ServiceOtherInformation + "<br/>"
                            + "<strong>Expiration Date : </strong>" + (result.ExpirationDate != null ? (Convert.ToDateTime(result.ExpirationDate).ToString("dd-MMM-yyyy")) : "-") + "<br/>"
                            + "<strong>Subscribtion Status : </strong>" + ShowSubscribedServiceStatus(result.RecordStatus, result.ExpirationDate.ToString(), Convert.ToBoolean(result.AutoProvisioningDone)) + "<br/><br/>";
                        }
                    }
                    Details += "<strong>Issue Type : </strong>" + selRequest.CRMIssueTypeDesc + "<br/>"
                                + "<strong>Issue Description : </strong>" + selRequest.IssueDescription + "<br/>"
                                + "<strong>Admin Notes : </strong>" + selRequest.AdminNotes + "<br/>";
                    EmailContent = EmailContent.Replace("++AddContentHere++", Details).Replace("++name++", (selRequest.CustomerFullName + " (" + selRequest.EmailID + ") ")).Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                    string Subject = selRequest.CRMIssueTypeDesc + " - CRM Request from " + selRequest.EmailID;
                    if (selRequest.AttachmentURL != null)
                        SendEmailAttachment(EmailContent, Subject, ddlAssignAdmin.SelectedItem.Text, false, true, selRequest.AttachmentURL);
                    else
                        SMTPManager.SendEmail(EmailContent, Subject, ddlAssignAdmin.SelectedItem.Text, false, true);
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name ), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        */
        public string ShowSubscribedServiceStatus(int RecordStatus, string ExpirationDate, bool AutoProvisioningDone)
        {
            if (RecordStatus == 0)
                return "Expired";
            else if (RecordStatus == -1)
                return "Cancelled";
            else
            {
                if (ExpirationDate.IsValid())
                {
                    return "Expired";
                }
                DateTime ExpDate = Convert.ToDateTime(ExpirationDate);
                if (ExpDate < DateTime.Now)
                {
                    return "Expired";
                }
                if (AutoProvisioningDone == true)
                    return "Active";
                else
                    return "Provision Pending";
            }

        }
        public void SendCRMDetailToAdminCustomer()
        {
            int RequestID = int.Parse(Request.QueryString[QueryStringKeys.EditCrmIssues]);
            var selRequest = objDBContext.vwManageCRMIssues.FirstOrDefault(cat => cat.CRMRequestID == RequestID);
            string EmailContent = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["CRMRequestStatusToCustomerAdmin"]));
            string Details, UpdateduserName = "";
            HttpCookie cookie = (HttpCookie)Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                UpdateduserName = FormsAuthentication.Decrypt(cookie.Value).Name;
            }
            string MainContentCutomer = "The below is the work log provided by our representative  who worked on your CRM Request. Please reach out to our Admin/Customer Care Department for any further details.";
            string MainContentAdmin = "The below is the work log provided by " + UpdateduserName + " worked on " + selRequest.CustomerFullName + " (" + selRequest.EmailID + ") " + " CRM Request<strong> CRM" + selRequest.CRMRequestID + "</strong>.";
            //For Customer =
            Details = "<strong>CRM Code : </strong>CRM" + selRequest.CRMRequestID + "<br/>";
            Details += ("<strong>Closing Comment : </strong>" + txtCustomerMailContent.Text);
            string CustomerEmailContent = EmailContent.Replace("++MainContent++", MainContentCutomer).Replace("++AddContentHere++", Details).Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]).Replace("++name++", selRequest.CustomerFullName);
            string AdminEmailContent = EmailContent.Replace("++MainContent++", MainContentAdmin).Replace("++AddContentHere++", Details).Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]).Replace("++name++", "Admin");

            string Subject = selRequest.CRMSubject;
            if (!Subject.IsValid())
                Subject = "CRM Request Status - CRM" + RequestID.ToString();
            string ccMail = objDBContext.WftSettings.FirstOrDefault(s => s.SettingKey == "SUPPORT_MAIL").SettingValue;
            if (selRequest.CRMAdminAttachment != null)
            {
                //Support
                //SendEmailAttachment(AdminEmailContent, Subject, ToMail, ccMail, false, true, selRequest.CRMAdminAttachment);
                //Customer
                SendEmailAttachment(CustomerEmailContent, Subject, selRequest.EmailID, ccMail, false, true, selRequest.CRMAdminAttachment);
            }
            else
            {
                //Customer
                SendEmailAttachment(CustomerEmailContent, Subject, selRequest.EmailID, ccMail, false, true, "");
            }
            //else
            //{ 
            //    SMTPManager.SendNotificationEmailToSupport(AdminEmailContent, Subject, false, ToMail,"");
            //    SMTPManager.SendEmail(CustomerEmailContent, Subject, selRequest.EmailID, false, true);
            //}
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            LoadManageCrmIssue();
        }

        protected void btnChangeissueType_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.EditCrmIssues].IsValid())
                {
                    int IssueID = int.Parse(Request.QueryString[QueryStringKeys.EditCrmIssues]);
                    var CrmIssue = objDBContext.CRMRequests.FirstOrDefault(usr => usr.CRMRequestID == IssueID);
                    CrmIssue.IssueType = CrmIssue.IssueType == 1 ? 2 : 1;
                    CrmIssue.AssignedToAdmin = null;
                    CrmIssue.CRMRequestStatus = objDBContext.CRMRequestStatus.FirstOrDefault(a => a.CRMRequestStatusDesc.Contains("Open")).CRMRequestStatusID;
                    objDBContext.SaveChanges();
                    LoadManageCrmIssue();
                    lblManageSuccessMessage.Visible = true;
                    var selRequest = objDBContext.vwManageCRMIssues.FirstOrDefault(cat => cat.CRMRequestID == IssueID);
                    string EmailContent = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["CRMRequestToSaleOrTechTeam"]));
                    string Details = "";
                    Details = "<strong>CRM Code : </strong>CRM" + selRequest.CRMRequestID + "<br/>";
                    if (trServiceCategory.Visible == true)
                    {
                        var ServiceProvisioinDetails = objDBContext.UserSubscribedServices.Where(a => a.UserID == selRequest.CustomerID && a.ServiceID == selRequest.ServiceID);
                        Details += "<strong>Service Category : </strong>" + selRequest.CategoryName + "<br/>"
                                  + "<strong>Service Name : </strong>" + selRequest.ServiceName + "<br/>";
                        if (ServiceProvisioinDetails.Count() > 0)
                            Details += "<strong>Service Provisioning Details : </strong><br/><br/>";
                        foreach (var result in ServiceProvisioinDetails)
                        {
                            Details += "<strong>User Subscription ID : </strong>" + result.UserSubscriptionID + "<br/>"
                                    + "<strong>Subscribed On : </strong>" + (result.CreatedOn != null ? (Convert.ToDateTime(result.CreatedOn).ToString("dd-MMM-yyyy")) : "-") + "<br/>"
                                    + "<strong>Service/SAP UserName : </strong>" + (result.ServiceUserName.IsValid() ? result.ServiceUserName : "-") + "<br/>"
                                    + "<strong>Service/SAP Password : </strong>" + (result.ServicePassword.IsValid() ? result.ServicePassword : "-") + "<br/>"
                                    + "<strong>Service/SAP URL : </strong>" + (result.ServiceUrl.IsValid() ? "<a href='" + result.ServiceUrl + "'" + ">Click Here</a><br/>" : "-<br/>")
                                    + "<strong>Service Other Information : </strong>" + result.ServiceOtherInformation + "<br/>"
                                    + "<strong>Expiration Date : </strong>" + (result.ExpirationDate != null ? (Convert.ToDateTime(result.ExpirationDate).ToString("dd-MMM-yyyy")) : "-") + "<br/>"
                                    + "<strong>Subscribtion Status : </strong>" + ShowSubscribedServiceStatus(result.RecordStatus, result.ExpirationDate.ToString(), Convert.ToBoolean(result.AutoProvisioningDone)) + "<br/><br/>";
                        }
                    }
                    Details += "<strong>Issue Type : </strong>" + selRequest.CRMIssueTypeDesc + "<br/>"
                                + "<strong>Issue Description : </strong>" + selRequest.IssueDescription + "<br/>";

                    EmailContent = EmailContent.Replace("++AddContentHere++", Details).Replace("++name++", (selRequest.CustomerFullName + " (" + selRequest.EmailID + ") ")).Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                    string Subject = selRequest.CRMSubject;
                    if (!Subject.IsValid())
                        Subject = "CRM Request Status - CRM" + IssueID.ToString();
                    string ToMail = "";
                    if (CrmIssue.IssueType == 2)
                    {
                        ToMail = objDBContext.WftSettings.FirstOrDefault(s => s.SettingKey == "SALES_EMAIL").SettingValue;
                    }
                    else
                    {
                        ToMail = objDBContext.WftSettings.FirstOrDefault(s => s.SettingKey == "TECH_EMAIL").SettingValue;
                    }

                    string CcMail = objDBContext.WftSettings.FirstOrDefault(s => s.SettingKey == "SUPPORT_MAIL").SettingValue;
                    if (selRequest.AttachmentURL != null)
                        SendEmailAttachment(EmailContent, Subject, ToMail, CcMail, false, true, selRequest.AttachmentURL);
                    else
                        SMTPManager.SendNotificationEmailToSupport(EmailContent, Subject, false, ToMail, CcMail);
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name ), Ex.Message, Ex.StackTrace, DateTime.Now);
                lblManageErrorMessage.Visible = false;
            }
        }

        protected void ddlCrmStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCrmStatus.SelectedItem.Text == "Closed" || ddlCrmStatus.SelectedItem.Text == "Resolved" || ddlCrmStatus.SelectedItem.Text == "Unable To Resolve" || ddlCrmStatus.SelectedItem.Text == "User Error")
            {
                //trCRMReslnSubject.Visible = true;
                trContentCustomer.Visible = true;
                trAddAttachment.Visible = true;
            }
            else
            {
                trAddAttachment.Visible = false;
                //trCRMReslnSubject.Visible = false;
                trContentCustomer.Visible = false;
            }
        }

        /*  protected void ddlAssignAdmin_SelectedIndexChanged(object sender, EventArgs e)
          {
              if (ddlAssignAdmin.SelectedValue != "0")
              {
                  ddlCrmStatus.SelectedValue = "4";
              }
          }
         */ 
    }
}
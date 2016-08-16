using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;
using WFTCloud.ResuableRoutines;
using WFTCloud.ResuableRoutines.SMTPManager;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace WFTCloud.Customer
{
    public partial class CRMRequests : System.Web.UI.Page
    {
        #region Global Variables and Properties
        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        public string UserMembershipID;
        public string CRMCodeRequestID;
        #endregion

        #region Pageload

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                divManageErrorMessage.Visible = divManageSuccessMessage.Visible = false;
                UserMembershipID = Request.QueryString["userid"];
                CRMCodeRequestID = Request.QueryString["crmid"];
                if (!IsPostBack && UserMembershipID!=null && UserMembershipID != "")
                {
                    btnNewRequest.Visible = true;
                    //Bind Datasource for the Service catogery and service name ddl
                    BindDDLDataSource();
                    //Show records based on pagination value and deleted flag for Manage Crm Issues.
                    ShowManageCrmPaginatedAndDeletedRecords();
                    // Load selected CRM
                    LoadManageCrmIssue();
                    // Load selected CRM history
                    LoadCrmIssueHistory();
                }
                //Load user name in header
                //LoadUserLabel();
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }


        #endregion

        #region ControlEvents

        protected void btnNewRequest_Click(object sender, EventArgs e)
        {
            try
            {
                btnCrmIssueSave.Text = "Save";
                lblCRMHeader.Text = "New CRM Request";
                pnlCRMStatus.Visible = false;
                mvCrmIssues.ActiveViewIndex = 1;
                ddlReportAgainst.Enabled = ddlServiceCategory.Enabled = txtIssue.Enabled = rblCRMRqType.Enabled = fluAddAttachments.Enabled = ddlIssueType.Enabled = txtIssue.Enabled = true;
                trBtnSave.Visible = true;
                txtSubject.Text = "";
                txtIssue.Text = "";
                trAddAttachment.Visible = true;
                trAttachment.Visible = false;
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        protected void btnCrmIssueSave_Click(object sender, EventArgs e)
        {
            try
            {
                string abcd = HttpContext.Current.Request.Url.AbsolutePath;
                string UserMembershipID = Request.QueryString["userid"];
                Guid ID = new Guid(UserMembershipID);
                var user = objDBContext.UserProfiles.FirstOrDefault(a => a.UserMembershipID == ID);
                string name = fluAddAttachments.FileName;
                string OrgName =  DateTime.Now.ToString("ddMMyyyyhhmmss")+name.Replace(" ", "_");

                if (hidCrmRequestID.Value == "")
                {
                    if (user != null)
                    {
                        int CRMCount =objDBContext.CRMRequests.Count() >0? Convert.ToInt32(objDBContext.CRMRequests.Max(count => count.CRMRequestID)):0;
                        int CRMID = CRMCount + 1;
                        if (fluAddAttachments.HasFile)
                        {
                            string EditPath = "";
                            EditPath = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/CRMRequest/" + CRMID + "/");

                            if (!Directory.Exists(EditPath))
                            {
                                Directory.CreateDirectory(EditPath);
                            }
                            fluAddAttachments.SaveAs(EditPath + OrgName);
                        }
                        
                        CRMRequest NewCRM = new CRMRequest();
                        if (fluAddAttachments.HasFile)
                            NewCRM.AttachmentURL = "/UploadedContents/CRMRequest/" + CRMID + "/" + OrgName;
                        NewCRM.LastModifiedBy = NewCRM.CreatedBy = ID;
                        NewCRM.LastModifiedOn = NewCRM.CreatedOn = DateTime.Now;
                        NewCRM.CRMRequestStatus = 1;//Open reffered CRMRequeststatus table
                        NewCRM.CustomerID = ID;
                        NewCRM.IssueType = Convert.ToInt32(ddlIssueType.SelectedValue);
                        NewCRM.IssueDescription = txtIssue.Text;
                        NewCRM.RecordStatus = DBKeys.RecordStatus_Active;
                        NewCRM.CRMSubject = txtSubject.Text;
                        if (trServiceCategory.Visible == true)
                        {
                            NewCRM.ServiceCategoryID = Convert.ToInt32(ddlServiceCategory.SelectedValue);
                            NewCRM.ServiceID = Convert.ToInt32(ddlReportAgainst.SelectedValue);
                        }

                        objDBContext.CRMRequests.AddObject(NewCRM);
                        objDBContext.SaveChanges();
                        int CRMID1 = objDBContext.CRMRequests.Max(a => a.CRMRequestID);

                        SendNotifiactionToSupport(CRMID1);

                        divManageErrorMessage.Visible = false;
                        divManageSuccessMessage.Visible = true;
                        lblCRMCuccess.Text = "CRM Request submitted successfully.";
                    }
                }
                else if (hidCrmRequestID.Value != "")
                {
                    int CRM = Convert.ToInt32(hidCrmRequestID.Value);
                    if (fluAddAttachments.HasFile)
                    {
                        string EditPath = "";
                        EditPath = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/CRMRequest/" + CRM + "/");

                        if (!Directory.Exists(EditPath))
                        {
                            Directory.CreateDirectory(EditPath);
                        }
                        fluAddAttachments.SaveAs(EditPath + OrgName);
                    }
                    var SelCRM = objDBContext.CRMRequests.FirstOrDefault(st => st.CRMRequestID == CRM);
                    if (SelCRM != null)
                    {
                        CRMRequestsHistory CRMHis = new CRMRequestsHistory();
                        CRMHis.CRMRequestID = SelCRM.CRMRequestID;
                        CRMHis.AdminNotes = SelCRM.AdminNotes;
                        CRMHis.AssignedToAdmin = SelCRM.AssignedToAdmin;
                        CRMHis.AttachmentURL = SelCRM.AttachmentURL;
                        CRMHis.CreatedBy = SelCRM.CreatedBy;
                        CRMHis.CreatedOn = SelCRM.CreatedOn;
                        CRMHis.CRMAdminAttachment = SelCRM.CRMAdminAttachment;
                        CRMHis.CRMRequestStatus = SelCRM.CRMRequestStatus;
                        CRMHis.CRMResolutionSent = SelCRM.CRMResolutionSent;
                        CRMHis.CRMResolutionSubject = SelCRM.CRMResolutionSubject;
                        CRMHis.CRMSubject = SelCRM.CRMSubject;
                        CRMHis.CustomerID = SelCRM.CustomerID;
                        CRMHis.IssueDescription = SelCRM.IssueDescription;
                        CRMHis.IssueType = SelCRM.IssueType;
                        CRMHis.LastModifiedBy = SelCRM.LastModifiedBy;
                        CRMHis.LastModifiedOn = SelCRM.LastModifiedOn;
                        CRMHis.RecordStatus = SelCRM.RecordStatus;
                        CRMHis.ServiceCategoryID = SelCRM.ServiceCategoryID;
                        CRMHis.ServiceID = SelCRM.ServiceID;
                        objDBContext.CRMRequestsHistories.AddObject(CRMHis);
                    }
                    if (trServiceCategory.Visible == true)
                    {
                        SelCRM.ServiceCategoryID = Convert.ToInt32(ddlServiceCategory.SelectedValue);
                        SelCRM.ServiceID = Convert.ToInt32(ddlReportAgainst.SelectedValue);
                    }
                    else
                    {
                        SelCRM.ServiceCategoryID = null;
                         SelCRM.ServiceID = null;
                    }
                    SelCRM.AttachmentURL = "/UploadedContents/CRMRequest/" + CRM + "/" + OrgName;
                    SelCRM.IssueType = Convert.ToInt32(ddlIssueType.SelectedValue);
                    SelCRM.IssueDescription = txtIssue.Text;
                    SelCRM.LastModifiedOn = DateTime.Now;
                    SelCRM.AssignedToAdmin = null;
                    SelCRM.CRMRequestStatus = 1;//Open reffered CRMRequeststatus table
                    SelCRM.CRMSubject = txtSubject.Text;
                    objDBContext.SaveChanges();
                    SendNotifiactionToSupport(SelCRM.CRMRequestID);
                    divManageErrorMessage.Visible = false;
                    divManageSuccessMessage.Visible = true;
                    lblCRMCuccess.Text = "CRM Request re-opened successfully.";
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
                divManageErrorMessage.Visible = true;
                divManageSuccessMessage.Visible = false;
                
            }
        }

        private void SendNotifiactionToSupport(int CRMID1)
        {
            var selRequest = objDBContext.vwManageCRMIssues.FirstOrDefault(cat => cat.CRMRequestID == CRMID1);
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
                    Details += "<strong>User Subscription ID : </strong>" + result.UserSubscriptionID+ "<br/>"
                            + "<strong>Subscribed On : </strong>" + (result.CreatedOn != null ? (Convert.ToDateTime(result.CreatedOn).ToString("dd-MMM-yyyy")) : "-") + "<br/>"
                            + "<strong>Service/SAP UserName : </strong>"+(result.ServiceUserName.IsValid() ? result.ServiceUserName:"-") +"<br/>"
                            + "<strong>Service/SAP Password : </strong>" + (result.ServicePassword.IsValid() ? result.ServicePassword : "-") + "<br/>"
                            + "<strong>Service/SAP URL : </strong>"+(result.ServiceUrl.IsValid()?"<a href='"+result.ServiceUrl+"'"+">Click Here</a><br/>":"-<br/>")
                            + "<strong>Service Other Information : </strong>"+ result.ServiceOtherInformation+"<br/>"
                            + "<strong>Expiration Date : </strong>" + (result.ExpirationDate!= null?(Convert.ToDateTime(result.ExpirationDate).ToString("dd-MMM-yyyy")):"-") + "<br/>"
                            + "<strong>Subscribtion Status : </strong>" + ShowSubscribedServiceStatus(result.RecordStatus,result.ExpirationDate.ToString(),Convert.ToBoolean(result.AutoProvisioningDone)) + "<br/><br/>";
                }
            }
            Details += "<strong>Issue Type : </strong>" + selRequest.CRMIssueTypeDesc + "<br/>"
                        + "<strong>Issue Description : </strong>" + selRequest.IssueDescription + "<br/>";

            EmailContent = EmailContent.Replace("++AddContentHere++", Details).Replace("++name++", (selRequest.CustomerFullName + " (" + selRequest.EmailID + ") ")).Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
            string Subject = selRequest.CRMSubject;
            if (!Subject.IsValid())
                Subject = "CRM Request Status - CRM" + CRMID1.ToString();
            string ToMail = "";
            if (selRequest.CRMIssueTypeDesc.ToLower().Contains("sale"))
            {
                ToMail = objDBContext.WftSettings.FirstOrDefault(s => s.SettingKey == "SALES_EMAIL").SettingValue;
            }
            else
            {
                ToMail = objDBContext.WftSettings.FirstOrDefault(s => s.SettingKey == "TECH_EMAIL").SettingValue;
            }

            string CcMail = objDBContext.WftSettings.FirstOrDefault(s => s.SettingKey == "SUPPORT_MAIL").SettingValue;
            if (selRequest.AttachmentURL != null)
                SendEmailAttachment(EmailContent, Subject, ToMail,CcMail, false, true, selRequest.AttachmentURL);
            else
                SendEmailAttachment(EmailContent, Subject, ToMail, CcMail, false, true, "");
        }

        private void LoadManageCrmIssue()
        {
            try
            {
                string UserMembershipID = Request.QueryString["userid"];
                Guid ID = new Guid(UserMembershipID);
                string CRMRequestID = Request.QueryString["crmid"];
                if (Request.QueryString["crmid"].IsValid() && Request.QueryString["Crmhistoryid"].IsValid() == false)
                {
                    int RequestID = int.Parse(CRMRequestID);
                    var selRequest = objDBContext.vwManageCRMIssues.FirstOrDefault(cat => cat.CRMRequestID == RequestID && cat.CustomerID == ID);
                    if (selRequest != null)
                    {
                        dvCRMHistory.Visible = false;
                        var CRmHistories = objDBContext.vwCRMIssueHistoryDetails.Where(cat => cat.CRMRequestID == RequestID);
                        if (CRmHistories.Count()>0)
                        {
                            dvCRMHistory.Visible = true;
                            rptrHistory.DataSource = CRmHistories;
                            rptrHistory.DataBind();
                        }
                        else
                        {
                            dvCRMHistory.Visible = false;
                        }
                        mvCrmIssues.ActiveViewIndex = 2;
                        if (selRequest.CategoryName == null)
                        {
                            trServiceCategoryVw.Visible = trServiceVw.Visible = false;
                        }
                        else
                        {
                            trServiceCategoryVw.Visible = trServiceVw.Visible = true;
                            lblServiceCategoryvw.Text = selRequest.CategoryName;
                            lblSevicenameVw.Text = selRequest.ServiceName;
                        }
                        lblCRMHeadVw.Text = "CRM Request - CRM" + selRequest.CRMRequestID;
                        lblCRMCodeVw.Text ="CRM"+ selRequest.CRMRequestID;
                        lblIssueDescVw.Text = selRequest.IssueDescription;
                        lblIssueTypeVw.Text = selRequest.CRMIssueTypeDesc;
                        lblCRMStatusVw.Text = selRequest.CRMRequestStatusDesc == "Assigned" ? "Work In Process" : selRequest.CRMRequestStatusDesc;
                        lblSubjectVw.Text = selRequest.CRMSubject;
                        if (!lblSubjectVw.Text.IsValid())
                            lblSubjectVw.Text = "CRM Request Status - CRM" + RequestID.ToString();
                        hidCRMRequetIDVw.Value = CRMRequestID;
                        if (selRequest.AttachmentURL != null)
                        {
                            hypAttachmentVw.Text = "Click Here To View";
                            hypAttachmentVw.NavigateUrl = selRequest.AttachmentURL;
                            hypAttachmentVw.Visible = true;
                            lblNoAttachmentVw.Visible = false;
                        }

                        if (selRequest.CRMRequestStatusDesc == "Resolved" || selRequest.CRMRequestStatusDesc == "Closed" || selRequest.CRMRequestStatusDesc == "Unable To Resolve" || selRequest.CRMRequestStatusDesc == "User Error")
                        {
                            lblResolutionDesc.Text = selRequest.CRMResolutionSent;
                            if (selRequest.CRMAdminAttachment != null)
                            {
                                hypReslnAttach.Text = "Click Here To View";
                                hypReslnAttach.NavigateUrl = selRequest.CRMAdminAttachment;
                                hypReslnAttach.Visible = true;
                                lblNoReslnAttach.Visible = false;
                            }
                            else
                            {
                                hypReslnAttach.NavigateUrl = "";
                                hypReslnAttach.Visible = false;
                                lblNoReslnAttach.Visible = true;
                            }
                            trReslAttach.Visible = true;
                            trResolutionDesc.Visible = true;
                            trEditIfResloved.Visible = true;
                        }
                        else
                        {
                            trReslAttach.Visible = false;
                            trResolutionDesc.Visible = false;
                            trEditIfResloved.Visible = false;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        public void LoadCrmIssueHistory()
        {
            try
            {
                string UserMembershipID = Request.QueryString["userid"];
                Guid ID = new Guid(UserMembershipID);
                string Crmhistoryid = Request.QueryString["Crmhistoryid"];
                if (Request.QueryString["crmid"].IsValid() && Request.QueryString["Crmhistoryid"].IsValid())
                {
                    int HistoryID = int.Parse(Crmhistoryid);
                    var selRequest = objDBContext.vwCRMIssueHistoryDetails.FirstOrDefault(cat => cat.CRMHistoryID == HistoryID && cat.CustomerID == ID);
                    if (selRequest != null)
                    {
                        mvCrmIssues.ActiveViewIndex = 4;
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
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        public void SendEmailAttachment(string messageBody, string subject, string ToMail, string ccMail, bool sendInBCC, bool IsHtml, string attachmentPath)
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
                if (ccMail != string.Empty)
                {
                    mail.CC.Add(ccMail);
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
                    ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
                }

                SmtpServer.Port = int.Parse(ConfigurationManager.AppSettings["SMTPPort"]);
                SmtpServer.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SMTPUsername"], ConfigurationManager.AppSettings["SMTPPassword"]);
                SmtpServer.EnableSsl = bool.Parse(ConfigurationManager.AppSettings["EnableSSL"]);

                SmtpServer.Send(mail);
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void ddlServiceCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string UserMembershipID = Request.QueryString["userid"];
                Guid ID = new Guid(UserMembershipID);
                int selectedCategory = Convert.ToInt32(ddlServiceCategory.SelectedValue);
                var selService = objDBContext.pr_GetServicesByMembershipID(ID).Where(s => s.ServiceCategoryID == selectedCategory).ToList();
                ddlReportAgainst.DataSource = selService;
                ddlReportAgainst.DataTextField = "ServiceName";
                ddlReportAgainst.DataValueField = "ServiceID";
                ddlReportAgainst.DataBind();
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        #endregion

        #region Resuable Routines
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
        private void ShowManageCrmPaginatedAndDeletedRecords()
        {

                try
                {
                    if (Request.QueryString.Count==1 && Request.QueryString["userid"].IsValid())
                    {

                        string UserMembershipID = Request.QueryString["userid"];
                        Guid ID = new Guid(UserMembershipID);
                        var CrmRequestCount = objDBContext.CRMRequests.OrderByDescending(obj => obj.CRMRequestID).Where(crm => crm.CustomerID == ID);
                        if (CrmRequestCount.Count() > 0)
                        {
                            if (UserMembershipID != null)
                            {
                                var CrmRequest = objDBContext.vwManageCRMIssues.OrderBy(obj => obj.CRMRequestID).Where(crm => crm.CustomerID == ID);
                                rptrManageCRMIssue.DataSource = CrmRequest;
                                rptrManageCRMIssue.DataBind();
                            }
                        }
                        var SubServices = objDBContext.UserSubscribedServices.Where(a => a.UserID == ID);
                        if (SubServices.Count() <= 0)
                        {
                            rblCRMRqType.SelectedValue = "0";
                            rblCRMRqType.Enabled = false;
                            hidOrShowCatservicedetails("0");
                        }  
                    }

            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void BindDDLDataSource()
        {
            try
            {
                string UserMembershipID = Request.QueryString["userid"];
                Guid ID = new Guid(UserMembershipID);
                ddlIssueType.DataSource = objDBContext.CRMIssueTypes.OrderBy(o => o.CRMIssueTypeID).Where(st => st.RecordStatus == DBKeys.RecordStatus_Active);
                ddlIssueType.DataTextField = "CRMIssueTypeDesc";
                ddlIssueType.DataValueField = "CRMIssueTypeID";
                ddlIssueType.DataBind();
                var userSubSer = objDBContext.UserSubscribedServices.Where(a => a.UserID == ID);
                if (userSubSer.Count() > 0)
                {
                    var selCat = objDBContext.pr_GetCategoriesByMembershipID(ID).ToList();
                    ddlServiceCategory.DataSource = selCat;
                    ddlServiceCategory.DataTextField = "CategoryName";
                    ddlServiceCategory.DataValueField = "ServiceCategoryID";
                    ddlServiceCategory.DataBind();
                }
                if (ddlServiceCategory.Items.Count != 0)
                {
                    int SelectedCategory = Convert.ToInt32(ddlServiceCategory.SelectedValue);
                    var selService = objDBContext.pr_GetServicesByMembershipID(ID).Where(s => s.ServiceCategoryID == SelectedCategory);
                    ddlReportAgainst.DataSource = selService;
                    ddlReportAgainst.DataTextField = "ServiceName";
                    ddlReportAgainst.DataValueField = "ServiceID";
                    ddlReportAgainst.DataBind();
                    rblCRMRqType.SelectedValue = "1";
                    rblCRMRqType.Enabled = true;
                }
                else
                {
                    rblCRMRqType.Enabled = false;
                    rblCRMRqType.SelectedValue = "0";
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), (System.Reflection.MethodBase.GetCurrentMethod().Name + "-" + Request.QueryString["userid"]), Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        #endregion

        protected void rblCRMRqType_SelectedIndexChanged(object sender, EventArgs e)
        {
            hidOrShowCatservicedetails(rblCRMRqType.SelectedValue);
        }

        public void hidOrShowCatservicedetails(string value)
        {
            if (value == "1")
            {
                trServiceCategory.Visible = trServiceName.Visible = true;
            }
            else
            {
                trServiceCategory.Visible = trServiceName.Visible = false;
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string UserMembershipID = Request.QueryString["userid"];
            Guid ID = new Guid(UserMembershipID);
            string CRMRequestID = Request.QueryString["crmid"];
            if (CRMRequestID != "")
            {
                int RequestID = int.Parse(CRMRequestID);
                var selRequest = objDBContext.vwManageCRMIssues.FirstOrDefault(cat => cat.CRMRequestID == RequestID && cat.CustomerID == ID);
                mvCrmIssues.ActiveViewIndex = 1;
                btnCrmIssueSave.Text = "Reopen CRM";
                lblCRMHeader.Text = "Edit CRM Request - CRM"+selRequest.CRMRequestID;
                pnlCRMStatus.Visible = true;
                hypAttchmentLink.Visible = selRequest.AttachmentURL != null ? true : false;
                hypAttchmentLink.NavigateUrl = selRequest.AttachmentURL != null ? selRequest.AttachmentURL : "#";
                lblNoAttachment.Visible = selRequest.AttachmentURL != null ? false : true;
                mvCrmIssues.ActiveViewIndex = 1;
                BindDDLDataSource();
                lblCRMStatusEdit.Text = selRequest.CRMRequestStatusDesc;
                txtIssue.Text = selRequest.IssueDescription;
                txtSubject.Text = selRequest.CRMSubject;
                hidCrmRequestID.Value = CRMRequestID;
                if (selRequest.ServiceCategoryID != null)
                {
                    ddlServiceCategory.SelectedValue = Convert.ToString(selRequest.ServiceCategoryID);
                    ddlServiceCategory_SelectedIndexChanged(sender, e);
                    ddlReportAgainst.SelectedValue = Convert.ToString(selRequest.ServiceID);
                }
                ddlServiceCategory.Enabled = ddlReportAgainst.Enabled = txtSubject.Enabled = false;
            }
        }

    }
}
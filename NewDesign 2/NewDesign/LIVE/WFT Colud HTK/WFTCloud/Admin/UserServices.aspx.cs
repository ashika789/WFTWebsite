using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.DataAccess;
using WFTCloud.ResuableRoutines;
using WFTCloud.ResuableRoutines.SMTPManager;

namespace WFTCloud.Admin
{
    public partial class UserServices : System.Web.UI.Page
    {
        #region Global Variables and Properties
        cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        public string UserMembershipID;

        #endregion

        #region Pageload

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
                        //UserMembershipID = Request.QueryString["checkserviceid"] == null ? (Request.QueryString["newserviceid"] == null ? (Request.QueryString["crmissueid"] == null ? "abc" : Request.QueryString["crmissueid"]) : Request.QueryString["newserviceid"]) : Request.QueryString["checkserviceid"];
                        UserMembershipID = Request.QueryString["userid"];
                        if (!IsPostBack)
                        {
                            //Show records based on pagination value and deleted flag for Available Services.
                            ShowPaginatedAndDeletedRecords();
                            //Show records based on pagination value and deleted flag for Subscribed Services.
                            ShowSubscribedPaginatedAndDeletedRecords();
                            //Check if the screen should delete any subscribed service from query string parameter.
                            UpdateSubscribedServiceStatus(QueryStringKeys.Delete, DBKeys.RecordStatus_Delete);
                            //Check if the screen should activate any subscribed service from query string parameter.
                            UpdateSubscribedServiceStatus(QueryStringKeys.Activate, DBKeys.RecordStatus_Active);
                            //Check if the screen should deactivate any subscribed service from query string parameter.
                            //Check if the screen should load edit User subscribed services from query string parameter.
                            LoadEditSubscribedService();
                            //check for view User Service Provision Details
                            LoadUserServiceProvision();
                            //Show the tab controls.
                            LoadTabView();
                            UpdateSubscribedServiceStatus(QueryStringKeys.Deactivate, DBKeys.RecordStatus_Inactive);
                            //Show records based on pagination value and deleted flag for Manage Crm Issues.
                            //ShowManageCrmPaginatedAndDeletedRecords();
                            //check for edit crm issues
                            //LoadManageCrmIssue();
                        }
                        //Load user name in header
                        LoadUserLabel();
                    }
                    else
                    {
                        Response.Redirect("/LoginPage.aspx", false);
                    }
                }
                else
                {
                    Response.Redirect("/LoginPage.aspx", false);
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        #endregion

        #region ControlEvents

        protected void btnUSPDSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.UserServiceProvision].IsValid())
                {
                    string AttachmentFileName = "";
                    string imageFileName = "";
                    divUSPDErrorMessage.Visible = false;
                    int UserSubscriptionID = int.Parse(Request.QueryString[QueryStringKeys.UserServiceProvision]);
                    var selServiceProvision = objDBContext.UserSubscribedServices.FirstOrDefault(cat => cat.UserSubscriptionID == UserSubscriptionID);
                    if (selServiceProvision != null)
                    {
                        if (fluUSPDAddAttachments.HasFile)
                        {
                            string type = fluUSPDAddAttachments.FileName.Substring(fluUSPDAddAttachments.FileName.LastIndexOf('.') + 1).ToLower();
                            if (type == "jpg" || type == "jpeg" || type=="png")
                            {
                                string EditPath = "";
                                string DeletePath = "";
                                EditPath = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/UserServiceProvisions/" + UserSubscriptionID + "/");
                                DeletePath = ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/UserServiceProvisions/" + UserSubscriptionID;
                                //if (Directory.Exists(EditPath))
                                //{
                                //    DirectoryInfo Folder = new DirectoryInfo(Server.MapPath(DeletePath));
                                //    if (Folder.Exists)
                                //    {
                                //        foreach (FileInfo fl in Folder.GetFiles())
                                //        {
                                //            fl.Delete();
                                //        }
                                //        Folder.Delete();
                                //    }
                                //}
                                if (!Directory.Exists(EditPath))
                                {
                                    Directory.CreateDirectory(EditPath);
                                }
                                imageFileName = DateTime.Now.ToString("ddMMyyyyhhmmss") + fluUSPDAddAttachments.FileName.Replace(" ", "_");
                                fluUSPDAddAttachments.SaveAs(EditPath + imageFileName);
                            }
                            else
                            {
                                divUSPDSuccessMessage.Visible = false;
                                divUSPDErrorMessage.Visible = true;
                                lblUSPDError.Text = "Only jpg, jpeg or png formats are supported for Upload. Please try uploading valid documents.";
                            }
                        }
                        
                        if (fluUSPDAddAtta.HasFile)
                        {
                                string EditPath = "";
                                string DeletePath = "";
                                EditPath = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/UserServiceProvisions/" + UserSubscriptionID + "/");
                                DeletePath = ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/UserServiceProvisions/" + UserSubscriptionID;
                                //if (Directory.Exists(EditPath))
                                //{
                                //    DirectoryInfo Folder = new DirectoryInfo(Server.MapPath(DeletePath));
                                //    if (Folder.Exists)
                                //    {
                                //        foreach (FileInfo fl in Folder.GetFiles())
                                //        {
                                //            fl.Delete();
                                //        }
                                //        Folder.Delete();
                                //    }
                                //}
                                if (!Directory.Exists(EditPath))
                                {
                                    Directory.CreateDirectory(EditPath);
                                }
                                AttachmentFileName = DateTime.Now.ToString("ddMMyyyyhhmmss") + fluUSPDAddAtta.FileName.Replace(" ", "_");
                                fluUSPDAddAtta.SaveAs(EditPath + AttachmentFileName );
                        }

                        if (divUSPDErrorMessage.Visible == false)
                        {
                            string ImageURL = "";
                            ImageURL = "/UploadedContents/UserServiceProvisions/" + UserSubscriptionID + "/" + imageFileName;
                            if (fluUSPDAddAtta.HasFile)
                            {
                                selServiceProvision.FilePath = "/UploadedContents/UserServiceProvisions/" + UserSubscriptionID + "/" +AttachmentFileName;
                            }
                            //selServiceProvision.ExpirationDate = Convert.ToDateTime(txtUSPDExpirationDate.Text);
                            //selServiceProvision.ExpirationPeriod = Convert.ToInt32(ddlExpirationPeriod.SelectedValue);
                            selServiceProvision.InstanceNumber = txtInstanceNumber.Text;
                            selServiceProvision.UIDOnServer = txtUSPDUIDOnServer.Text;
                            selServiceProvision.ApplicationServer = txtApplicationServer.Text;
                            selServiceProvision.DeveloperKey = txtDeveloperKey.Text;
                            if (fluUSPDAddAttachments.HasFile)
                            {
                                selServiceProvision.ImagePath = ImageURL;
                            }
                             
                            if (selServiceProvision.AutoProvisioningDone != true && selServiceProvision.RecordStatus == 1)
                            {
                                selServiceProvision.ServiceUserName = txtUSPDServiceUserName.Text;
                                selServiceProvision.ServicePassword = txtUSPDServicePassword.Text;
                                selServiceProvision.ServiceUrl = txtUSPDServiceURL.Text;
                                selServiceProvision.ServiceOtherInformation = txtUSPDOtherInormation.Text;
                                selServiceProvision.AutoProvisioningDone = true;
                                objDBContext.SaveChanges();
                                LoadUserServiceProvision();
                                string ServiceDetails1 = "";
                                string ServiceDetailstoSupport = "";
                                var serviceDetails = objDBContext.ServiceCatalogs.FirstOrDefault(scat => scat.ServiceID == selServiceProvision.ServiceID);

                                //var ServiceProvisioningDEtails = objDBContext.ServiceProvisions.FirstOrDefault(sp => sp.ServiceCategoryID == objSubscribedService.ServiceCategoryID && sp.ServiceID == objSubscribedService.ServiceID);
                                var userProf = objDBContext.UserProfiles.FirstOrDefault(d => d.UserProfileID == selServiceProvision.UserProfileID);
                                string UserFullName = userProf.FirstName + " " + userProf.MiddleName + " " + userProf.LastName;
                                //var APP_URL = objDBContext.WftSettings.FirstOrDefault(a => a.SettingKey == "APP_URL");

                                //ServiceDetails1 = ("<strong>Service Name :</strong> " + serviceDetails.ServiceName + "<br />"
                                //                  + "<strong>Service/SAP UserName :</strong>" + selServiceProvision.ServiceUserName + "<br />"
                                //                  + "<strong>Service/SAP Password :</strong>" + selServiceProvision.ServicePassword + "<br />"
                                //                  + (APP_URL != null ? ("<strong>Service/SAP URL :</strong><a href=" + APP_URL.SettingValue + " target='_blank'>" + "Click Here" + "</a>") : "") + "<br />"
                                //                  + "<strong>Service Other Information :</strong>" + selServiceProvision.ServiceOtherInformation + "<br /><br />");


                                var APP_URL = objDBContext.WftSettings.FirstOrDefault(a => a.SettingKey == "APP_URL");


                                var userOrder = objDBContext.UserOrders.FirstOrDefault(order => order.UserOrderID == selServiceProvision.UserOrderID && order.OrderStatus == DBKeys.RecordStatus_Active);
                                var UserNextPaymentDate = objDBContext.AllAutomatedPayments.FirstOrDefault(a => a.UserSubscriptionID == UserSubscriptionID);
                                var ussinfo = objDBContext.UserPaymentTransactions.FirstOrDefault(a => a.InvoiceNumber == userOrder.InvoiceNumber);

                                string TransactionID = string.Empty;
                                string PaymentMethod = string.Empty;
                                string NextBillDate = string.Empty;
                                if (serviceDetails.InitialHoldAmount > 0M)
                                {
                                    if (userOrder.PaymentMethod == "Authorize.net")
                                    {
                                        TransactionID = ussinfo.AuthTransactionID;
                                        PaymentMethod = "Authorize.net";
                                    }
                                    else
                                    {
                                        TransactionID = ussinfo.PalpalPaymentTransactionID;
                                        PaymentMethod = "PayPal";
                                    }
                                }
                                else
                                {
                                    TransactionID = "-";
                                    PaymentMethod = "-";
                                }

                                if (UserNextPaymentDate != null)
                                {
                                    NextBillDate = UserNextPaymentDate.PaymentDate.ToString("dd-MMM-yy");
                                }
                                else
                                {
                                    NextBillDate = "NA";
                                }
                                ServiceDetails1 = ("<table border='1'><tr><td rowspan='7'><strong>Subscription Information</strong></td>"
                                    + "<td><strong>Subscription ID </strong></td><td>" + UserSubscriptionID + "<br /></td></tr>"
                                    + "<tr><td><strong>User Name </strong></td><td>" + UserFullName + "<br /></td></tr>"
                                    + "<tr><td><strong>User Email </strong></td><td>" + userProf.EmailID + "<br /></td></tr>"
                                    + "<tr><td><strong>Payment For </strong></td><td>" + "New Subscription" + "<br /></td></tr>"
                                    + "<tr><td><strong>Service Category </strong></td><td>" + serviceDetails.SystemType + "<br /></td></tr>"
                                    + "<tr><td><strong>Service Name </strong></td><td>" + serviceDetails.ServiceName + "<br /></td></tr>"
                                    + "<tr><td><strong>Release Version </strong></td><td>" + serviceDetails.ReleaseVersion + "<br /></td></tr>"
                                    + "<tr><td rowspan='9'><strong>Payment Transaction Information</strong></td><td><strong>Subscription Purchased Date </strong></td><td>" + userOrder.OrderDateTime.ToString("dd-MMM-yy") + "<br /></td></tr>"
                                    + "<tr><td><strong>Initial Payment Date </strong></td><td>" + userOrder.OrderDateTime.ToString("dd-MMM-yy") + "<br /></td></tr>"
                                    + "<tr><td><strong>Recurring Billing Date </strong></td><td>" + NextBillDate + "<br /></td></tr>"
                                     + "<tr><td><strong>Cancel Date </strong></td><td>" + "Prior to Recurring Billing Date" + "<br /></td></tr>"
                                    + "<tr><td><strong>Payment Mode </strong></td><td>" + PaymentMethod + "<br /></td></tr>"
                                    + "<tr><td><strong>Invoice Number </strong></td><td>" + userOrder.InvoiceNumber + "<br /></td></tr>"
                                    + "<tr><td><strong>Transaction ID </strong></td><td>" + TransactionID + "<br /></td></tr>"
                                    + "<tr><td><strong>Service Amount </strong>"
                                    + "</td><td>" + serviceDetails.InitialHoldAmount + "<br /></td></tr>"
                                    + "<tr><td><strong>Payment Status </strong></td><td>" + "Success" + "<br /></td></tr>"
                                    + "<tr><td rowspan='8'><strong>SAP Credentials</strong></td><td><strong>Instance Number </strong></td><td>" + selServiceProvision.InstanceNumber + "<br /></td></tr>"
                                    + "<tr><td><strong>Application Server </strong></td><td>" + (selServiceProvision.ApplicationServer != null ? selServiceProvision.ApplicationServer : " ") + "<br />"
                                    + "</td></tr>"
                                    + "<tr><td><strong>SID </strong></td><td>" + selServiceProvision.UIDOnServer + "<br /></td></tr>"
                                    + "<tr><td><strong>UserName </strong></td><td>" + selServiceProvision.ServiceUserName + "<br /></td></tr>"
                                    + "<tr><td><strong>Password </strong></td><td>" + selServiceProvision.ServicePassword + "<br /></td></tr>"
                                    + "<tr><td><strong>Developer Key </strong></td><td>" + selServiceProvision.DeveloperKey + "<br /></td></tr>" + (APP_URL != null ? ("<tr><td><strong>WebURL </strong></td><td><a href=" + APP_URL.SettingValue + " target='_blank'>" + "Click Here" + "</a>"
                                    + "</td></tr>") : "") + "</table>");

                                ServiceDetails1 += ("<table><tr><td><strong>Other Service Information:</strong></td></table>");

                                ServiceDetails1 += ("<table border='1'><tr><td>" + selServiceProvision.ServiceOtherInformation + "<br /></td></tr></table>");

                                ServiceDetailstoSupport = ("<table border='1'><tr><td rowspan='7'><strong>Subscription Information</strong></td>"
                                   + "<td><strong>Subscription ID </strong></td><td>" + UserSubscriptionID + "<br /></td></tr>"
                                   + "<tr><td><strong>User Name </strong></td><td>" + UserFullName + "<br /></td></tr>"
                                   + "<tr><td><strong>User Email </strong></td><td>" + userProf.EmailID + "<br /></td></tr>"
                                   + "<tr><td><strong>Payment For </strong></td><td>" + "New Subscription" + "<br /></td></tr>"
                                   + "<tr><td><strong>Service Category </strong></td><td>" + serviceDetails.SystemType + "<br /></td></tr>"
                                   + "<tr><td><strong>Service Name </strong></td><td>" + serviceDetails.ServiceName + "<br /></td></tr>"
                                   + "<tr><td><strong>Release Version </strong></td><td>" + serviceDetails.ReleaseVersion + "<br /></td></tr>"
                                   + "<tr><td rowspan='8'><strong>SAP Credentials</strong></td><td><strong>Instance Number </strong></td><td>" + selServiceProvision.InstanceNumber + "<br /></td></tr>"
                                   + "<tr><td><strong>Application Server </strong></td><td>" + (selServiceProvision.ApplicationServer != null ? selServiceProvision.ApplicationServer : " ") + "<br />"
                                   + "</td></tr>"
                                   + "<tr><td><strong>SID </strong></td><td>" + selServiceProvision.UIDOnServer + "<br /></td></tr>"
                                   + "<tr><td><strong>UserName </strong></td><td>" + selServiceProvision.ServiceUserName + "<br /></td></tr>"
                                   + "<tr><td><strong>Password </strong></td><td>" + selServiceProvision.ServicePassword + "<br /></td></tr>"
                                   + "<tr><td><strong>Developer Key </strong></td><td>" + selServiceProvision.DeveloperKey + "<br /></td></tr>" + (APP_URL != null ? ("<tr><td><strong>WebURL </strong></td><td><a href=" + APP_URL.SettingValue + " target='_blank'>" + "Click Here" + "</a>"
                                   + "</td></tr>") : "") + "</table>");

                                ServiceDetailstoSupport += ("<table><tr><td><strong>Other Service Information:</strong></td></table>");

                                ServiceDetailstoSupport += ("<table border='1'><tr><td>" + selServiceProvision.ServiceOtherInformation + "<br /></td></tr></table>");

                                //*******
                                //Sent Email Notification to the Customer 
                                //******
                                string CustomerEmailContent = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["ServiceReadyToCustomer"]));//"/wftcloud/UploadedContents/EmailTemplates/Service-Ready.html"));
                                CustomerEmailContent = CustomerEmailContent.Replace("++UserName++", UserFullName);
                                CustomerEmailContent = CustomerEmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                                string CustomerContent = CustomerEmailContent.Replace("++AddContentHere++", ServiceDetails1).Replace("++name++", UserFullName);
                                CustomerContent = CustomerContent.Replace("++ServiceDescription++", serviceDetails.ServiceDescription);
                                string path1="";
                                string img = "";
                                string AttachmentPath = "";
                                if (selServiceProvision.ImagePath != null)
                                {
                                    path1 = ConfigurationManager.AppSettings["DomainNameForImageBindInMail"] + selServiceProvision.ImagePath;
                                    img = " <img alt='Logo' src='" + path1 + "' border='0' vspace='0' hspace='0' style='display:block; max-width:100%; height:auto !important;' /><br /><br />";
                                }

                                CustomerContent = CustomerContent.Replace("++AttachmentURL++", img);
                                if (selServiceProvision.FilePath != null)
                                {
                                        AttachmentPath = selServiceProvision.FilePath;
                                        //SMTPManager.SendEmail(CustomerContent, "Service Provisioning Details on WFTCloud.com", userProf.EmailID, false, true);
                                        SendEmailAttachment(CustomerContent, "Service Provisioning Details on WFTCloud.com", userProf.EmailID, false, true, AttachmentPath);
                                }
                                else
                                {
                                    SMTPManager.SendEmail(CustomerContent, "Service Provisioning Details on WFTCloud.com", userProf.EmailID, false, true);
                                }
                                //*******
                                //Sent Email Notification to the admin about ServiceProvision details provided to customer 
                                //******

                                string AdminEmailContent = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["ServiceProvisionSuccessNotificationToAdmin"]));//"/wftcloud/UploadedContents/EmailTemplates/ServiceProvisionSuccessNotificationToAdmin.html"));
                                AdminEmailContent = AdminEmailContent.Replace("++UserName++", UserFullName + " (" + userProf.EmailID + ") ");
                                AdminEmailContent = AdminEmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                                string AdminContent = AdminEmailContent.Replace("++AddContentHere++", ServiceDetails1);
                                AdminContent = AdminContent.Replace("++ServiceDescription++", serviceDetails.ServiceDescription);
                                AdminContent = AdminContent.Replace("++AttachmentURL++", img);

                                string SupportContent = AdminEmailContent.Replace("++AddContentHere++", ServiceDetailstoSupport);
                                SupportContent = SupportContent.Replace("++ServiceDescription++", serviceDetails.ServiceDescription);
                                SupportContent = SupportContent.Replace("++AttachmentURL++", img);


                                if (AttachmentPath == "")
                                {
                                    SMTPManager.SendAdminNotificationEmail(AdminContent, "Manual Provisioning Completed for - " + UserFullName, false);

                                    SMTPManager.SendSAPBasisNotificationEmail(SupportContent, "Manual Provisioning Completed for - " + UserFullName, false);
                                }
                                else
                                {
                                    SendAdminNotificationAttachmentEmail(AdminContent, "Manual Provisioning Completed for - " + UserFullName, false, AttachmentPath);
                                    SendSAPBasisNotificationAttachmentEmail(SupportContent, "Manual Provisioning Completed for - " + UserFullName, false, AttachmentPath);
                                }
                            }
                            objDBContext.SaveChanges();
                            divUSPDErrorMessage.Visible = false;
                            divUSPDSuccessMessage.Visible = true;
                        }
                    }
                    else
                    {
                        divUSPDSuccessMessage.Visible = false;
                        divUSPDErrorMessage.Visible = true;
                        lblUSPDError.Text = "There are no User Service Provision Details.";
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        public void SendAdminNotificationAttachmentEmail(string messageBody, string subject, bool sendInBCC, string attachmentPath)
        {
            try
            {
                cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"]);
                string FromMail = Convert.ToString(ConfigurationManager.AppSettings["FromEmail"]);
                mail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"]);
                string ToMail = objDBContext.WftSettings.FirstOrDefault(s => s.SettingsID == DBKeys.Admin_Email).SettingValue;
                if (!sendInBCC)
                    mail.To.Add(ToMail);//ConfigurationManager.AppSettings["SMTPUsernameAdminMail"]);
                else
                {
                    mail.Bcc.Add(ToMail);//ConfigurationManager.AppSettings["SMTPUsernameAdminMail"]);
                }
                try
                {
                    // Add attachment
                    attachmentPath = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + attachmentPath);
                    if (File.Exists(attachmentPath))
                    { mail.Attachments.Add(new Attachment(attachmentPath)); }
                }
                catch (Exception Ex)
                {
                    ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                }
                mail.Subject = subject;
                mail.Body = messageBody;
                mail.IsBodyHtml = true;
                SmtpServer.Port = int.Parse(ConfigurationManager.AppSettings["SMTPPort"]);
                SmtpServer.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SMTPUsernameAdminNotification"], ConfigurationManager.AppSettings["SMTPPasswordAdminNotification"]);
                SmtpServer.EnableSsl = bool.Parse(ConfigurationManager.AppSettings["EnableSSL"]);

                SmtpServer.Send(mail);
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        public void SendSAPBasisNotificationAttachmentEmail(string messageBody, string subject, bool sendInBCC, string attachmentPath)
        {
            try
            {
                cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"]);
                string FromMail = Convert.ToString(ConfigurationManager.AppSettings["FromEmail"]);
                mail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"]);
                string ToMail = objDBContext.WftSettings.FirstOrDefault(s => s.SettingsID == DBKeys.SAPBasis_Email).SettingValue;
                if (!sendInBCC)
                    mail.To.Add(ToMail);//ConfigurationManager.AppSettings["SMTPUsernameAdminMail"]);
                else
                {
                    mail.Bcc.Add(ToMail);//ConfigurationManager.AppSettings["SMTPUsernameAdminMail"]);
                }
                try
                {
                    // Add attachment
                    attachmentPath = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + attachmentPath);
                    if (File.Exists(attachmentPath))
                    { mail.Attachments.Add(new Attachment(attachmentPath)); }
                }
                catch (Exception Ex)
                {
                    ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                }
                mail.Subject = subject;
                mail.Body = messageBody;
                mail.IsBodyHtml = true;
                SmtpServer.Port = int.Parse(ConfigurationManager.AppSettings["SMTPPort"]);
                SmtpServer.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SMTPUsernameAdminNotification"], ConfigurationManager.AppSettings["SMTPPasswordAdminNotification"]);
                SmtpServer.EnableSsl = bool.Parse(ConfigurationManager.AppSettings["EnableSSL"]);

                SmtpServer.Send(mail);
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        private void LoadUserLabel()
        {
            try
            {
                string UserMembershipID = Request.QueryString["userid"];
                Guid ID = new Guid(UserMembershipID);
                string UserName = objDBContext.vwUsersListWithFullNames.FirstOrDefault(name => name.UserMembershipID == ID).FullName;
                lblUserName.Text = UserName;
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void btnAddService_Click(object sender, EventArgs e)
        {
            Button rItem = ((Button)(sender));
            GetAddNewServicesForCheckbox(rItem);
        }

        private void GetAddNewServicesForCheckbox(Control rptr)
        {
            try
            {
                RepeaterItem VisibledRepeater = ((RepeaterItem)(rptr.NamingContainer));
                Repeater Repeater1 = VisibledRepeater.FindControl("rptrWFTCloudPackages") as Repeater;
                Repeater Repeater2 = VisibledRepeater.FindControl("rptrWFTCloudNonPayAsYouGo") as Repeater;


                divAvaSuccessMessage.Visible = false;
                string UserMembershipID = Request.QueryString["userid"];
                Guid ID = new Guid(UserMembershipID);
                var user = objDBContext.UserProfiles.FirstOrDefault(a => a.UserMembershipID == ID);
                int UserProfileID = Convert.ToInt32(user.UserProfileID);
                if (Repeater1 != null)
                {
                    foreach (RepeaterItem rItem in Repeater1.Items)
                    {
                        CheckBox chkSelect = (rItem.FindControl("chkSelect") as CheckBox);
                        HiddenField hdnNewServiceID = (rItem.FindControl("hdnNewServicesID") as HiddenField);

                        DropDownList ddlDiscount = rItem.FindControl("ddlPriceModel") as DropDownList;
                        bool PriceModel = false;
                        if (ddlDiscount.Visible == true)
                            PriceModel = ddlDiscount.SelectedValue == "0" ? false : true;

                        if (hdnNewServiceID.Value != "")
                        {
                            UserCart objusercart = new UserCart();
                            int ServiceID = int.Parse(hdnNewServiceID.Value);
                            if (chkSelect.Checked)
                            {
                                if (objDBContext.UserCarts.Any(ot => ot.ServiceID == ServiceID && ot.RecordStatus == 999 && ot.UserProfileID == UserProfileID && ot.CreatedBy == ID && ot.IsPayAsYouGo == PriceModel))
                                {
                                    var UserService = objDBContext.UserCarts.FirstOrDefault(ot => ot.ServiceID == ServiceID && ot.UserProfileID == UserProfileID && ot.RecordStatus == 999 && ot.CreatedBy == ID && ot.IsPayAsYouGo == PriceModel);
                                    int QunatityCount = objDBContext.UserCarts.FirstOrDefault(qan => qan.ServiceID == ServiceID).Quantity;
                                    UserService.Quantity = QunatityCount + 1;
                                    UserService.ModifiedOn = DateTime.Now;
                                    objusercart.ModifiedBy = ID;
                                }
                                else
                                {
                                    objusercart.ServiceID = int.Parse(hdnNewServiceID.Value);
                                    objusercart.UserProfileID = Convert.ToInt32(user.UserProfileID);
                                    objusercart.CreatedOn = DateTime.Now;
                                    objusercart.CreatedBy = ID;
                                    objusercart.RecordStatus = 999;
                                    objusercart.SelectedDiscount = 0;
                                    objusercart.IsPayAsYouGo = PriceModel;
                                    objDBContext.UserCarts.AddObject(objusercart);
                                    divAvaSuccessMessage.Visible = true;
                                    lblAvaSuccessmsg.Text = "Service added to user cart.";
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (RepeaterItem rItem in Repeater2.Items)
                    {
                        CheckBox chkSelect = (rItem.FindControl("chkSelect") as CheckBox);
                        HiddenField hdnNewServiceID = (rItem.FindControl("hdnNewServicesID") as HiddenField);

                        DropDownList ddlDiscount = rItem.FindControl("ddlPriceModel") as DropDownList;
                        bool PriceModel = false;
                        if (ddlDiscount.Visible == true)
                            PriceModel = ddlDiscount.SelectedValue == "0" ? false : true;

                        if (hdnNewServiceID.Value != "")
                        {
                            UserCart objusercart = new UserCart();
                            int ServiceID = int.Parse(hdnNewServiceID.Value);
                            if (chkSelect.Checked)
                            {
                                if (objDBContext.UserCarts.Any(ot => ot.ServiceID == ServiceID && ot.RecordStatus == 999 && ot.UserProfileID == UserProfileID && ot.CreatedBy == ID && ot.IsPayAsYouGo == PriceModel))
                                {
                                    var UserService = objDBContext.UserCarts.FirstOrDefault(ot => ot.ServiceID == ServiceID && ot.UserProfileID == UserProfileID && ot.RecordStatus == 999 && ot.CreatedBy == ID && ot.IsPayAsYouGo == PriceModel);
                                    int QunatityCount = objDBContext.UserCarts.FirstOrDefault(qan => qan.ServiceID == ServiceID).Quantity;
                                    UserService.Quantity = QunatityCount + 1;
                                    UserService.ModifiedOn = DateTime.Now;
                                    objusercart.ModifiedBy = ID;
                                }
                                else
                                {
                                    objusercart.ServiceID = int.Parse(hdnNewServiceID.Value);
                                    objusercart.UserProfileID = Convert.ToInt32(user.UserProfileID);
                                    objusercart.CreatedOn = DateTime.Now;
                                    objusercart.CreatedBy = ID;
                                    objusercart.RecordStatus = 999;
                                    objusercart.SelectedDiscount = 0;
                                    objusercart.IsPayAsYouGo = PriceModel;
                                    objDBContext.UserCarts.AddObject(objusercart);
                                    divAvaSuccessMessage.Visible = true;
                                    lblAvaSuccessmsg.Text = "Service added to user cart.";
                                }
                            }
                        }
                    }
                }
                objDBContext.SaveChanges();
                divAvaErrorMessage.Visible = false;
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void LoadTabView()
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.ShowView].IsValid())
                {
                    string View = (Request.QueryString[QueryStringKeys.ShowView]);
                    if (View == "AvailableService")
                    {
                        liAvailable.Attributes.Add("class", "active");
                        Available.Attributes.Add("class", "tab-pane in active");
                    }
                    else if (View == "SubscribedService")
                    {
                        liSubscribed.Attributes.Add("class", "active");
                        Subscribed.Attributes.Add("class", "tab-pane in active");
                    }
                    //else if (View == "ManageCrmIssue")
                    //{
                    //    liManage.Attributes.Add("class", "active");
                    //    Manage.Attributes.Add("class", "tab-pane in active");
                    //}
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void btnSubscribedSave_Click(object sender, EventArgs e)
        {
            try
            {
                int SubscribedServiceID = int.Parse(lblUserSubscriptionID.Text);
                var selSubscribedService = GetSubscribedServiceFromID(SubscribedServiceID);
                //If new service then add to dbcontext
                if (selSubscribedService == null)
                {
                    lblSubscribedErrorMessage.Visible = true;
                    return;
                }
                else
                {
                    LoadSubscribedDataFromFields(selSubscribedService);
                    objDBContext.SaveChanges();
                }

            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                lblSubscribedErrorMessage.Visible = true;
            }
        }



        private void LoadEditSubscribedService()
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.EditSubscribedService].IsValid())
                {
                    int ServiceID = int.Parse(Request.QueryString[QueryStringKeys.EditSubscribedService]);
                    var selService = objDBContext.UserSubscribedServices.FirstOrDefault(cat => cat.UserSubscriptionID == ServiceID);
                    var ServiceCatalogDetails = objDBContext.ServiceCatalogs.FirstOrDefault(a => a.ServiceID == selService.ServiceID);
                    int CatID = selService.ServiceCategoryID;
                    var ServiceCategoryDetails = objDBContext.ServiceCategories.FirstOrDefault(c => c.ServiceCategoryID == CatID);
                    if (selService != null)
                    {
                        mvSubscribed.ActiveViewIndex = 1;
                        lblServiceID.Text = Convert.ToString(selService.ServiceID);
                        lblUserSubscriptionID.Text = Convert.ToString(selService.UserSubscriptionID);
                        txtUserName.Text = selService.ServiceUserName;
                        txtServicePassword.Text = selService.ServicePassword;
                        txtCouponCode.Text = Convert.ToString(selService.CouponCode);
                        txtServiceCategory.Text = ServiceCategoryDetails.CategoryName;
                        txtServiceName.Text = ServiceCatalogDetails.ServiceName;
                        txtInitialHoldAmount.Text = selService.InitialHoldAmount.ToString();
                        txtUsageUnit.Text = selService.UsageUnit;
                        txtServiceURL.Text = selService.ServiceUrl;
                        txtServiceInformation.Text = selService.ServiceOtherInformation;
                        lblExpDate.Text = Convert.ToDateTime(selService.ExpirationDate).ToString("dd-MMM-yyyy");
                        if (selService.AutoProvisioningDone == true)
                        {
                            txtServicePassword.ReadOnly = txtUserName.ReadOnly = txtServiceURL.ReadOnly = txtServiceInformation.ReadOnly = true;
                            trServicePassword.Visible = false;
                        }
                        else
                        {
                            txtServicePassword.ReadOnly = txtUserName.ReadOnly = txtServiceURL.ReadOnly = txtServiceInformation.ReadOnly = false;
                            trServicePassword.Visible = true;
                        }
                        if (ServiceCatalogDetails.IsPayAsYouGo == false)
                        {
                            trWFTCloudPrice.Visible = false;
                        }
                        else
                        {
                            trWFTCloudPrice.Visible = true;
                            txtWFTCloudPrice.Text = selService.WFTCloudPrice.ToString();
                        }
                        txtIPAddress.Text = Convert.ToString(selService.ServiceIPAddress);

                        int status = selService.RecordStatus;
                        if (status == 1)
                        {
                            ddlRecordStatus.SelectedValue = "1";
                        }
                        else if (status == 0)
                        {
                            ddlRecordStatus.SelectedValue = "0";
                        }
                        else if (status == -1)
                        {
                            ddlRecordStatus.SelectedValue = "-1";
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void LoadUserServiceProvision()
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.UserServiceProvision].IsValid())
                {
                    int UserSubscriptionID = int.Parse(Request.QueryString[QueryStringKeys.UserServiceProvision]);
                    var selServiceProvision = objDBContext.vwManageUserServiceProvisions.FirstOrDefault(cat => cat.UserSubscriptionID == UserSubscriptionID);
                    if (selServiceProvision != null)
                    {
                        mvSubscribed.ActiveViewIndex = 3;
                        lblSubscriptionID.Text = selServiceProvision.UserSubscriptionID.ToString();
                        lblUSPDCategoryName.Text = selServiceProvision.CategoryName;
                        lblUSPDServiceName.Text = selServiceProvision.ServiceName;
                        lblUSPDUserName.Text = selServiceProvision.EmailID;
                        txtUSPDServiceUserName.Text = selServiceProvision.ServiceUserName;
                        txtUSPDServicePassword.Text = selServiceProvision.ServicePassword;
                        txtUSPDServiceURL.Text = selServiceProvision.ServiceUrl;
                        txtUSPDUIDOnServer.Text = selServiceProvision.UIDOnServer;
                        txtUSPDOtherInormation.Text = selServiceProvision.ServiceOtherInformation;
                        txtUSPDExpirationDate.Text = Convert.ToDateTime(selServiceProvision.ExpirationDate).ToString("dd-MMM-yyyy");
                        //ddlExpirationPeriod.SelectedValue = Convert.ToString(selServiceProvision.ExpirationPeriod);

                        if (selServiceProvision.ImagePath != null && selServiceProvision.ImagePath != "")
                        {
                            hlAttachment.Visible = true;
                            hlAttachment.Text = "Click here to view attachment";
                            hlAttachment.NavigateUrl = selServiceProvision.ImagePath;
                        }
                        else
                        {
                            hlAttachment.Visible = false;
                        }
                        if (selServiceProvision.FilePath != null && selServiceProvision.FilePath != "")
                        {
                            hlAttachment2.Visible = true;
                            hlAttachment2.Text = "Click here to view attachment";
                            hlAttachment2.NavigateUrl = selServiceProvision.FilePath;
                        }
                        else
                        {
                            hlAttachment2.Visible = false;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void btnSubscribedActivate_Click(object sender, EventArgs e)
        {
            divSubscribedSuccess.Visible = false;
            SubscribedActivateDeactivateCategories(DBKeys.RecordStatus_Active);
            ShowSubscribedPaginatedAndDeletedRecords();
        }

        protected void btnSubscribedDeactivate_Click(object sender, EventArgs e)
        {
            divSubscribedSuccess.Visible = false;
            mpopupCanCelServices.Show();
            hdnFlagForCancel.Value = "1";
        }

        protected void btnSubscribedDelete_Click(object sender, EventArgs e)
        {
            divSubscribedSuccess.Visible = false;
            SubscribedActivateDeactivateCategories(DBKeys.RecordStatus_Delete);
            ShowSubscribedPaginatedAndDeletedRecords();
        }

        protected void SubscribedShowDeleted_CheckedChanged(object sender, EventArgs e)
        {
            divSubscribedSuccess.Visible = false;
            ShowSubscribedPaginatedAndDeletedRecords();
        }


        protected void ddlCouponCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string ServiceID = Request.QueryString[QueryStringKeys.CheckRepeater];
                Guid ID = new Guid(ServiceID);
                string CouponStatus = ddlCouponCode.SelectedValue;
                if (CouponStatus == "0")
                {
                    var SubscribedService = objDBContext.UserSubscribedServices.OrderBy(obj => obj.ServiceID).Where(ser => ser.UserID == ID && ser.RecordStatus == DBKeys.RecordStatus_Active);
                    if (SubscribedService.Count() > 0)
                    {
                        rptrSubscribedServices.DataSource = SubscribedService;
                        rptrSubscribedServices.DataBind();
                    }
                    else
                    {
                        mvSubscribed.ActiveViewIndex = 2;
                        pnlBackToSubscribedService.Visible = true;
                        lblSubscribedWarning.Text = "There are no services.";
                    }
                }
                else if (CouponStatus == "1")
                {
                    var SubscribedService = objDBContext.UserSubscribedServices.OrderBy(obj => obj.ServiceID).Where(ser => ser.UserID == ID && ser.CouponCode != null && ser.RecordStatus == DBKeys.RecordStatus_Active);
                    if (SubscribedService.Count() > 0)
                    {
                        rptrSubscribedServices.DataSource = SubscribedService;
                        rptrSubscribedServices.DataBind();
                    }
                    else
                    {
                        mvSubscribed.ActiveViewIndex = 2;
                        pnlBackToSubscribedService.Visible = true;
                        lblSubscribedWarning.Text = "There are no services with coupon code.";
                    }
                }
                else if (CouponStatus == "2")
                {
                    var SubscribedService = objDBContext.UserSubscribedServices.OrderBy(obj => obj.ServiceID).Where(ser => ser.UserID == ID && ser.CouponCode == null && ser.RecordStatus == DBKeys.RecordStatus_Active);
                    if (SubscribedService.Count() > 0)
                    {
                        rptrSubscribedServices.DataSource = SubscribedService;
                        rptrSubscribedServices.DataBind();
                    }
                    else
                    {
                        mvSubscribed.ActiveViewIndex = 2;
                        pnlBackToSubscribedService.Visible = true;
                        lblSubscribedWarning.Text = "There are no services without coupon code.";
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }



        protected void lkbAddServiceToUser_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton rItem = ((LinkButton)(sender));
                HiddenField hdnServiceID = (rItem.FindControl("hdnNewServicesID") as HiddenField);
                DropDownList ddlDiscount = rItem.FindControl("ddlPriceModel") as DropDownList;
                bool PriceModel = false;
                if (ddlDiscount != null)
                    PriceModel = ddlDiscount.SelectedValue == "0" ? false : true;

                int ServiceID = int.Parse(hdnServiceID.Value);
                string UserMembershipID = Request.QueryString["userid"];
                Guid ID = new Guid(UserMembershipID);
                var UserDetails = objDBContext.UserProfiles.FirstOrDefault(u => u.UserMembershipID == ID);
                if (objDBContext.UserCarts.Any(ot => ot.ServiceID == ServiceID && ot.UserProfileID == UserDetails.UserProfileID && ot.RecordStatus == 999 && ot.CreatedBy == ID && ot.IsPayAsYouGo == PriceModel))
                {
                    var UserService = objDBContext.UserCarts.FirstOrDefault(ot => ot.ServiceID == ServiceID && ot.UserProfileID == UserDetails.UserProfileID && ot.RecordStatus == 999 && ot.CreatedBy == ID && ot.IsPayAsYouGo == PriceModel);
                    int QunatityCount = UserService.Quantity;
                    UserService.Quantity = QunatityCount + 1;
                    UserService.ModifiedOn = DateTime.Now;
                    UserService.ModifiedBy = ID;
                    divAvaSuccessMessage.Visible = true;
                    lblAvaSuccessmsg.Text = "Service added to user cart";
                }
                else
                {
                    UserCart objusercart = new UserCart();
                    LoadAddNewService(objusercart, PriceModel, ServiceID);
                    objDBContext.UserCarts.AddObject(objusercart);
                    divAvaSuccessMessage.Visible = true;
                    lblAvaSuccessmsg.Text = "Service added to user cart";
                }
                objDBContext.SaveChanges();
                divAvaErrorMessage.Visible = false;
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void rptrWFTCloudPackagesCategoryName_ItemDataBound1(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    string UserMembershipID = Request.QueryString["userid"];
                    Guid ID = new Guid(UserMembershipID);
                    var user = objDBContext.UserProfiles.FirstOrDefault(a => a.UserMembershipID == ID);
                    int UserProfileID = Convert.ToInt32(user.UserProfileID);

                    var myHidden = (HiddenField)e.Item.FindControl("hdnServiceCategoryID");
                    int CatID = Convert.ToInt32(myHidden.Value);
                    Repeater rptrWFTCloudPackages = (Repeater)e.Item.FindControl("rptrWFTCloudPackages");
                    Repeater rptrWFTServicesWithOutPayAsYouGo = (Repeater)e.Item.FindControl("rptrWFTCloudNonPayAsYouGo");
                    List<ServiceCatalog> ServiceCatalogs = new List<ServiceCatalog>();

                    var CommonServices = objDBContext.ServiceCatalogs.Where(cs => cs.UserSpecific == false && cs.ServiceCategoryID == CatID && cs.RecordStatus == DBKeys.RecordStatus_Active).OrderBy(svc => svc.Priority);
                    var LoggedUserSpecificService = objDBContext.ServiceCatalogs.Where(cs => cs.UserSpecific == true && cs.ServiceCategoryID == CatID && cs.RecordStatus == DBKeys.RecordStatus_Active && cs.UserProfileID == UserProfileID).OrderBy(svc => svc.Priority);
                    ServiceCatalogs.AddRange(LoggedUserSpecificService.ToArray());
                    ServiceCatalogs.AddRange(CommonServices.ToArray());

                    bool PayAsYouGoModel = Convert.ToBoolean(objDBContext.ServiceCategories.FirstOrDefault(ct => ct.ServiceCategoryID == CatID).IsPayAsYouGo);
                    if (PayAsYouGoModel == true)
                    {
                        rptrWFTServicesWithOutPayAsYouGo.Visible = false;
                        rptrWFTCloudPackages.Visible = true;
                        rptrWFTCloudPackages.DataSource = ServiceCatalogs;
                        rptrWFTCloudPackages.DataBind();
                    }
                    else
                    {
                        rptrWFTServicesWithOutPayAsYouGo.Visible = true;
                        rptrWFTCloudPackages.Visible = false;
                        rptrWFTServicesWithOutPayAsYouGo.DataSource = ServiceCatalogs;
                        rptrWFTServicesWithOutPayAsYouGo.DataBind();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Resuable Routines

        public string GetServiceName(string CID)
        {
            int ServiceID = Convert.ToInt32(CID);
            var services = objDBContext.ServiceCatalogs.FirstOrDefault(s => s.ServiceID == ServiceID);
            return services.ServiceName;
        }

        private void UpdateSubscribedServiceStatus(string QueryStringKey, int RecordStatus)
        {
            try
            {
                //If redirected for delete then do delete
                if (Request.QueryString[QueryStringKey].IsValid())
                {
                    mpopupCanCelServices.Show();
                    Session["CancelService"] = Convert.ToString(Request.QueryString[QueryStringKey]);
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        protected void btnCancelServices_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnFlagForCancel.Value == "0")
                {
                    int SubscriptionID = Convert.ToInt32(Session["CancelService"]);
                    string UserMembershipID = Request.QueryString["userid"];
                    Guid ID = new Guid(UserMembershipID);

                    var UserServ = objDBContext.UserSubscribedServices.FirstOrDefault(ot => ot.UserSubscriptionID == SubscriptionID && ot.UserID == ID && ot.RecordStatus == DBKeys.RecordStatus_Active);
                    if (UserServ != null)
                    {
                        var UserProfile = objDBContext.UserProfiles.FirstOrDefault(u => u.UserMembershipID == ID);
                        int serv = UserServ.ServiceID;
                        int Ctgry = UserServ.ServiceCategoryID;
                        if (UserServ.ARBSubscriptionId != "0" && UserServ.ARBSubscriptionId.IsValid())
                        {
                            long ARBSubID = Convert.ToInt64(UserServ.ARBSubscriptionId);
                            string Response = ReusableRoutines.ARBCancelSubscription(ARBSubID);
                            if (Response == "Failed")
                                return;
                        }
                        var Unsubscribe = objDBContext.pr_CancelUserSubscription(UserServ.UserSubscriptionID, UserProfile.EmailID, txtReasons.Text, txtFeedbacks.Text, rblServiceRating.SelectedValue);


                        var service = objDBContext.ServiceCatalogs.FirstOrDefault(s => s.ServiceID == serv);
                        var category = objDBContext.ServiceCategories.FirstOrDefault(c => c.ServiceCategoryID == Ctgry);
                        var ServiceProvisioningDEtails = objDBContext.ServiceProvisions.FirstOrDefault(sp => sp.ServiceCategoryID == Ctgry && sp.ServiceID == serv);
                        string message = ((UserProfile.LastName + "   " + UserProfile.FirstName));
                        string UserID = ((UserProfile.UserProfileID + "<br />"));
                        string Username = ((UserProfile.EmailID + "<br />"));
                        string servicetype = ((service.UsageUnit + "<br />"));
                        string servicename = ((service.ServiceName + "<br />"));
                        string reasons = ((txtReasons.Text != null ? (txtReasons.Text != "" ? txtReasons.Text : " - ") : " - " + "<br />"));
                        string feedback = ((txtFeedbacks.Text != null ? (txtFeedbacks.Text != "" ? txtFeedbacks.Text : " - ") : " - " + "<br />"));

                        string servicecategoryname = ((category.CategoryName + "<br />"));
                        string serviceusername = ((UserServ.ServiceUserName != null ? (UserServ.ServiceUserName != "" ? UserServ.ServiceUserName : " - ") : " - " + "<br />"));
                        string serviceurl = (UserServ.ServiceUrl != null ? (UserServ.ServiceUrl != "" ? ("<a target='_blank' href='" + UserServ.ServiceUrl + "'>" + UserServ.ServiceUrl + "</a>") : " - ") : " - ");
                        string serviceotherinformation = ((UserServ.ServiceOtherInformation != null ? (UserServ.ServiceOtherInformation != "" ? UserServ.ServiceOtherInformation : " - ") : " - " + "<br />"));
                        string UIDOnServer = UserServ.UIDOnServer != null ? (UserServ.UIDOnServer != "" ? UserServ.UIDOnServer : " - ") : " - ";
                        string ExpirationDate = Convert.ToDateTime(UserServ.ExpirationDate).ToString("dd-MMM-yyyy");
                        string ActivatedDate = Convert.ToDateTime(UserServ.ActiveDate).ToString("dd-MMM-yyyy");
                        string InstanceNumber = string.Empty;
                        string ApplicationServer = string.Empty;
                        string SID = string.Empty;

                        var ServiceProvisioningCheck = objDBContext.ServiceProvisions.Where(sp => sp.ServiceID == serv);
                        if (ServiceProvisioningCheck.Count() > 0)
                        {
                            InstanceNumber = ((ServiceProvisioningDEtails.InstanceNumber != null ? (ServiceProvisioningDEtails.InstanceNumber != "" ? ServiceProvisioningDEtails.InstanceNumber : " N/A ") : " N/A "));
                            ApplicationServer = ((ServiceProvisioningDEtails.ApplicationServer != null ? (ServiceProvisioningDEtails.ApplicationServer != "" ? ServiceProvisioningDEtails.ApplicationServer : " N/A ") : " N/A "));
                            SID = ((ServiceProvisioningDEtails.UIDOnServer != null ? (ServiceProvisioningDEtails.UIDOnServer != "" ? ServiceProvisioningDEtails.UIDOnServer : " N/A ") : " N/A "));
                        }
                        else
                        {
                            InstanceNumber = ((UserServ.InstanceNumber != null ? (UserServ.InstanceNumber != "" ? UserServ.InstanceNumber : " N/A ") : " N/A "));
                            ApplicationServer = ((UserServ.ApplicationServer != null ? (UserServ.ApplicationServer != "" ? UserServ.ApplicationServer : " N/A ") : " N/A "));
                            SID = ((UserServ.UIDOnServer != null ? (UserServ.UIDOnServer != "" ? UserServ.UIDOnServer : " N/A ") : " N/A "));
                        }

                        string FullContent = ("<table border='1'><tr><td rowspan='6'><strong>Subscription Information</strong></td>"
                                    + "<td><strong>Subscription ID </strong></td><td>" + UserServ.UserSubscriptionID + "<br /></td></tr>"
                                    + "<tr><td><strong>User Name </strong></td><td>" + UserProfile.FirstName + UserProfile.MiddleName + UserProfile.LastName + "<br /></td></tr>"
                                    + "<tr><td><strong>User Email </strong></td><td>" + UserProfile.EmailID + "<br /></td></tr>"
                                    + "<tr><td><strong>Service Category </strong></td><td>" + servicecategoryname + "<br /></td></tr>"
                                    + "<tr><td><strong>Service Name </strong></td><td>" + servicename + "<br /></td></tr>"
                                    + "<tr><td><strong>Release Version </strong></td><td>" + service.ReleaseVersion + "<br /></td></tr>"
                                    + "<tr><td rowspan='7'><strong>SAP Credentials</strong></td><td><strong>Instance Number </strong></td><td>" + InstanceNumber + "<br /></td></tr>"
                                    + "<tr><td><strong>Application Server </strong></td><td>" + ApplicationServer + "<br />"
                                    + "</td></tr>"
                                    + "<tr><td><strong>SID </strong></td><td>" + SID + "<br /></td></tr>"
                                    + "<tr><td><strong>UserName </strong></td><td>" + UserServ.ServiceUserName + "<br /></td></tr>"
                                    + "<tr><td><strong>Activated Date </strong></td><td>" + ActivatedDate + "<br /></td></tr>"
                                    + "<tr><td><strong>Service URL </strong></td>"
                                    + "<td>" + serviceurl + "</td></tr></table><br />");

                        FullContent += ("<table><tr><td><strong>Reasons:</strong></td></tr>"
                                        + "<tr><td>" + reasons + "<br /></td></tr></table>");

                        FullContent += ("<table><tr><td><strong>Feedback:</strong></td></tr>"
                                        + "<tr><td>" + feedback + "<br /></td></tr></table>");

                        FullContent += ("<table><tr><td><strong>Service Rating:</strong></td></tr>"
                                        + "<tr><td>" + rblServiceRating.SelectedValue
                                        + "<br /></td></tr></table>");


                        string AdminEmailContent = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["UnSubScribeToAdminCustomer"]));
                        AdminEmailContent = AdminEmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);

                        string AdminContent = AdminEmailContent.Replace("++AddContentHere++", FullContent).Replace("++name++", message);

                        string UserEmailContent = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["UnSubScribeToUser"]));
                        UserEmailContent = UserEmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                        string UserContent = UserEmailContent.Replace("++AddContentHere++", FullContent).Replace("++name++", message);

                        //AdminContent = EmailContent.
                        SMTPManager.SendAdminNotificationEmail(AdminContent, "Un-Subscribe request from " + UserProfile.FirstName + UserProfile.MiddleName + UserProfile.LastName, false);
                        SMTPManager.SendEmail(UserContent, "Un-Subscribe request from " + " " + UserProfile.FirstName + " " + UserProfile.MiddleName + " " + UserProfile.LastName, UserProfile.EmailID, false, true);
                        SMTPManager.SendSupportNotificationEmail(AdminContent, "Un-Subscribe request from " + UserProfile.FirstName + UserProfile.MiddleName + UserProfile.LastName, false);
                        SMTPManager.SendSAPBasisNotificationEmail(AdminContent, "Un-Subscribe request from " + UserProfile.FirstName + UserProfile.MiddleName + UserProfile.LastName, false);

                        divSubscribedSuccess.Visible = true;
                        Session["CancelService"] = null;
                    }
                    ShowSubscribedPaginatedAndDeletedRecords();
                }
                else if (hdnFlagForCancel.Value == "1")
                {
                    CancelserviceforLoop();
                    ShowSubscribedPaginatedAndDeletedRecords();
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void LoadAddNewService(UserCart objcart, bool DropDown, int ServiceID)
        {
            try
            {
                string UserMembershipID = Request.QueryString["userid"];
                Guid ID = new Guid(UserMembershipID);
                var user = objDBContext.UserProfiles.FirstOrDefault(a => a.UserMembershipID == ID);
                //HiddenField hdNewServiceID = (rptrAvailableServices.FindControl("hdnNewServicesID") as HiddenField);
                //int.Parse(hdNewServiceID.Value);
                objcart.ServiceID = ServiceID;
                objcart.UserProfileID = Convert.ToInt32(user.UserProfileID);
                objcart.Quantity = 1;
                objcart.CreatedOn = DateTime.Now;
                objcart.CreatedBy = ID;
                objcart.RecordStatus = 999;
                objcart.SelectedDiscount = 0;
                objcart.IsPayAsYouGo = DropDown;
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void SubscribedActivateDeactivateCategories(int ServiceStatus)
        {
            try
            {
                foreach (RepeaterItem rItem in rptrSubscribedServices.Items)
                {
                    CheckBox chkSelect = (rItem.FindControl("chkSubscribedSelect") as CheckBox);
                    HiddenField hdnServiceID = (rItem.FindControl("hdnServiceID") as HiddenField);
                    if (chkSelect.Checked)
                    {
                        var selCategory = GetSubscribedServiceFromID(int.Parse(hdnServiceID.Value));
                        if (selCategory.IsNotNull())
                        {
                            selCategory.RecordStatus = ServiceStatus;
                            divSubscribedSuccess.Visible = true;
                        }
                    }
                }
                ShowSubscribedPaginatedAndDeletedRecords();
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void CancelserviceforLoop()
        {
            string UserMembershipID = Request.QueryString["userid"];
            Guid ID = new Guid(UserMembershipID);
            foreach (RepeaterItem rItem in rptrSubscribedServices.Items)
            {
                CheckBox chkSelect = (rItem.FindControl("chkSubscribedSelect") as CheckBox);
                HiddenField hdnServiceID = (rItem.FindControl("hdnServiceID") as HiddenField);
                if (chkSelect.Checked)
                {

                    int SubscriptionID = int.Parse(hdnServiceID.Value);
                    var UserServ = objDBContext.UserSubscribedServices.FirstOrDefault(ot => ot.UserSubscriptionID == SubscriptionID && ot.UserID == ID && ot.RecordStatus == DBKeys.RecordStatus_Active);
                    if (UserServ != null)
                    {
                        var UserProfile = objDBContext.UserProfiles.FirstOrDefault(u => u.UserMembershipID == ID);
                        int serv = UserServ.ServiceID;

                        var Unsubscribe = objDBContext.pr_CancelUserSubscription(UserServ.UserSubscriptionID, UserProfile.EmailID, txtReasons.Text, txtFeedbacks.Text, rblServiceRating.SelectedValue);
                        if (UserServ.ARBSubscriptionId != "0" && UserServ.ARBSubscriptionId.IsValid())
                        {
                            long ARBSubID = Convert.ToInt64(UserServ.ARBSubscriptionId);
                            ReusableRoutines.ARBCancelSubscription(ARBSubID);
                        }

                        var service = objDBContext.ServiceCatalogs.FirstOrDefault(s => s.ServiceID == serv);
                        string message = ((UserProfile.LastName + "   " + UserProfile.FirstName));
                        string UserID = ((UserProfile.UserProfileID + "<br />"));
                        string Username = ((UserProfile.EmailID + "<br />"));
                        string servicetype = ((service.UsageUnit + "<br />"));
                        string servicename = ((service.ServiceName + "<br />"));
                        string reasons = ((txtReasons.Text + "<br />"));
                        string feedback = ((txtFeedbacks.Text + "<br />"));
                        string AdminEmailContent = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["UnSubScribeToAdminCustomer"]));
                        AdminEmailContent = AdminEmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                        string AdminContent = AdminEmailContent.Replace("++name++", message).Replace("++Username++", Username).Replace("++UserID++", UserID).Replace("++servicename++", servicename).Replace("++servicetype++", servicetype).Replace("++reasons++", reasons).Replace("++feedback++", feedback).Replace("++ServiceID++", serv.ToString());

                        string UserEmailContent = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["UnSubScribeToUser"]));
                        UserEmailContent = UserEmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                        string UserContent = UserEmailContent.Replace("++name++", message).Replace("++Username++", Username).Replace("++UserID++", UserID).Replace("++servicename++", servicename).Replace("++servicetype++", servicetype).Replace("++reasons++", reasons).Replace("++feedback++", feedback).Replace("++ServiceID++", serv.ToString());

                        //AdminContent = EmailContent.
                        SMTPManager.SendAdminNotificationEmail(AdminContent, "Un-Subscribe request from " + UserProfile.FirstName + UserProfile.MiddleName + UserProfile.LastName, false);
                        SMTPManager.SendEmail(UserContent, "Un-Subscribe request from " + " " + UserProfile.FirstName + " " + UserProfile.MiddleName + " " + UserProfile.LastName, UserProfile.EmailID, false, true);
                        divSubscribedSuccess.Visible = true;
                        hdnFlagForCancel.Value = "0";
                    }
                }
            }
            objDBContext.SaveChanges();
        }

        private void LoadSubscribedDataFromFields(UserSubscribedService objSubscribedService)
        {
            try
            {
                string ServiceID = Request.QueryString[QueryStringKeys.CheckRepeater];
                Guid ID = new Guid(ServiceID);

                //objSubscribedService.CouponCode = Convert.ToInt32(txtCouponCode.Text);
                //objSubscribedService.Credit = Convert.ToDecimal(txtCredit.Text);
                //objSubscribedService.Usage = Convert.ToDecimal(txtUsage.Text);

                objSubscribedService.ServiceIPAddress = txtIPAddress.Text;
                //objSubscribedService.ARBPeriod = Convert.ToInt32(txtARBPeriod.Text);
                objSubscribedService.ServiceDetailsUpdatedDate = DateTime.Now;
                objSubscribedService.ServiceDetailsUpdatedBy = ID;
                objSubscribedService.ServiceOtherInformation = txtServiceInformation.Text;
                int Status = Convert.ToInt32(ddlRecordStatus.SelectedValue);
                if (Status == 1)
                {
                    objSubscribedService.RecordStatus = 1;
                }
                else if (Status == 0)
                {
                    objSubscribedService.RecordStatus = 0;
                }
                else if (Status == -1)
                {
                    objSubscribedService.RecordStatus = -1;
                }
                if (objSubscribedService.AutoProvisioningDone != true && objSubscribedService.RecordStatus == 1)
                {
                    objSubscribedService.ServiceUrl = txtServiceURL.Text;
                    objSubscribedService.ServiceUserName = txtUserName.Text;
                    objSubscribedService.ServicePassword = txtServicePassword.Text;
                    objSubscribedService.ServiceOtherInformation = txtServiceInformation.Text;
                    objSubscribedService.AutoProvisioningDone = true;
                    objDBContext.SaveChanges();
                    string ServiceDetails1 = "";
                    var serviceDetails = objDBContext.ServiceCatalogs.FirstOrDefault(scat => scat.ServiceID == objSubscribedService.ServiceID);
                    var serviceProvisioning = objDBContext.ServiceProvisions.FirstOrDefault(a => a.ServiceID == serviceDetails.ServiceID);

                    //var ServiceProvisioningDEtails = objDBContext.ServiceProvisions.FirstOrDefault(sp => sp.ServiceCategoryID == objSubscribedService.ServiceCategoryID && sp.ServiceID == objSubscribedService.ServiceID);
                    var userProf = objDBContext.UserProfiles.FirstOrDefault(d => d.UserProfileID == objSubscribedService.UserProfileID);
                    string UserFullName = userProf.FirstName + " " + userProf.MiddleName + " " + userProf.LastName;
                    var APP_URL = objDBContext.WftSettings.FirstOrDefault(a => a.SettingKey == "APP_URL");
                    ServiceDetails1 = ("<strong>Service Name :</strong> " + serviceDetails.ServiceName + "<br />"
                                      + "<strong>Service/SAP UserName :</strong>" + objSubscribedService.ServiceUserName + "<br />"
                                      + "<strong>Service/SAP Password :</strong>" + objSubscribedService.ServicePassword + "<br />"
                                      + (APP_URL != null ? ("<strong>Service/SAP URL :</strong><a href=" + APP_URL.SettingValue + " target='_blank'>" + "Click Here" + "</a>") : "") + "<br />"
                                      + "<strong>Service Other Information :</strong>" + objSubscribedService.ServiceOtherInformation + "<br /><br />");
                    //*******
                    //Sent Email Notification to the Customer 
                    //******
                    string CustomerEmailContent = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["ServiceReadyToCustomer"]));//"/wftcloud/UploadedContents/EmailTemplates/Service-Ready.html"));
                    CustomerEmailContent = CustomerEmailContent.Replace("++UserName++", UserFullName);
                    CustomerEmailContent = CustomerEmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                    string CustomerContent = CustomerEmailContent.Replace("++AddContentHere++", ServiceDetails1).Replace("++name++", UserFullName);
                    CustomerContent = CustomerContent.Replace("++ServiceDescription++", serviceDetails.ServiceDescription);
                    string path1 = "";
                    string img = "";
                    string AttachmentPath = "";
                    if (serviceProvisioning != null)
                    {
                        if (serviceProvisioning.AttachmentImgPath.IsValid())
                        {
                             path1 = ConfigurationManager.AppSettings["DomainNameForImageBindInMail"] + serviceProvisioning.AttachmentImgPath;
                             img = " <img alt='Logo' src='" + path1 + "' border='0' vspace='0' hspace='0' style='display:block; max-width:100%; height:auto !important;' /><br /><br />";
                            CustomerContent = CustomerContent.Replace("++AttachmentURL++", img);
                        }
                        else
                        {
                            CustomerContent = CustomerContent.Replace("++AttachmentURL++", "");
                        }
                        if (serviceProvisioning.AttachmentPath.IsValid())
                        {
                            AttachmentPath = serviceProvisioning.AttachmentPath;
                            //SMTPManager.SendEmail(CustomerContent, "Service Provisioning Details on WFTCloud.com", userProf.EmailID, false, true);
                            SendEmailAttachment(CustomerContent, "Service Provisioning Details on WFTCloud.com", userProf.EmailID, false, true, AttachmentPath);
                        }
                        else
                        {
                            SMTPManager.SendEmail(CustomerContent, "Service Provisioning Details on WFTCloud.com", userProf.EmailID, false, true);
                        }
                    }
                    else
                    {
                        CustomerContent = CustomerContent.Replace("++AttachmentURL++", "");
                        SMTPManager.SendEmail(CustomerContent, "Service Provisioning Details on WFTCloud.com", userProf.EmailID, false, true);
                    }
                    //*******
                    //Sent Email Notification to the admin about ServiceProvision details provided to customer 
                    //******
                    
                        string AdminEmailContent = File.ReadAllText(Server.MapPath(ConfigurationManager.AppSettings["ServiceProvisionSuccessNotificationToAdmin"]));//"/wftcloud/UploadedContents/EmailTemplates/ServiceProvisionSuccessNotificationToAdmin.html"));
                        AdminEmailContent = AdminEmailContent.Replace("++UserName++", UserFullName + " (" + userProf.EmailID + ") ");
                        AdminEmailContent = AdminEmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                        string AdminContent = AdminEmailContent.Replace("++AddContentHere++", ServiceDetails1);
                        AdminContent = AdminContent.Replace("++ServiceDescription++", serviceDetails.ServiceDescription);
                        AdminContent = AdminContent.Replace("++AttachmentURL++", img);

                    if(AttachmentPath=="")
                        SMTPManager.SendAdminNotificationEmail(AdminContent, "Manual Provisioning Completed for - " + userProf.EmailID, false);
                    else
                        SendAdminNotificationAttachmentEmail(AdminContent, "Manual Provisioning Completed for - " + userProf.EmailID, false, AttachmentPath);
                }
                objDBContext.SaveChanges();
                lblSubscribedSuccessMessage.Visible = true;
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private UserSubscribedService GetSubscribedServiceFromID(int UserSubscriptionID)
        {
            return objDBContext.UserSubscribedServices.FirstOrDefault(sub => sub.UserSubscriptionID == UserSubscriptionID);
        }

        private void ShowPaginatedAndDeletedRecords()
        {
            try
            {
                string UserMembershipID = Request.QueryString["userid"];
                Guid ID = new Guid(UserMembershipID);
                var UserDetails = objDBContext.UserProfiles.FirstOrDefault(u => u.UserMembershipID == ID);
                //List<ServiceCatalog> lstServices = new List<ServiceCatalog>();

                //var ServiceCat = objDBContext.ServiceCategories.Where(a => a.RecordStatus == DBKeys.RecordStatus_Active);
                //foreach (ServiceCategory catitem in ServiceCat)
                //{
                //    int CategoryID = Convert.ToInt32(catitem.ServiceCategoryID);
                //    var UserSpecific = objDBContext.ServiceCatalogs.Where(svc => svc.RecordStatus == DBKeys.RecordStatus_Active && svc.ServiceCategoryID == CategoryID && svc.UserProfileID == UserDetails.UserProfileID && svc.UserSpecific == true).OrderBy(svc => svc.ServiceID);

                //    foreach (ServiceCatalog item in UserSpecific)
                //    {
                //        lstServices.AddRange(UserSpecific);
                //    }
                //    var RegularServices = objDBContext.ServiceCatalogs.OrderBy(obj => obj.ServiceID).Where(cat => cat.RecordStatus != DBKeys.RecordStatus_Delete && cat.ServiceCategoryID == CategoryID && cat.RecordStatus == DBKeys.RecordStatus_Active && cat.UserSpecific != true);
                //    lstServices.AddRange(RegularServices);
                //}
                //rptrAvailableServices.DataSource = lstServices;
                //rptrAvailableServices.DataBind();
                List<ServiceCategory> sercat = new List<ServiceCategory>();
                var serviceCateg = objDBContext.ServiceCategories.Where(cat => cat.RecordStatus == DBKeys.RecordStatus_Active).OrderBy(svc => svc.CategoryPriority);
                foreach (var result in serviceCateg)
                {
                    if (objDBContext.ServiceCatalogs.OrderBy(o => o.Priority).Where(cat => cat.ServiceCategoryID == result.ServiceCategoryID && cat.UserSpecific == true && cat.UserProfileID == UserDetails.UserProfileID && cat.RecordStatus == DBKeys.RecordStatus_Active).Count() > 0)
                    {
                        sercat.Add(result);
                    }
                    else if (objDBContext.ServiceCatalogs.OrderBy(o => o.Priority).Where(cat => cat.ServiceCategoryID == result.ServiceCategoryID && cat.RecordStatus == DBKeys.RecordStatus_Active).Count() > 0)
                    {
                        sercat.Add(result);
                    }
                    
                }
                rptrWFTCloudPackagesCategoryName.DataSource = sercat;
                rptrWFTCloudPackagesCategoryName.DataBind();
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void ShowSubscribedPaginatedAndDeletedRecords()
        {
            try
            {
                string ServiceID = Request.QueryString[QueryStringKeys.CheckRepeater];
                Guid ID = new Guid(ServiceID);
                var SubscribedCount = objDBContext.UserSubscribedServices.Where(ser => ser.UserID == ID);
                if (SubscribedCount.Count() > 0)
                {
                    if (SubscribedShowDeleted.Checked)
                    {
                        if (ServiceID != null)
                        {
                            var SubscribedService = objDBContext.UserSubscribedServices.OrderBy(obj => obj.ServiceID).Where(ser => ser.UserID == ID);
                            rptrSubscribedServices.DataSource = SubscribedService;
                            rptrSubscribedServices.DataBind();
                        }
                    }
                    else
                    {
                        if (ServiceID != null)
                        {
                            var SubscribedService = objDBContext.UserSubscribedServices.OrderBy(obj => obj.ServiceID).Where(cat => cat.RecordStatus == DBKeys.RecordStatus_Active && cat.UserID == ID);
                            rptrSubscribedServices.DataSource = SubscribedService;
                            rptrSubscribedServices.DataBind();
                        }
                    }
                }
                else
                {
                    mvSubscribed.ActiveViewIndex = 2;
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        public string SubProvisionStatus(string Status)
        {
            bool a = Convert.ToBoolean(Status);
            if (a == true)
                return "Active";
            else
                return "Provision Pending";
        }
        public string ShowSubscribedServiceStatus(string Usid)
        {
            int UsubId = Convert.ToInt32(Usid);
            var usdetails = objDBContext.UserSubscribedServices.FirstOrDefault(u => u.UserSubscriptionID == UsubId);
            if (usdetails.RecordStatus == 0)
                return "Expired";
            else if (usdetails.RecordStatus == -1)
                return "Cancelled";
            else
            {
                if (usdetails.ExpirationDate == null)
                {
                    return "Expired";
                }
                DateTime ExpDate = Convert.ToDateTime(usdetails.ExpirationDate);
                if (ExpDate < DateTime.Now)
                {
                    return "Expired";
                }
                return "Active";
            }

        }

        public void SendEmailAttachment(string messageBody, string subject, string ToMail, bool sendInBCC, bool IsHtml, string attachmentPath)
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
                mail.Subject = subject;
                mail.Body = messageBody;
                mail.IsBodyHtml = IsHtml;
                try
                {
                    // Add attachment
                    attachmentPath = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + attachmentPath);
                    if (File.Exists(attachmentPath))
                    { mail.Attachments.Add(new Attachment(attachmentPath)); }
                }
                catch (Exception Ex)
                {
                    ReusableRoutines.LogException(this.GetType().ToString(), "SendEmailAttachment", Ex.Message, Ex.StackTrace, DateTime.Now);
                }

                SmtpServer.Port = int.Parse(ConfigurationManager.AppSettings["SMTPPort"]);
                SmtpServer.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SMTPUsername"], ConfigurationManager.AppSettings["SMTPPassword"]);
                SmtpServer.EnableSsl = bool.Parse(ConfigurationManager.AppSettings["EnableSSL"]);

                SmtpServer.Send(mail);
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException("ReusableRoutines.cs", "SendEmailAttachment", Ex.Message, Ex.StackTrace, DateTime.Now);
            }

        }
        #endregion

        #region Manage CRM Code Commented
        /*
        protected void ManageShowDeleted_CheckedChanged(object sender, EventArgs e)
        {
            ShowManageCrmPaginatedAndDeletedRecords();
        }
        protected void ddlServiceCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int selectedCategory = Convert.ToInt32(ddlServiceCategory.SelectedValue);
                ddlReportAgainst.DataSource = objDBContext.ServiceCatalogs.OrderBy(s => s.ServiceID).Where(st => st.ServiceCategoryID == selectedCategory && st.RecordStatus != DBKeys.RecordStatus_Delete);
                ddlReportAgainst.DataTextField = "ServiceName";
                ddlReportAgainst.DataValueField = "ServiceID";
                ddlReportAgainst.DataBind();
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
 *         protected void btnCrmIssueSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.EditCrmIssues].IsValid())
                {
                    int IssueID = int.Parse(Request.QueryString[QueryStringKeys.EditCrmIssues]);
                    int AssisgnAdmin = Convert.ToInt32(ddlAssignAdmin.SelectedValue);
                    string Admin = Convert.ToString(objDBContext.UserProfiles.FirstOrDefault(ad => ad.UserProfileID == AssisgnAdmin).UserMembershipID);
                    Guid AdminID = new Guid(Admin);
                    var CrmIssue = objDBContext.CRMRequests.FirstOrDefault(usr => usr.CRMRequestID == IssueID);
                    string issueDesc = ddlIssueType.SelectedItem.Text;
                    int IssueType = objDBContext.CRMIssueTypes.FirstOrDefault(i => i.CRMIssueTypeDesc == issueDesc).CRMIssueTypeID;
                    string ReportAgainst = ddlReportAgainst.SelectedItem.Text;
                    int ReportAgainstID = objDBContext.ServiceCatalogs.FirstOrDefault(r => r.ServiceName == ReportAgainst).ServiceID;
                    string ServiceCategory = ddlServiceCategory.SelectedItem.Text;
                    int CategoryID = objDBContext.ServiceCategories.FirstOrDefault(c => c.CategoryName == ServiceCategory).ServiceCategoryID;
                    // CrmIssue.IssueDescription = txtIssue.Text;
                    CrmIssue.AssignedToAdmin = AdminID;
                    //CrmIssue.IssueType = IssueType;
                    //CrmIssue.ServiceID = ReportAgainstID;
                    //CrmIssue.ServiceCategoryID = CategoryID;
                    CrmIssue.CRMRequestStatus = Convert.ToInt32(ddlCrmStatus.SelectedValue);
                    CrmIssue.AdminNotes = txtAdminNotes.Text;
                    objDBContext.SaveChanges();
                    lblManageSuccessMessage.Visible = true;
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }

        private void LoadManageCrmIssue()
        {
            try
            {
                if (Request.QueryString[QueryStringKeys.EditCrmIssues].IsValid())
                {
                    int RequestID = int.Parse(Request.QueryString[QueryStringKeys.EditCrmIssues]);
                    var selRequest = objDBContext.CRMRequests.FirstOrDefault(cat => cat.CRMRequestID == RequestID);
                    if (selRequest != null)
                    {
                        mvCrmIssues.ActiveViewIndex = 1;
                        //lblCurrentCaseID.Text = Convert.ToString(selRequest.CRMRequestID);
                        txtIssue.Text = selRequest.IssueDescription;
                        txtAdminNotes.Text = selRequest.AdminNotes;

                        var cate = objDBContext.ServiceCategories.OrderBy(sc => sc.CategoryName).Where(scs => scs.RecordStatus != DBKeys.RecordStatus_Delete).ToList();
                        ServiceCategory Category = new ServiceCategory();
                        Category.ServiceCategoryID = 0;
                        Category.CategoryName = "<-- Choose Service -->";
                        cate.Add(Category);
                        ddlServiceCategory.DataSource = cate.OrderBy(sc => sc.CategoryName).Where(scs => scs.RecordStatus != DBKeys.RecordStatus_Delete && scs.RecordStatus != DBKeys.RecordStatus_Inactive);
                        ddlServiceCategory.DataTextField = "CategoryName";
                        ddlServiceCategory.DataValueField = "ServiceCategoryID";
                        ddlServiceCategory.DataBind();
                        int ServiceID = Convert.ToInt32(selRequest.ServiceCategoryID);
                        trServices.Visible = false;
                        if (selRequest.ServiceCategoryID != null)
                        {
                            trServices.Visible = true;
                            ddlServiceCategory.SelectedValue = Convert.ToString(ServiceID);
                            ddlReportAgainst.DataSource = objDBContext.ServiceCatalogs.OrderBy(s => s.ServiceID).Where(st => st.ServiceCategoryID == ServiceID && st.RecordStatus != DBKeys.RecordStatus_Delete && st.RecordStatus != DBKeys.RecordStatus_Inactive);
                            ddlReportAgainst.DataTextField = "ServiceName";
                            ddlReportAgainst.DataValueField = "ServiceID";
                            ddlReportAgainst.DataBind();
                            ddlReportAgainst.SelectedValue = Convert.ToString(selRequest.ServiceID);
                        }
                        //else
                        //{
                        //    int SelectedCategory = Convert.ToInt32(ddlServiceCategory.SelectedValue);
                        //    if (SelectedCategory != null)
                        //    {
                        //        ddlReportAgainst.DataSource = objDBContext.ServiceCatalogs.OrderBy(s => s.ServiceID).Where(st => st.ServiceCategoryID == SelectedCategory && st.RecordStatus != DBKeys.RecordStatus_Delete && st.RecordStatus != DBKeys.RecordStatus_Inactive);
                        //        ddlReportAgainst.DataTextField = "ServiceName";
                        //        ddlReportAgainst.DataValueField = "ServiceID";
                        //    }
                        //}
                        //int ServiceID = Convert.ToInt32(selRequest.ServiceID);
                        //string ServiceName = objDBContext.ServiceCatalogs.FirstOrDefault(sn => sn.ServiceID == ServiceID).ServiceName;
                        ddlAssignAdmin.DataSource = objDBContext.UserProfiles.OrderBy(a => a.FirstName).Where(ad => ad.UserRole == DBKeys.Role_Administrator && ad.RecordStatus != DBKeys.RecordStatus_Delete && ad.RecordStatus != DBKeys.RecordStatus_Inactive);
                        ddlAssignAdmin.DataTextField = "FirstName";
                        ddlAssignAdmin.DataValueField = "UserProfileID";
                        ddlAssignAdmin.DataBind();
                        string AssignAdmin = Convert.ToString(selRequest.AssignedToAdmin);
                        if (AssignAdmin != "" || AssignAdmin != string.Empty)
                        {
                            Guid Assign = new Guid(AssignAdmin);
                            int AdminName = Convert.ToInt32(objDBContext.UserProfiles.FirstOrDefault(a => a.UserMembershipID == Assign).UserProfileID);
                            ddlAssignAdmin.SelectedValue = Convert.ToString(AdminName);
                        }
                        ddlIssueType.DataSource = objDBContext.CRMIssueTypes.OrderBy(i => i.CRMIssueTypeID).Where(sta => sta.RecordStatus != DBKeys.RecordStatus_Delete && sta.RecordStatus != DBKeys.RecordStatus_Inactive);
                        ddlIssueType.DataTextField = "CRMIssueTypeDesc";
                        ddlIssueType.DataValueField = "CRMIssueTypeID";
                        ddlIssueType.DataBind();
                        int IssueType = selRequest.IssueType;
                        ddlIssueType.SelectedValue = Convert.ToString(IssueType);
                        ddlCrmStatus.DataSource = objDBContext.CRMRequestStatus.OrderBy(crm => crm.CRMRequestStatusID).Where(s => s.RecordStatus != DBKeys.RecordStatus_Delete && s.RecordStatus != DBKeys.RecordStatus_Inactive);
                        ddlCrmStatus.DataTextField = "CRMRequestStatusDesc";
                        ddlCrmStatus.DataValueField = "CRMRequestStatusID";
                        ddlCrmStatus.DataBind();
                        ddlCrmStatus.SelectedValue = Convert.ToString(selRequest.CRMRequestStatus);

                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
         *         private void ShowManageCrmPaginatedAndDeletedRecords()
        {
            try
            {
                string UserID = Request.QueryString[QueryStringKeys.CheckRepeater];
                Guid ID = new Guid(UserID);
                var CrmRequestCount = objDBContext.CRMRequests.OrderBy(obj => obj.CRMRequestID).Where(crm => crm.CustomerID == ID);
                if (CrmRequestCount.Count() > 0)
                {
                    if (UserID != null)
                    {
                        if (ManageShowDeleted.Checked)
                        {
                            var CrmRequest = objDBContext.vwManageCRMIssues.OrderBy(obj => obj.CRMRequestID).Where(crm => crm.CustomerID == ID);
                            rptrManageCRMIssue.DataSource = CrmRequest;
                            rptrManageCRMIssue.DataBind();
                        }
                        else
                        {
                            var CrmRequest = objDBContext.vwManageCRMIssues.OrderBy(obj => obj.CRMRequestID).Where(cat => cat.RecordStatus == DBKeys.RecordStatus_Active && cat.CustomerID == ID);
                            rptrManageCRMIssue.DataSource = CrmRequest;
                            rptrManageCRMIssue.DataBind();
                        }
                    }
                }
                else
                {
                    mvCrmIssues.ActiveViewIndex = 2;
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
        */
        #endregion
    }
}
/*
 * 							<div id="Manage" class="tab-pane" runat="server">
                                <asp:MultiView ID="mvCrmIssues" runat="server" ActiveViewIndex="0">
                                    <asp:View ID="vwManageCRM" runat="server">
                                        <div class="row-fluid">
                                        <div class="span12">
                                        <div class="row-fluid">
	                                        <div class="table-header">
		                                        Manage CRM Issues	        
                                            </div>
                                            <div class="dataTables_wrapper">
                                                <div role="grid" class="dataTables_wrapper">
                                                    <table id="tblManageCrm" class="table table-striped table-bordered table-hover dataTable">
                                                        <thead>
                                                        <tr role="row">
                                                            <th class="center" role="columnheader">
                                                                <label><input type="checkbox" class="ace"/>
                                                                    <span class="lbl"></span>
                                                                </label>
                                                            </th>
                                                            <th role="columnheader">Category</th>
                                                            <th role="columnheader">Service</th>
                                                            <th role="columnheader">Issue Type</th>
                                                            <th role="columnheader">Status</th>
                                                            <th role="columnheader" style="text-align:center;">Options</th>
                                                        </tr>
                                                        </thead>
                                                        <tbody role="alert">
                                                        <asp:Repeater ID="rptrManageCRMIssue" runat="server">
                                                        <ItemTemplate>
                                                        <tr>
                                                            <td class="center">
                                                                <asp:CheckBox ID="chkSelect" runat="server" class="ace" />
                                                            </td>
                                                            <td><%# Eval("CategoryName") == null? "-":Eval("CategoryName")%>
                                                                <asp:HiddenField ID="hdnCRMRequestID" runat="server" Value='<%# Eval("CRMRequestID")%>' />
                                                            </td>
                                                            <td><%# Eval("ServiceName")== null? "-":Eval("ServiceName")%></td>
                                                            <td><%# WFTCloud.GeneralReusableMethods.GetCrmIssueType(Eval("IssueType").ToString())%></td>
                                                            <td><%# WFTCloud.GeneralReusableMethods.GetCRMIssueStatusString(Eval("CRMRequestStatus").ToString()) %></td>
                                                            <td>
                                                                <div class="action-buttons">
                                                                <a data-rel="tooltip" title="Edit CRM Issue" href='UserServices.aspx?crmrequestid=<%# Eval("CRMRequestID")%>&userid=<%=UserMembershipID %>&showview=ManageCrmIssue' class="green">
                                                                    <i class="icon-pencil bigger-130"></i>
                                                                </a>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        </ItemTemplate>
                                                        </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                                </div>
                                        </div>
                                            <div class="row-fluid">
                                                <asp:CheckBox ID="ManageShowDeleted" runat="server"
                                        AutoPostBack="true" OnCheckedChanged="ManageShowDeleted_CheckedChanged"/>
                                        <span class="label label-warning arrowed-right">Show Deactivated</span>
                                            </div>
                                        </div>
                                        </div>
                                        </div>
                                    </asp:View>
                                    <asp:View ID="vmCRMForm" runat="server">
                                        <a href="UserServices.aspx?userid=<%=UserMembershipID %>&showview=ManageCrmIssue">&lt;&lt; Back&nbsp;</a>
                                        <div class="table-header">
		                            Edit CRM Issues
	                            </div>
                                        <table class="table table-striped table-bordered table-hover dataTable">
                                    <%--<tr>
                                        <td class="span5">Current Case ID
                                        </td>
                                        <td><asp:Label ID="lblCurrentCaseID" runat="server" Text="Label"></asp:Label>
                                        </td>
                                    </tr>--%>
                                    <tr id="trServices" runat="server">
                                        <td class="span2">Report Against
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlServiceCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlServiceCategory_SelectedIndexChanged" Enabled="False">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlReportAgainst" runat="server" Enabled="False">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvServiceCategory" runat="server" ErrorMessage="Select Service Category"
                                                 ControlToValidate="ddlServiceCategory" InitialValue="0" ValidationGroup="CRMIssue" Display="Dynamic" CssClass="error" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="span2">Issue
                                        </td>
                                        <td><asp:TextBox ID="txtIssue" runat="server" TextMode="MultiLine" Height="200px" Enabled="False"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvIssue" runat="server" 
                                            ErrorMessage="Category Name" ControlToValidate="txtIssue" ValidationGroup="CRMIssue"
                                            Text="*" ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td class="span2">Issue Type
                                        </td>
                                        <td>  
                                            <asp:DropDownList ID="ddlIssueType" runat="server" Enabled="False">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="span2">Assign To
                                        </td>
                                        <td>  
                                            <asp:DropDownList ID="ddlAssignAdmin" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="span2">Admin Notes
                                        </td>
                                        <td><asp:TextBox ID="txtAdminNotes" runat="server" TextMode="MultiLine" Height="200px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvAdminNotes" runat="server" 
                                            ErrorMessage="Category Name" ControlToValidate="txtAdminNotes" ValidationGroup="CRMIssue"
                                            Text="*" ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="span2">Status
                                        </td>
                                        <td> <asp:DropDownList ID="ddlCrmStatus" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="span2">
                                        </td>
                                        <td><asp:Button ID="btnCrmIssueSave" runat="server" Text="Save" class="btn btn-primary" OnClick="btnCrmIssueSave_Click" ValidationGroup="CRMIssue" />
                                            <div id="lblManageSuccessMessage" runat="server" visible="false" class="alert alert-block alert-success">
							                    <button data-dismiss="alert" class="close" type="button">
								                    <i class="icon-remove"></i>
							                    </button>
							                    <p>
                                                    <i class="icon-ok"></i>
								                    CRM Issue updated successfully.
							                    </p>
						                    </div>
                                            <div id="lblManageErrorMessage" runat="server" visible="false" class="alert alert-error">
						                        <button data-dismiss="alert" class="close" type="button">
							                        <i class="icon-remove"></i>
						                        </button>
                                                <i class="icon-remove"></i>
					                            Error while save CRM Details. Please try again.
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                    </asp:View>
                                    <asp:View ID="vwNoCrmIssue" runat="server">
                                        <div id="divNoCrmIssue" runat="server" class="alert alert-warning">
                                            <button data-dismiss="alert" class="close" type="button">
                                            <i class="icon-remove"></i>
                                            </button>
                                            <i class="icon-remove"></i>
                                            <strong>There are no CRM Issues for the selected user.</strong>
                                        </div>
                                    </asp:View>
                                </asp:MultiView>
							</div> 
*/
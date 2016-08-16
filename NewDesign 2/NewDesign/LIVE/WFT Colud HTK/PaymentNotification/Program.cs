using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Net;
using WFTCloud.DataAccess;

namespace SubscriptionCancellation
{
    class Program
    {
        static SqlConnection sqlConnection = new SqlConnection();
        static string LogFileData = string.Empty;
        static cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        static void Main(string[] args)
        {
            // Get list of all records that are ready for payment for today. These records shouldbe grouped by customer and his credit card
            //On Successful payment,store all the details from Authorize.Net into the data base
            //ON Successful Payment, send out a confirmation email to the user and administrator
            // On any failures, the details should be notified to Administrator as a daily report.
            // Failure messages should be sent to the User too.

            LogExecutionComment("---------------------------------------------------------", false);
            LogExecutionComment("Payment Processing Notification Started...", true);
            LogExecutionComment(string.Empty, true);

            try
            {

                DataTable dtAuthorizePaymentRecords = new DataTable();
                DataTable dtPayPalPaymentRecords = new DataTable();

                sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                sqlConnection.Open();

                SqlCommand Authorizecommand = new SqlCommand("pr_GetTodaysPaymentNotification", sqlConnection);
                Authorizecommand.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader AuthorizesqlData = Authorizecommand.ExecuteReader();
                dtAuthorizePaymentRecords.Load(AuthorizesqlData);
                AuthorizesqlData.Close();

                SqlCommand PayPalcommand = new SqlCommand("pr_GetTodaysPaymentNotification", sqlConnection);
                PayPalcommand.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader PayPalsqlData = PayPalcommand.ExecuteReader();
                dtPayPalPaymentRecords.Load(PayPalsqlData);
                PayPalsqlData.Close();

                LogExecutionComment("Processing Individual Payments Notification...", true);

                foreach (DataRow dtRow in dtAuthorizePaymentRecords.Rows)
                {
                    string CustomerProfileID = dtRow["CustomerProfileID"].ToString();

                    string PaymentProfileID = dtRow["CustomerPaymentProfileID"].ToString();

                    decimal BillingAmount = Convert.ToDecimal(dtRow["BillingAmount"].ToString());

                    string PaypalBillingAgreementID = dtRow["PaypalBillingAgreementID"].ToString();

                    string PaypalPaymentTransactionID = dtRow["PaypalPaymentTransactionID"].ToString();

                    int UserSubscriptionID = Convert.ToInt32(dtRow["UserSubscriptionID"].ToString());

                    string PaymentMethod = dtRow["PaymentMethod"].ToString();
                    DateTime PaymentDate = Convert.ToDateTime(dtRow["PaymentDate"]);

                    if (dtRow["PaymentMethod"].ToString() == "Authorize.net" || PaymentMethod.ToString() == "")
                    {
                        CallAuthorizeNetPayment(PaypalBillingAgreementID, PaypalPaymentTransactionID, CustomerProfileID, PaymentProfileID,UserSubscriptionID, PaymentMethod, BillingAmount, PaymentDate);
                    }
                }

                foreach (DataRow dtRow in dtPayPalPaymentRecords.Rows)
                {
                    string CustomerProfileID = dtRow["CustomerProfileID"].ToString();

                    string PaymentProfileID = dtRow["CustomerPaymentProfileID"].ToString();

                    decimal BillingAmount = Convert.ToDecimal(dtRow["BillingAmount"].ToString());

                    int UserSubscriptionID = Convert.ToInt32(dtRow["UserSubscriptionID"]);

                    string PaypalBillingAgreementID = dtRow["PaypalBillingAgreementID"].ToString();

                    string PaypalPaymentTransactionID = dtRow["PaypalPaymentTransactionID"].ToString();

                    string PaymentMethod = dtRow["PaymentMethod"].ToString();

                    string PaypalPayerMailID = dtRow["PaypalPayerMailID"].ToString();

                    DateTime PaymentDate = Convert.ToDateTime(dtRow["PaymentDate"]);

                    if (dtRow["PaymentMethod"].ToString() == "PayPal")
                    {
                        CallPayPalPayment(PaypalBillingAgreementID, PaypalPaymentTransactionID, CustomerProfileID, PaymentProfileID, PaymentMethod, BillingAmount, UserSubscriptionID, PaymentDate, PaypalPayerMailID);
                    }
                }



                sqlConnection.Close();

                LogExecutionComment(string.Empty, true);
                LogExecutionComment("Payment Processing Notification for the day Completed!!!", true);
                LogExecutionComment("---------------------------------------------------------", false);

                LogTransactionDatatoFile(LogFileData);



            }
            catch (SqlException ex)
            {
                LogException("PaymentNotification", "Processing Individual Payments Notification", ex.Message, ex.StackTrace, DateTime.Now);
                LogExecutionComment("Error: " + ex.Message, true);

            }


         
            

            //Console.ReadLine();
        }

        //New method has been created to get sum of all payments(Authorize.net) for each user and send it as one transaction by the end of the day. If a single user has used multiple cards to buy services at different times, then the sum would be calculated based on the card number as well on 18-Sep-2014
        private static void  CallAuthorizeNetPayment(string PaypalBillingAgreementID, string PaypalPaymentTransactionID, string CustomerProfileID, string PaymentProfileId,int UserSubscriptionID, string PaymentMethod, decimal PaymentAmount, DateTime PaymentDate)
        {
            try
            {

                LogExecutionComment("CallAuthorizeNetPayment Notification...", true);
                string PaymentDetailsMailContent = "";
                int UserProfileID = 0;
                if (PaymentMethod.ToString() == "Authorize.net" || PaymentMethod.ToString() == "")
                {
                    var getUserProID = objDBContext.CustomerPaymentProfiles.FirstOrDefault(a => a.AuthCustomerProfileID == CustomerProfileID);// Authorize.net
                    UserProfileID = Convert.ToInt32(getUserProID.UserProfileID);
                    LogExecutionComment("Authorize.net - Started", true);
                    #region Authorize.net

                    var usersubscriptions = objDBContext.AllAutomatedPayments.FirstOrDefault(A => A.CustomerProfileID == CustomerProfileID && A.CustomerPaymentProfileID == PaymentProfileId && A.PaymentDate == PaymentDate && A.PaymentStatus == "PaymentFailed" && A.UserSubscriptionID == UserSubscriptionID);

                    String InstanceNumber = string.Empty;
                    String ApplicationServer = string.Empty;
                    String SID = string.Empty;
                    String DeveloperKey = string.Empty;


                        var UserSubInfo = objDBContext.UserSubscribedServices.FirstOrDefault(u => u.UserSubscriptionID == UserSubscriptionID);
                        var ServiceDetails = objDBContext.ServiceCatalogs.FirstOrDefault(s => s.ServiceID == UserSubInfo.ServiceID);
                        var APP_URL = objDBContext.WftSettings.FirstOrDefault(a => a.SettingKey == "APP_URL");
                        var User = objDBContext.UserProfiles.FirstOrDefault(u => u.UserProfileID == UserProfileID);
                        DateTime ServicePurchaseDate = Convert.ToDateTime(UserSubInfo.ActiveDate);

                        var ServiceProvisioningCheck = objDBContext.ServiceProvisions.Where(sp => sp.ServiceID == ServiceDetails.ServiceID);
                        var ServiceProvisioningDEtails = objDBContext.ServiceProvisions.FirstOrDefault(sp => sp.ServiceCategoryID == ServiceDetails.ServiceCategoryID && sp.ServiceID == ServiceDetails.ServiceID);
                        if (ServiceProvisioningCheck.Count() > 0)
                        {
                            InstanceNumber = ServiceProvisioningDEtails.InstanceNumber;
                            if (InstanceNumber == null || InstanceNumber == "")
                            {
                                InstanceNumber = "N/A";
                            }
                            else
                            {
                                InstanceNumber = ServiceProvisioningDEtails.InstanceNumber;
                            }
                            ApplicationServer = ServiceProvisioningDEtails.ApplicationServer;
                            if (ApplicationServer == null || ApplicationServer == "")
                            {
                                ApplicationServer = "N/A";
                            }
                            else
                            {
                                ApplicationServer = ServiceProvisioningDEtails.ApplicationServer;
                            }
                            SID = ServiceProvisioningDEtails.UIDOnServer;
                            if (SID == null || SID == "")
                            {
                                SID = "N/A";
                            }
                            else
                            {
                                SID = ServiceProvisioningDEtails.UIDOnServer;
                            }
                            DeveloperKey = ServiceProvisioningDEtails.DeveloperKey;
                            if (DeveloperKey == null || DeveloperKey == "")
                            {
                                DeveloperKey = "To be purchased separately";
                            }
                            else
                            {
                                DeveloperKey = ServiceProvisioningDEtails.DeveloperKey;
                            }
                        }
                        else
                        {
                            InstanceNumber = (UserSubInfo.InstanceNumber != null ? UserSubInfo.InstanceNumber : " N/A ");
                            ApplicationServer = (UserSubInfo.ApplicationServer != null ? UserSubInfo.ApplicationServer : " N/A ");
                            SID = (UserSubInfo.UIDOnServer != null ? UserSubInfo.UIDOnServer : " N/A ");
                            DeveloperKey = UserSubInfo.DeveloperKey;
                            if (DeveloperKey == null || DeveloperKey == "")
                            {
                                DeveloperKey = "To be purchased separately";
                            }
                            else
                            {
                                DeveloperKey = UserSubInfo.DeveloperKey;
                            }
                        }
                      

                        PaymentDetailsMailContent = ("<table border='1'><tr><td rowspan='6'><strong>Subscription Information</strong></td>"
                                        + "<td><strong>Subscription ID </strong></td><td>" + UserSubInfo.UserSubscriptionID + "<br /></td></tr>"
                                        + "<tr><td><strong>User Name </strong></td><td>" + User.LastName + " " + User.FirstName + "<br /></td></tr>"
                                         + "<tr><td><strong>User Email </strong></td><td>" + User.EmailID + "<br /></td></tr>"
                                        + "<tr><td><strong>Payment For </strong></td><td>" + "Renewal Subscription" + "<br /></td></tr>"
                                        + "<tr><td><strong>Service Category </strong></td><td>" + ServiceDetails.SystemType + "<br /></td></tr>"
                                        + "<tr><td><strong>Service Name </strong></td><td>" + ServiceDetails.ServiceName + "<br /></td></tr>"
                                        + "<tr><td rowspan='8'><strong>SAP Credentials</strong></td><td><strong>Instance Number </strong></td><td>" + InstanceNumber + "<br /></td></tr>"
                                        + "<tr><td><strong>Application Server </strong></td><td>" + ApplicationServer + "<br />"
                                        + "</td></tr>"
                                        + "<tr><td><strong>SID </strong></td><td>" + SID + "<br /></td></tr>"
                                        + "<tr><td><strong>UserName </strong></td><td>" + UserSubInfo.ServiceUserName + "<br /></td></tr>"
                                        + "<tr><td><strong>Password </strong></td><td>" + UserSubInfo.ServicePassword + "<br /></td></tr>"
                                        + "<tr><td><strong>Developer Key </strong></td><td>" + DeveloperKey + "<br /></td></tr>" + (APP_URL != null ? ("<tr><td><strong>WebURL </strong></td><td><a href=" + APP_URL.SettingValue + " target='_blank'>" + "Click Here" + "</a>"
                                        + "</td></tr>") : "") + "</table>");

                        string PaymentMonth = PaymentDate.ToString("MMMM");

                        string EmailContent = File.ReadAllText("./EmailTemplates/Payment-Notification.html");
                        EmailContent = EmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                        string AdminContent = EmailContent.Replace("++PaymentDetails++", PaymentDetailsMailContent).Replace("++PaymentMonth++", PaymentMonth).Replace("++PaymentDate++", PaymentDate.ToString("dd-MMM-yy"));
                        SendSupportNotificationEmail(AdminContent, "System Lock " + User.LastName + " " + User.FirstName + " - Payment Failure ", false);


                        


                    LogExecutionComment("Authorize.net - Completed", true);
                    #endregion
                }
                


              

            }
            catch (Exception ex)
            {
                LogException("PaymentNotification", "CallAuthorizeNetPayment", ex.Message, ex.StackTrace, DateTime.Now);
                LogExecutionComment("Error while Processing: " + ex.Message, true);
            }
        }



         //Calling Method For PayPal Method for individual subscription
        private static void CallPayPalPayment(string PaypalBillingAgreementID, string PaypalPaymentTransactionID, string CustomerProfileID, string PaymentProfileId, string PaymentMethod, decimal PaymentAmount, int UserSubscriptionID, DateTime PaymentDate, string PaypalPayerMailID)
        {
            try
            {

                LogExecutionComment("CallPayPalPayment Notification...", true);
                decimal ReturnAmount = 0M;
                string PaymentDetailsMailContent = "";
                string UserSubscriptionIDs = "";
                int UserProfileID;
                var usrSubSer = objDBContext.UserSubscribedServices.FirstOrDefault(s => s.UserSubscriptionID == UserSubscriptionID);
                UserProfileID = Convert.ToInt32(usrSubSer.UserProfileID);
                if (PaymentMethod == "PayPal")
                {
                    UserSubscriptionIDs = "";
                    LogExecutionComment("PayPal - Notification Started", true);
                    #region Paypal
                    var UserpaymenTrans = objDBContext.UserPaymentTransactions.FirstOrDefault(a => a.PalpalPaymentTransactionID == PaypalPaymentTransactionID && a.PaypalBillingAgreementID == PaypalBillingAgreementID);
                 
                    if (UserpaymenTrans != null)
                    {

                            String InstanceNumber = string.Empty;
                            String ApplicationServer = string.Empty;
                            String SID = string.Empty;
                            String DeveloperKey = string.Empty;
                            var UserSubInfo = objDBContext.UserSubscribedServices.FirstOrDefault(u => u.UserSubscriptionID == UserSubscriptionID);
                            var ServiceDetails = objDBContext.ServiceCatalogs.FirstOrDefault(s => s.ServiceID == UserSubInfo.ServiceID);
                            var APP_URL = objDBContext.WftSettings.FirstOrDefault(a => a.SettingKey == "APP_URL");
                            var User = objDBContext.UserProfiles.FirstOrDefault(u => u.UserProfileID == UserProfileID);
                            DateTime ServicePurchaseDate = Convert.ToDateTime(UserSubInfo.ActiveDate);

                            var ServiceProvisioningCheck = objDBContext.ServiceProvisions.Where(sp => sp.ServiceID == ServiceDetails.ServiceID);
                            var ServiceProvisioningDEtails = objDBContext.ServiceProvisions.FirstOrDefault(sp => sp.ServiceCategoryID == ServiceDetails.ServiceCategoryID && sp.ServiceID == ServiceDetails.ServiceID);
                            if (ServiceProvisioningCheck.Count() > 0)
                            {
                                InstanceNumber = ServiceProvisioningDEtails.InstanceNumber;
                                if (InstanceNumber == null || InstanceNumber == "")
                                {
                                    InstanceNumber = "N/A";
                                }
                                else
                                {
                                    InstanceNumber = ServiceProvisioningDEtails.InstanceNumber;
                                }
                                ApplicationServer = ServiceProvisioningDEtails.ApplicationServer;
                                if (ApplicationServer == null || ApplicationServer == "")
                                {
                                    ApplicationServer = "N/A";
                                }
                                else
                                {
                                    ApplicationServer = ServiceProvisioningDEtails.ApplicationServer;
                                }
                                SID = ServiceProvisioningDEtails.UIDOnServer;
                                if (SID == null || SID == "")
                                {
                                    SID = "N/A";
                                }
                                else
                                {
                                    SID = ServiceProvisioningDEtails.UIDOnServer;
                                }
                                DeveloperKey = ServiceProvisioningDEtails.DeveloperKey;
                                if (DeveloperKey == null || DeveloperKey == "")
                                {
                                    DeveloperKey = "To be purchased separately";
                                }
                                else
                                {
                                    DeveloperKey = ServiceProvisioningDEtails.DeveloperKey;
                                }
                            }
                            else
                            {
                                InstanceNumber = (UserSubInfo.InstanceNumber != null ? UserSubInfo.InstanceNumber : " N/A ");
                                ApplicationServer = (UserSubInfo.ApplicationServer != null ? UserSubInfo.ApplicationServer : " N/A ");
                                SID = (UserSubInfo.UIDOnServer != null ? UserSubInfo.UIDOnServer : " N/A ");
                                DeveloperKey = UserSubInfo.DeveloperKey;
                                if (DeveloperKey == null || DeveloperKey == "")
                                {
                                    DeveloperKey = "To be purchased separately";
                                }
                                else
                                {
                                    DeveloperKey = UserSubInfo.DeveloperKey;
                                }
                            }


                            PaymentDetailsMailContent = ("<table border='1'><tr><td rowspan='6'><strong>Subscription Information</strong></td>"
                                            + "<td><strong>Subscription ID </strong></td><td>" + UserSubInfo.UserSubscriptionID + "<br /></td></tr>"
                                            + "<tr><td><strong>User Name </strong></td><td>" + User.LastName + " " + User.FirstName + "<br /></td></tr>"
                                             + "<tr><td><strong>User Email </strong></td><td>" + User.EmailID + "<br /></td></tr>"
                                            + "<tr><td><strong>Payment For </strong></td><td>" + "Renewal Subscription" + "<br /></td></tr>"
                                            + "<tr><td><strong>Service Category </strong></td><td>" + ServiceDetails.SystemType + "<br /></td></tr>"
                                            + "<tr><td><strong>Service Name </strong></td><td>" + ServiceDetails.ServiceName + "<br /></td></tr>"
                                            + "<tr><td rowspan='8'><strong>SAP Credentials</strong></td><td><strong>Instance Number </strong></td><td>" + InstanceNumber + "<br /></td></tr>"
                                            + "<tr><td><strong>Application Server </strong></td><td>" + ApplicationServer + "<br />"
                                            + "</td></tr>"
                                            + "<tr><td><strong>SID </strong></td><td>" + SID + "<br /></td></tr>"
                                            + "<tr><td><strong>UserName </strong></td><td>" + UserSubInfo.ServiceUserName + "<br /></td></tr>"
                                            + "<tr><td><strong>Password </strong></td><td>" + UserSubInfo.ServicePassword + "<br /></td></tr>"
                                            + "<tr><td><strong>Developer Key </strong></td><td>" + DeveloperKey + "<br /></td></tr>" + (APP_URL != null ? ("<tr><td><strong>WebURL </strong></td><td><a href=" + APP_URL.SettingValue + " target='_blank'>" + "Click Here" + "</a>"
                                            + "</td></tr>") : "") + "</table>");

                            string PaymentMonth = PaymentDate.ToString("MMMM");
                            string EmailContent = File.ReadAllText("./EmailTemplates/Payment-Notification.html");
                            EmailContent = EmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                            string AdminContent = EmailContent.Replace("++PaymentDetails++", PaymentDetailsMailContent).Replace("++PaymentMonth++", PaymentMonth).Replace("++PaymentDate++", PaymentDate.ToString("dd-MMM-yy"));
                            SendSupportNotificationEmail(AdminContent, "System Lock " + User.LastName + " " + User.FirstName + " - Payment Failure ", false);


                        

                       
                       
                    }
                    #endregion
                    LogExecutionComment("PayPal - Notification Completed", true);
                }
              


            }
            catch (Exception ex)
            {
                LogException("PaymentNotification", "CallPayPalPayment", ex.Message, ex.StackTrace, DateTime.Now);
                LogExecutionComment("Error while Processing: " + ex.Message, true);
            }
        }

        //public static string ConvertDataTableToHTML(DataTable dt)
        //{
        //    string html = "<table>";
        //    //add header row
        //    html += "<tr>";
        //    for (int i = 0; i < dt.Columns.Count; i++)
        //        html += "<td>" + dt.Columns[i].ColumnName + "</td>";
        //    html += "</tr>";
        //    //add rows
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        html += "<tr>";
        //        for (int j = 0; j < dt.Columns.Count; j++)
        //            html += "<td>" + dt.Rows[i][j].ToString() + "</td>";
        //        html += "</tr>";
        //    }
        //    html += "</table>";
        //    return html;
        //}

        public static void LogException(string pageNameOrClassName, string MethodName, string ExMessage, string ExStackTrace, DateTime ExcepDateTime)
        {
            //Log exception into database.

            SqlConnection sqlConnection = new SqlConnection();

            try
            {
                sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                sqlConnection.Open();

                SqlCommand command = new SqlCommand("InsertExceptionLog", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ExceptionClassName", SqlDbType.VarChar).Value = pageNameOrClassName;
                command.Parameters.Add("@ExceptionMessage", SqlDbType.VarChar).Value = ExMessage;
                command.Parameters.Add("@ExceptionStackTrace", SqlDbType.VarChar).Value = ExStackTrace;
                command.Parameters.Add("@ExceptionDateTime", SqlDbType.VarChar).Value = ExcepDateTime;
                command.Parameters.Add("@ExceptionMethodName", SqlDbType.VarChar).Value = MethodName;
                command.Parameters.Add("@UserName", SqlDbType.VarChar).Value = "Payment Notification";
                command.ExecuteNonQuery();

                sqlConnection.Close();
            }
            catch (SqlException)
            {
                //Ignore exception

            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }

       
        private static void LogExecutionComment(string Description, bool LogDate)
        {
            string tmpDescription = string.Empty;

            if (LogDate)
                tmpDescription = DateTime.Now.ToString() + " : " + Description;
            else
                tmpDescription = Description;

            Console.WriteLine(tmpDescription);

            LogFileData += tmpDescription + Environment.NewLine;
        }

        private static void LogTransactionDatatoFile(string LogDetails)
        {
            string fileName = String.Format("{0:yyyy-MM-dd}", DateTime.Now) + ".Log";
            string Folder = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";

            bool isExists = System.IO.Directory.Exists(Folder);

            if (!isExists)
                System.IO.Directory.CreateDirectory(Folder);

            string FullFilePath = Path.Combine(Folder, fileName);

            TextWriter txtWr = new StreamWriter(FullFilePath, true);
            txtWr.Write(LogDetails);
            txtWr.Close();

        }

        public static void SendEmail(string messageBody, string subject, string ToMail, bool sendInBCC, bool IsHtml)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"]);
                string FromMail = Convert.ToString(ConfigurationManager.AppSettings["SMTPUsernameAdminNotification"]);
                mail.From = new MailAddress(FromMail);
                if (!sendInBCC)
                    mail.To.Add(ToMail);
                else
                {
                    mail.Bcc.Add(ToMail);
                }
                mail.Subject = subject;
                mail.Body = messageBody;
                mail.IsBodyHtml = IsHtml;

                SmtpServer.Port = int.Parse(ConfigurationManager.AppSettings["SMTPPort"]);
                SmtpServer.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SMTPUsernameAdminNotification"], ConfigurationManager.AppSettings["SMTPPasswordAdminNotification"]);
                SmtpServer.EnableSsl = bool.Parse(ConfigurationManager.AppSettings["EnableSSL"]);

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                LogException("PaymentNotification", "SendEmail", ex.Message, ex.StackTrace, DateTime.Now);
            }

        }

        public static void SendAdminNotificationEmail(string messageBody, string subject, bool sendInBCC)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"]);
                string FromMail = Convert.ToString(ConfigurationManager.AppSettings["SMTPUsernameAdminNotification"]);
                mail.From = new MailAddress(FromMail);
                string ToMail = objDBContext.WftSettings.FirstOrDefault(s => s.SettingKey == "ADMIN_EMAIL").SettingValue;
                if (!sendInBCC)
                    mail.To.Add(ToMail);
                else
                {
                    mail.Bcc.Add(ToMail);
                }
                mail.Subject = subject;
                mail.Body = messageBody;
                mail.IsBodyHtml = true;
                SmtpServer.Port = int.Parse(ConfigurationManager.AppSettings["SMTPPort"]);
                SmtpServer.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SMTPUsernameAdminNotification"], ConfigurationManager.AppSettings["SMTPPasswordAdminNotification"]);
                SmtpServer.EnableSsl = bool.Parse(ConfigurationManager.AppSettings["EnableSSL"]);

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                LogException("PaymentNotification", "SendAdminNotificationEmail", ex.Message, ex.StackTrace, DateTime.Now);
            }
        }
        public static void SendSupportNotificationEmail(string messageBody, string subject, bool sendInBCC)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"]);
                string FromMail = Convert.ToString(ConfigurationManager.AppSettings["SMTPUsernameAdminNotification"]);
                mail.From = new MailAddress(FromMail);
                string ToMail = objDBContext.WftSettings.FirstOrDefault(s => s.SettingKey == "SUPPORT_MAIL").SettingValue;
                string CcMail = objDBContext.WftSettings.FirstOrDefault(s => s.SettingKey == "ADMIN_EMAIL").SettingValue;
                if (!sendInBCC)
                    mail.To.Add(ToMail);
                else
                {
                    mail.Bcc.Add(ToMail);
                }
                mail.CC.Add(CcMail);
                mail.Subject = subject;
                mail.Body = messageBody;
                mail.IsBodyHtml = true;
                SmtpServer.Port = int.Parse(ConfigurationManager.AppSettings["SMTPPort"]);
                SmtpServer.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SMTPUsernameAdminNotification"], ConfigurationManager.AppSettings["SMTPPasswordAdminNotification"]);
                SmtpServer.EnableSsl = bool.Parse(ConfigurationManager.AppSettings["EnableSSL"]);

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                LogException("PaymentNotification", "SendAdminNotificationEmail", ex.Message, ex.StackTrace, DateTime.Now);
            }
        }
       
    }
}

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

                DataTable dtCancellationRecords = new DataTable();
               

                sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                sqlConnection.Open();

                SqlCommand Authorizecommand = new SqlCommand("pr_GetTodaysCancellationNotification", sqlConnection);
                Authorizecommand.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader AuthorizesqlData = Authorizecommand.ExecuteReader();
                dtCancellationRecords.Load(AuthorizesqlData);
                AuthorizesqlData.Close();


                LogExecutionComment("Processing Individual Cancellation Notification...", true);

                foreach (DataRow dtRow in dtCancellationRecords.Rows)
                {
                   
                    int UserSubscriptionID = Convert.ToInt32(dtRow["UserSubscriptionID"].ToString());



                    if (UserSubscriptionID != null)
                    {
                        CallFailedPaymentNotification(UserSubscriptionID);
                    }
                }

                



                sqlConnection.Close();

                LogExecutionComment(string.Empty, true);
                LogExecutionComment("Payment Cancellation Notification for the day Completed!!!", true);
                LogExecutionComment("---------------------------------------------------------", false);

                LogTransactionDatatoFile(LogFileData);



            }
            catch (SqlException ex)
            {
                LogException("CancellationNotification", "Processing Individual Cancellation Notification", ex.Message, ex.StackTrace, DateTime.Now);
                LogExecutionComment("Error: " + ex.Message, true);

            }


         
            

            //Console.ReadLine();
        }

        //New method has been created to get sum of all payments(Authorize.net) for each user and send it as one transaction by the end of the day. If a single user has used multiple cards to buy services at different times, then the sum would be calculated based on the card number as well on 18-Sep-2014
        private static void  CallFailedPaymentNotification(int UserSubscriptionID)
        {
            try
            {
                CancellationProcess(UserSubscriptionID);

            }
            catch (Exception ex)
            {
                LogException("CancellationNotification", "CallFailedPaymentNotification", ex.Message, ex.StackTrace, DateTime.Now);
                LogExecutionComment("Error while Processing: " + ex.Message, true);
            }
        }



       
        private static void CancellationProcess(int UserSubscriptionID)
        {
            try
            {

                int UserSubscripID = Convert.ToInt32(UserSubscriptionID);
                var UserServ = objDBContext.UserSubscribedServices.FirstOrDefault(ot => ot.UserSubscriptionID == UserSubscripID);
                var UserProfile = objDBContext.UserProfiles.FirstOrDefault(u => u.UserProfileID == UserServ.UserProfileID);
                if (UserServ != null)
                {


                    if (UserServ.ServiceCategoryID == 2 && UserServ.RecordStatus == 1)
                    {
                        var UnSubscribe = objDBContext.pr_CancelUserSubscription(UserServ.UserSubscriptionID, UserProfile.EmailID, "Due to non-payment, subscription has been locked and unsubscribed", "Service cancelled", "10");

                    //string UnsubscribeDetails = "";

                    int serv = UserServ.ServiceID;
                    int Ctgry = UserServ.ServiceCategoryID;
                    var service = objDBContext.ServiceCatalogs.FirstOrDefault(s => s.ServiceID == serv);
                    var category = objDBContext.ServiceCategories.FirstOrDefault(c => c.ServiceCategoryID == Ctgry);
                    var ServiceProvisioningDEtails = objDBContext.ServiceProvisions.FirstOrDefault(sp => sp.ServiceCategoryID == Ctgry && sp.ServiceID == serv);
                    string message = ((UserProfile.LastName + "   " + UserProfile.FirstName));
                    string Username = ((UserProfile.EmailID));
                    string servicetype = ((service.UsageUnit));
                    string servicename = ((service.ServiceName));
                    string InstanceNumber = string.Empty;
                    string ApplicationServer = string.Empty;
                    string SID = string.Empty;

                    string servicecategoryname = ((category.CategoryName));
                    string serviceusername = ((UserServ.ServiceUserName != null ? (UserServ.ServiceUserName != "" ? UserServ.ServiceUserName : " - ") : " - "));
                    string serviceurl = (UserServ.ServiceUrl != null ? (UserServ.ServiceUrl != "" ? ("<a target='_blank' href='" + UserServ.ServiceUrl + "'>" + UserServ.ServiceUrl + "</a>") : " N/A ") : " N/A ");
                    string serviceotherinformation = ((UserServ.ServiceOtherInformation != null ? (UserServ.ServiceOtherInformation != "" ? UserServ.ServiceOtherInformation : " N/A ") : " N/A " + "<br />"));
                    string UIDOnServer = UserServ.UIDOnServer != null ? (UserServ.UIDOnServer != "" ? UserServ.UIDOnServer : " N/A ") : " N/A ";
                    string ActivatedDate = Convert.ToDateTime(UserServ.ActiveDate).ToString("dd-MMM-yyyy");

                   

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
                    string EmailContent = File.ReadAllText("./EmailTemplates/Cancellation-AdminNotification.html");


                    EmailContent = EmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);


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
                                    + "<tr><td>" + "Due to non-payment, subscription has been locked and unsubscribed" + "<br /></td></tr></table>");

                    FullContent += ("<table><tr><td><strong>Feedback:</strong></td></tr>"
                                    + "<tr><td>" + "Service cancelled" + "<br /></td></tr></table>");

                    FullContent += ("<table><tr><td><strong>Service Rating:</strong></td></tr>"
                                    + "<tr><td>" + "10"
                                    + "<br /></td></tr></table>");

                    string AdminContent = EmailContent.Replace("++AddContentHere++", FullContent).Replace("++name++", message);

                    string UserEmailContent = File.ReadAllText("./EmailTemplates/Cancellation-UserNotification.html");
                    UserEmailContent = UserEmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                    string UserContent = UserEmailContent.Replace("++AddContentHere++", FullContent).Replace("++name++", message);

                    //AdminContent = EmailContent.
                    SendAdminNotificationEmail(AdminContent, "Non-Payment - un-subscribed after locking - " + UserProfile.FirstName + UserProfile.MiddleName + UserProfile.LastName, false);
                    SendEmail(UserContent, "Non-Payment - un-subscribed after locking - " + UserProfile.FirstName + " " + UserProfile.MiddleName + " " + UserProfile.LastName, UserProfile.EmailID, false, true);
                    SendSupportNotificationEmail(AdminContent, "Non-Payment - un-subscribed after locking - " + UserProfile.FirstName + UserProfile.MiddleName + UserProfile.LastName, false);
                    SendSAPBasisNotificationEmail(AdminContent, "Non-Payment - un-subscribed after locking - " + UserProfile.FirstName + UserProfile.MiddleName + UserProfile.LastName, false);
                    }
                }
               
            }
            catch (Exception ex)
            {
                LogException("CancellationProcess", "CancellationProcess", ex.Message, ex.StackTrace, DateTime.Now);
            }
        }


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
                LogException("CancellationProcess", "SendEmail", ex.Message, ex.StackTrace, DateTime.Now);
            }

        }
      
        public static void SendSAPBasisNotificationEmail(string messageBody, string subject, bool sendInBCC)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"]);
                string FromMail = Convert.ToString(ConfigurationManager.AppSettings["SMTPUsernameAdminNotification"]);
                mail.From = new MailAddress(FromMail);
                string ToMail = objDBContext.WftSettings.FirstOrDefault(s => s.SettingKey == "SAPBASIS_MAIL").SettingValue;
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
                LogException("CancellationProcess", "SendAdminNotificationEmail", ex.Message, ex.StackTrace, DateTime.Now);
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
                LogException("CancellationProcess", "SendAdminNotificationEmail", ex.Message, ex.StackTrace, DateTime.Now);
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
                LogException("CancellationProcess", "SendAdminNotificationEmail", ex.Message, ex.StackTrace, DateTime.Now);
            }
        }
       
    }
}

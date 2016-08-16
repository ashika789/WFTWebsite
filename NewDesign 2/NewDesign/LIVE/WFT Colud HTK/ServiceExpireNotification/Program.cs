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


namespace ServiceExpireNotification
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
            LogExecutionComment("Payment Processing Started...", true);
            LogExecutionComment(string.Empty, true);

            try
            {

                DataTable dtAuthorizePaymentRecords = new DataTable();
               
                sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                sqlConnection.Open();

                SqlCommand Authorizecommand = new SqlCommand("pr_GetGroupOfTodaysPaymentExpiredService", sqlConnection);
                Authorizecommand.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader AuthorizesqlData = Authorizecommand.ExecuteReader();
                dtAuthorizePaymentRecords.Load(AuthorizesqlData);
                AuthorizesqlData.Close();

              

                LogExecutionComment("Processing Individual Service Check...", true);

                foreach (DataRow dtRow in dtAuthorizePaymentRecords.Rows)
                {
                    int UserSubscriptionID = Convert.ToInt32(dtRow["UserSubscriptionID"]);



                    if (UserSubscriptionID != null)
                    {
                        CallServiceExpireNotification(UserSubscriptionID);
                    }
                }

              



                sqlConnection.Close();

                LogExecutionComment(string.Empty, true);
                LogExecutionComment("Payment Processing for the day Completed!!!", true);
                LogExecutionComment("---------------------------------------------------------", false);

                LogTransactionDatatoFile(LogFileData);



            }
            catch (SqlException ex)
            {
                LogException("PaymentProcessor", "Processing Individual Payments", ex.Message, ex.StackTrace, DateTime.Now);
                LogExecutionComment("Error: " + ex.Message, true);

            }


            LogExecutionComment("---------------------------------------------------------", false);
            LogExecutionComment("De-Activate ExpiredSubscription Processing Started...", true);
            LogExecutionComment(string.Empty, true);

            try
            {

                DataTable ExpiredSubscription = new DataTable();

                sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                sqlConnection.Open();

                SqlCommand command = new SqlCommand("pr_DeActivateExpiredSubscription", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader sqlData = command.ExecuteReader();
                ExpiredSubscription.Load(sqlData);
                sqlData.Close();

                sqlConnection.Close();

                LogExecutionComment(string.Empty, true);
                LogExecutionComment("De-Activate ExpiredSubscription Processing for the day Completed!!!", true);
                LogExecutionComment("---------------------------------------------------------", false);

                LogTransactionDatatoFile(LogFileData);



            }
            catch (SqlException ex)
            {
                LogException("PaymentProcessor", "De-Activate ExpiredSubscription", ex.Message, ex.StackTrace, DateTime.Now);
                LogExecutionComment("Error: " + ex.Message, true);
            }

            //Console.ReadLine();
        }
        public static DateTime NextMonth(DateTime date)
        {
            if (date.Day != DateTime.DaysInMonth(date.Year, date.Month))
                return date.AddMonths(1);
            else
                return date.AddDays(1).AddMonths(1).AddDays(-1);
        }
        //New method has been created to get sum of all payments(Authorize.net) for each user and send it as one transaction by the end of the day. If a single user has used multiple cards to buy services at different times, then the sum would be calculated based on the card number as well on 18-Sep-2014
        private static void CallServiceExpireNotification(int UserSubscriptionID)
        {
            try
            {

                LogExecutionComment("CallAuthorizeNetPayment...", true);
                //string BillingAddressID = GetBillingAddressID(PaymentProfileId);
             
              
                string PaymentDetailsMailContent = "";
             
                string UserSubscriptionIDs = "";
                int UserProfileID = 0;



               
                LogExecutionComment("Authorize.net - Started", true);




                var SubscriptionTotal = objDBContext.pr_GetPaymentExpiredServiceTotal(UserSubscriptionID);

                var ServiceTotal = SubscriptionTotal.First();
                if (ServiceTotal == 1)
                {
                    var getUserProID = objDBContext.UserSubscribedServices.FirstOrDefault(a => a.UserSubscriptionID == UserSubscriptionID);
                    UserProfileID = Convert.ToInt32(getUserProID.UserProfileID);

                    var UserInfo = objDBContext.UserProfiles.FirstOrDefault(u => u.UserProfileID == UserProfileID);
                    
                    string Content = "";
                    string PaymentStatus = "";
                    string ContactAdmin = "";

                    string Dear = "";


                    String InstanceNumber = string.Empty;
                    String ApplicationServer = string.Empty;
                    String SID = string.Empty;
                    String DeveloperKey = string.Empty;
                    String ServiceOtherInformation = string.Empty;

                    var UserSubInfo = objDBContext.UserSubscribedServices.FirstOrDefault(u => u.UserSubscriptionID == UserSubscriptionID);
                    var ServiceDetails = objDBContext.ServiceCatalogs.FirstOrDefault(s => s.ServiceID == UserSubInfo.ServiceID);
                    var APP_URL = objDBContext.WftSettings.FirstOrDefault(a => a.SettingKey == "APP_URL");
                    var UserRecurringInfo = objDBContext.AllAutomatedPayments.FirstOrDefault(a => a.UserSubscriptionID == UserSubscriptionID);
                    var User = objDBContext.UserProfiles.FirstOrDefault(u => u.UserProfileID == UserProfileID);
                    DateTime NextBillDate = NextMonth(DateTime.Now);
                    DateTime ServicePurchaseDate = Convert.ToDateTime(UserSubInfo.ActiveDate);

                    var ServiceProvisioningCheck = objDBContext.ServiceProvisions.Where(sp => sp.ServiceID == ServiceDetails.ServiceID);
                    var ServiceProvisioningDEtails = objDBContext.ServiceProvisions.FirstOrDefault(sp => sp.ServiceCategoryID == ServiceDetails.ServiceCategoryID && sp.ServiceID == ServiceDetails.ServiceID);
                    if (ServiceProvisioningCheck.Count() > 0)
                    {
                        InstanceNumber = ServiceProvisioningDEtails.InstanceNumber;
                        ApplicationServer = ServiceProvisioningDEtails.ApplicationServer;
                        SID = ServiceProvisioningDEtails.UIDOnServer;
                        DeveloperKey = ServiceProvisioningDEtails.DeveloperKey;
                        ServiceOtherInformation = ServiceProvisioningDEtails.AdditionalInformation;
                    }
                    else
                    {

                        InstanceNumber = (UserSubInfo.InstanceNumber != null ? UserSubInfo.InstanceNumber : " - ");
                        ApplicationServer = (UserSubInfo.ApplicationServer != null ? UserSubInfo.ApplicationServer : " - ");
                        SID = (UserSubInfo.UIDOnServer != null ? UserSubInfo.UIDOnServer : " - ");
                        DeveloperKey = UserSubInfo.DeveloperKey;
                        if (DeveloperKey == null || DeveloperKey == "")
                        {
                            DeveloperKey = "To be purchased separately";
                        }
                        else
                        {
                            DeveloperKey = UserSubInfo.DeveloperKey;
                        }
                        ServiceOtherInformation = (UserSubInfo.ServiceOtherInformation != null ? UserSubInfo.ServiceOtherInformation : " - ");
                    }

                    PaymentDetailsMailContent = ("<table border='1'><tr><td rowspan='6'><strong>Subscription Information</strong></td>"
                                    + "<td><strong>Subscription ID </strong></td><td>" + UserSubscriptionID + "<br /></td></tr>"
                                    + "<tr><td><strong>User Name </strong></td><td>" + UserInfo.LastName + " " + UserInfo.FirstName + "<br /></td></tr>"
                                     + "<tr><td><strong>User Email </strong></td><td>" + UserInfo.EmailID + "<br /></td></tr>"
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

                    PaymentDetailsMailContent += ("<table><tr><td><strong>Service Other Information:</strong></td></table>");

                    PaymentDetailsMailContent += ("<table border='1'><tr><td>" + ServiceOtherInformation + "<br /></td></tr></table>");

                    ContactAdmin = "";
                    Dear = "Admin,";

                    string EmailContent = File.ReadAllText("./EmailTemplates/ServiceExpire-Notification.html");
                    EmailContent = EmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                    string AdminContent = EmailContent.Replace("++dear++", Dear)
                                        .Replace("++Content++", Content).Replace("++PaymentStatus++", PaymentStatus)
                                        .Replace("++ContactAdmin++", ContactAdmin)
                                        .Replace("++PaymentDetails++", PaymentDetailsMailContent);
                    SendAdminNotificationEmail(AdminContent, "Service Expire remainder of " + User.LastName + " " + User.FirstName, false);


                    //string UserEmailContent = File.ReadAllText("./EmailTemplates/Payment-Details.html");
                    //UserEmailContent = UserEmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                    //string UserContent = UserEmailContent.Replace("++dear++", User.LastName + "," + User.FirstName)
                    //                   .Replace("++Content++", CustomerContent)
                    //                   .Replace("++PaymentStatus++", PaymentStatus).Replace("++ContactAdmin++", ContactAdmin)
                    //                   .Replace("++PaymentDetails++", PaymentDetailsMailContent);
                    //SendEmail(UserContent, "WFT Cloud : Your Monthly Payment Notification", User.EmailID, false, true);

                }

                    LogExecutionComment("Authorize.net - Completed", true);
                  




              
                LogExecutionComment("CallAuthorizeNetPayment...", false);




            }
            catch (Exception ex)
            {
                LogException("PaymentProcessor", "CallAuthorizeNetPayment", ex.Message, ex.StackTrace, DateTime.Now);
                LogExecutionComment("Error while Processing: " + ex.Message, true);
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
                command.Parameters.Add("@UserName", SqlDbType.VarChar).Value = "Payment Processor";
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
                LogException("PaymentProcessor", "SendEmail", ex.Message, ex.StackTrace, DateTime.Now);
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
                LogException("PaymentProcessor", "SendAdminNotificationEmail", ex.Message, ex.StackTrace, DateTime.Now);
            }
        }

      
    }
}

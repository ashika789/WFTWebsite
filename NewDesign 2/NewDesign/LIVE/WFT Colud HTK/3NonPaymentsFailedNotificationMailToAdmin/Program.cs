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

namespace _3NonPaymentsFailedNotificationMailToAdmin
{
    class Program
    {
        static string LogFileData = string.Empty;
        static cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();
        static void Main(string[] args)
        {

            try
            {
                LogExecutionComment("---------------------------------------------------------", false);
                LogExecutionComment("_3 Non Payments Failed Notification Mail To Admin Exe Started...", true);
                LogExecutionComment(string.Empty, true);

                var paymentsFailed = objDBContext.pr_AutomatedPaymentsFailedForCurrentActiveServices().ToList();

                var distinctUserSubscriptionID = paymentsFailed.Select(a => a.UserSubscriptionID).Distinct();

                foreach (var res1 in distinctUserSubscriptionID)
                {
                    string Message = "";
                    string Content = "";
                    bool sendMail = false;
                    var result = paymentsFailed.Where(a => a.UserSubscriptionID == res1).OrderByDescending(a=>a.AllPaymentID).ToList();
                    List<pr_AutomatedPaymentsFailedForCurrentActiveServices_Result> APFCA = new List<pr_AutomatedPaymentsFailedForCurrentActiveServices_Result>();

                    if (result.Count() >= 3)
                    {
                        long CheckPaymentID = 0;
                        int checkCount = 0;
                        foreach (var reslt in result)
                        {
                            if (checkCount == 0)
                            {
                                CheckPaymentID = reslt.AllPaymentID;
                            }
                            if (result.Where(A => A.AllPaymentID == CheckPaymentID).Count() != 0)
                            {
                                CheckPaymentID -= 1;
                                checkCount += 1;
                                APFCA.Add(reslt);
                            }
                            else
                            {
                                if (checkCount < 3)
                                {
                                    checkCount = 0;
                                    APFCA = new List<pr_AutomatedPaymentsFailedForCurrentActiveServices_Result>();
                                }
                            }
                            if (checkCount >= 3)
                            {
                                sendMail = true;
                                //break;
                            }
                        }
                    }
                    if (sendMail == true)
                    {
                        long CustomerProfileID = Convert.ToInt64(result.First().UserProfileID);
                        int ServiceCategoryID = Convert.ToInt32(result.First().ServiceCategoryID);
                        string CategoryName = objDBContext.ServiceCategories.FirstOrDefault(a => a.ServiceCategoryID == ServiceCategoryID) != null ?objDBContext.ServiceCategories.FirstOrDefault(a => a.ServiceCategoryID == ServiceCategoryID).CategoryName:"-";
                        int ServiceID = Convert.ToInt32(result.First().ServiceID);
                        string ServiceName = objDBContext.ServiceCatalogs.FirstOrDefault(a => a.ServiceID == ServiceID) != null ? objDBContext.ServiceCatalogs.FirstOrDefault(a => a.ServiceID == ServiceID).ServiceName : "-";
                        var Customer = objDBContext.UserProfiles.FirstOrDefault(a => a.UserProfileID == CustomerProfileID);
                        Message = " Please find below​, a serious of Continuous Subscribed Payment Failure for 3 or more consecutive cycles , for one of our current active User :<br/> <br/>";
                        Message += "<table cellpadding='3' cellspacing='0' width='100%' align='center' border='0' style='margin-top:-10px;'>";
                        Message += "<tr valign='top'>";
                        Message += "<td><strong>User Full Name</strong></td>";
                        Message += "<td><strong>:</strong></td>";
                        Message += "<td>"+Customer.FirstName + " " + Customer.MiddleName + " " + Customer.LastName+"("+Customer.EmailID+")</td>";
                        Message += "</tr>";
                        Message += "<tr valign='top'>";
                        Message += "<td><strong>Subscription ID</strong></td>";
                        Message += "<td><strong>:</strong></td>";
                        Message += "<td>" +res1+ "</td>";
                        Message += "</tr>";
                        Message += "<tr valign='top'>";
                        Message += "<td><strong>Category Name</strong></td>";
                        Message += "<td><strong>:</strong></td>";
                        Message += "<td>" + CategoryName + "</td>";
                        Message += "</tr>";
                        Message += "<tr valign='top'>";
                        Message += "<td><strong>Service Name</strong></td>";
                        Message += "<td><strong>:</strong></td>";
                        Message += "<td>"+ ServiceName+ "</td>";
                        Message += "</tr>";
                        Message += "<tr valign='top'>";
                        Message += "<td><strong>Payment Method</strong></td>";
                        Message += "<td><strong>:</strong></td>";
                        Message += "<td>" + result.First().PaymentMethod == "PayPal" ? "PayPal" : "Authorize.net" + "</td>";
                        Message += "</tr>";
                        Message += "</table>";

                        foreach (var res2 in APFCA.OrderByDescending(A=>A.AllPaymentID))
                        {
                            Content += "<hr/>";
                            Content += "<table cellpadding='3' cellspacing='0' width='100%' align='center' border='0' style='margin-top:-10px;'>";

                            Content += "<tr valign='top'>";
                            Content += "<td style='width:40.5%'><strong>Payment ID</strong></td>";
                            Content += "<td  style='width:0.5%'>:</td>";
                            Content += "<td  style='width:59%'>" + IsValid(res2.AllPaymentID.ToString()) + "</td>";
                            Content += "</tr>";

                            Content += "<tr valign='top'>";
                            Content += "<td style='width:40.5%'><strong>Payment Date</strong></td>";
                            Content += "<td  style='width:0.5%'>:</td>";
                            Content += "<td  style='width:59%'>" + IsValid(res2.PaymentDate.ToString("dd-MMM-yyyy"))+ "</td>";
                            Content += "</tr>";

                            Content += "<tr valign='top'>";
                            Content += "<td style='width:40.5%'><strong>Payment Status</strong></td>";
                            Content += "<td  style='width:0.5%'>:</td>";
                            Content += "<td  style='width:59%'>" + IsValid(res2.PaymentStatus) + "</td>";
                            Content += "</tr>";

                            Content += "<tr valign='top'>";
                            Content += "<td style='width:40.5%'><strong>Amount</strong></td>";
                            Content += "<td  style='width:0.5%'>:</td>";
                            Content += "<td  style='width:59%'>" + IsValid(res2.CurrentMonthBilling.ToString()) + "</td>";
                            Content += "</tr>";
                            
                            if (result.First().PaymentMethod == "PayPal")
                            {
                                Content += "<tr valign='top'>";
                                Content += "<td style='width:40.5%'><strong>Payer Mail ID</strong></td>";
                                Content += "<td  style='width:0.5%'>:</td>";
                                Content += "<td  style='width:59%'>" + IsValid(res2.PaypalPayerMailID) +"</td>";
                                Content += "</tr>";

                                Content += "<tr valign='top'>";
                                Content += "<td style='width:40.5%'><strong>Payer ID</strong></td>";
                                Content += "<td  style='width:0.5%'>:</td>";
                                Content += "<td  style='width:59%'>" + IsValid(res2.PaypalPayerID) + "</td>";
                                Content += "</tr>";

                                Content += "<tr valign='top'>";
                                Content += "<td style='width:40.5%'><strong>Billing Agreement ID</strong></td>";
                                Content += "<td  style='width:0.5%'>:</td>";
                                Content += "<td  style='width:59%'>" + IsValid(res2.PaypalBillingAgreementID )+"</td>";
                                Content += "</tr>";
                            }
                            else
                            {
                                Content += "<tr valign='top'>";
                                Content += "<td style='width:40.5%'><strong>Card Number</strong></td>";
                                Content += "<td  style='width:0.5%'>:</td>";
                                Content += "<td  style='width:59%'>" + IsValid(res2.CardNumber) +"</td>";
                                Content += "</tr>";

                                Content += "<tr valign='top'>";
                                Content += "<td style='width:40.5%'><strong>Auth Message</strong></td>";
                                Content += "<td  style='width:0.5%'>:</td>";
                                Content += "<td  style='width:59%'>" + IsValid(res2.Message) + "</td>";
                                Content += "</tr>";

                                Content += "<tr valign='top'>";
                                Content += "<td style='width:40.5%'><strong>Auth Response Code</strong></td>";
                                Content += "<td  style='width:0.5%'>:</td>";
                                Content += "<td  style='width:59%'>" + IsValid(res2.ResponseCode) + "</td>";
                                Content += "</tr>";
                            }


                            Content += "</table>";
                        }
                        if (sendMail == true)
                        {
                            string EmailContent = File.ReadAllText("./EmailTemplates/3NonPaymentsFailedNotificationMailToAdmin.html");
                            EmailContent = EmailContent.Replace("%DomainName%", ConfigurationManager.AppSettings["DomainName"]);
                            EmailContent = EmailContent.Replace("++Message++", Message).Replace("++Content++", Content);
                            SendAdminNotificationEmail(EmailContent, "Non Payment Failed Notification For User Subscription ID - " + res1.ToString(), false);
                        }
                    }

                }
                

                LogExecutionComment("---------------------------------------------------------", false);
                LogExecutionComment("_3 Non Payments Failed Notification Mail To Admin Exe Ended...", true);
                LogExecutionComment(string.Empty, true);

                LogTransactionDatatoFile(LogFileData);
            }
            catch (Exception ex)
            {

                LogException("3-NonPaymentsFailedNotificationMailToAdmin-Exe", "Main", ex.Message, ex.StackTrace, DateTime.Now);
                LogExecutionComment("Error: " + ex.Message, true);
                LogTransactionDatatoFile(LogFileData);
            }
        }


        #region Reusable funcations

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
                command.Parameters.Add("@UserName", SqlDbType.VarChar).Value = "Automated ExLog Notification Exe";
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

        public static string IsValid(string str)
        {
            if (string.IsNullOrEmpty(str))
                return "-";
            else
                return str;
        }

        #endregion
    }
}
